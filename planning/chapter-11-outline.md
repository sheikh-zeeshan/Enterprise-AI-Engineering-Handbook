# Chapter 11 — Knowledge Sources

## Part II — Repository Intelligence

**Target file:** `book/11-knowledge-sources.md`

## Chapter Objective

Explain Knowledge Sources as trusted reference material that provides technical, business, architectural, operational, and historical context to AI agents.

The chapter establishes the following core distinction:

* **Instructions** define persistent standards and constraints.
* **Skills** define reusable procedures.
* **Prompts** define the current task.
* **Roles** define responsibility and authority.
* **Steering Notes** define temporary direction.
* **Knowledge Sources** provide evidence and context for reasoning and decisions.

The chapter continues the handbook’s Claude-first but vendor-neutral approach, uses Alpha Car Detailing as the running enterprise example, and preserves the frozen terminology and chapter structure established for the project.

---

# 1. Story-driven Opening

## Section Title

**The Corporate Fleet Release and the Missing Decision**

## Purpose

Introduce Knowledge Sources through a realistic engineering scenario in which an AI agent has access to repository instructions, skills, prompts, roles, and a Steering Note but still lacks the factual evidence required to make a correct implementation decision.

## Key Topics

* The difference between direction and evidence
* Why repository instructions alone are insufficient
* Why an AI agent must retrieve authoritative context before modifying a system
* The risks of relying only on source code or recent prompts
* How undocumented assumptions create architectural drift
* How conflicting documents affect AI-assisted development
* Why citations and traceability matter
* The relationship between repository discovery and knowledge retrieval

## Alpha Car Detailing Example

The Alpha Car Detailing team is extending corporate fleet booking during a time-sensitive release.

The Developer Agent receives:

* Repository instructions defining Clean Architecture standards
* A reusable skill for creating API endpoints
* A task prompt for adding recurring corporate fleet bookings
* A Developer Agent role definition
* A Steering Note identifying the current release deadline and scope

However, the agent still needs answers to several factual questions:

* Can fleet customers book multiple vehicles in one request?
* Which service owns corporate contracts?
* What event schema must be used after a booking is confirmed?
* Are government fleet customers subject to different approval rules?
* How should failed payment authorisations be handled?
* Which architectural decision explains the existing service boundary?
* Which production incident led to the current idempotency requirement?

The agent initially infers answers from nearby code. During review, the team discovers that the implementation conflicts with:

* An Architecture Decision Record
* A corporate booking product requirement
* An approved event schema
* A previous incident report
* A security standard governing customer identifiers

The opening demonstrates that instructions tell the agent how to work, while Knowledge Sources tell the agent what is true, why decisions were made, and which evidence supports the implementation.

## Recommended Diagram or Table

**Diagram: “Direction Without Evidence”**

```text
Prompt
   │
   ├── Instructions
   ├── Skill
   ├── Role
   └── Steering Note
           │
           ▼
       AI Agent
           │
           ▼
    Implementation Guess
           │
           ▼
Architecture Conflict
```

Follow with a second version showing Knowledge Sources feeding verified context into the agent.

---

# 2. Learning Objectives

## Section Title

**What You Will Learn**

## Purpose

Define the knowledge and practical capabilities readers should gain from the chapter.

## Key Topics

By the end of the chapter, readers should be able to:

* Define a Knowledge Source in the context of Enterprise AI Engineering
* Distinguish Knowledge Sources from Instructions, Skills, Prompts, Roles, and Steering Notes
* Identify technical, business, architectural, operational, and historical Knowledge Sources
* Determine which source is authoritative for a specific decision
* Evaluate source ownership, freshness, reliability, scope, and accessibility
* Design source discovery, indexing, retrieval, and ranking strategies
* Handle conflicting, stale, missing, and incomplete knowledge
* Require AI agents to cite evidence used during reasoning
* Protect sensitive and restricted information
* Integrate Knowledge Sources into an AI harness
* Establish human approval for consequential knowledge changes
* Compare how Claude Code, GitHub Copilot, and OpenAI Codex consume repository knowledge
* Design an enterprise knowledge refresh and governance process

## Alpha Car Detailing Example

Readers will design a Knowledge Source strategy for the corporate fleet booking capability, including:

* Source inventory
* Authority ranking
* Metadata
* Access rules
* Retrieval expectations
* Citation format
* Conflict handling
* Refresh ownership

## Recommended Diagram or Table

**Table: Learning Objective to Practical Outcome**

| Learning Objective        | Practical Outcome                               |
| ------------------------- | ----------------------------------------------- |
| Identify source authority | Agents prefer approved ADRs over informal notes |
| Detect stale knowledge    | Outdated API specifications are flagged         |
| Rank retrieved sources    | Task-relevant evidence is prioritised           |
| Cite evidence             | Reviewers can trace agent decisions             |
| Handle conflicts          | Human escalation occurs before implementation   |

---

# 3. Background

## Section Title

**From Repository Files to Trusted Engineering Knowledge**

## Purpose

Explain why enterprise repositories contain large amounts of information but do not automatically provide reliable knowledge to AI agents.

## Key Topics

### 3.1 Information Is Not Automatically Knowledge

* Files versus trusted Knowledge Sources
* Raw content versus validated context
* Repository presence does not imply authority
* Search results do not imply correctness
* Recent content does not always supersede approved content

### 3.2 Why AI Agents Need More Than Source Code

* Code expresses current implementation
* Code may contain defects, workarounds, or legacy behaviour
* Business intent may not be visible in code
* Historical rationale is often stored outside implementation files
* Operational constraints may exist only in runbooks or incident reports
* Security obligations may be documented in controlled repositories

### 3.3 The Enterprise Knowledge Landscape

* Repository-local sources
* Organisation-wide documentation
* Product management systems
* API catalogues
* Schema registries
* CI/CD platforms
* Incident-management systems
* Pull request history
* External vendor documentation
* Human subject-matter experts

### 3.4 Knowledge Sources and Repository Intelligence

* Repository discovery identifies what exists
* Knowledge Source management determines what should be trusted
* Discovery builds an inventory
* Retrieval supplies task-specific evidence
* Validation establishes confidence
* Citations preserve traceability

### 3.5 The Cost of Poor Knowledge

* Incorrect service boundaries
* Repeated architecture debates
* Broken integrations
* Security violations
* Inconsistent business rules
* Reintroduction of previously resolved defects
* Hallucinated APIs or unsupported behaviours
* Reduced reviewer confidence

## Alpha Car Detailing Example

The Booking Service code suggests that one booking contains one vehicle. A product requirements document states that corporate bookings may contain a fleet batch. An ADR explains that batch orchestration belongs in the Fleet Management Service, not the Booking Service.

The correct design cannot be derived from code alone.

## Recommended Diagram or Table

**Diagram: Enterprise Knowledge Landscape**

Show the AI agent at the centre, connected to:

* Source repository
* ADR repository
* Product documentation
* API catalogue
* Event schema registry
* Database documentation
* CI/CD system
* Runbooks
* Incident records
* Pull requests
* External documentation

---

# 4. Concepts

## Section Title

**Knowledge Source Fundamentals**

## Purpose

Define the core concepts, classifications, quality dimensions, and distinctions required to manage Knowledge Sources professionally.

## Key Topics

## 4.1 What Is a Knowledge Source?

A Knowledge Source is a trusted reference that supplies evidence, facts, context, constraints, rationale, or historical understanding needed by an AI agent to reason about an engineering task.

A Knowledge Source should have identifiable:

* Content
* Purpose
* Scope
* Owner
* Authority
* Freshness
* Reliability
* Access classification
* Retrieval method
* Validation status

## Alpha Car Detailing Example

`docs/domain/corporate-fleet-booking.md` explains approved corporate booking rules, customer eligibility, recurring schedule restrictions, and approval thresholds.

