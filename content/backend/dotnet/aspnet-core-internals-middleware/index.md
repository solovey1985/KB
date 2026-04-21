# ASP.NET Core Internals and Middleware

This section turns the ASP.NET Core internals and middleware material from the `.NET Web API Interview Questions` PDF into focused study pages.

## Interview Practice By Level

- [Junior Questions](junior.interview.md)
- [Middle Questions](middle.interview.md)
- [Senior Questions](senior.interview.md)

## Concept Maps

- [Pipeline and DI Concept Map](aspnet-core-pipeline-di.concept.md)
- [Runtime and Production Concept Map](aspnet-core-runtime-production.concept.md)

## Study Flow

1. Start with [Junior Questions](junior.interview.md) for DI and endpoint-mapping basics.
2. Read the [Pipeline and DI Concept Map](aspnet-core-pipeline-di.concept.md) before the middle-level middleware questions.
3. Use [Middle Questions](middle.interview.md) for order, filters, options, and rate limiting.
4. Finish with [Senior Questions](senior.interview.md) and the [Runtime and Production Concept Map](aspnet-core-runtime-production.concept.md).

## Related Topics

- [API Design and REST](../api-design-rest/index.md)
- [Performance and Caching](../performance-caching/index.md)
- [.NET Backend Study Index](../index.md)

## Topic Coverage

- middleware order
- dependency injection and service lifetimes
- captive dependencies
- middleware versus endpoint filters
- exception handling with ProblemDetails
- duplicate registrations and `IEnumerable<T>` resolution
- reverse proxy and forwarded headers
- response headers and `OnStarting`
- memory leak investigation
- `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>`
- rate limiting algorithms and policies
- minimal APIs versus controllers mapping
