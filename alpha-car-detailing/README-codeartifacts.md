# Alpha Car Detailing — Companion Code Repository

This folder contains the evolving code sample for the **Enterprise AI Engineering Handbook**. The repository is intentionally developed chapter by chapter so each manuscript concept has a concrete GitHub artifact.

## Current baseline

This baseline aligns with the handbook through **Chapter 7 — Skills**.

It contains:

- A small .NET 10 Booking service using Clean Architecture boundaries.
- A straightforward application service (`BookingService`) rather than CQRS.
- A domain aggregate with one business rule and a domain event.
- EF Core with SQLite for a low-friction local sample.
- Minimal API endpoints, health checks, OpenAPI, unit tests, and an integration test.
- Repository Discovery documentation.
- Persistent AI-agent instructions.
- A governed reusable Skill library and validation scripts.

## Deliberately deferred

The first implementation does **not** use CQRS, MediatR, an event broker, transactional outbox processing, Redis, authentication, containers, Kubernetes, or a multi-agent harness. These capabilities will be introduced only after the basic sample is working and the corresponding chapters explain their purpose.

## Chapter-wise GitHub artifacts

### Chapter 1 — The Evolution of Software Development

Committed artifacts:

- Repository and solution bootstrap.
- `docs/business/business-overview.md`.
- `docs/architecture/architecture-vision.md`.
- Basic health endpoint.
- Shared build configuration and formatting rules.

Suggested commit: `feat(ch01): bootstrap Alpha Car Detailing companion repository`

### Chapter 2 — Understanding AI Coding Agents

Committed artifacts:

- `scripts/build.ps1`, `scripts/test.ps1`, and environment verification.
- Agent workflow and human-boundary documentation.
- Completion-evidence template.
- Liveness and readiness endpoints.

Suggested commit: `feat(ch02): establish AI-agent development workflow`

### Chapter 3 — Comparing AI Coding Platforms

Committed artifacts:

- `CLAUDE.md` and `AGENTS.md` entry points.
- `.github/copilot-instructions.md`.
- Vendor-neutral shared guidance.
- Platform portability notes.

Suggested commit: `feat(ch03): add vendor-neutral AI platform configuration`

### Chapter 4 — Enterprise AI Engineering

Committed artifacts:

- Clean Architecture project structure.
- Booking bounded-context baseline.
- Domain building blocks.
- Initial Architecture Decision Records.
- Pull-request and ownership conventions.

Suggested commit: `feat(ch04): establish enterprise AI engineering baseline`

### Chapter 5 — Repository Discovery

Committed artifacts:

- Repository map, project inventory, build guide, and risk register.
- Discovery scripts that inspect projects and references.
- A discovery checklist for AI agents.

Suggested commit: `feat(ch05): add repository discovery documentation and tooling`

### Chapter 6 — Instructions

Committed artifacts:

- Root and scoped instruction files.
- Authoritative instruction library under `/instructions`.
- Instruction validation script.
- Enforceable compiler and architecture conventions.

Suggested commit: `feat(ch06): introduce governed repository instructions`

### Chapter 7 — Skills

Committed artifacts:

- Reusable Skills under `/skills`.
- Skill catalog, governance, versioning, and template.
- Skill validation script.
- First working vertical slice: create and retrieve a Booking.
- Domain and integration tests.

Suggested commit: `feat(ch07): add governed skills and working booking slice`

## Run locally

```powershell
cd C:\ZeeWork\Enterprise-AI-Engineering-Handbook\alpha-car-detailing
dotnet restore
dotnet build
dotnet test
dotnet run --project .\src\Services\Booking\AlphaCarDetailing.Booking.Api
```

Open Swagger at `https://localhost:<port>/swagger` or use:

```http
POST /api/bookings
GET /api/bookings/{bookingId}
GET /health/live
GET /health/ready
```

Example request:

```json
{
  "customerId": "669c3922-9194-48ef-9593-142d4f6dd98d",
  "stationId": "ac74a17c-151b-4733-ae86-78e3a3984598",
  "vehicleRegistrationNumber": "LEA-1234",
  "serviceCode": "EXTERIOR-WASH",
  "scheduledAtUtc": "2026-08-01T09:00:00Z"
}
```

## Next development step

After the basic sample builds and runs, Chapter 8 can add governed task prompts. CQRS should be introduced later as an explicit refactoring milestone rather than being embedded in the initial baseline.
