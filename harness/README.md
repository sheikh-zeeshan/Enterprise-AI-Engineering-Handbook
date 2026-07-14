# Harness

This folder contains the reference automation harness used throughout the handbook.

## Responsibilities

- Read instructions, skills, prompts, role definitions, and the steering note
- Coordinate lead, developer, reviewer, validator, evaluator, and architect responsibilities
- Run builds, tests, linting, security checks, and policy validation
- Record execution evidence and metrics
- Create reviewable recommendations from learned evidence
- Preserve mandatory human approval gates

## Initial layout

- `scripts/` — orchestration and validation scripts
- `roles/` — role responsibilities and prompt contracts
- `search/` — steering notes and mission context
- `population/` — task or candidate-work population assets
- `metrics/` — quality, productivity, reliability, and cost evidence
- `knowledge/` — approved knowledge and proposed improvements
- `tests/` — harness tests

The harness must never silently change approved instructions, skills, policies, or steering notes.
