# Chapter 47 — Solution Architecture

**Part VII — Alpha Car Detailing**  
**Target file:** `book/47-solution-architecture.md`

This outline follows the frozen Part VII structure and the handbook’s standard 19-section chapter template. AI development handbook AI development handbook

---

## 1. Story-Driven Opening — From Business Capabilities to a Working Enterprise System

### 1.1 The Corporate Fleet Booking Challenge

- Reintroduce Alpha Car Detailing from Chapter 46 as a nationwide detailing business serving individual customers, corporate fleets, government departments, insurers, and rental organizations.
- Introduce the primary scenario:
  - A corporate customer manages hundreds or thousands of vehicles.
  - An authorized fleet manager requests detailing services for multiple vehicles.
  - Corporate booking limits must be enforced.
  - Vehicle and fleet eligibility must be verified.
  - Station capacity must be checked.
  - Pricing and corporate agreements must be applied.
  - A booking must be created reliably.
  - Billing information must be recorded.
  - Notifications must be issued.
  - Downstream operational systems must receive the booking.
- Show why a simple "booking application" quickly becomes an enterprise architecture problem.

### 1.2 The Architecture Conversation

- Business stakeholders describe capabilities.
- Architects translate those capabilities into system responsibilities.
- Development teams need clear service boundaries.
- Security teams need trust boundaries.
- Operations teams need observability and recovery mechanisms.
- Platform teams need deployment and scalability models.

### 1.3 Technology Is Not the Starting Point

- Contrast:
  - "We use Kubernetes, therefore we need microservices."
  - "The fleet-booking capability has independent ownership, scaling, availability, and integration requirements; therefore a separate service boundary may be justified."
- Establish the chapter principle:

> Solution architecture should reflect business boundaries and quality attributes rather than being driven by technology choices alone.

### 1.4 Chapter 46 to Chapter 47 Transition

- Chapter 46 defines the business domain.
- Chapter 47 translates the domain into an enterprise solution architecture.
- Later chapters will progressively deepen:
  - Clean Architecture
  - Microservices
  - Event-driven architecture
  - Security
  - Observability
  - CI/CD
  - AI-assisted implementation.

---

# 2. Learning Objectives

By the end of this chapter, readers should be able to:

- Translate business capabilities into enterprise system boundaries.
- Identify architecture drivers before choosing technologies.
- define functional and non-functional architecture requirements.
- Model the Alpha Car Detailing system context.
- Identify appropriate service boundaries.
- Explain the responsibilities of each major service.
- Determine service and data ownership.
- Decide when synchronous APIs or asynchronous events are appropriate.
- Design shared contracts without creating excessive coupling.
- Identify appropriate security and trust boundaries.
- Design caching without violating source-of-truth ownership.
- Apply reliability and resiliency patterns.
- Define observability requirements.
- Design container and Kubernetes deployment architecture.
- Relate application architecture to CI/CD.
- Document important architecture decisions and trade-offs.
- Plan architectural evolution rather than attempting to design the final system immediately.
- Use AI Agents to assist architecture work without delegating architecture accountability.

---

# 3. Background — From Domain Model to Solution Architecture

### 3.1 What Solution Architecture Means

- Relationship among:
  - Business architecture
  - Domain architecture
  - Solution architecture
  - Software architecture
  - Infrastructure architecture.
- Solution architecture as the bridge between business capabilities and implementable systems.

### 3.2 Inputs from Chapter 46

- Business capabilities.
- Bounded business responsibilities.
- Corporate accounts.
- Fleet management.
- Booking.
- Station operations.
- Billing.
- Payments.
- Notifications.
- Reporting and operational concerns.

### 3.3 Architecture Drivers

Organize drivers into:

#### Business Drivers

- Nationwide expansion.
- Corporate fleet growth.
- Multi-station operations.
- Faster onboarding of corporate customers.
- Reliable service delivery.
- Regulatory and audit requirements.

#### Technical Drivers

- Independent scalability.
- Integration with external providers.
- Resilience to partial outages.
- Traceability across distributed workflows.
- Controlled deployment risk.
- Maintainability.

#### Organizational Drivers

- Multiple development teams.
- Independent ownership.
- Platform engineering.
- DevOps practices.
- Security governance.
- AI-assisted engineering.

### 3.4 Constraints

- Existing corporate systems.
- Legacy station systems where applicable.
- Azure environment.
- Enterprise identity.
- SQL Server.
- Event infrastructure.
- Kubernetes platform.
- Cost and operational maturity.

---

# 4. Concepts

## 4.1 Architecture Goals

Define explicit goals for Alpha Car Detailing:

- Business alignment.
- Clear ownership.
- Loose coupling.
- High cohesion.
- Independent evolution.
- Reliable booking.
- Secure corporate access.
- Operational visibility.
- Controlled scalability.
- Deployment automation.
- Auditability.
- Extensibility.

