---
title: Emerging Technologies and Future Trends Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about newer technologies and shifting constraints.

Relevant concept maps:

- [Concept Map](software-architecture-emerging-technologies.concept.md)

## Emerging Trends

```interview-question
What role does AI play in modern software architecture?
---
answer:
AI adds architecture concerns around model serving, data pipelines, feature management, evaluation, drift monitoring, cost control, and human oversight.

It changes architecture when prediction quality and model lifecycle become part of system behavior.
hints:
- Think beyond one endpoint calling a model.
- Drift and evaluation matter.
- Data and lifecycle are part of the answer.
```

```interview-question
When can blockchain meaningfully affect software architecture?
---
answer:
Blockchain meaningfully affects architecture when decentralized trust, immutability, or shared ownership across organizations are central requirements.

It is not a general-purpose replacement for normal databases and should only be chosen when its trust model clearly fits the problem.
hints:
- Trust model is central.
- Multi-party coordination is a clue.
- Do not present blockchain as a universal upgrade.
```

```interview-question
How do AR and VR applications change architectural priorities?
---
answer:
AR and VR systems increase pressure on latency, synchronization, media throughput, device-aware optimization, and realtime communication.

Small delays or inconsistency can directly break the user experience.
hints:
- Latency matters a lot.
- Realtime sync is important.
- Throughput and device capability matter too.
```

```interview-question
How can 5G affect software architectures?
---
answer:
5G can make richer mobile, edge, and realtime workloads more practical by improving bandwidth and latency characteristics.

However, architecture still needs resilience and degraded behavior because networks are never perfect everywhere.
hints:
- Better network envelope.
- Edge and realtime workloads benefit.
- It does not remove the need for resilience.
```
