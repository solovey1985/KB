---
title: Microservices Foundations Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core concepts behind microservices architecture.

Study pages: [Section Index](index.md) | [Material Notes](microservices-foundations.md) | [Interview Practice](microservices-foundations.interview.md)

## Core Map

```concept-card
id: microservices-architecture
term: Microservices Architecture
children:
- microservice
- service-boundary
- bounded-context
- service-communication
- eventual-consistency
- api-gateway
- circuit-breaker
- service-mesh
related:
- modular-monolith
summary:
Microservices architecture structures a system as multiple independently deployable services aligned to business capabilities.
details:
Its value comes from clearer ownership and deployment independence, but it also introduces network, consistency, and operational complexity.
example:
An ecommerce platform may separate catalog, ordering, inventory, and billing into distinct services with different deployment and scaling needs.
mnemonic:
Split for ownership, pay with complexity.
recall:
- What makes microservices different from one large deployable application?
- Why is operational complexity part of the definition in practice?
```

```concept-card
id: microservice
term: Microservice
parents:
- microservices-architecture
summary:
A microservice is a small service focused on one business capability with its own deployable runtime boundary.
details:
The important trait is not tiny code size but clear responsibility, independent evolution, and explicit communication with other services.
example:
`InventoryService` manages stock reservations and availability rules without also owning payments or user profiles.
mnemonic:
One capability, one owned runtime boundary.
recall:
- What matters more than code size when defining a microservice?
- Why is business capability a better boundary than technical layer?
```

```concept-card
id: modular-monolith
term: Modular Monolith
related:
- microservices-architecture
summary:
A modular monolith is one deployable application with strong internal module boundaries.
details:
It keeps many of the design benefits of explicit domain boundaries while avoiding most distributed-systems costs early on.
example:
Orders, billing, and inventory can live in one deployable app while still being separated into strict modules.
mnemonic:
One deployment, disciplined boundaries.
recall:
- Why is a modular monolith often the safer starting point?
- Which microservice benefits can it preserve without network complexity?
```

```concept-card
id: service-boundary
term: Service Boundary
parents:
- microservices-architecture
related:
- bounded-context
summary:
A service boundary defines what a service owns, what it exposes, and what it must not leak.
details:
Strong boundaries reduce coupling and accidental data sharing, while weak boundaries produce chatty calls and confused ownership.
example:
Order placement may call inventory and billing, but order state transitions still belong to the ordering boundary.
mnemonic:
Weak boundary, noisy system.
recall:
- What problems appear when service boundaries are weak?
- Why should ownership be explicit at the boundary?
```

```concept-card
id: bounded-context
term: Bounded Context
parents:
- microservices-architecture
related:
- service-boundary
summary:
A bounded context is a domain boundary where terms and rules have one consistent meaning.
details:
DDD uses bounded contexts to identify where one model should stop and another should begin, which makes them strong candidates for service or module boundaries.
example:
The term `Customer` may mean buyer identity in one context and billing account holder in another.
mnemonic:
Same word, new context, new model.
recall:
- Why are bounded contexts useful when designing services?
- What warning sign appears when one term means different things in one model?
```

```concept-card
id: service-communication
term: Service Communication
parents:
- microservices-architecture
children:
- synchronous-communication
- asynchronous-communication
summary:
Service communication is how microservices exchange requests, commands, and events.
details:
The main design choice is whether the caller needs an immediate answer or whether the work can proceed asynchronously.
example:
Checkout may call pricing synchronously but publish `OrderPlaced` asynchronously for downstream fulfillment work.
mnemonic:
Need now or can wait.
recall:
- What is the main question behind choosing sync versus async communication?
- Why do communication choices shape coupling?
```

```concept-card
id: synchronous-communication
term: Synchronous Communication
parents:
- service-communication
related:
- asynchronous-communication
summary:
Synchronous communication means the caller waits for the callee to respond before continuing.
details:
It is easier to reason about in request flows, but it creates stronger runtime coupling and exposes the caller directly to downstream latency and outages.
example:
An API service calling a pricing service over HTTP during request handling is synchronous communication.
mnemonic:
Wait now, feel failure now.
recall:
- When is synchronous communication appropriate?
- What coupling cost does it create?
```

