# Chapter 41 — Skill Evolution  

**Part VI — Self-Learning AI Systems**  
**Target file:** `book/41-skill-evolution.md`

The chapter should preserve the handbook’s frozen distinction that a **Skill is a reusable engineering workflow**, while the Harness orchestrates execution and learning around those workflows. The self-learning model also requires that the Harness **never silently changes governed standards or Skills; it recommends improvements and requires human approval**. AI development handbook AI development handbook

The outline follows the frozen chapter structure established for the handbook. AI development handbook

---

## 1. Story-Driven Opening — When a Working Skill Is No Longer Good Enough

### 1.1 The Alpha Car Detailing scenario

- Alpha Car Detailing has successfully used reusable Skills for months.
- The `create-api` Skill has generated multiple APIs.
- The `add-integration-event` Skill has produced several event publishers.
- The `transactional-outbox` Skill has been reused across services.
- The `idempotent-consumer` Skill has helped developers build reliable consumers.
- EF Core persistence, authorization, observability, and testing Skills are now part of normal engineering work.

### 1.2 The pattern begins to emerge

- Individual implementations mostly succeed.
- However, review comments repeatedly identify similar omissions.
- Integration tests repeatedly expose the same weaknesses.
- Pull requests repeatedly receive similar corrections.
- Developers repeatedly add missing telemetry or authorization rules.
- Some generated implementations require more human correction than others.

### 1.3 The architectural question

- Should engineers manually update the Skill after every issue?
- Should the Harness automatically rewrite the Skill?
- How much evidence is sufficient before recommending improvement?
- Who decides whether a Skill should change?

### 1.4 Core lesson

- A reusable Skill must not remain permanently static.
- Repeated evidence can reveal weaknesses in a Skill.
- The Adaptive Harness can detect those patterns.
- The Harness can propose an improved Skill.
- The governed Skill changes only after testing, review, and human approval.

### 1.5 Transition from Chapter 40

- Chapter 40 examined learning from pull requests.
- Chapter 41 converts accumulated evidence into recommendations for improving reusable engineering workflows.
- Pull-request evidence becomes one input among several.
- Skill Evolution becomes the first major example of governed adaptive improvement.

---

# 2. Learning Objectives

After completing this chapter, readers should be able to:

- Define Skill Evolution in an Adaptive Harness.
- Explain why reusable Skills require controlled evolution.
- Identify valid learning signals for Skill improvement.
- Distinguish isolated failures from recurring Skill weaknesses.
- Measure Skill effectiveness.
- Detect incomplete, outdated, ambiguous, or overly broad Skills.
- Generate evidence-backed Skill improvement recommendations.
- Design Skill versioning strategies.
- Test Skill changes before adoption.
- Manage compatibility across repositories and Harness versions.
- Roll back problematic Skill versions.
- Define Skill ownership and approval responsibilities.
- Deprecate Skills safely.
- Evolve composed Skills without creating hidden coupling.
- Use Harness Memory to support Skill Evolution.
- Explain where Skill Evolution fits inside the Adaptive Harness.
- Prevent uncontrolled self-modification.
- Establish enterprise governance around Skill changes.

---

# 3. Background — From Reusable Skills to Adaptive Skills

## 3.1 Recap: What a Skill is

- A Skill represents a reusable engineering workflow.
- Skills describe how recurring engineering work should be performed.
- Examples:
  - Create API.
  - Add integration event.
  - Configure OpenTelemetry.
  - Implement transactional outbox.
  - Build idempotent consumers.
  - Add authorization.
  - Add persistence.
  - Create tests.

## 3.2 Skills versus neighboring concepts

Reinforce the frozen model:

```text
Instruction
    ↓
Repository-wide engineering expectations

Skill
    ↓
Reusable engineering workflow

Prompt
    ↓
Current task request

Harness
    ↓
Coordinates execution, evidence, validation, and learning
```

## 3.3 Why static Skills eventually become insufficient

- Architecture evolves.
- Framework versions change.
- Security expectations change.
- Testing standards improve.
- Production incidents expose missing safeguards.
- Reviewers discover recurring implementation gaps.
- New organizational patterns become preferred.
- Existing Skills may encode yesterday's best practice.

## 3.4 Static Skill versus evolving Skill

Compare:

**Static Skill**

- Authored once.
- Updated manually.
- Limited feedback loop.
- Improvement depends on engineers noticing patterns.

**Evolving Skill**

- Continuously observed.
- Measured using execution evidence.
- Weaknesses detected through repeated signals.
- Improvement candidates proposed automatically.
- Governed through human approval.

## 3.5 Skill Evolution is not autonomous rewriting

Clarify:

```text
Evidence → Detection → Recommendation → Testing → Approval → Versioned Skill
```

Not:

```text
Failure → AI silently edits Skill → New Skill becomes active
```

---

# 4. Concepts

## 4.1 What Skill Evolution is

