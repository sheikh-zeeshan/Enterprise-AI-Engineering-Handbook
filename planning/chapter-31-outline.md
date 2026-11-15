# **Chapter 31 — Human Approval**

**Part V — Enterprise AI Governance**

**Target File:** `book/31-human-approval.md`

**Repository:** [Enterprise AI Engineering Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)

## **Chapter Objective**

Explain Human Approval as a formal enterprise governance control that determines when AI-generated or AI-executed engineering activities require explicit authorization from an accountable human before proceeding.

The chapter establishes how approval boundaries are defined, enforced, recorded, and audited within an enterprise AI Engineering Harness. It demonstrates how organizations can preserve AI-assisted development velocity while retaining human authority over high-risk engineering decisions.

**Core Principle:**

> AI Agents may recommend, implement, review, validate, and evaluate engineering changes, but they cannot independently authorize decisions reserved for accountable humans. Human Approval is an enforceable governance boundary, not a substitute for technical quality controls.

**Running Example:** Alpha Car Detailing

**Primary architectural distinction:**

- **Reviewer Agent:** Assesses implementation and architectural quality.
- **Validator Agent:** Verifies objective, deterministic engineering conditions.
- **Evaluator Agent:** Assesses goal satisfaction and outcome quality.
- **Human Approver:** Authorizes a governed action within an explicitly defined scope.
- **Harness:** Enforces the approval boundary and prevents unauthorized execution.

## **1. Story-Driven Opening — When a Successful AI Implementation Must Not Proceed**

### **1.1 The Alpha Car Detailing Production Release**

- Introduce a new corporate fleet booking feature developed through the AI Engineering Harness.
- The Developer Agent implements changes to the booking API, database schema, and integration events.
- The Reviewer Agent finds the implementation architecturally acceptable.
- The Validator Agent reports successful builds, tests, security checks, and contract validation.
- The Evaluator Agent concludes that the implementation meets the business acceptance criteria.
- The Harness prepares the changes for production deployment.

### **1.2 The Governance Problem**

- The release includes a database migration affecting existing corporate fleet bookings.
- An integration event contract changes in a way that may affect downstream systems.
- The deployment requires a temporary operational security exception.
- All automated quality checks pass, but the changes carry residual business and operational risks.
- The central question: Does technical success grant authority to deploy?

### **1.3 The Required Human Decision**

- The solution architect must approve the architectural and contract implications.
- The security owner must authorize the security exception.
- The production release authority must authorize deployment.
- The Harness must prevent execution until all mandatory authorizations are satisfied.

### **1.4 The Chapter's Central Lesson**

- Technical correctness does not establish organizational authorization.
- An AI Agent's confidence is not an approval.
- A successful automated evaluation does not eliminate business accountability.
- Human authorization must be explicit, contextual, limited, and traceable.

## **2. Learning Objectives**

After completing this chapter, readers should be able to:

1. Define Human Approval within enterprise AI Engineering.
2. Distinguish approval from review, validation, evaluation, and automated gates.
3. Establish risk-based approval policies and authority boundaries.
4. Identify actions requiring architectural, security, privacy, contractual, or production approval.
5. Design approval levels and assign accountable approval owners.
6. Implement approval checkpoints within a multi-agent Harness.
7. Enforce separation of duties and prevent self-approval.
8. Define the scope, validity, expiry, and revocation of approvals.
9. Detect material changes requiring re-approval.
10. Design exception and emergency approval mechanisms.
11. Protect governed Instructions, Skills, and engineering standards from unauthorized modification.
12. Produce auditable evidence of approval decisions.
13. Incorporate approvals into CI/CD and production execution pipelines.
14. Design approval workflows that remain effective across Claude Code, GitHub Copilot, Codex, and other coding agents.

## **3. Background — Human Authority in AI-Assisted Engineering**

### **3.1 From Human-Written Changes to Agent-Generated Changes**

- Traditional engineering reviews and approvals.
- AI-assisted implementation and automated engineering workflows.
- The expansion from code generation to agent-executed actions.
- How increasing autonomy changes governance requirements.
- Why organizational accountability cannot simply be delegated to an AI Agent.

### **3.2 The Difference Between Capability and Authority**

- What an AI Agent is technically capable of executing.
- What an AI Agent is authorized to execute.
- The distinction between tool permissions and business authorization.
- The principle of least privilege.
- The need for externally enforced action boundaries.

### **3.3 Why Approval Boundaries Matter**

- Irreversible actions.
- Production service disruption.
- Sensitive information exposure.
- Architectural deviations.
- Compliance obligations.
- Backward-incompatible contracts.
- Changes to shared engineering standards.
- Financial and reputational consequences.

### **3.4 Human Approval as a Governance Mechanism**

- Approval as an explicit authorization decision.
- Approval as a controlled transition between workflow states.
- Approval as evidence of accountable human judgment.
- Approval as a time-bound and scope-bound permission.
- Approval as a control implemented by the Harness rather than a conversational convention.

### **3.5 Relationship to Earlier Chapters**

