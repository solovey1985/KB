# Query Fundamentals

Use this page to practice the shape of a relational query before moving into engine-specific T-SQL features.

Examples use `AdventureWorksLT2019`.

## Logical Query Shape

A readable SQL query usually follows this order:

```sql
SELECT c.CustomerID, c.CompanyName, COUNT(soh.SalesOrderID) AS OrderCount
FROM SalesLT.Customer AS c
JOIN SalesLT.SalesOrderHeader AS soh
  ON soh.CustomerID = c.CustomerID
WHERE soh.OrderDate >= '2008-01-01'
GROUP BY c.CustomerID, c.CompanyName
HAVING COUNT(soh.SalesOrderID) > 1
ORDER BY OrderCount DESC;
```

The engine's logical processing order is different from the written order. `FROM` and `JOIN` identify rows, `WHERE` filters rows, `GROUP BY` groups them, `HAVING` filters groups, `SELECT` projects values, and `ORDER BY` sorts the final result.

## Filtering Rows

Use `WHERE` for row-level predicates.

```sql
SELECT ProductID, Name, Color, ListPrice
FROM SalesLT.Product
WHERE Color IN ('Black', 'Red')
  AND ListPrice BETWEEN 500 AND 1500
  AND SellEndDate IS NULL
ORDER BY ListPrice DESC;
```

Common predicates are `=`, `<>`, `<`, `>`, `IN`, `BETWEEN`, `LIKE`, `IS NULL`, and `IS NOT NULL`.

## Joining Related Rows

Joins combine rows through relationships. In AdventureWorksLT, sales order details point to products and sales order headers.

```sql
SELECT TOP 20
    soh.SalesOrderNumber,
    p.Name AS ProductName,
    sod.OrderQty,
    sod.LineTotal
FROM SalesLT.SalesOrderHeader AS soh
JOIN SalesLT.SalesOrderDetail AS sod
  ON sod.SalesOrderID = soh.SalesOrderID
JOIN SalesLT.Product AS p
  ON p.ProductID = sod.ProductID
ORDER BY soh.OrderDate DESC;
```

Use `INNER JOIN` when a match is required. Use `LEFT JOIN` when the left-side row must be kept even if related rows are missing.

## Grouping And Aggregation

`GROUP BY` turns detail rows into summary rows. `HAVING` filters after aggregation.

```sql
SELECT
    pc.Name AS CategoryName,
    COUNT(*) AS ProductCount,
    AVG(p.ListPrice) AS AverageListPrice
FROM SalesLT.ProductCategory AS pc
JOIN SalesLT.Product AS p
  ON p.ProductCategoryID = pc.ProductCategoryID
GROUP BY pc.Name
HAVING COUNT(*) >= 5
ORDER BY AverageListPrice DESC;
```

Use this pattern for reports such as sales by customer, products by category, and totals by date.
