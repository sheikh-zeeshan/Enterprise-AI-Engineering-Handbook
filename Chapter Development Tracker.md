# Enterprise AI Engineering Handbook Development Tracker

Use each row as a development issue. The **Artifacts** column names the code, configuration, lab input, or evidence the chapter should link to. The **Milestone / completion check** column gives the condition for closing the issue. Each milestone groups a set of chapters into a reader checkpoint.

Paths are relative to the handbook repository at `C:\AIHandbook`. M0 names existing baseline artifacts and checks to complete. Paths in planned rows are proposed unless marked existing. Check in lab inputs and expected outputs. Capture generated reports in the run workspace and link to them from the issue. When a row passes, set its status to **Done** and record the verified Git revision and test or run evidence. Use **In progress** or **Blocked** while work is open.

Use the [implementation plan](<Chapter-to-Code Implementation Plan.md>) for build order and the reader lab template. Use the [architecture guide](<Alpha Car Detailing - Project Structure and Architecture.md>) for service ownership and vertical slice placement.

## M0 · Verify the Booking baseline · Chapters 1–7

**Milestone gate** — A clean checkout restores, builds, passes tests, and completes a Booking create/read smoke test. Suggested checkpoint `companion/ch07`.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 1 · The Evolution of Software Development | Verify the solution bootstrap and document the first local Booking run. | `alpha-car-detailing/AlphaCarDetailing.sln`, `alpha-car-detailing/docs/business/business-overview.md`, proposed run record in `examples/ch01-baseline/`. | The API starts from the documented commands and `/health/live` returns `200`. | Verify baseline |
| 2 · Understanding AI Coding Agents | Run the project build and test workflow and capture the work needed for human review. | `alpha-car-detailing/scripts/build.ps1`, `alpha-car-detailing/scripts/test.ps1`, proposed `examples/ch02-agent-workflow/completion-record.md`. | A reader reproduces the checks and records the command outcomes and review step. | Verify baseline |
| 3 · Comparing AI Coding Platforms | Walk through the same sample task with the existing platform entry points or saved output. | `alpha-car-detailing/CLAUDE.md`, `alpha-car-detailing/AGENTS.md`, `alpha-car-detailing/.github/copilot-instructions.md`, proposed `examples/ch03-platform-comparison/comparison.md`. | The comparison uses the same task and acceptance criteria and identifies which repository guidance each workflow used. | Verify baseline |
| 4 · Enterprise AI Engineering | Verify the Booking project references and the architecture described in the ADRs. | `alpha-car-detailing/docs/adr/0001-use-clean-architecture.md`, `alpha-car-detailing/docs/adr/0002-defer-cqrs.md`, projects in `alpha-car-detailing/src/Services/Booking/`. | A reader traces API → Application → Domain and identifies where Infrastructure implements an Application port. | Verify baseline |
| 5 · Repository Discovery | Check the project inventory, dependency map, build entry point, and known risks. | `alpha-car-detailing/scripts/discovery/list-projects.ps1`, `alpha-car-detailing/docs/repository-discovery/`. | The inventory agrees with the solution and points to the Booking source and test entry points. | Verify baseline |
| 6 · Instructions | Verify that current Instructions can be found, validated, and applied to a Booking change. | `alpha-car-detailing/AGENTS.md`, `alpha-car-detailing/instructions/`, `alpha-car-detailing/scripts/instructions/validate-instructions.ps1`. | The validator passes and the lab identifies which applicable Instructions govern the change. | Verify baseline |
| 7 · Skills | Validate the Skill library and verify Booking create/retrieve from the baseline. | `alpha-car-detailing/skills/add-domain-behavior/SKILL.md`, `alpha-car-detailing/scripts/skills/validate-skills.ps1`, `alpha-car-detailing/tests/Services/Booking/`. | The Skills validate. Domain and API tests pass. Creating a booking returns `201` and reading the returned ID returns `200`. | Verify baseline |

Prior verification found seven projects and passed the Instruction and Skill validators in a clean copy. NuGet package restore was blocked by DNS in that environment. Keep the build, tests, and API smoke test open until a full run succeeds.

## M1 · Deliver governed task context · Chapters 8–11

