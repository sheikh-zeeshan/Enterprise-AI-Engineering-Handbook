# Chapter 40 — Learning from Pull Requests  

**Part VI — Self-Learning AI Systems**  
**Target file:** `book/40-learning-from-pull-requests.md`

This outline follows the handbook’s frozen enterprise style, terminology, self-learning philosophy, and standard chapter structure. The handbook explicitly defines **Learning from PRs** as part of the Self-Learning Harness while requiring that the Harness **never silently changes standards** and that human approval remains mandatory. AI development handbook The chapter also follows the frozen 19-section chapter template. AI development handbook

---

## 1. Story-Driven Opening — The Pull Request That Keeps Coming Back

### 1.1 Alpha Car Detailing Scenario

- Introduce a new corporate fleet booking enhancement.
- Developer Agent completes implementation.
- Reviewer Agent identifies architecture concerns.
- Validator reports passing deterministic checks.
- Security review requests authorization changes.
- Human reviewer requests modifications to API behavior.
- PR receives multiple review cycles before approval.
- Similar problems have appeared in several previous PRs.

### 1.2 The Missed Learning Opportunity

- Individual PR is eventually merged.
- The engineering issue appears resolved.
- The Harness begins the next task with no awareness of:
  - repeated review comments,
  - recurring architecture violations,
  - repeated missing tests,
  - authorization mistakes,
  - inefficient implementation approaches.
- Same defects reappear in future work.

### 1.3 From Delivery Artifact to Learning Evidence

- A pull request is more than a merge mechanism.
- It records the interaction between:
  - implementation,
  - review,
  - validation,
  - architecture,
  - security,
  - business acceptance,
  - human judgment.
- PR history becomes lifecycle evidence for Adaptive Harness improvement.

### 1.4 Core Principle
>
> Pull requests provide valuable lifecycle evidence, but the Harness should learn from patterns across PRs rather than treating a single PR outcome as a permanent engineering rule.

### 1.5 Chapter Question

- How can an Adaptive Harness use PR evidence to improve future engineering runs without turning individual reviewer opinions or isolated outcomes into permanent standards?

---

# 2. Learning Objectives

By the end of the chapter, readers should be able to:

- Explain why pull-request evidence is valuable to an Adaptive Harness.
- Describe the PR lifecycle as a learning source.
- Identify useful PR metadata and evidence.
- Distinguish accepted implementation evidence from rejected implementation evidence.
- Analyze review comments and requested changes.
- Identify repeated PR issues and rework patterns.
- Classify root causes behind PR changes.
- Correlate PR outcomes with:
  - Prompts,
  - Skills,
  - Instructions,
  - Agent decisions,
  - Harness configuration.
- Generate candidate improvement recommendations.
- Explain how PR evidence enters Harness Memory.
- Define confidence thresholds for learning.
- Prevent false or premature learning.
- Apply human approval before promoting recommendations.
- Design auditable PR-learning workflows.
- Integrate PR learning into the broader Adaptive Harness architecture.

---

# 3. Background

## 3.1 Pull Requests in Enterprise Engineering

Explain the traditional PR role:

- change proposal,
- collaboration boundary,
- review mechanism,
- validation checkpoint,
- governance control,
- merge authorization,
- historical record.

## 3.2 Pull Requests in AI-Assisted Engineering

Explain how AI-generated implementations increase the importance of PR evidence.

Typical contributors:

- Lead Agent,
- Developer Agent,
- Reviewer Agent,
- Validator Agent,
- Evaluator Agent,
- human developers,
- architects,
- security teams,
- CI/CD systems.

## 3.3 Why Pull-Request Evidence Matters

PRs contain evidence not normally available from source code alone:

- what was originally implemented,
- what reviewers challenged,
- what was changed,
- why it was changed,
- which validation checks failed,
- which architectural concerns appeared,
- whether implementation was ultimately accepted.

## 3.4 PR Evidence as Lifecycle Evidence

Differentiate:

**Build evidence**

- Did the software compile and pass deterministic checks?

**Review evidence**

- Was implementation quality acceptable?

**PR evidence**

- What happened to the implementation across the complete approval lifecycle?

## 3.5 Pull Requests and the Adaptive Harness

Connect Chapter 40 with:

