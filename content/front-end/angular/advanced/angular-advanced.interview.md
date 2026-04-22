---
title: Angular Advanced Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise modern Angular architecture and runtime questions.

Relevant concept maps:

- [Concept Map](angular-advanced.concept.md)

## Signals and Templates

```interview-question
How do signals differ from using a `BehaviorSubject` for simple local component state?
---
answer:
Signals provide synchronous reads, automatic dependency tracking, and direct derived state through `computed`.

`BehaviorSubject` is still useful for observable streams and async boundaries, but signals are usually simpler for local component view state.
hints:
- One model reads like a value function.
- One model is stream-oriented.
- Local state is the key use case distinction.
```

Related concepts: [Angular Signals](angular-advanced.concept.md#angular-signals), [computed Signal](angular-advanced.concept.md#computed-signal), [RxJS Interop](angular-advanced.concept.md#rxjs-interop)

```interview-question
What problem do `@if`, `@for`, and `@switch` solve compared with older structural directives?
---
answer:
They replace the older microsyntax-heavy template style with explicit block syntax that is easier to read and easier for the compiler to analyze.

They also make tracking expressions and branching more explicit in templates.
hints:
- Think clarity and compiler understanding.
- The old approach used structural-directive microsyntax.
- Readability is part of the answer.
```

Related concepts: [Template Control Flow](angular-advanced.concept.md#template-control-flow), [@defer Block](angular-advanced.concept.md#defer-block)

```interview-question
Why would you enable hydration in a modern Angular application?
---
answer:
Hydration lets Angular reuse server-rendered HTML on the client instead of rebuilding the DOM immediately.

That improves perceived startup performance for SSR-enabled applications.
hints:
- Think SSR plus client takeover.
- DOM reuse is the key phrase.
- Performance is the reason.
```

Related concepts: [Hydration](angular-advanced.concept.md#hydration), [Server-Side Rendering](angular-advanced.concept.md#server-side-rendering)

```interview-choice
Which Angular provider enables client hydration?
---
options:
- `provideClientHydration()`
- `provideZoneChangeDetection()`
- `provideForms()`
correct: 0
explanation:
`provideClientHydration()` enables Angular to reuse server-rendered DOM during client startup.
```

```interview-code
language: ts
prompt: Complete the signal-based derived state so it recomputes from `theme` and `refreshMs`.
starter:
const theme = signal<'light' | 'dark'>('light');
const refreshMs = signal(30000);
const summary = 
solution:
const theme = signal<'light' | 'dark'>('light');
const refreshMs = signal(30000);
const summary = computed(() => `${theme()} · ${refreshMs()}ms`);
checks:
- includes: computed
- includes: theme()
- includes: refreshMs()
```
