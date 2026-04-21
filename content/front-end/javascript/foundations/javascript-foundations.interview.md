---
title: JavaScript Foundations Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the JavaScript language basics that appear in many frontend interviews.

Relevant concept maps:

- [Concept Map](javascript-foundations.concept.md)

## Values and Equality

```interview-question
What data types are present in JavaScript?
---
answer:
JavaScript has primitive types such as `string`, `number`, `boolean`, `null`, `undefined`, `bigint`, and `symbol`.

It also has objects, including arrays and functions, which behave as reference values.
hints:
- Start with primitives.
- Functions are still objects.
- Arrays are objects too.
```

Related concepts: [JavaScript Values](javascript-foundations.concept.md#javascript-values), [Primitive Types](javascript-foundations.concept.md#primitive-types), [Reference Types](javascript-foundations.concept.md#reference-types)

```interview-question
What is the difference between `null` and `undefined`?
---
answer:
`undefined` usually means a value has not been assigned or a property does not exist.

`null` is usually an intentional empty value set by the developer or returned by an API to mean "no object" or "no value here".
hints:
- One is usually implicit.
- One is usually explicit.
- Both represent absence, but not in the same way.
```

Related concepts: [null Versus undefined](javascript-foundations.concept.md#null-versus-undefined)

```interview-choice
Which comparison is `true`?
---
options:
- `'5' === 5`
- `'5' == 5`
- `null === undefined`
correct: 1
explanation:
`==` performs type coercion, so `'5' == 5` is `true`, while strict equality requires both value and type to match.
```

## Scope and Execution

```interview-question
What is hoisting in JavaScript?
---
answer:
Hoisting is the compile-time behavior where declarations are processed before execution of the surrounding scope.

With `var`, the name is hoisted and initialized to `undefined`. With `let` and `const`, the name is hoisted too, but it stays uninitialized until its declaration runs, which creates the temporal dead zone.
hints:
- Declarations are known before execution reaches them.
- `var` and `let` do not behave the same way.
- The temporal dead zone is the key modern distinction.
```

Related concepts: [Hoisting](javascript-foundations.concept.md#hoisting), [Temporal Dead Zone](javascript-foundations.concept.md#temporal-dead-zone)

```interview-question
What is scope in JavaScript?
---
answer:
Scope defines where a variable can be accessed.

JavaScript uses global scope, function scope, and block scope. `var` is function-scoped, while `let` and `const` are block-scoped.
hints:
- Think visibility of variables.
- Function scope and block scope are different.
- Declarations affect scope behavior.
```

Related concepts: [Scope](javascript-foundations.concept.md#scope), [var Versus let and const](javascript-foundations.concept.md#var-versus-let-and-const)

## Closures and this

```interview-question
Describe a closure in JavaScript.
---
answer:
A closure is a function that keeps access to variables from the lexical scope where it was created.

That access continues even after the outer function has finished executing, which makes closures useful for factories, data hiding, and callbacks.
hints:
- It is about lexical scope.
- The outer function may already be finished.
- State can still be remembered.
```

Related concepts: [Closure](javascript-foundations.concept.md#closure), [Lexical Scope](javascript-foundations.concept.md#lexical-scope)

```interview-question
What is the `this` keyword and how does its context change?
---
answer:
`this` refers to the current execution context for a function.

For regular functions, `this` depends on how the function is called. For arrow functions, `this` comes from the surrounding lexical scope and does not change based on invocation.
hints:
- Invocation matters for regular functions.
- Arrow functions are different.
- Methods, standalone calls, and constructors all behave differently.
```

Related concepts: [this Binding](javascript-foundations.concept.md#this-binding), [Arrow Function this](javascript-foundations.concept.md#arrow-function-this)

```interview-code
language: js
prompt: Complete the function so it returns a closure that increments an internal counter.
starter:
function createCounter() {
  let count = 0;

  return function () {
    
  };
}
solution:
function createCounter() {
  let count = 0;

  return function () {
    count += 1;
    return count;
  };
}
checks:
- includes: count += 1
- includes: return count
```
