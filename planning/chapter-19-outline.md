# Chapter 19 — Population  

**Part III — Harness Engineering**  
**Target file:** `book/19-population.md`

The outline follows the frozen chapter template and the established handbook architecture. Population is already identified as a first-class Harness component, alongside Scripts, Roles, Search, Metrics, and Knowledge, and Chapter 19 is positioned immediately before Chapter 20 — Metrics. AI development handbook AI development handbook

## 1. Story-Driven Opening — When One Task Becomes Hundreds

### 1.1 The Alpha Car Detailing Change Wave

- Introduce a release containing multiple fleet-booking changes.
- Changes span Booking, Fleet, Customer, Notification, Pricing, and Reporting services.
- Some changes are independent.
- Some depend on earlier changes.
- Some can execute concurrently.
- Some fail and require another attempt.
- One execution becomes stale after losing its worker.
- Another repeatedly fails and reaches its retry limit.
- Several complete successfully and produce evidence and pull requests.

### 1.2 The Coordination Problem

- An AI Agent can execute a task, but the Harness must know:
  - what work exists,
  - what has not started,
  - what is currently running,
  - what has completed,
  - what failed,
  - what can be retried,
  - what has become stale,
  - and what requires human attention.
- Explain why prompts alone cannot provide durable work management.

### 1.3 Introducing Population

- Population as the Harness's managed collection of execution units.
- Population converts incoming engineering demand into durable, trackable work.
- Establish the chapter's core principle:

> **Population defines what work enters the Harness, how that work is represented, prioritized, tracked, assigned, retried, and completed.**

---

# 2. Learning Objectives

After completing this chapter, readers should be able to:

- Define Population within an enterprise AI Harness.
- Explain Population versus Prompt and workflow state.
- Design a durable Population item.
- Define work identity and metadata.
- Design Population lifecycle states.
- Model dependencies and priorities.
- Support batching and parallel execution.
- Implement safe assignment and claiming.
- Design retry and exhaustion policies.
- Detect stale, stuck, and orphaned work.
- Prevent duplicate execution.
- Apply idempotency and checkpointing.
- Support re-entry and controlled reprocessing.
- Persist Population state and audit history.
- Query Population operationally.
- Connect Population with Lead, Developer, Reviewer, Validator, and Evaluator roles.
- Integrate Population with Metrics.
- Define human intervention points.
- Compare Claude Code, GitHub Copilot, and Codex implementation approaches.

---

# 3. Background — From Prompts to Managed Work

### 3.1 The Single-Prompt Model

- User submits request.
- AI Agent processes request.
- Result is returned.
- Why this works for interactive development but is insufficient for an enterprise Harness.

### 3.2 The Multi-Work-Item Reality

- Hundreds of programs, tickets, services, migrations, tests, or conversion units.
- Work arriving at different times.
- Parallel workers.
- Dependencies.
- Partial failures.
- Restarts.
- Human approvals.
- Long-running execution.

### 3.3 Why the Harness Needs a Population

- Durable work inventory.
- Explicit execution boundaries.
- Operational visibility.
- Repeatability.
- Recoverability.
- Scalability.
- Auditability.

### 3.4 Population as a First-Class Harness Capability

Place Population within the previously established Harness architecture. The project architecture already identifies `Population/` as a Harness component. AI development handbook

---

# 4. Population Concepts

## 4.1 What Population Means in a Harness

- Logical collection of work eligible for Harness processing.
- Population as more than a queue.
- Population as the authoritative operational representation of work.

## 4.2 Population Versus Prompt

| Population | Prompt |
| --- | --- |
| Defines work that exists | Defines an instruction/request |
| Durable | Often execution-specific |
| Tracks lifecycle | Describes desired action |
| Contains operational metadata | Contains task context |
| Can survive process restarts | May exist only during execution |
| Can be retried/reassigned | Can be reused during retries |

Reinforce the established terminology that a Prompt represents today's task while the Harness runs the engineering process. AI development handbook AI development handbook

## 4.3 Population Versus Workflow State

- Population = collection of work items.
- Workflow state = where an individual item currently is in its execution process.
- Avoid treating the two concepts as interchangeable.

