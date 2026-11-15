# Chapter 33 — Policies  

**Part V — Enterprise AI Governance**  
**Target file:** `book/33-policies.md`

The outline follows the handbook’s frozen chapter structure, enterprise audience, vendor-neutral philosophy, and Alpha Car Detailing running example. AI development handbook

---

## 1. Story-Driven Opening — When an AI Agent Can Do Something but Must Not

- Introduce an Alpha Car Detailing scenario in which an AI Agent is asked to resolve a production incident affecting corporate fleet bookings.
- The agent discovers that:
  - production database access would accelerate diagnosis,
  - customer records contain personally identifiable information,
  - an architecture change could solve the underlying problem,
  - a third-party AI tool could analyze logs more quickly,
  - deployment could be performed automatically.
- Technically, all actions are possible.
- Organizationally, several are prohibited or require approval.
- Show the distinction between **technical capability and organizational authority**.
- Introduce the governing question:
  - *What is the AI Agent allowed to do?*
  - *What must it never do?*
  - *What requires human authorization?*
- Establish the chapter principle:

> Policies define mandatory organizational boundaries within which AI-assisted engineering operates.

- Explain that Prompts, Instructions, Skills, Roles, Steering Notes, and Harness workflows cannot override organizational policy.

---

## 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define an AI Engineering Policy.
- Distinguish Policies from Instructions, Standards, Procedures, and Prompts.
- Design a policy hierarchy for enterprise AI-assisted development.
- Define policy ownership and approval authority.
- Establish policy scope and applicability.
- Version and govern policy changes.
- Translate written policies into enforceable Harness controls.
- Separate advisory, preventive, detective, and approval-based enforcement.
- Design security, privacy, tool usage, model, repository, and production-access policies.
- Govern Prompts and Skills through policy.
- Define policy exceptions without weakening governance.
- Detect and respond to policy violations.
- Validate that Harness workflows comply with organizational policy.
- Build auditable evidence showing policy enforcement.
- Apply these concepts to Alpha Car Detailing.

---

# 3. Background

## 3.1 Why AI-Assisted Engineering Requires Explicit Policies

- Traditional engineering governance versus AI-assisted engineering.
- AI Agents can:
  - inspect repositories,
  - modify source code,
  - invoke tools,
  - execute commands,
  - consume external information,
  - create Pull Requests,
  - access infrastructure,
  - influence architecture,
  - interact with sensitive data.
- Why implicit organizational expectations become dangerous when agents can act autonomously.
- Moving governance from tribal knowledge to explicit policy.

## 3.2 Technical Ability Is Not Authority

Explain:

```text
Can perform action
      ≠
Authorized to perform action
```

Examples:

- Agent can query production SQL Server.
- Agent is prohibited from accessing production customer records.
- Agent can install an external Skill.
- Organization only permits approved Skills.
- Agent can merge a Pull Request.
- Production policy requires human approval.

## 3.3 Policies as an Enterprise Governance Layer

Position Policies above implementation-specific mechanisms.

```text
Organization
     ↓
Policies
     ↓
Standards
     ↓
Instructions / Skills / Roles / Workflows
     ↓
AI Agents
     ↓
Engineering Actions
```

- Policies define boundaries.
- Standards translate some boundaries into engineering expectations.
- Instructions provide repository-specific direction.
- Skills encode reusable workflows.
- Harness controls enforce selected requirements.

---

# 4. Concepts

## 4.1 What Is an AI Engineering Policy?

Define an AI Engineering Policy as:

> An organization-approved governance statement defining mandatory requirements, restrictions, authorities, responsibilities, or approval conditions for AI-assisted engineering activities.

Key characteristics:

- authoritative,
- organization-owned,
- mandatory,
- scoped,
- versioned,
- auditable,
- enforceable where practical,
- exception-controlled.

Example:

> AI Agents must not access production customer data unless access has been explicitly approved through the production-access process.

