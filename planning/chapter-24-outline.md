# **Chapter 24 — Claude Commands**

**Part IV — Claude Engineering**

**Project:** Enterprise AI Engineering Handbook\
**Repository:** [Enterprise-AI-Engineering-Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)\
**Target file:** `book/24-claude-commands.md`\
**Running example:** Alpha Car Detailing

## **Chapter Objective**

Explain Claude Commands as explicit, user-invoked entry points for common engineering activities and workflows within enterprise AI-assisted software development.

The chapter will demonstrate how commands provide a consistent interface for initiating repository discovery, planning, implementation, review, validation, evaluation, testing, and bug fixing.

It will establish how commands integrate with Instructions, Skills, Prompts, AI Agent roles, deterministic gates, and the Harness without duplicating their responsibilities.

**Core principle:**

> Commands define how work is initiated. Instructions define engineering constraints. Skills define reusable procedures. Prompts provide task-specific context. Roles define responsibilities. The Harness coordinates execution, enforces deterministic gates, and preserves evidence.

The chapter will distinguish native Claude Code command behavior from enterprise-specific conventions and custom Harness implementations.

# **Detailed Chapter Outline**

## **1. Story-Driven Opening — From Repeated Prompts to Governed Commands**

### **1.1 The Enterprise Engineering Scenario**

- Introduce an Alpha Car Detailing development team implementing a new corporate fleet booking capability.
- Explain how developers repeatedly enter lengthy prompts for common engineering activities.
- Show inconsistencies in repository discovery, planning, implementation, testing, and review.
- Demonstrate how different developers invoke the same workflow differently.
- Highlight the risks of missing validation, architecture constraints, and approval requirements.

### **1.2 The Problem with Unstructured Workflow Invocation**

- Repeated prompt construction.
- Inconsistent engineering entry points.
- Missing task context.
- Unclear ownership of execution.
- Accidental omission of Skills and Instructions.
- Inconsistent validation and evidence collection.
- Difficulties standardizing team practices.

### **1.3 Introducing Claude Commands**

Illustrative commands:

```
/discover
/plan
/implement
/review
/validate
/evaluate
/test
/fix
/goal
```

Explain how each command represents a recognizable engineering action rather than an unrestricted instruction to modify code.

### **1.4 The Architectural Shift**

Transition from:

```
Developer → Ad hoc Prompt → AI Agent
```

To:

```
Developer
    ↓
Command
    ↓
Context and Policy Resolution
    ↓
Skills and Role Selection
    ↓
Harness Execution
    ↓
Evidence and Results
```

Clarify that the Harness-integrated flow is the handbook's proposed enterprise architecture, not an automatic property of every Claude Code command.

### **1.5 Chapter Thesis**

Commands standardize how engineering workflows begin, but they do not replace the systems responsible for implementation, validation, evaluation, governance, or approval.

## **2. Learning Objectives**

After completing this chapter, readers should be able to:

1. Explain the purpose and responsibilities of Claude Commands.
2. Differentiate Commands, Prompts, Skills, Instructions, and AI Agent roles.
3. Design discoverable, reusable, and maintainable engineering commands.
4. Define command inputs, arguments, preconditions, and expected outputs.
5. Understand command scope and execution behavior.
6. Map commands to reusable Skills and responsible AI Agent roles.
7. Integrate commands with enterprise Harness workflows.
8. Apply deterministic validation and structured error handling.
9. Establish security boundaries and human approval requirements.
10. Implement command composition without creating uncontrolled execution chains.
11. Version, test, review, and govern command definitions.
12. Build a practical command library for Alpha Car Detailing.

## **3. Background — Understanding Claude Commands**

### **3.1 What Is a Claude Command?**

- Definition and purpose.
- Explicit invocation through slash-command syntax.
- Commands as reusable workflow entry points.
- Relationship to natural-language interaction.
- Built-in commands versus project-defined commands.
- Claude Code's evolving command and Skill model.

### **3.2 Why Commands Matter in Enterprise AI Engineering**

