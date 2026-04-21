---
title: JavaScript Browser and Runtime Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page explains the browser runtime pieces around JavaScript execution.

Study pages: [Section Index](index.md) | [Material Notes](browser-runtime.md) | [Interview Practice](browser-runtime.interview.md)

## Runtime Map

```concept-card
id: host-environment
term: Host Environment
children:
- call-stack
- dom-events
- browser-storage
summary:
The host environment provides browser features such as the DOM, timers, network APIs, and storage.
details:
JavaScript the language does not create the DOM or timers by itself; the browser host provides those capabilities.
example:
`setTimeout`, `fetch`, and `localStorage` all come from the host environment.
mnemonic:
JavaScript runs the code, the browser provides the world.
recall:
- Why is the browser called the host environment?
- Which APIs come from the host instead of the language itself?
```

```concept-card
id: call-stack
term: Call Stack
parents:
- host-environment
summary:
The call stack is the execution structure where JavaScript function calls run.
details:
Only one stack frame runs at a time, which is why long synchronous work blocks responsiveness.
example:
`first()` calling `second()` pushes frames on the stack in order.
mnemonic:
One stack, one active step at a time.
recall:
- Why can long synchronous code freeze the UI?
- What does the call stack represent?
```

```concept-card
id: dom-events
term: DOM Events
children:
- event-bubbling
- event-delegation
summary:
DOM events are browser notifications for user and system interactions such as clicks, input, and load.
details:
They make frontend JavaScript event-driven instead of purely linear.
example:
`button.addEventListener('click', handleClick)`
mnemonic:
The UI speaks through events.
recall:
- Why are DOM events central to frontend JavaScript?
- What common interactions produce DOM events?
```

```concept-card
id: event-bubbling
term: Event Bubbling
parents:
- dom-events
related:
- event-delegation
summary:
Event bubbling is the upward propagation of an event from the target element through its ancestors.
details:
It allows parent elements to observe child interactions unless propagation is stopped.
example:
A button click can also trigger a listener on its parent container.
mnemonic:
Target first, parents after.
recall:
- What is the default direction of many DOM events?
- Why does bubbling matter for UI architecture?
```

```concept-card
id: event-delegation
term: Event Delegation
parents:
- dom-events
related:
- event-bubbling
summary:
Event delegation is the pattern of handling child events through one listener on an ancestor element.
details:
It reduces repeated listeners and works well for dynamic lists and repeated UI elements.
example:
Attach one click listener to a `<ul>` and detect the clicked `<li>` with `closest(...)`.
mnemonic:
One parent listener, many child actions.
recall:
- Why is event delegation efficient?
- When is it better than many direct listeners?
```

```concept-card
id: browser-storage
term: Browser Storage
parents:
- host-environment
summary:
Browser storage APIs keep small pieces of state on the client side.
details:
`localStorage` persists across sessions, while `sessionStorage` is scoped to the session.
example:
`localStorage.setItem('theme', 'dark')`
mnemonic:
Small state, stored in the browser.
recall:
- What is the difference between `localStorage` and `sessionStorage`?
- What types of data fit browser storage well?
```
