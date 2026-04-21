---
title: PostgreSQL Performance Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse PostgreSQL-specific performance and concurrency topics.

Relevant concept maps:

- [PostgreSQL Performance Concept Map](postgresql-performance.concept.md)
- [PostgreSQL Core Concept Map](postgresql-core.concept.md)

## Performance and Concurrency

```interview-question
What is MVCC in PostgreSQL and why does it matter?
---
answer:
MVCC stands for Multi-Version Concurrency Control.

It lets readers and writers work with different row versions so reads do not block writes in the same way as simpler locking models.

It improves concurrency, but also creates cleanup work that makes vacuum important.
hints:
- Think row versions.
- Readers and writers interfere less.
- Cleanup is part of the trade-off.
```

Related concepts: [MVCC](postgresql-performance.concept.md#mvcc), [Vacuum and Analyze](postgresql-performance.concept.md#vacuum-and-analyze)

```interview-question
What is the difference between `EXPLAIN` and `EXPLAIN ANALYZE` in PostgreSQL?
---
answer:
`EXPLAIN` shows the planned execution strategy without running the query normally to completion.

`EXPLAIN ANALYZE` actually runs the query and reports real timing and row counts, which makes it much more useful for diagnosing performance.

The key difference is estimate-only versus measured execution.
hints:
- One shows estimates.
- One runs and measures.
- Actual row counts matter for tuning.
```

Related concepts: [Explain Analyze](postgresql-performance.concept.md#explain-analyze), [Planner Statistics](postgresql-performance.concept.md#planner-statistics)

```interview-question
Why are `VACUUM` and `ANALYZE` important in PostgreSQL?
---
answer:
`VACUUM` helps reclaim dead row versions and maintain healthy table visibility behavior under MVCC.

`ANALYZE` updates statistics so the planner can make better decisions about scans, joins, and row estimates.

Without them, storage and planning quality degrade over time.
hints:
- One helps with dead rows.
- One helps with query planning.
- Both matter for long-running systems.
```

Related concepts: [Vacuum and Analyze](postgresql-performance.concept.md#vacuum-and-analyze), [Planner Statistics](postgresql-performance.concept.md#planner-statistics)

```interview-question
When would you choose GIN, GiST, or BRIN instead of the default B-tree index in PostgreSQL?
---
answer:
Choose B-tree for common equality and ordered lookups.

Choose GIN for structures such as `jsonb`, arrays, and full-text search where membership and containment matter. GiST fits more complex search spaces such as geometric and range-like queries. BRIN is useful for very large naturally ordered datasets where summarizing page ranges is efficient.

The right index type depends on operator semantics and data layout, not just habit.
hints:
- B-tree is the normal default.
- GIN is strong for `jsonb`, arrays, and text search.
- BRIN is for huge naturally ordered datasets.
```

Related concepts: [PostgreSQL Index Types](postgresql-performance.concept.md#postgresql-index-types)

```interview-choice
Which PostgreSQL index type is often a strong fit for `jsonb` containment queries?
---
options:
- B-tree
- GIN
- BRIN
correct: 1
explanation:
GIN indexes are commonly used for `jsonb`, arrays, and full-text search because they support membership and containment-style access patterns well.
```
