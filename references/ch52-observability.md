# Chapter 52 — Source and verification record

Source review date: 21 November 2026.

The manuscript applies the approved `planning/chapter-52-outline.md`, the repository editorial guide and frozen template, Chapters 46–51, and the current Booking create/read source. The reference architecture remains distinct from the runnable pending-booking baseline.

## Primary sources

| Source | Use in the chapter |
| --- | --- |
| [Microsoft .NET observability](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel) | Complementary signals and .NET instrumentation APIs. |
| [OpenTelemetry .NET instrumentation](https://opentelemetry.io/docs/languages/dotnet/instrumentation/) | SDK, explicit activities, and integration. |
| [OpenTelemetry .NET exporters](https://opentelemetry.io/docs/languages/dotnet/exporters/) | OTLP configuration and provider registration. |
| [OpenTelemetry log correlation](https://opentelemetry.io/docs/languages/dotnet/logs/correlation/) | Active Activity context in diagnostic log records. |
| [OpenTelemetry metric practices](https://opentelemetry.io/docs/languages/dotnet/metrics/best-practices/) | Instrument lifetime, attributes, and cardinality. |
| [W3C Trace Context](https://www.w3.org/TR/trace-context/) | Trace headers, identifiers, and propagation behavior. |
| [OpenTelemetry messaging conventions](https://opentelemetry.io/docs/specs/semconv/messaging/messaging-spans/) | Creation context, processing, parentage, and links. Conventions must be pinned for the selected instrumentation; the chapter does not claim current development conventions are universally emitted by version 1.14.0 libraries. |
| [Collector configuration](https://opentelemetry.io/docs/collector/configuration/) and [resiliency](https://opentelemetry.io/docs/collector/resiliency/) | Collection, queues, persistent buffering, and bounded loss. |
| [OpenTelemetry sampling](https://opentelemetry.io/docs/concepts/sampling/) | Head and tail sampling and their evidence limits. |
| [Serilog](https://serilog.net/) | Structured-event logging direction. |
| [ASP.NET Core health checks](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-10.0) | Check predicates and separate liveness/readiness endpoints. |
| [Azure Event Hubs monitoring reference](https://learn.microsoft.com/en-us/azure/event-hubs/monitor-event-hubs-reference) | Delivery lag versus checkpoint and business completion. |
| [Kubernetes observability](https://kubernetes.io/docs/concepts/cluster-administration/observability/) and [probes](https://kubernetes.io/docs/concepts/workloads/pods/probes/) | Platform signals and different probe consequences. |
| [SRE Workbook SLO definitions](https://sre.google/workbook/implementing-slos/) and [alerting](https://sre.google/workbook/alerting-on-slos/) | Reliability populations, error budgets, and multiwindow alerts. Numerical Alpha Car Detailing values remain laboratory proposals. |
| [OWASP Logging Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Logging_Cheat_Sheet.html) | Sensitive-field exclusion, sanitization, access, and failure testing. |
| [Claude Code project guidance](https://code.claude.com/docs/en/memory) | Product-specific instruction context. |
| [GitHub Copilot repository instructions](https://docs.github.com/en/copilot/customizing-copilot/adding-repository-custom-instructions-for-github-copilot) | Repository-wide instruction convention and execution-surface qualifications. |
| [Codex AGENTS.md guidance](https://learn.chatgpt.com/docs/agent-configuration/agents-md) | Product-specific repository instruction convention; local installed Codex README was inspected before the documentation lookup. |

## Manuscript validation

- Nineteen frozen sections appear in order, with one chapter title and the required completion status.
- Six Mermaid sources parse and render successfully; matching editable sources and SVG exports accompany the manuscript.
- The five complete C# listings and the composition fragment compile together in an isolated .NET 10 verification host using the actual Booking Application project and endpoint source. The fragment is incorporated into the verification host rather than compiled as a standalone file.
- Compilation used .NET SDK 10.0.404 and the five listed OpenTelemetry package references at 1.14.0; single-node build completed with zero warnings and zero errors.
- A controlled in-memory repository and ASP.NET Core TestServer verify pending creation, existing read behavior, missing-resource response, and domain rejection without changing the companion implementation.
- Seventeen fixture checks passed, covering absent tracing, regenerated public correlation identity, HTTP context continuation, structured correlation scope, truthful spans, bounded counter/histogram attributes, cancellation, failure classification, custom sensitive-field exclusion, stored messaging context, attempt identity, and invalid context.
- The HTTP fixture succeeds while the configured OTLP endpoint is unavailable. This demonstrates bounded behavior for that fixture; it does not establish production throughput, saturation limits, or complete exporter-loss detection.
- JSON examples parse and `git diff --check` passes.

## Verification limits

The verification does not claim a production Collector/backend, SQLite or SQL Server telemetry integration, real-broker propagation, inbox/outbox durability, provider reconciliation, cluster deployment, production alert delivery, or approved SLO targets. Those remain explicitly identified implementation and integration exercises. No production state or companion application source was changed by manuscript preparation.
