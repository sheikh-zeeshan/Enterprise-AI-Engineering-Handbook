# Chapter 17 — Validator Agent

Engineering confidence does not come from an agent saying, “The implementation looks correct.” It comes from repeatable evidence showing that the implementation satisfies defined conditions.

The Validator Agent is responsible for producing that evidence.

## Story-driven Opening

The Alpha Car Detailing engineering team had completed the first implementation of corporate fleet booking. The new capability allowed an authorized corporate customer to reserve detailing services for multiple vehicles at a selected service station.

The Developer Agent had implemented the application command, domain behavior, REST endpoint, persistence changes, and integration event. The Reviewer Agent had independently examined the changes and reported that the implementation followed the repository’s Clean Architecture boundaries, used established naming conventions, and handled authorization and idempotency concerns appropriately.

The pull request appeared ready.

However, appearing correct was not enough.

The Lead Agent handed the reviewed implementation to the Validator Agent with a validation profile containing the required gates:

- Restore and build the affected solution.
- Execute unit and integration tests.
- Run static analysis.
- Verify architecture rules.
- scan dependencies and source code for known security problems.
- Validate the REST API contract.
- Validate the `CorporateFleetBookingCreated` event contract.
- Verify that the database migration could be applied safely.
- Confirm that required configuration values existed in deployment templates.
- Preserve complete command output and machine-readable reports.

The Validator Agent started with the build. It passed without warnings.

The domain unit tests passed. The application tests passed. Most integration tests passed.

One contract compatibility check failed.

The implementation had changed the event field `stationId` from a required string to an optional string. The Developer Agent had made the field optional because an internal test fixture did not always provide a station. The code compiled, and the unit tests passed, but existing consumers expected a non-null station identifier.

The Validator Agent did not decide whether the change was acceptable. It did not rewrite the consumer contract, weaken the compatibility rule, or mark the check as passed because the implementation looked reasonable. It recorded:

| Result field | Recorded value |
| --- | --- |
| Check performed | Backward-compatibility validation for `CorporateFleetBookingCreated` |
| Expected condition | No breaking changes to required event fields |
| Actual result | `stationId` changed from required to optional |
| Evidence | Contract comparison report and generated schema diff |
| Status | Fail |
| Failure details | Existing consumers may receive an event that violates their assumptions |
| Recommended next action | Restore the required field or obtain approval for a versioned contract change |

The overall validation outcome was **Fail**.

The implementation returned to the Developer Agent. The Developer Agent corrected the event model, updated the invalid fixture, and submitted a new implementation evidence package. The Reviewer Agent examined the correction, and the Validator Agent ran the applicable gates again.

This time, the event contract check passed. The complete validation suite passed, and the evidence was forwarded to the Evaluator Agent.

The Validator Agent did not prove that the feature was valuable, usable, or ready for production approval. It proved something narrower and essential: the implementation satisfied the objective engineering conditions assigned to it.

> **Core Principle**
>
> The Validator Agent verifies deterministic evidence. It does not replace the Reviewer Agent, Evaluator Agent, or human approval.

---

## Learning Objectives

After completing this chapter, you should be able to:

- Define the Validator Agent’s responsibilities, authority, and limits.
- Distinguish deterministic validation from subjective review and outcome evaluation.
- Design repeatable validation gates for builds, tests, architecture, security, contracts, data changes, configuration, and infrastructure.
- Define evidence requirements for every validation result.
- Interpret pass, fail, blocked, and inconclusive outcomes correctly.
- Design safe retry and correction cycles without weakening validation controls.
- Prevent agents from modifying tests or gates merely to obtain a passing result.
- Establish escalation rules for unavailable environments, flaky tests, conflicting requirements, and incomplete evidence.
- Integrate the Validator Agent into a governed enterprise engineering harness.
- Preserve human approval boundaries after automated validation succeeds.

---

## Background

### From Agent Confidence to Engineering Evidence

AI coding agents can generate convincing explanations of their work. They can summarize changed files, describe intended behavior, and claim that an implementation follows an architecture. These capabilities are useful, but they are not validation.

Statements such as the following are assertions:

- “The solution should compile.”
- “The endpoint follows the existing pattern.”
- “The tests appear comprehensive.”
- “The migration is safe.”
- “The event change is backward compatible.”
- “No security vulnerabilities were introduced.”

A validation process converts assertions into evidence:

- The exact build command completed with exit code zero.
- The test runner executed 186 tests, of which 186 passed.
- The architecture test suite detected no forbidden dependencies.
- The API specification comparison detected no breaking changes.
- The event schema passed compatibility validation against the registered version.
- The migration was applied to an empty database and an upgraded representative database.
- The security scanner reported no findings above the configured severity threshold.

The difference is fundamental. An assertion depends on the confidence or interpretation of the agent making it. Deterministic evidence depends on an observable procedure with defined inputs, expected conditions, and reproducible results.

### Why Validation Requires a Separate Role

A Developer Agent naturally focuses on completing assigned work. It selects implementation techniques, modifies code, adds tests, and resolves failures. This creates a risk of confirmation bias: the same agent that produced a change may interpret ambiguous results in favor of its own implementation.

The Reviewer Agent adds independent judgment. It examines code quality, architecture alignment, maintainability, security concerns, and compliance with Instructions and acceptance criteria. Its findings are valuable, but many review conclusions remain interpretive.

The Validator Agent has a different responsibility. It operates defined checks and records what happened. Its independence protects the integrity of engineering gates.

For example:

- The Developer Agent writes an architecture test.
- The Reviewer Agent assesses whether that test represents the intended architectural boundary.
- The Validator Agent executes the approved architecture test and reports its result.
- The Evaluator Agent determines whether the complete implementation achieves the expected outcome.
- A human makes any approval decision reserved by policy.

Separating these responsibilities creates clearer accountability and makes automation safer.

### Validation in an Enterprise Harness

In an enterprise harness, validation is not a single command named `validate`. It is a controlled collection of checks selected according to the affected components and risks.

A backend-only change may require:

- compilation;
- unit tests;
- application integration tests;
- architecture tests;
- static analysis;
- dependency scanning;
- API contract validation;
- database migration validation.

A deployment change may additionally require:

- manifest validation;
- infrastructure-as-code checks;
- policy evaluation;
- container image scanning;
- environment-specific configuration verification.

A schema-only change may require compatibility checks even when no application source code changed.

The harness must therefore determine which validation profile applies to the change. That decision may come from repository Instructions, change classification rules, the Lead Agent’s plan, or human-approved policy. The Validator Agent executes that profile; it does not silently redefine it.

### Deterministic Does Not Mean Infallible

A deterministic check produces the same outcome when executed with the same inputs under the same defined conditions. That does not mean the check is perfectly designed.

A test can assert the wrong behavior. A security scanner can miss a vulnerability. A contract rule can be incomplete. An architecture test can encode an outdated boundary. A database migration test can use data that does not represent production conditions.

The Validator Agent establishes whether the implementation satisfies the configured conditions. It does not establish that the conditions themselves are complete or correct.

This limitation is why deterministic validation must coexist with:

- independent review;
- outcome evaluation;
- architecture governance;
- security expertise;
- operational readiness assessment;
- human approval.

### Validation Must Be Reproducible

A useful validation result must be reproducible by another authorized participant. Reproducibility normally requires recording:

- source revision;
- working-tree state;
- validation profile version;
- tool versions;
- commands or workflows executed;
- relevant environment characteristics;
- test data or fixture versions;
- configuration sources;
- start and completion times;
- exit codes;
- reports, logs, and artifacts;
- any retries or exceptions.

Without this context, a statement such as “all tests passed” has limited value. The tests might have run against the wrong commit, skipped an affected project, used a developer-specific environment, or excluded a failing category.

> **Enterprise Engineering**
>
> A passing result without traceable evidence is not equivalent to a verified pass.

---

## Concepts

### What Is a Validator Agent?

A Validator Agent is an independent harness role responsible for executing or coordinating objective, testable, and repeatable engineering checks against an implementation output.

Its principal question is:

> Does the implementation satisfy the defined validation conditions, and what evidence proves the result?

The Validator Agent receives an implementation evidence package, identifies the applicable validation profile, prepares or verifies the execution environment, runs the required gates, collects evidence, classifies outcomes, and reports the results.

It does not decide whether subjective design choices are elegant. It does not determine whether users will value the capability. It does not approve production deployment unless a narrowly defined organizational process explicitly assigns such authority—and even then, it reports validation evidence rather than replacing accountable human decision-makers.

### Validator Agent Responsibilities

The Validator Agent is responsible for:

1. **Receiving the correct implementation**

   It confirms the source revision, branch, pull request, change set, or artifact being validated.

2. **Determining the applicable validation profile**

   It applies repository Instructions, affected-area rules, risk classifications, and assigned acceptance conditions.

3. **Establishing a controlled environment**

   It verifies required tool versions, dependencies, configuration, services, test data, and credentials without exposing protected values.

4. **Executing required checks**

   It runs builds, tests, analysis tools, contract checks, migration checks, configuration checks, and infrastructure checks.

5. **Preserving raw evidence**

   It retains command output, logs, reports, diffs, test results, scanner findings, and artifact identifiers.

6. **Interpreting results according to explicit rules**

   It maps the evidence to pass, fail, blocked, or inconclusive outcomes.

7. **Reporting failures precisely**

   It identifies the failed condition, affected component, supporting evidence, and recommended next action.

8. **Controlling retries**

   It distinguishes legitimate reruns from attempts to hide nondeterminism or obtain a convenient pass.

9. **Escalating unresolved conditions**

   It reports unavailable environments, conflicting gates, flaky tests, missing permissions, incomplete evidence, and policy questions.

10. **Protecting gate integrity**

    It refuses to weaken, remove, bypass, or silently change required gates.

### Authority

The Validator Agent may:

- execute approved validation commands and workflows;
- prepare isolated test environments;
- restore dependencies from approved sources;
- collect logs and diagnostic artifacts;
- classify results using defined rules;
- stop the workflow when a mandatory gate fails;
- request correction from the Developer Agent;
- request clarification when expected conditions are ambiguous;
- escalate blocked or inconclusive validation;
- recommend that an unreliable validation control be investigated;
- invoke approved validation Skills and tools.

The Validator Agent may have limited authority to perform non-product changes required for validation, such as creating temporary test data or starting disposable containers. Such authority must be explicitly defined by the harness and must not extend to changing production behavior.

### Limits

The Validator Agent must not:

- treat subjective review as deterministic validation;
- ignore failed gates;
- change tests merely to make validation pass;
- approve its own implementation;
- hide incomplete evidence;
- convert an inconclusive result into a pass;
- bypass security or architecture checks;
- replace the Reviewer Agent;
- replace the Evaluator Agent;
- replace human approval;
- silently remove checks from the validation profile;
- validate a different revision from the one being considered for approval;
- claim complete validation when required checks were skipped;
- expose secrets while preserving evidence.

If the Validator Agent discovers that a test or gate appears incorrect, it reports the problem. Correction of the gate must follow the governed change process and receive appropriate independent review.

### Validator Versus Reviewer

The Reviewer Agent and Validator Agent both examine implementation quality, but they use different forms of evidence.

| Dimension | Reviewer Agent | Validator Agent |
| --- | --- | --- |
| Primary question | Is the implementation well designed and aligned with expectations? | Does the implementation satisfy defined, executable conditions? |
| Reasoning mode | Interpretive and analytical | Deterministic and procedural |
| Typical inputs | Code diff, architecture context, Instructions, acceptance criteria | Source revision, validation profile, commands, test environment |
| Typical activities | Code review, architecture review, maintainability analysis | Builds, tests, scans, contract checks, policy checks |
| Evidence | Findings, explanations, references to code and standards | Exit codes, reports, logs, test results, generated diffs |
| Outcome | Approved, changes requested, concerns, recommendations | Pass, fail, blocked, inconclusive |
| Can accept subjective trade-offs? | Can recommend or escalate them | No; follows defined conditions |
| Can replace the other role? | No | No |

Consider an Alpha Car Detailing endpoint that creates a corporate fleet booking.

The Reviewer Agent may conclude that the controller contains too much business logic and should delegate to the application layer. That is an architecture and maintainability judgment supported by repository conventions.

The Validator Agent may execute an architecture test that prohibits API projects from referencing infrastructure implementations. The result is deterministic: either the prohibited dependency exists or it does not.

Both findings matter, but they are not interchangeable.

### Validator Versus Evaluator

The Evaluator Agent determines whether the completed work achieves the intended outcome at an acceptable level of quality. Evaluation may consider usefulness, behavioral fitness, scenario completeness, operational acceptability, and business expectations.

The Validator Agent determines whether defined checks passed.

| Dimension | Validator Agent | Evaluator Agent |
| --- | --- | --- |
| Primary question | Did the implementation satisfy the configured engineering gates? | Did the implementation achieve the intended outcome? |
| Basis | Repeatable checks and measurable conditions | Acceptance scenarios, outcome quality, broader judgment |
| Example | All required tests passed | Fleet coordinators can complete the booking workflow effectively |
| Result | Pass, fail, blocked, inconclusive | Accepted, rejected, partially satisfied, further evaluation needed |
| Subjective judgment | Minimized and constrained | Often necessary |
| Replaces human approval | No | No |

A fleet booking endpoint can pass every build, test, and contract check while still failing evaluation because the workflow does not support a required business scenario. Conversely, an evaluator may believe the feature behaves well while validation fails because a vulnerable dependency exceeds the enterprise severity threshold.

Neither role may erase the other’s result.

### Deterministic Validation

A validation check is deterministic when it has:

- a known input;
- a defined procedure;
- an explicit expected condition;
- an observable actual result;
- a repeatable status rule;
- preserved evidence.

The following is a deterministic condition:

> Running `dotnet test` for the approved solution at revision `abc123` must complete with exit code zero, with no failed or skipped critical tests.

The following is not sufficiently deterministic:

> Review the tests and decide whether they look good.

That instruction belongs to review unless it is converted into measurable rules.

Deterministic validation should avoid unnecessary dependence on:

- agent interpretation;
- undocumented local state;
- uncontrolled external services;
- current time without a controlled clock;
- random values without fixed seeds;
- mutable third-party data;
- manual inspection without defined criteria.

### Validation Profiles

A validation profile is a versioned definition of the checks required for a category of change. Profiles prevent each agent from inventing a validation strategy during execution.

For example:

| Change category | Required validation |
| --- | --- |
| Domain behavior | Build, domain unit tests, architecture tests, static analysis |
| REST API | Build, unit tests, integration tests, OpenAPI validation, backward-compatibility check |
| Integration event | Build, serialization tests, schema validation, registry compatibility check |
| EF Core model | Build, tests, migration generation check, migration application and rollback assessment |
| Configuration | Schema validation, required-key check, environment overlay check, secret-reference check |
| Infrastructure | Syntax validation, plan generation, policy checks, security scan |
| Dependency update | Build, tests, vulnerability scan, license policy check, compatibility checks |

A change may match multiple categories. The harness should combine the applicable gates rather than selecting only the least demanding profile.

### Build Checks

A build check verifies that affected projects can be restored and compiled under controlled conditions.

A robust build check may validate:

- dependency restoration from approved sources;
- correct SDK and compiler versions;
- compilation of all affected projects;
- generated source;
- warnings-as-errors policies;
- packaging;
- repeatable or deterministic build settings;
- absence of uncommitted generated changes;
- build artifact identity.

Compilation is necessary for most code changes, but compilation alone does not prove behavioral correctness, contract compatibility, migration safety, or operational readiness.

For Alpha Car Detailing, a build check might include:

```bash
dotnet restore AlphaCarDetailing.sln --locked-mode
dotnet build AlphaCarDetailing.sln \
  --configuration Release \
  --no-restore \
  -warnaserror
```

The Validator Agent records both commands, exit codes, tool versions, and output artifacts.

### Unit and Integration Tests

Unit tests verify isolated behavior. Integration tests verify interactions across meaningful boundaries, such as:

- application code and a database;
- serialization and event schemas;
- API endpoints and middleware;
- authentication and authorization;
- outbox persistence and publication;
- idempotent event consumption;
- repository implementations and infrastructure dependencies.

For corporate fleet booking, unit tests might verify that:

- a booking requires at least one vehicle;
- duplicate vehicle identifiers are rejected;
- the requested station supports the selected service;
- the corporate account is active;
- the booking total is calculated correctly.

Integration tests might verify that:

- the API accepts an authorized request;
- the booking and outbox message are committed atomically;
- duplicate idempotency keys do not create multiple bookings;
- unauthorized callers receive the expected problem response;
- the persisted booking can be retrieved;
- the event payload matches the registered schema.

The Validator Agent does not infer a complete pass from a partial test run. It records:

- discovered tests;
- executed tests;
- passed tests;
- failed tests;
- skipped tests;
- filtered tests;
- duration;
- coverage information when required;
- generated test reports.

A skipped critical test may be a failure even when the test runner exits successfully. The status rule must be defined in the validation profile.

### Architecture Checks

Architecture validation converts important structural rules into executable controls.

For a Clean Architecture implementation, checks may enforce that:

- Domain does not reference Application, Infrastructure, or API.
- Application does not reference API.
- API does not access persistence implementations directly.
- Infrastructure implements ports defined by inner layers.
- domain types do not depend on framework-specific persistence attributes.
- handlers do not bypass established application abstractions.
- microservices do not reference another service’s internal projects.
- integration contracts reside in approved assemblies.
- forbidden packages are not referenced from protected layers.

Architecture tests do not replace architecture review. They protect rules that can be expressed objectively.

A Reviewer Agent may identify a new architectural concern that is not covered by existing tests. An architect may then approve a new rule. Once governed and implemented, that rule becomes part of future deterministic validation.

### Static Analysis

Static analysis examines source code or compiled artifacts without executing the complete application behavior.

It can detect:

- compiler warnings;
- nullable reference violations;
- unreachable code;
- suspicious exception handling;
- resource disposal problems;
- style and maintainability violations;
- unsafe API usage;
- duplicate code;
- potential concurrency errors;
- injection risks;
- secret patterns;
- dependency problems.

A static-analysis gate must define which findings cause failure. For example:

- any critical finding fails;
- any new high-severity finding fails;
- no increase in approved technical-debt count;
- warnings from selected rule sets fail;
- suppressed findings require an approved justification.

The Validator Agent must not suppress a rule to obtain a pass. If a rule produces a false positive, the suppression requires evidence, review, and the authority defined by organizational policy.

### Security Validation

Security validation applies approved automated controls to code, dependencies, images, configurations, and infrastructure.

Typical checks include:

- static application security testing;
- software composition analysis;
- secret scanning;
- container image scanning;
- infrastructure policy checks;
- manifest security checks;
- authorization integration tests;
- transport-security configuration checks;
- package signature or provenance verification;
- software bill of materials generation.

For corporate fleet booking, security validation may confirm that:

- only authorized corporate users can create bookings;
- one corporate customer cannot access another customer’s fleet;
- request fields cannot alter server-controlled account identifiers;
- vulnerable packages do not exceed the approved severity threshold;
- secrets are referenced through the approved secret store;
- container images do not run with prohibited privileges.

Automated security validation does not replace threat modeling, penetration testing, or security review. It verifies only the controls that were actually executed.

### API Contract Validation

API validation confirms that an implementation conforms to its published interface and compatibility rules.

Checks may include:

- OpenAPI syntax validation;
- implementation-to-specification conformance;
- required property validation;
- response status verification;
- content-type verification;
- problem-details validation;
- authentication declaration checks;
- breaking-change detection;
- generated client compatibility;
- consumer-driven contract tests.

For example, the fleet booking API might promise:

```http
POST /api/corporate-fleet-bookings
Idempotency-Key: 0f718e13-0f3f-43aa-95be-ae76152bc938
```

A successful response may be required to return `201 Created`, a location header, and a representation containing the booking identifier and status.

A test returning `200 OK` may demonstrate functional behavior, but it fails the defined contract if `201 Created` is required.

### Event Contract Validation

Event contracts require explicit validation because event producers and consumers are independently deployable and may evolve at different times.

Validation may verify:

- schema syntax;
- event name;
- schema name and version;
- envelope structure;
- required fields;
- data types and formats;
- nullability;
- serialization behavior;
- backward or forward compatibility;
- partition-key requirements;
- metadata such as event and correlation identifiers;
- representative consumer deserialization.

For Alpha Car Detailing, the event may use the project’s generic event envelope:

```json
{
  "eventId": "8966dc13-ae89-468b-bcdd-33f893872f75",
  "correlationId": "77be4ee5-7424-405e-a3e4-c85ce29697fd",
  "eventType": "CorporateFleetBookingCreated",
  "schemaName": "corporate-fleet-booking-created",
  "schemaVersion": "1.0",
  "data": {
    "bookingId": "7315e09c-f4b8-4289-8dc3-a1284756732d",
    "corporateAccountId": "6495c206-e570-4fb7-a28f-4de3f77375f8",
    "stationId": "7b96d113-4ad6-4f32-8f13-0e2912307429",
    "vehicleCount": 12,
    "scheduledDate": "2026-09-22"
  }
}
```

A passing serialization test does not prove compatibility. The serialized payload must also be compared with the governed schema and applicable compatibility policy.

### Database Migration Validation

A database migration can compile and still be unsafe.

Migration validation may include:

- generating the expected migration;
- confirming that no unexplained model differences remain;
- applying all migrations to an empty database;
- upgrading a database at the previous supported version;
- validating seed and reference data;
- checking indexes and constraints;
- detecting destructive operations;
- examining table rewrites or long-running locks;
- testing application startup against the upgraded schema;
- assessing rollback or roll-forward procedures;
- checking compatibility during staged deployment.

Suppose the corporate fleet booking change makes `StationId` non-nullable. Applying that migration to a populated database will fail if existing rows contain null values. A migration that succeeds only on an empty database is not sufficient evidence for an upgrade scenario.

The Validator Agent reports this as a failure or an inconclusive result according to the defined profile. It does not add a default value arbitrarily or delete incompatible records to force the migration to pass.

### Configuration Validation

Configuration validation confirms that required settings are correctly declared, typed, scoped, and referenced without exposing sensitive values.

Checks may verify:

- required keys;
- configuration schemas;
- environment-specific overlays;
- duplicate or conflicting values;
- option binding;
- startup validation;
- secret references;
- prohibited plaintext secrets;
- feature-flag declarations;
- endpoint formats;
- retry and timeout ranges;
- logging and telemetry settings.

For example, a fleet booking event publisher may require:

- Event Hubs namespace;
- topic or hub name;
- managed identity configuration;
- schema registry reference;
- retry policy;
- outbox processing interval.

The Validator Agent can prove that the deployment manifests contain the required references and that the application successfully binds its options. It must not reveal secret values in the validation report.

### Infrastructure Validation

Infrastructure validation applies executable checks to infrastructure-as-code definitions, deployment manifests, policies, and generated plans.

It may include:

- syntax and formatting checks;
- initialization and provider validation;
- plan generation;
- policy-as-code evaluation;
- Kubernetes manifest validation;
- Helm chart rendering;
- deprecated API detection;
- identity and access checks;
- network exposure checks;
- resource naming and tagging policies;
- cost or quota thresholds;
- container security context rules;
- drift detection where authorized.

A generated plan deserves special attention. Syntax validation may pass while the plan proposes deleting a database or replacing a production resource. The Validator Agent must apply the configured destructive-change rules and escalate changes requiring human approval.

### Evidence Collection

Evidence is a first-class validation output. It must be sufficient for another authorized person or agent to understand what was checked and why the status was assigned.

Every validation result must include:

| Field | Purpose |
| --- | --- |
| Check performed | Identifies the exact gate, command, workflow, or policy |
| Expected condition | Defines what must be true for the check to pass |
| Actual result | Records what the check observed |
| Evidence | Links or refers to logs, reports, artifacts, diffs, and exit codes |
| Status | Pass, fail, blocked, or inconclusive |
| Failure details | Explains the violated condition when applicable |
| Recommended next action | Identifies correction, investigation, rerun, or escalation |

At the validation-run level, the evidence package should also identify:

- source revision;
- validation profile and version;
- execution environment;
- toolchain versions;
- start and completion timestamps;
- agent or workflow identity;
- retry history;
- all required checks, including checks that did not run;
- overall outcome.

Evidence should be tamper-resistant where risk justifies it. Enterprise implementations may use signed build attestations, immutable pipeline logs, checksums, artifact digests, or controlled evidence stores.

### Validation Outcomes

The Validator Agent uses four primary outcomes.

#### Pass

Use **Pass** only when:

- the required check executed;
- the expected condition was satisfied;
- sufficient evidence was captured;
- no disqualifying exception applies.

An overall validation run passes only when every mandatory gate passes and any allowed informational findings are handled according to policy.

#### Fail

Use **Fail** when:

- the check executed;
- the actual result violated the expected condition.

Examples include:

- compilation error;
- failed test;
- prohibited architecture dependency;
- contract-breaking change;
- security finding above the threshold;
- unsafe migration operation;
- invalid configuration;
- infrastructure policy violation.

#### Blocked

Use **Blocked** when the required check cannot proceed because a prerequisite or external capability is unavailable.

Examples include:

- missing authorization to access the test environment;
- unavailable package feed;
- unavailable schema registry;
- required test container cannot start;
- approved test data is inaccessible;
- dependent service environment is down.

Blocked does not mean failed implementation. It also does not mean pass.

#### Inconclusive

Use **Inconclusive** when the check ran, but the evidence does not support a reliable pass-or-fail determination.

Examples include:

- the test runner terminated before producing a complete report;
- a migration completed but required post-conditions could not be inspected;
- a security scanner returned conflicting results;
- an integration test failed intermittently under identical conditions;
- a contract tool could not determine compatibility for a supported schema feature.

An inconclusive result must never be converted to pass merely because no confirmed defect was found.

### Overall Status Aggregation

A simple aggregation policy can be defined as follows:

| Individual results | Overall outcome |
| --- | --- |
| All mandatory checks pass | Pass |
| Any mandatory check fails | Fail |
| No failures, but at least one mandatory check is blocked | Blocked |
| No failures or blocks, but at least one mandatory check is inconclusive | Inconclusive |
| Required evidence missing | Inconclusive or fail, according to policy |
| Required check silently omitted | Invalid validation run; treat as fail or escalate |

This policy should be encoded in the harness instead of left to agent discretion.

### Failure Handling

When a gate fails, the Validator Agent should:

1. Preserve the original evidence.
2. Identify the exact violated condition.
3. Determine whether the failure is reproducible.
4. Avoid changing application code, tests, or gate definitions.
5. Report the affected scope.
6. Recommend the appropriate next action.
7. Return the implementation to the responsible role.
8. Retain the failure in the validation history.
9. Revalidate the corrected revision using the applicable profile.

Diagnostic work is allowed, but diagnosis must not alter the meaning of the gate.

For example, the Validator Agent may rerun a failed integration test with additional logging to determine whether the database container was unavailable. It must record both executions and explain why the diagnostic rerun occurred.

### Retry and Correction Cycles

Retries must be controlled because repeated execution can conceal flaky behavior.

A legitimate retry might occur when:

- an approved transient-dependency policy applies;
- the validation environment failed before the implementation was exercised;
- additional diagnostics are required;
- the Developer Agent submitted a new revision;
- the validation tool itself encountered a documented transient error.

An illegitimate retry pattern is:

> Run the failing test repeatedly and report only the first passing attempt.

A retry record should contain:

- original attempt;
- reason for retry;
- authorizing rule or decision;
- environment changes;
- source revision;
- result of every attempt;
- final classification.

If a test passes and fails under materially identical conditions, the correct outcome is normally **Inconclusive** or **Fail**, based on policy—not Pass.

After a code correction, the harness should validate the new revision. It may run a targeted diagnostic test first, but final evidence should include the complete set of gates required by the change and regression policy.

### Escalation

The Validator Agent escalates when:

- validation conditions conflict;
- a required gate has no executable implementation;
- a gate appears obsolete or incorrect;
- a test is persistently flaky;
- the environment prevents validation;
- required evidence cannot be preserved;
- the implementation changes the validation mechanism itself;
- security findings require risk acceptance;
- a migration contains destructive operations;
- contract compatibility requires a versioning decision;
- a policy exception is requested;
- the result remains inconclusive after permitted diagnostics.

An escalation should state:

- the blocked decision;
- the affected validation check;
- the evidence collected;
- the risk of proceeding;
- available options;
- the authority required to decide.

The Validator Agent must not manufacture a decision merely to keep the workflow moving.

### Human Approval Boundaries

A successful validation run means that the implementation passed the defined engineering gates. It does not automatically authorize:

- merging;
- release;
- production deployment;
- destructive database operations;
- acceptance of security risk;
- backward-incompatible contract changes;
- regulatory or compliance exceptions;
- changes to enterprise architecture;
- changes to validation policy.

Organizations may automate low-risk decisions after validation, but that automation is a governance choice. The accountable approval boundary must remain explicit.

