# Chapter 0 — Editorial Style Guide

> **Status:** Governing editorial standard. This chapter is not part of the public narrative sequence unless explicitly included in a release.

## 0.1 Purpose

This guide defines the mandatory writing, terminology, structure, code, diagram, review, and publishing standards for the **Enterprise AI Engineering Handbook**. It exists to make a multi-author, multi-agent handbook read as one coherent professional work.

The handbook should resemble a carefully edited enterprise engineering title from Microsoft Press, O'Reilly, or Addison-Wesley. It must not read like a collection of chat responses, product marketing pages, or disconnected tutorials.

## 0.2 Editorial Position

The handbook teaches **AI Engineering**: the disciplined application of AI agents, repository intelligence, reusable skills, task prompts, automation harnesses, governance controls, and evidence-driven improvement to professional software delivery.

Claude Code is the primary implementation platform because it is the organization's selected development agent. The engineering principles must remain vendor-neutral. GitHub Copilot, OpenAI Codex, and future platforms are compared only when the difference is technically or operationally meaningful.

The handbook is not:

- A beginner's introduction to programming
- A catalogue of prompts
- A Claude Code product manual
- A generic AI overview
- A collection of toy examples

## 0.3 Audience and Assumed Knowledge

Write for:

- Software architects
- Senior software developers
- Team leads
- Engineering managers
- DevOps and platform engineers
- AI champions and governance stakeholders
- Enterprise development teams

Assume readers understand professional software delivery, source control, application architecture, testing, CI/CD, and cloud fundamentals. Explain AI-engineering concepts rigorously without oversimplifying the software-engineering context.

## 0.4 Voice and Tone

Use a confident, measured, evidence-oriented professional voice.

### Required characteristics

- Clear rather than clever
- Precise rather than promotional
- Practical rather than abstract
- Architectural rather than tool-obsessed
- Candid about trade-offs and limitations
- Consistent across chapters

### Avoid

- Chatbot phrases such as “Sure,” “Let's dive in,” or “Here is a comprehensive answer”
- Hype such as “revolutionary,” “magic,” “guaranteed,” or “10x” without defensible evidence
- Excessive rhetorical questions
- Unsupported predictions
- Treating an AI agent as infallible or autonomous by default
- Repeating the same conclusion in multiple sections

Use second person sparingly for direct procedures. Prefer “the team,” “the organization,” “the architect,” or “the engineer” when discussing enterprise responsibilities.

## 0.5 Frozen Terminology

The following terms are authoritative across prose, diagrams, code comments, templates, and examples.

| Preferred term | Avoid | Editorial meaning |
|---|---|---|
| AI Agent | Bot, AI Assistant | A model-enabled actor that plans or performs engineering work within defined boundaries |
| Instruction | Rule, Configuration | Persistent repository- or organization-level engineering guidance |
| Skill | Macro, Script | A reusable engineering workflow with inputs, procedure, validation, and outputs |
| Harness | Wrapper, Framework | Automation that orchestrates agents, tools, validation, evidence, and feedback |
| Steering Note | Temporary Notes | A human-owned mission briefing containing current priorities, constraints, and acceptance criteria |
| Repository Intelligence | Project Memory | Structured and discoverable knowledge that helps an agent understand a repository |
| AI Engineering | Prompt Engineering | The broader engineering discipline; use “prompt engineering” only for prompt-specific techniques |

Additional standard distinctions:

- **Prompt:** A task-specific request for the current unit of work.
- **Role:** A bounded responsibility such as Lead, Developer, Reviewer, Validator, Evaluator, or Architect.
- **Harness Memory:** Retained evidence and approved knowledge used to improve future execution.
- **Recommendation:** A proposed change produced by a learning component; it is not an approved standard.
- **Human Approval Gate:** A required authorization before a controlled asset or production-affecting action changes.

Do not use “self-learning” to imply uncontrolled self-modification. The approved phrase is **self-learning harness**, and the text must explain that learning produces recommendations subject to human approval.

## 0.6 Vendor-Neutrality Rules

Separate three layers in every relevant discussion:

1. **Engineering principle** — durable and vendor-neutral.
2. **Primary implementation** — usually Claude Code.
3. **Material alternatives** — Copilot, Codex, or another platform when behavior differs.

