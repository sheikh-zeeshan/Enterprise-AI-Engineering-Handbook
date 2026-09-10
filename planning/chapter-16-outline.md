# Chapter 16 — Reviewer Agent

**Part III — Harness Engineering**
**Target file:** `book/16-reviewer-agent.md`

The outline follows the frozen handbook structure, terminology, Claude-first/vendor-neutral approach, Alpha Car Detailing running example, and the agreed Harness role model.

---

## 1. Story-Driven Opening

### Scenario: Corporate Fleet Booking Change Reaches Review

* Continue the Alpha Car Detailing corporate fleet booking scenario from Chapter 15.
* Developer Agent has completed a change that:

  * adds or modifies a fleet-booking API
  * introduces domain behavior
  * persists booking information
  * publishes an integration event
  * adds tests
  * updates configuration where necessary
* Developer Agent submits:

  * implementation changes
  * implementation summary
  * affected files
  * test evidence
  * known assumptions
  * contract changes
  * unresolved concerns
* Reviewer Agent receives the implementation independently.
* Reviewer discovers issues that compilation alone would not necessarily identify:

  * architecture boundary violation
  * missing authorization check
  * incompatible event schema change
  * insufficient test coverage
  * logging containing sensitive business data
* Establish the core distinction:

  * Developer Agent implements.
  * Reviewer Agent examines.
  * Deterministic Gates execute objective checks.
  * Validator Agent verifies that required validation has succeeded.
  * Evaluator Agent assesses broader quality or outcome criteria.
  * humans retain approval authority where required.

### Opening Principle

> The Reviewer Agent does not ask only whether the implementation works. It asks whether the implementation is appropriate, maintainable, compliant, secure, architecturally aligned, and supported by evidence.

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

* Define the Reviewer Agent within an enterprise AI Engineering Harness.
* Distinguish review responsibilities from development, deterministic validation, validation, evaluation, and human approval.
* Define Reviewer Agent authority and boundaries.
* Design evidence-based review workflows.
* Review implementation against:

  * Instructions
  * acceptance criteria
  * architecture
  * contracts
  * security expectations
  * persistence standards
  * observability standards
  * tests
  * configuration
  * dependencies
* Classify findings using a consistent severity model.
* Generate actionable review findings supported by evidence.
* Design review/correction iteration loops.
* Define Reviewer Agent handoffs.
* Identify situations requiring escalation.
* Implement the Reviewer role in a Claude-first Harness.
* Compare Reviewer implementations across Claude Code, GitHub Copilot, and OpenAI Codex.

---

# 3. Background

## 3.1 Why Independent Review Exists

* Software review before AI-assisted engineering.
* Human code-review practices.
* Why implementation and review should remain distinct responsibilities.
* Risks of self-review by the same AI Agent.
* Confirmation bias in AI-assisted development.
* Why generated code can be syntactically valid but architecturally wrong.
* Why enterprise review requires repository context.

## 3.2 Review in AI-Assisted Software Engineering

* AI-generated changes increase implementation speed.
* Faster implementation can increase review pressure.
* Reviewer Agent as a scalable quality-control role.
* Review as reasoning over evidence rather than merely reading a diff.
* Review as part of a governed Harness workflow.

## 3.3 Review Is Not Validation

Explain the distinction between:

* review
* deterministic validation
* Validator Agent
* Evaluator Agent
* human approval

Example:

```text
Reviewer:
"This endpoint bypasses the Application layer."

Architecture Gate:
"Forbidden Infrastructure reference detected."

Validator:
"All mandatory gates executed and passed."

Evaluator:
"The implementation satisfies the requested business outcome."

Human:
"Approve this change for production."
```

---

# 4. Concepts

## 4.1 What Is a Reviewer Agent?

Define the Reviewer Agent as:

> An independent AI Engineering role responsible for examining implementation evidence and identifying defects, risks, architectural deviations, maintainability issues, security concerns, contract impacts, and non-compliance with Instructions or acceptance criteria.

Clarify that Reviewer Agent:

* reads implementation
* understands requested work
* compares implementation with expected standards
* identifies concerns
* produces findings
* requests corrections where necessary
* does not modify implementation unless the Harness explicitly defines a separate correction role transition

## 4.2 Core Reviewer Principle

```text
Implementation evidence
        +
Repository Intelligence
        +
Instructions
        +
Acceptance Criteria
        +
Architecture constraints
        ↓
Reviewer reasoning
        ↓
Evidence-based findings
```

