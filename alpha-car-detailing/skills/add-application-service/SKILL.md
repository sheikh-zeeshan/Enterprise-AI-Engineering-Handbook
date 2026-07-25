---
name: add-application-service
version: 1.0.0
owner: AI Engineering Team
status: approved
risk-level: low
---

# Add Application Service

## Purpose
Add or extend a direct application service use case without introducing CQRS.

## When to Use
Use when the current task explicitly requires this recurring engineering outcome.

## When Not to Use
Do not use for unrelated refactoring or architecture changes.

## Inputs
Task scope, repository evidence, applicable instructions, and acceptance expectations.

## Preconditions
Read `/AGENTS.md`, `/instructions/architecture.md`, and the nearest scoped instructions.

## Required Repository Discovery
Identify affected projects, dependencies, existing conventions, and tests.

## Applicable Instructions
- `/instructions/architecture.md`
- `/instructions/coding-standards.md`
- `/instructions/testing.md`

## Execution Steps
1. Inspect existing implementation patterns.
2. Make the smallest coherent change.
3. Preserve dependency direction.
4. Add or update tests.
5. Run repository validation.

## Expected Outputs
Application interface, implementation, repository use, and tests.

## Validation Steps
Run `dotnet build` and the relevant `dotnet test` projects.

## Error Handling
Stop and report missing requirements or conflicting instructions rather than inventing behavior.

## Approval Requirements
Architecture or security-impacting changes require human approval.

## Completion Evidence
List changed files, commands executed, test results, assumptions, and remaining risks.

## Version History
- 1.0.0 — Initial approved version.
