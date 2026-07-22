# Chapter 6 — Instructions

## Part II — Repository Intelligence

**Target file:** `book/06-instructions.md`

---

## 1. Story-Driven Opening — The Feature That Compiled but Did Not Belong

### Purpose

Open the chapter with a realistic enterprise scenario showing why repository discovery alone is insufficient. Establish that an AI agent may understand a repository’s structure and still produce an architecturally invalid, insecure, or operationally incomplete implementation when enduring engineering expectations have not been explicitly defined.

### Narrative Scenario

Alpha Car Detailing asks an AI coding agent to add corporate fleet booking functionality.

The agent has already completed repository discovery and identified:

* Booking Service
* Corporate Account Service
* Fleet Service
* Station Operations Service
* API gateway
* Shared event contracts
* SQL databases
* Redis
* Kubernetes manifests
* CI/CD pipelines
* Unit and integration test projects

The agent produces code that compiles and passes basic tests. However, the implementation:

* Places corporate eligibility logic in an API controller
* Reads the Corporate Account database directly from Booking Service
* Publishes an unversioned event
* Omits the transactional outbox
* Adds a non-approved package
* Bypasses policy-based authorization
* Returns internal exception messages
* Stores a client secret in an application configuration file
* Adds no OpenTelemetry instrumentation
* Provides no pull-request compliance evidence

The failure did not occur because the repository was undiscovered. It occurred because the repository’s enduring engineering expectations were not expressed as explicit, discoverable, governed Instructions.

### Central Principle

> An AI agent should not infer critical engineering standards from code patterns alone when those standards can be explicitly defined, versioned, governed, and validated through repository Instructions.

### Key Concepts

* Repository Discovery explains what exists.
* Instructions define how work must be performed.
* Existing code is evidence, but not always authoritative guidance.
* Compilation is not proof of architectural compliance.
* Instructions are engineering artifacts, not conversational preferences.
* Instructions must be owned, reviewed, versioned, discoverable, and testable.

### Alpha Car Detailing Application

Introduce the chapter’s recurring feature request:

> Add corporate fleet booking functionality that allows authorized corporate customers to reserve detailing capacity across multiple stations.

This feature will be reused throughout the chapter to demonstrate:

* Scope resolution
* Instruction hierarchy
* Conflict handling
* Architecture compliance
* Security requirements
* Event publishing requirements
* Testing obligations
* Harness validation
* Governance and human approval

### Recommended Diagram

**Figure 6-1 — From Repository Understanding to Governed Implementation**

```text
Repository Discovery
        ↓
Applicable Instructions
        ↓
Task Prompt
        ↓
Skill Selection
        ↓
Implementation
        ↓
Validation and Compliance Evidence
```

### Recommended Callout

> **Common Mistake**
> A repository that is easy to navigate is not necessarily safe to modify. Discovery provides context; Instructions establish acceptable behavior.

---

## 2. Learning Objectives

### Purpose

Define the architectural, operational, and governance capabilities readers should acquire.

### Learning Objectives

By the end of this chapter, the reader should be able to:

1. Define Instructions as persistent engineering guidance.
2. Distinguish Instructions from Skills, Prompts, Roles, Steering Notes, Knowledge Sources, and Governance Policies.
3. Identify engineering behaviors that should be made explicit.
4. Design repository, directory, service, project, language, and organization-level Instruction scopes.
5. Define Instruction inheritance, specialization, precedence, and override behavior.
6. Classify Instructions by purpose and strength.
7. Author clear, specific, testable, and maintainable Instructions.
8. Resolve conflicts between multiple Instruction sources.
9. Establish ownership, versioning, review, exception, and approval workflows.
10. Validate Instructions through tests, static analysis, CI/CD, review controls, and harness evaluators.
11. Detect stale, duplicated, contradictory, and drifting Instructions.
12. Design an effective Instruction model for Alpha Car Detailing.
13. Compare Claude Code, GitHub Copilot, and OpenAI Codex Instruction mechanisms.
14. Integrate Instructions into an enterprise AI harness.
15. Ensure self-learning systems recommend Instruction improvements without silently modifying standards.

---

## 3. Background — From Inferred Conventions to Explicit Engineering Guidance

### Purpose

Explain why traditional repository conventions are not sufficient for AI-assisted enterprise development.

### Key Concepts

* Human developers learn conventions through onboarding, reviews, historical context, architecture discussions, and institutional knowledge.
* AI agents often receive only the repository, current task, execution environment, and available Instruction sources.
* Code patterns may contain:

  * Current standards
  * Legacy conventions
  * Temporary workarounds
  * Incomplete migrations
  * Experimental designs
  * Security exceptions
  * Deprecated dependencies
* Frequency does not establish authority.
* Repeated code does not necessarily represent approved architecture.
* An AI agent may amplify existing inconsistencies when Instructions are absent.

### Important Subsections

#### 3.1 The Limits of Pattern Inference

Discuss why an agent cannot safely determine whether a repeated pattern is:

* Required
* Preferred
* Accidental
* Legacy
* Deprecated
* Temporarily tolerated
* Under active replacement

#### 3.2 Institutional Knowledge and AI Agents

Explain how experienced team members know unwritten constraints such as:

* Which service owns a domain concept
* Which package is being phased out
* Which database cannot be queried directly
* Which event contract requires compatibility approval
* Which deployment requires a security review
* Which code patterns are preserved only for backward compatibility

#### 3.3 Why Conversational Reminders Are Insufficient

Explain that chat-level reminders:

* Are easy to omit
* Are not consistently inherited
* Are not version-controlled
* Are difficult to audit
* May conflict across sessions
* Are not easily validated
* Do not provide reliable organizational governance

#### 3.4 Instructions as Repository Intelligence

Position Instructions as the second discipline of Repository Intelligence:

```text
Repository Discovery → What exists?
Instructions         → How must work be performed?
Skills               → How is recurring work executed?
Prompts              → What must be done now?
```

### Alpha Car Detailing Application

Show that the Booking Service may contain both:

* Current Clean Architecture handlers
* Older controllers with embedded business logic

Without explicit Instructions, an agent may copy the older implementation because it appears simpler or more common.

### Recommended Table

**Table 6-1 — Evidence Sources and Their Authority**

| Evidence Source        | What It Reveals                 |        Typical Authority | Primary Risk                                    |
| ---------------------- | ------------------------------- | -----------------------: | ----------------------------------------------- |
| Existing code          | Current implementation patterns |                   Medium | May include legacy or invalid patterns          |
| Architecture decisions | Approved design reasoning       |                     High | May be outdated or incomplete                   |
| Tests                  | Enforced behavior               | High for tested behavior | May not cover all requirements                  |
| Documentation          | Intended behavior               |                 Variable | May drift from code                             |
| Instructions           | Required engineering behavior   |                     High | Must be governed and maintained                 |
| Prompt                 | Current task                    |            Task-specific | May conflict with enduring constraints          |
| Steering Note          | Temporary direction             |                Temporary | Must not silently weaken mandatory requirements |

---

## 4. Concepts — Instructions as a Formal Engineering Artifact

### Purpose

Define the handbook’s Instruction model and establish the terminology used throughout the chapter.

### 4.1 Definition of an Instruction

An Instruction is persistent organizational, repository, directory, project, service, language, framework, or platform guidance that defines how an AI agent must or should operate within an applicable scope.

An Instruction may communicate:

* Architectural constraints
* Coding standards
* Security obligations
* Testing requirements
* Dependency controls
* API conventions
* Eventing standards
* Persistence rules
* Observability expectations
* Review requirements
* Prohibited actions
* Approval boundaries
* Exception processes

### 4.2 Characteristics of Professional Instructions

Professional Instructions are:

* Explicit
* Persistent
* Scoped
* Discoverable
* Version-controlled
* Owned
* Reviewable
* Governed
* Testable where possible
* Traceable to architecture or policy
* Capable of producing compliance evidence

### 4.3 What Instructions Are Not

Instructions are not interchangeable with:

* Prompts
* Skills
* Roles
* Steering Notes
* Knowledge Sources
* Governance Policies
* Generic documentation
* Tool configuration
* Informal conversational preferences

### 4.4 Instructions Define Enduring Behavior

Establish the core distinction:

* **Instructions define enduring behavior.**
* **Skills define reusable procedures.**
* **Prompts define current work.**
* **Roles define responsibilities and authority.**
* **Steering Notes define temporary direction.**
* **Knowledge Sources provide reference information.**
* **Governance Policies define broader organizational controls.**

### Recommended Table

**Table 6-2 — Instructions, Skills, Prompts, Roles, Steering Notes, and Knowledge Sources**

| Artifact          | Primary Question                        | Duration                 | Typical Scope                  | Mandatory by Default? | Example                                      |
| ----------------- | --------------------------------------- | ------------------------ | ------------------------------ | --------------------: | -------------------------------------------- |
| Instruction       | How must work be performed?             | Persistent               | Organization to project        |                 Often | Do not access another service’s database     |
| Skill             | How is recurring work performed?        | Reusable                 | Task or engineering capability |   Procedure-dependent | Create an idempotent Event Hub consumer      |
| Prompt            | What should be done now?                | Current task             | Task                           |                    No | Add corporate fleet booking                  |
| Role              | Who is responsible for which decisions? | Persistent or task-bound | Agent or workflow              |   Authority-dependent | Reviewer must assess architecture compliance |
| Steering Note     | What currently matters?                 | Temporary                | Sprint, release, mission       |     Context-dependent | Prioritize Booking Service modernization     |
| Knowledge Source  | What information may inform the work?   | Persistent or external   | Reference                      |                    No | Event contract documentation                 |
| Governance Policy | What organizational control applies?    | Persistent               | Enterprise                     |       Often mandatory | Secrets must be stored in approved vaults    |

### Recommended Diagram

**Figure 6-2 — Relationship Between Instructions, Skills, Prompts, and Steering Notes**

```text
Governance Policies
        ↓
Instructions
        ↓ constrain
Skills
        ↓ execute
Prompt
        ↓ contextualized by
Steering Notes
        ↓ informed by
Knowledge Sources
```

### Recommended Callout

> **Architect’s Note**
> The distinction between Instructions and Skills is critical. An Instruction may require idempotent event handling. A Skill describes the reusable implementation procedure used to achieve it.

---

## 5. Instruction Classification Model

### Purpose

Provide a classification system that allows Instructions to be organized, reviewed, governed, and validated consistently.

### Instruction Categories

#### 5.1 Architectural Instruction

Defines structural and dependency constraints.

Examples:

* Domain projects must not reference Infrastructure.
* Services must not access another service’s database.
* Cross-domain communication must use approved APIs or events.

#### 5.2 Implementation Instruction

Defines coding and implementation expectations.

Examples:

* Use dependency injection for external adapters.
* Use cancellation tokens for asynchronous I/O.
* Do not place domain decisions in controllers.

#### 5.3 Security Instruction

Defines identity, authorization, secrets, input validation, and secure coding requirements.

Examples:

* All corporate fleet operations require policy-based authorization.
* Secrets must not be stored in source control.
* Sensitive values must not appear in logs.

#### 5.4 Testing Instruction

Defines required test coverage, test types, and validation expectations.

Examples:

* New domain behavior requires unit tests.
* External contracts require integration or contract tests.
* Architecture dependency direction must be protected by architecture tests.

#### 5.5 Operational Instruction

Defines runtime, deployment, reliability, and observability expectations.

Examples:

* Services must expose health checks.
* Distributed operations must propagate correlation identifiers.
* New dependencies require operational readiness review.

#### 5.6 Documentation Instruction

Defines required technical and operational documentation.

Examples:

* Public APIs must update OpenAPI documentation.
* New events must include schema documentation.
* Architectural deviations require an ADR.

#### 5.7 Workflow Instruction

Defines repository and delivery process requirements.

Examples:

* Pull requests must include test evidence.
* Database migrations require rollback notes.
* Generated files must not be edited manually.

#### 5.8 Prohibition

Defines behavior that is explicitly disallowed.

Examples:

* Do not commit secrets.
* Do not bypass service ownership boundaries.
* Do not disable failing tests to complete a task.

#### 5.9 Approval Requirement

Defines actions requiring human authorization.

Examples:

* New production dependencies require architecture approval.
* Security controls may not be weakened without security review.
* Shared event contract breaking changes require consumer approval.

#### 5.10 Exception Rule

Defines the approved path for temporary or permanent deviation.

Examples:

* Legacy projects may retain an older package until migration milestone M4.
* An exception must reference an approved architecture decision.

#### 5.11 Platform-Specific Instruction

Defines behavior required by a particular agent or execution platform.

Examples:

* Claude Code must run repository validation commands before completing.
* Copilot agent workflows must include repository-specific test commands.
* Codex must not modify generated deployment manifests directly.

#### 5.12 Temporary Override

Defines a time-bound and explicitly approved specialization.

Examples:

* During the Booking Service migration, new persistence code must use the compatibility adapter.
* The override expires after the migration release.

### Classification Guidance

Explain that one Instruction may belong to multiple categories. For example:

> “All new event consumers must implement idempotency and include replay integration tests.”

This is simultaneously:

* Architectural
* Operational
* Testing
* Workflow-related

The primary classification should reflect ownership and governance needs.

### Recommended Table

**Table 6-3 — Instruction Categories, Owners, and Validation Methods**

