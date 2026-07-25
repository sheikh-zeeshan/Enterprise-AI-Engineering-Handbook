# Chapter 9 — Roles

**Part II — Repository Intelligence**
**Target file:** `book/09-roles.md`

## Chapter Objective

Explain Roles as governed definitions of responsibility, perspective, authority, boundaries, collaboration, and expected outputs assigned to AI agents.

The chapter establishes the following distinction:

* **Instructions** define persistent engineering standards and constraints.
* **Skills** define reusable procedures for recurring work.
* **Prompts** define the current task, context, expected outcome, and acceptance criteria.
* **Roles** define who performs the work, from which professional perspective, with what authority, and with what responsibilities.

The chapter should prepare readers for the dedicated harness-agent chapters in Part III while keeping the current discussion focused on Roles as a Repository Intelligence construct.

The handbook remains Claude-first, enterprise-focused, and vendor-neutral, using Alpha Car Detailing as the running example and retaining the agreed terminology, governance principles, and frozen chapter structure.

---

# 1. Story-driven Opening

## Section title

**The Corporate Fleet Booking Change That Needed More Than One Agent**

## Purpose

Introduce Roles through a realistic scenario in which a single broad AI agent is asked to plan, implement, review, validate, secure, evaluate, and prepare a pull request for corporate fleet booking.

Show why assigning all responsibilities to one undifferentiated agent creates weak accountability, confirmation bias, excessive authority, and unreliable outputs.

## Key topics

* The Alpha Car Detailing team requests corporate fleet booking.
* The change affects booking, customer, station-capacity, authorization, persistence, integration events, tests, and API contracts.
* Repository instructions already define architecture and quality standards.
* Skills already define reusable implementation procedures.
* The prompt defines the current corporate fleet booking task.
* Roles determine which agent plans, implements, reviews, validates, evaluates, and approves each part.
* A Developer Agent should not automatically approve its own architecture decisions.
* A Validator Agent should report evidence rather than redesign the feature.
* A Reviewer Agent may identify concerns but may not have authority to approve contract-breaking changes.
* Human approval remains necessary for high-impact decisions.

## Alpha Car Detailing example

A Lead Agent decomposes the corporate fleet booking request. An Architect Agent evaluates service boundaries. A Developer Agent implements booking behavior. A Security Reviewer examines authorization. A Reviewer Agent checks architecture compliance. A Validator Agent runs tests and builds. An Evaluator Agent compares the result with the prompt’s acceptance criteria. A human architect approves an API contract change before the pull request is finalized.

## Recommended diagram or table

**Diagram:** “One Task, Multiple Governed Roles”

```text
Corporate Fleet Booking Prompt
              |
          Lead Agent
       /      |       \
Architect  Developer  Security Reviewer
              |
           Reviewer
              |
          Validator
              |
          Evaluator
              |
       Human Approval
              |
        Pull Request
```

---

# 2. Learning Objectives

## Section title

**Learning Objectives**

## Purpose

Define the knowledge and practical capabilities readers should gain from the chapter.

## Key topics

By the end of the chapter, readers should be able to:

* Define an AI-agent Role in enterprise engineering terms.
* Distinguish Roles from Instructions, Skills, Prompts, and Steering Notes.
* Define role purpose, responsibilities, authority, boundaries, inputs, and outputs.
* Design clear handoffs between agents.
* Separate implementation, review, validation, evaluation, and approval responsibilities.
* Identify inappropriate concentrations of agent authority.
* Resolve role conflicts and escalation paths.
* govern role ownership, versioning, testing, and lifecycle.
* Place roles inside an enterprise AI harness.
* Design multi-agent workflows without creating unnecessary complexity.
* Compare role implementation in Claude Code, GitHub Copilot, and OpenAI Codex.
* determine where human approval must remain mandatory.

## Alpha Car Detailing example

Readers should be able to design a role model for adding corporate fleet booking, from planning through pull-request preparation.

## Recommended diagram or table

**Table:** “Learning Objective to Practical Outcome”

| Learning objective     | Practical outcome                                       |
| ---------------------- | ------------------------------------------------------- |
| Define role authority  | Prevent unauthorized contract changes                   |
| Define role boundaries | Prevent reviewers from silently editing production code |
| Define handoffs        | Ensure implementation evidence reaches validation       |
| Separate duties        | Reduce self-review and confirmation bias                |
| Establish escalation   | Route security and architecture conflicts to humans     |

---

# 3. Background

## Section title

**From General-Purpose Agents to Defined Engineering Roles**

## Purpose

Explain why enterprise teams cannot rely on a generic “AI developer” identity for every activity.

Establish Roles as an organizational and technical control that translates professional engineering responsibilities into agent behavior.

## Key topics

### 3.1 The limits of the general-purpose AI agent

* Broad identity without explicit responsibility.
* Unclear authority.
* Unclear completion criteria.
* Mixed implementation and approval responsibilities.
* Inconsistent perspectives.
* Poor auditability.

### 3.2 Roles in human engineering organizations

* Architect.
* Developer.
* Reviewer.
* Tester.
* Security specialist.
* Release manager.
* Technical lead.

### 3.3 Translating human responsibilities into AI-agent roles

* Role purpose.
* Professional perspective.
* Assigned responsibilities.
* Permitted actions.
* Required outputs.
* Escalation obligations.
* Approval limits.

### 3.4 Roles as Repository Intelligence

* Roles must understand repository structure and conventions.
* Roles interpret repository instructions differently based on responsibility.
* The same instruction may produce different obligations for a Developer Agent and Reviewer Agent.
* Roles provide a stable operating perspective across prompts.

### 3.5 Roles and enterprise accountability

* Traceability.
* Separation of duties.
* Repeatability.
* Evidence.
* Human oversight.
* Governance.

## Alpha Car Detailing example

The instruction “all externally visible contract changes require approval” creates different responsibilities:

* The Developer Agent must identify the contract change.
* The Reviewer Agent must verify that it is documented.
* The Evaluator Agent must determine whether the acceptance criteria permit it.
* The human architect must approve it.

## Recommended diagram or table

**Diagram:** “Repository Intelligence Applied Through Roles”

```text
Repository Discovery
        |
Instructions — Skills — Prompts — Steering Notes
        |
      Roles
        |
Role-specific interpretation and execution
```

---

# 4. Concepts

## Section title

**Role Concepts and Definitions**

## Purpose

Define the core conceptual model for enterprise AI-agent roles.

---

## 4.1 What a Role Is

### Purpose

Provide a precise definition of a Role.

### Key topics

A Role is a governed definition of:

* Who performs a category of work.
* The perspective from which the work is examined.
* The responsibilities assigned to the agent.
* The authority granted to the agent.
* The boundaries the agent must not cross.
* The inputs the agent may consume.
* The outputs the agent must produce.
* The handoffs and escalation paths it must follow.

A Role is not merely a persona, job title, or tone instruction.

### Alpha Car Detailing example

The Reviewer Agent is not simply told to “act like a senior reviewer.” Its definition specifies that it must inspect architecture, security, tests, contracts, and repository compliance; report findings by severity; avoid silently rewriting implementation; and escalate approval-sensitive changes.

### Recommended diagram or table

**Table:** “Role Definition Model”

| Element          | Question answered                            |
| ---------------- | -------------------------------------------- |
| Purpose          | Why does this role exist?                    |
| Perspective      | From what viewpoint does it assess the work? |
| Responsibilities | What must it do?                             |
| Authority        | What may it decide or change?                |
| Boundaries       | What must it not do?                         |
| Inputs           | What evidence may it consume?                |
| Outputs          | What must it produce?                        |
| Handoffs         | Who receives the result?                     |
| Escalation       | When must a human or another role intervene? |

---

## 4.2 Why Roles Matter

### Purpose

Explain the engineering and governance value of explicit roles.

### Key topics

* Accountability.
* Consistency.
* Separation of duties.
* Reduced confirmation bias.
* Specialized perspective.
* Safer authority delegation.
* Better handoffs.
* Measurable outputs.
* Easier audit and troubleshooting.
* Reusable multi-agent workflows.
* Controlled autonomy.