Do not force a three-vendor comparison into every section. Add a comparison only when it changes setup, repository conventions, agent behavior, context management, permissions, execution, review, or governance.

Use current official product names. Avoid claims about features, limits, security, or pricing without verification at the time of publication.

## 0.7 Running Example Rules

Use **Alpha Car Detailing** as the continuous enterprise example.

The company operates multiple stations across the country and serves walk-in customers, corporate fleets, government departments, insurance companies, and rental companies.

Examples should demonstrate realistic concerns such as:

- Station and service management
- Customer and fleet contracts
- Vehicle bookings and walk-in jobs
- Service packages, pricing, and discounts
- Work queues and bay allocation
- Payments and invoicing
- Government and corporate billing
- Event-driven integration
- Security and auditability
- Observability and operational support

Do not introduce unrelated toy systems when the concept can be demonstrated within this domain. A focused standalone example is acceptable only when the main domain would obscure the concept.

## 0.8 Standard Chapter Structure

Every narrative chapter should contain the following sections unless the editorial owner approves an exception:

1. Story-driven opening
2. Learning Objectives
3. Background
4. Concepts
5. Architecture Discussion
6. Professional Diagrams
7. Hands-on Example
8. Claude Example
9. GitHub Copilot Comparison
10. Codex Comparison
11. Best Practices
12. Anti-patterns
13. Architect's Notes
14. Enterprise Tips
15. Decision Points
16. Exercises
17. Interview Questions
18. Chapter Summary
19. Further Reading

A chapter may combine adjacent sections when that improves flow, but it must preserve their intent. Vendor comparison sections may explicitly state that no material difference exists rather than inventing distinctions.

## 0.9 Story-Driven Openings

Open with a credible enterprise scenario that establishes a problem, consequence, or decision. The opening should create context for the chapter without becoming fictional drama.

A useful opening normally includes:

- The organization or team
- The delivery pressure or engineering problem
- The initial assumption
- The observed failure, risk, or opportunity
- The architectural question the chapter will answer

Return to the scenario later to demonstrate how the chapter's practices improve the outcome.

## 0.10 Learning Objectives

Use observable outcomes. Begin each objective with an action verb such as:

- Distinguish
- Design
- Evaluate
- Implement
- Govern
- Validate
- Diagnose
- Compare

Avoid vague objectives such as “understand AI” or “learn about harnesses.”

## 0.11 Headings and Organization

- Use one level-one heading for the chapter title.
- Use level-two headings for major chapter sections.
- Use level-three headings for substantial subsections.
- Avoid heading levels deeper than level four.
- Use sentence case for headings.
- Do not number ordinary headings manually unless the publication pipeline requires it.
- Keep paragraphs focused on one main idea.

Prefer short lists for genuine collections or procedures. Do not convert connected reasoning into excessive bullet lists.

## 0.12 Callout Conventions

Use callouts consistently and sparingly.

```markdown
> **Architect's Note**
> Explain a design implication, boundary, or long-term trade-off.
```

```markdown
> **Enterprise Tip**
> Provide immediately applicable organizational or operational guidance.
```

```markdown
> **Common Mistake**
> Describe a recurring failure mode and its consequence.
```

```markdown
> **Decision Point**
> State the decision, relevant forces, and conditions favoring each option.
```

```markdown
> **Real-World Scenario**
> Connect the concept to a credible enterprise situation.
```

A callout must add value beyond the surrounding paragraph. Do not stack multiple callouts without intervening analysis.

## 0.13 Architecture Discussions

Architecture sections should identify:

- Context and quality attributes
- System boundary
- Responsibilities
- Data ownership
- Interaction model
- Trust boundaries
- Failure modes
- Operational concerns
- Alternatives and trade-offs
- Decision criteria

Avoid presenting one architecture as universally correct. Explain the conditions under which a recommendation holds.

Use architecture decision records for decisions that affect multiple chapters or the Alpha Car Detailing reference implementation.

## 0.14 Diagram Standards

Every diagram must answer a specific question.

### Storage and naming

- Editable source: `diagrams/source/`
- Publication export: `diagrams/exported/`
- Filename: `chNN-short-purpose.ext`
- Example: `ch13-harness-execution-flow.drawio` and `ch13-harness-execution-flow.svg`

### Visual rules

