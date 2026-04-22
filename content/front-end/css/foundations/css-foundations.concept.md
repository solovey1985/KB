---
title: CSS Foundations Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes core CSS ideas around syntax, selectors, and rule conflict resolution.

Study pages: [Section Index](index.md) | [Material Notes](css-foundations.md) | [Interview Practice](css-foundations.interview.md)

## Foundations Map

```concept-card
id: css
term: CSS
children:
- css-inclusion-methods
- class-selector
- id-selector
- cascade
- specificity
- pseudo-class
- pseudo-element
- attribute-selector
- css-reset
summary:
CSS styles HTML by matching elements with selectors and applying presentation rules.
details:
Its power comes from reusable selectors, the cascade, and a layout and styling model that works across the document tree.
example:
`body { font-family: system-ui; }`
mnemonic:
Match elements, then style them.
recall:
- What is CSS responsible for?
- Why is CSS separate from HTML structure?
```

```concept-card
id: css-inclusion-methods
term: CSS Inclusion Methods
parents:
- css
summary:
CSS can be included with external stylesheets, internal style blocks, or inline styles.
details:
External stylesheets are usually the most maintainable because they are reusable and keep styling separate from markup.
example:
`<link rel="stylesheet" href="styles.css" />`
mnemonic:
External for scale, inline for exceptions.
recall:
- What are the common ways to include CSS?
- Why are external stylesheets usually preferred?
```

```concept-card
id: class-selector
term: Class Selector
parents:
- css
related:
- id-selector
summary:
A class selector styles any element carrying a given class name.
details:
It is reusable and is usually the best default for styling components and repeated UI patterns.
example:
`.button { padding: 0.75rem 1rem; }`
mnemonic:
Class means reusable style hook.
recall:
- Why are class selectors common in component styling?
- What makes them more reusable than IDs?
```

```concept-card
id: id-selector
term: ID Selector
parents:
- css
related:
- class-selector
summary:
An ID selector targets one uniquely identified element.
details:
IDs are valid in CSS, but they are usually less flexible than classes for long-term styling systems.
example:
`#hero { background: #111827; }`
mnemonic:
ID means one unique target.
recall:
- Why are IDs less reusable than classes?
- When might an ID still be acceptable?
```

```concept-card
id: cascade
term: Cascade
parents:
- css
related:
- specificity
summary:
The cascade is the rule system CSS uses to decide which declaration wins when multiple rules match.
details:
Importance, specificity, and source order all affect the final winning style.
example:
Two matching color rules may conflict, and CSS resolves the winner through cascade rules.
mnemonic:
When rules compete, the cascade decides.
recall:
- What problem does the cascade solve?
- Which factors influence which rule wins?
```

```concept-card
id: specificity
term: Specificity
parents:
- css
related:
- cascade
summary:
Specificity measures how targeted a selector is when CSS resolves conflicts.
details:
More specific selectors usually win over less specific ones when importance is equal.
example:
`button.primary` is more specific than `.primary`.
mnemonic:
More targeted selectors win more often.
recall:
- Why does specificity matter?
- Why can over-specific selectors become a maintenance problem?
```

```concept-card
id: pseudo-class
term: Pseudo-Class
parents:
- css
related:
- pseudo-element
summary:
A pseudo-class styles an element based on state, position, or relation.
details:
Examples include `:hover`, `:focus`, and `:first-child`.
example:
`button:hover { background: #2563eb; }`
mnemonic:
Pseudo-class means state-aware styling.
recall:
- What kinds of conditions do pseudo-classes represent?
- How is a pseudo-class different from a pseudo-element?
```

```concept-card
id: pseudo-element
term: Pseudo-Element
parents:
- css
related:
- pseudo-class
summary:
A pseudo-element styles a virtual part of an element or inserts generated content.
details:
Examples include `::before`, `::after`, and `::first-letter`.
example:
`.quote::before { content: '"'; }`
mnemonic:
Pseudo-element means part of an element, not a real node.
recall:
- What does a pseudo-element style?
- Why is `::before` different from `:hover`?
```

```concept-card
id: attribute-selector
term: Attribute Selector
parents:
- css
summary:
An attribute selector styles elements based on attributes and attribute values.
details:
It is useful for targeting links, form states, and semantic hooks without adding extra classes.
example:
`[href^="https"] { color: green; }`
mnemonic:
Style by what the element carries.
recall:
- What kinds of conditions can attribute selectors express?
- When are they useful instead of a class?
```

```concept-card
id: css-reset
term: CSS Reset
parents:
- css
summary:
A CSS reset or normalize layer reduces browser default-style differences to create a predictable baseline.
details:
It helps teams start from a more consistent visual foundation across browsers.
example:
`* { box-sizing: border-box; } body, h1, p { margin: 0; }`
mnemonic:
Start consistent before styling creatively.
recall:
- Why do teams use a reset or normalize layer?
- What problem does it reduce?
```