```concept-card
id: asynchronous-communication
term: Asynchronous Communication
parents:
- service-communication
related:
- synchronous-communication
summary:
Asynchronous communication lets the producer continue without waiting for the consumer to complete the work.
details:
It improves decoupling and durability for many workflows, but it introduces message semantics, eventual consistency, and idempotency concerns.
example:
Publishing `OrderPlaced` to a broker lets fulfillment continue later without blocking the checkout request.
mnemonic:
Send now, finish later.
recall:
- Why does asynchronous communication reduce direct runtime coupling?
- What new correctness concerns does it introduce?
```

```concept-card
id: eventual-consistency
term: Eventual Consistency
parents:
- microservices-architecture
children:
- saga-pattern
- outbox-pattern
summary:
Eventual consistency means different services may observe state changes at different times before converging later.
details:
It is the normal consistency model for many microservices workflows because each service commits locally and coordination happens through messages and recovery logic.
example:
An order can be accepted first, then inventory and billing move the overall business process forward asynchronously.
mnemonic:
Correct later, coordinated by flow.
recall:
- Why is eventual consistency common in microservices?
- How is it different from one local ACID transaction?
```

```concept-card
id: saga-pattern
term: Saga Pattern
parents:
- eventual-consistency
summary:
A saga coordinates a business process across services through local transactions and compensating actions.
details:
Instead of one distributed transaction, each step commits locally and failures are handled by reversing or compensating later steps when needed.
example:
If payment succeeds but stock reservation fails, the saga may trigger a payment refund or cancellation step.
mnemonic:
Commit locally, recover globally.
recall:
- What problem does a saga solve?
- Why are compensating actions important in sagas?
```

```concept-card
id: outbox-pattern
term: Outbox Pattern
parents:
- eventual-consistency
summary:
The outbox pattern stores integration messages in the same local transaction as the business state change.
details:
It prevents the dual-write problem by making the database commit the durable source of truth for both state and pending publish work.
example:
Save a new order row and an `OrderPlaced` outbox record together, then publish the message after commit.
mnemonic:
Write together, publish after.
recall:
- What dual-write problem does the outbox pattern avoid?
- Why is publishing delayed until after the local commit?
```

```concept-card
id: api-gateway
term: API Gateway
parents:
- microservices-architecture
summary:
An API Gateway is a client-facing entry point that routes requests to internal services and centralizes cross-cutting concerns.
details:
It can handle authentication, aggregation, throttling, and protocol translation so clients do not need to know the full internal topology.
example:
A mobile app calls one gateway endpoint that fans out to profile, recommendations, and orders services.
mnemonic:
One front door, many services behind it.
recall:
- Why is an API Gateway useful in microservices?
- Which client problems does it reduce?
```

```concept-card
id: circuit-breaker
term: Circuit Breaker
parents:
- microservices-architecture
summary:
A circuit breaker stops repeated calls to a failing dependency after a threshold of errors or timeouts.
details:
It protects resources, reduces cascading failure, and gives unhealthy dependencies time to recover instead of being hammered continuously.
example:
After repeated timeouts to a recommendation service, the caller returns a fallback response instead of opening more failing connections.
mnemonic:
Trip early, protect the system.
recall:
- Why can retries alone be dangerous during an outage?
- How does a circuit breaker reduce cascading failure?
```

```concept-card
id: service-mesh
term: Service Mesh
parents:
- microservices-architecture
summary:
A service mesh provides infrastructure-level control for service-to-service traffic, security, and telemetry.
details:
It can centralize mTLS, retries, traffic shaping, and observability, but it adds another operational layer that teams must understand well.
example:
Istio can apply traffic policies and collect telemetry without every service implementing the same client plumbing.
mnemonic:
Shared traffic rules, shared operational layer.
recall:
- What concerns can a service mesh move out of application code?
- Why is a service mesh not free simplicity?
```