## 4.4 Population Item

Define the atomic execution unit.

Possible Alpha Car Detailing examples:

- Add fleet-booking validation.
- Add Booking API endpoint.
- Add cancellation event.
- Update Pricing Service consumer.
- Generate integration tests.
- Migrate booking database schema.

## 4.5 Work Item Granularity

- Too large.
- Too small.
- Independently executable.
- Independently verifiable.
- Independently retryable.
- Traceable to the originating requirement.

---

# 5. Work Item Identity

## 5.1 Stable Identity

- `PopulationItemId`
- Business/work identifier.
- Correlation identifier.
- Execution/attempt identifier.

## 5.2 Identity Versus Execution Attempt

Distinguish:

```text
Population Item
    │
    ├── Attempt 1
    ├── Attempt 2
    └── Attempt 3
```

The work identity should remain stable even when execution attempts change.

## 5.3 Correlation

- Ticket.
- Goal.
- Repository.
- Branch.
- Pull request.
- Parent work item.
- Batch.
- Harness run.

---

# 6. Inputs and Metadata

### 6.1 Core Work Metadata

Possible fields:

```text
PopulationItemId
WorkType
Goal
Source
Repository
Service
Priority
Status
CreatedAt
UpdatedAt
AssignedAgent
AttemptCount
MaxAttempts
BatchId
ParentItemId
CorrelationId
Checkpoint
LastHeartbeat
CompletedAt
```

### 6.2 Engineering Context

- Instructions.
- Skills.
- Steering Notes.
- Knowledge Sources.
- Acceptance criteria.
- Repository Intelligence.
- Relevant paths.
- Required roles.

### 6.3 Operational Metadata

- Created time.
- Started time.
- heartbeat.
- attempt number.
- execution duration.
- failure category.
- completion evidence.

---

# 7. Population Status and Lifecycle

## 7.1 Recommended Lifecycle

Example:

```text
Created
   ↓
Ready
   ↓
Assigned
   ↓
Running
   ↓
Completed
```

Alternative outcomes:

```text
Running → Failed → RetryPending → Ready
Running → Stale → Recovery
Failed → Exhausted
Running → Blocked
Running → Cancelled
```

## 7.2 State Definitions

Define semantics for:

- Created
- Ready
- Assigned
- Running
- Blocked
- RetryPending
- Failed
- Stale
- Exhausted
- Completed
- Cancelled

## 7.3 Terminal Versus Non-Terminal States

## 7.4 Valid State Transitions

## 7.5 Preventing Invalid Transitions

### Diagram — Population Lifecycle

```text
Created
   │
   ▼
 Ready ───────────────► Cancelled
   │
   ▼
Assigned
   │
   ▼
Running
   │
   ├────────► Completed
   │
   ├────────► Blocked
   │
   ├────────► Stale
   │
   └────────► Failed
                  │
             Retry allowed?
              /        \
            Yes         No
             │           │
             ▼           ▼
       RetryPending   Exhausted
             │
             └────────► Ready
```

---

# 8. Work Item State Model

### 8.1 State as Durable Data

- Never rely only on an in-memory worker state.

### 8.2 Transition Ownership

- Harness owns operational state transitions.
- Agents provide execution results.
- Agents should not arbitrarily rewrite lifecycle state.

### 8.3 State Transition Evidence

- Actor.
- timestamp.
- previous state.
- new state.
- reason.
- attempt.
- evidence.

### Diagram — Work Item State Model

Show state transitions together with transition guards and audit records.

---

# 9. Priority

### 9.1 Why Priority Exists

- Critical production issue versus normal feature work.
- Release-blocking changes.
- Dependency-unblocking work.

### 9.2 Priority Model

Example:

```text
Critical
High
Normal
Low
```

### 9.3 Priority Is Not Execution Order

- Dependencies may override simple priority ordering.
- Capacity and concurrency constraints matter.

### 9.4 Priority Aging

- Prevent permanent starvation.

---

# 10. Dependencies

### 10.1 Work Item Dependencies

Example:

```text
Database Migration
       ↓
Domain Changes
       ↓
API Changes
       ↓
Integration Tests
```

### 10.2 Parent/Child Work

### 10.3 Blocking Dependencies

### 10.4 Dependency Graphs

### 10.5 Circular Dependency Detection

### 10.6 Dependency Failure Propagation

---

# 11. Assignment and Claiming

### 11.1 Assigning Work

- Lead Agent.
- Harness scheduler.
- Agent pool.
- Human assignment.

### 11.2 Claiming Work Safely

```text
Ready
  ↓
Claim
  ↓
Assigned
  ↓
Running
```

### 11.3 Preventing Double Claims

### 11.4 Lease-Based Ownership

### 11.5 Heartbeats

### 11.6 Assignment Expiration

---

# 12. Batching

### 12.1 Why Batch Population Items

- Related service changes.
- Program conversion sets.
- Release scope.
- migration groups.

### 12.2 Batch Identity

### 12.3 Batch Progress

### 12.4 Partial Batch Failure

### 12.5 Batch Completion Rules

Alpha Car Detailing example:

```text
Fleet Booking Release Batch
├── Booking API
├── Fleet validation
├── Pricing integration
├── Notification consumer
└── Reporting update
```

---

# 13. Concurrency

### 13.1 Parallel Work

### 13.2 Concurrency Limits

### 13.3 Per-Service Limits

### 13.4 Repository Collision Risks

### 13.5 Branch and Workspace Isolation

### 13.6 Dependency-Aware Parallelism

Example:

```text
              ┌─► Pricing Service
Fleet Change ─┼─► Notification Service
              └─► Reporting Service
```

---

# 14. Scheduling

### 14.1 Immediate Work

### 14.2 Scheduled Work

### 14.3 Delayed Retry

### 14.4 Dependency-Triggered Scheduling

### 14.5 Capacity-Aware Scheduling

### 14.6 Human-Approval-Triggered Scheduling

---

# 15. Retry

### 15.1 Why Retry Is Necessary

### 15.2 Retryable Versus Non-Retryable Failure

### 15.3 Retry Policy

- Maximum attempts.
- delay.
- exponential backoff.
- failure classification.

### 15.4 Retry Context

- Preserve previous evidence.
- Do not pretend previous attempts never occurred.

### 15.5 Agent Retry Versus Work Item Retry

Important distinction between retrying a tool/API operation inside an Agent execution and retrying the entire Population item.

---

# 16. Exhaustion

### 16.1 Definition

An item becomes exhausted when the configured execution policy determines that automatic retries should stop.

### 16.2 Exhaustion Criteria

- Maximum attempts reached.
- repeated deterministic failure.
- unrecoverable failure.
- policy violation.

### 16.3 Exhaustion Evidence

### 16.4 Escalation

- Human review.
- Lead Agent review.
- troubleshooting artifacts.
- failure logs.

### Diagram — Retry and Exhaustion Flow

```text
Failure
   │
   ▼
Classify
   │
   ├── Non-Retryable ─────► Exhausted
   │
   ▼
Retryable
   │
Attempts remaining?
   │
 ┌─┴─┐
Yes  No
 │    │
 ▼    ▼
Delay Exhausted
 │
 ▼
Ready
```

---

# 17. Stale, Stuck, and Orphaned Work

### 17.1 Stale Work

- Last heartbeat exceeds threshold.

### 17.2 Stuck Work

- Execution remains active but no meaningful progress occurs.

### 17.3 Orphaned Work

- Worker disappears while ownership remains recorded.

### 17.4 Detection Strategies

- Heartbeat.
- lease expiration.
- execution timeout.
- progress timestamp.

### 17.5 Recovery

- Release lease.
- classify previous attempt.
- return to Ready or RetryPending.
- preserve evidence.

---

# 18. Duplicate Detection

### 18.1 Sources of Duplication

- Duplicate ticket ingestion.
- repeated webhook.
- manual resubmission.
- Harness restart.
- race condition.

### 18.2 Duplicate Identity

### 18.3 Semantic Versus Exact Duplicates

### 18.4 Duplicate Handling Policies

---

# 19. Idempotency

