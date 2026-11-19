# Enterprise AI Engineering Handbook

## Chapter-to-code implementation plan for Chapters 1–45

Continue in the existing handbook repository at `C:\AIHandbook`. Use `alpha-car-detailing/` as the running .NET 10 application, `harness/` for the shared workflow, and `examples/` for the reader-facing labs. The existing code guide identifies Chapters 1–7 as the baseline. Verify those seven mappings before moving into Chapter 8, **Prompts**, as the first implementation story.

The proposed checkpoints and outputs below are release targets. A chapter can teach through application code, a reusable engineering artifact, a test, or analysis of a recorded run. Each chapter still needs a reader action and an observable result. Keep Chapter 20.2, **Scratchpad and Run Workspace**, as a companion to Chapter 20. The implementation scope here ends with the Chapter 45 capstone.

## Start with the existing Booking Service

From `alpha-car-detailing/`, use the SDK policy in `global.json`. The solution already contains a Booking domain, direct application services, EF Core with SQLite, a minimal API, and domain and integration tests. Preserve those choices while building the core Harness. Introduce new application behavior when a lab needs it, and use versioned fixtures for later services and event contracts that the baseline does not yet implement.

```powershell
dotnet --version
dotnet restore AlphaCarDetailing.sln
dotnet build AlphaCarDetailing.sln --no-restore
dotnet test AlphaCarDetailing.sln --no-build
dotnet run --project src/Services/Booking/AlphaCarDetailing.Booking.Api
```

Use the local address printed by the API. Check `/health/live` and `/health/ready` for a `200` response. The sample maps generic health checks to both routes. Create a synthetic booking with `POST /api/bookings` and expect `201`. Use its returned ID with `GET /api/bookings/{bookingId}` and expect `200` with the matching ID. Generate a future UTC appointment because the date hard-coded in `README-code.md` has passed. Use a disposable local database for repeatable runs. Record the SDK, Git revision, exit codes, and what passed.

Run the existing companion checks from the same directory with PowerShell.

```powershell
./scripts/discovery/list-projects.ps1
./scripts/instructions/validate-instructions.ps1
./scripts/skills/validate-skills.ps1
```

During planning, a clean copy reported seven projects and passed validation for four Instruction documents and eight Skills. The SDK selected in that copy was `10.0.401` under the repository’s SDK policy. Package restore stopped at `NU1301` because the current environment could not resolve `api.nuget.org`. Build, tests, and the HTTP smoke test remain open in M0. Repeat them in an environment with access to the configured package source before marking the Chapter 7 checkpoint verified.

## Repository and checkpoint design

| Location | Use |
| --- | --- |
| `book/` | Add a short **Code companion** block to each chapter’s hands-on section. Link to its lab at a verified checkpoint. |
| `alpha-car-detailing/` | Maintain the sample application, tests, ADRs, and application-specific agent guidance. Extend the Booking Service through its existing entry points. |
| `examples/chXX-<slug>/` | Proposed location for a lab README, small synthetic inputs, and a verifier or expected-result file. Reference shared code and fixtures in place. |
| `harness/` | Put the local runner, shared run artifacts, policy checks, learning components, and their tests here. Keep the first runner compatible with the roadmap’s PowerShell baseline. Use C# for shared logic that benefits from typed contracts and tests. |
| `prompts/`, `skills/`, `harness/roles/` | Own shared Prompt, Skill, and Role assets. Document whether a sample-specific artifact lives under `alpha-car-detailing/` and which file owns the rule if a platform uses a thin entry point. |
| `.github/workflows/` | Run handbook-level checks from the repository root. Use `alpha-car-detailing/` as the working directory for app checks. Review the existing app-local workflow as the starting point. |

Use one cumulative codebase. For each published lab, record a **start revision** and a **verified revision**. Give readers named Git tags at meaningful checkpoints, with exact commits for individual chapters as needed. Suggested tags are `companion/ch07`, `companion/ch11`, `companion/ch21`, `companion/ch28`, `companion/ch35`, and `companion/ch45`. Use an existing historical commit only after checking its lab. If the early code arrived in a single baseline, let Chapters 1–7 share that checkpoint and document a different inspection or exercise for each.

