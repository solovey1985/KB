---
title: Architecture and System Design Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level architecture and system design trade-offs from the Web API interview question set.

Relevant concept maps:

- [Application Architecture Concept Map](architecture-application-patterns.concept.md)
- [Distributed Systems Concept Map](architecture-distributed-systems.concept.md)

## Vertical Slice Versus Clean Architecture

```interview-question
What is Vertical Slice Architecture and how does it differ from Clean Architecture?
---
answer:
Vertical Slice Architecture organizes code by feature so each use case lives mostly in one place. Clean Architecture organizes code by technical layer such as Domain, Application, Infrastructure, and API.

Vertical Slice reduces feature scattering and is often simpler for API-focused systems. Clean Architecture is stronger when the domain is complex and the team benefits from stricter layer separation.

The best choice depends on the shape of the domain and the cost of abstractions, not on a universal rule.
hints:
- One groups by feature.
- One groups by layer.
- The trade-off is navigability versus layered separation.
```

Related concepts: [Vertical Slice Architecture](architecture-application-patterns.concept.md#vertical-slice-architecture), [Clean Architecture](architecture-application-patterns.concept.md#clean-architecture)

```interview-choice
Which architecture usually lets a developer understand a single feature by opening one feature-focused file or folder rather than tracing across many layers?
---
options:
- Vertical Slice Architecture
- Clean Architecture only
- Repository Pattern
correct: 0
explanation:
Vertical Slice Architecture groups the code for a use case together, which reduces the cross-layer navigation needed for feature work.
```

## Monolith Versus Microservices

```interview-question
How do you decide between a monolith and microservices for a new project?
---
answer:
Start with a monolith unless you have strong reasons not to. A monolith is usually the right default for small teams, unclear domain boundaries, and projects where delivery speed matters more than independent deployment.

Microservices are justified when team boundaries, scaling needs, deployment independence, and operational maturity make the extra complexity worth paying for.

A modular monolith is often the best middle ground because it keeps boundaries explicit without taking on distributed-system costs too early.
hints:
- Microservices solve some problems but create many others.
- Team size and domain clarity matter.
- Modular monolith is the common middle ground.
```

Related concepts: [Monolith Versus Microservices](architecture-distributed-systems.concept.md#monolith-versus-microservices), [Modular Monolith](architecture-distributed-systems.concept.md#modular-monolith)

## Background Service Versus Queue

```interview-question
What is the difference between `BackgroundService` and a message queue for background processing?
---
answer:
`BackgroundService` runs inside the API process, so it is simple and low overhead but does not survive process restarts with durable queued work.

A message queue adds infrastructure and complexity, but it gives stronger durability, scaling, and independent worker processing.

Use `BackgroundService` for simpler or less critical background work. Use a queue when reliability, durability, and horizontal scaling matter.
hints:
- One lives inside the app process.
- The other introduces separate infrastructure.
- Reliability is the main trade-off.
```

Related concepts: [BackgroundService](architecture-distributed-systems.concept.md#backgroundservice), [Message Queue](architecture-distributed-systems.concept.md#message-queue)

## Mediator Pattern

```interview-question
What is the Mediator pattern, and why do some .NET developers love it while others avoid it?
---
answer:
The Mediator pattern routes requests through a mediator rather than calling handlers or services directly.

Developers like it because it centralizes cross-cutting behaviors and keeps endpoints thin. Others avoid it because it adds indirection, makes navigation harder, and can feel like ceremony in simpler applications.

It is a tool, not a requirement, and its value depends on whether the extra abstraction pays for itself.
hints:
- The pattern introduces an extra indirection point.
- Pipeline behaviors are a major benefit.
- Simpler systems may not need the ceremony.
```

Related concepts: [Mediator Pattern](architecture-application-patterns.concept.md#mediator-pattern)
*** Add File: content/backend/dotnet/architecture-system-design/senior.interview.md
---
title: Architecture and System Design Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level architecture and system design scenarios from the Web API interview question set.

Relevant concept maps:

- [Application Architecture Concept Map](architecture-application-patterns.concept.md)
- [Distributed Systems Concept Map](architecture-distributed-systems.concept.md)

## Repository Pattern with EF Core

```interview-question
Your team is debating whether to use the repository pattern with EF Core. What is your answer?
---
answer:
In many applications, a generic repository over EF Core adds little value because `DbContext` and `DbSet<T>` already provide repository and unit-of-work behavior.

A thin generic abstraction often leaks EF Core concepts without reducing real coupling. Focused repositories or query services can still make sense when they encapsulate meaningful domain or query boundaries.

The right answer is not "always" or "never" but "use an abstraction only when it carries real value."
hints:
- Generic repositories often duplicate what `DbSet<T>` already does.
- Focused abstractions can still be useful.
- The nuanced answer is better than either extreme.
```

Related concepts: [Repository Pattern Trade-Off](architecture-application-patterns.concept.md#repository-pattern-trade-off)

## CQRS

```interview-question
Explain CQRS. When is it overkill and when is it essential?
---
answer:
CQRS separates read models and write models so each side can be optimized for its own job.

It is useful when read and write workloads differ significantly, when the domain model is complex but the read model is simple, or when different data stores and event-driven flows are involved.

It is overkill for straightforward CRUD systems where read and write shapes are nearly identical and the extra separation only adds ceremony.
hints:
- The core idea is read/write separation.
- Different workloads justify different models.
- Simple CRUD often does not need the split.
```

Related concepts: [CQRS](architecture-application-patterns.concept.md#cqrs)

```interview-choice
Which situation makes CQRS more attractive?
---
options:
- A tiny CRUD API with nearly identical read and write models
- A system with very different read and write workloads and specialized read models
- A project where no one wants additional abstractions
correct: 1
explanation:
CQRS pays off when the read side and write side have materially different needs and forcing them into one model becomes awkward.
```

## Service-to-Service Resilience

```interview-question
You are building a microservice that communicates with several other services. How do you handle failures when one service is down?
---
answer:
Use resilience policies such as retries with backoff, timeouts, and circuit breakers, and design for graceful degradation where possible.

Do not retry blindly in a loop. The client should fail fast when appropriate, stop hammering unhealthy services, and return partial or fallback data if the business flow allows it.

Separate HTTP clients and observability also matter so one failing dependency does not poison the rest of the system.
hints:
- Resilience is both technical policy and business behavior.
- Retry without limits can make outages worse.
- Some failures should degrade gracefully instead of crashing the whole request.
```

Related concepts: [Resilience Patterns](architecture-distributed-systems.concept.md#resilience-patterns), [Graceful Degradation](architecture-distributed-systems.concept.md#graceful-degradation)

## Outbox Pattern

```interview-question
OrderService must notify InventoryService when an order is placed. How do you ensure the event is delivered reliably?
---
answer:
Use the outbox pattern. Save the order and an outbox message in the same database transaction, then let a background worker publish pending outbox messages to the broker.

This avoids the dual-write problem where the database write succeeds but the message publish fails, or vice versa.

The consumer side must still be idempotent because delivery is usually at least once rather than exactly once.
hints:
- The database write and message publish should not be separate uncoordinated steps.
- The outbox record is the durable handoff.
- Reliable delivery still does not mean no duplicates.
```

Related concepts: [Outbox Pattern](architecture-distributed-systems.concept.md#outbox-pattern), [At-Least-Once Delivery](architecture-distributed-systems.concept.md#at-least-once-delivery)

## Multi-Tenancy

```interview-question
How do you design an API that supports multi-tenancy with isolated customer data?
---
answer:
Choose a tenancy model based on isolation and operational needs: database-per-tenant for strongest isolation, schema-per-tenant for a middle ground, or shared database with tenant ID for the most common SaaS default.

In shared-database designs, tenant resolution and enforcement must be centralized, often through middleware and global query filters.

The critical rule is to never trust a client-supplied tenant identifier without verifying it against the authenticated user's tenant membership.
hints:
- There are multiple isolation strategies.
- Shared database is common but must be enforced centrally.
- Trusting tenant IDs from the client alone is a security bug.
```

Related concepts: [Multi-Tenancy Strategies](architecture-distributed-systems.concept.md#multi-tenancy-strategies), [Tenant Isolation](architecture-distributed-systems.concept.md#tenant-isolation)

## Feature Flags

```interview-question
How would you design a feature flag system for an API?
---
answer:
Use a feature management system that supports boolean flags, targeted rollout, percentage rollout, and time-based enablement.

Flags should be dynamically reloadable without redeploying, and stale flags should be cleaned up after the rollout completes so the codebase does not accumulate dead branches.

Testing must cover both the enabled and disabled paths for critical behavior.
hints:
- The design is not only on or off.
- Dynamic updates are usually important.
- Old flags should not stay forever.
```

Related concepts: [Feature Flags](architecture-application-patterns.concept.md#feature-flags)
*** Add File: content/backend/dotnet/architecture-system-design/architecture-application-patterns.concept.md

