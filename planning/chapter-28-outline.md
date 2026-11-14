# **Chapter 28 — Enterprise Workflows**

**Part IV — Claude Engineering**

**Project:** Enterprise AI Engineering Handbook\
**Target file:** `book/28-enterprise-workflows.md`\
**Running Example:** Alpha Car Detailing\
**Primary Platform:** Claude Code\
**Engineering Approach:** Claude-first, vendor-neutral enterprise principles

## **Chapter Objective**

Explain how Claude Code capabilities—including `CLAUDE.md`, Skills, Commands, Memory, Tool Calling, and Planning Mode—are combined with repository intelligence, agent roles, deterministic validation gates, human oversight, and harness orchestration to establish repeatable enterprise software engineering workflows.

The chapter demonstrates how organizations move beyond isolated AI coding interactions toward controlled engineering processes that support feature development, defect resolution, refactoring, security remediation, and production support.

**Core Principle:**

> Enterprise AI workflows coordinate context, planning, implementation, validation, review, evaluation, and human approval as one controlled, observable, and auditable engineering process.

## **28.1 Story-Driven Opening — From Individual AI Tasks to Enterprise Workflows**

### **28.1.1 The Alpha Car Detailing Engineering Challenge**

- Introducing corporate fleet booking as an enterprise feature.
- Multiple development teams and microservices involved.
- Business requirements, architecture constraints, security expectations, and release deadlines.
- Why a successful AI-generated code change does not necessarily constitute a completed enterprise feature.

### **28.1.2 The Fragmented AI Development Approach**

- Developers independently prompting Claude Code.
- Repository context repeatedly rediscovered.
- Architecture instructions inconsistently applied.
- Testing and review treated as optional follow-up activities.
- Missing traceability between requirements, implementation, and approval.
- Risks of uncontrolled commits and pull requests.

### **28.1.3 Introducing a Controlled Enterprise Workflow**

- Establishing workflow entry and exit conditions.
- Loading authoritative repository instructions.
- Discovering relevant repository intelligence.
- Producing an approved implementation plan.
- Executing implementation through defined agent responsibilities.
- Applying deterministic gates and independent review.
- Evaluating business acceptance criteria.
- Creating a pull request with supporting evidence.
- Requiring human approval at designated control points.

### **28.1.4 The Engineering Outcome**

- Repeatable development behavior.
- Consistent application of architectural standards.
- Traceable evidence for engineering decisions.
- Clear responsibility and approval boundaries.
- Reduced dependence on individual prompt-writing practices.

## **28.2 Learning Objectives**

After completing this chapter, readers should be able to:

1. Define an enterprise AI engineering workflow and distinguish it from an isolated AI interaction.
2. Explain the relationship between Claude Code capabilities and the enterprise harness.
3. Identify workflow inputs, outputs, execution states, and control points.
4. Combine repository discovery, `CLAUDE.md`, Skills, Commands, Memory, Planning Mode, and Tool Calling.
5. Design single-agent and multi-agent engineering workflows.
6. Implement feature development, bug-fix, refactoring, security remediation, and incident-support workflows.
7. Integrate Reviewer, Validator, and Evaluator responsibilities without overlapping authority.
8. Define deterministic validation gates and failure-handling policies.
9. Establish approval requirements for sensitive and irreversible operations.
10. Design workflow state management, retry mechanisms, and recovery procedures.
11. Capture workflow evidence for auditing, governance, and operational metrics.
12. Build reusable workflow templates for enterprise engineering teams.

## **28.3 Background — Understanding Enterprise AI Workflows**

### **28.3.1 What Is an Enterprise AI Workflow?**

- Definition and fundamental purpose.
- A workflow as a structured sequence of engineering activities.
- Coordination between AI agents, deterministic tools, and human participants.
- Workflow orchestration versus task execution.
- Workflow lifecycle and completion criteria.

### **28.3.2 AI Interaction Versus Engineering Workflow**

- Individual prompts versus governed processes.
- Task completion versus business goal satisfaction.
- Ephemeral conversations versus persisted execution records.
- Tool execution versus controlled engineering outcomes.
- Why successful code generation is insufficient evidence of completion.

### **28.3.3 Workflow Versus Harness**

- Workflow as the definition of an engineering process.
- Harness as the execution, coordination, and enforcement mechanism.
- Claude Code as an AI execution environment.
- CI/CD systems as supporting validation and delivery infrastructure.
- Human participants as accountable decision-makers.

### **28.3.4 Workflow Versus Skill Versus Command**

- Skills as reusable task-specific engineering capabilities.
- Commands as explicit workflow entry points or task invocations.
- Workflows as coordinated sequences that may invoke multiple Skills and Commands.
- Harness policies as execution boundaries.
- Avoiding duplication of workflow logic across instructions and scripts.

### **28.3.5 Enterprise Workflow Characteristics**

- Repeatability.
- Deterministic control points.
- Traceability and auditability.
- Security and least privilege.
- Recoverability and idempotency.
- Human accountability.
- Observability and measurable outcomes.
- Extensibility across teams and repositories.

### **28.3.6 Relationship to Earlier Chapters**

- Chapters 5–11: Repository Intelligence.
- Chapters 12–18: Harness architecture and agent responsibilities.
- Chapters 19–21: Harness execution capabilities and enterprise patterns.
- Chapters 22–27: Claude Code implementation capabilities.
- Chapter 28: Integration of those capabilities into practical engineering workflows.

## **28.4 Concepts — Anatomy of an Enterprise Workflow**

### **28.4.1 Workflow Definition**

- Workflow identifier and version.
- Business or engineering objective.
- Trigger and initiation context.
- Preconditions and required permissions.
- Ordered activities and dependencies.
- Responsible agents and human owners.
- Validation and approval requirements.
- Completion and failure conditions.

### **28.4.2 Workflow Inputs**

- User request or engineering ticket.
- Business goal and acceptance criteria.
- Repository and target branch.
- Steering Notes and project instructions.
- Architecture and coding constraints.
- Existing tests and engineering contracts.
- Relevant security and compliance policies.
- Authorized tools and execution limits.

### **28.4.3 Workflow Outputs**

