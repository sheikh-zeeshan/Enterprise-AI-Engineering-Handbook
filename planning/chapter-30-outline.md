# **Chapter 30 — Security**

Part V — Enterprise AI Governance

**Project:** Enterprise AI Engineering Handbook\
**Repository:** [Enterprise-AI-Engineering-Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)\
**Target file:** `book/30-security.md`\
**Chapter type:** Enterprise architecture, AI agent security, and Harness security\
**Primary platform:** Claude Code\
**Comparative platforms:** GitHub Copilot and OpenAI Codex\
**Running example:** Alpha Car Detailing

## **Chapter Objective**

Explain how enterprises can securely adopt AI-assisted software engineering by protecting repositories, data, credentials, execution environments, tools, infrastructure, and production systems from unauthorized or unsafe AI Agent activities.

The chapter establishes a security architecture for AI Engineering that integrates identity, authorization, trust boundaries, controlled execution, deterministic security gates, human oversight, and auditable operations throughout the engineering lifecycle.

**Core principle:**

> AI Agents must operate with least privilege, explicit trust boundaries, controlled tool access, protected data, deterministic security checks, and human approval for high-risk actions.

Security must be enforced by the environment and Harness, not merely requested through Instructions, Prompts, or Steering Notes.

## **1. Story-Driven Opening — When a Coding Agent Becomes a Security Risk**

### **1.1 The Alpha Car Detailing scenario**

- Alpha Car Detailing supports individual customers, corporate fleets, government departments, and insurance organizations.
- Its enterprise platform contains confidential corporate customer records, government fleet information, API credentials, SQL Server databases, and production deployment infrastructure.
- A development team assigns an AI Agent to extend the corporate fleet booking API.
- The agent accesses repository files, reads development Instructions, installs a dependency, executes tests, and proposes a database migration.
- A malicious instruction embedded in a retrieved repository document attempts to persuade the agent to export customer data to an external endpoint.
- During implementation, the agent also attempts to access a production connection string that is unnecessary for the task.

### **1.2 The security dilemma**

- The agent is authorized to implement an application feature but not to access every connected system.
- Repository content is useful engineering context but is not necessarily trustworthy.
- A successful compilation does not establish that the implementation or its execution process was secure.
- Human approval of a task does not automatically authorize every operation performed while completing it.

### **1.3 The architectural response**

- Enforce identity-based access to approved resources.
- Separate trusted execution Instructions from untrusted retrieved content.
- Restrict tools, filesystem access, network destinations, and credentials.
- Validate security-relevant changes through independent deterministic gates.
- Escalate sensitive operations for human authorization.
- Preserve evidence for auditing and incident investigation.

### **1.4 Opening architectural question**

How can Alpha Car Detailing give AI Agents enough capability to deliver enterprise software changes without granting them uncontrolled access to enterprise assets?

## **2. Learning Objectives**

By the end of this chapter, readers should be able to:

1. Identify threats introduced by AI Agents, connected tools, repositories, and external knowledge sources.
2. Define trust boundaries between users, agents, Harnesses, execution environments, tools, and enterprise systems.
3. Design identity, authentication, authorization, and least-privilege controls for AI-assisted engineering.
4. Protect credentials, sensitive data, repositories, and production infrastructure.
5. Recognize prompt injection, malicious repository instructions, and data exfiltration attempts.
6. Implement secure tool execution using permissions, isolation, and network restrictions.
7. Integrate supply-chain scanning and deterministic security gates into an enterprise Harness.
8. Establish security responsibilities across Lead, Developer, Reviewer, Validator, and Evaluator Agents.
9. Define approval workflows, security exceptions, audit evidence, and incident response procedures.
10. Apply security patterns to Claude Code and compare relevant platform differences.
11. Design a repeatable, reusable enterprise AI Engineering security baseline.

## **3. Background — Security in AI-Assisted Software Engineering**

### **3.1 Traditional software engineering security**

- Application security and secure coding standards.
- Identity and Access Management.
- Developer workstation security.
- Repository and source-control protections.
- CI/CD pipeline security.
- Infrastructure and cloud security.
- Secrets, credentials, and data protection.

### **3.2 What changes with AI Agents**

- Agents can plan and execute sequences of engineering operations.
- Tools extend agent capabilities beyond text generation.
- Agents may retrieve and interpret untrusted content.
- Multiple tools and systems can be combined within a single run.
- Operations can occur at machine speed without continuous human inspection.
- Agents can propagate unsafe changes across source code, dependencies, infrastructure, and deployment configurations.

### **3.3 Security implications of increasing autonomy**

- Advisory agents versus agents capable of modifying systems.
- Read-only versus write-capable agents.
- Human-supervised versus autonomous execution.
- Local development versus remote execution.
- Single-agent versus multi-agent workflows.
- Development environments versus production environments.

### **3.4 Security as an engineering control**

- Preventive, detective, and corrective controls.
- Policy enforcement versus natural-language guidance.
- Permission boundaries versus behavioral expectations.
- Defense in depth.
- Fail-closed behavior for high-risk operations.
- Explicitly documented residual risks.

### **3.5 Scope and relationship to other chapters**

- Chapter 29 — AI Governance: policies, accountability, and ownership.
- Chapter 17 — Validator Agent: deterministic engineering evidence.
- Chapter 18 — Evaluator Agent: outcome evaluation and acceptance criteria.
- Chapter 26 — Tool Calling: tool execution mechanisms.
- Chapter 31 — Human Approval: detailed approval models.
- Chapter 32 — Audit Trails: enterprise-wide evidence and traceability.
- Chapter 34 — Compliance: regulatory and contractual obligations.
- Chapter 51 — Security: application-level security implementation in Alpha Car Detailing.

## **4. Concepts — The Enterprise AI Security Model**

### **4.1 AI Engineering threat model**

#### **4.1.1 Protected assets**

- Source code and intellectual property.
- Corporate and government customer information.
- API keys, tokens, credentials, and certificates.
- Databases and production records.
- Build artifacts and software packages.
- Deployment pipelines and cloud resources.
- Harness configurations, security policies, and approval records.
- Trusted Instructions, Skills, and knowledge repositories.