Initially one local runner can read task fixtures, replay example agent outputs, invoke known build/test checks, and save a manifest. Add a documented Claude Code workflow as the primary live path. Give GitHub Copilot and OpenAI Codex the same task, artifacts, and acceptance criteria where their workflow materially differs. Keep the required reader path executable with the local SDK and checked-in inputs. Clearly label any live lab with its account, tool, and permission prerequisites. Initial package restore may require dependency access.

## Chapter-by-chapter alignment

Treat each row as a proposed implementation issue. Read the chapter’s learning objectives and hands-on example before coding, then confirm names, paths, and outcomes against that example. **Existing baseline** denotes an artifact reported in the code guide and due for verification. The remaining rows are planned work.

### Chapters 1–7 — Verify the existing baseline

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 1 · The Evolution of Software Development | Existing solution bootstrap, business overview, architecture vision, and health endpoint. | Run Booking and identify which work the build and health checks automate. |
| 2 · Understanding AI Coding Agents | Existing build/test scripts, agent workflow, and completion-evidence template. | Run the checks and fill in a completion record with command results and the human review step. |
| 3 · Comparing AI Coding Platforms | Existing `CLAUDE.md`, `AGENTS.md`, Copilot guidance, and shared Instructions. | Give each workflow the same task fixture and compare its plan or saved transcript against the same acceptance criteria. |
| 4 · Enterprise AI Engineering | Existing Clean Architecture projects, Booking bounded context, and ADRs. | Trace one dependency from API to Application, then explain the ADR decision that defines the baseline. |
| 5 · Repository Discovery | Existing repository map, build guide, risk register, and discovery scripts. | Regenerate or verify the inventory against the solution and locate the test entry point. |
| 6 · Instructions | Existing root guidance, `instructions/` library, and validation script. | Resolve the Instructions for a Booking change and verify an applicable convention. |
| 7 · Skills | Existing Skill template, governed library, and Booking vertical slice. | Follow the domain-behavior Skill and verify Booking create/retrieve with the domain and integration tests. |

### Chapters 8–11 — Give the agents a governed task context

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 8 · Prompts | Add the `ACD-BOOK-042` corporate fleet-booking task Prompt and linked implementation, review, and validation Prompts. Map its acceptance criteria to evidence. | Trace one account, station, booking window, package, and purchase order, with 1–50 registered vehicles, from task input to expected checks. Catch a missing input or evidence link. |
| 9 · Roles | Define Role responsibilities, authority, input/output, and handoff records for a staged booking workflow. | Pass a Lead plan to the Developer and flag an attempt by a Role to take a decision outside its authority. |
| 10 · Steering Notes | Reuse the quarterly fleet-booking Steering Note under `harness/search/` and annotate its mission, release priorities, scope, and lifetime. | Resolve which work belongs in the current increment and identify a conflict that requires escalation. |
| 11 · Knowledge Sources | Create the `ACD-FLEET-284` source manifest for recurring fleet bookings. Record authority, owner, freshness, applicability, and citations. | Cite the approved source for a business rule and flag stale or conflicting evidence. |

