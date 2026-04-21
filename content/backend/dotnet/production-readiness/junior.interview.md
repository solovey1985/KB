---
title: Production Readiness Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level production readiness distinctions from the Web API interview question set.

Relevant concept maps:

- [Operations and Deployment Concept Map](production-operations-deployment.concept.md)
- [Observability and Runtime Concept Map](production-observability-runtime.concept.md)

## Entry Point

```interview-question
What is `Program.cs` in a .NET API and what does it configure?
---
answer:
`Program.cs` is the application entry point that sets up services, middleware, and endpoint mapping.

It usually has a builder phase for service registration and an app phase for middleware and routes.

Understanding `Program.cs` means understanding how the application starts and how requests will be processed.
hints:
- Think startup and composition.
- Services come before middleware.
- Routes are mapped near the end.
```

Related concepts: [Program Startup Composition](production-operations-deployment.concept.md#program-startup-composition)
