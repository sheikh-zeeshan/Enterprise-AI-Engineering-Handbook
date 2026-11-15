# **Chapter 36 — Static vs Adaptive Harnesses**

**Part VI — Self-Learning AI Systems**

**Target file:** `book/36-static-vs-adaptive-harnesses.md`

**Project:** Enterprise AI Engineering Handbook

**Running Example:** Alpha Car Detailing

## **Chapter Objective**

Explain how Static and Adaptive Harnesses differ in architecture, execution behavior, learning capabilities, operational complexity, and governance.

Establish how an enterprise Harness can learn from execution evidence, identify recurring engineering problems, and recommend improvements to Skills, Prompts, Instructions, and workflows without independently modifying governed engineering standards.

**Core principle:**

> A Static Harness executes predefined workflows and engineering rules. An Adaptive Harness learns from execution evidence and recommends improvements. Changes to governed behavior require explicit human approval, controlled implementation, validation, versioning, and auditability.

## **1. Story-Driven Opening**

### **1.1 The Alpha Car Detailing Scenario**

- Introduce the existing AI Engineering Harness used to develop Alpha Car Detailing.
- Describe the repeated implementation of corporate fleet booking enhancements.
- Explain recurring validation failures involving booking limits, API contracts, and integration tests.
- Show how the Static Harness detects failures and retries execution using its existing Instructions and Skills.
- Explain why repeated failures may indicate opportunities to improve the engineering workflow.

### **1.2 The Limitation of Repeated Execution**

- Deterministic execution provides consistency but does not inherently improve the workflow.
- Repeated failures across independent runs.
- Reviewer findings that identify the same architectural issues.
- Recurring Developer Agent corrections.
- Unnecessary retries and accumulated execution costs.
- The difference between correcting an implementation and improving the Harness responsible for producing implementations.

### **1.3 Introducing an Adaptive Harness**

- Capture execution evidence across runs.
- Identify recurring failure patterns.
- Generate candidate improvements.
- Evaluate recommendations against existing engineering standards.
- Submit proposed changes for human review.
- Apply approved improvements through a controlled change process.
- Measure whether subsequent executions demonstrate improvement.

### **1.4 The Central Enterprise Question**

How can an enterprise Harness improve over time without becoming an uncontrolled system that changes its own engineering standards?

## **2. Learning Objectives**

After completing this chapter, readers should be able to:

1. Define Static and Adaptive Harnesses.
2. Explain the architectural and operational differences between them.
3. Identify learning opportunities from Harness execution evidence.
4. Distinguish runtime adaptation from persistent Harness evolution.
5. Identify which Harness components may be optimized and which must remain governed.
6. Explain the role of Harness Memory in supporting evidence-based learning.
7. Design a feedback loop connecting execution, evidence, recommendations, and human approval.
8. Distinguish deterministic validation from probabilistic recommendation generation.
9. Design a controlled improvement lifecycle with versioning, rollback, and audit trails.
10. Apply drift prevention and safety boundaries to adaptive systems.
11. Evaluate whether adopting an Adaptive Harness is justified for an enterprise project.
12. Design an initial Adaptive Harness architecture for Alpha Car Detailing.

## **3. Background**

### **3.1 Evolution of Enterprise Harnesses**

- From manual AI-assisted development to orchestrated agent workflows.
- From individual agent execution to role-based Harness architecture.
- Introduction of deterministic validation gates.
- Collection of execution history and engineering metrics.
- Emergence of learning and continuous improvement capabilities.

### **3.2 Revisiting the Existing Harness Architecture**

- Lead Agent: task coordination and execution planning.
- Developer Agent: implementation.
- Reviewer Agent: independent engineering review.
- Validator Agent: verification of deterministic conditions.
- Evaluator Agent: assessment of goal satisfaction and outcome quality.
- Harness orchestration, retry policies, and execution controls.
- Evidence collection and run history.
- Existing human approval and governance boundaries.

### **3.3 Why Successful Execution Is Not the Same as Learning**

- Producing a correct implementation.
- Recovering from a failed execution.
- Remembering a previous execution.
- Recognizing recurring patterns.
- Recommending a reusable improvement.
- Establishing evidence that an approved improvement produces better outcomes.

### **3.4 Self-Learning Versus Self-Modifying Systems**

- Defining learning in an enterprise engineering context.
- Distinguishing recommendations from autonomous modifications.
- Why the ability to generate a change does not establish authority to apply it.
- Separating learning capability from change authority.
- The importance of human accountability.

## **4. Concepts**

### **4.1 What Is a Static Harness?**

#### **4.1.1 Definition**

- A Harness operating according to predefined workflows, Instructions, Skills, Prompts, and validation policies.
- Configuration and operational behavior governed through explicit changes.
- Execution results do not automatically evolve persistent Harness behavior.

#### **4.1.2 Characteristics**

- Predictable orchestration.
- Explicit execution sequences.
- Predefined retry limits.
- Stable engineering Instructions.
- Deterministic validation gates.
- Controlled dependency and tool selection.
- Reproducible execution configurations.

#### **4.1.3 How a Static Harness Operates**

- Receive goal and task context.
- Load approved Instructions, Skills, and Prompts.
- Execute the agent workflow.
- Run review, validation, and evaluation stages.
- Retry or fail according to predefined policies.
- Record execution evidence.
- Complete the run without automatically learning a new persistent workflow.

#### **4.1.4 Advantages and Limitations**

- Predictability, simplicity, and operational clarity.
- Easier testing and governance.
- Lower learning infrastructure complexity.
- Repeated inefficiencies without systematic improvement.
- Dependence on humans to identify cross-run patterns.

