# Chapter 13 — Harness Architecture

## Part III — Harness Engineering

### 1. Opening Scenario: Coordinating Corporate Fleet Booking Delivery

* Alpha Car Detailing begins implementing corporate fleet booking.
* Multiple agents can plan, develop, review, validate, and evaluate the feature.
* Initial automation problems:

  * Agents execute without clear boundaries.
  * Validation occurs inconsistently.
  * Agents judge their own implementations.
  * Failures trigger uncontrolled retries.
  * Human approvals happen too late.
  * Execution evidence is fragmented.
* The enterprise requirement for a governed Harness architecture.
* Transition from the conceptual Harness introduced in Chapter 12 to its architectural design.

---

## 2. Learning Objectives

By the end of this chapter, readers will be able to:

* Explain the architecture of an enterprise AI Engineering Harness.
* distinguish the control plane from the execution plane.
* Identify the responsibilities and boundaries of Harness components.
* Integrate Repository Intelligence into Harness execution.
* Design single-agent and multi-agent orchestration flows.
* Implement independent Deterministic Validation Gates.
* Structure a deterministic gate pipeline.
* Use hooks and automated guardrails at controlled lifecycle points.
* Separate deterministic validation from AI-assisted evaluation.
* Design retry, failure, approval, logging, and audit mechanisms.
* Apply security, permissions, isolation, and sandboxing controls.
* Compare local and enterprise Harness architectures.
* Identify integration points for Claude Code, GitHub Copilot, and OpenAI Codex.

---

## 3. From Harness Concept to Harness Architecture

### 3.1 Recap of Chapter 12

* The Harness as a controlled automation layer.
* The Harness coordinates work rather than performing all work itself.
* Relationship among:

  * Instructions
  * Skills
  * Prompts
  * Roles
  * Steering Notes
  * Knowledge Sources
  * Tools
  * Gates
  * Evaluations
  * Approvals
  * Outputs

### 3.2 Why Architecture Matters

* Moving from individual agent use to governed engineering execution.
* Making workflows repeatable and observable.
* Preventing hidden coupling between agents and validation.
* Supporting consistent behavior across repositories and teams.
* Creating enforceable enterprise controls.

### 3.3 Core Architectural Principle

* Agents propose, reason, and implement.
* Deterministic systems verify objective conditions.
* Evaluators assess quality and fitness.
* Humans retain authority over defined high-impact decisions.
* The Harness coordinates and records the complete process.

---

## 4. Harness Architecture Principles

### 4.1 Explicit Responsibility Boundaries

* Separate orchestration, execution, validation, evaluation, and approval.
* Prevent one component from assuming conflicting responsibilities.

### 4.2 Deterministic Checks Before Subjective Judgment

* Run objective checks before AI-assisted review or evaluation.
* Reject objectively invalid work early.

### 4.3 Evidence-Based Execution

* Require agents to use repository evidence and trusted Knowledge Sources.
* Record evidence used during decisions.

### 4.4 Least Privilege

* Grant only the tools, files, commands, and environments needed by each role.
* Separate read, write, execute, approve, and publish permissions.

### 4.5 Observable State Transitions

* Record every workflow transition.
* Preserve execution history, gate results, approvals, and outputs.

### 4.6 Fail Safely

* Stop on unknown or unsafe conditions.
* Prevent uncontrolled retries and partial publication.

### 4.7 Replaceable Components

* Avoid binding Harness logic to one model or coding platform.
* Use adapters for agents, tools, repositories, and validation systems.

### 4.8 Human Authority at Defined Boundaries

* Define where human approval is required.
* Avoid informal approval outside the Harness workflow.

---

## 5. High-Level Harness Architecture

### 5.1 Primary Architectural Layers

* Interaction layer
* Control plane
* Execution plane
* Validation and evaluation layer
* Governance and approval layer
* State and evidence layer
* Integration layer

### 5.2 End-to-End Reference Flow

`User → Harness → Lead → Developer → Deterministic Gates → Reviewer → Validator → Evaluator → Human Approval → Pull Request`

### 5.3 Responsibilities at Each Stage

* User provides intent and constraints.
* Harness creates and manages the workflow.
* Lead produces the execution plan.
* Developer creates the implementation.
* Deterministic Gates independently verify objective requirements.
* Reviewer examines implementation quality.
* Validator confirms acceptance criteria and expected behavior.
* Evaluator scores the overall result.
* Human approver authorizes publication.
* Harness creates or updates the pull request.

### Diagram: High-Level Harness Architecture

Show:

* User interaction
* Harness control plane
* Agent execution plane
* Deterministic gates
* Evaluation and approval
* Repository and external integrations
* State, evidence, and telemetry stores

---

## 6. Harness Components and Boundaries

### 6.1 Request Intake

* Capture task description.
* Validate required inputs.
* Assign a workflow identifier.
* Normalize goals and acceptance criteria.

### 6.2 Workflow Orchestrator

