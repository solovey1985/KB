# Signals and Component Reactivity

Signals graduated from developer preview in Angular 17, offering a fine-grained reactivity model that complements RxJS instead of replacing it. Think of signals as in-memory state holders with automatic dependency tracking.

## Previous Pattern: Observable State

```ts
// widget-settings.service.ts
@Injectable({ providedIn: 'root' })
export class WidgetSettingsService {
  private readonly settingsSubject = new BehaviorSubject<WidgetSettings>({
    theme: 'light',
    refreshMs: 30_000
  });

  readonly settings$ = this.settingsSubject.asObservable();

  update(partial: Partial<WidgetSettings>): void {
    this.settingsSubject.next({ ...this.settingsSubject.value, ...partial });
  }
}
```

```html
<!-- settings.component.html -->
<form *ngIf="settings$ | async as settings">
  <input [value]="settings.theme" (input)="onTheme($event.target.value)" />
</form>
```

- Requires manual `BehaviorSubject` plumbing and retains async pipe noise in templates.
- Harder to co-locate state with components unless you promote it to a service.

## Angular 17 Signals-Based Approach

```ts
import { Component, computed, effect, signal } from '@angular/core';

@Component({
  selector: 'app-settings',
  standalone: true,
  templateUrl: './settings.component.html'
})
export class SettingsComponent {
  readonly theme = signal<'light' | 'dark'>('light');
  readonly refreshMs = signal(30_000);
  readonly summary = computed(() => `${this.theme()} · ${this.refreshMs()}ms`);

  constructor() {
    effect(() => console.log('Settings changed:', this.summary()));
  }

  updateTheme(theme: 'light' | 'dark') {
    this.theme.set(theme);
  }
}
```

```html
<!-- settings.component.html -->
<form>
  <input [value]="theme()" (input)="updateTheme($event.target.value)" />
  <p>{{ summary() }}</p>
</form>
```

### Benefits

- **Synchronous reads:** Access signal values by calling them like functions; no `async` pipe necessary.
- **Automatic dependency graph:** `computed` recalculates only when dependencies change.
- **Effects:** Run side effects when signal values update—ideal for logging, bridging to observables, or imperative APIs.

## Integration Points

- **With RxJS:** Convert a signal to an observable via `toObservable(signal)`; convert back with `toSignal(observable)` for compatibility with existing services.
- **Forms:** Use `model` inputs (Angular 17 developer preview) to bind signals directly to form controls without `FormControl` boilerplate.
- **Change detection:** Signals are zone-aware; combine them with `provideZoneChangeDetection()` to minimise re-renders.

## Migration Tips

1. Replace local `BehaviorSubject` + `async` combos with component-level signals.
2. Keep RxJS for async data flows (HTTP, websockets) and bridge them into signals for the view layer.
3. Co-locate simple state inside standalone components; promote to services only when multiple components need the state.
4. Use `effect` sparingly—prefer derived `computed` signals for pure transformations.

Signals make reactivity more ergonomic while preserving Angular's deterministic change detection semantics.
