# Chapter 15 — Developer Agent

**Part III — Harness Engineering**
**Target file:** `book/15-developer-agent.md`

This outline follows the handbook’s frozen terminology, enterprise focus, Claude-first/vendor-neutral approach, Alpha Car Detailing running example, Harness philosophy, and standard chapter template.

---

## 1. Story-Driven Opening

### 1.1 From Plan to Implementation

* Open with the Alpha Car Detailing corporate fleet booking initiative.
* The Lead Agent has already:

  * interpreted the Goal
  * gathered Repository Intelligence
  * identified applicable Instructions
  * selected Skills
  * decomposed the work
  * established acceptance criteria
  * assigned implementation work
* Introduce the Developer Agent as the role that turns the approved plan into repository changes.

### 1.2 The Implementation Responsibility

* Explain why implementation requires a dedicated responsibility boundary.
* Contrast:

  * planning
  * implementation
  * review
  * deterministic validation
  * evaluation
  * approval
* Establish that implementation authority does not imply approval authority.

### 1.3 Running Scenario

Corporate fleet booking requires:

* a new REST API endpoint
* application-layer behavior
* domain changes
* persistence changes
* an integration event
* transactional consistency
* tests
* logging and observability
* security enforcement
* configuration changes

### 1.4 Core Principle

> The Developer Agent implements assigned work but does not approve its own output or replace deterministic validation, review, evaluation, or human approval.

---

## 2. Learning Objectives

By the end of the chapter, readers should be able to:

* Define the Developer Agent role.
* Explain its responsibilities and authority boundaries.
* Describe how implementation work is received from the Lead Agent.
* Use Repository Intelligence before modifying code.
* Apply Instructions and Skills during implementation.
* Distinguish task planning from Harness-level orchestration.
* Make controlled code and configuration changes.
* Implement API, persistence, event, security, and observability requirements.
* Add and update tests.
* Run appropriate local checks.
* Produce implementation evidence.
* Work correctly with Deterministic Validation Gates.
* Handle failed checks and correction cycles.
* Prepare a structured handoff to Reviewer and Validator roles.
* Identify situations requiring escalation or human approval.
* Compare Developer Agent implementation across Claude Code, GitHub Copilot, and OpenAI Codex.

---

## 3. Background

### 3.1 Why a Dedicated Developer Agent Exists

* Separation of concerns in agentic engineering.
* Prevent one AI Agent from becoming:

  * planner
  * implementer
  * reviewer
  * validator
  * approver
* Enterprise need for independent controls.

### 3.2 Developer Agent in Traditional Engineering Terms

Compare the role with:

* software developer
* implementation engineer
* pair-programming participant
* autonomous coding agent

### 3.3 Developer Agent Versus Lead Agent

| Responsibility         | Lead Agent         | Developer Agent      |
| ---------------------- | ------------------ | -------------------- |
| Interpret Goal         | Primary            | Consume              |
| Repository discovery   | Coordinate         | Task-focused         |
| Select Skills          | Primary            | Apply                |
| Select Roles           | Primary            | No                   |
| Decompose work         | Primary            | Refine assigned task |
| Modify code            | Normally delegated | Primary              |
| Run local checks       | Coordinate         | Primary              |
| Approve implementation | No                 | No                   |
| Escalate issues        | Yes                | Yes                  |

### 3.4 Developer Agent Versus Reviewer

* Developer produces changes.
* Reviewer independently examines them.
* Reviewer must not merely repeat Developer reasoning.

### 3.5 Developer Agent Versus Validator

* Developer may run checks.
* Validator independently verifies results.
* Deterministic gates remain authoritative for machine-verifiable requirements.

---

## 4. Concepts

### 4.1 What Is a Developer Agent?

Define the Developer Agent as:

> The implementation role responsible for modifying code, configuration, tests, contracts, and related repository artifacts according to an assigned plan, applicable Instructions, selected Skills, repository evidence, and defined acceptance criteria.

### 4.2 Inputs to the Developer Agent

Typical inputs:

```text
Goal
Plan
Task Assignment
Acceptance Criteria
Repository Intelligence
Instructions
Selected Skills
Steering Notes
Knowledge Sources
Architecture Constraints
Existing Code
```

### 4.3 Outputs from the Developer Agent

Typical outputs:

```text
Code Changes
Configuration Changes
Tests
Contract Changes
Migration Changes
Local Check Results
Implementation Evidence
Assumptions
Risks
Open Questions
Handoff Package
```

### 4.4 Responsibilities

* Understand assigned scope.
* Inspect relevant repository areas.
* Follow Instructions.
* Execute selected Skills.
* Implement required behavior.
* Preserve architectural boundaries.
* Add or modify tests.
* Run permitted local checks.
* Report failures accurately.
* Produce evidence.
* Hand work to independent roles.