### **4.2 What Is an Adaptive Harness?**

#### **4.2.1 Definition**

- A Harness that analyzes execution evidence across runs.
- Identifies recurring problems and improvement opportunities.
- Generates recommendations for changing future Harness behavior.
- Uses governed mechanisms to approve, apply, validate, and monitor persistent improvements.

#### **4.2.2 Characteristics**

- Evidence-driven learning.
- Cross-run pattern recognition.
- Historical feedback analysis.
- Recommendation generation.
- Harness Memory integration.
- Controlled Skill and Prompt evolution.
- Improvement effectiveness measurement.
- Governed change management.

#### **4.2.3 What Adaptation Does Not Mean**

- Unlimited agent autonomy.
- Automatic rewriting of Instructions.
- Bypassing validation gates.
- Removing security policies.
- Treating generated recommendations as approved decisions.
- Assuming an improved metric proves better engineering quality.

### **4.3 Static Versus Adaptive Harness Behavior**

| Dimension               | Static Harness                         | Adaptive Harness                                    |
| ----------------------- | -------------------------------------- | --------------------------------------------------- |
| Workflow                | Predefined                             | Predefined baseline with proposed improvements      |
| Execution history       | Recorded                               | Analyzed for learning                               |
| Failure handling        | Existing retry and recovery policies   | Existing recovery plus cross-run analysis           |
| Skills                  | Updated through deliberate maintenance | Improvement recommendations generated from evidence |
| Prompts                 | Maintained manually                    | Candidate refinements proposed                      |
| Instructions            | Governed                               | Governed; proposed modifications require approval   |
| Validation gates        | Deterministic and approved             | Deterministic and approved                          |
| Memory                  | Optional execution history             | Structured evidence and learning memory             |
| Feedback                | Operational reporting                  | Inputs to improvement recommendations               |
| Change authority        | Governed change process                | Governed change process                             |
| Improvement measurement | Usually manual                         | Explicit post-change assessment                     |
| Complexity              | Lower                                  | Higher                                              |

### **4.4 Three Levels of Adaptive Behavior**

#### **4.4.1 Run-Level Adaptation**

- Changing execution tactics within approved boundaries.
- Selecting existing Skills based on task requirements.
- Adjusting retry strategies within approved limits.
- Using Validator feedback to correct an implementation.
- No persistent change to governed Harness assets.

#### **4.4.2 Recommendation-Level Adaptation**

- Analyzing historical execution evidence.
- Identifying repeated problems.
- Preparing changes to Prompts, Skills, or workflows.
- Producing supporting evidence and expected outcomes.
- Submitting proposals for review.

#### **4.4.3 Governed Persistent Adaptation**

- Approval of a proposed improvement.
- Version-controlled change implementation.
- Verification through deterministic gates.
- Controlled deployment.
- Post-deployment evaluation.
- Rollback when improvement criteria are not satisfied.

### **4.5 Why Adaptation Is Useful**

- Reduce recurring implementation failures.
- Improve first-pass validation success.
- Reduce unnecessary retry cycles.
- Improve the relevance of reusable Skills.
- Strengthen Prompts through accumulated engineering evidence.
- Reduce repeated Reviewer findings.
- Improve task completion consistency.
- Detect ineffective workflows.
- Reduce operational cost without compromising quality.

### **4.6 What Can Adapt?**

- Prompt wording and task decomposition.
- Skill selection and recommendations for Skill improvements.
- Execution sequencing within approved policies.
- Context retrieval strategies.
- Retry strategies within authorized constraints.
- Tool selection among approved tools.
- Evidence presentation and reporting.
- Candidate test-generation strategies.
- Review checklists through approved revisions.
- Workflow optimization recommendations.

### **4.7 What Must Remain Governed?**

- Enterprise Instructions.
- Architecture standards.
- Security requirements.
- Compliance controls.
- Validation gate definitions.
- Quality thresholds.
- Approval policies.
- Agent permissions.
- Production access controls.
- Sensitive data handling.
- Resource and execution budgets.
- Promotion of recommendations into authoritative knowledge.

### **4.8 Adaptation Scope and Authority**

- Distinguish execution optimization from policy modification.
- Distinguish recommendation ownership from change approval.
- Define authorized, restricted, and prohibited adaptation.
- Define the default-deny posture for persistent changes.
- Ensure adaptation authority cannot be expanded by the learning engine itself.

## **5. Architecture Discussion**

### **5.1 Architectural Requirements for an Adaptive Harness**

- Preserve the existing role-based Harness architecture.
- Add learning without weakening deterministic validation.
- Keep the execution plane separate from the learning plane.
- Introduce a dedicated recommendation process.
- Introduce explicit governance and approval boundaries.
- Maintain versioned Harness assets.
- Support traceability from evidence to approved changes.
- Support rollback to a known-good configuration.

### **5.2 Static Harness Architecture**

- Goal and task intake.
- Approved Instructions and Skills.
- Lead, Developer, Reviewer, Validator, and Evaluator.
- Deterministic gates.
- Execution records and metrics.
- Completion or escalation.

### **5.3 Adaptive Harness Architecture**

#### **5.3.1 Execution Plane**

- Existing Harness orchestration.
- Agent role execution.
- Approved tool invocation.
- Deterministic gates.
- Run-scoped scratchpad and workspace.
- Execution outcomes.

#### **5.3.2 Evidence Collection Plane**

