# Chapter 43 — Instruction Evolution  

**Part VI — Self-Learning AI Systems**  
**Target file:** `book/43-instruction-evolution.md`

Chapter 43 should follow the frozen chapter structure while preserving the handbook’s established terminology: an **Instruction** is a persistent repository-wide engineering standard, distinct from a Prompt, Skill, Steering Note, or Harness behavior. AI development handbook The chapter should also reinforce the Self-Learning Harness principle that the Harness may recommend improvements but **never silently changes governed standards; human approval is mandatory**. AI development handbook

---

## 1. Story-Driven Opening — When the Same Mistake Keeps Returning

### Scenario: Alpha Car Detailing

Open with a recurring engineering problem across multiple Alpha Car Detailing changes.

Possible sequence:

- Developer Agent implements a new fleet-booking endpoint.
- Reviewer repeatedly flags missing idempotency handling.
- Validator repeatedly catches missing architecture dependency checks.
- Security validation identifies authorization inconsistencies.
- Event-contract validation detects inconsistent event metadata.
- Observability checks repeatedly identify missing correlation information.
- Several PRs contain almost identical corrective comments.
- Humans continually make the same corrections after AI-generated implementations.

Introduce the central question:

> If the same governed engineering expectation must be corrected repeatedly, is the implementation consistently wrong, or is the Instruction incomplete, ambiguous, stale, or insufficiently explicit?

Explain that an Adaptive Harness can recognize the pattern.

However, it must not rewrite `CLAUDE.md`, `AGENTS.md`, `copilot-instructions.md`, or equivalent governed Instructions automatically.

Instead:

```text
Observe
   ↓
Correlate
   ↓
Detect Pattern
   ↓
Generate Instruction Proposal
   ↓
Collect Evidence
   ↓
Human Review
   ↓
Approve / Reject
   ↓
Validate
   ↓
Version
   ↓
Release
```

Establish the chapter thesis:

> Instruction Evolution is a governed process for improving persistent engineering standards using accumulated operational evidence.

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define Instruction Evolution.
- Explain why Instructions need controlled evolution.
- Identify evidence that may indicate an Instruction problem.
- Distinguish implementation failures from Instruction failures.
- Detect ambiguous Instructions.
- Detect missing Instructions.
- Detect stale Instructions.
- Detect conflicting Instructions.
- Correlate evidence across builds, reviews, validation, PRs, and human feedback.
- Generate candidate Instruction improvements.
- assign confidence to Instruction proposals.
- Define Instruction ownership.
- design human-approval workflows.
- version Instructions safely.
- test proposed Instruction changes.
- validate backward compatibility.
- roll back problematic Instruction versions.
- deprecate obsolete Instructions.
- record Instruction history for auditability.
- use Harness Memory without treating learned evidence as authoritative Instructions.
- integrate Instruction Evolution into an Adaptive Harness.

---

# 3. Background — Instructions as Governed Engineering Standards

## 3.1 What an Instruction Is

Re-establish the previously frozen concept:

Instructions define persistent repository-wide engineering expectations such as:

- architecture boundaries
- naming conventions
- security requirements
- testing requirements
- logging requirements
- API conventions
- event conventions
- persistence expectations
- observability requirements

The handbook defines Instructions as persistent repository-wide engineering rules, with examples including `CLAUDE.md`, `AGENTS.md`, and Copilot instruction files. AI development handbook

---

## 3.2 Instructions versus Prompts

Clarify:

```text
Instruction
Persistent engineering standard

Prompt
Current task request
```

Example:

**Instruction**

> All state-changing booking operations must support idempotent execution.

**Prompt**

> Implement the corporate fleet booking endpoint.

---

## 3.3 Instructions versus Skills

Clarify:

- Instruction defines **what must be true**.
- Skill defines **how recurring work is performed**.

Example:

```text
Instruction:
All external event consumers must be idempotent.

Skill:
Create Event Consumer
    - deserialize message
    - validate contract
    - check idempotency key
    - process
    - persist checkpoint
```

---

## 3.4 Instructions versus Steering Notes

Explain:

- Instructions are durable standards.
- Steering Notes contain temporary mission context.
- Steering Notes may change per sprint or initiative.
- Instruction Evolution should not promote temporary context into durable standards without governance.

---

## 3.5 Instructions versus Harness Memory

Introduce an important boundary:

```text
Harness Memory
    stores evidence and observations

Instruction
    defines approved engineering standards
```

Memory can inform an Instruction proposal.

Memory cannot become an Instruction simply because the Harness has observed a pattern.

---

# 4. What Is Instruction Evolution?

Define:

> Instruction Evolution is the governed lifecycle through which evidence from engineering execution is analyzed to identify, propose, review, validate, version, and release improvements to persistent engineering Instructions.

Explain that Instruction Evolution is:

- evidence-driven
- proposal-based
- human-governed
- versioned
- testable
- reversible
- auditable

It is **not**:

- autonomous modification of standards
- silent rewriting of repository Instructions
- copying review comments directly into `CLAUDE.md`
- converting every failure into a new Instruction
- allowing Harness Memory to override repository governance

---

# 5. Why Instructions Evolve

## 5.1 Systems Change

Examples:

- new services
- new event types
- new API patterns
- new persistence models
- new security requirements
- new infrastructure

---

## 5.2 Architecture Evolves

Alpha Car Detailing may move from:

```text
Single Booking Service
```

to:

```text
Booking Service
Fleet Service
Payment Service
Notification Service
```

Existing Instructions may no longer provide enough guidance.

---

## 5.3 Engineering Practices Improve

Examples:

- transactional outbox adoption
- idempotent consumers
- OpenTelemetry standards
- structured logging
- architecture-test enforcement

---

## 5.4 Threat Models Change

Security Instructions may need evolution as:

- APIs become externally exposed
- new identity providers are introduced
- service-to-service authentication changes
- sensitive fleet information is introduced

---

## 5.5 Repeated AI Errors Expose Instruction Weaknesses

A particularly important Adaptive Harness signal:

```text
Same implementation mistake
        +
same review correction
        +
same validation failure
        +
multiple independent tasks
        ↓
Possible Instruction gap
```

---

# 6. Learning Signals

Introduce the major evidence sources.

```text
Builds
Reviews
Validation
Evaluation
Pull Requests
Human Feedback
Architecture Findings
Security Findings
Production Evidence
        ↓
Harness Memory
        ↓
Instruction Evolution Analysis
```

---

## 6.1 Build Evidence

Examples:

- recurring compilation problems caused by misunderstood conventions
- dependency-registration mistakes
- project-reference violations

Clarify that a build failure alone rarely justifies Instruction change.

---

## 6.2 Review Evidence

Repeated Reviewer Agent observations may expose vague standards.

Example:

> Repository implementations repeatedly place persistence concerns inside application handlers.

Potential implication:

The architecture Instruction may be too general.

---

## 6.3 Validation Evidence

Examples:

- architecture tests repeatedly fail
- security checks repeatedly identify authorization omissions
- API contract validation repeatedly detects naming inconsistencies
- event-contract validation repeatedly detects missing metadata

---

## 6.4 Evaluation Evidence

Evaluator findings may identify broader outcome mismatches not captured by deterministic gates.

---

## 6.5 Pull-Request Evidence

Build on Chapter 40.

Potential signals:

- repeated requested changes
- recurring reviewer comments
- recurrent architectural corrections
- changes repeatedly added before merge
- PRs rejected for the same standard violation

---

## 6.6 Human Feedback

Examples:

- architect corrections
- security-team guidance
- engineering-lead decisions
- production-support lessons
- developer feedback that an Instruction is confusing

Human feedback should generally receive greater governance weight than purely inferred patterns.

---

# 7. Repeated Violations

## 7.1 Defining a Repeated Violation

Explain that recurrence must be evaluated across:

- tasks
- developers
- AI Agents
- repositories or modules
- time periods
- PRs

---

## 7.2 Frequency versus Significance

A violation occurring frequently may matter.

A critical security violation may matter even if it occurs only once.

Introduce:

```text
Priority =
Frequency
× Severity
× Confidence
× Scope
```

Conceptual model only; avoid pretending it is universal mathematics.

---

## 7.3 Independent Recurrence

Differentiate:

```text
One failed run retried 12 times
```

from:

```text
12 independent tasks producing the same violation
```

The second is stronger evidence of an Instruction problem.

---

# 8. Detecting Ambiguous Instructions

## 8.1 What Makes an Instruction Ambiguous?

Examples:

> Use proper logging.

> APIs should be secure.

> Follow Clean Architecture.