* Select the workflow.
* Activate roles.
* Sequence execution steps.
* Manage handoffs.
* Apply retry and failure policies.
* Pause for approvals.

### 6.3 Context Assembler

* Load Instructions.
* Select relevant Skills.
* Apply the current Prompt.
* Load Role definitions.
* Include Steering Notes.
* Retrieve Knowledge Sources.
* Enforce context size and authority rules.

### 6.4 Agent Runtime

* Start agent sessions.
* Supply bounded context.
* Grant approved tools.
* Capture reasoning artifacts where permitted.
* Record outputs and execution status.

### 6.5 Tool Gateway

* Provide controlled access to:

  * Repository operations
  * File operations
  * Build tools
  * Test runners
  * Package managers
  * Infrastructure tools
  * Issue trackers
  * Pull-request systems
* Enforce permission and command policies.

### 6.6 Gate Runner

* Execute deterministic checks independently of agents.
* Normalize results.
* Store evidence.
* Block progression on mandatory failures.

### 6.7 Evaluation Engine

* Apply quality rubrics.
* Compare outputs with goals and acceptance criteria.
* Produce structured scores and findings.

### 6.8 Approval Manager

* Request human decisions.
* Record approver identity and rationale.
* Apply timeout and escalation policies.

### 6.9 State Store

* Persist workflow state.
* Support recovery and resumption.
* Prevent duplicate or invalid transitions.

### 6.10 Evidence and Artifact Store

* Preserve plans, patches, reports, logs, gate evidence, evaluations, and approvals.
* Maintain traceability between task, execution, and pull request.

### 6.11 Telemetry and Audit Service

* Collect logs, metrics, traces, and audit events.
* Separate operational telemetry from compliance evidence.

### Diagram: Harness Component Model

Show component responsibilities, interfaces, dependencies, and trust boundaries.

---

## 7. Control Plane versus Execution Plane

### 7.1 Control Plane Responsibilities

* Workflow definition
* Policy enforcement
* Role selection
* Context selection
* State management
* Permission assignment
* Gate sequencing
* Approval coordination
* Audit recording

### 7.2 Execution Plane Responsibilities

* Agent execution
* Tool invocation
* Repository modification
* Build and test execution
* Gate execution
* Artifact generation

### 7.3 Why the Separation Matters

* Security isolation
* Independent enforcement
* Fault containment
* Vendor portability
* Horizontal scaling
* Centralized governance

### 7.4 Control-Plane Failure Scenarios

* Lost workflow state
* Duplicate execution
* Incorrect role assignment
* Stale configuration
* Unauthorized state transition

### 7.5 Execution-Plane Failure Scenarios

* Agent timeout
* Tool failure
* Build failure
* Sandbox failure
* Resource exhaustion

---

## 8. Harness Inputs and Outputs

### 8.1 Primary Inputs

* User request
* Goal
* Acceptance criteria
* Repository reference
* Branch or workspace
* Instructions
* Skills
* Role definitions
* Steering Notes
* Knowledge Sources
* Harness configuration
* Security policies

### 8.2 Intermediate Artifacts

* Discovery report
* Execution plan
* Agent handoff package
* Code changes
* Gate reports
* Review findings
* Validation evidence
* Evaluation scores
* Approval records

### 8.3 Final Outputs

* Pull request
* Code patch
* Documentation updates
* Test results
* Validation report
* Evaluation report
* Audit record
* Metrics and execution summary

### 8.4 Output Contracts

* Structured versus unstructured output
* Required metadata
* Versioning
* Traceability
* Retention requirements

---

## 9. Repository Intelligence Integration

### 9.1 Repository Intelligence as a Harness Dependency

* Repository discovery
* Instructions
* Skills
* Prompts
* Roles
* Steering Notes
* Knowledge Sources

### 9.2 Context Assembly Order

* Persistent standards
* Reusable procedures
* Current mission direction
* Trusted evidence
* Task-specific request
* Role-specific responsibility

### 9.3 Context Selection

* Relevance
* Authority
* Freshness
* Scope
* Conflict resolution
* Token and size limits

### 9.4 Context Provenance

* Record which sources were loaded.
* Track versions and timestamps.
* Identify conflicts or unavailable sources.

### 9.5 Preventing Context Leakage

* Restrict sensitive sources by role.
* Avoid unnecessary context sharing.
* Sanitize agent handoff packages.

---

## 10. Role Orchestration

### 10.1 Roles as Architectural Components

* Lead
* Developer
* Reviewer
* Validator
* Evaluator
* Architect
* Security Reviewer
* Human Approver

### 10.2 Role Activation

* Static workflow assignment
* Conditional role activation
* Risk-based role selection
* Policy-driven escalation

### 10.3 Responsibility and Authority

* What each role can decide
* What each role can modify
* What each role can approve
* What each role must produce

### 10.4 Preventing Role Conflicts

* Developer does not approve its own implementation.
* Evaluator does not replace deterministic validation.
* Reviewer does not bypass failed gates.
* Harness does not silently expand agent authority.

