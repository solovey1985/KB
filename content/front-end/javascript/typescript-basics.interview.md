---
title: JavaScript Interview Preparation
interview:
  persistProgress: true
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

This page mirrors the sample interview layout while focusing on the JavaScript topics already documented in this section:

- asynchronous JavaScript fundamentals
- Promises and common pitfalls
- Observables and RxJS operators
- practical code-completion exercises

Use it as an interview-prep page and as a rendering check for the interview plugin.

## Hidden Answer Questions

```interview-question
Explain how the JavaScript event loop works.
---
answer:
The JavaScript runtime uses an **event loop** together with a **call stack** and task queues.

The core flow is:

1. Functions are pushed onto the call stack and executed in order.
2. Asynchronous operations such as `setTimeout`, network requests, or DOM events are delegated to the host environment.
3. When those operations finish, their callbacks are queued.
4. The event loop moves queued work onto the call stack when the stack is empty.

Promises use the **microtask queue**, which is processed before the next macrotask such as a `setTimeout` callback.
hints:
- Think about what happens when the call stack becomes empty.
- JavaScript itself is single-threaded, but the runtime can schedule async work.
- Promise callbacks and timer callbacks do not have the same priority.
```

```interview-question
What is callback hell, and how can modern JavaScript avoid it?
---
answer:
**Callback hell** is a situation where asynchronous logic becomes deeply nested and hard to read, maintain, and debug.

Typical problems:

- excessive nesting
- poor error handling
- difficult control flow

Modern JavaScript avoids it by using:

- **Promises** with chaining
- **async/await** for more linear code
- smaller named functions instead of anonymous nested callbacks

These approaches improve readability and centralize error handling.
hints:
- It is often called the "Pyramid of Doom".
- Think about what Promises improved compared with raw callbacks.
- One common solution is syntax sugar over Promises.
```

```interview-question
How does an Observable differ from a Promise?
---
answer:
An **Observable** represents a stream of values over time, while a **Promise** represents a single future value.

Key differences:

- Promises are **eager**; Observables are usually **lazy**.
- Promises resolve once; Observables can emit many values.
- Promises cannot be cancelled directly; Observables can be cancelled with `unsubscribe()`.
- Observables provide rich operators such as `map`, `filter`, and `switchMap`.

In short, Promises are suitable for one async result, while Observables are better for streams and more advanced async composition.
hints:
- One delivers a single result, the other can deliver many.
- One is commonly associated with RxJS.
- Think about cancellation support.
```

## Multiple Choice Questions

```interview-choice
Which statement about closures in JavaScript is correct?
---
options:
- A closure only exists inside async functions.
- A closure keeps access to variables from its lexical scope.
- A closure copies all outer variables by value when the function is created.
correct: 1
explanation:
A closure gives a function access to variables from the lexical scope where it was created.

It is not limited to async functions, and it does not copy every captured variable by value.
```

```interview-choice
Which option best describes `Promise.all()`?
---
options:
- It resolves after the first promise finishes.
- It resolves when all promises resolve, and rejects if any promise rejects.
- It ignores rejected promises and only returns resolved ones.
correct: 1
explanation:
`Promise.all()` waits for every input promise to resolve.

If any promise rejects, the combined promise rejects immediately with that error.
```

```interview-choice
Which RxJS operator is most appropriate for canceling an earlier request when a new search term arrives?
---
options:
- mergeMap
- concatMap
- switchMap
- take
correct: 2
explanation:
`switchMap` unsubscribes from the previous inner observable when a new value arrives.

That behavior makes it a strong fit for search-as-you-type scenarios where stale requests should be discarded.
```

## Code Completion Questions

```interview-code
language: js
prompt: Complete the function so it returns only even values from the input array.
starter:
function onlyEven(values) {
  return values.
}
solution:
function onlyEven(values) {
  return values.filter(value => value % 2 === 0);
}
checks:
- includes: filter
- includes: % 2 === 0
```

```interview-code
language: js
prompt: Complete the function so it fetches two resources in parallel and returns both results.
starter:
async function fetchBoth(url1, url2) {
  const results = await 
  return results;
}
solution:
async function fetchBoth(url1, url2) {
  const results = await Promise.all([fetch(url1), fetch(url2)]);
  return results;
}
checks:
- includes: Promise.all
- includes: fetch
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

## Mixed Markdown Content

Interview pages can include regular markdown between interactive blocks.

### Things to verify

- progress summary appears at the top
- hidden-answer questions reveal hints one by one
- multiple-choice questions show explanations after checking
- code-completion questions preserve draft state in `localStorage`
- progress survives a page reload

### Suggested manual test flow

1. Open this page in the browser.
2. Reveal one hint and one answer.
3. Answer a multiple-choice question.
4. Complete one code exercise.
5. Refresh the page.
6. Confirm your progress and draft were restored.

## Notes

This page is based on the JavaScript content in this folder, especially topics around async flow, Promises, Observables, and RxJS.

Current plugin constraints:

- activation happens on `*.interview.md`
- code checking is heuristic, not sandboxed execution
- progress is local to the browser
