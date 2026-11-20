# Chapter 49 — Microservices

**Part VII — Alpha Car Detailing**  
**Target manuscript:** `book/49-microservices.md`  
**Status:** Detailed outline

> **Core principle:** Microservices should follow clear business capabilities and ownership boundaries, not technical layers or database tables.

## 1. Story-driven opening

### 1.1 Northstar's booking crosses service boundaries

- Continue the Northstar Distribution scenario: twenty-four vehicles require service across two days.
- Show how a well-structured Booking implementation can still become operationally coupled if confirmation requires a long synchronous chain through every downstream service.
- Introduce a notification-provider outage and a simultaneous station-capacity request; distinguish a delayed consequence from a failed booking prerequisite.
- Frame the architectural challenge as preserving business commitments while allowing capabilities to evolve, deploy, and recover independently.

### 1.2 Establish the ownership questions

- Identify who may approve account eligibility, validate vehicles, commit capacity, confirm a booking, recognize billable work, execute payment, and deliver communications.
- Preserve the distinction between request acceptance, pending booking, committed capacity, confirmed booking, and notification delivery.
- Keep booking limits, request grouping, partial acceptance, reservation expiry, credit thresholds, and pricing formulas open unless an approved decision establishes them.

## 2. Learning objectives

- Derive service boundaries from business capabilities, bounded contexts, invariants, and change patterns.
- Define domain ownership and accountable engineering ownership for each service.
- Explain the responsibilities of the seven proposed business services and the API Gateway.
- Select synchronous APIs or asynchronous events according to business timing and failure requirements.
- Protect private service data and evolve producer-owned contracts without coordinated releases.
- Design local transactions, capacity coordination, transactional outbox publication, and idempotent processing.
- Handle partial failures, ambiguous outcomes, concurrency, and recovery without false confirmation or duplicate side effects.
- Specify service discovery, configuration, security, observability, deployment, testing, and governance controls.
- Use AI Agents and the Harness to develop service changes while preserving approved architecture and business rules.

## 3. Background

### 3.1 Position within Part VII

- Chapter 46 establishes business vocabulary, approved case-study rules, and unresolved policies.
- Chapter 47 establishes the proposed solution landscape, service responsibilities, and integration principles.
- Chapter 48 establishes Clean Architecture inside a service; Domain, Application, Infrastructure, and API are internal layers rather than separate microservices.
- Chapter 49 develops the service boundaries into explicit contracts, operational responsibilities, release practices, and recovery protocols.
- Introduce event-driven mechanisms only to the depth required here; reserve their broader treatment for Chapter 50.

### 3.2 Existing implementation and proposed architecture

- Identify the current Booking create/read baseline separately from proposed corporate-fleet integrations and additional deployables.
- Preserve `POST /api/bookings` as pending creation; an in-memory `BookingCreatedDomainEvent` does not establish confirmation or durable publication.
- Retain direct application services and the accepted decision to defer CQRS and a mediator.
- Distinguish logical bounded contexts, independently deployed services, and the sequence in which the companion application implements them.

### 3.3 When microservices are justified

- Evaluate independent change, differentiated scaling, business isolation, security boundaries, and accountable ownership.
- Compare a modular monolith, a small number of cohesive services, and finer decomposition against operational cost and team capacity.
- Explain the costs of network boundaries, eventual consistency, contract maintenance, distributed diagnosis, and recovery.
- Treat service extraction or consolidation as an architectural decision supported by evidence.

## 4. Concepts

### 4.1 Business capabilities, bounded contexts, and service boundaries

- Define a capability as business responsibility and a bounded context as a boundary within which a model and its language are consistent.
- Explain why a bounded context does not automatically require exactly one deployable service.
- Use invariants, lifecycle ownership, change frequency, security, and operational needs to evaluate boundaries.
- Reject decomposition by technical layer, database table, CRUD entity, or desired service count.

### 4.2 Business and engineering ownership

- Distinguish authority over business state from responsibility for implementing and operating the service.
- Assign ownership of code, private data, APIs, events, security, deployment, support, and recovery.
- Explain how one team may own several services while each service still has a clear accountable owner.
- Separate a shared platform's infrastructure responsibilities from business-service authority.

### 4.3 Synchronous APIs and asynchronous events

