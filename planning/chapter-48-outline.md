# Chapter 48 — Clean Architecture

**Part VII — Alpha Car Detailing**  
**Target manuscript:** `book/48-clean-architecture.md`  
**Status:** Detailed outline — awaiting approval before manuscript drafting

> **Core principle:** Business rules must remain independent of frameworks, databases, external systems, and delivery technologies.

## 1. Story-driven opening

### 1.1 Northstar's fleet request reaches the implementation team

- Continue Chapters 46–47: Northstar Distribution requests service for 24 vehicles over two days.
- Present an implementation proposal that places authorization, account checks, fleet validation, capacity calls, EF Core persistence, and notification delivery inside one endpoint.
- Show the consequences: duplicated rules, difficult tests, persistence types in responses, and booking behavior coupled to provider failures.
- Introduce a change request to support another delivery channel or replace an external adapter, and identify which business rules should remain unaffected.

### 1.2 Restructure responsibilities without changing business meaning

- Separate the Booking domain, application workflow, infrastructure adapters, and API delivery mechanism.
- Preserve authorized fleet access, eligible vehicles, station suitability, and confirmation only after committed capacity.
- Distinguish receiving a request, creating a pending booking, and confirming a commitment.
- Keep booking limits, fleet grouping, partial acceptance, and reservation expiry explicit decisions where Chapters 46–47 leave them unresolved.

## 2. Learning objectives

- Explain the dependency rule and distinguish source-code dependencies from runtime calls.
- Assign responsibilities to Domain, Application, Infrastructure, and API/presentation layers.
- Model booking entities, value objects, invariants, and domain events.
- Implement explicit application use cases through narrow interfaces and dependency inversion.
- Isolate persistence, external integrations, and delivery contracts from business rules.
- Locate composition, validation, authorization, errors, transactions, and observability appropriately.
- Preserve service ownership and govern shared contracts.
- Design unit, integration, contract, architecture, and workflow tests.
- Guide AI Agents with enforceable boundaries and evidence-based validation.

## 3. Background

### 3.1 From solution architecture to service internals

- Chapter 46 establishes business meaning and approved case-study invariants.
- Chapter 47 establishes candidate service responsibilities, data ownership, and communication boundaries.
- Chapter 48 establishes internal organization within a service; layers are not separately deployed services.
- Explain why sound internal layering cannot compensate for incorrect service boundaries.

### 3.2 Clean Architecture and related approaches

- Introduce separation of policy from mechanisms, high cohesion, and controlled coupling.
- Relate Clean Architecture to layered, hexagonal, and onion architectures through their shared concern for inward dependencies.
- Distinguish logical boundaries from the number of projects and folders.
- Explain that dependency injection supports the design but does not establish architectural correctness by itself.

### 3.3 Baseline and chapter scope

- Start from the existing Booking projects, `IBookingService`, `BookingService`, `IBookingRepository`, and create/read behavior.
- Preserve the initial approach of direct application services; CQRS and a mediator are deferred unless an approved task and ADR justify them.
- Separate existing code, proposed corporate-booking additions, and unresolved policies.
- Apply the structure incrementally without requiring every candidate service to be implemented.
- Keep this chapter focused on implementation inside Booking Service. Reserve microservice architecture, deployment topology, and service decomposition for Chapter 49; reference Chapter 47's established ownership decisions only where they constrain internal design.

## 4. Concepts

### 4.1 Clean Architecture principles

- Business policy at the center; replaceable mechanisms at the edge.
- The dependency rule: inner layers do not depend on outer implementations.
- Explicit boundaries, cohesive responsibilities, and deliberate data mapping.
- Independence means controlled coupling, rather than promising that technology changes require no work.

### 4.2 Domain layer

- Own booking identity, lifecycle behavior, invariants, value objects, and domain events.
- Place behavior in aggregate methods that protect valid state transitions.
- Keep HTTP, EF Core, provider SDKs, configuration, and transport serialization outside the Domain.
- Pass time and required facts explicitly; avoid hidden network, database, or system-clock access.

### 4.3 Domain entities and aggregate boundaries

- Use the existing `Booking` aggregate and `BookingId` as the starting point.
- Explain identity, lifecycle, encapsulation, and aggregate consistency.
- Evaluate fleet requests as grouped appointments versus a single aggregate only after the relevant business decision.
- Reference fleet, account, vehicle, and station identities without importing those services' domain models.

