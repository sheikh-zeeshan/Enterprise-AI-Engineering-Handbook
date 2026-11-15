# Chapter 42 — Prompt Evolution  

**Part VI — Self-Learning AI Systems**  
**Target file:** `book/42-prompt-evolution.md`

The outline below follows the handbook’s frozen chapter structure, terminology, enterprise focus, Alpha Car Detailing running example, and the governing principle that the Harness may recommend improvements but must not silently change governed engineering assets. AI development handbook The chapter also preserves the established distinction that a Prompt represents the current task, while Skills define reusable workflows and Instructions define persistent repository-wide engineering expectations. AI development handbook

---

## 1. Story-Driven Opening — When a Prompt Keeps Producing the Same Mistake

### 1.1 The Alpha Car Detailing scenario

- A development team repeatedly uses a Prompt to implement changes to the corporate fleet booking feature.
- The Prompt appears reasonable:
  - implement the requested feature;
  - add tests;
  - follow repository conventions;
  - prepare the change for review.
- Several Harness runs reveal recurring problems:
  - missing booking-limit edge cases;
  - incomplete acceptance criteria;
  - insufficient integration-test coverage;
  - incorrect assumptions about fleet authorization;
  - missing idempotency validation;
  - Reviewer requests for the same architectural corrections;
  - Validator failures caused by missing contract checks.
- Developers repeatedly fix the same omissions after generation.

### 1.2 The important question

- Should the team simply accept that AI-generated work requires recurring correction?
- Or can the Harness learn that the Prompt itself is incomplete?

### 1.3 From task execution to Prompt improvement

- The Harness already has evidence from:
  - builds;
  - tests;
  - review findings;
  - Validator outcomes;
  - Evaluator assessments;
  - pull requests;
  - human corrections.
- That evidence can reveal systematic weaknesses in Prompts.

### 1.4 Core principle
>
> Repeated evidence may justify recommending a better Prompt, but the Adaptive Harness must not silently rewrite governed Prompt templates.

### 1.5 The chapter's central idea

- Prompt Evolution is a governed learning loop.
- Evidence produces candidate improvements.
- Candidates are tested and compared.
- Humans approve governed Prompt changes.
- Versions remain traceable and reversible.

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Define Prompt Evolution in an Adaptive Harness.
- Distinguish Prompt Evolution from ordinary Prompt editing.
- Explain why Prompts become ineffective over time.
- Identify evidence that may indicate Prompt weakness.
- Recognize ambiguous, incomplete, overly broad, and context-poor Prompts.
- Separate Prompt problems from:
  - Skill problems;
  - Instruction problems;
  - repository problems;
  - implementation failures.
- Design a Prompt-improvement recommendation pipeline.
- Create governed Prompt templates.
- Version Prompt changes.
- Test candidate Prompt versions.
- Compare Prompts using controlled evaluation.
- Define Prompt compatibility and rollback strategies.
- Establish Prompt ownership and approval boundaries.
- Define Prompt deprecation policies.
- Measure Prompt effectiveness.
- Explain how Harness Memory supports Prompt Evolution.
- Integrate Prompt Evolution into the wider Adaptive Harness.
- Apply these ideas to Alpha Car Detailing development workflows.

---

# 3. Background

## 3.1 Revisiting Prompts from Chapter 8

- Prompt as a task-specific request.
- Prompt as "today's task."
- Relationship to:
  - Instructions;
  - Skills;
  - Roles;
  - Steering Notes;
  - Repository Intelligence;
  - Harness execution.

The handbook's established model defines Prompts as task-specific requests and distinguishes them from persistent Instructions and reusable Skills. AI development handbook

## 3.2 Prompts in a static Harness

- Prompt is supplied.
- Agent executes.
- Results are validated.
- Run ends.
- Human corrections may never improve the original Prompt.

## 3.3 Prompts in an Adaptive Harness

- Prompt is executed.
- Evidence is collected.
- Outcomes are correlated with the Prompt version.
- Recurring patterns are identified.
- Candidate improvements are proposed.
- Candidates are evaluated.
- Human-approved changes become new Prompt versions.

## 3.4 Why Prompt Evolution belongs in Part VI

- Chapter 36: Static vs Adaptive Harnesses.
- Chapter 37: Harness Memory.
- Chapter 38: Learning from Builds.
- Chapter 39: Learning from Reviews.
- Chapter 40: Learning from Pull Requests.
- Chapter 41: Skill Evolution.
- Chapter 42: Prompt Evolution.
- Forward connection:
  - Chapter 43 — Instruction Evolution;
  - Chapter 44 — Knowledge Graph;
  - Chapter 45 — Autonomous Improvement Engine.

This placement matches the established Self-Learning AI Systems sequence. AI development handbook

## 3.5 Why Prompt Evolution is not simply "prompt engineering"

- The chapter focuses on enterprise AI Engineering.
- The concern is not writing clever wording.
- The concern is:
  - evidence;
  - repeatability;
  - governance;
  - versioning;
  - effectiveness;
  - measurable outcomes;
  - controlled evolution.