## 4.3 Reviewer Agent Responsibilities

Cover responsibilities including:

* understand the task
* understand expected outcome
* inspect implementation evidence
* identify affected components
* read applicable Instructions
* verify acceptance-criteria coverage
* review architecture alignment
* review code quality
* review contracts
* inspect persistence impact
* inspect security concerns
* inspect observability
* inspect tests
* inspect configuration
* inspect dependencies
* detect likely regressions
* classify findings
* provide evidence
* determine review disposition
* escalate unresolved decisions

## 4.4 Authority

Reviewer Agent may:

* inspect code and configuration
* inspect diffs
* inspect test changes
* query Repository Intelligence
* consult applicable Knowledge Sources
* reference Instructions
* identify violations
* request changes
* block progression according to Harness policy
* escalate uncertainty
* recommend deterministic checks

## 4.5 Limits

Reviewer Agent must not:

* approve its own implementation
* silently rewrite architecture standards
* redefine acceptance criteria
* bypass Instructions
* replace deterministic validation
* claim tests passed without execution evidence
* replace the Validator Agent
* replace the Evaluator Agent
* approve production deployment unless explicitly authorized
* silently resolve ambiguous business decisions
* invent repository standards

---

# 5. Reviewer Inputs

## 5.1 Task Context

Reviewer should receive:

* original Prompt
* task identifier
* acceptance criteria
* Steering Note context
* selected Skills
* applicable Instructions
* repository scope

## 5.2 Implementation Evidence

Potential evidence:

* changed files
* diff
* implementation summary
* Developer Agent notes
* test additions
* local test results
* new dependencies
* API contract changes
* event schema changes
* database migration changes
* configuration changes
* known limitations
* architectural assumptions

## 5.3 Repository Intelligence

Reviewer should understand:

* project structure
* architectural boundaries
* existing patterns
* naming conventions
* previous implementations
* related tests
* cross-service dependencies
* contract ownership

## 5.4 Knowledge Sources

Potential sources:

* ADRs
* API specifications
* schema definitions
* security standards
* architecture diagrams
* business requirements
* deployment definitions
* historical decisions

---

# 6. Architecture Discussion

## 6.1 Reviewer Agent Responsibility Boundary

### Diagram: Reviewer Agent Responsibility Boundary

```text
                    Reviewer Agent
┌───────────────────────────────────────────────────┐
│ Inspect implementation                            │
│ Compare against Instructions                      │
│ Check acceptance criteria                         │
│ Review architecture                               │
│ Review quality/security/contracts/tests           │
│ Produce evidence-based findings                   │
│ Recommend accept / reject / request changes       │
└───────────────────────────────────────────────────┘
              │                     │
              │ does not replace    │ does not own
              ▼                     ▼
    Deterministic Gates       Human Approval
              │
              ▼
       Validator Agent
              │
              ▼
       Evaluator Agent
```

## 6.2 Reviewer Position in Role Architecture

```text
Lead Agent
    ↓
Developer Agent
    ↓
Reviewer Agent
    ↓
Deterministic Gates
    ↓
Validator Agent
    ↓
Evaluator Agent
```

Discuss possible workflow variation:

```text
Developer
   ↓
Reviewer
   ↓
Changes Required
   ↓
Developer
   ↓
Reviewer
   ↓
Deterministic Gates
```

## 6.3 Separation of Duties

Explain enterprise reasoning for separating:

* implementation
* review
* validation
* evaluation
* approval

## 6.4 Reviewer as a Semantic Quality Gate

Explain difference between:

### Semantic review

Requires reasoning:

* Is this architecture appropriate?
* Is the implementation maintainable?
* Does the API behavior match intent?
* Could this introduce a business regression?

### Deterministic gate

Produces mechanically reproducible evidence:

* build succeeds
* unit tests pass
* lint succeeds
* architecture tests pass
* dependency scan succeeds

---

# 7. Review Workflow

## 7.1 End-to-End Review Workflow

### Diagram: Review Workflow

```text
Developer Agent
      ↓
Implementation Evidence
      ↓
Reviewer Agent
      ↓
Understand Task
      ↓
Read Instructions
      ↓
Inspect Changes
      ↓
Review Architecture / Quality / Security / Contracts
      ↓
Produce Findings
      ↓
┌───────────────┬──────────────────┬───────────────┐
│ Accept        │ Request Changes  │ Escalate      │
└───────────────┴──────────────────┴───────────────┘
```

