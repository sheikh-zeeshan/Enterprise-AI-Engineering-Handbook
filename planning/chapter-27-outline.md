# Chapter 27 — Planning Mode  

**Part IV — Claude Engineering**  
**Target file:** `book/27-planning-mode.md`

The outline below follows the handbook’s frozen chapter structure and preserves the established Claude-first, vendor-neutral approach, enterprise audience, Alpha Car Detailing running example, and distinctions among Instructions, Skills, Prompts, Roles, and the Harness. AI development handbook AI development handbook

---

## 1. Story-Driven Opening — Before Claude Changes the First Line

### Scenario: Corporate Fleet Booking Enhancement

- Alpha Car Detailing receives a new enterprise requirement for **corporate fleet booking**.
- Corporate customers must be able to:
  - Create fleet bookings for multiple vehicles.
  - Respect account-specific booking limits.
  - Prevent duplicate booking submissions.
  - Produce reliable booking events for downstream systems.
- The request appears straightforward but affects:
  - API contracts.
  - Domain rules.
  - Application services.
  - Persistence.
  - Idempotency.
  - Transactional outbox behavior.
  - Event contracts.
  - Integration tests.
  - Architecture boundaries.
- Contrast two approaches:
  - Claude immediately starts modifying files.
  - Claude first enters Planning Mode.
- Show how premature implementation can result in:
  - Missed constraints.
  - Incorrect architectural assumptions.
  - Unnecessary file changes.
  - Broken contracts.
  - Incomplete validation.
  - Rework.
- Introduce the chapter principle:

> Planning separates understanding from execution.

- Planning Mode establishes:
  - What needs to change.
  - Why it needs to change.
  - Where the change belongs.
  - What dependencies exist.
  - Which Instructions and Skills apply.
  - How success will be validated.
- Establish that a plan is **not implementation** and should not silently become implementation.

---

## 2. Learning Objectives

After completing this chapter, readers should be able to:

- Define Planning Mode in AI-assisted engineering.
- Explain why planning and execution should be deliberately separated.
- Decide when explicit planning is appropriate.
- Distinguish a Plan from:
  - Prompt.
  - Scratchpad.
  - Steering Note.
  - Lead Agent responsibilities.
  - Repository Intelligence.
- Perform repository discovery before proposing changes.
- Determine Applicable Instructions.
- Select relevant Skills.
- Identify implementation scope and affected components.
- Decompose complex work into executable tasks.
- Identify dependencies, risks, assumptions, and unknowns.
- Convert business requirements into acceptance criteria.
- Design validation before implementation.
- Establish appropriate human approval points.
- Manage plan changes and plan drift.
- Hand an approved plan to the Developer Agent.
- Integrate Planning Mode into a Harness.
- Recognize planning anti-patterns.

---

## 3. Background — Why Coding Agents Need an Explicit Planning Phase

### 3.1 From Immediate Generation to Deliberate Engineering

- Traditional prompt-to-code behavior.
- Why immediate generation works for small isolated tasks.
- Why it becomes dangerous for enterprise repositories.
- Large repositories contain:
  - Architectural conventions.
  - Hidden dependencies.
  - Multiple services.
  - Shared libraries.
  - Security boundaries.
  - Database contracts.
  - Event contracts.
  - CI/CD requirements.

### 3.2 Planning as an Engineering Control

- Planning reduces uncertainty before mutation.
- Planning makes assumptions visible.
- Planning creates an inspectable engineering decision artifact.
- Planning supports:
  - Reviewability.
  - Predictability.
  - Traceability.
  - Safer automation.
  - Better agent handoffs.

### 3.3 Planning Is Not Waterfall

- Planning should not imply a heavyweight upfront design process.
- Plans can be:
  - Small.
  - Incremental.
  - Task-scoped.
  - Revised as new evidence appears.
- Distinguish deliberate planning from rigid planning.

### 3.4 The Cost-of-Change Argument

- Discovering a misunderstanding before code generation is cheaper than:
  - Rewriting implementation.
  - Repairing migrations.
  - Changing public contracts.
  - Reworking tests.
  - Reversing architecture decisions.

---

# 4. Concepts

## 4.1 What Is Planning Mode?

Define Planning Mode as:

> A deliberate phase in which an AI Agent investigates a task, gathers relevant repository and business context, identifies constraints, determines the required work, and prepares a validation-aware implementation plan before repository changes begin.

Planning Mode should answer:

1. What is being requested?
2. Why is it required?
3. What parts of the system are affected?
4. Which repository knowledge applies?
5. Which Instructions govern the change?
6. Which Skills may be used?
7. What dependencies exist?
8. What assumptions remain?
9. What are the implementation steps?
10. How will the result be validated?

### 4.1.1 Primary Outputs

- Problem understanding.
- Scope.
- Affected areas.
- Constraints.
- Dependencies.
- Risks.
- Assumptions.
- Task sequence.
- Acceptance criteria.
- Validation strategy.
- Approval requirements.

### 4.1.2 Planning Mode Does Not Normally

- Modify production code.
- Refactor unrelated files.
- Commit changes.
- Create migrations.
- Update governed standards.
- Circumvent approval gates.

---

## 4.2 When to Use Planning Mode

### Recommended situations

- Cross-layer changes.
- Multi-service work.
- Architectural changes.
- Database schema changes.
- Public API changes.
- Event contract changes.
- Security-sensitive work.
- High-risk refactoring.
- Unfamiliar repositories.
- Ambiguous requirements.
- Tasks involving several Skills.
- Tasks requiring deterministic validation.
- Work that will be delegated between agents.

### Lightweight planning

- Small bug fixes.
- Localized test changes.
- Minor documentation corrections.

### Planning may be unnecessary when

- Scope is extremely narrow.
- Change location is already deterministic.
- Risk is low.
- No architectural decision is involved.

### Decision heuristic

Evaluate:

- Complexity.
- Blast radius.
- Uncertainty.
- Reversibility.
- Security impact.
- Contract impact.
- Number of affected components.

---

## 4.3 Planning Versus Execution

### Planning

Answers:

- What?
- Why?
- Where?
- In what order?
- Under which constraints?
- How will success be demonstrated?

### Execution

Performs:

- Code changes.
- Configuration changes.
- Tests.
- Migrations.
- Documentation updates.
- Implementation-related repository mutations.

### Separation of authority

- Planning authorization does not necessarily imply execution authorization.
- Execution should consume an approved or accepted plan.

### Enterprise value

- Reduces accidental changes.
- Supports human review.
- Makes delegated work easier.
- Creates auditable intent.

---

## 4.4 Planning Versus Prompt

The handbook defines a Prompt as the **task-specific request**—effectively “today’s task.” AI development handbook

### Prompt

Describes requested work.

Example:

> Add corporate fleet booking limits.

### Plan

Explains how the repository should be changed to satisfy that request.

### Relationship

```text
Prompt
   ↓
Understand Task
   ↓
Plan
   ↓
Approved Execution
```

### Key distinction

- Prompt = request.
- Plan = proposed engineering response to the request.

---

## 4.5 Planning Versus Scratchpad

### Plan

- Intentional.
- Structured.
- Reviewable.
- Suitable for handoff.
- Describes proposed work.
- May require approval.

### Scratchpad

- Temporary working state.
- Task-scoped.
- Contains intermediate observations.
- Can contain incomplete reasoning artifacts.
- Tracks files under investigation.
- Records provisional assumptions and discoveries.
- Is not authoritative knowledge.

### Important rule

Information may originate in the Scratchpad but become part of the Plan only after it is sufficiently understood.

### Diagram — Plan Versus Scratchpad

```text
                TASK
                  │
                  ▼
          ┌───────────────┐
          │   Scratchpad  │
          │               │
          │ Observations  │
          │ Questions     │
          │ File notes    │
          │ Assumptions   │
          │ Discoveries   │
          └───────┬───────┘
                  │
          Refine / Validate
                  │
                  ▼
          ┌───────────────┐
          │     Plan      │
          │               │
          │ Scope         │
          │ Tasks         │
          │ Dependencies  │
          │ Risks         │
          │ Validation    │
          └───────┬───────┘
                  │
               Approval
                  │
                  ▼
              Execution
```

---

## 4.6 Planning Versus Lead Agent

### Planning Mode

- A phase or operating mode.

### Lead Agent

- A role.

The handbook’s established Harness architecture places the Lead Agent before Developer, Reviewer, Validator, and Evaluator. AI development handbook

### Lead Agent responsibilities during planning

- Interpret the Prompt.
- Coordinate repository discovery.
- Determine Applicable Instructions.
- Identify relevant Skills.
- Establish scope.
- Decompose work.
- Identify dependencies.
- Define validation expectations.
- Prepare the implementation plan.
- Request approval where required.
- Delegate approved tasks.

