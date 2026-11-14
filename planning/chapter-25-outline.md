# **Chapter 25 — Claude Memory**

Part IV — Claude Engineering

**Project:** Enterprise AI Engineering Handbook\
**Repository:** [Enterprise-AI-Engineering-Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)\
**Target file:** `book/25-claude-memory.md`\
**Chapter status:** Detailed Outline\
**Running example:** Alpha Car Detailing

## **Chapter Objective**

Explain Claude Memory as persistent context that helps AI Agents retain useful information across interactions, reducing repeated discovery and improving continuity.

Establish clear boundaries between Claude Memory, `CLAUDE.md`, Scratchpad, Repository Intelligence, and Harness Memory.

Demonstrate how persistent memory can be incorporated into enterprise AI engineering workflows without undermining repository authority, deterministic validation, human oversight, or knowledge governance.

**Core principle:**

> Memory should retain useful long-lived context, but it must not become an uncontrolled source of truth or silently replace governed repository knowledge.

## **1. Story-Driven Opening — The Agent That Remembered the Wrong Architecture**

### **1.1 The Alpha Car Detailing Scenario**

- A development team uses Claude Code to extend the corporate fleet booking platform.
- During an earlier development session, the team decided to use synchronous REST communication between two services.
- Claude retains this architectural context.
- Several weeks later, the architecture is changed to event-driven communication.
- The repository documentation and implementation now reflect the new architecture.
- Claude encounters the earlier memory during a new development task.
- The conflict between remembered context and current repository evidence creates a risk of incorrect implementation.

### **1.2 The Engineering Problem**

- Persistent memory improves continuity but can preserve outdated assumptions.
- A previously correct decision may no longer be valid.
- Remembered information may be incomplete, ambiguous, or scoped to another task.
- An AI Agent must not treat memory as automatically authoritative.

### **1.3 The Architectural Lesson**

- Memory supports continuity.
- Repository evidence establishes current implementation reality.
- Governed instructions establish approved engineering requirements.
- Conflicts require investigation rather than silent resolution.
- Human approval remains necessary for governed changes.

### **1.4 Chapter Transition**

- Why AI Agents need persistent context.
- Why persistent memory introduces governance challenges.
- How to design safe memory boundaries in enterprise environments.

## **2. Learning Objectives**

After completing this chapter, readers should be able to:

1. Explain Claude Memory and the purpose of persistent context.
2. Distinguish product-supported memory capabilities from explicitly managed project memory files.
3. Differentiate Memory, `CLAUDE.md`, Scratchpad, Repository Intelligence, and Harness Memory.
4. Identify information suitable for long-term retention.
5. Define memory scope, ownership, freshness, and retention rules.
6. Detect stale or conflicting memory.
7. Establish an evidence-based conflict resolution process.
8. Integrate persistent memory into Harness workflows.
9. Protect sensitive information and enforce access boundaries.
10. Promote valuable memory into governed repository knowledge through human approval.
11. Implement a practical memory management workflow using Alpha Car Detailing.

## **3. Background — Why Persistent Memory Matters**

### **3.1 The Stateless Interaction Problem**

- AI Agents often operate within bounded sessions or context windows.
- Context may be lost when sessions end, reset, or are compacted.
- Repeated repository discovery consumes time and tokens.
- Developers repeatedly explain preferences, decisions, and project history.

### **3.2 From Session Context to Persistent Context**

- Active conversation context.
- Temporary task state.
- Persistent remembered context.
- Version-controlled repository knowledge.
- Organizational knowledge.

### **3.3 Benefits of Persistent Memory**

- Reduced repeated explanations.
- Improved continuity across development sessions.
- Retention of useful developer preferences.
- Faster project orientation.
- Consistency in recurring workflows.
- Better handoffs between development sessions.

### **3.4 Limitations of Memory**

- Memory may be incomplete.
- Memory may become stale.
- Memory may lack supporting evidence.
- Memory may reflect a previous environment or branch.
- Memory does not guarantee correctness.
- Remembering information is different from verifying information.

### **3.5 The Enterprise Memory Challenge**

- Multiple developers.
- Multiple repositories.
- Parallel branches and worktrees.
- Multiple AI Agents.
- Changing architecture.
- Security and compliance requirements.
- Shared versus individual knowledge.

## **4. Understanding Claude Memory**

### **4.1 What Claude Memory Is**

- Persistent context used to support continuity across interactions.
- Distinction between remembered facts, preferences, summaries, and explicit instructions.
- Memory as supporting context rather than an independent decision authority.

### **4.2 Claude Code Memory Mechanisms**

- Product-supported memory capabilities.
- Explicitly maintained memory files.
- Relationship between Claude Code memory facilities and `CLAUDE.md`.
- User-level, project-level, and other supported scopes.
- Differences between automatically maintained and manually curated context, where supported.
- Product version and configuration dependencies.

### **4.3 Memory Versus Context Window**

- Active context is not equivalent to persistent memory.
- Context window limitations.
- Session continuation.
- Context compaction.
- Persistence across sessions.
- Retrieval of previously retained information.

### **4.4 Memory Versus Conversation History**

