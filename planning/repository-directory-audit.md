# Repository directory and companion-code audit

Reviewed: 22 November 2026. Repository: `C:\AIHandbook`. Source revision at review: `82baa72`.

## Findings

**Retain `alpha-car-detailing/src/`. It is a working Chapter 7 teaching baseline, not the complete application or coding Harness described in later chapters.** The solution compiled with zero warnings and errors, its three existing tests passed, and a separate HTTP smoke check passed. A subsequent standard restore failed because NuGet vulnerability metadata was unreachable; fresh dependency verification remains incomplete.

The main problems are incomplete implementation, stale status documents, duplicate documentation, a misplaced CI workflow, and useful verification code stored under ignored build output. An old baseline or an empty planned directory is not, by itself, a reason to delete it.

This audit marks recommendations only. No files or directories were deleted, and no application source, policies, Instructions, or Skills were changed. Test execution created generated outputs and the existing integration test inserted a synthetic booking into its configured local database. The separate HTTP smoke check used a disposable database under `build/repository-audit/` and its server was stopped afterward.

Action meanings:

- **Keep**: retain authoritative or useful content.
- **Update**: retain the location but correct content or implementation.
- **Populate**: intentional placeholder; planned capability is absent.
- **Consolidate / move**: preserve useful material in one documented location before removing duplication.
- **Delete candidate**: redundant or reproducible material; prerequisites are stated where applicable. This is not an instruction to delete immediately.

## Root directories