- Implementation plan.
- Modified source and configuration files.
- Tests and validation reports.
- Review findings and resolutions.
- Evaluation results.
- Approval records.
- Commits and pull-request references.
- Execution logs, metrics, and final workflow outcome.

### **28.4.4 Workflow Preconditions and Postconditions**

- Repository access and worktree cleanliness.
- Dependency availability.
- Branch protection requirements.
- Baseline validation.
- Required business and architecture context.
- Expected resulting repository state.
- Evidence required before completion.

### **28.4.5 Workflow Stages**

- Intake.
- Discovery.
- Planning.
- Implementation.
- Validation.
- Review.
- Evaluation.
- Approval.
- Pull-request preparation and creation.
- Final reporting and closure.

### **28.4.6 Workflow Control Points**

- Mandatory versus optional stages.
- Hard gates and advisory checks.
- Risk-based approvals.
- Stage transition conditions.
- Handling inconclusive results.
- Preventing unauthorized stage bypasses.

### **28.4.7 Workflow Ownership**

- Engineering requester.
- Technical Lead or Architect.
- Harness operator.
- AI agent roles.
- Human reviewer and approver.
- Security and operations responsibilities.

## **28.5 Combining Claude Code Capabilities**

### **28.5.1 Repository Discovery**

- Identifying project structure and technology boundaries.
- Discovering relevant microservices and modules.
- Understanding API and event contracts.
- Locating tests and existing implementation patterns.
- Detecting dependency and architectural relationships.
- Reusing Repository Intelligence instead of repeatedly reconstructing it.

### **28.5.2&#x20;**`CLAUDE.md`**&#x20;as Persistent Engineering Instructions**

- Loading repository and scoped instructions.
- Establishing architecture and coding conventions.
- Defining testing and security expectations.
- Clarifying project-specific execution boundaries.
- Instruction precedence and conflicts.
- Version control and authorized ownership of instructions.

### **28.5.3 Skills as Reusable Engineering Capabilities**

- Feature implementation Skills.
- API design Skills.
- Testing Skills.
- Refactoring Skills.
- Security analysis Skills.
- Observability Skills.
- Composing Skills without creating competing sources of authority.
- Skills as reusable procedures rather than unconditional permissions.

### **28.5.4 Commands as Workflow Entry Points**

- Initiating feature development.
- Starting defect investigation.
- Running approved refactoring procedures.
- Invoking validation.
- Preparing review and evaluation.
- Passing structured task parameters.
- Separating command invocation from authorization.

### **28.5.5 Planning Mode**

- Inspecting repository context before implementation.
- Decomposing requirements into executable tasks.
- Identifying affected components and architectural risks.
- Establishing test and validation strategies.
- Presenting plans for review where required.
- Distinguishing planning approval from implementation approval.
- Transitioning from plan to execution without losing constraints.

### **28.5.6 Tool Calling**

- File inspection and editing.
- Repository search and code navigation.
- Build and test execution.
- Git operations.
- Static analysis and security tooling.
- Interacting with approved external systems.
- Tool permission boundaries.
- Tool result verification and error handling.

### **28.5.7 Claude Memory and Harness Memory**

- Session context and persisted project knowledge.
- Claude Code memory and instruction scope.
- Distinguishing personal/session memory from repository-authoritative knowledge.
- Harness Memory as curated evidence from previous executions.
- Read-only retrieval versus governed memory updates.
- Preventing unverified observations from becoming engineering standards.

### **28.5.8 Scratchpad and Run Workspace**

- Run-scoped temporary working state.
- Implementation notes and intermediate observations.
- Plan checkpoints.
- Tool execution results.
- Agent handoff artifacts.
- Scratchpad versus authoritative instructions.
- Isolation across concurrent runs.
- Cleanup, retention, and selective promotion of approved knowledge.

### **28.5.9 Steering Notes and Knowledge Sources**

- Current engineering priorities.
- Task-specific architectural direction.
- Approved reference documents.
- Business rules and acceptance criteria.
- Resolving conflicting or stale knowledge.
- Preserving human ownership of Steering Notes.

### **28.5.10 Capability Composition**

- How the capabilities interact during an engineering task.
- Authoritative context versus temporary execution state.
- Where the harness coordinates Claude Code activities.
- Preventing conflicting instructions and duplicated responsibilities.
- Maintaining separation between AI decisions and deterministic enforcement.

## **28.6 Architecture Discussion — Enterprise Workflow Orchestration**

### **28.6.1 Reference Architecture**

- Workflow entry layer.
- Context and Repository Intelligence layer.
- Planning and task decomposition layer.
- AI execution layer.
- Harness orchestration layer.
- Deterministic validation layer.
- Review and evaluation layer.
- Approval and governance layer.
- Evidence and observability layer.
- Version control and delivery integration.

### **28.6.2 Workflow Orchestrator Responsibilities**

- Start and track workflow runs.
- Load the workflow definition.
- Resolve dependencies and stage ordering.
- Assign agent responsibilities.
- Enforce budgets, permissions, and limits.
- Invoke deterministic gates.
- Coordinate failure recovery.
- Maintain authoritative workflow state.
- Publish outcomes and evidence.

### **28.6.3 Agent Roles and Responsibility Boundaries**

| Role            | Primary responsibility                              | Authority boundary                            |
| --------------- | --------------------------------------------------- | --------------------------------------------- |
| Lead Agent      | Analyze the request and coordinate execution        | Cannot bypass governance or approval          |
| Developer Agent | Implement approved changes                          | Cannot self-certify completion                |
| Reviewer Agent  | Assess implementation and architecture quality      | Does not replace deterministic checks         |
| Validator Agent | Execute and interpret objective validation evidence | Does not judge overall business success       |
| Evaluator Agent | Assess goal satisfaction and acceptance criteria    | Does not replace validation or human approval |
| Human Approver  | Authorize designated decisions and operations       | Accountable within enterprise policy          |

### **28.6.4 Single-Agent Workflow Architecture**

- One AI execution context performing multiple activities.
- Logical separation of role responsibilities.
- Independent external validation.
- Advantages for bounded, low-risk changes.
- Limitations of self-review and shared context.
- Human oversight requirements.

### **28.6.5 Multi-Agent Workflow Architecture**

