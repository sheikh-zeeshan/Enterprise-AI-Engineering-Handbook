# Chapter 51 - Security

**Part VII - Alpha Car Detailing**  
**Target file:** `book/51-security.md`  
**Status:** Detailed outline

> **Core principle:** Security must be designed across identity, authorization, data, communication, infrastructure, and operations rather than added after implementation.

## 1. Story-driven Opening

### 1.1 A valid identity reaches the wrong fleet

- Continue Northstar Distribution's booking of twenty-four vehicles across two days.
- Introduce an authenticated fleet administrator who changes a booking identifier and attempts to retrieve another corporate customer's booking.
- Contrast successful token validation with missing authorization over the requested resource.
- Extend the review to a government department whose vehicle and appointment information requires explicitly approved access and disclosure rules.
- Establish the required outcome: every entry point preserves account, fleet, and operation boundaries.

### 1.2 Follow the attack surface beyond the API

- Examine a forged event, an overly privileged service identity, and a leaked payment-provider credential as related review scenarios.
- Introduce an AI-generated change that passes functional tests while weakening an authorization policy.
- Frame the chapter around explicit trust boundaries, independent enforcement, operational evidence, and accountable approval.

## 2. Learning Objectives

After completing this chapter, the reader will be able to:

- Model threats across users, APIs, services, event channels, data stores, infrastructure, and engineering tools.
- Distinguish authentication, authorization, business approval, and engineering Human Approval.
- Validate JWT access tokens and implement policy-based and resource-based authorization.
- Enforce corporate account and fleet isolation across reads, writes, asynchronous processing, and support operations.
- Separate workload identity from delegated user authority.
- Protect secrets, cryptographic keys, certificates, sensitive data, and payment integrations.
- Secure containers, Kubernetes, networks, databases, and deployment pipelines through least privilege.
- Combine scanning, security testing, audit logging, detection, and incident response.
- Use AI Agents to assist security engineering while preserving independently enforced controls.

## 3. Background

### 3.1 Relationship to earlier chapters

- Apply Chapters 30-34's security, Human Approval, Audit Trails, policy, and compliance principles to the running application.
- Preserve Chapter 46's approved fleet-scope and business-authorization invariants, including `BR-FLT-001` and `BR-AUT-001`.
- Build on Chapters 47-49's service ownership, Clean Architecture, private data, and explicit delegation boundaries.
- Extend Chapter 50's event provenance, consumer authorization, data minimization, and controlled replay requirements.
- Keep government customer obligations, retention periods, and financial approval thresholds explicit policy decisions.

### 3.2 Preserve the implementation baseline

- Distinguish the runnable Booking create/read baseline from the proposed production security architecture.
- Preserve `POST /api/bookings` as pending creation; authentication does not establish confirmation, capacity commitment, or business approval.
- Retain direct application services and the accepted decision to defer CQRS and mediator adoption.
- Label proposed identity integrations, policies, infrastructure, and security tests as extensions until implemented and verified.

### 3.3 Security as an ongoing engineering responsibility

- Separate secure design, correct implementation, deployment configuration, and operational effectiveness.
- Introduce security ownership across product, engineering, platform, operations, and security teams.
- Explain why passing a scanner or completing a checklist provides bounded evidence rather than proof of security.

## 4. Concepts

### 4.1 Security architecture and trust boundaries

- Define confidentiality, integrity, availability, accountability, and reduction of blast radius.
- Explain defense in depth, deny by default, least privilege, explicit verification, and assumption of compromise.
- Distinguish public ingress, internal service calls, broker access, administrative access, and engineering control planes.

### 4.2 Threat modeling

- Identify assets, actors, entry points, data flows, dependencies, and trust boundaries.
- Use STRIDE and abuse cases to examine impersonation, tampering, repudiation, disclosure, denial of service, and privilege escalation.
- Record each threat's affected invariant, mitigation, owner, residual risk, and verification evidence.
- Cover cross-account booking access, unauthorized payment operations, event forgery, resource exhaustion, and compromised AI tooling.

