# Ten High-Leverage SQL Server Features

This is a SQL Server adaptation of Anton Martyniuk's ["10 Rare SQL Features Every Developer Should Know"](https://antondevtips.com/blog/10-rare-sql-features-every-developer-should-know). The original examples target PostgreSQL; this guide uses T-SQL and the bundled [AdventureWorksLT2019 practice database](../database-example/README.md).

The point is not to use every feature everywhere. Use the smallest, clearest construct that accurately expresses the query or data-change problem.

## Before You Start

Restore AdventureWorksLT2019 using the [local setup guide](../database-example/README.md), then connect to that database.

Examples labelled **read-only** do not change the database. Examples that create an index or table are learning experiments: run them only in a disposable local copy and remove the object when finished. AdventureWorksLT is deliberately small, so an execution plan may not demonstrate the same benefit that a production-sized table would.

## 1. Common Table Expressions (CTEs)

A CTE gives a name to a result set for the one statement that follows it. It is useful for making multi-step query logic readable; it is not a guaranteed materialized temporary table.

**Read-only — show the best-selling products.**

```sql
;WITH ProductSales AS
(
    SELECT
        sod.ProductID,
        SUM(sod.OrderQty) AS TotalQuantity,
        SUM(sod.LineTotal) AS TotalSales
    FROM SalesLT.SalesOrderDetail AS sod
    GROUP BY sod.ProductID
),
RankedProducts AS
(
    SELECT
        ps.ProductID,
        ps.TotalQuantity,
        ps.TotalSales,
        ROW_NUMBER() OVER (ORDER BY ps.TotalSales DESC, ps.ProductID) AS SalesRank
    FROM ProductSales AS ps
)
SELECT
    rp.SalesRank,
    p.Name,
    rp.TotalQuantity,
    rp.TotalSales
FROM RankedProducts AS rp
JOIN SalesLT.Product AS p
    ON p.ProductID = rp.ProductID
WHERE rp.SalesRank <= 10
ORDER BY rp.SalesRank;
```

Start a CTE with `;WITH` when it might follow another statement in the same batch. The leading semicolon safely terminates the preceding statement. If an expensive CTE result must be reused several times, inspect the plan and consider a temporary table instead.

See also: [Common Table Expressions](common-table-expressions.md).

## 2. Window Functions

`GROUP BY` returns one row per group. A window function performs a calculation over related rows while preserving each detail row. The `OVER` clause defines the window.

**Read-only — number each customer's orders and compare each order with the previous one.**

```sql
SELECT
    soh.CustomerID,
    soh.SalesOrderID,
    soh.OrderDate,
    soh.TotalDue,
    ROW_NUMBER() OVER
    (
        PARTITION BY soh.CustomerID
        ORDER BY soh.OrderDate, soh.SalesOrderID
    ) AS OrderSequence,
    LAG(soh.TotalDue) OVER
    (
        PARTITION BY soh.CustomerID
        ORDER BY soh.OrderDate, soh.SalesOrderID
    ) AS PreviousOrderTotal,
    SUM(soh.TotalDue) OVER
    (
        PARTITION BY soh.CustomerID
        ORDER BY soh.OrderDate, soh.SalesOrderID
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS RunningCustomerTotal
FROM SalesLT.SalesOrderHeader AS soh
ORDER BY soh.CustomerID, soh.OrderDate, soh.SalesOrderID;
```

Use a tie-breaker such as `SalesOrderID` in the window ordering when the primary sort value is not unique. `ROW_NUMBER` assigns a unique sequence; `RANK` gives tied rows the same rank and leaves gaps; `DENSE_RANK` gives tied rows the same rank without gaps.

See also: [Advanced Query Patterns](advanced-query-patterns.md).

## 3. `CROSS APPLY` and `OUTER APPLY`

PostgreSQL calls this a `LATERAL` join. In SQL Server, `APPLY` lets the right-hand table expression refer to the current row on the left. It is especially expressive for "top one (or top N) row per parent" queries.

**Read-only — return every customer and, when present, their most recent order.**

