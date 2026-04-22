---
title: TypeScript Basic Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the TypeScript fundamentals that most frontend developers need first.

Study pages: [Section Index](index.md) | [Material Notes](typescript-basic.md) | [Interview Practice](typescript-basic.interview.md)

## Basic Map

```concept-card
id: typescript
term: TypeScript
children:
- static-typing
- everyday-types
- type-inference
- interface
- type-alias
- enum
- strictness
summary:
TypeScript is a typed superset of JavaScript that adds static checking and stronger tooling.
details:
It helps developers catch many mistakes before runtime and scale JavaScript codebases more safely.
example:
`function greet(name: string): string { return 'Hello ' + name; }`
mnemonic:
JavaScript with a safety and tooling layer.
recall:
- What does TypeScript add to JavaScript?
- Why is it useful in large frontend codebases?
```

```concept-card
id: static-typing
term: Static Typing
parents:
- typescript
summary:
Static typing checks how values are used before the code runs.
details:
It moves many mistakes from runtime into development-time feedback.
example:
Passing a number into a function expecting a string can be caught at compile time.
mnemonic:
Catch misuse before execution.
recall:
- What kinds of errors does static typing catch early?
- Why is this valuable in collaborative projects?
```

```concept-card
id: everyday-types
term: Everyday Types
parents:
- typescript
summary:
Everyday types are the common value types used constantly in application code.
details:
These include primitives, arrays, object shapes, unions, and function-related types.
example:
`string`, `number`, `boolean`, `string[]`, and `'idle' | 'loading'`.
mnemonic:
Use the simplest useful type first.
recall:
- Which types appear most often in application code?
- Why are unions so common in frontend state modeling?
```

```concept-card
id: type-inference
term: Type Inference
parents:
- typescript
summary:
Type inference means TypeScript can determine many types automatically from values and context.
details:
This reduces annotation noise while preserving useful type information.
example:
`let count = 10` is inferred as `number`.
mnemonic:
Let the compiler infer the obvious.
recall:
- When is inference enough?
- When are explicit types still worth writing?
```

```concept-card
id: interface
term: interface
parents:
- typescript
related:
- type-alias
summary:
An interface describes the expected shape of an object-like value.
details:
It is a common tool for modeling APIs, props, and structured domain objects.
example:
`interface User { id: number; name: string; }`
mnemonic:
Interface means shape contract.
recall:
- When is an interface a natural fit?
- Why is it commonly used for object shapes?
```

```concept-card
id: type-alias
term: type Alias
parents:
- typescript
related:
- interface
summary:
A type alias names a type expression such as a union, tuple, primitive, or object shape.
details:
It is more general than an interface and often useful for state unions and reusable type expressions.
example:
`type Status = 'idle' | 'loading' | 'success' | 'error';`
mnemonic:
Type alias names any useful type expression.
recall:
- Why is a type alias more general than an interface?
- What kinds of frontend state models fit aliases well?
```

```concept-card
id: enum
term: Enum
parents:
- typescript
summary:
An enum provides a named set of related constants.
details:
Enums can be useful, though many teams prefer unions for lighter-weight frontend state modeling.
example:
`enum RequestState { Idle, Loading, Success, Error }`
mnemonic:
One named set of related values.
recall:
- When can enums be useful?
- Why do some teams still prefer union literals?
```

```concept-card
id: strictness
term: Strictness
parents:
- typescript
summary:
Strictness is the set of compiler settings that control how aggressively TypeScript checks code.
details:
Options like `strict`, `noImplicitAny`, and `strictNullChecks` improve safety but may require more explicit typing discipline.
example:
`"strict": true` in `tsconfig.json` enables the main strict checking mode.
mnemonic:
More strictness, more early feedback.
recall:
- What practical benefit does strict mode provide?
- Which strictness flags matter most in frontend codebases?
```
