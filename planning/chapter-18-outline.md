# Chapter 18 — Evaluator Agent

**Part III — Harness Engineering**

**Target file:** `book/18-evaluator-agent.md`

---

## 1. Story-Driven Opening — When Passing Tests Is Not Enough

### Scenario: Alpha Car Detailing Corporate Fleet Booking

Introduce a completed feature for Alpha Car Detailing:

> Allow corporate fleet managers to create recurring vehicle detailing bookings for multiple vehicles.

The implementation has already moved through:

```text
Developer
   ↓
Reviewer
   ↓
Validator
```

The evidence shows:

- The solution builds successfully.
- Unit tests pass.
- Integration tests pass.
- Architecture rules pass.
- API contracts remain compatible.
- Security checks pass.
- Static analysis passes.
- The Reviewer found no blocking maintainability issues.

However, when the implementation is compared with the actual business goal, several problems appear:

- Recurring bookings work for only one vehicle at a time.
- The requested fleet-level operation was not implemented.
- A technically unnecessary scheduling abstraction was added.
- Cancellation behavior required by the acceptance criteria is missing.
- The API is technically correct but inconvenient for the intended corporate workflow.

Establish the central problem:

> A solution can be technically valid without being the right solution.

Introduce the Evaluator Agent as the role responsible for determining whether the implementation actually satisfies the requested goal, acceptance criteria, business expectations, and overall outcome quality.

### Core Principle

> **The Evaluator Agent assesses outcome quality and goal satisfaction. It does not replace deterministic validation, the Reviewer Agent, or human approval.**

Introduce the normal Harness progression:

```text
Developer
   ↓
Reviewer
   ↓
Validator
   ↓
Evaluator
   ↓
Human Approval
```

---

# 2. Learning Objectives

By the end of this chapter, readers should be able to:

- Explain the purpose of the Evaluator Agent.
- Distinguish evaluation from review and deterministic validation.
- Define the Evaluator Agent's authority and limitations.
- Evaluate implementations against the original engineering goal.
- Evaluate explicit and implicit acceptance criteria.
- Assess business outcomes rather than only technical correctness.
- Consume evidence from Developers, Reviewers, Validators, and Harness gates.
- Design structured evaluation criteria.
- Use scoring without allowing scores to hide critical failures.
- Classify evaluation outcomes as pass, fail, partial, or inconclusive.
- Detect technically valid but incomplete solutions.
- Detect over-implementation.
- Detect missed or misunderstood requirements.
- Assess risks and engineering trade-offs.
- Produce auditable evaluation reports.
- Route evaluation findings back through the Harness.
- Define the boundary between AI evaluation and human approval.
- Implement an Evaluator Agent using Claude Code.
- Compare Claude Code, GitHub Copilot, and OpenAI Codex approaches to evaluation.

---

# 3. Background — Why Evaluation Exists

## 3.1 Technical Correctness Is Only One Dimension

Explain the difference between:

- code that compiles,
- code that passes tests,
- code that follows architecture rules,
- code that satisfies the task,
- code that satisfies the business goal.

Show why deterministic validation answers questions such as:

> Did the implementation satisfy measurable engineering conditions?

while evaluation asks:

> Did we build the right thing?

---

## 3.2 The Gap Between Requirements and Implementation

Discuss common causes:

- incomplete interpretation,
- ambiguous requirements,
- technically narrow implementations,
- accidental scope reduction,
- gold-plating,
- misunderstood workflows,
- failure to consider business context,
- passing tests written around the wrong interpretation.

---

## 3.3 Why the Developer Should Not Perform Final Evaluation

Discuss independence.

The Developer may:

- verify implementation assumptions,
- run tests,
- self-check acceptance criteria.

But should not be the final authority for evaluating its own outcome.

Introduce separation of responsibilities:

```text
Developer   → Implements
Reviewer    → Reviews engineering quality
Validator   → Verifies objective conditions
Evaluator   → Assesses outcome and goal satisfaction
Human       → Approves
```

---

# 4. What Is an Evaluator Agent?

Define the Evaluator Agent.

The Evaluator Agent is an independent Harness role responsible for assessing whether a completed implementation:

- satisfies the original goal,
- satisfies acceptance criteria,
- achieves expected business outcomes,
- demonstrates sufficient overall quality,
- stays within intended scope,
- addresses important risks,
- represents a complete solution.

Clarify that the Evaluator primarily evaluates **results**, not implementation mechanics alone.