- Standardized engineering workflows.
- Reduced repetitive prompting.
- Discoverability.
- Consistent context preparation.
- Predictable invocation contracts.
- Easier onboarding.
- Reusable team practices.
- Governance and auditability.

### **3.3 Commands as Interfaces, Not Execution Authorities**

- A command expresses user intent.
- Command definitions may specify execution guidance.
- Commands do not inherently grant unrestricted tool permissions.
- Authorization and policy enforcement remain separate responsibilities.
- A command's apparent success does not establish implementation correctness.

### **3.4 Built-In Commands, Custom Commands, and Harness Commands**

Distinguish:

- Native Claude Code commands.
- User-defined and project-defined command entry points.
- Skill-backed slash-command entry points.
- Enterprise workflow commands.
- Harness-specific command abstractions.

### **3.5 Platform Evolution and Terminology**

- Historical standalone custom command files.
- Current Skill-oriented command definitions.
- Compatibility and migration considerations.
- Avoiding assumptions that every illustrative command is built into Claude Code.
- Distinguishing documented platform behavior from handbook conventions.

## **4. Core Concepts — Commands and Their Relationships**

### **4.1 Commands versus Prompts**

- Commands as named entry points.
- Prompts as task-specific requests and context.
- Parameterized prompts.
- Commands invoking or assembling prompts.
- Why a Prompt Library remains valuable.

Example:

```
/implement "Add corporate fleet booking API"
```

### **4.2 Commands versus Skills**

- Commands initiate workflows.
- Skills provide reusable engineering procedures.
- One command invoking one Skill.
- One command coordinating multiple Skills.
- Multiple commands reusing the same Skill.
- Skill-backed command definitions in Claude Code.
- Avoiding duplicate procedural logic.

### **4.3 Commands versus CLAUDE.md**

- Persistent Instructions versus invocation-specific requests.
- Repository-wide architecture and coding standards.
- Command-specific guidance.
- Instruction precedence and conflicts.
- Commands must not override mandatory engineering policies.

### **4.4 Commands versus AI Agent Roles**

- Commands identify requested activities.
- Roles identify responsibility and authority.
- Command-to-role routing.
- Role isolation.
- Role-specific evidence and outputs.

### **4.5 Commands versus Steering Notes**

- Commands initiate work.
- Steering Notes provide mission-specific priorities and constraints.
- Reading relevant Steering Notes before execution.
- Preserving human ownership.

### **4.6 Commands versus Harness Workflows**

- Interactive command invocation.
- Orchestrated multi-agent execution.
- Deterministic gates.
- Retry and failure handling.
- Evidence collection.
- Completion and approval boundaries.

### **4.7 Commands versus Tools and Hooks**

- Commands as workflow entry points.
- Tools as capabilities for performing actions.
- Hooks as lifecycle-triggered automation.
- Deterministic checks versus model-generated judgments.
- Why commands must not be treated as substitutes for enforcement.

### **4.8 Responsibility Comparison**

| Component          | Primary responsibility                  |
| ------------------ | --------------------------------------- |
| Command            | Initiate an engineering action          |
| Prompt             | Describe the task and its context       |
| Skill              | Define a reusable procedure             |
| Instruction        | Establish persistent constraints        |
| Role               | Assign responsibility and authority     |
| Steering Note      | Supply mission context                  |
| Tool               | Perform an authorized operation         |
| Hook               | Trigger lifecycle automation            |
| Harness            | Coordinate and control execution        |
| Deterministic Gate | Verify objective engineering conditions |

## **5. Command Architecture and Design**

### **5.1 Command Anatomy**

- Command name.
- Description.
- Invocation syntax.
- Arguments.
- Required context.
- Preconditions.
- Applicable Skills.
- Responsible roles.
- Expected outputs.
- Validation requirements.
- Failure behavior.
- Approval requirements.

### **5.2 Command Definition Structure**

- Markdown-based command definitions.
- Metadata and frontmatter where supported.
- Instruction body.
- Argument placeholders.
- References to Skills and repository resources.
- Separation between native syntax and enterprise conventions.

### **5.3 Command Naming Conventions**