| Category             | Typical Owner           | Example Validation                          |
| -------------------- | ----------------------- | ------------------------------------------- |
| Architectural        | Architecture team       | Architecture tests, review                  |
| Implementation       | Service team            | Static analysis, code review                |
| Security             | Security team           | SAST, policy checks, review                 |
| Testing              | Quality engineering     | Test execution, coverage checks             |
| Operational          | Platform/SRE            | Deployment checks, observability validation |
| Documentation        | Technical owner         | Documentation review                        |
| Workflow             | Engineering enablement  | CI/CD gate, PR template                     |
| Prohibition          | Governance owner        | Automated block or escalation               |
| Approval Requirement | Named authority         | Approval record                             |
| Exception Rule       | Architecture/governance | Exception registry                          |
| Platform-Specific    | AI platform owner       | Harness evaluator                           |
| Temporary Override   | Designated approver     | Expiry and scope validation                 |

---

## 6. Instruction Strength Model

### Purpose

Define a consistent vocabulary for expressing the authority and expected compliance level of Instructions.

### Strength Levels

#### 6.1 Mandatory

Must be followed. Deviation is not permitted without an explicitly defined exception process.

Example:

> All production secrets must be obtained from the approved secrets-management platform.

#### 6.2 Required Unless Formally Exempted

Must be followed unless a documented and approved exception exists.

Example:

> New services must use the standard observability package unless an architecture exception is approved.

#### 6.3 Recommended

Should be followed in normal circumstances. A documented rationale should be provided when not followed.

Example:

> Prefer immutable domain value objects for identifiers and constrained values.

#### 6.4 Preferred

Represents the organization’s default choice but allows justified alternatives.

Example:

> Prefer constructor injection over service location.

#### 6.5 Informational

Provides context, rationale, or non-binding guidance.

Example:

> The Booking Service is scheduled for modular decomposition in the next architecture phase.

#### 6.6 Prohibited

Must not be performed.

Example:

> Do not query another service’s database.

### Ambiguous Strength Language

Explain why wording such as the following is unreliable:

* Usually
* Try to
* Ideally
* When possible
* Where appropriate
* Consider
* Preferably

These terms may be acceptable only when accompanied by decision criteria.

Weak:

> Use caching where appropriate.

Stronger:

> Cache station availability only when the response can tolerate up to 30 seconds of staleness. Do not cache final booking confirmation or authorization decisions.

### Recommended Table

**Table 6-4 — Instruction Strength and Expected Agent Behavior**

| Strength                 | Agent Behavior                  | Deviation Handling              |
| ------------------------ | ------------------------------- | ------------------------------- |
| Mandatory                | Follow                          | Stop and escalate if impossible |
| Required unless exempted | Follow                          | Require approved exception      |
| Recommended              | Follow by default               | Record rationale for deviation  |
| Preferred                | Select unless better fit exists | Explain alternative             |
| Informational            | Use as context                  | No compliance action            |
| Prohibited               | Never perform                   | Block implementation            |

### Recommended Callout

> **Decision Point**
> If an Instruction is important enough to affect security, compliance, data ownership, or production reliability, its strength should not be left implicit.

---

## 7. Instruction Anatomy

### Purpose

Define the content expected in a professionally authored Instruction.

### Recommended Anatomy

1. **Title**
2. **Purpose**
3. **Scope**
4. **Applicability**
5. **Strength**
6. **Mandatory behavior**
7. **Prohibited behavior**
8. **Rationale**
9. **Exceptions**
10. **Examples**
11. **Validation method**
12. **Owner**
13. **Approver**
14. **Review date**
15. **Related architecture decision**
16. **Related Skill**
17. **Related test, policy, or CI control**
18. **Version or change history**

### Lightweight Instruction Format

Use for local, low-risk, easily understood guidance.

Example structure:

```markdown
## API Error Handling

All HTTP APIs must return RFC 7807 Problem Details for expected client and
domain errors. Internal exception details must not be returned.

Validation:
- API integration tests
- Global exception middleware tests
```

### Structured Instruction Format

Use when the Instruction:

* Has enterprise-wide authority
* Controls security or compliance
* Requires formal exception handling
* Applies across multiple repositories
* Has a named approver
* Requires traceability
* Is enforced by automation
* Has an expiry or review date

### Example Structured Metadata

```yaml
id: INS-API-004
title: API error response standard
category:
  - implementation
  - security
strength: mandatory
scope: services/http-api
owner: platform-architecture
review_date: 2027-01-15
validation:
  - architecture-test
  - integration-test
  - pull-request-review
related_decisions:
  - ADR-018
```

### Alpha Car Detailing Application

Define a structured Instruction for API error handling:

* Applies to Booking Service HTTP endpoints
* Requires RFC 7807
* Prohibits internal stack traces
* Requires correlation identifier inclusion
* Is validated through integration tests and middleware configuration checks
* Is owned by Platform Architecture
* Is reviewed annually

### Recommended Table

**Table 6-5 — Lightweight Versus Structured Instruction Formats**

| Factor       | Lightweight      | Structured                            |
| ------------ | ---------------- | ------------------------------------- |
| Risk         | Low to moderate  | Moderate to high                      |
| Scope        | Local            | Cross-team or enterprise              |
| Exceptions   | Rare or informal | Formal                                |
| Automation   | Minimal          | Strong                                |
| Metadata     | Limited          | Explicit                              |
| Traceability | Basic            | Required                              |
| Governance   | Team-level       | Architecture, security, or enterprise |

---

## 8. Instruction Quality Model

### Purpose

Introduce a reusable framework for assessing whether Instructions are suitable for enterprise use.

### Quality Dimensions

#### 8.1 Clarity

Can the Instruction be understood without interpretation from the author?

#### 8.2 Specificity

Does it describe the required behavior precisely enough to guide implementation?

#### 8.3 Scope

Is it clear where and when the Instruction applies?

#### 8.4 Rationale

Does it explain why the Instruction exists, particularly for non-obvious constraints?

#### 8.5 Testability

Can compliance be verified through automation, review, or evidence?

#### 8.6 Consistency

Does it align with other Instructions, architecture decisions, tests, and policies?

#### 8.7 Discoverability

Will the applicable agent reliably find it?

#### 8.8 Maintainability

Can it be updated without creating duplication or widespread inconsistency?

#### 8.9 Traceability

Can it be linked to an owner, decision, test, policy, or compliance control?

#### 8.10 Enforceability

Can violations be prevented, detected, or escalated?

#### 8.11 Ownership

Is responsibility for its accuracy and approval explicit?

#### 8.12 Freshness

Does it reflect the repository’s current architecture and operating model?

### Weak Versus Strong Examples

#### Error Handling

Weak:

> Use good error handling.

Strong:

> API endpoints must return RFC 7807 Problem Details for expected client and domain errors. Do not return internal exception details. Unexpected exceptions must be logged with correlation identifiers and handled by the global exception middleware.

#### Service Boundaries

Weak:

> Keep services independent.

Strong:

> A service must not read or write another service’s database. Cross-service data must be obtained through an approved API, event, or replicated read model.

#### Event Publishing

Weak:

> Publish events reliably.

Strong:

> Domain events that represent committed business state must be written to the service outbox in the same database transaction as the state change. Direct publication from the request handler is prohibited.

#### Testing

Weak:

> Add tests where needed.

Strong:

> New domain behavior requires unit tests. New database queries require integration tests against the supported database engine. New shared event contracts require producer and consumer compatibility tests.

#### Security

Weak:

> Protect corporate functionality.

Strong:

> Corporate fleet booking endpoints require authenticated callers and the `CorporateFleetBooking` authorization policy. Tenant and corporate account identifiers must be derived from validated claims or server-side lookup, not trusted from request payloads alone.

### Recommended Table

**Table 6-6 — Instruction Quality Checklist**

| Dimension       | Review Question                                 |
| --------------- | ----------------------------------------------- |
| Clarity         | Could two engineers interpret this differently? |
| Specificity     | Does it state the required behavior?            |
| Scope           | Is applicability explicit?                      |
| Rationale       | Is the engineering reason documented?           |
| Testability     | How will compliance be verified?                |
| Consistency     | Does it conflict with another source?           |
| Discoverability | Will the agent load it?                         |
| Maintainability | Is guidance duplicated elsewhere?               |
| Traceability    | Is it linked to ownership and evidence?         |
| Enforceability  | Can violations be blocked or detected?          |
| Ownership       | Who approves changes?                           |
| Freshness       | When was it last reviewed?                      |

### Recommended Callout

> **Enterprise Tip**
> A useful Instruction tells the agent what to do, where it applies, why it matters, and how compliance will be demonstrated.

---

## 9. Instructions Versus Repository Conventions

### Purpose

Explain how Instructions relate to existing code patterns without treating the codebase as an infallible source of standards.

### Key Concepts

* Conventions are inferred from repository evidence.
* Instructions explicitly state intended behavior.
* Instructions may confirm, clarify, constrain, replace, or deprecate conventions.
* Repository discovery should inform Instruction authoring.
* Instruction authors must not describe an architecture that the repository cannot yet support without acknowledging migration state.

### Important Subsections

#### 9.1 When Existing Conventions Are Trustworthy

Trust is stronger when patterns are:

* Consistent
* Recent
* Tested
* Documented
* Referenced by architecture decisions
* Enforced through CI/CD
* Present in current service templates

#### 9.2 When Existing Conventions Are Unsafe to Copy

Caution is required when patterns are:

* In legacy modules
* Marked obsolete
* Inconsistent across services
* Unsupported by tests
* Security-sensitive
* Under migration
* Contradicted by architecture decisions

#### 9.3 Declared Standards Versus Observed Standards

Introduce a comparison:

* **Declared standard:** The behavior expressed by Instructions.
* **Observed standard:** The behavior inferred from repository implementation.
* **Enforced standard:** The behavior protected by tests or automation.

Misalignment among these states is a form of Instruction drift.

### Alpha Car Detailing Application

The repository contains two event publishing patterns:

1. Direct publication from application handlers
2. Transactional outbox publication

The root Instruction states that all new business events must use the outbox. The older direct-publication pattern is therefore historical evidence, not an implementation template.

### Recommended Diagram

**Figure 6-3 — Declared, Observed, and Enforced Engineering Behavior**

```text
Declared Instructions
        ↘
          Engineering Standard
        ↗
Observed Repository Patterns

Enforcement Controls validate both
```

---

## 10. Instructions Versus Prompts

### Purpose

Prevent task requests from being confused with enduring repository guidance.

### Key Concepts

* A Prompt specifies current work.
* An Instruction constrains how that work must be performed.
* A Prompt may add task-specific acceptance criteria.
* A Prompt must not silently weaken mandatory Instructions.
* Repeated Prompt content may indicate missing Instructions.

### Example

Prompt:

> Add a corporate fleet booking endpoint.

Applicable Instructions:

* Follow Clean Architecture dependency direction.
* Require policy-based authorization.
* Return RFC 7807 errors.
* Use the transactional outbox.
* Add unit and integration tests.
* Propagate correlation identifiers.

### Prompt Override Boundaries

A task Prompt cannot authorize:

* Disabling security controls
* Committing secrets
* Bypassing service ownership
* Ignoring compliance requirements
* Removing required tests
* Weakening production safeguards

unless the request contains an approved exception from an authorized human.

### Alpha Car Detailing Application

Show two versions of the same Prompt:

Weak task Prompt:

> Add fleet booking quickly. Skip integration tests if necessary.

Governed interpretation:

* “Quickly” affects prioritization.
* “Skip integration tests” conflicts with a mandatory testing Instruction.
* The agent must preserve the Instruction and request an exception if needed.

### Recommended Callout

> **Common Mistake**
> A Prompt is not more authoritative merely because it is recent. Recency does not override security, compliance, or architecture authority.

---

## 11. Instructions Versus Skills

### Purpose

Clarify the distinction between mandatory engineering expectations and reusable implementation procedures.

### Key Concepts

* Instructions define outcomes and constraints.
* Skills define repeatable procedures.
* A Skill must execute within applicable Instructions.
* An Instruction may reference an approved Skill.
* Skills may evolve more frequently than Instructions.

### Example

Instruction:

> All new event consumers must be idempotent and support replay.

Skill:

> Implement an idempotent Event Hub consumer using the event ledger, checkpoint store, retry policy, poison-message handling, and replay workflow.

### Alpha Car Detailing Application

Applicable Instruction:

* Corporate booking events must be idempotently consumed.

Selected Skill:

* `create-idempotent-event-consumer`

The Skill provides the procedure, but the Instruction establishes the obligation.

### Recommended Table

**Table 6-7 — Instruction Versus Skill**

| Question        | Instruction              | Skill                              |
| --------------- | ------------------------ | ---------------------------------- |
| Primary purpose | Define required behavior | Define reusable procedure          |
| Typical wording | Must, must not, required | Steps, inputs, outputs, validation |
| Duration        | Enduring                 | Reusable and evolvable             |
| Scope           | Organization to project  | Engineering task                   |
| Compliance      | Evaluated                | Execution quality evaluated        |
| Example         | Use an outbox            | Implement an outbox publisher      |

---

## 12. Instructions Versus Steering Notes

### Purpose

Distinguish enduring standards from temporary mission context.

### Key Concepts

* Instructions remain applicable across tasks.
* Steering Notes express current priorities and temporary constraints.
* Steering Notes may specialize work sequencing.
* Steering Notes must not silently weaken mandatory Instructions.
* Temporary overrides must be explicit, authorized, scoped, and time-bound.

### Example

Instruction:

> New business events must use versioned schemas.

Steering Note:

> During the current release, prioritize corporate booking compatibility with the existing `booking.v1` consumer.

The Steering Note affects implementation strategy but does not authorize unversioned events.

### Alpha Car Detailing Application

A Steering Note states:

> Avoid modifying the Fleet Service during the current release freeze.

The agent may adapt by:

* Using an approved Booking Service integration
* Deferring Fleet Service changes
* Proposing follow-up work

It may not:

* Query the Fleet database directly
* Duplicate Fleet ownership logic permanently

### Recommended Callout

> **Decision Point**
> Temporary direction should remain temporary. When a Steering Note begins defining enduring behavior, it should be evaluated as a proposed Instruction change.