---

# 4. Concepts

## 4.1 What Prompt Evolution Is

### Definition

Prompt Evolution is the governed process by which an Adaptive Harness uses accumulated execution evidence to identify weaknesses in task Prompts, generate candidate improvements, evaluate those candidates, and recommend approved Prompt versions for future work.

### Characteristics

- Evidence-driven.
- Versioned.
- Measurable.
- Reversible.
- Governed.
- Human-approved.
- Task-oriented.

### Prompt Evolution is not

- autonomous rewriting of every Prompt;
- modifying Instructions;
- modifying Skills without evidence;
- learning arbitrary preferences from one run;
- silently embedding reviewer comments into templates;
- bypassing ownership and governance.

---

## 4.2 Why Prompts Evolve

### Reasons Prompts become inadequate

- Business requirements change.
- Architecture evolves.
- New acceptance criteria appear.
- Security requirements increase.
- Validation gates become stricter.
- Recurring omissions become visible.
- Teams learn better implementation approaches.
- New failure modes emerge.
- Repository conventions mature.
- Prompt wording proves ambiguous.
- Models interpret unclear instructions inconsistently.

### Example

Original Alpha Car Detailing Prompt:

> Implement corporate fleet booking limits.

Possible recurring omissions:

- maximum vehicles per corporate account;
- concurrency behavior;
- authorization;
- audit logging;
- integration tests;
- retry behavior;
- idempotency;
- API error semantics.

Prompt Evolution may recommend explicitly including these concerns.

---

## 4.3 Prompt Effectiveness

### Define effectiveness

A Prompt is effective when it consistently leads the Harness toward outcomes that satisfy the intended engineering goal with acceptable correction cost.

### Effectiveness dimensions

- Goal completion.
- Acceptance-criteria satisfaction.
- Build success.
- Test success.
- Review quality.
- Validator success.
- Evaluator score.
- Security compliance.
- Architecture compliance.
- Human correction effort.
- Rework rate.
- PR acceptance.
- First-pass success rate.

### Important distinction

- A successful run does not necessarily mean an effective Prompt.
- A failed run does not necessarily mean a bad Prompt.

---

## 4.4 Prompt Effectiveness versus Model Capability

Examine whether failure originates from:

- Prompt ambiguity.
- Missing repository context.
- Missing Skill.
- Incorrect Skill.
- Weak model reasoning.
- Tooling failure.
- Incomplete requirements.
- Architecture misunderstanding.
- Invalid acceptance criteria.
- Deterministic infrastructure failure.

### Architect's concern

Do not "fix the Prompt" when the actual problem belongs elsewhere.

---

## 4.5 Learning Signals

### Evidence categories

- Build evidence.
- Test evidence.
- Reviewer evidence.
- Validator evidence.
- Evaluator evidence.
- Pull-request evidence.
- Human feedback.
- Production feedback where governed and relevant.
- Harness run metrics.

### Signal characteristics

- Positive.
- Negative.
- Weak.
- Strong.
- Repeated.
- Contradictory.
- Context-specific.
- Repository-wide.

---

## 4.6 Build and Test Failures as Prompt Evidence

### Examples

- Prompt repeatedly omits tests.
- Prompt requests tests but not integration coverage.
- Tests repeatedly expose the same missing edge cases.
- Generated migrations break existing integration tests.
- Contract tests fail because Prompt ignores backward compatibility.

### Alpha Car Detailing example

Bug-fix Prompt:

> Fix duplicate fleet bookings.

Repeated failures reveal that the Prompt does not require:

- concurrency analysis;
- idempotency-key behavior;
- duplicate event handling;
- retry scenarios;
- regression tests.

### Learning question

Should these become:

- Prompt additions;
- Skill changes;
- Instructions;
- validation gates?

---

## 4.7 Review Findings as Prompt Evidence

### Recurring review patterns

- missing cancellation token;
- poor exception handling;
- business logic in controller;
- incomplete tests;
- missing observability;
- unnecessary architectural changes;
- missing documentation.

### Pattern threshold

- One review comment may be incidental.
- Recurring review comments across comparable tasks may indicate Prompt weakness.

### Relationship to Chapter 39

- Reviews produce evidence.
- Prompt Evolution consumes evidence.

---

## 4.8 Validator Outcomes as Prompt Evidence

### Deterministic signals

- build failures;
- unit-test failures;
- integration-test failures;
- architecture-test failures;
- security scanning failures;
- API contract failures;
- migration-validation failures.

### Key distinction

Validator provides objective evidence.
Prompt Evolution interprets recurring patterns across that evidence.

---

## 4.9 Evaluator Outcomes as Prompt Evidence

### Evaluator signals

- goal not fully satisfied;
- acceptance criteria partially implemented;
- business outcome incomplete;
- solution quality below expectations;
- implementation technically valid but functionally incomplete.

### Example

Validator passes all tests, but Evaluator identifies:

> Corporate fleet supervisors cannot determine why a booking was rejected.

Potential Prompt improvement:

- require user-visible business error semantics;
- require acceptance-criteria traceability.

