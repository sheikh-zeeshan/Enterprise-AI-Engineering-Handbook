# Chapter 21 — Enterprise Harness Patterns

**Part III — Harness Engineering**  
**Target file:** `book/21-enterprise-harness-patterns.md`

This chapter follows the frozen handbook structure for Harness Engineering and the standard chapter template, including architecture discussion, diagrams, hands-on material, platform comparisons, best practices, anti-patterns, decision points, exercises, interview questions, summary, and further reading. AI development handbook AI development handbook

---

## 1. Story-Driven Opening — From One Harness to an Enterprise Platform

- Introduce Alpha Car Detailing after successful adoption of its first AI Engineering Harness.
- Initial state:
  - One development team.
  - One repository.
  - One Harness.
  - One predictable workflow.
- Enterprise growth:
  - Booking Service team.
  - Fleet Management team.
  - Billing team.
  - Customer Service team.
  - Shared Platform team.
  - Security and Architecture governance teams.
- Multiple microservice repositories begin adopting AI-assisted development.
- Problems emerge when every team independently builds its own Harness:
  - duplicated Skills;
  - inconsistent validation gates;
  - different approval models;
  - incompatible run artifacts;
  - inconsistent metrics;
  - duplicated integrations;
  - security inconsistencies;
  - difficult platform upgrades.
- Show the opposite problem:
  - one enormous centrally controlled Harness attempting to handle every repository, team, workflow, and technology.
- Explain why neither extreme scales.
- Introduce **Enterprise Harness Patterns** as reusable architectural building blocks.
- Establish the chapter principle:

> A Harness should be assembled from reusable patterns rather than implemented as one large custom workflow.

- Explain that patterns describe proven structures and responsibilities rather than prescribing one implementation technology.
- Position Chapter 21 as the architectural synthesis of Part III.

---

## 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define an Enterprise Harness Pattern.
- Explain why Harness architectures require reusable patterns.
- Distinguish repository topology patterns from execution workflow patterns.
- Compare centralized, distributed, team-specific, and shared platform Harness models.
- Select between single-agent and multi-agent execution.
- Design sequential, parallel, and fan-out/fan-in workflows.
- Apply deterministic validation, evaluation, approval, retry, and recovery patterns.
- Integrate Population processing, Run Workspace, and Scratchpad concepts.
- Design plugin, adapter, and tool-abstraction boundaries.
- Share Skills, Instructions, Repository Intelligence, and validation policies safely.
- Scale Harness execution across repositories and teams.
- Preserve security, isolation, governance, and auditability.
- Isolate failures so one repository or agent cannot destabilize enterprise execution.
- Combine patterns without producing an unmaintainable "mega-Harness."
- Select appropriate patterns based on risk, scale, autonomy, repository topology, and governance requirements.

---

# 3. Background — Why Enterprise Harnesses Need Patterns

## 3.1 From Workflow Automation to Architecture

- Small Harness versus enterprise Harness.
- Why a successful local workflow does not automatically become an enterprise architecture.
- Increasing complexity dimensions:
  - repositories;
  - teams;
  - agents;
  - services;
  - technologies;
  - environments;
  - regulations;
  - security boundaries;
  - approval requirements;
  - workloads.

## 3.2 The Scaling Problem

Describe how complexity grows when Alpha Car Detailing moves from:

```text
Developer
   ↓
Harness
   ↓
Repository
```

to:

```text
Many Teams
     ↓
Many Harness Runs
     ↓
Many Repositories
     ↓
Many AI Agents
     ↓
Shared Enterprise Services
     ↓
Governance + Security + Observability
```

## 3.3 What Is an Enterprise Harness Pattern?

Define a pattern as a reusable solution structure for a recurring Harness engineering problem.

A pattern should describe:

- context;
- problem;
- forces and trade-offs;
- responsibilities;
- architectural structure;
- applicability;
- consequences;
- failure modes;
- implementation options.

Clarify:

```text
Pattern ≠ Product
Pattern ≠ Script
Pattern ≠ Prompt
Pattern ≠ Mandatory Architecture
```

## 3.4 Why Patterns Matter

Cover:

- architectural consistency;
- reuse;
- reduced implementation cost;
- standardized governance;
- predictable observability;
- safer scaling;
- easier platform evolution;
- simpler onboarding;
- controlled variation between teams.

## 3.5 Pattern Categories

Introduce the chapter's pattern taxonomy:

1. Repository topology patterns.
2. Harness ownership patterns.
3. Agent topology patterns.
4. Workflow orchestration patterns.
5. Control and governance patterns.
6. Reliability patterns.
7. Workspace and state patterns.
8. Validation patterns.
9. Integration patterns.
10. Knowledge-sharing patterns.
11. Operations and scalability patterns.

---

# 4. Concepts — Enterprise Harness Pattern Catalog

## 4.1 Diagram — Enterprise Harness Pattern Catalog

Include a professional diagram such as:

```text
Enterprise Harness Patterns
│
├── Repository Topology
│   ├── Single Repository
│   ├── Multi-Repository
│   └── Monorepo
│
├── Harness Ownership
│   ├── Centralized Harness
│   ├── Distributed Harness
│   ├── Team-Specific Harness
│   └── Shared Platform Harness
│
├── Agent Topology
│   ├── Single-Agent
│   └── Multi-Agent
│
├── Execution Flow
│   ├── Sequential
│   ├── Parallel
│   └── Fan-Out / Fan-In
│
├── Control
│   ├── Deterministic Gates
│   ├── Human Approval
│   └── Validation + Evaluation
│
├── Reliability
│   ├── Retry
│   ├── Recovery
│   └── Failure Isolation
│
├── State
│   ├── Run Workspace
│   └── Scratchpad
│
├── Integration
│   ├── Plugin / Adapter
│   └── Tool Abstraction
│
└── Enterprise Operations
    ├── Metrics
    ├── Security
    ├── Governance
    ├── Scalability
    └── Extensibility
```