## 4.2 What Policies Are Not

Policies are not:

- individual developer preferences,
- temporary task instructions,
- coding-agent prompts,
- implementation recipes,
- suggestions,
- team conventions without governance authority.

---

## 4.3 Policies versus Instructions

Clarify the fundamental distinction.

### Policy

Defines what the organization **requires, prohibits, or controls**.

Example:

> Customer PII must not be submitted to unapproved external AI providers.

### Instruction

Defines repository-specific engineering behavior.

Example:

> Mask customer identifiers before writing application logs.

Relationship:

```text
Policy
   ↓
Defines mandatory boundary

Instruction
   ↓
Explains repository-specific behavior within that boundary
```

### Alpha Car Detailing Example

**Policy**

> Production customer data must never be sent to unapproved AI services.

**Instruction**

```text
Never include customer names, email addresses,
registration numbers, or account identifiers
in AI troubleshooting prompts.
```

### Diagram — Policy versus Instruction Relationship

```text
Organization Policy
        │
        │ defines mandatory boundary
        ▼
Repository Instruction
        │
        │ translates boundary into engineering behavior
        ▼
Prompt / Skill / Agent Action
        │
        ▼
Harness Validation
```

Explain:

- Instructions cannot weaken Policies.
- A repository owner cannot locally override enterprise policy.
- Instructions may impose stricter requirements.

---

## 4.4 Policies versus Standards

### Policy

Defines **what must be true**.

Example:

> Sensitive data must be protected.

### Standard

Defines the approved technical expectation.

Example:

> Sensitive data must use TLS 1.3 in transit and approved encryption mechanisms at rest.

Discuss:

- Policies are broader.
- Standards are more technical and measurable.
- Standards may evolve faster than Policies.

---

## 4.5 Policies versus Procedures

### Policy

> Production deployment requires human authorization.

### Procedure

Defines how authorization occurs:

1. Pull Request approved.
2. Validation gates pass.
3. Change ticket created.
4. Release approver authorizes deployment.
5. CI/CD pipeline performs deployment.

Explain:

```text
Policy → Requirement
Standard → Technical expectation
Procedure → Operational process
Instruction → Repository-specific guidance
```

---

## 4.6 Policy Scope

Policies may apply at different levels:

- enterprise,
- business unit,
- program,
- product,
- repository,
- environment,
- data classification,
- AI provider,
- agent role,
- workflow,
- deployment target.

Example scopes:

```text
Enterprise
└── AI Tool Usage Policy

Business Unit
└── Customer Data Policy

Alpha Car Detailing
└── Production Access Policy

Repository
└── Additional Architecture Restrictions
```

Discuss precedence when multiple scopes apply.

---

## 4.7 Policy Hierarchy

Define possible hierarchy:

```text
Corporate Governance
        ↓
Enterprise AI Policies
        ↓
Security / Privacy / Compliance Policies
        ↓
Engineering Policies
        ↓
Architecture & Development Standards
        ↓
Repository Instructions
        ↓
Skills / Prompts / Workflows
```

### Diagram — Policy Hierarchy

Include a professional layered diagram showing:

1. Laws and regulatory obligations
2. Corporate governance
3. Enterprise AI policies
4. Security/privacy/compliance policies
5. Engineering standards
6. Repository Instructions
7. Skills and workflows
8. Prompts and agent execution

Discuss conflict resolution and higher-level precedence.

---

## 4.8 Mandatory, Conditional, and Prohibited Actions

Classify Policies as:

### Mandatory

> All AI-generated production changes must pass deterministic validation.

### Conditional

> AI-generated database migrations require DBA approval before production deployment.

### Prohibited

> AI Agents must not retrieve production database credentials.

This classification should support machine-readable enforcement later.

---

# 5. Policy Ownership and Governance

## 5.1 Policy Ownership

Potential owners:

- Enterprise Architecture,
- Security,
- Privacy,
- Legal,
- Compliance,
- Engineering Leadership,
- Platform Engineering,
- AI Governance Board.

Clarify that ownership depends on policy domain.

## 5.2 Policy Author versus Policy Owner

Distinguish:

- author,
- subject-matter expert,
- owner,
- approver,
- implementer,
- enforcement owner.

## 5.3 Approval

Policies should have explicit approval authority.

Possible workflow:

```text
Draft
  ↓
Architecture Review
  ↓
Security / Compliance Review
  ↓
Business Review
  ↓
Approval
  ↓
Publication
```

## 5.4 Separation of Duties

Discuss why:

- the Agent cannot approve its own permissions,
- the team subject to policy should not silently change it,
- Harness administrators should not automatically become policy owners.

---

# 6. Policy Lifecycle

## 6.1 Policy States

Suggested states:

- Draft
- Under Review
- Approved
- Active
- Superseded
- Retired

## 6.2 Versioning

Each Policy should identify:

- policy ID,
- title,
- version,
- owner,
- approver,
- effective date,
- scope,
- requirements,
- enforcement mechanism,
- exceptions process,
- review date.

Example:

```yaml
policy_id: AI-SEC-014
title: Production Data Access
version: 2.1
owner: Enterprise Security
status: active
effective_date: 2026-10-01
```

## 6.3 Policy Change Management

- Policy modification versus policy clarification.
- Breaking governance changes.
- Effective dates.
- Grace periods.
- Migration impact.
- Communicating changes to repositories and Harnesses.

## 6.4 Policy Lifecycle Diagram

```text
Identify Need
     ↓
Draft
     ↓
Review
     ↓
Approve
     ↓
Publish
     ↓
Implement Controls
     ↓
Validate
     ↓
Monitor
     ↓
Review
   ↙   ↘
Revise  Retire
```

---

# 7. Policy Categories

## 7.1 Security Policies

Cover policies governing:

- credentials,
- secrets,
- vulnerability handling,
- dependency usage,
- security scanning,
- privileged actions,
- command execution,
- network access.

Alpha Car Detailing example:

> AI Agents must never read or expose production SQL credentials.

Harness enforcement:

- secret scanning,
- credential isolation,
- blocked environment variables,
- restricted tool permissions.

---

## 7.2 Data and Privacy Policies

Address:

- PII,
- financial data,
- government customer information,
- customer vehicle details,
- retention,
- masking,
- external transmission,
- training-data restrictions.

Alpha Car Detailing example:

> Customer names, contact information, payment references, and fleet identifiers may not be submitted to external AI tools unless the provider is approved for that data classification.

Discuss:

- prompt sanitization,
- redaction,
- data minimization,
- data boundaries.

---

## 7.3 Tool Usage Policies

Define:

- approved tools,
- prohibited tools,
- conditional tools,
- tool permissions,
- network-enabled tools,
- shell execution,
- package installation.

Example:

```text
Approved:
- Repository search
- Build
- Test execution

Conditional:
- External documentation lookup
- Infrastructure CLI

Prohibited:
- Direct production database modification
```

---

## 7.4 Model and Provider Policies

Govern:

- approved AI providers,
- approved models,
- data-processing agreements,
- regional requirements,
- model capabilities,
- confidential-data handling,
- provider fallback.

Important principle:

> Provider availability does not imply organizational approval.

Address model substitution and failover.

---

## 7.5 Repository Access Policies

Define possible access levels:

```text
Read
Write
Branch creation
Pull Request creation
Merge
Release
Administrative access
```

Alpha Car Detailing example:

Developer Agent:

- repository read/write,
- feature branch creation,
- Pull Request creation,
- no direct merge to `main`.

Reviewer Agent:

- read access,
- comment capability,
- no implementation changes unless explicitly reassigned.

---

## 7.6 Human Approval Policies

Cross-reference Chapter 31.

Examples requiring approval:

- architecture changes,
- security-sensitive changes,
- production releases,
- schema migrations,
- customer-data access,
- new external AI providers,
- policy exceptions.

Reinforce:

> Human approval is a governance control, not merely another workflow step.

---

## 7.7 Production Access Policies

Define strong separation between:

- development,
- test,
- staging,
- production.

Possible policy:

> AI Agents operate with no standing production privileges.

Allow controlled mechanisms:

- temporary authorization,
- read-only diagnostic access,
- mediated tools,
- approved runbooks,
- time-bound credentials.

Alpha Car Detailing production incident example.

---

## 7.8 Architecture Change Policies

Use Alpha Car Detailing scenario where an Agent proposes:

```text
BookingService
     ↓
Direct database sharing
     ↓
FleetService
```

Policy prohibits shared-database coupling between independently owned services.

Agent must:

1. identify conflict,
2. stop automatic implementation,
3. produce architecture proposal,
4. request architectural review.

---

## 7.9 Prompt Governance Policies

Policies may define:

- where Prompts are stored,
- sensitive-data restrictions,
- approval requirements,
- version control,
- injection protection,
- external Prompt usage,
- traceability.

Examples:

> Production automation Prompts must be version-controlled.

> Prompts must not embed credentials.

---

## 7.10 Skill Governance Policies

Policies covering:

- Skill registration,
- approved Skill libraries,
- Skill ownership,
- validation,
- privilege level,
- dependency review,
- provenance,
- versioning.

Example:

A downloaded Skill requesting unrestricted shell access cannot automatically enter the enterprise Skill library.

---

## 7.11 Audit and Retention Policies

Cross-reference Chapter 32.

Policies may define retention for:

- Prompts,
- AI responses,
- tool calls,
- validation evidence,
- approval decisions,
- policy exceptions,
- deployment actions,
- Harness runs.

Discuss balancing:

- traceability,
- privacy,
- storage cost,
- regulatory obligations.

---

# 8. Architecture Discussion — Policies Inside the Harness

## 8.1 Policies as Inputs to Harness Execution

Describe policy-aware Harness architecture.

```text
Task
 ↓
Prompt
 ↓
Lead Agent
 ↓
Policy Evaluation
 ↓
Allowed?
 ├── No → Block / Escalate
 ├── Approval Required → Approval Gate
 └── Yes
        ↓
Developer Agent
        ↓
Deterministic Gates
        ↓
Reviewer
        ↓
Validator
        ↓
Evaluator
        ↓
Human Approval
        ↓
Release
```

## 8.2 Policy-to-Harness Enforcement Diagram

Required diagram showing:

```text
Policy Repository
      │
      ▼
Policy Resolver
      │
      ├── Repository Policies
      ├── Security Policies
      ├── Data Policies
      └── Environment Policies
      │
      ▼
Harness
 ┌────┼────────────┐
 ▼    ▼            ▼
Agent Tool      Validation
Rules Access      Gates
 │      │            │
 └──────┼────────────┘
        ▼
 Enforcement Decision
        │
   ┌────┼─────┐
   ▼    ▼     ▼
 Allow Block Approval
```

---

# 9. Policy Enforcement

## 9.1 Enforcement Models

Discuss:

### Preventive

Prevent prohibited behavior before execution.

Examples:

- no production credentials,
- blocked tools,
- repository permission boundaries.

### Detective

Identify violations after or during execution.

Examples:

- secret scanning,
- audit monitoring,
- policy validation.

### Corrective

Respond to detected violations.

Examples:

- revoke permissions,
- stop run,
- quarantine artifact.

### Approval-Based

Pause until authorized human approval.

---

## 9.2 Hard Controls versus Soft Guidance

### Hard control

Technically enforced.

```text
Agent cannot merge directly to main.
```

### Soft guidance

Instruction to Agent:

```text
Do not merge directly to main.
```

