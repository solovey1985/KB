---
title: T-SQL Advanced Query Patterns Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers advanced query patterns used in SQL Server reporting and data shaping.

Study pages: [Advanced Query Patterns](advanced-query-patterns.md) | [Interview Practice](advanced-query-patterns.interview.md)

```concept-card
id: advanced-query-patterns
term: Advanced Query Patterns
children:
- subquery
- common-table-expression
- recursive-cte
- window-function
- case-expression
- pagination
summary:
Advanced query patterns shape intermediate results, hierarchy traversal, ranking, running totals, labels, and paged output.
details:
They help express complex logic while keeping SQL readable and set-based.
recall:
- Which pattern names an intermediate result?
- Which pattern ranks rows without collapsing them?
```

```concept-card
id: subquery
term: Subquery
parents:
- advanced-query-patterns
related:
- common-table-expression
summary:
A subquery is a query nested inside another SQL statement.
details:
It can return a scalar value, a list, or a table-like result used by the outer query.
recall:
- When is a scalar subquery useful?
- When might a CTE be clearer than a nested subquery?
```

```concept-card
id: common-table-expression
term: Common Table Expression
aliases:
- CTE
parents:
- advanced-query-patterns
children:
- recursive-cte
summary:
A CTE names a temporary result set for use by a single statement.
details:
It improves readability for multi-step queries and is required for recursive CTE patterns.
example:
WITH ProductSales AS (...) SELECT ... FROM ProductSales;
recall:
- How long does a CTE exist?
- Why can a CTE improve readability?
```

```concept-card
id: recursive-cte
term: Recursive CTE
parents:
- common-table-expression
summary:
A recursive CTE repeatedly references itself to walk hierarchical data.
details:
It has an anchor query for starting rows and a recursive query that finds the next level.
example:
SalesLT.ProductCategory can be traversed through ParentProductCategoryID.
recall:
- What are the two parts of a recursive CTE?
- What kind of data is it used for?
```

```concept-card
id: window-function
term: Window Function
parents:
- advanced-query-patterns
summary:
A window function calculates across related rows without collapsing detail rows.
details:
It uses `OVER`, often with `PARTITION BY` and `ORDER BY`, for ranking, running totals, and per-group calculations.
example:
ROW_NUMBER() OVER (PARTITION BY CustomerID ORDER BY OrderDate DESC)
recall:
- Why do window functions keep detail rows?
- What does `PARTITION BY` define?
```

```concept-card
id: case-expression
term: CASE Expression
parents:
- advanced-query-patterns
summary:
`CASE` returns a value based on conditional logic.
details:
It is useful for labels, buckets, conditional aggregates, and derived business categories.
example:
CASE WHEN ListPrice < 100 THEN 'Budget' ELSE 'Premium' END
recall:
- When is `CASE` useful in reporting?
- What does `CASE` return?
```

```concept-card
id: pagination
term: Pagination
parents:
- advanced-query-patterns
summary:
Pagination returns one page of ordered rows at a time.
details:
SQL Server commonly uses `ORDER BY ... OFFSET ... FETCH`. The `ORDER BY` must be deterministic to avoid unstable pages.
example:
ORDER BY ListPrice DESC OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY
recall:
- Why does pagination require `ORDER BY`?
- What problem can unstable ordering cause?
```