---

## 4.10 Pull-Request Feedback

### Sources

- requested changes;
- review discussions;
- approvals;
- rejected PRs;
- merged PRs;
- human corrections;
- architecture observations;
- security findings;
- post-generation edits.

### What Prompt Evolution can infer

- recurring missing requirements;
- recurring unnecessary work;
- recurring code structure issues;
- recurring review friction;
- differences between generated and accepted implementations.

### Connection to Chapter 40

- PR evidence does not directly rewrite Prompts.
- It contributes governed learning signals.

---

## 4.11 Human Feedback

### Human feedback sources

- developers;
- reviewers;
- architects;
- security engineers;
- product owners;
- QA engineers;
- engineering managers.

### Types

- explicit Prompt rating;
- rejection;
- modification;
- clarification;
- accepted recommendation;
- "too broad";
- "missing business context";
- "generated too much";
- "did not follow requested scope."

### Human feedback weighting

- Not all feedback has equal authority.
- Ownership and role matter.

---

## 4.12 Detecting Ambiguous Prompts

### Ambiguity patterns

- "Improve performance."
- "Fix the booking issue."
- "Add proper validation."
- "Make it enterprise ready."
- "Clean up this service."

### Harness analysis

- undefined target;
- undefined scope;
- undefined measurable outcome;
- missing constraints;
- missing acceptance criteria.

### Recommendation behavior

The Harness may suggest:

> Specify the endpoint, expected performance target, test method, permitted architectural scope, and acceptance criteria.

---

## 4.13 Detecting Incomplete Prompts

### Missing elements

- business goal;
- repository location;
- constraints;
- acceptance criteria;
- required tests;
- security expectations;
- architecture boundaries;
- backward compatibility;
- definition of done.

### Alpha Car Detailing example

Weak Prompt:

> Add a vehicle history endpoint.

Improved candidate Prompt should clarify:

- customer versus fleet authorization;
- pagination;
- data ownership;
- returned fields;
- audit considerations;
- tests;
- API compatibility.

---

## 4.14 Missing Context versus Missing Prompt Instructions

Distinguish:

- Prompt did not request context.
- Repository Intelligence lacks context.
- Steering Note lacks current sprint guidance.
- Instructions lack persistent standards.
- Skill lacks reusable procedure.

### Decision framework

Ask:

> Is this information specific to this task, reusable across tasks, or persistent across the repository?

- Task-specific → Prompt.
- Reusable process → Skill.
- Persistent rule → Instruction.
- Repository understanding → Repository Intelligence.
- Current mission → Steering Note.

---

## 4.15 Missing Acceptance Criteria

### Why acceptance criteria matter

- Prompts without measurable outcomes are difficult to evaluate.
- Evaluator cannot reliably determine goal completion.
- Humans may disagree on what "done" means.

### Evolution opportunity

Harness may recommend converting vague intent into explicit acceptance criteria.

---

# 5. Architecture Discussion

## 5.1 Prompt Evolution Lifecycle

### Diagram 1 — Prompt Evolution Lifecycle

```text
Prompt v1
   │
   ▼
Harness Execution
   │
   ├── Build Evidence
   ├── Test Evidence
   ├── Review Evidence
   ├── Validator Evidence
   ├── Evaluator Evidence
   ├── PR Evidence
   └── Human Feedback
          │
          ▼
   Pattern Detection
          │
          ▼
Candidate Prompt v2
          │
          ▼
Prompt Evaluation
          │
   ┌──────┴──────┐
   │             │
Reject       Recommend
                 │
                 ▼
           Human Approval
                 │
                 ▼
             Prompt v2
                 │
                 ▼
             Monitor
```

### Explain each stage

1. Execute governed Prompt.
2. Capture evidence.
3. Aggregate evidence across comparable runs.
4. Detect recurrent Prompt deficiencies.
5. Generate candidate improvement.
6. Test candidate.
7. Compare with baseline.
8. Recommend change.
9. Obtain human approval.
10. Publish new version.
11. Monitor subsequent results.

---

## 5.2 Evidence-to-Prompt Recommendation Flow

### Diagram 2

```text
Build Results ───────────────┐
Test Results ────────────────┤
Review Findings ─────────────┤
Validator Results ───────────┤
Evaluator Results ───────────┼──► Evidence Normalization
PR Feedback ─────────────────┤
Human Feedback ──────────────┘
                                  │
                                  ▼
                           Pattern Correlation
                                  │
                                  ▼
                         Prompt Gap Detection
                                  │
                                  ▼
                     Candidate Improvement Generator
                                  │
                                  ▼
                        Recommendation Record
```

### Recommendation record contains

- source Prompt;
- Prompt version;
- evidence references;
- detected issue;
- frequency;
- confidence;
- proposed wording;
- expected benefit;
- risk;
- affected workflows;
- evaluation results;
- owner;
- approval status.

---

## 5.3 Candidate Prompt Improvements

### Types of candidate improvements

#### Add missing context

Example:

- specify the affected bounded context.

#### Add acceptance criteria