#### **4.1.2 Threat actors and sources**

- External attackers.
- Malicious repository contributors.
- Compromised dependencies and packages.
- Untrusted websites or external knowledge sources.
- Compromised tools or integration servers.
- Malicious or misconfigured workflow participants.
- Accidental misuse by authorized users or agents.

#### **4.1.3 Threat categories**

- Prompt injection and instruction manipulation.
- Unauthorized tool execution.
- Privilege escalation.
- Credential exposure and theft.
- Sensitive data leakage.
- Malicious code generation.
- Dependency and supply-chain compromise.
- Repository poisoning.
- Unsafe infrastructure modifications.
- Unauthorized deployment.
- Cross-agent and cross-run contamination.
- Audit evidence tampering.

#### **4.1.4 Threat modeling techniques**

- Map assets, entry points, trust boundaries, and data flows.
- Apply established threat modeling approaches such as STRIDE where useful.
- Assess likelihood, impact, detectability, and business criticality.
- Distinguish attacks on the agent from vulnerabilities introduced into generated software.
- Record threats, mitigations, residual risks, and control owners.

### **4.2 Trust boundaries**

#### **4.2.1 Trusted control surfaces**

- Enterprise security policies.
- Approved Harness execution policies.
- Identity and authorization services.
- Protected validation gates.
- Human approval systems.

#### **4.2.2 Lower-trust content and surfaces**

- User-supplied task content when the requester lacks authority for the requested action.
- Repository files and pull-request descriptions.
- Documentation and comments from unknown or compromised sources.
- Retrieved websites and search results.
- External tools and integration responses.
- Generated code and agent-produced artifacts.
- Scratchpads, run workspaces, and imported memories.

#### **4.2.3 Trust boundary principles**

- Data does not become authoritative simply because an agent reads it.
- Retrieved content cannot grant privileges.
- Role instructions cannot supersede independently enforced security policy.
- Delegation must not increase effective privileges.
- Context and knowledge provenance must be preserved.
- Trust must be reassessed when information crosses system boundaries.

### **4.3 Identity and authentication**

- Human identities versus AI workload identities.
- Service principals, managed identities, and workload federation.
- Authentication of tools, Harness services, and integrations.
- Short-lived credentials and token lifetimes.
- Distinct identities for development, testing, and production.
- Credential rotation and revocation.
- Non-repudiation and actor attribution.
- Avoiding shared service accounts and long-lived personal access tokens.

### **4.4 Authorization and least privilege**

- Role-Based Access Control (RBAC).
- Attribute-Based Access Control (ABAC) where appropriate.
- Task-scoped permissions.
- Repository-scoped permissions.
- Environment-scoped permissions.
- Read versus write versus execute access.
- Time-bound and just-in-time authorization.
- Deny-by-default access.
- Separation of duties.
- Permission revocation at run completion.
- Authorization independent of agent claims or self-assigned roles.

### **4.5 Tool permissions and controlled execution**

- Inventory and classification of available tools.
- File read, file write, shell command, network, database, and deployment permissions.
- Tool allowlists and operation-specific restrictions.
- Dangerous command classification.
- Permission checks before invocation.
- Argument and destination validation.
- Enforcement at the actual tool or service boundary.
- Approval requirements for elevated operations.
- Handling chained operations and indirect tool calls.
- Avoiding security bypass through alternative tools.
- Revoking access when the requested operation exceeds the task scope.

### **4.6 Repository security**

- Repository permissions and branch protections.
- Protected branches and mandatory pull requests.
- Signed commits and artifact provenance where supported.
- Untrusted pull requests and external contributors.
- Repository Instructions as guidance rather than authorization.
- Security review of changes to `CLAUDE.md`, `AGENTS.md`, Skills, and Harness scripts.
- Repository access segmentation.
- Read-only clones and isolated worktrees.
- Preventing unauthorized changes to security gate definitions.
- Protecting CODEOWNERS, CI configurations, and security policies.

### **4.7 Secrets management**

- Identifying secrets across engineering workflows.
- Secret stores and vault integration.
- Short-lived credentials and workload identity.
- Avoiding hardcoded credentials in source code.
- Secrets in Prompts, tool arguments, logs, and generated artifacts.
- Redaction versus complete exclusion from agent context.
- Scoped secret injection into approved processes.
- Credential rotation after suspected exposure.
- Secrets in environment variables and process inheritance.
- Preventing leakage through build output, temporary files, and debugging sessions.
- Handling confidential `.env` files and local configuration.

### **4.8 Sensitive data protection**

- Data classification and handling policies.
- Customer personal information.
- Corporate fleet operational data.
- Government customer records.
- Database schemas versus actual production records.
- Data minimization.
- Masking, tokenization, and synthetic test data.
- Encryption in transit and at rest.
- Data residency and approved processing locations.
- Model-provider and tool-provider data-handling boundaries.
- Retention and deletion requirements.
- Secure treatment of logs, transcripts, scratchpads, and run artifacts.

### **4.9 Prompt injection**

- Direct versus indirect prompt injection.
- Instructions concealed in repository documentation.
- Malicious instructions inside comments, issue descriptions, and tool responses.
- Attacker-controlled content masquerading as system or enterprise policy.
- Attempts to override higher-trust Instructions.
- Requests to execute dangerous commands or disclose credentials.
- Cross-agent instruction propagation.
- Distinguishing legitimate task data from attempted behavioral redirection.
- Source provenance and context labeling.
- Defense through authorization, restricted tools, isolation, and output checks.
- Why natural-language warnings alone are insufficient.

### **4.10 Malicious repository content**

- Poisoned `README.md` files.
- Modified `CLAUDE.md` or `AGENTS.md`.
- Compromised Skills and command definitions.
- Malicious build scripts and package hooks.
- Suspicious shell and PowerShell scripts.
- Repository-controlled tests that execute unintended operations.
- Tampered CI/CD workflows.
- Untrusted submodules and downloaded assets.
- Dependency installation side effects.
- Security implications of executing code while analyzing a repository.