## 7.2 Review Preparation

* confirm task scope
* inspect Developer summary
* identify affected systems
* identify critical contracts
* locate relevant Instructions
* determine risk areas
* identify review depth

## 7.3 Risk-Based Review

Examples of high-risk changes:

* authentication
* authorization
* payment logic
* customer data
* shared contracts
* event schemas
* database migrations
* infrastructure
* public APIs
* concurrency
* distributed workflows

---

# 8. Reviewing Against Instructions

## 8.1 Applicable Instructions

Reviewer should identify:

* repository-wide Instructions
* directory-specific Instructions
* service-level standards
* testing requirements
* naming conventions
* architecture constraints
* security requirements
* observability requirements

## 8.2 Instruction Compliance Review

Example checks:

* dependency direction
* logging conventions
* exception handling
* result patterns
* testing requirements
* DTO rules
* API conventions

## 8.3 Conflicting Instructions

* detect conflicts
* prefer scoped precedence rules defined by Harness
* do not resolve policy ambiguity silently
* escalate when needed

---

# 9. Reviewing Against Acceptance Criteria

## 9.1 Acceptance Criteria as Review Evidence

Reviewer maps each criterion to implementation evidence.

Example:

| Acceptance Criterion                        | Evidence                | Review Result |
| ------------------------------------------- | ----------------------- | ------------- |
| Corporate customer can create fleet booking | API + application logic | Met           |
| Customer must belong to active contract     | missing validation      | Not met       |
| Booking publishes event                     | event publisher         | Met           |
| Duplicate requests are rejected             | no idempotency handling | Not met       |

## 9.2 Detecting Partial Implementations

* happy path exists
* edge case missing
* expected error behavior absent
* required event not emitted
* authorization requirement omitted

## 9.3 Avoiding Assumed Compliance

Reviewer should distinguish:

* demonstrated
* inferred
* unverified
* missing

---

# 10. Architecture Review

## 10.1 Clean Architecture

Review:

* dependency direction
* Domain independence
* Application orchestration
* Infrastructure boundaries
* controller responsibilities
* repository abstractions

## 10.2 DDD Review

Inspect:

* aggregate boundaries
* domain invariants
* value objects
* entity responsibilities
* domain services
* domain events

## 10.3 Microservice Boundaries

Review:

* service ownership
* shared database risks
* cross-service coupling
* direct database access
* synchronous dependency growth

## 10.4 Event-Driven Architecture

Review:

* event ownership
* event naming
* schema evolution
* delivery assumptions
* idempotency
* failure handling

### Architect's Note

Reviewer Agent should detect architecture drift before it becomes a repository pattern.

---

# 11. Code Quality Review

## 11.1 Readability

* naming
* method responsibility
* unnecessary complexity
* duplication
* hidden side effects

## 11.2 Maintainability

* coupling
* cohesion
* abstraction quality
* unnecessary indirection
* testability

## 11.3 Error Handling

* meaningful errors
* correct boundaries
* swallowed exceptions
* inappropriate retries
* error translation

## 11.4 Concurrency and Async Code

* cancellation tokens
* async correctness
* blocking calls
* race conditions
* shared state

## 11.5 Over-Engineering

Reviewer identifies:

* unnecessary frameworks
* premature abstractions
* excessive interfaces
* unnecessary patterns
* speculative flexibility

---

# 12. API Contract Review

Review:

* route conventions
* HTTP verbs
* status codes
* request model
* response model
* validation
* backward compatibility
* error contracts
* pagination where relevant
* authentication
* authorization

Alpha Car Detailing example:

```text
POST /api/fleet-bookings
```

Potential review findings:

* returns `200` instead of `201`
* leaks internal entity
* missing contract identifier
* invalid error format
* allows unauthorized station assignment

---

# 13. Event Contract Review

Review:

* event type
* schema
* version
* required fields
* correlation identifiers
* timestamps
* backwards compatibility
* producer ownership
* consumer impact
* idempotency considerations

Alpha Car Detailing example:

```text
FleetBookingCreated
```

Potential contract concern:

Developer removes an existing field rather than introducing a compatible schema evolution.

---

# 14. Persistence Review

## 14.1 Entity Mapping

Review:

* required fields
* indexes
* constraints
* keys
* relationships

