---
title: SQL Intermediate Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse intermediate SQL topics that apply across database engines.

Relevant concept maps:

- [SQL Intermediate Concept Map](sql-intermediate.concept.md)
- [SQL Advanced Concept Map](sql-advanced.concept.md)

## Query Design

```interview-question
What is the difference between a subquery and a Common Table Expression?
---
answer:
A subquery is a query nested inside another query.

A Common Table Expression, or CTE, is a named intermediate result defined with `WITH` and then referenced by the main query.

CTEs often improve readability, and recursive CTEs support hierarchical queries.
hints:
- One is unnamed and nested.
- The other is named and defined first.
- Readability is a major difference.
```

Related concepts: [Subqueries](sql-intermediate.concept.md#subqueries), [Common Table Expression](sql-intermediate.concept.md#common-table-expression)

```interview-question
What is normalization?
---
answer:
Normalization is the process of organizing relational data to reduce redundancy and avoid update, insert, and delete anomalies.

It usually involves splitting data into related tables based on functional dependencies.

The goal is cleaner data integrity, not abstract purity for its own sake.
hints:
- Think redundancy reduction.
- It is about schema design.
- Anomalies are the core reason.
```

Related concepts: [Normalization](sql-intermediate.concept.md#normalization), [Denormalization](sql-intermediate.concept.md#denormalization)

```interview-question
What is denormalization and when would you use it?
---
answer:
Denormalization is the intentional introduction of redundancy to improve read performance or simplify access patterns.

It is useful in read-heavy systems, reporting, and analytical workloads where fewer joins matter more than strict normalization.

The trade-off is more complex writes and consistency management.
hints:
- It adds redundancy on purpose.
- It is usually a performance-driven choice.
- Reads get easier, writes get harder.
```

Related concepts: [Denormalization](sql-intermediate.concept.md#denormalization), [Normalization](sql-intermediate.concept.md#normalization)

```interview-choice
Which statement about transactions is correct?
---
options:
- `COMMIT` undoes the transaction
- `ROLLBACK` persists all changes permanently
- `SAVEPOINT` lets you roll back part of a transaction
correct: 2
explanation:
`SAVEPOINT` creates an internal checkpoint so part of a transaction can be rolled back without discarding all prior work.
```

```interview-code
language: sql
prompt: Complete the CTE so the outer query can read grouped totals by department.
starter:
WITH DepartmentTotals AS (
    SELECT DepartmentID, COUNT(*) AS EmployeeCount
    FROM Employees
    GROUP BY DepartmentID
)
SELECT *
 DepartmentTotals;
solution:
WITH DepartmentTotals AS (
    SELECT DepartmentID, COUNT(*) AS EmployeeCount
    FROM Employees
    GROUP BY DepartmentID
)
SELECT *
FROM DepartmentTotals;
checks:
- includes: WITH DepartmentTotals AS
- includes: FROM DepartmentTotals
```