### Alpha Car Detailing example

A Security Reviewer examines corporate-account authorization independently from the Developer Agent that implemented the endpoint.

### Recommended diagram or table

**Table:** “Without Roles versus With Roles”

| Without roles               | With roles                       |
| --------------------------- | -------------------------------- |
| One agent performs all work | Responsibilities are distributed |
| Authority is implicit       | Authority is explicit            |
| Self-review is common       | Independent review is designed   |
| Outputs vary                | Outputs follow contracts         |
| Escalation is unclear       | Escalation paths are defined     |
| Audit trail is weak         | Role decisions are traceable     |

---

## 4.3 Roles versus Instructions

### Purpose

Clarify that Instructions define standards while Roles define responsibility for applying, checking, or enforcing them.

### Key topics

* Instructions remain persistent and repository-wide.
* Roles consume and interpret instructions.
* Different roles apply the same instruction differently.
* Roles should not duplicate all instruction content.
* Role definitions may reference authoritative instruction sources.

### Alpha Car Detailing example

The repository instruction requires transactional outbox usage. The Developer Agent applies the pattern; the Reviewer Agent verifies it; the Validator Agent confirms the relevant tests and migrations succeed.

### Recommended table

| Instructions                      | Roles                                        |
| --------------------------------- | -------------------------------------------- |
| Define persistent standards       | Define responsibility and authority          |
| Apply across tasks                | Apply to a category of agent work            |
| State what must be followed       | State who applies or verifies it             |
| Repository-owned                  | Organization or harness-owned                |
| Example: use transactional outbox | Example: Reviewer verifies outbox compliance |

---

## 4.4 Roles versus Skills

### Purpose

Differentiate reusable procedures from the agents responsible for invoking or evaluating them.

### Key topics

* A Skill defines how recurring work is performed.
* A Role determines who may invoke, execute, inspect, or approve the skill.
* One role may use several skills.
* One skill may be used by multiple roles under different conditions.
* Skills should not silently expand role authority.

### Alpha Car Detailing example

The Developer Agent uses the “Add Application Command” and “Add Integration Event” skills. The Reviewer Agent may inspect their expected outputs but does not automatically execute implementation steps.

### Recommended diagram

```text
Developer Agent
   ├── Add Domain Behavior Skill
   ├── Add Application Command Skill
   ├── Add API Endpoint Skill
   └── Add Integration Event Skill
```

---

## 4.5 Roles versus Prompts

### Purpose

Explain that prompts assign current work while roles define the operating responsibility applied to that work.

### Key topics

* Prompt: what must be accomplished now.
* Role: who performs a defined part of the task.
* The same prompt may be interpreted by multiple roles.
* Each role produces different outputs from the same task context.
* Role prompts may specialize the master task prompt.

### Alpha Car Detailing example

The corporate fleet booking prompt is shared across the workflow:

* Lead Agent produces a plan.
* Developer Agent produces code.
* Reviewer Agent produces findings.
* Validator Agent produces test evidence.
* Evaluator Agent produces an acceptance decision.

### Recommended table

| Prompt                                         | Role                                        |
| ---------------------------------------------- | ------------------------------------------- |
| Defines the current task                       | Defines the responsible actor               |
| Contains business goal and acceptance criteria | Contains perspective, authority, and duties |
| Usually task-scoped                            | Usually reusable across tasks               |
| Example: add corporate fleet booking           | Example: validate build and test evidence   |

---

## 4.6 Roles versus Steering Notes

### Purpose

Clarify the relationship between stable role responsibility and temporary mission context.

### Key topics

* Steering Notes define current priorities, constraints, and mission context.
* Roles define how agents respond to those priorities.
* Roles must not silently rewrite Steering Notes.
* Conflicts between role obligations and Steering Notes require escalation.
* Steering Notes may activate or prioritize certain roles.

### Alpha Car Detailing example

A Steering Note states that the current release must not introduce a breaking public API change. The Architect Agent and Reviewer Agent treat this as an active constraint and escalate any unavoidable contract change.

### Recommended table

| Steering Notes                            | Roles                                        |
| ----------------------------------------- | -------------------------------------------- |
| Temporary mission context                 | Reusable responsibility definition           |
| Usually maintained by humans              | Governed by role owners                      |
| State current priorities                  | State who acts on them                       |
| May change each sprint                    | Change less frequently                       |
| Example: no breaking changes this release | Example: Architect evaluates contract impact |

---

## 4.7 Role Scope

### Purpose

Define the breadth and duration of a role’s responsibility.

### Key topics

* Repository-wide roles.
* Service-specific roles.
* Task-specific roles.
* Workflow-stage roles.
* Domain-specific roles.
* Temporary specialist roles.
* Role activation and deactivation.
* Scope by files, services, artifacts, or decisions.
* Avoiding roles that are too broad or too narrow.

### Alpha Car Detailing example

A Security Reviewer may inspect authorization changes across Booking, Customer, and Identity services but may not review unrelated station-maintenance code.

### Recommended diagram or table

**Table:** “Role Scope Dimensions”

| Dimension      | Example                                  |
| -------------- | ---------------------------------------- |
| Repository     | Entire Alpha Car Detailing solution      |
| Service        | Booking Service                          |
| Artifact       | API contracts and authorization policies |
| Workflow stage | Pre-merge validation                     |
| Decision type  | Breaking-contract approval               |
| Duration       | Active for current fleet-booking change  |

---

## 4.8 Role Responsibilities

### Purpose

Explain how to define mandatory duties without turning a role into an unbounded task list.

### Key topics

* Primary responsibilities.
* Secondary responsibilities.
* Mandatory checks.
* Required evidence.
* Completion obligations.
* Documentation responsibilities.
* Reporting responsibilities.
* Distinguishing responsibilities from optional capabilities.

### Alpha Car Detailing example

The Reviewer Agent must inspect domain boundaries, application-layer orchestration, persistence behavior, event publication, test coverage, and instruction compliance.

### Recommended table

| Responsibility                 | Required evidence                        |
| ------------------------------ | ---------------------------------------- |
| Review architecture compliance | Referenced files and findings            |
| Review tests                   | Test names and coverage gaps             |
| Review event handling          | Event contract and outbox evidence       |
| Review authorization           | Policies and access paths                |
| Report decision                | Approved, changes required, or escalated |

---

## 4.9 Role Authority

### Purpose

Define what decisions and actions an agent is permitted to perform.

### Key topics

* Read authority.
* Write authority.
* Execution authority.
* Decision authority.
* Approval authority.
* Merge or release authority.
* Tool authority.
* Environment authority.
* Least-authority principle.
* Time-limited or task-limited authority.
* Human-controlled authority elevation.

### Alpha Car Detailing example

The Developer Agent may modify feature code and tests in an isolated branch but may not approve a breaking API contract, merge to the protected branch, or change production authorization policies without approval.

### Recommended diagram

**Authority ladder:**

```text
Observe
  ↓
Recommend
  ↓
Modify
  ↓
Execute
  ↓
Approve
  ↓
Merge / Release
```

---

## 4.10 Role Boundaries

### Purpose

Define explicit prohibitions and stop conditions.

### Key topics

* Forbidden file areas.
* Prohibited decisions.
* No self-approval.
* No silent requirement changes.
* No weakening of tests or security.
* No modification of governance artifacts without permission.
* Stop conditions.
* Required escalation.
* Distinction between capability and permission.

### Alpha Car Detailing example

The Validator Agent may run the build and tests but must not change production code merely to make validation pass.

### Recommended table

| Role              | Boundary                                              |
| ----------------- | ----------------------------------------------------- |
| Developer         | Cannot approve its own implementation                 |
| Reviewer          | Cannot silently rewrite acceptance criteria           |
| Validator         | Cannot bypass failing tests                           |
| Evaluator         | Cannot invent business requirements                   |
| Security Reviewer | Cannot weaken authorization policy                    |
| Lead              | Cannot grant itself unrestricted repository authority |

---

## 4.11 Role Inputs and Outputs

### Purpose

Define clear information contracts for role execution.

### Key topics

#### Inputs