Explain why high-risk requirements should not depend exclusively on natural-language compliance.

---

## 9.3 Defense in Depth

Example:

Production policy should not rely only on:

```text
CLAUDE.md:
"Do not access production."
```

Instead:

```text
Policy
   +
Instruction
   +
Role permissions
   +
Credential isolation
   +
Network control
   +
Harness gate
   +
Audit logging
```

---

# 10. Policy Validation

## 10.1 What Is Policy Validation?

Define verification that:

- active policies are loaded,
- relevant policies are applied,
- Harness controls correspond to policy,
- controls remain operational,
- policy evidence exists.

## 10.2 Policy-as-Code

Discuss selective machine-readable representation.

Possible policy structure:

```yaml
policy:
  id: AI-PROD-001
  scope: production
  action: deploy
  actor: ai-agent
  requirement: human-approval
```

Explain that not every policy can or should become executable code.

## 10.3 Deterministic Policy Gates

Examples:

- prohibited dependency detection,
- architecture tests,
- secret scans,
- branch protection,
- approval verification,
- data-classification checks.

## 10.4 Policy Drift

Explain divergence between:

```text
Written Policy
        ≠
Actual Harness Enforcement
```

Causes:

- stale controls,
- changed repository structure,
- outdated Skills,
- changed provider capabilities,
- unmaintained validation rules.

Include periodic policy-control reconciliation.

---

# 11. Exceptions

## 11.1 Why Exceptions Exist

Legitimate cases:

- emergency production remediation,
- migration,
- regulatory request,
- legacy system limitation,
- temporary vendor dependency.

## 11.2 Exceptions Must Not Become Informal Overrides

Avoid:

> “The architect said it was okay in Slack.”

Require structured exception records.

## 11.3 Exception Requirements

Capture:

- policy ID,
- requested exception,
- reason,
- scope,
- risk,
- compensating controls,
- approver,
- effective date,
- expiration,
- evidence.

## 11.4 Exception Workflow Diagram

```text
Policy Conflict Detected
        ↓
Exception Requested
        ↓
Risk Assessment
        ↓
Policy Owner Review
      ↙       ↘
   Reject     Approve
               ↓
      Compensating Controls
               ↓
       Time-Bound Exception
               ↓
            Audit
               ↓
        Expire / Renew
```

## 11.5 Emergency Exceptions

Discuss:

- break-glass access,
- elevated monitoring,
- mandatory retrospective,
- automatic expiration.

---

# 12. Policy Violations

## 12.1 Types of Violation

- attempted violation,
- blocked violation,
- accidental violation,
- deliberate violation,
- configuration-induced violation,
- Harness defect.

## 12.2 Example

Developer Agent attempts to submit production customer logs to an unapproved external model.

Expected flow:

```text
Attempt
 ↓
Policy Check
 ↓
Blocked
 ↓
Recorded
 ↓
Security Notification if Required
 ↓
Run Continues Safely or Stops
```

## 12.3 Violation Severity

Potential classifications:

- informational,
- low,
- medium,
- high,
- critical.

## 12.4 Response

Possible outcomes:

- block action,
- terminate run,
- request human intervention,
- revoke credentials,
- quarantine generated artifacts,
- raise incident,
- require investigation.

## 12.5 Do Not Automatically Punish Every Agent Failure

Differentiate:

- malicious behavior,
- Agent reasoning error,
- ambiguous Policy,
- missing enforcement,
- faulty Skill,
- incorrect Prompt.

Use violations as governance feedback.

---

# 13. Compliance

## 13.1 Relationship Between Policy and Compliance

Explain:

```text
External Obligation
      ↓
Organizational Policy
      ↓
Engineering Controls
      ↓
Harness Enforcement
      ↓
Evidence
      ↓
Compliance Demonstration
```

## 13.2 Evidence

Examples:

- approval records,
- test results,
- access logs,
- policy versions,
- Harness execution history,
- Pull Request records,
- security scan results.

