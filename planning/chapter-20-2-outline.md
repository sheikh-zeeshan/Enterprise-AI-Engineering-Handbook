# Chapter — Scratchpad and Run Workspace  

## Detailed Outline

**Part III — Harness Engineering**

This chapter extends the Harness architecture by introducing the temporary execution state required while a Harness run is in progress. It follows the handbook’s established enterprise focus, frozen terminology, Claude-first/vendor-neutral approach, Alpha Car Detailing running example, and standard chapter structure. AI development handbook

---

## 1. Story-Driven Opening — The Work Between the Steps

Use an Alpha Car Detailing change request as the opening scenario:

> Add corporate fleet booking cancellation with cancellation-reason auditing and ensure existing booking workflows remain compatible.

Follow the request through the Harness:

```text
Prompt
   ↓
Lead Agent
   ↓
Plan
   ↓
Developer Agent
   ↓
Reviewer Agent
   ↓
Validator Agent
   ↓
Evaluator Agent
```

Show the practical problem: during execution, agents discover relevant files, make observations, identify uncertainties, investigate dependencies, record intermediate decisions, encounter validation failures, and hand work to other agents.

Ask the central architectural question:

**Where does temporary execution knowledge belong?**

Establish that putting everything into the Prompt, Plan, Repository Intelligence, or Harness Memory mixes fundamentally different kinds of state.

Introduce:

```text
Run Workspace
   ├── plan.md
   ├── scratchpad.md
   ├── agent-handoffs/
   ├── validation/
   └── outputs/
```

Establish the chapter principle:

> **The Scratchpad is temporary, task-scoped working state. It helps agents reason and coordinate during execution, but it is not an authoritative source of project knowledge.**

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define the purpose of a Scratchpad.
- Explain why agents require temporary execution state.
- Distinguish Prompt, Plan, Scratchpad, Steering Note, Repository Intelligence, and Harness Memory.
- Design a run-scoped workspace.
- Define what agents may write into a Scratchpad.
- Define what must never become authoritative merely because it appears in a Scratchpad.
- Coordinate state across Lead, Developer, Reviewer, Validator, and Evaluator Agents.
- Design explicit agent handoffs.
- Support parallel-agent Scratchpads.
- Design checkpoints for failure recovery and resumability.
- Promote validated discoveries into governed knowledge.
- Establish cleanup and retention policies.
- Preserve auditability without turning temporary state into permanent knowledge.
- Apply appropriate security controls to run-scoped data.
- integrate the Run Workspace into the Harness architecture.

---

# 3. Background — Why Agents Need Working State

## 3.1 AI execution is not a single operation

Explain the difference between:

```text
Request → Response
```

and enterprise Harness execution:

```text
Goal
  ↓
Discovery
  ↓
Planning
  ↓
Implementation
  ↓
Review
  ↓
Validation
  ↓
Evaluation
  ↓
Outcome
```

## 3.2 The state created during execution

Discuss examples:

- observations;
- assumptions;
- unresolved questions;
- files under investigation;
- dependencies discovered;
- implementation progress;
- decisions made during execution;
- validation failures;
- retry information;
- handoff context;
- checkpoints.

## 3.3 Why the Prompt is insufficient

A Prompt describes the task but should not continually absorb execution history.

## 3.4 Why the Plan is insufficient

A Plan represents intended execution, while execution itself produces discoveries not known when planning began.

## 3.5 Why persistent knowledge is insufficient

Repository Intelligence and Harness Memory serve different purposes and lifetimes.

Introduce the need for a distinct **temporary execution-state boundary**.

---

# 4. Concepts

## 4.1 What Is a Scratchpad?

Define the Scratchpad as:

> A temporary, run-scoped working area used by agents to record observations, assumptions, unresolved questions, intermediate decisions, investigation state, and execution progress.

Explain its characteristics:

