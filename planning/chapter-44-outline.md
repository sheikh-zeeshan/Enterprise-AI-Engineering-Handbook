# Chapter 44 — Knowledge Graph  

**Part VI — Self-Learning AI Systems**  
**Target file:** `book/44-knowledge-graph.md`

Chapter 44 should remain consistent with the handbook’s frozen terminology, enterprise audience, vendor-neutral principles, Claude-first examples, Alpha Car Detailing running scenario, and the established chapter structure. AI development handbook The chapter also fits directly into Part VI, where **Knowledge Graph** follows Instruction Evolution and precedes the Autonomous Improvement Engine. AI development handbook

---

## 1. Story-Driven Opening — When Engineering Knowledge Becomes Connected

### 1.1 The Alpha Car Detailing scenario

- Alpha Car Detailing has grown from a single application into an enterprise platform containing:
  - Booking services
  - Fleet-management services
  - Customer services
  - Payment components
  - Notification services
  - APIs
  - Events
  - Databases
  - CI/CD pipelines
  - Instructions
  - Skills
  - Prompts
  - Architecture decisions
  - Pull requests
  - Validation evidence
  - Historical Harness runs
- The Harness already possesses substantial information, but the information exists across many separate sources.

### 1.2 The engineering question

Introduce questions that cannot be answered effectively by isolated documents alone:

- Which services depend on the `BookingConfirmed` event?
- Which Skill is responsible for creating event consumers?
- Which Instruction requires idempotent event processing?
- Which architecture decision introduced the transactional outbox?
- Which previous pull requests changed the booking contract?
- Which validation failures occurred after similar changes?
- Which services might be affected if the booking schema changes?
- What evidence supports the Harness's recommendation?

### 1.3 The limitation of isolated knowledge

Explain that storing information is not enough.

The Harness needs to understand:

```text
Artifact → relationship → artifact → evidence → outcome
```

### 1.4 Introducing the Knowledge Graph

Introduce the Knowledge Graph as the capability that connects these relationships.

Core narrative:

> Repository knowledge becomes significantly more useful when the Harness understands not only what artifacts exist, but how those artifacts relate to one another.

### 1.5 Chapter premise

Establish the central principle:

> A Knowledge Graph connects evidence and relationships across the engineering system, but it must remain governed, traceable, and grounded in authoritative sources.

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Explain what a Knowledge Graph is in the context of AI Engineering.
- Distinguish a Knowledge Graph from Harness Memory.
- Distinguish a Knowledge Graph from Repository Intelligence.
- Model engineering concepts as nodes and relationships.
- Represent services, components, APIs, events, Skills, Instructions, Prompts, Roles, builds, PRs, decisions, and findings.
- Understand provenance and confidence.
- Design knowledge-ingestion pipelines.
- Handle entity resolution.
- Detect conflicting and stale knowledge.
- Query and traverse engineering relationships.
- Perform dependency and impact analysis.
- Support root-cause investigation.
- Use graph context to improve Agent planning.
- Apply security and access controls.
- Define human governance requirements.
- Integrate a Knowledge Graph into an enterprise Harness.

---

# 3. Background — From Documents to Connected Engineering Knowledge

## 3.1 Knowledge already exists across the Harness

Discuss knowledge distributed across:

- Repository files
- Source code
- Documentation
- Instructions
- Skills
- Prompts
- Roles
- Steering Notes
- Architecture Decision Records
- Build systems
- Test reports
- Security scanners
- Pull requests
- Review discussions
- Harness Memory
- Run history
- Evaluation results

## 3.2 The fragmentation problem

Explain that these sources answer different questions but usually exist independently.

Example:

```text
CLAUDE.md
    says:
    "Consumers must support idempotency."

BookingConfirmedConsumer.cs
    implements:
    IdempotencyStore

PR #418
    changed:
    BookingConfirmedConsumer.cs

Validation Run #7751
    failed:
    duplicate-event integration test

ADR-021
    decided:
    consumer-side idempotency is mandatory
```

Without relationships, these are disconnected facts.

With a graph:

```text
ADR-021
   └── mandates ──> Idempotency Requirement
                         │
                         ├── implemented-by ──> BookingConfirmedConsumer
                         │
                         ├── enforced-by ─────> Integration Test
                         │
                         └── referenced-by ───> PR #418
```

## 3.3 Why relationships matter

Discuss relationships as a source of engineering intelligence.

Relationships enable the Harness to reason about:

- Dependency
- Ownership
- Implementation
- Enforcement
- Evidence
- History
- Failure propagation
- Architecture boundaries
- Change impact

---

# 4. Concepts

## 4.1 What Is a Knowledge Graph?

Define a Knowledge Graph in the handbook's context:

> A Knowledge Graph is a structured representation of engineering entities and the explicit relationships between them, enriched with provenance, evidence, confidence, ownership, and lifecycle information.