### 4.3 Authentication and identity lifecycle

- Distinguish OpenID Connect authentication, OAuth authorization mechanisms, and access-token representation.
- Outline interactive sign-in through an approved identity provider, appropriate client flows, MFA, and session protection.
- Cover corporate federation, government users, invitations, account disabling, and membership removal.
- Separate identity-provider tenants from Alpha Car Detailing corporate accounts and fleet scope.

### 4.4 JWT access tokens

- Explain signed JWT structure, claims, access tokens versus ID tokens, and the limits of bearer credentials.
- Require signature, approved algorithm, issuer, audience, lifetime, and applicable token-type validation.
- Cover trusted signing-key discovery, rollover, clock tolerance, token leakage, and compromised credentials.
- Explain that signed claims are readable unless separately encrypted and that decoding a token is not validation.
- Distinguish token expiration, revocation requirements, and replay protection; an event or token identifier alone prevents neither replay nor misuse.

### 4.5 Authorization, roles, permissions, and policies

- Distinguish role membership, operation permission, delegated scope, and access to a particular resource.
- Explain ASP.NET Core policy requirements, handlers, and resource-based authorization.
- Use proposed permissions such as booking read, booking create, and fleet administration without presenting them as an approved production catalog.
- Separate permission to request an operation from any required business approval to execute it.

### 4.6 Corporate account and fleet isolation

- Resolve account membership from trusted evidence and authoritative mappings.
- Check the relationship among caller, corporate account, fleet, vehicle, booking, and requested action.
- Handle users with several authorized accounts and changes to membership during active sessions.
- Apply isolation to lists, searches, exports, caches, idempotency records, and operation-status endpoints.

### 4.7 Workload identity and delegated authority

- Distinguish service identity, client credentials, managed identity, workload federation, and user delegation.
- Explain audience-specific downstream credentials and protection against confused-deputy behavior.
- Compare explicitly authorized system operations with actions performed on behalf of a user.
- Explain why private networking and mutual TLS do not establish business authorization by themselves.

### 4.8 Secrets, keys, and certificates

- Distinguish passwords and provider credentials from cryptographic keys and certificates.
- Define ownership, issuance, storage, distribution, rotation, revocation, expiry monitoring, and recovery.
- Prefer short-lived or federated credentials where supported; govern unavoidable secrets through a managed store.
- Separate application-runtime privileges from privileges to administer secrets and key policies.

### 4.9 Data protection and encryption

- Classify customer identities, government fleet details, vehicle information, payment references, and engineering evidence.
- Explain TLS, encryption at rest, selective field protection, tokenization, and key separation.
- Distinguish database encryption, application encryption, and ASP.NET Core Data Protection and its shared key-ring requirements.
- Cover retention, deletion, backups, restored copies, and safe development fixtures.

### 4.10 API security and input validation

- Distinguish syntactic validation, semantic validation, authorization, and business invariants.
- Address object-level, property-level, and function-level authorization, including mass assignment.
- Cover injection, SSRF, unsafe outbound calls, excessive payloads, pagination, timeouts, and resource consumption.
- Explain the separate purposes of CORS, CSRF protection, rate limiting, and authentication.

### 4.11 Event security

- Authenticate publishers and consumers and authorize the specific channel operations they need.
- Separate authenticated source provenance from freely writable envelope fields.
- Cover schemas, payload limits, account scope, retained history, dead-letter storage, and controlled replay.
- Explain why a consumer group or subscription name is not sufficient evidence of an authorization boundary.

### 4.12 Audit logging and security events

- Distinguish business integration events, diagnostic logs, security detections, and authoritative Audit Trails.
- Define actor, workload, action, resource scope, decision, policy version, correlation, timestamp, and outcome fields.
- Protect records from sensitive-data leakage, tampering, unauthorized access, and silent loss.

### 4.13 Software supply-chain and infrastructure security

- Introduce dependency provenance, vulnerability management, SBOMs, build integrity, artifact signing, and deployment admission.
- Distinguish application packages, base images, cluster components, infrastructure definitions, and build tools.
- Define least privilege for both runtime workloads and the systems that build and deploy them.

