# Chapter 12 — What Is a Harness?

**Part III — Harness Engineering**
**Target file:** `book/12-what-is-a-harness.md`

---

## 1. Story-driven Opening

### Section title

**From AI-Assisted Coding to Controlled AI Engineering**

### Purpose

Introduce the practical problem that appears when enterprise teams move beyond isolated AI coding requests and begin coordinating multiple agents, tools, checks, approvals, and outputs.

### Key topics

* The Alpha Car Detailing team begins implementing corporate fleet booking.
* A Lead Agent creates the implementation plan.
* A Developer Agent changes the application.
* A Reviewer Agent examines the code.
* A Validator Agent runs builds and tests.
* An Evaluator Agent assesses the completed result.
* Agents produce inconsistent outputs when executed manually.
* Important steps are skipped because no system controls execution order.
* Test failures are not consistently returned to the Developer Agent.
* Architecture findings are recorded in different locations.
* Human approval is requested too late.
* Pull requests are created without complete evidence.
* The team realizes that AI Agents alone do not provide an enterprise development process.
* Introduction of the Harness as the controlled automation layer around AI-assisted engineering.

### Alpha Car Detailing example

The team asks AI Agents to add corporate fleet booking across the Booking, Customer, Pricing, and Notification services. The first attempt produces useful code, but the workflow is fragmented. The Lead Agent creates a plan, the Developer Agent modifies files, and the Reviewer Agent finds architectural violations. However, the build results, review findings, approval status, and final pull request are not coordinated. The team introduces a Harness to control the complete workflow.

### Recommended diagram or table

**Diagram: “Before and After the Harness”**

Before:

```text
Human
 ├── Lead Agent
 ├── Developer Agent
 ├── Reviewer Agent
 ├── Build Scripts
 ├── Test Commands
 └── Pull Request
```

After:

```text
Human
   ↓
Harness
   ├── Planning
   ├── Agent Execution
   ├── Tool Execution
   ├── Validation
   ├── Evaluation
   ├── Approval
   └── Pull Request
```

---

## 2. Learning Objectives

### Section title

**What You Will Learn**

### Purpose

Define the knowledge and architectural decisions readers should understand after completing the chapter.

### Key topics

By the end of the chapter, readers should be able to:

* Define an AI Engineering Harness.
* Explain why AI Agents require a controlled execution environment.
* Distinguish a Harness from an AI Agent.
* Distinguish a Harness from a workflow script.
* Distinguish a Harness from a CI/CD pipeline.
* Distinguish a Harness from an orchestration framework.
* Identify the primary responsibilities of a Harness.
* Describe Harness inputs and outputs.
* Explain how Instructions, Skills, Prompts, Roles, Steering Notes, and Knowledge Sources enter a Harness workflow.
* Explain planning, execution, validation, evaluation, and approval stages.
* Describe retry, failure handling, logging, metrics, permissions, state, and memory.
* Compare single-agent and multi-agent Harness workflows.
* Distinguish local development Harnesses from enterprise Harnesses.
* Explain how Claude Code, GitHub Copilot, and OpenAI Codex can participate in Harness-controlled workflows.
* Identify common Harness design mistakes.
* Recognize which responsibilities belong inside and outside the Harness.

### Alpha Car Detailing example

Readers should be able to describe how a Harness coordinates the complete corporate fleet booking implementation from task intake through pull-request creation.

### Recommended diagram or table

**Table: Learning Objective to Practical Outcome**

| Learning objective                     | Practical outcome                                   |
| -------------------------------------- | --------------------------------------------------- |
| Define a Harness                       | Explain its role to engineering leadership          |
| Identify responsibilities              | Design an initial Harness boundary                  |
| Understand validation and evaluation   | Separate correctness checks from quality assessment |
| Understand state and memory            | Prevent workflow loss and uncontrolled learning     |
| Compare local and enterprise Harnesses | Choose an appropriate adoption path                 |

---

## 3. Background

### Section title

**Why Repository Intelligence Is Not Enough**

### Purpose

Connect Part II, Repository Intelligence, with Part III, Harness Engineering. Explain that context enables informed work, but context alone does not coordinate execution.

### Key topics

* Repository Intelligence gives AI Agents access to:

  * Instructions
  * Skills
  * Prompts
  * Roles
  * Steering Notes
  * Knowledge Sources
* Repository Intelligence answers:

  * What standards apply?
  * How is recurring work performed?
  * What is the current task?
  * Who is responsible?
  * What temporary direction applies?
  * What evidence supports decisions?
* Repository Intelligence does not automatically:

  * Select the next Agent.
  * Sequence work.
  * Execute tools.
  * Run builds or tests.
  * Interpret failures.
  * enforce approval gates.
  * Record execution history.
  * Create pull requests.
* The transition from informed agents to controlled engineering workflows.
* Why manual coordination becomes unreliable as AI usage scales.
* Enterprise requirements for repeatability, traceability, security, and governance.
* Harness Engineering as an independent architectural discipline.
* The Harness as the operational bridge between repository intelligence and engineering delivery.

### Alpha Car Detailing example

The repository already contains architecture Instructions, reusable Skills, role definitions, the fleet-booking Prompt, a release Steering Note, and Knowledge Sources. The missing capability is a controlled process that uses those assets in the correct order and records what happened.

### Recommended diagram or table

**Diagram: “Repository Intelligence to Harness Execution”**

```text
Repository Intelligence
   ├── Instructions
   ├── Skills
   ├── Prompts
   ├── Roles
   ├── Steering Notes
   └── Knowledge Sources
              ↓
           Harness
              ↓
Plan → Execute → Validate → Evaluate → Approve → Record
```

---

## 4. Concepts

# 4.1 What a Harness Is

### Purpose

Establish the formal definition of an AI Engineering Harness.

### Key topics

* A Harness is the controlled automation layer surrounding AI-assisted engineering work.
* It coordinates:

  * Inputs
  * Agents
  * Roles
  * Prompts
  * Tools
  * Validation
  * Evaluation
  * Approvals
  * Metrics
  * Outputs
* It provides process control rather than domain intelligence.
* It does not replace AI Agents.
* It does not replace engineering judgment.
* It does not automatically make AI output correct.
* It creates a governed path through which AI work is performed.
* Core Harness characteristics:

  * Deterministic workflow boundaries
  * Explicit inputs
  * Controlled execution
  * Observable stages
  * Evidence-based gates
  * Recorded outcomes
  * Recoverable state
  * Human authority
* The Harness as an execution system, not merely a collection of scripts.

### Alpha Car Detailing example

The fleet-booking Harness receives the task Prompt, repository Instructions, role definitions, Skills, Steering Note, and Knowledge Sources. It then runs the Lead, Developer, Reviewer, Validator, and Evaluator stages in a controlled sequence.

### Recommended diagram or table

**Definition callout:**

> A Harness is the controlled automation layer that coordinates how AI-assisted engineering work is planned, executed, validated, evaluated, approved, governed, and recorded.

---

# 4.2 Why Enterprise Teams Need a Harness

### Purpose

Explain why direct Agent usage becomes insufficient in enterprise environments.

### Key topics

* Inconsistent Agent execution.
* Skipped validation steps.
* Uncontrolled tool access.
* Missing audit evidence.
* No common retry policy.
* Weak traceability between request and output.
* Inconsistent handling of architecture findings.
* Unclear human approval boundaries.
* Agent-specific workflows that cannot be reused.
* Difficulty measuring AI-assisted engineering performance.
* Lack of separation between generation, review, validation, and evaluation.
* Scaling from individual productivity to organizational capability.
* The Harness as a standard operating model for AI-assisted delivery.
* Benefits:

  * Repeatability
  * Governance
  * Safety
  * Visibility
  * Measurability
  * Recoverability
  * Platform independence

### Alpha Car Detailing example

Without a Harness, each developer uses Claude Code differently when implementing fleet-booking changes. Some run tests, some do not. Some request review, while others create pull requests immediately. A Harness establishes one governed process.

### Recommended diagram or table

**Table: Individual AI Usage versus Enterprise Harness**

| Concern          | Individual Agent usage | Harness-controlled usage |
| ---------------- | ---------------------- | ------------------------ |
| Execution order  | Informal               | Explicit                 |
| Validation       | Optional               | Required by policy       |
| Tool permissions | Agent-dependent        | Centrally controlled     |
| Evidence         | Scattered              | Recorded                 |
| Approval         | Inconsistent           | Gate-based               |
| Retry handling   | Manual                 | Policy-driven            |
| Metrics          | Limited                | Standardized             |

---

# 4.3 Harness versus AI Agent

### Purpose

Clarify that the Harness and AI Agent perform different architectural functions.

### Key topics

* AI Agent:

  * Reasons about work.
  * Produces plans or changes.
  * Uses assigned tools.
  * Operates from a Role.
  * Responds to Prompts and context.
* Harness:

  * Selects or invokes the Agent.
  * Supplies controlled context.
  * Limits permissions.
  * Captures outputs.
  * Decides which stage follows.
  * Executes validation and approval gates.
  * Records the workflow.