- Lead Agent coordinating specialized roles.
- Explicit handoff contracts.
- Evidence-driven communication.
- Shared context versus isolated execution contexts.
- Avoiding uncontrolled agent-to-agent delegation.
- Handling conflicting recommendations.
- Preventing concurrent changes to the same files.

### **28.6.6 Sequential and Parallel Execution**

- Sequential stages with dependent outcomes.
- Parallelizable independent investigations.
- Controlled test execution.
- Synchronization and merge points.
- Resource constraints and concurrency limits.
- Deterministic reconciliation before completion.

### **28.6.7 Deterministic Gates Architecture**

- Build validation.
- Unit and integration tests.
- Architecture conformance tests.
- API and event contract checks.
- Static analysis.
- Security and dependency checks.
- Database migration validation.
- Gate ordering and early termination.
- Failure evidence and retry eligibility.

### **28.6.8 Workflow State Management**

- Workflow states: Created, Discovering, Planning, Awaiting Approval, Implementing, Validating, Reviewing, Evaluating, PR Ready, Completed, Failed, and Cancelled.
- Stage execution state versus overall workflow state.
- Run identifiers and correlation identifiers.
- State persistence and checkpointing.
- Valid transitions and rejected transitions.
- Recovery after orchestrator interruptions.
- Cancellation and expiration.

### **28.6.9 State and Artifact Persistence**

- Durable workflow metadata.
- Run-scoped scratchpad files.
- Validation evidence.
- Review and evaluation artifacts.
- Approval decisions.
- Git commit and pull-request references.
- Retention policies and cleanup behavior.

### **28.6.10 Integration with Git and CI/CD**

- Isolated feature branches or worktrees.
- Repository permission boundaries.
- Commit preparation.
- Pull-request creation.
- Required CI checks.
- Branch protection policies.
- Separation between PR creation, approval, merging, and deployment.
- Preventing AI execution from bypassing protected delivery controls.

## **28.7 Professional Diagrams**

All diagrams should use consistent visual conventions, clearly distinguish AI-driven actions from deterministic gates, and identify human approval boundaries.

### **28.7.1 Diagram 28-1 — End-to-End Enterprise Workflow**

- Request intake.
- Repository discovery.
- Instruction and knowledge loading.
- Planning.
- Implementation.
- Deterministic validation.
- Independent review.
- Outcome evaluation.
- Human approval.
- Pull request and completion reporting.
- Feedback and rework paths.

### **28.7.2 Diagram 28-2 — Feature Development Workflow**

- Corporate fleet booking request.
- Requirements interpretation.
- Architecture impact analysis.
- Implementation plan.
- API and domain implementation.
- Persistence and event integration.
- Tests and contract validation.
- Review and evaluation.
- Pull-request preparation and approval checkpoints.

### **28.7.3 Diagram 28-3 — Bug-Fix Workflow**

- Defect report.
- Reproduction.
- Root-cause investigation.
- Baseline failing test.
- Targeted fix.
- Regression validation.
- Independent review.
- Closure evidence.

### **28.7.4 Diagram 28-4 — Multi-Agent Workflow**

- Lead Agent as coordinator.
- Developer Agent execution.
- Reviewer and Validator as independent controls.
- Evaluator assessing outcome evidence.
- Human decision authority.
- Handoff artifacts and bounded feedback loops.

### **28.7.5 Diagram 28-5 — Failure and Retry Flow**

- Stage execution.
- Success or failure classification.
- Transient versus deterministic failure.
- Retry eligibility.
- Attempt limits.
- Recovery checkpoint.
- Escalation or terminal failure.
- Preservation of evidence.

### **28.7.6 Diagram 28-6 — Human Approval Flow**

- Risk classification.
- Approval requirement determination.
- Approval request with evidence.
- Approve, reject, or request changes.
- Revalidation after material changes.
- Expiration or revocation.
- Authorized continuation.

### **28.7.7 Diagram 28-7 — Workflow State Machine**

- Main workflow states.
- Allowed transitions.
- Approval-waiting state.
- Retry and rework transitions.
- Cancellation and failure outcomes.
- Terminal state conditions.

### **28.7.8 Diagram 28-8 — Enterprise Workflow Component Architecture**

- Claude Code capabilities.
- Harness components.
- Agent roles.
- Repository Intelligence.
- Execution workspace.
- Tool adapters.
- Validation pipeline.
- Evidence store.
- CI/CD and Git provider integration.

## **28.8 Hands-on Example — Alpha Car Detailing Enterprise Workflows**

### **28.8.1 Example Environment**

- Existing Alpha Car Detailing .NET 10 solution.
- Clean Architecture boundaries.
- Corporate booking domain.
- SQL Server persistence.
- Existing API and event contracts.
- Automated tests and validation scripts.
- Claude Code and harness execution environment.

### **28.8.2 Preparing the Workflow Repository**

- Organizing workflow definitions and templates.
- Locating repository instructions.
- Identifying existing Skills and Commands.
- Preparing run workspace conventions.
- Connecting harness scripts and validation tools.
- Documenting required local dependencies and permissions.

### **28.8.3 Scenario A — Corporate Fleet Booking Feature**

**Business requirement:** Allow approved corporate customers to submit fleet booking requests while enforcing contract-specific booking limits.

Cover:

- Capturing business requirements.
- Defining success criteria.
- Discovering relevant booking components.
- Planning changes across API, domain, application, and persistence layers.
- Applying reusable implementation Skills.
- Implementing business rule enforcement.
- Updating tests and contracts.
- Executing deterministic gates.
- Performing review and evaluation.
- Preparing a traceable pull request.

**Expected artifacts:**

- Implementation plan.
- Source changes.
- Unit and integration tests.
- Validation report.
- Review findings.
- Acceptance criteria evaluation.
- Pull-request summary.

### **28.8.4 Scenario B — Booking Limit Defect**

**Problem:** Corporate customers occasionally exceed their permitted number of active fleet bookings.

Cover:

- Reproducing the defect.
- Investigating concurrent requests.
- Identifying the responsible domain or persistence boundary.
- Creating a regression test.
- Applying the smallest justified correction.
- Checking transactional consistency.
- Validating under concurrency.
- Producing a root-cause report.
- Creating a controlled bug-fix pull request.