All paths below are relative to `C:\AIHandbook\`.

| Directory | Purpose | Observed contents and status | Recommendation |
| --- | --- | --- | --- |
| `.git/` | Repository history and version-control metadata. | Required for this checkout; internal contents were not audited. | **Keep.** Never include in content cleanup. |
| `.github/` | Repository-level GitHub automation and collaboration configuration. | `workflows/` exists but is empty. The only application workflow is nested under the companion app. | **Populate.** Establish root workflows rather than deleting this directory. |
| `alpha-car-detailing/` | Running .NET application and application-specific engineering assets. | Seven-project Chapter 7 baseline: five source projects and two test projects. Several later-chapter assets are placeholders. | **Keep and extend.** |
| `book/` | Authoritative manuscript and editorial rules. | 57 Markdown files: editorial guide, Chapters 1–55, and Chapter 20.2. Manuscript presence does not establish that its companion features are implemented. | **Keep.** Review chapter-to-code claims as labs are implemented. |
| `build/` | Generated publication output and temporary verification workspaces. | About 297 MB before this audit. Contains chapter labs as well as generated files and browser caches. Only `.gitkeep` is tracked. | **Selective cleanup.** Preserve useful lab harnesses before deleting workspaces. |
| `diagrams/` | Diagram export configuration, manifest, extracted sources, and SVGs. | Current shared pipeline; all 1,025 diagram blocks, extracted sources, exports, and hashes agree. | **Keep.** This is actively used publication material. |
| `examples/` | Reader-facing chapter labs and focused demonstrations. | Only `.gitkeep`; no implemented labs. | **Populate.** Good destination for reusable verification labs currently under `build/`. |
| `harness/` | Shared coding-workflow orchestration, validation, evidence, and learning components. | Primarily scaffolding. Its executable scripts currently export/check diagrams; they do not implement a coding Harness. | **Keep and implement.** Correct the misleading impression that the coding Harness is already operational. |
| `images/` | Screenshots and other non-diagram publication images. | Only `.gitkeep`. | **Keep as placeholder.** Optional to omit from a minimal tree until used; not obsolete content. |
| `planning/` | Chapter outlines and editorial planning records. | 56 chapter outlines before this audit. Separate from final manuscripts. | **Keep as planning history.** Do not treat an outline as the current chapter text. |
| `prompts/` | Shared task Prompts and role-oriented prompt contracts. | README and empty category placeholders. | **Populate.** No functioning Prompt library is present here yet. |
| `references/` | Source notes and supporting technical references. | Chapter 52 observability reference notes plus `.gitkeep`. | **Keep and extend.** |
| `skills/` | Shared, reusable engineering Skills across the project. | README and category placeholders. Actual Booking Skills live inside the companion app. | **Keep; define shared versus application-specific ownership.** |
| `templates/` | Reusable authoring and engineering templates. | Chapter, ADR, Prompt, and Skill templates. | **Keep.** |

## Shared subdirectories

| Directory | Purpose | Observed status | Recommendation |
| --- | --- | --- | --- |
| `.github/workflows/` | Automatically discovered workflows for this GitHub repository. | Empty. | **Populate.** Move/adapt the application workflow here and set its working directory to `alpha-car-detailing/`. |
| `diagrams/source/` | Extracted Mermaid `.mmd` assets. | 1,025 generated sources. Despite the directory name, chapter Mermaid fences are authoritative under the current pipeline. | **Keep generated copies synchronized.** Edit `book/`, then regenerate. |
| `diagrams/exported/` | SVG figures for publication. | 1,025 exports verified by the manifest. | **Keep.** Do not mark for deletion because they are generated. |
| `harness/knowledge/` | Approved shared learning and knowledge assets. | `.gitkeep` only. | **Populate.** |
| `harness/metrics/` | Harness metric definitions and supporting assets. | `.gitkeep` only. | **Populate.** Separate tracked definitions from generated run measurements. |
| `harness/population/` | Work population, task selection, and scheduling assets. | `.gitkeep` only. | **Populate.** |
| `harness/roles/` | Shared Role contracts and responsibility definitions. | `.gitkeep` only. | **Populate.** Distinguish Role authority from the prompt text that invokes a Role. |
| `harness/scripts/` | Shared automation entry points. | Diagram exporter and four passing exporter tests; no coding-workflow runner. | **Keep.** Consider moving publication tooling to a dedicated tooling location later, with all references updated. |
| `harness/search/` | Existing Steering Note and proposed-update record. | Active objective still says establish the repository and prepare Chapter 1. This is handbook editorial context, not a corporate fleet-booking mission. | **Update.** Retain human ownership. Do not reuse this stale objective for application development. |
| `harness/tests/` | Shared coding-Harness tests. | `.gitkeep` only. Exporter tests currently sit beside the exporter in `scripts/`. | **Populate.** |
| `prompts/roles/` | Role-oriented invocation prompts. | `.gitkeep` only. | **Populate.** |
| `prompts/tasks/` | Versioned task Prompts and examples. | `.gitkeep` only. | **Populate.** |
| `skills/architecture/` | Shared architecture procedures. | `.gitkeep` only. | **Populate when used.** |
| `skills/devops/` | Shared delivery and operations procedures. | `.gitkeep` only. | **Populate when used.** |
| `skills/dotnet/` | Shared .NET development procedures. | `.gitkeep` only. | **Populate when used.** Keep Booking-specific procedures with the app. |
| `skills/governance/` | Shared review, approval, and governance procedures. | `.gitkeep` only. | **Populate when used.** |

## Alpha Car Detailing directories

Paths in this table are relative to `alpha-car-detailing/`.

| Directory | Purpose | Observed status | Recommendation |
| --- | --- | --- | --- |
| `.claude/` | Local Claude Code configuration. | Local settings file; not application code. | **Keep locally as needed.** Review portability before including local settings in shared configuration. |
| `.github/` | Copilot instructions and app-local GitHub assets. | Copilot guidance is useful; workflow is nested below it. | **Keep guidance; move CI workflow.** |
| `.github/workflows/` | Intended app build/test automation. | Contains `build.yml`, but it is below the repository root. Its relative commands also assume the app is the working directory. | **Move/adapt**, then remove the empty nested workflow directory if no longer needed. |
| `.idea/` | Rider/JetBrains local project state. | IDE metadata, about 0.01 MB. | **Optional local cleanup.** Close the IDE first and preserve settings you want. Not outdated application source. |
| `.vs/` | Visual Studio local caches and session state. | About 0.91 MB. | **Optional local cleanup** after closing Visual Studio. Not part of the book or source implementation. |
| `deploy/` | Deployment and infrastructure definitions. | `.gitkeep` only; no container/Kubernetes/Azure deployment is implemented here. | **Populate later.** |
| `docs/` | Application domain, architecture, decisions, and discovery knowledge. | Useful baseline documents. | **Keep and maintain.** |
| `docs/adr/` | Accepted architecture decisions. | Clean Architecture and deliberate CQRS deferral. | **Keep.** Baseline decisions remain applicable. |
| `docs/architecture/` | Architecture vision. | Describes the actual small Booking service and SQLite starting point. | **Keep.** Separate current implementation from future architecture. |
| `docs/business/` | Business overview. | Existing domain context. | **Keep.** |
| `docs/repository-discovery/` | Repository map, build instructions, and risk register. | Useful baseline information; explicitly records missing capacity, security, and integration behavior. | **Update verification details** and maintain against the code. |
| `harness/` | Intended application-specific Harness assets. | README-only Chapter 7 placeholder. | **Consolidate design.** Prefer shared execution in root `harness/` with an app profile; keep this path only if it owns real app-specific configuration. |
| `instructions/` | Persistent application engineering standards. | Four required Instruction files exist. | **Keep.** Existence checks do not establish enforcement. |
| `knowledge/` | Intended governed application knowledge manifest. | README-only placeholder. Useful application knowledge currently resides under `docs/`. | **Populate or consolidate with `docs/` through a manifest.** Avoid duplicating facts. |
| `prompts/` | Intended application-specific task Prompts. | README-only placeholder. | **Populate or link to root `prompts/tasks/booking/`.** Define one owner per Prompt. |
| `roles/` | Intended application-specific Role assets. | README-only placeholder. | **Consolidate with shared `harness/roles/`**, retaining local overrides only where needed. |
| `scripts/` | Application build/test and supporting validation commands. | Real scripts exist. | **Keep and harden.** Resolve the app working directory explicitly and propagate native command failures. |
| `scripts/discovery/` | Project discovery. | `list-projects.ps1`. Seven projects exist. | **Keep.** |
| `scripts/instructions/` | Instruction structure validation. | Checks four required files for existence. | **Keep as a limited structural check.** Add independent enforcement where required. |
| `scripts/skills/` | Skill structure validation. | Checks eight required headings in each Skill. | **Keep as a limited structural check.** It does not verify Skill behavior or model compliance. |
| `skills/` | Governed application Skill library, catalog, template, and governance. | Eight concrete Skills. All contain the validator's required headings. | **Keep.** Stronger than the empty shared root Skill categories. |
| `src/` | Runnable .NET 10 application source. | Five source projects compile; API smoke check passed. | **Keep and extend.** Detailed assessment below. |
| `steering/` | Intended application mission and temporary priorities. | README-only placeholder. | **Populate** with application scope; do not copy the handbook's stale Chapter 1 mission. |
| `tests/` | Application automated tests. | Two Domain unit tests and one API integration test. | **Keep and expand.** Test coverage is too small to establish the later enterprise scenarios. |

The eight Skill directories each have one specific purpose:

| Path under `alpha-car-detailing/skills/` | Purpose | Recommendation |
| --- | --- | --- |
| `add-application-service/` | Add a direct application use case. | **Keep.** Matches the non-CQRS baseline. |
| `add-domain-behavior/` | Add aggregate/value-object rules. | **Keep.** |
| `add-domain-event/` | Record a local domain event. | **Keep.** Recording is distinct from broker publication. |
| `add-ef-core-entity/` | Add persistence mappings and repository support. | **Keep.** |
| `create-api-endpoint/` | Add an HTTP endpoint and its checks. | **Keep.** |
| `validate-change/` | Collect build/test/review completion evidence. | **Keep.** |
| `write-integration-tests/` | Verify externally visible application behavior. | **Keep.** |
| `write-unit-tests/` | Verify isolated domain behavior. | **Keep.** |

## Source and test directory assessment

Paths below are relative to `alpha-car-detailing/`.

| Directory | Purpose | Assessment / action |
| --- | --- | --- |
| `src/BuildingBlocks/` | Container for shared low-level building blocks. | **Keep.** |
| `src/BuildingBlocks/AlphaCarDetailing.BuildingBlocks.Domain/` | Entity, AggregateRoot, DomainException, and IDomainEvent primitives. | **Keep.** Compiles. Review the direct native SQLite package reference: the domain building blocks do not use persistence APIs. |
| `src/Services/` | Container for service implementations. | **Keep.** Only Booking is implemented. |
| `src/Services/Booking/` | Booking bounded-context implementation. | **Keep.** |
| `src/Services/Booking/AlphaCarDetailing.Booking.Api/` | Composition root, HTTP routes, health endpoints, Swagger, and startup database creation. | **Keep; evolve.** Working local sample; no authentication/authorization. `EnsureCreatedAsync` is a local bootstrap, not a production migration strategy. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Api/Endpoints/` | Booking create/read HTTP handlers and domain-error mapping. | **Keep.** Create/read/400/404 paths passed the smoke check. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Api/Properties/` | Local launch profiles. | **Keep.** Development configuration. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Application/` | Use-case orchestration and dependency registration. | **Keep.** Direct application service is consistent with ADR 0002. Review the native SQLite package reference here too. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Application/Bookings/` | BookingService, requests, DTO, repository/service abstractions. | **Keep.** Creates and retrieves individual Bookings; not fleet orchestration. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Domain/` | Booking business model. | **Keep.** Review its unused persistence-related package reference. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Domain/Bookings/` | Aggregate, ID, status, and registration-number value object. | **Keep.** Validates required fields and future scheduling, normalizes values, and creates Pending bookings. No full lifecycle or capacity logic. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Domain/Bookings/Events/` | BookingCreatedDomainEvent. | **Keep.** Local event recording exists; dispatch/outbox/integration delivery does not. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Infrastructure/` | Persistence implementation and dependency registration. | **Keep.** EF Core/SQLite is intentional for the baseline. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Infrastructure/Persistence/` | BookingDbContext. | **Keep.** |
| `src/Services/Booking/AlphaCarDetailing.Booking.Infrastructure/Persistence/Configurations/` | EF Core aggregate/value-object mapping. | **Keep.** SQLite create and readback worked. |
| `src/Services/Booking/AlphaCarDetailing.Booking.Infrastructure/Persistence/Repositories/` | IBookingRepository implementation. | **Keep.** |
| `tests/Services/` | Container for service-level tests. | **Keep.** |
| `tests/Services/Booking/` | Container for Booking test projects. | **Keep.** |
| `tests/Services/Booking/AlphaCarDetailing.Booking.Domain.Tests/` | Tests Pending creation/event recording and rejection of past scheduling. | **Keep.** Two tests passed. |
| `tests/Services/Booking/AlphaCarDetailing.Booking.IntegrationTests/` | API integration test using WebApplicationFactory. | **Keep and improve isolation.** One POST test passed. It uses the app's configured file-backed database rather than a per-test disposable database. |
| Any `bin/` and `obj/` below these projects | Compiler outputs and restore/intermediate files. | **Delete candidates for cache cleanup only.** They regenerate; restoring afterward requires dependency availability. Their presence does not make source obsolete. |