---

# 5. Evaluator Agent Responsibilities

Cover the Evaluator Agent's major responsibilities.

## 5.1 Understand the Original Goal

Read:

- task prompt,
- Steering Notes,
- acceptance criteria,
- applicable Instructions,
- approved plan,
- architectural decisions,
- relevant business context.

---

## 5.2 Consume Implementation Evidence

Review evidence produced by:

- Developer Agent,
- Reviewer Agent,
- Validator Agent,
- deterministic gates,
- test systems,
- architecture checks,
- security tools,
- contract validators.

---

## 5.3 Assess Goal Satisfaction

Determine whether the completed work actually accomplishes the requested objective.

---

## 5.4 Assess Acceptance Criteria

Evaluate each acceptance criterion individually.

---

## 5.5 Assess Business Outcome

Determine whether the feature behaves meaningfully for the business scenario.

---

## 5.6 Assess Overall Quality

Examine whether the implementation is:

- complete,
- coherent,
- usable,
- maintainable enough for the requested scope,
- appropriately scoped,
- aligned with enterprise expectations.

---

## 5.7 Identify Missing Work

Detect:

- missing requirements,
- partial workflows,
- skipped acceptance criteria,
- unhandled edge cases,
- missing integration behavior.

---

## 5.8 Detect Over-Implementation

Identify unnecessary:

- abstractions,
- services,
- endpoints,
- dependencies,
- infrastructure,
- domain complexity,
- refactoring outside the approved scope.

---

## 5.9 Assess Risk and Trade-Offs

Identify residual risks that may remain even when validation passes.

---

## 5.10 Produce an Evaluation Result

Return a structured, evidence-backed result to the Harness.

---

# 6. Evaluator Agent Authority and Limits

## 6.1 What the Evaluator Agent May Do

The Evaluator may:

- inspect completed implementation evidence,
- inspect code where necessary,
- compare outcomes with goals,
- compare implementation with acceptance criteria,
- identify missing or excessive scope,
- classify evaluation outcomes,
- describe residual risks,
- recommend additional work,
- request reevaluation after changes.

---

## 6.2 What the Evaluator Agent Must Not Do

The Evaluator must not:

- silently rewrite requirements,
- approve its own standards,
- override deterministic gate failures,
- replace the Reviewer,
- replace the Validator,
- change Steering Notes without authorization,
- introduce new requirements as if they were original requirements,
- approve production deployment,
- replace human approval,
- modify the implementation while acting as the independent Evaluator.

---

## 6.3 Separation Between Evaluation and Implementation

Explain why an Evaluator should normally return findings to the Lead Agent rather than fixing issues itself.

---

# 7. Evaluator Agent Responsibility Boundary

Include a professional responsibility-boundary diagram.

```text
                    ┌────────────────────────┐
                    │     Original Goal      │
                    │ Acceptance Criteria    │
                    │ Business Expectations  │
                    └───────────┬────────────┘
                                │
                                ▼
┌──────────────┐      ┌──────────────────────┐
│ Review       │─────▶│   Evaluator Agent    │
│ Findings     │      │                      │
└──────────────┘      │ Assess Outcome       │
                      │ Assess Completeness  │
┌──────────────┐      │ Assess Goal Fit      │
│ Validation   │─────▶│ Assess Business Fit  │
│ Evidence     │      └──────────┬───────────┘
└──────────────┘                 │
                                 ▼
                       ┌─────────────────────┐
                       │ Evaluation Result   │
                       └──────────┬──────────┘
                                  │
                    ┌─────────────┴─────────────┐
                    ▼                           ▼
             Harness Rework               Human Approval
```

Explain what lies inside and outside the Evaluator's responsibility.

---

# 8. Evaluator Versus Reviewer

Provide a direct comparison.

| Area | Reviewer Agent | Evaluator Agent |
| --- | --- | --- |
| Primary question | Is the implementation well engineered? | Does the implementation satisfy the intended outcome? |
| Main focus | Code and design quality | Goal and outcome quality |
| Architecture | Reviews alignment and maintainability | Considers whether architecture supports the requested outcome |
| Code quality | Primary concern | Supporting evidence |
| Requirements | Checks implementation consistency | Evaluates actual satisfaction |
| Acceptance criteria | Reviews interpretation | Assesses final fulfillment |
| Business outcome | Secondary | Primary |
| Scope completeness | Relevant | Primary |
| Over-implementation | May flag | Explicit evaluation responsibility |
| Final authority | No | No |
| Replaces human approval | No | No |

