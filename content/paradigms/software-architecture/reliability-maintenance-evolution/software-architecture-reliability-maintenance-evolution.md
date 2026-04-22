# Reliability, Maintenance, and Evolution

Architecture should survive not only peak traffic, but also failure, change, and long-term accumulation of complexity.

## Fault tolerance

Fault tolerance means the system can keep operating acceptably even when parts fail.

Typical strategies:

- retries with backoff
- timeouts
- circuit breakers
- redundancy
- queue-based buffering
- fallback behavior

## Graceful degradation

Graceful degradation means the system provides a reduced but still useful experience instead of total failure.

Examples:

- serving cached content when a dependency fails
- disabling recommendations while checkout still works
- reducing optional features under overload

## Technical debt

Technical debt is the future cost created by short-term decisions that make the system harder to change or reason about.

Architecture accumulates debt through:

- unclear boundaries
- duplicated logic
- hidden coupling
- poorly owned integration contracts
- outdated deployment assumptions

Managing it requires visibility, prioritization, and regular cleanup work, not only heroic rewrites.

## Debugging and diagnosis

Reliable systems are diagnosable.

Architecture should support debugging through:

- structured logging
- distributed tracing
- metrics and alerting
- correlation identifiers
- explicit failure handling

## Evolution over time

Healthy architecture evolves by changing boundaries gradually while keeping contracts stable enough for teams to move safely.

Useful practices include:

- incremental refactoring
- compatibility layers when needed
- ADRs for major decisions
- fitness checks and operational feedback

## Interview reminders

- reliability is partly about controlled failure behavior
- graceful degradation is often better than all-or-nothing failure
- technical debt is architectural when it slows safe change across the system
- diagnosability is a design concern, not a last-minute operations task
- evolution works better through deliberate small changes than delayed perfect rewrites
