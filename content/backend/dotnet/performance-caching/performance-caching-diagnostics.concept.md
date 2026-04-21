---
title: Performance Caching and Diagnostics Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the caching and diagnostics concepts from the performance and caching interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Caching Map

```concept-card
id: api-performance-operations
term: API Performance Operations
children:
- response-caching
- output-caching
- hybridcache
- cache-strategy-by-volatility
- performance-diagnosis
- observability-for-performance
- benchmarking-discipline
- source-generation
summary:
API performance operations combine caching, measurement, and build-time optimization to keep real systems fast under load.
details:
Performance is not one trick. It is a mix of serving repeated work from the right cache, measuring real bottlenecks, and reducing runtime overhead where it matters.
mnemonic:
Cache wisely, measure honestly, optimize specifically.
recall:
- Why is performance work broader than caching?
- What do caching and diagnostics each contribute to stable API performance?
```

```concept-card
id: response-caching
term: Response Caching
parents:
- api-performance-operations
summary:
Response caching relies on HTTP cache headers so browsers, CDNs, or proxies can reuse responses.
details:
It is best when external infrastructure should cache the response and when the API can describe freshness with standard HTTP semantics.
example:
`Cache-Control: public, max-age=1800` lets a browser or CDN reuse a catalog response for 30 minutes.
mnemonic:
Tell the edge how long reuse is safe.
recall:
- Who usually respects response caching headers?
- When is response caching preferable to server-side caching?
```

```concept-card
id: output-caching
term: Output Caching
parents:
- api-performance-operations
summary:
Output caching stores full HTTP responses on the server for fast reuse.
details:
It is ideal for read-heavy endpoints that return repeatable responses and benefit from server-controlled invalidation.
example:
`app.MapGet("/api/products", handler).CacheOutput();`
mnemonic:
Same request, same response, skip the work.
recall:
- What does output caching store?
- Why is server-side invalidation valuable?
```

```concept-card
id: hybridcache
term: HybridCache
parents:
- api-performance-operations
summary:
HybridCache is application-level caching that combines fast local memory with distributed caching and stampede protection.
details:
It is useful for expensive lookups and computations shared across endpoints or services, especially in multi-instance applications.
example:
Cache `product-42` in HybridCache so product details and recommendations can reuse the same expensive lookup.
mnemonic:
One cache API, two cache layers.
recall:
- Why is HybridCache more than a plain memory cache?
- When is application-level caching a better fit than response caching?
```

```concept-card
id: cache-strategy-by-volatility
term: Cache Strategy by Volatility
parents:
- api-performance-operations
summary:
Cache strategy by volatility means choosing cache duration and mechanism based on how quickly the data changes.
details:
Slowly changing catalogs can use longer caching and HTTP reuse, while real-time data often needs push updates or only micro-caching to avoid stale results.
example:
Cache a product catalog for 30 minutes, but stock prices for only a few seconds or push them over SignalR.
mnemonic:
Stable data caches long, hot data caches lightly.
recall:
- Why should product catalogs and stock prices use different caching strategies?
- What does volatility tell you about the right cache TTL?
```

```concept-card
id: performance-diagnosis
term: Performance Diagnosis
parents:
- api-performance-operations
related:
- observability-for-performance
summary:
Performance diagnosis is the process of finding where latency or resource cost is actually coming from.
details:
It usually starts by separating database time, external call time, serialization cost, and middleware overhead so the real bottleneck becomes visible.
example:
An 800 ms endpoint might break down into 450 ms database time, 250 ms external API time, and 100 ms serialization.
mnemonic:
Do not optimize guesses.
recall:
- What should be measured first when an endpoint is slow?
- Why is "just add caching" a weak first response?
```

```concept-card
id: observability-for-performance
term: Observability for Performance
parents:
- api-performance-operations
related:
- performance-diagnosis
summary:
Observability for performance means using metrics, logs, and tracing to spot latency regressions and bottlenecks continuously.
details:
Without visibility into timing and failure patterns, teams only learn about performance problems after users feel them.
mnemonic:
If you cannot see it, you cannot tune it.
recall:
- Why does performance work need ongoing observability rather than one-time tuning?
- Which telemetry types help explain slow requests best?
```

```concept-card
id: benchmarking-discipline
term: Benchmarking Discipline
parents:
- api-performance-operations
summary:
Benchmarking discipline is the practice of measuring code with reliable tools and realistic methodology.
details:
BenchmarkDotNet handles warmup, multiple iterations, memory measurements, and noise reduction better than ad hoc `Stopwatch` timing.
example:
Use BenchmarkDotNet to compare `System.Text.Json` and `Newtonsoft.Json` instead of timing one serialization call manually.
mnemonic:
Real benchmarks beat timing guesses.
recall:
- Why is BenchmarkDotNet preferred over manual timing?
- What benchmark mistakes create misleading results?
```

```concept-card
id: source-generation
term: Source Generation
parents:
- api-performance-operations
related:
- json-serializer-choice
summary:
Source generation produces code at compile time to reduce runtime reflection and improve performance.
details:
In APIs, common cases include JSON serialization metadata, logging methods, and regex generation. It helps startup, throughput, and AOT compatibility.
example:
`[JsonSerializable(typeof(ProductResponse))]` pre-generates JSON metadata so serialization avoids some runtime reflection.
mnemonic:
Generate early, pay less later.
recall:
- Which API tasks commonly benefit from source generation?
- Why is source generation helpful beyond raw speed?
```