---

## 13. Instructions Versus Knowledge Sources

### Purpose

Explain the difference between mandatory guidance and reference material.

### Key Concepts

Knowledge Sources may include:

* Architecture documentation
* API specifications
* Product requirements
* Vendor documentation
* Domain glossaries
* ADRs
* Incident reports
* Runbooks
* External standards

A Knowledge Source informs decisions but does not automatically impose behavior unless referenced by an Instruction or Governance Policy.

### Example

Knowledge Source:

* OpenTelemetry semantic conventions documentation

Instruction:

> Booking operations must emit traces using the organization’s approved semantic naming and correlation conventions.

### Alpha Car Detailing Application

The fleet booking business process document explains reservation states. The repository Instruction determines:

* Which service owns those states
* How state changes are persisted
* Which events must be emitted
* Which tests are required

### Recommended Table

**Table 6-8 — Knowledge Source Versus Instruction**

| Characteristic | Knowledge Source              | Instruction                            |
| -------------- | ----------------------------- | -------------------------------------- |
| Purpose        | Inform                        | Direct behavior                        |
| Binding        | Not automatically             | According to strength                  |
| Ownership      | Documentation or domain owner | Engineering/governance owner           |
| Validation     | Accuracy review               | Compliance validation                  |
| Example        | OAuth specification           | Use approved JWT validation middleware |

---

## 14. Instructions Versus Governance Policies

### Purpose

Explain how repository Instructions relate to organizational controls.

### Key Concepts

* Governance Policies operate beyond a single repository.
* Instructions translate broader controls into actionable engineering behavior.
* Repository Instructions may specialize policy implementation.
* Repository Instructions must not silently weaken organization-wide mandatory controls.
* Policy conflicts require escalation to authorized governance owners.

### Example

Governance Policy:

> Production secrets must be centrally managed and auditable.

Repository Instruction:

> Alpha Car Detailing services must obtain production secrets through Azure Key Vault using workload identity. Static client secrets are prohibited.

### Authority Principle

> A lower-level Instruction may specialize a broader Instruction but must not silently weaken mandatory security, compliance, or governance requirements.

---

## 15. Architecture Discussion — Instruction Scope

### Purpose

Define how Instructions are applied to organizational and repository structures.

### Scope Levels

#### 15.1 Organization-Wide Instructions

Apply across repositories and teams.

Typical content:

* Security standards
* Compliance obligations
* Approved license requirements
* Secrets handling
* AI usage governance
* Human approval boundaries
* Enterprise observability requirements

#### 15.2 Repository-Root Instructions

Apply to all work in a repository unless specialized by a lower scope.

Typical content:

* Architecture style
* Repository layout
* Standard validation commands
* Dependency rules
* Pull-request expectations
* Shared terminology

#### 15.3 Directory-Level Instructions

Apply to a subtree.

Typical content:

* Infrastructure conventions
* Test project standards
* Documentation requirements
* Generated-code restrictions
* Deployment manifest requirements

#### 15.4 Project-Level Instructions

Apply to a buildable project or package.

Typical content:

* Language version
* Framework conventions
* Project-specific dependencies
* Test framework
* Build commands

#### 15.5 Service-Level Instructions

Apply to a bounded service.

Typical content:

* Domain ownership
* Data ownership
* API conventions
* Event publishing requirements
* Service-specific dependencies
* Operational controls

#### 15.6 Language-Specific Instructions

Apply to files or projects written in a particular language.

Examples:

* C# nullable reference types
* Async and cancellation behavior
* JavaScript linting standards
* SQL migration requirements

#### 15.7 Framework-Specific Instructions

Apply to a framework or infrastructure technology.

Examples:

* ASP.NET Core middleware conventions
* Entity Framework migration restrictions
* Kubernetes security-context requirements
* Terraform module standards

#### 15.8 Task-Specific Temporary Overrides

Apply to one approved task or change.

Requirements:

* Explicit scope
* Named approver
* Rationale
* Expiry
* Non-conflict with mandatory governance controls
* Recorded exception evidence

### Alpha Car Detailing Application

Example scope map:

```text
Organization
├── Security and compliance Instructions
└── AI governance Instructions

Repository Root
├── Clean Architecture
├── Testing baseline
├── Pull-request evidence
└── Validation commands

services/booking
├── Booking domain ownership
├── Booking API conventions
├── Booking event requirements
└── Booking persistence rules

services/booking/tests
└── Booking test conventions

infrastructure/
├── Terraform requirements
├── Kubernetes security
└── Secrets references
```

### Recommended Diagram

**Figure 6-4 — Instruction Scope Pyramid**

```text
             Organization
          Repository Root
        Directory / Domain
       Service / Project
   Language / Framework / Path
      Temporary Approved Override
```

### Recommended Table

**Table 6-9 — Scope and Ownership Matrix**

| Scope              | Example                    | Owner                  | Typical Approval                    |
| ------------------ | -------------------------- | ---------------------- | ----------------------------------- |
| Organization       | Secrets handling           | Security governance    | Security authority                  |
| Repository         | Architecture direction     | Repository architects  | Architecture review                 |
| Directory          | Infrastructure conventions | Platform team          | Platform review                     |
| Service            | Domain ownership           | Service owner          | Service architect                   |
| Project            | Framework usage            | Project maintainer     | Technical lead                      |
| Language           | C# standards               | Engineering enablement | Language guild                      |
| Temporary override | Migration exception        | Named approver         | Architecture/security as applicable |

---

## 16. Instruction Hierarchy, Inheritance, and Precedence

### Purpose

Define how multiple applicable Instructions are combined and how conflicts are resolved.

### 16.1 Instruction Hierarchy

Instructions may be arranged from broader authority to narrower applicability.

A typical hierarchy:

1. Organizational Governance Policies
2. Organization-wide Instructions
3. Repository-root Instructions
4. Directory Instructions
5. Service or project Instructions
6. Language or framework Instructions
7. Path-specific Instructions
8. Approved temporary override
9. Task Prompt and acceptance criteria

### 16.2 Inheritance

Lower scopes inherit broader Instructions unless:

* The broader Instruction explicitly limits inheritance
* A valid specialization exists
* An approved exception applies
* The Instruction is irrelevant to the lower scope

### 16.3 Specialization

A lower-level Instruction may provide more specific implementation guidance.

Broad Instruction:

> Services must use approved authentication mechanisms.

Service Instruction:

> Booking Service must validate JWT access tokens and enforce the `CorporateFleetBooking` authorization policy for corporate reservation endpoints.

### 16.4 Override

An override replaces or suspends broader behavior only when:

* Override authority is explicitly defined
* The requester is authorized
* Scope is narrow
* Rationale is documented
* Duration is limited where applicable
* Security and compliance controls remain protected
* The override is auditable

### 16.5 Precedence Questions

Address:

* What happens when organization and repository Instructions conflict?
* What happens when root and service Instructions conflict?
* Can a Prompt override a security Instruction?
* Can a Steering Note override a repository Instruction?
* Who approves exceptions?
* How should the agent react when authority is unclear?

### Required Precedence Principles

1. Higher-authority governance requirements cannot be weakened by lower scopes.
2. Narrower Instructions may specialize broader Instructions.
3. More recent content does not automatically have greater authority.
4. Prompts do not override persistent mandatory Instructions.
5. Steering Notes do not override mandatory Instructions unless they contain an authorized exception.
6. Security and compliance ambiguity must be escalated.
7. Material unresolved conflicts should block implementation.
8. Conflict and resolution evidence must be recorded.

### Conflict Example

Organization Instruction:

> Production workloads must use approved centralized secrets management.

Service Instruction:

> Store the external fleet provider API key in `appsettings.Production.json`.

Resolution:

* The service Instruction is invalid because it weakens an organization-level security requirement.
* The agent must not follow it.
* The conflict must be recorded and escalated.
* Implementation must use the approved secrets platform.

### Recommended Diagram

**Figure 6-5 — Instruction Hierarchy and Precedence**

Show:

* Authority axis
* Scope axis
* Specialization
* Valid override
* Invalid weakening
* Human escalation path

### Recommended Table

**Table 6-10 — Hierarchy and Precedence Examples**

| Conflict                                            | Resolution                                                               |
| --------------------------------------------------- | ------------------------------------------------------------------------ |
| Organization security vs repository convenience     | Organization security prevails                                           |
| Root general testing vs service-specific tests      | Apply both; service specializes                                          |
| Prompt asks to skip required tests                  | Instruction prevails                                                     |
| Steering Note changes implementation sequence       | Apply temporary direction if no mandatory conflict                       |
| Two service Instructions conflict                   | Determine owner, strength, and effective version; escalate if unresolved |
| Legacy local Instruction conflicts with current ADR | Review authority and version; do not guess                               |

---

## 17. Instruction Conflict Resolution Framework

### Purpose

Provide a systematic conflict-resolution process suitable for humans, agents, and harnesses.

### Conflict Resolution Process

1. Identify all applicable Instructions.
2. Determine each Instruction’s scope.
3. Determine each Instruction’s authority and owner.
4. Classify its strength.
5. Confirm version and freshness.
6. Check explicit inheritance and override rules.
7. Determine whether one Instruction specializes another.
8. Preserve mandatory security, compliance, and governance controls.
9. Record the conflict.
10. Apply a documented exception if one exists.
11. Request human resolution when ambiguity remains.
12. Prevent implementation when unresolved conflict creates material risk.
13. Record the final decision and supporting evidence.
14. Propose Instruction clarification where recurrence is likely.

### Conflict States

* Resolved by scope
* Resolved by authority
* Resolved by specialization
* Resolved by approved exception
* Resolved by deprecation
* Escalated for human decision
* Implementation blocked
* Instruction defect identified

### Alpha Car Detailing Application

Root Instruction:

> Shared contract changes must remain backward compatible.

Booking Service Instruction:

> Rename `CorporateAccountId` to `AccountId` in the existing event.

The change is breaking. The narrower Instruction cannot silently weaken the shared compatibility requirement. The agent must:

* Preserve the existing field
* Introduce a versioned event
* Propose a migration
* Or request formal approval for a breaking change

### Recommended Table

**Table 6-11 — Instruction Conflicts and Resolution**

| Conflict Type           | Example                             | Resolution Mechanism         |
| ----------------------- | ----------------------------------- | ---------------------------- |
| Scope conflict          | Root vs service                     | Specialization analysis      |
| Authority conflict      | Policy vs repository                | Higher authority             |
| Version conflict        | Current vs stale file               | Version and ownership review |
| Platform conflict       | CLAUDE.md vs Copilot file           | Align vendor-neutral source  |
| Prompt conflict         | Task asks to bypass control         | Reject or request exception  |
| Temporary conflict      | Steering Note vs Instruction        | Validate override authority  |
| Duplicate contradiction | Two files state different standards | Consolidate and govern       |

---

## 18. Instruction Lifecycle

### Purpose

Define the complete lifecycle from repository discovery through retirement.

### Structured Instruction Lifecycle

#### 18.1 Discover the Repository and Engineering Context

Use Repository Discovery to understand:

* Architecture
* Service boundaries
* Dependencies
* Existing tests
* Delivery workflows
* Security mechanisms
* Operational conventions
* Existing Instruction sources

#### 18.2 Identify Behaviors Requiring Explicit Instructions

Prioritize behaviors that are:

* Security-sensitive
* Architecturally significant
* Frequently violated
* Difficult to infer
* Inconsistent across the repository
* Required for compliance
* Important for reliability
* Necessary for code review
* Repeated across tasks

#### 18.3 Define Scope

Determine whether the Instruction belongs at:

* Organization
* Repository
* Directory
* Service
* Project
* Language
* Framework
* Task-specific override level

#### 18.4 Assign Ownership

Identify:

* Author
* Technical owner
* Required approver
* Reviewers
* Exception authority

#### 18.5 Author Clear and Testable Guidance

Define:

* Required behavior
* Prohibited behavior
* Strength
* Rationale
* Validation
* Examples
* Exceptions

#### 18.6 Organize According to Hierarchy

Place the Instruction where applicable agents can discover it without unnecessary duplication.

#### 18.7 Resolve Overlap and Precedence

Check for:

* Duplicated content
* Contradictions
* Invalid weakening
* Platform divergence
* Missing override rules

#### 18.8 Validate Against Repository State

Confirm that the Instruction:

* Reflects current architecture
* Is technically achievable
* Aligns with tests and CI/CD
* Does not preserve obsolete design unintentionally

#### 18.9 Execute Tasks Using the Effective Instruction Set

The agent determines and applies all Instructions relevant to:

* Current path
* Current project
* Current technology
* Current task
* Current organizational authority

#### 18.10 Verify Compliance

Use:

* Tests
* Static analysis
* Architecture checks
* Security scans
* Review
* Harness evaluators
* CI/CD gates

#### 18.11 Record Conflicts, Exceptions, and Violations

Maintain traceability for:

* Unresolved conflicts
* Approved exceptions
* Violations
* Unverifiable guidance
* Suggested improvements

#### 18.12 Review and Version Instructions

Review on:

* Scheduled cadence
* Architecture changes
* Security incidents
* Major migrations
* Repeated violations
* Platform changes

#### 18.13 Propose Improvements Through Governance

Agents and harnesses may recommend:

* Clarification
* Consolidation
* New Instructions
* Revised scope
* Additional validation
* Retirement

#### 18.14 Retire or Replace Obsolete Instructions

Deprecation must include:

* Replacement guidance
* Effective date
* Migration implications
* Removal plan
* Change history

### Recommended Diagram

**Figure 6-6 — Discovery-to-Instruction Lifecycle**

