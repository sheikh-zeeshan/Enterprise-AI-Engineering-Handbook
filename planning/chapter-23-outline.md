# Chapter 23 — Claude Skills  

**Part IV — Claude Engineering**  
**Target file:** `book/23-claude-skills.md`

## Detailed Chapter Outline

### 1. Story-Driven Opening — From Repeated Instructions to Reusable Engineering Workflows

- Introduce a recurring engineering problem in **Alpha Car Detailing**.
- A developer repeatedly asks Claude Code to:
  - create an application command,
  - add validation,
  - update persistence,
  - add tests,
  - follow Clean Architecture boundaries,
  - run validation,
  - report evidence.
- Show the weakness of solving the same recurring workflow with increasingly long Prompts.
- Introduce the idea that recurring engineering procedures should become **Skills**.
- Establish the chapter principle:
  - Instructions define persistent repository expectations.
  - Prompts describe the current task.
  - Skills encode reusable engineering workflows.
  - The Harness decides when and how those workflows participate in controlled execution.
- Reinforce the handbook's frozen terminology: a Skill is a reusable engineering workflow, not a macro or generic script. AI development handbook
- Connect Chapter 23 with Chapter 7:
  - Chapter 7 explained Skills as an AI Engineering concept.
  - Chapter 23 explains how those ideas are implemented specifically with Claude Code.
- State the core principle:

> **A Claude Skill should encode a reusable engineering workflow. It should not duplicate repository Instructions, task-specific Prompts, or temporary run state.**

---

## 2. Learning Objectives

By the end of the chapter, the reader should be able to:

- Explain what a Claude Skill is.
- Distinguish Claude Skills from:
  - `CLAUDE.md`,
  - Prompts,
  - Commands,
  - Harness workflows,
  - temporary run state.
- Identify engineering work that belongs in a Skill.
- Design a Skill with:
  - clear purpose,
  - inputs,
  - outputs,
  - preconditions,
  - workflow steps,
  - tool requirements,
  - validation,
  - failure behavior.
- Design reusable Skills without hard-coding a single task.
- Compose Skills safely.
- Define Skill discovery and selection rules.
- Version and test Skills.
- Establish ownership and governance.
- Use Skills within the Harness.
- Apply security and human-approval boundaries.
- Build a reusable enterprise Skill library for Alpha Car Detailing.

---

# 3. Background — From Conceptual Skills to Claude Skills

### 3.1 Revisiting the Skill Concept

- Recall the handbook definition:
  - Skills represent reusable implementation workflows.
  - They describe **how recurring engineering work is performed**. AI development handbook
- Examples:
  - Create API endpoint.
  - Add domain behavior.
  - Add integration event.
  - Add OpenTelemetry.
- Explain why reusable workflows are important in enterprise development.

### 3.2 Chapter 7 versus Chapter 23

- Chapter 7:
  - vendor-neutral Skill concept,
  - architectural purpose,
  - Skill libraries,
  - relationship with Repository Intelligence.
- Chapter 23:
  - Claude-oriented implementation,
  - how Claude discovers and executes Skills,
  - how Skills interact with Claude tools,
  - how Skills fit into a Harness.

### 3.3 Why Enterprise Teams Need Skills

- Reduce repeated prompt engineering.
- Standardize implementation patterns.
- Preserve architectural conventions.
- Reduce variability between developers and AI Agents.
- Make common engineering workflows reviewable.
- Provide reusable operational knowledge.
- Improve Harness orchestration.
- Support governance and controlled evolution.

### 3.4 What a Skill Is Not

A Skill is not:

- repository-wide policy,
- a replacement for `CLAUDE.md`,
- today's task description,
- temporary reasoning state,
- arbitrary shell automation,
- a complete autonomous Agent,
- a deterministic validation gate,
- a source of authority overriding architecture,
- a place for credentials or secrets.

---

# 4. What Is a Claude Skill?

### 4.1 Working Definition

Define a Claude Skill as:

> A reusable, discoverable engineering capability that tells Claude how to perform a recurring class of work within defined inputs, constraints, workflow steps, validation requirements, and output expectations.

### 4.2 Characteristics of a Good Skill

A strong Skill should be:

- reusable,
- focused,
- deterministic where possible,
- composable,
- repository-aware,
- explicit about assumptions,
- explicit about validation,
- safe,
- independently testable,
- governed.

### 4.3 Skill Scope

Discuss appropriate scope.

Too small:

```text
CreateClass
RenameMethod
AddUsingStatement
```

Too large:

```text
BuildEntireMicroservicePlatform
ImplementEntireBusinessRequirement
```

Appropriate:

```text
CreateApiEndpoint
AddApplicationCommand
AddIntegrationEvent
AddTransactionalOutbox
```

### 4.4 Capability versus Task

Example:

**Skill**

```text
AddIntegrationEvent
```

**Task**

```text
Publish VehicleDetailingCompleted when a corporate fleet booking
is completed.
```

The Skill supplies the reusable method.

The Prompt supplies the specific objective.

---

# 5. Claude Skills versus `CLAUDE.md`

### 5.1 Purpose of `CLAUDE.md`

Explain that Instructions contain persistent repository-wide engineering expectations, such as:

- architecture,
- naming conventions,
- dependency rules,
- testing requirements,
- security,
- logging,
- code style.