---

## Architecture Discussion

### Position in the Harness Workflow

The Validator Agent operates after implementation and independent review, and before outcome evaluation.

```mermaid
flowchart TD
    D["Developer Agent"] --> R["Reviewer Agent"]
    R --> V["Validator Agent"]
    V --> G{"Deterministic gates"}
    G -->|All required gates pass| E["Evaluator Agent"]
    G -->|Any gate fails| D
    G -->|Blocked or inconclusive| H["Escalation"]
    H --> D
    H --> A["Authorized human"]
```

This workflow communicates responsibility rather than requiring every organization to use an identical sequence. Some low-risk checks may execute earlier to provide rapid feedback. For example, a Developer Agent may run unit tests before requesting review. Those early checks are useful, but the independent Validator Agent must still verify the approved revision and preserve authoritative evidence.

### Validation Control Plane and Execution Plane

Enterprise validation benefits from separating coordination from check execution.

The **validation control plane** is responsible for:

- selecting the validation profile;
- binding the source revision;
- scheduling checks;
- enforcing required gates;
- aggregating outcomes;
- controlling retries;
- preserving provenance;
- routing pass, fail, blocked, and inconclusive results.

The **validation execution plane** contains:

- build runners;
- test runners;
- architecture-test frameworks;
- static-analysis tools;
- security scanners;
- contract validators;
- database test environments;
- infrastructure validation tools.

The Validator Agent participates primarily in the control plane. It can invoke execution tools, interpret their machine-readable outputs according to policy, and construct the evidence package. It should not replace mature deterministic tools with natural-language judgment.

```mermaid
flowchart TB
    subgraph CP["Validation control plane"]
        VP["Validation profile"]
        VA["Validator Agent"]
        OP["Outcome policy"]
        EV["Evidence package"]
    end

    subgraph EP["Validation execution plane"]
        BT["Build and tests"]
        AS["Architecture and static analysis"]
        SC["Security and contracts"]
        DI["Database and infrastructure"]
    end

    VP --> VA
    VA --> BT
    VA --> AS
    VA --> SC
    VA --> DI
    OP --> VA
    BT --> EV
    AS --> EV
    SC --> EV
    DI --> EV
```

### Inputs to the Validator Agent

A validation request should include:

| Input | Description |
| --- | --- |
| Task identifier | Connects validation to the original goal |
| Source revision | Identifies the immutable implementation under test |
| Change summary | Describes the intended modification |
| Changed-component inventory | Identifies affected projects, contracts, data, and infrastructure |
| Applicable Instructions | Supplies repository and enterprise rules |
| Validation profile | Defines mandatory and optional gates |
| Acceptance criteria | Identifies objective conditions that can be validated |
| Review result | Provides independent findings and unresolved concerns |
| Environment definition | Specifies required tools, services, and configuration |
| Evidence policy | Defines retention, redaction, and report requirements |

The Validator Agent should reject or block a request when the source revision is ambiguous. Validating a moving branch without recording the exact commit creates unreliable evidence.

### Outputs from the Validator Agent

The Validator Agent produces a validation result package containing:

- overall outcome;
- result for every required gate;
- commands and workflow identifiers;
- expected and actual results;
- machine-readable reports;
- human-readable summary;
- failure details;
- retry history;
- environment and toolchain metadata;
- evidence locations;
- recommended next action;
- escalation information;
- integrity or attestation information where required.

A concise machine-readable representation might resemble:

```json
{
  "taskId": "ACD-CFB-142",
  "sourceRevision": "abc123",
  "validationProfile": "dotnet-api-event-v3",
  "overallStatus": "Fail",
  "checks": [
    {
      "name": "solution-build",
      "expectedCondition": "Release build completes with no warnings",
      "actualResult": "Build completed with 0 warnings and 0 errors",
      "status": "Pass",
      "evidence": [
        "artifacts/build.log",
        "artifacts/build.binlog"
      ],
      "failureDetails": null,
      "recommendedNextAction": null
    },
    {
      "name": "event-contract-compatibility",
      "expectedCondition": "No breaking change against schema version 1.0",
      "actualResult": "Required field stationId became optional",
      "status": "Fail",
      "evidence": [
        "artifacts/event-schema-diff.json"
      ],
      "failureDetails": "Existing consumers may receive an invalid payload.",
      "recommendedNextAction": "Restore the required field or introduce an approved new schema version."
    }
  ]
}
```

The precise schema will vary, but the required concepts should remain stable.

### Change-Based Gate Selection

Running every enterprise check for every change can be expensive. Running too few checks creates blind spots. A governed harness can select gates from the intersection of change scope, risk, and policy.

```mermaid
flowchart TD
    C["Changed artifacts"] --> CL["Change classification"]
    R["Risk rules"] --> CL
    I["Repository Instructions"] --> CL
    CL --> P["Resolved validation profile"]
    P --> X["Execute mandatory gates"]
    X --> O["Aggregate outcomes"]
```

Examples include:

- A domain-only change triggers build, domain tests, architecture tests, and static analysis.
- An API model change additionally triggers OpenAPI and consumer contract checks.
- An event model change triggers schema and compatibility checks.
- An EF Core model change triggers migration safety checks.
- A deployment-file change triggers configuration, manifest, policy, and security checks.
- A change to validation scripts triggers additional review and protected-control verification.

Dynamic selection must be transparent. The evidence package should list both selected and excluded gates with the rule responsible for each decision.

### Protecting Validation Gates

Validation definitions are security- and quality-sensitive assets. If an AI agent can freely change a test, threshold, policy, or workflow, it can make a broken implementation appear compliant.

Protected assets may include:

- test projects;
- contract schemas;
- architecture rules;
- analyzer configurations;
- scanner thresholds;
- pipeline definitions;
- infrastructure policies;
- validation profiles;
- baseline files;
- suppression lists.

Not every test file must be immutable, because product changes often require legitimate test updates. However, changes to validation controls should receive elevated scrutiny.

A practical policy can require:

- explicit identification of gate changes;
- independent review;
- comparison with the previous validation behavior;
- approval for reduced coverage or weaker thresholds;
- execution of both old and new checks when feasible;
- audit history for suppressions and exceptions.

The Validator Agent must report when the revision under validation also modifies its validation gates.

### Trusted and Untrusted Evidence

Not all evidence has equal reliability.

| Evidence source | Typical confidence | Concern |
| --- | ---: | --- |
| Controlled CI runner bound to commit digest | High | Runner and pipeline integrity still matter |
| Signed build attestation | High | Signing identity and provenance must be trusted |
| Machine-readable test report | High | Test selection and skipped tests must be inspected |
| Local developer command output | Medium | Local state may differ from governed environment |
| Agent-written summary without raw output | Low | Claims cannot be independently verified |
| Screenshot of a passing command | Low | Source revision and full context may be missing |
| Verbal statement that tests passed | Insufficient | Not reproducible or auditable |

Enterprise workflows should prefer evidence generated by controlled systems. Locally generated evidence may be useful for diagnosis but should not automatically satisfy authoritative gates.

### Validation and Repository Intelligence

The Validator Agent requires Repository Intelligence to avoid executing an incomplete or inappropriate validation set.

It needs to understand:

- solution and project boundaries;
- build entry points;
- test organization;
- architecture test locations;
- contract definitions;
- migration ownership;
- deployment files;
- CI workflows;
- analyzer configuration;
- repository-specific scripts;
- applicable Instructions and Skills.

Repository discovery should occur before execution when the validation profile does not already provide authoritative commands. The Validator Agent must not guess that `dotnet test` at the repository root covers every required project.

### Validation Skills

Reusable Skills can standardize recurring checks, such as:

- validate a .NET solution;
- validate an API contract;
- validate an integration event;
- validate an EF Core migration;
- validate a Kubernetes deployment;
- validate Terraform changes;
- collect and redact evidence;
- classify flaky tests;
- compare validation profiles.

A validation Skill should define:

- prerequisites;
- allowed tools;
- exact procedure;
- expected outputs;
- status rules;
- evidence requirements;
- retry policy;
- escalation conditions.

Skills increase consistency, but they remain subordinate to repository Instructions and approved validation policy.

---

## Professional Diagrams

### End-to-End Validation Workflow

```mermaid
flowchart TD
    A["Receive reviewed revision"] --> B["Resolve validation profile"]
    B --> C["Verify environment and prerequisites"]
    C --> D["Execute deterministic checks"]
    D --> E["Collect evidence"]
    E --> F{"Classify results"}
    F -->|Pass| G["Send evidence to Evaluator"]
    F -->|Fail| H["Return correction request"]
    F -->|Blocked| I["Resolve prerequisite or escalate"]
    F -->|Inconclusive| J["Investigate or escalate"]
    H --> K["Developer submits new revision"]
    K --> A
```

### Validation Gate Structure

```mermaid
flowchart TB
    V["Validator Agent"] --> B["Build"]
    V --> T["Tests"]
    V --> Q["Quality and security"]
    V --> C["Contracts and data"]

    B --> B1["Restore and compile"]
    T --> T1["Unit and integration"]
    Q --> Q1["Static and architecture"]
    Q --> Q2["Security"]
    C --> C1["API and events"]
    C --> C2["Migrations and configuration"]
```

### Separation of Responsibilities

| Activity | Developer | Reviewer | Validator | Evaluator | Human approver |
| --- | ---: | ---: | ---: | ---: | ---: |
| Implement code and configuration | Primary | No | No | No | No |
| Add or update tests | Primary | Reviews | Executes approved tests | May inspect outcomes | May approve sensitive changes |
| Assess maintainability | Considers | Primary | Only through defined metrics | May consider | May decide trade-offs |
| Execute required gates | May run early checks | May inspect results | Primary | Consumes results | Consumes evidence |
| Classify deterministic outcomes | No authoritative classification | No | Primary | No | May adjudicate exceptions |
| Evaluate business outcome | Supports | Supports | No | Primary | May approve |
| Accept security risk | No | Recommends | No | No | Authorized human only |
| Approve production release | No | No | No | No | According to governance |

### Outcome State Model

```mermaid
stateDiagram-v2
    [*] --> Running
    Running --> Pass: All mandatory checks succeed
    Running --> Fail: A mandatory condition is violated
    Running --> Blocked: A prerequisite is unavailable
    Running --> Inconclusive: Evidence is insufficient
    Fail --> Running: Corrected revision submitted
    Blocked --> Running: Prerequisite restored
    Inconclusive --> Running: Approved investigation or rerun
    Pass --> [*]
```

### Correction Loop with Revision Integrity

```mermaid
sequenceDiagram
    participant D as Developer Agent
    participant R as Reviewer Agent
    participant V as Validator Agent
    participant E as Evaluator Agent

    D->>R: Revision A and implementation evidence
    R->>V: Reviewed Revision A
    V->>V: Execute mandatory gates
    V-->>D: Revision A fails contract validation
    D->>R: Revision B with correction
    R->>V: Reviewed Revision B
    V->>V: Validate Revision B
    V->>E: Pass result and evidence for Revision B
```

The revision identifier is part of every handoff. Evidence for Revision A cannot be reused as proof that Revision B passed unless the validation policy explicitly allows unaffected results to be carried forward and preserves the basis for that decision.

Chapter 17 status: In progress — next section: Hands-on Example

## Hands-on Example

The Alpha Car Detailing team has implemented corporate fleet booking in the Booking microservice. An authorized corporate fleet coordinator can submit one booking request containing multiple vehicles, select a station and service package, and receive a booking identifier.

The implementation also stores an outbox message for the `CorporateFleetBookingCreated` integration event in the same database transaction.

The Developer Agent has completed the implementation, and the Reviewer Agent has requested no further changes. The Validator Agent must now verify the reviewed revision.

### Validation Scenario

The change affects:

- the Domain layer;
- the Application layer;
- the REST API;
- EF Core persistence;
- the transactional outbox;
- the OpenAPI contract;
- the `CorporateFleetBookingCreated` event contract;
- application configuration;
- the Booking service deployment manifest.

Because the implementation crosses several boundaries, compilation and unit tests alone are insufficient. The applicable validation profile requires:

| Gate | Expected condition |
| --- | --- |
| Source integrity | The checked-out revision matches the reviewed revision |
| Dependency restore | Dependencies restore from approved sources |
| Release build | All projects compile with zero errors and no prohibited warnings |
| Domain tests | All corporate fleet booking rules pass |
| Application tests | Command handling and idempotency behavior pass |
| Integration tests | API, database, and outbox scenarios pass |
| Architecture tests | Clean Architecture dependency rules pass |
| Static analysis | No findings above the configured quality threshold |
| Security checks | No secrets or prohibited vulnerable dependencies are detected |
| API contract | OpenAPI is valid and contains no unapproved breaking changes |
| Event contract | Event schema is valid and backward compatible |
| Migration validation | Migration succeeds for empty and representative upgraded databases |
| Configuration validation | Required settings bind successfully and contain no plaintext secrets |
| Deployment validation | Kubernetes manifests render and pass approved policies |

### Example Repository Structure

The Validator Agent first confirms the relevant repository structure:

```text
alpha-car-detailing/
├── src/
│   └── Services/
│       └── Booking/
│           ├── AlphaCarDetailing.Booking.Api/
│           ├── AlphaCarDetailing.Booking.Application/
│           ├── AlphaCarDetailing.Booking.Domain/
│           ├── AlphaCarDetailing.Booking.Infrastructure/
│           └── AlphaCarDetailing.Booking.Contracts/
├── tests/
│   └── Services/
│       └── Booking/
│           ├── AlphaCarDetailing.Booking.Domain.Tests/
│           ├── AlphaCarDetailing.Booking.Application.Tests/
│           ├── AlphaCarDetailing.Booking.IntegrationTests/
│           ├── AlphaCarDetailing.Booking.ArchitectureTests/
│           └── AlphaCarDetailing.Booking.ContractTests/
├── contracts/
│   ├── api/
│   │   └── booking-openapi.json
│   └── events/
│       └── corporate-fleet-booking-created.v1.json
├── deploy/
│   └── booking/
│       ├── base/
│       └── overlays/
├── harness/
│   ├── roles/
│   │   └── validator.md
│   ├── validation/
│   │   └── booking-service.yaml
│   └── scripts/
│       └── validate-booking-service.ps1
└── AlphaCarDetailing.sln
```

Repository discovery is evidence-driven. The Validator Agent does not assume that solution-level test execution automatically includes contract, architecture, or deployment validation.

### Step 1: Receive the Validation Request

The Lead Agent supplies a validation request containing:

```yaml
taskId: ACD-CFB-142
feature: Corporate Fleet Booking
sourceRevision: 8d34e7a
reviewedRevision: 8d34e7a
reviewOutcome: approved-for-validation
validationProfile: booking-service-v3
riskClassification: medium
changedAreas:
  - domain
  - application
  - api
  - persistence
  - event-contract
  - configuration
  - deployment
requiredEvidence:
  - build-log
  - test-results
  - coverage-report
  - architecture-report
  - static-analysis-report
  - security-report
  - api-contract-diff
  - event-contract-diff
  - migration-report
  - configuration-report
  - deployment-policy-report
```

The Validator Agent verifies that:

- `sourceRevision` exists;
- `sourceRevision` equals `reviewedRevision`;
- the working tree does not contain unrelated changes;
- the validation profile exists;
- the profile is the approved version;
- all required tools are available.

If the source revision does not match the reviewed revision, validation stops with a blocked result. Passing evidence from an unreviewed revision would not satisfy the workflow.

### Step 2: Prepare an Isolated Environment

The Validator Agent uses an isolated validation environment containing:

- the approved .NET SDK;
- locked dependency versions;
- SQL Server in a disposable container;
- the approved static-analysis tools;
- API and event contract comparison tools;
- security scanners;
- Kubernetes manifest validation tools;
- sanitized representative test data.

The environment must not depend on undocumented developer-machine state.

Example environment verification:

```powershell
$ErrorActionPreference = "Stop"

dotnet --info
docker version
git --version

dotnet tool restore
```

The validation record captures tool versions. If the required SDK is unavailable, the result is **Blocked** rather than **Fail**, because the implementation has not yet violated a tested condition.

### Step 3: Verify Revision Integrity

The Validator Agent records the current revision:

```powershell
$expectedRevision = "8d34e7a"
$actualRevision = git rev-parse --short HEAD

if ($actualRevision -ne $expectedRevision) {
    throw "Revision mismatch. Expected $expectedRevision but found $actualRevision."
}

$statusOutput = git status --porcelain

if ($statusOutput) {
    throw "The validation workspace contains uncommitted changes."
}
```

A stronger enterprise workflow may validate a full commit digest instead of an abbreviated identifier.

The recorded result is:

| Field | Value |
| --- | --- |
| Check performed | Source revision integrity |
| Expected condition | Workspace contains reviewed revision `8d34e7a` with no local changes |
| Actual result | Revision matched and working tree was clean |
| Evidence | Revision and workspace-status log |
| Status | Pass |
| Failure details | None |
| Recommended next action | Continue with build validation |

### Step 4: Restore and Build

The Validator Agent executes a locked restore and Release build:

```powershell
dotnet restore .\AlphaCarDetailing.sln --locked-mode

dotnet build .\AlphaCarDetailing.sln `
    --configuration Release `
    --no-restore `
    -warnaserror `
    /bl:.\artifacts\build.binlog
```

The build check verifies more than whether the compiler emitted assemblies. It also confirms that:

- locked dependencies can be restored;
- prohibited warnings are not present;
- all affected projects participate in the build;
- the Release configuration compiles;
- a binary log is captured.

An example passing result is:

```text
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

The Validator Agent attaches the command log and binary build log to the evidence package.

### Step 5: Execute Domain Tests

The corporate fleet booking domain rules require:

- an active corporate account;
- at least one vehicle;
- no duplicate vehicle identifiers;
- a station that supports corporate fleet bookings;
- a service package available at the selected station;
- a scheduled date within the permitted booking window.

The Validator Agent executes the domain tests:

```powershell
dotnet test `
    .\tests\Services\Booking\AlphaCarDetailing.Booking.Domain.Tests `
    --configuration Release `
    --no-build `
    --logger "trx;LogFileName=domain-tests.trx" `
    --results-directory .\artifacts\test-results
```

Representative tests include:

```csharp
[Fact]
public void Create_ShouldRejectEmptyVehicleCollection()
{
    var result = CorporateFleetBooking.Create(
        CorporateAccountId.New(),
        StationId.New(),
        ServicePackageId.New(),
        scheduledDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
        vehicles: []);

    result.IsFailure.ShouldBeTrue();
    result.Error.Code.ShouldBe("FleetBooking.VehiclesRequired");
}

[Fact]
public void Create_ShouldRejectDuplicateVehicles()
{
    var vehicleId = VehicleId.New();

    var result = CorporateFleetBooking.Create(
        CorporateAccountId.New(),
        StationId.New(),
        ServicePackageId.New(),
        scheduledDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
        vehicles: [vehicleId, vehicleId]);

    result.IsFailure.ShouldBeTrue();
    result.Error.Code.ShouldBe("FleetBooking.DuplicateVehicles");
}
```

The Validator Agent executes these tests but does not determine whether the business rules are sufficient. That broader question belongs to review and evaluation.

### Step 6: Execute Application Tests

Application tests verify orchestration around the domain model. The required scenarios include:

- successful command handling;
- inactive corporate-account rejection;
- unavailable station rejection;
- idempotent request handling;
- persistence invocation;
- outbox message creation;
- cancellation-token propagation;
- correct error mapping.

Example command:

```powershell
dotnet test `
    .\tests\Services\Booking\AlphaCarDetailing.Booking.Application.Tests `
    --configuration Release `
    --no-build `
    --logger "trx;LogFileName=application-tests.trx" `
    --results-directory .\artifacts\test-results
```

A test for idempotent handling might execute the same command twice using the same idempotency key and verify that only one booking is created.

A passing test establishes that the approved test condition was satisfied. It does not prove that every possible concurrency scenario has been covered.

### Step 7: Execute Integration Tests

The integration tests start the Booking API with a disposable SQL Server database. They verify the application through externally meaningful boundaries.

Representative scenarios include:

1. An authorized fleet coordinator creates a booking.
2. An unauthorized user receives `403 Forbidden`.
3. Invalid input produces an RFC 7807-compatible problem response.
4. The booking and outbox record are committed atomically.
5. Repeated requests with the same idempotency key return the same logical result.
6. A failed transaction does not leave a partial booking or orphaned outbox record.
7. The response contains the expected booking location.
8. The event payload can be deserialized using the published contract.

Example command:

```powershell
dotnet test `
    .\tests\Services\Booking\AlphaCarDetailing.Booking.IntegrationTests `
    --configuration Release `
    --no-build `
    --logger "trx;LogFileName=integration-tests.trx" `
    --results-directory .\artifacts\test-results `
    --collect "XPlat Code Coverage"
```

A representative API test might be:

```csharp
[Fact]
public async Task CreateCorporateFleetBooking_ShouldPersistBookingAndOutboxMessage()
{
    var request = CorporateFleetBookingRequestFactory.CreateValid(vehicleCount: 12);
    using var message = TestHttpRequest.CreatePost(
        "/api/corporate-fleet-bookings",
        request);

    message.Headers.Add("Idempotency-Key", Guid.NewGuid().ToString());

    var response = await Client.SendAsync(message);

    response.StatusCode.ShouldBe(HttpStatusCode.Created);

    var result =
        await response.Content.ReadFromJsonAsync<CorporateFleetBookingResponse>();

    result.ShouldNotBeNull();

    await Database.Bookings
        .AnyAsync(x => x.Id == result.BookingId)
        .ShouldBeTrue();

    await Database.OutboxMessages
        .AnyAsync(x =>
            x.EventType == "CorporateFleetBookingCreated" &&
            x.AggregateId == result.BookingId)
        .ShouldBeTrue();
}
```

The Validator Agent preserves the test report and code-coverage artifact. If code coverage is an enforced gate, it records the configured threshold and actual percentage. It does not invent a threshold during validation.

### Step 8: Validate Architecture Rules

The Booking service follows Clean Architecture. The approved architecture tests enforce rules such as:

- Domain must not reference Application, Infrastructure, or API.
- Application must not reference Infrastructure or API.
- API controllers must not depend directly on `BookingDbContext`.
- Domain entities must not reference Entity Framework Core.
- Infrastructure implementations must satisfy interfaces owned by inner layers.
- Booking projects must not reference another microservice’s internal projects.

Example architecture test:

```csharp
[Fact]
public void Domain_ShouldNotDependOnOuterLayers()
{
    var result = Types
        .InAssembly(typeof(CorporateFleetBooking).Assembly)
        .ShouldNot()
        .HaveDependencyOnAny(
            "AlphaCarDetailing.Booking.Application",
            "AlphaCarDetailing.Booking.Infrastructure",
            "AlphaCarDetailing.Booking.Api")
        .GetResult();

    result.IsSuccessful.ShouldBeTrue();
}
```

The Validator Agent executes:

```powershell
dotnet test `
    .\tests\Services\Booking\AlphaCarDetailing.Booking.ArchitectureTests `
    --configuration Release `
    --no-build `
    --logger "trx;LogFileName=architecture-tests.trx" `
    --results-directory .\artifacts\test-results
```

If a rule fails, the Validator Agent reports the dependency path. It does not decide that the dependency is acceptable because it reduces implementation effort.

### Step 9: Run Static Analysis

The Validator Agent runs the repository’s approved analyzers and quality checks.

Depending on the repository configuration, this may include:

- .NET analyzers;
- nullable-reference analysis;
- formatting verification;
- quality-rule enforcement;
- duplication or complexity checks;
- prohibited API checks.

Example commands:

```powershell
dotnet format .\AlphaCarDetailing.sln `
    --verify-no-changes `
    --severity warn `
    --no-restore

dotnet build .\AlphaCarDetailing.sln `
    --configuration Release `
    --no-restore `
    -warnaserror
```

If the formatter detects differences, the check fails. The Validator Agent does not apply formatting automatically during authoritative validation because that would alter the revision being validated.

The recommended next action is for the Developer Agent to apply the required formatting and submit a new revision.

### Step 10: Run Security Validation

The validation profile requires:

- secret scanning;
- vulnerable dependency scanning;
- authorization integration tests;
- container or deployment security checks where applicable.

The result policy states:

- any detected secret fails validation;
- any new critical or high-severity dependency vulnerability fails validation;
- medium findings require recording and policy-based handling;
- suppressed findings require an approved suppression record.

A sample result might be:

| Check | Expected | Actual | Status |
| --- | --- | --- | --- |
| Secret scan | No secrets detected | No findings | Pass |
| Dependency scan | No new high or critical vulnerabilities | No high or critical findings | Pass |
| Tenant isolation tests | Cross-account access denied | API returned `403 Forbidden` | Pass |
| Plaintext secret check | No protected values in deployment files | Secret-store references only | Pass |

A scanner’s zero exit code is not always sufficient. Some tools report findings while returning success. The Validator Agent must parse results according to the approved severity rules.

### Step 11: Validate the API Contract

The Validator Agent generates or retrieves the OpenAPI document from the implementation and compares it with the approved baseline.

The required checks include:

- valid OpenAPI syntax;
- presence of the booking operation;
- correct HTTP method and route;
- required idempotency header;
- request-schema compatibility;
- response-schema compatibility;
- defined `201`, `400`, `401`, `403`, `409`, and `500` responses;
- RFC 7807 problem response definitions;
- no unapproved breaking changes.

The expected operation is conceptually:

```yaml
/api/corporate-fleet-bookings:
  post:
    operationId: CreateCorporateFleetBooking
    parameters:
      - name: Idempotency-Key
        in: header
        required: true
        schema:
          type: string
          format: uuid
    responses:
      "201":
        description: Corporate fleet booking created
      "400":
        description: Invalid request
      "401":
        description: Authentication required
      "403":
        description: Caller is not authorized
      "409":
        description: Booking conflict
```

Suppose the comparison identifies that the implementation removed the `409 Conflict` response from the specification. Even if the endpoint still compiles and its happy-path tests pass, the contract check fails.

The Validator Agent records the generated specification and contract diff as evidence.

### Step 12: Validate the Event Contract

The `CorporateFleetBookingCreated` event must conform to the approved JSON Schema and remain backward compatible with version 1 consumers.

The validation verifies:

- event envelope structure;
- event type;
- schema name;
- schema version;
- required identifiers;
- required `stationId`;
- vehicle count constraints;
- scheduled-date format;
- additional-property policy;
- backward compatibility.

A representative schema fragment is:

```json
{
  "$schema": "https://json-schema.org/draft/2020-12/schema",
  "title": "CorporateFleetBookingCreated",
  "type": "object",
  "required": [
    "bookingId",
    "corporateAccountId",
    "stationId",
    "vehicleCount",
    "scheduledDate"
  ],
  "properties": {
    "bookingId": {
      "type": "string",
      "format": "uuid"
    },
    "corporateAccountId": {
      "type": "string",
      "format": "uuid"
    },
    "stationId": {
      "type": "string",
      "format": "uuid"
    },
    "vehicleCount": {
      "type": "integer",
      "minimum": 1
    },
    "scheduledDate": {
      "type": "string",
      "format": "date"
    }
  }
}
```

The Validator Agent also serializes a representative event and validates the produced JSON—not only the declared C# type—against the schema.

This catches problems such as:

- unexpected property naming;
- null fields;
- incorrect date formats;
- omitted required data;
- envelope mismatch;
- incompatible serializer settings.

### Step 13: Validate the Database Migration

The implementation adds the following persistence objects:

- `CorporateFleetBookings`;
- `CorporateFleetBookingVehicles`;
- a unique idempotency constraint;
- an index supporting station and scheduled-date queries;
- the existing outbox relationship.

The Validator Agent performs two migration scenarios.

#### Empty-Database Scenario

The migration is applied to a newly created database:

```powershell
dotnet ef database update `
    --project .\src\Services\Booking\AlphaCarDetailing.Booking.Infrastructure `
    --startup-project .\src\Services\Booking\AlphaCarDetailing.Booking.Api `
    --context BookingDbContext
```

This confirms that the complete migration chain can create the database.

#### Upgrade Scenario

The Validator Agent:

1. Creates a database at the previously supported migration.
2. Loads sanitized representative data.
3. Applies the new migration.
4. Verifies required tables, columns, constraints, and indexes.
5. Starts the application against the upgraded database.
6. Executes a booking integration scenario.
7. confirms that existing records remain accessible.

The migration report must identify potentially destructive operations such as:

- dropping columns;
- narrowing column sizes;
- changing nullability;
- rebuilding large tables;
- deleting data;
- dropping indexes required by previous versions.

If the migration changes a nullable column to non-nullable without a safe data-handling strategy, it fails the upgrade validation even if it succeeds against an empty database.

### Step 14: Validate Configuration

The Booking API requires strongly typed configuration:

```csharp
public sealed class BookingEventOptions
{
    public const string SectionName = "BookingEvents";

    public required string EventHubName { get; init; }

