# Software Architecture Fundamentals

Software architecture describes the high-level structure of a system, the responsibilities of its major parts, and the trade-offs behind those choices.

## Architecture versus design

Architecture focuses on system-wide structure and decisions that are expensive to reverse.

Design focuses on local implementation details inside those architectural boundaries.

Examples of architectural decisions:

- monolith versus microservices
- synchronous versus event-driven communication
- layering and module boundaries
- choice of persistence and deployment model

Examples of design decisions:

- class responsibilities
- function signatures
- data mapping details
- validation logic inside a component

## Separation of concerns

Separation of concerns means splitting the system so each part handles one kind of responsibility well.

Common boundaries include:

- presentation versus application logic
- business rules versus infrastructure
- read models versus write models
- domain logic versus cross-cutting infrastructure

Good separation makes change safer because one concern can evolve without surprising unrelated parts.

## Quality attributes

Architecture is usually driven by quality attributes, not only by features.

Important attributes include:

- maintainability
- scalability
- reliability
- performance
- security
- testability
- deployability

A system rarely maximizes all of them at once. Architecture is mostly about balancing them in context.

## Architectural patterns

An architectural pattern is a reusable high-level way to structure a system.

Examples:

- layered architecture
- event-driven architecture
- microservices
- hexagonal architecture

Patterns are not goals by themselves. They are tools used when their trade-offs match the problem.

## Layering, modularity, coupling, and cohesion

Layering organizes responsibilities into levels with constrained dependencies.

Modularity breaks the system into parts with clear interfaces and focused responsibilities.

Good architecture usually aims for:

- high cohesion inside a module
- low coupling between modules

High cohesion means a module has a focused job.

Low coupling means other modules can change without forcing large ripple effects.

## Cross-cutting concerns

Some concerns affect many parts of the system at once.

Examples:

- logging
- authentication and authorization
- auditing
- metrics and tracing
- caching policies
- transaction handling

Architectures should address these explicitly instead of scattering them across business code.

Typical techniques include middleware, decorators, filters, shared libraries, or platform capabilities.

## Interview reminders

- architecture is about high-level structure and trade-offs
- quality attributes often drive architecture more than features do
- separation of concerns reduces accidental complexity
- high cohesion and low coupling are still core architectural goals
- layered architecture helps organize dependencies, but it does not solve every problem alone
