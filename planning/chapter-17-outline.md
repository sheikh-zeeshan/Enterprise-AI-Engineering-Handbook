# Chapter 17 — Validator Agent  

## Detailed Chapter Outline

**Part:** Part III — Harness Engineering  
**Target file:** `book/17-validator-agent.md`

Chapter 17 builds directly on the Harness architecture and role boundaries established in Chapters 12–16. It treats the Validator Agent as an evidence-driven engineering role whose responsibility is to determine whether implementation outputs satisfy objective, repeatable, machine-verifiable conditions. This remains consistent with the handbook’s established separation between Developer, Reviewer, Validator, Evaluator, and human approval responsibilities. AI development handbook

---

## 1. Story-Driven Opening

### Scenario: Corporate Fleet Booking Change Reaches Validation

- Continue the Alpha Car Detailing corporate fleet booking scenario.
- The Developer Agent has completed an implementation requested by the Lead Agent.
- The Reviewer Agent has examined:
  - implementation quality,
  - architectural alignment,
  - maintainability,
  - Instructions compliance,
  - API and event contract implications,
  - security concerns.
- The Reviewer Agent finds no blocking design concerns and hands the implementation forward.
- The change must now prove that it actually satisfies objective engineering conditions.
- Introduce questions such as:
  - Does the solution compile?
  - Do all required tests pass?
  - Are architectural dependency rules still satisfied?
  - Is the API contract valid?
  - Is the event schema compatible?
  - Can the database migration execute correctly?
  - Are required security checks clean?
  - Is configuration valid?
- Show why review confidence alone cannot answer these questions.
- Introduce the Validator Agent as the role responsible for obtaining deterministic evidence rather than subjective confidence.

### Opening principle

> The Reviewer Agent asks whether the implementation appears correct and appropriate.  
> The Validator Agent proves whether defined engineering conditions actually pass.

### Initial workflow

```text
Developer
    ↓
Reviewer
    ↓
Validator
    ↓
Deterministic Checks
    ↓
Pass / Fail
    ↓
Evaluator
```

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define the Validator Agent.
- Explain why validation is separate from review and evaluation.
- Identify objective and deterministic validation gates.
- Design validation pipelines for enterprise repositories.
- Distinguish:
  - build validation,
  - test validation,
  - architecture validation,
  - static analysis,
  - security validation,
  - contract validation,
  - database validation,
  - configuration validation,
  - infrastructure validation.
- Define evidence requirements for validation results.
- Model pass, fail, blocked, and inconclusive outcomes.
- Design correction and retry loops.
- Define the Validator Agent’s authority and limits.
- Establish safe handoffs between:
  - Developer Agent,
  - Reviewer Agent,
  - Validator Agent,
  - Evaluator Agent,
  - human approvers.
- Implement Validator Agent behavior inside an enterprise Harness.
- Understand Claude Code, GitHub Copilot, and OpenAI Codex approaches to validation.

---

# 3. Background

## 3.1 Why Enterprise AI Development Requires Validation

- AI-generated implementation can appear convincing while still failing mechanically.
- Examples:
  - compilation failures,
  - failing tests,
  - broken dependency rules,
  - incompatible API changes,
  - schema violations,
  - invalid migrations,
  - unsafe configuration,
  - infrastructure deployment failures.
- Human-readable plausibility is not proof.
- Reviewer judgment and deterministic evidence solve different problems.

## 3.2 From AI-Generated Code to Engineering Evidence

Describe the transition:

```text
Generated implementation
        ↓
Engineering review
        ↓
Executable validation
        ↓
Evidence
        ↓
Decision
```

Explain that enterprise AI Engineering requires claims to be backed by evidence.

## 3.3 Validation as a Harness Capability

Relate Chapter 17 to the established Harness philosophy:

```text
Prompt
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
```

Emphasize:

- the Harness orchestrates validation;
- validation tools execute deterministic checks;
- the Validator Agent interprets and records those results;
- validation does not imply production approval.

---

# 4. What Is a Validator Agent?

## 4.1 Definition

Define the Validator Agent as:

> The Harness role responsible for verifying that implementation outputs satisfy defined, objective, repeatable, and testable engineering conditions and for producing evidence-backed validation results.

## 4.2 Primary Purpose

The Validator Agent answers:

> **Did the implementation satisfy the required deterministic engineering gates?**

## 4.3 Inputs

Typical inputs:

- implementation changes,
- repository state,
- applicable Instructions,
- acceptance criteria,
- Reviewer Agent findings,
- build commands,
- test suites,
- architecture rules,
- static-analysis configuration,
- security scanners,
- API specifications,
- event schemas,
- database migrations,
- infrastructure definitions,
- deployment manifests,
- validation policy.

## 4.4 Outputs

