---
title: SQL Server Querying and Indexing Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page condenses the main SQL Server ideas in this section into a study map built around query composition and index design.

## Core Map

```concept-card
id: sql-server-querying
term: SQL Server Querying and Indexing
children:
- t-sql
- joins
- aggregate-functions
- common-table-expression
- index-design
related:
- group-by-having
- query-plan-analysis
summary:
SQL Server querying and indexing is the discipline of shaping T-SQL queries and supporting access paths so data can be retrieved correctly and efficiently.
details:
The material in this section focuses on relational query building, grouping and aggregation, intermediate result shaping with CTEs, and designing indexes around real query patterns.
mnemonic:
Query first, index second, measure always.
recall:
- Which subtopics support query correctness?
- Which subtopics support query performance?
- Why should index design follow actual query shapes?
```

```concept-card
id: t-sql
term: T-SQL
aliases:
- Transact-SQL
parents:
- sql-server-querying
children:
- group-by-having
- common-table-expression
related:
- joins
- aggregate-functions
summary:
T-SQL is Microsoft's SQL dialect for SQL Server, adding procedural features and SQL Server specific capabilities on top of standard SQL.
details:
Besides core querying syntax, T-SQL includes variables, control flow, procedures, triggers, transactions, temporary objects, and other SQL Server specific features.
example:
DECLARE @Total int = 10;
IF @Total > 5 SELECT @Total;
mnemonic:
SQL plus server-side control.
recall:
- What does T-SQL add beyond plain SQL?
- Which language features make T-SQL more procedural?
```

```concept-card
id: aggregate-functions
term: Aggregate Functions
parents:
- sql-server-querying
related:
- group-by-having
summary:
Aggregate functions compute a single value from a set of rows, such as a count, sum, average, minimum, or maximum.
details:
They are commonly paired with grouping so that each group produces its own summary values.
example:
SELECT CustomerID, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerID;
mnemonic:
Many rows in, one summary out.
recall:
- Which aggregate functions appear most often in reporting queries?
- Why are aggregates commonly paired with grouping?
```

```concept-card
id: group-by-having
term: GROUP BY and HAVING
parents:
- t-sql
related:
- aggregate-functions
summary:
`GROUP BY` partitions rows into groups, and `HAVING` filters those grouped results after aggregation.
details:
`WHERE` filters rows before grouping, while `HAVING` is used for predicates that depend on aggregates such as `COUNT(*)` or `SUM(...)`.
example:
SELECT CustomerID, COUNT(*)
FROM Orders
GROUP BY CustomerID
HAVING COUNT(*) > 5;
mnemonic:
Where before, having after.
recall:
- What is the difference between `WHERE` and `HAVING`?
- Why must aggregate filters use `HAVING`?
```

```concept-card
id: joins
term: Joins
parents:
- sql-server-querying
children:
- inner-join
- left-join
- cross-join
related:
- common-table-expression
summary:
Joins combine rows from two or more tables based on related columns.
details:
Different join types determine whether unmatched rows are discarded, preserved with `NULL`, or multiplied into combinations.
example:
SELECT o.OrderID, c.CustomerName
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID;
mnemonic:
Join rows by relationship, not by luck.
recall:
- Which join returns only matches?
- Which join preserves unmatched rows from the left side?
```

```concept-card
id: inner-join
term: INNER JOIN
parents:
- joins
related:
- left-join
summary:
`INNER JOIN` returns only rows that satisfy the join condition in both inputs.
details:
It is the default join choice when you want only matching related records.
example:
SELECT *
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID;
mnemonic:
Inner means intersection.
recall:
- When is `INNER JOIN` the right default?
```

```concept-card
id: left-join
term: LEFT JOIN
parents:
- joins
related:
- inner-join
summary:
`LEFT JOIN` returns every row from the left input and matching rows from the right input, using `NULL` when no match exists.
details:
It is useful when you must preserve the driving set even if related data is missing.
example:
SELECT s.StudentName, sc.Score
FROM Students s
LEFT JOIN Scores sc ON s.StudentID = sc.StudentID;
mnemonic:
Keep the left, fill the right.
recall:
- Why does `LEFT JOIN` preserve the left side?
- When do right-side columns become `NULL`?
```

