---
title: Modern .NET Runtime Features Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the .NET runtime and framework features from the modern .NET interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Runtime Map

```concept-card
id: modern-dotnet-runtime-features
term: Modern .NET Runtime Features
children:
- minimal-apis
- async-enumerable-feature
- source-generation
- timeprovider
- modern-dotnet-platform-features
summary:
Modern .NET runtime features improve API ergonomics, testability, serialization, and platform support in newer .NET releases.
details:
These features matter when they reduce boilerplate, replace custom infrastructure, or make new runtime models such as AOT more practical.
example:
Minimal APIs, source-generated JSON metadata, and `TimeProvider` all reduce different kinds of incidental complexity.
mnemonic:
New runtime features should remove custom work, not add novelty only.
recall:
- Which runtime features help most in modern Web APIs?
- Why should adoption be based on practical value rather than hype?
```

```concept-card
id: minimal-apis
term: Minimal APIs
parents:
- modern-dotnet-runtime-features
summary:
Minimal APIs define HTTP endpoints directly with route-mapping methods instead of controller classes.
details:
They fit lightweight, explicit API development and align well with route groups, endpoint filters, and modern ASP.NET Core defaults.
example:
`app.MapGet("/api/products/{id}", async (int id, AppDbContext db) => ...);`
mnemonic:
Map the route, write the handler, ship the endpoint.
recall:
- What makes minimal APIs feel lighter than controllers?
- When might controllers still be the better fit?
```

```concept-card
id: async-enumerable-feature
term: IAsyncEnumerable Feature
parents:
- modern-dotnet-runtime-features
summary:
`IAsyncEnumerable<T>` is the runtime support for async streams that yield values over time.
details:
It is useful for streaming data and processing large result sets without materializing them all at once.
example:
Return an async stream of products or write streamed CSV rows as they are fetched.
mnemonic:
Yield later, not all at once.
recall:
- What problem does `IAsyncEnumerable<T>` solve well?
- Why is it especially useful for large responses?
```

```concept-card
id: source-generation
term: Source Generation
parents:
- modern-dotnet-runtime-features
summary:
Source generation emits helper code at build time so the runtime can avoid some reflection and repeated setup work.
details:
This improves performance, trimming, and AOT support, especially for JSON serialization and logging.
example:
`[JsonSerializable(typeof(ProductResponse))]` generates metadata used by `System.Text.Json`.
mnemonic:
Build it early, pay for it once.
recall:
- Why is source generation helpful in modern .NET APIs?
- Which runtime scenarios benefit beyond pure speed?
```

```concept-card
id: timeprovider
term: TimeProvider
parents:
- modern-dotnet-runtime-features
summary:
`TimeProvider` abstracts access to current time so time-sensitive code can be tested predictably.
details:
It replaces scattered direct use of `DateTime.UtcNow` with an injectable clock dependency.
example:
Inject `TimeProvider` into a discount service so a test can simulate Black Friday explicitly.
mnemonic:
Time is a dependency too.
recall:
- Why does `TimeProvider` improve testability?
- What kind of bugs become easier to avoid with an injectable time source?
```

```concept-card
id: modern-dotnet-platform-features
term: Modern .NET Platform Features
parents:
- modern-dotnet-runtime-features
summary:
Modern .NET platform features are the built-in framework capabilities that replace older custom or third-party solutions.
details:
Examples include stronger built-in OpenAPI support, better rate limiting, improved caching primitives, and platform-aligned Web API defaults.
example:
Choosing built-in OpenAPI and rate limiting can reduce external dependency surface in newer .NET versions.
mnemonic:
Prefer the platform when the platform is ready.
recall:
- Which kinds of newer platform features reduce custom infrastructure work?
- What should guide adoption of new runtime features in production?
```