- Build and test outputs.
- Reviewer findings.
- Validator results.
- Evaluator assessments.
- Retry histories.
- Human feedback.
- Execution metrics.
- Artifact and version references.

#### **5.3.3 Learning Plane**

- Evidence normalization.
- Pattern classification.
- Recurrence detection.
- Root-cause hypothesis generation.
- Confidence and evidence-strength assessment.
- Recommendation generation.
- Duplicate recommendation detection.

#### **5.3.4 Governance Plane**

- Recommendation triage.
- Human review.
- Policy and architecture checks.
- Approval or rejection.
- Change ownership.
- Version-controlled implementation.
- Promotion authorization.

#### **5.3.5 Improvement Verification Plane**

- Candidate testing.
- Regression checks.
- Comparison against the approved baseline.
- Controlled rollout.
- Post-change monitoring.
- Rollback decisions.

### **5.4 Learning Signals**

#### **5.4.1 Build and Test Failures**

- Compilation failures.
- Unit test failures.
- Integration test failures.
- Architecture test failures.
- Contract test failures.
- Repeated failure signatures.
- Distinguishing product defects from Harness deficiencies.

#### **5.4.2 Review Findings**

- Repeated architecture violations.
- Missing error handling.
- Inconsistent logging.
- Security review observations.
- Maintainability concerns.
- Repeated recommendations across unrelated runs.

#### **5.4.3 Validation Results**

- Gate outcomes.
- Failure categories.
- Repeated gate failures.
- Flaky tests and environmental failures.
- Incomplete or unavailable validation evidence.
- Separation of a gate's observed results from learning-engine interpretations.

#### **5.4.4 Evaluator Outcomes**

- Goal satisfaction.
- Acceptance criteria coverage.
- Business outcome alignment.
- Partial and inconclusive evaluations.
- Recurring gaps between implementation completion and user expectations.

#### **5.4.5 Retry Patterns**

- Retry counts.
- Repeated failure and correction sequences.
- Time spent in recovery.
- Failed recovery strategies.
- Execution exhaustion.
- Relationship between retry behavior and Harness effectiveness.

#### **5.4.6 Human Feedback**

- Developer corrections.
- Reviewer and architect feedback.
- Rejected recommendations.
- Approved improvements.
- False-positive identification.
- Production feedback.
- Human feedback provenance and authority.

#### **5.4.7 Metrics as Learning Signals**

- First-pass validation success rate.
- Retry rate.
- Mean and percentile execution duration.
- Cost per successful task.
- Repeated finding rate.
- Recommendation acceptance rate.
- Regression rate.
- Post-change improvement effectiveness.
- Metric interpretation, baselines, and tradeoffs.

### **5.5 Harness Memory**

- Purpose of Harness Memory in adaptive engineering.
- Execution history versus persistent learning memory.
- Storing normalized observations and recurring patterns.
- Storing candidate and approved recommendations.
- Evidence references and provenance.
- Linking memory records to Harness versions.
- Confidence, freshness, and retention.
- Distinguishing provisional learning from authoritative standards.
- Preventing temporary scratchpad content from becoming authoritative without review.
- Preparing for deeper coverage in Chapter 37.

### **5.6 Feedback Loops**

#### **5.6.1 Execution Feedback Loop**

- Execute.
- Observe.
- Validate.
- Correct.
- Re-evaluate within the current run.

#### **5.6.2 Cross-Run Learning Loop**

- Collect evidence from multiple runs.
- Normalize and classify findings.
- Identify patterns.
- Generate recommendations.
- Route proposals to governance.

#### **5.6.3 Governed Improvement Loop**

- Approve a candidate.
- Apply a controlled change.
- Validate against baseline expectations.
- Deploy the approved revision.
- Measure subsequent results.
- Retain, revise, or roll back the improvement.

### **5.7 Evidence-to-Recommendation Flow**

- Evidence ingestion.
- Source validation.
- Normalization and deduplication.
- Pattern aggregation.
- Root-cause hypothesis.
- Candidate improvement generation.
- Impact and risk assessment.
- Evidence-backed recommendation package.
- Review and approval routing.

### **5.8 Recommendation Quality**

- Minimum evidence requirements.
- Multiple-run corroboration.
- Confidence versus certainty.
- Evidence strength and counterexamples.
- Causal uncertainty and alternative explanations.
- Cost-benefit assessment.
- Risk classification.
- Expected improvement.
- Proposed verification criteria.
- Avoiding changes based on isolated failures.

### **5.9 Human Approval Boundary**

- Proposal creation versus approval.
- Authorized approver roles.
- Approval based on change classification.
- Technical Lead and Architect responsibilities.
- Security approval for security-sensitive changes.
- Explicit rejection and deferral.
- Separation of recommendation generation and approval authority.
- Protection against self-approval.

### **5.10 Controlled Change Lifecycle**

- Proposal.
- Triage.
- Review.
- Approval.
- Implementation.
- Testing.
- Release.
- Monitoring.
- Retention or rollback.

### **5.11 Versioning and Rollback**

- Versioned Instructions.
- Versioned Skills.
- Versioned Prompts.
- Versioned workflow definitions.
- Compatibility between component versions.
- Baseline snapshots.
- Artifact provenance.
- Rollback triggers.
- Restoration of approved versions.
- Reproducibility of earlier runs.

### **5.12 Auditability**

- Original evidence.
- Recommendation identifier.
- Reasoning summary and supporting observations.
- Proposed changes.
- Responsible owner.
- Human approval decision.
- Implemented changeset.
- Validation evidence.
- Deployment version.
- Post-deployment outcomes.
- Rollback and incident history.