* Agent intelligence versus process control.
* One Harness may coordinate many Agents.
* One Agent may operate within different Harnesses.
* The Harness should not absorb the reasoning responsibilities of Agents.
* The Agent should not control the governance responsibilities of the Harness.

### Alpha Car Detailing example

The Reviewer Agent identifies that fleet discount logic was placed inside the API controller. The Harness records the finding, marks the review stage as failed, and returns the work to the Developer Agent.

### Recommended diagram or table

**Table: Harness versus AI Agent**

| Dimension              | AI Agent                       | Harness                                   |
| ---------------------- | ------------------------------ | ----------------------------------------- |
| Primary responsibility | Reasoning and task execution   | Workflow coordination and control         |
| Input                  | Prompt, Role, context, tools   | Task, policies, workflow definition       |
| Output                 | Plan, code, findings, decision | State transition, evidence, final package |
| Authority              | Role-specific                  | Process-specific                          |
| Persistence            | Usually task-scoped            | Workflow-scoped                           |
| Governance             | Follows controls               | Enforces controls                         |

---

# 4.4 Harness versus Workflow Script

### Purpose

Explain why a Harness may use scripts but is broader than a script.

### Key topics

* A workflow script executes a predefined sequence of commands.
* A Harness coordinates agents, decisions, state, evidence, tools, and gates.
* Scripts are implementation mechanisms within a Harness.
* Examples:

  * `lead.ps1`
  * `validate.ps1`
  * `evaluate.ps1`
* A script may:

  * Start an Agent.
  * Run a build.
  * Collect logs.
  * Write a result file.
* A Harness additionally manages:

  * Workflow state
  * Retry policy
  * Agent handoffs
  * Approval status
  * Permissions
  * Metrics
  * Audit evidence
* A collection of scripts is not automatically a Harness.
* Evolution from script-based Harness to service-based Harness.

### Alpha Car Detailing example

`validate.ps1` can run `dotnet build` and `dotnet test`. The Harness decides when validation runs, which commit is tested, what happens after failure, and whether the workflow may proceed.

### Recommended diagram or table

**Diagram: “Scripts Inside the Harness”**

```text
Harness
   ├── lead.ps1
   ├── developer.ps1
   ├── review.ps1
   ├── validate.ps1
   ├── evaluate.ps1
   └── create-pr.ps1
```

---

# 4.5 Harness versus CI/CD Pipeline

### Purpose

Differentiate AI engineering coordination from software delivery automation.

### Key topics

* CI/CD pipeline responsibilities:

  * Compile
  * Test
  * Package
  * Scan
  * Deploy
  * Promote
* Harness responsibilities:

  * Interpret engineering tasks.
  * Coordinate AI Agents.
  * Supply repository context.
  * Manage review and evaluation.
  * Request human approval.
  * Generate or prepare changes.
* Areas of overlap:

  * Builds
  * Tests
  * Static analysis
  * Security scans
  * Pull-request checks
* The Harness may invoke CI/CD pipelines.
* CI/CD may invoke a Harness for selected tasks.
* The Harness should not duplicate mature pipeline capabilities.
* CI/CD validates and delivers software.
* The Harness controls the AI-assisted engineering process that produces the change.
* Recommended separation of concerns.

### Alpha Car Detailing example

The Harness coordinates development of fleet booking and creates a pull request. GitHub Actions then performs organization-standard CI checks. The Harness records the CI result and may block approval if the pipeline fails.

### Recommended diagram or table

**Table: Harness versus CI/CD Pipeline**

| Capability               |    Harness | CI/CD pipeline |
| ------------------------ | ---------: | -------------: |
| Coordinate AI Agents     |        Yes |     Usually no |
| Supply Prompts and Roles |        Yes |             No |
| Run builds and tests     |        Yes |            Yes |
| Deploy applications      | Usually no |            Yes |
| Manage human AI approval |        Yes |        Limited |
| Record Agent findings    |        Yes |             No |
| Package artifacts        |   Optional |            Yes |

---

# 4.6 Harness versus Orchestration Framework

### Purpose

Differentiate the conceptual Harness from the technology used to implement orchestration.

### Key topics

* Orchestration framework as a technical platform.
* Harness as an enterprise engineering capability.
* Possible implementation technologies:

  * PowerShell
  * Bash
  * Python
  * .NET
  * GitHub Actions
  * Azure DevOps
  * Durable Functions
  * Container workflows
  * Agent orchestration libraries
* A Harness may be implemented with an orchestration framework.
* An orchestration framework does not provide enterprise engineering semantics by default.
* Harness-specific concepts:

  * Engineering Roles
  * Repository Instructions
  * Skills
  * Validation gates
  * Evaluation criteria
  * Human authority
  * Pull-request evidence
* Avoiding platform-driven architecture.
* Starting with explicit engineering requirements before selecting a framework.

### Alpha Car Detailing example

The team may initially implement the fleet-booking Harness with PowerShell and Markdown files. Later, it may migrate to a .NET orchestration service without changing the underlying roles, gates, and governance model.

### Recommended diagram or table

**Decision table: Harness Concept versus Implementation Technology**

---

# 4.7 Harness Responsibilities

### Purpose

Define the responsibility boundary of a well-designed Harness.

### Key topics

* Task intake.
* Context assembly.
* Planning coordination.
* Role assignment.
* Agent invocation.
* Tool invocation.
* Output capture.
* Validation.
* Evaluation.
* Human approval.
* Retry and recovery.
* Logging.
* Audit trails.
* Metrics.
* Permissions.
* State management.
* Memory access.
* Artifact management.
* Pull-request preparation.
* Notification and status reporting.
* Explicit non-responsibilities:

  * Replacing human accountability
  * Silently changing Instructions
  * Making unapproved production deployments
  * Granting itself new permissions
  * Treating Agent confidence as validation

### Alpha Car Detailing example

The Harness controls the workflow for adding fleet booking but does not decide unilaterally whether a new discount policy is legally or commercially acceptable.

### Recommended diagram or table

**Table: In-Scope and Out-of-Scope Harness Responsibilities**

---

# 4.8 Harness Inputs and Outputs

### Purpose

Explain the data and artifacts entering and leaving a Harness workflow.

### Key topics

#### Inputs

* Task Prompt
* Repository Instructions
* Skills
* Roles
* Steering Notes
* Knowledge Sources
* Source code
* Branch and commit information
* Tool configuration
* Permission policy
* Acceptance criteria
* Evaluation rubric
* Human decisions
* Prior workflow state

#### Intermediate outputs

* Execution plan
* Agent responses
* Code changes
* Build logs
* Test results
* Review findings
* Architecture findings
* Security findings
* Retry records
* Approval requests
* Evaluation scores

#### Final outputs

* Approved code changes

* Pull request

* Evidence package

* Audit trail

* Metrics

* Final workflow status

* Lessons or proposed improvements

* Structured versus unstructured outputs.

* Machine-readable contracts.

* File naming and location conventions.

* Output integrity and provenance.

### Alpha Car Detailing example

The Harness accepts the fleet-booking Prompt and related repository context. It produces code changes, validation reports, review findings, an evaluation summary, an approval record, and a pull request.

### Recommended diagram or table

**Diagram: Harness Input–Process–Output Model**

---

# 4.9 Repository Intelligence Assets Inside the Harness

### Purpose

Show how the concepts from Chapters 6–11 participate in Harness execution.

### Key topics

* Instructions define persistent standards.
* Skills define reusable procedures.
* Prompts define the current task.
* Roles define responsibility and authority.
* Steering Notes define temporary direction.
* Knowledge Sources provide evidence and context.
* The Harness coordinates when and how these assets are loaded.
* Context precedence and conflict handling.
* Required versus optional context.
* Scope-aware context assembly.
* Avoiding unrestricted context loading.
* Recording which sources influenced a decision.
* Context versioning for reproducibility.

### Alpha Car Detailing example

The fleet-booking workflow loads:

* Architecture and coding Instructions.
* The `create-rest-endpoint` and `add-integration-event` Skills.
* The fleet-booking Prompt.
* Lead, Developer, Reviewer, Validator, and Evaluator Roles.
* The current release Steering Note.
* Booking service ADRs and API specifications as Knowledge Sources.

### Recommended diagram or table

**Table: Repository Asset and Harness Usage**

| Asset             | Defines                      | Harness usage                     |
| ----------------- | ---------------------------- | --------------------------------- |
| Instructions      | Persistent standards         | Added to applicable Agent context |
| Skills            | Reusable workflow            | Invoked during implementation     |
| Prompts           | Current task                 | Starts the workflow               |
| Roles             | Responsibility and authority | Controls Agent behavior           |
| Steering Notes    | Temporary direction          | Constrains current mission        |
| Knowledge Sources | Evidence and context         | Supports decisions                |

---

# 4.10 Planning

### Purpose

Explain planning as a controlled Harness stage rather than an informal Agent response.

### Key topics

