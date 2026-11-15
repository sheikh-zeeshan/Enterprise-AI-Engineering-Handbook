# **Chapter 39 — Learning from Reviews**

Part VI — Self-Learning AI Systems

**Target file:** `book/39-learning-from-reviews.md`

**Repository:** [Enterprise AI Engineering Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)

## **Chapter Objective**

Explain how an Adaptive Harness systematically learns from Reviewer Agent findings, human code reviews, architecture reviews, security reviews, and recurring review patterns to improve the quality of future AI-assisted engineering runs.

The chapter establishes a governed approach for collecting review evidence, classifying findings, identifying root causes, discovering recurring problems, generating improvement recommendations, and promoting validated learning into Harness Memory.

It distinguishes learning from review feedback from automatically modifying engineering standards, ensuring that adaptive behavior remains traceable, evidence-based, reversible, and subject to human oversight.

**Core principle:** Review findings are learning signals, not automatic instructions. The Harness may detect patterns, retain verified evidence, and recommend improvements, but it must never silently modify governed Instructions, Skills, policies, or architecture standards.

## **1. Story-Driven Opening — When the Same Review Finding Appears Again**

### **1.1 The Alpha Car Detailing Scenario**

- A Developer Agent implements a new corporate fleet booking capability.
- The Reviewer Agent identifies a Clean Architecture dependency violation.
- The implementation is corrected and approved.
- During a later development run, another similar violation appears.
- A third implementation repeats the same architectural mistake.
- Each individual review succeeds in identifying the problem, but the overall engineering process continues producing it.

### **1.2 The Problem with Isolated Reviews**

- Reviews correct individual implementations without necessarily improving future engineering behavior.
- Valuable findings become fragmented across review artifacts, pull requests, logs, and discussions.
- Repeated issues consume review capacity and increase engineering costs.
- The difference between correcting a defect and preventing its recurrence.

### **1.3 Introducing Review-Driven Learning**

- The Adaptive Harness recognizes recurring review findings.
- It correlates findings with implementation decisions, repository context, and workflow execution.
- It recommends an improvement to the relevant Skill, Prompt, Instruction, or workflow.
- A designated owner reviews the recommendation before any governed change is introduced.

### **1.4 The Architectural Question**

- How can an AI engineering system learn from reviews without allowing reviews to become an uncontrolled source of authority?
- How can the Harness distinguish a genuine systemic weakness from an isolated reviewer opinion?
- How can learning improve future runs without compromising governance?

## **2. Learning Objectives**

By the end of this chapter, readers should be able to:

1. Explain why review evidence is an important learning source for Adaptive Harnesses.
2. Distinguish Reviewer Agent findings, human feedback, architecture findings, and security findings.
3. Design structured review evidence capture and classification mechanisms.
4. Identify recurring findings and correlate them with implementation choices.
5. Apply root-cause classification to recurring engineering problems.
6. Separate rejected implementations from rejected recommendations.
7. Design a review-driven learning pipeline connected to Harness Memory.
8. Generate evidence-supported improvement recommendations for Skills, Prompts, Instructions, roles, and workflows.
9. Define confidence thresholds, evidence requirements, and approval policies.
10. Implement versioning, rollback, traceability, and auditability for learning-driven improvements.
11. Recognize false learning, feedback bias, and conflicting review evidence.
12. Integrate review-driven learning into a production-grade Adaptive Harness.

## **3. Background — From Code Review to Organizational Learning**

### **3.1 The Traditional Purpose of Code Review**

- Detecting defects and maintainability problems.
- Verifying architecture alignment.
- Enforcing coding and security standards.
- Improving implementation quality before integration.
- Sharing engineering knowledge between team members.

### **3.2 Limitations of Traditional Review Processes**

- Review outcomes are frequently stored as unstructured comments.
- Historical findings are difficult to retrieve and compare.
- Recurrent defects may remain unnoticed across repositories or teams.
- Review resolution does not automatically produce organizational knowledge.
- Review activity metrics do not necessarily measure engineering learning.

### **3.3 Review Evidence in AI-Assisted Development**

- AI-generated code can reproduce recurring implementation patterns.
- Multiple Agents may independently produce similar defects.
- Reviewer Agent findings provide structured signals about implementation weaknesses.
- Human reviewers provide contextual knowledge and architectural judgment.
- Automated review must not be treated as authoritative simply because it is repeatable.

### **3.4 Static Versus Adaptive Review Processes**

- Static Harnesses execute review workflows without learning from previous findings.
- Adaptive Harnesses analyze historical evidence to identify improvement opportunities.
- The distinction between historical retrieval, adaptive recommendations, and governed evolution.
- Why adaptation does not imply unrestricted autonomy.

### **3.5 Relationship to Earlier Chapters**

- Chapter 16 — Reviewer Agent: review responsibilities and findings.
- Chapter 17 — Validator Agent: deterministic validation and objective engineering checks.
- Chapter 18 — Evaluator Agent: outcome quality and acceptance criteria.
- Chapter 31 — Human Approval: governance and authorization.
- Chapter 32 — Audit Trails: traceability of decisions and changes.
- Chapter 36 — Static vs Adaptive Harnesses: architectural foundations of adaptation.
- Chapter 37 — Harness Memory: governed learning storage.
- Chapter 38 — Learning from Builds: deterministic feedback and build failure patterns.
- Relationship to later chapters covering pull requests and governed evolution.

## **4. Concepts — Understanding Review Evidence as a Learning Signal**

### **4.1 What Is Review-Driven Learning?**

- Definition and scope.
- Learning from observations rather than treating suggestions as commands.
- Difference between review resolution and learning promotion.
- Relationship between observed evidence and engineering improvement recommendations.

### **4.2 Sources of Review Evidence**

#### **4.2.1 Reviewer Agent Findings**

- Correctness concerns.
- Architecture violations.
- Missing or weak tests.
- Maintainability problems.
- Code complexity.
- Error-handling weaknesses.
- Logging and observability gaps.
- Security concerns requiring verification.

#### **4.2.2 Human Code Review Feedback**

- Inline code comments.
- Pull request discussions.
- Requested changes.
- Approval decisions.
- Rejected approaches.
- Contextual engineering explanations.
- Expert observations not captured by automated checks.

#### **4.2.3 Architecture Review Findings**

- Clean Architecture boundary violations.
- Incorrect dependency direction.
- Transaction boundary problems.
- Domain responsibility leakage.
- Inconsistent API and event contract design.
- Incorrect service coupling.
- Architectural decision record conflicts.

#### **4.2.4 Security Review Findings**

- Authentication and authorization weaknesses.
- Missing tenant or corporate fleet isolation.
- Unsafe input handling.
- Sensitive-data exposure.
- Missing security tests.
- Insecure dependency usage.
- Unsafe logging practices.
- Findings requiring security-owner escalation.

