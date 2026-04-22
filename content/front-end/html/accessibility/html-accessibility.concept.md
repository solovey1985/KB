---
title: HTML Accessibility and Inclusive Markup Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes HTML accessibility around semantic elements, text alternatives, and supportive ARIA usage.

Study pages: [Section Index](index.md) | [Material Notes](html-accessibility.md) | [Interview Practice](html-accessibility.interview.md)

## Accessibility Map

```concept-card
id: semantic-accessibility
term: Semantic Accessibility
children:
- native-interactive-elements
- alternative-text
- aria
summary:
Semantic accessibility means using meaningful native HTML so browsers and assistive technologies understand the page correctly.
details:
Accessible HTML begins with element choice, not only with extra attributes added afterward.
example:
Use `<button>` for actions, `<nav>` for navigation, and `<label>` for form controls.
mnemonic:
Correct elements create the first accessibility layer.
recall:
- Why is semantic HTML central to accessibility?
- Why should accessibility start before ARIA?
```

```concept-card
id: native-interactive-elements
term: Native Interactive Elements
parents:
- semantic-accessibility
summary:
Native interactive elements already provide browser and accessibility behaviors appropriate to their role.
details:
They support expected keyboard interaction and semantic meaning without extra reimplementation.
example:
`<button>` handles activation and focus more correctly than a clickable `<div>`.
mnemonic:
Native controls come with built-in behavior.
recall:
- Why is a real button better than a clickable div?
- What kinds of behavior come for free with native controls?
```

```concept-card
id: alternative-text
term: Alternative Text
parents:
- semantic-accessibility
summary:
Alternative text gives non-visual meaning for images when appropriate.
details:
Descriptive alt text is useful for meaningful images, while decorative images should use empty alt text.
example:
`<img src="chart.png" alt="Revenue rose 18 percent quarter over quarter" />`
mnemonic:
Describe meaning, not decoration.
recall:
- When should alt text be descriptive?
- When is `alt=""` the right answer?
```

```concept-card
id: aria
term: ARIA
parents:
- semantic-accessibility
summary:
ARIA adds accessibility metadata when native HTML alone is not enough.
details:
It should supplement semantic HTML, not replace it without reason.
example:
`aria-describedby="password-help"` connects a control to supporting explanatory text.
mnemonic:
ARIA fills gaps, it should not replace good markup.
recall:
- When is ARIA appropriate?
- Why is ARIA not a substitute for semantic HTML?
```
