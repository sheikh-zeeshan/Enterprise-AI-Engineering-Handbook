# Chapter 34 — Compliance  

**Part V — Enterprise AI Governance**  
**Target file:** `book/34-compliance.md`

This outline follows the frozen handbook structure, enterprise writing standards, vendor-neutral principles, Claude-first implementation approach, and Alpha Car Detailing running example. The project handover confirms Compliance as Chapter 34 within Part V and requires the standard 19-section chapter template. AI development handbook AI development handbook

---

## 1. Story-Driven Opening — When “The Code Works” Is Not Enough

### Scenario: Alpha Car Detailing Government Fleet Enhancement

- Alpha Car Detailing wins a contract to service vehicles for government departments.
- The engineering team must enhance the fleet booking platform to:
  - store government vehicle information,
  - process authorized personnel data,
  - integrate with existing fleet systems,
  - generate operational reports,
  - retain records according to contractual requirements.
- A Developer Agent produces a technically correct implementation.
- Tests pass.
- Architecture checks pass.
- Security scanning reports no critical vulnerabilities.
- The Reviewer and Validator approve the implementation.
- However, several compliance questions remain:
  - Was customer data exposed to an AI model that was not approved for this contract?
  - Did the agent access production data?
  - Were required approvals obtained?
  - Was personal information unnecessarily included in prompts?
  - Were artifacts retained for the contractually required period?
  - Can the organization prove who approved the change?
  - Can the organization reconstruct which model, prompt, repository version, and tools produced the change?
  - Did the implementation satisfy government-specific access restrictions?
- Introduce the central lesson:

> A technically correct implementation can still be non-compliant.

### Core Principle

**Compliance must be designed into the AI engineering workflow through policies, controls, validation, approvals, traceability, and audit evidence rather than checked only after work is complete.**

### Chapter positioning

- Builds on:
  - Chapter 29 — AI Governance
  - Chapter 30 — Security
  - Chapter 31 — Human Approval
  - Chapter 32 — Audit Trails
  - Chapter 33 — Policies
- Establish that Chapter 34 connects these concepts into an enforceable compliance system.
- Compliance is not an isolated legal activity.
- It becomes part of the engineering lifecycle and Harness execution model.

---

# 2. Learning Objectives

By the end of this chapter, readers should be able to:

- Define compliance in enterprise AI engineering.
- Distinguish compliance from governance, policy, security, and audit.
- Identify regulatory, contractual, organizational, privacy, and security obligations.
- Translate obligations into engineering controls.
- Map policies to controls and controls to evidence.
- Define evidence required to demonstrate compliance.
- Design privacy controls for AI-assisted development.
- Restrict AI Agent, tool, repository, environment, and production access.
- Apply separation of duties.
- Determine where human approval is mandatory.
- Design compliance validation gates.
- Build traceability across prompts, agents, models, tools, code changes, approvals, and deployments.
- Define retention requirements for AI engineering artifacts.
- Manage compliance exceptions and waivers safely.
- Implement continuous compliance instead of periodic manual checking.
- Generate compliance reports from Harness evidence.
- Integrate compliance controls into an enterprise AI Harness.
- Recognize common compliance anti-patterns.

---

# 3. Background

## 3.1 Why Compliance Changes in AI-Assisted Engineering

- Traditional software development compliance.
- New AI-assisted development concerns:
  - prompts may contain sensitive data,
  - AI Agents may invoke tools autonomously,
  - models may be external services,
  - generated code may incorporate unapproved dependencies,
  - agents may navigate repositories autonomously,
  - automated workflows may make changes faster than manual governance processes can review,
  - tool calls may cross security boundaries,
  - evidence must include AI-specific execution context.
- Why conventional SDLC compliance controls remain necessary but may no longer be sufficient.

## 3.2 Compliance as an Engineering Concern

Explain that compliance cannot remain solely within:

- legal,
- risk,
- security,
- internal audit,
- management.

Engineering teams must translate obligations into executable controls.

Examples:

```text
Requirement:
Production data must not be submitted to external AI models.

Engineering control:
Prevent production data sources from being accessible to AI Agent environments.

Validation:
Scan prompt/tool inputs for prohibited data sources.

Evidence:
Model invocation log + data classification + access decision.
```

## 3.3 Compliance by Design

Introduce:

```text
Obligation
   ↓
Policy
   ↓
Control
   ↓
Implementation
   ↓
Validation
   ↓
Evidence
   ↓
Audit
```

Contrast:

```text
Build everything
   ↓
Finish project
   ↓
Ask compliance team to inspect it
```

versus:

```text
Compliance requirements
   ↓
Architecture
   ↓
Policies
   ↓
Controls
   ↓
Harness gates
   ↓
Evidence
   ↓
Continuous verification
```

## 3.4 Why AI Engineering Requires Stronger Evidence

Discuss evidence for:

- who initiated a task,
- what instruction governed it,
- which model was used,
- which tools were invoked,
- what repositories were accessed,
- what files were changed,
- what data was supplied to the model,
- which validations executed,
- who reviewed the change,
- who approved restricted operations,
- what was deployed,
- which policy version applied.

---

# 4. Concepts