Define Skill Evolution as:

> The governed process through which an Adaptive Harness uses accumulated engineering evidence to identify weaknesses in reusable Skills, propose improvements, validate those improvements, and promote approved versions.

Explain that Skill Evolution contains two distinct capabilities:

- **Learning**
  - Detecting patterns from evidence.
- **Governance**
  - Controlling whether proposed changes become authoritative.

---

## 4.2 Why Skills evolve

Cover common causes:

### Architecture evolution

- New architectural patterns.
- New boundaries.
- New infrastructure.
- New reliability requirements.

### Engineering maturity

- Stronger validation.
- Better testing practices.
- Improved observability.
- Security hardening.

### Repeated correction

- Developers repeatedly modifying generated code in the same way.

### Operational feedback

- Production failures reveal missing steps.

### Platform evolution

- .NET changes.
- EF Core changes.
- OpenTelemetry changes.
- Cloud platform changes.

### Organizational policy changes

- Authentication standards.
- Authorization policies.
- Logging requirements.
- compliance controls.

---

# 5. Learning Signals

## 5.1 Evidence sources

Skill Evolution should consume multiple evidence channels rather than relying on one source.

```text
Build Evidence
       +
Validation Evidence
       +
Review Findings
       +
PR Outcomes
       +
Failure History
       +
Human Feedback
       +
Evaluator Findings
       ↓
Skill Learning Signals
```

## 5.2 Builds

Examples:

- Compilation errors.
- Warnings.
- Dependency issues.
- Configuration errors.

## 5.3 Deterministic validation

- Unit test failures.
- Integration test failures.
- Architecture-test failures.
- Contract-validation failures.
- Security gate failures.
- Static-analysis failures.

## 5.4 Reviewer findings

Examples:

- Missing abstraction.
- Incorrect dependency direction.
- Poor error handling.
- Insufficient logging.
- Naming inconsistencies.
- Missing security controls.

## 5.5 Evaluator findings

- Implementation technically passes but does not satisfy the broader goal.
- Acceptance criteria only partially satisfied.
- Business behavior incomplete.

## 5.6 Pull-request evidence

Building directly on Chapter 40:

- Requested changes.
- Review comments.
- Approval patterns.
- Merge success.
- PR rejection.
- Human corrections before merge.

## 5.7 Operational evidence

Where governance permits:

- Production defects.
- Reliability incidents.
- Performance regressions.
- Telemetry gaps.

## 5.8 Human feedback

- Explicit developer feedback.
- Architect recommendations.
- Reviewer feedback.
- Skill ratings.
- "Generated result required heavy modification."

---

# 6. Repeated Failures as Evolution Signals

## 6.1 One failure is not necessarily a Skill defect

Differentiate:

- Task-specific failure.
- Repository-specific issue.
- Developer misuse.
- Environmental failure.
- Skill weakness.

## 6.2 Pattern detection

Example:

```text
Run 17 → Missing idempotency test
Run 31 → Missing idempotency test
Run 46 → Missing idempotency test
Run 53 → Reviewer requests idempotency test
Run 61 → PR requires same correction
```

Possible conclusion:

> The `idempotent-consumer` Skill may not adequately require idempotency verification.

## 6.3 Failure frequency

Potential indicators:

- Frequency.
- Severity.
- Recurrence.
- Similarity.
- Repository distribution.
- Human correction frequency.

## 6.4 Avoiding false learning

Discuss:

- Correlation versus causation.
- Repository-specific constraints.
- Temporary platform issues.
- Bad prompts.
- Poor execution by a specific agent.
- Invalid validation rules.

---

# 7. Review Findings as Skill Evidence

## 7.1 Repeated reviewer corrections

Examples:

- API Skill repeatedly omits cancellation tokens.
- Event Skill repeatedly omits correlation IDs.
- EF Core Skill repeatedly uses poor transaction boundaries.
- Authorization Skill repeatedly misses policy checks.

## 7.2 Structured findings

Recommend normalized categories:

```text
Architecture
Security
Reliability
Testing
Observability
Performance
Maintainability
Data
API Contract
Event Contract
```

## 7.3 From review comment to learning signal

Explain transformation:

```text
Natural-language review comment
        ↓
Classified finding
        ↓
Associated Skill
        ↓
Pattern aggregation
        ↓
Improvement candidate
```

---

# 8. Validation Results

## 8.1 Deterministic evidence as high-confidence signals

Explain why deterministic failures carry particular value.

## 8.2 Examples

### API Skill

- API contract validation repeatedly fails.

### Transactional outbox Skill

- Integration test reveals event not persisted atomically.

### EF Core Skill

- Migration checks identify incompatible schema changes.

### Authorization Skill

- Security tests reveal missing authorization requirement.

### OpenTelemetry Skill

- Observability checks reveal trace propagation failure.

## 8.3 Validation versus Skill diagnosis

Important distinction:

> Validation proves that an output violated a condition. It does not automatically prove that the Skill caused the violation.

---

# 9. Pull-Request Outcomes

## 9.1 Connecting Chapter 40 to Skill Evolution

Use:

```text
Skill execution
     ↓
Implementation
     ↓
Pull Request
     ↓
Review + Validation + Human Changes
     ↓
PR Outcome
     ↓
Skill Evidence
```

## 9.2 Positive outcomes

- Approved without change.
- All gates passed.
- Minimal human modification.
- Successfully merged.

## 9.3 Negative outcomes

- Repeated requested changes.
- Major code correction.
- Security rejection.
- Architecture rejection.
- PR abandoned.

## 9.4 Corrections as evidence

Compare generated implementation with final merged implementation.

Potential insight:

```text
Generated version
vs.
Human-approved merged version
```

The difference may reveal missing Skill guidance.

---

# 10. Human Feedback

## 10.1 Humans remain authoritative

Sources:

- Developers.
- Architects.
- Reviewers.
- Security specialists.
- Platform teams.
- Product/domain experts.

## 10.2 Explicit feedback mechanisms

Possible feedback:

```text
Skill useful
Skill incomplete
Skill outdated
Skill too restrictive
Skill produces excessive code
Skill misses required tests
Skill conflicts with repository architecture
```

## 10.3 Human feedback versus automated evidence

Explain weighting and conflict resolution.

## 10.4 Avoiding feedback capture

Do not allow:

- One developer's preference to redefine enterprise standards.
- One repository's implementation pattern to become universal automatically.

---

# 11. Skill Effectiveness

## 11.1 Define Skill effectiveness

A Skill is effective when it consistently produces implementations that:

- Satisfy intended goals.
- Pass deterministic gates.
- Require minimal corrective review.
- Conform to architecture.
- Satisfy security requirements.
- Remain maintainable.
- Receive human approval.

## 11.2 Possible effectiveness dimensions

- Execution success.
- Build success.
- Validation pass rate.
- Review acceptance.
- PR approval.
- Rework required.
- Time to approval.
- Human intervention.
- Defect escape rate.

## 11.3 Skill effectiveness score

Introduce conceptually, not as a universal formula.

Example:

```text
Skill Effectiveness
    = Validation Success
    + Review Acceptance
    + PR Success
    + Low Rework
    + Goal Satisfaction
```

## 11.4 Avoid oversimplified scoring

- A high merge rate does not prove quality.
- Low execution time does not prove correctness.
- One metric should not control Skill evolution.

---

# 12. Detecting Weak or Incomplete Skills

## 12.1 Weak Skill indicators

- High failure rate.
- High rework.
- Repeated review findings.
- Frequent human overrides.
- Inconsistent results.
- Excessive retries.
- Poor compatibility across repositories.

## 12.2 Incomplete Skills

Example:

`create-api` Skill creates:

- Controller.
- Handler.
- DTO.

But consistently omits:

- Authorization.
- Logging.
- Validation.
- Tests.
- OpenTelemetry instrumentation.

## 12.3 Outdated Skills

Examples:

- Deprecated framework APIs.
- Old security patterns.
- Obsolete package versions.
- superseded architectural conventions.

## 12.4 Overly broad Skills

Explain Skills that attempt too much and become unstable.

## 12.5 Overly narrow Skills

Explain excessive fragmentation and orchestration overhead.

---

# 13. Candidate Skill Improvements

## 13.1 Improvement recommendation structure

Recommend a standard proposal artifact containing:

- Skill identifier.
- Current version.
- Evidence summary.
- Observed weakness.
- Frequency.
- Impact.
- Proposed modification.
- Expected benefit.
- Risks.
- Compatibility impact.
- Required tests.
- Approval owner.

## 13.2 Example: Transactional Outbox Skill

Observed evidence:

- Repeated review comments about transaction boundaries.
- Integration tests reveal events occasionally not persisted with business updates.

Candidate recommendation:

```text
Add explicit requirement:

Business state changes and outbox messages
must be persisted within the same database transaction.
```

## 13.3 Recommendation confidence

Possible levels:

- Low.
- Moderate.
- High.

## 13.4 Evidence thresholds

Discuss configurable thresholds before creating a proposal.

---

# 14. Diagram — Evidence-to-Skill Recommendation Flow

```text
┌───────────────────────────────┐
│ Engineering Evidence          │
│                               │
│ Builds                        │
│ Reviews                       │
│ Validation                    │
│ PR Outcomes                   │
│ Failures                      │
│ Human Feedback                │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│ Evidence Normalization        │
└───────────────┬───────────────┘
                ▼
┌───────────────────────────────┐
│ Pattern Detection             │
└───────────────┬───────────────┘
                ▼
┌───────────────────────────────┐
│ Skill Attribution             │
└───────────────┬───────────────┘
                ▼
┌───────────────────────────────┐
│ Improvement Candidate         │
└───────────────┬───────────────┘
                ▼
┌───────────────────────────────┐
│ Evidence-backed Recommendation│
└───────────────────────────────┘
```