The project-reference direction is broadly consistent with the accepted architecture. However, the native SQLite package is directly referenced by the shared Domain building blocks, Booking Domain, and Application projects even though their code does not use it. Investigate and scope that dependency to the persistence/runtime projects that need it; do not delete entire projects to correct a package-placement issue.

Missing later-chapter capabilities include corporate account/fleet ownership checks, booking allowances and concurrency controls, station reservations, authentication and authorization, durable event publication, transactional outbox/idempotent consumers, other services, full observability, deployment assets, and the executable coding Harness. These are implementation gaps, not evidence that the existing Booking code should be discarded.

## Build workspace retention and deletion candidates

`build/` is ignored by Git except for `.gitkeep`. **Do not delete all of it blindly.** Some source listings are extracted from chapters and reproducible, but the lab hosts, project definitions, validators, fixtures, and results are not all covered by the shared diagram exporter.

| Directory / pattern | Purpose and contents | Recommendation |
| --- | --- | --- |
| `build/chapter50-verification/` | Event-contract lab: Contract.cs/project, JSON fixture/schema, Python verifier, figure copies, temporary databases. | **Preserve useful lab sources/fixtures first**, preferably under `examples/ch50-event-driven/`. Then delete generated outputs and disposable databases. |
| `build/chapter51-verification/` | Security lab: extracted chapter C#, host/project, validation/render scripts, figures, browser profile and caches. | **Preserve reusable lab harness first**, then delete generated previews/caches. Replace machine-specific rendering scripts with shared tooling where appropriate. |
| `build/chapter52-verification/` | Observability lab: extracted C#, project/host, verification results, render scripts and browser files. | **Preserve useful host/checks/results first**, preferably as a documented lab; then clean temporary output. |
| `build/chapter54-verification/` | Booking contract-protection tests, a separate test project, extraction/render scripts and results. | **Preserve/promote the reusable test setup.** Tests are not included in AlphaCarDetailing.sln; extracted test source alone does not put it in the normal suite. Clean generated files afterward. |
| `build/chapter55-verification/` | Fleet-state replay PowerShell example, Python verifier, results, figure previews and browser profiles. | **Preserve reusable replay/verification setup first**, then clean generated output. It is a demonstration, not a live Fleet control plane. |
| `build/diagrams/` | Temporary diagram debug/preview helpers and review images. | **Delete candidate** once visual review is complete and any useful helper has been retained elsewhere. Current diagram source/export authority is under `book/` and `diagrams/`. |
| `build/publication/` | 57 generated publication Markdown files under `book/`. | **Delete candidate.** Recreate using the shared exporter's `--check --publication` command. Keep authoritative manuscripts. |
| `build/repository-audit/` | This audit's TRX files, HTTP results, API logs, and disposable smoke database. | **Retain while reviewing this report; delete candidate afterward.** The report itself is tracked separately under `planning/`. |
| Browser profile/cache directories below verification workspaces | Local render-browser state, crash files and caches. | **Delete candidates** after render processes close; not authoritative book content. |
| Project `bin/` / `obj/` directories below verification workspaces | Generated lab build output. | **Delete candidates** once needed verification evidence is retained. |