- Use a synchronous API when the caller needs an authoritative decision before proceeding.
- Use an integration event to announce a committed business fact with independently processed consequences.
- Distinguish commands, queries, events, and transport acknowledgments; name each contract according to its semantics.
- Explain temporal coupling, stale projections, and why using a broker alone does not create service autonomy.

### 4.4 Database-per-service and data autonomy

- Give each service exclusive authority over its persistent state and schema evolution.
- Distinguish logical data isolation from a requirement for a dedicated physical database server.
- Prohibit direct access to another service's tables, shared EF Core models, and cross-service database joins.
- Introduce references, authorized API composition, and service-owned projections as controlled ways to use external facts.

### 4.5 Contract ownership and compatibility

- Let the API provider own its published API and the event producer own the meaning and schema of its events.
- Give consumers responsibility for their adapters, supported versions, processing behavior, and compatibility evidence.
- Distinguish public schemas from private domain entities and application-owned ports.
- Explain backward-compatible evolution, semantic compatibility, deprecation, and consumer migration.

### 4.6 Local consistency and distributed workflows

- Keep atomic invariants within the owning service's transaction boundary.
- Explain why eligibility checks are observations, while capacity or quota consumption may require an authoritative committed operation.
- Introduce durable workflow state, compensation, reconciliation, and explicit pending or uncertain outcomes.
- Compare orchestration and choreography without requiring a separate workflow service or distributed transaction.

### 4.7 Transactional outbox

- Persist business changes and the obligation to publish in the same local transaction.
- Publish through a relay using stable event identity and acknowledge publication only after broker acceptance.
- Explain the duplicate-publication window after a successful send and before the relay records completion.
- Separate durable publication obligations from broker availability and end-to-end processing guarantees.

### 4.8 Idempotency at different boundaries

- Distinguish HTTP request idempotency, reservation-operation idempotency, event-consumer deduplication, and external-provider idempotency.
- Define key scope, request fingerprint, uniqueness enforcement, stored outcome, concurrent attempts, retention, and authorized replay.
- Commit consumer deduplication records and local effects atomically before acknowledging a message.
- Explain why an inbox alone cannot make a payment charge or notification send exactly once.

### 4.9 Resilience and failure isolation

- Separate business rejection, transient transport failure, permanent contract failure, and unknown operation outcome.
- Explain deadlines, bounded retries with jitter, circuit breaking, concurrency limits, bulkheads, and backpressure.
- Relate fault isolation to shared databases, connection pools, brokers, gateways, and platform capacity as well as process boundaries.
- Define graceful degradation according to business commitments and dependency criticality.

### 4.10 Independent deployment and evolution

- Distinguish independent deployment from independent execution of every business workflow.
- Explain how compatible contracts, private schemas, service-specific releases, and operational ownership support autonomy.
- Identify coordinated releases, shared domain packages, and synchronized migrations as evidence of coupling.
- Treat containers, Kubernetes, and a particular broker as deployment choices rather than defining requirements of microservices.

## 5. Architecture discussion

### 5.1 Service ownership model

- Develop the seven logical services from Chapter 47 without claiming they are all implemented or must be extracted immediately.
- For each service, specify its owner, authoritative decisions, private data, published contracts, dependencies, and recovery responsibilities.

| Service | Business responsibility | Authoritative state and boundary |
| --- | --- | --- |
| Booking Service | Accept requests, coordinate prerequisites, and protect the booking lifecycle. | Booking identity, selected references and snapshots, lifecycle state, workflow recovery state, and booking events; account, fleet, and capacity decisions remain external. |
| Corporate Account Service | Govern corporate-account status and contractual eligibility. | Account and commercial-eligibility state, applicable agreement references, and authorized organizational relationships; numerical limits and consumption ownership require explicit decisions. |
| Fleet Service | Govern fleets, vehicles, membership, and vehicle eligibility. | Fleet and vehicle records and membership decisions; provide batch validation rather than one remote call per vehicle. |
| Station Operations Service | Govern station capability, operational capacity, reservations, and fulfillment. | Station capabilities, capacity allocation, reservation state, and qualifying service-completion facts; availability reads do not reserve capacity. |
| Billing Service | Recognize billable completed work, issue invoices, and manage receivables. | Billable records, invoice lines, consolidated invoices, and receivables; use approved commercial inputs without duplicating their source policy. |
| Payment Service | Execute or record payment and reconcile provider outcomes. | Payment attempts, provider references, and payment status; expose settlement facts while Billing owns receivable application. |
| Notification Service | Create and dispatch authorized communications. | Delivery intents, deduplication records, attempts, and delivery outcomes; delivery failure does not reverse a booking. |

