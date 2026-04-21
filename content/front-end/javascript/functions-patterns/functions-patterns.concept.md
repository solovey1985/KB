---
title: JavaScript Functions and Patterns Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes reusable language patterns around functions, arrays, and modules.

Study pages: [Section Index](index.md) | [Material Notes](functions-patterns.md) | [Interview Practice](functions-patterns.interview.md)

## Pattern Map

```concept-card
id: first-class-functions
term: First-Class Functions
children:
- higher-order-function
- function-forms
summary:
First-class functions means JavaScript treats functions as values.
details:
Functions can be assigned, passed around, stored, and returned just like other values.
example:
`const run = fn => fn();`
mnemonic:
Functions are values, not special exceptions.
recall:
- What does first-class mean for functions?
- Which JavaScript features rely on this behavior?
```

```concept-card
id: higher-order-function
term: Higher-Order Function
parents:
- first-class-functions
summary:
A higher-order function accepts a function, returns a function, or both.
details:
They power array methods, callbacks, and function factories.
example:
`const doubleAll = values => values.map(v => v * 2);`
mnemonic:
Function in, function out, or both.
recall:
- What makes a function higher-order?
- Why are array helpers good examples?
```

```concept-card
id: function-forms
term: Function Forms
summary:
JavaScript has function declarations, function expressions, and arrow functions.
details:
The forms differ in syntax, hoisting behavior, and `this` behavior.
example:
`function greet() {}` and `const greet = () => {}`
mnemonic:
Same goal, different shape.
recall:
- What are the common function forms?
- Why are arrow functions not just shorter syntax?
```

```concept-card
id: array-transformations
term: Array Transformations
summary:
Array transformation methods such as `map`, `filter`, and `reduce` support expressive data processing.
details:
They help turn loops into readable declarative steps, especially in UI code.
example:
`users.filter(u => u.active).map(u => u.name)`
mnemonic:
Filter what stays, map what changes.
recall:
- When do you use `map` versus `filter`?
- Why are these methods common in frontend code?
```

```concept-card
id: modules
term: Modules
summary:
Modules split JavaScript code into files with explicit imports and exports.
details:
They improve structure, reuse, and dependency clarity in modern codebases.
example:
`export function sum(a, b) { return a + b; }`
mnemonic:
Export intentionally, import explicitly.
recall:
- Why did modules reduce the need for older global patterns?
- What problem do modules solve in app codebases?
```

```concept-card
id: iife
term: IIFE
aliases:
- Immediately Invoked Function Expression
related:
- modules
summary:
An IIFE is a function expression that runs immediately after it is created.
details:
It was a common pattern for scope isolation before ES modules became standard.
example:
`(() => { const hidden = 1; })();`
mnemonic:
Define and run right away.
recall:
- Why were IIFEs useful historically?
- Why are modules often preferred now?
```