### 4.5 Authority

The Developer Agent may normally:

* read repository content
* inspect relevant dependencies
* modify files inside assigned scope
* create required implementation artifacts
* run approved development tools
* run local builds and tests
* correct implementation failures

### 4.6 Limits

The Developer Agent must not independently:

* change the Goal
* redefine acceptance criteria
* bypass Instructions
* ignore Steering Notes
* weaken tests to force success
* disable deterministic gates
* approve architecture exceptions
* approve security exceptions
* approve its own implementation
* merge to protected branches unless policy explicitly permits
* silently expand task scope
* silently change enterprise standards

### 4.7 Responsibility Boundary Diagram

```text
                     Developer Agent Boundary
┌─────────────────────────────────────────────────────────┐
│                                                         │
│ Read Assigned Work                                      │
│ Read Repository Intelligence                            │
│ Read Instructions                                       │
│ Apply Skills                                            │
│ Inspect Existing Code                                   │
│ Plan Local Implementation                               │
│ Modify Code / Configuration                             │
│ Add Tests                                               │
│ Run Local Checks                                        │
│ Correct Implementation Failures                         │
│ Produce Evidence                                        │
│                                                         │
└─────────────────────────────────────────────────────────┘
               │
               ▼
      Independent Controls

  Deterministic Gates
        Reviewer
        Validator
        Evaluator
     Human Approval

Developer Agent cannot replace these controls.
```

---

## 5. Architecture Discussion

### 5.1 Developer Agent Inside the Harness

```text
Goal
 │
 ▼
Lead Agent
 │
 │ Task Assignment
 ▼
Developer Agent
 │
 ├── Repository Intelligence
 ├── Instructions
 ├── Skills
 ├── Steering Notes
 ├── Knowledge Sources
 └── Tools
 │
 ▼
Implementation
 │
 ▼
Local Checks
 │
 ▼
Deterministic Gates
 │
 ├── Pass ──► Reviewer
 │             │
 │             ▼
 │          Validator
 │
 └── Fail ──► Correction Cycle
```

### 5.2 Control Plane Versus Execution Plane

Relate the Developer Agent to Chapter 13:

**Control plane**

* Lead Agent
* Harness workflow
* policy
* state
* gate definitions
* approval rules

**Execution plane**

* Developer Agent
* repository tools
* compiler
* test runner
* package tools
* code-generation tools

### 5.3 Receiving Work from the Lead Agent

A structured assignment should include:

* Goal reference
* task description
* scope
* target files or components where known
* acceptance criteria
* selected Skills
* applicable Instructions
* relevant Repository Intelligence
* dependencies
* risks
* required evidence
* expected handoff target

### 5.4 Task Scope Interpretation

The Developer Agent determines:

* what must change
* what must remain unchanged
* what evidence is required
* what dependencies are relevant
* where clarification or escalation is required

### 5.5 Scope Discipline

Distinguish:

**Required change**

* necessary to satisfy assigned acceptance criteria.

**Supporting change**

* technically necessary for required behavior.

**Opportunistic change**

* unrelated refactoring or cleanup.

Developer Agents should avoid opportunistic changes unless explicitly authorized.

---

## 6. Using Repository Intelligence

### 6.1 Repository Intelligence Before Modification

Inspect:

* solution structure
* project boundaries
* architecture
* existing feature patterns
* naming
* dependency direction
* testing conventions
* API conventions
* event conventions
* persistence patterns
* observability conventions

### 6.2 Task-Focused Repository Discovery

The Developer Agent should not rediscover the entire repository.

Focus on:

* relevant bounded context
* neighboring implementations
* interfaces
* contracts
* tests
* configuration
* deployment implications

### 6.3 Evidence Before Assumption

Example:

Before creating a fleet booking endpoint, inspect:

* existing booking endpoints
* route conventions
* authorization policies
* result types
* validation patterns
* mapping strategy
* logging conventions

### 6.4 Handling Conflicting Repository Evidence

Priority should follow established project governance:

* current authoritative Instructions
* architecture decisions
* accepted Steering Notes
* current source code
* approved contracts
* trusted Knowledge Sources

Escalate material conflicts rather than silently choosing.

---

## 7. Reading and Applying Instructions

### 7.1 Instructions as Mandatory Constraints

Examples:

* Clean Architecture dependency rules
* naming conventions
* error handling
* logging requirements
* testing standards
* event schema requirements
* security standards

### 7.2 Relevant Versus Irrelevant Instructions

The Developer Agent should identify instructions applicable to:

* repository
* project
* directory
* technology
* specific artifact

### 7.3 Instruction Precedence

