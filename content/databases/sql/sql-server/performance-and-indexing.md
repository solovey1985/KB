# Performance And Indexing

Indexes make reads faster when they match the query shape. They also cost storage and write overhead.

## Clustered And Nonclustered Indexes

A clustered index defines the table's row order at the leaf level. A nonclustered index is a separate access path that points back to the base row.

```sql
EXEC sp_helpindex 'SalesLT.SalesOrderDetail';
```

Use clustered indexes for stable, narrow, frequently joined keys. Use nonclustered indexes for common search, join, sort, and grouping patterns.

## Query Shape Drives Index Design

Design around predicates, joins, ordering, and returned columns.

```sql
SELECT SalesOrderID, ProductID, OrderQty, LineTotal
FROM SalesLT.SalesOrderDetail
WHERE ProductID = 707
ORDER BY SalesOrderID;
```

A useful index for this pattern would start with `ProductID` and may include `SalesOrderID` if ordering is important.

## Execution Plan Clues

Read execution plans for operators such as index seek, index scan, key lookup, sort, hash match, nested loops, and missing index suggestions.

```sql
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

SELECT ProductID, SUM(LineTotal) AS TotalSales
FROM SalesLT.SalesOrderDetail
GROUP BY ProductID;

SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;
```

Measure before and after changes. Do not add indexes based only on intuition.

## Index Tradeoffs

Indexes speed up some reads but slow down writes because each insert, update, or delete may also update indexes.

Keep indexes that serve real queries. Remove duplicate, unused, or overly wide indexes after verifying workload impact.
