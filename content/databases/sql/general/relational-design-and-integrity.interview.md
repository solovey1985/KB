---
title: Relational Design And Integrity Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise schema-design and integrity questions.

Study pages: [Relational Design And Integrity](relational-design-and-integrity.md) | [Concept Map](relational-design-and-integrity.concept.md)

## Keys And Integrity

```interview-question
What is a primary key?
---
answer:
A primary key is a column or set of columns that uniquely identifies each row in a table.

It must be unique and not null, and it is commonly referenced by foreign keys from related tables.
hints:
- It identifies one row.
- It cannot be null.
- Other tables often point to it.
```

```interview-question
What is a foreign key and what does it protect?
---
answer:
A foreign key is a column or set of columns in one table that references a primary key or unique key in another table.

It protects referential integrity by preventing child rows from referencing nonexistent parent rows.
hints:
- Think parent and child tables.
- It points to another table's key.
- It prevents broken references.
```

```interview-choice
Which AdventureWorksLT relationship is a foreign-key-style relationship?
---
options:
- `SalesLT.SalesOrderDetail.ProductID` points to `SalesLT.Product.ProductID`.
- `SalesLT.Product.Name` points to `SalesLT.Customer.CompanyName`.
- `SalesLT.Address.City` points to `SalesLT.Product.Color`.
correct: 0
explanation:
Order details reference products through `ProductID`, which is the relationship used to retrieve product information for each order line.
```

## Normalization

```interview-question
What is normalization?
---
answer:
Normalization is the process of structuring tables to reduce redundant data and update anomalies.

It separates facts into related tables so each fact is stored in one appropriate place.
hints:
- It reduces duplication.
- It prevents update anomalies.
- It separates facts by entity.
```

```interview-question
When would you denormalize data?
---
answer:
Denormalize when a measured read or reporting need justifies storing duplicated or precomputed data.

It should come with a clear strategy to keep redundant data consistent.
hints:
- It is usually performance-driven.
- It adds maintenance cost.
- Do not do it just because joins exist.
```

```interview-choice
Which statement best describes denormalization?
---
options:
- It always improves integrity by removing duplication.
- It intentionally adds redundancy for read performance or reporting simplicity.
- It means removing all foreign keys from the schema.
correct: 1
explanation:
Denormalization can help reads, but it introduces redundancy that must be maintained carefully.
```

## Constraints

```interview-question
Why should databases enforce constraints even when the application validates input?
---
answer:
The database is the final boundary for shared data.

Constraints protect integrity across all clients, scripts, imports, and application code paths, not just one application workflow.
hints:
- More than one client may write data.
- Imports and scripts can bypass application validation.
- The database owns the durable facts.
```

```interview-code
language: sql
prompt: Complete the query so it lists constraints and other child objects for `SalesLT.SalesOrderDetail`.
starter:
SELECT name, type_desc
FROM sys.objects
WHERE parent_object_id =
ORDER BY type_desc, name;
solution:
SELECT name, type_desc
FROM sys.objects
WHERE parent_object_id = OBJECT_ID('SalesLT.SalesOrderDetail')
ORDER BY type_desc, name;
checks:
- includes: OBJECT_ID('SalesLT.SalesOrderDetail')
- includes: ORDER BY type_desc, name
```
