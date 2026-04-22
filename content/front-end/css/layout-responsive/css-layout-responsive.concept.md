---
title: CSS Modern Layout and Responsive Design Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes modern CSS layout around Flexbox, Grid, and responsive strategy.

Study pages: [Section Index](index.md) | [Material Notes](css-layout-responsive.md) | [Interview Practice](css-layout-responsive.interview.md)

## Responsive Map

```concept-card
id: flexbox
term: Flexbox
children:
- flex-axes
summary:
Flexbox is a one-dimensional layout system for aligning and distributing items in a row or column.
details:
It is ideal for navbars, toolbars, cards, and many UI alignment problems where one axis is dominant.
example:
`display: flex; justify-content: space-between; align-items: center;`
mnemonic:
Flexbox is one axis done well.
recall:
- What kind of layout problem fits Flexbox best?
- Why is Flexbox a poor description for fully two-dimensional layout?
```

```concept-card
id: flex-axes
term: Flex Axes
parents:
- flexbox
summary:
Flexbox layout is defined by a main axis and a cross axis.
details:
`justify-content` works on the main axis, while `align-items` works on the cross axis.
example:
In a row layout, the main axis is horizontal and the cross axis is vertical.
mnemonic:
Justify on the main, align on the cross.
recall:
- What happens to the axes when `flex-direction` changes?
- Which property controls which axis?
```

```concept-card
id: css-grid
term: CSS Grid
summary:
CSS Grid is a two-dimensional layout system for arranging content in rows and columns.
details:
It is strong for dashboards, page scaffolds, galleries, and explicit layout structures where both axes matter.
example:
`display: grid; grid-template-columns: 240px 1fr;`
mnemonic:
Grid thinks in rows and columns together.
recall:
- When is Grid stronger than Flexbox?
- What layout problems does Grid solve especially well?
```

```concept-card
id: media-queries
term: Media Queries
children:
- mobile-first-design
summary:
Media queries conditionally apply CSS based on viewport or environment characteristics.
details:
They are one of the core tools for responsive design, but they are only one part of the overall strategy.
example:
`@media (max-width: 768px) { ... }`
mnemonic:
If the context changes, the styles can too.
recall:
- What kinds of conditions can media queries respond to?
- Why are media queries not the whole of responsive design?
```

```concept-card
id: mobile-first-design
term: Mobile-First Design
parents:
- media-queries
summary:
Mobile-first design starts with smaller-screen defaults and progressively enhances for larger screens.
details:
This often produces cleaner style layering and avoids oversized desktop assumptions leaking into every breakpoint.
example:
Write one-column defaults first, then expand to multiple columns at wider breakpoints.
mnemonic:
Small first, expand later.
recall:
- Why can mobile-first be easier to maintain?
- What is the difference between mobile-first and simply having media queries?
```