- Historical transcripts versus summarized knowledge.
- Why complete transcripts should not automatically become memory.
- Selecting durable information.
- Loss of detail during summarization.
- Source traceability.

### **4.5 Memory Is Not an Instruction System**

- Remembered preferences versus mandatory instructions.
- Memory cannot override governed engineering requirements.
- Memory cannot authorize restricted operations.
- Memory must not bypass validation gates.

### **4.6 Platform Capabilities and Architectural Abstractions**

- Distinguish native Claude features from custom Harness-managed memory.
- Avoid presenting proposed memory architecture as built-in Claude functionality.
- Verify supported file locations, precedence, persistence, and management behavior against the relevant Claude Code version.
- Preserve vendor-neutral memory principles.

## **5. Memory Versus Other Knowledge Layers**

### **5.1 Memory Versus&#x20;**`CLAUDE.md`

- Purpose of `CLAUDE.md` as a persistent instruction mechanism.
- Purpose of Memory as retained context.
- Explicit instructions versus remembered information.
- Version control and governance.
- Instruction scope and precedence.
- Why architectural standards belong in governed instructions or documentation rather than uncontrolled memory.

### **5.2 Memory Versus Scratchpad**

- Scratchpad as temporary, run-scoped working state.
- Memory as selectively retained long-lived context.
- Observations, assumptions, and intermediate decisions.
- Checkpoints and task progress.
- Cleanup and expiration.
- Criteria for promoting Scratchpad findings into persistent knowledge.

### **5.3 Memory Versus Repository Intelligence**

- Repository Intelligence as evidence-based understanding of the codebase.
- Repository discovery and analysis.
- Source code, configuration, tests, and documentation.
- Memory as a shortcut to potentially relevant context.
- Why remembered architecture must be checked against current repository evidence.

### **5.4 Memory Versus Harness Memory**

- Claude Memory supports agent continuity.
- Harness Memory records reusable execution outcomes and operational learning.
- Build history.
- Validation failures.
- Reviewer feedback.
- Evaluation outcomes.
- Successful and unsuccessful implementation patterns.
- Harness Memory ownership, provenance, and governance.
- Why Claude Memory must not silently update Harness Memory.

### **5.5 Memory Versus Steering Notes**

- Steering Notes communicate current mission priorities and constraints.
- Memory may retain historical context.
- Steering Notes must not be silently replaced by remembered goals.
- Human ownership and explicit updates.

### **5.6 Memory Versus Skills and Prompts**

- Skills define reusable workflows.
- Prompts define current tasks.
- Memory supplies contextual information.
- Memory must not silently redefine a Skill or alter a Prompt's acceptance criteria.

### **5.7 Knowledge Layer Comparison Table**

Include a comparison covering:

| Knowledge layer         | Primary purpose                         | Typical lifetime                |
| ----------------------- | --------------------------------------- | ------------------------------- |
| Prompt                  | Current task request                    | Task                            |
| Scratchpad              | Temporary working state                 | Run                             |
| Claude Memory           | Persistent contextual continuity        | Across sessions                 |
| `CLAUDE.md`             | Governed agent instructions             | Until revised                   |
| Repository Intelligence | Evidence-based repository understanding | Refreshed as repository changes |
| Steering Note           | Current mission direction               | Until updated                   |
| Harness Memory          | Reusable execution learning             | Long-lived, policy-controlled   |

### **5.8 Diagram 1 — Memory Versus Other Knowledge Layers**

**Diagram type:** Layered architecture.

Show the relationship among:

- User and Prompt.
- `CLAUDE.md` and Steering Notes.
- Claude Memory.
- Scratchpad.
- Repository Intelligence.
- Harness Memory.
- Repository evidence and deterministic gates.

Clearly distinguish context sources, instruction sources, evidence sources, and approval authority.

## **6. Memory Architecture and Scope**

### **6.1 Memory Scope**

- Individual developer context.
- Project-specific context.
- Repository-specific context.
- Team-shared context, where explicitly supported.
- Organization-level knowledge.
- Task-specific context that should remain temporary.

### **6.2 Memory Isolation**

- Separation between clients and repositories.
- Separation between development and production environments.
- Avoiding cross-project information leakage.
- Identity and access boundaries.
- Multi-tenant considerations.

### **6.3 Memory Ownership**

- User-owned preferences.
- Project-maintained context.
- Team-governed knowledge.
- Harness-managed operational history.
- Ownership of corrections and deletion requests.

### **6.4 Memory Storage and Retrieval**

- Native platform-supported persistence.
- Explicit project memory files.
- Optional custom memory stores for Harness integrations.
- Retrieval by scope and relevance.
- Context selection and token budgeting.
- Provenance and timestamps.

### **6.5 Memory Record Design**

For a custom enterprise memory layer, introduce an illustrative record containing:

- Memory identifier.
- Scope and owner.
- Content summary.
- Category.
- Source reference.
- Creation and verification timestamps.
- Repository and branch applicability.
- Status: active, stale, disputed, or retired.
- Sensitivity classification.
- Review or expiration policy.