### 4.14 Risk, exceptions, and Human Approval

- Classify sensitive changes by affected authority, data exposure, environment, and potential impact.
- Require concrete evidence and accountable approval for material security changes and approved policy exceptions.
- Distinguish an AI Agent's tool permission from organizational authority to change production security.

## 5. Architecture Discussion

### 5.1 Assign security controls to accountable owners

| Boundary or owner | Principal responsibility | Alpha Car Detailing example |
| --- | --- | --- |
| Identity provider | Authenticate identities and manage their lifecycle | Corporate sign-in and MFA |
| API Gateway | Protect ingress and apply admission controls | Token checks, payload limits, throttling |
| Booking | Authorize booking actions and protect booking state | Northstar can access only its authorized bookings |
| Corporate Account and Fleet | Own account eligibility and fleet relationships | Verify current membership and vehicle scope |
| Station Operations | Authorize capacity and fulfillment operations | Accept reservation actions only from permitted callers |
| Billing and Payment | Protect financial operations and provider integration | Restrict settlement, refunds, callbacks, and reconciliation |
| Notification and event consumers | Enforce contract, source, and local processing scope | Consume approved facts without exposing fleet details |
| Platform and operations | Secure identities, networks, stores, clusters, and recovery | Workload identity, private endpoints, incident containment |
| Harness and engineering governance | Control AI tools, validation, evidence, and release authority | Prevent an Agent from weakening its own security gates |

### 5.2 Place enforcement within Clean Architecture

- Authenticate at delivery boundaries and adapt verified evidence into a narrow caller context.
- Keep `HttpContext`, raw JWTs, and framework claims types outside Domain and Application contracts.
- Enforce use-case and resource authorization for HTTP, message consumers, background jobs, and administrative entry points.
- Keep domain invariants independent of security frameworks and preserve adapter-specific least privilege.

### 5.3 Secure authentication and authorization end to end

- Validate accepted issuers, API audiences, signing keys, and lifetimes through supported middleware.
- Establish how each service verifies credentials or an explicitly trusted gateway assertion; reject spoofed identity headers.
- Apply operation policies and resource checks before protected reads or mutations.
- Define consistent `401`, `403`, and deliberate resource-concealment behavior without exposing another account's existence.
- Address membership freshness, revocation latency, policy-cache expiry, and fail-closed handling of uncertain authority.

### 5.4 Protect the corporate fleet booking workflow

- Authenticate the Northstar administrator and establish the selected account's authorized fleet scope.
- Validate every selected vehicle against Fleet's authority and every protected booking operation against Booking's ownership.
- Preserve Corporate Account's authority over eligibility and required business approval.
- Obtain authorized Station Operations capacity commitments before confirmation.
- Commit approved state and publication obligations without placing bearer tokens in events.
- Prevent unauthorized reads, cancellations, bulk exports, status lookups, and idempotency-key reuse across accounts.
- Model government departmental scope and access restrictions as explicitly approved policy, not inferred corporate behavior.

### 5.5 Secure service-to-service communication

- Give Booking, Fleet, Station Operations, Billing, Payment, and consumers distinct workload identities.
- Use managed or federated identities for supported Azure resources and evaluate the selected internal API identity mechanism separately.
- Limit credential audience and target permissions; distinguish user delegation from system-operation authorization.
- Validate TLS certificates and evaluate mutual TLS against identity, network, and operational requirements.
- Test that one compromised service cannot read another service's database or publish its financial facts.

### 5.6 Protect gateway and API boundaries

- Apply HTTPS, trusted proxy configuration, ingress filtering, request-size limits, and safe error responses.
- Partition rate limits by relevant caller and account scope; consider distributed enforcement, fairness, and bypass paths.
- Bound fleet batch sizes, concurrency, pagination, execution time, and downstream resource consumption.
- Use explicit request DTOs to prevent clients setting confirmation status, approval evidence, capacity references, or privileged properties.
- Apply browser session, CSRF, and CORS controls according to the chosen client architecture.