---

# 5. Repository Topology Patterns

## 5.1 Single-Repository Pattern

### Context

A Harness operates against one repository.

### Structure

```text
Repository
   │
   ├── Instructions
   ├── Skills
   ├── Tests
   └── Harness Configuration
            ↓
          Harness
```

### Appropriate when

- one application or service;
- autonomous team;
- isolated technology stack;
- limited cross-repository dependencies.

### Alpha Car Detailing scenario

- Booking Service maintained independently.

### Advantages

- simple ownership;
- straightforward Repository Intelligence;
- easy local testing;
- isolated execution.

### Limitations

- duplicated enterprise logic;
- duplicated Skills and gates;
- harder cross-service coordination.

---

## 5.2 Multi-Repository Pattern

### Context

One business change spans multiple repositories.

Example:

```text
Fleet Portal
Booking Service
Pricing Service
Notification Service
Infrastructure Repository
```

### Challenges

- repository ordering;
- contract dependencies;
- coordinated branches;
- multiple pull requests;
- cross-repository validation;
- partial failure;
- version compatibility.

### Alpha Car Detailing scenario

New corporate-fleet booking policy requires changes to:

- Booking API;
- Pricing Service;
- Fleet Service;
- shared event contracts.

### Core principle

The Harness coordinates repositories without pretending that they form one repository.

---

## 5.3 Diagram — Multi-Repository Orchestration

```text
                  Enterprise Run
                       │
                    Lead Agent
                       │
          ┌────────────┼─────────────┐
          ↓            ↓             ↓
     Booking Repo   Pricing Repo   Fleet Repo
          │            │             │
       Harness       Harness        Harness
       Worker        Worker         Worker
          │            │             │
          └────────────┼─────────────┘
                       ↓
               Integration Validation
                       ↓
                  Human Approval
```

---

## 5.4 Monorepo Pattern

- One repository containing multiple services or modules.
- Repository-level versus module-level Instructions.
- Targeted Repository Intelligence.
- Module-scoped execution.
- Path-aware validation.
- Avoiding full-repository execution when unnecessary.
- Shared dependencies and architectural boundaries.
- Changes spanning multiple modules.

### Comparison

Contrast:

```text
Multi-Repository
Independent repository boundaries

Monorepo
Shared repository boundary with internal domains/modules
```

---

# 6. Harness Ownership Patterns

## 6.1 Centralized Harness Pattern

A central platform owns and operates Harness execution.

### Structure

```text
                 Central AI Platform
                        │
                 Enterprise Harness
                        │
       ┌────────────────┼────────────────┐
       ↓                ↓                ↓
 Team A Repo        Team B Repo      Team C Repo
```

### Advantages

- consistent controls;
- centralized upgrades;
- shared observability;
- governance enforcement;
- reduced duplication.

### Risks

- bottleneck;
- reduced team autonomy;
- platform-team dependency;
- one-size-fits-all workflows.

---

## 6.2 Distributed Harness Pattern

Each team operates its own Harness.

```text
Team A             Team B             Team C
  │                  │                  │
Harness A          Harness B          Harness C
  │                  │                  │
Repo A             Repo B             Repo C
```

### Advantages

- autonomy;
- domain specialization;
- independent release cycles.

### Risks

- divergence;
- inconsistent security;
- duplicated tooling;
- inconsistent metrics.

---

## 6.3 Diagram — Centralized Versus Distributed Harness

Compare:

```text
Centralized

Teams
  ↓
Shared Harness Platform
  ↓
Repositories
```

with:

```text
Distributed

Team A → Harness A → Repo A
Team B → Harness B → Repo B
Team C → Harness C → Repo C
```

Include comparison dimensions:

- ownership;
- autonomy;
- governance;
- operational burden;
- upgrade model;
- failure blast radius.

---

## 6.4 Team-Specific Harness Pattern

- Shared foundation.
- Team-owned orchestration.
- Domain-specific Skills.
- Team-specific validation.
- Controlled configuration extension.

Example:

```text
Enterprise Harness Core
        ↓
Team Extension
        ↓
Team Repository
```

Explain the distinction between:

- **forking the Harness**, and
- **extending the Harness**.

Prefer controlled extension.

---

## 6.5 Shared Platform Harness Pattern

Introduce as the recommended enterprise middle ground for many organizations.

```text
                 Shared Platform Harness
                         │
       ┌─────────────────┼──────────────────┐
       │                 │                  │
Shared Security    Shared Metrics     Shared Gates
       │                 │                  │
       └─────────────────┼──────────────────┘
                         │
           ┌─────────────┼─────────────┐
           ↓             ↓             ↓
        Team A         Team B        Team C
       Extension      Extension     Extension
           ↓             ↓             ↓
         Repo A        Repo B        Repo C
```

### Shared platform provides

- orchestration runtime;
- common role definitions;
- enterprise gates;
- metrics;
- audit logging;
- credentials;
- tool adapters;
- artifact storage.

### Teams provide

- domain Instructions;
- repository Skills;
- acceptance criteria;
- specialized validation;
- local workflow configuration.

---

# 7. Agent Topology Patterns

## 7.1 Single-Agent Pattern

One AI Agent handles multiple stages.

Example:

```text
Prompt
  ↓
Agent
  ├── analyze
  ├── implement
  ├── test
  └── report
```

### Appropriate when

- low-risk tasks;
- prototypes;
- narrow modifications;
- small repositories.