## 4.2 Quality Attributes

### Availability

- Booking must remain available during normal infrastructure failures.
- Define availability expectations for customer-facing versus back-office capabilities.

### Reliability

- Prevent lost bookings.
- Prevent duplicate bookings.
- Ensure downstream processing eventually completes.

### Scalability

- Scale booking traffic independently from billing or notification workloads.
- Handle corporate batch/fleet demand.

### Performance

- Fast interactive booking confirmation.
- Minimize synchronous dependency chains.

### Security

- Corporate identity and authorization.
- Service-to-service authentication.
- Data protection.
- Least privilege.

### Maintainability

- Clear boundaries.
- Independent modules.
- Governed contracts.

### Observability

- Trace business transactions across services.
- Measure business and technical outcomes.

### Recoverability

- Retry transient failures.
- Replay event processing where appropriate.
- Restore service and data after incidents.

### Auditability

- Record who performed sensitive corporate actions.
- Preserve relevant business and system events.

## 4.3 Functional Requirements Versus Quality Attributes

- Explain why both influence architecture.
- Show how the same business feature can result in different architecture depending on quality requirements.

---

# 5. Architecture Discussion

## 5.1 System Context

Define actors and external systems.

### Primary Actors

- Individual Customer
- Corporate Fleet Manager
- Station Employee
- Operations Manager
- Finance User
- System Administrator

### External Systems

- Identity Provider
- Payment Gateway
- Email/SMS provider
- Enterprise finance/accounting systems
- Monitoring platform
- Corporate integrations where required

### Diagram — System Context

Show:

```text
Customers / Fleet Managers / Employees
                 |
                 v
        Alpha Car Detailing
        Enterprise Platform
                 |
     +-----------+-----------+
     |           |           |
 Identity    Payments   Notifications
 Provider      Provider       Provider
```

Expand professionally in the chapter diagram.

---

## 5.2 Major Solution Boundaries

Introduce the principal services:

- Booking Service
- Corporate Account Service
- Fleet Service
- Station Operations Service
- Billing Service
- Payment Service
- Notification Service
- API Gateway

Clarify that boundaries originate from responsibility and ownership, not simply tables or CRUD entities.

---

# 5.3 API Gateway

### Responsibilities

- External API entry point.
- Authentication enforcement.
- Routing.
- Rate limiting.
- Request correlation.
- API version routing.
- Basic request policies.

### Responsibilities It Should Not Own

- Business rules.
- Fleet eligibility.
- Booking limits.
- Pricing decisions.
- Corporate authorization rules that belong inside business services.

### Corporate Fleet Scenario

- Fleet manager submits booking request through the gateway.
- Gateway authenticates and routes.
- Domain services remain responsible for business authorization.

---

# 5.4 Booking Service

### Primary Responsibilities

- Create bookings.
- Modify permitted booking details.
- Cancel bookings.
- Manage booking lifecycle.
- Enforce booking-specific invariants.
- Check booking availability.
- Coordinate required validation.

### Data Ownership

Examples:

- Booking
- BookingLine
- BookingStatus
- RequestedService
- ScheduledTime
- Booking metadata

### What Booking Does Not Own

- Corporate customer master data.
- Fleet master data.
- Payment provider state.
- Station operating configuration.

### Corporate Fleet Booking Responsibilities

- Receive booking command.
- Validate relevant references.
- prevent duplicate processing.
- persist booking atomically.
- publish booking-created event.

---

# 5.5 Corporate Account Service

### Responsibilities

- Corporate customer profile.
- Account status.
- Contractual arrangements.
- Booking entitlements.
- Corporate booking limits.
- Billing terms.
- Authorized fleet relationships.

### Example Rules

- Corporate account must be active.
- Booking request must remain within permitted limits.
- Account may have credit or contractual restrictions.

### Data Ownership

- CorporateAccount
- CorporateContract
- AccountLimit
- BillingTerms
- AccountStatus

---

# 5.6 Fleet Service

### Responsibilities

- Fleet definition.
- Vehicle registration.
- Fleet-to-account relationships.
- Vehicle status.
- Fleet eligibility.
- Fleet metadata.

### Corporate Fleet Scenario

- Confirm vehicle belongs to corporate account.
- Verify vehicle can receive requested service.
- Support large fleet operations.

### Avoiding Boundary Leakage

- Booking Service should reference fleet identifiers rather than owning fleet master records.

---

# 5.7 Station Operations Service

### Responsibilities

- Station configuration.
- Operating hours.
- Service availability.
- Capacity.
- Bay/resource scheduling.
- Station workload.
- Operational booking visibility.

### Corporate Booking Participation

