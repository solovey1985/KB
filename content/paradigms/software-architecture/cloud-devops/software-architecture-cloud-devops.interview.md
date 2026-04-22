---
title: Cloud Computing and DevOps Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about cloud platforms and delivery.

Relevant concept maps:

- [Concept Map](software-architecture-cloud-devops.concept.md)

## Cloud and Delivery

```interview-question
How does cloud computing influence software architecture design?
---
answer:
Cloud computing pushes architecture toward automation, elasticity, externalized state, observability, and resilience to instance loss.

It changes assumptions from carefully preserving one server to safely running many replaceable instances.
hints:
- Think elastic and disposable infrastructure.
- State should not stay trapped in one node.
- Observability and automation matter.
```

```interview-question
What is Infrastructure as Code and why does it matter architecturally?
---
answer:
Infrastructure as Code means describing infrastructure in versioned, reviewable code instead of manual setup.

It matters architecturally because reliable systems need reproducible environments and safer change management, not only correct application logic.
hints:
- Versioned infrastructure.
- Repeatability is key.
- Environment consistency affects system reliability.
```

```interview-question
How does CI/CD influence architecture decisions?
---
answer:
CI/CD favors architectures that support isolated testing, small deployable changes, clear contracts, and safe rollback.

If the architecture makes testing or deployment tightly coupled, delivery speed and confidence both suffer.
hints:
- Small safe changes.
- Test isolation matters.
- Deployment boundaries matter.
```

```interview-choice
What is the main operational advantage of blue-green deployment?
---
options:
- It removes the need for monitoring
- It allows low-downtime cutover with simpler rollback
- It guarantees zero data migration risk
correct: 1
explanation:
Blue-green deployment keeps the previous environment available, which makes switching and rollback simpler. It does not eliminate data compatibility concerns.
```
