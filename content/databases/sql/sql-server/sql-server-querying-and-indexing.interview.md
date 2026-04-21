---
title: SQL Server Querying and Indexing Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the SQL Server and T-SQL distinctions that come up most often:

- joins, grouping, and CTEs
- `WHERE` versus `HAVING`
- index design tradeoffs
- query-shape driven optimisation

## Query Foundations

```interview-question
What is the difference between `WHERE` and `HAVING` in SQL?
---
answer:
`WHERE` filters individual rows **before** grouping or aggregation happens.

`HAVING` filters grouped results **after** aggregation.

That means `WHERE` is used for row-level predicates, while `HAVING` is used for conditions such as `COUNT(*) > 10` or `SUM(Total) > 1000`.
hints:
- Think about when aggregation runs.
- One clause works on rows, the other on grouped results.
- `COUNT(...) > 1` belongs in only one of them.
```

```interview-question
Why would you use a Common Table Expression instead of writing one large query block?
---
answer:
A Common Table Expression improves readability and maintainability by naming an intermediate result set that the main query can reference.

It is also the standard way to write recursive queries for hierarchies and tree-like data.
hints:
- It helps break complex queries into smaller parts.
- One major use case is recursive querying.
- Think of a temporary named result set inside a single statement.
```

```interview-question
Why does column order matter in a composite index?
---
answer:
Composite indexes are most effective when the query filters or sorts by the leading columns in the same order the index was defined.

If a query skips the leftmost indexed column, the index often becomes much less useful for narrowing the search.
hints:
- The leftmost column matters first.
- Equality predicates usually come before range predicates.
- Query shape should drive index design.
```

```interview-question
Why is a low-cardinality column often a poor standalone index candidate?
---
answer:
A low-cardinality column has few distinct values, so it usually does not filter enough rows to justify an index on its own.

It can still be useful as part of a composite index when paired with a more selective leading column.
hints:
- Think about a column like `is_deleted`.
- An index is most valuable when it narrows the search a lot.
- Low selectivity is the main problem.
```

## Multiple Choice Questions

```interview-choice
Which join returns all rows from the left table and matching rows from the right table, filling unmatched right-side values with `NULL`?
---
options:
- INNER JOIN
- LEFT JOIN
- CROSS JOIN
correct: 1
explanation:
`LEFT JOIN` keeps every row from the left input and returns `NULL` for right-side columns when no match exists.
```

```interview-choice
Which clause is valid for filtering grouped rows based on an aggregate such as `COUNT(*) > 5`?
---
options:
- WHERE
- HAVING
- ORDER BY
correct: 1
explanation:
`HAVING` filters after grouping and aggregation, which is why it is used with aggregate predicates.
```

```interview-choice
Which index definition is most aligned with a query that filters by `account_id` and then ranges on `due_date`?
---
options:
- `(due_date, account_id)`
- `(account_id, due_date)`
- `(status, account_id)`
correct: 1
explanation:
The equality predicate on `account_id` should lead, followed by the range predicate on `due_date`.
That ordering best matches the access path described in the query.
```

## Code Completion Questions

```interview-code
language: sql
prompt: Complete the query so it returns customers with more than 5 orders.
starter:
SELECT CustomerID, COUNT(OrderID) AS OrderCount
FROM Orders
GROUP BY CustomerID

solution:
SELECT CustomerID, COUNT(OrderID) AS OrderCount
FROM Orders
GROUP BY CustomerID
HAVING COUNT(OrderID) > 5;
checks:
- includes: HAVING
- includes: COUNT(OrderID) > 5
```

```interview-code
language: sql
prompt: Complete the CTE so the outer query can join top-selling products to the product table.
starter:
WITH TopProducts AS (
    SELECT ProductID, SUM(OrderQty) AS TotalQuantity
    FROM Sales.SalesOrderDetail
    GROUP BY ProductID
)
SELECT p.ProductID, p.Name, tp.TotalQuantity
FROM TopProducts tp
 Production.Product p ON tp.ProductID = p.ProductID;
solution:
WITH TopProducts AS (
    SELECT ProductID, SUM(OrderQty) AS TotalQuantity
    FROM Sales.SalesOrderDetail
    GROUP BY ProductID
)
SELECT p.ProductID, p.Name, tp.TotalQuantity
FROM TopProducts tp
JOIN Production.Product p ON tp.ProductID = p.ProductID;
checks:
- includes: WITH TopProducts AS
- includes: JOIN Production.Product
- includes: ON tp.ProductID = p.ProductID
```

```interview-code
language: sql
prompt: Complete the index so it supports queries that filter by `customer_id` and sort by `created_at` while returning `status` and `total_amount`.
starter:
CREATE INDEX idx_orders_customer_created
ON orders (customer_id, created_at DESC)

solution:
CREATE INDEX idx_orders_customer_created
ON orders (customer_id, created_at DESC)
INCLUDE (status, total_amount);
checks:
- includes: INCLUDE
- includes: status
- includes: total_amount
```

## Study Notes

Use the section notes for the long-form explanations and examples:

- [Indexes](indexes.md)
- [Aggregate Functions](t-sql/aggregate-functions.md)
- [Common Table Expressions](t-sql/common-table-expressions.md)
- [Joins](t-sql/joins.md)
- [T-SQL Features](t-sql/features.md)
