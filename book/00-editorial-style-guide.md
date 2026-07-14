# Chapter 0 — Editorial Style Guide

**Version:** 1.0

**Status:** Foundation Document

**Audience:** Authors, Reviewers, Contributors, AI Agents, Editors

---

# Purpose

This document defines the editorial standards used throughout the *Enterprise AI Engineering Handbook*. It establishes consistent writing, formatting, terminology, diagrams, code presentation, review processes, and publishing requirements.

Every chapter, example, diagram, code sample, and pull request must comply with this guide.

This document is intentionally written first because it governs all future content.

---

# Table of Contents

1. Editorial Principles
2. Writing Standards
3. Audience Guidelines
4. Voice and Tone
5. Terminology Standards
6. Markdown Standards
7. Heading Conventions
8. Code Formatting
9. Diagram Standards
10. Image Guidelines
11. Chapter Structure
12. Callout Rules
13. Tables
14. Architecture Discussions
15. Vendor Neutrality
16. Alpha Car Detailing Rules
17. AI Platform Examples
18. Review Checklist
19. Publishing Checklist
20. Versioning

---

# 1. Editorial Principles

The handbook should resemble professional technical publications such as Microsoft Press, Addison-Wesley, and O'Reilly rather than AI-generated conversational content.

Every chapter should be suitable for enterprise engineers, architects, and technical leads.

The handbook is written as an engineering reference rather than a tutorial.

Readers should be able to return to individual chapters months later as a long-term reference.

---

# 2. Writing Standards

## Write for Professionals

Assume readers:

* understand software engineering
* know Git
* know APIs
* understand enterprise systems
* understand cloud computing

Avoid explaining elementary programming concepts unless directly relevant.

---

## Explain "Why" Before "How"

Every major section should answer:

* Why does this exist?
* What problem does it solve?
* When should it be used?
* When should it not be used?

Only then explain implementation.

---

## Enterprise First

Examples should prioritize:

* maintainability
* scalability
* governance
* security
* observability
* operational excellence

Avoid toy examples.

---

## Prefer Narrative Introductions

Each chapter begins with a short real-world scenario rather than definitions.

Example:

> A development team introduces AI coding agents into a mature microservices platform. Productivity improves immediately, but inconsistent architectural decisions begin appearing across repositories. The team quickly realizes that AI must be governed with the same discipline as human developers.

---

# 3. Audience Guidelines

Primary audience:

* Software Architects
* Senior Developers
* Technical Leads
* Engineering Managers
* AI Champions
* Enterprise Development Teams

Secondary audience:

* Experienced Developers
* DevOps Engineers
* Platform Engineers

Not intended for beginners.

---

# 4. Voice and Tone

Use:

* Professional
* Objective
* Evidence-based
* Practical
* Vendor-neutral

Avoid:

* Marketing language
* Hyperbole
* Excessive enthusiasm
* Informal internet slang
* Conversational filler

Preferred:

> The harness validates every generated pull request before human review.

Avoid:

> This awesome harness magically fixes everything!

---

# 5. Terminology Standards

The following terminology is frozen across the handbook.

| Preferred               | Avoid                                                         |
| ----------------------- | ------------------------------------------------------------- |
| AI Agent                | Bot                                                           |
| Instruction             | Configuration                                                 |
| Skill                   | Script (unless referring to actual scripts)                   |
| Harness                 | Wrapper                                                       |
| Steering Note           | Notes                                                         |
| Repository Intelligence | Project Memory                                                |
| AI Engineering          | Prompt Engineering (unless discussing prompting specifically) |
| Human Approval          | Manual Review                                                 |
| Knowledge Base          | AI Memory                                                     |

Every chapter must use these terms consistently.

---

# 6. Markdown Standards

Use ATX headings:

```markdown
# Chapter

## Section

### Topic
```

Never skip heading levels.

Leave one blank line before and after headings.

Maximum heading depth:

```
#### Level 4
```

Avoid deeper nesting unless absolutely necessary.

---

# 7. Heading Conventions

Each chapter should contain:

```
# Chapter Title

Story

Learning Objectives

Background

Core Concepts

Architecture Discussion

Hands-on Example

Vendor Comparisons

Best Practices

Anti-patterns

Architect's Notes

Enterprise Tips

Exercises

Interview Questions

Summary

Further Reading
```

This structure is frozen for all chapters.

---

# 8. Code Formatting

## Languages

Use fenced code blocks.

Example:

````markdown
```csharp
public sealed class CustomerService
{
}
```
````

Supported languages:

* csharp
* json
* yaml
* xml
* powershell
* bash
* sql
* terraform
* dockerfile
* mermaid

---

## Code Rules

Every sample must:

Compile (where applicable)

Use production naming

Follow Clean Architecture

Include logging where appropriate

Include exception handling where relevant

Avoid placeholder implementations

Avoid TODO comments unless intentional.

---

## File Paths

Always display using forward slashes.

```
skills/dotnet/api.skill.md
```

---

# 9. Diagram Standards

Preferred diagram types:

* Mermaid
* PlantUML
* Architecture diagrams
* Sequence diagrams
* Flowcharts
* Component diagrams
* Deployment diagrams
* C4-style architecture

Every diagram requires:

Title

Caption

Description

Referenced section

Example:

```
Figure 12-3

Harness Execution Pipeline
```

---

## Diagram Naming

```
Figure 1-1

Figure 3-4

Figure 18-2
```

Never use image filenames inside the text.

---

# 10. Image Guidelines

Every image should:

Support the discussion

Be referenced before appearing

Have a descriptive caption

Avoid screenshots where diagrams communicate the concept more effectively.

---

# 11. Chapter Template

Every chapter follows the same structure.

1. Story-driven opening
2. Learning objectives
3. Background
4. Core concepts
5. Architecture discussion
6. Professional diagrams
7. Hands-on example
8. Claude implementation
9. GitHub Copilot comparison
10. Codex comparison
11. Best practices
12. Anti-patterns
13. Architect's Notes
14. Enterprise Tips
15. Decision Points
16. Exercises
17. Interview Questions
18. Chapter Summary
19. Further Reading

No chapter should omit these sections without editorial approval.

---

# 12. Callout Rules

Use standardized callout blocks.

## Architect's Note

Highlights design rationale or long-term architectural considerations.

---

## Enterprise Tip

Provides practical guidance from enterprise experience.

---

## Common Mistake

Warns against frequently observed implementation errors.

---

## Decision Point

Presents a meaningful architectural trade-off and its implications.

---

## Real-World Scenario

Describes how the concept is applied in production environments.

---

## Security Consideration

Highlights security implications, threats, or compliance requirements.

---

## Governance Note

Explains ownership, approval, audit, or policy considerations.

---

# 13. Tables

Tables should be concise and compare concepts clearly.

Preferred columns:

| Topic | Recommendation | Rationale |
| ----- | -------------- | --------- |

Avoid excessively wide tables.

---

# 14. Architecture Discussions

Every architecture section should include:

Problem statement

Context

Design alternatives

Trade-offs

Recommended approach

Operational considerations

Scalability

Failure modes

Observability

Future evolution

---

# 15. Vendor Neutrality

The handbook is Claude-first because it reflects the organization's standard platform.

However:

Engineering principles remain vendor-neutral.

Where implementations differ:

Include comparison sections for:

* Claude Code
* GitHub Copilot
* OpenAI Codex

Do not promote one platform over another unless discussing platform-specific capabilities.

---

# 16. Alpha Car Detailing Rules

The handbook uses a single running enterprise example:

**Alpha Car Detailing**

The business serves:

* Walk-in customers
* Corporate fleets
* Government departments
* Insurance companies
* Rental agencies

Every technical example should extend this domain instead of introducing unrelated sample applications.

---

# 17. AI Platform Examples

Examples should follow this order:

1. Concept
2. Vendor-neutral explanation
3. Claude implementation
4. GitHub Copilot comparison (only if materially different)
5. OpenAI Codex comparison (only if materially different)

Avoid duplicating examples solely to change vendor names.

---

# 18. Review Checklist

Before approving a chapter, verify that it:

* Follows the standard chapter template
* Begins with a story-driven introduction
* States learning objectives
* Uses approved terminology
* Includes architecture discussion
* Includes at least one diagram
* Contains production-quality code
* Includes Claude implementation
* Includes vendor comparisons where relevant
* Highlights best practices
* Documents anti-patterns
* Includes Architect's Notes and Enterprise Tips
* Contains exercises and interview questions
* Ends with a summary and further reading
* Has been checked for grammar, spelling, and technical accuracy
* Uses consistent Markdown formatting
* References figures, tables, and code listings correctly

---

# 19. Publishing Checklist

Before merging a chapter into the main branch:

## Technical Validation

* Markdown renders correctly
* Links are valid
* Code blocks are syntactically correct
* Diagrams render successfully
* Images are present and correctly referenced

## Editorial Validation

* Terminology matches Chapter 0
* Voice and tone are consistent
* Formatting complies with standards
* Cross-references are correct
* Section numbering is accurate

## Repository Validation

* File name follows naming convention
* CHANGELOG updated
* ROADMAP updated (if scope changes)
* README updated (if required)
* Pull request reviewed and approved
* Version tag assigned if part of a release

---

# 20. Versioning

This guide follows semantic versioning.

| Version | Status  | Description                                                  |
| ------- | ------- | ------------------------------------------------------------ |
| 1.0     | Initial | Foundational editorial standards                             |
| 1.x     | Minor   | Clarifications and non-breaking additions                    |
| 2.0     | Major   | Structural or policy changes requiring handbook-wide updates |

Any change to this guide must be reviewed before publication, as it affects every chapter in the handbook.

---

## Recommendation

I would treat **Chapter 0 as a living governance document** rather than a normal book chapter. It should not appear in the printed table of contents as reader-facing content, but it should remain in the repository as the editorial standard that all contributors and AI agents follow. This aligns well with the handbook's philosophy of treating documentation with the same discipline as software engineering.
