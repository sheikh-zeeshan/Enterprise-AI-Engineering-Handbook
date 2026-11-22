# Chapters 1–10: development artifacts and completion criteria

Project: Enterprise AI Engineering Handbook. Workspace: `C:\AIHandbook`. Prepared: 22 November 2026.

This is a proposed development checklist based on the current chapters, the chapter-to-code implementation plan, and the repository audit. It does not claim the proposed files have been implemented. Retain the existing cumulative application rather than generating a separate application for each chapter.

## Starting point and path conventions

The current companion application is a .NET 10 Booking service with direct application services, SQLite, five source projects, and two test projects. CQRS remains deferred by ADR 0002. The previous audit compiled the solution, passed its three existing tests, and exercised the basic HTTP contract. A later standard restore failed while accessing NuGet vulnerability metadata. Resolve normal restore connectivity before closing a clean-checkout checkpoint; do not treat cached validation as a clean restore or complete dependency-security assessment.

Paths in the tables are relative to `C:\AIHandbook\` unless a table states otherwise. These abbreviations keep the artifact tables readable:

| Prefix | Resolves to |
| --- | --- |
| `A/` | `alpha-car-detailing/` |
| `B/` | `alpha-car-detailing/src/Services/Booking/` |
| `UT/` | `alpha-car-detailing/tests/Services/Booking/AlphaCarDetailing.Booking.Domain.Tests/` |
| `IT/` | `alpha-car-detailing/tests/Services/Booking/AlphaCarDetailing.Booking.IntegrationTests/` |
| `L1/` through `L10/` | The corresponding `examples/chXX-<topic>/` directory named in each chapter below. |

**Existing** means the file is present, not that it fully meets the chapter's teaching goal. **New** means proposed work. **Update** means retain and improve an existing artifact. The minimum completion criteria below are implementation checkpoints adapted to the current code, not a claim that every enterprise example in a chapter is runnable by that point.

Use the existing `A/instructions/` and `A/skills/` as application-specific authority. Put shared Role contracts under `harness/roles/` and task Prompts under `prompts/tasks/booking/`. For Chapter 10, use the existing application `A/steering/` directory and explicitly reference it from application entry points. Do not introduce a competing `Search/` directory just to match an illustrative manuscript tree.

## Summary table

| Chapter | Development objective | Expected artifact package | Current position | Completion checkpoint |
| --- | --- | --- | --- | --- |
| 1 — The Evolution of Software Development | Establish a runnable starting point and learn to define a safe change. | Solution/configuration, business overview, local run guide; fleet-quota ownership, task, impact, ADR, and test-design package. | Runnable baseline exists; exercise evidence package is missing. | Start the API; verify health; document the quota scenario without claiming its absent controls exist. |
| 2 — Understanding AI Coding Agents | Perform discovery, agree a bounded plan, implement a small change, and retain evidence. | Reliable build/test scripts, agent workflow guide, completion record, and registration-number tests. | Scripts and basic tests exist; workflow/completion assets are missing. | A small agent-assisted change produces a focused diff, real test output, and an explicit review state. |
| 3 — Comparing AI Coding Platforms | Reuse the same task and standards across supported tools. | Thin platform entry points, one vendor-neutral task contract, task/permission comparison, and recorded observations. | Entry points exist; controlled comparison package is missing. | Claude-first workflow is demonstrated; other platforms are evaluated consistently or marked not run. |
| 4 — Enterprise AI Engineering | Make architecture, ownership, review, and delivery controls concrete. | Existing layered projects/ADRs; capability map, asset catalog, ownership/approval matrix, PR template, and correctly placed CI. | Layers/ADRs exist; operating-model artifacts and root application CI are incomplete. | Trace a Booking use case, identify owners and enforced controls, and show how a change reaches review. |
| 5 — Repository Discovery | Produce an evidence-backed map of the actual repository. | Discovery script, current repository map, dependency map, behavior trace, risk register, discovery report. | Discovery baseline exists, but several manuscript example services are absent. | Inventory matches the solution; each claim cites real source or is marked proposed/unknown. |
| 6 — Instructions | Turn standards into scoped, discoverable guidance with meaningful enforcement. | Instruction library, scoped entry points, validation mappings, stronger validators, architecture checks, and exception template. | Four Instruction files and an existence checker exist. | Instructions resolve consistently; a deliberate prohibited dependency fails an independent check. |
| 7 — Skills | Make recurring development procedures reusable and test them on the current application. | Eight improved Skills, catalog/governance/template, stronger Skill validator, deterministic unit/API tests. | Eight Skill files exist; their procedures are generic and application tests are sparse. | Execute a Skill with explicit inputs and evidence; missing prerequisites block rather than trigger invented infrastructure. |
| 8 — Prompts | Define task-specific requirements, scope, assumptions, and acceptance evidence. | Corporate fleet feature Prompt and supporting prompt pack, a runnable-baseline verification Prompt, fixtures, prerequisite/evidence matrices. | Shared task Prompt categories are empty. | Baseline task can run; full fleet Prompt reports missing dependencies and remains blocked for implementation. |
| 9 — Roles | Define responsibility, authority, outputs, and handoffs. | Seven Role contracts, invocation Prompts, handoff schema, boundary cases, sample reports, human decision record. | Shared Role directory is empty; application Roles directory is a placeholder. | A manual/replayed handoff is traceable and an invalid handoff fails; no automated multi-agent runner is claimed. |
| 10 — Steering Notes | Give application work an owned, temporary mission and controlled update process. | Active-note template, proposal template, explicit entry-point links, context snapshot, lifecycle/conflict fixtures and checker. | Application steering is a placeholder; the shared note still describes Chapter 1 editorial work. | A valid note applies; expired/conflicting/missing-owner notes block the lab; proposals never modify the active note automatically. |

## Chapter 1 — The Evolution of Software Development

Lab directory: `examples/ch01-baseline/` (`L1/`). This chapter's fleet-quota exercise can be completed as design and validation planning; a complete quota implementation is not required yet.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/AlphaCarDetailing.sln`, `A/global.json`, `A/Directory.Build.props`, `A/Directory.Packages.props`, `A/.editorconfig` | Existing; verify | Solution, .NET 10 SDK policy, target framework, nullable/warning conventions, centrally managed packages, formatting. | Run from `A/`; record the selected SDK and actual command results. |
| `B/AlphaCarDetailing.Booking.Api/Program.cs` and `appsettings.json` | Existing; verify | Composition root, local SQLite configuration, development Swagger, health routes. | API starts; `/health/live` returns 200. Explain what the health check does and does not test. |
| `A/docs/business/business-overview.md`, `A/docs/domain-overview.md`, `A/docs/architecture/architecture-vision.md` | Existing; update as needed | Actual business domain and current implementation boundary, separate from proposed enterprise services. | A reader can distinguish implemented Booking from candidate contexts. |
| `L1/README.md`, `L1/expected-results.json` | New | Reproducible startup steps, future-date sample request, disposable database guidance, expected HTTP results. | Create returns 201 Pending; readback returns 200 with the same ID. Expected results are not represented as observed evidence. |
| `L1/domain-ownership.md`, `L1/task-request.md`, `L1/change-impact.mmd` | New | Chapter exercise: who owns quota terms, reservation state, authorization; bounded agent request; affected API/domain/persistence/tests/telemetry. | Identify missing account, quota, idempotency, and authorization prerequisites explicitly. |
| `L1/architecture-options.md`, `L1/test-matrix.md`, `L1/pr-evidence-checklist.md` | New | ADR-format comparison of modular application versus separately deployed Booking; quota dimensions; active/cancelled state; last-slot concurrency; retry and tenant checks. | The exercise's limit of five active reservations is scoped to its fixture. Every requirement maps to proposed evidence; unrun checks remain unrun. |