```text
Discover
  ↓
Identify
  ↓
Scope
  ↓
Own
  ↓
Author
  ↓
Organize
  ↓
Validate
  ↓
Apply
  ↓
Verify
  ↓
Record
  ↓
Review
  ↓
Improve or Retire
```

---

## 19. Instruction Discovery and Composition

### Purpose

Explain how an agent or harness discovers applicable Instruction sources and builds an effective Instruction set.

### Important Subsections

#### 19.1 Discovery Sources

Possible sources include:

* Repository-root Instruction files
* Directory-level files
* Service documentation
* Platform-specific Instruction files
* Organization-provided guidance
* Agent execution environment
* Approved temporary overrides
* Related architecture decisions

#### 19.2 Discovery Order

A platform or harness should:

1. Locate organization-level guidance.
2. Locate root repository Instructions.
3. Traverse applicable directory scopes.
4. Load project and service Instructions.
5. Load language and framework-specific Instructions.
6. Load approved temporary overrides.
7. Identify related governance policies.
8. Detect conflicts before execution.

#### 19.3 Composition

The effective Instruction set should be:

* Deduplicated
* Ordered by authority and scope
* Annotated with source
* Classified by strength
* Checked for conflict
* Bound to current task paths
* Preserved as execution evidence

#### 19.4 Instruction Manifest

Introduce the idea of a generated manifest:

```yaml
task_id: ACD-CORP-BOOKING-142
applicable_instructions:
  - id: ORG-SEC-002
    source: organization/security.md
    strength: mandatory
  - id: REP-ARCH-004
    source: CLAUDE.md
    strength: mandatory
  - id: BOOK-EVT-003
    source: services/booking/CLAUDE.md
    strength: required-unless-exempted
conflicts: []
exceptions: []
```

### Alpha Car Detailing Application

For a change under `services/booking`, the effective set may include:

* Organization security Instructions
* Root architecture Instructions
* Booking Service Instructions
* .NET Instructions
* Testing Instructions
* Event contract Instructions
* Current approved Steering Note
* Corporate booking task Prompt

### Recommended Diagram

**Figure 6-7 — Instruction Application Flow**

```text
Task and Changed Paths
          ↓
Discover Sources
          ↓
Resolve Scope
          ↓
Apply Hierarchy
          ↓
Detect Conflicts
          ↓
Build Effective Set
          ↓
Inject into Agent Roles
          ↓
Validate Output
```

---

## 20. Platform-Specific Instruction Files

### Purpose

Introduce common platform files without turning the chapter into a product syntax guide.

### Engineering Principle

The file format is secondary. The important questions are:

* What guidance is persistent?
* Where does it apply?
* How is it discovered?
* How is precedence determined?
* How is conflict handled?
* How is compliance verified?
* Who governs changes?

### Recommended Examples

* Root `CLAUDE.md`
* Root `AGENTS.md`
* GitHub Copilot repository Instructions
* Path-specific Copilot Instructions
* Service-level Instruction file
* .NET-specific Instruction file
* Testing Instruction file
* Infrastructure Instruction file
* Security Instruction file

### Canonical Source Strategy

Recommend maintaining vendor-neutral engineering guidance as the authoritative source and adapting it into platform-specific representations where required.

Avoid:

* Different architecture standards for different agents
* Copying large blocks into every platform file
* Allowing platform-specific guidance to silently diverge

### Recommended Table

**Table 6-12 — Platform-Specific Instruction File Comparison**

| Concern                    | Claude Code              | GitHub Copilot                   | OpenAI Codex                                  |
| -------------------------- | ------------------------ | -------------------------------- | --------------------------------------------- |
| Common repository guidance | `CLAUDE.md`              | Repository Instructions          | `AGENTS.md`-style guidance                    |
| Path-sensitive guidance    | Directory-scoped context | Path-specific Instructions       | Repository/directory guidance where supported |
| Task-specific requests     | User Prompt or command   | Chat/agent Prompt or prompt file | Task Prompt                                   |
| Reusable procedures        | Skills/commands          | Prompt files or team workflows   | Agent workflows and repository procedures     |
| Execution validation       | Commands, hooks, harness | Agent workflow and CI            | Command execution and validation              |
| Governance need            | High                     | High                             | High                                          |

---

## 21. Instruction Authoring

### Purpose

Provide practical guidance for writing Instructions that produce reliable agent behavior.

### Authoring Principles

#### 21.1 Write Observable Behavior

Prefer:

> Run `dotnet test` for all affected test projects before completion.

Avoid:

> Ensure the code is high quality.

#### 21.2 Define Scope Explicitly

Prefer:

> Applies to all ASP.NET Core API projects under `services/`.

Avoid:

> APIs should follow this standard.

#### 21.3 Separate Mandatory and Recommended Guidance

Do not mix:

* Required behavior
* Suggestions
* Background
* Examples

without signaling their authority.

#### 21.4 Include Rationale for Non-Obvious Constraints

Example:

> Booking Service must not query the Fleet database because Fleet Service owns vehicle eligibility and fleet status transitions.

#### 21.5 State Prohibitions Directly

Example:

> Do not place secrets, access keys, connection passwords, or tokens in source-controlled files.

#### 21.6 Define Exceptions

An Instruction should state:

* Whether exceptions are allowed
* Who approves them
* How they are recorded
* How long they remain valid

#### 21.7 Provide Validation

Example:

> Validation: architecture test `ServicesMustNotReferenceOtherServiceInfrastructure`.

#### 21.8 Avoid Over-Specification

Instructions should not dictate unnecessary details that:

* Reduce valid design choices
* Preserve obsolete implementations
* Prevent improvement
* Couple guidance to one library without justification

### Recommended Callout

> **Architect’s Note**
> Instructions should constrain engineering risk, not eliminate engineering judgment.

---

## 22. Architecture Instructions

### Purpose

Define Instructions that preserve architectural structure and ownership.

### Important Subsections

#### 22.1 Dependency Direction

* Domain does not reference Infrastructure.
* Application coordinates use cases.
* Adapters implement ports.
* API layers do not own domain behavior.

#### 22.2 Domain Ownership

* Each domain concept has an authoritative owner.
* Shared models must not become uncontrolled integration dependencies.
* Cross-service data access must use approved contracts.

#### 22.3 Service Boundaries

* No direct cross-service database access.
* No shared writable database ownership.
* No bypass of published service contracts.

#### 22.4 Contract Boundaries

* Public APIs and events are versioned.
* Breaking changes require migration planning and approval.

#### 22.5 Dependency Approval

* New production dependencies require:

  * License review
  * Security review
  * Maintenance assessment
  * Architecture justification where material

### Alpha Car Detailing Application

Corporate fleet booking logic belongs in Booking Service, while:

* Corporate Account Service owns account validity and entitlements.
* Fleet Service owns vehicle and fleet relationships.
* Station Operations Service owns station capacity.
* Booking Service coordinates the reservation workflow without taking ownership of those domains.

### Recommended Diagram

**Figure 6-8 — Alpha Car Detailing Instruction Map**

Show:

* Domain owners
* Permitted API/event interactions
* Prohibited database access
* Shared event contract governance
* Applicable Instruction scopes

---

## 23. Coding Standard Instructions

### Purpose

Define implementation-level consistency without turning Instructions into a complete language reference.

### Key Concepts

* Naming and structure
* Nullability
* Asynchronous I/O
* Cancellation
* Dependency injection
* Immutability
* Logging
* Mapping
* Error handling
* Generated code boundaries

### .NET Examples

* Enable nullable reference types.
* Pass `CancellationToken` through asynchronous boundaries.
* Do not use `.Result` or `.Wait()` on asynchronous calls.
* Avoid service locator patterns.
* Keep controllers thin.
* Prefer domain-specific value objects for constrained values.
* Do not catch exceptions only to log and rethrow without added context.
* Do not modify generated code directly.

### Alpha Car Detailing Application

The new booking endpoint:

* Delegates use-case execution to Application
* Uses policy-based authorization
* Returns typed results mapped to Problem Details
* Passes cancellation tokens
* Avoids business decisions in the controller

---

## 24. Testing Instructions

### Purpose

Define minimum evidence required for safe changes.

### Important Subsections

#### 24.1 Unit Tests

Required for:

* Domain decisions
* Application rules
* Validation
* Mapping with business significance

#### 24.2 Integration Tests

Required for:

* Database behavior
* External adapters
* API middleware
* Authorization
* Event publishing
* Cache interaction

#### 24.3 Contract Tests

Required for:

* Public APIs
* Shared events
* Consumer compatibility
* Schema evolution

#### 24.4 Architecture Tests

Required for:

* Dependency direction
* Layer isolation
* Forbidden references
* Naming and structural constraints

#### 24.5 Security Tests

Required for:

* Authorization policies
* Tenant isolation
* Input validation
* Sensitive data exposure

#### 24.6 Test Evidence

Pull requests must identify:

* Tests added
* Tests executed
* Results
* Exclusions
* Approved exceptions

### Alpha Car Detailing Application

Required test set:

* Corporate account eligibility unit tests
* Booking conflict unit tests
* Authorization integration tests
* Outbox persistence integration tests
* Event schema compatibility tests
* Idempotent consumer replay tests
* Problem Details response tests

### Recommended Table

**Table 6-13 — Instruction Compliance Validation Methods**

| Instruction                      | Validation                                  |
| -------------------------------- | ------------------------------------------- |
| Thin controllers                 | Architecture test and review                |
| No cross-service database access | Dependency checks and review                |
| RFC 7807 responses               | Integration tests                           |
| Outbox required                  | Integration test and persistence inspection |
| Event versioning                 | Schema validation and contract tests        |
| Authorization required           | Security integration tests                  |
| Correlation propagation          | Trace integration test                      |
| No secrets in source             | Secret scanning                             |
| Approved dependencies only       | Dependency policy gate                      |
| PR evidence required             | Pull-request template and CI check          |

---

## 25. Security Instructions

### Purpose

Define non-negotiable security behavior for AI-generated and human-authored changes.

### Important Subsections

#### 25.1 Authentication

* Use approved authentication middleware.
* Do not create custom token validation without approval.

#### 25.2 Authorization

* Enforce authorization at server-side trust boundaries.
* Use policy-based authorization.
* Do not trust role, tenant, or account values supplied only by the client.

#### 25.3 Secrets

* Store secrets in approved secrets-management systems.
* Do not commit secrets.
* Do not print secrets in logs.
* Do not generate example secrets that resemble production credentials.

#### 25.4 Input Validation

* Validate untrusted input.
* Apply length, format, and domain constraints.
* Prevent injection and unsafe deserialization.

#### 25.5 Sensitive Data

* Minimize data exposure.
* Avoid logging personal, financial, or sensitive identifiers.
* Apply masking where required.

#### 25.6 Dependency Security

* Evaluate vulnerabilities and licenses.
* Do not bypass vulnerability scanning.
* Require approval for risk acceptance.

### Alpha Car Detailing Application

Corporate booking endpoints must:

* Require authenticated corporate users
* Apply the `CorporateFleetBooking` policy
* Validate corporate account ownership
* Avoid trusting corporate account IDs from request payloads
* Avoid logging access tokens or sensitive fleet identifiers

### Recommended Callout

> **Common Mistake**
> An agent should not interpret “make the endpoint work” as permission to reduce authentication, authorization, or secrets-management requirements.

---

## 26. Dependency Instructions

### Purpose

Control third-party libraries, internal packages, SDKs, and service dependencies.

### Key Concepts

* Approved dependency lists
* Version constraints
* License requirements
* Vulnerability thresholds
* Maintenance status
* Duplication avoidance
* Dependency introduction approval
* Internal package ownership

### Alpha Car Detailing Application

The agent proposes a new resilience library despite the organization already using a standard package. Instructions require the agent to:

* Use the approved library
* Justify any alternative
* Obtain architecture approval before introducing another production dependency

---

## 27. API and Contract Instructions

### Purpose

Define stable and governable API behavior.

### Important Subsections

* API versioning
* Backward compatibility
* RFC 7807
* Validation responses
* Authentication and authorization
* Idempotency keys
* Pagination
* Correlation identifiers
* OpenAPI updates
* Deprecation
* Consumer communication

### Alpha Car Detailing Application

The fleet booking endpoint must:

* Use an approved versioned route
* Require authorization
* Return Problem Details
* Accept an idempotency key for retry-safe booking submission
* Update the OpenAPI definition
* Include integration tests

---

## 28. Event-Driven Architecture Instructions

### Purpose

Define reliable event production and consumption behavior.

### Important Subsections

#### 28.1 Event Ownership

The service that owns the business state owns the event.

#### 28.2 Schema Versioning

Events require:

* Schema identifier
* Version
* Compatibility rules
* Change review

#### 28.3 Transactional Outbox

Business state and event intent must be committed atomically where required.

#### 28.4 Idempotent Consumers

Consumers must:

* Detect duplicates
* Record processing state
* Support replay
* Define poison-message behavior
* Avoid unsafe repeated side effects

#### 28.5 Correlation and Causation

Events should preserve:

* Event identifier
* Correlation identifier
* Causation identifier where applicable
* Source
* Timestamp
* Schema information

#### 28.6 Failure Handling

Instructions should define:

* Retry boundaries
* Dead-letter or poison handling
* Alerting
* Replay authorization
* Checkpoint behavior

### Alpha Car Detailing Application

The `CorporateFleetBookingCreated` event requires:

* Versioned schema
* Transactional outbox
* Correlation identifier
* Stable event identifier
* Consumer idempotency
* Compatibility tests
* No direct synchronous dependency on all downstream services after commit

---

## 29. Database and Persistence Instructions

### Purpose

Protect data ownership, consistency, migration safety, and operational integrity.

### Important Subsections

* Database ownership
* Migration naming
* Migration review
* Backward-compatible rollout
* Index review
* Transaction boundaries
* Concurrency handling
* Query performance
* Data retention
* Auditing
* Soft deletion
* Rollback requirements