```text
Temporary
Task-scoped
Run-scoped
Mutable
Operational
Non-authoritative
Disposable
Potentially promotable
```

## 4.2 Scratchpad lifetime

```text
Run Starts
    ↓
Scratchpad Created
    ↓
Agents Read / Update
    ↓
Handoffs Occur
    ↓
Run Completes
    ↓
Useful Knowledge Evaluated
    ↓
Promote / Archive / Delete
```

## 4.3 Scratchpad is not project truth

Make the distinction explicit:

> **Presence in the Scratchpad does not make information authoritative.**

An observation may be:

- incomplete;
- incorrect;
- speculative;
- superseded;
- awaiting validation.

---

# 5. The Five-State Model

Make this a major conceptual section.

| Artifact | Primary Question | Lifetime | Authority |
| --- | --- | --- | --- |
| Prompt | What needs to be done? | Task/run | Task input |
| Plan | How will we approach it? | Run | Execution intent |
| Scratchpad | What are we discovering and tracking while doing it? | Run | Temporary working state |
| Repository Intelligence | What evidence do we have about the repository? | Persistent | Governed repository evidence |
| Harness Memory | What governed knowledge should survive across executions? | Cross-run | Governed retained knowledge |

## 5.1 Prompt — What needs to be done

Example:

```text
Add cancellation support for corporate fleet bookings.
```

## 5.2 Plan — How the task will be approached

Example:

```text
1. Locate booking cancellation behavior.
2. Inspect domain invariants.
3. Implement cancellation reason.
4. Add tests.
5. Validate contracts.
```

## 5.3 Scratchpad — What is being discovered while doing it

Example:

```text
Observation:
Booking aggregate already contains CancelledAt.

Question:
Does FleetBookingCancelled need a new event version?

Files under investigation:
Booking.cs
BookingService.cs
FleetBookingController.cs
```

## 5.4 Repository Intelligence — Evidence about the repository

Explain that this represents discovered and governed evidence concerning architecture, code organization, dependencies, conventions, contracts, and repository structure.

Maintain the handbook’s frozen distinction that Repository Intelligence is preferred over treating it as generic “Project Memory.” AI development handbook

## 5.5 Harness Memory — Governed cross-run knowledge

Introduce Harness Memory without pre-empting the later dedicated chapter.

Explain that it retains approved knowledge across executions and must never be populated simply because an agent wrote something into a Scratchpad.

---

# 6. Scratchpad Versus Related Concepts

## 6.1 Scratchpad versus Prompt

```text
Prompt
"What do we need?"

Scratchpad
"What have we learned while doing it?"
```

## 6.2 Scratchpad versus Plan

```text
Plan
Intended path

Scratchpad
Observed execution reality
```

Explain that execution may invalidate or modify assumptions behind the Plan.

## 6.3 Scratchpad versus Steering Note

Steering Notes contain current mission priorities, constraints, goals, and acceptance information and are normally human-maintained. AI development handbook

Contrast:

```text
Steering Note
    ↓
Guides execution

Scratchpad
    ↓
Records temporary execution state
```

## 6.4 Scratchpad versus Repository Intelligence

Distinguish:

```text
Scratchpad
Unverified / temporary discoveries

Repository Intelligence
Evidence-based repository understanding
```

## 6.5 Scratchpad versus Harness Memory

Distinguish:

```text
Scratchpad
Run N
  ↓
Temporary discoveries
  ↓
Review / Validate / Govern
  ↓
Harness Memory
  ↓
Available to Run N+1
```

---

# 7. Run-Scoped Storage

## 7.1 Definition of a Harness Run

Define the execution boundary.

## 7.2 Run identity

Introduce concepts such as:

```text
RunId
TaskId
StartedAt
Status
WorkspacePath
CurrentPhase
```

## 7.3 Isolation between runs

Explain why:

```text
Run A Scratchpad
        ≠
Run B Scratchpad
```