## 4.1 What Compliance Means in AI Engineering

Define compliance as:

> The demonstrable satisfaction of applicable regulatory, contractual, organizational, security, privacy, and operational obligations through enforced controls and verifiable evidence.

Three important properties:

- **Applicable** — not every regulation applies to every system.
- **Enforced** — requirements must result in real controls.
- **Demonstrable** — compliance requires evidence.

---

## 4.2 Compliance versus Governance

### Governance answers

- Who owns AI engineering?
- Who defines policies?
- Who approves models?
- Who accepts risk?
- Who owns the Harness?

### Compliance answers

- Are applicable obligations being satisfied?
- Are required controls operating?
- Can the organization prove that they operated?

Simple distinction:

```text
Governance = who decides and oversees
Compliance = whether obligations are satisfied and provable
```

---

## 4.3 Compliance versus Policy

Explain:

```text
Obligation
   ↓
Policy
   ↓
Control
   ↓
Evidence
```

Example:

**Contractual obligation**

Government customer data cannot leave approved environments.

**Policy**

Restricted customer data must not be supplied to external AI services.

**Control**

AI gateway blocks restricted classifications.

**Evidence**

Gateway decision log identifies:

- request,
- classification,
- model,
- policy,
- decision,
- timestamp.

Policy describes expectations.

Compliance proves that expectations and external obligations were actually satisfied.

---

## 4.4 Compliance versus Security

Security controls may contribute to compliance, but:

```text
Security ≠ Compliance
```

Examples:

- encryption,
- authentication,
- authorization,
- vulnerability scanning,

may be security controls.

Compliance determines whether the appropriate controls exist and whether their operation can be demonstrated.

---

## 4.5 Compliance versus Audit

- Compliance is an ongoing state and process.
- Audit evaluates evidence about that state.
- Audit trails support compliance.
- An audit trail alone does not establish compliance.

---

# 5. Regulatory and Contractual Obligations

## 5.1 Sources of Compliance Requirements

Categorize:

### Regulatory

- privacy laws,
- data residency requirements,
- sector regulations,
- record-keeping requirements,
- cybersecurity requirements.

### Contractual

- customer contracts,
- government contracts,
- data processing agreements,
- confidentiality requirements,
- vendor agreements,
- service-level obligations.

### Organizational

- internal security standards,
- approved development tools,
- coding policies,
- segregation requirements,
- retention standards.

### Industry and assurance frameworks

Explain that organizations may map engineering controls to frameworks such as:

- ISO-based controls,
- SOC-related controls,
- NIST-aligned practices,
- industry-specific standards.

Keep treatment architecture-focused rather than turning the chapter into a regulation catalog.

---

## 5.2 Applicability

Not every requirement applies to every task.

Introduce a compliance context:

```yaml
customer: government-fleet
data_classification: restricted
environment: production
change_type: security-sensitive
required_controls:
  - human-approval
  - restricted-model
  - production-access-approval
  - audit-evidence
  - seven-year-retention
```

---

## 5.3 Compliance Profiles

Introduce reusable profiles:

```text
Standard Commercial
Government
Regulated Customer
Production Emergency
Security-Sensitive Change
Personal-Data Processing
```

Explain how the Harness can select controls based on the applicable profile.

---

# 6. Control Mapping

## 6.1 From Obligation to Control

Explain the mapping chain:

```text
Requirement
    ↓
Policy
    ↓
Control Objective
    ↓
Technical / Procedural Control
    ↓
Validation
    ↓
Evidence
```

### Example

```text
Requirement:
Only authorized personnel may change government pricing.

Policy:
Government pricing modifications require restricted repository access
and independent approval.

Controls:
- RBAC
- protected branches
- reviewer requirement
- production approval
- immutable audit logging
```

---

## 6.2 Types of Controls

### Preventive controls

Stop prohibited actions.

Examples:

- deny production access,
- block prohibited model,
- reject secret-containing prompts,
- branch protection.

### Detective controls

Identify undesirable activity.

Examples:

- audit analysis,
- unusual tool invocation detection,
- compliance scans.

### Corrective controls

Restore compliant state.

Examples:

- revoke access,
- rotate leaked credentials,
- rollback deployment,
- remove improperly retained data.

### Compensating controls

Used when the preferred control cannot be implemented.

---

## 6.3 Manual versus Automated Controls

Discuss:

- manual approvals,
- automated gates,
- policy-as-code,
- security controls,
- audit reviews.

Architecture principle:

> Automate controls when they are objective and deterministic; retain human decision-making where judgment, risk acceptance, or legal authority is required.

---

# 7. Evidence Requirements

## 7.1 Evidence Is a First-Class Artifact

Explain that enterprise compliance requires more than:

```text
"the pipeline passed"
```

Evidence should establish:

- what happened,
- under which policy,
- who or what performed it,
- what checks executed,
- what their results were,
- who approved exceptions,
- when it occurred.

---

## 7.2 Evidence Categories

### Task evidence

- request,
- goal,
- acceptance criteria,
- ticket/change identifier.

### AI execution evidence

- agent role,
- model provider,
- model/version,
- prompt version,
- instruction version,
- Skills invoked,
- tool calls.

