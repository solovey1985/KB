---
title: HTML Accessibility and Inclusive Markup Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise accessibility-oriented HTML questions.

Relevant concept maps:

- [Concept Map](html-accessibility.concept.md)

## Accessibility Basics

```interview-question
What is the difference between semantic HTML and ARIA?
---
answer:
Semantic HTML uses native elements that already express meaning and behavior.

ARIA adds accessibility metadata when native HTML is not enough, but it should not be used as a replacement for good native markup.
hints:
- Native first, ARIA second.
- One is built into HTML semantics.
- The other supplements meaning.
```

Related concepts: [Semantic Accessibility](html-accessibility.concept.md#semantic-accessibility), [ARIA](html-accessibility.concept.md#aria)

```interview-question
When should you use `alt=""` on an image?
---
answer:
Use `alt=""` when the image is decorative and adds no meaningful information.

That tells assistive technologies to ignore the image instead of announcing useless content.
hints:
- Decorative images are the key case.
- Silence is better than noisy irrelevant alt text.
- Not every image needs descriptive alt text.
```

Related concepts: [Alternative Text](html-accessibility.concept.md#alternative-text)

```interview-question
Why is using a real `<button>` better than a clickable `<div>` for actions?
---
answer:
A real button already provides native semantics, keyboard behavior, and expected accessibility support.

A clickable `<div>` usually requires extra scripting and ARIA to approximate what the button already gives for free.
hints:
- Native behavior matters.
- Keyboard support is part of the answer.
- Semantics should come first.
```

Related concepts: [Native Interactive Elements](html-accessibility.concept.md#native-interactive-elements)

```interview-choice
Which attribute pair is commonly used to give a control an accessible extra description?
---
options:
- `for` and `id`
- `aria-describedby` and a matching element `id`
- `lang` and `charset`
correct: 1
explanation:
`aria-describedby` points to an element whose text should be announced as supporting description for the control.
```
