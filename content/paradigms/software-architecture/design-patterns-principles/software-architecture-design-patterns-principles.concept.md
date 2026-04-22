---
title: Design Patterns and Principles Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the common patterns and principles that appear in architecture interviews.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-design-patterns-principles.md) | [Interview Practice](software-architecture-design-patterns-principles.interview.md)

## Patterns Map

```concept-card
id: architectural-pattern
term: Architectural Pattern
children:
- mvc
- publish-subscribe
- monolith
- microservices-architecture
summary:
An architectural pattern is a reusable high-level way to structure system responsibilities and interactions.
details:
Patterns help teams reason from known trade-offs instead of inventing structure from scratch, but they still need context-sensitive judgment.
example:
Choosing a layered monolith or an event-driven microservice system are architectural pattern choices.
mnemonic:
Recurring structure for recurring problems.
recall:
- Why is a pattern not automatically the right answer?
- What makes a pattern architectural instead of purely local design?
```

```concept-card
id: mvc
term: MVC
parents:
- architectural-pattern
summary:
Model-View-Controller separates state and behavior, presentation, and user interaction coordination.
details:
It helps keep UI concerns from collapsing into one large unit, but it can still degrade if controllers become bloated.
example:
A web app controller receives input, updates the model, and selects the view to render.
mnemonic:
Model holds, view shows, controller coordinates.
recall:
- What responsibility belongs to the controller?
- Why can MVC still become messy in practice?
```

```concept-card
id: publish-subscribe
term: Publish-Subscribe
parents:
- architectural-pattern
related:
- microservices-architecture
summary:
Publish-subscribe lets producers emit events without knowing all consumers ahead of time.
details:
It reduces direct coupling between senders and receivers but adds observability, ordering, and idempotency challenges.
example:
An order service publishes `OrderPlaced`, and billing and notification handlers subscribe independently.
mnemonic:
Publish once, let many react.
recall:
- Why does publish-subscribe reduce direct dependency?
- What runtime complexity does it introduce?
```

```concept-card
id: monolith
term: Monolith
related:
- microservices-architecture
summary:
A monolith packages major capabilities into one deployable application.
details:
It often simplifies transactions, debugging, and local development, but can become hard to change if internal boundaries are weak.
example:
An ecommerce app with catalog, checkout, and admin features deployed as one service is a monolith.
mnemonic:
One deployable, many capabilities.
recall:
- What are the real strengths of a monolith?
- When does a monolith become a maintenance problem?
```

```concept-card
id: microservices-architecture
term: Microservices Architecture
related:
- monolith
- publish-subscribe
summary:
Microservices architecture splits a system into independently deployable services with explicit contracts.
details:
It can improve autonomy and service-level scaling, but it raises operational, observability, and consistency complexity.
example:
Separate order, payment, and shipping services communicate through APIs and events.
mnemonic:
Smaller services, bigger operational burden.
recall:
- What benefits do teams hope to gain from microservices?
- What new failure modes appear after the split?
```

```concept-card
id: solid-principles
term: SOLID Principles
summary:
SOLID principles are design heuristics that encourage maintainable abstractions and healthier boundaries.
details:
Even though they are often taught with classes, the same ideas influence architectural module design and dependency direction.
example:
Depending on abstractions instead of infrastructure details supports cleaner tests and more replaceable modules.
mnemonic:
Keep boundaries focused and dependencies intentional.
recall:
- Why do SOLID ideas matter beyond class design?
- Which SOLID idea most directly shapes dependency direction?
```
