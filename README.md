# Enterprise AI Engineering Handbook

> A practical, enterprise-focused guide to AI-assisted software development with Claude Code, GitHub Copilot, OpenAI Codex, and future coding agents.

## Purpose

This repository contains the source material, companion examples, reusable prompts, skills, governance assets, and automation harness for the **Enterprise AI Engineering Handbook**.

The handbook teaches durable **AI Engineering** practices rather than tool-specific prompt tricks. Claude Code is the primary implementation platform because it is the organization's selected development agent, while GitHub Copilot and OpenAI Codex are compared where their workflows materially differ.

## Audience

The handbook is written for software architects, senior developers, team leads, engineering managers, DevOps engineers, AI champions, and enterprise engineering teams. It assumes professional software-development experience and does not use toy examples.

## Running Example: Alpha Car Detailing

The companion system models a nationwide vehicle washing and detailing company serving:

- Walk-in customers
- Corporate fleets
- Government departments
- Insurance companies
- Rental companies

The example evolves throughout the handbook and demonstrates .NET 10, Clean Architecture, domain-driven design, microservices, event-driven architecture, Azure, Kubernetes, Docker, Event Hubs, Redis, SQL Server, JWT, Serilog, OpenTelemetry, GitHub Actions, and Azure DevOps.

## Repository Map

| Path | Purpose |
|---|---|
| `book/` | Handbook chapters in publication order |
| `diagrams/` | Editable diagram sources and exported assets |
| `images/` | Screenshots and supporting publication images |
| `templates/` | Reusable authoring and review templates |
| `examples/` | Focused, chapter-specific technical examples |
| `alpha-car-detailing/` | Complete running enterprise example |
| `prompts/` | Task prompts and role-specific prompt assets |
| `skills/` | Reusable AI engineering workflows |
| `harness/` | Harness scripts, roles, validation, metrics, and learning assets |
| `references/` | Approved references and source notes |
| `build/` | Generated publication outputs; not authoritative source |

## Core Model

```text
Instructions  -> persistent repository-wide engineering standards
Skills        -> reusable implementation workflows
Prompts       -> task-specific requests
Steering Note -> current mission, priorities, constraints, and acceptance criteria
Harness       -> orchestration, validation, evaluation, evidence, and feedback
```

A self-learning harness may recommend improvements to prompts, skills, or instructions, but it must never silently change engineering standards. Human approval is mandatory.

## Editorial Principles

- Enterprise engineering principles remain vendor-neutral.
- Claude-first examples are used where a concrete implementation is required.
- Copilot and Codex comparisons are included only where behavior or workflow differs.
- The frozen terminology in `book/00-editorial-style-guide.md` is mandatory.
- Every chapter follows the standard chapter template and review checklist.
- Diagrams, source code, commands, and claims must be reviewable and reproducible.

## Current Status

The repository starter kit and editorial foundation are established. The next publication milestone is the professional first draft of Chapter 1, **The Evolution of Software Development**.

See [ROADMAP.md](ROADMAP.md) for planned milestones and [CONTRIBUTING.md](CONTRIBUTING.md) before proposing changes.

## Local Setup

```powershell
cd C:\ZeeWork\Enterprise-AI-Engineering-Handbook
git status
```

Author Markdown with a formatter and Markdown linter enabled. Generated DOCX and PDF files belong under `build/` and should not replace Markdown as the source of truth.

## License

Add or approve the repository license before external publication. Until then, treat the content as internal and all rights reserved.
