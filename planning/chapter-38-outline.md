# **Chapter 38 — Learning from Builds**

**Book:** Enterprise AI Engineering Handbook\
**Part:** Part VI — Self-Learning AI Systems\
**Target File:** `book/38-learning-from-builds.md`\
**Repository:** [Enterprise-AI-Engineering-Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)

## **Chapter Objective**

Explain how an Adaptive Harness systematically captures, analyzes, classifies, and learns from build evidence to improve future AI-assisted engineering runs.

The chapter establishes how compiler errors, dependency conflicts, configuration problems, environment failures, recurring build patterns, and successful build outcomes can contribute to Harness Memory and generate actionable improvement recommendations.

It also defines the governance boundaries that prevent build-derived learning from silently modifying Instructions, Skills, Prompts, deterministic validation gates, or other governed engineering artifacts.

**Core Principle:**

> Build evidence can inform future engineering behavior, but the Harness must recommend improvements rather than silently changing governed Instructions, Skills, or validation rules.

**Primary scenario:** Alpha Car Detailing, a .NET 10 enterprise application using Clean Architecture, SQL Server, APIs, background processing, and event-driven integration.

## **1. Story-Driven Opening — When the Same Build Fails Again**

### **1.1 The recurring build failure**

- Introduce a new corporate fleet booking feature in Alpha Car Detailing.
- The Developer Agent generates code across application, domain, infrastructure, and API projects.
- The build fails because of a missing project reference.
- The Developer Agent resolves the issue during the current run.
- A subsequent engineering run encounters an equivalent failure.
- The Harness repeats its previous investigation without benefiting from the earlier resolution.

### **1.2 The cost of repeated failures**

- Repeated token consumption and agent execution cycles.
- Unnecessary build and remediation iterations.
- Increased CI/CD execution time.
- Delayed pull requests.
- Reduced engineering predictability.
- Difficulty identifying recurring structural problems.

### **1.3 The architectural question**

- Why can an AI Agent resolve an issue successfully yet encounter it again?
- What information should survive an engineering run?
- How should build observations become reusable knowledge?
- What prevents a temporary workaround from becoming a permanent engineering standard?

### **1.4 Introducing build-informed learning**

- Distinguish temporary remediation from reusable engineering learning.
- Introduce build evidence as a potential source of Harness Memory.
- Explain how repeated failures may reveal deficiencies in Skills, Prompts, workflows, or repository guidance.
- Establish the separation between learning, recommendation, and approved change.

### **1.5 Chapter narrative**

Follow a recurring .NET build problem from initial detection through evidence collection, classification, pattern discovery, recommendation, human approval, and verification in future engineering runs.

## **2. Learning Objectives**

After completing this chapter, readers should be able to:

1. Explain why build evidence is an important learning source for an Adaptive Harness.
2. Distinguish successful build signals from failure signals.
3. Classify compiler, dependency, configuration, environment, and contract-related build failures.
4. Design structured build evidence capture and storage.
5. Correlate build failures with source code and dependency changes.
6. Identify repeated failure patterns across engineering runs.
7. Distinguish symptoms, root causes, and contributing conditions.
8. Extract reusable remediation candidates from successful and failed builds.
9. Integrate build learning with Harness Memory.
10. Establish confidence and evidence thresholds for learning recommendations.
11. Recommend improvements to Skills, Prompts, tools, and workflows.
12. Enforce human approval before modifying governed artifacts.
13. Implement versioning, rollback, and auditability for approved improvements.
14. Prevent false learning, evidence contamination, and harmful automated remediation.
15. Design a build-learning subsystem within the enterprise Harness.
16. Evaluate whether build-derived recommendations improve engineering outcomes.

## **3. Background — Builds as Engineering Evidence**

### **3.1 The role of builds in software engineering**

- Builds as executable feedback about source code and project configuration.
- Compilation versus full engineering validation.
- Build pipelines in modern .NET applications.
- Build results as objective evidence within a larger engineering process.

### **3.2 Build behavior in traditional development**

- Developer-driven debugging.
- Local build feedback.
- CI/CD build pipelines.
- Human knowledge accumulated through repeated troubleshooting.
- Knowledge lost when troubleshooting remains undocumented.

### **3.3 Build behavior in AI-assisted development**

- Agent-generated code changes.
- Automated compilation.
- Iterative code correction.
- Validator Agent verification.
- Run-scoped troubleshooting.
- Repeated remediation without durable learning.

### **3.4 Static versus Adaptive Harness behavior**

| Dimension        | Static Harness                     | Adaptive Harness                                   |
| ---------------- | ---------------------------------- | -------------------------------------------------- |
| Build execution  | Executes configured build commands | Executes governed build commands                   |
| Failure analysis | Reports failures                   | Classifies and correlates failures                 |
| Build history    | Stores logs and run results        | Analyzes historical evidence                       |
| Recurring errors | Handles each occurrence            | Detects repeated patterns                          |
| Remediation      | Resolves current-run issues        | Proposes reusable remediation                      |
| Knowledge        | Primarily predefined               | Evidence-backed Harness Memory                     |
| Governance       | Applies established controls       | Recommends improvements under established controls |

### **3.5 Position within Part VI**

- Relationship to Chapter 36 — Static vs Adaptive Harnesses.
- Relationship to Chapter 37 — Harness Memory.
- Foundation for Chapter 39 — Learning from Reviews.
- Connection to Skill Evolution, Prompt Evolution, and Instruction Evolution.
- Build learning as one specialized evidence source within the broader self-learning architecture.

### **3.6 Limits of build evidence**

- Successful compilation does not establish functional correctness.
- Build failures do not necessarily indicate developer mistakes.
- Green builds do not guarantee architecture compliance.
- Build learning must not substitute for deterministic validation or human engineering judgment.

## **4. Concepts — Foundations of Learning from Builds**

### **4.1 What is Learning from Builds?**

- Definition and scope.
- Distinction between build automation and build learning.
- Evidence collection versus pattern discovery.
- Pattern discovery versus recommendation.
- Recommendation versus approved knowledge evolution.

### **4.2 Understanding build signals**

