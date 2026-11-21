# Chapter 52 — Observability

**Part VII — Alpha Car Detailing**  
**Target file:** `book/52-observability.md`  
**Status:** Detailed outline

> **Core principle:** Observability must provide enough evidence to understand what happened, where it happened, why it happened, and how users were affected.

## 1. Story-driven Opening

### 1.1 A confirmed booking with an incomplete customer experience

- Continue Northstar Distribution's booking of twenty-four vehicles across two days.
- Booking has committed confirmation, but the administrator has not received the expected notification and a downstream view remains stale.
- Show why healthy pods, successful HTTP responses, and isolated application logs cannot establish the whole workflow's outcome.
- Introduce the investigation questions: which commitment succeeded, which consequence is delayed, which customers are affected, and who owns recovery?

### 1.2 Follow evidence across independent commitments

- Follow gateway admission, account eligibility, fleet validation, capacity commitment, Booking persistence, event publication, consumer processing, and provider dispatch.
- Contrast a notification-provider outage with a lost reservation response: one delays communication, while the other can leave a booking operation pending reconciliation.
- Establish that an initial diagnosis is a hypothesis until traces, logs, metrics, durable state, and dependency evidence support it.

## 2. Learning Objectives

After completing this chapter, the reader will be able to:

- Explain how logs, metrics, and traces answer complementary operational questions.
- Design structured logging and distinguish correlation, trace, span, event, causation, and durable operation identities.
- Instrument .NET services with OpenTelemetry while preserving Clean Architecture boundaries.
- Trace synchronous calls and connect asynchronous processing, retries, batches, and replay.
- Monitor APIs, dependencies, databases, event consumers, infrastructure, and meaningful business outcomes.
- Define health checks, service-level indicators, service-level objectives, dashboards, and actionable alerts.
- Investigate distributed failures and quantify customer impact without confusing correlation with proven cause.
- Protect telemetry, preserve separate Audit Trails, and control retention, cardinality, sampling, and cost.
- Use AI Agents to assist troubleshooting under constrained access and independently validated recovery procedures.

## 3. Background

### 3.1 Relationship to earlier chapters

- Apply Chapters 46-47's domain vocabulary, business invariants, service responsibilities, and unresolved policy decisions.
- Preserve Chapter 48's separation of domain behavior, application outcomes, delivery context, and infrastructure instrumentation.
- Extend Chapters 49-50's independently owned services, transactional outboxes, idempotent consumers, dispatch intents, and reconciliation records.
- Apply Chapter 51's telemetry minimization, account isolation, workload identity, incident response, and protected audit requirements.
- Distinguish application reliability measurements from the Harness and engineering productivity measurements discussed earlier in the handbook.

### 3.2 Preserve the implementation baseline

- Retain the current Booking create/read baseline: `POST /api/bookings` creates a `Pending` booking; `201 Created` does not establish confirmation, reserved capacity, or published integration events.
- Keep direct application services and the accepted decision to defer CQRS and mediator adoption.
- Label confirmation workflows, other services, durable messaging, production backends, and Kubernetes deployment as proposed extensions until implemented and verified.
- Keep the twenty-four-vehicle scenario separate from an approved batch boundary, numerical limit, or whole-request atomicity guarantee.

### 3.3 Monitoring, observability, and diagnosis

- Distinguish watching known indicators from investigating system behavior through available evidence.
- Explain why more telemetry does not automatically produce useful evidence or establish root cause.
- Introduce outside-in customer checks, internal instrumentation, durable workflow status, release metadata, and accountable ownership.

## 4. Concepts

### 4.1 Observability principles

- Start with questions about customer commitments, service behavior, performance, partial failure, and recovery.
- Define the evidence needed to connect an observed symptom to an execution boundary and affected business operation.
- Make uncertainty, missing evidence, sampling, clock error, and delayed ingestion explicit.

### 4.2 Logs, metrics, and traces

- Logs: selected records of operations, decisions, errors, and state transitions with searchable context.
- Metrics: aggregate measurements of rates, distributions, saturation, backlog, and outcomes over time.
- Traces: spans describing execution relationships and time spent across instrumented boundaries.
- Explain how a metric anomaly leads to representative traces and contextual logs; introduce exemplars where supported.
- Clarify that metrics do not require retaining every trace and that sampled traces cannot establish exact business totals.

### 4.3 Structured logging

- Define a proposed log contract covering UTC timestamp, severity, stable event name, message template, service, version, environment, outcome, and applicable diagnostic identifiers.
- Use `ILogger` and the established Serilog direction for structured properties; select compatible export paths without duplicate ingestion.
- Define log levels, scopes, bounded fields, sanitization, and ownership of each meaningful record.
- Keep authorized booking, workflow, or event references in controlled diagnostic records; exclude complete requests, event bodies, tokens, and unnecessary personal data.

### 4.4 Correlation IDs and distinct identities

| Identifier | Intended purpose | Required distinction |
| --- | --- | --- |
| Correlation ID | Associate related work under a documented convention | Can span multiple traces and is not authorization evidence. |
| Trace ID and span ID | Identify a diagnostic trace and an operation within it | Can be absent from retained data because of sampling or expiry. |
| Workflow or operation ID | Find durable progress, commitments, and recovery state | Must remain useful without telemetry availability. |
| Booking or reservation ID | Identify an owned business resource or commitment | Access still requires account and resource authorization. |
| Producer and event ID | Identify a published fact within its source scope | Remains stable across delivery attempts and deduplication. |
| Causation ID | Identify an immediate trigger under the approved convention | Does not describe every input to a consolidated invoice. |

