---
title: Angular Intermediate Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes Angular application structure around DI, routing, and standalone composition.

Study pages: [Section Index](index.md) | [Material Notes](angular-intermediate.md) | [Interview Practice](angular-intermediate.interview.md)

## Intermediate Map

```concept-card
id: dependency-injection
term: Dependency Injection
children:
- angular-service
summary:
Dependency injection is Angular's system for providing and consuming shared values across the app.
details:
It supports reuse, testability, and clean separation of responsibilities.
example:
`private router = inject(Router);`
mnemonic:
Provide once, inject where needed.
recall:
- What does DI provide in Angular beyond convenience?
- Why is it useful for testing?
```

```concept-card
id: angular-service
term: Angular Service
parents:
- dependency-injection
summary:
An Angular service is a class, often decorated with `@Injectable`, that provides reusable logic or shared state.
details:
Services commonly handle data access, auth, logging, or shared app behavior.
example:
An `AuthService` can manage token state and permission checks for multiple components.
mnemonic:
Shared logic belongs in services.
recall:
- What kinds of responsibilities fit services well?
- Why is a service better than duplicating logic across components?
```

```concept-card
id: angular-router
term: Angular Router
children:
- router-outlet
- standalone-routing
summary:
The Angular Router controls SPA navigation by mapping URLs to views.
details:
It lets an application navigate without full page reloads while still keeping URLs meaningful and shareable.
example:
The `/settings` route can display `SettingsComponent` inside the router outlet.
mnemonic:
URL changes, app view changes, page stays alive.
recall:
- Why is routing essential in SPAs?
- What are the main pieces of Angular routing?
```

```concept-card
id: router-outlet
term: Router Outlet
parents:
- angular-router
summary:
`router-outlet` is the placeholder where the active route's component is rendered.
details:
It is the bridge between route configuration and visible UI.
example:
`<router-outlet />`
mnemonic:
Routes choose, outlet shows.
recall:
- What does the router outlet do?
- Why is it central to route rendering?
```

```concept-card
id: standalone-routing
term: Standalone Routing
parents:
- angular-router
related:
- bootstrapapplication
summary:
Standalone routing defines route trees without relying on an NgModule-based router setup.
details:
It fits the modern standalone Angular model and works naturally with `loadComponent`.
example:
`{ path: 'settings', loadComponent: () => import('./settings.component').then(m => m.SettingsComponent) }`
mnemonic:
Route directly to components, not only modules.
recall:
- How does standalone routing differ from older NgModule routing setup?
- Why is `loadComponent` important here?
```

```concept-card
id: bootstrapapplication
term: bootstrapApplication
summary:
`bootstrapApplication` starts an Angular app from a standalone root component plus providers.
details:
It replaces the old bootstrap NgModule pattern in modern Angular applications.
example:
`bootstrapApplication(AppComponent, { providers: [provideRouter(routes)] })`
mnemonic:
Boot the app from the component, not the module.
recall:
- What does `bootstrapApplication` replace?
- Why does it fit the standalone-first model?
```
