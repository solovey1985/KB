---
title: SQL Optimization Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes query tuning and access-path concepts.

Study pages: [Section Index](index.md) | [Optimization Questions](sql-optimization.interview.md)

## Optimization Map

```concept-card
id: indexes
term: Indexes
children:
- composite-index-order
- covering-index
- selectivity
- execution-plan-analysis
summary:
Indexes are data structures that help the database find rows efficiently for matching query patterns.
details:
They improve many reads, but increase storage and write cost.
example:
An index on `(customer_id, created_at)` can speed filtered customer order lookups.
mnemonic:
Faster reads, heavier writes.
recall:
- Why are indexes not free?
- What kinds of queries benefit most from them?
```

```concept-card
id: composite-index-order
term: Composite Index Order
parents:
- indexes
summary:
Composite index order determines how well a multi-column index matches filtering and sorting patterns.
details:
The leftmost columns matter most, so query predicates should align with the leading keys.
example:
`(customer_id, created_at)` is better than `(created_at, customer_id)` when customer filtering comes first.
mnemonic:
Leftmost leads.
recall:
- Why does the leftmost column matter so much?
- Why do equality predicates often lead range predicates?
```

```concept-card
id: covering-index
term: Covering Index
parents:
- indexes
summary:
A covering index contains enough data to satisfy a query without extra lookups to the base table for projected columns.
details:
It speeds hot read paths, but larger indexes cost more to maintain.
example:
Include `status` and `total_amount` in an order index used by an order-list endpoint.
mnemonic:
Cover the read, pay on the write.
recall:
- What lookup does a covering index try to avoid?
- Why can over-covering become expensive?
```

```concept-card
id: selectivity
term: Selectivity
parents:
- indexes
summary:
Selectivity describes how strongly a predicate narrows the result set.
details:
Highly selective columns are often stronger index candidates than low-cardinality flags used alone.
example:
`email` is usually more selective than `is_active`.
mnemonic:
Better filter, better index key.
recall:
- Why are low-cardinality columns weaker standalone index candidates?
- How does selectivity influence planner choices?
```

```concept-card
id: execution-plan-analysis
term: Execution Plan Analysis
parents:
- indexes
summary:
Execution plan analysis shows how the database intends to execute a query.
details:
It helps diagnose scans, join choices, sort cost, and missing or misused indexes.
example:
Use `EXPLAIN` or `EXPLAIN ANALYZE` to see whether a query used a sequential scan, index scan, hash join, or sort.
mnemonic:
Read the plan before changing the query.
recall:
- What can an execution plan reveal that query text alone cannot?
- Why is plan analysis the starting point for real tuning?
```
