# Chapter 20 — Metrics  

**Part III — Harness Engineering**  
**Target file:** `book/20-metrics.md`

The chapter should follow the handbook’s frozen chapter structure, remain Claude-first while keeping the engineering principles vendor-neutral, and continue using Alpha Car Detailing as the enterprise running example. The Harness is already defined as responsible for executing prompts and agents, validating and evaluating work, recording metrics, and storing iteration history, so this chapter should treat metrics as a first-class Harness capability rather than an external reporting concern. AI development handbook

---

## 1. Story-Driven Opening — Faster Is Not Always Better

- Introduce an Alpha Car Detailing corporate fleet booking change processed through the Harness.
- Compare two weeks of Harness execution:
  - Week A completes more implementation runs.
  - Week B completes fewer runs but produces fewer validation failures, retries, review findings, and human interventions.
- Present the leadership question:
  - Which week represents better Harness performance?
- Show why raw throughput alone can produce misleading conclusions.
- Establish the need to measure:
  - Productivity.
  - Quality.
  - Reliability.
  - Efficiency.
  - Cost.
  - Human involvement.
  - Engineering outcomes.
- Introduce the chapter’s central principle:

> **Metrics should measure both productivity and quality. A faster Harness is not successful if it produces more failures, rework, security issues, or poor outcomes.**

- Connect Metrics with the previous Population chapter:
  - Population defines the units of work.
  - Runs process those units.
  - Metrics aggregate evidence about how effectively those runs perform.

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Explain what Harness Metrics are.
- Distinguish productivity metrics from quality metrics.
- Design metrics around Population items and Harness runs.
- Measure throughput, success, failure, retries, and exhaustion.
- Measure Reviewer, Validator, and Evaluator outcomes.
- Measure gate performance and end-to-end run duration.
- Track human intervention and approval latency.
- Measure cost, token consumption, and agent utilization.
- Identify stale and stuck work.
- Define meaningful KPIs and baselines.
- Build dashboards and alerts around Harness execution.
- Assign metric ownership.
- Preserve metric auditability.
- Avoid vanity metrics and misleading optimization.
- Use metrics as feedback for improving Harness behavior.

---

# 3. Background — From Execution Logs to Engineering Evidence

### 3.1 Harnesses Produce More Than Code

Explain that every Harness execution produces evidence:

- Population item selected.
- Run created.
- Agent started.
- Prompt executed.
- Files changed.
- Reviewer findings produced.
- Validation gates executed.
- Evaluator outcome generated.
- Retries performed.
- Human approvals requested.
- Final outcome recorded.

### 3.2 Logs Are Not Metrics

Differentiate:

**Logs**

- Explain individual events.

**Metrics**

- Quantify behavior across events and runs.

**Traces**

- Explain execution flow.

**Artifacts**

- Preserve implementation and validation evidence.

### 3.3 Metrics as Operational Evidence

Explain how metrics answer questions such as:

- Are more Population items being completed?
- Are success rates improving?
- Which gates consume most execution time?
- Which agents cause repeated retries?
- Where does human intervention occur most often?
- Are implementation costs increasing?
- Are failures concentrated around certain task types?
- Is Harness reliability improving over time?

---

# 4. What Harness Metrics Are

Define Harness Metrics as:

> **Structured, measurable evidence collected from Harness execution to evaluate productivity, quality, reliability, efficiency, cost, and engineering outcomes.**

Explain metric dimensions including:

- Population.
- Run.
- Task.
- Agent.
- Gate.
- Repository.
- Branch.
- Skill.
- Prompt.
- Outcome.
- Time period.

### 4.1 Metric Categories

Introduce the major categories:

- Volume metrics.
- Productivity metrics.
- Quality metrics.
- Reliability metrics.
- Timing metrics.
- Rework metrics.
- Cost metrics.
- Agent metrics.
- Human interaction metrics.
- Governance metrics.

---

# 5. Why Metrics Matter

Cover how metrics support:

### Engineering Teams

- Detect regressions.
- Find bottlenecks.
- Improve skills and prompts.
- Improve validation gates.
- Reduce repeated failures.

### Architects

- Evaluate Harness architecture.
- Identify weak automation boundaries.
- Detect architecture-related rework.

### Engineering Managers

- Understand productivity.
- Understand human effort.
- Evaluate adoption and effectiveness.

### Platform Teams

- Monitor Harness reliability.
- Capacity-plan infrastructure.
- Analyze execution cost.

### Governance Teams

- Track approvals.
- Maintain traceability.
- Detect risky automation behavior.

---

# 6. Productivity Versus Quality

This should be one of the chapter’s major conceptual sections.

### 6.1 Productivity Metrics

Examples:

- Population items processed.
- Runs completed.
- Throughput.
- Mean execution time.
- Automation percentage.
- Agent utilization.

### 6.2 Quality Metrics

Examples:

- Validation pass rate.
- Reviewer findings.
- Evaluator outcomes.
- Rework rate.
- Security findings.
- Failed deployments.
- Human rejection rate.

### 6.3 Why Productivity Alone Is Dangerous

Example:

```text
Week 1
100 runs
85 successful
15 failed

Week 2
150 runs
95 successful
55 failed
```

Discuss why 50% higher throughput does not necessarily mean improvement.

### Diagram — Quality Versus Productivity

```text
                 Higher Quality
                       ↑
                       │
        Conservative   │   Desired
        but Reliable   │   Harness
                       │
───────────────────────┼──────────────→ Higher Productivity
                       │
        Weak Harness   │   Fast but
                       │   Unreliable
                       │
```

Explain that the desired Harness improves both dimensions rather than optimizing one at the expense of the other.

---

# 7. Population Metrics

Connect directly to Chapter 19.

Population metrics should describe the work available to the Harness.

Examples:

- Total Population size.
- Pending items.
- Running items.
- Completed items.
- Failed items.
- Exhausted items.
- Blocked items.
- Stale items.
- Items requiring human intervention.

Example:

```text
Corporate Fleet Booking Population

Total       120
Pending      18
Running       6
Succeeded    82
Failed        8
Exhausted     4
Blocked       2
```

### Diagram — Population-to-Metrics Relationship

```text
Population
    │
    ├── Pending
    ├── Running
    ├── Completed
    ├── Failed
    └── Exhausted
          │
          ▼
       Run Data
          │
          ▼
    Metric Aggregation
          │
          ▼
   KPI / Dashboard / Alert
```

---

# 8. Throughput

Define throughput as completed work over a defined time period.

Examples:

```text
Runs completed / hour
Population items completed / day
PRs produced / week
```

Discuss:

- Raw throughput.
- Successful throughput.
- Validated throughput.
- Approved throughput.

Important distinction:

```text
Execution Throughput
      ≠
Successful Engineering Throughput
```

### Alpha Car Detailing Example

Measure fleet-booking implementation items completed per day.

---

# 9. Success and Failure Rates

Define:

```text
Success Rate =
Successful Runs / Completed Runs
```

```text
Failure Rate =
Failed Runs / Completed Runs
```

Discuss what constitutes success.

Potential stages:

- Agent execution success.
- Build success.
- Validation success.
- Evaluation success.
- Human approval.
- Merge success.

Explain why the Harness should avoid treating:

```text
Agent finished
```

as equivalent to:

```text
Engineering task succeeded
```

---

# 10. Exhaustion Rate

Define exhaustion as reaching the Harness retry or recovery limit without achieving an acceptable outcome.

```text
Exhaustion Rate =
Exhausted Runs / Total Runs
```

Discuss:

- Why exhaustion matters.
- Difference between failure and exhaustion.
- What high exhaustion indicates:
  - Poor prompts.
  - Missing skills.
  - Repository misunderstanding.
  - Weak recovery strategy.
  - Impossible acceptance criteria.
  - Environment failures.