#### **4.2.5 Other Relevant Review Sources**

- Technical lead reviews.
- Performance and scalability reviews.
- Operational readiness reviews.
- Compliance and policy assessments.
- Post-implementation maintainability reviews.

### **4.3 Findings, Observations, Decisions, and Learning**

- Observation: an issue identified during review.
- Finding: a structured and evidence-backed assessment.
- Disposition: the outcome of reviewing or resolving the finding.
- Root cause: the likely underlying reason the problem occurred.
- Pattern: a recurring and sufficiently comparable finding.
- Learning candidate: a proposed reusable improvement derived from evidence.
- Approved learning: an improvement accepted for controlled future use.

### **4.4 Finding Severity and Priority**

- Critical, High, Medium, Low, and Informational categories.
- Severity versus recurrence.
- Severity versus remediation priority.
- Confidence versus impact.
- Security-critical findings requiring action even without recurrence.
- Distinguishing issue severity from the risk of changing the Harness.

### **4.5 Review Finding Lifecycle**

- Detected.
- Captured.
- Verified or disputed.
- Classified.
- Resolved, accepted as risk, or rejected.
- Correlated with historical findings.
- Evaluated as a learning candidate.
- Approved, deferred, or rejected.
- Monitored for future effectiveness.

### **4.6 Repeated Review Patterns**

- Exact recurrence.
- Semantic recurrence.
- Cross-file and cross-module recurrence.
- Recurrence across different development tasks.
- Recurrence across different Agents or models.
- Distinguishing persistent patterns from independent one-time defects.

### **4.7 Root-Cause Classification**

- Incomplete task Prompt.
- Missing or ambiguous Instruction.
- Incorrect or outdated Skill workflow.
- Insufficient Repository Intelligence.
- Missing architectural context.
- Inadequate planning.
- Developer Agent implementation mistake.
- Insufficient Reviewer Agent coverage.
- Missing deterministic validation gate.
- Workflow or role coordination failure.
- Conflicting engineering standards.
- Environmental or dependency-related causes.
- Root-cause uncertainty and competing hypotheses.

### **4.8 Correlating Findings with Implementation Choices**

- Mapping findings to implementation runs.
- Identifying relevant planning decisions.
- Tracing the selected Skill and its version.
- Capturing effective Prompt and Instruction versions.
- Correlating repository files and architectural components.
- Correlating deterministic gate results.
- Distinguishing correlation from proven causation.

### **4.9 Learning from Accepted and Rejected Changes**

- Accepted code changes and their review history.
- Rejected code changes and the reasons for rejection.
- Findings dismissed as incorrect or not applicable.
- Recommendations rejected by engineering owners.
- Accepted risk and documented exceptions.
- Why rejection is useful evidence without necessarily proving an engineering pattern.
- Avoiding the assumption that approval guarantees architectural correctness.

### **4.10 Harness Memory and Review Learning**

- Review evidence as a source for Harness Memory.
- Historical evidence versus authoritative engineering Instructions.
- Candidate patterns versus approved knowledge.
- Provenance, ownership, and expiration.
- Retrieval of relevant prior findings during future runs.
- Preventing transient review comments from becoming permanent standards.

### **4.11 Avoiding False Learning**

- Incorrect Reviewer Agent findings.
- Reviewer disagreement.
- Contradictory human feedback.
- One-off contextual exceptions.
- Duplicate findings incorrectly counted as independent evidence.
- Model-generated explanations mistaken for validated root causes.
- Learning from outdated review standards.
- Overfitting to one repository or implementation.
- Feedback loops in which the Harness reinforces its own incorrect recommendations.

## **5. Architecture Discussion — The Review Learning Pipeline**

### **5.1 Architectural Responsibilities**

- Review Evidence Collector.
- Evidence Normalizer.
- Finding Classifier.
- Review Pattern Detector.
- Root-Cause Analyzer.
- Learning Candidate Generator.
- Confidence and Evidence Assessor.
- Harness Memory Manager.
- Recommendation and Approval Manager.
- Version and Change Manager.
- Learning Effectiveness Monitor.
- Audit Trail Manager.

### **5.2 Review Evidence Collection Architecture**

- Consuming Reviewer Agent output.
- Integrating human review comments.
- Collecting architecture review artifacts.
- Linking security review findings.
- Capturing code revisions and review dispositions.
- Maintaining stable references to repository commits, pull requests, and Harness runs.
- Designing idempotent evidence ingestion.

### **5.3 Canonical Review Finding Schema**

Proposed fields for the companion implementation:

- `finding_id`
- `run_id`
- `review_id`
- `source_type`
- `reviewer_id`
- `repository_id`
- `commit_sha`
- `file_path`
- `component`
- `finding_category`
- `severity`
- `description`
- `evidence_references`
- `affected_architecture_rule`
- `prompt_version`
- `skill_version`
- `instruction_version`
- `disposition`
- `root_cause_hypothesis`
- `verification_status`
- `created_at`
- `resolved_at`

Explain mandatory versus optional fields and the need to avoid collecting unnecessary sensitive content.

### **5.4 Finding Classification Pipeline**

- Normalization of heterogeneous review formats.
- Controlled classification taxonomy.
- Severity assignment.
- Evidence verification.
- Duplicate detection.
- Related finding identification.
- Classification disagreement handling.
- Escalation of uncertain findings.

### **5.5 Pattern Detection Architecture**

- Exact finding fingerprinting.
- Semantic similarity and clustering.
- Context-sensitive grouping.
- Repeated finding frequency.
- Independent evidence counts.
- Recency and trend analysis.
- Repository-specific versus cross-repository patterns.
- Preventing duplicate comments from inflating pattern confidence.

### **5.6 Root-Cause Analysis Architecture**

- Mapping findings to task requirements.
- Mapping implementation decisions to selected Skills and Instructions.
- Comparing intended architecture with actual code.
- Using validation results as supporting evidence.
- Generating competing root-cause hypotheses.
- Requiring verification before systemic recommendations.
- Handling unknown or multiple root causes.

### **5.7 Learning Candidate Generation**

- Recommending Prompt refinements.
- Recommending Skill improvements.
- Recommending Instruction clarification.
- Recommending Reviewer Agent checklist changes.
- Recommending role coordination improvements.
- Recommending deterministic gates where suitable.
- Recognizing when repository code, rather than the Harness, needs correction.
- Separating candidates from executable changes.

### **5.8 Confidence and Evidence Thresholds**

- Confidence scoring inputs.
- Repeated independent findings.
- Human verification.
- Root-cause confidence.
- Severity and impact.
- Recency and applicability.
- Negative and contradictory evidence.
- Thresholds for observation, recommendation, and escalation.
- Why confidence alone cannot authorize changes.

### **5.9 Human Approval Architecture**