### 4.4 Value objects

- Use `VehicleRegistrationNumber` to explain value equality, normalization, immutability, and construction rules.
- Distinguish an identifier from the business object it references.
- Consider scheduling or reservation-reference types where they clarify approved semantics.
- Keep database conversion and transport formatting outside value-object business rules.

### 4.5 Domain rules and domain services

- Separate object invariants, rules spanning domain objects, and application orchestration.
- Use domain services only where behavior does not naturally belong to an entity or value object.
- Consume authoritative external decisions without duplicating Corporate Account, Fleet, or Station Operations policy.
- Show why a booking-limit number cannot be invented while its policy and consumption owner remain unresolved.

### 4.6 Application layer and use cases

- Coordinate actions such as creating, retrieving, and confirming bookings.
- Define inputs, results, required permissions, workflow steps, and transaction boundaries.
- Obtain eligibility facts, invoke domain behavior, and request persistence through ports.
- Reuse the same application behavior from HTTP and future message or job entry points.
- Organize by business action without requiring CQRS.

### 4.7 Interfaces, abstractions, and dependency inversion

- Keep `IBookingRepository` in Application, matching the current baseline.
- Introduce proposed ports for corporate-account eligibility, batch fleet validation, and capacity reservations only as needed.
- Let the consuming inner layer define the contract; let Infrastructure implement it.
- Keep port inputs and results independent of HTTP responses, SDK types, EF Core, and `IQueryable`.
- Distinguish dependency inversion, dependency injection, and runtime polymorphism.

### 4.8 Infrastructure layer

- Implement persistence, transaction mechanics, migrations, external clients, outbox storage, and broker adapters.
- Translate remote contracts into application-owned facts and outcomes.
- Isolate retry, timeout, serialization, and provider configuration details.
- Preserve business ownership even when infrastructure is shared operationally.

### 4.9 API/presentation layer

- Bind requests, apply transport validation, authenticate callers, invoke use cases, and map responses.
- Keep endpoints thin while retaining meaningful delivery responsibilities.
- Separate public request/response DTOs from domain entities and persistence models.
- Apply the same boundary discipline to message consumers and scheduled jobs.

### 4.10 Composition root

- Use the API host's `Program.cs` and registration extensions to connect implementations to ports.
- Permit the host to reference Infrastructure for composition; prevent endpoints from directly using concrete adapters.
- Explain scoped database contexts, compatible lifetimes, configuration validation, and worker scopes.
- Avoid service locators and global access to the dependency container.

### 4.11 Events and shared contracts

- Distinguish in-process domain events from versioned integration events.
- Explain why the existing `BookingCreatedDomainEvent` does not establish booking confirmation.
- Map confirmed business facts to contracts such as `BookingConfirmed` without exposing aggregate internals.
- Separate raising a domain event, committing an integration-event obligation to the outbox, and publishing it through a broker adapter. Explain at-least-once publication, stable event identity, and consumer idempotency without treating the outbox as an exactly-once guarantee.
- Keep shared schemas and technical primitives small; retain business rules inside their owning service.

## 5. Architecture discussion

### 5.1 Responsibility and dependency matrix

- Define each layer's purpose, allowed references, and prohibited dependencies.
- Document `Application -> Domain`, `Infrastructure -> Application/Domain`, and `API -> Application`.
- Document the `API -> Infrastructure` reference used for composition and how to constrain it. The composition root is an outer adapter; its reference does not permit an inner layer to depend outward.
- Explain the distinction between project references, package dependencies, and runtime collaboration.

| Project | Permitted dependencies | Dependencies to reject |
| --- | --- | --- |
| Domain | Framework-independent domain primitives | Application, Infrastructure, API, EF Core, ASP.NET Core, broker/provider SDKs |
| Application | Domain and approved application abstractions | Infrastructure, API, persistence/provider implementations, HTTP transport types |
| Infrastructure | Application and Domain | API and other services' internal projects or databases |
| API | Application; Infrastructure for host composition | Concrete repositories or provider adapters in endpoint behavior |

- Apply the same package-independence checks to shared domain primitives; a clean direct-reference list cannot compensate for a technology dependency brought in transitively.

### 5.2 Booking project structure

