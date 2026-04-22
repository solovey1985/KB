---
title: SCSS Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes SCSS concepts around maintainable stylesheet authoring.

Study pages: [Section Index](index.md) | [Material Notes](scss-basics.md) | [Interview Practice](scss-basics.interview.md)

## SCSS Map

```concept-card
id: scss
term: SCSS
children:
- mixins
- scss-modules
- scss-variables-versus-css-variables
summary:
SCSS is a CSS preprocessor syntax that adds authoring features before compiling to CSS.
details:
It improves maintainability with variables, nesting, mixins, and modular structure, but browsers still consume the compiled CSS output.
example:
SCSS source can define `$color-primary`, nested selectors, and mixins before compiling to normal CSS.
mnemonic:
Better authoring first, plain CSS in the browser later.
recall:
- What is SCSS adding to the authoring experience?
- Why is SCSS not itself a browser runtime feature?
```

```concept-card
id: mixins
term: Mixins
parents:
- scss
summary:
Mixins package reusable styling logic and can accept arguments.
details:
They are useful for repeated visual patterns such as focus rings, breakpoints, or button variants.
example:
`@mixin focus-ring($color) { outline: 2px solid $color; }`
mnemonic:
Write once, include many times.
recall:
- What problem do mixins solve?
- Why are arguments useful in a mixin?
```

```concept-card
id: scss-modules
term: SCSS Modules
parents:
- scss
summary:
SCSS modules organize stylesheet logic through modern `@use` and `@forward` boundaries.
details:
They reduce global leakage and make shared tokens and helpers easier to manage safely.
example:
`@use 'tokens' as t;`
mnemonic:
Import with structure, not with sprawl.
recall:
- Why are `@use` and `@forward` preferred over older global imports?
- What maintenance problem do modules reduce?
```

```concept-card
id: scss-variables-versus-css-variables
term: SCSS Variables Versus CSS Variables
parents:
- scss
summary:
SCSS variables exist at compile time, while CSS custom properties exist at runtime.
details:
SCSS variables help authoring, but CSS variables are better when the browser must still react to value changes such as themes.
example:
Use `$spacing-md` for build-time reuse and `--color-primary` for runtime theming.
mnemonic:
SCSS compiles early, CSS variables live late.
recall:
- What is the runtime difference between the two variable systems?
- When should CSS variables still be preferred even in an SCSS codebase?
```