## 14.2 Migration Safety

Review:

* destructive migration
* nullable-to-required changes
* default values
* large-table impact
* rollback implications

## 14.3 Transaction Boundaries

Review:

* atomic business operations
* database plus event publishing
* transactional outbox where required

## 14.4 Performance

Review:

* unnecessary queries
* N+1 queries
* missing indexes
* large result sets
* tracking where not needed

---

# 15. Security Review

## 15.1 Authentication

* authenticated caller assumptions
* token validation dependencies

## 15.2 Authorization

* tenant/customer isolation
* station permissions
* role permissions
* object-level authorization

Alpha Car Detailing example:

Corporate Fleet Manager A must not access Corporate Fleet B.

## 15.3 Input Security

* validation
* injection risks
* unsafe deserialization
* path manipulation

## 15.4 Sensitive Information

Review:

* logs
* exceptions
* telemetry
* responses
* event payloads

## 15.5 Secrets

Detect:

* hard-coded credentials
* connection strings
* tokens
* insecure configuration

### Enterprise Tip

Semantic security review should complement—not replace—SAST, dependency scanning, secret scanning, and other deterministic security gates.

---

# 16. Observability Review

Review:

* structured logging
* correlation ID
* trace propagation
* metrics
* business events
* failure logging
* sensitive-data exposure
* logging levels

Alpha Car Detailing example:

Fleet booking request should be traceable across:

```text
Booking API
   ↓
Application
   ↓
Database
   ↓
Outbox
   ↓
Event Broker
```

---

# 17. Test Review

## 17.1 Test Presence

* unit tests
* integration tests
* contract tests
* architecture tests

## 17.2 Test Quality

Reviewer should inspect:

* meaningful assertions
* edge cases
* failure scenarios
* authorization
* boundary conditions
* regression coverage

## 17.3 Tests That Pass but Prove Little

Examples:

* asserts `result != null`
* mocks everything
* never verifies behavior
* tests implementation details
* happy-path-only coverage

## 17.4 Test Traceability

Link tests to:

* acceptance criteria
* identified risks
* regression scenarios

---

# 18. Configuration Review

Inspect:

* environment variables
* appsettings changes
* feature flags
* Kubernetes configuration
* Docker settings
* infrastructure configuration
* defaults
* environment-specific behavior

Detect:

* unsafe production defaults
* development-only settings
* missing required configuration
* duplicated configuration

---

# 19. Dependency Review

Review:

* newly added packages
* reason for dependency
* existing equivalent capability
* version consistency
* licensing considerations
* security implications
* transitive dependency impact

Common finding:

> Developer introduces a new package for functionality already available in the platform.

---

# 20. Detecting Regressions

## 20.1 Behavioral Regressions

* changed existing API behavior
* altered validation
* removed event fields
* changed status codes

## 20.2 Architectural Regressions

* new dependency violation
* service boundary erosion
* bypassing established abstractions

## 20.3 Operational Regressions

* removed telemetry
* changed retry behavior
* increased synchronous dependency

## 20.4 Security Regressions

* weaker authorization
* broader data access
* sensitive logging

---

# 21. Findings

## 21.1 Structure of a Finding

Every finding should include:

* identifier
* title
* severity
* category
* location
* evidence
* impact
* violated Instruction or acceptance criterion where applicable
* recommended correction
* confidence where useful

Example:

```text
Finding: REV-004

Severity: High

Category: Authorization

Location:
FleetBookingsController.cs

Evidence:
The GetBooking endpoint retrieves booking data by booking ID
without verifying that the authenticated corporate account owns
the requested booking.

Impact:
A corporate fleet customer may access another customer's booking.

Required Action:
Apply corporate-account ownership validation before returning
booking information.
```

## 21.2 Evidence-Based Findings

Avoid:

> "This code seems insecure."

Prefer:

> "The endpoint accepts a booking ID and retrieves the booking directly without applying the existing `ICorporateAccountAccessPolicy` used by the equivalent fleet-order endpoint."

---

# 22. Finding Severity Model

### Diagram: Finding Severity Model

```text
Critical
   │
   ├─ security exposure
   ├─ data corruption
   └─ severe production risk

High
   │
   ├─ acceptance criterion failure
   ├─ architecture violation
   ├─ breaking contract
   └─ significant regression

Medium
   │
   ├─ maintainability concern
   ├─ insufficient tests
   └─ observability weakness

Low
   │
   ├─ minor consistency issue
   └─ low-risk improvement

Informational
   │
   └─ optional recommendation
```

