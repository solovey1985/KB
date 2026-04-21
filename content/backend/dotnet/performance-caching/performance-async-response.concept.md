---
title: Performance Async and Response Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the async and response-shaping concepts behind the performance and caching interview topic.

Study pages: [Section Index](index.md) | [Junior Questions](junior.interview.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Async Map

```concept-card
id: api-performance-fundamentals
term: API Performance Fundamentals
children:
- async-io
- thread-pool-throughput
- taskwhenall-parallelism
- async-pitfalls
- cancellation-propagation
- response-compression
- json-serializer-choice
- async-streams
- large-response-streaming
summary:
API performance fundamentals focus on how requests spend time waiting, allocating, serializing, and sending data back to clients.
details:
Many API bottlenecks come from I/O, serialization, and response size rather than CPU-heavy business code, so efficient waiting and response shaping matter a lot.
example:
An endpoint can be slow because of one database query, two sequential HTTP calls, or a huge JSON payload even when the business logic itself is simple.
mnemonic:
Wait efficiently, shape tightly, send less.
recall:
- Which parts of an API request often dominate performance cost?
- Why is async design closely tied to scalability?
```

```concept-card
id: async-io
term: Async I/O
parents:
- api-performance-fundamentals
related:
- thread-pool-throughput
summary:
Async I/O frees the request thread while external operations such as database and HTTP calls are in progress.
details:
It improves concurrency by letting the server use its limited thread pool for other requests instead of blocking during waits.
example:
`await httpClient.GetStringAsync(...)` frees the thread while the remote service responds.
mnemonic:
Do not block while the network works.
recall:
- Why does async I/O improve throughput?
- Which API operations are the best candidates for async?
```

```concept-card
id: thread-pool-throughput
term: Thread Pool Throughput
parents:
- api-performance-fundamentals
related:
- async-io
summary:
Thread pool throughput is the server's ability to keep handling more requests with a limited pool of worker threads.
details:
Blocking I/O reduces throughput because threads sit idle while external work completes. Async I/O protects throughput by releasing those threads during waits.
mnemonic:
Blocked threads mean wasted capacity.
recall:
- How does blocking I/O hurt request throughput?
- Why does async matter more under concurrency than in single-request demos?
```

```concept-card
id: taskwhenall-parallelism
term: Task.WhenAll Parallelism
parents:
- api-performance-fundamentals
summary:
`Task.WhenAll` runs independent async operations together so total latency approaches the slowest task instead of the sum of all tasks.
details:
It is effective only when the tasks are independent and the dependencies they use are safe for concurrent access.
example:
`await Task.WhenAll(profileTask, ordersTask, recommendationsTask);`
mnemonic:
Independent waits should overlap.
recall:
- When is `Task.WhenAll` the right optimization?
- What shared dependencies can make parallel execution unsafe?
```

```concept-card
id: async-pitfalls
term: Async Pitfalls
parents:
- api-performance-fundamentals
summary:
Async pitfalls are coding patterns that reduce throughput or hide failures even though the code appears asynchronous.
details:
Common examples include `.Result`, `.Wait()`, fire-and-forget work without supervision, and sequential awaits for independent tasks.
mnemonic:
Async code can still behave synchronously or unsafely.
recall:
- Why are `.Result` and `.Wait()` dangerous in request code?
- Why is fire-and-forget usually a weak pattern in APIs?
```

```concept-card
id: cancellation-propagation
term: Cancellation Propagation
parents:
- api-performance-fundamentals
summary:
Cancellation propagation passes `CancellationToken` through database, HTTP, and I/O calls so abandoned work can stop early.
details:
It protects server resources when clients disconnect, time out, or when the host is shutting down.
example:
Pass `ct` into `ToListAsync(ct)` and `GetAsync(url, ct)` so database and HTTP work stop when the request is aborted.
mnemonic:
Caller gone, work gone.
recall:
- Why does `CancellationToken` matter for resource usage?
- What happens if the token is accepted by the endpoint but never passed downstream?
```

```concept-card
id: response-compression
term: Response Compression
parents:
- api-performance-fundamentals
summary:
Response compression reduces payload size for text-heavy responses such as JSON.
details:
It is valuable for medium and large text responses, but it is wasteful for tiny responses, already compressed content, and some streaming workloads.
example:
Compress a 200 KB JSON report, but skip compression for a 700-byte health response or a PNG file.
mnemonic:
Compress text when bytes matter, skip it when overhead wins.
recall:
- Which kinds of responses benefit most from compression?
- When is compression more cost than value?
```

```concept-card
id: json-serializer-choice
term: JSON Serializer Choice
parents:
- api-performance-fundamentals
related:
- source-generation
summary:
JSON serializer choice affects throughput, allocations, compatibility, and AOT readiness.
details:
`System.Text.Json` is the modern default for performance and platform alignment, while `Newtonsoft.Json` remains useful for some legacy and specialized scenarios.
example:
Choose `System.Text.Json` for new APIs, but keep `Newtonsoft.Json` when JSON Patch or legacy converters are still required.
mnemonic:
Use the fast default unless a real legacy need wins.
recall:
- Why is `System.Text.Json` the usual default now?
- What are the typical reasons to keep `Newtonsoft.Json`?
```

```concept-card
id: async-streams
term: Async Streams
parents:
- api-performance-fundamentals
related:
- large-response-streaming
summary:
Async streams yield data items asynchronously one at a time with `IAsyncEnumerable<T>`.
details:
They are useful for large result sets where constant memory usage is more important than materializing everything before responding.
mnemonic:
Stream items as they arrive.
recall:
- What is the main benefit of `IAsyncEnumerable<T>` in APIs?
- When is streaming less helpful than normal materialization?
```

```concept-card
id: large-response-streaming
term: Large Response Streaming
parents:
- api-performance-fundamentals
related:
- async-streams
summary:
Large response streaming writes data directly to the response body instead of building the full payload in memory first.
details:
This is especially useful for exports such as CSV or large JSON streams, where memory pressure would otherwise grow with dataset size.
example:
`await foreach (var row in query.AsAsyncEnumerable()) { await writer.WriteLineAsync(...); }`
mnemonic:
Write as you read.
recall:
- Why is large response streaming useful for exports?
- What happens if a huge export is first built fully in memory?
```