- Define propagation and lookup conventions rather than reusing one identifier for every purpose.
- Bound and validate caller-supplied identifiers; define trust-boundary handling of incoming trace context and sensitive baggage.
- Preserve many-to-many relationships in owned evidence when one invoice incorporates several completed service operations.

### 4.5 Distributed tracing

- Explain server, client, internal, producer, and consumer spans, parent-child relationships, span links, attributes, status, and events.
- Introduce W3C trace context across HTTP and approved messaging metadata.
- Distinguish elapsed request time, concurrent child operations, queue waiting, handler execution, and later business milestones.
- Define behavior for absent or invalid context and uninstrumented dependencies; do not present service maps as a complete dependency inventory.

### 4.6 Asynchronous tracing, retries, and replay

- Preserve approved diagnostic context through durable publication without changing the business event's meaning or embedding credentials.
- Explain message creation, publication, receipt, processing, and external dispatch as separate observable operations.
- Use supported span links and documented parentage for delayed work, fan-out, batches, and replay; pin the selected messaging conventions.
- Give attempts distinct diagnostic spans while preserving stable event and business operation identities.
- Avoid one indefinitely open span or one mandatory trace spanning booking, fulfillment, invoicing, and settlement over several days.

### 4.7 OpenTelemetry

- Explain APIs, SDKs, instrumentation libraries, resource attributes, propagators, exporters, OTLP, and the Collector.
- Distinguish instrumentation and transport from telemetry storage, querying, dashboards, and alert evaluation.
- Introduce .NET `ActivitySource`, `Meter`, ASP.NET Core, `HttpClient`, runtime, and supported database instrumentation.
- Define service identity, version, and deployment attributes; pin package and semantic-convention versions during implementation.
- Compare automatic and explicit instrumentation and identify duplicate-span and sensitive-attribute risks.

### 4.8 Service metrics

- Apply request rate, errors, and duration to service interfaces; apply utilization, saturation, and errors to constrained resources.
- Cover counters, gauges, histograms, units, aggregation windows, and appropriate latency percentiles.
- Include runtime and resource indicators such as allocation, garbage collection, thread-pool pressure, CPU throttling, and connection-pool exhaustion.
- Use bounded dimensions such as service, route template, operation, dependency, outcome, and error category.
- Exclude booking, event, vehicle, user, and arbitrary account IDs from metric labels; avoid averaging precomputed percentiles across instances.

### 4.9 Business metrics

- Separate requested, created, pending, confirmed, rejected, cancelled, fulfilled, invoiced, and settled outcomes according to approved lifecycle meanings.
- Track unresolved capacity operations, workflow age, delayed communication, unrecognized completed work, and unreconciled financial operations.
- Distinguish request attempts, idempotent replays, unique operations, vehicles, and bookings; make the unit of each measurement explicit.
- Separate operational counters from authoritative commercial totals; use owned durable state and reconciliation when completeness and deduplication matter.

### 4.10 Health checks

- Distinguish startup, liveness, readiness, background-worker progress, and externally observed customer journeys.
- Keep liveness inexpensive and focused on process health; avoid restart storms caused by a shared dependency outage.
- Make readiness reflect the work a service can currently admit and its mandatory dependencies.
- Explain why a healthy process can have a stalled consumer or an unresolved workflow and why those conditions need separate indicators.

### 4.11 API and dependency monitoring

- Measure gateway admission, route-level latency, timeout rate, status categories, rate limiting, and dependency budgets.
- Distinguish expected domain rejection, authorization denial, throttling, cancellation, dependency failure, and internal defects.
- Monitor authoritative Corporate Account, Fleet, and Station Operations calls separately from Notification and Payment provider interactions.
- Observe retries, circuit-breaker state, DNS and TLS failures, and caller cancellation without multiplying one incident into misleading failure counts.
- Add controlled synthetic journeys using synthetic accounts and defined cleanup; prevent unintended production emails or charges.

### 4.12 Event and consumer monitoring

- Measure oldest unpublished outbox age, publication attempts, broker acceptance delay, consumer lag, oldest unhandled work, and processing duration.
- Track checkpoints or acknowledgments, rebalances, retries, duplicate suppression, schema rejection, quarantine, and controlled replay.
- Distinguish offset lag, record count, elapsed age, and business freshness; none alone proves successful downstream completion.
- Separate inbox commitment, delivery intent, provider acceptance, confirmed delivery where available, and uncertain external outcomes.
- Preserve Chapter 50's broker distinctions: Kafka and Event Hubs require an explicit failed-record isolation and recovery design rather than an assumed queue-style dead-letter feature.

### 4.13 Database and cache monitoring

- Monitor connection acquisition, query duration, transaction duration, locks, deadlocks, timeouts, storage pressure, and outbox or inbox growth.
- Investigate slow queries and query plans within the owning service's database access boundary.
- Monitor cache latency, hit and miss rates, eviction, and relevant projection freshness without treating cached state as authoritative commitment evidence.
- Distinguish the local SQLite baseline from a proposed SQL Server deployment; verify instrumentation against the actual engine and driver.
- Avoid recording raw parameter values, connection strings, sensitive SQL text, or complete financial records.