Example:

- duplicate booking requests must not create duplicate reservations.

#### Add constraints

Example:

- do not modify public API contracts.

#### Add evidence requirements

Example:

- include tests proving idempotency.

#### Clarify scope

Example:

- modify booking creation only; do not refactor unrelated pricing logic.

#### Improve output contract

Example:

- report files changed, tests executed, and unresolved assumptions.

#### Remove unnecessary Prompt text

- Reduce redundant instructions.
- Avoid duplicating governed Instructions.

---

## 5.4 Prompt Templates

### Why templates matter

- Repeatability.
- Governance.
- Version control.
- Discoverability.
- Consistent inputs.
- Easier evaluation.

### Example categories

- Feature Development Prompt.
- Bug Fix Prompt.
- Testing Prompt.
- Reviewer Prompt.
- Validator Prompt.
- Evaluator Prompt.
- PR Preparation Prompt.
- Refactoring Prompt.
- Architecture Investigation Prompt.

---

## 5.5 Example Prompt Template Structure

```text
Task
Context
Business Goal
Scope
Constraints
Acceptance Criteria
Required Evidence
Validation Expectations
Output Requirements
```

### Template versus populated Prompt

- Template provides governed structure.
- Run Prompt contains task-specific values.

---

## 5.6 Prompt Versioning

### Version properties

- Prompt ID.
- Version.
- Owner.
- Status.
- Effective date.
- Parent version.
- Change reason.
- Evidence links.
- Approval information.
- Compatibility metadata.

### Example

```text
prompt-id: feature-development
version: 3.2
status: active
owner: engineering-platform
previous-version: 3.1
```

---

## 5.7 Prompt Versioning and Rollback

### Diagram 3

```text
v1.0
  │
  ▼
v1.1
  │
  ▼
v1.2 ───────────────┐
  │                 │
  ▼                 │
v2.0                 │
  │                 │
  ▼                 │
Observed Regression  │
  │                 │
  └────────────► Rollback
                     │
                     ▼
                  v1.2
```

### Discuss

- immutable version history;
- active version pointer;
- previous known-good version;
- auditability;
- rollback reason;
- rollback metrics.

---

## 5.8 Prompt Testing

### Tests can evaluate

- expected scope recognition;
- acceptance-criteria coverage;
- output structure;
- generated implementation quality;
- tool-use behavior;
- validation compliance;
- consistency across representative tasks.

### Prompt test datasets

Use historical Alpha Car Detailing tasks such as:

- fleet booking limits;
- duplicate booking bug;
- vehicle-history endpoint;
- payment retry defect;
- integration-test generation.

---

## 5.9 Golden Prompt Evaluation Cases

### Define representative tasks

- straightforward feature.
- ambiguous requirement.
- security-sensitive feature.
- bug with concurrency implications.
- architecture-constrained change.
- backward-compatibility scenario.

### Purpose

Prevent Prompt improvements from optimizing only for one recent failure.

---

## 5.10 A/B or Comparative Prompt Evaluation

### Compare

- Prompt v1.
- Candidate Prompt v2.

### Same task set

- same repository baseline;
- same constraints;
- same model/configuration where practical;
- same gates;
- same evaluation criteria.

### Compare outcomes

- build success;
- test success;
- review findings;
- Validator success;
- Evaluator score;
- rework;
- token/compute cost;
- time;
- scope adherence.

### Avoid misleading A/B evaluation

- small sample sizes;
- changing model and Prompt simultaneously;
- different repository states;
- subjective-only scoring.

---

## 5.11 Prompt Compatibility

### Compatibility questions

- Does a new Prompt assume a newer Skill?
- Does it depend on new Instructions?
- Does it require new Validator gates?
- Does it rely on unavailable tools?
- Does it reference changed repository structure?
- Does it remain compatible with supported agent platforms?

### Compatibility metadata

- minimum Harness version;
- required Skills;
- required tools;
- supported roles;
- applicable repository types.

---

## 5.12 Rollback

### Reasons

- higher failure rate;
- increased rework;
- unexpected scope expansion;
- regression in task success;
- incompatible Skill dependency;
- excessive cost;
- weaker human acceptance.

### Rollback principle

Prompt Evolution must always preserve a known-good path.

---

## 5.13 Prompt Ownership

### Potential owners

- platform engineering;
- engineering enablement;
- architecture;
- development teams;
- domain teams;
- security teams.

### Ownership responsibilities

- approve versions;
- review recommendations;
- define tests;
- monitor metrics;
- approve deprecation;
- manage compatibility.

---

## 5.14 Human Approval

The project's governance model already requires human approval for Harness-recommended changes rather than silent modification. AI development handbook

### Approval may consider

- supporting evidence;
- scope of impact;
- measured improvement;
- risks;
- compatibility;
- governance requirements.

### Approval outcomes

- approved;
- rejected;
- revise;
- experiment only;
- defer;
- limited rollout.

---

## 5.15 Prompt Approval Flow

### Diagram 4