- Chapter 36 — Static vs Adaptive Harnesses
- Chapter 37 — Harness Memory
- Chapter 38 — Learning from Builds
- Chapter 39 — Learning from Reviews
- Chapter 41 — Skill Evolution
- Chapter 42 — Prompt Evolution
- Chapter 43 — Instruction Evolution
- Chapter 44 — Knowledge Graph
- Chapter 45 — Autonomous Improvement Engine

The book structure places Learning from Pull Requests directly between Learning from Reviews and Skill Evolution, making it a bridge between collected engineering evidence and governed improvement of the Harness. AI development handbook

---

# 4. Concepts

## 4.1 What Learning from Pull Requests Means

Define PR learning as:

> The process of collecting, classifying, correlating, and evaluating pull-request lifecycle evidence to identify recurring engineering patterns and propose improvements to the Harness.

Clarify what it is **not**:

- copying reviewer comments into Instructions,
- automatically changing Skills,
- treating every rejected PR as bad engineering,
- treating every merged PR as correct engineering,
- learning from isolated events without context.

---

## 4.2 The Pull-Request Lifecycle

Define the lifecycle:

```text
Implementation
      ↓
PR Created
      ↓
Automated Checks
      ↓
Review
      ↓
Requested Changes
      ↓
Rework
      ↓
Revalidation
      ↓
Approval / Rejection
      ↓
Merge / Close
      ↓
Learning Analysis
```

Cover:

- PR creation,
- automated validation,
- human review,
- automated review,
- change requests,
- revisions,
- approvals,
- rejection,
- merge,
- closure,
- post-merge signals.

---

## 4.3 PR Metadata

Potential evidence:

- PR identifier,
- repository,
- branch,
- author,
- originating agent,
- Harness run ID,
- task ID,
- Prompt ID/version,
- Skill version,
- Instruction version,
- commit history,
- files changed,
- lines changed,
- labels,
- reviewers,
- timestamps,
- status,
- merge strategy.

Discuss metadata governance and relevance.

---

## 4.4 Review Comments

Classify comments such as:

- correctness,
- maintainability,
- architecture,
- naming,
- testing,
- security,
- observability,
- performance,
- API design,
- data access,
- documentation,
- business logic.

Distinguish:

- informational comments,
- suggestions,
- blockers,
- preferences,
- policy violations,
- confirmed defects.

---

## 4.5 Requested Changes

Examine:

- what was requested,
- who requested it,
- why,
- whether it was accepted,
- what code changed afterward,
- whether similar changes occurred in other PRs.

Introduce:

**requested change → modification → outcome**

as a high-value learning chain.

---

## 4.6 Approvals and Rejections

Explain why approval alone is insufficient evidence.

An approved PR might still contain:

- technical debt,
- consciously accepted risk,
- temporary workaround,
- time-boxed compromise.

A rejected PR might reflect:

- wrong implementation,
- changed requirements,
- duplicate work,
- obsolete branch,
- business reprioritization.

Therefore:

```text
Approval ≠ universal correctness
Rejection ≠ universal failure
```

---

## 4.7 Merge Outcomes

Possible outcomes:

- merged unchanged,
- merged after minor revision,
- merged after significant rework,
- superseded,
- abandoned,
- rejected,
- closed because requirements changed.

Discuss merge outcome as one signal among many.

---

## 4.8 Build and Validation Results

Connect PR evidence to deterministic gates:

- build status,
- test results,
- architecture tests,
- static analysis,
- contract validation,
- migration checks,
- security scanning.

Clarify:

PR learning consumes validation evidence but does not replace the Validator Agent.

---

## 4.9 Architecture Findings

Examples:

- layer violation,
- domain boundary violation,
- cross-service coupling,
- inappropriate dependency,
- repository abstraction misuse,
- transaction boundary issue,
- integration-pattern violation.

Discuss repeated architecture findings as strong learning candidates.

---

## 4.10 Security Findings

Examples:

- missing authorization,
- insecure endpoint exposure,
- secret leakage,
- unsafe logging,
- improper validation,
- excessive permissions,
- insecure dependency.

Distinguish:

- deterministic security gate findings,
- security reviewer observations,
- accepted risk decisions.

---

## 4.11 Rework Patterns

Measure:

- commits after first review,
- changed files after requested changes,
- repeated test additions,
- repeated authorization fixes,
- repeated API contract corrections.

Introduce concept:

**PR Rework Signature**

---

## 4.12 Time to Approval

Possible measurements:

- PR creation → first review,
- first review → first change,
- requested change → resolution,
- PR creation → approval,
- PR creation → merge.

Explain that time must be interpreted carefully.

Long duration may reflect:

- difficult implementation,
- delayed reviewer availability,
- external dependency,
- architecture disagreement,
- unclear requirements.

---

## 4.13 Repeated PR Issues

Examples in Alpha Car Detailing:

- missing fleet authorization checks,
- inconsistent validation,
- incorrect transaction boundaries,
- missing integration tests,
- event schema inconsistencies.

Repeated evidence increases confidence.

---

## 4.14 Root-Cause Classification

Possible root causes:

- Prompt weakness,
- Skill weakness,
- missing Instruction,
- ambiguous Instruction,
- outdated Repository Intelligence,
- incomplete Steering Note,
- Developer Agent reasoning failure,
- Reviewer inconsistency,
- Validator gap,
- architecture ambiguity,
- requirement ambiguity,
- environment issue.

Introduce structured classification rather than free-form conclusions.

---

## 4.15 Learning from Accepted PRs

Look for:

- effective architectural patterns,
- successful Skill usage,
- reliable Prompts,
- low-rework implementations,
- fast approval with strong validation evidence.

Accepted PRs can reinforce proven approaches.

---

## 4.16 Learning from Rejected PRs

Study:

- rejection reason,
- requirements drift,
- design mistakes,
- recurring code defects,
- unsuitable Skill usage,
- Prompt misunderstanding.

Stress that rejection requires contextual classification.

---

## 4.17 Correlation with Prompts, Skills, and Agent Decisions

Track evidence back to:

```text
PR
 ↓
Harness Run
 ↓
Agent Decision
 ↓
Skill
 ↓
Prompt
 ↓
Instruction Context
```

Questions:

- Which Prompt generated the work?
- Which Skill was used?
- Which Instruction governed it?
- Which decisions were taken by the Developer Agent?
- Did the Reviewer already detect the issue?
- Did deterministic gates miss it?

---

## 4.18 Candidate Improvement Patterns

Examples:

- repeated missing tests,
- repeated wrong authorization pattern,
- frequent architecture-layer violation,
- recurring unnecessary rework,
- repeated reviewer correction.

Candidate improvement types:

- Skill recommendation,
- Prompt recommendation,
- Instruction proposal,
- validation gate recommendation,
- workflow recommendation,
- Repository Intelligence update.

---

## 4.19 Harness Memory

PR-learning observations should initially enter memory as evidence, not authoritative standards.

Potential memory records:

```text
Pattern:
Missing fleet authorization validation

Observed:
7 PRs

Repositories:
AlphaCarDetailing.Booking

Source:
Security and architecture reviews

Confidence:
High

Recommended action:
Update fleet-booking Skill

Status:
Pending approval
```

Reinforce separation between:

- observation,
- hypothesis,
- recommendation,
- approved knowledge.

---

## 4.20 Skill Recommendations

Examples:

- add authorization scaffolding,
- add mandatory integration test steps,
- add API contract validation,
- improve transaction handling.

Connect to Chapter 41 — Skill Evolution.

---

## 4.21 Prompt Recommendations

Examples:

- clarify acceptance criteria,
- explicitly request authorization requirements,
- specify contract compatibility,
- request required test categories.

Connect to Chapter 42 — Prompt Evolution.

---

## 4.22 Instruction Improvement Proposals

Examples:

- formalize service boundary requirements,
- require structured logging,
- clarify API authorization conventions.

Important:

The Harness proposes changes.

It does not directly rewrite enterprise Instructions.

This follows the handbook's established rule that the Harness recommends improvements and human approval is mandatory. AI development handbook

---

## 4.23 Workflow Improvements

Examples:

- architecture check before coding,
- security evaluation before PR creation,
- validator execution before reviewer handoff,
- additional evidence collection,
- early contract validation.

---

## 4.24 Confidence Thresholds

Possible confidence levels:

- Low,
- Medium,
- High,
- Very High.

Possible factors:

- number of PR occurrences,
- reviewer agreement,
- repository coverage,
- recency,
- deterministic evidence,
- consistency,
- severity.

Example:

```text
Single PR
→ observation

Repeated across 3 PRs
→ emerging pattern

Repeated across 10 PRs + deterministic evidence
→ high-confidence recommendation
```

Avoid rigid universal numbers; thresholds should be enterprise-configurable.

---

## 4.25 Human Approval

Potential approvers:

- Tech Lead,
- Architect,
- Security Lead,
- Engineering Manager,
- Platform Owner,
- Harness Owner.

Humans approve promotion into governed engineering knowledge.

---

## 4.26 Auditability

Record:

- source PRs,
- evidence,
- classification,
- confidence,
- recommendation,
- approver,
- decision,
- timestamp,
- resulting artifact version.

---

## 4.27 Avoiding False Learning

Sources of false learning:

- isolated reviewer preference,
- obsolete PR,
- emergency workaround,
- experimental branch,
- abandoned design,
- outdated architecture,
- changed requirement,
- reviewer inconsistency.

Introduce the principle:

> Frequency without context is not knowledge.

---

# 5. Architecture Discussion

## 5.1 PR Learning Architecture

Primary components:

```text
Pull Request Platform
        ↓
PR Evidence Collector
        ↓
Evidence Normalizer
        ↓
Classifier
        ↓
Correlation Engine
        ↓
Pattern Detector
        ↓
Confidence Engine
        ↓
Harness Memory
        ↓
Recommendation Engine
        ↓
Human Approval
        ↓
Governed Improvement
```

---

## 5.2 PR Evidence Collector

Responsibilities:

- collect PR metadata,
- review threads,
- status changes,
- commits,
- validation checks,
- approval events,
- merge events.

---

## 5.3 Evidence Normalizer

Convert provider-specific data into vendor-neutral records.

Potential sources:

- GitHub,
- GitLab,
- Azure DevOps,
- enterprise SCM platforms.

Example conceptual model:

```text
PullRequestEvidence
ReviewEvidence
ValidationEvidence
MergeEvidence
DecisionEvidence
```

---

## 5.4 Classification Engine

Classify:

- findings,
- changes,
- reviewer intent,
- root cause,
- severity,
- affected engineering artifact.

---

## 5.5 Correlation Engine

Correlate PR with:

- Harness run,
- Prompt,
- Skill,
- Instruction version,
- agent execution,
- build results,
- validation results,
- previous similar PRs.

---

## 5.6 Pattern Detection

Detect:

- repeated defects,
- recurring requested changes,
- repeated architecture corrections,
- frequent rework,
- successful implementation patterns.

---

## 5.7 Confidence Engine

Evaluate:

```text
Frequency
+ Consistency
+ Evidence Quality
+ Reviewer Agreement
+ Validation Support
+ Recency
+ Scope
```

---

## 5.8 Recommendation Engine

Possible outputs:

```text
SkillImprovementProposal
PromptImprovementProposal
InstructionChangeProposal
ValidationGateProposal
WorkflowImprovementProposal
KnowledgeCandidate
```

---

## 5.9 Governance Boundary

Critical architecture boundary:

```text
Learning Engine
     ↓
Recommendation
     ↓
Human Approval
     ↓
Governed Artifact
```

Never:

```text
PR Comment
     ↓
Automatic Instruction Change
```

---

# 6. Professional Diagrams

## Diagram 40.1 — PR Learning Lifecycle

```text
Implementation
      │
      ▼
Pull Request
      │
      ▼
Automated Validation
      │
      ▼
Review
      │
      ├──────────────┐
      ▼              │
Requested Changes    │
      │              │
      ▼              │
Rework               │
      │              │
      └──────► Revalidation
                     │
                     ▼
              Approval / Rejection
                     │
                     ▼
                 Merge / Close
                     │
                     ▼
              Evidence Collection
                     │
                     ▼
               Pattern Analysis
                     │
                     ▼
               Harness Learning
```

---

## Diagram 40.2 — PR Evidence Flow

```text
PR Metadata ──────────────┐
Review Comments ──────────┤
Requested Changes ────────┤
Validation Results ───────┤
Architecture Findings ────┤
Security Findings ────────┤
Merge Outcome ────────────┤
Rework History ───────────┘
            │
            ▼
      Evidence Store
            │
            ▼
       Normalization
            │
            ▼
       Classification
            │
            ▼
        Correlation
            │
            ▼
      Pattern Detection
```

