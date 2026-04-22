---
title: HTML Media and Responsive Content Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise HTML5 media and responsive-content questions.

Relevant concept maps:

- [Concept Map](html-media-responsive.concept.md)

## Media

```interview-question
How do `audio` and `video` tags work in HTML5?
---
answer:
They provide native browser elements for playing media without plugins.

They can use multiple `source` elements, support controls, and allow related features such as captions through `track`.
hints:
- Native browser media support is the key shift.
- `source` allows multiple formats.
- Accessibility is part of the discussion.
```

Related concepts: [HTML Media Elements](html-media-responsive.concept.md#html-media-elements), [Track Element](html-media-responsive.concept.md#track-element)

```interview-question
When would you use `picture` instead of only `img`?
---
answer:
Use `picture` when the actual image source should change based on viewport, format support, or art direction needs.

If the same image just needs responsive sizing, `img` with `srcset` and `sizes` may be enough.
hints:
- Different image source, not only different size.
- Art direction is the big clue.
- Format switching is another use case.
```

Related concepts: [Responsive Images](html-media-responsive.concept.md#responsive-images), [Picture Element](html-media-responsive.concept.md#picture-element)

```interview-question
Why is the `track` element important for video?
---
answer:
The `track` element provides timed text such as captions and subtitles.

That improves accessibility and can also help users in quiet or noisy environments.
hints:
- Captions and subtitles are the main use.
- Accessibility should be mentioned.
- It is not just a styling element.
```

Related concepts: [Track Element](html-media-responsive.concept.md#track-element)

```interview-choice
Which HTML feature helps the browser choose the most appropriate image file for different screen sizes?
---
options:
- `srcset`
- `fieldset`
- `details`
correct: 0
explanation:
`srcset` gives the browser multiple candidate image files so it can choose the right one for the current display context.
```