Typical outputs:

- validation status,
- gate-by-gate results,
- commands executed,
- tool outputs,
- test summaries,
- failures,
- warnings,
- evidence references,
- blocked checks,
- recommended correction path,
- handoff decision.

---

# 5. Validator Agent Responsibilities

## 5.1 Execute Required Validation Gates

The Validator Agent coordinates or invokes deterministic checks required by:

- repository Instructions,
- task acceptance criteria,
- Harness policy,
- architecture requirements,
- security requirements,
- CI/CD standards.

## 5.2 Collect Evidence

Evidence should include:

- command executed,
- working directory,
- tool/version where relevant,
- timestamp,
- exit code,
- result summary,
- output artifact,
- failure details.

## 5.3 Classify Results

Classify individual gates and overall validation.

Possible statuses:

```text
PASS
FAIL
BLOCKED
INCONCLUSIVE
```

## 5.4 Prevent Unsupported Claims

The Validator Agent must not claim:

> “All tests passed.”

unless the required tests were actually executed successfully.

## 5.5 Produce Machine- and Human-Readable Results

Outputs should be useful to:

- Developer Agent,
- Evaluator Agent,
- Lead Agent,
- CI/CD systems,
- dashboards,
- audit systems,
- human reviewers.

---

# 6. Authority and Limits

## 6.1 What the Validator Agent May Do

Depending on Harness policy:

- execute builds,
- execute test suites,
- run linters,
- run static-analysis tools,
- run architecture tests,
- validate contracts,
- inspect migrations,
- validate configuration,
- execute infrastructure validation commands,
- run approved security tools,
- collect artifacts,
- classify failures,
- request correction.

## 6.2 What the Validator Agent Must Not Do

The Validator Agent should not independently:

- redesign architecture,
- approve its own code modifications,
- replace Reviewer Agent judgment,
- determine whether business behavior is desirable,
- waive failed mandatory gates,
- change acceptance criteria,
- silently alter Instructions,
- approve production deployment,
- replace security or compliance approval,
- replace human approval.

## 6.3 Validator as Evidence Authority, Not Governance Authority

Distinguish:

```text
Validator:
"This gate passed."

Human / Policy:
"This gate is sufficient for approval."
```

---

# 7. Validator Versus Reviewer

## 7.1 Reviewer Agent

Reviewer concerns include:

- maintainability,
- architecture alignment,
- code clarity,
- design quality,
- security concerns,
- contract impact,
- compliance with Instructions.

## 7.2 Validator Agent

Validator concerns include:

- does it compile?
- do required tests pass?
- do architecture tests pass?
- does static analysis pass?
- do contract checks pass?
- do migrations validate?
- do security scans meet thresholds?

## 7.3 Comparison

| Reviewer Agent | Validator Agent |
| --- | --- |
| Judgment-oriented | Evidence-oriented |
| Examines implementation quality | Executes objective checks |
| May identify architectural concerns | Runs architecture tests |
| Reviews test quality | Executes test suites |
| Reviews security implications | Runs deterministic security checks |
| Reviews contract impact | Validates schemas/specifications |
| Produces findings | Produces gate results |

## 7.4 Why Both Are Required

Use an example where:

- code is clean and well structured,
- Reviewer approves,
- but integration tests fail.

Then another where:

- all tests pass,
- but Reviewer identifies serious architecture debt.

Conclusion:

```text
Review success ≠ Validation success
Validation success ≠ Review success
```

---

# 8. Validator Versus Evaluator

## 8.1 Validator Question

> Did the implementation satisfy defined engineering checks?

## 8.2 Evaluator Question

> Did the completed change achieve the intended task outcome and acceptance criteria at the required quality level?

## 8.3 Example

Corporate fleet booking:

Validator verifies:

- solution builds,
- tests pass,
- API specification validates,
- architecture rules pass,
- database migration is valid.

Evaluator determines:

- whether the delivered fleet booking behavior actually solves the requested business scenario,
- whether acceptance criteria are genuinely fulfilled,
- whether the implementation is complete enough for the requested outcome.

## 8.4 Boundary

```text
Validator → verifies conditions
Evaluator → assesses outcome
```

---

# 9. Deterministic Validation

## 9.1 Definition

A deterministic validation check should:

- have clearly defined input,
- follow repeatable execution,
- produce reproducible evidence,
- have known success/failure criteria.

## 9.2 Examples

- `dotnet build`
- `dotnet test`
- architecture tests,
- OpenAPI validation,
- JSON Schema validation,
- migration checks,
- dependency scanning,
- linting,
- policy validation,
- Terraform validation.

## 9.3 Deterministic Does Not Mean Perfect

Explain limitations:

- tests may be incomplete;
- security scanners may miss vulnerabilities;
- architecture rules may encode incorrect assumptions;
- contract validators verify structure, not business correctness.

Therefore:

> Deterministic evidence strengthens assurance but does not eliminate engineering judgment.

---

# 10. Validator Agent Responsibility Boundary

### Diagram

```text
                 Instructions
                      │
              Acceptance Criteria
                      │
                      ▼
              ┌─────────────────┐
              │ Validator Agent │
              └────────┬────────┘
                       │
        ┌──────────────┼───────────────┐
        │              │               │
        ▼              ▼               ▼
      Build           Tests          Contracts
        │              │               │
        ▼              ▼               ▼
 Architecture      Security        Configuration
     Checks          Checks            Checks
        │              │               │
        └──────────────┼───────────────┘
                       ▼
                Validation Evidence
                       │
                       ▼
              Pass / Fail / Blocked
                       │
                       ▼
                  Evaluator
```

### Boundary discussion

Inside Validator authority:

- execution,
- verification,
- evidence,
- classification.

Outside Validator authority:

- architecture redesign,
- subjective code approval,
- business acceptance,
- governance waiver,
- production authorization.

---

# 11. Validation Pipeline

## 11.1 Pipeline Design

Introduce a typical enterprise validation sequence:

```text
Repository State
      ↓
Restore Dependencies
      ↓
Build
      ↓
Static Analysis
      ↓
Unit Tests
      ↓
Architecture Tests
      ↓
Integration Tests
      ↓
Contract Checks
      ↓
Migration Checks
      ↓
Security Checks
      ↓
Configuration Checks
      ↓
Infrastructure Checks
      ↓
Validation Result
```

## 11.2 Fail-Fast Versus Full Evidence Collection

Discuss two approaches.

### Fail-fast

Stop after mandatory prerequisite failure.

Example:

```text
Build fails
   ↓
Tests cannot execute
```

### Full collection

Continue independent checks where safe.

Example:

- build fails,
- but static configuration scan can still run.

## 11.3 Gate Dependencies

Model gate relationships explicitly.

---

# 12. Build Validation

## 12.1 Objective

Verify that required projects compile successfully.

## 12.2 Alpha Car Detailing Example

Possible command:

```bash
dotnet build AlphaCarDetailing.sln --configuration Release
```

## 12.3 Validation Evidence

Capture:

- exit code,
- warnings,
- errors,
- project failures,
- compiler output.

## 12.4 Warning Policies

Discuss:

- warnings allowed,
- warnings treated as errors,
- baseline warnings,
- newly introduced warnings.

## 12.5 Build Failure Classification

Examples:

- compilation failure,
- dependency resolution failure,
- unavailable package source,
- environment failure.

---

# 13. Unit and Integration Tests

## 13.1 Unit-Test Validation

Validate domain/application behavior such as:

- booking rules,
- vehicle eligibility,
- fleet-account restrictions,
- booking-time constraints.

## 13.2 Integration-Test Validation

Examples:

- SQL persistence,
- API integration,
- Redis behavior,
- event publication,
- downstream service integration.

## 13.3 Test Execution Evidence

Capture:

- total tests,
- passed,
- failed,
- skipped,
- duration,
- failing test names,
- artifacts.

## 13.4 Test Failures Versus Infrastructure Failures

Distinguish:

```text
Assertion failure → FAIL

Database unavailable → BLOCKED
```

---

# 14. Architecture Tests

## 14.1 Purpose

Turn important architectural constraints into executable rules.

## 14.2 Alpha Car Detailing Examples

Validate that:

- Domain does not depend on Infrastructure.
- Application does not depend on API.
- Infrastructure implements interfaces defined inward.
- Booking domain logic does not directly reference Event Hub SDKs.
- Cross-service database access is prohibited.

## 14.3 Benefits

Architecture tests transform architectural expectations from documentation into repeatable validation.

## 14.4 Architectural Test Failure

Example:

```text
Booking.Application
      ↓
direct reference
      ↓
Booking.Infrastructure
```

Result:

```text
Architecture Gate: FAIL
```

---

# 15. Static Analysis

## 15.1 Purpose

Use static tooling to identify mechanically detectable defects.

## 15.2 Categories

- compiler analyzers,
- style analyzers,
- code-quality analyzers,
- nullable-reference checks,
- dead code,
- dependency rules,
- complexity thresholds.

## 15.3 Policy Thresholds

Discuss:

```text
Error      → fail
Critical   → fail
Warning    → policy-dependent
Info       → evidence only
```

## 15.4 Existing Technical Debt

Avoid treating thousands of historical warnings as new AI failures.

Possible approaches:

- baseline,
- changed-code analysis,
- differential validation.

---

# 16. Security Checks

## 16.1 Deterministic Security Gates

