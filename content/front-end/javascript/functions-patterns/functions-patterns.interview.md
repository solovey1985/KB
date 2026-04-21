---
title: JavaScript Functions and Patterns Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise common function and object-pattern questions.

Relevant concept maps:

- [Concept Map](functions-patterns.concept.md)

## Functions

```interview-question
What is a higher-order function in JavaScript?
---
answer:
A higher-order function is a function that takes another function as an argument, returns a function, or both.

This is a core part of JavaScript because callbacks, array methods, and reusable function factories all depend on it.
hints:
- Functions are first-class values.
- Another function is involved somehow.
- `map` is a common example.
```

Related concepts: [Higher-Order Function](functions-patterns.concept.md#higher-order-function), [First-Class Functions](functions-patterns.concept.md#first-class-functions)

```interview-question
Can functions be assigned to variables in JavaScript?
---
answer:
Yes. Functions are first-class values in JavaScript.

That means they can be stored in variables, passed to other functions, returned from functions, and used inside data structures.
hints:
- JavaScript treats functions like values.
- This enables callbacks and higher-order functions.
- Assignment is fully legal.
```

Related concepts: [First-Class Functions](functions-patterns.concept.md#first-class-functions)

## Practical Patterns

```interview-question
What are IIFEs and when were they useful?
---
answer:
IIFEs, or Immediately Invoked Function Expressions, are functions that run as soon as they are defined.

They were commonly used to create private scope and avoid polluting the global namespace before modern ES modules became the normal solution.
hints:
- The function runs immediately.
- Scope isolation is the main reason.
- Modules reduced their importance.
```

Related concepts: [IIFE](functions-patterns.concept.md#iife), [Modules](functions-patterns.concept.md#modules)

```interview-choice
Which array method is best for transforming every item into a new value?
---
options:
- `filter`
- `map`
- `find`
correct: 1
explanation:
`map` creates a new array by transforming each input value into a corresponding output value.
```

```interview-code
language: js
prompt: Complete the code so it returns only active user names.
starter:
const activeNames = users
  .filter(user => user.active)
  .
solution:
const activeNames = users
  .filter(user => user.active)
  .map(user => user.name);
checks:
- includes: map
- includes: user.name
```
