---
title: Object-Oriented Programming Advanced Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes advanced OOP concepts around patterns, anti-patterns, and long-term design trade-offs.

Study pages: [Section Index](index.md) | [Material Notes](object-oriented-advanced.md) | [Interview Practice](object-oriented-advanced.interview.md)

## Advanced Map

```concept-card
id: design-pattern
term: Design Pattern
children:
- strategy-pattern
- factory-method
- builder-pattern
- adapter-pattern
- decorator-pattern
- observer-pattern
- command-pattern
summary:
A design pattern is a reusable solution shape for a recurring design problem.
details:
Patterns should be used when they clarify structure and reduce design friction, not simply to make code look sophisticated.
example:
Use Strategy when behavior should vary behind one interface instead of spreading many conditionals across the system.
mnemonic:
Pattern for the problem, not pattern for the resume.
recall:
- Why are patterns tools rather than goals?
- What risk appears when patterns are applied mechanically?
```

```concept-card
id: strategy-pattern
term: Strategy Pattern
parents:
- design-pattern
summary:
The Strategy pattern encapsulates interchangeable algorithms or behaviors behind a common interface.
details:
It is useful when the behavior should vary without changing the calling code.
example:
A checkout flow can apply different discount strategies through a shared `apply(total)` method.
mnemonic:
Swap behavior, keep the caller stable.
recall:
- When is Strategy a strong fit?
- Why is it better than many conditional branches in some cases?
```

```concept-card
id: factory-method
term: Factory Method
parents:
- design-pattern
summary:
Factory Method delegates object creation to a dedicated method or collaborator.
details:
It is useful when the exact concrete type should not be hardcoded at every call site.
example:
A parser factory can choose which parser implementation to build based on input format.
mnemonic:
Separate creation from use.
recall:
- What problem does Factory Method solve?
- Why can centralizing creation reduce coupling?
```

```concept-card
id: builder-pattern
term: Builder Pattern
parents:
- design-pattern
summary:
Builder helps construct complex objects step by step.
details:
It is useful when constructor calls would otherwise become long, fragile, or unclear.
example:
A `ReportBuilder` can assemble title, filters, sections, and export options incrementally.
mnemonic:
Build step by step, not all at once.
recall:
- When is Builder more readable than a large constructor?
- What kind of object often suggests a builder?
```

```concept-card
id: adapter-pattern
term: Adapter Pattern
parents:
- design-pattern
summary:
Adapter makes one interface look like another so existing code can work with an incompatible component.
details:
It is especially useful when integrating third-party or legacy code into a cleaner design.
example:
Wrap a legacy payment API so the rest of the system sees a clean `PaymentGateway` interface.
mnemonic:
Translate one interface into another.
recall:
- What integration problem does Adapter solve?
- Why is it common in legacy systems?
```

```concept-card
id: decorator-pattern
term: Decorator Pattern
parents:
- design-pattern
summary:
Decorator adds responsibilities to an object by wrapping it instead of subclassing it.
details:
It is useful for layering behaviors such as logging, caching, or validation without changing the wrapped object's core role.
example:
A logging decorator can wrap a repository and log every call without changing repository consumers.
mnemonic:
Wrap behavior around the object.
recall:
- Why can Decorator be more flexible than inheritance?
- What kinds of cross-cutting behavior fit it well?
```

```concept-card
id: observer-pattern
term: Observer Pattern
parents:
- design-pattern
summary:
Observer allows one object to notify interested dependents when its state changes.
details:
It is common in UI systems, event systems, and any publish-subscribe style interaction.
example:
A stock-price publisher can notify many display components when the price changes.
mnemonic:
One source, many listeners.
recall:
- What relationship does Observer model?
- Why is it useful in event-driven systems?
```

```concept-card
id: command-pattern
term: Command Pattern
parents:
- design-pattern
summary:
Command turns a request into an object so it can be queued, logged, retried, or undone more easily.
details:
It separates the invoker from the receiver and adds flexibility around execution handling.
example:
A `CreateInvoiceCommand` object can be queued and retried by background infrastructure.
mnemonic:
Turn an action into an object.
recall:
- Why is Command useful beyond simple method calls?
- What extra behaviors become possible when requests are objects?
```

```concept-card
id: multiple-inheritance
term: Multiple Inheritance
related:
- diamond-problem
summary:
Multiple inheritance allows a type to inherit from more than one parent type.
details:
It can increase reuse, but it also introduces ambiguity and complexity in method and state resolution.
example:
One class inheriting two parents that both define the same method can create ambiguity.
mnemonic:
More parents, more power, more ambiguity.
recall:
- What benefit does multiple inheritance promise?
- What complexity does it introduce?
```

```concept-card
id: diamond-problem
term: Diamond Problem
parents:
- multiple-inheritance
summary:
The diamond problem occurs when two parent classes share a common ancestor and a child inherits from both.
details:
This can create ambiguity about which inherited implementation or state path the child should use.
example:
If both parent classes inherit from the same base `Animal`, a child inheriting both may face duplicate `Animal` behavior.
mnemonic:
Two paths down from one ancestor create ambiguity.
recall:
- Why is the diamond problem hard to resolve cleanly?
- Which language features are often introduced to manage it?
```

```concept-card
id: immutability
term: Immutability
summary:
Immutability means an object does not change after it is created.
details:
Immutable objects are often easier to reason about, safer to share, and friendlier to concurrent or distributed logic.
example:
A money value object can return a new value when changed instead of mutating the existing instance.
mnemonic:
If it cannot change, it cannot drift unexpectedly.
recall:
- Why can immutability improve correctness?
- Which kinds of objects benefit most from being immutable?
```

```concept-card
id: singleton
term: Singleton
summary:
Singleton ensures there is only one shared instance of a type.
details:
It can be useful in narrow cases, but it often acts like hidden global state and makes testing harder.
example:
A globally reachable config singleton can quietly spread hidden dependencies across the whole codebase.
mnemonic:
One instance, many hidden consequences.
recall:
- Why is Singleton often criticized?
- When might it still be acceptable?
```

```concept-card
id: circular-dependency
term: Circular Dependency
summary:
A circular dependency happens when two or more components depend on each other directly or indirectly.
details:
It often signals unclear responsibility boundaries and makes testing, initialization, and maintenance harder.
example:
`OrderService` depends on `InvoiceService`, while `InvoiceService` also depends on `OrderService`.
mnemonic:
If both sides need each other, the boundary is probably wrong.
recall:
- Why are circular dependencies harmful?
- What design move often helps break them?
```

```concept-card
id: anti-pattern
term: Anti-Pattern
children:
- legacy-refactoring
summary:
An anti-pattern is a recurring design choice that looks helpful but usually creates long-term problems.
details:
Examples include god objects, needless abstraction, fragile base classes, and overgrown inheritance trees.
example:
A `SystemManager` class that owns logging, database access, email, billing, and caching is a typical god object.
mnemonic:
Looks convenient now, costs you later.
recall:
- What makes something an anti-pattern rather than just an unusual pattern?
- Which OO anti-patterns show up most often in legacy systems?
```

```concept-card
id: legacy-refactoring
term: Legacy Refactoring
parents:
- anti-pattern
summary:
Legacy refactoring is the process of improving design in old code while preserving behavior.
details:
It usually starts by adding tests, creating seams, reducing coupling, and extracting responsibilities safely.
example:
Before splitting a god object, write tests around its current behavior so you can refactor without guessing.
mnemonic:
Protect behavior first, improve structure second.
recall:
- Why do tests matter before deep refactoring?
- What small steps are safer than a full rewrite?
```
