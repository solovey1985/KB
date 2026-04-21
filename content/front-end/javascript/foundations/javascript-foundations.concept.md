---
title: JavaScript Foundations Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core JavaScript language concepts that everything else in this section builds on.

Study pages: [Section Index](index.md) | [Material Notes](javascript-foundations.md) | [Interview Practice](javascript-foundations.interview.md)

## Core Map

```concept-card
id: javascript-values
term: JavaScript Values
children:
- primitive-types
- reference-types
- null-versus-undefined
- equality
summary:
JavaScript values are either primitive values or reference values.
details:
Knowing the value model explains copying behavior, equality surprises, and how objects differ from strings or numbers.
example:
`const name = 'JS'; const user = { name: 'JS' };`
mnemonic:
Primitive copies value, objects copy reference.
recall:
- What is the key difference between primitive and reference values?
- Why do arrays and objects behave differently from strings and numbers when copied?
```

```concept-card
id: primitive-types
term: Primitive Types
parents:
- javascript-values
summary:
Primitive types are basic immutable values like strings, numbers, booleans, `null`, `undefined`, `bigint`, and `symbol`.
details:
They are copied by value and do not expose mutable internal structure in the same way objects do.
example:
`let a = 5; let b = a; b = 10;`
mnemonic:
Primitive means small, direct, and copied by value.
recall:
- Which JavaScript values are primitives?
- What copying behavior do primitives have?
```

```concept-card
id: reference-types
term: Reference Types
parents:
- javascript-values
summary:
Reference types include objects, arrays, and functions.
details:
Variables hold references to these structures, so assigning one variable to another points both names at the same underlying object.
example:
`const a = { done: false }; const b = a;`
mnemonic:
Two names can point at one object.
recall:
- Why can changing one object reference affect another variable?
- Are functions primitives or objects?
```

```concept-card
id: null-versus-undefined
term: null Versus undefined
parents:
- javascript-values
summary:
`undefined` usually means a value is missing implicitly, while `null` usually represents an intentional empty value.
details:
They both express absence, but they often communicate different intent in code and APIs.
example:
`let x; // undefined` and `let user = null;`
mnemonic:
Undefined is missing, null is chosen empty.
recall:
- When is `undefined` produced automatically?
- Why would a developer choose `null` intentionally?
```

```concept-card
id: equality
term: Equality
related:
- type-coercion
summary:
JavaScript equality can be strict or coercive.
details:
`===` compares both type and value, while `==` allows coercion before comparison.
example:
`'5' == 5` is true, but `'5' === 5` is false.
mnemonic:
Double equals bends, triple equals stands firm.
recall:
- Why is `===` the safer default?
- What problem does `==` introduce?
```

```concept-card
id: type-coercion
term: Type Coercion
parents:
- equality
summary:
Type coercion is JavaScript's automatic conversion of one value type into another during operations or comparisons.
details:
It can be convenient, but it also creates surprising results if the developer is not explicit.
example:
`'5' - 2` becomes `3`, while `'5' + 2` becomes `'52'`.
mnemonic:
JavaScript may convert for you, but not always how you expect.
recall:
- What is implicit coercion?
- Why can arithmetic and comparison behave differently?
```

```concept-card
id: scope
term: Scope
children:
- var-versus-let-and-const
- hoisting
- temporal-dead-zone
summary:
Scope determines where variables can be read or written.
details:
JavaScript uses global, function, and block scope depending on the declaration form and location.
example:
`if (true) { const secret = 1; }`
mnemonic:
Scope answers where a name is visible.
recall:
- What types of scope are common in JavaScript?
- Why do declaration keywords matter for scope?
```

```concept-card
id: var-versus-let-and-const
term: var Versus let and const
parents:
- scope
summary:
`var` is function-scoped, while `let` and `const` are block-scoped.
details:
`let` allows reassignment, `const` does not, and both avoid several confusing `var` behaviors.
example:
`for (let i = 0; i < 3; i++) {}`
mnemonic:
Use `let` and `const` unless old code forces `var`.
recall:
- Why is `var` more error-prone?
- What is the practical difference between `let` and `const`?
```

```concept-card
id: hoisting
term: Hoisting
parents:
- scope
summary:
Hoisting is the compile-time behavior where declarations are processed before runtime reaches them.
details:
`var` declarations become available as `undefined`, while `let` and `const` remain uninitialized until execution reaches their declaration.
example:
`console.log(a); var a = 5;`
mnemonic:
The name exists early, but the value may not.
recall:
- How does hoisting differ for `var` versus `let` and `const`?
- Why does hoisting create confusing bugs?
```

```concept-card
id: temporal-dead-zone
term: Temporal Dead Zone
parents:
- scope
related:
- hoisting
summary:
The temporal dead zone is the period where a `let` or `const` binding exists but cannot be accessed yet.
details:
Accessing the variable before its declaration line runs throws a `ReferenceError`.
example:
`console.log(x); let x = 1;`
mnemonic:
Declared in scope, but not ready yet.
recall:
- Why does `let` throw instead of returning `undefined` before initialization?
- What problem does the temporal dead zone help catch?
```

```concept-card
id: lexical-scope
term: Lexical Scope
summary:
Lexical scope means variable access is determined by where code is written, not where it is called from.
details:
This rule is what makes closures work predictably in JavaScript.
example:
An inner function can read `count` from the outer function where it was declared.
mnemonic:
Written location decides visibility.
recall:
- Why is lexical scope important to closures?
- How is lexical scope different from dynamic scoping?
```

```concept-card
id: closure
term: Closure
parents:
- lexical-scope
summary:
A closure is a function that keeps access to variables from its lexical scope after the outer function has finished.
details:
Closures are used for private state, factories, and callbacks that need remembered context.
example:
`function createCounter() { let count = 0; return () => ++count; }`
mnemonic:
The function remembers where it came from.
recall:
- What does a closure retain?
- Why are closures useful for stateful factories?
```

```concept-card
id: this-binding
term: this Binding
children:
- arrow-function-this
summary:
`this` in JavaScript depends on execution context for regular functions.
details:
Method calls, standalone calls, constructor calls, and explicit binding with `call`, `apply`, or `bind` all affect its value.
example:
`user.print()` gives `this === user`, while a detached function call usually does not.
mnemonic:
Regular function `this` follows the call site.
recall:
- What decides `this` for a regular function?
- Why do detached methods often surprise developers?
```

```concept-card
id: arrow-function-this
term: Arrow Function this
parents:
- this-binding
summary:
Arrow functions do not create their own `this` and instead use the surrounding lexical one.
details:
That makes them useful for callbacks, but not ideal for object methods that should bind to the object itself.
example:
`const fn = () => this;`
mnemonic:
Arrow `this` is borrowed, not bound.
recall:
- Why do arrow functions behave differently from methods?
- When is lexical `this` helpful?
```
