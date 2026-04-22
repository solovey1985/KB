---
title: Angular Intermediate Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise Angular application-structure questions.

Relevant concept maps:

- [Concept Map](angular-intermediate.concept.md)

## DI and Routing

```interview-question
What is dependency injection in Angular and why is it useful?
---
answer:
Dependency injection in Angular is the system that provides values such as services, configuration, or framework helpers to components and other classes.

It is useful because it improves maintainability, reuse, and testability by keeping classes from constructing all their own dependencies directly.
hints:
- Think provide and inject.
- Services are the common example.
- Testability is one major benefit.
```

Related concepts: [Dependency Injection](angular-intermediate.concept.md#dependency-injection), [Angular Service](angular-intermediate.concept.md#angular-service)

```interview-question
Why is routing necessary in a single-page application?
---
answer:
Routing is necessary because SPAs do not reload the whole document for each navigation.

The router maps the URL to the correct component view and updates what the user sees without a full page refresh.
hints:
- Think SPA navigation.
- URL still matters.
- No full page reload is the key difference.
```

Related concepts: [Angular Router](angular-intermediate.concept.md#angular-router), [Router Outlet](angular-intermediate.concept.md#router-outlet)

```interview-question
Why is `bootstrapApplication` a major shift from the older Angular bootstrap model?
---
answer:
`bootstrapApplication` starts the app from a standalone root component and a providers list instead of a bootstrap NgModule.

That makes startup composition more direct and fits Angular's modern standalone-first approach.
hints:
- The root module is no longer central.
- The app starts from a component.
- Providers are still important.
```

Related concepts: [bootstrapApplication](angular-intermediate.concept.md#bootstrapapplication), [Standalone Routing](angular-intermediate.concept.md#standalone-routing)

```interview-choice
Which API is used for lazy-loading a standalone route component?
---
options:
- `loadChildren`
- `loadComponent`
- `loadStandaloneModule`
correct: 1
explanation:
`loadComponent` is the standalone-friendly route API for lazy-loading one component directly.
```

```interview-code
language: ts
prompt: Complete the bootstrap so the application uses standalone routing.
starter:
bootstrapApplication(AppComponent, {
  providers: [
    
  ],
});
solution:
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
  ],
});
checks:
- includes: provideRouter(routes)
```