### 5.7 Secure event publication, consumption, and replay

- Restrict publication of `BookingConfirmed`, `ServiceCompleted`, `InvoiceIssued`, and `PaymentRecorded` to their authoritative producers.
- Map broker permissions to the selected Kafka or Event Hubs design; verify actual isolation rather than assuming identical platform behavior.
- Preserve verifiable producer authority when channels contain several producers.
- Validate schema, provenance, size, identifiers, and account scope before business effects; retain idempotent processing from Chapter 50.
- Authorize replay and reconciliation separately, preserve original event identity, and prevent duplicate financial or notification effects.
- Protect outboxes, inboxes, checkpoints, archives, retries, and dead-letter stores throughout retention and recovery.

### 5.8 Manage secrets, keys, and certificate lifecycle

- Use Azure Key Vault as the concrete reference while keeping the design portable to equivalent managed stores.
- Separate environments and grant each workload only its required secret or cryptographic operations.
- Keep credentials out of source control, images, logs, prompts, artifacts, and ordinary application settings files.
- Specify rotation overlap, runtime refresh or reload behavior, expiry alerts, compromise response, and rollback constraints.
- Document vault outages, bounded caching, emergency access, and tested recovery without creating unrestricted fallback credentials.

### 5.9 Protect data, payment integrations, and databases

- Minimize personal, government fleet, and financial data in APIs, events, telemetry, and support exports.
- Prefer provider-hosted payment capture and tokenized references; define payment-data responsibilities with the relevant owners.
- Authenticate payment callbacks, verify operation references, deduplicate provider events, and reconcile uncertain outcomes.
- Keep `InvoiceIssued` separate from authorization to charge and preserve the actual meaning of `PaymentRecorded` outcomes.
- Restrict SQL Server identities to service-owned data; separate runtime access from migration and administrative privileges.
- Apply parameterized access, encrypted connections, protected backups, and policy-driven field protection.
- Treat row-level security and query filters as additional controls with independently verified bypass and administration paths.
- Scope Redis keys, caches, and idempotency records by trusted account context and minimize cached sensitive data.

### 5.10 Secure containers, Kubernetes, and networks

- Build from supported minimal images, pin approved artifact digests, and verify image provenance and scan results.
- Run workloads without root where feasible, drop capabilities, restrict privilege escalation, and use appropriate filesystem and syscall controls.
- Apply Kubernetes RBAC, dedicated ServiceAccounts, bounded credentials, Pod Security controls, and admission policy.
- Protect Kubernetes Secrets at rest; explain that base64 encoding provides no confidentiality.
- Restrict ingress and egress through supported network policy, private endpoints, and explicit dependency access.
- Verify policy enforcement by the deployed network implementation and protect control-plane, metadata, DNS, and management paths.
- Isolate production credentials, deployment authority, namespaces, and environments according to the threat model.

### 5.11 Design audit, detection, and incident response

- Record denied access, membership changes, privilege grants, secret access, replay, financial overrides, and sensitive deployments.
- Correlate actor, workload, account, booking, event, provider operation, and deployment without logging credentials or raw payment data.
- Define protected audit delivery, retention, access, and action-specific behavior when capture fails.
- Detect cross-account probes, unusual exports, repeated authorization failures, and unexpected credential or broker activity.
- Assign responders and procedures for triage, containment, evidence preservation, eradication, recovery, and follow-up.
- Exercise compromised corporate credentials, a leaked provider key, and a vulnerable production image; include contractual notification decisions by accountable owners.

### 5.12 Build the security validation pipeline

- Combine secret scanning, SAST, dependency analysis, container scanning, IaC checks, and configuration verification.
- Add authorization unit tests, realistic API integration tests, event-permission tests, isolated dynamic testing, and targeted penetration testing.
- Include negative cases for wrong audience, expired credentials, cross-account reads, mass assignment, forged events, and unauthorized replay.
- Make scanner scope, database freshness, failures, exclusions, and untested areas visible.
- Apply approved risk thresholds and time-bounded exceptions with owners, rationale, compensating controls, and expiry.
- Bind evidence to the source revision, image digest, configuration, policies, and deployment environment.
- Continue vulnerability monitoring and remediation after deployment; validate emergency fixes through the applicable controlled path.