### **4.11 Data leakage and exfiltration**

- Leakage through tool outputs.
- External API calls and web requests.
- Unauthorized uploads and generated artifacts.
- Clipboard, terminal, and logging exposure.
- Git commits and pull-request descriptions.
- Remote diagnostics and telemetry.
- Cross-project and cross-customer data exposure.
- Egress policies, domain allowlists, and destination verification.
- Limits of redaction and output inspection.
- Security response when leakage is suspected.

### **4.12 Software supply-chain security**

- Dependency provenance and package integrity.
- Trusted package registries and mirrors.
- Dependency pinning and reproducible builds.
- Software Bill of Materials (SBOM).
- Software Composition Analysis (SCA).
- Known vulnerability detection.
- Malicious packages and dependency confusion.
- Typosquatting and compromised maintainers.
- Build workflow and action security.
- Artifact integrity and provenance attestations.
- Dependency updates proposed by AI Agents.
- Approval policies for new packages and major version changes.

### **4.13 Code and secret scanning**

- Static Application Security Testing (SAST).
- Secret scanning.
- Software Composition Analysis.
- Infrastructure-as-Code scanning.
- Container image scanning.
- License and dependency-policy checks.
- False positives, false negatives, and suppression governance.
- Baseline findings versus newly introduced vulnerabilities.
- Severity thresholds and remediation requirements.
- Scanning generated code without assuming it is trustworthy.

### **4.14 Container and infrastructure security**

- Minimal and approved base images.
- Container image provenance.
- Image scanning and vulnerability policies.
- Non-root execution.
- Read-only filesystems where practical.
- Resource and capability restrictions.
- Runtime security monitoring.
- Infrastructure-as-Code validation.
- Cloud IAM policy review.
- Kubernetes admission controls.
- Deployment configuration security.
- Protected secrets and service connections.

### **4.15 Sandboxing and isolation**

- Process isolation.
- Container isolation.
- Dedicated execution workers.
- Ephemeral environments.
- Filesystem boundaries.
- CPU, memory, execution-time, and process limits.
- Workspace isolation per run.
- Separate execution environments for untrusted contributions.
- Sandboxed testing of generated code.
- Isolation limitations and defense-in-depth requirements.

### **4.16 Network access controls**

- Network access as an explicit capability.
- Deny-by-default egress.
- Approved package registries.
- Restricted external endpoints.
- Proxy enforcement and traffic monitoring.
- Internal API access controls.
- DNS and outbound destination restrictions.
- Blocking access to cloud metadata endpoints when unnecessary.
- Network segmentation between development and production.
- Controlled access to model providers and external tools.

### **4.17 Production access**

- Development versus staging versus production privileges.
- Production credentials excluded from routine coding-agent sessions.
- Read-only production diagnostics when explicitly authorized.
- Change control and deployment approvals.
- Separation of build, release, and deployment responsibilities.
- Deployment service identities.
- Emergency access procedures.
- Rollback authorization.
- Protecting production databases and infrastructure.
- Preventing prompt-based authorization of production commands.

### **4.18 Security gates**

- Security checks as deterministic engineering controls.
- Build checks versus dedicated security checks.
- Required versus advisory gates.
- Protected gate configuration.
- Gate ownership and execution independence.
- Vulnerability severity thresholds.
- Secret exposure blocking.
- Dependency approval checks.
- Infrastructure policy validation.
- Deployment eligibility checks.
- Timeouts, scanner failures, and inconclusive results.
- Fail-closed behavior for required gates.
- Evidence retention and independent verification.
- Security exception workflows.

### **4.19 Audit trails and security evidence**

- Actor identity and initiating request.
- Effective permissions and execution context.
- Tool invocations and authorization decisions.
- Changes to repositories and infrastructure.
- Security gate outcomes.
- Human approvals and exception records.
- Artifact hashes and execution identifiers.
- Tamper-resistant storage.
- Sensitive data minimization in audit records.
- Correlating agent runs with CI/CD and cloud audit events.

### **4.20 Incident response**

- Detecting suspicious agent behavior.
- Unauthorized command execution.
- Credential exposure.
- Malicious repository modifications.
- Data leakage.
- Supply-chain compromise.
- Immediate suspension and containment of affected runs.
- Credential and token revocation.
- Preserving security evidence.
- Root-cause investigation.
- Remediation and controlled recovery.
- Reviewing policy and gate coverage after incidents.

### **4.21 Human approval and security exceptions**

- Approval as a security control.
- Risk-based approval levels.
- High-risk commands and data access.
- Production and infrastructure operations.
- New dependencies and privileged integrations.
- Explicit approval scope and expiration.
- Human identity and accountability.
- Approval versus execution authorization.
- Exception ownership, justification, expiration, and compensating controls.
- Prohibiting agents from approving their own high-risk operations.
- Denying execution when mandatory approval cannot be verified.

### **4.22 Multi-agent security**

- Isolation between agent roles.
- Delegation with restricted capability subsets.
- Preventing cross-agent privilege escalation.
- Securing shared context and artifacts.
- Security of handoff messages.
- Treating another agent's output as untrusted evidence until verified.
- Run-specific scratchpad permissions.
- Preventing cross-run and cross-tenant data contamination.
- Securing parallel agent execution.
- Preventing collusion-like authorization bypass through chained delegation.
- Security enforcement by the Harness rather than agent agreement.

## **5. Architecture Discussion — Designing a Secure Enterprise AI Harness**

### **5.1 Enterprise security reference architecture**

- Human request and task authorization layer.
- Enterprise identity and access layer.
- Harness orchestration and security policy layer.
- AI Agent execution layer.
- Tool authorization and execution gateway.
- Repository and knowledge access layer.
- Isolated execution environments.
- Independent validation and security gate layer.
- Approval and deployment control layer.
- Centralized security evidence and audit layer.

### **5.2 Security responsibilities by component**

