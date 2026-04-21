---
title: ASP.NET Core Pipeline and DI Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core ASP.NET Core pipeline and dependency injection concepts behind the junior and part of the middle and senior interview questions.

Study pages: [Section Index](index.md) | [Junior Questions](junior.interview.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Core Map

```concept-card
id: aspnet-core-request-processing
term: ASP.NET Core Request Processing
children:
- middleware-pipeline
- dependency-injection
- minimal-api-mapping
- controllers-mapping
summary:
ASP.NET Core request processing combines the HTTP pipeline, dependency injection, and endpoint mapping into one execution model.
details:
Requests enter the middleware pipeline, use container-managed services, and eventually reach a mapped endpoint. Understanding those three pieces explains most framework behavior in APIs.
example:
An incoming request can hit exception handling, CORS, authentication, authorization, and finally a mapped minimal API or controller action.
mnemonic:
Pipeline runs, container supplies, endpoint answers.
recall:
- What three framework parts shape a typical request in ASP.NET Core?
- Why do middleware and DI need to be understood together?
```

```concept-card
id: middleware-pipeline
term: Middleware Pipeline
parents:
- aspnet-core-request-processing
children:
- middleware-order
- endpoint-filters
- cors-placement
summary:
The middleware pipeline is the ordered chain of components that process each HTTP request and response.
details:
Each middleware can inspect the `HttpContext`, do work before and after the next component, or short-circuit the pipeline entirely.
example:
`app.UseExceptionHandler(); app.UseCors(); app.UseAuthentication(); app.UseAuthorization();`
mnemonic:
Each step can pass, wrap, or stop.
recall:
- What can middleware do besides calling the next component?
- Why is the middleware chain sensitive to ordering?
```

```concept-card
id: middleware-order
term: Middleware Order
parents:
- middleware-pipeline
related:
- cors-placement
- forwarded-headers
summary:
Middleware order determines which components see the request first and which components can wrap or protect later stages.
details:
Placing middleware in the wrong order can cause broken preflight requests, missing exception handling, incorrect auth behavior, or ineffective protection layers.
mnemonic:
Right pieces, wrong order, wrong behavior.
recall:
- Why is order part of middleware correctness rather than a style issue?
- Which kinds of bugs are caused by incorrect middleware ordering?
```

```concept-card
id: cors-placement
term: CORS Placement
parents:
- middleware-pipeline
related:
- middleware-order
summary:
CORS placement defines where cross-origin policy checks happen in the pipeline.
details:
CORS usually needs to run before authentication and authorization so browser preflight requests are handled correctly and do not appear as misleading auth failures.
example:
`app.UseCors(); app.UseAuthentication(); app.UseAuthorization();`
mnemonic:
Let the browser ask before auth answers.
recall:
- Why does CORS often need to run before authentication?
- What common symptom appears when this order is wrong?
```

```concept-card
id: endpoint-filters
term: Endpoint Filters
parents:
- middleware-pipeline
related:
- minimal-api-mapping
summary:
Endpoint filters are endpoint-scoped pipeline components for minimal APIs that can inspect bound arguments and results.
details:
Unlike middleware, endpoint filters only run for matched endpoints and have typed access to endpoint arguments. They are useful for validation and per-endpoint behaviors.
example:
`group.MapGet("/{id}", handler).AddEndpointFilter(...)`
mnemonic:
Middleware sees requests, filters see endpoint intent.
recall:
- What makes endpoint filters different from middleware?
- When are endpoint filters a better fit than global middleware?
```

```concept-card
id: dependency-injection
term: Dependency Injection
parents:
- aspnet-core-request-processing
children:
- service-lifetime
- captive-dependency
- scope-validation
- multiple-registrations
summary:
Dependency injection is the container-based mechanism ASP.NET Core uses to supply services to application code.
details:
It improves testability, lifetime management, and configurability by making dependencies explicit and centrally registered.
example:
`public class OrderService(IEmailSender email, AppDbContext db)` lets the container supply both dependencies instead of constructing them manually.
mnemonic:
Ask for dependencies, do not build them.
recall:
- Why does ASP.NET Core rely on dependency injection pervasively?
- What framework benefits come from central registration of services?
```

```concept-card
id: service-lifetime
term: Service Lifetime
parents:
- dependency-injection
children:
- singleton-lifetime
- scoped-lifetime
- transient-lifetime
summary:
Service lifetime controls how long a registered dependency instance lives and how widely it is shared.
details:
The main lifetimes are singleton, scoped, and transient. Picking the wrong lifetime can cause stale state, contention, or unnecessary allocations.
mnemonic:
How long should this instance live?
recall:
- What decision does service lifetime actually control?
- Why is lifetime selection a correctness issue, not only a performance issue?
```

```concept-card
id: singleton-lifetime
term: Singleton Lifetime
parents:
- service-lifetime
related:
- captive-dependency
summary:
Singleton lifetime creates one instance for the whole application lifetime.
details:
Singletons are good for stateless shared services or caches, but they must not directly hold scoped state from requests.
mnemonic:
One app, one instance.
recall:
- What kinds of services are good singleton candidates?
- Why is a scoped dependency dangerous inside a singleton?
```

```concept-card
id: scoped-lifetime
term: Scoped Lifetime
parents:
- service-lifetime
summary:
Scoped lifetime creates one instance per request scope in a normal Web API.
details:
Scoped services fit request-level work such as `DbContext`, where each request should have its own isolated unit of work.
mnemonic:
One request, one instance.
recall:
- Why is `DbContext` usually scoped?
- What boundary does a scoped service normally follow in a Web API?
```

```concept-card
id: transient-lifetime
term: Transient Lifetime
parents:
- service-lifetime
summary:
Transient lifetime creates a new instance each time the service is resolved.
details:
It fits lightweight stateless services that do not need to be shared, but overuse can increase allocation and construction cost.
mnemonic:
New request for every resolution.
recall:
- When is transient appropriate?
- What is the trade-off of using transient everywhere?
```

```concept-card
id: captive-dependency
term: Captive Dependency
parents:
- dependency-injection
related:
- singleton-lifetime
- scoped-lifetime
summary:
A captive dependency happens when a longer-lived service captures a shorter-lived service, usually a singleton holding a scoped dependency.
details:
This breaks intended lifetimes and can cause stale data, threading bugs, improper disposal, and database access problems.
mnemonic:
Long life trapped short life.
recall:
- What lifetime mismatch creates a captive dependency?
- Why is this especially dangerous with `DbContext`?
```

```concept-card
id: scope-validation
term: Scope Validation
parents:
- dependency-injection
related:
- captive-dependency
summary:
Scope validation makes ASP.NET Core check for invalid service lifetime relationships during startup.
details:
Using `ValidateScopes` and `ValidateOnBuild` helps detect captive dependencies and other container misconfigurations before they reach runtime.
example:
`builder.Host.UseDefaultServiceProvider(options => { options.ValidateScopes = true; options.ValidateOnBuild = true; });`
mnemonic:
Fail early, not under traffic.
recall:
- What problem does scope validation catch well?
- Why is startup validation valuable in development?
```

```concept-card
id: multiple-registrations
term: Multiple Registrations
parents:
- dependency-injection
summary:
Multiple registrations happen when the same service type is added to the container more than once.
details:
Resolving a single service usually returns the last registration, while resolving `IEnumerable<T>` returns all implementations. This can be intentional for decorators or accidental and confusing.
mnemonic:
One type, many registrations, know the resolution rule.
recall:
- What happens when a service is registered twice?
- How does `IEnumerable<T>` resolution differ from single service resolution?
```

```concept-card
id: minimal-api-mapping
term: Minimal API Mapping
parents:
- aspnet-core-request-processing
related:
- endpoint-filters
- controllers-mapping
summary:
Minimal API mapping defines endpoints directly in code with methods such as `MapGet` and `MapPost`.
details:
This approach is explicit, lightweight, and integrates naturally with endpoint filters and route groups.
example:
`app.MapGet("/api/products", () => "Hello");`
mnemonic:
Map the verb, map the handler.
recall:
- What makes minimal API mapping explicit?
- Which methods are commonly used to define minimal API endpoints?
```

```concept-card
id: controllers-mapping
term: Controllers Mapping
parents:
- aspnet-core-request-processing
related:
- minimal-api-mapping
summary:
Controllers mapping activates controller-based endpoints discovered through MVC conventions and attributes.
details:
It is the traditional ASP.NET Core API model and remains useful for teams or codebases that prefer controller organization and MVC features.
example:
`app.MapControllers();`
mnemonic:
Discover the controller, honor its routes.
recall:
- What does `MapControllers()` enable?
- How does controller mapping differ from minimal API mapping?
```