Explain every stage.

---

# 15. Skill Versioning

## 15.1 Why Skills require versions

- Reproducibility.
- Auditability.
- Compatibility.
- Comparison.
- Rollback.

## 15.2 Example

```text
create-api v1.0
create-api v1.1
create-api v1.2
create-api v2.0
```

## 15.3 Version-change categories

### Patch

- Clarification.
- Minor guidance improvement.
- No expected behavioral incompatibility.

### Minor

- New optional capability.
- Additional validation.
- New recommended step.

### Major

- Changed workflow.
- Breaking assumption.
- New architecture model.
- Different generated structure.

## 15.4 Version metadata

Include:

- Version.
- Owner.
- Approval date.
- Change summary.
- Evidence reference.
- Compatibility notes.
- Test results.

---

# 16. Skill Testing

## 16.1 Skills require tests

A Skill should not become authoritative merely because its text looks better.

## 16.2 Skill test categories

### Scenario tests

- Can the Skill successfully execute representative tasks?

### Output tests

- Does generated code meet expected structure?

### Deterministic validation

- Build.
- Tests.
- Static analysis.
- Security.
- Architecture checks.

### Regression tests

- Does the new version preserve previous valid behavior?

### Comparative tests

Compare:

```text
Skill v1
vs.
Skill v2
```

using equivalent scenarios.

## 16.3 Skill benchmark suite

Introduce reusable scenarios for Alpha Car Detailing.

---

# 17. Alpha Car Detailing Skill Test Matrix

Suggested Skills:

| Skill | Representative test |
| --- | --- |
| Create API | Add corporate fleet booking endpoint |
| Integration Event | Publish `BookingConfirmed` |
| Transactional Outbox | Persist booking and event atomically |
| Idempotent Consumer | Handle duplicate booking events |
| EF Core Persistence | Persist fleet booking aggregate |
| Authorization | Restrict corporate booking operations |
| OpenTelemetry | Trace booking request across services |
| Unit Tests | Test booking policy |
| Integration Tests | Validate API → DB → event flow |

---

# 18. Compatibility

## 18.1 Skill compatibility dimensions

- Harness version.
- Repository architecture.
- Runtime version.
- Framework version.
- Infrastructure.
- Existing Instructions.
- Dependent Skills.

## 18.2 Compatibility declaration

Example:

```yaml
skill: transactional-outbox
version: 2.1
compatible:
  dotnet: ">=10"
  architecture:
    - clean-architecture
  persistence:
    - ef-core
```

Use as a conceptual example rather than prescribing a mandatory format.

## 18.3 Cross-repository concerns

A successful Skill in Alpha Car Detailing may not automatically fit every enterprise repository.

---

# 19. Rollback

## 19.1 Why rollback matters

Even approved improvements may introduce unexpected regressions.

## 19.2 Rollback triggers

- Validation regression.
- Increased reviewer rejection.
- Compatibility problem.
- Production issue.
- Security concern.

## 19.3 Rollback model

```text
Skill v3
   ↓
Unexpected regression
   ↓
Disable v3
   ↓
Restore v2
   ↓
Investigate
   ↓
Produce v3.1 candidate
```

## 19.4 Preserve evidence

Rollback must not erase:

- Why the version was introduced.
- Why it failed.
- Why it was rolled back.

---

# 20. Diagram — Skill Versioning and Rollback

```text
Skill v1
   │
   │ evidence
   ▼
Candidate v2
   │
   ├── Tests fail ───────────► Reject
   │
   ▼
Human approval
   │
   ▼
Skill v2 Active
   │
   ├── Healthy ─────────────► Continue
   │
   └── Regression
          │
          ▼
      Roll back
          │
          ▼
       Skill v1
```

---

# 21. Ownership

## 21.1 Every governed Skill requires an owner

Possible owners:

- Architecture team.
- Platform engineering.
- Security team.
- Domain engineering team.
- Developer enablement team.

## 21.2 Ownership responsibilities

- Review proposals.
- Evaluate evidence.
- Approve versions.
- Manage compatibility.
- Define deprecation.
- Maintain test scenarios.

## 21.3 Ownership by Skill category

Examples:

```text
API Skill                 → Platform/API Architecture
Authorization Skill       → Security Architecture
EF Core Skill             → Application Architecture
OpenTelemetry Skill       → Observability Platform Team
Transactional Outbox      → Architecture Team
```

---

# 22. Human Approval

## 22.1 Governing principle

Reinforce:

> The Adaptive Harness may recommend a Skill change. It must not silently promote that change into governed engineering practice.

## 22.2 Approval inputs

Approver receives:

- Evidence.
- Proposed diff.
- Test results.
- Impact analysis.
- Compatibility analysis.
- Confidence.
- Rollback plan.