- Validate station capability.
- Reserve or acknowledge capacity.
- Receive confirmed booking information.

### Future Evolution

- Workforce scheduling.
- Equipment availability.
- Dynamic capacity optimization.

---

# 5.8 Billing Service

### Responsibilities

- Calculate billable obligations.
- Corporate invoicing.
- Invoice lifecycle.
- Taxes where applicable.
- Discounts and contractual billing adjustments.
- Billing records.

### Separation from Booking

Explain why:

- Booking represents service commitment.
- Billing represents financial obligation.
- They evolve under different rules and ownership.

---

# 5.9 Payment Service

### Responsibilities

- Payment initiation.
- External payment-provider integration.
- Payment status.
- Refunds.
- Provider correlation.
- Payment reconciliation.

### Billing Versus Payment

Clarify:

```text
Billing = what is owed
Payment = how money is collected
```

### Corporate Scenario

- Some corporate accounts may use invoicing rather than immediate payment.
- Payment processing must therefore not be hard-coded into booking creation.

---

# 5.10 Notification Service

### Responsibilities

- Email.
- SMS.
- Push notifications where applicable.
- Notification templates.
- Delivery tracking.
- Retry policies.

### Event-Driven Integration

- Consume business events.
- Avoid making successful booking dependent on email/SMS provider availability.

---

# 5.11 Container/Service Architecture

### Diagram — Container/Service Architecture

Show:

```text
Web / Mobile / Corporate Client
              |
         API Gateway
              |
   +----------+-----------+
   |          |           |
Booking   Corporate     Fleet
Service    Account      Service
             Service
   |
Station Operations
   |
Billing ---- Payment
   |
 Event Infrastructure
   |
Notification / Other Consumers
```

Include:

- External dependencies.
- Databases.
- Redis.
- Event platform.
- Observability platform.

---

# 5.12 Service Dependency Map

### Purpose

- Identify synchronous dependencies.
- Identify event dependencies.
- Detect excessive coupling.

### Diagram — Service Dependency Map

Illustrate dependencies such as:

```text
Booking
 ├── Corporate Account
 ├── Fleet
 └── Station Operations

Billing
 └── Corporate Account

Payment
 └── External Payment Provider

Notification
 └── Event Stream
```

### Architecture Analysis

- Avoid long synchronous call chains.
- Distinguish required decision-time information from eventual downstream processing.

---

# 5.13 Service Communication

## Synchronous Communication

Use when:

- Immediate decision is required.
- Caller requires the result to proceed.

Examples:

- Validate fleet membership.
- Query station capacity.
- Verify corporate account status.

## Asynchronous Communication

Use when:

- Immediate response is unnecessary.
- Eventual consistency is acceptable.
- Multiple consumers need notification.
- Decoupling improves reliability.

Examples:

- BookingCreated.
- BookingCancelled.
- VehicleServiceCompleted.
- InvoiceGenerated.
- PaymentReceived.

---

# 5.14 APIs Versus Events

Create a decision model.

### APIs Represent Requests or Queries

Examples:

```text
POST /bookings
GET /fleets/{fleetId}
GET /corporate-accounts/{accountId}
```

### Events Represent Facts

Examples:

```text
BookingCreated
BookingCancelled
InvoiceGenerated
PaymentReceived
VehicleServiceCompleted
```

### Critical Distinction

Avoid using events as disguised remote procedure calls.

### Decision Questions

- Does the caller need an immediate result?
- Is the action a command or a historical fact?
- Must the producer know the consumer?
- Can processing occur later?
- Is eventual consistency acceptable?

---

# 5.15 Corporate Fleet Booking End-to-End Sequence

### Diagram — Corporate Fleet Booking Sequence

Participants:

```text
Fleet Manager
API Gateway
Booking Service
Corporate Account Service
Fleet Service
Station Operations Service
Booking Database
Event Infrastructure
Billing Service
Notification Service
```

Sequence:

1. Fleet manager submits booking.
2. Gateway authenticates user.
3. Booking Service receives command.
4. Corporate account eligibility is verified.
5. Fleet/vehicle ownership is verified.
6. Station capacity is validated.
7. Booking rules execute.
8. Booking is persisted.
9. Outbox event is persisted in the same transaction.
10. Booking confirmation returns.
11. Outbox publishes `BookingCreated`.
12. Station Operations receives the event.
13. Billing receives the event where appropriate.
14. Notification Service sends confirmation.
15. Observability correlates the full transaction.

Highlight the distinction between:

- Critical synchronous decision path.
- Asynchronous post-booking work.

---

# 5.16 Event Flow

### Diagram — Event Flow

Example:

```text
Booking Service
      |
 BookingCreated
      |
      v
Event Infrastructure
   |       |       |
   v       v       v
Station  Billing  Notification
Ops
```

Extend with:

- `BookingCancelled`
- `ServiceCompleted`
- `InvoiceGenerated`
- `PaymentReceived`

### Event Design Principles

- Stable event semantics.
- Versioned contracts.
- Idempotent consumers.
- Correlation identifiers.
- No distributed transactions.
- Explicit ownership.

---

# 5.17 Data Ownership

### Database-per-Service Principle

Explain logical ownership even when physical infrastructure is shared initially.

Example:

| Service | Authoritative Data |
| --- | --- |
| Booking | Bookings and booking lifecycle |
| Corporate Account | Corporate profiles, contracts, limits |
| Fleet | Fleets and vehicles |
| Station Operations | Station capacity and operational state |
| Billing | Invoices and billing records |
| Payment | Payment transactions |
| Notification | Notification delivery records |

### Rules

- One service is authoritative for each business concept.
- Other services do not directly update its tables.
- Cross-service joins should not become the integration mechanism.
- Data replication must have explicit purpose.

### Diagram — Data Ownership

Show each service with its owned store and permitted interaction through contracts/events.

---

# 5.18 Shared Contracts

### What May Be Shared

- API schemas.
- Event schemas.
- Standard correlation metadata.
- Common technical primitives.

### What Should Usually Not Be Shared

- Domain entities.
- ORM models.
- database models.
- Internal service business logic.

### Contract Governance

- Versioning.
- Compatibility.
- Consumer impact.
- Schema evolution.
- Ownership.

### Shared Kernel Warning

Discuss how a "common" library can silently recreate a distributed monolith.

---

# 5.19 Security Boundaries

### Identity Boundary

- Users authenticate through enterprise/customer identity.

### API Boundary

- Gateway protects internet-facing endpoints.

### Service Boundary

- Service-to-service identities.
- Least privilege.
- Workload identity.

### Data Boundary

- Each service receives minimum required data access.

### Secret Boundary

- Secrets managed outside application configuration files.

### External Integration Boundary

- Payment and notification integrations treated as separate trust zones.

### Diagram — Security Boundaries

Show zones such as:

```text
Internet
   |
[Edge / Gateway]
   |
[Application Trust Zone]
   |
[Service/Data Trust Zone]
   |
[External Providers]
```

Include:

- Authentication
- Authorization
- encryption
- service identity
- secret management
- audit logging.

---

# 5.20 Caching Architecture

### Appropriate Uses

- Read-heavy reference information.
- Station configuration.
- Selected corporate entitlement data.
- Frequently accessed non-transactional information.

### Redis

- Distributed caching.
- Appropriate shared ephemeral state.
- Carefully controlled invalidation.

### Do Not Cache Blindly

- Critical account status without freshness strategy.
- Booking capacity where stale data could cause double-booking.
- Security-sensitive authorization decisions indefinitely.

### Source-of-Truth Principle

Cache never becomes the authoritative owner of business data.

---

# 5.21 Reliability and Resiliency

Cover:

### Timeouts

- Bound synchronous dependency wait time.

### Retry

- Retry transient failures only.
- Use bounded retry strategies.

### Circuit Breakers

- Prevent cascading failure.

### Bulkheads

- Isolate expensive or unreliable dependencies.

### Idempotency

- Prevent duplicate booking commands and event processing.

### Transactional Outbox

- Persist state change and event intent atomically.

### Dead-Letter Handling

- Capture repeatedly failing asynchronous work.

### Graceful Degradation

Examples:

- Booking should not fail because SMS is unavailable.

### Dependency Failure Scenarios

Analyze:

- Corporate Account Service unavailable.
- Fleet Service slow.
- Notification provider unavailable.
- Event platform temporarily unavailable.
- Payment provider failure.

---

# 5.22 Observability Architecture

### Logs

- Structured logging.
- Business identifiers.
- Correlation IDs.

### Metrics

Examples:

- bookings created.
- failed booking attempts.
- station-capacity failures.
- booking latency.
- event-processing lag.
- payment failures.

### Distributed Traces

Trace:

```text
Gateway
  → Booking
  → Corporate Account
  → Fleet
  → Station Operations
```

### OpenTelemetry

- Standard instrumentation approach.
- vendor-neutral telemetry.
- Correlation across APIs and events.

### Business Observability

Distinguish:

- System health.
- Business process health.

---

# 5.23 Infrastructure Architecture

Cover infrastructure building blocks:

- Azure subscription/resource organization.
- Virtual networking.
- Kubernetes cluster.
- Container registry.
- Managed SQL databases.
- Redis.
- Event infrastructure.
- Secret management.
- Observability platform.
- API ingress/gateway.
- external provider connectivity.

### Environment Strategy

- Development.
- Test.
- Staging.
- Production.

---

# 5.24 Containers

### Why Containers