### 19.1 Why Duplicate Detection Is Not Enough

### 19.2 Idempotent Population Creation

### 19.3 Idempotent Assignment

### 19.4 Idempotent Completion

### 19.5 Idempotent Evidence Registration

### 19.6 Idempotency Keys

---

# 20. Checkpointing

### 20.1 Purpose

- Avoid restarting expensive work from zero.

### 20.2 Checkpoint Types

- workflow stage.
- repository state.
- validation state.
- generated artifacts.
- Agent handoff.

### 20.3 Durable Checkpoints

### 20.4 Checkpoint Validity

### 20.5 Resume Versus Restart

---

# 21. Progress Tracking

### 21.1 Item-Level Progress

### 21.2 Batch-Level Progress

### 21.3 Population-Level Progress

### 21.4 Stage Progress

Example:

```text
Population: 120 items

Completed     82
Running       12
Ready         15
RetryPending   4
Blocked        3
Exhausted      4
```

### 21.5 Avoiding Misleading Percentage Completion

---

# 22. Failure Handling

### 22.1 Failure Categories

- Agent failure.
- infrastructure failure.
- validation failure.
- repository conflict.
- dependency failure.
- policy failure.
- human rejection.

### 22.2 Failure Evidence

### 22.3 Failure Ownership

### 22.4 Recoverable Versus Terminal Failure

---

# 23. Re-entry and Reprocessing

### 23.1 Re-entry

Returning previously processed work to the Harness after a controlled event.

### 23.2 Reprocessing

Executing a completed, failed, or exhausted item again under explicit policy.

### 23.3 Reasons

- requirements changed.
- dependency fixed.
- Harness defect corrected.
- Prompt/Skill improved.
- validation rule changed.

### 23.4 Preserve History

Never erase the original execution.

### 23.5 New Attempt Versus New Population Item

---

# 24. Population Persistence

### 24.1 Why Population Must Be Durable

### 24.2 Persistence Model

Potential entities:

```text
Population
PopulationItem
PopulationAttempt
PopulationDependency
PopulationCheckpoint
PopulationTransition
PopulationEvidence
PopulationFailure
```

### 24.3 SQL-Based Implementation

### 24.4 Transaction Boundaries

### 24.5 Optimistic Concurrency

### 24.6 Recovery After Harness Restart

---

# 25. Audit History

### 25.1 Immutable Transition History

### 25.2 Who Changed What

### 25.3 Agent Actions

### 25.4 Human Actions

### 25.5 Retry History

### 25.6 Evidence History

### 25.7 Pull Request Correlation

---

# 26. Population Queries

### 26.1 Operational Queries

Examples:

```text
Ready work
Running work
Failed work
Stale work
Exhausted work
Blocked work
Items by service
Items by batch
Items by agent
Items by priority
```

### 26.2 Diagnostic Queries

### 26.3 Historical Queries

### 26.4 Dashboard Queries

### 26.5 Query Performance at Scale

---

# 27. Architecture Discussion — Population Inside the Harness

Position Population within the established principle that the Harness coordinates execution, validation, evaluation, commits, PR creation, metrics, and iteration history. AI development handbook

### Diagram — Population Inside the Harness

```text
                Incoming Goals / Work
                         │
                         ▼
                   ┌────────────┐
                   │ Population │
                   └─────┬──────┘
                         │
                         ▼
                    Lead Agent
                         │
                         ▼
                  Developer Agent
                         │
                         ▼
                   Reviewer Agent
                         │
                         ▼
                  Validator Agent
                         │
                         ▼
                  Evaluator Agent
                         │
                 ┌───────┴────────┐
                 │                │
              Complete          Retry
                 │                │
                 ▼                ▼
             Evidence         Population
                 │
                 ▼
             Pull Request
```

---

# 28. Population Processing Flow

### 28.1 Intake

### 28.2 Normalize

### 28.3 Deduplicate

### 28.4 Prioritize

### 28.5 Resolve Dependencies

### 28.6 Schedule

### 28.7 Claim

### 28.8 Execute

### 28.9 Review and Validate

### 28.10 Evaluate