- Short, descriptive names.
- Verb-based naming.
- Consistent terminology.
- Avoiding ambiguous names.
- Avoiding conflicting or overlapping commands.
- Reserved and built-in names.
- Naming conventions for organization-specific workflows.

### **5.4 Command Scope**

- Personal commands.
- Repository-level commands.
- Shared team commands.
- Organization-distributed commands.
- Plugin-provided commands where supported.
- Environment-specific workflow behavior.

### **5.5 Command Discovery**

- Finding available commands.
- Discovering command descriptions.
- Understanding argument requirements.
- Repository onboarding.
- Command catalogs.
- Deprecation and replacement guidance.

### **5.6 Inputs and Arguments**

- Positional arguments.
- Free-text arguments.
- Optional parameters.
- Required parameters.
- Paths and identifiers.
- Branches and work-item references.
- Input validation.
- Default behavior.
- Missing and ambiguous inputs.

### **5.7 Command Preconditions and Postconditions**

- Repository readiness.
- Branch state.
- Required files.
- Tool availability.
- Access permissions.
- Expected artifacts.
- Explicit completion criteria.

### **5.8 Command Outputs**

- Human-readable responses.
- Structured implementation plans.
- Code changes.
- Review findings.
- Validation reports.
- Evaluation results.
- Harness run identifiers.
- Evidence locations.

### **5.9 Command Contracts**

Introduce a handbook-defined command contract documenting:

- Intent.
- Inputs.
- Dependencies.
- Authorized operations.
- Outputs.
- Failure states.
- Completion conditions.

Distinguish this governance contract from native Claude Code command-file metadata.

## **6. Professional Diagrams**

### **6.1 Diagram 24.1 — Command Execution Flow**

Illustrate:

```
User Invocation
      ↓
Command Resolution
      ↓
Input and Context Preparation
      ↓
Instruction and Policy Checks
      ↓
Skill / Role Selection
      ↓
Authorized Execution
      ↓
Validation and Evidence
      ↓
Result to User
```

Include missing-input and failed-execution paths.

### **6.2 Diagram 24.2 — Command versus Skill versus Prompt**

Show the relationship among:

- Command.
- Task Prompt.
- Reusable Skill.
- Persistent Instructions.
- Role.
- Execution tools.

Emphasize that these are logical responsibilities that may be implemented together in a platform-specific artifact.

### **6.3 Diagram 24.3 — Command-to-Harness Flow**

Illustrate:

```
/implement
    ↓
Harness Entry Point
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
Approval / Completion
```

Include deterministic gates, iteration loops, and run-scoped evidence.

### **6.4 Diagram 24.4 — Command-to-Role Mapping**

Show commands mapped to the Lead, Developer, Reviewer, Validator, and Evaluator roles, including workflows that require more than one role.

### **6.5 Diagram 24.5 — Command Lifecycle and Governance**

Illustrate:

```
Propose → Design → Review → Test
    → Approve → Publish → Monitor
    → Version / Deprecate
```

## **7. Hands-on Example — Building the Alpha Car Detailing Command Library**

### **7.1 Scenario and Requirements**

Build a governed command library for a .NET 10 Clean Architecture solution supporting corporate fleet bookings.

### **7.2 Example Repository Layout**

- Application source code.
- Repository Instructions.
- Skills.
- Command definitions.
- Prompt Library.
- Harness scripts.
- Tests.
- Run artifacts.

### **7.3 Implementing&#x20;**`/discover`

- Inspect repository structure.
- Identify relevant projects and services.
- Discover architecture boundaries.
- Identify applicable Instructions and Skills.
- Generate repository discovery evidence.
- Enforce read-only discovery.

### **7.4 Implementing&#x20;**`/plan`

- Accept a feature request.
- Gather repository evidence.
- Identify affected components.
- Produce implementation steps.
- Identify risks and dependencies.
- Define acceptance criteria.
- Require approval before implementation when appropriate.

### **7.5 Implementing&#x20;**`/implement`