### **28.8.5 Scenario C — Booking Service Refactoring**

**Problem:** Booking orchestration logic has become difficult to maintain.

Cover:

- Identifying code complexity.
- Establishing current behavioral contracts.
- Preserving external API behavior.
- Planning incremental refactoring.
- Applying Clean Architecture boundaries.
- Running characterization and regression tests.
- Measuring changes in complexity and maintainability.
- Reviewing unintended behavioral changes.
- Distinguishing improvement from scope expansion.

### **28.8.6 Scenario D — Security Remediation**

**Problem:** A security review identifies insufficient authorization checks on corporate booking operations.

Cover:

- Classifying the vulnerability.
- Identifying affected endpoints.
- Mapping authorization requirements.
- Selecting an approved remediation strategy.
- Implementing authorization enforcement.
- Adding positive and negative security tests.
- Running static and dependency checks.
- Performing independent security review.
- Requiring appropriate approval before merging.

### **28.8.7 Scenario E — Production Incident Support**

**Problem:** Duplicate booking events are causing inconsistent downstream processing.

Cover:

- Incident intake and severity classification.
- Read-only investigation first.
- Log, trace, and event correlation.
- Determining whether retries are producing duplicate processing.
- Evaluating idempotent consumer behavior.
- Planning a safe remediation.
- Maintaining production access restrictions.
- Validating fixes outside production.
- Approval for production-affecting actions.
- Post-incident review and evidence capture.

### **28.8.8 Comparing the Five Workflows**

| Workflow             | Primary objective                                | Critical control                             |
| -------------------- | ------------------------------------------------ | -------------------------------------------- |
| Feature development  | Deliver new business capability                  | Acceptance criteria evaluation               |
| Bug fix              | Restore correct behavior                         | Reproduction and regression evidence         |
| Refactoring          | Improve internal design without behavior changes | Behavioral equivalence                       |
| Security remediation | Reduce verified security exposure                | Security validation and approval             |
| Production incident  | Restore service safely                           | Operational controls and human authorization |

## **28.9 Claude Example — Implementing Controlled Workflows with Claude Code**

### **28.9.1 Claude Code Workflow Entry**

- Preparing Claude Code for a repository task.
- Loading relevant `CLAUDE.md` instructions.
- Selecting the appropriate Skill or Command.
- Establishing task scope and permitted tools.
- Recording initial workflow metadata.

### **28.9.2 Planning a Feature Implementation**

- Using Planning Mode to analyze corporate booking requirements.
- Identifying affected files and contracts.
- Producing testable implementation steps.
- Reviewing the proposed plan.
- Establishing execution checkpoints.

### **28.9.3 Invoking Reusable Skills**

- Choosing a relevant repository Skill.
- Combining domain implementation and testing capabilities.
- Passing task-specific information.
- Recording Skill invocation evidence.
- Avoiding excessive capability chaining.

### **28.9.4 Executing Tools**

- Repository exploration.
- Source changes.
- Build and tests.
- Reading execution results.
- Capturing tool failures.
- Enforcing tool access restrictions.

### **28.9.5 Coordinating Agent Responsibilities**

- Lead Agent planning.
- Developer Agent implementation.
- Validator Agent execution.
- Reviewer Agent inspection.
- Evaluator Agent assessment.
- Human decision checkpoints.

### **28.9.6 Implementing Workflow Commands**

- Feature development entry command.
- Bug-fix entry command.
- Refactoring entry command.
- Security remediation entry command.
- Validation and review invocation.
- Command argument design and input validation.

### **28.9.7 Integrating Harness Automation**

- PowerShell-based orchestration examples.
- Stage execution and exit codes.
- Structured results for validation.
- Run workspace creation.
- Checkpoint persistence.
- Evidence consolidation.

### **28.9.8 Preparing the Pull Request**

- Reviewing the final change set.
- Verifying required gates.
- Summarizing architecture and implementation decisions.
- Linking acceptance criteria to evidence.
- Preparing the PR description.
- Ensuring human and branch-protection policies remain authoritative.

### **28.9.9 Example Outputs**

- Workflow definition.
- Run manifest.
- Implementation plan.
- Gate results.
- Review report.
- Evaluation report.
- PR evidence package.
- Final execution summary.

## **28.10 GitHub Copilot Comparison**

### **28.10.1 Workflow Capabilities**

- Repository instructions.
- Agent capabilities and reusable guidance.
- Planning and task execution.
- Tool and terminal integration.
- Pull-request workflows.
- Enterprise governance integration.

### **28.10.2 Mapping Concepts**

- `CLAUDE.md` and applicable Copilot repository instructions.
- Claude Skills and comparable reusable agent capabilities.
- Claude Commands and available task-entry mechanisms.
- Planning Mode and comparable planning workflows.
- Tool Calling and available tool integrations.

### **28.10.3 Enterprise Integration Differences**

- Differences in configuration and execution experience.
- Repository and organization controls.
- Human review and PR integration.
- Workflow evidence and observability.
- Platform-specific capability limitations.

### **28.10.4 Vendor-Neutral Workflow Architecture**

- Preserving core workflow definitions independently of the AI platform.
- Adapting instructions and execution capabilities.
- Reusing deterministic gates and approval policies.
- Avoiding platform-specific orchestration assumptions.

## **28.11 Codex Comparison**

### **28.11.1 Codex Workflow Model**

- Repository-oriented engineering tasks.
- Task planning and execution.
- Repository instructions through supported mechanisms.
- Tool use and validation.
- Git and pull-request integration.

### **28.11.2 Capability Mapping**

- Claude Code instructions versus Codex instructions.
- Reusable Skills and comparable task procedures.
- Command entry points.
- Context and memory management.
- Planning workflows.
- Execution environment and tool permissions.

### **28.11.3 Workflow Portability**

- Platform-neutral workflow contracts.
- Role and artifact portability.
- Validation and review independence.
- Tool adapter design.
- Handling differences in approval and execution mechanisms.

### **28.11.4 Enterprise Architectural Considerations**

- Platform capability boundaries.
- Access controls.
- Evidence generation.
- Reproducibility.
- Audit requirements.
- Avoiding vendor lock-in.

## **28.12 Workflow Reliability — Failure, Retry, and Recovery**

