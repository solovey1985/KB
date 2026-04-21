# Object-Oriented Programming Basics

Object-oriented programming, or OOP, organizes code around objects that combine state and behavior.

## Why OOP exists

OOP tries to make software easier to model, extend, and reason about by grouping related data and operations together.

Useful goals include:

- modularity
- reuse
- clearer boundaries
- safer state management

## Class versus object

A class is a blueprint.

An object is a concrete instance created from that blueprint.

```javascript
class BankAccount {
  constructor(owner, balance = 0) {
    this.owner = owner;
    this.balance = balance;
  }

  deposit(amount) {
    this.balance += amount;
  }
}

const account = new BankAccount('Mira', 100);
account.deposit(50);
```

In this example:

- `BankAccount` is the class
- `account` is the object

## Encapsulation

Encapsulation means keeping state and the operations that manage it together.

It also means exposing controlled access instead of letting outside code change everything directly.

```javascript
class Temperature {
  #celsius;

  constructor(value) {
    this.#celsius = value;
  }

  getCelsius() {
    return this.#celsius;
  }

  setCelsius(value) {
    if (value < -273.15) throw new Error('Below absolute zero');
    this.#celsius = value;
  }
}
```

## Abstraction

Abstraction means exposing what matters while hiding unnecessary internal detail.

Consumers of a class should care about what they can do, not about every internal step.

```javascript
class EmailService {
  sendWelcomeEmail(user) {
    // internal formatting and sending details stay hidden
    return `Welcome email sent to ${user.email}`;
  }
}
```

## Inheritance

Inheritance allows one class to reuse and extend behavior from another class.

```javascript
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
```

Inheritance is useful when there is a real `is-a` relationship.

## Polymorphism

Polymorphism means different objects can respond to the same message in different ways.

```javascript
const animals = [new Dog(), new Animal()];
console.log(animals.map(animal => animal.speak()));
```

The call site stays simple while each object preserves its own behavior.

## Constructors

A constructor prepares a new object so it starts in a valid state.

Good constructors:

- initialize required fields
- enforce basic invariants
- avoid doing too much unrelated work

## Cohesion and coupling

High cohesion means a class has one focused job.

Low coupling means it does not depend too heavily on many other classes.

Good OOP design aims for:

- high cohesion
- low coupling

## Interview reminders

- class is the blueprint, object is the instance
- encapsulation protects and organizes state
- abstraction hides internal detail
- inheritance is not the same as composition
- polymorphism is about shared interfaces with different implementations