### 5.2 API Gateway and service discovery

- Route external traffic through the gateway with authentication integration, request limits, rate limiting, version routing, and correlation propagation.
- Keep fleet validation, capacity decisions, and booking orchestration inside the owning services.
- Address gateway availability and scaling without using it as the universal route for internal traffic.
- Distinguish gateway routing, internal service discovery, and broker topic/subscription addressing.
- Compare configured endpoints for local development with platform-managed naming and discovery in deployment; avoid hard-coded instance addresses and unnecessary custom registries.

### 5.3 Corporate fleet booking: immediate decisions

1. Receive the authorized Northstar request through the gateway and establish trusted caller/account scope.
2. Validate request shape, resource access, and the scoped idempotency key.
3. Obtain Corporate Account's authoritative eligibility decision and Fleet's batch membership/eligibility result.
4. Ask Station Operations to commit the required capacity using a stable operation identity and an approved reservation protocol.
5. Confirm only when the required authorization and capacity commitments exist; distinguish temporary holds from committed reservations.
6. Atomically persist Booking-owned state, the idempotency outcome, and the `BookingConfirmed` outbox obligation.
7. Return the committed booking outcome; retain an explicit pending or uncertain result where the workflow has not reached confirmation.
- Keep the service interactions coordinated by Booking rather than forming an Account-to-Fleet-to-Station call chain.
- Treat grouping all twenty-four vehicles, partial acceptance, and whole-request atomicity as separate business decisions.

### 5.4 Corporate fleet booking: deferred consequences

- Publish `BookingConfirmed` after the Booking transaction commits; Notification creates a durable delivery intent.
- Let Station Operations publish `ServiceCompleted` when qualifying fulfillment occurs; Booking and Billing react through their own contracts and transactions.
- Accumulate Northstar's qualifying completed work for consolidated invoicing under approved billing terms.
- Introduce Payment when the financial workflow requires settlement; do not insert a mandatory payment-provider call into Northstar's booking confirmation.
- Resolve notification recipients through an authorized contract and minimize sensitive data in shared events.

### 5.5 Capacity coordination and distributed recovery

- Define reservation identity, ownership, lifecycle, status inquiry, release, and concurrency enforcement before implementing the protocol.
- Record enough durable intent to recover when Booking restarts after Station Operations commits capacity.
- Handle reservation success followed by Booking failure through approved compensation, expiry, or reconciliation; local rollback cannot undo remote work.
- Treat a lost reservation response as uncertain; query by stable operation identity before deciding whether to repeat or compensate.
- Address cancellation racing with delayed confirmation, stale responses, duplicate commands, and compensation failures.
- Require an authoritative consumption operation if a future quota spans concurrent requests; do not enforce it with a cached count or read-then-write sequence.

### 5.6 Event publication, inbox processing, and replay

- Specify producer outbox schema, transaction boundary, relay concurrency, acknowledgment, retry budget, and backlog monitoring.
- Specify consumer inbox uniqueness by consumer and event identity; commit the inbox record with local changes.
- Preserve stable event IDs during transport redelivery and operational replay.
- Define per-entity ordering requirements, version checks, out-of-order handling, and poison-message isolation without assuming global ordering.
- Govern dead-letter handling and replay through authorization, audit evidence, compatibility checks, and retention rules.
- Use durable dispatch and provider idempotency or reconciliation for effects outside the consumer's local database.

### 5.7 API and event contract design

- Define business-oriented APIs for eligibility, batch fleet validation, reservation commitment/status, and booking operations.
- Document success, business rejection, authorization failure, conflict, pending outcome, and dependency-unavailable responses.
- Define event envelopes with event ID, type/version, occurrence time, producer, correlation, and justified causation or ordering metadata.
- Keep domain events private and map public integration events from committed facts.
- Preserve the Chapter 47 mapping between chapter-specific event examples and the runnable contract; avoid silently treating creation and confirmation as the same fact.
- Validate additive changes and semantic defaults with actual consumers; plan explicit migration for breaking changes.

