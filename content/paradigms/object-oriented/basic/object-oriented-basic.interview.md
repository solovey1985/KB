---
title: Object-Oriented Programming Basic Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise core OOP interview questions.

Relevant concept maps:

- [Concept Map](object-oriented-basic.concept.md)

## OOP Foundations

```interview-question
What is Object-Oriented Programming?
---
answer:
Object-oriented programming is a paradigm that organizes code around objects that combine data and the behavior that works on that data.

It aims to improve modularity, reuse, extensibility, and the clarity of code structure.
hints:
- Think objects, not just functions.
- State and behavior live together.
- Modularity is one of the main goals.
```

Related concepts: [Object-Oriented Programming](object-oriented-basic.concept.md#object-oriented-programming)

```interview-question
What is the difference between a class and an object?
---
answer:
A class is a blueprint or definition for a kind of thing.

An object is a concrete instance created from that blueprint with its own current state.
hints:
- One defines, one exists.
- One is a template, the other is a created instance.
- Think blueprint versus built house.
```

Related concepts: [Class](object-oriented-basic.concept.md#class), [Object](object-oriented-basic.concept.md#object)

```interview-question
What is encapsulation?
---
answer:
Encapsulation is the practice of bundling data and related behavior together while restricting uncontrolled access to internal state.

It helps protect invariants and keeps the public API of a class focused.
hints:
- State and methods belong together.
- Access should be controlled.
- Protecting internal rules is part of the goal.
```

Related concepts: [Encapsulation](object-oriented-basic.concept.md#encapsulation), [Access Control](object-oriented-basic.concept.md#access-control)

```interview-question
What is abstraction?
---
answer:
Abstraction means exposing the essential behavior of a type while hiding unnecessary implementation detail.

It helps consumers think in terms of capabilities instead of internal mechanics.
hints:
- Show the important parts.
- Hide the internal details.
- Simpler interface is the idea.
```

Related concepts: [Abstraction](object-oriented-basic.concept.md#abstraction)

```interview-choice
Which statement best describes polymorphism?
---
options:
- One class can have only one method implementation
- Different objects can respond differently to the same method call
- Private fields are visible to all subclasses
correct: 1
explanation:
Polymorphism allows objects that share a common interface or base type to provide different concrete behavior for the same operation.
```

```interview-code
language: js
prompt: Complete the subclass so it overrides `speak()` with dog-specific behavior.
starter:
class Animal {
  speak() {
    return 'Generic sound';
  }
}

class Dog extends Animal {
  speak() {
    
  }
}
solution:
class Animal {
  speak() {
    return 'Generic sound';
  }
}

class Dog extends Animal {
  speak() {
    return 'Woof';
  }
}
checks:
- includes: return 'Woof'
```