- Deployment consistency.
- Runtime isolation.
- portability.
- immutable releases.

### Container Boundaries

- One deployable service per container workload.
- Avoid recreating monolithic process coupling inside containers.

### Image Concerns

- Minimal base images.
- vulnerability scanning.
- immutable tags/digests.
- non-root execution where practical.

---

# 5.25 Kubernetes

### Workload Model

For each service:

- Deployment.
- Service.
- ConfigMap where appropriate.
- Secret reference.
- autoscaling.
- health probes.
- resource requests/limits.

### Platform Responsibilities

- Scheduling.
- service discovery.
- scaling.
- rolling deployment.
- failure recovery.

### Kubernetes Is Not the Architecture

Reinforce:

- Business boundaries are determined before Kubernetes resources.
- Kubernetes deploys architecture; it does not define business architecture.

---

# 5.26 Deployment Architecture

### Diagram — Deployment Architecture

Show:

```text
Internet
   |
API Gateway / Ingress
   |
Azure Kubernetes Service
   |
+--------------------------------+
| Booking Pods                   |
| Corporate Account Pods         |
| Fleet Pods                     |
| Station Operations Pods        |
| Billing Pods                   |
| Payment Pods                   |
| Notification Pods              |
+--------------------------------+
     |        |         |
 SQL Stores  Redis   Event Platform
     |
Telemetry / Monitoring
```

Include external:

- Identity.
- payment provider.
- notification provider.
- container registry.
- CI/CD platform.

---

# 5.27 CI/CD Architecture

### Source Control

- Feature branches or agreed branching strategy.
- pull requests.
- protected main branch.

### Pipeline Stages

1. Restore/build.
2. Static analysis.
3. Unit tests.
4. Integration tests.
5. Security scanning.
6. Container build.
7. Image scanning.
8. Artifact publication.
9. Deploy to lower environment.
10. Automated validation.
11. Approval where required.
12. Production deployment.
13. Post-deployment verification.

### Architecture Alignment

- Independently deployable service boundaries should be reflected in pipelines.
- Shared pipeline templates can standardize governance without coupling releases.

---

# 5.28 Architecture Decisions and Trade-Offs

Potential ADR topics:

### ADR — Modular Monolith Versus Microservices

- Operational complexity versus independent evolution.

### ADR — Synchronous Validation During Booking

- Consistency versus availability.

### ADR — Events for Downstream Processing

- Decoupling versus eventual consistency.

### ADR — Database-per-Service

- Ownership versus reporting/query complexity.

### ADR — API Gateway

- Central governance versus additional infrastructure layer.

### ADR — Redis

- Performance versus cache consistency.

### ADR — Kubernetes

- operational power versus platform complexity.

### ADR — Event Infrastructure

- asynchronous resilience versus operational complexity.

---

# 5.29 Architecture Evolution Strategy

### Stage 1 — Establish Boundaries

- Clearly defined business modules.
- Explicit ownership.
- potentially deploy together initially.

### Stage 2 — Separate High-Value Boundaries

Candidates:

- Booking.
- Notification.
- Payment.
- Corporate/fleet capabilities.

### Stage 3 — Introduce Asynchronous Integration

- Business events.
- Outbox.
- idempotent consumers.

### Stage 4 — Independent Scaling and Deployment

- Kubernetes workloads.
- independent pipelines.
- service-specific capacity.

### Stage 5 — Advanced Operational Capabilities

- automated scaling.
- richer observability.
- resilience engineering.
- architecture fitness functions.

### Key Principle

Avoid premature distribution.

---

# 5.30 AI-Assisted Architecture Workflow

## Architecture Inputs Provided to the AI Agent

- Chapter 46 domain model.
- business capabilities.
- quality attributes.
- architecture constraints.
- existing repository structure.
- engineering instructions.
- architecture decisions.
- security requirements.

## Suggested Workflow

```text
Business Domain
      ↓
Repository Intelligence
      ↓
Architecture Drivers
      ↓
Candidate Boundaries
      ↓
Dependency Analysis
      ↓
Quality Attribute Review
      ↓
Architecture Proposal
      ↓
Human Architecture Review
      ↓
ADR Creation
      ↓
Implementation Planning
```

## AI Agent Contributions

An AI Agent may:

- extract candidate boundaries.
- identify missing quality attributes.
- generate dependency maps.
- detect suspicious coupling.
- propose alternatives.
- draft ADRs.
- analyze trade-offs.
- inspect implementation against intended architecture.

## Human Responsibilities

Architects retain responsibility for:

- system boundaries.
- risk acceptance.
- security architecture.
- cost decisions.
- regulatory implications.
- organization/team alignment.
- final ADR approval.

## Governance Principle

AI-generated architecture proposals are recommendations, not automatically approved architecture.

---

# 6. Professional Diagrams