### Alpha Car Detailing Example

A fleet-booking API implementation repeatedly fails a database migration gate and eventually becomes exhausted.

---

# 11. Retry Rate

Measure:

- Runs requiring retry.
- Average retries per run.
- Maximum retry count.
- Retry success rate.
- Retry cause.

Possible retry categories:

- Build failure.
- Test failure.
- Reviewer feedback.
- Validator failure.
- Evaluator failure.
- Tool failure.
- Environment failure.

Discuss why successful runs with many retries may still represent poor Harness performance.

---

# 12. Validation Pass Rate

Connect directly with the Validator Agent.

Metrics may include:

- Build gate pass rate.
- Unit-test pass rate.
- Integration-test pass rate.
- Architecture gate pass rate.
- Security gate pass rate.
- API contract gate pass rate.
- Database migration gate pass rate.

Formula:

```text
Validation Pass Rate =
Passed Gate Executions / Total Gate Executions
```

Also distinguish:

- First-pass success.
- Eventual success after retries.

---

# 13. Review Findings

Connect metrics to the Reviewer Agent.

Track:

- Findings per run.
- Findings by severity.
- Findings by category.
- Repeated findings.
- Findings accepted.
- Findings rejected.
- Findings causing rework.

Possible categories:

- Architecture.
- Maintainability.
- Security.
- Testing.
- Observability.
- Contract impact.
- Persistence.
- Configuration.

Explain why fewer findings do not automatically mean better quality: weak reviewing can also reduce finding counts.

---

# 14. Evaluation Outcomes

Connect with Chapter 18.

Track outcomes such as:

- Pass.
- Fail.
- Partial.
- Inconclusive.

Measure:

```text
Evaluation Pass Rate
Partial Outcome Rate
Inconclusive Rate
```

Analyze discrepancies such as:

```text
Validator = Pass
Evaluator = Fail
```

Explain why this is valuable evidence: technically correct implementation may still fail the intended business outcome.

---

# 15. Gate Duration

Measure each deterministic gate independently.

Examples:

```text
Build              42 sec
Unit Tests         71 sec
Architecture Scan  12 sec
Security Scan      96 sec
Integration Tests  185 sec
```

Track:

- Average.
- Median.
- P95.
- Maximum.
- Timeout rate.

Discuss how gate duration helps identify Harness bottlenecks without weakening validation.

---

# 16. End-to-End Execution Time

Define execution time from:

```text
Population item selected
        ↓
Run initialized
        ↓
Implementation
        ↓
Review
        ↓
Validation
        ↓
Evaluation
        ↓
Human approval
        ↓
Complete
```

Distinguish:

- Machine execution time.
- Agent processing time.
- Queue time.
- Gate time.
- Human wait time.
- Total elapsed time.

### Diagram — Run Lifecycle Metrics

```text
Population
    │
    ▼
Run Created
    │
    ├── Queue Time
    ▼
Lead Agent
    │
    ├── Planning Time
    ▼
Developer
    │
    ├── Implementation Time
    ▼
Reviewer
    │
    ├── Review Time
    ▼
Validator
    │
    ├── Gate Duration
    ▼
Evaluator
    │
    ├── Evaluation Time
    ▼
Human Approval
    │
    ├── Approval Latency
    ▼
Completed
```

---

# 17. Rework Metrics

Define rework as effort required after an implementation was initially produced.

Track:

- Number of rework cycles.
- Files changed during rework.
- Reviewer-triggered rework.
- Validator-triggered rework.
- Evaluator-triggered rework.
- Human-triggered rework.

Discuss:

```text
High Throughput + High Rework
```

as a warning signal.

Introduce:

```text
First-Pass Success Rate
```

as an especially valuable Harness KPI.

---

# 18. Cost and Token Usage

Track cost across:

- Lead Agent.
- Developer Agent.
- Reviewer Agent.
- Validator Agent where AI reasoning is involved.
- Evaluator Agent.
- Retry cycles.