- Routing recommendations to appropriate owners.
- Skill owner approval.
- Instruction and architecture owner approval.
- Security owner review.
- Separation of recommendation generation and approval authority.
- Mandatory review of governed changes.
- Recording approval conditions and decisions.

### **5.10 Versioning and Rollback**

- Version-controlled Prompt, Skill, and Instruction changes.
- Linking approved recommendations to pull requests.
- Capturing baseline behavior.
- Testing candidate changes before adoption.
- Gradual rollout where appropriate.
- Detecting harmful regressions.
- Restoring prior approved versions.

### **5.11 Auditability and Traceability**

- Original review evidence.
- Pattern detection results.
- Root-cause assessments.
- Generated recommendations.
- Human decisions.
- Approved artifact changes.
- Validation and evaluation results.
- Post-change monitoring.
- Retention and access controls.

### **5.12 Failure Handling and Recovery**

- Missing review evidence.
- Malformed findings.
- Duplicate events.
- Partial ingestion failures.
- Stale approval decisions.
- Unavailable Memory services.
- Conflicting recommendations.
- Interrupted learning workflows.
- Safe retries and recovery without unauthorized mutation.

## **6. Professional Diagrams**

Five diagrams will illustrate the chapter's principal architectural flows.

### **Diagram 39.1 — Review Feedback Loop**

Planned diagram content

\#chatgpt-mermaid-\_r_13ku\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_13ku\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13ku\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13ku\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_13ku\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_13ku\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_13ku\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_13ku\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_13ku\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_13ku\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13ku\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13ku\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_13ku\_ p{margin:0;}#chatgpt-mermaid-\_r_13ku\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_13ku\_ .label text,#chatgpt-mermaid-\_r_13ku\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .node rect,#chatgpt-mermaid-\_r_13ku\_ .node circle,#chatgpt-mermaid-\_r_13ku\_ .node ellipse,#chatgpt-mermaid-\_r_13ku\_ .node polygon,#chatgpt-mermaid-\_r_13ku\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .rough-node .label text,#chatgpt-mermaid-\_r_13ku\_ .node .label text,#chatgpt-mermaid-\_r_13ku\_ .image-shape .label,#chatgpt-mermaid-\_r_13ku\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_13ku\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .rough-node .label,#chatgpt-mermaid-\_r_13ku\_ .node .label,#chatgpt-mermaid-\_r_13ku\_ .image-shape .label,#chatgpt-mermaid-\_r_13ku\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_13ku\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_13ku\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13ku\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13ku\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_13ku\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13ku\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13ku\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13ku\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_13ku\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_13ku\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13ku\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_13ku\_ .icon-shape,#chatgpt-mermaid-\_r_13ku\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13ku\_ .icon-shape p,#chatgpt-mermaid-\_r_13ku\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_13ku\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_13ku\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13ku\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_13ku\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_13ku\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_13ku\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_13ku\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_13ku\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_13ku\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13ku\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_13ku\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13ku\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13ku\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13ku\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_13ku\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_13ku\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .node rect,#chatgpt-mermaid-\_r_13ku\_ .node circle,#chatgpt-mermaid-\_r_13ku\_ .node ellipse,#chatgpt-mermaid-\_r_13ku\_ .node polygon,#chatgpt-mermaid-\_r_13ku\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_13ku\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_13ku\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_13ku\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_13ku\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13ku\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Engineering RunImplementationReviewer AgentReview FindingsEvidence CollectionPattern AnalysisLearning RecommendationHuman ApprovalGoverned ImprovementRecord DecisionValidate and VersionFuture Engineering RunHistorical EvidenceApprovedRejected

Emphasize that evidence collection and recommendation generation do not imply automatic permission to change governed artifacts.

### **Diagram 39.2 — Finding Classification**

\#chatgpt-mermaid-\_r_13l5\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_13l5\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13l5\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13l5\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_13l5\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_13l5\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_13l5\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_13l5\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_13l5\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_13l5\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13l5\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13l5\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_13l5\_ p{margin:0;}#chatgpt-mermaid-\_r_13l5\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_13l5\_ .label text,#chatgpt-mermaid-\_r_13l5\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .node rect,#chatgpt-mermaid-\_r_13l5\_ .node circle,#chatgpt-mermaid-\_r_13l5\_ .node ellipse,#chatgpt-mermaid-\_r_13l5\_ .node polygon,#chatgpt-mermaid-\_r_13l5\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .rough-node .label text,#chatgpt-mermaid-\_r_13l5\_ .node .label text,#chatgpt-mermaid-\_r_13l5\_ .image-shape .label,#chatgpt-mermaid-\_r_13l5\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_13l5\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .rough-node .label,#chatgpt-mermaid-\_r_13l5\_ .node .label,#chatgpt-mermaid-\_r_13l5\_ .image-shape .label,#chatgpt-mermaid-\_r_13l5\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_13l5\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_13l5\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13l5\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13l5\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_13l5\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13l5\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13l5\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13l5\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_13l5\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_13l5\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13l5\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_13l5\_ .icon-shape,#chatgpt-mermaid-\_r_13l5\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13l5\_ .icon-shape p,#chatgpt-mermaid-\_r_13l5\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_13l5\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_13l5\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13l5\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_13l5\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_13l5\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_13l5\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_13l5\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_13l5\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_13l5\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13l5\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_13l5\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13l5\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13l5\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13l5\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_13l5\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_13l5\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .node rect,#chatgpt-mermaid-\_r_13l5\_ .node circle,#chatgpt-mermaid-\_r_13l5\_ .node ellipse,#chatgpt-mermaid-\_r_13l5\_ .node polygon,#chatgpt-mermaid-\_r_13l5\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_13l5\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_13l5\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_13l5\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_13l5\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13l5\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Review FindingNormalize EvidenceEvidence Verified?Dispute or InvestigateAssign CategoryAssess SeverityIdentify Related FindingsAssess Root CauseClassified FindingNoYes

Illustrate classification, verification, severity, and root-cause assessment as separate responsibilities.

### **Diagram 39.3 — Review-to-Memory Promotion**