The chapter should include at minimum the following eight diagrams:

### Diagram 47-1 — Alpha Car Detailing System Context

- Actors.
- platform boundary.
- external systems.

### Diagram 47-2 — Container/Service Architecture

- API Gateway.
- primary services.
- databases.
- event infrastructure.
- Redis.
- external dependencies.

### Diagram 47-3 — Service Dependency Map

- Synchronous API relationships.
- asynchronous relationships differentiated visually.

### Diagram 47-4 — Data Ownership Map

- Each service.
- authoritative business data.
- no shared-table dependencies.

### Diagram 47-5 — Corporate Fleet Booking Sequence

- Full booking lifecycle.
- synchronous validation.
- transaction.
- outbox.
- asynchronous downstream processing.

### Diagram 47-6 — Event Flow

- Event producers.
- event infrastructure.
- consumers.
- key domain events.

### Diagram 47-7 — Security Boundaries

- Internet.
- edge.
- application.
- service.
- data.
- external provider trust zones.

### Diagram 47-8 — Deployment Architecture

- CI/CD.
- registry.
- Kubernetes.
- data infrastructure.
- event infrastructure.
- external integrations.
- telemetry.

---

# 7. Hands-on Example — Designing Corporate Fleet Booking

## 7.1 Scenario

A corporate fleet manager books detailing for multiple vehicles.

## 7.2 Business Requirements

- Account must be active.
- User must be authorized.
- Vehicle must belong to account.
- Vehicle must be eligible.
- Station must provide requested service.
- Capacity must be available.
- Booking limit must not be exceeded.
- Duplicate submissions must not create duplicate bookings.

## 7.3 Identify Service Responsibilities

Map each rule to its authoritative service.

## 7.4 Define API Interactions

Example candidate APIs:

- `POST /bookings`
- corporate account eligibility query.
- fleet vehicle validation.
- station availability query.

## 7.5 Define Persistence Boundary

- Booking transaction.
- idempotency record.
- outbox entry.

## 7.6 Define Events

- `BookingCreated`
- `CorporateFleetBookingCreated` only if separate semantics are genuinely necessary.
- avoid unnecessary event proliferation.

## 7.7 Define Consumers

- Station Operations.
- Billing.
- Notification.
- analytics/operational reporting where appropriate.

## 7.8 Failure Analysis

Walk through:

- duplicate request.
- corporate service timeout.
- station service failure.
- event publication failure.
- notification failure.

## 7.9 Observability

Define correlation from initial API request through asynchronous consumers.

## 7.10 Resulting Architecture

Summarize why the selected architecture balances:

- consistency.
- availability.
- coupling.
- operability.
- complexity.

---

# 8. Claude Example

## 8.1 Architecture Analysis Prompt

Show how Claude Code can analyze:

- domain documentation.
- repository structure.
- architecture decisions.
- quality attributes.

## 8.2 Expected Claude Outputs

- candidate service boundaries.
- dependency analysis.
- data ownership matrix.
- risks.
- ADR proposals.
- diagram source.

## 8.3 Architecture Review Prompt

Ask Claude to challenge the proposal for:

- distributed monolith risks.
- shared database dependencies.
- synchronous dependency chains.
- missing failure handling.
- ownership ambiguity.

## 8.4 Repository-Aware Architecture Work

Claude should inspect:

- instructions.
- skills.
- architecture documents.
- source structure.
- existing ADRs.
- Chapter 46 domain artifacts.

## 8.5 Human Approval

Emphasize that generated architecture remains subject to architecture review.

---

# 9. GitHub Copilot Comparison

Compare how Copilot can support:

- architecture-related repository exploration.
- diagram/code assistance.
- API contract generation.
- ADR drafting.
- implementation-level architecture guidance.

Discuss:

- repository context considerations.
- instruction mechanisms.
- workflow differences from Claude Code.
- situations where IDE proximity is advantageous.

Keep comparison focused on meaningful workflow differences rather than feature cataloguing.

---

# 10. Codex Comparison

Discuss use of Codex for:

- repository-wide analysis.
- architecture-oriented implementation planning.
- contract generation.
- architecture validation.
- refactoring toward agreed boundaries.
- reviewing service dependencies.

Compare with Claude in terms of workflow rather than changing the chapter’s vendor-neutral architecture principles.

---

# 11. Best Practices

Cover practices such as:

- Start with business capabilities.
- Make quality attributes explicit.
- Give every business concept a clear authoritative owner.
- Keep service boundaries cohesive.
- Prefer contracts over shared databases.
- Keep synchronous booking paths short.
- Use events for facts and downstream reactions.
- Implement idempotency.
- Use transactional outbox where state and event publication must remain consistent.
- Treat external systems as unreliable.
- Design observability from the beginning.
- Protect trust boundaries explicitly.
- Document decisions with ADRs.
- Prefer evolutionary architecture.
- Deploy independently only when independence creates real value.
- Validate architecture continuously.
- Keep architecture documentation near the code.
- Use AI Agents to challenge assumptions, not replace architectural accountability.