### Important distinction

A Lead Agent commonly **owns orchestration of planning**, but Planning Mode is not equivalent to the Lead Agent.

Other roles may also perform bounded planning.

---

## 4.7 Planning Versus Steering Notes

### Steering Note

- Describes current mission, priorities, constraints, and acceptance criteria.
- Usually maintained by humans.
- Should be consumed as planning context.
- Should not normally be altered by the AI Agent without explicit authorization. AI development handbook

### Plan

- Is produced for a particular task.
- Must conform to applicable Steering Notes.

Relationship:

```text
Steering Note
      │
      ▼
Task / Prompt
      │
      ▼
Planning Mode
      │
      ▼
Implementation Plan
```

---

## 4.8 Planning Versus Repository Intelligence

### Repository Intelligence

Provides knowledge such as:

- Repository layout.
- Architecture.
- Existing patterns.
- Relevant components.
- Historical conventions.

### Planning Mode

Consumes Repository Intelligence to determine the appropriate implementation approach.

### Principle

Planning without Repository Intelligence becomes speculation.

---

# 5. Architecture Discussion

## 5.1 Planning Mode Lifecycle

### Diagram — Planning Mode Lifecycle

```text
Task Received
     │
     ▼
Understand Intent
     │
     ▼
Discover Repository
     │
     ▼
Load Applicable Instructions
     │
     ▼
Identify Relevant Skills
     │
     ▼
Determine Scope
     │
     ▼
Identify Dependencies
     │
     ▼
Assess Risks & Assumptions
     │
     ▼
Define Acceptance Criteria
     │
     ▼
Design Validation Strategy
     │
     ▼
Build Implementation Plan
     │
     ▼
Review / Human Approval
     │
     ├──── Rejected / Revised ────┐
     │                            │
     ▼                            │
Approved                         │
     │                            │
     ▼                            │
Execution                        │
                                  │
                   Update Plan ◄──┘
```

---

## 5.2 Repository Discovery During Planning

### Questions to answer before implementation

- Where is the relevant feature implemented?
- What architectural layer owns the behavior?
- What existing patterns already solve similar problems?
- Which tests cover the behavior?
- Which contracts might change?
- What dependencies are involved?
- Are there generated files?
- Are there migration conventions?
- Which CI checks are relevant?

### Alpha Car Detailing discovery targets

- Fleet booking API endpoints.
- Booking aggregate/domain rules.
- Corporate account policies.
- Existing booking repository.
- Idempotency infrastructure.
- Transactional outbox.
- Booking-created event.
- Integration tests.
- Architecture tests.

### Discovery evidence

A good plan should reference actual repository evidence rather than relying purely on assumptions.

---

## 5.3 Applicable Instructions

The handbook defines Instructions as persistent repository-wide engineering requirements such as architecture, coding standards, testing, logging, and security. AI development handbook

### Planning questions

- Which Instructions apply to the task?
- Are there root Instructions?
- Are there directory-scoped Instructions?
- Do different modules have different requirements?
- Are security or test requirements mandatory?

### Example

Corporate fleet booking may require:

- Clean Architecture boundaries.
- Domain validation rules.
- API naming conventions.
- Logging requirements.
- Idempotency standards.
- Required integration testing.

### Applicable Instructions set

Introduce the idea of creating a task-specific set of relevant Instructions rather than blindly treating every repository Instruction as equally relevant.

---

## 5.4 Skill Selection

The handbook defines Skills as reusable engineering workflows describing how recurring work is implemented. AI development handbook

### Planning should determine

- Whether a Skill is required.
- Whether multiple Skills apply.
- Skill execution order.
- Skill prerequisites.
- Whether the Skill fits the current architecture.

### Alpha Car Detailing examples

Potential Skills:

- Modify API endpoint.
- Add domain validation.
- Add integration tests.
- Configure transactional outbox.
- Add idempotent consumer behavior.

### Principle

Do not select a Skill merely because its name resembles the request.

---

## 5.5 Scope Identification

Define:

### In scope

- Required behavior.
- Required components.
- Required tests.
- Required contracts.

### Out of scope

- Opportunistic refactoring.
- Unrelated upgrades.
- Architecture redesign not required by the task.
- Cosmetic cleanup.

### Scope boundaries

Identify:

- Files.
- Projects.
- Services.
- APIs.
- Database objects.
- Events.
- Infrastructure.