- Chapter 12: What Is a Harness?
- Chapter 13: Harness Architecture.
- Chapters 14–18: Lead, Developer, Reviewer, Validator, and Evaluator Agents.
- Chapters covering deterministic gates, hooks, and automated guardrails.
- Chapters 29–30: AI Governance and Security.
- Forward references to Audit Trails, Policies, and Compliance.

## **4. Concepts — Understanding Human Approval**

### **4.1 What Human Approval Is**

- Definition and intended purpose.
- Explicit human authorization.
- Identifiable and accountable approval owners.
- Clearly defined approval scope.
- Evidence-backed approval requests.
- Enforceable decisions.
- Time-bounded authorization.
- Denial, deferral, and conditional decisions.

### **4.2 Approval Versus Review**

- Review establishes findings and recommendations.
- Approval establishes authorization.
- A Reviewer Agent may recommend acceptance without having approval authority.
- Human code review and formal governance approval may be separate controls.
- Approval does not imply that all review findings have disappeared.

### **4.3 Approval Versus Validation**

- Validation verifies objective engineering conditions.
- Approval addresses authority, accountability, and residual risk.
- A passing validation gate cannot waive mandatory authorization.
- A human approval cannot silently override a failed mandatory deterministic gate.
- Exceptions require a distinct policy-governed mechanism.

### **4.4 Approval Versus Evaluation**

- Evaluation assesses whether an implementation satisfies intended goals.
- Approval authorizes whether a governed action may proceed.
- Positive outcome evaluation does not necessarily justify deployment.
- The Evaluator Agent can provide evidence and recommendations without becoming an approval authority.

### **4.5 Human Approval Versus Automated Gates**

- Deterministic gates enforce machine-verifiable conditions.
- Approval gates enforce authorization requirements.
- The role of policy engines in determining approval obligations.
- Combining automated gates with human approval.
- Fail-closed behavior when approval cannot be established.

### **4.6 Approval Scope**

- Approval of a specific change.
- Approval of a specific artifact version.
- Approval of a deployment to a particular environment.
- Approval of a bounded security exception.
- Approval of a governed Instruction or Skill revision.
- Avoiding broad, unlimited authorization.

### **4.7 Risk-Based Approval**

- Risk classification.
- Likelihood and impact.
- Reversibility.
- Production criticality.
- Data sensitivity.
- Security exposure.
- Architectural significance.
- Regulatory obligations.
- Organizational risk tolerance.

### **4.8 Approval Levels**

Proposed reference model:

| Level | Risk classification      | Approval expectation                                          |
| ----- | ------------------------ | ------------------------------------------------------------- |
| L0    | Low-risk, reversible     | Automated authorization under approved policy                 |
| L1    | Moderate impact          | Designated engineering owner                                  |
| L2    | High impact              | Accountable domain owner, such as architect or security owner |
| L3    | Critical or irreversible | Multiple designated authorities and formal authorization      |

Clarify that these are illustrative governance levels rather than universal industry classifications.

### **4.9 Approval Outcomes and States**

- Not required.
- Required.
- Pending.
- Approved.
- Approved with conditions.
- Rejected.
- Deferred.
- Expired.
- Revoked.
- Invalidated by change.
- Escalated.

Distinguish an approval decision from the workflow state of the requested action.

### **4.10 Approval Ownership and Accountability**

- Requester.
- Implementer.
- Evidence producer.
- Approval policy owner.
- Designated approver.
- Delegated approver.
- Release executor.
- Auditor.

Define why responsibility for producing a change must not automatically include authority to approve it.

## **5. Architecture Discussion — Engineering an Approval Control Plane**

### **5.1 Approval Architecture Principles**

- Human Approval as a first-class Harness capability.
- Separation of implementation and authorization responsibilities.
- Authorization decisions made outside the AI Agent's discretion.
- Centralized policy with distributed enforcement.
- Deterministic approval state verification.
- Fail-closed handling of missing, invalid, or expired approvals.
- Least-privilege execution after authorization.

### **5.2 Approval Policy Model**

Define the information required to determine whether approval is mandatory:

- Action type.
- Target environment.
- Affected resources.
- Change classification.
- Risk score or risk category.
- Required approval level.
- Required approver role.
- Required number of approvals.
- Evidence requirements.
- Separation-of-duties constraints.
- Approval validity period.
- Re-approval conditions.
- Emergency override policy.

### **5.3 Approval Authority Matrix**

| Change type                     | Proposed approval owner                     | Primary concern                 |
| ------------------------------- | ------------------------------------------- | ------------------------------- |
| Significant architecture change | Solution Architect / Architecture Authority | Architectural integrity         |
| Security control exception      | Security Owner                              | Security risk acceptance        |
| Production deployment           | Authorized Release Owner                    | Operational readiness           |
| Sensitive data handling         | Data Owner / Privacy Authority              | Data protection                 |
| Breaking API change             | API / Contract Owner                        | Consumer compatibility          |
| Breaking event change           | Event Contract Owner                        | Downstream reliability          |
| High-risk database migration    | Database Owner / Release Authority          | Data integrity and recovery     |
| Governed Instruction change     | Engineering Standards Owner                 | Persistent engineering behavior |
| Governed Skill change           | Skill Owner / Platform Authority            | Reusable agent behavior         |