| Component        | Security responsibility                                          |
| ---------------- | ---------------------------------------------------------------- |
| Human Requester  | Initiates authorized work within assigned privileges             |
| Lead Agent       | Plans work within approved boundaries                            |
| Developer Agent  | Implements changes with restricted tools                         |
| Reviewer Agent   | Reviews security implications and implementation quality         |
| Validator Agent  | Runs and reports deterministic security checks                   |
| Evaluator Agent  | Assesses security-related acceptance criteria and residual risks |
| Harness          | Enforces execution policy, boundaries, and workflow transitions  |
| Tool Gateway     | Authorizes individual tool operations                            |
| Security Gates   | Independently verify mandatory security conditions               |
| Approval Service | Confirms authorized human decisions                              |
| Audit Service    | Preserves security-relevant execution evidence                   |

### **5.3 Security control plane versus execution plane**

- Centralized policies versus individual agent execution.
- Separating control decisions from tool execution.
- Independent policy enforcement.
- Preventing agents from modifying their own effective security controls.
- Preserving enforcement when workflows run autonomously.
- Securing the Harness orchestration service itself.

### **5.4 Security policy hierarchy**

- Enterprise security requirements.
- Project and repository security policies.
- Task-specific access restrictions.
- Agent role permissions.
- Tool-specific execution constraints.
- Environment-specific restrictions.
- Explicit handling of conflicting Instructions and authorization requests.

### **5.5 Security inside the Harness**

#### **5.5.1 Before execution**

- Authenticate the requesting identity.
- Validate task authorization.
- Classify data and requested operations.
- Assign a restricted agent identity.
- Establish the allowed tools and destinations.
- Provision an isolated run workspace.
- Resolve approved credentials without exposing unnecessary secrets.

#### **5.5.2 During execution**

- Inspect tool requests against policy.
- Enforce filesystem and network restrictions.
- Record security-relevant operations.
- Detect suspicious commands and policy violations.
- Protect secrets and sensitive outputs.
- Restrict delegation between agents.
- Stop or escalate unauthorized operations.

#### **5.5.3 After implementation**

- Execute deterministic security gates.
- Evaluate security findings.
- Require appropriate review and approval.
- Verify artifact and build provenance.
- Create protected pull requests.
- Retain audit evidence.
- Revoke temporary access and clean up sensitive run data.

### **5.6 Security of Harness configuration and lifecycle**

- Version-controlled security policies.
- Protected gate definitions.
- Trusted Harness releases.
- Restricted modification of orchestration scripts.
- Controlled updates to Instructions and Skills.
- Security regression testing.
- Secure Harness upgrades.
- Rollback and recovery procedures.

### **5.7 Security failure handling**

- Denied operations.
- Unauthorized tool requests.
- Missing credentials.
- Scanner failures.
- Incomplete security evidence.
- Tool gateway outages.
- Suspicious agent behavior.
- Mandatory gate failures.
- Failed human authorization.
- Recovery without silently weakening security policy.

## **6. Professional Diagrams**

The chapter will include six primary architecture diagrams.

### **Diagram 30.1 — AI Engineering Threat Model**

**Purpose:** Illustrate security threats affecting AI Agents, repositories, tools, data, and enterprise infrastructure.

Include:

- Requester and AI Agent.
- Repository and knowledge sources.
- External packages and tool integrations.
- Harness and execution environment.
- Secrets and sensitive data stores.
- Enterprise cloud and production systems.
- Trust boundaries and attack entry points.
- Threat paths for injection, credential theft, privilege escalation, and exfiltration.
- Mitigating security controls.

### **Diagram 30.2 — Enterprise Trust Boundary Model**

**Purpose:** Distinguish trusted enforcement mechanisms from lower-trust information and execution surfaces.

Include:

- Enterprise identity boundary.
- Harness policy enforcement boundary.
- AI Agent execution boundary.
- Tool execution boundary.
- Repository and external knowledge boundary.
- Customer and production data boundary.
- External network boundary.
- Authorized and prohibited interactions.

### **Diagram 30.3 — Tool Permission and Authorization Flow**

**Purpose:** Demonstrate how a Harness decides whether an AI Agent may execute a tool operation.

Include:

1. Agent requests an operation.
2. Tool gateway identifies the requester and run.
3. Policy engine evaluates effective permissions.
4. Operation arguments and destination are validated.
5. Risk classification is performed.
6. High-risk operations require human approval.
7. Authorized operations execute in a restricted environment.
8. Execution results and policy decisions are recorded.
9. Unauthorized requests are denied.

Show separate allow, deny, and approval-required outcomes.

### **Diagram 30.4 — Deterministic Security Gate Pipeline**

**Purpose:** Illustrate how code changes are independently checked before promotion.

Include:

- Agent implementation output.
- Source and dependency inspection.
- Secret scanning.
- SAST.
- Software Composition Analysis.
- Infrastructure-as-Code scanning.
- Container scanning.
- Security policy and contract checks.
- Aggregated gate results.
- Exception evaluation.
- Human review and approval.
- Pull-request merge and deployment eligibility.

Include pass, fail, and inconclusive outcomes.

### **Diagram 30.5 — Prompt Injection Defense Flow**

**Purpose:** Demonstrate how malicious instructions entering through repository or retrieved content are prevented from gaining execution authority.

Include:

- Untrusted content source.
- Agent context ingestion.
- Content provenance and trust classification.
- Suspicious instruction detection as a supporting control.
- Proposed agent action.
- Independent authorization and tool restrictions.
- Safe execution or denied operation.
- Security event recording.
- Escalation and incident handling.

Clarify that detection is imperfect and authorization remains mandatory.

### **Diagram 30.6 — Security Inside the Enterprise Harness**

**Purpose:** Show end-to-end security controls across the existing multi-agent Harness architecture.

Include:

- Task intake and authorization.
- Lead Agent.
- Developer Agent.
- Reviewer Agent.
- Validator Agent.
- Evaluator Agent.
- Policy engine.
- Tool gateway.
- Run-specific sandbox.
- Protected security gates.
- Approval service.
- Pull-request and deployment controls.
- Audit and evidence store.
- Shared security enforcement across all agent stages.

**Diagram standards:** Use consistent naming, clear trust boundaries, labeled permission flows, and vendor-neutral architecture. Store diagrams under `diagrams/` and reference them from Chapter 30.