### 4.14 Error and exception monitoring

- Define stable error categories, sanitized exception records, error ownership, and appropriate stack-trace access.
- Record a failure at the responsible boundary while retaining causal context; prevent repeated logging at every layer from inflating incident counts.
- Separate expected business outcomes from unexpected exceptions and terminal failure from an uncertain operation awaiting reconciliation.
- Connect errors to service and release versions, configuration revisions, dependency behavior, and affected workflows.

### 4.15 SLIs, SLOs, and error budgets

- Define service-level indicators, service-level objectives, measurement windows, error budgets, burn rates, and their distinction from contractual SLAs.
- Specify the measured population, good outcome, denominator, threshold, observation point, exclusions, owner, and missing-data behavior.
- Include request availability and latency, truthful booking outcomes, notification timeliness, and completed-work recognition freshness.
- Define deadline-based workflow cohorts that include overdue unfinished work; measuring only completed work hides stalled operations.
- Handle expected business rejection, low traffic, zero traffic, and telemetry gaps explicitly; do not exclude every `4xx` automatically.
- Treat numerical targets as illustrative proposals requiring business agreement and capacity evidence.

### 4.16 Dashboards and alerts

- Organize dashboards around customer journeys, service behavior, dependencies, events, infrastructure, and telemetry-pipeline health.
- Define alert severity, owner, runbook, evidence links, escalation, deduplication, inhibition, and recovery criteria.
- Introduce multiwindow burn-rate alerts and backlog-age alerts tied to business deadlines and recovery headroom.
- Separate an immediate page from a ticket or informational signal; monitor missing data and broken alert delivery.

### 4.17 Audit versus observability

- Distinguish operational diagnostics, security detection, integration events, durable recovery state, and authoritative Audit Trails.
- Compare purpose, attribution, completeness, integrity, retention, sampling, access, and delivery requirements.
- Retain Chapter 51's action-specific audit guarantees even when ordinary diagnostic exporters use bounded best-effort delivery.
- Explain why a trace, a dashboard count, an inbox, or a structured log line cannot automatically satisfy another record's obligations.

### 4.18 Sensitive data and telemetry governance

- Apply data classification, collection allowlists, sanitization, pseudonymization where justified, access control, encryption, and retention limits.
- Protect corporate account and fleet information in searches, dashboard drill-downs, exports, support tools, and AI context.
- Exclude credentials, bearer tokens, raw payment details, vehicle registrations, recipient details, and unnecessary payloads from diagnostic collection.
- Prefer minimization before emission; use Collector processing as an additional control with tests for leakage.
- Treat trace context, baggage, exception text, and provider responses as possible disclosure paths and untrusted input.

### 4.19 Sampling, retention, cost, and evidence quality

- Compare head and tail sampling, their collection and routing requirements, and their limits under resource pressure.
- Explain that tail sampling cannot recover spans discarded before collection and that metrics or durable outcome records need separate completeness policies.
- Budget metric cardinality, log volume, trace volume, retention, ingestion, egress, and query cost.
- Document loss, delayed export, clock skew, aggregation boundaries, and gaps rather than interpreting absence as proof of success.

## 5. Architecture Discussion

### 5.1 Define operational questions and accountable ownership

| Service | Primary operational evidence | Accountable response |
| --- | --- | --- |
| Booking | Truthful outcomes, confirmation latency, pending age, unresolved reservations, outbox age | Investigate stalled commitments and coordinate recovery. |
| Corporate Account | Eligibility latency, authority failures, membership or agreement freshness | Resolve authoritative account decisions and availability. |
| Fleet | Batch-validation latency, eligibility outcomes, projection freshness where used | Resolve vehicle and fleet eligibility problems. |
| Station Operations | Reservation outcomes, unresolved commitments, fulfillment and completion publication | Reconcile capacity and operational completion. |
| Billing | Unrecognized completed work, invoice-processing age, reconciliation differences | Restore correct billable-work recognition and invoicing. |
| Payment | Provider latency, uncertain attempts, settlement and reconciliation age | Establish the actual financial outcome before retry. |
| Notification | Intent age, dispatch attempts, provider acceptance and delivery evidence | Restore communication and resolve uncertain sends. |

- Add named platform ownership for broker, cluster, database platform, Collector, backend, and alert delivery.
- Record ownership and escalation in the service catalog; do not assume seven logical services require seven teams.

### 5.2 Place instrumentation at Clean Architecture boundaries

- Establish request context at API and messaging entry points; record application operation outcomes and adapter-level dependency behavior.
- Keep telemetry SDKs, backend clients, and logging providers out of the Domain layer.
- Derive business measurements from established outcomes without allowing telemetry availability to determine business correctness.
- Define consistent names, units, error categories, resource attributes, and versioning responsibilities across services.

### 5.3 Trace immediate booking decisions

- Follow authorized ingress, account and fleet decisions, durable capacity operation identity, reservation, and Booking's local commit.
- Show concurrent eligibility checks only where approved dependencies permit them; retain batched Fleet validation.
- Mark confirmation only after the required authority and capacity commitments exist.
- Distinguish the actual runnable create/read trace from the proposed confirmation trace and identify each lab adapter.

### 5.4 Connect asynchronous consequences and later business milestones

