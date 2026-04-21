# Object-Oriented Programming Advanced

Advanced OOP is less about memorizing textbook definitions and more about using abstraction carefully.

## Patterns as tools

Design patterns are reusable solutions to recurring design problems, not goals by themselves.

Some high-value patterns to know:

- Strategy
- Factory Method
- Builder
- Adapter
- Decorator
- Observer
- Command

### Strategy example

```javascript
class PercentageDiscount {
  apply(total) {
    return total * 0.9;
  }
}

class FixedDiscount {
  apply(total) {
    return total - 20;
  }
}

class Checkout {
  constructor(discountStrategy) {
    this.discountStrategy = discountStrategy;
  }

  total(amount) {
    return this.discountStrategy.apply(amount);
  }
}
```

## Anti-patterns

Advanced interviews often ask indirectly about design failures.

Watch for:

- god objects
- deep inheritance trees
- fragile base classes
- circular dependencies
- needless abstractions

## Immutability in OO design

Objects do not always need to be mutable.

Immutable value objects often make systems safer and easier to reason about.

## Singleton trade-offs

Singleton can look convenient, but it often hides global state and makes tests harder.

It should be treated as a trade-off, not as a default design answer.

## Legacy refactoring

In large object-oriented systems, the hard part is often improving structure without breaking behavior.

Useful steps:

- add tests around current behavior
- extract seams and abstractions carefully
- break circular dependencies gradually
- improve cohesion before introducing more patterns

## Interview reminders

- patterns are tools, not trophies
- senior answers should mention trade-offs
- prefer explaining why a pattern helps over reciting pattern names
