# Programmability And Security

T-SQL is more than ad-hoc queries. SQL Server can store reusable logic, enforce rules, and control access close to the data.

## Programmability Objects

Common objects include stored procedures, scalar functions, table-valued functions, views, and triggers.

Use stored procedures for operational commands. Use views for reusable read shapes. Use functions for reusable expressions or table expressions when they stay simple and predictable.

## Triggers And Guardrails

Triggers run automatically after or instead of data changes. They can enforce auditing or cross-row rules, but they also hide work from callers.

Prefer constraints for basic integrity. Use triggers only when the rule cannot be represented cleanly with constraints or application workflow.

## Dynamic SQL And Injection Safety

Dynamic SQL is useful for dynamic search, optional sort columns, or metadata-driven work. It is also dangerous when values are concatenated into command text.

Use `sp_executesql` with parameters for values.

```sql
DECLARE @sql nvarchar(max) = N'
SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ListPrice >= @MinPrice;';

EXEC sys.sp_executesql
    @sql,
    N'@MinPrice money',
    @MinPrice = 1000;
```

Never concatenate untrusted user input into executable SQL.

## Permissions And Roles

Grant the least privilege needed. Prefer granting execute permission on stored procedures instead of direct write access to underlying tables when a controlled operation is enough.

```sql
-- Example pattern only; run with appropriate security context.
-- GRANT EXECUTE ON OBJECT::SalesLT.usp_GetCustomerOrders TO ReportingUser;
```

Security belongs in both the application and the database boundary.
