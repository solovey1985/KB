---
title: Architecture Distributed Systems Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the distributed-systems and deployment-focused concepts from the architecture and system design interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Distributed Map

```concept-card
id: distributed-system-design
term: Distributed System Design
children:
- monolith-versus-microservices
- modular-monolith
- backgroundservice
- message-queue
- resilience-patterns
- graceful-degradation
- outbox-pattern
- at-least-once-delivery
- multi-tenancy-strategies
- tenant-isolation
summary:
Distributed system design focuses on how services communicate, fail, persist work, and isolate customers as the system grows.
details:
These decisions drive operational complexity more than local code structure does, so they must be made with realism about scale, failure, and team capability.
example:
Adding one network hop, one broker, and one more database can increase deployment, observability, and failure-handling complexity far beyond a local refactor.
mnemonic:
Distribute only what you can operate well.
recall:
- Which concerns become dominant once a system crosses process boundaries?
- Why do distributed design choices cost more than local architectural choices?
```

```concept-card
id: monolith-versus-microservices
term: Monolith Versus Microservices
parents:
- distributed-system-design
related:
- modular-monolith
summary:
Monolith versus microservices is the decision between one deployable application and many independently deployed services.
details:
Monoliths are usually the safer default, while microservices make sense when deployment independence, scaling differences, and team boundaries justify the complexity.
example:
An early-stage product team of four usually moves faster in one modular monolith than in six independently deployed services.
mnemonic:
Start simple, split only with reason.
recall:
- Why is a monolith usually the better starting point?
- What conditions make microservices worth their cost?
```

```concept-card
id: modular-monolith
term: Modular Monolith
parents:
- distributed-system-design
related:
- monolith-versus-microservices
summary:
A modular monolith is a single deployable application with strong internal module boundaries.
details:
It gives teams a practical middle ground: clear domain separation without immediately taking on network, consistency, and deployment complexity.
example:
`Modules/Orders`, `Modules/Inventory`, and `Modules/Billing` can stay in one process while keeping clear internal boundaries.
mnemonic:
One deployment, many clean modules.
recall:
- Why is a modular monolith often a strong middle ground?
- How does it help if extraction to services is needed later?
```

```concept-card
id: backgroundservice
term: BackgroundService
parents:
- distributed-system-design
related:
- message-queue
summary:
`BackgroundService` is in-process background execution hosted inside the application.
details:
It is simple and useful for moderate background work, but it does not provide the same durability and independent scaling as a separate message queue and worker setup.
example:
Sending welcome emails in a `BackgroundService` is usually fine, but payment settlement work often needs stronger guarantees.
mnemonic:
Easy to start, easier to lose.
recall:
- What makes `BackgroundService` attractive?
- What reliability limits does it have?
```

```concept-card
id: message-queue
term: Message Queue
parents:
- distributed-system-design
related:
- backgroundservice
- outbox-pattern
summary:
A message queue provides durable, asynchronous handoff of work between producers and consumers.
details:
It enables independent worker scaling and stronger resilience, but it adds infrastructure and distributed-systems complexity.
example:
RabbitMQ or Azure Service Bus can keep work durable even if the API process restarts before consumers run.
mnemonic:
Durable handoff, extra complexity.
recall:
- Why is a message queue more reliable than in-process background work?
- What complexity does a queue introduce?
```

```concept-card
id: resilience-patterns
term: Resilience Patterns
parents:
- distributed-system-design
children:
- graceful-degradation
summary:
Resilience patterns are techniques such as retries, circuit breakers, and timeouts that help services survive downstream failure.
details:
They prevent one failing dependency from turning every request into a cascade of slow or repeated failures.
example:
A circuit breaker can stop repeated calls to a failing pricing service after a run of timeouts.
mnemonic:
Expect failure, contain failure.
recall:
- Why are retries alone not enough?
- Which resilience patterns help stop a failing service from causing wider damage?
```

```concept-card
id: graceful-degradation
term: Graceful Degradation
parents:
- resilience-patterns
summary:
Graceful degradation means returning a reduced but still useful result when a dependency is down.
details:
Instead of failing the whole request, the system keeps core functionality alive and omits only the unavailable part when the business allows it.
mnemonic:
Return less value, not total failure.
recall:
- Why is graceful degradation a business decision as well as a technical one?
- What kinds of features are often safe to degrade?
```

```concept-card
id: outbox-pattern
term: Outbox Pattern
parents:
- distributed-system-design
related:
- message-queue
- at-least-once-delivery
summary:
The outbox pattern stores a message to be published in the same transaction as the business data change.
details:
It solves the dual-write problem by making the database the durable source of truth for both the business event and the pending publish work.
example:
Save `OrderPlaced` into `OutboxMessages` in the same transaction as the new `Order` row, then publish it later.
mnemonic:
Write with the data, publish after commit.
recall:
- What dual-write problem does the outbox pattern solve?
- Why does the publish happen after the transaction rather than inside it?
```

```concept-card
id: at-least-once-delivery
term: At-Least-Once Delivery
parents:
- distributed-system-design
related:
- outbox-pattern
summary:
At-least-once delivery means a message will be delivered one or more times, so consumers must tolerate duplicates.
details:
Reliable messaging systems often choose possible duplication over message loss, which means handlers must be idempotent.
mnemonic:
Maybe repeated, never assume unique.
recall:
- Why do reliable message systems often deliver duplicates?
- What must consumers do to stay correct under duplicate delivery?
```

```concept-card
id: multi-tenancy-strategies
term: Multi-Tenancy Strategies
parents:
- distributed-system-design
children:
- tenant-isolation
summary:
Multi-tenancy strategies define how tenant data is partitioned, such as database-per-tenant, schema-per-tenant, or shared database with tenant IDs.
details:
The right strategy depends on isolation needs, cost, compliance requirements, and operational complexity.
mnemonic:
Choose the partition that matches the promise.
recall:
- What are the main multi-tenancy storage strategies?
- Which trade-offs drive the choice between them?
```

```concept-card
id: tenant-isolation
term: Tenant Isolation
parents:
- multi-tenancy-strategies
summary:
Tenant isolation is the guarantee that one tenant cannot read or modify another tenant's data.
details:
In shared-database models, enforcement should be centralized through middleware, tenant context, and query filters rather than manual query habits.
example:
Resolve the tenant from the authenticated user, store it in `ITenantContext`, and apply a query filter like `p => p.TenantId == currentTenantId`.
mnemonic:
Isolation fails when enforcement is optional.
recall:
- Why is manual tenant filtering unsafe?
- What central mechanisms help enforce tenant isolation consistently?
```
