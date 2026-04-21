---
title: API Design and REST Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level API design trade-offs from the Web API interview question set.

Relevant concept maps:

- [Foundations Concept Map](api-design-rest-foundations.concept.md)
- [Advanced Concept Map](api-design-rest-advanced.concept.md)

## Creation Semantics

```interview-question
Your POST endpoint creates a resource but also triggers an asynchronous background job such as sending a welcome email. What HTTP status code should you return?
---
answer:
If the resource itself is created synchronously and the background work is only a side effect, return `201 Created` and include a `Location` header for the new resource.

If the whole operation is only accepted for later processing, return `202 Accepted` and point the client to a status endpoint.

Returning `200 OK` for resource creation hides the creation semantics and gives the client less useful information.
hints:
- Ask whether the resource already exists when the response is sent.
- One status means created now.
- The other means accepted for later processing.
```

Related concepts: [Write Operation Status Codes](api-design-rest-foundations.concept.md#write-operation-status-codes), [Async Request-Reply](api-design-rest-advanced.concept.md#async-request-reply)

```interview-choice
When the server queues the entire creation workflow for later processing, which status code is the best fit?
---
options:
- `200 OK`
- `201 Created`
- `202 Accepted`
correct: 2
explanation:
`202 Accepted` means the server accepted the request but will complete the actual processing later.
```

## Response Shaping

```interview-question
An API serves both a web frontend and a mobile app, but the mobile app needs less data. How would you handle that?
---
answer:
A practical default is to use separate DTOs or endpoint shapes for known consumers, such as a summary response for list views and a detailed response for richer screens.

Sparse fieldsets can work, but they add projection complexity and require careful field whitelisting. GraphQL can solve broad client-shaping needs, but it is often too heavy for only a few known consumers.

For most teams, explicit summary and detail endpoints are simpler to test, cache, and maintain.
hints:
- Do not return everything and let the client ignore it.
- Think summary versus detail models.
- Flexibility and simplicity trade off here.
```

Related concepts: [Consumer-Specific Responses](api-design-rest-foundations.concept.md#consumer-specific-responses), [Resource-Oriented Endpoint](api-design-rest-foundations.concept.md#resource-oriented-endpoint)

```interview-choice
Which approach is usually the clearest choice when you have two known consumers with different data needs?
---
options:
- Always return the full entity and let clients ignore fields
- Use separate DTOs or endpoints for summary and detail views
- Move immediately to GraphQL
correct: 1
explanation:
Separate DTOs or endpoints keep the contract explicit and predictable without adding dynamic query-building complexity.
```

## Versioning

```interview-question
You need to add API versioning to an API that is already in production. What is your approach?
---
answer:
Add explicit versioning without breaking current clients, usually by marking existing endpoints as `v1` and putting new or changed behavior in `v2`.

URL segment versioning such as `/api/v1/products` is often the clearest option for clients. Existing behavior should stay stable, older versions should be deprecated gradually, and the migration path should be documented.

Version the endpoints that changed, not the whole API by default.
hints:
- Existing clients must keep working.
- The version needs to be visible somewhere in the contract.
- Deprecation should be planned, not abrupt.
```

Related concepts: [API Versioning](api-design-rest-advanced.concept.md#api-versioning)

```interview-choice
Which versioning style is often the most explicit and easiest for clients to understand?
---
options:
- URL segment versioning
- Hidden header-only versioning by default
- No versioning, just change the endpoint
correct: 0
explanation:
URL segment versioning is highly visible and easy to reason about, especially for public or long-lived APIs.
```

## Content Negotiation

```interview-question
A client sends `Accept: application/xml`, but your API only supports JSON. What should happen?
---
answer:
The correct HTTP response is `406 Not Acceptable` because the client asked for a representation the server cannot provide.

ASP.NET Core does not enforce this strictly by default, so the API should be configured intentionally if it must honour content negotiation instead of always falling back to JSON.

If XML is not a real requirement, it is usually better not to add it just to be permissive.
hints:
- The issue is not the request body format.
- The client is asking for a response format.
- Think of the HTTP status that signals no acceptable representation.
```

Related concepts: [Content Negotiation](api-design-rest-advanced.concept.md#content-negotiation)

```interview-choice
Which configuration tells ASP.NET Core controllers to return `406` instead of silently defaulting to JSON?
---
options:
- `options.ReturnHttpNotAcceptable = true`
- `options.SuppressModelStateInvalidFilter = true`
- `options.AllowEmptyInputInBodyModelBinding = true`
correct: 0
explanation:
`ReturnHttpNotAcceptable = true` makes the framework enforce content negotiation when the requested media type is unsupported.
```

## Idempotent POST Requests

```interview-question
Your API needs idempotency for POST requests. How do you implement it?
---
answer:
Require an `Idempotency-Key` header, store the response for that key in a durable store such as Redis or a database, and return the stored response on duplicate requests instead of processing the operation again.

The key should expire after a reasonable window, and concurrent duplicate requests must be protected with a unique constraint or distributed lock so the operation still runs only once.

This is especially important for payments, orders, and other side-effecting operations.
hints:
- The mechanism usually starts with a header.
- The server needs durable memory of prior requests.
- Race conditions still matter.
```

Related concepts: [Idempotency Key](api-design-rest-advanced.concept.md#idempotency-key)

```interview-code
language: cs
prompt: Complete the endpoint so it reads an idempotency key from the request header.
starter:
app.MapPost("/api/orders", async (
    CreateOrderRequest request,
    [FromHeader(Name = "Idempotency-Key")] string idempotencyKey) =>
{
    return Results.Ok(idempotencyKey);
});
solution:
app.MapPost("/api/orders", async (
    CreateOrderRequest request,
    [FromHeader(Name = "Idempotency-Key")] string idempotencyKey) =>
{
    return Results.Ok(idempotencyKey);
});
checks:
- includes: Idempotency-Key
- includes: FromHeader
- includes: idempotencyKey
```