\#chatgpt-mermaid-\_r_13lc\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_13lc\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lc\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lc\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_13lc\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_13lc\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_13lc\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_13lc\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_13lc\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_13lc\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lc\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lc\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_13lc\_ p{margin:0;}#chatgpt-mermaid-\_r_13lc\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_13lc\_ .label text,#chatgpt-mermaid-\_r_13lc\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .node rect,#chatgpt-mermaid-\_r_13lc\_ .node circle,#chatgpt-mermaid-\_r_13lc\_ .node ellipse,#chatgpt-mermaid-\_r_13lc\_ .node polygon,#chatgpt-mermaid-\_r_13lc\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .rough-node .label text,#chatgpt-mermaid-\_r_13lc\_ .node .label text,#chatgpt-mermaid-\_r_13lc\_ .image-shape .label,#chatgpt-mermaid-\_r_13lc\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_13lc\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .rough-node .label,#chatgpt-mermaid-\_r_13lc\_ .node .label,#chatgpt-mermaid-\_r_13lc\_ .image-shape .label,#chatgpt-mermaid-\_r_13lc\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_13lc\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_13lc\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lc\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lc\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_13lc\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lc\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lc\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lc\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_13lc\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_13lc\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lc\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_13lc\_ .icon-shape,#chatgpt-mermaid-\_r_13lc\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lc\_ .icon-shape p,#chatgpt-mermaid-\_r_13lc\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_13lc\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_13lc\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lc\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_13lc\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_13lc\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_13lc\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_13lc\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_13lc\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_13lc\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lc\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_13lc\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lc\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lc\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lc\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_13lc\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_13lc\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .node rect,#chatgpt-mermaid-\_r_13lc\_ .node circle,#chatgpt-mermaid-\_r_13lc\_ .node ellipse,#chatgpt-mermaid-\_r_13lc\_ .node polygon,#chatgpt-mermaid-\_r_13lc\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_13lc\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_13lc\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_13lc\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_13lc\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lc\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Raw Review EvidenceVerified FindingHistorical Evidence StorePattern DetectionEvidence Threshold Met?Retain ObservationLearning CandidateGovernance DecisionApproved Harness MemoryRetain Decision HistoryNoYesApprovedDeferred or Rejected

Clarify that evidence may be retained without becoming approved guidance, and that governance applies to any promotion that changes future engineering behavior.

### **Diagram 39.4 — Recommendation and Approval Flow**

\#chatgpt-mermaid-\_r_13lj\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_13lj\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lj\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lj\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_13lj\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_13lj\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_13lj\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_13lj\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_13lj\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_13lj\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lj\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lj\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_13lj\_ p{margin:0;}#chatgpt-mermaid-\_r_13lj\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_13lj\_ .label text,#chatgpt-mermaid-\_r_13lj\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .node rect,#chatgpt-mermaid-\_r_13lj\_ .node circle,#chatgpt-mermaid-\_r_13lj\_ .node ellipse,#chatgpt-mermaid-\_r_13lj\_ .node polygon,#chatgpt-mermaid-\_r_13lj\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .rough-node .label text,#chatgpt-mermaid-\_r_13lj\_ .node .label text,#chatgpt-mermaid-\_r_13lj\_ .image-shape .label,#chatgpt-mermaid-\_r_13lj\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_13lj\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .rough-node .label,#chatgpt-mermaid-\_r_13lj\_ .node .label,#chatgpt-mermaid-\_r_13lj\_ .image-shape .label,#chatgpt-mermaid-\_r_13lj\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_13lj\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_13lj\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lj\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lj\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_13lj\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lj\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lj\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lj\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_13lj\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_13lj\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lj\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_13lj\_ .icon-shape,#chatgpt-mermaid-\_r_13lj\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lj\_ .icon-shape p,#chatgpt-mermaid-\_r_13lj\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_13lj\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_13lj\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lj\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_13lj\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_13lj\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_13lj\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_13lj\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_13lj\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_13lj\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lj\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_13lj\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lj\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lj\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lj\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_13lj\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_13lj\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .node rect,#chatgpt-mermaid-\_r_13lj\_ .node circle,#chatgpt-mermaid-\_r_13lj\_ .node ellipse,#chatgpt-mermaid-\_r_13lj\_ .node polygon,#chatgpt-mermaid-\_r_13lj\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_13lj\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_13lj\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_13lj\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_13lj\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lj\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Confirmed Review PatternImprovement ProposalEvidence and Risk AssessmentResponsible OwnerApprove Proposal?Reject or Request RevisionVersioned Change ProposalValidation and EvaluationAcceptance Gates Pass?Revise or Roll BackAuthorized AdoptionEffectiveness MonitoringNoYesNoYes

Show the distinction between approval of a proposal and authorization to deploy a tested version.

### **Diagram 39.5 — Learning from Reviews Inside the Harness**

