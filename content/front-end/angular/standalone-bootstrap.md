# Standalone Bootstrap and Routing in Angular 17

Angular 17 cements standalone components as the primary way to assemble applications. While NgModules remain supported, the recommended runtime is now built around `bootstrapApplication`, standalone route definitions, and feature composition via component imports.

## Prior Approach (v16 and below)

```ts
// main.ts
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';

platformBrowserDynamic().bootstrapModule(AppModule);
```

```ts
// app.module.ts
@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, RouterModule.forRoot(routes)],
  bootstrap: [AppComponent]
})
export class AppModule {}
```

- Routing was configured via `RouterModule.forRoot` inside an NgModule.
- Feature modules (`SharedModule`, `AdminModule`, etc.) were the primary composition tool.

## Angular 17 Standalone Flow

```ts
// main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app/app.routes';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [provideRouter(appRoutes)]
});
```

```ts
// app.routes.ts
import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SettingsComponent } from './settings/settings.component';

export const appRoutes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path: 'settings',
    loadComponent: () => import('./settings/settings.component').then(m => m.SettingsComponent)
  }
];
```

```ts
// dashboard.component.ts
import { Component } from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [NgIf],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {}
```

### Key Differences

- `bootstrapApplication` accepts a root component and provider array, eliminating the bootstrap NgModule.
- Routes are described by plain objects; `loadComponent` lazy loads standalone components without creating feature modules.
- Component dependencies (such as `NgIf`, `FormsModule`, or other standalone components) are pulled in directly via the `imports` array.

### Migration Tips

1. **Start at the top:** Convert `main.ts` to use `bootstrapApplication` and mark `AppComponent` as `standalone: true`.
2. **Flatten modules gradually:** Leave large feature modules in place while you migrate critical leaf components to standalone. When a module no longer declares components, delete it.
3. **Shared utilities:** Replace barrel `SharedModule`s with standalone directives/pipes that you import where needed. Optionally expose them through Angular's new [signals-based control flow](control-flow.md).
4. **Testing:** In Angular 17, `TestBed` can bootstrap standalone components directly. Use `TestBed.configureTestingModule({ imports: [ComponentUnderTest] })` instead of `declarations`.

### Common Pitfalls

- **Provider scope:** Without NgModules, ensure your providers are registered at the correct level (`bootstrapApplication` vs `provideRouter` vs component-level providers).
- **Lazy boundaries:** `loadComponent` returns a component class, so avoid returning a module (common when migrating from `loadChildren`).
- **Third-party libraries:** Verify that libraries export standalone directives/components. When they don't, keep a temporary compatibility NgModule.

Migrating incrementally is safe—Angular supports mixing standalone and module-based code during the transition period.
