# Chapter 14 — Lead Agent

**Part III — Harness Engineering**

**Target file:** `book/14-lead-agent.md`

This chapter builds on the handbook’s established distinction between Repository Intelligence, Roles, Skills, Prompts, Steering Notes, deterministic validation, and the Harness orchestration model.

---

## 1. Story-Driven Opening

### Scenario: Corporate Fleet Booking Expansion

* Alpha Car Detailing receives a new enterprise requirement for corporate fleet customers.
* Corporate customers must be able to:

  * create fleet bookings,
  * select multiple vehicles,
  * choose servicing stations,
  * request detailing packages,
  * receive booking confirmation,
  * comply with existing architectural, security, and observability standards.
* The user provides a high-level goal rather than detailed implementation instructions.
* The Harness starts a new execution.
* The Lead Agent receives the goal.
* The Lead Agent must determine:

  * what the user actually wants,
  * which repository areas are relevant,
  * which Instructions apply,
  * which Steering Notes affect the work,
  * which Skills are available,
  * which specialist Roles are required,
  * how the work should be decomposed,
  * what must happen sequentially,
  * what can happen independently,
  * where deterministic gates must run,
  * when humans must approve decisions.
* Contrast an uncontrolled AI Agent immediately editing code with a Lead Agent first coordinating the work.

### Central Question

How does an enterprise Harness convert a broad engineering goal into controlled, traceable, validated work without allowing the Lead Agent to become an all-powerful autonomous developer?

---

## 2. Learning Objectives

By the end of the chapter, the reader should be able to:

* Define the Lead Agent.
* Explain its role inside an AI Engineering Harness.
* Distinguish orchestration authority from implementation authority.
* Interpret goals and convert them into executable tasks.
* Use Repository Intelligence before planning implementation.
* Apply Instructions, Steering Notes, Skills, Roles, and Knowledge Sources during planning.
* Decompose work into dependent and independent tasks.
* Sequence work across specialist agents.
* Coordinate agent handoffs.
* Propagate relevant context without unnecessarily copying the entire conversation.
* Maintain workflow state.
* Identify risks and unresolved decisions.
* Decide when retry is appropriate.
* Coordinate deterministic validation gates.
* Hand work to Reviewer, Validator, and Evaluator roles.
* Escalate decisions requiring human authority.
* Explain Lead Agent behavior in single-agent and multi-agent workflows.
* Implement a practical Lead Agent workflow using Claude Code.
* Compare equivalent approaches in GitHub Copilot and OpenAI Codex.

---

## 3. Background

### 3.1 From Prompt Execution to Coordinated Engineering

* Why executing a prompt is not the same as orchestrating engineering work.
* Why enterprise tasks rarely map to a single AI interaction.
* Relationship to:

  * Chapter 5 — Repository Discovery,
  * Chapter 6 — Instructions,
  * Chapter 7 — Skills,
  * Chapter 8 — Prompts,
  * Chapter 9 — Roles,
  * Chapter 10 — Steering Notes,
  * Chapter 11 — Knowledge Sources,
  * Chapter 12 — What Is a Harness?,
  * Chapter 13 — Harness Architecture.

### 3.2 Why a Lead Role Is Necessary

Without explicit coordination:

* agents duplicate work,
* context becomes inconsistent,
* dependencies are missed,
* validation happens too late,
* failures cause uncontrolled retries,
* architectural decisions become implicit,
* humans cannot easily understand why work occurred.

### 3.3 Coordination Is Not Implementation

Establish the core principle:

> The Lead Agent coordinates the engineering process. It does not replace specialist roles, deterministic validation gates, or human approval.

---

## 4. Concepts

## 4.1 What Is a Lead Agent?

Define the Lead Agent as the orchestration and planning role responsible for converting a goal into a controlled execution plan.

Core activities:

```text
Interpret
    ↓
Discover
    ↓
Plan
    ↓
Delegate
    ↓
Coordinate
    ↓
Observe
    ↓
Escalate
    ↓
Complete
```

Clarify that the Lead Agent is a **Role**, not necessarily:

* a separate AI model,
* a separate process,
* a separate machine,
* a separate vendor product.

---

## 4.2 Lead Agent Responsibilities

Primary responsibilities:

* interpret goals,
* gather Repository Intelligence,
* read Instructions,
* read Steering Notes,
* inspect relevant Knowledge Sources,
* select Skills,
* select Roles,
* identify dependencies,
* decompose work,
* establish execution sequence,
* issue role-specific tasks,
* manage handoffs,
* track workflow state,
* coordinate gates,
* collect outcomes,
* identify failures,
* decide whether retry is permitted,
* escalate unresolved decisions,
* record important decisions,
* prepare work for human approval.

---

## 4.3 Lead Agent Authority

The Lead Agent may typically:

* inspect repository information,
* select approved Skills,
* invoke approved Roles,
* create execution plans,
* assign tasks,
* reorder tasks when dependencies require it,
* stop execution when validation fails,
* request retries within policy,
* request additional review,
* escalate uncertainty,
* collect workflow outputs.

---

## 4.4 Lead Agent Limits

The Lead Agent must not automatically:

* bypass deterministic gates,
* weaken test requirements,
* change Instructions,
* change enterprise policies,
* modify Steering Notes without permission,
* approve its own architectural exceptions,
* accept failed security gates,
* override human-required approvals,
* silently change acceptance criteria,
* repeatedly retry without limits,
* treat evaluator scores as deterministic proof,
* approve production deployment unless explicitly authorized.

### Architect's Note

Orchestration authority should always be narrower than enterprise governance authority.

---

## 4.5 Goal Interpretation

### Goal Example

```text
Add corporate fleet booking support while preserving existing
Clean Architecture boundaries and passing all validation gates.
```

Lead Agent interpretation should identify:

* desired outcome,
* scope,
* constraints,
* acceptance criteria,
* unknowns,
* potential architectural impact,
* affected bounded contexts,
* validation requirements.

### Goal vs Task

Explain:

```text
Goal
  ↓
Desired Outcome

Task
  ↓
A unit of work contributing to that outcome
```

---

## 4.6 Repository Intelligence Usage

Before planning implementation, the Lead Agent discovers:

* solution structure,
* projects,
* architectural boundaries,
* existing booking behavior,
* domain entities,
* application services,
* APIs,
* database patterns,
* event contracts,
* tests,
* CI validation,
* existing similar capabilities.

Reinforce:

> Planning without Repository Intelligence is speculation.

---

## 4.7 Reading Instructions

The Lead Agent determines which persistent engineering constraints affect the goal.

Examples:

* Clean Architecture boundaries,
* dependency direction,
* naming conventions,
* testing requirements,
* logging requirements,
* security requirements,
* integration event standards.

---

## 4.8 Reading Steering Notes

Use Steering Notes to understand current mission context such as:

* corporate fleet booking is the release priority,
* no breaking public API changes,
* database migration must remain backward compatible,
* security review is mandatory,
* release deadline requires minimum unrelated refactoring.

Explain that Steering Notes influence planning but do not replace Instructions.

---

## 4.9 Knowledge Sources

Lead Agent may consult:

* ADRs,
* architecture documentation,
* API specifications,
* event schemas,
* business rules,
* deployment documentation,
* issue history,
* domain documentation.

Discuss resolving conflicting evidence through escalation rather than unsupported assumptions.

---

## 4.10 Selecting Skills

Possible approved Skills:

```text
create-rest-endpoint
add-domain-behavior
add-application-command
add-integration-event
add-ef-core-migration
add-unit-tests
add-integration-tests
```

Skill selection criteria:

* task type,
* repository architecture,
* required outcome,
* approved implementation patterns,
* known constraints.

Clarify:

> The Lead Agent selects the appropriate procedure; the Skill defines how recurring work should be performed.

---

## 4.11 Selecting Participating Roles

Possible roles:

* Developer,
* Reviewer,
* Validator,
* Evaluator,
* Architect,
* Security Reviewer.

Role selection should depend on risk and task characteristics.

Example:

```text
API change
    ↓
Developer
    ↓
Reviewer
    ↓
Validator
    ↓
Evaluator
```

High-risk flow:

```text
Security-sensitive API change
        ↓
Developer
        ↓
Reviewer
        ↓
Security Reviewer
        ↓
Validator
        ↓
Evaluator
        ↓
Human Approval
```

---

## 4.12 Task Decomposition

Corporate fleet booking may be decomposed into:

1. inspect current booking domain,
2. define fleet booking domain changes,
3. implement application behavior,
4. expose API endpoint,
5. persist required data,
6. publish integration event,
7. add unit tests,
8. add integration tests,
9. run deterministic gates,
10. perform review,
11. evaluate result,
12. request approval.

Discuss decomposition granularity.

Too coarse:

```text
Implement corporate fleet booking.
```

Too fine:

```text
Create one property.
Create second property.
Create constructor argument.
```

Appropriate:

```text
Implement corporate fleet booking domain behavior
using existing booking aggregate conventions.
```

---

## 4.13 Planning

The Lead Agent produces an execution plan containing:

* goal,
* scope,
* repository evidence,
* applicable Instructions,
* selected Skills,
* participating Roles,
* tasks,
* dependencies,
* validation gates,
* risks,
* expected outputs,
* approval points.

---

## 4.14 Dependency Identification

Examples:

```text
Domain Model
    ↓
Application Logic
    ↓
Persistence
    ↓
API
```

Another dependency:

```text
Event Contract
    ↓
Publisher
    ↓
Consumer Compatibility Validation
```

Explain:

* structural dependency,
* data dependency,
* contract dependency,
* validation dependency,
* approval dependency.

---

## 4.15 Work Sequencing

Discuss:

### Sequential Work

Required where one output becomes another task's input.

### Parallel Work

Potential examples:

* unit-test preparation,
* documentation updates,
* contract review,
* security analysis.

### Controlled Parallelism

Parallel execution should not introduce contradictory edits or hidden dependencies.

---

## 4.16 Agent Handoffs

Define a handoff as transfer of responsibility plus sufficient context.

Example:

```text
Lead
  ↓
Developer
  ↓
Reviewer
  ↓
Validator
  ↓
Evaluator
```

Each handoff should communicate:

* task,
* scope,
* relevant context,
* constraints,
* produced artifacts,
* unresolved questions,
* expected output.

---

## 4.17 Context Propagation

Discuss why copying all historical context to every role is undesirable.

Propagate:

* goal,
* task-specific evidence,
* applicable Instructions,
* relevant Steering Notes,
* selected Skill,
* changed files,
* previous role output,
* validation results,
* unresolved issues.

Avoid:

* irrelevant conversation history,
* unrelated repository files,
* unnecessary secrets,
* stale findings.

Introduce the concept of **minimum sufficient context**.

---

## 4.18 State Management

Possible workflow states:

```text
Received
Discovered
Planned
Assigned
InProgress
GatePending
GateFailed
ReviewPending
ValidationPending
EvaluationPending
ApprovalPending
Completed
Escalated
Failed
```

Discuss storing:

* current task,
* active role,
* completed tasks,
* gate results,
* retries,
* decisions,
* blockers,
* generated artifacts.

---

## 4.19 Risk Identification

Lead Agent should recognize risks such as:

* public contract changes,
* database migration,
* security-sensitive changes,
* cross-service impact,
* architectural uncertainty,
* missing tests,
* ambiguous requirements,
* incompatible events,
* large modification scope.

Risk can influence:

* role selection,
* gate selection,
* approval level,
* retry limits.

---

## 4.20 Decision Logging

Record important orchestration decisions such as:

```text
Decision:
Reuse the existing Booking aggregate.

Evidence:
Existing reservation behavior already uses Booking as the
transactional consistency boundary.

Impact:
No new FleetBooking aggregate will be introduced.

Approval:
No architecture exception required.
```

Differentiate:

* execution logs,
* decision logs,
* ADRs.

Not every orchestration decision requires an ADR.

---

## 4.21 Escalation

Escalate when:

* repository evidence conflicts,
* requirements are ambiguous,
* architecture changes are required,
* Instructions conflict,
* validation repeatedly fails,
* security policies are affected,
* an approved Skill is insufficient,
* a breaking contract appears necessary.

---

## 4.22 Retry Decisions

Retry is not equivalent to repeating the same prompt.

A retry should occur only when:

* the failure is understood,
* corrective context exists,
* the failure is potentially recoverable,
* retry limits permit it.

Example:

```text
Test Gate Failed
      ↓
Diagnose Cause
      ↓
Recoverable?
   /       \
 Yes        No
  ↓          ↓
Developer   Escalate
Retry
```

---

## 4.23 Deterministic Gate Coordination

The Lead Agent coordinates gates but does not determine whether they passed.

Potential gates:

* build,
* unit tests,
* integration tests,
* lint,
* architecture,
* security,
* API contract,
* event contract.

Core rule:

> The Lead Agent may invoke, sequence, and react to gates. It may not reinterpret a failed deterministic result as a pass.

---

## 4.24 Validation and Evaluation Handoff

Explain distinction:

### Validation

Answers:

> Did the implementation satisfy deterministic technical requirements?

### Evaluation

Answers:

> Is the implementation suitable, complete, maintainable, and aligned with the intended outcome?

Lead Agent coordinates both but owns neither judgment independently.

---

## 4.25 Human Approval

Human approval may be mandatory for:

* architecture exceptions,
* security-sensitive changes,
* breaking APIs,
* production deployment,
* policy modifications,
* Steering Note updates,
* changes to Instructions,
* high-impact database migration.

---

## 4.26 Failure Handling

Categories:

* execution failure,
* tool failure,
* code failure,
* gate failure,
* review failure,
* evaluation failure,
* context failure,
* policy failure,
* unrecoverable ambiguity.

Possible actions:

```text
Retry
Re-plan
Reassign
Request Specialist
Rollback
Escalate
Stop
```

---

## 5. Architecture Discussion

## 5.1 Lead Agent Responsibility Boundary

Diagram:

```text
                    Human Authority
                          │
                          ▼
                 ┌─────────────────┐
                 │   Lead Agent    │
                 │                 │
Goal ──────────► │ Interpret       │
Context ───────► │ Plan            │
                 │ Coordinate      │
                 │ Track           │
                 │ Escalate        │
                 └────────┬────────┘
                          │
           ┌──────────────┼───────────────┐
           ▼              ▼               ▼
       Developer       Reviewer       Validator
                                            │
                                            ▼
                                     Deterministic
                                         Gates
```

Boundary emphasis:

Lead Agent coordinates.

Specialists perform specialized work.

Gates produce deterministic outcomes.

Humans retain governed authority.

---

## 5.2 Planning and Decomposition Flow

```text
User Goal
   ↓
Interpret Outcome
   ↓
Gather Repository Intelligence
   ↓
Read Instructions
   ↓
Read Steering Notes
   ↓
Identify Constraints
   ↓
Identify Dependencies
   ↓
Select Skills
   ↓
Select Roles
   ↓
Create Tasks
   ↓
Build Execution Plan
```

---

## 5.3 Role Orchestration

```text
                    ┌───────────┐
                    │   Lead    │
                    └─────┬─────┘
                          │
        ┌─────────────────┼─────────────────┐
        ▼                 ▼                 ▼
   Developer          Architect      Security Reviewer
        │
        ▼
      Gates
        │
        ▼
    Reviewer
        │
        ▼
   Validator
        │
        ▼
   Evaluator
        │
        ▼
 Human Approval
```

---

## 5.4 Agent Handoff Architecture

```text
Lead Agent
    │
    │ Task + Context + Constraints
    ▼
Developer
    │
    │ Changes + Notes
    ▼
Reviewer
    │
    │ Findings
    ▼
Validator
    │
    │ Gate Evidence
    ▼
Evaluator
    │
    │ Assessment
    ▼
Lead Agent
```

---

## 5.5 Escalation Flow