Possible metrics:

```text
Tokens per run
Tokens per successful run
Cost per Population item
Cost per approved implementation
Cost per retry
```

Explain why cost should be normalized by outcome.

Compare:

```text
Cost per Run
```

versus:

```text
Cost per Successful Validated Outcome
```

---

# 19. Agent Utilization

Measure how Harness work is distributed across agents.

Metrics may include:

- Agent invocation count.
- Agent execution duration.
- Agent idle time.
- Retry count by agent.
- Agent failure rate.
- Cost by agent.
- Token consumption by agent.

Discuss the danger of treating high utilization as automatically positive.

High utilization can indicate:

- Useful automation.

or:

- Excessive agent orchestration.

---

# 20. Stale and Stuck Work

Define:

### Stale Work

Work that has not progressed within an expected threshold.

### Stuck Work

Work that appears active but is no longer making meaningful progress.

Potential metrics:

- Stale run count.
- Stuck run count.
- Average stale duration.
- Recovery success rate.
- Runs automatically recovered.
- Runs requiring human intervention.

Discuss heartbeat-based detection.

---

# 21. Human Intervention

Track where automation requires a person.

Examples:

- Clarification.
- Architecture decision.
- Security approval.
- Acceptance-criteria clarification.
- Manual recovery.
- Final approval.

Metrics:

```text
Human Intervention Rate
Interventions per Run
Interventions by Reason
```

Explain that the goal should not automatically be zero human intervention.

Some decisions should intentionally remain human-controlled.

---

# 22. Approval Latency

Measure time between:

```text
Harness ready for approval
```

and:

```text
Human decision
```

Track:

- Median approval time.
- P95 approval time.
- Rejection rate.
- Approval after rework.
- Approval by work category.

Discuss why human latency should be distinguished from Harness execution performance.

---

# 23. Reliability Metrics

Cover Harness operational health.

Examples:

- Run completion rate.
- Harness crash rate.
- Tool invocation failure rate.
- Agent timeout rate.
- Infrastructure failure rate.
- Checkpoint recovery rate.
- Retry recovery rate.
- Unrecoverable-run rate.

Introduce reliability measures such as:

```text
Successful Runs / Attempted Runs
```

and possibly:

```text
Mean Time to Recovery
```

where useful.

---

# 24. Trends and Baselines

Explain why isolated metric values are weak evidence.

Use:

- Daily baselines.
- Weekly baselines.
- Repository baselines.
- Work-type baselines.
- Agent baselines.

Compare:

```text
Current Value
vs
Historical Baseline
vs
Expected Range
```

Example:

A validation gate taking 120 seconds may be normal for one repository but anomalous for another.

---

# 25. KPI Design

Explain the distinction between:

- Raw metric.
- Derived metric.
- KPI.

Example:

```text
Raw Metric:
42 failed validation executions

Derived Metric:
Validation Pass Rate = 91%

KPI:
First-Pass Validated Completion Rate = 78%
```

### Recommended Harness KPI Categories

#### Productivity

- Successful throughput.
- Completed Population items.

#### Quality

- First-pass validation rate.
- Evaluation pass rate.
- Rework rate.

#### Reliability

- Exhaustion rate.
- Stuck-run rate.

#### Efficiency

- Median run duration.
- Cost per successful outcome.

#### Human Dependency

- Human intervention rate.
- Approval latency.

---

# 26. Composite Metrics and Guardrails

Explain why KPIs should often be interpreted together.

Example:

```text
Increase throughput
WHILE
Validation failure rate does not increase
AND
Rework rate does not increase
AND
Cost per success remains within threshold
```

Introduce metric guardrails.

Avoid building one opaque “Harness score” that hides important trade-offs.

---

# 27. Dashboards

Explain the purpose of dashboards:

- Operational visibility.
- Engineering trends.
- Management visibility.
- Bottleneck detection.

Suggested dashboard sections:

### Population

