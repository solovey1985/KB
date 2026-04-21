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
