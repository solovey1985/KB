---
title: Testing Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level testing scenarios from the Web API interview question set.

Relevant concept maps:

- [Test Strategy Concept Map](testing-strategy.concept.md)
- [Integration and Reliability Concept Map](testing-integration-reliability.concept.md)

## Full Integration Setup

```interview-question
How do you write integration tests for an API that depends on a database and external HTTP services?
---
answer:
Use `WebApplicationFactory` for the application host, `Testcontainers` for a real database, and a fake HTTP server such as WireMock for external services.

This gives you realistic infrastructure behavior without depending on shared external environments.

The goal is to test the full application pipeline with deterministic dependencies.
hints:
- One tool hosts the app.
- One tool gives you a real ephemeral database.
- One tool fakes downstream HTTP services.
```

Related concepts: [WebApplicationFactory](testing-integration-reliability.concept.md#webapplicationfactory), [Testcontainers](testing-integration-reliability.concept.md#testcontainers), [WireMock](testing-integration-reliability.concept.md#wiremock)

## Migration Testing

```interview-question
How do you test that your EF Core migrations actually work against a real database?
---
answer:
Run migrations against a real disposable database in CI, usually through Testcontainers.

The test should verify that migrations apply successfully, that the resulting schema is usable, and that no pending model changes remain unexpectedly.

This catches migration problems before they break deployments.
hints:
- Real database, not just the in-memory provider.
- Apply all migrations from scratch.
- Schema usability should be verified too.
```

Related concepts: [Migration Testing](testing-integration-reliability.concept.md#migration-testing)

## Large Suite Performance

```interview-question
Your team has 500 integration tests and they take 15 minutes to run. How do you speed them up?
---
answer:
Reuse expensive infrastructure where possible, parallelize test execution safely, and reset database state efficiently instead of rebuilding everything for every test.

Techniques such as shared containers, Respawn-based cleanup, and avoiding unnecessary global setup usually matter more than rewriting everything as unit tests.

The goal is faster feedback without losing the confidence that integration tests provide.
hints:
- Container startup is expensive.
- Database reset strategy matters.
- Speeding tests up should not change what they prove.
```

Related concepts: [Test Suite Throughput](testing-integration-reliability.concept.md#test-suite-throughput)

## External API Testing

```interview-question
How do you test an endpoint that calls an external HTTP API such as a payment gateway?
---
answer:
Use a fake HTTP server such as WireMock and configure the application under test to call that fake server.

This lets the test verify headers, body shape, timeouts, retries, and error handling without calling the real provider.

Mocking raw `HttpClient` behavior directly is usually less realistic and more coupled to implementation details.
hints:
- Fake the dependency at the HTTP boundary.
- The test should still exercise serialization and retry logic.
- Real external calls should not happen in normal CI.
```

Related concepts: [WireMock](testing-integration-reliability.concept.md#wiremock)