```text
Problem Detected
      ↓
Can Lead resolve within authority?
       / \
     Yes  No
      │    │
      │    ▼
      │  Specialist Required?
      │     / \
      │   Yes  No
      │    │    │
      │    ▼    ▼
      │ Specialist Human
      │    │    Approval
      └────┴───────► Resume / Stop
```

---

## 5.6 Lead Agent Inside the Harness

```text
┌──────────────────────────────────────────────┐
│                AI Engineering Harness        │
│                                              │
│  Goal                                        │
│    ↓                                         │
│  Lead Agent                                  │
│    │                                         │
│    ├── Repository Intelligence               │
│    ├── Instructions                          │
│    ├── Steering Notes                        │
│    ├── Skills                                │
│    ├── Knowledge Sources                     │
│    │                                         │
│    ├── Developer                             │
│    ├── Reviewer                              │
│    ├── Validator                             │
│    └── Evaluator                             │
│                                              │
│         ↓                                    │
│   Deterministic Gates                        │
│         ↓                                    │
│   Workflow State                             │
│         ↓                                    │
│   Human Approval                             │
│         ↓                                    │
│       Output                                 │
└──────────────────────────────────────────────┘
```

---

## 6. Professional Diagrams

Include polished diagrams for:

1. Lead Agent responsibility boundary.
2. Goal interpretation and planning flow.
3. Task decomposition.
4. Dependency graph.
5. Role orchestration.
6. Agent handoffs.
7. Context propagation.
8. Workflow state machine.
9. Retry and failure handling.
10. Escalation flow.
11. Deterministic gate coordination.
12. Lead Agent inside the complete Harness.

---

## 7. Hands-on Example

## 7.1 Goal

```text
Implement corporate fleet booking support for Alpha Car Detailing.

Corporate customers must be able to submit multiple vehicles
under one booking while preserving existing booking behavior,
architecture rules, validation, security, and observability.
```

---

## 7.2 Discovery

Lead Agent identifies:

* Booking API,
* Booking application layer,
* Booking aggregate,
* customer model,
* station model,
* persistence implementation,
* integration events,
* test projects,
* applicable Instructions,
* current Steering Note.

---

## 7.3 Proposed Plan

Example:

```text
Task 1
Inspect current Booking aggregate and identify extension points.

Task 2
Define fleet booking domain behavior.

Task 3
Implement application-layer use case.

Task 4
Extend persistence.

Task 5
Expose REST endpoint.

Task 6
Publish FleetBookingCreated integration event.

Task 7
Add unit and integration tests.

Task 8
Run deterministic validation gates.

Task 9
Perform code review.

Task 10
Validate acceptance criteria.

Task 11
Evaluate maintainability and architectural alignment.

Task 12
Request human approval where required.
```

---

## 7.4 Dependency Model

```text
Domain
   ↓
Application
   ↓
Infrastructure
   ↓
API
   ↓
Integration Event
   ↓
Validation
```

---

## 7.5 Role Assignment

| Task                 | Role      | Skill                     |
| -------------------- | --------- | ------------------------- |
| Domain changes       | Developer | `add-domain-behavior`     |
| Application use case | Developer | `add-application-command` |
| API                  | Developer | `create-rest-endpoint`    |
| Event                | Developer | `add-integration-event`   |
| Code quality         | Reviewer  | Review workflow           |
| Deterministic checks | Validator | Validation workflow       |
| Quality assessment   | Evaluator | Evaluation workflow       |

---

## 7.6 Gate Sequence

```text
Build
  ↓
Unit Tests
  ↓
Integration Tests
  ↓
Architecture Gate
  ↓
Contract Gate
  ↓
Security Gate
```

---

## 7.7 Example Failure

Integration tests fail because a corporate customer identifier is not persisted.

Lead Agent:

1. records the failed gate,
2. preserves failure evidence,
3. assigns corrective work to Developer,
4. reruns only allowed prerequisite gates,
5. continues when deterministic validation succeeds.

It does **not** mark the failure acceptable.

---

## 7.8 Completion

Final Lead Agent output contains:

* goal status,
* completed tasks,
* changed artifacts,
* gate results,
* reviewer findings,
* validation evidence,
* evaluator result,
* unresolved risks,
* approvals,
* decision log.

---

## 8. Claude Example

## 8.1 Claude Code as Lead Agent

Show a practical Claude-first implementation using:

* `CLAUDE.md`,
* repository Instructions,
* Skills,
* role prompts,
* Steering Notes,
* harness scripts,
* structured execution state.

---

## 8.2 Example Lead Role Definition

Illustrate a conceptual Lead role file containing:

* responsibility,
* permitted actions,
* prohibited actions,
* required discovery,
* planning format,
* handoff requirements,
* escalation requirements.

---

## 8.3 Planning with Claude

Example interaction:

```text
/goal Add corporate fleet booking and ensure all relevant
architecture, test, security, and contract gates pass.
```

Claude Lead workflow:

```text
Goal
 ↓
Repository Discovery
 ↓
Relevant Instructions
 ↓
Steering Note
 ↓
Plan
 ↓
Role Assignment
 ↓
Execution
```

---

## 8.4 Claude Skills

Show the Lead Agent invoking approved Skills instead of inventing implementation procedures repeatedly.

---

## 8.5 Claude Role Handoffs

Example:

```text
Lead → Developer
Lead → Reviewer
Lead → Validator
Lead → Evaluator
```

Discuss separate context windows versus same-session role simulation.

---

## 8.6 Claude Hooks and Gates

Explain how Claude-triggered workflow actions can invoke deterministic commands while the pass/fail decision remains external and deterministic.

---

## 9. GitHub Copilot Comparison

Compare:

* instruction handling,
* repository context,
* agent mode,
* custom agents,
* prompt files,
* Skills-equivalent reusable procedures,
* tool invocation,
* task planning,
* orchestration capabilities,
* validation integration.

Explain where an external Harness is still necessary.

Focus on engineering equivalence rather than feature-by-feature marketing comparison.

---

## 10. OpenAI Codex Comparison

Compare:

* repository discovery,
* task planning,
* agent execution,
* instruction usage,
* delegated work,
* command/tool execution,
* validation,
* iteration,
* external orchestration.

Reinforce:

> Lead Agent is an architectural responsibility. The implementation mechanism can change between AI coding platforms.

---

## 11. Best Practices

* Discover before planning.
* Make plans evidence-based.
* Keep Lead authority explicit.
* Decompose goals into meaningful engineering tasks.
* Identify dependencies before execution.
* Prefer approved Skills over improvised procedures.
* Select roles based on task risk.
* Propagate minimum sufficient context.
* Preserve deterministic gate ownership.
* Record decisions separately from raw logs.
* Define retry limits.
* Escalate uncertainty early.
* Preserve failed gate evidence.
* Keep human approval explicit.
* Make workflow state observable.
* Treat plan changes as auditable events.
* Keep orchestration vendor-neutral.

---

## 12. Anti-patterns

### 12.1 The Lead Agent Does Everything

One agent plans, codes, reviews, validates, evaluates, and approves itself.

### 12.2 Planning Before Discovery

The Lead Agent invents architecture from the prompt.

### 12.3 Unlimited Authority

The Lead Agent overrides standards or gates.

### 12.4 Fake Delegation

Roles exist only as labels while one context performs every decision without separation of responsibility.

### 12.5 Context Dumping

Every agent receives the entire repository and complete conversation history.

### 12.6 Infinite Retry Loop

Failed gates repeatedly trigger regeneration without diagnosis or limits.

### 12.7 Self-Approval

The Lead Agent decides its own architectural exception is acceptable.

### 12.8 Ignoring Failed Gates

AI reasoning is used to explain why a deterministic failure can be ignored.

### 12.9 Hidden Re-planning

The execution plan changes without being recorded.

### 12.10 Over-Decomposition

The Lead Agent turns implementation into hundreds of microscopic tasks.

### 12.11 Under-Decomposition

The plan contains only:

```text
Implement feature.
Test feature.
```

### 12.12 Handoffs Without Evidence

Reviewer or Validator receives changes without goal, constraints, or relevant context.

