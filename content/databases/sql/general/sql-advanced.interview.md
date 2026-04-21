---
title: SQL Advanced Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise advanced SQL patterns that still remain mostly vendor-neutral.

Relevant concept maps:

- [SQL Advanced Concept Map](sql-advanced.concept.md)
- [SQL Optimization Concept Map](sql-optimization.concept.md)

## Advanced Querying

```interview-question
What problem do window functions solve?
---
answer:
Window functions let you compute values across related rows without collapsing them into one row per group.

They are useful for ranking, running totals, lag/lead comparisons, and per-partition calculations while still returning detail rows.
hints:
- Think ranking and running totals.
- They do not collapse rows the way `GROUP BY` does.
- `OVER(...)` is the key syntax clue.
```

Related concepts: [Window Functions](sql-advanced.concept.md#window-functions), [Grouping and Aggregation](sql-basic.concept.md#grouping-and-aggregation)

```interview-question
Why would you use a recursive CTE?
---
answer:
A recursive CTE is used to traverse hierarchical or tree-like data such as organizational structures, categories, or graph-like parent-child relationships.

It allows a query to repeatedly reference the growing result set until no more related rows are found.
hints:
- Think hierarchy.
- The query refers back to itself.
- Parent-child traversal is the common use case.
```

Related concepts: [Recursive CTE](sql-advanced.concept.md#recursive-cte), [Common Table Expression](sql-intermediate.concept.md#common-table-expression)

```interview-choice
Which feature is the best fit for computing a row number within each department ordered by salary?
---
options:
- `GROUP BY`
- `ROW_NUMBER() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC)`
- `HAVING`
correct: 1
explanation:
Window functions are designed for row-aware calculations within partitions without collapsing the result set.
```
