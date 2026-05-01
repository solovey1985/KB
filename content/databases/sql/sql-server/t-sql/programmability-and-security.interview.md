---
title: T-SQL Programmability And Security Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise T-SQL programmability objects and database security boundaries.

Study pages: [Programmability And Security](programmability-and-security.md) | [Concept Map](programmability-and-security.concept.md) | [Stored Procedures](stored-procedures.md)

## Programmability Objects

```interview-question
What are common SQL Server programmability objects?
---
answer:
Common objects include stored procedures, views, user-defined functions, and triggers.

They store reusable database logic or reusable read shapes inside SQL Server.
hints:
- Some are called with `EXEC`.
- Some can be used inside queries.
- Some run automatically after events.
```

```interview-choice
Which object is best described as a reusable virtual table based on a query?
---
options:
- Stored procedure
- View
- Trigger
correct: 1
explanation:
A view stores a query shape that can be selected from like a table, though it is usually virtual.
```

```interview-question
Why should triggers be used carefully?
---
answer:
Triggers run automatically and can hide side effects from the statement that caused them.

They are useful for auditing or complex rules, but simple integrity rules should usually be constraints instead.
hints:
- They are automatic.
- They can hide work.
- Constraints are clearer for simple rules.
```

## Security

```interview-question
What does least privilege mean in a database?
---
answer:
Least privilege means granting only the permissions needed for a user, role, or operation.

For example, a user may receive execute permission on a stored procedure without direct table write permission.
hints:
- Grant only what is needed.
- Roles help group permissions.
- Procedure execution can be narrower than table access.
```

```interview-choice
Which permission pattern is usually safer for a controlled read operation?
---
options:
- Grant broad `SELECT` access to every table.
- Grant `EXECUTE` on an approved stored procedure.
- Make the caller a database owner.
correct: 1
explanation:
Granting execute permission on a focused procedure exposes only the approved operation instead of broad table access.
```

```interview-question
What is SQL injection?
---
answer:
SQL injection occurs when untrusted input is mixed into SQL command text and changes the intended query structure.

The main defense is parameterization so values stay separate from executable SQL code.
hints:
- User input becomes code.
- String concatenation is the usual risk.
- Parameters are the main defense.
```

## Dynamic SQL

```interview-question
When is dynamic SQL justified?
---
answer:
Dynamic SQL is justified when query structure must vary at runtime, such as dynamic search predicates, dynamic sort columns, or metadata-driven administration.

If only values change, use static SQL with parameters instead.
hints:
- Structure changes, not just values.
- Search and sort variations are common examples.
- Avoid it when static SQL is enough.
```

```interview-code
language: sql
prompt: Complete the dynamic SQL call so the minimum price is parameterized.
starter:
DECLARE @sql nvarchar(max) = N'
SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ListPrice >= @MinPrice;';

EXEC sys.sp_executesql
    @sql,
solution:
DECLARE @sql nvarchar(max) = N'
SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ListPrice >= @MinPrice;';

EXEC sys.sp_executesql
    @sql,
    N'@MinPrice money',
    @MinPrice = 1000;
checks:
- includes: sp_executesql
- includes: N'@MinPrice money'
- includes: @MinPrice = 1000
```

```interview-choice
Which dynamic SQL pattern is most vulnerable?
---
options:
- Using `sp_executesql` with typed parameters.
- Concatenating untrusted user input into the SQL command text.
- Keeping query values in local variables.
correct: 1
explanation:
Concatenating untrusted input can let values become executable SQL code, which is the core SQL injection risk.
```