The handbook already defines Instructions as persistent repository-wide engineering rules. AI development handbook

### 5.2 Purpose of a Skill

Skills answer:

> How should Claude perform this recurring engineering workflow?

Examples:

```text
CLAUDE.md
Controllers must not contain business logic.

Skill
Create an API endpoint using the project's endpoint,
application-command, validation, and test conventions.
```

### 5.3 Responsibility Boundary

| Concern | `CLAUDE.md` | Skill |
| --- | --- | --- |
| Architecture rules | Yes | Reads them |
| Naming conventions | Yes | Applies them |
| Recurring implementation workflow | No | Yes |
| Task-specific values | No | Receives them |
| Validation procedure | Defines requirement | Executes workflow-specific validation |
| Temporary state | No | No |

### 5.4 Avoiding Duplication

- Skills should reference Instructions rather than restating them.
- Do not copy architecture policies into every Skill.
- Repository policy remains authoritative.

---

# 6. Diagram — `CLAUDE.md` versus Skill versus Prompt

```text
                   ┌───────────────────────┐
                   │      CLAUDE.md        │
                   │ Persistent Standards  │
                   │ Architecture / Rules  │
                   └───────────┬───────────┘
                               │ constrains
                               ▼
┌──────────────────┐   ┌───────────────────────┐
│      Prompt      │──▶│        Skill          │
│ Current Goal     │   │ Reusable Workflow     │
│ Task Parameters  │   │ Implementation Method │
└──────────────────┘   └───────────┬───────────┘
                                   │
                                   ▼
                         ┌─────────────────────┐
                         │ Implementation      │
                         │ + Validation        │
                         │ + Evidence          │
                         └─────────────────────┘
```

Key message:

```text
CLAUDE.md = how the repository must behave
Skill     = how recurring work is performed
Prompt    = what needs to be done now
```

This follows the handbook's existing distinction between Instructions, Skills, Prompts, and Harness. AI development handbook

---

# 7. Skills versus Prompts

### 7.1 Prompts Represent Today's Task

Examples:

```text
Add an endpoint that allows fleet managers to cancel
an unstarted detailing booking.
```

### 7.2 Skills Represent Reusable Procedure

Possible Skills:

```text
CreateApiEndpoint
AddApplicationCommand
AddDomainBehavior
AddUnitAndIntegrationTests
```

### 7.3 Parameterizing Skills

Prompt supplies:

- entity,
- operation,
- business goal,
- endpoint,
- authorization requirement,
- event requirement.

Skill supplies:

- implementation sequence,
- repository discovery,
- architecture boundaries,
- validation procedure.

### 7.4 Why Prompts Must Not Become Skills

Avoid Skills named:

```text
FixBookingBug123
ImplementFleetCancellation
AddOctoberFeature
```

These encode tasks rather than reusable capabilities.

---

# 8. Skills versus Commands

### 8.1 Commands as Explicit Invocation Mechanisms

Introduce Commands at a conceptual level while deferring full treatment to Chapter 24.

A Command may provide an explicit invocation surface such as:

```text
/create-endpoint
/add-event
/run-feature-tests
```

### 8.2 Skill as Capability

A Skill contains reusable engineering knowledge.

### 8.3 Command as Entry Point

A Command can invoke or expose that capability.

Conceptual model:

```text
Command
   ↓
Select Skill
   ↓
Provide Arguments
   ↓
Execute Workflow
```

### 8.4 Not Every Skill Requires a Command

Skills may be:

- explicitly invoked,
- selected by Claude,
- selected by a Lead Agent,
- invoked by the Harness,
- composed by another Skill.

### 8.5 Chapter Boundary

Note:

> Chapter 23 focuses on reusable engineering capability. Chapter 24 focuses on explicit Claude Commands and command-oriented interaction.

---

# 9. Skill Structure

### 9.1 Recommended Logical Structure

A Claude Skill should define:

1. Name
2. Purpose
3. Intended use
4. Inputs
5. Outputs
6. Preconditions
7. Repository context required
8. Workflow
9. Tool permissions
10. Validation
11. Failure handling
12. Evidence
13. Composition rules
14. Approval boundaries
15. Version information
16. Ownership

### 9.2 Example Skeleton

```text
Skill: AddApplicationCommand

Purpose
Inputs
Preconditions
Repository Discovery
Workflow
Validation
Outputs
Failure Handling
Evidence
Security Boundaries
Composition
Version
Owner
```

### 9.3 Skill Metadata

Discuss metadata such as:

- identifier,
- description,
- category,
- tags,
- version,
- maintainer,
- dependencies,
- related Skills.

### 9.4 Repository Placement

Possible logical organization:

```text
skills/
├── api/
├── application/
├── domain/
├── integration/
├── persistence/
├── security/
├── observability/
└── testing/
```

---

# 10. Inputs and Outputs

## 10.1 Skill Inputs

Inputs should represent task-specific values.

Example for `CreateApiEndpoint`:

```text
Resource: Booking
Operation: Cancel
Method: DELETE
Route: /api/bookings/{bookingId}
AuthorizationPolicy: FleetManager
```

### 10.2 Required versus Optional Inputs

Required:

- operation,
- target,
- business objective.

Optional:

- route preference,
- authorization policy,
- event name,
- performance requirement.

### 10.3 Derived Inputs

Claude may derive some inputs from Repository Intelligence.

Examples:

- namespace,
- folder,
- handler pattern,
- validation framework,
- testing conventions.

### 10.4 Skill Outputs

Possible outputs:

- created files,
- modified files,
- tests,
- validation evidence,
- implementation summary,
- unresolved issues,
- required follow-up.

### 10.5 Outputs Are More Than Code

A Skill should often return structured execution evidence:

```text
Status
Files changed
Tests added
Validation executed
Validation result
Warnings
Human approval required
```

---

# 11. Preconditions

### 11.1 Why Preconditions Matter

A Skill should not start execution when necessary context is missing.

### 11.2 Typical Preconditions

Examples:

- repository discovery completed,
- applicable Instructions loaded,
- target project exists,
- architecture pattern identified,
- required dependencies already approved,
- destination aggregate identified.

### 11.3 Example — `AddDomainBehavior`

Preconditions:

```text
- Target aggregate exists.
- Domain layer identified.
- Current domain conventions discovered.
- Business invariant supplied.
- Persistence changes are not assumed automatically.
```

### 11.4 Failed Preconditions

Expected behavior:

```text
STOP
REPORT missing information
DO NOT invent architecture
DO NOT modify unrelated files
```

---

# 12. Workflow Steps

### 12.1 Workflows Must Be Explicit

A reusable Skill should define an ordered procedure.

### 12.2 Standard Engineering Sequence

```text
Understand
    ↓
Discover
    ↓
Validate Preconditions
    ↓
Plan
    ↓
Implement
    ↓
Test
    ↓
Validate
    ↓
Report Evidence
```

### 12.3 Repository Discovery Before Modification

Reuse the handbook's earlier principle:

> No implementation should begin until relevant repository evidence has been collected.

### 12.4 Example — `CreateApiEndpoint`

Possible workflow:

1. Identify endpoint conventions.
2. Locate application boundary.
3. Identify request and response contracts.
4. Create endpoint.
5. Create or reuse application command.
6. Add validation.
7. Apply authorization.
8. Add tests.
9. Run scoped tests.
10. Run architecture checks.
11. Report evidence.

---

# 13. Tool Usage

### 13.1 Skills May Require Tools

Potential tool categories:

- repository search,
- file reading,
- editing,
- build tools,
- test runners,
- Git,
- static analysis,
- schema inspection.

### 13.2 Minimum Necessary Tool Principle

A Skill should request only the tools necessary for its workflow.

### 13.3 Read versus Write Tool Boundaries

Classify tools as:

```text
Read
Analyze
Modify
Execute
External
Privileged
```

### 13.4 Tools Do Not Define the Skill

Avoid turning Skills into lists of shell commands.

The workflow remains the abstraction.

### 13.5 External Tools

Special care for:

- deployment tools,
- database migration execution,
- cloud management,
- secrets,
- production resources.

Require explicit approval where appropriate.

---

# 14. Validation

### 14.1 Every Implementation Skill Should Define Validation

A Skill should not consider file creation equivalent to success.

### 14.2 Validation Layers

Possible validation:

- compilation,
- unit tests,
- integration tests,
- architecture tests,
- static analysis,
- contract validation,
- security validation.

### 14.3 Skill Validation versus Validator Agent

Important ownership distinction:

**Skill**

```text
Knows which validation should be executed for its workflow.
```

**Validator Agent**

```text
Independently verifies deterministic evidence and decides
whether deterministic gates pass.
```

### 14.4 Example

`AddTransactionalOutbox` may require:

```text
Build
Outbox persistence tests
Transaction rollback test
Serialization test
Architecture test
```

The Validator Agent independently verifies required evidence.

---

# 15. Failure Handling

### 15.1 Skills Must Define Failure Behavior

Potential failure classes:

- missing prerequisite,
- repository ambiguity,
- tool failure,
- build failure,
- test failure,
- validation failure,
- architectural conflict,
- permission failure.

### 15.2 Fail Explicitly

Avoid:

```text
Continue and hope later steps fix the problem.
```

Prefer:

```text
Failure:
Integration event contract could not be identified.

Action:
Stop before modifying producer or consumer contracts.
```

### 15.3 Recoverable versus Non-Recoverable Failures

Recoverable:

- formatter failure,
- scoped test fixture issue,
- temporarily unavailable local tool.

Non-recoverable:

- unclear aggregate ownership,
- unapproved schema change,
- security policy conflict,
- ambiguous event contract.

### 15.4 Preserve Evidence

Failure output should contain:

- failed step,
- command/tool used,
- error summary,
- affected files,
- recovery recommendation.

---

# 16. Diagram — Claude Skill Lifecycle

```text
┌───────────────────┐
│ Identify Repeated │
│ Engineering Work  │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Design Skill      │
│ Contract          │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Implement Skill   │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Test Skill        │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Review / Approve  │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Publish Skill     │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Discover / Select │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Execute           │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Measure Outcomes  │
└─────────┬─────────┘
          ▼
┌───────────────────┐
│ Propose Evolution │
└───────────────────┘
```

Emphasize:

> Skill evolution should be governed. Repeated success or failure may justify a proposed Skill improvement, but the Harness must not silently rewrite enterprise engineering standards.