Include examples using corporate fleet booking.

---

# 9. Evaluator Versus Validator

Explain one of the most important distinctions in the Harness architecture.

## 9.1 Validator Question

> Can we prove that defined engineering conditions passed?

Examples:

- build succeeds,
- tests pass,
- architecture rules pass,
- migrations apply,
- API schema is valid,
- security scan passes.

---

## 9.2 Evaluator Question

> Does the proven implementation actually solve the requested problem?

---

## 9.3 Validator Versus Evaluator Diagram

```text
                 Implementation
                       │
          ┌────────────┴────────────┐
          ▼                         ▼
 ┌─────────────────┐       ┌──────────────────┐
 │ Validator Agent │       │ Evaluator Agent  │
 ├─────────────────┤       ├──────────────────┤
 │ Deterministic   │       │ Goal satisfaction│
 │ Build           │       │ Completeness     │
 │ Tests           │       │ Business outcome │
 │ Contracts       │       │ Scope fitness    │
 │ Security gates  │       │ Trade-offs       │
 │ Architecture    │       │ Overall quality  │
 └────────┬────────┘       └────────┬─────────┘
          │                         │
          ▼                         ▼
    Proven Evidence           Evaluation Result
```

Emphasize:

> Validation establishes facts. Evaluation interprets those facts in relation to the goal.

---

# 10. Evaluation Workflow

Show the complete workflow:

```text
Goal
  ↓
Lead Agent Plan
  ↓
Developer Agent
  ↓
Reviewer Agent
  ↓
Validator Agent
  ↓
Evaluator Agent
  ↓
Human Approval
```

Expand the Evaluator stage:

```text
Receive Goal
    ↓
Load Acceptance Criteria
    ↓
Read Reviewer Findings
    ↓
Read Validation Evidence
    ↓
Inspect Relevant Implementation
    ↓
Compare Outcome With Goal
    ↓
Assess Completeness
    ↓
Assess Scope
    ↓
Assess Business Outcome
    ↓
Assess Risks
    ↓
Generate Evaluation Result
```

---

# 11. Goal Evaluation

Explain how the Evaluator traces the final implementation back to the original goal.

## 11.1 Preserve the Original Goal

The evaluation should reference the original requested outcome rather than inventing a new interpretation.

---

## 11.2 Decompose the Goal

For example:

> Corporate fleet managers must be able to create recurring detailing bookings for multiple fleet vehicles.

Possible evaluation dimensions:

- corporate user support,
- multi-vehicle selection,
- recurring scheduling,
- fleet ownership enforcement,
- booking creation,
- conflict handling,
- meaningful result feedback.

---

## 11.3 Goal-to-Evidence Traceability

Create explicit mapping from each goal component to evidence.

---

# 12. Goal-to-Evidence Assessment Diagram

```text
Business Goal
     │
     ├── Fleet booking
     │       └── API + integration evidence
     │
     ├── Multiple vehicles
     │       └── Request model + tests
     │
     ├── Recurring schedule
     │       └── Domain behavior + tests
     │
     ├── Authorization
     │       └── Security validation
     │
     └── Booking confirmation
             └── API response + workflow evidence

                    ↓

             Evaluator Assessment

                    ↓

     Complete / Partial / Failed / Inconclusive
```

---

# 13. Acceptance Criteria Evaluation

Explain that acceptance criteria become explicit evaluation targets.

Example:

```text
AC1: Fleet manager can select multiple eligible fleet vehicles.
AC2: Fleet manager can create a recurring weekly schedule.
AC3: Conflicting bookings are rejected.
AC4: User cannot schedule vehicles outside their corporate fleet.
AC5: Created bookings are returned with confirmation identifiers.
```

For each criterion capture:

- requirement,
- supporting evidence,
- status,
- gaps,
- risk,
- evaluator notes.

---

# 14. Business Outcome Evaluation

Explain why acceptance criteria alone may not describe the complete business intent.

Assess questions such as:

- Can a fleet manager realistically use the workflow?
- Does the solution support fleet-level operations?
- Are important business rules preserved?
- Does the implementation deliver the intended operational benefit?
- Is the workflow practical at enterprise scale?

Alpha Car Detailing example:

A fleet of 200 vehicles should not require 200 manually independent booking operations when the requested feature explicitly intended fleet scheduling.

