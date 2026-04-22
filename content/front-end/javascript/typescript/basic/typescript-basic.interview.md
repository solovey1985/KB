---
title: TypeScript Basic Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise TypeScript fundamentals.

Relevant concept maps:

- [Concept Map](typescript-basic.concept.md)

## Core Ideas

```interview-question
What is TypeScript and how does it differ from JavaScript?
---
answer:
TypeScript is a statically typed superset of JavaScript.

It adds a type system, compile-time checking, and stronger tooling while still compiling down to JavaScript for runtime execution.
hints:
- It builds on JavaScript rather than replacing it.
- Static typing is the main distinction.
- It still runs as JavaScript after compilation.
```

Related concepts: [TypeScript](typescript-basic.concept.md#typescript), [Static Typing](typescript-basic.concept.md#static-typing)

```interview-question
What are some basic types available in TypeScript?
---
answer:
Common TypeScript basic types include `string`, `number`, `boolean`, arrays, tuples, enums, unions, `void`, `null`, `undefined`, `never`, and object types.

In day-to-day frontend work, the most common are primitive types, arrays, object types, unions, and function-related types.
hints:
- Start with primitives.
- Arrays and unions are common too.
- `never` and `void` are worth mentioning.
```

Related concepts: [Everyday Types](typescript-basic.concept.md#everyday-types), [Enum](typescript-basic.concept.md#enum)

```interview-question
What is type inference in TypeScript?
---
answer:
Type inference means TypeScript can often determine a variable's type from its assigned value or usage context without an explicit annotation.

This keeps code concise while still preserving strong type information in many common cases.
hints:
- Types do not always need to be written manually.
- Initialization often gives the compiler enough information.
- Conciseness is part of the benefit.
```

Related concepts: [Type Inference](typescript-basic.concept.md#type-inference)

```interview-question
What is the difference between an interface and a type alias?
---
answer:
Both can describe object shapes, but `type` aliases are more general because they can describe unions, intersections, tuples, and primitives.

Interfaces are especially natural for object contracts and shape-based APIs.
hints:
- Both can model objects.
- One is more general.
- Unions are a key clue.
```

Related concepts: [interface](typescript-basic.concept.md#interface), [type Alias](typescript-basic.concept.md#type-alias)

```interview-choice
Which keyword is usually the best modern default for a variable that should not be reassigned?
---
options:
- `var`
- `let`
- `const`
correct: 2
explanation:
`const` is the usual modern default because it makes non-reassignment explicit and reduces accidental mutation of bindings.
```

```interview-code
language: ts
prompt: Complete the type alias so `Status` only allows four string states.
starter:
type Status = 
solution:
type Status = 'idle' | 'loading' | 'success' | 'error';
checks:
- includes: idle
- includes: loading
- includes: success
- includes: error
```