Do not create quota, Fleet, or Identity implementations simply to make the introductory diagram look complete. The scenario is a design exercise until its prerequisites and implementation scope are accepted.

## Chapter 2 — Understanding AI Coding Agents

Lab directory: `examples/ch02-agent-workflow/` (`L2/`). Use a small executable change before attempting the chapter's larger inspection workflow.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/scripts/build.ps1`, `A/scripts/test.ps1` | Existing; update | Resolve the application solution relative to the script, propagate nonzero native command exits, stop after failed restore/build, support ordinary documented invocation. | Run from the app and repository root. A failed native command produces a failed script result rather than a successful completion message. |
| `A/scripts/verify-environment.ps1` | New | Report SDK selection, required local tools, solution existence, and prerequisites. Avoid printing credentials. | Missing prerequisite has a clear diagnostic; running at root still checks the app's SDK policy. |
| `A/docs/ai-development/agent-workflow.md` | New | Inspect → plan → bounded edit → build/test → review. Define which decisions remain with a human and what constitutes completion evidence. | Distinguish an agent suggestion from an executed command or approval. |
| `L2/inspect-task.md`, `L2/implementation-task.md`, `L2/completion-record-template.md` | New | Read-only discovery request, explicit edit scope, and a record for files inspected/changed, commands, exit codes, results, assumptions, skipped checks, and review status. | Discovery stays read-only; implementation starts with a concrete agreed scope. |
| `UT/VehicleRegistrationNumberTests.cs` | New | Exercise current behavior: trim/uppercase normalization, blank rejection, more-than-20-character rejection, and valid boundary length. No new business rule is implied. | Tests pass against the actual value object and distinguish accepted from rejected input. |
| `L2/inspection-prerequisites.md` | New | Map the manuscript's approved/missing/rejected inspection and cross-tenant cases onto the current code. Identify absent inspection model, lifecycle transition, account policy, and identity boundary. | No claim that inspection gating was implemented. Mark its implementation as deferred until those decisions/components exist. |