**Milestone gate** — A reader can assemble the fleet-booking task from versioned Prompts, Roles, a Steering Note, and approved source evidence. Suggested checkpoint `companion/ch11`.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 8 · Prompts | Implement the `ACD-BOOK-042` task and its implementation, review, and validation Prompts. Map each acceptance criterion to evidence. | `prompts/tasks/booking/ACD-BOOK-042-add-corporate-fleet-booking.md`, `examples/ch08-prompts/` with fixture, evidence matrix, and verifier. | The complete case passes. A case missing a required account detail or vehicle-limit check produces a specific diagnostic. | Planned |
| 9 · Roles | Define the authority, input, output, and handoff for each Role in the fleet-booking scenario. | `harness/roles/`, `examples/ch09-roles/handoff.json`. | A valid Lead-to-Developer handoff passes. An out-of-authority decision is flagged. | Planned |
| 10 · Steering Notes | Exercise the quarterly fleet-booking Steering Note and its limits. | Existing `harness/search/steering-note.md`, `examples/ch10-steering-notes/scope-cases.json`. | The lab identifies an in-scope request and sends a conflicting or out-of-scope request for review. | Planned |
| 11 · Knowledge Sources | Assemble approved sources for the `ACD-FLEET-284` recurring fleet-booking scenario. | `harness/knowledge/`, `examples/ch11-knowledge-sources/source-manifest.json`. | A reader cites the owner and revision for a rule and flags a stale or conflicting source. | Planned |

## M2 · Run and measure the Harness · Chapters 12–21

**Milestone gate** — One local Harness run produces traceable handoffs, independent validation, business evaluation, and metrics. A failed gate stops the applicable step. Suggested checkpoint `companion/ch21`.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 12 · What Is a Harness? | Build the first local runner for a fleet registration and recurring scheduling fixture. | `harness/scripts/run-harness.ps1`, `examples/ch12-harness/` with task, run manifest, and PR summary. | The passing fixture produces an evidence bundle. The failing fixture stops or follows the bounded retry rule. | Planned |
| 13 · Harness Architecture | Define the stage contracts and separate implementation results from gate decisions for `ACD-FLEET-BOOKING-001`. | `harness/contracts/run-manifest.schema.json`, `examples/ch13-harness-architecture/stage-handoffs.json`. | Each handoff carries the same run ID, and a Developer result cannot satisfy the validation gate. | Planned |
| 14 · Lead Agent | Build a discovery-backed plan and send a versioned handoff to the Developer. | `harness/roles/lead-agent.md`, `examples/ch14-lead-agent/implementation-plan.md`. | The plan cites sources and defines scope, Skills, risks, and checks. Missing context triggers escalation. | Planned |
| 15 · Developer Agent | Implement `POST /api/fleet-bookings` as a vertical slice and capture the applicable `FleetBookingCreated` evidence. | Booking feature files under `alpha-car-detailing/src/Services/Booking/`, `examples/ch15-developer-agent/implementation-record.json`, versioned fixtures for unavailable services. | Tests cover approved and denied requests. The record ties the API and event behavior claimed by the chapter to the tested revision. | Planned |
| 16 · Reviewer Agent | Run an independent review of `ACD-FLEET-142` and track a confirmed finding through remediation. | `harness/roles/reviewer-agent.md`, `examples/ch16-reviewer-agent/review-findings.json`. | Each actionable finding identifies its location, severity, evidence, and disposition. A fixed finding links to the follow-up diff. | Planned |
| 17 · Validator Agent | Run applicable build, API, event, persistence, and configuration checks for the reviewed revision. | `harness/roles/validator-agent.md`, `examples/ch17-validator-agent/gate-report.json`, relevant service tests. | A failed check, missing required result, or revision mismatch blocks progression. | Planned |
| 18 · Evaluator Agent | Score the multi-vehicle fleet-booking outcome against the chapter’s business rubric. | `harness/roles/evaluator-agent.md`, `examples/ch18-evaluator-agent/evaluation-rubric.md`, `evaluation-report.json` in the lab. | The report traces eligibility, authorization, size limit, event outcome, and single-vehicle compatibility to evidence. | Planned |
| 19 · Population | Model dependent work items and claims for inspection before `ReadyForDetailing` in `GOAL-1042`. | `harness/population/`, `examples/ch19-population/goal-1042.json`, claim and retry test output. | Two workers competing for the same item produce one claim. Dependency blocking and retry exhaustion are visible. | Planned |
| 20 · Metrics | Aggregate run, gate, and Population events into quality, reliability, latency, rework, and cost measures. | `harness/metrics/`, `examples/ch20-metrics/seed-events.jsonl` and `metrics-report.json`. | A reader can recompute a rate or percentile from the source events and identify the eligible denominator. | Planned |
| 20.2 · Scratchpad and Run Workspace | Persist plan, handoffs, and evidence so a detailing-location or event-change run can stop and resume. | `examples/ch20-2-run-workspace/` with scratchpad, saved run manifest, and resume case. | A run resumes from its verified checkpoint. Changed input or revision invalidates stale saved state. | Planned |
| 21 · Enterprise Harness Patterns | Run the shared workflow with different Booking, Fleet, and Billing profiles. | `harness/profiles/`, `examples/ch21-harness-patterns/profile-comparison.json`. | Two selected profiles choose the expected checks and permissions, with isolated outputs. | Planned |

