---
title: Async JavaScript Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the async JavaScript questions that appear frequently in frontend interviews.

Relevant concept maps:

- [Concept Map](async-javascript.concept.md)

## Event Loop

```interview-question
Explain how the JavaScript event loop works.
---
answer:
JavaScript uses a call stack for synchronous execution and queues for deferred work.

The host environment finishes async work such as timers or network requests and then schedules callbacks. The event loop moves queued work onto the call stack when the stack is ready.

Promise handlers are microtasks, so they run before the next macrotask like a `setTimeout` callback.
hints:
- Think call stack plus queues.
- The browser or runtime host participates.
- Microtasks and timers do not have the same priority.
```

Related concepts: [Event Loop](async-javascript.concept.md#event-loop), [Microtask Queue](async-javascript.concept.md#microtask-queue), [Macrotask Queue](async-javascript.concept.md#macrotask-queue)

## Async Models

```interview-question
What is callback hell, and how does modern JavaScript avoid it?
---
answer:
Callback hell is deeply nested asynchronous code that becomes hard to read, maintain, and debug.

Modern JavaScript avoids it with Promise chaining, `async`/`await`, and clearer separation of logic into named functions.
hints:
- It is also called the pyramid of doom.
- Nesting is the main problem.
- Promises and `async`/`await` are the modern solutions.
```

Related concepts: [Callback Hell](async-javascript.concept.md#callback-hell), [Promise](async-javascript.concept.md#promise), [async and await](async-javascript.concept.md#async-and-await)

```interview-question
What are the states of a Promise?
---
answer:
A Promise can be pending, fulfilled, or rejected.

Pending means the async work is still in progress. Fulfilled means it completed successfully. Rejected means it failed.
hints:
- There are three states.
- One means still waiting.
- The other two are the settled outcomes.
```

Related concepts: [Promise](async-javascript.concept.md#promise), [Promise State](async-javascript.concept.md#promise-state)

```interview-choice
Which output order is correct?
---
options:
- `start`, `timeout`, `promise`, `end`
- `start`, `end`, `promise`, `timeout`
- `promise`, `start`, `end`, `timeout`
correct: 1
explanation:
Synchronous code runs first, then the Promise microtask, then the timer callback.
```

```interview-code
language: js
prompt: Complete the function so it loads both resources in parallel.
starter:
async function loadBoth(url1, url2) {
  const results = await 
  return results;
}
solution:
async function loadBoth(url1, url2) {
  const results = await Promise.all([fetch(url1), fetch(url2)]);
  return results;
}
checks:
- includes: Promise.all
- includes: fetch
```