Clarify that it is not simply:

- A document repository
- A vector database
- A search index
- Harness Memory
- Repository Intelligence
- A replacement for source control

## 4.2 Graph terminology

Introduce:

- Node
- Entity
- Relationship
- Edge
- Property
- Metadata
- Provenance
- Source
- Confidence
- Timestamp
- Ownership
- Evidence

---

# 5. Knowledge Graph versus Harness Memory

## 5.1 Harness Memory

Reinforce the Chapter 37 concept:

Harness Memory captures useful historical observations and outcomes.

Examples:

- Previous failures
- Successful implementation patterns
- Review findings
- Validation history
- Historical outcomes

## 5.2 Knowledge Graph

The graph emphasizes explicit relationships.

Example:

```text
BookingService
      │
      ├── publishes ──> BookingConfirmed
      │
      ├── governed-by ──> Booking Instructions
      │
      ├── changed-by ──> PR #418
      │
      └── validated-by ──> BookingIntegrationTests
```

## 5.3 Key distinction

Present conceptually:

```text
Harness Memory
    remembers observations and outcomes.

Knowledge Graph
    connects entities and evidence.
```

## 5.4 How they cooperate

Example:

```text
Knowledge Graph
      ↓
Identifies affected entities
      ↓
Harness Memory
      ↓
Provides historical outcomes
      ↓
Agent
      ↓
Builds stronger plan
```

---

# 6. Knowledge Graph versus Repository Intelligence

## 6.1 Repository Intelligence

Repository Intelligence helps Agents understand the current repository:

- Project structure
- Architecture
- Code organization
- Dependencies
- Conventions
- Existing implementation

The frozen handbook terminology explicitly distinguishes **Repository Intelligence** from project memory. AI development handbook

## 6.2 Knowledge Graph

The Knowledge Graph can extend beyond repository structure into:

- Historical PRs
- Build results
- Validation failures
- Decisions
- Security findings
- Skills
- Instructions
- Human approvals
- Harness runs

## 6.3 Relationship

Explain:

```text
Repository Intelligence
        ↓
Discovers repository facts
        ↓
Knowledge ingestion
        ↓
Knowledge Graph
        ↓
Connects facts with engineering evidence
```

---

# 7. Nodes and Relationships

## 7.1 Node model

Possible node categories:

### Repository nodes

- Repository
- Solution
- Project
- Service
- Component
- Module
- Class
- Interface
- Database
- Table

### Contract nodes

- API
- Endpoint
- Event
- Message
- Schema

### AI Engineering nodes

- Instruction
- Skill
- Prompt
- Role
- Steering Note

### Execution nodes

- Harness Run
- Agent Run
- Build
- Test
- Validation Gate
- Evaluation

### Development lifecycle nodes

- Branch
- Commit
- Pull Request
- Review
- Review Finding

### Governance nodes

- Architecture Decision
- Policy
- Security Requirement
- Approval
- Owner

## 7.2 Relationship examples

Examples:

```text
Service ── exposes ──> API
Service ── publishes ──> Event
Service ── consumes ──> Event
Component ── belongs-to ──> Service
Skill ── implements ──> Pattern
Instruction ── governs ──> Service
Prompt ── invokes ──> Skill
Role ── executes ──> Prompt
PR ── changes ──> Component
Build ── validates ──> Commit
ReviewFinding ── concerns ──> Component
ADR ── governs ──> Architecture Pattern
```

---

# 8. Diagram — Node and Relationship Model

Include a professional diagram similar to:

```text
                   ┌───────────────┐
                   │ ADR-021       │
                   └───────┬───────┘
                           │ governs
                           ▼
                  ┌───────────────────┐
                  │ Booking Service   │
                  └────┬─────────┬────┘
                       │         │
                exposes│         │publishes
                       ▼         ▼
                ┌──────────┐  ┌─────────────────┐
                │ POST     │  │BookingConfirmed │
                │ /booking │  │Event            │
                └──────────┘  └────────┬────────┘
                                      │ consumed-by
                                      ▼
                             ┌──────────────────┐
                             │Notification      │
                             │Service           │
                             └──────────────────┘
```

Explain that relationships are explicit engineering facts, not speculative Agent associations.

---

# 9. Modeling Services and Components

## 9.1 Services as graph entities

Alpha Car Detailing examples:

- Booking Service
- Fleet Service
- Payment Service
- Customer Service
- Notification Service

## 9.2 Component relationships

Example:

```text
BookingService
    ├── contains → BookingController
    ├── contains → BookingApplicationService
    ├── writes-to → BookingDatabase
    └── publishes → BookingConfirmed
```

## 9.3 Architectural boundaries

Discuss representation of:

- Bounded contexts
- Clean Architecture layers
- Dependency rules
- Microservice boundaries