### 28.11 Complete, Retry, Block, or Exhaust

### Diagram — Population Processing Flow

```text
Input
  ↓
Normalize
  ↓
Deduplicate
  ↓
Population
  ↓
Prioritize
  ↓
Dependency Check
  ↓
Schedule
  ↓
Claim
  ↓
Agent Workflow
  ↓
Evidence
  ↓
Outcome
```

---

# 29. Lead Agent Interaction

### 29.1 Lead Reads Eligible Population

### 29.2 Work Decomposition

- One incoming requirement may become several Population items.

### 29.3 Dependency Identification

### 29.4 Role Selection

### 29.5 Execution Planning

### 29.6 Lead Agent Boundaries

- Lead coordinates.
- Population remains Harness-controlled operational state.
- Lead cannot silently discard failed or inconvenient work.

---

# 30. Developer, Reviewer, Validator, and Evaluator Interaction

### 30.1 Developer

- Receives assigned execution unit.
- Implements within defined scope.
- produces implementation evidence.

### 30.2 Reviewer

- Reviews implementation independently.

### 30.3 Validator

- Produces deterministic validation evidence.

### 30.4 Evaluator

- Determines outcome quality and goal satisfaction.

### 30.5 Returning Outcomes to Population

```text
Population Item
      ↓
Developer
      ↓
Reviewer
      ↓
Validator
      ↓
Evaluator
      ↓
Population Outcome
```

This maintains the established separation of Lead, Developer, Reviewer, Validator, and Evaluator roles in the Harness architecture. AI development handbook

---

# 31. Population and Metrics Integration

Chapter 19 should establish the data boundary for Chapter 20 rather than fully teaching Metrics.

### 31.1 Population as an Operational Metrics Source

### 31.2 Useful Measurements

- total items.
- ready.
- running.
- completed.
- failed.
- exhausted.
- stale.
- throughput.
- retry rate.
- completion duration.
- queue/wait duration.
- attempts per item.

### 31.3 Metrics Must Not Own Population State

- Population produces operational facts.
- Metrics observes and aggregates them.

### Diagram — Population-to-Metrics Relationship

```text
Population
    │
    ├── State transitions
    ├── Attempts
    ├── Durations
    ├── Failures
    ├── Retries
    └── Outcomes
          │
          ▼
       Metrics
          │
     ┌────┼─────┐
     ▼    ▼     ▼
 Dashboard KPI Trends
```

### 31.4 Bridge to Chapter 20 — Metrics

---

# 32. Human Intervention

### 32.1 When Humans Enter the Population Lifecycle

- Exhaustion.
- ambiguous failure.
- dependency resolution.
- priority override.
- security concern.
- architectural decision.
- approval requirement.

### 32.2 Human Reassignment

### 32.3 Human Retry

### 32.4 Human Cancellation

### 32.5 Human Re-entry

### 32.6 Auditing Human Actions

---

# 33. Hands-on Example — Alpha Car Detailing Fleet Booking Population

Follow the frozen requirement for a hands-on section. AI development handbook

### 33.1 Scenario

Introduce multiple fleet-booking requirements.

### 33.2 Create Population

### 33.3 Define Work Items

### 33.4 Assign Priorities

### 33.5 Establish Dependencies

### 33.6 Start Parallel Processing

### 33.7 Simulate Failure

### 33.8 Retry Failed Work

### 33.9 Detect Stale Execution

### 33.10 Recover Stale Work

### 33.11 Exhaust Repeated Failure

### 33.12 Escalate to Human

### 33.13 Complete Successful Work

### 33.14 Produce Evidence

### 33.15 Create Pull Requests

### 33.16 Query Final Population State

---

# 34. Claude Code Example

### 34.1 Claude-First Implementation

The handbook's established approach is Claude-first while keeping the underlying engineering principles vendor-neutral. AI development handbook

### 34.2 Repository Structure

Possible structure:

```text
harness/
├── population/
│   ├── models/
│   ├── persistence/
│   ├── scheduling/
│   ├── retry/
│   ├── recovery/
│   └── queries/
├── roles/
├── skills/
├── prompts/
└── metrics/
```

