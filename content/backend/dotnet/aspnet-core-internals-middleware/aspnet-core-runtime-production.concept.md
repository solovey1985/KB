---
title: ASP.NET Core Runtime and Production Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the runtime and production-focused ASP.NET Core concepts from the middleware and internals interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Runtime Map

```concept-card
id: aspnet-core-runtime-governance
term: ASP.NET Core Runtime Governance
children:
- problemdetails
- exception-mapping
- forwarded-headers
- reverse-proxy-hosting
- response-header-injection
- options-pattern
- configuration-reload-semantics
- rate-limiting
- rate-limiting-algorithms
- managed-memory-leak-investigation
summary:
ASP.NET Core runtime governance covers the operational rules that keep APIs safe, diagnosable, and predictable in production.
details:
It includes error shaping, proxy awareness, configuration flow, runtime protection, and memory diagnostics.
example:
When a request fails in production, the app should return `ProblemDetails`, respect forwarded headers, and expose enough telemetry to diagnose the failure.
mnemonic:
Handle failures, trust infra correctly, protect runtime health.
recall:
- Which concerns belong to runtime governance rather than business logic?
- Why do these concepts become more important in production than in local development?
```

```concept-card
id: problemdetails
term: ProblemDetails
parents:
- aspnet-core-runtime-governance
children:
- exception-mapping
summary:
`ProblemDetails` is the standard HTTP error response shape used to return structured API failures.
details:
It provides a consistent contract for titles, status codes, and optional extensions, which makes API failures easier for clients to parse and debug.
example:
Both validation and server failures can use the same `ProblemDetails` shape while changing only `status`, `title`, and extensions.
mnemonic:
One error shape, many failure types.
recall:
- Why is `ProblemDetails` better than ad hoc error JSON?
- What kinds of fields should remain consistent across API failures?
```

```concept-card
id: exception-mapping
term: Exception Mapping
parents:
- problemdetails
summary:
Exception mapping converts different exception categories into the right HTTP status codes and `ProblemDetails` payloads.
details:
It keeps the API contract centralized and prevents controller code from being filled with repetitive try-catch blocks.
example:
Validation exceptions can map to `400`, business rule exceptions to `422`, and unhandled crashes to `500`.
mnemonic:
Map once, respond consistently.
recall:
- Why should exception-to-response rules live in one place?
- What is the difference between validation failures and unexpected crashes?
```

```concept-card
id: forwarded-headers
term: Forwarded Headers
parents:
- aspnet-core-runtime-governance
related:
- reverse-proxy-hosting
summary:
Forwarded headers carry the original client IP, scheme, and host through a reverse proxy to ASP.NET Core.
details:
Without them, the app may think every request came from the proxy and may mis-handle HTTPS, rate limiting, and logging.
example:
`X-Forwarded-For` and `X-Forwarded-Proto`
mnemonic:
Proxy speaks for the original request.
recall:
- Which client properties are commonly lost behind a proxy?
- Why does ASP.NET Core need explicit forwarded-header configuration?
```

```concept-card
id: reverse-proxy-hosting
term: Reverse Proxy Hosting
parents:
- aspnet-core-runtime-governance
related:
- forwarded-headers
summary:
Reverse proxy hosting means Kestrel runs behind infrastructure such as Nginx or Azure Application Gateway rather than being directly exposed to the internet.
details:
The proxy handles internet-facing concerns such as TLS termination, buffering, and edge routing, while Kestrel serves the application. This architecture is safer and more common in production.
mnemonic:
Proxy at the edge, app behind it.
recall:
- Why is Kestrel commonly hosted behind a reverse proxy?
- What responsibilities does the proxy often handle at the edge?
```

```concept-card
id: response-header-injection
term: Response Header Injection
parents:
- aspnet-core-runtime-governance
summary:
Response header injection is the practice of adding response metadata such as request IDs through middleware.
details:
Using `Response.OnStarting` ensures headers are added at the last safe moment before the response is sent, which avoids fragile timing bugs.
example:
`context.Response.OnStarting(() => { context.Response.Headers["X-Request-Id"] = requestId; return Task.CompletedTask; });`
mnemonic:
Set final headers at final time.
recall:
- Why is `OnStarting` safer than setting headers earlier?
- What useful metadata is often added to every response?
```

```concept-card
id: options-pattern
term: Options Pattern
parents:
- aspnet-core-runtime-governance
children:
- configuration-reload-semantics
summary:
The options pattern binds configuration into typed .NET objects that application code can consume safely.
details:
It makes configuration explicit, testable, and easier to validate than reading arbitrary keys throughout the codebase.
mnemonic:
Typed config beats scattered strings.
recall:
- Why is the options pattern better than reading configuration keys everywhere?
- What .NET abstractions expose options values at runtime?
```

```concept-card
id: configuration-reload-semantics
term: Configuration Reload Semantics
parents:
- options-pattern
summary:
Configuration reload semantics describe when changed configuration becomes visible to application code.
details:
`IOptions<T>` stays fixed after startup, `IOptionsSnapshot<T>` refreshes per request, and `IOptionsMonitor<T>` supports live updates for long-lived services.
mnemonic:
Fixed, per-request, or live.
recall:
- Which options abstraction refreshes per request?
- Which options abstraction works best in a singleton or background worker?
```

```concept-card
id: rate-limiting
term: Rate Limiting
parents:
- aspnet-core-runtime-governance
children:
- rate-limiting-algorithms
summary:
Rate limiting protects APIs by restricting how frequently or how concurrently clients can make requests.
details:
It reduces abuse, prevents overload, and helps preserve fair usage under pressure. Modern ASP.NET Core includes built-in rate limiting support.
mnemonic:
Protect capacity before it is gone.
recall:
- Why is rate limiting a production concern even for internal APIs?
- What types of problems does rate limiting mitigate?
```

```concept-card
id: rate-limiting-algorithms
term: Rate Limiting Algorithms
parents:
- rate-limiting
summary:
Rate limiting algorithms define how permits are issued and exhausted over time or across concurrent work.
details:
Fixed windows are simple, sliding windows smooth time boundaries, token buckets allow controlled bursts, and concurrency limiters focus on active simultaneous requests instead of time windows.
example:
Sliding window is a common default because it reduces burstiness at window edges.
mnemonic:
Choose the limit shape that matches the traffic shape.
recall:
- Which algorithm helps smooth fixed-window burst problems?
- When is a concurrency limiter more relevant than a request-per-minute limit?
```

```concept-card
id: managed-memory-leak-investigation
term: Managed Memory Leak Investigation
parents:
- aspnet-core-runtime-governance
summary:
Managed memory leak investigation is the process of finding object retention patterns that keep the .NET heap growing over time.
details:
It usually involves comparing dumps, identifying growing object types, and finding the references that keep them alive. Common causes include static caches without eviction, unsubscribed events, and lifetime mistakes.
example:
`dotnet-dump analyze`, `dumpheap -stat`, and `gcroot` are common analysis tools.
mnemonic:
Measure growth, find roots, remove retention.
recall:
- What should you confirm before calling something a memory leak?
- Which retention patterns often cause heap growth in Web APIs?
```
