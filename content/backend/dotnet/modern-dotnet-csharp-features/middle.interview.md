---
title: Modern .NET and C# Features Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level modern .NET and C# feature trade-offs from the Web API interview question set.

Relevant concept maps:

- [Modern C# Language Features Concept Map](modern-csharp-language-features.concept.md)
- [Modern .NET Runtime Features Concept Map](modern-dotnet-runtime-features.concept.md)

## Primary Constructors

```interview-question
What are primary constructors in C# 12 and how do they change how you write services?
---
answer:
Primary constructors let you declare constructor parameters directly on the type declaration and use them throughout the type body.

They reduce boilerplate for dependency-injected services and DTO-like types.

They are most useful when you do not need complex constructor logic or multiple constructor overloads.
hints:
- The constructor parameters move to the class header.
- Boilerplate reduction is the main benefit.
- Not every type should use them.
```

Related concepts: [Primary Constructors](modern-csharp-language-features.concept.md#primary-constructors)

## Records

```interview-question
What are records in C# and when would you use them instead of classes for DTOs?
---
answer:
Records provide concise syntax and value-based equality, which makes them a natural fit for DTOs, events, and other data-carrier types.

They are especially useful when immutability and simple comparison semantics matter.

Regular classes still fit entities and behavior-heavy services better.
hints:
- Value equality is the key difference.
- DTOs are the common use case.
- Entities usually remain classes.
```

Related concepts: [Records](modern-csharp-language-features.concept.md#records), [Type Shape Choices](modern-csharp-language-features.concept.md#type-shape-choices)

## Pattern Matching

```interview-question
What is pattern matching in C# and how do you use it in API code?
---
answer:
Pattern matching lets you branch on structure, type, and values in a compact and expressive way.

In APIs, it is often used for result mapping, validation rules, and converting domain outcomes into HTTP responses.

It usually produces clearer code than long nested `if` chains.
hints:
- It is more than just `is null` checks.
- Switch expressions are a common form.
- Result-to-response mapping is a strong example.
```

Related concepts: [Pattern Matching](modern-csharp-language-features.concept.md#pattern-matching)

## Minimal APIs

```interview-question
What are minimal APIs and why would you choose them over controllers for a new project?
---
answer:
Minimal APIs define endpoints directly through route-mapping methods without the ceremony of controller classes.

They are explicit, lightweight, and fit well with modern ASP.NET Core patterns.

Controllers still make sense when a team wants the MVC structure or already has a controller-heavy codebase.
hints:
- Less ceremony is part of the appeal.
- Route methods define endpoints directly.
- Controllers are still valid in some contexts.
```

Related concepts: [Minimal APIs](modern-dotnet-runtime-features.concept.md#minimal-apis)

## TimeProvider

```interview-question
What is the `TimeProvider` abstraction and why should you use it?
---
answer:
`TimeProvider` abstracts access to the current time so time-dependent code can be tested deterministically.

It is better than scattering `DateTime.UtcNow` through the codebase because tests can supply a fake time source.

This improves reliability for scheduling, expiry, and other time-sensitive logic.
hints:
- It solves a testability problem.
- Think fake time in tests.
- Time is a dependency too.
```

Related concepts: [TimeProvider](modern-dotnet-runtime-features.concept.md#timeprovider)