Explain that this is a proposed Harness design, not a guaranteed native Claude Memory schema.

### **6.6 Memory Isolation in Parallel Development**

- Multiple Claude sessions.
- Concurrent branches.
- Worktrees.
- Shared repository changes.
- Avoiding cross-run contamination.
- Synchronization and conflict handling.

## **7. What Should Be Remembered**

### **7.1 User Preferences**

- Preferred explanation depth.
- Coding style preferences that do not conflict with repository standards.
- Preferred documentation format.
- Preferred development workflow.
- Stable tool preferences.

### **7.2 Project Context**

- High-level project purpose.
- Major business domains.
- Technology stack.
- Service responsibilities.
- Stable terminology.
- Links to authoritative documentation.

### **7.3 Long-Lived Decisions**

- Important historical architecture decisions.
- Reasons behind previously approved approaches.
- Known tradeoffs.
- References to Architecture Decision Records (ADRs).
- Decision status and applicable versions.

### **7.4 Recurring Development Context**

- Frequently used commands.
- Established development environment characteristics.
- Common navigation references.
- Stable workflow conventions.

### **7.5 Alpha Car Detailing Examples**

- The platform serves corporate fleet customers.
- The solution uses .NET and Clean Architecture.
- Booking and fleet management are separate business capabilities.
- Architecture decisions are recorded in repository documentation.
- Current service contracts must be verified from source-controlled evidence.

### **7.6 Memory Selection Criteria**

- Is the information useful beyond the current session?
- Is it sufficiently stable?
- Is its source identifiable?
- Is it permitted to retain?
- Does it already belong in governed documentation?
- Does it have a clear owner and review policy?

## **8. What Should Not Be Remembered**

### **8.1 Sensitive Information**

- Passwords.
- API keys.
- Access tokens.
- Private keys.
- Connection strings containing credentials.
- Personal or regulated data without an approved basis.

### **8.2 Temporary Execution Details**

- Current build errors.
- Temporary file paths.
- Intermediate implementation assumptions.
- Unverified debugging observations.
- Short-lived task progress.
- Run-specific state better suited to the Scratchpad.

### **8.3 Unverified Claims**

- Agent-generated assumptions.
- Speculative architecture descriptions.
- Unsupported conclusions.
- Unreviewed code analysis.
- Outdated third-party documentation.

### **8.4 Governed Information That Belongs Elsewhere**

- Security policies.
- Mandatory coding standards.
- Acceptance criteria.
- Approved architecture constraints.
- Compliance requirements.
- Production operational procedures.

### **8.5 Memory Contamination**

- Prompt injection disguised as project knowledge.
- Malicious instructions in retrieved content.
- Cross-client information.
- Incorrect assumptions promoted into durable memory.

### **8.6 Alpha Car Detailing Examples**

- Temporary fleet booking test data.
- Customer payment information.
- Production database credentials.
- Unapproved service redesign suggestions.
- Debugging notes from a failed Harness run.

## **9. Memory Lifecycle**

### **9.1 Memory Creation**

- Identifying a potentially reusable fact.
- Determining scope and category.
- Assessing sensitivity.
- Capturing provenance.
- Selecting appropriate retention.

### **9.2 Memory Validation**

- Confirming facts against available evidence.
- Distinguishing user preferences from technical facts.
- Identifying unverified assumptions.
- Determining whether human review is required.

### **9.3 Memory Storage**

- Persisting only approved or permitted information.
- Avoiding unnecessary duplication.
- Recording applicable scope.
- Assigning review or expiration conditions.

### **9.4 Memory Retrieval**

- Selecting relevant context.
- Checking scope.
- Checking freshness.
- Confirming current applicability.
- Preventing unrelated memory from influencing execution.

### **9.5 Memory Updates**

- Revising changed information.
- Replacing outdated summaries.
- Retaining appropriate provenance.
- Avoiding contradictory active entries.
- Recording corrections in governed stores where applicable.

### **9.6 Memory Retirement**

- Detecting obsolete entries.
- Marking stale or disputed information.
- Deleting or archiving under policy.
- Respecting privacy and retention requirements.

### **9.7 Diagram 2 — Memory Lifecycle**

**Diagram type:** Lifecycle flowchart.

```
Observe → Classify → Verify → Retain
                            ↓
                          Retrieve
                            ↓
                       Check Freshness
                            ↓
                   Reuse / Update / Retire
```

Include security screening, ownership, and human approval where required.

## **10. Temporary Versus Persistent State**

### **10.1 Temporary State**

- Scratchpad observations.
- Current task assumptions.
- Work-in-progress implementation notes.
- Agent checkpoints.
- Pending questions.
- Temporary validation results.

### **10.2 Persistent State**

- Stable developer preferences.
- Durable project context.
- References to approved decisions.
- Recurring workflow context.

### **10.3 Governed Knowledge**

- Architecture Decision Records.
- `CLAUDE.md`.
- Repository documentation.
- Approved Skills.
- Engineering standards.
- Version-controlled contracts.

### **10.4 Promotion Criteria**

- Relevance beyond the current run.
- Evidence quality.
- Long-term usefulness.
- Ownership.
- Security classification.
- Human approval when required.