    public required string SchemaGroup { get; init; }

    public int MaximumRetryCount { get; init; }

    public TimeSpan RetryDelay { get; init; }
}
```

Startup validation is configured:

```csharp
services
    .AddOptions<BookingEventOptions>()
    .BindConfiguration(BookingEventOptions.SectionName)
    .ValidateDataAnnotations()
    .Validate(
        options => options.MaximumRetryCount is >= 0 and <= 10,
        "MaximumRetryCount must be between 0 and 10.")
    .ValidateOnStart();
```

The Validator Agent verifies that:

- all required values are provided through approved sources;
- integer and duration values bind correctly;
- invalid settings cause startup failure;
- environment overlays do not remove required values;
- protected values use secret references;
- no secret value appears in logs or reports.

It records key names and validation outcomes, not secret values.

### Step 15: Validate Deployment Artifacts

The deployment profile requires the Validator Agent to:

- render the Kubernetes manifests;
- validate manifest syntax;
- detect deprecated APIs;
- apply enterprise policy checks;
- verify resource requests and limits;
- verify health probes;
- verify non-root execution;
- verify the required service account;
- verify that secret-store references are present;
- confirm that no public service exposure was introduced unintentionally.

Example policy results:

| Deployment rule | Actual result | Status |
| --- | --- | --- |
| Container runs as non-root | `runAsNonRoot: true` | Pass |
| CPU and memory limits exist | Limits declared | Pass |
| Readiness probe exists | `/health/ready` configured | Pass |
| Liveness probe exists | `/health/live` configured | Pass |
| Plaintext secrets prohibited | No plaintext secrets found | Pass |
| Approved service account required | `booking-api` service account configured | Pass |
| Public load balancer prohibited | Internal service only | Pass |

The Validator Agent does not deploy to production. It validates the deployment artifacts within the authority assigned to the validation workflow.

### Step 16: Classify the Results

Assume that all checks pass except one integration test:

```text
CreateCorporateFleetBooking_WithDuplicateIdempotencyKey_ShouldReturnExistingResult

Expected booking count: 1
Actual booking count: 2
```

The Validator Agent classifies the integration-test gate as **Fail**.

It does not:

- exclude the test;
- mark the test as flaky without evidence;
- rerun until it passes;
- change the expected count to two;
- claim that idempotency is optional;
- mark the overall validation as passed because most checks succeeded.

The result is recorded as:

```yaml
checkPerformed: Corporate fleet booking integration tests
expectedCondition: Repeated requests using the same idempotency key create one booking
actualResult: Two booking records were created
evidence:
  - artifacts/test-results/integration-tests.trx
  - artifacts/logs/idempotency-failure.log
  - artifacts/database/duplicate-bookings.json
status: Fail
failureDetails: >
  Concurrent requests using the same idempotency key created separate
  booking records. The uniqueness control did not prevent duplicate
  processing.
recommendedNextAction: >
  Return the implementation to the Developer Agent to correct the
  idempotency transaction or uniqueness handling. Review the correction
  and rerun the complete required validation profile.
```

The overall validation status is **Fail**.

### Step 17: Return the Failure for Correction

The Validator Agent sends a correction request to the Developer Agent through the Lead Agent or configured harness route.

The handoff includes:

- failed revision;
- failing gate;
- expected condition;
- actual result;
- reproducible test command;
- evidence locations;
- relevant logs;
- recommended next action;
- gates that passed;
- revalidation requirements.

The Validator Agent avoids prescribing an implementation unless the validation procedure specifically calls for diagnostic guidance. The Developer Agent remains responsible for choosing the correction.

### Step 18: Validate the Corrected Revision

The Developer Agent corrects the idempotency handling and submits revision `c719b42`. The Reviewer Agent independently reviews the correction.

The Validator Agent then:

1. Confirms that `c719b42` is the reviewed revision.
2. Executes the targeted failing test for rapid diagnosis.
3. Executes the full integration-test project.
4. Runs the complete validation profile required for the corrected change.
5. Generates a new evidence package.
6. Preserves the failed results from revision `8d34e7a`.
7. Associates all results with their exact revisions.

A targeted passing test is not enough for the final outcome. The correction may have affected transaction behavior, persistence, or event publication, so the required regression gates must run again.

### Example Final Validation Report

After the correction, the Validator Agent produces:

```markdown
# Validation Report

Task: ACD-CFB-142  
Feature: Corporate Fleet Booking  
Source revision: c719b42  
Reviewed revision: c719b42  
Validation profile: booking-service-v3  
Overall status: PASS

## Results

| Check | Expected condition | Actual result | Status |
|---|---|---|---|
| Revision integrity | Reviewed revision with clean workspace | Revision matched; workspace clean | Pass |
| Restore | Locked restore succeeds | Restore completed | Pass |
| Build | Release build succeeds with no warnings | 0 warnings, 0 errors | Pass |
| Domain tests | All tests pass | 42 of 42 passed | Pass |
| Application tests | All tests pass | 57 of 57 passed | Pass |
| Integration tests | All tests pass | 38 of 38 passed | Pass |
| Architecture tests | No prohibited dependencies | 12 of 12 passed | Pass |
| Static analysis | No prohibited findings | No findings above threshold | Pass |
| Security checks | No disallowed findings | No secrets; no high or critical vulnerabilities | Pass |
| API contract | Valid and backward compatible | No breaking changes | Pass |
| Event contract | Valid and backward compatible | Schema and compatibility checks passed | Pass |
| Database migration | Empty and upgrade scenarios succeed | Both scenarios passed | Pass |
| Configuration | Required settings bind securely | All checks passed | Pass |
| Deployment | Manifests satisfy approved policies | All policies passed | Pass |

## Previous Failure

Revision `8d34e7a` failed the duplicate-idempotency integration test.
Revision `c719b42` contains the reviewed correction and passed the full
validation profile.

## Evidence

- Build binary log
- Test result files
- Coverage report
- Architecture test report
- Static-analysis report
- Security reports
- OpenAPI compatibility report
- Event schema compatibility report
- Database migration report
- Configuration validation report
- Deployment policy report

## Recommended Next Action

Forward revision `c719b42` and its validation evidence package to the
Evaluator Agent. A passing validation result does not constitute production
approval.
```

### Outcome of the Example

The Validator Agent has established that revision `c719b42` satisfies the defined deterministic conditions. The result is traceable, reproducible, and bound to the reviewed source revision.

The Validator Agent has not established that:

- fleet coordinators find the workflow convenient;
- the feature satisfies every business expectation;
- production deployment risk is acceptable;
- a backward-incompatible future event change should be approved;
- the organization should release the feature.

Those questions remain with the Evaluator Agent and the authorized human decision-makers.

Chapter 17 status: In progress — next section: Claude Example

## Claude Example

Claude Code can act as the interactive agent interface for a Validator Agent, but deterministic validation must remain grounded in repository commands, test frameworks, scanners, contract tools, and governed validation policies.

Claude should coordinate validation, interpret machine-readable results, preserve evidence, and report outcomes. It should not replace executable gates with its own opinion.

### Validation Request

Assume that the corporate fleet booking implementation has completed development and review. The Lead Agent invokes Claude Code with a validation request similar to the following:

```text
Act as the Validator Agent for the Alpha Car Detailing repository.

Task:
Validate the reviewed corporate fleet booking implementation.

Source revision:
c719b42

Validation profile:
harness/validation/booking-service.yaml

Required checks:
- Verify revision integrity
- Restore and build the solution
- Run domain, application, integration, architecture, and contract tests
- Run static analysis
- Run security validation
- Validate the OpenAPI contract
- Validate the CorporateFleetBookingCreated event contract
- Validate the EF Core migration
- Validate configuration
- Validate deployment manifests
- Collect evidence for every check

Rules:
- Read all applicable repository Instructions before validation.
- Do not modify application code, tests, validation scripts, policies, or
  configuration.
- Do not skip failed gates.
- Do not rerun a failing test merely to obtain a pass.
- Do not expose secrets in output.
- Use only Pass, Fail, Blocked, or Inconclusive outcomes.
- Bind all evidence to revision c719b42.
- Stop and escalate if the validation profile is missing, ambiguous, or
  requires unavailable authorization.
- A passing validation result must be forwarded to the Evaluator Agent.
  It is not production approval.

For every check, report:
- Check performed
- Expected condition
- Actual result
- Evidence
- Status
- Failure details
- Recommended next action
```

This prompt supplies the current task and boundaries. The authoritative validation behavior should still come from repository-controlled Instructions, Skills, and the selected validation profile.

### Claude-First Validation Workflow

A disciplined Claude Code session follows five stages:

1. Discover the applicable validation controls.
2. Verify the source revision and environment.
3. Execute the mandatory checks.
4. collect complete evidence.
5. Classify and report the results without weakening the gates.

```mermaid
flowchart TD
    A["Read Instructions and profile"] --> B["Verify revision and environment"]
    B --> C["Execute required checks"]
    C --> D["Collect raw evidence"]
    D --> E{"Apply outcome rules"}
    E -->|Pass| F["Handoff to Evaluator Agent"]
    E -->|Fail| G["Return to Developer Agent"]
    E -->|Blocked or inconclusive| H["Escalate"]
```

### Stage 1: Discover the Validation Controls

Claude begins by locating and reading the authoritative files, which may include:

```text
CLAUDE.md
harness/roles/validator.md
harness/validation/booking-service.yaml
harness/instructions/testing.md
harness/instructions/security.md
harness/instructions/contracts.md
harness/instructions/database-migrations.md
harness/skills/validate-dotnet-service.md
```

Claude should also inspect:

- the solution and affected projects;
- test-project locations;
- existing validation scripts;
- CI workflow definitions;
- analyzer settings;
- security-scanner configuration;
- API and event contract baselines;
- migration ownership;
- deployment directories.

A suitable discovery summary might be:

```markdown
Validation controls discovered:

- Repository instructions: CLAUDE.md
- Validator role: harness/roles/validator.md
- Validation profile: booking-service-v3
- Build target: AlphaCarDetailing.sln
- Affected service: Booking
- Required test groups: Domain, Application, Integration, Architecture,
  Contract
- API baseline: contracts/api/booking-openapi.json
- Event baseline:
  contracts/events/corporate-fleet-booking-created.v1.json
- Migration owner: AlphaCarDetailing.Booking.Infrastructure
- Deployment manifests: deploy/booking
- Mandatory gate count: 13

No validation command has been executed yet.
```

Claude must distinguish discovery from execution. Finding a test project does not prove that its tests passed.

### Stage 2: Verify Revision and Environment

Claude verifies that the workspace represents the reviewed source revision:

```bash
git rev-parse HEAD
git status --porcelain
git diff --stat
```

It also inspects the required toolchain:

```bash
dotnet --info
dotnet tool restore
docker version
```

Claude records the output rather than merely stating that the environment appears correct.

If the current revision differs from `c719b42`, Claude should stop authoritative validation:

```markdown
Status: Blocked

Check performed:
Source revision integrity

Expected condition:
The workspace must contain reviewed revision c719b42.

Actual result:
The workspace contains revision d841e63.

Evidence:
git-revision.txt

Failure details:
The requested revision is not checked out. Evidence generated from the
current workspace would not apply to the reviewed implementation.

Recommended next action:
Provide or check out revision c719b42, verify that the workspace is clean,
and restart validation.
```

Claude must not decide that the revisions are “close enough.”

### Stage 3: Execute the Validation Profile

When the repository provides a governed validation script, Claude should prefer it over inventing an alternative sequence:

```powershell
.\harness\scripts\validate-booking-service.ps1 `
    -SourceRevision "c719b42" `
    -Profile "booking-service-v3" `
    -EvidenceDirectory ".\artifacts\validation\ACD-CFB-142"
```

The script may coordinate:

```powershell
dotnet restore .\AlphaCarDetailing.sln --locked-mode

dotnet build .\AlphaCarDetailing.sln `
    --configuration Release `
    --no-restore `
    -warnaserror

dotnet test .\AlphaCarDetailing.sln `
    --configuration Release `
    --no-build `
    --logger "trx" `
    --results-directory .\artifacts\validation\ACD-CFB-142\tests
```

However, Claude must verify what the script actually covers. A script named `validate-booking-service.ps1` may run only the build and unit tests while the profile also requires contract, security, migration, and deployment checks.

Claude should compare:

- required gates declared by the profile;
- gates invoked by the script;
- reports actually produced;
- exit codes returned;
- checks omitted or skipped.

A command name is not evidence of complete coverage.

### Handling Command Failures

Suppose the build succeeds but an architecture test fails:

```text
Failed AlphaCarDetailing.Booking.ArchitectureTests
  Application_ShouldNotDependOnInfrastructure

Detected dependency:
AlphaCarDetailing.Booking.Application.Bookings.CreateCorporateFleetBookingHandler
  ->
AlphaCarDetailing.Booking.Infrastructure.Persistence.BookingDbContext
```

Claude should record the failure and stop any workflow stage that requires architecture validation to pass before continuing.

A suitable report is:

```markdown
### Architecture dependency validation

Check performed:
Execute the Booking service architecture test suite.

Expected condition:
The Application layer must not depend on the Infrastructure layer.

Actual result:
CreateCorporateFleetBookingHandler depends directly on BookingDbContext
from the Infrastructure project.

Evidence:
- architecture-tests.trx
- architecture-test-output.log
- dependency-path.txt

Status:
Fail

Failure details:
The dependency violates the repository's Clean Architecture rule and the
mandatory booking-service-v3 architecture gate.

Recommended next action:
Return the implementation to the Developer Agent. Replace the direct
infrastructure dependency with the approved application-owned abstraction,
submit a new revision, and obtain independent review before revalidation.
```

Claude must not edit the architecture test or add an exception to make the gate pass.

### Using Claude for Diagnostics

Claude can assist with deterministic failure diagnosis while remaining within the Validator Agent boundary.

For the architecture failure, Claude may:

- locate the dependency reported by the architecture test;
- identify the source file and project reference;
- compare it with the repository rule;
- capture the dependency path;
- explain why the test failed.

Claude should not modify the handler unless it has been explicitly reassigned to the Developer Agent role through a new governed task.

Role boundaries remain important even when one AI platform can technically perform both activities.

### Handling a Flaky Test

Assume an integration test fails with a timeout, passes on the next run, and fails again on a third execution under materially identical conditions.

Claude should not report Pass based on the successful attempt.

Instead, it should preserve all attempts:

| Attempt | Revision | Environment | Result |
| --- | --- | --- | --- |
| 1 | `c719b42` | Validation runner 27 | Fail—timeout |
| 2 | `c719b42` | Validation runner 27 | Pass |
| 3 | `c719b42` | Validation runner 27 | Fail—timeout |

The result should be classified according to the approved flaky-test policy. In the absence of a policy allowing another classification, **Inconclusive** is appropriate:

```markdown
Status: Inconclusive

Failure details:
The same integration test produced inconsistent results under materially
identical conditions. The evidence does not support a reliable pass.

Recommended next action:
Quarantine is not authorized. Escalate the unstable test for investigation,
identify whether the source is product behavior, shared infrastructure, test
isolation, or timing assumptions, and rerun the required profile only after
the issue is addressed.
```

### Handling an Unavailable Dependency

Suppose event compatibility validation requires access to the organization’s schema registry, but Claude lacks authorization.

The correct result is **Blocked**:

```markdown
### Event contract compatibility

Check performed:
Compare CorporateFleetBookingCreated schema with the registered version.

Expected condition:
The proposed schema must satisfy the configured backward-compatibility
policy.

Actual result:
The schema registry rejected the request because the validation identity
does not have read access.

Evidence:
- schema-registry-command.log
- sanitized-access-error.json

Status:
Blocked

Failure details:
The required compatibility comparison could not be executed.

Recommended next action:
Grant the validation identity approved read access or provide an
authoritative exported baseline. Do not advance the implementation to the
Evaluator Agent until the required check completes.
```

Claude must not infer compatibility by visually examining the schemas and mark the deterministic gate as passed. It may provide an advisory comparison, but the required gate remains blocked.

### Preventing Secret Exposure

Claude may encounter configuration containing credentials, tokens, connection strings, or certificate data. Validation evidence should identify whether required secret references exist without recording secret values.

Unsafe evidence:

```text
BookingDatabasePassword=P@ssword123
```

Acceptable evidence:

```text
Configuration key: BookingDatabase
Source: Approved secret-store reference
Binding result: Success
Secret value captured: No
Status: Pass
```

Prompts and Instructions should explicitly prohibit Claude from printing environment variables, secret files, credentials, or raw configuration sources containing protected values.

### Validating API Compatibility

Claude can coordinate API validation by:

1. generating the OpenAPI document from the reviewed revision;
2. validating its syntax;
3. comparing it with the approved baseline;
4. storing the generated contract and compatibility report;
5. applying the configured breaking-change policy.

Suppose the tool reports:

```text
Breaking change:
POST /api/corporate-fleet-bookings
Request property "stationId" changed from required to optional.
```

Claude reports **Fail** even if it reasons that optional station selection could support future business requirements. That reasoning belongs in a separate design decision. The deterministic rule detected an unapproved breaking change.

### Validating Event Compatibility

For the `CorporateFleetBookingCreated` event, Claude should validate:

- the JSON Schema document;
- a representative serialized event;
- the registered compatibility policy;
- schema metadata;
- consumer deserialization tests where required.

Claude can summarize the result, but the compatibility tool’s report remains the primary evidence:

```markdown
Check performed:
Backward-compatibility validation for CorporateFleetBookingCreated v1.

Expected condition:
No required field may be removed or weakened without an approved versioned
contract change.

Actual result:
No incompatible differences detected.

Evidence:
- corporate-fleet-booking-created.generated.json
- event-schema-validation.json
- event-compatibility-report.json
- contract-tests.trx

Status:
Pass

Failure details:
None.

Recommended next action:
Continue with the remaining mandatory gates.
```

### Validating Database Migrations

Claude can orchestrate migration validation in disposable databases:

1. create an empty database;
2. apply the complete migration chain;
3. record the schema and result;
4. create a database at the previous supported version;
5. load sanitized representative data;
6. apply the proposed migration;
7. execute post-migration checks;
8. preserve migration logs and schema differences.

Claude must not delete or modify production data. It must also avoid interpreting an empty-database success as proof of upgrade safety.

If the migration includes an unapproved destructive operation, Claude reports it:

```markdown
Status: Fail

Failure details:
The generated migration drops CorporateFleetBookings.ExternalReference.
The booking-service-v3 profile prohibits destructive column removal without
an approved data-retention and rollout decision.

Recommended next action:
Escalate the schema change to the responsible architect and database owner.
The Developer Agent must not suppress the destructive-operation check.
```

### Producing a Claude Validation Report

After executing all required checks, Claude produces both:

- a concise human-readable report;
- machine-readable results for the harness.

Example human-readable report:

```markdown
# Corporate Fleet Booking Validation Report

Task: ACD-CFB-142
Source revision: c719b42
Reviewed revision: c719b42
Profile: booking-service-v3
Overall status: PASS

## Gate Summary

| Gate | Status | Evidence |
|---|---|---|
| Revision integrity | Pass | revision-integrity.json |
| Restore and build | Pass | build.binlog |
| Domain tests | Pass | domain-tests.trx |
| Application tests | Pass | application-tests.trx |
| Integration tests | Pass | integration-tests.trx |
| Architecture tests | Pass | architecture-tests.trx |
| Static analysis | Pass | static-analysis.sarif |
| Security validation | Pass | security-results.sarif |
| API contract | Pass | openapi-diff.json |
| Event contract | Pass | event-compatibility.json |
| Database migration | Pass | migration-report.json |
| Configuration | Pass | configuration-report.json |
| Deployment manifests | Pass | deployment-policy.json |

All mandatory gates completed successfully. No required check was skipped.

Recommended next action:
Forward revision c719b42 and this evidence package to the Evaluator Agent.
This result does not authorize merge, release, or production deployment.
```

The corresponding machine-readable output might be:

```json
{
  "taskId": "ACD-CFB-142",
  "sourceRevision": "c719b42",
  "reviewedRevision": "c719b42",
  "profile": "booking-service-v3",
  "status": "Pass",
  "requiredChecks": 13,
  "executedChecks": 13,
  "passedChecks": 13,
  "failedChecks": 0,
  "blockedChecks": 0,
  "inconclusiveChecks": 0,
  "skippedChecks": 0,
  "recommendedNextAction": "Forward to Evaluator Agent",
  "humanApprovalRequired": true
}
```

### Claude Instruction File Example

A repository may define the Validator Agent’s permanent boundaries in a Claude-specific instruction file while keeping the underlying principles vendor-neutral:

```markdown
# Validator Agent

You verify objective and repeatable engineering conditions.

## Required behavior

- Read the assigned validation profile before running commands.
- Validate only the specified reviewed revision.
- Execute every mandatory gate.
- Preserve raw evidence and exit codes.
- Report Pass, Fail, Blocked, or Inconclusive.
- Stop advancement when a mandatory gate does not pass.
- Redact secrets from all output.
- Record every retry and its reason.
- Forward successful validation to the Evaluator Agent.

## Prohibited behavior

- Do not modify implementation code.
- Do not edit tests to make validation pass.
- Do not weaken rules, thresholds, baselines, or policies.
- Do not skip failed checks.
- Do not hide missing evidence.
- Do not convert blocked or inconclusive results into passes.
- Do not treat compilation as complete validation.
- Do not approve merge, release, or production deployment.
```

This file helps Claude maintain its role, but enforcement should not rely exclusively on instructions. The harness should also use:

- read-only validation workspaces where practical;
- protected branches and validation files;
- controlled CI identities;
- mandatory pipeline gates;
- artifact retention;
- policy-as-code;
- independent approval rules.

### Claude-Specific Strengths

Claude Code is particularly useful when validation requires:

- understanding repository-specific instructions;
- discovering validation entry points across a large codebase;
- connecting failures to relevant code and architecture rules;
- summarizing logs without discarding raw evidence;
- coordinating multiple command-line tools;
- preparing precise correction reports;
- maintaining context across a multi-stage validation session.

These capabilities improve orchestration and diagnosis. They do not transform subjective language-model reasoning into deterministic proof.

### Claude-Specific Risks

Teams should control several risks:

- Claude may infer that an omitted check is unnecessary.
- It may summarize a failure too aggressively and omit evidence.
- It may attempt a helpful code correction while acting as Validator.
- It may rerun a command and report only the successful result.
- It may accept its own visual inspection instead of invoking the required tool.
- It may expose secrets while collecting configuration diagnostics.
- It may validate the current workspace without confirming the revision.
- It may interpret “mostly passed” as sufficient progress.

The harness should counter these risks with executable profiles, protected gates, structured result schemas, evidence requirements, and explicit role boundaries.

### Appropriate Claude Handoff

A passing Claude validation session ends with a controlled handoff:

```text
Validation completed for revision c719b42.

Overall result: Pass
Mandatory gates executed: 13 of 13
Mandatory gates passed: 13 of 13
Skipped gates: 0
Evidence package: artifacts/validation/ACD-CFB-142

Next role: Evaluator Agent

This result verifies the configured deterministic engineering conditions.
It does not constitute business acceptance, merge approval, release
approval, or production authorization.
```

A failing session returns to the Developer Agent through the governed workflow. A blocked or inconclusive session escalates to the role authorized to resolve the missing prerequisite or ambiguous condition.

In every case, Claude’s most important responsibility as the Validator Agent is not to sound confident. It is to preserve the integrity of the validation process.

Chapter 17 status: In progress — next section: GitHub Copilot Comparison

## GitHub Copilot Comparison

GitHub Copilot can support Validator Agent activities within the developer workspace and GitHub delivery workflow. It can help discover repository validation commands, execute tests, interpret failures, summarize workflow results, and prepare structured validation reports.

The underlying principle remains unchanged:

> GitHub Copilot can coordinate and explain validation, but builds, tests, analyzers, scanners, policy engines, and contract tools produce the deterministic evidence.

The effectiveness of Copilot as a Validator Agent depends on how it is used. An informal chat request such as “check whether this code is good” does not create an authoritative validation process. The repository must supply explicit Instructions, protected validation profiles, repeatable commands, outcome rules, and evidence requirements.

### Copilot Validation Context

For the Alpha Car Detailing corporate fleet booking change, Copilot may operate through several surfaces:

- an IDE or editor;
- an agent-capable coding workflow;
- repository prompt files;
- pull-request assistance;
- GitHub Actions;
- command-line and terminal integrations;
- repository Instructions.

These surfaces provide different levels of isolation and authority. A local IDE session is useful for early checks and diagnosis, while a controlled GitHub Actions workflow is generally better suited to authoritative validation evidence.

### Repository Instructions

The team can encode permanent validation boundaries in repository instruction files. The exact mechanism may differ across Copilot environments, but the content should remain vendor-neutral.

An example instruction file might contain:

```markdown
# Validator Agent Instructions

When acting as the Validator Agent:

- Read the assigned validation profile before running any check.
- Confirm the exact reviewed commit before validation.
- Treat repository scripts and CI workflows as authoritative.
- Execute every mandatory validation gate.
- Preserve commands, exit codes, logs, and machine-readable reports.
- Use only Pass, Fail, Blocked, or Inconclusive outcomes.
- Record every skipped check and retry.
- Redact credentials and protected configuration values.
- Forward a passing result to the Evaluator Agent.

Do not:

- Modify implementation code while validating.
- Change tests, baselines, thresholds, or policies to obtain a pass.
- Ignore failed tests or security findings.
- Re-run flaky tests until one passes.
- Treat compilation as complete validation.
- Convert blocked or inconclusive results into passes.
- Approve merge, release, or production deployment.
```

These Instructions guide Copilot, but technical controls must enforce critical boundaries independently. Branch protection, required checks, restricted workflow permissions, protected environments, and review requirements must not depend solely on the model following written directions.

### Reusable Validation Prompt

A repository prompt can standardize how users invoke Copilot for validation:

```markdown
---
description: Validate a reviewed Alpha Car Detailing change
---

Act as the Validator Agent.

Inputs:

- Task ID: ${input:taskId}
- Reviewed commit: ${input:reviewedCommit}
- Validation profile: ${input:validationProfile}

Process:

1. Read the repository Instructions and assigned validation profile.
2. Confirm that the workspace contains the reviewed commit.
3. Identify every mandatory gate.
4. Execute the repository-provided validation commands.
5. Preserve raw and structured evidence.
6. Apply the configured outcome rules.
7. Produce a validation report.

For every check, include:

- Check performed
- Expected condition
- Actual result
- Evidence
- Status
- Failure details
- Recommended next action

Constraints:

- Do not modify code, tests, gates, baselines, or policies.
- Do not skip or conceal failed checks.
- Do not expose secrets.
- Do not report Pass if any mandatory gate failed, was blocked, or was
  inconclusive.
- Do not replace the Evaluator Agent or human approval.
```

A shared prompt reduces variation across validation sessions. It should reference repository-controlled validation logic rather than duplicating every command inside the prompt.

### Local Validation with Copilot

A developer or validator can ask Copilot to execute the approved local workflow:

```text
Validate task ACD-CFB-142 at reviewed commit c719b42 using
harness/validation/booking-service.yaml.

Do not modify any files. First show the required gates resolved from the
profile. Then run the approved validation script and collect evidence under
artifacts/validation/ACD-CFB-142.

Stop advancement if any mandatory gate does not pass.
```

Copilot may then use repository commands such as:

```powershell
.\harness\scripts\validate-booking-service.ps1 `
    -SourceRevision "c719b42" `
    -Profile "booking-service-v3" `
    -EvidenceDirectory ".\artifacts\validation\ACD-CFB-142"
```

Before accepting the result, the user or harness should confirm that:

- the correct commit was validated;
- the working tree remained unchanged;
- every mandatory gate executed;
- expected reports were generated;
- skipped tests were identified;
- retry history was preserved;
- the final classification matches policy.

Local execution provides fast feedback, but its evidence may be less authoritative than controlled pipeline evidence because local tool versions, cached dependencies, environment variables, and running services can affect results.

### Authoritative Validation in GitHub Actions

GitHub Actions can provide a controlled execution environment for deterministic validation. Copilot can help create, explain, diagnose, and summarize the workflow, but the workflow itself should remain repository-controlled and independently reviewed.

A simplified workflow might be:

```yaml
name: Validate Booking Service

on:
  pull_request:
    paths:
      - "src/Services/Booking/**"
      - "tests/Services/Booking/**"
      - "contracts/api/**"
      - "contracts/events/**"
      - "deploy/booking/**"
      - "harness/validation/**"

permissions:
  contents: read
  security-events: write

jobs:
  validate:
    runs-on: ubuntu-latest
    timeout-minutes: 30

    steps:
      - name: Check out reviewed revision
        uses: actions/checkout@v4
        with:
          persist-credentials: false

      - name: Set up .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: "10.0.x"

      - name: Restore tools
        run: dotnet tool restore

      - name: Validate Booking service
        shell: pwsh
        run: |
          ./harness/scripts/validate-booking-service.ps1 `
            -SourceRevision "${{ github.event.pull_request.head.sha }}" `
            -Profile "booking-service-v3" `
            -EvidenceDirectory "./artifacts/validation"

      - name: Upload validation evidence
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: booking-validation-evidence
          path: artifacts/validation
          if-no-files-found: error
          retention-days: 30