---

# 10. APIs and Events

## 10.1 API nodes

Represent:

- Endpoint
- Method
- Contract
- Version
- Service ownership

## 10.2 Event nodes

Represent:

- Event name
- Producer
- Consumers
- Schema
- Version
- Delivery expectations

## 10.3 Example

```text
Booking Service
      │
      └── publishes
             ↓
      BookingConfirmed
          /       \
 consumes         consumes
   ↓                 ↓
Fleet Service   Notification Service
```

## 10.4 Why graph modeling matters

Support:

- Contract impact analysis
- Consumer discovery
- Version-change analysis
- Deployment planning

---

# 11. Data Ownership

Discuss graph representation of:

- Database ownership
- Table ownership
- Read/write responsibility
- Shared-data risks
- Cross-service dependencies

Example:

```text
BookingService
    └── owns → BookingDatabase

FleetService
    └── reads-via → BookingAPI
```

Contrast with an undesirable relationship:

```text
FleetService
    └── directly-reads → BookingDatabase
```

Explain how the graph can expose architectural violations.

---

# 12. Instructions and Skills

The handbook establishes Instructions as persistent repository-wide engineering requirements and Skills as reusable engineering workflows. AI development handbook

## 12.1 Instruction nodes

Examples:

```text
Instruction:
"All event consumers must be idempotent."
```

Relationships:

```text
Instruction
    ├── governs → Event Consumers
    ├── enforced-by → Validation Gate
    └── derived-from → Architecture Policy
```

## 12.2 Skill nodes

Example:

```text
create-event-consumer
       │
       ├── implements → Idempotency Pattern
       ├── creates → Consumer
       └── governed-by → Event Instructions
```

## 12.3 Knowledge value

Explain how Agents can determine:

- Which Skill applies
- Which Instruction governs the Skill
- Which architectural decision justifies the Instruction
- Which tests validate the generated output

---

# 13. Prompts and Roles

The established model treats Prompts as task-specific requests and Roles as specialized Agent responsibilities. AI development handbook

Discuss graph relationships such as:

```text
Prompt
   └── assigned-to → Developer Agent

Developer Agent
   └── uses → Skill

Reviewer Agent
   └── evaluates → Implementation

Validator Agent
   └── executes → Validation Gates
```

Use these relationships to support execution traceability.

---

# 14. Builds and Validation Results

## 14.1 Build entities

Capture:

- Build
- Commit
- Branch
- Pipeline
- Result
- Timestamp

## 14.2 Validation nodes

Capture:

- Unit test
- Integration test
- Architecture test
- Static analysis
- Security scan
- Contract test

## 14.3 Relationship example

```text
Commit
  │
  └── produced → Build #771
                     │
                     ├── failed → Architecture Test
                     │
                     └── passed → Security Scan
```

## 14.4 Preserve evidence

Explain that results should reference authoritative build artifacts rather than copying unverified interpretations.

---

# 15. Pull Requests and Review Findings

## 15.1 PR nodes

Capture:

- PR identifier
- Branch
- Commit range
- Author
- Reviewers
- Outcome
- Merge status

## 15.2 Review findings

Example:

```text
PR #418
   ├── changes → BookingConsumer
   ├── received → Finding #77
   └── merged-after → Fix #79
```

## 15.3 Historical learning

Connect Chapter 40 concepts to the graph:

```text
PR
 ↓
Review Finding
 ↓
Code Change
 ↓
Validation Result
 ↓
Merge Outcome
```

---

# 16. Architecture Decisions

## 16.1 ADR representation

Model:

- Decision
- Status
- Owner
- Date
- Context
- Affected components
- Replaced decisions

## 16.2 Example

```text
ADR-021
"Use transactional outbox"

       │
       ├── governs → Booking Service
       ├── implemented-by → OutboxWriter
       ├── validated-by → OutboxIntegrationTest
       └── supersedes → ADR-011
```

## 16.3 Why this matters

Agents can understand not just **what exists**, but **why it exists**.

---

# 17. Provenance and Confidence

## 17.1 Provenance as a first-class property

Every significant graph assertion should record:

- Source
- Source type
- Extraction method
- Timestamp
- Owner
- Run identifier
- Version

## 17.2 Authoritative versus derived knowledge

Example levels:

```text
Authoritative
    Source code
    ADR
    Approved Instruction

Observed
    Build result
    Test result
    Review finding

Derived
    Dependency inference
    Historical pattern
    Agent recommendation
```

## 17.3 Confidence

Discuss confidence metadata for inferred knowledge.

Example:

```text
relationship:
NotificationService → depends-on → BookingConfirmed

source:
static-analysis

confidence:
0.98
```

## 17.4 Confidence must not replace provenance

Emphasize:

> A high confidence score does not make an unsupported claim authoritative.

---

# 18. Knowledge Ingestion

## 18.1 Ingestion sources

Possible sources:

- Repository parser
- Static analysis
- Git
- Pull-request API
- CI/CD platform
- Test reports
- Security tools
- Harness logs
- ADRs
- Instruction files
- Skill definitions
- Prompt library

## 18.2 Ingestion pipeline

Discuss stages:

```text
Discover
   ↓
Extract
   ↓
Normalize
   ↓
Resolve Entities
   ↓
Validate
   ↓
Attach Provenance
   ↓
Persist
```

---

# 19. Diagram — Evidence-to-Graph Ingestion

```text
┌────────────────────────────────────────────┐
│ Engineering Evidence                       │
│                                            │
│ Source Code   ADRs   PRs   Builds   Skills │
└──────────────────────┬─────────────────────┘
                       │
                       ▼
               ┌───────────────┐
               │ Ingestion     │
               │ Pipeline      │
               └───────┬───────┘
                       │
             ┌─────────▼─────────┐
             │ Entity Resolution │
             └─────────┬─────────┘
                       │
             ┌─────────▼─────────┐
             │ Provenance +      │
             │ Confidence        │
             └─────────┬─────────┘
                       │
                       ▼
              ┌─────────────────┐
              │ Knowledge Graph │
              └─────────────────┘
```

---

# 20. Entity Resolution

## 20.1 The problem

The same entity may appear under multiple names.

Example:

```text
BookingService
Booking.Service
booking-api
alpha-booking
src/Booking
```

## 20.2 Canonical identity

Discuss stable identifiers.

Example:

```text
service://alpha-car-detailing/booking
```

## 20.3 Resolution strategies

- Repository path
- Project identifier
- Service metadata
- Namespace
- API ownership
- Explicit configuration

## 20.4 Avoid Agent-only entity matching

Explain why fuzzy Agent interpretation should not silently merge entities.

---

# 21. Conflicting Knowledge

## 21.1 Example conflict

```text
ADR:
Booking Service owns Booking data.

Documentation:
Fleet Service may update Booking tables directly.
```

## 21.2 Graph response

Do not silently choose one source.

Represent:

```text
Conflict
   ├── Source A
   ├── Source B
   └── requires → Human Review
```

## 21.3 Authority hierarchy

Discuss establishing governed authority levels such as:

```text
Approved Policy
      ↓
Approved ADR
      ↓
Repository Instruction
      ↓
Current Implementation
      ↓
Documentation
      ↓
Historical Observation
      ↓
Agent Inference
```

Clarify that exact precedence must be enterprise-defined.

---

# 22. Stale Knowledge

## 22.1 How knowledge becomes stale

Examples:

- Component deleted
- API renamed
- Event version changed
- ADR superseded
- Skill updated
- Instruction replaced
- Service ownership transferred

## 22.2 Temporal metadata

Graph entities should support:

- `createdAt`
- `observedAt`
- `validFrom`
- `validTo`
- `supersededBy`

## 22.3 Revalidation

Discuss:

- Scheduled repository scans
- Event-driven graph refresh
- PR-triggered updates
- Harness-run updates

## 22.4 Stale does not mean delete

Historical relationships may remain useful for explaining previous outcomes.

---

# 23. Query and Retrieval

## 23.1 Basic graph queries

Examples:

- Which services consume `BookingConfirmed`?
- Which Instructions govern the Booking Service?
- Which Skills generate event consumers?
- Which architecture decision introduced the outbox?
- Which PRs previously modified this event?
- Which validation failures involved this component?

## 23.2 Structured retrieval

Explain difference between:

```text
keyword search:
"BookingConfirmed"

graph query:
BookingConfirmed
    → consumers
    → owning services
    → related PRs
    → validation history
```

## 23.3 Retrieval for Agents

Provide only relevant graph slices rather than dumping the entire graph into context.

---

# 24. Graph Traversal

Introduce bounded traversal.

Example:

```text
Booking API
    ↓ implemented-by
Booking Handler
    ↓ publishes
BookingConfirmed
    ↓ consumed-by
Notification Service
    ↓ deployed-by
Notification Pipeline
```

Discuss:

- Traversal depth
- Relationship filters
- Time boundaries
- Confidence boundaries
- Authorization boundaries

---

# 25. Impact Analysis

## 25.1 Change-impact question

Alpha Car Detailing example:

> What happens if `BookingConfirmed.v2` changes the customer identifier?

## 25.2 Graph traversal

```text
BookingConfirmed
      │
      ├── produced-by → BookingService
      │
      ├── consumed-by → FleetService
      │
      ├── consumed-by → NotificationService
      │
      ├── validated-by → ContractTests
      │
      └── referenced-by → Skill:create-event-consumer
```

## 25.3 Impact categories

