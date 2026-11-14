# **Chapter 26 — Tool Calling**

Part IV — Claude Engineering

**Project:** Enterprise AI Engineering Handbook\
**Repository:** [Enterprise-AI-Engineering-Handbook](https://github.com/sheikh-zeeshan/Enterprise-AI-Engineering-Handbook?utm_source=chatgpt.com)\
**Target file:** `book/26-tool-calling.md`\
**Chapter type:** Architecture, implementation, security, and operational guidance\
**Primary platform:** Claude Code\
**Comparative platforms:** GitHub Copilot and OpenAI Codex\
**Running example:** Alpha Car Detailing

## **Chapter Objective**

Explain how Claude Code and other AI coding agents use tools to interact with development environments, repositories, build systems, test frameworks, APIs, databases, and cloud infrastructure.

The chapter establishes tool calling as the operational execution layer of AI Engineering, connecting agent reasoning and engineering instructions to observable actions while enforcing enterprise security, permission, validation, and approval boundaries.

**Core principle:**

> Tool calling gives AI Agents operational capability, but every invocation must respect scope, permissions, validation, security, and human approval boundaries. A successful tool execution does not automatically mean the engineering task is correct or approved.

## **Detailed Chapter Outline**

### **1. Story-Driven Opening — When an Agent Needs to Act**

**Scenario: Implementing Fleet Booking Validation in Alpha Car Detailing**

1. Introduce a development request to enhance corporate fleet booking validation.
2. The Developer Agent needs to:
   - Discover the booking-related projects and source files.
   - Inspect existing domain rules and API contracts.
   - Read applicable repository Instructions and Skills.
   - Modify an application command handler and its tests.
   - Run `dotnet build` and targeted `dotnet test` commands.
   - Execute architecture and security checks.
   - Collect evidence for the Validator Agent.
3. Explain why language-model reasoning alone cannot inspect or change a real repository.
4. Introduce tool calls as bounded interactions with the execution environment.
5. Demonstrate risks arising from unrestricted access:
   - Unauthorized file modification.
   - Destructive shell execution.
   - Production database changes.
   - Secret exposure.
   - Uncontrolled cloud operations.
6. Contrast an unrestricted agent with an agent operating through a governed Harness.
7. Establish the separation between an agent's proposed action and authorization to execute it.

**Key takeaway:** An AI Agent becomes operationally useful through tools, but trustworthy execution requires explicit controls.

### **2. Learning Objectives**

By the end of this chapter, readers should be able to:

1. Define tool calling and explain its role in AI-assisted software development.
2. Differentiate Tools, Skills, Commands, Prompts, Instructions, and Harness responsibilities.
3. Explain tool discovery, selection, input validation, execution, and result handling.
4. Use Claude Code tools for repository exploration, file modification, builds, and tests.
5. Design read-only and write-capable tool access according to least privilege.
6. Identify high-risk operations and introduce approval boundaries.
7. Implement timeout, retry, cancellation, and idempotency policies.
8. Design controlled tool chaining and dependency management.
9. Integrate tool calling with the Harness execution lifecycle and agent roles.
10. Capture reliable execution evidence for validation, evaluation, and audit.
11. Secure external API, database, cloud, and MCP-based tool integrations.
12. Compare Claude Code, GitHub Copilot, and OpenAI Codex tool-calling approaches.
13. Diagnose failed, unauthorized, or unsafe tool execution.
14. Apply enterprise tool governance patterns to Alpha Car Detailing.

### **3. Background — From Reasoning to Operational Execution**

#### **3.1 The Limits of Model-Only Reasoning**

- Generated suggestions versus actual repository state.
- Why models cannot assume files, builds, tests, or infrastructure exist as described.
- Knowledge limitations and environment-dependent facts.
- The distinction between describing an operation and executing it.

#### **3.2 What Tool Calling Is**

- Definition of a tool.
- Tools as externally executable capabilities.
- Structured and unstructured tool interfaces.
- Tool invocation requests and execution results.
- Responsibility of the host environment or Harness.
- Model-requested versus policy-authorized actions.

#### **3.3 Why AI Agents Need Tools**

- Repository discovery.
- Source inspection and modification.
- Build and test execution.
- Architecture verification.
- Documentation retrieval.
- External system integration.
- Operational diagnostics.

#### **3.4 Tool Calling Versus Traditional Automation**

- Fixed automation scripts.
- Dynamic agent tool selection.
- Deterministic execution versus model-driven decisions.
- Benefits and limitations of adaptive execution.
- Why deterministic checks remain necessary.

#### **3.5 Tool Calling in the Enterprise AI Engineering Model**

- Relationship to previously established engineering concepts.
- Tool calling as an execution mechanism rather than a source of authority.
- Why tool availability does not imply permission.
- Why the Harness retains control of execution policy.

### **4. Concepts — Tool Calling Fundamentals**

#### **4.1 Tools, Skills, and Commands**

Define and compare:

| Concept       | Primary responsibility                                              |
| ------------- | ------------------------------------------------------------------- |
| Tool          | Performs a specific operation                                       |
| Skill         | Defines a reusable engineering workflow                             |
| Command       | Provides an invocation mechanism or workflow entry point            |
| Prompt        | Specifies the current task                                          |
| Instruction   | Establishes applicable engineering requirements                     |
| Steering Note | Provides task or mission context and constraints                    |
| Agent Role    | Defines responsibilities and operating boundaries                   |
| Harness       | Orchestrates execution, permissions, gates, evidence, and approvals |

Additional topics:

- A Skill may invoke multiple tools.
- A command may trigger an agent workflow.
- A tool does not independently establish engineering policy.
- Skills and Instructions do not override execution permissions.
- Examples using the Alpha Car Detailing booking service.

#### **4.2 Tool Anatomy**

- Tool name and description.
- Supported capability.
- Input parameters.
- Input schema and validation.
- Required and optional arguments.
- Output schema and return format.
- Exit status and error information.
- Side effects.
- Security classification.
- Execution environment.

#### **4.3 Tool Discovery**

- Built-in tools.
- Environment-provided tools.
- Repository-specific tooling.
- MCP-provided tools.
- Tool metadata and discoverability.
- Capability availability checks.
- Version compatibility.
- Unavailable or disabled tools.

#### **4.4 Tool Selection**

- Selecting the correct tool for an operation.
- Choosing a dedicated tool over generic shell execution when appropriate.
- Read-only alternatives to write operations.
- Matching capabilities to task objectives.
- Avoiding unnecessarily privileged tools.
- Preventing tool misuse through ambiguity.

#### **4.5 Tool Inputs**

- Structured arguments.
- File and directory paths.
- Command arguments.
- API request parameters.
- Environment-specific configuration.
- Schema enforcement.
- Input sanitization.
- Repository path restrictions.
- Avoiding shell command injection.

#### **4.6 Tool Outputs**

- Standard output and standard error.
- Process exit codes.
- Structured API responses.
- File modifications and diffs.
- Test reports.
- Build logs.
- Diagnostic evidence.
- Output truncation and pagination.
- Sensitive information redaction.

#### **4.7 Tool Result Interpretation**

- Distinguishing observations from assumptions.
- Confirming that a command actually executed.
- Separating execution success from business correctness.
- Handling incomplete or contradictory results.
- Treating tool output as potentially untrusted data.
- Prohibiting repository content or external responses from silently changing agent authority.

#### **4.8 Tool Execution State**

- Requested.
- Authorized.
- Running.
- Succeeded.
- Failed.
- Timed out.
- Cancelled.
- Blocked.
- Requires approval.
- Indeterminate after interrupted execution.

### **5. Architecture Discussion — Enterprise Tool Calling**

#### **5.1 Core Architectural Components**

- AI Agent.
- Tool registry or capability catalog.
- Tool selection mechanism.
- Policy enforcement layer.
- Permission evaluator.
- Approval mechanism.
- Tool execution adapter.
- Execution sandbox.
- Result collector.
- Evidence store.
- Harness orchestration layer.

#### **5.2 Control Plane Versus Execution Plane**

- Agent proposes actions.
- Harness and host enforce policy.
- Tools execute permitted operations.
- Evidence is collected independently.
- Validators verify objective conditions.
- Evaluators assess task outcomes.
- Humans authorize protected decisions.

#### **5.3 Trust Boundaries**

- Agent-to-tool boundary.
- Workspace-to-host boundary.
- Repository-to-external-system boundary.
- Development-to-production boundary.
- Agent-to-Harness boundary.
- External content-to-instruction boundary.

#### **5.4 Tool Execution Environments**

- Local developer workstation.
- Isolated repository workspace.
- Container-based sandbox.
- CI runner.
- Harness-managed execution worker.
- Cloud-hosted agent environment.
- Environment portability and reproducibility.

#### **5.5 Tool Registry and Contracts**

- Tool identifiers.
- Tool descriptions and input contracts.
- Versioned tool definitions.
- Allowed operations.
- Side-effect metadata.
- Approval requirements.
- Ownership and lifecycle management.
- Contract testing.

#### **5.6 MCP and Extensible Tool Integration**

- Introduction to the Model Context Protocol.
- MCP clients, servers, and exposed tools.
- Tool discovery through MCP.
- Enterprise MCP server trust and approval.
- Authentication and authorization.
- Scoped credentials.
- Server-side enforcement.
- Managing third-party and internal tool providers.
- MCP as an integration mechanism, not an alternative to Harness governance.

#### **5.7 Tool Failure and Isolation Boundaries**

- Tool process failure.
- Partial modification.
- Network interruption.
- Workspace corruption.
- Compromised tool provider.
- Resource exhaustion.
- Recovery ownership.

### **6. Professional Diagrams**

The chapter should include five required diagrams and one supporting reference diagram.

#### **Diagram 26.1 — Tool Calling Lifecycle**

Show the complete sequence:

`Task Context → Tool Selection → Input Preparation → Permission Check → Approval (if required) → Execution → Result Capture → Interpretation → Next Action`

Also show failure, cancellation, and denial paths.

#### **Diagram 26.2 — Agent-to-Tool Interaction**

Show the relationship between the Agent, Harness, policy layer, execution adapter, filesystem, shell, and external systems.

Distinguish logical agent requests from actual execution authority.

#### **Diagram 26.3 — Permission and Human Approval Model**

Illustrate:

- Read-only allowed operations.
- Restricted write operations.
- Privileged actions requiring human approval.
- Prohibited actions.
- Denied requests and escalation.
- Separation between approval and deterministic validation.

#### **Diagram 26.4 — Tool Chaining and Dependencies**

Example:

`Repository Search → Read Source → Modify Code → Build → Targeted Tests → Architecture Checks → Evidence Collection`

Include conditional branches, failed-tool handling, and bounded retries.

#### **Diagram 26.5 — Tool Execution Inside the Harness**

Integrate tool calling with:

- Lead Agent.
- Developer Agent.
- Reviewer Agent.
- Validator Agent.
- Evaluator Agent.
- Policy enforcement.
- Deterministic validation gates.
- Human approval.
- Evidence and audit storage.

#### **Diagram 26.6 — Tool Trust Boundaries**

Illustrate:

- Repository sandbox.
- External APIs.
- Development databases.
- Azure cloud resources.
- Secret management.
- MCP servers.
- Authorization boundaries between environments.

### **7. Hands-on Example — Building a Controlled Tool Workflow**

**Scenario:** Implement a corporate fleet booking rule in Alpha Car Detailing using controlled, auditable tool execution.

#### **7.1 Scenario and Requirements**

- Define the business request.
- Identify acceptance criteria.
- Define repository scope.
- Identify permitted file changes.
- Establish required validation gates.
- Define operations that require approval.

#### **7.2 Repository Discovery and Search Tools**

- Locate the booking-related solution and projects.
- Search for booking command handlers.
- Find existing domain validation logic.
- Identify relevant test projects.
- Inspect architecture test conventions.
- Read repository Instructions and applicable Skills.
- Record discovered paths as evidence.

Example tools:

- File discovery.
- Content search.
- Git inspection.
- Repository metadata tools.

#### **7.3 Filesystem Tools**

- Read individual source files.
- Read configuration files.
- Inspect directory structures.
- Create new test files.
- Modify existing C# source files.
- Review file differences.
- Restrict changes to approved paths.
- Protect unrelated repository content.

#### **7.4 Shell and Build Tools**

- Explain shell execution capabilities and risks.
- Run `dotnet restore` when required.
- Run `dotnet build`.
- Capture command output and exit codes.
- Detect build failures.
- Handle missing dependencies.
- Restrict shell execution to approved commands and environments.

#### **7.5 Test Tools**

- Discover relevant unit tests.
- Execute scoped tests using `dotnet test`.
- Execute integration tests.
- Capture test reports.
- Identify failed assertions.
- Rerun relevant tests after corrections.
- Execute broader regression tests at the required final gate.

#### **7.6 Architecture Validation Tools**

- Run architecture tests.
- Verify Clean Architecture dependency rules.
- Check domain and application boundaries.
- Detect prohibited project references.
- Verify coding conventions.
- Collect deterministic architecture evidence.

#### **7.7 Configuration Inspection**

- Read application settings.
- Inspect dependency injection registration.
- Understand environment configuration.
- Identify configuration drift.
- Avoid revealing secrets in output.
- Distinguish configuration discovery from modification.

#### **7.8 API Tools**

- Call local development APIs.
- Use HTTP request tools.
- Provide authenticated requests using scoped credentials.
- Test booking endpoints.
- Inspect HTTP status codes and response bodies.
- Verify negative and positive cases.
- Handle unexpected API responses.

#### **7.9 Database Tools**

- Query a development SQL Server database.
- Inspect relevant schema metadata.
- Run parameterized read-only queries.
- Verify test data.
- Examine migration scripts without automatically applying them.
- Define explicit authorization for database mutations.
- Prevent accidental production access.

#### **7.10 Cloud and Deployment Validation Tools**

- Inspect non-production Azure resources.
- Check deployment configuration.
- Validate infrastructure definitions.
- Perform read-only deployment readiness checks.
- Verify required identity and configuration bindings.
- Separate deployment validation from deployment execution.
- Require explicit approval for protected cloud changes.

#### **7.11 Capturing Execution Evidence**

- Tool name and invocation identifier.
- Validated input parameters.
- Execution environment.
- Start and completion times.
- Exit status.
- Output artifact references.
- Modified files.
- Test results.
- Approval record, where applicable.
- Traceability to task and acceptance criteria.

#### **7.12 Completing the Workflow**

- Review the final diff.
- Run required deterministic gates.
- Prepare evidence for Reviewer, Validator, and Evaluator Agents.
- Identify unresolved issues.
- Produce an execution summary.
- Obtain human approval before protected completion actions.

**Expected hands-on outcome:** A successfully implemented and validated change, with traceable tool execution and no unauthorized side effects.

### **8. Claude Example — Tool Calling in Claude Code**

#### **8.1 Claude Code Tool Execution Model**

- How Claude Code requests tool operations.
- Built-in tool capabilities.
- Native filesystem, search, and shell interactions.
- Host-controlled execution and permissions.
- Tool-call lifecycle and result handling.

#### **8.2 Claude Code Tool Categories**

- Reading and searching files.
- Editing repository content.
- Executing commands.
- Running tests and builds.
- Managing task context.
- Accessing external capabilities through configured integrations.

#### **8.3 Claude Code Tool Discovery**

- Identifying available built-in tools.
- Discovering configured MCP tools.
- Understanding tool descriptions and parameters.
- Checking capability availability.
- Recognizing unsupported or restricted operations.

#### **8.4 Claude Code Permissions**

- Permission configuration.
- Allowed and denied operations.
- Approval prompts.
- Working directory and sandbox restrictions.
- Interactive versus non-interactive execution.
- Permission precedence and enterprise-enforced restrictions.
- Risks of permissive or bypass configurations.

#### **8.5 Claude Code Repository Search Example**

- Prompt to locate corporate fleet booking logic.
- Search strategy.
- File inspection.
- Evidence-based repository understanding.
- Avoiding modifications before discovery is complete.

#### **8.6 Claude Code File Modification Example**

- Request a narrowly scoped implementation.
- Inspect relevant source files.
- Apply minimal edits.
- Review the resulting diff.
- Confirm no unrelated files changed.

#### **8.7 Claude Code Build and Test Example**

- Execute `dotnet build`.
- Run focused unit tests.
- Interpret failures.
- Implement corrections.
- Repeat controlled validation.
- Capture final test evidence.

#### **8.8 Claude Code External Tool Integration**

- Integrating a trusted MCP server.
- Exposing an internal read-only tool.
- Calling development APIs.
- Accessing approved database capabilities.
- Handling credentials and authentication.
- Verifying tool-provider trust.

#### **8.9 Claude Code Tool Hooks and Guardrails**

- Relationship between tool calling and previously introduced Hooks.
- Before-execution policy checks.
- Post-execution observations.
- Blocking prohibited operations.
- Triggering validation where appropriate.
- Avoiding reliance on hooks as the sole security boundary.
- Harness-enforced controls versus platform-specific hook mechanisms.

#### **8.10 Claude Code Tool Error Handling**

- Command failures.
- Tool denials.
- Missing tools.
- Invalid inputs.
- Process timeout.
- Network failure.
- Partial file modifications.
- Recovery and escalation.

#### **8.11 Claude Code in Non-Interactive Harness Execution**

- Running Claude Code through a Harness-managed worker.
- Providing task context and approved tools.
- Defining execution limits.
- Restricting side effects.
- Recording tool calls.
- Handling approval-required actions in unattended runs.
- Returning structured outcomes to the Harness.

#### **8.12 End-to-End Claude Code Walkthrough**

- Use the Alpha Car Detailing booking requirement.
- Present the task prompt.
- Show a representative tool-call sequence.
- Capture tool outputs.
- Explain tool selection decisions.
- Demonstrate permission enforcement.
- Produce final validation evidence.

### **9. GitHub Copilot Comparison**

#### **9.1 Tool Calling in GitHub Copilot**

- Tool-enabled workflows in supported Copilot environments.
- Workspace access.
- Terminal and file operations.
- Development tool integrations.
- Agent execution models.

#### **9.2 Tool Discovery and Selection**

- Available tools in different supported execution modes.
- Repository context.
- Tool integration capabilities.
- Platform and environment dependencies.

#### **9.3 Tool Permissions and Approvals**

- Permission mechanisms.
- Execution restrictions.
- Human intervention requirements.
- Workspace isolation and protected operations.

#### **9.4 Skills, Instructions, and Tools**

- Relationship between Copilot Instructions and tool usage.
- Reusable workflows.
- Tool behavior under repository guidance.
- Differences from Claude Code's execution model.

#### **9.5 Alpha Car Detailing Comparison**

- Implement the same fleet booking change.
- Search the repository.
- Modify source code.
- Run builds and tests.
- Review permission handling.
- Compare evidence capture.

#### **9.6 Enterprise Considerations**

- Tool integration flexibility.
- Organization-level controls.
- Auditability.
- Suitability for Harness orchestration.
- Differences that materially affect enterprise adoption.

### **10. OpenAI Codex Comparison**

#### **10.1 Tool Calling in Codex**

- Tool-enabled software engineering workflows.
- Repository inspection.
- File editing.
- Command execution.
- Test execution.
- Available execution environments.

#### **10.2 Tool Discovery and Selection**

- Available environment tools.
- Tool invocation behavior.
- Repository context.
- Integration and extension mechanisms.

#### **10.3 Sandbox and Permission Controls**

- Execution isolation.
- Filesystem and network restrictions.
- Approval mechanisms.
- Protected operations.
- Environment-specific access policies.

#### **10.4 Codex Tool Execution Workflow**

- Inspect the Alpha Car Detailing repository.
- Identify affected projects.
- Modify the booking implementation.
- Run targeted validation.
- Capture output and changes.
- Report completion evidence.

#### **10.5 Integration with Enterprise Harnesses**

- Execution orchestration.
- Tool governance.
- Result collection.
- Failure reporting.
- Human approval integration.
- Vendor-neutral execution contracts.

#### **10.6 Cross-Platform Comparison Summary**

Compare Claude Code, GitHub Copilot, and Codex across:

- Tool discovery.
- File access.
- Shell execution.
- Test invocation.
- External integrations.
- Sandboxing.
- Tool permissions.
- Approval boundaries.
- Tool-output handling.
- Non-interactive execution.
- Auditability.
- Harness integration.

Distinguish platform-supported features from enterprise controls that must be implemented separately.

### **11. Best Practices**

#### **11.1 Design Principles**

- Prefer explicit tool contracts.
- Use least privilege.
- Prefer narrow, task-specific tools.
- Default to read-only access where sufficient.
- Validate arguments before execution.
- Enforce policies outside the model.
- Separate tool execution from approval authority.

#### **11.2 Reliable Execution**

- Set operation-specific timeouts.
- Bound retries and backoff.
- Use idempotency keys when applicable.
- Avoid blindly retrying non-idempotent actions.
- Detect partial execution.
- Support safe cancellation.
- Prevent uncontrolled command loops.

#### **11.3 Tool Chaining**

- Define preconditions and postconditions.
- Execute dependent operations in order.
- Parallelize only independent, safe operations.
- Limit tool-call depth and budgets.
- Avoid cycles without progress.
- Stop when failure conditions require human intervention.

#### **11.4 Security**

- Restrict filesystem access.
- Use scoped credentials and managed identities.
- Keep secrets out of prompts, logs, and tool responses.
- Validate untrusted tool outputs.
- Prevent prompt injection through retrieved content.
- Restrict external network access.
- Protect production environments.
- Prevent shell and SQL injection.

#### **11.5 Evidence and Auditability**

- Capture inputs, results, and execution metadata.
- Correlate tool invocations with Harness runs.
- Record denied and approved operations.
- Preserve test and build artifacts.
- Redact sensitive information.
- Keep evidence tamper-resistant where required.
- Follow enterprise retention policies.

#### **11.6 Validation and Quality**

- Never treat exit code zero as proof of business correctness.
- Run required deterministic checks.
- Distinguish tool results from agent conclusions.
- Require independent review for protected changes.
- Make failures visible rather than silently suppressing them.

### **12. Anti-patterns**

#### **12.1 Unrestricted Shell Access**

- Granting agents broad command execution without boundaries.
- Risks of destructive filesystem or infrastructure operations.

#### **12.2 Tool Permission Escalation**

- Allowing the agent to expand its own privileges.
- Treating a user prompt as authorization to bypass policy.

#### **12.3 Blind Tool Chaining**

- Executing dependent steps without inspecting earlier results.
- Continuing after invalid prerequisites.

#### **12.4 Infinite Retry Loops**

- Repeated failed API calls.
- Repeated builds with no corrective change.
- Resource exhaustion and cost growth.

#### **12.5 Unsafe Write Operations**

- Overwriting unrelated files.
- Automatically modifying production configuration.
- Applying database migrations without approval.

#### **12.6 Trusting Tool Output as Instructions**

- Prompt injection embedded in repository comments.
- Malicious content returned by an external API.
- Untrusted MCP responses attempting to alter execution policy.

#### **12.7 Missing Execution Evidence**

- Reporting tests as passed without running them.
- Failing to preserve error output.
- Inability to reconstruct agent actions.

#### **12.8 Exposing Secrets**

- Printing environment variables.
- Logging access tokens.
- Including connection strings in generated reports.

#### **12.9 Confusing Tool Success with Task Completion**

- A successful `dotnet build` does not prove acceptance criteria are satisfied.
- A successful API call does not prove business behavior.
- A completed tool chain does not replace evaluation or approval.

#### **12.10 Tool Sprawl**

- Introducing overlapping tools without ownership.
- Duplicate capabilities with conflicting contracts.
- Unversioned tool definitions.
- Uncontrolled third-party tool integrations.

### **13. Architect’s Notes**

#### **13.1 Tool Calling Is an Execution Boundary**

- The architectural distinction between agent intent and execution authority.
- Why tools should not define their own approval policy.
- The importance of independently enforced controls.

#### **13.2 Design a Vendor-Neutral Tool Abstraction**

- Avoid embedding every business workflow directly into platform-specific tools.
- Define reusable capability contracts.
- Use adapters for Claude Code, GitHub Copilot, and Codex where practical.
- Separate tool interfaces from execution infrastructure.
- Recognize that not every platform capability maps to a common abstraction.

#### **13.3 Tool Calling and Clean Architecture**

- Keep tool execution infrastructure outside domain logic.
- Avoid coupling business rules to an agent runtime.
- Model external execution through explicit interfaces.
- Separate application commands from agent tool commands.

#### **13.4 Tool Calling and the Harness**

- The Harness owns orchestration policy.
- Agents select or request appropriate operations within their authority.
- Execution adapters invoke tools.
- Deterministic gates verify engineering conditions.
- Evaluators assess outcomes.
- Humans retain final approval authority.

#### **13.5 Read-Only Versus Mutating Operations**

- Capability-based permission classification.
- Side effects and external state changes.
- Idempotent and non-idempotent operations.
- Reversible and irreversible actions.
- Approval classification based on actual risk.

#### **13.6 Concurrency and Shared Resources**

- Parallel execution boundaries.
- Concurrent file modification risks.
- Database transaction collisions.
- Workspace ownership.
- Shared build directories and test artifacts.
- Concurrency limits and resource budgets.

#### **13.7 Failure Recovery and Compensation**

- Transaction boundaries.
- Partial success.
- Rollback strategies.
- Compensating operations.
- Recovery after process interruption.
- Human escalation for ambiguous external outcomes.

#### **13.8 Tool Calling, Scratchpad, and Harness Memory**

- Scratchpad as temporary execution state.
- Tool outputs as evidence.
- Distinguishing observations from authoritative repository knowledge.
- Promoting verified information into Repository Intelligence.
- Retaining only approved, useful learning in Harness Memory.
- Preventing transient tool output from silently changing standards.

#### **13.9 Designing for Observability**

- Structured logging.
- Correlation IDs.
- Execution traces.
- Tool latency.
- Failure rates.
- Retry counts.
- Permission denials.
- OpenTelemetry integration.
- Tool execution cost and token consumption where measurable.

#### **13.10 Operational Scalability**

- Tool execution worker pools.
- Rate limiting.
- Resource quotas.
- Execution queues.
- Workspace isolation.
- Large-output handling.
- Distributed tracing across Harness workers.

### **14. Enterprise Tips**

#### **14.1 Establish a Tool Capability Catalog**

- Maintain an approved tool inventory.
- Define business purpose and ownership.
- Record permissions and side effects.
- Identify supported environments.
- Establish a retirement process for obsolete tools.

#### **14.2 Introduce Risk-Based Tool Classification**

- Read-only development tools.
- Repository modification tools.
- External-system mutation tools.
- Privileged infrastructure tools.
- Prohibited operations.

#### **14.3 Enforce Environment Separation**

- Local development.
- CI validation.
- Shared development.
- Staging.
- Production.
- Distinct credentials and approval policies for each.

#### **14.4 Apply Enterprise Identity Controls**

- Managed identities.
- Workload identities.
- Short-lived credentials.
- Role-based access control.
- Service-specific authorization.
- Credential rotation.

#### **14.5 Protect the Software Supply Chain**

- Trusted tool publishers.
- Version pinning.
- Signed or otherwise verified components where supported.
- Dependency vulnerability scanning.
- MCP server trust assessment.
- Execution environment hardening.

#### **14.6 Build Reusable Tool Adapters**

- Repository search adapter.
- .NET build adapter.
- Test execution adapter.
- Architecture validation adapter.
- SQL Server read-only adapter.
- Azure resource validation adapter.

#### **14.7 Establish Tool Execution Service-Level Objectives**

- Maximum execution duration.
- Failure classification.
- Expected availability.
- Resource consumption limits.
- Recovery and escalation policies.

#### **14.8 Align Tool Governance with Enterprise Compliance**

- Audit retention.
- Access reviews.
- Separation of duties.
- Approval records.
- Sensitive data handling.
- Traceability of code and infrastructure changes.

#### **14.9 Measure Tool Effectiveness**

- Tool success and failure rates.
- Average invocation latency.
- Retry frequency.
- Number of unnecessary tool calls.
- Time spent building and testing.
- Permission denial patterns.
- Successful task completion after validation.

#### **14.10 Standardize Tool Evidence Contracts**

- Define a common result envelope.
- Include status, timing, correlation identifiers, and artifact references.
- Support consistent evidence consumption across agents.
- Ensure execution evidence is reproducible where feasible.

### **15. Decision Points**

Present each decision as an enterprise engineering trade-off, including alternatives, selection criteria, consequences, and recommended circumstances.

| Decision                 | Alternatives to evaluate                                        |
| ------------------------ | --------------------------------------------------------------- |
| Tool execution ownership | Direct agent execution versus Harness-controlled execution      |
| Tool interface design    | Generic shell versus specialized tools                          |
| Permission strategy      | Broad access versus least-privilege capability access           |
| Filesystem modification  | Direct editing versus controlled patch application              |
| Execution environment    | Developer workstation versus isolated sandbox                   |
| External integration     | Native integration versus MCP-based adapter                     |
| Database access          | Direct SQL tools versus constrained data-access services        |
| Cloud operations         | Read-only inspection versus privileged mutation                 |
| Approval policy          | Per-call approval versus policy-authorized bounded operations   |
| Tool chaining            | Agent-selected sequences versus predefined Harness workflows    |
| Retry strategy           | Immediate retry versus bounded exponential backoff              |
| Evidence collection      | Agent summaries versus independently captured execution records |
| Validation ownership     | Agent self-checks versus independent Validator gates            |
| Tool abstraction         | Platform-specific integration versus vendor-neutral contracts   |
| Failure recovery         | Resume, rollback, compensate, or escalate                       |

Include a decision tree showing when an agent may execute a tool automatically, when approval is required, and when execution must be denied.

### **16. Exercises**

#### **Exercise 26.1 — Repository Discovery**

Use Claude Code to identify booking-related files in Alpha Car Detailing without modifying the repository.

**Deliverable:** Repository discovery report with paths and evidence.

#### **Exercise 26.2 — Controlled File Modification**

Implement a small change to fleet booking validation while restricting modifications to approved source and test files.

**Deliverable:** Source diff and change summary.

#### **Exercise 26.3 — Build and Test Execution**

Run `dotnet build`, targeted unit tests, and the required regression validation.

**Deliverable:** Build and test evidence report.

#### **Exercise 26.4 — Design a Tool Permission Matrix**

Classify repository read, file write, shell execution, database query, deployment validation, and deployment execution capabilities.

**Deliverable:** Permission matrix with risk levels and approval requirements.

#### **Exercise 26.5 — Implement a Read-Only Database Tool**

Design a constrained tool that retrieves fleet booking information from a development SQL Server database.

**Deliverable:** Tool contract, security restrictions, sample inputs, and expected outputs.

#### **Exercise 26.6 — Implement a Tool Execution Adapter**

Create a .NET 10 abstraction for executing permitted commands with:

- Input validation.
- Timeout.
- Cancellation.
- Output capture.
- Exit-code handling.
- Structured execution results.

**Deliverable:** A reusable C# tool execution component and automated tests.

#### **Exercise 26.7 — Build a Tool Chain**

Create the following controlled workflow:

`Search → Read → Edit → Build → Test → Architecture Check → Evidence`

**Deliverable:** Execution sequence, failure branches, and artifact references.

#### **Exercise 26.8 — Handle Tool Failure**

Simulate a timeout, invalid command, unavailable API, and interrupted operation.

**Deliverable:** Failure classification and recovery report.

#### **Exercise 26.9 — Human Approval**

Introduce an approval requirement before a simulated privileged database or cloud operation.

**Deliverable:** Approval policy and blocked/approved execution evidence.

#### **Exercise 26.10 — MCP Tool Integration**

Expose a constrained, read-only Alpha Car Detailing development capability through a trusted MCP server.

**Deliverable:** Tool schema, integration configuration, authorization rules, and demonstration.

#### **Exercise 26.11 — Harness Integration**

Integrate the .NET tool execution adapter with the existing Harness and enforce a deterministic validation gate.

**Deliverable:** Harness execution trace and validation result.

#### **Exercise 26.12 — Security Assessment**

Review a deliberately unsafe tool workflow for excessive permissions, unsafe command construction, leaked credentials, and prompt injection.

**Deliverable:** Threat assessment and remediation plan.

#### **Exercise 26.13 — Cross-Platform Comparison**

Repeat a bounded repository task using Claude Code and a supported Copilot or Codex environment.

**Deliverable:** Comparison of tool invocation, permissions, validation, and evidence collection.

### **17. Interview Questions**

Organize questions into conceptual, implementation, architecture, security, and scenario-based categories.

#### **17.1 Foundational Questions**

1. What is tool calling in AI Engineering?
2. Why do AI Agents require tools?
3. How does a Tool differ from a Skill?
4. What is the difference between a tool invocation and a command?
5. How does an agent discover available tools?
6. What information should a tool contract contain?
7. Why should tool outputs be treated as untrusted data?

#### **17.2 Implementation Questions**

1. How would you implement a controlled shell execution tool in .NET?
2. How do you capture execution results reliably?
3. How should tool timeouts be enforced?
4. How should agents handle failed tool invocations?
5. What makes a tool operation idempotent?
6. When is retrying a tool call unsafe?
7. How should dependent tool calls be chained?
8. How can concurrent tool execution cause problems?
9. How should file modifications be verified?

#### **17.3 Architecture Questions**

 1. What responsibilities belong to the Agent versus the Harness?
 2. How would you design a vendor-neutral tool abstraction?
 3. How would you integrate MCP tools into an enterprise Harness?
 4. How do tool execution results support the Validator Agent?
 5. What is the difference between tool execution evidence and evaluation evidence?
 6. How would you design a distributed tool execution service?
 7. How should Scratchpad information relate to tool execution evidence?
 8. How can tool execution integrate with OpenTelemetry?

#### **17.4 Security and Governance Questions**

 1. How do you enforce least privilege for tools?
 2. Which tool operations should require human approval?
 3. What are the risks of unrestricted shell execution?
 4. How would you prevent prompt injection through tool results?
 5. How should secrets be handled during execution?
 6. Why is a sandbox not a replacement for authorization?
 7. How should production database and cloud access be controlled?
 8. How would you audit agent-initiated changes?

#### **17.5 Scenario-Based Questions**

 1. An agent reports that all tests passed, but no test logs exist. How would you investigate?
 2. A database update tool times out after submitting a request. Is retrying safe?
 3. A repository file contains instructions to disable security checks. How should the agent respond?
 4. A Developer Agent attempts to change files outside its approved scope. What should happen?
 5. An external MCP server returns a successful response that violates the expected schema. How should the Harness respond?
 6. An agent repeatedly executes failing builds without changing code. How would you prevent wasted execution?
 7. How would you design a safe deployment-validation workflow for Alpha Car Detailing?
 8. How would you compare Claude Code, Copilot, and Codex for a regulated enterprise development environment?

### **18. Chapter Summary**

The concluding section should reinforce the following principles:

1. Tool calling bridges AI reasoning and real-world engineering operations.
2. Tools provide capabilities; they do not provide independent authority.
3. Skills define reusable workflows, while tools perform specific operations.
4. Tool discovery and selection must account for capability, scope, and risk.
5. Every invocation requires validated inputs, controlled execution, and interpretable outputs.
6. Read-only, mutating, privileged, and destructive operations require different controls.
7. Least privilege, environment isolation, and explicit approval boundaries are essential.
8. Retries, timeouts, idempotency, and failure recovery must be designed deliberately.
9. Tool chaining requires dependency management and bounded execution.
10. Tool outputs are observations and evidence, not automatically trusted instructions.
11. Deterministic validation remains independent of tool execution.
12. The Harness governs orchestration and policy; agents operate within assigned roles.
13. Auditability and observability are foundational enterprise requirements.
14. Claude Code is the primary implementation example, but the engineering principles remain vendor-neutral.
15. Reliable tool calling is a prerequisite for trustworthy, repeatable, and scalable enterprise AI Engineering.

**Closing architectural principle:**

> An enterprise AI Agent should never be trusted merely because it can execute a tool. Trust comes from constrained authority, controlled execution, independently verified evidence, and accountable approval.

### **19. Further Reading**

Organize references into five categories, prioritizing official documentation and authoritative engineering sources.

#### **19.1 Claude Code**

- Official Claude Code documentation.
- Tool usage and permission configuration.
- Claude Code security and sandboxing.
- Claude Code hooks.
- Claude Code MCP integration.
- Programmatic and automated execution.

#### **19.2 Model Context Protocol**

- MCP architecture.
- Tool definitions and schemas.
- Tool discovery.
- MCP security and authorization.
- Building custom MCP servers.
- Enterprise integration patterns.

#### **19.3 GitHub Copilot and OpenAI Codex**

- Official GitHub Copilot agent and tool documentation.
- Copilot workspace and execution permissions.
- Official OpenAI Codex tooling documentation.
- Codex execution environments and approval controls.
- Platform security guidance.

#### **19.4 .NET and Azure Engineering**

- .NET process execution APIs.
- `System.Diagnostics.Process`.
- .NET cancellation and timeout patterns.
- ASP.NET Core HTTP clients and resilience.
- SQL Server least-privilege access.
- Azure managed identities.
- Azure RBAC.
- Azure Key Vault.
- OpenTelemetry for .NET.
- Secure CI/CD execution with GitHub Actions and Azure DevOps.

#### **19.5 Enterprise Security and Architecture**

- NIST secure software development guidance.
- OWASP guidance for LLM applications and prompt injection.
- OWASP guidance for agentic applications.
- Zero Trust Architecture.
- Least-privilege security design.
- Software supply chain security.
- Audit logging and traceability.
- Resilient distributed systems.
- Idempotent processing patterns.

## **Chapter Integration and Editorial Checklist**

The following points should be checked when writing the full chapter:

- **Chapter 7 — Skills:** Preserve the distinction between a reusable implementation workflow and a tool operation.
- **Chapter 9 — Roles:** Respect the authority of Lead, Developer, Reviewer, Validator, and Evaluator Agents.
- **Chapter 12 — What Is a Harness?:** Build on the established concepts of deterministic gates, Hooks, and automated guardrails.
- **Chapter 13 — Harness Architecture:** Preserve the principle that the Harness controls execution and agents implement tasks.
- **Chapters 16–18 — Reviewer, Validator, Evaluator:** Avoid assigning independent validation, evaluation, or approval authority to tool execution.
- **Chapter 25 — Claude Memory:** Distinguish temporary execution results and evidence from durable memory.
- **Chapter 27 — Planning Mode:** Establish tool calling as the operational foundation for the planning and execution workflows discussed next.
- **Part V — Enterprise AI Governance:** Introduce tool security and approval boundaries here without duplicating the later dedicated governance chapters.

All diagrams should use consistent terminology and identifiers, all hands-on examples should align with the Alpha Car Detailing .NET 10 architecture, and the completed chapter should retain the frozen 19-section structure.

**Chapter 26 outline is complete.**
