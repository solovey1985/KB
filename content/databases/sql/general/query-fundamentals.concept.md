---
title: SQL Query Fundamentals Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map organizes the core pieces of a relational `SELECT` query.

Study pages: [Query Fundamentals](query-fundamentals.md) | [Interview Practice](query-fundamentals.interview.md)

```concept-card
id: query-fundamentals
term: Query Fundamentals
children:
- logical-query-processing
- where-predicate
- join-operation
- grouping-and-aggregation
- having-filter
- result-ordering
summary:
Query fundamentals are the clauses and relationships used to retrieve, filter, combine, summarize, and sort relational data.
details:
They form the base mental model for readable SQL before moving into vendor-specific syntax or performance tuning.
example:
SELECT ... FROM ... JOIN ... WHERE ... GROUP BY ... HAVING ... ORDER BY ...;
recall:
- Which clauses shape rows before grouping?
- Which clauses work after grouping?
- Why should query shape be understood before tuning?
```

```concept-card
id: logical-query-processing
term: Logical Query Processing
parents:
- query-fundamentals
children:
- where-predicate
- grouping-and-aggregation
- having-filter
- result-ordering
summary:
Logical query processing is the conceptual order in which SQL clauses produce a result.
details:
`FROM` and joins identify rows, `WHERE` filters rows, `GROUP BY` groups rows, `HAVING` filters groups, `SELECT` projects columns, and `ORDER BY` sorts the final output.
mnemonic:
From rows, where filters, group summarizes, having filters groups.
recall:
- Why is `WHERE` evaluated before `SELECT` conceptually?
- Why can `HAVING` reference aggregates?
```

```concept-card
id: where-predicate
term: WHERE Predicate
parents:
- query-fundamentals
related:
- having-filter
summary:
A `WHERE` predicate filters individual rows before grouping or aggregation.
details:
It is used with comparisons, ranges, lists, pattern matching, and null checks such as `=`, `BETWEEN`, `IN`, `LIKE`, and `IS NULL`.
example:
WHERE Color IN ('Black', 'Red') AND ListPrice BETWEEN 500 AND 1500
recall:
- Why should aggregate filters not go in `WHERE`?
- What predicate checks for missing values?
```

```concept-card
id: join-operation
term: Join Operation
parents:
- query-fundamentals
summary:
A join combines rows from related tables using a join condition.
details:
Inner joins require matching rows. Left joins keep all left-side rows and fill missing right-side values with `NULL`.
example:
SalesLT.SalesOrderDetail joins SalesLT.Product on ProductID.
recall:
- When should an inner join be used?
- What does a left join preserve?
```

```concept-card
id: grouping-and-aggregation
term: Grouping And Aggregation
parents:
- query-fundamentals
related:
- having-filter
summary:
Grouping partitions rows into sets, and aggregate functions summarize each set.
details:
Common aggregates are `COUNT`, `SUM`, `AVG`, `MIN`, and `MAX`. The grouped columns define the result grain.
example:
SELECT ProductCategoryID, COUNT(*) FROM SalesLT.Product GROUP BY ProductCategoryID;
recall:
- What does result grain mean in a grouped query?
- Which aggregate counts rows?
```

```concept-card
id: having-filter
term: HAVING Filter
parents:
- query-fundamentals
related:
- where-predicate
- grouping-and-aggregation
summary:
`HAVING` filters groups after aggregation.
details:
Use it for conditions such as `COUNT(*) > 5` or `SUM(TotalDue) >= 10000` that depend on grouped values.
example:
HAVING COUNT(*) >= 5
mnemonic:
Having happens after grouping.
recall:
- Which clause filters groups?
- Why is `HAVING` not a replacement for `WHERE`?
```

```concept-card
id: result-ordering
term: Result Ordering
parents:
- query-fundamentals
summary:
`ORDER BY` sorts the final result set.
details:
Without `ORDER BY`, SQL does not guarantee row order. Stable ordering is especially important for top-N queries and pagination.
example:
ORDER BY TotalSales DESC, CustomerID ASC
recall:
- Why should result order not be assumed?
- Why does pagination require deterministic ordering?
```