---

# 15. Quality Evaluation

Discuss outcome-oriented quality dimensions.

Possible dimensions:

- completeness,
- correctness relative to goal,
- clarity,
- usability,
- consistency,
- maintainability impact,
- operational readiness,
- observability,
- resilience,
- security implications,
- performance suitability,
- enterprise readiness.

Explain that not every dimension applies equally to every task.

---

# 16. Evidence Consumption

The Evaluator should consume evidence rather than rediscover everything.

## 16.1 Developer Evidence

Examples:

- implementation summary,
- changed files,
- design decisions,
- assumptions,
- tests added,
- known limitations.

---

## 16.2 Reviewer Evidence

Examples:

- architecture concerns,
- maintainability findings,
- security observations,
- contract concerns,
- code quality findings.

---

## 16.3 Validator Evidence

Examples:

- build results,
- test results,
- security results,
- static analysis,
- architecture checks,
- API contract checks,
- migration checks.

---

## 16.4 Harness Evidence

Examples:

- gate results,
- command logs,
- changed-file summaries,
- execution history,
- retry history,
- artifact links.

---

# 17. Evaluation Criteria

Explain how evaluation criteria should be derived from authoritative inputs.

Possible sources:

1. Goal
2. Acceptance criteria
3. Instructions
4. Steering Notes
5. Architecture decisions
6. Business rules
7. Approved task plan
8. Relevant enterprise standards

Discuss criteria characteristics:

- explicit,
- traceable,
- understandable,
- evidence-backed,
- scoped,
- non-contradictory.

---

# 18. Structured Assessment and Scoring

Explain where structured scoring can help without replacing engineering judgment.

Example assessment model:

| Dimension | Assessment |
| --- | --- |
| Goal satisfaction | Met |
| Acceptance criteria | 4 of 5 met |
| Business outcome | Partial |
| Technical evidence | Passed |
| Scope discipline | Met |
| Residual risk | Medium |
| Overall outcome | Partial |

Discuss optional numerical scoring for analytics.

Example:

```text
Goal Satisfaction:      40%
Acceptance Criteria:    30%
Business Outcome:       15%
Quality Expectations:   10%
Scope Discipline:        5%
```

Warn against treating aggregate scores as automatic approval.

Example:

> A 92% score cannot override a missing security-critical acceptance criterion.

---

# 19. Evaluation Outcomes

Define standard result states.

## 19.1 Pass

The implementation sufficiently satisfies:

- the intended goal,
- required acceptance criteria,
- business expectations,
- applicable quality expectations.

---

## 19.2 Fail

A material requirement or outcome has not been satisfied.

---

## 19.3 Partial

Meaningful progress exists, but one or more important requirements remain incomplete.

---

## 19.4 Inconclusive

The Evaluator cannot reach a responsible conclusion because evidence or requirements are insufficient.

Examples:

- missing test evidence,
- conflicting acceptance criteria,
- unavailable business context,
- unresolved human decision.

---

# 20. Detecting Technically Valid but Incomplete Solutions

Use the Alpha Car Detailing example.

The Validator reports:

```text
Build: PASS
Unit Tests: PASS
Integration Tests: PASS
Architecture Gates: PASS
Security Checks: PASS
```

But the Evaluator discovers:

```text
Requested:
Recurring bookings for multiple fleet vehicles.

Implemented:
Recurring bookings for a single vehicle.
```

Therefore:

```text
Validation Result: PASS
Evaluation Result: PARTIAL
```

Explain why this distinction is central to the Harness.

---

# 21. Detecting Over-Implementation

Define over-implementation.

Examples:

- adding a new scheduling microservice unnecessarily,
- introducing a message broker when synchronous behavior was sufficient,
- refactoring unrelated booking services,
- creating generic workflow infrastructure for one feature,
- introducing CQRS where the approved task did not require it.

Discuss why over-implementation matters:

- increases risk,
- increases review surface,
- increases maintenance,
- delays delivery,
- introduces architecture drift.

---

# 22. Detecting Missed Requirements

Discuss missed requirement patterns:

- acceptance criterion omitted,
- edge case ignored,
- business constraint missed,
- security condition overlooked,
- integration requirement forgotten,
- wrong user persona implemented,
- incomplete workflow,
- requirement interpreted too narrowly.

Create a traceability technique:

```text
Requirement
   ↓
Expected Behavior
   ↓
Implementation Evidence
   ↓
Validation Evidence
   ↓
Evaluator Decision
```