### Chapters 12–21 — Execute and measure the Harness

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 12 · What Is a Harness? | Run a minimal PowerShell Harness against a fleet registration and recurring scheduling fixture. Save the plan, checks, decision checkpoint, and PR summary. | A passing run produces an evidence bundle. A failed check stops or follows the documented bounded retry. |
| 13 · Harness Architecture | Formalize the stages and artifact contracts for `ACD-FLEET-BOOKING-001`, including independent validation and evaluation. | Trace the same run ID across stages and show that a Developer result cannot stand in for a required gate result. |
| 14 · Lead Agent | Produce a discovery-backed plan naming scope, relevant Skills, affected files, risk, verification, and handoffs. | The plan cites source evidence and can be rejected or escalated when required context is unresolved. |
| 15 · Developer Agent | Deliver the chapter’s `POST /api/fleet-bookings` increment. Cover authorization, vehicle and station rules, persistence, and applicable `FleetBookingCreated` evidence. | The focused diff and tests show accepted and denied requests. A final implementation record lists exactly what ran. |
| 16 · Reviewer Agent | Review the pinned `ACD-FLEET-142` corporate-booking diff using the chapter’s API and event contract. | Submit a finding with location, severity, evidence, and disposition. Trace a verified fix to its follow-up diff. |
| 17 · Validator Agent | Run the applicable build, contract, transactional consistency, OpenAPI, and configuration checks on the reviewed revision. | A failed check, missing required result, or revision mismatch blocks the delivery step. |
| 18 · Evaluator Agent | Apply the business rubric for multi-vehicle bookings to the validated result. Include fleet membership, active status, authorization, maximum size, event outcome, and single-vehicle compatibility. | Compare the business evaluation with the Validator Agent report and explain each unmet criterion using evidence. |
| 19 · Population | Split `GOAL-1042`, inspection before `ReadyForDetailing`, into dependent work items with atomic claim and bounded retries. | Run two claim attempts and observe a single claim. Trace a blocked dependent item and an exhausted retry. |
| 20 · Metrics | Aggregate Population, run, and gate events into a report covering success, latency, rework, reliability, and cost. | Recalculate a published rate or percentile from the seed records and identify its denominator. |
| 20.2 · Scratchpad and Run Workspace | Pause and resume a detailing-location or compatible `FleetBookingCreated` example using saved plan, handoffs, and evidence. | Resume from a verified checkpoint. Detect a changed revision or input that invalidates saved state. |
| 21 · Enterprise Harness Patterns | Use shared orchestration with Booking, Fleet, and Billing repository profiles, including worker isolation and repository-specific checks. | Replay two profiles and see which checks and permissions each profile selects. |

Chapters 15–17 use different endpoint and event names. Maintain an explicit fixture or contract mapping for each example. Choose one canonical runnable booking increment and update its book references if the interface is intentionally standardized. For a chapter that teaches architecture beyond the baseline, list the executable fixture or prerequisite that supplies it.

### Chapters 22–28 — Apply the workflow with Claude Code

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 22 · CLAUDE.md | Exercise root and Booking-specific Claude guidance against shared Instructions and gate mappings. | Identify the applicable guidance and flag a scope conflict. Verify tool behavior in the live walkthrough. |
| 23 · Claude Skills | Exercise `add-application-command` with the fleet cancellation scenario using a scoped fixture or its documented application prerequisite. | Verify authorization, the recorded cancellation reason, a single cancellation event, and the listed tests. |
| 24 · Claude Commands | Provide thin commands for discovery, planning, implementation, review, validation, evaluation, test, repair, and goals. Reuse the cancellation case. | Trace a command call to the shared procedure and observe its expected artifact and exit status. |
| 25 · Claude Memory | Replay stale synchronous HTTP advice against current repository evidence for event and outbox behavior. | Identify the contradiction, cite the current source, and save a correction proposal. |
| 26 · Tool Calling | Add a repository-scoped adapter for named build and test operations. Use the duplicate-booking retry scenario and record tool outcomes. | Observe permitted checks, rejection of work outside the configured scope, and correlated audit records. |
| 27 · Planning Mode | Produce a fleet-booking plan with scope, risks, assumptions, tasks, and validation criteria in a read-only session. | Compare the plan with the rubric and confirm that source files are unchanged. |
| 28 · Enterprise Workflows | Run `ACD-FLEET-028`, the monthly booking allowance, through planning, change, review, gates, evaluation, and a reviewable PR artifact. | Resume from saved state. Trace authorization, idempotency, concurrency, event publication, and regression checks to the accepted result. |