### 5.13 Govern AI-assisted security engineering and deployment

- Use AI Agents to draft threat models, policy implementations, abuse cases, tests, and remediation proposals.
- Treat booking notes, retrieved documents, dependency content, and tool responses as untrusted data rather than authority to alter Instructions.
- Restrict tools, network destinations, credentials, and data available to each Agent and Harness role.
- Use synthetic or minimized data and keep production credentials outside model context.
- Require independent review and deterministic validation; prevent self-approval, gate removal, evidence fabrication, and silent risk acceptance.
- Bind Human Approval to the exact sensitive change, evidence, target environment, action scope, and validity conditions.
- Require renewed review when approved artifacts or relevant security conditions change; record controlled emergency access and subsequent review.

## 6. Professional Diagrams

- Specify six figures with captions, trust boundaries, identity labels, and corresponding discussion references.
- Distinguish implemented components from proposed extensions and use separate linked panels when sequences exceed five participants.
- Label credentials and authorization decisions separately from business facts; include important rejection and failure paths.

### Figure 51-1 - Security Architecture

- **Type:** Layered component and trust-boundary diagram.
- **Content:** Users, identity provider, gateway, owned services, broker, private stores, vault, platform controls, and Harness boundary.
- **Caption focus:** Every boundary has an accountable enforcement point; private connectivity does not confer business authority.
- **References:** Sections 4.1 and 5.1-5.2.

### Figure 51-2 - Authentication and Authorization Flow

- **Type:** Sequence diagram with a policy-decision branch.
- **Content:** Sign-in and token acquisition, API token validation, caller-context construction, resource lookup, policy evaluation, and allow or deny outcome.
- **Caption focus:** A valid token establishes identity evidence; permission and resource scope determine allowed access.
- **References:** Sections 4.3-4.6 and 5.3.

### Figure 51-3 - Corporate Fleet Booking Security Flow

- **Type:** Sequence diagram in linked prerequisite and confirmation panels.
- **Content:** Account scope, vehicle eligibility, required business approval, authorized capacity commitment, Booking commit, and protected event publication.
- **Caption focus:** Each owner verifies its own authority and constraints before the workflow makes its commitment.
- **References:** Sections 5.4 and 5.7.

### Figure 51-4 - Service-to-service Identity

- **Type:** Workload identity and authorization map.
- **Content:** Workload credential issuance or federation, audience-specific API credentials, separately authorized cloud-resource access, and optional delegated user context.
- **Caption focus:** Workload authentication and authority to act for a corporate user are distinct decisions.
- **References:** Sections 4.7 and 5.5.

### Figure 51-5 - Secrets and Key Management

- **Type:** Lifecycle and access-boundary diagram.
- **Content:** Provisioning authority, managed vault, workload access, rotation, runtime refresh, expiry detection, revocation, and recovery.
- **Caption focus:** Credential retrieval, key use, and secret administration require different privileges and lifecycle controls.
- **References:** Sections 4.8-4.9 and 5.8.

### Figure 51-6 - Security Validation Pipeline

- **Type:** Branching pipeline flowchart in source, artifact, and release stages.
- **Content:** Threat-informed changes, review, scans, negative tests, image and configuration evidence, blocking findings, approved exceptions, Human Approval, admission, and production monitoring.
- **Caption focus:** Promotion depends on applicable evidence and authorized decisions; failed or incomplete checks remain visible.
- **References:** Sections 5.11-5.13 and 7.7.

## 7. Hands-on Example

### 7.1 Establish a bounded security lab

- Inspect existing Instructions, ADRs, Booking endpoints, and test infrastructure.
- Define a proposed Booking read/create security slice using Northstar, another corporate account, and an illustrative government department.
- Use deterministic identities, synthetic data, and explicit test adapters for unimplemented services.
- Record the threat model, permission proposal, policy assumptions, and acceptance criteria before changing code.