---

# 23. Risk and Trade-Off Assessment

Explain that evaluation may identify acceptable and unacceptable trade-offs.

Possible dimensions:

- delivery speed,
- maintainability,
- operational complexity,
- cost,
- scalability,
- performance,
- security,
- compatibility,
- technical debt.

The Evaluator should describe trade-offs, not silently approve them.

---

# 24. Evaluation Reports

Define a standard evaluation report.

Suggested structure:

```yaml
evaluation:
  goal: "Support recurring fleet booking for multiple vehicles"

  outcome: partial

  goal_satisfaction:
    status: partial
    notes:
      - Multi-vehicle booking is supported.
      - Recurring scheduling is supported.
      - Cancellation behavior is missing.

  acceptance_criteria:
    total: 5
    passed: 4
    failed: 1

  business_outcome:
    status: partial

  validation_evidence:
    build: passed
    tests: passed
    architecture: passed
    security: passed

  missed_requirements:
    - recurring cancellation support

  over_implementation:
    - none

  residual_risks:
    - incomplete recurring schedule lifecycle

  recommendation:
    - return to Lead Agent for corrective work
```

---

# 25. Evaluation Result Flow

Include the result-flow diagram.

```text
                    Evaluator
                        │
          ┌─────────────┼─────────────┐
          │             │             │
          ▼             ▼             ▼
        PASS          PARTIAL        FAIL
          │             │             │
          ▼             └──────┬──────┘
 Human Approval                │
                               ▼
                         Lead Agent
                               │
                               ▼
                        Developer Agent
                               │
                               ▼
                       Review / Validate
                               │
                               ▼
                           Reevaluate

INCONCLUSIVE
     │
     ▼
Need More Evidence
or Human Decision
```

---

# 26. Handoff to the Lead Agent

Explain that the Lead Agent coordinates corrective action.

Evaluator returns:

- failed criteria,
- missing requirements,
- evidence gaps,
- scope concerns,
- residual risk,
- recommended next action.

The Lead Agent decides how work should be decomposed and reassigned.

---

# 27. Handoff to the Developer Agent

Explain indirect versus direct handoff.

Preferred:

```text
Evaluator
   ↓
Lead Agent
   ↓
Developer Agent
```

The Developer receives:

- precise gap,
- affected acceptance criterion,
- supporting evidence,
- expected correction,
- scope constraints.

Avoid vague feedback such as:

> Improve the implementation.

Prefer:

> Acceptance criterion AC4 is unmet. The API currently allows booking Vehicle 783 even when it belongs to another corporate fleet. Add fleet ownership enforcement without modifying unrelated booking workflows.

---

# 28. Re-Evaluation After Rework

Explain that evaluation must be repeatable.

Workflow:

```text
Evaluation Finds Gap
        ↓
Lead Assigns Rework
        ↓
Developer Changes
        ↓
Reviewer Reviews
        ↓
Validator Revalidates
        ↓
Evaluator Reevaluates
```

Do not allow previous validation evidence to be blindly reused after relevant changes.

---

# 29. Human Approval Boundaries

Clarify the boundary between evaluation and decision authority.

The Evaluator may recommend:

```text
Outcome: PASS
Ready for human approval.
```

But must not conclude:

```text
Approved for production.
```

unless explicit organizational authority has been granted outside the normal model.

Human decisions may include:

- business acceptance,
- architecture exceptions,
- risk acceptance,
- production approval,
- regulatory decisions,
- scope changes,
- conflicting requirement resolution.

---

# 30. Evaluator Agent Inside the Harness

Describe where evaluation fits into Harness orchestration.

```text
┌─────────────────────────────────────────────┐
│                   Harness                   │
│                                             │
│  Goal                                       │
│    ↓                                        │
│  Lead Agent                                 │
│    ↓                                        │
│  Developer Agent                            │
│    ↓                                        │
│  Reviewer Agent                             │
│    ↓                                        │
│  Deterministic Gates                        │
│    ↓                                        │
│  Validator Agent                            │
│    ↓                                        │
│  Evaluator Agent                            │
│    ↓                                        │
│  Human Approval                             │
│                                             │
│  Evidence Store / Metrics / Audit Trail     │
└─────────────────────────────────────────────┘
```

Explain interaction with:

- Harness state,
- execution history,
- artifact storage,
- metrics,
- gate evidence,
- evaluation records,
- audit trails.