\#chatgpt-mermaid-\_r_13lq\_{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;fill:rgb(237, 237, 237);}@keyframes edge-animation-frame{from{stroke-dashoffset:0;}}@keyframes dash{to{stroke-dashoffset:0;}}#chatgpt-mermaid-\_r_13lq\_ .edge-animation-slow{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 50s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lq\_ .edge-animation-fast{stroke-dasharray:9,5!important;stroke-dashoffset:900;animation:dash 20s linear infinite;stroke-linecap:round;}#chatgpt-mermaid-\_r_13lq\_ .error-icon{fill:rgb(48, 48, 48);}#chatgpt-mermaid-\_r_13lq\_ .error-text{fill:rgb(237, 237, 237);stroke:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .edge-thickness-normal{stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .edge-thickness-thick{stroke-width:3.5px;}#chatgpt-mermaid-\_r_13lq\_ .edge-pattern-solid{stroke-dasharray:0;}#chatgpt-mermaid-\_r_13lq\_ .edge-thickness-invisible{stroke-width:0;fill:none;}#chatgpt-mermaid-\_r_13lq\_ .edge-pattern-dashed{stroke-dasharray:3;}#chatgpt-mermaid-\_r_13lq\_ .edge-pattern-dotted{stroke-dasharray:2;}#chatgpt-mermaid-\_r_13lq\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lq\_ .marker.cross{stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lq\_ svg{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:16px;}#chatgpt-mermaid-\_r_13lq\_ p{margin:0;}#chatgpt-mermaid-\_r_13lq\_ .label{font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .cluster-label text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .cluster-label span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .cluster-label span p{background-color:transparent;}#chatgpt-mermaid-\_r_13lq\_ .label text,#chatgpt-mermaid-\_r_13lq\_ span{fill:rgb(237, 237, 237);color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .node rect,#chatgpt-mermaid-\_r_13lq\_ .node circle,#chatgpt-mermaid-\_r_13lq\_ .node ellipse,#chatgpt-mermaid-\_r_13lq\_ .node polygon,#chatgpt-mermaid-\_r_13lq\_ .node path{fill:rgb(9, 23, 44);stroke:rgb(31, 78, 148);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .rough-node .label text,#chatgpt-mermaid-\_r_13lq\_ .node .label text,#chatgpt-mermaid-\_r_13lq\_ .image-shape .label,#chatgpt-mermaid-\_r_13lq\_ .icon-shape .label{text-anchor:middle;}#chatgpt-mermaid-\_r_13lq\_ .node .katex path{fill:#000;stroke:#000;stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .rough-node .label,#chatgpt-mermaid-\_r_13lq\_ .node .label,#chatgpt-mermaid-\_r_13lq\_ .image-shape .label,#chatgpt-mermaid-\_r_13lq\_ .icon-shape .label{text-align:center;}#chatgpt-mermaid-\_r_13lq\_ .node.clickable{cursor:pointer;}#chatgpt-mermaid-\_r_13lq\_ .root .anchor path{fill:rgb(175, 175, 175)!important;stroke-width:0;stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lq\_ .arrowheadPath{fill:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lq\_ .edgePath .path{stroke:rgb(175, 175, 175);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .flowchart-link{stroke:rgb(175, 175, 175);fill:none;}#chatgpt-mermaid-\_r_13lq\_ .edgeLabel{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lq\_ .edgeLabel p{background-color:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lq\_ .edgeLabel rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lq\_ .labelBkg{background-color:rgba(0, 0, 0, 0.5);}#chatgpt-mermaid-\_r_13lq\_ .cluster rect{fill:rgb(48, 48, 48);stroke:rgba(255, 255, 255, 0.15);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .cluster text{fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ .cluster span{color:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ div.mermaidTooltip{position:absolute;text-align:center;max-width:200px;padding:2px;font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";font-size:12px;background:rgb(48, 48, 48);border:1px solid rgba(255, 255, 255, 0.15);border-radius:2px;pointer-events:none;z-index:100;}#chatgpt-mermaid-\_r_13lq\_ .flowchartTitleText{text-anchor:middle;font-size:18px;fill:rgb(237, 237, 237);}#chatgpt-mermaid-\_r_13lq\_ rect.text{fill:none;stroke-width:0;}#chatgpt-mermaid-\_r_13lq\_ .icon-shape,#chatgpt-mermaid-\_r_13lq\_ .image-shape{background-color:rgb(0, 0, 0);text-align:center;}#chatgpt-mermaid-\_r_13lq\_ .icon-shape p,#chatgpt-mermaid-\_r_13lq\_ .image-shape p{background-color:rgb(0, 0, 0);padding:2px;}#chatgpt-mermaid-\_r_13lq\_ .icon-shape .label rect,#chatgpt-mermaid-\_r_13lq\_ .image-shape .label rect{opacity:0.5;background-color:rgb(0, 0, 0);fill:rgb(0, 0, 0);}#chatgpt-mermaid-\_r_13lq\_ .label-icon{display:inline-block;height:1em;overflow:visible;vertical-align:-0.125em;}#chatgpt-mermaid-\_r_13lq\_ .node .label-icon path{fill:currentColor;stroke:revert;stroke-width:revert;}#chatgpt-mermaid-\_r_13lq\_ .node .neo-node{stroke:rgb(31, 78, 148);}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node rect,#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].cluster rect,#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node polygon{stroke:url(#chatgpt-mermaid-\_r_13lq\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].swimlane.cluster rect{filter:none;}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node path{stroke:url(#chatgpt-mermaid-\_r_13lq\_-gradient);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node .outer-path{filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node .neo-line path{stroke:rgb(31, 78, 148);filter:none;}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node circle{stroke:url(#chatgpt-mermaid-\_r_13lq\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].node circle .state-start{fill:#000000;}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].icon-shape .icon{fill:url(#chatgpt-mermaid-\_r_13lq\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lq\_ [data-look="neo"].icon-shape .icon-neo path{stroke:url(#chatgpt-mermaid-\_r_13lq\_-gradient);filter:drop-shadow( 1px 2px 2px rgba(185,185,185,1));}#chatgpt-mermaid-\_r_13lq\_ .node text{font-size:14px;font-weight:600;letter-spacing:normal;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lq\_ .edgeLabels text{font-size:13px;font-weight:600;letter-spacing:-0.08px;fill:rgb(153, 206, 255);}#chatgpt-mermaid-\_r_13lq\_ .node tspan[font-weight="normal"],#chatgpt-mermaid-\_r_13lq\_ .edgeLabels tspan[font-weight="normal"]{font-weight:600;}#chatgpt-mermaid-\_r_13lq\_ .edgeLabel .label rect{opacity:1;rx:13px;ry:13px;fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .node rect,#chatgpt-mermaid-\_r_13lq\_ .node circle,#chatgpt-mermaid-\_r_13lq\_ .node ellipse,#chatgpt-mermaid-\_r_13lq\_ .node polygon,#chatgpt-mermaid-\_r_13lq\_ .node path{fill:rgb(0, 40, 77);stroke:rgba(255, 255, 255, 0.1);stroke-width:1px;}#chatgpt-mermaid-\_r_13lq\_ .node rect{rx:16px;ry:16px;}#chatgpt-mermaid-\_r_13lq\_ .mermaid-decision .label-container{fill:rgb(0, 14, 26);stroke:rgb(26, 62, 95);stroke-dasharray:2,2;}#chatgpt-mermaid-\_r_13lq\_ .edgePaths .flowchart-link{stroke:rgb(175, 175, 175);stroke-width:1px;stroke-linecap:round;stroke-linejoin:round;}#chatgpt-mermaid-\_r_13lq\_ .marker{fill:rgb(175, 175, 175);stroke:rgb(175, 175, 175);}#chatgpt-mermaid-\_r_13lq\_ :root{--mermaid-font-family:-apple-system-body,ui-sans-serif,-apple-system,system-ui,"Segoe UI",Helvetica,"Apple Color Emoji",Arial,sans-serif,"Segoe UI Emoji","Segoe UI Symbol";}Task PromptLead AgentDeveloper AgentReviewer AgentValidator AgentEvaluator AgentReview Evidence CollectorHuman and ArchitectureReviewsLearning EnginePattern and Root-CauseAnalysisHarness MemoryImprovementRecommendationsHuman GovernanceVersioned Harness ArtifactsFuture Engineering RunsApproved and Verified

Explain that the Harness orchestrates the workflow, while individual Agents retain their defined responsibilities and authority boundaries. Memory retrieval must remain subject to trust and approval status.

## **7. Hands-on Example — Learning from Alpha Car Detailing Reviews**

### **7.1 Scenario and Business Context**

- Corporate fleet booking enhancements.
- Multiple API and domain-layer changes.
- Repeated code reviews over several development iterations.
- Need for systematic improvement across engineering runs.

### **7.2 Initial Harness Configuration**

- Existing Lead, Developer, Reviewer, Validator, and Evaluator roles.
- Clean Architecture Instructions.
- API implementation Skills.
- Testing requirements.
- Review artifact storage.
- Harness Memory integration.
- Existing deterministic validation gates.

### **7.3 Review Scenario A — Recurring Architecture Violations**

- Developer Agent introduces infrastructure dependencies into application-layer code.
- Reviewer Agent identifies the violation.
- Similar findings recur across multiple runs.
- Learning Engine groups findings into an architecture pattern.
- Root-cause analysis considers insufficient Skill guidance, unclear Instructions, and missing architecture gates.
- Proposed improvement: clarify the implementation workflow and introduce or strengthen deterministic architecture checks.