```text
Evidence
   │
   ▼
Prompt Recommendation
   │
   ▼
Automated Evaluation
   │
   ▼
Prompt Owner Review
   │
   ├──────── Reject
   │
   ├──────── Revise
   │
   └──────── Approve
               │
               ▼
          Version Publish
               │
               ▼
         Controlled Rollout
               │
               ▼
            Monitor
```

---

## 5.16 Prompt Deprecation

### States

- experimental;
- candidate;
- active;
- deprecated;
- retired.

### Deprecation triggers

- replaced by better Prompt;
- obsolete workflow;
- incompatible architecture;
- duplicated functionality;
- superseded by Skill or Instruction changes.

### Deprecation safeguards

- migration guidance;
- replacement Prompt;
- grace period;
- usage tracking.

---

## 5.17 Prompt Metrics

### Quality metrics

- first-pass completion rate;
- Validator pass rate;
- Evaluator acceptance rate;
- reviewer-finding count;
- average rework;
- PR approval rate;
- human correction rate;
- acceptance-criteria coverage.

### Efficiency metrics

- average iterations;
- completion time;
- tool calls;
- token/compute cost;
- unnecessary file-change count.

### Stability metrics

- variance between runs;
- rollback rate;
- regression frequency.

### Governance metrics

- recommendation count;
- approval rate;
- rejection rate;
- time to approval;
- deprecated Prompt usage.

---

## 5.18 Harness Memory and Prompt Evolution

### What Harness Memory may retain

- Prompt version used;
- run outcomes;
- evidence;
- review patterns;
- failure classifications;
- improvement recommendations;
- approval outcomes;
- evaluation results.

### What Harness Memory must not become

- an uncontrolled source of authoritative Prompt changes.

### Principle

Harness Memory provides evidence.
Governance determines authority.

---

## 5.19 Prompt Evolution inside the Harness

### Diagram 5

```text
                    ┌────────────────────┐
                    │   Prompt Registry   │
                    └─────────┬──────────┘
                              │
                              ▼
                     ┌─────────────────┐
                     │ Harness Runtime │
                     └────────┬────────┘
                              │
             ┌────────────────┼─────────────────┐
             ▼                ▼                 ▼
          Build/Test       Review          Validate/Evaluate
             │                │                 │
             └────────────────┼─────────────────┘
                              ▼
                       Evidence Store
                              │
                              ▼
                       Harness Memory
                              │
                              ▼
                       Learning Engine
                              │
                              ▼
                    Prompt Recommendation
                              │
                              ▼
                     Evaluation Pipeline
                              │
                              ▼
                      Human Governance
                              │
                              ▼
                       Prompt Registry
```

### Components

- Prompt Registry.
- Harness Runtime.
- Evidence Store.
- Harness Memory.
- Pattern Detector.
- Recommendation Engine.
- Prompt Evaluation Pipeline.
- Approval Workflow.
- Version Registry.
- Metrics and Observability.

---

## 5.20 Prompt Evolution and the Wider Learning Architecture

Discuss relationships with:

### Learning from Builds

- identifies technical failure patterns.

### Learning from Reviews

- identifies quality and maintainability patterns.

### Learning from Pull Requests

- identifies accepted versus corrected generated work.

### Skill Evolution

- improves reusable procedures.

### Instruction Evolution

- improves persistent engineering rules.

### Knowledge Graph

- connects:
  - Prompt;
  - task;
  - Skill;
  - Instruction;
  - failure;
  - review;
  - owner;
  - outcome.

### Autonomous Improvement Engine

- coordinates recommendations without bypassing governance.

---

# 6. Professional Diagrams

The chapter should contain at minimum the following diagrams:

### Diagram 42-1 — Prompt Evolution Lifecycle

From Prompt execution through evidence, recommendation, approval, new version, and monitoring.

### Diagram 42-2 — Evidence-to-Prompt Recommendation Flow

Shows build, review, validation, evaluation, PR, and human signals converging into candidate Prompt improvements.

### Diagram 42-3 — Prompt Versioning and Rollback

Shows immutable Prompt versions, active version selection, regression detection, and rollback.

### Diagram 42-4 — Prompt Approval Flow

Shows recommendation, automated evaluation, human review, approval/rejection, rollout, and monitoring.

### Diagram 42-5 — Prompt Evolution inside the Harness

Shows Prompt Registry, runtime, evidence, Harness Memory, Learning Engine, approval, and version publication.

### Optional Diagram 42-6 — Prompt versus Skill versus Instruction Evolution

```text
Task-specific weakness
       ↓
Prompt Evolution

Reusable procedural weakness
       ↓
Skill Evolution

Persistent engineering-rule weakness
       ↓
Instruction Evolution
```

---

# 7. Hands-on Example — Evolving Alpha Car Detailing Prompts

## 7.1 Scenario

The Alpha Car Detailing team maintains multiple governed Prompt templates.

## 7.2 Feature Development Prompt

### Original

Implement the fleet-booking-limit feature and add tests.

### Observed evidence

- architecture review asks where domain rule belongs;
- tests omit concurrency;
- Validator identifies missing API contract test;
- Evaluator identifies incomplete business error handling.