---

# 12. Anti-patterns

## Technology-First Architecture

"We are using Kubernetes, so everything must be a microservice."

## Entity-per-Service

Creating services mechanically from database entities.

## Shared Database Architecture

Multiple services directly manipulating the same schema.

## Distributed Monolith

Services deploy separately but require synchronous coordination for almost every operation.

## Chatty Booking Workflow

Booking requires numerous sequential remote calls.

## Event Everything

Using asynchronous messaging where an immediate request/response is clearly needed.

## API Everything

Using synchronous APIs for notifications and downstream activities that could be asynchronous.

## Shared Domain Model Package

All services depend on one "common" domain library.

## Gateway Business Logic

Moving domain decisions into the API Gateway.

## Cache as Database

Treating Redis as authoritative state without an explicit ownership model.

## Retry Everything

Repeatedly retrying permanent or business failures.

## Missing Idempotency

Duplicate requests or events create duplicate state.

## Infrastructure Equals Architecture

Architecture documentation contains only Azure/Kubernetes components.

## Premature Microservices

Splitting boundaries before scale, ownership, or operational requirements justify distribution.

## AI-Generated Architecture Without Review

Accepting plausible AI recommendations without assessing business and quality-attribute implications.

---

# 13. Architect’s Notes

Include dedicated callouts addressing:

### Architect’s Note — Boundaries Before Technology

A well-designed boundary can survive changes in programming language, messaging technology, database, cloud provider, and orchestration platform.

### Architect’s Note — Logical Ownership Before Physical Separation

Database ownership can be established before every bounded context receives a physically separate server.

### Architect’s Note — Minimize Critical Path Dependencies

Every synchronous dependency added to booking affects latency and availability.

### Architect’s Note — Distribution Has a Cost

Network calls introduce:

- latency.
- partial failure.
- consistency challenges.
- operational overhead.

### Architect’s Note — Events Are Enterprise Contracts

Changing an event can affect consumers the producer does not directly control.

---

# 14. Enterprise Tips

Include practical guidance:

### Enterprise Tip — Create an Architecture Decision Log

Store major ADRs with the source repository.

### Enterprise Tip — Maintain a Service Catalog

Document:

- owner.
- purpose.
- APIs.
- events.
- dependencies.
- data owner.
- operational SLOs.

### Enterprise Tip — Correlate Business Transactions

Use booking ID, account ID, and trace ID appropriately.

### Enterprise Tip — Test Failure Paths

Architecture confidence depends on known behavior when dependencies fail.

### Enterprise Tip — Monitor Business Outcomes

"Booking API is healthy" does not necessarily mean customers are successfully booking.

### Enterprise Tip — Evolve Boundaries Deliberately

Do not split services merely because the platform makes deployment easy.

---

# 15. Decision Points

Create decision-oriented subsections for:

### Decision Point 1 — Modular Monolith or Microservices?

Consider:

- team structure.
- scaling.
- deployment independence.
- operational maturity.

### Decision Point 2 — Synchronous API or Event?

Consider:

- immediate result requirement.
- coupling.
- consistency.
- failure handling.

### Decision Point 3 — Separate Database?

Consider:

- ownership.
- transaction boundaries.
- operational overhead.

### Decision Point 4 — Cache or Direct Read?

Consider:

- latency.
- freshness.
- failure behavior.
- invalidation.

### Decision Point 5 — Immediate Payment or Corporate Invoicing?

Consider differing business workflows.

### Decision Point 6 — Orchestration or Choreography?

Use the corporate fleet workflow to compare.

### Decision Point 7 — Extract a Service or Keep a Module?

Define measurable triggers for extraction.

### Decision Point 8 — Build or Buy an Integration Capability?

Examples:

- notifications.
- payment processing.
- identity.

---

# 16. Exercises

### Exercise 1 — Identify Architecture Drivers

List five business and five technical drivers for Alpha Car Detailing.

### Exercise 2 — Quality Attribute Scenarios

Write measurable scenarios for:

- availability.
- performance.
- security.
- scalability.
- recoverability.

### Exercise 3 — Define Service Boundaries

Assign Chapter 46 capabilities to the proposed services.

### Exercise 4 — Data Ownership

Determine the authoritative owner for:

- Corporate Account.
- Vehicle.
- Booking.
- Invoice.
- Payment.
- Station Capacity.

### Exercise 5 — API or Event?

Classify ten interactions as:

- command/API.
- query/API.
- event.

Explain each choice.