## **7. Hands-on Example — Securing Alpha Car Detailing AI Engineering**

### **7.1 Scenario: Corporate fleet booking enhancement**

- Implement a corporate fleet booking API enhancement in the existing .NET 10 application.
- Preserve corporate customer data isolation.
- Protect government customer information.
- Use approved development and test databases.
- Prevent unnecessary access to production systems.
- Apply deterministic security checks before pull-request submission.

### **7.2 Define a security baseline**

Create an example policy covering:

- Authorized repositories.
- Allowed agent roles.
- Permitted filesystem operations.
- Allowed command categories.
- Approved package sources.
- Database access restrictions.
- Network egress controls.
- Protected files and security configurations.
- Mandatory security gates.
- High-risk approval requirements.

### **7.3 Implement restricted agent identities**

- Create a task-specific development identity.
- Assign minimum repository permissions.
- Allow approved test environment access.
- Deny production database access.
- Restrict deployment privileges.
- Define token expiration and revocation.

### **7.4 Secure API key handling**

- Configure a secret store.
- Use workload identity or appropriately scoped credentials.
- Prevent secrets from entering Prompts and agent context.
- Test whether logs and artifacts expose secrets.
- Demonstrate credential revocation after a simulated incident.

### **7.5 Secure database access**

- Provide a dedicated development database.
- Use synthetic corporate and government fleet data.
- Restrict schema modification permissions.
- Protect connection strings.
- Require approval for destructive migrations.
- Verify unauthorized production queries are rejected.

### **7.6 Simulate malicious repository instructions**

- Place a harmless, simulated prompt injection inside a repository document.
- Request an unrelated prohibited action.
- Demonstrate how the agent may encounter the content.
- Show how independent tool controls deny the attempted operation.
- Capture denial evidence without exposing real data.
- Explain residual prompt injection risks.

### **7.7 Control dependency changes**

- Request a new dependency for the booking enhancement.
- Verify package source and package integrity.
- Review known vulnerabilities.
- Inspect licensing and transitive dependencies.
- Generate or update an SBOM.
- Require approval if dependency policies demand it.

### **7.8 Implement security validation**

Create example automated checks for:

- Secret scanning.
- Code scanning.
- Dependency vulnerabilities.
- Repository security policy violations.
- Infrastructure configuration violations.
- Unauthorized changes to protected files.
- Missing or failed required security gates.

### **7.9 Secure the build and deployment workflow**

- Demonstrate GitHub Actions or Azure DevOps integration.
- Use restricted pipeline identities.
- Protect deployment environments.
- Verify required approval before production promotion.
- Prevent the development agent from directly executing production deployment commands.
- Preserve security results and approval evidence.

### **7.10 Build the Harness security extension**

Proposed companion sample components:

ILLUSTRATIVE SAMPLE STRUCTURE

```
alpha-car-detailing/
├── .github/
│   └── workflows/
│       └── security-validation.yml
├── harness/
│   └── security/
│       ├── policies/
│       ├── permissions/
│       ├── gates/
│       ├── approvals/
│       ├── audit/
│       └── scripts/
└── docs/
    └── security/
        ├── threat-model.md
        ├── security-baseline.md
        └── incident-response.md
```

### **7.11 Define acceptance criteria**

- The agent cannot access production databases during ordinary development.
- Restricted tools reject unauthorized operations.
- Secrets are excluded from generated artifacts and logs.
- Malicious repository instructions cannot grant additional permissions.
- New dependencies undergo required security checks.
- Failed mandatory security gates block promotion.
- High-risk operations require independently verified human authorization.
- All security-relevant actions are attributable and auditable.
- Temporary credentials and execution resources are cleaned up after the run.

## **8. Claude Example — Secure Engineering with Claude Code**

### **8.1 Claude Code security model**

- Execution permissions.
- Approval and permission modes.
- Tool access boundaries.
- Project and user settings.
- Filesystem and command execution controls.
- Sandboxing capabilities and limitations.
- Security implications of unattended execution.

### **8.2 Securing&#x20;**`CLAUDE.md`

- Repository-level security Instructions.
- Avoiding embedded credentials.
- Protecting trusted security guidance.
- Treating contributed changes as reviewable content.
- Preventing repository Instructions from becoming an authorization mechanism.

### **8.3 Securing Claude Skills and commands**

- Review Skill provenance.
- Control shell execution.
- Limit external dependencies.
- Restrict privileged actions.
- Protect trusted Skills from unauthorized modification.
- Validate reusable security-sensitive workflows.

### **8.4 Tool integrations and MCP security**

- Trust assessment of MCP servers.
- Server authentication and authorization.
- Tool scope minimization.
- External server and tool-output trust boundaries.
- Managing connection credentials.
- Preventing excessive tool permissions.
- Reviewing potentially dangerous tool capabilities.

### **8.5 Secure Claude Code workflow for Alpha Car Detailing**

- Configure a restricted development workspace.
- Permit approved repository operations.
- Restrict access to secrets and production resources.
- Execute approved validation commands.
- Require authorization for sensitive file modifications.
- Deny unauthorized network or database operations.
- Submit a pull request for independent review.

### **8.6 Harness integration**

- Use Claude Code as an execution engine.
- Keep enterprise security policy outside agent-controlled content.
- Enforce authorization through the Harness and underlying systems.
- Capture execution evidence.
- Validate required security checks independently.
- Apply human approval at controlled workflow boundaries.

### **8.7 Limitations and security considerations**

- Permission configuration versus actual operating-system isolation.
- Risks of broad command permissions.
- Risks of executing repository-controlled scripts.
- Security implications of non-interactive operation.
- Remaining prompt injection risks.
- Importance of external policy enforcement.

## **9. GitHub Copilot Comparison**

### **9.1 Security architecture comparison**

- Copilot's repository and development environment integrations.
- Identity and organization access controls.
- Repository permissions and policy enforcement.
- Agent execution capabilities and restrictions.
- Tool and extension permissions.
- Data protection and organizational settings.

### **9.2 Repository and agent configuration**