#### **4.2.1 Build success signals**

- Successful compilation.
- Successful dependency restoration.
- Resolved warnings.
- Stable build duration.
- Repeatable builds.
- Consistent build behavior across environments.

#### **4.2.2 Build failure signals**

- Compiler errors.
- Missing dependencies.
- Package version conflicts.
- Missing references.
- Invalid project configuration.
- SDK incompatibilities.
- Resource constraints.
- Environment failures.
- Build-time contract or source-generation failures.

#### **4.2.3 Weak and ambiguous signals**

- Intermittent failures.
- Non-deterministic results.
- Warning-only outcomes.
- Cached build results.
- Failures unrelated to current code changes.
- Transient network and infrastructure errors.

### **4.3 Compiler errors as structured evidence**

- Syntax errors.
- Type mismatches.
- Missing namespaces.
- Missing method or property definitions.
- Interface implementation errors.
- Inaccessible members.
- Nullability warnings and errors.
- Conflicting definitions.
- Generated-code failures.
- Error codes, messages, file locations, and diagnostic context.

**Alpha Car Detailing scenario:** A new `FleetBookingService` references a contract not exposed by the application project.

### **4.4 Dependency failures**

- NuGet restore failures.
- Package version incompatibilities.
- Transitive dependency conflicts.
- Missing package sources.
- Locked or incompatible dependency versions.
- Central Package Management inconsistencies.
- Project reference cycles.
- Restore failures caused by unavailable feeds.

**Alpha Car Detailing scenario:** An infrastructure project introduces an incompatible dependency version during integration with an event-processing component.

### **4.5 Configuration failures**

- Invalid `.csproj` settings.
- Incorrect target framework.
- Missing required MSBuild properties.
- Invalid solution references.
- Incorrect build configurations.
- Misconfigured code generation.
- Incorrect conditional compilation settings.

**Alpha Car Detailing scenario:** A new worker project targets a framework incompatible with the application solution's approved .NET baseline.

### **4.6 Environment-related failures**

- Missing .NET SDK.
- Operating system differences.
- Missing environment variables required by build tooling.
- File permissions.
- Path and case-sensitivity differences.
- Insufficient disk or memory.
- Network failures.
- Container image inconsistencies.
- Self-hosted build agent configuration drift.

### **4.7 Contract-related failures**

- Compile-time API contract mismatches.
- Changed interfaces not reflected in consumers.
- Generated client compatibility issues.
- Event schema code-generation errors.
- Shared DTO changes.
- Distinction between build-detectable contract failures and runtime contract violations.

**Alpha Car Detailing scenario:** The fleet booking request contract changes without updating a dependent application service.

### **4.8 Failure classification model**

Define a reusable classification taxonomy:

- Compiler.
- Dependency.
- Configuration.
- Environment.
- Contract.
- Build tooling.
- Generated code.
- Unknown or inconclusive.

Address primary and secondary categories, classification confidence, and the distinction between error symptoms and root causes.

### **4.9 Root-cause analysis**

- Error symptom versus underlying defect.
- First failure versus cascading failures.
- Primary and secondary diagnostic messages.
- Root-cause hypotheses.
- Evidence supporting each hypothesis.
- Confirmed versus suspected causes.
- Unresolved and inconclusive cases.
- Avoiding assumptions based on error text alone.

### **4.10 Repeated failure patterns**

- Error frequency.
- Error signature normalization.
- Failure recurrence across runs.
- Failure recurrence across repositories.
- Similar failures with different error messages.
- Repeated remediation actions.
- Distinguishing recurring defects from shared environmental incidents.

### **4.11 Learning from successful builds**

- Identifying successful remediation sequences.
- Recognizing stable implementation patterns.
- Detecting consistently effective build configurations.
- Identifying reliable dependency combinations.
- Capturing evidence from successful workflows.
- Distinguishing success correlation from proven causation.

### **4.12 Learning from failed builds**

- Capturing unresolved failure evidence.
- Identifying ineffective remediation attempts.
- Detecting repeated unsuccessful changes.
- Recognizing failure loops.
- Identifying missing prerequisite checks.
- Learning when to escalate rather than retry.

### **4.13 Candidate remediation patterns**

- Add or correct project references.
- Align package versions.
- Correct namespace usage.
- Update interface implementations.
- Correct build configuration.
- Restore required prerequisites.
- Improve dependency verification.
- Introduce earlier compatibility checks.
- Propose changes to reusable workflows.

### **4.14 Build evidence versus Harness Memory**

- Raw logs as evidence artifacts.
- Normalized diagnostics as structured observations.
- Temporary hypotheses in run-scoped scratchpads.
- Validated remediation patterns as memory candidates.
- Governed knowledge distinguished from provisional memory.
- Evidence provenance and applicability boundaries.

### **4.15 Learning versus self-modification**

- Learning does not automatically authorize modification.
- Memory retrieval does not override Instructions.
- Proposed improvements must pass governance checks.
- The Harness must not weaken deterministic gates to improve apparent success.
- All changes to governed artifacts require explicit authorization.

## **5. Architecture Discussion — Designing a Build-Learning Subsystem**

### **5.1 Architectural overview**

Introduce the Build Learning subsystem as an evidence-processing and recommendation component of the Adaptive Harness.

Primary responsibilities:

- Collect build results.
- Normalize diagnostics.
- Correlate build evidence.
- Classify failure causes.
- Identify recurring patterns.
- Evaluate remediation outcomes.
- Generate improvement recommendations.
- Integrate with Harness Memory.
- Support approval and audit workflows.

### **5.2 Architectural boundaries**

Separate the following responsibilities:

1. Build execution.
2. Deterministic validation.
3. Evidence collection.
4. Learning analysis.
5. Memory management.
6. Recommendation generation.
7. Governance approval.
8. Approved artifact updates.

Explain why the subsystem must not bypass the Validator Agent or independently redefine build success.

### **5.3 Core architectural components**

#### **5.3.1 Build Executor**

- Executes configured build commands.
- Supports `dotnet restore`, `dotnet build`, and approved build workflows.
- Captures exit codes and execution outcomes.
- Operates within controlled environments.

