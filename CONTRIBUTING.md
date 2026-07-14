# Contributing

Thank you for contributing to the Enterprise AI Engineering Handbook. This repository is managed like a professional software product: changes must be traceable, reviewable, and aligned with the handbook's architecture and editorial standards.

## Before You Begin

Read:

1. `README.md`
2. `book/00-editorial-style-guide.md`
3. `ROADMAP.md`
4. The relevant chapter, prompt, skill, harness, or Alpha Car Detailing documentation

## Contribution Types

- Handbook chapter content
- Technical examples
- Alpha Car Detailing implementation
- Architecture diagrams
- Prompts and role definitions
- Reusable skills
- Harness scripts and validation
- Governance, security, and compliance guidance
- Corrections, citations, and editorial improvements

## Branching and Commits

Use a focused branch:

```text
chapter/01-evolution
example/event-hub-consumer
skill/create-dotnet-api
harness/add-evaluator
fix/editorial-terminology
```

Write clear commits in the imperative mood:

```text
Add Chapter 1 architecture timeline
Define reviewer-agent responsibilities
Correct repository intelligence terminology
```

Avoid mixing unrelated chapter, code, and harness changes in one pull request.

## Pull Request Requirements

A pull request should explain:

- The problem or objective
- The files and concepts changed
- Architectural or editorial decisions
- Validation performed
- Screenshots or exported diagrams when visual assets change
- Known limitations and follow-up work

## Editorial Requirements

All prose must follow `book/00-editorial-style-guide.md`.

Mandatory rules include:

- Use the frozen terminology consistently.
- Prefer precise enterprise language over promotional language.
- Do not write in conversational chatbot style.
- Avoid unsupported claims and invented metrics.
- Distinguish principle, recommendation, example, and opinion.
- Keep vendor-neutral principles separate from tool-specific implementation.
- Use the standard chapter structure unless an approved exception is documented.

## Technical Requirements

Code examples must be:

- Relevant to the Alpha Car Detailing domain or the chapter objective
- Secure by default
- Explicit about assumptions
- Consistent with .NET 10 and the selected architecture unless demonstrating an alternative
- Testable or accompanied by validation guidance
- Free of credentials, secrets, client data, and proprietary code

Commands must identify the expected shell when ambiguity exists.

## Diagram Requirements

- Store editable source in `diagrams/source/`.
- Store publication-ready exports in `diagrams/exported/`.
- Use stable filenames such as `ch13-harness-architecture.drawio` and `ch13-harness-architecture.svg`.
- Include a title, readable labels, direction of flow, and a legend when notation is not obvious.
- Ensure diagrams remain readable in grayscale and in PDF output.

## Prompt and Skill Requirements

Prompts describe a specific objective and context. Skills describe reusable workflows.

Every prompt or skill should define, as applicable:

- Purpose
- Inputs
- Preconditions
- Procedure
- Expected outputs
- Validation
- Failure handling
- Security considerations
- Human approval points

Do not conceal critical behavior inside prose that an operator is unlikely to review.

## Harness Requirements

Harness changes must preserve:

- Auditability
- Deterministic validation where possible
- Human approval for standards changes
- Separation of execution, validation, evaluation, and learning
- Evidence collection for builds, tests, reviews, and pull requests

A learning component may propose modifications but must not directly update approved instructions, skills, policies, or steering notes without authorization.

## Review Checklist

Before opening a pull request:

- [ ] Markdown renders correctly.
- [ ] Terminology follows the style guide.
- [ ] Headings are in logical order.
- [ ] Links and file paths are valid.
- [ ] Code and commands have been checked.
- [ ] Security implications are addressed.
- [ ] Vendor comparisons are accurate and necessary.
- [ ] Diagram source and export are both included when applicable.
- [ ] Changelog entry is added for a meaningful published change.
- [ ] Generated files are not presented as authoritative source.

## Governance

Repository-wide instructions, approved skills, governance policies, and steering-note ownership are controlled assets. Changes require review by the designated technical or editorial owner.
