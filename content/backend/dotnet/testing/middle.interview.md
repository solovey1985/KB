---
title: Testing Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level testing trade-offs from the Web API interview question set.

Relevant concept maps:

- [Test Strategy Concept Map](testing-strategy.concept.md)
- [Integration and Reliability Concept Map](testing-integration-reliability.concept.md)

## Test Scope

```interview-question
What should you test with unit tests versus integration tests in an API project?
---
answer:
Unit tests are best for business rules, validation logic, and pure logic with small boundaries.

Integration tests are best for the full request-to-response path, database behavior, auth pipeline, serialization, and framework wiring.

Do not unit test framework behavior or pretend the in-memory database provider proves a real EF Core query works against production infrastructure.
hints:
- One test type is narrow and logic-focused.
- The other proves real wiring and infrastructure behavior.
- Framework plumbing usually belongs in integration tests.
```

Related concepts: [Unit Versus Integration Tests](testing-strategy.concept.md#unit-versus-integration-tests)

```interview-choice
Which kind of test is the better fit for verifying routing, auth, JSON serialization, and real database access together?
---
options:
- Unit test
- Integration test
- Snapshot test only
correct: 1
explanation:
Those behaviors cross framework and infrastructure boundaries, so they are better covered by integration tests.
```

## Auth Testing

```interview-question
How do you test an endpoint that requires authentication in your integration tests?
---
answer:
Register a test authentication handler that produces a `ClaimsPrincipal` with the roles or claims the scenario needs.

This keeps authorization enabled while letting tests control identity shape without depending on real JWT validation.

You should also include tests proving that missing or insufficient auth still returns `401` or `403` when expected.
hints:
- Do not remove authorization just because it is test code.
- Fake the authentication handler, not the whole security model.
- Different claims should be testable.
```

Related concepts: [Test Authentication Handler](testing-integration-reliability.concept.md#test-authentication-handler)

## Snapshot Testing

```interview-question
What is snapshot testing and when is it useful for API testing?
---
answer:
Snapshot testing stores a known-good output and compares future test runs against it.

It is useful for response shapes, serialized JSON contracts, and OpenAPI documents where a full output diff is often more useful than many tiny assertions.

It is a weak fit for highly unstable output unless non-deterministic values are scrubbed.
hints:
- The expected output is stored outside the test code.
- It is especially useful for bigger response shapes.
- Stable outputs work best.
```

Related concepts: [Snapshot Testing](testing-strategy.concept.md#snapshot-testing)

## Architecture Tests

```interview-question
What are architecture tests and why would you add them to your project?
---
answer:
Architecture tests verify structural rules such as dependency directions, naming conventions, or required authorization markers.

They turn architectural rules into executable checks instead of relying only on code review.

This helps keep the codebase consistent as it grows.
hints:
- They test structure, not business behavior.
- They are useful for layering and conventions.
- They automate rules humans often forget.
```

Related concepts: [Architecture Tests](testing-strategy.concept.md#architecture-tests)