### **28.12.1 Common Failure Categories**

- Invalid task inputs.
- Missing repository context.
- Planning failures.
- Tool invocation errors.
- Build failures.
- Test failures.
- Architecture violations.
- Security findings.
- Review rejection.
- Evaluation failure.
- Approval rejection or timeout.
- CI and Git provider failures.

### **28.12.2 Failure Classification**

- Transient failures.
- Deterministic implementation failures.
- Environment or infrastructure failures.
- Policy failures.
- Inconclusive evidence.
- Non-recoverable workflow failures.

### **28.12.3 Retry Strategies**

- Retry eligibility rules.
- Bounded attempts.
- Backoff for transient failures.
- Targeted re-execution.
- Avoiding repeated execution of expensive stages.
- Never treating a failed deterministic gate as successful because retries are exhausted.

### **28.12.4 Rework Versus Retry**

- Retry as repetition of a failed operation.
- Rework as a change to implementation or plan.
- New validation evidence after rework.
- Reopening review after material changes.
- Maintaining iteration history.

### **28.12.5 Recovery and Resume**

- Persisting checkpoints.
- Resuming interrupted workflows.
- Determining completed versus incomplete operations.
- Preventing duplicate commits or pull requests.
- Revalidating stale outputs.
- Handling abandoned or orphaned runs.

### **28.12.6 Idempotency and Side-Effect Control**

- Safe repetition of tool operations.
- Idempotency keys and operation identifiers.
- Detecting partially completed external actions.
- Compensation where supported.
- Escalating irreversible side effects.

### **28.12.7 Failure Budgets and Escalation**

- Maximum execution iterations.
- Resource and cost limits.
- Time limits.
- Repeated failure thresholds.
- Mandatory human intervention.
- Preserving unsuccessful execution evidence.

## **28.13 Human Approval and Controlled Delivery**

### **28.13.1 Approval as a First-Class Workflow Stage**

- Human decision responsibility.
- Approval versus automated gate success.
- Risk-based approval placement.
- Avoiding unnecessary approvals for routine low-risk tasks.

### **28.13.2 Approval Categories**

- Implementation plan approval.
- Architecture exception approval.
- Security exception approval.
- Sensitive data access approval.
- Pull-request review and merge approval.
- Production change approval.

### **28.13.3 Approval Evidence**

- Task objective.
- Implementation summary.
- Changed components.
- Validation results.
- Review findings.
- Evaluation outcome.
- Security impact.
- Known residual risks.

### **28.13.4 Approval Decision Handling**

- Approved.
- Rejected.
- Changes requested.
- Deferred.
- Expired or revoked.
- Reapproval after material changes.

### **28.13.5 Segregation of Duties**

- Developer versus reviewer independence.
- Validator evidence versus evaluator judgment.
- AI recommendation versus human authorization.
- Production deployment authority.
- Preventing AI agents from self-approving privileged changes.

## **28.14 Workflow Security and Governance**

### **28.14.1 Workflow Threat Model**

- Untrusted repository content.
- Malicious tool output.
- Prompt injection.
- Secret exposure.
- Unauthorized command execution.
- Dependency and supply-chain attacks.
- Agent privilege escalation.
- Tampering with validation evidence.

### **28.14.2 Least-Privilege Execution**

- Scoped repository credentials.
- Read-only discovery where practical.
- Restricted command execution.
- Environment and network isolation.
- Separation of development and production credentials.

### **28.14.3 Trusted and Untrusted Context**

- Authoritative instructions.
- Repository documents.
- External knowledge sources.
- Retrieved web content.
- Logs and diagnostic output.
- Preventing untrusted content from overriding approved Instructions.

### **28.14.4 Tool Calling Security**

- Tool allowlists.
- Command restrictions.
- Sandboxed execution.
- External API authorization.
- Secrets management.
- Audit logging.
- Approval for sensitive or irreversible operations.

### **28.14.5 Workflow Template Governance**

- Template ownership.
- Versioning and change review.
- Approval policy mapping.
- Compatibility checks.
- Controlled rollout.
- Deprecation and replacement.

### **28.14.6 Governance Boundaries**

- Workflow execution policy.
- Organization-wide AI policy.
- Repository branch protection.
- Security and compliance controls.
- Human decision authority.
- Relationship to Part V — Enterprise AI Governance.

## **28.15 Auditability, Observability, and Metrics**

### **28.15.1 Workflow Audit Trail**

- Request origin and identity.
- Workflow version.
- Repository and commit identity.
- Agent assignments.
- Tool invocations.
- State transitions.
- Gate results.
- Approval decisions.
- Pull-request references.

### **28.15.2 Evidence Integrity**

- Structured execution records.
- Timestamps and correlation identifiers.
- Artifact hashes where appropriate.
- Tamper-resistant storage.
- Evidence retention and access restrictions.
- Avoiding unnecessary capture of secrets and sensitive content.

### **28.15.3 Workflow Observability**

- Stage-level execution logs.
- Structured events.
- Failure classification.
- Execution durations.
- Retry counts.
- Tool execution telemetry.
- Distributed tracing across harness components.

### **28.15.4 Operational Metrics**

- Workflow completion rate.
- Gate pass and failure rates.
- Average attempts per task.
- Retry and rework frequency.
- Approval waiting time.
- PR acceptance and rejection trends.
- Workflow abandonment rate.
- Cost and resource utilization.

### **28.15.5 Quality and Outcome Metrics**

- Regression escape rate.
- Reopened defects.
- Acceptance criteria satisfaction.
- Review finding recurrence.
- Security finding closure.
- Post-merge defects.
- Evidence completeness.
- Workflow reliability over time.

### **28.15.6 Metric Interpretation**

- Avoiding misleading productivity claims.
- Distinguishing volume from quality.
- Accounting for task complexity.
- Comparing workflows across similar task categories.
- Avoiding incentives to bypass gates.
- Privacy and responsible use of engineering telemetry.

## **28.16 Enterprise Workflow Templates**

### **28.16.1 Why Workflow Templates Matter**

- Standardizing recurring engineering processes.
- Reducing duplicated configuration.
- Consistent enforcement of policies.
- Reuse across teams and projects.
- Supporting controlled customization.

