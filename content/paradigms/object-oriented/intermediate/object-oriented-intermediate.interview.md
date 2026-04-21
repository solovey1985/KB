---
title: Object-Oriented Programming Intermediate Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise intermediate OOP design and relationship questions.

Relevant concept maps:

- [Concept Map](object-oriented-intermediate.concept.md)

## Relationships and Abstractions

```interview-question
What is the difference between an interface and an abstract class?
---
answer:
An interface defines a contract that implementing types must satisfy.

An abstract class can also define a contract, but it may additionally provide shared state and partial implementation for related subclasses.

Interfaces are best when unrelated types share a capability. Abstract classes are best when related types share common base behavior.
hints:
- Contract-only versus shared implementation is the key distinction.
- Interfaces are often broader and more capability-focused.
- Abstract classes usually imply a family relationship.
```

Related concepts: [Interface](object-oriented-intermediate.concept.md#interface), [Abstract Class](object-oriented-intermediate.concept.md#abstract-class)

```interview-question
Compare inheritance, mixin, and composition.
---
answer:
Inheritance models an `is-a` relationship and reuses behavior through a class hierarchy.

A mixin adds reusable behavior to a type without representing a full standalone base type.

Composition builds an object from collaborating parts and models a `has-a` relationship. It is often preferred because it is more flexible and less fragile than deep inheritance.
hints:
- `is-a` and `has-a` are central.
- Mixins are about reusable behavior.
- Flexibility is the reason composition is often preferred.
```

Related concepts: [Inheritance Versus Composition](object-oriented-intermediate.concept.md#inheritance-versus-composition), [Mixin](object-oriented-intermediate.concept.md#mixin), [Aggregation and Composition](object-oriented-intermediate.concept.md#aggregation-and-composition)

```interview-question
What is the difference between aggregation and composition?
---
answer:
Aggregation is a weaker whole-part relationship where the child can exist independently of the parent.

Composition is a stronger relationship where the part is tightly owned by the whole and its lifecycle is closely bound to it.
hints:
- Both are whole-part relationships.
- One is weaker, one is stronger.
- Lifecycle ownership is the key clue.
```

Related concepts: [Aggregation and Composition](object-oriented-intermediate.concept.md#aggregation-and-composition)

## SOLID

```interview-question
What is the Open/Closed Principle?
---
answer:
The Open/Closed Principle says software entities should be open for extension but closed for modification.

That means new behavior should often be added by introducing new types or strategies instead of repeatedly editing stable core code.
hints:
- Extension without repeated core edits.
- New behavior should not always require rewriting old classes.
- Think pluggable behavior.
```

Related concepts: [Open-Closed Principle](object-oriented-intermediate.concept.md#open-closed-principle)

```interview-choice
Which design usually shows better object-oriented flexibility?
---
options:
- A deep inheritance hierarchy used for every variation
- A small focused class that delegates behavior through composition
- One giant base class that every type extends
correct: 1
explanation:
Composition usually makes behavior easier to swap and test without making the hierarchy brittle.
```

```interview-code
language: js
prompt: Complete the class so it uses composition by delegating to the provided engine.
starter:
class Car {
  constructor(engine) {
    this.engine = engine;
  }

  drive() {
    
  }
}
solution:
class Car {
  constructor(engine) {
    this.engine = engine;
  }

  drive() {
    return this.engine.start();
  }
}
checks:
- includes: this.engine.start()
```