* Task interpretation.
* Scope identification.
* Repository discovery confirmation.
* Required files and services.
* Dependency identification.
* Risk assessment.
* Work breakdown.
* Role assignment.
* Validation plan.
* Evaluation plan.
* Approval points.
* Plan review before execution.
* Plan versioning.
* Plan acceptance criteria.
* Difference between an Agent-generated plan and a Harness-approved plan.
* Preventing execution before planning evidence exists.

### Alpha Car Detailing example

The Lead Agent identifies that fleet booking affects Booking, Customer, Pricing, and Notification services. The Harness verifies that the plan includes database changes, API changes, event contracts, tests, and architecture review.

### Recommended diagram or table

**Table: Fleet Booking Implementation Plan**

| Step                       | Owner           | Inputs                | Output                   | Gate                     |
| -------------------------- | --------------- | --------------------- | ------------------------ | ------------------------ |
| Discover impacted services | Lead Agent      | Repository            | Scope report             | Scope accepted           |
| Implement booking flow     | Developer Agent | Plan and Skills       | Code changes             | Build                    |
| Review architecture        | Reviewer Agent  | Diff and Instructions | Findings                 | No critical violations   |
| Validate solution          | Validator Agent | Source branch         | Test evidence            | All required checks pass |
| Evaluate result            | Evaluator Agent | Complete evidence     | Score and recommendation | Threshold met            |

---

# 4.11 Agent Execution

### Purpose

Describe how the Harness invokes Agents using explicit contracts.

### Key topics

* Agent identity.
* Role assignment.
* Prompt construction.
* Context selection.
* Tool access.
* Execution timeout.
* Output schema.
* Completion conditions.
* Isolation between Agents.
* Agent handoff packages.
* Avoiding hidden conversational dependency.
* Capturing Agent reasoning summaries without depending on private internal reasoning.
* Deterministic metadata around non-deterministic outputs.
* Agent version and model metadata.
* Handling partial Agent output.
* Agent cancellation.

### Alpha Car Detailing example

The Developer Agent receives only the approved plan, applicable Instructions, selected Skills, scoped Knowledge Sources, and permitted repository tools.

### Recommended diagram or table

**Agent Execution Contract**

```text
Agent Role
Task Prompt
Allowed Context
Allowed Tools
Expected Output
Completion Criteria
Failure Conditions
```

---

# 4.12 Tool Execution

### Purpose

Explain controlled access to commands, repositories, files, tests, and external systems.

### Key topics

* Tool categories:

  * Read-only repository tools
  * File modification tools
  * Build tools
  * Test tools
  * Static analysis tools
  * Git tools
  * Pull-request tools
  * Deployment tools
* Least-privilege access.
* Tool allowlists and denylists.
* Command validation.
* Working-directory restrictions.
* Secret protection.
* Network restrictions.
* Timeouts.
* Output capture.
* Tool error classification.
* Approval before high-impact tools.
* Sandboxed execution.
* Separating Agent request from Harness authorization.
* Tool provenance and auditability.

### Alpha Car Detailing example

The Developer Agent may edit files and run local tests. It cannot merge branches, access production secrets, or deploy the Booking service. Pull-request creation occurs only after approval.

### Recommended diagram or table

**Table: Tool Permission Matrix by Role**

| Tool            |     Lead | Developer | Reviewer | Validator |                 Evaluator |
| --------------- | -------: | --------: | -------: | --------: | ------------------------: |
| Read repository |      Yes |       Yes |      Yes |       Yes |                       Yes |
| Modify code     |       No |       Yes |       No |        No |                        No |
| Run build       | Optional |       Yes | Optional |       Yes |                  Optional |
| Run tests       | Optional |       Yes | Optional |       Yes |                  Optional |
| Create PR       |       No |        No |       No |        No | Requested through Harness |
| Merge PR        |       No |        No |       No |        No |                        No |

---

# 4.13 Validation

### Purpose

Define validation as objective verification that required checks pass.

### Key topics

* Validation answers: “Does the result satisfy measurable technical checks?”
* Build validation.
* Unit tests.
* Integration tests.
* Contract tests.
* Static analysis.
* Formatting.
* Security scanning.
* Architecture tests.
* Dependency checks.
* Schema validation.
* Migration validation.
* Required versus advisory checks.
* Evidence capture.
* Exit codes and machine-readable results.
* Validation failure routing.
* Difference between Agent claims and executed proof.

### Alpha Car Detailing example

The Validator Agent and Harness run:

* `dotnet build`
* Unit tests for fleet booking
* Integration tests for corporate customer pricing
* Architecture tests for Clean Architecture dependencies
* Event schema validation
* Security checks for authorization policies

### Recommended diagram or table

**Table: Validation Evidence**

| Check          | Evidence                 | Pass condition           |
| -------------- | ------------------------ | ------------------------ |
| Build          | Build log                | Zero errors              |
| Unit tests     | Test result file         | All required tests pass  |
| Architecture   | Architecture test output | No forbidden dependency  |
| Event contract | Schema validation report | Valid versioned contract |
| Security       | Scan report              | No blocking findings     |

---

# 4.14 Evaluation

### Purpose

Differentiate evaluation from mechanical validation.

### Key topics

* Evaluation answers: “Is the result good enough, appropriate, complete, and aligned with expectations?”
* Quality assessment.
* Architecture alignment.
* Maintainability.
* Completeness.
* Security reasoning.
* Operational readiness.
* Test quality.
* Documentation quality.
* Acceptance-criteria coverage.
* Rubrics and scorecards.
* Qualitative findings.
* Evaluator independence.
* Threshold-based decisions.
* Evaluation confidence.
* Human review of ambiguous findings.
* Validation may pass while evaluation fails.

### Alpha Car Detailing example

The code builds and all tests pass, but the Evaluator Agent finds that fleet discounts are hard-coded and not extensible for government departments. Validation passes; evaluation does not meet the quality threshold.

### Recommended diagram or table

**Table: Validation versus Evaluation**

| Dimension        | Validation                | Evaluation                                |
| ---------------- | ------------------------- | ----------------------------------------- |
| Primary question | Did required checks pass? | Is the solution acceptable?               |
| Evidence         | Builds, tests, scanners   | Rubrics, findings, architectural judgment |
| Nature           | Mostly objective          | Partly judgment-based                     |
| Typical owner    | Validator Agent/tools     | Evaluator Agent/human reviewer            |
| Outcome          | Pass/fail                 | Score, recommendation, findings           |

---

# 4.15 Human Approval

### Purpose

Establish human authority as an explicit Harness control.

### Key topics

* Approval gates.
* Risk-based approval.
* Required approver roles.
* Approval evidence.
* Approve, reject, or request changes.
* Approval expiration.
* Segregation of duties.
* Escalation.
* High-impact actions requiring human approval:

  * Architecture changes
  * Security-sensitive changes
  * Data model changes
  * Public API changes
  * Production deployments
  * Merges
  * Changes to Instructions or Skills
* The Harness never silently changes engineering standards.
* Human approval for proposed Harness learning.
* Avoiding approval fatigue.
* Approval summaries with evidence.

### Alpha Car Detailing example

A Technical Lead must approve the new fleet-pricing model because it affects business rules and a public API contract. The Harness blocks pull-request creation until approval is recorded.

### Recommended diagram or table

**Diagram: Human Approval Gate**

```text
Evaluation Complete
        ↓
Approval Request
   ├── Approve → Create PR
   ├── Request Changes → Developer
   └── Reject → Close Workflow
```

---

# 4.16 Retry and Failure Handling

### Purpose

Explain how the Harness responds predictably to Agent, tool, validation, and infrastructure failures.

### Key topics

* Failure categories:

  * Agent failure
  * Tool failure
  * Build failure
  * Test failure
  * Review failure
  * Evaluation failure
  * Permission failure
  * Infrastructure failure
  * Timeout
  * Invalid output
* Retriable versus non-retriable failures.
* Maximum retry count.
* Backoff.
* Retry with additional evidence.
* Returning findings to the responsible Agent.
* Avoiding infinite loops.
* Preserving failed attempts.
* Human escalation.
* Partial progress recovery.
* Idempotent workflow stages.
* Compensation and cleanup.
* Poisoned workflow state.
* Terminal status definitions.

### Alpha Car Detailing example

If fleet-booking tests fail, the Harness returns the exact test evidence to the Developer Agent. After two unsuccessful repair attempts, the workflow pauses and requests human intervention.

### Recommended diagram or table

**Failure Handling State Diagram**

```text
Stage Failed
   ↓
Classify Failure
   ├── Retriable → Retry
   ├── Repairable → Return to Responsible Agent
   ├── Approval Needed → Human Gate
   └── Terminal → Stop and Record
```

---

# 4.17 Logging and Audit Trails

### Purpose

Define the evidence required to reconstruct what the Harness did.

### Key topics

* Difference between operational logs and audit records.
* Workflow identifier.
* Correlation identifier.
* Task origin.
* Agent Role.
* Model and tool metadata.
* Context sources used.
* Commands executed.
* Files changed.
* Validation results.
* Review findings.
* Approval decisions.
* State transitions.
* Retry history.
* Pull-request identifier.
* Timestamp integrity.
* Tamper resistance.
* Secret and sensitive-data redaction.
* Retention policy.
* Searchability.
* Compliance use cases.
* Reproducing an AI-assisted change.