#### **5.3.2 Build Evidence Collector**

- Collects compiler diagnostics.
- Captures structured build logs.
- Records dependency restoration output.
- Associates results with run identifiers.
- Records build timing and execution metadata.

#### **5.3.3 Evidence Normalizer**

- Standardizes diagnostic formats.
- Removes irrelevant output noise.
- Preserves original error details.
- Produces normalized diagnostic signatures.
- Separates raw evidence from derived interpretations.

#### **5.3.4 Failure Classifier**

- Classifies failure types.
- Identifies likely root causes.
- Separates related and unrelated failures.
- Records confidence and uncertainty.
- Supports unknown classifications.

#### **5.3.5 Change Correlator**

- Associates errors with code changes.
- Identifies modified projects and dependencies.
- Compares current and baseline build results.
- Distinguishes newly introduced failures from pre-existing failures.
- Records correlation evidence without assuming causation.

#### **5.3.6 Pattern Detection Engine**

- Searches historical evidence.
- Detects recurring diagnostic signatures.
- Groups similar failure patterns.
- Measures recurrence and remediation effectiveness.
- Identifies environmental clusters.

#### **5.3.7 Learning Engine**

- Evaluates candidate lessons.
- Combines observations from multiple runs.
- Estimates confidence.
- Identifies applicable engineering contexts.
- Rejects unsupported learning candidates.

#### **5.3.8 Harness Memory Integration**

- Stores approved or appropriately classified learned observations.
- Maintains references to supporting evidence.
- Tracks confidence and applicability.
- Enforces retention and supersession policies.
- Separates provisional observations from authoritative knowledge.

#### **5.3.9 Recommendation Engine**

- Proposes Skill improvements.
- Proposes Prompt improvements.
- Recommends workflow or tooling changes.
- Produces structured, reviewable change proposals.
- Includes impact, risks, and supporting evidence.

#### **5.3.10 Governance and Approval Gateway**

- Enforces artifact ownership.
- Routes recommendations to authorized reviewers.
- Requires explicit approval for governed changes.
- Records approval and rejection decisions.
- Applies versioning and rollback controls.

### **5.4 End-to-end data flow**

- Engineering task initiated.
- Lead Agent establishes implementation plan.
- Developer Agent generates or modifies code.
- Validator Agent executes build checks.
- Build evidence is collected.
- Failures are classified and correlated.
- Current-run remediation proceeds within authorized boundaries.
- Learning analysis generates candidate patterns.
- Candidates are evaluated against historical evidence.
- Memory and recommendation workflows process qualified candidates.
- Human-approved changes influence subsequent runs.

### **5.5 Learning and operational feedback separation**

Distinguish two feedback cycles:

**Current-run remediation loop**

- Detect failure.
- Diagnose issue.
- Correct implementation.
- Rebuild.
- Validate outcome.

**Cross-run learning loop**

- Aggregate evidence.
- Detect patterns.
- Assess confidence.
- Generate recommendation.
- Obtain approval.
- Version approved changes.
- Evaluate future outcomes.

### **5.6 Integration with Harness roles**

| Role            | Responsibility                                         |
| --------------- | ------------------------------------------------------ |
| Lead Agent      | Coordinates implementation and remediation             |
| Developer Agent | Corrects implementation defects                        |
| Reviewer Agent  | Reviews code quality and proposed improvements         |
| Validator Agent | Produces objective build and validation evidence       |
| Evaluator Agent | Assesses goal satisfaction and engineering outcomes    |
| Learning Engine | Analyzes build evidence and proposes reusable learning |
| Human Approver  | Authorizes changes to governed artifacts               |

Explain that the Learning Engine consumes evidence but does not assume the authority of other roles.

### **5.7 Build evidence data contract**

Define the proposed conceptual fields:

- Evidence identifier.
- Harness run identifier.
- Repository and commit reference.
- Branch or pull-request reference.
- Build command.
- Build target and configuration.
- SDK and tooling versions.
- Environment fingerprint.
- Start and completion timestamps.
- Exit code and outcome.
- Diagnostic code and message.
- Affected project and file.
- Error signature.
- Failure category.
- Root-cause hypothesis.
- Remediation actions.
- Rebuild outcome.
- Supporting artifact references.
- Classification confidence.
- Analysis version.

### **5.8 Remediation pattern data contract**

- Pattern identifier.
- Pattern version.
- Normalized failure signature.
- Root-cause category.
- Required context.
- Prerequisites.
- Recommended remediation.
- Known exclusions.
- Supporting examples.
- Success and failure observations.
- Confidence level.
- Approval state.
- Expiration or review date.

### **5.9 Recommendation data contract**

- Recommendation identifier and version.
- Recommendation type.
- Target Skill, Prompt, tool, workflow, or Instruction.
- Problem statement.
- Evidence references.
- Proposed change.
- Expected benefit.
- Risks and tradeoffs.
- Confidence assessment.
- Proposed owner.
- Approval status.
- Verification criteria.
- Rollback instructions.

### **5.10 Storage and retention architecture**

- Raw build logs in artifact storage.
- Structured diagnostics in a queryable evidence store.
- Run-specific observations in scratchpad storage.
- Reusable patterns in Harness Memory.
- Recommendations in a governed proposal repository.
- Approval and implementation history in an audit store.
- Retention, access control, and sensitive data handling.

### **5.11 Confidence and evidence thresholds**

- Minimum evidence quantity.
- Recurrence thresholds.
- Independent supporting observations.
- Remediation success rates.
- Environment consistency.
- Causal plausibility.
- Counterexamples.
- Confidence decay.
- Risk-adjusted approval requirements.
- Threshold configuration under governance.

Distinguish illustrative thresholds from organization-specific production policies.

### **5.12 Versioning and rollback**

- Version learned patterns.
- Version memory entries.
- Version recommendation records.
- Link changes to repository commits.
- Preserve previous approved states.
- Detect regressions following approved changes.
- Support rollback and re-evaluation.

### **5.13 Security and trust boundaries**