- Preserve the existing service root: `alpha-car-detailing/src/Services/Booking/`.
- Retain `AlphaCarDetailing.Booking.Domain`, `.Application`, `.Infrastructure`, and `.Api`.
- Organize Application and API features by use case while keeping domain behavior with aggregates.
- Identify proposed corporate-booking feature folders, adapter folders, contract artifacts, and test projects as additions.
- Keep the engineering Harness outside the runtime dependency graph.

### 5.3 Service boundaries and contract ownership

- Booking owns its lifecycle and stored booking commitments.
- Corporate Account supplies authoritative account and commercial eligibility decisions.
- Fleet supplies authoritative vehicle membership and eligibility facts.
- Station Operations owns capacity allocation and reservation state.
- Billing and Notification retain their Chapter 47 responsibilities.
- Prohibit cross-service database access and references to another service's implementation projects.
- Use governed APIs/events and deliberate snapshots with freshness semantics.

### 5.4 Corporate fleet booking through the layers

- Derive trusted caller scope, validate input, and invoke an explicit application use case.
- Coordinate account and fleet checks through narrow ports; prefer batch validation for the vehicle list.
- Obtain capacity commitment according to the selected reservation protocol.
- Apply booking rules and transition to confirmed state only when required commitments and authorizations exist.
- Commit Booking-owned state and an outbound-event record in one local transaction.
- Return an appropriate application outcome; publish and notify asynchronously after commit.
- Keep multi-vehicle grouping and partial acceptance conditional on approved policy.

### 5.5 Persistence, transactions, and concurrency

- Place `BookingDbContext`, EF Core mappings, migrations, and repositories in Infrastructure.
- Preserve aggregate encapsulation while supporting persistence rehydration and value-object mapping.
- Let Application define the required unit of work while Infrastructure implements atomic storage.
- Evaluate whether the existing repository save operation is sufficient before adding a separate commit abstraction.
- Address unique constraints, optimistic concurrency, and scoped request idempotency.
- Explain why an eligibility read followed by a write cannot safely enforce a shared allowance under concurrency.
- Separate local transaction guarantees from distributed reservation and recovery protocols.

### 5.6 External integrations and failure recovery

- Map remote responses and errors through an adapter boundary.
- Propagate cancellation and deadlines; use bounded retries only where operations are safe to repeat.
- Retain stable request/reservation identities for ambiguous timeouts and duplicate requests.
- Handle reservation success followed by Booking commit failure through the selected release, expiry, or reconciliation policy.
- Keep notification and broker failures outside the synchronous confirmation requirement when a durable outbox exists.
- Cross-reference Chapter 47's distributed coordination discussion without treating a local database rollback as remote compensation.

### 5.7 Validation across boundaries

| Boundary | Planned responsibility |
| --- | --- |
| API/presentation | Request shape, parsing, required fields, and transport constraints. |
| Application | Use-case prerequisites, authoritative facts, permissions, and workflow coordination. |
| Domain | Invariants, valid value objects, and legal state transitions. |
| Persistence | Referential/uniqueness constraints and concurrency enforcement for owned data. |

- Distinguish necessary defense in depth from duplicated business-policy implementations.
- Test that an alternative entry point cannot bypass domain invariants or application authorization.

### 5.8 Error handling

- Separate invalid input, business rejection, authorization denial, missing resources, concurrency conflicts, unavailable dependencies, and unexpected failures.
- Define application outcomes or typed errors without HTTP status codes in the Domain.
- Map outcomes to stable Problem Details at the API boundary and to retry/dead-letter decisions at message boundaries.
- Distinguish pending or uncertain distributed outcomes from definite rejection.
- Avoid catch-all success responses, swallowed exceptions, provider stack traces, and misleading retries.

### 5.9 Logging and observability

- Use structured logs and traces at entry points, use-case boundaries, persistence, and external adapters.
- Carry request, workflow, reservation, and event correlation across boundaries.
- Keep telemetry SDKs outside Domain; expose business facts through outcomes and events.
- Separate operational logging from durable audit requirements.
- Track booking outcomes, dependency latency, reservation failures, outbox age, and reconciliation backlog without sensitive or unbounded metric labels.

### 5.10 Security

