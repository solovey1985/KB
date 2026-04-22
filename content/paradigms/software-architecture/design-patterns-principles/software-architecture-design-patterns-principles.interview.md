---
title: Design Patterns and Principles Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise common architecture patterns and design principles.

Relevant concept maps:

- [Concept Map](software-architecture-design-patterns-principles.concept.md)

## Patterns and Principles

```interview-question
Describe the Model-View-Controller architectural pattern.
---
answer:
MVC separates the system into a model that represents state and business behavior, a view that renders output, and a controller that handles input and coordinates updates.

Its goal is to keep presentation concerns from being tightly mixed with domain behavior.
hints:
- Think model, view, controller.
- One handles rendering.
- One coordinates interaction.
```

Related concepts: [MVC](software-architecture-design-patterns-principles.concept.md#mvc)

```interview-question
Explain the publish-subscribe pattern and where it is useful.
---
answer:
Publish-subscribe lets producers publish events without directly depending on all consumers. Subscribers react to those events independently.

It is useful for integration, notifications, event-driven workflows, and systems that need extensibility without tight direct coupling.
hints:
- Producers do not need to know every consumer.
- Events are central.
- Integration is a common use case.
```

Related concepts: [Publish-Subscribe](software-architecture-design-patterns-principles.concept.md#publish-subscribe)

```interview-question
How would you contrast microservices architecture with a monolithic architecture?
---
answer:
A monolith keeps major capabilities in one deployable unit, while microservices split capabilities into independently deployable services.

Microservices can improve autonomy and targeted scaling, but they add operational, consistency, and observability complexity.
hints:
- One deployable versus many deployables.
- Independent deployment is a key difference.
- Operational overhead should appear in your answer.
```

Related concepts: [Monolith](software-architecture-design-patterns-principles.concept.md#monolith), [Microservices Architecture](software-architecture-design-patterns-principles.concept.md#microservices-architecture)

```interview-question
What are the SOLID principles trying to achieve at a practical level?
---
answer:
SOLID principles try to produce code and module boundaries that are easier to understand, extend, test, and change without creating wide ripple effects.

In architecture, they help keep responsibilities focused and dependencies pointing in healthier directions.
hints:
- Think maintainability.
- Think changeability.
- Think focused responsibilities and better dependency direction.
```

Related concepts: [SOLID Principles](software-architecture-design-patterns-principles.concept.md#solid-principles)

```interview-choice
When is the Singleton pattern most dangerous?
---
options:
- When it represents one logical shared resource with no mutable state
- When it hides global mutable state and makes testing harder
- When it is used behind an interface
correct: 1
explanation:
Singleton becomes risky when it acts like a hidden global dependency. That often increases coupling and makes tests harder to isolate.
```