### Limitations

- weak separation of duties;
- reduced independent review;
- harder attribution of failures.

---

## 7.2 Multi-Agent Pattern

Use specialized roles already established in the handbook:

```text
Lead
  ↓
Developer
  ↓
Reviewer
  ↓
Validator
  ↓
Evaluator
```

Discuss:

- specialization;
- independent evidence;
- responsibility boundaries;
- different tool permissions;
- separation of concerns.

Stress that **multi-agent does not mean uncontrolled agent-to-agent conversation**.

The Harness remains responsible for orchestration.

---

# 8. Workflow Orchestration Patterns

## 8.1 Sequential Workflow Pattern

```text
Lead
 ↓
Developer
 ↓
Reviewer
 ↓
Validator
 ↓
Evaluator
```

### Benefits

- deterministic progression;
- simple audit history;
- clear dependencies.

### Costs

- latency;
- idle resources;
- unnecessary serialization.

---

## 8.2 Parallel Workflow Pattern

Identify stages that can run independently.

Example:

```text
                 Implementation
                      │
          ┌───────────┼───────────┐
          ↓           ↓           ↓
      Unit Tests   Security    Static Analysis
          │           │           │
          └───────────┼───────────┘
                      ↓
                 Gate Decision
```

Explain:

- dependency analysis before parallelization;
- concurrency limits;
- shared workspace safety;
- deterministic aggregation.

---

## 8.3 Diagram — Sequential Versus Parallel Workflows

Show direct comparison of:

```text
Sequential
A → B → C → D
```

versus:

```text
Parallel
A
├→ B ─┐
├→ C ─┼→ E
└→ D ─┘
```

Discuss throughput versus complexity.

---

# 9. Fan-Out and Fan-In Pattern

## 9.1 Fan-Out

One orchestration step creates multiple independent work items.

Examples:

- repositories;
- programs;
- microservices;
- validation suites;
- files;
- feature variants.

## 9.2 Fan-In

Results return to a synchronization point.

```text
                    Work Item
                       │
                ┌──────┼──────┐
                ↓      ↓      ↓
              Task A Task B Task C
                │      │      │
                └──────┼──────┘
                       ↓
                 Aggregation
                       ↓
                  Gate Decision
```

## 9.3 Fan-In Decision Semantics

Explore:

- all must pass;
- minimum threshold;
- optional checks;
- fail-fast;
- partial completion;
- inconclusive status.

## 9.4 Diagram — Fan-Out and Fan-In

Use Alpha Car Detailing microservices as the running example.

---

# 10. Human Approval Pattern

## 10.1 Where Human Approval Belongs

High-risk examples:

- database migration;
- authorization changes;
- payment changes;
- production infrastructure;
- public API breaking changes;
- security policy modifications.

## 10.2 Approval as a Harness State

Avoid treating approval as an informal chat message.

Example:

```text
validated
   ↓
awaiting_approval
   ↓
approved
   ↓
continue
```

or:

```text
awaiting_approval
       ↓
    rejected
       ↓
    terminate
```

## 10.3 Approval Evidence

Record:

- who approved;
- what was approved;
- artifact version;
- commit;
- evidence package;
- timestamp;
- decision rationale where required.

---

# 11. Deterministic Gate Pattern

Reinforce concepts introduced earlier in Part III.

```text
AI Output
   ↓
Deterministic Gate
   ├── Build
   ├── Tests
   ├── Architecture Rules
   ├── Security Checks
   └── Contract Checks
         ↓
       Pass?
```

Discuss:

- independent deterministic execution;
- AI cannot self-declare a gate passed;
- gates produce evidence;
- gate policy versus gate implementation;
- local versus enterprise gates;
- mandatory versus advisory gates.

---

# 12. Retry and Recovery Pattern

## 12.1 Retry

Appropriate for transient failures:

- network failure;
- temporary tool outage;
- rate limit;
- unavailable dependency.

## 12.2 What Should Not Be Blindly Retried

- compilation errors;
- deterministic test failures;
- policy failures;
- invalid Instructions;
- unauthorized operations.

## 12.3 Retry Policy

Cover:

- retry count;
- backoff;
- bounded retry;
- retry classification;
- idempotency.

## 12.4 Recovery

Recovery after interruption:

```text
Run
 ↓
Checkpoint
 ↓
Failure
 ↓
Recover Workspace
 ↓
Resume from Safe Step
```

## 12.5 Resume Versus Restart

Explain decision criteria.

---

# 13. Failure Isolation Pattern

## 13.1 Why Failure Isolation Matters

One failure must not unnecessarily terminate an enterprise workload.

## 13.2 Isolation Boundaries

Potential boundaries:

- task;
- agent;
- repository;
- population item;
- team;
- environment.

## 13.3 Alpha Car Detailing Example

A failure in the Notification Service Harness run should not invalidate completed independent work in the Fleet Reporting Service.

## 13.4 Blast Radius

Introduce failure blast radius as a key design consideration.

---

# 14. Population Processing Pattern

Connect with Chapter 19.

## 14.1 Population as an Enterprise Pattern

Apply one governed workflow across many targets.

```text
Population
   │
   ├── Service A
   ├── Service B
   ├── Service C
   └── Service D
```

## 14.2 Population Uses

- migration;
- dependency upgrade;
- security remediation;
- policy enforcement;
- API modernization;
- large-scale refactoring.

## 14.3 Population + Fan-Out/Fan-In

Explain how the patterns compose.

## 14.4 Population Failure Isolation

- one target fails;
- other targets continue;
- aggregate results;
- retry failed population members.

---

# 15. Run Workspace Pattern

Define the run-scoped execution boundary.