### Alpha Car Detailing example

The audit trail shows which Instructions, Skills, Steering Note, and Knowledge Sources were used when the fleet-booking pull request was created.

### Recommended diagram or table

**Table: Minimum Audit Record**

| Field        | Example                                         |
| ------------ | ----------------------------------------------- |
| Workflow ID  | `fleet-booking-2026-0012`                       |
| Task         | Add corporate fleet booking                     |
| Branch       | `feature/corporate-fleet-booking`               |
| Agents       | Lead, Developer, Reviewer, Validator, Evaluator |
| Tools        | Git, .NET CLI, test runner                      |
| Approval     | Technical Lead approved                         |
| Final output | Pull request reference                          |

---

# 4.18 Metrics

### Purpose

Introduce measurements that show Harness reliability, efficiency, and engineering value.

### Key topics

* Workflow completion rate.
* First-pass validation rate.
* Retry count.
* Agent failure rate.
* Tool failure rate.
* Validation duration.
* Evaluation duration.
* Approval wait time.
* Total lead time.
* Human intervention rate.
* Rework rate.
* Findings by category.
* Pull-request acceptance rate.
* Escaped defect rate.
* Cost and token usage.
* Context size.
* Tool usage.
* Quality score trends.
* Metrics for improvement, not individual surveillance.
* Avoiding vanity metrics.
* Establishing baselines.
* Separating productivity from quality.
* Metrics as evidence for Harness evolution.

### Alpha Car Detailing example

The team records how many fleet-booking implementation attempts were needed, which validation stage failed, how long approval took, and whether the final pull request required manual correction.

### Recommended diagram or table

**Table: Initial Harness Metrics**

| Metric                     | Why it matters                         |
| -------------------------- | -------------------------------------- |
| Workflow completion rate   | Measures reliability                   |
| First-pass validation rate | Reveals initial implementation quality |
| Average retries            | Shows rework                           |
| Human intervention rate    | Shows automation maturity              |
| Evaluation acceptance rate | Measures output suitability            |
| Lead time                  | Measures end-to-end efficiency         |

---

# 4.19 Security and Permissions

### Purpose

Explain how the Harness constrains Agent and tool authority.

### Key topics

* Least privilege.
* Role-based permissions.
* Repository scope.
* Branch protection.
* Secret isolation.
* Credential management.
* Environment separation.
* Network controls.
* Read versus write access.
* Command execution policy.
* Pull-request versus merge permission.
* Production deployment restrictions.
* Prompt injection resistance.
* Untrusted repository content.
* Knowledge Source trust levels.
* Tool output sanitization.
* Data exfiltration risk.
* Security audit records.
* Approval for privilege escalation.
* Temporary credentials.
* Permission revocation.
* Policy enforcement independent of Agent instructions.

### Alpha Car Detailing example

The Harness allows the Developer Agent to modify only the fleet-booking branch. It prevents access to production credentials and rejects commands attempting to modify protected deployment infrastructure.

### Recommended diagram or table

**Security Boundary Diagram**

```text
AI Agent
   ↓ request
Harness Authorization
   ↓ approved action
Restricted Tool
   ↓
Scoped Repository or Environment
```

---

# 4.20 Harness State

### Purpose

Explain how the Harness tracks the current position and status of a workflow.

### Key topics

* Workflow identifier.
* Current stage.
* Current owner.
* Branch and commit.
* Attempt number.
* Stage status.
* Pending findings.
* Validation result.
* Evaluation result.
* Approval status.
* Artifacts produced.
* Timestamps.
* Terminal states.
* Persistent versus in-memory state.
* Recovery after interruption.
* State transitions.
* Optimistic concurrency.
* Idempotency.
* State ownership.
* Avoiding state hidden only inside Agent conversations.

### Alpha Car Detailing example

The fleet-booking workflow state records that development is complete, review found two issues, one issue remains unresolved, validation has not started, and approval is pending.

### Recommended diagram or table

**Harness Workflow State Machine**

```text
Created
  ↓
Planned
  ↓
In Development
  ↓
In Review
  ↓
In Validation
  ↓
In Evaluation
  ↓
Awaiting Approval
  ↓
Completed
```

Alternative states:

```text
Failed
Paused
Cancelled
Changes Requested
```

---

# 4.21 Harness Memory

### Purpose

Introduce Harness Memory while preserving the distinction between workflow state, repository knowledge, and future self-learning capabilities.

### Key topics

* State records what is happening now.
* Memory preserves relevant information across workflows.
* Examples:

  * Common build failures
  * Repeated review findings
  * Successful remediation patterns
  * Prior evaluation outcomes
  * Approved implementation decisions
* Memory should not replace authoritative Knowledge Sources.
* Memory quality and provenance.
* Expiration and freshness.
* Privacy and security.
* Approved versus unapproved memory.
* Retrieval scope.
* Avoiding accidental reinforcement of bad outputs.
* The Harness never silently changes Instructions, Skills, or standards.
* Memory may support recommendations.
* Human approval remains mandatory for governed changes.
* Preview of Part VI, Self-Learning AI Systems.

### Alpha Car Detailing example

The Harness remembers that previous Booking service changes failed architecture tests when controllers referenced Infrastructure directly. It may warn the Developer Agent, but it cannot silently rewrite the architecture Instructions.

### Recommended diagram or table

**Table: State versus Memory versus Knowledge Sources**

| Concept          | Primary purpose                  | Lifetime                    | Authority                   |
| ---------------- | -------------------------------- | --------------------------- | --------------------------- |
| Harness State    | Track current workflow           | Workflow duration           | Operational                 |
| Harness Memory   | Preserve useful prior experience | Across workflows            | Advisory unless approved    |
| Knowledge Source | Provide trusted evidence         | Source-controlled lifecycle | Defined by source authority |

---

# 4.22 Single-Agent Workflows

### Purpose

Show that a Harness remains useful even when only one AI Agent performs the work.

### Key topics

* One Agent with multiple controlled stages.
* Prompt execution.
* Tool control.
* Validation.
* Approval.
* Logging.
* Suitable use cases:

  * Small code changes
  * Documentation updates
  * Test generation
  * Isolated refactoring
* Benefits:

  * Lower complexity
  * Easier adoption
  * Reduced coordination overhead
* Limitations:

  * Weak role separation
  * Self-review bias
  * Less independent evaluation
* When to remain single-agent.
* When to evolve to multi-agent.

### Alpha Car Detailing example

A single Claude Code Agent adds validation to an existing fleet-booking request model. The Harness still runs tests, records evidence, and requires approval.

### Recommended diagram or table

**Diagram: Single-Agent Harness**

```text
Prompt
  ↓
Harness
  ↓
Developer Agent
  ↓
Validation
  ↓
Approval
```

---

# 4.23 Multi-Agent Workflows

### Purpose

Explain how role separation improves review quality, accountability, and control.

### Key topics

* Lead Agent.
* Developer Agent.
* Reviewer Agent.
* Validator Agent.
* Evaluator Agent.
* Architect Agent.
* Security Reviewer.
* Handoff contracts.
* Shared state versus isolated context.
* Independent review.
* Agent disagreement.
* Conflict resolution.
* Parallel versus sequential execution.
* Avoiding unnecessary Agent proliferation.
* Cost and latency trade-offs.
* Clear authority boundaries.
* Human escalation.

### Alpha Car Detailing example

The Lead Agent plans fleet booking, the Developer Agent implements it, the Reviewer Agent checks maintainability, the Validator Agent runs technical checks, and the Evaluator Agent assesses the overall solution.

### Recommended diagram or table

**Diagram: Multi-Agent Harness Workflow**

```text
Task
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
Human Approval
 ↓
Pull Request
```

---

# 4.24 Local Harnesses

### Purpose

Describe lightweight Harnesses designed for individual developers or small teams.

### Key topics

* Runs on a developer workstation.
* File-based state.
* Markdown-based roles and prompts.
* PowerShell or Bash scripts.
* Local Git branch.
* Local build and test execution.
* Manual approval.
* Limited concurrency.
* Fast experimentation.
* Low operational overhead.
* Suitable starting point.
* Risks:

  * Local machine inconsistency
  * Limited central auditability
  * Credential exposure
  * Weak shared visibility
* Path from local to team-managed Harness.

### Alpha Car Detailing example

A developer runs `lead.ps1`, `validate.ps1`, and `evaluate.ps1` locally while implementing fleet booking on a feature branch.

### Recommended diagram or table

**Table: Local Harness Characteristics**

---

# 4.25 Enterprise Harnesses

### Purpose

Explain the capabilities needed when Harness execution becomes an organizational service.

### Key topics

* Centralized execution.
* Shared workflow service.
* Persistent state store.
* Queue-based execution.
* Agent worker pools.
* Enterprise identity.
* Policy enforcement.
* Central secret management.
* Approval integration.
* Audit retention.
* Metrics dashboards.
* Multi-repository support.
* Concurrency.
* Cost controls.
* Tenant or team isolation.
* Resilience.
* Disaster recovery.
* Versioned workflow definitions.
* Governance boards.
* Platform team ownership.
* Integration with GitHub, Azure DevOps, CI/CD, and enterprise observability.

