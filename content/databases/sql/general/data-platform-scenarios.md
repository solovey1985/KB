# Data Platform Scenarios

SQL appears in transactional systems, reporting systems, ETL pipelines, and mixed data platforms. The query style changes with the workload.

## OLTP And OLAP

OLTP systems optimize small, consistent business transactions. OLAP systems optimize larger analytical reads.

AdventureWorksLT is closer to OLTP: customers, products, orders, and order details are normalized for transactional integrity.

```sql
SELECT TOP 10 SalesOrderID, CustomerID, OrderDate, TotalDue
FROM SalesLT.SalesOrderHeader
ORDER BY OrderDate DESC;
```

## Reporting Queries

Reporting queries usually aggregate many detail rows into business summaries.

```sql
SELECT
    c.CustomerID,
    c.CompanyName,
    SUM(soh.TotalDue) AS TotalSales
FROM SalesLT.Customer AS c
JOIN SalesLT.SalesOrderHeader AS soh
  ON soh.CustomerID = c.CustomerID
GROUP BY c.CustomerID, c.CompanyName
ORDER BY TotalSales DESC;
```

For repeated reports, consider views, summary tables, or indexed views when supported and justified.

## ETL Basics

ETL means extract, transform, and load. SQL is often used for staging, cleansing, deduplicating, and validating data.

```sql
SELECT EmailAddress, COUNT(*) AS DuplicateCount
FROM SalesLT.Customer
WHERE EmailAddress IS NOT NULL
GROUP BY EmailAddress
HAVING COUNT(*) > 1;
```

Good ETL scripts are repeatable, auditable, and safe to rerun.

## SQL In Mixed Platforms

Modern systems may combine SQL databases with caches, queues, search engines, data lakes, or NoSQL stores.

Keep SQL databases responsible for relational integrity and transactional truth. Use other stores when their access patterns justify the extra synchronization cost.
