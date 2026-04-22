---
title: TypeScript Advanced Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise advanced TypeScript type-system questions.

Relevant concept maps:

- [Concept Map](typescript-advanced.concept.md)

## Generics and Type Design

```interview-question
What problem do generics solve in TypeScript?
---
answer:
Generics let code work across many value types while preserving meaningful type information and relationships.

They are better than `any` because they keep safety and inference instead of discarding type information.
hints:
- Reusable code is part of the answer.
- `any` is the contrast case.
- Input and output relationships matter.
```

Related concepts: [Generics](typescript-advanced.concept.md#generics), [Generic Constraints](typescript-advanced.concept.md#generic-constraints)

```interview-question
What is a generic constraint and why would you use one?
---
answer:
A generic constraint limits what kinds of types a generic parameter is allowed to be.

You use it when generic code needs some guaranteed capability, such as a `.length` property or a valid key of another type.
hints:
- It narrows a generic parameter.
- Required capabilities are the reason.
- `.length` is a common example.
```

Related concepts: [Generic Constraints](typescript-advanced.concept.md#generic-constraints)

```interview-question
What are utility types and why are they useful?
---
answer:
Utility types are built-in generic helpers that transform existing types into new ones.

They reduce repetition and make common transformations such as partial updates, readonly views, or selected subsets much easier to express.
hints:
- Built-in helpers are the key idea.
- They transform existing types.
- `Partial` and `Pick` are strong examples.
```

Related concepts: [Utility Types](typescript-advanced.concept.md#utility-types)

```interview-choice
Which utility type is best for making every property on a type optional?
---
options:
- `Pick<T, K>`
- `Partial<T>`
- `Readonly<T>`
correct: 1
explanation:
`Partial<T>` transforms every property of `T` into an optional property.
```

```interview-code
language: ts
prompt: Complete the generic constraint so the function only accepts values with a `length` property.
starter:
interface Lengthwise {
  length: number;
}

function logLength<T >(value: T): T {
  console.log(value.length);
  return value;
}
solution:
interface Lengthwise {
  length: number;
}

function logLength<T extends Lengthwise>(value: T): T {
  console.log(value.length);
  return value;
}
checks:
- includes: extends Lengthwise
```