Discuss:

* repository-wide Instructions
* directory-scoped Instructions
* technology-specific Instructions
* task-specific requirements

### 7.4 What to Do When Instructions Conflict

* do not improvise silently
* report conflict
* preserve evidence
* escalate to Lead Agent or human authority

---

## 8. Using Selected Skills

### 8.1 Skills as Reusable Implementation Workflows

Examples for fleet booking:

* `create-rest-endpoint`
* `add-application-command`
* `add-domain-behavior`
* `add-integration-event`
* `transactional-outbox`
* `add-unit-tests`
* `add-observability`

### 8.2 Skill and Instruction Usage Diagram

```text
                  Assigned Task
                       │
                       ▼
               Developer Agent
                  /          \
                 /            \
                ▼              ▼
         Instructions        Skills
      "What must be true"  "How recurring
                            work is done"
                \              /
                 \            /
                  ▼          ▼
                 Implementation
                       │
                       ▼
                 Repository Change
```

### 8.3 Skills Do Not Override Instructions

```text
Instruction
    ↓
Constraint

Skill
    ↓
Reusable Workflow

Task
    ↓
Specific Requirement
```

Implementation must satisfy all three.

### 8.4 When a Skill Does Not Fit

* do not force-fit obsolete workflows
* report mismatch
* implement only within authorized scope
* recommend Skill improvement separately
* never silently rewrite a governed Skill

---

## 9. Implementation Planning

### 9.1 Lead Plan Versus Developer Implementation Plan

The Lead Agent plans the overall work.

The Developer Agent may refine its assigned task into implementation steps.

### 9.2 Example Implementation Plan

```text
1. Inspect existing Booking feature.
2. Confirm customer authorization pattern.
3. Add fleet booking domain behavior.
4. Add application use case.
5. Add REST endpoint.
6. Add persistence mapping.
7. Add integration event.
8. Add transactional outbox record.
9. Add tests.
10. Run build and targeted tests.
11. Collect implementation evidence.
```

### 9.3 Dependency Awareness

Identify dependencies such as:

* Customer service
* Station service
* availability logic
* SQL schema
* Event Hub contracts
* authentication policies

### 9.4 Small, Reviewable Changes

Promote:

* cohesive modifications
* minimal unrelated churn
* explicit file changes
* understandable commits or patches

---

## 10. Code Modification

### 10.1 Modifying Existing Code

Before modification:

* understand existing design
* inspect adjacent code
* preserve conventions
* avoid speculative refactoring

### 10.2 Creating New Code

Ensure:

* appropriate layer
* correct namespace
* dependency direction
* standard naming
* error handling
* logging
* tests

### 10.3 Clean Architecture Boundaries

Fleet booking example:

```text
API
 ↓
Application
 ↓
Domain

Infrastructure
 ├── implements Application ports
 └── integrates external systems
```

### 10.4 Avoiding Architectural Leakage

Examples:

* Domain should not reference EF Core.
* Application should not directly depend on Azure Event Hub SDK.
* API should not contain business rules.
* Infrastructure concerns should not leak into domain behavior.

---

## 11. Configuration Changes

### 11.1 Configuration as Production Code

Treat changes to:

* `appsettings.json`
* environment variables
* Docker files
* Kubernetes manifests
* CI/CD
* feature flags
* telemetry settings

with the same discipline as source code.

### 11.2 Secret Handling

Never:

* hardcode credentials
* expose secrets in logs
* place sensitive values in generated output
* create insecure defaults

### 11.3 Environment-Aware Changes

Consider:

* local development
* test
* staging
* production

### 11.4 Configuration Evidence

Document:

* new keys
* required environment values
* deployment implications
* migration requirements

---

## 12. Tests

### 12.1 Tests Are Part of Implementation

The Developer Agent should not treat testing as somebody else’s responsibility.

### 12.2 Test Categories

* unit tests
* application tests
* integration tests
* contract tests
* architecture tests
* security-related tests where applicable

### 12.3 Fleet Booking Tests

Examples:

* valid corporate customer can create booking
* inactive corporate account rejected
* unavailable station rejected
* invalid vehicle count rejected
* duplicate booking command handled correctly
* integration event created
* outbox record persisted

### 12.4 Never Manipulate Tests to Manufacture Success

Anti-patterns:

* deleting failing assertions
* skipping tests
* broadening expected values
* disabling test categories
* changing tests without requirement justification

---

## 13. API and Event Contracts

### 13.1 API Contract Responsibility

Developer Agent must preserve:

* route conventions
* request/response models
* status code semantics
* versioning
* validation
* authorization

### 13.2 Example Corporate Fleet Booking API

```text
POST /api/fleet-bookings
```

