---
title: Reliability, Maintenance, and Evolution Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the concepts behind architecture longevity and controlled failure behavior.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-reliability-maintenance-evolution.md) | [Interview Practice](software-architecture-reliability-maintenance-evolution.interview.md)

## Reliability Map

```concept-card
id: fault-tolerance
term: Fault Tolerance
children:
- graceful-degradation
- technical-debt
summary:
Fault tolerance is the ability of a system to keep functioning acceptably even when components fail.
details:
It depends on failure detection, isolation, fallback behavior, and patterns such as retries, timeouts, and circuit breakers.
example:
If the recommendation engine fails, checkout can still proceed because the dependency is isolated.
mnemonic:
Parts fail, system still works.
recall:
- What patterns improve fault tolerance?
- Why is isolation as important as redundancy?
```

```concept-card
id: graceful-degradation
term: Graceful Degradation
parents:
- fault-tolerance
summary:
Graceful degradation means delivering reduced functionality instead of total failure when conditions are bad.
details:
It protects critical user flows by sacrificing optional behavior first.
example:
An app may hide live recommendations during outage conditions but keep ordering available.
mnemonic:
Fail softer, preserve the core.
recall:
- Why is graceful degradation valuable in user-facing systems?
- Which features should degrade first?
```

```concept-card
id: technical-debt
term: Technical Debt
parents:
- fault-tolerance
summary:
Technical debt is the future cost of choices that make the system harder to change, test, or understand.
details:
At architectural scale it often appears as weak boundaries, duplication, hidden coupling, and outdated assumptions.
example:
Multiple services depending on one undocumented shared database schema creates architectural debt.
mnemonic:
Short-term speed, future drag.
recall:
- When does technical debt become architectural rather than local?
- Why is hidden coupling a debt amplifier?
```