### Prohibited Actions

* Direct cross-service database access
* Manual production schema changes
* Destructive migration without approval
* Embedding credentials in connection strings
* Unreviewed data backfills

### Alpha Car Detailing Application

Booking Service stores booking state in its own database. It must not join directly against:

* Corporate Account database
* Fleet database
* Station Operations database

Required data is obtained through:

* APIs
* Events
* Approved replicated read models

---

## 30. Observability Instructions

### Purpose

Ensure that generated changes are diagnosable and operable.

### Key Concepts

* Structured logging
* Correlation propagation
* Distributed tracing
* Metrics
* Health checks
* Error classification
* Sensitive-data handling
* Service-level indicators
* Operational dashboards

### Alpha Car Detailing Application

The corporate booking flow must include:

* Trace spans across gateway and services
* Correlation identifier propagation
* Booking success and failure metrics
* Outbox backlog metrics
* Consumer retry and poison-message metrics
* No sensitive JWT or customer data in logs

---

## 31. Error-Handling Instructions

### Purpose

Define predictable and secure failure behavior.

### Key Concepts

* Expected domain errors
* Client validation errors
* Authorization failures
* Unexpected failures
* Problem Details
* Internal exception logging
* Correlation identifiers
* Retryable versus non-retryable failures
* Event-processing errors

### Alpha Car Detailing Application

Examples:

* Invalid station capacity request → domain Problem Details
* Unauthorized corporate account → authorization failure
* Duplicate booking submission → idempotent response
* Unexpected database failure → generic error response plus correlated internal log

---

## 32. Configuration and Secrets Instructions

### Purpose

Separate safe configuration from sensitive material and environment-specific deployment values.

### Key Concepts

* Environment variables
* Approved configuration providers
* Secrets vaults
* Workload identity
* Local development secrets
* Configuration validation
* Safe defaults
* Source-control restrictions

### Alpha Car Detailing Application

The external fleet provider credential must:

* Reside in the approved secrets store
* Be accessed through workload identity
* Never appear in repository files
* Never appear in test snapshots
* Never be emitted in diagnostics

---

## 33. CI/CD Instructions

### Purpose

Define delivery requirements that agents must preserve.

### Important Subsections

* Required build commands
* Test gates
* Security scans
* Dependency scans
* Infrastructure validation
* Container scans
* Migration checks
* Artifact signing
* Deployment approval
* Rollback evidence
* Branch and pull-request requirements

### Alpha Car Detailing Application

A corporate booking pull request must pass:

* Build
* Unit tests
* Integration tests
* Contract tests
* Architecture tests
* SAST
* Secret scanning
* Dependency checks
* Container scanning
* Infrastructure validation where manifests change

---

## 34. Documentation Instructions

### Purpose

Define documentation obligations for code, contracts, architecture, and operations.

### Required Documentation Areas

* Public APIs
* Event schemas
* ADRs
* Configuration
* Operational runbooks
* Migrations
* Deployment changes
* Security implications
* Breaking changes
* New dependencies

### Alpha Car Detailing Application

The feature must update:

* API documentation
* Event schema documentation
* Booking workflow diagram
* Operational dashboard notes
* Migration documentation where persistence changes

---

## 35. Pull-Request and Review Instructions

### Purpose

Define the evidence required for human review.

### Pull-Request Evidence

* Change summary
* Business rationale
* Affected services
* Applicable Instructions
* Tests added
* Tests executed
* Architecture impact
* Security impact
* Data migration impact
* Event/API compatibility
* Observability changes
* Known exceptions
* Rollback approach

### Human Review Requirements

Certain changes require named reviewers:

* Shared contracts → contract owner
* Security controls → security reviewer
* New production dependencies → architecture reviewer
* Database migration → service owner or DBA
* Infrastructure changes → platform reviewer

### Alpha Car Detailing Application

The corporate fleet booking PR must provide evidence that:

* No cross-service database access was introduced
* Authorization is enforced
* Event schema is versioned
* Outbox behavior is tested
* Consumer idempotency is verified
* Telemetry is present
* No secrets were introduced

---

## 36. Prohibited Actions and Human Approval Requirements

### Purpose

Define behaviors the agent must not perform autonomously.

### Prohibited Actions

* Commit secrets
* Disable security controls
* Bypass authorization
* Access another service’s database
* Remove tests to obtain a passing build
* Suppress critical security findings without approval
* Modify generated files directly
* Publish breaking contracts silently
* Add unapproved dependencies
* Alter production data
* Change enterprise Instructions silently

### Approval Requirements

Human approval may be required for:

* Breaking contract changes
* Security exceptions
* New infrastructure resources
* Production database migrations
* Risk acceptance
* Dependency introduction
* Instruction changes
* Temporary architecture deviations
* Deletion of production-facing capabilities

### Agent Behavior

When approval is missing, the agent should:

1. Stop the affected action.
2. Explain the requirement.
3. Produce a proposed change or exception request.
4. Continue only with non-conflicting work where safe.
5. Record the unresolved approval dependency.

---

## 37. Instructions for Generated Code

### Purpose

Address risks specific to AI-generated implementations.

### Key Concepts

* Generated code is subject to the same standards as human-authored code.
* AI attribution does not reduce review requirements.
* Agents must not introduce placeholders as completed production code.
* Generated tests must test behavior, not merely mirror implementation.
* Generated code should avoid speculative abstractions.
* Generated code must use approved libraries and patterns.
* Agents must clearly identify unverifiable assumptions.

### Alpha Car Detailing Application

The agent may generate handlers, validators, tests, and adapters, but must:

* Follow service boundaries
* Include meaningful failure cases
* Avoid hard-coded secrets
* Validate authorization behavior
* Run required checks
* Report assumptions

---

## 38. Instructions for Legacy Code

### Purpose

Explain how Instructions apply when the current repository does not fully conform to the desired architecture.

### Key Concepts

* Do not expand legacy patterns into new code.
* Preserve compatibility where required.
* Improve code incrementally within approved scope.
* Do not perform broad refactoring without authorization.
* Distinguish current standard from tolerated legacy behavior.
* Document modernization boundaries.
* Use exception metadata where old behavior remains temporarily valid.

### Alpha Car Detailing Application

A legacy Booking controller contains direct SQL and business logic. Instructions state:

* Existing behavior may remain until migration.
* New corporate booking behavior must use Application handlers and repositories.
* Do not copy the legacy controller pattern.
* Avoid unrelated large-scale migration in the feature PR.

### Recommended Callout

> **Enterprise Tip**
> A good legacy Instruction prevents both uncontrolled modernization and uncontrolled replication of obsolete patterns.

---

## 39. Instructions for Monorepositories

### Purpose

Address scale, scope, inheritance, and discoverability in large repositories.

### Key Concepts

* Root guidance should remain concise.
* Domain-specific guidance should live near applicable code.
* Shared requirements should not be copied into every service.
* Path-specific Instructions should specialize, not contradict.
* Platform files should remain synchronized with canonical guidance.
* The harness should produce an effective Instruction set for changed paths.

### Alpha Car Detailing Application

The monorepository may contain:

* Services
* Shared contracts
* Infrastructure
* Tests
* Documentation
* Harness assets
* Frontend applications

Each area requires specialized Instructions while inheriting the root architecture and security baseline.

### Recommended Diagram

**Figure 6-9 — Monorepository Instruction Resolution**

Show:

* Root Instructions
* Service subtree Instructions
* Infrastructure subtree Instructions
* Test subtree Instructions
* Effective set by changed path

---

## 40. Instructions for Multi-Repository Systems

### Purpose

Explain how organizations maintain consistency across independently versioned repositories.

### Key Concepts

* Organization-wide canonical Instructions
* Repository-specific specialization
* Shared templates
* Versioned Instruction packages
* Policy distribution
* Repository onboarding checks
* Cross-repository contract governance
* Drift reporting
* Platform-specific synchronization

### Alpha Car Detailing Application

If each service has its own repository:

* Organization security Instructions apply everywhere.
* Shared event contract Instructions are centrally governed.
* Each repository specializes service ownership and build commands.
* A central harness verifies minimum Instruction coverage.

---

## 41. Instruction Ownership and Governance

### Purpose

Define the human accountability model for Instruction management.

### Governance Roles

#### 41.1 Instruction Author

Drafts or updates guidance.

#### 41.2 Instruction Owner

Accountable for correctness and ongoing maintenance.

#### 41.3 Technical Reviewer

Confirms technical accuracy.

#### 41.4 Architecture Approver

Approves architectural scope or exceptions.

#### 41.5 Security Approver

Approves security-sensitive changes or exceptions.

#### 41.6 Compliance Reviewer

Confirms regulated obligations where applicable.

#### 41.7 Repository Maintainer

Ensures discoverability and integration.

#### 41.8 Harness Maintainer

Implements loading, validation, reporting, and evidence.

### Governance Activities

* Version control
* Pull-request review
* Change history
* Scheduled review
* Architecture review
* Security review
* Compliance review
* Exception approval
* Deprecation
* Rollback
* Audit trails

### Governance Principle

> AI agents and self-learning harnesses must not silently modify enterprise Instructions.

They may:

* Identify ambiguity
* Detect conflicts
* Detect repeated violations
* Recommend clearer wording
* Propose new Instructions
* Propose removal of obsolete Instructions
* Produce evidence supporting a proposal

Human approval remains mandatory.

### Recommended Diagram

**Figure 6-10 — Instruction Governance Lifecycle**

```text
Proposal
   ↓
Technical Review
   ↓
Architecture / Security Review
   ↓
Approval
   ↓
Versioned Publication
   ↓
Enforcement
   ↓
Effectiveness Review
   ↓
Revision or Retirement
```

---

## 42. Instruction Versioning and Change Management

### Purpose

Treat Instruction changes as controlled engineering changes.

### Key Concepts

* Git version control
* Semantic change description
* Effective date
* Compatibility impact
* Migration period
* Deprecation marker
* Rollback
* Change communication
* Affected repository inventory

### Change Types

* Clarification
* Scope expansion
* Scope reduction
* Strength increase
* Strength reduction
* New prohibition
* New approval requirement
* Validation change
* Platform representation change
* Deprecation
* Replacement

### High-Risk Changes

Require enhanced review when they:

* Weaken security
* Reduce testing
* Permit new dependencies
* Change service boundaries
* Change data ownership
* Alter contract compatibility
* Modify approval authority
* Remove compliance controls

### Alpha Car Detailing Application

Changing the outbox requirement from Mandatory to Recommended would be a material architecture and reliability change requiring architecture approval and impact review.

---

## 43. Instruction Review

### Purpose

Define when and how Instructions should be reassessed.

### Review Triggers

* Scheduled review date
* Architecture migration
* New platform adoption
* Security incident
* Repeated violations
* Frequent exception requests
* New compliance obligation
* Repository restructure
* Tooling or framework upgrade
* Contradictory implementation patterns
* Failed harness evaluation

### Review Questions

* Is the Instruction still accurate?
* Is its scope correct?
* Is its strength appropriate?
* Is it discoverable?
* Can it be verified?
* Is ownership current?
* Is it duplicated?
* Does it conflict with current policy?
* Is the rationale still valid?
* Should it be retired?

---

## 44. Instruction Validation and Testing

### Purpose

Explain how declared Instructions become verified engineering controls.

### Validation Mechanisms

#### 44.1 Static Analysis

Examples:

* Forbidden API usage
* Naming
* Nullability
* Secret patterns
* Dependency direction

#### 44.2 Architecture Tests

Examples:

* Layer references
* Service isolation
* Namespace boundaries
* Contract ownership

#### 44.3 Unit Tests

Examples:

* Domain policies
* Validation behavior
* Error mapping

#### 44.4 Integration Tests

Examples:

* Database behavior
* Authorization
* Middleware
* Outbox persistence

#### 44.5 Contract Tests

Examples:

* API compatibility
* Event schemas
* Consumer compatibility

#### 44.6 Security Scanning

Examples:

* SAST
* Secret detection
* Dependency vulnerabilities
* Container scanning
* Infrastructure scanning

#### 44.7 Dependency Checks

Examples:

* Approved package list
* License policy
* Version policy
* Vulnerability threshold

#### 44.8 CI/CD Gates

Examples:

* Required test suites
* Quality gates
* Infrastructure validation
* Approval gates

#### 44.9 Pull-Request Templates

Require evidence that cannot be inferred automatically.

#### 44.10 Reviewer Checklists

Validate architecture and business judgment.

#### 44.11 Harness Evaluators

Compare generated output against:

* Effective Instruction set
* Changed paths
* Test evidence
* Architecture constraints
* Security controls
* Prohibited actions

#### 44.12 Compliance Reports

Provide auditable evidence of:

* Applicable Instructions
* Validation methods
* Results
* Exceptions
* Violations
* Human approvals

### Machine-Enforceable Versus Judgment-Based Instructions

Machine-enforceable:

* No project reference from Domain to Infrastructure
* No secrets matching known patterns
* Required tests executed
* Event schema includes version
* Disallowed dependency absent

Judgment-based:

* Domain boundaries are appropriately modeled
* Abstraction complexity is justified
* Service ownership remains coherent
* Error messages are operationally useful
* The change introduces unnecessary coupling

### Required Instruction States

#### Declared Instruction

The Instruction exists and is published.

#### Applied Instruction

The Instruction was included in the task’s effective Instruction set.

#### Verified Instruction

Compliance was confirmed.

#### Violated Instruction

Evidence shows non-compliance.

#### Exception-Approved Instruction

A valid approved exception applies.

#### Unverifiable Instruction

The Instruction cannot currently be verified with available automation or evidence.

### Recommended Diagram

**Figure 6-11 — Instruction State Model**