- Consume an approved plan.
- Resolve implementation Skills.
- Assign the Developer role.
- Implement corporate fleet booking functionality.
- Preserve Clean Architecture boundaries.
- Produce implementation evidence.
- Submit changes for review and validation.

### **7.6 Implementing&#x20;**`/review`

- Review code changes.
- Evaluate architecture alignment.
- Identify maintainability and security concerns.
- Produce structured findings.
- Avoid silently modifying reviewed code.

### **7.7 Implementing&#x20;**`/validate`

- Run deterministic build checks.
- Execute unit and integration tests.
- Verify architecture boundaries.
- Check API and event contracts.
- Execute configured security checks.
- Produce reproducible pass/fail evidence.

### **7.8 Implementing&#x20;**`/evaluate`

- Consume Reviewer and Validator evidence.
- Compare implementation with acceptance criteria.
- Assess business outcome quality.
- Report pass, fail, partial, or inconclusive.
- Avoid replacing human approval.

### **7.9 Implementing&#x20;**`/test`

- Select appropriate test scope.
- Execute unit and integration tests.
- Support targeted test execution.
- Report failures and evidence.
- Distinguish targeted checks from required final validation.

### **7.10 Implementing&#x20;**`/fix`

- Accept a defect description.
- Identify affected components.
- Reproduce the defect.
- Develop a correction plan.
- Implement the fix.
- Execute focused regression tests.
- Route the result through required review and validation.

### **7.11 Implementing&#x20;**`/goal`

- Define an explicit desired engineering outcome.
- Specify measurable acceptance conditions.
- Distinguish goals from implementation instructions.
- Translate the goal into a Harness execution plan.
- Define completion evidence.
- Prevent unsupported declarations of success.

Example:

```
/goal "All fleet booking tests pass and
architecture validation reports zero violations"
```

### **7.12 Command Catalog and Mapping**

| Command      | Primary role     | Representative output        |
| ------------ | ---------------- | ---------------------------- |
| `/discover`  | Lead             | Repository discovery report  |
| `/plan`      | Lead / Architect | Implementation plan          |
| `/implement` | Developer        | Code changes                 |
| `/review`    | Reviewer         | Review findings              |
| `/validate`  | Validator        | Deterministic evidence       |
| `/evaluate`  | Evaluator        | Outcome assessment           |
| `/test`      | Validator        | Test results                 |
| `/fix`       | Lead / Developer | Defect correction            |
| `/goal`      | Lead             | Goal and completion criteria |

Clarify that mappings are enterprise conventions, not automatic Claude Code role assignments.

### **7.13 End-to-End Workflow**

Demonstrate a corporate fleet booking change moving through discovery, planning, implementation, review, validation, evaluation, and approval.

## **8. Claude Example — Native Commands and Enterprise Workflows**

### **8.1 Claude Code Command Capabilities**

- Native slash-command interaction.
- Custom command definitions.
- Skill-backed command invocation.
- Command discovery.
- Invocation arguments.
- Platform-supported metadata.

### **8.2 Creating a Project-Level Command**

- Select the appropriate supported command definition format.
- Define metadata.
- Write the command instructions.
- Accept task arguments.
- Reference repository context.
- Test interactive invocation.

### **8.3 Mapping Commands to Claude Skills**

- Reusing existing Skills.
- Avoiding duplicate instructions.
- Skill selection.
- Explicit versus automatic invocation.
- Understanding limitations of invocation controls.

### **8.4 Commands and CLAUDE.md**

- Loading repository Instructions.
- Applying architecture constraints.
- Handling conflicts.
- Maintaining separation of responsibilities.

### **8.5 Commands and Claude Agent Roles**

- Delegation to specialized agents.
- Role-specific context.
- Execution boundaries.
- Handoff artifacts.
- Maintaining independent review and validation.

### **8.6 Commands and Claude Execution Modes**

- Interactive execution.
- Planning-oriented execution.
- Tool authorization.
- Human confirmation.
- Noninteractive automation considerations.

### **8.7 Integrating Claude Commands with the Harness**

