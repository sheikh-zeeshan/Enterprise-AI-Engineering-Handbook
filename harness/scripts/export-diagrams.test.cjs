const { test } = require('node:test');
const assert = require('node:assert/strict');
const { extract } = require('./export-diagrams.cjs');

test('literal Mermaid nested inside a longer Markdown fence is excluded', () => {
  const result = extract('# Heading\n````markdown\n```mermaid\nflowchart TD\nA-->B\n```\n````\n');
  assert.equal(result.blocks.length, 0);
});
test('mixed fence styles keep diagram boundaries and source locations', () => {
  const result = extract('# Heading\r\n~~~mermaid\r\nflowchart TD\r\nA-->B\r\n~~~\r\n');
  assert.deepEqual(result.blocks, [{ start: 1, end: 4, line: 2, title: 'Heading', body: 'flowchart TD\nA-->B\n' }]);
});
test('short or mismatched closing fences do not end an outer fence', () => {
  const result = extract('````text\n~~~\n```\n```mermaid\nA-->B\n```\n````\n');
  assert.equal(result.blocks.length, 0);
});
test('an unclosed fence fails rather than silently losing diagrams', () => {
  assert.throws(() => extract('Intro\n```mermaid\nflowchart TD\n'), /Unclosed fence at line 2/);
});