### **7.4 Review Scenario B — Missing Tests**

- Corporate fleet booking limits are implemented.
- Reviewer Agent identifies missing boundary tests.
- Similar omissions appear in later implementations.
- Pattern detection identifies recurring testing gaps.
- Candidate improvement: strengthen the API implementation Skill's testing checklist and task-specific acceptance criteria.

### **7.5 Review Scenario C — API Contract Issues**

- Booking endpoints introduce inconsistent request or response contracts.
- Human review identifies backward-compatibility concerns.
- Harness correlates findings with changes to API design.
- Candidate improvement: include an API contract compatibility checkpoint and deterministic contract tests.

### **7.6 Review Scenario D — Security Findings**

- Corporate fleet endpoints fail to consistently enforce tenant-level authorization.
- Security review identifies unauthorized access risks.
- Findings are escalated independently of recurrence thresholds.
- Learning Engine proposes improved security checks and Skill guidance.
- Security owner approval remains mandatory.

### **7.7 Review Scenario E — Repeated Maintainability Problems**

- Business logic is duplicated across booking services.
- Reviewer Agent repeatedly requests refactoring.
- Harness identifies similar code organization issues.
- Learning analysis considers whether architecture guidance or Skill design contributes to the problem.
- Recommendation prioritizes maintainability without enforcing unnecessary abstraction.

### **7.8 Review Scenario F — Rejected Findings and False Learning**

- Reviewer Agent reports a perceived architectural violation.
- Human architect confirms the implementation is an approved exception.
- Finding is marked not applicable.
- Learning Engine retains the disposition and prevents the finding from becoming an unsupported standard.
- Future review behavior is assessed without automatically weakening architecture enforcement.

### **7.9 Implementing the Review Evidence Model**

- Sample structured findings.
- Evidence ingestion interface.
- Review classification service.
- Finding correlation service.
- Learning candidate record.
- Memory storage abstraction.
- Approval decision record.
- Audit event model.

### **7.10 Example Harness Workspace Structure**

Proposed illustrative structure:

```
harness/
├── reviews/
│   ├── schemas/
│   ├── collectors/
│   ├── classifiers/
│   └── patterns/
├── learning/
│   ├── analyzers/
│   ├── recommendations/
│   └── effectiveness/
├── memory/
│   ├── evidence/
│   ├── candidates/
│   └── approved/
├── governance/
│   ├── approvals/
│   └── policies/
├── audit/
└── runs/
```

Explain how these responsibilities can map to the actual companion Harness implementation without requiring a separate service for every folder.

### **7.11 End-to-End Review Learning Run**

1. Execute a development task.
2. Produce review findings.
3. Capture and normalize findings.
4. Verify evidence and disposition.
5. Compare findings with prior runs.
6. Identify a recurring pattern.
7. Generate root-cause hypotheses.
8. Produce an improvement recommendation.
9. Submit the recommendation for human review.
10. Apply an approved versioned change.
11. Validate and evaluate the revised behavior.
12. Monitor whether recurrence decreases.

### **7.12 Success Criteria**

- Review findings are traceable to their original evidence.
- Similar findings can be grouped without losing context.
- Learning candidates include reproducible justification.
- Human approval is enforced.
- Governed artifacts remain unchanged before authorization.
- Versioning and rollback are supported.
- Subsequent runs can be measured against baseline recurrence.

## **8. Claude Example — Review-Driven Learning with Claude Code**

### **8.1 Claude Code's Role in the Workflow**

- Generating structured review findings.
- Examining relevant repository context.
- Analyzing historical findings supplied by the Harness.
- Producing candidate recommendations.
- Respecting existing Instructions and Skills.
- Avoiding unauthorized modification.

### **8.2 Reviewer Agent Instructions**

- Structured finding format.
- Evidence-first assessment.
- Severity classification.
- Confidence and uncertainty.
- Relevant architecture references.
- Separation of factual observations and recommendations.

### **8.3 Review Analysis Prompt**

- Analyze multiple historical review artifacts.
- Identify recurring patterns.
- Distinguish duplicates from independent occurrences.
- Consider conflicting findings.
- Produce root-cause hypotheses.
- Reference evidence rather than fabricating conclusions.

### **8.4 Skill Improvement Recommendation**

- Inspect the existing API creation Skill.
- Identify missing testing or architecture checks.
- Prepare a proposed Skill diff.
- Explain expected benefits and potential risks.
- Require human approval.

### **8.5 Instruction Improvement Recommendation**

- Identify ambiguity in existing architecture Instructions.
- Draft a candidate clarification.
- Explain potential compatibility effects.
- Never automatically overwrite `CLAUDE.md`.

### **8.6 Integrating Claude with the Harness**

- Read-only evidence access.
- Restricted recommendation output.
- Structured response validation.
- Separate approval and application steps.
- Run-scoped workspace isolation.
- Controlled access to Harness Memory.

### **8.7 Claude Example Acceptance Criteria**

- All claims trace to supplied evidence.
- Unverified hypotheses are explicitly marked.
- No unauthorized file changes.
- Outputs conform to the expected schema.
- Approval decisions remain external to the recommending Agent.

## **9. GitHub Copilot Comparison**

### **9.1 Review Capabilities**

- GitHub Copilot-assisted code review.
- Review findings in pull request workflows.
- Repository-level instructions and contextual guidance.
- Dependence on available product capabilities and configured integrations.

### **9.2 Evidence Capture**

- Collecting review comments and related metadata.
- Relating feedback to commits and pull requests.
- Capturing human dispositions.
- Normalizing review evidence for the Harness.

### **9.3 Learning Architecture**

- Why the Harness should own persistent learning logic.
- Separating product-native review assistance from enterprise Memory.
- External pattern detection and recommendation management.
- Governance and approval independent of the review tool.

### **9.4 Comparison with Claude Code**

- Review workflow integration.
- Instruction mechanisms.
- Artifact portability.
- Approval boundaries.
- Vendor-neutral learning contracts.

## **10. Codex Comparison**

### **10.1 Codex-Assisted Review Workflows**

- Repository-aware code inspection.
- Structured findings.
- Patch and implementation review.
- Evidence collection from supported development workflows.

### **10.2 Integration with Harness Memory**

- Exporting findings through a controlled contract.
- Mapping review artifacts to canonical schemas.
- Preserving model and tool provenance.
- Maintaining learning across tool changes.

### **10.3 Governance and Safety**

- Restricted permissions.
- Review versus modification authority.
- Controlled proposal generation.
- Human approval and traceability.

### **10.4 Vendor-Neutral Design**

- Common evidence model across Claude Code, Copilot, and Codex.
- Tool-specific adapters.
- Stable governance rules.
- Avoiding dependence on one vendor's review format.

## **11. Best Practices**

### **11.1 Evidence Quality**