- Entry-point adapters.
- Harness invocation contracts.
- Task context transfer.
- Run-scoped workspaces.
- Status reporting.
- Evidence persistence.
- Error propagation.

### **8.8 Full Alpha Car Detailing Demonstration**

Demonstrate:

```
/discover
/plan "Add corporate fleet booking cancellation"
/implement
/review
/validate
/evaluate
```

Explain which steps are native platform features and which require custom command definitions or Harness integration.

## **9. GitHub Copilot Comparison**

### **9.1 Comparable Invocation Mechanisms**

- Chat-based workflow invocation.
- Reusable prompt files.
- Custom instructions.
- Agent capabilities.
- Platform-specific command behavior.

### **9.2 Claude Commands versus Copilot Prompt Files**

- Definition format.
- Invocation experience.
- Argument handling.
- Discovery.
- Context loading.
- Reuse.

### **9.3 Skills and Instructions Comparison**

- Reusable procedures.
- Repository-wide constraints.
- Role-specific behavior.
- Differences in platform support.

### **9.4 Harness Integration**

- Vendor-neutral command contracts.
- Provider adapters.
- Shared Skills and prompts.
- Common evidence formats.

### **9.5 Portability Considerations**

Identify which command concepts transfer across platforms and which require platform-specific implementation.

## **10. Codex Comparison**

### **10.1 Comparable Codex Entry Points**

- Task-oriented invocation.
- Reusable Skills.
- Repository Instructions.
- Automation entry points where supported.

### **10.2 Claude Commands versus Codex Workflows**

- Invocation model.
- Command definition and discovery.
- Skill integration.
- Task context.
- Execution permissions.

### **10.3 Role and Harness Integration**

- Shared role contracts.
- Provider-independent workflow definitions.
- Consistent validation and evaluation.

### **10.4 Portability and Migration**

- Mapping command intent.
- Translating platform-specific syntax.
- Preserving governance.
- Maintaining consistent outcomes.

### **10.5 Comparison Summary**

A concise comparison matrix covering Claude Code, GitHub Copilot, and Codex without implying unsupported feature equivalence.

## **11. Best Practices**

### **11.1 Command Design**

- Keep commands focused on a clear engineering action.
- Use consistent naming.
- Document arguments.
- Define expected outcomes.
- Make preconditions explicit.
- Avoid excessive command proliferation.

### **11.2 Reusability**

- Reuse Skills instead of copying procedures.
- Keep task context in Prompts.
- Preserve persistent constraints in Instructions.
- Centralize common governance contracts.

### **11.3 Execution Reliability**

- Validate inputs.
- Handle missing context.
- Define failure behavior.
- Preserve idempotency where appropriate.
- Prevent accidental repeated side effects.
- Distinguish retryable and non-retryable failures.

### **11.4 Validation and Quality**

- Associate commands with appropriate checks.
- Require deterministic evidence.
- Preserve independent review.
- Avoid bypassing final validation gates.
- Keep evaluation separate from validation.

### **11.5 Security**

- Apply least privilege.
- Treat arguments and repository content as untrusted.
- Restrict sensitive operations.
- Protect secrets.
- Prevent command-based policy bypass.
- Require approval for high-impact actions.

### **11.6 Command Composition**

- Prefer explicit workflow dependencies.
- Avoid hidden execution chains.
- Define handoff contracts.
- Prevent uncontrolled recursion.
- Preserve failure and cancellation propagation.

### **11.7 Testing and Maintenance**

- Test argument handling.
- Test expected behavior.
- Test failure scenarios.
- Maintain regression coverage.
- Review commands after platform upgrades.

### **11.8 Ownership and Governance**

- Assign maintainers.
- Require peer review.
- Version command definitions.
- Maintain a changelog.
- Establish deprecation policies.
- Monitor adoption and reliability.

## **12. Anti-patterns**

### **12.1 Commands as Giant Prompts**

Embedding entire engineering workflows in oversized command definitions.

### **12.2 Commands Duplicating Skills**

Maintaining the same reusable procedure in multiple places.

### **12.3 Commands Overriding Instructions**