## 13.3 Traceability

Illustrate:

```text
Requirement
   ↓
Policy
   ↓
Control
   ↓
Harness Gate
   ↓
Evidence
```

Prepare conceptual bridge into Chapter 34 — Compliance.

---

# 14. Hands-on Example — Alpha Car Detailing Policy Model

Create an end-to-end example.

## Scenario

A Developer Agent receives:

> Optimize corporate fleet booking performance and deploy the fix.

The Agent discovers:

- production logs contain customer IDs,
- database access would simplify diagnosis,
- Redis configuration requires modification,
- architecture changes may affect Booking Service,
- production deployment would be required.

## Policy Evaluation

### Policy 1 — Customer Data

Production PII cannot be submitted to external models.

**Result:** redact diagnostic evidence.

### Policy 2 — Production Database

AI Agents cannot directly query production SQL Server.

**Result:** request approved diagnostic data.

### Policy 3 — Architecture

Service-boundary changes require Architect approval.

**Result:** generate architecture proposal.

### Policy 4 — Tool Usage

Only approved AI providers may process company source code.

**Result:** prohibit unapproved external tools.

### Policy 5 — Production Deployment

AI-generated changes require human release approval.

**Result:** Harness stops at deployment approval gate.

## Final Workflow

```text
Prompt
  ↓
Policy Resolution
  ↓
Repository Investigation
  ↓
Sanitized Evidence
  ↓
Implementation
  ↓
Architecture Check
  ↓
Tests
  ↓
Security Gates
  ↓
Reviewer
  ↓
Validator
  ↓
Evaluator
  ↓
Human Approval
  ↓
Production Deployment
```

---

# 15. Claude Example

Demonstrate how Claude Code operates under policy boundaries.

Possible repository structure:

```text
governance/
├── policies/
│   ├── ai-tool-policy.md
│   ├── production-access-policy.md
│   ├── customer-data-policy.md
│   └── architecture-change-policy.md
│
CLAUDE.md
skills/
harness/
```

Explain:

- Policies remain authoritative.
- `CLAUDE.md` may summarize relevant operational obligations.
- Claude must not treat repository Instructions as authority to bypass policy.
- High-risk enforcement belongs outside natural-language instructions as well.

Show conceptual workflow:

```text
Policy
   ↓
CLAUDE.md guidance
   ↓
Claude task execution
   ↓
Harness restrictions
   ↓
Deterministic validation
```

---

# 16. GitHub Copilot Comparison

Compare conceptually:

- repository instructions,
- organization-managed settings,
- Pull Request governance,
- GitHub permissions,
- branch protection,
- required checks,
- security controls.

Emphasize:

> Enterprise Policies remain platform-independent even when enforcement mechanisms vary.

Discuss where organization-level controls should remain outside Copilot-specific configuration.

---

# 17. Codex Comparison

Compare:

- execution environment,
- repository access,
- tool permissions,
- task instructions,
- sandbox restrictions,
- external access,
- approval boundaries.

Explain that the same policy architecture should survive changes in AI platform.

---

# 18. Best Practices

Cover principles such as:

- Keep Policies concise and authoritative.
- Give every Policy an owner.
- Define explicit scope.
- Assign stable policy identifiers.
- Version Policies.
- Separate Policies from Procedures.
- Translate high-risk Policies into deterministic controls.
- Apply least privilege to AI Agents.
- Require explicit approval for privileged actions.
- Use defense in depth.
- Make exceptions time-bound.
- Maintain traceability between policy and enforcement.
- Audit policy decisions.
- Test policy controls.
- Review Policies periodically.
- Prefer vendor-neutral Policies.
- Keep provider-specific implementation details in lower governance layers.
- Ensure Instructions, Skills, and Prompts inherit policy constraints.
- Never assume an AI Agent will reliably enforce critical policy through natural-language instruction alone.

---

# 19. Anti-Patterns