### 5.8 Data ownership, projections, and retention

- Give each service private schemas, credentials, migrations, and backup/recovery responsibilities, including its outbox or inbox.
- Reference external identities without cross-database foreign keys or imports of another service's domain projects.
- Define which transaction snapshots must remain stable and which projections may refresh asynchronously.
- Make projection freshness visible where it affects a user decision; reserve authoritative calls or commitments for critical invariants.
- Use read-only, governed reporting projections rather than operational cross-service joins; address sensitive-data retention and deletion across replicas.

### 5.9 Resilience by dependency and failure mode

- Budget the end-to-end booking deadline and limit synchronous fan-out; propagate cancellation and avoid retries multiplying across layers.
- Keep failure of required eligibility or capacity decisions from producing false confirmation.
- Permit continued local commits during a broker outage only while durable storage, backlog limits, and recovery capacity support that operating mode.
- Isolate Notification and Payment provider failures from unrelated capabilities.
- Define bounded queues, worker concurrency, retry exhaustion, reconciliation ownership, and operator intervention.
- Separate liveness from readiness; assess readiness against the work a service promises to accept rather than every downstream dependency.

### 5.10 Configuration and secrets

- Separate service configuration, environment configuration, secrets, business policy, and runtime feature controls.
- Configure endpoints, timeouts, retry budgets, broker addresses, and database connections without service-instance addresses in business code.
- Validate required configuration at startup, control changes, rotate credentials, and define reload or restart behavior.
- Keep secret values out of source control, AI context, logs, and release evidence.

### 5.11 Security across boundaries

- Validate caller identity at entry points and enforce resource-level account/fleet authorization inside services.
- Define workload identity and delegated user authority for service calls; do not trust arbitrary forwarded headers or body-supplied organization IDs.
- Apply least privilege to private stores, broker publications/subscriptions, gateways, and provider adapters.
- Protect replay, support, and administrative operations; authenticate and deduplicate payment callbacks.
- Use transport security, payload validation, safe error responses, data minimization, and auditable privileged actions.

### 5.12 Observability and operational ownership

- Trace the gateway request, eligibility calls, reservation operation, local commit, relay publication, consumer handling, and dispatch.
- Distinguish trace correlation from stable business, workflow, reservation, and event identities.
- Track request latency/error rate, confirmation outcomes, uncertain reservations, compensation backlog, outbox age, consumer lag, dead letters, and delivery outcomes.
- Use service dashboards, business-workflow views, service-level objectives, and alerts tied to accountable operators.
- Keep sensitive data and unbounded identifiers out of metric labels; separate diagnostic telemetry from durable audit records.

### 5.13 Independent deployment and versioning

- Build, version, and release each deployable with service-owned configuration and migrations; explain that a monorepo can support independent releases.
- Identify affected services when contracts, shared build files, or approved technical packages change.
- Support old and new instances concurrently through compatible APIs, events, and expand-and-contract schema migrations.
- Record image/source revision, contract versions, migration revision, and validation evidence for each release.
- Test application rollback against the resulting schema and retained messages; use forward recovery where data changes cannot be reversed safely.
- Evaluate containers, orchestration, databases, and brokers against requirements and ADRs rather than selecting products through the chapter outline.

### 5.14 Testing service boundaries and failure behavior

- Use domain/application tests for owned rules and local orchestration; retain Chapter 48's dependency checks.
- Use database integration tests for constraints, concurrent capacity commitments, idempotency races, and state/outbox atomicity.
- Use provider and consumer contract tests for API/event compatibility at supported versions.
- Use broker-backed tests for redelivery, acknowledgment timing, relay crash windows, consumer restarts, and poison messages.
- Use focused workflow tests for accepted bookings, unauthorized fleets, rejected capacity, ambiguous reservation results, and downstream recovery.
- Add security isolation, representative load/backpressure, and controlled failure-injection tests where their risks justify them; avoid relying on an exhaustive end-to-end suite.
- Verify independent release with compatible neighboring service versions and preserve existing Booking create/read behavior.