- Total.
- Pending.
- Running.
- Passed.
- Failed.
- Exhausted.

### Quality

- Validation pass rate.
- Reviewer findings.
- Evaluation outcomes.
- Rework.

### Performance

- Throughput.
- Run duration.
- Gate duration.

### Reliability

- Stale.
- Stuck.
- Retry.
- Exhaustion.

### Economics

- Tokens.
- Cost per run.
- Cost per success.

### Human Workflow

- Human intervention.
- Approval latency.

---

# 28. Metrics Dashboard Architecture

### Diagram — Dashboard Architecture

```text
Harness Execution
       │
       ├── Run Events
       ├── Agent Events
       ├── Gate Results
       ├── Review Results
       ├── Evaluation Results
       ├── Cost / Token Data
       └── Human Decisions
               │
               ▼
        Metrics Collection
               │
               ▼
        Metrics Repository
               │
      ┌────────┼─────────┐
      ▼        ▼         ▼
 Dashboard   Alerts    Reports
      │
      ▼
Engineering Decisions
```

Discuss separation between:

- Operational store.
- Execution history.
- Metric aggregation.
- Dashboard queries.

---

# 29. Alerts

Explain that not every metric requires an alert.

Appropriate examples:

- Exhaustion rate exceeds threshold.
- Stuck run detected.
- Validation pass rate falls below baseline.
- Gate duration spikes.
- Cost per successful run increases sharply.
- Human approval backlog grows.
- Harness failure rate exceeds threshold.

Discuss:

- Warning.
- Critical.
- Informational.

Warn against alert fatigue.

---

# 30. Metric Ownership

Define responsibilities.

Possible ownership model:

### Harness Platform Team

Owns:

- Reliability.
- Run duration.
- Infrastructure.
- Agent execution.

### Development Team

Owns:

- Implementation outcomes.
- Rework.
- Validation trends.

### Architecture Team

Owns:

- Architecture findings.
- Architecture-related gates.

### Security Team

Owns:

- Security findings.
- Security-related thresholds.

### Engineering Leadership

Owns:

- KPI interpretation.
- Organizational objectives.

Emphasize that every important KPI should have a clear owner.

---

# 31. Auditability

Metrics should be traceable back to evidence.

A dashboard number such as:

```text
Validation Pass Rate = 94%
```

should allow investigation down to:

```text
Metric
  ↓
Population Item
  ↓
Harness Run
  ↓
Gate Execution
  ↓
Result
  ↓
Log / Artifact
```

Discuss:

- Run IDs.
- Population IDs.
- Correlation IDs.
- Agent identifiers.
- Gate results.
- Timestamps.
- Metric versions.

---

# 32. Avoiding Vanity Metrics

Examples of misleading metrics:

- Number of prompts executed.
- Number of agent calls.
- Number of generated files.
- Lines of generated code.
- Tokens consumed.
- Number of commits.
- Number of PRs.

Explain that these may be useful operational counters but should not be presented as engineering success.

Example:

```text
10,000 lines generated
```

says little about:

- Correctness.
- Maintainability.
- Business value.
- Security.
- Architecture compliance.

---

# 33. Metrics Inside the Harness

Discuss where instrumentation belongs.

Possible conceptual structure:

```text
harness/
├── execution/
├── population/
├── roles/
├── gates/
├── metrics/
│   ├── collectors/
│   ├── aggregators/
│   ├── exporters/
│   └── definitions/
└── reports/
```

The agreed Harness architecture already identifies `Population/` and `Metrics/` as first-class components, reinforcing that measurement belongs inside the Harness architecture rather than being added as an afterthought. AI development handbook

Cover instrumentation points:

- Run start.
- Run completion.
- Agent start/end.
- Gate start/end.
- Retry.
- Review completion.
- Validation completion.
- Evaluation completion.
- Human decision.
- Run exhaustion.

---

# 34. Harness Metrics Flow

### Diagram — Harness Metrics Flow