```text
Declared
   ↓
Applied
   ↓
Verified
   ├── Violated
   ├── Exception-Approved
   └── Unverifiable
```

---

## 45. Instruction Compliance Evidence

### Purpose

Define the records required to prove that Instructions were applied.

### Evidence Types

* Effective Instruction manifest
* Test results
* Static analysis reports
* Architecture test results
* Security scan reports
* Dependency reports
* Contract compatibility reports
* Pull-request checklist
* Human approvals
* Exception records
* Harness evaluation
* Agent execution log
* Changed-path analysis
* Traceability links

### Compliance Record Example

```yaml
instruction_id: REP-EVT-003
instruction: Business events must use the transactional outbox
status: verified
evidence:
  - test: BookingOutboxPersistenceTests
  - file: services/booking/infrastructure/BookingDbContext.cs
  - ci_job: integration-tests
reviewer: booking-service-owner
```

### Alpha Car Detailing Application

The corporate booking PR should show:

* Outbox integration test result
* Event schema compatibility result
* Authorization integration test result
* Secret scan result
* Dependency approval status
* Observability checklist
* Architecture review evidence

---

## 46. Instruction Drift

### Purpose

Explain how Instructions become misaligned with repositories, platforms, and organizational policy.

### Drift Patterns

* Code evolves but Instructions do not.
* Instructions describe an architecture no longer used.
* Multiple files contain duplicated guidance.
* Local exceptions become undocumented standards.
* Platform-specific files diverge.
* Repository Instructions conflict with organizational policy.
* Tests enforce behavior no longer described.
* Documentation and Instructions disagree.
* Ownership changes but metadata does not.
* Temporary overrides never expire.
* Deprecated tools remain required.

### Drift Detection Strategies

* Compare Instructions to repository structure.
* Compare Instructions to dependency graphs.
* Compare declared validation commands to CI/CD.
* Detect referenced files or projects that no longer exist.
* Detect duplicated or semantically conflicting text.
* Compare CLAUDE.md, Copilot Instructions, and AGENTS.md.
* Track exception frequency.
* Track violation frequency.
* Review stale ownership and dates.
* Compare architecture tests with declared standards.

### Drift Reduction Strategies

* Canonical source for shared guidance
* Scheduled reviews
* Automated linting
* Ownership metadata
* Expiry dates
* Change impact analysis
* Harness-generated drift reports
* Consolidation of duplicated guidance
* Explicit deprecation
* PR checks when architecture changes

### Alpha Car Detailing Application

Instruction:

> All services use Redis for distributed caching.

Repository reality:

* Booking Service has migrated to a different approved cache abstraction.
* The Instruction is now too implementation-specific.

The harness should identify the mismatch and propose updated wording rather than silently changing the Instruction.

### Recommended Table

**Table 6-14 — Drift Symptoms and Mitigation**

| Drift Symptom                             | Risk                        | Mitigation                       |
| ----------------------------------------- | --------------------------- | -------------------------------- |
| Instruction references deleted project    | Agent failure               | Repository validation            |
| Different platform files disagree         | Inconsistent agent behavior | Canonical source and sync checks |
| Repeated approved exceptions              | Standard no longer fits     | Governance review                |
| Tests enforce undocumented behavior       | Hidden standards            | Create or update Instruction     |
| Instruction describes legacy architecture | Invalid generated code      | Deprecate and replace            |
| Temporary override has no expiry          | Permanent shadow standard   | Expiry validation                |

---

## 47. Stale and Contradictory Instructions

### Purpose

Provide agent behavior for obsolete or inconsistent guidance.

### Required Agent Behavior

When an Instruction appears stale or contradictory, the agent should:

1. Identify the source and scope.
2. Check version, owner, and review date.
3. Compare with repository state and architecture decisions.
4. Determine whether a newer authoritative Instruction exists.
5. Avoid guessing when material risk exists.
6. Record the ambiguity.
7. Request human resolution.
8. Continue only with unaffected work.
9. Propose clarification or retirement.

### Material Risk Examples

Implementation should be blocked when ambiguity affects:

* Authentication
* Authorization
* Secrets
* Data ownership
* Financial transactions
* Contract compatibility
* Production migrations
* Regulatory controls
* Destructive actions

---

## 48. Measuring Instruction Effectiveness

### Purpose

Define meaningful measures without reducing Instructions to simplistic metrics.

### Effectiveness Indicators

* Compliance rate
* Violation frequency
* Rework caused by missed Instructions
* Review comments tied to known Instructions
* Build and test failures
* Architecture defect rate
* Security defect rate
* Exception frequency
* Conflict frequency
* Time required to resolve ambiguity
* Agent success rate
* Percentage of Instructions with validation
* Percentage with current owners
* Percentage reviewed on schedule
* Drift findings
* Duplicate guidance findings

### Interpretation Guidance

A high violation rate may indicate:

* Poor agent performance
* Weak discoverability
* Ambiguous wording
* Invalid scope
* Missing enforcement
* Unrealistic requirements
* Repository drift

Metrics should support diagnosis, not automatic weakening of standards.

### Alpha Car Detailing Application

Track whether corporate booking changes repeatedly fail due to:

* Missing outbox usage
* Incorrect service ownership
* Missing authorization
* Incomplete tests
* Unversioned events

Repeated failures may justify:

* Clearer Instructions
* Better Skills
* Stronger architecture tests
* Improved harness injection
* Better examples

---

## 49. Instructions Inside an Enterprise AI Harness

### Purpose

Explain how a harness operationalizes Instruction discovery, application, validation, and governance.

### Harness Capabilities

#### 49.1 Instruction Loading

* Discover relevant sources
* Load platform and vendor-neutral guidance
* Capture versions and locations

#### 49.2 Instruction Interpretation

* Classify category
* Determine strength
* Resolve scope
* Extract validation requirements

#### 49.3 Instruction Enforcement

* Prevent prohibited actions
* Restrict commands
* Require approvals
* Apply execution policies

#### 49.4 Instruction Validation

* Run tests and checks
* Compare implementation to standards
* Gather evidence
* Identify unverifiable requirements

#### 49.5 Instruction Governance

* Record violations
* Record exceptions
* Produce change proposals
* Preserve audit trails
* Require human approval

### Harness Workflow

```text
Task Received
     ↓
Repository Discovery
     ↓
Instruction Discovery
     ↓
Effective Instruction Set
     ↓
Role Prompt Composition
     ↓
Implementation
     ↓
Validation
     ↓
Compliance Report
     ↓
Human Review
```

### Role Integration

#### Lead Agent

* Identifies applicable Instructions
* Creates the implementation plan
* Detects conflicts
* Requests approvals

#### Developer Agent

* Implements within applicable constraints
* Records assumptions
* Produces tests

#### Reviewer Agent

* Reviews architecture, security, and maintainability
* Checks Instruction compliance

#### Validator Agent

* Runs machine-enforceable checks
* Collects evidence

#### Evaluator Agent

* Assesses quality, completeness, and remaining risk
* Identifies Instruction ambiguity

### Alpha Car Detailing Application

The harness should prevent completion if:

* The event is unversioned
* The outbox is omitted
* Authorization tests are missing
* A secret appears in source
* Another service database is referenced
* A mandatory Instruction remains unresolved

### Recommended Diagram

**Figure 6-12 — Instruction Enforcement Inside the Harness**

Show:

* Instruction registry
* Resolver
* Conflict detector
* Role Prompt composer
* Policy gate
* Validation engine
* Evidence store
* Human approval gate

---

## 50. Instruction Change Proposals

### Purpose

Define how agents and harnesses recommend improvements without modifying standards autonomously.

### Proposal Contents

* Current Instruction
* Observed issue
* Evidence
* Affected scope
* Suggested wording
* Reason for change
* Risk analysis
* Validation impact
* Migration impact
* Required reviewers
* Suggested effective date

### Proposal Types

* Clarification
* Consolidation
* New Instruction
* Scope adjustment
* Strength adjustment
* Additional example
* New validation
* Deprecation
* Retirement
* Platform alignment

### Alpha Car Detailing Application

The harness detects repeated confusion around correlation identifiers. It proposes:

> All inbound HTTP and event-processing flows must establish or propagate a correlation identifier. The identifier must be included in structured logs, trace context, outgoing events, and Problem Details extensions where safe.

The proposal is reviewed by architecture and observability owners before publication.

---

## 51. Self-Learning Harness Recommendations

### Purpose

Explain safe adaptive behavior.

### Allowed Behaviors

A self-learning harness may:

* Detect ambiguity
* Detect repeated review comments
* Detect recurring violations
* Compare successful and failed implementations
* Recommend stronger examples
* Suggest new validation
* Propose consolidation
* Identify obsolete guidance
* Rank Instruction effectiveness
* Generate evidence-backed proposals

### Prohibited Autonomous Behaviors

The harness must not:

* Modify Instructions silently
* Reduce Instruction strength
* Approve exceptions
* Change ownership
* Remove security requirements
* Rewrite governance policy
* Promote temporary behavior into permanent guidance
* Publish new standards without human approval

### Feedback Loop

```text
Implementation Outcomes
        ↓
Violations and Review Feedback
        ↓
Pattern Analysis
        ↓
Instruction Improvement Proposal
        ↓
Human Review
        ↓
Approved Versioned Change
        ↓
Future Execution
```

### Recommended Diagram

**Figure 6-13 — Instruction Feedback and Improvement Loop**

---

## 52. Professional Diagrams

### Purpose

Consolidate the chapter’s visual architecture.

### Required Diagrams

#### Figure 6-1 — From Repository Understanding to Governed Implementation

Shows the transition from Chapter 5 discovery into Instruction-driven execution.

#### Figure 6-2 — Prompt, Skill, Instruction, and Steering Note Relationship

Shows duration, authority, and interaction among core artifacts.

#### Figure 6-3 — Declared, Observed, and Enforced Engineering Behavior

Shows the relationship among Instructions, repository patterns, and automated controls.

#### Figure 6-4 — Instruction Scope Pyramid

Shows organization through task-specific scope.

#### Figure 6-5 — Instruction Hierarchy and Precedence

Shows authority, specialization, valid overrides, and prohibited weakening.

#### Figure 6-6 — Discovery-to-Instruction Lifecycle

Shows the complete Instruction lifecycle.

#### Figure 6-7 — Instruction Application Flow

Shows discovery, resolution, conflict detection, injection, and verification.

#### Figure 6-8 — Alpha Car Detailing Instruction Map

Shows service ownership, contracts, data boundaries, and applicable Instructions.

#### Figure 6-9 — Monorepository Instruction Resolution

Shows path-based composition.

#### Figure 6-10 — Instruction Governance Lifecycle

Shows proposal through retirement.

#### Figure 6-11 — Instruction State Model

Shows Declared, Applied, Verified, Violated, Exception-Approved, and Unverifiable states.

#### Figure 6-12 — Instruction Enforcement Inside the Harness

Shows harness components and approval gates.

#### Figure 6-13 — Instruction Feedback and Improvement Loop

Shows evidence-based recommendations and human approval.

---

## 53. Hands-On Example — Designing Alpha Car Detailing Instructions

### Purpose

Develop a complete practical Instruction design for corporate fleet booking.

### 53.1 Feature Request

Add corporate fleet booking functionality that allows authorized corporate customers to reserve detailing services for multiple vehicles at selected stations.

### 53.2 Repository Discovery Findings

* Booking Service owns reservations.
* Corporate Account Service owns customer account status and entitlements.
* Fleet Service owns fleet and vehicle relationships.
* Station Operations Service owns station capacity.
* Shared event contracts are centrally governed.
* Each service owns its SQL database.
* Redis supports selected read caching.
* Kubernetes hosts the services.
* CI/CD includes build, tests, security scans, and deployment gates.

### 53.3 Applicable Instructions

#### Architecture

* Follow Clean Architecture dependency direction.
* Keep business rules outside controllers.
* Do not access another service’s database.
* Preserve service domain ownership.

#### API

* Version public contracts.
* Return RFC 7807 Problem Details.
* Require idempotency for booking submission.
* Update OpenAPI documentation.

#### Eventing

* Version event schemas.
* Use the transactional outbox.
* Propagate event and correlation identifiers.
* Require idempotent consumers.
* Add compatibility tests.

#### Security

* Require JWT authentication.
* Enforce policy-based authorization.
* Validate corporate account ownership.
* Do not trust client-supplied tenant identity.
* Do not store secrets in source control.

#### Observability

* Add OpenTelemetry traces.
* Propagate correlation identifiers.
* Add booking metrics.
* Avoid sensitive data in logs.

#### Testing

* Add domain unit tests.
* Add API authorization integration tests.
* Add outbox integration tests.
* Add contract compatibility tests.
* Add replay/idempotency tests.

#### Delivery

* Run all required validation commands.
* Document dependency additions.
* Include PR compliance evidence.
* Require approval for migrations and breaking contracts.

### 53.4 Effective Instruction Manifest

Show how root, service, language, testing, security, and infrastructure Instructions are composed.

### 53.5 Implementation Plan Derived from Instructions

1. Add corporate booking use case in Application.
2. Add domain policies for eligibility and reservation validation.
3. Integrate with Corporate Account and Fleet through approved contracts.
4. Query Station Operations through an approved API or read model.
5. Persist booking and outbox record atomically.
6. Publish a versioned event.
7. Enforce authorization policy.
8. Add telemetry.
9. Add tests.
10. Produce compliance evidence.

### 53.6 Non-Compliant Alternatives

Show why the agent must reject:

* Controller-based business logic
* Direct cross-service SQL
* Unversioned event payloads
* Direct broker publication before database commit
* Missing idempotency
* Hard-coded credentials
* Unapproved package introduction
* Test omission

### 53.7 Expected Deliverables