## 22.3 Approval decisions

- Approve.
- Reject.
- Modify.
- Request more evidence.
- Run additional tests.
- Limit adoption.

---

# 23. Diagram — Skill Approval Flow

```text
Evidence
   ↓
Harness Recommendation
   ↓
Candidate Skill Version
   ↓
Automated Testing
   ↓
Architecture / Security / Quality Review
   ↓
Human Owner
   │
   ├── Reject
   │
   ├── Request Changes
   │
   └── Approve
           ↓
      Versioned Skill
           ↓
       Controlled Rollout
```

---

# 24. Skill Deprecation

## 24.1 Why Skills become obsolete

- Better Skill replaces them.
- Architecture changes.
- Platform retired.
- Security approach changes.
- Workflow incorporated into another Skill.

## 24.2 Deprecation stages

```text
Active
  ↓
Deprecated
  ↓
Migration Period
  ↓
Disabled
  ↓
Archived
```

## 24.3 Deprecation metadata

- Replacement Skill.
- Deprecation reason.
- Deadline.
- Migration instructions.
- Owner.

## 24.4 Never silently delete historical Skills

Auditability requires historical versions.

---

# 25. Skill Composition Changes

## 25.1 Skills often depend on other Skills

Example:

```text
Create API
   ├── Validation
   ├── Authorization
   ├── Logging
   ├── OpenTelemetry
   └── Testing
```

## 25.2 Composition evolution

The Harness may discover that certain capabilities should become reusable sub-Skills.

Example:

- `create-api` repeatedly contains observability instructions.
- Extract observability into `instrument-api`.

## 25.3 Composition risks

- Circular dependencies.
- Hidden coupling.
- Excessive orchestration.
- Conflicting instructions.
- Version incompatibility.

## 25.4 Composition contracts

Define what each Skill:

- Requires.
- Provides.
- Assumes.
- Produces.

---

# 26. Skill Metrics

## 26.1 Operational metrics

- Execution count.
- Success rate.
- Failure rate.
- Retry count.
- Average execution duration.

## 26.2 Quality metrics

- Build pass rate.
- Test pass rate.
- Validation pass rate.
- Reviewer finding count.
- Security finding count.
- Architecture finding count.

## 26.3 PR metrics

- Approval rate.
- Requested-change rate.
- Merge rate.
- Human rework.
- Time to merge.

## 26.4 Evolution metrics

- Improvement recommendations.
- Recommendations approved.
- Recommendations rejected.
- Rollback frequency.
- Version adoption.

## 26.5 Avoid productivity theater

Do not optimize Skills merely for:

- More generated code.
- Faster generation.
- Fewer review comments without considering quality.

---

# 27. Harness Memory and Skill Evolution

## 27.1 Role of Harness Memory

Harness Memory stores accumulated evidence needed to identify patterns over time.

## 27.2 What Harness Memory may contain

- Skill executions.
- Skill version.
- Repository.
- Task category.
- Build outcomes.
- Validation outcomes.
- Review findings.
- PR outcome.
- Human corrections.
- Improvement proposals.

## 27.3 Memory is evidence, not authority

Important principle:

> Harness Memory may influence recommendations, but it does not redefine governed engineering standards.

## 27.4 Example memory relationship

```text
Skill
  ↓
Executions
  ↓
Evidence
  ↓
Harness Memory
  ↓
Patterns
  ↓
Recommendation
```

## 27.5 Preventing historical bias

- Old evidence may no longer reflect current architecture.
- Weight evidence by version and context.
- Avoid mixing incompatible repository generations.

---

# 28. Skill Evolution Inside the Harness

## 28.1 Architectural placement

Build on the existing Harness model:

```text
Prompt
   ↓
Lead Agent
   ↓
Developer Agent
   ↓
Reviewer Agent
   ↓
Validator Agent
   ↓
Evaluator Agent
   ↓
Pull Request
   ↓
Learning Engine
   ↓
Harness Memory
   ↓
Skill Evolution Engine
   ↓
Recommendation
   ↓
Human Approval
   ↓
Skill Registry
```

## 28.2 Core components

### Evidence Collector

Collects:

- Builds.
- Tests.
- Reviews.
- Pull requests.
- human feedback.

### Evidence Normalizer

Converts heterogeneous evidence into structured signals.

### Pattern Detector

Finds repeated observations.

### Skill Attribution Engine

Determines which Skill may have influenced the outcome.

### Skill Analyzer

Evaluates current Skill instructions.

### Recommendation Generator

Creates candidate improvements.

### Skill Test Runner

Executes test scenarios.

### Governance Engine

Enforces approval rules.

### Skill Registry

Stores:

- Versions.
- Ownership.
- status.
- Compatibility.
- Approval history.

---

# 29. Diagram — Skill Evolution Inside the Harness