## Stale or redundant files requiring attention

These file-level findings explain why some directories look out of date. Retain the directories themselves where they still have a purpose.

| File | Evidence | Recommendation |
| --- | --- | --- |
| `README.md` | Current Status still names Chapter 1's first draft as the next milestone; Local Setup uses the previous `C:\ZeeWork\...` path. | **Update.** Distinguish manuscript completion from companion implementation. |
| `ROADMAP.md` | Immediate milestone remains Chapter 1; chapter ranges stop at 54 and do not reflect the current manuscript set. Checkboxes do not establish verified code delivery. | **Update.** Separate editorial review status and implementation milestones. |
| `REPOSITORY-STRUCTURE.md` | Describes the initial tree and omits current planning and application engineering assets. | **Update.** Link to the maintained directory inventory. |
| `harness/README.md` | Describes intended Harness responsibilities without distinguishing the current scaffolding from implemented behavior. | **Update.** State which runner/stages/checks actually exist. |
| `harness/search/steering-note.md` | Objective is repository bootstrap and Chapter 1 review. | **Update or archive as historical mission**, with a new explicitly owned current mission. Do not silently treat it as application instructions. |
| `alpha-car-detailing/README.md` | Still describes all contents as planned despite an implemented Booking slice. | **Update/consolidate** into the canonical application entry point. |
| `alpha-car-detailing/README-code.md` | Useful detailed baseline guide, but old local path and a fixed appointment date of 2026-08-01, now in the past. Some claimed chapter artifacts are not present. | **Keep useful content and correct it**, preferably consolidate into `README.md`. Use a generated future appointment in examples. |
| `alpha-car-detailing/README-codeartifacts.md` | SHA-256 matches `README-code.md` exactly. | **Delete candidate.** Consolidate to one authoritative code guide and update references before removal. |
| `alpha-car-detailing/alpha-car-detailing-file-list.txt` | Static list contains `/mnt/data/Enterprise-AI-Engineering-Handbook/...` absolute paths from another environment. | **Delete candidate or regenerate** as portable relative paths. The project discovery script is a better live inventory. |
| `alpha-car-detailing/.github/workflows/build.yml` | Nested workflow location and app-relative commands are incompatible with using it directly as this monorepo's root workflow. | **Move/adapt** into root `.github/workflows/` with explicit app working directory and applicable paths. Keep Copilot instructions under the app. |
| `alpha-car-detailing/scripts/build.ps1`, `scripts/test.ps1` | Bare dotnet commands depend on caller location; scripts do not explicitly inspect `$LASTEXITCODE`. | **Update.** Ensure a failed native command stops the workflow and returns failure to its caller. |
| `Chapter Development Tracker.md`, `Chapter-to-Code Implementation Plan.md` | Useful current implementation plans. Their historical verification notes say restore/build/test/smoke remain open; the implementation plan also describes a quarterly fleet Steering Note that the current file does not contain. | **Keep and update evidence/context references.** This audit establishes local build/test/smoke results, but fresh restore is still blocked. Do not mark the whole M0 clean-checkout gate complete. |
| `Alpha Car Detailing - Project Structure and Architecture.md` | Clearly distinguishes the current Booking implementation from a proposed architecture. | **Keep.** Future service directories shown here are not evidence those services exist. |
| `notest.txt` | Loose instruction-layering notes outside the approved Instruction/reference structure. | **Review then delete candidate**, or move vetted content into an owned reference document. Do not treat as active repository Instructions. |
| `Create handbook repository starter kit.ps1` | Already modified before the audit. Its current content includes git add/commit/push commands rather than directory creation. | **Preserve the user's working change.** Review/rename/archive separately; do not execute as setup or include in automatic cleanup. |