### 34.3 Population Definition

### 34.4 Claude Lead Agent Consuming Population

### 34.5 Work Assignment

### 34.6 Claude Execution

### 34.7 Capturing Evidence

### 34.8 Updating Population State

### 34.9 Retry Execution

### 34.10 Stale Recovery

### 34.11 Exhaustion Handling

---

# 35. GitHub Copilot Comparison

### 35.1 Applying the Same Population Model

### 35.2 Copilot as Execution Assistance Versus Harness Control

### 35.3 Repository Instructions

### 35.4 Population Orchestration Outside the Coding Assistant

### 35.5 Evidence Capture

### 35.6 Where Copilot Integration Differs

### 35.7 What Must Remain Vendor-Neutral

---

# 36. OpenAI Codex Comparison

### 36.1 Codex as a Population Worker

### 36.2 Supplying Work Context

### 36.3 Isolated Execution

### 36.4 Capturing Results

### 36.5 Retry and Re-entry

### 36.6 Population State Remains Harness-Owned

### 36.7 Claude, Copilot, and Codex Comparison

Compare only meaningful implementation differences rather than redefining Population per platform, consistent with the project's vendor-neutral constraints. AI development handbook

---

# 37. Best Practices

Cover practices such as:

- Give every Population item stable identity.
- Separate work identity from execution attempts.
- Persist state durably.
- Define explicit lifecycle transitions.
- Make state changes atomic.
- Preserve attempt history.
- Make claiming concurrency-safe.
- Use heartbeats or leases for long-running work.
- Classify failures before retrying.
- Limit retries.
- Make exhaustion explicit.
- Detect stale work automatically.
- Design for idempotency.
- Preserve checkpoints.
- Track dependencies explicitly.
- Keep Population independent of specific AI vendors.
- Make human intervention auditable.
- Treat evidence as part of completion.
- Design Population with Metrics requirements in mind.

---

# 38. Anti-Patterns

Cover:

### 38.1 Population as a Folder of Prompt Files

### 38.2 Population as an In-Memory Queue

### 38.3 No Stable Work Identity

### 38.4 Status Stored Only in Logs

### 38.5 Agent-Controlled Lifecycle

### 38.6 Unlimited Retry

### 38.7 Retry Without Failure Classification

### 38.8 Resetting Attempt History

### 38.9 Treating Stale as Failed Automatically

### 38.10 No Duplicate Protection

### 38.11 No Idempotency

### 38.12 Ignoring Dependencies

### 38.13 Excessive Concurrency

### 38.14 Permanent Running State

### 38.15 Manual Recovery Without Audit

### 38.16 Completed Without Evidence

### 38.17 Population Coupled to Claude/Copilot/Codex

---

# 39. Architect's Notes

The frozen chapter format requires dedicated Architect's Notes. AI development handbook

Topics:

- Population is an operational control-plane capability.
- Work identity is more important than worker identity.
- Persist intent separately from execution attempts.
- Design recovery before designing concurrency.
- Assume workers will crash.
- Assume messages will be duplicated.
- Assume execution will occasionally be interrupted.
- State transitions should be explicit and observable.
- Exhaustion is a valid engineering outcome, not merely an error.
- Population should remain independent from individual AI platforms.
- Population is a key boundary between orchestration and AI execution.

---

# 40. Enterprise Tips

Use the handbook's established Enterprise Tip callout convention. AI development handbook

Cover:

- Partition large populations.
- Use indexes appropriate to scheduling queries.
- Separate operational and historical queries where scale demands it.
- Avoid polling every item individually.
- Track heartbeat efficiently.
- Design retry policies by failure category.
- Add administrative tooling for recovery.
- Protect manual override operations with authorization.
- Capture complete audit history.
- Correlate Population items with commits, builds, evidence, and PRs.
- Retain exhaustion artifacts for troubleshooting.
- Establish retention and archival policies.

---

# 41. Decision Points

Provide architectural decisions such as:

- Database versus queue as authoritative Population store.
- Push versus pull assignment.
- Explicit assignment versus worker claiming.
- Lease duration.
- heartbeat interval.
- stale threshold.
- maximum retry attempts.
- fixed versus exponential retry delay.
- work-item granularity.
- dependency representation.
- batch semantics.
- checkpoint granularity.
- completion criteria.
- exhaustion policy.
- human escalation rules.
- retention period.
- audit strategy.
- Population-to-Metrics integration.

---

# 42. Exercises

### Exercise 1 — Design a Population Item

Create a Population item schema for Alpha Car Detailing.

### Exercise 2 — Build the Lifecycle

Define legal state transitions.

### Exercise 3 — Model Dependencies

Break a fleet-booking feature into dependent work.

### Exercise 4 — Design Retry

Define retryable and non-retryable failures.

### Exercise 5 — Detect Stale Work

Design heartbeat and stale thresholds.

### Exercise 6 — Handle Exhaustion

Design the escalation workflow.

### Exercise 7 — Prevent Duplicate Execution

Add idempotency and duplicate detection.

### Exercise 8 — Add Checkpointing

Design resume behavior after worker failure.

### Exercise 9 — Parallelize Work

Identify which Alpha Car Detailing changes can safely run concurrently.

### Exercise 10 — Design Queries

Create queries for ready, running, stale, failed, and exhausted work.

### Exercise 11 — Connect Metrics

Identify metrics derivable from Population state.

### Exercise 12 — Failure Simulation

Model a Harness crash halfway through a 100-item Population and explain recovery.

---

# 43. Interview Questions

Include questions covering:

1. What does Population mean in an AI engineering Harness?
2. How is Population different from a Prompt?
3. How is Population different from workflow state?
4. What makes a good Population item?
5. Why must work identity remain stable across retries?
6. How should Population state be persisted?
7. How do you prevent two workers from claiming the same item?
8. What is the difference between failed, stale, stuck, and exhausted work?
9. How should retries be classified?
10. What is exhaustion?
11. Why is unlimited retry dangerous?
12. How does idempotency apply to Population processing?
13. What is checkpointing?
14. How should dependency-aware scheduling work?
15. How do you safely reprocess completed work?
16. How does the Lead Agent interact with Population?
17. Should an AI Agent own Population state?
18. How should Population integrate with Validator and Evaluator Agents?
19. What Population data should feed Metrics?
20. How would you design Population processing for thousands of concurrent engineering tasks?
21. How do you recover Population after a Harness crash?
22. How do you keep Population vendor-neutral?

---

# 44. Chapter Summary

Summarize the central ideas:

- Population is the Harness's durable representation of work.
- Population is not merely a queue.
- Population items require stable identity and metadata.
- Lifecycle state must be explicit and persistent.
- Priority and dependencies control eligibility.
- Batching and concurrency enable scalable execution.
- Safe assignment prevents duplicate work.
- Retry handles recoverable failure.
- Exhaustion stops uncontrolled repeated execution.
- Heartbeats and leases support stale-work recovery.
- Duplicate detection and idempotency protect correctness.
- Checkpoints enable controlled continuation.
- Re-entry and reprocessing preserve history rather than rewriting it.
- Population provides the operational facts consumed by Metrics.
- Agents perform engineering work; the Harness controls Population lifecycle.
- Human intervention remains necessary for defined escalation and approval scenarios.

End with a transition:

> Population tells the Harness **what work exists and what is happening to it**. The next challenge is understanding **how effectively that work is moving through the system**. Chapter 20 introduces **Metrics**, the observability and measurement capability of the enterprise Harness.

---

# 45. Further Reading

Organize references around:

- Durable work queues.
- Workflow engines.
- Distributed job processing.
- Idempotent consumers.
- Retry and backoff strategies.
- Lease-based distributed processing.
- Distributed locks and concurrency.
- State machines.
- Checkpointing.
- Event-driven processing.
- Workflow orchestration.
- Audit trails.
- Distributed systems failure recovery.
- Operational metrics and observability.

Keep vendor documentation supplemental; the chapter's Population model should remain an **AI Engineering architecture concept rather than a product-specific implementation**, consistent with the handbook's overall philosophy. AI development handbook

**Chapter 19 outline is complete.**
