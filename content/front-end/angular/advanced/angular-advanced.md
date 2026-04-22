# Angular Advanced

This level covers the major modern Angular shifts around reactivity, templates, and rendering.

## Signals

Signals are Angular's fine-grained reactive state primitive.

```ts
import { computed, effect, signal } from '@angular/core';

const theme = signal<'light' | 'dark'>('light');
const refreshMs = signal(30000);
const summary = computed(() => `${theme()} · ${refreshMs()}ms`);

effect(() => {
  console.log('Settings changed:', summary());
});
```

Signals are especially attractive for:

- local synchronous state
- derived state via `computed`
- clear component-level reactivity

RxJS still matters for async streams and system boundaries.

## Template control flow

Modern Angular templates use built-in blocks such as `@if`, `@for`, and `@switch`.

```html
@if (userSignal(); as user) {
  <h2>{{ user.name }}</h2>
  @for (widget of widgetsSignal(); track widget.id) {
    <app-widget [data]="widget" />
  }
} @else {
  <p>Loading...</p>
}
```

These blocks are clearer than older microsyntax and give Angular stronger compiler understanding.

## Hydration and SSR

Angular supports server-side rendering and hydration so the client can reuse server-rendered DOM.

```ts
bootstrapApplication(AppComponent, {
  providers: [provideClientHydration()],
});
```

Hydration improves perceived startup performance for SSR-enabled apps because the client does not need to throw away and rebuild the already rendered DOM immediately.

## Modern Angular mindset

Modern Angular is:

- standalone-first
- signal-aware
- template-block oriented
- more SSR and hydration friendly

## Interview reminders

- explain where signals help and where RxJS still belongs
- mention `track` in `@for` as a performance concern
- hydration should be described as DOM reuse after SSR