- Capture findings in structured, versioned formats.
- Preserve the original source and context.
- Retain disposition and resolution.
- Verify consequential claims.
- Record uncertainty and disagreement.

### **11.2 Pattern Detection**

- Require independent evidence where possible.
- Distinguish high-severity isolated issues from recurring low-severity findings.
- Use contextual similarity rather than keyword matching alone.
- Track pattern age and relevance.
- Avoid overgeneralization.

### **11.3 Root-Cause Analysis**

- Compare multiple plausible causes.
- Verify assumptions before changing engineering artifacts.
- Separate Agent mistakes from missing organizational standards.
- Prefer a deterministic gate for an objectively testable invariant.

### **11.4 Governance**

- Preserve approval authority.
- Make recommendation generation read-only by default.
- Version approved changes.
- Maintain rollback paths.
- Record rejected recommendations.
- Keep Harness Memory distinct from authoritative Instructions.

### **11.5 Continuous Improvement**

- Track recurrence before and after changes.
- Use representative regression tasks.
- Assess false-positive rates.
- Monitor review workload and resolution quality.
- Retire ineffective recommendations.
- Avoid optimizing solely for fewer review comments.

## **12. Anti-patterns**

### **12.1 Automatically Rewriting Instructions**

- Allowing Reviewer Agent findings to directly modify `CLAUDE.md`.
- Risks of silent policy drift.
- Corrective governance pattern.

### **12.2 Treating Every Finding as Truth**

- Reviewer hallucinations and incomplete context.
- Unverified concerns.
- Importance of disposition and corroboration.

### **12.3 Learning from a Single Contextual Exception**

- Local implementation choices becoming global rules.
- Context-sensitive applicability requirements.

### **12.4 Counting Duplicate Comments as Repeated Evidence**

- One finding copied across multiple review systems.
- Artificially inflated recurrence confidence.

### **12.5 Confusing Correlation with Root Cause**

- Blaming Skills or Prompts without verifying their effect.
- Proposing changes unrelated to the actual implementation defect.

### **12.6 Overfitting to Reviewer Preferences**

- Formatting or stylistic opinions mistaken for architecture requirements.
- Conflicting preferences among reviewers.

### **12.7 Creating Endless Instructions**

- Accumulating corrections without consolidation.
- Growing, contradictory, or unmaintainable Instructions.
- Knowledge lifecycle and periodic review.

### **12.8 Ignoring Rejected Recommendations**

- Repeatedly proposing previously rejected changes.
- Losing governance context.

### **12.9 Allowing Self-Reinforcing Learning**

- Agents citing their own earlier unverified findings.
- Circular evidence and false confidence.

### **12.10 Measuring Success by Review Volume**

- Fewer comments interpreted as higher quality.
- Reviewer fatigue and coverage changes.
- Importance of independently validated outcomes.

## **13. Architect's Notes**

### **13.1 Learning Is Not Authority**

- The Harness may propose but cannot independently establish standards.
- Authority remains with designated owners and governance mechanisms.

### **13.2 Review Findings Are Contextual**

- An architecture concern may be valid for one component but not another.
- Evidence must preserve applicability constraints.

### **13.3 Memory Must Preserve Provenance**

- Historical observations are not the same as approved instructions.
- Memory entries need sources, owners, dates, and status.

### **13.4 Root-Cause Analysis Is Probabilistic**

- Similar defects can have different causes.
- Confidence must not be represented as certainty.
- Critical decisions require independent verification.

### **13.5 Deterministic Enforcement Should Be Preferred Where Possible**

- Repeated dependency-direction violations may justify architecture tests.
- API compatibility defects may justify contract validation.
- Security invariants may justify dedicated policy or security gates.

### **13.6 Approval Should Be Risk-Based but Never Implicit**

- Different owners may approve different artifact classes.
- Approval must be explicit for governed changes.
- Risk-based routing determines the review process, not whether governance is bypassed.

### **13.7 Design for Multiple Repositories and Teams**

- Shared learning patterns.
- Repository-specific standards.
- Tenant and project isolation.
- Controlled promotion of generally applicable improvements.

### **13.8 Keep the Learning Plane Separate from the Execution Plane**

- Observation, analysis, and recommendation are distinct from applying changes.
- Avoid allowing learning services to bypass normal engineering controls.

## **14. Enterprise Tips**

### **14.1 Begin with High-Value Findings**

- Recurring architecture violations.
- Missing test coverage.
- API contract regressions.
- Authorization and security problems.

### **14.2 Use Existing Review Infrastructure**

- Pull request reviews.
- Static analysis and architecture check outputs.
- Existing security scanning evidence.
- Technical review checklists.

### **14.3 Introduce a Review Taxonomy**

- Standard categories across projects.
- Consistent severity definitions.
- Explicit disposition labels.

### **14.4 Assign Learning Ownership**

- Architecture owners.
- Security owners.
- Skill maintainers.
- Prompt maintainers.
- Harness platform team.

### **14.5 Measure Meaningful Improvements**

- Recurrence rate per comparable engineering run.
- Number of independent confirmed findings.
- Time spent correcting recurring issues.
- Escaped defects.
- False-positive and rejected-finding rates.
- Effectiveness of approved interventions.

### **14.6 Protect Sensitive Review Evidence**

- Minimize code and personal data retained in Memory.
- Apply access controls.
- Prevent secrets from entering learning artifacts.
- Define retention and deletion rules.
- Avoid cross-client knowledge leakage.

### **14.7 Use Controlled Pilots**

- Begin with a limited repository scope.
- Compare before and after results.
- Expand only after validated improvements.
- Maintain rollback capability.

## **15. Decision Points**

| Decision                        | Options                                                          | Architectural consideration          |
| ------------------------------- | ---------------------------------------------------------------- | ------------------------------------ |
| Where to store findings?        | Repository artifacts, centralized evidence store, hybrid         | Traceability, scale, access          |
| What qualifies as recurrence?   | Exact match, semantic similarity, contextual pattern             | False positives and evidence quality |
| When to recommend improvements? | Severity-based, recurrence-based, combined                       | Risk and engineering impact          |
| How to determine root cause?    | Rule-based, AI-assisted, hybrid                                  | Reliability and explainability       |
| Where to retain learning?       | Raw evidence, candidate Memory, approved Memory                  | Authority separation                 |
| Which artifact should change?   | Prompt, Skill, Instruction, workflow, gate                       | Root-cause alignment                 |
| Who approves changes?           | Technical lead, architect, security owner, designated maintainer | Ownership and governance             |
| How to validate improvements?   | Replay tasks, regression suites, pilot runs                      | Evidence of effectiveness            |
| When to roll back?              | Quality regression, false positives, policy conflict             | Operational safety                   |
| How to share patterns?          | Repository-local, team-wide, enterprise-wide                     | Relevance, isolation, governance     |