### Scope containment

Prevent Planning Mode from turning a bounded feature request into repository-wide modernization.

---

## 5.6 Task Decomposition

Break work into:

- Independently understandable tasks.
- Dependency-aware order.
- Validation-aware units.

### Alpha Car Detailing example

Possible decomposition:

1. Confirm corporate fleet booking policy.
2. Extend domain model for fleet limits.
3. Add application command behavior.
4. Add persistence changes if required.
5. Enforce idempotency.
6. Publish outbox event.
7. Update API contract.
8. Add unit tests.
9. Add integration tests.
10. Execute architecture/security/contract checks.

### Good decomposition characteristics

- Cohesive.
- Ordered.
- Testable.
- Minimal coupling.
- Clear ownership.

---

## 5.7 Dependencies

### Technical dependencies

- Existing domain models.
- Shared libraries.
- Database schemas.
- Message contracts.
- Authentication.
- External services.

### Task dependencies

- Migration before integration test.
- Contract before consumer update.
- Domain behavior before application orchestration.

### Organizational dependencies

- Architecture approval.
- Security review.
- Schema approval.
- Product clarification.

### Dependency representation

- Explicit dependency graph.
- Blocking versus non-blocking dependencies.

---

## 5.8 Risks and Assumptions

### Risks

Examples:

- Breaking existing booking clients.
- Duplicate bookings.
- Race conditions.
- Incorrect fleet limits.
- Event duplication.
- Transaction consistency problems.

### Assumptions

Examples:

- Fleet limits belong to corporate accounts.
- Existing idempotency infrastructure can be reused.
- Existing outbox supports the event.

### Unknowns

Separate unknowns from assumptions.

### Rule

A plan should not disguise uncertainty as certainty.

---

## 5.9 Acceptance Criteria

### From business language to testable outcomes

Corporate fleet booking examples:

- A corporate account can book multiple vehicles.
- The booking cannot exceed configured fleet limits.
- Repeated requests with the same idempotency key do not create duplicate bookings.
- A successful booking produces exactly one durable outbox message.
- Existing individual booking behavior remains unchanged.

### Acceptance criteria should be

- Observable.
- Testable.
- Specific.
- Traceable to the task.

---

## 5.10 Validation Strategy Before Implementation

### Define validation during planning

Possible layers:

- Build.
- Unit tests.
- Integration tests.
- Architecture tests.
- Static analysis.
- Security checks.
- API contract checks.
- Event contract checks.
- Migration checks.

### Key principle

Validation should not be invented after the code is already written.

### Validator relationship

Planning specifies **what evidence will be needed**.

The Validator later verifies the deterministic evidence.

### Evaluator relationship

Planning establishes expected business outcomes and acceptance criteria.

The Evaluator later assesses whether those outcomes were satisfied.

---

# 6. Professional Diagrams

## 6.1 Diagram — Planning-to-Execution Flow

```text
┌──────────────┐
│    Prompt    │
└──────┬───────┘
       │
       ▼
┌──────────────────────┐
│    Planning Mode     │
│                      │
│ Discover             │
│ Analyze              │
│ Scope                │
│ Decompose            │
│ Define validation    │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ Implementation Plan  │
└──────────┬───────────┘
           │
           ▼
     Approval Gate
       │        │
      No       Yes
       │        │
       ▼        ▼
    Revise   Developer
                │
                ▼
            Implementation
                │
                ▼
              Review
                │
                ▼
             Validate
                │
                ▼
             Evaluate
```

---

## 6.2 Diagram — Lead Agent Planning Flow

```text
                   Prompt
                     │
                     ▼
              ┌────────────┐
              │ Lead Agent │
              └─────┬──────┘
                    │
       ┌────────────┼─────────────┐
       ▼            ▼             ▼
 Repository     Instructions     Skills
 Intelligence
       │            │             │
       └────────────┼─────────────┘
                    ▼
               Determine Scope
                    │
                    ▼
             Decompose Tasks
                    │
                    ▼
       Risks / Dependencies / Assumptions
                    │
                    ▼
        Acceptance + Validation Plan
                    │
                    ▼
            Implementation Plan
                    │
                    ▼
              Approval / Handoff
```

---

## 6.3 Diagram — Planning Mode Inside the Harness

The Harness is already established as the automation layer responsible for executing prompts, AI work, validation, testing, evaluation, commits, PR creation, metrics, and iteration history. AI development handbook