Allowing task-level invocation text to bypass mandatory architecture or security constraints.

### **12.4 Unrestricted Implementation Commands**

Permitting code changes without appropriate context, review, or validation.

### **12.5 Hidden Command Chains**

Automatically triggering multiple consequential actions without clear user intent or approval.

### **12.6 Command Explosion**

Creating numerous overlapping commands with unclear responsibilities.

### **12.7 Weak Input Validation**

Accepting ambiguous or unsafe arguments without checks.

### **12.8 Commands That Self-Approve**

Allowing implementation workflows to declare their own completion without independent evidence.

### **12.9 Commands That Hide Failures**

Reporting success despite failed tests, incomplete execution, or missing evidence.

### **12.10 Hardcoded Environment Assumptions**

Embedding machine-specific paths, credentials, branch names, or deployment targets.

### **12.11 Unversioned Command Changes**

Changing shared command behavior without review or compatibility consideration.

### **12.12 Commands as Security Boundaries**

Assuming a command description alone can enforce permissions, tool restrictions, or organizational policy.

## **13. Architect's Notes**

### **13.1 Commands as an Enterprise Interface Layer**

- Commands expose engineering capabilities.
- Underlying implementation should remain replaceable.
- Command contracts should outlive individual AI platforms.

### **13.2 Separating Intent from Execution**

- Command identifies requested action.
- Prompt supplies context.
- Skill provides procedure.
- Role owns responsibility.
- Harness controls execution.

### **13.3 Command-to-Harness Adapter Pattern**

- Platform-specific invocation adapters.
- Normalized command contracts.
- Workflow orchestration.
- Standardized results.

### **13.4 Command Execution State**

- Invocation identifiers.
- Run-scoped workspace.
- Execution status.
- Checkpoints.
- Retry history.
- Evidence references.

### **13.5 Idempotency and Side Effects**

- Read-only commands.
- Mutating commands.
- External side effects.
- Duplicate invocation protection.
- Recovery from partial execution.

### **13.6 Command Composition Architecture**

- Explicit workflow graphs.
- Dependency ordering.
- Conditional transitions.
- Failure propagation.
- Cancellation and timeouts.

### **13.7 Trust and Authority Boundaries**

- User authority.
- Agent authority.
- Harness authority.
- Tool permissions.
- Human approval.

### **13.8 Architectural Decision Records**

Document when to introduce a new command, reuse an existing Skill, or implement a workflow directly in the Harness.

## **14. Enterprise Tips**

### **14.1 Establish an Approved Command Catalog**

Maintain a discoverable catalog of supported engineering commands.

### **14.2 Standardize Command Contracts**

Define organization-wide requirements for arguments, outputs, validation, errors, and approval.

### **14.3 Introduce Command Ownership**

Assign responsible maintainers and reviewers.

### **14.4 Treat Commands as Version-Controlled Assets**

Apply pull requests, review policies, automated checks, and release practices.

### **14.5 Integrate Commands with CI/CD**

Connect command-triggered changes to established build, test, security, and deployment controls.

### **14.6 Preserve Audit Evidence**

Record command intent, relevant inputs, run identifiers, execution outcomes, approvals, and evidence references.

### **14.7 Protect Sensitive Information**

Avoid exposing credentials, personal information, or regulated data through command arguments, logs, or artifacts.

### **14.8 Measure Command Effectiveness**

Track:

- Invocation frequency.
- Success and failure rates.
- Execution duration.
- Validation failures.
- Retry frequency.
- Approval delays.
- Manual intervention.

### **14.9 Support Multiple AI Platforms**

Keep enterprise command contracts independent of Claude-specific implementation details.

### **14.10 Govern Command Evolution**

Require explicit review and approval before adopting proposed changes to shared command behavior or engineering standards.

## **15. Decision Points**

### **15.1 When Should an Engineering Action Become a Command?**

Evaluate repetition, discoverability, complexity, and organizational value.

### **15.2 Command or Skill?**

Determine whether the requirement concerns workflow invocation or reusable implementation knowledge.

### **15.3 Command or Prompt?**

