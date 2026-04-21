---
title: Reactive Streams Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise Observable and RxJS questions.

Relevant concept maps:

- [Reactive Concept Map](reactive-streams.concept.md)

## Promise versus Observable

```interview-question
How does an Observable differ from a Promise?
---
answer:
An Observable represents a stream of values over time, while a Promise represents one future value.

Observables are usually lazy, can emit many values, and can be cancelled with `unsubscribe()`. Promises are eager, settle once, and do not provide the same cancellation model.
hints:
- One value versus many values.
- Eager versus lazy.
- Cancellation is a major distinction.
```

Related concepts: [Observable](reactive-streams.concept.md#observable), [Promise Versus Observable](reactive-streams.concept.md#promise-versus-observable)

```interview-choice
Which RxJS operator is most appropriate for canceling an older request when a new search term arrives?
---
options:
- `mergeMap`
- `concatMap`
- `switchMap`
correct: 2
explanation:
`switchMap` switches to the newest inner observable and unsubscribes from the previous one, which is ideal for live search flows.
```

```interview-code
language: js
prompt: Complete the RxJS pipeline so it emits only even values multiplied by 10.
starter:
import { from } from 'rxjs';
import { filter, map } from 'rxjs/operators';

from([1, 2, 3, 4, 5, 6]).pipe(
  
).subscribe(console.log);
solution:
import { from } from 'rxjs';
import { filter, map } from 'rxjs/operators';

from([1, 2, 3, 4, 5, 6]).pipe(
  filter(value => value % 2 === 0),
  map(value => value * 10)
).subscribe(console.log);
checks:
- includes: filter
- includes: map
- includes: % 2 === 0
- includes: * 10
```