Keep Chapters 15–17’s route and event variants explicit. Use pinned fixtures for dependencies the current app has yet to implement. See the architecture guide for when to add Confirm Booking, the outbox, Notifications, and Fleet as real runtime components.

## M3 · Exercise platform workflows · Chapters 22–28

**Milestone gate** — A reader can replay a complete governed delivery workflow and follow the primary Claude Code path when the live prerequisites are available. Suggested checkpoint `companion/ch28`.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 22 · CLAUDE.md | Verify shared and Booking-scoped guidance against a task that exercises both scopes. | Existing `alpha-car-detailing/CLAUDE.md`, `examples/ch22-claude-md/guidance-check.md`. | The relevant guidance and gate mappings are identified and a conflicting scope is explained. | Planned |
| 23 · Claude Skills | Exercise `add-application-command` on the fleet cancellation scenario with the required domain and persistence fixture. | Proposed `alpha-car-detailing/skills/add-application-command/SKILL.md`, `examples/ch23-claude-skills/cancellation-fixture.json`. | The case verifies authorized cancellation, a recorded reason, one event, and regression tests. | Planned |
| 24 · Claude Commands | Provide command entry points that reuse the shared discovery, planning, implementation, and validation procedures. | `examples/ch24-claude-commands/` with command definitions, procedure links, and outputs. | A reader can trace a command to its shared procedure and verify the expected artifact or exit status. | Planned |
| 25 · Claude Memory | Compare stale synchronous HTTP advice with current event and outbox evidence. | `examples/ch25-claude-memory/memory-records.json`, `correction-proposal.md` in the lab. | The stale entry is flagged with a source citation and a reviewable correction proposal. | Planned |
| 26 · Tool Calling | Exercise approved repository-local build and test operations and the denial of work outside configured scope. | `harness/scripts/` tool adapter, `examples/ch26-tool-calling/tool-policy.json` and `tool-audit.jsonl`. | Allowed calls record an outcome. Denied calls produce an audit decision without executing. | Planned |
| 27 · Planning Mode | Produce the fleet-booking plan in a read-only session with scope, assumptions, risks, and validation criteria. | `examples/ch27-planning-mode/plan.md`, `plan-rubric.md`, and source-diff evidence. | The plan passes the rubric and the source diff is empty. | Planned |
| 28 · Enterprise Workflows | Deliver the `ACD-FLEET-028` monthly-allowance workflow through review and a PR handoff. | `examples/ch28-enterprise-workflows/` with task manifest, PR summary, gate report, and resume case. | A reader can resume a saved run and trace allowance, authorization, concurrency, idempotency, and event checks to the result. | Planned |

## M4 · Enforce governance and measure delivery · Chapters 29–35

**Milestone gate** — A governed action blocks when authority, approval, or mandatory evidence is missing. The audit trail explains why. Suggested checkpoint `companion/ch35`. Enforce any control required for a live operation as soon as that operation is introduced.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 29 · AI Governance | Model delegation, scope, expiry, and revocation for corporate booking authority. | `examples/ch29-ai-governance/authority-matrix.md` and `delegation-cases.json`. | The lab identifies the responsible Role and distinguishes an active delegation from a revoked or expired one. | Planned |
| 30 · Security | Verify project and data access boundaries when producing the synthetic incomplete-bookings report. | `examples/ch30-security/security-gate-report.json`, synthetic allowed and denied cases. | A prohibited request is denied and the independent gate records its result without exposing sensitive contents. | Planned |
| 31 · Human Approval | Bind a scoped approval to the exact version of a recurring fleet release. | `harness/contracts/approval-record.schema.json`, `examples/ch31-human-approval/approval-cases.json`. | Valid approval proceeds through the gate. Missing, expired, revoked, or mismatched approval blocks it. | Planned |
| 32 · Audit Trails | Correlate `ACD-284` task, API/import work, permissions, checks, and human decisions. | `examples/ch32-audit-trails/audit-events.jsonl` and `release-timeline.md`. | A reader reconstructs the timeline. Ingesting the same event again preserves its identity. | Planned |
| 33 · Policies | Apply the fleet analytics data policy before an operation is allowed. | `examples/ch33-policies/fleet-analytics-policy.json` and `policy-decisions.json`. | Restricted input is denied, approved minimized input is accepted, and both decisions include the policy version. | Planned |
| 34 · Compliance | Map multi-location fleet booking requirements to controls, applicable checks, approval, and retained evidence. | `examples/ch34-compliance/control-matrix.md` and `evidence-manifest.json`. | Each applicable control has evidence. Removing required evidence blocks simulated release. | Planned |
| 35 · Productivity Metrics | Compare matched fleet-booking delivery runs using time to acceptance, rework, and cost. | `harness/metrics/`, `examples/ch35-productivity-metrics/accepted-outcomes.json` and `comparison-report.md`. | Measures can be recalculated from shared scope, acceptance criteria, and stated denominators. | Planned |

