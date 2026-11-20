# Chapter 50 — Event-Driven Architecture

**Part VII — Alpha Car Detailing**  
**Target file:** `book/50-event-driven.md`  
**Status:** Detailed outline

> **Core principle:** Events communicate facts that have already happened. Services should react independently without creating hidden synchronous dependencies.

## 1. Story-driven Opening

### 1.1 A confirmed booking meets an unavailable consumer

- Continue Northstar Distribution’s request for twenty-four vehicles across two days.
- Corporate Account establishes account eligibility, Fleet validates the selected vehicles, and Station Operations commits the required capacity.
- Booking records confirmation and its publication obligation in one local transaction.
- Introduce a Notification outage followed by a relay restart that causes duplicate publication.
- Establish the required outcome: the booking remains confirmed, communication remains recoverable, and duplicate delivery must not create duplicate notification intents.

### 1.2 Follow the business beyond confirmation

- Station Operations later records qualifying completed work.
- Booking updates its lifecycle while Billing independently recognizes billable work.
- Consolidated invoicing follows Northstar’s approved commercial terms; Payment participates when settlement is required.
- Show why these consequences need distinct owners, processing timelines, and recovery procedures.
- Frame the chapter around durable facts, independent reactions, and explicit operational responsibility.

## 2. Learning Objectives

After completing this chapter, the reader will be able to:

- Distinguish commands, domain events, integration events, and transport acknowledgments.
- Assign event ownership and define stable, business-oriented contracts.
- Design topics, subscriptions, consumer groups, partitions, and processing checkpoints.
- Evaluate Azure Event Hubs and Apache Kafka against workload and recovery requirements.
- Combine transactional outboxes, idempotent consumers, and at-least-once processing.
- Handle duplicates, retries, dead-letter isolation, ordering, and eventual consistency.
- Recover interrupted workflows and external side effects without false success claims.
- Apply security, observability, contract testing, schema evolution, and event governance.
- Use AI Agents and the Harness to develop event integrations with reviewable evidence.

## 3. Background

### 3.1 Relationship to Chapters 46–49

- Chapter 46 establishes business vocabulary, approved invariants, and unresolved policies.
- Chapter 47 establishes the proposed service landscape and initial integration-event catalog.
- Chapter 48 establishes Clean Architecture within each service.
- Chapter 49 establishes service ownership, synchronous prerequisites, distributed recovery, and independent deployment.
- Chapter 50 develops the event contracts, delivery mechanisms, consumer behavior, and operational controls supporting those boundaries.

### 3.2 Preserve the implementation baseline

- Distinguish the runnable Booking create/read baseline from proposed event-driven extensions.
- Preserve `POST /api/bookings` as pending creation unless an approved implementation changes that behavior.
- Explain why the existing in-memory `BookingCreatedDomainEvent` establishes neither confirmation nor durable integration-event publication.
- Retain direct application services and the accepted decision to defer CQRS and a mediator.
- Mark proposed contracts, adapters, workers, and deployables explicitly.

### 3.3 When event-driven architecture is appropriate

- Assess independent reactions, workload buffering, differentiated scaling, and recovery from temporary consumer unavailability.
- Compare asynchronous integration with a direct API when the caller requires an immediate authoritative decision.
- Explain operational costs: delayed propagation, duplicate processing, retained contracts, distributed diagnosis, and replay.
- Distinguish event-driven architecture from event sourcing and CQRS; neither is a prerequisite.

## 4. Concepts

### 4.1 Events as committed business facts

- Define an event through its business meaning, authoritative producer, and publication conditions.
- Use past-tense names such as `BookingConfirmed`, `ServiceCompleted`, and `InvoiceIssued`.
- Separate the time a fact occurred from publication, receipt, and processing times.
- Explain why a published fact remains historical even when a later event changes the current state.

### 4.2 Commands versus events

