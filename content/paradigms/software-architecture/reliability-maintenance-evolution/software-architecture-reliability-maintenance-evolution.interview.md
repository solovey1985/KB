---
title: Reliability, Maintenance, and Evolution Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about failure, maintenance, and long-term change.

Relevant concept maps:

- [Concept Map](software-architecture-reliability-maintenance-evolution.concept.md)

## Reliability and Evolution

```interview-question
Explain fault tolerance and how it is incorporated into software architecture.
---
answer:
Fault tolerance is the ability of a system to keep operating acceptably when components fail.

Architecture incorporates it through redundancy, isolation, retries, timeouts, circuit breakers, buffering, and explicit fallback behavior.
hints:
- Failure is expected, not ignored.
- Isolation and fallback are important.
- Retries alone are not enough.
```

```interview-question
What is graceful degradation in system design?
---
answer:
Graceful degradation means the system intentionally offers a reduced but still useful experience instead of completely failing when resources or dependencies are impaired.

It usually protects the most important business flows first.
hints:
- Reduced service, not total outage.
- Core user journey should remain available.
- Optional features often degrade first.
```

```interview-question
How do you manage technical debt within architecture?
---
answer:
Managing technical debt requires making it visible, prioritizing it alongside feature work, and reducing it through incremental refactoring, clearer boundaries, and safer contracts.

It is best handled continuously rather than delayed until a large rewrite becomes unavoidable.
hints:
- Visibility matters.
- Incremental cleanup is better than pure avoidance.
- Boundaries and contracts are part of debt management.
```

```interview-choice
Which architectural practice most directly improves debugging in distributed systems?
---
options:
- Removing all retries
- Using correlation IDs, tracing, and structured logs
- Avoiding metrics to reduce overhead
correct: 1
explanation:
Distributed systems are hard to diagnose without a way to connect events across services. Correlation IDs, tracing, and structured logs improve diagnosability significantly.
```