### 5.15 Microservice governance and evolution

- Maintain a service catalog with capability, accountable owner, contracts, data classification, dependencies, service-level objectives, and runbooks.
- Standardize security, telemetry, contract compatibility, evidence, and recovery expectations while allowing justified service-local implementation choices.
- Record boundary changes, new synchronous dependencies, shared packages, and exceptions in ADRs.
- Detect distributed-monolith symptoms through coordinated releases, chatty calls, shared schema changes, cyclic dependencies, and incident propagation.
- Extract incrementally with one authoritative writer during migration; consider merging boundaries when evidence shows persistent inseparability.
- Keep the engineering Harness outside the runtime booking path and bind its validation evidence to the tested revision.

## 6. Professional diagrams

- Specify the following six figures for the manuscript, each with a title, descriptive caption, legend where needed, and a reference from the discussion.
- Label proposed components and distinguish synchronous requests, asynchronous events, ownership, and infrastructure dependencies.
- Use top-down layouts for the landscape and split sequence views into linked panels with no more than five participants per panel.

### Figure 49-1 — Microservice landscape

- **Type:** Logical component/context diagram.
- **Content:** Corporate portal, gateway, seven business services, broker, and external payment/communication providers, grouped by business responsibility.
- **Caption focus:** Independent business capabilities collaborate through explicit contracts; the proposed landscape is distinct from the current implementation.
- **References:** Sections 5.1–5.4.

### Figure 49-2 — Service ownership

- **Type:** Bounded-context and ownership map.
- **Content:** Each capability, its authoritative decisions, accountable engineering owner role, and published relationship to adjacent contexts; include Booking's coordination boundary.
- **Caption focus:** Coordinating the workflow does not transfer account, fleet, capacity, financial, or delivery authority to Booking.
- **References:** Sections 4.1–4.2, 5.1, and 5.15.

### Figure 49-3 — Service communication

- **Type:** Directed communication diagram with request/event legend.
- **Content:** Booking's immediate calls to Corporate Account, Fleet, and Station Operations; confirmed-booking events to Notification; completion events to Booking/Billing; later financial integration with Payment.
- **Caption focus:** Required decisions and deferred consequences have different timing and failure contracts; the gateway does not orchestrate business work.
- **References:** Sections 4.3 and 5.2–5.7.

### Figure 49-4 — Data ownership

- **Type:** Service-to-private-store diagram.
- **Content:** Each service's private database, schema/credential boundary, producer outbox or consumer inbox where relevant, and separately owned projections.
- **Caption focus:** Shared physical infrastructure does not permit shared business-table access; copied facts retain their source and freshness semantics.
- **References:** Sections 4.4, 5.6, and 5.8.

### Figure 49-5 — Corporate fleet booking sequence

- **Type:** Sequence diagram in linked panels.
- **Content:** Panel A covers caller authorization and account/fleet decisions; Panel B covers capacity commitment and Booking's atomic state/outbox transaction; Panel C covers publication and notification. Show later completion, consolidated billing, and settlement in a separate continuation panel.
- **Caption focus:** Confirmation requires committed capacity; notification and later corporate settlement remain distinct outcomes. Grouping and partial acceptance remain explicit policy decisions.
- **References:** Sections 5.3–5.6.

### Figure 49-6 — Failure and resiliency flow

- **Type:** Recovery flowchart with distinct failure branches.
- **Content:** Required decision unavailable; reservation response lost; capacity committed but Booking not committed; broker unavailable after local commit; duplicate event; notification/provider send uncertain or failed.
- **Caption focus:** Rejection, pending reconciliation, compensation, durable retry, and operator escalation follow the actual business outcome; downstream delivery failure does not undo confirmed capacity.
- **References:** Sections 5.5–5.6, 5.9, and 5.12.

## 7. Hands-on example

### 7.1 Establish a reproducible baseline

- Inspect the current Booking projects, accepted ADRs, pending-create/read behavior, tests, and applicable repository Instructions.
- Record implemented behavior, proposed additions, and policy decisions required for the lab.
- Define a two-service executable slice using Booking and Notification, with contract fixtures/adapters for Corporate Account, Fleet, and Station Operations; label test doubles explicitly.
- Keep the full seven-service landscape as the target model rather than claiming seven completed implementations.