```text
             ┌─────────────────────┐
             │       Harness       │
             │                     │
Prompt ─────►│  Lead Agent         │
             │       │             │
             │       ▼             │
             │  Planning Mode      │
             │       │             │
             │       ▼             │
             │  Plan Artifact      │
             │       │             │
             │       ▼             │
             │  Approval Gate      │
             │       │             │
             │       ▼             │
             │  Developer Agent    │
             │       │             │
             │       ▼             │
             │  Reviewer Agent     │
             │       │             │
             │       ▼             │
             │  Validator Agent    │
             │       │             │
             │       ▼             │
             │  Evaluator Agent    │
             └─────────────────────┘
```

---

# 7. Hands-on Example — Planning Corporate Fleet Booking

## 7.1 Initial Prompt

Example requirement for corporate fleet booking.

## 7.2 Planning Without Making Changes

Walk through:

- Requirement interpretation.
- Repository discovery.
- Applicable Instructions.
- Skill identification.
- Scope.
- Dependencies.
- Risks.
- Assumptions.
- Acceptance criteria.
- Validation.

## 7.3 Example Planning Artifact Structure

Suggested structure:

```text
Goal
Context
Applicable Instructions
Repository Findings
Scope
Out of Scope
Affected Components
Dependencies
Assumptions
Risks
Implementation Tasks
Acceptance Criteria
Validation Strategy
Approval Requirements
```

## 7.4 Example Task Sequence

Develop the fleet-booking implementation plan without implementing it.

## 7.5 Review the Plan

Questions:

- Does it contain unsupported assumptions?
- Is the scope too broad?
- Is validation sufficient?
- Are contracts identified?
- Are human approvals required?

---

# 8. Claude Example

## 8.1 Entering Planning Mode

- Claude-first workflow.
- Request analysis before modifications.
- Explicit instruction not to mutate repository state during planning.

## 8.2 Context Gathering

Claude examines:

- Repository structure.
- Instructions.
- Existing fleet booking behavior.
- Existing tests.
- Relevant Skills.

## 8.3 Producing the Plan

Expected structured output.

## 8.4 Human Review

Possible responses:

- Approve.
- Reject.
- Request clarification.
- Narrow scope.
- Change implementation approach.

## 8.5 Execution After Approval

- Transition from planning to implementation.
- Preserve plan context.
- Avoid silently extending scope.

## 8.6 Replanning

Claude discovers an unexpected dependency during execution.

Explain how to:

- Stop.
- Record the discovery.
- Update the plan.
- Reassess validation.
- Request renewed approval when material.

---

# 9. GitHub Copilot Comparison

### Compare conceptually

- Planning support available in Copilot workflows.
- Repository-context gathering.
- Task decomposition.
- Interactive planning.
- Transition to implementation.

### Enterprise engineering comparison

Focus on:

- Separation of planning from execution.
- Repository Instructions.
- Human review.
- Handoff.
- Validation strategy.

### Principle

Avoid binding the architecture of Planning Mode to a specific vendor feature.

---

# 10. Codex Comparison

Discuss:

- Planning before code changes.
- Repository inspection.
- Task decomposition.
- Implementation handoff.
- Validation-aware execution.
- Human oversight.

### Vendor-neutral conclusion

Regardless of platform:

```text
Understand
   ↓
Discover
   ↓
Plan
   ↓
Approve
   ↓
Execute
   ↓
Validate
```

---

# 11. Plan Updates

## 11.1 Why Plans Change

- New repository evidence.
- Incorrect assumption.
- Unexpected dependency.
- Test failure.
- Contract conflict.
- Security constraint.
- Human decision.

## 11.2 Minor Versus Material Updates

### Minor

- File path correction.
- Test filename adjustment.
- Implementation detail with no scope impact.

### Material

- New service added.
- Public contract changes.
- New migration.
- Security behavior changes.
- New dependency.
- Architecture change.

## 11.3 Approval Policy

Material changes may require renewed approval.

## 11.4 Preserve Change History

Possible fields:

- Original plan.
- Revision.
- Reason.
- Evidence.
- Approval status.

---

# 12. Plan Drift

## 12.1 Definition

Plan drift occurs when execution progressively diverges from the approved plan without deliberate reconsideration.

## 12.2 Examples

- Developer Agent modifies unrelated code.
- New refactoring appears.
- Additional services become involved.
- Validation steps are skipped.
- Original acceptance criteria are altered.