## Validation evidence and limits

| Check | Result | What it establishes |
| --- | --- | --- |
| SDK selected from the app directory | `10.0.404`, target `net10.0`; runtime `10.0.15`. | Repository SDK policy selects .NET 10 even though a newer SDK is installed at the parent level. |
| Build all seven projects without restore | **Passed: 0 warnings, 0 errors.** | Current source compiles using locally available restored dependencies. |
| Existing solution tests | **Passed: 3/3** (2 Domain, 1 API integration). | Narrow baseline behavior; not fleet/security/production correctness. |
| Separate smoke: `/health/live`, `/health/ready` | **200 / 200**. | App starts and responds. Both map generic health checks; readiness does not test database/broker availability. |
| Separate smoke: create / read Booking | **201 Pending / 200**, matching ID. | Local SQLite write/read round trip works. |
| Separate smoke: past appointment / unknown Booking | **400 / 404**. | Two baseline rejection/missing-resource paths work. |
| Separate smoke: Swagger JSON | **200** in Development. | API metadata endpoint is usable in the tested local configuration. |
| Standard package restore after the local build/tests | **Blocked: NU1900 promoted to error** because `https://api.nuget.org/v3/index.json` was unreachable for vulnerability data. | A normal fresh dependency verification was not completed. Do not infer package safety or clean-checkout reproducibility from the cached build. |
| Instruction/Skill structural checks | Manual equivalents passed: **4 required files; 8 Skills with required headings**. | Structural presence only. Direct `.ps1` execution was blocked by this host's unsigned-script policy; no policy was changed. |
| Diagram integrity | **Passed: 1,025 blocks/sources/exports/hashes agree.** | Exported diagrams are current under the checked-in manifest. |
| Diagram exporter tests | **4/4 passed**. | Exporter parsing tests; not application or coding-Harness tests. |