- Treat build logs as untrusted evidence.
- Prevent log-based prompt injection.
- Redact secrets and credentials.
- Enforce repository and tenant isolation.
- Limit access to sensitive build artifacts.
- Prevent cross-project memory contamination.
- Require authorization for artifact modification.
- Preserve deterministic validation rules.

### **5.14 Operational reliability**

- Learning subsystem failures must not mask build failures.
- Asynchronous evidence processing where appropriate.
- Idempotent ingestion.
- Duplicate evidence detection.
- Retry and dead-letter policies.
- Evidence integrity checks.
- Monitoring and operational alerts.
- Failure recovery and replay.

### **5.15 Measuring learning effectiveness**

- Recurring failure rate.
- Average build attempts per successful run.
- Build remediation duration.
- Time to classify root cause.
- Recommendation approval rate.
- Recommendation precision.
- Regression rate after approved changes.
- False-learning rate.
- Build cost per engineering run.
- Improvement durability across future runs.

Explain measurement limitations and the need for comparable workloads and environments.

## **6. Professional Diagrams**

Five diagrams will support the chapter.

### **6.1 Diagram 38.1 — Build Evidence Feedback Loop**

**Purpose:** Show how build outcomes contribute to future engineering improvement.

Conceptual feedback loop: future runs benefit from evidence-backed, authorized improvements.

Outline discussion:

- Build evidence enters the learning pipeline.
- Patterns inform recommendations.
- Governed approval controls changes to reusable guidance.
- Future runs provide evidence to evaluate those changes.
- Learning does not bypass validation.

### **6.2 Diagram 38.2 — Failure Classification Flow**

**Purpose:** Demonstrate how an observed build failure becomes a classified, evidence-backed diagnostic record.

Diagram elements:

- Build failure detected.
- Diagnostic evidence normalized.
- Failure classification decision.
- Compiler / dependency / configuration / environment / contract / unknown branches.
- Root-cause investigation.
- Confidence assessment.
- Known pattern or new observation.
- Remediation or escalation.
- Outcome captured.

Include unresolved classification and low-confidence paths.

### **6.3 Diagram 38.3 — Build-to-Memory Promotion**

**Purpose:** Explain how raw evidence becomes reusable memory without creating unverified authoritative knowledge.

Diagram elements:

- Raw build evidence.
- Structured diagnostic.
- Run-scoped scratchpad observation.
- Candidate remediation pattern.
- Evidence validation.
- Confidence evaluation.
- Memory admission decision.
- Provisional or approved memory record.
- Supersession and expiration.

Include rejection, insufficient evidence, and human review paths.

### **6.4 Diagram 38.4 — Recommendation and Approval Flow**

**Purpose:** Show the governance process for changes to engineering artifacts.

Diagram elements:

- Learning candidate.
- Evidence package.
- Recommendation.
- Artifact owner.
- Review and approval.
- Approve / reject / request additional evidence.
- Version-controlled change.
- Deterministic validation.
- Outcome monitoring.
- Rollback when required.

Explicitly show that recommendations do not directly modify governed Instructions, Skills, or validation gates.

### **6.5 Diagram 38.5 — Learning from Builds Inside the Harness**

**Purpose:** Present the complete architectural integration.

Diagram layers:

- Engineering Orchestration.
- AI Agent Roles.
- Build and Validation Execution.
- Evidence Collection.
- Classification and Correlation.
- Pattern Detection.
- Learning Engine.
- Harness Memory.
- Recommendation Management.
- Governance and Approval.
- Metrics, Audit, and Observability.

Diagram boundaries:

- Operational execution boundary.
- Learning analysis boundary.
- Memory trust boundary.
- Governed artifact modification boundary.

## **7. Hands-on Example — Alpha Car Detailing Build Learning**

### **7.1 Scenario overview**

Implement build learning for recurring failures introduced while extending corporate fleet booking functionality.

### **7.2 Sample project context**

- .NET 10 solution.
- Domain layer.
- Application layer.
- Infrastructure layer.
- API layer.
- Unit and integration test projects.
- Approved project references and dependency conventions.

### **7.3 Scenario A — Missing project reference**

- Developer Agent introduces a cross-project type reference.
- Build fails.
- Diagnostic evidence is captured.
- Missing project dependency is identified.
- Current-run fix is applied.
- Subsequent successful build is recorded.
- Repeated occurrences are detected across runs.
- Harness proposes a project-reference prerequisite in the relevant Skill.

### **7.4 Scenario B — NuGet package conflict**

- A new package is introduced.
- Dependency resolution reports incompatible versions.
- Restore evidence is captured.
- Direct and transitive dependencies are analyzed.
- Remediation is validated.
- Learning Engine identifies a reusable compatibility pattern.
- Harness recommends an earlier dependency compatibility check.

### **7.5 Scenario C — Broken interface contract**

- Fleet booking API changes.
- Dependent application component no longer satisfies an interface.
- Compiler diagnostics identify contract incompatibility.
- Change correlation identifies the modified interface.
- Developer Agent updates affected implementations.
- Harness proposes a workflow improvement for contract-change impact analysis.

### **7.6 Scenario D — Missing build configuration**

- A new background-processing project introduces build configuration requirements.
- Build fails in a controlled environment.
- Configuration evidence is captured.
- Root cause is classified.
- The applicable workflow is reviewed.
- Harness proposes a prerequisite verification step.

### **7.7 Scenario E — Environment-related failure**

- A self-hosted build worker lacks the required SDK.
- Multiple builds fail with similar diagnostics.
- Correlation reveals a shared environment fingerprint.
- The Harness distinguishes infrastructure failure from source-code failure.
- A tooling/environment remediation is recommended.
- No code-level learning is promoted.

### **7.8 Scenario F — Successful build after repeated remediation**

- Historical runs contain several attempted fixes.
- Only one remediation sequence consistently leads to successful builds.
- The Harness compares the outcomes.
- A reusable remediation candidate is produced.
- Limitations and counterexamples are documented.

### **7.9 Implementing evidence capture**

- Execute `dotnet restore`.
- Execute `dotnet build`.
- Capture machine-readable build artifacts where supported.
- Preserve diagnostic codes and messages.
- Record run and environment context.
- Persist immutable evidence references.