---

## 11. Prompt and Skill Execution

### 11.1 Prompt Resolution

* Select the current task Prompt.
* Bind task-specific variables.
* Validate required sections.
* Include measurable acceptance criteria.

### 11.2 Skill Resolution

* Select applicable Skills.
* Check Skill version and compatibility.
* Apply prerequisites.
* Execute steps within the permitted role boundary.

### 11.3 Execution Precedence

* Enterprise policies
* Repository Instructions
* Active Steering Notes
* Role constraints
* Skill procedure
* Task Prompt

### 11.4 Conflicting Guidance

* Detect conflicts.
* Apply authority rules.
* Escalate unresolved conflicts.
* Record the final decision.

---

## 12. Tool Execution Architecture

### 12.1 Tool Categories

* Read-only repository tools
* File modification tools
* Build and test tools
* Security scanners
* Package-management tools
* Deployment tools
* Collaboration tools
* Pull-request tools

### 12.2 Tool Contracts

* Input schema
* Output schema
* Timeout
* Error behavior
* Idempotency
* Audit metadata

### 12.3 Tool Permission Model

* Role-based permissions
* Task-scoped permissions
* Repository-scoped permissions
* Environment-scoped permissions
* Temporary privilege elevation

### 12.4 Safe Tool Invocation

* Command allowlists
* Path restrictions
* Secret filtering
* Network restrictions
* Output-size limits
* Destructive-operation controls

---

## 13. Workflow State Architecture

### 13.1 Workflow States

* Requested
* Discovering
* Planning
* Awaiting plan approval
* Implementing
* Running gates
* Reviewing
* Validating
* Evaluating
* Awaiting human approval
* Publishing
* Completed
* Failed
* Cancelled

### 13.2 State Transition Rules

* Permitted transitions
* Required evidence
* Actor authority
* Transition timestamps
* Failure transitions

### 13.3 Durable State

* Restart recovery
* Long-running workflows
* Approval waits
* Agent interruption
* Tool failure recovery

### 13.4 Idempotency

* Workflow identifiers
* Step identifiers
* Duplicate-request detection
* Safe step replay
* Pull-request creation protection

### 13.5 Checkpoints

* Context checkpoint
* Plan checkpoint
* Implementation checkpoint
* Gate checkpoint
* Approval checkpoint
* Publication checkpoint

---

## 14. Agent Handoffs

### 14.1 Purpose of a Handoff

* Transfer responsibility without losing evidence or constraints.
* Prevent every agent from rediscovering the complete task.

### 14.2 Handoff Package

* Goal
* Scope
* Acceptance criteria
* Current state
* Relevant evidence
* Changed files
* Open risks
* Previous findings
* Required output
* Permitted tools

### 14.3 Handoff Validation

* Confirm required fields.
* Check artifact availability.
* Detect stale evidence.
* Verify receiving-role permissions.

### 14.4 Handoff Failure Modes

* Missing context
* Excessive context
* Unresolved findings
* Conflicting instructions
* Unauthorized responsibility transfer

### Diagram: Agent Orchestration Flow

Show role activation, handoff packages, feedback loops, and Harness-controlled transitions.

---

## 15. Deterministic Validation Gates

### 15.1 What a Deterministic Gate Is

* An independently executed check with objective inputs, rules, and outcomes.
* A repeatable check that produces the same result for the same relevant state.

### 15.2 Why Gates Must Be Independent

* The implementing agent must not decide whether its own work is correct.
* Agent confidence is not validation evidence.
* Self-review cannot replace executable checks.
* Gate execution must occur outside the agent’s decision authority.

### 15.3 Gate Result Model

* Passed
* Failed
* Blocked
* Skipped by approved policy
* Error
* Timed out

### 15.4 Mandatory versus Advisory Gates

* Mandatory gates block progression.
* Advisory gates report risk without automatically blocking.
* Policies determine severity and override authority.

### 15.5 Gate Evidence

* Command executed
* Configuration used
* Environment
* Start and end time
* Exit code
* Findings
* Logs
* Artifact references
* Gate version

---

## 16. Gate Pipeline and Architecture

### 16.1 Gate Pipeline Stages

* Workspace integrity
* Dependency restore
* Build
* Unit tests
* Integration tests
* Lint and formatting
* Architecture rules
* Security scanning
* API and event contract validation
* Packaging
* Deployment-readiness checks

### 16.2 Gate Ordering

* Fast checks before expensive checks.
* Foundational checks before dependent checks.
* Security-critical checks at required stages.
* Parallel execution for independent gates.

### 16.3 Gate Dependencies

* Build before runtime tests
* Schema generation before contract comparison
* Test environment readiness before integration tests
* Artifact generation before packaging validation

### 16.4 Gate Policies

* Failure thresholds
* Warning thresholds
* Timeouts
* Retry rules
* Required evidence
* Override authority

### 16.5 Gate Execution Environments