## 7.4 Preventing accidental cross-run contamination

Discuss stale assumptions and previous-run state leaking into new execution.

---

# 8. What Belongs in the Scratchpad

## 8.1 Agent observations

Examples:

```text
BookingController delegates cancellation to BookingService.

Cancellation currently has no reason field.
```

## 8.2 Assumptions

Require assumptions to be explicitly labeled.

```text
ASSUMPTION:
Existing event consumers tolerate additive fields.
```

## 8.3 Unresolved questions

```text
QUESTION:
Should cancellation reason be mandatory for corporate bookings?
```

## 8.4 Intermediate decisions

Record why an execution path was selected.

## 8.5 Files under investigation

Example:

```text
src/Booking/Domain/Booking.cs
src/Booking/Application/CancelBooking.cs
tests/Booking.Tests/CancelBookingTests.cs
```

## 8.6 Task progress

Example:

```text
[done] Locate domain behavior
[done] Identify event
[in progress] Update command
[pending] Integration tests
```

## 8.7 Checkpoints

Capture enough state to restart safely.

---

# 9. What Must Not Be Stored in the Scratchpad

Cover prohibited or inappropriate content:

- long-lived architecture standards;
- authoritative coding standards;
- permanent business rules;
- approved architectural decisions;
- secrets;
- passwords;
- access tokens;
- private keys;
- unnecessary personally identifiable information;
- unrestricted production data;
- permanent copies of sensitive logs;
- unverified claims presented as facts;
- information that belongs in governed knowledge sources.

Establish:

> **The Scratchpad must not become an undocumented shadow knowledge base.**

---

# 10. Run Workspace Architecture

Use the requested reference architecture:

```text
Harness Run
│
├── Input
│   ├── Prompt
│   ├── Instructions
│   ├── Skills
│   └── Steering Notes
│
├── Run Workspace
│   ├── plan.md
│   ├── scratchpad.md
│   ├── agent-handoffs/
│   ├── validation/
│   └── outputs/
│
└── Persistent Knowledge
    ├── Repository Intelligence
    ├── Metrics
    └── Harness Memory
```

Explain each architectural boundary.

---

# 11. Run Workspace Structure

Present a more concrete implementation:

```text
harness/
└── runs/
    └── {run-id}/
        ├── run.json
        ├── input/
        │   ├── prompt.md
        │   └── steering-context.md
        │
        ├── plan.md
        ├── scratchpad.md
        │
        ├── agents/
        │   ├── lead/
        │   ├── developer/
        │   ├── reviewer/
        │   ├── validator/
        │   └── evaluator/
        │
        ├── agent-handoffs/
        ├── validation/
        ├── outputs/
        └── checkpoints/
```

Discuss logical structure versus physical implementation.

---

# 12. Diagram — Scratchpad Versus Persistent Knowledge

Required diagram:

```text
              Harness Run
                   │
          ┌────────┴────────┐
          │                 │
     Temporary          Persistent
       State             Knowledge
          │                 │
    ┌─────┴─────┐      ┌────┴───────────┐
    │           │      │                │
   Plan     Scratchpad Repository     Harness
                      Intelligence    Memory
```

Explain authority, lifecycle, mutability, and governance boundaries.

---

# 13. Agent Usage of the Scratchpad

## 13.1 Lead Agent

Uses the Scratchpad to track:

- decomposition discoveries;
- dependencies;
- unresolved questions;
- task sequencing;
- delegation state;
- risk observations.

## 13.2 Developer Agent

Tracks:

- files examined;
- implementation observations;
- assumptions;
- changes in progress;
- discovered dependencies;
- test observations.

## 13.3 Reviewer Agent

Tracks:

- review concerns;
- areas requiring inspection;
- questions for Developer;
- potential architecture deviations.

## 13.4 Validator Agent

Tracks execution context while authoritative validation evidence belongs under `validation/`.

Emphasize:

```text
Scratchpad observation:
"Integration test appears to fail because SQL is unavailable."

Validation evidence:
Actual command, result, exit code, logs.
```

## 13.5 Evaluator Agent

Tracks:

- evaluation observations;
- acceptance-criteria questions;
- business-outcome concerns;
- evidence still required.

---

# 14. Agent Handoffs

## 14.1 Why handoffs require explicit state

Avoid dependence on implicit conversational context.

## 14.2 Handoff contract

Suggested fields:

```text
From
To
Task
Completed
Remaining
Key observations
Open questions
Files changed
Evidence
Risks
Next action
```

## 14.3 Handoff artifacts

Example:

```text
agent-handoffs/
├── lead-to-developer.md
├── developer-to-reviewer.md
├── reviewer-to-developer.md
├── developer-to-validator.md
└── validator-to-evaluator.md
```

---

# 15. Diagram — Agent Handoff Flow

```text
Lead
 │
 │ plan + context
 ▼
Developer
 │
 │ implementation + observations
 ▼
Reviewer
 │
 │ findings
 ▼
Developer
 │
 │ corrections
 ▼
Validator
 │
 │ deterministic evidence
 ▼
Evaluator
 │
 │ outcome assessment
 ▼
Harness
```

Explain what belongs in handoff state versus shared Scratchpad state.

---

# 16. Sharing State Between Agents

Discuss three approaches:

### Shared Scratchpad

All agents access one controlled run-level Scratchpad.

### Agent-specific Scratchpads

Each role maintains its own working state.

### Hybrid model

```text
Run Scratchpad
      │
      ├── Lead
      ├── Developer
      ├── Reviewer
      ├── Validator
      └── Evaluator
```

Discuss visibility, ownership, conflict handling, and auditability.

---

# 17. Parallel-Agent Scratchpads

Explain why concurrent agents should not freely mutate one shared file.

Example:

```text
scratchpads/
├── developer-api.md
├── developer-domain.md
├── developer-tests.md
└── reviewer-security.md
```

Cover:

- isolation;
- ownership;
- synchronization;
- conflict avoidance;
- merge points;
- aggregation;
- shared findings.

---

# 18. Diagram — Parallel-Agent Scratchpads

```text
                  Lead Agent
                      │
             ┌────────┼────────┐
             ▼        ▼        ▼
         Agent A   Agent B   Agent C
             │        │        │
             ▼        ▼        ▼
          Pad A     Pad B     Pad C
             │        │        │
             └────────┼────────┘
                      ▼
               Handoff / Merge
                      │
                      ▼
              Shared Run State
```

Explain why parallel working state should be merged deliberately rather than implicitly.

---

# 19. Task Progress and Checkpoints

## 19.1 Execution states

Example:

```text
Pending
Running
Blocked
Completed
Failed
```

## 19.2 Checkpoint contents

Capture:

- current phase;
- completed tasks;
- pending tasks;
- modified files;
- relevant observations;
- last successful gate;
- unresolved issues.

## 19.3 Checkpoint boundaries

Potential checkpoints after:

- planning;
- implementation;
- review;
- validation;
- evaluation.

---

# 20. Failure Recovery

Introduce failure scenarios:

- agent timeout;
- Harness crash;
- process termination;
- infrastructure outage;
- validation failure;
- tool failure;
- context exhaustion.

Explain why restarting from the original Prompt alone may be unsafe or wasteful.

---

# 21. Resumability

Define resumability as:

> The ability to safely continue an interrupted Harness run from a known checkpoint without treating incomplete temporary state as authoritative truth.

Cover:

- identifying the last valid checkpoint;
- restoring run context;
- verifying repository state;
- detecting external changes;
- invalidating stale observations;
- re-running deterministic gates;
- resuming from the appropriate phase.

---

# 22. Diagram — Failure Recovery and Resume Flow