## Recommended Diagram or Table

**Table: Knowledge Source Definition**

| Attribute   | Description                           |
| ----------- | ------------------------------------- |
| Purpose     | Why the source exists                 |
| Authority   | What decisions it can govern          |
| Owner       | Who maintains and approves it         |
| Scope       | Systems or domains it covers          |
| Freshness   | Whether it reflects the current state |
| Reliability | Confidence in its accuracy            |
| Access      | Who or what may retrieve it           |

---

## 4.2 Why Knowledge Sources Matter

## Purpose

Explain the engineering value provided by trusted evidence.

## Key Topics

* Grounding AI reasoning
* Reducing unsupported assumptions
* Preserving architecture rationale
* Connecting business intent to implementation
* Reusing operational lessons
* Improving review quality
* Supporting governance and auditability
* Enabling reproducible agent decisions
* Reducing repeated repository discovery
* Supporting multi-agent consistency

## Alpha Car Detailing Example

A Validator Agent checks a booking implementation against:

* OpenAPI specification
* Event schema
* Database constraints
* Security policy
* Product acceptance criteria

The validation is evidence-based rather than based only on generic coding expectations.

## Recommended Diagram or Table

**Diagram: Evidence-Grounded Agent Decision**

```text
Task Prompt
     │
     ▼
Source Discovery
     │
     ▼
Source Ranking
     │
     ▼
Evidence Retrieval
     │
     ▼
Reasoning with Citations
     │
     ▼
Implementation or Recommendation
```

---

## 4.3 Knowledge Sources Versus Instructions

## Purpose

Clarify the difference between persistent engineering standards and factual reference material.

## Key Topics

### Instructions

* Define mandatory repository-wide behaviour
* State coding, architecture, testing, security, and documentation standards
* Tell an agent what it must or must not do
* Are normative

### Knowledge Sources

* Explain facts, context, rationale, history, and current system behaviour
* Support reasoning and interpretation
* Provide evidence
* Are informative or evidentiary

### Important Overlap

* A security policy may be both a Knowledge Source and the basis for an Instruction
* Instructions should reference authoritative sources where practical
* Agents should not silently convert documentation into mandatory standards

## Alpha Car Detailing Example

**Instruction:**

> All externally exposed APIs must use the approved authentication middleware.

**Knowledge Source:**

> `security/customer-authentication-standard.md` explains the approved identity flows, token claims, threat model, and exception process.

## Recommended Diagram or Table

**Comparison Table: Instructions and Knowledge Sources**

| Dimension        | Instructions              | Knowledge Sources            |
| ---------------- | ------------------------- | ---------------------------- |
| Primary purpose  | Define required behaviour | Provide evidence and context |
| Nature           | Normative                 | Informative or evidentiary   |
| Typical question | “What must I do?”         | “What is true, and why?”     |
| Example          | Testing requirement       | Test strategy document       |
| Change control   | Repository governance     | Source-specific ownership    |

---

## 4.4 Knowledge Sources Versus Steering Notes

## Purpose

Differentiate temporary mission direction from underlying evidence.

## Key Topics

### Steering Notes

* Describe current priorities
* Define temporary scope and constraints
* Highlight immediate risks and dependencies
* Apply to a sprint, release, or initiative
* Usually maintained by a Lead or Tech Lead

### Knowledge Sources

* Supply the evidence behind decisions
* May remain valid across multiple initiatives
* Include historical and operational context
* May be referenced by many Steering Notes

### Relationship

A Steering Note may state that a service is out of scope. An ADR may explain why the service boundary exists.

## Alpha Car Detailing Example

The Steering Note says:

> Payment Service changes are out of scope for the fleet booking release.

The payment integration ADR explains:

* Why payment orchestration remains isolated
* Which integration events are permitted
* Which failure modes must not be handled inside Booking Service

## Recommended Diagram or Table

**Table: Temporary Direction Versus Supporting Evidence**

---

## 4.5 Knowledge Sources Versus Prompts

## Purpose

Explain why a task prompt should refer to evidence rather than attempting to duplicate all repository knowledge.

## Key Topics

### Prompts

* Define the task to perform now
* State expected outcomes
* Identify constraints and acceptance criteria
* May identify required Knowledge Sources

### Knowledge Sources

* Supply facts required to complete the task
* Exist independently of the individual task
* Can be reused by different prompts and agents

### Prompt Design Principle

A prompt should specify what evidence must be consulted when that evidence is material.

## Alpha Car Detailing Example

The prompt requests:

> Add recurring fleet booking support. Review the corporate fleet product requirements, Booking Service API specification, booking event schema, and ADR-014 before proposing changes.

## Recommended Diagram or Table

**Diagram: Prompt-to-Evidence Relationship**

---

## 4.6 Knowledge Source Types

## Purpose

Classify the major source categories used in enterprise software engineering.

## Key Topics

### Technical Sources

* README files
* API specifications
* Event schemas
* Database documentation
* Source code
* Test suites
* Configuration documentation
* Deployment manifests
* Dependency documentation

### Architectural Sources

* Architecture Decision Records
* System context documents
* Container and component diagrams
* Service ownership maps
* Integration standards
* Data ownership definitions
* Non-functional requirements

### Business Sources

* Product requirements
* Business policies
* Domain documentation
* Process definitions
* Customer agreements
* Pricing and eligibility rules
* Acceptance criteria

### Operational Sources

* Runbooks
* Monitoring dashboards
* Alert definitions
* Deployment procedures
* Disaster recovery documentation
* Service-level objectives
* Support procedures

### Historical Sources

* Incident reports
* Post-incident reviews
* Pull requests
* Code review findings
* Release notes
* Change logs
* Previous implementation proposals
* Deprecated ADRs

### Quality Sources

* Test results
* Static analysis findings
* Security scan results
* Performance benchmarks
* Architecture review findings
* Compliance reports

### External Sources

* Official framework documentation
* Cloud-provider documentation
* Standards documents
* Vendor API documentation
* Regulatory guidance
* Approved third-party references

## Alpha Car Detailing Example

Create a source catalogue for the Fleet Booking capability containing at least one source from each category.

## Recommended Diagram or Table

**Table: Knowledge Source Taxonomy**

| Category     | Example                          | Typical Authority     |
| ------------ | -------------------------------- | --------------------- |
| Business     | Fleet booking requirements       | Product Owner         |
| Architecture | ADR-014                          | Architecture Board    |
| Technical    | Booking OpenAPI document         | Service Owner         |
| Operational  | Booking recovery runbook         | Operations Team       |
| Historical   | Duplicate booking incident       | Incident Review Board |
| External     | Official messaging documentation | External authority    |

---

## 4.7 Source Authority

## Purpose

Explain how organisations determine which source governs a decision.

## Key Topics

* Normative versus informative sources
* Approved versus draft content
* Enterprise-wide versus team-local authority
* Domain-specific authority
* Legal and regulatory precedence
* Architecture board decisions
* Product ownership
* Service ownership
* Official external documentation
* Authority metadata
* Authority hierarchy
* Context-dependent authority

## Alpha Car Detailing Example

For corporate booking eligibility:

1. Approved business policy
2. Product requirement
3. Domain documentation
4. Current implementation
5. Informal team notes

For event structure:

1. Approved schema registry entry
2. Integration standard
3. Service API documentation
4. Sample payload in README
5. Existing test fixture

## Recommended Diagram or Table

**Table: Source Authority Matrix**

| Decision Type        | Primary Authority      | Supporting Sources    |
| -------------------- | ---------------------- | --------------------- |
| Business eligibility | Business policy        | Product requirements  |
| Service boundary     | Approved ADR           | Architecture diagrams |
| Event payload        | Schema registry        | Consumer contracts    |
| Deployment recovery  | Runbook                | Incident report       |
| Coding convention    | Repository instruction | Style guide           |