* Developer workstation
* Harness sandbox
* Container
* CI runner
* Dedicated enterprise validation environment

### Diagram: Deterministic Gate Pipeline

Show ordered and parallel gates, blocking conditions, normalized results, and evidence collection.

---

## 17. Build, Test, Lint, Architecture, Security, and Contract Gates

### 17.1 Build Gate

* Dependency restoration
* Compilation
* Warning policies
* Reproducible builds
* Target-framework validation

### 17.2 Test Gate

* Unit tests
* Integration tests
* Component tests
* End-to-end tests
* Test-result publication
* Flaky-test handling

### 17.3 Lint and Formatting Gate

* Code style
* Static analysis
* Formatting rules
* Documentation checks
* Repository cleanliness

### 17.4 Architecture Gate

* Clean Architecture dependency rules
* Layer boundary enforcement
* Microservice ownership
* Forbidden references
* Domain isolation

### 17.5 Security Gate

* Dependency vulnerability scanning
* Secret scanning
* Static application security testing
* Infrastructure-policy checks
* License checks

### 17.6 Contract Gate

* REST API compatibility
* Event schema compatibility
* Database migration checks
* Consumer contract tests
* Versioning-rule enforcement

### 17.7 Alpha Car Detailing Gate Set

* .NET build
* Automated tests
* Formatting and analyzers
* Clean Architecture dependency tests
* Fleet-booking API contract validation
* Integration-event schema validation
* Security and secret scanning

---

## 18. Hooks and Automated Guardrails

### 18.1 What a Hook Is

* A defined lifecycle extension point.
* A mechanism for running controlled automation before or after an event.

### 18.2 Hooks versus Gates

* Hooks trigger actions.
* Gates determine whether progression is allowed.
* A hook may invoke a gate, but the concepts remain distinct.

### 18.3 Pre-Execution Hooks

* Validate request completeness.
* Confirm repository state.
* Load required configuration.
* Check permissions.
* Create an isolated workspace.
* Detect prohibited files or secrets.

### 18.4 Pre-Tool Hooks

* Validate commands.
* Check paths.
* Apply allowlists.
* Redact sensitive inputs.
* Record intended execution.

### 18.5 Post-Tool Hooks

* Capture outputs.
* Normalize errors.
* Scan generated changes.
* Update workflow state.
* Emit telemetry.

### 18.6 Post-Execution Hooks

* Run mandatory gates.
* Generate reports.
* Clean temporary resources.
* Archive evidence.
* Notify responsible parties.

### 18.7 Automated Guardrails

* File-scope restrictions
* Protected-branch restrictions
* Command restrictions
* Dependency policies
* Maximum retry limits
* Token and cost limits
* Secret-handling policies
* Approval requirements
* Publication controls

### 18.8 Hook Failure Behavior

* Fail open versus fail closed
* Mandatory versus advisory hooks
* Timeout behavior
* Retry behavior
* Audit requirements

### Diagram: Hooks and Guardrails

Show lifecycle events, hook points, policy checks, permitted actions, and blocking outcomes.

---

## 19. Validation versus Evaluation

### 19.1 Deterministic Validation

* Objective
* Repeatable
* Rule-based
* Independently executable
* Produces evidence-backed pass or fail results

### 19.2 AI-Assisted Evaluation

* Judgment-based
* Rubric-driven
* Context-sensitive
* Useful for maintainability, clarity, completeness, and design quality
* Produces findings and scores rather than absolute proof

### 19.3 Human Review and Approval

* Business judgment
* Risk acceptance
* Architectural exceptions
* Production or merge authorization

### 19.4 Correct Execution Order

1. Implementation
2. Deterministic validation
3. AI-assisted review and evaluation
4. Human approval
5. Publication

### 19.5 Prohibited Substitutions

* Evaluation cannot replace compilation.
* Agent review cannot replace tests.
* A high AI score cannot override a failed mandatory gate.
* Human approval should not erase failed evidence without a recorded exception.

### Diagram: Validation and Evaluation Flow

Show deterministic evidence, AI findings, human judgment, and final decision authority.

---

## 20. Retry and Failure Handling

### 20.1 Failure Categories

* Agent failure
* Tool failure
* Gate failure
* Infrastructure failure
* Configuration failure
* Permission failure
* Approval timeout
* Publication failure

### 20.2 Retry Eligibility

* Transient versus permanent failures
* Safe versus unsafe replay
* Idempotent versus non-idempotent steps
* Retry budgets

### 20.3 Retry Strategies

* Immediate retry
* Exponential backoff
* Bounded retry
* Retry with revised context
* Retry after human intervention

### 20.4 Implementation-Correction Loop

* Gate reports failure.
* Harness prepares structured evidence.
* Developer receives the failure package.
* Developer corrects the implementation.
* Harness reruns affected gates.
* Unaffected evidence is reused only when still valid.

### 20.5 Failure Escalation