Conceptual request:

```json
{
  "customerId": "...",
  "stationId": "...",
  "scheduledDate": "...",
  "vehicles": [...]
}
```

### 13.3 Event Contract Responsibility

Example:

```text
FleetBookingCreated
```

Consider:

* EventId
* CorrelationId
* schema version
* occurred time
* business identifiers
* compatibility

### 13.4 Contract Changes Require Additional Caution

Escalate:

* breaking API changes
* event schema breaking changes
* shared DTO changes
* public contract removal

---

## 14. Persistence Changes

### 14.1 Persistence Is More Than Updating an Entity

Potential changes:

* domain entity
* EF Core configuration
* repository
* migration
* indexes
* constraints
* transactional behavior

### 14.2 Transactional Consistency

Fleet booking scenario:

```text
Booking
+
Outbox Event
+
Database Commit
```

should form the required transactional boundary.

### 14.3 Migration Safety

Consider:

* backward compatibility
* destructive operations
* large tables
* default values
* indexes
* rollback implications

### 14.4 Developer Agent Limits

High-risk production database changes may require:

* DBA review
* Architect approval
* human approval
* deployment coordination

---

## 15. Security Requirements

### 15.1 Security Is an Implementation Requirement

The Developer Agent must actively apply:

* authentication
* authorization
* input validation
* least privilege
* secure configuration
* secret handling
* dependency safety

### 15.2 Corporate Fleet Booking Example

Only permitted corporate users should create fleet bookings.

### 15.3 Security-Sensitive Changes

Escalate:

* authorization policy changes
* authentication changes
* encryption changes
* secret management changes
* external identity integrations

### 15.4 Developer Agent Cannot Self-Approve Security Exceptions

---

## 16. Observability Requirements

### 16.1 Instrumentation as Part of Done

Include where applicable:

* structured logs
* traces
* metrics
* correlation IDs
* failure diagnostics

### 16.2 Fleet Booking Observability

Potential telemetry:

* booking creation duration
* rejected booking count
* event publication status
* outbox processing latency
* failures by station

### 16.3 Avoiding Sensitive Data in Telemetry

* credentials
* tokens
* personal data
* unnecessary business-sensitive fields

---

## 17. Tool Usage

### 17.1 Typical Developer Agent Tools

* repository search
* file editing
* build tools
* test runner
* package manager
* Git
* linters
* formatters
* schema tools
* container tools

### 17.2 Tool Permissions

Harness policies may restrict:

* shell execution
* network access
* package installation
* deployment
* database access
* secret access
* Git push
* branch modification

### 17.3 Least-Privilege Tooling

Developer Agent receives only the access required to implement its assignment.

### 17.4 Tool Output Is Evidence

Preserve relevant:

* build output
* test summary
* lint summary
* changed-file list
* migration result

---

## 18. Local Checks

### 18.1 Purpose of Local Checks

Catch obvious issues before formal gate execution.

### 18.2 Typical Checks

```text
dotnet restore
dotnet build
dotnet test
formatter
lint
architecture tests
targeted contract checks
```

### 18.3 Local Checks Versus Deterministic Gates

Local checks:

* Developer-controlled
* fast feedback
* may be targeted

Deterministic gates:

* Harness-controlled
* independently executed
* authoritative for workflow progression

### 18.4 Local Success Does Not Equal Approval

---

## 19. Handling Build and Test Failures

### 19.1 Failure as Engineering Evidence

A failure is information, not permission to bypass controls.

### 19.2 Failure Classification

Possible causes:

* Developer implementation
* pre-existing repository issue
* environment issue
* dependency issue
* incorrect task assumption
* stale Skill
* contradictory Instruction

### 19.3 Correction Strategy

* capture failure
* identify probable cause
* correct within assigned authority
* rerun appropriate checks
* record result

### 19.4 Do Not Hide Pre-Existing Failures

Report them explicitly.

---

## 20. Deterministic Validation Gates

### 20.1 Relationship with Chapter 12 and Chapter 13

Reinforce deterministic gates as independent machine-verifiable controls.

### 20.2 Example Gate Pipeline

```text
Implementation
      │
      ▼
   Build Gate
      │
      ▼
   Test Gate
      │
      ▼
   Lint Gate
      │
      ▼
Architecture Gate
      │
      ▼
 Security Gate
      │
      ▼
 Contract Gate
```

### 20.3 Developer Interaction with Gates

The Developer Agent:

* receives results
* fixes valid implementation failures
* resubmits

The Developer Agent does not:

* mark gates successful
* edit gate results
* suppress gate failures
* remove gates

### 20.4 Independent Execution

Prefer the Harness or Validator environment to execute authoritative gates.

---

## 21. Failure and Correction Cycle

