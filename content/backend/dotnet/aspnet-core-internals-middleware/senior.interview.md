---
title: ASP.NET Core Internals and Middleware Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level ASP.NET Core internals and middleware scenarios from the Web API interview question set.

Relevant concept maps:

- [Pipeline and DI Concept Map](aspnet-core-pipeline-di.concept.md)
- [Runtime and Production Concept Map](aspnet-core-runtime-production.concept.md)

## Captive Dependencies

```interview-question
You have a scoped service injected into a singleton. What happens, and how do you detect this before it reaches production?
---
answer:
This creates a captive dependency. The singleton captures a scoped service and keeps it alive longer than its intended request scope.

That can cause stale state, threading issues, improper disposal, and severe problems if the scoped service is something like `DbContext`.

In development, enable service provider validation with `ValidateScopes` and `ValidateOnBuild` so ASP.NET Core fails fast at startup instead of letting the bug appear at runtime.
hints:
- The problem is a lifetime mismatch.
- Think of a request-scoped service surviving forever.
- ASP.NET Core can detect this during startup when configured to validate.
```

Related concepts: [Captive Dependency](aspnet-core-pipeline-di.concept.md#captive-dependency), [Service Lifetime](aspnet-core-pipeline-di.concept.md#service-lifetime), [Scope Validation](aspnet-core-pipeline-di.concept.md#scope-validation)

```interview-choice
Which settings help catch captive dependencies during startup in development?
---
options:
- `ValidateScopes` and `ValidateOnBuild`
- `UseHttpsRedirection` and `UseHsts`
- `MapControllers` and `MapGet`
correct: 0
explanation:
`ValidateScopes` and `ValidateOnBuild` tell the container to validate service registrations and lifetime mismatches early.
```

## Structured Error Handling

```interview-question
Your API has a global exception handler, but you need different error formats for validation errors, business rule violations, and unexpected crashes. How do you structure this?
---
answer:
Use a centralized exception handling strategy that maps exception types to `ProblemDetails` responses.

Validation errors should return a client-fault response such as `400`, business rule violations often map to `422`, and truly unexpected failures map to `500` without exposing internal exception details.

This avoids try-catch duplication in controllers and produces one consistent error contract across the API.
hints:
- The key is central mapping, not repeating try-catch in each endpoint.
- Each class of failure should map to a different status and detail level.
- `ProblemDetails` is the standard response shape.
```

Related concepts: [ProblemDetails](aspnet-core-runtime-production.concept.md#problemdetails), [Exception Mapping](aspnet-core-runtime-production.concept.md#exception-mapping)

```interview-choice
Which status code is commonly used for a business rule violation that is validly formed but rejected by domain rules?
---
options:
- `200 OK`
- `422 Unprocessable Entity`
- `500 Internal Server Error`
correct: 1
explanation:
`422 Unprocessable Entity` is a common way to communicate that the request was well-formed but violated business constraints.
```

## Reverse Proxy Awareness

```interview-question
How does the ASP.NET Core request pipeline differ between Kestrel directly and Kestrel behind a reverse proxy such as Nginx or Azure Application Gateway? What do you need to configure?
---
answer:
Behind a reverse proxy, the original client IP, host, and scheme are forwarded through headers such as `X-Forwarded-For` and `X-Forwarded-Proto`.

If ASP.NET Core is not configured to trust and process those headers, the app can log the proxy IP instead of the client IP, break IP-based rate limiting, and produce HTTPS redirect loops because it thinks the incoming scheme is still HTTP.

Configure forwarded headers and trusted proxies early in the pipeline, before middleware that depends on client IP or scheme.
hints:
- The proxy changes what the app sees unless headers are processed.
- Scheme and client IP are the main practical issues.
- Middleware order matters here too.
```

Related concepts: [Forwarded Headers](aspnet-core-runtime-production.concept.md#forwarded-headers), [Reverse Proxy Hosting](aspnet-core-runtime-production.concept.md#reverse-proxy-hosting)

```interview-choice
What common production problem appears when forwarded headers are not configured correctly behind a reverse proxy?
---
options:
- JSON serialization stops working
- HTTPS redirect loops or incorrect client IP logging
- `IOptionsMonitor<T>` stops reloading
correct: 1
explanation:
Without forwarded headers, the app may believe every request is local HTTP from the proxy and can mis-handle redirects and request logging.
```

## Memory Leak Investigation

```interview-question
Your API's memory usage keeps growing over time and never drops. What is your debugging approach?
---
answer:
First confirm it is an actual leak or retention problem rather than normal allocation behavior by comparing memory dumps over time.

Then inspect common causes such as singletons holding scoped services, event handlers that were never unsubscribed, static collections that never evict entries, and large object heap pressure.

Use tools such as `dotnet-dump` and `dotnet-counters` to identify the growing object types and the roots keeping them alive.
hints:
- The first job is diagnosis, not restarting the service.
- Compare snapshots over time.
- Find what objects are growing and what still references them.
```

Related concepts: [Managed Memory Leak Investigation](aspnet-core-runtime-production.concept.md#managed-memory-leak-investigation)

```interview-choice
Which tool helps you inspect what object graph is keeping a managed object alive in a dump?
---
options:
- `gcroot`
- `git log`
- `dotnet restore`
correct: 0
explanation:
`gcroot` is used during dump analysis to identify the references that are preventing an object from being collected.
```
