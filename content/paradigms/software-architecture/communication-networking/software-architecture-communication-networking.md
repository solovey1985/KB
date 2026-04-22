# Communication and Networking

Architectural communication choices determine latency, coupling, compatibility, and operational complexity.

## REST

RESTful APIs emphasize resource-oriented endpoints, standard HTTP semantics, statelessness, and predictable contracts.

REST works well for broad interoperability and conventional service APIs.

## GraphQL

GraphQL allows clients to ask for exactly the fields they need through a typed schema.

It is powerful for flexible client-driven data access, but can complicate authorization, caching, and query cost control.

## WebSockets and realtime communication

WebSockets support persistent bidirectional communication.

They are useful for:

- live dashboards
- chat
- collaborative editing
- fast event push to clients

They add connection-management complexity and do not replace every normal request-response API.

## Network latency

Latency shapes architecture because remote calls are slower and less reliable than in-process calls.

Ways to reduce latency impact include:

- caching
- batching
- reducing chatty protocols
- colocating dependent services when appropriate
- async workflows when strict immediacy is not required

## Interview reminders

- REST and GraphQL optimize different interaction styles
- realtime communication is useful when push matters more than simple request-response
- network boundaries are expensive compared with local method calls
- reducing chatty cross-service calls is often an architectural win