- Connect the Booking transaction, outbox relay, broker, Notification inbox and delivery intent, dispatcher, and provider evidence.
- Trace later Station Operations completion separately; link Billing recognition, consolidated invoicing, and authorized Payment processing through durable references.
- Preserve the rule that Notification delay does not undo confirmation and that Payment is not an invented confirmation prerequisite.
- Retain workflow and event evidence when tracing is unavailable, sampled out, or expired.

### 5.5 Define a proposed SLI catalog

| Journey or boundary | Measurement definition to specify | Important qualification |
| --- | --- | --- |
| API request | Good eligible responses within the defined latency threshold divided by eligible requests | Define valid rejections, timeouts, and the ingress observation point. |
| Booking operation | Unique eligible operations reaching a truthful contract-defined outcome by the deadline | Include overdue unresolved operations; do not count retries as new bookings. |
| Confirmation communication | Required notification intents reaching the agreed evidence milestone by the deadline | Provider acceptance and customer receipt are different milestones. |
| Completed-work recognition | Qualifying completion facts recognized by Billing within the agreed delay | Include due but unprocessed work and distinguish recognition from invoice issuance. |
| Payment reconciliation | Uncertain financial operations resolved within their approved deadline | Settlement success and reconciliation timeliness answer different questions. |

- Specify sources, units, cohorts, windows, exclusions, missing-data policy, and ownership before selecting numerical targets.
- Use component indicators for diagnosis and end-to-end indicators for customer commitments; do not infer the latter by averaging component availability.

### 5.6 Design the telemetry collection architecture

- Compare direct export, per-node collection, and a shared Collector gateway against scale, isolation, configuration, and operating cost.
- Define receivers, processing, bounded queues, retry behavior, memory limits, batching, optional persistent buffering, and exporters.
- Compare managed and self-operated storage and analysis backends as alternatives; retain vendor-neutral instrumentation and avoid silently selecting a new production stack.
- Authenticate collection and query access, restrict destinations, isolate environments, and define tenant visibility.
- Monitor accepted, exported, delayed, rejected, and dropped telemetry, queue saturation, backend ingestion, and alert delivery.
- Specify ordinary telemetry failure behavior separately from required audit delivery; avoid making a Collector outage cause routine booking failure or a restart storm.

### 5.7 Observe services inside Kubernetes

- Correlate service and release identity with namespace, workload, pod, node, and container evidence at appropriate cardinality.
- Cover scheduling failures, pending pods, restarts, `OOMKilled`, CPU throttling, resource limits, node pressure, network errors, and rollout changes.
- Distinguish application metrics, Kubernetes object state, node metrics, control-plane signals, and ephemeral Kubernetes events.
- Describe JSON container output with log collection and OTLP export without duplicate ingestion; account for short-lived workloads and termination flush limits.
- Map startup, readiness, and liveness probes to service behavior; a broker outage can leave Booking writable while a durable outbox has safe headroom.
- Give consumers explicit progress and backlog indicators; pod readiness alone does not prove message-processing progress.
- Keep telemetry collection permissions least-privileged and distinguish Metrics Server from durable observability storage.

### 5.8 Build dashboards for decisions

- Fleet-booking overview: request outcomes, pending age, confirmation timing, capacity uncertainty, and communication freshness.
- Service view: request rate, latency distribution, errors, saturation, dependencies, and release annotations.
- Event view: producer outbox age, consumer lag, unresolved failed work, duplicates, dispatch age, and reconciliation backlog.
- Infrastructure and telemetry views: database pressure, broker health, cluster resources, ingestion gaps, and export loss.
- Provide authorized links to traces, logs, durable operation status, service ownership, and runbooks; show data freshness and gaps.

### 5.9 Route actionable alerts

- Define examples for fast SLO burn, old unresolved reservations, outbox age approaching recovery limits, stalled consumers, and uncertain financial operations.
- Route technical symptoms to the owner who can act while correlating them under a shared customer-impact incident.
- Tune notification grouping, suppression during known maintenance, escalation, and recovery windows without hiding unresolved work.
- Exercise alert delivery and silence expiry; use an independent check for monitoring-pipeline failure where appropriate.

### 5.10 Investigate Northstar's failure with bounded hypotheses

- Establish affected time windows, accounts, operations, and customer commitments using authorized queries.
- Inspect the journey indicator, select an operation, follow available traces, and compare structured logs with durable producer, consumer, and provider records.
- Check deployment changes, resource pressure, dependency latency, retry amplification, stale projections, and telemetry gaps.
- Separate triggering failure, contributing conditions, customer impact, and unproven hypotheses.
- Recover through the owning service's controlled reconciliation or replay procedure; preserve evidence and avoid blind retries of uncertain sends or charges.
- Verify workflow recovery, backlog drainage, customer outcomes, and sustained indicator recovery before closing the incident.

### 5.11 Design secure access to diagnostic evidence

- Apply Chapter 51's account and fleet isolation to support searches, dashboards, operational APIs, and exported evidence.
- Separate platform diagnostics from access to protected business records; avoid cross-service database joins as a troubleshooting shortcut.
- Preserve required Audit Trails for replay, sensitive queries, overrides, and remediation while minimizing data copied into incident records.
- Define retention, legal or contractual preservation decisions, and deletion ownership without inventing compliance periods.

### 5.12 Evolve and validate the observability contract

