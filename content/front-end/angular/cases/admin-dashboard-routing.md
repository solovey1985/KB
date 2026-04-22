# Admin Dashboard with Standalone Routing

## Situation

You are building an internal admin dashboard with:

- a login page
- a dashboard home
- lazy-loaded feature pages such as users and billing

The app should avoid unnecessary upfront bundle size and should not depend on legacy NgModules.

## Why this case matters

This case exercises:

- standalone bootstrapping
- route configuration
- lazy loading with `loadComponent`
- dependency injection in navigation-aware components

## Example

```ts
import { Routes } from '@angular/router';
import { DashboardHomeComponent } from './dashboard-home.component';

export const routes: Routes = [
  { path: '', component: DashboardHomeComponent },
  {
    path: 'users',
    loadComponent: () =>
      import('./users/users-page.component').then(m => m.UsersPageComponent),
  },
  {
    path: 'billing',
    loadComponent: () =>
      import('./billing/billing-page.component').then(m => m.BillingPageComponent),
  },
];
```

## Practical design notes

- use standalone components for route pages
- lazy load real feature areas, not everything blindly
- keep route definitions close to the feature boundaries when scale grows

## Related concepts

- [Angular Router](../intermediate/angular-intermediate.concept.md#angular-router)
- [Standalone Routing](../intermediate/angular-intermediate.concept.md#standalone-routing)
- [bootstrapApplication](../intermediate/angular-intermediate.concept.md#bootstrapapplication)