---

## 13. Architect's Notes

### Architect's Note: The Lead Agent Is a Control Role

The Lead Agent should be designed more like a workflow coordinator than a senior developer with unlimited permissions.

### Architect's Note: Authority Must Be Machine-Enforceable

Important boundaries should not depend solely on prompt wording.

### Architect's Note: Plans Are Runtime Artifacts

Execution plans should be recordable, inspectable, and auditable.

### Architect's Note: Specialization Matters More as Risk Increases

Simple changes may need fewer roles.

Enterprise-critical changes may require:

* Architect,
* Security Reviewer,
* Compliance,
* human approval.

### Architect's Note: Deterministic Truth Lives Outside the Model

Build, test, lint, security, and contract outcomes should remain machine-verifiable.

---

## 14. Enterprise Tips

### Enterprise Tip: Define an Authority Matrix

Document which decisions may be made by:

* Lead Agent,
* specialist Agent,
* deterministic system,
* Tech Lead,
* Architect,
* Security team,
* release approver.

### Enterprise Tip: Store Plans with Runs

Preserve:

* original goal,
* initial plan,
* plan revisions,
* task outcomes,
* failures,
* approvals.

### Enterprise Tip: Use Risk-Based Role Selection

Do not require every possible role for every trivial change.

### Enterprise Tip: Limit Retry Budgets

Define retry count by failure class.

### Enterprise Tip: Make Escalation a Successful Outcome

An Agent that recognizes insufficient authority is behaving correctly.

---

## 15. Decision Points

### Decision Point 1

Should the Lead Agent be permitted to modify code directly?

Discuss options:

* orchestration only,
* orchestration plus low-risk implementation,
* full implementation authority.

Recommended enterprise baseline:

Separate orchestration from Developer responsibility.

---

### Decision Point 2

Should every task use a multi-agent workflow?

Consider:

* task complexity,
* risk,
* cost,
* latency,
* audit requirements.

---

### Decision Point 3

Should planning be human-approved before execution?

Potential model:

```text
Low Risk
Automatic Plan Execution

Medium Risk
Plan + Recorded Review

High Risk
Human Plan Approval
```

---

### Decision Point 4

When should Architect involvement become mandatory?

Examples:

* new service boundary,
* new aggregate,
* breaking contract,
* cross-service transaction design,
* new infrastructure dependency.

---

### Decision Point 5

Who controls retry limits?

Prefer Harness policy rather than Lead Agent discretion alone.

---

### Decision Point 6

What state should survive Harness restart?

At minimum:

* goal,
* plan,
* completed tasks,
* active task,
* gate evidence,
* retries,
* unresolved decisions.

---

## 16. Single-Agent Versus Multi-Agent Workflows

## 16.1 Single-Agent Workflow

```text
User
 ↓
Lead Role
 ↓
Developer Role
 ↓
Reviewer Role
 ↓
Validator Role
```

One AI execution environment adopts separate governed roles sequentially.

Advantages:

* simpler,
* lower cost,
* easier initial adoption.

Limitations:

* weaker isolation,
* context contamination,
* self-review risk.

---

## 16.2 Multi-Agent Workflow

```text
                Lead
          ┌──────┼──────┐
          ▼      ▼      ▼
      Developer Architect Security
          │
          ▼
       Reviewer
          │
          ▼
       Validator
          │
          ▼
       Evaluator
```

Advantages:

* stronger specialization,
* context isolation,
* independent perspectives,
* scalable execution.

Limitations:

* orchestration complexity,
* state management,
* higher execution cost,
* harder debugging.

---

## 16.3 Hybrid Enterprise Model

Recommend a pragmatic model where role separation reflects task risk rather than maximizing agent count.

---

## 17. Exercises

### Exercise 1 — Goal Interpretation

Given:

```text
Allow corporate customers to cancel fleet bookings.
```

Identify:

* outcome,
* likely scope,
* constraints,
* unknowns,
* required Repository Intelligence.

---

### Exercise 2 — Task Decomposition

Create an execution plan for adding fleet booking cancellation.

---

