# ADR 0002 — Defer CQRS Until the Basic Sample Works

## Status
Accepted

## Context
CQRS, mediator pipelines, commands, queries, and handlers add concepts that can obscure the first end-to-end sample.

## Decision
Use a direct `BookingService` application service and repository abstraction initially. Introduce CQRS later as a documented refactoring milestone.

## Consequences
The initial sample is easier to run and understand. The application boundary still permits later refactoring without moving domain logic into the API.
