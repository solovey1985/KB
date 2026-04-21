---
title: ASP.NET Core Internals and Middleware Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level ASP.NET Core internals and middleware distinctions from the Web API interview question set.

Relevant concept maps:

- [Pipeline and DI Concept Map](aspnet-core-pipeline-di.concept.md)
- [Runtime and Production Concept Map](aspnet-core-runtime-production.concept.md)

## Dependency Injection Basics

```interview-question
What is dependency injection, and why does ASP.NET Core use it by default?
---
answer:
Dependency injection means a class receives the services it depends on from the outside instead of creating them directly with `new`.

ASP.NET Core uses it by default because it improves testability, centralizes lifetime management, reduces coupling, and makes implementations easy to swap through configuration.

The built-in container also manages service lifetimes such as singleton, scoped, and transient.
hints:
- Think about constructor parameters instead of manual object creation.
- The benefit is not only cleaner code but also easier testing and lifetime control.
- ASP.NET Core builds most of its infrastructure around the container.
```

Related concepts: [Dependency Injection](aspnet-core-pipeline-di.concept.md#dependency-injection), [Service Lifetime](aspnet-core-pipeline-di.concept.md#service-lifetime)

```interview-choice
Which service lifetime is usually the correct choice for `DbContext` in a Web API?
---
options:
- Singleton
- Scoped
- Transient always, no exceptions
correct: 1
explanation:
`DbContext` is typically scoped so each HTTP request gets its own instance and unit of work boundary.
```

## Endpoint Mapping

```interview-question
What is the difference between `app.Map`, `app.MapGet`, `app.MapPost`, and `app.MapControllers` in ASP.NET Core?
---
answer:
`MapGet`, `MapPost`, and the related methods create minimal API endpoints for specific HTTP methods.

`MapControllers` discovers and maps controller-based endpoints.

`Map` is the generic path-mapping method and is less specific for normal API work. In practice, `MapGet` and `MapPost` are the explicit minimal API choices, while `MapControllers` enables the MVC controller model.
hints:
- Some methods are specific to an HTTP verb.
- One method activates controller discovery.
- Think minimal APIs versus controllers.
```

Related concepts: [Minimal API Mapping](aspnet-core-pipeline-di.concept.md#minimal-api-mapping), [Controllers Mapping](aspnet-core-pipeline-di.concept.md#controllers-mapping)

```interview-choice
Which API maps controller classes decorated with routing attributes?
---
options:
- `app.MapControllers()`
- `app.Map()`
- `app.MapGroup()`
correct: 0
explanation:
`app.MapControllers()` enables controller-based endpoints and lets ASP.NET Core discover them from the application.
```

```interview-code
language: cs
prompt: Complete the code so it defines a minimal API GET endpoint that returns `Hello`.
starter:
app.
solution:
app.MapGet("/api/products", () => "Hello");
checks:
- includes: MapGet
- includes: /api/products
- includes: Hello
```
