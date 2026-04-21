---
title: Architecture Application Patterns Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the application-architecture concepts behind the architecture and system design interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Patterns Map

```concept-card
id: application-architecture-patterns
term: Application Architecture Patterns
children:
- repository-pattern-trade-off
- cqrs
- vertical-slice-architecture
- clean-architecture
- mediator-pattern
- feature-flags
summary:
Application architecture patterns organize how use cases, dependencies, and cross-cutting concerns are structured inside an application.
details:
These patterns shape code navigation, abstraction cost, testability, and how easily teams can evolve features without drowning in incidental structure.
example:
The same `CreateOrder` use case feels very different when it is spread across five layers versus kept together as one slice.
mnemonic:
Choose the structure that serves the domain.
recall:
- What problems are application architecture patterns trying to solve?
- Why is pattern choice a trade-off rather than a universal best practice?
```

```concept-card
id: repository-pattern-trade-off
term: Repository Pattern Trade-Off
parents:
- application-architecture-patterns
summary:
The repository pattern trade-off is the decision of whether an additional abstraction over data access adds real value or only duplicates the ORM.
details:
With EF Core, generic repositories often mirror `DbSet<T>` without simplifying the real design. Focused repositories or query services are more useful when they express a meaningful boundary.
example:
`IRepository<Product>.GetByIdAsync(id)` often adds no value over `db.Products.FindAsync(id)`.
mnemonic:
No wrapper without a real reason.
recall:
- Why do generic repositories over EF Core often feel redundant?
- When can a focused repository still be a good idea?
```

```concept-card
id: cqrs
term: CQRS
parents:
- application-architecture-patterns
summary:
CQRS separates the read side and write side of an application into different models or handlers.
details:
It is useful when reads and writes have different performance, modeling, or storage needs, but it adds extra concepts that simple CRUD systems may not need.
example:
A write model can validate ordering rules while a read model serves flattened order-history DTOs from a different store.
mnemonic:
Read and write can grow apart.
recall:
- What is the core split in CQRS?
- When does CQRS pay off and when does it mostly add ceremony?
```

```concept-card
id: vertical-slice-architecture
term: Vertical Slice Architecture
parents:
- application-architecture-patterns
related:
- clean-architecture
summary:
Vertical Slice Architecture organizes code by feature or use case instead of by technical layer.
details:
Each slice contains the endpoint, handler, validation, and DTOs for one operation, which keeps feature work local and reduces cross-project scattering.
example:
`Features/Orders/CreateOrder.cs` can hold the request, validator, handler, and endpoint mapping for one use case.
mnemonic:
Organize by behavior, not by layer first.
recall:
- What is grouped together in a vertical slice?
- Why does this structure often feel simpler for API teams?
```

```concept-card
id: clean-architecture
term: Clean Architecture
parents:
- application-architecture-patterns
related:
- vertical-slice-architecture
summary:
Clean Architecture organizes code into technical layers with dependency rules that protect the domain from infrastructure concerns.
details:
It can be valuable for complex domains and larger teams, but it also increases the number of places a single feature must touch.
mnemonic:
Strong layers, higher navigation cost.
recall:
- What does Clean Architecture optimize for?
- What is the main feature-development cost of layered separation?
```

```concept-card
id: mediator-pattern
term: Mediator Pattern
parents:
- application-architecture-patterns
summary:
The Mediator pattern routes requests through a mediator that dispatches them to handlers.
details:
It can centralize cross-cutting behaviors and keep endpoints thin, but it also adds indirection and can become ceremony in simpler codebases.
example:
`mediator.Send(new GetProductQuery(id))` adds a dispatch hop that may or may not be worth it for the project.
mnemonic:
Central dispatch, extra indirection.
recall:
- Why do teams adopt mediator libraries?
- Why do some teams remove them later?
```

```concept-card
id: feature-flags
term: Feature Flags
parents:
- application-architecture-patterns
summary:
Feature flags let teams ship code separately from rollout decisions.
details:
Good flag systems support targeted rollout, percentage rollout, and cleanup after release so temporary branching does not become permanent architecture debt.
example:
Enable `NewRecommendationEngine` for 10% of users before rolling it out globally.
mnemonic:
Ship first, expose later, delete afterward.
recall:
- What operational value do feature flags provide?
- Why must stale flags be removed?
```
