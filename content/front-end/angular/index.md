# Angular 17 Upgrade Notes

Angular 17 marks one of the largest DX-focused releases since the Ivy compiler, introducing a revamped rendering pipeline, modern template primitives, and a streamlined project bootstrap experience. This section tracks the conceptual shifts you will encounter while migrating code that was written around the Angular 14-16 era.

## What's New at a Glance

- **Opt-in is over:** Standalone components are the default pattern; NgModules stay only for backwards compatibility.
- **Template control flow:** The new `@if`, `@for`, and `@switch` blocks replace structural directives like `*ngIf` for clearer ergonomics and improved type-checking.
- **Signals-based reactivity:** Signals, introduced in v16, are now first-class and power built-in forms of change detection.
- **Hydration & SSR by default:** The new application builder optimises for server-side rendering with out-of-the-box hydration support.
- **Command palette enhancements:** The `ng` CLI now scaffolds and updates standalone-first projects and offers improved test tooling.

## Suggested Practice Project

Experiment with the new APIs by evolving a small app from the "Widgets HQ Dashboard" template:

1. **Legacy state:** A dashboard built with NgModules, `*ngIf`, and `*ngFor`, fetching data via RxJS observables.
2. **Migration goals:**
   - Replace the `AppModule` bootstrap with `bootstrapApplication` and standalone components.
   - Rewrite key views using `@if`, `@for`, and deferred views (`@defer`).
   - Introduce signals to manage widget settings locally, keeping RxJS only for async boundaries.
   - Turn on hydration and SSR to improve first paint for the analytics route.
3. **Stretch ideas:** experiment with the new test runner, or refactor a feature module into a lazy, standalone route.

## Deep-Dive Articles

- [Standalone bootstrap and routing](/content/front-end/angular/standalone-bootstrap.md)
- [Modern template control flow](/content/front-end/angular/control-flow.md)
- [Signals and component reactivity](/content/front-end/angular/signals-reactivity.md)
- [Builders, hydration, and ecosystem updates](/content/front-end/angular/tooling-and-ecosystem.md)

Use these notes while iteratively upgrading code or onboarding teammates to the Angular 17 mindset.