---

## 4.8 Source Ownership

## Purpose

Define accountability for source accuracy, approval, maintenance, and retirement.

## Key Topics

* Named owner
* Business owner
* Technical owner
* Architecture owner
* Operational owner
* Security owner
* Backup owner
* Review responsibility
* Approval authority
* Escalation path
* Ownership metadata
* Orphaned sources
* Shared ownership risks

## Alpha Car Detailing Example

* Corporate fleet policy: Head of Fleet Products
* Booking API specification: Booking Service team
* Booking event schema: Integration Architecture team
* Recovery runbook: Site Reliability Engineering team
* Authentication standard: Security Architecture team

## Recommended Diagram or Table

**RACI Table for Knowledge Source Ownership**

---

## 4.9 Source Freshness

## Purpose

Explain how to determine whether a source still reflects current enterprise reality.

## Key Topics

* Created date versus effective date
* Last modified date versus last validated date
* Review cadence
* Expiry date
* Superseded sources
* Deprecated sources
* Stale references
* Version alignment
* Release alignment
* Automated freshness checks
* Freshness confidence
* Sources that remain valid despite age

## Alpha Car Detailing Example

An event schema modified yesterday may still be a draft. An ADR approved two years ago may remain authoritative. Freshness cannot be determined from modification date alone.

## Recommended Diagram or Table

**Table: Freshness Metadata**

| Field         | Meaning                               |
| ------------- | ------------------------------------- |
| Created       | When the source was created           |
| Effective     | When it became authoritative          |
| Last reviewed | Last human validation                 |
| Expires       | Mandatory review deadline             |
| Supersedes    | Earlier source replaced               |
| Status        | Draft, approved, deprecated, archived |

---

## 4.10 Source Reliability

## Purpose

Define confidence criteria for evaluating whether a source should influence an AI agent’s reasoning.

## Key Topics

* Accuracy
* Completeness
* Consistency
* Provenance
* Approval status
* Testability
* Reproducibility
* Observed production evidence
* Known limitations
* Source quality scoring
* Reliability versus authority
* Confidence statements

## Alpha Car Detailing Example

A production incident report may be highly reliable for describing an observed failure but may not be authoritative for defining future architecture.

## Recommended Diagram or Table

**Authority and Reliability Quadrant**

* High authority, high reliability
* High authority, low reliability
* Low authority, high reliability
* Low authority, low reliability

---

## 4.11 Source Scope

## Purpose

Prevent agents from applying valid knowledge outside the context in which it is authoritative.

## Key Topics

* Enterprise scope
* Repository scope
* Service scope
* Domain scope
* Environment scope
* Geographic scope
* Customer-segment scope
* Version scope
* Release scope
* Temporal scope
* Exception scope

## Alpha Car Detailing Example

A government fleet approval policy applies only to government customers in selected regions. It must not be applied to all corporate fleet customers.

## Recommended Diagram or Table

**Table: Scope Dimensions and Examples**

---

## 4.12 Source Discovery

## Purpose

Explain how agents and humans identify available Knowledge Sources before performing work.

## Key Topics

* Repository discovery
* Source manifests
* Documentation maps
* Service catalogues
* Search conventions
* File naming standards
* Metadata headers
* Linked references
* Dependency mapping
* Human-curated entry points
* Discovery commands
* Discovery permissions
* Avoiding exhaustive irrelevant searches

## Alpha Car Detailing Example

The repository includes:

```text
knowledge/
├── index.md
├── architecture/
├── business/
├── api/
├── events/
├── database/
├── operations/
├── security/
└── history/
```

`knowledge/index.md` identifies authoritative sources for each service and domain.

## Recommended Diagram or Table

**Diagram: Knowledge Discovery Paths**

---

## 4.13 Source Indexing

## Purpose

Explain how content is organised and made searchable by AI agents and harness components.

## Key Topics

* File-based indexes
* Metadata indexes
* Search indexes
* Vector indexes
* Keyword indexes
* Knowledge graphs
* Schema registries
* API catalogues
* Chunking strategy
* Document boundaries
* Metadata preservation
* Version-aware indexing
* Access-aware indexing
* Index refresh
* Index validation
* Index drift

## Alpha Car Detailing Example

The knowledge index records:

* Source identifier
* Title
* Type
* Domain
* Service
* Owner
* Status
* Authority
* Version
* Effective date
* Access classification
* Location
* Related sources

## Recommended Diagram or Table

**Sample Knowledge Index Schema**

---

## 4.14 Source Retrieval

## Purpose

Describe how agents retrieve the smallest sufficient set of evidence for a task.

## Key Topics

* Task decomposition
* Query formation
* Keyword retrieval
* Semantic retrieval
* Metadata filtering
* Service and domain filtering
* Date and version filtering
* Source-type filtering
* Retrieval limits
* Context-window management
* Full-document versus section retrieval
* Iterative retrieval
* Retrieval logging

## Alpha Car Detailing Example

For recurring fleet bookings, the agent retrieves:

1. Corporate fleet requirements
2. Booking ownership ADR
3. Booking OpenAPI specification
4. Booking event schema
5. Previous duplicate-booking incident report
6. Relevant security standard

It does not load every document in the repository.

## Recommended Diagram or Table

**Diagram: Task-Aware Retrieval Pipeline**

---

## 4.15 Source Ranking

## Purpose

Explain how agents choose which retrieved sources deserve the greatest influence.

## Key Topics

* Authority
* Relevance
* Scope match
* Freshness
* Reliability
* Approval status
* Version compatibility
* Source specificity
* Source proximity to the decision
* Access level
* Conflict indicators
* Ranking transparency
* Weighted scoring
* Human-defined priority rules

## Alpha Car Detailing Example

An approved fleet booking policy ranks above an implementation comment. A service-specific ADR ranks above a generic architecture guideline for a service-boundary decision.

## Recommended Diagram or Table

**Example Ranking Formula**

```text
Source Score =
    Authority Weight
  + Relevance Weight
  + Scope Match
  + Freshness Confidence
  + Reliability Confidence
  - Conflict Penalty
  - Deprecation Penalty
```

Include a table explaining that numerical scoring supports, but does not replace, explicit authority rules.

---

## 4.16 Source Conflicts

## Purpose

Define how agents should behave when trusted sources disagree.

## Key Topics

* Detecting explicit and implicit conflicts
* Version conflicts
* Business versus technical conflicts
* Policy versus implementation conflicts
* Architecture versus delivery conflicts
* External documentation versus local implementation
* Authority hierarchy
* Conflict records
* Escalation
* Temporary resolution
* Decision capture
* Prohibition against silent reconciliation

## Alpha Car Detailing Example

The product requirement permits booking cancellation until service begins. The API specification permits cancellation only 24 hours before the appointment.

The agent must:

* Identify the conflict
* Cite both sources
* Avoid choosing silently
* Request a human decision
* Record the approved resolution
* Recommend updating the losing source

## Recommended Diagram or Table

**Decision Tree: Handling Conflicting Sources**

---

## 4.17 Source Validation

## Purpose

Explain how Knowledge Sources are verified before agents rely on them.

## Key Topics

* Structural validation
* Link validation
* Schema validation
* Ownership validation
* Approval validation
* Version validation
* Freshness validation
* Cross-source consistency checks
* Test-backed validation
* Human review
* Automated validation pipelines
* Validation evidence
* Validation status

## Alpha Car Detailing Example

The event schema source is validated by:

* JSON Schema validation
* Producer contract tests
* Consumer contract tests
* Schema registry compatibility checks
* Integration Architect approval

## Recommended Diagram or Table

**Validation Checklist by Source Type**

---