### **7.10 Implementing basic classification**

- Define failure categories.
- Normalize diagnostic records.
- Map known diagnostics to candidate classifications.
- Handle unknown and ambiguous errors.
- Preserve classification provenance.

### **7.11 Detecting recurring build failures**

- Normalize error signatures.
- Query historical records.
- Group equivalent failures.
- Count occurrences and affected runs.
- Avoid double-counting repeated logs from the same incident.
- Calculate recurrence indicators.

### **7.12 Generating remediation recommendations**

- Identify candidate patterns.
- Review successful and unsuccessful fixes.
- Produce evidence-backed recommendations.
- Distinguish project-specific and reusable recommendations.
- Avoid recommending unsupported fixes.

### **7.13 Recording candidates in Harness Memory**

- Store candidate pattern metadata.
- Associate supporting build evidence.
- Record confidence.
- Record applicable scope.
- Preserve unapproved status.
- Enable later re-evaluation.

### **7.14 Human approval demonstration**

- Present a proposed Skill improvement.
- Show supporting build evidence.
- Review expected benefits and risks.
- Approve or reject.
- Record reviewer identity and decision.
- Apply approved change through version control.

### **7.15 Verification after approval**

- Execute regression builds.
- Verify the proposed workflow change.
- Compare future run behavior.
- Measure repeated-failure reduction.
- Monitor for new failure patterns.
- Revert the approved change if it causes regressions.

### **7.16 Suggested companion sample artifacts**

Outline proposed additions beneath the existing `alpha-car-detailing/` and `harness/` directories:

- Build evidence model.
- Build diagnostic parser.
- Failure classification service.
- Change correlation service.
- Pattern detection service.
- Learning recommendation model.
- Memory integration adapter.
- Approval workflow.
- Synthetic failure fixtures.
- Unit and integration tests.
- Example build reports.

Keep the implementation independent of a particular AI coding provider.

### **7.17 Hands-on acceptance criteria**

- Every build outcome is associated with a run identifier.
- Raw evidence and derived analysis remain distinguishable.
- Known failures are consistently classified.
- Unknown failures do not receive fabricated classifications.
- Repeated patterns are detected without duplicate inflation.
- Recommendations contain supporting evidence.
- Governed artifacts cannot be changed without approval.
- Approved improvements are versioned and traceable.
- Future build evidence can confirm or challenge learning effectiveness.

## **8. Claude Example — Build Learning with Claude Code**

### **8.1 Role of Claude Code**

- Investigate build diagnostics.
- Analyze related source and project files.
- Propose root causes.
- Recommend code remediation.
- Produce structured learning candidates.
- Respect the Harness governance boundary.

### **8.2 Claude-first build investigation workflow**

- Receive normalized build evidence.
- Inspect relevant files.
- Identify likely failure causes.
- Compare proposed remediation with repository Instructions.
- Recommend bounded implementation changes.
- Request deterministic rebuild verification.

### **8.3 Example Prompt — Analyze Build Failure**

Define a reusable Prompt that instructs Claude to:

- Analyze the supplied diagnostic evidence.
- Identify plausible root causes.
- Cite the relevant files and diagnostics.
- Distinguish observations from hypotheses.
- Recommend minimal remediation.
- Avoid unrelated modifications.
- Return structured findings.

### **8.4 Example Prompt — Identify Recurring Build Patterns**

- Analyze multiple run records.
- Identify repeated diagnostic signatures.
- Review remediation outcomes.
- Identify potentially reusable patterns.
- Document counterexamples.
- Assign evidence-supported confidence.
- Produce recommendations without applying them.

### **8.5 Example Prompt — Recommend Skill Improvement**

- Inspect repeated failures associated with a Skill.
- Identify missing prerequisites or workflow steps.
- Draft a proposed improvement.
- Explain expected benefits.
- Include supporting evidence.
- Require human approval before modification.

### **8.6 Structured Claude output**

Proposed output fields:

- Failure category.
- Root-cause hypothesis.
- Evidence references.
- Suggested remediation.
- Confidence.
- Applicable scope.
- Recommended artifact.
- Approval requirement.

### **8.7 Claude execution safeguards**

- Read-only access during historical evidence analysis.
- Scoped write access for current-run remediation.
- No direct modification of governed guidance.
- Deterministic verification of suggested code fixes.
- Explicit review of generated recommendations.

### **8.8 Claude limitations**

- Incorrect root-cause inference.
- Overgeneralization.
- Context limitations.
- Misinterpretation of build logs.
- Unjustified confidence.
- Need for deterministic evidence and human oversight.

## **9. GitHub Copilot Comparison**

### **9.1 Build diagnostics with GitHub Copilot**

- Repository-aware assistance.
- Compiler and build error investigation.
- Suggested source-code corrections.
- Interaction with development workflows.

### **9.2 Copilot in an Adaptive Harness**

- Harness-owned evidence capture.
- External memory and recommendation services.
- Structured diagnostic exchange.
- Governance-independent integration.

### **9.3 Comparison with Claude Code**

- Differences in available integration mechanisms.
- Tool permissions and execution boundaries.
- Context provisioning.
- Structured output integration.
- Workflow automation considerations.

### **9.4 Vendor-neutral architecture**

- Treat Copilot as a replaceable analysis or implementation participant.
- Keep deterministic builds in the Harness.
- Maintain external, governed Harness Memory.
- Preserve provider-independent audit records.

## **10. Codex Comparison**

### **10.1 Build investigation with Codex**

- Repository-level source analysis.
- Build execution through available tools.
- Compiler error investigation.
- Implementation remediation.

### **10.2 Codex and structured build learning**

- Consuming build evidence.
- Returning classified findings.
- Producing remediation proposals.
- Supporting repeatable learning workflows.

### **10.3 Harness integration considerations**

- Execution isolation.
- Tool access.
- Artifact capture.
- Context management.
- Deterministic validation.
- Human approval requirements.

### **10.4 Claude, Copilot, and Codex comparison matrix**