* Maximum attempt reached
* Repeated identical failure
* Security-policy failure
* Conflicting requirements
* Non-deterministic result
* Required human decision

### 20.6 Compensation and Cleanup

* Revert temporary environment changes.
* Remove abandoned workspaces.
* Release leases.
* Preserve evidence.
* Prevent partial publication.

### Diagram: Failure and Retry Flow

Show failure classification, retry eligibility, correction loop, escalation, and termination.

---

## 21. Human Approval Gates

### 21.1 Approval Scenarios

* Plan approval
* Architecture exception
* Security exception
* High-risk dependency change
* Database migration
* Production-impacting change
* Pull-request publication
* Merge authorization

### 21.2 Approval Context

* Proposed change
* Gate results
* Evaluation summary
* Risks
* Exceptions
* Recommended action
* Supporting evidence

### 21.3 Approval Outcomes

* Approved
* Approved with conditions
* Rejected
* Changes requested
* Escalated
* Expired

### 21.4 Separation of Duties

* Implementer
* Reviewer
* Approver
* Exception authority
* Audit owner

### 21.5 Approval Traceability

* Identity
* Timestamp
* Decision
* Rationale
* Conditions
* Related workflow and commit

---

## 22. Logging and Audit Trails

### 22.1 Operational Logging

* Workflow progress
* Agent execution
* Tool invocation
* Gate execution
* Errors and retries
* Performance data

### 22.2 Audit Events

* Request submitted
* Context loaded
* Permissions granted
* Files modified
* Gate completed
* Exception approved
* Human decision recorded
* Pull request created

### 22.3 Correlation

* Workflow ID
* Execution ID
* Agent session ID
* Gate-run ID
* Repository commit
* Pull-request ID

### 22.4 Sensitive Data Handling

* Secret redaction
* Personal-data filtering
* Prompt and response retention
* Access control
* Retention policies

### 22.5 Audit Integrity

* Append-only records
* Tamper evidence
* Time synchronization
* Identity verification
* Export for compliance review

---

## 23. Metrics and Telemetry

### 23.1 Workflow Metrics

* Completion rate
* Failure rate
* Cycle time
* Approval wait time
* Retry count
* Abandonment rate

### 23.2 Agent Metrics

* Task success rate
* Correction cycles
* Tool failures
* Context consumption
* Cost and token usage
* Handoff quality

### 23.3 Gate Metrics

* Pass and failure rates
* Execution duration
* Flaky-gate rate
* Most common findings
* False-positive rate
* Gate coverage

### 23.4 Quality Metrics

* Defect escape rate
* Review findings
* Acceptance-criteria coverage
* Evaluation scores
* Rework after approval

### 23.5 Distributed Tracing

* Trace request intake through pull-request creation.
* Connect orchestration, agents, tools, gates, and approvals.
* Identify bottlenecks and repeated failure loops.

### 23.6 Metrics Misuse

* Optimizing agent output volume instead of engineering outcomes.
* Treating evaluation scores as objective truth.
* Comparing models without equivalent tasks and controls.
* Ignoring safety and quality to reduce cycle time.

---

## 24. Security and Permissions

### 24.1 Harness Threat Model

* Prompt injection
* Malicious repository content
* Unauthorized tool use
* Secret exposure
* Dependency compromise
* Privilege escalation
* Data exfiltration
* Audit manipulation

### 24.2 Identity Model

* User identity
* Harness service identity
* Agent execution identity
* Tool identity
* Approval identity

### 24.3 Authorization Model

* Role-based access
* Attribute-based policies
* Repository boundaries
* Environment boundaries
* Time-limited permissions

### 24.4 Secret Management

* External secret stores
* Short-lived credentials
* Secret injection
* Output redaction
* Rotation and revocation

### 24.5 Supply-Chain Security

* Approved models and providers
* Tool provenance
* Skill and Prompt versioning
* Dependency integrity
* Signed artifacts
* Trusted execution images

---

## 25. Isolation and Sandboxing

### 25.1 Isolation Boundaries

* Per workflow
* Per repository
* Per agent
* Per tool
* Per tenant
* Per environment

### 25.2 Sandbox Controls

* Filesystem boundaries
* Network boundaries
* Process restrictions
* Resource limits
* Time limits
* Package-installation restrictions

### 25.3 Workspace Strategies

* Temporary working directory
* Git worktree
* Containerized workspace
* Ephemeral virtual machine
* Remote development environment

### 25.4 Cleanup and Retention

* Remove temporary compute.
* Preserve required evidence.
* Retain approved artifacts.
* Revoke temporary credentials.
* Verify cleanup completion.

---

## 26. Harness Configuration

### 26.1 Configuration Areas

* Workflow definitions
* Role definitions
* Agent providers
* Model selection
* Tool registry
* Gate definitions
* Hook definitions
* Retry policies
* Approval policies
* Security policies
* Telemetry settings
* Artifact retention

### 26.2 Configuration Hierarchy