### **10.5 Diagram 3 — Temporary Versus Persistent State**

**Diagram type:** State promotion diagram.

Show the progression:

Scratchpad → Candidate Memory → Verified Persistent Context → Proposed Governed Knowledge → Human Approval → Repository Update.

Emphasize that promotion is selective and that not every observation should be retained.

## **11. Memory Freshness and Stale Memory**

### **11.1 Why Memory Becomes Stale**

- Architecture evolution.
- Service refactoring.
- Dependency upgrades.
- Configuration changes.
- Repository restructuring.
- Branch divergence.
- Changes in business requirements.

### **11.2 Detecting Stale Memory**

- Repository evidence conflicts.
- Referenced files no longer exist.
- Superseded ADRs.
- Changed API contracts.
- Changed service responsibilities.
- Expired verification dates.

### **11.3 Freshness Policies**

- Review intervals.
- Event-driven invalidation.
- Repository change triggers.
- Version and commit references.
- Time-to-live policies for appropriate categories.
- Revalidation before consequential use.

### **11.4 Handling Stale Memory**

- Do not silently reuse.
- Identify the conflicting evidence.
- Revalidate the remembered fact.
- Update or retire outdated context.
- Escalate unresolved decisions.

### **11.5 Alpha Car Detailing Example**

- Earlier memory states that Fleet Booking calls Billing synchronously.
- Current code and approved ADR indicate event-driven communication.
- Claude identifies the conflict.
- The outdated memory is excluded from implementation planning.
- The memory is corrected or retired through the supported mechanism.

## **12. Memory Conflicts and Repository Evidence**

### **12.1 Understanding Conflicts**

- Memory versus source code.
- Memory versus `CLAUDE.md`.
- Memory versus current Steering Notes.
- Memory versus ADRs.
- Memory versus API contracts.
- Memory versus test results.

### **12.2 Evidence and Authority**

- User instructions within their authorized scope.
- Applicable governed instructions and policies.
- Approved architecture documentation.
- Current repository implementation.
- Deterministic test evidence.
- Remembered historical context.

Explain why no single universal precedence list can resolve every conflict: repository implementation describes current behavior, while approved specifications may define intended behavior.

### **12.3 Conflict Resolution Workflow**

1. Detect disagreement.
2. Identify affected scope.
3. Locate authoritative evidence.
4. Compare intended and implemented behavior.
5. Determine whether memory is outdated or the repository is inconsistent.
6. Escalate unresolved architectural decisions.
7. Update memory only after appropriate verification.
8. Record the decision and supporting evidence.

### **12.4 Conflicts Between Memories**

- Duplicate entries.
- Contradictory historical decisions.
- Different scope applicability.
- Missing timestamps.
- Unclear ownership.

### **12.5 Diagram 4 — Memory Conflict Resolution**

**Diagram type:** Decision flowchart.

Include:

Memory retrieved → Scope/freshness check → Conflict detected? → Evidence investigation → Resolve or escalate → Continue with verified context → Update or retire memory.

### **12.6 Non-Negotiable Rule**

Memory must never independently override security policy, approved instructions, deterministic validation results, or required human approval.

## **13. Security, Privacy, and Governance**

### **13.1 Memory as a Security Boundary**

- Persistent context can expose sensitive information.
- Memory can influence future Agent behavior.
- Retained content may outlive its original purpose.

### **13.2 Data Classification**

- Public.
- Internal.
- Confidential.
- Restricted.

### **13.3 Access Control**

- User-level access.
- Project-level boundaries.
- Team-shared access.
- Least privilege.
- Cross-client isolation.

### **13.4 Prompt Injection and Memory Poisoning**

- Untrusted source content.
- Malicious remembered instructions.
- Incorrect knowledge promoted through repeated exposure.
- Trust boundaries during retrieval and persistence.

### **13.5 Data Minimization and Retention**

- Retain only useful information.
- Avoid unnecessary personal information.
- Define retention and deletion procedures.
- Respect organizational data policies.
- Consider applicable privacy and regulatory requirements.

### **13.6 Auditability**

- Memory provenance.
- Update history where supported.
- Ownership.
- Review decisions.
- Promotion history.
- Incident investigation.

### **13.7 Human Control**

- Ability to inspect and correct remembered context where supported.
- Approval of changes to governed knowledge.
- Explicit review of consequential architecture changes.
- Prevention of unauthorized policy changes.

### **13.8 Enterprise Governance Model**

- Memory policy owner.
- Engineering team responsibilities.
- Security review responsibilities.
- Harness operational responsibilities.
- Periodic review process.

## **14. Memory Inside the Harness**

### **14.1 Why the Harness Needs Memory Awareness**

- Repeated AI Agent execution.
- Multi-step development workflows.
- Multiple Agent roles.
- Reusable execution context.
- Continuity between runs.

### **14.2 Harness Memory Integration Architecture**

- Prompt and goal intake.
- Governed instruction loading.
- Repository discovery.
- Relevant memory retrieval.
- Scratchpad initialization.
- Agent execution.
- Deterministic validation.
- Evaluation.
- Memory candidate extraction.
- Controlled persistence.

