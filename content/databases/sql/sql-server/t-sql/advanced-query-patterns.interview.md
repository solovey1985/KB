---
title: T-SQL Advanced Query Patterns Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise CTEs, window functions, conditional expressions, and pagination.

Study pages: [Advanced Query Patterns](advanced-query-patterns.md) | [Concept Map](advanced-query-patterns.concept.md)

## CTEs And Subqueries

```interview-question
What is a Common Table Expression?
---
answer:
A Common Table Expression, or CTE, is a named temporary result set available to a single statement.

It helps break complex logic into readable steps and is also used for recursive queries.
hints:
- It starts with `WITH`.
- It exists for one statement.
- It can be recursive.
```

```interview-choice
Which advanced query pattern is used to walk parent-child hierarchies?
---
options:
- Recursive CTE
- `UNION ALL` by itself
- `ORDER BY OFFSET`
correct: 0
explanation:
A recursive CTE has an anchor part and a recursive part, making it appropriate for hierarchy traversal.
```

```interview-code
language: sql
prompt: Complete the CTE query so it returns top products by sales.
starter:
WITH ProductSales AS (
    SELECT ProductID, SUM(LineTotal) AS TotalSales
    FROM SalesLT.SalesOrderDetail
    GROUP BY ProductID
)
SELECT TOP 10 p.Name, ps.TotalSales
FROM ProductSales AS ps
JOIN SalesLT.Product AS p
  ON
solution:
WITH ProductSales AS (
    SELECT ProductID, SUM(LineTotal) AS TotalSales
    FROM SalesLT.SalesOrderDetail
    GROUP BY ProductID
)
SELECT TOP 10 p.Name, ps.TotalSales
FROM ProductSales AS ps
JOIN SalesLT.Product AS p
  ON p.ProductID = ps.ProductID
ORDER BY ps.TotalSales DESC;
checks:
- includes: WITH ProductSales AS
- includes: ON p.ProductID = ps.ProductID
- includes: ORDER BY ps.TotalSales DESC
```

## Window Functions

```interview-question
What is a window function?
---
answer:
A window function calculates across a set of related rows while preserving the individual rows in the result.

It uses `OVER`, often with `PARTITION BY` and `ORDER BY`, for ranking, running totals, and per-group calculations.
hints:
- It does not collapse rows like `GROUP BY`.
- It uses `OVER`.
- Ranking is a common use case.
```

```interview-choice
Which function assigns a unique sequence number within each partition?
---
options:
- `ROW_NUMBER()`
- `COUNT(*)`
- `COALESCE()`
correct: 0
explanation:
`ROW_NUMBER()` assigns a unique row number based on the window's ordering.
```

```interview-code
language: sql
prompt: Complete the window expression to rank orders per customer by newest order first.
starter:
SELECT CustomerID, SalesOrderID, OrderDate,
       ROW_NUMBER() OVER (
       ) AS OrderRank
FROM SalesLT.SalesOrderHeader;
solution:
SELECT CustomerID, SalesOrderID, OrderDate,
       ROW_NUMBER() OVER (PARTITION BY CustomerID ORDER BY OrderDate DESC) AS OrderRank
FROM SalesLT.SalesOrderHeader;
checks:
- includes: PARTITION BY CustomerID
- includes: ORDER BY OrderDate DESC
- includes: ROW_NUMBER() OVER
```

## Shaping Output

```interview-question
When would you use a `CASE` expression?
---
answer:
Use `CASE` when a query needs to return a value based on conditional logic.

Common uses include labels, price bands, flags, conditional summaries, and report-friendly categories.
hints:
- It returns a value.
- It can create labels.
- It is useful in reports.
```

```interview-question
Why must pagination use a deterministic `ORDER BY`?
---
answer:
Pagination returns slices of ordered rows. If the order is unstable, rows can appear on multiple pages or be skipped between requests.

Use enough sort columns to produce a predictable sequence.
hints:
- Pages are based on order.
- Ties can move around.
- Stable sort keys prevent duplicates and gaps.
```
