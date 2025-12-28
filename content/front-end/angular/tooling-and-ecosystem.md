# Builders, Hydration, and Ecosystem Updates

Angular 17 continues the move toward a modern, fast toolchain while baking in server rendering and test improvements.

## Application Builder 17

- **Unified builder:** `ng build`, `ng serve`, and `ng test` share a Rust-powered pipeline (via esbuild & ngcc removal) for faster cold starts.
- **Hydration default:** Creating a project with `ng new` enables hydration automatically. Existing projects opt in with `provideClientHydration()`.
- **Environment targets:** Browser support tables live in `browserslist` by default; differential loading and ES5 bundles are removed.

```ts
// main.ts
bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(appRoutes),
    provideClientHydration()
  ]
});
```

## Test Runner Refresh

- `ng test` now calls the Watch Mode-powered web test runner.
- Standalone components register via `imports` instead of `declarations` in `TestBed` configurations.
- Snapshot-style assertions are encouraged with `@angular/dev-infra/testing` utilities.

```ts
beforeEach(() => TestBed.configureTestingModule({
  imports: [SettingsComponent]
}));
```

## Server-Side Rendering & Edge

- `ng generate app-shell` has been replaced by `ng add @angular/ssr`.
- Hybrid rendering lets you stream SSR responses and hydrate in segments.
- Builder presets include `cloudflare` and `vercel` adapters.

```ts
import { provideServerRendering } from '@angular/platform-server';

export const config = {
  providers: [provideServerRendering()]
};
```

## Tooling Checklist for Upgrades

1. Update to the latest `@angular/cli` and run `ng update` to migrate workspace config.
2. Remove custom webpack builders: the default pipeline handles Tailwind, Sass, and asset optimisation.
3. Enable hydration and verify that DOM diffs are stable; adjust third-party scripts that mutate the DOM eagerly.
4. Adopt the new test runner and convert module-based specs to standalone imports.

## Ecosystem Notes

- **Material 17:** Ships standalone components by default and exposes signal-friendly APIs.
- **Router:** Deferrable Views (`@defer` blocks) integrate with the router for progressive route activation.
- **Nx / Monorepos:** Nx 17 generators align with Angular's standalone defaults—no more `app.module.ts` in generated libs.

Stay mindful of third-party packages that still require NgModules; most libraries now expose standalone entry points, but verify before deleting your last shared module.
