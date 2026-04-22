# Data Management and Integration

Architecture depends heavily on how systems store, move, and synchronize data.

## Data stores and workload fit

Choosing between SQL and NoSQL is rarely about popularity. It is about the workload.

Consider:

- transaction needs
- query patterns
- schema evolution
- consistency requirements
- scale characteristics

## Message brokers and integration

Message brokers decouple producers and consumers by transporting commands, events, or data between systems.

They help with:

- asynchronous processing
- back-pressure handling
- system integration
- retry and buffering

But they also add ordering, duplication, and observability challenges.

## Data lake and data pipelines

A data lake stores raw or semi-processed data for later analytics, modeling, or downstream transformation.

This differs from operational architecture, where data is optimized for application workflows rather than broad analysis.

## ETL versus ELT

ETL transforms data before loading into the target system.

ELT loads data first and transforms it inside a more capable target platform.

The better fit depends on where computation should happen, how much raw data should be retained, and how frequently transformations change.

## Integration boundaries

Good integration architecture requires explicit contracts, versioning discipline, and failure-handling rules.

Questions that matter:

- who owns the source of truth?
- how do consumers handle missing or delayed data?
- what is the retry and deduplication strategy?

## Interview reminders

- database choice follows workload and consistency needs
- brokers decouple systems but add operational complexity
- analytics data architecture differs from transactional application architecture
- ETL and ELT differ mainly in where transformation happens
- integration quality depends on contracts and failure handling, not only protocols