* Task prompt.
* Repository instructions.
* Relevant skills.
* Steering Notes.
* Repository discovery report.
* Code changes.
* Test results.
* Architecture decisions.
* Security policies.
* Prior role outputs.

#### Outputs

* Plan.
* Implementation.
* Review findings.
* Validation report.
* Evaluation score.
* Approval request.
* Escalation notice.
* Pull-request summary.
* Evidence bundle.

#### Output quality requirements

* Structured.
* Traceable.
* Evidence-based.
* Role-specific.
* Machine-readable where useful.
* Human-readable where approval is needed.

### Alpha Car Detailing example

The Validator Agent receives the implementation commit and validation commands, then returns build status, test counts, failed tests, migration results, and unresolved blockers.

### Recommended diagram

```text
Role Inputs → Role Responsibilities → Role Outputs
```

---

## 4.12 Role Handoffs

### Purpose

Explain how work and evidence move safely between roles.

### Key topics

* Explicit entry and exit conditions.
* Artifact-based handoffs.
* Handoff completeness.
* Ownership transfer.
* Required evidence.
* Rejected handoffs.
* Retry and remediation loops.
* Handoff timestamps and traceability.
* Avoiding reliance on conversational memory alone.

### Alpha Car Detailing example

The Developer Agent hands the Reviewer Agent:

* Changed-file list.
* Design summary.
* Test additions.
* Known assumptions.
* Contract changes.
* Migration details.
* Unresolved concerns.

### Recommended diagram

```text
Lead Plan
   ↓
Developer Change Set
   ↓
Reviewer Findings
   ↓
Developer Remediation
   ↓
Validator Evidence
   ↓
Evaluator Decision
```

---

## 4.13 Role Collaboration

### Purpose

Describe controlled collaboration without blurring accountability.

### Key topics

* Shared context versus role-specific context.
* Parallel and sequential collaboration.
* Requesting specialist input.
* Commenting versus editing.
* Shared evidence store.
* Consensus and disagreement.
* Collaboration protocols.
* Human visibility.

### Alpha Car Detailing example

The Architect Agent and Security Reviewer independently assess a proposed corporate-account authorization model, then provide separate findings to the Lead Agent.

### Recommended table

| Collaboration mode | Appropriate use                                |
| ------------------ | ---------------------------------------------- |
| Sequential         | Implementation followed by review              |
| Parallel           | Architecture and security assessment           |
| Consultative       | Developer requests architect guidance          |
| Adversarial        | Reviewer challenges implementation assumptions |
| Supervisory        | Lead coordinates specialist outputs            |

---

## 4.14 Role Separation

### Purpose

Establish separation of duties as a core enterprise safety mechanism.

### Key topics

* Builder versus reviewer.
* Reviewer versus approver.
* Validator versus implementer.
* Evaluator versus task author.
* Human approver versus autonomous execution.
* Risk-based separation.
* Independent evidence.
* Small-team compromises.
* Avoiding artificial separation that adds no value.

### Alpha Car Detailing example

The agent that implements authorization for corporate fleet managers should not be the only agent that declares the authorization secure.

### Recommended diagram

**Separation-of-duties matrix**

| Activity                | Developer |  Reviewer | Validator |             Human approver |
| ----------------------- | --------: | --------: | --------: | -------------------------: |
| Implement feature       |   Primary |        No |        No |                         No |
| Review architecture     |   Support |   Primary |        No | Final for high-risk change |
| Run validation          |   Support |   Observe |   Primary |            Review evidence |
| Approve contract change |        No | Recommend |        No |                    Primary |
| Merge protected branch  |        No |        No |        No |   Authorized human/process |

---

## 4.15 Human Approval

### Purpose

Define where agent authority ends and accountable human decision-making begins.

### Key topics

* Architecture boundary changes.
* Breaking API or event contracts.
* Security-policy changes.
* Data-retention changes.
* Production deployment.
* Schema-destructive migration.
* Compliance-sensitive decisions.
* Financial or operational risk.
* Approval evidence.
* Approval expiry.
* Reapproval after material changes.

### Alpha Car Detailing example

Adding a required field to the public fleet-booking request contract requires explicit human approval because existing corporate clients may be affected.

### Recommended table

**Human Approval Matrix**

| Change type                | Agent action            | Human action                |
| -------------------------- | ----------------------- | --------------------------- |
| Internal refactoring       | Recommend and implement | Optional review             |
| New non-breaking endpoint  | Implement and validate  | Standard PR approval        |
| Breaking contract          | Analyze and propose     | Explicit architect approval |
| Authorization model change | Review and test         | Security owner approval     |
| Destructive migration      | Produce plan            | Database owner approval     |
| Production release         | Prepare evidence        | Authorized release approval |

---

## 4.16 Role Ownership

### Purpose

Define accountable ownership for role definitions.

### Key topics

* Business owner.
* Technical owner.
* Governance owner.
* Security owner.
* Harness maintainer.
* Review cadence.
* Ownership metadata.
* Ownership transfer.
* Orphaned roles.
* Shared ownership risks.

### Alpha Car Detailing example

The Platform Engineering team owns the Validator Agent definition, while the Architecture group owns the Architect Agent definition.

### Recommended table

| Role              | Likely owner                                 |
| ----------------- | -------------------------------------------- |
| Lead Agent        | AI engineering or platform team              |
| Developer Agent   | Engineering enablement team                  |
| Reviewer Agent    | Architecture and engineering standards group |
| Validator Agent   | Platform or DevOps team                      |
| Security Reviewer | Application security team                    |
| Evaluator Agent   | Product and engineering governance           |

---

## 4.17 Role Versioning

### Purpose

Explain how role definitions evolve safely.

### Key topics

* Semantic or date-based versioning.
* Role schema version.
* Compatibility with harness workflows.
* Changelog.
* Migration of prompts and outputs.
* Deprecation.
* Rollback.
* Role-version traceability in run records.
* Preventing silent behavioral drift.

### Alpha Car Detailing example

Reviewer Agent version 1.2 introduces mandatory checks for event-schema compatibility. Existing harness runs retain their original role-version reference.

### Recommended table

| Version change | Example                             |
| -------------- | ----------------------------------- |
| Patch          | Clarify output wording              |
| Minor          | Add a new review category           |
| Major          | Change authority or output contract |

---

## 4.18 Role Governance

### Purpose

Define the controls required to manage roles as enterprise assets.

### Key topics

* Role registry.
* Approval workflow.
* Ownership.
* Authority review.
* Security review.
* Change history.
* Auditability.
* Usage metrics.
* Retirement.
* Policy alignment.
* Cross-repository consistency.
* Human approval for authority expansion.

### Alpha Car Detailing example

A proposal to let the Developer Agent automatically create database migrations is reviewed for repository fit, risk, rollback behavior, and environment permissions.

### Recommended diagram

```text
Propose → Review → Test → Approve → Publish → Monitor → Revise/Retire
```

---

## 4.19 Role Testing

### Purpose

Explain how teams verify that roles behave as intended.

### Key topics

* Responsibility tests.
* Boundary tests.
* Authority tests.
* Output-contract tests.
* Escalation tests.
* Conflict tests.
* Adversarial scenarios.
* Regression suites.
* Golden examples.
* Human evaluation.
* Measuring false approvals and false escalations.

### Alpha Car Detailing example

Test the Reviewer Agent with a change that bypasses the transactional outbox and confirm that it identifies the violation rather than approving the implementation.

### Recommended table

| Test type           | Example                                         |
| ------------------- | ----------------------------------------------- |
| Responsibility test | Reviewer checks architecture rules              |
| Boundary test       | Validator refuses to edit code                  |
| Authority test      | Developer requests approval for contract change |
| Escalation test     | Security conflict reaches human owner           |
| Output test         | Findings follow required schema                 |
| Regression test     | Previous known violation remains detectable     |

---

## 4.20 Role Conflicts

### Purpose

Explain how conflicting responsibilities, conclusions, or authority claims are handled.

### Key topics

* Conflicting findings.
* Overlapping authority.
* Contradictory instructions.
* Different interpretations of acceptance criteria.
* Architecture versus delivery pressure.
* Security versus usability.
* Role precedence.
* Evidence-based resolution.
* Human arbitration.
* Recording unresolved disagreement.

