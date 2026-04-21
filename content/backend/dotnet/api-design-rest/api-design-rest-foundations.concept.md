---
title: API Design and REST Foundations Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the foundational API design and REST concepts behind the junior and part of the middle and senior interview questions.

Study pages: [Section Index](index.md) | [Junior Questions](junior.interview.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Core Map

```concept-card
id: api-design-rest
term: API Design and REST
children:
- resource-oriented-endpoint
- route-parameters
- query-parameters
- parameter-binding-source
- pagination
- write-operation-status-codes
- partial-update
- consumer-specific-responses
summary:
API design and REST focus on clear resource modelling, predictable HTTP semantics, and contracts that clients can understand and evolve safely.
details:
Good API design is not only about returning JSON. It is about shaping URLs, parameters, status codes, payloads, and behaviors so that consumers can reason about the system without guessing.
example:
`GET /api/products/42` should feel more obvious than `GET /doThing?id=42` because the contract exposes the resource and operation clearly.
mnemonic:
Resource in the path, intent in the method, no surprises in the response.
recall:
- What kinds of design decisions make an API predictable?
- Why is HTTP semantics part of API design rather than an implementation detail?
```

```concept-card
id: resource-oriented-endpoint
term: Resource-Oriented Endpoint
parents:
- api-design-rest
children:
- route-parameters
- query-parameters
related:
- consumer-specific-responses
summary:
A resource-oriented endpoint uses URLs to identify resources clearly and uses HTTP semantics to describe operations on them.
details:
Paths should describe the resource space, while modifiers such as filters and sorting should stay outside the core identity of the resource. This keeps endpoints readable and makes contracts easier to document and maintain.
example:
`/api/products/42` identifies one product, while `/api/products?category=electronics` queries a collection.
mnemonic:
Path identifies, query qualifies.
recall:
- What should the path represent in a resource-oriented API?
- Why is `/api/products/42` clearer than `/api/products?id=42`?
```

```concept-card
id: route-parameters
term: Route Parameters
parents:
- resource-oriented-endpoint
related:
- query-parameters
- parameter-binding-source
summary:
Route parameters identify required values that are part of a resource path.
details:
They are used when the endpoint would be incomplete or ambiguous without the value, such as an entity identifier or another structural part of the route.
example:
`GET /api/orders/123`
mnemonic:
No route value, no resource.
recall:
- When does a parameter belong in the route?
- What is a common sign that an identifier should not be in the query string?
```

```concept-card
id: query-parameters
term: Query Parameters
parents:
- resource-oriented-endpoint
related:
- route-parameters
- pagination
- parameter-binding-source
summary:
Query parameters modify how a request is processed without changing the core identity of the endpoint.
details:
They are commonly used for filtering, sorting, paging, and other optional behaviors. The endpoint is still meaningful when they are omitted because default behavior still exists.
example:
`GET /api/products?category=electronics&sort=price&page=2`
mnemonic:
Query tunes, route names.
recall:
- What kinds of concerns usually belong in the query string?
- Why are query parameters normally optional modifiers?
```

```concept-card
id: parameter-binding-source
term: Parameter Binding Source
parents:
- api-design-rest
related:
- route-parameters
- query-parameters
summary:
Parameter binding source defines where ASP.NET Core reads a value from in an HTTP request.
details:
Common sources include route, query string, body, and headers. Being explicit matters when inference is ambiguous or when the intended source is important to the contract.
example:
`app.MapGet("/api/products/{id}", ([FromRoute] int id, [FromQuery] string? sort) => Results.Ok(new { id, sort }));`
mnemonic:
Know the source before you trust the value.
recall:
- Which request sources are most common in Web APIs?
- When should you specify binding attributes explicitly?
```

```concept-card
id: pagination
term: Pagination
parents:
- api-design-rest
children:
- offset-pagination
- cursor-pagination
- pagination-metadata
related:
- query-parameters
summary:
Pagination splits a large collection into smaller, navigable result slices.
details:
It protects the API and database from huge responses, improves client experience, and creates a stable contract for browsing large datasets.
example:
`GET /api/products?page=2&pageSize=20` returns only one slice instead of every product in the catalog.
mnemonic:
Big collection, small windows.
recall:
- Why is pagination a default expectation in production APIs?
- What information does a client usually need beyond the data itself?
```

```concept-card
id: offset-pagination
term: Offset Pagination
parents:
- pagination
related:
- cursor-pagination
- pagination-metadata
summary:
Offset pagination selects a page by skipping a number of items and taking the next page-sized slice.
details:
It is easy to understand and works well for simple cases, but performance degrades on large offsets and the results can shift when records are inserted or removed between requests.
example:
`GET /api/products?page=3&pageSize=20`
mnemonic:
Count past, then take next.
recall:
- What makes offset pagination simple?
- Why can large offsets become expensive or unstable?
```

```concept-card
id: cursor-pagination
term: Cursor Pagination
parents:
- pagination
related:
- offset-pagination
summary:
Cursor pagination uses a stable cursor such as the last seen identifier to fetch the next slice of data.
details:
It usually performs better on high-volume endpoints because it can continue from an indexed position rather than skipping a growing number of rows.
example:
`GET /api/products?cursor=abc123&pageSize=20`
mnemonic:
Resume from position, not from count.
recall:
- Why is cursor pagination often better for large collections?
- What kind of value usually makes a good cursor?
```

```concept-card
id: pagination-metadata
term: Pagination Metadata
parents:
- pagination
related:
- offset-pagination
summary:
Pagination metadata explains the state of the current page so the client does not have to infer it.
details:
Useful metadata includes page size, current page, whether next and previous pages exist, and sometimes total count or total pages. Total counts can be expensive on very large datasets, so they should be included intentionally.
example:
`currentPage`, `pageSize`, `hasNextPage`, `totalPages`
mnemonic:
Data tells what, metadata tells where.
recall:
- Why is pagination metadata useful to clients?
- Why might `totalCount` be omitted on high-volume endpoints?
```

```concept-card
id: write-operation-status-codes
term: Write Operation Status Codes
parents:
- api-design-rest
children:
- accepted-for-processing
summary:
Write operation status codes communicate whether a resource was created, updated, or only accepted for later processing.
details:
Using the right status code makes the API contract more expressive. `201 Created` means the resource exists now, `202 Accepted` means processing continues later, and update endpoints often choose between `200 OK` and `204 No Content` depending on whether a response body is returned.
example:
`POST /api/orders` can return `201 Created` with `Location: /api/orders/123`, while report generation can return `202 Accepted` with a status URL.
mnemonic:
Created means here, accepted means later.
recall:
- What does `201 Created` tell a client that `200 OK` does not?
- When is `202 Accepted` a better fit than `201 Created`?
```

```concept-card
id: accepted-for-processing
term: Accepted for Processing
parents:
- write-operation-status-codes
related:
- async-request-reply
summary:
Accepted for processing means the server received the request but will finish the work asynchronously.
details:
This pattern is common when the operation is queued or when completion takes too long for a normal request-response cycle. The API should normally give the client a status resource to check later.
example:
`202 Accepted` with `Location: /api/reports/abc123/status`
mnemonic:
Accepted means not done yet.
recall:
- Why is `202 Accepted` not the same as success completion?
- What extra information should usually accompany `202 Accepted`?
```

```concept-card
id: partial-update
term: Partial Update
aliases:
- PATCH update
parents:
- api-design-rest
related:
- consumer-specific-responses
summary:
Partial update changes only selected fields instead of replacing the full resource representation.
details:
Its main difficulty is representing client intent accurately, especially the difference between omitted fields and fields explicitly set to `null`. Teams often choose PATCH only when the extra complexity is justified.
example:
`app.MapPatch("/users/{id}", (int id, JsonPatchDocument<UserDto> patch) => Results.NoContent());`
mnemonic:
Missing means ignore, null may mean clear.
recall:
- What modelling problem makes PATCH tricky?
- When might a full `PUT` be a better trade-off?
```

```concept-card
id: consumer-specific-responses
term: Consumer-Specific Responses
parents:
- api-design-rest
related:
- resource-oriented-endpoint
summary:
Consumer-specific responses shape payloads around what a known client actually needs.
details:
Instead of always returning the full entity, APIs can use summary and detail DTOs or dedicated endpoints for different views. This reduces bandwidth, avoids accidental overexposure, and keeps contracts explicit.
example:
`GET /api/products` returns a summary DTO, while `GET /api/products/{id}` returns a detail DTO.
mnemonic:
Return needed data, not possible data.
recall:
- Why is returning everything usually a weak design choice?
- When are separate DTOs a better fit than sparse fieldsets?
```