- Repository Instructions.
- Agent-specific configuration.
- Pull-request execution boundaries.
- Branch and workflow protections.
- Security implications of external contributions.

### **9.3 Integration with GitHub security capabilities**

- Code scanning.
- Secret scanning.
- Dependency scanning and alerts.
- Dependabot.
- Dependency review.
- Pull-request protection.
- Security remediation workflows.

### **9.4 Alpha Car Detailing comparison**

- Implement the same secured corporate fleet booking enhancement.
- Compare permissions and review boundaries.
- Compare execution environment assumptions.
- Compare scanning and approval integrations.
- Identify controls enforced by GitHub versus controls requiring the enterprise Harness.

### **9.5 Architectural assessment**

- Strengths of repository-centric workflows.
- Enterprise policy integration.
- Security control portability.
- Limitations and responsibilities outside the coding agent.

## **10. Codex Comparison**

### **10.1 Codex security architecture**

- Execution environments and isolation.
- Repository and workspace permissions.
- Tool execution controls.
- Network access restrictions.
- Approval and escalation mechanisms.
- Local versus remote execution considerations.

### **10.2 Secure agent execution**

- Restricting filesystem access.
- Limiting command execution.
- Controlling outbound connections.
- Preventing unnecessary credential exposure.
- Handling sensitive outputs.
- Reviewing changes before repository promotion.

### **10.3 Secure Codex workflow for Alpha Car Detailing**

- Assign the corporate booking enhancement.
- Limit access to the application repository.
- Provide synthetic test data.
- Deny direct production access.
- Execute controlled validation steps.
- Capture security evidence.
- Submit changes through protected review workflows.

### **10.4 Enterprise Harness integration**

- Codex as an interchangeable agent execution platform.
- External enforcement of organizational security policies.
- Independent validation gates.
- Human approval requirements.
- Common audit evidence contracts.

### **10.5 Cross-platform comparison criteria**

| Security dimension   | Claude Code                       | GitHub Copilot                          | Codex                             |
| -------------------- | --------------------------------- | --------------------------------------- | --------------------------------- |
| Identity integration | Examine supported mechanisms      | Examine supported mechanisms            | Examine supported mechanisms      |
| Permission controls  | Examine execution permissions     | Examine organization and agent controls | Examine execution permissions     |
| Tool access          | Examine tool and MCP boundaries   | Examine tool and integration boundaries | Examine tool boundaries           |
| Sandboxing           | Evaluate supported isolation      | Evaluate relevant execution environment | Evaluate supported isolation      |
| Network access       | Examine available restrictions    | Examine supported restrictions          | Examine available restrictions    |
| Security scanning    | Integrate external gates          | Evaluate GitHub security integration    | Integrate external gates          |
| Human approval       | Evaluate authorization mechanisms | Evaluate review and approval workflows  | Evaluate authorization mechanisms |
| Auditability         | Integrate enterprise evidence     | Evaluate available execution evidence   | Integrate enterprise evidence     |

Emphasize that platform controls vary by product, environment, configuration, and release. The enterprise security architecture must not depend solely on a vendor-specific permission feature.

## **11. Best Practices**

### **11.1 Identity and permissions**

- Use dedicated workload identities.
- Grant minimum required privileges.
- Prefer temporary credentials.
- Apply task-scoped authorization.
- Avoid shared administrative accounts.

### **11.2 Tools and execution**

- Maintain a tool inventory.
- Restrict commands and destinations.
- Enforce permissions outside the model.
- Isolate untrusted execution.
- Prevent security policy modification by agents.

### **11.3 Data and secrets**

- Use synthetic or masked data.
- Minimize agent-visible information.
- Store secrets in approved secret managers.
- Protect logs and execution artifacts.
- Apply retention and cleanup policies.

### **11.4 Repositories and supply chain**

- Protect security-sensitive configuration.
- Validate external dependencies.
- Require independent security checks.
- Enforce branch protection.
- Review changes to Instructions and Skills.

### **11.5 Harness security**

- Centralize security policy enforcement.
- Keep validation independent.
- Fail closed for mandatory controls.
- Preserve traceable evidence.
- Require human authorization for high-risk actions.

### **11.6 Operational security**

- Monitor suspicious activity.
- Test denial paths.
- Revoke temporary credentials promptly.
- Reassess privileges as tasks change.
- Regularly review the security baseline.

## **12. Anti-patterns**

### **12.1 Granting unrestricted shell access**

Allowing agents to execute arbitrary commands without enforced boundaries.

### **12.2 Giving agents production administrator privileges**

Using production credentials during ordinary development tasks.

### **12.3 Treating Prompts as security enforcement**

Assuming an Instruction such as "do not access production" replaces IAM and network restrictions.

### **12.4 Trusting all repository content**

Treating comments, documentation, and contributed files as authoritative Instructions.

### **12.5 Sharing credentials between agents**

Using one unrestricted identity across Lead, Developer, Reviewer, and Validator workflows.

### **12.6 Running untrusted repository scripts on privileged hosts**

Executing package installation hooks, tests, or build scripts without isolation.

### **12.7 Allowing agents to bypass failed security gates**

Accepting generated explanations as substitutes for required security evidence.

### **12.8 Silently suppressing security findings**

Allowing vulnerabilities to be ignored without documented authorization.

### **12.9 Logging secrets and sensitive data**

Persisting credentials or customer records in Harness logs, scratchpads, or telemetry.

### **12.10 Combining implementation and approval authority**

Allowing the same agent to implement, validate, and authorize a high-risk change without independent controls.

### **12.11 Treating sandboxing as complete protection**

Assuming process or container isolation eliminates every security risk.

### **12.12 Retaining unrestricted Harness Memory**

Persisting sensitive execution context or credentials as reusable knowledge.

## **13. Architect's Notes**

### **13.1 AI Agents are execution principals, not security authorities**

Distinguish an agent's ability to propose an operation from its authorization to execute that operation.

### **13.2 Trust boundaries are more important than prompt wording**

Explain why enforcement must exist independently of natural-language Instructions.

### **13.3 Security policy must outlive individual AI platforms**