Examples:

- dependency vulnerability scanning,
- secret scanning,
- container scanning,
- static application-security analysis,
- policy checks.

## 16.2 Validator Versus Security Reviewer

Security Reviewer:

- reasons about threat exposure.

Validator:

- executes required security gates.

## 16.3 Severity Thresholds

Example policy:

```text
Critical vulnerability → FAIL
High vulnerability     → policy-dependent
Medium                  → report
```

## 16.4 False Positives

A Validator Agent should not silently suppress tool findings.

---

# 17. API Contract Validation

## 17.1 Corporate Fleet Booking API

Example endpoint:

```text
POST /api/fleet-bookings
```

## 17.2 Validate

- OpenAPI syntax,
- request schema,
- response schema,
- required properties,
- status codes,
- backwards compatibility where required.

## 17.3 Breaking-Change Detection

Examples:

- required property added,
- property removed,
- response type changed,
- endpoint removed.

## 17.4 Reviewer Versus Validator

Reviewer:

> Is this contract change appropriate?

Validator:

> Does this contract violate the compatibility policy?

---

# 18. Event Contract Validation

## 18.1 Example Event

```text
CorporateFleetBookingCreated
```

## 18.2 Validate

- schema syntax,
- required fields,
- event type,
- schema version,
- field compatibility,
- serialization.

## 18.3 Example Envelope

```text
EventId
CorrelationId
EventType
SchemaName
SchemaVersion
OccurredUtc
Data
```

## 18.4 Compatibility

Discuss:

- additive changes,
- removed fields,
- changed types,
- renamed fields,
- incompatible version changes.

## 18.5 Schema Registry Integration

Describe Validator Agent interaction with approved schema-validation tooling.

---

# 19. Database Migration Validation

## 19.1 Purpose

Database changes require validation beyond code compilation.

## 19.2 Validate

- migration creation,
- migration ordering,
- generated SQL,
- migration execution,
- rollback strategy where applicable,
- schema compatibility.

## 19.3 Alpha Car Detailing Example

New tables/columns supporting corporate fleet booking.

## 19.4 Dangerous Changes

Examples:

- dropped column,
- narrowing data type,
- unexpected nullable change,
- table rename,
- destructive index operation.

## 19.5 Production Safety Boundary

Successful migration validation does not authorize production execution.

---

# 20. Configuration Validation

## 20.1 Validate Configuration Structure

Examples:

- required settings,
- invalid values,
- environment-specific configuration,
- malformed JSON/YAML,
- duplicated configuration.

## 20.2 Secret Handling

Ensure validation confirms:

- secrets are not committed,
- secret references use approved mechanisms.

## 20.3 Alpha Car Detailing Example

Validate:

- booking database settings,
- Redis configuration,
- Event Hub references,
- feature flags.

---

# 21. Infrastructure Validation

## 21.1 Infrastructure-as-Code Checks

Examples:

- Terraform validation,
- Kubernetes manifest validation,
- Helm template validation,
- Docker build validation,
- Azure deployment validation.

## 21.2 Example Commands

Conceptually show:

```text
terraform validate
kubectl dry-run
helm lint
docker build
```

## 21.3 Environment Limitations

Differentiate:

```text
Manifest invalid → FAIL

Azure credentials unavailable → BLOCKED
```

---

# 22. Evidence Collection

## 22.1 Evidence as a First-Class Harness Artifact

A validation result must be traceable.

## 22.2 Suggested Evidence Model

```text
ValidationRun
 ├── ValidationRunId
 ├── TaskId
 ├── RepositoryCommit
 ├── StartedAt
 ├── CompletedAt
 ├── OverallStatus
 └── Gates[]
```

Each gate:

```text
ValidationGate
 ├── Name
 ├── Command
 ├── Status
 ├── ExitCode
 ├── Duration
 ├── Summary
 └── ArtifactReference
```

## 22.3 Why Evidence Matters

Supports:

- auditability,
- debugging,
- Evaluator decisions,
- metrics,
- governance,
- learning systems,
- human approval.

---

# 23. Validation Results

## 23.1 Structured Result

Example:

```yaml
validation:
  status: FAIL
  gates:
    build: PASS
    unit_tests: PASS
    architecture_tests: FAIL
    integration_tests: PASS
    api_contract: PASS
    security: PASS
```

## 23.2 Evidence Over Narrative

Avoid:

> “Everything mostly looks good.”

Prefer:

```text
Build: PASS
Unit Tests: 184/184 PASS
Architecture Tests: 1 FAIL
Integration Tests: 36/36 PASS
API Contract: PASS
Security Scan: PASS
Overall: FAIL
```

---

# 24. Pass, Fail, Blocked, and Inconclusive Outcomes

## 24.1 PASS

