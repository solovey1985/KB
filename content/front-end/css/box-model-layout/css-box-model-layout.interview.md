---
title: CSS Box Model and Layout Basics Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise box-model and basic layout questions.

Relevant concept maps:

- [Concept Map](css-box-model-layout.concept.md)

## Box Model

```interview-question
What is the CSS box model?
---
answer:
The CSS box model describes an element as content surrounded by padding, border, and margin.

Those layers determine how much space the element takes up and how it interacts with nearby elements.
hints:
- There are four parts.
- Content is only the center.
- Space around the content matters too.
```

Related concepts: [Box Model](css-box-model-layout.concept.md#box-model)

```interview-question
What is margin collapsing?
---
answer:
Margin collapsing is the behavior where adjacent vertical margins combine into a single effective margin instead of adding together normally.

It often happens between stacked block elements and between parent and child blocks under certain conditions.
hints:
- It is about vertical margins.
- Adjacent margins are involved.
- The result is often one effective margin, not two.
```

Related concepts: [Margin Collapsing](css-box-model-layout.concept.md#margin-collapsing)

```interview-question
What are the common values of `box-sizing` and what do they do?
---
answer:
The main values are `content-box` and `border-box`.

With `content-box`, width and height apply only to the content area. With `border-box`, the declared size includes padding and border.

`border-box` is often easier to reason about in modern layouts.
hints:
- One is the default traditional model.
- One keeps padding and border inside the declared size.
- Layout predictability is the practical difference.
```

Related concepts: [box-sizing](css-box-model-layout.concept.md#box-sizing)

```interview-choice
Which value usually makes width calculations easier in modern layouts?
---
options:
- `content-box`
- `border-box`
- `inherit-box`
correct: 1
explanation:
`border-box` is often easier because padding and border stay inside the declared width instead of expanding the total rendered size.
```

```interview-question
What is the difference between `block`, `inline`, and `inline-block`?
---
answer:
`block` elements usually start on a new line and can take full available width.

`inline` elements stay in text flow and do not respect width and height the same way blocks do.

`inline-block` stays inline with neighboring content but still allows box-like sizing behavior.
hints:
- One creates a new line by default.
- One stays in text flow.
- One combines both behaviors partly.
```

Related concepts: [Display Types](css-box-model-layout.concept.md#display-types)

```interview-code
language: css
prompt: Complete the rule so the element uses `border-box` sizing.
starter:
* {
  box-sizing: 
}
solution:
* {
  box-sizing: border-box;
}
checks:
- includes: border-box
```
