---
title: T-SQL Stored Procedures Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise stored procedures as a standalone T-SQL topic.

Study pages: [Stored Procedures](stored-procedures.md) | [Concept Map](stored-procedures.concept.md)

## Procedure Basics

```interview-question
What is a stored procedure in SQL Server?
---
answer:
A stored procedure is a named T-SQL routine stored in SQL Server and executed as a unit.

It is used to expose a stable database operation with parameters, reusable logic, controlled permissions, and optional transaction/error handling.
hints:
- It is stored in the database.
- It is invoked with `EXEC`.
- Think stable operation boundary.
```

Related concepts: [Stored Procedure](stored-procedures.concept.md#stored-procedure)

```interview-question
Why is `SET NOCOUNT ON` commonly used inside stored procedures?
---
answer:
`SET NOCOUNT ON` suppresses extra row-count messages such as `(3 rows affected)`.

That keeps procedure output cleaner for applications and scripts that expect result sets or output parameters.
hints:
- It does not stop result sets.
- It affects row-count messages.
- It helps callers consume output predictably.
```

```interview-choice
Which statement best describes a stored procedure contract?
---
options:
- Only the procedure name matters; result columns can change freely.
- Parameters, result set shape, side effects, and errors are part of the contract.
- A stored procedure contract exists only when the procedure changes data.
correct: 1
explanation:
Callers depend on parameter names and types, returned columns, side effects, permissions, and failure behavior.
```

## Parameters And Results

```interview-question
When should you use an output parameter instead of returning a result set?
---
answer:
Use an output parameter when the procedure needs to return a single scalar value such as a total, generated identifier, status value, or calculated amount.

Use a result set when the caller needs rows and columns.
hints:
- Output parameters are scalar.
- Result sets are tabular.
- Totals and status values are common examples.
```

Related concepts: [Output Parameter](stored-procedures.concept.md#output-parameter), [Procedure Result Set](stored-procedures.concept.md#procedure-result-set)

```interview-choice
What is the safest way to call a procedure that has multiple parameters?
---
options:
- Pass values positionally without parameter names.
- Use named parameters such as `@CustomerID = 29485`.
- Concatenate parameter values into a string and execute it.
correct: 1
explanation:
Named parameters make calls readable and reduce breakage when optional parameters are added or reordered.
```

```interview-code
language: sql
prompt: Complete the procedure so it returns AdventureWorksLT orders for one customer.
starter:
CREATE OR ALTER PROCEDURE dbo.usp_GetCustomerOrders
    @CustomerID int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SalesOrderID, SalesOrderNumber, OrderDate, TotalDue
    FROM SalesLT.SalesOrderHeader
    WHERE
END;
solution:
CREATE OR ALTER PROCEDURE dbo.usp_GetCustomerOrders
    @CustomerID int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SalesOrderID, SalesOrderNumber, OrderDate, TotalDue
    FROM SalesLT.SalesOrderHeader
    WHERE CustomerID = @CustomerID
    ORDER BY OrderDate DESC;
END;
checks:
- includes: WHERE CustomerID = @CustomerID
- includes: ORDER BY OrderDate DESC
- includes: SET NOCOUNT ON
```

## Transactions And Errors

```interview-question
Why should a data-changing stored procedure make transaction ownership clear?
---
answer:
The caller and procedure need to agree on who controls `BEGIN TRAN`, `COMMIT`, and `ROLLBACK`.

Unclear transaction ownership can leave partial work, cause unexpected rollbacks, or hold locks longer than intended.
hints:
- Think caller-owned versus procedure-owned transaction.
- Locks can be held too long.
- Partial work is the main risk.
```

Related concepts: [Procedure Transaction Scope](stored-procedures.concept.md#procedure-transaction-scope)

```interview-question
What should a `CATCH` block usually do in a data-changing stored procedure?
---
answer:
It should roll back open work when `@@TRANCOUNT > 0`, then rethrow the error with `THROW` so the caller sees the failure.
hints:
- Clean up the transaction.
- Do not silently hide the error.
- `THROW` preserves failure behavior.
```

Related concepts: [Procedure Error Handling](stored-procedures.concept.md#procedure-error-handling)

```interview-code
language: sql
prompt: Complete the catch block so the procedure rolls back open work and rethrows.
starter:
BEGIN CATCH
    IF @@TRANCOUNT > 0

END CATCH;
solution:
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
checks:
- includes: @@TRANCOUNT > 0
- includes: ROLLBACK
- includes: THROW
```

## Security And Dynamic SQL

```interview-question
How can stored procedures support least-privilege security?
---
answer:
A user can be granted permission to execute a stored procedure without receiving direct permissions on the underlying tables.

That lets the database expose approved operations while limiting unrestricted reads or writes.
hints:
- Think `GRANT EXECUTE`.
- The caller does not necessarily need direct table access.
- The procedure becomes a controlled boundary.
```

Related concepts: [Procedure Security Boundary](stored-procedures.concept.md#procedure-security-boundary)

```interview-choice
Which dynamic SQL pattern is safest for user-provided values?
---
options:
- Concatenate user input directly into the SQL string.
- Use `sp_executesql` with typed parameters.
- Replace single quotes manually and use `EXEC(@sql)`.
correct: 1
explanation:
`sp_executesql` with parameters separates values from command text and reduces SQL injection risk.
```

```interview-question
Are stored procedures automatically faster than ad-hoc SQL?
---
answer:
No. Stored procedure performance still depends on query shape, indexes, statistics, parameter values, and execution plans.

Procedures improve reuse and boundaries, but they do not remove the need to measure and tune the statements inside them.
hints:
- The optimizer still chooses plans.
- Indexes and statistics still matter.
- Reuse does not guarantee speed.
```

Related concepts: [Execution Plan](stored-procedures.concept.md#execution-plan)

## Design Choices

```interview-choice
Which operation is usually a good stored procedure candidate?
---
options:
- A one-off exploratory query used once in SSMS.
- A stable customer order lookup used by an application.
- A hidden trigger-like side effect that callers do not know about.
correct: 1
explanation:
Stable, repeated operations with clear parameters and expected output are good stored procedure candidates.
```

```interview-question
What is the key difference between a stored procedure and a user-defined function?
---
answer:
A stored procedure is executed as an operation and can perform broader workflow tasks, including controlled data changes and transaction handling.

A user-defined function returns a scalar value or table expression and is usually used inside queries, with more restrictions on side effects.
hints:
- One is called as an operation.
- One is used as an expression or table source.
- Side effects and transaction control differ.
```

Related concepts: [User-Defined Function](stored-procedures.concept.md#user-defined-function), [Stored Procedure](stored-procedures.concept.md#stored-procedure)