Possible structure:

```text
/run/{run-id}/
│
├── context/
├── repository-intelligence/
├── prompts/
├── scratchpads/
├── plans/
├── patches/
├── evidence/
├── validation/
├── evaluation/
├── logs/
└── artifacts/
```

Cover:

- per-run isolation;
- deterministic artifact locations;
- resumability;
- auditability;
- retention;
- cleanup;
- parallel execution safety.

---

# 16. Scratchpad Pattern

## 16.1 Purpose

Temporary agent working state.

## 16.2 Suitable Scratchpad Content

- observations;
- hypotheses;
- files under investigation;
- unresolved questions;
- temporary reasoning artifacts;
- work progress;
- intermediate decisions.

## 16.3 Scratchpad Is Not Authoritative Knowledge

Distinguish:

```text
Scratchpad
Temporary, run-scoped

Repository Intelligence
Governed repository knowledge

Instructions
Authoritative engineering requirements
```

## 16.4 Promotion Pattern

Useful discoveries may be proposed for promotion:

```text
Scratchpad Discovery
        ↓
Review
        ↓
Approved?
   ┌────┴────┐
   No        Yes
   ↓          ↓
Discard    Governed Knowledge
```

---

# 17. Validation and Evaluation Pattern

Combine two independent concerns without merging their responsibilities.

```text
Implementation
     ↓
Validation
"Is it objectively correct?"
     ↓
Evaluation
"Does it satisfy the intended outcome?"
```

Cover:

- deterministic evidence from Validator;
- qualitative/business assessment by Evaluator;
- evidence handoff;
- conflicting outcomes.

Example:

```text
Validation: PASS
Evaluation: FAIL
```

Possible because technically correct software may still fail the business objective.

---

# 18. Plugin and Adapter Pattern

## 18.1 Problem

Enterprise Harnesses interact with changing tools:

- source control;
- AI platforms;
- CI systems;
- issue trackers;
- artifact stores;
- security scanners;
- cloud services.

Hard-coding every integration creates coupling.

## 18.2 Adapter Boundary

```text
Harness
   ↓
Tool Interface
   ↓
┌───────────┬───────────┬───────────┐
│ GitHub    │ Azure     │ Future    │
│ Adapter   │ DevOps    │ Adapter   │
└───────────┴───────────┴───────────┘
```

## 18.3 Plugin Responsibilities

- translation;
- authentication;
- capability discovery;
- error normalization;
- telemetry;
- version handling.

## 18.4 Avoid Plugin Leakage

Core orchestration should not depend on vendor-specific response structures.

---

# 19. Tool Abstraction Pattern

Distinguish Tool Abstraction from Plugin/Adapter.

Example:

```text
Harness Requirement:
"Run source-code security scan"

            ↓

SecurityScanner Interface

       ┌────┼────┐
       ↓    ↓    ↓
   Scanner A B    C
```

Discuss abstractions for:

- source control;
- AI Agent provider;
- test runner;
- security scanner;
- deployment platform;
- work-item system;
- artifact storage.

---

# 20. Repository Intelligence Integration Pattern

Explain how each run receives repository-specific knowledge.

```text
Repository
   ↓
Discovery / Intelligence
   ↓
Governed Repository Context
   ↓
Harness Run
   ↓
Agents
```

Cover:

- scope;
- freshness;
- repository boundaries;
- caching;
- inheritance;
- invalidation;
- cross-repository knowledge.

For multi-repository execution:

```text
Repo A Intelligence
Repo B Intelligence
Repo C Intelligence
        ↓
Cross-Repository Context
```

Avoid merging unrelated repository knowledge indiscriminately.

---

# 21. Shared Skills and Instructions Pattern

## 21.1 Layered Instructions

```text
Enterprise Instructions
        ↓
Platform Instructions
        ↓
Team Instructions
        ↓
Repository Instructions
        ↓
Task Context
```

Define precedence and conflict handling.

## 21.2 Shared Skills

Examples:

- create API endpoint;
- create integration event;
- implement outbox;
- create idempotent consumer;
- add telemetry.

## 21.3 Local Skills

Domain-specific workflows remain closer to repositories.

## 21.4 Versioning Shared Assets

- Skill version;
- Instruction version;
- compatibility;
- controlled rollout;
- deprecation.

---

# 22. Shared Platform Harness Pattern — Detailed Architecture

## 22.1 Diagram — Shared Platform Harness

```text
                       Enterprise AI Platform
                               │
        ┌──────────────────────┼──────────────────────┐
        │                      │                      │
  Shared Harness Core   Enterprise Policies    Shared Observability
        │                      │                      │
        ├──────────────┬───────┴───────┬──────────────┤
        ↓              ↓               ↓              ↓
  Tool Adapters    Shared Skills   Common Gates   Artifact Store
        │
 ┌──────┼──────────┐
 ↓      ↓          ↓
Team A Team B    Team C
 │      │          │
Repo   Repo       Repo
Config Config     Config
```

## 22.2 Platform Responsibilities

- runtime;
- orchestration;
- identity;
- secrets;
- telemetry;
- shared policies;
- adapters;
- artifacts;
- run history.

## 22.3 Team Responsibilities

- domain context;
- acceptance criteria;
- local Instructions;
- repository-specific Skills;
- additional validation;
- approved extensions.

---

# 23. Metrics and Observability Pattern

Connect with Chapter 20.

## 23.1 Three Levels of Observability

### Run-level

- duration;
- status;
- retries;
- token/tool consumption;
- gate outcomes.

### Workflow-level

- agent durations;
- validation time;
- review loops;
- approval wait time.

### Enterprise-level

