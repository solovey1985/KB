---
title: API Design and REST Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level API design scenarios from the Web API interview question set.

Relevant concept maps:

- [Foundations Concept Map](api-design-rest-foundations.concept.md)
- [Advanced Concept Map](api-design-rest-advanced.concept.md)

## Pagination

```interview-question
Your API returns a list of products and a client asks for pagination. How do you implement it, and what metadata do you include in the response?
---
answer:
For simple use cases, start with offset pagination using parameters like `page` and `pageSize`, and return pagination metadata so the client can understand the current slice.

Useful metadata includes current page, page size, total pages, and whether next or previous pages exist. For large tables, `totalCount` can be expensive because it usually requires a separate count query.

For high-volume endpoints, cursor-based pagination is often better because it uses a stable cursor such as the last seen identifier and avoids the cost and instability of large offsets.
hints:
- `Skip` and `Take` are only part of the answer.
- The client should not have to guess the paging state.
- Large offsets and total counts have performance costs.
```

Related concepts: [Pagination](api-design-rest-foundations.concept.md#pagination), [Offset Pagination](api-design-rest-foundations.concept.md#offset-pagination), [Cursor Pagination](api-design-rest-foundations.concept.md#cursor-pagination), [Pagination Metadata](api-design-rest-foundations.concept.md#pagination-metadata)

```interview-choice
Which statement best explains why cursor pagination is often preferred on large, high-volume endpoints?
---
options:
- It removes the need for ordering entirely
- It usually avoids expensive large-offset scans and works better with indexed seeks
- It guarantees an exact total count for every page
correct: 1
explanation:
Cursor pagination usually performs better at scale because it continues from a stable cursor rather than skipping a growing number of rows.
```

## Partial Updates

```interview-question
A client sends a PATCH request to update only the email field of a user. How do you implement partial updates safely?
---
answer:
The core problem is distinguishing between a field that is missing and a field that is present with a `null` value.

You can use JSON Patch, but many teams prefer a DTO model that tracks which fields were actually sent, such as an `Optional<T>` style wrapper or request-body inspection. That lets `null` mean clear the field while omission means leave it unchanged.

If the entity is small and partial updates add more complexity than value, using a full `PUT` model can be the simpler and safer choice.
hints:
- `null` and missing are not the same thing.
- The client might want to clear a field, not ignore it.
- PATCH is sometimes more complexity than the domain needs.
```

Related concepts: [Partial Update](api-design-rest-foundations.concept.md#partial-update)

```interview-choice
What is the main modelling problem with a naive PATCH DTO that only checks for `null`?
---
options:
- It cannot be serialized to JSON
- It cannot distinguish a missing field from a field explicitly set to `null`
- It prevents validation from running
correct: 1
explanation:
The main risk is collapsing two different client intentions into the same value, which leads to incorrect updates.
```

## Bulk Operations

```interview-question
Your API must support creating 500 products in a single request. How would you design the endpoint?
---
answer:
Use a dedicated bulk endpoint such as `POST /api/products/bulk` and define how failures behave up front.

An all-or-nothing approach wraps the batch in a transaction and rejects the entire request if one item fails. A partial-success approach processes items independently and returns per-item errors so the client can fix only the failures.

You should also enforce a maximum batch size and use efficient bulk persistence instead of saving each item one by one.
hints:
- Batch size and failure semantics are part of the contract.
- One bad item should not be an accidental design decision.
- Performance matters as much as correctness here.
```

Related concepts: [Bulk Operations](api-design-rest-advanced.concept.md#bulk-operations)

```interview-choice
What is the main advantage of a partial-success bulk API response?
---
options:
- The client can fix only failed items instead of resubmitting the whole batch
- It guarantees full transactional consistency across all items
- It removes the need for validation
correct: 0
explanation:
Partial success can be much friendlier because the client receives item-level outcomes instead of losing all successful work because one item failed.
```

## Request Size Limits

```interview-question
Your API accepts a JSON body, but you want to enforce a maximum request size of 1 MB. How do you do that, and what happens when the limit is exceeded?
---
answer:
Set a global or per-endpoint request body limit in Kestrel and align that limit with any reverse proxy in front of the app.

When the limit is exceeded, the server should return `413 Payload Too Large`. In practice, teams often add middleware or standardized error handling so that oversized requests still produce a consistent API error format.

This is both a resource-protection and abuse-prevention concern.
hints:
- Kestrel and the reverse proxy both matter.
- The status code is specific to oversized payloads.
- The goal is not only validation but also protection.
```

Related concepts: [Request Size Limits](api-design-rest-advanced.concept.md#request-size-limits)

```interview-code
language: cs
prompt: Complete the Kestrel configuration so the maximum request body size is set to 1 MB.
starter:
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 
});
solution:
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1_048_576;
});
checks:
- includes: MaxRequestBodySize
- includes: 1_048_576
```

## Long-Running Processes

```interview-question
You need an endpoint that triggers a long-running process such as report generation, and it takes several minutes. How do you design the API?
---
answer:
Do not keep the client waiting on a single long HTTP request. Use the async request-reply pattern instead.

The initial request should return `202 Accepted` and a `Location` header or status endpoint. The long-running work runs in a background service or queue, while the client polls the status endpoint or receives a push notification when the work completes.

The final status response should make it clear whether the job is still running, completed successfully, or failed.
hints:
- Long request timeouts are not the right design.
- The client needs a way to check progress later.
- Think background processing plus a status resource.
```

Related concepts: [Async Request-Reply](api-design-rest-advanced.concept.md#async-request-reply), [Write Operation Status Codes](api-design-rest-foundations.concept.md#write-operation-status-codes)

```interview-choice
Which status code is the best fit for the initial response to a long-running report generation request?
---
options:
- `200 OK`
- `202 Accepted`
- `204 No Content`
correct: 1
explanation:
`202 Accepted` tells the client the server has accepted the request and that processing will continue asynchronously.
```