### 7.2 Configure authentication and caller context

- Configure an approved test issuer and JWT bearer validation without introducing a custom production token issuer.
- Adapt validated identity into an Application-facing caller context.
- Test accepted credentials, invalid signatures, untrusted issuers, wrong audiences, and expired tokens.

### 7.3 Implement policies and resource isolation

- Define booking permissions and resource checks using authoritative account and fleet relationships.
- Protect create, retrieve, list, status, and idempotency access as applicable to the slice.
- Test that changing an account or booking identifier cannot expand access.
- Keep pending creation and existing business invariants unchanged unless a separately approved extension is included.

### 7.4 Add service and event boundary checks

- Demonstrate permitted workload access and denial for an unrelated service identity.
- Validate an illustrative `BookingConfirmed` consumer's source, schema, and account scope.
- Reject a forged producer label and verify that replay requires separate authority.
- Preserve durable deduplication and avoid embedding user bearer tokens in retained messages.

### 7.5 Add secret handling, limits, and audit evidence

- Demonstrate development secret access and a proposed production vault identity with narrowly scoped permissions.
- Exercise rotation or expiry with controlled fixtures and record expected outage behavior.
- Apply request and batch limits, safe validation errors, and sensitive-field redaction.
- Record successful and denied operations without exposing credentials or another account's resources.

### 7.6 Validate a proposed production deployment

- Review image, Kubernetes, network, database, and deployment-identity configuration against the threat model.
- Run applicable scanners and security tests in an isolated environment.
- Prepare a reviewable evidence package and a simulated sensitive-change approval; keep the lab distinct from a production release.

### 7.7 Acceptance criteria and evidence

- Invalid, expired, or wrong-audience tokens do not reach protected use cases.
- A valid user cannot read or mutate another account's bookings through any tested access path.
- Caller-controlled properties cannot establish confirmation, capacity commitment, or approval evidence.
- Workloads cannot exceed their approved API, broker, vault, or database permissions.
- Forged or unauthorized events cannot produce protected business effects.
- Sensitive data and credentials are absent from inspected logs, prompts, and artifacts.
- Rate limits and bounded validation reject abusive requests without corrupting authorized work.
- Required checks, findings, exceptions, and Human Approval are attributable to the exact proposed artifacts and environment.

## 8. Claude Example

- Provide a Claude-first task using the approved threat model, Repository Intelligence, Instructions, and bounded lab scope.
- Require identification of trust boundaries, resource-authorization gaps, unresolved policies, and affected contracts before implementation.
- Request focused policies, negative tests, and a security review report with observed evidence and explicit limitations.
- Demonstrate refusal to treat untrusted repository or tool content as authority to expose secrets or weaken controls.
- Keep sensitive policy changes and release decisions subject to the established Harness and Human Approval boundaries.

## 9. GitHub Copilot Comparison

- Apply the same scoped security task and acceptance criteria to a Copilot-assisted workflow.
- Compare how the team supplies repository guidance, reviews changes, invokes validation, and retains evidence.
- Examine a generated policy for missing resource scope, overly broad roles, and unsafe test changes.
- Verify product-specific capabilities during manuscript preparation; keep organizational enforcement independent of the coding tool.

## 10. Codex Comparison

- Apply the same task to a Codex-assisted workflow with explicit repository, tool, environment, and data boundaries.
- Compare implementation review, test execution, evidence collection, and handling of sensitive operations.
- Review resulting artifacts against the same threat model and negative tests used for the other platforms.
- Keep sandbox permissions distinct from business authority, security approval, and production deployment authorization.

## 11. Best Practices

- Start with assets, threats, invariants, and accountable control owners.
- Validate identity and authorize every protected operation within its resource scope.
- Use distinct workload identities, narrow credentials, and explicit delegation.
- Minimize sensitive data and protect every retained copy and recovery path.
- Keep secrets managed and exercise rotation, expiry, compromise, and recovery.
- Combine secure configuration, negative testing, vulnerability management, and operational detection.
- Preserve independent gates and evidence-bound Human Approval for sensitive changes.