### **5.13 Safety Boundaries and Drift Prevention**

- Instruction hierarchy protection.
- Prompt injection resistance.
- Untrusted evidence handling.
- Prevention of policy weakening.
- Architecture constraint enforcement.
- Security and compliance gate immutability during execution.
- Evaluation bias and metric gaming.
- Distribution changes and stale learning signals.
- Detecting deterioration after approved changes.
- Preventing recursive, unreviewed modification of the learning mechanism.
- Emergency suspension of adaptive capabilities.

### **5.14 Adaptive Skills, Prompts, and Instructions**

#### **5.14.1 Adaptive Skills**

- Detect repeated implementation patterns.
- Identify missing Skill steps.
- Recommend improved Skill preconditions.
- Recommend additional validation steps.
- Evaluate candidate Skill revisions.

#### **5.14.2 Adaptive Prompts**

- Identify repeated ambiguity.
- Detect missing task context.
- Recommend more explicit acceptance criteria.
- Improve failure-recovery instructions.
- Measure candidate Prompt effectiveness.

#### **5.14.3 Instruction Change Recommendations**

- Detect repeated conflicts or ambiguities.
- Identify missing organizational guidance.
- Propose changes without directly editing authoritative Instructions.
- Require appropriate ownership and approval.
- Evaluate compatibility with existing standards.

### **5.15 Operational Considerations**

- Learning cadence: per run, batch, or scheduled review.
- Learning infrastructure cost.
- Storage and retention.
- Evidence quality and scale.
- Multi-project and tenant isolation.
- Recommendation backlog management.
- Observability of the learning engine.
- Recovery from incomplete learning cycles.
- Adaptation during concurrent Harness executions.

## **6. Professional Diagrams**

### **Diagram 36.1 — Static Versus Adaptive Harness**

**Purpose:** Contrast a fixed execution lifecycle with a governed evidence-driven improvement lifecycle.

Include:

- Shared execution architecture.
- Static workflow completion.
- Adaptive evidence collection.
- Learning and recommendation components.
- Approval and controlled improvement path.
- Clear distinction between execution and persistent evolution.

### **Diagram 36.2 — Learning Feedback Loop**

**Purpose:** Explain how execution outcomes produce improvement opportunities.

Include:

- Execution.
- Evidence collection.
- Harness Memory.
- Pattern analysis.
- Recommendation generation.
- Human decision.
- Approved change.
- Subsequent execution.
- Improvement measurement.

Represent rejection, insufficient evidence, and rollback paths.

### **Diagram 36.3 — Evidence-to-Recommendation Flow**

**Purpose:** Illustrate how raw operational evidence becomes a reviewable improvement proposal.

Include:

- Builds and tests.
- Reviewer findings.
- Validator results.
- Evaluator outcomes.
- Retry patterns.
- Human feedback.
- Metrics.
- Evidence normalization.
- Pattern detection.
- Recommendation generation.
- Risk and confidence assessment.
- Recommendation record.

### **Diagram 36.4 — Human Approval Boundary**

**Purpose:** Demonstrate the separation between learning authority and change authority.

Include:

- Learning engine.
- Proposed change.
- Risk classification.
- Human approver.
- Approved, rejected, and deferred decisions.
- Controlled implementation.
- Validation gates.
- Versioned release.
- Audit trail.

Clearly show that the learning engine cannot directly modify governed engineering standards.

### **Diagram 36.5 — Adaptive Harness Architecture**

**Purpose:** Present a reference architecture integrating adaptive learning into the existing enterprise Harness.

Include:

- Goal and task intake.
- Lead Agent.
- Developer Agent.
- Reviewer Agent.
- Validator Agent.
- Evaluator Agent.
- Deterministic gate pipeline.
- Execution workspace and scratchpad.
- Evidence collectors.
- Metrics and observability.
- Harness Memory.
- Pattern analysis and Learning Engine.
- Recommendation registry.
- Human governance boundary.
- Version-controlled Harness assets.
- Controlled deployment and rollback.
- Improvement monitoring.

Distinguish execution, learning, and governance responsibilities.

## **7. Hands-on Example — Alpha Car Detailing**

### **7.1 Scenario: Repeated Corporate Fleet Booking Failures**

- Implement a recurring corporate fleet booking enhancement.
- Apply booking limits and eligibility constraints.
- Observe repeated failures involving validation logic and integration tests.
- Demonstrate the existing Static Harness retry behavior.

### **7.2 Establish the Static Baseline**

- Identify the current Harness version.
- Capture approved Instructions, Skills, and Prompts.
- Record deterministic gate definitions.
- Execute representative booking tasks.
- Preserve build, review, validation, and evaluation evidence.

### **7.3 Collect Execution Evidence**

- Gather several run records.
- Extract failure signatures.
- Capture repeated Reviewer observations.
- Record retry counts and corrective actions.
- Capture outcomes and execution costs.
- Associate observations with exact Harness asset versions.

### **7.4 Identify a Recurring Pattern**

- Detect repeated omission of corporate fleet booking-limit checks.
- Compare failures across multiple tasks.
- Distinguish missing implementation logic from deficient Skill guidance.
- Establish the strength of the supporting evidence.

### **7.5 Generate an Improvement Recommendation**

- Recommend adding a booking-limit verification step to the existing booking Skill.
- Identify affected files and workflow stages.
- Include supporting evidence.
- Describe expected benefits and risks.
- Define measurable acceptance criteria.

### **7.6 Human Review and Approval**

