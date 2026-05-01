---
title: Data Platform Scenarios Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map connects SQL skills to common data platform workloads.

Study pages: [Data Platform Scenarios](data-platform-scenarios.md) | [Interview Practice](data-platform-scenarios.interview.md)

```concept-card
id: data-platform-scenarios
term: Data Platform Scenarios
children:
- oltp
- olap
- reporting-query
- etl
- data-warehouse
- mixed-data-platform
summary:
Data platform scenarios describe how SQL is used across transactional systems, reporting, ETL, warehousing, and mixed storage architectures.
details:
The same SQL language appears in different workloads, but the schema design, query style, and performance priorities change by scenario.
recall:
- Why does workload type change SQL design choices?
- Which scenarios prioritize writes versus reads?
```

```concept-card
id: oltp
term: OLTP
aliases:
- Online Transaction Processing
parents:
- data-platform-scenarios
related:
- olap
summary:
OLTP systems optimize small, consistent business transactions.
details:
They usually use normalized schemas, constraints, and short transactions for operations such as creating orders or updating customer data.
example:
AdventureWorksLT order tables are OLTP-style tables.
recall:
- What does OLTP optimize for?
- Why are transactions important in OLTP?
```

```concept-card
id: olap
term: OLAP
aliases:
- Online Analytical Processing
parents:
- data-platform-scenarios
related:
- oltp
- data-warehouse
summary:
OLAP systems optimize analytical reads over large volumes of data.
details:
They commonly use denormalized, dimensional, or summary structures to support reporting and analysis.
recall:
- How does OLAP differ from OLTP?
- Why are summaries common in analytical systems?
```

```concept-card
id: reporting-query
term: Reporting Query
parents:
- data-platform-scenarios
summary:
A reporting query summarizes operational data into business-friendly metrics.
details:
It often joins multiple tables and uses grouping, aggregation, filtering, and ordering to produce totals or rankings.
example:
SUM(TotalDue) by customer from SalesLT.SalesOrderHeader.
recall:
- What makes a query a reporting query?
- Which SQL features appear most often in reports?
```

```concept-card
id: etl
term: ETL
aliases:
- Extract Transform Load
parents:
- data-platform-scenarios
related:
- data-warehouse
summary:
ETL extracts data, transforms it into a usable shape, and loads it into a target system.
details:
SQL is often used for staging, cleansing, deduplication, validation, and loading summary or warehouse tables.
recall:
- What are the three ETL phases?
- Why should ETL scripts be repeatable?
```

```concept-card
id: data-warehouse
term: Data Warehouse
parents:
- data-platform-scenarios
related:
- olap
- etl
summary:
A data warehouse stores integrated, historical data for analytics and reporting.
details:
Warehouses usually receive data through ETL or ELT pipelines and organize it for read-heavy analytical workloads.
recall:
- Why is warehouse design different from OLTP design?
- How does ETL feed a warehouse?
```

```concept-card
id: mixed-data-platform
term: Mixed Data Platform
parents:
- data-platform-scenarios
summary:
A mixed data platform combines SQL databases with other stores such as caches, queues, search indexes, data lakes, or NoSQL databases.
details:
SQL should usually remain the transactional source of truth when relational integrity matters, while other stores serve specialized access patterns.
recall:
- When should SQL remain the source of truth?
- What cost appears when the same data lives in multiple stores?
```