- Authenticate at delivery boundaries; enforce resource-level authorization in the application path used by every entry point.
- Adapt trusted identity into a narrow caller context rather than passing `HttpContext`, tokens, or `ClaimsPrincipal` into Domain.
- Verify corporate-account and fleet scope; never trust a body-supplied organization identifier as authorization evidence.
- Apply least privilege to database, broker, and provider identities; keep secrets at the infrastructure/host boundary.
- Test cross-organization access, over-posting, unauthorized transitions, and safe error/log output.

### 5.11 Testing strategy and architecture tests

- Domain tests: invariants, value objects, deterministic time inputs, and lifecycle transitions.
- Application tests: observable outcomes, authoritative decisions, commit requirements, and meaningful failure paths through narrow fakes.
- Persistence/integration tests: real mappings, constraints, migrations, concurrency, idempotency, and booking/outbox atomicity.
- API/contract tests: public DTOs, authorization, error mapping, and compatibility.
- Workflow tests: selected cross-service failure, redelivery, reservation recovery, and notification scenarios.
- Architecture tests: project and assembly references, forbidden type dependencies, package metadata, shared-code scope, and cross-service separation.
- Include the existing native SQLite references in Domain/domain primitives as a discovery finding to investigate and move outward through a separately validated change.
- Explain that architecture tests provide structural evidence; behavioral tests and review establish business correctness.

### 5.12 Deterministic architecture gates

- Define executable rules against project references, resolved package metadata, and compiled type dependencies; naming and folder checks alone are insufficient.
- Reject Domain/Application references to Infrastructure or API, including provider types exposed in signatures or inherited types.
- Check direct and transitive technology dependencies in Domain and shared primitives, including the known native SQLite reference.
- Permit Infrastructure registration at the host composition boundary while rejecting concrete infrastructure use from API endpoint types.
- Reject cross-service implementation references and business entities in shared contract packages.
- Use a controlled negative fixture or temporary dependency mutation to prove that a forbidden reference fails the gate, then restore the valid graph.
- Run the checks in pull-request CI and the Harness; bind results to the tested revision. Keep business correctness, SQL access review, and runtime behavior within their appropriate behavioral tests and review.

### 5.13 Trade-offs and evolution

- Compare project-level separation with lighter logical boundaries for simpler services.
- Balance explicit ports and DTOs against unnecessary interfaces and mapping layers.
- Discuss when feature folders improve cohesion without weakening inward dependencies.
- Preserve direct use cases initially; identify evidence that could justify CQRS or additional abstractions later.
- Record meaningful boundary changes in ADRs and enforce accepted rules through CI and the Harness.

## 6. Professional diagrams

Include the following figures in the completed chapter; these are diagram specifications for the outline. Each figure will have a title, caption, legend where needed, and a reference from the relevant discussion.

### Figure 48-1 — Clean Architecture layers

- **Type:** Nested component/layer diagram.
- **Content:** Domain at the center; Application around it; Infrastructure and API/presentation as outer adapters.
- **Caption focus:** Stable booking policy is separated from persistence, external providers, and delivery technologies.
- **Reference:** Sections 4.1–4.9.

### Figure 48-2 — Dependency direction inside Booking Service

- **Type:** Directed project-dependency diagram.
- **Content:** Allowed inward references, framework-independent domain primitives, and the API host's composition-only Infrastructure reference.
- **Caption focus:** Compile-time dependencies differ from runtime execution order; forbidden outward dependencies are explicitly identified.
- **Reference:** Sections 4.7, 4.10, and 5.1.

### Figure 48-3 — Booking Service project structure

- **Type:** Hierarchical package diagram.
- **Content:** The four existing projects; proposed feature, persistence, integration, and contract locations; and corresponding test boundaries.
- **Caption focus:** Projects enforce layer separation while feature folders organize business actions; proposed additions are labeled.
- **Reference:** Section 5.2.

### Figure 48-4 — Corporate fleet request flow through the layers

- **Type:** Sequence diagram, split into decision and commit/publication views if necessary.
- **Content:** API mapping, application orchestration, external decisions through ports, domain behavior, local booking/outbox commit, response mapping, and later publication.
- **Caption focus:** Confirmation requires capacity commitment; runtime calls may reach outer implementations without reversing source dependencies.
- **Reference:** Sections 5.4–5.6.

### Figure 48-5 — Infrastructure dependency inversion