### Chapters 29–35 — Govern and assess delivery

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 29 · AI Governance | Model delegation for corporate bookings, including organization scope, expiry, revocation, and accountable owners. | Resolve the authorizing Role and show the outcome for authorized and revoked fixtures. |
| 30 · Security | Run the failed or incomplete fleet-bookings report scenario with synthetic data and scoped access. Cover protected files and sensitive-data handling through defensive checks. | An unauthorized request is denied, required checks run independently, and evidence records the outcome without sensitive contents. |
| 31 · Human Approval | Pause a recurring fleet release at approval for its shared event, corporate data, or migration impact. Bind the decision to the revision and scope. | The prescribed human decision can proceed through the release gate. Missing, expired, revoked, or revision-mismatched approvals block it. |
| 32 · Audit Trails | Record `ACD-284` booking limits across API and import paths with task, run, authorization, outcome, diff, check, and decision evidence. | Reconstruct a single timeline and check that duplicate ingestion preserves the original event identity. |
| 33 · Policies | Evaluate the fleet analytics provider rule before a tool operation. Give the lab one restricted input and one approved, data-minimized input. | The policy decision explains the denial or approval and records the rule version. |
| 34 · Compliance | Map the multi-location, account-scoped fleet-booking requirements to controls, gates, evidence, owners, and approval. | A manifest identifies which evidence supports each control and which missing item prevents release. |
| 35 · Productivity Metrics | Compare accepted delivery for the configurable active-booking limit using common scope, quality criteria, and Harness events. | Compute time to accepted result, human rework, and cost per accepted outcome with traceable denominators. |

### Chapters 36–45 — Learn from the Harness

| Chapter | Companion work | What the reader verifies |
| --- | --- | --- |
| 36 · Static vs Adaptive Harnesses | Replay the same corporate fleet booking-limit records through a static runner and an adaptive recommendation path. | Both preserve the existing checks. The adaptive run records a proposal and the evidence behind it. |
| 37 · Harness Memory | Store a validated remediation for duplicate bookings during retries, with provenance, scope, and an approval record. | Retrieve the approved, relevant entry and exclude stale, unapproved, or out-of-scope entries. |
| 38 · Learning from Builds | Normalize build and retry records for the recurring fleet booking example. Group compilation and dependency failures by verified signature. | Distinguish repeated engineering failures from an infrastructure retry and trace a candidate remediation to its build evidence. |
| 39 · Learning from Reviews | Normalize confirmed Booking review findings with evidence and dispositions, including a Clean Architecture dependency finding. | Count verified recurrence across independent changes and link a recommendation to resolved findings. |
| 40 · Learning from Pull Requests | Replay a versioned set of fleet endpoint PRs with diffs, review cycles, gate results, and dispositions. | Reconstruct a PR’s timeline and report recurrence with an explicit eligible-change denominator. |
| 41 · Skill Evolution | Draft a versioned change to the Transactional Outbox Skill for `FleetBookingCreated`. Replay prior misses and passing controls. | See the candidate’s validation results and the decision required to adopt it. |
| 42 · Prompt Evolution | Compare the existing feature-development Prompt with a candidate under the same tasks and rubric. | Attribute changes in task fidelity and evidence completeness to the candidate comparison, with held-out results visible. |
| 43 · Instruction Evolution | Propose a scoped Instruction refinement for direct database access across service boundaries. Check authority and precedence. | Detect a conflicting or out-of-scope change and produce a proposal for the responsible owner. |
| 44 · Knowledge Graph | Trace `BookingConfirmed` through versioned consumers, owners, and checks. Verify graph edges against subscription and contract artifacts. | Identify the affected consumers for the optional `FleetAccountReference` field and report a stale or unverified relationship with its source. |
| 45 · Autonomous Improvement Engine | Implement the `AIE-045` capstone for an event-change Skill revision, with replay, exact-revision approval, activation, and recovery. | Reproduce the two omissions over three comparable changes and trace a restored revision back to the original evidence. |

## Author build order and release gates