- Code impact
- Contract impact
- Test impact
- Deployment impact
- Security impact
- Architecture impact
- Documentation impact
- Skill impact

---

# 26. Diagram — Impact Analysis

```text
                 Event Contract Change
                         │
             ┌───────────┼───────────┐
             ▼           ▼           ▼
        Producer     Consumer A   Consumer B
             │           │           │
             ▼           ▼           ▼
          Tests        Tests       Tests
             │           │           │
             └───────────┼───────────┘
                         ▼
                   Deployment
                         │
                         ▼
                  Risk Assessment
```

Include relationship traversal showing how the Harness discovers potentially affected artifacts before implementation.

---

# 27. Root-Cause Analysis

## 27.1 Failure scenario

Example:

```text
Fleet booking integration test failed.
```

The Harness follows:

```text
Failed Test
   ↓
Affected Consumer
   ↓
Consumed Event
   ↓
Recent Contract Change
   ↓
PR
   ↓
Architecture Decision
```

## 27.2 Historical comparison

Combine graph data with Harness Memory:

```text
Current Failure
      ↓
Related Components
      ↓
Historical Failures
      ↓
Previous Fixes
```

## 27.3 Avoid claiming causation prematurely

Distinguish:

- Correlation
- Suspected cause
- Verified cause

---

# 28. Agent Planning

## 28.1 Planning before modification

A Lead Agent may query the graph before proposing work.

Example prompt:

```text
Change the BookingConfirmed event to include fleetAccountId.
```

Graph-derived planning context:

```text
Producer:
BookingService

Consumers:
FleetService
NotificationService

Relevant Skills:
update-event-contract

Relevant Instructions:
event-versioning
backward-compatibility

Relevant Tests:
BookingContractTests
FleetConsumerTests
NotificationConsumerTests
```

## 28.2 Better task decomposition

Lead Agent creates:

```text
1. Update event contract.
2. Preserve backward compatibility.
3. Update producer.
4. Update Fleet consumer.
5. Update Notification consumer.
6. Update contract tests.
7. Execute validation gates.
```

## 28.3 Graph as evidence, not authority

The Agent still follows governed Instructions and human approval boundaries.

---

# 29. Architecture Discussion — Knowledge Graph Architecture

Cover logical components:

- Source Connectors
- Repository Intelligence
- Evidence Collectors
- Ingestion Pipeline
- Entity Resolver
- Provenance Manager
- Graph Store
- Query Service
- Retrieval Service
- Access-Control Layer
- Harness Integration Layer
- Governance Workflow

---

# 30. Diagram — Knowledge Graph Architecture

```text
┌──────────────────────────────────────────────┐
│                Knowledge Sources             │
│                                              │
│ Repo │ Git │ CI/CD │ PRs │ ADRs │ Security │
└──────────────────────┬───────────────────────┘
                       │
                       ▼
              ┌──────────────────┐
              │ Source Connectors│
              └─────────┬────────┘
                        ▼
              ┌──────────────────┐
              │ Ingestion Layer  │
              └─────────┬────────┘
                        ▼
              ┌──────────────────┐
              │ Entity Resolver  │
              └─────────┬────────┘
                        ▼
              ┌──────────────────┐
              │ Provenance Layer │
              └─────────┬────────┘
                        ▼
              ┌──────────────────┐
              │ Knowledge Graph  │
              └──────┬─────┬─────┘
                     │     │
            ┌────────▼─┐ ┌─▼──────────┐
            │ Query API│ │ Retrieval  │
            └────┬─────┘ └─────┬──────┘
                 │             │
                 └──────┬──────┘
                        ▼
                 ┌─────────────┐
                 │   Harness   │
                 └─────────────┘
```

---

# 31. Knowledge Graph Inside the Harness

The handbook defines the Harness as the automation layer responsible for orchestrating prompts, AI execution, validation, testing, evaluation, commits, PR creation, metrics, and iteration history. AI development handbook

## 31.1 Integration with Lead Agent

Uses graph data for:

- Scope discovery
- Dependency identification
- Change decomposition
- Risk identification

## 31.2 Developer Agent

Uses:

- Related components
- Required Skills
- Applicable Instructions
- Similar implementations

## 31.3 Reviewer Agent

Uses:

- Architecture relationships
- Ownership
- Relevant decisions
- Previous review findings

## 31.4 Validator Agent

Uses:

- Required gates
- Affected tests
- Contract relationships

## 31.5 Evaluator Agent

Uses:

- Intended business capability
- Related acceptance criteria
- Historical outcomes

## 31.6 Learning Engine

Updates graph relationships from governed evidence.

---

# 32. Diagram — Knowledge Graph Inside the Harness

