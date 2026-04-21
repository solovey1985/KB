---
title: SQL Intermediate Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers intermediate SQL design and query concepts.

Study pages: [Section Index](index.md) | [Intermediate Questions](sql-intermediate.interview.md) | [Advanced Questions](sql-advanced.interview.md)

## Intermediate Map

```concept-card
id: subqueries
term: Subqueries
summary:
Subqueries are queries nested inside other queries.
details:
They are useful for filtering, comparison, or building derived result sets inside a larger statement.
example:
`SELECT Name FROM Products WHERE CategoryID IN (SELECT CategoryID FROM Categories WHERE IsActive = 1);`
mnemonic:
One query inside another.
recall:
- When is a subquery a good fit?
- What makes correlated subqueries more expensive or more complex?
```

```concept-card
id: common-table-expression
term: Common Table Expression
aliases:
- CTE
summary:
A Common Table Expression is a named intermediate result defined with `WITH` and used by the main query.
details:
It improves readability, can simplify complex logic, and supports recursive querying.
example:
`WITH ActiveCustomers AS (SELECT * FROM Customers WHERE IsActive = 1) SELECT * FROM ActiveCustomers;`
mnemonic:
Name the middle, simplify the query.
recall:
- Why can a CTE be clearer than one large nested query?
- What special kind of query uses recursive CTEs?
```

```concept-card
id: normalization
term: Normalization
related:
- denormalization
summary:
Normalization reduces redundancy and data anomalies by organizing related data into well-structured tables.
details:
It is mainly about integrity and maintainability, not about making every schema as fragmented as possible.
example:
Store customers once in `Customers` and reference them from `Orders` instead of duplicating customer details in every order row.
mnemonic:
Store once, reference many.
recall:
- Which anomalies does normalization reduce?
- Why is normalization a schema-design tool rather than a query feature?
```

```concept-card
id: denormalization
term: Denormalization
related:
- normalization
summary:
Denormalization adds controlled redundancy to simplify reads or improve performance.
details:
It is often used in reporting, analytics, or read-heavy systems where fewer joins matter more than strict write normalization.
example:
Keep `CustomerRegion` directly on a reporting table to avoid repeated joins in dashboard queries.
mnemonic:
Repeat some data to speed some reads.
recall:
- Why would a team denormalize on purpose?
- What write-side trade-off does denormalization introduce?
```

```concept-card
id: transactions
term: Transactions
summary:
Transactions group multiple SQL statements into one all-or-nothing unit of work.
details:
They protect consistency by letting the database commit or roll back related changes together.
example:
Insert an order and its order lines in one transaction so either both succeed or neither persists.
mnemonic:
All together or not at all.
recall:
- Why are transactions central to correctness?
- What do `COMMIT` and `ROLLBACK` do?
```

```concept-card
id: constraints
term: Constraints
summary:
Constraints are schema rules that the database enforces automatically.
details:
They include `NOT NULL`, `UNIQUE`, `CHECK`, and foreign keys, and they protect data quality at the database boundary.
example:
`CHECK (Quantity > 0)` prevents impossible order-line quantities.
mnemonic:
Let the schema reject bad data.
recall:
- Which constraint types are most common?
- Why are constraints stronger than app-only validation?
```
