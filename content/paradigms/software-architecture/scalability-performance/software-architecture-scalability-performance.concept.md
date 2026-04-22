---
title: Scalability and Performance Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page captures the main ideas used when discussing scale, latency, and availability.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-scalability-performance.md) | [Interview Practice](software-architecture-scalability-performance.interview.md)

## Scale Map

```concept-card
id: scalability
term: Scalability
children:
- horizontal-scaling
- load-balancing
- stateless-service
- caching
- high-availability
summary:
Scalability is the ability of a system to handle increasing load without unacceptable degradation.
details:
It can involve adding resources, changing workload distribution, or redesigning bottlenecked parts of the system.
example:
Moving heavy report generation to asynchronous workers can improve scalability even without adding more CPUs.
mnemonic:
More load, still stable.
recall:
- Why is scalability not the same thing as pure performance?
- What kinds of architectural changes improve scalability?
```

```concept-card
id: horizontal-scaling
term: Horizontal Scaling
parents:
- scalability
related:
- stateless-service
- load-balancing
summary:
Horizontal scaling increases capacity by adding more instances rather than making one node bigger.
details:
It improves elastic growth, but often requires stateless services, request routing, and shared external state stores.
example:
Running six API instances behind a load balancer is horizontal scaling.
mnemonic:
Scale out, not only up.
recall:
- Why does horizontal scaling often require statelessness?
- What new operational complexity appears after scaling out?
```

```concept-card
id: load-balancing
term: Load Balancing
parents:
- scalability
summary:
Load balancing distributes requests across multiple healthy instances.
details:
It improves throughput and availability by preventing one instance from carrying all traffic and by routing around failures.
example:
An ingress routes incoming HTTP traffic across several API pods.
mnemonic:
Spread the traffic, reduce the pain.
recall:
- Why is load balancing useful even before peak traffic arrives?
- How does it help availability as well as performance?
```

```concept-card
id: stateless-service
term: Stateless Service
parents:
- scalability
related:
- load-balancing
summary:
A stateless service does not rely on local in-memory session state between requests.
details:
This makes any healthy instance able to serve a request, which is valuable for scaling and failover.
example:
Authentication state stored in a token or shared session store supports stateless API nodes.
mnemonic:
Any node can serve the next request.
recall:
- Why does statelessness simplify scaling?
- Where should required shared state live instead?
```

```concept-card
id: caching
term: Caching
parents:
- scalability
summary:
Caching stores frequently used data in a faster layer to reduce latency and backend work.
details:
It is powerful for read-heavy paths, but cache invalidation and stale data handling become major design concerns.
example:
Product catalog results cached in Redis reduce repeated database reads.
mnemonic:
Reuse what is asked for often.
recall:
- Why is cache invalidation a hard design problem?
- Which read paths benefit most from caching?
```

```concept-card
id: high-availability
term: High Availability
parents:
- scalability
related:
- cap-theorem
summary:
High availability means the system remains usable despite component failures.
details:
It usually depends on redundancy, fast failure detection, automatic recovery, and careful removal of single points of failure.
example:
Running multiple instances across zones with health checks and failover supports high availability.
mnemonic:
Fail small, stay up.
recall:
- Why is redundancy alone not enough for high availability?
- What capabilities are needed to recover from failure quickly?
```

```concept-card
id: cap-theorem
term: CAP Theorem
summary:
CAP theorem states that during a network partition a distributed system cannot fully maximize both consistency and availability at the same time.
details:
It is a trade-off model that helps explain why different parts of a system may prioritize stronger consistency or better availability under failure.
example:
A payment write path might favor consistency, while a feed service might favor availability.
mnemonic:
Partition forces a choice.
recall:
- When does CAP actually matter?
- Why is it wrong to use CAP as a shallow label for every database?
```
