---
title: Modern .NET and C# Features Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level modern .NET and C# feature distinctions from the Web API interview question set.

Relevant concept maps:

- [Modern C# Language Features Concept Map](modern-csharp-language-features.concept.md)
- [Modern .NET Runtime Features Concept Map](modern-dotnet-runtime-features.concept.md)

## Collection Expressions

```interview-question
What are collection expressions in C# 12 and how do they simplify code?
---
answer:
Collection expressions use a unified bracket syntax such as `[1, 2, 3]` to create arrays, lists, spans, and some other collection-like targets.

They reduce syntax noise and make combining collections easier, especially with spread elements.

The main value is readability and consistency across collection types.
hints:
- Think one syntax for many collection targets.
- The brackets are the new part.
- Simpler initialization is the main benefit.
```

Related concepts: [Collection Expressions](modern-csharp-language-features.concept.md#collection-expressions)

```interview-choice
Which example uses collection expression syntax?
---
options:
- `List<int> values = [1, 2, 3];`
- `List<int> values = new List<int>() -> 1, 2, 3;`
- `List<int> values = (1, 2, 3);`
correct: 0
explanation:
`[1, 2, 3]` is the new collection expression syntax in modern C#.
```