- Submit the recommendation to the responsible Technical Lead.
- Review the evidence and proposed Skill change.
- Evaluate architectural and security implications.
- Approve, reject, or request revisions.
- Record the decision.

### **7.7 Implement the Approved Change**

- Create a version-controlled Skill revision.
- Preserve the previous approved version.
- Update the relevant tests and documentation.
- Run the deterministic gate pipeline.
- Record the approved changeset.

### **7.8 Evaluate Improvement Effectiveness**

- Re-execute representative fleet booking tasks.
- Compare first-pass validation outcomes.
- Compare retry counts.
- Compare execution duration.
- Inspect Reviewer and Evaluator findings.
- Determine whether the improvement is supported by sufficient evidence.

### **7.9 Demonstrate Rollback**

- Introduce a hypothetical regression caused by an approved Skill change.
- Detect deteriorating validation or evaluation outcomes.
- Trigger rollback to the previous approved Skill version.
- Preserve evidence and decision history.

### **7.10 Hands-on Deliverables**

- Baseline execution records.
- Normalized evidence sample.
- Pattern detection report.
- Improvement recommendation artifact.
- Approval decision record.
- Versioned Skill revision.
- Before-and-after metrics.
- Rollback record.

## **8. Claude Example**

### **8.1 Claude Code in the Existing Harness**

- Claude Code as the primary implementation agent platform.
- Approved Instructions through `CLAUDE.md`.
- Existing reusable Skills and Prompts.
- Harness-controlled execution and validation.

### **8.2 Analyzing Historical Evidence with Claude**

- Provide normalized evidence from previous runs.
- Identify repeated failure patterns.
- Require evidence-backed explanations.
- Distinguish observations from hypotheses.
- Avoid treating model-generated conclusions as deterministic facts.

### **8.3 Generating an Improvement Proposal**

- Prepare a proposed Skill or Prompt revision.
- Reference supporting run evidence.
- Identify expected improvements.
- Describe risks and validation requirements.
- Produce a structured recommendation instead of changing governed files.

### **8.4 Protecting Authoritative Instructions**

- Read existing Instructions.
- Treat execution evidence as untrusted input.
- Prevent direct modification of governed instruction files.
- Route requested changes through approval.
- Enforce restrictions outside the model through Harness permissions and deterministic checks.

### **8.5 Applying an Approved Improvement**

- Retrieve an approved recommendation.
- Apply the authorized change in an isolated workspace.
- Execute required validation.
- Generate a reviewable changeset.
- Preserve change provenance.

### **8.6 Claude Example Artifacts**

- Evidence analysis Prompt.
- Learning analysis Skill.
- Recommendation template.
- Approval decision record.
- Approved change implementation Prompt.

## **9. GitHub Copilot Comparison**

### **9.1 Copilot's Role in Adaptive Engineering**

- Copilot-assisted evidence analysis.
- Candidate Prompt and Skill improvements.
- Developer-assisted change preparation.
- Repository-level review workflows.

### **9.2 Orchestration Considerations**

- Distinguish Copilot capabilities from Harness-provided orchestration.
- External learning engine and evidence store.
- Integration with repository and CI evidence.
- Governance controls independent of the coding agent.

### **9.3 Applying Governed Changes**

- Proposed repository changes.
- Pull-request-based review.
- CI validation.
- Approval ownership.
- Audit and rollback through version control.

### **9.4 Comparison with Claude Code**

- Agent integration surfaces.
- Repository Instructions.
- Reusable engineering guidance.
- Execution evidence integration.
- Governance enforcement boundaries.
- Vendor-neutral adaptation principles.

## **10. Codex Comparison**

### **10.1 Codex in Adaptive Harness Workflows**

- Codex as an implementation and analysis agent.
- Harness-managed context and evidence.
- Candidate improvement generation.
- Controlled implementation workflows.

### **10.2 Harness-Owned Learning Infrastructure**

- Cross-run evidence storage outside the individual agent session.
- Learning engine responsibilities.
- Recommendation management.
- Human approval workflow.
- Deterministic validation.

### **10.3 Governed Improvement Example**

- Analyze recurring booking implementation failures.
- Propose a Prompt revision.
- Prepare a reviewable changeset after approval.
- Validate the proposed revision.
- Track improvement effectiveness.

### **10.4 Comparison with Claude Code and Copilot**

- Available integration approaches.
- Instruction and context mechanisms.
- Change proposal workflows.
- External governance requirements.
- Architectural portability across coding agent platforms.

## **11. Best Practices**

### **11.1 Design Principles**

1. Start with a reliable Static Harness before introducing learning.
2. Keep learning separate from execution authority.
3. Preserve approved engineering standards.
4. Use evidence from multiple representative runs.
5. Treat learning output as recommendations, not authoritative facts.
6. Maintain deterministic validation independently of model-generated assessments.
7. Require explicit approval for governed changes.
8. Version all persistent Harness assets.
9. Preserve complete evidence provenance.
10. Define measurable improvement criteria before implementing changes.
11. Compare candidate revisions against a stable baseline.
12. Support rollback from the beginning.
13. Protect sensitive execution evidence.
14. Avoid optimizing a single metric at the expense of engineering quality.
15. Periodically review learning effectiveness and recommendation quality.

### **11.2 Evidence Quality Practices**

- Normalize failure classifications.
- Preserve original evidence.
- Capture environmental context.
- Distinguish flaky infrastructure from code defects.
- Correlate findings with Harness versions.
- Use counterexamples to test learning hypotheses.

### **11.3 Governance Practices**

- Document ownership.
- Classify recommendations by risk.
- Require appropriate approval.
- Keep approval decisions auditable.
- Validate the exact proposed changeset.
- Prohibit approval bypass through learning workflows.

