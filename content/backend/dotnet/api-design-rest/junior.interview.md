---
title: API Design and REST Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level API design distinctions from the Web API interview question set.

Relevant concept maps:

- [Foundations Concept Map](api-design-rest-foundations.concept.md)
- [Advanced Concept Map](api-design-rest-advanced.concept.md)

## Resource Identification

```interview-question
In an HTTP API, what is the difference between route parameters and query string parameters, and when do you use each?
---
answer:
Route parameters identify a specific resource and are usually required for the URL to make sense, such as `/api/products/42`.

Query string parameters modify how a collection or resource is returned, such as filtering, sorting, or paging, for example `/api/products?category=electronics&sort=price`.

If removing the value makes the route meaningless, it usually belongs in the route. If removing it still leaves a valid default request, it usually belongs in the query string.
hints:
- One usually identifies the resource itself.
- The other usually modifies the result.
- Think `/api/products/42` versus `/api/products?sort=price`.
```

Related concepts: [Resource-Oriented Endpoint](api-design-rest-foundations.concept.md#resource-oriented-endpoint), [Route Parameters](api-design-rest-foundations.concept.md#route-parameters), [Query Parameters](api-design-rest-foundations.concept.md#query-parameters)

```interview-choice
Which URL best represents a request for a single product by its identifier?
---
options:
- `/api/products?id=42`
- `/api/products/42`
- `/api/products?filter=id:42`
correct: 1
explanation:
`/api/products/42` is the clearest resource-oriented route because the identifier is part of the resource path rather than an optional modifier.
```

## Binding Sources

```interview-question
What is the difference between `[FromBody]`, `[FromQuery]`, `[FromRoute]`, and `[FromHeader]` in ASP.NET Core, and when does it matter to specify them explicitly?
---
answer:
These attributes tell ASP.NET Core where to bind a parameter from.

`[FromRoute]` binds from URL path segments, `[FromQuery]` from the query string, `[FromBody]` from the request body, and `[FromHeader]` from HTTP headers.

It matters to specify them explicitly when inference would be ambiguous or misleading, especially for complex types in minimal APIs and for endpoints that need parameters from headers or query strings instead of the body.

Only one parameter can be bound from the body, so multiple body values should be wrapped in one DTO.
hints:
- Each attribute maps to a different request source.
- Complex types often need explicit intent.
- There is an important limitation around body binding.
```

Related concepts: [Parameter Binding Source](api-design-rest-foundations.concept.md#parameter-binding-source), [Route Parameters](api-design-rest-foundations.concept.md#route-parameters), [Query Parameters](api-design-rest-foundations.concept.md#query-parameters)

```interview-choice
Which binding source is the best fit for a correlation identifier sent as `X-Correlation-Id`?
---
options:
- `[FromRoute]`
- `[FromHeader]`
- `[FromBody]`
correct: 1
explanation:
Headers are request metadata, so a correlation identifier like `X-Correlation-Id` should be bound with `[FromHeader]`.
```

```interview-code
language: cs
prompt: Complete the endpoint so the `id` comes from the route and the search term comes from the query string.
starter:
app.MapGet("/api/products/{id}", (
    [FromRoute] int id,
    [FromQuery] string search) =>
{
    return Results.Ok(new { id, search });
});
solution:
app.MapGet("/api/products/{id}", (
    [FromRoute] int id,
    [FromQuery] string search) =>
{
    return Results.Ok(new { id, search });
});
checks:
- includes: [FromRoute]
- includes: [FromQuery]
- includes: id
- includes: search
```