Design controls that remain effective when an organization replaces Claude Code with another agent platform.

### **13.4 The Harness is part of the trusted computing base**

Analyze the consequences of compromising orchestration services, tool gateways, security policies, and evidence stores.

### **13.5 Deterministic security gates and AI review serve different purposes**

Clarify that an AI Reviewer can identify potential security problems, while deterministic gates verify specified conditions. Neither replaces comprehensive security assessment.

### **13.6 Secure autonomy requires constrained capability**

Discuss how enterprises should select permissible autonomy levels based on task risk and business impact.

### **13.7 Multi-agent systems require explicit privilege separation**

Examine the security implications of delegation, handoff artifacts, shared context, and parallel execution.

### **13.8 Security must extend into Harness learning**

Ensure learning and memory mechanisms cannot silently promote untrusted content into authoritative security Instructions.

### **13.9 Security architecture must account for failure modes**

Consider unavailable scanners, broken policy services, expired approvals, and compromised execution workers.

### **13.10 Residual risk must be visible**

Discuss the limitations of automated detection, scanning, sandboxing, and agent-level behavioral controls.

## **14. Enterprise Tips**

### **14.1 Establish a minimum enterprise AI security baseline**

Define mandatory identity, repository, tool, data, and validation controls before enabling agent execution.

### **14.2 Classify agent use cases by risk**

Separate documentation changes, code implementation, dependency updates, infrastructure modifications, and production deployments.

### **14.3 Create approved execution environments**

Provide repeatable, isolated environments instead of relying on unrestricted developer machines.

### **14.4 Use organization-managed credentials**

Centralize token lifecycle management and avoid unmanaged personal credentials.

### **14.5 Integrate existing security platforms**

Reuse enterprise SAST, SCA, secret scanning, infrastructure scanning, and SIEM systems rather than duplicating security logic inside agent Prompts.

### **14.6 Secure government and regulated customer workflows**

Apply customer-specific access restrictions, handling requirements, retention policies, and approval obligations.

### **14.7 Standardize security evidence**

Define common evidence formats across AI platforms, repositories, and delivery pipelines.

### **14.8 Exercise incident response**

Run controlled simulations involving prompt injection, leaked credentials, compromised dependencies, and unauthorized deployment attempts.

### **14.9 Manage security policy changes through governance**

Require approved changes to security thresholds, trusted tools, and access profiles.

### **14.10 Measure security control effectiveness**

Track policy denials, security gate failures, exposure incidents, exception aging, credential hygiene, and remediation outcomes without treating more alerts as automatically better security.

## **15. Decision Points**

Present each decision as an architectural trade-off with context, alternatives, risks, and recommended selection criteria.

### **15.1 Should AI Agents receive direct repository write access?**

- Read-only access.
- Feature-branch access.
- Broad repository write access.
- Criteria for choosing the appropriate model.

### **15.2 Should agents execute arbitrary shell commands?**

- Restricted command allowlists.
- Policy-aware command execution.
- Isolated execution with monitored privileges.
- Security and productivity trade-offs.

### **15.3 Should agents have network access?**

- No outbound access.
- Approved destinations only.
- Controlled proxy access.
- Unrestricted access and its risks.

### **15.4 Should production access be permitted?**

- No production access.
- Explicit read-only diagnostics.
- Controlled operational access under approved procedures.
- Separation from routine development roles.

### **15.5 Where should security policies be enforced?**

- Agent Instructions.
- Harness policy engine.
- Tool gateway.
- Infrastructure and identity systems.
- Layered enforcement.

### **15.6 What should happen when a security gate fails?**

- Block the workflow.
- Permit remediation and rerun.
- Require formal exception approval.
- Distinguish policy failure from scanner unavailability.

### **15.7 How should sensitive information reach agents?**

- Direct source-data access.
- Masked datasets.
- Synthetic datasets.
- Governed retrieval services.
- Criteria based on classification and business requirements.

### **15.8 How should high-risk operations be approved?**

- Synchronous human approval.
- Time-bound preauthorization.
- Deployment-stage approval.
- Emergency exception workflow.
- Controls preventing approval reuse and privilege escalation.

### **15.9 How should multi-agent permissions be designed?**

- Shared execution identity.
- Separate role identities.
- Task-specific delegated credentials.
- Isolation and operational complexity trade-offs.

### **15.10 How much security evidence should be retained?**

- Minimal execution metadata.
- Detailed tool and policy records.
- Full transcripts with sensitive data protections.
- Retention, privacy, and investigation requirements.

## **16. Exercises**

### **Exercise 30.1 — Develop an AI Engineering threat model**

Create a threat model for Alpha Car Detailing's agent-assisted corporate fleet booking workflow.

**Deliverables:** Asset inventory, threat matrix, trust boundaries, proposed mitigations, and residual risk assessment.

### **Exercise 30.2 — Design an agent permission matrix**

Define permissions for Lead, Developer, Reviewer, Validator, and Evaluator Agents.

**Deliverables:** Role-to-resource access matrix, least-privilege policy, and escalation requirements.

### **Exercise 30.3 — Configure restricted tool execution**

Implement an isolated agent execution environment with restricted commands, filesystem access, and outbound connections.

**Deliverables:** Tool access policy, permitted operations, denied-operation tests, and captured evidence.

### **Exercise 30.4 — Protect Alpha Car Detailing credentials**

Implement secure handling for API keys and database credentials.

**Deliverables:** Secret store integration, scoped credentials, leakage checks, and rotation procedure.

### **Exercise 30.5 — Simulate prompt injection**

Introduce a benign but maliciously worded instruction into a repository document and verify that it cannot authorize prohibited actions.

**Deliverables:** Attack scenario, defensive controls, test results, and residual risk discussion.

### **Exercise 30.6 — Implement security gates**

Extend the sample Harness with code scanning, dependency scanning, secret scanning, and policy checks.

**Deliverables:** Gate configuration, execution results, blocking conditions, and evidence format.

### **Exercise 30.7 — Secure a dependency update**

Ask an AI Agent to introduce a new package into the fleet booking application.

