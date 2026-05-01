---
title: SQL Query Fundamentals Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the core shape of SQL queries using AdventureWorksLT examples.

Study pages: [Query Fundamentals](query-fundamentals.md) | [Concept Map](query-fundamentals.concept.md)

## Query Shape

```interview-question
What is the logical difference between the written order of a `SELECT` query and the processing order?
---
answer:
SQL is written starting with `SELECT`, but conceptually the data source is processed first.

`FROM` and joins identify rows, `WHERE` filters rows, `GROUP BY` groups them, `HAVING` filters groups, `SELECT` projects values, and `ORDER BY` sorts the final result.
hints:
- `SELECT` is written first but not logically first.
- Filtering happens before grouping.
- Sorting is near the end.
```

Related concepts: [Logical Query Processing](query-fundamentals.concept.md#logical-query-processing)

```interview-choice
Which clause is the correct place for `COUNT(*) > 3` in a grouped query?
---
options:
- `WHERE`
- `HAVING`
- `ORDER BY`
correct: 1
explanation:
`COUNT(*)` is an aggregate, so the condition can only be evaluated after grouping. That makes `HAVING` the correct clause.
```

```interview-question
Why should a query use `ORDER BY` when the order matters?
---
answer:
Relational tables do not guarantee result order unless the query explicitly asks for it.

`ORDER BY` is required for predictable presentation, top-N queries, and pagination.
hints:
- Physical storage order is not a contract.
- Top-N needs a definition of top.
- Pagination without stable ordering can skip or duplicate rows.
```

## Filtering And Joining

```interview-question
What is the purpose of a `WHERE` predicate?
---
answer:
A `WHERE` predicate filters individual rows before grouping or aggregation.

It is used for row-level conditions such as status, price range, dates, `IN` lists, pattern matches, and null checks.
hints:
- It works before `GROUP BY`.
- It is row-level filtering.
- It can use `IN`, `BETWEEN`, `LIKE`, and `IS NULL`.
```

```interview-choice
Which join keeps every row from the left table even when there is no matching row on the right?
---
options:
- `INNER JOIN`
- `LEFT JOIN`
- `CROSS JOIN`
correct: 1
explanation:
`LEFT JOIN` preserves left-side rows and returns `NULL` for right-side columns when there is no match.
```

```interview-code
language: sql
prompt: Complete the query so it returns products in selected colors and a price range.
starter:
SELECT ProductID, Name, Color, ListPrice
FROM SalesLT.Product
WHERE Color
solution:
SELECT ProductID, Name, Color, ListPrice
FROM SalesLT.Product
WHERE Color IN ('Black', 'Red')
  AND ListPrice BETWEEN 500 AND 1500
ORDER BY ListPrice DESC;
checks:
- includes: Color IN
- includes: ListPrice BETWEEN 500 AND 1500
- includes: ORDER BY ListPrice DESC
```

## Aggregation

```interview-question
What does `GROUP BY` change about a query's result shape?
---
answer:
`GROUP BY` collapses detail rows into one summary row per group.

The grouped columns define the result grain, and aggregate functions summarize values inside each group.
hints:
- Detail rows become summary rows.
- Grouped columns define the grain.
- Aggregates summarize each group.
```

```interview-code
language: sql
prompt: Complete the grouped query so it returns categories with at least 5 products.
starter:
SELECT pc.Name AS CategoryName, COUNT(*) AS ProductCount
FROM SalesLT.ProductCategory AS pc
JOIN SalesLT.Product AS p
  ON p.ProductCategoryID = pc.ProductCategoryID
GROUP BY pc.Name
solution:
SELECT pc.Name AS CategoryName, COUNT(*) AS ProductCount
FROM SalesLT.ProductCategory AS pc
JOIN SalesLT.Product AS p
  ON p.ProductCategoryID = pc.ProductCategoryID
GROUP BY pc.Name
HAVING COUNT(*) >= 5
ORDER BY ProductCount DESC;
checks:
- includes: GROUP BY pc.Name
- includes: HAVING COUNT(*) >= 5
- includes: ORDER BY ProductCount DESC
```
