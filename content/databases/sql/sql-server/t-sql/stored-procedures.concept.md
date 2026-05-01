---
title: T-SQL Stored Procedures Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map focuses on stored procedures as a SQL Server programming topic.

Study pages: [Stored Procedures](stored-procedures.md) | [Interview Practice](stored-procedures.interview.md)

```concept-card
id: stored-procedure
term: Stored Procedure
aliases:
- proc
- procedure
parents:
- t-sql-programmability
children:
- procedure-parameters
- procedure-result-set
- output-parameter
- procedure-transaction-scope
- procedure-error-handling
- procedure-security-boundary
related:
- dynamic-sql
- execution-plan
summary:
A stored procedure is a named T-SQL routine stored in SQL Server and executed as a unit.
details:
It provides a stable database interface for reusable operations, controlled permissions, input validation, error handling, and optional transaction management.
example:
EXEC dbo.usp_GetCustomerOrders @CustomerID = 29485;
mnemonic:
Name the operation, then execute it.
recall:
- What problem does a stored procedure solve?
- Why is a procedure an interface, not just a query container?
- When is a stored procedure a good fit?
```

```concept-card
id: t-sql-programmability
term: T-SQL Programmability
children:
- stored-procedure
- user-defined-function
- trigger
summary:
T-SQL programmability is the set of SQL Server features for storing reusable logic inside the database.
details:
It includes stored procedures, user-defined functions, triggers, dynamic SQL, variables, control flow, transactions, and error handling.
recall:
- Which database objects store reusable logic?
- How does programmability differ from one-off querying?
```

```concept-card
id: procedure-parameters
term: Procedure Parameters
parents:
- stored-procedure
children:
- output-parameter
summary:
Procedure parameters pass values into or out of a stored procedure.
details:
Input parameters shape filtering and behavior. Output parameters return scalar values. Named arguments make calls easier to read and safer to evolve.
example:
EXEC dbo.usp_GetCustomerOrders @CustomerID = 29485;
recall:
- Why should procedure calls use parameter names?
- What is the difference between input and output parameters?
```

```concept-card
id: procedure-result-set
term: Procedure Result Set
parents:
- stored-procedure
summary:
A procedure result set is the rowset returned by a `SELECT` statement inside the procedure.
details:
Applications often depend on the result set shape, so column names, data types, and row meaning should be treated as part of the procedure contract.
example:
SELECT SalesOrderID, OrderDate, TotalDue FROM SalesLT.SalesOrderHeader WHERE CustomerID = @CustomerID;
recall:
- Why is result set shape part of the procedure contract?
- When should a procedure return rows instead of an output parameter?
```

```concept-card
id: output-parameter
term: Output Parameter
parents:
- procedure-parameters
summary:
An output parameter returns a scalar value from a stored procedure to its caller.
details:
It is useful for totals, generated identifiers, status codes, or single calculated values, but it is not a substitute for returning tabular data.
example:
EXEC dbo.usp_GetCustomerSalesTotal @CustomerID = 29485, @TotalDue = @Total OUTPUT;
recall:
- What kind of value belongs in an output parameter?
- Why is `OUTPUT` required in the call?
```

```concept-card
id: procedure-transaction-scope
term: Procedure Transaction Scope
parents:
- stored-procedure
related:
- procedure-error-handling
summary:
Transaction scope defines whether the procedure or its caller controls `BEGIN TRAN`, `COMMIT`, and `ROLLBACK`.
details:
Data-changing procedures should make transaction ownership clear. Hidden nested transaction assumptions can cause partial work, blocking, or unexpected rollbacks.
example:
BEGIN TRY BEGIN TRAN; UPDATE ...; COMMIT; END TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK; THROW; END CATCH;
recall:
- Why must transaction ownership be explicit?
- What can go wrong with long procedure transactions?
```

```concept-card
id: procedure-error-handling
term: Procedure Error Handling
parents:
- stored-procedure
related:
- procedure-transaction-scope
summary:
Procedure error handling uses `TRY...CATCH`, `THROW`, and transaction cleanup to keep failures predictable.
details:
When a procedure changes data, the catch block should roll back open work and rethrow the error so the caller can see the failure.
example:
BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK; THROW; END CATCH;
recall:
- Why should a catch block usually rethrow?
- Why check `@@TRANCOUNT` before rollback?
```

```concept-card
id: procedure-security-boundary
term: Procedure Security Boundary
parents:
- stored-procedure
summary:
A stored procedure can expose a controlled operation without giving callers direct table permissions.
details:
This supports least privilege, especially when users need to run approved reads or writes but should not freely query or modify base tables.
example:
GRANT EXECUTE ON OBJECT::dbo.usp_GetCustomerOrders TO ReportingUser;
recall:
- How can stored procedures support least privilege?
- Why is execute permission different from table access?
```

```concept-card
id: dynamic-sql
term: Dynamic SQL
parents:
- t-sql-programmability
related:
- stored-procedure
- procedure-security-boundary
summary:
Dynamic SQL builds command text at runtime and executes it with `EXEC` or `sp_executesql`.
details:
Use it only when static SQL cannot express the requirement. Parameterize values with `sp_executesql` to reduce SQL injection risk and improve plan reuse.
example:
EXEC sys.sp_executesql @sql, N'@MinPrice money', @MinPrice = 1000;
recall:
- When is dynamic SQL justified?
- Why should values be parameters instead of concatenated strings?
```

```concept-card
id: execution-plan
term: Execution Plan
parents:
- stored-procedure
related:
- procedure-parameters
summary:
An execution plan is SQL Server's chosen strategy for running a query or procedure statement.
details:
Procedure performance depends on the plans chosen for its statements, the available indexes, and the parameter values used during compilation and execution.
recall:
- Why are stored procedures not automatically fast?
- What factors influence procedure performance?
```

```concept-card
id: user-defined-function
term: User-Defined Function
parents:
- t-sql-programmability
related:
- stored-procedure
summary:
A user-defined function returns a scalar value or table expression and is intended for reusable calculations or table-valued logic.
details:
Unlike stored procedures, functions are commonly used inside queries, but they have more restrictions around side effects and transaction control.
recall:
- How is a function different from a stored procedure?
- When is a table-valued function useful?
```

```concept-card
id: trigger
term: Trigger
parents:
- t-sql-programmability
related:
- stored-procedure
summary:
A trigger is procedural logic that runs automatically in response to table or view events.
details:
Triggers can enforce auditing or complex rules, but they hide work from callers and should not replace simple constraints.
recall:
- Why can triggers make behavior harder to reason about?
- When is a constraint better than a trigger?
```