**Deliverables:** Dependency provenance review, vulnerability results, SBOM updates, and approval decision.

### **Exercise 30.8 — Protect the deployment workflow**

Design a deployment pipeline that prohibits direct production deployment by development agents.

**Deliverables:** Pipeline configuration, identity separation, protected environments, and human approval flow.

### **Exercise 30.9 — Respond to a suspected security incident**

Simulate the discovery of an exposed development API key in an agent execution log.

**Deliverables:** Containment steps, revocation record, evidence preservation, root-cause findings, and remediation plan.

### **Exercise 30.10 — Conduct an enterprise security architecture review**

Evaluate the end-to-end AI Engineering security architecture for Alpha Car Detailing.

**Deliverables:** Architecture review checklist, identified control gaps, prioritized remediation plan, and security sign-off recommendation.

## **17. Interview Questions**

### **Foundational security architecture**

1. What security risks distinguish AI Agent execution from traditional developer automation?
2. Why must an AI Agent not be treated as an independent security authority?
3. How do trust boundaries affect repository intelligence and agent execution?
4. What is the difference between an Instruction and an enforceable security policy?
5. How should enterprises determine the minimum permissions an agent requires?

### **Identity and authorization**

1. How would you design workload identities for a multi-agent Harness?
2. How do RBAC and ABAC apply to AI Engineering?
3. What security advantages do short-lived credentials provide?
4. How can privilege escalation occur through agent tool delegation?
5. Why should development agents be restricted from directly accessing production environments?

### **Prompt injection and data protection**

 1. How does indirect prompt injection differ from a legitimate repository Instruction?
 2. Why is prompt injection detection alone insufficient?
 3. How should untrusted tool responses be handled?
 4. What methods can prevent sensitive data leakage during agent execution?
 5. How would you protect government customer information while enabling AI-assisted software development?

### **Harness and tool security**

 1. What responsibilities should a Harness security policy engine own?
 2. How should tool permissions be evaluated before command execution?
 3. What are the limitations of container-based sandboxing?
 4. How would you secure external MCP servers and their tool capabilities?
 5. How should permissions and trust be managed between cooperating agents?

### **Security gates and supply chain**

 1. What distinguishes a security gate from an AI-based security review?
 2. How should a Harness respond when a required vulnerability scanner is unavailable?
 3. How can AI-generated dependency changes introduce supply-chain risks?
 4. What should be included in the security evidence for a pull request?
 5. How should security exceptions be approved and controlled?

### **Enterprise scenarios**

 1. An agent proposes running a production database migration. How would the security architecture respond?
 2. A repository document instructs an agent to upload an environment file to an external endpoint. Which controls should prevent the operation?
 3. A developer agent changes a Harness gate configuration to make a failing build pass. How should the system detect and prevent this?
 4. An AI Agent introduces a dependency containing a critical vulnerability. How should the workflow respond?
 5. How would you design a secure, auditable, vendor-neutral Harness that supports Claude Code, GitHub Copilot, and Codex?

## **18. Chapter Summary**

The chapter summary will reinforce the following architectural lessons:

### **18.1 Security is foundational to AI Engineering**

AI-assisted engineering requires the same enterprise security discipline as traditional development, extended to autonomous execution and external tools.

### **18.2 Least privilege determines agent capability**

Agents receive only the permissions necessary for authorized tasks.

### **18.3 Trust boundaries prevent instruction-based privilege escalation**

Untrusted content cannot grant additional authority or override enforced security controls.

### **18.4 Tools are the critical enforcement boundary**

Security controls must be applied where real operations execute.

### **18.5 Data and credentials require explicit protection**

Agent context, logs, scratchpads, and artifacts must be treated as potential exposure surfaces.

### **18.6 Deterministic security gates protect engineering workflows**

Mandatory objective checks independently verify required security conditions.

### **18.7 Human approval remains essential**

High-risk operations require accountable authorization outside the requesting agent.

### **18.8 The Harness must enforce security throughout execution**

Security controls apply before, during, and after agent operations.

### **18.9 Security must remain vendor-neutral**

Enterprise security requirements must survive changes in AI platform or agent implementation.

### **18.10 Secure autonomy is governed autonomy**

Greater agent capability requires stronger enforcement, monitoring, separation of duties, and evidence.

## **19. Further Reading**

### **19.1 AI security and threat modeling**

- OWASP Top 10 for Large Language Model Applications / Generative AI security guidance.
- OWASP guidance on agentic applications and AI security.
- MITRE ATLAS — Adversarial Threat Landscape for Artificial-Intelligence Systems.
- NIST AI Risk Management Framework.
- Microsoft Threat Modeling guidance.

### **19.2 Enterprise security architecture**

- NIST SP 800-53 — Security and Privacy Controls.
- NIST SP 800-207 — Zero Trust Architecture.
- NIST SP 800-218 — Secure Software Development Framework.
- OWASP Application Security Verification Standard.
- Cloud Security Alliance guidance on AI and cloud security.

### **19.3 Software supply-chain security**

- SLSA — Supply-chain Levels for Software Artifacts.
- OpenSSF Scorecard.
- SPDX and CycloneDX SBOM specifications.
- OWASP Software Component Verification guidance.
- NIST software supply-chain security publications.

### **19.4 Container and cloud security**

- Kubernetes security documentation.
- Docker security guidance.
- Microsoft Azure Well-Architected Framework — Security.
- Microsoft Defender for Cloud documentation.
- GitHub Actions security hardening guidance.

### **19.5 AI coding agent security**

- Anthropic Claude Code security and permissions documentation.
- Anthropic guidance on prompt injection defenses.
- GitHub Copilot security and organization policy documentation.
- GitHub Code Security and Secret Protection documentation.
- OpenAI Codex security, sandboxing, and approval documentation.
- Model Context Protocol authorization and security guidance.

### **19.6 Related handbook chapters**

- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 26 — Tool Calling.
- Chapter 29 — AI Governance.
- Chapter 31 — Human Approval.
- Chapter 32 — Audit Trails.
- Chapter 34 — Compliance.
- Chapter 51 — Security.

**Chapter 30 outline is complete.**