### 7.2 Define ownership and contract artifacts

- Produce a capability/service matrix, dependency inventory, data-ownership map, and scoped decision register.
- Define the required API contracts, `BookingConfirmed` schema, consumer subscription, and compatibility fixtures.
- Trace approved rules BR-FLT-001/002, BR-SVC-001, BR-CAP-001/002, and BR-AUT-001 to enforcement points and tests.

### 7.3 Implement the governed booking interaction

- Extend direct application orchestration through narrow ports and external adapters while preserving Clean Architecture.
- Add caller/resource authorization, batch fleet validation, stable reservation identity, and the selected capacity protocol.
- Preserve explicit pending/confirmed/uncertain outcomes and protect concurrent attempts with database-enforced rules.

### 7.4 Add atomic persistence and outbox publication

- Commit Booking state, scoped request outcome, and event obligation together.
- Add a relay with safe claiming, stable event identity, bounded retries, and observable backlog.
- Demonstrate the crash window between broker acceptance and the relay's completion record.

### 7.5 Add an independently owned notification consumer

- Use a separate service, private store, and versioned event subscription.
- Atomically record inbox processing and a durable delivery intent; dispatch through a fake provider with documented duplicate/uncertainty behavior.
- Resolve authorized recipient information without reading Booking's private database.

### 7.6 Exercise failure and recovery

- Simulate account/fleet unavailability, capacity rejection, lost reservation responses, and Booking failure after remote commitment.
- Interrupt the broker, relay, and consumer at defined handoff points; verify restart, redelivery, and deduplication behavior.
- Simulate provider failure and ambiguous sending; verify durable recovery and stable booking state.
- Test a caller from another account and an idempotency-key collision with a different request.

### 7.7 Verify deployment and compatibility

- Release a compatible Notification change without redeploying Booking and validate old/new supported event fixtures.
- Show service-specific configuration, readiness, private credentials, telemetry, and deployment evidence.
- Run unit, integration, contract, architecture, and selected recovery checks; distinguish fixture evidence from real cross-service integration evidence.

### 7.8 Hands-on acceptance criteria

- Unauthorized callers cannot access another account's fleets or bookings.
- No booking is confirmed without the approved prerequisites and committed capacity.
- Concurrent retries do not create duplicate booking or reservation commitments; uncertain operations remain recoverable.
- Booking state and its publication obligation commit atomically, and publication failure remains visible.
- Redelivery creates one durable notification intent for the consumer/event identity; provider-side limits are stated accurately.
- Notification failure leaves the confirmed booking intact and produces actionable recovery evidence.
- Services use published contracts and their own data; architecture checks reject cross-service implementation dependencies.
- A compatible consumer release succeeds independently, and existing Booking create/read behavior remains valid.

## 8. Claude example

### 8.1 Prepare Repository Intelligence

- Supply the business-rule baseline, service catalog, accepted ADRs, current code map, contracts, and relevant failure fixtures.
- Separate persistent Instructions, reusable Skills, the task Prompt, and temporary Steering Notes.
- Identify permitted service boundaries, forbidden database access, unresolved policies, and acceptance evidence.

### 8.2 Outline a focused implementation prompt

- Request one booking-to-notification slice with discovery before changes, direct application orchestration, and private stores.
- Require explicit contract ownership, outbox/inbox transactions, replay identity, authorization, and recovery behavior.
- Prohibit invented booking limits, shared domain entities, gateway business orchestration, unapproved CQRS, and silent contract breaking.
- Require a file/change summary and actual verification results, including any environment limitations.

### 8.3 Apply Harness responsibilities and validation

- Use the established Lead, Developer, Reviewer, Validator, and Evaluator responsibilities to plan, implement, challenge, verify, and assess the change.
- Enforce deterministic gates for project references, contract compatibility, private-store access configuration, security checks, and relevant behavioral tests.
- Retain human accountability for business policy, boundary changes, release decisions, and governed replay.
- Keep temporary run observations separate from authoritative architecture knowledge.

## 9. GitHub Copilot comparison

- Apply the same slice, Repository Intelligence, contracts, boundaries, and acceptance criteria in an IDE-centered workflow.
- Focus review on generated clients, service references, shared packages, persistence access, and timeout/retry behavior.
- Compare context delivery and review workflow only where materially different; use the same deterministic gates and recovery evidence.