Clarify that high-risk changes may require several approvals, and that organizational ownership models determine the actual approvers.

### **5.4 Approval Request Model**

Outline a structured approval request containing:

- Unique approval request ID.
- Harness run ID.
- Task or change ID.
- Requester identity.
- Action requiring authorization.
- Target system and environment.
- Risk classification.
- Summary of proposed changes.
- Relevant artifact references and digests.
- Review, validation, and evaluation results.
- Known risks and exceptions.
- Rollback or recovery strategy.
- Required approver roles.
- Expiration policy.

### **5.5 Approval Decision Model**

- Approver identity.
- Authenticated organizational role.
- Authorization scope.
- Decision.
- Decision timestamp.
- Reason or justification.
- Attached conditions.
- Associated change or artifact digest.
- Expiration timestamp.
- Policy version.
- Evidence and audit references.

### **5.6 Approval Service and Policy Engine**

- Approval orchestration service.
- Risk classification component.
- Policy evaluation engine.
- Approver directory and identity integration.
- Approval evidence repository.
- Approval status store.
- Notification and escalation mechanism.
- Enforcement adapter for Harness actions.
- Audit event publisher.

### **5.7 Approval Lifecycle**

- Proposal created.
- Risk classified.
- Policy evaluated.
- Evidence assembled.
- Request submitted.
- Approver authenticated and authorized.
- Decision recorded.
- Conditions checked.
- Approval validated at execution time.
- Governed action executed.
- Outcome and evidence retained.

### **5.8 Separation of Duties**

- Requester versus approver.
- Implementer versus approver.
- Security exception requester versus security authority.
- Standards contributor versus standards owner.
- Deployment initiator versus release authority.
- Multiple independent approvals for critical actions.
- Preventing one person from satisfying multiple independent approval requirements unless policy explicitly allows it.

### **5.9 Delegation and Escalation**

- Explicit delegation rules.
- Eligibility of delegated approvers.
- Time-bounded delegation.
- Accountability of the designated authority.
- Conflict-of-interest checks.
- Escalation of pending or unavailable approvals.
- Prevention of unauthorized implicit delegation.

### **5.10 Approval Expiry and Revocation**

- Fixed expiration.
- Deployment-window expiration.
- Approval tied to an immutable artifact.
- Explicit revocation.
- Role or policy changes.
- Superseded requests.
- Invalidation after material change.
- Rechecking authorization before execution.

### **5.11 Re-Approval After Material Change**

- Definition of material change.
- Artifact hash comparison.
- Change scope expansion.
- New security findings.
- Contract incompatibility.
- Database migration modification.
- Target environment change.
- Approval condition violation.
- Policy changes affecting authorization.
- Automatic invalidation followed by a new approval cycle.

### **5.12 Emergency Approval**

- Break-glass authorization.
- Restricted emergency approvers.
- Required justification.
- Short-lived authorization.
- Additional monitoring.
- Incident linkage.
- Post-action review.
- Retrospective evidence.
- Prevention of repeated emergency misuse.

### **5.13 Approval Failure Handling**

- Approver unavailable.
- Policy engine unavailable.
- Approval service timeout.
- Conflicting decisions.
- Evidence missing.
- Invalid approver identity.
- Approval expired during execution preparation.
- Artifact modified after approval.
- Unauthorized replay of approval tokens.
- Safe suspension and recovery of Harness execution.

### **5.14 Human Approval Inside the Harness**

- Approval checkpoints between agent activities and governed actions.
- Approval requirements determined by policy, not agent preference.
- Lead Agent assembling approval requests.
- Reviewer, Validator, and Evaluator evidence aggregation.
- Harness pausing controlled execution.
- Approval decision verification.
- Authorized continuation.
- Rejection handling.
- Run workspace state preservation.
- Recovery from an interrupted approval workflow.

### **5.15 Approval in CI/CD and Production Systems**

- Pull-request authorization.
- Protected branches.
- Environment protection.
- Release approvals.
- Infrastructure deployment authorization.
- Database migration checkpoints.
- Production execution credentials.
- Short-lived authorization.
- Guarding execution against time-of-check/time-of-use changes.

### **5.16 Approval Evidence and Auditability**

- Append-only decision records.
- Identity and authorization evidence.
- Immutable artifact references.
- Policy version preservation.
- Reason codes and justification.
- Approval timestamps.
- Decision history.
- Evidence integrity.
- Restricted access to sensitive approval material.
- Relationship with Chapter 32 — Audit Trails.

### **5.17 Threat Model for Approval Bypass**

- Forged approval messages.
- Prompt injection instructing agents to skip approval.
- Self-approval.
- Unauthorized delegation.
- Replaying an earlier approval.
- Changing artifacts after approval.
- Manipulating risk classifications.
- Bypassing the Harness through direct tool access.
- Fail-open behavior during service outages.
- Required trust boundaries and independent enforcement.

## **6. Professional Diagrams**

### **Diagram 31.1 — Risk-Based Approval Model**

Illustrate the relationship among:

- Proposed engineering change.
- Risk classification.
- Risk factors.
- Organizational policy.
- Approval level.
- Required owners.
- Authorization outcome.

Include a four-level approval model and examples from Alpha Car Detailing.