* Enterprise configuration
* Business-unit configuration
* Repository configuration
* Workflow configuration
* Task-level overrides

### 26.3 Configuration Precedence

* Mandatory enterprise policy
* Repository standards
* Workflow defaults
* Approved task override

### 26.4 Configuration Validation

* Schema validation
* Required fields
* Version compatibility
* Referenced component availability
* Permission validation
* Circular-dependency detection

### 26.5 Configuration as Code

* Source control
* Pull-request review
* Versioning
* Testing
* Promotion across environments
* Rollback

---

## 27. Local versus Enterprise Harness Architecture

### 27.1 Local Harness

* Developer workstation
* Local repository
* Local scripts and gates
* Single-user execution
* Lightweight state
* Manual approval

### 27.2 Enterprise Harness

* Central control plane
* Distributed execution workers
* Durable workflow state
* Enterprise identity
* Central policy enforcement
* Managed secrets
* Shared telemetry
* Formal approvals
* Audit retention
* Multi-repository support

### 27.3 Evolution Path

* Scripted local workflow
* Repository-level Harness
* Team Harness service
* Enterprise Harness platform

### 27.4 Selection Criteria

* Team size
* Repository count
* Risk
* Compliance
* Execution volume
* Integration needs
* Operational maturity

---

## 28. Single-Agent versus Multi-Agent Architecture

### 28.1 Single-Agent Architecture

* One agent performs planning and implementation.
* Harness still runs independent gates.
* Suitable for bounded, low-risk work.

### 28.2 Multi-Agent Architecture

* Specialized roles perform separate stages.
* Structured handoffs connect agents.
* Harness retains central workflow authority.

### 28.3 Benefits of Multiple Agents

* Separation of concerns
* Independent perspectives
* Specialized context
* Clearer accountability

### 28.4 Costs of Multiple Agents

* Increased latency
* Greater context-management complexity
* More state transitions
* Higher execution cost
* Risk of contradictory outputs

### 28.5 Choosing an Architecture

* Task complexity
* Risk level
* Required independence
* Repository size
* Cost constraints
* Approval requirements

### 28.6 Hybrid Architecture

* Single developer agent
* Independent reviewer or evaluator
* Deterministic gate pipeline
* Human approval for high-risk changes

---

## 29. Extensibility

### 29.1 Extension Points

* Agent adapters
* Model providers
* Tool adapters
* Repository providers
* Knowledge Source connectors
* Gate plugins
* Hook handlers
* Evaluation rubrics
* Approval providers
* Telemetry exporters

### 29.2 Extension Contracts

* Stable interfaces
* Versioned schemas
* Capability declaration
* Permission requirements
* Failure semantics
* Observability requirements

### 29.3 Safe Extension

* Trust classification
* Sandbox execution
* Configuration validation
* Compatibility testing
* Signed versions
* Controlled rollout

### 29.4 Avoiding Platform Lock-In

* Keep workflow definitions vendor-neutral.
* Separate Harness policy from provider-specific commands.
* Normalize agent, tool, gate, and evaluation outputs.

---

## 30. Alpha Car Detailing Harness Architecture

### 30.1 Corporate Fleet Booking Request

* Add corporate fleet booking capability.
* Preserve Clean Architecture boundaries.
* Support corporate accounts and vehicle groups.
* Publish required integration events.
* Maintain compatibility with existing station operations.

### 30.2 Repository Intelligence Inputs

* Repository Instructions
* Fleet-booking Prompt
* Relevant Skills
* Lead and Developer Roles
* Current Steering Note
* Architecture Decision Records
* API and event contracts

### 30.3 Execution Workflow

1. Harness accepts the request.
2. Context Assembler loads authoritative repository intelligence.
3. Lead produces a bounded plan.
4. Human or policy approves the plan when required.
5. Developer implements the feature.
6. Gate Runner executes independent checks.
7. Developer corrects deterministic failures.
8. Reviewer assesses design and maintainability.
9. Validator checks acceptance criteria.
10. Evaluator applies the quality rubric.
11. Human approver reviews evidence.
12. Harness creates the pull request.

### 30.4 Alpha Car Detailing Gates

* .NET restore and build
* Unit tests
* Integration tests
* Code formatting and analyzers
* Clean Architecture dependency tests
* REST contract checks
* Integration-event schema checks
* Security and secret scans

### 30.5 Hooks and Guardrails

* Pre-execution repository-state check
* Protected-file guardrail
* Pre-tool command validation
* Post-edit change-scope scan
* Post-execution gate trigger
* Pre-publication approval check

### 30.6 State and Evidence

* Workflow record
* Lead plan
* Developer patch
* Gate reports
* Reviewer findings
* Validation results
* Evaluation score
* Human decision
* Pull-request reference

### Diagram: Alpha Car Detailing Harness Architecture

Show the control plane, isolated execution workspace, repository intelligence, agent roles, deterministic gates, approval service, telemetry, and GitHub pull-request integration.

---

## 31. Claude Code Integration