- **Type:** Port-and-adapter component diagram.
- **Content:** Application use cases depend on application-owned ports; EF Core and remote adapters implement them; the composition root selects implementations.
- **Caption focus:** The use case defines what it needs without importing EF Core, HTTP-client responses, or provider SDKs.
- **Reference:** Sections 4.7–4.10.

### Figure 48-6 — Testing architecture

- **Type:** Test-boundary diagram.
- **Content:** Domain/application tests inside the service, integration tests around adapters, contract tests at published boundaries, and selected workflow tests across services; architecture checks span project/package boundaries.
- **Caption focus:** Each test type addresses a distinct risk; structural checks complement behavioral evidence.
- **Reference:** Section 5.11.
- **Architecture-gate overlay:** Include Section 5.12's checks over projects, packages, and type dependencies, distinguishing them from runtime test calls.

## 7. Hands-on example

### 7.1 Establish the baseline and implementation decisions

- Inspect existing projects, references, domain behavior, create/read routes, tests, and repository instructions.
- Record current behavior separately from the corporate fleet capabilities to be added.
- Build a short decision register for fleet grouping, partial acceptance, limits, and reservation protocol; do not encode unresolved choices.

### 7.2 Define the corporate booking use case

- Specify caller scope, input, outcomes, required facts, confirmation conditions, and failure scenarios.
- Trace approved rules such as BR-FLT-001/002, BR-SVC-001, BR-CAP-001/002, and BR-AUT-001 to their enforcement points.
- Label any fleet endpoint or request contract as proposed until implemented and versioned.

### 7.3 Implement the domain behavior

- Extend the existing booking model only where the approved lifecycle requires it.
- Add value objects or domain methods that enforce meaningful rules.
- Keep pending creation and confirmation distinct; reject confirmation without required capacity evidence.

### 7.4 Define ports and implement application orchestration

- Reuse the existing persistence port where appropriate.
- Add only the eligibility and reservation ports needed for the scenario.
- Coordinate authorization, authoritative facts, domain behavior, and the local commit through a direct application service/use case.
- Make time, cancellation, and retry identities explicit.

### 7.5 Implement adapters and reliable persistence

- Add EF Core mappings and external-client implementations outside the inner layers.
- Translate remote DTOs into application-owned results.
- Persist booking changes and an outbound event atomically; publish only after commit.
- Preserve durable recovery state for ambiguous reservation and commit outcomes where the selected protocol requires it.

### 7.6 Add API delivery and composition

- Map the proposed HTTP contract to application input and results to HTTP responses.
- Configure authentication, resource authorization, Problem Details, and correlation.
- Register application services and infrastructure adapters with compatible lifetimes.
- Keep business decisions out of endpoints and startup code.

### 7.7 Verify behavior and boundaries

- Exercise accepted, rejected, unauthorized, duplicate, concurrent, and dependency-failure scenarios.
- Verify reservation success followed by local commit failure and broker failure after commit.
- Add architecture checks for forbidden layer, package, and cross-service dependencies.
- Demonstrate a failing forbidden-dependency case and a passing composition-only reference; capture the tested revision and gate outcome.
- Recheck existing create/read behavior after structural changes.

### 7.8 Hands-on acceptance criteria

- Domain and Application depend only on permitted inward abstractions and framework-independent types.
- Booking policy can be tested without a web host, database, or remote service.
- Changing an adapter does not require moving business rules into Infrastructure.
- Confirmation preserves the approved fleet, authorization, station, and capacity invariants.
- Requests cannot gain access by substituting another organization's identifiers.
- Booking/outbox changes commit together; retries do not silently create duplicate commitments.
- Shared contracts expose no service-internal entities or persistence types.
- Architecture checks detect a deliberately introduced forbidden dependency.
- Changes, validation evidence, assumptions, and unresolved decisions remain reviewable.

## 8. Claude example

### 8.1 Prepare Repository Intelligence

- Supply the approved business rules, Chapter 47 boundaries, architecture instructions, current code map, and relevant tests.
- Distinguish persistent Instructions, reusable Skills, the current Prompt, and temporary Steering Notes.
- Identify permitted files, prohibited dependencies, and open decisions.

### 8.2 Outline the implementation prompt

- Request one focused corporate-booking slice using direct application orchestration.
- Require discovery before edits, explicit port ownership, preserved contracts, and tests for the affected risks.
- Prohibit invented limits, remote domain-entity sharing, provider types in inner layers, and unapproved CQRS.
- Require a change summary and actual validation results rather than unsupported claims.

