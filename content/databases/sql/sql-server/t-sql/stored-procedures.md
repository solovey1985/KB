# Stored Procedures

Stored procedures are named T-SQL routines stored in SQL Server. They are useful when a database operation needs a stable interface, controlled permissions, error handling, and repeatable execution.

Examples use `AdventureWorksLT2019`.

## Procedure Shape

Use `CREATE OR ALTER PROCEDURE` during practice so the script can be rerun.

```sql
CREATE OR ALTER PROCEDURE dbo.usp_GetCustomerOrders
    @CustomerID int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        soh.SalesOrderID,
        soh.SalesOrderNumber,
        soh.OrderDate,
        soh.TotalDue
    FROM SalesLT.SalesOrderHeader AS soh
    WHERE soh.CustomerID = @CustomerID
    ORDER BY soh.OrderDate DESC;
END;
GO

EXEC dbo.usp_GetCustomerOrders @CustomerID = 29485;
```

`SET NOCOUNT ON` avoids extra row-count messages. That makes procedure output easier for applications and scripts to consume.

## Parameters And Result Sets

Input parameters pass values into the procedure. Output parameters return scalar values. Result sets return rows.

```sql
CREATE OR ALTER PROCEDURE dbo.usp_GetCustomerSalesTotal
    @CustomerID int,
    @TotalDue money OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @TotalDue = COALESCE(SUM(TotalDue), 0)
    FROM SalesLT.SalesOrderHeader
    WHERE CustomerID = @CustomerID;
END;
GO

DECLARE @Total money;
EXEC dbo.usp_GetCustomerSalesTotal
    @CustomerID = 29485,
    @TotalDue = @Total OUTPUT;

SELECT @Total AS CustomerTotalDue;
```

Prefer explicit parameter names when calling procedures. It protects callers when optional parameters are added later.

## Transactions And Error Handling

Procedures that change data should either own the transaction clearly or document that the caller owns it.

```sql
CREATE OR ALTER PROCEDURE dbo.usp_AdjustProductListPrice
    @ProductID int,
    @PercentChange decimal(9,4)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        UPDATE SalesLT.Product
        SET ListPrice = ListPrice * (1 + @PercentChange)
        WHERE ProductID = @ProductID;

        IF @@ROWCOUNT <> 1
            THROW 50001, 'Expected exactly one product row.', 1;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO
```

For learning, execute data-changing procedures inside an outer transaction and roll back.

```sql
BEGIN TRAN;
EXEC dbo.usp_AdjustProductListPrice @ProductID = 680, @PercentChange = 0.05;
SELECT ProductID, Name, ListPrice FROM SalesLT.Product WHERE ProductID = 680;
ROLLBACK;
```

## Security, Performance, And Maintenance

Stored procedures can provide a controlled boundary. A user can receive permission to execute a procedure without receiving direct write access to the underlying tables.

Performance still depends on query shape, indexes, parameter selectivity, and plan quality. Procedures are not automatically faster than ad-hoc SQL.

Use these habits:

- Keep procedures focused on one business operation.
- Validate inputs near the top.
- Avoid dynamic SQL unless it is necessary.
- Use `sp_executesql` with parameters when dynamic SQL is necessary.
- Version procedure scripts in source control.
- Include examples for expected result sets and edge cases.

```sql
-- Example permission pattern; run only with an appropriate security setup.
-- GRANT EXECUTE ON OBJECT::dbo.usp_GetCustomerOrders TO ReportingUser;
```

Stored procedures are a good fit for stable operations such as order lookup, safe status changes, administrative routines, and report entry points.