## 22.1 Severity Is Not Style Preference

Explain why reviewer should not mark stylistic preferences as High severity.

## 22.2 Severity and Harness Policy

Example policy:

```text
Critical → block
High     → block
Medium   → changes normally required
Low      → configurable
Info     → advisory
```

---

# 23. Review Dispositions

## 23.1 Accept

Appropriate when:

* no blocking findings
* acceptance criteria appear implemented
* no material review concerns remain
* evidence is sufficient for progression

Clarify:

Reviewer acceptance does **not** mean production approval.

## 23.2 Request Changes

Used when:

* implementation is correctable
* specific findings exist
* Developer Agent should revise the implementation

## 23.3 Reject

Use for cases such as:

* implementation fundamentally violates scope
* architecture direction is incorrect
* major requirements are missing
* unsafe approach requires reimplementation

## 23.4 Escalate

Used when:

* requirement is ambiguous
* architecture decision is missing
* Instructions conflict
* business policy requires human judgment
* security risk requires specialist review

---

# 24. Review and Correction Loop

### Diagram: Review and Correction Loop

```text
Developer Agent
      ↓
Implementation
      ↓
Reviewer Agent
      ↓
Findings?
  ┌───┴────┐
  │        │
 No       Yes
  │        │
  │        ↓
  │   Developer Correction
  │        ↓
  │   Updated Evidence
  │        ↓
  └──── Reviewer Agent
           ↓
     Review Accepted
           ↓
   Deterministic Gates
```

## 24.1 Iteration Discipline

Each iteration should record:

* review number
* findings
* corrections
* unresolved findings
* disposition

## 24.2 Preventing Endless Loops

Harness may define:

* maximum review iterations
* escalation threshold
* repeated-finding detection

Example:

```text
3 unsuccessful review cycles
        ↓
Architect or human escalation
```

---

# 25. Handoff to Developer Agent

Reviewer handoff should include:

* blocking findings
* severity
* evidence
* affected file
* expected correction
* acceptance criterion reference
* relevant Instruction
* unresolved questions

Avoid implementing fixes inside the review response unless that behavior is explicitly part of the Harness design.

---

# 26. Handoff to Validator Agent

Reviewer communicates:

* review disposition
* findings status
* unresolved advisory findings
* known risk areas
* expected deterministic gates

Validator then verifies:

* required gates ran
* required results are present
* expected deterministic evidence exists

---

# 27. Reviewer Handoffs

### Diagram: Reviewer Handoffs

```text
                    Lead Agent
                        │
                        ▼
                 Developer Agent
                        │
                        ▼
                 Reviewer Agent
                 /      |       \
                /       |        \
               ▼        ▼         ▼
         Developer   Validator   Escalation
         Correction    Agent       Path
                         │
                         ▼
                    Evaluator
```

Discuss handoffs to:

* Developer
* Validator
* Lead
* Architect
* Security Reviewer
* Human Approver

---

# 28. Escalation

## 28.1 Architecture Escalation

Examples:

* unclear service ownership
* missing ADR
* competing architecture patterns

## 28.2 Security Escalation

Examples:

* identity design
* sensitive-data handling
* encryption decisions
* trust-boundary changes

## 28.3 Business Escalation

Examples:

* acceptance criteria conflict with existing behavior
* corporate policy is unclear

## 28.4 Instruction Conflict

Reviewer reports:

```text
Conflict detected
   +
Evidence
   +
Affected decision
   ↓
Lead / Architect / Human
```

---

# 29. Human Approval Boundaries

Explain that humans may remain responsible for:

* security-sensitive changes
* architecture changes
* public contract breaking changes
* production deployment
* governance exceptions
* regulatory concerns
* high-risk data changes

Reviewer Agent provides evidence, not organizational authority.

---

# 30. Reviewer Agent Inside the Harness

### Diagram: Reviewer Agent Inside the Harness