### **Diagram 31.2 — Approval Decision Flow**

Show:

1. AI Agent proposes an action.
2. Harness identifies the target action.
3. Policy engine determines approval requirements.
4. If no manual approval is required, automated gates and existing permissions still apply.
5. If approval is required, the Harness collects supporting evidence.
6. Authorized human receives the request.
7. Human approves, rejects, or defers.
8. Harness verifies decision validity and conditions.
9. Authorized action proceeds or remains blocked.

Include paths for missing evidence, expiry, and denial.

### **Diagram 31.3 — Separation of Duties**

Illustrate:

- Developer Agent and change requester.
- Reviewer Agent.
- Validator Agent.
- Evaluator Agent.
- Solution Architect.
- Security Owner.
- Production Release Owner.

Show the separation between evidence generation, authorization, and execution.

### **Diagram 31.4 — Human Approval Inside the Harness**

Show the integrated architecture:

```
Engineering Request
        |
        v
    Lead Agent
        |
        v
   Developer Agent
        |
        v
   Reviewer Agent
        |
        v
   Validator Agent
        |
        v
   Evaluator Agent
        |
        v
  Approval Policy Engine
        |
        +---- No Human Approval Required
        |             |
        |             v
        |      Execution Eligibility
        |
        +---- Human Approval Required
                      |
                      v
               Approval Service
                      |
                      v
              Authorized Human
                      |
                      v
              Decision Recorded
                      |
                      v
              Harness Enforcement
                      |
                      v
            Authorized Action / Block
                      |
                      v
                Audit Evidence
```

Explain that this is a conceptual flow: additional approvals may also be required earlier, before sensitive tools, deployments, or other high-risk actions execute.

### **Diagram 31.5 — Re-Approval After Material Change**

Illustrate:

- Initial artifact version.
- Approval tied to a particular digest and scope.
- New implementation change.
- Materiality assessment.
- Approval remains valid only when permitted by policy.
- Material change invalidates previous authorization.
- Updated evidence and renewed approval.
- Controlled execution of the approved revision.

### **Diagram 31.6 — Approval Lifecycle State Machine**

Show pending, approved, rejected, deferred, expired, revoked, and invalidated states, including allowed transitions.

### **Diagram 31.7 — Enterprise Approval Control Plane**

Illustrate the relationship among:

- AI Engineering Harness.
- Policy engine.
- Approval service.
- Enterprise identity provider.
- Approver directory.
- Evidence store.
- CI/CD enforcement.
- Audit subsystem.
- Production deployment infrastructure.

## **7. Hands-on Example — Human Approval in Alpha Car Detailing**

### **7.1 Scenario Overview**

Implement a policy-driven approval workflow around five enterprise engineering situations:

1. Production deployment.
2. API and event contract changes.
3. Security exceptions.
4. High-risk database migrations.
5. Changes to governed Instructions and Skills.

### **7.2 Scenario A — Production Deployment Approval**

- AI-generated fleet booking feature.
- Successful review, validation, and evaluation.
- Production risk classification.
- Release evidence preparation.
- Production approver authorization.
- Deployment blocked until valid authorization.
- Deployment record tied to the approved artifact.

### **7.3 Scenario B — Breaking API Contract Change**

- Modify the corporate fleet booking endpoint.
- Identify backward-incompatible request or response changes.
- Gather API compatibility evidence.
- Identify downstream consumers.
- Obtain API Contract Owner approval.
- Require updated approval after further contract modification.

### **7.4 Scenario C — Event Contract Change**

- Modify the `FleetBookingConfirmed` integration event.
- Assess event versioning and consumer compatibility.
- Collect contract-test evidence.
- Route approval to the Event Contract Owner.
- Block unauthorized publishing of a breaking event revision.

### **7.5 Scenario D — Security Exception**

- A temporary exception is requested for a production deployment.
- Define affected security controls.
- Describe residual risk.
- Set an explicit expiration.
- Obtain Security Owner authorization.
- Require remediation tracking and closure.
- Prevent broad or indefinite security waivers.

### **7.6 Scenario E — Database Migration**

- Modify corporate fleet booking storage.
- Identify destructive or high-risk schema operations.
- Capture migration scripts.
- Evaluate data loss and recovery risks.
- Record backup and rollback considerations.
- Obtain appropriate database and production authorization.
- Bind approval to the exact migration artifact.
- Re-approve when the migration script materially changes.

### **7.7 Scenario F — Governed Instruction Change**

- AI Agent proposes modifying `CLAUDE.md`.
- Determine whether the change affects coding standards, architecture constraints, or security requirements.
- Produce a proposed change rather than directly modifying the governed source.
- Obtain Engineering Standards Owner approval.
- Preserve provenance and version history.
- Apply the authorized revision.

### **7.8 Scenario G — Governed Skill Change**

- AI Agent proposes updating a reusable deployment Skill.
- Identify the impact on future Harness runs.
- Execute Skill tests and validation.
- Require Skill Owner or Platform Authority approval.
- Approve and publish a specific Skill version.
- Prevent unapproved changes from becoming globally active.

### **7.9 Suggested Companion Implementation Artifacts**