Determine whether the request requires a stable named entry point or only task-specific context.

### **15.4 Native Command or Harness Workflow?**

Evaluate orchestration complexity, deterministic gates, audit requirements, and role separation.

### **15.5 Single-Role or Multi-Role Execution?**

Determine when delegation and independent assessment are necessary.

### **15.6 Interactive or Automated Invocation?**

Evaluate risk, permissions, repeatability, and operational controls.

### **15.7 Local, Repository, or Organization Scope?**

Determine appropriate distribution and ownership.

### **15.8 When Is Human Approval Required?**

Consider code changes, infrastructure modifications, deployments, data migrations, and security-sensitive operations.

### **15.9 When Should Commands Be Composed?**

Evaluate dependencies, failure handling, transparency, and reuse.

### **15.10 When Should a Command Be Deprecated?**

Consider duplication, obsolete platform behavior, changing architecture, and maintenance cost.

## **16. Exercises**

### **Exercise 1 — Design a Repository Discovery Command**

Create a `/discover` command for Alpha Car Detailing that gathers repository evidence without modifying source code.

### **Exercise 2 — Design a Planning Command**

Define `/plan` with task arguments, architecture context, expected outputs, and approval requirements.

### **Exercise 3 — Build an Implementation Command**

Create `/implement` for a corporate fleet booking API using an existing implementation Skill.

### **Exercise 4 — Map Commands to Roles**

Define the responsible roles and expected evidence for all nine example commands.

### **Exercise 5 — Implement Validation**

Design `/validate` to execute deterministic checks for a .NET 10 solution.

### **Exercise 6 — Define a Goal Command**

Create `/goal` with measurable completion conditions and explicit evidence requirements.

### **Exercise 7 — Compose an Engineering Workflow**

Design an end-to-end workflow connecting discovery, planning, implementation, review, validation, and evaluation.

### **Exercise 8 — Handle Command Failures**

Define behavior for invalid arguments, missing files, unavailable tools, failed tests, and permission denial.

### **Exercise 9 — Introduce Approval Boundaries**

Add human approval checkpoints to commands capable of modifying infrastructure or production data.

### **Exercise 10 — Test Command Definitions**

Create tests for argument handling, expected outputs, security restrictions, and failure propagation.

### **Exercise 11 — Design Command Governance**

Define ownership, versioning, review, publication, and deprecation policies.

### **Exercise 12 — Integrate Commands with the Harness**

Design a provider-independent adapter that maps `/implement` to an existing Harness workflow.

## **17. Interview Questions**

### **Foundational Questions**

1. What is a Claude Command?
2. How does a Command differ from a Prompt?
3. How does a Command differ from a Skill?
4. What is the relationship between Commands and CLAUDE.md?
5. Why are commands useful in enterprise AI-assisted development?
6. What is the difference between built-in and custom commands?

### **Design Questions**

1. What information should a command definition contain?
2. How should command arguments be designed and validated?
3. How should commands be named and discovered?
4. What are the different scopes of command definitions?
5. How should a command reference reusable Skills?
6. How can multiple commands share a common Skill?
7. What makes a command suitable for composition?

### **Architecture Questions**

 1. How do commands map to AI Agent roles?
 2. How can commands integrate with an enterprise Harness?
 3. What is the difference between a command and a Harness workflow?
 4. How should command execution state be maintained?
 5. How can command execution be made idempotent?
 6. How should command failures and retries be handled?
 7. How can command contracts remain portable across AI platforms?

### **Validation and Governance Questions**

 1. Why should commands not replace deterministic validation?
 2. How should commands interact with Reviewer, Validator, and Evaluator roles?
 3. When should human approval be required?
 4. What security risks arise from command arguments?
 5. How should command definitions be tested?
 6. How should commands be versioned and governed?
 7. What audit evidence should be preserved?
 8. Why should commands not be allowed to silently modify engineering standards?