### 21.1 Correction Loop Diagram

```text
Developer Agent
      │
      ▼
 Implementation
      │
      ▼
 Local Checks
      │
      ▼
Deterministic Gates
   │          │
 PASS        FAIL
   │          │
   ▼          ▼
Reviewer   Failure Evidence
              │
              ▼
       Developer Agent
              │
              ▼
          Correction
              │
              └──────────────► Local Checks
```

### 21.2 Bounded Retries

Discuss:

* configurable retry limits
* avoiding infinite loops
* escalating repeated failure

### 21.3 When to Stop Retrying

Escalate when:

* requirements appear contradictory
* architecture decision required
* external dependency unavailable
* same gate repeatedly fails
* requested change exceeds authority
* security concern appears
* acceptance criteria cannot be met safely

---

## 22. Reporting Assumptions and Risks

### 22.1 Assumptions Must Be Visible

Example:

```text
Assumption:
Corporate fleet bookings use the existing CorporateCustomer
authorization policy.
```

### 22.2 Risk Reporting

Examples:

* schema change may affect existing consumers
* migration may lock a high-volume table
* new event requires downstream consumer update

### 22.3 Unresolved Questions

Separate:

* implementation issue
* architecture decision
* business decision
* operational decision

### 22.4 Never Hide Uncertainty Behind Generated Code

---

## 23. Producing Implementation Evidence

### 23.1 Why Evidence Matters

Evidence supports:

* review
* validation
* audit
* debugging
* governance
* metrics
* future learning

### 23.2 Suggested Evidence Package

```yaml
task: create-corporate-fleet-booking
status: implementation-complete

changed_files:
  - ...
  - ...

local_checks:
  build: passed
  unit_tests: passed
  integration_tests: passed

tests_added:
  - ...

contracts_changed:
  - FleetBookingCreated

assumptions:
  - ...

risks:
  - ...

unresolved_items:
  - ...
```

### 23.3 Evidence Versus Self-Approval

Evidence says:

> “This is what I changed and what happened.”

It does not say:

> “Therefore my work is approved.”

---

## 24. Handoff to Reviewer and Validator

### 24.1 Developer Agent Handoff Diagram

```text
Lead Agent
    │
    ▼
Developer Agent
    │
    ├── Code Changes
    ├── Tests
    ├── Config Changes
    ├── Assumptions
    ├── Risks
    ├── Local Check Results
    └── Implementation Evidence
    │
    ▼
Deterministic Gates
    │
    ▼
Reviewer Agent
    │
    ▼
Validator Agent
```

### 24.2 Reviewer Handoff Contents

Reviewer needs:

* task intent
* changed files
* implementation rationale
* known risks
* test changes
* contract implications

### 24.3 Validator Handoff Contents

Validator needs:

* acceptance criteria
* gate results
* reproducible verification steps
* environmental requirements

### 24.4 Avoiding Context Loss

Structured handoff is preferable to copying the entire Developer conversation.

---

## 25. Human Approval Boundaries

### 25.1 Changes That May Require Human Approval

Examples:

* breaking public API
* breaking event schema
* production database migration
* security policy modification
* architecture exception
* infrastructure deletion
* irreversible operation
* deployment to production
* protected branch merge

### 25.2 Approval Is Policy-Driven

Not every implementation requires direct human intervention.

### 25.3 Developer Agent Must Respect Approval State

```text
Implement
   ↓
Validate
   ↓
Review
   ↓
Approval Required?
   ├── No ─► Continue workflow
   └── Yes ─► Human Approval
```

---

## 26. Developer Agent Inside the Harness

### 26.1 Full Workflow Diagram

```text
                       Harness
┌─────────────────────────────────────────────────────┐
│                                                     │
│ Goal                                                │
│  │                                                  │
│  ▼                                                  │
│ Lead Agent                                          │
│  │                                                  │
│  │ Assignment                                       │
│  ▼                                                  │
│ Developer Agent                                     │
│  ├── Repository Intelligence                        │
│  ├── Instructions                                   │
│  ├── Skills                                         │
│  ├── Steering Notes                                 │
│  ├── Knowledge Sources                              │
│  └── Tools                                          │
│  │                                                  │
│  ▼                                                  │
│ Implementation                                      │
│  │                                                  │
│  ▼                                                  │
│ Local Checks                                        │
│  │                                                  │
│  ▼                                                  │
│ Deterministic Gates                                 │
│  │                                                  │
│  ├── Fail ──► Developer Correction Cycle            │
│  │                                                  │
│  └── Pass                                           │
│       │                                             │
│       ▼                                             │
│    Reviewer                                         │
│       │                                             │
│       ▼                                             │
│    Validator                                        │
│       │                                             │
│       ▼                                             │
│    Evaluator / Approval / Next Stage                │
│                                                     │
└─────────────────────────────────────────────────────┘
```