### **14.3 Lead Agent Responsibilities**

- Determine relevant context.
- Establish task scope.
- Identify memory conflicts.
- Prevent unverified assumptions from entering the plan.
- Coordinate escalation.

### **14.4 Developer Agent Responsibilities**

- Use verified context during implementation.
- Record temporary findings in the Scratchpad.
- Avoid turning assumptions into durable memory.
- Report newly discovered repository facts.

### **14.5 Reviewer Agent Responsibilities**

- Detect memory-driven architectural inconsistencies.
- Identify unsupported assumptions.
- Verify alignment with repository conventions.
- Recommend correction where appropriate.

### **14.6 Validator Agent Responsibilities**

- Execute deterministic checks.
- Produce objective evidence.
- Identify implementation failures.
- Avoid treating remembered success as proof of current correctness.

### **14.7 Evaluator Agent Responsibilities**

- Assess goal satisfaction.
- Evaluate business outcomes.
- Consume Reviewer and Validator evidence.
- Identify gaps between intended and actual outcomes.
- Avoid treating remembered acceptance as current acceptance.

### **14.8 Harness Memory Versus Claude Memory**

- Claude Memory retains contextual continuity.
- Harness Memory retains structured execution learning.
- Separate ownership and persistence mechanisms.
- Controlled information exchange.
- No automatic promotion of Agent conclusions into organizational knowledge.

### **14.9 Diagram 5 — Memory Inside the Harness**

**Diagram type:** Component architecture.

Show:

- User / Goal.
- Harness Orchestrator.
- `CLAUDE.md` / Steering Notes.
- Repository Intelligence.
- Claude Memory.
- Scratchpad / Run Workspace.
- Lead, Developer, Reviewer, Validator, Evaluator.
- Deterministic Gates.
- Harness Memory.
- Human Approval.
- Governed Repository Knowledge.

Use arrows to distinguish context retrieval, execution evidence, proposed memory updates, and approved knowledge promotion.

## **15. Promotion to Governed Knowledge**

### **15.1 Why Promotion Is Necessary**

- Useful discoveries should not remain hidden in private Agent memory.
- Teams require shared and reviewable knowledge.
- Important decisions need traceability.
- Institutional knowledge should survive changes in tools and personnel.

### **15.2 Promotion Destinations**

- `CLAUDE.md` for appropriate persistent instructions.
- ADRs for architecture decisions.
- Repository documentation for verified technical facts.
- Skills for reusable implementation workflows.
- Steering Notes for approved mission changes.
- Harness Memory for structured execution learning.

### **15.3 Promotion Workflow**

1. Identify reusable information.
2. Classify the information.
3. Verify evidence.
4. Select the authoritative destination.
5. Prepare a proposed change.
6. Request appropriate review.
7. Obtain human approval where required.
8. Commit the governed update.
9. Correct or retire redundant memory.
10. Preserve traceability.

### **15.4 Preventing Silent Knowledge Mutation**

- No automatic changes to engineering standards.
- No unauthorized modification of `CLAUDE.md`.
- No automatic approval of architecture decisions.
- No bypass of repository review.
- No hidden evolution of Skills or policies.

### **15.5 Alpha Car Detailing Promotion Example**

- A recurring integration issue is discovered.
- The Developer records the finding.
- The Reviewer confirms its significance.
- The Validator supplies deterministic evidence.
- The Lead proposes a reusable engineering practice.
- The team reviews the proposed Skill or documentation update.
- Approved knowledge becomes version-controlled.

## **16. Hands-on Example — Managing Claude Memory for Alpha Car Detailing**

### **16.1 Scenario**

Implement a new corporate fleet booking capability using Claude Code while maintaining continuity across sessions.

### **16.2 Prerequisites**

- Alpha Car Detailing repository.
- Claude Code installation and authentication.
- Existing `CLAUDE.md`.
- Repository documentation.
- A sample architecture decision.
- Harness run workspace.

### **16.3 Step 1 — Inspect Existing Context**

- Identify relevant project instructions.
- Inspect supported memory locations or interfaces.
- Review remembered project context.
- Confirm repository scope.
- Identify sensitive or outdated information.

### **16.4 Step 2 — Classify Information**

- User preference.
- Project context.
- Governed instruction.
- Temporary observation.
- Architecture decision.
- Execution learning.

### **16.5 Step 3 — Establish Appropriate Memory**

- Retain a stable preference.
- Retain a concise project description.
- Reference approved architectural documentation.
- Avoid duplicating complete repository standards.
- Include provenance where the mechanism permits.

### **16.6 Step 4 — Execute a Development Task**

- Introduce a fleet booking requirement.
- Start a new Claude session.
- Retrieve relevant context.
- Verify current repository implementation.
- Develop the feature using the established Harness workflow.

### **16.7 Step 5 — Introduce a Stale Memory Scenario**

- Supply an outdated architectural assumption.
- Compare it with current repository evidence.
- Detect the disagreement.
- Prevent the stale context from directing implementation.

### **16.8 Step 6 — Correct the Memory**

