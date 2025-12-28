# Modern Template Control Flow

Angular 17 introduces built-in control flow blocks that replace structural directives like `*ngIf` and `*ngFor`. The new syntax is friendlier to TypeScript inference, avoids the microsyntax arcana, and unlocks smarter template analysis by the compiler.

## Old Structural Directives

```html
<!-- dashboard.component.html -->
<div *ngIf="user$ | async as user; else loading">
  <h2>Welcome, {{ user.name }}</h2>
  <ng-container *ngFor="let widget of widgets; trackBy: trackWidget">
    <app-widget [data]="widget"></app-widget>
  </ng-container>
</div>
<ng-template #loading>
  <p>Fetching data...</p>
</ng-template>
```

- Uses asterisks (`*`) as sugar for `ng-template`.
- Advanced scenarios require `let` bindings, `ng-template`, and `trackBy` micro-syntax.

## Angular 17 Control Flow

```html
@defer (when widgetsSignal()) {
  @if (userSignal(); as user) {
    <h2>Welcome, {{ user.name }}</h2>
    @for (widget of widgetsSignal(); track widget.id) {
      <app-widget [data]="widget" />
    }
  } @else {
    <p>Fetching data...</p>
  }
}
```

### Highlights

- `@if` with `as` bindings exposes scoped values without `let` syntax.
- `@for` uses explicit `track` expressions and supports block-level animations.
- `@switch` mirrors JavaScript switch semantics with fallthrough prevention.
- `@defer` schedules template rendering based on triggers (idle, viewport, interaction) for performance gains.

## Interop With Existing Code

You can mix legacy structural directives and new blocks while refactoring. First, enable the required feature flag in the template by importing `provideZoneChangeDetection()` in `main.ts` or ensure your project targets v17 defaults.

```ts
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideZoneChangeDetection({ eventCoalescing: true })
  ]
});
```

## Accessibility & DX Benefits

- **Type narrowing:** Inside `@if (userSignal())`, the compiler understands that `userSignal()` returns a non-null value.
- **Template diagnostics:** Angular can warn you about unused variables or invalid tracking expressions because the syntax is more explicit.
- **Cleaner diffing:** Without extra `<ng-container>` wrappers, DOM output matches template structure.

## Migration Checklist

1. Identify templates with nested `*ngIf` wrappers—replace them with `@if` / `@else` blocks.
2. Convert long `*ngSwitch` chains to `@switch` for readability.
3. Use `@for (...; track ...)` with stable identifiers to prevent re-renders.
4. Introduce `@defer` on heavy dashboard widgets or routes to improve initial load metrics.

Combine these blocks with signals-based data access to reduce reliance on `async` pipes while keeping templates declarative.