```

The use of `if: always()` for evidence upload is important. Failure evidence is often more valuable than success output. A workflow that uploads reports only after a pass can conceal the information needed for diagnosis.

In a production repository, actions should be pinned according to organizational supply-chain policy, permissions should follow least privilege, and sensitive deployment environments should remain protected.

### Required Checks and Branch Protection

The authoritative validation workflow should be configured as a required check before merge. This prevents Copilot, a developer, or another agent from advancing a change based only on a conversational statement that validation passed.

For the corporate fleet booking pull request, required checks might include:

- `booking/build`;
- `booking/unit-tests`;
- `booking/integration-tests`;
- `booking/architecture`;
- `booking/static-analysis`;
- `booking/security`;
- `booking/api-contract`;
- `booking/event-contract`;
- `booking/migration`;
- `booking/deployment-policy`.

The organization may use one aggregate workflow or several independent workflows. Separate checks improve visibility and allow targeted ownership, while an aggregate gate can simplify overall status enforcement.

The critical requirement is that a missing, cancelled, or inconclusive mandatory check must not appear as a successful validation.

### Copilot and Pull-Request Validation

Copilot can help summarize validation evidence on a pull request. For example:

```markdown
## Validator Agent Result

Task: ACD-CFB-142  
Commit: c719b42  
Profile: booking-service-v3  
Overall status: FAIL

| Gate | Status | Evidence |
|---|---|---|
| Build | Pass | build.binlog |
| Domain tests | Pass | domain-tests.trx |
| Application tests | Pass | application-tests.trx |
| Integration tests | Fail | integration-tests.trx |
| Architecture tests | Pass | architecture-tests.trx |
| Static analysis | Pass | static-analysis.sarif |
| Security | Pass | security-results.sarif |
| API contract | Pass | openapi-diff.json |
| Event contract | Pass | event-compatibility.json |
| Migration | Pass | migration-report.json |
| Configuration | Pass | configuration-report.json |
| Deployment policy | Pass | deployment-policy.json |

### Failure

The duplicate-idempotency integration scenario created two booking records.

Expected:
One logical booking for repeated requests with the same idempotency key.

Actual:
Two records were persisted.

Recommended next action:
Return the change to the Developer Agent. After correction and independent
review, run the complete booking-service-v3 profile against the new commit.
```

The summary makes results accessible, but it should link to or reference raw artifacts. It must not replace them.

### Handling Failed Tests

Copilot is effective at locating the code associated with a failed test and explaining possible causes. That capability must be used carefully within the Validator Agent role.

Copilot may:

- identify the failing test and assertion;
- locate the affected implementation;
- correlate logs with the failure;
- show a reproducible command;
- summarize the violated condition;
- recommend that the Developer Agent investigate a specific area.

Copilot must not, while acting as Validator:

- edit the implementation;
- update expected values;
- disable the test;
- add a skip attribute;
- change the workflow filter;
- weaken the coverage threshold;
- modify the baseline.

If implementation work is needed, the harness creates a new Developer Agent task. This preserves responsibility even if Copilot is used for both roles at different stages.

### Detecting Test Selection Problems

A common weakness in automated validation is incomplete test selection. A command can succeed while failing to discover or execute required tests.

Copilot should compare:

- expected test projects;
- discovered test projects;
- expected test categories;
- executed tests;
- skipped tests;
- filters passed to the test runner;
- produced result files.

Suppose the profile expects five test projects, but only four `.trx` files are generated. The correct result is not Pass.

A suitable classification is:

```markdown
Status: Inconclusive

Failure details:
The validation profile requires five test groups, but evidence was produced
for only four. No contract-test result was generated, and the available logs
do not prove that the contract tests executed.

Recommended next action:
Correct the validation workflow or provide the missing report, then rerun
the validation profile. Do not merge based on the incomplete evidence.
```

### Handling Workflow Retries

GitHub allows failed workflow jobs to be rerun. This is operationally useful but can hide unreliable tests if the history is ignored.

The validation report must preserve:

- the original failed attempt;
- who or what requested the rerun;
- the stated reason;
- whether the source commit changed;
- whether the runner or environment changed;
- every subsequent result.

A passing rerun does not automatically erase the original failure. If the same commit produces inconsistent results under comparable conditions, the gate should follow the repository’s flaky-test policy.

Copilot can summarize retry history:

```markdown
The integration-test job ran three times for commit c719b42:

1. Failed due to test timeout.
2. Passed without source changes.
3. Failed due to the same timeout.

Classification: Inconclusive.

The inconsistent outcomes require investigation. Reporting only attempt 2
would misrepresent the available evidence.
```

### Contract Compatibility in Pull Requests

Copilot can explain an API or event contract diff, but the compatibility status must come from the approved comparison tool and policy.

For example:

```text
Event: CorporateFleetBookingCreated
Baseline version: 1.0
Proposed version: 1.0
Detected change: stationId changed from required to optional
Compatibility policy: backward
Tool result: incompatible
```

Copilot should report **Fail**. It must not reinterpret the change as harmless because consumers “probably handle null.”

If the contract change is intentional, it requires a separate versioning and governance decision. The Validator Agent cannot approve the exception.

### Migration Validation in CI

Database migration validation often requires more than generating SQL. The GitHub Actions workflow can:

1. start a disposable SQL Server container;
2. apply the full migration chain to an empty database;
3. restore a sanitized previous-version database;
4. apply the proposed migration;
5. run post-migration verification;
6. preserve generated SQL and execution logs;
7. destroy the disposable environment.

Copilot may help diagnose failures, but it should not automatically modify migration files or delete representative data to obtain a pass.

A migration result might be:

```markdown
Status: Fail

Expected condition:
Upgrade from the previous supported schema completes without data loss.

Actual result:
Migration failed while changing StationId to non-nullable because 14
representative rows contained null values.

Evidence:
- migration-script.sql
- upgrade-execution.log
- post-migration-check.json

Recommended next action:
Return the change to the Developer Agent and database owner. Define and
review an explicit backfill or staged migration strategy before
revalidation.
```

### Security Findings and Code Scanning

GitHub’s security and code-scanning capabilities can produce machine-readable findings that Copilot helps interpret.

The validation policy should define:

- enabled scanners;
- severity thresholds;
- treatment of new versus existing findings;
- suppression authority;
- evidence-retention requirements;
- response rules for unavailable scanners.

Copilot must not dismiss a security finding because the vulnerable path appears unlikely to execute. Risk acceptance belongs to an authorized security or human decision-maker.

If a security scanner is unavailable, the result is **Blocked** rather than Pass. If the scanner completed but returned an unreadable report, the result may be **Inconclusive**.

### Protecting Workflow and Gate Changes

A pull request that modifies the implementation and its validation workflow creates a special risk. For example, a change could:

- remove the integration-test job;
- narrow the workflow path filter;
- exclude a test category;
- reduce a coverage threshold;
- change a scanner from blocking to advisory;
- add a vulnerability suppression;
- alter an API compatibility baseline.

GitHub controls should require additional ownership or approval for these files. A `CODEOWNERS` policy might protect:

```text
/.github/workflows/              @platform-engineering
/harness/validation/             @architecture-team
/harness/scripts/                @platform-engineering
/tests/**/ArchitectureTests/     @architecture-team
/contracts/                      @api-governance
/security/                       @security-team
```

Copilot should explicitly report validation-control changes:

```markdown
Validation-control change detected:

- .github/workflows/booking-validation.yml
- harness/validation/booking-service.yaml

Authoritative validation cannot rely solely on gates changed by the same
revision without the required independent approval.

Status: Blocked
Recommended next action: Obtain review from the configured control owners.
```

### Copilot Strengths for Validation

GitHub Copilot is particularly useful for:

- validation close to the source repository;
- discovering build and test commands;
- interpreting compiler and test failures;
- explaining pull-request changes alongside validation results;
- generating structured pull-request summaries;
- assisting with GitHub Actions workflows;
- correlating findings with changed files;
- helping developers reproduce CI failures locally.

Its proximity to GitHub workflows can make evidence visible where code review and merge decisions occur.

### Copilot Limitations and Risks

Teams should account for several limitations and risks:

- Local Copilot sessions may depend on uncontrolled workstation state.
- Repository context may be incomplete unless Instructions are explicit.
- Copilot may propose fixing code while assigned to validation.
- It may treat a successful rerun as sufficient without preserving prior failures.
- It may summarize pipeline output without checking whether all gates executed.
- It may rely on workflow names instead of inspecting their actual behavior.
- It may suggest changing tests or baselines as the quickest correction.
- Pull-request commentary may be mistaken for authoritative validation.
- Agent-generated workflow changes can weaken gates if not independently reviewed.

These risks are controlled through repository governance, not through model confidence.

### Claude and GitHub Copilot Comparison

Claude Code and GitHub Copilot can both support the Validator Agent role. Their practical differences relate primarily to workflow integration and operating context.

| Area | Claude Code | GitHub Copilot |
| --- | --- | --- |
| Primary operating context | Repository and command-line agent workflow | IDE, repository, pull request, and GitHub workflow |
| Instruction source | Claude-oriented repository files plus harness Instructions | Copilot-oriented repository files plus harness Instructions |
| Local validation | Strong interactive command orchestration | Strong editor-integrated validation and diagnosis |
| Pull-request integration | Depends on the surrounding harness | Naturally aligned with GitHub workflows |
| CI evidence | Consumes or coordinates external pipeline evidence | Closely integrated with GitHub Actions and checks |
| Failure diagnosis | Strong repository-wide investigation | Strong changed-file and developer-workflow context |
| Deterministic authority | Comes from invoked tools and governed gates | Comes from invoked tools, Actions, and governed gates |
| Can replace validation tools | No | No |
| Can replace Evaluator or human approval | No | No |

The handbook remains Claude-first because Claude is the primary development platform in the running enterprise strategy. However, the role model is portable. The same Validator Agent contract can be implemented through Copilot when the organization’s workflow is centered on GitHub.

### Appropriate Copilot Handoff

After a complete passing run, Copilot can post or generate the following handoff:

```text
Validation completed for task ACD-CFB-142.

Validated commit: c719b42
Validation profile: booking-service-v3
Overall result: Pass
Mandatory checks executed: 13 of 13
Mandatory checks passed: 13 of 13
Skipped checks: 0
Unresolved findings: 0

Evidence is attached to the required GitHub Actions run.

Next role: Evaluator Agent

This result verifies the configured deterministic conditions. It does not
constitute merge approval, business acceptance, release approval, or
production authorization.
```

This keeps the boundary clear: GitHub Copilot helps place reliable validation evidence into the engineering workflow, but the evidence—not Copilot’s confidence—determines the validation result.

Chapter 17 status: In progress — next section: Codex Comparison

## Codex Comparison

OpenAI Codex can perform the Validator Agent role by inspecting repository Instructions, operating within an isolated workspace, executing approved commands, collecting evidence, and reporting deterministic outcomes. It is especially useful when validation requires coordinated repository exploration, terminal execution, failure diagnosis, and artifact generation.

As with Claude Code and GitHub Copilot, Codex is not itself the deterministic gate. The build system, test runner, architecture rules, analyzers, security scanners, contract tools, migration checks, and policy engines produce the authoritative evidence.

> **Key Principle**
>
> Codex may coordinate validation and interpret its output, but its natural-language assessment must not replace an executable check.

### Codex Validation Request

For the Alpha Car Detailing corporate fleet booking implementation, Codex can receive a task such as:

```text
Act as the Validator Agent for Alpha Car Detailing.

Task ID:
ACD-CFB-142

Reviewed source revision:
c719b42

Validation profile:
harness/validation/booking-service.yaml

Validate:
- Source revision integrity
- Dependency restore
- Release build
- Domain tests
- Application tests
- Integration tests
- Architecture tests
- Static analysis
- Security controls
- API contract compatibility
- Event contract compatibility
- EF Core migration safety
- Configuration
- Deployment manifests

Requirements:
- Read all applicable repository Instructions before execution.
- Use existing validation scripts and Skills where available.
- Do not modify source code, tests, contracts, baselines, thresholds,
  policies, or workflows.
- Do not skip failed gates.
- Preserve raw output and machine-readable reports.
- Record every retry.
- Redact secrets.
- Classify every check as Pass, Fail, Blocked, or Inconclusive.
- Bind all evidence to revision c719b42.
- Do not treat a successful build as complete validation.
- Do not approve merge, release, or production deployment.

For every result, include:
- Check performed
- Expected condition
- Actual result
- Evidence
- Status
- Failure details
- Recommended next action
```

A strong request describes the role and its boundaries while delegating the exact commands to repository-controlled definitions.

### Codex Validation Workflow

A governed Codex validation workflow follows this sequence:

```mermaid
flowchart TD
    A["Read repository Instructions"] --> B["Resolve validation profile"]
    B --> C["Verify reviewed revision"]
    C --> D["Inspect validation environment"]
    D --> E["Execute approved gates"]
    E --> F["Preserve evidence"]
    F --> G{"Classify outcome"}
    G -->|Pass| H["Send to Evaluator Agent"]
    G -->|Fail| I["Return to Developer Agent"]
    G -->|Blocked or inconclusive| J["Escalate"]
```

The workflow should use a clean workspace or isolated checkout. This reduces the chance that uncommitted files, cached artifacts, or developer-specific configuration will influence the result.

### Reading Repository Instructions

Before running commands, Codex should discover and read the repository’s governing files. Depending on the repository, these may include:

```text
AGENTS.md
harness/roles/validator.md
harness/validation/booking-service.yaml
harness/instructions/testing.md
harness/instructions/security.md
harness/instructions/contracts.md
harness/instructions/database-migrations.md
harness/skills/validate-dotnet-service/
```

`AGENTS.md` files may apply at different directory levels. Codex must determine which Instructions govern each affected component.

For example:

- the root Instructions may define general evidence requirements;
- Booking service Instructions may define service-specific test commands;
- contract Instructions may define compatibility rules;
- deployment Instructions may define Kubernetes policy checks;
- migration Instructions may require both empty-database and upgrade testing.

Codex should report the Instructions and profile used so another participant can reconstruct the validation basis.

### Planning Without Redefining the Gates

Codex may create an execution plan to coordinate a large validation task. The plan must reflect the approved validation profile rather than becoming a new source of validation policy.

A suitable plan might be:

```text
1. Verify the repository revision and clean workspace.
2. Resolve the mandatory gates from booking-service-v3.
3. Verify required SDKs, containers, tools, and permissions.
4. Run build and test gates.
5. Run architecture, static-analysis, and security gates.
6. Run API and event contract gates.
7. Run migration, configuration, and deployment gates.
8. verify that evidence exists for every mandatory check.
9. Aggregate results according to the approved outcome policy.
```

Codex must not drop an expensive or inconvenient check merely to simplify the plan.

### Using Existing Validation Automation

When the repository includes an approved validation script, Codex should use it:

```powershell
.\harness\scripts\validate-booking-service.ps1 `
    -SourceRevision "c719b42" `
    -Profile "booking-service-v3" `
    -EvidenceDirectory ".\artifacts\validation\ACD-CFB-142"
```

Codex then verifies that the script:

- checked the requested revision;
- invoked every mandatory gate;
- produced the expected artifacts;
- returned meaningful status information;
- did not silently ignore errors;
- recorded skipped checks;
- preserved evidence after failure.

If no approved script exists, Codex may execute the commands defined by the profile individually. It should not invent substitute tools when the profile requires a specific authoritative control.

### Workspace and Revision Integrity

Codex should establish the validation target before executing any gate:

```bash
git rev-parse HEAD
git status --porcelain
git diff --stat
```

The source revision must match the reviewed revision. The workspace must also be clean unless the validation request explicitly identifies and authorizes additional generated inputs.

If Codex detects local changes, it must not delete, reset, overwrite, or ignore them automatically. It should classify authoritative validation as blocked:

```markdown
### Source integrity

Check performed:
Verify the source revision and working-tree state.

Expected condition:
Revision c719b42 with no uncommitted changes.

Actual result:
Revision c719b42 is checked out, but two uncommitted files are present.

Evidence:
- git-revision.txt
- git-status.txt

Status:
Blocked

Failure details:
The uncommitted changes make the validation input ambiguous.

Recommended next action:
Provide a clean isolated workspace containing the reviewed revision. Preserve
the current user changes; do not remove them as part of validation.
```

This behavior protects both evidence integrity and user work.

### Safe Command Execution

Codex can execute terminal commands, but validation authority should be constrained to the task.

Safe validation operations include:

- reading source and configuration;
- restoring approved dependencies;
- compiling projects;
- running tests and analyzers;
- starting disposable test containers;
- generating temporary contracts;
- creating validation artifacts;
- applying migrations to disposable databases;
- rendering deployment manifests.

Operations requiring explicit authorization include:

- changing production infrastructure;
- applying migrations to shared or production databases;
- rotating or reading protected secrets;
- deleting user files;
- modifying branch protection;
- weakening pipeline requirements;
- publishing artifacts;
- merging or deploying.

Codex should use disposable resources for database, contract, and infrastructure validation whenever practical.

### Evidence as a Deliverable

Codex is well suited to collecting command output into a structured evidence directory:

```text
artifacts/
└── validation/
    └── ACD-CFB-142/
        ├── manifest.json
        ├── revision/
        │   ├── commit.txt
        │   └── workspace-status.txt
        ├── build/
        │   ├── build.log
        │   └── build.binlog
        ├── tests/
        │   ├── domain-tests.trx
        │   ├── application-tests.trx
        │   ├── integration-tests.trx
        │   ├── architecture-tests.trx
        │   └── contract-tests.trx
        ├── analysis/
        │   ├── static-analysis.sarif
        │   └── security-results.sarif
        ├── contracts/
        │   ├── openapi-diff.json
        │   └── event-compatibility.json
        ├── database/
        │   ├── migration-script.sql
        │   └── migration-report.json
        ├── configuration/
        │   └── configuration-report.json
        ├── deployment/
        │   └── policy-report.json
        └── validation-report.md
```

The evidence manifest should identify:

- task identifier;
- reviewed revision;
- validation profile and version;
- tool versions;
- execution timestamps;
- required checks;
- actual checks executed;
- evidence files;
- retry history;
- status of each gate;
- overall status.

Codex’s summary is useful for navigation, but raw reports must remain available for inspection.

### Verifying Evidence Completeness

Codex should compare expected evidence with actual evidence before reporting an overall result.

For example:

| Required evidence | Expected artifact | Actual artifact | Result |
| --- | --- | --- | --- |
| Build | `build.binlog` | Present | Complete |
| Domain tests | `domain-tests.trx` | Present | Complete |
| Application tests | `application-tests.trx` | Present | Complete |
| Integration tests | `integration-tests.trx` | Present | Complete |
| Architecture tests | `architecture-tests.trx` | Present | Complete |
| Contract tests | `contract-tests.trx` | Missing | Incomplete |
| Security | `security-results.sarif` | Present | Complete |

Even if every executed check passed, the missing contract-test report prevents a complete Pass. Codex should classify the result as **Inconclusive** or **Fail** according to the validation policy.

It must not assume that the missing report means the tests had nothing to report.

### Diagnosing a Build Failure

Suppose the Booking service fails to compile:

```text
CreateCorporateFleetBookingHandler.cs(47,19):
error CS1061: 'IBookingRepository' does not contain a definition for
'FindByIdempotencyKeyAsync'
```

Codex may inspect:

- the interface definition;
- implementation class;
- changed files;
- compiler output;
- relevant project references.

It can then produce:

```markdown
### Release build

Check performed:
Build AlphaCarDetailing.sln in Release mode with warnings treated as errors.

Expected condition:
All projects compile with zero errors and no prohibited warnings.

Actual result:
The Application project failed with CS1061. The handler calls
FindByIdempotencyKeyAsync, but the referenced IBookingRepository contract
does not define that member.

Evidence:
- build.log
- build.binlog
- compiler-diagnostic.json

Status:
Fail

Failure details:
The reviewed revision does not compile.

Recommended next action:
Return the revision to the Developer Agent to align the application contract
and its implementation. Obtain independent review of the correction and
validate the new revision.
```

Codex may identify the likely fix, but it must not implement it while assigned to the Validator Agent role.

### Diagnosing Test Failures

Codex can correlate test output with application logs and database state.

For the duplicate-idempotency scenario, it may establish that:

- two concurrent requests used the same idempotency key;
- both requests passed the initial existence check;
- both attempted insertion;
- the expected unique constraint was absent;
- two booking rows were committed.

This diagnostic evidence improves the failure report. It does not authorize Codex to modify the repository.

A precise recommendation is:

```text
Investigate the atomicity of idempotency enforcement and confirm that the
database schema contains the approved unique constraint.
```

A recommendation that exceeds the Validator Agent’s responsibility is:

```text
I changed the repository and handler so the test now passes.
```

That implementation belongs to the Developer Agent.

### Build Success Is Not Validation Success

Codex may be tempted to stop after:

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

The profile must prevent this.

For the corporate fleet booking change, a passing build does not prove:

- the domain rejects duplicate vehicles;
- idempotency works under concurrency;
- the outbox write is atomic;
- authorization isolates corporate accounts;
- the API remains compatible;
- the event schema remains compatible;
- the migration is safe for existing data;
- deployment configuration satisfies policy.

Codex must execute all mandatory gates before producing an overall Pass.

### Handling Flaky Tests

Codex can rerun a failed test for diagnosis only when the policy permits it. Every attempt must remain visible.

Suppose the same test produces:

```text
Attempt 1: Fail
Attempt 2: Pass
Attempt 3: Fail
```

Codex should report:

```markdown
Status:
Inconclusive

Actual result:
The integration test produced inconsistent outcomes for the same source
revision under materially identical conditions.

Evidence:
- attempt-1.trx
- attempt-2.trx
- attempt-3.trx
- retry-history.json

Failure details:
The available evidence does not establish repeatable success.

Recommended next action:
Investigate product timing, test isolation, shared dependencies, and runner
stability. Do not treat the successful second attempt as an authoritative
pass.
```

Codex must not overwrite earlier result files or summarize only the final attempt.

### Contract Validation

Codex can coordinate OpenAPI and event-schema validation and explain generated differences.

Suppose the API compatibility tool reports:

```text
Breaking change:
Response property bookingId changed from required to optional.
```

Codex must report the tool result as a failure. It may inspect the generated OpenAPI document and connect the change to a nullable response property, but it cannot waive the compatibility policy.

Similarly, for the event contract:

```text
Schema: corporate-fleet-booking-created
Baseline: 1.0
Proposed: 1.0
Change: stationId is no longer required
Compatibility result: incompatible
```

The correct outcome is **Fail**. An intentional change requires contract versioning and an authorized decision outside validation.

### Migration Validation

Codex can use disposable databases to validate EF Core migrations. The workflow should test both:

- clean creation;
- upgrade from the previous supported schema with representative data.

Codex should preserve:

- the generated SQL;
- migration identifiers;
- database-engine version;
- representative data version;
- application logs;
- schema comparison;
- pre- and post-migration verification results.

If the migration succeeds against an empty database but fails during upgrade, the migration gate fails. Codex must not average the two results or report a partial pass.

### Configuration and Infrastructure Validation

Codex can inspect and validate:

- strongly typed .NET options;
- startup validation;
- environment-specific configuration overlays;
- Kubernetes manifests;
- Helm output;
- Terraform plans;
- policy-as-code reports;
- secret-store references;
- workload identity configuration.

A successful syntax check is not enough when the profile also requires policy validation.

For example:

```markdown
Check performed:
Validate Booking API Kubernetes manifests.

Expected condition:
Rendered manifests must pass syntax validation and all mandatory enterprise
policies.

Actual result:
Manifest syntax passed. The security policy failed because the container
does not set runAsNonRoot to true.

Status:
Fail

Evidence:
- rendered-manifest.yaml
- manifest-validation.log
- policy-report.json

Recommended next action:
Return the deployment change to the Developer Agent or Platform Engineer.
After correction and review, rerun the complete deployment validation gate.
```

### Validation in Local and Remote Environments

Codex may run locally or within a managed remote environment.

| Consideration | Local Codex workflow | Managed validation environment |
| --- | --- | --- |
| Feedback speed | Usually fast | Depends on provisioning and queue time |
| Access to developer services | Often convenient | Must be explicitly configured |
| Environment consistency | May vary | More controllable |
| Workspace isolation | Must be verified | Can be created per task |
| Evidence authority | Usually advisory unless governed | Better suited to authoritative evidence |
| Secret exposure risk | Depends on local configuration | Can use restricted validation identities |
| Reproducibility | Requires careful environment capture | Easier with immutable images and definitions |

A mature harness may use Codex locally for pre-validation and diagnosis, then require a controlled remote workflow for the authoritative result.

### Codex Strengths for Validation

Codex is particularly useful for:

- navigating large repositories;
- locating scoped `AGENTS.md` Instructions;
- executing multi-step terminal workflows;
- coordinating tests and analysis tools;
- working in isolated repository environments;
- preserving generated artifacts;
- diagnosing compiler, test, and configuration failures;
- creating structured reports from raw evidence;
- identifying incomplete validation coverage;
- connecting a failure to relevant source and configuration.

These strengths make Codex an effective validation orchestrator. They do not make the model’s own judgment deterministic.

### Codex Limitations and Risks

Teams should control the following risks:

- Codex may attempt to fix a failure because it has repository write capability.
- It may run a convenient command instead of the authoritative repository command.
- It may omit a slow gate unless the validation profile is explicit.
- It may interpret missing output as success.
- It may retry a flaky test without preserving the initial failure.
- It may validate uncommitted workspace changes.
- It may generate evidence that is not bound to an immutable revision.
- It may expose protected values during configuration diagnostics.
- It may treat its explanation of a contract as equivalent to a compatibility-tool result.
- It may make local changes to formatting or generated files while validating.

Controls should include:

- clean or read-only validation workspaces;
- protected test and policy files;
- least-privilege execution identities;
- immutable revision identifiers;
- approved validation scripts;
- mandatory artifact manifests;
- structured outcome schemas;
- independent workflow enforcement;
- review requirements for gate changes.

### Claude, GitHub Copilot, and Codex Comparison

The three platforms can support the same vendor-neutral Validator Agent contract.

| Capability | Claude Code | GitHub Copilot | OpenAI Codex |
| --- | --- | --- | --- |
| Repository-wide reasoning | Strong | Strong within supported repository context | Strong |
| Terminal orchestration | Strong | Depends on operating surface | Strong |
| IDE integration | Depends on client and workflow | Strong | Depends on operating environment |
| GitHub pull-request integration | Requires surrounding integration | Strong | Requires surrounding integration |
| Controlled workspace execution | Depends on harness | Available through configured workflows | Strong in isolated task environments |
| Instruction mechanism | Claude and repository Instructions | Copilot and repository Instructions | `AGENTS.md` and repository Instructions |
| Validation artifact generation | Strong | Strong through workflows | Strong |
| Failure diagnosis | Strong | Strong, especially near developer workflow | Strong |
| Deterministic authority | External tools and governed gates | External tools and governed gates | External tools and governed gates |
| Can approve its own work | No | No | No |
| Can replace evaluation | No | No | No |
| Can replace human approval | No | No | No |

The differences are operational rather than conceptual. An enterprise harness should define the role once and provide platform adapters for Claude Code, GitHub Copilot, and Codex.

### Portable Validator Agent Contract

A portable contract can be represented as:

```yaml
role: validator
version: 1.0

inputs:
  - taskId
  - reviewedRevision
  - validationProfile
  - implementationEvidence
  - reviewResult

requiredBehavior:
  - verifyRevisionIntegrity
  - executeMandatoryGates
  - preserveRawEvidence
  - recordRetries
  - redactSecrets
  - classifyOutcomes
  - recommendNextAction

allowedOutcomes:
  - Pass
  - Fail
  - Blocked
  - Inconclusive

prohibitedBehavior:
  - modifyImplementation
  - weakenValidationControls
  - skipFailedGates
  - hideEvidence
  - convertInconclusiveToPass
  - approveRelease
  - replaceEvaluator
  - replaceHumanApproval

handoffs:
  Pass: evaluator
  Fail: developer
  Blocked: escalation
  Inconclusive: escalation
```

Each AI coding platform can consume this contract through its own instruction and automation mechanisms. The enterprise principles remain stable even when tooling changes.

### Appropriate Codex Handoff

After successful validation, Codex should produce a constrained handoff:

```text
Validation completed for task ACD-CFB-142.

Validated revision: c719b42
Reviewed revision: c719b42
Validation profile: booking-service-v3
Overall result: Pass

Mandatory checks required: 13
Mandatory checks executed: 13
Mandatory checks passed: 13
Failed checks: 0
Blocked checks: 0
Inconclusive checks: 0
Skipped checks: 0

Evidence package:
artifacts/validation/ACD-CFB-142

Next role:
Evaluator Agent

This result confirms that the reviewed revision satisfied the configured
deterministic engineering conditions. It does not constitute merge,
business, release, deployment, or production approval.
```

When a gate fails, Codex returns the evidence to the Developer Agent through the governed workflow. When validation is blocked or inconclusive, it escalates rather than inventing certainty.

Claude Code, GitHub Copilot, and Codex can all make validation more accessible and efficient. The trustworthy design is the same across platforms: AI coordinates the work, deterministic tools produce the evidence, the harness enforces the outcome, and accountable people retain approval authority.

Chapter 17 status: In progress — next section: Best Practices

## Best Practices

The Validator Agent becomes trustworthy when its results are repeatable, complete, traceable, and protected from convenient reinterpretation. The following practices establish those qualities in an enterprise harness.

### Validate an Immutable Revision

Every validation run must identify the exact source revision being tested. A branch name is insufficient because its contents can change during execution.

Record:

- full commit identifier;
- reviewed commit identifier;
- repository location;
- working-tree status;
- submodule revisions where applicable;
- generated inputs not stored in the repository;
- artifact digests.

If the validated revision differs from the reviewed revision, stop the workflow. Evidence from one revision must not be presented as proof for another.

### Use a Clean, Isolated Environment

Authoritative validation should run in a clean and reproducible environment. This prevents local files, cached artifacts, installed tools, environment variables, and running services from influencing the result unexpectedly.

Prefer:

- disposable workspaces;
- pinned SDK and tool versions;
- locked dependencies;
- approved container images;
- isolated test databases;
- controlled test data;
- least-privilege validation identities;
- explicit environment definitions.

A test that passes only on one developer’s machine is not dependable enterprise evidence.

### Define Validation Profiles as Versioned Assets

Do not allow each agent to decide independently what “complete validation” means. Create versioned validation profiles for common change categories.

A profile should define:

- applicable change types;
- mandatory and optional gates;
- execution order;
- approved commands and tools;
- prerequisites;
- expected conditions;
- severity thresholds;
- retry rules;
- evidence requirements;
- outcome aggregation;
- escalation conditions.

Example:

```yaml
name: booking-service-v3
version: 3.0

appliesTo:
  - booking-domain
  - booking-api
  - booking-persistence
  - booking-contracts
  - booking-deployment

gates:
  - id: release-build
    required: true
    expected: zero-errors-and-prohibited-warnings

  - id: integration-tests
    required: true
    expected: all-tests-pass

  - id: event-contract
    required: true
    expected: backward-compatible

  - id: migration-upgrade
    required: true
    expected: safe-upgrade-with-representative-data

outcomePolicy:
  failedRequiredGate: Fail
  blockedRequiredGate: Blocked
  inconclusiveRequiredGate: Inconclusive
```

Changes to the profile should be reviewed like changes to production controls.

### Separate Validation from Implementation

The Validator Agent should not modify the implementation it validates. If a gate fails, return the change to the Developer Agent.

This separation reduces:

- confirmation bias;
- unauthorized scope expansion;
- test manipulation;
- hidden implementation changes;
- ambiguous ownership;
- invalid evidence.

The same AI platform may perform both roles in separate sessions, but each session must have a distinct assignment, input revision, authority boundary, and output.

### Prefer Repository-Controlled Commands

Use checked-in, reviewed validation scripts and workflows where available. Repository-controlled commands make local, CI, and agent validation more consistent.

A standard entry point might be:

```powershell
.\harness\scripts\validate.ps1 `
    -Profile "booking-service-v3" `
    -Revision "c719b42" `
    -EvidenceDirectory ".\artifacts\validation\ACD-CFB-142"
```

The script should:

- fail on unexpected command errors;
- propagate exit codes;
- produce machine-readable reports;
- preserve partial evidence;
- identify skipped checks;
- avoid logging secrets;
- generate an evidence manifest.

A convenient script name does not guarantee completeness. Compare the script’s actual behavior with the selected validation profile.

### Make Expected Conditions Explicit

Each gate must define what constitutes success before execution.

Weak condition:

> Run security checks and review the output.

Strong condition:

> Run the approved dependency scanner. Fail if the revision introduces any critical or high-severity vulnerability without an authorized exception.

Explicit conditions prevent the Validator Agent from adjusting interpretations after seeing the result.

### Preserve Raw and Structured Evidence

A validation summary is not enough. Preserve the evidence from which the summary was produced.

Useful evidence includes:

- command lines;
- exit codes;
- build logs;
- binary logs;
- test reports;
- code-coverage reports;
- SARIF files;
- contract diffs;
- schema validation results;
- migration scripts;
- migration execution logs;
- database verification reports;
- rendered infrastructure plans;
- policy reports;
- retry histories;
- environment metadata.

Store evidence using stable names and associate it with the task and source revision.

### Require a Complete Evidence Manifest

The Validator Agent should generate a manifest listing every mandatory check and its evidence.

```json
{
  "taskId": "ACD-CFB-142",
  "sourceRevision": "c719b42",
  "profile": "booking-service-v3",
  "requiredChecks": 13,
  "executedChecks": 13,
  "checks": [
    {
      "id": "release-build",
      "status": "Pass",
      "evidence": [
        "build/build.log",
        "build/build.binlog"
      ]
    }
  ]
}
```

The harness should compare expected artifacts with actual artifacts. Missing evidence must not be treated as an implied pass.

### Use Machine-Readable Outputs

Prefer validation tools that produce structured output such as:

- TRX or JUnit test reports;
- SARIF analysis reports;
- JSON contract differences;
- XML coverage reports;
- JSON policy results;
- machine-readable migration summaries.

Structured outputs reduce dependence on natural-language interpretation and allow the harness to enforce outcome rules automatically.

Human-readable summaries remain valuable, but they should be derived from—not substituted for—the machine-readable evidence.

### Treat Skipped Tests Explicitly

A test runner may exit successfully even when tests are skipped. The validation profile must define how skipped tests affect the result.

A practical policy may state:

- skipped critical tests cause failure;
- unauthorized skips cause failure;
- approved environment-specific skips are reported;
- newly skipped tests require investigation;
- the total discovered, executed, passed, failed, and skipped counts must be recorded.

The Validator Agent should never hide skipped tests inside a general statement that the test suite passed.

### Design Tests for Determinism

Reliable validation requires deterministic tests. Engineering teams should reduce dependence on:

- uncontrolled clocks;
- random data without fixed seeds;
- shared mutable environments;
- execution order;
- external networks;
- long timing assumptions;
- eventually consistent services without bounded polling;
- stale cached state.

For Alpha Car Detailing, a booking-window test should use an injected clock rather than the current system time:

```csharp
var clock = new FakeClock(
    new DateTimeOffset(2026, 9, 15, 10, 0, 0, TimeSpan.Zero));

var service = new FleetBookingService(clock);
```

This makes the expected condition repeatable.

### Test Concurrency and Idempotency Directly

Idempotency claims should be validated under realistic concurrency, not only through sequential duplicate requests.

For corporate fleet booking, validation should confirm that concurrent requests using the same idempotency key:

- create one logical booking;
- return a consistent result;
- create one applicable outbox message;
- do not leave partial records;
- satisfy the approved database uniqueness strategy.

This is particularly important in event-driven and horizontally scaled services.

### Validate Transactional Boundaries

Where the system uses a transactional outbox, validation should verify that the business record and outbox message are committed atomically.

Test both directions:

- successful booking creates both records;
- failed booking creates neither record.

Do not assume that the presence of an outbox table proves correct transactional behavior.

### Validate API and Event Contracts Independently

Source-level tests and serialization tests do not replace compatibility checks.

For APIs:

- validate the OpenAPI document;
- compare it with the approved baseline;
- execute provider or consumer contract tests;
- verify status codes and error formats.

For events:

- validate the schema;
- test representative serialized payloads;
- compare with the registered version;
- apply the approved compatibility policy;
- verify consumer deserialization where required.

A passing API test does not automatically prove that an event contract remains compatible, and vice versa.

### Validate Migrations Against Representative Data

Every meaningful migration profile should include:

- empty-database creation;
- upgrade from the previous supported schema;
- representative data;
- constraint and index verification;
- application startup after migration;
- destructive-operation detection;
- rollout compatibility assessment.

Large or high-risk migrations may also require:

- estimated execution time;
- lock analysis;
- online migration strategy;
- backup and recovery validation;
- roll-forward testing;
- dual-version application compatibility.

The Validator Agent should never use an empty-database result as complete proof of upgrade safety.

### Validate Configuration at Startup

Use strongly typed options and fail-fast validation so invalid configuration produces deterministic results.

Validate:

- required values;
- formats and ranges;
- environment overlays;
- secret references;
- feature-flag dependencies;
- service endpoints;
- retry and timeout values;
- telemetry settings.

Configuration checks must prove correctness without exposing protected values.

### Validate Infrastructure Plans, Not Only Syntax

Infrastructure syntax can be valid while the proposed change remains unsafe.

Validation should inspect:

- planned resource creation, update, replacement, and deletion;
- identity permissions;
- public network exposure;
- encryption;
- secret handling;
- container privileges;
- resource limits;
- health probes;
- policy compliance;
- deprecated APIs;
- naming and tagging rules.

Destructive or high-risk plan changes should trigger escalation or human approval.

### Apply Least Privilege

The Validator Agent needs enough authority to execute checks, but it should not receive unnecessary permissions.

Prefer identities that can:

- read the relevant repository revision;
- restore from approved dependency sources;
- read contract baselines;
- create disposable test resources;
- write validation artifacts.

Avoid granting authority to:

- deploy to production;
- modify protected branches;
- change validation policies;
- approve security exceptions;
- alter shared data;
- read unrelated secrets.

Least privilege limits damage from errors, prompt injection, compromised tools, or incorrect agent reasoning.

### Protect Validation Controls

Tests, validation profiles, baselines, policies, workflows, and suppression files are part of the control system.

Protect them using:

- branch rules;
- required reviewers;
- code ownership;
- signed commits or artifacts where appropriate;
- audit logs;
- restricted workflow permissions;
- independent approval for weakened controls.

A change that modifies both product behavior and its validating gate deserves elevated scrutiny.

### Preserve Every Retry

Record:

- why a retry occurred;
- who or what authorized it;
- source revision;
- environment;
- original result;
- subsequent results;
- configuration differences.

Do not overwrite previous evidence.

Retries should be used for diagnosis or approved transient recovery—not as a search for a passing outcome.

### Treat Flakiness as a Defect

A flaky validation check weakens trust in the entire harness.

When the same check passes and fails under materially identical conditions:

- preserve all attempts;
- classify the outcome according to policy;
- investigate the source;
- assign ownership;
- track recurrence;
- prevent indefinite reruns.

Quarantine may be necessary in some organizations, but it must be visible, time-bound, owned, and approved. Silent quarantine converts missing confidence into false confidence.

### Stop on Failed Mandatory Gates

The Validator Agent may continue selected independent checks to gather a complete diagnostic picture, but it must not advance the implementation after a mandatory failure.

Whether execution should stop immediately or continue collecting independent evidence is a profile decision.

For example:

- a failed build normally prevents tests that require compiled output;
- a failed API contract check need not prevent an independent manifest scan;
- a failed security gate must prevent advancement even when all other checks pass.

The overall status must remain Fail.

### Distinguish Blocked from Failed

A failed condition and an unavailable prerequisite communicate different engineering facts.

Use **Fail** when the implementation violates an executed condition.

Use **Blocked** when the condition cannot be tested because a prerequisite is unavailable.

This distinction improves routing:

- Fail usually returns to the Developer Agent.
- Blocked usually goes to a platform owner, environment owner, permission owner, or Lead Agent.

Neither result permits advancement.

### Use Inconclusive Honestly

Use **Inconclusive** when evidence cannot establish Pass or Fail reliably.

Examples include:

- truncated test output;
- inconsistent repeated results;
- unreadable scanner reports;
- incomplete environment data;
- a partially completed migration check;
- unsupported contract comparison behavior.

Inconclusive is a protective result. It acknowledges uncertainty instead of hiding it.

### Separate Gate Status from Overall Status

Record the outcome of each gate before calculating the overall result.

For example:

| Gate | Status |
| --- | --- |
| Build | Pass |
| Unit tests | Pass |
| Integration tests | Fail |
| Architecture tests | Pass |
| Security scan | Blocked |
| API contract | Pass |

The overall result cannot be Pass. Depending on aggregation policy, Fail may take precedence, while the blocked security check must still remain visible.

Never collapse mixed results into “mostly passed.”

### Make Failure Reports Actionable

A failure report should identify:

- the exact violated condition;
- affected component;
- relevant evidence;
- reproducible command;
- source revision;
- whether the failure is new;
- recommended responsible role;
- required revalidation scope.

Avoid vague statements such as:

> Some tests failed. Please investigate.

Prefer:

> Revision `c719b42` failed the duplicate-idempotency integration test. Two booking rows were created for concurrent requests using the same idempotency key. Return the change to the Developer Agent, review the correction, and rerun the complete integration and persistence validation profile.

### Keep Diagnostic Recommendations Separate from Decisions

The Validator Agent may recommend likely investigation areas. It should not make architecture, security-risk, or business decisions outside its authority.

Appropriate:

> The failure correlates with the absence of the approved unique constraint. The Developer Agent should investigate idempotency enforcement.

Inappropriate:

> The unique constraint is unnecessary, so the gate should be removed.

The latter changes the validation policy and requires authorized review.

### Revalidate Corrected Revisions

After a correction:

1. bind validation to the new revision;
2. confirm independent review of the correction;
3. rerun the failed check for rapid feedback;
4. run the complete required regression profile;
5. create a new evidence package;
6. preserve the previous failure history.

Do not copy a passing status from the earlier revision without an explicit and auditable evidence-reuse policy.

### Measure Validation Quality

Enterprises should monitor the health of the validation system itself.

Useful measures include:

- gate execution time;
- failure rate by gate;
- blocked-run frequency;
- inconclusive-run frequency;
- flaky-test rate;
- retry count;
- missing-evidence rate;
- false-positive rate;
- escaped-defect correlation;
- time from failure to correction;
- validation-control change frequency;
- policy-exception volume.

Metrics should improve the system, not encourage agents or teams to weaken gates for better numbers.

### Keep Validation Results Auditable

An auditor or engineering leader should be able to answer:

- What revision was validated?
- Which profile applied?
- Which checks were mandatory?
- Which checks executed?
- What were their results?
- What evidence supports each result?
- Were any checks skipped?
- Were any retries performed?
- Did the implementation modify its own gates?
- Who approved any exception?
- What revision advanced to evaluation or release?

If these questions cannot be answered, the validation process is incomplete.

### Preserve the Evaluator and Human Boundaries

A passing validation report should always state what it does—and does not—mean.

It means:

- the identified revision satisfied the configured deterministic conditions;
- required evidence was collected;
- no mandatory gate remained failed, blocked, or inconclusive.

It does not mean:

- the feature fully satisfies user needs;
- the implementation is risk-free;
- a contract exception is acceptable;
- a destructive migration is approved;
- the change is authorized for production.

The next governed step is evaluation, followed by any required human approval.

### Recommended Validation Checklist

Before reporting Pass, the Validator Agent should confirm:

- [ ] The validated revision matches the reviewed revision.
- [ ] The workspace was clean and controlled.
- [ ] The correct validation profile was applied.
- [ ] Every mandatory gate executed.
- [ ] Expected conditions were defined before result classification.
- [ ] No failed gate was ignored.
- [ ] No blocked or inconclusive gate was converted to Pass.
- [ ] Skipped tests were identified and handled according to policy.
- [ ] Every retry was recorded.
- [ ] Raw and structured evidence was preserved.
- [ ] Evidence contains no exposed secrets.
- [ ] API and event compatibility were validated where applicable.
- [ ] Migration safety was tested against representative upgrade data.
- [ ] Configuration and infrastructure checks completed where applicable.
- [ ] Validation-control changes received required scrutiny.
- [ ] The overall outcome was calculated according to policy.
- [ ] The report identifies the recommended next role.
- [ ] The report does not claim evaluation or production approval.

The checklist is a final completeness control. It does not replace any underlying deterministic gate.

Chapter 17 status: In progress — next section: Anti-patterns

## Anti-patterns

Validation anti-patterns create the appearance of engineering confidence without the evidence required to justify it. In an AI-assisted workflow, these failures are especially dangerous because an agent can produce a persuasive explanation even when validation is incomplete, inconsistent, or compromised.

### Agent Self-Validation

**Anti-pattern:** The Developer Agent implements the change, runs selected checks, interprets the results, and approves its own work.

The Developer Agent should run tests during implementation, but those checks are development feedback rather than independent validation. The agent may unconsciously select favorable tests, explain away failures, or overlook assumptions embedded in its implementation.

For Alpha Car Detailing, the Developer Agent might implement corporate fleet booking and report:

> The solution builds, and the tests I added pass. The feature is validated.

This statement does not establish that:

- the correct revision was tested;
- existing tests passed;
- architecture rules were checked;
- contract compatibility was verified;
- migration safety was tested;
- security controls ran;
- independent evidence was preserved.

**Correction:** Assign authoritative validation to a separate Validator Agent context. Bind it to the reviewed revision, apply a governed profile, and preserve the results independently.

### Treating Compilation as Complete Validation

**Anti-pattern:** The solution compiles, so the implementation is declared valid.

Compilation verifies syntax, type relationships, and selected compiler rules. It does not prove correct business behavior or integration safety.

A corporate fleet booking implementation may compile while:

- creating duplicate bookings;
- omitting the outbox message;
- allowing cross-account access;
- returning incorrect HTTP status codes;
- publishing an incompatible event;
- applying an unsafe migration;
- missing required production configuration.

**Correction:** Treat the build as one gate within a broader validation profile.

### Treating Test Presence as Test Execution

**Anti-pattern:** The Validator Agent finds relevant test files and assumes the required behavior is covered and passing.

The existence of tests does not prove that they:

- compiled;
- were discovered;
- executed;
- passed;
- ran against the reviewed revision;
- exercised the intended environment.

**Correction:** Record discovered, executed, passed, failed, skipped, and filtered tests. Preserve the test runner’s machine-readable output.

### Skipping Failed Tests

**Anti-pattern:** A failed test is excluded because it appears unrelated, old, slow, or inconvenient.

For example:

```powershell
dotnet test --filter "FullyQualifiedName!~DuplicateIdempotency"
```

The agent may then report that all selected tests passed while hiding the excluded mandatory test.

**Correction:** A required test may be excluded only through an authorized, visible policy decision. Record the exclusion, owner, reason, expiration, and associated risk. Until then, the gate remains failed.

### Re-running Until a Flaky Test Passes

**Anti-pattern:** The Validator Agent repeatedly executes a failing test and retains only the successful attempt.

This converts nondeterminism into false confidence.

For example:

```text
Attempt 1: Fail
Attempt 2: Fail
Attempt 3: Pass
Reported result: Pass
```

The actual evidence shows instability, not repeatable success.

**Correction:** Preserve every attempt and classify the result according to the flaky-test policy. In the absence of a reliable policy-based conclusion, report **Inconclusive** or **Fail**.

### Editing Tests to Match Broken Behavior

**Anti-pattern:** When implementation behavior conflicts with a test, the Validator Agent changes the expected result.

Suppose the idempotency test expects one booking, but two are created. Changing the expected count to two makes the test pass while destroying the control that detects the defect.

**Correction:** The Validator Agent must not edit tests during authoritative validation. It reports the failed condition and returns the implementation to the Developer Agent.

If the test itself is incorrect, changing it requires a separate governed task and independent review.

### Weakening Assertions

**Anti-pattern:** A precise assertion is replaced by a less meaningful condition.

Original:

```csharp
bookingCount.ShouldBe(1);
```

Weakened:

```csharp
bookingCount.ShouldBeGreaterThan(0);
```

The test now allows duplicate bookings while continuing to appear successful.

**Correction:** Treat weakened assertions as validation-control changes. Require explicit justification and independent review.

### Disabling Tests Silently

**Anti-pattern:** A test is marked skipped, ignored, or quarantined without visibility.

Examples include:

```csharp
[Fact(Skip = "Temporarily unstable")]
```

or:

```csharp
[Ignore]
```

“Temporary” exclusions often become permanent gaps.

**Correction:** Quarantine must be visible, approved, owned, time-bound, and tracked. Critical behavior may require the overall validation run to remain blocked or failed until reliable coverage is restored.

### Missing Evidence

**Anti-pattern:** The report claims that checks passed but contains no logs, reports, exit codes, or artifact references.

Example:

```text
Build passed.
Tests passed.
Security looks good.
```

This cannot establish:

- which commands ran;
- which tests executed;
- what severity policy applied;
- whether reports were complete;
- which revision was tested.

**Correction:** Require structured fields and raw evidence for every result. Missing evidence must produce **Inconclusive** or **Fail**, according to policy.

### Hiding Partial Execution

**Anti-pattern:** The Validator Agent reports “all tests passed” after running only one test project or an affected-code subset that was not authorized by the validation profile.

For example, domain tests pass, but integration and contract tests never run.

**Correction:** Compare the required gate inventory with the actual execution inventory. Report missing required checks explicitly.

### Treating a Zero Exit Code as Universal Success

**Anti-pattern:** The Validator Agent assumes that exit code zero always means the gate passed.

Some tools return success while reporting:

- skipped tests;
- policy warnings;
- vulnerabilities below a tool-default threshold but above the enterprise threshold;
- partial scans;
- excluded files;
- incomplete analysis.

**Correction:** Parse the tool’s structured result and apply the repository’s explicit outcome policy.

### Ignoring Failed Gates Because Most Checks Passed

**Anti-pattern:** The Validator Agent uses majority voting across gates.

Example:

```text
Twelve of thirteen checks passed, so overall confidence is high.
```

A single failed security, contract, or migration gate may be sufficient to prevent advancement.

**Correction:** Aggregate outcomes using policy. Mandatory gates are conjunctive: all must pass unless an authorized exception process says otherwise.

### Converting Inconclusive Results into Passes

**Anti-pattern:** The agent reports Pass because it did not prove a failure.

Absence of confirmed failure is not proof of success.

An interrupted scanner, incomplete test report, or inconsistent migration result does not satisfy the expected condition.

**Correction:** Use **Inconclusive** when the available evidence cannot reliably establish Pass or Fail.

### Converting Blocked Results into Passes

**Anti-pattern:** A required check cannot run, so the Validator Agent assumes the implementation would have passed.

For example, the event schema registry is unavailable, and the agent reports compatibility based on visual inspection.

**Correction:** Report **Blocked**, preserve the access or availability error, and prevent advancement until the prerequisite is restored or an authorized alternative is provided.

### Mixing Validation with Review

**Anti-pattern:** The Validator Agent substitutes subjective opinion for an executable condition.

Examples include:

- “The architecture looks clean.”
- “The tests seem comprehensive.”
- “The migration appears safe.”
- “The endpoint design feels consistent.”

These may be useful review findings, but they are not deterministic validation evidence.

**Correction:** Route subjective assessment to the Reviewer Agent. Where valuable, convert stable architecture or policy rules into approved executable gates.

### Mixing Validation with Evaluation

**Anti-pattern:** The Validator Agent declares the feature successful because the configured checks passed.

Passing tests do not prove that the feature delivers the intended business outcome.

For example, corporate fleet booking may pass all technical gates but still fail to support an essential fleet coordinator scenario.

**Correction:** Forward passing validation evidence to the Evaluator Agent. Preserve the distinction between gate compliance and outcome fitness.

### Approving Its Own Implementation

**Anti-pattern:** The same agent changes application code during validation and immediately reports the revised code as passed.

The original reviewed revision is no longer the validated input, and the new changes have not received independent review.

**Correction:** Stop validation after identifying the failure. Create a Developer Agent task, review the new revision, and then start a fresh validation run.

### Validating the Wrong Revision

**Anti-pattern:** The Validator Agent runs checks against the current branch without confirming the commit.

The branch may have advanced after review, or the workspace may contain uncommitted changes.

**Correction:** Bind every check and artifact to an immutable source revision. Verify the working tree before execution.

### Reusing Evidence Across Revisions Without Control

**Anti-pattern:** Results from one revision are copied to another because the agent assumes the correction could not affect them.

Even a small code change can alter compilation, dependency resolution, generated contracts, or configuration.

**Correction:** Rerun the validation profile for the new revision. If evidence reuse is allowed, encode explicit impact-analysis rules and record the decision.

### Ignoring Contract Compatibility

**Anti-pattern:** API or event changes are accepted because provider-side tests pass.

A producer can serialize its own event successfully while breaking existing consumers.

For Alpha Car Detailing, making `stationId` optional may pass producer tests but violate the version 1 event contract.

**Correction:** Validate API and event contracts against approved baselines and compatibility policies. Treat unapproved breaking changes as failures.

### Updating the Baseline to Eliminate a Contract Failure

**Anti-pattern:** The Validator Agent replaces the approved API or event baseline with the generated contract and reruns the comparison.

The difference disappears because the reference point was overwritten.

**Correction:** Baseline updates require a separate contract-governance decision. Preserve the original diff and failed result.

### Ignoring Migration Safety

**Anti-pattern:** The Validator Agent applies the migration only to an empty database.

This misses problems involving:

- existing null values;
- duplicate data;
- constraint violations;
- destructive changes;
- table rewrites;
- application-version compatibility.

**Correction:** Test both clean creation and representative upgrades. Escalate destructive operations and rollout risks.

### Modifying Representative Data to Make a Migration Pass

**Anti-pattern:** The Validator Agent deletes or edits conflicting records from the upgrade fixture without an approved migration strategy.

This hides how the migration behaves against realistic data.

**Correction:** Preserve the failure. Any cleanup, backfill, defaulting, or staged migration must be intentionally designed, reviewed, and validated.

### Treating Migration Generation as Migration Validation

**Anti-pattern:** The migration file was generated successfully, so the database change is considered safe.

Generation does not prove that the migration executes correctly or preserves data.

**Correction:** Apply the migration, verify post-conditions, and test the supported upgrade path.

### Ignoring Configuration Differences

**Anti-pattern:** Validation uses local settings that do not represent deployment environments.

The service may pass locally while production manifests omit required event, identity, or secret-store configuration.

**Correction:** Validate strongly typed configuration and every governed environment overlay. Record configuration sources without exposing values.

### Exposing Secrets as Evidence

**Anti-pattern:** The Validator Agent captures full environment variables, connection strings, tokens, or secret files in logs.

This turns a quality-control activity into a security incident.

**Correction:** Record key presence, reference source, binding result, and redacted diagnostics. Never store protected values in validation artifacts.

### Ignoring Infrastructure Plan Impact

**Anti-pattern:** Infrastructure validation stops after syntax succeeds.

A syntactically valid plan may remove a database, expose a service publicly, or grant excessive permissions.

**Correction:** Validate planned effects and policy compliance. Escalate destructive or privileged changes.

### Bypassing Security or Architecture Checks

**Anti-pattern:** Security or architecture gates are marked advisory because they delay delivery.

This allows schedule pressure to redefine engineering policy without accountable approval.

**Correction:** Enforce mandatory checks mechanically. Exceptions must be explicit, authorized, time-bound, and auditable.

### Accepting Existing Vulnerabilities Without Policy

**Anti-pattern:** The Validator Agent ignores a finding because it existed before the current change.

Existing risk may be treated differently from newly introduced risk, but that distinction must come from policy.

**Correction:** Record whether the finding is new or pre-existing and apply the configured rule. Never invent an exception.

### Silently Changing Validation Gates

**Anti-pattern:** An agent modifies tests, thresholds, workflow filters, analyzer rules, or security policies in the same change and then validates against the weakened configuration.

Examples include:

- reducing code-coverage thresholds;
- excluding integration-test directories;
- changing a vulnerability gate from blocking to advisory;
- suppressing architecture rules;
- narrowing path triggers;
- removing required contract checks.

**Correction:** Detect validation-control changes and require additional independent review. When feasible, compare results using both the previous and proposed controls.

### Trusting Agent Summaries Over Raw Evidence

**Anti-pattern:** The team accepts a polished AI-generated report without inspecting whether the referenced artifacts exist.

Language models can omit details, misunderstand output, or overstate certainty.

**Correction:** Make summaries traceable to raw evidence. Allow the harness to verify artifact existence and calculate statuses from machine-readable results.

### Producing Unactionable Failure Reports

**Anti-pattern:** The Validator Agent reports:

> Validation failed. Please fix the tests.

This does not identify the condition, evidence, revision, or responsible next action.

**Correction:** Include every required result field and provide the reproducible command and failure evidence.

### Continuing After an Invalid Validation Run

**Anti-pattern:** The workflow advances even though the validation process itself was compromised.

Examples include:

- the workspace changed during execution;
- the evidence directory is incomplete;
- the profile could not be loaded;
- mandatory tools used unapproved versions;
- gate definitions changed without required review.

**Correction:** Mark the run invalid, blocked, or inconclusive according to policy. Restart in a controlled environment after resolving the issue.

### Declaring Production Readiness

**Anti-pattern:** The Validator Agent states that a technical Pass means the feature is ready for production.

Validation does not decide:

- business acceptance;
- rollout timing;
- operational readiness;
- security-risk acceptance;
- change-window approval;
- migration authorization;
- production deployment.

**Correction:** Send passing evidence to the Evaluator Agent and retain explicit human approval boundaries.

### Anti-pattern Summary

| Anti-pattern | Why it is dangerous | Correct response |
| --- | --- | --- |
| Agent self-validation | Encourages confirmation bias | Use independent validation |
| Compilation-only validation | Misses behavior and integration risks | Apply the complete profile |
| Skipping failed tests | Conceals known failure | Preserve and report the failure |
| Rerunning until pass | Hides flakiness | Record all attempts |
| Editing tests to pass | Weakens the control | Return work to Developer Agent |
| Missing evidence | Makes results unverifiable | Require raw and structured artifacts |
| Validation-review mixing | Converts opinion into false proof | Keep role boundaries |
| Validation-evaluation mixing | Confuses gate compliance with outcome | Hand off to Evaluator Agent |
| Ignoring contract compatibility | Breaks independent consumers | Run compatibility checks |
| Ignoring migration safety | Risks failure or data loss | Test representative upgrades |
| Silent gate changes | Allows controls to be weakened | Protect and independently review gates |
| Inconclusive converted to Pass | Hides uncertainty | Preserve Inconclusive status |
| Blocked converted to Pass | Claims unexecuted proof | Preserve Blocked status |
| Production-readiness claim | Exceeds validation authority | Require evaluation and approval |

The defining characteristic of these anti-patterns is not merely poor automation. It is loss of evidence integrity. Once failed checks, missing artifacts, retries, or gate changes can be hidden, the validation process no longer provides a trustworthy control.

Chapter 17 status: In progress — next section: Architect’s Notes

## Architect’s Notes

### Validation Is an Architectural Capability

Validation should not be treated as a collection of commands added near the end of delivery. It is an architectural capability that connects design rules, implementation constraints, security policy, contract governance, deployment standards, and operational expectations.

A well-designed system makes important properties verifiable.

For Alpha Car Detailing, architectural decisions such as Clean Architecture boundaries, transactional outbox usage, idempotent request handling, versioned event contracts, and workload identity should have corresponding validation controls.

| Architectural decision | Deterministic validation |
| --- | --- |
| Domain remains framework-independent | Architecture dependency tests |
| Application depends on abstractions | Project and type dependency rules |
| Booking and outbox records are atomic | Database integration tests |
| Requests are idempotent | Concurrent duplicate-request tests |
| Events follow governed schemas | Schema and compatibility validation |
| APIs preserve consumer compatibility | OpenAPI breaking-change detection |
| Services use managed identity | Deployment policy checks |
| Secrets remain outside source control | Secret and configuration scanning |
| Migrations support rolling deployment | Upgrade and version-compatibility tests |

An architectural decision that cannot be validated is not necessarily invalid, but its enforcement remains dependent on review, discipline, or human inspection.

### Validate Architectural Invariants, Not Preferences

Executable architecture rules should protect meaningful boundaries and risks. They should not encode every architect’s stylistic preference.

Good architecture gates include:

- Domain cannot reference Infrastructure.
- One microservice cannot reference another service’s internal assembly.
- API controllers cannot access database contexts directly.
- Integration events must use the approved envelope.
- Database updates and outbox writes must share a transaction.
- Production workloads must not use embedded credentials.

Weak architecture gates include:

- every class must have a particular suffix without functional reason;
- every method must remain below an arbitrary size;
- every implementation must use the architect’s preferred pattern;
- new abstractions must be created for all dependencies, regardless of value.

Overly rigid validation encourages teams to work around controls instead of trusting them.

### Design for Independent Verifiability

Architects should make important system behaviors observable and testable without requiring privileged production access.

Useful design choices include:

- dependency injection for clocks and external services;
- explicit domain results;
- stable API and event schemas;
- isolated persistence boundaries;
- transactional integration tests;
- health and readiness endpoints;
- structured logs;
- correlation identifiers;
- versioned configuration schemas;
- reproducible infrastructure plans.

Testability is not only a developer concern. It is a prerequisite for reliable autonomous validation.

### Keep Policy Separate from Execution

The validation profile defines what must be checked. Validation scripts and tools define how the checks execute.

Separating these concerns prevents implementation details from becoming the only source of policy.

```mermaid
flowchart TD
    A["Architecture and enterprise policy"] --> B["Validation profile"]
    B --> C["Scripts and workflows"]
    C --> D["Validation tools"]
    D --> E["Evidence"]
    E --> F["Outcome policy"]
