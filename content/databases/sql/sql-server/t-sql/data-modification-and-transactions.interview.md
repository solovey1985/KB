---
title: T-SQL Data Modification And Transactions Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise safe T-SQL writes and transaction handling.

Study pages: [Data Modification And Transactions](data-modification-and-transactions.md) | [Concept Map](data-modification-and-transactions.concept.md)

## Data Modification

```interview-question
What are the main T-SQL statements for changing table data?
---
answer:
The main statements are `INSERT`, `UPDATE`, and `DELETE`.

`INSERT` adds rows, `UPDATE` changes existing rows, and `DELETE` removes rows.
hints:
- One adds rows.
- One changes rows.
- One removes rows.
```

```interview-question
Why should you run a `SELECT` before an `UPDATE` or `DELETE`?
---
answer:
Running a `SELECT` with the same predicate verifies which rows will be affected before changing data.

This reduces the risk of broad updates or deletes caused by an incorrect `WHERE` clause.
hints:
- It checks the target rows.
- The `WHERE` clause is the risk point.
- It is a safety habit.
```

```interview-code
language: sql
prompt: Complete the safe practice update so it can be inspected and rolled back.
starter:
BEGIN TRAN;

UPDATE SalesLT.Product
SET ListPrice = ListPrice * 1.05
WHERE ProductID = 680;

SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ProductID = 680;

solution:
BEGIN TRAN;

UPDATE SalesLT.Product
SET ListPrice = ListPrice * 1.05
WHERE ProductID = 680;

SELECT ProductID, Name, ListPrice
FROM SalesLT.Product
WHERE ProductID = 680;

ROLLBACK;
checks:
- includes: BEGIN TRAN
- includes: WHERE ProductID = 680
- includes: ROLLBACK
```

## Transactions

```interview-question
What does a transaction do?
---
answer:
A transaction groups related work so it can be committed or rolled back as one unit.

This helps preserve consistency when multiple statements must succeed or fail together.
hints:
- Think unit of work.
- It can be committed.
- It can be rolled back.
```

```interview-choice
Which command makes changes in the current transaction permanent?
---
options:
- `ROLLBACK`
- `COMMIT`
- `BEGIN TRAN`
correct: 1
explanation:
`COMMIT` persists the transaction's changes. `ROLLBACK` undoes them, and `BEGIN TRAN` starts the transaction.
```

```interview-question
What is the risk of keeping a transaction open for too long?
---
answer:
Long transactions can hold locks, block other sessions, increase contention, and make rollback more expensive.

Keep transactions short and avoid waiting for user input while a transaction is open.
hints:
- Think locks.
- Other sessions may wait.
- Short transactions are safer.
```

## Error Handling

```interview-question
What should a T-SQL `CATCH` block do when a data-changing transaction fails?
---
answer:
It should roll back open work if `@@TRANCOUNT > 0`, then rethrow the error with `THROW`.

That prevents partial work and keeps failure visible to the caller.
hints:
- Clean up open work.
- Do not swallow the error.
- Use `THROW`.
```

```interview-code
language: sql
prompt: Complete the error-handling pattern.
starter:
BEGIN TRY
    BEGIN TRAN;
    -- write statements here
    COMMIT;
END TRY
BEGIN CATCH

END CATCH;
solution:
BEGIN TRY
    BEGIN TRAN;
    -- write statements here
    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK;
    THROW;
END CATCH;
checks:
- includes: BEGIN TRAN
- includes: IF @@TRANCOUNT > 0 ROLLBACK
- includes: THROW
```