```text
Run
 │
 ▼
Checkpoint
 │
 ▼
Execution
 │
 ├──────── Success ────────► Continue
 │
 └──────── Failure
              │
              ▼
        Load Checkpoint
              │
              ▼
       Verify Current State
              │
        ┌─────┴─────┐
        │           │
      Valid       Stale
        │           │
        ▼           ▼
      Resume     Re-evaluate
```

---

# 23. Promotion into Governed Knowledge

Make this another major architectural section.

A useful Scratchpad discovery should not automatically become Harness Memory.

Use:

```text
Scratchpad Discovery
        ↓
Evidence Collection
        ↓
Verification
        ↓
Review
        ↓
Governance Decision
        ↓
Approved?
   ┌────┴────┐
   │         │
  No        Yes
   │         │
Discard/   Promote
Archive      │
             ▼
     Repository Intelligence
          or
       Harness Memory
```

Relate this to the existing self-learning Harness principle that the Harness recommends improvements rather than silently changing standards; human approval remains part of governed change. AI development handbook

---

# 24. Diagram — Temporary Discovery to Governed Knowledge

Show examples of promotion candidates:

```text
Scratchpad
   │
   ├── Repeated architecture discovery
   │           ↓
   │   Repository Intelligence
   │
   ├── Reusable implementation lesson
   │           ↓
   │      Harness Memory
   │
   ├── New recurring workflow
   │           ↓
   │    Proposed Skill Change
   │
   └── New engineering standard
               ↓
       Proposed Instruction Change
               ↓
          Human Approval
```

Emphasize that promotion targets depend on the type of knowledge.

---

# 25. Cleanup and Retention

Cover lifecycle states:

```text
Active
  ↓
Completed
  ↓
Retention Decision
  ├── Delete
  ├── Archive
  └── Retain selected evidence
```

Discuss:

- temporary workspace cleanup;
- retention windows;
- compliance requirements;
- debugging requirements;
- storage costs;
- separation of operational evidence from temporary reasoning state.

---

# 26. Auditability

Discuss what enterprises may need to reconstruct:

- what task was requested;
- what agents participated;
- what changes were made;
- what gates ran;
- what evidence was produced;
- what handoffs occurred;
- what knowledge was promoted.

Make an important distinction:

> **Auditability does not require permanently retaining every piece of temporary agent working state.**

Discuss structured audit events versus unrestricted Scratchpad retention.

---

# 27. Security and Sensitive-Data Considerations

Cover:

- least-privilege workspace access;
- workspace isolation;
- secrets redaction;
- credential handling;
- PII minimization;
- production-data controls;
- encryption;
- log sanitization;
- retention policies;
- access auditing;
- secure deletion.

Discuss risks from copying tool output indiscriminately into `scratchpad.md`.

---

# 28. Scratchpad Inside the Harness

Place the concept into the broader architecture.

```text
Harness
│
├── Run Manager
│
├── Agent Orchestrator
│
├── Workspace Manager
│   ├── Plan
│   ├── Scratchpads
│   ├── Handoffs
│   ├── Checkpoints
│   └── Outputs
│
├── Gate Pipeline
│
├── Evidence Store
│
├── Metrics
│
└── Knowledge Promotion
```

Explain Harness responsibilities:

- create workspace;
- assign Run ID;
- isolate runs;
- control agent access;
- persist checkpoints;
- coordinate handoffs;
- recover interrupted runs;
- clean temporary state;
- extract metrics;
- initiate governed promotion.

---

# 29. Architecture Discussion

Discuss design decisions around:

- filesystem versus database storage;
- structured versus Markdown Scratchpads;
- append-only versus mutable state;
- per-agent versus shared state;
- local versus remote workspace;
- containerized execution;
- distributed Harness execution;
- concurrent access;
- optimistic locking;
- workspace versioning;
- artifact size limits;
- retention policies;
- checkpoint durability.

Include a recommendation that the **conceptual model should remain independent of the storage implementation**.