> Handle duplicate events appropriately.

Explain why each leaves substantial interpretation to the Agent.

---

## 8.2 Evidence of Ambiguity

Signals:

- different Agents interpret the same Instruction differently
- reviewers repeatedly request clarification
- implementations satisfy the wording but violate architectural intent
- validators reveal inconsistent outcomes

---

## 8.3 Alpha Car Detailing Example — Observability

Weak Instruction:

> Log important booking operations.

Candidate improvement:

> Every booking command must emit structured logs containing `BookingId`, `CorrelationId`, operation name, result, and failure category. Do not log customer secrets or authentication tokens.

---

# 9. Detecting Missing Instructions

Define a missing Instruction as a recurring governed expectation that exists operationally but is not captured clearly in repository Instructions.

## 9.1 Architecture Example

Repeated finding:

Developers access SQL Server directly from API controllers.

Potential missing Instruction:

> Presentation-layer components must not directly access persistence infrastructure.

---

## 9.2 Security Example

Repeated finding:

New administrative APIs omit explicit authorization policies.

Potential candidate Instruction:

> Administrative endpoints must require named authorization policies; authentication alone is insufficient.

---

## 9.3 Idempotency Example

Repeated finding:

Fleet booking retries create duplicate bookings.

Candidate Instruction:

> All externally retriable state-changing operations must define and enforce an idempotency strategy.

---

# 10. Detecting Stale Instructions

## 10.1 What Is a Stale Instruction?

An Instruction may remain syntactically valid while no longer matching the architecture.

Examples:

- references to removed projects
- obsolete libraries
- deprecated authentication patterns
- old event schemas
- outdated testing tools

---

## 10.2 Staleness Signals

- referenced files no longer exist
- newer approved patterns contradict older guidance
- PRs repeatedly bypass the Instruction
- architecture documentation has changed
- validation gates enforce something different

---

# 11. Detecting Conflicting Instructions

Examples:

Instruction A:

> Application layer must not reference Infrastructure.

Instruction B:

> Application handlers should instantiate repository implementations directly.

Show how both cannot simultaneously be authoritative.

---

## 11.1 Sources of Conflict

- duplicated Instruction files
- local versus repository-level Instructions
- old standards not removed
- platform-specific Instruction overlap
- generated Instructions
- team-specific additions

---

## 11.2 Conflict Resolution

The Harness should:

1. detect the conflict
2. identify sources
3. collect evidence
4. identify owners
5. propose resolution
6. request approval

It should not decide silently which Instruction wins.

---

# 12. Candidate Instruction Improvements

Define the output of the analysis as an **Instruction Change Proposal**.

Example structure:

```yaml
proposal_id: INST-043
instruction_area: idempotency

problem:
  repeated duplicate booking behavior

current_instruction:
  "Prevent duplicate requests."

evidence:
  review_findings: 7
  validation_failures: 4
  pull_requests: 5

proposed_instruction:
  "All externally retriable state-changing operations must
   implement idempotency using a stable request identifier
   and persisted processing state."

confidence: high
owner: architecture-team
status: proposed
```

---

# 13. Evidence and Confidence

## 13.1 Evidence Strength

Possible evidence categories:

- deterministic validation evidence
- Reviewer Agent findings
- Evaluator findings
- PR review history
- human architect feedback
- production incidents

---

## 13.2 Confidence Levels

Example:

```text
Low
Interesting pattern, insufficient evidence.

Medium
Repeated independent evidence exists.

High
Multiple evidence sources consistently support the proposal.
```

---

## 13.3 Confidence Is Not Authority

Critical principle:

```text
High Confidence ≠ Automatic Approval
```

Even extremely strong evidence cannot bypass governance.

---

# 14. Instruction Ownership

Define possible owners:

- Software Architect
- Platform Team
- Security Team
- Engineering Standards Group
- Service Owner
- Repository Maintainer

---

## 14.1 Ownership by Instruction Category

Example:

| Instruction Area | Likely Owner |
| --- | --- |
| Architecture | Architecture team |
| Security | Security team |
| Testing | Engineering/platform team |
| API contracts | API governance team |
| Event contracts | Architecture/platform team |
| Persistence | Architecture/data team |
| Observability | Platform/SRE team |

---

# 15. Human Approval

## 15.1 Mandatory Governance Boundary

Reinforce the handbook’s established principle:

```text
Harness
   may observe
   may analyze
   may recommend
   may generate proposals

Harness
   must not silently modify governed standards
```

This aligns directly with the established Self-Learning Harness rule. AI development handbook

---

## 15.2 Approval Outcomes

```text
Approve
Reject
Request Revision
Defer
Experiment
```

---

## 15.3 Multiple Approvers

High-risk Instruction changes may require:

```text
Architecture Owner
        +
Security Owner
        +
Repository Owner
```

---

# 16. Testing Proposed Instruction Changes

Explain why Instruction text itself requires testing.

## 16.1 Replay Historical Tasks

Use tasks that previously failed.

Ask:

> Would the proposed Instruction have prevented or reduced the original failure?

---

## 16.2 Regression Task Set

Maintain representative engineering scenarios.

Examples:

- create booking endpoint
- create event consumer
- modify database persistence
- add administrative API
- implement retry behavior

---

## 16.3 Agent Interpretation Testing

Run the same task under:

```text
Instruction v3
```

and:

```text
Instruction v4 candidate
```

Compare:

- architecture quality
- validation results
- review findings
- unnecessary constraints
- implementation variability

---

# 17. Compatibility

## 17.1 Backward Compatibility

A new Instruction may unintentionally invalidate existing code.

Example:

New rule:

> Every event consumer must use InboxMessage persistence.

Question:

Does this apply to:

- new consumers only?
- all existing consumers?
- external consumers?
- internal events?

---

## 17.2 Scope Compatibility

Instruction proposals should define:

```text
Applies To
Excludes
Effective From
Migration Requirement
```

---

# 18. Versioning

Treat Instructions as versioned engineering assets.

Example:

```text
instructions/
    architecture-v3.md
    security-v2.md
    api-contracts-v4.md
```

Or Git-based evolution:

```text
Commit
   ↓
Pull Request
   ↓
Review
   ↓
Approval
   ↓
Merge
```

---

## Diagram — Versioning and Rollback

```text
Instruction v1
      ↓
Instruction v2
      ↓
Instruction v3
      ↓
Candidate v4
      ↓
Validation
      ↓
Approved v4
      ↓
Problems detected
      ↓
Rollback to v3
```

---

# 19. Rollback

Explain that evolved Instructions can create unintended consequences.

Possible symptoms:

- excessive implementation complexity
- reduced Agent effectiveness
- contradictory behavior
- widespread validation failures
- over-constrained generation

Rollback should preserve:

- previous version
- reason
- approving authority
- evidence
- timestamp
- affected scope

---

# 20. Deprecation

Differentiate:

```text
Active
Deprecated
Retired
Superseded
```

Explain why obsolete Instructions should not simply disappear.

Example:

> Direct SQL access from service code is deprecated and superseded by repository-based persistence requirements.

---

# 21. Auditability

Every Instruction proposal should retain:

- proposal ID
- triggering evidence
- original wording
- proposed wording
- author or proposing Agent
- confidence
- owner
- reviewers
- approval decision
- effective version
- rollback history
- linked PR
- validation results

---

# 22. Harness Memory and Instruction Evolution

Explain the relationship carefully.

```text
Engineering Events
      ↓
Harness Memory
      ↓
Pattern Detection
      ↓
Instruction Evolution
      ↓
Proposal
```

Harness Memory stores:

- recurring findings
- validation failures
- review outcomes
- PR evidence
- accepted corrections
- human feedback

But:

```text
Harness Memory ≠ Instruction Authority
```

This distinction prevents learned observations from becoming accidental policy.

---

# 23. Architecture Discussion — Instruction Evolution Service

Introduce a conceptual subsystem:

```text
Instruction Evolution Engine

Inputs
 ├── Harness Memory
 ├── Build Evidence
 ├── Review Findings
 ├── Validator Results
 ├── Evaluation Results
 ├── PR History
 └── Human Feedback

Processing
 ├── Pattern Detection
 ├── Instruction Gap Detection
 ├── Conflict Detection
 ├── Staleness Detection
 ├── Evidence Correlation
 ├── Confidence Assessment
 └── Proposal Generation

Outputs
 ├── Instruction Proposal
 ├── Evidence Package
 ├── Compatibility Analysis
 └── Approval Request
```

---

# 24. Diagram — Instruction Evolution Lifecycle