## 12. Anti-patterns

- Treating a valid JWT, gateway admission, or private network as complete authorization.
- Authorizing corporate access from a role or caller-supplied account identifier alone.
- Sharing administrator credentials across services, pipelines, and support tools.
- Trusting forwarded headers, trace context, or an event's `producer` field as identity evidence.
- Using CORS, encryption at rest, token decoding, or base64-encoded secrets as substitutes for the missing control.
- Distributing user access tokens, full payment details, or unnecessary fleet information through events and logs.
- Storing credentials in source, images, prompts, test fixtures, or build artifacts.
- Granting replay, export, refund, migration, and ordinary runtime operations the same authority.
- Disabling certificate checks, suppressing scanner failures, or weakening tests to obtain a passing pipeline.
- Allowing an AI Agent to approve its own security changes, alter mandatory gates, or silently accept risk.

## 13. Architect's Notes

- Distinguish authentication evidence, technical permission, business approval, and engineering approval.
- Explain where authorization decisions belong and how authoritative membership remains sufficiently fresh.
- Evaluate shared infrastructure against isolation requirements; namespaces and consumer groups require supporting enforcement.
- Balance availability with revocation, vault dependencies, audit durability, and fail-closed decisions.
- Treat payment tokenization and government customer labels as inputs to scope analysis, not proof of compliance.
- Record residual risks and operational costs in ADRs rather than concealing them behind product choices.

## 14. Enterprise Tips

- Maintain service, identity, permission, secret, certificate, and security-control inventories.
- Review account membership, privileged access, and emergency credentials at approved intervals.
- Give every finding and policy exception an owner, remediation expectation, and review date.
- Use synthetic fixtures and tightly controlled support exports for corporate and government investigations.
- Rehearse credential compromise, certificate expiry, cross-account exposure, and deployment rollback.
- Measure control effectiveness through authorization defects, remediation time, detection coverage, rotation failures, and incident recovery evidence.

## 15. Decision Points

| Decision | Evaluation criteria |
| --- | --- |
| Identity provider and federation | User populations, MFA, lifecycle ownership, approved trust relationships |
| Roles, permissions, and resource policies | Operation granularity, account scope, maintainability, authoritative relationships |
| Membership and authorization freshness | Revocation needs, caching, outage behavior, business impact |
| Delegation or system authority | User intent, target API audience, audit attribution, permitted business action |
| TLS and mutual TLS | Threat model, workload identity, certificate operations, platform support |
| Shared or isolated messaging resources | Producer provenance, ACL granularity, consumer isolation, retained data exposure |
| Managed identities or stored credentials | Target-system support, rotation burden, compromise impact |
| Encryption and key ownership | Data classification, search needs, recovery, key administration boundaries |
| Container and Kubernetes controls | Isolation requirements, runtime compatibility, enforcement coverage |
| Security gates and exceptions | Severity, exploitability, exposure, compensating controls, accountable acceptance |
| Human Approval and emergency access | Sensitive action, exact evidence, target environment, expiry, retrospective accountability |
| Government and payment requirements | Approved contracts, applicable obligations, data flows, accountable specialist review |

## 16. Exercises

### 16.1 Model the fleet booking threats

- Produce a trust-boundary diagram and prioritized threat register for Northstar and a government department.
- Map each significant threat to an owner, mitigation, and testable condition.

### 16.2 Close an authorization gap

- Design or implement policy and resource checks for booking access.
- Prove isolation across two corporate accounts, multiple memberships, list queries, and status lookups.

### 16.3 Constrain a service and event consumer

- Specify least-privilege API, broker, database, and vault permissions.
- Demonstrate rejection of an unrelated workload, forged event provenance, and unauthorized replay.

### 16.4 Exercise credential and key lifecycle

- Design a rotation and expiry drill with bounded runtime behavior and auditable recovery.
- Explain which credentials can be revoked immediately and which access remains valid until expiry or another enforcement step.

### 16.5 Review a production deployment

