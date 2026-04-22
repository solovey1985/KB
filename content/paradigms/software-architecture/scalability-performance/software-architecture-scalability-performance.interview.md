---
title: Scalability and Performance Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about load, latency, and growth.

Relevant concept maps:

- [Concept Map](software-architecture-scalability-performance.concept.md)

## Scaling and Runtime Behavior

```interview-question
What strategies can be used to scale a software application?
---
answer:
Common strategies include vertical scaling, horizontal scaling, partitioning workloads, using asynchronous processing, and removing bottlenecks with caching or dedicated data stores.

The right mix depends on where the actual constraint appears in the system.
hints:
- Scale up and scale out should both appear.
- Queues and partitioning are also valid.
- Bottleneck analysis matters.
```

Related concepts: [Scalability](software-architecture-scalability-performance.concept.md#scalability), [Horizontal Scaling](software-architecture-scalability-performance.concept.md#horizontal-scaling)

```interview-question
Explain the concept of a stateless architecture.
---
answer:
A stateless architecture avoids depending on local in-memory request history or session state inside a single server instance.

That allows any healthy instance to handle a request, which simplifies load balancing, failover, and horizontal scaling.
hints:
- Any node should be able to serve the request.
- Session state should not be trapped in one instance.
- This helps scale-out scenarios.
```

Related concepts: [Stateless Service](software-architecture-scalability-performance.concept.md#stateless-service)

```interview-question
How does caching improve system performance?
---
answer:
Caching improves performance by serving frequently requested data from a faster layer, which lowers latency and reduces repeated work on slower backend components.

It is especially useful on read-heavy paths, but it introduces invalidation and staleness trade-offs.
hints:
- Faster layer is the core idea.
- Lower latency and lower backend load.
- Mention invalidation trade-offs.
```

Related concepts: [Caching](software-architecture-scalability-performance.concept.md#caching)

```interview-question
What practices are vital for designing high-availability systems?
---
answer:
High-availability systems rely on redundancy, health checks, automatic failover, timeout and retry strategy, removing single points of failure, and deploying across independent failure domains.

Observability and recovery automation are also important because failures must be detected and handled quickly.
hints:
- Multiple instances are only one part.
- Failure detection matters.
- Think failover and failure domains.
```

Related concepts: [High Availability](software-architecture-scalability-performance.concept.md#high-availability)

```interview-choice
What does CAP theorem say in the most accurate short form?
---
options:
- A distributed system can never be consistent and available
- During a network partition, a distributed system must trade off consistency and availability
- Every database is either CP or AP in all situations
correct: 1
explanation:
CAP matters specifically under partition. It does not mean systems permanently live at one simple label in every circumstance.
```
