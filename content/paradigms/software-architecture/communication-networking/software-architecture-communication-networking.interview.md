---
title: Communication and Networking Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about API style and network behavior.

Relevant concept maps:

- [Concept Map](software-architecture-communication-networking.concept.md)

## Communication Styles

```interview-question
What are the main RESTful API design principles?
---
answer:
RESTful APIs usually emphasize resource-oriented endpoints, standard HTTP verbs and status codes, stateless requests, and clear predictable contracts.

They work well when broad interoperability and conventional web semantics are important.
hints:
- Think resources and HTTP semantics.
- Statelessness matters.
- Predictable contracts should appear.
```

```interview-question
What should architecture consider when designing a GraphQL API?
---
answer:
Architecture should consider schema design, authorization, query complexity limits, caching behavior, resolver performance, and how the GraphQL layer maps to backend services.

GraphQL gives clients flexibility, but that flexibility must be governed carefully.
hints:
- Client flexibility is the benefit.
- Query cost is a risk.
- Auth and caching get harder.
```

```interview-question
When is WebSocket communication preferred?
---
answer:
WebSocket is preferred when the system needs low-latency, bidirectional, realtime communication, such as chat, collaborative editing, or live dashboards.

It is less useful when simple request-response APIs are enough.
hints:
- Realtime push is the clue.
- Bidirectional matters.
- Not every API needs a persistent connection.
```

```interview-question
How can network latency impact architecture, and how is it mitigated?
---
answer:
Network latency makes remote calls slower and less predictable than local calls, so chatty service interactions can severely hurt responsiveness.

Mitigations include caching, batching, reducing synchronous hops, colocating dependent systems when sensible, and using asynchronous workflows for non-immediate work.
hints:
- Remote calls are expensive.
- Chatty interactions are a warning sign.
- Batching and caching are useful mitigations.
```
