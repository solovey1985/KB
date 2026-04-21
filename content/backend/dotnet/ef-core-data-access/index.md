# EF Core and Data Access

This section turns the EF Core and data access material from the `.NET Web API Interview Questions` PDF into focused study pages.

## Interview Practice By Level

- [Junior Questions](junior.interview.md)
- [Middle Questions](middle.interview.md)
- [Senior Questions](senior.interview.md)

## Concept Maps

- [Querying and Mapping Concept Map](ef-core-querying-mapping.concept.md)
- [Persistence and Scale Concept Map](ef-core-persistence-scale.concept.md)

## Study Flow

1. Start with [Junior Questions](junior.interview.md) to reinforce relationship mapping and query API choices.
2. Read the [Querying and Mapping Concept Map](ef-core-querying-mapping.concept.md) before moving into production concerns.
3. Use [Middle Questions](middle.interview.md) for tracking, migrations, raw SQL, and bulk operations.
4. Finish with [Senior Questions](senior.interview.md) and the [Persistence and Scale Concept Map](ef-core-persistence-scale.concept.md).

## Related Topics

- [ASP.NET Core Internals and Middleware](../aspnet-core-internals-middleware/index.md)
- [Performance and Caching](../performance-caching/index.md)
- [.NET Backend Study Index](../index.md)

## Topic Coverage

- tracking versus no-tracking queries
- `Find`, `FirstOrDefault`, and `SingleOrDefault`
- fluent relationship mapping
- migrations in CI/CD
- concurrency control
- raw SQL safety
- value converters
- bulk updates and deletes
- seeding approaches
- soft delete with global query filters
- diagnosing slow EF Core queries
- N+1 queries and split queries