- Identify excessive container privileges, missing network enforcement, broad database access, and deployment identity risks.
- Produce a prioritized remediation plan and the corresponding validation evidence.

### 16.6 Investigate a security incident

- Run a tabletop exercise involving a leaked payment-provider credential or cross-account booking exposure.
- Document containment, preserved evidence, affected operations, recovery, and prevention work.

### 16.7 Review an AI-generated security change

- Examine a plausible change that weakens a policy or disables a gate while retaining passing functional tests.
- Require independent detection, corrected negative tests, and an evidence-bound approval decision.

## 17. Interview Questions

1. Why can an authenticated fleet administrator still be unauthorized to retrieve a booking?
2. How do identity-provider tenants, corporate accounts, fleets, and resource permissions differ?
3. Which checks make JWT validation meaningful, and why is decoding insufficient?
4. How do policy-based and resource-based authorization complement each other?
5. How should account membership changes affect tokens, caches, and active workflows?
6. What distinguishes workload authentication from delegated user authority?
7. Where should security enforcement sit within Clean Architecture?
8. Why do private networks and mutual TLS still require application authorization?
9. How can consumers verify producer authority without trusting a JSON field?
10. How should replay, payment callbacks, refunds, and reconciliation be secured?
11. What distinguishes secrets, keys, certificates, and application data-protection keys?
12. Which database, container, and Kubernetes permissions should ordinary runtime workloads lack?
13. How do scanning, security testing, audit evidence, and production monitoring complement one another?
14. What evidence should Human Approval cover for a sensitive production security change?
15. How can AI Agents assist security engineering without gaining authority to weaken its controls?
16. What should happen after a provider credential leaks or another account's booking becomes visible?

## 18. Chapter Summary

- Revisit Northstar's workflow from authenticated access through authoritative prerequisites, confirmation, events, and later financial processing.
- Reinforce explicit trust boundaries, resource authorization, distinct identities, least privilege, and sensitive-data minimization.
- Summarize the lifecycle controls protecting secrets, keys, certificates, infrastructure, and retained evidence.
- Connect secure delivery with detection, response, recovery, independent validation, and accountable Human Approval.
- Bridge to Chapter 52's observability discussion through security-relevant telemetry and protected operational evidence.

## 19. Further Reading

### 19.1 Application security and threat-informed verification

- [OWASP Application Security Verification Standard](https://asvs.owasp.org/): derive explicit requirements and verification criteria.
- [OWASP API Security Top 10](https://owasp.org/www-project-api-security/): structure API abuse cases and authorization reviews.
- [NIST Secure Software Development Framework, SP 800-218](https://csrc.nist.gov/pubs/sp/800/218/final): connect secure development with organizational practices.

### 19.2 Identity and authorization

- [Microsoft: Configure JWT bearer authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0).
- [Microsoft: Policy-based authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-10.0) and [resource-based authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/resource-based?view=aspnetcore-10.0).
- [IETF RFC 8725: JSON Web Token Best Current Practices](https://www.rfc-editor.org/rfc/rfc8725) and [RFC 9700: Best Current Practice for OAuth 2.0 Security](https://www.rfc-editor.org/rfc/rfc9700).

### 19.3 Workload, secret, and platform controls

- [Microsoft Entra Workload ID on AKS](https://learn.microsoft.com/en-us/azure/aks/workload-identity-overview).
- [Microsoft: Secure your Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/secure-key-vault).
- [Kubernetes Security Checklist](https://kubernetes.io/docs/concepts/security/security-checklist/).
- Include the selected broker, database, container, and payment provider's primary security documentation when preparing the manuscript.

### 19.4 Incident response and handbook cross-references

- [NIST SP 800-61 Revision 3](https://csrc.nist.gov/pubs/sp/800/61/r3/final): integrate preparation, detection, response, and recovery with cybersecurity risk management.
- Revisit Chapters 30-34 for governance controls and Chapters 46-50 for the application's authoritative business and architecture decisions.
- Verify platform-specific AI tooling details against primary documentation during manuscript preparation.

Chapter 51 outline is complete.