## 12.3 Detecting Drift

Compare:

- Planned scope.
- Changed files.
- Executed tasks.
- Produced artifacts.
- Validation performed.

## 12.4 Responding to Drift

```text
Execution
   │
Unexpected Change
   │
   ▼
Does Existing Plan Cover It?
   │
 ┌─┴─┐
Yes  No
 │    │
 ▼    ▼
Continue  Replan
            │
            ▼
       Approval if needed
```

## 12.5 Harness Enforcement

Possible guardrails:

- Changed-file scope monitoring.
- Plan-task tracking.
- Approval gates.
- Validation requirements.
- Plan/version association.

---

# 13. Plan Handoff to Developer Agent

## 13.1 What the Developer Receives

- Goal.
- Approved scope.
- Relevant repository evidence.
- Applicable Instructions.
- Required Skills.
- Ordered tasks.
- Acceptance criteria.
- Validation requirements.
- Known risks.
- Explicit exclusions.

## 13.2 What the Developer Should Not Need to Rediscover

Avoid forcing the Developer Agent to restart repository discovery unnecessarily.

## 13.3 Developer Feedback

Developer may return:

- Blocker.
- Invalid assumption.
- Missing dependency.
- Proposed plan change.

## 13.4 Closed-Loop Handoff

```text
Lead Agent
    │
    ▼
Approved Plan
    │
    ▼
Developer Agent
    │
    ├── Implement
    │
    └── Discovery contradicts plan
             │
             ▼
          Lead Agent
             │
             ▼
          Replanning
```

---

# 14. Human Approval Before Execution

## 14.1 Why Approval Matters

Especially for:

- High-risk tasks.
- Architecture changes.
- Security changes.
- Public contracts.
- Schema migrations.
- Destructive operations.
- Broad refactoring.

## 14.2 Approval Levels

Possible model:

- Automatic execution.
- Lightweight confirmation.
- Technical lead approval.
- Architecture approval.
- Security approval.
- Change-management approval.

## 14.3 Risk-Based Approval

Not every plan requires the same governance.

## 14.4 Approval Is Not Validation

Approval authorizes the proposed direction.

Validation later demonstrates whether implementation satisfies deterministic conditions.

---

# 15. Planning Mode Inside the Harness

## 15.1 Planning as a Harness Stage

Possible pipeline:

```text
Intake
  ↓
Discovery
  ↓
Planning
  ↓
Plan Review
  ↓
Approval Gate
  ↓
Development
  ↓
Review
  ↓
Validation
  ↓
Evaluation
```

## 15.2 Harness Responsibilities

- Create a planning run.
- Gather context.
- Load applicable governance.
- Record plan artifact.
- Track plan version.
- Record approval.
- Pass plan to Developer Agent.
- Detect drift.
- Associate validation evidence with planned criteria.
- Retain an audit trail.

## 15.3 Plan as Run Artifact

Possible location conceptually:

```text
runs/
  <run-id>/
    prompt.md
    plan.md
    scratchpad/
    implementation/
    validation/
    evaluation/
```

Explain that exact filesystem structure is an implementation choice rather than a universal requirement.

## 15.4 Plan State Model

```text
Draft
  ↓
Under Review
  ↓
Approved
  ↓
Executing
  ↓
Revised
  ↓
Completed
```

Possible alternate states:

- Rejected.
- Superseded.
- Blocked.

## 15.5 Planning Metrics

Potential enterprise metrics:

- Planning duration.
- Number of revisions.
- Approval latency.
- Plan drift frequency.
- Unplanned file changes.
- Rework caused by missed dependencies.
- Validation failures attributable to planning gaps.

---

# 16. Best Practices

- Separate planning from repository mutation.
- Begin with evidence, not assumptions.
- Read relevant Instructions before designing the change.
- Use Repository Intelligence before proposing architecture.
- Select Skills intentionally.
- Define explicit in-scope and out-of-scope boundaries.
- Decompose work into testable tasks.
- Make dependencies visible.
- Distinguish assumptions from confirmed facts.
- Define acceptance criteria before implementation.
- Define validation before coding.
- Keep the plan proportional to task risk.
- Use human approval where risk justifies it.
- Version materially changed plans.
- Detect and control plan drift.
- Preserve traceability from:
  - Prompt.
  - Plan.
  - Implementation.
  - Validation.
  - Evaluation.