### Alpha Car Detailing example

The Architect Agent recommends a new Fleet Service, while the Lead Agent proposes extending Booking Service to meet the release deadline. The disagreement is documented and escalated rather than silently resolved by whichever agent runs last.

### Recommended diagram

```text
Conflict Detected
      ↓
Compare Scope and Authority
      ↓
Collect Evidence
      ↓
Apply Precedence Rules
      ↓
Human Escalation if Unresolved
```

---

## 4.21 Role Escalation

### Purpose

Define when and how a role transfers a decision to a higher-authority role or human.

### Key topics

* Escalation triggers.
* Severity levels.
* Decision uncertainty.
* Missing information.
* Policy conflict.
* Security concern.
* Contract impact.
* Repeated validation failure.
* Escalation package.
* Response time and ownership.
* Blocking versus non-blocking escalation.

### Alpha Car Detailing example

The Security Reviewer identifies that station employees may gain access to corporate fleet pricing outside their assigned station. The finding is escalated as a blocking authorization issue.

### Recommended table

| Trigger                | Escalation target           |
| ---------------------- | --------------------------- |
| Breaking contract      | Architect or API owner      |
| Security vulnerability | Security owner              |
| Unclear business rule  | Product owner               |
| Repeated test failure  | Lead and developer          |
| Data-loss risk         | Data owner                  |
| Instruction conflict   | Repository governance owner |

---

# 5. Architecture Discussion

## Section title

**Role Architecture for Enterprise AI Engineering**

## Purpose

Show how roles should be designed and positioned within repository intelligence and harness architecture.

---

## 5.1 Role definition architecture

### Key topics

A role definition should contain:

* Identifier.
* Name.
* Purpose.
* Perspective.
* Scope.
* Responsibilities.
* Authority.
* Boundaries.
* Inputs.
* Outputs.
* Required instructions.
* Permitted skills.
* Handoff rules.
* Escalation rules.
* Approval requirements.
* Owner.
* Version.
* Test references.
* Audit metadata.

### Alpha Car Detailing example

Define `reviewer-agent` with authority to inspect all changed files, execute static-analysis commands, and report findings, but not merge, approve security exceptions, or alter acceptance criteria.

### Recommended table

**Canonical Role Definition Schema**

---

## 5.2 Repository placement

### Key topics

Possible structures:

```text
roles/
├── lead-agent.md
├── developer-agent.md
├── reviewer-agent.md
├── validator-agent.md
├── evaluator-agent.md
├── architect-agent.md
└── security-reviewer.md
```

Or:

```text
harness/
└── roles/
    ├── lead/
    ├── developer/
    ├── reviewer/
    ├── validator/
    └── evaluator/
```

Considerations:

* Central versus repository-specific roles.
* Inheritance and overlays.
* Discoverability.
* Version control.
* Tool portability.
* Avoiding hidden role definitions embedded only in scripts.

### Alpha Car Detailing example

Shared enterprise role templates are extended by repository-specific boundaries for Alpha Car Detailing.

### Recommended diagram

**Layered Role Model**

```text
Enterprise Base Role
        ↓
Technology Profile
        ↓
Repository-Specific Role
        ↓
Task-Specific Activation
```

---

## 5.3 Role execution model

### Key topics

* Role selection.
* Context loading.
* Authority assignment.
* Tool access.
* Execution.
* Output validation.
* Handoff.
* Audit recording.
* Human intervention.

### Alpha Car Detailing example

Before the Developer Agent begins, the harness loads the task prompt, repository instructions, approved skills, relevant Steering Note, assigned service scope, and write permissions.

### Recommended diagram

```text
Select Role
   ↓
Load Context
   ↓
Apply Authority
   ↓
Execute Responsibilities
   ↓
Validate Output Contract
   ↓
Handoff or Escalate
```

---

## 5.4 Roles inside an AI harness

### Key topics

* Role registry.
* Role runner.
* Role-specific prompts.
* Tool restrictions.
* State and evidence store.
* Handoff coordinator.
* Validation gates.
* Approval gates.
* Metrics.
* Audit trail.
* Retry loops.
* Failure recovery.

### Alpha Car Detailing example

The harness records which role produced each planning decision, code change, review finding, validation result, and acceptance decision.

### Recommended diagram

```text
Task Prompt
    ↓
Role Orchestrator
    ├── Lead Agent
    ├── Developer Agent
    ├── Reviewer Agent
    ├── Validator Agent
    ├── Evaluator Agent
    └── Human Approval Gate
            ↓
       Evidence Store
            ↓
       Pull Request
```

---

## 5.5 Multi-agent workflow architecture

### Key topics

* Sequential workflow.
* Parallel workflow.
* Supervisor-worker model.
* Peer-review model.
* Debate or adversarial model.
* Event-driven handoffs.
* Shared versus isolated context.
* State synchronization.
* Duplicate work.
* Cost and latency.
* Termination conditions.
* Avoiding agent proliferation.

### Alpha Car Detailing example

Architecture and security reviews run in parallel after implementation. Validation begins only after blocking review findings are resolved.

### Recommended table

| Workflow model     | Strength                 | Risk                | Suitable use               |
| ------------------ | ------------------------ | ------------------- | -------------------------- |
| Sequential         | Clear accountability     | Longer duration     | Plan–build–review–validate |
| Parallel           | Faster specialist review | Conflicting results | Architecture and security  |
| Supervisor-worker  | Central coordination     | Lead bottleneck     | Complex feature delivery   |
| Adversarial review | Strong challenge         | Higher cost         | High-risk changes          |
| Event-driven       | Scalable automation      | State complexity    | Large harness platforms    |

---

## 5.6 Authority and trust boundaries

### Key topics

* Repository read/write boundaries.
* Branch protections.
* Command execution.
* Secret access.
* Network access.
* Deployment access.
* Approval tokens.
* Sandboxed execution.
* Least privilege.
* Temporary credentials.
* Audit logging.

### Alpha Car Detailing example

The Validator Agent may access build infrastructure and test containers but not production secrets or deployment credentials.

### Recommended diagram

**Trust Boundary Diagram**

```text
Repository
  ├── Read Zone
  ├── Feature-Branch Write Zone
  ├── Validation Environment
  ├── Protected Branch
  └── Production Environment
```

---

# 6. Professional Diagrams

## Section title

**Role Models and Workflow Diagrams**

## Purpose

Define the professional visual assets that should appear in the finished chapter.

## Recommended diagrams

### 6.1 Role relationship model

Show the relationship among:

* Instructions.
* Skills.
* Prompts.
* Roles.
* Steering Notes.
* Harness.

```text
Instructions → Standards
Skills       → Procedures
Prompts      → Current Task
Roles        → Responsibility and Authority
Steering     → Current Mission Context
Harness      → Execution and Governance
```

### 6.2 Role anatomy diagram

Show:

* Purpose.
* Perspective.
* Responsibilities.
* Authority.
* Boundaries.
* Inputs.
* Outputs.
* Handoffs.
* Escalation.

### 6.3 Multi-agent corporate fleet booking workflow

Show the complete journey from task intake through pull request.

### 6.4 Role-authority matrix

Map roles against:

* Read code.
* Modify code.
* Run commands.
* Approve design.
* Approve security exception.
* Merge.
* Deploy.

### 6.5 Role handoff sequence diagram

Participants:

* Human Product Owner.
* Lead Agent.
* Architect Agent.
* Developer Agent.
* Reviewer Agent.
* Security Reviewer.
* Validator Agent.
* Evaluator Agent.
* Human Approver.

### 6.6 Conflict and escalation flowchart

Show conflict detection, authority comparison, evidence collection, and human arbitration.

### 6.7 Role lifecycle diagram

```text
Proposed → Drafted → Tested → Approved → Active → Revised → Deprecated → Retired
```

## Alpha Car Detailing example

All diagrams should use the corporate fleet booking workflow rather than generic or toy examples.

---

# 7. Hands-on Example

## Section title

**Designing Roles for Corporate Fleet Booking**

## Purpose

Guide the reader through creating a complete role model for a realistic Alpha Car Detailing feature.

