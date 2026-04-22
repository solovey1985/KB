---
title: CSS Modern Layout and Responsive Design Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise modern CSS layout and responsive design questions.

Relevant concept maps:

- [Concept Map](css-layout-responsive.concept.md)

## Flexbox and Grid

```interview-question
When would you choose Flexbox over Grid?
---
answer:
Choose Flexbox when the layout problem is mainly one-dimensional, such as arranging items in a row or a column.

Choose Grid when both rows and columns matter and you need more explicit two-dimensional control.
hints:
- One dimension versus two dimensions.
- Alignment of items in one line is a Flexbox strength.
- Explicit row and column control points toward Grid.
```

Related concepts: [Flexbox](css-layout-responsive.concept.md#flexbox), [CSS Grid](css-layout-responsive.concept.md#css-grid)

```interview-question
What is the difference between `justify-content` and `align-items` in Flexbox?
---
answer:
`justify-content` distributes items along the main axis.

`align-items` aligns items along the cross axis.

Which direction each axis represents depends on the flex direction.
hints:
- Main axis versus cross axis.
- Flex direction changes the interpretation.
- Both are alignment controls, but on different axes.
```

Related concepts: [Flex Axes](css-layout-responsive.concept.md#flex-axes)

```interview-question
What is mobile-first responsive design?
---
answer:
Mobile-first responsive design means the base styles are written for smaller screens first, and larger-screen enhancements are added progressively.

This often leads to simpler defaults and fewer overrides than starting from a desktop-heavy design.
hints:
- Base styles come first.
- Larger screens get enhancements.
- It is a strategy, not only a media query technique.
```

Related concepts: [Media Queries](css-layout-responsive.concept.md#media-queries), [Mobile-First Design](css-layout-responsive.concept.md#mobile-first-design)

```interview-choice
Which layout system is most naturally suited to a two-dimensional dashboard with explicit rows and columns?
---
options:
- Floats
- Flexbox
- CSS Grid
correct: 2
explanation:
CSS Grid is designed for two-dimensional layout where rows and columns both matter directly.
```

```interview-code
language: css
prompt: Complete the media query so the grid collapses to one column on smaller screens.
starter:
@media (max-width: 768px) {
  .dashboard {
    grid-template-columns: 
  }
}
solution:
@media (max-width: 768px) {
  .dashboard {
    grid-template-columns: 1fr;
  }
}
checks:
- includes: 1fr
```