---

# 31. Evaluator Agent Inputs and Outputs

## Inputs

- original prompt,
- goal,
- acceptance criteria,
- Instructions,
- Steering Notes,
- Lead Agent plan,
- Developer summary,
- Reviewer report,
- Validator report,
- deterministic evidence,
- changed files,
- relevant business context.

## Outputs

- evaluation status,
- goal satisfaction assessment,
- acceptance criteria assessment,
- business outcome assessment,
- missed requirements,
- over-implementation findings,
- residual risks,
- evidence gaps,
- recommended next step.

---

# 32. Evaluator Agent Prompt Contract

Show the conceptual Evaluator prompt.

Example structure:

```text
Role:
You are the Evaluator Agent.

Goal:
Assess whether the completed implementation satisfies the original
task goal, acceptance criteria, business expectations, and quality expectations.

Inputs:
- Original task
- Acceptance criteria
- Developer summary
- Reviewer report
- Validator evidence
- Relevant repository context

Responsibilities:
- Assess goal satisfaction.
- Assess each acceptance criterion.
- Identify missing requirements.
- Identify over-implementation.
- Assess business outcome.
- Identify residual risks.
- Return PASS, FAIL, PARTIAL, or INCONCLUSIVE.

Constraints:
- Do not modify implementation.
- Do not override failed deterministic gates.
- Do not invent requirements.
- Do not approve production deployment.
- Cite evidence for findings.
```

---

# 33. Evaluation Data Model

Introduce a possible Harness representation.

```csharp
public sealed record EvaluationResult(
    string TaskId,
    EvaluationStatus Status,
    IReadOnlyList<CriterionEvaluation> Criteria,
    IReadOnlyList<string> MissingRequirements,
    IReadOnlyList<string> OverImplementation,
    IReadOnlyList<string> ResidualRisks,
    string Summary);
```

Possible status enum:

```csharp
public enum EvaluationStatus
{
    Pass,
    Fail,
    Partial,
    Inconclusive
}
```

Explain that the schema should be machine-readable as well as human-readable.

---

# 34. Evaluation Workflow for Alpha Car Detailing

Walk through the corporate fleet booking feature.

## Step 1 — Original Goal

Support recurring bookings for multiple corporate fleet vehicles.

## Step 2 — Developer Output

Implementation completed.

## Step 3 — Reviewer

Engineering quality acceptable.

## Step 4 — Validator

All deterministic checks pass.

## Step 5 — Evaluator

Compares actual behavior against business intent.

## Step 6 — Findings

For example:

- multi-vehicle booking: pass,
- recurring creation: pass,
- fleet authorization: pass,
- cancellation workflow: fail,
- unrelated scope increase: none.

## Step 7 — Outcome

```text
PARTIAL
```

## Step 8 — Harness Action

Return to Lead Agent for targeted rework.

---

# 35. Hands-on Example

Create a complete hands-on scenario based on Alpha Car Detailing corporate fleet booking.

Include:

- feature request,
- Steering Note excerpt,
- acceptance criteria,
- Developer output,
- Reviewer report,
- Validator evidence,
- Evaluator analysis,
- final evaluation result.

Demonstrate how all previous Harness roles contribute evidence without duplicating responsibility.

---

# 36. Claude Example

Show Claude Code as the primary implementation.

Cover:

- Evaluator role definition,
- evaluator prompt,
- repository evidence supplied to Claude,
- structured evaluation output,
- evidence references,
- execution through the Harness,
- preventing Claude from modifying source files during evaluation.

Possible Harness layout:

```text
harness/
├── roles/
│   ├── lead.md
│   ├── developer.md
│   ├── reviewer.md
│   ├── validator.md
│   └── evaluator.md
├── scripts/
│   ├── lead.ps1
│   ├── validate.ps1
│   └── evaluate.ps1
└── results/
    ├── validation-result.json
    └── evaluation-result.json
```

Discuss structured output and auditability.

---

# 37. GitHub Copilot Comparison

Explain how equivalent evaluation workflows may differ when using GitHub Copilot.

Discuss:

- repository context,
- custom instructions,
- reusable prompts,
- agent capabilities,
- manual orchestration versus Harness orchestration,
- structured evaluation prompts,
- evidence provisioning,
- external scripts for deterministic evaluation pipelines.

Keep engineering principles vendor-neutral.

---

# 38. OpenAI Codex Comparison