---

## 7.1 Scenario definition

### Key topics

The feature should allow:

* Authorized corporate fleet managers to create bookings for multiple vehicles.
* Station-capacity validation.
* Corporate pricing-plan application.
* Booking confirmation.
* Integration-event publication.
* Audit logging.
* Authorization enforcement.
* Backward-compatible API behavior where possible.

### Alpha Car Detailing example

The request affects Booking Service, Customer Service, station-capacity logic, corporate authorization, persistence, integration events, and test infrastructure.

### Recommended table

**Affected Components and Risk Areas**

---

## 7.2 Define the Lead Agent

### Purpose

Coordinate the workflow without absorbing every specialist responsibility.

### Key topics

#### Responsibilities

* Read task context.
* Identify affected services.
* Decompose work.
* Select roles.
* Define sequence and gates.
* Track blockers.
* Coordinate handoffs.
* Prepare consolidated status.

#### Authority

* Assign work within approved workflow.
* Request specialist analysis.
* Pause work when a gate fails.

#### Boundaries

* Cannot approve its own exceptions.
* Cannot bypass mandatory review.
* Cannot silently change acceptance criteria.

#### Outputs

* Execution plan.
* Role assignments.
* Dependency map.
* Risk register.
* Handoff schedule.
* Escalation list.

### Alpha Car Detailing example

The Lead Agent divides work into architecture assessment, implementation, security review, architecture review, validation, evaluation, and PR preparation.

### Recommended table

**Lead Agent Role Card**

---

## 7.3 Define the Architect Agent

### Purpose

Evaluate service boundaries, contracts, domain ownership, and architectural consequences.

### Key topics

#### Responsibilities

* Assess service ownership.
* Review domain boundaries.
* Evaluate synchronous and asynchronous interactions.
* Review API and event contracts.
* Identify architecture decision requirements.
* Assess backward compatibility.

#### Authority

* Recommend architecture.
* Block implementation pending clarification where risk is high.
* Request an Architecture Decision Record.

#### Boundaries

* Does not approve business requirements.
* Does not independently authorize breaking changes.
* Does not replace implementation ownership.

#### Outputs

* Architecture assessment.
* Service-impact map.
* Contract-impact analysis.
* Risks and alternatives.
* Approval requests.

### Alpha Car Detailing example

Determine whether corporate fleet booking belongs within Booking Service or requires a separate Fleet Management capability.

### Recommended diagram

**Service Impact Diagram**

---

## 7.4 Define the Developer Agent

### Purpose

Implement the approved design according to repository instructions and applicable skills.

### Key topics

#### Responsibilities

* Implement domain behavior.
* Add application commands and handlers.
* Add API endpoints.
* Add persistence changes.
* Add integration events.
* Add tests.
* Document assumptions.
* Prepare implementation evidence.

#### Authority

* Modify approved feature areas.
* Execute development and test commands.
* Use approved skills.
* Create feature-branch commits where permitted.

#### Boundaries

* Cannot approve its own design.
* Cannot weaken tests.
* Cannot bypass authorization.
* Cannot introduce breaking contracts without approval.
* Cannot alter instructions or Steering Notes.

#### Outputs

* Code.
* Tests.
* Migration.
* Event schema.
* Implementation notes.
* Known limitations.

### Alpha Car Detailing example

Implement `CreateCorporateFleetBookingCommand`, capacity validation, authorization checks, booking persistence, and `CorporateFleetBookingCreated` event publication.

### Recommended table

**Developer Inputs and Deliverables**

---

## 7.5 Define the Reviewer Agent

### Purpose

Perform an independent review of implementation quality and repository compliance.

### Key topics

#### Responsibilities

* Inspect changed files.
* Check architectural layering.
* Review naming and conventions.
* Review domain invariants.
* Review event and persistence patterns.
* Review tests.
* Identify missing validation.
* Classify findings by severity.

#### Authority

* Approve review from a code-quality perspective.
* Request changes.
* Escalate architecture or security concerns.

#### Boundaries

* Cannot merge.
* Cannot approve security exceptions.
* Cannot silently modify task requirements.
* Should not conceal findings by fixing them without reporting.

#### Outputs

* Review report.
* Findings with evidence.
* Severity.
* Required remediation.
* Approval or changes-required recommendation.

### Alpha Car Detailing example

Identify that capacity validation occurs only in the API controller rather than in domain or application logic.

### Recommended table

**Review Finding Format**

| Field           | Example                                        |
| --------------- | ---------------------------------------------- |
| Severity        | High                                           |
| Area            | Architecture                                   |
| Evidence        | `FleetBookingsController.cs`                   |
| Finding         | Capacity rule exists only in controller        |
| Risk            | Other entry points may bypass validation       |
| Required action | Move enforcement into application/domain layer |

---

## 7.6 Define the Security Reviewer

### Purpose

Evaluate authentication, authorization, data exposure, abuse cases, and security-policy compliance.

### Key topics

#### Responsibilities

* Verify corporate-manager authorization.
* Validate tenant or corporate-account boundaries.
* Review station-level access.
* Review sensitive data exposure.
* Examine audit logging.
* Assess input abuse and privilege escalation.
* Review secrets and configuration changes.

#### Authority

* Raise blocking security findings.
* Require security-owner approval.
* Recommend mitigations.

#### Boundaries

* Cannot waive security policy.
* Cannot approve exceptions on behalf of security leadership.
* Cannot replace business authorization decisions.

#### Outputs

* Threat-focused findings.
* Authorization matrix.
* Data-exposure review.
* Required mitigations.
* Escalation request.

### Alpha Car Detailing example

Verify that a fleet manager belonging to Corporate Account A cannot view or create bookings for Corporate Account B.

### Recommended table

**Corporate Fleet Authorization Matrix**

---

## 7.7 Define the Validator Agent

### Purpose

Produce objective technical evidence that the implementation builds and behaves as required.

### Key topics

#### Responsibilities

* Restore dependencies.
* Build affected projects.
* Run unit tests.
* Run integration tests.
* Validate migrations.
* Validate formatting and static analysis.
* Validate event-schema compatibility.
* Record results.

#### Authority

* Execute approved validation commands.
* Fail the validation gate.
* Request remediation.

#### Boundaries

* Cannot edit production code.
* Cannot suppress failing tests.
* Cannot redefine acceptance criteria.
* Cannot report success without evidence.

#### Outputs

* Build report.
* Test results.
* Failure evidence.
* Validation summary.
* Reproduction commands.

### Alpha Car Detailing example

Run Booking Service tests, authorization integration tests, database migration validation, and event-contract compatibility checks.

### Recommended table

**Validation Evidence Report**

---

## 7.8 Define the Evaluator Agent

### Purpose

Determine whether the completed work satisfies the original prompt and acceptance criteria.

### Key topics

#### Responsibilities

* Compare outputs against acceptance criteria.
* Verify required behavior.
* Check whether all requested artifacts exist.
* Examine unresolved assumptions.
* Confirm validation evidence.
* Identify partial completion.
* Produce an acceptance recommendation.

#### Authority

* Mark criteria as met, unmet, or unclear.
* Request missing evidence.
* Escalate ambiguous requirements.

#### Boundaries

* Cannot invent acceptance criteria.
* Cannot treat build success as business acceptance.
* Cannot approve high-risk exceptions.
* Cannot ignore unmet criteria.

#### Outputs

* Acceptance matrix.
* Evidence links.
* Completion score or status.
* Unmet criteria.
* Final recommendation.

### Alpha Car Detailing example

Confirm that authorized fleet managers can create multi-vehicle bookings, capacity is enforced, events are published transactionally, unauthorized users are rejected, and all required tests pass.

### Recommended table

| Acceptance criterion            | Evidence                | Status            |
| ------------------------------- | ----------------------- | ----------------- |
| Multi-vehicle booking           | Integration test        | Met               |
| Capacity enforcement            | Domain test             | Met               |
| Corporate authorization         | Security test           | Met               |
| Transactional event publication | Outbox integration test | Met               |
| Backward compatibility          | Contract comparison     | Requires approval |

---

## 7.9 Define the PR preparation responsibility