```text
Operational Evidence
        ↓
Harness Memory
        ↓
Pattern Detection
        ↓
Instruction Analysis
        ↓
Candidate Improvement
        ↓
Evidence Assessment
        ↓
Human Review
        ↓
Approved?
   ┌────┴─────┐
   │          │
  No         Yes
   │          │
Archive    Validate
              ↓
            Version
              ↓
             Release
              ↓
            Observe
              ↓
         Future Evidence
```

---

# 25. Diagram — Evidence-to-Instruction Proposal Flow

```text
Build Failures ─────────┐
Review Findings ────────┤
Validation Failures ────┤
PR Evidence ────────────┤
Human Feedback ─────────┤
Security Findings ──────┤
Architecture Findings ──┘
             ↓
       Evidence Store
             ↓
     Correlation Engine
             ↓
      Pattern Detection
             ↓
Instruction Gap Analysis
             ↓
   Candidate Instruction
             ↓
      Evidence Package
             ↓
      Proposal Created
```

---

# 26. Diagram — Approval and Governance Flow

```text
Harness Proposal
      ↓
Instruction Owner
      ↓
Technical Review
      ↓
Architecture Review
      ↓
Security Review
   (if required)
      ↓
Approve / Reject / Revise
      ↓
Validation
      ↓
Versioned Pull Request
      ↓
Human Approval
      ↓
Merge
```

---

# 27. Diagram — Instruction Evolution Inside the Harness

```text
                  ┌────────────────────────┐
                  │      User / Task       │
                  └───────────┬────────────┘
                              ↓
                          Lead Agent
                              ↓
                        Developer Agent
                              ↓
                         Reviewer Agent
                              ↓
                         Validator Agent
                              ↓
                         Evaluator Agent
                              ↓
                         Pull Request
                              ↓
                    ┌───────────────────┐
                    │ Engineering       │
                    │ Evidence          │
                    └─────────┬─────────┘
                              ↓
                       Harness Memory
                              ↓
                    Learning Engine
                              ↓
                 Instruction Evolution
                              ↓
                 Candidate Improvement
                              ↓
                     Human Governance
                              ↓
                    Approved Instruction
                              ↓
                       Future Harness Run
```

Connect this to the established Harness flow where the Lead, Developer, Reviewer, Validator, Evaluator, learning capability, and knowledge layer form a feedback cycle. AI development handbook

---

# 28. Hands-on Example — Alpha Car Detailing

## Scenario

Repeated implementation issue:

Corporate fleet booking commands occasionally create duplicate reservations after retries.

### Existing Instruction

```text
Prevent duplicate booking requests.
```

### Evidence

Across several tasks:

- Reviewer Agent identifies missing idempotency.
- Validator detects duplicate-record integration-test failures.
- PR reviewers request retry-safe behavior.
- Production-like tests reproduce duplicate booking creation.
- Architects repeatedly recommend persisted idempotency records.

---

## Proposed Improvement

```text
All externally retriable state-changing operations must
implement idempotency using a stable request identifier.

Idempotency state must be persisted before the operation
is considered successfully completed.

Repeated requests using the same identifier must return
the previously committed outcome rather than creating a
second business transaction.
```

---

## Evidence Package

Include:

- affected PRs
- failed tests
- Reviewer findings
- Validator evidence
- human architect comments
- implementation examples

---

## Governance

Proposed owner:

```text
Architecture Team
```

Required reviewers:

```text
Architecture
Backend Engineering
Platform Engineering
```

---

## Validation

Replay:

- initial fleet-booking implementation
- retry behavior
- timeout recovery
- duplicate API request
- concurrent retry

Compare old versus proposed Instruction.

---

# 29. Alpha Car Detailing Instruction Evolution Examples

Include a focused set of examples across required domains.

## 29.1 Architecture

Weak:

> Follow Clean Architecture.

Improved candidate:

> Domain and Application projects must not reference Infrastructure or Presentation projects.

---

## 29.2 Security

Weak:

> Secure administrative endpoints.

Improved candidate:

> Administrative APIs must require explicit authorization policies. Authentication alone is not sufficient.

---

## 29.3 Testing

Weak:

> Add tests.

Improved candidate:

> Every state-changing application command must include success, validation-failure, authorization-failure, and idempotency tests where applicable.

---

## 29.4 API Contracts

Weak:

> Follow API standards.

Improved candidate:

> Public booking endpoints must return standardized error contracts and must not expose internal exception messages.

---

## 29.5 Event Contracts

Weak:

> Events should include metadata.

Improved candidate:

Required event metadata:

```text
EventId
EventType
OccurredAt
CorrelationId
SchemaVersion
```

---

## 29.6 Persistence

Weak:

> Save changes safely.

Improved candidate:

> Business-state changes and integration-event publication requirements must use a transactionally consistent persistence strategy such as the transactional outbox where cross-boundary publication is required.

---

## 29.7 Idempotency

Define requirements for:

- APIs
- message consumers
- retries
- duplicate events
- reprocessing

---

## 29.8 Observability

Require:

- correlation IDs
- structured logging
- trace propagation
- operational metrics
- appropriate error context

---

# 30. Claude Example

Show how Claude Code may consume an existing Instruction set and how an Adaptive Harness could separately produce a proposed Instruction patch.

Keep separate:

```text
CLAUDE.md
```

from:

```text
proposals/instruction-evolution/INST-043.md
```

Emphasize that Claude does not directly modify governed Instructions without explicit authorization.

---

# 31. GitHub Copilot Comparison

Discuss equivalent concepts using repository-level Copilot instructions.

Focus on:

- repository instructions
- pull-request evidence
- review findings
- proposed updates
- governance

Avoid turning the section into a product tutorial.

The handbook remains vendor-neutral even though Claude-first examples are preferred. AI development handbook

---

# 32. Codex Comparison

Discuss:

- repository guidance
- task context
- repeated implementation patterns
- candidate Instruction changes
- external Harness governance

Emphasize that Instruction Evolution belongs primarily to the **engineering system around the Agent**, rather than to a particular model.

---

# 33. Best Practices

Cover practices such as:

- require multiple evidence sources where possible
- distinguish recurrence from retries of one failure
- prioritize severity as well as frequency
- keep proposed Instructions precise
- make Instructions testable
- define scope explicitly
- identify owners
- validate changes before rollout
- version all governed Instructions
- maintain rollback paths
- separate evidence from authority
- retain rejected proposals
- monitor effects after rollout
- periodically review stale Instructions
- prefer improving existing Instructions over continuously adding new ones

---

# 34. Anti-patterns

## 34.1 Silent Instruction Mutation

Harness rewrites `CLAUDE.md` after detecting failures.

Why dangerous:

- uncontrolled policy change
- no ownership
- no review
- difficult rollback
- unpredictable future AI behavior

---

## 34.2 One Failure, One New Instruction

Every failure creates another repository rule.

Result:

```text
Instruction Explosion
```

---

## 34.3 Review Comment Copying

Copying reviewer comments directly into Instructions without identifying the underlying engineering principle.

---

## 34.4 Harness Memory as Policy

Treating frequently observed patterns as automatically authoritative.

---

## 34.5 Conflicting Instruction Accumulation

Adding new guidance without removing or superseding obsolete guidance.

---

## 34.6 Unversioned Instruction Changes

Standards change without traceability.

---

## 34.7 Over-Specific Instructions

Encoding one implementation detail as a permanent enterprise rule when it applies only to a single scenario.

---

## 34.8 Self-Reinforcing Learning

AI-generated behavior produces evidence which the same AI uses to justify making that behavior the standard.

Discuss circular reinforcement risk.

---

# 35. Architect's Notes

Suggested callouts:

### Architect's Note — Instructions Are Policy

Treat important Instruction changes with the same seriousness as architecture standards.

### Architect's Note — Repetition Is Evidence, Not Proof

Repeated failure may indicate:

- poor Instructions
- poor Skills
- poor prompts
- inadequate Repository Intelligence
- model limitations
- missing deterministic gates

Instruction Evolution must diagnose before prescribing.

### Architect's Note — Smaller Instruction Sets Can Be Better

Do not equate maturity with more Instructions.

---

# 36. Enterprise Tips

Include guidance such as:

- assign explicit owners to Instruction domains
- link Instruction changes to architecture decisions
- require security review for security-related Instructions
- maintain an Instruction changelog
- monitor post-release effects
- create organization-wide versus repository-level scopes
- build reusable Instruction proposal templates
- integrate approval with existing pull-request workflows

---

# 37. Decision Points

Include decision frameworks.

## Decision Point 1 — Is This an Instruction Problem?

Ask:

```text
Is the expectation persistent?
Is it repository-wide?
Is it governed?
Is the violation recurring?
Is the existing Instruction missing or unclear?
Would a Skill change solve the problem instead?
Would better Repository Intelligence solve it?
Would a validation gate solve it?
```

---

## Decision Point 2 — Modify or Add?

Prefer:

```text
Clarify existing Instruction
```

before:

```text
Create another Instruction
```

---

## Decision Point 3 — Automatic or Human Approved?

Answer:

Governed Instruction changes require human approval.

---

## Decision Point 4 — Local or Enterprise Scope?

Determine whether the requirement applies to:

- one service
- one repository
- one platform
- the entire enterprise

---

# 38. Exercises

### Exercise 1 — Detect an Instruction Gap

Given repeated Alpha Car Detailing review findings, identify whether the root cause is:

- Prompt
- Skill
- Instruction
- Repository Intelligence
- validation gate

---

### Exercise 2 — Improve an Ambiguous Instruction

Rewrite:

> Implement secure APIs.

into a precise, testable Instruction.

---

### Exercise 3 — Evidence Analysis

Given:

- six PR review comments
- three validator failures
- one production defect

construct an Instruction proposal and confidence assessment.

---

### Exercise 4 — Conflict Detection

Resolve two contradictory persistence Instructions.

---

### Exercise 5 — Versioning

Design an Instruction versioning and rollback model.

---

### Exercise 6 — Governance Workflow

Design an approval workflow for a new security Instruction.

---

### Exercise 7 — Idempotency

Create an enterprise-grade idempotency Instruction for Alpha Car Detailing covering:

- HTTP requests
- event consumers
- retries
- duplicate events

---

### Exercise 8 — Observability

Design an evolved observability Instruction based on repeated missing-correlation findings.

---

# 39. Interview Questions

Include questions such as:

1. What is Instruction Evolution?
2. Why should Instructions evolve?
3. What evidence can trigger an Instruction proposal?
4. How do you distinguish an Instruction problem from a Skill problem?
5. What is an ambiguous Instruction?
6. How can an Adaptive Harness detect a missing Instruction?
7. What makes an Instruction stale?
8. How should conflicting Instructions be handled?
9. Why should repeated violations not automatically create Instructions?
10. What role does Harness Memory play?
11. Why is confidence different from authority?
12. Who should own Instructions?
13. Why is human approval mandatory?
14. How should Instructions be versioned?
15. How do you validate a proposed Instruction?
16. What compatibility risks can Instruction changes introduce?
17. When should an Instruction be deprecated?
18. How should Instruction rollback work?
19. Why must Instruction Evolution remain auditable?
20. How can self-reinforcing AI learning create governance risks?

---

# 40. Chapter Summary

Summarize the central ideas:

- Instructions are durable governed engineering standards.
- Engineering evidence may reveal weaknesses in those standards.
- Repeated violations are valuable learning signals.
- Adaptive Harnesses can detect ambiguous, missing, stale, and conflicting Instructions.
- Builds, reviews, validation, PRs, and human feedback provide evidence.
- Evidence should generate proposals, not silent changes.
- Confidence assists prioritization but does not grant authority.
- Instruction ownership must be explicit.
- Proposed changes require testing.
- Compatibility must be evaluated.
- Instructions should be versioned.
- Rollback and deprecation should be supported.
- Harness Memory supplies evidence but does not become policy.
- Instruction changes must remain auditable.
- Humans retain final governance authority.

Conclude with:

> A mature Adaptive Harness does not decide what the engineering standards should become. It helps the organization discover where its standards need improvement, supplies the evidence, proposes a controlled change, and leaves authority with the humans responsible for the system.

---

# 41. Further Reading

Organize references around:

- policy-as-code
- software architecture governance
- architecture decision records
- configuration versioning
- software quality management
- continuous improvement
- secure software development
- API governance
- event-contract governance
- observability standards
- human-in-the-loop AI governance
- change-management systems
- feedback-driven engineering systems

The final chapter should retain the handbook’s frozen professional structure—including diagrams, hands-on examples, Claude-first treatment, meaningful GitHub Copilot/Codex comparisons, best practices, anti-patterns, Architect’s Notes, Enterprise Tips, Decision Points, exercises, interview questions, summary, and further reading. AI development handbook

**Chapter 43 outline is complete**
