---
title: Data Management and Integration Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page summarizes the main data and system-integration ideas used in architecture discussions.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-data-management-integration.md) | [Interview Practice](software-architecture-data-management-integration.interview.md)

## Data Map

```concept-card
id: data-integration
term: Data Integration
children:
- message-broker
- data-lake
- etl
- elt
summary:
Data integration is the architecture of moving, synchronizing, and transforming data across systems.
details:
It includes event flow, batch movement, source-of-truth boundaries, and the failure handling needed to keep distributed data useful.
example:
An order service publishing events that feed analytics, billing, and notifications is a data integration problem.
mnemonic:
Data must move with rules.
recall:
- Why is integration more than just connecting two APIs?
- What questions define a safe data boundary?
```

```concept-card
id: message-broker
term: Message Broker
parents:
- data-integration
summary:
A message broker moves messages between producers and consumers while providing decoupling and delivery support.
details:
It helps with asynchronous workflows and buffering, but introduces ordering, duplication, and operational visibility concerns.
example:
RabbitMQ or Kafka can carry events between services without direct synchronous calls.
mnemonic:
Move messages, loosen direct ties.
recall:
- Why does a broker reduce direct coupling?
- What new failure-handling responsibilities come with it?
```

```concept-card
id: data-lake
term: Data Lake
parents:
- data-integration
summary:
A data lake stores large amounts of raw or semi-processed data for analytics and downstream processing.
details:
It is usually different from the transactional stores that power operational applications.
example:
Clickstream events and operational exports land in a lake before later transformation.
mnemonic:
Store broad raw data for later use.
recall:
- How does a data lake differ from an operational database?
- Why might raw retention be valuable?
```

```concept-card
id: etl
term: ETL
related:
- elt
summary:
ETL means extract, transform, then load.
details:
Transformation happens before the data reaches the target store, which can simplify the target but limits raw retention.
example:
Data cleaned and normalized before loading into a reporting warehouse is ETL.
mnemonic:
Transform first, load later.
recall:
- Where does ETL place the transformation step?
- What are the practical benefits of that order?
```

```concept-card
id: elt
term: ELT
related:
- etl
summary:
ELT means extract, load, then transform.
details:
It takes advantage of more capable target platforms and can preserve raw data for future transformation logic.
example:
Raw event data loaded into a warehouse and transformed there is ELT.
mnemonic:
Load first, shape later.
recall:
- Why is ELT attractive with modern data platforms?
- What flexibility does raw loading preserve?
```