### Alpha Car Detailing example

The enterprise Harness executes fleet-booking workflows in isolated containers, stores state centrally, integrates with GitHub pull requests, and requires Technical Lead approval.

### Recommended diagram or table

**Table: Local versus Enterprise Harness**

| Dimension  | Local Harness           | Enterprise Harness                     |
| ---------- | ----------------------- | -------------------------------------- |
| Execution  | Developer machine       | Managed platform                       |
| State      | Files or local database | Central persistent store               |
| Identity   | Developer credentials   | Managed identities/service accounts    |
| Audit      | Local logs              | Central immutable records              |
| Scale      | One or few workflows    | Concurrent organization-wide workflows |
| Governance | Team conventions        | Enforced enterprise policies           |

---

## 5. Architecture Discussion

# 5.1 Logical Harness Architecture

### Purpose

Present the main logical components of a Harness without committing to a specific implementation platform.

### Key topics

* Task intake.
* Context assembler.
* Workflow coordinator.
* Agent adapter.
* Tool gateway.
* Validation engine.
* Evaluation engine.
* Approval gateway.
* State store.
* Memory store.
* Artifact store.
* Audit store.
* Metrics collector.
* Pull-request integration.
* Notification integration.
* Policy engine.
* Security boundary.
* Component responsibilities and interactions.

### Alpha Car Detailing example

The Task Intake component receives the fleet-booking request. The Context Assembler loads repository intelligence. The Workflow Coordinator runs the Agent sequence. The Validation Engine runs .NET checks. The Approval Gateway requests Technical Lead approval. The Pull Request Adapter creates the final pull request.

### Recommended diagram or table

**Primary Logical Architecture Diagram**

```text
Developer / Product Owner
          ↓
      Task Intake
          ↓
   Workflow Coordinator
    ├── Context Assembler
    ├── Agent Adapter
    ├── Tool Gateway
    ├── Validation Engine
    ├── Evaluation Engine
    ├── Approval Gateway
    └── PR Integration
          ↓
 State / Memory / Artifacts / Audit / Metrics
```

---

# 5.2 Harness Control Plane and Execution Plane

### Purpose

Separate workflow governance from actual Agent and tool execution.

### Key topics

* Control plane:

  * Workflow definition
  * Policies
  * Scheduling
  * State transitions
  * Permissions
  * Approval
  * Metrics
* Execution plane:

  * Agent invocation
  * Repository operations
  * Builds
  * Tests
  * Scans
  * Artifact generation
* Isolation.
* Scaling.
* Failure boundaries.
* Security benefits.
* Local simplification versus enterprise separation.
* Avoiding direct Agent access to control-plane data.

### Alpha Car Detailing example

The Harness control plane authorizes the Developer Agent to work on the fleet-booking branch. An isolated execution worker performs code changes and tests.

### Recommended diagram or table

**Diagram: Control Plane and Execution Plane**

---

# 5.3 Harness Workflow Lifecycle

### Purpose

Define the standard end-to-end lifecycle of a Harness-managed task.

### Key topics

1. Task submitted.
2. Inputs validated.
3. Repository context assembled.
4. Plan generated.
5. Plan approved or accepted.
6. Development executed.
7. Review completed.
8. Validation executed.
9. Evaluation completed.
10. Human approval requested.
11. Pull request created.
12. Metrics and evidence recorded.
13. Workflow completed or failed.

* Re-entry points.
* Retry loops.
* Cancellation.
* Manual intervention.
* Lifecycle invariants.

### Alpha Car Detailing example

Use the complete corporate fleet-booking workflow as the lifecycle walkthrough.

### Recommended diagram or table

**End-to-End Sequence Diagram**

---

# 5.4 Harness Boundaries

### Purpose

Prevent the Harness from becoming an uncontrolled enterprise platform containing every engineering concern.

### Key topics

* What belongs inside:

  * AI workflow control
  * Agent coordination
  * AI-specific context assembly
  * Evidence and approval
* What remains external:

  * Source-control platform
  * CI/CD platform
  * Production deployment system
  * Identity provider
  * Secret store
  * Observability platform
  * Issue tracker
* Integration through explicit adapters.
* Avoiding duplication.
* Avoiding vendor lock-in.
* Maintaining replaceable Agent providers.

### Alpha Car Detailing example

The Harness creates a GitHub pull request but does not replace GitHub. It invokes GitHub Actions but does not become the deployment pipeline.

### Recommended diagram or table

**Context Diagram: Harness and Enterprise Systems**

---

# 5.5 Agent and Tool Abstraction

### Purpose

Explain how a vendor-neutral Harness avoids coupling workflows directly to a single AI platform.

### Key topics

* Agent provider adapters.
* Model configuration.
* Prompt packaging.
* Tool invocation abstraction.
* Provider-specific capabilities.
* Common execution contract.
* Fallback and provider switching.
* Output normalization.
* Capability discovery.
* Cost and policy controls.
* Vendor-neutral engineering workflow with provider-specific implementation.

### Alpha Car Detailing example

The same validation and approval workflow can surround Claude Code, GitHub Copilot, or Codex, even though each platform exposes different execution capabilities.

### Recommended diagram or table

**Adapter Diagram**

```text
Harness Agent Contract
   ├── Claude Code Adapter
   ├── GitHub Copilot Adapter
   └── OpenAI Codex Adapter
```

---

# 5.6 State, Memory, and Artifact Storage

### Purpose

Define separate storage responsibilities.

### Key topics

* Workflow state store.
* Harness Memory store.
* Artifact store.
* Audit store.
* Metrics store.
* Source repository.
* Data ownership.
* Retention.
* Versioning.
* Consistency.
* Encryption.
* Backup.
* Avoiding one undifferentiated database.
* File-based local implementation.
* Managed enterprise implementation.

### Alpha Car Detailing example

Fleet-booking state is stored in the workflow store, build logs in the artifact store, approval records in the audit store, and recurring lessons in Harness Memory.

### Recommended diagram or table

**Storage Responsibility Matrix**

---

# 5.7 Failure Domains and Recovery

### Purpose

Show how architecture isolates failures and supports resumable workflows.

### Key topics

* Agent provider outage.
* Tool worker failure.
* State store failure.
* Repository conflict.
* Build infrastructure failure.
* Approval timeout.
* Pull-request API failure.
* Idempotent retries.
* Checkpoints.
* Stage replay.
* Recovery point.
* Manual restart.
* Dead-letter or failed-work queue.
* Preventing duplicate pull requests.
* Preserving audit consistency.

### Alpha Car Detailing example

The workflow completes evaluation but fails while creating the pull request. The Harness resumes from the pull-request stage without rerunning development.

### Recommended diagram or table

**Table: Failure Domain and Recovery Strategy**

---

## 6. Professional Diagrams

### Section title

**Visual Models for Harness Engineering**

### Purpose

Define the professional diagrams that should appear in the chapter and the architectural questions each diagram answers.

### Key topics and recommended diagrams

#### Diagram 1: Repository Intelligence and Harness Relationship

Shows how Instructions, Skills, Prompts, Roles, Steering Notes, and Knowledge Sources feed the Harness.

#### Diagram 2: Harness Logical Architecture

Shows task intake, context assembly, workflow coordination, Agent adapters, validation, evaluation, approval, state, audit, metrics, and pull-request integration.

#### Diagram 3: End-to-End Multi-Agent Workflow

Shows Lead, Developer, Reviewer, Validator, Evaluator, human approval, and pull-request creation.

#### Diagram 4: Harness State Machine

Shows normal states, retry states, paused states, failed states, and completed states.

#### Diagram 5: Control Plane and Execution Plane

Shows governance services separated from Agent and tool execution workers.

#### Diagram 6: Tool Authorization Boundary

Shows that Agents request tools but the Harness authorizes execution.

#### Diagram 7: Validation versus Evaluation

Shows objective technical checks and qualitative assessment as separate stages.

#### Diagram 8: Local versus Enterprise Harness

Shows the evolutionary path from scripts and local files to managed services and centralized state.

#### Diagram 9: Claude, Copilot, and Codex Adapters

Shows a vendor-neutral Harness contract with provider-specific integrations.

#### Diagram 10: Alpha Car Detailing Fleet Booking Workflow

Shows the complete example from Prompt to pull request.

### Alpha Car Detailing example

All diagrams should use fleet-booking terminology, repository names, Agent Roles, build commands, and pull-request outputs where practical.

### Recommended table

**Diagram Inventory**

| Diagram                             | Architectural question                 |
| ----------------------------------- | -------------------------------------- |
| Repository Intelligence and Harness | Where does context enter the workflow? |
| Logical Architecture                | What components form a Harness?        |
| Multi-Agent Workflow                | How are Roles coordinated?             |
| State Machine                       | How is progress controlled?            |
| Tool Authorization                  | How are permissions enforced?          |
| Local versus Enterprise             | How does adoption mature?              |