### 8.3 Review and validate the result

- Use the Lead, Developer, Reviewer, Validator, and Evaluator responsibilities established earlier in the handbook.
- Check business placement, package references, idempotency, security, and failure recovery.
- Run deterministic validation gates; retain human accountability for policy and architecture decisions.

## 9. GitHub Copilot comparison

- Apply the same use case, repository context, boundaries, and acceptance criteria.
- Outline an IDE-centered workflow for endpoint changes, domain methods, adapters, and targeted tests.
- Review generated imports, project references, abstractions, and direct database access carefully.
- Compare workflow and context delivery where materially different; avoid duplicating the entire example or assuming tool behavior establishes correctness.

## 10. Codex comparison

- Apply the same task through repository discovery, focused changes, and executable validation.
- Require inspection of applicable `AGENTS.md` instructions and the current baseline.
- Outline checks for project/package dependencies, existing contract behavior, and the corporate-booking failure paths.
- Use the same architecture rubric as Claude and Copilot; compare reviewable evidence rather than vendor claims.

## 11. Best practices

- Model business behavior before persistence and delivery concerns.
- Keep aggregates responsible for invariants and Application responsible for orchestration.
- Define narrow ports at the consuming boundary and isolate provider translation.
- Keep endpoints thin, DTO mapping deliberate, and dependency registration explicit.
- Preserve service data ownership, versioned contracts, and local transaction guarantees.
- Design authorization, concurrency, recovery, and observability with the workflow.
- Test behavior at useful boundaries and enforce dependency rules continuously.
- Introduce abstractions when they clarify responsibility or enable a required substitution.

## 12. Anti-patterns

- Business rules in endpoints, EF Core configurations, or external-client wrappers.
- Domain depending on EF Core, or Application calling concrete Infrastructure implementations directly.
- Anemic entities paired with an oversized application service containing every rule.
- EF Core, HTTP, broker, or provider types leaking through inner-layer interfaces.
- Returning domain aggregates or database entities as public API contracts.
- Referencing another service's database, domain assembly, or implementation project.
- Sharing business-policy libraries that blur authoritative ownership.
- Treating folders, dependency injection, or a mediator as proof of Clean Architecture.
- Creating generic repositories, interfaces, or mapping layers with no meaningful boundary.
- Using availability checks as reservations or remote checks as concurrency-safe quota enforcement.
- Publishing before commit, assuming exactly-once delivery, or swallowing uncertain outcomes.
- Authorizing only at the gateway or trusting request-supplied organizational scope.
- Allowing AI Agents to invent business policies or weaken validation to make changes pass.
- Allowing AI Agents to add an outward reference or bypass a port to implement a change faster.

## 13. Architect's notes

- Clean Architecture protects the direction of policy dependencies; project count is a means of enforcement.
- Business and consistency boundaries deserve more attention than visual symmetry between layers.
- Layering neither eliminates distributed failures nor transfers ownership of external facts.
- Domain independence includes the dependency graph of shared primitives and packages.
- A composition exception should remain narrow and mechanically reviewable.
- Preserve simplicity now while documenting evidence that would justify later architectural changes.

## 14. Enterprise tips

- Publish an allowed-dependency matrix alongside service templates and reference examples.
- Review project and package changes with the same care as source changes.
- Establish owners for ports, external contracts, shared packages, and architecture exceptions.
- Keep representative contract and failure fixtures for AI-assisted work.
- Include architecture checks in pull-request validation and Harness evidence.
- Refactor in small steps that separate structural movement from behavior changes.
- Track exceptions with scope, rationale, ownership, and an explicit review point.

## 15. Decision points

| Decision | Trade-off to examine |
| --- | --- |
| Separate projects or logical boundaries? | Enforcement strength versus complexity and build overhead. |
| Existing application service or dedicated use-case classes? | Current simplicity versus cohesion as workflows grow. |
| Repository save or a distinct commit abstraction? | Existing capability versus explicit coordination of multiple local writes. |
| Interface in Domain or Application? | Which inner policy consumes it; retain the current Application repository placement. |
| Fleet group or independent appointments? | Customer commitment, partial outcomes, and aggregate consistency; requires business agreement. |
| Synchronous or durable reservation coordination? | Response needs, ambiguous failures, recovery state, and confirmed capacity semantics. |
| Replicated eligibility facts or live calls? | Freshness requirements, availability, ownership, and decision authority. |
| Shared schema or shared runtime package? | Compatibility governance versus deployment coupling and internal-model leakage. |
| Exceptions or typed outcomes? | Unexpected failures versus expected business decisions and delivery mapping. |
| When to introduce CQRS? | Demonstrated model/workload divergence versus unnecessary initial complexity. |