## 19.1 Policy Only in CLAUDE.md

Treating repository Instructions as enterprise policy enforcement.

## 19.2 Policy by Prompt

Example:

> “Please don't access production.”

without technical restrictions.

## 19.3 Invisible Policies

Policies known only through organizational culture.

## 19.4 Policy Without Owner

Nobody is accountable for maintenance.

## 19.5 Policy Without Enforcement

Mandatory requirements exist but no controls verify them.

## 19.6 Policy Without Exceptions Process

Teams begin bypassing governance informally.

## 19.7 Permanent Exceptions

Temporary approvals become indefinite.

## 19.8 Overly Broad Policies

Example:

> AI must always be secure.

Too vague to guide or enforce.

## 19.9 Tool-Specific Governance

Policies tightly coupled to Claude, Copilot, or Codex instead of organizational risk.

## 19.10 Autonomous Policy Modification

Allowing a self-learning Harness to silently change governance.

Reaffirm previously established principle:

```text
Harness learns
      ↓
Harness recommends
      ↓
Humans review
      ↓
Policy owner approves
```

Never:

```text
Harness learns
      ↓
Harness silently rewrites policy
```

---

# 20. Architect's Notes

Include notes around:

### Architect's Note — Policy Is Above the Agent

No AI Agent should have authority to redefine the governance boundary controlling that Agent.

### Architect's Note — Enforcement Belongs at the Strongest Available Layer

Critical restrictions should be implemented through identity, permissions, networks, CI/CD, Harness gates, or infrastructure whenever possible.

### Architect's Note — Policy and Architecture Must Be Traceable

Architecture decisions affecting regulated or sensitive systems should identify applicable Policies.

### Architect's Note — Vendor Neutrality

Policies should normally describe organizational intent rather than provider-specific commands.

---

# 21. Enterprise Tips

Include practical guidance such as:

- Start with a small AI Engineering Policy catalog.
- Categorize Policies by security, privacy, tools, production, repositories, and approvals.
- Assign stable identifiers.
- Store machine-consumable metadata where useful.
- Integrate policy checks with the Harness.
- Maintain a policy-to-control matrix.
- Include Policy IDs in violations and audit events.
- Connect exceptions to expiration dates.
- Review policy enforcement during platform changes.
- Test whether agents can bypass restrictions.
- Treat policy enforcement as part of Harness engineering, not documentation alone.

---

# 22. Decision Points

## Decision Point 1 — Should This Be a Policy or an Instruction?

Ask:

- Is it organization-wide?
- Is it mandatory?
- Does violating it create governance risk?
- Should individual repositories be allowed to override it?

If no override is allowed, it likely belongs at Policy level.

## Decision Point 2 — Must the Policy Be Technically Enforced?

Evaluate:

```text
Risk
×
Impact
×
Likelihood
×
Agent autonomy
```

Higher-risk requirements should favor deterministic enforcement.

## Decision Point 3 — Should the Agent Stop or Escalate?

Options:

- continue,
- warn,
- request approval,
- block,
- terminate execution.

## Decision Point 4 — Is an Exception Appropriate?

Evaluate:

- business necessity,
- risk,
- duration,
- compensating controls,
- approval authority.

## Decision Point 5 — Should the Policy Be Machine-Readable?

Use machine-readable representation when automated evaluation provides clear value without oversimplifying governance intent.

---

# 23. Exercises

## Exercise 1 — Identify Policies

Given Alpha Car Detailing engineering requirements, classify each as:

- Policy,
- Standard,
- Procedure,
- Instruction.

## Exercise 2 — Design a Security Policy

Create an organizational policy covering:

- production credentials,
- secrets,
- AI Agent permissions.

## Exercise 3 — Customer Data Policy

Design a Policy governing customer data sent to AI providers.

## Exercise 4 — Production Access

Define:

- prohibited actions,
- conditional actions,
- approval requirements,
- emergency access.