---

## 7. Hands-on Example

### Section title

**Building a Minimal Fleet Booking Harness**

### Purpose

Provide a practical, incremental example of a lightweight Harness that coordinates the addition of corporate fleet booking.

### Key topics

* Example repository structure.
* Harness folders and files.
* Task definition.
* Role prompts.
* Steering Note.
* Knowledge Source references.
* Workflow state file.
* PowerShell scripts.
* Build and test commands.
* Review findings.
* Evaluation rubric.
* Approval record.
* Pull-request preparation.
* The example should be understandable without introducing a full orchestration platform.

### Suggested repository structure

```text
harness/
├── README.md
├── workflows/
│   └── corporate-fleet-booking.yaml
├── roles/
│   ├── lead.md
│   ├── developer.md
│   ├── reviewer.md
│   ├── validator.md
│   └── evaluator.md
├── scripts/
│   ├── lead.ps1
│   ├── develop.ps1
│   ├── review.ps1
│   ├── validate.ps1
│   ├── evaluate.ps1
│   └── create-pr.ps1
├── state/
│   └── corporate-fleet-booking.json
├── results/
│   ├── plan.md
│   ├── review-findings.md
│   ├── validation-result.json
│   └── evaluation-result.json
└── approvals/
    └── approval-request.md
```

### Suggested walkthrough

#### Step 1: Define the task

* Add corporate fleet booking.
* Support approved corporate customers.
* Apply fleet pricing.
* Publish a booking-created event.
* Add tests.
* Preserve existing walk-in booking behavior.

#### Step 2: Load repository intelligence

* Instructions.
* Skills.
* Prompt.
* Roles.
* Steering Note.
* Knowledge Sources.

#### Step 3: Run the Lead stage

* Identify affected services.
* Produce the implementation plan.
* Define expected validation.

#### Step 4: Run the Developer stage

* Create or modify the required code.
* Record changed files.
* Run developer-level tests.

#### Step 5: Run the Reviewer stage

* Check Clean Architecture boundaries.
* Review domain behavior.
* Review event contract.
* Record findings.

#### Step 6: Run the Validator stage

* Run build.
* Run unit and integration tests.
* Run architecture tests.
* Store machine-readable evidence.

#### Step 7: Run the Evaluator stage

* Score architecture, completeness, maintainability, security, and test quality.
* Produce a recommendation.

#### Step 8: Request human approval

* Present summary, findings, validation evidence, and risks.
* Record approval or requested changes.

#### Step 9: Create the pull request

* Generate a structured title and description.
* Attach validation and evaluation evidence.
* Record the pull-request identifier.

#### Step 10: Close the workflow

* Store metrics.
* Store final status.
* Preserve artifacts.
* Record any proposed improvements.

### Alpha Car Detailing example

The entire section uses corporate fleet booking as the task and follows it from Prompt intake to pull-request creation.

### Recommended diagram or table

**Table: Hands-on Workflow Artifacts**

| Stage          | Input                       | Output                   |
| -------------- | --------------------------- | ------------------------ |
| Lead           | Task and repository context | `plan.md`                |
| Developer      | Approved plan               | Code changes             |
| Reviewer       | Code diff and Instructions  | `review-findings.md`     |
| Validator      | Branch and test policy      | `validation-result.json` |
| Evaluator      | Evidence package            | `evaluation-result.json` |
| Human approval | Summary and evidence        | Approval record          |
| PR creation    | Approved artifacts          | Pull request             |

---

## 8. Claude Example

### Section title

**Using Claude Code Inside a Harness**

### Purpose

Show how Claude Code participates as an execution engine within a controlled, vendor-neutral Harness.

### Key topics

* Claude Code as the primary platform for handbook examples.
* Supplying repository Instructions.
* Loading Role prompts.
* Providing scoped Skills and Knowledge Sources.
* Invoking Claude for planning, implementation, review, and evaluation.
* Separating Claude sessions by Role.
* Capturing outputs to files.
* Restricting tool access.
* Using scripts to invoke stages.
* Preserving state outside Claude conversations.
* Requiring executed validation evidence.
* Avoiding reliance on a single long-running session.
* Handling Claude output that does not match the expected contract.
* Approval before pull-request creation.
* Claude-specific capabilities versus Harness-level responsibilities.

### Alpha Car Detailing example

* Lead Claude session creates the fleet-booking plan.
* Developer Claude session implements the plan.
* Reviewer Claude session independently reviews the change.
* Validator stage runs deterministic tools.
* Evaluator Claude session assesses the full evidence package.
* The Harness requests human approval before creating the pull request.

### Recommended diagram or table

**Table: Claude Code Responsibilities versus Harness Responsibilities**

| Activity                    |          Claude Code |                  Harness |
| --------------------------- | -------------------: | -----------------------: |
| Reason about implementation |                  Yes |              Coordinates |
| Modify files                | Yes, when authorized | Grants scoped permission |
| Run tools                   | Requests or performs |     Controls and records |
| Maintain workflow state     |                   No |                      Yes |
| Enforce approval gates      |                   No |                      Yes |
| Record audit trail          |              Limited |                      Yes |
| Create PR                   |             Possible |  Allowed only after gate |

---

## 9. GitHub Copilot Comparison

### Section title

**Using GitHub Copilot in Harness-Controlled Workflows**

### Purpose

Explain where GitHub Copilot fits well and where additional Harness components are needed.

### Key topics

* Copilot as an IDE- and repository-integrated development assistant.
* Interactive coding assistance.
* Agent capabilities where available.
* Repository Instructions.
* Prompt files.
* Pull-request review support.
* GitHub-native workflow integration.
* GitHub Actions for validation.
* Differences from Claude Code:

  * Interaction model
  * Tool execution model
  * Session control
  * Repository integration
  * Automation boundaries
* Harness options:

  * Developer-driven Copilot stage
  * GitHub Actions-based validation
  * Automated review assistance
  * Pull-request evidence collection
* External state and approval remain Harness responsibilities.
* Do not assume Copilot itself is the Harness.
* Use Copilot where GitHub-native integration provides value.

### Alpha Car Detailing example

A developer uses Copilot to implement fleet-booking code. The Harness records the branch, invokes GitHub Actions, collects review findings, requests approval, and controls pull-request completion.

### Recommended diagram or table

**Comparison Table: Claude Code and GitHub Copilot in a Harness**

| Dimension               | Claude Code                          | GitHub Copilot                       |
| ----------------------- | ------------------------------------ | ------------------------------------ |
| Primary interaction     | Agent-oriented command-line workflow | IDE and GitHub-integrated workflow   |
| Repository modification | Agent-driven                         | Developer- or Agent-assisted         |
| Harness integration     | Script and tool adapters             | IDE, GitHub, and Actions integration |
| Validation              | Harness invokes tools                | Often GitHub Actions                 |
| State management        | External Harness                     | External Harness                     |
| Approval                | External Harness                     | GitHub review plus Harness policy    |

---

## 10. Codex Comparison

### Section title

**Using OpenAI Codex Inside a Harness**

### Purpose

Explain how Codex can operate as an Agent execution provider while the Harness retains workflow control.

### Key topics

* Codex as a coding Agent within controlled execution.
* Supplying task context.
* Repository access.
* Tool and command execution.
* Agent isolation.
* Structured output capture.
* Integration through adapters.
* Differences from Claude Code and GitHub Copilot:

  * Execution environment
  * Tool model
  * Task delegation
  * Output handling
  * Repository workflow integration
* Harness-controlled validation and evaluation.
* Provider-independent Role definitions.
* External state, permissions, approvals, and metrics.
* Avoiding Codex-specific workflow coupling.

### Alpha Car Detailing example

The Harness delegates the fleet-booking implementation stage to Codex, then uses the same Reviewer, Validator, Evaluator, approval, and pull-request workflow defined for other providers.

### Recommended diagram or table

**Three-Platform Comparison**

| Capability           | Claude Code                    | GitHub Copilot                              | Codex                  |
| -------------------- | ------------------------------ | ------------------------------------------- | ---------------------- |
| Primary usage model  | Agent-oriented coding workflow | IDE and GitHub assistance                   | Delegated coding Agent |
| Harness role         | Execution provider             | Assisted development and GitHub integration | Execution provider     |
| State ownership      | Harness                        | Harness                                     | Harness                |
| Validation ownership | Harness/tools                  | Harness/GitHub Actions                      | Harness/tools          |
| Human approval       | Harness                        | Harness/GitHub reviews                      | Harness                |

---

## 11. Best Practices

### Section title

**Designing Reliable AI Engineering Harnesses**

### Purpose

Provide practical guidance for creating Harnesses that remain understandable, governable, and maintainable.

### Key topics

