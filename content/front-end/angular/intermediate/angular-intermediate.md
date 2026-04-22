# Angular Intermediate

This level covers the features that let a small Angular component tree become a real application.

## Dependency injection

Angular's dependency injection system lets components and services receive dependencies without constructing them directly.

```ts
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  template: `<button (click)="openSettings()">Settings</button>`,
})
export class NavbarComponent {
  private router = inject(Router);

  openSettings() {
    this.router.navigate(['/settings']);
  }
}
```

DI improves:

- reuse
- testability
- separation of concerns

Services are the most common DI example. They usually hold shared business logic, API calls, or app-wide behavior that should not be duplicated across components.

## Routing

Routing lets Angular change visible content inside a SPA without a full page reload.

```ts
import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path: 'settings',
    loadComponent: () =>
      import('./settings.component').then(m => m.SettingsComponent),
  },
];
```

## Standalone bootstrap

Modern Angular apps bootstrap from a root component and providers instead of a root NgModule.

```ts
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app.component';
import { routes } from './app.routes';

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes)],
});
```

## Forms and HTTP

Angular provides standardized systems for forms and HTTP communication.

At this level, the important idea is choosing the right abstraction and keeping state flow clear between UI, validation, and server communication.

Form submission is usually handled through `(ngSubmit)` or reactive form handlers, with validation happening before data is passed into a service layer.

`HttpClient` is Angular's standard API for HTTP calls. In modern standalone applications, it is commonly enabled through `provideHttpClient()` and then injected where needed.

## CLI and project workflow

Angular CLI is the standard tool for:

- generating components, services, and other scaffolding
- running the development server
- building production bundles
- running tests

It matters in interviews because it represents the normal Angular workflow, not just a convenience command.

## Interview reminders

- explain DI as provide and inject, not only constructor magic
- describe routing in SPA terms
- mention standalone bootstrapping as the modern default
- keep forms and HTTP discussion practical, not only API-list based
