---
title: SQL Advanced Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers advanced SQL querying patterns.

Study pages: [Section Index](index.md) | [Advanced Questions](sql-advanced.interview.md) | [Optimization Questions](sql-optimization.interview.md)

## Advanced Map

```concept-card
id: window-functions
term: Window Functions
summary:
Window functions compute values across related rows without collapsing them into one row per group.
details:
They support ranking, running totals, partition-based calculations, and lag/lead comparisons.
example:
`ROW_NUMBER() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC)`
mnemonic:
See the surrounding rows, keep the current row.
recall:
- Why are window functions different from aggregates?
- Which analytical tasks do they solve well?
```

```concept-card
id: recursive-cte
term: Recursive CTE
summary:
A recursive CTE repeatedly references itself to traverse hierarchies or graph-like relationships.
details:
It is commonly used for trees, reporting structures, and category hierarchies.
example:
Query all descendants of one category by starting at the root category and recursively joining children.
mnemonic:
Start at one node, walk the hierarchy.
recall:
- What kind of data structure fits recursive CTEs best?
- Why is recursion useful in SQL here?
```

```concept-card
id: set-operations
term: Set Operations
summary:
Set operations combine query results vertically.
details:
Common operators are `UNION`, `UNION ALL`, `INTERSECT`, and `EXCEPT`.
example:
Use `UNION ALL` when you want both result sets combined without duplicate elimination cost.
mnemonic:
Combine result sets as sets.
recall:
- When is `UNION ALL` preferable to `UNION`?
- What do `INTERSECT` and `EXCEPT` represent?
```

```concept-card
id: views-and-materialized-views
term: Views and Materialized Views
summary:
Views present reusable query logic, while materialized views store query results physically.
details:
Regular views always run the underlying query, while materialized views trade freshness for faster reads.
example:
A sales summary materialized view can speed dashboard reads that would otherwise aggregate millions of rows each time.
mnemonic:
View is logic, materialized view is cached result.
recall:
- What is the core difference between the two?
- When is a materialized view worth the refresh cost?
```