### Purpose

Clarify whether PR preparation is a separate role or a Lead Agent responsibility.

### Key topics

* Change summary.
* Business context.
* Architecture decisions.
* Test evidence.
* Security findings.
* Contract changes.
* Known risks.
* Approval references.
* Reviewer guidance.
* Linked issues.

### Alpha Car Detailing example

Prepare a pull-request description summarizing corporate fleet booking behavior, affected services, migrations, events, validation results, and required human approvals.

### Recommended table

**Pull Request Evidence Checklist**

---

## 7.10 Create role files

### Purpose

Show the proposed repository artifacts without writing the full chapter implementation.

### Key topics

Suggested files:

```text
harness/
└── roles/
    ├── lead-agent.md
    ├── architect-agent.md
    ├── developer-agent.md
    ├── reviewer-agent.md
    ├── security-reviewer.md
    ├── validator-agent.md
    └── evaluator-agent.md
```

Each role file should include:

* Role identifier.
* Purpose.
* Perspective.
* Scope.
* Responsibilities.
* Authority.
* Boundaries.
* Inputs.
* Outputs.
* Handoffs.
* Escalations.
* Human approvals.
* Owner.
* Version.
* Tests.

### Alpha Car Detailing example

Provide concise role-definition excerpts for the corporate fleet booking workflow.

### Recommended table

**Role File Template**

---

# 8. Claude Example

## Section title

**Implementing Roles with Claude Code**

## Purpose

Show how Claude Code can be used as the primary implementation platform while keeping the role model conceptually vendor-neutral.

## Key topics

### 8.1 Role context in Claude Code

* Repository instructions through `CLAUDE.md`.
* Role-specific prompt files.
* Skills available to the assigned role.
* Steering Note loading.
* Explicit role activation.
* Plan and execution separation.
* Tool permissions.
* Read-only versus write-capable sessions.
* Subagent or multi-agent orchestration where available.
* Capturing outputs in repository artifacts.

### 8.2 Example Claude role structure

```text
.claude/
├── roles/
│   ├── lead.md
│   ├── developer.md
│   ├── reviewer.md
│   ├── validator.md
│   └── evaluator.md
├── skills/
├── commands/
└── settings/
```

Clarify that exact platform capabilities and file conventions may evolve, while the enterprise role model remains stable.

### 8.3 Claude Developer Agent example

* Load repository instructions.
* Read the fleet-booking prompt.
* Use approved implementation skills.
* Modify only assigned services.
* Produce implementation notes.

### 8.4 Claude Reviewer Agent example

* Operate read-first.
* Inspect changed files and diffs.
* Apply repository instructions.
* Produce evidence-based findings.
* Avoid implementation unless explicitly reassigned.

### 8.5 Claude Validator Agent example

* Execute approved commands.
* Record exact results.
* Avoid modifying source.
* Return a structured validation report.

### 8.6 Claude role handoffs

* Files rather than ephemeral conversation alone.
* Structured Markdown or JSON reports.
* Commit or run identifiers.
* Human-readable approval packages.

## Alpha Car Detailing example

A Claude-based harness activates the Architect, Developer, Reviewer, Validator, and Evaluator roles for the corporate fleet booking task.

## Recommended diagram or table

**Diagram:** “Claude-first Role Execution Flow”

**Table:** “Claude Role Context Sources”

| Context source | Role use                        |
| -------------- | ------------------------------- |
| `CLAUDE.md`    | Persistent repository standards |
| Skill files    | Approved reusable procedures    |
| Task prompt    | Current fleet-booking objective |
| Steering Note  | Current release constraints     |
| Role file      | Responsibility and authority    |
| Prior output   | Handoff evidence                |

---

# 9. GitHub Copilot Comparison

## Section title

**Roles with GitHub Copilot**

## Purpose

Compare how explicit role behavior may be represented and enforced in GitHub Copilot-centered workflows.

## Key topics

* Repository instructions such as Copilot instruction files.
* Prompt files in `.github/prompts`.
* Agent or mode-specific workflows where supported.
* IDE-centered versus harness-centered execution.
* Pull-request review integration.
* Human-in-the-loop development.
* Differences in context loading and orchestration.
* Need for external harness logic when native role orchestration is limited.
* Role separation through prompt files, workflows, permissions, and review stages.
* Avoid overstating platform capabilities.

## Alpha Car Detailing example

Use separate Copilot prompt files for:

* Fleet-booking implementation.
* Architecture review.
* Security review.
* Validation summary.
* Pull-request preparation.

The surrounding GitHub workflow enforces branch protection and human approvals.

## Recommended table

| Concern                   | Claude-first approach          | GitHub Copilot approach              |
| ------------------------- | ------------------------------ | ------------------------------------ |
| Persistent instructions   | Claude repository instructions | Copilot repository instructions      |
| Role definition           | Role files and harness context | Prompt files, agent modes, workflows |
| Multi-agent orchestration | Harness or platform features   | Often external workflow coordination |
| PR integration            | Harness-prepared PR            | Strong GitHub-native workflow        |
| Authority control         | Tool and harness permissions   | GitHub permissions and branch rules  |

---

# 10. Codex Comparison

## Section title

**Roles with OpenAI Codex**

## Purpose

Explain how the same role model can be applied in Codex-based coding workflows.

## Key topics

* Repository instruction files such as `AGENTS.md`.
* Task-specific execution context.
* Isolated task runs.
* Role-specific prompts.
* Sandboxed execution.
* Code modification and validation workflows.
* Parallel task execution where supported.
* External orchestration for multi-role workflows.
* Capturing diffs, commands, and evidence.
* Human approval before merge or deployment.
* Distinguishing platform implementation from the role-governance model.

## Alpha Car Detailing example

Separate Codex runs perform:

* Architecture analysis.
* Fleet-booking implementation.
* Independent code review.
* Validation.
* Acceptance evaluation.

The harness collects outputs and routes approval-sensitive findings to a human architect.

## Recommended table

| Role requirement     | Codex implementation approach             |
| -------------------- | ----------------------------------------- |
| Persistent standards | `AGENTS.md` or repository guidance        |
| Task assignment      | Role-specific task prompt                 |
| Execution boundary   | Isolated environment or branch            |
| Validation           | Explicit command set and evidence capture |
| Handoff              | Diff, report, artifact, or run output     |
| Approval             | External governance or human gate         |

---

# 11. Best Practices

## Section title

**Best Practices for Enterprise AI-Agent Roles**

## Purpose

Provide practical guidance for designing roles that are clear, safe, testable, and reusable.

## Key topics

### 11.1 Define responsibility before persona

A professional title alone is insufficient.

### 11.2 Separate responsibilities from authority

A role may be responsible for identifying a contract issue without having authority to approve it.

### 11.3 Use least authority

Grant only the tools, files, and decisions necessary for the role.

### 11.4 Make boundaries explicit

State prohibited actions and mandatory stop conditions.

### 11.5 Require evidence-based outputs

Findings should reference files, tests, commands, contracts, or requirements.

### 11.6 Design handoffs as contracts

Every role should have defined inputs, outputs, and acceptance conditions.

### 11.7 Separate implementation from independent review

Avoid self-approval for meaningful changes.

### 11.8 Keep role definitions reusable

Do not embed a single task’s details permanently in a reusable role.

### 11.9 Keep prompts task-specific

The role defines responsibility; the prompt defines today’s work.

### 11.10 Version role definitions

Record which version participated in each harness run.

### 11.11 Test boundaries and escalation

A role is incomplete until refusal and escalation behaviors are tested.

### 11.12 Preserve human authority for high-risk decisions

Architecture, security, compliance, data loss, and production-release decisions require accountable approval.

### 11.13 Avoid unnecessary agent count

Introduce a separate role only when it adds a distinct perspective, responsibility, or control.

### 11.14 Use shared evidence, not shared assumptions

Collaboration should rely on artifacts and traceable outputs.

### 11.15 Measure role effectiveness

Track:

* Review defects found.
* Validation accuracy.
* Escalation quality.
* Rework rate.
* False approvals.
* Unnecessary escalations.
* Handoff failures.