---

## Diagram 40.3 — PR-to-Harness-Memory Promotion

```text
PR Evidence
    │
    ▼
Observation
    │
    ▼
Repeated Observation
    │
    ▼
Candidate Pattern
    │
    ▼
Confidence Assessment
    │
    ▼
Harness Memory Candidate
    │
    ▼
Human Review
    │
    ├── Reject
    │
    ├── Retain as Observation
    │
    └── Approve
           │
           ▼
Governed Harness Knowledge
```

---

## Diagram 40.4 — Recommendation and Approval Flow

```text
Detected Pattern
      │
      ▼
Root-Cause Analysis
      │
      ▼
Recommendation
      │
      ├── Skill Proposal
      ├── Prompt Proposal
      ├── Instruction Proposal
      ├── Gate Proposal
      └── Workflow Proposal
              │
              ▼
        Human Approval
              │
      ┌───────┼────────┐
      ▼       ▼        ▼
   Reject   Revise   Approve
                       │
                       ▼
               Versioned Change
                       │
                       ▼
                Future Harness Run
```

---

## Diagram 40.5 — Learning from Pull Requests Inside the Harness

```text
                   ┌─────────────────────┐
                   │      User Task      │
                   └──────────┬──────────┘
                              ▼
                         Lead Agent
                              │
                              ▼
                       Developer Agent
                              │
                              ▼
                       Reviewer Agent
                              │
                              ▼
                       Validator Agent
                              │
                              ▼
                       Evaluator Agent
                              │
                              ▼
                          Create PR
                              │
               ┌──────────────┴───────────────┐
               ▼                              ▼
        Automated Checks                Human Review
               │                              │
               └──────────────┬───────────────┘
                              ▼
                        PR Outcome
                              │
                              ▼
                       Evidence Collector
                              │
                              ▼
                        Learning Engine
                              │
                ┌─────────────┼──────────────┐
                ▼             ▼              ▼
           PR Patterns   Root Causes    Correlations
                │             │              │
                └─────────────┼──────────────┘
                              ▼
                       Harness Memory
                              │
                              ▼
                    Recommendation Engine
                              │
                              ▼
                       Human Approval
                              │
                              ▼
            Skills / Prompts / Instructions /
              Gates / Workflow Improvements
                              │
                              ▼
                      Future Harness Runs
```

---

# 7. Hands-on Example — Alpha Car Detailing

## 7.1 Scenario

Corporate fleet booking requires:

- fleet authorization,
- booking-limit validation,
- audit logging,
- integration tests.

## 7.2 PR History

Analyze several PRs where:

- authorization logic was repeatedly omitted,
- reviewers repeatedly requested authorization checks,
- validation gates passed because authorization behavior was not tested,
- implementation was corrected before merge.

## 7.3 Evidence Collection

Example evidence:

```text
PR #412
Issue: Missing fleet authorization
Source: Security reviewer
Outcome: Fixed before merge

PR #438
Issue: Missing fleet authorization
Source: Architecture reviewer
Outcome: Fixed before merge

PR #461
Issue: Missing fleet authorization
Source: Human reviewer
Outcome: Fixed before merge
```

## 7.4 Pattern Detection

Candidate pattern:

```text
Repeated omission:
Fleet authorization validation
```

## 7.5 Root-Cause Analysis

Potential causes:

- Prompt does not mention authorization.
- Booking Skill lacks authorization step.
- Validator lacks security behavior test.
- Repository Instructions may be insufficient.

## 7.6 Recommendation

Example:

```text
Recommendation:
Update corporate-fleet-booking Skill to include authorization verification.

Confidence:
High

Supporting PRs:
#412, #438, #461

Action:
Human review required.
```

## 7.7 Approval

Architect reviews evidence and approves Skill evolution.

## 7.8 Future Run

Next implementation automatically uses the improved Skill.

Explain closed learning loop.

---

# 8. Claude Example

## 8.1 Claude as PR Evidence Analyst

Show conceptual workflow in which Claude:

- reads structured PR evidence,
- groups similar comments,
- identifies recurring patterns,
- correlates patterns with Skills and Prompts,
- produces recommendations.