Keep real per-run logs and reports in ignored `build/chapter-development/ch02/<run-id>/`. Track the template, synthetic inputs, and expected outcomes in the lab directory.

## Chapter 3 — Comparing AI Coding Platforms

Lab directory: `examples/ch03-platform-comparison/` (`L3/`). Claude Code is the primary live path; installing or paying for all platforms is not a completion prerequisite.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/CLAUDE.md`, `A/AGENTS.md`, `A/.github/copilot-instructions.md` | Existing; align | Thin platform entry points that reference the same application standards and baseline. Document the root relative to which each path is resolved. | A platform entry point does not introduce conflicting architecture, CQRS, security, or testing rules. |
| `L3/task-contract.md` | New | One bounded task, explicit start revision, permitted files/tools, acceptance criteria, validation commands, and output format. The registration-number test task from Chapter 2 can be replayed against its recorded pre-change revision. | Each evaluated platform receives equivalent inputs and permission scope. Do not rerun against a checkout where the requested patch already exists. |
| `L3/claude-task.md`, `L3/copilot-task.md`, `L3/codex-task.md` | New | Small invocation adapters referencing the common task rather than copied business rules. | Compare the adapters to the same contract version. |
| `L3/evaluation-rubric.md`, `L3/comparison.md`, `L3/observations-template.json` | New | Observe inspected context, diff scope, executed checks, corrections, evidence quality, human effort, environment and tool/date/version details where available. | Report measured observations only. Mark unavailable platform runs as not executed; avoid invented scores. |
| `L3/platform-prerequisites.md` | New | Accounts/tools/permissions required for optional live runs; distinguish a recorded/replayed demonstration from current live execution. | A reader can complete the primary workflow without being told that an untested alternative passed. |

The manuscript's larger service-agreement examples are not a reason to generate every assumed service. Preserve their contract-design lessons in the task template and record missing prerequisites.

## Chapter 4 — Enterprise AI Engineering

Lab directory: `examples/ch04-enterprise-engineering/` (`L4/`). Retain the existing Clean Architecture baseline.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/src/BuildingBlocks/AlphaCarDetailing.BuildingBlocks.Domain/` | Existing; verify/refine | Entity, AggregateRoot, DomainException, IDomainEvent. Review the unnecessary direct native SQLite package reference in this Domain-level project. | Shared domain primitives have no HTTP/persistence behavior; runtime still works after any approved dependency cleanup. |
| `B/AlphaCarDetailing.Booking.Domain/` | Existing; verify/refine | Booking aggregate, BookingId, BookingStatus, registration value object, local BookingCreatedDomainEvent. | Domain business invariants remain independent of HTTP and EF Core. Review its direct native SQLite package reference. |
| `B/AlphaCarDetailing.Booking.Application/` | Existing; verify/refine | BookingService, request/DTO, IBookingService, IBookingRepository, dependency registration. | Direct service coordinates the use case; Application does not depend on Infrastructure/API. Review its native SQLite package reference. |
| `B/AlphaCarDetailing.Booking.Infrastructure/` | Existing; verify | BookingDbContext, mapping, repository implementation, SQLite registration. | A local database write/read round trip succeeds. Persistence packages belong in projects that require them. |
| `B/AlphaCarDetailing.Booking.Api/` | Existing; verify | Endpoints delegate to the use case, composition root, Problem Details mapping, local startup. | POST/GET and expected error paths remain compatible. Anonymous baseline access is documented as a local limitation, not production security. |
| `A/docs/adr/0001-use-clean-architecture.md`, `0002-defer-cqrs.md` | Existing; retain | Accepted boundaries and the decision to defer CQRS. | New work follows the accepted decision unless a later approved task changes it. |
| `L4/capability-map.mmd`, `L4/asset-catalog.md`, `L4/ownership-and-approvals.md` | New | Owners for Instructions/Skills/Prompts, architecture/security/product/review decisions, and enforced versus advisory controls. Include at least three examples of each asset type as existing, draft, or planned. | Owners and decision boundaries are explicit; planned assets are not counted as implemented controls. |
| `L4/delivery-workflow.md`, `L4/quality-evidence-matrix.md`, `L4/measurement-plan.md`, `L4/maturity-assessment.md` | New | Requirement-to-review/deployment design; future concurrency/idempotency/tenant/rollback checks; baseline delivery/quality measurements and next improvements. | Missing outbox, identity, deployment, or telemetry prerequisites are visible. No invented maturity score or productivity result. |
| `.github/pull_request_template.md`, `.github/workflows/alpha-car-detailing-ci.yml` | New/adapt | Reviewable summary/evidence/risks. Move/adapt the nested app workflow into this repository's root workflows, with app working directory, correct toolchain, build/tests, and relevant validators. | Review workflow paths and run the checks in its actual environment before declaring CI green. Local command success alone is not CI evidence. |