| Milestone | Work in order | Ready when |
| --- | --- | --- |
| M0 · Baseline, Chapters 1–7 | Verify the current solution, document setup from a clean clone, reconcile the seven existing code mappings, and record the first working revision. | A fresh checkout builds, tests, and completes the Booking smoke test with the documented commands. |
| M1 · Context, Chapters 8–11 | Add the first governed Prompt, then Role assets, a Steering Note, and approved knowledge sources. Give a single fleet booking task versioned inputs for all four labs. | A reader can assemble the task context and trace the source and version of each input. |
| M2 · Harness, Chapters 12–21 | Build the local PowerShell workflow and run manifest, then implement the Role handoffs, checks, outcome rubric, and metrics. Start with captured outputs before adding live provider workflows. | One scenario completes end to end. A failing gate produces actionable evidence and blocks progression. |
| M3 · Platform workflows, Chapters 22–28 | Apply the shared workflow in Claude Code, exercise equivalent paths where Copilot or Codex differ, and record a full fleet booking delivery run. | A reader can reproduce the shared acceptance checks using the replay or run the documented live workflow with the listed prerequisites. |
| M4 · Governance, Chapters 29–35 | Teach the ownership model, restrictions, human approval, audit, Policy, compliance evidence, and measurement. Exercise these controls on the same delivery run. | A missing permission, failed gate, or stale approval blocks the applicable action. An audit record explains the decision. |
| M5 · Learning, Chapters 36–45 | Add memory and evidence ingestion, then artifact evolution and Knowledge Graph queries. Finish with the AIE-045 replay and recovery exercise. | A clean run reproduces the capstone and links the final effective revision to the evidence, validation, decision, release, and rollback. |

Implement governance controls when the runner first needs them, then expose their configuration and failure modes in Chapters 29–35. In particular, a local replay can generate a reviewable PR artifact at Chapter 28. A live merge or activation needs enforceable checks and authorization from the first time that operation exists.

At Chapter 12, start a versioned run manifest with the task and run IDs, input and repository revisions, stage and result, and references to generated artifacts. Add check applicability and outcome at Chapter 17, then owner and decision details when governance is exercised. The later learning chapters should read those same records. Store synthetic source records with the lab and keep generated output in a disposable run directory. This gives the reader evidence to inspect at each stage and gives Chapter 45 a consistent history to replay.

### First five working sessions

1. **Verify Chapters 1–7.** Run the baseline in a clean checkout, fix any setup issue in its own change, and record the verified commit and SDK. Check that the documentation matches the actual scripts and routes.
2. **Define Chapter 8’s task and expected outputs.** Use `ACD-BOOK-042` from the manuscript. Make a small fixture, choose one authoritative location for the Prompt, and link to the existing Instructions and Skill.
3. **Implement and verify the Chapter 8 lab.** Add the Prompt, a sample response or expected output, and a simple verifier. Check a complete output and a deliberately incomplete one. Record any live Claude Code steps as a separate recipe within the lab.
4. **Try the lab as a reader.** Start at the baseline revision, run the lab using only its README, and check the expected result. Add the exact command, example output, and exercise to the manuscript at the right checkpoint.
5. **Carry the artifact into Chapters 9–11.** Feed the Chapter 8 task into a Role handoff. Add the Steering Note and approved knowledge evidence on the same scenario so Chapter 12 has a working input bundle for orchestration.

Split bigger chapters into a core lab and an extension. Open one implementation issue per chapter and group small dependent chapters into one release only after each has its own verifier and manuscript link.

### First implementation issue — Chapter 8 Prompts

Start from the verified Chapter 7 revision. Place the shared Prompt under the existing `prompts/tasks/` library, for example `prompts/tasks/booking/ACD-BOOK-042-add-corporate-fleet-booking.md`. The manuscript example uses `prompts/booking/`. Resolve the path in the first implementation issue and point the book and lab to the same file.

| Deliverable | Acceptance check |
| --- | --- |
| Task Prompt and versioned input fixture | The task states the booking scope, required inputs, allowed changes, relevant Chapter 6 Instructions and Chapter 7 Skills, expected outputs, and escalation conditions. |
| Implementation, review, and validation Prompts | Each uses the same task and acceptance criteria. Each produces the handoff or evidence its recipient needs. |
| Acceptance-evidence matrix and verifier | A complete captured run can be checked locally. A fixture missing a required detail, such as the corporate account reference or a boundary check for 51 vehicles, gets a specific diagnostic. |
| Reader lab | The README names the start revision, exact local command, expected pass and failure outputs, and where to find the captured artifacts. The exercise changes one task constraint and asks the reader to update the affected checks. |