Compare the tools on:

- Build failure diagnosis.
- Repository context.
- Tool execution.
- Structured analysis.
- Remediation assistance.
- Memory integration approach.
- Governance enforcement.
- Harness orchestration compatibility.

Distinguish native tool capabilities from functionality supplied by the surrounding Harness.

### **10.5 Provider independence**

Explain why the architecture for learning from builds must remain usable when AI coding providers or models change.

## **11. Best Practices**

### **11.1 Evidence collection**

- Preserve original build logs.
- Capture structured diagnostics.
- Include environmental context.
- Correlate evidence with exact repository revisions.
- Maintain evidence integrity.

### **11.2 Failure analysis**

- Classify before generalizing.
- Separate symptoms and root causes.
- Identify cascading diagnostics.
- Recognize transient failures.
- Preserve uncertainty.

### **11.3 Pattern discovery**

- Require sufficient independent evidence.
- Deduplicate related incidents.
- Track successful and unsuccessful remediation.
- Avoid overfitting to individual repositories.
- Apply expiration and revalidation policies.

### **11.4 Harness Memory**

- Store provenance.
- Scope learned patterns.
- Distinguish provisional and approved entries.
- Maintain confidence metadata.
- Avoid treating memory as an Instruction.

### **11.5 Governance**

- Require approval for governed changes.
- Identify artifact owners.
- Use version control.
- Establish rollback procedures.
- Preserve audit history.

### **11.6 Operational engineering**

- Make evidence processing idempotent.
- Monitor analysis quality.
- Control storage and inference costs.
- Isolate sensitive repository data.
- Keep build validation deterministic.

### **11.7 Measuring effectiveness**

- Compare equivalent workloads.
- Monitor recurring failure rates.
- Measure avoided remediation effort.
- Track recommendation acceptance and rejection.
- Detect unintended regressions.

## **12. Anti-patterns**

### **12.1 Automatically modifying Instructions after build failures**

Why it fails and how governance prevents unauthorized changes.

### **12.2 Treating every compiler error as reusable learning**

Confusing isolated implementation mistakes with recurring workflow weaknesses.

### **12.3 Learning from a single successful build**

Assuming one success proves general applicability.

### **12.4 Confusing correlation with causation**

Attributing failures to recent code changes without sufficient evidence.

### **12.5 Ignoring environmental failures**

Repeatedly changing source code to resolve infrastructure problems.

### **12.6 Promoting temporary workarounds into Skills**

Turning narrowly successful fixes into permanent procedures without review.

### **12.7 Weakening validation to achieve green builds**

Disabling checks, suppressing diagnostics, or changing success criteria without authorization.

### **12.8 Treating AI explanations as verified root causes**

Promoting plausible but unproven diagnostic interpretations.

### **12.9 Polluting Harness Memory**

Storing duplicate, contradictory, obsolete, or unscoped knowledge.

### **12.10 Unbounded remediation loops**

Continuing to regenerate code despite repeated unsuccessful attempts.

### **12.11 Cross-repository learning contamination**

Applying a pattern from one technology stack or environment to an incompatible repository.

### **12.12 Ignoring negative outcomes**

Recording successful fixes while discarding failures, regressions, or counterexamples.

### **12.13 Uncontrolled build-log ingestion**

Allowing secrets or malicious instructions contained in logs to affect future agent behavior.

### **12.14 Measuring success only by build pass rate**

Ignoring code quality, functional outcomes, security, and business acceptance.

## **13. Architect's Notes**

### **13.1 Build evidence is not engineering authority**

Explain the difference between observable facts, inferred patterns, approved knowledge, and governed standards.

### **13.2 The Harness must separate execution and learning**

Avoid allowing evidence-analysis components to redefine validation criteria or execution controls.

### **13.3 Classification accuracy matters more than classification completeness**

A correctly unresolved diagnosis is preferable to an unsupported root-cause claim.

### **13.4 Build success is a necessary but insufficient quality signal**

Explain how compilation relates to architecture, behavioral correctness, security, and business acceptance.

### **13.5 Learning quality depends on evidence provenance**

Emphasize reproducibility, source revisions, environment identity, and artifact integrity.

### **13.6 Prefer narrow and reversible improvements**

Promote scoped recommendations that can be validated and rolled back.

### **13.7 Human governance is part of the architecture**

Treat approval, ownership, versioning, and auditability as system capabilities rather than administrative afterthoughts.

### **13.8 Design for changing technology stacks**

Build patterns may become obsolete as SDKs, package versions, architectures, and tooling evolve.

### **13.9 Negative evidence is valuable**

Repeated unsuccessful remediation can identify prohibited or ineffective approaches.

### **13.10 Avoid coupling the Learning Engine to a model provider**

Keep evidence schemas, pattern storage, recommendation records, and approval workflows provider-independent.

## **14. Enterprise Tips**

### **14.1 Introduce build learning incrementally**

Start with deterministic evidence capture and classification before introducing advanced pattern discovery.

### **14.2 Establish an organization-wide diagnostic taxonomy**

Use consistent categories across supported repositories and build environments.

### **14.3 Integrate with existing CI/CD pipelines**

Reuse build outputs from GitHub Actions and Azure DevOps where appropriate.

### **14.4 Define ownership for learning recommendations**

Assign artifact owners for Skills, Prompts, Instructions, and build workflows.

### **14.5 Apply repository and tenant isolation**

Prevent unauthorized sharing of proprietary source code and diagnostic context.

### **14.6 Use risk-based approval**

Require stronger evidence and review for changes affecting security, compliance, and deterministic gates.

### **14.7 Monitor recommendation usefulness**

Track accepted, rejected, expired, and reverted recommendations.

### **14.8 Build operational dashboards**

Expose recurring failures, classification results, recommendation status, and learning effectiveness.

### **14.9 Control cost and retention**

Balance evidence detail with storage cost, inference cost, privacy, and operational value.

### **14.10 Support organizational reuse carefully**

Allow validated, appropriately scoped patterns to be reused across projects without transferring incompatible assumptions.

## **15. Decision Points**

### **15.1 What build evidence should the Harness retain?**