---

# 30. Hands-on Example — Alpha Car Detailing

Use the corporate fleet cancellation feature.

Walk through creation of:

```text
runs/
└── RUN-2026-00142/
    ├── plan.md
    ├── scratchpad.md
    ├── agent-handoffs/
    ├── validation/
    ├── checkpoints/
    └── outputs/
```

Show progression from Prompt through execution.

Include sample Scratchpad entries such as:

```text
## Observations

- Booking aggregate already supports CancelledAt.
- Cancellation reason is not currently persisted.

## Assumptions

- Existing event consumers tolerate additive fields.
  Status: UNVERIFIED

## Questions

- Is cancellation reason mandatory for all fleet customers?

## Files Under Investigation

- Booking.cs
- CancelBookingCommand.cs
- BookingRepository.cs
```

Then show how assumptions are verified, rejected, or promoted.

---

# 31. Claude Example

Demonstrate how Claude can participate in a Harness-managed Run Workspace while respecting:

- Instructions;
- Skills;
- Prompt;
- Plan;
- Steering Notes;
- Scratchpad boundaries;
- agent handoffs;
- deterministic gates;
- governed knowledge.

Keep the engineering model vendor-neutral even though Claude is the handbook’s primary implementation platform. This follows the established Claude-first but vendor-neutral approach. AI development handbook

---

# 32. GitHub Copilot Comparison

Compare how the same architecture can be represented when using GitHub Copilot.

Focus only on materially different execution and context-management behavior.

Keep:

```text
Prompt
Plan
Scratchpad
Run Workspace
Persistent Knowledge
```

as architecture concepts independent of product capabilities.

---

# 33. Codex Comparison

Demonstrate the same principle for Codex.

Emphasize:

> The Scratchpad and Run Workspace belong to the Harness architecture, not to a particular AI coding platform.

Compare run isolation, agent execution, context passing, workspace access, and evidence capture where relevant.

---

# 34. Best Practices

Cover practices such as:

- keep Scratchpads run-scoped;
- label assumptions explicitly;
- separate observations from facts;
- timestamp important checkpoints;
- identify agent ownership;
- maintain explicit handoffs;
- isolate parallel-agent state;
- store deterministic evidence separately;
- promote knowledge deliberately;
- enforce retention policies;
- sanitize sensitive data;
- design for resumability;
- treat the Scratchpad as disposable;
- keep authoritative knowledge outside the Scratchpad.

---

# 35. Anti-patterns

Cover:

### Scratchpad as permanent documentation

Temporary state gradually becomes project documentation.

### Scratchpad as Harness Memory

Everything discovered gets reused automatically.

### Scratchpad as Repository Intelligence

Unverified observations become repository facts.

### Prompt mutation

Agents continually rewrite the original task based on temporary discoveries.

### Plan-as-Scratchpad

Execution observations are mixed into the planned execution sequence.

### Secret dumping

Credentials and sensitive tool outputs are copied into working state.

### One global Scratchpad

Every Harness run and agent writes into the same workspace.

### Parallel write collision

Multiple agents mutate the same file without ownership rules.

### Infinite retention

All run state is stored forever.

### Automatic knowledge promotion

Agent observations silently become standards or persistent memory.

---

# 36. Architect’s Notes

Highlight architectural principles:

> **Temporary state needs an explicit architectural home.**

> **Persistence does not imply authority.**

> **Resumability requires checkpoints, not merely conversation history.**

> **Agent coordination should depend on explicit artifacts rather than hidden conversational context.**

> **Knowledge promotion is a governance operation, not a file-copy operation.**

---

# 37. Enterprise Tips

Cover:

- assign every run a unique identity;
- separate working state from evidence;
- classify workspace data;
- make handoffs machine-readable where useful;
- record provenance for promoted discoveries;
- establish organization-wide retention defaults;
- make recovery behavior deterministic;
- prevent one run from consuming another run’s temporary state;
- measure workspace growth and cleanup;
- integrate workspace lifecycle with Harness observability.

