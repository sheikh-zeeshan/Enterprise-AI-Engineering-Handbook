# **Chapter 27 — Planning Mode**

**Part IV — Claude Engineering**

*Enterprise AI Engineering Handbook*

## **1. Story-driven Opening**

### **1.1 The Corporate Fleet Booking Change**

Alpha Car Detailing operates a nationwide network of vehicle washing and detailing stations. Its enterprise booking platform supports individual customers, corporate fleets, government departments, and commercial vehicle operators.

A new business requirement arrives from the corporate operations team.

Corporate customers must be able to reserve detailing appointments for multiple vehicles in a single booking request. Each corporate account has an agreed booking limit, and the platform must prevent customers from exceeding their permitted capacity. The system must also ensure that a booking cannot consume the same capacity twice when requests are retried because of network failures.

At first glance, the requirement appears straightforward: introduce a corporate fleet booking endpoint, validate the booking limit, and persist the reservation.

A developer working without AI assistance would ordinarily investigate the existing booking implementation, identify affected components, review business constraints, and prepare an implementation approach before modifying the system.

An AI coding agent faces the same engineering responsibility, but its ability to generate changes quickly introduces an additional risk.

The agent may begin implementing the first interpretation of the requirement before understanding its architectural consequences.

Consider the following request submitted to Claude Code:

> Implement corporate fleet booking. Corporate customers must be able to reserve appointments for multiple vehicles while respecting their booking limits. Follow the existing architecture and add appropriate tests.

The request identifies the desired capability. It does not establish how the existing booking system represents corporate accounts, enforces capacity, handles concurrent reservations, processes retries, or publishes booking events.

Without deliberate investigation, an agent might introduce a new controller, service, and repository method. It might implement a booking-limit check by counting existing reservations and rejecting requests that exceed the configured limit.

The implementation could compile successfully. Unit tests might pass. The new endpoint might appear correct during a demonstration.

Yet two simultaneous requests could both pass the limit check before either commits its reservation. A retried request might create duplicate bookings. An event might be published before the booking transaction commits, leaving downstream systems with information about a reservation that does not exist.

These are not primarily code-generation problems.

They are failures to understand the task and its operational context before execution.

### **1.2 The Hidden Complexity**

The Lead Agent examines the request and identifies several unresolved engineering questions.

Does the corporate booking limit represent the number of vehicles per request, total active reservations, or total bookings during a contract period?

Does the limit apply across all stations or independently to each station?

Should cancelled bookings release previously reserved capacity?

What happens when two authorized users from the same corporate account submit requests concurrently?

How should the system distinguish a legitimate second booking from a retry of an earlier request?

Does the existing architecture already provide a transactional outbox, an idempotency mechanism, or a shared booking-limit policy?

What evidence will demonstrate that the change is correct?

Each question can materially affect the implementation.

A limit based on active reservations requires different data access and transaction behavior from a limit based on monthly completed bookings. A global corporate limit requires a different consistency boundary from a station-specific limit. Idempotency and concurrency must be addressed separately because preventing duplicate retries does not necessarily prevent two distinct requests from exceeding capacity.

An agent that starts coding before investigating these questions is not accelerating the full engineering lifecycle. It is moving uncertainty from the analysis phase into the implementation phase, where mistakes become more expensive.

### **1.3 Introducing Planning Mode**

The team decides that the corporate fleet booking change must go through Planning Mode before implementation.

Claude is instructed to inspect the repository, read applicable Instructions, identify existing booking components, select relevant Skills, and prepare a proposed implementation plan.

During this phase, Claude must not modify production code or configuration.

The expected output is an engineering plan that identifies:

- The intended goal and acceptance criteria.
- Repository components affected by the change.
- Existing capabilities that should be reused.
- Required implementation tasks and their dependencies.
- Architectural decisions requiring confirmation.
- Technical and operational risks.
- Assumptions that remain unresolved.
- Validation evidence needed before completion.
- Approval requirements and the conditions for beginning execution.

The result is not a collection of generated files. It is a structured approach to producing those files safely.

For example, repository discovery may reveal that the booking service already includes an idempotency mechanism and a transactional outbox. The plan should reuse these capabilities rather than introduce competing implementations.

It may also reveal that corporate account limits are maintained by a separate service. That dependency changes both the implementation sequence and the validation strategy.

At this point, the plan becomes an important coordination artifact.

The human architect can verify the proposed transaction boundary, challenge assumptions about booking limits, and approve the implementation direction before the Developer Agent begins changing code.

### **1.4 Why Planning Changes the Outcome**

Planning Mode does not guarantee correct implementation.

It improves the conditions under which implementation occurs.

The difference is substantial.

Without planning, the agent discovers important dependencies while editing source files. Design decisions become embedded in implementation changes before they are reviewed. Validation is often added afterward, based on the code that was written rather than the business behavior that was required.

With planning, the agent identifies dependencies and validation obligations before making changes. Important decisions are exposed to review. The Developer Agent receives an executable approach rather than an ambiguous requirement.

For Alpha Car Detailing, this means the corporate fleet booking capability begins with an understanding of the existing booking model, capacity rules, transaction boundaries, and event-processing behavior.

It also means that implementation remains accountable to the original business objective.

The distinction is central:

**Planning Mode determines how the work should be approached. Execution performs the approved work. Validation and evaluation establish whether the result is acceptable.**

These activities cooperate, but they must not be confused.

### **1.5 The Enterprise Engineering Principle**

Traditional engineering disciplines distinguish requirements analysis, design, implementation, verification, and acceptance.

AI-assisted development should preserve those responsibilities even when an agent can perform several of them in a single working session.

Planning Mode introduces an explicit boundary between understanding a problem and changing the system.

That boundary makes engineering decisions visible before they are embedded in code.

It also enables the enterprise Harness to coordinate agents, enforce approval requirements, record plan changes, and determine whether execution is permitted to begin.

The resulting principle is simple:

> **Planning separates understanding from execution. An AI Agent should understand the goal, inspect the available evidence, identify dependencies, and establish a verifiable implementation approach before modifying the repository.**

Planning is therefore not an administrative delay. For consequential changes, it is an engineering control that reduces avoidable rework and improves accountability.

## **2. Learning Objectives**

After completing this chapter, the reader should be able to:

1\. Explain Planning Mode as a deliberate, non-mutating phase of AI-assisted software development.

2\. Distinguish a Prompt, a Plan, and a Scratchpad by purpose, ownership, lifecycle, and authority.

3\. Identify when a task requires formal planning and when direct execution is proportionate to its risk.

4\. Explain how Repository Discovery, applicable Instructions, Skills, Knowledge Sources, and Steering Notes influence implementation planning.

5\. Determine task scope, dependencies, sequencing constraints, risks, assumptions, and acceptance criteria before execution.

6\. Design plans that define measurable validation requirements rather than relying on general statements about testing.

7\. Explain the relationship between Planning Mode and the Lead, Developer, Reviewer, Validator, and Evaluator Agents.

8\. Establish appropriate human approval boundaries for architectural, security-sensitive, or high-impact changes.

9\. Recognize plan drift and define when implementation must pause for re-planning.

10\. Implement a practical Claude Code planning workflow that produces an actionable handoff without prematurely modifying source files.

11\. Integrate planning into an enterprise Harness using explicit state transitions, approval decisions, traceable artifacts, and deterministic gates.

12\. Apply these practices to Alpha Car Detailing's corporate fleet booking capability, including booking limits, idempotency, concurrency, transactional consistency, and validation.

## **3. Background**

### **3.1 From Immediate Code Generation to Deliberate Engineering**

Early AI-assisted development workflows often followed a simple interaction pattern.

A developer described a required change, an AI Agent generated implementation code, and the developer reviewed the result.

This pattern works adequately for small, isolated changes with well-defined scope.

For example, updating a localized formatting function or correcting a straightforward conditional expression may require little design work. The affected code is easy to identify, dependencies are limited, and correctness can be established through focused tests.

Enterprise software development is rarely that isolated.

A seemingly small change can influence multiple services, persistence models, API contracts, event schemas, security boundaries, and operational assumptions.

In distributed systems, the most significant implementation risks often exist outside the method being edited.

Consider a new corporate booking operation.

The request may enter through an ASP.NET Core endpoint, invoke an application service, consult corporate account policy data, update booking records, and publish events for downstream processing.

Its behavior may also depend on shared authentication, authorization, retry policies, database transaction management, and observability standards.

Generating the endpoint is only one part of delivering the business capability.

The surrounding engineering decisions determine whether the feature remains correct under realistic operating conditions.

Planning Mode exists to address this gap between syntactic code generation and engineering responsibility.

### **3.2 The Cost of Premature Implementation**

Premature implementation occurs when an agent starts changing the repository before establishing enough understanding to select an appropriate approach.

This creates several categories of engineering risk.

**Architectural divergence.** The agent introduces new patterns despite existing repository conventions, such as adding a direct database dependency to an API controller when application-layer services are required.

**Duplicated capabilities.** The agent builds a second idempotency mechanism, event publisher, or validation abstraction instead of reusing an existing implementation.

**Incomplete scope.** The agent changes the booking endpoint but overlooks persistence migrations, event consumers, authorization policies, or contract tests.

**Incorrect assumptions.** The agent assumes booking limits are global when the business actually defines limits per corporate account and station.

**Weak validation.** The agent verifies that the solution compiles but does not demonstrate that concurrent booking requests preserve capacity constraints.

**Uncontrolled change expansion.** The agent modifies unrelated components to make its initial approach work, increasing the review surface without corresponding business value.

These failures are especially costly when an AI Agent can generate many files before its underlying assumptions are examined.

Planning helps prevent the first implementation from becoming the default design merely because code already exists.

### **3.3 Planning as an Explicit Engineering Phase**

Planning Mode establishes a distinct activity with defined inputs, permitted operations, outputs, and exit conditions.

| Dimension            | Planning Mode                                              | Execution                                            |
| -------------------- | ---------------------------------------------------------- | ---------------------------------------------------- |
| Primary objective    | Determine the implementation approach                      | Implement the approved approach                      |
| Main input           | Goal, repository evidence, Instructions, constraints       | Approved plan and implementation context             |
| Repository access    | Read and inspect                                           | Read and modify within authorization                 |
| Source-code changes  | Not permitted                                              | Permitted within approved scope                      |
| Primary output       | Actionable engineering plan                                | Code, configuration, tests, and supporting artifacts |
| Main uncertainty     | What must change and how                                   | Whether the implementation behaves correctly         |
| Completion condition | Plan is sufficiently supported and approved where required | Implementation and required checks are complete      |
| Authority            | Propose and document decisions                             | Execute authorized decisions                         |
| Evidence             | Discovery findings, dependency analysis, risk assessment   | Diffs, build results, test results, review findings  |

This separation is logical rather than necessarily physical.

A human engineer may conduct planning and execution in the same development environment. A Harness may use the same underlying model for both activities.

The critical requirement is that permissions and responsibilities remain distinct.

A planning session should not become an implementation session simply because the AI Agent encounters a straightforward change.

### **3.4 Planning Is Not Waterfall Development**

An explicit planning phase does not require every implementation decision to be finalized upfront.

Enterprise systems contain uncertainty that cannot always be eliminated through repository inspection.

A dependency may be incompletely documented. An integration environment may behave differently from local tests. An implementation may expose a hidden database constraint.

A useful plan anticipates these possibilities.

It records known decisions, identifies unresolved assumptions, defines validation checkpoints, and provides a controlled mechanism for revising the approach.

The plan is a working engineering agreement, not an immutable prediction.

For example, the initial corporate fleet booking plan might assume that existing database constraints can enforce the required booking limit.

During implementation, the Developer Agent may discover that the current schema cannot provide the necessary concurrency guarantee.

The correct response is not to silently introduce a new persistence architecture. The agent should document the discovery, assess its impact, and request re-planning or approval when the change exceeds its delegated authority.

Planning and adaptive development are therefore compatible.

Planning establishes the initial direction. Execution supplies additional evidence. Re-planning manages material changes.

### **3.5 The Relationship to Repository Intelligence**

Planning Mode depends on the Repository Intelligence concepts introduced in Part II.

Repository Discovery establishes what exists.

Instructions define persistent engineering constraints.

Skills provide reusable engineering workflows.

Prompts define task-specific goals.

Steering Notes identify current priorities and constraints.

Knowledge Sources provide additional architectural, business, or operational context.

Planning brings those inputs together into a task-specific implementation approach.

The relationship is important because a plan created without repository evidence is often little more than an imagined architecture.

For Alpha Car Detailing, the agent should not assume a particular folder structure, service boundary, data-access technology, or event-processing implementation merely because those patterns are common in .NET systems.

It should inspect the repository and report what it actually finds.

Where evidence is missing, the plan must identify that limitation.

This is especially important for reusable Skills. A Skill may describe how to implement a new API in the organization, but its procedure must be reconciled with the target repository.

The Skill contributes a repeatable method. It does not establish that every prerequisite is present.

### **3.6 The Relationship to Harness Engineering**

Part III introduced the Harness as the orchestration and control system surrounding AI-assisted development.

The Harness coordinates agent execution, validation, review, evaluation, approvals, and operational evidence.

Planning Mode fits naturally into that architecture.

The Harness can establish a planning-only execution context, supply repository evidence, collect a structured plan, evaluate whether required fields are present, and route the plan for approval.

After approval, it can pass the plan to the Developer Agent.

This does not eliminate the Lead Agent's responsibility.

The Lead Agent remains accountable for understanding the goal, coordinating work, identifying implementation boundaries, and determining whether specialist roles are needed.

Planning Mode is a phase or operating capability through which those responsibilities can be performed.

A role is not the same thing as a mode.

A Lead Agent may own the planning activity while Claude Code supplies the environment and behavior needed to perform it. The Harness supplies orchestration, permissions, and evidence handling.

The division can be summarized as follows:

**The Lead Agent owns the coordination responsibility. Planning Mode defines how analysis is conducted. The Harness enforces the workflow.**

## **4. Concepts**

### **4.1 What Is Planning Mode?**

Planning Mode is a deliberate operating phase in which an AI Agent investigates a requested change, evaluates available engineering evidence, and constructs an implementation approach before modifying source code or configuration.

Its primary purpose is to answer six questions:

1. What outcome is required?
2. What already exists in the repository?
3. What must change, and what must remain unchanged?
4. What dependencies, constraints, and risks affect the work?
5. In what sequence should implementation proceed?
6. How will the completed work be verified and evaluated?

A planning process is successful when it produces sufficient clarity for authorized execution to begin.

It is not successful merely because the agent produces a detailed document.

An elaborate plan built on unsupported assumptions may be less valuable than a concise plan grounded in repository evidence.

A useful definition is:

> Planning Mode is a controlled engineering activity that transforms a task-specific Prompt and repository evidence into a reviewable, executable, and verifiable implementation Plan.

The transformation is important. The Prompt expresses intent; the Plan translates that intent into engineering work.

### **4.2 Planning Mode Versus a Plan**

Although closely related, Planning Mode and a Plan are not interchangeable.

Planning Mode is the activity or operating state.

The Plan is the artifact produced by that activity.

| Dimension | Planning Mode                          | Plan                                                             |
| --------- | -------------------------------------- | ---------------------------------------------------------------- |
| Nature    | Execution phase or operating mode      | Engineering artifact                                             |
| Purpose   | Investigate and design an approach     | Communicate the proposed approach                                |
| Duration  | Until planning exit conditions are met | Through execution and subsequent review                          |
| Behavior  | Read, inspect, reason, consult         | Record decisions, tasks, dependencies, and evidence requirements |
| Authority | Limited by planning permissions        | Limited by approval and governance policies                      |
| Output    | Proposed engineering decisions         | Versioned implementation approach                                |

An AI Agent may enter Planning Mode several times during a task.

For example, it might create an initial plan, receive architectural feedback, investigate a dependency, and produce a revised plan.

The Plan persists as the agreed implementation reference for the relevant task or run.

It should retain enough traceability to explain why important implementation decisions were made.

### **4.3 Planning Mode Versus Prompt**

A Prompt describes the task the agent is expected to perform.

A Plan describes the approach the agent proposes to take.

For Alpha Car Detailing, the Prompt might state:

> Add corporate fleet booking with booking-limit enforcement, retry safety, and automated tests. Follow the existing architecture.

The corresponding Plan must provide additional engineering detail.

It might identify the existing booking application service, the corporate account policy provider, the transaction boundary, the required API changes, and the sequence of tests.

The difference is not simply the amount of detail.

A Prompt expresses the requested outcome and constraints. A Plan interprets those requirements in the context of the repository and converts them into implementation steps.

The Plan must remain subordinate to the Prompt's authorized goals and applicable Instructions.

It cannot create permission for work the original request did not authorize.

For example, a Plan cannot justify replacing the persistence technology solely because a different database might simplify the proposed implementation.

Such a change would require separate authorization.

### **4.4 Planning Mode Versus Scratchpad**

The Scratchpad and Run Workspace concepts introduced earlier serve a different purpose from the Plan.

A Plan captures the intended approach.

A Scratchpad captures temporary working information discovered or used during execution.

The distinction is essential because execution produces observations that may be useful without being authoritative.

For example, an agent implementing corporate fleet booking may discover that a particular integration test fails intermittently because of test-environment initialization.

That observation belongs initially in the Scratchpad or run evidence.

If the finding materially changes the approach, the agent may propose a Plan update.

The observation should not silently become an architectural decision.

| Aspect                           | Prompt                                  | Plan                                                  | Scratchpad                                                          |
| -------------------------------- | --------------------------------------- | ----------------------------------------------------- | ------------------------------------------------------------------- |
| Main question                    | What needs to be done?                  | How will it be approached?                            | What has been observed or tracked during work?                      |
| Primary content                  | Goal and requirements                   | Scope, tasks, dependencies, risks, validation         | Findings, experiments, temporary notes, unresolved execution issues |
| Typical creator                  | Human requester or orchestration system | Planning agent, often under Lead Agent responsibility | Executing agent                                                     |
| Lifecycle                        | Task request                            | Planning through execution and review                 | Task or run scoped                                                  |
| Authority                        | Defines authorized intent               | Defines approved approach within constraints          | No independent design authority                                     |
| Persistence                      | Task record                             | Reviewable planning artifact                          | Temporary run workspace, retained as required by policy             |
| Modification                     | Through authorized requirement changes  | Through controlled revision                           | Continuously during execution                                       |
| Relationship to repository truth | Does not replace repository evidence    | Must be grounded in repository evidence               | Does not replace authoritative knowledge                            |

The operational distinction can be reduced to three statements:

**Prompt:** What needs to be done.

**Plan:** How the task will be approached.

**Scratchpad:** What is discovered and tracked while the task is being executed.

A Plan should not become a dumping ground for terminal output, failed experiments, temporary hypotheses, or repeated observations.

Conversely, important architectural decisions must not remain buried in a Scratchpad where reviewers and downstream agents may overlook them.

### **4.5 Planning Mode Versus Lead Agent Responsibility**

The Lead Agent is a role within the Harness architecture.

Planning Mode is a phase in which planning-related responsibilities can be performed.

The Lead Agent may be responsible for coordinating repository discovery, identifying relevant Instructions, selecting Skills, decomposing the task, and deciding whether specialist input is required.

However, Planning Mode does not inherently belong to a particular agent role.

A Developer Agent may use Planning Mode before a complex implementation. An Architect Agent may use it to evaluate alternative designs. A Lead Agent may orchestrate several planning activities and consolidate their findings.

The important requirement is explicit ownership.

| Responsibility                  | Lead Agent                   | Planning Mode                   | Harness                         |
| ------------------------------- | ---------------------------- | ------------------------------- | ------------------------------- |
| Interpret the engineering goal  | Accountable                  | Supports analysis               | Supplies task context           |
| Coordinate repository discovery | Responsible                  | Provides investigation phase    | Controls access                 |
| Identify task dependencies      | Responsible                  | Produces findings               | Records artifacts               |
| Select implementation approach  | Proposes or coordinates      | Documents options and rationale | Enforces decision process       |
| Select applicable Skills        | Coordinates                  | Examines suitability            | Resolves available capabilities |
| Request human approval          | Initiates when required      | Supplies reviewable plan        | Routes and records approval     |
| Authorize repository mutation   | Only if explicitly delegated | No authority                    | Enforces authorization          |
| Coordinate execution            | Responsible                  | Provides handoff artifact       | Dispatches Developer Agent      |
| Respond to plan drift           | Coordinates revision         | Supports re-planning            | Enforces pause and reapproval   |

A common architectural mistake is to treat Planning Mode as a replacement for the Lead Agent.

That removes responsibility from a named role and assigns it to a tool capability.

Modes govern how work is performed. Roles establish who is accountable for that work.

### **4.6 When Planning Mode Should Be Used**

Not every change requires the same level of planning.

Planning effort should be proportional to uncertainty, system impact, and implementation risk.

A localized modification with clear requirements and focused validation may need only a short internal task sequence.

A change involving multiple bounded contexts, external dependencies, transaction boundaries, or security policies usually requires an explicit Plan.

Illustrative planning decisions

| Change                                            | Planning intensity                  | Rationale                                        |
| ------------------------------------------------- | ----------------------------------- | ------------------------------------------------ |
| Correct a localized error message                 | Minimal                             | Small scope and straightforward verification     |
| Add a field to an existing API response           | Lightweight                         | Requires contract and compatibility checks       |
| Refactor a shared application service             | Formal                              | Multiple consumers and regression risk           |
| Introduce corporate fleet booking                 | Formal                              | Business rules, concurrency, persistence, events |
| Change authentication or authorization boundaries | Formal with mandatory review        | Security impact                                  |
| Replace an event-delivery mechanism               | Extensive architectural review      | Distributed consistency and migration impact     |
| Modify a production deployment topology           | Extensive with operational approval | Availability and rollback risk                   |

These classifications are illustrative rather than universal policy.

Organizations should establish their own thresholds based on system criticality and governance requirements.

The objective is not to maximize planning. It is to ensure that consequential decisions receive sufficient investigation before implementation.

### **4.7 Repository Discovery Before Planning**

Repository Discovery should precede detailed implementation planning.

The agent must identify the current implementation before deciding which changes are necessary.

This includes examining:

- Solution and project structure.
- Existing application boundaries.
- Relevant domain models and business policies.
- API endpoints and contracts.
- Persistence abstractions and migrations.
- Transaction handling.
- Event publishers and consumers.
- Authentication and authorization behavior.
- Existing unit, integration, and architecture tests.
- Applicable CI/CD validation requirements.

For Alpha Car Detailing, repository discovery might identify an existing corporate booking service and a reusable booking policy abstraction.

These findings should influence the Plan.

A planning agent should distinguish confirmed observations from assumptions.

For example:

| Finding                                                            | Classification                           | Planning consequence                                        |
| ------------------------------------------------------------------ | ---------------------------------------- | ----------------------------------------------------------- |
| Existing booking service uses an application-layer command handler | Confirmed only after inspection          | Reuse the established application boundary                  |
| Corporate limits are supplied by an account policy service         | Requires repository or contract evidence | Include policy integration                                  |
| Booking transactions support atomic outbox persistence             | Requires implementation verification     | Evaluate reuse for event consistency                        |
| Corporate limits apply across all stations                         | Unconfirmed business assumption          | Request clarification                                       |
| Concurrent requests cannot exceed the limit                        | Required invariant                       | Define an atomic enforcement approach and concurrency tests |

Repository evidence must be cited within the Plan using identifiable files, symbols, contracts, or design documents.

A claim such as "the existing architecture already supports idempotency" is insufficient unless the agent can identify the relevant implementation and explain its applicability.

### **4.8 Applicable Instructions**

Instructions constrain the planning process and subsequent implementation.

They may establish coding standards, architecture boundaries, naming conventions, testing requirements, logging policies, security practices, or allowed dependencies.

For Claude Code, repository guidance commonly appears in `CLAUDE.md` files. Other repositories or platforms may use `AGENTS.md` or platform-specific instruction files.

The planner must identify which Instructions are applicable to the task and their scope.

For example, Alpha Car Detailing may require application services to depend on abstractions rather than infrastructure implementations.

A proposed Plan that injects a SQL Server connection directly into an API controller would conflict with that standard.

The planner must resolve the conflict by changing the approach, not by disregarding the Instruction.

Instruction precedence matters.

Platform and organizational security controls, explicitly authorized task constraints, and applicable repository Instructions must be respected according to their established authority and scope.

A lower-trust source discovered during repository inspection must not override higher-priority Instructions.

Similarly, a Plan cannot grant itself permission to change protected Instructions.

Where Instructions conflict or lack clarity, the planner should record the issue and request authorized resolution.

### **4.9 Skill Selection**

Skills supply reusable procedures for recurring engineering activities.

A Skill may describe how to introduce an ASP.NET Core endpoint, add an integration test, implement a transactional outbox, or validate API contracts.

Planning Mode determines which Skills are relevant to the requested change.

Selection should be based on task requirements and repository compatibility.

For corporate fleet booking, the planner might identify Skills relating to:

- API implementation.
- Application-layer business logic.
- Transactional persistence.
- Idempotent request handling.
- Integration testing.
- Event contract validation.

Selecting a Skill does not mean executing it during planning.

The Plan should identify when the Skill will be applied, what prerequisites it requires, and which deliverables it should produce.

If an existing Skill assumes an architecture that differs from the repository, the planner should identify the mismatch rather than applying the Skill mechanically.

A Skill is a reusable implementation method, not a substitute for architectural judgment.

### **4.10 Scope Identification**

Scope defines the boundaries of authorized change.

A useful Plan identifies both in-scope and out-of-scope work.

For the corporate fleet booking feature, in-scope work may include the booking API contract, application logic, capacity enforcement, persistence operations, idempotency behavior, event publication, and related tests.

Out-of-scope work might include replacing the customer management system, redesigning station scheduling, introducing a new messaging platform, or changing unrelated customer booking flows.

Explicit exclusions prevent the agent from expanding the task in pursuit of a technically attractive but unauthorized design.

The scope must also identify cross-cutting effects.

A change may be limited to one service while still requiring contract verification with downstream consumers.

Thus, scope has at least three dimensions:

**Modification scope** identifies what the agent may change.

**Dependency scope** identifies systems and components the change relies upon.

**Validation scope** identifies what must be checked to establish acceptable behavior.

These dimensions are related but not identical.

### **4.11 Task Decomposition**

Task decomposition converts the implementation objective into smaller, ordered engineering activities.

Each task should produce an identifiable result and have an associated completion condition.

For corporate fleet booking, a possible decomposition is:

| Task                              | Intended output                          | Completion condition                                        |
| --------------------------------- | ---------------------------------------- | ----------------------------------------------------------- |
| Confirm corporate booking rules   | Agreed business invariants               | Unresolved policy questions answered                        |
| Define API contract               | Request and response contract            | Contract reviewed for compatibility                         |
| Extend application behavior       | Booking workflow implementation          | Required business rules represented                         |
| Enforce booking limits atomically | Concurrency-safe capacity control        | Competing requests cannot violate limits                    |
| Integrate idempotency             | Safe retry handling                      | Equivalent retries do not create duplicate bookings         |
| Preserve event consistency        | Transactionally coordinated event record | Failed transactions cannot publish committed booking events |
| Add tests                         | Automated behavioral evidence            | Required positive and negative scenarios covered            |
| Validate implementation           | Gate results                             | All mandatory deterministic gates pass                      |
| Evaluate business outcome         | Acceptance assessment                    | Goal and acceptance criteria satisfied                      |

The order may change based on actual repository dependencies.

The Plan should not invent arbitrary implementation sequences merely to appear complete.

A high-quality task breakdown is detailed enough for execution without prescribing unnecessary code-level decisions.

### **4.12 Dependency Analysis**

Dependencies determine whether implementation tasks can safely proceed independently.

Some are technical dependencies, such as an application service requiring an existing policy abstraction.

Others are decision dependencies, such as needing a confirmed booking-limit definition before designing enforcement logic.

A useful distinction is between:

**Hard dependencies**, which must be resolved before the dependent task can proceed.

**Soft dependencies**, which influence the approach but do not necessarily block all work.

**External dependencies**, which require information or action from another team, service, or authority.

For example, the API contract may depend on a decision about whether booking-limit failures should report remaining capacity.

The capacity enforcement implementation depends on the confirmed limit semantics.

Integration testing may depend on a corporate policy test fixture and an appropriately isolated SQL Server environment.

The Plan should identify these relationships rather than presenting tasks as an unstructured checklist.

### **4.13 Risk Identification**

Planning risk assessment focuses on conditions that could cause implementation failure, unsafe behavior, or unacceptable operational consequences.

A risk should be described in terms of a plausible failure and its impact.

For Alpha Car Detailing:

| Risk                                  | Potential consequence          | Planned mitigation                                  |
| ------------------------------------- | ------------------------------ | --------------------------------------------------- |
| Concurrent booking requests           | Corporate limit exceeded       | Atomic capacity enforcement and concurrency tests   |
| Duplicate retries                     | Duplicate reservations         | Durable idempotency and retry testing               |
| Event publication outside transaction | Inconsistent downstream state  | Reuse or introduce an approved transactional outbox |
| Incorrect limit interpretation        | Rejected or excessive bookings | Confirm business rules before implementation        |
| Breaking API changes                  | Existing clients fail          | Compatibility review and contract tests             |
| Excessive database contention         | Booking latency or deadlocks   | Load-focused testing and transaction analysis       |
| Missing authorization checks          | Cross-account booking access   | Corporate account authorization tests               |

Risk analysis should influence implementation priorities and validation design.

It should not be a ceremonial table added after the rest of the Plan has been written.

### **4.14 Assumptions and Unresolved Questions**

An assumption is a proposition used to construct the Plan without sufficient confirming evidence.

Assumptions are inevitable in some engineering tasks.

Hiding them is unacceptable.

An effective Plan identifies each material assumption, its impact, and its resolution strategy.

For example:

**Assumption:** Corporate booking limits apply to the total number of vehicles with active reservations across all stations.

**Impact:** Determines the capacity calculation and required consistency boundary.

**Resolution:** Obtain confirmation from the business owner or an authoritative contract.

**Status:** Unresolved; blocks capacity enforcement design.

Not every assumption must block execution.

An assumption concerning a non-critical naming convention may be resolved during implementation if existing Instructions delegate that decision.

An assumption affecting contractual capacity, security, money movement, or data integrity generally requires stronger confirmation.

Planning should distinguish between:

- Confirmed facts.
- Approved design decisions.
- Explicit assumptions.
- Open questions.
- Implementation details delegated to the Developer Agent.

This distinction helps prevent an AI Agent from presenting uncertainty as established architecture.

### **4.15 Acceptance Criteria**

Acceptance criteria describe observable conditions that must be satisfied for the business requirement to be considered fulfilled.

They should be established before implementation and remain distinguishable from implementation tasks.

For corporate fleet booking, illustrative acceptance criteria include:

| ID    | Acceptance criterion                                                                                           |
| ----- | -------------------------------------------------------------------------------------------------------------- |
| AC-01 | An authorized corporate user can submit a booking containing multiple eligible vehicles                        |
| AC-02 | A booking that exceeds the applicable corporate limit is rejected without partially reserving capacity         |
| AC-03 | Concurrent requests cannot collectively exceed the applicable booking limit                                    |
| AC-04 | A repeated request using the same idempotency key and equivalent payload does not create an additional booking |
| AC-05 | Reuse of an idempotency key with a conflicting payload produces the documented conflict behavior               |
| AC-06 | Booking events are recorded consistently with successful booking transactions                                  |
| AC-07 | Existing individual booking behavior remains compatible                                                        |
| AC-08 | Unauthorized users cannot create bookings for another corporate account                                        |

These criteria are proposed examples, not confirmed Alpha Car Detailing policies. They must be reconciled with the actual business contract before approval.

Each criterion should map to verification evidence.

A Plan that says only "add unit tests" is incomplete.

It should identify which behavior those tests must establish and whether the evidence requires unit, integration, contract, concurrency, security, or other checks.

### **4.16 Validation Strategy**

Validation planning determines how objective implementation conditions will be checked.

The Validator Agent, introduced in Chapter 17, is responsible for verifying deterministic evidence.

Planning Mode does not perform that role.

Instead, planning identifies which gates and test scenarios will be required.

A practical validation strategy for corporate fleet booking might include:

| Validation area    | Required evidence                              |
| ------------------ | ---------------------------------------------- |
| Compilation        | .NET solution builds successfully              |
| Unit tests         | Booking-limit calculations and policy behavior |
| Integration tests  | Transactional booking persistence              |
| Concurrency tests  | Parallel requests preserve capacity invariants |
| Idempotency tests  | Retries do not create duplicate bookings       |
| Contract tests     | API request and response compatibility         |
| Event tests        | Outbox and consumer expectations remain valid  |
| Security tests     | Corporate account authorization boundaries     |
| Architecture tests | Dependencies conform to Clean Architecture     |
| Regression tests   | Existing booking scenarios remain correct      |

Validation results must come from actual execution of the appropriate checks.

A planning document cannot substitute for those results.

Nor does the presence of a proposed test establish that the test has passed.

The Plan defines the required evidence. The Validator Agent examines evidence generated through execution.

The Evaluator Agent subsequently assesses whether the implementation satisfies the goal and broader acceptance expectations.

### **4.17 Human Approval**

Human approval is an explicit authorization decision, not merely an opportunity to read the Plan.

The organization should define which changes require approval and who is authorized to grant it.

Examples may include:

- Changes to shared architecture.
- New infrastructure dependencies.
- Authentication or authorization changes.
- Database schema changes affecting production data.
- Public API breaking changes.
- High-impact business policy changes.
- Changes requiring elevated tool permissions.
- Exceptions to established engineering standards.

For Alpha Car Detailing, an architect may need to approve the concurrency strategy, while a business owner confirms the meaning of corporate booking limits.

Approval should be tied to a particular Plan version and repository baseline.

Otherwise, the approved document may no longer represent the work being performed.

Approval also must not automatically authorize every operational action.

Approving a Plan to add a database migration does not necessarily authorize applying that migration to production.

Separate deployment, data access, and change-management controls remain in force.

### **4.18 Plan Updates and Plan Drift**

A Plan is based on the best evidence available at the time of approval.

During execution, new evidence may invalidate parts of it.

Plan drift occurs when the actual implementation diverges from the approved approach.

Not every deviation is material.

Renaming a private helper function or adjusting the order of independent tasks may remain within delegated implementation discretion.

However, replacing a transaction strategy, adding an external dependency, expanding security permissions, or modifying a different bounded context may constitute material drift.

The Harness should distinguish between minor implementation adjustments and material scope or design changes.

When material drift occurs:

1. Execution should pause before the unauthorized change proceeds.
2. The agent should document the new finding and its impact.
3. The Lead Agent should coordinate revision of the Plan.
4. Required reviewers should approve the revised approach.
5. Execution should resume using the new approved version.

This approach allows adaptive engineering without permitting uncontrolled architectural changes.

### **4.19 Handoff to Execution**

Planning ends with a controlled handoff.

The Developer Agent should receive more than a natural-language summary.

At minimum, the handoff should identify the approved goal, applicable Plan version, repository baseline, task sequence, scope boundaries, relevant Instructions and Skills, unresolved non-blocking assumptions, and required validation evidence.

The Developer Agent must also understand which decisions are fixed and which remain delegated.

For example, the Plan may require atomic capacity enforcement while allowing the Developer Agent to choose a suitable implementation within the existing persistence model.

If the selected approach cannot satisfy the required invariant, the Developer Agent must escalate rather than silently weaken the acceptance criterion.

Planning completion means the approach is ready for execution.

It does not mean the engineering task is complete.

## **5. Architecture Discussion**

### **5.1 Planning as an Architectural Boundary**

Within an enterprise Harness, Planning Mode should be treated as a first-class workflow boundary rather than an optional conversational convention.

The distinction matters because a conversational instruction such as "do not edit files yet" can express intent without independently enforcing permissions.

A robust Harness combines that instruction with tool restrictions, workspace isolation, state management, and approval controls.

The planning architecture should provide four guarantees.

**Non-mutation:** Planning cannot alter protected repository files, production configuration, or external resources.

**Traceability:** Material findings, decisions, assumptions, and planned validation steps are recorded.

**Authorization:** Execution cannot begin until applicable approval conditions have been satisfied.

**Continuity:** The approved Plan and its evidence remain available to downstream agents.

These guarantees establish Planning Mode as an engineering control rather than a prompt-writing technique.

### **5.2 Inputs to Planning**

Planning should receive a defined set of inputs.

| Input                   | Purpose                                           | Source                                   |
| ----------------------- | ------------------------------------------------- | ---------------------------------------- |
| Task Prompt             | Establishes requested outcome                     | Human or authorized workflow             |
| Repository snapshot     | Defines actual code baseline                      | Version control                          |
| Applicable Instructions | Establishes engineering constraints               | Repository and managed policy            |
| Steering Note           | Supplies mission priorities and constraints       | Authorized project owner                 |
| Repository Intelligence | Provides architecture and component understanding | Discovery and approved knowledge sources |
| Skills                  | Supplies reusable implementation procedures       | Governed Skill library                   |
| Acceptance criteria     | Defines expected observable behavior              | Business owner or task authority         |
| Validation policy       | Defines mandatory engineering gates               | Harness configuration                    |
| Approval policy         | Defines authorization requirements                | Organizational governance                |

Each input has a different authority.

The Harness should not treat them as interchangeable.

For example, a repository comment discovered during inspection may provide useful context, but it must not be allowed to override organizational security policy.

Likewise, a retrieved Knowledge Source may be outdated or incomplete.

The planning process should establish provenance and identify conflicts before incorporating findings into its proposed approach.

### **5.3 A Reference Planning Architecture**

A practical reference architecture contains six logical components.

**Task Intake** receives the Prompt and associates it with a task identity, requester, repository, and baseline revision.

**Discovery Context** supplies read-only repository access and applicable knowledge sources.

**Planning Agent** analyzes the change, decomposes work, identifies risks, and constructs the Plan.

**Plan Quality Gate** checks that mandatory planning information is present, internally consistent where deterministically checkable, and linked to the task.

**Approval Gateway** routes material decisions to authorized reviewers.

**Execution Handoff** supplies the approved Plan to the Developer Agent with the appropriate permissions and constraints.

A Plan Quality Gate should not be confused with the Validator Agent's implementation validation.

It may verify that the Plan contains required fields, references an existing repository revision, includes validation requirements, and declares unresolved blockers.

It cannot deterministically prove that the proposed architecture is correct merely by checking document completeness.

Architectural quality still requires substantive review.

### **5.4 Planning Mode Inside the Harness**

A simplified Harness workflow can be modeled as:

```
Task Intake
    |
    v
Repository Discovery
    |
    v
Instruction and Skill Resolution
    |
    v
Planning
    |
    v
Plan Quality Gate
    |
    v
Review and Approval
    |
    v
Developer Agent
    |
    v
Implementation
    |
    v
Reviewer / Validator / Evaluator
    |
    v
Completion Decision
```

This model should not be interpreted as requiring every activity to occur strictly once.

A failed validation gate may return work to the Developer Agent.

An architectural review finding may require re-planning.

An acceptance mismatch may require clarification of business requirements.

The Harness coordinates these transitions while preserving evidence.

### **5.5 Plan Artifact Design**

The Plan should be structured enough to support both human review and automated Harness processing.

A representative artifact might contain the following fields:

| Field                   | Description                                   |
| ----------------------- | --------------------------------------------- |
| Plan ID                 | Stable identifier                             |
| Task ID                 | Associated engineering request                |
| Revision                | Plan version                                  |
| Repository baseline     | Commit or immutable snapshot identifier       |
| Goal                    | Intended business outcome                     |
| Scope                   | Included and excluded changes                 |
| Evidence references     | Relevant repository findings                  |
| Applicable Instructions | Instruction sources and scopes                |
| Selected Skills         | Reusable procedures expected during execution |
| Decisions               | Proposed or approved design choices           |
| Assumptions             | Known uncertainties                           |
| Dependencies            | Prerequisites and sequencing                  |
| Tasks                   | Implementation work units                     |
| Risks                   | Failure scenarios and mitigations             |
| Acceptance criteria     | Required observable behavior                  |
| Validation requirements | Mandatory checks and evidence                 |
| Approval status         | Authorization state                           |
| Handoff status          | Readiness for implementation                  |

The representation may be Markdown, YAML, JSON, or a combination of a human-readable document and machine-readable metadata.

The format is less important than maintaining an unambiguous relationship between the approved decisions and the work performed.

For example, a Harness may store a Markdown Plan for engineers while keeping approval status and repository revision in a structured run record.

### **5.6 Planning Artifact Versus Repository Knowledge**

A Plan is task-specific.

It should not be promoted automatically into permanent Repository Intelligence.

This distinction protects the authority of established architectural documentation.

A Plan may contain a decision that is valid for one change without establishing a reusable standard.

For instance, choosing a specific transaction strategy for corporate fleet booking does not automatically mean every Alpha Car Detailing service must use that strategy.

If execution produces a reusable architectural finding, it may be proposed for inclusion in an Architecture Decision Record, Instruction, Skill, or Knowledge Source.

That promotion should follow the governance process defined for the destination artifact.

The Plan itself does not silently amend repository standards.

### **5.7 Approval as a State Transition**

Approval must be represented as an explicit transition in the Harness state model.

A simplified state sequence is:

```
Draft
  |
  v
Ready for Review
  |
  v
Approved
  |
  v
Ready for Execution
```

Alternative transitions are equally important.

A reviewer may reject the Plan.

The requester may withdraw the task.

The repository baseline may change and invalidate planning assumptions.

New information may require a revised Plan.

The state model should account for these possibilities rather than assuming that every proposed Plan eventually becomes approved.

### **5.8 Repository Baseline and Plan Freshness**

A Plan is created against a particular understanding of the repository.

If the repository changes materially before execution begins, the Plan may become stale.

For example, another team may modify the booking service, migrate the persistence model, or replace an event contract after the Plan has been approved.

The Harness should compare the planned baseline with the execution baseline.

A changed commit does not always require complete re-planning. Unrelated documentation changes may have no impact.

The required response should depend on the files, contracts, and assumptions affected.

A useful policy is:

- Record the exact repository baseline used during planning.
- Identify the files and interfaces on which material decisions depend.
- Detect changes to those dependencies before execution.
- Reassess affected assumptions.
- Require renewed approval if the approved design or scope changes materially.

This establishes plan freshness as a measurable property.

### **5.9 Security and Permissions**

A planning phase should follow least-privilege principles.

The planner may require access to source files, test descriptions, architecture documentation, and repository history.

It ordinarily does not require permission to modify production resources, deploy services, rotate credentials, delete data, or alter source files.

The distinction should be enforced through tool permissions and execution isolation.

Even commands that appear observational can have side effects if they invoke untrusted scripts, build targets, or external services.

Therefore, a strict enterprise planning environment should permit only approved read-oriented operations, ideally against a read-only repository snapshot.

The Harness should also prevent lower-trust repository content from becoming executable instructions.

An attacker-controlled README, source-code comment, or retrieved document may attempt to redirect the AI Agent into unauthorized actions.

Planning must treat such material as evidence to evaluate, not as an authority capable of overriding Instructions.

### **5.10 Failure Handling**

A planning process may fail without producing an executable Plan.

Possible causes include missing repository access, contradictory Instructions, unavailable business requirements, unresolved architectural dependencies, or insufficient evidence.

The correct result in these situations is an explicit blocked or inconclusive planning outcome.

For example, if the corporate booking limit has not been defined, the agent may still analyze the existing architecture and identify preliminary tasks.

However, it should not claim that the capacity enforcement design is ready for execution.

The Plan should distinguish work that can proceed independently from work blocked by missing decisions.

The Harness should record the reason for the block and identify the authority required to resolve it.

A missing decision must not become permission for the AI Agent to invent a convenient business rule.

## **6. Professional Diagrams**

### **6.1 Planning-to-Execution Flow**

The following diagram illustrates the separation between task interpretation, planning, approval, and implementation.

\#chatgpt-mermaid-\_r_1bok\_ :is(.node,.pele-node).success :is(rect,circle,ellipse,polygon,path):where(:not(.label \*)) {fill:#1d3827;stroke:#194b2d}#chatgpt-mermaid-\_r_1bok\_ :is(.node,.pele-node).success [fill="none"]:where(:not(.label \*)) {fill:none}#chatgpt-mermaid-\_r_1bok\_ :is(.node,.pele-node).success [stroke="none"]:where(:not(.label \*)) {stroke:none}#chatgpt-mermaid-\_r_1bok\_ :is(.node,.pele-node).success :where(text,.label,.nodeLabel) {fill:#53b559;color:#53b559}#chatgpt-mermaid-\_r_1bok\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bok\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bok\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bok\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bok\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bok\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bok\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bok\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bok\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bok\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bok\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bok\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bok\_ p{margin:0;}#chatgpt-mermaid-\_r_1bok\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bok\_ .label text,#chatgpt-mermaid-\_r_1bok\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .node rect,#chatgpt-mermaid-\_r_1bok\_ .node circle,#chatgpt-mermaid-\_r_1bok\_ .node ellipse,#chatgpt-mermaid-\_r_1bok\_ .node polygon,#chatgpt-mermaid-\_r_1bok\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bok\_ .node .label text,#chatgpt-mermaid-\_r_1bok\_ .image-shape .label,#chatgpt-mermaid-\_r_1bok\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bok\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .rough-node .label,#chatgpt-mermaid-\_r_1bok\_ .node .label,#chatgpt-mermaid-\_r_1bok\_ .image-shape .label,#chatgpt-mermaid-\_r_1bok\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bok\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bok\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bok\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bok\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bok\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bok\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bok\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bok\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bok\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bok\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bok\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bok\_ .icon-shape,#chatgpt-mermaid-\_r_1bok\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bok\_ .icon-shape p,#chatgpt-mermaid-\_r_1bok\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bok\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bok\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bok\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bok\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bok\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bok\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bok\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bok\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bok\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bok\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bok\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bok\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bok\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bok\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bok\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bok\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .node rect,#chatgpt-mermaid-\_r_1bok\_ .node circle,#chatgpt-mermaid-\_r_1bok\_ .node ellipse,#chatgpt-mermaid-\_r_1bok\_ .node polygon,#chatgpt-mermaid-\_r_1bok\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bok\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bok\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bok\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bok\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bok\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1bok\_ .decision rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .decision polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .decision ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .decision circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .decision path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .decision tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase rect{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase polygon{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase ellipse{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase circle{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase path{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .phase tspan{fill:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bok\_ .success rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bok\_ .success polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bok\_ .success ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bok\_ .success circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bok\_ .success path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bok\_ .success tspan{fill:rgb(33, 84, 53)!important;}Planning ActivitiesIdentify ScopeDecompose TasksAnalyze DependenciesIdentify RisksRecord AssumptionsDefine Validation PlanTask PromptRepository DiscoveryApplicable InstructionsSkill and Knowledge ResolutionPlanning ModeProposed PlanReview and ApprovalDeveloper AgentExecutionReview, Validation andEvaluationRevision requiredApproved

The approval boundary is explicit. Reaching the end of planning does not automatically authorize implementation.

### **6.2 Relationship Between Prompt, Plan, and Scratchpad**

\#chatgpt-mermaid-\_r_1bor\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bor\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bor\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bor\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bor\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bor\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bor\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bor\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bor\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bor\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bor\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bor\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bor\_ p{margin:0;}#chatgpt-mermaid-\_r_1bor\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bor\_ .label text,#chatgpt-mermaid-\_r_1bor\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .node rect,#chatgpt-mermaid-\_r_1bor\_ .node circle,#chatgpt-mermaid-\_r_1bor\_ .node ellipse,#chatgpt-mermaid-\_r_1bor\_ .node polygon,#chatgpt-mermaid-\_r_1bor\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bor\_ .node .label text,#chatgpt-mermaid-\_r_1bor\_ .image-shape .label,#chatgpt-mermaid-\_r_1bor\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bor\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .rough-node .label,#chatgpt-mermaid-\_r_1bor\_ .node .label,#chatgpt-mermaid-\_r_1bor\_ .image-shape .label,#chatgpt-mermaid-\_r_1bor\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bor\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bor\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bor\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bor\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bor\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bor\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bor\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bor\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bor\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bor\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bor\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bor\_ .icon-shape,#chatgpt-mermaid-\_r_1bor\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bor\_ .icon-shape p,#chatgpt-mermaid-\_r_1bor\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bor\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bor\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bor\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bor\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bor\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bor\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bor\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bor\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bor\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bor\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bor\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bor\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bor\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bor\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bor\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bor\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .node rect,#chatgpt-mermaid-\_r_1bor\_ .node circle,#chatgpt-mermaid-\_r_1bor\_ .node ellipse,#chatgpt-mermaid-\_r_1bor\_ .node polygon,#chatgpt-mermaid-\_r_1bor\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bor\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bor\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bor\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bor\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bor\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1bor\_ .intent rect{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .intent polygon{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .intent ellipse{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .intent circle{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .intent path{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .intent tspan{fill:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .temporary tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bor\_ .evidence tspan{fill:rgb(33, 84, 53)!important;}PromptRequested OutcomePlanning ModeRepository EvidenceInstructions and SkillsApproved PlanImplementation ApproachDeveloper AgentExecutionScratchpadTemporary Run StateMaterial Discovery?Continue ExecutionChange AssessmentValidation EvidenceNoYes

The Scratchpad supports execution without becoming an alternative Plan.

Material discoveries are assessed and, when necessary, incorporated through controlled re-planning.

### **6.3 Agent Responsibility Boundaries**

\#chatgpt-mermaid-\_r_1bp2\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bp2\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bp2\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bp2\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bp2\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bp2\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bp2\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bp2\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bp2\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bp2\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp2\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp2\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bp2\_ p{margin:0;}#chatgpt-mermaid-\_r_1bp2\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bp2\_ .label text,#chatgpt-mermaid-\_r_1bp2\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .node rect,#chatgpt-mermaid-\_r_1bp2\_ .node circle,#chatgpt-mermaid-\_r_1bp2\_ .node ellipse,#chatgpt-mermaid-\_r_1bp2\_ .node polygon,#chatgpt-mermaid-\_r_1bp2\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bp2\_ .node .label text,#chatgpt-mermaid-\_r_1bp2\_ .image-shape .label,#chatgpt-mermaid-\_r_1bp2\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bp2\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .rough-node .label,#chatgpt-mermaid-\_r_1bp2\_ .node .label,#chatgpt-mermaid-\_r_1bp2\_ .image-shape .label,#chatgpt-mermaid-\_r_1bp2\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bp2\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bp2\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp2\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp2\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bp2\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bp2\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bp2\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bp2\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bp2\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bp2\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp2\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bp2\_ .icon-shape,#chatgpt-mermaid-\_r_1bp2\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bp2\_ .icon-shape p,#chatgpt-mermaid-\_r_1bp2\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bp2\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bp2\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bp2\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bp2\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bp2\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bp2\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bp2\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bp2\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bp2\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp2\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bp2\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp2\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bp2\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bp2\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bp2\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bp2\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .node rect,#chatgpt-mermaid-\_r_1bp2\_ .node circle,#chatgpt-mermaid-\_r_1bp2\_ .node ellipse,#chatgpt-mermaid-\_r_1bp2\_ .node polygon,#chatgpt-mermaid-\_r_1bp2\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp2\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bp2\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bp2\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bp2\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp2\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Harness OrchestratorLead AgentPlanning ModePlan ApprovalDeveloper AgentReviewer AgentValidator AgentEvaluator AgentCompletion DecisionCompleteApprovedRejected or RevisedArchitectural findingsValidation failuresGoal gapsAcceptedRework required

This is a logical responsibility model, not a requirement that every Harness implementation use separate model instances.

The Reviewer, Validator, and Evaluator retain distinct responsibilities even when they operate within a coordinated workflow.

### **6.4 Plan Lifecycle and Drift Control**

\#chatgpt-mermaid-\_r_1bp9\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bp9\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bp9\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bp9\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bp9\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bp9\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bp9\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bp9\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bp9\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bp9\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bp9\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bp9\_ p{margin:0;}#chatgpt-mermaid-\_r_1bp9\_ defs [id$="-barbEnd"]{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ g.stateGroup text{fill:rgb(31, 78, 148);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1bp9\_ g.stateGroup text{fill:rgb(237, 237, 237);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1bp9\_ g.stateGroup .state-title{font-weight:bolder;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ g.stateGroup rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bp9\_ g.stateGroup line{stroke:rgb(175, 175, 175);stroke-width:1;}#chatgpt-mermaid-\_r_1bp9\_ .transition{stroke:rgb(175, 175, 175);stroke-width:1;fill:none;}#chatgpt-mermaid-\_r_1bp9\_ .stateGroup .composit{fill:transparent;border-bottom:1px;}#chatgpt-mermaid-\_r_1bp9\_ .stateGroup .alt-composit{fill:#e0e0e0;border-bottom:1px;}#chatgpt-mermaid-\_r_1bp9\_ .state-note{stroke:rgb(58, 132, 63);fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bp9\_ .state-note text{fill:rgb(237, 237, 237);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1bp9\_ .stateLabel .box{stroke:none;stroke-width:0;fill:rgb(9, 23, 44);opacity:0.5;}#chatgpt-mermaid-\_r_1bp9\_ .edgeLabel .label rect{fill:rgb(9, 23, 44);opacity:0.5;}#chatgpt-mermaid-\_r_1bp9\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bp9\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bp9\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bp9\_ .edgeLabel .label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .label div .edgeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .stateLabel text{fill:rgb(237, 237, 237);font-size:10px;font-weight:bold;}#chatgpt-mermaid-\_r_1bp9\_ .node circle.state-start{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ .node .fork-join{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ .node circle.state-end{fill:rgb(31, 78, 148);stroke:transparent;stroke-width:1.5;}#chatgpt-mermaid-\_r_1bp9\_ .end-state-inner{fill:transparent;stroke-width:1.5;}#chatgpt-mermaid-\_r_1bp9\_ .node rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp9\_ .node polygon{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp9\_ [id$="-barbEnd"]{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-cluster rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp9\_ .cluster-label,#chatgpt-mermaid-\_r_1bp9\_ .nodeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-cluster rect.outer{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-state .divider{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-state .title-state{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-cluster.statediagram-cluster .inner{fill:transparent;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-cluster.statediagram-cluster-alt .inner{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-cluster .inner{rx:0;ry:0;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-state rect.basic{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-state rect.divider{stroke-dasharray:10,10;fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bp9\_ .note-edge{stroke-dasharray:5;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-note rect{fill:rgb(48, 48, 48);stroke:rgb(58, 132, 63);stroke-width:1px;rx:0;ry:0;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-note rect{fill:rgb(48, 48, 48);stroke:rgb(58, 132, 63);stroke-width:1px;rx:0;ry:0;}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-note text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram-note .nodeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ .statediagram .edgeLabel{color:red;}#chatgpt-mermaid-\_r_1bp9\_ [id$="-dependencyStart"],#chatgpt-mermaid-\_r_1bp9\_ [id$="-dependencyEnd"]{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);stroke-width:1;}#chatgpt-mermaid-\_r_1bp9\_ .statediagramTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].statediagram-cluster rect{fill:rgb(9, 23, 44);stroke:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);stroke-width:1;}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].statediagram-cluster rect.outer{rx:5px;ry:5px;filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bp9\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bp9\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}DraftUnderReviewApprovedRejectedExecutingReassessmentDriftAssessmentVerificationCompletePlan submittedAuthorized approvalRevision requestedPlan rejectedBaseline and permissionscheckedBaseline changedMaterial deviation detectedWithin approved toleranceMaterial change requiredRevise planRevalidated and authorizedImplementation finishedRework within approvedscopeRequired checks andacceptance satisfied

The state model distinguishes approval, execution, verification, and completion.

It also identifies the point at which material drift requires new planning decisions.

### **6.5 Dependency Graph for Corporate Fleet Booking**

\#chatgpt-mermaid-\_r_1bpg\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bpg\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bpg\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bpg\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bpg\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bpg\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bpg\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bpg\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bpg\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bpg\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpg\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpg\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bpg\_ p{margin:0;}#chatgpt-mermaid-\_r_1bpg\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bpg\_ .label text,#chatgpt-mermaid-\_r_1bpg\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .node rect,#chatgpt-mermaid-\_r_1bpg\_ .node circle,#chatgpt-mermaid-\_r_1bpg\_ .node ellipse,#chatgpt-mermaid-\_r_1bpg\_ .node polygon,#chatgpt-mermaid-\_r_1bpg\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bpg\_ .node .label text,#chatgpt-mermaid-\_r_1bpg\_ .image-shape .label,#chatgpt-mermaid-\_r_1bpg\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bpg\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .rough-node .label,#chatgpt-mermaid-\_r_1bpg\_ .node .label,#chatgpt-mermaid-\_r_1bpg\_ .image-shape .label,#chatgpt-mermaid-\_r_1bpg\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bpg\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bpg\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpg\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpg\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bpg\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bpg\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpg\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpg\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bpg\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bpg\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpg\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bpg\_ .icon-shape,#chatgpt-mermaid-\_r_1bpg\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bpg\_ .icon-shape p,#chatgpt-mermaid-\_r_1bpg\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bpg\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bpg\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpg\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bpg\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bpg\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bpg\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bpg\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bpg\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bpg\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpg\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bpg\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpg\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bpg\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bpg\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bpg\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bpg\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .node rect,#chatgpt-mermaid-\_r_1bpg\_ .node circle,#chatgpt-mermaid-\_r_1bpg\_ .node ellipse,#chatgpt-mermaid-\_r_1bpg\_ .node polygon,#chatgpt-mermaid-\_r_1bpg\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpg\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bpg\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bpg\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bpg\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpg\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Confirm Business RulesDefine API ContractDefine Capacity InvariantRepository DiscoverySelect Existing IntegrationPointsDesign Application WorkflowAtomic Booking PersistenceIdempotency IntegrationTransactional OutboxIntegrationRetry and Conflict TestsConcurrency TestsEvent Consistency TestsAPI Contract TestsAuthorization TestsValidation Gates

This graph shows why task sequencing should follow real dependencies rather than an arbitrary implementation checklist.

Business-rule confirmation is a prerequisite for designing the capacity invariant. Persistence design influences concurrency testing. Idempotency and transaction consistency require separate verification.

### **6.6 Planning Control Boundaries**

\#chatgpt-mermaid-\_r_1bpn\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bpn\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bpn\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bpn\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bpn\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bpn\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bpn\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bpn\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bpn\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bpn\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpn\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpn\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bpn\_ p{margin:0;}#chatgpt-mermaid-\_r_1bpn\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bpn\_ .label text,#chatgpt-mermaid-\_r_1bpn\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .node rect,#chatgpt-mermaid-\_r_1bpn\_ .node circle,#chatgpt-mermaid-\_r_1bpn\_ .node ellipse,#chatgpt-mermaid-\_r_1bpn\_ .node polygon,#chatgpt-mermaid-\_r_1bpn\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bpn\_ .node .label text,#chatgpt-mermaid-\_r_1bpn\_ .image-shape .label,#chatgpt-mermaid-\_r_1bpn\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bpn\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .rough-node .label,#chatgpt-mermaid-\_r_1bpn\_ .node .label,#chatgpt-mermaid-\_r_1bpn\_ .image-shape .label,#chatgpt-mermaid-\_r_1bpn\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bpn\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bpn\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpn\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpn\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bpn\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bpn\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpn\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpn\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bpn\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bpn\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bpn\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bpn\_ .icon-shape,#chatgpt-mermaid-\_r_1bpn\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bpn\_ .icon-shape p,#chatgpt-mermaid-\_r_1bpn\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bpn\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bpn\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bpn\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bpn\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bpn\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bpn\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bpn\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bpn\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bpn\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpn\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bpn\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bpn\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bpn\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bpn\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bpn\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bpn\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .node rect,#chatgpt-mermaid-\_r_1bpn\_ .node circle,#chatgpt-mermaid-\_r_1bpn\_ .node ellipse,#chatgpt-mermaid-\_r_1bpn\_ .node polygon,#chatgpt-mermaid-\_r_1bpn\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bpn\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bpn\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bpn\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bpn\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bpn\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1bpn\_ .planning rect{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .planning polygon{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .planning ellipse{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .planning circle{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .planning path{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .planning tspan{fill:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .control tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1bpn\_ .execution tspan{fill:rgb(33, 84, 53)!important;}Execution BoundaryAuthorization BoundaryPlanning BoundaryModify SourceRun ValidationProduce EvidencePlan Quality GateHuman ApprovalExecution AuthorizationRead RepositoryInspect InstructionsAnalyze ContractsProduce Proposed Plan

The architectural value is not simply that planning happens before execution.

It is that the transition between the two is controlled, reviewable, and enforceable.

## **7. Hands-on Example**

### **7.1 Scenario: Planning Corporate Fleet Booking**

The Alpha Car Detailing engineering team receives a change request to extend the existing booking capability.

The proposed operation must accept multiple vehicles associated with a corporate account and create reservations without violating contractual booking limits.

The architecture already includes a booking domain and several shared enterprise capabilities, but the planner must verify how those capabilities are implemented in the current repository.

The objective of this exercise is to produce an approved implementation approach without modifying source files.

The exercise assumes the companion repository contains the relevant Alpha Car Detailing application or that the reader has a working branch containing the booking capability.

Any example paths or component names that are not verified against the reader's checkout must be treated as illustrative.

### **7.2 Establish the Task**

The engineering request is recorded as a task-specific Prompt.

```
Goal:
Implement corporate fleet booking for Alpha Car Detailing.

Requirements:
- Authorized corporate users can book multiple vehicles.
- Corporate booking limits must be enforced.
- Concurrent requests must not exceed permitted capacity.
- Request retries must not create duplicate bookings.
- Booking events must remain transactionally consistent.
- Existing individual booking behavior must not regress.

Constraints:
- Follow applicable repository Instructions.
- Reuse existing architecture and shared capabilities.
- Do not introduce CQRS or new infrastructure solely for this change.
- Do not modify code during planning.

Required output:
A proposed implementation plan containing scope,
dependencies, risks, assumptions, implementation tasks,
acceptance criteria, and validation strategy.
```

The instruction not to introduce CQRS reflects the companion implementation's incremental development approach.

The Plan should work with the architecture that currently exists, rather than using the task as an opportunity for unrelated restructuring.

### **7.3 Inspect the Repository Baseline**

Before designing the implementation, the planning agent should identify the exact repository revision.

For a local Git checkout, a human or an approved read-only discovery tool can obtain the current revision using:

```
git rev-parse HEAD
git status --short
```

The first command identifies the commit used as the planning baseline.

The second reports working-tree differences.

The planner should not assume that a clean commit represents all local state if uncommitted changes are present.

If the working tree contains modifications, the Plan should identify their relevance or request an isolated snapshot.

The agent should then inspect the solution structure, booking-related projects, test projects, applicable Instructions, and existing architectural documentation.

Useful questions include:

- Where is the current booking application workflow implemented?
- How does the API identify and authorize corporate accounts?
- Where are booking limits stored or retrieved?
- Does the current implementation already provide idempotency?
- How are booking and outbox records persisted?
- What transaction or concurrency guarantees are already available?
- Which tests establish existing behavior?

The purpose is to collect evidence, not to execute implementation commands.

### **7.4 Build the Repository Evidence Register**

The planning agent should produce a small evidence register.

For example:

| Evidence item        | Required finding                        | Source reference                                 |
| -------------------- | --------------------------------------- | ------------------------------------------------ |
| Booking entry point  | Current endpoint and request contract   | Actual controller or endpoint file               |
| Application workflow | Existing service or handler             | Actual application-layer implementation          |
| Corporate policy     | Source of account booking limits        | Actual policy interface or contract              |
| Persistence          | Booking transaction mechanism           | Actual repository and transaction implementation |
| Idempotency          | Existing request deduplication behavior | Actual implementation and tests                  |
| Events               | Existing outbox and publisher behavior  | Actual infrastructure implementation             |
| Tests                | Applicable test suites and patterns     | Actual test projects                             |

The third column must be populated from inspected repository files.

Inventing file names to make the Plan appear complete would undermine the purpose of repository discovery.

Where a component cannot be found, the planner should say so explicitly.

### **7.5 Resolve the Booking-Limit Definition**

The most significant business ambiguity is the meaning of a corporate booking limit.

Consider three possible interpretations:

| Interpretation                                   | Enforcement model                                        |
| ------------------------------------------------ | -------------------------------------------------------- |
| Maximum vehicles per booking request             | Validate the size of each incoming request               |
| Maximum active vehicle reservations              | Enforce aggregate active capacity atomically             |
| Maximum vehicles booked during a contract period | Enforce time-window usage according to contractual rules |

These models are not equivalent.

For example, if a corporate customer may maintain at most 100 active reservations, two concurrent requests for 60 vehicles must not both succeed when the account currently has no active reservations.

A naive count-then-insert implementation can violate the invariant even when both requests independently pass validation.

The business owner must confirm the applicable interpretation.

For this exercise, assume the approved rule is:

> A corporate account may not have more active vehicle reservations than its configured booking limit, across all stations.

This is an exercise assumption, not an established business fact.

With that rule confirmed, the planner can define the invariant:

\\[ \text{ActiveReservationsAfterCommit} \leq \text{CorporateBookingLimit} \\]

The Plan must require enforcement under concurrency, not merely sequential validation.

### **7.6 Evaluate Persistence and Concurrency Options**

The planner should investigate how the existing persistence architecture can enforce the invariant.

Possible approaches include appropriately scoped database locking, serializable transactions, conditional capacity updates, optimistic concurrency using a versioned aggregate or capacity record, or an existing repository-supported reservation mechanism.

The correct choice depends on the actual schema, transaction model, contention profile, and distributed service boundaries.

The Plan should not prescribe a new mechanism without examining those conditions.

For example, a conditional update against a dedicated corporate capacity record could provide atomic enforcement if the data model and lifecycle support that approach.

However, if the booking limit is derived from active reservations with complex cancellation and expiration semantics, a separate capacity counter introduces additional synchronization requirements.

The architect must evaluate the tradeoff.

The Plan should record the chosen invariant, the existing persistence mechanisms, the proposed enforcement boundary, and the required concurrency tests.

### **7.7 Plan Idempotency Behavior**

Idempotency protects against duplicate processing of logically equivalent retries.

It does not solve general concurrent capacity enforcement.

Two distinct booking requests may each have valid idempotency keys and still compete for the same corporate capacity.

The Plan must therefore address both concerns.

For example, the planned API contract might require a client-provided idempotency key.

The implementation should ensure that a repeated request with the same key and equivalent payload returns the documented result without creating another booking.

A conflicting payload using the same key should be rejected according to the approved contract.

The key's scope, retention policy, persistence strategy, and relationship to the booking transaction must be defined.

The planner should prefer an existing idempotency implementation when suitable.

### **7.8 Plan Transactional Event Consistency**

If the booking workflow emits an integration event, the Plan must preserve consistency between committed booking data and the corresponding event record.

The existing transactional outbox should be reused if repository discovery confirms that it satisfies the requirement.

A successful booking transaction should atomically persist the reservation and its outbox record.

A failed transaction should not leave a committed event describing an uncommitted booking.

Downstream consumers should remain idempotent because outbox delivery may be repeated.

This responsibility belongs to implementation and subsequent validation, but the requirement must appear in the Plan before coding starts.

### **7.9 Proposed Implementation Tasks**

A concrete execution Plan might contain the following sequence.

| Step | Task                                                       | Dependency                             | Validation obligation                           |
| ---- | ---------------------------------------------------------- | -------------------------------------- | ----------------------------------------------- |
| 1    | Confirm the booking-limit policy and cancellation behavior | Business approval                      | Approved policy definition                      |
| 2    | Review the existing API contract and authorization model   | Repository discovery                   | Contract compatibility assessment               |
| 3    | Select the capacity enforcement mechanism                  | Existing persistence architecture      | Concurrency design review                       |
| 4    | Extend the application booking workflow                    | Steps 1–3                              | Unit and application tests                      |
| 5    | Integrate existing idempotency support                     | Request contract and storage mechanism | Retry and conflicting-payload tests             |
| 6    | Persist bookings with required consistency guarantees      | Transaction design                     | Transaction and rollback tests                  |
| 7    | Integrate outbox event recording                           | Existing event infrastructure          | Event consistency tests                         |
| 8    | Add API and authorization coverage                         | API and application behavior           | Integration and security tests                  |
| 9    | Execute mandatory gates                                    | Completed implementation               | Build, test, contract, and architecture results |
| 10   | Conduct independent review and evaluation                  | Validation evidence                    | Review findings and acceptance assessment       |

Every task is intended to produce an observable implementation result or decision.

The sequence should be revised if repository evidence establishes different prerequisites.

### **7.10 Define the Validation Matrix**

The Plan should map acceptance criteria to actual verification techniques.

| Acceptance criterion                  | Verification method               | Expected evidence                          |
| ------------------------------------- | --------------------------------- | ------------------------------------------ |
| AC-01: Multiple vehicle booking       | API integration test              | Valid booking created                      |
| AC-02: Booking-limit rejection        | Application and integration tests | No partial persistence                     |
| AC-03: Concurrency safety             | Parallel-request integration test | Committed reservations remain within limit |
| AC-04: Idempotent retries             | Replay test                       | Single logical booking result              |
| AC-05: Conflicting idempotency reuse  | API integration test              | Documented conflict response               |
| AC-06: Event consistency              | Transaction and outbox test       | No event record for rolled-back booking    |
| AC-07: Existing booking compatibility | Regression test suite             | Existing behavior preserved                |
| AC-08: Corporate authorization        | Security integration test         | Unauthorized cross-account access rejected |

The concurrency scenario deserves particular attention.

A test that submits requests sequentially does not prove that the capacity invariant holds under concurrent transactions.

The integration tests must create actual contention against the persistence mechanism used by the application.

The test environment must also avoid replacing database behavior with an in-memory abstraction that lacks the relevant transaction and isolation semantics.

### **7.11 Define Approval Requirements**

Before implementation begins, the Plan should identify required approval decisions.

For this exercise:

**Business approval:** Confirm corporate capacity semantics, reservation lifecycle, and rejection behavior.

**Architectural approval:** Confirm the persistence and concurrency strategy and whether the existing outbox and idempotency mechanisms are appropriate.

**Engineering approval:** Confirm implementation scope and required validation.

The approval record should identify the approved Plan revision and repository baseline.

Approval should not be inferred from silence or from the mere existence of the Plan.

### **7.12 Planning Deliverable**

At the end of the exercise, the expected deliverable is a structured Plan with:

- A clear goal.
- Confirmed repository references.
- Applicable Instructions and selected Skills.
- Explicit scope boundaries.
- Approved or unresolved business decisions.
- Implementation steps and dependencies.
- Architecture and operational risks.
- Acceptance criteria.
- Validation matrix.
- Approval requirements.
- Defined handoff conditions.

No production code should have been changed.

The exercise is complete when the Plan is sufficiently supported, reviewable, and ready for authorization.

The corporate fleet booking feature itself remains unimplemented.

That distinction is essential.

## **8. Claude Example**

### **8.1 Planning Mode in Claude Code**

Claude Code provides a Plan permission mode intended for investigating a task and proposing an approach before making implementation changes.

In the Claude Code terminal interface, developers can enter Plan Mode using `/plan` or cycle permission modes using `Shift+Tab`. A session can also be started with the `plan` permission mode. Exact interface behavior should be checked against the installed version and environment.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://support.claude.com\&sz=32)

Claude Help Center+1

For a typical local session:

```
cd C:\ZeeWork\Enterprise-AI-Engineering-Handbook\alpha-car-detailing

claude --permission-mode plan
```

This example assumes the companion application exists at that location.

An alternative is to enter an ordinary Claude Code session and select Plan Mode before submitting the implementation request.

The essential requirement is that the agent remains in a planning-only permission context while investigating the task.

For stricter enterprise environments, the Harness should additionally enforce read-only filesystem access, prohibit mutating external tools, and restrict available commands.

### **8.2 Planning Prompt**

The following Prompt provides Claude with the goal and expected planning output.

```
We need to implement corporate fleet booking in
Alpha Car Detailing.

Enter and remain in Planning Mode.

Goal:
Allow authorized corporate customers to book
multiple vehicles while enforcing corporate
booking limits safely.

Before proposing implementation:

1. Inspect the relevant repository structure.
2. Read applicable Instructions.
3. Identify relevant Skills and Knowledge Sources.
4. Locate existing booking logic and contracts.
5. Examine idempotency and transactional outbox support.
6. Identify affected components and dependencies.
7. Record assumptions and unresolved questions.
8. Propose an implementation sequence.
9. Define acceptance criteria and validation gates.
10. Identify required human approvals.

Constraints:
- Do not modify production code or configuration.
- Do not invent repository files or existing capabilities.
- Do not override applicable Instructions.
- Do not introduce unrelated architectural changes.
- Do not claim tests have passed.
- Do not begin implementation.

Produce a reviewable Plan with repository evidence.
Stop after presenting the Plan.
```

This Prompt provides the task-specific operating instructions.

It does not replace repository Instructions or organizational policy.

### **8.3 Expected Planning Behavior**

Claude should first inspect the relevant context.

It should determine whether the existing architecture already includes abstractions for corporate accounts, booking policies, persistence, idempotency, and event publishing.

It should then distinguish verified findings from assumptions.

For example:

```
Confirmed:
- The current booking workflow has been identified.
- The existing API contract has been inspected.
- Applicable repository Instructions have been read.

Requires confirmation:
- Whether limits apply globally or per station.
- Whether cancelled reservations release capacity.
- Whether the current persistence mechanism can
  enforce the limit atomically.

Implementation status:
- Not started.

Validation status:
- Planned, not executed.
```

These are examples of the reporting structure, not claims about the current repository.

A planning agent should avoid statements such as "the transactional outbox is already available" unless it has inspected evidence supporting that conclusion.

### **8.4 Proposed Plan Format**

For the corporate fleet booking task, a practical Claude-generated Plan could use the following structure.

```
# Corporate Fleet Booking — Implementation Plan

## Goal
Describe the required corporate booking capability.

## Repository Baseline
Identify the inspected commit and relevant local state.

## Repository Evidence
Identify verified services, contracts, persistence
components, tests, and relevant source locations.

## Applicable Instructions
List applicable Instruction sources and constraints.

## Selected Skills
Identify reusable Skills and their intended use.

## Scope
Describe in-scope and out-of-scope changes.

## Architecture Decisions
Document the proposed implementation approach,
alternatives considered, and decision rationale.

## Dependencies
List business, technical, and external dependencies.

## Assumptions and Questions
Separate confirmed facts from unresolved assumptions.

## Implementation Tasks
Provide an ordered, executable task sequence.

## Risks
Identify potential failures and mitigations.

## Acceptance Criteria
Define observable business behavior.

## Validation Strategy
Map acceptance criteria to deterministic checks.

## Required Approvals
Identify approvers and approval boundaries.

## Execution Handoff
Define prerequisites for beginning implementation.
```

A Plan should be concise enough to review but detailed enough to execute.

A collection of vague tasks such as "update backend," "add tests," and "verify functionality" does not satisfy the standard.

### **8.5 Review the Plan Before Execution**

Once Claude proposes the Plan, the engineer should review its architectural and operational implications.

Particular attention should be paid to transaction boundaries, concurrency, authorization, API compatibility, and data consistency.

The reviewer should challenge assumptions that could invalidate the implementation.

For example:

> The Plan proposes checking the current number of active reservations before inserting new bookings. Explain how the design prevents two concurrent transactions from both passing that check.

The planning agent should respond with an evidence-based concurrency strategy or acknowledge that the design is incomplete.

It should not begin editing code merely because the reviewer asked a technical question.

The Plan remains in review until an authorized decision permits execution.

### **8.6 Transition from Planning to Execution**

After approval, Claude can transition to an execution-capable permission mode through the supported interface.

However, this transition is distinct from approving the engineering Plan.

An interactive permission-mode change does not, by itself, establish organizational approval.

In a governed Harness, the approval decision must be recorded independently and associated with the specific Plan revision.

The Developer Agent then receives the approved Plan and begins implementation within the allowed scope.

The implementation should preserve traceability between planned tasks, actual changes, and verification evidence.

### **8.7 Handling New Evidence During Execution**

Suppose the Developer Agent discovers that the existing booking persistence abstraction cannot enforce the required global corporate capacity invariant.

The agent should first determine whether this discovery can be addressed within the approved design.

If it requires a materially different persistence model or transaction boundary, execution should pause.

The agent should record:

```
Plan deviation:
The approved capacity enforcement approach cannot
guarantee the required invariant using the existing
persistence contract.

Evidence:
Relevant implementation and test findings.

Impact:
Persistence design and affected implementation tasks.

Proposed action:
Return to Planning Mode and revise the approach.

Status:
Awaiting architectural decision.
```

The Lead Agent can coordinate the revised Plan and route it for approval.

This is not a failure of Planning Mode.

It is the intended mechanism for incorporating new evidence without losing control of the implementation.

### **8.8 Claude Code and Enterprise Planning Controls**

Claude Code's built-in planning experience is valuable for interactive engineering work, but an enterprise Harness should not depend exclusively on a model's verbal commitment to remain read-only.

The Harness should enforce the intended boundary through the relevant permission controls, tool allowlists, workspace isolation, and approval state.

The distinction is important for external tools and connected services.

An operation that does not edit a source file can still mutate infrastructure, create resources, change tickets, or expose data through external tool calls.

An enterprise planning environment should permit only the discovery capabilities required for the task.

The more consequential the repository or environment, the more important it becomes to enforce those restrictions outside the agent's natural-language context.

Chapter 27 status: In progress — next section: GitHub Copilot Comparison

## **9. GitHub Copilot Comparison**

### **9.1 Planning Mode in GitHub Copilot**

GitHub Copilot supports a planning-first development workflow that separates implementation design from code modification.

Like Claude Code, GitHub Copilot can investigate an existing repository, identify relevant components, decompose a requested change into implementation tasks, and produce a Plan for review before execution begins.

GitHub Copilot provides a dedicated **Plan agent** in supported development environments. The agent uses read-oriented codebase investigation to prepare an implementation approach, identify ambiguities, and present proposed steps. After reviewing the Plan, the developer can initiate implementation through an execution-capable agent.

The precise interface and available controls vary across IDEs and Copilot environments.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://docs.github.com\&sz=32)

GitHub Docs+1

The underlying engineering principle remains the same:

> Planning should establish an evidence-based implementation approach before an AI Agent modifies the repository.

The distinction is particularly important in enterprise systems, where the correctness of a change depends not only on generated code but also on architectural boundaries, business rules, integration contracts, and operational behavior.

For Alpha Car Detailing, the corporate fleet booking requirement presents the same planning challenges regardless of whether Claude Code or GitHub Copilot performs the investigation.

Both platforms must account for corporate capacity limits, transaction consistency, idempotency, authorization, existing architecture, and validation requirements.

A change in coding platform does not change the business invariants that the implementation must preserve.

### **9.2 Using the GitHub Copilot Plan Agent**

In Visual Studio Code, a developer can select the Plan agent from the Copilot agent selector or invoke the dedicated planning workflow using `/plan`.

A typical workflow consists of:

1. Opening the Alpha Car Detailing repository.
2. Selecting the Plan agent.
3. Submitting the corporate fleet booking requirement.
4. Allowing Copilot to investigate relevant repository components.
5. Reviewing the proposed implementation approach.
6. Resolving assumptions and architectural questions.
7. Approving the Plan for execution when appropriate.
8. Transitioning to an implementation-capable agent.

The Plan agent is intended to investigate and propose changes rather than directly modify implementation files. GitHub's documented workflow supports reviewing and refining the Plan before beginning implementation.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://docs.github.com\&sz=32)

GitHub Docs+1

For example, the developer might submit:

```
/plan Implement corporate fleet booking for
Alpha Car Detailing.

Before implementation:

- Inspect the existing booking architecture.
- Read applicable repository Instructions.
- Identify reusable services and components.
- Determine affected APIs and data contracts.
- Analyze booking-limit enforcement.
- Identify concurrency and idempotency risks.
- Review transactional outbox integration.
- Define implementation tasks and dependencies.
- Establish acceptance criteria.
- Define deterministic validation requirements.

Do not modify source code.

Produce an evidence-based implementation plan
for architectural review.
```

The expected output should contain repository findings, design decisions, implementation tasks, assumptions, risks, and validation requirements.

As with Claude Code, merely selecting a planning capability does not guarantee that all architectural concerns have been addressed.

The quality of the result depends on available evidence, applicable Instructions, the agent's reasoning, and the developer's review.

### **9.3 Planning Versus Agent Mode in GitHub Copilot**

GitHub Copilot distinguishes planning from execution-oriented agent behavior.

The Plan agent focuses on analyzing the request and preparing the approach.

Agent mode can make code changes, use development tools, execute permitted commands, and iterate on implementation results.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://docs.github.com\&sz=32)

GitHub Docs+1

This distinction closely parallels the separation established for Claude Code earlier in this chapter.

| Responsibility                 | GitHub Copilot Plan Agent  | GitHub Copilot Agent Mode                          |
| ------------------------------ | -------------------------- | -------------------------------------------------- |
| Interpret requirements         | Primary activity           | Uses established requirements                      |
| Inspect repository             | Yes                        | Yes                                                |
| Identify implementation scope  | Yes                        | Refines as needed                                  |
| Analyze dependencies           | Yes                        | Handles discovered dependencies                    |
| Document assumptions           | Expected planning activity | Reports new assumptions or findings                |
| Prepare task sequence          | Yes                        | Executes and adjusts tasks                         |
| Modify application code        | Not the intended operation | Yes, subject to permissions                        |
| Execute implementation tests   | Defines required tests     | Can run permitted tests                            |
| Produce implementation changes | No                         | Yes                                                |
| Handle plan drift              | Supports revisions         | Identifies deviations and escalates when necessary |

These responsibilities describe the intended engineering workflow, not an automatic guarantee of enterprise governance.

For example, an interactive transition from Plan to Agent mode does not necessarily satisfy an organization's requirement for independent architectural approval.

The Harness must enforce that requirement separately.

### **9.4 Repository Instructions and Planning Context**

GitHub Copilot supports repository-specific customization, including instruction files that guide agent behavior.

For example, a repository may contain:

```
.github/
    copilot-instructions.md
```

The Alpha Car Detailing team may use this file to describe coding conventions, architectural constraints, testing standards, and expected engineering practices.

GitHub Copilot also supports more targeted instruction and agent customization mechanisms, depending on the environment and configuration.

Planning should apply the relevant Instructions rather than treating the Prompt as the only source of direction.

Consider a repository Instruction requiring all booking persistence operations to remain behind application and infrastructure abstractions consistent with Clean Architecture.

If the Plan proposes placing SQL Server queries directly inside an API controller, it conflicts with the repository's architectural standard.

The correct response is to revise the Plan.

The Plan agent should not bypass the Instruction merely because the proposed implementation would be shorter.

This illustrates a vendor-neutral rule:

**Planning determines an implementation approach within established engineering constraints. It does not create authority to override those constraints.**

### **9.5 The Corporate Fleet Booking Example in Copilot**

The same corporate fleet booking requirement can be used to evaluate whether Copilot produces an adequate Plan.

The agent should first identify the existing application boundaries and the repository's supported extension points.

It should then determine how the booking operation will interact with corporate account limits, persistence, idempotency, and event publication.

A useful Copilot-generated planning artifact should address the following engineering decisions.

| Planning concern         | Expected analysis                                                       |
| ------------------------ | ----------------------------------------------------------------------- |
| Business behavior        | Define the meaning and lifecycle of corporate booking limits            |
| API contract             | Determine request, response, validation, and compatibility requirements |
| Authorization            | Identify how users are authorized to book for corporate accounts        |
| Application architecture | Reuse existing application-layer patterns                               |
| Capacity enforcement     | Select a mechanism that preserves limits under concurrency              |
| Idempotency              | Reuse existing request deduplication capabilities where appropriate     |
| Persistence              | Define transaction boundaries and rollback expectations                 |
| Event delivery           | Preserve consistency using the approved event-publication architecture  |
| Dependencies             | Identify external contracts and prerequisite decisions                  |
| Validation               | Define deterministic evidence for each critical invariant               |

A Plan that merely recommends adding a `CorporateFleetBookingController` and corresponding service would be insufficient.

Those implementation components may be necessary, but they do not demonstrate that the agent has addressed the system's most significant risks.

The expected planning quality should be identical whether the agent operates through Claude Code or Copilot.

### **9.6 Reviewing and Refining the Copilot Plan**

GitHub Copilot allows developers to review and refine a proposed Plan before beginning implementation.

In supported VS Code workflows, the developer can initiate implementation or open the Plan for further editing and discussion. The VS Code documentation also describes planning workflows that support explicit review and implementation decisions.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://docs.github.com\&sz=32)

GitHub Docs+1

For Alpha Car Detailing, the architect might challenge the proposed design:

> The Plan identifies a booking-limit check before reservation persistence. Explain which database consistency mechanism prevents two concurrent requests from exceeding the corporate limit. Update the Plan to include the corresponding integration test.

Copilot should refine the proposed approach using repository evidence.

It should not present a sequential unit test as adequate proof of concurrency safety.

If the current architecture cannot satisfy the requirement without a significant persistence change, the Plan should identify that limitation and request an architectural decision.

This review process illustrates an important distinction between planning completeness and planning correctness.

A Plan may contain every required section and still propose an unsafe design.

Document completeness can be checked mechanically. Architectural correctness requires additional reasoning, evidence, and review.

### **9.7 Comparison with Claude Code**

Claude Code and GitHub Copilot provide different interaction models and customization surfaces, but both can support deliberate planning before implementation.

| Dimension                   | Claude Code                                               | GitHub Copilot                                                    |
| --------------------------- | --------------------------------------------------------- | ----------------------------------------------------------------- |
| Primary planning experience | Plan permission mode in Claude Code                       | Dedicated Plan agent in supported IDEs                            |
| Common interface            | Terminal or supported integrations                        | IDE-integrated agent interface                                    |
| Planning activation         | Plan permission mode or supported commands                | Plan agent selection or `/plan` in supported VS Code workflows    |
| Repository understanding    | Repository inspection through permitted tools             | IDE and agent-based codebase investigation                        |
| Instructions                | Commonly `CLAUDE.md` and applicable configuration         | Copilot instruction files and supported customization             |
| Reusable workflows          | Claude Skills and related tooling                         | Supported agent, instruction, and reusable workflow customization |
| Implementation boundary     | Transition from planning to execution-capable permissions | Transition from Plan to implementation-capable agent              |
| Human review                | Can review proposed Plan before execution                 | Can review and refine Plan before implementation                  |
| Harness integration         | External orchestration and permission controls            | External orchestration and permission controls                    |
| Validation responsibility   | Remains with implementation and validation workflow       | Remains with implementation and validation workflow               |

The differences are primarily in interfaces, configuration, and tool integration.

Neither platform should be treated as providing complete enterprise governance solely because it supports a planning workflow.

The enterprise Harness remains responsible for enforcing the organization's approval, validation, auditability, and execution policies.

### **9.8 Planning Artifacts and Portability**

One advantage of representing the approved Plan as a structured Markdown artifact is that it can be reviewed independently of the tool that produced it.

For example, a planning document might be stored at an illustrative run-workspace location:

```
harness/
    runs/
        fleet-booking-001/
            plan.md
            approvals.json
            evidence/
```

This structure represents a possible Harness implementation, not a required GitHub Copilot repository layout.

The Plan should contain sufficient information for a Developer Agent using another platform to execute it without reconstructing all design decisions.

A Claude-based Lead Agent might produce the Plan, while a Copilot-based Developer Agent performs implementation.

The reverse is also possible.

However, portability requires more than moving a Markdown file between tools.

The receiving agent must have access to the appropriate repository baseline, Instructions, Skills or equivalent procedures, and validation requirements.

Any platform-specific assumptions must be identified.

For example, a Plan should not depend on an undocumented IDE interaction that another agent cannot reproduce.

Where possible, implementation tasks should describe engineering outcomes and constraints rather than platform-specific clicks.

This supports the handbook's central vendor-neutral architecture.

### **9.9 Copilot Planning Inside the Enterprise Harness**

GitHub Copilot's Plan agent can support interactive planning, but an enterprise Harness may require additional capabilities.

These include a stable Plan identifier, repository baseline tracking, immutable approval records, deterministic planning gates, and explicit control of execution permissions.

The organization's Harness might implement the following logical sequence:

\#chatgpt-mermaid-\_r_1bt6\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1bt6\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bt6\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1bt6\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1bt6\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1bt6\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1bt6\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1bt6\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1bt6\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1bt6\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bt6\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bt6\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1bt6\_ p{margin:0;}#chatgpt-mermaid-\_r_1bt6\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1bt6\_ .label text,#chatgpt-mermaid-\_r_1bt6\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .node rect,#chatgpt-mermaid-\_r_1bt6\_ .node circle,#chatgpt-mermaid-\_r_1bt6\_ .node ellipse,#chatgpt-mermaid-\_r_1bt6\_ .node polygon,#chatgpt-mermaid-\_r_1bt6\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .rough-node .label text,#chatgpt-mermaid-\_r_1bt6\_ .node .label text,#chatgpt-mermaid-\_r_1bt6\_ .image-shape .label,#chatgpt-mermaid-\_r_1bt6\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1bt6\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .rough-node .label,#chatgpt-mermaid-\_r_1bt6\_ .node .label,#chatgpt-mermaid-\_r_1bt6\_ .image-shape .label,#chatgpt-mermaid-\_r_1bt6\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1bt6\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1bt6\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bt6\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bt6\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1bt6\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bt6\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bt6\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bt6\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1bt6\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1bt6\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1bt6\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1bt6\_ .icon-shape,#chatgpt-mermaid-\_r_1bt6\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1bt6\_ .icon-shape p,#chatgpt-mermaid-\_r_1bt6\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1bt6\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1bt6\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1bt6\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1bt6\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1bt6\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1bt6\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1bt6\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1bt6\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1bt6\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bt6\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1bt6\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1bt6\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bt6\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1bt6\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1bt6\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1bt6\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .node rect,#chatgpt-mermaid-\_r_1bt6\_ .node circle,#chatgpt-mermaid-\_r_1bt6\_ .node ellipse,#chatgpt-mermaid-\_r_1bt6\_ .node polygon,#chatgpt-mermaid-\_r_1bt6\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1bt6\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1bt6\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1bt6\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1bt6\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1bt6\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Engineering PromptRepository DiscoveryApplicable InstructionsCopilot Plan AgentProposed PlanPlan Quality GateApproval Required?Human ReviewApproved?Execution AuthorizationDeveloper AgentImplementationIndependent ValidationOutcome EvaluationYesNoYesNo

The Copilot Plan agent is one implementation of the planning capability.

It does not need to own the entire enterprise workflow.

The Harness can maintain responsibility for coordinating the process, preserving evidence, and preventing unauthorized transitions.

This also allows an organization to use different coding platforms without replacing the governance architecture.

### **9.10 Planning Versus Developer Task Tracking**

GitHub Copilot's agent workflows can also use task lists and intermediate execution planning to manage complex implementation work.

These mechanisms should not be confused with the approved engineering Plan.

An approved Plan defines the implementation approach and its constraints.

A Developer Agent's task list tracks progress as work is performed.

A Scratchpad records temporary observations and execution context.

These artifacts may reference one another, but they have different responsibilities.

For example, the approved Plan may require an atomic booking-limit enforcement mechanism and a concurrency integration test.

During execution, Copilot may break the implementation into smaller activities such as creating a migration, updating a service method, and adding test fixtures.

Those activities can be tracked as execution tasks without requiring new approval for every minor implementation detail.

However, if Copilot discovers that the approved concurrency strategy cannot work and proposes a different architectural boundary, that is a potential Plan change.

The distinction preserves flexibility without sacrificing governance.

### **9.11 Enterprise Considerations**

GitHub Copilot's integration with development environments can make planning accessible within an engineer's normal workflow.

This is useful when reviewing an existing solution, exploring affected components, and refining requirements interactively.

However, enterprise teams should not assume that IDE availability automatically establishes the operational controls required for regulated or high-impact changes.

Three concerns are particularly important.

**Permission control.** A planning agent should not receive unnecessary write or external mutation permissions. Organizations should verify the actual tools exposed by the selected environment, including extensions and external integrations.

**Approval independence.** Starting implementation from the IDE is a tool action. It must not be confused with organizational authorization.

**Artifact continuity.** Approved Plans, repository baselines, and validation requirements should remain traceable after the interactive session ends.

These concerns apply equally to Claude Code and other AI coding platforms.

An organization may use Copilot for the planning interface while relying on its Harness to enforce workflow state, approval records, and deterministic validation.

### **9.12 Architectural Interpretation**

The significance of GitHub Copilot's planning support is not that it introduces a fundamentally different engineering method from Claude Code.

Both platforms can separate investigation from implementation.

Both can use repository context to create an approach.

Both depend on appropriate Instructions and engineering review.

Both require controls beyond planning itself to establish production readiness.

For enterprise architects, the more important decision is where the planning responsibility belongs.

If planning exists only as an interactive IDE feature, it may be difficult to enforce consistently across teams, automation pipelines, and unattended agent executions.

If planning is defined as a Harness capability, Claude Code, GitHub Copilot, and other AI Agents can participate through platform-specific adapters while preserving consistent engineering obligations.

The architecture should therefore define Planning Mode independently of any specific interface.

It should specify the required inputs, prohibited operations, Plan structure, approval conditions, and handoff requirements.

Platform-specific implementations can then satisfy that contract.

> **Architect's Note**
>
> Do not select an AI coding platform merely because it can produce a detailed Plan. Evaluate whether the Plan can be grounded in repository evidence, reviewed before implementation, associated with a controlled repository baseline, revised when assumptions change, and enforced through the organization's Harness.
>
> A well-designed enterprise planning process should remain valid even when the organization changes its preferred AI coding agent.

The central conclusion is that GitHub Copilot and Claude Code offer different practical interfaces for a shared engineering discipline.

**Planning is an architectural responsibility. The AI coding platform is an implementation choice.**

Chapter 27 status: In progress — next section: Codex Comparison

## **10. Codex Comparison**

### **10.1 Planning Mode in OpenAI Codex**

OpenAI Codex supports planning-first software development through a dedicated Plan Mode and repository-based planning conventions.

Like Claude Code and GitHub Copilot, Codex can investigate an existing codebase, interpret requirements, identify dependencies, and propose an implementation approach before modifying application code.

Codex provides an interactive planning experience in which the agent can gather context, ask clarifying questions, examine alternatives, and produce a proposed implementation plan. Supported interfaces allow developers to enter Plan Mode using `/plan` or the appropriate mode-selection control.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://learn.chatgpt.com\&sz=32)

ChatGPT Learn+1

Codex also supports longer-running execution plans through repository conventions such as `PLANS.md`, which can define how complex work should be decomposed, documented, implemented, and verified.

These capabilities address related but distinct concerns:

- **Plan Mode** separates investigation and design from implementation.
- **Execution Plans** describe how substantial engineering work should proceed over multiple implementation steps.
- **Repository Instructions** establish persistent constraints that apply while planning and executing.
- **Harness orchestration** governs approvals, agent responsibilities, validation gates, and execution authority.

The distinction matters because producing an implementation plan does not itself authorize implementation.

For Alpha Car Detailing, Codex should investigate the corporate fleet booking requirement, identify the relevant architectural constraints, and establish a verifiable implementation approach before changing the booking service.

The same engineering principle applies:

> Planning determines how a change should be implemented. Execution performs the authorized change. Validation and evaluation establish whether the resulting system satisfies its engineering and business obligations.

### **10.2 Starting a Planning Session in Codex**

Codex can operate through several development environments, including a command-line interface, IDE integrations, and supported desktop experiences.

For an engineer working with the Alpha Car Detailing repository, the command-line interface provides a practical example.

From the repository's application directory:

```
cd C:\ZeeWork\Enterprise-AI-Engineering-Handbook\alpha-car-detailing

codex
```

Within an interactive session, the developer can select Plan Mode using:

```
/plan
```

The developer can then submit the planning request.

In supported Codex interfaces, Plan Mode establishes a conversational planning phase that remains distinct from ordinary implementation execution.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://learn.chatgpt.com\&sz=32)

ChatGPT Learn+1

For enterprise use, a planning-only mode should be reinforced by actual permissions and workspace restrictions. A natural-language instruction not to modify code should not be treated as a complete security boundary.

### **10.3 Corporate Fleet Booking Planning Prompt**

The following Prompt is suitable for a Codex planning session.

```
We need to implement corporate fleet booking
in Alpha Car Detailing.

Remain in Planning Mode.

Business goal:
Allow authorized corporate customers to book
multiple vehicles while respecting contractual
booking limits.

Investigate the existing repository before
proposing implementation.

Planning responsibilities:

1. Read applicable repository Instructions.
2. Discover existing booking components.
3. Identify corporate account and authorization logic.
4. Inspect booking persistence and transaction handling.
5. Examine existing idempotency mechanisms.
6. Identify event publishing and outbox capabilities.
7. Determine affected APIs, services, and contracts.
8. Decompose implementation into executable tasks.
9. Identify technical dependencies and risks.
10. Record assumptions and unresolved questions.
11. Define acceptance criteria.
12. Establish deterministic validation requirements.
13. Identify required architectural approvals.

Constraints:
- Do not modify application code.
- Do not alter configuration or infrastructure.
- Do not invent repository capabilities.
- Do not introduce unrelated architectural changes.
- Do not bypass applicable Instructions.
- Do not assume unresolved business rules are approved.
- Do not begin implementation.

Output:
A reviewable engineering Plan supported
by repository evidence.

Stop after presenting the Plan.
```

This request separates the desired outcome from the implementation approach.

It also makes clear that the planning agent must examine the existing architecture rather than immediately generating new endpoints, services, or persistence abstractions.

### **10.4 Codex Plan Mode Versus Execution**

Codex distinguishes its conversational Plan Mode from implementation-oriented operation.

Its documented Plan Mode emphasizes investigation, clarification, and producing a sufficiently detailed proposed Plan for subsequent execution. The planning instructions also distinguish this mode from the separate progress-tracking mechanism used during implementation.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://github.com\&sz=32)

GitHub

That separation aligns with the role boundaries established in Part III of the handbook.

| Responsibility                     | Codex Plan Mode                                  | Codex Execution                           |
| ---------------------------------- | ------------------------------------------------ | ----------------------------------------- |
| Understand the engineering goal    | Primary responsibility                           | Uses approved requirements                |
| Inspect repository evidence        | Yes                                              | Yes                                       |
| Identify affected components       | Yes                                              | Refines findings as necessary             |
| Review applicable Instructions     | Yes                                              | Must continue complying                   |
| Evaluate architecture alternatives | Yes                                              | Implements approved decisions             |
| Identify dependencies              | Yes                                              | Resolves implementation dependencies      |
| Record assumptions                 | Yes                                              | Reports new or invalidated assumptions    |
| Prepare implementation tasks       | Yes                                              | Performs tasks                            |
| Modify production code             | Not permitted by the chapter's planning contract | Permitted within authorization            |
| Define validation requirements     | Yes                                              | Implements and executes applicable checks |
| Produce implementation evidence    | No                                               | Yes                                       |
| Handle material Plan drift         | Re-plan and propose revisions                    | Detect, report, and escalate              |

An important architectural distinction follows.

A Codex execution session may maintain a task checklist or progress record. That record is not automatically the approved engineering Plan.

The approved Plan establishes the design and implementation obligations.

Execution progress describes which parts of the authorized work have been performed.

The Scratchpad captures temporary discoveries, experiments, and observations.

These artifacts should remain separate even when the same Codex session participates in more than one phase.

### **10.5 Repository Instructions Through AGENTS.md**

Codex recognizes `AGENTS.md` as a repository Instruction mechanism.

Such files can communicate architectural boundaries, coding conventions, testing expectations, and other engineering requirements.

Instructions can be scoped to directories, allowing teams to establish general repository guidance and more specific requirements for individual components.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://github.com\&sz=32)

GitHub

An illustrative Alpha Car Detailing structure might be:

```
alpha-car-detailing/
│
├── AGENTS.md
│
├── src/
│   ├── Booking.API/
│   ├── Booking.Application/
│   ├── Booking.Domain/
│   └── Booking.Infrastructure/
│
├── tests/
│   ├── Booking.UnitTests/
│   └── Booking.IntegrationTests/
│
└── harness/
```

This structure is illustrative and must be reconciled with the actual companion application's repository layout.

The root `AGENTS.md` might establish requirements such as:

```
# Alpha Car Detailing — Engineering Instructions

## Architecture

- Follow the existing Clean Architecture boundaries.
- Keep business invariants within the appropriate domain
  or application components.
- Do not introduce direct infrastructure dependencies
  into the API layer.
- Reuse existing persistence abstractions.

## Reliability

- Preserve transactional consistency.
- Reuse existing idempotency mechanisms when applicable.
- Do not publish integration events before the
  corresponding transaction is committed.

## Testing

- Add automated tests for changed business behavior.
- Validate concurrent operations where invariants
  depend on transaction isolation.
- Preserve existing API contracts unless breaking
  changes are explicitly approved.

## Planning

- Inspect existing implementation before proposing changes.
- Record assumptions and unresolved dependencies.
- Define acceptance criteria and validation requirements.
- Obtain required approval before implementation.
```

These Instructions guide both planning and execution.

They do not replace the task-specific Prompt.

For corporate fleet booking, the Prompt establishes the requested capability. The Instructions establish how work must conform to the repository's engineering standards.

The Plan reconciles both with actual repository evidence.

A Plan proposing direct SQL Server operations inside an API controller would conflict with the illustrative architecture Instructions and should be revised.

### **10.6 Using PLANS.md for Complex Engineering Work**

Codex supports a repository-based approach to substantial implementation tasks through execution-plan conventions.

OpenAI's published guidance describes using `PLANS.md` to define the structure and requirements of an executable implementation plan, sometimes called an *ExecPlan*.

Such plans can act as living engineering documents, recording objectives, implementation progress, discoveries, decisions, and validation evidence over longer development activities.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://github.com\&sz=32)

GitHub

This approach is particularly relevant for enterprise work that cannot be completed safely in one isolated coding step.

However, the handbook's terminology requires an important distinction.

A Codex ExecPlan may combine planning guidance with execution-progress information. The enterprise Harness separates these responsibilities more explicitly.

The approved engineering Plan should contain the authorized approach.

The Scratchpad should contain temporary observations and experiments.

Execution history should contain actual progress and verification evidence.

An organization may use the Codex `PLANS.md` convention while retaining those logical boundaries.

For example:

```
alpha-car-detailing/
│
├── AGENTS.md
├── PLANS.md
│
└── harness/
    └── runs/
        └── corporate-fleet-booking-001/
            ├── plan.md
            ├── scratchpad.md
            ├── execution-log.md
            ├── approvals.json
            └── evidence/
```

In this illustrative arrangement:

| Artifact           | Responsibility                                                                              |
| ------------------ | ------------------------------------------------------------------------------------------- |
| `AGENTS.md`        | Persistent repository Instructions                                                          |
| `PLANS.md`         | Repository convention defining how complex execution plans should be written and maintained |
| `plan.md`          | Task-specific proposed or approved implementation approach                                  |
| `scratchpad.md`    | Temporary observations and unresolved execution findings                                    |
| `execution-log.md` | Actual implementation activities and results                                                |
| `approvals.json`   | Authorization records linked to the Plan revision                                           |
| `evidence/`        | Build, test, review, and validation artifacts                                               |

The example is a proposed Harness structure, not a mandatory Codex file layout.

Nor should a team automatically introduce all these files into an existing repository.

The appropriate structure depends on current repository conventions, operational needs, and governance requirements.

### **10.7 Planning an Executable Sequence**

A central characteristic of a useful Codex Plan is that another engineer or agent should be able to implement it without reconstructing essential decisions.

The Plan should be sufficiently complete to answer which components will change, in what order, under which constraints, and how success will be established.

For Alpha Car Detailing, an implementation sequence might be represented as follows.

| Phase                      | Responsibility                              | Completion evidence          |
| -------------------------- | ------------------------------------------- | ---------------------------- |
| Business confirmation      | Resolve corporate booking-limit semantics   | Approved business rules      |
| Architecture design        | Select transaction and concurrency approach | Reviewed design decision     |
| API contract               | Define fleet booking request and response   | Contract assessment          |
| Application implementation | Extend existing booking workflow            | Focused behavior tests       |
| Persistence integration    | Guarantee atomic capacity enforcement       | Concurrency tests            |
| Idempotency integration    | Prevent duplicate logical bookings          | Retry and conflict tests     |
| Event integration          | Preserve transactional outbox behavior      | Event consistency tests      |
| Security validation        | Enforce corporate authorization             | Authorization test results   |
| Final validation           | Execute mandatory deterministic gates       | Recorded gate evidence       |
| Outcome evaluation         | Assess business acceptance criteria         | Structured evaluation result |

This sequence is more useful than an unstructured list of files to create.

It identifies dependencies and verification obligations.

The Plan should also distinguish decisions that require approval from implementation details delegated to the Developer Agent.

For example, the architectural decision to enforce capacity atomically is significant.

The names of private helper methods used to implement the approved approach are generally not.

A Plan that attempts to prescribe every trivial implementation detail risks becoming unnecessarily rigid.

A Plan that omits transaction consistency, idempotency, and concurrency decisions is too weak.

### **10.8 Plan Revisions During Codex Execution**

Codex execution-plan guidance supports maintaining planning artifacts as work progresses.

This is consistent with the handbook's principle that Plans must remain adaptable when new evidence emerges.

However, adaptability does not mean unrestricted self-authorization.

Suppose Codex begins implementing corporate fleet booking and discovers that existing persistence abstractions cannot guarantee atomic booking-limit enforcement.

The agent may need to propose a new persistence mechanism.

That discovery affects an approved architectural decision.

The appropriate sequence is:

1. Record the discovery and supporting evidence in the run workspace.
2. Determine whether the change remains within the approved scope.
3. Identify the required Plan revision.
4. Obtain architectural approval if the deviation is material.
5. Resume implementation using the revised approved approach.

The agent should not silently introduce a new capacity service or database synchronization mechanism simply because the change would make the implementation easier.

The approval requirement belongs to the enterprise Harness and governance policy.

It must remain effective regardless of the planning conventions used by Codex.

### **10.9 Comparing Claude Code, GitHub Copilot, and Codex**

All three platforms can support the separation of planning and implementation, but their interfaces and repository customization mechanisms differ.

| Dimension                  | Claude Code                                    | GitHub Copilot                                 | OpenAI Codex                                 |
| -------------------------- | ---------------------------------------------- | ---------------------------------------------- | -------------------------------------------- |
| Primary planning interface | Plan permission mode                           | Plan agent in supported environments           | Plan Mode                                    |
| Common activation          | Permission-mode selection                      | Agent selection or `/plan`                     | `/plan` or supported mode selector           |
| Repository Instructions    | `CLAUDE.md`                                    | Copilot instruction files                      | `AGENTS.md`                                  |
| Planning artifact          | Proposed Plan or organization-defined artifact | Proposed Plan or organization-defined artifact | Proposed Plan; optional ExecPlan conventions |
| Longer execution plans     | Can use governed Plan artifacts                | Can use governed Plan artifacts                | Documented `PLANS.md` conventions            |
| Repository investigation   | Through permitted tools                        | Through IDE and agent tools                    | Through permitted repository tools           |
| Plan approval              | Interactive or Harness-governed                | Interactive or Harness-governed                | Interactive or Harness-governed              |
| Execution transition       | Permission-mode or workflow change             | Transition to implementation-capable agent     | Transition to execution mode                 |
| Scratchpad separation      | Defined by Harness design                      | Defined by Harness design                      | Defined by Harness design                    |
| Deterministic validation   | External engineering gates                     | External engineering gates                     | External engineering gates                   |
| Enterprise governance      | Requires organizational controls               | Requires organizational controls               | Requires organizational controls             |

The practical differences should not obscure the common engineering requirement.

A Plan must be grounded in repository evidence, respect applicable Instructions, identify material risks, and define measurable validation requirements.

No platform-specific planning interface removes that responsibility.

### **10.10 Codex Inside the Enterprise Harness**

Codex can be integrated into the enterprise planning architecture as a planning-capable agent.

The Harness remains responsible for enforcing the lifecycle.

\#chatgpt-mermaid-\_r_1c04\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c04\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c04\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c04\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c04\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c04\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c04\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c04\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c04\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c04\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c04\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c04\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c04\_ p{margin:0;}#chatgpt-mermaid-\_r_1c04\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1c04\_ .label text,#chatgpt-mermaid-\_r_1c04\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .node rect,#chatgpt-mermaid-\_r_1c04\_ .node circle,#chatgpt-mermaid-\_r_1c04\_ .node ellipse,#chatgpt-mermaid-\_r_1c04\_ .node polygon,#chatgpt-mermaid-\_r_1c04\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .rough-node .label text,#chatgpt-mermaid-\_r_1c04\_ .node .label text,#chatgpt-mermaid-\_r_1c04\_ .image-shape .label,#chatgpt-mermaid-\_r_1c04\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1c04\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .rough-node .label,#chatgpt-mermaid-\_r_1c04\_ .node .label,#chatgpt-mermaid-\_r_1c04\_ .image-shape .label,#chatgpt-mermaid-\_r_1c04\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1c04\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1c04\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c04\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c04\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1c04\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c04\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c04\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c04\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1c04\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1c04\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c04\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1c04\_ .icon-shape,#chatgpt-mermaid-\_r_1c04\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c04\_ .icon-shape p,#chatgpt-mermaid-\_r_1c04\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1c04\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1c04\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c04\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1c04\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1c04\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c04\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c04\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c04\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c04\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c04\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c04\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c04\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c04\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c04\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1c04\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1c04\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .node rect,#chatgpt-mermaid-\_r_1c04\_ .node circle,#chatgpt-mermaid-\_r_1c04\_ .node ellipse,#chatgpt-mermaid-\_r_1c04\_ .node polygon,#chatgpt-mermaid-\_r_1c04\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1c04\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1c04\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1c04\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1c04\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c04\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Task PromptHarness Task IntakeRead-Only RepositoryDiscoveryApplicable Instructions andSkillsCodex Planning SessionProposed Engineering PlanPlan Quality GatePlan Ready?Approval Required?Human ReviewApproved?Execution AuthorizationDeveloper AgentImplementationReviewer and ValidatorEvaluatorCompletion DecisionNoYesYesRevisionApprovedNo

The diagram illustrates a logical architecture.

It does not require that every planning task be performed by a separate Codex process or that the Harness use Codex for execution.

An organization may use a Claude-based Lead Agent to coordinate planning, Codex for specialized repository analysis, and another supported agent for implementation.

The approved Plan provides the common contract between these components.

To support such workflows, the Harness should establish a consistent representation for:

- Task identity and requested goal.
- Repository baseline.
- Applicable Instructions and constraints.
- Planning findings.
- Design decisions and assumptions.
- Implementation tasks and dependencies.
- Validation requirements.
- Approval status.
- Material Plan revisions.
- Execution and acceptance evidence.

The AI platform may change. The engineering contract should remain stable.

### **10.11 Security and Permission Boundaries**

Codex provides execution permissions and sandbox controls that can help restrict the actions available to an agent. These controls should be configured according to the organization's security requirements.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://learn.chatgpt.com\&sz=32)

ChatGPT Learn

Planning is not inherently harmless merely because it does not intentionally modify code.

A planning agent may have access to sensitive configuration, credentials, internal architecture documentation, and external tools.

It may encounter malicious or misleading content embedded in repository files.

It may also execute commands that appear observational but have side effects.

For these reasons, enterprise planning should follow least-privilege principles.

A strict implementation may use a read-only repository snapshot, approved inspection commands, restricted network access, and separate execution permissions.

The Harness should also ensure that the planning agent cannot authorize its own transition into a more privileged state.

A proposed Plan must not become an implicit grant of permission to modify source files, access production resources, or perform deployment actions.

Those permissions must be supplied through the authorized workflow.

### **10.12 The Value of Codex Execution Plans**

Codex's execution-plan conventions highlight an important engineering requirement: complex work needs continuity beyond a single model interaction.

Long-running implementations may involve multiple development sessions, repeated validations, intermediate findings, and revisions.

Without a durable Plan, the agent may lose the original rationale or make contradictory decisions during later execution steps.

A structured Plan reduces this risk by preserving:

**Intent:** Why the change is required.

**Scope:** Which components and behaviors may be changed.

**Design:** Which implementation approach was selected and why.

**Dependencies:** What must be resolved before subsequent work proceeds.

**Validation:** Which evidence demonstrates acceptable implementation.

**Continuity:** What another agent or engineer needs to resume the work.

However, the Plan must not become an unstructured accumulation of temporary execution observations.

The approved approach should remain distinguishable from the historical record of what happened during implementation.

This is where the handbook's separation of Plan, Scratchpad, Harness Memory, and execution evidence becomes valuable.

Each artifact has a different lifecycle and authority.

### **10.13 Architectural Interpretation**

Codex demonstrates that planning can be both an interactive activity and a durable engineering practice.

Plan Mode supports investigation and design before implementation.

Execution-plan conventions provide a way to preserve substantial implementation intent across multiple steps or sessions.

Neither capability, by itself, establishes the complete enterprise Harness described in this handbook.

The Harness must still enforce agent responsibilities, permissions, approval boundaries, deterministic validation, evaluation, and auditability.

For Alpha Car Detailing, the ultimate requirement is not that Codex produces a detailed Plan.

It is that the corporate fleet booking change can be implemented in a controlled manner, with documented business rules, a defensible concurrency strategy, safe retry behavior, transactional event consistency, and verifiable acceptance criteria.

These obligations remain constant regardless of the selected coding agent.

> **Architect's Note**
>
> Treat Codex Plan Mode and execution-plan conventions as capabilities that can support the enterprise planning architecture, not as replacements for it.
>
> Preserve an explicit distinction between the approved Plan, temporary Scratchpad observations, execution progress, and validation evidence. Where Codex workflows combine these concerns in a living document, establish clear ownership and approval boundaries before adopting that convention across enterprise repositories.

The comparison leads to a broader conclusion.

Claude Code, GitHub Copilot, and Codex can all participate in planning-first engineering workflows.

Their interaction models differ, but the architectural responsibilities are shared.

**The enterprise should standardize the planning contract and governance requirements, not require every AI coding platform to expose an identical planning interface.**

This makes Planning Mode a durable part of Enterprise AI Engineering rather than a feature tied to one vendor.

Chapter 27 status: In progress — next section: Best Practices

## **11. Best Practices**

Planning Mode delivers the greatest value when it is treated as a disciplined engineering activity rather than a mechanism for generating lengthy implementation descriptions.

A useful Plan must be grounded in repository evidence, respect established Instructions, establish an executable sequence, expose uncertainty, and define how success will be verified.

In enterprise environments, planning quality is determined not by the amount of documentation produced but by the quality of the decisions it enables.

The following practices establish a reliable foundation for Planning Mode in Claude Code and other AI coding platforms.

### **11.1 Plan According to Task Complexity and Risk**

Planning effort should be proportional to the complexity, uncertainty, and potential impact of the requested change.

Not every development task requires a comprehensive architectural analysis.

A localized correction to an error message may require only inspection of the affected component and its associated tests.

A change to authorization, distributed transactions, database consistency, or public API contracts requires considerably more investigation.

Organizations should avoid applying the same planning requirements to every task regardless of risk.

A practical classification is:

| Task category                 | Planning approach                                 | Typical approval                              |
| ----------------------------- | ------------------------------------------------- | --------------------------------------------- |
| Trivial change                | Minimal investigation and focused validation      | Existing delegated authority                  |
| Localized enhancement         | Lightweight Plan identifying scope and tests      | Team policy                                   |
| Cross-component change        | Structured Plan with dependencies and risks       | Technical lead where required                 |
| Architectural change          | Formal Plan with alternatives and impact analysis | Architect                                     |
| Security-sensitive change     | Formal Plan with security analysis                | Authorized security reviewer                  |
| High-impact enterprise change | Formal Plan with technical and operational review | Relevant business and engineering authorities |

For Alpha Car Detailing, adding corporate fleet booking requires formal planning because the feature affects business invariants, concurrency, persistence, authorization, and event consistency.

The goal is proportionate control, not maximum documentation.

### **11.2 Establish the Repository Baseline Before Planning**

A Plan must be associated with the repository state used to create it.

Without a known baseline, the planning agent cannot reliably determine whether its findings remain valid when execution begins.

The baseline should include the inspected commit and the status of relevant uncommitted changes.

Where appropriate, the Harness should create an immutable or isolated repository snapshot for planning.

For example:

```
git rev-parse HEAD
git status --short
```

The planner should record the resulting revision identifier in the Plan.

When execution begins, the Harness should determine whether changes to the repository invalidate material assumptions.

This is particularly important when multiple developers or AI Agents work on the same application simultaneously.

A Plan based on an outdated booking service implementation may prescribe changes that conflict with recently merged work.

**Best practice:** Treat repository freshness as a planning prerequisite rather than assuming the codebase remains unchanged between planning and execution.

### **11.3 Perform Repository Discovery Before Selecting the Implementation Approach**

The planning agent should investigate existing implementation patterns before designing new components.

This requires more than searching for filenames matching the requested feature.

The agent should understand relevant application flows, dependencies, data boundaries, contracts, and existing extension points.

For corporate fleet booking, the agent should determine how the application currently creates reservations, enforces business policies, authorizes corporate accounts, and persists booking events.

It should also identify reusable mechanisms for idempotency and transaction management.

A Plan that proposes building a new idempotency subsystem without checking for existing support is not sufficiently grounded.

The investigation should be targeted rather than exhaustive.

The agent does not need to read every file in the repository. It should follow dependencies relevant to the task and expand the investigation when evidence indicates additional impact.

**Best practice:** Prefer targeted, evidence-driven discovery over broad repository exploration without a defined purpose.

### **11.4 Resolve Applicable Instructions Before Planning Implementation Tasks**

Instructions establish engineering constraints that the Plan must respect.

These may be defined through `CLAUDE.md`, `AGENTS.md`, platform-specific instruction files, or managed organizational controls.

The agent should identify applicable Instructions before proposing architectural or implementation decisions.

For example, Alpha Car Detailing may require Clean Architecture dependency boundaries, application-layer business logic, structured logging, and defined test coverage.

If those requirements already exist, the Plan should incorporate them.

The planning agent should not treat Instructions as optional recommendations.

Where multiple Instructions apply, their scope and precedence must be considered.

Where Instructions conflict, the planner should expose the conflict and seek an authorized decision rather than silently selecting whichever requirement is easiest to implement.

**Best practice:** Make instruction compliance an explicit planning condition and retain relevant references in the Plan.

### **11.5 Distinguish Confirmed Evidence from Assumptions**

One of the most important practices in AI-assisted planning is separating facts from hypotheses.

An agent may infer likely architecture from project structure, naming conventions, or familiar implementation patterns.

Those inferences can help direct investigation, but they must not be represented as confirmed findings.

A Plan should clearly classify material statements.

| Classification     | Meaning                                                          | Example                                                                |
| ------------------ | ---------------------------------------------------------------- | ---------------------------------------------------------------------- |
| Confirmed evidence | Supported by inspected repository or authoritative documentation | The inspected booking workflow uses a specific transaction abstraction |
| Approved decision  | Accepted by an authorized decision-maker                         | Corporate limits apply across all stations                             |
| Assumption         | Believed necessary but not yet confirmed                         | Cancelled bookings release capacity immediately                        |
| Open question      | Requires additional investigation or decision                    | How are expired reservations counted?                                  |
| Proposed approach  | Recommended implementation requiring appropriate authorization   | Enforce capacity using an existing concurrency mechanism               |

The distinction enables reviewers to identify which aspects of the Plan are established and which remain uncertain.

For Alpha Car Detailing, an incorrect assumption about corporate booking-limit semantics could invalidate the entire capacity enforcement design.

**Best practice:** Never convert an unverified assumption into an implementation requirement without the necessary confirmation.

### **11.6 Identify Both In-Scope and Out-of-Scope Work**

A Plan should establish explicit modification boundaries.

In-scope work describes the components and behaviors the agent is authorized to change.

Out-of-scope work identifies changes that must not be introduced as part of the task.

This prevents implementation from expanding into unrelated refactoring, architectural replacement, or infrastructure migration.

For example, corporate fleet booking may require extending existing application services and persistence behavior.

It does not automatically justify introducing CQRS, replacing the messaging infrastructure, or restructuring unrelated booking capabilities.

Out-of-scope declarations are especially valuable when an agent identifies opportunities for improvement during repository discovery.

Those improvements may be legitimate, but they should be raised separately instead of silently incorporated.

**Best practice:** Make scope boundaries explicit enough that the Developer Agent and Reviewer Agent can recognize unauthorized change expansion.

### **11.7 Decompose Work into Verifiable Engineering Tasks**

Implementation tasks should be actionable and associated with clear completion conditions.

Avoid vague steps such as:

```
1. Update the backend.
2. Implement business logic.
3. Add tests.
4. Verify everything.
```

Such tasks provide little guidance for sequencing or validation.

A more useful decomposition identifies the actual engineering outcomes.

For example:

```
1. Confirm corporate booking-limit semantics.
2. Define or extend the fleet booking API contract.
3. Integrate the booking workflow with corporate policy.
4. Enforce booking capacity atomically.
5. Integrate the existing idempotency mechanism.
6. Persist booking and outbox records consistently.
7. Implement authorization and compatibility tests.
8. Execute mandatory deterministic validation gates.
9. Submit implementation evidence for review and evaluation.
```

The Plan should also identify dependencies between tasks.

A capacity enforcement design cannot be finalized until the booking-limit semantics are understood.

An integration test cannot provide meaningful concurrency evidence until the transaction behavior is implemented.

**Best practice:** Define tasks in terms of deliverables, dependencies, and verification obligations rather than arbitrary coding activities.

### **11.8 Design Validation Before Implementation**

Validation should be planned alongside the implementation approach, not added after code generation.

For every significant acceptance criterion, the Plan should identify how correctness will be established.

A corporate booking feature may require unit tests for business policy, integration tests for transactional persistence, concurrency tests for booking limits, and contract tests for API compatibility.

Each technique addresses a different failure mode.

For example, unit tests of a booking-limit calculation cannot prove that simultaneous database transactions preserve the same invariant.

Similarly, a successful build cannot establish that retry behavior is idempotent.

The validation strategy should distinguish between deterministic engineering checks and broader business evaluation.

The Validator Agent verifies objective evidence.

The Evaluator Agent assesses whether the implementation satisfies the intended outcome and acceptance expectations.

**Best practice:** Do not approve a consequential implementation Plan that lacks a credible strategy for verifying its most important invariants.

### **11.9 Define Negative, Boundary, and Concurrency Scenarios**

A Plan should not focus exclusively on successful user journeys.

Enterprise failures often occur at boundaries: invalid requests, exhausted capacity, expired credentials, duplicate submissions, partial failures, and competing transactions.

For corporate fleet booking, the planned test scenarios should include:

- A valid booking within the corporate limit.
- A request exceeding the limit.
- A request containing invalid or unauthorized vehicles.
- Two concurrent requests competing for remaining capacity.
- A repeated request with the same idempotency key.
- Reuse of an idempotency key with a conflicting payload.
- Transaction failure before commit.
- Event delivery retry.
- Cancellation or expiration behavior, according to confirmed business policy.

These scenarios should be linked to the applicable acceptance criteria and system invariants.

**Best practice:** Plan tests for how the system must behave when conditions are unfavorable, not only when everything succeeds.

### **11.10 Make Dependencies and Blocking Decisions Visible**

A dependency should be recorded when it influences implementation order, technical feasibility, or authorization.

This includes dependencies on other services, shared contracts, infrastructure capabilities, and decisions from business owners or architects.

For example, a capacity enforcement design may depend on whether corporate limits are managed centrally or by individual stations.

If the answer is unknown, the Plan should indicate which implementation tasks are blocked.

The agent may continue non-dependent work, such as reviewing the existing API or examining test patterns, when authorized.

However, it should not make an irreversible design choice based on an unresolved business rule.

**Best practice:** Identify blocking decisions explicitly and distinguish them from questions that can safely be resolved during execution.

### **11.11 Keep the Plan Concise but Executable**

Overly detailed Plans can become difficult to review and maintain.

A Plan should contain enough information to support correct execution without prescribing unnecessary implementation mechanics.

For example, it may specify that corporate capacity enforcement must be atomic and must use the approved persistence architecture.

It does not necessarily need to prescribe every class name, private helper method, or line of code.

The appropriate level of detail depends on the risk of the decision.

Architecturally significant choices should be explicit.

Routine implementation details can remain delegated to the Developer Agent.

An effective Plan should allow a competent engineer or AI Agent to understand the intended approach without requiring the original planner to reconstruct missing decisions.

**Best practice:** Optimize for clarity, executability, and reviewability rather than document length.

### **11.12 Keep Planning Artifacts Separate from Scratchpad Observations**

The Plan and Scratchpad must have distinct purposes.

The Plan records the intended approach, approved decisions, implementation sequence, and validation obligations.

The Scratchpad records temporary observations, intermediate findings, experiments, and unresolved execution details.

During implementation, the Developer Agent may discover useful information that does not immediately require a Plan revision.

For example, it might record a failing test fixture, a temporary diagnostic command, or an intermediate hypothesis about a database deadlock.

Those observations belong in the run workspace.

If the finding changes an approved architectural decision, it should be promoted through a controlled Plan revision.

The agent should not continuously rewrite the approved Plan to include every intermediate observation.

**Best practice:** Preserve the Plan as the implementation contract and the Scratchpad as temporary working state.

### **11.13 Define Human Approval Boundaries Before Execution**

Human approval should be determined by risk and organizational policy.

The planning process must identify which decisions require authorization and who is empowered to provide it.

For corporate fleet booking, approval may be necessary for a changed database consistency strategy, public API breaking changes, or modifications to corporate authorization behavior.

Approval should be associated with a specific Plan revision.

If a material change is introduced after approval, the original authorization may no longer be sufficient.

The Harness should enforce the transition between planning and execution.

An agent should not infer approval merely because a human reviewed the Plan or because the planning interface allows implementation to begin.

**Best practice:** Treat approval as an explicit, recorded authorization event rather than conversational acknowledgment.

### **11.14 Detect and Manage Plan Drift**

The implementation may reveal information that was unavailable during planning.

An effective workflow must support controlled adaptation.

The Developer Agent should compare actual work against the approved Plan and identify material deviations.

Examples include introducing an additional external service, changing a database transaction boundary, expanding modifications into another bounded context, or weakening an acceptance criterion.

Minor internal implementation adjustments may remain within approved discretion.

Material deviations require impact assessment and, where necessary, revised approval.

A practical drift assessment should answer:

| Question                                      | Purpose                        |
| --------------------------------------------- | ------------------------------ |
| What differs from the approved Plan?          | Identify the deviation         |
| What evidence caused the change?              | Establish justification        |
| Does the change affect scope or architecture? | Determine materiality          |
| Are acceptance criteria affected?             | Protect the business objective |
| Must validation requirements change?          | Preserve verification coverage |
| Is new approval required?                     | Enforce governance             |

**Best practice:** Treat the initial Plan as revisable, but require controlled revision for material changes.

### **11.15 Enforce Planning as a Non-Mutating Phase**

Planning should not modify production code, configuration, database schemas, or infrastructure.

This boundary should be enforced technically where possible.

A natural-language instruction to avoid modifications is useful but insufficient for high-impact enterprise workflows.

The Harness should restrict tools, apply least-privilege permissions, and use isolated or read-only workspaces where appropriate.

Particular care is needed when planning agents can call external tools.

A command may appear to retrieve information while executing scripts or contacting services that create side effects.

The organization should define which discovery operations are permitted during planning.

If a proposed investigation requires a mutating action, that operation should be moved into a separately authorized phase.

**Best practice:** Enforce the distinction between planning and execution through permissions, not merely through the agent's stated intentions.

### **11.16 Preserve Plan Provenance and Version History**

A Plan should be traceable to its originating task and repository baseline.

The Harness should record sufficient metadata to explain how the Plan was created and approved.

Useful metadata includes:

| Metadata            | Purpose                                      |
| ------------------- | -------------------------------------------- |
| Task identifier     | Links the Plan to the business request       |
| Plan revision       | Identifies the approved version              |
| Repository baseline | Establishes inspected code state             |
| Planning agent      | Identifies the producing role or agent       |
| Evidence references | Supports technical findings                  |
| Approval record     | Identifies authorized decisions              |
| Revision history    | Explains material Plan changes               |
| Execution handoff   | Identifies the implementation starting point |

This information supports review, troubleshooting, governance, and reproducibility.

For example, if a later defect is traced to a booking-limit assumption, the organization should be able to determine whether that assumption was documented, approved, or introduced during execution.

**Best practice:** Maintain traceability from the original requirement through planning decisions, implementation changes, validation evidence, and acceptance.

### **11.17 Validate Plan Quality Before Approval**

A Plan should pass a basic quality assessment before being submitted for architectural or business approval.

Some checks can be deterministic.

For example, the Harness can verify that required fields exist, that the Plan references a valid task identifier, and that blocking assumptions are declared.

Other checks require engineering judgment.

A mechanically complete Plan may still contain incorrect architectural reasoning.

A useful quality checklist is:

| Quality condition                  | Expected result                         |
| ---------------------------------- | --------------------------------------- |
| Goal is defined                    | Required outcome is clear               |
| Repository baseline is recorded    | Planning context is traceable           |
| Evidence is referenced             | Important findings are supported        |
| Instructions are respected         | No unresolved unauthorized conflict     |
| Scope is bounded                   | Included and excluded work are clear    |
| Tasks are executable               | Implementation sequence is actionable   |
| Dependencies are identified        | Prerequisites are visible               |
| Risks are addressed                | Material failure modes have mitigations |
| Assumptions are explicit           | Uncertainty is not concealed            |
| Acceptance criteria are measurable | Business outcomes can be evaluated      |
| Validation is defined              | Mandatory checks are identifiable       |
| Approval requirements are recorded | Execution authority is clear            |

The Plan Quality Gate should not claim architectural correctness solely because these fields are present.

**Best practice:** Combine deterministic completeness checks with substantive engineering review.

### **11.18 Design Plans for Multi-Agent Handoff**

An enterprise Harness may use different agents for planning, implementation, review, validation, and evaluation.

The Plan must therefore communicate engineering intent independently of the agent that produced it.

A Developer Agent should receive enough context to begin implementation without reconstructing all important decisions.

The handoff should include the approved Plan, repository baseline, relevant Instructions, applicable Skills, unresolved non-blocking assumptions, and validation requirements.

The Reviewer Agent should be able to compare implementation changes against the Plan.

The Validator Agent should be able to identify required deterministic checks.

The Evaluator Agent should be able to compare delivered behavior against the original acceptance criteria.

This requires traceability between requirements, planned tasks, implementation artifacts, and evidence.

**Best practice:** Write Plans as durable engineering handoff artifacts, not as conversational notes understood only by the originating agent.

### **11.19 Avoid Treating Planning as Completion**

Planning completion and task completion are separate states.

An approved Plan authorizes an implementation approach.

It does not demonstrate that the feature exists.

It does not establish that code compiles, tests pass, architectural requirements are satisfied, or business acceptance criteria have been met.

The Harness should report these states distinctly.

For example:

```
Planning Status:
Approved

Implementation Status:
Not started

Validation Status:
Not executed

Evaluation Status:
Not started

Overall Task Status:
Ready for execution
```

The status should accurately reflect completed work.

An agent must not claim that corporate fleet booking has been delivered merely because the Plan describes how to implement it.

**Best practice:** Preserve explicit lifecycle states for planning, execution, validation, evaluation, and final acceptance.

### **11.20 Use Planning Outcomes to Improve Engineering Practices**

Completed planning activities can provide valuable information about recurring engineering difficulties.

For example, the Alpha Car Detailing team may repeatedly encounter uncertainty about transaction boundaries, idempotency mechanisms, or API versioning conventions.

These patterns may indicate missing Instructions, incomplete Knowledge Sources, or opportunities for reusable Skills.

The Harness can record such findings and recommend improvements.

However, planning observations should not automatically become permanent repository knowledge or new engineering standards.

The organization should review and approve proposed changes through the appropriate governance process.

This is consistent with the self-learning Harness philosophy introduced earlier in the handbook.

The Harness may identify improvement opportunities, but it must not silently change authoritative Instructions or architectural policies.

**Best practice:** Use recurring planning findings as evidence for governed improvement, not as permission for uncontrolled standard evolution.

### **11.21 Recommended Planning Review Checklist**

Before authorizing implementation, an architect or technical lead should be able to answer the following questions:

| Review question                             | Required outcome                                       |
| ------------------------------------------- | ------------------------------------------------------ |
| Is the business goal clearly understood?    | The Plan expresses the intended outcome                |
| Has the current repository been inspected?  | Findings reference real implementation evidence        |
| Are applicable Instructions identified?     | The approach complies with established constraints     |
| Are relevant Skills selected appropriately? | Existing workflows are reused where suitable           |
| Is implementation scope explicit?           | Unauthorized change expansion can be identified        |
| Are material dependencies understood?       | Blocking prerequisites have been addressed             |
| Are architectural decisions justified?      | Alternatives and tradeoffs are sufficiently explained  |
| Are assumptions visible?                    | Uncertainty is not presented as fact                   |
| Are acceptance criteria testable?           | Required behavior is observable                        |
| Is the validation strategy credible?        | Critical invariants have suitable verification methods |
| Are approval requirements satisfied?        | Execution is authorized                                |
| Is the Plan current?                        | Repository changes have not invalidated the approach   |
| Can the Developer Agent execute it?         | The handoff is complete enough for implementation      |
| Can reviewers assess compliance?            | Implementation obligations are traceable               |

If material conditions remain unresolved, the Plan should be revised or marked as blocked.

A partially complete Plan may still be useful, but it should not be represented as ready for execution when essential decisions are missing.

### **11.22 Applying the Practices to Alpha Car Detailing**

For the corporate fleet booking feature, these practices produce an identifiable engineering outcome before implementation begins.

The team has confirmed the applicable corporate booking-limit policy.

The planning agent has inspected the existing booking architecture and identified reusable components.

The Plan defines the required changes without introducing unnecessary architectural patterns.

Dependencies and risks are visible.

The concurrency strategy is justified in relation to the current persistence model.

Idempotency and transactional outbox requirements are addressed independently.

Acceptance criteria are mapped to deterministic validation evidence.

Material decisions have received the required approval.

The Developer Agent can begin implementation with a clear understanding of the approved approach and its boundaries.

The resulting process does not eliminate engineering uncertainty.

It ensures that significant uncertainty is managed deliberately rather than hidden inside generated code.

> **Enterprise Tip**
>
> Standardize a lightweight Plan review checklist across repositories, but allow planning depth to vary with task risk. A uniform planning contract is more valuable than requiring every change to produce an equally long document.
>
> Where possible, have the Harness verify Plan completeness and approval status automatically, while reserving architectural judgment and business decisions for appropriately authorized reviewers.

The central best practice is to make planning proportional, evidence-based, executable, verifiable, and governed.

A successful Planning Mode does not attempt to predict every implementation detail.

It establishes enough understanding and control for implementation to proceed responsibly.

Chapter 27 status: In progress — next section: Anti-patterns

## **12. Anti-patterns**

Planning Mode introduces a deliberate boundary between understanding a task and executing it. That boundary is valuable only when the planning process produces reliable engineering decisions and those decisions influence subsequent implementation.

Poor planning practices can undermine this objective. Some encourage premature implementation, while others introduce unnecessary ceremony, conceal uncertainty, or create documents that cannot be executed or verified.

In enterprise AI-assisted development, planning failures are particularly consequential because an AI Agent can rapidly generate substantial implementation changes based on an incorrect assumption.

The following anti-patterns illustrate common failures, their consequences, and the engineering practices needed to prevent them.

### **12.1 Coding Before Understanding Scope**

One of the most common anti-patterns occurs when an AI Agent begins modifying the repository immediately after receiving a Prompt.

The agent recognizes familiar technical terms, identifies a likely implementation location, and starts generating code before establishing the full scope of the requirement.

For Alpha Car Detailing, the request to introduce corporate fleet booking may appear to require a new API endpoint and application service.

An agent might immediately create a controller, add a request model, and implement a booking operation.

However, it may not have established whether corporate booking limits apply per station, per account, or across a contractual period.

It may also overlook existing idempotency or transactional outbox capabilities.

The result can be a technically plausible implementation built on an incomplete understanding of the system.

**Why it fails**

Premature implementation allows initial assumptions to become embedded in source code before they have been validated.

Once code exists, subsequent decisions may be biased toward preserving the initial design rather than selecting the most appropriate architecture.

The agent may spend additional time adapting surrounding components to accommodate an incorrect approach.

**Corrective practice**

Require sufficient repository discovery and scope identification before implementation begins.

For consequential changes, the Plan should explicitly identify affected components, required business decisions, architectural constraints, and validation obligations.

> **Common Mistake:** Treating rapid code generation as evidence of engineering progress. Code created before the problem is sufficiently understood may increase rework rather than reduce delivery time.

### **12.2 Over-planning Trivial Changes**

The opposite failure occurs when an agent applies extensive planning procedures to every task regardless of complexity.

A simple correction to a localized error message may trigger repository-wide investigation, architectural analysis, dependency mapping, risk assessment, and multiple approval steps.

Although these activities may appear disciplined, they can create unnecessary friction.

Planning has a cost.

The cost includes analysis time, context consumption, artifact maintenance, and human review effort.

When those costs exceed the plausible risk of the change, formal planning becomes counterproductive.

**Why it fails**

Excessive planning reduces development efficiency without providing proportionate engineering value.

It can also encourage teams to bypass the planning process because the process is perceived as burdensome.

**Corrective practice**

Apply risk-based planning thresholds.

Use lightweight planning for isolated changes with clear requirements and straightforward verification.

Reserve formal planning and approval for tasks involving significant uncertainty, architectural impact, security boundaries, distributed consistency, or operational risk.

Planning depth should be proportional to the consequences of an incorrect implementation.

### **12.3 Producing a Plan Without Repository Evidence**

A planning agent may generate a convincing implementation approach based on familiar architecture patterns without examining the actual repository.

For example, it might assume that Alpha Car Detailing uses a command-handler architecture, a particular repository abstraction, or a dedicated corporate booking microservice.

Those assumptions may resemble common enterprise designs, but they are not evidence of the application's actual implementation.

The Plan may recommend introducing components that already exist or rely on abstractions that the repository does not provide.

**Why it fails**

A Plan based on imagined architecture cannot reliably guide implementation.

It may create duplicated capabilities, violate dependency boundaries, or require unnecessary refactoring.

**Corrective practice**

Require material architectural claims to reference actual repository evidence.

The Plan should identify the inspected files, interfaces, contracts, tests, or approved design documents supporting important decisions.

Where evidence cannot be obtained, the agent should record the limitation instead of inventing a plausible answer.

A technically sophisticated Plan is not necessarily an evidence-based Plan.

### **12.4 Ignoring Applicable Instructions**

An AI Agent may identify an implementation approach that appears efficient but conflicts with established repository Instructions.

For example, the Plan may propose directly accessing SQL Server from an ASP.NET Core controller even though Alpha Car Detailing requires Clean Architecture dependency boundaries.

Another Plan might recommend introducing an external package that conflicts with approved dependency policies.

These decisions may be technically feasible while still violating the organization's architecture.

**Why it fails**

The planning process becomes disconnected from established engineering governance.

Subsequent code generation can produce implementations that pass functional tests but fail architecture reviews or compliance requirements.

**Corrective practice**

Resolve applicable Instructions before selecting the implementation approach.

The Plan should identify relevant architectural, security, testing, and operational requirements.

When Instructions conflict or are unclear, the agent must expose the conflict and seek authorized resolution.

Planning Mode cannot override Instructions simply by documenting a different preference.

### **12.5 Creating Plans Without a Validation Strategy**

A Plan may describe implementation tasks in considerable detail while providing little explanation of how correctness will be established.

For example:

```
1. Add corporate fleet booking endpoint.
2. Implement corporate booking service.
3. Update database.
4. Add unit tests.
5. Complete implementation.
```

This sequence does not define the expected behavior or the evidence required to demonstrate that the feature works correctly.

For corporate fleet booking, it omits critical questions about concurrency, idempotency, authorization, and transactional consistency.

**Why it fails**

Validation becomes an afterthought.

Tests may be written to confirm the implementation that was produced rather than to verify the original business requirements.

The agent may report success after compilation and a small number of positive tests, even though important invariants remain unverified.

**Corrective practice**

Establish acceptance criteria and validation requirements during planning.

Map significant requirements to appropriate deterministic checks.

For example, atomic booking-limit enforcement requires concurrency testing against persistence behavior representative of production.

A successful unit test of a capacity calculation is not sufficient evidence.

> **Architect's Note:** A Plan without a credible validation strategy is an implementation proposal, not a complete engineering Plan.

### **12.6 Ignoring Dependencies and Implementation Order**

A weak Plan may identify the correct tasks but place them in an arbitrary sequence.

For example, it may instruct the Developer Agent to implement corporate capacity enforcement before confirming the booking-limit policy.

Alternatively, it may schedule event integration before the relevant transaction boundary has been established.

**Why it fails**

The Developer Agent may implement components based on unresolved decisions.

When those decisions are clarified later, already-completed work may need significant revision.

Unrecognized dependencies can also cause agents to work on incompatible assumptions during parallel execution.

**Corrective practice**

Represent important dependencies explicitly.

Identify hard prerequisites, independent activities, and external decisions.

Implementation tasks should follow the actual dependency structure of the system.

Where parallel work is appropriate, the Plan should define stable contracts and integration boundaries.

Parallelism should not be introduced at the expense of architectural consistency.

### **12.7 Hiding Assumptions Inside the Plan**

An AI Agent may present inferred behavior as if it were a confirmed business requirement.

For example:

> Corporate customers can maintain a maximum of 100 active reservations across all stations.

If the figure and scope were not supplied by an authoritative source, this statement is an assumption.

Embedding it directly in the implementation tasks conceals uncertainty.

**Why it fails**

Reviewers may approve the Plan without realizing that a material business rule was invented.

The resulting implementation can behave consistently with the Plan while violating the actual business contract.

**Corrective practice**

Separate confirmed facts, assumptions, open questions, and approved decisions.

For every material assumption, identify its impact and resolution mechanism.

When an assumption affects data integrity, authorization, contractual obligations, or transaction design, obtain appropriate confirmation before proceeding.

Planning quality depends partly on how clearly uncertainty is exposed.

### **12.8 Mixing Scratchpad Observations into the Approved Plan**

During implementation, an agent accumulates temporary observations.

These may include diagnostic output, failed experiments, provisional hypotheses, test results, and intermediate technical questions.

An anti-pattern occurs when the agent continuously appends this material to the approved Plan without distinguishing observations from decisions.

The Plan gradually becomes a chronological execution log.

**Why it fails**

The intended implementation approach becomes difficult to identify.

Reviewers cannot easily distinguish approved decisions from temporary findings.

A downstream agent may treat an unverified observation as authoritative architectural guidance.

**Corrective practice**

Maintain separate Plan and Scratchpad responsibilities.

Use the Scratchpad for temporary working state.

Use execution logs for implementation activity and test evidence.

Revise the Plan when a material finding changes the approved approach.

Such revisions should be explicit, traceable, and approved where required.

### **12.9 Treating the Plan as Authoritative Repository Knowledge**

A task-specific Plan may contain decisions that are correct for one implementation but inappropriate as permanent architectural standards.

For example, a corporate fleet booking Plan may select a particular transaction mechanism because of the current booking schema.

Another agent might later treat that decision as a universal persistence standard for every Alpha Car Detailing service.

**Why it fails**

Task-specific decisions are promoted into repository-wide authority without proper review.

This can create architectural inconsistency and cause future agents to apply techniques outside their intended context.

**Corrective practice**

Treat the Plan as an implementation artifact scoped to a task and repository baseline.

Permanent architectural decisions should be recorded through the organization's approved Knowledge Sources, Architecture Decision Records, or Instructions.

Promotion from task-specific findings to authoritative repository knowledge must be governed.

A Plan may inform future decisions, but it does not automatically establish enterprise standards.

### **12.10 Treating the Initial Plan as Immutable**

Some workflows assume that an approved Plan must be followed exactly even when execution reveals new evidence.

For example, the Developer Agent may discover that the existing booking persistence abstraction cannot enforce the required capacity invariant.

Continuing with the original approach simply because it was approved would preserve procedural compliance at the expense of technical correctness.

**Why it fails**

The Plan becomes detached from repository reality.

The agent may introduce fragile workarounds or weaken acceptance criteria to avoid requesting a revision.

**Corrective practice**

Allow controlled re-planning.

When material evidence invalidates an approved assumption or design decision, the Developer Agent should report the finding and assess its impact.

The Lead Agent should coordinate a revised Plan.

Required reviewers should approve material changes before execution resumes.

An approved Plan is a controlled engineering agreement, not an immutable prediction.

### **12.11 Allowing Uncontrolled Plan Drift**

The opposite anti-pattern occurs when the Developer Agent changes the implementation approach without recording or authorizing the deviation.

For example, the approved corporate booking Plan may require reuse of the existing transactional outbox.

During implementation, the agent might introduce direct message publication because it appears simpler.

The resulting implementation may still pass basic functional tests while weakening transactional consistency.

**Why it fails**

The implementation no longer corresponds to the approved architecture.

Reviewers may evaluate the change against an outdated Plan.

Audit records may incorrectly suggest that the actual implementation received approval.

**Corrective practice**

Define material drift criteria.

Changes affecting architecture, security, data consistency, public contracts, infrastructure dependencies, or business acceptance criteria should trigger impact assessment.

Minor implementation adjustments may remain within delegated authority.

Material deviations require controlled Plan revision and appropriate reapproval.

### **12.12 Creating Plans That Cannot Be Executed**

A Plan may contain broad recommendations without actionable engineering tasks.

For example:

```
Improve the corporate booking architecture.

Ensure scalability and reliability.

Follow enterprise best practices.

Add appropriate validation.

Deploy the completed solution.
```

These statements express desirable outcomes but provide no executable implementation approach.

They do not identify affected components, dependencies, decisions, or completion criteria.

**Why it fails**

The Developer Agent must reconstruct the actual design during execution.

Different agents may interpret the Plan differently, producing inconsistent implementation results.

**Corrective practice**

Define tasks with identifiable deliverables, prerequisites, scope boundaries, and verification obligations.

A competent engineer or AI Agent should be able to use the Plan without needing the original planner to explain its essential decisions.

The Plan should be sufficiently specific for execution while avoiding unnecessary code-level prescription.

### **12.13 Creating Plans That Cannot Be Verified**

A Plan may contain implementation steps but define acceptance in subjective terms.

For example:

> The corporate fleet booking solution should be scalable, robust, maintainable, and enterprise-ready.

These qualities are relevant, but the statement does not identify how they will be assessed.

**Why it fails**

The Validator Agent cannot determine pass or fail from an undefined quality expectation.

The Evaluator Agent may also lack agreed criteria for judging whether the business outcome is satisfactory.

**Corrective practice**

Translate important quality expectations into measurable conditions or explicitly defined review criteria.

For corporate fleet booking, this may include preserving the capacity invariant under concurrent requests, preventing duplicate booking creation during retries, and maintaining API compatibility.

Where non-functional requirements such as latency or throughput matter, establish workload assumptions and approved thresholds.

Do not invent performance targets without evidence of business or operational requirements.

### **12.14 Using Planning Mode to Make Unauthorized Changes**

An agent may interpret planning as permission to perform preparatory implementation work.

For example, it might create database migrations, update project files, install dependencies, or modify test code while claiming that these actions are necessary to evaluate the proposed approach.

Such operations cross the planning boundary.

**Why it fails**

The distinction between investigation and execution becomes meaningless.

Source changes may occur before architectural review or approval.

Potentially unsafe operations can be hidden inside a phase that stakeholders believe is non-mutating.

**Corrective practice**

Use read-only repository access and restricted tools during formal planning.

If experimentation is necessary, place it in a separately authorized activity with appropriate isolation and evidence handling.

The Harness should enforce permissions rather than relying exclusively on the agent's natural-language commitment.

Planning Mode must not modify production code or configuration.

### **12.15 Skipping Human Approval Where Required**

A Plan may be complete, technically sound, and supported by repository evidence while still requiring human authorization.

An anti-pattern occurs when the agent treats successful planning as sufficient permission to begin execution.

For example, a Plan may propose changing the booking database schema or altering corporate account authorization behavior.

Those changes may require architectural or security approval under organizational policy.

**Why it fails**

The AI Agent assumes authority that belongs to designated human decision-makers.

The resulting work may violate change-management, compliance, or operational controls.

**Corrective practice**

Define approval requirements before implementation.

Associate approval with a specific Plan revision and repository baseline.

The Harness should block execution when mandatory approval is absent.

An interactive tool action that begins implementation should not be treated as equivalent to organizational approval.

### **12.16 Confusing Plan Completion with Feature Completion**

A planning agent may produce a detailed implementation document and report the overall engineering task as complete.

For example:

```
Corporate fleet booking completed successfully.

Implementation plan generated.

All requirements addressed.
```

This statement is misleading if no implementation has occurred.

**Why it fails**

Planning output is confused with delivered software.

Dashboards, task records, or management reports may incorrectly indicate that the capability is available.

Downstream teams may act on an inaccurate status.

**Corrective practice**

Maintain explicit lifecycle states.

```
Planning:
Complete

Approval:
Approved

Implementation:
Not started

Validation:
Not executed

Evaluation:
Not started

Overall Task:
Ready for execution
```

Planning completion establishes readiness for implementation.

Feature completion requires implementation, required validation, evaluation, and applicable acceptance decisions.

### **12.17 Excessive Planning Detail Without Decision Value**

An agent may generate a large Plan containing extensive descriptions of common architecture patterns, generic best practices, or speculative implementation details.

The document may appear comprehensive while making the actual engineering decisions difficult to locate.

For example, a corporate fleet booking Plan might contain several pages explaining REST principles but fail to specify how booking limits are enforced atomically.

**Why it fails**

Document volume obscures important decisions.

Reviewers spend time reading material that does not reduce implementation uncertainty.

The Plan becomes harder to maintain when changes occur.

**Corrective practice**

Prioritize decision-relevant content.

Explain architecture alternatives when they influence the chosen design.

Document dependencies, risks, assumptions, and validation requirements with sufficient detail.

Avoid repeating general engineering guidance already established in repository Instructions or Skills.

A concise, evidence-based Plan is often more useful than a long speculative document.

### **12.18 Failing to Revalidate the Plan Against Repository Changes**

A Plan may be approved against one repository revision while implementation begins against another.

For example, another team may change the booking transaction mechanism after the corporate fleet booking Plan has been approved.

If the Developer Agent begins execution without checking the new baseline, the implementation may rely on obsolete assumptions.

**Why it fails**

The Plan no longer describes the current system accurately.

The agent may introduce conflicting changes, duplicate recently implemented functionality, or break new integration contracts.

**Corrective practice**

Record the repository baseline during planning.

Before execution, compare the current repository state with the approved baseline.

Assess whether relevant changes affect architectural decisions, dependencies, or validation requirements.

Require re-planning when material assumptions are invalidated.

A change in commit identifier does not always require complete reapproval, but relevant changes must be evaluated.

### **12.19 Failing to Consider Operational and Security Risks**

A Plan may focus exclusively on functional implementation.

For example, the corporate fleet booking Plan might define request models and persistence operations while omitting authorization, observability, rollback behavior, and operational failure scenarios.

**Why it fails**

The implementation may satisfy the immediate functional requirement but introduce security or reliability problems.

In enterprise environments, operational behavior is part of the system's engineering contract.

**Corrective practice**

Include relevant non-functional concerns in planning.

For corporate fleet booking, the Plan should consider cross-account authorization, transaction failures, event-delivery retries, meaningful diagnostic logging, and safe handling of failed requests.

The depth of analysis should reflect the actual risks.

Not every change requires a complete threat model or operational redesign, but consequential risks should not be ignored.

### **12.20 Treating Agent-Generated Plans as Automatically Correct**

A detailed AI-generated Plan can create an impression of technical authority.

It may use precise terminology, identify plausible components, and present a convincing implementation sequence.

However, fluency does not establish correctness.

A Plan may contain incorrect assumptions, overlook architectural constraints, or recommend an unsafe concurrency strategy.

**Why it fails**

Reviewers may accept the Plan based on presentation quality rather than evidence.

The organization transfers engineering judgment to a probabilistic system without sufficient verification.

**Corrective practice**

Require evidence-based review of material decisions.

Architects should challenge transaction boundaries, failure handling, security assumptions, and validation adequacy.

The Plan Quality Gate may verify document completeness, but it cannot substitute for substantive architectural assessment.

The AI Agent proposes the approach. The organization's authorized engineering process determines whether the approach is acceptable.

### **12.21 Anti-pattern Summary**

The following table consolidates the principal planning failures and their corrective practices.

| Anti-pattern                          | Consequence                            | Corrective practice                    |
| ------------------------------------- | -------------------------------------- | -------------------------------------- |
| Coding before understanding scope     | Incorrect or incomplete implementation | Discover and define scope first        |
| Over-planning trivial changes         | Unnecessary overhead                   | Use risk-based planning                |
| Planning without repository evidence  | Imagined architecture                  | Reference verified repository findings |
| Ignoring Instructions                 | Architecture and policy violations     | Resolve applicable Instructions        |
| No validation strategy                | Unverifiable implementation            | Define evidence requirements upfront   |
| Ignoring dependencies                 | Rework and sequencing failures         | Model task prerequisites               |
| Hidden assumptions                    | Incorrect business behavior            | Classify and resolve uncertainty       |
| Mixing Scratchpad and Plan            | Loss of decision clarity               | Separate artifacts by responsibility   |
| Treating Plan as repository authority | Uncontrolled standard changes          | Govern knowledge promotion             |
| Immutable Plan                        | Inability to adapt to evidence         | Support controlled revisions           |
| Uncontrolled Plan drift               | Unauthorized architectural changes     | Enforce material-change assessment     |
| Non-executable Plan                   | Ambiguous implementation               | Define actionable tasks                |
| Non-verifiable Plan                   | Unclear success criteria               | Map acceptance to evidence             |
| Mutating during planning              | Approval boundary violation            | Enforce read-only permissions          |
| Skipping required approval            | Governance failure                     | Record explicit authorization          |
| Planning marked as completion         | Misleading delivery status             | Separate lifecycle states              |
| Excessive documentation               | Poor reviewability                     | Focus on engineering decisions         |
| Stale repository baseline             | Invalid implementation assumptions     | Check Plan freshness                   |
| Ignoring operational risks            | Reliability and security exposure      | Include relevant operational analysis  |
| Blind acceptance of AI Plans          | Unreviewed technical decisions         | Apply independent engineering judgment |

### **12.22 Applying Anti-pattern Awareness to the Harness**

In a mature Harness, avoiding anti-patterns should not depend entirely on individual agent behavior.

The orchestration architecture should establish controls that prevent or expose common planning failures.

Some controls can be deterministic.

For example, the Harness can verify that a Plan contains an identifier, a repository baseline, scope information, acceptance criteria, validation requirements, and approval status.

It can also restrict repository mutation during planning and prevent execution from starting when mandatory approval is absent.

Other controls require agent-based assessment or human judgment.

The Harness may use a reviewer to examine whether repository evidence supports the proposed architecture.

An architect may need to evaluate whether the selected transaction strategy is appropriate.

A business owner may need to confirm contractual booking-limit semantics.

These responsibilities should not be collapsed into one automated completeness check.

A practical planning-control model is:

\#chatgpt-mermaid-\_r_1c30\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c30\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c30\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c30\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c30\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c30\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c30\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c30\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c30\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c30\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c30\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c30\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c30\_ p{margin:0;}#chatgpt-mermaid-\_r_1c30\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1c30\_ .label text,#chatgpt-mermaid-\_r_1c30\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .node rect,#chatgpt-mermaid-\_r_1c30\_ .node circle,#chatgpt-mermaid-\_r_1c30\_ .node ellipse,#chatgpt-mermaid-\_r_1c30\_ .node polygon,#chatgpt-mermaid-\_r_1c30\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .rough-node .label text,#chatgpt-mermaid-\_r_1c30\_ .node .label text,#chatgpt-mermaid-\_r_1c30\_ .image-shape .label,#chatgpt-mermaid-\_r_1c30\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1c30\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .rough-node .label,#chatgpt-mermaid-\_r_1c30\_ .node .label,#chatgpt-mermaid-\_r_1c30\_ .image-shape .label,#chatgpt-mermaid-\_r_1c30\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1c30\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1c30\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c30\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c30\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1c30\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c30\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c30\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c30\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1c30\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1c30\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c30\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1c30\_ .icon-shape,#chatgpt-mermaid-\_r_1c30\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c30\_ .icon-shape p,#chatgpt-mermaid-\_r_1c30\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1c30\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1c30\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c30\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1c30\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1c30\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c30\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c30\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c30\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c30\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c30\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c30\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c30\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c30\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c30\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1c30\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1c30\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .node rect,#chatgpt-mermaid-\_r_1c30\_ .node circle,#chatgpt-mermaid-\_r_1c30\_ .node ellipse,#chatgpt-mermaid-\_r_1c30\_ .node polygon,#chatgpt-mermaid-\_r_1c30\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1c30\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1c30\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1c30\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1c30\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c30\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1c30\_ .decision rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .decision polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .decision ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .decision circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .decision path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .decision tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c30\_ .approved tspan{fill:rgb(33, 84, 53)!important;}Proposed PlanDeterministic CompletenessGateRequired Fields Present?Return for RevisionEvidence and ArchitectureReviewMaterial Issues?Approval Policy CheckApproval Satisfied?Await Authorized DecisionApproved PlanExecution HandoffNoYesYesNoNoYes

This model establishes multiple levels of assurance.

The deterministic gate checks structural requirements.

The evidence and architecture review assesses substantive quality.

The approval policy check verifies authorization.

Execution begins only after the applicable conditions are satisfied.

### **12.23 The Underlying Engineering Lesson**

The most damaging planning anti-pattern is not necessarily the absence of a Plan.

It is the creation of false confidence.

A poorly constructed Plan can make an implementation appear controlled even when material assumptions remain unresolved, important dependencies have been ignored, or validation requirements are missing.

For Alpha Car Detailing, a polished corporate fleet booking Plan that overlooks concurrent capacity enforcement may be more dangerous than an explicitly incomplete Plan that identifies the concurrency problem as unresolved.

The latter preserves uncertainty and invites engineering judgment.

The former conceals a potentially serious defect behind apparent completeness.

Planning Mode should therefore encourage an AI Agent to acknowledge limitations, expose unresolved questions, and request appropriate decisions.

> **Architect's Note**
>
> Evaluate planning quality by the engineering uncertainty it resolves, the risks it makes visible, and the verification obligations it establishes.
>
> Do not reward an AI Agent merely for producing a detailed Plan or for completing planning quickly. A useful Plan must remain executable, evidence-based, adaptable, and subject to the organization's approval and validation controls.

A reliable planning process does not eliminate every mistake.

It reduces the likelihood that avoidable mistakes become embedded in implementation before they are recognized.

**The purpose of Planning Mode is not to make an AI Agent appear prepared. It is to establish sufficient engineering understanding for responsible execution.**

Chapter 27 status: In progress — next section: Architect’s Notes

## **13. Architect’s Notes**

Planning Mode should be understood as an architectural control within the enterprise AI engineering lifecycle, not merely as a feature of an AI coding platform.

Claude Code, GitHub Copilot, and Codex provide mechanisms for investigating tasks before implementation. However, enterprise architects must determine how planning fits into the broader system of responsibilities, permissions, repository intelligence, validation, approval, and governance.

The architect's primary concern is not whether an agent can produce a detailed implementation Plan. It is whether the planning process consistently produces sound engineering decisions and whether those decisions remain traceable and enforceable throughout execution.

### **13.1 Planning Is an Architectural Control, Not Just an Agent Capability**

An AI Agent can be instructed to inspect a repository and produce a Plan. That capability alone does not establish a reliable planning architecture.

A complete enterprise planning capability requires defined inputs, controlled permissions, structured outputs, review conditions, and an authorized transition into implementation.

This distinction becomes important when multiple agents operate within the same Harness.

The Lead Agent may coordinate planning. The Developer Agent may execute the approved Plan. The Reviewer Agent may examine architectural alignment. The Validator Agent verifies deterministic evidence. The Evaluator Agent assesses the delivered outcome.

Planning must produce an artifact that these roles can consume consistently.

The architect should establish a planning contract defining the minimum requirements for any Plan, regardless of which AI coding platform creates it.

A practical contract includes:

| Planning contract element | Architectural requirement                                        |
| ------------------------- | ---------------------------------------------------------------- |
| Goal                      | Explicitly identify the intended business or engineering outcome |
| Repository baseline       | Record the code revision against which planning occurred         |
| Evidence                  | Reference verified repository findings                           |
| Instructions              | Identify applicable engineering constraints                      |
| Scope                     | Define permitted and excluded modifications                      |
| Dependencies              | Identify implementation prerequisites                            |
| Decisions                 | Explain significant architectural choices                        |
| Assumptions               | Make unresolved uncertainty visible                              |
| Acceptance criteria       | Define observable success conditions                             |
| Validation                | Identify required deterministic evidence                         |
| Approvals                 | Establish authorization requirements                             |
| Handoff                   | Define conditions under which execution may begin                |

The contract should remain independent of the planning agent's interface.

> **Architect's Note**
>
> Standardize what a valid engineering Plan must contain before standardizing how agents generate Plans. The enterprise should own the planning contract; the AI coding platform should implement it.

### **13.2 Separate Planning Authority from Execution Authority**

One of the most important architectural decisions is determining what authority an agent possesses during planning.

A planning agent needs sufficient access to understand the repository and identify dependencies.

It does not ordinarily need authority to modify source files, create infrastructure, change database schemas, or perform deployments.

The architect should establish separate permission boundaries for planning and execution.

This separation should be enforced by the Harness where possible, rather than depending entirely on natural-language Instructions.

A useful authority model is:

| Capability                        | Planning phase                     | Execution phase                               |
| --------------------------------- | ---------------------------------- | --------------------------------------------- |
| Read repository files             | Allowed within policy              | Allowed within policy                         |
| Inspect relevant architecture     | Allowed                            | Allowed                                       |
| Read applicable Instructions      | Required                           | Required                                      |
| Propose architectural changes     | Allowed                            | Allowed when new evidence requires escalation |
| Modify source code                | Not allowed                        | Allowed within authorized scope               |
| Modify configuration              | Not allowed                        | Allowed within authorized scope               |
| Change infrastructure             | Not allowed                        | Separately authorized                         |
| Run potentially mutating tools    | Not allowed                        | Subject to permissions                        |
| Request human approval            | Allowed                            | Allowed                                       |
| Grant its own approval            | Not allowed                        | Not allowed                                   |
| Change authoritative Instructions | Not allowed                        | Requires separate governance                  |
| Record planning evidence          | Allowed in designated plan storage | Allowed according to run policy               |

The Plan itself must never become a source of elevated permissions.

For example, a Plan that recommends modifying a production database does not authorize that operation.

The Harness must separately determine whether the agent has permission to perform the change.

This prevents architectural analysis from becoming an implicit mechanism for privilege escalation.

### **13.3 Treat the Plan as a Versioned Engineering Agreement**

An approved Plan should be treated as a versioned agreement about the intended implementation approach.

It is not the same as a legal contract, but it establishes an explicit reference against which implementation decisions can be evaluated.

For Alpha Car Detailing, a corporate fleet booking Plan may establish that capacity enforcement must occur atomically within a defined transaction boundary.

The Developer Agent is then expected to preserve that decision while implementing the feature.

If execution reveals that the selected approach cannot satisfy the required invariant, the agent should initiate a controlled revision.

A practical Plan identity may be represented as:

```
Task ID: ACD-FLEET-001
Plan ID: PLAN-ACD-FLEET-001
Plan Revision: 3
Repository Baseline: <commit-sha>
Approval Status: Approved
Execution Status: Ready
```

These values are illustrative.

The architectural requirement is that approval applies to an identifiable Plan revision and repository context.

Without versioning, a Plan may be modified after approval without making it clear whether the revised design was authorized.

> **Architect's Note**
>
> Approval should bind to a specific Plan revision and relevant repository baseline. An approval record that cannot identify what was approved provides weak governance evidence.

### **13.4 Architectural Decisions Require Rationale**

A Plan should not simply list the implementation technology selected by the AI Agent.

Material decisions should include sufficient rationale to explain why the approach is appropriate.

For corporate fleet booking, consider the requirement to prevent concurrent requests from exceeding contractual capacity.

Several implementation strategies may be technically possible, including optimistic concurrency, conditional database updates, or appropriately isolated transactions.

The correct approach depends on existing persistence capabilities, contention characteristics, transaction boundaries, and business semantics.

The Plan should explain:

1. The invariant that must be preserved.
2. The existing repository mechanisms relevant to that invariant.
3. The feasible approaches considered.
4. The selected approach and its rationale.
5. Relevant tradeoffs.
6. The evidence required to verify correctness.

The architect need not require an exhaustive comparison of every imaginable alternative.

The level of analysis should reflect the materiality of the decision.

A minor internal refactoring does not require the same architectural justification as a change to distributed transaction behavior.

### **13.5 Protect Business Invariants from Implementation Convenience**

An AI Agent may favor implementation approaches that are familiar, concise, or easy to generate.

These properties do not establish architectural correctness.

Business invariants must constrain implementation choices.

For Alpha Car Detailing, the corporate capacity invariant may be expressed as:

\\[ \text{ActiveReservedVehicles} \leq \text{AuthorizedCorporateCapacity} \\]

The implementation must preserve this condition according to the approved business semantics, including relevant concurrency and lifecycle behavior.

A simple count-then-insert sequence may appear correct under sequential testing but fail when concurrent requests compete for the same remaining capacity.

The architect should ensure that the Plan describes the guarantee required, rather than merely prescribing an apparently convenient implementation.

Similarly, idempotency must not be confused with capacity enforcement.

Idempotency prevents repeated processing of the same logical request under the defined key semantics.

Capacity enforcement prevents distinct valid requests from collectively exceeding the business limit.

These responsibilities may interact, but they solve different problems.

> **Architect's Note**
>
> Express critical business invariants before selecting implementation mechanisms. An AI-generated architecture should be evaluated by the guarantees it provides, not by how familiar or concise its code appears.

### **13.6 Plan the Consistency Boundary Before the Component Structure**

AI coding agents are often effective at proposing class structures, interfaces, controllers, and service methods.

However, component structure should follow the system's consistency requirements.

For corporate fleet booking, the architect must first determine which operations must succeed or fail together.

A successful booking may require reservation persistence, capacity consumption, an idempotency record, and a transactional outbox entry.

The Plan must establish which operations share the transaction boundary and which are handled asynchronously.

It should also consider failure and retry behavior.

For example, if a booking transaction commits but the client does not receive the response, a retry must not create an additional reservation.

If an event publisher fails after the booking commits, the event-delivery mechanism must preserve the required consistency guarantees.

These decisions should precede detailed class design.

A component diagram that looks clean but ignores transaction boundaries is not sufficient architecture.

### **13.7 Design Planning Around Existing Architecture, Not Idealized Architecture**

A planning agent may recommend a theoretically attractive architecture that requires substantial changes to the existing codebase.

For example, it may propose introducing CQRS, replacing persistence abstractions, or creating a new microservice to support corporate fleet booking.

Such changes may be appropriate in some systems, but they should not be introduced automatically.

The architect should distinguish between the architecture the organization intends to build over time and the architecture required for the current task.

Planning should begin with the existing system.

The agent must identify which capabilities can be reused and which limitations genuinely require change.

If a larger architectural improvement is warranted, it should be presented as a separate decision with its own scope, risks, migration strategy, and approval requirements.

This approach avoids converting every feature request into an uncontrolled modernization initiative.

### **13.8 Plan for Compatibility and Evolution**

Enterprise applications rarely operate in isolation.

A change to an API, database schema, event contract, or shared abstraction may affect consumers beyond the immediately modified component.

Planning must therefore consider compatibility.

For Alpha Car Detailing, introducing corporate fleet booking may influence scheduling, corporate reporting, notifications, and downstream event processing.

The architect should determine whether the new capability can be introduced without breaking existing individual booking behavior.

Potential considerations include API versioning, optional versus mandatory fields, event schema evolution, database migration compatibility, and deployment sequencing.

The Plan should identify the required compatibility checks.

Where a breaking change is unavoidable, it should be explicitly approved and accompanied by a migration approach.

The planning agent should not assume that modifying one repository means the impact is confined to that repository.

### **13.9 Design for Failure Before Designing for Success**

A Plan that describes only successful execution is incomplete for consequential enterprise changes.

Architecture must account for failures and recovery.

For corporate fleet booking, relevant scenarios include:

- A request fails after capacity validation but before commit.
- Two requests attempt to consume the final available capacity simultaneously.
- A client retries after a response timeout.
- The event publisher becomes temporarily unavailable.
- A downstream consumer receives the same event more than once.
- An integration dependency fails during booking.
- The application restarts while a request is being processed.

The Plan should identify which guarantees apply to each scenario and how the design satisfies them.

This does not mean every failure needs a unique implementation mechanism.

Existing transaction, idempotency, retry, and outbox capabilities may address several scenarios.

The architectural task is to determine whether those mechanisms provide the required guarantees in the actual system.

### **13.10 Distinguish Deterministic Gates from Architectural Judgment**

The Harness should use deterministic gates wherever objective conditions can be checked automatically.

Examples include successful compilation, unit and integration test execution, architecture dependency tests, static analysis, and contract validation.

However, these checks cannot establish every aspect of architectural quality.

A set of tests may pass while the implementation introduces an unnecessary service boundary or duplicates an existing capability.

The Reviewer Agent and authorized human reviewers remain responsible for examining such concerns.

Planning should therefore define two complementary forms of assurance.

| Assurance mechanism  | Primary responsibility                                                |
| -------------------- | --------------------------------------------------------------------- |
| Deterministic gates  | Verify objective, repeatable engineering conditions                   |
| Architectural review | Assess design suitability, tradeoffs, boundaries, and maintainability |
| Business evaluation  | Assess satisfaction of the intended goal and acceptance criteria      |
| Human approval       | Authorize decisions requiring organizational accountability           |

These mechanisms should reinforce one another without being treated as interchangeable.

The Plan should identify which evidence each mechanism will require.

### **13.11 Do Not Confuse Planning with the Reviewer Agent**

Planning involves evaluating possible approaches before implementation.

The Reviewer Agent examines implementation quality and alignment with the applicable architecture and requirements.

Although both activities involve engineering judgment, their responsibilities differ.

The planner asks:

*What approach should be used?*

The Reviewer Agent asks:

*Does the implemented change follow the required approach and satisfy applicable quality expectations?*

The Reviewer Agent may identify findings that require re-planning.

For example, a review may reveal that the approved implementation introduces an unacceptable coupling between application and infrastructure components.

The Lead Agent should then determine whether the issue can be corrected within the approved Plan or requires a revision.

Planning does not eliminate independent review.

Nor should the planning agent approve its own implementation simply because its original design was followed.

### **13.12 Define the Materiality Threshold for Plan Drift**

Not every implementation difference should trigger formal reapproval.

If the Developer Agent changes a private method name or refactors a local helper without altering behavior or architecture, that may remain within delegated implementation discretion.

However, some changes affect the basis of the approved Plan.

The architect should define a materiality threshold.

| Change discovered during execution                  | Typical response                       |
| --------------------------------------------------- | -------------------------------------- |
| Private method naming adjustment                    | Continue within delegated authority    |
| Additional focused unit test                        | Continue and record evidence           |
| Internal refactoring preserving approved boundaries | Assess within existing scope           |
| New external dependency                             | Review and potentially revise Plan     |
| Change to persistence consistency strategy          | Architectural reassessment             |
| New public API breaking change                      | Contract review and approval           |
| Expanded authorization permissions                  | Security review                        |
| Change to business acceptance criteria              | Business authorization                 |
| New production infrastructure requirement           | Architectural and operational approval |

These are examples rather than universal classifications.

The organization's governance policy should determine which changes require approval.

The important principle is that agents should have enough discretion to implement efficiently without being permitted to redefine the approved engineering outcome.

### **13.13 Account for Concurrent Agents and Shared Repositories**

Multi-agent workflows introduce additional planning concerns.

Two agents may independently inspect the same repository baseline and produce Plans that are individually reasonable but mutually incompatible.

For example, one agent may plan a booking persistence refactoring while another plans corporate fleet booking using the existing persistence contract.

If both implementations proceed without coordination, integration failures and architectural conflicts may occur.

The Harness should identify overlapping modification scopes and shared architectural dependencies.

Possible controls include task ownership, isolated worktrees, dependency-aware scheduling, integration checkpoints, and explicit coordination through the Lead Agent.

Planning should also identify whether a task can be decomposed safely for parallel execution.

Independent test development may be parallelizable when contracts are stable.

Competing changes to a shared transaction boundary may require sequential design and integration.

Concurrency among agents should be treated as an engineering coordination problem, not merely a productivity opportunity.

### **13.14 Separate Plan Storage from Authoritative Knowledge**

The approved Plan should be durable enough to support execution and auditability, but it should not automatically become permanent repository guidance.

This distinction is central to the handbook's Repository Intelligence and Harness Memory architecture.

A Plan may contain task-specific decisions that are valid only for a particular repository revision.

A Scratchpad may contain temporary observations.

An Architecture Decision Record may document a durable architectural decision.

An Instruction may establish a persistent engineering requirement.

A Skill may define a reusable implementation workflow.

These artifacts have different authorities and lifecycles.

| Artifact                     | Primary purpose                              | Authority                        |
| ---------------------------- | -------------------------------------------- | -------------------------------- |
| Prompt                       | Define the requested task                    | Task-specific authorized intent  |
| Plan                         | Define the proposed or approved approach     | Approved task scope              |
| Scratchpad                   | Track temporary execution context            | No independent design authority  |
| Repository Intelligence      | Describe established system knowledge        | Depends on source and governance |
| Architecture Decision Record | Preserve accepted architectural decisions    | Governed architectural authority |
| Instruction                  | Establish persistent engineering constraints | Applicable instruction authority |
| Skill                        | Provide reusable engineering procedure       | Governed workflow authority      |
| Harness Memory               | Retain approved or qualified learning        | Governed memory policy           |

An architect should require explicit promotion of reusable findings into authoritative knowledge.

The Harness may recommend a new Instruction or Skill based on repeated planning findings.

It must not silently change established standards.

### **13.15 Planning Quality Should Be Measured by Outcomes**

The number of Plans produced is not a meaningful measure of engineering effectiveness.

Neither is average Plan length.

Even planning duration, considered alone, can be misleading.

A short planning phase may be appropriate for a simple task and dangerously insufficient for a complex one.

More useful indicators include whether material assumptions were identified before execution, whether architectural changes required unplanned rework, and whether validation requirements were defined before implementation.

Potential planning metrics include:

| Metric                                     | Architectural interpretation                      |
| ------------------------------------------ | ------------------------------------------------- |
| Plan approval revision count               | Indicates review iterations; requires context     |
| Material plan drift rate                   | Reveals divergence between planning and execution |
| Unresolved blockers at execution start     | Indicates readiness control effectiveness         |
| Planning-related rework                    | Identifies missed dependencies or assumptions     |
| Validation coverage of acceptance criteria | Measures verification planning completeness       |
| Repository baseline invalidation events    | Shows planning freshness challenges               |
| Unapproved scope deviations                | Identifies governance failures                    |
| Reusable findings proposed for improvement | Supports governed organizational learning         |

These metrics should be interpreted carefully.

A high revision count may indicate weak initial planning, but it may also reflect a healthy review process on a difficult task.

A low drift rate may indicate strong planning or an unwillingness to report deviations.

Metrics should encourage reliable engineering outcomes rather than incentivize agents to conceal uncertainty.

### **13.16 Planning Must Remain Portable Across AI Platforms**

The handbook uses Claude Code as the primary practical implementation, but the planning architecture should remain vendor-neutral.

An organization may use Claude Code today and introduce GitHub Copilot, Codex, or another agent in the future.

The approved Plan should remain understandable and executable across platforms.

This requires separating platform-specific controls from engineering obligations.

For example:

| Engineering requirement | Vendor-neutral contract                     | Platform-specific implementation                   |
| ----------------------- | ------------------------------------------- | -------------------------------------------------- |
| Non-mutating planning   | Planning cannot modify protected resources  | Permission mode, sandbox, or tool policy           |
| Repository discovery    | Findings must be evidence-based             | Agent-specific inspection tools                    |
| Applicable Instructions | Relevant constraints must be respected      | `CLAUDE.md`, `AGENTS.md`, or supported equivalents |
| Plan creation           | Produce structured implementation approach  | Native planning feature or Harness workflow        |
| Approval                | Record authorized decision                  | Harness approval gateway                           |
| Execution handoff       | Transfer approved decisions and constraints | Agent-specific execution adapter                   |
| Validation              | Run required deterministic gates            | Build, test, and CI/CD integration                 |

The Harness should define the contract.

Each platform adapter should implement the required behavior using its supported capabilities.

This reduces vendor lock-in and allows the organization to replace or combine AI Agents without redesigning its governance model.

### **13.17 Planning Should Produce a Decision, Not Merely a Document**

A Plan is useful because it enables an engineering decision.

The possible outcomes of Planning Mode should not be limited to approval.

A planning process may conclude that the requested change is ready for implementation, that further investigation is required, that a business decision is missing, or that the proposed approach is unacceptable.

A practical decision model is:

| Outcome            | Meaning                                    | Next action                  |
| ------------------ | ------------------------------------------ | ---------------------------- |
| Ready for approval | Sufficient evidence and approach available | Route for required review    |
| Approved           | Required authorization satisfied           | Prepare execution handoff    |
| Revision required  | Proposed approach needs modification       | Update Plan                  |
| Blocked            | Essential dependency or decision missing   | Resolve blocker              |
| Rejected           | Proposed approach is not acceptable        | Reassess or close request    |
| Withdrawn          | Task is no longer authorized or required   | Close without implementation |

An inconclusive planning result is preferable to an unsupported claim of readiness.

For Alpha Car Detailing, uncertainty about the contractual meaning of booking limits may prevent approval of the capacity enforcement approach.

The planner should expose that condition rather than inventing a rule to complete the document.

### **13.18 Architect’s Decision Framework**

Before approving a Plan for a consequential enterprise change, the architect should evaluate five dimensions.

**1. Correctness**

Does the proposed design preserve business invariants, transaction guarantees, and contract expectations?

**2. Alignment**

Does the approach conform to the repository architecture, Instructions, Skills, and approved standards?

**3. Feasibility**

Can the proposed tasks be executed using existing capabilities, dependencies, and available resources?

**4. Verifiability**

Can required outcomes be demonstrated through appropriate tests, deterministic gates, and evaluation?

**5. Governance**

Are scope, permissions, material decisions, and required approvals explicitly controlled?

A Plan that fails materially in any one dimension may require revision.

For example, a design may be feasible and easy to validate but violate an established architecture boundary.

Another design may align with architecture standards but fail to guarantee correct behavior under concurrent requests.

Architectural approval should consider the complete engineering obligation.

### **13.19 The Long-Term Architectural Role of Planning Mode**

As enterprise Harnesses evolve, planning may become increasingly automated.

Agents may discover repository structures, select Skills, analyze past validation failures, compare architectural patterns, and propose implementation sequences with less manual intervention.

Such capabilities can improve consistency and reduce repetitive analysis.

However, increased automation does not eliminate the need for authority boundaries.

An adaptive Harness may use approved historical evidence to improve planning recommendations.

It should not silently change business requirements, relax validation gates, modify architectural Instructions, or authorize new infrastructure.

The planning process must remain explainable and accountable.

The architect should ensure that the enterprise can answer three questions about any significant AI-assisted change:

**Why was this implementation approach selected?**

**Who or what authorized the important decisions?**

**What evidence demonstrates that the implementation satisfies the required outcome?**

A well-designed Planning Mode contributes directly to answering all three.

> **Architect's Note**
>
> Planning Mode should be considered part of the enterprise architecture for AI-assisted software delivery.
>
> Its purpose is not to slow agents down or require documentation for its own sake. Its purpose is to ensure that implementation speed does not outpace understanding, authorization, or verification.
>
> The most effective architecture separates the agent's ability to propose a solution from its authority to implement that solution, while preserving controlled adaptation when new evidence emerges.

### **13.20 Architectural Conclusion**

Planning Mode provides the bridge between task intent and authorized implementation.

It connects the Repository Intelligence concepts introduced in Part II with the agent responsibilities and validation mechanisms established in Part III.

Within Part IV, Claude Code supplies practical capabilities for performing planning work, but the governing principles remain platform-independent.

For Alpha Car Detailing, the corporate fleet booking Plan should establish the business invariants, implementation boundaries, dependency relationships, transaction strategy, idempotency behavior, validation requirements, and required approvals before code changes begin.

The result is not a guarantee that execution will be error-free.

It is a stronger engineering foundation for implementation, review, validation, evaluation, and controlled delivery.

**An enterprise architect should judge Planning Mode by whether it converts uncertain requirements into an evidence-based, authorized, and verifiable implementation approach.**

Chapter 27 status: In progress — next section: Enterprise Tips

## **14. Enterprise Tips**

Planning Mode becomes particularly valuable when AI-assisted development moves beyond individual engineers and becomes part of a standardized enterprise delivery process.

At the enterprise level, planning must support multiple repositories, development teams, AI Agents, approval authorities, and delivery pipelines. A planning approach that works well for one developer in an interactive Claude Code session may not provide sufficient control for unattended execution or multi-agent development.

The objective is to establish consistent engineering expectations without creating unnecessary bureaucracy.

The following enterprise practices focus on operationalizing Planning Mode within the Harness while preserving the separation between planning, execution, validation, evaluation, and governance.

### **14.1 Establish an Organization-Wide Planning Policy**

Organizations should define when formal planning is required, which information the Plan must contain, and which decisions require human approval.

Without a common policy, individual teams may apply inconsistent standards.

One team may permit an AI Agent to modify database schemas immediately after receiving a Prompt. Another may require architectural approval for every minor code change.

Neither extreme provides a reliable enterprise-wide approach.

A planning policy should establish risk-based requirements.

| Change category                         | Planning requirement                | Typical authorization                     |
| --------------------------------------- | ----------------------------------- | ----------------------------------------- |
| Low-risk localized change               | Lightweight planning                | Delegated engineering authority           |
| Multi-component feature                 | Structured implementation Plan      | Technical lead as required                |
| Shared architecture change              | Formal Plan with design rationale   | Architect                                 |
| Security-sensitive change               | Formal Plan with security analysis  | Security authority                        |
| Database or integration contract change | Impact and compatibility assessment | Relevant technical owner                  |
| Production infrastructure change        | Operational and rollback planning   | Authorized operations or change authority |

These categories should be adapted to the organization's systems and risk tolerance.

The policy should also define exceptions and emergency procedures.

A production incident may require accelerated planning and approval, but urgency should not silently eliminate essential security or data-integrity safeguards.

> **Enterprise Tip**
>
> Establish one organizational planning policy with risk-based profiles rather than creating a different approval process for every repository. Teams should be able to adopt common controls while applying additional requirements to critical systems.

### **14.2 Standardize the Plan Artifact**

A common Plan structure improves consistency across development teams and AI coding platforms.

The organization should define a reusable Plan template containing the information needed for execution, review, validation, and governance.

An illustrative template location is:

```
harness/
    templates/
        implementation-plan.md
```

The template might require:

```
# Implementation Plan

## Task Identity
## Goal
## Repository Baseline
## Repository Evidence
## Applicable Instructions
## Selected Skills
## Scope
## Architectural Decisions
## Assumptions and Open Questions
## Dependencies
## Risks and Mitigations
## Acceptance Criteria
## Implementation Tasks
## Validation Strategy
## Required Approvals
## Execution Handoff
```

A shared structure allows the Harness to perform deterministic completeness checks and helps reviewers locate important information quickly.

However, the template should not encourage unnecessary repetition.

A small change may require short responses to many sections, while a significant architectural change may require detailed rationale and additional diagrams.

The objective is consistency of engineering obligations, not uniform document length.

### **14.3 Implement Planning as a Harness State**

In an enterprise Harness, Planning Mode should be represented explicitly in the task lifecycle.

The Harness should know whether a task is awaiting investigation, under planning review, approved for execution, blocked, or being re-planned.

A representative lifecycle is:

\#chatgpt-mermaid-\_r_1c5h\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c5h\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c5h\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c5h\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c5h\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c5h\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c5h\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c5h\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c5h\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c5h\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c5h\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c5h\_ p{margin:0;}#chatgpt-mermaid-\_r_1c5h\_ defs [id$="-barbEnd"]{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ g.stateGroup text{fill:rgb(31, 78, 148);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1c5h\_ g.stateGroup text{fill:rgb(237, 237, 237);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1c5h\_ g.stateGroup .state-title{font-weight:bolder;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ g.stateGroup rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c5h\_ g.stateGroup line{stroke:rgb(175, 175, 175);stroke-width:1;}#chatgpt-mermaid-\_r_1c5h\_ .transition{stroke:rgb(175, 175, 175);stroke-width:1;fill:none;}#chatgpt-mermaid-\_r_1c5h\_ .stateGroup .composit{fill:transparent;border-bottom:1px;}#chatgpt-mermaid-\_r_1c5h\_ .stateGroup .alt-composit{fill:#e0e0e0;border-bottom:1px;}#chatgpt-mermaid-\_r_1c5h\_ .state-note{stroke:rgb(58, 132, 63);fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c5h\_ .state-note text{fill:rgb(237, 237, 237);stroke:none;font-size:10px;}#chatgpt-mermaid-\_r_1c5h\_ .stateLabel .box{stroke:none;stroke-width:0;fill:rgb(9, 23, 44);opacity:0.5;}#chatgpt-mermaid-\_r_1c5h\_ .edgeLabel .label rect{fill:rgb(9, 23, 44);opacity:0.5;}#chatgpt-mermaid-\_r_1c5h\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c5h\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c5h\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c5h\_ .edgeLabel .label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .label div .edgeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .stateLabel text{fill:rgb(237, 237, 237);font-size:10px;font-weight:bold;}#chatgpt-mermaid-\_r_1c5h\_ .node circle.state-start{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ .node .fork-join{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ .node circle.state-end{fill:rgb(31, 78, 148);stroke:transparent;stroke-width:1.5;}#chatgpt-mermaid-\_r_1c5h\_ .end-state-inner{fill:transparent;stroke-width:1.5;}#chatgpt-mermaid-\_r_1c5h\_ .node rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c5h\_ .node polygon{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c5h\_ [id$="-barbEnd"]{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-cluster rect{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c5h\_ .cluster-label,#chatgpt-mermaid-\_r_1c5h\_ .nodeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-cluster rect.outer{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-state .divider{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-state .title-state{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-cluster.statediagram-cluster .inner{fill:transparent;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-cluster.statediagram-cluster-alt .inner{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-cluster .inner{rx:0;ry:0;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-state rect.basic{rx:5px;ry:5px;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-state rect.divider{stroke-dasharray:10,10;fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c5h\_ .note-edge{stroke-dasharray:5;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-note rect{fill:rgb(48, 48, 48);stroke:rgb(58, 132, 63);stroke-width:1px;rx:0;ry:0;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-note rect{fill:rgb(48, 48, 48);stroke:rgb(58, 132, 63);stroke-width:1px;rx:0;ry:0;}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-note text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram-note .nodeLabel{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ .statediagram .edgeLabel{color:red;}#chatgpt-mermaid-\_r_1c5h\_ [id$="-dependencyStart"],#chatgpt-mermaid-\_r_1c5h\_ [id$="-dependencyEnd"]{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);stroke-width:1;}#chatgpt-mermaid-\_r_1c5h\_ .statediagramTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].statediagram-cluster rect{fill:rgb(9, 23, 44);stroke:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);stroke-width:1;}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].statediagram-cluster rect.outer{rx:5px;ry:5px;filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c5h\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c5h\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}ReceivedDiscoveringPlanningPlanReviewBlockedApprovedExecutingReplanningValidatingEvaluatingCompletedRevision requiredUnresolved prerequisiteRequired approvals satisfiedPrerequisite resolvedExecution authorizedMaterial driftRevised planImplementation readyRework requiredRequired gates passedAcceptedCorrection required

The implementation should enforce valid transitions.

For example, a task requiring architectural approval must not move directly from Planning to Executing.

The Harness must record why a transition was permitted and which authorization conditions were satisfied.

This is especially important for unattended agent execution.

### **14.4 Keep Planning Workspaces Isolated**

Enterprise planning environments should operate with the minimum permissions needed to investigate the requested change.

A planning agent may need to inspect source code, repository history, API contracts, Instructions, and existing tests.

It should not automatically receive permission to modify those resources or perform external actions.

A robust implementation may provide a read-only repository snapshot and a separate controlled area for Plan artifacts.

For example:

```
harness/
    runs/
        corporate-fleet-booking-001/
            context/
            plan.md
            approvals.json
            scratchpad.md
            evidence/
```

This structure is illustrative.

A strict implementation may store planning artifacts outside the source checkout entirely, allowing the agent to write the proposed Plan without obtaining write access to application code.

The distinction matters because some development commands can have side effects even when they appear to perform inspection.

Build commands may generate files. Package-management commands may modify lockfiles or caches. External tools may create resources.

The enterprise should define an explicit allowlist of permitted planning operations.

### **14.5 Use a Deterministic Plan Quality Gate**

The Harness should verify basic structural requirements before routing a Plan for approval.

A deterministic gate may check that:

- The Plan has a valid task identifier and revision.
- The inspected repository baseline is recorded.
- Required sections are present.
- Scope boundaries are declared.
- Acceptance criteria are identified.
- Validation requirements exist.
- Material assumptions have a status.
- Blocking questions are not silently ignored.
- Required approval fields are present.

These checks improve consistency and reduce administrative review effort.

However, the gate must not claim that the proposed architecture is correct merely because the document contains the required information.

For example, a Plan may contain a concurrency section while still recommending an unsafe count-then-insert implementation.

Such a defect requires substantive architectural review.

The enterprise should distinguish between *structural completeness* and *engineering adequacy*.

### **14.6 Centralize Approval Policies Without Centralizing Every Decision**

Approval responsibilities should reflect the organizational ownership of systems and risks.

An enterprise architecture team may own shared architectural standards, while individual development teams retain authority over routine implementation decisions.

A security team may own authorization policy exceptions.

A business owner may be responsible for contractual behavior.

Planning should route decisions to the appropriate authority rather than requiring one central committee to approve every task.

For Alpha Car Detailing, confirmation of the corporate booking-limit definition belongs to the appropriate business authority.

Approval of a change to transactional consistency may belong to a solution architect or designated technical owner.

Approval of expanded corporate account permissions may require security review.

The Harness should record the decision, approver authority, Plan revision, and outcome.

It should also prevent an AI Agent from approving its own material changes.

### **14.7 Establish a Clear Handoff Contract Between Agents**

An enterprise Harness may coordinate several specialized roles.

The Lead Agent creates or coordinates the Plan. The Developer Agent implements it. The Reviewer Agent examines code quality and architectural alignment. The Validator Agent verifies deterministic evidence. The Evaluator Agent assesses goal satisfaction.

These roles should exchange explicit artifacts rather than depending on informal conversational continuity.

An execution handoff should contain:

| Information                 | Purpose                                  |
| --------------------------- | ---------------------------------------- |
| Task and Plan identifiers   | Preserve traceability                    |
| Approved Plan revision      | Establish the implementation contract    |
| Repository baseline         | Identify the expected starting state     |
| Applicable Instructions     | Preserve engineering constraints         |
| Selected Skills             | Identify reusable implementation methods |
| Scope boundaries            | Prevent unauthorized modifications       |
| Ordered tasks               | Guide execution                          |
| Approved decisions          | Preserve architectural intent            |
| Open non-blocking questions | Prevent hidden uncertainty               |
| Validation obligations      | Define required evidence                 |
| Escalation conditions       | Identify when execution must pause       |

The receiving Developer Agent should acknowledge the constraints before making changes.

Where the Harness supports automated enforcement, it should compare implementation changes with the authorized scope.

> **Enterprise Tip**
>
> Design the Plan as a platform-independent handoff artifact. The Lead Agent may use Claude Code while the Developer Agent uses another supported coding agent, provided the approved engineering contract and governance controls remain intact.

### **14.8 Integrate Planning with Pull Request Governance**

Planning should connect naturally with existing pull request processes.

For significant changes, the pull request should reference the approved Plan or its immutable record.

Reviewers should be able to determine whether the implementation follows the approved approach and whether material deviations were authorized.

The pull request should also reference relevant validation evidence.

A practical relationship is:

```
Engineering Task
      |
      v
Approved Plan
      |
      v
Implementation Branch
      |
      v
Pull Request
      |
      +-- Plan Reference
      +-- Scope Summary
      +-- Implementation Changes
      +-- Validation Results
      +-- Review Findings
      +-- Approval Evidence
```

The Plan should not duplicate the entire pull request description.

Instead, the two artifacts should complement each other.

The Plan records the intended approach.

The pull request records the actual proposed implementation and its evidence.

If significant differences exist, reviewers should be able to see how those differences were assessed.

### **14.9 Use Planning to Coordinate Parallel Agent Work**

Multi-agent implementation can improve throughput when tasks are genuinely independent.

However, parallel execution should be introduced only after dependency and ownership boundaries are understood.

For corporate fleet booking, API contract analysis and test-environment preparation may proceed independently once shared assumptions are established.

By contrast, simultaneous changes to the same booking transaction boundary may create conflicts.

The planning process should identify:

- Tasks that can proceed independently.
- Shared interfaces that must remain stable.
- Components requiring exclusive modification ownership.
- Integration checkpoints.
- Dependencies between parallel workstreams.
- Conditions that invalidate parallel execution.

The Harness should coordinate these relationships.

A task should not be assigned to multiple Developer Agents merely because parallel processing is available.

Concurrency among agents requires the same discipline as concurrency within distributed software systems: defined ownership, coordination, and conflict handling.

### **14.10 Define Plan Freshness and Revalidation Policies**

Enterprise repositories change continuously.

A Plan prepared against one repository revision may become stale before execution begins.

The organization should define when a Plan must be revalidated.

A practical policy may use the following criteria:

| Condition                                   | Response                                                 |
| ------------------------------------------- | -------------------------------------------------------- |
| Repository revision unchanged               | Proceed if other conditions remain satisfied             |
| Unrelated documentation changed             | Record difference; no material revision may be necessary |
| Relevant source component changed           | Reassess affected planning assumptions                   |
| API or event contract changed               | Reevaluate compatibility and dependencies                |
| Applicable Instruction changed              | Reconcile the Plan with updated constraints              |
| Security policy changed                     | Reassess authorization and compliance                    |
| Approved architectural decision invalidated | Revise and obtain required approval                      |

An automated freshness check can compare repository revisions and relevant dependencies.

However, it should not assume that every commit change invalidates the entire Plan.

The purpose is to identify material changes, not to create unnecessary reapproval cycles.

### **14.11 Preserve an Auditable Planning History**

An enterprise Harness should preserve enough evidence to reconstruct significant planning decisions.

This is particularly important for regulated systems, high-impact business changes, and unattended agent execution.

An audit record should identify the original requirement, the repository baseline, material planning findings, Plan revisions, approval decisions, and execution authorization.

A representative record might contain:

```
task_id: ACD-FLEET-001
plan_id: PLAN-ACD-FLEET-001
plan_revision: 3

repository:
  baseline: "<commit-sha>"

planning:
  status: approved
  agent_role: lead
  evidence_reference: evidence/repository-discovery.json

approval:
  required: true
  status: approved
  approved_plan_revision: 3

execution:
  authorized: true
  status: not_started
```

This is an illustrative Harness record rather than a platform-defined configuration file.

In production, approval records should be protected against unauthorized modification and associated with authenticated approval identities.

A plain editable YAML file alone is not sufficient evidence of trustworthy authorization.

The Harness may store signed or access-controlled approval records in a dedicated system while presenting a readable representation to engineers.

### **14.12 Protect Sensitive Information During Planning**

Repository discovery can expose configuration details, architecture documents, service endpoints, and other sensitive information.

Planning agents should receive only the access necessary to perform the task.

The organization should ensure that secrets, credentials, and sensitive customer data are not unnecessarily copied into Plans or Scratchpads.

For Alpha Car Detailing, corporate booking analysis may require understanding account identifiers and authorization relationships.

It ordinarily does not require exposing real customer data in the planning artifact.

Use representative test data and redact sensitive values where appropriate.

The Harness should also apply retention and access policies to planning artifacts.

A temporary Scratchpad should not become an uncontrolled storage location for sensitive information.

### **14.13 Plan Production Changes Separately from Development Changes**

A Plan authorizing source-code implementation should not automatically authorize production deployment.

Development and production changes have different operational consequences.

For example, corporate fleet booking may require a database migration.

The engineering Plan can define the migration design, compatibility requirements, and validation strategy.

Applying the migration to production may require a separate deployment authorization, maintenance procedure, or change-management decision.

The organization should distinguish:

**Implementation approval:** Permission to develop the change.

**Merge approval:** Permission to incorporate the implementation into the controlled codebase.

**Deployment approval:** Permission to release the change into a target environment.

**Operational authorization:** Permission to perform specific privileged actions.

These decisions may be combined under an approved organizational process, but they should never be assumed to be identical.

### **14.14 Plan Rollback and Recovery for High-Impact Changes**

For changes with operational consequences, Planning Mode should consider rollback and recovery requirements before implementation.

This is especially important for schema changes, external contracts, and distributed workflows.

For Alpha Car Detailing, the Plan may need to address what happens if the new corporate fleet booking capability must be disabled after release.

Can the API be disabled through a feature flag?

Are database changes backward compatible?

Will existing booking consumers continue to function?

Can partially deployed components operate safely during a rolling deployment?

How will failed or duplicated events be reconciled?

Not every task requires a detailed rollback design.

However, when a change can affect production data integrity or availability, recovery planning should be part of the engineering approach.

> **Enterprise Tip**
>
> For high-impact changes, require the Plan to explain not only how the feature will be implemented but also how the organization will detect problems, contain failures, and restore acceptable operation.

### **14.15 Use Planning Metrics Carefully**

Enterprise engineering leaders may want to measure the effectiveness of AI-assisted planning.

Useful measurements may include how frequently material assumptions are discovered after implementation begins, how often Plans require substantial revision, and how much rework results from missed dependencies.

However, these metrics should not be interpreted without context.

A Plan that requires multiple review cycles may reflect poor preparation, but it may also reflect a complex problem receiving appropriate scrutiny.

Similarly, a short planning duration may indicate an efficient workflow or insufficient investigation.

Organizations should avoid simplistic targets such as minimizing the number of Plan revisions.

Such targets may encourage agents or engineers to conceal uncertainty.

A better objective is to reduce preventable implementation rework while preserving engineering quality.

Planning metrics should support improvement rather than become incentives for superficial compliance.

### **14.16 Build a Governed Library of Planning Patterns**

Enterprise teams often encounter recurring categories of engineering work.

Examples include adding APIs, evolving database schemas, introducing event consumers, implementing authorization changes, and refactoring shared services.

The organization can establish reusable planning patterns for these categories.

A planning pattern may define common questions, dependency checks, risk areas, and validation expectations.

For example, an event-driven feature planning pattern might require investigation of event ownership, schema compatibility, delivery guarantees, idempotent consumers, outbox integration, and observability.

A database migration planning pattern might require backward compatibility, transactional behavior, deployment sequencing, rollback, and data integrity validation.

These patterns can be implemented through governed Skills or planning templates.

However, they should guide investigation rather than dictate a universal architecture.

A reusable pattern must remain subordinate to the actual repository evidence and applicable Instructions.

### **14.17 Use Planning Findings to Improve Repository Intelligence**

Repeated planning difficulties may indicate gaps in the organization's engineering knowledge.

For example, if agents repeatedly struggle to locate Alpha Car Detailing's transaction-management abstractions, the repository may need better architectural documentation.

If multiple Plans misunderstand the idempotency mechanism, a reusable Skill or Knowledge Source may be incomplete.

The Harness can analyze these patterns and recommend improvements.

Possible recommendations include:

| Repeated planning issue                       | Potential improvement                       |
| --------------------------------------------- | ------------------------------------------- |
| Agents cannot identify transaction boundaries | Improve architecture documentation          |
| Inconsistent API implementation approaches    | Refine applicable Instructions or API Skill |
| Repeated confusion about idempotency          | Add implementation guidance and tests       |
| Missing validation requirements               | Improve planning template and gate policy   |
| Frequent contract compatibility defects       | Add contract-validation Skill               |
| Repeated approval delays                      | Clarify ownership and escalation procedures |

Any proposed change to authoritative Instructions, Skills, or architectural standards should follow the established governance process.

The Harness must not silently revise those artifacts based on unreviewed observations.

This preserves the distinction between temporary experience and approved organizational knowledge.

### **14.18 Support Planning Across Multiple Repositories**

Large enterprise systems frequently span multiple repositories.

A feature request may affect an API repository, shared contract package, infrastructure repository, and integration test suite.

Planning Mode must recognize these boundaries.

The Plan should identify which repositories are involved, which teams own them, and which changes must be coordinated.

For example, corporate fleet booking may require a booking service change and an event contract update consumed by another application.

If the consuming application is maintained separately, the planning process should identify that dependency before execution.

A multi-repository Plan should establish compatible baselines, contract ownership, and integration checkpoints.

The Harness should not grant write access to every related repository simply because the planning agent discovered a dependency.

Each repository modification must remain within authorized scope.

### **14.19 Provide a Standard Escalation Process**

An AI Agent may encounter a problem that cannot be resolved within its delegated authority.

The enterprise should define how such issues are escalated.

Examples include contradictory business requirements, missing security approvals, incompatible architectural constraints, or a newly discovered dependency on another team's system.

The escalation record should identify:

| Field                  | Purpose                            |
| ---------------------- | ---------------------------------- |
| Blocking issue         | Explain what prevents progress     |
| Supporting evidence    | Identify the relevant findings     |
| Affected Plan tasks    | Show which work is blocked         |
| Available alternatives | Present feasible options           |
| Recommended decision   | Provide engineering rationale      |
| Decision owner         | Identify the responsible authority |
| Impact of delay        | Explain delivery consequences      |
| Resolution status      | Track the outcome                  |

The agent should not resolve an organizational authority problem by making an unsupported assumption.

A clearly blocked Plan is preferable to unauthorized execution.

### **14.20 Establish Operational Readiness Criteria for Execution**

Before the Harness transitions from planning to execution, it should verify that the necessary conditions have been met.

A representative execution-readiness policy is:

```
Execution may begin only when:

1. The engineering goal is defined.
2. The repository baseline is known.
3. Applicable Instructions are resolved.
4. The proposed implementation scope is explicit.
5. Blocking assumptions are resolved.
6. Required architectural decisions are approved.
7. Implementation tasks are executable.
8. Validation requirements are defined.
9. Required human approvals are recorded.
10. The execution agent has appropriate permissions.
```

The Harness should evaluate these conditions according to the task's risk classification.

For low-risk changes, some checks may be simplified.

For high-impact changes, additional security, operational, or compliance requirements may apply.

The objective is to create a reliable transition into execution without imposing unnecessary ceremony.

### **14.21 Enterprise Planning Reference Model**

The following architecture illustrates how planning can operate as an enterprise capability across repositories and AI coding platforms.

\#chatgpt-mermaid-\_r_1c69\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c69\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c69\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c69\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c69\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c69\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c69\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c69\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c69\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c69\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c69\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c69\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c69\_ p{margin:0;}#chatgpt-mermaid-\_r_1c69\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1c69\_ .label text,#chatgpt-mermaid-\_r_1c69\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .node rect,#chatgpt-mermaid-\_r_1c69\_ .node circle,#chatgpt-mermaid-\_r_1c69\_ .node ellipse,#chatgpt-mermaid-\_r_1c69\_ .node polygon,#chatgpt-mermaid-\_r_1c69\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .rough-node .label text,#chatgpt-mermaid-\_r_1c69\_ .node .label text,#chatgpt-mermaid-\_r_1c69\_ .image-shape .label,#chatgpt-mermaid-\_r_1c69\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1c69\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .rough-node .label,#chatgpt-mermaid-\_r_1c69\_ .node .label,#chatgpt-mermaid-\_r_1c69\_ .image-shape .label,#chatgpt-mermaid-\_r_1c69\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1c69\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1c69\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c69\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c69\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1c69\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c69\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c69\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c69\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1c69\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1c69\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c69\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1c69\_ .icon-shape,#chatgpt-mermaid-\_r_1c69\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c69\_ .icon-shape p,#chatgpt-mermaid-\_r_1c69\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1c69\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1c69\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c69\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1c69\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1c69\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c69\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c69\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c69\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c69\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c69\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c69\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c69\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c69\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c69\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1c69\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1c69\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .node rect,#chatgpt-mermaid-\_r_1c69\_ .node circle,#chatgpt-mermaid-\_r_1c69\_ .node ellipse,#chatgpt-mermaid-\_r_1c69\_ .node polygon,#chatgpt-mermaid-\_r_1c69\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1c69\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1c69\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1c69\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1c69\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c69\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1c69\_ .planning rect{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .planning polygon{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .planning ellipse{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .planning circle{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .planning path{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .planning tspan{fill:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c69\_ .control rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .control polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .control ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .control circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .control path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .control tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c69\_ .execution tspan{fill:rgb(33, 84, 53)!important;}Engineering RequestHarness Task IntakeRisk ClassificationRepository DiscoveryRepository InstructionsPlanning ContextSkills and Knowledge SourcesBusiness ConstraintsPlanning AgentProposed PlanDeterministic Plan GateArchitectural ReviewApproval GatewayAuthorized?Execution HandoffDeveloper AgentImplementationReviewer AgentValidator AgentEvaluator AgentCompletion DecisionRun Evidence and MetricsGoverned ImprovementProposalsRevisionYes

The diagram separates three architectural concerns.

The **planning layer** establishes the proposed implementation approach.

The **control layer** checks readiness, architectural quality, and authorization.

The **execution layer** implements the approved change and produces evidence for review, validation, and evaluation.

The Harness coordinates all three.

The planning agent may be Claude Code, GitHub Copilot, Codex, or another supported platform.

The engineering responsibilities remain consistent.

### **14.22 Enterprise Adoption Strategy**

Organizations should introduce governed Planning Mode incrementally.

A practical adoption sequence begins with a small set of repositories and consequential development tasks.

The team can establish a common Plan template, define risk-based approval thresholds, and introduce a basic Plan Quality Gate.

Once the process is stable, the Harness can automate baseline tracking, approval routing, execution handoff, and drift detection.

More advanced capabilities, such as cross-repository dependency analysis and learning from historical planning outcomes, can be added as organizational evidence and operational maturity increase.

An illustrative maturity progression is:

| Maturity level         | Planning capability                                                    |
| ---------------------- | ---------------------------------------------------------------------- |
| Level 1 — Interactive  | Engineers manually request and review Plans                            |
| Level 2 — Standardized | Shared Plan templates and review expectations                          |
| Level 3 — Governed     | Explicit approval states and execution permissions                     |
| Level 4 — Integrated   | Harness-managed planning, handoff, and drift detection                 |
| Level 5 — Adaptive     | Governed recommendations based on prior planning and delivery evidence |

These levels represent a conceptual adoption model, not an external certification or industry standard.

The organization should prioritize reliability over reaching a particular maturity label.

A well-governed, straightforward planning workflow may be more valuable than an elaborate autonomous planning system with unclear authority boundaries.

### **14.23 Enterprise Planning Readiness Checklist**

Before rolling out Planning Mode across enterprise teams, engineering leadership should evaluate whether the operating model is adequately defined.

| Readiness area         | Enterprise requirement                                            |
| ---------------------- | ----------------------------------------------------------------- |
| Planning policy        | Formal planning thresholds are documented                         |
| Plan template          | Required planning information is standardized                     |
| Repository discovery   | Agents can access appropriate evidence                            |
| Instruction governance | Applicable Instructions have defined ownership                    |
| Skill governance       | Reusable workflows are reviewed and maintained                    |
| Permission model       | Planning and execution permissions are separated                  |
| Approval model         | Required approvers and authorization rules are identified         |
| Plan quality           | Structural and substantive review mechanisms exist                |
| Baseline tracking      | Plans can be associated with repository revisions                 |
| Drift management       | Material implementation deviations have a defined process         |
| Agent handoff          | Approved Plans can be transferred reliably                        |
| Validation integration | Acceptance criteria map to required checks                        |
| Auditability           | Significant decisions and transitions are traceable               |
| Security               | Sensitive data and tools are appropriately restricted             |
| Metrics                | Planning effectiveness can be assessed without encouraging gaming |
| Knowledge evolution    | Reusable findings follow governed promotion processes             |

Not every organization requires full automation of every capability before introducing Planning Mode.

However, these concerns should be understood and addressed proportionately as AI-assisted development scales.

### **14.24 Enterprise Conclusion**

Planning Mode should become a predictable part of enterprise AI-assisted delivery rather than an optional behavior selected independently by each developer or AI Agent.

The enterprise Harness provides the appropriate architectural boundary for standardizing planning requirements, enforcing permissions, recording approvals, and coordinating downstream execution.

For Alpha Car Detailing, this means the corporate fleet booking requirement enters implementation only after the necessary business rules, architectural decisions, dependencies, risks, and validation requirements have been established.

The organization can then use Claude Code or another supported AI coding platform to implement the approved approach while retaining consistent engineering governance.

The broader principle is that enterprise AI-assisted development requires more than capable coding agents.

It requires a system that determines when implementation is ready to begin, what agents are authorized to change, and what evidence must be produced before work can be accepted.

> **Enterprise Tip**
>
> Make Planning Mode a governed Harness capability with proportionate controls. Standardize the planning contract, approval boundaries, and execution handoff while allowing individual teams and AI coding platforms flexibility in how they perform the underlying analysis.
>
> The objective is not to institutionalize planning paperwork. It is to make consequential engineering decisions visible, reviewable, and enforceable before they become code.

**At enterprise scale, Planning Mode is most effective when it combines the adaptability of AI-assisted investigation with the discipline of established software architecture and delivery governance.**

Chapter 27 status: In progress — next section: Decision Points

## **15. Decision Points**

Planning Mode introduces a structured decision-making process between understanding an engineering requirement and authorizing its implementation.

The value of this process is not simply that an AI Agent produces a Plan. Its value lies in making consequential engineering choices explicit before they become embedded in source code, configuration, infrastructure, or operational behavior.

In enterprise environments, some decisions can be delegated to an AI Agent, while others require technical leadership, architectural review, business confirmation, or security approval.

The challenge is determining which decisions should be made during planning, what evidence is required, and when the Harness should permit execution to proceed.

The following decision points provide a practical framework for architects, technical leads, and enterprise teams.

### **15.1 Decision Point: Is Formal Planning Required?**

The first decision is whether the task requires an explicit Planning Mode or can proceed through a lightweight development workflow.

Formal planning introduces useful controls, but it also consumes engineering time and review effort.

The decision should be based on risk, uncertainty, and potential impact rather than task size alone.

A modification involving only a few lines of code may still require planning if it affects security or transactional consistency.

Conversely, a larger but mechanically straightforward change may require limited architectural investigation if its scope and validation are already well defined.

| Condition                                                  | Recommended approach                    |
| ---------------------------------------------------------- | --------------------------------------- |
| Localized change with clear requirements and focused tests | Lightweight planning                    |
| Change involving several application components            | Structured Planning Mode                |
| Unclear business rules or acceptance criteria              | Planning with clarification             |
| Changes to transaction or consistency boundaries           | Formal architectural planning           |
| Security or authorization changes                          | Formal planning with security review    |
| Public API or event contract changes                       | Planning with compatibility assessment  |
| Production infrastructure changes                          | Formal planning with operational review |

For Alpha Car Detailing, corporate fleet booking requires formal planning because the feature introduces or extends behavior involving contractual limits, concurrent requests, idempotency, and transaction consistency.

**Decision:** Require formal Planning Mode when the potential consequences of an incorrect implementation justify explicit analysis and review.

### **15.2 Decision Point: Is the Requirement Sufficiently Understood?**

A Prompt may express the desired outcome without defining all the business rules necessary for implementation.

The planning agent must determine whether the available requirements support meaningful design decisions.

For corporate fleet booking, the phrase *respect corporate booking limits* is insufficient by itself.

The planner must establish whether limits apply per request, per corporate account, per station, or across a time period.

It must also determine how cancellations, expired reservations, and partially completed bookings affect capacity.

These decisions can change the domain model and persistence strategy.

An AI Agent should not select a convenient interpretation merely to continue planning.

| Requirement condition                                 | Planning response                                                 |
| ----------------------------------------------------- | ----------------------------------------------------------------- |
| Fully defined and supported by authoritative evidence | Continue planning                                                 |
| Minor ambiguity within delegated discretion           | Document the assumption and proceed where permitted               |
| Material ambiguity affecting business behavior        | Request an authorized decision                                    |
| Contradictory requirements                            | Identify the conflict and block dependent design decisions        |
| Missing acceptance criteria                           | Propose criteria for review rather than treating them as approved |

The planner may continue investigating independent technical concerns while awaiting clarification.

However, implementation tasks dependent on unresolved business semantics should remain blocked.

**Decision:** Do not authorize implementation of behavior whose essential business meaning remains unresolved.

### **15.3 Decision Point: Is Repository Discovery Sufficient?**

Planning requires an understanding of the existing system.

The architect must determine whether the agent has inspected enough repository evidence to justify its proposed approach.

This does not require exhaustive analysis of every project or file.

It requires targeted investigation of the components and relationships relevant to the change.

For corporate fleet booking, the minimum useful discovery should establish the booking workflow, corporate account integration, persistence behavior, existing idempotency capabilities, event publication strategy, and applicable tests.

A decision framework is:

| Discovery finding                               | Decision                                                    |
| ----------------------------------------------- | ----------------------------------------------------------- |
| Relevant components and dependencies identified | Proceed to architecture planning                            |
| Existing implementation partially understood    | Continue targeted discovery                                 |
| Important component inaccessible                | Document limitation and assess whether planning can proceed |
| Conflicting architecture evidence               | Investigate and resolve discrepancy                         |
| Relevant repository baseline changed            | Reassess affected findings                                  |

The planner should distinguish evidence from inference.

For example, the existence of an `Outbox` directory does not by itself prove that booking transactions persist outbox records atomically.

The actual implementation must be inspected.

**Decision:** Approve the proposed approach only when material architectural claims are supported by sufficient repository evidence.

### **15.4 Decision Point: Should Existing Capabilities Be Reused or New Components Introduced?**

AI coding agents can generate new abstractions quickly.

However, the ability to create new components does not establish that they are necessary.

The planner must decide whether the requested capability can be implemented using the existing architecture.

For Alpha Car Detailing, existing services may already provide idempotency, transaction management, event delivery, and authorization.

Introducing competing implementations may increase maintenance cost and create inconsistent behavior.

The preferred approach is to reuse suitable existing capabilities.

New abstractions should be introduced when the existing architecture cannot satisfy the requirement without unacceptable coupling, complexity, or correctness risks.

The decision should evaluate:

| Consideration           | Architectural question                                             |
| ----------------------- | ------------------------------------------------------------------ |
| Functional suitability  | Does the existing capability satisfy the requirement?              |
| Architectural alignment | Does reuse preserve established boundaries?                        |
| Reliability             | Does the mechanism provide required guarantees?                    |
| Compatibility           | Will the change affect existing consumers?                         |
| Maintainability         | Does reuse reduce duplication without creating excessive coupling? |
| Extensibility           | Will the approach support foreseeable authorized needs?            |
| Implementation scope    | Is introducing a new component justified by the task?              |

For example, an existing idempotency mechanism should be reused only after verifying that its key scope, persistence behavior, and retry semantics are suitable for corporate fleet booking.

**Decision:** Prefer compatible existing capabilities, but do not force reuse when evidence shows they cannot satisfy the required engineering guarantees.

### **15.5 Decision Point: What Must Be Included in the Implementation Scope?**

The planner must define the boundary between necessary change and unrelated improvement.

This decision is important because AI Agents may identify opportunities to refactor or modernize nearby components during repository discovery.

Those opportunities may be valid but should not automatically expand the task.

For corporate fleet booking, the implementation may require API changes, application logic, persistence modifications, outbox integration, and tests.

It does not automatically authorize replacing the booking architecture, changing unrelated endpoints, or introducing a new messaging platform.

The Plan should distinguish three categories.

**Required changes** are necessary to satisfy the approved goal.

**Supporting changes** are justified by implementation or validation dependencies.

**Deferred improvements** may be useful but fall outside the authorized scope.

A supporting change should still be assessed for architectural impact.

For example, a database migration may be necessary for capacity enforcement, but the production rollout of that migration may require additional approval.

**Decision:** Include only work that is necessary, justified by dependencies, or separately authorized.

### **15.6 Decision Point: What Level of Task Decomposition Is Appropriate?**

The Plan must be sufficiently detailed to guide implementation without attempting to prescribe every line of code.

Too little decomposition makes execution ambiguous.

Too much decomposition creates rigidity and unnecessary maintenance.

The architect should select a level of detail appropriate to task complexity.

A well-defined implementation task should identify its intended output, dependencies, and verification obligations.

| Decomposition level      | Example                                                                                                      | Assessment                                       |
| ------------------------ | ------------------------------------------------------------------------------------------------------------ | ------------------------------------------------ |
| Too broad                | Implement corporate booking                                                                                  | Insufficient guidance                            |
| Appropriate              | Implement atomic capacity enforcement using the approved persistence strategy and verify concurrent requests | Executable and verifiable                        |
| Excessively prescriptive | Create every helper method with predetermined names and line-by-line logic                                   | May constrain legitimate implementation judgment |

The Plan should be explicit about architectural decisions while leaving routine implementation choices to the Developer Agent.

**Decision:** Decompose work until significant tasks are independently understandable and verifiable, without unnecessary code-level prescription.

### **15.7 Decision Point: Which Dependencies Block Execution?**

Not every unresolved issue must prevent all implementation work.

The planning agent should determine which dependencies are mandatory prerequisites and which can be addressed later.

For corporate fleet booking, confirmation of booking-limit semantics is a hard dependency for capacity enforcement.

The final naming of an internal helper method is not.

A useful dependency classification is:

| Dependency type                    | Example                                       | Execution consequence             |
| ---------------------------------- | --------------------------------------------- | --------------------------------- |
| Business decision                  | Meaning of corporate booking limit            | Blocks dependent behavior         |
| Architectural decision             | Transaction boundary for capacity enforcement | Blocks dependent persistence work |
| Technical prerequisite             | Availability of existing policy interface     | May block integration             |
| External dependency                | Required contract from another service team   | May block dependent integration   |
| Non-blocking implementation detail | Internal helper naming                        | Can be delegated                  |
| Validation dependency              | Representative database environment           | May block final verification      |

The Harness should permit independent work only when doing so does not violate the approved planning and authorization policy.

**Decision:** Block tasks whose correctness depends on unresolved material prerequisites, while permitting authorized independent work where appropriate.

### **15.8 Decision Point: Which Concurrency Strategy Is Appropriate?**

Corporate fleet booking introduces a particularly important architectural decision.

If booking limits represent active reservations, concurrent requests must not collectively exceed the approved capacity.

The planning agent must identify the persistence mechanism capable of enforcing this invariant.

Possible strategies include optimistic concurrency, conditional updates, appropriately scoped locking, or transaction isolation mechanisms.

The selection depends on the current schema, transaction boundaries, expected contention, and operational requirements.

| Strategy                        | Key consideration                                                                |
| ------------------------------- | -------------------------------------------------------------------------------- |
| Optimistic concurrency          | Requires conflict detection and a reliable retry or rejection policy             |
| Conditional capacity update     | Requires an authoritative capacity representation                                |
| Transactional locking           | Requires appropriate lock scope and contention analysis                          |
| Serializable transaction        | Requires careful evaluation of predicates, database behavior, and retry handling |
| Dedicated capacity coordination | Introduces additional state and consistency responsibilities                     |

The planner should avoid choosing a strategy solely because it is easy for an AI Agent to generate.

The selected mechanism must preserve the business invariant under the actual persistence semantics.

**Decision:** Select the concurrency strategy based on demonstrated correctness, architectural compatibility, and operational tradeoffs.

### **15.9 Decision Point: How Should Idempotency Be Handled?**

Idempotency must be planned independently of concurrency control.

For Alpha Car Detailing, a client may retry a booking request after a network timeout.

The system must distinguish the retry of an existing logical operation from a new booking request.

The planner should determine whether the repository already provides an appropriate idempotency mechanism.

Important decisions include:

| Concern             | Required decision                                                         |
| ------------------- | ------------------------------------------------------------------------- |
| Idempotency key     | How is the operation uniquely identified?                                 |
| Key scope           | Is uniqueness defined per account, endpoint, tenant, or another boundary? |
| Request equivalence | How are repeated and conflicting payloads distinguished?                  |
| Persistence         | How is the idempotency result stored?                                     |
| Atomicity           | How is duplication prevented when requests overlap?                       |
| Retention           | How long must retry protection remain effective?                          |
| Response behavior   | What result is returned for an equivalent retry?                          |

The implementation must ensure that the idempotency guarantee is meaningful under concurrent retries and transaction failures.

**Decision:** Reuse an existing suitable idempotency mechanism or explicitly justify the required extension, with corresponding deterministic tests.

### **15.10 Decision Point: What Consistency Guarantees Are Required?**

The planning agent must identify which operations belong to the same consistency boundary.

For corporate fleet booking, the successful creation of a reservation may require changes to booking data, capacity state, idempotency records, and outbox records.

The Plan must establish which operations must commit atomically.

If integration events are required, the existing transactional outbox should be evaluated for suitability.

The architect should determine whether event publication must occur after durable transaction commit, how retries are handled, and what guarantees downstream consumers provide.

A critical distinction is that event delivery may be repeated even when the originating transaction commits only once.

Consumers must therefore preserve their required idempotency semantics.

**Decision:** Establish transactional and asynchronous consistency requirements before designing component-level implementation details.

### **15.11 Decision Point: What Validation Evidence Is Required?**

The Plan must determine how the completed implementation will be verified.

A simple build-and-test requirement is insufficient for complex enterprise behavior.

The planning agent should map acceptance criteria and identified risks to suitable validation mechanisms.

For corporate fleet booking:

| Requirement                      | Required evidence                               |
| -------------------------------- | ----------------------------------------------- |
| Successful multi-vehicle booking | API integration test                            |
| Booking-limit enforcement        | Business policy and persistence tests           |
| Concurrency safety               | Parallel-request database integration test      |
| Idempotent retries               | Duplicate-request and conflicting-payload tests |
| Transactional consistency        | Commit, rollback, and outbox tests              |
| Authorization                    | Cross-account access tests                      |
| API compatibility                | Contract and regression tests                   |
| Architecture alignment           | Dependency and architecture checks              |
| Existing behavior preservation   | Relevant regression suite                       |

The validation strategy should distinguish planned tests from completed test results.

During planning, no implementation success should be claimed.

**Decision:** Do not authorize consequential implementation without an adequate strategy for producing objective evidence of correctness.

### **15.12 Decision Point: Is Human Approval Required?**

Approval requirements should follow organizational policy and the materiality of the change.

Some decisions may fall within delegated engineering authority.

Others require explicit authorization from architects, security owners, business stakeholders, or operations teams.

For Alpha Car Detailing, business owners may need to confirm contractual booking-limit semantics.

An architect may need to approve a new persistence strategy.

Security reviewers may need to authorize changes to corporate account permissions.

The approval decision should establish:

- What is being approved.
- Which Plan revision is covered.
- Which repository baseline was considered.
- Who has authority to approve.
- Whether conditions or limitations apply.
- Which subsequent actions remain separately controlled.

Approval of the implementation Plan should not automatically authorize production deployment.

**Decision:** Require explicit, recorded approval whenever established policy assigns the decision to a human authority.

### **15.13 Decision Point: Is the Plan Ready for Execution?**

A Plan should not enter execution merely because the planning agent has finished writing it.

Execution readiness requires a distinct determination.

The Harness should evaluate whether the Plan satisfies the applicable requirements.

| Readiness criterion | Required condition                           |
| ------------------- | -------------------------------------------- |
| Goal                | Clearly defined                              |
| Repository evidence | Sufficient for material decisions            |
| Instructions        | Applicable constraints resolved              |
| Scope               | Explicit and authorized                      |
| Dependencies        | Blocking prerequisites resolved              |
| Risks               | Material risks identified and addressed      |
| Assumptions         | No unresolved execution-blocking assumptions |
| Tasks               | Executable sequence established              |
| Validation          | Required evidence defined                    |
| Approval            | Mandatory authorization recorded             |
| Repository baseline | Still valid for implementation               |
| Permissions         | Appropriate execution authority available    |

If one or more mandatory conditions are not satisfied, the task should remain in planning, review, or blocked status.

A Plan can be useful while incomplete, but it must not be mislabeled as execution-ready.

**Decision:** Transition to execution only when the Plan meets the applicable readiness and authorization conditions.

### **15.14 Decision Point: When Must Execution Return to Planning?**

New evidence may emerge during implementation.

The Developer Agent must determine whether the discovery can be handled within the approved Plan or requires a formal revision.

For example, the agent may discover that the proposed capacity enforcement approach cannot operate within the existing persistence contract.

If correcting the issue requires a different transaction strategy or a new service dependency, the change may be material.

A practical decision table is:

| Discovery                                 | Recommended action                                      |
| ----------------------------------------- | ------------------------------------------------------- |
| Minor implementation detail differs       | Continue within delegated discretion                    |
| Additional test scenario identified       | Add appropriate coverage                                |
| Existing helper abstraction is unsuitable | Assess whether a local alternative remains within scope |
| Approved transaction strategy is invalid  | Pause and re-plan                                       |
| New external service is required          | Architectural review                                    |
| Business rule changes                     | Business confirmation and Plan revision                 |
| Security permissions must expand          | Security approval                                       |
| Acceptance criterion cannot be satisfied  | Escalate; do not weaken the requirement silently        |

Re-planning should preserve the original task goal unless an authorized requirement change occurs.

**Decision:** Return to Planning Mode when new evidence materially changes the approved implementation approach, scope, or verification obligations.

### **15.15 Decision Point: Should Planning Findings Become Permanent Knowledge?**

Planning may uncover reusable engineering insights.

For example, several Alpha Car Detailing tasks may reveal that developers frequently misunderstand the existing transactional outbox integration.

The organization may benefit from improved documentation or a reusable Skill.

However, a task-specific Plan is not authoritative repository knowledge.

The architect should determine whether a finding is sufficiently general, verified, and approved for promotion.

| Finding                              | Appropriate destination               |
| ------------------------------------ | ------------------------------------- |
| Temporary implementation observation | Scratchpad                            |
| Task-specific design decision        | Plan                                  |
| Durable architectural decision       | Approved Architecture Decision Record |
| Persistent coding constraint         | Governed Instruction                  |
| Reusable implementation procedure    | Governed Skill                        |
| Verified system description          | Repository Intelligence               |
| Repeated improvement opportunity     | Harness improvement proposal          |

The Harness may recommend changes, but it should not silently modify standards.

**Decision:** Promote planning findings into permanent knowledge only through the governance process appropriate to the destination artifact.

### **15.16 Decision Point: How Much Planning Should Be Automated?**

Organizations must determine which planning decisions can be automated and which require human judgment.

Deterministic checks are suitable for verifying document structure, required fields, valid identifiers, and recorded approval states.

AI Agents can assist with repository investigation, dependency analysis, risk identification, and design alternatives.

Human reviewers remain necessary where organizational authority, material architectural judgment, or business ownership requires them.

| Planning activity                  | Suitable primary mechanism      |
| ---------------------------------- | ------------------------------- |
| Required field completeness        | Deterministic gate              |
| Repository revision identification | Deterministic tooling           |
| Repository dependency discovery    | Tool-assisted AI analysis       |
| Skill relevance assessment         | AI analysis under Instructions  |
| Architectural tradeoff assessment  | AI-supported engineering review |
| Business-rule confirmation         | Authorized business authority   |
| Security exception approval        | Authorized security authority   |
| Plan approval verification         | Harness policy enforcement      |
| Execution authorization            | Harness-controlled transition   |

The objective is not to automate every decision.

It is to automate repetitive analysis and objective checks while preserving accountability for material choices.

**Decision:** Automate what can be verified reliably, assist what requires engineering reasoning, and preserve human authority where organizational policy requires it.

### **15.17 Consolidated Planning Decision Matrix**

The following matrix summarizes the main decisions that should be resolved before and during enterprise planning.

| Decision                                    | Primary responsibility                     | Required outcome                             |
| ------------------------------------------- | ------------------------------------------ | -------------------------------------------- |
| Is formal planning required?                | Lead Agent under Harness policy            | Appropriate planning depth                   |
| Are requirements sufficiently clear?        | Lead Agent and business owner              | Confirmed or explicitly blocked requirements |
| Is repository discovery sufficient?         | Planning Agent                             | Evidence-supported understanding             |
| Which Instructions apply?                   | Planning Agent and Harness                 | Applicable constraints identified            |
| Which Skills should be used?                | Lead or Planning Agent                     | Suitable reusable workflows                  |
| What is in scope?                           | Lead Agent and task owner                  | Authorized modification boundaries           |
| Which architecture approach is appropriate? | Architect or delegated technical authority | Justified implementation design              |
| What dependencies block work?               | Planning Agent                             | Explicit prerequisites                       |
| What risks must be mitigated?               | Planning Agent and relevant specialists    | Risk-aware implementation approach           |
| What evidence proves correctness?           | Planning Agent with validation ownership   | Defined validation strategy                  |
| Is approval required?                       | Harness policy                             | Correct authorization path                   |
| Is the Plan ready for execution?            | Harness and authorized reviewers           | Controlled execution handoff                 |
| Does new evidence require re-planning?      | Developer and Lead Agents                  | Controlled drift response                    |
| Should findings become permanent knowledge? | Designated knowledge owner                 | Governed knowledge promotion                 |

### **15.18 Reference Decision Flow**

The decision-making process can be represented as a series of controlled transitions.

\#chatgpt-mermaid-\_r_1c7i\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c7i\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c7i\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c7i\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c7i\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c7i\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c7i\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c7i\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c7i\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c7i\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c7i\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c7i\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c7i\_ p{margin:0;}#chatgpt-mermaid-\_r_1c7i\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1c7i\_ .label text,#chatgpt-mermaid-\_r_1c7i\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .node rect,#chatgpt-mermaid-\_r_1c7i\_ .node circle,#chatgpt-mermaid-\_r_1c7i\_ .node ellipse,#chatgpt-mermaid-\_r_1c7i\_ .node polygon,#chatgpt-mermaid-\_r_1c7i\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .rough-node .label text,#chatgpt-mermaid-\_r_1c7i\_ .node .label text,#chatgpt-mermaid-\_r_1c7i\_ .image-shape .label,#chatgpt-mermaid-\_r_1c7i\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1c7i\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .rough-node .label,#chatgpt-mermaid-\_r_1c7i\_ .node .label,#chatgpt-mermaid-\_r_1c7i\_ .image-shape .label,#chatgpt-mermaid-\_r_1c7i\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1c7i\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1c7i\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c7i\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c7i\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1c7i\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c7i\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c7i\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c7i\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1c7i\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1c7i\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c7i\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1c7i\_ .icon-shape,#chatgpt-mermaid-\_r_1c7i\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c7i\_ .icon-shape p,#chatgpt-mermaid-\_r_1c7i\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1c7i\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1c7i\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c7i\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1c7i\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1c7i\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c7i\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c7i\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c7i\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c7i\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c7i\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c7i\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c7i\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c7i\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c7i\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1c7i\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1c7i\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .node rect,#chatgpt-mermaid-\_r_1c7i\_ .node circle,#chatgpt-mermaid-\_r_1c7i\_ .node ellipse,#chatgpt-mermaid-\_r_1c7i\_ .node polygon,#chatgpt-mermaid-\_r_1c7i\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1c7i\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1c7i\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1c7i\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1c7i\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c7i\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}#chatgpt-mermaid-\_r_1c7i\_ .decision rect{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .decision polygon{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .decision ellipse{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .decision circle{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .decision path{fill:rgb(255, 242, 213)!important;stroke:rgb(169, 121, 22)!important;color:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .decision tspan{fill:rgb(66, 46, 8)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning rect{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning polygon{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning ellipse{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning circle{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning path{fill:rgb(232, 240, 255)!important;stroke:rgb(71, 117, 186)!important;color:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .planning tspan{fill:rgb(23, 59, 114)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution rect{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution polygon{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution ellipse{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution circle{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution path{fill:rgb(229, 244, 234)!important;stroke:rgb(67, 140, 91)!important;color:rgb(33, 84, 53)!important;}#chatgpt-mermaid-\_r_1c7i\_ .execution tspan{fill:rgb(33, 84, 53)!important;}Engineering PromptFormal Planning Required?Lightweight InvestigationRepository DiscoveryAuthorized ExecutionWorkflowRequirements and EvidenceSufficient?Clarify or InvestigateDefine Scope and ArchitectureIdentify Dependencies andRisksDefine Acceptance andValidationBlocking Issues?Resolve DependenciesProposed PlanPlan Quality GatePlan Adequate?Approval Required?Authorized ReviewApproved?Execution HandoffDeveloper AgentMaterial Plan Drift?Impact AssessmentContinue ImplementationValidation and EvaluationNoYesNoYesYesNoNoYesYesNoYesNoYesNo

The diagram illustrates the intended lifecycle rather than prescribing a specific Harness implementation.

A lightweight task must still comply with applicable Instructions, permissions, validation requirements, and approval policy. The lightweight path should not be used to bypass mandatory controls.

Likewise, an execution task that encounters material drift must return to an appropriate planning and authorization state before the revised work proceeds.

### **15.19 Applying the Decision Framework to Corporate Fleet Booking**

The Alpha Car Detailing corporate fleet booking requirement demonstrates how the decisions work together.

The Lead Agent determines that formal planning is required because the change affects several components and introduces significant concurrency and data-consistency concerns.

Repository Discovery identifies the existing booking workflow, persistence abstractions, API contracts, and event-processing capabilities.

The planning agent identifies applicable Instructions and determines whether existing Skills can guide implementation.

The business owner confirms the corporate booking-limit semantics.

The architect evaluates the proposed concurrency and transaction strategies.

The Plan then defines the authorized scope, implementation sequence, dependencies, risks, and validation requirements.

The Harness checks planning completeness and routes material decisions for approval.

After approval, the Developer Agent receives the Plan and begins implementation.

If execution exposes an invalid assumption, the agent records the finding and initiates controlled re-planning.

The Reviewer Agent examines the implementation's quality and architectural alignment. The Validator Agent verifies deterministic evidence. The Evaluator Agent assesses whether the completed capability satisfies the intended business goal.

Each decision preserves a distinct responsibility.

No single agent or operating mode replaces the entire engineering process.

### **15.20 Decision Points Conclusion**

Planning Mode is effective when significant engineering decisions are made deliberately and at the appropriate level of authority.

The planning agent should investigate and propose.

The Lead Agent should coordinate.

Architects and business owners should resolve decisions within their respective authority.

The Harness should enforce workflow boundaries and approvals.

The Developer Agent should implement within the authorized scope.

The Reviewer, Validator, and Evaluator should provide distinct forms of assurance.

This separation does not prevent efficient development. It creates a reliable structure within which development can proceed rapidly without losing architectural control.

> **Decision Point**
>
> Before moving an enterprise task from Planning Mode into execution, determine whether the proposed approach is sufficiently understood, evidence-based, authorized, and verifiable.
>
> If essential business rules remain unresolved, architectural guarantees are unsupported, or required approval is absent, execution should remain blocked.
>
> If the Plan satisfies those conditions, implementation can proceed within the approved boundaries, with controlled re-planning when material evidence changes.

The central decision is not whether the AI Agent has finished planning.

**It is whether the organization has sufficient engineering evidence and authority to permit implementation to begin.**

Chapter 27 status: In progress — next section: Exercises

## **16. Exercises**

The exercises in this section reinforce the engineering principles introduced throughout Chapter 27. They are designed for software architects, senior developers, technical leads, and engineers responsible for implementing AI-assisted development workflows in enterprise environments.

Each exercise focuses on a specific aspect of Planning Mode, from repository discovery and scope definition to approval boundaries, validation requirements, and Harness integration.

The exercises use Alpha Car Detailing's corporate fleet booking capability as the primary scenario. They progress from individual planning activities to the design of a governed planning workflow within an enterprise Harness.

The objective is not to generate production code. It is to develop the ability to produce, review, and govern implementation Plans that are evidence-based, executable, and verifiable.

### **16.1 Exercise 1 — Create a Planning-Only Prompt**

**Objective:** Construct a task-specific Prompt that instructs an AI Agent to investigate a feature without beginning implementation.

**Scenario**

Alpha Car Detailing needs to introduce corporate fleet booking. The new capability must allow authorized corporate customers to reserve appointments for multiple vehicles while respecting contractual booking limits.

The team wants Claude Code to investigate the existing repository and propose an implementation approach.

**Tasks**

1. Write a Prompt that clearly describes the intended business capability.
2. Require the agent to perform Repository Discovery before making architectural recommendations.
3. Instruct the agent to identify applicable Instructions and relevant Skills.
4. Require explicit scope, dependencies, risks, assumptions, and acceptance criteria.
5. Require a validation strategy.
6. Prohibit changes to production code, configuration, and infrastructure during planning.
7. Require the agent to stop after presenting the proposed Plan.

**Expected deliverable**

A reusable planning Prompt suitable for a Claude Code Plan Mode session.

**Acceptance criteria**

The Prompt must clearly separate requested outcomes from implementation decisions. It must not assume that repository capabilities exist without investigation.

The Prompt must also establish that completion of planning does not constitute completion of the feature.

**Reflection question**

What could go wrong if the Prompt asks Claude to both investigate and implement the feature without an explicit planning boundary?

### **16.2 Exercise 2 — Conduct Evidence-Based Repository Discovery**

**Objective:** Identify the existing repository components relevant to corporate fleet booking.

**Scenario**

The development team has provided access to the Alpha Car Detailing companion application.

Before selecting an implementation approach, the planning agent must establish how the current booking workflow operates.

**Tasks**

Inspect the actual repository and identify the implementation locations for booking requests, application services, domain policies, persistence, authorization, event publication, and tests.

Record the relevant file paths and symbols.

Investigate whether the repository already includes an idempotency mechanism and transactional outbox support.

Do not assume that a component exists merely because it appears in the handbook's reference architecture.

**Expected deliverable**

A Repository Discovery report containing the following evidence register.

| Area                     | Repository evidence              | Finding            | Planning implication               |
| ------------------------ | -------------------------------- | ------------------ | ---------------------------------- |
| Booking API              | Actual file and symbol           | To be investigated | Identify contract changes          |
| Application workflow     | Actual file and symbol           | To be investigated | Determine extension point          |
| Corporate account policy | Actual source reference          | To be investigated | Define policy integration          |
| Persistence              | Actual implementation            | To be investigated | Establish transaction strategy     |
| Idempotency              | Actual implementation or absence | To be investigated | Determine retry design             |
| Event publication        | Actual implementation or absence | To be investigated | Establish consistency requirements |
| Tests                    | Actual test locations            | To be investigated | Reuse testing conventions          |

**Acceptance criteria**

Every material finding must be supported by inspected repository evidence.

Unknown capabilities must be classified as unresolved rather than invented.

The exercise must not modify application code.

**Reflection question**

How would the proposed architecture change if the repository already contains an idempotency mechanism but no transactional outbox?

### **16.3 Exercise 3 — Distinguish Prompt, Plan, and Scratchpad**

**Objective:** Classify engineering information according to its purpose and authority.

**Scenario**

During corporate fleet booking development, an AI Agent encounters several pieces of information.

Classify each item as primarily belonging to the Prompt, Plan, or Scratchpad.

| Information                                                                                  | Classification   |
| -------------------------------------------------------------------------------------------- | ---------------- |
| Corporate customers must be able to reserve multiple vehicles                                | To be determined |
| The implementation will extend the existing booking application service                      | To be determined |
| A database integration test failed during a local run                                        | To be determined |
| The feature must preserve existing individual booking behavior                               | To be determined |
| The approved approach uses the existing transactional outbox                                 | To be determined |
| A temporary investigation suggests that two requests may contend on the same capacity record | To be determined |
| The Developer Agent will implement idempotency before event integration                      | To be determined |
| A diagnostic command produced an unexpected deadlock error                                   | To be determined |

**Tasks**

Classify each statement and explain the reasoning.

Identify which observations might require promotion into a revised Plan.

Explain why the Scratchpad must not become authoritative repository knowledge.

**Expected deliverable**

A classification table with an explanation of each decision.

**Acceptance criteria**

The classification must preserve the distinction:

- Prompt: What needs to be done.
- Plan: How the task will be approached.
- Scratchpad: What is discovered and tracked during execution.

**Reflection question**

Under what circumstances should a temporary Scratchpad finding trigger a formal Plan revision?

### **16.4 Exercise 4 — Define Scope and Exclusions**

**Objective:** Establish controlled implementation boundaries.

**Scenario**

The corporate fleet booking requirement affects several components.

During discovery, the planning agent also identifies an opportunity to refactor the existing individual booking workflow and introduce a new messaging library.

The original business request does not authorize these changes.

**Tasks**

Define the implementation scope for corporate fleet booking.

Identify the required modifications to the API, application behavior, persistence, idempotency, and event integration.

Identify relevant regression and contract-validation work.

Explicitly document out-of-scope changes.

Evaluate whether introducing a new messaging library is necessary or whether the existing event infrastructure should be reused.

**Expected deliverable**

A Plan scope section containing:

| Category                   | Description                                      |
| -------------------------- | ------------------------------------------------ |
| Required modifications     | Changes directly necessary for the feature       |
| Supporting modifications   | Changes justified by implementation dependencies |
| Validation scope           | Components and behaviors requiring verification  |
| Exclusions                 | Changes not authorized by the task               |
| Approval-sensitive changes | Modifications requiring additional review        |

**Acceptance criteria**

The scope must support the business goal without introducing unrelated architectural changes.

**Reflection question**

How should the planning agent respond when a technically beneficial refactoring is outside the authorized task scope?

### **16.5 Exercise 5 — Resolve Ambiguous Business Requirements**

**Objective:** Identify and manage assumptions that materially influence implementation.

**Scenario**

The requirement states:

> Corporate customers must not exceed their booking limits.

The business request does not define how limits are calculated.

**Tasks**

Identify at least five questions that must be resolved before designing capacity enforcement.

Consider:

- Whether limits apply per request or across active reservations.
- Whether limits are account-wide or station-specific.
- How cancelled bookings affect capacity.
- How expired reservations are treated.
- Whether different corporate contracts define different limits.
- Whether pending reservations consume capacity.

Classify each question as blocking or non-blocking.

Identify the authority responsible for answering it.

**Expected deliverable**

An assumption and decision register.

| Question              | Current assumption | Impact                | Decision owner      | Blocking?   |
| --------------------- | ------------------ | --------------------- | ------------------- | ----------- |
| Booking-limit scope   | Unconfirmed        | Capacity model        | Business owner      | Yes         |
| Cancellation behavior | Unconfirmed        | Reservation lifecycle | Business owner      | Yes         |
| Capacity storage      | Requires discovery | Persistence design    | Technical architect | Potentially |

**Acceptance criteria**

No material assumption may be represented as an approved business rule.

The deliverable must distinguish technical recommendations from business decisions.

**Reflection question**

Why should an AI Agent not independently decide how cancelled bookings affect contractual capacity?

### **16.6 Exercise 6 — Decompose the Feature into Executable Tasks**

**Objective:** Transform a business requirement into an implementation sequence.

**Scenario**

The corporate booking-limit policy has been confirmed, and Repository Discovery has identified the existing application boundaries.

**Tasks**

Create a task breakdown covering API contracts, application behavior, capacity enforcement, idempotency, persistence, event consistency, authorization, and validation.

For each task, identify prerequisites, expected outputs, and verification obligations.

Determine which tasks may proceed independently and which must follow a strict sequence.

**Expected deliverable**

A dependency-aware implementation task table.

| Task                           | Dependency                        | Output                      | Verification      |
| ------------------------------ | --------------------------------- | --------------------------- | ----------------- |
| Define API contract            | Confirmed requirements            | Reviewed contract           | Contract review   |
| Implement booking policy       | Existing application architecture | Policy behavior             | Unit tests        |
| Implement capacity enforcement | Approved transaction design       | Atomic reservation behavior | Concurrency tests |
| Integrate idempotency          | Request and persistence contracts | Retry-safe behavior         | Replay tests      |
| Integrate outbox               | Transaction model                 | Consistent event record     | Integration tests |

**Acceptance criteria**

Tasks must be actionable and verifiable.

The sequence must reflect actual engineering dependencies.

**Reflection question**

What risks arise when an AI Agent parallelizes implementation tasks that depend on the same unresolved architectural decision?

### **16.7 Exercise 7 — Design a Concurrency-Safe Booking Plan**

**Objective:** Evaluate architectural alternatives for enforcing corporate booking limits.

**Scenario**

For this exercise, assume a corporate account has a confirmed capacity limit of 100 active vehicle reservations.

The account currently has 70 active reservations.

Two authorized users simultaneously submit separate requests for 20 vehicles each.

A naive implementation performs a database count and inserts new reservations when the count remains within the limit.

Both requests may observe 70 active reservations and independently pass validation.

If both transactions commit, the account reaches 110 active reservations.

**Tasks**

1. Explain why sequential validation is insufficient.
2. Identify the required capacity invariant.
3. Evaluate at least two concurrency-control strategies compatible with the existing persistence model.
4. Select a proposed strategy and justify it.
5. Define the expected behavior when competing requests cannot both succeed.
6. Design an integration test that reproduces concurrent contention.
7. Define the evidence required to demonstrate that the invariant is preserved.

**Expected deliverable**

A concurrency design decision within the implementation Plan.

**Acceptance criteria**

The proposed design must prevent committed reservations from exceeding authorized capacity.

The validation strategy must test actual competing transactions rather than only sequential requests.

**Reflection question**

Why does idempotency alone fail to prevent two distinct requests from exceeding a corporate booking limit?

### **16.8 Exercise 8 — Build an Acceptance Criteria and Validation Matrix**

**Objective:** Define verification requirements before implementation begins.

**Scenario**

The corporate fleet booking capability must satisfy functional, consistency, and security expectations.

**Tasks**

Create measurable acceptance criteria covering successful booking, booking-limit rejection, concurrent requests, idempotent retries, transaction rollback, event consistency, authorization, and backward compatibility.

Map each criterion to a validation mechanism.

Identify which checks belong to unit testing, integration testing, contract testing, security testing, and architecture validation.

**Expected deliverable**

A validation matrix.

| Acceptance criterion                            | Validation method            | Expected evidence             |
| ----------------------------------------------- | ---------------------------- | ----------------------------- |
| Valid multi-vehicle booking succeeds            | API integration test         | Successful persisted booking  |
| Capacity cannot be exceeded                     | Concurrency integration test | Invariant preserved           |
| Equivalent retry does not duplicate booking     | Idempotency integration test | Single logical booking result |
| Failed booking does not persist an outbox event | Transaction integration test | Atomic rollback               |
| Cross-account booking is rejected               | Security integration test    | Access denied                 |
| Existing booking behavior remains valid         | Regression suite             | Passing existing tests        |

**Acceptance criteria**

Every material acceptance criterion must map to suitable verification evidence.

The exercise must distinguish planned validation from actual test execution.

**Reflection question**

Why can a successful build and passing unit tests still be insufficient evidence for approving corporate fleet booking?

### **16.9 Exercise 9 — Evaluate and Revise an Incomplete Plan**

**Objective:** Perform an architectural review of an AI-generated Plan.

**Scenario**

Claude Code produces the following implementation Plan:

```
1. Create CorporateFleetBookingController.
2. Create CorporateFleetBookingService.
3. Add a database table for corporate bookings.
4. Check the corporate booking limit before saving.
5. Publish a booking event after creating a booking.
6. Add unit tests.
7. Mark the task complete.
```

**Tasks**

Review the Plan and identify its architectural deficiencies.

Determine whether it addresses repository reuse, business-rule ambiguity, transaction boundaries, concurrency, idempotency, authorization, API compatibility, and validation.

Identify unsupported architectural assumptions.

Rewrite the Plan to satisfy the chapter's planning requirements.

**Expected deliverable**

A structured Plan review containing findings, severity, required corrections, and a revised implementation approach.

**Acceptance criteria**

The revised Plan must address important business and technical risks without assuming nonexistent repository capabilities.

It must include explicit approval and execution-handoff conditions.

**Reflection question**

Why might this Plan produce a functioning demonstration while still being unsuitable for enterprise deployment?

### **16.10 Exercise 10 — Design a Controlled Plan Drift Scenario**

**Objective:** Establish a process for responding to new evidence discovered during execution.

**Scenario**

The corporate fleet booking Plan has been approved.

During implementation, the Developer Agent discovers that the current persistence abstraction cannot provide the atomic capacity enforcement required by the approved design.

The agent proposes introducing a dedicated capacity coordination component.

**Tasks**

Determine whether the proposed change is material.

Identify the evidence the Developer Agent must capture.

Define which implementation activities must pause.

Describe the Lead Agent's responsibility in coordinating a revised Plan.

Identify which reviewers should authorize the new approach.

Define how the Harness records the old and revised Plan versions.

**Expected deliverable**

A Plan Drift Assessment document containing:

| Field                | Required information                          |
| -------------------- | --------------------------------------------- |
| Original decision    | Approved capacity enforcement approach        |
| New evidence         | Why the approach cannot satisfy the invariant |
| Proposed deviation   | Alternative design                            |
| Impact               | Affected scope, dependencies, and tests       |
| Approval requirement | Relevant architectural authority              |
| Execution status     | Paused or permitted independent work          |
| Revised Plan         | New version linked to the original            |

**Acceptance criteria**

The exercise must preserve traceability and prevent unauthorized architectural expansion.

**Reflection question**

How should the Harness distinguish harmless implementation adjustments from changes requiring formal reapproval?

### **16.11 Exercise 11 — Design a Plan Quality Gate**

**Objective:** Implement the concept of deterministic planning completeness checks.

**Scenario**

The organization wants its Harness to reject incomplete Plans before they reach architectural reviewers.

**Tasks**

Define a machine-readable Plan structure containing task identity, repository baseline, scope, dependencies, assumptions, risks, implementation tasks, validation requirements, and approval status.

Identify which conditions can be checked deterministically.

Define failure messages for missing or invalid required information.

Explain which aspects of architectural correctness cannot be established through deterministic completeness checks.

**Expected deliverable**

A proposed Plan schema and gate specification.

An illustrative validation result might be:

```
{
  "planId": "PLAN-ACD-FLEET-001",
  "status": "failed",
  "checks": [
    {
      "name": "repository-baseline",
      "status": "passed"
    },
    {
      "name": "scope-definition",
      "status": "passed"
    },
    {
      "name": "validation-strategy",
      "status": "failed",
      "reason": "No concurrency validation defined"
    },
    {
      "name": "approval-record",
      "status": "pending",
      "reason": "Architectural approval required"
    }
  ]
}
```

This representation is illustrative and does not prescribe the Harness implementation format.

**Acceptance criteria**

The gate must produce reproducible results from the same structured inputs.

It must not claim that the Plan's architectural design is correct merely because required sections are present.

**Reflection question**

Which planning defects are best detected through deterministic validation, and which require independent architectural judgment?

### **16.12 Exercise 12 — Design a Planning-to-Execution Handoff**

**Objective:** Define the information required when the Lead Agent transfers an approved Plan to the Developer Agent.

**Scenario**

The enterprise Harness uses separate Lead and Developer Agent responsibilities.

The Lead Agent has completed planning and obtained the required approvals.

The Developer Agent must now implement corporate fleet booking.

**Tasks**

Design a handoff contract containing the approved Plan revision, repository baseline, applicable Instructions, selected Skills, implementation scope, task dependencies, validation requirements, and escalation conditions.

Explain how the Developer Agent should respond when repository evidence differs from the approved Plan.

Determine how implementation changes can be associated with Plan tasks.

**Expected deliverable**

A structured execution handoff specification.

**Acceptance criteria**

Another competent engineer or agent should be able to begin implementation without reconstructing essential architectural decisions.

The handoff must not grant permissions beyond those authorized by the Harness.

**Reflection question**

What information is lost when the Lead Agent merely tells the Developer Agent to "implement the feature according to the Plan" without providing a stable planning artifact?

### **16.13 Exercise 13 — Compare Planning Across Claude Code, GitHub Copilot, and Codex**

**Objective:** Distinguish vendor-specific planning mechanisms from vendor-neutral engineering requirements.

**Scenario**

An enterprise development organization uses Claude Code as its primary coding platform but wants its Harness to support GitHub Copilot and Codex.

**Tasks**

Compare the supported planning interfaces and repository Instruction mechanisms of the three platforms.

Identify which requirements must remain consistent regardless of the selected agent.

Propose a common Plan artifact that all three platforms could produce or consume.

Describe how platform-specific permission controls would map to the Harness's planning and execution boundaries.

**Expected deliverable**

A portability assessment.

| Capability              | Vendor-neutral requirement     | Platform-specific implementation   |
| ----------------------- | ------------------------------ | ---------------------------------- |
| Planning-only operation | Prevent unauthorized mutation  | Supported planning controls        |
| Repository context      | Evidence-based discovery       | Platform-specific inspection tools |
| Instructions            | Respect applicable constraints | Supported instruction mechanisms   |
| Plan artifact           | Structured executable approach | Native or Harness-managed output   |
| Approval                | Explicit authorization         | Harness approval process           |
| Execution handoff       | Preserve approved decisions    | Agent adapter                      |
| Validation              | Produce objective evidence     | Common engineering gates           |

**Acceptance criteria**

The proposed design must not depend on a specific chat interface or vendor-specific terminology for its core engineering guarantees.

**Reflection question**

Why should an enterprise standardize planning obligations rather than require every AI coding platform to provide an identical user interface?

### **16.14 Exercise 14 — Design Planning Mode Inside the Harness**

**Objective:** Extend the Harness architecture to support governed Planning Mode.

**Scenario**

The existing Harness coordinates the Lead, Developer, Reviewer, Validator, and Evaluator Agents.

The organization now wants formal planning, review, and approval before complex implementation work.

**Tasks**

Design the planning lifecycle, including task intake, repository discovery, applicable Instruction resolution, Plan creation, Plan Quality Gate, approval routing, execution handoff, and drift management.

Define the responsibilities of the Harness and each participating agent.

Identify which state transitions require authorization.

Describe how the Harness prevents a planning agent from modifying production code.

**Expected deliverable**

An architecture diagram, state-transition model, and responsibility matrix.

Use the following flow as a starting point:

\#chatgpt-mermaid-\_r_1c8o\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1c8o\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c8o\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1c8o\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1c8o\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1c8o\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1c8o\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1c8o\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1c8o\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1c8o\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c8o\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c8o\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1c8o\_ p{margin:0;}#chatgpt-mermaid-\_r_1c8o\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1c8o\_ .label text,#chatgpt-mermaid-\_r_1c8o\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .node rect,#chatgpt-mermaid-\_r_1c8o\_ .node circle,#chatgpt-mermaid-\_r_1c8o\_ .node ellipse,#chatgpt-mermaid-\_r_1c8o\_ .node polygon,#chatgpt-mermaid-\_r_1c8o\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .rough-node .label text,#chatgpt-mermaid-\_r_1c8o\_ .node .label text,#chatgpt-mermaid-\_r_1c8o\_ .image-shape .label,#chatgpt-mermaid-\_r_1c8o\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1c8o\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .rough-node .label,#chatgpt-mermaid-\_r_1c8o\_ .node .label,#chatgpt-mermaid-\_r_1c8o\_ .image-shape .label,#chatgpt-mermaid-\_r_1c8o\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1c8o\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1c8o\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c8o\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c8o\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1c8o\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c8o\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c8o\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c8o\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1c8o\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1c8o\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1c8o\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1c8o\_ .icon-shape,#chatgpt-mermaid-\_r_1c8o\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1c8o\_ .icon-shape p,#chatgpt-mermaid-\_r_1c8o\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1c8o\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1c8o\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1c8o\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1c8o\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1c8o\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1c8o\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1c8o\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1c8o\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1c8o\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c8o\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1c8o\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1c8o\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c8o\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1c8o\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1c8o\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1c8o\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .node rect,#chatgpt-mermaid-\_r_1c8o\_ .node circle,#chatgpt-mermaid-\_r_1c8o\_ .node ellipse,#chatgpt-mermaid-\_r_1c8o\_ .node polygon,#chatgpt-mermaid-\_r_1c8o\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1c8o\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1c8o\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1c8o\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1c8o\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1c8o\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}PromptRepository DiscoveryInstructions and SkillsPlanning ModeProposed PlanPlan Quality GateReview and ApprovalDeveloper AgentExecutionReview, Validation andEvaluation

The implementation should expand this simplified flow to include rejection, blocked states, material Plan drift, and re-planning.

**Acceptance criteria**

The architecture must preserve the separation between planning, implementation, review, validation, evaluation, and human authorization.

**Reflection question**

Why should Planning Mode be represented as a Harness workflow capability rather than as another permanent agent role?

### **16.15 Exercise 15 — Capstone: Produce an Enterprise-Ready Corporate Fleet Booking Plan**

**Objective:** Combine all chapter concepts into a complete planning deliverable suitable for architectural review and execution authorization.

**Scenario**

Alpha Car Detailing has approved the business objective of supporting corporate fleet booking.

You are acting as the Lead Agent's supervising architect.

Your responsibility is to direct an AI coding agent through Planning Mode and produce a complete implementation Plan.

No production code may be modified during the exercise.

**Required activities**

Conduct Repository Discovery against the actual companion application and identify existing booking components.

Read applicable Instructions, identify relevant Skills, and examine approved architecture documentation.

Confirm business-rule assumptions or clearly identify unresolved blocking decisions.

Define implementation scope and exclusions.

Evaluate transaction consistency, concurrency, idempotency, authorization, and event processing.

Decompose the work into ordered implementation tasks.

Define acceptance criteria and a validation matrix.

Establish architectural approval requirements.

Design the execution handoff.

Document how material Plan drift will be handled.

**Expected deliverables**

The completed exercise should produce four artifacts.

| Artifact                  | Purpose                                                |
| ------------------------- | ------------------------------------------------------ |
| `repository-discovery.md` | Verified repository findings and relevant dependencies |
| `implementation-plan.md`  | Proposed or approved implementation approach           |
| `validation-matrix.md`    | Acceptance criteria mapped to required evidence        |
| `execution-handoff.md`    | Controlled transition to the Developer Agent           |

These filenames are suggested exercise artifacts rather than mandatory repository conventions.

If the Harness already provides equivalent structures, those should be reused.

**Capstone acceptance criteria**

The completed Plan must satisfy the following conditions:

- The business objective is clear.
- Material architectural decisions are evidence-based.
- Applicable Instructions are respected.
- Existing capabilities are reused where appropriate.
- Scope boundaries are explicit.
- Dependencies and risks are identified.
- Assumptions are not concealed.
- Implementation tasks are actionable.
- Concurrency and idempotency are addressed independently.
- Transactional event consistency is considered.
- Validation requirements are measurable.
- Required approvals are identified.
- The Plan is associated with a repository baseline.
- The handoff can be consumed by a separate Developer Agent.
- No production code is modified during planning.

If blocking decisions remain unresolved, the capstone should report the Plan as blocked or awaiting approval rather than incorrectly declaring it execution-ready.

### **16.16 Exercise Assessment Framework**

The exercises should be assessed primarily on engineering reasoning and evidence quality, not document length.

The following scoring framework can be used for the capstone or for a peer architectural review.

| Assessment dimension          | Weight   | Evaluation focus                                             |
| ----------------------------- | -------- | ------------------------------------------------------------ |
| Repository evidence           | 20%      | Accuracy and traceability of discovery findings              |
| Requirements and scope        | 15%      | Clarity of goals, boundaries, and assumptions                |
| Architecture and dependencies | 20%      | Quality of design decisions and sequencing                   |
| Risk and failure analysis     | 15%      | Coverage of meaningful engineering risks                     |
| Validation strategy           | 15%      | Measurable acceptance and deterministic evidence             |
| Governance and handoff        | 15%      | Approval, versioning, drift control, and execution readiness |
| **Total**                     | **100%** |                                                              |

A high score should require more than complete sections.

The reviewer should examine whether the Plan's decisions are justified by the actual repository and whether the proposed validation can establish the required system behavior.

An elegant but unsupported architecture should not receive a strong assessment.

Likewise, a Plan with excellent repository discovery but no credible concurrency testing should be considered incomplete for the corporate fleet booking scenario.

### **16.17 Recommended Peer Review Activity**

After completing the capstone, exchange Plans between two engineers or teams.

The reviewing engineer should evaluate the Plan without access to the original planning conversation.

This tests whether the Plan functions as a durable handoff artifact.

The reviewer should identify ambiguities, unsupported assumptions, missing dependencies, and insufficient validation requirements.

The author should then revise the Plan based on findings.

If a significant architectural decision changes, the revision should be clearly identified.

The exercise reinforces three important enterprise principles.

First, a Plan should be independently understandable.

Second, architectural quality benefits from review rather than relying exclusively on the originating agent's reasoning.

Third, revisions should preserve the relationship between the original requirement, the proposed approach, and the resulting decisions.

### **16.18 Exercise Outcomes**

The exercises progress through four levels of engineering responsibility.

| Level                   | Exercises | Competency developed                                            |
| ----------------------- | --------- | --------------------------------------------------------------- |
| Foundation              | 1–3       | Planning boundaries, repository evidence, artifact distinctions |
| Engineering analysis    | 4–8       | Scope, assumptions, dependencies, concurrency, validation       |
| Review and governance   | 9–12      | Plan quality, drift management, gates, handoff                  |
| Enterprise architecture | 13–15     | Platform portability, Harness integration, end-to-end planning  |

By completing the exercises, the reader should be capable of producing an implementation Plan that guides an AI Agent without surrendering architectural or governance control.

The reader should also be able to distinguish a genuinely execution-ready Plan from one that merely appears detailed.

The critical learning outcome is the ability to transform an engineering request into a structured implementation approach that is grounded in repository evidence, respects established Instructions, exposes uncertainty, and defines how correctness will be established.

Planning Mode is therefore not simply a prerequisite to generating code.

It is a practical engineering discipline for ensuring that code generation begins with sufficient understanding, appropriate authority, and measurable expectations.

Chapter 27 status: In progress — next section: Interview Questions

## **17. Interview Questions**

Planning Mode is an important competency for architects, senior developers, technical leads, and engineers responsible for designing enterprise AI-assisted development workflows.

The ability to generate code with an AI Agent is only one part of enterprise AI engineering. Experienced engineers must also understand how to investigate requirements, assess architectural impact, establish implementation boundaries, manage uncertainty, and define the evidence required before a change can be accepted.

The following interview questions examine Planning Mode from conceptual, practical, architectural, and governance perspectives.

They are suitable for technical interviews, architecture discussions, internal engineering assessments, and AI engineering capability evaluations.

Each question includes the key elements expected in a strong professional answer.

### **17.1 What Is Planning Mode in AI-Assisted Software Development?**

**Question**

Explain Planning Mode and its purpose in enterprise AI-assisted software development.

**Expected answer**

Planning Mode is a deliberate phase in which an AI Agent investigates a requested engineering change and constructs an implementation approach before modifying source code or configuration.

It transforms a task-specific Prompt and repository evidence into a structured Plan.

The Plan should identify scope, dependencies, implementation tasks, risks, assumptions, acceptance criteria, validation requirements, and approval conditions.

Its purpose is to separate understanding from execution.

Planning Mode reduces the risk of premature implementation, duplicated architecture, incorrect assumptions, and incomplete validation.

It does not guarantee implementation correctness and must not be treated as evidence that the feature has been delivered.

### **17.2 What Is the Difference Between a Prompt, a Plan, and a Scratchpad?**

**Question**

Explain the responsibilities and lifecycles of these three artifacts.

**Expected answer**

A Prompt defines what needs to be accomplished.

A Plan defines how the task will be approached.

A Scratchpad records temporary observations, investigations, and intermediate findings during execution.

| Artifact   | Primary responsibility             | Lifecycle                             |
| ---------- | ---------------------------------- | ------------------------------------- |
| Prompt     | Define task intent and constraints | Task-specific                         |
| Plan       | Define the implementation approach | Planning through execution and review |
| Scratchpad | Maintain temporary working state   | Task or run scoped                    |

The Plan must not become a collection of execution logs.

The Scratchpad must not become authoritative architectural knowledge.

Material Scratchpad findings may require controlled Plan revisions, but they should not silently alter approved decisions.

### **17.3 When Should an AI Agent Enter Planning Mode?**

**Question**

Should Planning Mode be required for every code change?

**Expected answer**

Formal planning should be proportional to task complexity, uncertainty, and risk.

Small, localized changes with clear requirements and straightforward tests may need only lightweight investigation.

Formal Planning Mode is appropriate when changes affect multiple components, architectural boundaries, security, database consistency, external contracts, or significant business rules.

For Alpha Car Detailing, corporate fleet booking requires formal planning because the feature involves capacity limits, concurrent requests, idempotency, persistence, and integration events.

The goal is not to maximize planning activity but to establish sufficient understanding before implementation.

### **17.4 How Does Planning Mode Differ from the Lead Agent?**

**Question**

Is Planning Mode another agent role within the Harness?

**Expected answer**

No.

Planning Mode is an operating phase or capability.

The Lead Agent is an accountable role responsible for coordinating the task and implementation workflow.

The Lead Agent may use Planning Mode to investigate requirements, establish scope, identify dependencies, and prepare a proposed implementation Plan.

The Harness enforces permissions, workflow states, and approval requirements.

A Developer Agent or Architect Agent may also use planning capabilities when appropriate.

Modes determine how work is performed. Roles determine responsibility and accountability.

### **17.5 Why Must Repository Discovery Precede Detailed Planning?**

**Question**

What risks arise when an AI Agent prepares an implementation Plan without inspecting the repository?

**Expected answer**

Without Repository Discovery, an agent may rely on imagined architecture or familiar patterns rather than actual implementation evidence.

It may propose components that already exist, introduce unnecessary abstractions, violate dependency boundaries, or overlook integration contracts.

Repository Discovery establishes current application structure, existing capabilities, relevant dependencies, and validation conventions.

A strong Plan should reference actual files, interfaces, tests, and approved architectural documentation.

Where repository evidence is unavailable, the agent should identify the limitation rather than inventing implementation details.

### **17.6 How Should Applicable Instructions Influence Planning?**

**Question**

What should an AI Agent do when its preferred implementation approach conflicts with repository Instructions?

**Expected answer**

The agent must respect applicable Instructions and their established authority.

Instructions may define architecture boundaries, coding conventions, dependency restrictions, security requirements, or validation obligations.

If the proposed implementation conflicts with an applicable Instruction, the agent should revise its approach.

If the Instructions themselves conflict or require clarification, the issue should be escalated through the authorized process.

Planning Mode does not permit an agent to override persistent engineering standards.

A Plan is subordinate to applicable Instructions and authorized task constraints.

### **17.7 What Role Do Skills Play During Planning?**

**Question**

Should an AI Agent execute every potentially relevant Skill while preparing a Plan?

**Expected answer**

No.

Planning should identify which Skills are relevant and determine when they should be applied during implementation.

Skills provide reusable engineering procedures, such as introducing an API, implementing idempotency, or adding integration tests.

The planner must verify that the Skill is compatible with the current repository.

Selection does not automatically authorize execution.

A Skill may describe a reusable implementation pattern, but it does not establish that the required components already exist.

The Plan should identify selected Skills and their intended use without performing unauthorized source modifications.

### **17.8 How Do You Define Implementation Scope During Planning?**

**Question**

What is the difference between modification scope, dependency scope, and validation scope?

**Expected answer**

Modification scope identifies the components the agent is authorized to change.

Dependency scope identifies systems, interfaces, services, or decisions required for the implementation.

Validation scope identifies the components and behaviors that must be checked to establish correctness.

These scopes may differ.

A corporate fleet booking change may modify one booking service while depending on a corporate policy service and requiring regression validation of several downstream consumers.

A strong Plan defines in-scope and out-of-scope work and prevents unrelated architectural changes from being introduced without authorization.

### **17.9 How Should an AI Agent Handle Ambiguous Business Requirements?**

**Question**

The business requirement says, "Corporate customers must not exceed their booking limits." What should the planning agent do?

**Expected answer**

The agent should identify the ambiguity and determine which decisions are required before implementation.

It must establish whether limits apply per request, per account, per station, or within a contractual period.

It should also investigate cancellation, expiration, and concurrent reservation behavior.

The agent may propose interpretations but should not present them as confirmed business facts.

Material assumptions must be documented and resolved by the appropriate authority.

Tasks dependent on unresolved business decisions should remain blocked.

### **17.10 What Makes an Implementation Plan Executable?**

**Question**

How would you distinguish an actionable engineering Plan from a generic task list?

**Expected answer**

An executable Plan identifies concrete engineering tasks, dependencies, outputs, relevant constraints, and verification obligations.

For example, "implement booking service" is too broad.

A stronger task is to extend the existing booking workflow to enforce the approved corporate capacity invariant using the selected persistence strategy, with corresponding integration and concurrency tests.

An executable Plan should be understandable by another competent engineer or AI Agent without reconstructing essential decisions.

It should provide enough direction for implementation while leaving routine code-level choices to the Developer Agent.

### **17.11 How Should a Plan Address Technical Dependencies?**

**Question**

Why is dependency analysis essential before task decomposition and execution?

**Expected answer**

Dependencies determine which tasks can proceed independently and which require prior decisions or implementation outputs.

They may be technical, architectural, business-related, or external.

For example, capacity enforcement cannot be finalized before corporate booking-limit semantics are confirmed.

Similarly, transaction-level integration testing depends on the selected persistence strategy.

A Plan that ignores dependencies may cause agents to implement incompatible components, introduce rework, or proceed using unresolved assumptions.

The planning process should identify hard dependencies, soft dependencies, and external prerequisites.

### **17.12 How Would You Plan Concurrency-Safe Corporate Booking?**

**Question**

A corporate account permits 100 active vehicle reservations and currently has 70. Two users simultaneously request 20 additional reservations each. How should Planning Mode address this scenario?

**Expected answer**

The Plan must establish the invariant that committed active reservations cannot exceed the corporate limit.

A simple count-then-insert implementation is insufficient because both requests may observe the same available capacity before either commits.

The planner should examine the actual persistence model and evaluate suitable concurrency-control mechanisms.

Potential approaches include optimistic concurrency, conditional database updates, or appropriately isolated transactions.

The selected approach must be justified using repository evidence.

The validation strategy should include a concurrent integration test that verifies the invariant under competing transactions.

The agent should not select a mechanism solely because it is easy to generate.

### **17.13 What Is the Difference Between Idempotency and Concurrency Control?**

**Question**

Can an idempotency mechanism alone guarantee corporate booking limits?

**Expected answer**

No.

Idempotency prevents repeated processing of the same logical request according to defined key and request-equivalence semantics.

Concurrency control ensures that distinct requests do not collectively violate shared business invariants.

Two separate corporate booking requests may use different valid idempotency keys while competing for the same capacity.

Therefore, the Plan must address retry safety and capacity enforcement independently.

It must also consider atomicity between idempotency records and booking persistence where required.

### **17.14 Why Should Transaction Boundaries Be Established During Planning?**

**Question**

Why should an architect examine transaction boundaries before designing detailed application components?

**Expected answer**

Transaction boundaries determine which operations must succeed or fail together.

For corporate fleet booking, reservation persistence, capacity consumption, idempotency state, and outbox recording may need coordinated transactional behavior.

If the Plan does not identify these requirements, an agent may generate components that appear correct individually but produce inconsistent system state during failures.

Planning should establish the required consistency guarantees before selecting implementation details.

The specific mechanism should be compatible with the existing architecture and verified through appropriate integration tests.

### **17.15 How Should Planning Address Transactional Outbox Integration?**

**Question**

What should the planner investigate before recommending a new event-publication mechanism?

**Expected answer**

The planner should inspect the repository to determine whether a transactional outbox already exists and whether it satisfies the booking workflow's requirements.

It should examine how booking data and outbox records are persisted, how publication retries are handled, and whether downstream consumers support idempotent processing.

A successful booking transaction should preserve the required consistency between committed data and the corresponding event record.

The agent should not introduce a competing event-publication mechanism without architectural justification.

The Plan should identify suitable transaction, rollback, event-delivery, and retry tests.

### **17.16 How Do Acceptance Criteria Differ from Implementation Tasks?**

**Question**

Explain why acceptance criteria and task decomposition must remain distinct.

**Expected answer**

Implementation tasks describe the work to be performed.

Acceptance criteria describe the observable conditions that must hold for the delivered capability to be accepted.

For example:

**Implementation task:** Integrate the existing idempotency mechanism into corporate fleet booking.

**Acceptance criterion:** Repeated equivalent requests using the same idempotency key must not create additional reservations.

The distinction matters because completing a task does not necessarily prove the expected behavior.

Acceptance criteria should be established before implementation and mapped to appropriate verification evidence.

### **17.17 What Is the Difference Between Planning, Validation, and Evaluation?**

**Question**

Explain how these activities cooperate without duplicating responsibility.

**Expected answer**

Planning determines the implementation approach and the evidence that will be required.

Validation verifies objective, repeatable engineering conditions through builds, tests, static analysis, architecture checks, and other deterministic gates.

Evaluation assesses whether the completed implementation satisfies the intended goal, acceptance criteria, and broader quality expectations.

The Plan may require a concurrency test, but it does not execute that test.

The Validator Agent examines the actual result.

The Evaluator Agent assesses whether the demonstrated behavior satisfies the business objective.

Planning, validation, and evaluation are complementary activities with distinct responsibilities.

### **17.18 What Is a Plan Quality Gate?**

**Question**

What can a deterministic Plan Quality Gate verify, and what remains outside its authority?

**Expected answer**

A deterministic Plan Quality Gate can verify structural requirements.

Examples include the presence of required fields, valid identifiers, repository baseline information, declared scope, validation requirements, and recorded approval status.

It can also detect certain inconsistencies in structured planning data.

However, it cannot establish architectural correctness merely by checking document completeness.

A Plan may include a concurrency section while recommending an unsafe design.

Substantive architectural assessment requires evidence-based technical judgment.

The Plan Quality Gate should therefore complement architectural review rather than replace it.

### **17.19 How Should Human Approval Be Integrated into Planning?**

**Question**

Why is a completed Plan not automatically authorized for execution?

**Expected answer**

Planning completeness and execution authorization are separate conditions.

A Plan may propose changes requiring approval from architects, security owners, business stakeholders, or operations teams.

The Harness should determine which approvals are required according to organizational policy.

Approval should be associated with a specific Plan revision and relevant repository baseline.

An AI Agent must not approve its own material changes unless explicitly operating within delegated authority that permits the particular decision.

An interactive transition from Plan Mode to execution mode does not inherently satisfy organizational approval requirements.

### **17.20 What Is Plan Drift?**

**Question**

Define Plan drift and explain when it requires re-planning.

**Expected answer**

Plan drift occurs when actual implementation diverges from the approved approach.

Minor internal implementation adjustments may remain within delegated authority.

Material deviations may affect architecture, security, consistency guarantees, external dependencies, scope, or acceptance criteria.

For example, introducing a new persistence mechanism after the approved Plan required reuse of an existing abstraction may constitute material drift.

The Developer Agent should document the discovery, assess the impact, and escalate when necessary.

The Lead Agent coordinates revised planning, while the Harness enforces required authorization before affected execution resumes.

### **17.21 Should an Approved Plan Be Immutable?**

**Question**

How should an enterprise balance architectural control with adaptation to new evidence?

**Expected answer**

An approved Plan should be versioned and controlled, but it should not be treated as an immutable prediction.

Execution may reveal information that invalidates earlier assumptions.

The organization should permit controlled revision while preserving approval history and decision rationale.

Minor implementation adjustments may not require reapproval.

Material changes should trigger impact assessment and an updated Plan.

The goal is to preserve accountability without preventing responsible adaptation.

### **17.22 How Do You Prevent Planning from Modifying the Repository?**

**Question**

Is a natural-language instruction such as "do not change files" sufficient for enterprise planning?

**Expected answer**

No.

Natural-language Instructions help communicate the expected behavior, but they are not a complete permission boundary.

A strict planning environment should use technical controls such as read-only repository access, restricted tools, isolated workspaces, and controlled external integrations.

Potentially mutating commands should not be permitted merely because they are presented as investigation steps.

The Harness should establish separate planning and execution permissions.

This is particularly important for unattended AI Agents and repositories containing sensitive systems or infrastructure configuration.

### **17.23 How Should Planning Work When Multiple Agents Are Involved?**

**Question**

What challenges arise when a Lead Agent coordinates several Developer Agents?

**Expected answer**

Multi-agent execution introduces dependencies, shared modification scope, integration conflicts, and coordination requirements.

Planning should identify tasks that can safely proceed independently and tasks that require sequential execution.

Shared contracts and ownership boundaries should be established before parallel implementation.

The Lead Agent coordinates task allocation and dependency resolution.

The Harness maintains the approved Plan, task states, and evidence.

Parallel execution should not be introduced merely because multiple agents are available.

The architecture must ensure that their work remains compatible and authorized.

### **17.24 How Should Planning Handle Changes Across Multiple Repositories?**

**Question**

A corporate booking feature requires changes in separate API, contract, and consumer repositories. How should the Plan address this?

**Expected answer**

The Plan should identify all relevant repositories and their ownership boundaries.

It should record the applicable baseline for each repository and identify shared contracts, dependencies, and integration requirements.

The implementation sequence should account for compatibility and deployment ordering.

The Harness should not grant access to every repository automatically.

Modification permissions must remain scoped to authorized changes.

Validation should include relevant cross-repository contract and integration checks.

### **17.25 How Would You Compare Planning in Claude Code, GitHub Copilot, and Codex?**

**Question**

What should remain consistent across these platforms?

**Expected answer**

The platforms provide different planning interfaces, Instructions mechanisms, and execution controls.

Claude Code supports a planning permission mode. GitHub Copilot provides planning capabilities through supported agent workflows. Codex supports planning-oriented operation and execution-plan conventions.

Despite implementation differences, enterprise planning obligations should remain consistent.

Every Plan should identify the goal, repository evidence, applicable Instructions, scope, tasks, dependencies, risks, assumptions, acceptance criteria, validation requirements, and approval conditions.

The Harness should standardize these obligations independently of the chosen coding platform.

### **17.26 How Should a Plan Be Handed Off to the Developer Agent?**

**Question**

What information must accompany an approved Plan during execution handoff?

**Expected answer**

The handoff should include the task identity, approved Plan revision, repository baseline, applicable Instructions, selected Skills, modification scope, implementation tasks, dependencies, validation requirements, and escalation conditions.

The Developer Agent should understand which decisions are approved and which implementation details remain delegated.

The handoff must preserve traceability between planned work and actual changes.

It must not independently grant permissions beyond those authorized by the Harness.

### **17.27 How Can You Determine Whether a Plan Is Ready for Execution?**

**Question**

What conditions should be satisfied before the Harness authorizes implementation?

**Expected answer**

The Plan should have a clear goal, sufficient repository evidence, resolved applicable Instructions, explicit scope, executable tasks, identified dependencies, and a credible validation strategy.

Material assumptions must be resolved or explicitly accepted within authorized policy.

Required human approvals must be recorded.

The repository baseline must remain suitable for execution.

The Developer Agent must have the appropriate permissions.

If essential conditions remain unresolved, the task should remain in planning, review, or blocked status.

### **17.28 How Should Planning Findings Influence Harness Memory?**

**Question**

Should the Harness automatically convert completed Plans into permanent engineering standards?

**Expected answer**

No.

A Plan contains task-specific decisions that may not apply to other systems or future repository revisions.

Scratchpad observations are even less authoritative.

The Harness may analyze recurring planning difficulties and recommend improvements to Instructions, Skills, Knowledge Sources, or architectural documentation.

Such recommendations must follow the relevant governance process.

The Harness should not silently change established engineering standards based on individual planning outcomes.

### **17.29 How Would You Measure Planning Effectiveness?**

**Question**

What metrics would help an enterprise evaluate whether Planning Mode improves AI-assisted development?

**Expected answer**

Useful metrics include material Plan drift, unresolved blockers at execution start, planning-related rework, missed dependency frequency, acceptance-criteria validation coverage, and unapproved scope deviations.

These metrics require contextual interpretation.

A high number of Plan revisions may reflect weak initial planning or healthy architectural review.

A low number of reported deviations may indicate strong planning or inadequate reporting.

The organization should evaluate planning by its contribution to engineering quality, delivery reliability, and reduced preventable rework rather than by Plan length or production speed alone.

### **17.30 Scenario-Based Architecture Question**

**Question**

An AI Agent receives the following Prompt:

> Implement corporate fleet booking for Alpha Car Detailing. Reuse existing architecture, support retries, and enforce booking limits.

The agent inspects the repository and proposes:

1. Creating a new corporate booking controller.
2. Introducing a dedicated booking database.
3. Adding a custom idempotency mechanism.
4. Publishing events directly after database insertion.
5. Adding unit tests.
6. Beginning implementation immediately.

As the solution architect, how would you evaluate this Plan?

**Expected answer**

The Plan should not be approved in its current form.

First, the agent has not demonstrated why a new database is necessary or whether it conflicts with the existing architecture.

Second, it has not established whether suitable idempotency functionality already exists.

Third, direct event publication may introduce transactional inconsistency.

Fourth, the Plan does not define how corporate booking limits are enforced under concurrency.

Fifth, the validation strategy is insufficient because unit tests alone may not establish transaction, concurrency, and integration guarantees.

The Plan also lacks explicit acceptance criteria, dependency analysis, scope exclusions, and approval conditions.

A revised Plan should investigate the existing architecture, establish business invariants, justify persistence decisions, define required validation evidence, and obtain appropriate approval before execution.

### **17.31 Scenario-Based Governance Question**

**Question**

A corporate fleet booking Plan has been approved by the architect.

During execution, the Developer Agent discovers that the existing transaction strategy cannot enforce booking limits safely.

The agent proposes introducing a new distributed coordination service and implementing it without requesting approval.

Is this acceptable?

**Expected answer**

Not automatically.

Introducing a distributed coordination service is a material architectural change involving new infrastructure, operational responsibilities, and potentially additional failure modes.

The Developer Agent should document the finding and supporting evidence.

The Lead Agent should coordinate evaluation of available alternatives.

The Plan should be revised if the approved approach is no longer suitable.

Required architectural and operational approvals must be obtained before implementing the new dependency.

The agent may continue independent work only if permitted by the Harness and the approved scope.

The discovery demonstrates why Planning Mode must support controlled revision rather than unrestricted implementation changes.

### **17.32 Scenario-Based Harness Design Question**

**Question**

Design a Harness workflow that prevents source-code modification until an implementation Plan has passed required checks and approvals.

**Expected answer**

The Harness should establish separate planning and execution states with distinct permissions.

Planning should operate using approved read-oriented tools and repository access.

The planning agent produces a structured Plan referencing the task and repository baseline.

A deterministic Plan Quality Gate checks mandatory structural requirements.

Architectural review assesses substantive design quality.

The approval gateway verifies required authorization.

Only then does the Harness transition the task into an execution-capable state and provide the Developer Agent with the approved Plan.

Material drift should trigger reassessment and, where required, reapproval.

The workflow should record state transitions, approval decisions, and relevant evidence.

A strong design must prevent the planning agent from granting its own execution permissions.

### **17.33 Scenario-Based Validation Question**

**Question**

A corporate fleet booking implementation passes compilation, unit tests, and API contract tests.

The Developer Agent reports the feature as complete.

However, no concurrency tests have been executed.

Should the task be accepted?

**Expected answer**

Not if concurrency safety is a required acceptance condition.

Compilation and unit tests cannot establish that competing transactions preserve the corporate capacity invariant.

The Plan should have identified concurrency validation as mandatory.

The Validator Agent must verify the required deterministic evidence.

Until that evidence exists, validation remains incomplete.

The Evaluator Agent should not treat the business goal as satisfied when a critical invariant has not been adequately verified.

The task should remain pending validation or be classified according to the organization's defined incomplete-evidence status.

### **17.34 Scenario-Based Planning Efficiency Question**

**Question**

A development team requires a ten-page implementation Plan and architectural approval for every change, including minor documentation fixes.

What is wrong with this policy?

**Expected answer**

The policy applies planning controls without considering task risk or complexity.

It introduces unnecessary overhead, slows development, and may encourage engineers to bypass the process.

Planning requirements should be proportionate to uncertainty and potential consequences.

Low-risk changes may require lightweight investigation and focused validation.

Consequential architectural or security changes should receive stronger review.

The objective is consistent engineering assurance, not uniform planning ceremony.

### **17.35 Scenario-Based Repository Intelligence Question**

**Question**

An AI Agent finds a previous corporate booking Plan that recommends a particular concurrency mechanism.

Can it treat that Plan as the current architectural standard?

**Expected answer**

No.

The previous Plan is task-specific and may refer to an older repository baseline.

The agent should inspect the current implementation and applicable authoritative sources.

If the earlier Plan contains useful information, it may be treated as historical context, subject to verification.

Persistent architecture standards should be established through approved Instructions, Architecture Decision Records, or other governed Knowledge Sources.

The agent must not promote historical planning decisions into repository-wide authority without appropriate review.

### **17.36 Interview Assessment Guidance**

Interviewers should evaluate more than terminology.

A candidate may correctly define Planning Mode while still lacking the architectural judgment required to use it effectively.

Strong candidates should demonstrate the ability to:

- Distinguish task intent from implementation approach.
- Ground architectural decisions in repository evidence.
- Recognize ambiguous business requirements.
- Identify consistency, concurrency, and integration risks.
- Separate planning from execution, review, validation, and evaluation.
- Define measurable acceptance criteria.
- Establish appropriate approval boundaries.
- Explain when re-planning is necessary.
- Design controlled handoffs between agents.
- Apply planning consistently across different AI coding platforms.

The strongest responses recognize that AI-generated Plans are proposals requiring appropriate evidence and authority.

They also recognize that formal planning is not equally necessary for every task.

The engineering objective is to achieve the right level of understanding and control for the consequences of the work.

### **17.37 Suggested Interview Evaluation Matrix**

| Competency           | Foundational                      | Proficient                                           | Advanced                                                  |
| -------------------- | --------------------------------- | ---------------------------------------------------- | --------------------------------------------------------- |
| Planning concepts    | Defines Planning Mode             | Distinguishes related artifacts and responsibilities | Designs enterprise planning boundaries                    |
| Repository evidence  | Recognizes need for discovery     | Grounds Plans in inspected implementation            | Manages evidence quality and baseline changes             |
| Architecture         | Identifies affected components    | Evaluates dependencies and tradeoffs                 | Protects invariants and consistency boundaries            |
| Validation           | Recommends tests                  | Maps acceptance criteria to deterministic evidence   | Designs validation obligations across agents and services |
| Governance           | Understands approval requirements | Applies role and permission boundaries               | Designs Harness-controlled authorization                  |
| Plan evolution       | Understands Plans can change      | Identifies material drift                            | Establishes versioning and reapproval policies            |
| Platform portability | Recognizes multiple tools         | Explains tool-specific differences                   | Defines vendor-neutral planning contracts                 |

The matrix is an assessment aid rather than a formal certification standard.

A candidate for a senior architectural role should demonstrate advanced reasoning in architecture, verification, and governance, even when specific AI coding platform details are unfamiliar.

### **17.38 Interview Questions Conclusion**

Planning Mode sits at the intersection of requirements analysis, repository understanding, architectural decision-making, implementation coordination, and governance.

For an enterprise AI engineer, competence in Planning Mode means more than knowing how to activate a planning feature in Claude Code.

It means understanding how to transform a requested business outcome into an implementation approach that is technically defensible, executable, verifiable, and appropriately authorized.

The corporate fleet booking scenario illustrates why this distinction matters.

An AI Agent may generate an API endpoint quickly, but delivering a reliable enterprise booking capability requires understanding contractual limits, concurrency, idempotency, transaction boundaries, event consistency, authorization, and regression behavior.

Planning creates the opportunity to address these concerns before they become implementation defects.

> **Architect's Note**
>
> When interviewing engineers for AI-assisted development roles, evaluate their ability to challenge an AI-generated Plan rather than merely their ability to produce one.
>
> A mature enterprise AI engineer should recognize unsupported assumptions, unsafe architectural decisions, missing validation requirements, and unauthorized scope changes before execution begins.

**The defining competency is not how quickly an engineer can move from Prompt to code, but how reliably that engineer can determine when the proposed implementation is ready to begin.**

Chapter 27 status: In progress — next section: Chapter Summary

## **18. Chapter Summary**

Planning Mode establishes a deliberate boundary between understanding an engineering task and executing its implementation.

In enterprise AI-assisted software development, this distinction is essential. AI coding agents can generate substantial amounts of code quickly, but implementation speed does not guarantee that business requirements have been understood, architectural constraints have been respected, or critical risks have been addressed.

A reliable engineering workflow must establish sufficient understanding before repository modifications begin.

Throughout this chapter, Planning Mode has been presented as an engineering discipline supported by AI coding platforms and governed by the enterprise Harness.

The fundamental principle is:

> **Planning separates understanding from execution. The agent determines scope, dependencies, risks, implementation steps, and validation requirements before modifying the repository.**

### **18.1 Planning Mode Transforms Intent into an Implementation Approach**

A Prompt expresses what needs to be accomplished.

Planning Mode investigates that request in the context of the existing repository and produces an implementation Plan.

The Plan identifies the proposed approach, the components that may require modification, the dependencies between tasks, and the evidence necessary to demonstrate correctness.

The relationship is straightforward:

**Prompt:** What needs to be done.

**Plan:** How the task will be approached.

**Scratchpad:** What is discovered and tracked while the task is being executed.

These artifacts must retain their distinct responsibilities.

A Prompt does not replace architectural analysis. A Plan does not become authoritative repository knowledge. A Scratchpad does not independently authorize changes to the approved approach.

Maintaining these distinctions supports traceability, reviewability, and controlled execution.

### **18.2 Repository Evidence Is the Foundation of Reliable Planning**

An implementation Plan should be based on the actual repository rather than the agent's assumptions about how an application is likely to be structured.

Repository Discovery identifies existing application boundaries, services, contracts, persistence mechanisms, dependencies, and tests.

Applicable Instructions establish persistent engineering constraints.

Skills provide reusable implementation procedures.

Steering Notes and Knowledge Sources contribute additional task context.

Planning combines these inputs into an implementation approach suited to the existing system.

The agent must distinguish confirmed evidence from assumptions and unresolved questions.

When important information is missing, the correct response is to document the uncertainty and determine whether an authorized decision is required.

A plausible architecture is not necessarily the architecture implemented in the repository.

### **18.3 Planning Must Remain Separate from Execution**

Planning Mode is a non-mutating engineering phase.

Its purpose is to investigate, analyze, evaluate alternatives, and propose an implementation approach.

It must not begin modifying production code or configuration while planning remains active.

The boundary should be reinforced through tool permissions, workspace isolation, and Harness-controlled authorization.

A natural-language instruction not to modify files is useful, but it does not replace enforceable permission controls.

The transition from planning to execution should occur only when the Plan satisfies the applicable readiness requirements and required approvals have been recorded.

Planning completion means that an approach is ready for execution.

It does not mean the requested feature has been implemented.

### **18.4 Planning Is a Capability, Not an Agent Role**

The chapter distinguished Planning Mode from the Lead Agent's responsibilities.

The Lead Agent coordinates the engineering task, establishes implementation direction, and manages relationships among participating agents.

Planning Mode provides the controlled operating phase in which the required investigation and design activities occur.

The Harness orchestrates the process and enforces workflow boundaries.

The Developer Agent implements the authorized approach.

The Reviewer Agent examines implementation quality and architectural alignment.

The Validator Agent verifies objective, deterministic evidence.

The Evaluator Agent assesses whether the delivered implementation satisfies the intended goal and acceptance criteria.

These responsibilities are complementary.

Planning does not replace development, review, validation, evaluation, or human approval.

### **18.5 An Executable Plan Requires More Than a Task List**

A useful Plan must establish how the work can be performed and verified.

Its essential elements include:

| Element                 | Purpose                                          |
| ----------------------- | ------------------------------------------------ |
| Goal                    | Define the intended outcome                      |
| Repository baseline     | Establish the inspected code state               |
| Evidence                | Support architectural findings                   |
| Applicable Instructions | Preserve engineering constraints                 |
| Scope                   | Define authorized and excluded changes           |
| Tasks                   | Establish executable implementation activities   |
| Dependencies            | Identify prerequisites and sequencing            |
| Risks                   | Expose possible failure conditions               |
| Assumptions             | Make uncertainty explicit                        |
| Acceptance criteria     | Define observable success                        |
| Validation strategy     | Identify required verification evidence          |
| Approval requirements   | Establish authorization conditions               |
| Execution handoff       | Transfer the approved approach to implementation |

The Plan should be sufficiently detailed to guide another competent engineer or AI Agent without reconstructing essential decisions.

However, it should not prescribe unnecessary code-level details or become a collection of temporary observations.

Planning quality is measured by engineering clarity and decision value, not document length.

### **18.6 Alpha Car Detailing Demonstrates the Importance of Planning**

The corporate fleet booking scenario illustrated why apparently straightforward requirements may conceal significant architectural complexity.

Allowing a corporate customer to book multiple vehicles requires more than introducing an API endpoint.

The implementation must respect approved corporate booking limits, handle concurrent requests, prevent duplicate processing, preserve transaction consistency, enforce authorization, and maintain compatibility with existing booking behavior.

Planning must establish the relevant business invariants before selecting implementation mechanisms.

For example, when a corporate limit applies to active vehicle reservations, the system must prevent concurrent transactions from collectively exceeding authorized capacity.

Idempotency and concurrency control must be addressed independently.

Idempotency protects against repeated processing of the same logical request.

Concurrency control protects shared business invariants when distinct requests compete for limited capacity.

Transactional outbox integration introduces another consistency concern: a booking event must remain consistent with the committed booking transaction.

The Plan should identify existing repository capabilities that can satisfy these requirements before recommending new infrastructure or abstractions.

This example reinforces the importance of planning around business invariants and transaction boundaries rather than beginning with class structures and endpoints.

### **18.7 Validation Must Be Designed Before Implementation**

Planning should establish how the completed change will be verified.

Validation is not an activity added only after implementation.

Acceptance criteria should map to appropriate deterministic evidence.

For corporate fleet booking, this includes unit tests for business policies, integration tests for persistence, concurrency tests for booking limits, idempotency tests for retries, contract tests for API behavior, and authorization tests for corporate account boundaries.

A successful build does not establish business correctness.

Passing unit tests does not necessarily establish transaction safety.

An implementation should not be considered validated until its required engineering checks have been executed and the evidence has been assessed.

The Plan defines the validation obligations.

The Validator Agent verifies the actual evidence.

The Evaluator Agent assesses whether the implementation fulfills the intended outcome.

### **18.8 Plans Must Be Adaptable but Governed**

A Plan is based on the best available evidence at the time of planning.

Execution may expose new information that invalidates an assumption or architectural decision.

The initial Plan must therefore remain revisable.

However, revision should not become an excuse for uncontrolled implementation changes.

Minor implementation adjustments may remain within the Developer Agent's delegated authority.

Material changes affecting architecture, security, data consistency, scope, external dependencies, or acceptance criteria should trigger impact assessment and appropriate reapproval.

The Harness should retain Plan revisions and their relationship to the authorized implementation.

This provides adaptability without sacrificing accountability.

The principle is not to follow the initial Plan regardless of evidence.

It is to preserve controlled engineering decision-making as evidence changes.

### **18.9 Human Approval Is an Explicit Authority Boundary**

A technically complete Plan is not necessarily authorized for execution.

Some changes require approval from architects, business owners, security authorities, or operations teams.

For Alpha Car Detailing, confirmation of contractual booking-limit semantics may belong to a business owner.

A changed persistence consistency strategy may require architectural approval.

Expanded corporate account permissions may require security review.

Approval must be associated with an identifiable Plan revision and relevant repository baseline.

The planning agent must not grant itself authority to make changes beyond its delegated permissions.

Similarly, approval of implementation does not automatically authorize production deployment.

The Harness should distinguish planning approval, implementation authorization, merge approval, and operational deployment controls according to organizational policy.

### **18.10 Planning Should Be Proportional to Risk**

Formal planning is valuable, but excessive planning can reduce engineering efficiency.

A localized change with clear requirements and focused tests may require only lightweight investigation.

A change involving distributed transactions, authorization, public contracts, or production infrastructure requires more extensive planning.

The organization should establish planning thresholds based on complexity, uncertainty, and potential consequences.

The objective is neither to eliminate planning nor to require elaborate documentation for every task.

It is to apply sufficient engineering discipline to the risks involved.

### **18.11 Planning Mode Must Be Vendor-Neutral in Principle**

The chapter examined Planning Mode through Claude Code, GitHub Copilot, and OpenAI Codex.

These platforms provide different interfaces, repository Instruction mechanisms, and execution controls.

However, the underlying engineering requirements remain consistent.

Each platform should support an approach in which the agent investigates the task, identifies relevant repository evidence, respects applicable Instructions, constructs a verifiable Plan, and observes required authorization boundaries.

Claude Code provides the chapter's primary practical implementation.

GitHub Copilot and Codex demonstrate how the same engineering principles can be applied through alternative tooling.

The enterprise should standardize the planning contract rather than depend on one vendor's interface.

This enables different agents to participate in the same Harness architecture without changing the organization's engineering responsibilities.

### **18.12 Planning Mode Is a First-Class Harness Capability**

Within the enterprise Harness, planning should be represented as an explicit workflow capability.

The Harness coordinates task intake, repository discovery, Instruction and Skill resolution, Plan creation, completeness checks, review, approval, and execution handoff.

It also manages Plan freshness, material drift, and re-planning.

A reference lifecycle is:

\#chatgpt-mermaid-\_r_1cb1\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_1cb1\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1cb1\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_1cb1\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_1cb1\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_1cb1\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_1cb1\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_1cb1\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_1cb1\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_1cb1\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1cb1\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1cb1\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_1cb1\_ p{margin:0;}#chatgpt-mermaid-\_r_1cb1\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_1cb1\_ .label text,#chatgpt-mermaid-\_r_1cb1\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .node rect,#chatgpt-mermaid-\_r_1cb1\_ .node circle,#chatgpt-mermaid-\_r_1cb1\_ .node ellipse,#chatgpt-mermaid-\_r_1cb1\_ .node polygon,#chatgpt-mermaid-\_r_1cb1\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .rough-node .label text,#chatgpt-mermaid-\_r_1cb1\_ .node .label text,#chatgpt-mermaid-\_r_1cb1\_ .image-shape .label,#chatgpt-mermaid-\_r_1cb1\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_1cb1\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .rough-node .label,#chatgpt-mermaid-\_r_1cb1\_ .node .label,#chatgpt-mermaid-\_r_1cb1\_ .image-shape .label,#chatgpt-mermaid-\_r_1cb1\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_1cb1\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_1cb1\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1cb1\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1cb1\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_1cb1\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1cb1\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1cb1\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1cb1\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_1cb1\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_1cb1\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_1cb1\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_1cb1\_ .icon-shape,#chatgpt-mermaid-\_r_1cb1\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_1cb1\_ .icon-shape p,#chatgpt-mermaid-\_r_1cb1\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_1cb1\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_1cb1\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_1cb1\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_1cb1\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_1cb1\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_1cb1\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_1cb1\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_1cb1\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_1cb1\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1cb1\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_1cb1\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_1cb1\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1cb1\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_1cb1\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_1cb1\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_1cb1\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .node rect,#chatgpt-mermaid-\_r_1cb1\_ .node circle,#chatgpt-mermaid-\_r_1cb1\_ .node ellipse,#chatgpt-mermaid-\_r_1cb1\_ .node polygon,#chatgpt-mermaid-\_r_1cb1\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_1cb1\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_1cb1\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_1cb1\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_1cb1\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_1cb1\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}PromptRepository DiscoveryApplicable Instructions andSkillsPlanning ModeScope, Tasks, Dependencies,Risks, AssumptionsAcceptance Criteria andValidation PlanProposed PlanPlan Quality GateReview and ApprovalExecution HandoffDeveloper AgentExecutionMaterial Plan Drift?Reviewer and ValidatorEvaluatorCompletion DecisionRevision RequiredApprovedYesNo

The workflow separates analysis, authorization, implementation, and assurance.

It also allows execution to return to planning when significant new evidence emerges.

The Harness should enforce these transitions rather than relying entirely on the conversational behavior of the AI Agent.

### **18.13 Key Architectural Takeaways**

Several principles define a mature Planning Mode implementation.

First, planning must begin with the actual repository and applicable engineering constraints, not an imagined architecture.

Second, the Plan must distinguish verified findings, assumptions, proposed decisions, and approved decisions.

Third, implementation scope and task dependencies must be explicit.

Fourth, critical business invariants and validation obligations must be established before coding.

Fifth, Planning Mode should not modify production code or configuration.

Sixth, human approval must remain an explicit boundary wherever organizational governance requires it.

Seventh, the Plan must support controlled revision when material evidence changes.

Finally, the approved Plan must provide a reliable handoff from the Lead Agent to the Developer Agent without replacing the distinct responsibilities of review, validation, and evaluation.

### **18.14 Final Perspective**

The most significant benefit of Planning Mode is not the production of a planning document.

It is the establishment of a deliberate engineering decision point before implementation begins.

An AI coding agent can produce source code rapidly. That capability becomes substantially more valuable when the agent first understands the system, identifies relevant constraints, evaluates dependencies, exposes risks, and defines how correctness will be demonstrated.

For Alpha Car Detailing, Planning Mode ensures that corporate fleet booking is approached as an enterprise business capability rather than merely as an endpoint-generation task.

The implementation begins with an understanding of the booking domain, contractual capacity rules, concurrency requirements, idempotency behavior, transactional consistency, and required validation evidence.

The Harness then provides the control structure needed to translate the approved approach into implementation while preserving accountability.

The central lesson of this chapter is:

> **Planning Mode is the discipline of establishing an evidence-based, authorized, and verifiable implementation approach before allowing an AI Agent to change the system.**

Planning does not replace execution.

It makes execution more deliberate.

Planning does not replace validation.

It defines what must be verified.

Planning does not replace architectural judgment.

It makes important architectural decisions visible.

And planning does not replace human accountability.

It creates a structured mechanism through which that accountability can be exercised.

In enterprise AI engineering, the ability to generate code is important. The ability to determine what should be implemented, why it should be implemented that way, and how its correctness will be established is equally fundamental.

**The purpose of Planning Mode is to ensure that implementation speed never becomes a substitute for engineering understanding.**

Chapter 27 status: In progress — next section: Further Reading


## **19. Further Reading**

Planning Mode draws on several established areas of software engineering, including requirements analysis, software architecture, repository comprehension, risk management, iterative development, and verification.

Although modern AI coding platforms provide specialized planning capabilities, the underlying engineering principles are not new. Experienced software teams have long distinguished understanding requirements, selecting designs, implementing changes, and establishing evidence of correctness.

What changes with AI-assisted development is the speed at which implementation can begin and the degree to which investigation, design, and execution can be delegated to an AI Agent.

The following resources provide additional depth for engineers and architects who want to develop reliable planning-first workflows.

### **19.1 Claude Code — Planning and Permission Modes**

**Source:** Anthropic, Claude Code Documentation and Help Center

**Reference:** [Claude Code — Permissions](https://code.claude.com/docs/en/permissions?utm_source=chatgpt.com) and [Claude Code User FAQ](https://support.claude.com/en/articles/14554922-claude-code-user-faq?utm_source=chatgpt.com)

Claude Code's permission model provides the practical foundation for separating planning from implementation.

The documentation explains permission modes, including the read-oriented Plan Mode, and the mechanisms available for controlling tool execution and repository modifications.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://support.claude.com\&sz=32)

Claude Help Center

The material is particularly relevant to the chapter's distinction between a natural-language instruction to avoid edits and a technically enforced planning boundary.

Readers should examine how permission settings interact with tools, external integrations, and organization-managed controls.

**Connection to Alpha Car Detailing**

Use the documentation to configure a planning session that can investigate corporate fleet booking without receiving unnecessary permission to modify the application.

The important outcome is an evidence-based Plan produced before implementation begins.

### **19.2 Claude Code — Memory and Repository Instructions**

**Source:** Anthropic, Claude Code Documentation

**Reference:** [Claude Code — Memory and Instructions](https://code.claude.com/docs/en/memory?utm_source=chatgpt.com)

Repository Instructions are an essential input to Planning Mode.

Claude Code uses `CLAUDE.md` and related memory mechanisms to provide persistent guidance about a project's architecture, engineering conventions, and working practices.

These mechanisms help distinguish durable engineering context from the task-specific Prompt and implementation Plan.

This resource is particularly useful when establishing how repository-level constraints are discovered and applied during planning.

**Connection to Alpha Car Detailing**

Review how the companion application's Clean Architecture conventions, testing requirements, and implementation restrictions should be communicated to Claude before corporate fleet booking is planned.

The Plan must respect applicable Instructions rather than replacing them with task-specific preferences.

### **19.3 Visual Studio Code — Planning with Agents**

**Source:** Microsoft, Visual Studio Code Documentation

**Reference:** [Plan Work with Agents in VS Code](https://code.visualstudio.com/docs/agents/run/planning?utm_source=chatgpt.com)

The Visual Studio Code documentation describes planning-first agent workflows, including defining a task, investigating the codebase, resolving questions, reviewing a proposed approach, and transitioning to implementation.

It also explains when explicit planning is useful and when a small, well-defined change may proceed without an elaborate Plan.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://code.visualstudio.com\&sz=32)

code.visualstudio.com

This material provides a practical comparison with Claude Code's planning capabilities.

It demonstrates how planning can be incorporated into the developer's existing workflow without changing the underlying engineering responsibility.

**Connection to Alpha Car Detailing**

Use the same corporate fleet booking Prompt in an appropriate Copilot planning workflow.

Compare how the agents identify affected components, assumptions, dependencies, and validation requirements.

The comparison should focus on engineering completeness rather than interface differences.

### **19.4 GitHub Copilot — Repository Custom Instructions**

**Source:** GitHub Documentation

**Reference:** [Custom Instructions for GitHub Copilot](https://docs.github.com/en/copilot/concepts/prompting-and-context/custom-instructions?utm_source=chatgpt.com)

GitHub Copilot supports persistent Instructions that can guide coding agents in repository-specific tasks.

The documentation explains repository-wide guidance, path-specific Instructions, and agent-related instruction mechanisms. Support varies by environment and workflow.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://docs.github.com\&sz=32)

GitHub Docs

This resource reinforces the distinction between Instructions and Plans.

Instructions establish persistent engineering constraints.

A Plan describes the approach to a particular task within those constraints.

**Connection to Alpha Car Detailing**

Compare how the same Clean Architecture and validation requirements could be expressed through `CLAUDE.md` and supported GitHub Copilot instruction files.

The objective is to preserve architectural consistency even when different AI coding platforms participate in development.

### **19.5 OpenAI Codex — Plan Mode**

**Source:** OpenAI, Codex

**Reference:** [Codex — Conversational Plan Mode](https://github.com/openai/codex/blob/main/codex-rs/collaboration-mode-templates/templates/plan.md?utm_source=chatgpt.com)

Codex's planning guidance distinguishes planning from implementation and describes how an agent can investigate requirements, resolve ambiguities, and produce an actionable proposed Plan.

It also distinguishes conversational Plan Mode from execution-progress tracking.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://github.com\&sz=32)

GitHub

This distinction closely aligns with the handbook's treatment of the Plan and Scratchpad as separate artifacts.

The resource is useful for understanding how planning responsibilities can be represented as an explicit agent operating mode.

**Connection to Alpha Car Detailing**

Evaluate whether Codex can produce a corporate fleet booking Plan containing the same architecture decisions, dependency analysis, risk assessment, and validation obligations expected from Claude Code.

The engineering contract should remain consistent even when the planning implementation differs.

### **19.6 OpenAI — Using PLANS.md for Multi-Hour Problem Solving**

**Source:** OpenAI Cookbook

**Reference:** [Using PLANS.md for Multi-Hour Problem Solving](https://developers.openai.com/cookbook/articles/codex_exec_plans?utm_source=chatgpt.com)

This resource describes a structured approach to long-running implementation through execution plans.

It discusses maintaining a durable Plan that identifies intended changes, implementation steps, validation expectations, and recovery considerations.

The guidance illustrates how planning can preserve continuity when a complex task spans multiple execution activities or sessions. The original article is archived, so its implementation details should be treated as historical guidance rather than a definitive description of current Codex behavior.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://developers.openai.com\&sz=32)

developers.openai.com

For the enterprise Harness, the important distinction is that an execution plan can evolve without allowing its approved architectural decisions to change silently.

**Connection to Alpha Car Detailing**

Consider how corporate fleet booking could be divided into milestones covering API contracts, capacity enforcement, idempotency, outbox integration, and validation.

Each milestone should have an identifiable outcome and verification requirement.

### **19.7 OpenAI — Iterating Development Workflows with Codex**

**Source:** OpenAI Cookbook

**Reference:** [Iterating Development Workflows with Codex](https://developers.openai.com/cookbook/examples/codex/iterating-development-workflows-with-codex?utm_source=chatgpt.com)

This resource explores repository-based conventions for organizing goals, Instructions, implementation phases, and execution evidence.

It demonstrates how artifacts such as `AGENTS.md`, `PLANS.md`, and separate workflow records can support iterative development.

The example conventions are optional and should be adapted to the target repository rather than treated as mandatory Codex files.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://developers.openai.com\&sz=32)

developers.openai.com

The resource is especially relevant to the handbook's distinction between permanent Instructions, task-specific Plans, temporary run context, and observed execution evidence.

**Connection to Alpha Car Detailing**

Use the ideas to evaluate how the corporate fleet booking Plan could remain available to the Developer, Reviewer, Validator, and Evaluator Agents without turning the Plan into an unstructured execution log.

### **19.8 Microsoft Azure Well-Architected Framework — Architecture Decision Records**

**Source:** Microsoft Learn

**Reference:** [Maintain an Architecture Decision Record](https://learn.microsoft.com/en-us/azure/well-architected/architect-role/architecture-decision-record?utm_source=chatgpt.com)

Architecture Decision Records provide a disciplined way to capture significant architectural choices, their context, alternatives, rationale, and consequences.

Microsoft's guidance emphasizes documenting decisions that affect system structure, important quality attributes, or choices that are difficult to reverse. It also recommends preserving decision history when an earlier decision is superseded.&#x20;

[image](https://www.google.com/s2/favicons?domain=https://learn.microsoft.com\&sz=32)

Microsoft Learn

This practice complements Planning Mode.

A task-specific Plan may contain architectural decisions needed for one implementation.

When a decision establishes a durable system-level direction, it may deserve a separate Architecture Decision Record.

The Plan should reference that decision without automatically becoming the authoritative architecture document.

**Connection to Alpha Car Detailing**

If corporate fleet booking requires a significant change to transaction coordination or introduces a new persistence consistency mechanism, evaluate whether the decision warrants an Architecture Decision Record.

Record the alternatives, rationale, tradeoffs, and consequences.

### **19.9 Martin Fowler — Evolutionary Architecture**

**Source:** Martin Fowler and contributors

**Reference:** [Evolutionary Architecture](https://martinfowler.com/articles/evo-architecture.html?utm_source=chatgpt.com)

Evolutionary architecture emphasizes designing systems that can adapt as requirements and evidence change.

This perspective is important because Planning Mode should not be mistaken for an attempt to finalize every implementation detail before development begins.

A Plan establishes an evidence-based initial approach, but execution may reveal new constraints.

The architecture must support controlled adaptation without allowing uncontrolled changes to established boundaries.

**Connection to Alpha Car Detailing**

Consider how the corporate fleet booking Plan should respond if implementation reveals that the existing persistence abstraction cannot preserve the required capacity invariant.

The appropriate response is to reassess the design and revise the Plan through the required approval process rather than forcing the original approach to remain unchanged.

### **19.10 Software Engineering at Google**

**Source:** Titus Winters, Tom Manshreck, and Hyrum Wright, *Software Engineering at Google*, O'Reilly Media, 2020

**Reference:** [Software Engineering at Google — Online Book](https://abseil.io/resources/swe-book?utm_source=chatgpt.com)

This book examines software engineering as a long-term organizational discipline rather than merely a code-production activity.

Its discussions of engineering practices, code review, testing, maintainability, and organizational coordination provide useful context for AI-assisted software delivery.

Planning Mode fits into this broader perspective by ensuring that implementation decisions remain connected to maintainability, reviewability, and established engineering processes.

**Connection to Alpha Car Detailing**

Consider how the corporate fleet booking Plan can minimize unnecessary complexity while preserving architectural consistency and adequate regression protection.

### **19.11 Fundamentals of Software Architecture**

**Source:** Mark Richards and Neal Ford, *Fundamentals of Software Architecture: An Engineering Approach*, O'Reilly Media, 2020

This book provides a foundation for understanding architecture characteristics, architectural styles, tradeoff analysis, and the responsibilities of software architects.

Planning Mode often requires evaluating several technically feasible approaches.

The appropriate decision depends on architectural characteristics such as maintainability, reliability, scalability, and operational complexity.

The book reinforces that architecture involves tradeoffs rather than universally correct patterns.

**Connection to Alpha Car Detailing**

Evaluate alternative approaches to corporate capacity enforcement.

Explain why the selected mechanism fits the existing architecture and business invariants instead of choosing it merely because the AI Agent can generate it efficiently.

### **19.12 Designing Data-Intensive Applications**

**Source:** Martin Kleppmann, *Designing Data-Intensive Applications*, O'Reilly Media, 2017

This book examines storage systems, replication, transactions, consistency, distributed processing, and the challenges of building reliable data-intensive applications.

These subjects are directly relevant to planning features that involve concurrent requests and distributed components.

For corporate fleet booking, a correct implementation depends on understanding transaction isolation, concurrency anomalies, durable state, and retry behavior.

Planning Mode should identify these concerns before an agent begins changing persistence logic.

**Connection to Alpha Car Detailing**

Use the concepts to assess whether a proposed booking-limit enforcement strategy can preserve the required invariant under concurrent database transactions.

Also examine how event processing and retries influence consistency across system boundaries.

### **19.13 Release It!**

**Source:** Michael T. Nygard, *Release It!*, Second Edition, Pragmatic Bookshelf, 2018

*Release It!* explores the engineering practices needed to operate reliable software systems under realistic conditions.

It emphasizes failure behavior, stability, operational risk, and the consequences of dependencies.

These concerns are essential inputs to Planning Mode.

A Plan that defines successful business behavior but ignores timeouts, failures, retries, and recovery is incomplete for high-impact enterprise functionality.

**Connection to Alpha Car Detailing**

Identify how corporate fleet booking should behave when account policy services are unavailable, booking transactions fail, or downstream event consumers experience temporary outages.

Use those failure scenarios to strengthen the Plan's risk assessment and validation strategy.

### **19.14 Accelerate**

**Source:** Nicole Forsgren, Jez Humble, and Gene Kim, *Accelerate: The Science of Lean Software and DevOps*, IT Revolution, 2018

*Accelerate* provides an evidence-based perspective on software delivery performance and organizational improvement.

It is relevant to evaluating Planning Mode because increased planning activity should not automatically be considered beneficial.

Planning should improve delivery reliability and reduce preventable rework without introducing disproportionate process overhead.

The enterprise should assess the relationship between planning practices, delivery outcomes, quality, and operational stability.

**Connection to Alpha Car Detailing**

Evaluate whether formal planning reduces missed dependencies and post-implementation corrections for complex booking changes.

Avoid assessing planning success solely through document length, planning duration, or number of Plans generated.

### **19.15 Recommended Reading Sequence**

The following sequence helps connect platform-specific planning capabilities with the broader engineering principles required for an enterprise Harness.

| Reading stage                   | Recommended resources                                                     | Learning focus                                             |
| ------------------------------- | ------------------------------------------------------------------------- | ---------------------------------------------------------- |
| 1. Practical planning           | Claude Code permissions and Plan Mode                                     | Understand non-mutating investigation                      |
| 2. Repository context           | Claude Code memory and GitHub Copilot Instructions                        | Understand persistent engineering constraints              |
| 3. Cross-platform comparison    | VS Code planning and Codex Plan Mode                                      | Separate platform interfaces from engineering requirements |
| 4. Durable planning             | Codex execution-plan guidance                                             | Understand long-running implementation Plans               |
| 5. Architectural governance     | Architecture Decision Records and *Fundamentals of Software Architecture* | Improve design rationale and tradeoff analysis             |
| 6. Reliability                  | *Designing Data-Intensive Applications* and *Release It!*                 | Plan for concurrency, consistency, and failure             |
| 7. Organizational effectiveness | *Software Engineering at Google* and *Accelerate*                         | Connect planning with sustainable engineering delivery     |

These resources should be read as complementary perspectives.

Product documentation explains supported tool behavior.

Architecture literature develops engineering judgment.

Reliability literature helps identify technical failure conditions.

Software delivery research provides context for evaluating whether planning improves organizational outcomes.

### **19.16 Further Reading in the Enterprise AI Engineering Handbook**

Chapter 27 builds on several earlier chapters and prepares the reader for the next stage of Claude Engineering.

The most relevant internal references are:

| Chapter                           | Relationship to Planning Mode                                     |
| --------------------------------- | ----------------------------------------------------------------- |
| Chapter 5 — Repository Discovery  | Establishes the evidence required before planning                 |
| Chapter 6 — Instructions          | Defines persistent constraints that Plans must respect            |
| Chapter 7 — Skills                | Provides reusable engineering procedures selected during planning |
| Chapter 8 — Prompts               | Defines the task-specific intent transformed into a Plan          |
| Chapter 9 — Roles                 | Establishes agent responsibilities and ownership                  |
| Chapter 10 — Steering Notes       | Supplies mission-specific priorities and constraints              |
| Chapter 11 — Knowledge Sources    | Provides supporting architectural and operational context         |
| Chapter 13 — Harness Architecture | Establishes orchestration and workflow boundaries                 |
| Chapter 14 — Lead Agent           | Coordinates planning and execution responsibilities               |
| Chapter 15 — Developer Agent      | Implements the approved Plan                                      |
| Chapter 16 — Reviewer Agent       | Reviews implementation quality and architectural alignment        |
| Chapter 17 — Validator Agent      | Verifies deterministic engineering evidence                       |
| Chapter 18 — Evaluator Agent      | Assesses outcome quality and goal satisfaction                    |
| Chapter 22 — CLAUDE.md            | Explains Claude-specific persistent repository guidance           |
| Chapter 23 — Claude Skills        | Explains reusable Claude engineering workflows                    |
| Chapter 24 — Claude Commands      | Describes reusable command-driven interactions                    |
| Chapter 25 — Claude Memory        | Addresses retained context and its boundaries                     |
| Chapter 26 — Tool Calling         | Explains how tools are invoked and controlled                     |

These chapters establish the architectural context required to understand Planning Mode as more than an isolated Claude Code feature.

The next chapter, **Chapter 28 — Enterprise Workflows**, extends these principles into coordinated development workflows in which planning, implementation, review, validation, evaluation, and approval operate together.

### **19.17 Final Reading Perspective**

The resources in this section reinforce a common engineering principle: reliable software development requires explicit reasoning about goals, constraints, dependencies, risks, and verification.

AI coding agents increase the speed at which implementation can be produced, but they do not eliminate the need for architectural understanding or evidence-based decision-making.

Planning Mode provides a disciplined mechanism for applying those responsibilities before implementation begins.

For Alpha Car Detailing, the practical objective is not merely to produce a Plan for corporate fleet booking.

It is to ensure that the eventual implementation preserves contractual business rules, concurrency safety, idempotency, transactional consistency, authorization, and compatibility with the existing enterprise system.

The same principle applies to every consequential change performed by an AI Agent.

A mature enterprise Harness should therefore make planning a governed capability, maintain clear authority boundaries, and preserve traceability from the original Prompt through implementation and validation.

**Planning determines the approach. Execution delivers the implementation. Validation verifies objective conditions. Evaluation assesses whether the intended goal has been satisfied. Human governance retains accountability for decisions requiring organizational authority.**

Together, these responsibilities form the foundation of disciplined enterprise AI-assisted software engineering.

**Chapter 27 status: Complete**