## 16. Exercises

1. **Classify responsibilities:** Assign booking rules, authorization, EF mappings, HTTP DTOs, and provider calls to layers; justify ambiguous cases.
2. **Refactor a coupled endpoint:** Move orchestration and rules behind appropriate boundaries while preserving its public behavior.
3. **Design a value object:** Define valid construction, equality, normalization, and persistence mapping separately.
4. **Design a capacity port:** Model success, rejection, timeout uncertainty, stable identity, and recovery without provider types.
5. **Implement a booking slice:** Demonstrate approved fleet and capacity invariants through a direct application use case.
6. **Investigate infrastructure leakage:** Trace the existing SQLite dependencies in Domain/domain primitives and propose a validated outward move.
7. **Write architecture tests:** Demonstrate detection of a forbidden reference and a package/type leak without blocking valid composition.
8. **Test isolation and recovery:** Cover another organization's fleet, repeated requests, concurrency, and reservation success followed by commit failure.
9. **Review shared contracts:** Remove internal entities and preserve published semantics and compatibility.
10. **Evaluate AI-generated code:** Identify misplaced behavior, unsupported assumptions, and missing evidence; produce a focused correction plan.

- Define a concrete deliverable and observable acceptance condition for each exercise in the chapter.

## 17. Interview questions

1. What problem does the Clean Architecture dependency rule solve?
2. How can runtime execution reach Infrastructure while source dependencies point inward?
3. How do domain behavior and application orchestration differ in corporate fleet booking?
4. What distinguishes an entity, a value object, and an aggregate?
5. Where should `IBookingRepository` live in this repository, and why?
6. What makes an abstraction independent of EF Core or a remote provider?
7. Why may the API host reference Infrastructure while endpoint logic should avoid concrete adapters?
8. How should validation and authorization work across multiple entry points?
9. Why is viewing availability insufficient for booking confirmation?
10. What happens when capacity is reserved but Booking cannot commit?
11. How do domain events differ from integration events?
12. Which contracts may services share without sharing their domain models?
13. How do you test business behavior without databases or HTTP?
14. What do architecture tests prove, and what remains outside their scope?
15. How can transitive or native package dependencies undermine Domain independence?
16. When do additional interfaces or CQRS make the architecture worse?
17. How would you govern an AI-generated change that crosses a service boundary?

- Provide expected answer points and practical follow-up scenarios when writing the chapter.

## 18. Chapter summary

- Revisit Northstar's request and the responsibilities now assigned to each layer.
- Reinforce the core principle and the difference between dependencies and runtime flow.
- Recap domain modeling, use-case orchestration, ports/adapters, composition, and contract separation.
- Connect correctness to authorization, capacity commitments, transaction boundaries, recovery, and observability.
- Emphasize behavioral evidence, architecture checks, and accountable AI-assisted implementation.
- Bridge explicitly to Chapter 49 — Microservice Architecture, where service decomposition and distributed deployment receive fuller treatment; keep unresolved policies and proposed services visibly distinct from approved or deployed behavior.

## 19. Further reading

- **Architecture foundations:** Robert C. Martin's Clean Architecture and dependency-rule writings; Alistair Cockburn's hexagonal architecture; foundational domain-driven design material.
- **Official .NET guidance:** Dependency injection and lifetimes, ASP.NET Core authorization and Problem Details, EF Core mapping, transactions, concurrency, and testing.
- **Operational standards:** OpenTelemetry documentation and primary guidance on reliable messaging, idempotency, and contract evolution.
- **Handbook cross-references:** Chapters 46–47 for business and service boundaries; Chapters 13–18 for Harness architecture and Agent responsibilities; relevant governance, security, policy, and audit chapters.
- Select and verify primary-source links during chapter drafting; connect each reference to a specific architectural decision.

Chapter 48 outline is complete.