### **Scenario-Based Questions**

 1. Design `/implement` for a corporate fleet booking API.
 2. Explain how `/fix` should handle a failed regression test.
 3. Design `/goal` to establish measurable completion criteria.
 4. Explain how to prevent `/validate` from reporting success when required checks were skipped.
 5. Design a command-to-Harness workflow involving multiple AI Agent roles.
 6. Explain how to migrate an enterprise command library from Claude Code to another AI coding platform.

## **18. Chapter Summary**

### **18.1 Key Concepts**

Summarize commands as explicit entry points for recurring engineering activities.

### **18.2 Separation of Responsibilities**

Reinforce the distinction between Commands, Prompts, Skills, Instructions, Roles, Tools, Hooks, and the Harness.

### **18.3 Enterprise Command Architecture**

Summarize command contracts, inputs, outputs, execution flow, and role mapping.

### **18.4 Validation and Approval**

Reinforce that command invocation does not establish correctness or authorization.

### **18.5 Governance and Maintainability**

Summarize ownership, testing, versioning, security, and auditability.

### **18.6 Alpha Car Detailing Outcomes**

Review the command library created for discovery, planning, implementation, review, validation, evaluation, testing, fixing, and goal definition.

### **18.7 Final Principle**

> A well-designed Command makes engineering work easy to initiate, but the quality and safety of that work depend on governed execution, reusable Skills, persistent Instructions, deterministic evidence, independent assessment, and appropriate human approval.

## **19. Further Reading**

### **19.1 Official Claude Documentation**

- Claude Code slash commands and Skills.
- Claude Code memory and CLAUDE.md.
- Claude Code subagents.
- Claude Code hooks.
- Claude Code permissions and security.
- Claude Code noninteractive and automation workflows.

### **19.2 Handbook Cross-References**

- Chapter 7 — Skills.
- Chapter 8 — Prompts.
- Chapter 9 — Roles.
- Chapter 10 — Steering Notes.
- Chapter 12 — What Is a Harness?
- Chapter 13 — Harness Architecture.
- Chapter 14 — Lead Agent.
- Chapter 15 — Developer Agent.
- Chapter 16 — Reviewer Agent.
- Chapter 17 — Validator Agent.
- Chapter 18 — Evaluator Agent.
- Chapter 22 — CLAUDE.md.
- Chapter 23 — Claude Skills.

### **19.3 Related Engineering Topics**

- Command pattern.
- Workflow orchestration.
- Contract-driven engineering.
- Least-privilege security.
- Deterministic verification.
- Human-in-the-loop systems.
- Auditability and traceability.
- Version-controlled automation.

### **19.4 Companion Repository Resources**

Planned supporting assets:

- Alpha Car Detailing command definitions.
- Command catalog.
- Command contract template.
- Command-to-Skill mapping examples.
- Command-to-role mapping examples.
- Harness integration examples.
- Command test scenarios.
- Governance checklist.

## **Chapter Coverage Checklist**

| Requirement                                 | Outline coverage    |
| ------------------------------------------- | ------------------- |
| Command fundamentals and comparisons        | Sections 3–4        |
| Structure, inputs, naming, scope, discovery | Section 5           |
| Execution and architecture                  | Sections 5–8        |
| Command-to-Skill mapping                    | Sections 4, 7–8     |
| Command-to-role mapping                     | Sections 4, 6–8     |
| Validation and error handling               | Sections 7, 11–12   |
| Human approval and security                 | Sections 11, 13–15  |
| Command composition                         | Sections 11, 13, 15 |
| Versioning, testing, governance             | Sections 11, 14–16  |
| Commands inside the Harness                 | Sections 6–8, 13    |
| Claude, Copilot, Codex                      | Sections 8–10       |
| Best practices and anti-patterns            | Sections 11–12      |
| Architect's Notes and Enterprise Tips       | Sections 13–14      |
| Decision Points                             | Section 15          |
| Exercises and Interview Questions           | Sections 16–17      |
| Chapter Summary and Further Reading         | Sections 18–19      |
| All four required diagrams                  | Section 6           |

The outline follows the frozen 19-section chapter template and preserves the handbook's Claude-first, vendor-neutral engineering approach.

**Chapter 24 outline is complete.**
