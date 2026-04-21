---
title: EF Core Persistence and Scale Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the scale, persistence, and production-focused EF Core concepts from the interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Scale Map

```concept-card
id: ef-core-production-scale
term: EF Core Production and Scale
children:
- migrations-pipeline
- optimistic-concurrency
- soft-delete
- global-query-filters
- raw-sql
- executeupdate-and-executedelete
- n-plus-1-queries
- split-queries
- query-diagnosis
- data-seeding
summary:
EF Core production and scale concerns focus on safe schema changes, concurrency, bulk operations, and diagnosing inefficient queries under load.
details:
These concerns matter once an API moves beyond simple CRUD and starts operating with large datasets, concurrent users, and deployment pipelines.
mnemonic:
Change safely, query efficiently, scale deliberately.
recall:
- Which EF Core concerns become much more important in production than in prototypes?
- Why do concurrency and query shape belong in the same mental model of scale?
```

```concept-card
id: migrations-pipeline
term: Migrations Pipeline
parents:
- ef-core-production-scale
summary:
The migrations pipeline is the controlled process for applying schema changes through deployment rather than through random app startup behavior.
details:
A separate deployment step avoids race conditions across multiple app instances and gives the team a clear place to review, run, and audit schema changes.
example:
Run `dotnet ef migrations script -idempotent` in CI and apply the script before deploying the new app version.
mnemonic:
Deploy schema on purpose, not by surprise.
recall:
- Why is app-startup migration risky in multi-instance production?
- What does a dedicated migration step improve?
```

```concept-card
id: optimistic-concurrency
term: Optimistic Concurrency
parents:
- ef-core-production-scale
summary:
Optimistic concurrency detects conflicting updates by verifying that a row still has the same concurrency token when it is written.
details:
A `RowVersion` or similar token is included in the update condition. If another writer changed the row first, EF Core raises `DbUpdateConcurrencyException` and the API can return `409 Conflict`.
example:
`[Timestamp] public byte[] RowVersion { get; set; } = null!;`
mnemonic:
Update only if the row is still the one you read.
recall:
- Why is optimistic concurrency a better default than pessimistic locking for many APIs?
- What tells EF Core that a conflicting write happened?
```

```concept-card
id: soft-delete
term: Soft Delete
parents:
- ef-core-production-scale
related:
- global-query-filters
summary:
Soft delete marks a row as deleted instead of physically removing it from the database.
details:
It preserves recoverability and auditability, but it needs global enforcement so deleted rows do not leak into normal application queries.
example:
An admin screen may use `IgnoreQueryFilters()` to review deleted records, while normal product lists hide them automatically.
mnemonic:
Hide by default, keep for audit.
recall:
- Why do teams choose soft delete over hard delete?
- What must be added so soft-deleted rows do not appear everywhere?
```

```concept-card
id: global-query-filters
term: Global Query Filters
parents:
- ef-core-production-scale
related:
- soft-delete
summary:
Global query filters automatically apply predicates to all queries for an entity type.
details:
They are commonly used for soft delete and tenant isolation because they turn a cross-cutting rule into a model-level default instead of a manual query habit.
example:
`modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);`
mnemonic:
Set the rule once, apply it everywhere.
recall:
- Why are global query filters safer than manual repeated predicates?
- Which cross-cutting concerns commonly use them?
```

```concept-card
id: raw-sql
term: Raw SQL
parents:
- ef-core-production-scale
summary:
Raw SQL is the controlled use of handwritten SQL inside EF Core for cases where LINQ is insufficient or inefficient.
details:
It is powerful and sometimes necessary, but it must stay parameterized to remain safe against SQL injection.
example:
`FromSqlInterpolated($"SELECT * FROM Products WHERE Price > {minPrice}")` is safe, while string concatenation with user input is not.
mnemonic:
Use exact SQL only when exact control is worth it.
recall:
- When is raw SQL a reasonable choice in EF Core?
- What is the core safety rule when user input is involved?
```

```concept-card
id: executeupdate-and-executedelete
term: ExecuteUpdate and ExecuteDelete
parents:
- ef-core-production-scale
summary:
`ExecuteUpdateAsync` and `ExecuteDeleteAsync` perform bulk changes directly in the database without materializing entities.
details:
They are ideal for large set-based updates and deletes, but they bypass the normal change tracker lifecycle.
example:
`await db.Products.Where(p => p.CategoryId == id).ExecuteUpdateAsync(...);`
mnemonic:
Set-based work belongs in set-based SQL.
recall:
- Why are these APIs more efficient than load-modify-save loops?
- What application behavior might be skipped because the change tracker is bypassed?
```

```concept-card
id: n-plus-1-queries
term: N+1 Queries
parents:
- ef-core-production-scale
related:
- projection
- split-queries
summary:
N+1 queries happen when one query loads the main set and additional queries are triggered per item for related data.
details:
They usually appear through lazy loading or response code that touches navigation properties after the main query. Projection and eager loading are common fixes.
example:
Loading 200 products and then reading `product.Category.Name` in a loop can trigger 200 extra queries.
mnemonic:
One root query, too many follow-ups.
recall:
- What behavior usually triggers N+1 queries?
- Why is projection often a strong fix?
```

```concept-card
id: split-queries
term: Split Queries
parents:
- ef-core-production-scale
related:
- n-plus-1-queries
summary:
Split queries break a large included graph into multiple SQL queries to avoid cartesian explosion.
details:
They are helpful when a single joined query would duplicate rows massively because of multiple collection includes.
example:
`db.Products.Include(p => p.Tags).Include(p => p.Reviews).AsSplitQuery()`
mnemonic:
Split the load before the join explodes.
recall:
- What problem do split queries solve?
- When can a single included query become too large?
```

```concept-card
id: query-diagnosis
term: Query Diagnosis
parents:
- ef-core-production-scale
summary:
Query diagnosis is the process of inspecting generated SQL, execution plans, and data shape to understand why a query is slow.
details:
Typical fixes include indexes, projection, pagination, and avoiding large joined graphs. Diagnosis starts with observing the actual SQL instead of guessing.
example:
`var sql = query.ToQueryString();`
mnemonic:
Read the SQL before changing the code.
recall:
- What is the first useful step when an EF Core query slows down at scale?
- Why is increasing the command timeout usually not a real fix?
```

```concept-card
id: data-seeding
term: Data Seeding
parents:
- ef-core-production-scale
summary:
Data seeding is the controlled creation of initial or reference data for an application.
details:
`HasData` fits small static reference data, custom initialization fits flexible startup data, and SQL in migrations fits provider-specific production transformations.
example:
Seed `Electronics` and `Clothing` with `HasData`, but use a migration script for a large production backfill.
mnemonic:
Seed reference data simply, migrate complex data deliberately.
recall:
- Which seeding approach fits stable reference data well?
- Why is there no single best seeding mechanism for every dataset?
```