```text
┌──────────────────────────────────────────┐
│              Harness Run                 │
│                                          │
│ Lead → Developer → Reviewer              │
│          → Validator → Evaluator         │
└───────────────────┬──────────────────────┘
                    │
                    ▼
            ┌──────────────┐
            │ PR / Outcome │
            └──────┬───────┘
                   ▼
┌──────────────────────────────────────────┐
│            Evidence Collector            │
└───────────────────┬──────────────────────┘
                    ▼
┌──────────────────────────────────────────┐
│              Harness Memory              │
└───────────────────┬──────────────────────┘
                    ▼
┌──────────────────────────────────────────┐
│             Pattern Detector             │
└───────────────────┬──────────────────────┘
                    ▼
┌──────────────────────────────────────────┐
│        Skill Evolution Analyzer          │
└───────────────────┬──────────────────────┘
                    ▼
┌──────────────────────────────────────────┐
│      Candidate Skill Recommendation      │
└───────────────────┬──────────────────────┘
                    ▼
        Tests → Review → Approval
                    │
                    ▼
┌──────────────────────────────────────────┐
│         Versioned Skill Registry         │
└──────────────────────────────────────────┘
```

---

# 30. Diagram — Skill Evolution Lifecycle

```text
Observe
   ↓
Collect Evidence
   ↓
Detect Pattern
   ↓
Diagnose Skill
   ↓
Propose Improvement
   ↓
Create Candidate Version
   ↓
Test
   ↓
Review
   ↓
Human Approval
   ↓
Publish
   ↓
Monitor
   ↓
Keep / Improve / Roll Back
```

Emphasize that this is a lifecycle rather than a one-time editing operation.

---

# 31. Architecture Discussion

## 31.1 Separate learning from execution

The component executing Skills should not automatically control their evolution.

## 31.2 Separate recommendation from promotion

Recommendation engine:

```text
Can propose
Cannot approve
```

## 31.3 Separate evidence from authoritative knowledge

- Evidence is observational.
- Skills are governed assets.

## 31.4 Skill Registry

Discuss the value of a central registry containing:

- Skill ID.
- Versions.
- Owner.
- status.
- compatibility.
- test suite.
- dependencies.
- approval history.

## 31.5 Repository-local versus enterprise Skills

Explain evolution policies for:

- Repository-specific Skills.
- Shared platform Skills.
- Enterprise-wide Skills.

## 31.6 Progressive rollout

Potential model:

```text
Candidate
   ↓
Sandbox
   ↓
Pilot Repository
   ↓
Limited Adoption
   ↓
Enterprise Default
```

---

# 32. Hands-on Example — Evolving the Transactional Outbox Skill

## 32.1 Initial Skill

The Skill currently instructs the agent to:

- Create outbox entity.
- Persist integration event.
- Add background processor.

## 32.2 Evidence collected

Across Alpha Car Detailing:

- Reviewer identifies transaction-boundary issues.
- Integration tests detect non-atomic writes.
- PR reviewer repeatedly requests same fix.
- Architect comments that the Skill does not explicitly require one transaction.

## 32.3 Harness detects pattern

Associate failures with:

- `transactional-outbox`.
- Current version.
- Specific persistence pattern.

## 32.4 Candidate improvement

Proposed new requirement:

- Aggregate update and outbox record must be persisted atomically.
- Transaction semantics must be verified by an integration test.

## 32.5 Candidate Skill version

Example:

- Current: `transactional-outbox v1.3`.
- Candidate: `v1.4`.

## 32.6 Candidate validation

Run scenarios:

- Booking creation succeeds.
- Event persisted.
- Transaction rollback prevents partial persistence.
- Background delivery works.
- Duplicate processing remains safe.

## 32.7 Human approval

Architecture owner reviews:

- Evidence.
- diff.
- tests.
- compatibility.

## 32.8 Promotion

Approve and publish `v1.4`.

## 32.9 Monitoring

Compare:

- Reviewer findings before and after.
- Integration test failures.
- PR correction rate.

---

# 33. Additional Alpha Car Detailing Examples

## 33.1 Creating APIs

Evolution signal:

- Generated APIs repeatedly lack consistent validation and telemetry.

Improvement:

- Include mandatory validation, authorization, trace enrichment, and standardized error handling.

## 33.2 Adding integration events

Signal:

- Events frequently lack correlation metadata.

Improvement:

- Add event metadata requirements.

## 33.3 Transactional outbox

Signal:

- Transaction boundaries repeatedly corrected.

Improvement:

- Require atomicity test.

## 33.4 Idempotent consumers

Signal:

- Duplicate event tests repeatedly added manually.

Improvement:

- Make duplicate-processing tests part of the Skill.

## 33.5 EF Core persistence

Signal:

- Reviewers repeatedly flag entity-loading strategy.

Improvement:

- Add guidance on query behavior and transaction ownership.

## 33.6 Authorization

Signal:

- Security validation repeatedly detects missing policy enforcement.

Improvement:

- Require explicit authorization policy mapping.

## 33.7 OpenTelemetry

Signal:

- Reviewers repeatedly add activity tags.

Improvement:

- Define required business telemetry.

## 33.8 Unit and integration tests

Signal:

- Generated tests focus on happy paths.

Improvement:

- Require failure, authorization, duplicate, and transactional scenarios.

---

# 34. Claude Example

## 34.1 Claude-first Skill Evolution workflow

Demonstrate Claude analyzing:

- Current Skill.
- Evidence summary.
- Reviewer findings.
- Validation outcomes.
- final merged PR changes.

## 34.2 Expected output

Claude should produce:

- Observed pattern.
- Likely Skill weakness.
- Proposed change.
- Evidence references.
- risks.
- test plan.

## 34.3 Claude's authority limit

Claude may:

- Analyze.
- Recommend.
- Draft.

Claude may not:

- Promote the Skill automatically.
- Override Skill owner.
- bypass validation.

---

# 35. GitHub Copilot Comparison

Discuss only meaningful differences.

## 35.1 Repository instruction mechanisms

- How reusable workflow guidance may be represented.
- How repository context influences generated implementations.

## 35.2 Evolution workflow

- Changes commonly flow through normal source control and PR review.

## 35.3 Enterprise implication

The same evidence-driven Skill Evolution architecture should remain vendor-neutral even when implementation mechanisms differ.

---

# 36. Codex Comparison

## 36.1 Skill-equivalent reusable workflows

Discuss how reusable agent guidance may be represented differently.

## 36.2 Harness-managed evolution

The Harness should maintain its own governed Skill model independently of any specific agent platform.

## 36.3 Portability principle

A mature Skill architecture should survive AI platform replacement.

---

# 37. Best Practices

Include guidance such as:

- Base Skill changes on repeated evidence.
- Keep evidence traceable.
- Version every governed Skill.
- Assign clear Skill ownership.
- Separate recommendation from approval.
- Test every meaningful Skill change.
- Maintain rollback capability.
- Measure effectiveness before and after evolution.
- Maintain compatibility metadata.
- Keep Skills focused.
- Prefer composition over oversized Skills.
- Preserve previous versions.
- Use controlled rollout for high-impact Skills.
- Require stronger evidence for enterprise-wide Skills.
- Distinguish repository-specific evidence from global patterns.
- Record rejected recommendations.
- Reevaluate Skills after architecture changes.
- Preserve human authority.

---

# 38. Anti-Patterns

## 38.1 Silent Skill rewriting

Harness modifies production Skill automatically.

## 38.2 Learning from one failure

One failed run triggers enterprise-wide Skill modification.

## 38.3 Reviewer-comment copying

Every review comment is inserted directly into the Skill.

## 38.4 Skill inflation

Skills accumulate endless instructions.

## 38.5 No versioning

Current Skill overwritten in place.

## 38.6 No rollback

Previous versions unavailable.

## 38.7 No ownership

Nobody is responsible for approvals.

## 38.8 Metric-driven distortion

Skill optimized only for merge rate.

## 38.9 Repository leakage

One project's pattern becomes universal.

## 38.10 Self-reinforcing learning

Harness trains recommendations using its own previous recommendations without independent evidence.

## 38.11 Hidden Skill composition

Dependencies between Skills remain undocumented.

## 38.12 Deprecated Skills remaining active

Old Skills silently continue influencing agents.

---

# 39. Architect's Notes

Cover architectural observations such as:

- Treat Skills as governed software assets.
- Skill Evolution should resemble software lifecycle management.
- Reusable Skills deserve versioning, tests, ownership, and release discipline.
- Evidence attribution is one of the hardest parts of adaptive engineering.
- Not every failure should become institutional knowledge.
- High-impact Skills require stronger governance.
- Skill Evolution should operate on evidence accumulated across time, not immediate agent intuition.
- The learning system and authoritative Skill registry must remain separate.

---

# 40. Enterprise Tips

Include enterprise guidance:

- Maintain an enterprise Skill catalog.
- Classify Skills by criticality.
- Require security approval for authorization/security Skills.
- Establish Skill maintainers.
- Store evidence alongside version history.
- Create test packs for high-value Skills.
- Use pilot repositories before enterprise rollout.
- Track adoption and rollback.
- Integrate Skill approval into existing PR/change-management processes.
- Preserve auditability for regulated environments.
- Allow teams to pin approved Skill versions.
- Distinguish experimental Skills from governed Skills.

---

# 41. Decision Points

## Decision Point 1

**When should repeated evidence trigger a Skill recommendation?**

Consider:

- Frequency.
- severity.
- confidence.
- repository diversity.

## Decision Point 2

**Should the Skill be repository-specific or enterprise-wide?**

## Decision Point 3

**Does the proposed change require a major version?**

## Decision Point 4

**Who owns approval?**

## Decision Point 5

