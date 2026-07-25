# Architecture Vision

The sample begins with a small Booking service and four boundaries: API, Application, Domain, and Infrastructure.

The Application layer uses a direct service-oriented use case. CQRS is deliberately deferred until the sample works and the added complexity can be justified. Domain logic remains independent of persistence and HTTP concerns. Infrastructure uses EF Core with SQLite for local development.
