---
title: EF Core and Data Access Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level EF Core and data access distinctions from the Web API interview question set.

Relevant concept maps:

- [Querying and Mapping Concept Map](ef-core-querying-mapping.concept.md)
- [Persistence and Scale Concept Map](ef-core-persistence-scale.concept.md)

## Relationship Mapping

```interview-question
How do you configure a one-to-many relationship in EF Core using Fluent API?
---
answer:
Define the reference and collection navigation pair with `HasOne(...).WithMany(...)`, specify the foreign key with `HasForeignKey(...)`, and choose the delete behavior explicitly with `OnDelete(...)`.

Fluent API is usually preferred because it keeps persistence rules out of the entity class and makes relationship behavior explicit in one place.
hints:
- One side has one reference.
- The other side has many children.
- The foreign key and delete behavior should be explicit.
```

Related concepts: [Fluent Relationship Mapping](ef-core-querying-mapping.concept.md#fluent-relationship-mapping), [Delete Behavior](ef-core-querying-mapping.concept.md#delete-behavior)

```interview-code
language: cs
prompt: Complete the relationship mapping so `Product` has one `Category` and uses `CategoryId` as the foreign key.
starter:
builder.HasOne(p => p.Category)
    .WithMany(c => c.Products)
    .
solution:
builder.HasOne(p => p.Category)
    .WithMany(c => c.Products)
    .HasForeignKey(p => p.CategoryId)
    .OnDelete(DeleteBehavior.Restrict);
checks:
- includes: HasForeignKey
- includes: CategoryId
- includes: DeleteBehavior.Restrict
```

## Single-Entity Query APIs

```interview-question
What is the difference between `FirstOrDefault()`, `SingleOrDefault()`, and `Find()` in EF Core?
---
answer:
`Find()` is the primary-key lookup API and checks the change tracker first, so it can return an already tracked entity without hitting the database.

`FirstOrDefault()` returns the first matching row or `null` and is used when multiple matches are possible but only the first matters.

`SingleOrDefault()` enforces that there is at most one match and throws if duplicates exist, which makes it the right choice when uniqueness is part of the assumption.
hints:
- One method is PK-oriented and change-tracker aware.
- One returns the first match.
- One verifies uniqueness.
```

Related concepts: [Tracking Queries](ef-core-querying-mapping.concept.md#tracking-queries)

```interview-choice
Which API is the best fit for looking up an entity by primary key when it may already be tracked in the current context?
---
options:
- `FindAsync`
- `FirstOrDefaultAsync`
- `SingleOrDefaultAsync`
correct: 0
explanation:
`FindAsync` is optimized for primary-key lookup and can return the tracked entity directly without querying the database again.
```