- `harness/approval/approval-policy.yaml`
- `harness/approval/approval-request.schema.json`
- `harness/approval/approval-decision.schema.json`
- `harness/approval/check-approval.ps1`
- `harness/approval/examples/`
- `alpha-car-detailing/docs/governance/approval-matrix.md`

These paths represent proposed example artifacts, not a change to the previously established repository architecture.

### **7.10 Hands-on Acceptance Criteria**

- High-risk actions cannot proceed without valid approval.
- Only appropriately authorized identities can approve.
- A requester cannot self-approve when separation of duties is required.
- Every approval is tied to a defined action and artifact version.
- Expired or revoked approvals are rejected.
- Material changes trigger re-approval.
- Approval service failure does not permit unauthorized execution.
- Approval decisions and execution outcomes are traceable.
- Emergency actions follow a separately governed authorization path.

## **8. Claude Example — Enforcing Human Approval with Claude Code**

### **8.1 Scenario**

Use Claude Code to implement an Alpha Car Detailing API enhancement that requires a production database migration.

### **8.2 Claude's Responsibilities**

- Discover repository governance requirements.
- Read relevant Instructions and Steering Notes.
- Identify approval requirements.
- Implement permitted changes.
- Generate review and validation evidence.
- Prepare an approval request.
- Stop before executing a governed action.
- Continue only after the Harness verifies explicit authorization.

### **8.3 Claude Code Approval Mechanisms**

- Repository Instructions through `CLAUDE.md`.
- Task-specific Prompts.
- Reusable Skills.
- Permission boundaries.
- Hooks and deterministic guardrails.
- External approval enforcement.
- Restricted deployment tools.

Distinguish Claude Code's native tool permission mechanisms from enterprise governance approval, which must be independently enforced.

### **8.4 Approval-Aware Workflow**

- Identify approval-required actions.
- Produce a structured change summary.
- Attach test, contract, and migration evidence.
- Submit an approval request.
- Receive the authorization result through the governed workflow.
- Verify scope and expiry.
- Execute only within authorized boundaries.

### **8.5 Failure and Misuse Scenarios**

- Claude attempts a deployment before authorization.
- A prompt instructs Claude to ignore approval policy.
- An approval artifact has expired.
- A migration script changes after approval.
- Approval is claimed in conversation but not present in the authoritative approval store.

### **8.6 Expected Outcomes**

- No unauthorized execution.
- Clear human decision point.
- Deterministic enforcement.
- Complete evidence.
- Reproducible and auditable approval workflow.

## **9. GitHub Copilot Comparison**

### **9.1 Governance Principles Independent of the Tool**

- Human ownership of high-risk decisions.
- Approval boundaries defined by organizational policy.
- Separation of duties.
- Independent authorization enforcement.
- Audit evidence.

### **9.2 GitHub Copilot Workflow Considerations**

- Repository-level Instructions.
- Agent-assisted changes.
- Pull-request review.
- Branch protection and required checks.
- GitHub environment protection.
- Approvals through repository and delivery workflows.

### **9.3 Enterprise Approval Integration**

- Distinguish human code review from formal governance authorization.
- Integrate approval requirements into CI/CD.
- Control production deployment independently of agent output.
- Bind authorization to the artifact being released.

### **9.4 Comparison with Claude Code**

- Instruction mechanisms.
- Agent execution boundaries.
- Approval workflows.
- Integration with organizational identity.
- Auditability.
- Portability of Harness approval policies.

## **10. Codex Comparison**

### **10.1 Codex in Enterprise Approval Workflows**

- AI-generated implementation proposals.
- Agent execution permissions.
- Repository and environment constraints.
- Human interaction and review workflows.
- External authorization enforcement.

### **10.2 Approval-Aware Codex Workflow**

- Generate proposed changes.
- Run permitted validation activities.
- Prepare approval evidence.
- Request formal authorization through the Harness.
- Enforce authorization before a protected action.

### **10.3 Differences from Claude Code**

- Agent configuration and execution environments.
- Tool permission boundaries.
- Approval interaction mechanisms.
- CI/CD integration points.
- Operational governance controls.

### **10.4 Vendor-Neutral Architectural Principle**

The approval policy, decision records, and authorization checks should remain part of the enterprise governance architecture rather than depend on any one AI coding platform.

## **11. Best Practices**

### **11.1 Design Approval Around Risk**

- Use risk classification rather than blanket approval requirements.
- Make approval thresholds explicit.
- Reserve manual approval for meaningful governance decisions.

### **11.2 Enforce Approval Outside the AI Agent**

- Treat AI instructions as guidance, not an authorization boundary.
- Use independently enforced controls.
- Prevent direct access to protected execution paths.

### **11.3 Bind Authorization to Specific Actions and Artifacts**

- Approve immutable artifact versions.
- Restrict authorization to target environments and scopes.
- Verify validity immediately before execution.

### **11.4 Maintain Separation of Duties**

- Restrict self-approval.
- Define approver independence.
- Require multiple authorities where policy demands.

### **11.5 Require Evidence-Based Decisions**

- Supply relevant validation reports.
- Provide architectural and security impact analysis.
- Record residual risks.
- Capture rollback and recovery readiness.

