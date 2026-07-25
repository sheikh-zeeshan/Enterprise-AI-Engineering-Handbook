---
title: Architecture Instructions
owner: Architecture Team
status: approved
version: 1.0.0
scope: repository
---

# Architecture Instructions

- Domain must not reference Application, Infrastructure, or API.
- Application may reference Domain but not Infrastructure implementations or API.
- Infrastructure implements Application abstractions.
- API composes Application and Infrastructure.
- Use a direct application service in the initial baseline. Do not introduce CQRS without an approved task and ADR.