### 31.1 Integration Position

* Claude Code as an agent runtime within the Harness.
* The Harness remains responsible for orchestration and governance.

### 31.2 Repository Context

* Project instructions
* Skills
* Prompts
* Role-specific task packages
* Steering Notes and Knowledge Sources

### 31.3 Tool and Hook Integration

* Controlled shell and repository tools
* Lifecycle hooks
* Permission policies
* Structured execution outputs

### 31.4 Harness Responsibilities That Remain External

* Durable workflow state
* Independent gate execution
* Cross-agent orchestration
* Enterprise approval
* Central audit and telemetry

---

## 32. GitHub Copilot Integration

### 32.1 Integration Position

* Copilot as an interactive or agentic development capability.
* GitHub as the repository and pull-request system.

### 32.2 Repository Guidance

* Repository instructions
* Prompt files
* Coding standards
* Task-specific context

### 32.3 Workflow Integration

* Issue or task intake
* Agent execution
* GitHub Actions gates
* Pull-request review
* Branch protection
* Human approval

### 32.4 Enterprise Controls

* Repository permissions
* Protected branches
* Required status checks
* CODEOWNERS
* Audit records
* Organization policies

---

## 33. OpenAI Codex Integration

### 33.1 Integration Position

* Codex as a bounded coding and execution agent.
* Harness supplies context, tools, permissions, and expected outputs.

### 33.2 Isolated Execution

* Controlled workspace
* Restricted filesystem
* Restricted network
* Approved commands
* Captured artifacts

### 33.3 Workflow Integration

* Role-specific task delegation
* Repository inspection
* Code modification
* Test execution
* Structured handoffs
* Pull-request preparation

### 33.4 Harness Responsibilities That Remain Independent

* Gate authority
* Approval authority
* Workflow state
* Enterprise policy
* Audit and compliance

---

## 34. Hands-on Example

### 34.1 Objective

Design a repository-level Harness architecture for the Alpha Car Detailing corporate fleet booking feature.

### 34.2 Proposed Directory Structure

* `harness/config/`
* `harness/workflows/`
* `harness/roles/`
* `harness/hooks/`
* `harness/gates/`
* `harness/evaluations/`
* `harness/policies/`
* `harness/scripts/`
* `harness/artifacts/`

### 34.3 Workflow Definition

* Request intake
* Discovery
* Planning
* Implementation
* Deterministic validation
* Review
* Validation
* Evaluation
* Approval
* Pull-request creation

### 34.4 Gate Configuration

* Gate identifiers
* Commands
* Ordering
* Dependencies
* Timeouts
* Mandatory status
* Evidence output

### 34.5 Hook Configuration

* Lifecycle event
* Handler
* Permissions
* Timeout
* Failure policy
* Audit behavior

### 34.6 Role Configuration

* Responsibility
* Inputs
* Allowed tools
* Required outputs
* Handoff target
* Prohibited actions

### 34.7 Expected Artifacts

* Workflow manifest
* Agent handoff records
* Gate results
* Evaluation report
* Approval record
* Pull-request summary

---

## 35. Best Practices

* Keep orchestration separate from agent implementation.
* Run deterministic gates outside agent authority.
* Execute fast, objective gates before subjective evaluation.
* Make every workflow transition explicit.
* Use structured and versioned handoff packages.
* Grant tools through least-privilege policies.
* Treat Harness configuration as reviewed source code.
* Preserve evidence for every blocking decision.
* Use bounded retries with failure classification.
* Require human approval for defined high-risk actions.
* Isolate untrusted execution.
* Keep provider integrations replaceable.
* Measure engineering outcomes, not only agent activity.

---

## 36. Anti-patterns

* Allowing an agent to declare its own implementation valid.
* Treating an AI review as a substitute for tests or compilation.
* Combining control-plane and execution-plane privileges.
* Giving every role unrestricted repository and shell access.
* Passing the complete repository context to every agent.
* Retrying failures without classification or limits.
* Ignoring partial state after execution failure.
* Allowing hooks to perform untracked changes.
* Bypassing failed mandatory gates through prompt instructions.
* Storing secrets in Prompts, Skills, logs, or handoff packages.
* Creating multi-agent workflows without clear responsibility boundaries.
* Recording only the final output and discarding execution evidence.
* Binding the entire Harness to one model provider.
* Creating pull requests before required approval is recorded.

---

## 37. Architect’s Notes

* The Harness is a governance and coordination architecture, not merely an agent launcher.
* Deterministic gates form an independent trust boundary.
* Control-plane durability becomes essential as workflows become long-running.
* Multi-agent execution should be introduced only when responsibility separation provides measurable value.
* Human approval is an architectural component, not an informal team convention.
* Hooks require the same security review as other executable extensions.
* Auditability must be designed into the workflow rather than added after deployment.
* Local and enterprise Harnesses should share concepts even when their infrastructure differs.

---

## 38. Enterprise Tips

