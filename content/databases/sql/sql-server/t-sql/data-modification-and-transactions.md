# Data Modification And Transactions

Use T-SQL data modification carefully. Practice against AdventureWorksLT with transactions and `ROLLBACK` unless you intentionally want to persist changes.

## INSERT, UPDATE, DELETE

`INSERT`, `UPDATE`, and `DELETE` change table data. Always include a specific predicate for updates and deletes.

```sql
BEGIN TRAN;

UPDATE SalesLT.Product
SET ListPrice = ListPrice * 1.05
WHERE ProductID = 680;

SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ProductID = 680;

ROLLBACK;
```

Run the `SELECT` first when possible, then reuse the same `WHERE` clause in the data modification statement.

## Explicit Transactions

A transaction groups statements into one unit of work.

```sql
BEGIN TRAN;

DELETE FROM SalesLT.ProductDescription
WHERE ProductDescriptionID = -1;

ROLLBACK;
```

Use `COMMIT` only after verifying the affected rows and business rule. Use `ROLLBACK` to undo uncommitted changes.

## Isolation, Locks, And Blocking

SQL Server uses locks to protect consistency. Long transactions can block other sessions.

Keep transactions short. Do not wait for user input while a transaction is open. Add useful indexes so updates and deletes find target rows without scanning too much data.

## TRY...CATCH Safety

Use `TRY...CATCH` to roll back when an error happens inside a transaction.

```sql
BEGIN TRY
    BEGIN TRAN;

    UPDATE SalesLT.Product
    SET ListPrice = ListPrice * 1.05
    WHERE ProductID = 680;

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
```

This pattern keeps failures from leaving partial work open.
