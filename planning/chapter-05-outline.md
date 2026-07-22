# Chapter 5 — Repository Discovery

**Part II — Repository Intelligence**
**Target file:** `book/05-repository-discovery.md`

The chapter establishes Repository Discovery as the first formal discipline of Repository Intelligence. It builds on the handbook’s frozen distinction among instructions, skills, prompts, roles, steering notes, harness automation, and reusable organizational knowledge.

---

## 1. Story-Driven Opening — The Corporate Fleet Booking Request

### Purpose

Introduce Repository Discovery through a realistic enterprise change request rather than an abstract definition.

The opening demonstrates why an apparently simple request—adding corporate fleet booking—cannot be implemented safely until the team and AI agent understand the repository’s architecture, ownership boundaries, operational constraints, and current implementation.

### Key Concepts

* Enterprise changes rarely belong to one file or one service.
* A feature request does not reveal the system boundaries affected by that feature.
* Repository familiarity must not be assumed.
* AI-generated implementation speed is dangerous when architectural understanding is incomplete.
* Discovery precedes planning.
* Planning precedes modification.
* Evidence precedes architectural claims.

### Important Subsections

#### 1.1 The Request

An engineering manager asks an AI coding agent to add corporate fleet booking to Alpha Car Detailing.

The initial request appears straightforward:

* Corporate customers should create recurring fleet bookings.
* A booking may include multiple vehicles.
* The system must select stations with sufficient capacity.
* Corporate account rules and negotiated service rates must apply.
* Authorized fleet managers should manage bookings.
* Operational teams should receive booking events.

#### 1.2 The First Dangerous Assumption

The agent finds a project named `BookingService` and prepares to modify it.

The chapter highlights that the project name alone does not establish:

* Business ownership.
* Data ownership.
* Runtime responsibility.
* Contract ownership.
* Deployment ownership.
* Authorization responsibility.
* Whether the project is current, deprecated, or transitional.

#### 1.3 Questions That Must Be Answered First

Before implementation, the agent must determine:

* Which service owns bookings?
* Which service owns corporate accounts?
* How station capacity is modeled?
* Whether capacity is checked synchronously or asynchronously?
* Where booking contracts are defined?
* Which identity provider and authorization model are used?
* Which database owns each affected aggregate?
* Which tests validate booking behavior?
* Which CI/CD workflow deploys the service?
* Which AI instructions and repository conventions apply?
* Whether the documentation matches the implementation?

#### 1.4 The Discovery Decision

The team rejects immediate code generation and requires a formal discovery report.

This establishes the chapter’s central principle:

> An AI agent should not modify a repository until it has developed sufficient evidence-based understanding of the repository’s structure, architecture, conventions, dependencies, constraints, and current state.

### Alpha Car Detailing Application

The scenario follows the fleet-booking request throughout the chapter. Each discovery activity adds evidence to the final repository map and confidence assessment.

### Recommended Diagram

**Figure 5-1 — From Feature Request to Evidence-Based Change**

A left-to-right diagram:

```text
Feature Request
      ↓
Initial Assumptions
      ↓
Repository Discovery
      ↓
Evidence and Gaps
      ↓
Discovery Confidence
      ↓
Planning Authorization
      ↓
Implementation
```

---

## 2. Learning Objectives

### Purpose

Define the professional capabilities readers should develop by completing the chapter.

### Key Concepts

By the end of the chapter, readers should be able to:

1. Define Repository Discovery as a formal engineering discipline.
2. Distinguish Repository Search, Repository Discovery, and Repository Intelligence.
3. Establish discovery scope based on task risk and system boundaries.
4. Identify repository entry points and architectural evidence.
5. Map projects, services, modules, dependencies, contracts, data stores, and runtime paths.
6. Examine build, configuration, testing, security, observability, CI/CD, and infrastructure assets.
7. Classify findings as facts, inferences, contradictions, unknowns, or out-of-scope items.
8. Detect stale documentation and conflicting architectural evidence.
9. Produce reusable discovery outputs and repository maps.
10. Assess whether discovery confidence is sufficient to proceed.
11. Execute discovery in monorepositories and multi-repository systems.
12. Integrate discovery into an automated AI engineering harness.
13. Protect secrets, credentials, production data, and other sensitive assets during discovery.
14. Compare repository-discovery workflows across Claude Code, GitHub Copilot, and OpenAI Codex.

### Alpha Car Detailing Application

The objectives are applied to the fleet-booking feature and its dependencies across booking, corporate account, station, identity, pricing, messaging, and deployment assets.

---

## 3. Background — From File Browsing to Repository Intelligence

### Purpose

Explain why traditional repository browsing is inadequate for AI-assisted enterprise engineering.

### Key Concepts

* Repositories are partial representations of deployed systems.
* Enterprise architecture is distributed across source code, configuration, contracts, tests, pipelines, infrastructure, and documentation.
* Repository names and directory structures may not reflect current runtime ownership.
* AI agents may overgeneralize from incomplete evidence.
* Discovery produces inputs for later Repository Intelligence disciplines.

### Important Subsections

#### 3.1 Why Repository Understanding Became an Engineering Problem

Discuss the increasing complexity created by:

* Microservices.
* Polyglot platforms.
* Shared packages.
* Generated code.
* Infrastructure as Code.
* External schemas.
* Event-driven integration.
* Multiple deployment environments.
* Repository-specific AI instructions.
* Automated harnesses.
* Architecture documentation distributed across multiple formats.

#### 3.2 Why AI Agents Misunderstand Repositories

Explain common causes:

* Limited context windows.
* Partial file retrieval.
* Search-result bias.
* Premature pattern matching.
* Trusting names without tracing behavior.
* Treating documentation as authoritative without verification.
* Ignoring generated, hidden, or infrastructure files.
* Assuming conventional architecture where exceptions exist.
* Confusing local code references with runtime dependencies.
* Inferring system behavior from interfaces without inspecting implementations.
* Ignoring branch age, deprecation indicators, or migration states.

#### 3.3 Repository Discovery as an Engineering Discipline

Define Repository Discovery as:

> A systematic, evidence-driven process for examining a repository or repository set to understand its structure, architecture, behavior, dependencies, conventions, constraints, operational model, and current state before planning or implementing change.

Discovery is not:

* Casual browsing.
* Reading a few top-level files.
* Running a single search.
* Asking an AI agent to summarize the repository.
* Generating an architecture diagram from project names alone.
* Treating a repository tree as the system architecture.

#### 3.4 Repository Discovery and Enterprise Change Risk

Explain how insufficient discovery contributes to:

* Incorrect service ownership.
* Cross-boundary data access.
* Duplicate business logic.
* Contract incompatibility.
* Event duplication.
* Security bypasses.
* Incorrect deployment changes.
* Broken build pipelines.
* Undetected architectural drift.
* Misleading AI-generated pull requests.

### Alpha Car Detailing Application

Show how the fleet-booking request crosses business and technical boundaries even before implementation begins.

### Recommended Table

**Table 5-1 — Symptoms of Inadequate Repository Discovery**

Columns:

* Symptom.
* Typical assumption.
* Hidden reality.
* Enterprise consequence.

---

## 4. Concepts — Search, Discovery, and Repository Intelligence

### Purpose

Establish the chapter’s foundational terminology and conceptual boundaries.

### Key Concepts

#### Repository Search

Finding files, symbols, text, references, paths, commits, configuration keys, or dependency declarations.

#### Repository Discovery

Systematically collecting and validating evidence about structure, architecture, behavior, conventions, constraints, dependencies, and current state.

#### Repository Intelligence

The maintained and reusable organizational understanding produced from discovery, instructions, skills, prompts, roles, steering notes, knowledge sources, and validated engineering experience.

### Important Subsections

#### 4.1 Repository Search

Explain search activities such as:

* File-name search.
* Symbol search.
* Text search.
* Reference search.
* Dependency search.
* Commit-history search.
* Configuration-key search.
* Contract search.
* Test search.

Emphasize that search retrieves evidence but does not interpret the system as a whole.

#### 4.2 Repository Discovery

Explain that discovery:

* Has a task and scope.
* Follows a repeatable process.
* Collects evidence from multiple asset types.
* Resolves or records contradictions.
* Produces reusable outputs.
* Measures confidence.
* Establishes readiness to plan or modify.

#### 4.3 Repository Intelligence

Explain that Repository Intelligence:

* Is the organizational capability produced from repeated discovery.
* Must be maintained.
* Can be reused across tasks.
* Includes explicit engineering guidance and accumulated evidence.
* Supports agents, developers, architects, and harnesses.
* Is broader than repository documentation.
* Is broader than AI memory.
* Must preserve provenance and confidence.

#### 4.4 Activity versus Capability

Clarify:

| Term                    | Classification        | Primary Question                                                |
| ----------------------- | --------------------- | --------------------------------------------------------------- |
| Repository Search       | Retrieval technique   | Where is the relevant evidence?                                 |
| Repository Discovery    | Engineering activity  | What does the repository reveal about the system?               |
| Repository Intelligence | Maintained capability | What does the organization know and trust about the repository? |

#### 4.5 Why the Distinction Matters

Discuss the risks of calling search results “repository understanding.”

### Alpha Car Detailing Application

* Search locates `FleetBookingController`.
* Discovery determines whether it is current, how it interacts with capacity and corporate accounts, and which runtime owns the flow.
* Repository Intelligence preserves the validated ownership, contract, and convention model for later work.

### Recommended Diagram

**Figure 5-2 — Search, Discovery, and Repository Intelligence**

```text
Search
  ↓ supplies evidence to
Discovery
  ↓ produces and updates
Repository Intelligence
  ↓ guides
Planning, Implementation, Review, Validation
```

---

## 5. Concepts — Discovery Scope and Boundaries

### Purpose

Explain how discovery scope is established and controlled.

### Key Concepts

* Discovery should be task-informed but not task-blinded.
* Scope must include directly affected assets and likely architectural dependencies.
* Scope must be explicit.
* Out-of-scope findings must still be recorded when they may affect risk.
* Discovery depth should scale with task risk.

### Important Subsections

#### 5.1 Task Scope

Capture:

* Requested behavior.
* Acceptance criteria.
* Known services.
* Expected interfaces.
* Security requirements.
* Operational requirements.
* Compliance constraints.
* Deployment expectations.

#### 5.2 Repository Scope

Identify whether the task involves:

* One project.
* One service.
* Multiple projects in one repository.
* A monorepository.
* Multiple repositories.
* Shared packages.
* External contract repositories.
* Infrastructure repositories.
* Documentation repositories.
* Harness repositories.

#### 5.3 Architectural Scope

Examine:

* Directly modified component.
* Upstream callers.
* Downstream dependencies.
* Shared libraries.
* Data ownership boundaries.
* Events and APIs.
* Deployment assets.
* Security policies.
* Tests.
* Operational dependencies.

#### 5.4 Temporal Scope

Determine whether discovery must include:

* Current branch only.
* Recent migrations.
* Deprecated modules.
* Feature flags.
* Parallel implementations.
* Transitional architecture.
* Pending pull requests.
* Release branches.
* Versioned contracts.

#### 5.5 Explicit Boundaries

Record:

* Included repositories.
* Excluded repositories.
* Environments considered.
* Assets not accessible.
* Systems represented only by contracts.
* Assumptions requiring confirmation.

### Alpha Car Detailing Application

The corporate fleet-booking request initially appears limited to the booking service but expands to include:

* Corporate account ownership.
* Station capacity.
* Identity and authorization.
* Pricing.
* Events.
* Tests.
* CI/CD.
* Kubernetes manifests.
* Repository instructions.

### Recommended Table

**Table 5-2 — Discovery Scope Definition**

Columns:

* Scope dimension.
* Included.
* Excluded.
* Rationale.
* Risk if omitted.

---

## 6. Architecture Discussion — The Structured Repository Discovery Model

### Purpose

Present the chapter’s primary reusable discovery framework.

### Key Concepts

The model should be executable by:

* A developer.
* An architect.
* An AI coding agent.
* A multi-agent workflow.
* An automated harness.

### Important Subsections

#### 6.1 Stage 1 — Establish the Task and Scope

Activities:

* Parse the requested change.
* Identify business capabilities involved.
* Record initial assumptions.
* Define repository and architectural boundaries.
* Estimate task risk.
* Identify required access.

Outputs:

* Task statement.
* Discovery scope.
* Initial risk classification.
* Initial open questions.

#### 6.2 Stage 2 — Identify Repository Entry Points

Activities:

* Read root-level documentation.
* Identify solution and workspace files.
* Locate build and package manifests.
* Locate repository instructions.
* Locate CI/CD definitions.
* Locate infrastructure entry points.
* Locate architecture documentation.
* Locate development scripts.

Outputs:

* Entry-point inventory.
* Initial repository orientation.

#### 6.3 Stage 3 — Map Repository Topology

Activities:

* Map directories.
* Classify code, tests, infrastructure, documentation, generated assets, tooling, prompts, skills, and harness files.
* Identify naming patterns.
* Identify repository subdivisions.

Outputs:

* Repository topology map.
* Asset classification.

#### 6.4 Stage 4 — Identify Architecture Boundaries

Activities:

* Map solutions, projects, modules, services, bounded contexts, and shared libraries.
* Identify ownership boundaries.
* Identify architectural layering.
* Identify cross-boundary references.

Outputs:

* Project and service inventory.
* Architecture boundary map.

#### 6.5 Stage 5 — Trace Build and Runtime Paths

Activities:

* Identify build commands.
* Trace startup and composition roots.
* Identify executables, workers, functions, and jobs.
* Trace deployment units.
* Trace request and event flows.
* Identify runtime configuration sources.

Outputs:

* Build path.
* Runtime topology.
* Deployment-unit inventory.

#### 6.6 Stage 6 — Discover Dependencies and Contracts

Activities:

* Map package dependencies.
* Map project references.
* Identify internal and external APIs.
* Identify event contracts and schema registries.
* Identify generated clients.
* Identify integration boundaries.

Outputs:

* Dependency map.
* API inventory.
* Event contract inventory.

#### 6.7 Stage 7 — Inspect Tests and Validation Mechanisms

Activities:

* Identify unit, integration, contract, architecture, security, performance, and end-to-end tests.
* Trace tests to production components.
* Identify validation scripts.
* Identify test-data dependencies.
* Identify quality gates.

Outputs:

* Test inventory.
* Validation-path map.
* Coverage gaps.

#### 6.8 Stage 8 — Examine Deployment and Operational Assets

Activities:

* Inspect containers.
* Inspect Kubernetes manifests or Helm charts.
* Inspect Terraform, Bicep, or equivalent IaC.
* Inspect CI/CD workflows.
* Inspect observability configuration.
* Inspect environment promotion rules.

Outputs:

* CI/CD inventory.
* Deployment map.
* Operational dependency map.

#### 6.9 Stage 9 — Review Documentation and AI Guidance

Activities:

* Read architecture documents.
* Read ADRs.
* Read repository instructions.
* Read agent files.
* Read skills and prompts.
* Read steering notes.
* Read harness configuration.
* Compare documented behavior with implementation.

Outputs:

* Documentation inventory.
* AI guidance inventory.
* Contradiction candidates.

#### 6.10 Stage 10 — Classify Evidence

Activities:

* Mark each finding by evidence category.
* Link findings to sources.
* Record contradictions and unknowns.
* Separate observations from conclusions.

Outputs:

* Evidence register.
* Contradiction log.
* Unknown register.

#### 6.11 Stage 11 — Produce the Discovery Report

Activities:

* Consolidate maps and inventories.
* Summarize conventions.
* Identify impact areas.
* Record limitations.
* Recommend further exploration.

Outputs:

* Repository Discovery Report.
* Recommended exploration steps.

#### 6.12 Stage 12 — Determine Discovery Readiness

Activities:

* Assess confidence.
* Evaluate unresolved high-risk gaps.
* Decide whether to proceed, deepen discovery, or escalate.

Possible decisions:

* Proceed to planning.
* Proceed with explicit assumptions.
* Perform deeper discovery.
* Request human clarification.
* Block implementation.

### Alpha Car Detailing Application

Each stage is applied to the fleet-booking request, progressively replacing assumptions with verified evidence.

### Recommended Diagram

**Figure 5-3 — Progressive Repository Discovery Workflow**

A 12-stage circular or staged workflow showing that later evidence may trigger return to earlier stages.

### Recommended Table

**Table 5-3 — Discovery Stage, Inputs, Activities, and Outputs**

---

## 7. Architecture Discussion — Repository Entry Points

### Purpose

Teach readers how to identify the files and assets that provide the fastest reliable orientation.

### Key Concepts

Entry points are not necessarily application startup files. They are the highest-value starting locations for understanding repository intent and structure.

### Important Subsections

#### 7.1 Root-Level Entry Points

Examples:

* `README.md`
* `CONTRIBUTING.md`
* `ROADMAP.md`
* `CHANGELOG.md`
* `LICENSE`
* Solution files.
* Workspace files.
* Package-management files.
* Build scripts.
* Container orchestration files.
* Repository instruction files.

#### 7.2 Build Entry Points

Examples:

* `.sln`
* `.slnx`
* `.csproj`
* `Directory.Build.props`
* `Directory.Build.targets`
* `global.json`
* `NuGet.config`
* `package.json`
* `Makefile`
* PowerShell or shell build scripts.

#### 7.3 Runtime Entry Points

Examples:

* `Program.cs`
* Worker startup.
* Function triggers.
* Message consumers.
* Scheduled jobs.
* Composition roots.
* Dependency-injection registration.
* Host builders.

#### 7.4 Deployment Entry Points

Examples:

* Dockerfiles.
* Helm charts.
* Kubernetes manifests.
* Terraform modules.
* Bicep templates.
* GitHub Actions workflows.
* Azure DevOps pipeline YAML.
* Release scripts.

#### 7.5 Architectural Entry Points

Examples:

* Architecture overview.
* ADR index.
* Context maps.
* Dependency diagrams.
* Service catalogues.
* API specifications.
* Event-schema registries.

#### 7.6 AI Engineering Entry Points

Examples:

* `CLAUDE.md`
* `AGENTS.md`
* `.github/copilot-instructions.md`
* Agent role definitions.
* Skill files.
* Prompt libraries.
* Steering notes.
* Harness configuration.
* Evaluation scripts.

### Alpha Car Detailing Application

The discovery begins with:

* Root handbook guidance.
* Alpha Car Detailing solution file.
* Service directories.
* CI/CD workflows.
* Kubernetes overlays.
* Repository AI instructions.
* ADRs related to booking and account ownership.

### Recommended Table

**Table 5-4 — Entry-Point Priority by Discovery Goal**

---

## 8. Architecture Discussion — File and Directory Topology

### Purpose

Explain how to convert a repository tree into a meaningful asset map without treating directory names as architectural truth.

### Key Concepts

* Directory structure is evidence, not proof.
* Topology discovery must classify assets by role.
* Generated and archived assets must be distinguished from active source.
* Repository structure may reflect history rather than architecture.

### Important Subsections

#### 8.1 Topology Classification

Classify directories as:

* Application source.
* Domain source.
* Infrastructure source.
* Shared libraries.
* Tests.
* Contracts.
* Schemas.
* Documentation.
* Infrastructure as Code.
* Deployment manifests.
* Build tooling.
* Generated code.
* Samples.
* Migration assets.
* AI instructions.
* Skills.
* Prompts.
* Harness automation.
* Archived or deprecated code.

#### 8.2 Naming Patterns

Identify:

* Service naming.
* Project suffixes.
* Test naming.
* Contract naming.
* Versioning conventions.
* Infrastructure naming.
* Environment overlays.
* Generated code markers.

#### 8.3 Topology Anomalies

Examples:

* Service implementation outside expected directory.
* Duplicate project names.
* Deprecated code still referenced.
* Shared library containing business logic.
* Tests stored with production code.
* Infrastructure maintained in a separate tree.
* Multiple competing instruction files.

#### 8.4 Active versus Historical Assets

Evidence sources:

* Build references.
* Pipeline references.
* Recent commits.
* Deployment manifests.
* Package publication.
* Deprecation notes.
* Feature flags.
* Archive markers.

### Alpha Car Detailing Application

The topology map identifies:

* `src/Booking`
* `src/CorporateAccounts`
* `src/Stations`
* `src/Pricing`
* `src/Identity`
* `contracts`
* `infrastructure`
* `deploy`
* `tests`
* `.github`
* `skills`
* `prompts`
* `harness`

The chapter cautions that these paths do not yet prove runtime or data ownership.

### Recommended Diagram

**Figure 5-4 — Alpha Car Detailing Repository Topology Map**

### Recommended Table

**Table 5-5 — Repository Asset Classification**

---

## 9. Architecture Discussion — Solution, Project, Module, and Service Boundaries

### Purpose

Teach readers to distinguish source organization from architectural ownership.

### Key Concepts

* A solution is a build grouping.
* A project is a compilation or packaging unit.
* A module is a logical boundary.
* A service is a runtime and deployment boundary.
* A bounded context is a domain ownership boundary.
* These boundaries may overlap but are not interchangeable.

### Important Subsections

#### 9.1 Solution Boundaries

Discover:

* Included projects.
* Build configurations.
* Solution folders.
* Excluded or optional projects.
* Multiple solution files.

#### 9.2 Project Boundaries

Discover:

* Project type.
* Target framework.
* Package references.
* Project references.
* Executable status.
* Published artifacts.
* Internal visibility.
* Shared build properties.

#### 9.3 Module Boundaries

Discover:

* Internal module interfaces.
* Feature folders.
* Vertical slices.
* Domain assemblies.
* Shared kernels.
* Modular-monolith boundaries.

#### 9.4 Service Boundaries

Discover:

* Deployment unit.
* Runtime process.
* Network endpoint.
* Database ownership.
* Event ownership.
* Operational ownership.
* Scaling characteristics.

#### 9.5 Boundary Violations

Identify:

* Direct database access across services.
* Business logic in shared packages.
* Circular references.
* Internal implementation types exposed as contracts.
* Shared authentication assumptions.
* Cross-service transaction coupling.

### Alpha Car Detailing Application

The agent determines whether bookings are:

* A module within a customer platform.
* A standalone booking service.
* A capability split between reservation and station operations.
* A legacy API backed by a newer event-driven service.

### Recommended Diagram

**Figure 5-5 — Build Boundaries versus Runtime Boundaries**

### Recommended Table

**Table 5-6 — Boundary Type and Evidence Required**

---

## 10. Architecture Discussion — Runtime and Deployment Topology

### Purpose

Show how discovery moves from static repository structure to deployed-system behavior.

### Key Concepts

* Source dependencies do not fully describe runtime dependencies.
* Deployment manifests reveal scaling, identity, networking, and configuration relationships.
* Runtime entry points must be traced.
* One repository may produce multiple deployment units.

### Important Subsections

#### 10.1 Runtime Components

Identify:

* APIs.
* Workers.
* Consumers.
* Scheduled jobs.
* Serverless functions.
* Batch processes.
* Migration executables.
* Administrative tools.

#### 10.2 Composition Roots

Inspect:

* Dependency registration.
* Middleware registration.
* Configuration binding.
* Authentication setup.
* Client registration.
* Messaging registration.
* Observability registration.
* Feature flags.

#### 10.3 Runtime Communication

Map:

* HTTP.
* gRPC.
* Events.
* Queues.
* Shared databases.
* Cache.
* File transfer.
* Service mesh.
* External SaaS APIs.

#### 10.4 Deployment Units

Identify:

* Container images.
* Kubernetes deployments.
* Stateful sets.
* Cron jobs.
* Azure Functions.
* App Services.
* Background workers.
* Database migration jobs.

#### 10.5 Environment Differences

Inspect:

* Development.
* Test.
* Staging.
* Production.
* Regional overlays.
* Tenant-specific configuration.
* Feature toggles.

### Alpha Car Detailing Application

Trace the fleet-booking flow from:

1. Corporate portal.
2. API gateway.
3. Booking API.
4. Corporate account service.
5. Station-capacity service.
6. Pricing service.
7. Booking database.
8. Event hub or message broker.
9. Station operations consumer.
10. Notification service.

### Recommended Diagram

**Figure 5-6 — Alpha Car Detailing Fleet-Booking Runtime Flow**

---

## 11. Architecture Discussion — Dependency Discovery

### Purpose

Explain how to identify code, runtime, package, and organizational dependencies.

### Key Concepts

* Dependency discovery must cover more than package manifests.
* Dependencies should be classified by direction, ownership, criticality, and coupling.
* Transitive dependencies may introduce hidden operational and security risk.

### Important Subsections

#### 11.1 Project Dependencies

Inspect:

* Project references.
* Shared libraries.
* Internal packages.
* Generated clients.
* Build-time tools.

#### 11.2 Package Dependencies

Inspect:

* Direct packages.
* Transitive packages.
* Version centralization.
* Private feeds.
* Vulnerability policies.
* Locked versions.
* Deprecated packages.

#### 11.3 Runtime Dependencies

Inspect:

* Service endpoints.
* Message brokers.
* Databases.
* Caches.
* Identity providers.
* Object storage.
* External APIs.

#### 11.4 Contract Dependencies

Inspect:

* OpenAPI.
* Protobuf.
* JSON Schema.
* Avro.
* AsyncAPI.
* Shared DTO packages.
* Event-schema packages.

#### 11.5 Organizational Dependencies

Identify:

* Owning teams.
* External platform teams.
* Shared-service owners.
* Release coordination.
* Approval requirements.

#### 11.6 Dependency Direction and Criticality

Classify:

* Required versus optional.
* Synchronous versus asynchronous.
* Compile-time versus runtime.
* Internal versus external.
* Stable versus volatile.
* Owned versus third-party.

### Alpha Car Detailing Application

The booking service depends on:

* Corporate account validation.
* Station capacity.
* Pricing rules.
* Identity claims.
* Event publication.
* Shared contract packages.
* Operational infrastructure.

### Recommended Diagram

**Figure 5-7 — Fleet-Booking Dependency Map**

### Recommended Table

**Table 5-7 — Dependency Inventory**

Columns:

* Dependency.
* Type.
* Direction.
* Owner.
* Evidence.
* Criticality.
* Failure impact.

---

## 12. Architecture Discussion — Build and Package Discovery

### Purpose

Establish how the repository is restored, compiled, tested, packaged, and released.

### Key Concepts

* A successful local build is not equivalent to a production build.
* Build conventions may be centralized.
* Package publication may create external compatibility obligations.
* Generated source can alter the effective codebase.

### Important Subsections

#### 12.1 Build Toolchain

Discover:

* SDK versions.
* Tool manifests.
* Build scripts.
* Shared targets.
* Environment prerequisites.
* Code generation.
* Native dependencies.

#### 12.2 Restore and Package Sources

Discover:

* Public feeds.
* Private feeds.
* Authentication mechanisms.
* Package pinning.
* Package lock files.
* Internal versioning.

#### 12.3 Build Variants

Inspect:

* Debug.
* Release.
* CI.
* Container.
* Platform-specific.
* Feature-specific builds.

#### 12.4 Packaging

Identify:

* NuGet packages.
* Container images.
* Deployment bundles.
* Helm charts.
* Generated SDKs.
* Database migration packages.

#### 12.5 Build Validation

Determine:

* Required commands.
* Static analysis.
* Formatting.
* Security scanning.
* Architecture tests.
* Container scanning.
* Quality gates.

### Alpha Car Detailing Application

The agent identifies the exact commands and pipelines required to build and validate changes to the booking capability and its contracts.

### Recommended Table

**Table 5-8 — Build and Packaging Inventory**

---

## 13. Architecture Discussion — Configuration Discovery

### Purpose

Explain how runtime behavior is shaped by layered configuration.

### Key Concepts

* Configuration values may be distributed across code, environment files, secret stores, manifests, and deployment systems.
* Default values may differ from deployed values.
* Sensitive values must not be copied into discovery reports.
* Configuration keys can reveal hidden dependencies.

### Important Subsections

#### 13.1 Configuration Sources

Inspect:

* `appsettings` files.
* Environment variables.
* Kubernetes ConfigMaps.
* Helm values.
* Terraform variables.
* Feature flags.
* Key Vault references.
* Secret references.
* Command-line parameters.
* Database configuration tables.

#### 13.2 Configuration Precedence

Determine:

* Base configuration.
* Environment overrides.
* Deployment-time injection.
* Runtime refresh.
* Secret resolution.
* Local-development overrides.

#### 13.3 Configuration Ownership

Identify:

* Application-owned settings.
* Platform-owned settings.
* Environment-owned settings.
* Security-owned settings.
* Tenant-owned settings.

#### 13.4 Configuration Drift

Detect:

* Unused keys.
* Multiple names for the same setting.
* Defaults inconsistent with production manifests.
* Documentation showing retired values.
* Environment overlays missing required keys.

#### 13.5 Sensitive Configuration

Explain safe handling:

* Record key names, not secret values.
* Record secret-store references, not credentials.
* Avoid loading production secrets unless authorized.
* Redact connection details.
* Preserve access-control boundaries.

### Alpha Car Detailing Application

Discover configuration for:

* Corporate booking limits.
* Capacity-service endpoint.
* Event topic names.
* Authentication authority.
* Booking database.
* Feature flags.
* Retry policies.
* Station-region routing.

### Recommended Table

**Table 5-9 — Configuration Inventory and Ownership**

---

## 14. Architecture Discussion — Data Storage and Persistence Discovery

### Purpose

Determine which components own data and how persistence behavior affects the requested change.

### Key Concepts

* Database presence does not establish ownership by itself.
* Data models, repositories, migrations, and runtime configuration must be examined together.
* Cross-service reads may be intentional, transitional, or violations.
* Persistence behavior includes consistency, concurrency, migration, and retention.

### Important Subsections

#### 14.1 Storage Technologies

Identify:

* Relational databases.
* Document databases.
* Caches.
* Search indexes.
* Object storage.
* Event stores.
* Analytics stores.

#### 14.2 Data Ownership

Determine:

* Owning service.
* Owning aggregate.
* Authoritative source.
* Read replicas.
* Projection stores.
* Shared schemas.
* Legacy stores.

#### 14.3 Persistence Implementation

Inspect:

* ORM contexts.
* Repositories.
* Unit-of-work patterns.
* Direct SQL.
* Stored procedures.
* Migration tools.
* Schema scripts.
* Connection registration.

#### 14.4 Consistency and Transactions

Discover:

* Local transactions.
* Distributed workflows.
* Outbox pattern.
* Inbox or idempotency handling.
* Saga coordination.
* Eventual consistency.
* Concurrency tokens.

#### 14.5 Data Lifecycle

Inspect:

* Retention.
* Archival.
* Soft deletion.
* Auditing.
* Encryption.
* Data residency.
* Backup expectations.

### Alpha Car Detailing Application

Determine:

* Where fleet booking records are stored.
* Whether vehicle lists are embedded or referenced.
* Where corporate account entitlements reside.
* How station capacity is updated.
* Whether booking events are emitted through an outbox.
* Whether read models exist for station operations.

### Recommended Diagram

**Figure 5-8 — Alpha Car Detailing Data Ownership Map**

### Recommended Table

**Table 5-10 — Data Store and Ownership Inventory**

---

## 15. Architecture Discussion — API and Contract Discovery

### Purpose

Identify synchronous interfaces and compatibility obligations.

### Key Concepts

* Controllers are not the full API contract.
* Contracts may be generated, versioned, published, or duplicated.
* Runtime behavior may differ from specifications.
* Consumers determine compatibility risk.

### Important Subsections

#### 15.1 API Surface Discovery

Inspect:

* REST endpoints.
* GraphQL schemas.
* gRPC services.
* Internal service interfaces.
* Administrative endpoints.
* Health endpoints.

#### 15.2 Contract Sources

Identify:

* OpenAPI documents.
* Protobuf files.
* Shared DTO packages.
* Generated clients.
* Schema repositories.
* Consumer examples.
* Contract tests.

#### 15.3 Versioning

Determine:

* URL versioning.
* Header versioning.
* Package versioning.
* Schema versioning.
* Compatibility policy.
* Deprecation policy.

#### 15.4 Consumer Discovery

Identify:

* Internal callers.
* External clients.
* Frontend applications.
* Mobile clients.
* Partner integrations.
* Automated jobs.

#### 15.5 Contract Drift

Detect:

* Endpoint behavior not reflected in OpenAPI.
* DTOs duplicated across services.
* Generated clients behind the server version.
* Deprecated fields still in use.
* Documentation referencing removed endpoints.

### Alpha Car Detailing Application

Discover whether corporate fleet booking should use:

* An existing booking API.
* A new endpoint in a corporate account API.
* A GraphQL mutation.
* A gRPC internal call.
* A workflow command sent through messaging.

### Recommended Table

**Table 5-11 — API and Contract Inventory**

---

## 16. Architecture Discussion — Event and Messaging Discovery

### Purpose

Map asynchronous integration, ownership, delivery guarantees, and schema responsibilities.

### Key Concepts

* An event name alone does not reveal semantic ownership.
* Producers, consumers, brokers, schemas, retries, and idempotency must be traced together.
* Commands and events must not be conflated.
* Topic and queue configuration may exist outside application code.

### Important Subsections

#### 16.1 Messaging Platforms

Identify:

* Azure Event Hubs.
* Azure Service Bus.
* Kafka.
* RabbitMQ.
* Cloud queues.
* Internal event buses.

#### 16.2 Producer Discovery

Inspect:

* Publication code.
* Outbox implementation.
* Serialization.
* Schema validation.
* Topic selection.
* Correlation identifiers.

#### 16.3 Consumer Discovery

Inspect:

* Handlers.
* Consumer groups.
* Subscription filters.
* Retry logic.
* Dead-letter handling.
* Idempotency.
* Checkpointing.

#### 16.4 Contract Discovery

Identify:

* Event names.
* Schema versions.
* Registry references.
* Envelope formats.
* Required metadata.
* Compatibility rules.

#### 16.5 Operational Messaging Configuration

Inspect:

* Topic creation.
* Partitioning.
* Retention.
* Consumer groups.
* Access policies.
* Monitoring alerts.

#### 16.6 Event Semantics

Differentiate:

* Domain event.
* Integration event.
* Command.
* Notification.
* Change-data event.
* Operational event.

### Alpha Car Detailing Application

Determine:

* Whether booking confirmation is synchronous.
* Whether station capacity is reserved through a command or API.
* Which event informs station operations.
* Whether pricing changes are event-driven.
* How duplicate booking events are handled.

### Recommended Diagram

**Figure 5-9 — Corporate Fleet Booking Event Flow**

### Recommended Table

**Table 5-12 — Event and Messaging Inventory**

---

## 17. Architecture Discussion — Test Architecture Discovery

### Purpose

Identify how the repository proves correctness and where validation gaps exist.

### Key Concepts

* Tests reveal expected behavior, architecture, and conventions.
* Test names may be more current than documentation, but they are still evidence rather than absolute truth.
* Missing tests are discovery findings.
* Test execution paths must be understood before code changes.

### Important Subsections

#### 17.1 Test Types

Identify:

* Unit tests.
* Integration tests.
* Contract tests.
* Architecture tests.
* Component tests.
* End-to-end tests.
* Security tests.
* Performance tests.
* Resilience tests.
* Deployment tests.

#### 17.2 Test Organization

Discover:

* Test project naming.
* Fixture structure.
* Shared test libraries.
* Test data builders.
* Containerized dependencies.
* Environment requirements.
* Test categories.

#### 17.3 Test-to-Production Traceability

Map tests to:

* Services.
* Endpoints.
* Events.
* Persistence.
* Authorization policies.
* Deployment units.

#### 17.4 Validation Scripts and Quality Gates

Inspect:

* Linting.
* Formatting.
* Static analysis.
* Security scanning.
* Mutation testing.
* Coverage thresholds.
* Harness evaluation scripts.

#### 17.5 Test Gaps

Record:

* Critical flows without coverage.
* Contract behavior without tests.
* Environment-dependent tests.
* Disabled or quarantined tests.
* Tests inconsistent with current code.

### Alpha Car Detailing Application

The agent identifies tests for:

* Corporate account authorization.
* Fleet size validation.
* Capacity reservation.
* Pricing.
* Duplicate booking prevention.
* Event publication.
* End-to-end booking flow.

### Recommended Table

**Table 5-13 — Test Inventory and Coverage Gaps**

---

## 18. Architecture Discussion — Security and Identity Discovery

### Purpose

Determine authentication, authorization, trust boundaries, secret handling, and security-sensitive assets.

### Key Concepts

* Identity behavior is distributed across code and infrastructure.
* Authentication does not establish authorization.
* Service-to-service identity may differ from user identity.
* Discovery must respect least privilege.

### Important Subsections

#### 18.1 Authentication Discovery

Inspect:

* Identity provider.
* Token validation.
* JWT claims.
* API gateway authentication.
* Managed identity.
* Workload identity.
* Client credentials.
* Certificate authentication.

#### 18.2 Authorization Discovery

Inspect:

* Roles.
* Claims.
* Policies.
* Permission services.
* Resource-based authorization.
* Tenant boundaries.
* Corporate-account ownership rules.

#### 18.3 Trust Boundaries

Map:

* Public endpoints.
* Internal endpoints.
* Administrative endpoints.
* Service-to-service calls.
* External integrations.
* Data boundaries.
* Cluster boundaries.

#### 18.4 Secret Management

Identify:

* Secret references.
* Key Vault integration.
* Kubernetes secrets.
* Local-development secret storage.
* Pipeline credentials.
* Certificate handling.

#### 18.5 Security Controls

Inspect:

* Input validation.
* Rate limiting.
* Audit logging.
* Encryption.
* Dependency scanning.
* Container scanning.
* Admission policies.
* Network policies.

#### 18.6 Sensitive Files and Restricted Discovery

Establish rules for:

* Secret files.
* Production configuration.
* Personal data.
* Certificates.
* Private keys.
* Credential caches.
* Security incident records.
* Proprietary customer data.

### Alpha Car Detailing Application

Determine whether only authorized corporate fleet managers can create, change, or cancel bookings and how station personnel are restricted to operational views.

### Recommended Diagram

**Figure 5-10 — Fleet Booking Security Boundary Map**

### Recommended Table

**Table 5-14 — Identity and Authorization Evidence**

