#!/usr/bin/env node
// Chapter Markdown is authoritative. No network access is used by this script.
const fs = require('node:fs');
const path = require('node:path');
const crypto = require('node:crypto');
const { createRequire } = require('node:module');
const root = path.resolve(__dirname, '../..');
const args = process.argv.slice(2);
const option = name => { const i = args.indexOf(name); return i < 0 ? undefined : args[i + 1]; };
const check = args.includes('--check');
const publication = args.includes('--publication');
const hash = value => crypto.createHash('sha256').update(value).digest('hex');
const read = file => fs.readFileSync(path.join(root, file), 'utf8');
const write = (file, content) => {
  const target = path.join(root, file);
  fs.mkdirSync(path.dirname(target), { recursive: true });
  if (!fs.existsSync(target) || fs.readFileSync(target, 'utf8') !== content) fs.writeFileSync(target, content);
};
const config = JSON.parse(read('diagrams/config.json'));
const configHash = hash(JSON.stringify(config));

// Respect outer fences: literal Mermaid examples inside Markdown code are not figures.
function extract(text) {
  const lines = text.replace(/^\uFEFF/, '').split(/\r?\n/);
  const blocks = [];
  let fence = null;
  let heading = '';
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];
    if (fence) {
      const close = line.match(/^ {0,3}(`+|~+)\s*$/);
      if (close && close[1][0] === fence.mark[0] && close[1].length >= fence.mark.length) {
        if (fence.lang === 'mermaid') blocks.push({
          start: fence.start, end: i, line: fence.start + 1, title: fence.heading,
          body: lines.slice(fence.start + 1, i).join('\n').trim() + '\n'
        });
        fence = null;
      }
    } else {
      if (/^#{1,6} /.test(line)) heading = line.replace(/^#+\s*/, '');
      const open = line.match(/^ {0,3}(`{3,}|~{3,})(.*)$/);
      if (open) fence = { mark: open[1], lang: open[2].trim().split(/\s/)[0].toLowerCase(), start: i, heading };
    }
  }
  if (fence) throw Error(`Unclosed fence at line ${fence.start + 1}`);
  return { lines, blocks };
}

