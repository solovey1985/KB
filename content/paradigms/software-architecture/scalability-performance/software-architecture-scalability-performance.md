# Scalability and Performance Considerations

Scalability and performance architecture is about maintaining acceptable user experience and system stability as load grows.

## Scaling strategies

Common scaling strategies include:

- vertical scaling by adding more resources to one node
- horizontal scaling by adding more nodes
- partitioning workloads by feature, tenant, or data range
- asynchronous processing for expensive background work

Vertical scaling is simple but hits hard limits.

Horizontal scaling is more flexible, but it often requires stateless services, distributed coordination, and better observability.

## Load balancing and statelessness

Load balancing spreads requests across multiple instances to improve throughput and availability.

This works best when services are stateless, meaning a request can be handled by any healthy instance without relying on in-memory session state.

When state must exist, teams usually externalize it into:

- databases
- distributed caches
- session stores
- message queues

## Caching

Caching improves performance by serving frequently requested data from a faster layer closer to the consumer.

Common cache locations:

- browser or client cache
- CDN or edge cache
- reverse proxy cache
- application cache
- database cache

Caching improves latency and reduces backend load, but it creates consistency and invalidation problems.

## High availability and failure handling

High availability means designing the system so one failure does not take the whole service down.

Typical techniques include:

- redundancy
- health checks and automatic failover
- timeout and retry policies
- circuit breakers
- multiple instances across failure zones

Availability is not just about having duplicates. It also depends on whether the system can detect and route around failures quickly.

## CAP theorem and data trade-offs

In distributed systems, the CAP theorem says that under a network partition you cannot fully optimize consistency and availability at the same time.

That does not mean teams pick only one forever. It means different parts of a system may prioritize consistency or availability depending on the business need.

## Replication and failover

Replication creates additional copies of data for resilience, read scaling, or geographic distribution.

Failover promotes a standby or alternate instance when the primary fails.

These help availability, but also raise questions about:

- replication lag
- stale reads
- split-brain risks
- recovery point objectives

## Interview reminders

- scaling is about more than adding servers
- stateless services make horizontal scaling easier
- caching improves speed but introduces invalidation trade-offs
- high availability requires redundancy plus detection and recovery
- CAP is a trade-off model for partitions, not a slogan to repeat without context