---

# 38. Decision Points

Include architectural decision scenarios.

### Decision: Shared or agent-specific Scratchpads?

Evaluate coordination versus isolation.

### Decision: Markdown or structured storage?

Evaluate readability versus automation.

### Decision: What survives run completion?

Evaluate debugging, compliance, security, and cost.

### Decision: When can temporary knowledge be promoted?

Define evidence and approval requirements.

### Decision: What constitutes a resumable checkpoint?

Define minimum durable execution state.

### Decision: Where should validation evidence live?

Clarify that deterministic evidence should not depend solely on Scratchpad contents.

---

# 39. Exercises

Include exercises such as:

1. Design a Run Workspace for an Alpha Car Detailing feature.
2. Classify 20 sample pieces of information as Prompt, Plan, Scratchpad, Repository Intelligence, or Harness Memory.
3. Design `scratchpad.md`.
4. Define an agent-handoff schema.
5. Design parallel Developer and Reviewer Scratchpads.
6. Create a checkpoint format.
7. Design failure recovery after Developer Agent interruption.
8. Define a workspace retention policy.
9. Identify security violations in a deliberately unsafe Scratchpad.
10. Design a knowledge-promotion workflow.
11. Decide which Alpha Car Detailing discoveries should be promoted into Repository Intelligence.
12. Extend the existing Harness architecture with a Workspace Manager.

---

# 40. Interview Questions

Include questions covering both concepts and architecture:

- What problem does a Scratchpad solve in an AI Engineering Harness?
- How does a Scratchpad differ from a Prompt?
- How does it differ from a Plan?
- Why should a Scratchpad not be treated as Repository Intelligence?
- What distinguishes Scratchpad state from Harness Memory?
- Why should assumptions be explicitly labeled?
- How should multiple agents share temporary execution state?
- Why are parallel-agent Scratchpads useful?
- What information should be included in an agent handoff?
- How would you make a Harness run resumable?
- What constitutes a valid checkpoint?
- How should Scratchpad information be promoted into persistent knowledge?
- Why is automatic promotion dangerous?
- How would you secure a Run Workspace?
- What should happen to a Scratchpad after successful execution?
- How can auditability be preserved without retaining all temporary reasoning indefinitely?

---

# 41. Chapter Summary

Reinforce the chapter’s conceptual model:

```text
Prompt
   ↓
What needs to be done

Plan
   ↓
How we intend to do it

Scratchpad
   ↓
What we discover and track while doing it

Repository Intelligence
   ↓
Governed evidence about the repository

Harness Memory
   ↓
Governed knowledge retained across executions
```

Conclude with the central principle:

> **The Scratchpad is temporary, task-scoped working state. It helps agents reason and coordinate during execution, but it is not an authoritative source of project knowledge.**

Reinforce that the **Run Workspace is the execution boundary that gives temporary state, plans, handoffs, validation evidence, checkpoints, and outputs an explicit architectural home.**

---

# 42. Further Reading

Plan references and cross-references to:

- Repository Discovery
- Repository Intelligence and Knowledge Sources
- Prompts
- Roles
- Steering Notes
- What Is a Harness?
- Harness Architecture
- Lead Agent
- Developer Agent
- Reviewer Agent
- Validator Agent
- Evaluator Agent
- Deterministic Gates
- Hooks and Automated Guardrails
- Metrics
- Harness Memory
- AI Governance
- Security
- Audit Trails

The section should also cross-reference the later **Harness Memory** material because this chapter deliberately establishes the boundary between ephemeral run state and governed cross-run learning. The handbook already defines Harness Memory as part of its self-learning architecture and explicitly requires that the Harness never silently change standards. AI development handbook

**Scratchpad and Run Workspace outline is complete.**
