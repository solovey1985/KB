# Design Patterns and Principles

Patterns and principles give architecture repeatable structure, but they only help when their trade-offs match the problem.

## Patterns versus principles

Patterns are common structural solutions.

Principles are decision rules that help you judge whether a solution is healthy.

Examples of principles:

- single responsibility
- dependency inversion
- explicit boundaries
- favor composition over inheritance when appropriate

Examples of patterns:

- MVC
- publish-subscribe
- repository
- adapter
- decorator

## MVC

Model-View-Controller splits responsibilities between data and behavior, rendering, and input coordination.

MVC is useful when UI interactions need a clearer separation between presentation and application behavior.

It becomes weaker when teams force everything through controllers and end up with bloated middle layers.

## Publish-subscribe

Publish-subscribe allows components to emit events without knowing every consumer ahead of time.

Benefits:

- looser runtime coupling
- easier extension with new consumers
- better fit for integration and notifications

Trade-offs:

- harder tracing
- eventual consistency concerns
- duplicate handling and idempotency needs

## Monolith versus microservices

A monolith keeps major capabilities in one deployable unit.

Microservices split capabilities into independently deployable services with explicit contracts.

Monolith strengths:

- simpler local development
- easier transactions
- lower operational overhead

Microservice strengths:

- stronger independent deployment
- service-level scaling
- clearer ownership boundaries in some organizations

The right choice depends on complexity, team structure, and operational maturity.

## SOLID in architecture

SOLID is often taught at class level, but the ideas matter at architectural scale too.

Examples:

- single responsibility encourages focused modules
- open/closed encourages extension without broad rewrites
- dependency inversion encourages relying on abstractions rather than infrastructure details

These ideas support cleaner module boundaries and testable systems.

## Common pattern trade-offs

Singleton:

- useful for one logical shared instance
- risky when it hides global mutable state or harms testability

Repository:

- useful when a domain-facing abstraction over persistence adds clarity
- less useful when it only wraps an ORM with no real benefit

Adapter:

- useful when integrating incompatible interfaces

Decorator:

- useful for adding behavior such as caching, logging, or authorization without changing the wrapped core type

Command:

- useful when requests need to be queued, retried, logged, or handled uniformly

## SOA and service boundaries

Service-Oriented Architecture structures systems as interoperable services, often with shared integration standards and enterprise governance.

SOA and microservices both use services, but SOA often emphasizes centralized integration and broader enterprise reuse, while microservices emphasize smaller autonomous teams and independently deployable services.

## Interview reminders

- patterns are reusable structures, not mandatory defaults
- principles help evaluate patterns in context
- MVC and publish-subscribe solve different kinds of separation problems
- monolith versus microservices is mostly an organizational and operational trade-off
- a pattern becomes harmful when it hides complexity instead of managing it