## 10. Codex comparison

- Apply the same task through repository discovery, applicable `AGENTS.md` Instructions, focused changes, and executable checks.
- Require explicit inspection of current service implementations and contract fixtures before adding deployables or dependencies.
- Compare workflow and resulting evidence against the same ownership, compatibility, security, and recovery rubric used for Claude and Copilot.

## 11. Best practices

- Align boundaries with business authority, cohesive models, and accountable ownership.
- Keep local invariants atomic and cross-service commitments explicit.
- Design coarse enough APIs to avoid chatty synchronous dependencies.
- Publish committed facts through an outbox and make consumers safely repeatable.
- Keep private data private; use governed contracts and justified projections.
- Build partial-failure handling, security, observability, and runbooks into each service.
- Prove compatibility and independent deployment through CI and release evidence.
- Introduce services incrementally and revisit boundaries using change and operational evidence.

## 12. Anti-patterns

- Creating one service per table, entity, technical layer, or team preference.
- Sharing business databases, EF Core contexts, domain entities, or policy libraries across services.
- Building a distributed monolith with cyclic calls and coordinated releases.
- Turning the gateway or Booking into the owner of every business rule.
- Performing a synchronous call per vehicle or waiting for every downstream consequence before confirming.
- Treating cached eligibility or an availability response as a committed reservation.
- Using distributed transactions as the default workflow design or hiding incomplete work behind success responses.
- Publishing before commit, claiming exactly-once processing, or acknowledging before durable consumer work.
- Retrying non-idempotent writes, payments, or uncertain sends without reconciliation.
- Enforcing authorization only at the gateway or trusting organization IDs in request bodies.
- Treating process separation as sufficient failure isolation or making optional dependencies mandatory readiness checks.
- Allowing AI Agents to invent policy, weaken validation, expose private stores, or change contracts without governance.

## 13. Architect's notes

- A bounded context protects meaning; a deployment boundary adds operational commitments that must be justified.
- Correct ownership and consistency boundaries matter more than service size or technology diversity.
- Booking coordination can remain cohesive without absorbing neighboring services' authority.
- Compensation is a new business action with its own failure modes, not a remote database rollback.
- Independent deployment requires compatibility and operational evidence, while required business dependencies remain real.
- Service consolidation can be a sound evolution when repeated change and recovery costs reveal an unsuitable split.

## 14. Enterprise tips

- Publish service scorecards containing owners, contracts, dependencies, service-level objectives, and recovery links.
- Maintain contract registries and representative consumer fixtures for human and AI-assisted changes.
- Establish retry, timeout, backlog, and replay standards with service-specific justification.
- Give replay and reconciliation explicit owners and rehearse representative recovery scenarios.
- Review shared libraries, synchronous call graphs, and coordinated-release frequency as architecture signals.
- Keep reusable platform capabilities focused on technical concerns and governed interfaces.
- Record validation evidence against source and contract revisions; keep diagrams aligned with deployed behavior.

## 15. Decision points

| Decision | Alternatives and evidence required |
| --- | --- |
| When should a capability become independently deployed? | Compare modular boundaries with service extraction using change patterns, scaling needs, security, ownership, and operational cost. |
| How should Northstar's vehicles be grouped? | Decide individual versus grouped bookings, partial acceptance, and capacity commitment semantics with business owners. |
| How is capacity committed and recovered? | Choose the reservation lifecycle, durable coordination, compensation, status inquiry, and reconciliation behavior. |
| Where is any booking allowance consumed? | Define the policy and authoritative writer before implementing concurrency-safe consumption or release. |
| Which interactions require an immediate answer? | Identify the business deadline, freshness requirement, failure result, and cost of synchronous coupling. |
| When is choreography sufficient? | Compare simple event reactions with durable orchestration for multi-step workflows, deadlines, and compensation. |
| How should external facts be consumed? | Compare live APIs, transaction snapshots, and projections according to authority and acceptable staleness. |
| How are contracts and schemas evolved? | Define supported consumers, compatibility evidence, migration windows, deprecation, and rollback constraints. |
| Which platform and messaging products are justified? | Evaluate delivery semantics, operations, cost, discovery, local reproducibility, and recovery requirements in ADRs. |
| What changes require governance review? | Identify owners for new boundaries, contract breaks, security changes, shared components, and policy exceptions. |