Include a decision framework showing when a finding should result in immediate defect remediation, continued observation, a recommendation, or human escalation.

## **16. Exercises**

### **Exercise 39.1 — Classify Review Findings**

Given a set of Alpha Car Detailing findings:

- Assign categories.
- Assign severity.
- Identify evidence quality.
- Determine disposition.
- Document ambiguous cases.

### **Exercise 39.2 — Identify Recurring Architecture Problems**

- Analyze findings from several booking implementation runs.
- Group comparable violations.
- Remove duplicates.
- Determine whether a systemic pattern exists.

### **Exercise 39.3 — Perform Root-Cause Analysis**

- Investigate repeated missing boundary tests.
- Compare Prompt, Skill, Instruction, and workflow explanations.
- Identify supporting and contradictory evidence.
- Prepare a justified root-cause assessment.

### **Exercise 39.4 — Design a Review Evidence Schema**

- Define a structured review record.
- Include version and source references.
- Support human dispositions.
- Include evidence and confidence fields.
- Ensure auditability.

### **Exercise 39.5 — Design Memory Promotion**

- Identify candidate findings.
- Define evidence thresholds.
- Route candidates to the appropriate owners.
- Preserve rejected decisions.
- Prevent unauthorized promotions.

### **Exercise 39.6 — Generate an Improvement Recommendation**

- Select a recurring API contract defect.
- Recommend a Skill or validation gate improvement.
- Include evidence references.
- Define acceptance criteria.
- Specify rollback conditions.

### **Exercise 39.7 — Investigate False Learning**

- Introduce a deliberately incorrect Reviewer Agent finding.
- Record a human rejection.
- Verify the learning process does not promote it.
- Explain how negative evidence affects future recommendations.

### **Exercise 39.8 — Validate Improvement Effectiveness**

- Establish a baseline recurrence rate.
- Apply an approved controlled improvement.
- Run comparable implementation tasks.
- Measure recurrence, quality, and false positives.
- Decide whether to retain or roll back the change.

### **Exercise 39.9 — Extend the Harness**

- Add review evidence ingestion to the Alpha Car Detailing Harness.
- Implement finding classification and correlation.
- Store learning candidates.
- Add an approval boundary.
- Produce a review learning report.
- Demonstrate that governed artifacts are never changed automatically.

## **17. Interview Questions**

### **Foundational**

1. What is review-driven learning in an Adaptive Harness?
2. How does learning from reviews differ from conventional code review?
3. Why should review findings be considered signals rather than authoritative Instructions?
4. What is the difference between a review finding, a recurring pattern, and a learning candidate?
5. How do Reviewer Agent findings differ from deterministic validation results?

### **Architecture**

1. What components are required to implement review-driven learning?
2. How would you design a canonical review finding schema?
3. How should an Adaptive Harness identify semantically similar findings?
4. How can review findings be correlated with implementation decisions?
5. How do you distinguish root-cause hypotheses from verified root causes?
6. How does Harness Memory support review-driven learning?
7. What architectural boundaries should separate learning from execution?
8. How should review-driven learning support multiple repositories?
9. When should a repeated review finding become a deterministic validation gate?

### **Governance**

 1. Why must governed Instructions and Skills require human approval?
 2. How should conflicting human review decisions be handled?
 3. What evidence is sufficient to recommend an Instruction change?
 4. How should rejected findings influence future learning?
 5. How do versioning and rollback protect an Adaptive Harness?
 6. What audit records should accompany an approved learning-driven change?

### **Advanced Scenarios**

 1. An AI Reviewer repeatedly flags an approved architectural exception. How should the Harness respond?
 2. A security issue appears only once but has critical severity. Should the Harness wait for recurrence?
 3. Multiple reviews identify missing tests, but the existing Skill already requires them. What should be investigated?
 4. A newly approved Skill reduces review comments but increases production defects. How should the improvement be evaluated?
 5. How would you prevent a Harness from reinforcing its own incorrect historical findings?
 6. How can the system distinguish a repository-specific pattern from an enterprise-wide improvement opportunity?
 7. Which metrics demonstrate that review-driven learning improves engineering outcomes rather than merely reducing review activity?

## **18. Chapter Summary**

The chapter summary will reinforce the following conclusions:

- Review findings provide valuable evidence for improving future AI-assisted engineering.
- Reviewer Agent findings, human reviews, architecture reviews, and security reviews contribute different kinds of learning signals.
- Structured evidence capture is essential for reliable classification and pattern identification.
- Repeated findings can indicate weaknesses in Prompts, Skills, Instructions, workflow coordination, or deterministic validation.
- Root-cause analysis must distinguish evidence from hypotheses and correlation from causation.
- Harness Memory preserves historical findings and governed learning without replacing authoritative engineering standards.
- Accepted and rejected changes both provide useful learning context.
- Confidence thresholds help prioritize investigation but do not grant modification authority.
- Human approval, version control, validation, rollback, and auditability are essential enterprise safeguards.
- The effectiveness of learning must be evaluated through subsequent engineering outcomes, not simply review volume.
- The Adaptive Harness improves by learning from reviews while preserving established governance boundaries.

**Closing principle:** An Adaptive Harness becomes more effective not by automatically obeying every review comment, but by transforming verified review evidence into carefully governed improvements that prevent recurring engineering problems.

## **19. Further Reading**

The completed chapter will reference authoritative material covering:

### **19.1 Code Review and Engineering Quality**

- *Software Engineering at Google* — code review and engineering practices.
- *Code Complete* — maintainability and software construction quality.
- *Accelerate* — software delivery performance and improvement measurement.

### **19.2 Software Architecture**

- *Clean Architecture* — dependency boundaries and architectural principles.
- *Fundamentals of Software Architecture* — architecture characteristics and governance.
- *Building Evolutionary Architectures* — architectural fitness functions and controlled evolution.

### **19.3 Security and Governance**

- NIST Secure Software Development Framework (SSDF).
- OWASP guidance on secure code review.
- NIST AI Risk Management Framework.
- Relevant organizational change-management and audit standards.

### **19.4 AI-Assisted Engineering**

- Current Claude Code documentation concerning Instructions, Skills, and development workflows.
- Current GitHub Copilot code review documentation.
- Current OpenAI Codex documentation concerning code review and repository workflows.
- Documentation for structured evaluation and human-in-the-loop approval patterns.

### **19.5 Handbook Cross-References**

- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 31 — Human Approval.
- Chapter 32 — Audit Trails.
- Chapter 37 — Harness Memory.
- Chapter 38 — Learning from Builds.
- Chapter 40 — Learning from Pull Requests.
- Chapter 41 — Skill Evolution.
- Chapter 42 — Prompt Evolution.
- Chapter 43 — Instruction Evolution.
- Chapter 45 — Autonomous Improvement Engine.

**Chapter 39 outline is complete.**
