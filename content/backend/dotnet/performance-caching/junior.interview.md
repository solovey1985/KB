---
title: Performance and Caching Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level performance and caching distinctions from the Web API interview question set.

Relevant concept maps:

- [Async and Response Performance Concept Map](performance-async-response.concept.md)
- [Caching and Diagnostics Concept Map](performance-caching-diagnostics.concept.md)

## Async Fundamentals

```interview-question
What do `async` and `await` actually do in C#, and why not just use synchronous code?
---
answer:
`async` and `await` let .NET release the current thread while waiting for I/O such as database calls, HTTP calls, or file access.

They do not make one request magically faster, but they improve server throughput because threads are not blocked doing nothing while external work completes.

Synchronous I/O wastes request threads during waits, which limits how many concurrent requests the API can handle.
hints:
- Think throughput, not only latency.
- The thread is not busy doing CPU work during most I/O waits.
- Async is mainly about scalable waiting.
```

Related concepts: [Async I/O](performance-async-response.concept.md#async-io), [Thread Pool Throughput](performance-async-response.concept.md#thread-pool-throughput)

```interview-choice
Which statement is the most accurate?
---
options:
- `async` always makes a single request complete faster
- `async` mainly improves concurrency for I/O-bound work
- `async` is only useful in UI applications
correct: 1
explanation:
The main benefit of `async` in Web APIs is better throughput and scalability for I/O-bound operations, not automatically faster single-request execution.
```

## Collection Streaming

```interview-question
How does `IAsyncEnumerable<T>` work, and when would you use it in an API?
---
answer:
`IAsyncEnumerable<T>` yields items asynchronously one at a time instead of materializing the full collection in memory first.

It is useful for large exports, streamed responses, and long-running result sets where constant memory usage matters more than knowing the full count upfront.

It is less useful for small datasets or when the API must sort, count, or reshape the entire set before responding.
hints:
- Think one item at a time.
- The main benefit is memory usage.
- Not every endpoint needs streaming.
```

Related concepts: [Async Streams](performance-async-response.concept.md#async-streams), [Large Response Streaming](performance-async-response.concept.md#large-response-streaming)

```interview-choice
What is the main benefit of returning an `IAsyncEnumerable<T>` for a large export endpoint?
---
options:
- It guarantees the query will always be faster
- It can keep memory usage roughly constant while items are streamed
- It automatically provides total count metadata
correct: 1
explanation:
Streaming items as they are produced avoids loading the entire dataset into memory before sending the response.
```