## Alpha Car Detailing example

The corporate fleet booking workflow uses seven roles only because each role contributes a distinct responsibility or control.

## Recommended table

**Role Design Checklist**

---

# 12. Anti-patterns

## Section title

**Role Anti-patterns**

## Purpose

Identify common failures that make role-based AI engineering unsafe or ineffective.

## Key topics

### 12.1 The omnipotent agent

One role plans, codes, reviews, approves, merges, and deploys.

### 12.2 Persona-only roles

“Act as a senior architect” without responsibilities, boundaries, or outputs.

### 12.3 Role-instruction duplication

Copying all repository instructions into every role file.

### 12.4 Hidden authority

Allowing tools or scripts to grant more power than the role definition states.

### 12.5 Self-review

The Developer Agent declares its own work compliant without independent review.

### 12.6 Validator as fixer

The Validator Agent edits code until tests pass.

### 12.7 Reviewer as silent developer

The Reviewer Agent changes code without reporting the original defect.

### 12.8 Evaluator as requirement author

The Evaluator Agent invents or relaxes acceptance criteria.

### 12.9 Lead Agent as unchecked supervisor

The Lead Agent bypasses specialist or human gates.

### 12.10 Too many roles

Creating a separate agent for every minor activity.

### 12.11 Overlapping roles without precedence

Multiple roles claim the same decision authority.

### 12.12 Handoff by conversation memory

Critical decisions are not written into durable artifacts.

### 12.13 No escalation path

Agents encounter uncertainty but continue making assumptions.

### 12.14 Permanent authority elevation

Temporary write or execution access is never revoked.

### 12.15 Role drift

A role gradually accumulates responsibilities without review or versioning.

### 12.16 Rubber-stamp reviewer

The Reviewer Agent consistently confirms work without meaningful challenge.

### 12.17 Human approval theater

A human approves a result without receiving usable evidence.

## Alpha Car Detailing example

The same agent implements corporate authorization, reviews it, marks the security criterion as met, and creates the merge approval.

## Recommended table

| Anti-pattern       | Risk                           | Corrective action                        |
| ------------------ | ------------------------------ | ---------------------------------------- |
| Omnipotent agent   | Excessive authority            | Separate duties                          |
| Persona-only role  | Unpredictable output           | Define role contract                     |
| Validator as fixer | Invalid evidence               | Enforce read/execute-only boundary       |
| No escalation      | Unsafe assumptions             | Add explicit triggers                    |
| Too many roles     | Cost and coordination overhead | Consolidate overlapping responsibilities |

---

# 13. Architect’s Notes

## Section title

**Architect’s Notes**

## Purpose

Provide concise strategic guidance for architects designing role-based AI systems.

## Key topics

### Architect’s Note: Roles are control boundaries

A Role should be treated as an authority and accountability boundary, not merely a prompt-writing technique.

### Architect’s Note: Independence is contextual

Not every task needs separate agents, but high-risk decisions require genuinely independent review or human oversight.

### Architect’s Note: Tool access defines real authority

A role file may prohibit deployment, but the prohibition is weak if the underlying agent still has unrestricted production credentials.

### Architect’s Note: Role outputs are architecture artifacts

Plans, findings, validation reports, and approval packages should be retained as engineering evidence.

### Architect’s Note: Multi-agent does not automatically mean better

Additional agents increase coordination cost, latency, token usage, and conflict-management requirements.

### Architect’s Note: Human accountability cannot be delegated

An AI agent may recommend, analyze, implement, and validate, but accountable enterprise approval remains a human or formally authorized organizational responsibility.

## Alpha Car Detailing example

A role architecture should reflect the risk of corporate authorization and public contract changes, not merely the number of files being modified.

## Recommended table

None required; use consistent Architect’s Note callout boxes.

---

# 14. Enterprise Tips

## Section title

**Enterprise Tips**

## Purpose

Provide operational advice for teams adopting roles across repositories and engineering groups.

## Key topics

### Enterprise Tip: Start with a small role set

Begin with Lead, Developer, Reviewer, Validator, and Evaluator.

### Enterprise Tip: Add specialist roles based on risk

Introduce Architect or Security Reviewer roles when justified by architecture and security exposure.

### Enterprise Tip: Maintain a role registry

Track owner, version, scope, authority, and status.

### Enterprise Tip: Integrate roles with branch protection

Role recommendations should not replace repository permissions and approval policies.

### Enterprise Tip: Use standard output schemas

Make plans, review reports, and validation reports comparable across teams.

### Enterprise Tip: Review authority quarterly

Tool permissions and responsibilities tend to expand over time.

### Enterprise Tip: Log role identity in every run

Record role name, version, prompt version, instruction version, and skills used.

### Enterprise Tip: Measure escalation quality

Too few escalations may indicate unsafe autonomy; too many may indicate poor role design.

### Enterprise Tip: Create role test scenarios

Maintain known-good and known-bad examples for regression testing.

### Enterprise Tip: Keep vendor adapters separate

The enterprise role model should remain stable even when Claude, Copilot, or Codex execution mechanics change.

## Alpha Car Detailing example

Alpha Car Detailing maintains a shared enterprise role catalog with repository-specific restrictions for Booking, Customer, Payment, and Station services.

## Recommended table

**Enterprise Role Registry Example**

---

# 15. Decision Points

## Section title

**Role Design Decision Points**

## Purpose

Help architects and engineering leaders make deliberate choices when introducing roles.

---

## Decision Point 1: One agent or multiple roles?

### Considerations

* Change risk.
* Need for independent review.
* Specialist knowledge.
* Cost.
* Latency.
* Audit requirements.
* Regulatory impact.

### Alpha Car Detailing example

A documentation correction may use one agent. Corporate authorization changes require multiple roles and human approval.

---

## Decision Point 2: Role or Skill?

### Considerations

* Is the concern about who is responsible?
* Or how recurring work is performed?

### Alpha Car Detailing example

“Security Reviewer” is a Role. “Review JWT authorization policy” may be a Skill.

---

## Decision Point 3: Role or Prompt?

### Considerations

* Is the definition reusable across tasks?
* Or specific to the current feature?

### Alpha Car Detailing example

“Developer Agent” is reusable. “Implement corporate fleet booking” is task-specific.

---

## Decision Point 4: Separate Architect Agent?

### Considerations

* Architectural impact.
* Contract changes.
* Service-boundary questions.
* Long-term consequences.
* Availability of human architect review.

### Alpha Car Detailing example

A new fleet-booking orchestration across services justifies architectural assessment.

---

## Decision Point 5: Separate Security Reviewer?

### Considerations

* Authentication changes.
* Authorization complexity.
* Sensitive data.
* External exposure.
* Compliance.
* Privilege boundaries.

### Alpha Car Detailing example

Corporate-account and station-level access controls justify a security-specific role.

---

## Decision Point 6: Can a reviewer modify code?

### Considerations

* Review independence.
* Traceability.
* Workflow efficiency.
* Severity of issue.
* Whether role reassignment is recorded.

### Recommended decision

The Reviewer should normally report findings. If assigned to fix them, the harness should explicitly transition the agent into a Developer role.

---

## Decision Point 7: Who may approve?

### Considerations

* Decision impact.
* Organizational accountability.
* Legal or compliance obligations.
* Branch protection.
* Security ownership.
* Architecture ownership.

### Alpha Car Detailing example

An AI agent may recommend approval, but a human architect approves breaking service contracts.

---

## Decision Point 8: Central or repository-specific roles?

### Considerations

* Enterprise consistency.
* Repository variation.
* Technology differences.
* Governance overhead.
* Local ownership.

### Recommended approach

Use centrally governed base roles with repository-specific overlays.

---

## Decision Point 9: Sequential or parallel workflow?

### Considerations

* Dependency between roles.
* Cost.
* Time.
* Need for independent findings.
* Context synchronization.

### Alpha Car Detailing example

Architecture and security reviews may run in parallel; validation follows remediation.

---

## Decision Point 10: When should a role escalate?

### Considerations

* Missing authority.
* Policy conflict.
* Ambiguous requirement.
* High-risk assumption.
* Failed validation.
* Contract impact.
* Security concern.