Explain the same Evaluator responsibilities in a Codex-based workflow.

Discuss:

- task context,
- repository access,
- evaluation prompts,
- structured outputs,
- isolated evaluation execution,
- integration with deterministic scripts,
- Harness-controlled evidence flow.

Emphasize:

> The role belongs to the Harness architecture, not to a specific AI platform.

---

# 39. Best Practices

Cover practices such as:

- evaluate against the original goal,
- make acceptance criteria traceable,
- consume existing evidence,
- keep Reviewer, Validator, and Evaluator responsibilities separate,
- use structured outputs,
- explicitly report missing requirements,
- explicitly report over-implementation,
- separate facts from evaluator judgment,
- cite evidence,
- preserve inconclusive outcomes when evidence is insufficient,
- reevaluate after meaningful changes,
- maintain human approval boundaries,
- store evaluation history,
- keep evaluation criteria versioned,
- prefer specific findings over vague quality judgments.

---

# 40. Anti-Patterns

Cover anti-patterns such as:

## 40.1 Evaluator as Second Reviewer

Repeating code review instead of outcome evaluation.

## 40.2 Evaluator as Validator

Rerunning deterministic checks instead of consuming evidence.

## 40.3 Score-Only Evaluation

Returning:

```text
Score: 87/100
```

without explaining missing requirements.

## 40.4 Rubber-Stamp Evaluation

Automatically passing whenever validation passes.

## 40.5 Requirement Invention

Adding evaluator-created requirements not present in authoritative sources.

## 40.6 Silent Scope Expansion

Treating optional improvements as required.

## 40.7 Self-Fixing Evaluator

Changing code while simultaneously evaluating it.

## 40.8 Ignoring Over-Implementation

Passing solutions merely because they contain all required functionality.

## 40.9 Ignoring Business Context

Assessing technical behavior without considering intended workflow.

## 40.10 Replacing Human Approval

Treating evaluator PASS as organizational approval.

---

# 41. Architect's Notes

Include architecture-level guidance.

Topics:

- evaluation should be independent,
- validation and evaluation should remain separate,
- evidence should be immutable where practical,
- evaluation criteria must be traceable,
- Harness state should retain previous evaluation outcomes,
- evaluation must be repeatable,
- ambiguous requirements should be escalated rather than guessed,
- evaluator independence improves trust,
- scoring should never hide critical requirements,
- human governance remains authoritative.

---

# 42. Enterprise Tips

Include enterprise implementation guidance.

Examples:

- maintain organization-level evaluator templates,
- standardize evaluation result schemas,
- store evaluation evidence with PR artifacts,
- include requirement identifiers,
- make evaluation results searchable,
- aggregate evaluation metrics,
- track common missed requirements,
- track over-implementation frequency,
- distinguish technical gate failures from outcome failures,
- use evaluation history to improve prompts and Skills,
- never allow learning systems to silently modify enterprise standards.

Connect forward to later chapters on:

- Metrics,
- Governance,
- Harness Memory,
- Learning from Reviews,
- Prompt Evolution,
- Skill Evolution.

---

# 43. Decision Points

Present architecture decisions readers must make.

## Decision 1 — Does Every Task Require an Evaluator?

Discuss risk-based evaluation.

## Decision 2 — Should Evaluation Use Scores?

Discuss structured statuses versus numerical scoring.

## Decision 3 — Where Should Evaluation Criteria Live?

Possible locations:

- task prompt,
- Steering Note,
- Harness configuration,
- repository policy,
- dedicated evaluation definition.

## Decision 4 — Can the Evaluator Inspect Source Code?

Discuss evidence-first evaluation versus selective implementation inspection.

## Decision 5 — Should Failed Evaluation Return Directly to Developer?

Compare direct routing with Lead Agent orchestration.

## Decision 6 — When Is Human Evaluation Mandatory?

Discuss:

- financial impact,
- regulated workflows,
- architecture exceptions,
- production deployment,
- ambiguous business requirements.

---

# 44. Exercises

Include exercises such as:

### Exercise 1

Define an Evaluator Agent role for Alpha Car Detailing.

### Exercise 2

Create evaluation criteria for recurring corporate fleet bookings.

### Exercise 3

Given passing Validator evidence, identify why an implementation should still receive PARTIAL.

### Exercise 4

Identify over-implementation in a proposed booking feature.

### Exercise 5

Create an evaluation report schema.

### Exercise 6

Design a Harness workflow for reevaluation after failed acceptance criteria.