The Chapter 8 lab can replay the approved task context and sample responses while later chapters implement the fleet behavior. Mark account permissions, pricing rules, event publication, and other features outside the current Booking baseline as fixture inputs or future implementation prerequisites.

## Reader workflow for every chapter

1. **Start** at the commit or tag printed in the chapter. Check the lab’s SDK, shell, and any prerequisites. Readers who skip chapters can use the prepared start state.
2. **Inspect** the files named in the lab and follow the connection from the learning objective to the code, configuration, evidence, or decision record.
3. **Run** the exact command in the listed working directory. The lab explains any setup, generated file locations, and repeat-run cleanup.
4. **Verify** the result using the listed test, output field, golden file, exit status, or rubric. Compare the result with the example in the book.
5. **Extend** the exercise by changing one input or assumption. Use a focused check or answer rubric to assess the result, then carry the output into the next chapter if listed as a dependency.

Use this block in the lab README and link to it from the chapter. Keep commands and expected results in the lab as the maintained copy.

```markdown
## Code companion

- Learning objective
- Start checkpoint and verified checkpoint
- Prerequisites and working directory
- Files to inspect or change
- Exact setup and run commands
- Expected result and verification command or rubric
- Exercise, expected output, and reset instructions
- Artifact used by the next chapter, where applicable
```

An author can mark a lab ready when its chapter reference resolves, its setup works from the published start state, its expected behavior and relevant failure case are verifiable, and a second run or reset gives a predictable result. Run the deterministic labs in CI on the platforms the book claims to support. Keep live platform walkthroughs in an explicit review pass and record which tool version or environment was used.

## Chapter 45 capstone acceptance

Use the manuscript’s `AIE-045` evidence bundle as the integration target for preceding chapters. Give the author and reader the following checkpoints in the capstone lab.

1. **Normalize and count evidence.** Preserve source references and link PR 458’s interrupted attempt to its retry. PRs 451 and 458 account for two confirmed downstream omissions across three comparable changes, including the clean PR 446. Preserve the interrupted attempt’s unknown validation outcome.
2. **Verify dependencies and choose the owning artifact.** Confirm the Notification and Fleet Service consumers against subscription declarations and contract fixtures. Use the evidence to propose a scoped change to the event-change Skill. Record alternatives and confidence separately from impact.
3. **Replay the exact candidate.** Pair baseline and candidate on the two historical omissions, a clean event change, an unrelated change, an incompatible payload, and a missing graph edge. Require coverage on applicable cases, no redundant checks on the controls, a block on incompatibility, and a verified check or escalation for the missing edge. Include the version-pinned unaffected consumer as an exercise or additional regression case. Report timing separately with a recorded budget and case count.
4. **Exercise the decision boundary.** Bind evidence, validation, and the recorded human decision to the proposal, candidate revision, and base revision. Simulate denied activation with missing or stale approval and with a failed gate. Activate the candidate in local lab state using a clearly labeled fixture decision.
5. **Exercise rollback.** Replay the manuscript’s two false blocks within the first ten eligible changes. Restore the previous approved compatible version, confirm the effective revision on a fresh run, and show that required gates still execute. Reconstruct the audit trail from the final run back through rollback, activation, decision, replay, and original PR evidence.

Label the capstone’s PRs, decisions, and timings as synthetic or recorded lab inputs where applicable. Exercises can share the same evidence package so readers can verify correlation, dependency claims, candidate choice, conflicting signals, recovery criteria, and traceability independently.

## Manuscript and repository references

Use the chapter files in `book/` and their `planning/chapter-XX-outline.md` counterparts for lab scope. The existing implementation references are `alpha-car-detailing/README-code.md`, `alpha-car-detailing/README-codeartifacts.md`, and `alpha-car-detailing/global.json`. The root `README.md`, `ROADMAP.md`, `REPOSITORY-STRUCTURE.md`, `harness/README.md`, and `CONTRIBUTING.md` define the existing repository layout and editorial direction. For the capstone, use `book/45-autonomous-improvement-engine.md` and its Hands-on Example and Exercises sections.