1. Start with a small, explicit workflow.
2. Keep Instructions, Skills, Prompts, Roles, Steering Notes, and Knowledge Sources separate.
3. Store workflow state outside Agent conversations.
4. Use clear stage contracts.
5. Separate validation from evaluation.
6. Make human approval explicit.
7. Enforce least-privilege tool access.
8. Record every material state transition.
9. Preserve validation evidence.
10. Make retries bounded.
11. Use idempotent stages.
12. Keep provider adapters replaceable.
13. Prefer machine-readable outputs for gates.
14. Keep qualitative findings human-readable.
15. Treat Agent output as untrusted until validated.
16. Version workflow definitions.
17. Capture source and context provenance.
18. Avoid duplicating CI/CD capabilities.
19. Define terminal failure states.
20. Measure quality and reliability, not only speed.
21. Introduce multi-agent workflows only when role separation adds value.
22. Require approval before standards evolve.
23. Test the Harness itself.
24. Design for interruption and recovery.
25. Keep the first implementation operationally simple.

### Alpha Car Detailing example

The first fleet-booking Harness uses five explicit stages, file-based state, deterministic validation scripts, a bounded retry count, and a mandatory approval gate.

### Recommended diagram or table

**Best-Practice Checklist**

---

## 12. Anti-patterns

### Section title

**Common Harness Design Failures**

### Purpose

Identify designs that create the appearance of automation without reliable engineering control.

### Key topics

* Calling a single Agent script a Harness.
* Allowing one Agent to plan, implement, approve, and merge.
* Storing all workflow state inside chat history.
* Treating Agent confidence as validation.
* Combining validation and evaluation into one vague review.
* Giving Agents unrestricted shell access.
* Allowing silent changes to Instructions or Skills.
* Retrying indefinitely.
* Discarding failed attempts.
* Creating pull requests before approval.
* Replacing CI/CD with ad hoc Agent commands.
* Hard-coding workflows to one provider.
* Loading every repository document into every Agent context.
* Recording logs without correlation identifiers.
* Measuring only generated lines of code.
* Using too many Agents for simple tasks.
* Failing to define ownership.
* Allowing outdated Steering Notes or Knowledge Sources to control execution.
* Mixing workflow state, memory, and authoritative knowledge.
* Automatically promoting Harness Memory into standards.
* Running high-risk tools with developer workstation credentials.
* Ignoring cancellation and recovery.
* Building a complex orchestration platform before validating the workflow.

### Alpha Car Detailing example

A flawed fleet-booking workflow allows the Developer Agent to modify architecture Instructions, approve its own changes, skip failed tests, and create the pull request.

### Recommended diagram or table

**Table: Anti-pattern, Risk, and Correction**

| Anti-pattern                  | Risk                     | Correction                |
| ----------------------------- | ------------------------ | ------------------------- |
| One Agent controls everything | No independent assurance | Separate Roles and gates  |
| State exists only in chat     | Workflow cannot recover  | Persist explicit state    |
| Unlimited retries             | Cost and infinite loops  | Set retry limits          |
| Agent claims tests passed     | False confidence         | Execute and capture tests |
| Silent standard changes       | Governance failure       | Require human approval    |

---

## 13. Architect’s Notes

### Section title

**Architectural Guidance for Harness Adoption**

### Purpose

Highlight decisions that require architectural judgment rather than simple implementation rules.

### Key topics

* A Harness is a socio-technical system.
* Process design matters more than orchestration technology.
* The Harness should make authority visible.
* Deterministic controls should surround probabilistic Agent behavior.
* Validation, evaluation, and approval are separate architectural concerns.
* Enterprise Harnesses need stable contracts between stages.
* State must be external, durable, and inspectable.
* Provider-specific capabilities should remain behind adapters.
* Security policy should be enforced outside Agent reasoning.
* Harness Memory is advisory until governed.
* Multi-agent design should follow assurance needs, not novelty.
* Architecture boundaries should prevent the Harness from becoming a replacement for GitHub, CI/CD, identity, or observability platforms.
* The simplest effective Harness is usually the best starting architecture.
* Workflow definitions should be treated as version-controlled software assets.
* Harness changes require testing and review because they alter how engineering work is performed.

### Alpha Car Detailing example

The Alpha Car Detailing Harness should begin as a small, repository-based workflow before evolving into a centralized enterprise platform.

### Recommended diagram or table

**Architect’s Note callouts throughout the section**

---

## 14. Enterprise Tips

### Section title

**Operating a Harness Across Teams**

### Purpose

Provide organizational guidance for scaling Harness usage.

### Key topics

* Establish Harness ownership.
* Create a platform team or clearly assigned maintainers.
* Define onboarding standards.
* Provide approved workflow templates.
* Maintain an Agent and tool registry.
* Centralize permission policies.
* Integrate with enterprise identity.
* Define retention and audit requirements.
* Establish approval matrices.
* Create standard validation packs.
* Use reusable evaluation rubrics.
* Track adoption and reliability.
* Provide escape hatches for human intervention.
* Separate experimentation from production Harnesses.
* Introduce cost budgets.
* Monitor provider availability.
* Define incident response.
* Review Harness changes as platform changes.
* Create maturity levels:

  * Manual Agent usage
  * Scripted local Harness
  * Team Harness
  * Enterprise Harness
  * Adaptive Harness
* Preserve team autonomy while enforcing minimum controls.
* Avoid using Harness metrics as simplistic individual performance measures.

### Alpha Car Detailing example

The Platform Engineering team maintains approved Harness templates, while the Alpha Car Detailing team owns domain-specific Prompts, Skills, Steering Notes, and evaluation criteria.

### Recommended diagram or table

**Harness Maturity Model**

| Level | Description                |
| ----- | -------------------------- |
| 0     | Ad hoc Agent usage         |
| 1     | Repeatable local scripts   |
| 2     | Team-managed Harness       |
| 3     | Central enterprise Harness |
| 4     | Governed adaptive Harness  |

---

## 15. Decision Points

### Section title

**Key Harness Design Decisions**

### Purpose

Help architects select an appropriate Harness design for their organization and use case.

### Key topics and decision questions

#### Decision 1: Single-agent or multi-agent?

* How much independent review is required?
* What is the cost and latency tolerance?
* Are duties meaningfully separable?

#### Decision 2: Local or enterprise Harness?

* How many teams and repositories are involved?
* Is centralized auditability required?
* Are workflows concurrent?

#### Decision 3: Script-based or service-based?

* Is the workflow stable?
* Is centralized state required?
* What operational maturity exists?

#### Decision 4: File-based or database state?

* Does the workflow need recovery across machines?
* Are concurrent updates possible?
* Is reporting required?

#### Decision 5: Synchronous or asynchronous execution?

* How long do stages run?
* Must users remain connected?
* Are queues and workers required?

#### Decision 6: Human approval location

* Before implementation?
* Before pull-request creation?
* Before merge?
* Before deployment?

#### Decision 7: Validation policy

* Which checks are mandatory?
* Which are advisory?
* Who owns the policy?

#### Decision 8: Evaluation approach

* Rule-based?
* Agent-based?
* Human-based?
* Hybrid?

#### Decision 9: Tool permission model

* Per Agent?
* Per workflow?
* Per repository?
* Per environment?

#### Decision 10: Provider strategy

* Claude-only?
* Multiple providers?
* Provider fallback?
* Provider selection by task?

#### Decision 11: Memory usage

* What may be remembered?
* How is provenance recorded?
* What requires approval?

#### Decision 12: Pull-request automation

* Create draft pull request automatically?
* Require approval first?
* Allow merge automation?

### Alpha Car Detailing example

Provide a recommended decision profile for the first fleet-booking Harness:

* Multi-agent for major feature work.
* Local script-based implementation initially.
* File-based state.
* Mandatory build, test, and architecture validation.
* Human approval before pull-request creation.
* Claude Code as the primary Agent provider.
* No automatic merge or deployment.

### Recommended diagram or table

**Decision Matrix: Recommended Initial Alpha Car Detailing Harness**

---

## 16. Exercises

### Section title

**Applying Harness Engineering**

### Purpose

Test conceptual understanding and guide readers toward practical implementation.

### Exercises

#### Exercise 1: Define a Harness boundary

List which fleet-booking activities belong inside the Harness and which remain in GitHub Actions, source control, or human governance.

#### Exercise 2: Design a single-agent workflow

Create a Harness workflow for adding validation to an existing Booking API request.

#### Exercise 3: Design a multi-agent workflow

Assign Lead, Developer, Reviewer, Validator, and Evaluator Roles for corporate fleet booking.

#### Exercise 4: Create a state model

Define states, transitions, retries, and terminal outcomes for the fleet-booking workflow.

#### Exercise 5: Define tool permissions

Create a permission matrix for each Agent Role.

#### Exercise 6: Separate validation and evaluation

Define five validation checks and five evaluation criteria for fleet booking.

#### Exercise 7: Design an approval gate

Specify who approves:

* Database changes
* Public API changes
* Security changes
* Pull-request creation
* Production deployment

#### Exercise 8: Create a retry policy

Define which failures are retriable and when human escalation occurs.

#### Exercise 9: Define audit evidence

List the information required to reconstruct the complete fleet-booking workflow.

#### Exercise 10: Define initial metrics

Select six metrics for measuring reliability, quality, and efficiency.

#### Exercise 11: Compare implementation options

Compare a PowerShell Harness, a .NET service, and GitHub Actions-based orchestration.