---

# 17. Skill Execution Flow

### 17.1 Selection

Claude or the Harness identifies an applicable Skill.

### 17.2 Input Resolution

Resolve:

- Prompt inputs,
- repository evidence,
- Instructions,
- dependencies.

### 17.3 Preconditions

Confirm execution is safe.

### 17.4 Execution

Perform workflow steps.

### 17.5 Validation

Run required checks.

### 17.6 Evidence

Produce structured results.

---

# 18. Diagram — Skill Execution Flow

```text
User Prompt
    │
    ▼
Determine Intent
    │
    ▼
Discover Candidate Skills
    │
    ▼
Select Skill
    │
    ▼
Resolve Inputs
    │
    ▼
Load Repository Instructions
    │
    ▼
Check Preconditions
    │
    ├── Fail ──► Report / Escalate
    │
    ▼
Execute Workflow
    │
    ▼
Run Skill-Level Validation
    │
    ▼
Collect Evidence
    │
    ▼
Return Result
```

---

# 19. Skill Composition

### 19.1 Why Composition Matters

Enterprise features usually require multiple capabilities.

Example:

```text
CreateApiEndpoint
       +
AddApplicationCommand
       +
AddAuthorization
       +
AddUnitAndIntegrationTests
```

### 19.2 Composition Models

#### Sequential

```text
Skill A → Skill B → Skill C
```

#### Conditional

```text
CreateApiEndpoint
       │
       ├── writes domain state?
       │       └── AddApplicationCommand
       │
       ├── restricted endpoint?
       │       └── AddAuthorization
       │
       └── externally visible behavior?
               └── AddIntegrationEvent
```

#### Nested

A high-level Skill may invoke lower-level Skills.

### 19.3 Avoid Mega-Skills

Do not create:

```text
ImplementCompleteEnterpriseFeature
```

with every capability embedded internally.

### 19.4 Composition Contract

A composable Skill should clearly define:

- required inputs,
- outputs,
- side effects,
- dependencies,
- validation status.

---

# 20. Diagram — Skill Composition

```text
                 ┌─────────────────────┐
                 │ Feature Objective   │
                 └─────────┬───────────┘
                           │
               ┌───────────▼────────────┐
               │ Create API Endpoint    │
               └───────────┬────────────┘
                           │
            ┌──────────────┼──────────────┐
            ▼              ▼              ▼
┌──────────────────┐ ┌──────────────┐ ┌──────────────────┐
│ Add Application  │ │ Authorization│ │ Add Tests        │
│ Command          │ │ Skill        │ │ Skill            │
└─────────┬────────┘ └──────────────┘ └──────────────────┘
          │
          ▼
┌──────────────────────┐
│ Add Domain Behavior  │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│ Integration Event    │
└──────────────────────┘
```

---

# 21. Skill Reuse

### 21.1 Repository-Level Reuse

Use the same Skill across:

- Booking,
- Customer,
- Fleet,
- Vehicle,
- Payment,
- Detailing Job.

### 21.2 Organization-Level Reuse

Discuss reusable enterprise Skills shared across repositories.

### 21.3 Generic versus Repository-Specific Skills

Example:

Generic:

```text
AddOpenTelemetry
```

Repository-specific:

```text
AddAlphaCarDetailingTelemetry
```

Prefer generic workflow + repository-derived conventions where possible.

### 21.4 Reuse without Architecture Leakage

A shared Skill should not force patterns inappropriate for another repository.

---

# 22. Skill Discovery

### 22.1 Discovery Problem

As the library grows:

```text
10 Skills → manageable
50 Skills → discovery challenge
200 Skills → governance problem
```

### 22.2 Skill Catalog

Possible metadata:

```text
Name
Description
Category
Tags
Inputs
Preconditions
Dependencies
Owner
Version
```

### 22.3 Categories

Example:

```text
API
Application
Domain
Events
Persistence
Security
Observability
Testing
Infrastructure
```

### 22.4 Discovery by Intent

Example Prompt:

```text
Publish a FleetBookingCancelled event.
```

Likely Skill:

```text
AddIntegrationEvent
```

### 22.5 Discovery by Repository Context

Claude should consider:

- architecture,
- available libraries,
- existing implementation patterns,
- repository policies.

---

# 23. Skill Selection

### 23.1 Selection Is Different from Discovery

Discovery answers:

> Which Skills might apply?

Selection answers:

> Which Skill should be executed?

### 23.2 Selection Criteria

- task intent,
- repository context,
- preconditions,
- capability match,
- version compatibility,
- risk,
- allowed tools.

### 23.3 Ambiguous Selection

Claude should not arbitrarily pick between incompatible Skills.

Example:

```text
AddMessageConsumer
```

Candidate Skills:

```text
AddEventHubConsumer
AddKafkaConsumer
AddServiceBusConsumer
```

Selection should come from repository evidence or explicit task input.

### 23.4 Lead Agent Selection

Within the Harness, Skill selection may be part of Lead Agent planning.

---

# 24. Skill Versioning

### 24.1 Why Version Skills

Workflows evolve because of:

- architecture changes,
- SDK updates,
- framework changes,
- security requirements,
- lessons from production,
- improved validation.

### 24.2 Semantic Versioning Concept

Possible model:

```text
1.0.0
1.1.0
2.0.0
```

### 24.3 Breaking Changes

Examples:

- changed mandatory inputs,
- different persistence strategy,
- new security boundary,
- new required validation gate.

### 24.4 Repository Compatibility

A Skill version should identify compatibility where necessary.

### 24.5 Never Silently Change Workflow Semantics

Enterprise Skills require review and change history.

---

# 25. Skill Testing

### 25.1 Skills Are Engineering Assets

Treat Skills as testable artifacts rather than documentation fragments.

### 25.2 Skill Test Types

#### Contract Tests

Validate required:

- inputs,
- outputs,
- metadata.

#### Scenario Tests

Execute against representative repository fixtures.

#### Positive Tests

Expected implementation succeeds.

#### Negative Tests

Skill correctly rejects invalid preconditions.

#### Regression Tests

Previous working scenarios remain supported.

#### Architecture Tests

Skill output preserves required architecture boundaries.

### 25.3 Golden Scenarios

Maintain representative examples.

Example:

```text
Scenario:
Add CancelBookingCommand

Expected:
- Command created.
- Handler created.
- Aggregate method called.
- Domain invariant preserved.
- Tests generated.
- Build passes.
```

### 25.4 Evaluate Outcome, Not Exact Text

Do not require Claude to produce byte-for-byte identical code.

Test architectural and behavioral expectations.

---

# 26. Skill Ownership and Governance

### 26.1 Skills Need Owners

Potential owners:

- architecture team,
- platform engineering,
- domain team,
- security engineering,
- observability team.

### 26.2 Ownership Responsibilities

Skill owner should review:

- workflow correctness,
- architecture alignment,
- dependencies,
- security,
- version changes,
- deprecation.

### 26.3 Proposed Skill Changes

Developers or the Harness may propose improvements.

### 26.4 Human Approval

No autonomous promotion of changed enterprise standards.

### 26.5 Skill Review Checklist

Review:

- scope,
- duplication,
- preconditions,
- permissions,
- validation,
- failure behavior,
- security,
- test coverage,
- ownership.

---

# 27. Skills Inside the Harness

### 27.1 Harness Role

The handbook defines the Harness as the automation layer responsible for executing prompts, AI roles, validation, evaluation, metrics, and iteration history. AI development handbook

### 27.2 Skills Are Capabilities Used by Agents

Potential model:

```text
Lead Agent
   ↓
Selects capability
   ↓
Developer Agent
   ↓
Executes Skill
```

### 27.3 Skills Do Not Control the Harness

Important distinction:

```text
Harness controls execution.

Agents perform assigned responsibilities.

Skills provide reusable workflows.

Deterministic gates verify.

Evaluators assess outcomes.

Humans approve governed changes.
```

### 27.4 Harness Responsibilities Around Skills

The Harness may:

- expose available Skills,
- record selected Skill/version,
- supply inputs,
- enforce tool boundaries,
- capture execution logs,
- run gates,
- record metrics,
- retain evidence.

### 27.5 Skill Execution Record

Example:

```text
Skill: AddIntegrationEvent
Version: 2.1.0
Agent: Developer
Inputs:
  Event: BookingCancelled
Result: Success
FilesChanged: 5
TestsAdded: 4
Validation: Passed
```

---

# 28. Diagram — Skill Execution Inside the Harness

```text
┌──────────────────────┐
│ User / Work Item     │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Prompt               │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Lead Agent           │
│ Plan + Skill Select  │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Developer Agent      │
│ Executes Skill       │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Skill Workflow       │
│ Tools + Changes      │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Reviewer Agent       │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Validator Agent      │
│ Deterministic Gates  │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Evaluator Agent      │
│ Goal Satisfaction    │
└──────────┬───────────┘
           ▼
┌──────────────────────┐
│ Human Approval       │
└──────────────────────┘
```

---

# 29. Security and Approval Boundaries

### 29.1 Skills Must Not Expand Their Own Authority

A Skill cannot grant itself:

- production access,
- secret access,
- deployment rights,
- database modification permission,
- infrastructure privileges.

### 29.2 Least Privilege

Tool and resource permissions should match the Skill's purpose.

### 29.3 High-Risk Operations

Require explicit controls for:

- production deployments,
- database migrations,
- destructive operations,
- IAM modifications,
- secrets,
- infrastructure provisioning.

### 29.4 Prompt Injection

Skills should not treat arbitrary repository text as trusted instructions.

### 29.5 Untrusted Inputs

Examples:

- issue descriptions,
- external documentation,
- generated files,
- copied logs,
- third-party repositories.

### 29.6 Approval Boundaries

Example:

```text
Skill may generate EF Core migration.

Skill may validate migration.

Skill may NOT apply migration to production automatically.
```

### 29.7 Auditability

Record:

- selected Skill,
- version,
- invoking Agent,
- inputs,
- tools used,
- files changed,
- validations,
- approvals.

---

# 30. Realistic Claude Skill Library for Alpha Car Detailing

## 30.1 Skill — Create API Endpoint

Purpose:

Create an HTTP endpoint following repository API conventions.

Typical workflow:

```text
Discover endpoint pattern
→ define request/response
→ connect application layer
→ authorization
→ error mapping
→ tests
→ validation
```

Example:

```text
POST /api/fleet-bookings/{id}/cancel
```

---

## 30.2 Skill — Add Application Command

Purpose:

Implement a CQRS command workflow.

Possible output:

```text
CancelFleetBookingCommand
CancelFleetBookingCommandHandler
CancelFleetBookingValidator
```

---

## 30.3 Skill — Add Domain Behavior

Purpose:

Add behavior to an aggregate while preserving domain invariants.

Example:

```csharp
booking.Cancel(reason, cancelledBy);
```

Instead of manipulating properties directly.

---

## 30.4 Skill — Add Integration Event

Purpose:

Publish an event representing a completed business fact.

Example:

```text
FleetBookingCancelled
```

Coverage:

- contract,
- producer,
- serialization,
- versioning,
- tests.

---

## 30.5 Skill — Add Transactional Outbox

Purpose:

Persist business changes and integration-event intent atomically.

Workflow includes:

- outbox record,
- serialization,
- transactional persistence,
- dispatcher,
- retry behavior,
- observability,
- tests.

---

## 30.6 Skill — Add Idempotent Consumer

Purpose:

Prevent repeated message processing from producing duplicate business effects.

Possible workflow:

```text
Receive
→ identify message
→ check processed-message store
→ execute business operation
→ record completion
→ acknowledge
```

---

## 30.7 Skill — Add EF Core Persistence

Purpose:

Add repository and persistence configuration consistent with Clean Architecture.

Coverage:

- entity configuration,
- indexes,
- constraints,
- repository integration,
- migration generation,
- tests.

---

## 30.8 Skill — Add Authorization

Purpose:

Protect application behavior and endpoints using repository security conventions.

Example:

```text
FleetManagerPolicy
```

Check:

- authentication,
- authorization policy,
- resource ownership,
- negative tests.

---

## 30.9 Skill — Add OpenTelemetry

Purpose:

Instrument business and technical operations.

Possible outputs:

- ActivitySource usage,
- spans,
- tags,
- metrics,
- trace propagation.

Example business tags:

```text
booking.id
fleet.id
detailing.location
```

Avoid sensitive or high-cardinality values where inappropriate.

---

## 30.10 Skill — Add Unit and Integration Tests

Purpose:

Add tests appropriate to the changed behavior.

Selection may include:

- domain unit tests,
- handler tests,
- API integration tests,
- persistence integration tests,
- event contract tests.

---

# 31. End-to-End Alpha Car Detailing Example

### Scenario

Requirement:

> Fleet managers must be able to cancel an unstarted detailing booking. Cancellation must be authorized, persist the new booking status, and publish a `FleetBookingCancelled` integration event.

### Skill Selection

Potential composition:

```text
CreateApiEndpoint
        ↓
AddApplicationCommand
        ↓
AddDomainBehavior
        ↓
AddAuthorization
        ↓
AddIntegrationEvent
        ↓
AddTransactionalOutbox
        ↓
AddUnitAndIntegrationTests
```

### Expected Execution

Walk through:

- discovery,
- Skill selection,
- execution ordering,
- validations,
- evidence,
- Harness handoff.

### Expected Architecture

```text
API
 ↓
Application Command
 ↓
Domain Aggregate
 ↓
Persistence Transaction
 ├── Booking
 └── Outbox Message
        ↓
Outbox Publisher
        ↓
FleetBookingCancelled
```

---

# 32. Architecture Discussion

### 32.1 Skills as an Engineering Abstraction Layer

```text
Business Objective
       ↓
Prompt
       ↓
Reusable Skill
       ↓
Repository Pattern
       ↓
Code + Tests + Evidence
```

### 32.2 What Belongs in Skills

Good candidates:

- recurring,
- multi-step,
- architecture-sensitive,
- repeatable,
- verifiable.

### 32.3 What Does Not Belong in Skills

Avoid:

- permanent repository policies,
- temporary investigation notes,
- one-off task descriptions,
- secrets,
- business decisions requiring human authority,
- deterministic gate implementations that belong to Harness validation infrastructure.

### 32.4 Skill Granularity

Discuss the trade-off between:

- too many microscopic Skills,
- a small number of enormous Skills.

### 32.5 Skills as Organizational Knowledge

A mature Skill library becomes executable engineering knowledge.

---

# 33. Hands-on Example

Build an example Skill:

```text
AddIntegrationEvent
```

Walk through:

- objective,
- inputs,
- preconditions,
- repository discovery,
- workflow,
- file changes,
- tests,
- validation,
- failure handling,
- output evidence,
- governance metadata.

Apply it to:

```text
FleetBookingCancelled
```

Demonstrate how the same Skill can later support:

```text
VehicleCheckedIn
DetailingCompleted
InvoiceIssued
FleetAccountSuspended
```

without rewriting the workflow.

---

# 34. Claude Example

Demonstrate Claude Code using a reusable Skill.

Example task:

```text
Add FleetBookingCancelled integration event when an
unstarted corporate fleet booking is cancelled.
```

Show Claude conceptually:

1. Reads `CLAUDE.md`.
2. Understands Prompt.
3. Finds `AddIntegrationEvent`.
4. Checks prerequisites.
5. Discovers existing event conventions.
6. Executes workflow.
7. Adds tests.
8. Runs scoped validation.
9. Reports evidence.

