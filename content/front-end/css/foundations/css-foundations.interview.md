---
title: CSS Foundations Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the CSS basics that appear in many frontend interviews.

Relevant concept maps:

- [Concept Map](css-foundations.concept.md)

## CSS Basics

```interview-question
What does CSS stand for and what is its primary use?
---
answer:
CSS stands for Cascading Style Sheets.

Its main purpose is to control presentation and visual layout while keeping that concern separate from HTML content structure.
hints:
- The C is important.
- It controls presentation.
- Separation of concerns is part of the answer.
```

Related concepts: [CSS](css-foundations.concept.md#css), [Cascade](css-foundations.concept.md#cascade)

```interview-question
How do you include CSS in an HTML document?
---
answer:
CSS can be included with an external stylesheet, a `<style>` block, or inline `style` attributes.

The usual best practice for maintainable projects is an external stylesheet linked from the document head.
hints:
- There are multiple ways.
- One is best for reuse and maintainability.
- The `<link>` element is the common answer.
```

Related concepts: [CSS Inclusion Methods](css-foundations.concept.md#css-inclusion-methods)

```interview-question
What is the difference between class and ID selectors?
---
answer:
Class selectors are reusable and can style many elements.

ID selectors are intended to target one unique element in the document.

Classes are usually the better styling default because they are more reusable and less rigid.
hints:
- One is reusable.
- One is unique.
- Reusability is the practical difference.
```

Related concepts: [Class Selector](css-foundations.concept.md#class-selector), [ID Selector](css-foundations.concept.md#id-selector)

```interview-choice
Which selector type is generally the better default for reusable component styling?
---
options:
- ID selector
- Class selector
- Inline style only
correct: 1
explanation:
Class selectors are reusable and fit component-style patterns better than unique IDs.
```

```interview-question
What is the difference between a pseudo-class and a pseudo-element?
---
answer:
A pseudo-class styles an element based on state or relationship, such as `:hover` or `:first-child`.

A pseudo-element styles a virtual part of an element, such as `::before` or `::after`.
hints:
- One is about state.
- One is about a virtual part.
- `:hover` and `::before` are the anchor examples.
```

Related concepts: [Pseudo-Class](css-foundations.concept.md#pseudo-class), [Pseudo-Element](css-foundations.concept.md#pseudo-element)

```interview-code
language: css
prompt: Complete the selector so it styles an element with the class `card`.
starter:

{
  border-radius: 12px;
}
solution:
.card {
  border-radius: 12px;
}
checks:
- includes: .card
- includes: border-radius
```