- Provide a clear title or descriptive caption.
- Use one direction of flow whenever practical.
- Label relationships and asynchronous boundaries.
- Distinguish human actors, AI agents, repositories, services, and external systems.
- Show trust boundaries when security is relevant.
- Include a legend when notation is not obvious.
- Avoid decorative icons that do not convey information.
- Ensure readable typography in standard PDF page width.
- Ensure meaning does not depend on color alone.
- Verify grayscale readability and accessible contrast.

### Diagram types

Use an appropriate notation:

- Context and container views for system structure
- Sequence diagrams for interactions
- State diagrams for lifecycle behavior
- Data-flow diagrams for information movement and trust boundaries
- Decision trees for selection guidance
- Deployment diagrams for runtime topology

Introduce the purpose before a diagram and interpret the important implications after it.

## 0.15 Code and Command Formatting

Use fenced code blocks with a language identifier.

```csharp
public sealed record CreateBookingCommand(Guid StationId, Guid VehicleId);
```

```powershell
./harness/scripts/validate.ps1
```

### Code rules

- Prefer complete, focused examples over large unexplained listings.
- Use filenames before multi-file examples.
- Use `...` only when omitted code is irrelevant and the omission cannot mislead.
- Follow current .NET naming and design conventions.
- Use nullable reference types in C# examples.
- Prefer asynchronous APIs for I/O-bound work.
- Include cancellation where operationally meaningful.
- Show validation, logging, error handling, and security when central to the lesson.
- Never include live credentials, tokens, client identifiers, or production endpoints.
- Explain whether code is illustrative, compilable, or extracted from the reference implementation.

### Command rules

- Identify PowerShell, Bash, Azure CLI, .NET CLI, or another shell when syntax differs.
- Show the expected working directory when necessary.
- Do not use destructive commands without an explicit warning and recovery guidance.
- Keep placeholders visually clear, for example `<subscription-id>`.

## 0.16 Tables

Use tables for structured comparison, not long-form explanation.

- Give columns precise labels.
- Keep cell content concise.
- Explain complex implications in prose after the table.
- Avoid tables so wide that they cannot fit a printed page.
- Do not use checkmarks as a substitute for nuanced capability analysis.

## 0.17 Citations and Evidence

Cite external claims that are specific, disputed, quantitative, security-sensitive, legal, compliance-related, or product-dependent.

Prefer:

1. Official documentation and standards
2. Primary research
3. Maintainer documentation
4. Reputable technical publications

Record source notes under `references/` when a citation needs publication review. Do not copy large passages. Paraphrase accurately and preserve the original meaning.

Mark internal experience as an example or observation rather than universal fact.

## 0.18 Security and Governance Language

Security guidance must define actors, assets, permissions, boundaries, threats, controls, and residual risk. Avoid statements such as “this is secure” without scope and assumptions.

Every autonomous or semi-autonomous workflow should address:

- Least privilege
- Secret handling
- Tool and network access
- Prompt injection and untrusted content
- Data classification
- Audit evidence
- Human approval
- Rollback or recovery
- Separation of duties

The harness must never silently change approved instructions, skills, policies, or steering notes. A learning component produces a proposal with supporting evidence; an authorized human approves or rejects it.

## 0.19 Best Practices, Anti-patterns, and Decision Points

A best practice must state the context in which it applies. An anti-pattern must describe both the behavior and the consequence. A decision point must present alternatives and criteria rather than disguise a preference as a choice.

Recommended decision-point structure:

- Decision to make
- Context
- Forces and constraints
- Option A and when it fits
- Option B and when it fits
- Risks
- Recommended default
- Exceptions

## 0.20 Exercises

Exercises should reinforce architecture and engineering judgment, not simple recall.

Use a mix of:

- Analysis exercises
- Design exercises
- Implementation exercises
- Review exercises
- Governance exercises

For implementation exercises, define inputs, constraints, expected evidence, and acceptance criteria. Avoid solutions that depend on undocumented product behavior.

## 0.21 Interview Questions

Interview questions should assess professional reasoning. Include a balanced mix of:

- Conceptual distinctions
- Architecture trade-offs
- Failure analysis
- Governance and security decisions
- Applied scenarios

Avoid trivia. A good answer should reveal how the candidate thinks, not only what the candidate remembers.

## 0.22 Chapter Summaries and Further Reading

