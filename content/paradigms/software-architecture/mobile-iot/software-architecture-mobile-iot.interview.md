---
title: Mobile and IoT Architecture Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions around devices, connectivity, and the edge.

Relevant concept maps:

- [Concept Map](software-architecture-mobile-iot.concept.md)

## Mobile and IoT

```interview-question
What architectural concerns are important for mobile applications?
---
answer:
Mobile architecture must account for intermittent connectivity, limited battery and CPU, background execution limits, local storage needs, and efficient network usage.

These constraints often lead to sync strategies, careful API design, and offline-aware behavior.
hints:
- Device constraints matter.
- Network conditions are unreliable.
- Offline and sync concerns are common.
```

```interview-question
How does IoT architecture differ from traditional architectures?
---
answer:
IoT architecture deals with many constrained devices, unreliable networks, telemetry ingestion, fleet management, device identity, and update/security concerns at scale.

Traditional architectures usually do not have the same combination of physical device constraints and large telemetry flows.
hints:
- Many devices, not just many users.
- Device management matters.
- Telemetry and unreliable connectivity are clues.
```

```interview-question
What is edge computing in the context of IoT?
---
answer:
Edge computing means processing some data closer to the device or source instead of sending everything directly to a central cloud system.

It helps reduce latency, bandwidth use, and dependence on always-on connectivity.
hints:
- Closer to the source.
- Less round-trip dependency.
- Latency and bandwidth are important reasons.
```