### **11.4 Operational Practices**

- Control learning frequency and cost.
- Limit recommendation volume.
- Monitor recurring false positives.
- Detect stale recommendations.
- Establish learning suspension mechanisms.
- Review the effectiveness of previously approved improvements.

## **12. Anti-patterns**

### **12.1 Silent Instruction Evolution**

- Learning engine directly modifies `CLAUDE.md` or equivalent authoritative Instructions.
- Engineering standards change without approval.

### **12.2 Self-Approval**

- The same adaptive component proposes and authorizes its own governed changes.
- No independent authority boundary.

### **12.3 Learning from a Single Failure**

- A transient failure triggers a permanent workflow revision.
- Insufficient evidence and missing corroboration.

### **12.4 Retry Count as the Only Optimization Metric**

- Minimizing retries while reducing test coverage or implementation quality.
- Metric optimization without outcome evaluation.

### **12.5 Weakening Validation Gates to Improve Success Rates**

- Removing failing tests.
- Relaxing security checks.
- Lowering acceptance criteria.
- Reclassifying failures as successful outcomes.

### **12.6 Treating Harness Memory as Authoritative Policy**

- Historical observations override approved Instructions.
- Provisional recommendations become execution constraints without promotion.

### **12.7 Unbounded Prompt Evolution**

- Repeatedly extending Prompts without assessing clarity, maintainability, or context cost.
- Conflicting or redundant guidance.

### **12.8 Learning from Contaminated Evidence**

- Prompt injection embedded in logs, code, tickets, or review comments.
- Untrusted artifacts influencing governance decisions.

### **12.9 No Baseline or Rollback**

- Changes deployed without comparison.
- Inability to reproduce a previous Harness version.

### **12.10 Learning Engine as a Replacement for Human Architecture Judgment**

- Generated recommendations treated as architectural decisions.
- Lack of ownership or accountability.

### **12.11 Cross-Project Learning Leakage**

- Sharing client-sensitive patterns or artifacts across isolated projects.
- Inappropriate promotion of one project's conventions into another.

## **13. Architect's Notes**

### **13.1 Adaptation Is a Governance Problem as Much as a Technical Problem**

- Learning capability does not imply change authority.
- The architecture must make unauthorized changes difficult or impossible.

### **13.2 Separate the Control Plane from the Learning Plane**

- Approved execution policy belongs to the governance/control plane.
- Learning components generate evidence-backed proposals.
- Explicit promotion connects the two.

### **13.3 Deterministic Gates Must Remain Independent**

- Validation results are inputs to learning.
- The learning engine cannot reinterpret objective failures as success.
- Gate changes require their own governed lifecycle.

### **13.4 Learning Requires Stable Identity and Provenance**

- Harness version.
- Prompt version.
- Skill version.
- Instruction version.
- Model and tool configuration.
- Execution environment.
- Evidence identifiers.

### **13.5 Recommendations Must Be Falsifiable**

- State the problem.
- State the proposed change.
- Predict observable improvement.
- Define measurement criteria.
- Establish rollback conditions.

### **13.6 Avoid Premature Self-Learning Architecture**

- Evaluate execution volume.
- Evaluate recurring failure patterns.
- Estimate operational overhead.
- Introduce adaptation incrementally.

### **13.7 Preserve Organizational Knowledge Boundaries**

- Scratchpad versus Harness Memory.
- Harness Memory versus Repository Intelligence.
- Provisional evidence versus authoritative Instructions.
- Context retrieval versus policy modification.

### **13.8 Adaptation Is Not Equivalent to Model Training**

- Learning from execution evidence can occur without updating model weights.
- Adaptation may involve revised Skills, Prompts, selection strategies, or governed workflows.
- Model fine-tuning is a separate engineering and governance decision.

## **14. Enterprise Tips**

### **14.1 Introduce Adaptation in Stages**

- Stage 1: Collect execution evidence.
- Stage 2: Produce recurring failure reports.
- Stage 3: Generate improvement recommendations.
- Stage 4: Establish human review and approval.
- Stage 5: Deploy versioned changes.
- Stage 6: Measure effectiveness and support rollback.

### **14.2 Establish Recommendation Ownership**

- Harness engineering team.
- Application Technical Lead.
- Solution Architect.
- Security and compliance stakeholders.
- Platform engineering.

### **14.3 Treat Learning as an Engineering Product**

- Maintain a recommendation backlog.
- Define service-level objectives for the learning process where appropriate.
- Review recommendation acceptance and rejection patterns.
- Track measurable value from approved improvements.

### **14.4 Use Risk-Based Approval**

- Low-risk operational tuning.
- Medium-risk workflow or Skill changes.
- High-risk Instruction, architecture, or security changes.
- Prohibited changes beyond delegated authority.

### **14.5 Preserve Client and Repository Isolation**

- Dedicated evidence stores.
- Access-controlled memory.
- Tenant-aware recommendation processing.
- Explicitly authorized knowledge sharing.
- Retention and deletion controls.

### **14.6 Define Adaptation Success Metrics**

- Reduction in repeated failures.
- Improvement in first-pass validation success.
- Reduction in avoidable retries.
- Stable or improved Reviewer and Evaluator outcomes.
- Reduced execution cost.
- No increase in regression or security risk.

### **14.7 Establish Emergency Controls**

- Disable learning-driven recommendations.
- Freeze promotion of approved changes during incidents.
- Roll back a problematic revision.
- Investigate suspicious evidence.
- Preserve audit records.