A chapter summary should synthesize the major decisions and relationships. It should not repeat every heading.

Further reading should favor durable standards, official documentation, seminal books, and high-quality primary sources. Verify URLs and current product documentation during publication review.

## 0.23 File Naming and Repository Conventions

### Chapters

```text
book/01-the-evolution-of-software-development.md
book/02-understanding-ai-coding-agents.md
```

Use two-digit numeric prefixes until the chapter plan requires three digits.

### Prompts

```text
prompts/roles/lead-agent.md
prompts/tasks/create-booking-api.md
```

### Skills

```text
skills/dotnet/create-rest-api/SKILL.md
skills/devops/add-opentelemetry/SKILL.md
```

### Harness assets

```text
harness/scripts/lead.ps1
harness/roles/reviewer.md
harness/search/steering-note.md
harness/knowledge/proposals/
```

Use lowercase kebab-case unless a platform requires a specific filename such as `README.md`, `CLAUDE.md`, `AGENTS.md`, or `SKILL.md`.

## 0.24 Review Roles

A substantial chapter should receive distinct reviews:

- **Editorial review:** clarity, structure, tone, terminology, consistency
- **Technical review:** correctness, architecture, code, operational realism
- **Security and governance review:** access, data, threats, approvals, auditability
- **Vendor review:** current product behavior and fair comparison
- **Production review:** formatting, diagrams, links, publication output

One reviewer may cover multiple roles, but the review checklist must make the responsibilities explicit.

## 0.25 Chapter Review Checklist

### Narrative and structure

- [ ] Opening scenario establishes a real engineering problem.
- [ ] Learning objectives are observable.
- [ ] Chapter follows the standard structure or documents an exception.
- [ ] The argument progresses logically without repetition.
- [ ] Summary synthesizes the chapter's main decisions.

### Terminology and tone

- [ ] Frozen terminology is used consistently.
- [ ] Tone is professional and free of chatbot phrasing.
- [ ] Claims are precise and not promotional.
- [ ] AI agents are not presented as infallible or unrestricted.

### Architecture and technical quality

- [ ] System boundaries and responsibilities are clear.
- [ ] Trade-offs and alternatives are included.
- [ ] Code examples are accurate, secure, and appropriately scoped.
- [ ] Commands identify the intended shell where needed.
- [ ] Failure modes and operational concerns are addressed.

### Vendor neutrality

- [ ] Principles are separated from implementation details.
- [ ] Claude-first guidance is clearly identified.
- [ ] Copilot or Codex comparisons are included only when material.
- [ ] Product-dependent claims have been verified.

### Diagrams and tables

- [ ] Each diagram answers a defined question.
- [ ] Editable source and publication export are present.
- [ ] Labels are readable and notation is explained.
- [ ] Tables remain readable in print output.

### Governance and evidence

- [ ] Security, data, permissions, and approval concerns are addressed.
- [ ] External claims are cited appropriately.
- [ ] No secrets, confidential data, or proprietary client material is included.
- [ ] Human approval is explicit for controlled changes.

## 0.26 Publishing Checklist

- [ ] Markdown linting passes.
- [ ] Internal links and anchors resolve.
- [ ] External links are verified.
- [ ] Code snippets compile or are marked illustrative.
- [ ] Commands have been validated in the stated environment.
- [ ] Diagram exports match their source files.
- [ ] Image resolution is sufficient for print.
- [ ] Terminology scan passes.
- [ ] Technical review is complete.
- [ ] Security and governance review is complete.
- [ ] Copy edit is complete.
- [ ] Changelog is updated.
- [ ] DOCX and PDF builds are generated from approved Markdown.
- [ ] Table of contents, page breaks, captions, and cross-references are checked.
- [ ] Release version and date are approved.

## 0.27 Definition of Done

A chapter is complete when:

1. Its purpose and audience value are clear.
2. The standard structure is satisfied or an exception is approved.
3. Technical examples and diagrams have been validated.
4. Architecture, security, operations, and governance implications are addressed.
5. Vendor-specific guidance is accurate and subordinate to durable principles.
6. All required reviews are complete.
7. Publication output has been inspected.
8. The changelog and roadmap reflect the result.

This guide governs all future handbook content. Changes to it require explicit editorial and technical approval because they affect the consistency of the complete publication.
