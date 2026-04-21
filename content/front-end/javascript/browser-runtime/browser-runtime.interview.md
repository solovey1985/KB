---
title: JavaScript Browser and Runtime Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise browser-runtime and event-driven JavaScript questions.

Relevant concept maps:

- [Concept Map](browser-runtime.concept.md)

## Runtime Basics

```interview-question
How does JavaScript stay responsive in the browser even though it is single-threaded?
---
answer:
JavaScript itself executes on a single call stack, but the browser host environment handles timers, events, and network work outside that stack.

Completed async work is queued and then processed when the stack is ready, which keeps the UI responsive.
hints:
- The browser does part of the work.
- The call stack is single-threaded.
- Queues are involved.
```

Related concepts: [Host Environment](browser-runtime.concept.md#host-environment), [Call Stack](browser-runtime.concept.md#call-stack)

## Events

```interview-question
What is event bubbling and why is event delegation useful?
---
answer:
Event bubbling is the process where an event triggered on a child element moves upward through its ancestor elements.

Event delegation uses that behavior by putting one handler on a parent instead of many handlers on children.

This improves performance and makes dynamic UI elements easier to manage.
hints:
- The event moves upward.
- One parent handler can manage many children.
- Dynamic lists are a strong use case.
```

Related concepts: [Event Bubbling](browser-runtime.concept.md#event-bubbling), [Event Delegation](browser-runtime.concept.md#event-delegation)

```interview-choice
Which method is commonly used to stop an event from bubbling upward?
---
options:
- `event.preventDefault()`
- `event.stopPropagation()`
- `event.cancel()`
correct: 1
explanation:
`stopPropagation()` stops the event from continuing through the propagation chain.
```