- Treat the plan as a coordination artifact, not permanent repository truth.

---

# 17. Anti-Patterns

## 17.1 Planning After Coding

Creating a plan that merely documents implementation already performed.

## 17.2 The Generic Checklist Plan

Example:

```text
1. Update code.
2. Add tests.
3. Run tests.
```

Explain why this adds little engineering value.

## 17.3 Planning Without Repository Discovery

Inventing architecture rather than inspecting the repository.

## 17.4 Unlimited Planning

Spending disproportionate effort planning trivial work.

## 17.5 Planning Every Detail

Attempting to predict every line of code before execution.

## 17.6 Hidden Assumptions

Presenting uncertain conclusions as established repository facts.

## 17.7 Scope Expansion

Using a feature request as justification for unrelated refactoring.

## 17.8 Validation as an Afterthought

Planning implementation without specifying proof of correctness.

## 17.9 Silent Plan Drift

Allowing implementation to diverge without updating the plan.

## 17.10 Treating the Plan as Authoritative Knowledge

Confusing a task-scoped proposal with long-lived Repository Intelligence or governed Instructions.

## 17.11 Human Approval Theater

Requesting approval for a vague plan that does not expose meaningful decisions.

---

# 18. Architect’s Notes

### Architect’s Note — Plans Are Decision Boundaries

A useful plan exposes decisions that humans may need to challenge before implementation cost is incurred.

### Architect’s Note — Architecture Should Be Discovered Before It Is Extended

Agents should find existing repository patterns before inventing new ones.

### Architect’s Note — Planning and Validation Are Connected

A plan that does not explain how success will be demonstrated is incomplete.

### Architect’s Note — Planning Is Risk Management

The amount of planning should increase with:

- Uncertainty.
- Blast radius.
- Irreversibility.
- Security sensitivity.
- Contract impact.

### Architect’s Note — Avoid Plan Permanence

Task plans naturally expire. Long-lived insights should be promoted into governed knowledge through the appropriate process.

---

# 19. Enterprise Tips

### Enterprise Tip — Standardize Plan Structure

Provide teams with a common plan schema.

### Enterprise Tip — Require Scope Boundaries

Explicitly record both in-scope and out-of-scope work.

### Enterprise Tip — Attach Evidence

Plans should cite relevant repository components and requirements.

### Enterprise Tip — Use Risk-Based Approval

Do not require heavyweight approval for every minor change.

### Enterprise Tip — Record Plan Revisions

For governed work, preserve why the plan changed.

### Enterprise Tip — Connect Plans to Pull Requests

A PR can reference:

- Original Prompt.
- Approved plan.
- Validation evidence.
- Plan revisions.

### Enterprise Tip — Measure Drift

Repeated plan drift may indicate:

- Weak repository discovery.
- Poor Instructions.
- Missing Skills.
- Inadequate Repository Intelligence.
- Ambiguous Prompts.

---

# 20. Decision Points

## Decision Point 1 — Does This Task Need Explicit Planning Mode?

Evaluate complexity, risk, uncertainty, and blast radius.

## Decision Point 2 — How Much Discovery Is Enough?

Balance evidence gathering against unnecessary repository exploration.

## Decision Point 3 — Does This Change Require Human Approval?

Use risk classification.

## Decision Point 4 — Is a New Discovery a Minor Adjustment or Replanning Event?

Establish material-change criteria.

## Decision Point 5 — Can the Developer Proceed?

Verify:

- Scope defined.
- Dependencies known.
- Acceptance criteria clear.
- Validation defined.
- Required approvals obtained.

## Decision Point 6 — Should Planning Knowledge Be Promoted?

Determine whether a discovery belongs in:

- Instructions.
- Repository Intelligence.
- Skill.
- Steering Note proposal.
- Architecture documentation.

---

# 21. Exercises

### Exercise 1 — Build a Planning Artifact

Given the corporate fleet booking requirement:

- Identify scope.
- Identify likely affected layers.
- Identify dependencies.
- Define acceptance criteria.
- Define validation.

### Exercise 2 — Plan or Execute?

Classify several tasks according to whether explicit Planning Mode is justified.

Examples:

- Fix a typo.
- Add a fleet booking limit.
- Replace authentication provider.
- Rename a local private method.
- Introduce a new event contract.

### Exercise 3 — Identify Assumptions

Review a proposed fleet-booking plan and separate:

- Facts.
- Assumptions.
- Unknowns.