### Repository evidence

- commit SHA,
- files changed,
- branch,
- pull request,
- code owners.

### Validation evidence

- build,
- tests,
- static analysis,
- architecture checks,
- security scans,
- compliance gates.

### Human evidence

- reviewer identity,
- approver identity,
- timestamps,
- approval reason.

### Deployment evidence

- artifact digest,
- environment,
- deployment approval,
- release identifier.

---

## 7.3 Evidence Quality

Evidence should be:

- attributable,
- timestamped,
- complete,
- tamper-resistant,
- retrievable,
- correlated,
- retained appropriately.

---

# 8. Policy-to-Control-to-Evidence Mapping

## Required Diagram — Policy-to-Control-to-Evidence Mapping

```text
Regulatory / Contractual Requirement
                │
                ▼
             Policy
                │
                ▼
        Control Objective
                │
       ┌────────┼────────┐
       ▼        ▼        ▼
    Access    Approval   Data
    Control   Control    Control
       │        │        │
       ▼        ▼        ▼
     Gate      Gate     Gate
       │        │        │
       └────────┼────────┘
                ▼
             Evidence
                │
                ▼
          Compliance Record
                │
                ▼
              Audit
```

Explain one obligation may require multiple controls, and one control may satisfy several obligations.

---

# 9. Data Privacy

## 9.1 Privacy Risks in AI Engineering

- personal data embedded in prompts,
- customer records copied into scratchpads,
- production logs sent to models,
- data retained by model providers,
- generated artifacts containing personal data,
- embeddings and vector stores,
- screenshots or diagnostic dumps,
- agent-generated summaries.

---

## 9.2 Data Classification

Recommended classes:

```text
Public
Internal
Confidential
Restricted
Regulated
```

Explain that classification should influence:

- which models may process the data,
- which tools may access it,
- where it may be stored,
- retention period,
- required approval.

---

## 9.3 Data Minimization

Core principle:

> Supply the AI Agent only the minimum data required for the task.

Examples:

Bad:

```text
Upload complete production customer database.
```

Better:

```text
Use anonymized schema + synthetic representative records.
```

---

## 9.4 Masking, Tokenization, and Synthetic Data

Discuss appropriate techniques.

Alpha Car Detailing example:

Instead of:

```text
Customer:
Name: John Smith
National ID: ...
Phone: ...
```

provide:

```text
CustomerId: synthetic-001
VehicleType: Sedan
FleetCategory: Government
```

---

## 9.5 Model Data Handling

Evaluate:

- training usage,
- retention,
- geographical processing,
- enterprise agreements,
- tenant isolation,
- logging,
- data deletion.

---

# 10. Security Controls

Connect Chapter 30 to compliance.

Controls may include:

- secret scanning,
- SAST,
- dependency scanning,
- container scanning,
- infrastructure scanning,
- SBOM generation,
- signing,
- provenance,
- least privilege,
- protected branches,
- encryption,
- approved package sources.

Explain that compliance requires evidence showing these controls were executed where required.

---

# 11. Access Control

## 11.1 Human Access

Apply:

- least privilege,
- role-based access control,
- just-in-time access,
- privileged identity management,
- periodic access reviews.

---

## 11.2 AI Agent Access

AI Agents must have explicit identities or attributable execution contexts.

Avoid:

```text
Agent inherits unrestricted developer credentials.
```

Prefer:

```text
Lead Agent
   → read repository

Developer Agent
   → scoped write workspace

Reviewer Agent
   → read-only

Validator Agent
   → validation environment

Deployment Agent
   → controlled release permissions
```

---

## 11.3 Tool-Level Access

Restrict:

- shell execution,
- database access,
- cloud CLI,
- secret stores,
- production logs,
- deployment systems,
- external web access.

---

# 12. Repository and Production Access

## 12.1 Repository Permissions

Differentiate:

- public repositories,
- internal repositories,
- confidential repositories,
- restricted repositories.

Agent access may depend on:

- repository classification,
- customer,
- task,
- role.

---

## 12.2 Production Access

Establish:

> AI Agents should not receive unrestricted production access merely because the executing human possesses it.

Discuss:

- read-only diagnostics,
- temporary credentials,
- approved commands,
- mediated tooling,
- audit logging,
- break-glass access.

---

## 12.3 Alpha Car Detailing Government Scenario

A production issue affects government fleet bookings.

Developer Agent needs diagnostic evidence.

Preferred flow:

```text
Developer Agent
      │
      ▼
Request Production Evidence
      │
      ▼
Approval / Policy Check
      │
      ▼
Controlled Diagnostic Tool
      │
      ▼
Sanitized Result
      │
      ▼
Agent Workspace
```

Not:

```text
Agent → unrestricted production SQL access
```

---

# 13. Human Approval

Connect with Chapter 31.

## 13.1 When Approval Becomes a Compliance Control

Examples:

- production deployment,
- restricted data access,
- security-sensitive changes,
- policy exceptions,
- model exceptions,
- customer-specific actions,
- destructive database operations.

---

## 13.2 Approval Evidence

Capture:

```yaml
approval:
  action: production-deployment
  approver: security-lead
  timestamp: ...
  policy: GOV-PROD-04
  reason: approved government booking hotfix
  artifact: commit-sha
```

---

# 14. Separation of Duties

## 14.1 Why Separation Matters

No single actor should be able to:

```text
create → approve → deploy → certify
```

a sensitive change without independent control.

---

## 14.2 Human Separation of Duties

Examples:

- developer cannot approve own production access,
- requester cannot approve own policy waiver,
- code author cannot provide sole security approval.

---

## 14.3 Agent Separation of Duties

Connect to earlier roles:

```text
Developer Agent
      ↓
Reviewer Agent
      ↓
Validator Agent
      ↓
Evaluator Agent
      ↓
Human Approval
```

Clarify:

Multiple AI Agent roles do not automatically satisfy legal or organizational independence requirements.

Where human independence is required, agent separation cannot replace it.

---

# 15. Audit Trails

Build on Chapter 32.

## 15.1 Minimum AI Engineering Audit Record

Record:

- run ID,
- user,
- agent,
- role,
- prompt,
- instruction version,
- Skill,
- model,
- tool calls,
- repository,
- commit,
- validation results,
- approval events,
- exceptions,
- deployment.

---

## 15.2 Correlation

Introduce a shared identifier:

```text
ComplianceCorrelationId
```

that connects:

```text
Ticket
  ↓
Harness Run
  ↓
Prompt
  ↓
Tool Calls
  ↓
Commit
  ↓
Pull Request
  ↓
Validation
  ↓
Approval
  ↓
Deployment
```

---

# 16. Retention

## 16.1 What May Need Retention

- prompts,
- run manifests,
- validation reports,
- approvals,
- pull requests,
- security reports,
- policy decisions,
- waiver records,
- deployment manifests.

---

## 16.2 Retention Is Not “Keep Everything Forever”

Discuss conflicting obligations:

- minimum retention,
- maximum privacy retention,
- contractual deletion,
- legal hold,
- storage cost,
- sensitive data exposure.

---

## 16.3 Retention Policy by Artifact

Example:

| Artifact | Example Retention |
| --- | ---: |
| Agent runtime scratchpad | short-lived |
| Build log | operational period |
| Compliance decision | long-lived |
| Approval evidence | contractual period |
| Security scan | audit-defined period |
| Production access record | security-defined period |

Make clear durations are organization/contract dependent.

---

## 16.4 Scratchpad Compliance

Reinforce:

> Scratchpad is temporary working state, not authoritative enterprise knowledge.

Compliance implications:

- no unnecessary regulated data,
- cleanup,
- promotion only through governed processes,
- defined retention,
- audit only where required.

---

# 17. Traceability

## 17.1 End-to-End Traceability

Explain desired chain:

```text
Business Requirement
      ↓
Compliance Requirement
      ↓
Policy
      ↓
Control
      ↓
Harness Gate
      ↓
Evidence
      ↓
Code Change
      ↓
Approval
      ↓
Deployment
```

---

## 17.2 Bidirectional Traceability

Auditors should be able to ask:

> Why does this control exist?

and trace upward.

Or:

> Show how requirement GOV-17 was satisfied.

and trace downward.

---

# 18. Tool and Model Usage Restrictions

## 18.1 Approved Model Registry

Maintain metadata:

```yaml
models:
  claude-enterprise:
    approved_for:
      - internal
      - confidential
    prohibited_for:
      - restricted-government-data

  internal-model:
    approved_for:
      - restricted
      - government
```

---

## 18.2 Approved Tool Registry

Classify tools by:

- network access,
- filesystem access,
- cloud access,
- data-handling capability,
- production capability.

---

## 18.3 Policy Decision Example

```text
Task
  ↓
Contains Restricted Data?
  ├── No → Approved enterprise model
  └── Yes
       ↓
Customer permits external processing?
       ├── Yes → Contract-approved model
       └── No → Internal approved model only
```

---

# 19. Compliance Validation Gates

## 19.1 Why Compliance Needs Deterministic Gates

Connect to the established gate philosophy.

Examples:

- prohibited model check,
- restricted repository check,
- missing approval check,
- branch protection check,
- artifact signing check,
- security scan check,
- dependency policy check,
- missing evidence check.

---

## 19.2 Gate Categories

```text
Pre-Execution Gates
Execution-Time Controls
Pre-Commit Gates
Pull Request Gates
Pre-Deployment Gates
Post-Deployment Compliance Checks
```

---

## 19.3 Example Gate Pipeline

```text
Task Submitted
     ↓
Compliance Profile Selected
     ↓
Data Classification Gate
     ↓
Model Authorization Gate
     ↓
Tool Authorization Gate
     ↓
Agent Execution
     ↓
Security Gate
     ↓
Evidence Completeness Gate
     ↓
Human Approval Gate
     ↓
Deployment Authorization
```

---

## 19.4 Gate Outcomes

Recommended states:

```text
PASS
FAIL
BLOCKED
REQUIRES_APPROVAL
WAIVER_REQUIRED
INCONCLUSIVE
```

Avoid vague:

```text
Looks compliant.
```

---

