---
title: Advanced HTML Patterns Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise advanced HTML questions around metadata, browser behavior, and resilient markup.

Relevant concept maps:

- [Concept Map](html-advanced-patterns.concept.md)

## Advanced Markup

```interview-question
What are `data-*` attributes and when should they be used?
---
answer:
`data-*` attributes store custom metadata on HTML elements.

They are useful for DOM hooks, small configuration values, or structured element-specific data that does not fit standard HTML attributes.
hints:
- Think custom metadata.
- They are valid HTML.
- They should not replace semantic attributes when real semantic attributes exist.
```

Related concepts: [data-* Attributes](html-advanced-patterns.concept.md#data-attributes)

```interview-question
What is the difference between `async` and `defer` on script tags?
---
answer:
`defer` downloads the script in parallel but waits to execute it until HTML parsing is complete, preserving document order among deferred scripts.

`async` downloads in parallel and executes as soon as it is ready, which can interrupt parsing order expectations.
hints:
- Parsing order is the key distinction.
- Both improve over naive blocking script loading.
- `defer` is usually easier for app scripts.
```

Related concepts: [Script Loading](html-advanced-patterns.concept.md#script-loading)

```interview-question
What is progressive enhancement in the context of HTML?
---
answer:
Progressive enhancement means starting with meaningful core content and behavior that works broadly, then layering richer behavior on top when more capabilities are available.

It encourages resilient markup and better baseline user experience.
hints:
- Start from the basic useful version.
- Add enhancements later.
- Resilience is a central theme.
```

Related concepts: [Progressive Enhancement](html-advanced-patterns.concept.md#progressive-enhancement)

```interview-choice
Which attribute is usually the safer default for app bundle scripts that should execute after HTML parsing finishes?
---
options:
- `async`
- `defer`
- `blocking`
correct: 1
explanation:
`defer` is typically the safer default for application scripts because it preserves parsing flow and executes after the document has been parsed.
```