| Message | Meaning | Alpha Car Detailing example |
| --- | --- | --- |
| Command | Request that an accountable owner perform an action | Request capacity reservation from Station Operations |
| Event | Announce a fact established by its owner | `BookingConfirmed` |
| Query | Request information or an authoritative observation | Inquire about a reservation operation |
| Acknowledgment | Confirm a defined transport or processing milestone | Broker accepts a published record |

- Explain rejection, acceptance, completion, and uncertain outcomes for commands.
- Identify commands disguised as events, such as naming a request `SendInvoiceEvent`.
- Show why publishing an event does not prove that every consumer completed its work.

### 4.3 Domain events versus integration events

- Keep domain events within the owning bounded context and implementation model.
- Publish integration events as explicitly governed contracts for external consumers.
- Map selected domain changes into integration contracts without exposing entities or persistence models.
- Persist the public publication obligation atomically with the originating state change.
- Preserve the distinction between pending creation and confirmed booking when reviewing earlier event examples.

### 4.4 Producers, consumers, and ownership

- Define producer responsibility for fact validity, schema, publication, compatibility, and support.
- Define consumer responsibility for validation, local effects, progress tracking, recovery, and supported versions.
- Distinguish ownership of a business event from operation of the messaging platform.
- Explain how consumers can evolve independently while remaining dependent on the event’s documented meaning.

### 4.5 Event notification and event-carried state

- Compare small notifications containing references with payloads carrying sufficient facts for independent processing.
- Explain how mandatory callbacks to the producer can reintroduce synchronous availability dependencies.
- Balance consumer autonomy against sensitive-data exposure, payload size, and duplicated information.
- Distinguish immutable transaction evidence from refreshable projections.

### 4.6 Event envelopes and schemas

- Outline common metadata: event ID, type, schema version, producer, occurrence time, correlation ID, and justified causation metadata.
- Define business payload fields, identifiers, units, timestamps, optionality, and validation rules.
- Separate schema version, entity revision, broker offset, and service build version.
- Evaluate JSON Schema, Avro, or Protobuf according to the selected platform and tooling.
- Explain the complementary roles of an event envelope, payload schema, schema registry, and AsyncAPI description.

### 4.7 Delivery guarantees and duplicate events