# 20. Exceptions and Waivers

## 20.1 Why Exceptions Exist

Examples:

- emergency production fix,
- unavailable approved tooling,
- legacy platform limitations,
- temporary business constraint.

---

## 20.2 Exception versus Silent Bypass

Critical principle:

> An approved exception is governed risk acceptance. A silent bypass is a control failure.

---

## Required Diagram — Exception Workflow

```text
Control Fails
     ↓
Can Requirement Be Satisfied Normally?
     ├── Yes → Remediate
     │
     └── No
          ↓
     Exception Request
          ↓
      Risk Assessment
          ↓
     Authorized Approver
          ↓
   ┌──────┴──────┐
   ▼             ▼
Reject         Approve
   │             │
   ▼             ▼
Block      Temporary Waiver
                 ↓
        Compensating Controls
                 ↓
          Expiration Date
                 ↓
             Re-review
```

---

## 20.3 Required Waiver Metadata

```yaml
waiver:
  control: MODEL-04
  reason: ...
  approved_by: ...
  approved_at: ...
  expires_at: ...
  compensating_controls:
    - ...
  ticket: ...
```

---

# 21. Continuous Compliance

## 21.1 Point-in-Time Compliance Is Insufficient

A system compliant today can drift tomorrow because of:

- dependency changes,
- policy changes,
- model updates,
- access changes,
- infrastructure changes,
- expiring waivers,
- new regulations.

---

## 21.2 Continuous Compliance Model

## Required Diagram — Continuous Compliance Lifecycle

```text
Requirements
     ↓
Policies
     ↓
Controls
     ↓
Implementation
     ↓
Automated Validation
     ↓
Evidence Collection
     ↓
Monitoring
     ↓
Compliance Reporting
     ↓
Findings / Changes
     ↓
Control Improvement
     └───────────────→ Requirements / Policies
```

---

## 21.3 Drift Detection

Monitor:

- access drift,
- policy drift,
- configuration drift,
- model registry drift,
- repository protection drift,
- dependency drift,
- infrastructure drift.

---

## 21.4 Expiring Controls

Track:

- temporary access,
- waivers,
- certificates,
- exemptions,
- customer approvals,
- model approvals.

---

# 22. Compliance Reporting

## 22.1 Compliance Reports Should Be Evidence-Based

Not:

```text
We believe the system follows policy.
```

Prefer:

```text
97.8% of applicable Harness runs satisfied all mandatory controls.
12 runs required authorized waivers.
2 waivers expire within 14 days.
0 unauthorized production deployments occurred.
```

---

## 22.2 Reporting Dimensions

- customer,
- repository,
- team,
- control,
- agent,
- model,
- environment,
- risk class,
- time period.

---

## 22.3 Example Metrics

- percentage of compliant runs,
- failed controls,
- waived controls,
- expired waivers,
- missing approvals,
- unapproved model attempts,
- restricted-data violations,
- unauthorized production attempts,
- evidence completeness,
- remediation time.

Clarify that Chapter 35 will go deeper into productivity metrics; this chapter focuses on compliance indicators.

---

# 23. Architecture Discussion — Compliance Inside the Harness

Compliance should be a capability of the Harness, not merely an external review process.

## 23.1 Proposed Components

```text
Compliance/
├── requirements/
├── profiles/
├── policies/
├── controls/
├── gates/
├── evidence/
├── waivers/
├── reports/
└── retention/
```

---

## 23.2 Compliance Engine Responsibilities

- determine applicable requirements,
- resolve compliance profile,
- evaluate policies,
- authorize models,
- authorize tools,
- invoke gates,
- capture evidence,
- enforce approvals,
- record exceptions,
- generate compliance results.

---

## Required Diagram — Compliance Inside the Harness

```text
                 ┌─────────────────────┐
                 │ Compliance Registry │
                 │ requirements        │
                 │ policies            │
                 │ controls            │
                 └─────────┬───────────┘
                           │
                           ▼
User → Prompt → Lead Agent → Compliance Engine
                           │
              ┌────────────┼────────────┐
              ▼            ▼            ▼
        Data Policy   Model Policy   Tool Policy
              │            │            │
              └────────────┼────────────┘
                           ▼
                    Developer Agent
                           │
                           ▼
                       Reviewer
                           │
                           ▼
                      Validator
                           │
                           ▼
                 Compliance Gates
                           │
                           ▼
                    Human Approval
                           │
                           ▼
                       Deployment
                           │
             ┌─────────────┴─────────────┐
             ▼                           ▼
       Evidence Store               Audit Trail
             │                           │
             └─────────────┬─────────────┘
                           ▼
                  Compliance Reporting
```

---

# 24. Compliance Control Flow

## Required Diagram — Compliance Control Flow

```text
Task Request
    ↓
Identify Customer / System / Data
    ↓
Determine Applicable Obligations
    ↓
Select Compliance Profile
    ↓
Evaluate Access + Data + Model + Tool Policies
    ↓
Execute Approved Workflow
    ↓
Run Compliance Gates
    ↓
Evidence Complete?
 ┌──┴───┐
No      Yes
│        │
Block    ▼
   Required Human Approval?
       ┌──┴───┐
      Yes      No
       │        │
       ▼        │
    Approve     │
       └────┬───┘
            ▼
         Release
            ↓
      Retain Evidence
            ↓
     Continuous Monitor
```