## Chapter 5 — Repository Discovery

Lab directory: `examples/ch05-repository-discovery/` (`L5/`). Discovery reports must describe the current checkout rather than reconstructing an illustrative diagram as if it already existed.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/scripts/discovery/list-projects.ps1` | Existing; extend if useful | List projects and optionally their declared references; exclude generated/IDE directories. | Matches the solution's actual project set at this checkpoint. Account for any architecture-test project added later. |
| `A/docs/repository-discovery/repository-map.md` | Existing; update | Actual application, shared assets, source/test roots, scripts, entry points, and ownership. | Every current path exists; future paths are explicitly proposed. |
| `A/docs/repository-discovery/build-and-test-guide.md` | Existing; update | Correct working directory/SDK, restore/build/test/startup, structural checks, local database, and known environment limitations. | Commands run as documented; failures retain their real outcomes. |
| `A/docs/repository-discovery/risk-and-unknowns-register.md` | Existing; update | Capacity, account/identity ownership, pricing, event publication, CQRS deferral, dependency/CI limitations. | Each unresolved fact has a question, impact, owner or decision needed, and disposition. |
| `L5/discovery-checklist.md`, `L5/discovery-report-template.md` | New | Scope, files inspected, facts/assumptions, dependency and execution paths, risks, decisions, tests, and implementation readiness. | A reviewer can reproduce the important claims from cited files/revision. |
| `L5/booking-flow.mmd`, `L5/dependency-map.json` | New | Actual flow: POST endpoint → BookingService → Booking.Create → repository → SQLite; response is Pending; event is stored only in the aggregate's in-memory list. | Do not draw authorization, a capacity service, an outbox worker, or broker publication as implemented components. |
| `L5/expected-inventory.json` | New | Expected projects/capabilities at a named checkpoint, with implemented/proposed/unknown classifications. | A missing mandatory path or unexpected dependency is detected; absent future services remain explicit gaps. |

## Chapter 6 — Instructions

Lab directory: `examples/ch06-instructions/` (`L6/`). Define applicable rules before implementing more features, and distinguish writing a rule from enforcing it.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/instructions/architecture.md`, `coding-standards.md`, `testing.md`, `skills.md` | Existing; improve | Owner, version/status/scope, precise rules, precedence and conflict behavior, expected evidence. Preserve direct application services initially. | No contradictions across the four documents or platform entry points. Governed edits receive the required review. |
| `A/instructions/api.md`, `security.md`, `event-driven.md`, `infrastructure.md` | New, scoped | Error/API contracts; security requirements; event/outbox requirements when integration publication is introduced; deployment requirements when deployment is in scope. Document absent prerequisites and scope. | Do not imply that documenting authentication, an outbox, or infrastructure implements it. Applicable unmet mandatory rules block the relevant work. |
| `B/AGENTS.md`, `A/tests/AGENTS.md` | New | Booking/test-specific guidance that specializes shared rules. Reference common sources instead of copying them. Add a thin scoped Claude entry point only where the chosen live workflow needs it. | Scoped guidance cannot silently weaken permanent standards. Verify actual platform loading separately from lab parsing. |
| `A/instructions/validation-map.md` | New | Stable rule IDs → applicable scope → enforcing command/check → expected evidence → gap status. Include architecture, compiler, API/testing, and future controls. | An existence-only check is never presented as proof that generated code obeys the rule. |
| `A/scripts/instructions/validate-instructions.ps1` | Existing; strengthen | Check required metadata/references and report missing or conflicting declarations, with proper exit status. | Valid fixture succeeds; missing owner/reference fails clearly. Text scanning is labeled a structural check. |
| `A/tests/Architecture/AlphaCarDetailing.Architecture.Tests/` | New | An architecture-test project added to the solution for real project/package dependency rules, including Domain/Application boundary restrictions. | Known misplaced SQLite references are resolved before claiming compliance. A deliberately prohibited dependency fixture is rejected by the checker. Do not leave a broken production reference in the solution. |
| `L6/instruction-resolution.md`, `L6/conflict-cases.json`, `L6/exception-template.md` | New | Scope-resolution examples, contradictory/ambiguous inputs, and exceptions with rule, owner, reason, scope, approver, expiry, and compensating controls. | The local exercise flags conflicts and incomplete exceptions rather than choosing whichever text is convenient. Synthetic approvals remain labeled fixtures. |