All mandatory gates successfully executed and satisfied.

## 24.2 FAIL

At least one required gate executed and failed.

## 24.3 BLOCKED

A required check could not execute due to an external prerequisite.

Examples:

- environment unavailable,
- credential unavailable,
- package feed unavailable,
- required service unavailable.

## 24.4 INCONCLUSIVE

Evidence is insufficient to determine a trustworthy result.

Examples:

- partial test execution,
- corrupted output,
- ambiguous tool result,
- timeout without definitive state.

## 24.5 Why These States Matter

Do not collapse every non-pass result into failure.

---

# 25. Deterministic Gate Flow

### Diagram

```text
             Start Validation
                    │
                    ▼
             Can Gate Execute?
              /            \
            No              Yes
            │                │
            ▼                ▼
         BLOCKED          Run Gate
                             │
                    ┌────────┴────────┐
                    │                 │
                   Pass             Fail
                    │                 │
                    ▼                 ▼
                  PASS              FAIL
                                      │
                                      ▼
                             Correction Required
```

Add an inconclusive branch where output exists but cannot support a trustworthy determination.

---

# 26. Handling Failed Gates

## 26.1 Failures Must Be Actionable

Bad result:

```text
Tests failed.
```

Better result:

```text
Integration Gate: FAIL

Failed test:
CreateFleetBooking_WhenFleetAccountInactive_ReturnsForbidden

Expected:
403

Actual:
201
```

## 26.2 Preserve Evidence

Do not discard output after failure.

## 26.3 Identify Failure Ownership

Possible ownership:

- implementation defect,
- test defect,
- environment defect,
- configuration defect,
- specification conflict.

## 26.4 Do Not Automatically Rewrite Requirements

The Validator Agent reports evidence rather than redefining expected behavior.

---

# 27. Retry and Correction Loops

## 27.1 Retry Versus Correction

Retry is appropriate when failure may be transient.

Correction is required when deterministic evidence shows implementation defects.

## 27.2 Retry Policy

Examples:

- transient network failure,
- package-feed outage,
- external test dependency failure.

## 27.3 Prevent Infinite Loops

Define:

- retry limits,
- correction limits,
- failure thresholds,
- escalation rules.

## 27.4 Preserve Iteration History

Example:

```text
Validation Run 1
Architecture tests: FAIL

Validation Run 2
Architecture tests: PASS
Integration tests: FAIL

Validation Run 3
All required gates: PASS
```

---

# 28. Failure and Correction Loop

### Diagram

```text
Developer
    │
    ▼
Reviewer
    │
    ▼
Validator
    │
    ▼
Run Gates
    │
 ┌──┴───┐
 │      │
PASS   FAIL
 │      │
 ▼      ▼
Evaluator   Failure Evidence
               │
               ▼
          Developer Agent
               │
               ▼
            Correction
               │
               ▼
             Review
               │
               ▼
           Revalidation
```

Explain why corrected implementation may need Reviewer Agent re-entry before validation.

---

# 29. Handoff to Developer Agent

## 29.1 When Handoff Occurs

Examples:

- compilation failure,
- failed test,
- architecture violation,
- contract incompatibility,
- migration error.

## 29.2 Required Handoff Information

Provide:

- failing gate,
- exact evidence,
- affected artifact,
- relevant acceptance criterion,
- reproduction command,
- expected condition.

## 29.3 Validator Should Avoid Prescribing Large Redesigns

Correction implementation belongs primarily to the Developer Agent.

---

# 30. Handoff to Evaluator Agent

## 30.1 Preconditions

Normally:

```text
All mandatory validation gates = PASS
```

## 30.2 Evidence Package

Evaluator receives:

- implementation summary,
- Reviewer findings,
- validation result,
- evidence artifacts,
- known warnings,
- unresolved non-blocking concerns.

## 30.3 Validation Pass Does Not Guarantee Evaluation Pass

An implementation can be technically valid but still fail the requested outcome.

---

# 31. Validator Handoffs

### Diagram

```text
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
                  /      |       \
                 /       |        \
             FAIL      BLOCKED    PASS
              │           │         │
              ▼           ▼         ▼
          Developer   Lead/Human  Evaluator
              │           │         │
              └───────────┴─────────┘
```

---

# 32. Escalation

## 32.1 When Validator Should Escalate

Examples:

- contradictory acceptance criteria,
- missing mandatory tool,
- unavailable validation environment,
- repeated deterministic failure,
- security gate requiring human disposition,
- incompatible contract requiring architectural decision,
- test policy ambiguity.

## 32.2 Escalation Targets

Possible targets:

- Lead Agent,
- Architect,
- Security Reviewer,
- human technical lead,
- governance authority.

## 32.3 Escalation Is Not Failure

