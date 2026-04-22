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

## Services, Forms, and Tooling

```interview-question
How do you create a service in Angular and why would you use one?
---
answer:
A service is typically a class marked with `@Injectable` and provided through Angular's dependency injection system.

You use a service to keep shared logic, data access, or cross-cutting behavior out of components so the app stays easier to test and maintain.
hints:
- Think `@Injectable` plus DI.
- Shared logic is the key reason.
- Components should not own every responsibility.
```

Related concepts: [Angular Service](angular-intermediate.concept.md#angular-service), [Dependency Injection](angular-intermediate.concept.md#dependency-injection)

```interview-question
What is a module in Angular, and what is its purpose today?
---
answer:
An NgModule is the older Angular mechanism for grouping declarations, imports, and providers.

It is still relevant in existing applications and some libraries, but modern Angular often prefers standalone components and provider-based bootstrapping instead of putting everything behind root and feature modules.
hints:
- Answer both the historical role and the modern shift.
- Grouping declarations was the old model.
- Standalone components change the default approach.
```

```interview-question
How do you handle form submissions in Angular?
---
answer:
Angular usually handles form submission through template-driven or reactive forms, then triggers submission logic from the form submit event such as `(ngSubmit)`.

The component reads validated form values and passes them to a service or HTTP layer instead of mixing server logic directly into the template.
hints:
- Mention template-driven or reactive forms.
- Submission is event-driven.
- Validation and server calls are usually separate concerns.
```

```interview-question
What is Angular CLI and what can it be used for?
---
answer:
Angular CLI is the official command-line tool for creating, running, generating, testing, and building Angular projects.

It standardizes project structure and automates common tasks such as scaffolding components, services, and configuration updates.
hints:
- Think project automation.
- Generation and build tasks are common examples.
- It helps keep project setup consistent.
```

```interview-question
How do you make HTTP requests in Angular using `HttpClient`?
---
answer:
You provide Angular's HTTP support, inject `HttpClient` into a service or component, and call methods such as `get`, `post`, `put`, or `delete`.

Those methods return observables, so Angular code usually subscribes in the right layer or composes them with RxJS operators before updating UI state.
hints:
- Start by injecting `HttpClient`.
- The API returns observables.
- Services are the usual home for API calls.
```

```interview-code
language: ts
prompt: Complete the standalone bootstrap so `HttpClient` can be injected anywhere in the app.
starter:
bootstrapApplication(AppComponent, {
  providers: [
    
  ],
});
solution:
bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
  ],
});
checks:
- includes: provideHttpClient()
```