- Version dashboards, alert rules, instrumentation configuration, and runbooks alongside the owning changes.
- Validate propagation, outcome classification, metric units, sensitive-field exclusion, bounded labels, and expected alert behavior.
- Exercise dependency failure, exporter outage, telemetry overload, and recovery with explicit performance and loss budgets.
- Review observability when API contracts, event versions, service boundaries, or deployment topology change.

## 6. Professional Diagrams

- Specify the following six figures for the manuscript, each with a title, caption, description, legend where needed, and discussion references.
- Use Mermaid sources and publication exports following repository conventions; separate business flow from telemetry flow.
- Distinguish implemented components from proposed extensions and use linked panels when a sequence would exceed five participants.

### Figure 52-1 — Observability Architecture

- **Type:** Layered component and deployment diagram.
- **Content:** Instrumented gateway and services, structured log collection, infrastructure signals, OpenTelemetry Collectors, storage and query backends, dashboards, alerts, and operators.
- **Failure paths:** Collector saturation, dropped telemetry, backend unavailability, and independent monitoring of the collection path.
- **Caption focus:** Common collection and analysis connect evidence while service owners retain responsibility for behavior and recovery.
- **References:** Sections 4.7, 4.18-4.19, 5.2, and 5.6-5.7.

### Figure 52-2 — Distributed Trace Flow

- **Type:** HTTP sequence and asynchronous span-relationship panels.
- **Content:** Incoming trace context, server and client spans, database operations, producer context, consumer processing, and correlated logs.
- **Notation:** Solid relationships for parent-child execution, dashed relationships for span links, and a separate label for durable correlation.
- **Caption focus:** Diagnostic relationships cross service boundaries without making trace identity a business identity or authorization credential.
- **References:** Sections 4.4-4.7 and 5.3-5.4.

### Figure 52-3 — Corporate Fleet Booking Trace

- **Type:** Linked prerequisite, commitment, and consequence panels.
- **Content:** Booking with Corporate Account and Fleet decisions; Station Operations capacity commitment and Booking transaction; outbox publication and Notification dispatch.
- **Later-work panel:** Qualifying service completion, Billing recognition and invoicing, and Payment when authorized, connected through durable references.
- **Failure paths:** Lost capacity response versus delayed notification, with the affected commitment and recovery owner labeled.
- **Caption focus:** Request success, confirmed commitment, communication, completed work, invoicing, and settlement are distinct milestones.
- **References:** Sections 1, 5.3-5.5, and 5.10.

### Figure 52-4 — Logs, Metrics, and Traces Relationship

- **Type:** Investigation flowchart with branches for available evidence.
- **Content:** Metric anomaly, representative trace or exemplar, correlated structured logs, release and dependency evidence, and durable operation lookup.
- **Alternative path:** Investigation through authorized operation references when a trace is missing or expired.
- **Caption focus:** Aggregate signals reveal patterns; execution records explain examples; durable state establishes commitments and unresolved work.
- **References:** Sections 4.2-4.5, 4.19, and 5.10.

### Figure 52-5 — Event Consumer Observability

- **Type:** Durable handoff and recovery flowchart.
- **Content:** Producer transaction and outbox, relay, broker, consumer inbox and local effect, delivery intent, dispatcher, and provider outcome.
- **Annotations:** Publication age, lag, processing latency, checkpoint boundary, duplicate suppression, retry age, quarantine age, and reconciliation state.
- **Caption focus:** Broker acceptance, local handling, external dispatch, and confirmed business effect require different evidence.
- **References:** Sections 4.6, 4.12, 5.4, and 5.8-5.10.

### Figure 52-6 — Alert and Investigation Flow

- **Type:** Decision flowchart.
- **Content:** SLI or backlog signal, data-quality check, severity and routing, responder acknowledgment, customer-impact assessment, evidence gathering, recovery, and verification.
- **Branches:** Missing telemetry, unsupported hypothesis, unresolved financial outcome, and recovery that requires controlled replay or Human Approval.
- **Feedback:** Post-incident actions update instrumentation, runbooks, alert rules, and prevention checks through normal governance.
- **Caption focus:** Alerts lead to accountable investigation and verified recovery rather than an unexamined automated restart.
- **References:** Sections 4.15-4.18, 5.9-5.12, and 8.

## 7. Hands-on Example

### 7.1 Establish a bounded observability lab

- Inventory existing Instructions, ADRs, Booking endpoints, telemetry packages, and test infrastructure.
- Define a runnable create/read instrumentation slice first; identify optional confirmation, consumer, and Kubernetes extension profiles.
- Use synthetic Northstar records, deterministic dependency adapters, a controlled clock, and an isolated telemetry destination.
- Record proposed signal names, fields, propagation rules, sampling behavior, and acceptance criteria before implementation.

### 7.2 Instrument the actual Booking boundary

- Plan ASP.NET Core and outbound HTTP tracing, structured request context, a focused application activity, and route-level metrics.
- Record pending creation and read outcomes truthfully; retain application behavior and dependency direction.
- Inspect emitted spans and logs for useful service identity, outcome categories, and trace correlation without sensitive request bodies.

### 7.3 Add a proposed distributed workflow fixture