```text
                      User Request
                           │
                           ▼
                     Lead Agent
                           │
                    queries graph
                           ▼
                 ┌──────────────────┐
                 │ Knowledge Graph  │
                 └──────────────────┘
                     ▲    ▲    ▲
                     │    │    │
         ┌───────────┘    │    └────────────┐
         │                │                 │
         ▼                ▼                 ▼
   Developer          Reviewer         Validator
         │                │                 │
         └───────────┬────┴──────┬──────────┘
                     ▼           ▼
                 Evaluator   Run Evidence
                     │           │
                     └─────┬─────┘
                           ▼
                    Learning Engine
                           │
                   governed ingestion
                           ▼
                  Knowledge Graph
```

---

# 33. Security and Access Control

## 33.1 Knowledge Graph security risks

Cover:

- Sensitive code relationships
- Security findings
- Credentials accidentally ingested
- Infrastructure topology
- Customer information
- Production configuration
- Cross-team access

## 33.2 Authorization

Discuss:

- Repository-level access
- Service-level permissions
- Role-based access
- Attribute-based policies
- Tenant boundaries

## 33.3 Retrieval-time enforcement

Important principle:

> Authorization must apply when knowledge is retrieved, not merely when it is ingested.

## 33.4 Prevent graph-assisted information leakage

Example:

An Agent authorized for Fleet Service must not infer restricted Payment Service information through unrestricted graph traversal.

---

# 34. Human Governance

The handbook's self-learning principle already establishes that the Harness must never silently change standards; it recommends improvements and requires human approval. AI development handbook

## 34.1 Governed graph updates

Differentiate:

### Automatic factual updates

Potential examples:

- Build succeeded
- PR merged
- File exists
- Event consumer detected

### Governed semantic updates

Examples:

- New architecture standard
- New ownership assignment
- New Instruction
- New approved Skill behavior

## 34.2 Human review

Require human review for:

- Conflicting authoritative sources
- Ownership changes
- Architecture interpretation
- Policy changes
- Promotion of learned patterns

## 34.3 Auditability

Every meaningful graph mutation should be traceable.

---

# 35. Hands-on Example — Alpha Car Detailing

Develop a complete scenario.

### Requirement

```text
Add fleetAccountId to BookingConfirmed.
```

### Step 1 — Query graph

Discover:

- Booking Service produces the event.
- Fleet Service consumes it.
- Notification Service consumes it.
- Two integration tests validate the contract.
- ADR-031 governs event compatibility.
- Skill `update-event-contract` applies.
- Instruction requires backward-compatible schema evolution.

### Step 2 — Lead Agent plans change

### Step 3 — Developer implements change

### Step 4 — Reviewer checks architecture implications

### Step 5 — Validator executes identified gates

### Step 6 — Evaluator assesses business outcome

### Step 7 — PR evidence is created

### Step 8 — Learning Engine updates the graph

Show resulting relationships.

---

# 36. Claude Example

Demonstrate how Claude Code could consume a bounded graph result.

Example contextual input:

```text
Affected entity: BookingConfirmed
Producer: BookingService
Consumers:
- FleetService
- NotificationService

Governed by:
- ADR-031
- event-versioning Instruction

Relevant Skill:
- update-event-contract

Required validation:
- BookingContractTests
- FleetConsumerTests
- NotificationConsumerTests
```

Show how Claude uses the context without independently treating inferred graph facts as authoritative.

---

# 37. GitHub Copilot Comparison

Discuss where GitHub Copilot may participate through:

- Repository context
- Agent workflows
- Custom Instructions
- Tool integrations
- External graph retrieval
- Pull-request workflows

Focus on architecture rather than feature marketing.

Compare how the Harness remains responsible for:

- Retrieval
- Governance
- Evidence
- Validation
- Access control

---

# 38. Codex Comparison

Discuss similar integration principles:

- Repository-aware work
- Tool-enabled retrieval
- Graph query tools
- Structured planning
- Validation workflow

Reinforce vendor neutrality.

The handbook's agreed philosophy is Claude-first while maintaining principles that remain valid across coding agents. AI development handbook

---

# 39. Best Practices

Cover practices such as:

1. Model stable engineering entities before modeling every possible artifact.
2. Make provenance mandatory.
3. Separate factual relationships from inferred relationships.
4. Assign confidence only to derived knowledge.
5. Preserve source references.
6. Use stable entity identifiers.
7. Define ownership explicitly.
8. Track temporal validity.
9. Keep historical relationships where useful.
10. Revalidate stale knowledge.
11. Restrict traversal depth for Agent retrieval.
12. Apply authorization during traversal.
13. Keep graph ingestion deterministic where possible.
14. Require governance for standards and architectural knowledge.
15. Combine graph retrieval with Harness Memory rather than replacing it.
16. Prefer targeted graph slices over large context dumps.
17. Record conflicts rather than silently reconciling them.
18. Monitor graph quality.