### **28.16.2 Standard Template Structure**

- Template identifier and version.
- Purpose and supported task types.
- Inputs and required context.
- Required agent roles.
- Stage definitions and dependencies.
- Skill and Command references.
- Tool permissions.
- Validation gates.
- Approval requirements.
- Retry and recovery policies.
- Outputs and evidence requirements.
- Completion criteria.

### **28.16.3 Feature Development Template**

- Requirement intake.
- Repository discovery.
- Planning.
- Implementation.
- Validation.
- Review.
- Evaluation.
- Approval and PR creation.

### **28.16.4 Bug-Fix Template**

- Defect intake.
- Reproduction.
- Root-cause investigation.
- Regression test.
- Targeted correction.
- Validation.
- Review and resolution.

### **28.16.5 Refactoring Template**

- Behavioral baseline.
- Architecture assessment.
- Change planning.
- Incremental modification.
- Behavioral equivalence testing.
- Review and approval.

### **28.16.6 Security Remediation Template**

- Finding intake.
- Risk classification.
- Root-cause assessment.
- Remediation planning.
- Implementation.
- Security validation.
- Independent review.
- Risk acceptance or approval where required.

### **28.16.7 Production Incident Template**

- Incident classification.
- Read-only investigation.
- Root-cause hypothesis.
- Containment recommendations.
- Controlled remediation.
- Approval.
- Verification.
- Post-incident documentation.

### **28.16.8 Template Composition and Customization**

- Common reusable stages.
- Parameterized policies.
- Project-specific overrides.
- Inheritance versus composition.
- Avoiding overly complex workflow definitions.

### **28.16.9 Template Testing and Versioning**

- Schema validation.
- Transition tests.
- Failure-path tests.
- Tool contract tests.
- Approval-policy tests.
- Backward compatibility.
- Safe template migrations.

## **28.17 Best Practices**

### **28.17.1 Workflow Design**

- Begin with explicit business outcomes.
- Define inputs, outputs, and acceptance criteria.
- Keep stages focused and independently verifiable.
- Use the simplest suitable workflow architecture.
- Separate planning, implementation, verification, and authorization.
- Make workflow completion evidence-based.

### **28.17.2 Context and Instructions**

- Treat repository Instructions as authoritative only within their approved scope.
- Use Repository Intelligence rather than unnecessary full-repository loading.
- Keep Steering Notes human-governed.
- Use scratchpads only for temporary execution state.
- Separate persistent knowledge from unverified observations.

### **28.17.3 Execution and Reliability**

- Use reusable Skills for common engineering activities.
- Prefer deterministic checks for objective conditions.
- Bound retries and execution budgets.
- Persist state before external side effects.
- Make resumable operations idempotent wherever possible.
- Preserve evidence from failed runs.

### **28.17.4 Quality and Governance**

- Ensure reviewer independence appropriate to task risk.
- Keep Validator and Evaluator responsibilities distinct.
- Require revalidation after relevant code changes.
- Use risk-based human approval.
- Preserve branch protection and CI/CD requirements.
- Maintain traceable workflow and template versions.

### **28.17.5 Operational Effectiveness**

- Measure quality alongside throughput.
- Monitor workflow bottlenecks.
- Review recurring failures.
- Refine templates through approved changes.
- Prefer predictable engineering outcomes over maximum automation.

## **28.18 Anti-patterns**

### **28.18.1 Prompt Chains Without Workflow State**

- Treating a collection of prompts as a reliable orchestration mechanism.
- Losing context and completion status between steps.

### **28.18.2 One Agent Does Everything Without Independent Checks**

- Implementation, review, and validation performed without meaningful separation.
- Confusing self-assessment with objective evidence.

### **28.18.3 Skipping Planning for Complex Changes**

- Editing across services without impact analysis.
- Discovering architectural constraints after implementation.

### **28.18.4 Treating&#x20;**`CLAUDE.md`**&#x20;as a Workflow Engine**

- Overloading persistent Instructions with dynamic task orchestration.
- Mixing policies with run-specific state.

### **28.18.5 Treating Skills as Unrestricted Automation**

- Invoking Skills without validating their scope.
- Assuming a reusable procedure grants execution authority.

### **28.18.6 Treating Scratchpad Content as Authoritative Knowledge**

- Promoting temporary observations into persistent standards.
- Allowing one execution to influence unrelated runs without review.

### **28.18.7 Infinite Retry Loops**

- Repeating deterministic failures.
- Exhausting execution budgets.
- Hiding persistent defects behind repeated attempts.

### **28.18.8 Automatic Approval of AI-Generated Changes**

- Self-approved pull requests.
- Unreviewed sensitive modifications.
- Bypassing enterprise delivery policies.

### **28.18.9 Generating a Pull Request Without Evidence**

- Missing test results.
- Unresolved review findings.
- Unknown acceptance criteria status.
- Incomplete change descriptions.

### **28.18.10 Excessive Multi-Agent Complexity**

- Introducing agents without clear responsibility boundaries.
- Duplicated analysis and inconsistent findings.
- Expensive coordination without measurable improvement.

### **28.18.11 Silent Workflow Failure**

- Lost execution state.
- Missing audit records.
- Undetected partial side effects.
- Incorrectly reporting completion.

### **28.18.12 Optimizing for Speed Instead of Engineering Quality**

- Measuring only lines of code or task completion counts.
- Ignoring escaped defects, rework, and security findings.

## **28.19 Architect's Notes**

### **28.19.1 Workflows Are Engineering Contracts**

- A workflow defines expected behavior, control points, and evidence.
- The harness enforces the workflow rather than trusting agent assertions.

### **28.19.2 AI Reasoning Must Not Become the Sole Control Mechanism**

- AI agents interpret requirements and propose changes.
- Deterministic systems enforce verifiable technical conditions.
- Humans retain designated approval authority.

### **28.19.3 Design for Recoverability from the Beginning**

- Failures are normal operating conditions.
- Persist state at meaningful boundaries.
- Preserve failed-run evidence.
- Make resumption behavior explicit.

### **28.19.4 Separate Process Policy from Platform Implementation**

- Workflow definitions should not depend unnecessarily on one AI provider.
- Claude Code is the primary execution platform.
- Governance and outcome contracts should remain portable.