## 4.18 Source Citations

## Purpose

Establish citations as a standard output of evidence-grounded AI engineering.

## Key Topics

* Why agents should cite sources
* File and section references
* Version references
* Commit references
* ADR identifiers
* API version references
* Schema identifiers
* Pull request references
* Test run references
* Citation granularity
* Citation quality
* Citations in plans, code reviews, and implementation summaries
* Unsupported claims
* Citation logging in the harness

## Alpha Car Detailing Example

An implementation summary states:

> The Fleet Management Service owns recurring booking orchestration based on ADR-014, section “Service Responsibilities.” The Booking Service continues to own individual appointment creation under Booking API v3.2.

## Recommended Diagram or Table

**Table: Citation Format by Source Type**

| Source Type  | Citation Example                       |
| ------------ | -------------------------------------- |
| ADR          | `ADR-014, Service Responsibilities`    |
| API          | `Booking API v3.2, POST /bookings`     |
| Schema       | `fleet.booking.requested v2.1`         |
| Pull request | `PR #284, reviewer decision`           |
| Incident     | `INC-2026-017, root cause section`     |
| Test run     | `FleetBooking.ContractTests, run 8421` |

---

## 4.19 Source Access Control

## Purpose

Explain how Knowledge Sources must respect enterprise security and authorisation boundaries.

## Key Topics

* Least-privilege access
* User identity
* Agent identity
* Role-based access control
* Attribute-based access control
* Repository permissions
* Document-level access
* Section-level restrictions
* Environment separation
* Tenant separation
* Retrieval filtering
* Access logging
* Denied-source behaviour
* Privilege escalation prohibition
* Human approval for restricted access

## Alpha Car Detailing Example

A Developer Agent may access public API documentation and service ADRs but may not retrieve:

* Customer personal data
* Production credentials
* Restricted incident attachments
* Confidential pricing agreements
* Government customer contracts

## Recommended Diagram or Table

**Diagram: Access-Aware Retrieval**

---

## 4.20 Sensitive Information

## Purpose

Define protections for confidential, regulated, personal, and secret information.

## Key Topics

* Personally identifiable information
* Customer data
* Authentication secrets
* Connection strings
* Private keys
* Production logs
* Commercial agreements
* Security vulnerabilities
* Legal material
* Data minimisation
* Redaction
* Tokenisation
* Secret scanning
* Prompt leakage
* Output filtering
* Retention controls
* External model restrictions

## Alpha Car Detailing Example

A production incident report includes customer registration numbers and contact details. The indexed version must contain a redacted technical summary rather than the raw incident attachment.

## Recommended Diagram or Table

**Table: Information Classification and Agent Handling**

---

## 4.21 Stale Knowledge

## Purpose

Explain how outdated knowledge should be detected, marked, and prevented from influencing current work.

## Key Topics

* Staleness indicators
* Review deadlines
* Version mismatches
* Broken links
* Deprecated APIs
* Retired services
* Superseded ADRs
* Historical documents presented as current
* Stale-source warnings
* Quarantine
* Revalidation
* Automated issue creation
* Owner notification

## Alpha Car Detailing Example

An old README references direct database access from the Booking Service to the Customer database. A newer ADR prohibits this integration.

The old README should be marked as stale or corrected rather than left available as an apparently valid source.

## Recommended Diagram or Table

**Lifecycle Diagram: Current → Review Due → Stale → Deprecated → Archived**

---

## 4.22 Missing Knowledge

## Purpose

Explain how agents should respond when required information cannot be found.

## Key Topics

* Recognising absence
* Avoiding fabricated assumptions
* Distinguishing unavailable from inaccessible knowledge
* Asking focused questions
* Identifying responsible owners
* Recording missing-source findings
* Creating temporary assumptions
* Expiry of assumptions
* Approval requirements
* Updating documentation after resolution

## Alpha Car Detailing Example

No approved source defines whether a recurring booking may cross multiple service stations.

The agent must state that the rule is missing and request a Product Owner decision rather than infer behaviour.

## Recommended Diagram or Table

**Decision Tree: What to Do When Knowledge Is Missing**

---

## 4.23 Knowledge Gaps

## Purpose

Define knowledge gaps as managed engineering risks rather than isolated documentation problems.

## Key Topics

* Gap identification
* Gap classification
* Business gaps
* Architecture gaps
* Operational gaps
* Security gaps
* Historical gaps
* Risk scoring
* Gap backlog
* Ownership
* Resolution priority
* Temporary controls
* Measurement
* Repeated question detection
* Harness-generated recommendations

## Alpha Car Detailing Example

The harness identifies that four recent prompts required human clarification about fleet approval limits. It proposes creating an authoritative business policy source.

## Recommended Diagram or Table

**Knowledge Gap Register**

| Gap | Impact | Owner | Temporary Guidance | Due Date | Status |
| --- | ------ | ----- | ------------------ | -------- | ------ |

---

# 5. Architecture Discussion

## Section Title

**Architecting an Enterprise Knowledge System for AI Agents**

## Purpose

Present an enterprise architecture for discovering, validating, retrieving, ranking, and governing Knowledge Sources.

## Key Topics

## 5.1 Architectural Goals

* Evidence-grounded reasoning
* Traceability
* Least privilege
* Source freshness
* Reproducibility
* Vendor neutrality
* Tool interoperability
* Human governance
* Scalable retrieval
* Low-noise context
* Auditability

## 5.2 Core Components

* Knowledge Source repositories
* Source registry
* Metadata catalogue
* Indexing pipeline
* Search service
* Semantic retrieval service
* Ranking engine
* Access-control filter
* Source validator
* Citation generator
* Conflict detector
* Knowledge gap register
* Refresh scheduler
* Audit log
* Human approval workflow

## 5.3 Repository-Local Versus Enterprise Knowledge

### Repository-Local

* README files
* ADRs
* API documents
* Service runbooks
* Test results
* Code review findings

### Enterprise-Level

* Security policies
* Business policies
* Platform standards
* Regulatory guidance
* Shared integration standards
* Organisation-wide service catalogue

### Architectural Decision

Agents should receive a unified retrieval experience without erasing ownership or access boundaries.

## 5.4 Push Versus Pull Knowledge

* Preloaded context
* Task-triggered retrieval
* Agent-initiated search
* Harness-mandated sources
* Reviewer-requested evidence
* Context-window trade-offs

## 5.5 Centralised Versus Federated Ownership

### Centralised Model

* Easier governance
* Risk of bottlenecks
* Reduced domain ownership

### Federated Model

* Domain teams own their knowledge
* Central platform defines metadata and retrieval standards
* Requires strong consistency controls

### Recommended Enterprise Model

Federated content ownership with central platform governance.

## 5.6 Knowledge Source Lifecycle

* Creation
* Classification
* Review
* Approval
* Publication
* Indexing
* Retrieval
* Revalidation
* Supersession
* Deprecation
* Archival

## 5.7 Knowledge Sources Inside an AI Harness

* Lead Agent identifies required evidence
* Developer Agent retrieves implementation context
* Reviewer Agent verifies citations
* Validator Agent checks specifications and tests
* Evaluator Agent measures source use
* Harness records retrieved source versions
* Harness detects repeated knowledge gaps
* Harness proposes source updates
* Humans approve authoritative changes

## 5.8 Human Approval

Human approval is required when:

* Sources conflict
* A source changes mandatory behaviour
* An AI agent proposes a new authoritative source
* Sensitive information is involved
* Business policy is incomplete
* Architecture boundaries are affected
* Existing instructions may need revision
* A stale source is being replaced
* An external source is adopted as an enterprise standard

## Alpha Car Detailing Example

Design a Knowledge Source architecture supporting Booking, Fleet Management, Customer, Payment, Notification, and Station Management services.

