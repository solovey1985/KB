---
title: HTML Semantic Layout and Content Meaning Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise semantic HTML5 structure questions.

Relevant concept maps:

- [Concept Map](html-semantic-layout.concept.md)

## Semantics

```interview-question
What are semantic HTML tags and why are they important?
---
answer:
Semantic HTML tags are elements whose names describe the meaning or role of the content they contain.

They are important because they improve accessibility, SEO, maintainability, and machine understanding of the page structure.
hints:
- Meaning is the key word.
- Accessibility and SEO should be mentioned.
- They do more than visual grouping.
```

Related concepts: [Semantic HTML](html-semantic-layout.concept.md#semantic-html), [Landmarks](html-semantic-layout.concept.md#landmarks)

```interview-question
What is the difference between `section` and `article`?
---
answer:
`article` is for content that can stand on its own as an independent piece.

`section` is for a thematic grouping within a larger document.

An article might contain sections, but a section is not automatically an article.
hints:
- Independent content versus thematic grouping.
- Reusability outside the page is the clue.
- They are related, but not interchangeable.
```

Related concepts: [article Element](html-semantic-layout.concept.md#article-element), [section Element](html-semantic-layout.concept.md#section-element)

```interview-question
When should you use `button` instead of `a`?
---
answer:
Use `button` when the element triggers an action inside the current page or application.

Use `a` when the element navigates to another location or document.

This distinction matters for semantics, accessibility, and default browser behavior.
hints:
- Action versus navigation.
- Semantics matters more than visual similarity.
- Keyboard behavior is part of the reason.
```

Related concepts: [Button Versus Anchor](html-semantic-layout.concept.md#button-versus-anchor)

```interview-choice
Which element is the most appropriate semantic container for the main unique content of the page?
---
options:
- `<main>`
- `<div>`
- `<small>`
correct: 0
explanation:
`<main>` is the semantic element for the document's primary unique content area.
```

```interview-code
language: html
prompt: Complete the layout so the navigation is wrapped in the correct semantic element.
starter:
<header>
  
    <a href="/">Home</a>
    <a href="/about">About</a>
  
</header>
solution:
<header>
  <nav>
    <a href="/">Home</a>
    <a href="/about">About</a>
  </nav>
</header>
checks:
- includes: <nav>
- includes: </nav>
```
