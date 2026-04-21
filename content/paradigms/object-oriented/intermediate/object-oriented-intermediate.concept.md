---
title: Object-Oriented Programming Intermediate Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes intermediate OOP design ideas around relationships, abstraction choices, and SOLID principles.

Study pages: [Section Index](index.md) | [Material Notes](object-oriented-intermediate.md) | [Interview Practice](object-oriented-intermediate.interview.md)

## Intermediate Map

```concept-card
id: interface
term: Interface
related:
- abstract-class
summary:
An interface defines a contract that implementing types must follow.
details:
It focuses on required behavior rather than shared implementation details.
example:
A `Payable` interface can be implemented by `Invoice`, `Salary`, and `Subscription` types even if they share no common base state.
mnemonic:
Interface defines what must be done.
recall:
- What does an interface guarantee?
- Why is it useful across unrelated types?
```

```concept-card
id: abstract-class
term: Abstract Class
related:
- interface
summary:
An abstract class provides a base type that cannot be instantiated directly and may include shared implementation.
details:
It is useful when related subclasses share both common rules and shared default behavior.
example:
An abstract `Employee` base class can store `name` and define shared introduction logic.
mnemonic:
Abstract class is base behavior with room for specialization.
recall:
- How is an abstract class different from an interface?
- When is shared implementation valuable?
```

```concept-card
id: inheritance-versus-composition
term: Inheritance Versus Composition
children:
- mixin
- aggregation-and-composition
summary:
Inheritance reuses behavior through hierarchy, while composition builds behavior from collaborating parts.
details:
Composition is often preferred because it is usually more flexible, easier to test, and less fragile than deep inheritance.
example:
A `Car` that owns an `Engine` collaborator is usually more adaptable than a hierarchy of `EngineCar`, `ElectricCar`, and `HybridCar` base variations.
mnemonic:
Prefer parts over deep trees unless is-a is truly strong.
recall:
- Why is composition often preferred?
- When is inheritance still the right choice?
```

```concept-card
id: mixin
term: Mixin
parents:
- inheritance-versus-composition
summary:
A mixin is a reusable bundle of behavior added to a type without forming a full parent-child hierarchy.
details:
It can be useful for cross-cutting capabilities, but it also risks unclear method origins if overused.
example:
A logging mixin can add `log()` behavior to many unrelated classes.
mnemonic:
Reuse behavior without forcing a family tree.
recall:
- What does a mixin solve well?
- Why can mixins become confusing?
```

```concept-card
id: aggregation-and-composition
term: Aggregation and Composition
parents:
- inheritance-versus-composition
summary:
Aggregation is a weak ownership relationship, while composition is a strong lifecycle-bound ownership relationship.
details:
Composition implies the whole is responsible for the part more strongly than aggregation does.
example:
A `Team` aggregating `Player` objects is weaker than a `House` composing a `Room` layout in a design model.
mnemonic:
Aggregation shares parts, composition owns them deeply.
recall:
- What makes composition stronger than aggregation?
- Why does lifecycle matter in this distinction?
```

```concept-card
id: open-closed-principle
term: Open-Closed Principle
summary:
The Open-Closed Principle says a design should be open to extension but closed to repeated modification.
details:
It encourages adding new behavior through new implementations, strategies, or subclasses instead of rewriting stable core logic again and again.
example:
Adding a new discount strategy class is often better than adding another `if` branch in one giant pricing class.
mnemonic:
Extend behavior, avoid rewriting the center.
recall:
- What kind of design change does OCP try to reduce?
- Why does pluggable behavior help?
```

```concept-card
id: liskov-substitution-principle
term: Liskov Substitution Principle
summary:
The Liskov Substitution Principle says a subtype should be usable wherever its base type is expected without breaking correctness.
details:
It is about behavioral compatibility, not just inheritance syntax.
example:
If a subclass throws on normal base-class usage assumptions, it may violate substitutability.
mnemonic:
If it inherits, it must still fit.
recall:
- What does substitutability mean in practice?
- Why is inheritance alone not enough to satisfy LSP?
```

```concept-card
id: interface-segregation-principle
term: Interface Segregation Principle
summary:
The Interface Segregation Principle says clients should not depend on methods they do not use.
details:
Smaller focused interfaces reduce unnecessary coupling and awkward implementations.
example:
Split a large `Machine` interface into `Printable`, `Scannable`, and `Faxable` capabilities.
mnemonic:
Small interfaces, fewer forced dependencies.
recall:
- Why are giant interfaces harmful?
- How does ISP help maintainability?
```

```concept-card
id: dependency-inversion-principle
term: Dependency Inversion Principle
summary:
The Dependency Inversion Principle says high-level policies should depend on abstractions rather than concrete details.
details:
It supports loose coupling and easier testing by reducing direct dependence on specific implementations.
example:
A payment workflow depending on a `PaymentGateway` interface instead of a specific Stripe class follows DIP better.
mnemonic:
Depend on contracts, not concrete details.
recall:
- What does DIP invert?
- Why does it help testing and extensibility?
```
