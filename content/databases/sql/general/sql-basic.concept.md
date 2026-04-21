---
title: SQL Basic Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the vendor-neutral SQL basics.

Study pages: [Section Index](index.md) | [Basic Questions](sql-basic.interview.md) | [Intermediate Questions](sql-intermediate.interview.md)

## Basic Map

```concept-card
id: sql
term: SQL
children:
- sql-command-families
- where-clause
- having-clause
- grouping-and-aggregation
- primary-key
- foreign-key
- joins
summary:
SQL is the standard language for defining, querying, and manipulating data in relational databases.
details:
It covers schema definition, row operations, querying, transaction control, and permissions.
example:
`SELECT Name FROM Customers WHERE Country = 'USA';`
mnemonic:
Define, query, change, control.
recall:
- What major jobs does SQL perform besides querying?
- Why is SQL broader than `SELECT` statements alone?
```

```concept-card
id: sql-command-families
term: SQL Command Families
parents:
- sql
summary:
SQL command families group statements by purpose, such as definition, manipulation, querying, security, and transaction control.
details:
Common families are DDL, DML, DQL, DCL, and TCL.
example:
`CREATE`, `INSERT`, `SELECT`, `GRANT`, and `COMMIT` each belong to different SQL command families.
mnemonic:
Define, modify, query, secure, commit.
recall:
- What does each major SQL command family do?
- Why is transaction control treated separately from data manipulation?
```

```concept-card
id: where-clause
term: WHERE Clause
parents:
- sql
related:
- having-clause
summary:
The `WHERE` clause filters rows before grouping or aggregation.
details:
It is the right place for row-level predicates such as dates, statuses, or exact values.
example:
`SELECT * FROM Orders WHERE Status = 'Pending';`
mnemonic:
Where filters rows first.
recall:
- When does `WHERE` run logically?
- Why can aggregate conditions not live there?
```

```concept-card
id: having-clause
term: HAVING Clause
parents:
- sql
related:
- where-clause
summary:
The `HAVING` clause filters grouped results after aggregation.
details:
It is used for conditions based on grouped output, such as `COUNT(*) > 5`.
example:
`SELECT CustomerID, COUNT(*) FROM Orders GROUP BY CustomerID HAVING COUNT(*) > 5;`
mnemonic:
Having filters groups after.
recall:
- Why is `HAVING` used with aggregate predicates?
- What is the core contrast with `WHERE`?
```

```concept-card
id: grouping-and-aggregation
term: Grouping and Aggregation
parents:
- sql
related:
- having-clause
summary:
Grouping partitions rows into sets, and aggregate functions summarize each set.
details:
It is the basis for reporting queries such as totals, averages, counts, and grouped summaries.
example:
`SELECT DepartmentID, AVG(Salary) FROM Employees GROUP BY DepartmentID;`
mnemonic:
Group rows, summarize each group.
recall:
- What does `GROUP BY` change about result shape?
- Which aggregate functions are most common?
```

```concept-card
id: primary-key
term: Primary Key
parents:
- sql
related:
- foreign-key
summary:
A primary key uniquely identifies each row in a table.
details:
It must be unique and not null, and it usually anchors relationships from other tables.
example:
`CustomerID` can serve as the primary key of `Customers`.
mnemonic:
One row, one identity.
recall:
- Why must a primary key be unique and not null?
- Why is it central to relational design?
```

```concept-card
id: foreign-key
term: Foreign Key
parents:
- sql
related:
- primary-key
- joins
summary:
A foreign key links a row in one table to a primary or unique key in another table.
details:
It enforces referential integrity and models relationships across tables.
example:
`Orders.CustomerID` can reference `Customers.CustomerID`.
mnemonic:
Child points to parent.
recall:
- What integrity rule does a foreign key enforce?
- Why is it different from a join condition even though they are related?
```

```concept-card
id: joins
term: Joins
parents:
- sql
summary:
Joins combine rows from two or more tables based on related columns.
details:
They let queries reconstruct relational data from normalized tables.
example:
`SELECT o.OrderID, c.Name FROM Orders o JOIN Customers c ON o.CustomerID = c.CustomerID;`
mnemonic:
Separate tables, connected rows.
recall:
- Why are joins essential in normalized schemas?
- What common mistake turns a join into an accidental cartesian product?
```