```sql
SELECT
    c.CustomerID,
    c.FirstName,
    c.LastName,
    latest.SalesOrderID,
    latest.OrderDate,
    latest.TotalDue
FROM SalesLT.Customer AS c
OUTER APPLY
(
    SELECT TOP (1)
        soh.SalesOrderID,
        soh.OrderDate,
        soh.TotalDue
    FROM SalesLT.SalesOrderHeader AS soh
    WHERE soh.CustomerID = c.CustomerID
    ORDER BY soh.OrderDate DESC, soh.SalesOrderID DESC
) AS latest
ORDER BY c.CustomerID;
```

`CROSS APPLY` omits a left row when the right expression returns no row. `OUTER APPLY` retains it and returns `NULL` for the right-hand columns, analogous to the difference between an inner and left join.

## 4. `GROUPING SETS`, `ROLLUP`, and `CUBE`

These forms calculate multiple report levels in one aggregation. Prefer `GROUPING SETS` when you need an exact list; use `ROLLUP` for hierarchical subtotals; use `CUBE` only when every combination of dimensions is genuinely useful.

**Read-only — report detailed, per-customer, per-status, and grand totals together.**

```sql
SELECT
    CASE WHEN GROUPING(soh.CustomerID) = 1
         THEN 'All customers'
         ELSE CONVERT(varchar(12), soh.CustomerID)
    END AS Customer,
    CASE WHEN GROUPING(soh.Status) = 1
         THEN 'All statuses'
         ELSE CONVERT(varchar(12), soh.Status)
    END AS OrderStatus,
    COUNT_BIG(*) AS OrderCount,
    SUM(soh.TotalDue) AS TotalDue
FROM SalesLT.SalesOrderHeader AS soh
GROUP BY GROUPING SETS
(
    (soh.CustomerID, soh.Status),
    (soh.CustomerID),
    (soh.Status),
    ()
)
ORDER BY
    GROUPING(soh.CustomerID),
    soh.CustomerID,
    GROUPING(soh.Status),
    soh.Status;
```

Do not infer a subtotal from a `NULL` grouping value alone: source data can also contain `NULL`. `GROUPING(column)` identifies a value introduced by the grouping operation.

## 5. Conditional Aggregates Instead of `FILTER`

PostgreSQL's aggregate `FILTER (WHERE ...)` clause is not T-SQL syntax. Put the condition inside the aggregate with `CASE` instead. `COUNT(CASE WHEN ... THEN 1 END)` counts only matching rows because `COUNT` ignores `NULL`.

**Read-only — count order states side by side for each customer.**

```sql
SELECT
    soh.CustomerID,
    COUNT(*) AS TotalOrders,
    COUNT(CASE WHEN soh.Status = 1 THEN 1 END) AS InProcessOrders,
    COUNT(CASE WHEN soh.Status = 5 THEN 1 END) AS ShippedOrders,
    COUNT(CASE WHEN soh.Status = 6 THEN 1 END) AS CancelledOrders,
    SUM(CASE WHEN soh.Status = 5 THEN soh.TotalDue ELSE 0 END) AS ShippedTotalDue
FROM SalesLT.SalesOrderHeader AS soh
GROUP BY soh.CustomerID
ORDER BY soh.CustomerID;
```

The status codes are the AdventureWorksLT sample's values; in an application, avoid hiding business meanings in a query if a lookup or well-named view is available.

## 6. Synchronizing Rows: `MERGE` and Safer Upsert Design

SQL Server's single-statement counterpart to `INSERT ... ON CONFLICT` is `MERGE`. It can synchronize a target from a source, but it is not an automatic default for every upsert. A unique key, a correct source-to-target match, concurrency testing, and a terminating semicolon are essential.

**Write — a transaction-scoped practice target populated from AdventureWorksLT products.**