---

## 19. Architecture Discussion — Observability Discovery

### Purpose

Understand how the system records, correlates, measures, and alerts on runtime behavior.

### Key Concepts

* Observability configuration reveals operational expectations.
* Logs, metrics, and traces should be mapped to business flows.
* Correlation conventions are part of repository behavior.
* Missing telemetry may affect feature design.

### Important Subsections

#### 19.1 Logging

Inspect:

* Logging framework.
* Structured logging.
* Event identifiers.
* Sensitive-data filtering.
* Log levels.
* Correlation fields.

#### 19.2 Metrics

Inspect:

* Business metrics.
* Technical metrics.
* Custom meters.
* Prometheus exporters.
* Dashboard definitions.
* Alert thresholds.

#### 19.3 Distributed Tracing

Inspect:

* OpenTelemetry setup.
* Trace propagation.
* Activity sources.
* External dependency instrumentation.
* Messaging instrumentation.

#### 19.4 Health and Readiness

Inspect:

* Liveness checks.
* Readiness checks.
* Dependency checks.
* Startup probes.
* Operational dashboards.

#### 19.5 Alerting and Incident Evidence

Inspect:

* Alert rules.
* Runbooks.
* Known failure modes.
* SLOs.
* Incident notes.
* Error-budget policies.

### Alpha Car Detailing Application

Determine how a fleet booking is traced across:

* Portal request.
* Booking API.
* Corporate account validation.
* Capacity reservation.
* Event publication.
* Station consumer.
* Notification flow.

### Recommended Table

**Table 5-15 — Observability Inventory**

---

## 20. Architecture Discussion — CI/CD and Infrastructure Discovery

### Purpose

Establish how code becomes a deployed enterprise system.

### Key Concepts

* Pipelines encode policy.
* Infrastructure repositories may contain critical runtime truth.
* Deployment ownership may differ from source ownership.
* Environment promotion and rollback behavior affect implementation decisions.

### Important Subsections

#### 20.1 Continuous Integration

Inspect:

* Trigger rules.
* Build matrix.
* Test stages.
* Code-quality gates.
* Security scanning.
* Package publication.
* Container builds.
* Artifact retention.

#### 20.2 Continuous Delivery and Deployment

Inspect:

* Environment promotion.
* Approval gates.
* Deployment strategies.
* Rollback mechanisms.
* Database migration order.
* Feature-flag activation.
* Regional deployment.

#### 20.3 Infrastructure as Code

Inspect:

* Terraform.
* Bicep.
* ARM templates.
* CloudFormation.
* Helm.
* Kubernetes manifests.
* Service mesh configuration.
* Network policy.

#### 20.4 Runtime Resources

Discover:

* Databases.
* Brokers.
* Caches.
* Identity resources.
* Secret stores.
* Storage accounts.
* Monitoring resources.
* DNS and ingress.

#### 20.5 Pipeline-to-Source Traceability

Determine:

* Which pipeline builds which project.
* Which artifact produces which deployment.
* Which environment variables are injected.
* Which tests block release.
* Which branches release to which environments.

### Alpha Car Detailing Application

Trace the booking service from source path to:

* Build workflow.
* Container image.
* Registry.
* Helm chart.
* Kubernetes deployment.
* Configuration.
* Database migration.
* Production approval.

### Recommended Diagram

**Figure 5-11 — Source-to-Deployment Traceability**

### Recommended Table

**Table 5-16 — CI/CD and Deployment Inventory**

---

## 21. Architecture Discussion — Documentation and Architecture Decision Discovery

### Purpose

Teach readers to locate, evaluate, and validate written architectural guidance.

### Key Concepts

* Documentation is evidence with a timestamp and scope.
* ADRs explain decisions, but implementation may have drifted.
* Diagrams may describe intended architecture rather than deployed architecture.
* Contradictions must be recorded, not silently resolved by the AI agent.

### Important Subsections

#### 21.1 Documentation Sources

Identify:

* READMEs.
* Architecture overviews.
* Service documentation.
* Runbooks.
* API documentation.
* Data dictionaries.
* Operational guides.
* Migration plans.
* Threat models.
* Diagrams.

#### 21.2 Architecture Decision Records

Inspect:

* Decision.
* Context.
* Status.
* Date.
* Superseding ADRs.
* Scope.
* Consequences.
* Implementation references.

#### 21.3 Documentation Authority

Classify documents as:

* Normative.
* Informative.
* Historical.
* Proposed.
* Deprecated.
* Unverified.

#### 21.4 Documentation Drift

Detect:

* Removed services still shown.
* Old package versions.
* Superseded patterns.
* Incorrect ownership.
* Obsolete deployment topology.
* Incomplete migration status.

### Alpha Car Detailing Application

The discovery finds:

* An ADR stating capacity checks are synchronous.
* Current code publishing a capacity reservation command.
* A diagram still showing a retired `FleetManagementService`.

This becomes a formal contradiction requiring resolution.

### Recommended Table

**Table 5-17 — Documentation Authority and Freshness Assessment**

---

## 22. Architecture Discussion — Existing AI Instructions and Agent Assets

### Purpose

Establish AI-specific repository assets as first-class discovery sources.

### Key Concepts

* AI agents must discover applicable instructions before generating changes.
* Instruction scope may vary by repository, directory, project, or task.
* Skills describe recurring workflows.
* Prompts describe tasks.
* Roles define responsibilities.
* Steering notes describe current priorities and constraints.
* Harness files define execution and validation behavior.

### Important Subsections

#### 22.1 Instruction Discovery

Inspect:

* `CLAUDE.md`
* `AGENTS.md`
* `.github/copilot-instructions.md`
* Directory-level instructions.
* Coding standards.
* Security requirements.
* Testing requirements.
* Architecture constraints.

#### 22.2 Skill Discovery

Identify reusable workflows for:

* API changes.
* Event publication.
* Database migrations.
* Test generation.
* Security reviews.
* Deployment validation.

#### 22.3 Prompt Library Discovery

Inspect:

* Approved prompts.
* Prompt versions.
* Task templates.
* Review prompts.
* Validation prompts.

#### 22.4 Role Discovery

Identify:

* Lead.
* Developer.
* Reviewer.
* Validator.
* Evaluator.
* Architect.

#### 22.5 Steering-Note Discovery

Review:

* Current sprint priorities.
* Active constraints.
* Acceptance criteria.
* Known exclusions.
* Temporary architectural direction.

#### 22.6 Harness Discovery

Inspect:

* Execution scripts.
* Tool permissions.
* Validation steps.
* Evaluation criteria.
* Pull-request automation.
* Metrics.
* Knowledge caches.
* Human approval gates.

#### 22.7 Precedence and Conflict

Determine what happens when:

* Root instructions conflict with directory instructions.
* A skill conflicts with current architecture.
* A prompt requests prohibited behavior.
* A steering note conflicts with a published standard.
* Harness validation differs from documentation.

### Alpha Car Detailing Application

Before modifying booking code, the agent discovers:

* Root architecture instructions.
* Booking-service-specific conventions.
* An event-publication skill.
* A corporate-feature prompt template.
* A steering note prohibiting direct cross-service database access.
* Harness validation requiring architecture and contract tests.

### Recommended Diagram

**Figure 5-12 — AI Guidance Precedence during Discovery**

### Recommended Table

**Table 5-18 — AI Asset Inventory**

---

## 23. Concepts — Identifying Conventions from Evidence

### Purpose

Explain how repository conventions are derived without overgeneralizing from isolated examples.

### Key Concepts

* A repeated pattern is not automatically a mandated convention.
* Conventions require multiple supporting examples or explicit documentation.
* Exceptions must be identified.
* The agent should record confidence and scope.

### Important Subsections

#### 23.1 Sources of Convention Evidence

* Explicit instructions.
* Shared templates.
* Repeated implementation patterns.
* Architecture tests.
* Static-analysis rules.
* Code-review checklists.
* CI validation.
* Recent pull requests.
* Approved skills.

#### 23.2 Convention Categories

* Naming.
* Project structure.
* Dependency registration.
* Error handling.
* API responses.
* Logging.
* Events.
* Persistence.
* Testing.
* Security.
* Configuration.
* Deployment.

#### 23.3 Scope of a Convention

Determine whether a convention is:

* Repository-wide.
* Language-specific.
* Service-specific.
* Layer-specific.
* Legacy-only.
* New-development-only.

#### 23.4 Convention Confidence

Examples:

* Explicitly mandated.
* Strongly evidenced.
* Common but inconsistent.
* Historical.
* Unknown.

### Alpha Car Detailing Application

The agent identifies that:

* New integration events use a versioned envelope.
* Older services publish unversioned payloads.
* Booking belongs to the newer convention because its service already uses the versioned envelope.

### Recommended Table

**Table 5-19 — Repository Convention Summary**

---

## 24. Concepts — Detecting Contradictions and Stale Documentation

### Purpose

Provide a disciplined method for handling conflicting evidence.

### Key Concepts

* Contradictions are discovery outputs, not inconveniences to hide.
* Source recency alone does not establish authority.
* Runtime evidence may outweigh documentation, but architectural intent may still require confirmation.
* AI agents must not silently select the most convenient interpretation.

### Important Subsections

#### 24.1 Common Contradiction Types

* Documentation versus code.
* Code versus tests.
* Tests versus contracts.
* Contracts versus deployed configuration.
* Instructions versus implementation.
* ADR versus current runtime.
* Pipeline versus local build instructions.
* Service ownership versus database access.

#### 24.2 Evaluating Contradictions

Consider:

* Source authority.
* Recency.
* Runtime usage.
* Build references.
* Deployment references.
* Ownership.
* Explicit supersession.
* Current branch.
* Release status.

#### 24.3 Contradiction Resolution Outcomes

* Resolved through evidence.
* Resolved through owner confirmation.
* Accepted as transitional.
* Recorded as technical debt.
* Blocked pending decision.
* Declared out of scope with risk noted.

#### 24.4 Contradiction Log

Required fields:

* Identifier.
* Conflicting claims.
* Evidence sources.
* Impact.
* Current interpretation.
* Owner.
* Resolution status.

### Alpha Car Detailing Application

The booking-capacity interaction is documented as synchronous but implemented asynchronously. The agent records the conflict instead of designing around either model prematurely.

### Recommended Table

**Table 5-20 — Contradiction Log Example**

---

## 25. Concepts — Facts, Inferences, Contradictions, and Unknowns

### Purpose

Introduce the required discovery evidence-classification framework.

### Key Concepts

AI agents must never present inferred architecture as verified fact.

### Important Subsections

#### 25.1 Verified Fact

Definition:

A finding directly supported by authoritative, inspectable evidence.

Examples:

* `Booking.Api` references `Booking.Application`.
* The Kubernetes manifest deploys image `booking-api`.
* The booking service registers an `IBookingRepository`.
* A pipeline runs the booking integration-test project.

Required attributes:

* Evidence source.
* Location.
* Date or version where relevant.
* Scope.

#### 25.2 Strong Inference

Definition:

A conclusion supported by multiple consistent pieces of evidence but not explicitly verified.

Example:

The booking service likely owns booking data because it contains migrations, repository implementations, and deployment configuration for the booking database.

#### 25.3 Weak Inference

Definition:

A tentative conclusion based on limited, indirect, or ambiguous evidence.

Example:

A directory named `Fleet` may represent the corporate fleet bounded context.

#### 25.4 Contradiction

Definition:

Two or more credible evidence sources make incompatible claims.

#### 25.5 Unknown

Definition:

A material question cannot be answered with available evidence.

Example:

The production consumer of `FleetBookingCreated` is defined in an inaccessible repository.

#### 25.6 Out-of-Scope Item

Definition:

A discovered item is relevant to the broader system but intentionally excluded from the current discovery scope.

#### 25.7 Evidence Provenance

Each finding should capture:

* Source.
* Path or asset.
* Evidence type.
* Classification.
* Confidence.
* Scope.
* Reviewer.
* Last validated date.

### Alpha Car Detailing Application

Classify findings about:

* Booking ownership.
* Corporate-account integration.
* Capacity reservation.
* Event publication.
* Authorization.
* Deployment.

### Recommended Diagram

**Figure 5-13 — Discovery Evidence Model**

A spectrum from verified fact to unknown, with contradiction as a separate conflict state.

### Recommended Table

**Table 5-21 — Evidence Classification Examples**

---

## 26. Concepts — Progressive Repository Exploration

### Purpose

Explain how discovery moves from broad orientation to focused technical tracing.

### Key Concepts

* Discovery should begin broadly enough to avoid tunnel vision.
* Exploration becomes progressively deeper as relevance and risk increase.
* Full-repository reading is rarely efficient or necessary.
* Evidence should determine the next exploration step.

### Important Subsections

#### 26.1 Level 1 — Orientation

Examine:

* Root files.
* Repository topology.
* Solutions and workspaces.
* Instructions.
* Build entry points.
* Documentation index.

