---
title: SQL Optimization Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse query tuning and index-design concepts that apply across SQL engines.

Relevant concept maps:

- [SQL Optimization Concept Map](sql-optimization.concept.md)
- [SQL Advanced Concept Map](sql-advanced.concept.md)

## Optimization Foundations

```interview-question
What are indexes and how do they improve query performance?
---
answer:
Indexes are data structures that help the database locate rows more efficiently than scanning the entire table.

They improve query performance when the query filters, joins, or sorts in ways that match the index shape.

Indexes are not free because they consume storage and increase write overhead.
hints:
- They are about faster access paths.
- Reads improve, writes often get more expensive.
- Query shape determines usefulness.
```

Related concepts: [Indexes](sql-optimization.concept.md#indexes), [Composite Index Order](sql-optimization.concept.md#composite-index-order), [Covering Index](sql-optimization.concept.md#covering-index)

```interview-question
Why does column order matter in a composite index?
---
answer:
Composite indexes are most useful when the query uses the leading columns in the same order the index was defined.

If the query skips the leftmost indexed column, the index often becomes less effective for filtering and ordering.

That is why index design should follow actual query patterns, not just the schema.
hints:
- The leftmost column leads.
- Equality predicates often come before ranges.
- Query shape should drive index order.
```

Related concepts: [Composite Index Order](sql-optimization.concept.md#composite-index-order)

```interview-question
What does `EXPLAIN` or execution-plan analysis tell you?
---
answer:
It shows how the database plans to execute a query, including scan types, join strategies, sorts, and estimated costs.

This helps identify full scans, bad join orders, missing indexes, or poor selectivity assumptions.

Real optimization starts by observing the actual plan rather than guessing.
hints:
- It reveals the access path.
- Joins and scans are key clues.
- Guessing is not tuning.
```

Related concepts: [Execution Plan Analysis](sql-optimization.concept.md#execution-plan-analysis), [Selectivity](sql-optimization.concept.md#selectivity)

```interview-choice
Which index best matches a query that filters on `customer_id` and then ranges on `created_at`?
---
options:
- `(created_at, customer_id)`
- `(customer_id, created_at)`
- `(status, customer_id)`
correct: 1
explanation:
The equality filter should lead, followed by the range column, so `(customer_id, created_at)` is the best match.
```