```sql
BEGIN TRANSACTION;

CREATE TABLE #ProductPriceTarget
(
    ProductID int NOT NULL PRIMARY KEY,
    ListPrice money NOT NULL,
    ModifiedDate datetime NOT NULL
);

INSERT #ProductPriceTarget (ProductID, ListPrice, ModifiedDate)
SELECT TOP (3) ProductID, ListPrice, ModifiedDate
FROM SalesLT.Product
ORDER BY ProductID;

CREATE TABLE #ProductPriceSource
(
    ProductID int NOT NULL PRIMARY KEY,
    ListPrice money NOT NULL,
    ModifiedDate datetime NOT NULL
);

INSERT #ProductPriceSource (ProductID, ListPrice, ModifiedDate)
SELECT TOP (4) ProductID, ListPrice * 0.95, ModifiedDate
FROM SalesLT.Product
ORDER BY ProductID;

MERGE #ProductPriceTarget WITH (HOLDLOCK) AS target
USING #ProductPriceSource AS source
    ON target.ProductID = source.ProductID
WHEN MATCHED THEN
    UPDATE SET
        ListPrice = source.ListPrice,
        ModifiedDate = source.ModifiedDate
WHEN NOT MATCHED BY TARGET THEN
    INSERT (ProductID, ListPrice, ModifiedDate)
    VALUES (source.ProductID, source.ListPrice, source.ModifiedDate)
OUTPUT $action AS ChangeAction, inserted.ProductID, inserted.ListPrice;

ROLLBACK TRANSACTION;
```

`HOLDLOCK` provides serializable semantics for the target read in this example, but it can increase blocking. For high-concurrency OLTP paths, a carefully designed transaction with separate `UPDATE` and `INSERT ... WHERE NOT EXISTS` statements is often easier to reason about and can scale better. Never let multiple source rows match the same target key.

## 7. JSON: Generate, Read, and Relationalize

SQL Server commonly stores JSON text in `nvarchar` and exposes it through `ISJSON`, `JSON_VALUE`, `JSON_QUERY`, `JSON_MODIFY`, and `OPENJSON`. JSON is appropriate for flexible payloads; keep stable, frequently joined or constrained attributes relational.

**Read-only — turn AdventureWorksLT orders into JSON, then turn the JSON array back into rows.**

```sql
DECLARE @OrdersJson nvarchar(max) =
(
    SELECT TOP (3)
        soh.SalesOrderID,
        soh.CustomerID,
        soh.OrderDate,
        soh.TotalDue
    FROM SalesLT.SalesOrderHeader AS soh
    ORDER BY soh.SalesOrderID
    FOR JSON PATH
);

SELECT
    orders.SalesOrderID,
    orders.CustomerID,
    orders.OrderDate,
    orders.TotalDue
FROM OPENJSON(@OrdersJson)
WITH
(
    SalesOrderID int '$.SalesOrderID',
    CustomerID int '$.CustomerID',
    OrderDate datetime '$.OrderDate',
    TotalDue money '$.TotalDue'
) AS orders;
```

Use `JSON_VALUE` for one scalar value and `OPENJSON ... WITH` when an array or object needs typed relational columns. Validate externally supplied text with `ISJSON` before accepting it as a JSON payload.

## 8. Computed Columns

A computed column derives its value from an expression stored in the table definition. Mark it `PERSISTED` when the deterministic expression should be stored and maintained by SQL Server; this can enable indexing when the expression satisfies the relevant requirements.

**Write — create a disposable practice table seeded from AdventureWorksLT price data.**

```sql
CREATE TABLE dbo.ProductPricePractice
(
    ProductID int NOT NULL PRIMARY KEY,
    BasePrice decimal(19, 4) NOT NULL,
    DiscountRate decimal(5, 4) NOT NULL,
    DiscountedPrice AS
        CONVERT(decimal(19, 4), BasePrice * (1 - DiscountRate)) PERSISTED
);

INSERT dbo.ProductPricePractice (ProductID, BasePrice, DiscountRate)
SELECT TOP (10)
    p.ProductID,
    CONVERT(decimal(19, 4), p.ListPrice),
    CONVERT(decimal(5, 4), 0.1000)
FROM SalesLT.Product AS p
ORDER BY p.ProductID;

SELECT ProductID, BasePrice, DiscountRate, DiscountedPrice
FROM dbo.ProductPricePractice
ORDER BY ProductID;

DROP TABLE dbo.ProductPricePractice;
```

