---
title: SQL Basic Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise vendor-neutral SQL fundamentals.

Relevant concept maps:

- [SQL Basic Concept Map](sql-basic.concept.md)
- [SQL Intermediate Concept Map](sql-intermediate.concept.md)

## Foundations

```interview-question
What is SQL and what is it used for?
---
answer:
SQL is the standard language used to define, query, and manipulate data in relational databases.

It is used for schema definition, inserting and updating rows, querying datasets, controlling transactions, and managing permissions.
hints:
- It is the main language of relational databases.
- It is not only for `SELECT`.
- Think schema, data, and transactions.
```

Related concepts: [SQL](sql-basic.concept.md#sql), [SQL Command Families](sql-basic.concept.md#sql-command-families)

```interview-question
What is the difference between `WHERE` and `HAVING`?
---
answer:
`WHERE` filters rows before grouping and aggregation.

`HAVING` filters grouped results after aggregation.

That is why `COUNT(*) > 5` belongs in `HAVING`, not in `WHERE`.
hints:
- One works before grouping.
- One works after grouping.
- Aggregate conditions are the key clue.
```

Related concepts: [WHERE Clause](sql-basic.concept.md#where-clause), [HAVING Clause](sql-basic.concept.md#having-clause), [Grouping and Aggregation](sql-basic.concept.md#grouping-and-aggregation)

```interview-choice
Which clause is used to filter grouped rows based on an aggregate?
---
options:
- `WHERE`
- `HAVING`
- `ORDER BY`
correct: 1
explanation:
`HAVING` runs after grouping and is therefore the right place for predicates based on aggregate values.
```

```interview-question
What is a primary key?
---
answer:
A primary key is a column or set of columns that uniquely identifies each row in a table.

It must be unique and not null, and it is usually the target of foreign key references from related tables.
hints:
- It identifies a row.
- Uniqueness is required.
- Other tables often point to it.
```

Related concepts: [Primary Key](sql-basic.concept.md#primary-key), [Foreign Key](sql-basic.concept.md#foreign-key)

```interview-question
What is a foreign key and how is it used?
---
answer:
A foreign key is a column or set of columns in one table that references a primary key or unique key in another table.

It is used to enforce referential integrity so child rows cannot reference nonexistent parent rows.
hints:
- Think parent table and child table.
- It protects relationship integrity.
- It points to a unique identifier in another table.
```

Related concepts: [Foreign Key](sql-basic.concept.md#foreign-key), [Joins](sql-basic.concept.md#joins)

```interview-code
language: sql
prompt: Complete the query so it returns customers with more than 5 orders.
starter:
SELECT CustomerID, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerID

solution:
SELECT CustomerID, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerID
HAVING COUNT(*) > 5;
checks:
- includes: HAVING
- includes: COUNT(*) > 5
```