```concept-card
id: cross-join
term: CROSS JOIN
parents:
- joins
related:
- inner-join
summary:
`CROSS JOIN` produces the Cartesian product of two inputs.
details:
Because every row on one side pairs with every row on the other, it can expand result size dramatically.
example:
SELECT c.ColorName, s.SizeName
FROM Colors c
CROSS JOIN Sizes s;
mnemonic:
Every left with every right.
recall:
- Why can `CROSS JOIN` become dangerous on large tables?
```

```concept-card
id: common-table-expression
term: Common Table Expression
aliases:
- CTE
parents:
- sql-server-querying
related:
- t-sql
- joins
summary:
A Common Table Expression is a named intermediate result set defined with `WITH` and used by the main query.
details:
CTEs improve readability, help structure complex queries, and enable recursive querying for hierarchies.
example:
WITH TopProducts AS (
  SELECT ProductID, SUM(OrderQty) AS TotalQty
  FROM Sales.SalesOrderDetail
  GROUP BY ProductID
)
SELECT * FROM TopProducts;
mnemonic:
Name the middle, simplify the whole.
recall:
- Why does a CTE improve readability?
- Which kind of query especially depends on recursive CTEs?
```

```concept-card
id: index-design
term: Index Design
parents:
- sql-server-querying
children:
- composite-index-order
- covering-index
- low-selectivity-column
related:
- query-plan-analysis
summary:
Index design is the process of choosing keys and included columns that support important query access patterns without creating unnecessary write overhead.
details:
Good indexes follow actual filtering, join, sorting, and projection patterns rather than being added mechanically to many columns.
mnemonic:
Index for workload, not for decoration.
recall:
- Why can too many indexes hurt a write-heavy table?
- Why should indexes be designed from query shapes instead of schema alone?
```

```concept-card
id: composite-index-order
term: Composite Index Order
parents:
- index-design
related:
- low-selectivity-column
summary:
Composite index order determines how effectively a multi-column index can narrow and sort data for a query.
details:
The leading columns matter most. Queries that filter by the leftmost keys usually benefit much more than queries that skip them.
example:
CREATE INDEX idx_orders_customer_status_created
ON orders (customer_id, status, created_at);
mnemonic:
Leftmost leads the search.
recall:
- Why are leftmost columns so important?
- Why do equality predicates often come before range predicates?
```

```concept-card
id: covering-index
term: Covering Index
parents:
- index-design
related:
- composite-index-order
summary:
A covering index contains enough key and included columns to satisfy a query without extra lookups to the base table for projected columns.
details:
It can speed up hot read paths, but it increases index size and write maintenance cost.
example:
CREATE INDEX idx_orders_customer_created
ON orders (customer_id, created_at DESC)
INCLUDE (status, total_amount);
mnemonic:
Cover reads, pay writes.
recall:
- What problem does a covering index try to avoid?
- What is the tradeoff of adding included columns?
```

```concept-card
id: low-selectivity-column
term: Low Selectivity Column
parents:
- index-design
related:
- composite-index-order
summary:
A low selectivity column has few distinct values and often performs poorly as a standalone index key.
details:
Columns such as simple flags are usually more useful when paired with a more selective leading column in a composite index.
example:
CREATE INDEX idx_users_tenant_deleted
ON users (tenant_id, is_deleted);
mnemonic:
Weak alone, useful in company.
recall:
- Why is a low-cardinality flag a weak standalone index?
- When can that same column still help inside a composite index?
```

```concept-card
id: query-plan-analysis
term: Query Plan Analysis
parents:
- sql-server-querying
related:
- index-design
summary:
Query plan analysis is the practice of validating index and query decisions by examining how the optimizer actually executes the statement.
details:
It keeps optimisation grounded in measurement instead of intuition and helps confirm whether an index is really being used effectively.
example:
EXPLAIN
SELECT *
FROM invoices
WHERE account_id = 42;
mnemonic:
Trust the plan, not the guess.
recall:
- Why is `EXPLAIN` or the execution plan more trustworthy than intuition?
- What decision can a query plan validate after adding an index?
```