### Candidate improvement

Add explicit requirements for:

- architecture boundary;
- concurrency behavior;
- API compatibility;
- domain tests;
- integration tests;
- acceptance-criteria mapping.

---

## 7.3 Bug-Fixing Prompt

### Original

Fix the duplicate corporate booking issue.

### Recurring failure

Developer Agent changes database uniqueness but ignores:

- retries;
- idempotency;
- duplicate messages;
- client retry behavior.

### Candidate evolution

Require:

- root-cause analysis;
- reproduction test;
- regression test;
- retry analysis;
- idempotency analysis;
- minimal change scope.

---

## 7.4 Testing Prompt

### Original

Add tests for fleet booking.

### Problem

Generated tests focus only on happy path.

### Candidate improvement

Require coverage for:

- authorization;
- booking limits;
- duplicate requests;
- concurrency;
- invalid vehicle;
- cancellation;
- existing booking conflicts.

---

## 7.5 Review Prompt

### Observed problem

Reviewer Agent repeatedly misses event-contract compatibility.

### Candidate improvement

Add targeted review considerations:

- public API changes;
- event schema changes;
- backward compatibility;
- migration risk.

---

## 7.6 Validator Prompt

### Observed problem

Validator executes tests but does not verify required evidence.

### Candidate improvement

Require explicit reporting of:

- build result;
- unit tests;
- integration tests;
- architecture checks;
- security checks;
- API contract checks;
- migration checks.

---

## 7.7 PR Preparation Prompt

### Observed problem

Generated PR summaries omit validation evidence.

### Candidate improvement

Require:

- intent;
- files changed;
- business impact;
- architecture impact;
- validation performed;
- known limitations;
- rollback considerations.

---

## 7.8 Comparative evaluation

Execute:

- current Prompt;
- candidate Prompt.

Across multiple representative Alpha Car Detailing tasks.

### Record

- Validator outcomes;
- Reviewer findings;
- Evaluator outcomes;
- PR corrections;
- human effort.

---

## 7.9 Approval

Prompt owner reviews evidence.

### Decision

Candidate:

- approved;
- rejected;
- revised;
- limited to experimental use.

---

## 7.10 Rollout

- Publish new version.
- Maintain previous version.
- Monitor outcome metrics.
- Roll back if quality degrades.

---

# 8. Claude Example

## 8.1 Claude-first implementation

Use Claude Code to:

- consume a governed Prompt template;
- populate task-specific context;
- execute work;
- record evidence;
- generate a Prompt-improvement proposal.

## 8.2 Example governed files

```text
prompts/
├── feature-development/
│   ├── v1.0.md
│   ├── v1.1.md
│   └── metadata.yaml
├── bug-fix/
├── testing/
├── review/
├── validation/
└── pull-request/
```

## 8.3 Recommendation artifact

```text
harness/
└── recommendations/
    └── prompts/
        └── feature-development-v1.2-proposal.md
```

## 8.4 Claude's authority

Claude may:

- analyze evidence;
- suggest improvements;
- produce candidate versions;
- explain rationale;
- evaluate candidate behavior.

Claude must not:

- silently replace the governed active Prompt;
- approve its own governance change;
- erase historical versions.

---

# 9. GitHub Copilot Comparison

## 9.1 Common principles

Prompt Evolution remains:

- evidence-based;
- governed;
- version-controlled;
- human-approved.

## 9.2 Differences in implementation surfaces

Discuss Prompt evolution across:

- repository prompt files;
- reusable instructions;
- agent workflows;
- PR-oriented workflows.

## 9.3 Enterprise concern

Avoid tying the conceptual model to one vendor's Prompt format.

## 9.4 Key message

The Harness owns the learning architecture; vendor tools are execution surfaces.

---

# 10. Codex Comparison

## 10.1 Same governance model

Codex-generated Prompt recommendations remain proposals.

## 10.2 Possible differences

- Prompt packaging.
- Tool usage.
- context delivery.
- agent orchestration.
- evaluation infrastructure.

## 10.3 Portability

Store governed Prompt intent independently from a specific AI provider wherever practical.

---

# 11. Best Practices

- Treat Prompt Evolution as an engineering lifecycle.
- Keep Prompt templates version-controlled.
- Associate every run with the Prompt version used.
- Require evidence before recommending changes.
- Prefer repeated patterns over isolated failures.
- Keep task-specific information in Prompts.
- Do not duplicate persistent Instructions unnecessarily.
- Keep reusable procedural behavior in Skills.
- Define Prompt ownership.
- Use representative Prompt tests.
- Compare candidate Prompt versions against a baseline.
- Maintain rollback capability.
- Record approval decisions.
- Monitor Prompt effectiveness after release.
- Separate experimental and production Prompt versions.
- Preserve provenance.
- Capture human corrections as evidence.
- Define measurable Prompt success criteria.
- Prefer small, explainable Prompt changes.
- Periodically remove obsolete Prompt content.

---

# 12. Anti-Patterns

## 12.1 Silent Prompt Self-Modification

