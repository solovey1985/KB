---
title: Data Management and Integration Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise storage and integration architecture questions.

Relevant concept maps:

- [Concept Map](software-architecture-data-management-integration.concept.md)

## Data and Integration

```interview-question
What role do message brokers play in system integration?
---
answer:
Message brokers decouple producers and consumers by transporting messages asynchronously, which helps with integration, retries, buffering, and back-pressure.

They are valuable when systems should not depend on synchronous availability of every downstream consumer.
hints:
- Asynchronous decoupling is central.
- Buffering and retries are useful clues.
- Producers and consumers do not need to talk directly.
```

```interview-question
How would you compare ETL and ELT?
---
answer:
ETL transforms data before loading it into the target system, while ELT loads data first and transforms it inside the target platform.

The better choice depends on where computation belongs, how much raw data should be preserved, and how often transformation rules change.
hints:
- Focus on where transformation happens.
- Raw retention is a useful distinction.
- Modern platforms often make ELT attractive.
```

```interview-question
When choosing between SQL and NoSQL databases, what should architecture consider?
---
answer:
Architecture should consider transaction requirements, consistency expectations, query patterns, schema flexibility, scaling behavior, and operational complexity.

The right choice depends on workload fit rather than a simple rule that one category is always better.
hints:
- Think workload fit.
- Transactions and query patterns matter.
- Consistency and scaling should appear too.
```

```interview-choice
Which statement best describes a data lake?
---
options:
- It is mainly an in-memory cache for live application requests
- It is a store for broad raw or semi-processed data used for analytics and downstream processing
- It is just another name for a relational OLTP database
correct: 1
explanation:
A data lake is typically used for broader analytical storage and processing, not as a direct replacement for operational transactional databases.
```