- Explain at-most-once, at-least-once, and the scope of exactly-once guarantees.
- Adopt at-least-once processing as the reference design assumption.
- Identify duplicate windows in producer retries, relay crashes, consumer restarts, and operational replay.
- Distinguish repeated delivery of one event from separate events describing the same logical business effect.
- Explain why Kafka transaction guarantees do not automatically cover a service database, payment provider, or email system. [Apache Kafka](https://kafka.apache.org/41/design/design/?utm_source=chatgpt.com)

### 4.8 Ordering and concurrency

- Distinguish partition order, entity lifecycle order, causal order, and global order.
- Select partition keys according to the business ordering requirement.
- Explain how concurrent publishers, parallel handlers, retry paths, and repartitioning affect processing order.
- Define handling for duplicates, stale revisions, missing predecessors, and late-arriving facts.
- Avoid assuming that every entity revision necessarily produces a public event.

### 4.9 Eventual consistency

- Separate immediate local correctness from delayed propagation across services.
- Define acceptable freshness and the evidence required to identify stalled convergence.
- Explain why eventual consistency does not guarantee automatic recovery from permanently failed processing.
- Preserve authoritative account, fleet, and capacity decisions where stale projections cannot satisfy an invariant.

### 4.10 Choreography and orchestration

- Use choreography for independent reactions to established facts.
- Consider durable orchestration when a workflow requires coordinated commitments, deadlines, compensation, or explicit completion.
- Preserve Booking’s coordination responsibility without transferring authority over other services’ state.
- Explain how uncontrolled event chains can obscure workflow ownership and create cycles.

## 5. Architecture Discussion

### 5.1 Event ownership across Alpha Car Detailing

- Extend the event catalog from Chapters 47–49.
- Record each contract’s status as proposed, implemented, supported, or superseded.
- Require a demonstrated consumer need before introducing additional public events.

| Service | Owned facts or responsibility | Principal event interactions |
| --- | --- | --- |
| Booking | Booking lifecycle; `BookingConfirmed`, `BookingCancelled` | Consume relevant completion and reservation-release facts under approved transition rules |
| Corporate Account | Account status and contractual eligibility | Publish account-change facts only through an approved contract; retain authoritative eligibility decisions |
| Fleet | Vehicle membership and eligibility | Publish justified fleet-change facts; retain authority over membership and eligibility |
| Station Operations | Capacity and fulfillment; `ServiceCompleted`, `CapacityReservationReleased`, `StationAvailabilityChanged` | Publish committed operational facts; receive reservation commands through explicit contracts |
| Billing | Billable work, invoices, receivables; `InvoiceIssued` | Consume qualifying `ServiceCompleted` and relevant `PaymentRecorded` outcomes |
| Payment | Payment attempts, provider outcomes, reconciliation; `PaymentRecorded` | Execute authorized financial operations and publish their recorded outcomes |
| Notification | Durable communication intents and delivery processing | Consume approved booking, invoice, and payment facts independently |

- Clarify that `PaymentRecorded` must describe its actual outcome; its name alone does not prove successful settlement.
- Keep notification delivery failure separate from booking validity.
- Avoid treating `InvoiceIssued` as automatic authorization to charge a customer.

### 5.2 Messaging topology: Azure Event Hubs or Kafka

- Compare retained event streams with queue-style and subscription-based messaging.
- Evaluate throughput, replay, ordering, operational effort, isolation, security, and cost.
- Select the reference implementation through an ADR rather than implying that both platforms must be deployed.

| Concern | Apache Kafka | Azure Event Hubs |
| --- | --- | --- |
| Event channel | Topic divided into partitions | Event hub divided into partitions |
| Independent consumption | Separate consumer groups | Separate consumer groups |
| Processing progress | Consumer offsets | Consumer checkpoints or protocol-specific offsets |
| Failed-record isolation | Explicit application or framework design | Explicit application or framework design |

- Explain that Event Hubs consumer groups provide independent views of retained streams, rather than queue-style per-message subscriptions. [Microsoft Learn](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-features?utm_source=chatgpt.com)
- Document client, protocol, and runtime behavior instead of assuming identical capabilities.
- Explain that classic stream consumption requires explicit retry and dead-letter design; Event Hubs does not provide an inherent dead-letter queue. [Microsoft Learn](https://learn.microsoft.com/en-us/azure/architecture/serverless/event-hubs-functions/resilient-design?utm_source=chatgpt.com)

### 5.3 Topics, subscriptions, and independent scaling

- Organize channels around cohesive business contracts, access boundaries, retention, and throughput requirements.
- Give Billing and Booking independent consumption positions for `ServiceCompleted`.
- Use multiple instances within one logical consumer group to scale the same consumer responsibility.
- Explain why placing unrelated services in one competing group prevents each from receiving all required facts.
- Address partition count, hot keys, worker concurrency, backpressure, and batch-size trade-offs.
- Define filtering behavior explicitly; avoid assuming server-side subscription filters exist on every platform.

### 5.4 Corporate fleet booking event flow

1. Establish caller authorization, account eligibility, fleet eligibility, and committed station capacity through the approved workflow.
2. Commit Booking confirmation and the `BookingConfirmed` outbox obligation atomically.
3. Publish the event when the broker is available.
4. Let Notification commit its delivery intent independently.
5. Let Station Operations later commit qualifying completion and its `ServiceCompleted` publication obligation.
6. Let Booking and Billing process completion through separate local transactions.
7. Let Billing issue a consolidated invoice under approved terms and publish `InvoiceIssued`.
8. Introduce Payment when the financial workflow requires it; publish the recorded outcome for authorized consumers.

- Preserve unresolved grouping, partial acceptance, reservation expiry, limits, and commercial policies as explicit decisions.
- Avoid drawing this flow as a mandatory synchronous chain through all seven services.

### 5.5 Transactional outbox

- Explain the dual-write failure between committing business state and publishing directly to a broker.
- Persist state and an immutable publication record in the same local transaction.
- Define event identity, payload, creation time, attempts, claim state, and publication outcome.
- Design safe concurrent relay claiming, crash recovery, and broker acceptance handling.
- Mark publication complete only after the selected broker acceptance milestone.
- Demonstrate duplicate publication after send success followed by relay failure.
- Compare polling and change-data-capture approaches without making either universally mandatory.
- Define backlog limits, retention, cleanup, and recovery ownership.

### 5.6 Idempotent consumers and local effects

- Validate the envelope, schema, producer authority, and applicable account scope.
- Enforce inbox uniqueness using the documented event-identity scope and logical consumer identity.
- Commit deduplication evidence, local state changes, and any downstream outbox obligation together.
- Advance processing progress only after durable handling.
- Handle concurrent duplicates through database-enforced constraints.
- Add business uniqueness where different event IDs could otherwise create the same billable completion or delivery intent.
- Align deduplication retention with supported redelivery and replay horizons.

### 5.7 External side effects

- Separate durable event handling from payment execution and notification dispatch.
- Use service-owned dispatch intents and stable provider operation identifiers.
- Apply provider idempotency where supported and verify its scope and retention.
- Recover the crash window after provider success but before local success recording.
- Define inquiry, reconciliation, escalation, and approved duplicate-risk policies for uncertain outcomes.
- Avoid claiming that an inbox transaction guarantees exactly-once email or payment execution.

### 5.8 Retry policies and dead-letter handling

- Classify transient infrastructure failure, invalid contract, permanent business rejection, and uncertain external outcome.
- Apply bounded exponential backoff with jitter and dependency-aware retry budgets.
- Separate broker-client retries, handler retries, workflow retries, and provider retries.
- Compare partition blocking, durable deferred work, and retry streams against ordering requirements.
- Persist a failed record or retry obligation durably before advancing past it.
- Include original identity, consumer identity, failure classification, attempts, timestamps, and schema metadata in quarantine records.
- Assign investigation, repair, replay, and escalation ownership.
- Treat dead-letter isolation as unresolved work requiring an operational outcome.

### 5.9 Ordering, checkpoints, and recovery

- Define ordering requirements per consumer rather than globally.
- Coordinate relay concurrency, partition keys, handler concurrency, and lifecycle checks.
- Prevent checkpointing beyond an unresolved record unless responsibility has been transferred durably.
- Handle consumer restarts and rebalances without losing local effects or processing obligations.
- Preserve original event identity during redelivery and ordinary replay.
- Explain how retry streams can allow later records to overtake earlier ones.

### 5.10 Eventual consistency and user-visible state

- Distinguish confirmed booking, pending communication, completed service, recognized billable work, issued invoice, and recorded payment.
- Define freshness indicators for projections and operation-status views.
- Identify which customer actions require current authoritative decisions.
- Specify reconciliation for missing or delayed effects.
- Keep a delayed consumer from falsely changing an already committed producer outcome.

### 5.11 Failure recovery and replay

- Cover broker outages, relay crashes, consumer crashes, malformed events, retention expiry, and restored databases.
- Define catch-up capacity and recovery objectives against expected backlog growth.
- Distinguish redelivery, targeted repair, projection rebuild, and historical reprocessing.
- Preserve original facts; use explicit correction events or governed migration artifacts where necessary.
- Suppress or isolate external side effects during projection rebuilds.
- Address deduplication records lost through backup restoration and replay beyond normal retention.
- Record replay scope, authorization, source range, affected consumer, and resulting evidence.

### 5.12 Contract versioning and schema evolution

- Define syntactic and semantic compatibility.
- Evaluate added fields, defaults, enum expansion, identifiers, units, timestamps, and changed publication conditions.
- Test new consumers against retained historical events.
- Test new producers against supported existing consumers.
- Define coexistence, deprecation, migration, and rollback constraints for breaking changes.
- Prevent duplicate business effects when two contract versions represent the same fact.
- Map earlier `eventVersion`, `schemaVersion`, and versioned-name examples explicitly before adopting a canonical convention.

### 5.13 Correlation, causation, observability, and tracing

- Distinguish event identity, business-operation identity, correlation ID, causation ID, and trace context.
- Preserve lineage across outbox publication, retries, consumption, and newly produced events.
- Represent fan-in from multiple completion events without inventing one misleading causal parent.
- Use asynchronous trace links where appropriate for delayed work, batches, and replay.
- Define structured logging without recording sensitive payloads by default.
- Monitor publication delay, oldest outbox age, consumer lag, processing latency, retries, duplicates, dead-letter age, and reconciliation backlog.
- Separate booking success, publication success, consumer completion, and provider delivery in dashboards.

### 5.14 Security

- Authenticate producers and consumers through workload identity.
- Authorize publication and consumption by channel, contract, and operational role.
- Enforce resource scope within consumers; treat payload identifiers and forwarded metadata as untrusted input.
- Prevent event spoofing, cross-account processing, unauthorized replay, and excessive payload consumption.
- Protect broker connections, stored records, retry streams, and dead-letter storage.
- Minimize personal and financial data in broadly distributed events.
- Define retention and deletion responsibilities across logs, projections, archives, and failed-event stores.

### 5.15 Testing, governance, and architectural evolution

- Maintain producer contract tests, consumer behavior tests, and old/new compatibility fixtures.
- Test serialization, unknown fields, semantic defaults, malformed records, and historical versions.
- Use real persistence and selected-broker integration tests for transaction, checkpoint, and restart behavior.
- Inject duplicate, delayed, reordered, and interrupted processing.
- Maintain an event catalog containing owners, meaning, schemas, consumers, security classification, ordering, retention, and support links.
- Apply deterministic gates to contract changes and service-boundary violations.
- Review unused contracts, consumer coupling, event cycles, and repeated recovery incidents.

## 6. Professional Diagrams

- Specify six figures with titles, captions, legends, and references from the corresponding discussion.
- Distinguish implemented components from proposed extensions.
- Use separate linked panels when a sequence would exceed five participants.

### Figure 50-1 — Event-driven Architecture

- **Type:** Logical component diagram.
- **Content:** Service-owned producers and outboxes, broker channels, independent consumer groups, private stores, and external dispatchers.
- **Caption focus:** Producers announce committed facts; consumers own their reactions and recovery.
- **References:** Sections 4.4 and 5.1–5.3.

### Figure 50-2 — Corporate Fleet Booking Event Flow

- **Type:** Sequence diagram in linked panels.
- **Content:** Authoritative prerequisites; Booking confirmation and publication; independent notification; later completion, consolidated billing, and payment outcomes.
- **Caption focus:** Confirmation, fulfillment, communication, and financial processing are separate commitments.
- **References:** Sections 5.4 and 5.10.

### Figure 50-3 — Transactional Outbox

- **Type:** Transaction-boundary and sequence diagram.
- **Content:** Atomic business-state/outbox commit, relay claiming, broker acceptance, publication recording, and crash windows.
- **Caption focus:** The outbox protects the publication obligation while allowing duplicate sends.
- **References:** Sections 5.5–5.6.

### Figure 50-4 — Consumer Retry and Dead-letter Flow

- **Type:** Branching recovery flowchart.
- **Content:** Validation, local transaction, duplicate detection, transient retry, durable quarantine, operator repair, and controlled replay.
- **Caption focus:** Processing progress advances only after durable handling or durable transfer of responsibility.
- **References:** Sections 5.6–5.9 and 5.11.

### Figure 50-5 — Eventual Consistency Across Services

- **Type:** Timeline with service-state lanes.
- **Content:** Booking confirmation followed by independently delayed Notification and later Booking/Billing completion processing.
- **Caption focus:** Each service remains locally authoritative while cross-service views converge through recoverable processing.
- **References:** Sections 4.9 and 5.10.

### Figure 50-6 — Event Ownership and Contracts

- **Type:** Ownership and contract map.
- **Content:** Producer-owned facts, versioned public schemas, consumer-owned adapters and projections, and platform responsibilities.
- **Caption focus:** Shared transport and schema infrastructure do not transfer business authority.
- **References:** Sections 5.1, 5.12, and 5.15.

## 7. Hands-on Example

### 7.1 Establish the executable scope

- Inspect current projects, accepted ADRs, Instructions, and contract fixtures.
- Define a Booking-to-Notification slice with separate persistence boundaries.
- Use explicitly labeled adapters or test doubles for prerequisites not yet implemented.
- Preserve pending-create behavior and add confirmation only through the approved workflow.

### 7.2 Define contract artifacts

- Specify `BookingConfirmed`, its envelope, payload schema, channel, consumer identity, and compatibility fixtures.
- Trace fields and publication conditions to approved business rules.
- Record unresolved policy rather than generating assumptions.

### 7.3 Implement producer durability

- Commit confirmed state and outbox obligation together.
- Add safe relay claiming, bounded retries, restart recovery, and backlog metrics.
- Demonstrate broker unavailability after the business transaction commits.

### 7.4 Implement consumer durability

- Commit Notification’s inbox record and delivery intent atomically.
- Add a dispatcher with a test provider and explicit outcome semantics.
- Prove that concurrent duplicate deliveries create one durable intent.

### 7.5 Add failure isolation and replay

- Introduce malformed records, transient failures, retry exhaustion, and durable quarantine.
- Recover after relay and consumer crashes.
- Replay a repaired case with original identity and audit evidence.

### 7.6 Extend the contract exercise to Billing

- Define Station Operations’ `ServiceCompleted` contract and a separate Billing consumer.
- Protect billable-work recognition through event deduplication and business uniqueness.
- Keep consolidated invoice timing and grouping governed by approved commercial policy.

### 7.7 Acceptance criteria and evidence

- No integration event is published for a rolled-back state change.
- A committed publication obligation survives broker outage and process restart.
- Duplicate delivery produces one intended local business effect.
- Consumer failure does not invalidate a confirmed booking.
- Failed records remain recoverable before processing progress advances.
- Historical and supported contract versions pass compatibility checks.
- External-provider uncertainty remains visible and recoverable.
- Recovery evidence identifies source revision, schema version, event identity, and processing outcome.

## 8. Claude Example

- Provide a concise task using Repository Intelligence, approved contracts, and the executable slice.
- Ask Claude to identify transaction boundaries, duplicate windows, hidden callbacks, and unresolved policies before implementation.
- Require focused changes preserving Clean Architecture and direct application services.
- Use the Lead, Developer, Reviewer, Validator, and Evaluator responsibilities established earlier in the handbook.
- Require deterministic evidence for atomicity, deduplication, compatibility, security, and recovery.
- Retain accountable human ownership of business semantics, contract approval, and operational replay.

## 9. GitHub Copilot Comparison

- Apply the same contracts, scenario, constraints, and acceptance criteria.
- Focus review on generated handlers, persistence transactions, checkpoint timing, and retry behavior.
- Compare context delivery and review workflow where materially different.
- Evaluate resulting behavior and evidence using the same rubric.

## 10. Codex Comparison

- Apply the same task through repository discovery, applicable `AGENTS.md` Instructions, focused implementation, and executable checks.
- Require inspection of existing event names, baseline behavior, and supported consumers before changes.
- Review cross-service dependencies, schema changes, external effects, and recovery procedures.
- Compare outcomes using the same ownership, compatibility, and reliability criteria.

## 11. Best Practices

- Publish meaningful facts owned by the service establishing them.
- Keep required commitments explicit and deferred reactions independently recoverable.
- Persist state and publication obligations atomically.
- Design consumers for duplicates, concurrency, restart, and replay.
- Define ordering only where business behavior requires it.
- Treat contracts as retained public interfaces with accountable owners.
- Make backlog age, failed processing, and external uncertainty observable.
- Validate recovery behavior before increasing throughput or service count.

## 12. Anti-patterns

- Publishing before commit or relying on an unprotected database/broker dual write.
- Treating `BookingCreated` as proof of confirmed capacity.
- Hiding commands inside event names.
- Requiring a synchronous producer callback for every consumed event.
- Sharing private databases or domain assemblies through event integrations.
- Placing unrelated consumers in one competing group.
- Advancing offsets or checkpoints before durable processing.
- Retrying indefinitely or silently discarding poison records.
- Assuming global ordering or automatic dead-letter support.
- Claiming exactly-once external effects from broker or inbox guarantees.
- Replaying historical events into payment or communication dispatchers without controls.
- Allowing AI-generated schemas or tests to establish unapproved business policy.

## 13. Architect’s Notes

- Service autonomy depends on contracts, data sufficiency, recovery, and ownership.
- Eventual consistency cannot replace authoritative enforcement of capacity or financial invariants.
- Event-carried data creates replication and lifecycle responsibilities.
- Ordering requirements constrain partitioning, concurrency, retry, and recovery together.
- Outbox, inbox, dispatch intent, and reconciliation address different failure boundaries.
- Persistent event history creates compatibility obligations beyond the current deployment.
- Choreography should remain understandable to the people responsible for business completion.

## 14. Enterprise Tips

- Maintain searchable event catalogs and producer/consumer ownership records.
- Establish naming, envelope, compatibility, retry, and replay conventions.
- Alert on oldest unresolved work as well as backlog counts.
- Retain representative historical records as sanitized compatibility fixtures.
- Rehearse broker outages, poison records, consumer restoration, and provider uncertainty.
- Give replay and reconciliation explicit operators and auditable procedures.
- Use operational evidence to revise topology and contracts through governed changes.

## 15. Decision Points

| Decision | Alternatives and required evidence |
| --- | --- |
| API, command, or event? | Assess authority, timing, completion semantics, and failure behavior. |
| Kafka or Azure Event Hubs? | Compare stream semantics, operations, replay, isolation, throughput, and cost. |
| Notification or event-carried state? | Balance consumer autonomy, freshness, privacy, and payload size. |
| Topic and consumer-group boundaries? | Evaluate ownership, independent processing, security, retention, and scaling. |
| Partition key and ordering scope? | Identify required lifecycle order, concurrency, and hot-key risk. |
| Polling or change-data-capture outbox? | Assess database support, operational complexity, latency, and recovery. |
| Blocking retry or deferred processing? | Compare ordering requirements, partition availability, and durable transfer. |
| Choreography or orchestration? | Assess workflow visibility, compensation, deadlines, and accountable completion. |
| Contract evolution strategy? | Define supported versions, retained history, migration, and rollback constraints. |
| Replay and retention policy? | Align recovery horizons, deduplication, privacy, and external-effect controls. |

## 16. Exercises

### 16.1 Classify messages and assign ownership

- Review proposed booking, capacity, completion, billing, and notification messages.
- **Acceptance:** Correctly distinguish commands and events and identify the authoritative owner.

### 16.2 Design the event contract catalog

- Specify `BookingConfirmed`, `ServiceCompleted`, `InvoiceIssued`, and `PaymentRecorded`.
- **Acceptance:** Document meaning, publication conditions, schema, consumers, ordering, and privacy classification.

### 16.3 Prove outbox recovery

- Interrupt publication before send and after broker acceptance.
- **Acceptance:** Show no lost committed obligation and explain resulting duplicates.

### 16.4 Prove consumer idempotency

- Deliver duplicates concurrently and repeat one business completion under another event ID.
- **Acceptance:** Demonstrate both transport deduplication and business uniqueness.

### 16.5 Recover a poison record

- Introduce an unsupported or malformed event.
- **Acceptance:** Show bounded handling, durable isolation, controlled progress, repair, and replay.

### 16.6 Evaluate ordering and partitioning

- Model confirmation, cancellation, delayed processing, and retry overtaking.
- **Acceptance:** Specify a consumer policy preserving valid state transitions without assuming global order.

### 16.7 Evolve a retained contract

- Propose an additive change and a breaking semantic change.
- **Acceptance:** Demonstrate old/new compatibility, historical-event handling, and a governed migration plan.

### 16.8 Review an AI-generated design

- Identify premature acknowledgment, shared data access, hidden callbacks, invented policies, and unsafe replay.
- **Acceptance:** Provide focused corrections and executable validation evidence.

## 17. Interview Questions

1. How do domain events and integration events differ?
2. What distinguishes a command from an event?
3. Why does pending booking creation not establish confirmation?
4. Who owns event meaning, schema compatibility, and consumer recovery?
5. How can an asynchronous consumer introduce hidden synchronous coupling?
6. How do subscriptions, consumer groups, and competing consumers differ?
7. Which failure does the transactional outbox solve, and which duplicate window remains?
8. Why must inbox records and local effects commit together?
9. When is event-ID deduplication insufficient?
10. What are the limits of broker exactly-once guarantees?
11. How do partitioning, parallelism, and retries affect ordering?
12. When may a checkpoint advance past a failed event?
13. How should dead-letter isolation differ from successful completion?
14. How do correlation IDs, causation IDs, and trace context differ?
15. How should consumers handle retained events from older schema versions?
16. How can replay duplicate payments or notifications?
17. Which security controls prevent unauthorized production, consumption, and replay?
18. What evidence should a Harness require before accepting an AI-generated event integration?

## 18. Chapter Summary

- Reaffirm events as producer-owned facts about committed outcomes.
- Recap the distinction between authoritative booking prerequisites and independent downstream reactions.
- Connect outbox publication, idempotent consumption, bounded retries, quarantine, and reconciliation.
- Summarize ordering and eventual consistency as explicit business and operational contracts.
- Reinforce compatibility, security, observability, and accountable event governance.
- Revisit Northstar’s scenario: confirmed commitments remain reliable while communication and financial consequences recover independently.
- Connect the chapter to subsequent security and observability discussions.

## 19. Further Reading

- [Event-driven architecture style — Azure Architecture Center](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/event-driven): architectural models, benefits, and trade-offs.
- [Apache Kafka design documentation](https://kafka.apache.org/41/design/design/): delivery semantics, durability, and transaction boundaries.
- [Azure Event Hubs features and terminology](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-features): partitions, consumer groups, offsets, and checkpoints.
- [Resilient design with Event Hubs](https://learn.microsoft.com/en-us/azure/architecture/serverless/event-hubs-functions/resilient-design): processing failures, retry behavior, and failed-event isolation.
- [CloudEvents](https://cloudevents.io/): standardized event-envelope concepts.
- [AsyncAPI Specification](https://www.asyncapi.com/docs/reference/specification/latest): machine-readable descriptions of asynchronous interfaces.
- **Handbook cross-references:** Chapters 46–49 for domain and architecture foundations; Chapters 12–18 for Harness responsibilities; Chapters 29–34 for governance, security, approval, audit, policies, and compliance.

Chapter 50 outline is complete.