### 26.2 Harness State

Potential states:

```text
ASSIGNED
IMPLEMENTING
LOCAL_CHECK
GATE_FAILED
CORRECTING
IMPLEMENTATION_COMPLETE
READY_FOR_REVIEW
ESCALATED
```

### 26.3 Audit Trail

Record:

* assignment
* agent identity/version
* tools used
* files changed
* gate executions
* retry count
* assumptions
* handoff
* timestamps

### 26.4 Metrics

Possible metrics:

* implementation duration
* first-pass build success
* first-pass test success
* gate failure rate
* correction count
* files modified
* review findings
* escaped defects

---

## 27. Hands-on Example

### 27.1 Scenario

Implement corporate fleet booking for Alpha Car Detailing.

### 27.2 Assigned Goal

```text
Corporate customers must be able to create fleet bookings
for an eligible station while preserving existing booking
architecture, security, eventing, and transactional standards.
```

### 27.3 Lead Agent Assignment

Include:

* task scope
* selected Skills
* applicable Instructions
* acceptance criteria
* relevant repository locations

### 27.4 Repository Inspection

Developer Agent examines:

* existing booking endpoint
* corporate customer authorization
* booking aggregate
* persistence
* outbox
* events
* tests

### 27.5 Implementation Steps

* add request model
* add endpoint
* add application use case
* add domain behavior
* update persistence
* generate migration
* add integration event
* add outbox entry
* add logs/tracing
* add tests

### 27.6 Local Checks

Run:

* targeted build
* unit tests
* integration tests
* architecture tests

### 27.7 Gate Failure Example

Architecture gate reports an Infrastructure dependency in Application.

### 27.8 Correction

* inspect dependency
* replace direct SDK dependency with Application port
* move implementation to Infrastructure
* rerun checks

### 27.9 Final Evidence

Produce structured change summary and handoff.

---

## 28. Claude Example

### 28.1 Claude Code as Developer Agent

Demonstrate Claude Code receiving:

* task
* Instructions
* repository context
* Skill references
* acceptance criteria

### 28.2 Example Developer Agent Prompt

Include a governed implementation prompt covering:

* assigned scope
* files to inspect
* applicable Skills
* constraints
* local checks
* prohibited actions
* evidence requirements

### 28.3 Claude Code Workflow

```text
Lead Task
   ↓
Claude Code Developer Role
   ↓
Read CLAUDE.md
   ↓
Read Relevant Skills
   ↓
Inspect Repository
   ↓
Implement
   ↓
Run Local Checks
   ↓
Report Evidence
```

### 28.4 Permission Controls

Discuss Claude Code permissions for:

* reads
* edits
* shell commands
* Git commands
* network access

### 28.5 Claude-Specific Considerations

* repository instruction discovery
* tool execution
* iterative code modification
* context management
* role prompting

### 28.6 Keep Principles Vendor-Neutral

Claude Code is the implementation mechanism, not the architectural definition of Developer Agent.

---

## 29. GitHub Copilot Comparison

### 29.1 Copilot as an Implementation Participant

Discuss:

* IDE-centered development
* coding agent capabilities where available
* repository Instructions
* prompt files
* agent workflows

### 29.2 Differences from Claude Code

Compare:

* interaction model
* tool execution
* repository autonomy
* context gathering
* role orchestration
* permission model

### 29.3 Developer Agent Pattern Still Applies

Regardless of product:

```text
Assigned Work
+
Instructions
+
Repository Context
+
Skills/Workflow
+
Implementation
+
Independent Validation
```

### 29.4 Enterprise Harness Integration

Explain that external Harness orchestration may be used around Copilot capabilities.

---

## 30. OpenAI Codex Comparison

### 30.1 Codex as an Implementation Agent

Discuss Codex in terms of:

* task execution
* repository changes
* command execution
* tests
* implementation reporting

### 30.2 Comparison Dimensions

* autonomy
* execution environment
* repository context
* instruction mechanisms
* tool model
* handoff integration

### 30.3 Vendor-Neutral Boundary

The Harness role model remains stable even as agent capabilities change.

---

## 31. Best Practices

### 31.1 Start with Evidence

Inspect before editing.

### 31.2 Keep Scope Explicit

Modify only what the task requires.

### 31.3 Follow Instructions Before Personal Preference

Repository standards win.

### 31.4 Use Skills for Recurring Engineering Work

Avoid reinventing established workflows.

### 31.5 Preserve Architecture Boundaries

Do not take shortcuts merely because generated code compiles.

### 31.6 Treat Tests as Implementation

