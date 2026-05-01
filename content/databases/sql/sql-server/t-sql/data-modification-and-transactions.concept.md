---
title: T-SQL Data Modification And Transactions Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers safe T-SQL writes and transaction control.

Study pages: [Data Modification And Transactions](data-modification-and-transactions.md) | [Interview Practice](data-modification-and-transactions.interview.md)

```concept-card
id: data-modification-and-transactions
term: Data Modification And Transactions
children:
- insert-statement
- update-statement
- delete-statement
- transaction
- isolation-level
- try-catch-transaction
summary:
Data modification changes rows, while transactions control whether related changes commit or roll back as one unit.
details:
T-SQL write statements should be scoped with precise predicates and protected with transaction and error-handling patterns when multiple changes must succeed together.
recall:
- Why should writes be tested inside a transaction?
- What makes a data modification statement risky?
```

```concept-card
id: insert-statement
term: INSERT Statement
parents:
- data-modification-and-transactions
summary:
`INSERT` adds new rows to a table.
details:
It should provide explicit columns so the statement remains clear when table definitions change.
recall:
- Why should insert statements list columns explicitly?
- When might identity values be omitted?
```

```concept-card
id: update-statement
term: UPDATE Statement
parents:
- data-modification-and-transactions
summary:
`UPDATE` changes existing rows that match a predicate.
details:
Always verify the target rows first. Missing or incorrect `WHERE` clauses can update far more rows than intended.
example:
UPDATE SalesLT.Product SET ListPrice = ListPrice * 1.05 WHERE ProductID = 680;
recall:
- Why should you run the target `SELECT` first?
- What is the danger of an update without `WHERE`?
```

```concept-card
id: delete-statement
term: DELETE Statement
parents:
- data-modification-and-transactions
summary:
`DELETE` removes rows that match a predicate.
details:
Deletes should be scoped carefully and usually tested inside a transaction. Foreign keys may block deletes that would break relationships.
recall:
- Why can foreign keys block a delete?
- How can a transaction make delete practice safer?
```

```concept-card
id: transaction
term: Transaction
parents:
- data-modification-and-transactions
children:
- commit
- rollback
related:
- isolation-level
summary:
A transaction groups work so it can be committed or rolled back as one unit.
details:
Use `BEGIN TRAN`, `COMMIT`, and `ROLLBACK` to control whether changes become permanent.
example:
BEGIN TRAN; UPDATE ...; ROLLBACK;
recall:
- What does `COMMIT` do?
- What does `ROLLBACK` do?
```

```concept-card
id: isolation-level
term: Isolation Level
parents:
- data-modification-and-transactions
related:
- transaction
summary:
An isolation level controls how a transaction sees data changed by other transactions.
details:
It balances consistency and concurrency. Stronger isolation can reduce anomalies but increase blocking or versioning overhead.
recall:
- What tradeoff does isolation control?
- Why can stronger isolation increase blocking?
```

```concept-card
id: try-catch-transaction
term: TRY...CATCH Transaction Pattern
parents:
- data-modification-and-transactions
summary:
The `TRY...CATCH` transaction pattern commits successful work and rolls back failed work.
details:
In the catch block, check `@@TRANCOUNT`, roll back open work, and rethrow with `THROW`.
example:
BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK; THROW; END CATCH;
recall:
- Why should a catch block rethrow?
- Why check `@@TRANCOUNT` before rollback?
```

```concept-card
id: commit
term: COMMIT
parents:
- transaction
summary:
`COMMIT` makes the transaction's changes durable.
details:
Only commit after verifying the expected rows and business rule.
recall:
- What should be checked before committing?
```

```concept-card
id: rollback
term: ROLLBACK
parents:
- transaction
summary:
`ROLLBACK` undoes uncommitted work in the current transaction.
details:
It is essential for safe practice scripts and error cleanup.
recall:
- Why is rollback useful while learning data modification?
```
