---
title: Angular Advanced Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes modern Angular around reactivity, template primitives, and SSR-aware rendering.

Study pages: [Section Index](index.md) | [Material Notes](angular-advanced.md) | [Interview Practice](angular-advanced.interview.md)

## Advanced Map

```concept-card
id: angular-signals
term: Angular Signals
children:
- computed-signal
- effect
- rxjs-interop
summary:
Signals are Angular's fine-grained reactive state primitive for synchronous application and component state.
details:
They support direct reads, derived state, and reactive updates without making every local state concern stream-based.
example:
`const theme = signal<'light' | 'dark'>('light');`
mnemonic:
Call to read, set to change.
recall:
- Why are signals strong for local state?
- Why do signals complement rather than replace RxJS?
```

```concept-card
id: computed-signal
term: computed Signal
parents:
- angular-signals
related:
- effect
summary:
`computed` creates derived state from other signals.
details:
It is the right tool for pure reactive transformations instead of imperative side effects.
example:
`const summary = computed(() => `${theme()} · ${refreshMs()}ms`);`
mnemonic:
Compute values, do not manually sync them.
recall:
- Why is `computed` preferred for pure derivation?
- How is it different from an effect?
```

```concept-card
id: effect
term: effect
parents:
- angular-signals
related:
- computed-signal
summary:
`effect` runs side effects when the signals it reads change.
details:
It is useful for logging, imperative bridges, or non-reactive API coordination, but should be used more sparingly than `computed`.
example:
`effect(() => console.log(summary()));`
mnemonic:
Effects do, computed knows.
recall:
- What kind of work belongs in an effect?
- Why should effects not be used for all derived values?
```

```concept-card
id: rxjs-interop
term: RxJS Interop
parents:
- angular-signals
summary:
RxJS interop is the bridge between stream-based asynchronous APIs and signal-based local state.
details:
It lets Angular apps use observables where streams fit best while still exposing signal-friendly values to templates.
example:
Convert an HTTP stream into a signal-backed view model for component rendering.
mnemonic:
Streams at the edge, signals in the view.
recall:
- Why does Angular still need RxJS even with signals?
- Where is the boundary between streams and local signal state?
```

```concept-card
id: template-control-flow
term: Template Control Flow
children:
- defer-block
summary:
Template control flow uses block syntax such as `@if`, `@for`, and `@switch` to express branching and iteration.
details:
It improves template readability and gives Angular stronger compile-time understanding.
example:
`@for (item of items; track item.id) { ... }`
mnemonic:
Blocks over microsyntax.
recall:
- Why is block syntax easier to read?
- Why is `track` important in `@for`?
```

```concept-card
id: defer-block
term: @defer Block
parents:
- template-control-flow
summary:
`@defer` delays rendering until a trigger such as idle time, viewport visibility, or interaction occurs.
details:
It is used to improve startup performance by postponing heavy UI work.
example:
Defer a large analytics widget until the user scrolls near it.
mnemonic:
Render later, load faster.
recall:
- What does `@defer` optimize?
- Which kinds of UI blocks are strong candidates for deferral?
```

```concept-card
id: server-side-rendering
term: Server-Side Rendering
related:
- hydration
summary:
Server-side rendering generates the initial HTML on the server before the client-side app takes over.
details:
It improves first render and SEO-related scenarios compared with purely client-rendered startup.
example:
An Angular product page can be rendered on the server so content appears before the full client bundle finishes loading.
mnemonic:
Render first on the server, then wake up on the client.
recall:
- Why is SSR useful in Angular applications?
- What user-facing problems can SSR improve?
```

```concept-card
id: hydration
term: Hydration
related:
- server-side-rendering
summary:
Hydration lets Angular reuse server-rendered DOM during client startup.
details:
It avoids throwing away and rebuilding the already rendered HTML immediately after SSR.
example:
`provideClientHydration()` enables client hydration during app bootstrap.
mnemonic:
Reuse the DOM, do not rebuild it blindly.
recall:
- What does hydration reuse?
- Why is hydration valuable after SSR?
```
