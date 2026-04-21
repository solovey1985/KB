# Object-Oriented Programming Intermediate

This level moves from vocabulary into design decisions.

## Interface versus abstract class

An interface expresses a contract.

An abstract class can express a contract and also provide shared implementation.

```java
interface Payable {
    void pay();
}

abstract class Employee {
    protected String name;

    public Employee(String name) {
        this.name = name;
    }

    public void introduce() {
        System.out.println("I am " + name);
    }
}
```

Use an interface when many unrelated types should expose the same capability.

Use an abstract class when related types share common base behavior and state.

## Composition versus inheritance

Composition models a `has-a` relationship.

Inheritance models an `is-a` relationship.

```javascript
class Engine {
  start() {
    return 'Engine started';
  }
}

class Car {
  constructor(engine) {
    this.engine = engine;
  }

  drive() {
    return this.engine.start();
  }
}
```

Composition is often more flexible because collaborators can be swapped without changing the class hierarchy.

## Aggregation versus composition

Aggregation is a weaker whole-part relationship.

Composition is a stronger ownership relationship where the child is tightly bound to the parent lifecycle.

## SOLID introduction

Intermediate OOP interviews often move quickly into SOLID.

The most practical ideas:

- single responsibility
- open for extension, closed for modification
- subclasses should remain valid substitutes
- focused interfaces are better than giant ones
- depend on abstractions, not concrete details

## Interview reminders

- prefer composition when inheritance does not express a strong `is-a`
- distinguish interface from abstract class by contract-only versus shared base behavior
- describe SOLID through design problems, not slogans only
