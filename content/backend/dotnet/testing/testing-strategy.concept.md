---
title: Testing Strategy Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core testing strategy concepts behind the testing interview topic.

Study pages: [Section Index](index.md) | [Junior Questions](junior.interview.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Strategy Map

```concept-card
id: api-testing-strategy
term: API Testing Strategy
children:
- aaa-pattern
- xunit-and-fluentassertions
- unit-versus-integration-tests
- snapshot-testing
- architecture-tests
summary:
API testing strategy defines how a team proves correctness, structure, and behavior at different levels of confidence.
details:
Good test strategy is not about maximizing test count. It is about choosing the right kind of test for the kind of risk being checked.
example:
Use unit tests for discount rules, integration tests for auth plus database behavior, and architecture tests for dependency rules.
mnemonic:
Test the risk at the right level.
recall:
- Why is test strategy about fit rather than volume?
- What kinds of risks belong to unit, integration, or architecture tests?
```

```concept-card
id: aaa-pattern
term: AAA Pattern
parents:
- api-testing-strategy
summary:
The AAA pattern structures tests into Arrange, Act, and Assert phases.
details:
It improves readability by separating setup, execution, and verification so each test's intent is easy to follow.
example:
Arrange a `CreateProductRequest`, Act by calling the endpoint, Assert on `201 Created` and returned payload.
mnemonic:
Set up, do it, prove it.
recall:
- What belongs in each AAA section?
- Why does AAA improve test maintainability?
```

```concept-card
id: xunit-and-fluentassertions
term: xUnit and FluentAssertions
parents:
- api-testing-strategy
summary:
`xUnit` and `FluentAssertions` are a common .NET testing stack for readable tests and clear failure diagnostics.
details:
`xUnit` provides the execution model and fixtures, while `FluentAssertions` makes verification expressive and easier to read than many low-level asserts.
example:
`response.StatusCode.Should().Be(HttpStatusCode.Created);`
mnemonic:
One runs the test, one explains the expectation.
recall:
- What role does `xUnit` play versus `FluentAssertions`?
- Why do good assertion messages matter?
```

```concept-card
id: unit-versus-integration-tests
term: Unit Versus Integration Tests
parents:
- api-testing-strategy
summary:
Unit tests isolate logic, while integration tests prove real wiring across framework and infrastructure boundaries.
details:
Choosing the wrong level can either give false confidence or waste effort testing framework behavior in unrealistic ways.
example:
Unit test a pricing rule, but integration test the request pipeline, database access, and auth behavior together.
mnemonic:
Logic alone in unit, real wiring in integration.
recall:
- What kinds of behavior fit unit tests best?
- What kinds of risks need integration tests instead?
```

```concept-card
id: snapshot-testing
term: Snapshot Testing
parents:
- api-testing-strategy
summary:
Snapshot testing compares current output to a stored expected snapshot to detect unintended changes.
details:
It is especially helpful for response shapes, OpenAPI specs, and other outputs where full-document diffs are clearer than many small assertions.
example:
Store the JSON for `GET /api/products/1` and review diffs when the contract changes.
mnemonic:
Freeze the shape, diff the future.
recall:
- When is snapshot testing more useful than explicit field-by-field asserts?
- What kinds of output make poor snapshot candidates?
```

```concept-card
id: architecture-tests
term: Architecture Tests
parents:
- api-testing-strategy
summary:
Architecture tests enforce structural rules such as layer boundaries, naming rules, and endpoint conventions.
details:
They keep architectural intent executable so the codebase does not drift silently over time.
example:
Assert that the domain layer does not reference EF Core or that every endpoint requires authorization.
mnemonic:
Turn architecture rules into failing tests.
recall:
- What do architecture tests catch that normal behavior tests do not?
- Why are they better than relying only on code review?
```