The computed value is not independently writable, so the formula cannot drift between application write paths. Do not add `PERSISTED` merely by habit: it trades storage and write work for read or indexing benefits. Indexed computed columns also have determinism, precision, data-type, ownership, and session `SET` option requirements.

## 9. `TABLESAMPLE`

`TABLESAMPLE SYSTEM` quickly selects a page-based sample. It is useful for exploratory work on a large table, not for a statistically uniform sample or a substitute for `ORDER BY NEWID()` when every row must have an equal chance.

**Read-only — inspect an approximate sample of sales-order details.**

```sql
SELECT
    sod.SalesOrderID,
    sod.SalesOrderDetailID,
    sod.ProductID,
    sod.OrderQty,
    sod.LineTotal
FROM SalesLT.SalesOrderDetail AS sod
TABLESAMPLE SYSTEM (10 PERCENT) REPEATABLE (20260910);
```

`REPEATABLE` makes the page sample reproducible only while the table's physical layout remains sufficiently unchanged. On the compact AdventureWorksLT tables, sampling may return a surprisingly uneven number of rows; use a much larger table to experience its intended performance benefit.

## 10. Filtered Indexes

SQL Server's counterpart to a partial index is a filtered nonclustered index. It stores only rows satisfying a simple predicate, which can reduce its size, maintenance cost, and read cost for a small, frequently queried subset.

**Write — try a filtered index for in-process orders in a disposable copy.**

```sql
CREATE INDEX IX_SalesOrderHeader_InProcess_Customer_OrderDate
ON SalesLT.SalesOrderHeader (CustomerID, OrderDate DESC)
INCLUDE (TotalDue)
WHERE Status = 1;

SELECT
    soh.CustomerID,
    soh.SalesOrderID,
    soh.OrderDate,
    soh.TotalDue
FROM SalesLT.SalesOrderHeader AS soh
WHERE soh.Status = 1
ORDER BY soh.CustomerID, soh.OrderDate DESC;

DROP INDEX IX_SalesOrderHeader_InProcess_Customer_OrderDate
ON SalesLT.SalesOrderHeader;
```

The query predicate needs to be compatible with the filter for the optimizer to consider the index. Choose this pattern only after inspecting an actual workload and execution plan; a filtered index adds write overhead and the tiny sample database may not choose it.

## Choosing the Feature

| Need | T-SQL feature |
| --- | --- |
| Name readable single-statement steps | CTE |
| Rank, compare, or accumulate without losing detail rows | Window function |
| Find top rows for every parent | `CROSS APPLY` / `OUTER APPLY` |
| Produce several summary levels | `GROUPING SETS`, `ROLLUP`, or `CUBE` |
| Count or sum different subsets side by side | `CASE` inside an aggregate |
| Synchronize a well-understood source and target | `MERGE`, with explicit concurrency design |
| Query a flexible document payload | JSON functions and `OPENJSON` |
| Centralize a derived value | Computed column |
| Quickly inspect part of a large table | `TABLESAMPLE SYSTEM` |
| Optimize a predictable small subset | Filtered index |

## References

- [Source article: 10 Rare SQL Features Every Developer Should Know](https://antondevtips.com/blog/10-rare-sql-features-every-developer-should-know)
- [Common Table Expressions — Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/queries/with-common-table-expression-transact-sql)
- [GROUP BY, including `GROUPING SETS`, `ROLLUP`, and `CUBE` — Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/queries/select-group-by-transact-sql)
- [`MERGE` and its concurrency considerations — Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql)
- [Work with JSON data in SQL Server — Microsoft Learn](https://learn.microsoft.com/en-us/sql/relational-databases/json/json-data-sql-server)
- [Computed-column index requirements — Microsoft Learn](https://learn.microsoft.com/en-us/sql/relational-databases/indexes/indexes-on-computed-columns)
- [`CREATE INDEX`, including filtered indexes — Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/statements/create-index-transact-sql)
