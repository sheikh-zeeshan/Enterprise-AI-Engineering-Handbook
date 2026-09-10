Below is the detailed outline for **Chapter 10 — Steering Notes**, following the frozen chapter template, the terminology established in Chapters 1–9, and the agreed project architecture and editorial standards. It is grounded in the handbook's established concepts, including the distinction between Instructions, Skills, Prompts, Roles, Steering Notes, and the AI Harness.

---

# Chapter 10 — Steering Notes

**Part II — Repository Intelligence**

**Target File**

`book/10-steering-notes.md`

## Chapter Objective

Explain **Steering Notes** as temporary, human-maintained mission guidance that communicates current priorities, constraints, risks, dependencies, and expected outcomes to AI agents working within an enterprise repository.

Demonstrate how Steering Notes align AI agents with current engineering objectives without modifying permanent engineering standards.

Reinforce the repository intelligence hierarchy established in previous chapters:

* Instructions define persistent engineering standards.
* Skills define reusable engineering workflows.
* Prompts define today's task.
* Roles define responsibilities and authority.
* Steering Notes define temporary mission direction.

---

# 1. Story-driven Opening

### Purpose

Introduce Steering Notes through a realistic enterprise development scenario where multiple AI agents must understand not only the requested task, but also the current priorities and constraints of the engineering organization.

### Key Topics

* Sprint planning
* Current engineering priorities
* Temporary business goals
* Human guidance
* Why permanent instructions are insufficient

### Alpha Car Detailing Example

The Product Owner announces:

> "Corporate Fleet Booking is our highest priority for this sprint. All development effort should focus on booking workflows. Loyalty features are postponed until the next release."

Although repository Instructions remain unchanged, the Lead Agent must understand this temporary direction before delegating work.

### Recommended Diagram

Mission Flow

```text
Business Goal
        ↓
Steering Note
        ↓
Lead Agent
        ↓
Developer
        ↓
Reviewer
        ↓
Validator
```

---

# 2. Learning Objectives

### Purpose

Define the knowledge readers should gain after completing the chapter.

### Key Topics

* Understand Steering Notes
* Know when they should be created
* Distinguish them from other repository intelligence artifacts
* Understand lifecycle and governance
* Learn enterprise implementation patterns

### Alpha Car Detailing Example

The engineering team prepares AI agents for the next fleet booking sprint using a new Steering Note.

### Recommended Table

Learning Objectives Matrix

---

# 3. Background

## Purpose

Explain why enterprise AI development requires temporary guidance beyond permanent repository documentation.

### Key Topics

* Software priorities constantly change
* Business context evolves
* Sprint objectives change
* Risks emerge
* Deadlines move
* AI requires awareness of current context

### Alpha Car Detailing Example

Repository standards remain constant while release priorities change weekly.

### Recommended Diagram

Permanent Repository Knowledge vs Temporary Mission Guidance

---

# 4. Concepts

---

## 4.1 What is a Steering Note?

### Purpose

Define Steering Notes precisely.

### Key Topics

* Human-authored
* Temporary
* Mission guidance
* Repository-aware
* Read by AI
* Not executable

### Alpha Car Detailing Example

Current sprint focuses only on Corporate Fleet Booking.

### Recommended Table

Definition Summary

---

## 4.2 Why Steering Notes Matter

### Purpose

Explain why enterprise AI systems require temporary direction.

### Key Topics

* Prevent wasted effort
* Align AI with business priorities
* Communicate temporary constraints
* Reduce conflicting implementations

### Alpha Car Detailing Example

AI avoids spending time improving loyalty rewards during the fleet booking sprint.

### Recommended Diagram

Without Steering Notes vs With Steering Notes

---

## 4.3 Steering Notes vs Instructions

### Purpose

Clarify the distinction.

### Key Topics

* Permanent vs temporary
* Standards vs mission
* Repository-wide vs initiative-specific

### Alpha Car Detailing Example

Instructions require Clean Architecture.

Steering Note prioritizes Fleet Booking.

### Recommended Table

Comparison Matrix

---

## 4.4 Steering Notes vs Skills

### Purpose

Show different responsibilities.

### Key Topics

* Skills describe how
* Steering Notes describe current priorities

### Alpha Car Detailing Example

Skill:

Create Booking API

Steering Note:

Booking API is highest priority.

### Recommended Table

Skills vs Steering Notes

---

## 4.5 Steering Notes vs Prompts

### Purpose

Clarify mission versus task.

### Key Topics

* Prompt defines work
* Steering Note provides context

### Alpha Car Detailing Example

Prompt:

Implement Fleet Booking Endpoint.

Steering Note:

Avoid modifying Payment Service during this sprint.

### Recommended Diagram

Prompt Inside Mission Context

---

## 4.6 Steering Notes vs Roles

