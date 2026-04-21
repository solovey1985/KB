---
title: API Design and REST Advanced Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the more advanced API design concepts from the API Design and REST interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Advanced Map

```concept-card
id: api-contract-governance
term: API Contract Governance
children:
- bulk-operations
- api-versioning
- content-negotiation
- request-size-limits
- async-request-reply
- idempotency-key
summary:
API contract governance is the set of decisions that keep an API safe, evolvable, and predictable as usage grows.
details:
It covers changes over time, accepted formats, payload constraints, long-running workflows, and guarantees about retries and duplicate requests.
mnemonic:
Evolve safely, fail clearly, retry safely.
recall:
- What kinds of rules help an API stay predictable in production?
- Why do advanced API contracts go beyond routes and JSON payloads?
```

```concept-card
id: bulk-operations
term: Bulk Operations
parents:
- api-contract-governance
summary:
Bulk operations let a client create, update, or delete many items in one request.
details:
The contract should define the maximum batch size, performance expectations, and whether failures are all-or-nothing or item-by-item. Returning per-item outcomes is often better for client recovery when partial success is allowed.
example:
`POST /api/products/bulk`
mnemonic:
Batch size, failure rule, result per item.
recall:
- What decisions must a bulk endpoint define before implementation?
- Why is partial success often easier for clients to recover from?
```

```concept-card
id: api-versioning
term: API Versioning
parents:
- api-contract-governance
summary:
API versioning introduces new behavior without breaking existing clients.
details:
A practical approach is to keep existing behavior stable, introduce a new version only where behavior changes, and communicate deprecation timelines clearly. URL segment versioning is often the most visible option for clients.
example:
`/api/v1/products` and `/api/v2/products`
mnemonic:
Keep old clients working while new clients move.
recall:
- Why is changing a production endpoint in place risky?
- What makes URL versioning attractive to many teams?
```

```concept-card
id: content-negotiation
term: Content Negotiation
parents:
- api-contract-governance
summary:
Content negotiation is the process of choosing a response representation that matches what the client asked for.
details:
The client declares acceptable media types through headers such as `Accept`. If the API cannot produce any of them, it should return `406 Not Acceptable` when negotiation is being enforced.
example:
`Accept: application/xml` against a JSON-only API should lead to `406 Not Acceptable` when negotiation is enforced.
mnemonic:
Client asks for a format, server serves it or says no.
recall:
- Which request header drives response content negotiation?
- When should an API return `406 Not Acceptable`?
```

```concept-card
id: request-size-limits
term: Request Size Limits
parents:
- api-contract-governance
summary:
Request size limits cap how much data a client can send in a single request.
details:
They protect infrastructure from abuse, accidents, and oversized payloads. Limits should be aligned between Kestrel and any reverse proxy so clients see consistent behavior.
example:
`builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1_048_576);`
mnemonic:
Cap the payload before it caps the server.
recall:
- Why should size limits exist even on internal APIs?
- Why must the reverse proxy be configured too?
```

```concept-card
id: async-request-reply
term: Async Request-Reply
parents:
- api-contract-governance
related:
- accepted-for-processing
summary:
Async request-reply is a pattern where the server accepts a request immediately and lets the client check a status resource later.
details:
It is the right fit for long-running work such as report generation, imports, and heavy background workflows. The initial response usually returns `202 Accepted`, and a later status or download endpoint completes the interaction.
example:
`POST /api/reports -> 202 Accepted -> Location: /api/reports/abc123/status`
mnemonic:
Accept, track, complete.
recall:
- Why is this pattern better than increasing the HTTP timeout?
- What should the client receive after the initial request?
```

```concept-card
id: idempotency-key
term: Idempotency Key
parents:
- api-contract-governance
summary:
An idempotency key lets a client retry a side-effecting request without accidentally performing the operation twice.
details:
The client sends a unique key, and the server stores the resulting response. If the same key is received again for the same operation, the server returns the stored result instead of reprocessing it.
example:
`[FromHeader(Name = "Idempotency-Key")] string idempotencyKey`
mnemonic:
Same key, same side effect, same response.
recall:
- Why are idempotency keys especially important for payments and orders?
- What server-side store is needed to make the key useful?
```