## 16. Exercises

### 16.1 Evaluate a proposed decomposition

- Review a table-driven service design and produce revised capability boundaries, rationale, and conditions for extraction or consolidation.

### 16.2 Specify service and data ownership

- Create the seven-service catalog and private-data map, identifying authoritative writers, contract owners, and prohibited access paths.

### 16.3 Design the corporate fleet booking sequence

- Model successful, rejected, pending, and uncertain outcomes; document unresolved grouping and partial-acceptance policies without inventing answers.

### 16.4 Prove capacity recovery

- Define and test the lost-response and remote-success/local-failure cases using stable operation identity, restart recovery, and an explicit reconciliation owner.

### 16.5 Validate outbox and consumer idempotency

- Inject a relay crash and duplicate delivery; show one durable consumer effect and explain the separate limits of external-provider guarantees.

### 16.6 Evolve a contract independently

- Propose an event change, run old/new consumer compatibility checks, and document independent release, migration, and rollback constraints.

### 16.7 Review operational and security readiness

- Define trusted identity propagation, cross-account tests, discovery/configuration, service-level objectives, backlog alerts, and a recovery runbook for the scenario.

### 16.8 Review an AI-generated implementation

- Identify boundary, consistency, security, and compatibility violations; propose focused corrections and the evidence needed to accept them.

## 17. Interview questions

1. How do business capabilities, bounded contexts, services, and deployment units differ?
2. What evidence justifies extracting a microservice from a modular monolith?
3. What does Booking own, and which decisions must remain with Corporate Account, Fleet, and Station Operations?
4. Why can separate databases on a shared server preserve ownership while shared tables undermine it?
5. When should a service use a synchronous API rather than consume an event or projection?
6. Why does an eligibility read not enforce a concurrent quota or reserve capacity?
7. What happens when capacity is committed but Booking cannot finish its transaction?
8. How should a lost response from a reservation or payment operation be handled?
9. Which failure window does a transactional outbox address, and which duplicate-delivery window remains?
10. How do request idempotency, consumer deduplication, and provider idempotency differ?
11. When would orchestration be preferable to choreography, and what must be durable?
12. How do bounded retries, deadline budgets, and bulkheads limit failure propagation?
13. What belongs in an API Gateway, and what belongs in service discovery?
14. How should resource authorization and workload identity operate across service boundaries?
15. Which signals reveal a booking stuck between reservation, confirmation, publication, and notification?
16. How do contract and database changes affect independent deployment and rollback?
17. Which tests provide useful evidence without requiring every service in every test run?
18. How can a Harness detect AI-generated architectural drift, and which decisions still require accountable human judgment?

## 18. Chapter summary

- Recap capability-based boundaries and the seven services' distinct authority.
- Reinforce private data, explicit contracts, local transactions, and compatible independent releases.
- Summarize corporate fleet booking as immediate authoritative decisions followed by durable asynchronous consequences.
- Reaffirm that confirmation requires committed capacity and that corporate billing/payment timing follows approved terms.
- Connect outbox, idempotency, failure isolation, reconciliation, and operational ownership into a coherent recovery model.
- Reinforce governed AI-assisted development and introduce Chapter 50's deeper treatment of event-driven architecture.

## 19. Further reading

- **Service design:** Foundational microservices and domain-driven design references covering capability decomposition, bounded contexts, and independent evolution.
- **Distributed workflows and data:** Authoritative references for sagas, transactional outbox, idempotent consumers, compensation, and projection consistency.
- **Contracts and operations:** Official API/schema specifications and guidance for compatibility, workload identity, OpenTelemetry, deployment, and recovery on the selected runtime/platform.
- **Handbook cross-references:** Chapters 46–48 for business and architecture foundations; Chapters 12–18 for Harness responsibilities and validation; Chapters 29–34 for governance, security, approval, audit, policies, and compliance; Chapters 44–45 for dependency evidence and governed improvement.
- **Continuation:** Chapter 50 for event-driven architecture, Chapter 51 for security, and Chapter 52 for observability.

Chapter 49 outline is complete.