- Determine the authoritative evidence.
- Update or retire the outdated entry through supported controls.
- Confirm the corrected context.
- Record any required documentation change separately.

### **16.9 Step 7 — Validate the Implementation**

- Run build and tests.
- Execute architecture checks.
- Review implementation quality.
- Evaluate acceptance criteria.
- Confirm that memory was not treated as validation evidence.

### **16.10 Expected Artifacts**

- Memory classification examples.
- Sample non-sensitive memory entries.
- Conflict investigation record.
- Proposed governed knowledge update.
- Harness execution evidence.
- Review and validation results.

## **17. Claude Example**

### **17.1 Claude Code Memory Workflow**

- Discover supported memory features.
- Inspect current memory.
- Understand memory scope.
- Add or maintain useful context through supported mechanisms.
- Review and correct outdated context.

### **17.2 Claude Memory and&#x20;**`CLAUDE.md`

- Practical separation of context and instructions.
- Avoiding unnecessary duplication.
- Maintaining clear ownership.
- Verifying actual platform behavior.

### **17.3 Multi-Session Development**

- Session A: initial repository discovery.
- Session B: continued feature development.
- Session C: architecture change.
- Session D: stale memory correction.

### **17.4 Claude Code in the Harness**

- Context initialization.
- Memory-aware planning.
- Scratchpad use.
- Evidence verification.
- Controlled persistence.

### **17.5 Operational Limitations**

- Version-specific capabilities.
- Availability and configuration differences.
- Context selection behavior.
- Retrieval limitations.
- Memory correction and deletion controls.
- Security considerations.

## **18. GitHub Copilot Comparison**

### **18.1 Context and Persistence**

- Supported personalization and contextual mechanisms.
- Repository instruction files.
- Workspace context.
- Session continuity.

### **18.2 Comparison with Claude**

- Memory mechanisms.
- Instruction mechanisms.
- Repository grounding.
- User versus project scope.
- Enterprise administration.

### **18.3 Harness Portability**

- Abstracting memory retrieval.
- Keeping governed knowledge independent of vendor.
- Avoiding assumptions about identical persistence behavior.

### **18.4 Enterprise Considerations**

- Product and licensing differences.
- Privacy.
- Governance.
- Verification of currently supported capabilities.

## **19. Codex Comparison**

### **19.1 Codex Context Model**

- Session context.
- Repository instructions.
- `AGENTS.md`.
- Supported persistence and continuation mechanisms.

### **19.2 Claude Versus Codex**

- Persistent context.
- Explicit instruction files.
- Repository discovery.
- Scope.
- Context lifecycle.
- Governance.

### **19.3 Vendor-Neutral Memory Design**

- Common memory classification.
- Portable knowledge records.
- Repository-grounded verification.
- Independent Harness Memory.
- Human-controlled promotion.

### **19.4 Portability Limitations**

- Avoid assuming feature parity.
- Distinguish native capabilities from custom integrations.
- Validate product behavior before implementation.

## **20. Best Practices**

### **20.1 Keep Memory Small and Relevant**

- Prefer concise context.
- Avoid storing entire conversations.
- Remove redundant information.

### **20.2 Separate Context from Authority**

- Memory informs.
- Governed instructions constrain.
- Repository evidence verifies.
- Deterministic gates validate.
- Humans approve consequential changes.

### **20.3 Record Provenance**

- Link to source evidence.
- Identify applicable scope.
- Capture verification status.

### **20.4 Revalidate Important Facts**

- Check current repository state.
- Detect stale assumptions.
- Avoid blindly trusting remembered architecture.

### **20.5 Enforce Security**

- Minimize sensitive data.
- Apply scope boundaries.
- Protect against memory poisoning.
- Follow retention requirements.

### **20.6 Establish Lifecycle Management**

- Review.
- Update.
- Retire.
- Promote.
- Audit where supported.

### **20.7 Preserve Human Control**

- Review important memory corrections.
- Require approval for governed changes.
- Keep standards version-controlled.

## **21. Anti-patterns**

### **21.1 Memory as the Source of Truth**

Treating remembered context as authoritative without verification.

### **21.2 Memory as a Replacement for&#x20;**`CLAUDE.md`

Storing mandatory engineering instructions as informal remembered preferences.

### **21.3 Scratchpad Pollution**

Persisting temporary observations and debugging details unnecessarily.

### **21.4 Memory Hoarding**

Retaining excessive information without relevance or expiration policies.

### **21.5 Stale Architecture Memory**

Continuing to use superseded architectural decisions.

### **21.6 Silent Memory Mutation**

Changing retained context without appropriate visibility or control.

### **21.7 Sensitive Data Persistence**

Retaining credentials, regulated data, or confidential information without authorization.

### **21.8 Cross-Project Contamination**

Using knowledge from one client or repository in another.

### **21.9 Memory-Driven Validation Bypass**

Assuming a previously successful build or test guarantees current correctness.

### **21.10 Automatic Promotion Without Review**

Allowing Agent conclusions to become organizational standards without governance.

