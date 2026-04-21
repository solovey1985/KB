---
title: Async JavaScript Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes async JavaScript around the event loop, callbacks, Promises, and `async`/`await`.

Study pages: [Section Index](index.md) | [Material Notes](async-javascript.md) | [Interview Practice](async-javascript.interview.md)

## Async Map

```concept-card
id: event-loop
term: Event Loop
children:
- microtask-queue
- macrotask-queue
- callback-hell
- promise
- async-and-await
summary:
The event loop coordinates when queued asynchronous work gets executed.
details:
It works with the call stack and runtime queues so JavaScript can stay responsive while waiting for host-provided async operations.
example:
Promise callbacks run after synchronous code, but before the next timer callback.
mnemonic:
Run the stack, then drain the right queue.
recall:
- What does the event loop coordinate?
- Why do Promises and timers not run in the same order?
```

```concept-card
id: microtask-queue
term: Microtask Queue
parents:
- event-loop
related:
- macrotask-queue
summary:
The microtask queue is where Promise reactions are scheduled.
details:
It is processed before the next macrotask, which is why Promise handlers often run before timers.
example:
`Promise.resolve().then(() => console.log('microtask'));`
mnemonic:
Promises cut the line before timers.
recall:
- Which JavaScript feature commonly schedules microtasks?
- Why do microtasks matter in execution ordering questions?
```

```concept-card
id: macrotask-queue
term: Macrotask Queue
parents:
- event-loop
related:
- microtask-queue
summary:
The macrotask queue holds tasks such as timer callbacks and many host callbacks.
details:
It is processed after the current synchronous work and after queued microtasks are drained.
example:
`setTimeout(() => console.log('timer'), 0);`
mnemonic:
Timers wait behind microtasks.
recall:
- What kinds of callbacks are macrotasks?
- Why can `setTimeout(..., 0)` still run later than Promise handlers?
```

```concept-card
id: callback-hell
term: Callback Hell
related:
- promise
- async-and-await
summary:
Callback hell is deeply nested async code that becomes hard to read and maintain.
details:
It usually appears when multiple async steps are chained through nested anonymous callbacks.
example:
`loadUser(id, user => loadOrders(user.id, orders => render(user, orders)));`
mnemonic:
Too much nesting, too little clarity.
recall:
- What makes callback hell hard to work with?
- Which newer patterns solve it better?
```

```concept-card
id: promise
term: Promise
children:
- promise-state
related:
- async-and-await
summary:
A Promise represents one future asynchronous result.
details:
It can be pending, fulfilled, or rejected and supports chaining with `then`, `catch`, and `finally`.
example:
`fetch('/api/user').then(r => r.json()).catch(console.error)`
mnemonic:
One future result, one chainable contract.
recall:
- What does a Promise represent?
- Why are Promises easier to manage than raw nested callbacks?
```

```concept-card
id: promise-state
term: Promise State
parents:
- promise
summary:
A Promise can be pending, fulfilled, or rejected.
details:
Once settled, its state cannot switch to a different final state.
example:
A pending `fetch(...)` promise eventually becomes fulfilled with a response or rejected with an error.
mnemonic:
Wait, succeed, or fail once.
recall:
- Which state comes first?
- Can a fulfilled Promise become rejected later?
```

```concept-card
id: async-and-await
term: async and await
related:
- promise
summary:
`async`/`await` is Promise-based syntax that makes asynchronous code read more sequentially.
details:
An `async` function returns a Promise, and `await` pauses that function until the awaited Promise settles.
example:
`const user = await fetch('/api/user').then(r => r.json());`
mnemonic:
Promise power, sequential reading.
recall:
- What does an `async` function return?
- What does `await` do inside that function?
```