- Model Corporate Account, Fleet, and Station Operations with explicit test adapters before claiming an integrated deployment.
- Add a separate proposed confirmation/outbox/Notification fixture using Chapter 50's event semantics.
- Demonstrate successful processing, redelivery, delayed processing, and a provider timeout with stable business IDs and distinct attempt evidence.
- Declare which profiles use a real broker and database; adapter-only evidence does not prove transport or production storage behavior.

### 7.4 Define metrics, one journey SLI, and a focused dashboard

- Choose bounded service indicators and a deadline-based asynchronous outcome indicator.
- Define good events, eligible operations, observation points, deadline, denominator, and behavior for unfinished work or missing data.
- Specify dashboard panels and trace/log/status links that answer the opening incident's questions.
- Use illustrative lab thresholds rather than asserting approved production SLOs.

### 7.5 Inject failures and exercise alerting

- Slow Fleet validation, lose a capacity response, pause publication or consumption, and fail Notification dispatch as separate controlled cases.
- Stop or saturate the telemetry path and confirm loss indicators and bounded application impact.
- Verify that expected business rejection differs from technical failure and that idempotent replay does not become an additional business success.
- Exercise a backlog-age or SLO alert through a test destination, including routing, evidence links, acknowledgment, and recovery.

### 7.6 Investigate and prepare an evidence package

- Reconstruct one affected operation using traces, logs, aggregate metrics, and owned durable records.
- Document the hypothesis, supporting evidence, gaps, customer impact, recovery decision, and verification outcome.
- Repeat the lookup with trace sampling disabled or the trace absent; recoverability still depends on stable operation state.
- Inspect prompts and exported artifacts for sensitive data and record the scope of the completed validation.

### 7.7 Acceptance criteria

- The tested HTTP boundaries propagate valid diagnostic context and provide useful local evidence when context is missing or invalid.
- Durable publication and consumer processing preserve approved correlation without treating trace IDs as deduplication keys.
- Request attempts, unique booking outcomes, duplicates, pending work, and external delivery milestones remain distinguishable.
- Metric labels are bounded; inspected logs, spans, baggage, and exports contain no prohibited sensitive fields.
- Alerts identify an accountable owner and a demonstrable action; missing data is visible.
- Health and readiness behavior match the tested dependency contract, and telemetry failure remains bounded.
- Recovery is justified by durable evidence and the owning service's procedure, with no duplicate uncontrolled external effect.
- Findings identify fixture, local integration, and proposed deployment coverage separately.

## 8. Claude Example

### 8.1 Instrument a narrowly scoped operation

- Specify the approved architecture, baseline behavior, permitted files, telemetry contract, synthetic inputs, and validation commands.
- Ask Claude Code to implement the bounded Booking observability slice and explain the evidence it will make available.
- Require the AI Agent to flag missing policy or architecture decisions rather than inventing SLO targets, broker guarantees, or new services.

### 8.2 Assist a troubleshooting investigation

- Provide a sanitized incident bundle containing a time window, service versions, representative telemetry, durable status, and known gaps.
- Request a timestamped evidence map, ranked hypotheses, disconfirming checks, and a bounded investigation sequence.
- Require references to actual supplied records and separate observed facts from inferred causes.
- Treat log messages, payloads, retrieved documentation, and tool output as untrusted data that cannot authorize new actions.

### 8.3 Preserve the established Harness responsibilities

- Lead Agent defines scope and coordinates evidence; Developer Agent implements approved changes.
- Reviewer Agent checks architectural meaning and data exposure; Validator Agent executes applicable deterministic checks.
- Evaluator Agent assesses evidence against the objective without substituting evaluation for a protected gate.
- Keep production queries constrained and attributable; changes, replay, payment actions, and sensitive recovery follow their existing authorization and Human Approval rules.
- Promote validated lessons through governed Instructions, Skills, runbooks, or Knowledge Base updates rather than automatic policy changes.

## 9. GitHub Copilot Comparison

- Apply the same instrumentation contract and incident evidence to a focused repository task.
- Discuss only material differences in repository context, editing, tool access, execution, and review workflow verified during manuscript preparation.
- Preserve identical requirements for synthetic data, bounded queries, evidence references, independent validation, and authorized recovery.
- Avoid duplicate implementation listings that merely change the AI platform name.

## 10. Codex Comparison

- Apply the same bounded change or sanitized investigation using the available repository and execution environment.
- Explain the effect of workspace access, network restrictions, available tooling, and production-system isolation on the evidence Codex can obtain.
- Keep implementation checks separate from unperformed production verification; do not claim access to telemetry systems that were not connected.
- Verify platform-specific details against current primary documentation and compare workflows only where they materially differ.

## 11. Best Practices

- Begin with customer promises and the questions responders need to answer.
- Instrument owning boundaries and use consistent semantic conventions, units, and error classifications.
- Preserve business operation and event identity independently of sampled traces.
- Combine bounded aggregate signals with authorized diagnostic drill-down and durable recovery state.
- Measure unfinished work, uncertainty, and eventual-consistency delay as first-class operational conditions.
- Validate important outcomes and failure paths, including collector loss, before relying on dashboards or alerts.
- Minimize sensitive data before emission and budget cardinality, volume, retention, and query cost.
- Give every dashboard, alert, runbook, and telemetry component an accountable owner.

## 12. Anti-patterns