```

A script may change because a tool is replaced, while the required condition remains stable. For example, the organization may change its API compatibility tool without changing the rule that unapproved breaking changes must fail validation.

### Layer Validation Gates

Validation should provide fast feedback without sacrificing completeness.

A practical gate architecture uses layers:

| Layer | Purpose | Typical checks |
| --- | --- | --- |
| Developer feedback | Detect obvious problems quickly | Formatting, build, focused unit tests |
| Pull-request validation | Verify the complete reviewed change | Full tests, architecture, contracts, security |
| Pre-release validation | Confirm release-candidate fitness | Migration rehearsal, deployment policies, package integrity |
| Deployment validation | Protect environment transition | Plan review, configuration, readiness checks |
| Post-deployment verification | Confirm expected system state | Smoke tests, health checks, telemetry checks |

The earlier layers reduce feedback time. Later layers validate conditions that require broader integration or environment context.

Passing an early layer must not be confused with passing the complete validation chain.

### Use Risk-Based Profiles Carefully

Not every change requires every available check. A documentation correction should not need a database migration rehearsal. A database-model change should not bypass migration validation because only a few lines changed.

Risk-based selection should consider:

- affected architectural boundaries;
- contract exposure;
- data impact;
- security impact;
- deployment impact;
- operational criticality;
- reversibility;
- blast radius;
- regulatory obligations;
- history of failures.

Line count is a weak risk measure. A one-line authorization or schema change can carry more risk than a large internal refactoring.

### Make Gate Selection Explainable

Dynamic validation selection must be traceable.

The harness should record:

- which change categories were detected;
- which rules matched;
- which gates were selected;
- which gates were excluded;
- which profile version was used;
- whether a human override occurred.

An AI-selected validation profile without an explanation creates an audit and governance gap.

### Preserve Deterministic Gates Outside Agent Control

AI agents can help propose tests and controls, but authoritative gate enforcement should reside in systems that the active agent cannot silently bypass.

Examples include:

- required CI checks;
- branch protection;
- protected workflow definitions;
- code ownership;
- policy-as-code;
- immutable artifact storage;
- security-scanner thresholds;
- signed attestations;
- protected contract registries.

Instructions shape agent behavior. Platform controls enforce organizational boundaries.

### Treat Validation Changes as High-Impact Changes

A change to a validation control can affect every future implementation.

Architects should require explicit review for changes to:

- validation profiles;
- architecture tests;
- compatibility baselines;
- scanner configuration;
- severity thresholds;
- test filters;
- suppression files;
- evidence-retention rules;
- outcome aggregation;
- workflow triggers.

The review should answer:

- Does the change strengthen or weaken the control?
- Which risks are no longer detected?
- Is the change temporary or permanent?
- Who approved it?
- Can both the old and new control run during transition?
- Is historical comparability affected?

### Prevent Circular Validation

Circular validation occurs when an implementation supplies the rule by which it is judged.

Examples include:

- generating a new API contract and using it as its own baseline;
- modifying a test and using the modified test as proof of correctness;
- lowering a threshold in the same change that violates it;
- replacing a migration fixture with data that avoids the failure.

Architects should maintain independent sources of expected conditions, such as:

- approved contract baselines;
- acceptance criteria;
- architecture decisions;
- consumer contracts;
- policy repositories;
- protected test suites;
- representative data snapshots.

### Design Evidence as a Product

Validation evidence has consumers:

- Developer Agents need reproducible failures.
- Reviewer Agents need confirmation of addressed concerns.
- Evaluator Agents need technical-gate results.
- architects need boundary and compatibility results.
- security teams need scanner findings.
- platform teams need infrastructure and configuration reports.
- auditors need traceability.
- human approvers need concise risk information.

Evidence packages should therefore be stable, searchable, structured, and appropriately retained.

A strong evidence package supports both machine enforcement and human understanding.

### Separate Product Failures from Harness Failures

The validation architecture should distinguish:

- implementation failure;
- test failure;
- validation-tool failure;
- environment failure;
- evidence-persistence failure;
- policy-resolution failure.

These conditions require different responses.

| Failure category | Example | Typical routing |
| --- | --- | --- |
| Product | Duplicate bookings created | Developer Agent |
| Contract | Breaking event change | Developer Agent and contract owner |
| Test control | Approved test asserts obsolete behavior | Test owner and Reviewer |
| Tool | Scanner crashes | Platform engineering |
| Environment | Test database unavailable | Environment owner |
| Evidence | Artifact upload fails | Harness or platform owner |
| Policy | Two profiles specify conflicting thresholds | Architect or governance owner |

Classifying every problem as “tests failed” hides ownership and slows resolution.

### Design for Partial Execution Without Partial Approval

After one gate fails, the harness may continue running independent checks to collect a fuller risk picture. This can reduce correction cycles.

For example, an API contract failure does not necessarily prevent:

- security scanning;
- deployment-policy validation;
- migration analysis.

However, dependent checks should not run when their prerequisite fails. Integration tests cannot reliably execute after a build failure.

The architecture should represent gate dependencies explicitly while keeping the overall result failed.

### Model Gate Dependencies

A flat list of commands is insufficient for complex validation.

```mermaid
flowchart TD
    R["Restore"] --> B["Build"]
    B --> U["Unit tests"]
    B --> A["Architecture tests"]
    B --> I["Integration tests"]
    B --> API["API contract"]
    B --> EVT["Event contract"]
    I --> M["Migration verification"]
    API --> O["Aggregate outcome"]
    EVT --> O
    U --> O
    A --> O
    M --> O
```

This model allows the harness to:

- stop dependent checks;
- continue independent checks;
- distinguish failure from non-execution;
- explain why a gate did not run;
- optimize safe parallel execution.

A gate that does not run because its prerequisite failed should be reported explicitly, not silently omitted.

### Keep Reviewer, Validator, and Evaluator Independent

These roles form complementary controls:

- The Reviewer Agent uses informed judgment to examine implementation quality.
- The Validator Agent executes objective checks and preserves evidence.
- The Evaluator Agent assesses whether the completed work achieves the intended outcome.

Combining the roles may reduce short-term workflow cost, but it also reduces independence and makes results harder to interpret.

In smaller teams, one person or AI platform may perform multiple roles. Independence can still be improved through:

- separate sessions;
- isolated context;
- distinct prompts;
- immutable handoffs;
- different evidence sources;
- human checkpoints;
- explicit role transitions.

### Protect Human Approval Boundaries

Architects must identify decisions that automated validation cannot authorize.

Typical human approval boundaries include:

- accepting security risk;
- deploying destructive migrations;
- introducing breaking contract versions;
- changing regulatory controls;
- altering architecture standards;
- granting privileged access;
- approving production release;
- weakening validation gates.

The harness should route these decisions to named accountable roles rather than to a generic “human” stage.

### Use Exceptions Sparingly

Enterprise delivery sometimes requires exceptions, but exceptions must not be disguised as passing validation.

An exception record should contain:

- failed or bypassed condition;
- business reason;
- technical risk;
- compensating controls;
- accountable approver;
- scope;
- expiration date;
- remediation owner;
- follow-up date.

The validation result remains historically accurate. The approval system records that an authorized exception allowed advancement despite the result.

### Plan for Tool Failure

Validation tools and infrastructure will fail. The architecture should define how the harness responds when:

- a package registry is unavailable;
- a scanner cannot download its database;
- a test container fails to start;
- an artifact store is unavailable;
- a contract registry times out;
- a runner is interrupted.

A tool failure should not become a product Pass. Depending on the condition, it produces **Blocked** or **Inconclusive**.

### Control Nondeterminism

Architects should treat flaky tests and unstable environments as architecture and platform debt.

Useful controls include:

- fixed random seeds;
- injectable clocks;
- isolated databases;
- disposable namespaces;
- bounded eventual-consistency polling;
- deterministic test data;
- controlled external-service substitutes;
- recorded dependency versions;
- stable runner images.

Retry policies should absorb known transient infrastructure behavior without hiding product instability.

### Validate Operational Characteristics Separately

Builds and functional tests do not establish all operational properties. Depending on risk, the architecture may require separate validation for:

- performance;
- load;
- resilience;
- recovery;
- failover;
- observability;
- scaling;
- resource consumption;
- deployment rollback;
- data restoration.

These checks can be deterministic when their environment, workload, thresholds, and outcome rules are defined.

For example:

> Under the approved workload profile, the corporate fleet booking API must sustain 100 requests per second for 15 minutes with a 95th-percentile response time below 500 milliseconds and an error rate below 1 percent.

The Validator Agent can verify this condition. Deciding whether those thresholds satisfy business expectations belongs to architecture, product, and evaluation.

### Prefer Roll-Forward Thinking for Data Changes

Database rollback is often more complex than reversing application code. Architects should validate:

- forward-compatible schema transitions;
- expand-and-contract migrations;
- coexistence of old and new application versions;
- backfill observability;
- retry safety;
- recovery from partial progress.

A migration file that provides a syntactic `Down` operation does not automatically establish safe rollback.

### Connect Validation to Observability

Some conditions are best verified through runtime evidence. The system should expose enough telemetry to validate:

- outbox publication;
- idempotent duplicate handling;
- failed-message behavior;
- dependency health;
- authorization failures;
- database migration version;
- configuration loading;
- correlation propagation.

The Validator Agent should use structured telemetry in controlled environments without treating the absence of logged errors as proof of correctness.

### Build a Feedback Loop Without Weakening Governance

Validation history can improve future engineering.

Repeated failures may suggest:

- a missing development Skill;
- unclear Instructions;
- inadequate repository discovery;
- weak test isolation;
- an architectural hotspot;
- an incomplete validation profile;
- a recurring configuration problem.

The harness can propose improvements, but it should not autonomously weaken gates based on failure frequency. Changes to Instructions, Skills, profiles, and policies require the same governance expected for other high-impact engineering assets.

### Architect’s Review Questions

When designing the Validator Agent capability, ask:

- Are the most important architectural decisions represented by executable checks?
- Can the harness prove which revision was validated?
- Are profile selection rules explicit and explainable?
- Can an implementation modify its own gates without additional review?
- Are API, event, and database compatibility validated independently?
- Are build, test, and policy results stored in machine-readable formats?
- Does the system preserve failed attempts and retries?
- Are blocked and inconclusive outcomes prevented from advancing?
- Are product, harness, tool, and environment failures routed differently?
- Can another authorized participant reproduce the result?
- Are secrets protected during diagnostics and evidence collection?
- Are validation identities limited by least privilege?
- Are human approval boundaries named and enforced?
- Can the organization audit exceptions and expired suppressions?
- Does a passing result clearly hand off to evaluation rather than claiming production readiness?

The goal is not to automate every engineering judgment. The goal is to create a validation architecture in which objective claims are supported by repeatable evidence, subjective decisions remain visible, and no agent can manufacture confidence by weakening the conditions used to measure it.

Chapter 17 status: In progress — next section: Enterprise Tips

## Enterprise Tips

### Start with a Minimum Mandatory Gate Set

Organizations do not need to automate every possible validation check before introducing a Validator Agent. Begin with a small set of high-value gates that applies to every implementation:

- source revision integrity;
- dependency restore;
- production-configuration build;
- unit and integration tests;
- static analysis;
- security scanning;
- evidence collection.

Add specialized gates for APIs, events, migrations, infrastructure, performance, and compliance as the engineering context requires.

A small enforced profile is more trustworthy than a large profile whose checks are routinely skipped.

### Define One Enterprise Outcome Vocabulary

Use the same outcome vocabulary across repositories and platforms:

- **Pass** — the check executed, satisfied the expected condition, and produced sufficient evidence.
- **Fail** — the check executed and violated the expected condition.
- **Blocked** — a prerequisite prevented execution.
- **Inconclusive** — execution occurred, but the evidence cannot support a reliable Pass or Fail.

Avoid repository-specific alternatives such as “mostly successful,” “soft pass,” or “probably safe.” If the organization supports warnings or advisory findings, keep them separate from the four primary outcomes.

### Standardize Validation Result Schemas

Define a common machine-readable result schema for all Validator Agents.

At minimum, require:

- task identifier;
- repository;
- source revision;
- reviewed revision;
- validation profile;
- profile version;
- check identifier;
- expected condition;
- actual result;
- evidence references;
- status;
- failure details;
- recommended next action;
- execution timestamps;
- tool versions;
- retry history.

A standard schema enables centralized reporting, audit, trend analysis, and cross-platform portability.

### Assign Ownership to Every Gate

Every validation gate should have an accountable owner.

| Gate | Typical owner |
| --- | --- |
| Build and dependency restore | Development or platform team |
| Unit and integration tests | Service team |
| Architecture tests | Architecture group and service team |
| Static analysis | Engineering enablement |
| Security scanning | Security and platform teams |
| API contracts | API governance and service team |
| Event contracts | Integration governance and producer team |
| Database migrations | Service team and database owner |
| Configuration | Service and platform teams |
| Infrastructure policies | Cloud platform team |

Without ownership, broken gates become permanent blockers or are eventually bypassed.

### Separate Enterprise Policy from Repository Customization

Enterprise policy should define non-negotiable controls, such as:

- secret scanning;
- prohibited vulnerability thresholds;
- evidence retention;
- protected production identities;
- audit requirements;
- human approval for destructive changes.

Repositories should extend these controls with domain-specific validation, such as Alpha Car Detailing booking rules and event contracts.

A repository may strengthen an enterprise policy but should not silently weaken it.

### Build Reusable Validation Skills

Convert recurring procedures into governed Skills, such as:

- validate a .NET service;
- validate a REST API;
- validate an integration event;
- validate an EF Core migration;
- validate a container image;
- validate Kubernetes manifests;
- collect and redact evidence;
- diagnose a flaky test.

Skills should produce consistent evidence across Claude Code, GitHub Copilot, Codex, and conventional automation.

### Provide a Single Local Entry Point

Developers and agents should be able to reproduce validation using one repository-controlled entry point:

```powershell
.\harness\scripts\validate.ps1 `
    -Profile "booking-service-v3" `
    -Revision "c719b42"
```

The CI system should invoke the same core automation where practical. This reduces differences between local and authoritative validation.

### Make CI the Authoritative Execution Environment

Local validation provides rapid feedback, but controlled CI should normally produce the authoritative result for merge decisions.

CI offers:

- clean workspaces;
- known runner images;
- explicit identities;
- immutable revision binding;
- centralized logs;
- artifact retention;
- required checks;
- auditable retries.

Agent-generated local evidence may supplement CI evidence but should not silently replace it.

### Protect Validation Files with Ownership Rules

Use repository protections for:

- validation profiles;
- test infrastructure;
- architecture tests;
- contract baselines;
- analyzer configurations;
- security thresholds;
- suppression files;
- workflow definitions;
- validation scripts.

Require approval from the relevant engineering, architecture, platform, or security owners when these controls change.

### Detect Control Changes Automatically

The harness should classify a change as high risk when it modifies validation controls.

For example:

```yaml
protectedValidationPaths:
  - ".github/workflows/**"
  - "harness/validation/**"
  - "harness/scripts/**"
  - "contracts/**"
  - "tests/**/ArchitectureTests/**"
  - "security/**"
```

A matching change can trigger:

- additional review;
- execution under the previous profile;
- comparison of old and new results;
- security or architecture approval;
- prevention of automated merge.

### Preserve Evidence on Failure

Configure workflows to publish evidence regardless of success or failure. This includes:

- failed test reports;
- partial scanner output;
- build logs;
- migration logs;
- contract diffs;
- policy findings;
- environment diagnostics.

Do not make evidence upload dependent on earlier gates passing.

### Keep Evidence Useful but Safe

Evidence should be detailed enough for diagnosis and audit without exposing protected data.

Apply:

- secret redaction;
- access controls;
- retention policies;
- data classification;
- encryption;
- artifact integrity checks;
- tenant and customer-data anonymization.

Use sanitized representative data instead of production data whenever possible.

### Use Short-Lived Validation Credentials

Validation identities should be:

- scoped to the required repository and services;
- read-only where possible;
- time-limited;
- issued dynamically;
- auditable;
- separate from deployment identities.

A Validator Agent that can deploy to production or modify security policy has excessive authority.

### Pin Tools and Dependencies

Record and control:

- .NET SDK versions;
- validation tool versions;
- scanner databases;
- container image digests;
- package-lock files;
- GitHub Action versions;
- infrastructure providers;
- contract-tool versions.

Unexpected tool upgrades can change validation outcomes without any source-code change.

### Establish a Flaky-Test Service Level Objective

Track flaky tests as engineering defects. Define expectations such as:

- maximum time to assign an owner;
- maximum quarantine duration;
- required evidence for quarantine;
- escalation for repeated failures;
- expiration of skip approvals.

A growing quarantine list is a signal that the organization is losing confidence in its validation system.

### Distinguish Existing and New Findings

For static-analysis and security adoption, repositories may already contain technical debt. A practical rollout can:

- establish an approved baseline;
- prevent new prohibited findings;
- track existing findings separately;
- define remediation ownership;
- reduce the baseline over time.

The Validator Agent must report both categories accurately. It must not create or update the baseline without authorization.

### Validate the Validation System

Test the harness itself.

Useful tests include:

- a failing build produces Fail;
- a missing scanner produces Blocked;
- a malformed report produces Inconclusive;
- a skipped critical test prevents Pass;
- missing evidence prevents Pass;
- a breaking contract change fails;
- a destructive migration is detected;
- secret values are redacted;
- a retry preserves the original result;
- a modified gate triggers additional approval.

A validation system that has never been tested under failure conditions cannot be assumed to fail safely.

### Use Policy as Code for Aggregation

Do not leave overall-result calculation to conversational interpretation.

Encode rules such as:

```text
If any mandatory gate fails:
    Overall status = Fail

Else if any mandatory gate is blocked:
    Overall status = Blocked

Else if any mandatory gate is inconclusive:
    Overall status = Inconclusive

Else if any required evidence is missing:
    Overall status = Inconclusive

Else:
    Overall status = Pass