- Full logs.
- Structured diagnostics.
- Selected artifacts.
- Context metadata.
- Retention and confidentiality constraints.

### **15.2 When should a failure become a learning candidate?**

- First occurrence.
- Repeated occurrences.
- Confirmed remediation.
- Business-critical failures.
- Risk-adjusted thresholds.

### **15.3 Where should learning occur?**

- Inside the engineering run.
- Asynchronous post-run analysis.
- Centralized cross-run processing.
- Hybrid architecture.

### **15.4 Should the Harness use deterministic classification or AI-assisted classification?**

- Rule-based classification.
- AI-assisted classification.
- Combined approach.
- Confidence and escalation policies.

### **15.5 When should learning enter Harness Memory?**

- Immediately as provisional observation.
- After evidence validation.
- After human review.
- After repeated successful remediation.

### **15.6 What requires human approval?**

- Memory promotion to authoritative knowledge.
- Skill modifications.
- Prompt template modifications.
- Instruction changes.
- Build pipeline or deterministic gate modifications.

### **15.7 What is the correct scope of a learned pattern?**

- Run.
- Repository.
- Application.
- Team.
- Organization.

### **15.8 When should a learning candidate be rejected?**

- Insufficient evidence.
- Conflicting evidence.
- Environment-specific anomalies.
- Unsafe remediation.
- Architecture inconsistency.
- Unclear applicability.

### **15.9 How should obsolete knowledge be handled?**

- Expiration.
- Revalidation.
- Supersession.
- Archival.
- Removal from active recommendation retrieval.

### **15.10 How should learning effectiveness be measured?**

- Build reliability.
- Remediation efficiency.
- Recommendation quality.
- Governance compliance.
- Long-term improvement without quality regressions.

## **16. Exercises**

### **Exercise 38.1 — Classify Build Failures**

Given a set of Alpha Car Detailing build diagnostics:

- Classify failures.
- Identify primary and secondary errors.
- Record confidence.
- Explain unknown classifications.

**Expected outcome:** A structured failure classification report.

### **Exercise 38.2 — Implement Build Evidence Capture**

Extend the sample Harness to:

- Execute a .NET build.
- Capture diagnostics.
- Record metadata.
- Persist build evidence.
- Associate evidence with a Harness run.

**Expected outcome:** Reproducible build evidence records.

### **Exercise 38.3 — Detect Repeated Compiler Errors**

Use multiple simulated runs to:

- Normalize compiler diagnostics.
- Identify repeated signatures.
- Remove duplicate incident records.
- Calculate recurrence counts.

**Expected outcome:** A recurring failure report.

### **Exercise 38.4 — Correlate Build Errors with Code Changes**

Simulate an interface modification.

- Compare baseline and current builds.
- Identify affected files.
- Determine whether failure introduction is supported by evidence.
- Record confidence and alternative explanations.

**Expected outcome:** A change correlation report.

### **Exercise 38.5 — Analyze a Dependency Conflict**

Introduce an incompatible NuGet package version.

- Capture restore evidence.
- Identify dependency incompatibility.
- Validate remediation.
- Propose a reusable prerequisite check.

**Expected outcome:** An evidence-backed dependency recommendation.

### **Exercise 38.6 — Design Build-to-Memory Promotion**

Create a memory candidate from repeated failures.

- Define evidence requirements.
- Define confidence thresholds.
- Establish approval boundaries.
- Specify memory lifecycle states.

**Expected outcome:** A governed memory promotion design.

### **Exercise 38.7 — Generate a Skill Improvement Proposal**

Use recurring missing-reference failures to propose an improvement to a project-generation Skill.

- Document supporting evidence.
- Identify proposed changes.
- Define acceptance criteria.
- Require human approval.

**Expected outcome:** A versionable Skill improvement proposal.

### **Exercise 38.8 — Demonstrate False Learning**

Simulate intermittent build failures caused by an unavailable package feed.

- Identify the common environmental cause.
- Show why code changes are inappropriate.
- Reject an incorrect learning candidate.

**Expected outcome:** A documented false-learning prevention case.

### **Exercise 38.9 — Implement Approval and Rollback**

- Submit a learning recommendation.
- Record approval.
- Apply the change through version control.
- Simulate regression.
- Revert the change.
- Preserve audit evidence.

**Expected outcome:** A complete approval and rollback record.

### **Exercise 38.10 — Evaluate Learning Effectiveness**

Compare controlled baseline and learning-assisted runs.

- Build attempts.
- Remediation time.
- Repeated errors.
- Recommendation precision.
- Regression rates.

**Expected outcome:** A measured effectiveness assessment with stated limitations.

### **Exercise 38.11 — Architecture Challenge**

Design a multi-repository Build Learning service supporting:

- .NET services.
- Repository-specific policies.
- Shared classification.
- Isolated memory.
- Centralized reporting.
- Role-based approvals.

**Expected outcome:** An enterprise architecture proposal with trust boundaries.

## **17. Interview Questions**

### **Foundational questions**

1. What does Learning from Builds mean in the context of an Adaptive Harness?
2. How does build learning differ from ordinary build automation?
3. Why should an enterprise Harness retain build evidence?
4. What are the primary categories of build failures?
5. How does a compiler error differ from a dependency failure?
6. Why does a successful build not prove software correctness?
7. What is a recurring build failure pattern?
8. What is the difference between a diagnostic symptom and a root cause?

### **Architecture questions**

1. How would you design a Build Evidence Collector for .NET applications?
2. What information should a structured build evidence record contain?
3. How should build errors be correlated with source code changes?
4. How can the Harness distinguish environmental failures from implementation defects?
5. What responsibilities belong to the Validator Agent versus the Learning Engine?
6. How should repeated build failures be detected across engineering runs?
7. How should build evidence integrate with Harness Memory?
8. What architectural boundaries prevent learning from bypassing validation?
9. How would you design a provider-independent Build Learning subsystem?

