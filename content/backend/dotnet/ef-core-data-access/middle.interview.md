---
title: EF Core and Data Access Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level EF Core and data access trade-offs from the Web API interview question set.

Relevant concept maps:

- [Querying and Mapping Concept Map](ef-core-querying-mapping.concept.md)
- [Persistence and Scale Concept Map](ef-core-persistence-scale.concept.md)

## Production Migrations

```interview-question
How do you run EF Core migrations in a production CI/CD pipeline, and do you apply them at application startup?
---
answer:
Do not run production migrations automatically at application startup. In multi-instance deployments that creates race conditions, lock contention, and unpredictable startup failures.

Instead, generate and review migration scripts or run a dedicated migration step in CI/CD before the app deployment. Use idempotent scripts when possible so the same deployment step can be re-run safely.
hints:
- Startup is the wrong place for multi-instance schema changes.
- The deployment pipeline should own the migration step.
- Re-runnability matters.
```

Related concepts: [Migrations Pipeline](ef-core-persistence-scale.concept.md#migrations-pipeline)

```interview-choice
Which approach is usually the safest production default?
---
options:
- Call `Database.Migrate()` in `Program.cs`
- Run migrations as a separate deployment step
- Let each pod apply migrations on startup
correct: 1
explanation:
Running migrations in a dedicated deployment step avoids startup races and gives the team a clear control point for schema changes.
```

## Tracking Behavior

```interview-question
What is the difference between `AsNoTracking()` and the default tracking behavior in EF Core, and when do you use each?
---
answer:
Tracked queries keep entity instances in the change tracker so EF Core can detect modifications and persist them later.

`AsNoTracking()` skips that bookkeeping, which makes read-only queries faster and lighter on memory. It is the right choice for GET endpoints, reports, and other read scenarios where the entity will not be updated through the same context.

Use tracking when you plan to modify the returned entity and call `SaveChangesAsync()`.
hints:
- One mode remembers original values.
- The other is optimized for reads.
- The update workflow is the deciding factor.
```

Related concepts: [Tracking Queries](ef-core-querying-mapping.concept.md#tracking-queries), [No-Tracking Queries](ef-core-querying-mapping.concept.md#no-tracking-queries)

```interview-choice
Which choice is usually best for a simple read-only list endpoint?
---
options:
- `AsNoTracking()`
- Force tracking on every query
- Load entities, detach them manually later
correct: 0
explanation:
`AsNoTracking()` is the normal read-only optimization because it avoids change-tracker overhead when updates are not needed.
```

## Raw SQL Safety

```interview-question
When is raw SQL acceptable in EF Core, and how do you prevent SQL injection?
---
answer:
Raw SQL is acceptable when EF Core cannot express the query efficiently, when database-specific features are needed, or when exact SQL control is required for a critical query.

Prevent SQL injection by using parameterized APIs such as `FromSqlInterpolated(...)` or explicit parameters with `FromSqlRaw(...)`. Never concatenate user input into SQL strings.
hints:
- The problem is not raw SQL itself but unsafe construction.
- Parameterization is the safety boundary.
- String concatenation with user input is the red flag.
```

Related concepts: [Raw SQL](ef-core-persistence-scale.concept.md#raw-sql)

```interview-choice
Which option is safe for user-provided filter values?
---
options:
- `FromSqlInterpolated($"SELECT * FROM Products WHERE Price > {minPrice}")`
- `FromSqlRaw($"SELECT * FROM Products WHERE Name = '{userInput}'")`
- String concatenation followed by `FromSqlRaw`
correct: 0
explanation:
`FromSqlInterpolated` parameterizes interpolated values instead of embedding them directly into the SQL string.
```

## Value Converters

```interview-question
What are value converters in EF Core and when would you use them?
---
answer:
Value converters transform a property value between the CLR model type and the database column representation.

They are useful for scenarios such as storing enums as strings, mapping strongly typed IDs, persisting JSON, or applying encryption and decryption logic at the persistence boundary.

They are powerful, but complex converters can affect query translation and should be used thoughtfully.
hints:
- The model type and database type do not have to match directly.
- Think enum-to-string and strongly typed IDs.
- Translation limits still matter.
```

Related concepts: [Value Converters](ef-core-querying-mapping.concept.md#value-converters)

## Bulk Updates

```interview-question
You need to execute a bulk update such as setting `IsActive = false` for all products in a category. What is the most efficient way?
---
answer:
Use `ExecuteUpdateAsync(...)` so EF Core issues a single SQL `UPDATE` statement without loading all entities into memory.

This is much more efficient than querying all rows, looping through them, and saving each entity. The same idea applies to bulk deletes with `ExecuteDeleteAsync(...)`.

The trade-off is that the change tracker is bypassed, so interceptors or in-memory entity events may not run.
hints:
- The best solution avoids loading every row.
- EF Core 7+ added dedicated APIs for this.
- Efficiency comes from one SQL statement.
```

Related concepts: [ExecuteUpdate and ExecuteDelete](ef-core-persistence-scale.concept.md#executeupdate-and-executedelete)

```interview-code
language: cs
prompt: Complete the query so it performs a single bulk update that sets `IsActive` to `false`.
starter:
await db.Products
    .Where(p => p.CategoryId == categoryId)
    .
solution:
await db.Products
    .Where(p => p.CategoryId == categoryId)
    .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsActive, false));
checks:
- includes: ExecuteUpdateAsync
- includes: SetProperty
- includes: IsActive
```