Do not introduce messaging or deployment infrastructure solely to exercise a future rule. Record the rule's applicability and missing prerequisite, and keep the current development scope explicit.

## Chapter 7 — Skills

Lab directory: `examples/ch07-skills/` (`L7/`). The eight existing Skills are a starting library. Most currently share generic steps; make each procedure specific to the engineering task it teaches.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/skills/catalog.md`, `governance.md`, `skill-template.md` | Existing; improve | Applicability, owner/version/status, inputs, dependencies, preconditions, procedure, outputs, checks, failure behavior, approval boundaries, completion evidence. | Catalog names match Skill metadata and the actual directory set. |
| `A/skills/add-domain-behavior/SKILL.md`, `add-domain-event/SKILL.md` | Existing; improve | Place/verify invariants and value objects; distinguish local event recording from integration publication. | Test a domain rule and recorded event; do not claim delivery to a broker. |
| `A/skills/add-application-service/SKILL.md`, `create-api-endpoint/SKILL.md` | Existing; improve | Direct use-case orchestration; endpoint precondition checks, request/response mapping, errors, delegation, and applicable authorization requirements. | No hidden CQRS adoption; missing operation/security prerequisite is reported before changes. |
| `A/skills/add-ef-core-entity/SKILL.md` | Existing; improve | EF mapping, repository behavior, provider-backed round trip, explicit schema-change boundaries. | Local persistence check passes; no shared database change is implied by a generated migration. |
| `A/skills/write-unit-tests/SKILL.md`, `write-integration-tests/SKILL.md`, `validate-change/SKILL.md` | Existing; improve | Relevant behavioral coverage, isolated database/time, real command outcomes and evidence, blocked/failed outcomes. | The procedure cannot declare completion based on an agent's prediction or a heading check. |
| `A/scripts/skills/validate-skills.ps1` | Existing; strengthen | Required headings plus metadata, reference/catalog consistency, and specific diagnostics. | Valid Skill passes; missing required input/precondition/validation metadata fails. This checks structure, not procedural correctness. |
| `UT/BookingTests.cs` | Existing; extend | Meaningful missing cases: empty customer/station, blank service, equal-to-now schedule, normalization, and retained creation/event behavior. Reuse Chapter 2 registration tests. | Domain tests cover accepted and rejected current behavior using controlled time. |
| `IT/Fixtures/BookingApiFactory.cs`, `IT/Fixtures/FixedTimeProvider.cs` | New | Per-test disposable SQLite database and fixed TimeProvider, with cleanup. | Tests do not write the developer's normal application database or depend on today's wall-clock date. |
| `IT/BookingApiTests.cs`, `IT/BookingReadTests.cs`, `IT/BookingValidationTests.cs` | Update/new | Retain POST 201 Pending, assert Location/readback, 404 missing resource, and 400 invalid input/schedule without persistence. | Run against real SQLite, repeat successfully, and verify database isolation. |
| `L7/skill-selection-cases.json`, `L7/skill-execution-record-template.md` | New | Task → chosen Skill/version/inputs → preconditions → changed files → checks/outcomes. Include an outbox task whose prerequisite is absent. | Runnable baseline exercise succeeds; unsupported integration task is blocked, not silently approximated. |

The chapter's enterprise sample names commands, CQRS, authorization, telemetry, and an outbox. Those are not the current baseline. Use the existing application-service Skill and record missing prerequisites. Add advanced Skills when their implementation milestone arrives, rather than relabeling the eight baseline Skills as full enterprise coverage.

## Chapter 8 — Prompts

Lab directory: `examples/ch08-prompts/` (`L8/`). The primary deliverable is a usable task contract and prompt set. A Prompt requesting a feature does not make absent dependencies available.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `prompts/tasks/booking/ACD-BOOK-042-add-corporate-fleet-booking.md` | New | Chapter feature: one account, 1–50 existing active fleet vehicles, one station/window/package, purchase-order reference, identity-derived account, server-side pricing, atomic outbox, compatibility and evidence requirements. Retain stable acceptance IDs. | All chapter acceptance criteria map to expected evidence; missing ownership/security/pricing/outbox prerequisites are explicit. |
| `prompts/tasks/booking/ACD-BOOK-042/` | New | Supporting task Prompts for API, domain behavior, integration event, EF persistence, authorization, observability, tests, architecture review, build repair, and PR preparation. Reference common Instructions/Skills instead of copying them. | Each has a bounded objective, inputs, preconditions, output, checks, and stop conditions. None grants authority to change governed assets or invent missing infrastructure. |
| `prompts/tasks/booking/ACD-BASE-001-verify-booking-contract.md` | New | A currently executable task: verify/create/read/rejection behavior of the single-booking baseline, inspect relevant code/tests, and report real outcomes. No fleet feature completion claim. | A live workflow can complete this task with existing prerequisites and a recorded revision. |
| `L8/prompt-catalog.md`, `L8/prerequisite-matrix.md` | New | Prompt/version/owner/scope and implemented/missing prerequisite inventory for the enterprise feature and runnable baseline task. | Clearly separate ready-to-run tasks from blocked implementation requests. |
| `L8/fixtures/valid-task.json`, `L8/fixtures/ambiguous-task.json`, `L8/fixtures/missing-prerequisite.json` | New | Synthetic requirements and defects: absent purchase-order requirement, unresolved ownership, missing authorization/outbox capability. Include 0/1/50/51 vehicle boundary expectations where applicable. | Incomplete input yields a specific question/blocker rather than fabricated assumptions. |
| `L8/acceptance-evidence-matrix.md`, `L8/review-checklist.md` | New | Acceptance ID → intended component → required test/review → evidence location → actual status. Review scope, architecture ownership, compatibility, PII exposure, and approval boundaries. | Proposed tests remain proposed; unavailable suites remain incomplete. Do not mark 1–50 fleet behavior verified from single-booking tests. |
| `L8/verify-prompt-fixtures.ps1` | New | A small deterministic lab checker for the declared fixture fields and expected classifications; return nonzero for an unexpected result. | Positive/negative fixtures work. Label its limits: it cannot judge every business ambiguity or guarantee that a model follows the Prompt. |

The 1–50 count is the Chapter 8 batch-size rule, not the Chapter 1 contract quota of five active reservations and not a universal fleet limit.

## Chapter 9 — Roles

Lab directory: `examples/ch09-roles/` (`L9/`). Define all seven Roles, then demonstrate the necessary subset through manual sessions or fixture replay. Automated orchestration and actual tool-permission enforcement arrive with the Harness implementation.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `harness/roles/lead-agent.md` | New | Interpret task, plan, sequence work, track risks/handoffs/blockers; no authority to redefine requirements, approve exceptions, merge, or deploy. | Incomplete evidence blocks the example handoff. |
| `harness/roles/architect-agent.md` | New | Assess boundaries/dependencies/contracts and propose architecture decisions; record decisions needing human authority. | Does not claim an AI recommendation is an approved ADR. |
| `harness/roles/developer-agent.md` | New | Implement within the agreed files/scope, use Skills, add tests, report evidence. | Cannot approve its own work or change permanent Instructions to make the task easier. |
| `harness/roles/reviewer-agent.md`, `security-reviewer.md` | New | Inspect an identified candidate; issue stable findings with severity/evidence/disposition. Security review addresses applicable trust/authorization/data issues. | Reports refer to actual code/fixtures; review does not silently repair and approve its own finding. |
| `harness/roles/validator-agent.md`, `evaluator-agent.md` | New | Validator obtains/checks executed evidence; Evaluator assesses requested business outcomes using a rubric. | Three passing baseline tests do not count as full fleet acceptance. Missing checks stay incomplete. |
| `harness/roles/catalog.md`, `authority-matrix.md` | New | Owner/version/inputs/outputs, advisory versus blocking responsibilities, permitted actions, escalation and human-only decisions. | Each output has a responsible Role; no AI Role approves consequential human decisions. |
| `prompts/roles/lead.md`, `architect.md`, `developer.md`, `reviewer.md`, `security-reviewer.md`, `validator.md`, `evaluator.md` | New | Thin invocation Prompts referencing the Role contract, task, repository revision, and expected output. | Role contract defines authority; invocation Prompt defines this run's task. |
| `harness/contracts/role-handoff.schema.json` | New | Task/run IDs, source/target Role, repository revision, input/context versions, artifact references, checks, unresolved findings, status. Approval fields reference actual records rather than accepting an agent's assertion as proof. | Required fields/enums are checked; separate semantic checks validate authority and candidate identity. |
| `L9/handoff.json`, `L9/fixtures/invalid-handoff.json`, `L9/verify-handoffs.ps1` | New | Valid handoff plus wrong-revision, missing-evidence, and unauthorized-action cases. | Expected valid case passes; invalid cases are rejected. Structured-message validation alone does not enforce read-only tools or process isolation. |
| `L9/sample-reports/`, `L9/human-decision-template.md` | New | Clearly labeled sample plan/developer/review/security/validation/evaluation reports and a separate human decision record. | Live reports cite observed evidence; examples are never presented as executed runs. |

Use `ACD-BASE-001` for an immediately executable demonstration. Keep the chapter's broader fleet-registration/driver/recurrence task as a separately scoped future fixture. It must not silently expand Chapter 8's one-time batch-booking contract.

## Chapter 10 — Steering Notes

Lab directory: `examples/ch10-steering-notes/` (`L10/`). The chapter's July 2026 mission is historical example data, not a current approval. Use fresh human-selected metadata for an active mission; retain fixed historical dates only in labeled lifecycle fixtures.

| Artifact | Status | Expected content or behavior | Verification |
| --- | --- | --- | --- |
| `A/steering/README.md` | Existing; update | Explain the canonical application mission location, owner, scope, lifecycle and discovery links. Map the manuscript's illustrative `Search/` location to this actual path. | No competing active mission is accidentally loaded from the handbook's editorial `harness/search/`. |
| `A/steering/steering-note.md` | New | Mission ID/version, Draft/Approved/Expired/Superseded status as applicable, named human owner, effective/review/expiry times, scope, non-goals, dependencies/risks, required outputs, stop conditions, retirement and verifiable approval reference. | No invented owner, approval, deadline or SLO. Draft/missing/expired approval blocks work requiring an approved mission. |
| `A/steering/proposed-steering-update-template.md` | New | Observation, evidence, current note/version, requested change, impact/risk and pending human decision. Create an actual proposal only when there is a real recommendation. | A proposal does not edit or supersede the active note automatically. |
| `A/AGENTS.md`, `A/CLAUDE.md`, `A/.github/copilot-instructions.md` | Existing; planned update | Explicitly point to the same application Steering Note and describe applicability, precedence, stale/conflicting context and human update ownership. | Verify the intended live workflow actually reads the file. Directory naming alone does not provide automatic loading. |
| `L10/scope-cases.json`, `L10/steering-context-template.json` | New | Cases for in-scope/out-of-scope work, a request to bypass permanent rules, valid/expired/missing-owner notes, and a changed version. Context captures note ID/version/hash, applicable scope and time of use. | Each case has a stated expected result and rationale. Pin context consistently across the manual Role handoff. |
| `L10/verify-steering-context.ps1` | New | Small lab checker for required metadata, scope declarations, explicit clock/expiry cases, version agreement, and the modeled conflict rules. | Valid fixture passes; expired/unauthorized scope fixtures fail. Do not describe this simple checker as a general natural-language policy engine. |
| `L10/README.md`, `L10/approval-checklist.md` | New | Walkthrough of scope selection, conflict escalation, proposal review, supersession, and retention. Include how to archive a completed mission. | Active-note checksum stays unchanged during proposal generation; only an attributable authorized update makes it effective. |

Do not silently replace `harness/search/steering-note.md` with application release data. It currently describes the handbook's editorial mission. Give the application its own scope and explicit context link.

## Cross-chapter alignment issues to resolve while reading

| Chapter material | Current repository / agreed baseline | Development treatment |
| --- | --- | --- |
| Chapter 1 uses five active reservations per fleet/station/local date. | No durable quota/idempotency implementation exists. | Preserve as a scoped requirement/design fixture. Do not infer that a passing creation test verifies it. |
| Chapter 2 inspects an inspection-gated government-fleet lifecycle. | No inspection aggregate, approval flow, tenant authorization or detailing transition exists. | Produce a discovery/prerequisite report; use a bounded existing behavior for the first live code change. |
| Chapters 3–6 sometimes show multiple services, workers, infrastructure, contracts and gates. | Current runnable app contains Booking only. | Record absent capabilities; do not invent paths, test outcomes or infrastructure. |
| Chapter 7's enterprise example assumes CQRS and advanced platform Skills. | ADR 0002 and application Instructions explicitly defer CQRS; eight baseline Skills exist. | Use direct application services now. Document the adaptation and implement advanced prerequisites later. |
| Chapter 8 scopes one-time batch booking for 1–50 existing fleet vehicles and uses `CorporateFleetBookingCreated`. | No corporate identity, pricing abstraction or outbox exists. | Keep the full task contract, classify blockers, and demonstrate Prompt execution using a baseline-compatible task. |
| Chapter 9 illustrates fleet creation, drivers and recurring schedules with other event names. | That scope differs from Chapter 8 and is not implemented. | Define Role contracts once; keep separate task IDs/contracts/fixtures. Do not combine scenario assumptions without a decision. |
| Chapter 10 shows `Search/steering-note.md` and expired July example dates. | `A/steering/` already exists; the shared root note is stale editorial context. | Propose `A/steering/steering-note.md` as the application path, link it explicitly, and use real metadata only when approved. |

These are implementation mappings and known alignment issues. This plan does not rewrite the manuscript or change accepted Instructions/Skills.

## What should exist after Chapter 10

| Deliverable | Expected state |
| --- | --- |
| Booking application | Existing .NET 10 service retained, current behavior protected, dependency placement reviewed, tests isolated and expanded. |
| Repeatable development checks | Correct working directories, native failure propagation, local build/test evidence, meaningful structural/architecture checks. Root CI is configured and its actual execution status is recorded. |
| Repository Intelligence | Discoverable application docs, current discovery evidence, scoped Instructions, governed Skills, task Prompts, Role contracts, and an owned application mission. |
| Reader labs | Ten documented exercises with tracked synthetic inputs/templates/expected results and clear revision/prerequisite information. Actual run evidence is separately identified. |
| Harness readiness | Inputs and contracts are ready for later orchestration. Automatic planning → coding → review → validation → evaluation is not yet implemented merely because these files exist. |

Use a verified baseline checkpoint after Chapters 1–7. Close Chapter 10 only when its own task-context checks pass; Chapter 11 still adds governed Knowledge Sources. Build the first executable shared Harness in Chapter 12 and refine its architecture and Role implementations in the following chapters.

## Completion record for every chapter

Each lab README should name the chapter, learning objective, start revision, required tools/account/environment, actual artifact paths, commands, expected results, common failures, limitations, and the verified revision when available. For each development session record changed files, checks run and exit codes, unexecuted/failed checks, decisions and outstanding blockers.

Track reusable source, fixtures, templates and expected outputs in Git. Put disposable databases, logs and actual run outputs under ignored `build/chapter-development/chXX/<run-id>/`; retain any release-quality evidence through the project's chosen durable evidence mechanism rather than relying on ignored local files alone. Do not create Git tags, commits or approval records simply to make the checklist appear complete.