### **Governance questions**

 1. Why must build-derived recommendations require human approval before modifying governed artifacts?
 2. How should confidence thresholds be established?
 3. How can false learning be prevented?
 4. What information must be preserved for auditability?
 5. How should approved changes be versioned and rolled back?
 6. How should conflicting memory patterns be handled?
 7. What risks arise when AI Agents learn from untrusted build logs?

### **Advanced scenario questions**

 1. A Harness repeatedly fixes the same compiler error. What architectural improvements would you investigate?
 2. Multiple repositories begin failing after a shared SDK upgrade. How would the Learning Engine distinguish a common environmental cause from independent code defects?
 3. An approved Skill change improves build pass rates but introduces architecture violations. How should the Harness respond?
 4. A successful remediation works in one repository but fails in another. How should applicability and confidence be revised?
 5. How would you measure whether build learning reduces engineering cost without reducing quality?
 6. How should an enterprise manage shared build learning across multiple teams while preserving repository-specific governance?

## **18. Chapter Summary**

### **18.1 Builds as learning evidence**

- Build outcomes provide valuable, objective engineering signals.
- Raw evidence must remain distinct from interpretation.
- Build success does not establish complete engineering correctness.

### **18.2 Failure understanding**

- Compiler, dependency, configuration, environment, and contract failures require appropriate classification.
- Root-cause investigation must account for context, uncertainty, and cascading diagnostics.
- Repeated errors can reveal reusable engineering weaknesses.

### **18.3 Learning mechanisms**

- Evidence capture.
- Diagnostic normalization.
- Change correlation.
- Pattern detection.
- Remediation evaluation.
- Memory candidate generation.
- Recommendation development.

### **18.4 Governance and safety**

- Provisional learning does not override Instructions.
- The Harness must not silently alter governed Skills, Prompts, Instructions, or validation rules.
- Human approval, versioning, auditability, and rollback remain mandatory for governed changes.

### **18.5 Enterprise implications**

- Reduced repeated remediation.
- Improved workflow reliability.
- Better build diagnostics.
- Evidence-backed engineering improvements.
- Increased operational predictability.
- Stronger knowledge reuse under governance.

### **18.6 Connection to the next chapter**

Introduce Chapter 39 — Learning from Reviews.

Explain that build evidence focuses primarily on compilation and build-system behavior, while review evidence contributes additional insights about architecture alignment, code quality, maintainability, and engineering judgment.

## **19. Further Reading**

### **19.1 Official technical documentation**

- Microsoft Learn — .NET build and MSBuild.
- Microsoft Learn — NuGet dependency management and package restore.
- Microsoft Learn — .NET compiler diagnostics.
- Microsoft Learn — Central Package Management.
- Microsoft Learn — Binary logging and build diagnostics.

### **19.2 Continuous integration and delivery**

- GitHub Actions documentation.
- Azure DevOps Pipelines documentation.
- Continuous Integration by Martin Fowler.
- *Continuous Delivery* — Jez Humble and David Farley.

### **19.3 Software architecture and engineering reliability**

- *Software Engineering at Google* — Titus Winters, Tom Manshreck, and Hyrum Wright.
- *Accelerate* — Nicole Forsgren, Jez Humble, and Gene Kim.
- *Building Evolutionary Architectures* — Neal Ford, Rebecca Parsons, and Patrick Kua.
- *Site Reliability Engineering* — Betsy Beyer and others.

### **19.4 Related handbook chapters**

- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 15 — Developer Agent.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 20 — Metrics.
- Chapter 29 — AI Governance.
- Chapter 31 — Human Approval.
- Chapter 32 — Audit Trails.
- Chapter 36 — Static vs Adaptive Harnesses.
- Chapter 37 — Harness Memory.
- Chapter 39 — Learning from Reviews.
- Chapter 41 — Skill Evolution.
- Chapter 42 — Prompt Evolution.
- Chapter 43 — Instruction Evolution.
- Chapter 44 — Knowledge Graph.
- Chapter 45 — Autonomous Improvement Engine.

### **19.5 Suggested research topics**

- Automated build failure classification.
- CI/CD failure prediction.
- Software repair and remediation analysis.
- Evidence provenance in AI systems.
- Continual learning for software engineering agents.
- Human-in-the-loop governance.
- Learning effectiveness evaluation.
- Preventing feedback-loop contamination.

## **Chapter 38 — Coverage and Editorial Checklist**

| Requirement                                 | Planned coverage                     |
| ------------------------------------------- | ------------------------------------ |
| Frozen 19-section chapter template          | All 19 sections                      |
| Alpha Car Detailing continuity              | Opening, concepts, hands-on examples |
| Compiler and dependency failures            | Sections 4, 5, 7                     |
| Configuration and environment failures      | Sections 4, 7                        |
| Repeated patterns and root causes           | Sections 4, 5, 7                     |
| Build evidence capture and correlation      | Sections 5, 7                        |
| Successful and failed build learning        | Sections 4, 7                        |
| Harness Memory integration                  | Sections 4, 5, 7                     |
| Skill, Prompt, and workflow recommendations | Sections 5, 7, 8                     |
| Confidence and evidence thresholds          | Sections 5, 15                       |
| Human approval and governance               | Sections 5, 7, 15                    |
| Versioning, rollback, and auditability      | Sections 5, 7, 12                    |
| False-learning prevention                   | Sections 11, 12, 16                  |
| Complete Harness integration                | Sections 5, 6                        |
| Five architectural diagrams                 | Section 6                            |
| Claude-first examples                       | Section 8                            |
| GitHub Copilot comparison                   | Section 9                            |
| Codex comparison                            | Section 10                           |
| Best practices and anti-patterns            | Sections 11–12                       |
| Architect's Notes and Enterprise Tips       | Sections 13–14                       |
| Decision Points                             | Section 15                           |
| Practical exercises                         | 11 exercises                         |
| Interview questions                         | 30 questions                         |
| Chapter summary and further reading         | Sections 18–19                       |

**Editorial boundary:** Chapter 38 establishes how build evidence produces actionable learning and governed recommendations. It should introduce recommendations affecting Skills, Prompts, and Instructions without duplicating the full artifact-evolution mechanisms reserved for Chapters 41–43.

**Chapter 38 outline is complete.**