#### 26.2 Level 2 — Structural Mapping

Examine:

* Projects.
* Services.
* Modules.
* Deployment units.
* Contracts.
* Tests.
* Infrastructure assets.

#### 26.3 Level 3 — Feature Tracing

Trace:

* Entry point.
* Application workflow.
* Domain logic.
* Persistence.
* External calls.
* Events.
* Tests.
* Deployment.

#### 26.4 Level 4 — Risk-Focused Deep Dive

Deeply inspect:

* Security.
* Data consistency.
* Contract compatibility.
* Concurrency.
* Migration.
* Performance.
* Operational recovery.

#### 26.5 Level 5 — Enterprise Dependency Expansion

Expand into:

* Other repositories.
* Shared platforms.
* External teams.
* Central schema registries.
* Identity platforms.
* Enterprise pipelines.

### Alpha Car Detailing Application

The agent begins with repository orientation, then narrows into booking, corporate accounts, station capacity, identity, contracts, and deployment.

### Recommended Diagram

**Figure 5-14 — Repository Discovery Funnel**

```text
Repository Orientation
        ↓
Architecture Mapping
        ↓
Feature-Relevant Assets
        ↓
Critical Runtime Paths
        ↓
Risk-Specific Deep Inspection
```

---

## 27. Concepts — Discovery Depth Based on Task Risk

### Purpose

Provide a practical model for scaling discovery effort.

### Key Concepts

Discovery depth should be proportional to potential impact, irreversibility, coupling, and uncertainty.

### Important Subsections

#### 27.1 Risk Factors

Assess:

* Number of services affected.
* Data migration.
* Security impact.
* Public contract change.
* Event-schema change.
* Financial impact.
* Regulatory impact.
* Deployment complexity.
* Operational criticality.
* Architectural uncertainty.
* Availability requirements.
* Rollback difficulty.

#### 27.2 Low-Risk Discovery

Appropriate for:

* Isolated documentation correction.
* Localized test improvement.
* Nonfunctional internal refactoring with strong test coverage.

Required depth:

* Local instructions.
* Direct dependencies.
* Relevant tests.
* Build validation.

#### 27.3 Medium-Risk Discovery

Appropriate for:

* New internal API.
* New background job.
* Local schema extension.
* Service-level behavior change.

Required depth:

* Service architecture.
* Persistence.
* contracts.
* security.
* deployment.
* operational validation.

#### 27.4 High-Risk Discovery

Appropriate for:

* Cross-service workflow.
* Public API change.
* Event-contract change.
* Identity change.
* Data migration.
* Shared platform change.

Required depth:

* Enterprise dependency mapping.
* Multi-repository discovery.
* Contract consumers.
* threat boundaries.
* rollout and rollback.
* owner confirmation.

#### 27.5 Critical-Risk Discovery

Appropriate for:

* Payment.
* safety.
* compliance.
* high-volume core workflows.
* irreversible migration.
* production identity infrastructure.

Required depth:

* Formal architectural review.
* security review.
* operational validation.
* explicit human approval.
* documented residual risk.

### Alpha Car Detailing Application

Corporate fleet booking is classified as high risk because it crosses services, authorization, data ownership, pricing, capacity, messaging, and deployment.

### Recommended Diagram

**Figure 5-15 — Task Risk versus Discovery Depth Matrix**

### Recommended Table

**Table 5-22 — Risk Classification Criteria**

---

## 28. Concepts — Local, Service-Level, and Enterprise-Level Discovery

### Purpose

Differentiate discovery scope by system level.

### Key Concepts

* Local discovery answers implementation questions.
* Service-level discovery answers ownership and runtime questions.
* Enterprise-level discovery answers cross-system and governance questions.
* The levels should not be confused.

### Important Subsections

#### 28.1 Local Discovery

Focus:

* One class.
* One module.
* One feature path.
* Direct tests.
* Local conventions.

#### 28.2 Service-Level Discovery

Focus:

* Service boundaries.
* Data ownership.
* APIs.
* Events.
* deployment.
* observability.
* security.
* service tests.

#### 28.3 Enterprise-Level Discovery

Focus:

* Cross-repository dependencies.
* Shared platforms.
* Identity.
* governance.
* central contracts.
* organizational ownership.
* release coordination.
* compliance.

#### 28.4 Escalation between Levels

Explain signals that require expanding discovery:

* Cross-service call discovered.
* Shared database.
* external contract.
* central identity policy.
* enterprise event.
* shared deployment pipeline.
* unresolved ownership.

### Alpha Car Detailing Application

The task begins as a service-level booking change but escalates to enterprise-level discovery when corporate identity, station capacity, and event consumers are identified.

### Recommended Table

**Table 5-23 — Discovery Level Comparison**

---

## 29. Architecture Discussion — Large Monorepository Discovery

### Purpose

Address scale, performance, ownership, and context-management challenges in monorepositories.

### Key Concepts

* Large repositories require indexing, partitioning, prioritization, and evidence caching.
* Root-level conventions may coexist with domain-specific conventions.
* Global search can create irrelevant-context overload.
* Build graphs are often more useful than raw file trees.

### Important Subsections

#### 29.1 Monorepository Orientation

Identify:

* Workspace definition.
* Build graph.
* Ownership files.
* Domain directories.
* Shared platforms.
* global tooling.
* generated assets.

#### 29.2 Scope Reduction

Use:

* Affected path analysis.
* dependency graph.
* ownership metadata.
* build targets.
* code references.
* runtime manifests.
* change history.

#### 29.3 Instruction Precedence

Determine:

* Root instructions.
* directory instructions.
* language-specific instructions.
* team-specific conventions.
* exceptions.

#### 29.4 Partial Builds and Tests

Identify:

* Targeted build commands.
* affected test execution.
* package-level validation.
* dependency-aware CI.

#### 29.5 Monorepository Failure Modes

* Treating root conventions as universal.
* Loading excessive context.
* Missing shared package consumers.
* Ignoring generated dependency graphs.
* Assuming directory boundaries equal deployment boundaries.

### Alpha Car Detailing Application

A monorepository contains all business services, contracts, infrastructure, and harness assets. Discovery narrows from the global repository into booking and its dependency graph.

### Recommended Diagram

**Figure 5-16 — Monorepository Discovery Layers**

---

## 30. Architecture Discussion — Multi-Repository System Discovery

### Purpose

Explain how to discover systems whose architecture spans independently versioned repositories.

### Key Concepts

* A single repository may not contain the full implementation truth.
* Contracts, infrastructure, consumers, and documentation may live elsewhere.
* Access limitations must be recorded as unknowns.
* Repository relationships require explicit mapping.

### Important Subsections

#### 30.1 Repository Categories

Identify:

* Service repositories.
* Contract repositories.
* Shared library repositories.
* Infrastructure repositories.
* deployment repositories.
* documentation repositories.
* frontend repositories.
* harness repositories.

#### 30.2 Cross-Repository Entry Points

Use:

* Package references.
* Git submodules.
* pipeline references.
* image names.
* schema identifiers.
* URLs.
* repository links.
* ownership metadata.

#### 30.3 Version Alignment

Determine:

* Contract version.
* package version.
* deployed version.
* branch alignment.
* release cadence.
* compatibility window.

#### 30.4 Access and Ownership

Record:

* Repository owner.
* access availability.
* required approvals.
* inaccessible evidence.
* external dependencies.

#### 30.5 Multi-Repository Discovery Report

Include:

* Repository relationship map.
* version map.
* ownership map.
* unresolved external dependencies.
* coordination requirements.

### Alpha Car Detailing Application

The booking code is in one repository, event contracts in another, infrastructure in a platform repository, and corporate portal code in a frontend repository.

### Recommended Diagram

**Figure 5-17 — Multi-Repository System Map**

### Recommended Table

**Table 5-24 — Repository Relationship Inventory**

---

## 31. Architecture Discussion — Discovery Outputs and Repository Maps

### Purpose

Define the practical artifacts produced by discovery.

### Key Concepts

Discovery outputs should be reusable, reviewable, traceable, and maintainable.

### Important Subsections

#### 31.1 Repository Topology Map

Shows:

* Major directories.
* Asset categories.
* active versus historical areas.
* repository instructions.
* build and deployment areas.

#### 31.2 Project and Service Inventory

Includes:

* Name.
* type.
* responsibility.
* owner.
* runtime status.
* deployment unit.
* primary interfaces.

#### 31.3 Dependency Map

Shows:

* Build dependencies.
* runtime dependencies.
* contract dependencies.
* data dependencies.
* organizational dependencies.

#### 31.4 Runtime Flow Map

Shows:

* Entry point.
* calls.
* messages.
* persistence.
* external systems.
* failure boundaries.

#### 31.5 Data Ownership Map

Shows:

* Data domain.
* owner.
* authoritative store.
* projections.
* consumers.
* consistency model.

#### 31.6 API and Event Contract Inventory

Includes:

* Contract name.
* owner.
* version.
* producer.
* consumers.
* compatibility expectations.

#### 31.7 Configuration Inventory

Includes:

* Key.
* source.
* owner.
* sensitivity.
* environment scope.
* default behavior.

#### 31.8 Test Inventory

Includes:

* Test type.
* project.
* target.
* execution command.
* environment dependency.
* known gap.

#### 31.9 CI/CD and Deployment Inventory

Includes:

* Pipeline.
* artifact.
* environment.
* approval.
* deployment unit.
* rollback path.

#### 31.10 Security Boundary Map

Includes:

* Trust zone.
* identity.
* authorization.
* secret source.
* external exposure.
* data classification.

#### 31.11 Repository Convention Summary

Includes:

* Convention.
* scope.
* evidence.
* exceptions.
* confidence.

#### 31.12 Contradiction Log

Records unresolved conflicts.

#### 31.13 Open-Question Register

Records:

* Question.
* impact.
* owner.
* priority.
* required evidence.

#### 31.14 Discovery Confidence Assessment

Summarizes readiness to proceed.

#### 31.15 Recommended Next Exploration Steps

Prioritizes remaining investigation.

### Alpha Car Detailing Application

All outputs are produced for the fleet-booking scenario and consolidated into the final discovery report.

### Recommended Diagram

**Figure 5-18 — Discovery Output Portfolio**

---

## 32. Hands-On Example — Alpha Car Detailing Repository Walkthrough

### Purpose

Provide an end-to-end professional walkthrough using the chapter’s structured model.

### Key Concepts

The walkthrough should demonstrate disciplined evidence collection rather than jump directly into code.

### Important Subsections

#### 32.1 Step 1 — Define the Fleet-Booking Task

Task statement:

Add support for corporate fleet managers to create multi-vehicle bookings at stations with available service capacity.

Initial impact hypotheses:

* Booking.
* Corporate accounts.
* Station operations.
* Pricing.
* Identity.
* Messaging.
* Notifications.
* Reporting.

Classify all as hypotheses, not facts.

#### 32.2 Step 2 — Review Repository Entry Points

Inspect:

* Repository README.
* Solution files.
* Build scripts.
* AI instructions.
* architecture documentation.
* deployment directories.
* service catalogue.

#### 32.3 Step 3 — Build the Topology Map

Identify:

* Booking service.
* Corporate account service.
* Station service.
* pricing service.
* identity integration.
* shared contracts.
* test projects.
* infrastructure.
* pipelines.
* harness assets.

#### 32.4 Step 4 — Determine Booking Ownership

Evidence:

* Booking domain model.
* booking database migrations.
* repository implementation.
* API deployment.
* service documentation.

Classification:

* Verified facts.
* Strong inference about aggregate ownership.
* Unknowns requiring confirmation.

#### 32.5 Step 5 — Determine Corporate Account Ownership

Trace:

* Corporate account API.
* organization identifiers.
* fleet-manager roles.
* contract package.
* client registration.

#### 32.6 Step 6 — Discover Station Capacity Representation

Investigate:

* Capacity domain model.
* schedule tables.
* station availability API.
* reservation event.
* caching.
* concurrency handling.

#### 32.7 Step 7 — Trace Communication Style

Determine whether booking uses:

* Direct HTTP.
* gRPC.
* commands.
* integration events.
* orchestrated workflow.
* saga.

#### 32.8 Step 8 — Locate Contracts

Find:

* Booking request DTO.
* corporate account client.
* capacity contract.
* booking-created event.
* schema version.
* generated clients.

#### 32.9 Step 9 — Trace Authentication and Authorization

Determine:

* Identity provider.
* corporate fleet-manager claim.
* account-scoped authorization.
* service-to-service authentication.
* audit requirements.

#### 32.10 Step 10 — Discover Persistence

Determine:

* Booking aggregate.
* fleet vehicle representation.
* database ownership.
* transaction handling.
* outbox behavior.
* migration process.

#### 32.11 Step 11 — Discover Tests

Locate:

* Booking domain tests.
* corporate authorization tests.
* capacity integration tests.
* event contract tests.
* end-to-end tests.
* architecture tests.

