---
title: Relational Design And Integrity Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers the schema-design concepts behind reliable SQL data.

Study pages: [Relational Design And Integrity](relational-design-and-integrity.md) | [Interview Practice](relational-design-and-integrity.interview.md)

```concept-card
id: relational-design
term: Relational Design
children:
- primary-key
- foreign-key
- referential-integrity
- normalization
- denormalization
- database-constraint
summary:
Relational design organizes data into tables, keys, relationships, and constraints so facts are stored clearly and consistently.
details:
Good design reduces duplication, prevents invalid relationships, and makes queries easier to reason about.
recall:
- Why do keys matter for relational design?
- How do constraints protect data quality?
```

```concept-card
id: primary-key
term: Primary Key
parents:
- relational-design
related:
- foreign-key
summary:
A primary key uniquely identifies each row in a table.
details:
It must be unique and not null. Other tables commonly reference it through foreign keys.
example:
SalesLT.Product.ProductID uniquely identifies a product.
recall:
- What two properties must a primary key have?
- Why should primary keys be stable?
```

```concept-card
id: foreign-key
term: Foreign Key
parents:
- relational-design
related:
- primary-key
- referential-integrity
summary:
A foreign key is a column or set of columns that references a key in another table.
details:
It models parent-child relationships and prevents child rows from pointing to missing parent rows.
example:
SalesLT.SalesOrderDetail.ProductID references SalesLT.Product.ProductID.
recall:
- Which side is the parent table?
- What invalid state does a foreign key prevent?
```

```concept-card
id: referential-integrity
term: Referential Integrity
parents:
- relational-design
related:
- foreign-key
summary:
Referential integrity means relationships between tables remain valid.
details:
The database enforces this by checking that referenced parent rows exist before child rows are inserted or updated.
recall:
- Why is referential integrity not just an application concern?
- What happens if child rows can reference missing parents?
```

```concept-card
id: normalization
term: Normalization
parents:
- relational-design
related:
- denormalization
summary:
Normalization structures tables to reduce redundancy and update anomalies.
details:
Each table should store facts about one kind of thing, with relationships connecting related facts.
example:
Product names live in SalesLT.Product instead of being repeated in every order detail.
recall:
- What problem does normalization reduce?
- Why can repeated facts cause update anomalies?
```

```concept-card
id: denormalization
term: Denormalization
parents:
- relational-design
related:
- normalization
summary:
Denormalization intentionally duplicates or precomputes data for read performance or reporting simplicity.
details:
It trades some write complexity and consistency risk for faster reads or simpler analytical queries.
recall:
- When is denormalization justified?
- What maintenance cost does it introduce?
```

```concept-card
id: database-constraint
term: Database Constraint
parents:
- relational-design
summary:
A database constraint is a rule enforced by the database engine.
details:
Common constraints include primary keys, foreign keys, unique constraints, check constraints, and `NOT NULL` rules.
recall:
- Why should constraints exist even when applications validate input?
- Which constraints protect uniqueness and relationships?
```
