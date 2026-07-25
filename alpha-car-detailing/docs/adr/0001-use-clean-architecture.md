# ADR 0001 — Use Clean Architecture Boundaries

## Status
Accepted

## Context
The companion sample must demonstrate clear architectural boundaries without unnecessary framework complexity.

## Decision
Use API, Application, Domain, and Infrastructure projects for the Booking service.

## Consequences
Domain code remains independent. More projects exist, but responsibilities are explicit.