### Exercise 7

Separate Reviewer, Validator, and Evaluator responsibilities for the same feature.

### Exercise 8

Design a human escalation path for an INCONCLUSIVE evaluation.

---

# 45. Interview Questions

Include questions such as:

1. What is the purpose of an Evaluator Agent?
2. How does an Evaluator differ from a Reviewer?
3. How does an Evaluator differ from a Validator?
4. Why can a solution pass validation but fail evaluation?
5. What evidence should an Evaluator consume?
6. How should acceptance criteria be evaluated?
7. What does PARTIAL mean?
8. When should an Evaluator return INCONCLUSIVE?
9. How can an Evaluator detect over-implementation?
10. Why should an Evaluator not modify implementation code?
11. How should evaluation results feed back into the Harness?
12. Why should scoring not automatically determine approval?
13. What role does business context play in evaluation?
14. When should evaluation be escalated to humans?
15. How does an Evaluator support enterprise governance?
16. How should evaluation evidence be stored?
17. What is the relationship between deterministic gates and evaluation?
18. Why is goal-to-evidence traceability important?
19. How can evaluation results improve future AI Engineering workflows?
20. Why should an Evaluator PASS still require human approval where policy requires it?

---

# 46. Chapter Summary

Reinforce the core concepts:

- The Evaluator Agent determines whether the implementation achieved the intended outcome.
- The Reviewer evaluates engineering quality.
- The Validator verifies deterministic engineering evidence.
- The Evaluator assesses goal satisfaction, completeness, business outcome, and overall outcome quality.
- Passing validation does not guarantee passing evaluation.
- Evaluation must remain traceable to authoritative goals and acceptance criteria.
- Evaluators should identify both missing requirements and over-implementation.
- PASS, FAIL, PARTIAL, and INCONCLUSIVE provide clearer workflow semantics than a binary result alone.
- Evaluation results should feed back through the Lead Agent when corrective action is required.
- AI evaluation does not replace human approval.
- The Harness coordinates all roles and preserves their separation of responsibilities.

End with the central model:

```text
Developer
   ↓
Reviewer
   ↓
Validator
   ↓
Evaluator
   ↓
Human Approval
```

And the chapter principle:

> **Validation determines whether the implementation passed defined engineering checks. Evaluation determines whether the resulting implementation actually satisfies the intended goal.**

---

# 47. Further Reading

Plan references covering:

- software verification versus validation,
- requirements traceability,
- acceptance testing,
- software quality models,
- evaluation frameworks,
- AI agent evaluation,
- software architecture governance,
- human-in-the-loop systems,
- structured evaluation,
- responsible autonomous engineering systems.

Prefer authoritative sources such as:

- ISO/IEC software quality standards,
- IEEE software engineering references,
- Microsoft architecture guidance,
- NIST AI risk and evaluation guidance where applicable,
- Anthropic Claude Code documentation,
- GitHub Copilot documentation,
- OpenAI Codex documentation,
- established software architecture and requirements engineering literature.

---

# 48. Chapter 18 Cross-Reference Map

Include a concise cross-reference section connecting the Evaluator Agent to earlier and later chapters.

| Related Chapter | Relationship |
| --- | --- |
| Chapter 8 — Prompts | Defines the original task and expected outcome |
| Chapter 9 — Roles | Establishes role separation |
| Chapter 10 — Steering Notes | Supplies current goals, constraints, and acceptance criteria |
| Chapter 11 — Knowledge Sources | Supplies authoritative context |
| Chapter 12 — What Is a Harness? | Establishes Harness responsibilities |
| Chapter 13 — Harness Architecture | Places evaluation in the orchestration pipeline |
| Chapter 14 — Lead Agent | Coordinates work after evaluation |
| Chapter 15 — Developer Agent | Produces the implementation being evaluated |
| Chapter 16 — Reviewer Agent | Provides independent engineering review |
| Chapter 17 — Validator Agent | Supplies deterministic evidence |
| Chapter 19 — Population | Uses repeated executions and candidate results |
| Chapter 20 — Metrics | Measures evaluation outcomes and Harness effectiveness |
| Chapter 21 — Enterprise Harness Patterns | Integrates evaluation into complete enterprise workflows |
| Later Governance Chapters | Define human approval and policy boundaries |
| Self-Learning Chapters | Use evaluation history as controlled learning evidence |

---

**Chapter 18 outline is complete.**
