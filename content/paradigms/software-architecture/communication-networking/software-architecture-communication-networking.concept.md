---
title: Communication and Networking Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the communication styles and network realities that matter in architecture.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-communication-networking.md) | [Interview Practice](software-architecture-communication-networking.interview.md)

## Communication Map

```concept-card
id: api-communication
term: API Communication
children:
- rest
- graphql
- websocket
- network-latency
summary:
API communication is how system parts and clients exchange data and commands across boundaries.
details:
The chosen style affects contract shape, latency, client flexibility, and operational behavior.
example:
The same backend may expose REST for public APIs and WebSockets for live updates.
mnemonic:
Boundary style shapes system behavior.
recall:
- Why is communication style an architectural choice?
- Which trade-offs come from each API style?
```

```concept-card
id: rest
term: REST
parents:
- api-communication
summary:
REST is a resource-oriented API style built around HTTP semantics, stateless requests, and predictable contracts.
details:
It works well for broad interoperability and conventional CRUD-style interactions, especially when HTTP caching and standard semantics are useful.
example:
`GET /orders/123` and `POST /orders` are typical REST endpoints.
mnemonic:
Resources over HTTP with standard verbs.
recall:
- Why is statelessness important in REST?
- What makes REST easy to adopt across teams and tools?
```

```concept-card
id: graphql
term: GraphQL
parents:
- api-communication
summary:
GraphQL is a typed query interface that lets clients ask for exactly the data shape they need.
details:
It can reduce over-fetching and fit complex client screens, but it adds query-cost, caching, and authorization complexity.
example:
A client requests only `id`, `name`, and `price` fields for products in one GraphQL query.
mnemonic:
Client asks for the shape it needs.
recall:
- What problem does GraphQL solve well?
- Why can GraphQL be harder to secure and cache?
```

```concept-card
id: websocket
term: WebSocket
parents:
- api-communication
summary:
WebSocket provides persistent bidirectional communication between client and server.
details:
It is useful for realtime experiences, but it introduces connection lifecycle and scaling considerations beyond normal request-response APIs.
example:
A trading dashboard uses WebSocket updates to stream price changes.
mnemonic:
Open connection, live updates.
recall:
- When is WebSocket a better fit than polling?
- What extra operational concerns come with long-lived connections?
```

```concept-card
id: network-latency
term: Network Latency
parents:
- api-communication
summary:
Network latency is the time cost of communication across a network boundary.
details:
It accumulates quickly in chatty architectures, which is why remote calls must be treated as expensive compared with in-process calls.
example:
One endpoint that fans out to five synchronous downstream services can build noticeable latency quickly.
mnemonic:
Every remote hop has a cost.
recall:
- Why are chatty distributed calls dangerous?
- What design choices reduce latency sensitivity?
```