#### 32.12 Step 12 — Discover Deployment

Trace:

* Build pipeline.
* container.
* deployment manifest.
* configuration.
* secrets.
* database migration.
* production approval.

#### 32.13 Step 13 — Review AI Guidance

Read:

* Root instructions.
* service instructions.
* relevant skills.
* prompt templates.
* steering note.
* harness validation.

#### 32.14 Step 14 — Detect Documentation Drift

Compare:

* ADR.
* code.
* deployment.
* tests.
* diagrams.
* current event flow.

#### 32.15 Step 15 — Produce Findings

Prepare:

* Topology map.
* service inventory.
* runtime flow.
* dependency map.
* data ownership map.
* contract inventory.
* test inventory.
* contradiction log.
* open questions.
* confidence assessment.

#### 32.16 Step 16 — Decide Whether to Proceed

Possible conclusion:

Discovery is sufficient for architectural planning but not implementation because production consumers of the booking event remain unverified.

### Recommended Diagrams

* **Figure 5-19 — Alpha Car Detailing Repository Map**
* **Figure 5-20 — Corporate Fleet Booking Runtime Flow**
* **Figure 5-21 — Corporate Fleet Booking Evidence Register**

### Recommended Tables

* **Table 5-25 — Fleet Booking Service Inventory**
* **Table 5-26 — Fleet Booking Open Questions**
* **Table 5-27 — Fleet Booking Discovery Confidence**

---

## 33. Hands-On Example — Repository Discovery Report

### Purpose

Define a publication-ready report structure that teams and harnesses can reuse.

### Key Concepts

The report should be concise enough for decision-making but detailed enough to preserve evidence and gaps.

### Important Subsections

#### 33.1 Report Header

Include:

* Task.
* repository.
* branch or revision.
* discovery date.
* discoverer.
* scope.
* risk classification.
* status.

#### 33.2 Executive Discovery Summary

Summarize:

* What is known.
* major architecture boundaries.
* key dependencies.
* material contradictions.
* readiness decision.

#### 33.3 Scope and Method

Document:

* Repositories examined.
* assets examined.
* excluded areas.
* commands or tools used.
* limitations.

#### 33.4 Repository Topology

Include map and inventory.

#### 33.5 Architecture Findings

Cover:

* services.
* modules.
* ownership.
* runtime flows.
* dependencies.
* contracts.
* persistence.

#### 33.6 Operational Findings

Cover:

* build.
* tests.
* deployment.
* observability.
* security.

#### 33.7 AI Engineering Findings

Cover:

* instructions.
* skills.
* prompts.
* roles.
* steering notes.
* harness behavior.

#### 33.8 Evidence Register

List findings by classification.

#### 33.9 Contradictions and Unknowns

Prioritize by implementation risk.

#### 33.10 Confidence Assessment

State:

* overall confidence.
* confidence by area.
* blocking gaps.
* residual risk.

#### 33.11 Recommended Next Actions

Examples:

* Read another repository.
* consult service owner.
* validate production contract version.
* run targeted tests.
* inspect deployment environment.
* request architecture decision.

### Alpha Car Detailing Application

Include a sample report for the fleet-booking request without proceeding to implementation.

### Recommended Table

**Table 5-28 — Repository Discovery Report Template**

---

## 34. Hands-On Example — Discovery Checklists

### Purpose

Provide operational checklists without reducing discovery to a mechanical box-ticking exercise.

### Key Concepts

* Checklists ensure coverage.
* Evidence quality remains more important than completion percentage.
* Checklists should vary by task risk and discovery scope.

### Important Subsections

#### 34.1 Repository Orientation Checklist

* Root documentation reviewed.
* Instructions located.
* solution/workspace located.
* build entry points located.
* pipeline entry points located.
* infrastructure entry points located.
* architecture documents located.

#### 34.2 Architecture Checklist

* Projects inventoried.
* services identified.
* modules identified.
* deployment units mapped.
* data ownership identified.
* APIs inventoried.
* events inventoried.
* dependencies mapped.

#### 34.3 Operational Checklist

* Build commands validated.
* tests identified.
* deployment traced.
* configuration sources mapped.
* observability reviewed.
* rollback path identified.

#### 34.4 Security Checklist

* Authentication identified.
* authorization identified.
* trust boundaries mapped.
* secret references identified.
* sensitive assets excluded or protected.
* security tests identified.

#### 34.5 AI Engineering Checklist

* Instructions read.
* applicable skills identified.
* prompt constraints reviewed.
* role definitions reviewed.
* steering note reviewed.
* harness validation reviewed.

#### 34.6 Readiness Checklist

* Facts separated from inferences.
* contradictions recorded.
* unknowns recorded.
* confidence assessed.
* blockers resolved or escalated.
* recommendation documented.

### Alpha Car Detailing Application

Complete each checklist for the fleet-booking scenario.

### Recommended Table

**Table 5-29 — Risk-Adjusted Discovery Checklist**

---

## 35. Concepts — Discovery Confidence

### Purpose

Provide a structured method for deciding whether discovery is sufficient.

### Key Concepts

* Confidence is not certainty.
* Confidence should be assessed by architectural area.
* A high overall score must not hide a critical unknown.
* Critical unknowns override aggregate confidence.

### Important Subsections

#### 35.1 Confidence Dimensions

Assess:

* Repository topology.
* Architecture boundaries.
* Runtime flow.
* dependencies.
* contracts.
* data ownership.
* security.
* testing.
* deployment.
* documentation accuracy.
* AI guidance.

#### 35.2 Confidence Levels

##### Insufficient

Major ownership, contract, security, or deployment questions remain unresolved.

##### Limited

Basic structure is understood, but important inferences remain unverified.

##### Sufficient for Planning

Architecture and impact are understood well enough to create a plan, but implementation details may still require targeted discovery.

##### Sufficient for Implementation

All critical paths, constraints, validation steps, and ownership boundaries are understood.

##### High Confidence

Evidence is broad, current, consistent, and validated by runtime, tests, or authoritative owners.

#### 35.3 Confidence Scoring

Possible scoring dimensions:

* Evidence quality.
* evidence coverage.
* consistency.
* recency.
* owner validation.
* runtime validation.

Emphasize that numeric scoring supports judgment but does not replace it.

#### 35.4 Blocking Unknowns

Examples:

* Unverified data owner.
* unknown public contract consumer.
* unclear authorization policy.
* unverified migration process.
* missing deployment ownership.
* conflicting instructions.

#### 35.5 Proceed, Pause, or Escalate

Decision outcomes:

* Proceed.
* proceed with documented assumptions.
* deepen discovery.
* seek owner confirmation.
* block change.

### Alpha Car Detailing Application

The agent reaches:

* High confidence in repository topology.
* sufficient confidence in booking ownership.
* limited confidence in station-capacity consistency.
* insufficient confidence in external event consumers.

The implementation is paused pending contract-consumer verification.

### Recommended Diagram

**Figure 5-22 — Discovery Confidence Decision Model**

### Recommended Table

**Table 5-30 — Confidence Assessment Matrix**

---

## 36. Concepts — Discovery Gaps and Escalation

### Purpose

Explain how unresolved questions are managed professionally.

### Key Concepts

* Unknowns are acceptable when visible.
* Hidden unknowns are dangerous.
* Escalation is an engineering control, not an AI failure.
* High-risk assumptions require human ownership.

### Important Subsections

#### 36.1 Types of Discovery Gaps

* Missing repository access.
* inaccessible production configuration.
* undocumented external consumers.
* unavailable system owner.
* incomplete tests.
* contradictory architecture.
* missing deployment evidence.
* generated assets unavailable.
* stale branch.

#### 36.2 Gap Prioritization

Classify by:

* Impact.
* likelihood.
* reversibility.
* security relevance.
* contract relevance.
* deployment relevance.

#### 36.3 Escalation Paths

Escalate to:

* Service owner.
* architect.
* platform team.
* security team.
* DevOps team.
* product owner.
* compliance owner.

#### 36.4 Escalation Package

Include:

* Question.
* evidence found.
* interpretations considered.
* implementation impact.
* required decision.
* deadline or dependency.

#### 36.5 Assumption Approval

If implementation must proceed:

* Record assumption.
* identify approver.
* constrain scope.
* add validation.
* create rollback plan.
* track resolution.

### Alpha Car Detailing Application

The team asks the messaging platform owner to confirm all consumers of `CorporateFleetBookingCreated`.

### Recommended Table

**Table 5-31 — Discovery Gap and Escalation Register**

---

## 37. Architecture Discussion — Repository Discovery inside an AI Harness

### Purpose

Explain how discovery becomes a repeatable automated workflow.

### Key Concepts

* Discovery should be a harness stage.
* Discovery outputs should inform planning and implementation agents.
* The harness must preserve provenance and confidence.
* Human approval is required for unresolved high-risk findings.
* The harness must not silently convert inference into organizational truth.

### Important Subsections

#### 37.1 Discovery Stage in the Harness

Suggested flow:

```text
Task Intake
   ↓
Scope Classification
   ↓
Repository Discovery
   ↓
Evidence Classification
   ↓
Discovery Report
   ↓
Confidence Gate
   ↓
Planning Agent
   ↓
Implementation Agent
```

#### 37.2 Discovery Agent Responsibilities

* Read permitted repository assets.
* map topology.
* identify instructions.
* trace relevant paths.
* collect evidence.
* classify findings.
* produce report.
* flag gaps.
* recommend next exploration.

#### 37.3 Lead Agent Responsibilities

* Define scope.
* review risk classification.
* approve expansion.
* resolve role conflicts.
* decide readiness.

#### 37.4 Reviewer and Architect Responsibilities

* Challenge weak inferences.
* validate architecture boundaries.
* review contradictions.
* approve assumptions.
* confirm readiness.

#### 37.5 Tool Permissions

Define:

* Read-only discovery by default.
* no code modification.
* no secret extraction.
* restricted production access.
* explicit permission for external repositories.
* command allowlists.

#### 37.6 Discovery Artifacts in the Harness

Store:

* Topology map.
* service inventory.
* evidence register.
* contradiction log.
* open questions.
* confidence assessment.
* source revision.
* expiration metadata.

#### 37.7 Confidence Gate

The harness blocks planning or implementation when:

* Critical ownership is unknown.
* security boundaries are unclear.
* public contracts are unverified.
* instructions conflict.
* discovery evidence is stale.
* required repositories are inaccessible.

#### 37.8 Human Approval

Required for:

* Accepting high-risk assumptions.
* overriding contradictory guidance.
* expanding access.
* using sensitive assets.
* proceeding below confidence threshold.

### Alpha Car Detailing Application

The harness performs discovery for the fleet-booking task and blocks the developer agent until the event-consumer gap is resolved.

### Recommended Diagram

**Figure 5-23 — Repository Discovery inside the Harness**

---

## 38. Architecture Discussion — Caching and Refreshing Discovery Results

### Purpose

Explain how discovery results become reusable without becoming stale organizational memory.

### Key Concepts

* Discovery is expensive and should be reusable.
* Cached discovery must be tied to repository revision and scope.
* Reuse requires freshness checks.
* Contradictions and unknowns must survive cache reuse.

### Important Subsections

#### 38.1 Discovery Cache Contents

Store:

* Repository revision.
* branch.
* topology map.
* project inventory.
* dependency graph.
* contract inventory.
* conventions.
* evidence sources.
* confidence.
* expiration policy.

#### 38.2 Cache Keys

Possible keys:

* Repository.
* branch.
* commit.
* service.
* discovery scope.
* toolchain version.
* instruction version.

#### 38.3 Refresh Triggers

Refresh when:

* Relevant files change.
* build graph changes.
* contracts change.
* instructions change.
* deployment manifests change.
* architecture documents change.
* confidence expires.
* contradictions are resolved.
* task risk increases.

#### 38.4 Incremental Discovery

Re-examine:

* Changed files.
* affected dependency graph.
* related contracts.
* impacted tests.
* deployment assets.
* instructions.

#### 38.5 Cache Invalidation Risks

Avoid:

* Reusing discovery across unrelated branches.
* treating old maps as verified.
* ignoring deleted services.
* ignoring renamed contracts.
* carrying resolved contradictions forward.
* merging results from incompatible revisions.

#### 38.6 Repository Intelligence Integration

Validated discovery may update Repository Intelligence, but:

* Provenance must be preserved.
* Confidence must be recorded.
* human approval may be required.
* temporary task findings must not become permanent standards automatically.

### Alpha Car Detailing Application

A prior booking discovery map is reused, but refreshed because capacity-service contracts and booking instructions changed after the cached revision.

### Recommended Diagram

**Figure 5-24 — Discovery-to-Repository-Intelligence Lifecycle**

```text
Discovery
   ↓
Validation
   ↓
Repository Intelligence
   ↓
Reuse
   ↓
Change Detection
   ↓
Targeted Refresh
   ↓
Revalidation
```

