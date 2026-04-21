---
title: TypeScript Basics Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise TypeScript fundamentals that frontend interviews often expect alongside JavaScript.

Relevant concept maps:

- [TypeScript Concept Map](typescript-basics.concept.md)

## Core Ideas

```interview-question
What problem does TypeScript solve for JavaScript developers?
---
answer:
TypeScript adds static type information to JavaScript so many mistakes can be caught earlier during development instead of only at runtime.

It also improves editor tooling, autocomplete, refactoring confidence, and API clarity.
hints:
- Think earlier feedback.
- Tooling is part of the answer.
- It still builds on JavaScript rather than replacing it.
```

Related concepts: [TypeScript](typescript-basics.concept.md#typescript), [Static Typing](typescript-basics.concept.md#static-typing)

```interview-question
What is the difference between an `interface` and a `type` alias?
---
answer:
Both can describe object shapes, but `type` aliases are more general and can describe unions, intersections, primitives, and tuples.

Interfaces are especially associated with object contracts and can be extended in a more declaration-oriented way.

In practice, both are useful and teams often choose based on consistency and use case.
hints:
- One is more general.
- Both can describe object shapes.
- Unions are a major clue.
```

Related concepts: [interface](typescript-basics.concept.md#interface), [type Alias](typescript-basics.concept.md#type-alias)

```interview-choice
Which TypeScript feature is designed to let one function work across many value types while preserving type information?
---
options:
- `any`
- Generics
- `never`
correct: 1
explanation:
Generics let code stay reusable while still preserving strong type information.
```

```interview-code
language: ts
prompt: Complete the generic function so it returns the first item from the array.
starter:
function first<T>(items: T[]): T | undefined {
  return 
}
solution:
function first<T>(items: T[]): T | undefined {
  return items[0];
}
checks:
- includes: items[0]
```
