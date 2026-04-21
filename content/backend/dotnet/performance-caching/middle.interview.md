---
title: Performance and Caching Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level performance and caching trade-offs from the Web API interview question set.

Relevant concept maps:

- [Async and Response Performance Concept Map](performance-async-response.concept.md)
- [Caching and Diagnostics Concept Map](performance-caching-diagnostics.concept.md)

## Cache Types

```interview-question
What is the difference between response caching, output caching, and HybridCache in .NET? When do you use each?
---
answer:
Response caching is driven by HTTP headers and is respected by browsers, CDNs, or proxies. It is best when external caches should control reuse.

Output caching stores full HTTP responses on the server and is useful for read-heavy endpoints where the API wants server-side control and invalidation.

HybridCache is application-level caching for expensive data or business computations and supports in-memory plus distributed caching with stampede protection.
hints:
- One lives at the HTTP layer.
- One caches full server responses.
- One caches application data rather than whole responses.
```

Related concepts: [Response Caching](performance-caching-diagnostics.concept.md#response-caching), [Output Caching](performance-caching-diagnostics.concept.md#output-caching), [HybridCache](performance-caching-diagnostics.concept.md#hybridcache)

```interview-choice
Which caching mechanism is the best fit when multiple endpoints share the same expensive data lookup logic?
---
options:
- Response caching only
- HybridCache
- Gzip compression
correct: 1
explanation:
HybridCache is application-level caching, so it is the right tool when the expensive work is shared across multiple endpoints or service methods.
```

## Parallel Async Work

```interview-question
You have three independent async operations that each take around 200 ms. How do you optimize the endpoint?
---
answer:
Start all three tasks first and await them together with `Task.WhenAll(...)` so the total time is closer to the slowest task instead of the sum of all three.

This is appropriate only when the work is truly independent and the dependencies involved are safe to use concurrently.

For example, parallel queries on the same `DbContext` are not safe because `DbContext` is not thread-safe.
hints:
- Sequential awaits waste time when operations are independent.
- `Task.WhenAll` is the main tool.
- Shared dependencies can still make concurrency unsafe.
```

Related concepts: [Task.WhenAll Parallelism](performance-async-response.concept.md#taskwhenall-parallelism)

```interview-code
language: cs
prompt: Complete the endpoint so it runs `profileTask`, `ordersTask`, and `recommendationsTask` in parallel.
starter:
await Task.
solution:
await Task.WhenAll(profileTask, ordersTask, recommendationsTask);
checks:
- includes: WhenAll
- includes: profileTask
- includes: ordersTask
- includes: recommendationsTask
```

## Response Compression

```interview-question
What is response compression, and when should you not use it?
---
answer:
Response compression reduces payload size for text-based responses such as JSON by using algorithms like gzip or brotli.

You should avoid it for very small responses, already compressed content such as images or PDFs, and some streaming scenarios where buffering adds unwanted latency.

If a reverse proxy or CDN already handles compression, the API should usually avoid compressing the same response again.
hints:
- The benefit depends on response type and size.
- Binary content is often already compressed.
- Double compression is wasted work.
```

Related concepts: [Response Compression](performance-async-response.concept.md#response-compression)

## Async Mistakes

```interview-question
What common async and await mistakes do you see in .NET APIs that hurt performance or correctness?
---
answer:
Common mistakes include blocking with `.Result` or `.Wait()`, wrapping methods in unnecessary `async` and `await`, firing off background work without proper supervision, and awaiting independent tasks sequentially.

These patterns either reduce throughput, hide failures, or create fragile code under production load.

The fix is to keep async flows fully async, run independent tasks together when safe, and move real background work into managed infrastructure such as hosted services or queues.
hints:
- Blocking async code is a classic bug.
- Not every method needs an `async` state machine.
- Fire-and-forget is often a smell in APIs.
```

Related concepts: [Async Pitfalls](performance-async-response.concept.md#async-pitfalls)

## Serialization Choice

```interview-question
What is the difference between `System.Text.Json` and `Newtonsoft.Json`? When would you still choose `Newtonsoft.Json`?
---
answer:
`System.Text.Json` is the default in modern ASP.NET Core and is generally faster, lower allocation, and better aligned with AOT and source generation.

`Newtonsoft.Json` is still useful for some legacy codebases, JSON Patch support, or niche serialization cases where its maturity and older feature set still matter.

For new projects, `System.Text.Json` is usually the default choice.
hints:
- One is the modern default.
- Performance and source generation are major advantages.
- Legacy compatibility is the usual reason to keep the older library.
```

Related concepts: [JSON Serializer Choice](performance-async-response.concept.md#json-serializer-choice), [Source Generation](performance-caching-diagnostics.concept.md#source-generation)

```interview-choice
Which serializer is the normal default for new ASP.NET Core projects?
---
options:
- `System.Text.Json`
- `Newtonsoft.Json` always
- `BinaryFormatter`
correct: 0
explanation:
`System.Text.Json` is the default modern serializer because of its performance and platform alignment.
```

## Cancellation

```interview-question
What is `CancellationToken`, and why should every async API endpoint accept one?
---
answer:
`CancellationToken` signals that the request has been aborted or that the work should stop, such as when the client disconnects or the server is shutting down.

Passing it into database, HTTP, and I/O operations helps the server stop doing useless work for requests nobody is waiting for anymore.

It is an important resource-protection mechanism, not just a coding style preference.
hints:
- It tells ongoing work to stop.
- Client disconnects are a major source of cancellation.
- It must be propagated into downstream async calls to matter.
```

Related concepts: [Cancellation Propagation](performance-async-response.concept.md#cancellation-propagation)
*** Add File: content/backend/dotnet/performance-caching/senior.interview.md
---
title: Performance and Caching Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level performance and caching scenarios from the Web API interview question set.

Relevant concept maps:

- [Async and Response Performance Concept Map](performance-async-response.concept.md)
- [Caching and Diagnostics Concept Map](performance-caching-diagnostics.concept.md)

## Latency Diagnosis

```interview-question
An API endpoint takes 800 ms to respond. How do you systematically diagnose and reduce the latency?
---
answer:
Start by measuring where the time is spent instead of guessing. Add timing middleware or tracing and separate database time, external HTTP time, serialization time, and middleware overhead.

Then fix the actual bottleneck: indexes and projection for slow queries, caching or parallel calls for slow external dependencies, compression and payload shaping for large responses, or allocation reduction for GC-heavy paths.

Performance work should follow evidence, not a default answer like "just add caching."
hints:
- Measure before optimizing.
- Break the latency into layers.
- The right fix depends on the real bottleneck.
```

Related concepts: [Performance Diagnosis](performance-caching-diagnostics.concept.md#performance-diagnosis), [Observability for Performance](performance-caching-diagnostics.concept.md#observability-for-performance)

## Large CSV Streaming

```interview-question
How do you stream a large CSV export from an API without loading everything into memory?
---
answer:
Stream rows directly to the response body as they are read from the database, often using `IAsyncEnumerable<T>` and a `StreamWriter`.

This avoids building a giant in-memory list or a giant string before sending the response. Periodic flushing helps long exports make progress without waiting for the whole dataset.

The design should also consider cancellation so the export stops if the client disconnects.
hints:
- Write directly to the response.
- Avoid materializing the whole dataset first.
- Constant memory usage is the goal.
```

Related concepts: [Large Response Streaming](performance-async-response.concept.md#large-response-streaming), [Async Streams](performance-async-response.concept.md#async-streams)

## Mixed Volatility Cache Design

```interview-question
Your API serves both real-time stock prices and a slowly changing product catalog. How do you design the caching strategy for each?
---
answer:
Use aggressive caching for the product catalog because it changes infrequently and is usually read-heavy. Output caching and HTTP cache headers are strong fits there.

For stock prices, avoid long-lived caching because the data becomes stale quickly. Prefer push-based mechanisms like SignalR or, if polling is required, very short-lived application caching to prevent thundering herds.

Cache strategy should match both data volatility and access pattern, not use one fixed TTL for everything.
hints:
- Volatility is the key difference.
- Catalog data and real-time data need different strategies.
- A single TTL for both is usually wrong.
```

Related concepts: [Cache Strategy by Volatility](performance-caching-diagnostics.concept.md#cache-strategy-by-volatility)

## Benchmarking

```interview-question
How do you benchmark .NET code properly, and what mistakes do developers make when writing benchmarks?
---
answer:
Use BenchmarkDotNet instead of ad hoc `Stopwatch` timing.

Proper benchmarking needs warmup, multiple iterations, memory measurements, and protection against misleading effects such as JIT warmup, GC noise, and dead code elimination.

Common mistakes include benchmarking in Debug mode, measuring too much code at once, and ignoring allocations while only looking at elapsed time.
hints:
- Real benchmarking needs more than one timing measurement.
- Warmup and memory metrics matter.
- Debug-mode numbers are misleading.
```

Related concepts: [Benchmarking Discipline](performance-caching-diagnostics.concept.md#benchmarking-discipline)

## Source Generation

```interview-question
What is source generation in .NET, and how do you use it for better API performance?
---
answer:
Source generation produces code at compile time instead of depending on runtime reflection.

In APIs, common uses include `System.Text.Json` source generation, logging source generation, and generated regex. This improves performance, reduces allocations, and helps AOT and trimming scenarios.

The value is not only speed but also more predictable startup and runtime behavior.
hints:
- Compile-time code replaces some runtime reflection.
- Serialization is one of the biggest API uses.
- AOT compatibility is part of the benefit.
```

Related concepts: [Source Generation](performance-caching-diagnostics.concept.md#source-generation)
*** Add File: content/backend/dotnet/performance-caching/performance-async-response.concept.md
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
mnemonic:
Wait well, shape well, send well.
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
mnemonic:
If the caller is gone, the work should stop.
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
mnemonic:
Default modern, keep legacy only with reason.
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