- repository adoption;
- success rates;
- failure trends;
- throughput;
- cost;
- repeated failure categories.

## 23.2 Correlation

Every run should have:

```text
Run ID
Task ID
Repository ID
Commit
Agent execution IDs
Gate executions
Artifacts
```

## 23.3 Metrics Without Surveillance

Separate engineering-system metrics from individual developer performance judgments.

---

# 24. Security and Isolation Pattern

## 24.1 Isolation Boundaries

- repository;
- tenant;
- team;
- agent;
- workspace;
- credentials;
- network;
- secrets.

## 24.2 Least Privilege

Different agents may receive different permissions.

Example:

```text
Developer Agent
Write source code

Reviewer Agent
Read-only repository access

Validator
Execute validation

Deployment Step
Environment-specific credentials
```

## 24.3 Secret Handling

Secrets must not be copied into:

- prompts;
- Scratchpads;
- logs;
- evaluation reports;
- persisted agent transcripts.

## 24.4 Untrusted Repository Content

Discuss:

- prompt injection risk;
- malicious files;
- generated scripts;
- untrusted dependencies.

## 24.5 Sandbox Boundaries

Use sandboxing where appropriate for:

- builds;
- tests;
- agent-generated scripts;
- external tools.

---

# 25. Governance and Approval Pattern

## 25.1 Central Governance With Team-Level Autonomy

Alpha Car Detailing example:

```text
Enterprise
├── Mandatory security gates
├── Audit requirements
└── Approved AI providers

Teams
├── Domain Skills
├── Workflow configuration
├── Local validation
└── Repository Instructions
```

## 25.2 Policy Layers

Separate:

- mandatory policy;
- recommended defaults;
- team configuration;
- task configuration.

## 25.3 Policy as Configuration

Avoid burying governance inside orchestration code.

---

# 26. Scalability Patterns

## 26.1 Horizontal Worker Scaling

```text
Run Queue
   │
┌──┼──┬──┐
↓  ↓  ↓  ↓
W1 W2 W3 W4
```

## 26.2 Queue-Based Execution

Advantages:

- load leveling;
- retry;
- distributed workers;
- prioritization.

## 26.3 Concurrency Controls

Control by:

- repository;
- team;
- AI provider;
- external API;
- environment.

## 26.4 Resource-Aware Scheduling

Different workloads need different resources:

- code analysis;
- compilation;
- integration tests;
- browser testing;
- container builds.

## 26.5 Backpressure

Prevent downstream systems from being overwhelmed.

---

# 27. Extensibility Pattern

Design extension points intentionally.

Possible extension points:

```text
BeforeRun
AfterDiscovery
BeforeAgent
AfterAgent
BeforeValidation
AfterValidation
BeforeApproval
AfterRun
```

Explain relationship to:

- hooks;
- plugins;
- Skills;
- tool adapters.

Distinguish safe extension from arbitrary scripting.

---

# 28. Pattern Selection

## 28.1 Pattern Selection Questions

Ask:

- How many repositories?
- How many teams?
- What level of team autonomy?
- How much central governance?
- What is the risk level?
- Are workflows independent?
- Are tasks naturally parallelizable?
- Are shared tools mandatory?
- Do runs require human approval?
- How frequently will integrations change?
- What isolation boundaries exist?

---

# 29. Diagram — Pattern Selection Decision Model

Example:

```text
Start
  │
  ├─ One repository?
  │      ├─ Yes → Single-Repository Pattern
  │      └─ No
  │          │
  │          ├─ One repository containing many modules?
  │          │      └─ Monorepo Pattern
  │          │
  │          └─ Independent repositories?
  │                 └─ Multi-Repository Pattern
  │
  ├─ Need enterprise consistency?
  │      └─ Shared Platform Pattern
  │
  ├─ Teams require strong autonomy?
  │      └─ Team Extension / Distributed Pattern
  │
  ├─ Independent tasks?
  │      └─ Parallel / Fan-Out Pattern
  │
  ├─ High-risk change?
  │      └─ Human Approval Pattern
  │
  └─ External tools likely to vary?
         └─ Plugin + Tool Abstraction Pattern
```

Explain that pattern selection is multidimensional rather than a single decision tree.

---

# 30. Combining Patterns

## 30.1 Patterns Are Composable

Example enterprise solution:

```text
Shared Platform Harness
        +
Multi-Repository
        +
Multi-Agent
        +
Fan-Out / Fan-In
        +
Run Workspace
        +
Deterministic Gates
        +
Human Approval
        +
Plugin Adapters
```

## 30.2 Alpha Car Detailing Combined Scenario

Corporate Fleet Booking enhancement:

1. Lead Agent analyzes the change.
2. Multi-repository orchestration identifies affected services.
3. Repository-specific Run Workspaces are created.
4. Development work fans out.
5. Each repository receives its own Developer Agent.
6. Shared security and architecture gates execute.
7. Results fan in.
8. Cross-service integration tests execute.
9. Validator verifies deterministic evidence.
10. Evaluator checks business outcome.
11. Human approval is required because the change affects billing.
12. Pull requests are created.
13. Metrics and artifacts are stored centrally.

---

# 31. When Not to Combine Patterns

Warn against architectural overengineering.

Do not introduce:

- multi-agent execution where one agent is sufficient;
- distributed Harness deployment for one small team;
- fan-out where strict ordering is required;
- plugins where only one stable implementation exists;
- cross-repository orchestration for unrelated repositories.

Core principle:

> Pattern composition should reduce complexity, not merely relocate it.

---

# 32. Anti-Patterns

Introduce major Harness anti-patterns.

## 32.1 The Mega-Harness

One workflow knows everything about every team and repository.

Symptoms:

- giant orchestration file;
- repository-specific conditions everywhere;
- fragile branching;
- impossible independent upgrades.

---

## 32.2 Copy-and-Paste Harnesses

Every team clones the original Harness and modifies it.

Consequences:

- drift;
- duplicated fixes;
- policy inconsistency;
- version fragmentation.

---

## 32.3 Vendor Leakage

Core Harness logic depends directly on one vendor's API or command format.

---

## 32.4 Agent-as-Orchestrator

Allowing an AI Agent to make uncontrolled execution decisions that should belong to the Harness.

Reinforce:

```text
Harness controls execution.
Agents perform bounded work.
```

---

## 32.5 AI-as-Gate

Asking an AI Agent:

> "Did all tests pass?"

instead of executing the tests deterministically.

---

## 32.6 Shared Mutable Workspace

Multiple parallel agents modifying the same uncontrolled workspace.

---

## 32.7 Unbounded Parallelism

Starting arbitrary numbers of agents, tests, or repository runs.

---

## 32.8 Infinite Retry

Retrying deterministic failures without changing conditions.

---

## 32.9 Hidden Governance

Security and compliance requirements embedded as undocumented workflow code.

---

## 32.10 Shared Credentials

All agents and repositories using a powerful shared identity.

---

## 32.11 Scratchpad as Permanent Knowledge

Temporary observations becoming authoritative without governance.

---

## 32.12 Centralization Without Extension Points

A central platform forces every team into the same exact workflow.

---

## 32.13 Distributed Everything

Every team chooses:

- different Harness;
- different gates;
- different metrics;
- different security models;
- different evidence formats.

Explain why enterprise architecture usually needs a balance.

---

# 33. Architecture Discussion — Reference Enterprise Harness Architecture

Provide a complete conceptual architecture.

```text
                        Users / CI / Work Items
                                 │
                          Harness API / Queue
                                 │
                        Enterprise Orchestrator
                                 │
            ┌────────────────────┼────────────────────┐
            │                    │                    │
      Repository Intel      Policy Engine        Run Manager
            │                    │                    │
            └────────────────────┼────────────────────┘
                                 │
                            Run Workspace
                                 │
                              Lead Agent
                                 │
                      ┌──────────┼──────────┐
                      ↓          ↓          ↓
                    Repo A     Repo B     Repo C
                      │          │          │
                  Developer  Developer  Developer
                      │          │          │
                      └──────────┼──────────┘
                                 ↓
                           Reviewer Layer
                                 ↓
                        Deterministic Gates
                                 ↓
                            Validator
                                 ↓
                            Evaluator
                                 ↓
                       Human Approval Gate
                                 ↓
                             PR / Output
                                 │
              ┌──────────────────┼──────────────────┐
              ↓                  ↓                  ↓
          Metrics            Artifacts           Audit
```

Discuss which concepts are patterns versus implementation components.

---

# 34. Hands-on Example — Alpha Car Detailing Enterprise Harness

Build the conceptual scenario around several teams.

## 34.1 Organization

```text
Alpha Car Detailing Engineering
│
├── Booking Team
├── Fleet Team
├── Billing Team
├── Notification Team
├── Platform Team
└── Architecture/Security
```

## 34.2 Repository Model

```text
alpha-booking
alpha-fleet
alpha-billing
alpha-notifications
alpha-shared-contracts
alpha-platform
```

## 34.3 Requirement

Add **Priority Corporate Fleet Booking**.

Requirement affects:

- Booking Service;
- Fleet Service;
- Billing Service;
- shared contracts.

## 34.4 Selected Patterns

- Shared Platform Harness.
- Multi-repository orchestration.
- Multi-agent.
- Fan-out/fan-in.
- Run Workspace.
- Scratchpad.
- Shared Skills.
- Deterministic Gates.
- Validator/Evaluator.
- Human approval.
- Plugin/adapters.

## 34.5 Execution Walkthrough

Step through:

```text
Request
  ↓
Lead Analysis
  ↓
Affected Repository Detection
  ↓
Run Workspace Creation
  ↓
Fan-Out
  ↓
Parallel Repository Changes
  ↓
Repository Gates
  ↓
Fan-In
  ↓
Integration Validation
  ↓
Evaluation
  ↓
Approval
  ↓
Pull Requests
```

## 34.6 Failure Scenario

Billing Service fails validation.

Show how:

- Booking and Fleet results remain preserved.
- Billing failure is isolated.
- Billing work can be retried.
- cross-service integration remains blocked.
- approval does not begin prematurely.

---

# 35. Claude Code Considerations

The handbook remains Claude-first while keeping the architecture vendor-neutral, consistent with the project's established philosophy. AI development handbook

Cover:

- `CLAUDE.md` as repository Instruction source.
- Claude Code Skills and commands.
- role-specific execution.
- repository-scoped context.
- use of subagents where appropriate.
- hooks as deterministic lifecycle extensions.
- workspace isolation.
- Claude-specific capabilities should sit behind Harness abstractions where enterprise portability matters.
- avoid designing the enterprise architecture around Claude-specific behavior.

### Alpha Car Detailing example

Show how the shared Harness may launch Claude Code for Developer work while deterministic validation remains outside the agent.

---

# 36. GitHub Copilot Comparison

Discuss only where materially different.

Cover:

- repository instructions;
- GitHub-native development workflow;
- pull request integration;
- coding-agent workflow where available;
- GitHub Actions as deterministic execution infrastructure;
- repository and organization policies;
- interaction with shared Harness services.

Explain that the **pattern remains the same** even when the execution mechanism differs.

Example:

```text
Shared Harness Pattern
       ↓
AI Provider Adapter
       ↓
GitHub Copilot
```