```text
┌──────────────── Enterprise AI Engineering Harness ───────────────┐
│                                                                  │
│  Prompt                                                          │
│    ↓                                                             │
│  Lead Agent                                                      │
│    ↓                                                             │
│  Developer Agent                                                 │
│    ↓                                                             │
│  Implementation Evidence                                         │
│    ↓                                                             │
│  Reviewer Agent                                                  │
│    ├── Instructions                                               │
│    ├── Repository Intelligence                                   │
│    ├── Knowledge Sources                                         │
│    ├── Acceptance Criteria                                       │
│    └── Architecture Context                                      │
│           ↓                                                      │
│       Findings                                                   │
│           ↓                                                      │
│    Developer Correction ───────────────┐                          │
│           ↓                           │                          │
│       Reviewer Recheck ◄──────────────┘                          │
│           ↓                                                      │
│    Deterministic Gates                                           │
│           ↓                                                      │
│      Validator Agent                                             │
│           ↓                                                      │
│      Evaluator Agent                                             │
│           ↓                                                      │
│     Human Approval                                               │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
```

## 30.1 Reviewer Inputs

* Prompt
* plan
* Steering Note
* Instructions
* implementation diff
* Developer evidence
* Repository Intelligence
* Knowledge Sources

## 30.2 Reviewer Outputs

Potential structured artifact:

```text
review-result.json
```

Fields may include:

* taskId
* reviewId
* disposition
* findings
* severity
* category
* evidence
* file
* line/reference
* requiredAction
* escalationRequired

## 30.3 Review History

Harness stores:

* findings by category
* repeated defects
* correction iterations
* false-positive review patterns
* review duration
* final dispositions

Later chapters can use this information for:

* Metrics
* Learning from Reviews
* Skill Evolution
* Instruction Evolution

---

# 31. Hands-on Example

## Alpha Car Detailing Corporate Fleet Booking

### Starting Change

Developer Agent implements:

```text
POST /api/fleet-bookings
```

Implementation includes:

* controller
* application service
* domain behavior
* EF Core persistence
* integration event
* tests

### Reviewer Assignment

Review for:

* corporate account authorization
* service boundaries
* domain invariants
* API contract
* event contract
* persistence
* tests
* observability
* Instructions

### Example Findings

Possible findings:

1. **High — Authorization**

   * corporate-account ownership check missing.

2. **High — Event Contract**

   * `FleetBookingCreated` removed an existing required field.

3. **Medium — Architecture**

   * controller directly calls Infrastructure repository.

4. **Medium — Testing**

   * no duplicate-booking test.

5. **Low — Observability**

   * station identifier not included in structured operation context.

### Correction Cycle

```text
Developer
   ↓
Reviewer
   ↓
5 Findings
   ↓
Developer Correction
   ↓
Reviewer
   ↓
0 Blocking Findings
   ↓
Deterministic Gates
```

---

# 32. Claude Example

## 32.1 Claude Code Reviewer Role

Show a representative Harness structure:

```text
harness/
├── roles/
│   ├── lead.md
│   ├── developer.md
│   ├── reviewer.md
│   └── validator.md
```

## 32.2 Reviewer Role Definition

Conceptual responsibilities:

* read task context
* inspect implementation
* inspect applicable Instructions
* review against acceptance criteria
* report evidence-based findings
* avoid modifying code
* return structured disposition

## 32.3 Claude Review Prompt

Prompt context may provide:

```text
Task
Acceptance Criteria
Developer Summary
Changed Files
Instructions
Repository Intelligence
Review Output Schema
```

## 32.4 Claude Review Output

Example structure:

```text
Review Result

Disposition: REQUEST_CHANGES

Findings:
REV-001 High Authorization
REV-002 Medium Architecture
REV-003 Medium Testing
```

## 32.5 Tool Usage

Claude may use:

* repository search
* file reading
* diff inspection
* test inspection
* architecture references
* Knowledge Sources

Emphasize:

Claude's tool capability helps gather evidence but does not redefine Reviewer authority.

---

# 33. GitHub Copilot Comparison

Compare areas such as:

* IDE-centered review experience
* repository instruction support
* pull-request review workflows
* developer interaction model
* context gathering
* agent capabilities
* Harness integration

Discuss where enterprise teams may need additional orchestration to achieve:

* strict role separation
* structured review outputs
* deterministic handoffs
* persistent iteration history

Keep principles vendor-neutral.

---

# 34. OpenAI Codex Comparison

Compare:

* repository task execution
* isolated review assignment
* diff inspection
* codebase reasoning
* review-to-correction workflow
* automation integration
* structured evidence output

Explain that platform capability may differ while the role contract remains:

```text
Reviewer responsibility
    ≠
vendor implementation
```

---

# 35. Best Practices

Include guidance such as:

