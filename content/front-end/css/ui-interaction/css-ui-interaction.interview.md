---
title: CSS UI States, Forms, and Interaction Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise CSS interaction and form-state questions.

Relevant concept maps:

- [Concept Map](css-ui-interaction.concept.md)

## State Styling

```interview-question
Why is `:focus-visible` often better than styling only `:hover` for interaction feedback?
---
answer:
`:focus-visible` provides visible focus feedback for keyboard and assistive-technology users.

`:hover` only helps pointer interactions, so relying on it alone can make the interface less accessible.
hints:
- Think keyboard users.
- Hover is not enough.
- Accessibility is the real reason.
```

Related concepts: [Interactive States](css-ui-interaction.concept.md#interactive-states), [Focus Visibility](css-ui-interaction.concept.md#focus-visibility)

```interview-question
What is the difference between transitions and animations in CSS?
---
answer:
Transitions animate a change between two states when a property value changes.

Animations use `@keyframes` and can express multi-step motion over time, often without a state change trigger.
hints:
- One is property-change driven.
- One is keyframe driven.
- State change versus timeline is the key difference.
```

Related concepts: [Transitions](css-ui-interaction.concept.md#transitions), [Animations](css-ui-interaction.concept.md#animations)

```interview-choice
Which selector is commonly best for showing accessible keyboard focus?
---
options:
- `:hover`
- `:focus-visible`
- `::before`
correct: 1
explanation:
`:focus-visible` is designed to show meaningful focus feedback when it is needed for keyboard and similar navigation modes.
```

```interview-code
language: css
prompt: Complete the rule so a pseudo-element inserts an arrow after an external link.
starter:
.external-link::after {
  content: 
}
solution:
.external-link::after {
  content: '↗';
}
checks:
- includes: ↗
```