```

The AI agent may explain the outcome, but the harness should calculate it.

### Model Gate Dependencies Explicitly

Declare which gates require earlier gates.

For example:

- tests require a successful build;
- migration application requires a running disposable database;
- contract compatibility requires a valid generated contract;
- deployment policy evaluation requires rendered manifests.

When a prerequisite fails, report dependent gates as not executed because of that dependency. Do not omit them from the report.

### Parallelize Only Independent Gates

Enterprise validation can be expensive. Safely reduce duration by running independent checks concurrently after prerequisites succeed.

For example, after a successful build:

- architecture tests;
- static analysis;
- security scanning;
- API contract validation;
- event contract validation;
- deployment-policy validation

may run in parallel.

Parallelization must not weaken evidence collection or conceal dependencies.

### Introduce Risk-Based Validation Transparently

Use change classification to select specialized gates, but record why each gate was or was not selected.

For corporate fleet booking:

| Detected change | Triggered validation |
| --- | --- |
| Domain behavior | Domain and architecture tests |
| API model | OpenAPI and integration tests |
| Event model | Schema and compatibility tests |
| EF Core entity | Migration and database tests |
| Deployment manifest | Configuration and policy checks |

Avoid allowing an agent to classify a risky change as low impact merely to reduce validation time.

### Establish Exception Governance

When business urgency requires advancement despite a failed gate, record an exception rather than changing the validation result.

Require:

- accountable approver;
- affected gate;
- reason;
- risk statement;
- compensating control;
- expiration;
- remediation task;
- affected revisions and environments.

A failed check remains failed. The exception record explains why an authorized person accepted the risk.

### Keep Agent Roles Visible in Audit Records

Record which role performed each activity:

```text
Developer Agent: Revision c719b42 implementation
Reviewer Agent: Architecture and code review
Validator Agent: booking-service-v3 execution
Evaluator Agent: Outcome evaluation
Human approver: Release authorization
```

If the same platform performed multiple roles, record separate sessions and role transitions.

### Design for Multiple AI Platforms

Avoid binding the validation model to one vendor. Keep core definitions in portable repository assets:

- YAML validation profiles;
- scripts;
- test projects;
- contract schemas;
- policy definitions;
- structured result formats.

Platform-specific files for Claude Code, GitHub Copilot, and Codex should adapt these assets rather than redefine the rules.

### Use Centralized Reporting Carefully

A Harness Management UI can display:

- current validation runs;
- gate status;
- failed and blocked checks;
- retry count;
- median validation time;
- flaky-test trends;
- evidence links;
- revision and profile information.

The dashboard should obtain status from structured evidence. It should not infer success from the absence of error messages.

### Monitor Stale Validation Runs

Long-running or abandoned validations can incorrectly appear active.

Use:

- heartbeats;
- lease expirations;
- maximum gate durations;
- runner health checks;
- cancellation handling;
- explicit stale status.

A stale run should become **Blocked**, **Inconclusive**, or a dedicated operational status mapped by policy. It must not remain indefinitely “in progress” or be treated as passed.

### Make Failure Notifications Actionable

When validation fails, notify the responsible role with:

- task and revision;
- failed gate;
- expected and actual results;
- evidence link;
- reproducible command;
- recommended owner;
- next action.

Avoid notifications that contain only a failed job name.

### Track Validation Economics

Validation has a cost. Monitor:

- total execution time;
- compute consumption;
- environment provisioning time;
- queue time;
- artifact-storage cost;
- failure-detection value;
- repeated false positives;
- redundant gates.

Optimize execution without weakening coverage. For example, cache approved dependencies, parallelize independent checks, and use targeted pre-validation while preserving complete authoritative gates.

### Apply Different Profiles to Different Risk Levels

A useful enterprise model may define:

| Risk level | Example | Validation depth |
| --- | --- | --- |
| Low | Documentation or nonfunctional text | Formatting, link, and content checks |
| Moderate | Internal application behavior | Build, unit, integration, static analysis |
| High | Public API, event, database, identity | Full compatibility, security, migration, architecture checks |
| Critical | Regulated or production-control change | Full profile plus specialist and human approval |

Risk classification itself must be governed and auditable.

### Retain Human Oversight Where Consequences Are Significant

Human approval remains essential for:

- security exceptions;
- destructive data changes;
- breaking external contracts;
- privileged identity changes;
- production release;
- regulatory controls;
- validation-policy reductions.

Automation should make the decision evidence clearer, not erase accountability.

### Recommended Enterprise Rollout

A phased rollout can reduce adoption risk:

1. **Observe**

   Run the Validator Agent in advisory mode and compare its reports with current CI behavior.

2. **Standardize**

   Introduce shared result schemas, outcome vocabulary, validation profiles, and evidence manifests.

3. **Enforce**

   Make stable, high-value gates mandatory for protected branches.

4. **Expand**

   Add API, event, migration, infrastructure, resilience, and operational validation.

5. **Govern**

   Protect controls, manage exceptions, monitor quality, and audit role boundaries.

6. **Improve**

   Use failure history to strengthen tests, Skills, Instructions, architecture, and developer feedback.

The goal is not maximum automation on the first day. The goal is a validation capability whose results become more comprehensive without becoming less trustworthy.

Chapter 17 status: In progress — next section: Decision Points

## Decision Points

Designing a Validator Agent requires explicit decisions about scope, independence, execution, evidence, failure handling, and authority. These decisions should be encoded in repository Instructions, validation profiles, policies, and architecture records rather than left to an agent during execution.

### What Must Be Validated?

The first decision is which engineering conditions must be verified for each type of change.

Consider:

- affected application layers;
- business-critical behavior;
- API exposure;
- event publication or consumption;
- persistence changes;
- security impact;
- configuration changes;
- infrastructure impact;
- operational risk;
- regulatory obligations.

For Alpha Car Detailing corporate fleet booking, the change affects domain behavior, API contracts, persistence, the transactional outbox, event contracts, configuration, and deployment. The validation profile must cover all these areas.

**Decision:** Define change categories and map them to mandatory gates.

### What Is the Authoritative Validation Profile?

An organization may have enterprise profiles, repository profiles, service-specific extensions, and temporary release requirements. The harness must determine which combination is authoritative.

Consider:

- enterprise mandatory controls;
- repository Instructions;
- service-specific requirements;
- temporary Steering Notes;
- risk classification;
- regulatory requirements;
- release-specific gates.

**Decision:** Establish precedence and merging rules for validation profiles. Record the resolved profile and version in every validation report.

### Who Selects the Validation Profile?

Profile selection may be performed by:

- the Lead Agent;
- deterministic change-classification rules;
- repository configuration;
- a platform workflow;
- a human reviewer;
- a combination of these mechanisms.

Allowing the Validator Agent to choose freely may create inconsistent coverage.

**Decision:** Prefer deterministic or governed selection. Allow the Validator Agent to identify missing coverage and escalate, but not silently remove required gates.

### Which Gates Are Mandatory?

Not all checks necessarily block advancement. Some may be advisory while an organization is introducing a new control.

Classify gates as:

- mandatory;
- advisory;
- conditional;
- informational.

A mandatory gate cannot be ignored because most other checks passed. Advisory findings must remain visible and must not be presented as mandatory failures.

**Decision:** Declare gate criticality in the validation profile before execution.

### What Does Pass Mean for Each Gate?

Each gate requires a predefined success condition.

Examples include:

- build completes with zero errors and no prohibited warnings;
- all critical tests execute and pass;
- no new high or critical security findings exist;
- API comparison reports no unapproved breaking changes;
- event schema satisfies backward compatibility;
- migration succeeds against representative upgrade data;
- deployment manifests satisfy all mandatory policies.

**Decision:** Define measurable expected conditions and avoid phrases such as “looks acceptable.”

### Which Evidence Is Required?

A command may complete successfully without producing enough evidence for audit or reproduction.

Decide whether each gate requires:

- raw command output;
- exit code;
- machine-readable report;
- human-readable summary;
- tool version;
- environment metadata;
- artifact checksum;
- signed attestation;
- retained test data reference.

**Decision:** Define evidence requirements per gate and prevent Pass when mandatory evidence is missing.

### Where Should Validation Run?

Possible environments include:

- developer workstation;
- local container;
- isolated agent workspace;
- pull-request CI runner;
- dedicated validation environment;
- pre-production environment.

| Environment | Best use | Main limitation |
| --- | --- | --- |
| Developer workstation | Rapid feedback | Uncontrolled local state |
| Isolated agent workspace | Reproducible diagnosis | May lack enterprise services |
| Pull-request CI | Authoritative merge gates | Limited environment realism |
| Dedicated validation environment | Integration and migration checks | Provisioning cost |
| Pre-production | Release-level verification | Shared-state and scheduling concerns |

**Decision:** Specify which environment produces advisory evidence and which produces authoritative evidence.

### How Independent Must the Validator Be?

Independence can be implemented through:

- a different agent session;
- an isolated context;
- a clean checkout;
- a separate execution identity;
- a controlled CI workflow;
- different human ownership;
- protected validation assets.

Smaller teams may use one platform for development and validation, but role boundaries should still be explicit.

**Decision:** Define the minimum independence required for each risk level.

### Can the Validator Modify Files?

Authoritative validation should normally be read-only with respect to product source, tests, contracts, policies, and baselines.

The Validator Agent may need to create:

- build artifacts;
- test reports;
- generated API documents;
- temporary databases;
- rendered manifests;
- evidence packages.

**Decision:** Restrict writes to approved temporary and evidence locations. Any product modification must become a separate Developer Agent task.

### How Are Validation-Control Changes Handled?

A revision may legitimately modify:

- tests;
- architecture rules;
- contract baselines;
- validation profiles;
- workflows;
- scanner thresholds;
- suppression files.

Such changes can weaken the controls that judge the same revision.

**Decision:** Require additional ownership, independent review, and comparison with previous controls when validation assets change.

### Should Validation Fail Fast or Continue?

Fail-fast behavior shortens feedback time, but continuing independent checks may reveal additional defects in one run.

A practical policy is:

- stop checks whose prerequisites failed;
- continue independent checks when safe;
- preserve every result;
- keep the overall outcome failed after a mandatory failure.

**Decision:** Model gate dependencies and continuation rules explicitly.

### Which Gates Can Run in Parallel?

Build-dependent tests cannot run before compilation, but many post-build checks may execute concurrently.

Possible parallel gates include:

- architecture tests;
- static analysis;
- security scanning;
- API contract validation;
- event contract validation;
- deployment-policy checks.

**Decision:** Parallelize only independent checks and preserve separate evidence for each gate.

### How Should Skipped Tests Be Treated?

Skipped tests may represent:

- approved environment-specific exclusions;
- missing prerequisites;
- temporary quarantine;
- accidental test discovery failure;
- deliberate control avoidance.

**Decision:** Define which skipped tests cause Fail, Blocked, or Inconclusive. Record all skips and their authorization.

### What Is the Retry Policy?

Retries may be appropriate for documented transient infrastructure failures, but dangerous for inconsistent product behavior.

Define:

- which failures are retryable;
- maximum attempts;
- delay strategy;
- required evidence;
- authorization;
- outcome after inconsistent results;
- escalation threshold.

**Decision:** Preserve all attempts. Never allow the Validator Agent to rerun indefinitely until success.

### How Is Flakiness Classified?

A test that alternates between Pass and Fail does not provide reliable proof.

Possible policies include:

- classify as Fail;
- classify as Inconclusive;
- block advancement;
- allow visible, approved quarantine;
- require a reliability threshold over multiple controlled runs.

**Decision:** Establish a single organization-wide flaky-test policy and assign remediation ownership.

### What Happens When a Tool Is Unavailable?

Scanners, registries, package feeds, test environments, and artifact stores may be unavailable.

The harness must distinguish between:

- implementation failure;
- tool failure;
- environment failure;
- authorization failure;
- evidence-upload failure.

**Decision:** Define when unavailable tooling produces Blocked versus Inconclusive, and identify the escalation owner.

### How Are Security Findings Aggregated?

Security tools may produce findings with different severity scales and confidence levels.

Define:

- blocking severity;
- treatment of newly introduced findings;
- treatment of existing findings;
- false-positive review;
- suppression authority;
- exception expiration;
- unavailable-scanner behavior.

**Decision:** Let policy determine the outcome. Do not allow the Validator Agent to accept security risk.

### What API Compatibility Policy Applies?

API compatibility may be:

- backward compatible;
- forward compatible;
- both;
- versioned without compatibility;
- consumer-contract driven.

The appropriate choice depends on consumers and deployment strategy.

**Decision:** Define the baseline, compatibility direction, allowed changes, versioning rules, and approval authority.

### What Event Compatibility Policy Applies?

Event producers and consumers often deploy independently, making compatibility decisions especially important.

Define:

- schema format;
- registry or baseline authority;
- compatibility direction;
- required metadata;
- nullability rules;
- versioning rules;
- consumer tests;
- retention of historical schemas.

**Decision:** Require tool-based compatibility validation and prohibit the Validator Agent from visually approving an incompatible change.

### What Migration Scenarios Are Required?

Possible migration validation levels include:

- migration generation only;
- empty-database application;
- previous-version upgrade;
- representative-data upgrade;
- staged-deployment compatibility;
- performance and lock assessment;
- recovery or roll-forward rehearsal.

**Decision:** Match migration depth to data risk and deployment strategy. Production-relevant changes should not rely only on empty-database testing.

### What Infrastructure Effects Require Approval?

Infrastructure plans may create, modify, replace, or destroy resources.

Define approval requirements for:

- database deletion or replacement;
- public network exposure;
- privileged identity changes;
- firewall changes;
- region changes;
- encryption changes;
- resource replacement;
- significant cost changes;
- production configuration changes.

**Decision:** Let the Validator Agent detect and report these conditions. Reserve approval for authorized people.

### How Are Secrets Protected?

Validation may need configuration access without needing secret values in its output.

Decide:

- which identity can read secrets;
- whether secret access is necessary;
- how values are redacted;
- where evidence is stored;
- who can access sensitive artifacts;
- how long artifacts are retained.

**Decision:** Prefer validating secret references and binding behavior with short-lived, least-privilege credentials.

### How Long Is Evidence Retained?

Retention depends on:

- regulatory requirements;
- audit needs;
- incident investigation;
- release cadence;
- storage cost;
- data sensitivity.

**Decision:** Define retention by evidence class rather than using one indefinite policy for all artifacts.

### Can Evidence Be Reused?

Reusing unaffected evidence can reduce validation time but risks applying results to a different implementation.

Consider:

- whether the source revision changed;
- whether dependencies changed;
- whether generated output changed;
- whether gate configuration changed;
- whether the environment changed;
- whether impact analysis is deterministic.

**Decision:** Default to revalidation. Allow reuse only through explicit, auditable rules.

### How Is the Overall Result Calculated?

Avoid leaving aggregation to an agent’s prose summary.

A typical precedence rule is:

1. Any mandatory Fail makes the overall result Fail.
2. Otherwise, any mandatory Blocked makes it Blocked.
3. Otherwise, any mandatory Inconclusive makes it Inconclusive.
4. Otherwise, missing required evidence prevents Pass.
5. Only then may the result be Pass.

**Decision:** Encode aggregation as policy and generate the summary from structured results.

### Who Receives Each Outcome?

Define routing before execution:

| Outcome | Typical next destination |
| --- | --- |
| Pass | Evaluator Agent |
| Fail | Developer Agent |
| Blocked | Lead Agent, platform owner, or prerequisite owner |
| Inconclusive | Lead Agent, test owner, specialist, or human authority |
| Policy exception requested | Authorized human approver |

**Decision:** Make handoffs automatic where safe, but retain escalation boundaries.

### Can an Exception Permit Advancement?

An exception process may allow advancement despite a failed or unavailable gate. It must not rewrite the validation history.

**Decision:** Store validation and exception outcomes separately. Require an accountable approver, risk statement, compensating controls, expiration, and remediation owner.

### What Requires Human Approval?

Typical boundaries include:

- security-risk acceptance;
- destructive migration execution;
- breaking contract rollout;
- validation-policy weakening;
- production deployment;
- privileged infrastructure changes;
- regulatory exceptions.

**Decision:** Name specific accountable roles. “Human approval required” is less useful than “Database Owner and Release Manager approval required.”

### How Will Validation Quality Be Measured?

Measures may include:

- escaped defects;
- flaky-test frequency;
- validation duration;
- blocked-run rate;
- inconclusive-run rate;
- missing-evidence rate;
- retry frequency;
- false-positive rate;
- gate bypass attempts;
- time to resolution.

**Decision:** Use measurements to improve validation reliability, not to pressure teams into weakening controls.

### Alpha Car Detailing Decision Record

For the corporate fleet booking capability, the team records:

| Decision | Selected approach |
| --- | --- |
| Authoritative environment | Controlled pull-request CI |
| Profile | `booking-service-v3` |
| Revision binding | Full reviewed commit digest |
| Validator independence | Separate session and clean workspace |
| Build policy | Release build with warnings treated as errors |
| Test policy | All mandatory tests must execute and pass |
| API policy | No unapproved breaking OpenAPI changes |
| Event policy | Backward-compatible JSON Schema |
| Migration policy | Empty and representative upgrade scenarios |
| Security policy | No new high or critical findings |
| Retry policy | Infrastructure retries only; all attempts retained |
| Gate changes | Additional owner review required |
| Pass handoff | Evaluator Agent |
| Production approval | Release Manager and accountable service owner |

These decisions remove ambiguity before validation begins. The Validator Agent can then operate as an evidence-producing role rather than a policy-making role.

Chapter 17 status: In progress — next section: Exercises

## Exercises

The following exercises apply the Validator Agent concepts to the Alpha Car Detailing corporate fleet booking capability. They progress from role definition and gate design to failure handling and enterprise governance.

### Exercise 1: Define the Validator Agent Contract

Create a Validator Agent role definition containing:

- purpose;
- responsibilities;
- allowed actions;
- prohibited actions;
- required inputs;
- expected outputs;
- allowed outcome statuses;
- handoff rules;
- escalation conditions;
- human approval boundaries.

Your role definition must explicitly prevent the Validator Agent from:

- modifying implementation code;
- weakening tests or gates;
- ignoring failed checks;
- converting blocked or inconclusive results into passes;
- replacing the Reviewer Agent;
- replacing the Evaluator Agent;
- approving production deployment.

**Expected outcome:** A concise, platform-neutral role contract that can be adapted for Claude Code, GitHub Copilot, and Codex.

### Exercise 2: Distinguish Review, Validation, and Evaluation

Classify each activity as primarily belonging to the Reviewer Agent, Validator Agent, or Evaluator Agent:

1. Determine whether the application handler is easy to maintain.
2. Execute the architecture test suite.
3. Decide whether fleet coordinators can complete the booking workflow effectively.
4. Identify an unnecessary abstraction.
5. Compare the generated OpenAPI document with the approved baseline.
6. Decide whether the implementation fulfills the intended business outcome.
7. Run a vulnerable dependency scan.
8. assess whether the selected domain model expresses the business clearly.
9. verify that repeated requests with the same idempotency key create one booking.
10. determine whether the feature should be accepted despite a confusing user workflow.

**Expected outcome:**

- Reviewer Agent: 1, 4, 8
- Validator Agent: 2, 5, 7, 9
- Evaluator Agent: 3, 6, 10

Explain why none of the three roles replaces human approval.

### Exercise 3: Design a Validation Profile

Create a YAML validation profile for the Booking service.

Include gates for:

- revision integrity;
- locked dependency restoration;
- Release build;
- domain tests;
- application tests;
- integration tests;
- architecture tests;
- static analysis;
- security scanning;
- API contract compatibility;
- event contract compatibility;
- database migration safety;
- configuration validation;
- deployment-policy validation.

For each gate, define:

- identifier;
- description;
- required or advisory status;
- prerequisites;
- expected condition;
- evidence artifacts;
- timeout;
- retry policy;
- failure owner.

**Expected outcome:** A versioned profile that can be interpreted consistently by an AI agent or conventional CI workflow.

### Exercise 4: Model Gate Dependencies

Create a Mermaid flowchart showing the dependencies among these checks:

- restore;
- build;
- unit tests;
- architecture tests;
- integration tests;
- API contract validation;
- event contract validation;
- migration validation;
- security scanning;
- deployment-policy validation;
- result aggregation.

Apply these rules:

- build depends on restore;
- compiled-code tests depend on build;
- migration validation requires the integration database;
- security and deployment checks may run independently of compilation when their inputs are available;
- aggregation receives every executed or non-executed result.

**Expected outcome:** A graph that allows safe parallel execution while preserving prerequisite relationships.

### Exercise 5: Define Outcome Aggregation

Write pseudocode that calculates the overall validation outcome.

Requirements:

- any failed mandatory gate produces **Fail**;
- otherwise, any blocked mandatory gate produces **Blocked**;
- otherwise, any inconclusive mandatory gate produces **Inconclusive**;
- missing required evidence prevents **Pass**;
- advisory findings remain visible but do not automatically fail validation;
- only complete success produces **Pass**.

Test the logic against these cases:

| Case | Gate results | Expected overall result |
|---|---|---|
| A | All mandatory gates pass | Pass |
| B | Twelve pass, one fails | Fail |
| C | All completed gates pass, one is blocked | Blocked |
| D | All gates pass, but a required report is missing | Inconclusive |
| E | Mandatory gates pass, advisory gate fails | Pass with advisory finding |
| F | One gate fails and another is blocked | Fail |

### Exercise 6: Create a Validation Result Schema

Design a JSON Schema or C# model for an individual validation result.

It must include:

- check identifier;
- check performed;
- expected condition;
- actual result;
- evidence;
- status;
- failure details;
- recommended next action;
- start and completion timestamps;
- tool name and version;
- source revision;
- retry history.

Then design a validation-run model that aggregates all results.

**Expected outcome:** A machine-readable structure suitable for automation, reporting, auditing, and a Harness Management UI.

### Exercise 7: Validate Revision Integrity

Write a PowerShell script that:

1. accepts an expected Git commit;
2. reads the current full commit identifier;
3. confirms that it matches the expected commit;
4. checks for uncommitted changes;
5. writes a JSON evidence report;
6. exits unsuccessfully when the revision differs or the workspace is dirty.

The script must not delete, reset, stash, or overwrite user changes.

**Expected outcome:** A safe revision-integrity gate that can run before all other validation.

### Exercise 8: Build with Evidence

Create a PowerShell validation step that:

- restores the solution using locked dependencies;
- builds the Release configuration;
- treats warnings as errors;
- captures a binary log;
- captures standard output;
- records the exit code;
- creates a structured result;
- preserves evidence after failure.

Use the Alpha Car Detailing solution as the target:

```text
AlphaCarDetailing.sln
```

**Expected outcome:** A reproducible build gate rather than a conversational claim that the solution compiles.

### Exercise 9: Validate Test Completeness

Assume the Booking service has five required test projects:

1. Domain tests
2. Application tests
3. Integration tests
4. Architecture tests
5. Contract tests

Design a validation step that compares:

- expected test projects;
- discovered test projects;
- executed test projects;
- produced test reports;
- total tests;
- passed tests;
- failed tests;
- skipped tests.

Classify these scenarios:

| Scenario | Expected status |
|---|---|
| All five projects run and pass | Pass |
| One project produces no report | Inconclusive |
| One required test fails | Fail |
| A critical test is skipped without approval | Fail |
| The integration environment is unavailable | Blocked |
| The test report is truncated | Inconclusive |

### Exercise 10: Test Idempotency Under Concurrency

Write an integration test for the corporate fleet booking endpoint that submits two concurrent requests with the same idempotency key.

Verify that:

- one logical booking is created;
- both responses refer to the same booking;
- one applicable outbox message exists;
- no partial records remain;
- the database contains the approved unique constraint.

**Expected outcome:** Deterministic evidence for idempotent behavior under concurrency rather than sequential duplicate requests alone.

### Exercise 11: Validate Transactional Outbox Behavior

Create integration tests covering:

1. A successful booking commits the booking and outbox message.
2. A domain failure commits neither.
3. A persistence failure commits neither.
4. A retry does not create duplicate outbox messages.
5. The outbox payload conforms to the event contract.

**Expected outcome:** Evidence that business persistence and event publication intent share the correct transactional boundary.

### Exercise 12: Detect an Architecture Violation

Introduce the following controlled violation on an exercise branch:

```text
Application project
    ↓ direct dependency