Successful build command, from `alpha-car-detailing/`:

```powershell
dotnet build AlphaCarDetailing.sln --no-restore --verbosity minimal --disable-build-servers -m:1 -p:UseSharedCompilation=false -nodeReuse:false
```

Successful test command, from the same directory:

```powershell
dotnet test AlphaCarDetailing.sln --no-build --no-restore --verbosity normal -m:1 -nodeReuse:false --logger 'trx;LogFilePrefix=directory-audit' --results-directory C:\AIHandbook\build\repository-audit\tests
```

The initial default parallel build was canceled by the execution environment; the initial default test attempt also stopped without diagnostics. The serial commands above completed. A subsequent standard restore wrote new intermediate restore state and failed on NU1900; these successful commands describe the earlier verified state, not a successful post-restore rerun. Resolve NuGet connectivity before repeating the full normal restore/build/test sequence. No project settings were relaxed to suppress this failure.

Diagram commands, from the repository root:

```powershell
node harness/scripts/export-diagrams.cjs --check
node --test harness/scripts/export-diagrams.test.cjs
```

Local evidence: [HTTP smoke results](../build/repository-audit/smoke-results.json) and TRX files under `build/repository-audit/tests/`. These generated files are ignored by Git and are not durable publication assets. Historical chapter-lab result files were inspected for retention decisions, not rerun or accepted as current verification of those labs.

## Recommended next work

1. Retain the five source projects and two test projects. Recover normal NuGet restore connectivity and record a full clean-checkout verification.
2. Consolidate application READMEs, fix stale root status/Steering Notes, and move/adapt CI into root workflows.
3. Preserve reusable lab harnesses currently under ignored `build/` in documented, tracked `examples/` or test locations. Regenerate chapter listings instead of maintaining competing copies.
4. Remove confirmed redundant files and selected generated caches only after preservation and reference updates.
5. Implement Chapters 8–11's actual Prompt/Role/Steering/Knowledge assets, then the shared .NET/PowerShell coding Harness. Keep shared execution in root `harness/` and application-specific rules in the companion app.
