# Microservices Foundations

This page turns the publicly visible microservices interview topics into compact study notes.

## What A Microservice Is

A microservice is a small, independently deployable service built around a specific business capability.

It is not defined mainly by code size. The more important properties are:

- clear ownership
- a focused business responsibility
- independent deployment
- explicit communication with other services
- local control over its data and runtime decisions

The contrast with a monolith is deployment and operational shape. A monolith runs as one deployable unit, while microservices split the system into multiple deployable processes.

## Core Principles

Good microservices architecture usually follows these principles:

- align services to business domains, not technical layers
- keep service boundaries explicit
- prefer loose coupling and high cohesion
- let teams deploy services independently
- expect partial failure and design for it
- automate delivery, configuration, and observability
- avoid shared databases as the default integration model

Microservices work best when team and domain boundaries are both meaningful.

## Benefits

The main benefits are:

- teams can work and release more independently
- services can scale differently based on load
- failures can be isolated better than in one large process
- different services can evolve at different speeds
- boundaries can make large domains easier to reason about

These benefits only matter when the system is large or the organization needs that independence. For smaller systems, a modular monolith is often faster and cheaper.

## Main Costs And Challenges

Microservices replace local complexity with distributed-systems complexity.

The common costs are:

- more network calls and more latency
- more deployment and runtime infrastructure
- harder debugging across process boundaries
- eventual consistency instead of simple local transactions
- duplicated cross-cutting concerns such as auth, tracing, retries, and configuration
- more operational burden for monitoring, alerting, and incident response

The most common mistake is choosing microservices too early, before the system has stable business boundaries or the team has the operational maturity to support them.

## Communication Between Services

Services usually communicate in two broad ways:

### Synchronous communication

Typical options are HTTP or gRPC.

Use synchronous calls when:

- the caller needs an immediate answer
- the dependency is part of the request flow
- the operation is short-lived and easy to time out

The trade-off is stronger runtime coupling. If the downstream service is slow or unavailable, the caller feels it directly.

### Asynchronous communication

Typical options are queues, topics, or event buses.

Use asynchronous messaging when:

- the caller does not need an immediate response
- work should survive restarts or spikes
- multiple consumers need to react to one event
- eventual consistency is acceptable

The trade-off is more infrastructure, more message semantics, and more care around duplicates and ordering.

## DDD, Bounded Contexts, And Decomposition

Domain-Driven Design is useful in microservices because it helps identify boundaries based on the business domain instead of controllers, tables, or teams alone.

A bounded context is a boundary where a domain model has a clear meaning and vocabulary. In practice, it is a strong candidate for a service or module boundary.

When decomposing a monolith:

1. start from business capabilities and domain workflows
2. identify bounded contexts and ownership lines
3. extract the least-coupled areas first
4. prefer a modular monolith first if boundaries are still emerging
5. move one seam at a time instead of splitting everything at once

Do not decompose by CRUD tables alone. That usually creates chatty services and weak business boundaries.

## Transactions Across Services

Distributed transactions are expensive and fragile in microservices. Avoid two-phase commit unless there is a rare, strong reason.

Prefer these strategies instead:

- eventual consistency
- saga orchestration or choreography
- compensating actions
- transactional outbox
- idempotent consumers

The core idea is to keep each local database transaction small and reliable, then coordinate business progress across services through messages and recovery steps.

## Data Consistency

Each service should usually own its own data store or at least its own data boundary.

To keep data consistent across services:

- publish domain events after local commits
- use the outbox pattern to avoid dual writes
- make handlers idempotent
- design for retries and duplicate delivery
- keep business invariants local when possible

Strong consistency across many services should be the exception, not the normal design target.

## Handling Failure

Failure is normal in microservices because networks, processes, and dependencies all fail independently.

Basic resilience patterns include:

- timeouts
- retries with backoff
- circuit breakers
- bulkheads
- fallback responses
- graceful degradation

Retries should be selective. Retrying every error can make an outage worse.

## Common Patterns

### API Gateway

An API Gateway provides one entry point for clients and can handle routing, authentication, aggregation, rate limits, and cross-cutting policies.

It is useful when many client-facing services would otherwise expose too much internal structure.

### Circuit Breaker

A circuit breaker stops repeated calls to an unhealthy dependency after failures cross a threshold. This protects the caller from wasting resources and helps prevent cascading failure.

### Service Mesh

A service mesh moves common service-to-service concerns like mTLS, retries, traffic shaping, and telemetry into infrastructure sidecars or proxies.

It reduces duplicated client logic, but it also adds operational complexity and another layer to understand.

## Interview Heuristics

In interviews, strong answers usually show these instincts:

- start with business boundaries, not with technology hype
- say when a monolith is the better choice
- separate local consistency from cross-service consistency
- mention failure handling as a first-class concern
- explain trade-offs instead of claiming a universal best practice

If you can explain why a modular monolith might be the right starting point, you usually understand microservices better than someone who always recommends them.