Harness changes production Prompt automatically.

**Why dangerous:**  
No governance, auditability, or predictable behavior.

---

## 12.2 Learning from One Failure

One failed task immediately changes the Prompt.

**Risk:**  
Overfitting.

---

## 12.3 The Ever-Growing Prompt

Every failure causes another paragraph to be appended.

**Result:**  
Bloated, contradictory, difficult-to-maintain Prompt.

---

## 12.4 Putting Everything into the Prompt

Architecture standards, reusable procedures, and repository knowledge all become Prompt text.

**Result:**  
Loss of separation between Prompt, Skill, Instruction, and Repository Intelligence.

---

## 12.5 Optimizing Only for Validator Passes

Prompt is tuned to pass deterministic gates while missing the business goal.

---

## 12.6 Optimizing Only for Human Preference

Prompt is changed based solely on subjective reviewer style.

---

## 12.7 No Version Tracking

Teams cannot determine which Prompt produced a problematic change.

---

## 12.8 No Rollback

A bad Prompt version affects all future tasks.

---

## 12.9 Model-Specific Prompt Lock-In

All Prompt governance becomes dependent on vendor-specific syntax.

---

## 12.10 Self-Approval

Learning engine recommends, validates, approves, and publishes its own Prompt modifications.

---

## 12.11 Treating Harness Memory as Authority

Recorded history is assumed to be correct without governance.

---

# 13. Architect's Notes

### Architect's Note — Prompts Are Governed Engineering Assets

Prompts should increasingly be treated like other versioned engineering assets rather than transient chat text.

### Architect's Note — Evolution Requires Causality

Correlation between a Prompt and a failure does not prove that Prompt wording caused the failure.

### Architect's Note — Keep Boundaries Clear

When a recommendation appears, first decide whether it belongs in:

- Prompt;
- Skill;
- Instruction;
- repository knowledge;
- acceptance criteria;
- validation gate.

### Architect's Note — Optimize for Outcomes

The goal is not a "better sounding Prompt."  
The goal is measurably better engineering results.

### Architect's Note — Evidence Must Be Traceable

A Prompt change without evidence provenance is difficult to defend, audit, or roll back.

---

# 14. Enterprise Tips

### Enterprise Tip — Start with High-Volume Prompts

Prioritize:

- feature development;
- bug fixing;
- test generation;
- review;
- validation;
- PR preparation.

### Enterprise Tip — Use Prompt Registries

Centralize:

- versions;
- ownership;
- status;
- metrics;
- evaluations.

### Enterprise Tip — Require Minimum Evidence

Do not open a Prompt change request until a configured evidence threshold is met.

### Enterprise Tip — Maintain Experimental Channels

Allow teams to trial candidate Prompts without immediately affecting production workflows.

### Enterprise Tip — Track Prompt Provenance

Record:

- why it changed;
- who approved it;
- supporting evidence;
- evaluation results.

### Enterprise Tip — Measure Human Rework

Reduction in correction effort is often more meaningful than raw generation volume.

---

# 15. Decision Points

## Decision Point 1 — Is the Problem Actually the Prompt?

Determine whether evidence indicates:

- Prompt deficiency;
- Skill deficiency;
- Instruction deficiency;
- missing knowledge;
- model limitation;
- infrastructure problem.

---

## Decision Point 2 — Is the Evidence Strong Enough?

Ask:

- How frequently has the issue occurred?
- Across how many tasks?
- Across which Prompt versions?
- Is evidence consistent?

---

## Decision Point 3 — Should the Prompt Become More Specific?

Trade-off:

- specificity improves predictability;
- excessive specificity can reduce adaptability.

---

## Decision Point 4 — Should the Change Move to a Skill?

If the issue is procedural and reusable, Skill Evolution may be the correct mechanism.

---

## Decision Point 5 — Should the Change Move to an Instruction?

If it represents a persistent repository-wide standard, Instruction Evolution may be appropriate.

---

## Decision Point 6 — Does the Candidate Require Comparative Evaluation?

Minor wording correction versus behavior-changing modification.

---

## Decision Point 7 — Who Owns Approval?

Determine responsible:

- engineering team;
- architecture;
- platform team;
- security;
- domain owner.

---

## Decision Point 8 — Can the Candidate Be Rolled Back?

No governed Prompt version should be deployed without rollback capability.

---

# 16. Exercises

## Exercise 1 — Identify Prompt Weaknesses

Given an Alpha Car Detailing Prompt:

> Add support for corporate fleet booking.

Identify:

- ambiguities;
- missing context;
- missing acceptance criteria;
- missing validation expectations.

---

## Exercise 2 — Classify Learning Signals

Given:

- build failure;
- Reviewer finding;
- Validator failure;
- Evaluator partial result;
- human correction.

Determine whether each indicates:

- Prompt change;
- Skill change;
- Instruction change;
- no change.

---

## Exercise 3 — Design a Prompt Recommendation

Create a recommendation record containing:

- evidence;
- detected problem;
- proposed change;
- expected improvement;
- risk;
- approval owner.