Add meaningful verification.

### 31.7 Run Fast Local Checks Early

Reduce unnecessary gate cycles.

### 31.8 Preserve Failure Evidence

Never conceal failures.

### 31.9 Produce Structured Handoffs

Make downstream review reproducible.

### 31.10 Keep Approval Independent

Developer Agent must never approve its own work.

### 31.11 Make Assumptions Explicit

Unstated assumptions are hidden risk.

### 31.12 Escalate Material Decisions

Do not silently decide architecture, security, or business policy.

---

## 32. Anti-patterns

### 32.1 The Autonomous Everything Agent

One agent:

* plans
* codes
* tests
* reviews
* validates
* approves

Explain why this destroys separation of duties.

### 32.2 Coding Before Repository Inspection

Creates convention and architecture violations.

### 32.3 Ignoring Instructions

Produces locally valid but organizationally invalid code.

### 32.4 Skill Blindness

Reimplementing existing governed workflows.

### 32.5 Scope Creep

Refactoring unrelated components during a targeted task.

### 32.6 Test Manipulation

Changing tests until code appears correct.

### 32.7 Gate Bypass

Disabling checks to progress the workflow.

### 32.8 Infinite Retry Loop

Repeatedly applying speculative fixes without escalation.

### 32.9 Silent Architecture Decisions

Introducing new libraries, patterns, or dependencies without approval.

### 32.10 Generated-Code Confidence

Assuming syntactically plausible output is production-ready.

### 32.11 Evidence-Free Handoff

Handing Reviewer only a patch without implementation context.

### 32.12 Self-Approval

Treating successful local tests as proof of acceptance.

---

## 33. Architect’s Notes

### Architect’s Note: Developer Is a Responsibility Boundary

The Developer Agent should be defined by authority and accountability, not merely by a prompt name.

### Architect’s Note: Independent Validation Matters More as Autonomy Grows

More capable implementation agents increase the need for independent controls.

### Architect’s Note: Repository Intelligence Should Reduce Guessing

High-quality implementation starts with evidence.

### Architect’s Note: Deterministic Gates Are Not AI Reviewers

Machine-verifiable properties should be checked deterministically wherever practical.

### Architect’s Note: Skills Are Governance Assets

A mature organization reduces variation by encoding recurring workflows as governed Skills.

### Architect’s Note: Correction Loops Need Boundaries

Unlimited autonomous retries create cost, noise, and hidden risk.

---

## 34. Enterprise Tips

### Enterprise Tip: Give Developer Agents Least Privilege

Implementation agents rarely need unrestricted production access.

### Enterprise Tip: Separate Build Credentials from Deployment Credentials

A Developer Agent should not automatically inherit deployment authority.

### Enterprise Tip: Log Agent-Initiated Changes

Audit:

* task
* model
* tools
* changed files
* checks
* gate outcomes

### Enterprise Tip: Standardize Implementation Evidence

A common handoff schema makes review and metrics easier.

### Enterprise Tip: Measure First-Pass Success

First-pass gate success can reveal:

* Skill quality
* Instruction clarity
* repository health
* implementation reliability

### Enterprise Tip: Make High-Risk File Areas Explicit

Examples:

* authentication
* authorization
* Terraform
* production manifests
* database migrations
* public contracts

---

## 35. Decision Points

### Decision Point 1

Should the Developer Agent modify files directly or produce a patch for later application?

Consider:

* repository risk
* tool permissions
* branch protections
* audit requirements

### Decision Point 2

Which local checks should the Developer Agent run?

Consider:

* execution time
* feedback speed
* resource consumption
* formal gate coverage

### Decision Point 3

How many automatic correction cycles should be allowed?

Consider:

* task complexity
* model cost
* failure classification
* risk

### Decision Point 4

Which changes require immediate human escalation?

Examples:

* security
* public contracts
* migrations
* architecture exceptions

### Decision Point 5

Should Developer Agents commit changes?

Possible policies:

* no Git writes
* local commit allowed
* feature branch only
* Harness-owned commit

### Decision Point 6

Should Developer Agents install dependencies automatically?

Evaluate:

* supply-chain risk
* license policy
* version standards
* architecture implications

---

## 36. Exercises

### Exercise 1 — Define the Developer Agent Contract

Create:

* responsibilities
* inputs
* outputs
* permissions
* prohibited actions

for Alpha Car Detailing.

### Exercise 2 — Design an Assignment Package

Write a Lead-to-Developer task assignment for fleet booking.

### Exercise 3 — Map Instructions and Skills

Identify which Instructions and Skills apply to:

* endpoint
* domain
* persistence
* integration event
* tests

### Exercise 4 — Design the Implementation Workflow

Create:

```text
Lead Agent
    ↓
Developer Agent
    ↓
Implement
    ↓
Local Checks
    ↓
Deterministic Gates
    ↓
Reviewer
    ↓
Validator
```

### Exercise 5 — Design a Correction Cycle

Handle:

* build failure
* architecture gate failure
* contract test failure

### Exercise 6 — Define Escalation Rules

Determine which failures:

* can be fixed automatically
* should return to Lead Agent
* require Architect
* require human approval

### Exercise 7 — Create an Implementation Evidence Schema

Include:

* files
* tests
* gates
* assumptions
* risks
* unresolved questions

### Exercise 8 — Identify Anti-patterns

Review a hypothetical Developer Agent that:

* changes unrelated files
* disables a test
* adds a dependency
* modifies an event schema
* declares its work approved

Identify each governance violation.

---

## 37. Interview Questions

### Foundational

1. What is the purpose of a Developer Agent?
2. How does a Developer Agent differ from a Lead Agent?
3. What inputs should a Developer Agent receive?
4. What should a Developer Agent produce?
5. Why should implementation and approval be separated?

### Intermediate

1. How should a Developer Agent use Repository Intelligence?
2. What is the relationship between Instructions and Skills?
3. Why should a Developer Agent run local checks if deterministic gates exist?
4. What should happen when local tests fail?
5. What should happen when a deterministic gate fails?
6. Why should assumptions be included in the handoff?
7. How should task scope be controlled?

### Advanced

 1. How would you prevent a Developer Agent from bypassing validation?
 2. How would you design retry policies for implementation failures?
 3. Which Developer Agent actions should require human approval?
 4. How would you isolate Developer Agent permissions in an enterprise Harness?
 5. How would you prevent supply-chain risk from autonomous dependency installation?
 6. How would you make Developer Agent output auditable?
 7. How would you measure Developer Agent effectiveness without rewarding unsafe shortcuts?
 8. How do Developer Agent boundaries change as coding agents become more autonomous?

### Architecture Scenario

 1. A Developer Agent needs to break an existing event contract to satisfy its task. What should happen?
 2. A test fails because of a pre-existing repository problem. How should the Developer Agent report it?
 3. A Skill conflicts with a current repository Instruction. Which takes precedence?
 4. The Developer Agent repeatedly fails the same architecture gate. When should the Harness stop retrying?
 5. The Developer Agent discovers that implementing the feature requires an architecture decision not included in the task. What is the correct escalation path?

---

## 38. Chapter Summary

Reinforce:

* The Developer Agent is the Harness implementation role.
* It receives bounded work from the Lead Agent.
* Repository Intelligence provides evidence for implementation.
* Instructions define mandatory engineering constraints.
* Skills provide reusable implementation workflows.
* The Developer Agent modifies code, configuration, contracts, persistence, and tests within assigned authority.
* Local checks provide rapid feedback.
* Deterministic Validation Gates remain independent and authoritative.
* Failed gates trigger controlled correction cycles.
* The Developer Agent must report assumptions, risks, and evidence.
* Reviewer and Validator roles independently assess the result.
* The Developer Agent never approves its own implementation.
* Human approval remains mandatory wherever enterprise policy requires it.
* Claude Code, GitHub Copilot, and OpenAI Codex may implement the role differently, but the engineering responsibility boundary remains vendor-neutral.

Conclude with the transition:

> Chapter 15 established how implementation work is performed inside the Harness. Chapter 16 moves to the next independent responsibility: the Reviewer Agent, which examines the implementation for correctness, maintainability, architectural alignment, risks, and defects without inheriting the Developer Agent’s assumptions.

---

## 39. Further Reading

Organize references under:

### AI Coding Agents

* Claude Code documentation
* GitHub Copilot documentation
* OpenAI Codex documentation

### Software Architecture

* Clean Architecture
* Domain-Driven Design
* enterprise application architecture
* microservice architecture

### Software Quality

* automated testing
* architecture testing
* static analysis
* contract testing

### Secure Software Development

* OWASP guidance
* secure development lifecycle practices
* software supply-chain security

### DevOps and Automation

* CI/CD pipelines
* policy-as-code
* automated validation
* GitOps
* protected branch workflows

### Internal Handbook References

* Chapter 5 — Repository Discovery
* Chapter 6 — Instructions
* Chapter 7 — Skills
* Chapter 8 — Prompts
* Chapter 9 — Roles
* Chapter 10 — Steering Notes
* Chapter 11 — Knowledge Sources
* Chapter 12 — What Is a Harness?
* Chapter 13 — Harness Architecture
* Chapter 14 — Lead Agent
* Chapter 16 — Reviewer Agent

**Chapter 15 outline is complete.**
