---
title: SQL Server Performance And Indexing Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise SQL Server performance and indexing decisions.

Study pages: [Performance And Indexing](performance-and-indexing.md) | [Concept Map](performance-and-indexing.concept.md)

## Index Basics

```interview-question
What is the difference between a clustered and nonclustered index?
---
answer:
A clustered index defines the table's row order at the leaf level, so a table can have only one.

A nonclustered index is a separate access path that points back to the base row and can be created for different query patterns.
hints:
- One defines row order.
- One is separate from the base table.
- A table can have many of only one type.
```

```interview-question
What does it mean for an index to cover a query?
---
answer:
An index covers a query when it contains all columns needed to filter, join, sort, and return the result.

This can avoid extra lookups to the base table.
hints:
- All needed columns are present.
- Lookups can be avoided.
- Included columns often help.
```

```interview-choice
Which index key order best matches a query filtering by `ProductID` and ordering by `SalesOrderID`?
---
options:
- `(SalesOrderID, ProductID)`
- `(ProductID, SalesOrderID)`
- `(LineTotal, OrderQty)`
correct: 1
explanation:
The equality filter on `ProductID` should lead, followed by `SalesOrderID` to support ordering within that product.
```

## Measuring Performance

```interview-question
Why should you measure a query before and after adding an index?
---
answer:
Measurement confirms whether the index actually improved the workload and whether it introduced tradeoffs.

Without before-and-after evidence, index changes are guesses.
hints:
- Tuning should be evidence-based.
- Reads and timing can change.
- Indexes have costs.
```

```interview-code
language: sql
prompt: Complete the measurement wrapper around the grouped query.
starter:
SET STATISTICS IO ON;

SELECT ProductID, SUM(LineTotal) AS TotalSales
FROM SalesLT.SalesOrderDetail
GROUP BY ProductID;

solution:
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

SELECT ProductID, SUM(LineTotal) AS TotalSales
FROM SalesLT.SalesOrderDetail
GROUP BY ProductID;

SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;
checks:
- includes: SET STATISTICS TIME ON
- includes: SET STATISTICS IO OFF
- includes: SET STATISTICS TIME OFF
```

```interview-question
What execution plan operators often deserve attention when tuning?
---
answer:
Operators such as large scans, expensive sorts, key lookups, hash joins, and spills often deserve attention.

They are not automatically wrong, but they show where the query is doing significant work.
hints:
- Scans are not always bad.
- Sorts can be expensive.
- Lookups can multiply cost.
```

## Tradeoffs

```interview-question
How can indexing hurt performance?
---
answer:
Indexes consume storage and must be maintained during inserts, updates, and deletes.

Too many or overly wide indexes can slow writes and increase maintenance work.
hints:
- Indexes are not free.
- Writes update indexes too.
- Wide indexes cost more.
```

```interview-choice
Which statement is the best indexing habit?
---
options:
- Add an index for every column that appears in a query.
- Design indexes around real query shapes and measure the effect.
- Avoid indexes because they always slow databases down.
correct: 1
explanation:
Indexing should be workload-driven and evidence-based. Indexes help some queries and hurt some writes.
```