Emphasize Claude-first implementation while keeping the engineering principles vendor-neutral, consistent with the project's agreed platform strategy. AI development handbook

---

# 35. GitHub Copilot Comparison

Compare how equivalent reusable engineering guidance can be represented or approximated with GitHub Copilot.

Cover:

- repository Instructions,
- reusable prompt assets,
- agent/workflow features where applicable,
- explicit versus implicit reusable procedure,
- orchestration differences.

Focus on engineering equivalence rather than syntax parity.

Comparison dimensions:

| Capability | Claude | GitHub Copilot |
| --- | --- | --- |
| Persistent repository guidance | | |
| Reusable workflow | | |
| Explicit invocation | | |
| Tool use | | |
| Skill composition | | |
| Harness integration | | |

---

# 36. Codex Comparison

Discuss equivalent concepts when working with Codex-style engineering agents.

Compare:

- reusable engineering procedures,
- repository instructions,
- tool access,
- execution environment,
- orchestration,
- validation integration,
- Harness-controlled execution.

Reinforce:

> The reusable engineering workflow is the enduring concept; the vendor-specific representation may change.

---

# 37. Best Practices

Cover practices such as:

1. Keep one clear responsibility per Skill.
2. Make inputs explicit.
3. Define outputs.
4. Check preconditions.
5. Discover repository patterns before writing code.
6. Reference Instructions rather than duplicate them.
7. Keep Prompts task-specific.
8. Make validation mandatory.
9. Return structured evidence.
10. Keep Skills composable.
11. Use least-privilege tool access.
12. Version Skills.
13. Test Skills.
14. Assign ownership.
15. Record executions in the Harness.
16. Require approval for high-risk operations.
17. Prefer reusable capabilities over feature-specific Skills.
18. Keep vendor-specific mechanics separate from core engineering intent.

---

# 38. Anti-Patterns

### 38.1 The Giant Skill

```text
BuildEntireFeature
```

contains dozens of unrelated workflows.

### 38.2 `CLAUDE.md` Copied into Every Skill

Causes policy duplication and drift.

### 38.3 Task-Specific Skill

```text
FixBookingCancellationBug
```

has no reuse value.

### 38.4 Hidden Inputs

Skill assumes target project, aggregate, or technology.

### 38.5 No Preconditions

Claude begins modifying files before understanding architecture.

### 38.6 No Validation

The Skill reports success after code generation.

### 38.7 Tool Overreach

Skill requests unnecessary shell, cloud, or production privileges.

### 38.8 Silent Architecture Decisions

Skill invents a new pattern because existing architecture was unclear.

### 38.9 Skill-as-Script

Skill becomes nothing more than shell commands.

### 38.10 Skill-as-Agent

Skill starts making planning, review, evaluation, and approval decisions beyond its responsibility.

### 38.11 Unversioned Skill Changes

Shared behavior changes with no audit trail.

### 38.12 Self-Modifying Skill

Skill silently rewrites itself based on an execution outcome.

---

# 39. Architect's Notes

Include notes such as:

### Architect's Note — Skills Are Executable Engineering Knowledge

The real enterprise value is not reduced typing. It is consistent execution of known engineering patterns.

### Architect's Note — Keep Authority Separate from Capability

A Skill may know **how** to perform a workflow without having authority to decide **whether** a high-risk operation is permitted.

### Architect's Note — Avoid Architecture Inside Every Skill

Architecture belongs primarily in Instructions and governed repository knowledge.

### Architect's Note — Skills Should Produce Evidence

Without evidence, Skill execution is difficult to audit, validate, and improve.

### Architect's Note — Skill Libraries Become Platform Assets

As reuse increases, Skills should receive the same engineering discipline as shared libraries.

---

# 40. Enterprise Tips

Include guidance such as:

### Enterprise Tip — Build the Skill Library from Real Repetition

Do not create hundreds of speculative Skills before observing recurring work.

### Enterprise Tip — Start with High-Frequency Workflows

Examples:

```text
Create API endpoint
Add application command
Add tests
Add authorization
Add integration event
```

### Enterprise Tip — Give Every Shared Skill an Owner

### Enterprise Tip — Record Version and Execution Evidence

### Enterprise Tip — Separate Organization and Repository Skills

Organization-wide Skills should remain portable.

### Enterprise Tip — Use Metrics Carefully

Potential metrics:

- Skill usage count,
- success rate,
- validation failure rate,
- average iteration count,
- human rework rate.

Metrics inform improvement rather than automatic policy mutation.

---

# 41. Decision Points

### Decision Point 1 — Should This Be a Skill?

Ask:

```text
Is the workflow recurring?
Is it multi-step?
Does it require engineering knowledge?
Can inputs vary?
Can success be validated?
```

If yes, a Skill may be appropriate.

### Decision Point 2 — Skill or Instruction?

Use Instruction when:

```text
This must always be true.
```

Use Skill when:

```text
This is how recurring work should be performed.
```

### Decision Point 3 — Skill or Prompt?

Use Prompt when:

```text
This describes the current objective.
```

### Decision Point 4 — Skill or Command?

Use Skill for reusable capability.

Use Command when an explicit invocation interface is useful.

### Decision Point 5 — Shared or Repository-Specific Skill?

Evaluate:

- dependency on repository architecture,
- portability,
- organization standards,
- maintenance ownership.

### Decision Point 6 — Compose or Create a New Skill?

Prefer composition when existing Skills already represent the required capabilities.

---

# 42. Exercises

### Exercise 1 — Classify the Artifact

Determine whether each item should be:

- Instruction,
- Skill,
- Prompt,
- Command,
- Harness logic.

Examples:

```text
All API endpoints require authorization.
Create a cancellation endpoint.
Add authorization to an endpoint.
Run all validation gates.
```

### Exercise 2 — Design `CreateApiEndpoint`

Define:

- inputs,
- outputs,
- preconditions,
- workflow,
- validation,
- failure handling.

### Exercise 3 — Build `AddDomainBehavior`

Use Alpha Car Detailing:

```text
Booking.Reschedule(...)
```

### Exercise 4 — Compose Skills

Design a workflow for:

```text
Complete detailing job and publish DetailingCompleted.
```

### Exercise 5 — Add Failure Handling

Define how `AddIntegrationEvent` should behave when the event-contract location cannot be determined.

### Exercise 6 — Add Governance

Create metadata containing:

- owner,
- version,
- approval status,
- dependencies.

### Exercise 7 — Design Skill Tests

Create positive and negative scenarios for:

```text
AddIdempotentConsumer
```

### Exercise 8 — Security Review

Identify which operations within:

```text
AddEfCorePersistence
```

should require explicit human approval.

---

# 43. Interview Questions

Include questions such as:

1. What is a Claude Skill?
2. What makes an engineering workflow suitable for a Skill?
3. How does a Skill differ from `CLAUDE.md`?
4. How does a Skill differ from a Prompt?
5. How does a Skill differ from a Command?
6. What information should a Skill receive as input?
7. Why are preconditions important?
8. What should a Skill return besides source code?
9. How should Skill validation work?
10. How does Skill validation differ from the Validator Agent?
11. What is Skill composition?
12. When should Skills not be composed?
13. How can Claude discover appropriate Skills?
14. How should Skill selection handle ambiguity?
15. Why should Skills be versioned?
16. How can Skills be tested?
17. Who should own enterprise Skills?
18. What role does the Harness play in Skill execution?
19. Why must Skills not expand their own permissions?
20. How should Skill evolution be governed?
21. What is the danger of putting architecture policy inside every Skill?
22. What makes a Skill portable across repositories?
23. How would you design an `AddTransactionalOutbox` Skill?
24. How would you prevent a Skill library from becoming unmanageable?
25. Why are Skills best viewed as executable engineering knowledge?

---

# 44. Chapter Summary

Summarize the major principles:

- A Claude Skill encodes a reusable engineering workflow.
- Skills implement the vendor-neutral Skill concept introduced earlier in the handbook.
- `CLAUDE.md` contains persistent repository Instructions.
- Prompts contain the current task.
- Commands provide invocation surfaces.
- Skills provide reusable execution knowledge.
- Good Skills define:
  - purpose,
  - inputs,
  - outputs,
  - preconditions,
  - workflow,
  - tools,
  - validation,
  - failure handling.
- Skills should be focused and composable.
- Skills should be discoverable and selectable based on task intent and repository evidence.
- Enterprise Skills require:
  - versioning,
  - testing,
  - ownership,
  - governance.
- A Skill does not replace:
  - the Lead Agent,
  - Developer Agent,
  - Reviewer Agent,
  - Validator Agent,
  - Evaluator Agent,
  - deterministic gates,
  - human approval.
- The Harness controls Skill execution and records evidence.
- Skills must operate within explicit security and approval boundaries.
- Mature Skill libraries become reusable, executable engineering knowledge.

---

# 45. Further Reading

Organize references into:

### Claude

- Claude Code documentation.
- Claude Code repository instruction concepts.
- Claude extensibility and reusable workflow documentation.
- Claude tool-use guidance.

### AI Engineering

- reusable Agent capabilities,
- tool-using Agents,
- workflow orchestration,
- capability composition.

### Software Architecture

- Clean Architecture,
- Domain-Driven Design,
- CQRS,
- event-driven architecture,
- transactional outbox,
- idempotent consumers.

### Enterprise Engineering

- reusable engineering platforms,
- internal developer platforms,
- governance of shared engineering assets,
- versioning,
- policy-as-code,
- secure automation.

### Related Handbook Chapters

- Chapter 5 — Repository Discovery
- Chapter 6 — Repository Analysis / Instructions
- Chapter 7 — Skills
- Chapter 8 — Prompts
- Chapter 9 — Roles
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture
- Chapter 14 — Lead Agent
- Chapter 15 — Developer Agent
- Chapter 16 — Reviewer Agent
- Chapter 17 — Validator Agent
- Chapter 18 — Evaluator Agent
- Chapter 22 — `CLAUDE.md`
- Chapter 24 — Claude Commands
- later chapters covering Claude Memory, Tool Calling, and enterprise workflows.

The final published chapter should retain the handbook's frozen chapter structure: story-driven opening, learning objectives, background, concepts, architecture discussion, diagrams, hands-on and platform examples, best practices, anti-patterns, architect guidance, enterprise tips, decision points, exercises, interview questions, summary, and further reading. AI development handbook

**Chapter 23 outline is complete.**