### Purpose

Explain interaction.

### Key Topics

* Roles define authority
* Steering Notes define temporary direction

### Alpha Car Detailing Example

Architect approves architecture.

Steering Note explains release priorities.

### Recommended Table

Roles vs Steering Notes

---

## 4.7 Steering Notes vs Knowledge Sources

### Purpose

Clarify repository intelligence boundaries.

### Key Topics

* Knowledge Sources contain facts
* Steering Notes contain current direction

### Alpha Car Detailing Example

Architecture document explains services.

Steering Note identifies services in scope.

### Recommended Table

Knowledge Sources vs Steering Notes

---

# 5. Architecture Discussion

---

## 5.1 Steering Note Scope

### Purpose

Explain appropriate scope.

### Key Topics

* Sprint
* Release
* Initiative
* Migration
* Incident response

### Alpha Car Detailing Example

Sprint 24 Fleet Booking Initiative

### Recommended Diagram

Scope Timeline

---

## 5.2 Current Priorities

### Purpose

Explain communicating priorities.

### Key Topics

* High priority
* Deferred work
* Nice-to-have features

### Alpha Car Detelling Example

Fleet Booking first

Analytics postponed

### Recommended Table

Priority Matrix

---

## 5.3 Business Context

### Purpose

Explain business reasoning.

### Key Topics

* Customer commitments
* Revenue goals
* Regulatory deadlines

### Alpha Car Detailing Example

Corporate customer launch date.

---

## 5.4 Constraints

### Purpose

Document temporary engineering limitations.

### Key Topics

* Budget
* Time
* Services
* Technology
* Infrastructure

### Alpha Car Detailing Example

No database schema changes.

---

## 5.5 Risks

### Purpose

Identify current engineering risks.

### Key Topics

* Performance
* Security
* Schedule
* Integration
* Dependencies

### Alpha Car Detailing Example

Fleet pricing service remains unstable.

---

## 5.6 Dependencies

### Purpose

Explain external dependencies.

### Key Topics

* APIs
* Teams
* Vendors
* Infrastructure

### Alpha Car Detailing Example

Fleet Management API must be delivered first.

---

## 5.7 Acceptance Expectations

### Purpose

Define mission-level completion expectations.

### Key Topics

* Testing
* Documentation
* Reviews
* Demonstrations

### Alpha Car Detailing Example

Feature must pass end-to-end booking validation.

---

## 5.8 Decision Boundaries

### Purpose

Clarify what AI may decide.

### Key Topics

* Allowed decisions
* Escalations
* Human approvals

### Alpha Car Detailing Example

AI may optimize APIs but cannot redesign booking architecture.

---

## 5.9 Human Ownership

### Purpose

Define ownership.

### Key Topics

* Tech Lead
* Architect
* Engineering Manager

### Alpha Car Detailing Example

Steering Note maintained by Technical Lead.

---

## 5.10 Agent Permissions

### Purpose

Define AI authority.

### Key Topics

* Read
* Follow
* Suggest
* Never silently modify

### Alpha Car Detailing Example

Agent proposes updates instead of editing Steering Notes.

---

## 5.11 Update Frequency

### Purpose

Discuss lifecycle.

### Key Topics

* Sprint
* Weekly
* Release
* Incident

---

## 5.12 Versioning

### Purpose

Track evolution.

### Key Topics

* Revision history
* Change tracking
* Auditability

---

## 5.13 Approval

### Purpose

Explain governance.

### Key Topics

* Human review
* Architecture approval
* Release approval

---

## 5.14 Expiration

### Purpose

Prevent obsolete guidance.

### Key Topics

* End of sprint
* Release complete
* Automatic retirement

---

## 5.15 Conflict Handling

### Purpose

Resolve conflicting guidance.

### Key Topics

* Instructions override Steering Notes
* Human escalation
* Architecture review

### Alpha Car Detailing Example

Steering Note requests bypassing tests, but repository Instructions require mandatory testing.

---

# 6. Professional Diagrams

### Purpose

Visualize Steering Notes within Repository Intelligence.

### Diagrams

* Repository Intelligence hierarchy
* AI decision flow
* Steering Note lifecycle
* Sprint guidance flow
* Approval workflow
* Conflict resolution
* Repository document relationships
* Mission timeline

---

# 7. Hands-on Example

### Purpose

Build a complete Steering Note.

### Key Topics

* Current sprint
* Priorities
* Constraints
* Risks
* Services
* Deadlines

### Alpha Car Detailing Example

Corporate Fleet Booking Sprint Steering Note

### Recommended Artifact

Example `Search/steering-note.md`

---

# 8. Claude Code Example

### Purpose

Show how Claude Code consumes Steering Notes.

### Key Topics

* Reading repository guidance
* Following temporary priorities
* Respecting Instructions
* Suggesting updates