```text
Population Item
      │
      ▼
Harness Run
      │
      ├── Lead Agent
      │
      ├── Developer Agent
      │
      ├── Reviewer Agent
      │
      ├── Validator Agent
      │
      └── Evaluator Agent
      │
      ▼
Execution Events
      │
      ▼
Metrics Collector
      │
      ▼
Metrics Store
      │
      ├── Dashboard
      ├── Alerts
      ├── Reports
      └── Trend Analysis
```

---

# 35. Feedback Loop from Metrics to Harness Improvement

Metrics should support improvement, not autonomous uncontrolled changes.

### Diagram — Improvement Feedback Loop

```text
Harness Execution
       │
       ▼
Metrics
       │
       ▼
Trend Analysis
       │
       ▼
Problem Identified
       │
       ▼
Improvement Proposal
       │
       ├── Prompt Change
       ├── Skill Change
       ├── Gate Change
       ├── Agent Change
       └── Harness Change
       │
       ▼
Human Review / Approval
       │
       ▼
Controlled Update
       │
       ▼
Next Harness Runs
```

Reinforce the handbook’s existing self-learning rule: the Harness may recommend improvements, but should not silently change governed engineering standards; human approval remains mandatory. AI development handbook

---

# 36. Hands-on Example — Measuring Alpha Car Detailing Fleet Booking Runs

Build a complete scenario around a Population containing corporate fleet booking tasks.

Example Population:

```text
FLEET-101 Create corporate fleet booking command
FLEET-102 Add booking validation
FLEET-103 Add fleet booking API
FLEET-104 Add booking integration event
FLEET-105 Add persistence
FLEET-106 Add tests
```

Track each run:

```text
Run ID
Population Item
Start Time
End Time
Status
Retries
Review Findings
Validation Result
Evaluation Result
Token Usage
Human Intervention
Approval Time
```

Derive:

- Population completion rate.
- Successful throughput.
- Retry rate.
- Exhaustion rate.
- First-pass success rate.
- Median gate duration.
- End-to-end execution time.
- Cost per successful result.
- Human intervention rate.

---

# 37. Detailed Alpha Car Detailing Scenario

Compare two Harness configurations.

### Configuration A

- Faster Developer Agent.
- Minimal review.
- Fewer gates.

### Configuration B

- Slightly slower.
- Full Reviewer pass.
- Deterministic validation.
- Evaluation.

Compare metrics:

| Metric | A | B |
| --- | ---: | ---: |
| Runs/day | High | Moderate |
| First-pass success | Lower | Higher |
| Rework | Higher | Lower |
| Validation failures | Higher | Lower |
| Human intervention | Higher | Lower |
| Cost/run | Lower | Higher |
| Cost/success | Higher | Lower |

Use the example to reinforce:

> Optimize the complete engineering outcome, not one isolated number.

---

# 38. Claude Code Example

Show how a Claude-first Harness could produce structured run metrics.

Discuss capturing:

- Session/run identifier.
- Role.
- Start/end timestamps.
- Token usage where available.
- Tool calls.
- Result.
- Retry reason.
- Validation evidence.

Keep implementation principles vendor-neutral.

The handbook’s agreed approach is Claude-first, while comparisons with GitHub Copilot and Codex should focus only on meaningful differences. AI development handbook

---

# 39. GitHub Copilot Comparison

Discuss considerations such as:

- Capturing Harness-owned metrics independently of Copilot.
- Avoiding dependence on vendor-specific telemetry.
- Recording run-level events outside the coding assistant.
- Correlating Copilot-assisted work with Harness execution.
- Measuring outcomes rather than suggestion counts.

Emphasize that coding-assistant activity metrics should not replace engineering outcome metrics.

---

# 40. Codex Comparison

Discuss:

- Run/session telemetry.
- Tool invocation tracking.
- Execution duration.
- Outcome collection.
- Harness-controlled metric recording.
- Cost/token measurements where exposed.

Reinforce portability:

```text
Harness Metric Model
        │
        ├── Claude
        ├── GitHub Copilot
        ├── Codex
        └── Future Agent
```

---

# 41. Best Practices

Cover practices such as:

- Measure outcomes, not activity.
- Separate productivity from quality.
- Maintain first-pass success metrics.
- Track retries and exhaustion.
- Measure gate duration separately.
- Normalize cost by successful outcome.
- Preserve Population-to-run traceability.
- Maintain historical baselines.
- Use percentiles for latency.
- Tag metrics with meaningful dimensions.
- Define KPI owners.
- Version metric definitions.
- Review thresholds periodically.
- Keep dashboards role-specific.
- Correlate metrics rather than interpreting them independently.
- Keep measurement vendor-neutral.
- Preserve evidence behind aggregates.

---

# 42. Anti-patterns

Cover:

### Measuring Only Throughput

```text
More runs = success
```

### Treating Agent Completion as Engineering Success

### Ignoring Failed and Exhausted Runs

### Optimizing for Token Reduction at the Expense of Quality

### Counting Lines of Generated Code

### Measuring Reviewer Finding Count Without Context

### Hiding Retry Cost

### Mixing Human Approval Time with Agent Execution Time

### Using Average Duration Without Percentiles

### Creating Too Many Metrics

### Dashboard Without Ownership

### Alerts Without Actionability

### Metrics Without Historical Baselines

### Changing Metric Definitions Without Versioning

### Automatically Changing Harness Behavior Based on Metrics

---

# 43. Architect’s Notes

Highlight architectural principles such as:

> **Metrics are part of the Harness control plane.**

> **A successful AI engineering metric should connect activity to an engineering outcome.**

> **Population provides the denominator required to understand Harness performance correctly.**

> **Every aggregate metric should be traceable to run-level evidence.**

> **Metric collection must not become tightly coupled to one AI vendor.**

> **Optimization should occur across quality, productivity, reliability, and cost—not one dimension.**

---

# 44. Enterprise Tips

Include guidance such as:

- Start with a small KPI set.
- Establish baselines before setting targets.
- Separate platform metrics from engineering outcome metrics.
- Add repository and work-type dimensions.
- Track first-pass success aggressively.
- Investigate retry causes, not only retry counts.
- Keep cost metrics visible but contextual.
- Track human intervention reasons.
- Include security and quality metrics in executive reporting.
- Preserve long-term trend data.
- Use dashboards to trigger investigation rather than automatically assign blame.

---

# 45. Decision Points

Questions architects should answer:

- What defines a successful run?
- What defines a failed run?
- When is a run considered exhausted?
- Which metrics are collected per run?
- Which are aggregated by Population?
- What constitutes first-pass success?
- How are retries classified?
- How are human interventions recorded?
- Which gates require timing metrics?
- How is token usage captured?
- How is cost normalized?
- Which metrics become KPIs?
- What baseline period is appropriate?
- Which alerts are actionable?
- Who owns each metric?
- How long should metric history be retained?
- How are metrics linked to run evidence?
- How are metric definitions versioned?
- Which vendor-specific metrics should be avoided?

---

# 46. Exercises

### Exercise 1 — Define Core Harness Metrics

Define ten metrics for an Alpha Car Detailing Harness.

### Exercise 2 — Productivity Versus Quality

Given two sets of run results, determine which Harness configuration performed better.

### Exercise 3 — Calculate Success Rates

Calculate:

- Success rate.
- Failure rate.
- Exhaustion rate.
- Retry rate.

### Exercise 4 — Design Gate Metrics

Define measurements for:

- Build.
- Unit tests.
- Integration tests.
- Architecture validation.
- Security validation.

### Exercise 5 — Measure Rework

Design a method for recording Reviewer-, Validator-, and Evaluator-triggered rework.

### Exercise 6 — Design a KPI Dashboard

Create sections for:

- Population.
- Productivity.
- Quality.
- Reliability.
- Cost.
- Human intervention.