## M5 · Learn from evidence and govern changes · Chapters 36–45

**Milestone gate** — The Chapter 45 capstone reproduces the historical pattern, verifies affected consumers, replays the candidate, binds the decision to the tested revision, and traces rollback. Suggested checkpoint `companion/ch45`.

| Chapter | Development task | Artifacts | Milestone / completion check | Status |
| --- | --- | --- | --- | --- |
| 36 · Static vs Adaptive Harnesses | Run the same fleet-limit evidence through the static workflow and the recommendation path. | `examples/ch36-static-vs-adaptive/` with common inputs and paired reports. | Both runs preserve the required checks. The adaptive run also presents a sourced proposal. | Planned |
| 37 · Harness Memory | Store the approved remediation for duplicate booking retries and implement scoped retrieval. | `harness/knowledge/`, `examples/ch37-harness-memory/memory-fixtures.json`. | Approved, relevant memory is available. Stale, pending, or out-of-scope entries are excluded. | Planned |
| 38 · Learning from Builds | Normalize recurring fleet build failures, retries, and successful reruns. | `examples/ch38-learning-from-builds/build-events.jsonl` and `build-learning-report.md`. | The report separates recurrence from infrastructure interruption and links a candidate to original records. | Planned |
| 39 · Learning from Reviews | Group verified review findings by signature, independent change, and disposition. | `examples/ch39-learning-from-reviews/review-findings.jsonl` and `review-learning-report.md`. | Duplicate comments are counted once per occurrence and recommended changes cite confirmed findings. | Planned |
| 40 · Learning from Pull Requests | Replay versioned fleet-endpoint PRs and summarize findings, rework, and clean comparisons. | `examples/ch40-learning-from-prs/pr-history.jsonl` and `cohort-report.md`. | The report reconstructs a PR timeline and uses an explicit eligible-change denominator. | Planned |
| 41 · Skill Evolution | Propose and replay a change to the Transactional Outbox Skill for `FleetBookingCreated`. | `examples/ch41-skill-evolution/` with baseline and candidate revisions, replay cases, and draft decision. | Results show what changed on previous misses and clean cases and identify the approver. | Planned |
| 42 · Prompt Evolution | Compare the feature-development Prompt with a candidate under the same cases and rubric. | `examples/ch42-prompt-evolution/paired-replay.json` and `evaluation-report.md`. | Baseline and candidate use the same inputs. The report shows acceptance and held-out results. | Planned |
| 43 · Instruction Evolution | Propose a scoped rule refinement for direct data access across service boundaries. | `examples/ch43-instruction-evolution/instruction-proposal.md` and `scope-checks.json`. | A conflicting or out-of-scope change is flagged. The proposal shows the applicable owner and gate. | Planned |
| 44 · Knowledge Graph | Trace `BookingConfirmed` versions to consumers, owners, and applicable contract checks. | `examples/ch44-knowledge-graph/dependencies.json`, subscription fixtures, and `impact-report.md`. | The lab distinguishes a verified dependency from a stale edge and identifies the consumers affected by `FleetAccountReference`. | Planned |
| 45 · Autonomous Improvement Engine | Run `AIE-045` across evidence correlation, dependency checks, candidate replay, human decision, simulated activation, and rollback. | `examples/ch45-improvement-engine/` with evidence bundle, replay manifest, decision, and rollback audit records. | The run reproduces two omissions across three comparable changes, exercises the six paired cases, blocks a stale approval, and traces the restored revision to the original evidence. | Planned |

## First issues to open

1. **M0 verification** — Resolve package access for the baseline run, execute the Booking smoke test, and record the verified revision and results.
2. **Chapter 8 Prompt lab** — Add the `ACD-BOOK-042` prompts, evidence matrix, fixture, and verifier. Link the lab from Chapter 8 after the positive and negative cases pass.
3. **Booking vertical slice refactor** — In a focused code issue, organize the existing Create and Get endpoints by feature and verify route, response, and persistence behavior. Track it alongside M1 and link it to the Chapter 15 implementation work it supports.