---

## 39. Architecture Discussion — Security Boundaries and Sensitive Files

### Purpose

Define safe discovery behavior in enterprise repositories.

### Key Concepts

* Discovery access should follow least privilege.
* The existence of a file does not authorize an agent to read or expose it.
* Sensitive values should not enter prompts, reports, caches, or logs.
* Repository Intelligence must not become a secret store.

### Important Subsections

#### 39.1 Sensitive Asset Categories

* Credentials.
* connection strings.
* private keys.
* certificates.
* production secrets.
* personal data.
* customer data.
* security incident evidence.
* vulnerability reports.
* proprietary algorithms.
* regulated data.

#### 39.2 Safe Discovery Rules

* Record metadata where possible.
* redact values.
* avoid copying secrets.
* do not index restricted files without authorization.
* use read-only access.
* separate public and restricted reports.
* audit discovery access.

#### 39.3 Prompt and Context Protection

Prevent:

* Secret inclusion in prompts.
* accidental external transmission.
* log retention of sensitive values.
* exposure through generated reports.
* model-context contamination.
* retrieval of unrelated sensitive files.

#### 39.4 Prompt Injection in Repositories

Explain that repositories may contain malicious or untrusted text instructing agents to:

* Ignore root instructions.
* reveal secrets.
* execute unsafe commands.
* modify unrelated files.
* disable tests.
* bypass reviews.

Discovery must classify repository text by authority and provenance.

#### 39.5 Access Escalation

Require explicit approval for:

* Production secrets.
* restricted repositories.
* customer datasets.
* incident archives.
* infrastructure credentials.

### Alpha Car Detailing Application

The agent identifies Key Vault references and authorization configuration without copying secret values into the discovery report.

### Recommended Diagram

**Figure 5-25 — Discovery Trust and Access Boundaries**

### Recommended Table

**Table 5-32 — Sensitive Asset Handling Rules**

---

## 40. Claude Example — Repository Discovery with Claude Code

### Purpose

Show how the structured discovery model can be applied with the organization’s primary implementation platform.

### Key Concepts

The example remains focused on engineering workflow rather than tool promotion.

### Important Subsections

#### 40.1 Start in Read-Only Discovery Mode

The Claude task should explicitly state:

* Do not modify files.
* Do not generate implementation.
* Read applicable instructions first.
* Record evidence sources.
* Separate facts from inferences.
* Stop when critical access is missing.

#### 40.2 Establish Scope

Claude identifies:

* Fleet-booking task boundaries.
* likely services.
* repository areas requiring inspection.
* initial unknowns.

#### 40.3 Read Instruction Hierarchy

Claude inspects:

* Root `CLAUDE.md`.
* service-level instructions.
* relevant steering note.
* applicable skill definitions.
* harness rules.

#### 40.4 Map the Repository

Claude produces:

* Top-level asset map.
* project inventory.
* service candidates.
* test and infrastructure locations.

#### 40.5 Trace the Booking Flow

Claude follows:

* API entry point.
* application handler.
* domain model.
* persistence.
* corporate account client.
* capacity client or event.
* event publication.
* tests.
* deployment.

#### 40.6 Produce an Evidence-Classified Report

Required format:

* Verified facts.
* strong inferences.
* weak inferences.
* contradictions.
* unknowns.
* out-of-scope items.

#### 40.7 Apply a Confidence Gate

Claude recommends:

* Proceed to planning.
* perform additional discovery.
* request human clarification.
* block implementation.

### Alpha Car Detailing Application

The Claude example uses the fleet-booking scenario and produces a discovery report rather than code.

### Recommended Table

**Table 5-33 — Claude Discovery Prompt Structure**

---

## 41. GitHub Copilot Comparison

### Purpose

Explain meaningful workflow differences without turning the section into a product overview.

### Key Concepts

GitHub Copilot repository discovery may be more closely integrated with IDE navigation, workspace search, code references, and developer-guided interaction.

### Important Subsections

#### 41.1 Workspace-Guided Discovery

Strengths may include:

* IDE-native symbol navigation.
* open-file context.
* references and definitions.
* developer-selected workspace scope.
* inline exploration.

#### 41.2 Instruction Discovery

Review:

* `.github/copilot-instructions.md`
* path-specific instruction mechanisms where available.
* repository prompt files.
* IDE workspace settings.

#### 41.3 Developer-in-the-Loop Orientation

Copilot discovery may rely more heavily on:

* Developer-selected files.
* IDE search.
* solution explorer.
* test explorer.
* source-control view.

#### 41.4 Discovery Risks

* Overreliance on open files.
* incomplete workspace indexing.
* missing infrastructure repositories.
* confusing IDE context with enterprise system scope.
* limited visibility into external consumers.

#### 41.5 Appropriate Use

GitHub Copilot is effective when:

* The developer actively guides scope.
* The workspace is correctly configured.
* IDE navigation provides precise evidence.
* discovery outputs are captured explicitly rather than left in chat history.

### Alpha Car Detailing Application

Use Copilot to trace booking symbols, references, tests, and project relationships, while separately inspecting infrastructure and multi-repository dependencies.

### Recommended Table

**Table 5-34 — Claude Code and GitHub Copilot Discovery Workflow Differences**

---

## 42. Codex Comparison

### Purpose

Explain meaningful differences in repository exploration and agent execution.

### Key Concepts

Codex-based workflows may emphasize task execution in a repository environment, command-driven inspection, file changes, and validation loops. Discovery must therefore be explicitly separated from implementation.

### Important Subsections

#### 42.1 Repository Environment Inspection

Potential strengths:

* Command execution.
* repository-wide file inspection.
* build and test execution.
* script-based topology analysis.
* automated report generation.

#### 42.2 Instruction Discovery

Inspect:

* `AGENTS.md`
* repository-level guidance.
* directory-specific instructions.
* task prompts.
* validation scripts.

#### 42.3 Discovery-Only Task Definition

Require:

* No modifications.
* no commits.
* no package upgrades.
* no generated code.
* no destructive commands.
* evidence-classified output.

#### 42.4 Validation through Commands

Codex may verify:

* Build graph.
* project references.
* test discovery.
* package versions.
* deployment manifests.
* generated contracts.

#### 42.5 Discovery Risks

* Executing commands too early.
* modifying generated or lock files.
* interpreting successful builds as architectural correctness.
* proceeding from exploration directly into implementation.
* insufficient distinction between repository state and deployed state.

### Alpha Car Detailing Application

Codex performs scripted discovery and test enumeration, then stops at a confidence gate before any feature modification.

### Recommended Table

**Table 5-35 — Claude Code, GitHub Copilot, and Codex Comparison**

Suggested columns:

* Discovery capability.
* Claude Code.
* GitHub Copilot.
* Codex.
* Engineering implication.

---

## 43. Best Practices

### Purpose

Consolidate the chapter’s recommended professional behaviors.

### Key Concepts

### Important Subsections

#### 43.1 Start with Scope, Not Search

Define the task, risk, and repository boundaries first.

#### 43.2 Read Instructions before Source Modification

Identify all applicable repository and directory guidance.

#### 43.3 Use Multiple Evidence Types

Validate conclusions across:

* Code.
* tests.
* configuration.
* deployment.
* contracts.
* documentation.
* runtime assets.

#### 43.4 Trace End-to-End Paths

Follow behavior from entry point to persistence, external dependencies, messages, tests, and deployment.

#### 43.5 Record Provenance

Every material finding should identify its evidence source.

#### 43.6 Separate Facts from Inferences

Never state inferred ownership or architecture as verified fact.

#### 43.7 Record Contradictions Explicitly

Do not silently choose one source.

#### 43.8 Scale Discovery to Risk

High-risk changes require deeper, broader, and more formally reviewed discovery.

#### 43.9 Treat Tests and Pipelines as Architecture Evidence

They reveal enforced system behavior and organizational policy.

#### 43.10 Include AI Assets in Discovery

Instructions, skills, prompts, roles, steering notes, and harness rules are part of the engineering system.

#### 43.11 Preserve Security Boundaries

Use least privilege and exclude sensitive values from reports.

#### 43.12 Produce Reusable Outputs

Discovery should improve future Repository Intelligence.

#### 43.13 Refresh before Reuse

Tie cached findings to revisions and invalidate them when relevant assets change.

#### 43.14 Require Confidence before Planning

Do not allow implementation urgency to bypass unresolved critical gaps.

### Alpha Car Detailing Application

Summarize how each practice improves the fleet-booking discovery process.

---

## 44. Anti-Patterns

### Purpose

Identify recurring behaviors that produce false repository understanding.

### Key Concepts

### Important Subsections

#### 44.1 The README-Only Discovery

Assuming the root README describes current architecture.

#### 44.2 The Directory-Name Assumption

Treating a folder or project name as proof of ownership.

#### 44.3 The First-Match Bias

Stopping after locating the first plausible implementation.

#### 44.4 The Controller-to-Database Shortcut

Tracing only the API controller and repository without inspecting contracts, events, tests, or deployment.

#### 44.5 The Search-Is-Discovery Fallacy

Equating symbol search with architectural understanding.

#### 44.6 The Monorepository Context Dump

Loading the entire repository without risk-based prioritization.

#### 44.7 The Documentation-Is-Truth Assumption

Trusting diagrams and ADRs without implementation validation.

#### 44.8 The Code-Is-Truth Assumption

Ignoring architectural intent, transitional states, or operational configuration.

#### 44.9 The Successful-Build Fallacy

Assuming compilation proves correct architecture or runtime behavior.

#### 44.10 The Hidden-Inference Report

Presenting assumptions as facts.

#### 44.11 The Missing-Consumer Problem

Changing an API or event without discovering downstream consumers.

#### 44.12 The Ignored-Test Architecture

Generating code without understanding test conventions and validation gates.

#### 44.13 The Secret-Harvesting Discovery

Reading or copying sensitive values unnecessarily.

#### 44.14 The Stale-Cache Assumption

Reusing repository maps without checking revisions.

#### 44.15 The Discovery-and-Implementation Merge

Allowing the same agent step to explore and modify before confidence is established.

#### 44.16 The Silent Contradiction Resolution

Choosing one conflicting source without recording or escalating the conflict.

### Alpha Car Detailing Application

For each anti-pattern, show the likely failure in the fleet-booking feature.

### Recommended Table

**Table 5-36 — Discovery Anti-Patterns and Consequences**

---

## 45. Architect’s Notes

### Purpose

Provide architecture-level guidance for experienced readers.

### Important Subsections

#### Architect’s Note 1 — Repository Structure Is Not System Architecture

A directory tree reflects packaging and history. Runtime boundaries require additional evidence.

#### Architect’s Note 2 — Ownership Must Be Proven

Service names, database names, and namespaces do not independently establish domain ownership.

#### Architect’s Note 3 — Architecture Is Distributed

The effective architecture exists across code, contracts, deployment, configuration, tests, and operating procedures.

#### Architect’s Note 4 — Unknown Is a Valid Engineering Result

A visible unknown is safer than an invented answer.

#### Architect’s Note 5 — Contradictions Reveal Architectural Drift

Contradictions should feed governance, documentation maintenance, and technical-debt processes.

#### Architect’s Note 6 — Discovery Should Be Reviewable

A discovery conclusion that cannot be traced to evidence should not influence implementation.

#### Architect’s Note 7 — Discovery Reports Are Temporary Views

They describe a repository revision and scope, not timeless system truth.

#### Architect’s Note 8 — High-Risk Discovery Requires Human Authority

AI agents can collect and analyze evidence, but ownership, security, and contract decisions may require accountable human approval.

### Alpha Car Detailing Application

Relate each note to the booking, corporate account, station capacity, and event-contract boundaries.

---

## 46. Enterprise Tips

### Purpose

Provide operational guidance for organizations implementing Repository Discovery at scale.

### Important Subsections

#### Enterprise Tip 1 — Maintain a Service Catalogue

Record ownership, repositories, runtime units, data stores, contracts, and escalation contacts.

#### Enterprise Tip 2 — Standardize Discovery Reports

Use a consistent format across teams and harnesses.

#### Enterprise Tip 3 — Version Repository Maps

Tie maps to commits, releases, and architecture revisions.

#### Enterprise Tip 4 — Add Ownership Metadata

Use code-owner files, service catalogues, package ownership, and infrastructure tags.

#### Enterprise Tip 5 — Make Contracts Discoverable

Centralize API and event-contract metadata where possible.

#### Enterprise Tip 6 — Treat Instructions as Governed Assets

Define ownership, precedence, review, and versioning.

#### Enterprise Tip 7 — Automate Evidence Collection

Generate project graphs, package inventories, test inventories, and deployment maps.

#### Enterprise Tip 8 — Preserve Human Review for Contradictions

Do not allow automated discovery to resolve architectural conflicts silently.

#### Enterprise Tip 9 — Measure Discovery Quality

Track:

* Unknowns found before implementation.
* architecture issues prevented.
* stale documentation detected.
* discovery reuse.
* rework caused by missed dependencies.

#### Enterprise Tip 10 — Integrate Discovery with Pull Requests

Attach or reference discovery evidence for high-risk changes.

#### Enterprise Tip 11 — Separate Discovery Access from Implementation Access

Discovery agents should use read-only permissions by default.

#### Enterprise Tip 12 — Build Refresh Signals

Trigger rediscovery when instructions, contracts, pipelines, or architecture-critical files change.

### Alpha Car Detailing Application

Show how an enterprise service catalogue and contract registry would reduce uncertainty for the fleet-booking request.

---

## 47. Decision Points

### Purpose

Provide explicit architecture and process decisions teams must make.

### Important Subsections

#### Decision Point 1 — How Much Discovery Is Required?

Options:

* Local.
* service-level.
* enterprise-level.

Decision factors:

* Risk.
* coupling.
* uncertainty.
* reversibility.

#### Decision Point 2 — When Is Discovery Sufficient?

Options:

* Informal reviewer judgment.
* checklist completion.
* confidence matrix.
* formal approval gate.

#### Decision Point 3 — Who Owns the Discovery Report?

Options:

* Developer.
* architect.
* lead agent.
* platform team.
* harness.

#### Decision Point 4 — Which Findings Enter Repository Intelligence?

Options:

* All findings.
* verified facts only.
* verified facts and approved strong inferences.
* selected reusable outputs.

#### Decision Point 5 — How Are Contradictions Resolved?

Options:

* Latest implementation.
* architectural authority.
* owner decision.
* formal ADR.
* temporary exception.

#### Decision Point 6 — How Long May Discovery Results Be Reused?

Options:

* Commit-scoped.
* branch-scoped.
* release-scoped.
* time-scoped.
* change-triggered.

#### Decision Point 7 — What Blocks Implementation?

Possible blockers:

* Unknown data owner.
* unknown contract consumers.
* unresolved security model.
* unclear deployment path.
* conflicting instructions.
* inaccessible critical repository.

#### Decision Point 8 — Which Discovery Tasks May Be Automated?

Classify:

* Fully automated.
* automated with review.
* human-led.
* prohibited.

### Alpha Car Detailing Application

Apply all decision points to the fleet-booking scenario.

### Recommended Table

**Table 5-37 — Repository Discovery Decision Matrix**

---

## 48. Exercises

### Purpose

Allow readers to apply the chapter’s techniques to realistic enterprise situations.

### Exercise 1 — Search versus Discovery

Given a list of repository search results for `CorporateBooking`, identify which additional evidence is required before claiming booking ownership.

### Exercise 2 — Build a Repository Topology Map

Create a topology map for a repository containing:

* .NET APIs.
* workers.
* tests.
* Terraform.
* Helm.
* contracts.
* prompts.
* skills.
* harness scripts.

### Exercise 3 — Classify Discovery Evidence

Classify a set of findings as:

* Verified Fact.
* Strong Inference.
* Weak Inference.
* Contradiction.
* Unknown.
* Out-of-Scope Item.

### Exercise 4 — Trace a Runtime Flow

Trace corporate fleet booking from API entry to data persistence and event publication.

### Exercise 5 — Identify Data Ownership

Given migrations, repositories, shared schemas, and read models, determine what can and cannot be concluded about data ownership.

### Exercise 6 — Detect Documentation Drift

Compare an ADR, deployment manifest, service diagram, and current implementation. Produce a contradiction log.

### Exercise 7 — Risk-Based Discovery

Determine the required discovery depth for:

* Adding a log field.
* changing a public API contract.
* adding a database column.
* changing an event schema.
* modifying authorization.
* migrating a shared database.

### Exercise 8 — Monorepository Discovery

Design a progressive exploration strategy for a repository containing 500 projects.

### Exercise 9 — Multi-Repository Discovery

Create a repository relationship map for:

* Booking service.
* contract repository.
* infrastructure repository.
* corporate portal.
* identity platform.

### Exercise 10 — AI Instruction Discovery

Determine instruction precedence among:

* Root `CLAUDE.md`.
* service-level `CLAUDE.md`.
* `AGENTS.md`.
* Copilot instructions.
* steering note.
* skill file.

### Exercise 11 — Discovery Confidence Assessment

Score discovery confidence for the fleet-booking scenario and identify blocking unknowns.

### Exercise 12 — Harness Design

Design a discovery stage for an enterprise AI harness with:

* Read-only permissions.
* evidence classification.
* confidence gate.
* human escalation.
* cache refresh.

### Exercise 13 — Security Review

Identify which repository assets should be excluded, redacted, or restricted during automated discovery.

### Exercise 14 — Produce a Discovery Report

Create a complete Repository Discovery Report for the corporate fleet-booking feature without writing implementation code.

---

## 49. Interview Questions

### Purpose

Provide advanced discussion and assessment questions for architects, senior engineers, and AI engineering teams.

### Questions

1. What is the difference between Repository Search, Repository Discovery, and Repository Intelligence?
2. Why should Repository Discovery be treated as an engineering discipline?
3. Why can a repository directory tree be misleading?
4. What evidence is required to establish service ownership?
5. How do project boundaries differ from runtime service boundaries?
6. How should an AI agent distinguish a verified fact from a strong inference?
7. What should happen when documentation contradicts implementation?
8. How does task risk affect discovery depth?
9. What is the difference between local, service-level, and enterprise-level discovery?
10. How would you discover all consumers of a versioned event contract?
11. Why are tests important sources of architectural evidence?
12. How can CI/CD pipelines reveal runtime architecture?
13. What is the role of deployment manifests in repository discovery?
14. How should discovery handle inaccessible repositories?
15. What should a Repository Discovery Report contain?
16. How should discovery confidence be assessed?
17. Which unknowns should block implementation?
18. How should discovery findings be cached and refreshed?
19. Why should discovery agents use read-only permissions?
20. How can prompt injection occur through repository files?
21. How should AI instructions, skills, prompts, roles, and steering notes be incorporated into discovery?
22. How does discovery differ in a monorepository and a multi-repository environment?
23. What are the risks of combining discovery and implementation in one agent step?
24. How would you prove which service owns corporate fleet bookings?
25. How would you validate whether station capacity is reserved synchronously or asynchronously?
26. How should an enterprise resolve conflicting architectural evidence?
27. Which discovery outputs should become durable Repository Intelligence?
28. What human approvals should be required before an AI agent proceeds with a high-risk change?
29. How would you compare repository-discovery workflows across Claude Code, GitHub Copilot, and Codex?
30. How would you measure whether a Repository Discovery process is effective?

---

## 50. Chapter Summary

### Purpose

Reinforce the chapter’s central engineering principles.

### Key Concepts to Summarize

* Repository Discovery is the first discipline of Repository Intelligence.
* Repository Search retrieves evidence; it does not establish architectural understanding.
* Repository Discovery is the systematic activity.
* Repository Intelligence is the maintained organizational capability produced from discovery and related AI engineering assets.
* Enterprise repository architecture is distributed across source, contracts, configuration, data, tests, security, deployment, observability, documentation, and AI guidance.
* Discovery must begin before planning or implementation.
* Findings must be classified as:

  * Verified Fact.
  * Strong Inference.
  * Weak Inference.
  * Contradiction.
  * Unknown.
  * Out-of-Scope Item.
* AI agents must never present inferred architecture as verified fact.
* Discovery depth must scale with task risk.
* Discovery should produce maps, inventories, contradiction logs, open questions, and confidence assessments.
* Critical unknowns should block or escalate implementation.
* Discovery can be automated inside a harness, but high-risk assumptions require human approval.
* Cached discovery must be revision-aware and refreshed when relevant assets change.
* Sensitive files and trust boundaries must be protected.
* The Alpha Car Detailing fleet-booking scenario demonstrates that evidence-based discovery is a prerequisite for safe enterprise change.

### Alpha Car Detailing Application

Conclude that the fleet-booking feature becomes implementable only after the team understands booking ownership, corporate authorization, station capacity, persistence, contracts, events, testing, deployment, instructions, contradictions, and remaining unknowns.

---

## 51. Further Reading

### Purpose

Direct readers to authoritative categories of material that deepen the chapter’s concepts.

### Recommended Topics

#### Software Architecture

* Architecture decision records.
* bounded contexts.
* modular architecture.
* microservice boundaries.
* architecture fitness functions.
* evolutionary architecture.

#### Repository and Code Intelligence

* Static program analysis.
* dependency graphs.
* call graphs.
* code indexing.
* semantic search.
* software cartography.

#### Build and Dependency Systems

* .NET build architecture.
* package dependency management.
* reproducible builds.
* software bills of materials.
* supply-chain security.

#### API and Event Contracts

* OpenAPI.
* AsyncAPI.
* JSON Schema.
* Protocol Buffers.
* schema evolution.
* consumer-driven contract testing.

#### Testing

* Test architecture.
* contract testing.
* architecture tests.
* integration-test environments.
* deployment validation.

#### DevOps and Platform Engineering

* Infrastructure as Code.
* GitOps.
* Kubernetes deployment architecture.
* CI/CD policy.
* progressive delivery.
* observability.

#### Security

* Least privilege.
* secret management.
* workload identity.
* threat modeling.
* software supply-chain security.
* prompt injection and untrusted repository content.

#### AI Engineering

* Repository instructions.
* agent roles.
* skill libraries.
* prompt libraries.
* harness design.
* human approval gates.
* evidence provenance.
* Repository Intelligence lifecycle.

### Recommended Handbook Cross-References

* Chapter 4 — Enterprise AI Engineering.
* Chapter 6 — Instructions.
* Chapter 7 — Skills.
* Chapter 8 — Prompts.
* Chapter 9 — Roles.
* Chapter 10 — Steering Notes.
* Chapter 11 — Knowledge Sources.
* Chapter 12 — What Is a Harness?
* Chapter 13 — Harness Architecture.
* Part V — Enterprise AI Governance.
* Part VI — Self-Learning AI Systems.

---

# Recommended Chapter Diagram Set

The completed chapter should include the following professional diagrams:

1. **From Feature Request to Evidence-Based Change**
2. **Search, Discovery, and Repository Intelligence**
3. **Progressive Repository Discovery Workflow**
4. **Alpha Car Detailing Repository Topology Map**
5. **Build Boundaries versus Runtime Boundaries**
6. **Fleet-Booking Runtime Flow**
7. **Fleet-Booking Dependency Map**
8. **Alpha Car Detailing Data Ownership Map**
9. **Corporate Fleet Booking Event Flow**
10. **Fleet Booking Security Boundary Map**
11. **Source-to-Deployment Traceability**
12. **AI Guidance Precedence during Discovery**
13. **Discovery Evidence Model**
14. **Repository Discovery Funnel**
15. **Task Risk versus Discovery Depth Matrix**
16. **Monorepository Discovery Layers**
17. **Multi-Repository System Map**
18. **Discovery Output Portfolio**
19. **Alpha Car Detailing Repository Map**
20. **Corporate Fleet Booking Evidence Register**
21. **Discovery Confidence Decision Model**
22. **Repository Discovery inside the Harness**
23. **Discovery-to-Repository-Intelligence Lifecycle**
24. **Discovery Trust and Access Boundaries**

---

# Recommended Chapter Table Set

The completed chapter should include the following tables:

1. Symptoms of Inadequate Repository Discovery.
2. Discovery Scope Definition.
3. Discovery Stage, Inputs, Activities, and Outputs.
4. Entry-Point Priority by Discovery Goal.
5. Repository Asset Classification.
6. Boundary Type and Evidence Required.
7. Dependency Inventory.
8. Build and Packaging Inventory.
9. Configuration Inventory and Ownership.
10. Data Store and Ownership Inventory.
11. API and Contract Inventory.
12. Event and Messaging Inventory.
13. Test Inventory and Coverage Gaps.
14. Identity and Authorization Evidence.
15. Observability Inventory.
16. CI/CD and Deployment Inventory.
17. Documentation Authority and Freshness Assessment.
18. AI Asset Inventory.
19. Repository Convention Summary.
20. Contradiction Log Example.
21. Evidence Classification Examples.
22. Risk Classification Criteria.
23. Discovery Level Comparison.
24. Repository Relationship Inventory.
25. Fleet Booking Service Inventory.
26. Fleet Booking Open Questions.
27. Fleet Booking Discovery Confidence.
28. Repository Discovery Report Template.
29. Risk-Adjusted Discovery Checklist.
30. Confidence Assessment Matrix.
31. Discovery Gap and Escalation Register.
32. Sensitive Asset Handling Rules.
33. Claude Discovery Prompt Structure.
34. Claude Code and GitHub Copilot Discovery Workflow Differences.
35. Claude Code, GitHub Copilot, and Codex Comparison.
36. Discovery Anti-Patterns and Consequences.
37. Repository Discovery Decision Matrix.

---
 