async function main() {
  const chapters = fs.readdirSync(path.join(root, 'book')).filter(f => /^\d.*\.md$/.test(f) && !/\.edited\.md$/.test(f)).sort();
  const figures = [];
  const documents = [];
  for (const name of chapters) {
    const file = `book/${name}`;
    const parsed = extract(read(file));
    parsed.blocks.forEach((block, i) => {
      const stem = `ch${name.slice(0, -3)}-${i + 1}`;
      figures.push({ chapter: file, index: i + 1, line: block.line, title: block.title,
        source: `diagrams/source/${stem}.mmd`, svg: `diagrams/exported/${stem}.svg`,
        sourceSha256: hash(block.body), body: block.body });
    });
    documents.push({ file, ...parsed });
  }
  const manifestPath = 'diagrams/manifest.json';
  const previous = fs.existsSync(path.join(root, manifestPath)) ? JSON.parse(read(manifestPath)) : null;
  const old = new Map((previous?.figures || []).map(f => [f.source, f]));
  if (check) {
    const errors = [];
    if (!previous || previous.configSha256 !== configHash) errors.push('Missing manifest or changed renderer configuration');
    if (previous?.pipelineSha256 !== hash(fs.readFileSync(__filename))) errors.push('Export script changed; regenerate assets');
    if (previous?.figures.length !== figures.length) errors.push('Figure count changed');
    for (const f of figures) {
      const before = old.get(f.source);
      if (!before || before.sourceSha256 !== f.sourceSha256 || before.line !== f.line || before.title !== f.title) errors.push(`${f.chapter}:${f.line}: stale manifest`);
      if (!fs.existsSync(path.join(root, f.source)) || hash(fs.readFileSync(path.join(root, f.source))) !== f.sourceSha256) errors.push(`Missing/stale source: ${f.source}`);
      if (!before || !fs.existsSync(path.join(root, f.svg)) || hash(fs.readFileSync(path.join(root, f.svg))) !== before.svgSha256) errors.push(`Missing/changed export: ${f.svg}`);
    }
    const expected = new Set(figures.flatMap(f => [f.source, f.svg]));
    for (const dir of ['source', 'exported']) for (const name of fs.readdirSync(path.join(root, 'diagrams', dir))) {
      if (/\.(mmd|svg)$/.test(name) && !expected.has(`diagrams/${dir}/${name}`)) errors.push(`Unmapped asset: diagrams/${dir}/${name}`);
    }
    if (errors.length) throw Error(errors.join('\n'));
    console.log(`PASS: ${figures.length} chapter blocks, sources, exports and hashes agree.`);
  } else {
    const mermaidPath = option('--mermaid-js') || process.env.MERMAID_JS;
    if (!mermaidPath || !fs.existsSync(mermaidPath)) throw Error('Supply --mermaid-js PATH to a local Mermaid browser bundle (or MERMAID_JS).');
    const rendererHash = hash(fs.readFileSync(mermaidPath));
    const modulePath = option('--playwright-module') || process.env.PLAYWRIGHT_MODULE;
    const playwright = modulePath ? require(path.resolve(modulePath)) : createRequire(__filename)('playwright');
    const browser = await playwright.chromium.launch({ channel: option('--browser') || 'msedge', headless: true });
    const pending = figures.filter(f => {
      const before = old.get(f.source);
      return args.includes('--force') || previous?.configSha256 !== configHash || previous?.rendererSha256 !== rendererHash || previous?.pipelineSha256 !== hash(fs.readFileSync(__filename)) || before?.sourceSha256 !== f.sourceSha256 || !fs.existsSync(path.join(root, f.svg)) || hash(fs.readFileSync(path.join(root, f.svg))) !== before?.svgSha256;
    });
    const failures = [];
    let cursor = 0, completed = 0;
    try {
      await Promise.all(Array.from({ length: Math.min(4, pending.length) }, async () => {
        const page = await browser.newPage();
        await page.route('**/*', route => route.abort());
        await page.setContent('<!doctype html><html><head><meta charset="utf-8"></head><body></body></html>');
        await page.addScriptTag({ path: path.resolve(mermaidPath) });
        await page.evaluate(settings => mermaid.initialize(settings), config.renderer);
        while (cursor < pending.length) {
          const f = pending[cursor++];
          try {
            const svg = await page.evaluate(async ({ body, id }) => {
              document.body.innerHTML = '';
              await mermaid.parse(body);
              const result = await mermaid.render(id, body);
              const parsed = new DOMParser().parseFromString(result.svg, 'image/svg+xml');
              if (parsed.querySelector('parsererror,.error-icon,.error-text') || !parsed.documentElement.getAttribute('viewBox')) throw Error('Invalid SVG output');
              // Preserve spaces between adjacent text spans in SVG/PDF converters.
              return result.svg.replace('<svg ', '<svg xml:space="preserve" ');
            }, { body: f.body, id: `diagram${hash(f.source).slice(0, 12)}` });
            write(f.svg, svg.trim() + '\n');
          } catch (error) {
            failures.push({ chapter: f.chapter, line: f.line, source: f.source, error: error.message });
          }
          completed++;
          if (completed % 50 === 0) console.log(`Rendered ${completed}/${pending.length}; failures: ${failures.length}`);
        }
        await page.close();
      }));
    } finally { await browser.close(); }
    for (const f of figures) write(f.source, f.body);
    if (failures.length) {
      write('build/diagrams/errors.json', JSON.stringify(failures, null, 2) + '\n');
      throw Error(`${failures.length} diagrams failed. See build/diagrams/errors.json. Manifest not updated.`);
    }
    const entries = figures.map(({ body, ...f }) => ({ ...f, svgSha256: hash(fs.readFileSync(path.join(root, f.svg))) }));
    write(manifestPath, JSON.stringify({ schemaVersion: 1, configSha256: configHash, rendererSha256: rendererHash, pipelineSha256: hash(fs.readFileSync(__filename)), figures: entries }, null, 2) + '\n');
    const errorReport = path.join(root, 'build/diagrams/errors.json');
    if (fs.existsSync(errorReport)) fs.unlinkSync(errorReport);
    console.log(`Exported ${pending.length}; retained ${figures.length - pending.length}; total ${figures.length}.`);
  }
  if (publication) {
    for (const doc of documents) {
      const lines = [...doc.lines];
      for (let i = doc.blocks.length - 1; i >= 0; i--) {
        const block = doc.blocks[i];
        const f = figures.find(f => f.chapter === doc.file && f.index === i + 1);
        const alt = block.title.replace(/[\[\]]/g, '');
        lines.splice(block.start, block.end - block.start + 1, `![${alt}](../../../${f.svg})`);
      }
      // Relocate existing relative Markdown links from book/ to build/publication/book/.
      let codeFence = null;
      const output = lines.map(line => {
        const marker = line.match(/^ {0,3}(`{3,}|~{3,})/);
        if (marker) {
          if (!codeFence) codeFence = marker[1];
          else if (marker[1][0] === codeFence[0] && marker[1].length >= codeFence.length) codeFence = null;
          return line;
        }
        if (codeFence) return line;
        return line.replace(/(!?\[[^\]\n]*\]\()([^\s)]+)(\))/g, (all, open, target, close) => {
        if (/^(?:[a-z][a-z0-9+.-]*:|#|\/|\.\.\/\.\.\/\.\.\/diagrams\/)/i.test(target)) return all;
        const [local, anchor] = target.split('#');
        return open + '../../../book/' + local + (anchor ? '#' + anchor : '') + close;
        });
      }).join('\n');
      write(`build/publication/${doc.file}`, output);
    }
    console.log(`Prepared ${documents.length} publication Markdown files under build/publication/book/.`);
  }
}
module.exports = { extract };
if (require.main === module) main().catch(error => { console.error(error.message); process.exitCode = 1; });