The architecture must allow the corporate fleet implementation team to retrieve business, API, event, database, security, and operational knowledge without exposing restricted customer or production information.

## Recommended Diagram or Table

**Primary Architecture Diagram: Enterprise Knowledge Retrieval Architecture**

```text
Knowledge Producers
├── Product Teams
├── Architecture Board
├── Service Teams
├── Security Team
├── Operations Team
└── CI/CD Systems
          │
          ▼
Source Repositories and Systems
          │
          ▼
Ingestion and Validation Pipeline
          │
          ▼
Metadata Catalogue and Indexes
          │
          ▼
Access-Control and Ranking Layer
          │
          ▼
AI Harness Retrieval Service
          │
          ├── Lead Agent
          ├── Developer Agent
          ├── Reviewer Agent
          ├── Validator Agent
          └── Evaluator Agent
          │
          ▼
Citations, Decisions, Gaps, and Audit Records
```

---

# 6. Professional Diagrams

## Section Title

**Visualising Knowledge Flow and Trust**

## Purpose

Define the professional diagrams that should accompany the chapter and reinforce its major concepts.

## Key Topics and Recommended Diagrams

## 6.1 Diagram: Repository Intelligence Model

Show the relationship among:

* Repository Discovery
* Instructions
* Skills
* Prompts
* Roles
* Steering Notes
* Knowledge Sources
* Harness

## Alpha Car Detailing Example

Corporate fleet booking request flowing through the complete repository intelligence model.

---

## 6.2 Diagram: Knowledge Source Lifecycle

Show:

```text
Create
  ↓
Classify
  ↓
Review
  ↓
Approve
  ↓
Publish
  ↓
Index
  ↓
Retrieve
  ↓
Validate
  ↓
Refresh or Supersede
  ↓
Archive
```

## Alpha Car Detailing Example

Lifecycle of the corporate fleet booking policy.

---

## 6.3 Diagram: Retrieval and Ranking Pipeline

Show:

* User task
* Query generation
* Metadata filtering
* Search
* Access filtering
* Ranking
* Conflict detection
* Context assembly
* Citation generation

## Alpha Car Detailing Example

Retrieving evidence for recurring booking implementation.

---

## 6.4 Diagram: Source Authority Pyramid

Possible layers:

1. Law, regulation, contractual obligation
2. Approved enterprise policy
3. Approved business and architecture decisions
4. Service specifications and schemas
5. Operational evidence
6. Implementation and tests
7. Informal notes and discussions

Clarify that hierarchy remains context-dependent.

---

## 6.5 Diagram: AI Harness Knowledge Integration

Show each role consuming different source types:

* Lead Agent: priorities, architecture, product scope
* Developer Agent: APIs, schemas, code, standards
* Reviewer Agent: ADRs, review findings, security rules
* Validator Agent: tests, contracts, acceptance criteria
* Evaluator Agent: metrics, historical runs, quality benchmarks

---

## 6.6 Table: Knowledge Source Quality Scorecard

Include:

* Authority
* Ownership
* Freshness
* Reliability
* Scope clarity
* Accessibility
* Citation support
* Conflict status

---

# 7. Hands-on Example

## Section Title

**Building the Alpha Car Detailing Knowledge Source Catalogue**

## Purpose

Provide a practical repository-based example that readers can implement without requiring a complete knowledge platform.

## Key Topics

## 7.1 Scenario

The team is preparing to implement recurring corporate fleet bookings.

The Lead Agent must identify authoritative evidence before development begins.

## 7.2 Proposed Repository Structure

```text
alpha-car-detailing/
├── CLAUDE.md
├── AGENTS.md
├── knowledge/
│   ├── index.md
│   ├── business/
│   │   ├── corporate-fleet-policy.md
│   │   └── government-fleet-policy.md
│   ├── architecture/
│   │   ├── adr-014-fleet-booking-ownership.md
│   │   └── service-boundaries.md
│   ├── api/
│   │   ├── booking-api.yaml
│   │   └── fleet-management-api.yaml
│   ├── events/
│   │   ├── fleet-booking-requested.schema.json
│   │   └── booking-confirmed.schema.json
│   ├── database/
│   │   └── booking-data-dictionary.md
│   ├── operations/
│   │   └── duplicate-booking-recovery-runbook.md
│   ├── security/
│   │   └── fleet-customer-data-standard.md
│   └── history/
│       ├── incident-2026-017-duplicate-bookings.md
│       └── pr-284-review-findings.md
└── harness/
    └── knowledge/
        ├── source-manifest.yaml
        ├── retrieval-policy.md
        └── citation-policy.md
```

## 7.3 Source Manifest

Define metadata such as:

```yaml
id: ADR-014
title: Fleet Booking Ownership
type: architecture-decision
status: approved
authority: architecture-board
owner: enterprise-architecture
scope:
  domains:
    - fleet-management
    - booking
effectiveDate: 2026-05-01
lastReviewed: 2026-07-10
classification: internal
location: knowledge/architecture/adr-014-fleet-booking-ownership.md
```

## 7.4 Knowledge Index

Create a human-readable index that explains:

* Which source governs each decision type
* Source owner
* Approval status
* Scope
* Related sources
* Known conflicts
* Review date

## 7.5 Retrieval Policy

Define task-specific retrieval rules:

* API changes require API specification and relevant ADRs
* Event changes require schema and consumer contract evidence
* Business-rule changes require approved product or policy sources
* Security changes require the applicable security standard
* Operational changes require runbooks and incident history

## 7.6 Conflict Handling

Demonstrate a conflict between:

* Product requirement
* API specification
* Existing tests

Create a `knowledge-conflict.md` record rather than silently choosing one source.

## 7.7 Citation Requirements

Require implementation plans and review summaries to include:

* Source identifier
* Version
* Relevant section
* Decision supported
* Unresolved uncertainty

## 7.8 Knowledge Gap Record

Create a record for the undefined cross-station recurring booking rule.

## 7.9 Human Approval Workflow

The Product Owner resolves the business rule. The Architecture Owner confirms the service boundary. The updated source is approved and re-indexed.

## Alpha Car Detailing Example

The entire hands-on section is based on the corporate fleet booking capability.

## Recommended Diagram or Table

* Source manifest example
* Source authority matrix
* Retrieval checklist
* Knowledge conflict record
* Knowledge gap record
* Citation checklist

---

# 8. Claude Example

## Section Title

**Using Knowledge Sources with Claude Code**

## Purpose

Show how Claude Code can discover and use repository-local Knowledge Sources while maintaining vendor-neutral engineering principles.

## Key Topics

## 8.1 Repository Guidance

Use `CLAUDE.md` to tell Claude:

* Where trusted Knowledge Sources are located
* Which source index to read first
* How to distinguish approved and draft content
* When citations are required
* How to report conflicts
* How to report missing knowledge
* Which restricted paths must not be accessed
* That Claude must not silently modify authoritative sources

## 8.2 Example Claude Task

A concise task asks Claude to:

* Review the corporate fleet booking prompt
* Read the Steering Note
* Consult named Knowledge Sources
* Identify conflicts or missing knowledge
* Produce an evidence-based implementation plan
* Cite every architectural and business decision
* Avoid code changes until uncertainties are resolved

## 8.3 Claude Planning Workflow

1. Read repository instructions
2. Read the current Steering Note
3. Read the source index
4. Identify required source types
5. Retrieve relevant approved sources
6. Compare scope and version
7. Detect conflicts
8. Report knowledge gaps
9. Produce a cited plan
10. Request human approval where required

## 8.4 Claude Output Expectations

The output should include:

* Sources consulted
* Sources rejected and reasons
* Decisions supported by evidence
* Conflicts found
* Missing knowledge
* Assumptions requiring approval
* Proposed implementation approach
* Validation plan

