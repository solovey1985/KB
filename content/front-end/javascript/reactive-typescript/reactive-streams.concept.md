---
title: Reactive Streams Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the main Observable and RxJS ideas for frontend work.

Study pages: [Section Index](index.md) | [Reactive Notes](reactive-streams.md) | [Reactive Interview Practice](reactive-streams.interview.md)

## Reactive Map

```concept-card
id: observable
term: Observable
children:
- promise-versus-observable
- subscription
- rxjs-operators
summary:
An Observable represents a stream of values over time.
details:
It can emit zero, one, or many values and allows subscription-based reactive programming.
example:
User input over time is a stream, not a single result.
mnemonic:
Observable means a sequence, not a single answer.
recall:
- What does an Observable represent?
- Why is it a good fit for event streams?
```

```concept-card
id: promise-versus-observable
term: Promise Versus Observable
parents:
- observable
summary:
Promises model one future result, while Observables model streams over time.
details:
Observables add laziness, repeated emissions, and cancellation through unsubscription.
example:
One HTTP request fits a Promise, but search input changes fit an Observable.
mnemonic:
Promise once, Observable over time.
recall:
- What are the three biggest differences between Promises and Observables?
- When is a Promise still the simpler choice?
```

```concept-card
id: subscription
term: Subscription
parents:
- observable
summary:
A subscription starts observing an Observable and can later be used for cleanup.
details:
It controls lifecycle and is especially important for avoiding leaks in long-lived UI code.
example:
`const sub = stream$.subscribe(); sub.unsubscribe();`
mnemonic:
Subscribe to start, unsubscribe to stop.
recall:
- Why is subscription cleanup important?
- What problem does `unsubscribe()` solve?
```

```concept-card
id: rxjs-operators
term: RxJS Operators
children:
- switchmap
summary:
RxJS operators transform, combine, filter, or control Observable streams.
details:
Operators such as `map`, `filter`, `tap`, and `switchMap` make streams composable and expressive.
example:
`stream$.pipe(filter(Boolean), map(x => x * 2))`
mnemonic:
Operators shape the stream.
recall:
- Why are operators central to RxJS?
- Which operators are most common in frontend code?
```

```concept-card
id: switchmap
term: switchMap
parents:
- rxjs-operators
summary:
`switchMap` maps each value to a new inner Observable and switches to the newest one.
details:
It is useful when older in-flight work should be abandoned when newer input arrives.
example:
Search input should usually switch to the latest request instead of waiting for stale ones.
mnemonic:
New value in, old inner work out.
recall:
- Why is `switchMap` strong for live search?
- How is it different from `mergeMap` or `concatMap`?
```