1. Separate Developer and Reviewer responsibilities.
2. Review against explicit evidence.
3. Always provide acceptance criteria to Reviewer.
4. Supply applicable Instructions.
5. Give Reviewer Repository Intelligence.
6. Use risk-based review depth.
7. Keep findings actionable.
8. Require evidence for important findings.
9. Use a consistent severity model.
10. Separate semantic findings from deterministic gate failures.
11. Re-review corrected implementation.
12. Record review history.
13. Escalate uncertainty instead of inventing policy.
14. Keep Reviewer role read-oriented where practical.
15. Use deterministic tools for claims that can be mechanically verified.

---

# 36. Anti-Patterns

## 36.1 Developer Reviews Its Own Work

Why it weakens independent review.

## 36.2 Reviewer Automatically Rewrites Code

Blurs responsibility boundaries.

## 36.3 Reviewer Runs Only a Build

Build success is not code review.

## 36.4 Reviewer Produces Style Opinions Without Evidence

Example:

> "I prefer this method name."

## 36.5 Reviewer Invents Requirements

Reviewer must evaluate requirements, not create them.

## 36.6 Every Finding Is High Severity

Destroys signal.

## 36.7 Reviewer Replaces Security Tooling

Semantic review should not replace scanning.

## 36.8 Reviewer Approves Production

Review acceptance is not deployment approval.

## 36.9 Ignoring Contract Impact

Local implementation may break distributed consumers.

## 36.10 Endless AI Review Loops

Require escalation policy.

---

# 37. Architect's Notes

Topics:

* Reviewer Agent as a semantic control in enterprise Harnesses.
* Separation of duties should be explicit rather than implied.
* Review quality depends heavily on Repository Intelligence quality.
* Reviewer Agent should inspect architectural intent, not only code syntax.
* Review results should be machine-readable enough for Harness orchestration.
* Findings should become measurable engineering data.
* Repeated findings may expose weaknesses in Instructions or Skills.
* Reviewer recommendations must not silently modify standards.

---

# 38. Enterprise Tips

Potential callouts:

### Enterprise Tip: Risk-Based Review Profiles

Define review profiles such as:

```text
Standard
Security-Sensitive
Contract-Sensitive
Data-Migration
Architecture-Critical
```

### Enterprise Tip: Review Baselines

Maintain reusable review checklists for:

* REST APIs
* events
* persistence
* authentication
* infrastructure

### Enterprise Tip: Structured Findings

Machine-readable findings enable:

* metrics
* trend analysis
* review dashboards
* self-learning recommendations

### Enterprise Tip: Independent Context

Do not feed only the Developer Agent's explanation to Reviewer. Provide direct repository evidence.

---

# 39. Decision Points

## Decision Point 1

Should the Reviewer Agent be allowed to modify code?

Discuss:

* read-only Reviewer
* Reviewer with patch suggestions
* Reviewer transitioning explicitly into Developer role

Preferred enterprise pattern:

Keep role authority explicit.

## Decision Point 2

When does a finding block progression?

Factors:

* severity
* category
* risk policy
* affected environment

## Decision Point 3

Should review occur before or after deterministic gates?

Discuss possible patterns:

```text
Developer → Reviewer → Gates
```

versus:

```text
Developer → Fast Gates → Reviewer → Full Gates
```

## Decision Point 4

Should the same model instance perform Developer and Reviewer roles?

Discuss:

* logical role separation
* fresh context
* independent execution
* different models where appropriate

## Decision Point 5

When should humans enter the review loop?

Define risk thresholds.

---

# 40. Exercises

## Exercise 1 — Define the Reviewer Role

Create:

```text
harness/roles/reviewer.md
```

Include:

* responsibilities
* authority
* prohibited actions
* required inputs
* required output

## Exercise 2 — Review a Fleet Booking Endpoint

Review a sample implementation for:

* API contract
* authorization
* architecture
* tests

Produce structured findings.

## Exercise 3 — Create a Severity Model

Define:

* Critical
* High
* Medium
* Low
* Informational

Specify blocking policy.

## Exercise 4 — Review an Event Contract

Compare:

```text
FleetBookingCreated v1
FleetBookingCreated v2
```

Identify breaking changes.

## Exercise 5 — Build the Review Loop

Design:

```text
Developer
→ Reviewer
→ Correction
→ Reviewer
→ Gates
→ Validator
```

## Exercise 6 — Architecture Review