### Exercise 7 — Identify Vanity Metrics

Review a fictional Harness dashboard and identify misleading metrics.

### Exercise 8 — Design Alerts

Define warning and critical thresholds for:

- Exhaustion.
- Stuck work.
- Validation failures.
- Gate duration.
- Cost spikes.

### Exercise 9 — Build Traceability

Design the relationship:

```text
Metric
→ Population Item
→ Run
→ Gate
→ Evidence
```

### Exercise 10 — Analyze Alpha Car Detailing Trends

Given four weeks of fleet-booking Harness data, identify:

- Improvement.
- Regression.
- Bottlenecks.
- Unexpected cost growth.
- Quality problems.

---

# 47. Interview Questions

Include questions such as:

1. What are Harness Metrics?
2. Why should productivity and quality be measured together?
3. What is the relationship between Population and Metrics?
4. How do you calculate Harness throughput?
5. What is first-pass success rate?
6. What is exhaustion rate?
7. Why is retry rate important?
8. How would you measure Validator effectiveness?
9. How would Reviewer findings be represented as metrics?
10. How are Evaluator outcomes different from validation metrics?
11. Why should gate duration be measured independently?
12. What is the difference between execution time and approval latency?
13. How would you measure rework?
14. Why is cost per successful outcome more useful than cost per run?
15. What is agent utilization?
16. How should stale and stuck runs be detected?
17. Why is zero human intervention not necessarily desirable?
18. What makes a good KPI?
19. What is a vanity metric?
20. Why are historical baselines important?
21. How do dashboards differ from alerts?
22. How should metric ownership work?
23. How do you make Harness metrics auditable?
24. Why should Harness metrics remain vendor-neutral?
25. How can metrics improve the Harness without allowing uncontrolled self-modification?

---

# 48. Chapter Summary

Summarize the major principles:

- Metrics convert Harness activity into measurable engineering evidence.
- Population defines what is being measured.
- Throughput alone does not represent success.
- Productivity must be balanced with quality.
- Validation, review, and evaluation produce important quality signals.
- Retry and exhaustion reveal hidden inefficiencies.
- Gate timing reveals execution bottlenecks.
- First-pass success exposes rework.
- Cost should be measured against successful outcomes.
- Human intervention and approval latency must be visible.
- Reliability metrics reveal Harness operational health.
- Baselines and trends provide context.
- KPIs should represent meaningful engineering outcomes.
- Dashboards provide visibility.
- Alerts should be actionable.
- Metrics require ownership and auditability.
- Vanity metrics should not drive engineering decisions.
- Metrics should create a governed feedback loop for Harness improvement.

End with the central principle:

> **A Harness should not be judged by how much AI activity it produces. It should be judged by how reliably, efficiently, and safely it produces high-quality engineering outcomes.**

---

# 49. Further Reading

Organize references around:

- Software engineering metrics.
- DevOps performance metrics.
- DORA metrics.
- Observability and telemetry.
- OpenTelemetry.
- SRE indicators and objectives.
- CI/CD analytics.
- Software quality measurement.
- AI system observability.
- AI cost monitoring.
- Human-in-the-loop systems.
- Engineering productivity measurement.
- Statistical baselines and percentile analysis.

Keep this section vendor-neutral while including platform-specific documentation where implementation detail is useful.

---

## Diagram Inventory

The completed chapter should contain at least these six diagrams:

1. **Harness Metrics Flow**
2. **Population-to-Metrics Relationship**
3. **Run Lifecycle Metrics**
4. **Quality Versus Productivity**
5. **Metrics Dashboard Architecture**
6. **Metrics-to-Harness Improvement Feedback Loop**

These diagrams align with the handbook’s frozen requirement for professional diagrams, architecture discussion, hands-on examples, platform comparisons, best practices, anti-patterns, decision points, exercises, interview questions, summary, and further reading. AI development handbook

**Chapter 20 outline is complete.**
