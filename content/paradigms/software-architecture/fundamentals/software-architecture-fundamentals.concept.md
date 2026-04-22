---
title: Software Architecture Fundamentals Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page captures the core concepts behind architecture vocabulary and structural trade-offs.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-fundamentals.md) | [Interview Practice](software-architecture-fundamentals.interview.md)

## Fundamentals Map

```concept-card
id: software-architecture
term: Software Architecture
children:
- separation-of-concerns
- quality-attribute
- modularity
- coupling
- cohesion
- cross-cutting-concern
summary:
Software architecture defines the high-level structure of a system and the trade-offs behind its major decisions.
details:
It focuses on boundaries, responsibilities, communication styles, and the quality attributes the system must satisfy.
example:
Choosing layered architecture with an API, application, domain, and infrastructure boundary is an architectural decision.
mnemonic:
Shape first, details later.
recall:
- What kinds of decisions are architectural rather than local design choices?
- Why are architecture decisions expensive to reverse?
```

```concept-card
id: separation-of-concerns
term: Separation of Concerns
parents:
- software-architecture
summary:
Separation of concerns means dividing the system so each part focuses on one kind of responsibility.
details:
It helps reduce accidental coupling and makes changes more local, predictable, and testable.
example:
Keeping validation, persistence, and UI rendering in separate modules is separation of concerns.
mnemonic:
Split by responsibility, not by accident.
recall:
- Why does separation of concerns make systems easier to change?
- What happens when one module handles too many kinds of work?
```

```concept-card
id: quality-attribute
term: Quality Attribute
parents:
- software-architecture
related:
- modularity
summary:
A quality attribute is a non-functional property such as scalability, reliability, performance, or security.
details:
Architectural decisions are usually evaluated by how well they support the most important quality attributes for the system.
example:
Choosing asynchronous processing might improve scalability and resilience but add consistency complexity.
mnemonic:
Features say what, quality attributes say how well.
recall:
- Why do quality attributes drive architecture?
- Which architectural trade-offs improve one attribute but hurt another?
```

```concept-card
id: modularity
term: Modularity
parents:
- software-architecture
related:
- coupling
- cohesion
summary:
Modularity means structuring the system into parts with clear responsibilities and interfaces.
details:
Good modules can evolve somewhat independently because their internal details are hidden behind stable boundaries.
example:
A payment module can expose a small API without leaking database details to the rest of the system.
mnemonic:
Strong boundaries, focused parts.
recall:
- What makes a module truly useful instead of just being a folder split?
- Why do clear interfaces matter for modularity?
```

```concept-card
id: coupling
term: Coupling
parents:
- software-architecture
related:
- cohesion
summary:
Coupling measures how strongly one part of the system depends on another.
details:
Low coupling is usually preferred because it reduces ripple effects when code, infrastructure, or contracts change.
example:
A service that directly knows another service's schema, database, and deployment order is tightly coupled.
mnemonic:
Tighter dependency, harder change.
recall:
- Why is tight coupling dangerous at system scale?
- What kinds of hidden coupling show up in distributed systems?
```

```concept-card
id: cohesion
term: Cohesion
parents:
- software-architecture
related:
- coupling
summary:
Cohesion measures how well the responsibilities inside one module belong together.
details:
High cohesion means a module has a focused purpose instead of collecting unrelated behavior.
example:
A reporting module that only handles report generation is more cohesive than one that also sends emails and manages users.
mnemonic:
One area, one focus.
recall:
- What does low cohesion feel like in a codebase?
- Why do high cohesion and low coupling usually reinforce each other?
```

```concept-card
id: cross-cutting-concern
term: Cross-Cutting Concern
parents:
- software-architecture
summary:
A cross-cutting concern affects many parts of a system and cannot be cleanly isolated inside one business module.
details:
Examples include logging, authentication, auditing, and observability. Architecture should centralize these concerns instead of duplicating them everywhere.
example:
HTTP middleware can apply authentication and logging across all endpoints consistently.
mnemonic:
One concern, many touch points.
recall:
- Why are cross-cutting concerns easy to scatter by accident?
- What architectural mechanisms help centralize them?
```