### Exercise 6 — Corporate Fleet Booking Sequence

Create a detailed sequence diagram.

### Exercise 7 — Failure Analysis

Determine system behavior when:

- Notification Service fails.
- Fleet Service fails.
- event infrastructure fails.
- Redis fails.

### Exercise 8 — Architecture Decision Record

Write an ADR for database-per-service.

### Exercise 9 — Distributed Monolith Review

Inspect a hypothetical architecture containing six services and twelve synchronous dependencies. Propose improvements.

### Exercise 10 — AI-Assisted Architecture Review

Use an AI Agent to review the proposed architecture, then classify recommendations as:

- Accept.
- Modify.
- Reject.

Provide architectural justification.

---

# 17. Interview Questions

Include questions progressing from conceptual to architect-level discussion.

### Foundational

1. What is solution architecture?
2. How does solution architecture differ from software architecture?
3. What are architecture drivers?
4. What are quality attributes?
5. Why should architecture begin with business boundaries?

### Service Architecture

6. How do you identify a microservice boundary?
2. Why should services own their data?
3. What creates a distributed monolith?
4. When is a shared database acceptable as an evolutionary step?
5. Why is a service-per-entity strategy problematic?

### Integration
 1. When should services communicate synchronously?
 2. When should events be used?
 3. What is the difference between a command and an event?
 4. Why must event consumers be idempotent?
 5. What problem does the transactional outbox solve?

### Reliability
 1. How do retries differ from circuit breakers?
 2. How would you design booking when Notification Service is unavailable?
 3. How should duplicate booking requests be handled?

### Security
 1. What are trust boundaries?
 2. Why is gateway authentication insufficient by itself?
 3. How should service-to-service authorization work conceptually?

### Data
 1. Who should own corporate account data?
 2. Why should another service not directly query that service's tables?
 3. What trade-offs arise from database-per-service?

### Infrastructure
 1. What problems do containers solve?
 2. What problems does Kubernetes solve?
 3. Why should Kubernetes not determine business service boundaries?

### Architecture Leadership
 1. How do you document architecture trade-offs?
 2. When should a modular monolith evolve into microservices?
 3. How do quality attributes influence architecture?
 4. How would you reduce a long synchronous dependency chain?
 5. How do you evolve architecture without a large rewrite?

### AI Engineering
 1. How can an AI Agent assist solution architecture?
 2. Which architectural decisions should remain under human authority?
 3. How would you validate an AI-generated service-boundary proposal?
 4. How can Repository Intelligence improve architecture recommendations?

---

# 18. Chapter Summary

Summarize that:

- Chapter 46 established Alpha Car Detailing's business domain.
- Solution architecture translates that domain into implementable system boundaries.
- Architecture begins with business capabilities and quality attributes.
- Booking, Corporate Account, Fleet, Station Operations, Billing, Payment, and Notification have distinct responsibilities.
- Clear data ownership prevents uncontrolled coupling.
- APIs and events solve different integration problems.
- Corporate fleet booking demonstrates how synchronous and asynchronous interactions coexist.
- Reliability requires idempotency, outbox patterns, bounded retries, and failure isolation.
- Security must be designed across explicit trust boundaries.
- Observability must cover technical and business transactions.
- Containers and Kubernetes are deployment mechanisms rather than the source of architecture boundaries.
- CI/CD operationalizes architecture through controlled independent delivery.
- ADRs preserve architectural reasoning and trade-offs.
- Architecture should evolve as business and operational demands justify greater distribution.
- AI Agents can accelerate architecture discovery, analysis, documentation, and review while humans remain accountable for architectural decisions.

Conclude by preparing the transition to:

**Chapter 48 — Clean Architecture**, where the internal architecture of individual Alpha Car Detailing services is examined in detail.

---

# 19. Further Reading

Organize references into categories rather than an unstructured link list.

### Domain and Architecture

- Domain-Driven Design.
- bounded contexts.
- enterprise application architecture.
- evolutionary architecture.

### Distributed Systems

- microservice architecture.
- distributed systems reliability.
- eventual consistency.
- messaging patterns.

### Integration

- REST/API design.
- event-driven architecture.
- transactional outbox.
- idempotent consumers.

### Security

- Zero Trust.
- OAuth/OIDC.
- service identity.
- cloud-native security.

### Cloud-Native Architecture

- containers.
- Kubernetes.
- resilience patterns.
- cloud architecture guidance.

### Observability

- OpenTelemetry.
- distributed tracing.
- structured logging.
- SRE and service-level objectives.

### Architecture Governance

- Architecture Decision Records.
- architecture fitness functions.
- evolutionary architecture.

### AI-Assisted Architecture

- Repository Intelligence.
- architectural review using AI Agents.
- governed AI-assisted decision workflows.

**Chapter 47 outline is complete.**
