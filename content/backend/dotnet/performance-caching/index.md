# Performance and Caching

This section turns the performance and caching material from the `.NET Web API Interview Questions` PDF into focused study pages.

## Interview Practice By Level

- [Junior Questions](junior.interview.md)
- [Middle Questions](middle.interview.md)
- [Senior Questions](senior.interview.md)

## Concept Maps

- [Async and Response Performance Concept Map](performance-async-response.concept.md)
- [Caching and Diagnostics Concept Map](performance-caching-diagnostics.concept.md)

## Study Flow

1. Start with [Junior Questions](junior.interview.md) for async fundamentals and streaming basics.
2. Read the [Async and Response Performance Concept Map](performance-async-response.concept.md) before tuning endpoint implementations.
3. Use [Middle Questions](middle.interview.md) for caching layers, compression, serializer choices, and cancellation.
4. Finish with [Senior Questions](senior.interview.md) and the [Caching and Diagnostics Concept Map](performance-caching-diagnostics.concept.md).

## Related Topics

- [ASP.NET Core Internals and Middleware](../aspnet-core-internals-middleware/index.md)
- [EF Core and Data Access](../ef-core-data-access/index.md)
- [.NET Backend Study Index](../index.md)

## Topic Coverage

- response compression
- async and await fundamentals
- common async mistakes
- parallel async operations
- `CancellationToken`
- `System.Text.Json` versus `Newtonsoft.Json`
- `IAsyncEnumerable<T>` and streaming exports
- response caching, output caching, and HybridCache
- cache strategy by data volatility
- latency diagnosis and benchmarking
- source generation for API performance