---

# 37. OpenAI Codex Comparison

Cover:

- Codex as another agent implementation behind enterprise Harness boundaries;
- repository/task execution;
- tool usage;
- sandbox/workspace concerns;
- deterministic validation external to the model;
- compatibility with multi-agent or isolated worker execution.

Reinforce:

```text
Harness Pattern
      ≠
AI Vendor
```

The book's engineering principles are intended to remain vendor-neutral even though Claude is the primary implementation platform. AI development handbook

---

# 38. Best Practices

Cover recommendations such as:

1. Start with the simplest pattern that satisfies the requirement.
2. Keep orchestration inside the Harness.
3. Keep AI Agent responsibilities bounded.
4. Separate shared platform capabilities from repository-specific behavior.
5. Prefer extension over Harness forks.
6. Make deterministic gates reusable enterprise components.
7. Use explicit Run Workspace boundaries.
8. Isolate parallel agents and repositories.
9. Treat Scratchpads as temporary state.
10. Version shared Skills and Instructions.
11. Use plugin boundaries for changing external tools.
12. Abstract vendor-specific AI platforms where appropriate.
13. Record evidence for every critical transition.
14. Design retry around failure classification.
15. Make operations idempotent where possible.
16. Design for resumability.
17. Establish concurrency limits.
18. Apply least privilege to agents and tools.
19. Make approval gates explicit workflow states.
20. Correlate logs, metrics, evidence, and artifacts through the Run ID.
21. Preserve team autonomy inside enterprise guardrails.
22. Measure before optimizing.
23. Prefer composition over one massive Harness workflow.

---

# 39. Architect's Notes

Include architectural observations such as:

### Architect's Note — Patterns Are Boundaries

The value of a Harness pattern often comes less from code reuse and more from defining clear responsibility boundaries.

### Architect's Note — Centralize Policy, Not Every Decision

Central platforms should enforce enterprise invariants while allowing teams to make domain-specific decisions.

### Architect's Note — Parallelism Is an Architecture Decision

Parallel execution affects:

- state;
- locking;
- cost;
- observability;
- failure handling;
- reproducibility.

It should not be added merely for speed.

### Architect's Note — Abstraction Has a Cost

Do not create an abstraction solely because another vendor might theoretically be used someday.

Abstract volatile or strategically important dependencies.

---

# 40. Enterprise Tips

Include callouts such as:

### Enterprise Tip — Publish a Pattern Catalog

Maintain approved Harness patterns with:

- diagrams;
- reference implementations;
- configuration templates;
- security guidance;
- selection criteria.

### Enterprise Tip — Build a Golden Path

Offer teams a supported default Harness architecture rather than requiring every team to design its own.

### Enterprise Tip — Separate Mandatory and Optional Controls

Clearly identify:

```text
Mandatory enterprise controls
Recommended defaults
Team extensions
```

### Enterprise Tip — Measure Pattern Effectiveness

Track whether patterns actually improve:

- success rates;
- cycle time;
- failure recovery;
- adoption;
- governance compliance.

---

# 41. Decision Points

Include structured decisions.

## Decision Point 1 — Centralized or Distributed?

Consider:

- organizational scale;
- autonomy;
- compliance;
- platform maturity;
- skill distribution.

## Decision Point 2 — Single-Agent or Multi-Agent?

Consider:

- risk;
- complexity;
- need for independent review;
- execution cost.

## Decision Point 3 — Sequential or Parallel?

Consider:

- dependency;
- shared state;
- runtime;
- cost;
- deterministic ordering.

## Decision Point 4 — Single Repository or Multi-Repository Orchestration?

Consider:

- ownership;
- deployment independence;
- contract changes;
- release coordination.

## Decision Point 5 — Shared Skill or Repository Skill?

Ask whether the behavior is:

- enterprise-wide;
- technology-specific;
- domain-specific;
- repository-specific.

## Decision Point 6 — Automatic or Human Approval?

Base decision on risk rather than developer convenience.

## Decision Point 7 — Plugin or Direct Integration?

Use plugins/adapters when volatility, reuse, or provider interchangeability justifies the abstraction.

---

# 42. Pattern Selection Matrix

Include a table in the eventual chapter:

| Situation | Primary Pattern | Supporting Patterns |
| --- | --- | --- |
| One team, one service | Single Repository | Sequential, Run Workspace |
| Many teams using common controls | Shared Platform Harness | Shared Skills, Gates |
| Independent teams | Distributed/Team-Specific | Governance, Metrics |
| Cross-service feature | Multi-Repository | Fan-Out/Fan-In |
| Bulk modernization | Population Processing | Parallel, Failure Isolation |
| High-risk production change | Human Approval | Validation, Evaluation |
| Multiple external providers | Plugin/Adapter | Tool Abstraction |
| Large independent workload | Parallel Workflow | Queue, Failure Isolation |
| Complex outcome assessment | Multi-Agent | Validator + Evaluator |

---

# 43. Pattern Trade-Off Matrix

Compare major patterns against:

- simplicity;
- autonomy;
- governance;
- scalability;
- operational cost;
- failure blast radius;
- extensibility;
- observability.

Example patterns:

- centralized;
- distributed;
- team-specific;
- shared platform.

---

# 44. Common Pattern Combinations

## 44.1 Small Team

```text
Single Repository
+
Single/Small Multi-Agent
+
Sequential Workflow
+
Deterministic Gates
```

## 44.2 Enterprise Product Team

```text
Shared Platform Harness
+
Team-Specific Configuration
+
Multi-Agent
+
Run Workspace
+
Shared Gates
```

## 44.3 Microservice Feature

