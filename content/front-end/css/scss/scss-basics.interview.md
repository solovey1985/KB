---
title: SCSS Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise SCSS-specific authoring and architecture questions.

Relevant concept maps:

- [Concept Map](scss-basics.concept.md)

## SCSS Basics

```interview-question
Why would a team use SCSS instead of only plain CSS?
---
answer:
SCSS adds authoring conveniences such as variables, nesting, mixins, and modular file organization.

These features help large stylesheets stay more maintainable and reusable before the code is compiled to standard CSS.
hints:
- It improves authoring, not browser support directly.
- Variables and mixins are key examples.
- It still produces CSS in the end.
```

Related concepts: [SCSS](scss-basics.concept.md#scss), [Mixins](scss-basics.concept.md#mixins), [SCSS Modules](scss-basics.concept.md#scss-modules)

```interview-question
What is the difference between SCSS variables and CSS custom properties?
---
answer:
SCSS variables are resolved at compile time before the browser receives the final CSS.

CSS custom properties exist at runtime in the browser and can change dynamically, which makes them useful for themes and contextual styling.
hints:
- Compile time versus runtime.
- One disappears into the compiled CSS.
- The other still exists in the browser.
```

Related concepts: [SCSS Variables Versus CSS Variables](scss-basics.concept.md#scss-variables-versus-css-variables)

```interview-choice
Which SCSS feature is designed for reusable chunks of styling logic that can accept arguments?
---
options:
- Mixins
- Nesting
- Placeholder comments
correct: 0
explanation:
Mixins package reusable styles and can accept parameters, which makes them useful for repeated patterns such as focus rings or responsive utilities.
```

```interview-code
language: scss
prompt: Complete the SCSS so the button uses the `$color-primary` variable.
starter:
$color-primary: #2563eb;

.button {
  background: 
}
solution:
$color-primary: #2563eb;

.button {
  background: $color-primary;
}
checks:
- includes: $color-primary
```