## **15. Decision Points**

### **15.1 Should the Enterprise Introduce an Adaptive Harness?**

Consider:

- Execution volume.
- Recurring failure frequency.
- Existing Harness reliability.
- Evidence quality.
- Governance maturity.
- Expected benefit versus complexity.

### **15.2 What Level of Adaptation Is Appropriate?**

- Static execution only.
- Run-level adaptation.
- Recommendation-based learning.
- Governed persistent evolution.

### **15.3 What Evidence Is Sufficient?**

- Minimum observation counts.
- Cross-run consistency.
- Representative task coverage.
- Confidence and evidence strength.
- Negative evidence and exceptions.

### **15.4 What Assets Are Eligible for Change?**

- Prompts.
- Skills.
- Context retrieval.
- Execution workflows.
- Instructions.
- Validation policies.

Determine authority independently for each asset category.

### **15.5 Who Approves Improvements?**

- Harness owner.
- Technical Lead.
- Architect.
- Security owner.
- Compliance owner.

### **15.6 When Should an Improvement Be Rejected?**

- Insufficient evidence.
- Unclear expected benefit.
- Conflict with approved standards.
- Security or compliance risk.
- Excessive maintenance cost.
- Inability to verify the claimed improvement.

### **15.7 When Should an Approved Improvement Be Rolled Back?**

- Increased regression frequency.
- Degraded validation outcomes.
- Reduced business-goal satisfaction.
- Unexpected cost or latency growth.
- Security concerns.
- Loss of reproducibility.

### **15.8 Centralized or Project-Specific Learning?**

- Shared learning infrastructure.
- Repository-specific recommendations.
- Enterprise-wide standards.
- Client confidentiality.
- Ownership of promoted knowledge.

## **16. Exercises**

### **Exercise 36.1 — Identify Static and Adaptive Behaviors**

**Objective:** Distinguish fixed orchestration from learning-based improvement.

Tasks:

- Review a sample Alpha Car Detailing Harness workflow.
- Classify its execution and recovery behaviors.
- Identify which behaviors constitute genuine adaptation.
- Explain whether persistent standards are modified.

**Deliverable:** Behavior classification matrix.

### **Exercise 36.2 — Analyze Repeated Validation Failures**

**Objective:** Identify evidence-backed opportunities for improvement.

Tasks:

- Examine several corporate fleet booking runs.
- Group validation failures.
- Identify recurrence patterns.
- Separate application defects from Harness shortcomings.

**Deliverable:** Recurring failure analysis.

### **Exercise 36.3 — Design a Recommendation Artifact**

**Objective:** Create a structured improvement proposal.

Tasks:

- Select a repeated failure.
- Identify the affected Skill or Prompt.
- Propose an improvement.
- Include evidence, risk, expected benefit, and verification criteria.

**Deliverable:** Harness improvement recommendation.

### **Exercise 36.4 — Design an Approval Boundary**

**Objective:** Prevent unauthorized persistent changes.

Tasks:

- Define recommendation and approval responsibilities.
- Identify protected Instructions and policies.
- Design approval states.
- Specify enforcement mechanisms.

**Deliverable:** Approval workflow and responsibility matrix.

### **Exercise 36.5 — Design Harness Memory Records**

**Objective:** Support reliable learning across executions.

Tasks:

- Define evidence record fields.
- Include source and version references.
- Model observations and recommendations separately.
- Define retention and promotion policies.

**Deliverable:** Conceptual Harness Memory schema.

### **Exercise 36.6 — Implement an Adaptive Skill Recommendation**

**Objective:** Demonstrate controlled Skill improvement.

Tasks:

- Use repeated fleet booking failures.
- Generate a proposed Skill change.
- Record human approval.
- Apply the approved revision.
- Run deterministic validation.

**Deliverable:** Version-controlled Skill improvement and validation report.

### **Exercise 36.7 — Compare Improvement Effectiveness**

**Objective:** Evaluate whether an approved change delivers value.

Tasks:

- Establish baseline metrics.
- Execute comparable tasks before and after the change.
- Compare outcomes.
- Identify confounding factors.
- Recommend retaining or rolling back the revision.

**Deliverable:** Improvement effectiveness report.

### **Exercise 36.8 — Threat-Model an Adaptive Harness**

**Objective:** Identify security and governance risks.

Tasks:

- Analyze an untrusted execution log containing malicious instructions.
- Identify possible learning-engine compromise paths.
- Design evidence sanitization and authorization boundaries.
- Verify that protected Instructions cannot be silently modified.

**Deliverable:** Adaptive Harness threat model.

### **Exercise 36.9 — Design a Complete Adaptive Harness**

**Objective:** Integrate the chapter's architectural concepts.

Tasks:

- Extend the Alpha Car Detailing Harness architecture.
- Add evidence collection and Harness Memory.
- Add pattern detection and recommendation generation.
- Add governance and approval.
- Add validation, deployment, monitoring, and rollback.
- Describe failure-recovery behavior.

**Deliverable:** Adaptive Harness reference architecture and design rationale.

## **17. Interview Questions**

### **17.1 Foundational Questions**

1. What is a Static Harness?
2. What is an Adaptive Harness?
3. What is the primary difference between static execution and adaptive learning?
4. Why does retrying a failed task not necessarily constitute learning?
5. What benefits can adaptation provide to enterprise AI Engineering?
6. How is an Adaptive Harness different from a self-modifying AI system?

### **17.2 Architecture Questions**