For each anti-pattern, include symptoms, consequences, an Alpha Car Detailing example, and the recommended corrective approach.

## **22. Architect’s Notes**

### **22.1 Memory Is an Optimization, Not an Authority**

Persistent context improves efficiency but does not establish correctness.

### **22.2 Design for Memory Failure**

- Missing memory.
- Corrupted or incomplete memory.
- Conflicting memory.
- Stale memory.
- Unavailable memory services.

### **22.3 Prefer Evidence Over Recollection**

- Repository-grounded analysis.
- Source-controlled documentation.
- Deterministic validation.
- Explicit decision records.

### **22.4 Separate Personalization from Organizational Knowledge**

- Individual preferences.
- Team conventions.
- Approved architecture.
- Enterprise policy.

### **22.5 Make Knowledge Portable**

- Avoid vendor lock-in.
- Keep critical decisions in repositories.
- Use portable documentation formats.
- Treat vendor-specific memory as supplementary.

### **22.6 Design Memory as a Governed Capability**

- Scope.
- Ownership.
- Lifecycle.
- Security.
- Observability.
- Human oversight.

## **23. Enterprise Tips**

### **23.1 Define a Memory Policy**

Establish what may be retained, by whom, and for how long.

### **23.2 Use Clear Ownership**

Separate developer-controlled context from team-governed knowledge.

### **23.3 Integrate with Repository Change Management**

Revalidate affected memories when architecture or contracts change.

### **23.4 Prevent Cross-Client Leakage**

Apply strict isolation across enterprise projects.

### **23.5 Monitor Memory Quality**

For custom Harness integrations, consider:

- Stale memory rate.
- Conflict detection rate.
- Correction frequency.
- Memory retrieval relevance.
- Promotion approval rate.
- Memory-related implementation defects.

### **23.6 Establish Incident Procedures**

Handle sensitive memory exposure, incorrect persistence, and memory poisoning.

### **23.7 Keep Humans in Control**

Require explicit approval before memory-derived recommendations change governed engineering knowledge.

## **24. Decision Points**

### **24.1 Should This Information Be Remembered?**

- Long-lived or temporary?
- Verified or speculative?
- Sensitive or permitted?
- Useful beyond the current task?

### **24.2 Where Should It Be Stored?**

- Claude Memory.
- Scratchpad.
- `CLAUDE.md`.
- Repository documentation.
- ADR.
- Harness Memory.

### **24.3 Who Owns the Information?**

- Developer.
- Team Lead.
- Architect.
- Security team.
- Harness operator.

### **24.4 When Should Memory Be Revalidated?**

- On retrieval.
- After relevant repository changes.
- Before consequential implementation.
- During periodic review.

### **24.5 When Is Human Approval Required?**

- Architecture changes.
- Security requirements.
- Engineering standards.
- Shared organizational knowledge.
- Retention of sensitive context.

### **24.6 Should Memory Be Updated or Retired?**

- Current relevance.
- Evidence quality.
- Duplicate information.
- Superseded decisions.
- Retention requirements.

Include a decision matrix linking memory category, recommended storage destination, validation requirements, and approval ownership.

## **25. Exercises**

### **Exercise 1 — Classify Knowledge Sources**

Classify sample Alpha Car Detailing information into Memory, `CLAUDE.md`, Scratchpad, Repository Intelligence, Steering Notes, and Harness Memory.

### **Exercise 2 — Design a Memory Policy**

Define:

- Permitted categories.
- Prohibited categories.
- Ownership.
- Freshness rules.
- Retention.
- Approval requirements.

### **Exercise 3 — Implement a Claude Memory Workflow**

Use the supported Claude Code memory mechanisms to retain useful, non-sensitive project context and verify continuity across sessions.

### **Exercise 4 — Detect Stale Memory**

Introduce an outdated service communication assumption and resolve it against repository evidence.

### **Exercise 5 — Promote Knowledge**

Take a verified recurring engineering finding and prepare a proposed update to a governed repository artifact.

### **Exercise 6 — Design Harness Memory Integration**

Design the memory retrieval and persistence boundaries for the Lead, Developer, Reviewer, Validator, and Evaluator Agents.

### **Exercise 7 — Security Review**

Identify sensitive information, cross-project contamination, and prompt-injection risks in a sample memory dataset.

### **Exercise 8 — Failure Recovery**

Design Harness behavior when persistent memory is missing, unavailable, or contradictory.

### **Exercise 9 — Multi-Agent Scenario**

Run parallel development tasks and define how task-local observations remain isolated while approved knowledge is shared.

### **Exercise 10 — Architecture Challenge**

Design a vendor-neutral persistent context interface that supports Claude Code, GitHub Copilot, and Codex without assuming identical native memory capabilities.

## **26. Interview Questions**

### **Foundational**

1. What is Claude Memory, and why is persistent context useful?
2. How does persistent memory differ from an active context window?
3. What is the difference between Memory and `CLAUDE.md`?
4. How does Memory differ from a Scratchpad?
5. What distinguishes Repository Intelligence from remembered context?

### **Architecture**

