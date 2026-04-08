# Database Indexes: You Might Be Using Them Wrong

## What the video is emphasizing

Indexes are not “always add more = faster.” PostgreSQL’s own docs say indexes speed up row retrieval, but they also add system-wide overhead and should be used sensibly. Every extra index must be kept in sync on writes, which increases `INSERT`/`UPDATE`/`DELETE` cost. ([PostgreSQL][2])

The core practical lessons appear to be:

* Understand the underlying **B-tree** mental model.
* Design **composite indexes** in the order your queries actually filter/sort.
* Use **covering indexes** only when they avoid expensive table lookups.
* Avoid indexing columns with poor selectivity or indexes that are rarely used.
* Measure with query plans instead of assuming the optimizer will benefit.

## Documentation on index usage

### 1. Use indexes for access paths, not as a default decoration

Create an index when it supports a frequent and important access pattern:

* equality filters: `WHERE email = ?`
* range filters: `WHERE created_at >= ?`
* joins: `... JOIN users u ON u.id = orders.user_id`
* sorting/limiting: `ORDER BY created_at DESC LIMIT 20`

Do **not** add an index just because a column appears in queries occasionally. Each extra index increases write work and storage overhead. PostgreSQL explicitly warns that seldom-used indexes should be removed. ([PostgreSQL][3])

### 2. Composite index order is critical

For multicolumn B-tree indexes, PostgreSQL states they are most efficient when there are constraints on the **leading (leftmost) columns**. Equality conditions on leading columns, plus at most one inequality on the first non-equality column, are what most effectively narrow the scanned portion of the index. ([PostgreSQL][4])

Example:

```sql
CREATE INDEX idx_orders_customer_status_created
ON orders (customer_id, status, created_at);
```

This is strong for:

```sql
WHERE customer_id = ?
WHERE customer_id = ? AND status = ?
WHERE customer_id = ? AND status = ? AND created_at >= ?
```

It is much weaker for:

```sql
WHERE status = ?
WHERE created_at >= ?
```

because those skip the leftmost key.

**Rule of thumb:**

* Put the most consistently used equality predicates first.
* Then put range predicates.
* Put sort columns only if they align with the same query pattern.

### 3. Covering indexes are for avoiding heap/table reads

PostgreSQL’s `INCLUDE` lets you add non-key payload columns so the engine may satisfy a query via an **index-only scan**. The included columns are not used to navigate the B-tree; they are only there to avoid fetching the base table row when possible. ([PostgreSQL][5])

Example:

```sql
CREATE INDEX idx_orders_customer_created
ON orders (customer_id, created_at DESC)
INCLUDE (total_amount, status);
```

Good for:

```sql
SELECT total_amount, status
FROM orders
WHERE customer_id = ?
ORDER BY created_at DESC
LIMIT 20;
```

This can be excellent for hot read paths, but the tradeoff is a larger index and more write maintenance.

### 4. Do not index low-value columns blindly

Indexes on low-cardinality columns (for example, `is_active`, `status` with only 2–3 values) are often poor standalone indexes because they do not filter much. That aligns with the video summary’s mention of **low cardinality** as a common mistake. ([LinkedIn][6])

Bad standalone candidate:

```sql
CREATE INDEX idx_users_is_deleted ON users (is_deleted);
```

Often better:

```sql
CREATE INDEX idx_users_tenant_deleted
ON users (tenant_id, is_deleted);
```

Now the low-cardinality field is paired with a more selective leading column.

### 5. More indexes can make writes slower

Every insert/update/delete may need to update multiple index structures. PostgreSQL explicitly documents this overhead. ([PostgreSQL][3])

This means:

* write-heavy tables should have fewer, more intentional indexes
* avoid duplicate or near-duplicate indexes
* periodically audit unused indexes

Typical anti-pattern:

```sql
CREATE INDEX idx_a ON events (user_id);
CREATE INDEX idx_b ON events (user_id, created_at);
```

If the second index already serves the dominant access path, the first may be redundant.

### 6. Match indexes to real query shapes

Design indexes from the actual SQL, not from the schema alone.

Examples:

**Query**

```sql
SELECT *
FROM invoices
WHERE account_id = ?
  AND due_date < ?
ORDER BY due_date
LIMIT 50;
```

**Index**

```sql
CREATE INDEX idx_invoices_account_due
ON invoices (account_id, due_date);
```

Why this works:

* `account_id` is leading equality
* `due_date` supports the range
* same order can help with sorting

Another:

**Query**

```sql
SELECT id, name
FROM products
WHERE category_id = ?
  AND price BETWEEN ? AND ?;
```

**Index**

```sql
CREATE INDEX idx_products_category_price
ON products (category_id, price)
INCLUDE (name);
```

### 7. Validate with EXPLAIN, not intuition

The optimizer may choose a sequential scan if:

* too many rows match
* the index is not selective enough
* statistics indicate scanning the table is cheaper
* predicates don’t match the index prefix well

So the workflow should be:

1. Identify slow query.
2. Inspect `EXPLAIN` / `EXPLAIN ANALYZE`.
3. Create or adjust index based on the access path.
4. Re-check the plan.
5. Remove redundant indexes later if they add no value.

## Practical checklist

Before creating an index, ask:

* Is this query frequent enough to justify write overhead?
* Is the leading column selective?
* Does column order match equality → range → sort?
* Could `INCLUDE` avoid heap lookups?
* Is there already an existing composite index that covers this?
* Is the table write-heavy enough that this index may cost more than it saves?

## Minimal “safe” patterns

Common useful indexes:

```sql
-- point lookup
CREATE INDEX idx_users_email ON users (email);

-- tenant-scoped lookup
CREATE INDEX idx_orders_tenant_created ON orders (tenant_id, created_at DESC);

-- join/filter combo
CREATE INDEX idx_order_items_order_product ON order_items (order_id, product_id);

-- covering read path
CREATE INDEX idx_sessions_user_created
ON sessions (user_id, created_at DESC)
INCLUDE (ip_address, expires_at);
```

## Bottom line

The main message of the video is correct: indexes are powerful, but most problems come from **wrong column order, over-indexing, and indexing without thinking about selectivity and write cost**. The most important practical principle is: **design indexes around real query patterns, then verify with execution plans**. ([LinkedIn][6])

If you want, I can convert this into a **Markdown cheat sheet** or give you a **PostgreSQL/MySQL/SQL Server comparison** of index usage rules.

[2]: https://www.postgresql.org/docs/current/indexes.html?utm_source=chatgpt.com "Documentation: 18: Chapter 11. Indexes"
[3]: https://www.postgresql.org/docs/current/indexes-intro.html?utm_source=chatgpt.com "PostgreSQL: Documentation: 18: 11.1. Introduction"
[4]: https://www.postgresql.org/docs/current/indexes-multicolumn.html?utm_source=chatgpt.com "Documentation: 18: 11.3. Multicolumn Indexes"
[5]: https://www.postgresql.org/docs/current/indexes-index-only-scans.html?utm_source=chatgpt.com "11.9. Index-Only Scans and Covering Indexes"
[6]: https://www.linkedin.com/posts/jiunnhao_datamesh-bigdata-trino-activity-7430081101492248577-r1Im?utm_source=chatgpt.com "Boost Data Performance with Starburst | Jiunn Hao Choo ..."