### **11.6 Manage Approval Lifecycle Explicitly**

- Define expiry.
- Support revocation.
- Detect material changes.
- Re-approve when necessary.

### **11.7 Govern Exceptions**

- Use bounded, justified exceptions.
- Set expiration dates.
- Require remediation ownership.
- Audit emergency usage.

### **11.8 Minimize Approval Friction Without Weakening Controls**

- Automate low-risk decisions under approved policy.
- Consolidate evidence.
- Route requests to the correct owners.
- Avoid redundant approvals.
- Monitor approval bottlenecks.

### **11.9 Protect the Approval Mechanism**

- Use authenticated approvers.
- Restrict policy modifications.
- Preserve tamper-evident records.
- Protect against approval replay and impersonation.
- Deny unauthorized actions during outages.

### **11.10 Test Approval Enforcement**

- Missing approval.
- Unauthorized approver.
- Self-approval.
- Expired approval.
- Material artifact change.
- Revoked authorization.
- Conflicting approval decisions.
- Emergency authorization.
- Direct execution bypass attempts.

## **12. Anti-patterns**

### **12.1 Approval by AI Recommendation**

Treating an AI Agent's recommendation as authorization.

### **12.2 Approval by Passing Tests**

Assuming successful deterministic validation grants production deployment authority.

### **12.3 Approval by Conversation**

Accepting phrases such as "approved" in an AI conversation as authoritative evidence.

### **12.4 Self-Approval**

Allowing the same agent or unauthorized requester to produce and approve a high-risk change.

### **12.5 Approval Without Defined Scope**

Granting unlimited authorization for future actions.

### **12.6 Approval Without Artifact Binding**

Approving a change that can subsequently be modified without invalidating authorization.

### **12.7 Permanent Security Exceptions**

Allowing temporary exceptions to become indefinite policy bypasses.

### **12.8 Rubber-Stamp Approval**

Presenting insufficient evidence while treating approval as a formality.

### **12.9 Excessive Manual Approvals**

Requiring human intervention for routine, reversible, low-risk actions without a justified governance need.

### **12.10 Fail-Open Approval Gates**

Allowing execution when the approval service is unavailable.

### **12.11 Uncontrolled Emergency Access**

Using emergency procedures to bypass ordinary accountability.

### **12.12 Silent Modification of Governed Instructions or Skills**

Allowing AI Agents or learning mechanisms to alter persistent engineering standards without authorization.

### **12.13 Approval After Execution**

Treating retrospective acknowledgment as equivalent to prior authorization.

### **12.14 Approval Laundering**

Using an approval granted for one action to justify a different action, environment, or artifact.

## **13. Architect's Notes**

### **13.1 Human Approval Is an Authorization Boundary**

Explain why governance approval belongs in the enterprise control plane, not inside an AI Agent's subjective reasoning.

### **13.2 Approval and Deterministic Validation Are Complementary**

Clarify why neither control replaces the other.

### **13.3 Approval Must Be Bound to Immutable Evidence**

Discuss artifact digests, policy versions, approval records, and execution integrity.

### **13.4 Governance Policies Must Themselves Be Governed**

Address ownership and authorization for approval policy modifications.

### **13.5 Multi-Agent Systems Require Clear Authority Boundaries**

Explain why adding more agents does not create an independent human approval authority.

### **13.6 Avoid Centralized Approval Bottlenecks**

Discuss delegated authority, risk tiers, approval routing, and scalable operating models.

### **13.7 Design for Interrupted and Long-Running Workflows**

Consider persisted Harness state, idempotent continuation, expired approvals, retries, and safe recovery.

### **13.8 Protect Against Time-of-Check/Time-of-Use Risks**

Explain how an approved artifact or environment can change between authorization and execution.

### **13.9 Differentiate Approval of a Change from Approval of Its Consequences**

Discuss residual risk acceptance, operational readiness, and accountability.

### **13.10 Governance in Self-Learning Harnesses**

Reinforce that learning mechanisms may propose changes to Skills, Prompts, Instructions, and standards but cannot silently publish governed revisions.

## **14. Enterprise Tips**

### **14.1 Establish a Formal Approval Matrix**

Identify change categories, risk levels, approval owners, and evidence requirements.

### **14.2 Integrate Enterprise Identity**

Use centrally managed identities, roles, and delegated authority.

### **14.3 Reuse Existing Enterprise Governance**

Integrate with established architecture review, security approval, change management, and production release controls instead of creating unnecessary parallel bureaucracy.

### **14.4 Automate Evidence Collection**

Collect build, test, architecture, security, and evaluation results from the Harness.

### **14.5 Make Approval Status Visible**

Expose pending, approved, rejected, expired, and invalidated states in the Harness management interface.

### **14.6 Define Approval Service-Level Objectives**

Measure request turnaround, backlog, and emergency approval frequency without incentivizing rubber-stamping.

### **14.7 Establish Explicit Emergency Procedures**

Define eligible situations, authorized responders, required evidence, and retrospective review.

### **14.8 Maintain Approval Records for Audit and Compliance**

Align retention, access controls, and integrity requirements with organizational policy.

### **14.9 Review Approval Policies Periodically**