1. What additional architectural components does an Adaptive Harness require?
2. Why should execution and learning responsibilities be separated?
3. What is the role of Harness Memory?
4. How should the Learning Engine interact with the Validator Agent?
5. How do Reviewer findings differ from Validator results as learning signals?
6. What role does the Evaluator Agent play in adaptive improvement?
7. How can an Adaptive Harness identify recurring failures across runs?
8. How should an enterprise structure an evidence-to-recommendation pipeline?
9. Why should the learning engine not directly modify validation gates?

### **17.3 Governance Questions**

 1. Why is human approval important for persistent Harness changes?
 2. Which Harness components should remain governed?
 3. How should approval responsibilities be assigned?
 4. How do you prevent unauthorized Instruction evolution?
 5. How can an enterprise audit changes recommended by an Adaptive Harness?
 6. Why are versioning and rollback essential?
 7. How should conflicting recommendations be resolved?

### **17.4 Learning and Evaluation Questions**

 1. Which signals are most valuable for detecting Harness improvement opportunities?
 2. How can a system distinguish a recurring Harness deficiency from an application defect?
 3. Why is learning from a single failed execution risky?
 4. What evidence should accompany an improvement recommendation?
 5. How can a team measure whether an approved Prompt or Skill revision is effective?
 6. How do you prevent metric gaming in an Adaptive Harness?
 7. What is the difference between behavioral adaptation and model fine-tuning?

### **17.5 Advanced Enterprise Questions**

 1. How would you design adaptive learning for multiple isolated enterprise repositories?
 2. How can prompt injection affect an evidence-driven learning pipeline?
 3. How do you prevent long-term drift in adaptive workflows?
 4. What controls are required before deploying adaptive improvements in regulated environments?
 5. How would you design a rollback strategy for an approved change that causes regressions?
 6. When would a Static Harness be a better architectural choice than an Adaptive Harness?
 7. How would you evolve an existing production Static Harness toward governed adaptive learning without disrupting delivery?

## **18. Chapter Summary**

### **18.1 Key Concepts to Reinforce**

- Static Harnesses execute predefined workflows.
- Adaptive Harnesses use execution evidence to identify improvement opportunities.
- Runtime recovery and cross-run learning are different capabilities.
- Learning signals originate from builds, tests, reviews, validation, evaluation, retries, human feedback, and metrics.
- Harness Memory preserves reusable evidence and learning history.
- Learning engines generate recommendations rather than authoritative decisions.
- Persistent changes must pass through governed approval processes.
- Deterministic gates remain independent of the Learning Engine.
- Versioning, auditability, and rollback are essential to safe evolution.
- Improvement must be measured against a stable baseline.
- An Adaptive Harness should become more effective without reducing engineering rigor.

### **18.2 Architectural Takeaway**

An enterprise Adaptive Harness combines three responsibilities:

1. **Execution:** Perform engineering tasks using approved workflows and standards.
2. **Learning:** Analyze evidence and recommend improvements.
3. **Governance:** Authorize, validate, release, and monitor controlled changes.

These responsibilities must remain logically separated and technically enforceable.

### **18.3 Alpha Car Detailing Outcome**

- The Static Harness repeatedly encounters fleet booking validation problems.
- The Adaptive Harness identifies a recurring pattern.
- The Learning Engine proposes an improvement.
- An authorized human reviews and approves the proposal.
- The revised Skill is validated and versioned.
- Subsequent executions provide evidence about improvement effectiveness.
- Rollback remains available if the change degrades outcomes.

### **18.4 Transition to Chapter 37 — Harness Memory**

- Why evidence must survive beyond individual execution runs.
- How historical observations support learning.
- How recommendations and outcomes should be represented.
- Why Harness Memory must distinguish provisional knowledge from governed engineering standards.
- How Harness Memory becomes foundational infrastructure for self-learning systems.

## **19. Further Reading**

### **19.1 Foundational Engineering Concepts**

- Feedback control systems and closed-loop architectures.
- Continuous improvement and the Plan–Do–Check–Act cycle.
- Software configuration management.
- Change control and engineering governance.
- Observability and evidence-driven operations.

### **19.2 AI Engineering and Adaptive Systems**

- Agent orchestration architecture.
- AI system evaluation.
- Human-in-the-loop decision systems.
- AI feedback mechanisms.
- Learning from operational telemetry.
- AI system monitoring and drift detection.

### **19.3 Security and Governance References**

- NIST AI Risk Management Framework.
- NIST Secure Software Development Framework.
- OWASP guidance on LLM and agentic application security.
- Supply-chain provenance and software integrity.
- Enterprise auditability and access-control practices.

### **19.4 Platform Documentation**

- Anthropic Claude Code documentation.
- GitHub Copilot documentation.
- OpenAI Codex documentation.
- GitHub Actions documentation.
- Azure DevOps Pipelines documentation.
- OpenTelemetry documentation.

### **19.5 Internal Handbook Cross-References**

- Chapter 7 — Skills.
- Chapter 8 — Prompts.
- Chapter 10 — Steering Notes.
- Chapter 11 — Knowledge Sources.
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 14 — Lead Agent.
- Chapter 15 — Developer Agent.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Relevant chapters on metrics, governance, human approval, audit trails, policies, compliance, and productivity measurement.
- Chapter 37 — Harness Memory.
- Chapter 38 — Learning from Builds.
- Chapter 39 — Learning from Reviews.
- Chapter 41 — Skill Evolution.
- Chapter 42 — Prompt Evolution.
- Chapter 43 — Instruction Evolution.
- Chapter 45 — Autonomous Improvement Engine.

**Chapter 36 outline is complete.**