## 8.5 Claude Limitations and Controls

* Context-window limitations
* Risk of over-weighting nearby files
* Risk of treating outdated files as current
* Need for explicit source hierarchy
* Need for access restrictions
* Need for harness-level logging
* Human review of consequential decisions

## Alpha Car Detailing Example

Claude prepares the corporate fleet booking implementation plan using:

* `ADR-014`
* Corporate fleet policy
* Booking API specification
* Event schemas
* Duplicate-booking incident report
* Security standard

## Recommended Diagram or Table

**Table: Claude Knowledge Workflow and Required Controls**

---

# 9. GitHub Copilot Comparison

## Section Title

**Using Knowledge Sources with GitHub Copilot**

## Purpose

Compare Copilot’s repository-context capabilities with the chapter’s broader Knowledge Source architecture.

## Key Topics

* Repository instructions
* Workspace context
* Referencing files and symbols
* Chat-based repository questions
* Prompt files
* Pull request context
* Limitations of implicit context selection
* Need to explicitly identify authoritative files
* Need for source metadata and repository organisation
* Review and citation expectations
* Differences between interactive assistance and harness-controlled retrieval

## Alpha Car Detailing Example

A developer asks Copilot to propose recurring booking changes while explicitly referencing:

* Fleet booking ADR
* Product requirement
* OpenAPI document
* Event schema
* Relevant tests

The section explains why relying on automatically selected neighbouring files may omit business or historical context.

## Recommended Diagram or Table

**Comparison Table: Claude Code and GitHub Copilot Knowledge Use**

| Dimension               | Claude Code                     | GitHub Copilot                          |
| ----------------------- | ------------------------------- | --------------------------------------- |
| Repository exploration  | Agent-oriented                  | IDE and workspace-oriented              |
| Explicit source loading | Strongly task-driven            | Often user-selected or context-assisted |
| Harness integration     | Suitable for scripted workflows | Commonly developer-interactive          |
| Citation governance     | Must be designed                | Must be designed                        |
| Authority handling      | Repository conventions required | Repository conventions required         |

Avoid feature-level claims that are not essential to the engineering comparison.

---

# 10. Codex Comparison

## Section Title

**Using Knowledge Sources with OpenAI Codex**

## Purpose

Compare Codex-based repository work with the same vendor-neutral Knowledge Source principles.

## Key Topics

* Repository instructions and agent guidance
* Task-scoped context
* Source selection
* Evidence requirements
* Sandboxed execution
* Validation through tests
* Source citations in plans and summaries
* Harness integration
* Explicit authority rules
* Access control
* Differences between generated implementation evidence and business authority

## Alpha Car Detailing Example

Codex receives a task requiring it to:

* Inspect approved Knowledge Sources
* Produce a cited plan
* Implement only after conflicts are resolved
* Run contract and integration tests
* Report the source basis for significant decisions

## Recommended Diagram or Table

**Three-Platform Comparison**

| Capability Area           | Claude Code                        | GitHub Copilot                      | OpenAI Codex                       |
| ------------------------- | ---------------------------------- | ----------------------------------- | ---------------------------------- |
| Primary interaction model | Agent-led repository work          | IDE-assisted development            | Task-oriented coding agent         |
| Source discovery          | Explicit repository exploration    | Workspace and user-provided context | Task and repository context        |
| Authority model           | Enterprise-defined                 | Enterprise-defined                  | Enterprise-defined                 |
| Citation requirement      | Harness or prompt enforced         | Instruction or workflow enforced    | Task or harness enforced           |
| Human approval            | Required for consequential changes | Required for consequential changes  | Required for consequential changes |

Emphasise that no platform automatically solves authority, freshness, ownership, conflict resolution, or governance.

---

# 11. Best Practices

## Section Title

**Best Practices for Enterprise Knowledge Sources**

## Purpose

Provide actionable standards for designing and operating reliable Knowledge Sources.

## Key Topics

### 11.1 Maintain a Knowledge Source Index

* Provide a recognised entry point
* Organise sources by domain and source type
* Record ownership and authority

### 11.2 Assign an Explicit Owner

* Every authoritative source must have an accountable owner
* Avoid team names without accountable roles

### 11.3 Record Approval and Status

Use statuses such as:

* Draft
* Under review
* Approved
* Superseded
* Deprecated
* Archived

### 11.4 Separate Standards from Evidence

* Keep mandatory Instructions concise
* Link Instructions to detailed supporting sources
* Do not overload `CLAUDE.md` or equivalent files with all enterprise knowledge

### 11.5 Define Source Authority Before Retrieval

* Retrieval ranking cannot compensate for an undefined authority model

### 11.6 Preserve Source Scope

* Tag sources by domain, service, environment, version, geography, and customer segment

### 11.7 Require Citations for Significant Decisions

* Architecture
* Security
* Business rules
* Integration contracts
* Data ownership
* Operational procedures

### 11.8 Detect and Escalate Conflicts

* Never silently merge contradictory sources
* Capture the resolution and update affected documents

### 11.9 Automate Freshness Checks

* Review dates
* Broken links
* Deprecated versions
* Missing owners
* Superseded references

### 11.10 Minimise Retrieved Context

* Retrieve the smallest sufficient evidence set
* Avoid sending entire repositories or documentation collections unnecessarily

### 11.11 Protect Sensitive Sources

* Apply access controls before retrieval
* Redact indexed content
* Log restricted access
* Avoid copying secrets into prompts

### 11.12 Treat External Sources Carefully

* Prefer official documentation
* Record version and access date
* Distinguish external guidance from enterprise policy

### 11.13 Capture Knowledge Created During Work

* Approved decisions
* Resolved conflicts
* Review findings
* Operational lessons
* New knowledge gaps

### 11.14 Keep Humans Accountable

* AI agents may propose updates
* Owners approve authoritative changes
* The harness must not silently rewrite standards or policies

## Alpha Car Detailing Example

Apply the practices to the corporate fleet source catalogue and retrieval policy.

## Recommended Diagram or Table

**Best-Practice Checklist for Pull Requests**

---

# 12. Anti-patterns

## Section Title

**Common Knowledge Source Failures**

## Purpose

Identify practices that create unreliable AI reasoning and enterprise risk.

## Key Topics

## 12.1 Treating Every Repository File as Authoritative

A file’s presence does not establish trust.

## 12.2 Using Modification Date as the Only Freshness Signal

Recently copied legacy documents may appear current.

## 12.3 Allowing AI Agents to Choose Between Conflicting Policies

Consequential conflicts require human resolution.

## 12.4 Storing All Knowledge in Instructions

Large instruction files become difficult to maintain and reduce task relevance.

## 12.5 Indexing Sensitive Information Without Access Controls

Search convenience must not bypass security.

## 12.6 Relying Only on Source Code

Code may not reveal business intent, rationale, incidents, or external obligations.

## 12.7 Using External Documentation as Local Policy

Vendor guidance does not automatically become an enterprise standard.

## 12.8 Ignoring Historical Sources

Incident reports and review findings prevent repeated failures.

## 12.9 Retrieving Too Much Context

Large undifferentiated context increases noise and contradiction.

## 12.10 Failing to Record Source Versions

An agent decision cannot be reproduced without knowing which source version was used.

## 12.11 Allowing Orphaned Sources

Unowned documentation becomes stale and untrustworthy.

## 12.12 Silent Knowledge Reconciliation

Agents must not create an undocumented compromise between conflicting sources.

## 12.13 Citing Sources That Do Not Support the Claim

Citation presence is not enough; evidence must be relevant.

## 12.14 Treating Test Results as Business Authority

Tests show expected implementation behaviour but may encode obsolete assumptions.

## 12.15 Creating a Vector Database Without Governance