A correctly escalated unresolved condition may be the safest Validator outcome.

---

# 33. Human Approval Boundaries

## 33.1 What Automation Can Prove

Automation can determine that defined gates passed.

## 33.2 What Humans May Still Need to Approve

Examples:

- production deployment,
- security exceptions,
- breaking API contracts,
- destructive database changes,
- architecture exceptions,
- compliance-sensitive changes.

## 33.3 No Silent Waivers

The Harness must never turn:

```text
FAIL
```

into:

```text
PASS
```

because an AI Agent considers the failure unimportant.

This aligns with the handbook’s wider governance principle that AI systems must not silently change standards or approval requirements. AI development handbook

---

# 34. Validator Agent Inside the Harness

## 34.1 Harness Integration

Show:

```text
User / Work Item
       ↓
     Prompt
       ↓
   Lead Agent
       ↓
 Developer Agent
       ↓
 Reviewer Agent
       ↓
 Validator Agent
       ↓
 Validation Pipeline
       ↓
 Validation Evidence
       ↓
 Evaluator Agent
       ↓
 Human Approval
```

## 34.2 Validator Components

Possible Harness structure:

```text
harness/
├── roles/
│   └── validator.md
├── scripts/
│   └── validate.ps1
├── validation/
│   ├── build/
│   ├── tests/
│   ├── architecture/
│   ├── security/
│   ├── contracts/
│   └── infrastructure/
└── artifacts/
    └── validation-results/
```

## 34.3 Separation of Agent Reasoning and Tool Execution

Critical distinction:

```text
Validator Agent
     ↓
decides required gates
     ↓
Harness / Tools
     ↓
execute checks
     ↓
Validator Agent
     ↓
classifies evidence
```

---

# 35. Validator Agent Inside the Harness Diagram

```text
                     Harness
┌──────────────────────────────────────────────┐
│                                              │
│  Lead                                        │
│    ↓                                         │
│  Developer                                   │
│    ↓                                         │
│  Reviewer                                    │
│    ↓                                         │
│  ┌──────────────────────┐                    │
│  │   Validator Agent    │                    │
│  └──────────┬───────────┘                    │
│             │                                │
│   ┌─────────┼──────────────┐                 │
│   ▼         ▼              ▼                 │
│ Build      Tests        Contracts            │
│   │         │              │                 │
│   ▼         ▼              ▼                 │
│ Static   Security      Infrastructure        │
│   └─────────┬──────────────┘                 │
│             ▼                                │
│       Evidence Store                         │
│             │                                │
│             ▼                                │
│         Evaluator                            │
│                                              │
└──────────────────────────────────────────────┘
```

---

# 36. Hands-on Example

## 36.1 Scenario

Implement:

> Corporate fleet customers can create a booking for an approved vehicle at an available Alpha Car Detailing station.

## 36.2 Developer Output

Assume implementation includes:

- API endpoint,
- application service/command,
- domain validation,
- EF Core persistence,
- integration event,
- tests,
- configuration changes.

## 36.3 Reviewer Result

Reviewer finds implementation acceptable.

## 36.4 Validator Execution

Run:

1. repository restore,
2. Release build,
3. unit tests,
4. architecture tests,
5. integration tests,
6. OpenAPI validation,
7. event-schema validation,
8. migration validation,
9. static analysis,
10. security checks.

## 36.5 Example Failure

Architecture test discovers:

```text
Booking.Application
directly references
Azure.Messaging.EventHubs
```

Violation:

Infrastructure SDK dependency has leaked into Application.

Overall result:

```text
FAIL
```

## 36.6 Correction

Developer introduces appropriate abstraction and moves Event Hub implementation into Infrastructure.

## 36.7 Revalidation

All gates pass.

## 36.8 Evaluator Handoff

Validator passes the evidence package to Evaluator.

---

# 37. Claude Example

## 37.1 Validator Role Instruction

Show a conceptual validator role file defining:

- responsibility,
- required evidence,
- allowed tools,
- required gates,
- status model,
- prohibition against unsupported success claims.

## 37.2 Claude Code Validation Workflow

Example pattern:

```text
Read Instructions
      ↓
Read Reviewer handoff
      ↓
Determine required gates
      ↓
Execute approved validation commands
      ↓
Collect outputs
      ↓
Classify results
      ↓
Produce structured evidence
```

## 37.3 Example Validator Prompt

Conceptually include:

```text
Validate the corporate fleet booking implementation.

Run all validation gates required by repository Instructions
and acceptance criteria.

Do not modify implementation code.

Return PASS, FAIL, BLOCKED, or INCONCLUSIVE for each gate.

Include evidence for every conclusion.
```

## 37.4 Tool Permission Boundaries

Discuss limiting Validator permissions to:

- read repository,
- execute approved commands,
- write validation artifacts,

while avoiding unrestricted implementation modification.

## 37.5 Claude-Specific Enterprise Considerations

- repository Instructions,
- CLAUDE.md interaction,
- approved commands,
- tool execution,
- role isolation,
- structured output.

---

# 38. GitHub Copilot Comparison

Discuss where GitHub Copilot can participate in:

- test execution,
- build troubleshooting,
- repository analysis,
- review workflows,
- agent-assisted validation.

Compare with the Harness architecture rather than presenting Copilot as equivalent to the Validator role.

Topics:

- agent workflow capabilities,
- IDE-centric validation,
- CI integration,
- repository instructions,
- deterministic checks remain external engineering tools.

Core point:

> Regardless of AI platform, the validation authority comes from the executed engineering checks, not from the model claiming success.

---

# 39. OpenAI Codex Comparison

Discuss:

- command execution,
- repository modification boundaries,
- test execution,
- validation evidence,
- automation possibilities,
- isolated Validator-style workflows.

Compare:

```text
Claude Code
GitHub Copilot
OpenAI Codex
```

against vendor-neutral requirements:

- deterministic execution,
- evidence capture,
- role separation,
- reproducibility,
- auditability.

---

# 40. Best Practices

Cover practices such as:

- Make every mandatory gate explicit.
- Prefer deterministic checks over model judgment.
- Capture evidence for every result.
- Separate Validator from Developer authority.
- Treat blocked checks differently from failed checks.
- Revalidate after implementation corrections.
- Keep validation commands version-controlled.
- Validate changed contracts explicitly.
- Turn important architecture rules into executable tests.
- Use stable machine-readable output where possible.
- Preserve validation history.
- Run the same important gates in CI/CD.
- Define severity thresholds centrally.
- Avoid hidden waivers.
- Keep validation reproducible locally and in automation.

---

# 41. Anti-patterns

## 41.1 AI Says “Looks Good”

Using an LLM statement as proof.

## 41.2 Developer Self-Validation Only

Developer implements and declares success without independent validation.

## 41.3 Reviewer as Test Runner

Mixing Reviewer and Validator responsibilities without clear boundaries.

## 41.4 Tests Are the Only Gate

Ignoring:

- architecture,
- contracts,
- security,
- migrations,
- configuration,
- infrastructure.

## 41.5 Green Build Equals Production Ready

Treating compilation success as full validation.

## 41.6 Silent Gate Skipping

Not running a required check but still returning PASS.

## 41.7 Retrying Real Failures

Repeatedly rerunning a deterministic failing test instead of correcting the defect.

## 41.8 Infinite Agent Correction Loops

Validator and Developer endlessly exchange work.

## 41.9 Validation by Log Interpretation Alone

Ignoring exit codes and structured evidence.

## 41.10 Validator Changes the Code

Destroying role independence by allowing Validator to fix implementation and validate its own change.

---

# 42. Architect’s Notes

Include guidance such as:

### Architect’s Note — Automate Architecture

If an architectural constraint matters enough to appear repeatedly in reviews, consider expressing it as an executable architecture test.

### Architect’s Note — Validation Is a Contract

Define the repository’s minimum validation contract so that humans and AI Agents use the same gates.

### Architect’s Note — Validation Layers

Use layers:

```text
Fast local gates
      ↓
Repository gates
      ↓
Integration gates
      ↓
Environment gates
      ↓
Release gates
```

### Architect’s Note — Evidence Enables Governance

The most valuable Validator output is often not PASS or FAIL but the evidence supporting that result.

---

# 43. Enterprise Tips

Potential callouts:

### Enterprise Tip — Maintain a Validation Catalog

Document each gate with:

- owner,
- purpose,
- command,
- expected result,
- failure policy.

### Enterprise Tip — Differential Validation

For large repositories, validate changed scope where safe while retaining mandatory solution-wide gates.

### Enterprise Tip — Align Harness and CI/CD

Avoid a situation where AI Harness validation and CI validation use different rules.

### Enterprise Tip — Preserve Artifacts

Store validation results using work-item or execution identifiers.

### Enterprise Tip — Classify Environmental Failure

Do not send Developers chasing defects caused by infrastructure outages.

---

# 44. Decision Points

## Decision Point 1

Should a gate be:

- mandatory,
- advisory,
- informational?

## Decision Point 2

Should validation stop after the first failed prerequisite or collect all independent results?

## Decision Point 3

Which validation checks run:

- locally,
- inside the Harness,
- in CI,
- before deployment?

## Decision Point 4

Which failures may be retried automatically?

## Decision Point 5

When should repeated failure escalate to humans?

## Decision Point 6

Which security findings block progress?

## Decision Point 7