Identify violations in a deliberately incorrect implementation where:

```text
API → Infrastructure → Domain
```

bypasses the intended Application layer.

## Exercise 7 — Security Review

Identify tenant-isolation weaknesses in corporate fleet booking.

## Exercise 8 — Reviewer Escalation

Write escalation output for an ambiguous corporate booking cancellation policy.

---

# 41. Interview Questions

Include questions such as:

1. What is the primary responsibility of a Reviewer Agent?
2. Why should the Reviewer Agent be independent of the Developer Agent?
3. How does a Reviewer Agent differ from a Validator Agent?
4. How does semantic review differ from deterministic validation?
5. What inputs should a Reviewer Agent receive?
6. Why are Instructions important during review?
7. How should acceptance criteria be used during review?
8. What makes a review finding evidence-based?
9. What information should a finding contain?
10. How should finding severity be determined?
11. What is the difference between request changes and reject?
12. When should a Reviewer Agent escalate?
13. How should API compatibility be reviewed?
14. How should event contracts be reviewed?
15. What persistence concerns should Reviewer inspect?
16. What security concerns cannot be delegated entirely to deterministic tools?
17. Why does passing unit tests not imply review acceptance?
18. How can AI review loops become harmful?
19. What review data should the Harness retain?
20. How can review history support a self-learning Harness?
21. Can Reviewer Agent approve production deployment?
22. Should Reviewer Agent be allowed to change code?
23. How would you design role separation in an enterprise Harness?
24. How can repeated Reviewer findings influence future Skills or Instructions?

---

# 42. Chapter Summary

Summarize key principles:

* Reviewer Agent is an independent implementation-review role.
* It evaluates more than compilation and test success.
* Review must be grounded in:

  * Instructions
  * acceptance criteria
  * Repository Intelligence
  * architecture
  * implementation evidence
* Reviewer examines:

  * architecture
  * code quality
  * contracts
  * persistence
  * security
  * observability
  * tests
  * configuration
  * dependencies
* Findings must be evidence-based and severity-classified.
* Reviewer may accept, reject, request changes, or escalate.
* Reviewer does not replace deterministic gates.
* Reviewer does not replace Validator Agent.
* Reviewer does not replace Evaluator Agent.
* Reviewer does not replace human approval.
* Review corrections should form controlled, auditable iterations.
* Review history becomes valuable input for later Metrics and Self-Learning Harness chapters.

---

# 43. Further Reading

Organize future references around:

* code review practices
* software architecture review
* secure code review
* Clean Architecture
* DDD
* API compatibility
* event schema evolution
* software supply-chain security
* automated testing
* AI-assisted software review
* Claude Code documentation
* GitHub Copilot documentation
* OpenAI Codex documentation

Do not make the vendor material the foundation of the chapter; use it only to support platform-specific implementation sections.

---

# 44. Chapter 16 Cross-Reference Map

| Chapter                            | Relationship to Chapter 16                                                |
| ---------------------------------- | ------------------------------------------------------------------------- |
| Chapter 5 — Repository Discovery   | Reviewer requires evidence-based repository understanding                 |
| Chapter 6 — Instructions           | Reviewer checks implementation compliance                                 |
| Chapter 7 — Skills                 | Reviewer verifies whether implementation follows governed workflows       |
| Chapter 8 — Prompts                | Original task defines review context                                      |
| Chapter 9 — Roles                  | Establishes Reviewer responsibility and authority                         |
| Chapter 10 — Steering Notes        | Provides current priorities, risks, and temporary constraints             |
| Chapter 11 — Knowledge Sources     | Supplies architectural and business evidence                              |
| Chapter 12 — What Is a Harness?    | Places review within automated AI Engineering                             |
| Chapter 13 — Harness Architecture  | Defines orchestration, state, gates, and handoffs                         |
| Chapter 14 — Lead Agent            | Lead assigns and coordinates review                                       |
| Chapter 15 — Developer Agent       | Produces implementation submitted for independent review                  |
| Chapter 17 — Validator Agent       | Confirms required deterministic validation evidence                       |
| Chapter 18 — Evaluator Agent       | Evaluates broader outcome and quality criteria                            |
| Chapter 20 — Metrics               | Uses review findings and iteration data                                   |
| Chapter 39 — Learning from Reviews | Learns from recurring review outcomes without silently changing standards |

---

**Chapter 16 outline is complete.**