1. How would you design memory scopes for a multi-repository enterprise?
2. How should an AI Agent resolve conflicts between memory and current repository evidence?
3. Why should memory not become an authoritative source of engineering standards?
4. How would you detect stale architecture memory?
5. What metadata would you include in a custom Harness memory record?

### **Harness Engineering**

 1. How should the Lead Agent consume memory?
 2. What is the difference between Claude Memory and Harness Memory?
 3. How should Reviewer and Validator evidence influence memory updates?
 4. How would you prevent temporary Scratchpad observations from contaminating persistent memory?
 5. How should the Harness behave when memory is unavailable?

### **Governance and Security**

 1. What information should never be retained in persistent memory?
 2. How can memory poisoning affect AI-assisted development?
 3. How would you prevent cross-client memory leakage?
 4. When should remembered knowledge be promoted into repository documentation?
 5. Why is human approval important for memory-driven engineering changes?

### **Advanced**

 1. How would you design a vendor-neutral memory abstraction?
 2. How would you measure memory quality and usefulness?
 3. How would you manage conflicting memories across parallel branches?
 4. What is the relationship between memory freshness and repository versioning?
 5. How would you prevent a self-learning Harness from silently modifying engineering standards?

## **27. Chapter Summary**

### **27.1 Key Concepts**

- Persistent context improves continuity across development sessions.
- Claude Memory and `CLAUDE.md` serve different purposes.
- Scratchpad state should remain temporary unless deliberately promoted.
- Repository Intelligence provides evidence-based understanding.
- Harness Memory represents a separate organizational learning capability.
- Memory must be scoped, verified, maintained, and governed.

### **27.2 Architectural Principles**

- Memory is not the source of truth.
- Memory does not replace repository discovery.
- Memory does not override governed instructions.
- Memory does not replace deterministic validation.
- Memory does not authorize architectural changes.
- Persistent context requires freshness and security controls.
- Governed knowledge changes require appropriate review and approval.

### **27.3 Alpha Car Detailing Takeaways**

- Use memory to preserve useful development context.
- Verify architectural assumptions against the current repository.
- Keep temporary execution state in the Scratchpad.
- Keep approved engineering decisions in governed artifacts.
- Integrate memory into the Harness without weakening existing quality gates.

### **27.4 Transition to Chapter 26 — Tool Calling**

- Memory provides useful context.
- Tool calling enables AI Agents to interact with external systems.
- Tools introduce additional authority, permission, security, and execution boundaries.
- The next chapter explores how Claude uses tools within controlled enterprise workflows.

## **28. Further Reading**

Include authoritative and maintained references covering:

### **28.1 Claude Documentation**

- Claude Code memory and instruction mechanisms.
- Claude Code project configuration.
- Claude Code context management.
- Claude Code security and permissions.

### **28.2 Related AI Coding Platforms**

- GitHub Copilot customization and repository instructions.
- OpenAI Codex repository instruction mechanisms.
- Supported session and context management capabilities.

### **28.3 Enterprise Engineering References**

- Architecture Decision Records.
- Knowledge management.
- Data classification and retention.
- Secure AI system design.
- Prompt injection defenses.
- Human-in-the-loop governance.

### **28.4 Related Handbook Chapters**

- Chapter 5 — Repository Discovery.
- Chapter 6 — Repository Analysis.
- Chapter 7 — Skills.
- Chapter 10 — Steering Notes.
- Chapter 11 — Knowledge Sources.
- Chapter 13 — Harness Architecture.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 22 — `CLAUDE.md`.
- Chapter 23 — Claude Skills.
- Chapter 24 — Claude Commands.
- Scratchpad and Run Workspace chapter.
- Future Harness Memory and AI Governance chapters.

## **Required Diagram Inventory**

| Diagram | Title                                | Primary purpose                                                    |
| ------- | ------------------------------------ | ------------------------------------------------------------------ |
| 25.1    | Memory Versus Other Knowledge Layers | Establish boundaries and authority                                 |
| 25.2    | Memory Lifecycle                     | Explain creation, verification, retrieval, updates, and retirement |
| 25.3    | Temporary Versus Persistent State    | Show selective promotion from Scratchpad to durable knowledge      |
| 25.4    | Memory Conflict Resolution           | Explain evidence-based handling of conflicting context             |
| 25.5    | Memory Inside the Harness            | Show retrieval, Agent roles, gates, and controlled persistence     |

All diagrams should follow the handbook's established architecture conventions, remain platform-neutral at the conceptual level, and use Alpha Car Detailing where concrete examples are needed.

## **Chapter Boundary and Editorial Guidance**

Chapter 25 should remain focused on **persistent context management and its engineering boundaries**.

It should not duplicate the full instruction architecture from Chapter 22, the operational Scratchpad design, or the future detailed treatment of Harness Memory and self-learning systems.

The chapter should establish enough integration detail for readers to understand how persistent memory works within the Harness while reserving deeper learning-engine implementation, organizational memory evolution, and adaptive improvement mechanisms for their dedicated chapters.

Claude Code should remain the primary practical platform, with GitHub Copilot and Codex comparisons included where capabilities and behavior materially differ.

**Chapter 25 outline is complete.**