### Exercise 4 — Plan Versus Scratchpad

Classify artifacts as:

- Plan content.
- Scratchpad content.
- Repository Intelligence.
- Instruction.

### Exercise 5 — Detect Plan Drift

Compare an approved plan with a hypothetical Git diff and identify unplanned changes.

### Exercise 6 — Design an Approval Policy

Create a risk-based Planning Mode approval matrix.

### Exercise 7 — Validation-First Planning

Design validation requirements for:

- Booking limit.
- Idempotency.
- Outbox event.
- API compatibility.

### Exercise 8 — Harness Integration

Design the Harness states and artifacts required to implement governed Planning Mode.

---

# 22. Interview Questions

1. What is Planning Mode in AI-assisted software engineering?
2. Why should planning be separated from execution?
3. When should a coding agent enter Planning Mode?
4. When may explicit Planning Mode be unnecessary?
5. What is the difference between a Prompt and a Plan?
6. How does a Plan differ from a Scratchpad?
7. How does Planning Mode differ from the Lead Agent role?
8. Why is repository discovery necessary during planning?
9. What are Applicable Instructions?
10. How should an agent select Skills during planning?
11. What information should a strong implementation plan contain?
12. Why should scope and out-of-scope boundaries be explicit?
13. How should assumptions and unknowns be represented?
14. Why should acceptance criteria be defined before implementation?
15. Why should validation strategy be established during planning?
16. What is plan drift?
17. How can a Harness detect plan drift?
18. When should a plan be updated?
19. When should an updated plan require renewed human approval?
20. How should a plan be handed to a Developer Agent?
21. What is the relationship between planning and deterministic validation?
22. Should plans become permanent repository knowledge?
23. How can enterprise teams govern Planning Mode without slowing low-risk work?
24. What metrics can indicate poor planning quality?
25. How can repeated plan failures improve Instructions, Skills, or Repository Intelligence?

---

# 23. Chapter Summary

Summarize the chapter around these principles:

- Planning Mode deliberately separates understanding from execution.
- Planning begins with task interpretation and repository discovery.
- The agent must identify Applicable Instructions and relevant Skills.
- A useful plan defines:
  - Scope.
  - Tasks.
  - Dependencies.
  - Risks.
  - Assumptions.
  - Acceptance criteria.
  - Validation.
- A Prompt requests work; a Plan explains the proposed engineering approach.
- A Scratchpad supports temporary investigation; it is not the Plan.
- Planning Mode is a phase; the Lead Agent is a role that may orchestrate that phase.
- Human approval should be proportional to risk.
- Material discoveries require replanning rather than silent divergence.
- Plan drift should be observable and governable.
- The Developer Agent should receive a sufficiently complete plan to execute without unnecessarily rediscovering the entire problem.
- Inside a Harness, planning becomes a traceable lifecycle stage connecting intent to implementation, validation, evaluation, and governance.
- Planning improves AI-assisted engineering by making intent, assumptions, scope, and proof of success explicit before repository mutation.

End with the principle:

> **Understand first. Plan second. Execute only when the intended change, its boundaries, and its validation are sufficiently clear.**

---

# 24. Further Reading

Organize the final chapter references around:

### Internal handbook cross-references

- Chapter 5 — Repository Discovery
- Chapter 6 — Instructions
- Chapter 7 — Skills
- Chapter 8 — Prompts
- Chapter 9 — Roles
- Chapter 10 — Steering Notes
- Chapter 11 — Knowledge Sources
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture
- Chapter 14 — Lead Agent
- Chapter 15 — Developer Agent
- Chapter 16 — Reviewer Agent
- Chapter 17 — Validator Agent
- Chapter 18 — Evaluator Agent
- Chapter 22 — CLAUDE.md
- Chapter 23 — Claude Skills
- Chapter 26 — Tool Calling

### External topic categories

- Claude Code planning and agent workflows.
- Repository-aware AI development.
- Human-in-the-loop AI systems.
- Software change planning.
- Architecture decision-making.
- Risk-based software governance.
- Deterministic software validation.
- Multi-agent software engineering.
- Plan-and-execute agent architectures.

The handbook’s Part IV placement confirms **Chapter 27 — Planning Mode** follows Tool Calling and precedes Enterprise Workflows, making it the bridge between Claude’s repository capabilities and controlled enterprise execution. AI development handbook

**Chapter 27 outline is complete.**
