---
title: TypeScript Basics Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes TypeScript fundamentals that commonly appear next to JavaScript interview questions.

Study pages: [Section Index](index.md) | [TypeScript Notes](typescript-basics.md) | [TypeScript Interview Practice](typescript-basics.interview.md)

## TypeScript Map

```concept-card
id: typescript
term: TypeScript
children:
- static-typing
- interface
- type-alias
- generics
summary:
TypeScript is a typed superset of JavaScript that adds static type checking and stronger tooling.
details:
It improves editor support, maintainability, and early feedback while still compiling to JavaScript.
example:
`function greet(name: string): string { return 'Hi ' + name; }`
mnemonic:
JavaScript plus type-aware feedback.
recall:
- What does TypeScript add to JavaScript?
- Why is TypeScript valuable even though JavaScript already works without it?
```

```concept-card
id: static-typing
term: Static Typing
parents:
- typescript
summary:
Static typing checks value shapes and usage before runtime.
details:
It reduces many categories of runtime mistakes by shifting them into development-time feedback.
example:
Passing a number to a function expecting a string can be caught before the app runs.
mnemonic:
Catch more before the browser runs it.
recall:
- What kind of mistakes does static typing catch early?
- Why is that useful in larger frontend codebases?
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
It is commonly used for API responses, component props, and domain models.
example:
`interface User { id: number; name: string; }`
mnemonic:
Interface means shape contract.
recall:
- When is an interface a natural choice?
- Why is it often used for object-like data models?
```

```concept-card
id: type-alias
term: type Alias
parents:
- typescript
related:
- interface
summary:
A type alias names a type expression and can describe unions, intersections, tuples, and more.
details:
It is more general than an interface because it can describe more than object shapes alone.
example:
`type Status = 'idle' | 'loading' | 'success' | 'error';`
mnemonic:
Type alias names any useful type shape.
recall:
- Why is a type alias more general than an interface?
- What kind of types are especially natural for aliases?
```

```concept-card
id: generics
term: Generics
parents:
- typescript
summary:
Generics let code work across many value types while preserving type information.
details:
They are useful for reusable helpers, collections, and APIs where input and output types are related.
example:
`function first<T>(items: T[]): T | undefined { return items[0]; }`
mnemonic:
Reusable code without giving up type precision.
recall:
- What problem do generics solve?
- Why are generics better than `any` for reusable utilities?
```