---

## Exercise 4 — Version a Prompt

Create:

- v1.0;
- v1.1;
- v2.0.

Explain why each version changed.

---

## Exercise 5 — Design an A/B Evaluation

Compare two Alpha Car Detailing bug-fix Prompt versions across five representative defects.

Define:

- test cases;
- metrics;
- acceptance threshold.

---

## Exercise 6 — Design a Rollback Strategy

Define:

- regression criteria;
- rollback trigger;
- active-version pointer;
- audit record.

---

## Exercise 7 — Build Prompt Metrics

Define metrics for:

- quality;
- efficiency;
- stability;
- governance.

---

## Exercise 8 — Separate Prompt, Skill, and Instruction Evolution

Classify ten proposed improvements into:

- Prompt;
- Skill;
- Instruction;
- Repository Intelligence;
- validation gate.

---

## Exercise 9 — Design the Prompt Evolution Component

Create an architecture for:

- evidence ingestion;
- pattern detection;
- recommendation generation;
- candidate evaluation;
- approval;
- version publication.

---

# 17. Interview Questions

1. What is Prompt Evolution?
2. How does Prompt Evolution differ from ordinary Prompt editing?
3. Why should an Adaptive Harness evolve Prompts?
4. What evidence can indicate Prompt weakness?
5. How can build failures contribute to Prompt Evolution?
6. How can review findings influence Prompt recommendations?
7. What role does the Validator Agent play in Prompt Evolution?
8. What role does the Evaluator Agent play?
9. How should pull-request feedback be used?
10. Why should the Harness not automatically modify governed Prompts?
11. How do you detect Prompt ambiguity?
12. How do you determine whether missing information belongs in a Prompt, Skill, or Instruction?
13. What is Prompt effectiveness?
14. What metrics would you use to measure it?
15. How would you version Prompt templates?
16. Why is Prompt rollback important?
17. What is comparative Prompt evaluation?
18. How would you perform A/B evaluation of Prompts?
19. How do you prevent Prompt overfitting?
20. What is the "ever-growing Prompt" anti-pattern?
21. How does Harness Memory support Prompt Evolution?
22. Why should Harness Memory not be authoritative by itself?
23. What does Prompt compatibility mean?
24. How should deprecated Prompt templates be handled?
25. Who should own enterprise Prompt templates?
26. How does Prompt Evolution relate to Skill Evolution?
27. How does Prompt Evolution relate to Instruction Evolution?
28. What evidence is sufficient to justify a Prompt change?
29. How would you implement Prompt Evolution inside an enterprise Harness?
30. How do you ensure Prompt Evolution remains vendor-neutral?

---

# 18. Chapter Summary

Summarize the chapter around the following conclusions:

- Prompts are task-specific engineering assets.
- Prompts can become incomplete or ineffective as systems evolve.
- Adaptive Harnesses can learn from repeated execution evidence.
- Builds, tests, reviews, Validators, Evaluators, pull requests, and human feedback provide learning signals.
- Not every failure is a Prompt failure.
- The Harness must distinguish Prompt problems from:
  - Skill problems;
  - Instruction problems;
  - knowledge problems;
  - model limitations;
  - infrastructure failures.
- Candidate Prompt improvements should be evidence-backed.
- Prompt templates should be version-controlled.
- Prompt changes should be tested.
- Comparative evaluation can demonstrate whether a candidate is actually better.
- Compatibility and rollback are essential.
- Prompt ownership must be explicit.
- Prompt changes require human approval.
- Harness Memory supplies evidence but not authority.
- Prompt Evolution transforms recurring engineering corrections into governed organizational learning.
- The Adaptive Harness recommends improvements; it does not silently redefine engineering behavior.

---

# 19. Further Reading

Organize references around:

### Internal handbook references

- Chapter 6 — Instructions
- Chapter 7 — Skills
- Chapter 8 — Prompts
- Chapter 9 — Roles
- Chapter 10 — Steering Notes
- Chapter 11 — Knowledge Sources
- Chapter 13 — Harness Architecture
- Chapter 16 — Reviewer Agent
- Chapter 17 — Validator Agent
- Chapter 18 — Evaluator Agent
- Chapter 20 — Metrics
- Chapter 29 — AI Governance
- Chapter 31 — Human Approval
- Chapter 32 — Audit Trails
- Chapter 36 — Static vs Adaptive Harnesses
- Chapter 37 — Harness Memory
- Chapter 38 — Learning from Builds
- Chapter 39 — Learning from Reviews
- Chapter 40 — Learning from Pull Requests
- Chapter 41 — Skill Evolution
- Chapter 43 — Instruction Evolution
- Chapter 44 — Knowledge Graph
- Chapter 45 — Autonomous Improvement Engine

### External topic areas

- Prompt lifecycle management.
- AI evaluation frameworks.
- Experiment design.
- A/B testing.
- configuration versioning.
- software governance.
- provenance and auditability.
- continuous improvement systems.
- human-in-the-loop AI.
- enterprise AI observability.

---

**Chapter 42 outline is complete.**