## 8.2 Example Analysis Prompt

Conceptually request Claude to:

- summarize repeated issues,
- separate evidence from inference,
- identify root causes,
- assign confidence,
- recommend candidate improvements.

## 8.3 Claude Guardrails

Claude must not:

- automatically rewrite Instructions,
- promote one reviewer preference into policy,
- ignore conflicting evidence,
- infer causation from weak correlation.

## 8.4 Structured Output

Example categories:

```text
Observation
Evidence
Pattern
Root Cause Hypothesis
Confidence
Affected Artifact
Recommendation
Approval Required
```

---

# 9. GitHub Copilot Comparison

Discuss where GitHub Copilot fits materially differently.

Cover:

- pull-request review workflows,
- repository context,
- PR suggestions,
- developer-facing feedback,
- integration with GitHub-native lifecycle signals.

Emphasize:

- provider-specific capabilities may simplify evidence collection,
- Harness learning architecture remains vendor-neutral.

Compare:

```text
GitHub-native PR intelligence
vs.
Harness-wide enterprise learning
```

---

# 10. Codex Comparison

Discuss Codex-based workflows where applicable:

- coding-agent implementation,
- code review,
- repository interaction,
- execution evidence.

Explain that PR learning belongs primarily to the enterprise Harness rather than any single coding-agent product.

Focus on architectural portability.

---

# 11. Best Practices

Cover practices such as:

- collect complete lifecycle evidence,
- normalize provider-specific PR data,
- retain source references,
- classify comments before learning,
- distinguish preference from requirement,
- correlate PR evidence with Harness runs,
- learn from multiple PRs,
- track positive and negative patterns,
- apply confidence thresholds,
- require human approval,
- version recommendations,
- preserve rejected recommendations,
- monitor whether approved improvements actually reduce future defects.

---

# 12. Anti-Patterns

## 12.1 One PR Becomes a Rule

Turning a single reviewer request into an Instruction.

## 12.2 Merge Equals Correct

Assuming merged code is always exemplary.

## 12.3 Rejection Equals Bad Code

Ignoring business or requirement changes.

## 12.4 Reviewer Preference Learning

Learning stylistic preferences as enterprise standards.

## 12.5 Comment Counting

Treating frequency alone as proof.

## 12.6 Missing Root-Cause Analysis

Fixing symptoms instead of source artifacts.

## 12.7 Automatic Instruction Mutation

Allowing the Harness to rewrite standards without governance.

## 12.8 Ignoring Successful PRs

Learning only from defects.

## 12.9 Learning Without Version Context

Comparing PRs governed by different architecture versions as equivalent.

## 12.10 No Feedback Measurement

Changing a Skill but never checking whether PR quality improved.

---

# 13. Architect's Notes

Include architectural guidance around:

- PR evidence as historical engineering telemetry,
- temporal context,
- repository scope,
- confidence modeling,
- provenance,
- false causation,
- organizational versus repository-specific learning.

Key principle:

> The goal is not to make the Harness remember every PR. The goal is to make it recognize durable engineering patterns.

Discuss enterprise boundary:

```text
Observation
≠
Recommendation
≠
Approved Standard
```

---

# 14. Enterprise Tips

Cover:

- start with high-value PR signals,
- focus initially on repeated requested changes,
- capture reviewer role and authority,
- associate every PR with Harness run identifiers,
- create consistent review taxonomies,
- separate security and architecture findings,
- retain evidence links,
- establish approval ownership,
- periodically expire stale learning,
- measure reduction in PR rework after changes.

---

# 15. Decision Points

## Decision Point 1

Should the Harness learn from every PR or only Harness-generated PRs?

## Decision Point 2

How many repeated observations are required before recommendation?

## Decision Point 3

Which reviewers carry authoritative signals?

## Decision Point 4

Should comment categories be manually defined or AI-classified?

## Decision Point 5

When should PR evidence enter Harness Memory?

## Decision Point 6

What requires Architect approval versus Team Lead approval?

## Decision Point 7

Should learning be repository-specific or organization-wide?

## Decision Point 8

When should old PR evidence expire?

## Decision Point 9

How should conflicting reviewer feedback be handled?

## Decision Point 10

When should a recurring PR issue become a deterministic gate?

---