Use incidents, near misses, delivery delays, and audit findings to refine risk-based thresholds.

## **15. Decision Points**

### **Decision Point 31.1 — When Is Human Approval Mandatory?**

- Risk threshold.
- Target environment.
- Reversibility.
- Compliance requirements.
- Business impact.

### **Decision Point 31.2 — Who Is Authorized to Approve?**

- Resource ownership.
- Architectural responsibility.
- Security authority.
- Operational accountability.
- Delegation eligibility.

### **Decision Point 31.3 — How Many Approvals Are Necessary?**

- Single approver.
- Multiple independent approvers.
- Sequential or parallel approval.
- Conditional authorization.

### **Decision Point 31.4 — What Evidence Is Sufficient?**

- Validation evidence.
- Evaluation findings.
- Architecture impact.
- Security assessment.
- Rollback plan.
- Consumer compatibility.

### **Decision Point 31.5 — When Should Approval Expire?**

- Fixed duration.
- Deployment window.
- Artifact change.
- Policy revision.
- Explicit revocation.

### **Decision Point 31.6 — What Constitutes a Material Change?**

- Contract changes.
- Migration changes.
- Security-impacting changes.
- Target environment changes.
- Change scope expansion.

### **Decision Point 31.7 — Should the Harness Pause or Terminate?**

- Pending human approval.
- Rejected approval.
- Expired approval.
- Failed authorization service.
- Safe resume and retry behavior.

### **Decision Point 31.8 — When Is Emergency Approval Acceptable?**

- Business continuity.
- Security incidents.
- Production outages.
- Time-critical remediation.
- Retrospective accountability.

### **Decision Point 31.9 — Who Approves Changes to Governance Controls?**

- Approval policy owner.
- Engineering standards authority.
- Security governance.
- Separation of policy authoring and policy activation.

### **Decision Point 31.10 — How Is Approval Enforcement Tested?**

- Threat-based testing.
- Policy conformance.
- Negative authorization tests.
- CI/CD integration tests.
- Audit evidence verification.

## **16. Exercises**

### **Exercise 31.1 — Design an Approval Matrix**

Create a risk-based approval matrix for Alpha Car Detailing covering API changes, event changes, database migrations, security exceptions, production releases, and governed Instruction/Skill revisions.

**Expected deliverable:** An approval matrix with owners, approval levels, evidence requirements, and expiry policies.

### **Exercise 31.2 — Implement an Approval Gate**

Design a deterministic Harness checkpoint that prevents protected actions from executing without valid authorization.

**Expected deliverable:** Approval gate pseudocode or implementation, with positive and negative test cases.

### **Exercise 31.3 — Implement Separation of Duties**

Define controls that prevent an implementation requester from approving the same high-risk change.

**Expected deliverable:** Authorization rules and test scenarios.

### **Exercise 31.4 — Design an Approval Request Schema**

Model approval requests, decisions, approver identities, scopes, evidence, and timestamps.

**Expected deliverable:** JSON Schema or equivalent structured contract.

### **Exercise 31.5 — Implement Material Change Detection**

Demonstrate how changes to an approved database migration invalidate its prior authorization.

**Expected deliverable:** Artifact digest comparison and re-approval workflow.

### **Exercise 31.6 — Design a Security Exception Workflow**

Create a bounded security exception process for Alpha Car Detailing.

**Expected deliverable:** Exception request, risk justification, expiration, approver requirements, and remediation plan.

### **Exercise 31.7 — Govern Instruction and Skill Updates**

Design a workflow for approving AI-proposed modifications to `CLAUDE.md` and a reusable deployment Skill.

**Expected deliverable:** Proposed changes, validation evidence, authorization process, and publication controls.

### **Exercise 31.8 — Design an Emergency Approval Mechanism**

Model a production incident requiring urgent corrective action.

**Expected deliverable:** Break-glass procedure, permissions, audit evidence, and post-action review.

### **Exercise 31.9 — Threat-Model Approval Bypass**

Identify ways AI Agents or compromised execution environments could bypass approval controls.

**Expected deliverable:** Threat model and mitigating enforcement mechanisms.

### **Exercise 31.10 — Integrate Human Approval into the Complete Harness**

Extend the Alpha Car Detailing Harness architecture with an approval control plane.

**Expected deliverable:** Architecture diagram, policy model, approval state machine, enforcement points, and end-to-end acceptance tests.

## **17. Interview Questions**

### **Foundational Questions**

1. What is Human Approval in enterprise AI Engineering?
2. Why is Human Approval necessary when automated tests and reviews pass?
3. How does approval differ from review?
4. How does approval differ from validation?
5. How does approval differ from evaluation?
6. What is the difference between an AI Agent's technical capability and its organizational authority?

### **Architecture Questions**

1. How would you design Human Approval as a first-class capability of an AI Engineering Harness?
2. Why should approval enforcement be independent of AI Agent instructions?
3. How should an approval policy engine determine whether authorization is required?
4. How would you design an approval state machine?
5. How would you integrate Human Approval into CI/CD and production deployments?
6. How do you enforce separation of duties in a multi-agent system?
7. What should happen when the approval service is unavailable?