Infrastructure BookingDbContext
```

Create or execute an architecture test that detects the dependency.

Record:

- expected condition;
- detected dependency path;
- test evidence;
- status;
- recommended next action.

Then remove the exercise violation and verify that the test passes.

**Expected outcome:** An understanding of how architectural decisions become executable gates.

### Exercise 13: Validate an API Contract

Generate the OpenAPI document for the Booking API and compare it with the approved baseline.

Introduce one breaking change at a time:

- remove the `409 Conflict` response;
- make `bookingId` optional;
- change `stationId` from a UUID string to an integer;
- remove the required `Idempotency-Key` header;
- rename the endpoint route.

For each change:

1. execute the compatibility tool;
2. record the detected difference;
3. classify the result;
4. identify who must approve any intended breaking change.

**Expected outcome:** A clear separation between detecting incompatibility and approving a new contract version.

### Exercise 14: Validate an Event Contract

Validate the `CorporateFleetBookingCreated` payload against JSON Schema draft 2020-12.

Check:

- event envelope;
- schema name;
- schema version;
- required identifiers;
- `stationId`;
- positive `vehicleCount`;
- ISO date format;
- additional properties;
- representative serialization.

Then make `stationId` optional without changing the schema version.

**Expected outcome:** A failed compatibility result supported by a schema-difference report.

### Exercise 15: Validate a Database Migration

Create a migration for the corporate fleet booking tables and validate it in two environments:

1. an empty database;
2. a database at the previous supported version containing representative data.

Verify:

- migration execution;
- schema objects;
- foreign keys;
- unique idempotency constraint;
- required indexes;
- data preservation;
- application startup;
- booking creation after upgrade.

**Expected outcome:** A migration report that distinguishes clean creation from upgrade safety.

### Exercise 16: Analyze a Destructive Migration

Assume a proposed migration drops `ExternalReference` from the booking table.

Prepare a validation report containing:

- detected destructive operation;
- affected data;
- expected policy;
- actual result;
- evidence;
- status;
- required escalation;
- potential roll-forward options.

Do not approve or implement the migration strategy.

**Expected outcome:** A **Fail** result followed by escalation to the architect, database owner, and appropriate human approver.

### Exercise 17: Validate Configuration Safely

Create tests for strongly typed `BookingEventOptions`.

Verify that:

- required properties are present;
- retry count remains within range;
- invalid duration formats fail;
- startup validation is enabled;
- secret-store references are used;
- no secret values appear in validation evidence.

**Expected outcome:** A configuration report that proves binding and validation without revealing protected values.

### Exercise 18: Validate Deployment Manifests

Render the Booking API Kubernetes manifests and apply checks for:

- valid syntax;
- non-root execution;
- resource requests and limits;
- readiness and liveness probes;
- approved service account;
- workload identity;
- secret-store integration;
- no unintended public service;
- approved container registry;
- prohibited privilege settings.

Introduce one policy violation and confirm that the gate fails.

**Expected outcome:** A machine-readable deployment-policy report and an actionable failure summary.

### Exercise 19: Classify Failure Scenarios

Assign **Pass**, **Fail**, **Blocked**, or **Inconclusive** to each scenario:

1. Build completes with zero errors and all evidence is present.
2. An integration test assertion fails consistently.
3. The schema registry rejects the validation identity.
4. The security scanner terminates without producing a complete report.
5. A test fails, passes, and fails under identical conditions.
6. All tests pass, but the architecture report is missing.
7. The migration succeeds against an empty database but fails against representative data.
8. The API compatibility tool reports a breaking change.
9. All mandatory checks pass; one informational lint rule reports a warning.
10. The validation profile cannot be found.

**Expected outcome:**

| Scenario | Status |
|---|---|
| 1 | Pass |
| 2 | Fail |
| 3 | Blocked |
| 4 | Inconclusive |
| 5 | Inconclusive or Fail, according to approved flaky-test policy |
| 6 | Inconclusive |
| 7 | Fail |
| 8 | Fail |
| 9 | Pass with informational finding |
| 10 | Blocked |

### Exercise 20: Design a Retry Policy

Create a retry policy for:

- package-feed timeout;
- disposable database startup failure;
- assertion failure;
- security scanner crash;
- schema-registry rate limit;
- flaky integration test;
- evidence-upload failure.

For each condition, define:

- whether retry is allowed;
- maximum attempts;
- delay;
- required evidence;
- final status after exhaustion;
- escalation owner.

**Expected outcome:** A policy that supports transient recovery without allowing repeated execution to hide product failures.

### Exercise 21: Preserve Retry History

Create a structured retry record for this test sequence:

```text
Attempt 1: Fail—timeout
Attempt 2: Pass
Attempt 3: Fail—timeout
```

Include:

- source revision;
- runner identity;
- environment;
- timestamps;
- command;
- result artifacts;
- reason for each retry;
- final classification.

Explain why reporting only Attempt 2 would be misleading.

### Exercise 22: Detect Validation-Control Changes

Create a rule that identifies modifications to:

```text
.github/workflows/**
harness/validation/**
harness/scripts/**
contracts/**
tests/**/ArchitectureTests/**
security/**
```

When such a change is detected, require:

- additional code owners;
- explicit risk classification;
- comparison with previous behavior;
- prevention of automatic approval;
- complete audit evidence.

**Expected outcome:** A protected workflow for changes that can alter future validation outcomes.

### Exercise 23: Create a Failure Handoff

Prepare a handoff for this failure:

```text
Task: ACD-CFB-142
Revision: 8d34e7a
Gate: Integration tests
Expected: One booking per idempotency key
Actual: Two bookings created
```

Include all required result fields and route the work to the Developer Agent.

Your handoff must not prescribe an unreviewed implementation or claim that the test is incorrect.

### Exercise 24: Create a Passing Handoff

Prepare a passing handoff containing:

- task identifier;
- reviewed revision;
- validation profile;
- number of required and executed gates;
- evidence location;
- overall status;
- unresolved findings;
- next role;
- human approval reminder.

Route the result to the Evaluator Agent.

**Expected outcome:** A concise handoff that confirms deterministic validation without claiming business acceptance or production readiness.

### Exercise 25: Design a Harness Dashboard

Design a Validator Agent dashboard for the Alpha Car Detailing harness.

Include:

- task and revision;
- current role;
- validation profile;
- overall status;
- gate status;
- running, passed, failed, blocked, and inconclusive counts;
- execution duration;
- retry count;
- stale-run indicator;
- evidence links;
- correction history;
- next assigned role.

The dashboard must calculate status from structured evidence rather than from agent-generated prose.

### Exercise 26: Threat-Model the Validator Agent

Identify threats involving:

- prompt injection from repository content;
- malicious test changes;
- weakened workflow filters;
- forged evidence;
- secret exposure;
- excessive permissions;
- altered contract baselines;
- deleted failure reports;
- unauthorized retries;
- validation of the wrong revision.

For each threat, define:

- likelihood;
- impact;
- preventive control;
- detective control;
- response.

**Expected outcome:** A security model for the validation capability itself.

### Exercise 27: Compare Platform Implementations

Design the same Validator Agent workflow for:

- Claude Code;
- GitHub Copilot;
- OpenAI Codex.

Keep these elements identical:

- role responsibilities;
- validation profile;
- outcome vocabulary;
- prohibited actions;
- evidence schema;
- handoff rules.

Document only the platform-specific differences in:

- instruction files;
- command execution;
- CI integration;
- workspace isolation;
- artifact handling.

**Expected outcome:** A vendor-neutral Validator Agent contract with three platform adapters.

### Exercise 28: Test the Validation Harness

Create controlled failure cases proving that the harness:

- reports Fail when a mandatory test fails;
- reports Blocked when a required dependency is unavailable;
- reports Inconclusive for malformed evidence;
- prevents Pass when a report is missing;
- preserves failed retry attempts;
- detects a breaking contract;
- detects a destructive migration;
- redacts secrets;
- identifies a changed validation gate;
- prevents advancement after a mandatory failure.

**Expected outcome:** Evidence that the validation system itself fails safely.

### Exercise 29: Write an Architecture Decision Record

Create an ADR for the Alpha Car Detailing Validator Agent architecture.

Address:

- problem and context;
- role boundaries;
- authoritative execution environment;
- validation-profile ownership;
- gate-selection strategy;
- evidence storage;
- outcome aggregation;
- retry policy;
- validation-control protection;
- exception governance;
- human approval boundaries;
- consequences and trade-offs.

**Expected outcome:** An approved architectural foundation for implementing the Validator Agent in the enterprise harness.

### Exercise 30: Conduct a Full Validation Simulation

Simulate the complete workflow:

1. Developer Agent submits the corporate fleet booking revision.
2. Reviewer Agent approves it for validation.
3. Validator Agent resolves the applicable profile.
4. The build passes.
5. Unit and architecture tests pass.
6. An integration test detects duplicate bookings.
7. The Validator Agent reports Fail.
8. The Developer Agent submits a corrected revision.
9. The Reviewer Agent reviews the correction.
10. The Validator Agent executes the complete profile.
11. All mandatory gates pass.
12. The evidence package is forwarded to the Evaluator Agent.
13. The Evaluator Agent assesses the business outcome.
14. A human retains release approval.

For each step, record:

- active role;
- input;
- action;
- evidence;
- outcome;
- next role.

**Expected outcome:** A complete, traceable exercise demonstrating that review, deterministic validation, evaluation, and human approval are complementary controls.

Chapter 17 status: In progress — next section: Interview Questions

## Interview Questions

### 1. What is a Validator Agent?

A Validator Agent is an independent harness role responsible for verifying that implementation outputs satisfy objective, testable, and repeatable engineering conditions. It executes or coordinates deterministic gates, collects evidence, classifies results, and routes the outcome to the next appropriate role.

### 2. What is the core responsibility of the Validator Agent?

Its core responsibility is to answer:

> Does the reviewed implementation satisfy the defined validation conditions, and what evidence proves the result?

It must produce traceable evidence rather than rely on confidence, explanation, or subjective judgment.

### 3. What is deterministic validation?

Deterministic validation uses a defined input, repeatable procedure, explicit expected condition, observable result, and consistent status rule.

For example:

> Build revision `c719b42` using the approved .NET SDK in Release configuration. The gate passes only if the command completes with zero errors and no prohibited warnings.

### 4. Why is the Validator Agent separate from the Developer Agent?

The Developer Agent creates the implementation and naturally develops assumptions about its correctness. Independent validation reduces confirmation bias and prevents the implementation role from selecting favorable checks, dismissing failures, or approving its own work.

The Developer Agent should run tests during development, but authoritative validation belongs to an independent role and controlled process.

### 5. How does the Validator Agent differ from the Reviewer Agent?

The Reviewer Agent applies informed judgment to code quality, architecture alignment, maintainability, security concerns, and compliance with Instructions.

The Validator Agent executes objective checks and reports deterministic evidence.

For example:

- Reviewer: “The handler contains too much business logic.”
- Validator: “The architecture test passed because the API does not reference Infrastructure directly.”

Review and validation complement each other; neither replaces the other.

### 6. How does the Validator Agent differ from the Evaluator Agent?

The Validator Agent verifies whether defined engineering gates passed. The Evaluator Agent determines whether the completed implementation achieves the intended outcome at an acceptable quality level.

A feature may pass all builds, tests, and contract checks but still fail evaluation because it does not satisfy an important user scenario.

### 7. Can a Validator Agent approve production deployment?

No. A validation Pass confirms only that the identified revision satisfied the configured deterministic conditions. Production approval may require business acceptance, operational readiness, security-risk decisions, change management, and accountable human authorization.

### 8. What are the primary inputs to a Validator Agent?

Typical inputs include:

- task identifier;
- reviewed source revision;
- implementation evidence;
- review result;
- applicable Instructions;
- validation profile;
- acceptance criteria;
- affected-component inventory;
- environment definition;
- evidence policy.

The source revision must be explicit and immutable.

### 9. What outputs should a Validator Agent produce?

It should produce:

- overall status;
- per-gate status;
- check performed;
- expected condition;
- actual result;
- evidence;
- failure details;
- recommended next action;
- environment and tool information;
- retry history;
- source revision;
- handoff or escalation destination.

### 10. What result statuses should a Validator Agent use?

The standard outcomes are:

- **Pass** — the check executed, met its expected condition, and produced sufficient evidence.
- **Fail** — the check executed and violated its expected condition.
- **Blocked** — a prerequisite prevented the check from executing.
- **Inconclusive** — the check ran or partially ran, but the evidence cannot support a reliable Pass or Fail.

### 11. What is the difference between Blocked and Inconclusive?

**Blocked** means validation could not proceed because a prerequisite was unavailable, such as missing permissions or an unavailable test environment.

**Inconclusive** means execution occurred, but the evidence is incomplete, contradictory, or unreliable, such as a truncated report or inconsistent test outcomes.

Neither result permits a Pass.

### 12. How should the overall validation result be calculated?

A typical policy is:

1. Any mandatory failure produces **Fail**.
2. Otherwise, any mandatory blocked result produces **Blocked**.
3. Otherwise, any mandatory inconclusive result produces **Inconclusive**.
4. Missing required evidence prevents **Pass**.
5. Only complete success produces **Pass**.

The harness should calculate this outcome from structured results rather than leaving it to agent interpretation.

### 13. Why is a successful build not complete validation?

A build verifies compilation and selected compiler rules. It does not prove:

- correct business behavior;
- authorization;
- idempotency;
- contract compatibility;
- migration safety;
- secure configuration;
- deployment-policy compliance;
- operational suitability.

Compilation is one validation gate, not the complete validation process.

### 14. What should a build gate verify?

A robust build gate may verify:

- restoration from approved dependency sources;
- locked dependencies;
- approved SDK and compiler versions;
- Release configuration;
- affected project coverage;
- warnings-as-errors policy;
- generated source;
- package creation;
- build artifact identity;
- reproducibility requirements.

### 15. How should skipped tests be handled?

The validation profile must define how skipped tests affect the result. Critical or unauthorized skips should normally fail validation. Approved environment-specific skips should remain visible.

The report should record:

- discovered tests;
- executed tests;
- passed tests;
- failed tests;
- skipped tests;
- filtered tests.

A successful test-runner exit code must not hide skipped mandatory tests.

### 16. How should a Validator Agent handle a flaky test?

It should preserve every attempt and apply the approved flaky-test policy. It must not rerun the test until one attempt passes and then report only the successful result.

If the same test passes and fails under materially identical conditions, the result is normally **Inconclusive** or **Fail**, depending on organizational policy.

### 17. When is a retry appropriate?

A retry may be appropriate for:

- a documented package-feed timeout;
- approved rate-limit recovery;
- disposable environment startup failure;
- a scanner’s transient infrastructure error;
- additional diagnostic collection.

A retry is generally not appropriate merely because a product assertion failed.

Every retry must record its reason, source revision, environment, authorization, and outcome.

### 18. Why must earlier failed attempts be preserved?

Earlier attempts are part of the evidence. Removing them can make unstable behavior appear reliable.

For example:

```text
Attempt 1: Fail
Attempt 2: Pass
Attempt 3: Fail
```

This sequence demonstrates inconsistency. Reporting only Attempt 2 would misrepresent the result.

### 19. Can the Validator Agent edit a failing test?

Not during authoritative validation. Editing the test would change the control used to judge the implementation.

If the test is believed to be incorrect, the Validator Agent should report and escalate the problem. The test correction must be handled as a governed change with independent review.

### 20. Can the Validator Agent modify implementation code?

No, unless it is explicitly reassigned to the Developer Agent role through a separate task. Changing implementation code during validation invalidates the relationship between the reviewed revision and the evidence.

The corrected revision must be reviewed and validated again.

### 21. What is a validation profile?

A validation profile is a versioned definition of the checks required for a particular category of change.

It defines:

- applicable change types;
- mandatory and advisory gates;
- approved tools and commands;
- prerequisites;
- expected conditions;
- severity thresholds;
- evidence requirements;
- retry policies;
- aggregation rules;
- escalation conditions.

### 22. How should a validation profile be selected?

Selection should be based on governed rules involving:

- changed components;
- architecture boundaries;
- API or event impact;
- persistence changes;
- security impact;
- infrastructure impact;
- risk classification;
- enterprise policy.

The Validator Agent may identify missing coverage, but it should not silently select a weaker profile.

### 23. What is change-based validation?

Change-based validation selects specialized gates according to the artifacts and risks affected by a change.

For example:

- domain changes trigger domain and architecture tests;
- API changes trigger OpenAPI compatibility checks;
- event changes trigger schema validation;
- EF Core changes trigger migration validation;
- deployment changes trigger infrastructure-policy checks.

The selection rules and exclusions must be traceable.

### 24. What evidence should be collected for each validation check?

Each result should include:

- check performed;
- expected condition;
- actual result;
- command or workflow;
- exit code;
- relevant logs;
- machine-readable report;
- tool version;
- source revision;
- status;
- failure details;
- recommended next action.

Evidence must be sufficient for another authorized participant to understand and reproduce the result.

### 25. Why are machine-readable reports important?

Machine-readable reports allow deterministic aggregation and automated verification. Examples include:

- TRX or JUnit test results;
- SARIF analysis findings;
- JSON contract differences;
- XML coverage reports;
- JSON policy results;
- structured migration reports.

They reduce dependence on an AI agent’s natural-language summary.

### 26. What should happen when required evidence is missing?

A missing required artifact prevents a complete Pass. The result should be **Inconclusive** or **Fail**, depending on policy.

The Validator Agent must not assume that a missing report means the tool found no issues.

### 27. How do you ensure that the correct revision was validated?

Record and verify:

- the full commit identifier;
- the reviewed commit identifier;
- working-tree status;
- submodule revisions;
- generated inputs;
- artifact digests where appropriate.

Authoritative validation should stop if the workspace is dirty or the commit does not match the reviewed revision.

### 28. Can evidence from one revision be reused for another?

The safe default is no. Even a small change can affect compilation, dependencies, generated contracts, or test behavior.

Evidence reuse should occur only through explicit and auditable impact-analysis rules. The report must identify which evidence was reused and why.

### 29. Why should validation run in an isolated environment?

Isolation reduces the influence of:

- uncommitted files;
- cached outputs;
- developer-specific tools;
- uncontrolled environment variables;
- shared mutable databases;
- locally running services;
- unapproved credentials.

A clean environment improves repeatability and confidence in the evidence.

### 30. What is architecture validation?

Architecture validation converts selected architectural rules into executable checks.

Examples include:

- Domain cannot reference Infrastructure.
- Application cannot reference API.
- API controllers cannot access the database context directly.
- Microservices cannot reference another service’s internal assemblies.
- Integration events must use the approved envelope.

Architecture tests complement architecture review; they do not replace it.

### 31. What is static analysis?

Static analysis examines source code or compiled artifacts without executing the full application behavior.

It may detect:

- compiler and nullable warnings;
- suspicious code paths;
- unsafe APIs;
- injection risks;
- resource-management problems;
- maintainability issues;
- secrets;
- prohibited patterns.

The validation policy must define which findings cause failure.

### 32. How should security validation be governed?

Security validation should define:

- approved tools;
- severity thresholds;
- treatment of new and existing findings;
- suppression authority;
- evidence requirements;
- scanner-unavailability behavior;
- exception governance.

The Validator Agent detects and reports security conditions. It does not accept security risk.

### 33. How should existing vulnerabilities be handled?

The organization may baseline existing findings and block newly introduced ones while remediation proceeds. That treatment must be defined by policy.

The Validator Agent must report whether a finding is new or pre-existing and must not create a new baseline without authorization.

### 34. What is API contract validation?

API contract validation confirms that the implementation conforms to its published interface and compatibility policy.

It may verify:

- OpenAPI validity;
- paths and operations;
- required parameters;
- request and response schemas;
- status codes;
- media types;
- authentication definitions;
- problem response formats;
- breaking changes;
- consumer contract tests.

### 35. Why are provider-side API tests insufficient for compatibility?

Provider tests prove only the scenarios they execute against the current provider. They may not detect changes that break existing consumers, such as removed fields, changed types, or altered required properties.

Compatibility validation compares the proposed contract with an approved baseline or consumer expectations.

### 36. What is event contract validation?

Event contract validation confirms that published or consumed events satisfy their schemas and evolution policies.

It may verify:

- schema syntax;
- required fields;
- data types;
- event envelope;
- schema name and version;
- serialization behavior;
- backward or forward compatibility;
- representative consumer deserialization.

### 37. What should happen when an event change is intentionally incompatible?

The Validator Agent should report the incompatibility as a failure under the current contract policy.

A separate governance process may approve a new schema version, rollout plan, consumer migration, or exception. The Validator Agent cannot convert the incompatible result into a Pass.

### 38. How should EF Core migrations be validated?

Migration validation should include:

- applying the full migration chain to an empty database;
- upgrading from the previous supported schema;
- using sanitized representative data;
- verifying constraints and indexes;
- detecting destructive operations;
- starting the application after upgrade;
- executing relevant business scenarios;
- assessing staged-deployment compatibility.

Empty-database success alone is insufficient.

### 39. Why is representative data important for migration validation?

Existing data can reveal problems that an empty database cannot, including:

- null values;
- duplicates;
- constraint violations;
- incompatible formats;
- long-running updates;
- destructive transformations.

Representative data helps establish whether the supported upgrade path is safe.

### 40. Does the existence of an EF Core `Down` method prove rollback safety?

No. A syntactically valid `Down` method may lose data or be unsafe after production traffic uses the new schema.

Enterprises should assess roll-forward, expand-and-contract deployment, backup, recovery, and application-version compatibility separately.

### 41. What is configuration validation?

Configuration validation verifies that required settings are present, typed, scoped, securely referenced, and accepted by application startup.

It may check:

- strongly typed options;
- required keys;
- ranges and formats;
- environment overlays;
- secret-store references;
- feature flags;
- retry and timeout settings;
- telemetry configuration.

### 42. How should secrets be handled during validation?

Evidence should record secret-reference presence and binding success without recording protected values.

The Validator Agent should use least-privilege, short-lived identities and redact logs. It should never capture full environment dumps or plaintext credentials as evidence.

### 43. What is infrastructure validation?

Infrastructure validation verifies infrastructure-as-code, deployment manifests, and generated plans against objective conditions.

Checks may include:

- syntax;
- plan generation;
- policy compliance;
- network exposure;
- identity privileges;
- encryption;
- resource limits;
- container security;
- health probes;
- deprecated APIs;
- destructive actions.

### 44. Why is infrastructure syntax validation insufficient?

A syntactically valid plan may still:

- delete a database;
- expose a service publicly;
- replace a critical resource;
- grant excessive permissions;
- disable encryption;
- remove resource limits.

Validation must assess planned effects and policy compliance.

### 45. How should a Validator Agent handle a destructive infrastructure plan?

It should record the planned destructive action, applicable policy, evidence, affected resources, and risk. It should then fail or escalate according to policy.

It must not approve or apply the change unless separately authorized, and production authorization should remain with accountable humans.

### 46. What is the transactional outbox, and how should it be validated?

The transactional outbox stores the business change and the intended integration event in the same database transaction.

Validation should verify:

- successful business operations create both records;
- failed operations create neither;
- retries do not create duplicate messages;
- outbox payloads satisfy the event contract;
- dispatch behavior is observable and recoverable.

The existence of an outbox table alone does not prove correct behavior.

### 47. How should idempotency be validated?

Idempotency should be tested with repeated and concurrent requests using the same key.

Validation should confirm:

- one logical result;
- no duplicate business records;
- no duplicate applicable outbox messages;
- consistent responses;
- atomic database enforcement;
- safe retry behavior.

Sequential tests alone may miss race conditions.

### 48. What happens when a mandatory gate fails?

The Validator Agent should:

1. preserve the original evidence;
2. identify the violated condition;
3. record the exact source revision;
4. provide a reproducible command;
5. recommend the responsible next role;
6. prevent advancement;
7. return the work for correction;
8. require review and revalidation of the corrected revision.

### 49. Should validation stop after the first failure?

It depends on gate dependencies and policy.

Dependent checks should stop when their prerequisite fails. Independent checks may continue to produce a fuller diagnostic picture. The overall result remains Fail after any mandatory failure.

### 50. What is a validation-control change?

A validation-control change modifies how future implementations are judged.

Examples include changes to:

- tests;
- architecture rules;
- profiles;
- workflow triggers;
- thresholds;
- contract baselines;
- security suppressions;
- evidence requirements.

Such changes require elevated scrutiny because they may weaken the control system.

### 51. How can validation-control changes be protected?

Use:

- code ownership;
- required specialist review;
- protected branches;
- path-based change detection;
- comparison with previous behavior;
- audit logs;
- signed artifacts;
- restricted permissions.

Where practical, execute both the previous and proposed controls during transition.

### 52. What is circular validation?

Circular validation occurs when an implementation supplies or modifies the rule by which it is judged.

Examples include:

- replacing an API baseline with the newly generated contract;
- modifying a failing test to match current behavior;
- lowering the threshold that the implementation violates;
- changing migration data to avoid a failure.

Expected conditions should come from independent, governed sources.

### 53. What is the difference between advisory and mandatory gates?

A mandatory gate must pass before advancement unless an authorized exception exists. An advisory gate records information but does not automatically block advancement.

The gate classification must be defined before execution. The Validator Agent must not downgrade a mandatory gate to advisory.

### 54. How should exceptions be handled?

An exception should not rewrite a failed validation result.

The exception record should include:

- affected gate;
- risk;
- business reason;
- compensating controls;
- authorized approver;
- scope;
- expiration;
- remediation owner.

Validation history and approval history remain separate.

### 55. What permissions should a Validator Agent have?

It should have the minimum authority required to:

- read the repository;
- restore approved dependencies;
- read contract baselines;
- create disposable test resources;
- execute validation tools;
- write evidence artifacts.

It should not normally have authority to merge, deploy to production, change protected policies, accept security risk, or access unrelated secrets.

### 56. How does Claude Code support the Validator Agent role?

Claude Code can:

- read repository Instructions;
- discover validation entry points;
- execute approved commands;
- coordinate multiple tools;
- diagnose failures;
- produce structured evidence summaries.

Its reasoning helps orchestrate validation, but authoritative results still come from deterministic tools and governed gates.

### 57. How does GitHub Copilot support the Validator Agent role?

GitHub Copilot can support:

- IDE-based pre-validation;
- repository prompt libraries;
- failure diagnosis;
- GitHub Actions authoring and interpretation;
- pull-request summaries;
- changed-file analysis.

GitHub Actions and required checks are typically better suited to authoritative merge validation than an informal local chat session.

### 58. How does OpenAI Codex support the Validator Agent role?

Codex can:

- inspect scoped repository Instructions;
- work in an isolated repository environment;
- execute terminal workflows;
- collect artifacts;
- diagnose compiler and test failures;
- create structured validation reports.

It must maintain role boundaries and avoid changing code while acting as Validator.

### 59. Can the same AI platform perform Developer and Validator roles?

Yes, but not as one undifferentiated operation.

Use:

- separate sessions;
- explicit role assignments;
- clean workspaces;
- immutable revisions;
- independent handoffs;
- different execution identities where appropriate;
- controlled CI for authoritative gates.

The role boundary matters more than the vendor name.

### 60. How should a vendor-neutral Validator Agent be designed?

Keep the core contract in portable repository assets:

- validation profiles;
- scripts;
- tests;
- schemas;
- policy definitions;
- structured evidence formats;
- handoff rules.

Use platform-specific Instructions only as adapters for Claude Code, GitHub Copilot, or Codex.

### 61. How can the validation system itself be tested?

Create controlled cases proving that the harness:

- fails on mandatory test failure;
- blocks on unavailable prerequisites;
- reports inconclusive evidence correctly;
- detects missing reports;
- preserves retries;
- catches breaking contracts;
- detects destructive migrations;
- redacts secrets;
- detects control changes;
- prevents advancement after failure.

A validation system must demonstrate that it fails safely.

### 62. What metrics indicate validation quality?

Useful measures include:

- escaped defects;
- flaky-test rate;
- failed, blocked, and inconclusive rates;
- retry frequency;
- missing-evidence frequency;
- false-positive rate;
- validation duration;
- time to correct failures;
- control-change frequency;
- exception volume and age.

Metrics should improve reliability rather than incentivize gate weakening.

### 63. How should stale validation runs be handled?

Use heartbeats, leases, maximum durations, and runner-health signals. A stale run should transition to a governed operational outcome rather than remain indefinitely active.

It must not be interpreted as Pass. Depending on policy and available evidence, it may become Blocked or Inconclusive.

### 64. Why is evidence integrity important?

A technically correct implementation cannot be distinguished from an unverified implementation if the evidence is incomplete or mutable.

Evidence integrity establishes:

- what was validated;
- which revision was used;
- which controls ran;
- what each control observed;
- whether retries occurred;
- whether gates changed;
- why the final status was assigned.

### 65. What makes a good validation failure report?

A good failure report is specific, reproducible, and actionable. It contains:

- the violated condition;
- expected and actual results;
- exact revision;
- affected component;
- raw evidence;
- reproducible command;
- status;
- recommended next role;
- required revalidation scope.

It does not hide the failure or prescribe an unreviewed implementation.

### 66. What should happen after the Developer Agent corrects a failed implementation?

The correction should:

1. produce a new immutable revision;
2. receive independent review;
3. run the targeted failed check for quick feedback;
4. run the complete required regression profile;
5. generate a new evidence package;
6. preserve the prior failure history;
7. proceed only if all mandatory gates pass.

### 67. What is the appropriate handoff after a validation Pass?

The Validator Agent forwards:

- reviewed revision;
- validation profile;
- gate results;
- evidence package;
- advisory findings;
- retry history;
- overall Pass status

to the Evaluator Agent.

The handoff must state that validation does not constitute business acceptance or production approval.

### 68. What is the most important design principle for a Validator Agent?

The Validator Agent must report what the evidence proves—not what the agent expects, prefers, or wants the result to be.

Its credibility depends on refusing to weaken conditions, conceal uncertainty, or manufacture a Pass.

Chapter 17 status: In progress — next section: Chapter Summary

## Further Reading

The following resources provide authoritative guidance for implementing deterministic validation, secure software delivery, contract verification, migration safety, and evidence-based engineering controls.

### .NET Build and Test Validation

- **Microsoft Learn — Testing in .NET**

  Introduces .NET test platforms, frameworks, unit tests, integration tests, load tests, code coverage, and execution through `dotnet test`. This is the primary starting point for building repeatable test gates for Alpha Car Detailing services. [Testing in .NET](https://learn.microsoft.com/en-us/dotnet/core/testing/)

- **Microsoft Learn — Unit Testing Best Practices with .NET**

  Describes practices for creating readable, isolated, repeatable, and maintainable unit tests. These principles help reduce nondeterministic tests and misleading validation results. [Unit testing best practices with .NET](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

- **Microsoft Learn — Integration Tests in ASP.NET Core**

  Explains integration testing with ASP.NET Core test hosts, test servers, dependency replacement, application configuration, and HTTP-based verification. [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)

- **Microsoft Learn — `dotnet test` Command**

  Documents the .NET CLI test command, result logging, filtering, configuration selection, diagnostic output, and other options required by automated validation scripts. [.NET CLI: `dotnet test`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test)

### Database Migration Validation

- **Microsoft Learn — EF Core Migrations Overview**

  Explains how EF Core generates, tracks, and applies incremental schema changes. It also provides the foundation for understanding why migration files must be reviewed and executed against realistic database states. [EF Core migrations overview](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)

- **Microsoft Learn — Applying EF Core Migrations**

  Compares migration deployment strategies, including command-line updates, SQL scripts, idempotent scripts, migration bundles, and application-time migration. [Applying migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying)

- **Microsoft Learn — Managing EF Core Migrations**

  Covers migration customization, rollback, removal, model snapshots, and detection of pending model changes. [Managing migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing)

Migration generation should be treated as the beginning of validation rather than proof of deployment safety. The migration must also be inspected and tested against supported upgrade scenarios.

### API and Event Contract Validation

- **OpenAPI Initiative — OpenAPI Specification**

  Defines the vendor-neutral format for describing HTTP APIs. Use it as the authoritative reference when validating paths, operations, parameters, request schemas, response schemas, authentication definitions, and compatibility. [OpenAPI Specification](https://spec.openapis.org/oas/latest.html)

- **JSON Schema — Draft 2020-12**

  Defines the JSON Schema version used by the Alpha Car Detailing event-contract examples. It covers schema structure, validation vocabularies, references, annotations, and instance-validation behavior. [JSON Schema Draft 2020-12](https://json-schema.org/draft/2020-12)

- **JSON Schema — Validating OpenAPI and JSON Schema**

  Discusses validation of OpenAPI documents containing JSON Schema dialects and explains the relationship between OpenAPI and JSON Schema validation. [Validating OpenAPI and JSON Schema](https://json-schema.org/blog/posts/validating-openapi-and-json-schema)

API or event schema validity does not by itself prove backward compatibility. Enterprise teams must also define and automate their permitted contract-evolution rules.

### Security Validation

- **NIST SP 800-218 — Secure Software Development Framework**

  Defines high-level secure software-development practices that can be incorporated into an organization’s software development lifecycle. It provides a useful foundation for security gates, toolchain protection, vulnerability response, and validation governance. [NIST Secure Software Development Framework](https://csrc.nist.gov/pubs/sp/800/218/final)

- **OWASP Application Security Verification Standard**

  Provides a structured basis for testing technical security controls in web applications and services. Organizations can select an appropriate verification level and translate relevant requirements into review, validation, and specialist security activities. [OWASP ASVS](https://owasp.org/www-project-application-security-verification-standard/)

- **OWASP Software Assurance Maturity Model**

  Provides a risk-driven framework for assessing and improving software-security practices across governance, design, implementation, verification, and operations. [OWASP SAMM](https://owaspsamm.org/)

- **OWASP Top 10**

  Summarizes important categories of web application security risk. It is useful for awareness and baseline coverage, although it is not a complete validation standard. [OWASP Top 10](https://owasp.org/www-project-top-ten/)

- **OWASP Dependency-Check**

  Documents a software composition analysis tool that identifies publicly disclosed vulnerabilities in project dependencies. Its output can become part of a governed dependency-security gate. [OWASP Dependency-Check](https://owasp.org/www-project-dependency-check/)

### Software Supply Chain and Evidence Provenance

- **SLSA — Supply-chain Levels for Software Artifacts**

  Defines a framework for improving software supply-chain integrity. Its provenance model is relevant to binding build and validation evidence to source revisions, build processes, and produced artifacts. [SLSA specification](https://slsa.dev/spec/)

- **SLSA — Provenance**

  Describes verifiable information about where, when, and how a software artifact was produced. These concepts support stronger validation evidence and artifact traceability. [SLSA provenance](https://slsa.dev/spec/v1.2/provenance)

- **in-toto Attestation Framework**

  Defines mechanisms for producing and verifying attestations about software supply-chain steps. It is relevant when validation evidence must be signed, portable, and independently verifiable. [in-toto attestation framework](https://in-toto.io/)

- **The Update Framework**

  Describes a framework for securing software update systems against repository and signing-key compromise. It provides useful background for organizations designing trusted tool, package, and artifact distribution. [The Update Framework](https://theupdateframework.io/)

### Continuous Integration and Evidence Retention

- **GitHub Docs — Workflow Artifacts**

  Explains how GitHub Actions workflows can store and share build outputs, test reports, logs, and other evidence. Artifact upload should normally occur even when an earlier validation step fails. [Store and share data with workflow artifacts](https://docs.github.com/en/actions/using-workflows/storing-workflow-data-as-artifacts)

- **GitHub Docs — About Protected Branches**

  Describes required status checks, review requirements, and branch protections that prevent conversational or local validation claims from bypassing authoritative workflows. [About protected branches](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches)

- **GitHub Docs — Code Owners**

  Explains how repository ownership rules can require specialist review for validation profiles, security policy, architecture tests, workflows, and contract baselines. [About code owners](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/about-code-owners)

### Infrastructure and Deployment Validation

- **Kubernetes Documentation — Configuration Best Practices**

  Provides guidance for defining and managing Kubernetes configuration. These practices support repeatable rendering, environment separation, configuration validation, and safer deployment artifacts. [Kubernetes configuration best practices](https://kubernetes.io/docs/concepts/configuration/overview/)

- **Kubernetes Documentation — Pod Security Standards**

  Defines privileged, baseline, and restricted security profiles for Kubernetes workloads. These standards can inform deterministic deployment-policy gates. [Pod Security Standards](https://kubernetes.io/docs/concepts/security/pod-security-standards/)

- **Open Policy Agent**

  Provides a general-purpose policy engine that can evaluate structured configuration and infrastructure decisions. It can be used to implement policy-as-code gates for Kubernetes, APIs, infrastructure plans, and application configuration. [Open Policy Agent documentation](https://www.openpolicyagent.org/docs/)

- **Conftest**

  Uses Open Policy Agent policies to test structured configuration files. It can support repeatable validation of Kubernetes manifests, infrastructure-as-code, and other declarative artifacts. [Conftest documentation](https://www.conftest.dev/)

### Architecture Validation

- **NetArchTest**

  Provides a fluent API for creating architecture tests in .NET. It can enforce namespace, dependency, inheritance, and naming rules as executable validation gates. [NetArchTest repository](https://github.com/BenMorris/NetArchTest)

- **ArchUnitNET**

  Provides architecture testing for .NET based on concepts from ArchUnit. It can validate layer dependencies, type relationships, naming rules, and architectural boundaries. [ArchUnitNET documentation](https://archunitnet.readthedocs.io/)

Architecture-test frameworks should enforce stable architectural invariants. They should not be used to encode every stylistic preference or replace independent architecture review.

### Reliability and Operational Validation

- **Google — Site Reliability Engineering**

  Covers monitoring, automation, testing, release engineering, incident response, and reliability practices. These concepts help extend validation beyond compilation and functional testing to operational behavior. [Site Reliability Engineering](https://sre.google/sre-book/table-of-contents/)

- **Google — Building Secure and Reliable Systems**

  Explores the relationship between security, reliability, design, deployment, recovery, and operational controls. It provides useful background for enterprise validation architecture. [Building Secure and Reliable Systems](https://google.github.io/building-secure-and-reliable-systems/)

### Related Chapters

Readers should connect this chapter with:

- **Chapter 5 — Repository Discovery**, for locating validation entry points and governing files.
- **Chapter 6 — Repository Analysis**, for understanding dependencies, test structure, contracts, and deployment assets.
- **Chapter 7 — Skills**, for packaging repeatable validation procedures.
- **Chapter 8 — Prompts**, for defining the current validation task and acceptance conditions.
- **Chapter 9 — Roles**, for responsibility, authority, and separation of duties.
- **Chapter 10 — Steering Notes**, for temporary delivery risks and validation priorities.
- **Chapter 11 — Knowledge Sources**, for selecting authoritative contracts, policies, and baselines.
- **Chapter 12 — What Is a Harness?**, for the overall controlled execution model.
- **Chapter 13 — Harness Architecture**, for deterministic gates, gate pipelines, hooks, and automated guardrails.
- **Chapter 14 — Lead Agent**, for validation-profile selection, workflow coordination, and escalation.
- **Chapter 15 — Developer Agent**, for implementation evidence and correction cycles.
- **Chapter 16 — Reviewer Agent**, for the independent judgment that precedes deterministic validation.

Together, these chapters establish a governed engineering workflow in which implementation, review, validation, evaluation, and approval remain separate but connected responsibilities.

Chapter 17 status: Complete