---

# 25. Hands-on Example — Alpha Car Detailing Government Fleet Change

## 25.1 Scenario

Requirement:

> Add authorized fleet representatives to government fleet accounts.

The feature handles:

- representative name,
- business contact details,
- department identifier,
- fleet permissions.

---

## 25.2 Compliance Context

```yaml
customer_type: government
data_classification: restricted
environment: production-capable
change_type: customer-data
```

---

## 25.3 Applicable Controls

Example controls:

```text
C-001 Approved model only
C-002 No production customer records in prompts
C-003 Restricted repository authorization
C-004 Security scan required
C-005 Independent review required
C-006 Human approval before production deployment
C-007 Evidence retention required
C-008 Production access requires JIT authorization
```

---

## 25.4 Execution Walkthrough

### Step 1 — Lead Agent interprets task

- identifies customer type,
- reads Steering Note,
- determines applicable compliance profile.

### Step 2 — Data policy

- blocks real production customer data.
- instructs Developer Agent to use synthetic fixtures.

### Step 3 — Model policy

- selects organization-approved model.

### Step 4 — Developer Agent

- implements domain and API changes.

### Step 5 — Reviewer Agent

- reviews architecture, privacy, and security implications.

### Step 6 — Validator Agent

- runs:
  - build,
  - tests,
  - static analysis,
  - architecture tests,
  - security gates,
  - compliance gates.

### Step 7 — Evaluator Agent

- determines whether outcome satisfies requested business behavior.

### Step 8 — Human approval

- required due to government data processing and production deployment.

### Step 9 — Evidence package

Generated automatically.

---

## 25.5 Example Evidence Manifest

```yaml
run_id: ACD-2026-03421
customer: GovernmentFleet
compliance_profile: GOV-RESTRICTED
repository_commit: abc123
model: approved-enterprise-model
instruction_version: 8f102d
controls:
  C-001: pass
  C-002: pass
  C-003: pass
  C-004: pass
  C-005: pass
  C-006: approved
  C-007: pass
approvals:
  production:
    approved_by: release-manager
evidence_status: complete
```

---

# 26. Security-Sensitive Change Scenario

Alpha Car Detailing changes JWT authorization rules for fleet administrators.

Explain why additional controls apply:

- security classification,
- mandatory security reviewer,
- security test suite,
- authorization regression tests,
- dependency check,
- elevated approval.

Possible execution:

```text
Developer
   ↓
Reviewer
   ↓
Security Reviewer
   ↓
Validator
   ↓
Compliance Gate
   ↓
Human Approval
```

---

# 27. Production Access Scenario

Incident:

Government fleet bookings are failing in production.

Developer Agent requests database access.

Demonstrate compliant approach:

```text
Agent
  ↓
Diagnostic Request
  ↓
Policy Engine
  ↓
Human Approval
  ↓
Read-Only Diagnostic Service
  ↓
Sanitization
  ↓
Agent
```

Compare against anti-pattern:

```text
Developer Agent
   ↓
Production SQL connection
   ↓
SELECT *
FROM Customers
```

---

# 28. Retention and Audit Scenario

Government contract requires long-lived evidence for selected system changes.

Discuss:

- deployment record,
- code version,
- control results,
- approvals,
- change request,
- security evidence.

But temporary agent reasoning state and scratchpads do not automatically receive the same retention period.

Explain retention classification.

---

# 29. Claude Example

Show Claude-first enterprise workflow.

Possible artifacts:

```text
CLAUDE.md
Compliance/
policies/
controls/
```

Example instruction concepts:

```text
Government customer data must never be supplied to external tools
unless the applicable compliance profile explicitly permits it.

Production access requires a human approval artifact.

Do not bypass failed compliance gates.
```

Claude workflow:

1. read governing instructions,
2. identify compliance profile,
3. inspect task constraints,
4. use permitted tools,
5. produce change,
6. run validation,
7. generate evidence,
8. stop when human approval is required.

Emphasize:

Claude should not decide that a legal requirement does not apply unless governance has delegated that authority through explicit rules.

---

# 30. GitHub Copilot Comparison

Compare where materially different.

Discuss:

- repository instructions,
- GitHub organization policy controls,
- repository permissions,
- protected branches,
- required reviewers,
- GitHub Actions compliance gates,
- secret scanning,
- code scanning,
- audit logs,
- enterprise model/data controls where applicable.

Architecture point:

The compliance architecture should survive regardless of whether implementation occurs through Claude, GitHub Copilot, or another AI Agent.

---

# 31. Codex Comparison

Discuss:

- sandboxing,
- repository permissions,
- tool authorization,
- environment isolation,
- network restrictions,
- command execution,
- approval boundaries,
- evidence captured externally by the Harness.

Reinforce vendor-neutral controls around the coding agent.

---

# 32. Best Practices

Include guidance such as:

1. Treat compliance requirements as architecture inputs.
2. Determine applicability before AI execution begins.
3. Convert requirements into explicit controls.
4. Automate deterministic controls.
5. Treat evidence as a first-class output.
6. Assign machine-readable control IDs.
7. Use compliance profiles.
8. Minimize sensitive data exposed to AI models.
9. Maintain approved model and tool registries.
10. Apply least privilege to humans and AI Agents.
11. Separate implementation from approval.
12. Enforce production access independently.
13. Maintain bidirectional traceability.
14. Define artifact-specific retention.
15. Make waivers explicit and time-bound.
16. Continuously test controls.
17. Detect configuration and policy drift.
18. Generate compliance evidence automatically.
19. Keep immutable records for sensitive approval events.
20. Never permit AI Agents to silently override compliance controls.

---

# 33. Anti-Patterns

## 33.1 Compliance at the End

```text
Build first → ask compliance later
```

Why it fails:

- costly redesign,
- missing evidence,
- unauthorized data exposure may already have occurred.

---

## 33.2 Compliance by Documentation

Writing:

```text
"We follow security policy."
```

without enforceable controls.

---

## 33.3 Checkbox Compliance

Treating compliance as:

```text
☑ Security
☑ Privacy
☑ Audit
```

without evidence.

---

## 33.4 Giving Agents Developer Credentials

AI Agent inherits broad permissions from the operator.

Risks:

- repository access,
- cloud access,
- production access,
- secrets.

---

## 33.5 Storing Production Data in Prompts

Especially dangerous for:

- customer records,
- government data,
- personal data,
- authentication records.

---

## 33.6 Treating Security Scan as Complete Compliance

Security scanning addresses only part of the compliance problem.

---

## 33.7 Untracked Exceptions

Example:

```text
Temporarily disable this check.
```

with no waiver, owner, or expiration.

---

## 33.8 Permanent Waivers

A temporary exception quietly becomes permanent architecture.

---

## 33.9 Self-Approval by Agents

Developer Agent determines:

```text
"This change does not need approval."
```

without policy authorization.

---

## 33.10 Retain Everything Forever

Creates:

- privacy risk,
- breach exposure,
- cost,
- legal complexity.

---

## 33.11 Delete Everything Immediately

Destroys required audit evidence.

---

## 33.12 Compliance Logic Hidden in Prompts

Critical controls should not exist only as natural-language suggestions inside a prompt.

Prefer:

```text
Prompt
+
Policy
+
Deterministic Gate
+
Approval
```

---

# 34. Architect’s Notes

### Architect’s Note — Compliance Architecture Is Traceability Architecture

A significant portion of enterprise compliance depends on being able to demonstrate how requirements became policies, controls, evidence, approvals, and deployed artifacts.

### Architect’s Note — AI Agents Are Security Principals

Architect agent access with the same seriousness as users, applications, service identities, and deployment systems.

### Architect’s Note — Policy Is Not Enforcement

Natural-language policy without technical or procedural enforcement creates an expectation, not necessarily a control.

### Architect’s Note — Evidence Should Be Produced During Execution

Reconstructing evidence weeks later is less reliable and more expensive.

### Architect’s Note — Compliance Controls Must Survive Tool Replacement

Changing from Claude to Copilot, Codex, or another agent should not invalidate the enterprise compliance architecture.

---

# 35. Enterprise Tips

### Enterprise Tip — Use Compliance Profiles

Avoid repeatedly implementing customer-specific requirements manually.

### Enterprise Tip — Attach Control IDs to Pipeline Results

Example:

```text
CTRL-SEC-018   PASS
CTRL-PRIV-006  PASS
CTRL-GOV-022   APPROVAL_REQUIRED
```

### Enterprise Tip — Store Evidence by Run ID

Correlate all compliance artifacts through the Harness run.

### Enterprise Tip — Keep Legal Meaning Outside Agent Interpretation

Compliance officers and governance owners determine obligations. Agents execute approved interpretations.

### Enterprise Tip — Test Compliance Controls

A control that has never been tested may not be a reliable control.

---

# 36. Decision Points

## Decision Point 1 — Does this task process regulated or restricted data?

- No → standard profile.
- Yes → restricted compliance profile.

## Decision Point 2 — May the selected AI model process this data?

- Yes → continue.
- No → switch model or sanitize data.

## Decision Point 3 — Does the Agent require production access?

- No → remain isolated.
- Yes → mediated access and approval.

## Decision Point 4 — Is the control deterministic?

- Yes → automate as a gate.
- No → route to authorized human decision.

## Decision Point 5 — Is a control failure acceptable?

- No → block.
- Potentially → formal waiver workflow.

## Decision Point 6 — Must the evidence be retained?

Determine by:

- regulation,
- contract,
- policy,
- data sensitivity.

## Decision Point 7 — Can the Harness determine compliance automatically?

- Objective requirement → yes.
- Legal interpretation/risk acceptance → human authority remains necessary.

---

# 37. Exercises

### Exercise 1 — Identify Compliance Requirements

For Alpha Car Detailing's government fleet booking system, identify:

- regulatory requirements,
- contractual requirements,
- privacy requirements,
- organizational requirements.

### Exercise 2 — Build a Control Map

Create:

```text
Requirement → Policy → Control → Evidence
```

for five requirements.

### Exercise 3 — Create a Compliance Profile

Design:

```text
government-restricted.yaml
```

containing:

- allowed models,
- allowed tools,
- data classifications,
- approvals,
- required gates,
- retention.

### Exercise 4 — Design an AI Agent Access Matrix

Define permissions for:

- Lead,
- Developer,
- Reviewer,
- Validator,
- Evaluator.

Across:

- source repository,
- secrets,
- production logs,
- databases,
- cloud infrastructure,
- deployment pipelines.

### Exercise 5 — Design Compliance Gates

Create gates for:

- prohibited model use,
- missing security scan,
- unauthorized production access,
- missing approval,
- missing evidence.

### Exercise 6 — Design a Waiver

Define a compliant temporary exception to one failed control.

### Exercise 7 — Build Evidence Manifest

Design the JSON or YAML evidence record for one Alpha Car Detailing Harness run.

### Exercise 8 — Retention Strategy

Classify:

- prompts,
- scratchpads,
- audit logs,
- validation reports,
- production access logs,
- approvals.

### Exercise 9 — Threat Scenario

A developer copies a production error log containing customer information into an AI Agent prompt.

Identify:

- policy failure,
- compliance impact,
- preventive control,
- detective control,
- evidence.

### Exercise 10 — Continuous Compliance

Design checks that detect:

- expired waivers,
- unapproved models,
- branch protection drift,
- access drift,
- missing evidence.

---

# 38. Interview Questions

### Foundational

1. What does compliance mean in enterprise AI engineering?
2. What is the difference between governance and compliance?
3. What is the difference between policy and compliance?
4. Why is security not equivalent to compliance?
5. What is compliance evidence?

### Intermediate

1. How do you translate a regulatory requirement into an engineering control?
2. What is control mapping?
3. What is separation of duties?
4. Why should AI Agent access differ from human developer access?
5. What information should an AI engineering audit trail contain?
6. Why is data minimization important when using AI coding agents?
7. What is a compliance profile?
8. When should compliance controls become deterministic gates?
9. What is the difference between a control failure and an authorized waiver?
10. Why should waivers expire?

### Advanced

 1. How would you architect compliance into an AI Harness?
 2. How would you prove that restricted customer data was never sent to an unauthorized model?
 3. How would you implement bidirectional compliance traceability?
 4. How would you enforce model restrictions across several AI coding platforms?
 5. How should compliance evidence be correlated across prompts, commits, pull requests, approvals, and deployments?
 6. How would you implement continuous compliance?
 7. How would you prevent an autonomous AI Agent from bypassing production access controls?
 8. Which compliance decisions should remain human-owned?
 9. How can one deterministic gate satisfy several regulatory requirements?
10. How would you design compliance controls that survive replacement of the AI coding platform?

---

# 39. Chapter Summary

Summarize the major principles:

- Compliance is an engineering concern.
- Compliance begins before AI Agent execution.
- Governance establishes authority; compliance demonstrates satisfaction of obligations.
- Policies define expectations; controls enforce them.
- Evidence proves controls operated.
- Data privacy must include prompts, tool inputs, logs, scratchpads, and AI model processing.
- AI Agents require explicit access boundaries.
- Production access requires special controls.
- Separation of duties protects high-risk operations.
- Human approval remains mandatory where judgment or formal authority is required.
- Compliance gates should automate objective requirements.
- Exceptions require formal waivers.
- Retention must be intentional.
- Traceability must connect requirements through deployment.
- Compliance must be continuous.
- The Harness should enforce controls and automatically generate evidence.
- Compliance architecture must remain vendor-neutral.

End with the core principle:

> **Compliance must be designed into the AI engineering workflow through policies, controls, validation, approvals, and audit evidence rather than checked only after work is complete.**

---

# 40. Further Reading

Organize future references under:

### AI Governance

- organizational AI governance frameworks,
- responsible AI governance guidance,
- enterprise AI control models.

### Security and Risk

- NIST cybersecurity guidance,
- AI risk management frameworks,
- secure software development frameworks.

### Privacy

- privacy engineering principles,
- data minimization,
- privacy-by-design guidance.

### Compliance Controls

- ISO-oriented control frameworks,
- SOC assurance concepts,
- industry-specific compliance frameworks.

### Software Supply Chain

- SBOM,
- artifact provenance,
- signing,
- secure build pipelines.

### Audit and Evidence

- audit logging,
- tamper-resistant records,
- control evidence management,
- continuous compliance.

Further Reading should focus on authoritative standards and primary sources rather than vendor marketing material.

---

## Diagram Inventory

The final chapter should contain at minimum these five professional diagrams:

1. **Compliance Control Flow**
2. **Policy-to-Control-to-Evidence Mapping**
3. **Compliance Inside the Harness**
4. **Exception and Waiver Workflow**
5. **Continuous Compliance Lifecycle**

Supporting diagrams may additionally include:

- Compliance traceability chain
- AI Agent access-control model
- Compliance gate pipeline
- Requirement-to-evidence architecture
- Production diagnostic access flow

**Chapter 34 outline is complete.**