## Recommended table

**Role Design Decision Matrix**

---

# 16. Exercises

## Section title

**Exercises**

## Purpose

Help readers apply role concepts to enterprise scenarios.

## Exercises

### Exercise 1: Define a Developer Agent

Create a role definition for implementing Alpha Car Detailing booking features.

Include:

* Purpose.
* Scope.
* Responsibilities.
* Authority.
* Boundaries.
* Inputs.
* Outputs.
* Escalations.

### Exercise 2: Define a Reviewer Agent

Create a Reviewer Agent that checks Clean Architecture, CQRS, event publication, persistence, tests, logging, and security compliance.

### Exercise 3: Create an authority matrix

Map the Lead, Developer, Reviewer, Validator, Evaluator, Architect, and Security Reviewer roles against:

* Read.
* Write.
* Execute.
* Approve.
* Merge.
* Deploy.

### Exercise 4: Design a handoff contract

Define the artifacts passed from Developer to Reviewer and from Reviewer to Validator.

### Exercise 5: Identify role conflicts

Analyze a scenario in which:

* The Architect Agent recommends a new service.
* The Lead Agent wants to extend an existing service.
* The Developer Agent has already started implementation.

Define the resolution and escalation path.

### Exercise 6: Test a role boundary

Design a test proving that the Validator Agent refuses to edit production code after a failing test.

### Exercise 7: Design the corporate fleet workflow

Create a complete multi-agent workflow from prompt intake to pull request.

### Exercise 8: Reduce unnecessary roles

Given twelve proposed roles, consolidate them into the smallest effective enterprise workflow.

### Exercise 9: Create a role versioning policy

Define how role changes are reviewed, versioned, tested, and rolled back.

### Exercise 10: Compare platforms

Describe how the same Reviewer Agent could be implemented using Claude Code, GitHub Copilot, and Codex without changing its conceptual responsibilities.

### Exercise 11: Define human approval gates

Identify which Alpha Car Detailing changes require:

* Product approval.
* Architect approval.
* Security approval.
* Database approval.
* Release approval.

### Exercise 12: Review a flawed role definition

Provide a deliberately flawed “Full-Stack AI Agent” role and ask readers to identify excessive authority, missing boundaries, poor outputs, and self-approval risks.

## Recommended table

**Exercise Deliverables and Review Criteria**

---

# 17. Interview Questions

## Section title

**Interview Questions**

## Purpose

Assess understanding of AI-agent roles at senior developer, architect, lead, and engineering-management levels.

## Questions

1. What is an AI-agent Role in enterprise software engineering?
2. How does a Role differ from an Instruction?
3. How does a Role differ from a Skill?
4. How does a Role differ from a Prompt?
5. How do Steering Notes influence Roles?
6. Why is a Role more than a persona?
7. What should a complete role definition contain?
8. How should role authority be defined?
9. What is the difference between responsibility and authority?
10. Why are role boundaries important?
11. What is the least-authority principle?
12. Why should a Developer Agent not approve its own work?
13. What is the difference between a Reviewer Agent and a Validator Agent?
14. What is the difference between validation and evaluation?
15. When should an Architect Agent be introduced?
16. When should a Security Reviewer be mandatory?
17. What is a role handoff contract?
18. How should role outputs be structured?
19. How do you prevent role drift?
20. How should role definitions be versioned?
21. How can roles be tested?
22. What is a role-conflict resolution process?
23. When should an AI role escalate to a human?
24. What kinds of decisions should remain under human approval?
25. How do roles operate inside an AI harness?
26. What are the advantages and risks of multi-agent workflows?
27. When is a single-agent workflow preferable?
28. How can parallel agents create conflicting conclusions?
29. How should a Lead Agent coordinate other roles?
30. What authority should a Validator Agent have?
31. Should a Reviewer Agent be allowed to modify code?
32. How should role activity be audited?
33. How do tool permissions relate to role authority?
34. How can enterprise base roles be adapted to individual repositories?
35. How would you design roles for Alpha Car Detailing corporate fleet booking?
36. How would role implementation differ among Claude Code, GitHub Copilot, and Codex?
37. What is the omnipotent-agent anti-pattern?
38. What is human-approval theater?
39. How can role effectiveness be measured?
40. Why should the role model remain vendor-neutral?

## Recommended table

Group questions by:

* Foundational.
* Design.
* Governance.
* Architecture.
* Scenario-based.
* Platform comparison.

---

# 18. Chapter Summary

## Section title

**Chapter Summary**

## Purpose

Reinforce the chapter’s central distinctions and prepare readers for Chapter 10 on Steering Notes.

## Key topics

* Roles define who performs work, from what perspective, with what responsibility and authority.
* Instructions define persistent standards.
* Skills define reusable procedures.
* Prompts define the current task.
* Steering Notes define current mission context.
* Roles require explicit scope, responsibilities, authority, boundaries, inputs, outputs, handoffs, and escalation.
* Role separation reduces self-review and confirmation bias.
* Multi-agent workflows should be introduced based on risk and responsibility, not fashion.
* Tool permissions must enforce stated authority.
* Role definitions require ownership, versioning, governance, and testing.
* Human approval remains mandatory for high-impact enterprise decisions.
* Claude Code, GitHub Copilot, and Codex may implement roles differently, but the engineering model remains vendor-neutral.
* The next chapter will explain how Steering Notes provide temporary priorities, constraints, and mission context to these governed roles.

## Alpha Car Detailing example

The corporate fleet booking change demonstrates how Lead, Architect, Developer, Reviewer, Security Reviewer, Validator, and Evaluator roles collaborate while preserving independent responsibility and human approval.

## Recommended diagram or table

**Summary table:** “Instructions, Skills, Prompts, Roles, and Steering Notes”

| Concept        | Defines                                             |
| -------------- | --------------------------------------------------- |
| Instructions   | Persistent standards and constraints                |
| Skills         | Reusable procedures                                 |
| Prompts        | Current task and expected outcome                   |
| Roles          | Responsibility, perspective, authority, and outputs |
| Steering Notes | Current priorities and mission context              |
| Harness        | Execution, orchestration, validation, and evidence  |

---

# 19. Further Reading

## Section title

**Further Reading**

## Purpose

Direct readers to authoritative concepts and project materials that deepen understanding of responsibility design, governance, security, and multi-agent orchestration.

## Key topics

### Internal handbook references

* Chapter 5 — Repository Discovery.
* Chapter 6 — Instructions.
* Chapter 7 — Skills.
* Chapter 8 — Prompts.
* Chapter 10 — Steering Notes.
* Chapter 11 — Knowledge Sources.
* Part III — Harness Engineering.
* Chapter 14 — Lead Agent.
* Chapter 15 — Developer Agent.
* Chapter 16 — Reviewer Agent.
* Chapter 17 — Validator Agent.
* Chapter 18 — Evaluator Agent.
* Part V — Enterprise AI Governance.

### External topic areas

* Separation of duties.
* Least privilege.
* Responsibility assignment matrices.
* Software architecture governance.
* Secure software development lifecycle.
* Pull-request review practices.
* Human-in-the-loop AI systems.
* Multi-agent system coordination.
* AI-agent evaluation.
* Auditability and traceability.
* Change-management governance.

### Platform documentation categories

* Claude Code repository instructions, skills, commands, permissions, and agent workflows.
* GitHub Copilot custom instructions, prompt files, coding-agent workflows, and pull-request integration.
* OpenAI Codex repository guidance, task execution, sandboxing, validation, and review workflows.

## Alpha Car Detailing example

Readers should apply further reading to improve the role registry, authority matrix, handoff contracts, and approval gates used by the corporate fleet booking workflow.

## Recommended table

**Further Reading by Role Concern**

| Concern                  | Reading area                             |
| ------------------------ | ---------------------------------------- |
| Authority                | Least privilege and separation of duties |
| Review independence      | Pull-request governance                  |
| Security role            | Secure development lifecycle             |
| Multi-agent coordination | Multi-agent systems                      |
| Human approval           | Human-in-the-loop governance             |
| Role testing             | AI evaluation and regression testing     |

Chapter 9 outline is complete.
