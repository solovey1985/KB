---
title: Advanced CSS Engineering Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise advanced CSS architecture and performance questions.

Relevant concept maps:

- [Concept Map](css-advanced-engineering.concept.md)

## Debugging and Architecture

```interview-question
How do you debug a CSS rule that is not applying?
---
answer:
Check whether the selector matches the element, whether a competing rule has higher specificity, whether source order changes the winner, and whether the property is being overridden or invalid in that context.

Browser DevTools are usually the fastest way to inspect the winning and losing rules.
hints:
- Matching is the first check.
- Specificity and source order matter.
- DevTools should be mentioned.
```

Related concepts: [CSS Debugging](css-advanced-engineering.concept.md#css-debugging), [Specificity Management](css-advanced-engineering.concept.md#specificity-management)

```interview-question
Why do teams adopt CSS architecture approaches like BEM or design tokens?
---
answer:
They help large codebases stay readable, predictable, and easier to scale.

Naming systems reduce ambiguity, and design tokens centralize repeated values such as colors, spacing, and radius choices.

Without structure, CSS often becomes harder to maintain as the application grows.
hints:
- Think scaling and maintenance.
- Names and shared values are the big themes.
- The problem is not syntax but codebase growth.
```

Related concepts: [CSS Architecture](css-advanced-engineering.concept.md#css-architecture), [Design Tokens](css-advanced-engineering.concept.md#design-tokens)

```interview-choice
Which CSS feature is the strongest built-in choice for runtime theming and reusable design values?
---
options:
- CSS custom properties
- `!important`
- inline styles on every element
correct: 0
explanation:
CSS custom properties are well suited for design tokens, theming, and shared visual values that may vary by context.
```
