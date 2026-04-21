---
title: Testing Integration and Reliability Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the integration and reliability-focused testing concepts from the interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Reliability Map

```concept-card
id: api-integration-testing
term: API Integration Testing
children:
- webapplicationfactory
- testcontainers
- wiremock
- test-authentication-handler
- migration-testing
- test-suite-throughput
summary:
API integration testing verifies full application behavior against realistic infrastructure and boundaries.
details:
It is the layer where routing, serialization, auth, databases, and external-service contracts are exercised together instead of in isolation.
example:
Spin up the app with `WebApplicationFactory`, run PostgreSQL in Testcontainers, and fake downstream HTTP with WireMock.
mnemonic:
Host the app, wire the real boundaries, fake only the outside.
recall:
- What makes integration testing different from pure unit testing?
- Which dependencies should be real versus faked?
```

```concept-card
id: webapplicationfactory
term: WebApplicationFactory
parents:
- api-integration-testing
summary:
`WebApplicationFactory` hosts the ASP.NET Core app in tests so the full request pipeline can be exercised.
details:
It gives tests an in-memory host with realistic middleware, routing, binding, and DI behavior.
example:
Use `CreateClient()` to issue real HTTP requests against the in-test host.
mnemonic:
Run the app inside the test.
recall:
- What does `WebApplicationFactory` let you prove?
- Why is it better than calling controller methods directly for integration coverage?
```

```concept-card
id: testcontainers
term: Testcontainers
parents:
- api-integration-testing
summary:
Testcontainers runs disposable real infrastructure such as databases in containers during tests.
details:
It gives higher confidence than fake providers because it exercises migrations, SQL behavior, constraints, and provider-specific quirks.
example:
Run PostgreSQL in a container for the integration suite instead of using EF Core's in-memory provider.
mnemonic:
Disposable real infra beats fake confidence.
recall:
- Why are Testcontainers better than fake DB providers for integration tests?
- Which production failures can they catch early?
```

```concept-card
id: wiremock
term: WireMock
parents:
- api-integration-testing
summary:
WireMock is a fake HTTP server used to simulate external API behavior during tests.
details:
It lets tests verify request shape, retries, timeouts, and failure handling without calling real external services.
example:
Stub `/api/charges` to return `200`, `500`, or a 30-second delay depending on the scenario.
mnemonic:
Fake the HTTP boundary, not the client internals.
recall:
- Why is a fake HTTP server better than mocking raw `HttpClient` in many cases?
- What kinds of failure scenarios can WireMock simulate?
```

```concept-card
id: test-authentication-handler
term: Test Authentication Handler
parents:
- api-integration-testing
summary:
A test authentication handler creates predictable identities for integration tests while keeping authorization enabled.
details:
It lets tests vary roles and claims without depending on real token issuance or cryptographic validation.
example:
Return a principal with `Role = Admin` for one test and `Role = User` for another.
mnemonic:
Fake the identity, keep the auth rules.
recall:
- Why is replacing auth with a test handler better than disabling authorization entirely?
- What types of scenarios does this make easy to test?
```

```concept-card
id: migration-testing
term: Migration Testing
parents:
- api-integration-testing
summary:
Migration testing proves that EF Core migrations apply successfully to a real database.
details:
It catches schema drift, broken SQL, and provider-specific issues before deployment pipelines hit them.
example:
Create a fresh PostgreSQL container, run `Database.MigrateAsync()`, and verify the schema is usable.
mnemonic:
Migrate in CI before production has to.
recall:
- Why should migrations be tested against a real database engine?
- What failures can migration tests catch early?
```

```concept-card
id: test-suite-throughput
term: Test Suite Throughput
parents:
- api-integration-testing
summary:
Test suite throughput is the speed and feedback quality of the integration suite as it grows.
details:
Shared containers, database reset strategies, and safe parallelization can keep suites fast without reducing what they prove.
example:
Reuse one database container and reset state with Respawn instead of rebuilding the whole database for every test class.
mnemonic:
Reuse expensive setup, reset cheap state.
recall:
- What usually makes integration suites slow at scale?
- Why is speeding a suite up different from changing it into a different kind of test?
```