### Alpha Car Detailing Example

Claude reads `Search/steering-note.md` before implementing Fleet Booking.

### Recommended Diagram

Claude Repository Context Flow

---

# 9. GitHub Copilot Comparison

### Purpose

Explain practical differences.

### Key Topics

* Context limitations
* Repository awareness
* Manual inclusion
* Workflow considerations

### Alpha Car Detailing Example

Supplying Steering Notes as additional repository context.

### Recommended Table

Claude vs GitHub Copilot

---

# 10. OpenAI Codex Comparison

### Purpose

Explain Codex usage.

### Key Topics

* Task context
* Repository awareness
* Engineering workflows

### Alpha Car Detailing Example

Codex reads Steering Notes before code generation.

### Recommended Table

Claude vs Codex

---

# 11. Best Practices

### Purpose

Summarize recommended enterprise usage.

### Key Topics

* Keep concise
* Update regularly
* Clearly define priorities
* Assign ownership
* Include expiration
* Separate permanent standards from temporary guidance
* Require human approval
* Keep AI informed without overloading context

### Alpha Car Detailing Example

Sprint Steering Note maintained weekly by the Tech Lead.

### Recommended Table

Best Practice Checklist

---

# 12. Anti-patterns

### Purpose

Highlight common mistakes.

### Key Topics

* Duplicating Instructions
* Permanent information in Steering Notes
* Missing expiration dates
* Conflicting guidance
* AI editing Steering Notes directly
* Unclear ownership
* Excessive detail
* Outdated mission guidance

### Alpha Car Detailing Example

Old release priorities causing AI to work on deferred features.

### Recommended Table

Anti-pattern → Consequence → Recommended Fix

---

# 13. Architect's Notes

### Purpose

Provide enterprise architecture insights.

### Key Topics

* Mission guidance is temporary
* Repository standards remain authoritative
* Steering Notes improve alignment, not governance
* Avoid policy duplication

### Alpha Car Detailing Example

Architect reviews Steering Notes at sprint planning.

---

# 14. Enterprise Tips

### Purpose

Offer practical implementation advice.

### Key Topics

* Store in predictable location
* Review during sprint planning
* Archive expired notes
* Keep notes focused and actionable
* Integrate with AI harness

### Alpha Car Detailing Example

`Search/steering-note.md` updated at each sprint kickoff.

---

# 15. Decision Points

### Purpose

Help readers evaluate implementation choices.

### Key Topics

* When to create a Steering Note
* Who owns it
* How frequently to update
* When AI may propose changes
* When to retire guidance

### Alpha Car Detailing Example

Deciding whether a production hotfix requires a dedicated Steering Note.

### Recommended Table

Decision Matrix

---

# 16. Exercises

### Purpose

Reinforce learning.

### Exercises

* Write a Steering Note for a Fleet Booking sprint.
* Define release constraints.
* Identify risks and dependencies.
* Separate permanent Instructions from temporary guidance.
* Propose a Steering Note update without modifying the original.

---

# 17. Interview Questions

### Purpose

Assess understanding.

### Sample Topics

* What is a Steering Note?
* Why shouldn't Steering Notes replace Instructions?
* How do Steering Notes differ from Prompts?
* Who owns Steering Notes?
* When should they expire?
* How should AI handle conflicting guidance?
* How does a harness use Steering Notes?
* Why are Steering Notes important for enterprise AI governance?

---

# 18. Chapter Summary

### Purpose

Recap key concepts.

### Key Topics

* Definition of Steering Notes
* Temporary mission guidance
* Repository intelligence hierarchy
* Governance
* Lifecycle
* Human ownership
* AI consumption
* Enterprise implementation

### Alpha Car Detailing Example

Fleet Booking sprint recap demonstrating how Steering Notes kept all AI agents aligned with current business priorities.

---

# 19. Further Reading

### Purpose

Prepare readers for the next chapter.

### Suggested Topics

* Repository Knowledge Sources
* AI Harness Architecture
* Enterprise AI Governance
* Human Approval Workflows
* Repository Documentation Strategy
* Organizational Knowledge Management

### Alpha Car Detailing Example

Transition from temporary Steering Notes to long-lived Knowledge Sources that preserve architectural and business knowledge across releases.

---

## Chapter Flow Summary

1. Story-driven Opening
2. Learning Objectives
3. Background
4. Concepts
5. Architecture Discussion
6. Professional Diagrams
7. Hands-on Example
8. Claude Code Example
9. GitHub Copilot Comparison
10. OpenAI Codex Comparison
11. Best Practices
12. Anti-patterns
13. Architect's Notes
14. Enterprise Tips
15. Decision Points
16. Exercises
17. Interview Questions
18. Chapter Summary
19. Further Reading

---

**Chapter 10 outline is complete.**
