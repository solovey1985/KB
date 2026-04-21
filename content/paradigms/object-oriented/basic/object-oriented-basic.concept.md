---
title: Object-Oriented Programming Basic Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core object-oriented concepts that most interview questions build on.

Study pages: [Section Index](index.md) | [Material Notes](object-oriented-basic.md) | [Interview Practice](object-oriented-basic.interview.md)

## Basic Map

```concept-card
id: object-oriented-programming
term: Object-Oriented Programming
children:
- class
- object
- encapsulation
- abstraction
- inheritance
- polymorphism
- constructor
- cohesion
- coupling
summary:
Object-oriented programming organizes code around objects that combine state and behavior.
details:
It is a design style focused on modularity, reuse, and clearer boundaries between parts of the system.
example:
A `BankAccount` object can store balance and expose operations like `deposit()` and `withdraw()`.
mnemonic:
State plus behavior in one place.
recall:
- What problem is OOP trying to solve?
- Why are objects more than just data containers?
```

```concept-card
id: class
term: Class
parents:
- object-oriented-programming
related:
- object
summary:
A class is a blueprint that defines the state and behavior of future objects.
details:
It describes what instances should contain and what operations they should support.
example:
`class BankAccount { constructor(owner) { this.owner = owner; } }`
mnemonic:
Class defines, object lives.
recall:
- Why is a class called a blueprint?
- What does a class define?
```

```concept-card
id: object
term: Object
parents:
- object-oriented-programming
related:
- class
summary:
An object is a concrete instance created from a class.
details:
Each object has its own current state even when multiple objects share the same class definition.
example:
`const account = new BankAccount('Mira');`
mnemonic:
One blueprint, many instances.
recall:
- What is the difference between a class and an object?
- Why can two objects of the same class have different state?
```

```concept-card
id: encapsulation
term: Encapsulation
children:
- access-control
summary:
Encapsulation bundles data and related behavior together while controlling outside access.
details:
It protects invariants and keeps classes responsible for managing their own state safely.
example:
Expose `setFuel()` instead of letting outside code write a negative fuel value directly.
mnemonic:
Keep the data close and the rules closer.
recall:
- Why does encapsulation improve safety?
- What kind of bugs can it prevent?
```

```concept-card
id: access-control
term: Access Control
parents:
- encapsulation
summary:
Access control limits who can read or modify members of a class.
details:
It supports information hiding by exposing only what the rest of the system actually needs.
example:
Private fields and public methods form a controlled boundary.
mnemonic:
Hide what should not be touched.
recall:
- Why is access control useful?
- How does it support encapsulation?
```

```concept-card
id: abstraction
term: Abstraction
summary:
Abstraction exposes essential behavior while hiding unnecessary internal detail.
details:
It helps consumers think in terms of capabilities rather than implementation steps.
example:
A `send()` method hides SMTP setup and formatting details behind a small interface.
mnemonic:
Show the what, hide the how.
recall:
- What is the difference between abstraction and encapsulation?
- Why does abstraction make APIs easier to use?
```

```concept-card
id: inheritance
term: Inheritance
related:
- polymorphism
summary:
Inheritance lets one class reuse and extend behavior from another class.
details:
It is strongest when there is a real `is-a` relationship, but it can become rigid if overused.
example:
`Dog` can extend `Animal` and override `speak()`.
mnemonic:
Reuse through a real is-a relationship.
recall:
- When is inheritance a good fit?
- Why can inheritance become a design problem if used carelessly?
```

```concept-card
id: polymorphism
term: Polymorphism
related:
- inheritance
summary:
Polymorphism allows different objects to respond differently to the same operation.
details:
It lets client code stay simple while each concrete type keeps its own implementation.
example:
`animals.forEach(animal => animal.speak())`
mnemonic:
Same call, different behavior.
recall:
- What does polymorphism make easier in client code?
- How is it related to overriding?
```

```concept-card
id: constructor
term: Constructor
parents:
- object-oriented-programming
summary:
A constructor initializes a new object so it starts in a valid state.
details:
It is where required fields and simple invariants are usually established.
example:
`constructor(owner, balance = 0) { this.owner = owner; this.balance = balance; }`
mnemonic:
Set the object up before it is used.
recall:
- Why do constructors matter for correctness?
- What should a good constructor avoid doing?
```

```concept-card
id: cohesion
term: Cohesion
related:
- coupling
summary:
Cohesion measures how strongly the data and behavior inside one class belong together.
details:
High cohesion means the class has one focused responsibility instead of many unrelated jobs.
example:
A `FileUtility` that reads files, clears caches, and writes to a database has weak cohesion.
mnemonic:
One class, one focused job.
recall:
- What does high cohesion feel like?
- Why is low cohesion hard to maintain?
```

```concept-card
id: coupling
term: Coupling
related:
- cohesion
summary:
Coupling measures how strongly one class depends on other classes.
details:
Low coupling is usually preferred because it makes systems easier to test, change, and reason about.
example:
A class that directly depends on many concrete collaborators is more tightly coupled.
mnemonic:
Fewer tight dependencies, easier change.
recall:
- Why is loose coupling valuable?
- How do cohesion and coupling work together in good design?
```
