---
title: Object-Oriented Programming Advanced Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise advanced OOP design and architecture interview questions.

Relevant concept maps:

- [Concept Map](object-oriented-advanced.concept.md)

## Advanced Design

```interview-question
What is the diamond problem?
---
answer:
The diamond problem appears in multiple inheritance when one class inherits from two parents that both inherit from the same base class.

This can create ambiguity about which inherited implementation or state should be used.
hints:
- It comes from multiple inheritance.
- One shared ancestor causes the issue.
- Ambiguity is the key problem.
```

Related concepts: [Multiple Inheritance](object-oriented-advanced.concept.md#multiple-inheritance), [Diamond Problem](object-oriented-advanced.concept.md#diamond-problem)

```interview-question
Why can Singleton be a risky pattern?
---
answer:
Singleton can hide global state, increase coupling, and make tests harder because many parts of the system silently depend on the same shared instance.

It can be useful in narrow cases, but it is often chosen too quickly when dependency injection or explicit composition would be clearer.
hints:
- Global state is a warning sign.
- Testability gets harder.
- Hidden dependencies are part of the risk.
```

Related concepts: [Singleton](object-oriented-advanced.concept.md#singleton), [Circular Dependency](object-oriented-advanced.concept.md#circular-dependency)

```interview-question
How would you break a circular dependency in an object-oriented system?
---
answer:
Start by identifying why the two components know too much about each other.

Typical fixes include extracting a smaller shared abstraction, moving one responsibility outward into a coordinating service, or inverting one dependency so both sides depend on a contract instead of each other directly.

The goal is not only to remove the cycle, but to reveal a cleaner responsibility boundary.
hints:
- The cycle usually points to a design boundary problem.
- Shared abstractions often help.
- One class may be doing too much.
```

Related concepts: [Circular Dependency](object-oriented-advanced.concept.md#circular-dependency), [Legacy Refactoring](object-oriented-advanced.concept.md#legacy-refactoring)

```interview-choice
Which pattern is usually the best fit when one family of behavior should be swappable at runtime?
---
options:
- Strategy
- Singleton
- Builder
correct: 0
explanation:
Strategy is designed for choosing between interchangeable algorithms or behaviors behind a common interface.
```

```interview-code
language: js
prompt: Complete the checkout class so it delegates price calculation to the injected strategy.
starter:
class Checkout {
  constructor(strategy) {
    this.strategy = strategy;
  }

  total(amount) {
    
  }
}
solution:
class Checkout {
  constructor(strategy) {
    this.strategy = strategy;
  }

  total(amount) {
    return this.strategy.apply(amount);
  }
}
checks:
- includes: return this.strategy.apply(amount)
```