### **28.19.5 Multi-Agent Systems Require Clear Ownership**

- Different roles must provide distinct value.
- Coordination must not obscure accountability.
- Independence matters more than agent count.

### **28.19.6 Approval Is an Architectural Boundary**

- High-risk operations must be independently authorized.
- Approval should be linked to immutable or identifiable evidence.
- Material changes may invalidate previous approval.

### **28.19.7 A Workflow Is Complete Only When Its Exit Contract Is Satisfied**

- Generated code alone is insufficient.
- Required gates, reviews, evaluations, and approvals must be resolved.
- Completion must be supported by verifiable evidence.

## **28.20 Enterprise Tips**

### **28.20.1 Start with a Small Workflow Catalog**

- Feature development.
- Bug fixing.
- Refactoring.
- Security remediation.
- Incident investigation.

### **28.20.2 Establish a Standard Run Manifest**

- Unique run identity.
- Workflow and template version.
- Repository reference.
- Current state.
- Assigned responsibilities.
- Evidence locations.
- Final outcome.

### **28.20.3 Standardize Gate Results**

- Consistent pass, fail, skipped, and inconclusive statuses.
- Machine-readable outputs.
- Clear failure ownership.
- Evidence references.

### **28.20.4 Use Risk-Based Approval Policies**

- Low-risk changes.
- Architectural changes.
- Security-sensitive changes.
- Production-impacting changes.

### **28.20.5 Keep Production Access Separate**

- Development agents should not inherit production privileges.
- Require explicit authorization for operational actions.
- Prefer read-only investigation during incidents.

### **28.20.6 Integrate with Existing Engineering Systems**

- GitHub or Azure DevOps.
- Issue tracking.
- CI/CD pipelines.
- Test reporting.
- Security scanning.
- Observability systems.

### **28.20.7 Establish Template Ownership**

- Technical Lead or architecture ownership.
- Security oversight.
- Versioned changes.
- Controlled rollout.
- Feedback-driven improvement.

### **28.20.8 Create an Evidence-First Engineering Culture**

- Require objective proof of success.
- Make failures explainable.
- Capture decisions and residual risks.
- Avoid unverified claims of completion.

## **28.21 Decision Points**

### **28.21.1 When Should an Enterprise Workflow Be Introduced?**

- Repeated manual AI tasks.
- Inconsistent engineering outcomes.
- Multi-stage development requirements.
- Audit or approval expectations.
- Need for recoverable execution.

### **28.21.2 Single-Agent or Multi-Agent?**

- Task complexity.
- Risk level.
- Need for independent assessment.
- Coordination overhead.
- Cost and performance considerations.

### **28.21.3 Command, Skill, or Workflow?**

- One-time task initiation.
- Reusable engineering procedure.
- Multi-stage governed process.
- Role of the harness in each case.

### **28.21.4 When Is Planning Approval Required?**

- Changes across multiple services.
- Public API modifications.
- Database migrations.
- Security-sensitive implementation.
- Significant architecture changes.

### **28.21.5 Which Gates Should Be Mandatory?**

- Baseline build and tests.
- Architecture validation.
- Security requirements.
- Contract compatibility.
- Risk-based additional checks.

### **28.21.6 When Should a Failure Trigger Retry or Escalation?**

- Transient tool failure.
- Deterministic test failure.
- Environment problem.
- Repeated unsuccessful remediation.
- Policy or approval failure.

### **28.21.7 When May a Pull Request Be Created?**

- Implementation completed.
- Required evidence available.
- Blocking findings resolved.
- Required pre-PR authorization satisfied.
- Remaining post-PR approval controls identified.

### **28.21.8 When Should a Workflow Be Considered Complete?**

- Required stages completed.
- Gate evidence recorded.
- Review and evaluation resolved.
- Required approvals satisfied.
- Delivery state recorded.
- Final outcome and residual risks documented.

## **28.22 Exercises**

### **Exercise 28.1 — Design a Feature Development Workflow**

Create an enterprise workflow for Alpha Car Detailing corporate fleet booking.

**Required deliverables:**

- Workflow stages.
- Inputs and outputs.
- Agent responsibilities.
- Deterministic gates.
- Approval checkpoints.
- Completion criteria.

### **Exercise 28.2 — Build a Claude Code Workflow Entry Command**

Design a Command that initiates a controlled feature development workflow.

**Required deliverables:**

- Command definition.
- Task parameters.
- Repository context requirements.
- Workflow invocation contract.
- Validation of required inputs.

### **Exercise 28.3 — Integrate Skills with Planning Mode**

Create a plan for implementing booking limits using reusable Skills.

**Required deliverables:**

- Implementation plan.
- Skill selection.
- Affected components.
- Validation strategy.
- Identified architecture risks.

### **Exercise 28.4 — Design a Bug-Fix Workflow**

Address the corporate booking limit concurrency defect.

**Required deliverables:**

- Reproduction procedure.
- Root-cause evidence.
- Regression test.
- Targeted remediation.
- Validation results.

### **Exercise 28.5 — Implement a Workflow State Machine**

Model the lifecycle of a feature development workflow.

**Required deliverables:**

- State definitions.
- Valid transitions.
- Failure and cancellation behavior.
- Checkpoint rules.
- Recovery strategy.

### **Exercise 28.6 — Design a Multi-Agent Workflow**

Coordinate Lead, Developer, Reviewer, Validator, and Evaluator responsibilities.

**Required deliverables:**

- Agent handoff contracts.
- Responsibility boundaries.
- Evidence requirements.
- Conflict resolution.
- Human approval points.

### **Exercise 28.7 — Implement Failure and Retry Policies**

Introduce a transient test infrastructure failure and a deterministic test failure.

**Required deliverables:**

- Failure classification.
- Retry rules.
- Maximum attempts.
- Escalation behavior.
- Final workflow reporting.

### **Exercise 28.8 — Create a Security Remediation Workflow**

Remediate a missing corporate booking authorization check.

**Required deliverables:**

- Risk assessment.
- Planned correction.
- Security test cases.
- Validation gates.
- Independent review.
- Approval evidence.

### **Exercise 28.9 — Build an Auditable Workflow Report**

