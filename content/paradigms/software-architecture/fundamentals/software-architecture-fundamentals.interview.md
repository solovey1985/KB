---
title: Software Architecture Fundamentals Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the basic language of software architecture.

Relevant concept maps:

- [Concept Map](software-architecture-fundamentals.concept.md)

## Fundamentals

```interview-question
What is the difference between software architecture and software design?
---
answer:
Software architecture focuses on the high-level structure of the system, the responsibilities of major parts, and the trade-offs behind large decisions.

Software design focuses on local implementation choices inside those architectural boundaries.
hints:
- One is system-wide, one is local.
- Think expensive-to-reverse decisions.
- Class design is usually not the same thing as system architecture.
```

Related concepts: [Software Architecture](software-architecture-fundamentals.concept.md#software-architecture)

```interview-question
Explain separation of concerns in software architecture.
---
answer:
Separation of concerns means splitting the system so each part focuses on one type of responsibility, such as presentation, business rules, persistence, or messaging.

This reduces accidental coupling and makes change, testing, and reasoning easier.
hints:
- Each part should have a focused responsibility.
- Mixing unrelated jobs is the problem.
- Think clearer boundaries and easier maintenance.
```

Related concepts: [Separation of Concerns](software-architecture-fundamentals.concept.md#separation-of-concerns)

```interview-question
What is a system quality attribute, and why is it important in architecture?
---
answer:
A quality attribute is a non-functional property such as scalability, reliability, security, or performance.

It is important because architecture is usually evaluated by how well it supports the most important quality attributes for the system.
hints:
- This is about non-functional behavior.
- Performance and reliability are examples.
- Architecture trade-offs are often driven by these attributes.
```

Related concepts: [Quality Attribute](software-architecture-fundamentals.concept.md#quality-attribute)

```interview-choice
Which combination is usually the architectural goal when designing modules?
---
options:
- Low cohesion and high coupling
- High cohesion and low coupling
- High cohesion and high coupling
correct: 1
explanation:
Good architecture usually tries to keep each module focused internally while minimizing the number and strength of dependencies between modules.
```

```interview-question
How are cross-cutting concerns usually addressed in software architecture?
---
answer:
Cross-cutting concerns are usually handled through shared architectural mechanisms such as middleware, decorators, filters, shared infrastructure, or platform capabilities.

The goal is to centralize them instead of scattering the same logic across business code.
hints:
- Logging and auth are common examples.
- Avoid duplicating them in every feature module.
- Centralization is the key idea.
```

Related concepts: [Cross-Cutting Concern](software-architecture-fundamentals.concept.md#cross-cutting-concern)