Semantic search does not solve ownership, authority, scope, freshness, or security.

## Alpha Car Detailing Example

For each anti-pattern, provide a brief failure scenario related to fleet booking, event contracts, customer eligibility, or duplicate-booking recovery.

## Recommended Diagram or Table

**Anti-pattern Table**

| Anti-pattern | Consequence | Corrective Action |
| ------------ | ----------- | ----------------- |

---

# 13. Architect’s Notes

## Section Title

**Architect’s Notes: Knowledge Is an Architectural Dependency**

## Purpose

Present senior-level observations about the relationship between enterprise architecture and knowledge management.

## Key Topics

### Architect’s Note 1

An AI agent’s reasoning quality is limited by the authority and quality of the evidence it receives.

### Architect’s Note 2

Repository intelligence should be treated as part of system architecture, not merely documentation hygiene.

### Architect’s Note 3

Architecture Decision Records preserve rationale, but they do not replace current service specifications.

### Architect’s Note 4

Observed production behaviour may reveal truth, but it does not automatically define desired behaviour.

### Architect’s Note 5

The most relevant source is not always the most authoritative source.

### Architect’s Note 6

Knowledge conflict is often a governance problem disguised as a retrieval problem.

### Architect’s Note 7

An AI harness should record which evidence influenced a decision, not merely which prompt was executed.

### Architect’s Note 8

A knowledge graph can improve relationships and discovery, but it cannot independently determine organisational authority.

### Architect’s Note 9

Knowledge Source design must account for service ownership, bounded contexts, and data sovereignty.

### Architect’s Note 10

Human approval remains mandatory when evidence is incomplete, conflicting, sensitive, or consequential.

## Alpha Car Detailing Example

Discuss how fleet booking rules cross the Fleet Management, Booking, Customer, Payment, and Station Management bounded contexts.

## Recommended Diagram or Table

**Table: Architectural Decision Type and Required Evidence**

---

# 14. Enterprise Tips

## Section Title

**Enterprise Tips for Operationalising Knowledge Sources**

## Purpose

Provide implementation guidance for organisations adopting Knowledge Source practices across multiple teams.

## Key Topics

### Enterprise Tip 1

Start with a source inventory before investing in advanced retrieval technology.

### Enterprise Tip 2

Define an authority model before selecting vector databases or search platforms.

### Enterprise Tip 3

Create standard metadata templates for ADRs, policies, runbooks, schemas, and product requirements.

### Enterprise Tip 4

Add source ownership and review dates to engineering governance.

### Enterprise Tip 5

Integrate source validation into CI/CD.

### Enterprise Tip 6

Require pull requests to update related Knowledge Sources when behaviour changes.

### Enterprise Tip 7

Create redacted summaries for sensitive incident and operational records.

### Enterprise Tip 8

Measure unresolved knowledge gaps and repeated clarification requests.

### Enterprise Tip 9

Maintain source-specific retention and archival policies.

### Enterprise Tip 10

Test retrieval using realistic engineering tasks rather than generic search questions.

### Enterprise Tip 11

Review whether agents consistently select the correct source hierarchy.

### Enterprise Tip 12

Use the same source identifiers across prompts, reviews, ADRs, tests, and harness logs.

## Alpha Car Detailing Example

Define an enterprise rollout beginning with the Booking and Fleet Management teams before expanding to the full Alpha Car Detailing platform.

## Recommended Diagram or Table

**Phased Adoption Roadmap**

1. Inventory
2. Classify
3. Assign ownership
4. Establish authority
5. Add metadata
6. Implement retrieval
7. Add citations
8. Automate validation
9. Integrate with harness
10. Measure and improve

---

# 15. Decision Points

## Section Title

**Decision Points for Knowledge Source Architecture**

## Purpose

Help architects make explicit, documented choices when establishing an enterprise knowledge system.

## Key Topics

## Decision Point 1: Repository-Local or Central Knowledge?

Consider:

* Team autonomy
* Shared standards
* Access boundaries
* Search experience
* Duplication
* Availability

## Decision Point 2: Centralised or Federated Ownership?

Consider:

* Domain expertise
* Governance consistency
* Bottlenecks
* Accountability
* Scale

## Decision Point 3: Keyword, Semantic, Graph, or Hybrid Retrieval?

Consider:

* Exact identifiers
* Domain language
* Relationship queries
* Operational cost
* Explainability

## Decision Point 4: What Constitutes an Authoritative Source?

Consider:

* Approval process
* Owner
* source type
* legal or regulatory precedence
* architecture governance
* product ownership

## Decision Point 5: How Will Freshness Be Measured?

Consider:

* Review cadence
* version alignment
* effective date
* observed drift
* automated checks

## Decision Point 6: When Are Citations Mandatory?

Consider:

* architecture
* security
* business rules
* data changes
* operational procedures
* external dependencies

## Decision Point 7: How Should Conflicts Be Escalated?

Consider:

* authority hierarchy
* issue workflow
* human approver
* temporary guidance
* resolution deadline

## Decision Point 8: Which Sources May Be Indexed?

Consider:

* sensitive information
* licences
* customer data
* production logs
* contractual restrictions
* external documentation rights

## Decision Point 9: What May the AI Harness Update?

Consider:

* indexes
* derived summaries
* knowledge-gap records
* proposed changes
* authoritative content

Recommended rule:

The harness may propose changes but must not silently change authoritative standards or policies.

## Decision Point 10: How Long Should Evidence Be Retained?

Consider:

* audit requirements
* reproducibility
* storage cost
* privacy
* legal retention
* incident analysis

## Alpha Car Detailing Example

Provide a decision record for the initial Alpha Car Detailing knowledge architecture:

* Federated source ownership
* Central metadata catalogue
* Hybrid keyword and semantic retrieval
* Mandatory citations for architecture, business, security, and integration decisions
* Human approval for conflicts and authoritative updates

## Recommended Diagram or Table

**Knowledge Architecture Decision Matrix**

---

# 16. Exercises

## Section Title

**Exercises**

## Purpose

Allow readers to apply chapter concepts to realistic enterprise engineering work.

## Exercise 1: Build a Knowledge Source Inventory

Identify at least 20 Knowledge Sources for Alpha Car Detailing and classify each as:

* Technical
* Business
* Architectural
* Operational
* Historical
* Quality
* External

## Exercise 2: Define Source Authority

Create an authority hierarchy for:

* Business rules
* Service boundaries
* API behaviour
* Event schemas
* Security controls
* Operational recovery

## Exercise 3: Design Source Metadata

Create a metadata template containing:

* Identifier
* Type
* Owner
* Authority
* Status
* Scope
* Version
* Effective date
* Review date
* Classification
* Location
* Related sources

## Exercise 4: Resolve a Source Conflict

The product requirement and API specification disagree on cancellation timing.

Document:

* Sources involved
* Nature of conflict
* Impact
* Temporary decision
* Required approver
* Final resolution
* Sources requiring updates

## Exercise 5: Identify Stale Knowledge

Review a simulated repository containing:

* Superseded ADR
* Old API specification
* Retired runbook
* Current incident report
* Recently copied legacy README

Determine which sources are stale and explain why.

## Exercise 6: Create a Retrieval Plan

For the recurring fleet booking prompt, list:

* Required source types
* Search queries
* Metadata filters
* Ranking criteria
* Expected citations
* Escalation conditions

## Exercise 7: Design an Access-Control Model

Define which sources may be accessed by:

* Lead Agent
* Developer Agent
* Reviewer Agent
* Validator Agent
* Evaluator Agent
* Human Product Owner
* Security Reviewer

## Exercise 8: Create a Knowledge Gap Register

Identify at least five missing or ambiguous areas in the Alpha Car Detailing domain.

## Exercise 9: Add Harness Integration

Design a harness step that:

* Retrieves sources
* Records source versions
* Detects conflicts
* Produces citations
* Stores knowledge gaps
* Requires human approval where necessary

## Exercise 10: Compare Platform Workflows

Write equivalent evidence-grounded workflows for:

* Claude Code
* GitHub Copilot
* OpenAI Codex

Identify which principles remain unchanged across platforms.

## Recommended Diagram or Table

Provide exercise worksheets for:

* Source inventory
* Authority matrix
* Conflict record
* Gap register
* Retrieval plan

---

# 17. Interview Questions

## Section Title

**Interview Questions**

## Purpose

Test conceptual, architectural, governance, and practical understanding of enterprise Knowledge Sources.

## Key Topics and Questions

1. What is a Knowledge Source in Enterprise AI Engineering?

2. How does a Knowledge Source differ from an Instruction?

3. How does a Knowledge Source differ from a Steering Note?

4. Why should an AI agent not rely only on source code?

5. What makes a Knowledge Source authoritative?

6. What is the difference between source authority and source reliability?

7. Why is modification date insufficient for determining freshness?

8. How should an AI agent respond when two authoritative sources conflict?

9. What metadata should be recorded for an enterprise Knowledge Source?

10. How can an organisation prevent stale Knowledge Sources from influencing AI agents?

11. What is source scope, and why does it matter?

12. How should sensitive Knowledge Sources be indexed and retrieved?

13. What is the role of citations in AI-assisted software development?

14. How can a harness record evidence used during an agent run?

15. What is a knowledge gap?

16. How should recurring knowledge gaps influence documentation priorities?

17. When should human approval be mandatory?

18. What are the advantages and disadvantages of centralised Knowledge Source ownership?

19. What are the advantages and disadvantages of federated ownership?

20. How do keyword, semantic, and graph retrieval differ?

21. Why does a vector database not solve knowledge governance?

22. How should external technical documentation be treated?

23. Can test results be considered authoritative? Explain the limitations.

24. How would you rank an ADR, a README, source code, and a pull request comment for an architecture decision?

25. How should AI agents behave when required knowledge is unavailable?

26. What controls are needed when integrating Knowledge Sources into Claude Code?

27. How do the same principles apply to GitHub Copilot and OpenAI Codex?

28. How can Knowledge Sources support auditability?

29. How should an organisation retire superseded Knowledge Sources?

30. Why should an AI harness propose rather than silently apply authoritative knowledge changes?

## Alpha Car Detailing Example

Include scenario-based interview questions involving:

* Conflicting cancellation rules
* Outdated event schemas
* Missing fleet approval policies
* Restricted incident reports
* Cross-service ownership disputes

## Recommended Diagram or Table

**Table: Interview Question Categories**

* Fundamentals
* Architecture
* Governance
* Security
* Retrieval
* Platform comparison
* Scenario analysis

---

# 18. Chapter Summary

## Section Title

**Chapter Summary**

## Purpose

Reinforce the chapter’s distinctions, architecture principles, and enterprise practices.

## Key Topics

The summary should reinforce that:

* Knowledge Sources provide evidence and context
* Instructions define persistent standards
* Skills define reusable procedures
* Prompts define the current task
* Roles define responsibility and authority
* Steering Notes define temporary direction
* Knowledge Sources must be governed by authority, ownership, freshness, reliability, and scope
* Retrieval must be task-aware and access-aware
* Search relevance does not equal authority
* Conflicts must be surfaced rather than silently resolved
* Missing knowledge must be acknowledged
* Citations create traceability
* Sensitive information requires strict controls
* Knowledge gaps should be managed as engineering risks
* AI harnesses should record evidence and recommend improvements
* Human approval remains mandatory for consequential knowledge decisions
* Claude Code, GitHub Copilot, and OpenAI Codex require the same underlying governance principles

## Alpha Car Detailing Example

Summarise how the corporate fleet booking implementation became safer and more accurate after the team introduced:

* A source catalogue
* An authority matrix
* Retrieval rules
* Citation requirements
* Conflict handling
* Knowledge gap tracking
* Human approval

## Recommended Diagram or Table

**Final Summary Diagram: The Complete Repository Intelligence Stack**

```text
Repository Discovery
        │
        ▼
Instructions ─ Persistent Standards
Skills ─────── Reusable Procedures
Prompts ────── Current Task
Roles ──────── Responsibility and Authority
Steering Notes ─ Temporary Direction
Knowledge Sources ─ Evidence and Context
        │
        ▼
AI Harness
        │
        ▼
Evidence-Grounded Engineering Outcome
```

---

# 19. Further Reading

## Section Title

**Further Reading**

## Purpose

Direct readers toward authoritative material that expands their understanding of documentation, architecture knowledge, information retrieval, security, and AI grounding.

## Key Topics

Recommended categories should include:

### Architecture and Decision Documentation

* Architecture Decision Records
* Software architecture documentation
* C4 model documentation
* Domain-driven design knowledge capture
* Service ownership documentation

### API and Integration Knowledge

* OpenAPI specifications
* AsyncAPI specifications
* JSON Schema
* Schema registries
* Consumer-driven contract testing

### Knowledge Retrieval

* Information retrieval fundamentals
* Keyword search
* Semantic search
* Retrieval-augmented generation
* Knowledge graphs
* Ranking and relevance

### Documentation Governance

* Documentation as code
* Version-controlled documentation
* Technical writing standards
* Documentation lifecycle management
* Records management

### Security and Access Control

* Least privilege
* Information classification
* Secret management
* Data loss prevention
* Secure AI usage
* Retrieval access controls

### Operational and Historical Knowledge

* Runbook design
* Incident management
* Post-incident reviews
* Observability
* Change management
* Pull request and code review practices

### Platform Documentation

* Official Claude Code documentation
* Official GitHub Copilot documentation
* Official OpenAI Codex documentation

Platform references should support implementation details while the chapter’s architectural and governance principles remain vendor-neutral.

## Alpha Car Detailing Example

Recommend that the team maintain a curated `references/knowledge-sources.md` file containing approved enterprise and external references relevant to the sample application.

## Recommended Diagram or Table

**Table: Further Reading by Knowledge Source Concern**

| Concern             | Reading Category               |
| ------------------- | ------------------------------ |
| Source authority    | Architecture governance        |
| Source structure    | Documentation as code          |
| Retrieval           | Information retrieval and RAG  |
| Event knowledge     | AsyncAPI and schema registries |
| Access control      | Security architecture          |
| Historical learning | Incident and change management |

---

# Proposed Chapter Flow

The chapter should progress through the following narrative:

1. An AI agent receives clear direction but lacks evidence.
2. The reader learns why repository files are not automatically trusted knowledge.
3. Knowledge Sources are defined and distinguished from other Repository Intelligence elements.
4. Source types and quality dimensions are introduced.
5. Discovery, indexing, retrieval, ranking, and citation are explained.
6. Conflicts, stale knowledge, missing knowledge, and access risks are addressed.
7. An enterprise knowledge architecture is designed.
8. Alpha Car Detailing receives a practical Knowledge Source catalogue.
9. Claude Code demonstrates the primary implementation workflow.
10. GitHub Copilot and Codex are compared where their workflows materially differ.
11. Governance, best practices, anti-patterns, decisions, and exercises reinforce the chapter.
12. The chapter closes Part II by showing how all Repository Intelligence elements work together.

# Expected Chapter Outcome

After completing Chapter 11, the reader should understand that an enterprise AI agent must not reason from undifferentiated repository content. It must work from sources that have identifiable authority, ownership, scope, freshness, reliability, and access controls.

The reader should also be prepared for Part III, where the harness will automate source retrieval, agent execution, validation, evaluation, evidence recording, and human approval workflows.

Chapter 11 outline is complete.
