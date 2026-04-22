---
title: TypeScript Intermediate Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise practical TypeScript design questions.

Relevant concept maps:

- [Concept Map](typescript-intermediate.concept.md)

## Functions and Objects

```interview-question
How do you define and use a function in TypeScript?
---
answer:
You can define function parameters and return types explicitly, while also using optional parameters, default parameters, rest parameters, and overload signatures where needed.

TypeScript uses those types to check function usage and improve tooling.
hints:
- Parameters and return types are the foundation.
- Rest and optional parameters are common extensions.
- Overloads may be relevant for more complex APIs.
```

Related concepts: [Typed Functions](typescript-intermediate.concept.md#typed-functions), [Function Overloads](typescript-intermediate.concept.md#function-overloads)

```interview-question
How are classes in TypeScript different from plain ES classes?
---
answer:
TypeScript classes build on ES classes by adding type annotations, access modifiers, readonly members, parameter properties, abstract classes, and stronger compile-time checks.

At runtime they are still JavaScript classes, but at development time they offer much stronger structure.
hints:
- Runtime stays JavaScript.
- Compile-time structure is richer.
- Access modifiers and abstract classes are key clues.
```

Related concepts: [TypeScript Classes](typescript-intermediate.concept.md#typescript-classes), [Access Modifiers](typescript-intermediate.concept.md#access-modifiers), [Abstract Class](typescript-intermediate.concept.md#abstract-class)

```interview-question
What are access modifiers in TypeScript?
---
answer:
Access modifiers such as `public`, `private`, and `protected` control where class members are intended to be used.

They improve encapsulation and API clarity, though they mainly exist as compile-time constraints in typical TypeScript output.
hints:
- They control visibility.
- Encapsulation is part of the purpose.
- Compile-time intent matters.
```

Related concepts: [Access Modifiers](typescript-intermediate.concept.md#access-modifiers)

```interview-question
How do you compile TypeScript files into JavaScript?
---
answer:
TypeScript is compiled with `tsc` or equivalent build tooling configured through `tsconfig.json`.

The compiler checks types and emits JavaScript according to target and module settings.
hints:
- `tsc` is the core compiler.
- `tsconfig.json` shapes project behavior.
- Output is still JavaScript.
```

Related concepts: [TypeScript Compiler](typescript-intermediate.concept.md#typescript-compiler), [tsconfig](typescript-intermediate.concept.md#tsconfig)

```interview-choice
Which TypeScript feature lets you declare a class property directly from a constructor parameter?
---
options:
- Parameter properties
- Template literal types
- Declaration merging
correct: 0
explanation:
Parameter properties let a constructor parameter also declare and initialize a class member in one place.
```

```interview-code
language: ts
prompt: Complete the function so it accepts any number of numeric arguments and returns their sum.
starter:
function sumAll(...values: number[]): number {
  return 
}
solution:
function sumAll(...values: number[]): number {
  return values.reduce((acc, value) => acc + value, 0);
}
checks:
- includes: reduce
- includes: acc + value
```