Create a structured execution report for a completed Alpha Car Detailing change.

**Required deliverables:**

- Workflow identity.
- Stage history.
- Agent activities.
- Validation results.
- Review findings.
- Approval history.
- PR reference.

### **Exercise 28.10 — Create a Reusable Workflow Template Library**

Design five templates covering feature development, bug fixing, refactoring, security remediation, and incident support.

**Required deliverables:**

- Common template schema.
- Task-specific stage definitions.
- Policy and gate mappings.
- Versioning strategy.
- Template validation tests.

## **28.23 Interview Questions**

### **Foundational Questions**

1. What distinguishes an enterprise AI workflow from an individual AI coding interaction?
2. How does an enterprise workflow differ from a harness?
3. What are the essential inputs and outputs of an enterprise workflow?
4. What is the difference between a Claude Command, a Skill, and a workflow?
5. Why should workflow state exist independently of an AI conversation?
6. How does Repository Intelligence support reliable workflow execution?

### **Claude Engineering Questions**

1. How does `CLAUDE.md` influence enterprise workflows?
2. When should Planning Mode be introduced into a workflow?
3. How should Claude Skills be composed within a feature development process?
4. What security controls are necessary for Tool Calling?
5. What distinguishes Claude Memory, Harness Memory, and the Scratchpad?
6. How can Claude Code be integrated into an external harness without making the workflow dependent on Claude-specific behavior?

### **Architecture Questions**

 1. What responsibilities should an enterprise workflow orchestrator own?
 2. When is a multi-agent workflow preferable to a single-agent workflow?
 3. How should agent handoff contracts be designed?
 4. What is the distinction between review, validation, and evaluation?
 5. Why are deterministic gates necessary even when AI agents perform code review?
 6. How would you design workflow checkpoints and recovery after interruption?
 7. How can idempotency protect workflow operations from duplicate side effects?

### **Governance and Security Questions**

 1. At which points should human approval be mandatory?
 2. How do you prevent an agent from bypassing branch protection?
 3. How should prompt injection threats be handled inside workflow execution?
 4. What information should be captured in a workflow audit trail?
 5. How should a workflow handle a rejected approval?
 6. What controls should apply to AI-assisted production incident remediation?

### **Scenario-Based Questions**

 1. Design a workflow for implementing corporate fleet booking across multiple microservices.
 2. How would you investigate a concurrency defect without repeatedly running the entire test suite?
 3. How would you prove that an AI-assisted refactoring preserved existing behavior?
 4. What steps would you require before accepting a security remediation pull request?
 5. How would you design an enterprise workflow template library shared by multiple teams?

## **28.24 Chapter Summary**

The chapter summary should reinforce the following conclusions:

### **28.24.1 Enterprise Workflows Unite Claude Code Capabilities**

- Instructions establish persistent engineering expectations.
- Skills provide reusable engineering capabilities.
- Commands initiate defined activities.
- Planning Mode supports deliberate change design.
- Tool Calling enables controlled execution.
- Memory and Repository Intelligence supply relevant context.
- Scratchpads maintain temporary execution state.

### **28.24.2 Harness Orchestration Creates Repeatability**

- Workflows require explicit stages and transitions.
- Agent responsibilities must remain distinguishable.
- Persistent execution state enables recovery.
- Deterministic gates enforce objective requirements.

### **28.24.3 Enterprise Quality Requires Multiple Forms of Evidence**

- Implementation does not imply correctness.
- Validation establishes deterministic compliance.
- Review assesses implementation quality.
- Evaluation determines whether intended outcomes are satisfied.
- Human approval provides designated accountability.

### **28.24.4 Reliability and Governance Are Architectural Concerns**

- Failures and retries must be controlled.
- Privileges must be constrained.
- External side effects require safeguards.
- Workflow evidence must be retained appropriately.
- Enterprise policies must remain enforceable.

### **28.24.5 Workflows Become Reusable Engineering Assets**

- Standardized templates increase consistency.
- Shared gates reduce duplicated engineering effort.
- Metrics expose bottlenecks and recurring defects.
- Controlled evolution improves workflows without silently changing standards.

### **28.24.6 Transition to Enterprise AI Governance**

- Chapter 28 completes the Claude Engineering section.
- Part V expands the governance, security, approval, audit, policy, and compliance foundations introduced through enterprise workflows.
- Readers should understand that reliable AI engineering requires both capable agents and governed execution processes.

## **28.25 Further Reading**

### **28.25.1 Claude Code Documentation**

- Claude Code overview and architecture.
- Claude Code memory and `CLAUDE.md`.
- Skills and command mechanisms.
- Planning Mode and permission controls.
- Tool use and supported integrations.

### **28.25.2 AI Agent Engineering**

- Anthropic agent engineering guidance.
- Agent tool-use and coordination patterns.
- Agent evaluation approaches.
- Human-in-the-loop design.

### **28.25.3 Software Architecture and Workflow Reliability**

- Martin Fowler — Patterns of Enterprise Application Architecture.
- Enterprise workflow orchestration and state machine patterns.
- Idempotency and reliable distributed execution.
- Transactional outbox and idempotent consumer patterns.
- Recovery and compensating action strategies.

### **28.25.4 Software Delivery and Quality**

- Continuous Integration.
- Continuous Delivery.
- Trunk-based development.
- Automated testing strategies.
- Software supply-chain security.
- OWASP secure development guidance.

### **28.25.5 Governance and Operational Excellence**

- NIST AI Risk Management Framework.
- NIST Secure Software Development Framework.
- OpenTelemetry documentation.
- Audit logging and evidence retention principles.
- Enterprise change management and separation of duties.

### **28.25.6 Internal Handbook Cross-References**

- Chapter 5 — Repository Discovery.
- Chapter 6 — Instructions.
- Chapter 7 — Skills.
- Chapter 8 — Prompts.
- Chapter 9 — Roles.
- Chapter 10 — Steering Notes.
- Chapter 11 — Knowledge Sources.
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapters 14–18 — Agent Responsibilities.
- Chapters 19–21 — Harness Execution and Enterprise Patterns.
- Chapters 22–27 — Claude Engineering Capabilities.
- Part V — Enterprise AI Governance.

**Chapter 28 outline is complete.**