* Code changes
* Tests
* Event schema
* OpenAPI update
* Architecture validation
* Security evidence
* Migration plan if required
* PR checklist
* Harness compliance report

### Recommended Table

**Table 6-15 — Corporate Fleet Booking Instruction Map**

| Concern                 | Instruction                               | Validation                   |
| ----------------------- | ----------------------------------------- | ---------------------------- |
| Domain ownership        | Booking owns reservation workflow         | Architecture review          |
| Account data            | Use Corporate Account contract            | Dependency and review checks |
| Fleet data              | Use Fleet contract                        | Integration tests            |
| Station capacity        | Use approved Station Operations interface | Contract test                |
| State/event consistency | Use transactional outbox                  | Integration test             |
| Event evolution         | Version schema                            | Compatibility test           |
| Duplicate submission    | Use idempotency                           | API integration test         |
| Authorization           | Apply policy                              | Security integration test    |
| Errors                  | RFC 7807                                  | API tests                    |
| Telemetry               | Propagate correlation and traces          | Observability test           |
| Secrets                 | Use approved secrets store                | Secret scan                  |
| PR completion           | Provide evidence                          | PR gate                      |

---

## 54. Claude Example

### Purpose

Show how the Instruction model is applied through Claude Code while preserving vendor-neutral principles.

### 54.1 Root `CLAUDE.md`

Recommended outline:

```markdown
# Repository Instructions

## Architecture
- Follow Clean Architecture dependency direction.
- Do not access another service’s database.
- Keep domain behavior outside API controllers.

## Contracts
- Version public APIs and events.
- Preserve backward compatibility unless an approved migration exists.

## Reliability
- Use the transactional outbox for committed business events.
- Event consumers must be idempotent.

## Security
- Use approved authentication and policy-based authorization.
- Do not commit secrets.

## Validation
- Run build, unit tests, integration tests, architecture tests, and security checks.
- Report commands executed and results.

## Governance
- Do not modify enterprise Instructions without explicit human approval.
```

### 54.2 Service-Level `CLAUDE.md`

Under `services/booking/`:

```markdown
# Booking Service Instructions

## Scope
These Instructions apply to Booking Service code and tests.

## Domain Ownership
Booking Service owns reservation state and booking lifecycle decisions.

## Integration
- Use approved contracts to access Corporate Account, Fleet, and Station Operations data.
- Direct access to another service database is prohibited.

## Events
- Persist booking state and outbox records atomically.
- Publish versioned booking events.
- Consumers must be idempotent.

## Security
Corporate fleet booking requires the CorporateFleetBooking authorization policy.

## Testing
Add unit, integration, authorization, outbox, and contract tests for new behavior.
```

### 54.3 Directory Context

Explain how narrower directory guidance specializes root guidance.

### 54.4 Claude Commands and Skills

Clarify:

* Instructions define required behavior.
* Claude commands or Skills define repeatable procedures.
* The task Prompt invokes current work.
* The harness validates completion.

### 54.5 Enterprise Usage

Recommend:

* Root guidance for universal standards
* Local guidance near specialized code
* Canonical ownership
* Automated synchronization where guidance is duplicated
* Harness capture of effective Instructions
* Human approval for Instruction changes

### Recommended Callout

> **Real-World Scenario**
> Claude Code finds an older direct-event-publishing implementation. The local Booking Service Instruction requires the outbox for all new business events, so the older pattern is treated as legacy evidence rather than a template.

---

## 55. GitHub Copilot Comparison

### Purpose

Explain meaningful differences in how GitHub Copilot applies repository and path-specific guidance.

### Key Concepts

* Repository-wide Instructions
* Path-specific Instructions
* Workspace context
* Prompt files
* Agent workflows
* CI/CD validation
* Governance of duplicated platform guidance

### Comparison Topics

#### Repository Instructions

Use for broad engineering standards.

#### Path-Specific Instructions

Use for:

* Booking Service
* Tests
* Infrastructure
* Documentation
* Language-specific paths

#### Prompt Files

Prompt files remain task or workflow oriented and should not replace persistent Instructions.

#### Agent Workflows

Copilot agent behavior should be combined with:

* Repository guidance
* Path applicability
* Required validation commands
* PR evidence
* CI enforcement

### Governance Considerations

* Maintain alignment with canonical Instructions.
* Avoid placing temporary sprint tasks in persistent files.
* Validate that path patterns apply correctly.
* Detect divergence from `CLAUDE.md` or shared guidance.
* Treat platform files as governed engineering assets.

### Recommended Table

**Table 6-16 — Claude Code and GitHub Copilot Instruction Application**

| Concern              | Claude Code          | GitHub Copilot                        |
| -------------------- | -------------------- | ------------------------------------- |
| Root guidance        | `CLAUDE.md`          | Repository Instructions               |
| Local specialization | Directory context    | Path-specific Instructions            |
| Reusable workflow    | Skill or command     | Prompt file or agent workflow         |
| Current task         | Prompt               | Chat or agent task                    |
| Validation           | Commands and harness | Agent workflow and CI                 |
| Governance risk      | Hierarchy ambiguity  | Path mismatch and duplicated guidance |

---

## 56. OpenAI Codex Comparison

### Purpose

Explain how repository guidance is applied in Codex-oriented execution.

### Key Concepts

* Repository guidance
* `AGENTS.md`-style Instruction patterns where applicable
* Execution environment
* Planning
* Command execution
* Changed-path scope
* Validation
* Task-level behavior

### Recommended `AGENTS.md` Content

* Architecture boundaries
* Build and test commands
* Security restrictions
* Dependency requirements
* Generated-code boundaries
* Pull-request evidence
* Human approval requirements

### Execution Considerations

Codex-oriented workflows should:

* Discover repository guidance before planning.
* Identify all changed paths.
* Apply directory-specific Instructions.
* Execute validation commands.
* Report failed or skipped checks.
* Avoid destructive actions without approval.
* Preserve an audit trail in harness-driven workflows.

### Governance Considerations

* Align `AGENTS.md` with canonical standards.
* Avoid vendor-specific divergence.
* Ensure execution permissions reflect prohibited actions.
* Validate shell and environment assumptions.
* Require human approval for sensitive operations.

### Recommended Table

**Table 6-17 — Claude Code, GitHub Copilot, and Codex**

| Engineering Concern            | Claude Code                  | GitHub Copilot             | Codex                                 |
| ------------------------------ | ---------------------------- | -------------------------- | ------------------------------------- |
| Persistent repository guidance | `CLAUDE.md`                  | Repository Instructions    | `AGENTS.md`-style guidance            |
| Local scope                    | Directory guidance           | Path-specific Instructions | Directory/repository guidance         |
| Reusable procedures            | Skills and commands          | Prompt files/workflows     | Repository workflows                  |
| Current task                   | Prompt                       | Chat/agent Prompt          | Task request                          |
| Execution                      | Tool and command use         | Agent workflow             | Command execution environment         |
| Validation                     | Explicit commands/harness    | Agent and CI               | Explicit command execution            |
| Key risk                       | Hidden hierarchy assumptions | Path configuration drift   | Environment and execution assumptions |

---

## 57. Best Practices

### Purpose

Provide actionable enterprise guidance.

### 57.1 Authoring

* Write Instructions as observable engineering behavior.
* State scope and strength explicitly.
* Include rationale for important constraints.
* Define exceptions and approvers.
* Link to validation.

### 57.2 Scope Selection

* Place guidance at the broadest valid scope.
* Specialize near applicable code.
* Avoid both excessive centralization and excessive fragmentation.
* Keep root files focused on universal standards.

### 57.3 Hierarchy Design

* Publish a documented precedence model.
* Preserve security and governance authority.
* Allow specialization without silent weakening.
* Record approved overrides.

### 57.4 Conflict Resolution

* Resolve by authority, scope, strength, and explicit override.
* Escalate material ambiguity.
* Block high-risk implementation when unresolved.
* Convert recurring conflicts into governance improvements.

### 57.5 Ownership

* Assign named owners.
* Define approvers.
* Maintain review dates.
* Avoid ownerless Instructions.

### 57.6 Versioning

* Review Instruction changes through pull requests.
* Record the reason and impact.
* Treat strength reductions as high-risk changes.
* Support rollback.

### 57.7 Validation

* Automate what can be enforced.
* Use human judgment where architecture cannot be reduced to static checks.
* Store compliance evidence.
* Identify unverifiable Instructions.

### 57.8 Platform Consistency

* Maintain canonical vendor-neutral guidance.
* Generate or synchronize platform representations carefully.
* Detect divergence automatically.
* Avoid platform-specific architecture standards.

### 57.9 Harness Integration

* Build an effective Instruction set per task.
* Inject applicable guidance into each role.
* Detect conflicts before implementation.
* Validate after implementation.
* Store evidence.

### 57.10 Security

* Make critical security behavior Mandatory or Prohibited.
* Require explicit approval for exceptions.
* Prevent Prompts from weakening security.
* Block unsafe execution.

### 57.11 Exception Handling

* Scope exceptions narrowly.
* Require owners and expiry.
* Record rationale and risk.
* Review repeated exceptions as evidence of Instruction mismatch.

### 57.12 Drift Detection

* Compare files, code, tests, and CI/CD.
* Review platform-specific divergence.
* Track stale dates and owners.
* Retire obsolete guidance.

### 57.13 Human Approval

* Require approval for Instruction changes.
* Require human resolution for material conflicts.
* Preserve audit trails.
* Do not allow self-learning systems to publish standards autonomously.

### 57.14 Effectiveness Measurement

* Measure violations, ambiguity, rework, drift, and compliance evidence.
* Use metrics to improve guidance, Skills, validation, and discoverability.
* Do not optimize for compliance counts alone.

---

## 58. Anti-Patterns

### Purpose

Describe failure modes that weaken Instruction reliability.

### 58.1 Vague Preferences

Example:

> Write clean code.

Why it fails:

* No observable behavior
* No scope
* No validation
* Multiple interpretations

### 58.2 Copying Standards into Every File

Why it fails:

* Duplication
* Drift
* Conflicting versions
* Maintenance burden

### 58.3 Mixing Prompts with Instructions

Example:

> Complete ticket ACD-412 before Friday.

Why it fails:

* Temporary task direction becomes persistent guidance.

### 58.4 Mixing Skills with Instructions

Example:

A 40-step deployment procedure placed inside a root architecture Instruction file.

Why it fails:

* Procedures and constraints evolve differently.
* Root guidance becomes unreadable.

### 58.5 Encoding Sprint Goals as Permanent Instructions

Why it fails:

* Temporary priorities remain after the sprint.
* Agents receive obsolete direction.

### 58.6 One Enormous Instruction File

Why it fails:

* Poor discoverability
* Irrelevant context
* Difficult ownership
* Increased contradiction risk

### 58.7 Excessive Fragmentation

Why it fails:

* Agents miss applicable files.
* Scope becomes unclear.
* Conflicts are hard to detect.

### 58.8 Conflicting Instruction Sources

Why it fails:

* Platform-dependent behavior
* Non-deterministic implementation
* Unsafe guessing

### 58.9 Missing Rationale

Why it fails:

* Developers bypass constraints they do not understand.
* Agents cannot make safe decisions in adjacent cases.

### 58.10 Unverifiable Instructions

Example:

> Ensure the architecture is excellent.

Why it fails:

* No measurable or reviewable criteria.

### 58.11 Treating Existing Code as the Only Instruction Source

Why it fails:

* Legacy patterns are replicated.
* Transitional architecture becomes permanent.

### 58.12 Allowing Prompts to Override Security

Why it fails:

* Task urgency defeats enduring controls.

### 58.13 Silent Agent Modification

Why it fails:

* Governance is bypassed.
* Standards change without accountability.

### 58.14 Undefined Ownership

Why it fails:

* Stale guidance persists.
* Conflicts remain unresolved.

### 58.15 No Review of Stale Instructions

Why it fails:

* Agents implement obsolete architecture.

### 58.16 Platform-Specific Contradictions

Why it fails:

* Claude, Copilot, and Codex produce different architectures.

### 58.17 Over-Constraining Implementation

Why it fails:

* Prevents valid alternatives.
* Preserves accidental detail.
* Reduces innovation.

### 58.18 Preserving Obsolete Architecture

Why it fails:

* Instructions become a barrier to modernization.

### 58.19 Treating Instructions as Documentation Only

Why it fails:

* No enforcement
* No compliance evidence
* No operational effect

### 58.20 Assuming Compliance Without Evidence

Why it fails:

* Generated code may compile while violating architecture or security.

### Recommended Table

**Table 6-18 — Common Failure Modes and Mitigations**

| Failure Mode                 | Consequence                  | Mitigation                |
| ---------------------------- | ---------------------------- | ------------------------- |
| Vague wording                | Inconsistent behavior        | Strong quality model      |
| Duplication                  | Drift                        | Canonical source          |
| Missing scope                | Over-application             | Explicit applicability    |
| Hidden precedence            | Conflict                     | Published hierarchy       |
| No validation                | Assumed compliance           | Tests and evidence        |
| No owner                     | Stale content                | Ownership metadata        |
| Silent agent edits           | Governance failure           | Human approval gate       |
| Platform divergence          | Different outcomes           | Synchronization checks    |
| Permanent temporary guidance | Obsolete direction           | Steering Notes and expiry |
| Over-constraint              | Reduced engineering judgment | Outcome-based wording     |

---

## 59. Architect’s Notes

### Purpose

Provide concise architecture-level guidance throughout the chapter.

### Architect’s Note 1 — Explicit Does Not Mean Exhaustive

Instructions should make critical constraints explicit without attempting to describe every implementation decision.

### Architect’s Note 2 — Existing Code Is Evidence, Not Authority

Repository patterns must be interpreted through current architecture, tests, and approved Instructions.

