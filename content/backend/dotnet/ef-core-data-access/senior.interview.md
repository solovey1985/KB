---
title: EF Core and Data Access Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level EF Core and data access scenarios from the Web API interview question set.

Relevant concept maps:

- [Querying and Mapping Concept Map](ef-core-querying-mapping.concept.md)
- [Persistence and Scale Concept Map](ef-core-persistence-scale.concept.md)

## N+1 Queries

```interview-question
Your API returns 200 products but SQL Profiler shows 201 queries. What happened and how do you fix it?
---
answer:
This is the N+1 query problem. One query loaded the products, and additional queries were triggered for a related navigation property, often because of lazy loading.

Fix it by eagerly loading the related data with `Include(...)`, projecting directly into DTOs, or using `AsSplitQuery()` when a large joined query would create a cartesian explosion.

The best fix depends on whether you need full entities or only a small response shape.
hints:
- One query loaded the root set.
- The rest came from relationship access.
- Projection is often the fastest fix when you only need response fields.
```

Related concepts: [N+1 Queries](ef-core-persistence-scale.concept.md#n-plus-1-queries), [Projection](ef-core-querying-mapping.concept.md#projection), [Split Queries](ef-core-persistence-scale.concept.md#split-queries)

```interview-choice
Which approach is often best when an endpoint only needs a response DTO and not full tracked entities?
---
options:
- Projection with `Select(...)`
- Rely on lazy loading
- Loop over entities and access navigation properties later
correct: 0
explanation:
Projection usually generates one efficient query and loads only the data needed for the response shape.
```

## Concurrency Control

```interview-question
Two users submit conflicting updates to the same product price at the same time. How do you handle this?
---
answer:
Use optimistic concurrency with a concurrency token such as a `RowVersion` column.

EF Core includes that token in the `WHERE` clause of the update. If another update changed the row first, the affected row count becomes zero and EF Core throws `DbUpdateConcurrencyException`.

The API should usually translate that into `409 Conflict` so the client can reload the current state and retry intentionally.
hints:
- The solution is usually optimistic, not pessimistic, locking.
- A special column tracks whether the row changed.
- The conflict appears when the update no longer matches the original token.
```

Related concepts: [Optimistic Concurrency](ef-core-persistence-scale.concept.md#optimistic-concurrency)

## Soft Delete

```interview-question
You need to soft-delete records instead of hard-deleting them. How do you implement this globally across all entities?
---
answer:
Use a soft-delete contract such as `IsDeleted` and `DeletedAt`, add a global query filter so deleted rows are excluded automatically, and intercept delete operations in `SaveChangesAsync()` to turn them into updates.

This makes soft-delete the default behavior across the model instead of relying on every query author to remember a manual `WHERE IsDeleted = false` clause.

Administrative or audit scenarios can opt out with `IgnoreQueryFilters()`.
hints:
- The goal is a global rule, not manual repetition.
- Query filters hide deleted rows by default.
- Deletes become updates at save time.
```

Related concepts: [Soft Delete](ef-core-persistence-scale.concept.md#soft-delete), [Global Query Filters](ef-core-persistence-scale.concept.md#global-query-filters)

```interview-choice
Why is adding `WHERE IsDeleted = false` manually to every query a weak approach?
---
options:
- EF Core does not support it
- Someone will eventually forget it and leak deleted rows
- It prevents migrations from running
correct: 1
explanation:
Manual filtering is fragile because it depends on every query author remembering the rule every time.
```

## Query Timeouts at Scale

```interview-question
An EF Core query works fine with 100 records but times out with 100,000. How do you diagnose and fix it?
---
answer:
Start by logging the generated SQL with `ToQueryString()` and inspect the execution plan in the database tool.

Typical fixes include adding missing indexes, reducing loaded columns with projection, avoiding cartesian explosions from multiple `Include(...)` calls, adding pagination, or using compiled queries for hot paths.

Increasing the timeout only hides the real issue and should not be the first response.
hints:
- Inspect the generated SQL before changing code randomly.
- Query shape and indexing are the usual suspects.
- Timeouts are symptoms, not fixes.
```

Related concepts: [Query Diagnosis](ef-core-persistence-scale.concept.md#query-diagnosis), [Projection](ef-core-querying-mapping.concept.md#projection), [Split Queries](ef-core-persistence-scale.concept.md#split-queries)

## Seeding Strategy

```interview-question
How do you seed initial data in EF Core, and what are the trade-offs of each approach?
---
answer:
`HasData()` is good for small stable reference data and is tracked through migrations, but it becomes awkward for large or computed datasets.

Custom initialization logic is flexible and can use application services and navigation logic, but it is not automatically tracked in migrations.

SQL in migrations gives full control for complex production data changes, but it is database-specific and less portable.
hints:
- There is no single best seeding approach.
- Reference data and environment-specific data are different problems.
- Migration tracking is one of the main trade-offs.
```

Related concepts: [Data Seeding](ef-core-persistence-scale.concept.md#data-seeding)
*** Add File: content/backend/dotnet/ef-core-data-access/ef-core-querying-mapping.concept.md
---
title: EF Core Querying and Mapping Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core EF Core querying and mapping concepts behind the junior and middle interview questions.

## Core Map

```concept-card
id: ef-core-data-access
term: EF Core Data Access
children:
- tracking-queries
- no-tracking-queries
- projection
- fluent-relationship-mapping
- delete-behavior
- value-converters
summary:
EF Core data access combines object mapping, query translation, tracking behavior, and relationship configuration into one persistence model.
details:
Understanding when entities are tracked, how relationships are mapped, and how query shapes affect SQL explains most day-to-day EF Core behavior.
mnemonic:
Map clearly, query intentionally, track only when needed.
recall:
- Which parts of EF Core most affect everyday query behavior?
- Why are tracking and projection core concepts rather than optimizations only?
```

```concept-card
id: tracking-queries
term: Tracking Queries
parents:
- ef-core-data-access
related:
- no-tracking-queries
summary:
Tracking queries keep returned entities in the change tracker so EF Core can detect and persist modifications.
details:
Tracking is useful when the same context will update the loaded entities, but it adds memory and CPU overhead for read-only endpoints.
mnemonic:
Tracked means ready to update.
recall:
- Why does EF Core track entities by default?
- What cost does tracking add to read-heavy endpoints?
```

```concept-card
id: no-tracking-queries
term: No-Tracking Queries
parents:
- ef-core-data-access
related:
- tracking-queries
summary:
No-tracking queries skip the change tracker to make read-only queries lighter and faster.
details:
They are a strong default for list endpoints, reporting, and DTO-oriented queries where the entity will not be edited in the same context.
example:
`await db.Products.AsNoTracking().ToListAsync();`
mnemonic:
Read it, return it, forget it.
recall:
- When is `AsNoTracking()` usually the right choice?
- Why is it a weak default for write workflows?
```

```concept-card
id: projection
term: Projection
parents:
- ef-core-data-access
related:
- n-plus-1-queries
summary:
Projection shapes query results directly into DTOs or anonymous types instead of loading full entities.
details:
It reduces transferred columns, often avoids unnecessary tracking, and is one of the best ways to improve read-path efficiency.
example:
`db.Products.Select(p => new ProductDto { Name = p.Name, CategoryName = p.Category.Name })`
mnemonic:
Load the shape you need, not the entity you know.
recall:
- Why is projection often better than loading full entities for API responses?
- How can projection help with N+1-style response problems?
```

```concept-card
id: fluent-relationship-mapping
term: Fluent Relationship Mapping
parents:
- ef-core-data-access
children:
- delete-behavior
summary:
Fluent relationship mapping defines navigations, foreign keys, and relationship behavior in the model configuration layer.
details:
It keeps persistence rules centralized and explicit, which scales better than relying entirely on attributes scattered across entity types.
example:
`builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);`
mnemonic:
Map relationships where mapping belongs.
recall:
- Why do many teams prefer Fluent API over data annotations for relationships?
- What core decisions are made in relationship mapping?
```

```concept-card
id: delete-behavior
term: Delete Behavior
parents:
- fluent-relationship-mapping
summary:
Delete behavior defines what happens to related rows when the principal row is deleted.
details:
Common choices include `Restrict`, `Cascade`, and `SetNull`. This decision should be explicit because default cascading can be dangerous in production data models.
mnemonic:
Delete one, know the ripple.
recall:
- Why should delete behavior be configured explicitly?
- When is cascade delete too risky?
```

```concept-card
id: value-converters
term: Value Converters
parents:
- ef-core-data-access
summary:
Value converters transform a property between its CLR representation and its database representation.
details:
They are useful for enums stored as strings, strongly typed IDs, JSON storage, and encryption boundaries, but complex conversion logic can affect query translation.
example:
`builder.Property(p => p.Status).HasConversion<string>();`
mnemonic:
One value, two representations.
recall:
- What kinds of persistence problems do value converters solve well?
- What trade-off appears when converter logic becomes too complex?
```
