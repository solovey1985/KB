# Advanced CSS Engineering

Advanced CSS work is usually about scale, debugging, maintainability, and performance, not only about syntax.

## Debugging rule conflicts

When CSS behaves unexpectedly, inspect:

- which selector wins
- specificity
- source order
- inherited values
- layout context

## CSS architecture

Large codebases usually need naming and structure conventions.

Common approaches:

- BEM
- utility-first systems
- design tokens with CSS variables
- component-scoped CSS systems

```css
.card__title {
  font-weight: 700;
}

.card--featured {
  border-color: gold;
}
```

## Performance concerns

Expensive layout or rendering work can come from:

- repeated layout thrashing
- overly complex selectors
- heavy paint effects
- unnecessary animations

## CSS variables

CSS custom properties are useful for tokens and theming.

```css
:root {
  --color-primary: #2563eb;
  --radius-card: 12px;
}

.button {
  background: var(--color-primary);
}
```

## Interview reminders

- talk about maintainability, not only styling tricks
- explain CSS architecture as a scaling concern
- mention DevTools when discussing debugging