## Exercise 5 — Tool Governance

Create an approved/conditional/prohibited tool matrix.

## Exercise 6 — Policy-to-Control Mapping

Map:

```text
Policy
→ Control
→ Harness Gate
→ Evidence
```

for five Alpha Car Detailing Policies.

## Exercise 7 — Exception Workflow

Design an exception request for emergency production access.

## Exercise 8 — Policy Validation

Identify how the Harness can prove that a production-deployment policy was enforced.

## Exercise 9 — Find Governance Weaknesses

Review a hypothetical Harness relying entirely on natural-language Instructions and identify policy bypass risks.

## Exercise 10 — Create an Enterprise Policy Catalog

Design an initial catalog covering:

- security,
- privacy,
- models,
- tools,
- repositories,
- production,
- approvals,
- Prompts,
- Skills,
- audit retention.

---

# 24. Interview Questions

Include questions such as:

1. What is an AI Engineering Policy?
2. How does a Policy differ from an Instruction?
3. How does a Policy differ from a Standard?
4. How does a Policy differ from a Procedure?
5. Why should repository Instructions not override enterprise Policies?
6. What is policy hierarchy?
7. Who should own AI Engineering Policies?
8. How should Policies be versioned?
9. What is policy-as-code?
10. Should every Policy be executable?
11. What is policy drift?
12. How should AI tool usage be governed?
13. How should organizations govern model providers?
14. Why is production access especially important for AI Agents?
15. How should customer data be protected during AI-assisted development?
16. How can a Harness enforce policy?
17. What is the difference between preventive and detective policy controls?
18. Why should high-risk Policies use deterministic enforcement?
19. How should policy exceptions work?
20. Why should exceptions expire?
21. What should happen when an AI Agent violates Policy?
22. How do Policies relate to human approval?
23. How should Prompts and Skills be governed?
24. What evidence demonstrates policy compliance?
25. Why must a self-learning Harness not silently modify Policy?

---

# 25. Chapter Summary

Summarize the core principles:

- Policies establish mandatory organizational boundaries.
- Policies are different from Instructions, Standards, and Procedures.
- Higher-level Policies take precedence over repository-level AI guidance.
- AI Agents operate inside Policies; they do not define them.
- Security, privacy, tool usage, models, repository access, production access, and human approval require explicit governance.
- Policies require ownership, approval, versioning, lifecycle management, and review.
- Critical Policies should be enforced through deterministic controls whenever possible.
- Harnesses should consume and enforce applicable Policies.
- Exceptions must be explicit, approved, auditable, and time-bound.
- Policy violations should generate structured evidence.
- Policies provide the governance bridge between organizational intent and AI-assisted engineering execution.

Conclude with the principle:

> **Policies define the boundaries. Instructions guide behavior within those boundaries. Skills automate approved practices. Prompts request work. Roles assign responsibility. The Harness coordinates and enforces the system—but none of them may override organizational policy.**

---

# 26. Further Reading

Structure the section around categories rather than locking the outline to specific publications:

- Enterprise AI governance frameworks
- NIST AI Risk Management Framework
- Secure software development frameworks
- Zero Trust architecture
- Software supply-chain governance
- Privacy engineering
- Policy-as-code approaches
- Identity and least-privilege architecture
- AI risk management
- Model governance
- Secure CI/CD
- Auditability and compliance automation
- Internal organizational security, architecture, privacy, and data-governance Policies

Include cross-references to:

- Chapter 6 — Instructions
- Chapter 7 — Skills
- Chapter 8 — Prompts
- Chapter 9 — Roles
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture
- Chapter 17 — Validator Agent
- Chapter 18 — Evaluator Agent
- Chapter 29 — AI Governance
- Chapter 30 — Security
- Chapter 31 — Human Approval
- Chapter 32 — Audit Trails
- Chapter 34 — Compliance

**Chapter 33 outline is complete**