Which contract changes require explicit human approval?

## Decision Point 8

Should the Validator Agent have permission to modify files?

Recommended enterprise default:

> Validation artifacts only, not implementation files.

---

# 45. Exercises

## Exercise 1 — Identify Deterministic Gates

Given a corporate fleet booking change, identify required gates.

## Exercise 2 — Reviewer or Validator?

Classify ten example responsibilities between:

- Reviewer,
- Validator,
- Evaluator.

## Exercise 3 — Define Validation Outcomes

Create rules for:

```text
PASS
FAIL
BLOCKED
INCONCLUSIVE
```

## Exercise 4 — Architecture Validation

Create architecture rules for the Alpha Car Detailing Clean Architecture solution.

## Exercise 5 — Contract Validation

Define validation gates for:

```text
CorporateFleetBookingCreated
```

## Exercise 6 — Failure Handoff

Create a structured handoff from Validator to Developer after a failed integration test.

## Exercise 7 — Retry Policy

Determine which failures are safe to retry automatically.

## Exercise 8 — Validation Pipeline

Design a complete validation pipeline for the Booking microservice.

## Exercise 9 — Evidence Schema

Design a JSON structure for storing validation results.

## Exercise 10 — Human Boundary

Identify which successful automated validations still require human approval.

---

# 46. Interview Questions

Include questions progressing from foundational to architectural.

### Foundational

1. What is a Validator Agent?
2. Why is validation different from review?
3. What makes a validation check deterministic?
4. What is validation evidence?
5. What is the difference between FAIL and BLOCKED?

### Intermediate

1. How should a Validator Agent handle failing tests?
2. Why should validation results be machine-readable?
3. What should happen after a correction?
4. How are architecture tests different from architecture review?
5. How should contract validation be performed?

### Advanced

 1. How would you design validation for a large microservice repository?
 2. When should a Harness use fail-fast validation?
 3. How do you prevent endless Developer–Validator loops?
 4. How should transient failures be classified?
 5. How should validation policy interact with CI/CD?

### Architect-Level

 1. Where should validation policy be defined in an enterprise?
 2. How do you ensure AI Agents cannot bypass required gates?
 3. What validation evidence should be retained for auditability?
 4. How would you validate breaking event-contract changes?
 5. How does Validator Agent evidence support a future self-learning Harness?
 6. Why should a Validator Agent not approve production deployment?
 7. How would you separate Reviewer, Validator, Evaluator, and human approval in a regulated environment?

---

# 47. Chapter Summary

Summarize the chapter around the core principles:

- The Validator Agent verifies objective engineering conditions.
- Validation must be evidence-based.
- The Validator Agent does not replace the Reviewer Agent.
- The Validator Agent does not replace the Evaluator Agent.
- The Validator Agent does not replace human approval.
- Builds, tests, architecture rules, static analysis, security checks, contract validation, migrations, configuration, and infrastructure checks form the validation pipeline.
- Validation outcomes should distinguish:
  - PASS,
  - FAIL,
  - BLOCKED,
  - INCONCLUSIVE.
- Failed gates trigger controlled correction loops.
- Corrected implementation must be revalidated.
- Validation results should be repeatable, traceable, and auditable.
- A successful Validator handoff gives the Evaluator Agent trustworthy engineering evidence.
- Enterprise Harnesses should make deterministic validation a mandatory boundary between implementation and outcome evaluation.

Close with the transition:

> Chapter 16 established independent engineering review. Chapter 17 adds deterministic proof. Chapter 18 moves beyond mechanical correctness to the Evaluator Agent, which determines whether the completed implementation actually achieves the intended engineering and business outcome.

---

# 48. Further Reading

Organize the eventual section around:

- deterministic software validation,
- automated testing,
- architecture fitness functions,
- static code analysis,
- software supply-chain security,
- API specification validation,
- schema compatibility,
- database migration safety,
- infrastructure-as-code validation,
- CI/CD quality gates,
- policy-as-code,
- software verification,
- auditability and evidence,
- Claude Code documentation,
- GitHub Copilot documentation,
- OpenAI Codex documentation.

Prefer official platform and tool documentation when the chapter is written.

---

## Recommended Chapter 17 Diagram Set

The final chapter should contain at minimum these six diagrams:

1. **Validator Agent Responsibility Boundary**
2. **Validation Pipeline**
3. **Deterministic Gate Flow**
4. **Failure and Correction Loop**
5. **Validator Handoffs**
6. **Validator Agent Inside the Harness**

Together they should reinforce the central flow:

```text
Developer
    ↓
Reviewer
    ↓
Validator
    ↓
Deterministic Checks
    ↓
Pass / Fail
    ↓
Evaluator
```

**Chapter 17 outline is complete.**