# 16. Exercises

## Exercise 1 — PR Evidence Inventory

Identify all evidence produced by an enterprise PR.

## Exercise 2 — Comment Classification

Classify review comments into:

- defect,
- architecture,
- security,
- preference,
- test gap,
- business requirement.

## Exercise 3 — Pattern Detection

Analyze ten fictional Alpha Car Detailing PRs and identify repeated issues.

## Exercise 4 — Root-Cause Mapping

Map recurring PR findings to:

- Prompt,
- Skill,
- Instruction,
- Agent,
- Validator,
- workflow.

## Exercise 5 — Confidence Model

Design a confidence-scoring model for PR recommendations.

## Exercise 6 — False Learning

Identify scenarios where repeated review comments should **not** become Harness knowledge.

## Exercise 7 — Skill Evolution Proposal

Create a Skill improvement proposal from repeated fleet-authorization issues.

## Exercise 8 — Governance Workflow

Design the approval workflow for promoting PR learning into governed knowledge.

## Exercise 9 — Architecture Design

Design the PR Evidence Collector and Pattern Detection components.

## Exercise 10 — Enterprise Metrics

Define metrics for measuring whether PR-driven Harness improvements are effective.

---

# 17. Interview Questions

Include questions such as:

1. Why are pull requests useful learning sources for an Adaptive Harness?
2. How does PR learning differ from review learning?
3. Why should a merged PR not automatically represent a best practice?
4. Why should a rejected PR not automatically represent bad engineering?
5. What PR metadata is useful for Harness learning?
6. How should review comments be classified?
7. What is a PR rework pattern?
8. How can PR evidence be correlated with Skills and Prompts?
9. What role does Harness Memory play?
10. How should confidence thresholds be applied?
11. How do you prevent false learning?
12. Why is human approval necessary?
13. When should a recurring PR issue become an Instruction?
14. When should it instead become a Skill improvement?
15. When should it become a deterministic gate?
16. How do you handle conflicting reviewers?
17. What evidence should be retained for auditability?
18. How can time-to-approval be misinterpreted?
19. How do you measure whether PR learning improved future engineering runs?
20. How would you architect PR learning for multiple repositories and SCM platforms?

---

# 18. Chapter Summary

Reinforce:

- Pull requests are rich lifecycle evidence.
- PR learning goes beyond comments and merge status.
- Both successful and unsuccessful PRs provide useful information.
- Requested changes and rework patterns reveal recurring engineering weaknesses.
- PR evidence should be correlated with:
  - Prompts,
  - Skills,
  - Instructions,
  - agent decisions,
  - validation outcomes.
- Root-cause classification matters more than raw comment frequency.
- Repeated evidence creates candidate patterns.
- Candidate patterns can enter Harness Memory.
- Recommendations may target:
  - Skills,
  - Prompts,
  - Instructions,
  - gates,
  - workflows.
- Confidence must increase through repeated, contextual evidence.
- The Harness recommends improvements rather than silently changing standards.
- Human approval preserves governance.
- Auditability ensures every learned recommendation can be traced back to evidence.
- PR learning becomes an important input into the following chapters on Skill, Prompt, and Instruction evolution.

---

# 19. Further Reading

Organize further reading around:

### Pull-Request Engineering

- code-review practices,
- branch policies,
- merge governance,
- PR lifecycle management.

### Software Analytics

- repository mining,
- engineering telemetry,
- developer productivity research,
- change-history analysis.

### Adaptive Systems

- feedback loops,
- pattern detection,
- confidence scoring,
- continuous improvement.

### AI Engineering

- agent evaluation,
- Harness Memory,
- reinforcement through evidence,
- human-in-the-loop governance.

### Enterprise Governance

- audit trails,
- change management,
- policy evolution,
- approval workflows.

### Related Handbook Chapters

- Chapter 36 — Static vs Adaptive Harnesses
- Chapter 37 — Harness Memory
- Chapter 38 — Learning from Builds
- Chapter 39 — Learning from Reviews
- Chapter 41 — Skill Evolution
- Chapter 42 — Prompt Evolution
- Chapter 43 — Instruction Evolution
- Chapter 44 — Knowledge Graph
- Chapter 45 — Autonomous Improvement Engine

---

**Chapter 40 outline is complete.**