### Architect’s Note 3 — Scope Is Part of Meaning

An otherwise clear Instruction becomes dangerous when its applicability is uncertain.

### Architect’s Note 4 — Security Precedence Must Be Designed

Do not expect agents to infer that a security Instruction outranks a local convenience Instruction. Publish the precedence model.

### Architect’s Note 5 — Validation Changes Behavior

Instructions that are validated consistently become part of the engineering system. Instructions that are never checked often degrade into optional documentation.

### Architect’s Note 6 — Exceptions Are Part of Governance

A formal exception mechanism is safer than encouraging teams to ignore Instructions when unusual circumstances arise.

### Architect’s Note 7 — Platform Files Are Representations

`CLAUDE.md`, Copilot Instructions, and `AGENTS.md` should represent the same engineering model, not define competing architectures.

### Architect’s Note 8 — Self-Learning Requires Human Authority

A harness may discover that an Instruction is weak. It does not gain authority to rewrite that Instruction.

---

## 60. Enterprise Tips

### Purpose

Provide practical guidance for enterprise adoption.

### Enterprise Tip 1

Start with Instructions that protect architecture, security, data ownership, testing, and production reliability.

### Enterprise Tip 2

Create a minimum Instruction baseline for every repository.

### Enterprise Tip 3

Use service-level Instructions to define domain ownership and integration boundaries.

### Enterprise Tip 4

Link important Instructions to architecture tests, security controls, or CI gates.

### Enterprise Tip 5

Add owner and review-date metadata to high-risk Instructions.

### Enterprise Tip 6

Treat repeated review comments as evidence that an Instruction or Skill may be missing.

### Enterprise Tip 7

Use a canonical source to reduce divergence across AI platforms.

### Enterprise Tip 8

Include applicable Instruction identifiers in pull-request evidence.

### Enterprise Tip 9

Review temporary overrides automatically when they expire.

### Enterprise Tip 10

Measure Instruction effectiveness through engineering outcomes, not merely file presence.

---

## 61. Decision Points

### Purpose

Present architectural choices teams must make explicitly.

### Decision Point 1 — Centralized or Distributed Instructions

Questions:

* Which guidance is universal?
* Which guidance belongs near the code?
* How will duplication be avoided?

### Decision Point 2 — Lightweight or Structured Format

Questions:

* Is formal ownership required?
* Are exceptions allowed?
* Is auditability required?
* Can the Instruction be machine-validated?

### Decision Point 3 — Canonical Source Strategy

Options:

* One vendor-neutral canonical source
* Platform files maintained manually
* Platform files generated from structured Instructions
* Hybrid approach

### Decision Point 4 — Precedence Model

Questions:

* Which sources have authority?
* Can lower scopes override?
* Who approves exceptions?
* What blocks implementation?

### Decision Point 5 — Enforcement Level

Options:

* Informational only
* Review-enforced
* CI-validated
* Harness-enforced
* Execution-blocking

### Decision Point 6 — Instruction Granularity

Questions:

* Is the root file too broad?
* Are local files too fragmented?
* Can agents discover all relevant guidance?

### Decision Point 7 — Legacy Treatment

Questions:

* Which old patterns are tolerated?
* Which are prohibited for new code?
* What migration boundary applies?

### Decision Point 8 — Self-Learning Recommendations

Questions:

* What evidence may the harness collect?
* Who reviews proposals?
* Which categories require security or architecture approval?

---

## 62. Exercises

### Exercise 1 — Classify Instructions

Classify a set of sample statements as:

* Architectural
* Implementation
* Security
* Testing
* Operational
* Workflow
* Prohibition
* Approval Requirement
* Exception Rule

Explain overlaps.

### Exercise 2 — Rewrite Weak Instructions

Rewrite:

* Use good logging.
* Keep services independent.
* Add enough tests.
* Secure the endpoint.
* Use caching appropriately.
* Avoid breaking events.

Include:

* Scope
* Strength
* Rationale
* Validation

### Exercise 3 — Design an Instruction Hierarchy

Create an Instruction hierarchy for:

* Organization
* Alpha Car Detailing repository
* Booking Service
* Infrastructure
* Tests
* .NET projects

Document inheritance and precedence.

### Exercise 4 — Resolve Conflicting Instructions

Resolve:

Organization Instruction:

> Secrets must use centralized secrets management.

Service Instruction:

> Store the provider API key in production configuration.

Document:

* Authority
* Conflict
* Resolution
* Agent response
* Governance action

### Exercise 5 — Create a Root Instruction File

Draft a root Instruction file covering:

* Clean Architecture
* Service boundaries
* Testing
* Security
* Eventing
* Observability
* Validation
* Human approval

### Exercise 6 — Create a Booking Service Instruction File

Define:

* Domain ownership
* Allowed integrations
* Persistence
* Eventing
* Authorization
* Testing
* Prohibitions

### Exercise 7 — Define Machine-Enforceable Validation

For ten Instructions, identify:

* Static analysis
* Architecture test
* Unit test
* Integration test
* Contract test
* Security scan
* Human review

### Exercise 8 — Design an Exception Workflow

Design:

* Request format
* Required evidence
* Approvers
* Expiry
* Audit record
* Renewal
* Closure

### Exercise 9 — Identify Instruction Drift

Review a hypothetical repository where:

* The root file references removed projects.
* Copilot Instructions require a deprecated package.
* Architecture tests enforce undocumented boundaries.
* A temporary override has existed for two years.

Produce a drift report.

### Exercise 10 — Design Harness-Based Enforcement

Define components for:

* Discovery
* Resolution
* Conflict detection
* Prompt injection
* Validation
* Evidence
* Approval
* Change proposals

### Exercise 11 — Compare Platform Implementations

Represent the same engineering standard using:

* Claude Code
* GitHub Copilot
* OpenAI Codex

Identify:

* Common semantics
* Scope differences
* Discovery differences
* Governance risks

### Exercise 12 — Corporate Fleet Booking Compliance Plan

Create the complete Instruction compliance plan for Alpha Car Detailing corporate fleet booking.

---

## 63. Interview Questions

### Question 1

**What is an Instruction in enterprise AI engineering?**

**Model answer:**
An Instruction is persistent, scoped, and governed engineering guidance that defines how an AI agent must or should operate within an organization, repository, directory, service, project, language, or framework.

### Question 2

**Why should an AI agent not infer all standards from existing code?**

**Model answer:**
Existing code may include legacy patterns, temporary workarounds, deprecated designs, security exceptions, and inconsistent implementations. Explicit Instructions distinguish approved behavior from historical evidence.

### Question 3

**How do Instructions differ from Prompts?**

**Model answer:**
Instructions define enduring engineering behavior. Prompts define the current task. A Prompt operates within applicable Instructions and cannot silently override mandatory security, compliance, or governance requirements.

### Question 4

**How do Instructions differ from Skills?**

**Model answer:**
Instructions define required outcomes and constraints. Skills define reusable procedures for performing recurring work within those constraints.

### Question 5

**Can a service-level Instruction override a repository-root Instruction?**

**Model answer:**
It may specialize broader guidance when explicitly allowed, but it must not silently weaken mandatory security, compliance, governance, or higher-authority architectural requirements.

### Question 6

**Can a Steering Note override a mandatory Instruction?**

**Model answer:**
Not by default. It may affect temporary priorities or implementation sequencing. A conflicting override requires explicit authority, scope, rationale, approval, and usually an expiry.

### Question 7

**What should an agent do when Instruction precedence is unclear?**

**Model answer:**
It should identify the conflict, determine scope and authority, preserve mandatory controls, record the ambiguity, request human resolution, and block materially risky implementation until resolved.

### Question 8

**What makes an Instruction testable?**

**Model answer:**
It describes observable behavior and identifies a validation method such as static analysis, an architecture test, an integration test, a security scan, CI/CD enforcement, or human review evidence.

### Question 9

**What is Instruction drift?**

**Model answer:**
Instruction drift occurs when Instructions no longer align with repository structure, architecture, tests, platform files, organizational policies, or actual approved engineering behavior.

### Question 10

**What is the difference between a Declared and Verified Instruction?**

**Model answer:**
A Declared Instruction exists and is published. A Verified Instruction has been applied to a task and supported by compliance evidence.

### Question 11

**Why is ownership important?**

**Model answer:**
Ownership establishes accountability for accuracy, review, conflict resolution, approval, exception handling, and retirement.

### Question 12

**Which Instructions should be machine-enforceable?**

**Model answer:**
Instructions with objective technical criteria should be automated where practical, including dependency direction, secret detection, required tests, schema versioning, forbidden packages, and cross-service references.

### Question 13

**Why can architecture not be fully enforced through automation?**

**Model answer:**
Some concerns require judgment, including domain ownership, abstraction quality, coupling, bounded-context design, and whether a technically valid implementation aligns with business architecture.

### Question 14

**How should an enterprise manage Instructions across Claude Code, Copilot, and Codex?**

**Model answer:**
It should maintain consistent vendor-neutral engineering semantics, represent them using each platform’s supported mechanism, and automatically detect divergence where possible.

### Question 15

**May a self-learning harness update Instructions automatically?**

**Model answer:**
No. It may detect ambiguity, collect evidence, and propose changes, but human approval is mandatory before enterprise Instructions are modified.

### Question 16

**What should happen when a Prompt requests bypassing a security Instruction?**

**Model answer:**
The security Instruction remains authoritative. The agent should reject the conflicting action or request a formally approved exception.

### Question 17

**How should legacy code be handled?**

**Model answer:**
Instructions should distinguish tolerated legacy behavior from current standards. Agents should avoid expanding obsolete patterns while also avoiding unrelated broad modernization without approval.

### Question 18

**What is an effective Instruction set?**

**Model answer:**
It is the resolved collection of all applicable Instructions for a task, ordered by authority and scope, checked for conflicts, and linked to validation requirements.

### Question 19

**What evidence should a harness record?**

**Model answer:**
Applicable Instructions, source versions, conflicts, exceptions, tests, scans, validation outcomes, violations, approvals, and unresolved risks.

### Question 20

**Why is a lower-level specialization safer than an unrestricted override?**

**Model answer:**
Specialization provides context-specific detail while preserving broader obligations. Unrestricted overrides can silently weaken security, compliance, architecture, or governance.

---

## 64. Chapter Summary

### Purpose

Consolidate the chapter’s engineering model and prepare the reader for Skills.

### Summary Points

* Repository Discovery determines what the repository contains.
* Instructions define how work must be performed within that repository.
* Instructions are persistent, scoped, versioned, owned, governed engineering artifacts.
* Existing code patterns are evidence but are not automatically authoritative.
* Instructions must remain distinct from Skills, Prompts, Roles, Steering Notes, Knowledge Sources, and Governance Policies.
* Professional Instructions define scope, strength, required behavior, prohibited behavior, rationale, validation, ownership, and exceptions.
* Instruction hierarchy must support inheritance and specialization without allowing lower scopes to silently weaken mandatory controls.
* Prompts and Steering Notes do not override security, compliance, or governance requirements by default.
* Conflicts must be resolved systematically through authority, scope, strength, version, and explicit override rules.
* Instructions should be validated through automation where possible and human judgment where necessary.
* Compliance requires evidence, not assumption.
* Instructions may be Declared, Applied, Verified, Violated, Exception-Approved, or Unverifiable.
* Instruction drift must be detected across code, documentation, tests, CI/CD, policies, and platform-specific files.
* Enterprise harnesses should load, interpret, enforce, validate, and govern Instructions as separate responsibilities.
* Self-learning harnesses may recommend improvements but must not silently modify enterprise Instructions.
* Claude Code, GitHub Copilot, and Codex represent Instructions differently, but the underlying engineering model should remain consistent and vendor-neutral.
* Alpha Car Detailing demonstrates that explicit Instructions prevent code that compiles but violates architecture, security, reliability, and operational expectations.

### Transition to Chapter 7 — Skills

Instructions establish the enduring standards, boundaries, obligations, and prohibitions that govern engineering work.

They tell an AI agent:

* What architecture must be preserved
* Which actions are prohibited
* Which controls are mandatory
* Which evidence must be produced
* Which decisions require human approval

Instructions do not, however, provide the complete repeatable procedure for every recurring engineering activity.

That is the role of Skills.

Chapter 7 introduces Skills as reusable engineering procedures for performing recurring work—such as creating an API, implementing an idempotent consumer, adding OpenTelemetry, creating a database migration, or validating a pull request—while remaining within the constraints established by Instructions.

---

## 65. Further Reading

### Purpose

Identify authoritative topics readers should study without turning the chapter into a platform documentation index.

### Recommended Categories

#### Architecture and Governance

* Architecture decision records
* Software architecture governance
* Clean Architecture
* Domain-driven design
* Evolutionary architecture
* Policy as code
* Enterprise security governance

#### API and Contract Design

* RFC 7807 Problem Details
* API versioning
* Consumer-driven contract testing
* Schema evolution
* Backward compatibility

#### Event-Driven Systems

* Transactional outbox
* Idempotent consumers
* Event schema governance
* Retry and poison-message design
* Distributed tracing

#### Security

* Secure software development lifecycle
* Secrets management
* Policy-based authorization
* Supply-chain security
* Dependency governance

#### Testing and Validation

* Architecture testing
* Static analysis
* Integration testing
* Contract testing
* CI/CD quality gates
* Compliance evidence

#### AI Coding Platforms

* Claude Code repository guidance
* GitHub Copilot repository and path-specific Instructions
* OpenAI Codex repository guidance and execution practices

#### AI Engineering Governance

* Human-in-the-loop approval
* AI audit trails
* Agent permission boundaries
* Prompt injection defenses
* Controlled self-learning systems

---

**Chapter 6 outline is complete.**
