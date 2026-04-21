---
title: ASP.NET Core Internals and Middleware Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level ASP.NET Core internals and middleware trade-offs from the Web API interview question set.

Relevant concept maps:

- [Pipeline and DI Concept Map](aspnet-core-pipeline-di.concept.md)
- [Runtime and Production Concept Map](aspnet-core-runtime-production.concept.md)

## Pipeline Composition

```interview-question
You need request logging, authentication, rate limiting, CORS, and exception handling middleware. In what order do you register them, and what breaks if you get it wrong?
---
answer:
Exception handling should be very early so it can catch failures from later middleware. CORS must run before authentication so browser preflight requests do not fail with authorization errors. Rate limiting should usually run before authentication to protect expensive auth work. Authentication must run before authorization. Endpoint mapping comes after the middleware pipeline is prepared.

Wrong order causes subtle production bugs, such as browsers failing CORS preflights, missing ProblemDetails responses, or attackers still reaching the authentication layer before rate limiting applies.
hints:
- The request flows top to bottom.
- CORS and authentication order matters especially for browsers.
- Exception handling cannot catch failures that happen before it.
```

Related concepts: [Middleware Pipeline](aspnet-core-pipeline-di.concept.md#middleware-pipeline), [Middleware Order](aspnet-core-pipeline-di.concept.md#middleware-order), [CORS Placement](aspnet-core-pipeline-di.concept.md#cors-placement)

```interview-choice
What commonly breaks when CORS runs after authentication?
---
options:
- EF Core tracking stops working
- Browser preflight requests can fail with `401` or misleading auth errors
- The app cannot bind route parameters
correct: 1
explanation:
CORS must run before authentication so browser preflight requests are handled correctly rather than being blocked as unauthorized.
```

## Filters Versus Middleware

```interview-question
What is the difference between middleware and endpoint filters in ASP.NET Core, and when would you use each?
---
answer:
Middleware runs in the global HTTP pipeline and works at the raw request-response level for every request that passes through it.

Endpoint filters run only on matched endpoints and can inspect typed arguments and results for those endpoints.

Use middleware for broad cross-cutting concerns such as exception handling, CORS, logging, and authentication. Use endpoint filters when the behavior is endpoint-specific and needs access to the bound parameters.
hints:
- One sees the whole pipeline.
- The other only runs after an endpoint is matched.
- Typed argument access is the key difference.
```

Related concepts: [Middleware Pipeline](aspnet-core-pipeline-di.concept.md#middleware-pipeline), [Endpoint Filters](aspnet-core-pipeline-di.concept.md#endpoint-filters)

```interview-choice
Which mechanism has typed access to endpoint arguments?
---
options:
- Middleware
- Endpoint filters
- `IOptionsMonitor<T>`
correct: 1
explanation:
Endpoint filters can inspect bound endpoint arguments directly, which middleware cannot do in the same typed way.
```

## Duplicate Registrations

```interview-question
What happens when you call `builder.Services.AddScoped<IProductService, ProductService>()` twice with different implementations?
---
answer:
The last registration wins when resolving a single `IProductService`, but the earlier registration is still kept in the container.

If you inject `IEnumerable<IProductService>`, you get all registered implementations in registration order. This is useful for patterns such as decorators or chains, but it can also hide accidental duplicate registrations.

If you want to avoid overwriting an existing registration, use `TryAdd*` methods.
hints:
- The container does not throw by default.
- Single resolution and enumerable resolution behave differently.
- There is a helper API to avoid accidental override.
```

Related concepts: [Multiple Registrations](aspnet-core-pipeline-di.concept.md#multiple-registrations)

```interview-choice
Which API helps register a service only if it is not already registered?
---
options:
- `TryAddScoped`
- `AddScoped`
- `ReplaceScoped`
correct: 0
explanation:
`TryAddScoped` prevents overwriting an existing registration and is commonly used by library authors.
```

## Response Headers

```interview-question
You need to add a custom header such as `X-Request-Id` to every API response. What is the best approach?
---
answer:
Use middleware and register the header with `context.Response.OnStarting(...)`.

`OnStarting` is safer than setting the header before `await next()` because it runs right before the response headers are sent. That avoids cases where downstream middleware changes the response and overwrites earlier header state.

The same request identifier should also be included in logging scope for traceability.
hints:
- The timing of response header mutation matters.
- The response may still be modified later in the pipeline.
- There is a callback designed specifically for the final header phase.
```

Related concepts: [Response Header Injection](aspnet-core-runtime-production.concept.md#response-header-injection)

```interview-code
language: cs
prompt: Complete the middleware so it sets `X-Request-Id` using `OnStarting`.
starter:
app.Use(async (context, next) =>
{
    var requestId = context.TraceIdentifier;
    context.Response.OnStarting(() =>
    {
        context.Response.Headers["X-Request-Id"] = requestId;
        return Task.CompletedTask;
    });

    
});
solution:
app.Use(async (context, next) =>
{
    var requestId = context.TraceIdentifier;
    context.Response.OnStarting(() =>
    {
        context.Response.Headers["X-Request-Id"] = requestId;
        return Task.CompletedTask;
    });

    await next();
});
checks:
- includes: OnStarting
- includes: X-Request-Id
- includes: await next()
```

## Options Pattern

```interview-question
What is the difference between `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>`? When do you use each?
---
answer:
`IOptions<T>` is a singleton-style view that reads configuration once and does not update dynamically.

`IOptionsSnapshot<T>` is scoped and refreshes per request, which makes it useful for request-scoped services that should observe configuration changes.

`IOptionsMonitor<T>` is singleton-safe and supports live updates, so it is the right choice for long-lived services such as background workers or other singletons.
hints:
- One is static after startup.
- One refreshes per request.
- One works well inside singletons with live updates.
```

Related concepts: [Options Pattern](aspnet-core-runtime-production.concept.md#options-pattern), [Configuration Reload Semantics](aspnet-core-runtime-production.concept.md#configuration-reload-semantics)

```interview-choice
Which option type is usually the right choice inside a background service?
---
options:
- `IOptionsSnapshot<T>`
- `IOptionsMonitor<T>`
- `IEnumerable<T>`
correct: 1
explanation:
Background services are long-lived and often singleton-scoped, so `IOptionsMonitor<T>` is the right fit for current values and change notifications.
```

## Rate Limiting

```interview-question
How would you implement rate limiting in a .NET API, and what algorithm would you choose?
---
answer:
Use the built-in rate limiting middleware and choose the algorithm based on the traffic shape you want to allow.

Fixed windows are simple, sliding windows smooth out boundary bursts, token buckets allow controlled bursts, and concurrency limiters restrict simultaneous work instead of requests over time.

In many APIs, sliding window is a sensible default because it avoids the burstiness of fixed windows while staying simpler than custom solutions.
hints:
- The answer is not only middleware setup but also algorithm selection.
- Boundary bursts are a weakness of one common strategy.
- Not every rate limiter counts requests per minute.
```

Related concepts: [Rate Limiting](aspnet-core-runtime-production.concept.md#rate-limiting), [Rate Limiting Algorithms](aspnet-core-runtime-production.concept.md#rate-limiting-algorithms)

```interview-choice
Which algorithm is often preferred over a fixed window because it smooths boundary bursts?
---
options:
- Sliding window
- Concurrency limiter
- `Task.WhenAll`
correct: 0
explanation:
Sliding window spreads permits across segments of time, which reduces the burst problem at window boundaries.
```