### Exercise 3 — Role Selection

Select participating roles for:

* adding a validation rule,
* changing authentication,
* introducing a new integration event,
* changing database ownership,
* renaming an internal method.

Explain each selection.

---

### Exercise 4 — Dependency Mapping

Create a dependency graph for:

```text
Corporate Fleet Booking → Payment Authorization
```

---

### Exercise 5 — Handoff Design

Design a Developer → Reviewer handoff contract.

---

### Exercise 6 — Retry Policy

Define retry rules for:

* compile failure,
* unit test failure,
* security gate failure,
* unavailable external tool,
* architecture violation.

---

### Exercise 7 — Escalation

Identify which of ten sample Lead Agent decisions require human escalation.

---

### Exercise 8 — State Machine

Design the Harness state transitions for a failed validation followed by a successful retry.

---

### Exercise 9 — Lead Role Definition

Create a Lead Agent role specification containing:

* responsibilities,
* authority,
* prohibited actions,
* required outputs,
* escalation rules.

---

### Exercise 10 — Alpha Car Detailing Workflow

Design the complete workflow:

```text
User Goal
    ↓
Lead Agent
    ↓
Discover Context
    ↓
Build Plan
    ↓
Assign Work
    ↓
Developer
    ↓
Gates
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

## 18. Interview Questions

1. What is the purpose of a Lead Agent?
2. How is a Lead Agent different from a Developer Agent?
3. Why should the Lead Agent gather Repository Intelligence before planning?
4. What is the difference between a goal and a task?
5. How should a Lead Agent select Skills?
6. How should it select participating Roles?
7. What makes task decomposition effective?
8. Why are dependencies important during planning?
9. What is an agent handoff?
10. What information should be propagated during a handoff?
11. Why should agents receive minimum sufficient context?
12. What state should an enterprise Harness maintain?
13. How should the Lead Agent respond to a deterministic gate failure?
14. Why must the Lead Agent not override deterministic validation?
15. What is the difference between validation and evaluation?
16. When should the Lead Agent escalate to a human?
17. How should retry limits be governed?
18. What risks arise when the same Agent develops and reviews its own work?
19. When is a single-agent workflow appropriate?
20. When is a multi-agent workflow justified?
21. How should Lead Agent decisions be logged?
22. Why should Instructions and Steering Notes have different ownership models?
23. Can a Lead Agent modify Instructions automatically?
24. How does the Lead Agent fit within Harness Architecture?
25. How would you design Lead Agent authority for a regulated enterprise environment?

---

## 19. Chapter Summary

Reinforce:

* The Lead Agent is the orchestration and planning role of the Harness.
* It converts goals into controlled execution plans.
* Repository Intelligence must precede implementation planning.
* Instructions establish persistent constraints.
* Steering Notes communicate current priorities.
* Skills provide reusable procedures.
* Roles provide specialist responsibilities.
* The Lead Agent decomposes work and identifies dependencies.
* It coordinates execution and agent handoffs.
* Workflow state must remain observable and auditable.
* The Lead Agent can coordinate deterministic gates but cannot override them.
* Validation, evaluation, and approval remain independent responsibilities.
* Retry should be bounded and evidence-driven.
* Escalation is part of correct Harness behavior.
* Human authority remains mandatory for governed decisions.
* Lead Agent is an architectural pattern independent of Claude Code, GitHub Copilot, or Codex.

Conclude with the transition:

> The Lead Agent determines what work must happen and coordinates its execution. Chapter 15 moves to the role that performs the implementation itself: the Developer Agent.

---

## 20. Further Reading

Include references covering:

* agent orchestration,
* multi-agent systems,
* workflow engines,
* distributed workflow state,
* task decomposition,
* software architecture governance,
* human-in-the-loop systems,
* deterministic software validation,
* Claude Code agent workflows,
* GitHub Copilot agent capabilities,
* OpenAI Codex agent workflows,
* enterprise AI governance.

Prefer:

* official vendor documentation,
* established software architecture references,
* primary research,
* recognized enterprise engineering publications.

---

**Chapter 14 outline is complete.**