**What evidence is required before promotion?**

## Decision Point 6

**Can the new Skill version be rolled out gradually?**

## Decision Point 7

**When should a Skill be decomposed rather than expanded?**

## Decision Point 8

**When should the old Skill be deprecated?**

## Decision Point 9

**When should an automated recommendation be rejected despite strong statistical evidence?**

---

# 42. Exercises

## Exercise 1 — Detect a weak Skill

Given repeated Alpha Car Detailing review findings, identify whether the problem belongs to:

- Prompt.
- Skill.
- Instruction.
- Agent behavior.
- Repository design.

## Exercise 2 — Design an evolution recommendation

Create a proposal for improving the `create-api` Skill after repeated authorization failures.

## Exercise 3 — Design Skill metrics

Define five metrics for the `idempotent-consumer` Skill.

## Exercise 4 — Versioning

Classify example changes as:

- Patch.
- Minor.
- Major.

## Exercise 5 — Skill testing

Create a test suite for the transactional-outbox Skill.

## Exercise 6 — Rollback

Design a rollback workflow for an OpenTelemetry Skill that causes performance degradation.

## Exercise 7 — Human approval

Define approvers for:

- API Skill.
- Authorization Skill.
- Persistence Skill.
- Observability Skill.

## Exercise 8 — Skill composition

Refactor an oversized `create-api` Skill into composable Skills.

## Exercise 9 — Harness Memory

Design the evidence records required to support Skill Evolution.

## Exercise 10 — Architecture

Draw the end-to-end Skill Evolution pipeline for Alpha Car Detailing.

---

# 43. Interview Questions

Include questions such as:

1. What is Skill Evolution?
2. Why should reusable Skills evolve?
3. What evidence can drive Skill Evolution?
4. Why should the Harness not silently modify governed Skills?
5. How do repeated validation failures identify possible Skill weaknesses?
6. What is the difference between a Skill defect and an execution failure?
7. How can pull-request outcomes improve a Skill?
8. What is Skill effectiveness?
9. Which metrics can measure Skill quality?
10. Why is Skill versioning necessary?
11. How should Skills be tested?
12. Why is compatibility important?
13. When should a Skill be rolled back?
14. Who should own enterprise Skills?
15. How should human approval work?
16. What is Skill deprecation?
17. What risks exist when Skills depend on other Skills?
18. How does Harness Memory support Skill Evolution?
19. Why must evidence remain separate from authoritative standards?
20. How would you implement Skill Evolution inside an enterprise Harness?
21. How do you prevent a self-learning Harness from reinforcing its own mistakes?
22. How should repository-specific evidence differ from enterprise-wide learning?
23. When should a Skill be split into multiple composable Skills?
24. How would you evaluate whether a new Skill version is actually better?
25. What governance controls are required before Adaptive Skill Evolution can operate at enterprise scale?

---

# 44. Chapter Summary

Summarize the major principles:

- Skills represent reusable engineering workflows.
- Reusable workflows must evolve as architecture and engineering practices change.
- Adaptive Harnesses can use accumulated evidence to detect Skill weaknesses.
- Evidence should come from multiple independent sources.
- Repeated failures are signals, not automatic proof.
- Review findings and PR outcomes provide valuable human evidence.
- Harness Memory enables longitudinal pattern detection.
- Skill effectiveness should be measurable.
- Candidate improvements require testing.
- Every governed Skill needs versioning.
- Compatibility and rollback are essential.
- Skill ownership must be explicit.
- Deprecation requires a controlled lifecycle.
- Composition changes require dependency management.
- The Harness may recommend improvements.
- Humans remain responsible for approving governed Skill changes.

End with the central architectural principle:

> **A self-learning Harness should become better at recommending how Skills should evolve without becoming the uncontrolled authority that rewrites them.**

---

# 45. Further Reading

Organize further-reading topics around:

- Adaptive systems.
- Continuous improvement.
- Software configuration management.
- Versioning strategies.
- Policy-as-code.
- Software supply-chain governance.
- Evidence-driven engineering.
- Agent evaluation.
- Human-in-the-loop AI systems.
- Change management.
- Software architecture governance.
- Observability-driven development.
- Continuous validation.
- Enterprise AI governance.

Include cross-references to:

- Chapter 7 — Skills.
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 20 — Metrics.
- Chapter 29 — AI Governance.
- Chapter 31 — Human Approval.
- Chapter 32 — Audit Trails.
- Chapter 36 — Static vs Adaptive Harnesses.
- Chapter 37 — Harness Memory.
- Chapter 38 — Learning from Builds.
- Chapter 39 — Learning from Reviews.
- Chapter 40 — Learning from Pull Requests.
- Chapter 42 — Prompt Evolution.
- Chapter 43 — Instruction Evolution.
- Chapter 44 — Knowledge Graph.
- Chapter 45 — Autonomous Improvement Engine.

---

**Chapter 41 outline is complete.**
