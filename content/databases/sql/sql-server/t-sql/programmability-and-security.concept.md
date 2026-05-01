---
title: T-SQL Programmability And Security Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers database-side programmability and secure execution boundaries.

Study pages: [Programmability And Security](programmability-and-security.md) | [Interview Practice](programmability-and-security.interview.md) | [Stored Procedures](stored-procedures.md)

```concept-card
id: programmability-and-security
term: Programmability And Security
children:
- stored-procedure
- view
- user-defined-function
- trigger
- dynamic-sql
- least-privilege
summary:
Programmability and security combine reusable database logic with controlled access to data and operations.
details:
SQL Server supports procedures, views, functions, triggers, dynamic SQL, roles, permissions, and ownership boundaries.
recall:
- Which database objects store reusable logic?
- Why should access be granted narrowly?
```

```concept-card
id: stored-procedure
term: Stored Procedure
parents:
- programmability-and-security
related:
- least-privilege
summary:
A stored procedure is a named T-SQL operation stored in SQL Server.
details:
Use it for stable operations with parameters, controlled permissions, error handling, and optional transaction logic.
example:
EXEC dbo.usp_GetCustomerOrders @CustomerID = 29485;
recall:
- Why is a stored procedure a security boundary?
- When is a procedure better than ad-hoc SQL?
```

```concept-card
id: view
term: View
parents:
- programmability-and-security
summary:
A view stores a reusable query shape as a virtual table.
details:
Views can simplify reads, hide complexity, and expose a narrower projection of underlying tables.
recall:
- How is a view different from a stored procedure?
- Why might a view expose fewer columns?
```

```concept-card
id: user-defined-function
term: User-Defined Function
parents:
- programmability-and-security
related:
- stored-procedure
summary:
A user-defined function returns a scalar value or table expression for reuse in queries.
details:
Functions are useful for reusable calculations or table expressions, but they have restrictions compared with procedures.
recall:
- When is a function better than a procedure?
- Why should scalar functions be used carefully?
```

```concept-card
id: trigger
term: Trigger
parents:
- programmability-and-security
summary:
A trigger runs automatically in response to table or view events.
details:
Triggers can support auditing or complex rules, but they hide side effects and should not replace simple constraints.
recall:
- Why can triggers surprise callers?
- When is a constraint better than a trigger?
```

```concept-card
id: dynamic-sql
term: Dynamic SQL
parents:
- programmability-and-security
related:
- sql-injection
summary:
Dynamic SQL builds and executes command text at runtime.
details:
Use `sp_executesql` with typed parameters for values. Avoid concatenating untrusted input into executable SQL.
example:
EXEC sys.sp_executesql @sql, N'@MinPrice money', @MinPrice = 1000;
recall:
- When is dynamic SQL justified?
- How does parameterization reduce injection risk?
```

```concept-card
id: least-privilege
term: Least Privilege
parents:
- programmability-and-security
related:
- stored-procedure
summary:
Least privilege grants only the access needed for a role or operation.
details:
In SQL Server, users can receive execute permission on approved procedures instead of broad table permissions.
recall:
- Why is `GRANT EXECUTE` useful?
- What risk comes from broad table access?
```

```concept-card
id: sql-injection
term: SQL Injection
parents:
- programmability-and-security
related:
- dynamic-sql
summary:
SQL injection occurs when untrusted input is treated as executable SQL code.
details:
Parameterized queries and `sp_executesql` separate command text from values, preventing values from changing query structure.
recall:
- Why is manual quote escaping not enough as a main defense?
- What should be parameterized in dynamic SQL?
```