---

# 40. Anti-Patterns

## 40.1 The Graph Becomes the Source of Truth

Mistake:

Treating the graph as more authoritative than its originating sources.

Correction:

The graph should point back to authoritative evidence.

## 40.2 AI-Invented Relationships

Allowing an Agent to persist speculative relationships as facts.

## 40.3 Missing Provenance

Creating nodes without recording their origin.

## 40.4 Infinite Graph Traversal

Passing huge dependency graphs into Agent context.

## 40.5 Knowledge Without Ownership

No team or system responsible for maintaining important graph entities.

## 40.6 Permanent Stale Knowledge

Never validating relationships after repository evolution.

## 40.7 Silent Conflict Resolution

Automatically choosing between conflicting architecture sources.

## 40.8 Knowledge Graph as Vector Database

Treating similarity search and graph relationships as the same capability.

## 40.9 Knowledge Graph Replaces Repository Intelligence

Failing to maintain current repository analysis.

## 40.10 Knowledge Graph Replaces Harness Memory

Losing historical observations and outcome-oriented learning.

---

# 41. Architect's Notes

Include callouts around key architectural principles.

### Architect's Note — Model relationships with purpose

Do not build a graph merely because graph technology is available. Start from enterprise engineering questions.

### Architect's Note — Provenance before intelligence

A relationship without evidence may create more risk than value.

### Architect's Note — Graphs amplify both good and bad knowledge

Incorrect relationships can propagate wrong assumptions across multiple Agents.

### Architect's Note — Temporal knowledge matters

Software architecture changes. A relationship that was correct six months ago may be wrong today.

### Architect's Note — The graph should explain itself

An engineer should be able to ask:

> Why does the Harness believe this service depends on that event?

and receive traceable evidence.

---

# 42. Enterprise Tips

Include practical enterprise guidance:

- Start with a small ontology.
- Integrate repositories incrementally.
- Prioritize architecture and dependency relationships.
- Capture PR and validation evidence early.
- Establish authority levels.
- Define retention policies.
- Separate environments where appropriate.
- Use graph metrics to monitor quality.
- Create governance ownership.
- Protect confidential architecture information.
- Avoid organization-wide ingestion before authorization boundaries are understood.

---

# 43. Decision Points

Include explicit engineering decisions.

### Decision Point 1

**Should the Knowledge Graph be repository-scoped or enterprise-wide?**

Discuss trade-offs.

### Decision Point 2

**Which entities deserve first-class nodes?**

### Decision Point 3

**Which relationships can be created automatically?**

### Decision Point 4

**Which relationships require human approval?**

### Decision Point 5

**How long should historical relationships remain?**

### Decision Point 6

**How should confidence be represented?**

### Decision Point 7

**Which source wins when knowledge conflicts?**

### Decision Point 8

**Should stale entities be removed or retained historically?**

### Decision Point 9

**Which Agents may query which parts of the graph?**

### Decision Point 10

**When should learned information be promoted into governed knowledge?**

---

# 44. Knowledge Graph Quality and Operational Metrics

Add a subsection covering measurable graph quality:

- Entity-resolution accuracy
- Unresolved entities
- Duplicate entities
- Stale-node percentage
- Stale relationships
- Provenance coverage
- Conflicting assertions
- Orphan nodes
- Retrieval latency
- Query success rate
- Authorization failures
- Knowledge freshness
- Human-review backlog

Explain why graph quality becomes an operational concern once Agents depend on it.

---

# 45. Failure Modes and Recovery

Cover operational failures such as:

- Source unavailable
- Partial ingestion
- Duplicate ingestion
- Corrupt relationship
- Incorrect entity resolution
- Stale graph snapshot
- Unauthorized retrieval
- Failed graph migration

Discuss:

- Idempotent ingestion
- Replay
- Checkpointing
- Source reconciliation
- Audit logs
- Rollback
- Rebuilding from authoritative evidence

---

# 46. Relationship with the Self-Learning Harness

Connect the preceding Part VI chapters:

```text
Chapter 37
Harness Memory
      ↓
Chapter 38
Learning from Builds
      ↓
Chapter 39
Learning from Reviews
      ↓
Chapter 40
Learning from Pull Requests
      ↓
Chapter 41
Skill Evolution
      ↓
Chapter 42
Prompt Evolution
      ↓
Chapter 43
Instruction Evolution
      ↓
Chapter 44
Knowledge Graph
```

Explain that Chapter 44 provides the **relationship layer** connecting these learning sources.

The broader Part VI sequence is explicitly designed around the self-learning Harness concept. AI development handbook

---

# 47. Preparing for Chapter 45 — Autonomous Improvement Engine

Create the architectural bridge to the next chapter.

Explain:

```text
Evidence
    ↓
Harness Memory
    ↓
Knowledge Graph
    ↓
Patterns
    ↓
Improvement Candidates
    ↓
Autonomous Improvement Engine
    ↓
Human Approval
```

Clarify:

> The Knowledge Graph does not autonomously improve engineering standards. It provides connected, traceable evidence that the Autonomous Improvement Engine can use when proposing governed improvements.

This should naturally transition into **Chapter 45 — Autonomous Improvement Engine**. AI development handbook

---

# 48. Exercises

### Exercise 1 — Identify nodes

Given the Alpha Car Detailing repository, identify graph nodes for:

- Booking Service
- Fleet Service
- APIs
- Events
- Skills
- Instructions
- ADRs
- Tests

### Exercise 2 — Define relationships

Create relationships among those nodes.

### Exercise 3 — Model provenance

Design provenance metadata for a relationship discovered through static analysis.

### Exercise 4 — Resolve duplicate entities

Determine whether:

```text
Booking.API
booking-service
src/services/booking
```

represent the same service.

### Exercise 5 — Model a conflict

Represent a conflict between an ADR and outdated documentation.

### Exercise 6 — Impact analysis

Determine the likely impact of changing `BookingConfirmed`.

### Exercise 7 — Root-cause investigation

Trace a failed Fleet Service integration test through the graph.

### Exercise 8 — Security design

Define authorization rules for teams accessing the graph.

### Exercise 9 — Staleness

Design a graph-refresh strategy after each merged pull request.

### Exercise 10 — Harness integration

Design how Lead, Developer, Reviewer, Validator, and Evaluator Agents should consume graph information.

---

# 49. Interview Questions

Include questions such as:

1. What is a Knowledge Graph?
2. Why is a Knowledge Graph useful in an enterprise AI Harness?
3. How does a Knowledge Graph differ from Harness Memory?
4. How does it differ from Repository Intelligence?
5. What is a graph node?
6. What is a graph relationship?
7. Why is provenance important?
8. What is entity resolution?
9. How should conflicting knowledge be handled?
10. How should stale graph data be handled?
11. What is graph traversal?
12. How can a Knowledge Graph support impact analysis?
13. How can it support root-cause analysis?
14. How can an Agent use graph context when creating a plan?
15. What security risks arise from enterprise Knowledge Graphs?
16. Why should inferred knowledge have confidence metadata?
17. Should an AI Agent be allowed to modify graph knowledge automatically?
18. How should historical relationships be retained?
19. How does a Knowledge Graph complement Harness Memory?
20. How does the Knowledge Graph support an Autonomous Improvement Engine?

---

# 50. Chapter Summary

Summarize the chapter around these principles:

- Enterprise engineering knowledge exists across many sources.
- Relationships make isolated evidence substantially more useful.
- A Knowledge Graph represents entities and explicit engineering relationships.
- Nodes may represent code, services, contracts, Skills, Instructions, PRs, builds, findings, decisions, and outcomes.
- Provenance is mandatory.
- Authoritative, observed, and inferred knowledge must be distinguished.
- Entity resolution is necessary for reliable graph construction.
- Conflicting knowledge must be exposed rather than silently reconciled.
- Stale knowledge must be detected and revalidated.
- Graph traversal enables dependency discovery.
- Knowledge Graphs enable impact analysis and root-cause investigation.
- Agents can use graph context to create stronger plans.
- Access control must constrain both stored information and graph traversal.
- The Knowledge Graph complements Repository Intelligence and Harness Memory.
- The Knowledge Graph must remain governed.
- Human approval remains mandatory for changes to engineering standards and authoritative knowledge.

Conclude with the core principle:

> A Knowledge Graph gives the Harness connected engineering context. Its value comes not from how much information it stores, but from how reliably it connects authoritative evidence, engineering relationships, and historical outcomes.

---

# 51. Further Reading

Organize the eventual Further Reading section around:

- Knowledge Graph fundamentals
- Graph data modeling
- Enterprise knowledge management
- Graph databases
- Semantic relationships
- Software dependency graphs
- Software architecture knowledge
- Provenance systems
- Temporal graphs
- Entity resolution
- Graph security
- Graph-based impact analysis
- Root-cause analysis
- AI retrieval architectures
- Retrieval-Augmented Generation and graph retrieval
- Software supply-chain metadata
- Architecture Decision Records
- AI governance and knowledge governance

Use authoritative standards, vendor-neutral architecture material, academic references, and official documentation where practical.

The frozen handbook template requires the story-driven opening, learning objectives, background, concepts, architecture discussion, diagrams, hands-on example, Claude example, Copilot and Codex comparisons, best practices, anti-patterns, Architect's Notes, Enterprise Tips, Decision Points, exercises, interview questions, summary, and further reading; this outline preserves that structure. AI development handbook

**Chapter 44 outline is complete.**
