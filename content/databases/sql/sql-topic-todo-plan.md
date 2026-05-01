# SQL Topic Todo Plan

This plan prepares the SQL and T-SQL topic pages before adding more interview and concept blocks.

Source files:

- `sql.json` and `sql.answers.md`
- `t-sql.json` and `t-sql.answers.md`
- `sql-server/database-example/AdventureWorksLT2019.bak`

Practice database:

- Use `AdventureWorksLT2019` examples wherever possible.
- Prefer `SalesLT.Customer`, `SalesLT.Product`, `SalesLT.ProductCategory`, `SalesLT.SalesOrderHeader`, `SalesLT.SalesOrderDetail`, and `SalesLT.Address`.
- Keep examples safe by default: use `SELECT`, or wrap `INSERT` / `UPDATE` / `DELETE` examples in a transaction with `ROLLBACK`.

## Coverage Summary

- SQL source has 100 questions across fundamentals, data types, advanced queries, design, performance, security, transactions, ETL, scenarios, troubleshooting, and analytics.
- T-SQL source has 100 questions across SQL Server querying, data manipulation, CTEs, window functions, stored procedures, programming constructs, transactions, indexing, security, deployment, administration, and real-world scenarios.
- Free answer material exists for the first 15 SQL questions and first 15 T-SQL questions. Premium questions need authored answers.

## Document First

Create or refresh short documents before new question/concept pages. Each document should cover only 3-4 related subtopics.

1. `general/query-fundamentals.md`
   - Logical query shape: `SELECT`, `FROM`, `WHERE`, `GROUP BY`, `HAVING`, `ORDER BY`
   - Filtering with `WHERE`, `IN`, `BETWEEN`, `LIKE`, and `NULL`
   - Joining related rows with customer/order/product examples
   - Grouping and aggregate reporting over AdventureWorksLT sales data

2. `general/relational-design-and-integrity.md`
   - Primary keys, foreign keys, and referential integrity
   - Normalization, denormalization, and update anomalies
   - Entity-relationship modeling and many-to-many bridge tables
   - Constraints as the first line of data quality

3. `sql-server/t-sql/data-modification-and-transactions.md`
   - `INSERT`, `UPDATE`, `DELETE`, and conditional updates
   - Explicit transactions with `BEGIN TRAN`, `COMMIT`, and `ROLLBACK`
   - Isolation levels, locks, blocking, and deadlocks
   - `TRY...CATCH` patterns for safe T-SQL changes

4. `sql-server/t-sql/advanced-query-patterns.md`
   - Subqueries, CTEs, and recursive CTEs
   - Window functions: `ROW_NUMBER`, `RANK`, `DENSE_RANK`, running totals
   - `CASE`, `UNION` vs `UNION ALL`, and duplicate detection
   - Pagination with `OFFSET...FETCH`

5. `sql-server/performance-and-indexing.md`
   - Clustered vs nonclustered indexes and included columns
   - Reading execution plans and spotting scans, seeks, lookups, and sorts
   - Slow query triage with statistics and measured baselines
   - Index tradeoffs for reads, writes, and maintenance

6. `sql-server/t-sql/programmability-and-security.md`
   - Stored procedures, scalar functions, and table-valued functions
   - Triggers and when not to use them
   - Dynamic SQL and parameterization against SQL injection
   - Permissions, roles, and least-privilege access

7. `general/data-platform-scenarios.md`
   - OLTP vs OLAP and reporting query design
   - ETL basics: extract, transform, load, validate
   - Warehousing, materialized summaries, and calendar tables
   - SQL with cloud stores, data lakes, and mixed SQL/NoSQL systems

## Then Add Interactive Pages

1. Update indexes so every new document is reachable from `index.md`, `general/index.md`, or `sql-server/index.md`.
2. Build or refresh concept pages from the documents, not directly from the raw question list.
3. Build or refresh interview pages using `AGENTS.md` block syntax.
4. Use `sql.answers.md` and `t-sql.answers.md` as the base for the first 15 questions in each topic.
5. Author premium answers from knowledge, aligned with the short documents and AdventureWorksLT examples.
6. Keep cross-links relative and connect every interview section to the relevant concept/document page.

## Verification

- Check that every new markdown link resolves.
- Check that fenced blocks use exact `interview-*` and `concept-card` syntax.
- Run sample SQL against `AdventureWorksLT2019` when the Docker SQL Server is available.
- Prefer examples that can be pasted into SSMS, Azure Data Studio, VS Code SQL tools, or `sqlcmd`.