```text
Multi-Repository
+
Fan-Out/Fan-In
+
Parallel Execution
+
Cross-Service Validation
```

## 44.4 Enterprise Migration

```text
Population Processing
+
Queue-Based Workers
+
Parallel Execution
+
Failure Isolation
+
Retry/Recovery
```

## 44.5 High-Risk Change

```text
Multi-Agent
+
Deterministic Validation
+
Evaluation
+
Human Approval
+
Audit Evidence
```

---

# 45. Exercises

## Exercise 1 — Choose a Harness Topology

Alpha Car Detailing has ten teams and twenty repositories.

Design:

- centralized;
- distributed;
- or shared platform Harness architecture.

Explain the trade-offs.

## Exercise 2 — Multi-Repository Feature

Design the Harness flow for a feature affecting:

- Booking;
- Billing;
- Notifications;
- shared contracts.

Identify:

- fan-out point;
- fan-in point;
- validation gates;
- approval point.

## Exercise 3 — Parallelization

Given:

```text
Build
Unit Tests
Integration Tests
Architecture Validation
Security Scan
Evaluation
```

Identify which steps can safely execute in parallel.

## Exercise 4 — Failure Recovery

A three-hour population run fails after processing 80 of 100 services.

Design:

- checkpoint strategy;
- retry behavior;
- failure isolation;
- resume logic.

## Exercise 5 — Shared Platform Design

Identify which components belong centrally versus inside individual repositories.

## Exercise 6 — Pattern Anti-Pattern Review

Given a large Harness containing hundreds of repository-specific `if` statements, identify the architectural problems and propose a pattern-based redesign.

## Exercise 7 — Security Architecture

Design identities and permissions for:

- Lead;
- Developer;
- Reviewer;
- Validator;
- deployment process.

## Exercise 8 — Pattern Combination

Select patterns for Alpha Car Detailing's organization where:

- 15 repositories exist;
- teams require autonomy;
- enterprise security gates are mandatory;
- high-risk changes require architects' approval;
- multiple AI providers may be supported.

---

# 46. Interview Questions

Include questions ranging from conceptual to architecture-level.

1. What is an Enterprise Harness Pattern?
2. Why should Harness architecture use reusable patterns?
3. What is the difference between a centralized and distributed Harness?
4. When is a Shared Platform Harness preferable?
5. How does a team-specific Harness differ from a completely independent Harness?
6. What challenges appear in multi-repository Harness orchestration?
7. How does the monorepo pattern differ from the multi-repository pattern?
8. When should an enterprise use a multi-agent Harness?
9. What is the difference between sequential and parallel Harness workflows?
10. Explain fan-out and fan-in.
11. How should partial failures be handled during fan-out?
12. Why should deterministic gates remain independent of AI Agents?
13. What is the role of human approval in an enterprise Harness?
14. How do retry and recovery differ?
15. When should a failure not be automatically retried?
16. What is failure isolation?
17. How does Population processing combine with fan-out/fan-in?
18. What is the purpose of a Run Workspace?
19. Why should Scratchpad state not become authoritative knowledge automatically?
20. What is the difference between a plugin pattern and tool abstraction?
21. How should Repository Intelligence work in multi-repository workflows?
22. How can an organization share Skills without eliminating team autonomy?
23. What should be centralized in a Shared Platform Harness?
24. How do you scale Harness workers horizontally?
25. What is backpressure and why does it matter?
26. How should agent credentials be isolated?
27. What is the mega-Harness anti-pattern?
28. Why is copying a Harness for every team problematic?
29. How would you choose between centralized and distributed Harness architectures?
30. How can an enterprise remain vendor-neutral while primarily using Claude Code?

---

# 47. Chapter Summary

Summarize the major lessons:

- Enterprise Harnesses should be designed from reusable architectural patterns.
- Repository topology affects Harness topology.
- Centralized and distributed architectures represent trade-offs rather than universally correct choices.
- Shared Platform Harnesses can balance central governance with team autonomy.
- Single-agent and multi-agent patterns should be selected based on complexity and risk.
- Sequential execution is simpler; parallel execution improves throughput where dependencies permit it.
- Fan-out/fan-in enables large-scale multi-target processing.
- Deterministic gates remain outside AI judgment.
- Human approval belongs at explicit risk boundaries.
- Retry, recovery, idempotency, and failure isolation make long-running workflows resilient.
- Run Workspace and Scratchpad patterns provide controlled temporary execution state.
- Repository Intelligence, Instructions, and Skills should be layered rather than duplicated.
- Plugins and tool abstractions reduce inappropriate vendor coupling.
- Metrics, security, governance, and observability must be part of the architecture rather than retrofits.
- Patterns should be composed deliberately.
- The goal is not to create the most sophisticated Harness; it is to create the simplest reusable architecture capable of operating safely at the required enterprise scale.

Reinforce the central principle:

> **A Harness should be designed from reusable patterns rather than as one large custom workflow.**

---

# 48. Further Reading

Organize eventual references around:

- enterprise architecture patterns;
- distributed systems patterns;
- workflow orchestration;
- queue-based load leveling;
- competing consumers;
- retry and circuit-breaker patterns;
- bulkhead/failure-isolation patterns;
- idempotency;
- distributed tracing;
- platform engineering;
- golden paths;
- multi-repository CI/CD;
- secure workload isolation;
- human-in-the-loop AI systems;
- agent orchestration;
- AI governance;
- Claude Code documentation;
- GitHub Copilot documentation;
- OpenAI Codex documentation.

Keep this section vendor-neutral in its architectural recommendations while retaining Claude as the handbook's primary implementation platform, as required by the project's established constraints. AI development handbook

---

**Chapter 21 outline is complete.**