### **Security and Governance Questions**

 1. How can approval decisions be protected against forgery or replay?
 2. Why should approvals be associated with immutable artifacts?
 3. What conditions should trigger re-approval?
 4. How would you design approval expiry and revocation?
 5. How would you govern temporary security exceptions?
 6. What are the risks of unrestricted emergency approval?
 7. Who should be authorized to modify approval policies?

### **Scenario-Based Questions**

 1. An AI Agent changes a production database migration after approval. What should the Harness do?
 2. All validation gates pass, but an event contract is backward-incompatible. Can the change proceed?
 3. An AI Agent recommends removing a security restriction. What approvals are necessary?
 4. A production deployment is approved, but the artifact digest changes before execution. What should happen?
 5. An authorized approver is unavailable during a critical incident. How should the system respond?
 6. A self-learning Harness proposes modifying `CLAUDE.md`. How should the change be governed?
 7. How would you implement a scalable approval workflow without creating excessive delivery delays?
 8. How would you demonstrate to an auditor that no unauthorized production action occurred?

## **18. Chapter Summary**

Structure the summary around the following conclusions:

### **18.1 Human Approval Preserves Accountability**

AI Agents can produce evidence and recommendations, but accountable humans retain authority over governed decisions.

### **18.2 Approval Is Distinct from Engineering Quality Controls**

Review, validation, evaluation, and approval provide different forms of assurance and must remain separate.

### **18.3 Approval Must Be Risk-Based**

Not every engineering action needs manual intervention. Approval policies should match the impact and sensitivity of the action.

### **18.4 Approval Must Be Enforceable**

Organizational authorization cannot rely solely on AI Agent instructions or conversational confirmation.

### **18.5 Approval Must Be Scoped and Time-Bounded**

Approvals should identify the authorized action, relevant artifacts, target environment, responsible approver, and validity period.

### **18.6 Material Changes Require Renewed Authorization**

Changes that invalidate the basis of approval must trigger re-evaluation and re-approval.

### **18.7 Separation of Duties Is Essential**

Implementation, evidence generation, approval, and execution should have appropriately separated responsibilities.

### **18.8 Governance Must Support Enterprise Delivery**

Well-designed approval mechanisms balance safety, accountability, auditability, and engineering velocity.

### **18.9 Human Approval Completes the Harness Governance Boundary**

The Harness must be capable of stopping, suspending, resuming, or rejecting work according to verified authorization decisions.

### **18.10 Human Approval Enables Governed AI Autonomy**

Increasing agent autonomy requires stronger authorization boundaries, not the elimination of human accountability.

## **19. Further Reading**

Organize the references into the following categories. Verify current titles, versions, and applicability during chapter drafting.

### **19.1 Enterprise Governance and Risk**

- NIST AI Risk Management Framework.
- ISO/IEC 42001 — Artificial Intelligence Management Systems.
- ISO 31000 — Risk Management.
- COBIT governance framework.

### **19.2 Security and Authorization**

- NIST SP 800-53 — Security and Privacy Controls.
- NIST SP 800-207 — Zero Trust Architecture.
- OWASP guidance on authorization and access control.
- OWASP guidance on agentic AI security risks.

### **19.3 Software Delivery Governance**

- GitHub documentation on branch protection and deployment environments.
- Azure DevOps documentation on approvals and checks.
- Microsoft guidance on secure deployment and change management.

### **19.4 Architecture and Change Control**

- Architecture Decision Records.
- Enterprise architecture governance practices.
- Database migration and release safety patterns.
- API and event contract versioning practices.

### **19.5 AI Agent Governance**

- Anthropic Claude Code documentation on permissions, Instructions, and Hooks.
- GitHub Copilot documentation on agent workflows and governance.
- OpenAI Codex documentation on agent execution, permissions, and safety boundaries.

### **19.6 Handbook Cross-References**

- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 29 — AI Governance.
- Chapter 30 — Security.
- Chapter 32 — Audit Trails.
- Subsequent chapters on Policies, Compliance, and Self-Learning AI Systems.

## **Chapter Coverage and Editorial Acceptance Criteria**

The completed chapter should satisfy the following requirements:

- Preserve the frozen 19-section chapter structure.
- Maintain the distinction between review, validation, evaluation, and approval.
- Clearly define the limits of AI Agent authority.
- Explain risk-based approval, ownership, delegation, and separation of duties.
- Cover architecture, security, production, privacy, contracts, migrations, and governed Instructions and Skills.
- Include enforceable authorization gates rather than relying solely on prompts.
- Describe approval evidence, validity, expiry, revocation, and re-approval.
- Include emergency approval and failure-handling mechanisms.
- Demonstrate integration into the existing multi-agent Harness.
- Include the five required diagrams and the additional lifecycle and control-plane diagrams.
- Use realistic Alpha Car Detailing scenarios throughout.
- Provide Claude-first examples with meaningful GitHub Copilot and Codex comparisons.
- Include actionable best practices, anti-patterns, exercises, and interview questions.
- Maintain vendor-neutral architectural principles.
- Avoid duplicating the broader security, audit-trail, and compliance chapters.
- Reinforce the established principle that a self-learning Harness cannot silently modify governed engineering standards.

**Chapter 31 outline is complete.**
