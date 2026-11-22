# Chapter diagram assets

The Mermaid fences in `book/*.md` are the authoritative diagram sources. Every rendered Mermaid block in a chapter is exported; literal examples inside an outer code fence are excluded. The remaining `20-2-scratchpad.md` is included. Files named `*.edited.md` are excluded to avoid publishing duplicate drafts.

- `source/`: extracted Mermaid, using `ch<chapter filename>-<block number>.mmd`.
- `exported/`: generated SVGs with the same stem.
- `config.json`: shared neutral theme, Arial labels, strict security and SVG text labels.
- `manifest.json`: chapter location, title, source and SVG hashes, renderer bundle hash, configuration hash and pipeline hash.

Do not edit generated `.mmd` or `.svg` files directly. Edit the chapter and regenerate. Block numbers follow chapter order, so inserting a diagram can renumber subsequent files. Review the diff and remove obsolete assets reported by the check after renaming or deleting figures.

## Generate

Requires Node.js, Playwright and an installed Edge or Chrome browser, plus a local Mermaid browser bundle. Install dependencies in your normal tooling environment. The exporter does not download dependencies or load network resources.

PowerShell:

```powershell
$env:MERMAID_JS = 'C:/path/to/mermaid.min.js'
$env:PLAYWRIGHT_MODULE = 'C:/path/to/node_modules/playwright'
node harness/scripts/export-diagrams.cjs --browser chrome --publication
```

Alternatively pass `--mermaid-js PATH` and `--playwright-module PATH`. A standard local installation of Playwright needs no module argument. Edge is the default browser; `--browser chrome` selects Chrome. `--force` rerenders everything. The manifest pins the exact Mermaid bundle by SHA-256; use that same bundle for reproducible updates. A changed bundle, configuration or exporter triggers regeneration.

Rendering errors are recorded in `build/diagrams/errors.json`; the command fails and does not update the manifest. Correct syntax in the chapter rather than in extracted files, then rerun. Successful exports are checked for XML parsing errors, Mermaid error elements and a viewBox.

## Validate

```powershell
node harness/scripts/export-diagrams.cjs --check
```

This read-only check requires only Node.js. It rejects missing assets, changed chapter diagrams, changed exports, stale metadata, changed configuration or exporter, and orphaned assets. It verifies hashes rather than launching a browser. Run after regeneration and before publishing.

## Publication

```powershell
node harness/scripts/export-diagrams.cjs --check --publication
```

This prepares Markdown under `build/publication/book/`, replacing Mermaid fences with relative SVG image references. Chapter prose and captions stay in place. Use these generated chapters as input to a publication converter that supports SVG. The original `book/` files retain Mermaid for authoring. Generated publication files reference the repository assets; keep them within this checkout or package the images with the document. Review page fit, captions and grayscale readability in the final document; successful SVG rendering alone does not verify print layout.
