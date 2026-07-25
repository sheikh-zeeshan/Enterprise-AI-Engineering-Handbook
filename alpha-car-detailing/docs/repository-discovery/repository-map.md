# Repository Map

- `/src/BuildingBlocks`: small shared domain primitives.
- `/src/Services/Booking`: Booking API, Application, Domain, and Infrastructure projects.
- `/tests/Services/Booking`: domain and API integration tests.
- `/instructions`: persistent repository engineering standards.
- `/skills`: reusable engineering procedures.
- `/scripts`: build, discovery, instruction, and Skill validation scripts.
- `/docs`: business, architecture, ADR, and discovery knowledge.

Dependency direction: API → Application and Infrastructure; Infrastructure → Application and Domain; Application → Domain; Domain → BuildingBlocks.Domain.
