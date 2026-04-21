---
title: Modern .NET and C# Features Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level modern .NET and C# feature scenarios from the Web API interview question set.

Relevant concept maps:

- [Modern C# Language Features Concept Map](modern-csharp-language-features.concept.md)
- [Modern .NET Runtime Features Concept Map](modern-dotnet-runtime-features.concept.md)

## Span and Memory

```interview-question
What are `Span<T>` and `Memory<T>`? When would you use them in an API?
---
answer:
`Span<T>` and `Memory<T>` let you work with slices of memory without copying data unnecessarily.

They matter in APIs when profiling shows allocation pressure in parsing, serialization-adjacent work, or high-throughput data handling.

They are powerful, but they should be used where they solve a measured problem rather than as a default style.
hints:
- Allocation reduction is the main reason.
- `Span<T>` is more restrictive than `Memory<T>`.
- Use profiling before reaching for them.
```

Related concepts: [Span and Memory](modern-csharp-language-features.concept.md#span-and-memory)

## Source Generation

```interview-question
What is source generation in .NET and how do you use it for better API performance?
---
answer:
Source generation emits code at build time so the application can avoid some runtime reflection and setup overhead.

In APIs, it is commonly used for JSON serialization, logging, and regex scenarios.

Its benefits include performance, trimming support, and stronger AOT friendliness.
hints:
- Build-time code replaces runtime work.
- JSON is a major use case.
- AOT compatibility is part of the value.
```

Related concepts: [Source Generation](modern-dotnet-runtime-features.concept.md#source-generation)

## New Platform Features

```interview-question
What is new in .NET 10 that affects how you build Web APIs?
---
answer:
Key themes include stronger built-in OpenAPI support, more mature caching and rate limiting, continued minimal API improvements, and EF Core alignment with the .NET release cadence.

The important answer is not memorizing a changelog but recognizing which platform features reduce custom infrastructure or third-party dependencies.

Senior-level value comes from knowing which new features are worth adopting in a real production codebase.
hints:
- Think practical platform impact.
- Built-in replacements for older libraries matter.
- Adoption trade-offs still matter.
```

Related concepts: [Modern .NET Platform Features](modern-dotnet-runtime-features.concept.md#modern-dotnet-platform-features)

## field Keyword

```interview-question
What is the `field` keyword in C# 14 and how does it change property definitions?
---
answer:
The `field` keyword gives property accessors access to the compiler-generated backing field of an auto-property.

It reduces the need to declare a separate private field when you want validation or lightweight logic inside a getter or setter.

Its main benefit is less boilerplate for property logic, not a change in overall design principles.
hints:
- It affects auto-properties.
- Backing field access is the key idea.
- Boilerplate reduction is the main benefit.
```

Related concepts: [field Keyword](modern-csharp-language-features.concept.md#field-keyword)