- Collecting large volumes of unstructured logs without questions, context, or retention rules.
- Treating `200`, `201`, broker acceptance, or healthy pods as proof of complete business success.
- Using correlation or trace IDs as authorization, business identity, or consumer deduplication evidence.
- Recording tokens, payment details, full event bodies, vehicle registrations, or recipient data in telemetry.
- Putting unique booking, event, customer, or account identifiers into metric labels.
- Averaging service percentiles or measuring only completed operations while stalled work disappears.
- Treating offset lag as elapsed delay or a checkpoint as proof of external delivery.
- Logging the same exception at every layer or creating duplicate spans through overlapping instrumentation.
- Making every dependency failure trigger liveness restarts or allowing unbounded telemetry buffering.
- Treating sampled diagnostics as an authoritative Audit Trail, invoice ledger, or exact commercial count.
- Replaying messages or retrying uncertain payments solely because a dashboard shows an error.
- Allowing an AI-generated explanation or recommended remediation to bypass evidence, access controls, or approval.

> **Common Mistake**  
> Frame a case in which Notification is delayed while Booking is correctly confirmed; changing booking state to make a dashboard green corrupts the business meaning instead of repairing the failed consequence.

## 13. Architect's Notes

### 13.1 Evidence follows ownership

- Every business commitment, observation, and recovery action has an accountable owner.
- A central telemetry backend improves access to evidence without acquiring authority over every service's state.

### 13.2 Business time outlives execution time

- Booking, fulfillment, invoicing, and settlement can span many independent executions.
- Model durable relationships and deadlines rather than requiring one enormous trace to explain the lifecycle.

### 13.3 Availability has several boundaries

- Process health, admission readiness, dependency availability, asynchronous freshness, and customer outcomes can diverge.
- State which promise each indicator supports before deciding what it should alert or restart.

### 13.4 Observability has its own failure modes

- Sampling, overload, clock skew, ingestion delay, access restrictions, and backend outages limit the evidence available.
- Design independent recovery records and visible telemetry gaps.

> **Architect's Note**  
> Use the simplest collection and analysis architecture that makes customer commitments, ownership, uncertainty, and recovery understandable at the required scale.

## 14. Enterprise Tips

- Maintain a service catalog containing owner, SLOs, dependencies, telemetry links, runbooks, and escalation routes.
- Make observability part of API and event readiness reviews, release checklists, and deployment verification.
- Record release and configuration changes on dashboards to support comparison without assuming every incident was caused by the latest deployment.
- Standardize bounded queries, access reviews, incident exports, and synthetic test identities.
- Rehearse a slow dependency, stalled consumer, lost capacity response, uncertain provider result, and failed Collector.
- Review alert noise, recovery time, evidence gaps, telemetry cost, and overdue remediation after incidents.
- Retain protected Audit Trails independently of debug-log retention and sampling policy.

> **Enterprise Tip**  
> Require each production alert to identify the customer risk, accountable responder, first useful query, and safe recovery procedure.

## 15. Decision Points

| Decision | Alternatives and trade-off to examine |
| --- | --- |
| Collection topology | Direct export, node collection, and Collector gateways balance simplicity, isolation, cost, and operational ownership. |
| Analysis backend | Managed and self-operated platforms differ in operating effort, access controls, residency, query capability, and cost. |
| Instrumentation | Automatic and explicit instrumentation differ in coverage, semantic precision, control, and duplication risk. |
| Asynchronous trace relationships | Parentage and span links depend on messaging conventions, batch behavior, retention, and query support. |
| Sampling and retention | Balance investigation needs, collection capacity, privacy, and cost without making recovery depend on trace retention. |
| Business measurements | Approximate operational counters versus reconciled durable outcomes depend on the decision being supported. |
| SLO population and target | Agree on promises, exclusions, units, deadlines, windows, missing data, and numerical targets with accountable owners. |
| Health and admission | Restart, stop admitting work, or continue degraded operation according to dependency necessity and safe buffer headroom. |
| Alert action | Page, ticket, or record information according to customer impact, burn rate, recovery urgency, and responder authority. |
| AI troubleshooting access | Choose sanitized bundles or constrained read access; separately authorize any state-changing remediation. |

> **Decision Point**  
> For an unavailable broker, decide how long Booking can safely accept work into its durable outbox, which limits end degraded operation, and which evidence must trigger intervention.

## 16. Exercises

### 16.1 Build an observability requirements map

- Map each Northstar milestone to its owner, operational question, signal, stable lookup reference, failure condition, and recovery procedure.

### 16.2 Design the identity and logging contract

- Define structured fields and context propagation across HTTP, outbox publication, consumer processing, and dispatch.
- Identify sensitive fields and unsuitable metric labels; explain how investigation works without a retained trace.

### 16.3 Trace and investigate a distributed failure

- Use synthetic evidence to distinguish slow Fleet validation, a lost capacity response, and delayed Notification delivery.
- Produce a supported hypothesis, impact assessment, disconfirming check, and safe recovery decision for each case.

### 16.4 Define measurable SLOs

- Specify one request indicator and one deadline-based asynchronous outcome indicator.
- Include unfinished operations, expected rejections, duplicate attempts, zero traffic, missing data, and illustrative targets.

### 16.5 Observe an event consumer

- Design backlog, age, retry, duplicate, quarantine, and dispatch indicators for a proposed Notification consumer.
- Demonstrate how checkpoint progress and provider acceptance differ from the customer's final outcome.