#### Exercise 12: Create an Agent adapter contract

Define common inputs and outputs that could support Claude Code, GitHub Copilot, and Codex.

#### Exercise 13: Identify anti-patterns

Review a workflow in which one Agent writes code, claims tests passed, and opens a pull request. Identify the missing Harness controls.

#### Exercise 14: Design recovery

Explain how the workflow resumes after evaluation succeeds but pull-request creation fails.

#### Exercise 15: Define memory governance

Describe which lessons may enter Harness Memory and which changes require human approval.

### Alpha Car Detailing example

All exercises should use the Alpha Car Detailing repository and corporate fleet-booking scenario.

### Recommended diagram or table

**Exercise Completion Checklist**

---

## 17. Interview Questions

### Section title

**Harness Engineering Interview Questions**

### Purpose

Provide discussion questions for architects, senior developers, platform engineers, and engineering leaders.

### Questions

1. What is an AI Engineering Harness?
2. Why is an AI Agent not itself a Harness?
3. How does a Harness differ from a workflow script?
4. How does a Harness differ from a CI/CD pipeline?
5. How does a Harness differ from an orchestration framework?
6. What are the primary responsibilities of a Harness?
7. What inputs should a Harness receive?
8. What outputs should a Harness produce?
9. How do Instructions participate in Harness execution?
10. How do Skills participate in Harness execution?
11. How do Prompts, Roles, Steering Notes, and Knowledge Sources differ inside a Harness?
12. Why should workflow state exist outside Agent conversations?
13. What is the difference between validation and evaluation?
14. Why should Agent output be treated as untrusted?
15. Where should human approval be required?
16. How should retries be bounded?
17. What makes a Harness stage idempotent?
18. What information belongs in an audit trail?
19. Which Harness metrics are useful?
20. How should tool permissions be assigned?
21. What is the difference between Harness State and Harness Memory?
22. Why must Harness Memory not silently modify Instructions?
23. When is a single-agent Harness sufficient?
24. When does a multi-agent workflow provide value?
25. What are the main trade-offs between local and enterprise Harnesses?
26. How can a Harness remain vendor-neutral?
27. How would Claude Code be integrated into a Harness?
28. How would GitHub Copilot participate differently?
29. How would Codex be integrated through an adapter?
30. What should happen when validation passes but evaluation fails?
31. How should a Harness recover from an interrupted workflow?
32. Which responsibilities should remain outside the Harness?
33. How would you prevent duplicate pull requests after retries?
34. How should prompt injection risks be handled?
35. What is the minimum viable Harness for an enterprise team beginning adoption?

### Alpha Car Detailing example

Include scenario-based questions asking candidates to design a Harness workflow for corporate fleet booking and explain failure, approval, and recovery behavior.

### Recommended diagram or table

**Table: Interview Question Categories**

| Category            | Focus                         |
| ------------------- | ----------------------------- |
| Fundamentals        | Definitions and distinctions  |
| Architecture        | Components, state, boundaries |
| Governance          | Approval, audit, security     |
| Operations          | Retry, recovery, metrics      |
| Platform comparison | Claude, Copilot, Codex        |
| Scenario design     | Alpha Car Detailing workflow  |

---

## 18. Chapter Summary

### Section title

**From Context to Controlled Execution**

### Purpose

Reinforce the major ideas and prepare readers for Chapter 13, Harness Architecture.

### Key topics

* Repository Intelligence gives Agents the context required to work.
* The Harness coordinates how work is performed.
* A Harness is not an Agent, script, CI/CD pipeline, or orchestration framework.
* The Harness controls:

  * Planning
  * Agent execution
  * Tool execution
  * Validation
  * Evaluation
  * Approval
  * Retry
  * State
  * Logging
  * Metrics
  * Outputs
* Instructions, Skills, Prompts, Roles, Steering Notes, and Knowledge Sources become controlled workflow inputs.
* Validation provides technical evidence.
* Evaluation provides quality assessment.
* Human approval preserves accountability.
* State enables recovery.
* Memory supports future improvement but remains governed.
* Single-agent Harnesses provide a practical starting point.
* Multi-agent Harnesses add role separation and independent assurance.
* Local Harnesses support experimentation.
* Enterprise Harnesses provide centralized policy, scale, security, and auditability.
* Claude Code, GitHub Copilot, and Codex are execution platforms that can operate inside a Harness.
* The Harness remains the vendor-neutral engineering control layer.
* Transition to Chapter 13:

  * Chapter 12 defines what a Harness is.
  * Chapter 13 will design its internal architecture in greater detail.

### Alpha Car Detailing example

Summarize how the fleet-booking Harness converted a fragmented set of Agent activities into a controlled, observable, recoverable, and approved engineering workflow.

### Recommended diagram or table

**Summary Diagram**

```text
Repository Intelligence
          ↓
       Harness
          ↓
Plan → Execute → Review → Validate → Evaluate → Approve → PR
          ↓
 State + Audit + Metrics + Artifacts
```

---

## 19. Further Reading

### Section title

**Sources for Deeper Study**

### Purpose

Direct readers toward authoritative material related to workflow orchestration, secure automation, AI Agent systems, validation, governance, and software delivery.

### Key topics

* Claude Code documentation.
* GitHub Copilot documentation.
* OpenAI Codex documentation.
* GitHub Actions documentation.
* Azure DevOps pipeline documentation.
* Workflow orchestration patterns.
* Durable execution patterns.
* State-machine design.
* Idempotency and retry patterns.
* Secure tool execution.
* Least-privilege access.
* Software supply-chain security.
* Audit logging.
* OpenTelemetry.
* AI governance frameworks.
* Human-in-the-loop systems.
* Multi-agent system architecture.
* Evaluation frameworks for AI-generated software.
* Enterprise architecture references.
* Repository documentation from completed Chapters 1–11.
* Internal Alpha Car Detailing architecture decisions, Instructions, Skills, Roles, Steering Notes, and Knowledge Sources.

### Alpha Car Detailing example

Recommend reviewing the Alpha Car Detailing repository’s approved architecture Instructions, fleet-booking Skills, Agent Roles, release Steering Note, Knowledge Sources, and CI workflow before implementing the Chapter 12 example.

### Recommended diagram or table

**Categorized Further Reading Table**

| Category                | Recommended focus                                 |
| ----------------------- | ------------------------------------------------- |
| Agent platforms         | Claude Code, GitHub Copilot, Codex                |
| Workflow execution      | Orchestration, durable workflows, state machines  |
| Engineering validation  | Builds, tests, architecture tests, security scans |
| Governance              | Approval, audit, permissions, compliance          |
| Observability           | Logs, metrics, traces                             |
| Enterprise architecture | Boundaries, adapters, platform design             |

---

# Recommended Chapter Callouts

The following callouts should be distributed throughout the chapter:

### Architect’s Note

A Harness should surround probabilistic AI behavior with deterministic engineering controls.

### Enterprise Tip

Begin with a small repository-based Harness before investing in a centralized orchestration platform.

### Common Mistake

A collection of scripts is not automatically a Harness. A Harness must also manage state, evidence, permissions, decisions, and workflow transitions.

### Decision Point

Decide whether pull-request creation requires approval before automation is implemented.

### Real-World Scenario

A solution may compile and pass all tests while still failing evaluation because it violates architecture principles or omits important operational requirements.

### Architect’s Note

Harness State records the current workflow. Harness Memory preserves prior experience. Knowledge Sources provide authoritative evidence. These concepts must not be combined indiscriminately.

### Enterprise Tip

Keep Agent providers replaceable by defining stable Harness contracts for inputs, outputs, tools, and completion status.

### Common Mistake

Never accept an Agent statement that tests passed as validation evidence. The Harness must execute the tests and record the result.

---

# Recommended Chapter Tables

1. Individual AI usage versus Harness-controlled usage
2. Harness versus AI Agent
3. Harness versus workflow script
4. Harness versus CI/CD pipeline
5. Harness versus orchestration framework
6. Harness responsibilities and non-responsibilities
7. Harness inputs, intermediate outputs, and final outputs
8. Repository Intelligence assets inside the Harness
9. Tool permission matrix by Agent Role
10. Validation versus evaluation
11. Harness State versus Harness Memory versus Knowledge Sources
12. Local versus enterprise Harness
13. Claude Code versus GitHub Copilot versus Codex
14. Harness failure categories and recovery policies
15. Initial Harness metrics
16. Harness architecture decision matrix
17. Harness maturity model

---

# Recommended Chapter Diagrams

1. Before and after the Harness
2. Repository Intelligence to Harness execution
3. Logical Harness architecture
4. End-to-end fleet-booking workflow
5. Multi-agent sequence diagram
6. Harness state machine
7. Control plane and execution plane
8. Agent tool-authorization boundary
9. Validation and evaluation separation
10. Human approval gate
11. Local-to-enterprise Harness evolution
12. Provider adapter architecture
13. State, memory, artifact, audit, and metrics storage
14. Failure handling and retry loop
15. Harness and enterprise platform context diagram

---

Chapter 12 outline is complete.
