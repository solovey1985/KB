---
title: Data Platform Scenarios Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise SQL's role in transactional, reporting, ETL, and mixed-platform scenarios.

Study pages: [Data Platform Scenarios](data-platform-scenarios.md) | [Concept Map](data-platform-scenarios.concept.md)

## Workload Types

```interview-question
What is the difference between OLTP and OLAP?
---
answer:
OLTP systems optimize small, consistent business transactions such as orders and updates.

OLAP systems optimize analytical reads over larger datasets for reporting, trend analysis, and decision support.
hints:
- One is transaction-heavy.
- One is analysis-heavy.
- Schema and indexing choices differ.
```

```interview-choice
AdventureWorksLT customer, product, and order tables are closest to which workload style?
---
options:
- OLTP
- OLAP
- Search index
correct: 0
explanation:
AdventureWorksLT models normalized transactional data such as customers, products, orders, and order details.
```

```interview-question
What is a reporting query?
---
answer:
A reporting query summarizes operational data into business metrics such as totals, counts, rankings, and trends.

It commonly uses joins, filters, grouping, aggregate functions, and ordering.
hints:
- It produces business-friendly summaries.
- It often uses `GROUP BY`.
- It may rank or order results.
```

## ETL And Warehousing

```interview-question
What does ETL stand for?
---
answer:
ETL stands for extract, transform, and load.

Data is extracted from sources, transformed or cleansed into a usable shape, and loaded into a target such as a reporting table or data warehouse.
hints:
- Three phases.
- One phase cleans or reshapes data.
- One phase writes to the target.
```

```interview-choice
Which SQL query is most clearly an ETL data-quality check?
---
options:
- A query that finds duplicate email addresses before loading customer data.
- A query that displays one product by primary key.
- A query that grants execute permission on a stored procedure.
correct: 0
explanation:
Finding duplicates before loading data is a validation and cleansing activity common in ETL pipelines.
```

```interview-code
language: sql
prompt: Complete the query so it finds duplicate customer email addresses.
starter:
SELECT EmailAddress, COUNT(*) AS DuplicateCount
FROM SalesLT.Customer
WHERE EmailAddress IS NOT NULL
GROUP BY EmailAddress
solution:
SELECT EmailAddress, COUNT(*) AS DuplicateCount
FROM SalesLT.Customer
WHERE EmailAddress IS NOT NULL
GROUP BY EmailAddress
HAVING COUNT(*) > 1;
checks:
- includes: WHERE EmailAddress IS NOT NULL
- includes: GROUP BY EmailAddress
- includes: HAVING COUNT(*) > 1
```

## Mixed Platforms

```interview-question
How should SQL fit into a system that also uses caches, search indexes, or NoSQL stores?
---
answer:
SQL should usually remain the transactional source of truth when relational integrity and consistency matter.

Other stores can serve specialized read patterns, but they introduce synchronization and consistency tradeoffs.
hints:
- Think source of truth.
- Other stores optimize access patterns.
- Duplication creates synchronization cost.
```

```interview-question
When might a summary table or warehouse be better than querying OLTP tables directly?
---
answer:
Use a summary table or warehouse when reports repeatedly scan or aggregate large operational datasets and interfere with transactional workload performance.

The tradeoff is that summaries must be refreshed and may not be fully real-time.
hints:
- Read-heavy repeated reports.
- Protect transactional workload.
- Refresh strategy matters.
```
