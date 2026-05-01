# Advanced Query Patterns

Advanced T-SQL patterns help make complex reports readable and correct.

## Subqueries And CTEs

A subquery nests one query inside another. A CTE names an intermediate result for one statement.

```sql
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
```

Use CTEs to clarify multi-step logic, especially when the same intermediate idea would otherwise be repeated.

## Recursive CTEs

Recursive CTEs walk hierarchical data. AdventureWorksLT product categories have parent categories.

```sql
WITH CategoryTree AS (
    SELECT ProductCategoryID, ParentProductCategoryID, Name, 0 AS Level
    FROM SalesLT.ProductCategory
    WHERE ParentProductCategoryID IS NULL

    UNION ALL

    SELECT c.ProductCategoryID, c.ParentProductCategoryID, c.Name, ct.Level + 1
    FROM SalesLT.ProductCategory AS c
    JOIN CategoryTree AS ct
      ON c.ParentProductCategoryID = ct.ProductCategoryID
)
SELECT *
FROM CategoryTree
ORDER BY Level, Name;
```

## Window Functions

Window functions calculate across related rows without collapsing detail rows.

```sql
SELECT
    soh.CustomerID,
    soh.SalesOrderID,
    soh.TotalDue,
    ROW_NUMBER() OVER (PARTITION BY soh.CustomerID ORDER BY soh.OrderDate DESC) AS OrderRank,
    SUM(soh.TotalDue) OVER (PARTITION BY soh.CustomerID) AS CustomerTotalDue
FROM SalesLT.SalesOrderHeader AS soh;
```

Use them for ranking, running totals, moving averages, and top-N-per-group queries.

## CASE, Sets, And Paging

`CASE` labels rows, `UNION ALL` appends compatible results without removing duplicates, and `OFFSET...FETCH` pages ordered rows.

```sql
SELECT ProductID, Name, ListPrice,
       CASE
           WHEN ListPrice < 100 THEN 'Budget'
           WHEN ListPrice < 1000 THEN 'Standard'
           ELSE 'Premium'
       END AS PriceBand
FROM SalesLT.Product
ORDER BY ListPrice DESC
OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY;
```

Always use a deterministic `ORDER BY` with paging.