* Begin with one repository and one well-defined workflow.
* Standardize gate result and agent handoff schemas early.
* Centralize mandatory policies while allowing controlled repository extensions.
* Use enterprise identity for every actor and service.
* Separate operational logs from compliance audit records.
* Establish ownership for gates, hooks, Skills, Prompts, and workflows.
* Test Harness configuration before promotion.
* Maintain an approved catalog of tools and extensions.
* Monitor retry loops, gate instability, approval delays, and cost.
* Introduce cross-repository orchestration only after repository-level workflows are stable.

---

## 39. Decision Points

### 39.1 Control Plane

* Local process or centralized service?
* Stateless orchestration or durable workflow engine?
* Repository-specific or shared enterprise control plane?

### 39.2 Execution Plane

* Host process, container, virtual machine, or remote worker?
* Persistent or ephemeral workspace?
* Shared or per-agent isolation?

### 39.3 Agent Architecture

* Single-agent, multi-agent, or hybrid?
* Fixed roles or dynamically selected roles?
* Shared context or bounded handoff packages?

### 39.4 Gate Architecture

* Local commands, CI jobs, or dedicated validation workers?
* Sequential or parallel execution?
* Mandatory or advisory result?
* Who can approve an exception?

### 39.5 Approval Architecture

* Which stages require approval?
* Who is authorized to approve?
* How are expired or rejected approvals handled?

### 39.6 State and Evidence

* Where is workflow state stored?
* Which artifacts must be retained?
* How long must audit evidence remain available?

### 39.7 Vendor Integration

* Which responsibilities belong to the Harness?
* Which responsibilities can be delegated to Claude Code, GitHub Copilot, or Codex?
* How will provider-specific outputs be normalized?

---

## 40. Exercises

1. Draw a high-level Harness architecture for a single Alpha Car Detailing repository.
2. Separate its components into control-plane and execution-plane responsibilities.
3. Define the workflow states for corporate fleet booking delivery.
4. Create a structured handoff contract between the Lead and Developer roles.
5. Design a deterministic gate pipeline for the .NET solution.
6. Classify each gate as mandatory or advisory.
7. Define pre-execution, pre-tool, post-tool, and post-execution hooks.
8. Create retry rules for agent, tool, gate, and infrastructure failures.
9. Identify the steps that require human approval.
10. Define security boundaries for repository, filesystem, network, and secrets.
11. Compare single-agent and multi-agent approaches for the same feature.
12. Design an enterprise version of the local Harness architecture.
13. Map Claude Code, GitHub Copilot, and Codex into the architecture without transferring gate authority to the agents.
14. Define the audit evidence required before pull-request creation.

---

## 41. Interview Questions

1. What is the difference between an AI agent and an AI Engineering Harness?
2. Why should the control plane be separated from the execution plane?
3. What responsibilities belong to a Harness orchestrator?
4. How does Repository Intelligence integrate with Harness execution?
5. What information should an agent handoff package contain?
6. What makes a validation gate deterministic?
7. Why must deterministic gates remain independent of the implementing agent?
8. How do gates differ from hooks?
9. How do validation and evaluation differ?
10. Can an AI evaluation override a failed build or test gate?
11. How should a Harness classify and retry failures?
12. Why is idempotency important in Harness workflows?
13. What information belongs in a human approval request?
14. How would you enforce least privilege for agent tools?
15. What security risks arise from repository content and Prompt injection?
16. When should a team use a multi-agent architecture?
17. How should Harness configuration be governed?
18. What metrics indicate Harness effectiveness?
19. How does a local Harness differ from an enterprise Harness?
20. How would you integrate Claude Code, GitHub Copilot, or Codex without creating vendor lock-in?

---

## 42. Chapter Summary

* An enterprise AI Engineering Harness separates coordination, execution, validation, evaluation, approval, and evidence management.
* The control plane governs workflows, policies, state, permissions, and approvals.
* The execution plane runs agents, tools, and validation processes in controlled environments.
* Repository Intelligence supplies authoritative context for agent work.
* Role orchestration and structured handoffs establish responsibility boundaries.
* Deterministic gates independently verify objective conditions.
* Agents must not decide whether their own implementations pass mandatory gates.
* Hooks trigger lifecycle automation, while guardrails restrict unsafe actions.
* Validation, evaluation, and human approval provide different kinds of assurance.
* Durable state, bounded retries, audit trails, telemetry, security, and isolation are essential for enterprise operation.
* Provider-specific agents remain replaceable execution components within the broader Harness architecture.

---

## 43. Further Reading

* Workflow orchestration and durable execution
* Policy-as-code
* Secure sandbox design
* Software supply-chain security
* Continuous integration and required status checks
* Architecture fitness functions
* Contract testing
* OpenTelemetry
* Human-in-the-loop system design
* AI agent security and Prompt injection defense
* Claude Code documentation
* GitHub Copilot documentation
* OpenAI Codex documentation

Chapter 13 outline is complete.