### 16.6 Review Kubernetes and telemetry failure behavior

- Separate startup, liveness, readiness, consumer progress, and business health.
- Examine a database outage, broker outage, pod restart, and failed Collector; document the intended admission and alert behavior.

### 16.7 Review telemetry security and cost

- Find sensitive attributes, unbounded labels, duplicate exports, excessive retention, and access gaps in a proposed design.
- Produce a prioritized correction plan with evidence of the affected data and query paths.

### 16.8 Evaluate an AI-assisted investigation

- Provide a sanitized bundle containing a missing trace, a misleading log message, and an uncertain payment result.
- Assess whether the AI Agent distinguishes facts from hypotheses, resists embedded instructions, and preserves independent recovery authority.

## 17. Interview Questions

1. How do monitoring, observability, and diagnosis differ in a distributed booking workflow?
2. Which questions are best answered by logs, metrics, traces, and durable business state?
3. How do correlation IDs, trace IDs, span IDs, event IDs, causation IDs, and operation IDs differ?
4. How would you trace work through an outbox, repeated delivery, consumer processing, and external dispatch?
5. When are span links useful, and why should a consolidated invoice not have one arbitrary booking as its sole cause?
6. What does OpenTelemetry provide, and which storage, querying, dashboard, and alert responsibilities remain elsewhere?
7. Where should instrumentation sit within Clean Architecture?
8. How do you prevent sensitive telemetry and uncontrolled metric cardinality?
9. How do latency histograms support aggregation, and why is averaging instance percentiles misleading?
10. How do service indicators differ from business outcome and reconciliation indicators?
11. Why can pod readiness, broker acceptance, checkpoint progress, and HTTP success give different pictures of customer impact?
12. How do startup, liveness, readiness, and worker progress checks differ?
13. How would you define an asynchronous SLI without hiding work that never completed?
14. How should expected business rejections, retries, zero traffic, and missing telemetry affect an SLO calculation?
15. Why should event monitoring include both backlog age and transport lag?
16. How would you diagnose database contention, retry amplification, or a stalled consumer across service boundaries?
17. What trade-offs distinguish head sampling, tail sampling, and retaining all diagnostic data?
18. How should the system behave when its telemetry pipeline is unavailable or overloaded?
19. What distinguishes operational diagnostics from authoritative Audit Trails and financial evidence?
20. What makes an alert actionable, and when should it page a responder rather than create a ticket?
21. How can AI Agents assist troubleshooting without inventing causes or performing unauthorized remediation?
22. What evidence demonstrates recovery after a delayed notification or uncertain payment operation?

## 18. Chapter Summary

- Return to Northstar's incident and reconstruct what succeeded, what was delayed, why the evidence supports the diagnosis, and how users were affected.
- Reinforce complementary logs, metrics, traces, and durable workflow records with distinct identifiers and ownership.
- Connect service, dependency, event, database, Kubernetes, and telemetry-pipeline evidence to customer commitments.
- Summarize explicit SLI definitions, agreed SLOs, meaningful dashboards, actionable alerts, and verified recovery.
- Reaffirm sensitive-data minimization, separate audit guarantees, controlled AI assistance, and independent authorization for remediation.

## 19. Further Reading

### 19.1 OpenTelemetry and distributed context

- [OpenTelemetry: Context propagation](https://opentelemetry.io/docs/concepts/context-propagation/): propagation, correlation, and trust-boundary considerations.
- [OpenTelemetry: Semantic conventions for messaging spans](https://opentelemetry.io/docs/specs/semconv/messaging/messaging-spans/): producer, consumer, batch, and link relationships; pin the implementation's conventions.
- [OpenTelemetry Collector](https://opentelemetry.io/docs/collector/): collection, processing, export, and deployment responsibilities.
- [W3C Trace Context](https://www.w3.org/TR/trace-context/): interoperable trace-context representation and propagation.

### 19.2 .NET instrumentation and health

- [Microsoft: .NET observability with OpenTelemetry](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel): .NET instrumentation and telemetry integration.
- [Microsoft: Health checks in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-10.0): health endpoints and dependency-specific checks.
- Consult the selected Serilog integration, database driver, and broker instrumentation documentation when preparing verified examples.

### 19.3 Reliability objectives and alerting

- [Google SRE Workbook: Implementing SLOs](https://sre.google/workbook/implementing-slos/): customer-oriented indicators, objectives, windows, and error budgets.
- [Google SRE Workbook: Alerting on SLOs](https://sre.google/workbook/alerting-on-slos/): burn-rate alerting and appropriate response urgency.

### 19.4 Kubernetes, safe logging, and handbook continuity

- [Kubernetes: Observability](https://kubernetes.io/docs/concepts/cluster-administration/observability/): cluster and application signals.
- [Kubernetes: Liveness, readiness, and startup probes](https://kubernetes.io/docs/concepts/workloads/pods/probes/): distinct probe purposes and failure behavior.
- [OWASP Logging Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Logging_Cheat_Sheet.html): event selection, sensitive-data exclusion, access, and logging failure modes.
- Revisit Chapters 32 and 46-51 for Audit Trails, business meaning, owned commitments, reliable event processing, security, and incident-response boundaries.
- Verify AI-platform capabilities and deployment-specific integrations against current primary documentation when writing the manuscript.

Chapter 52 outline is complete.
