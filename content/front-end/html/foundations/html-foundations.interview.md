---
title: HTML Document Foundations Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise HTML document structure and foundational markup questions.

Relevant concept maps:

- [Concept Map](html-foundations.concept.md)

## Document Basics

```interview-question
What does HTML stand for and what is its purpose?
---
answer:
HTML stands for HyperText Markup Language.

Its purpose is to structure web content and describe the meaning of page elements so browsers, search engines, and assistive technologies can understand the document.
hints:
- It is markup, not a programming language.
- Structure and meaning are the key ideas.
- It is the base layer of the web page.
```

Related concepts: [HTML](html-foundations.concept.md#html), [Document Structure](html-foundations.concept.md#document-structure)

```interview-question
What does `<!DOCTYPE html>` do?
---
answer:
`<!DOCTYPE html>` tells the browser to use the modern HTML parsing mode.

It helps prevent quirks-mode rendering and makes the browser interpret the page as an HTML5 document.
hints:
- It is not a normal HTML tag.
- It affects browser parsing mode.
- Quirks mode is the contrast case.
```

Related concepts: [DOCTYPE](html-foundations.concept.md#doctype)

```interview-question
What is the difference between the `head` and `body` tags?
---
answer:
The `head` contains metadata, linked resources, and document-level information that is not usually displayed as main content.

The `body` contains the visible page content users interact with.
hints:
- Metadata versus visible content.
- One prepares the document, the other displays it.
- Stylesheets and title go in one, content goes in the other.
```

Related concepts: [Head Section](html-foundations.concept.md#head-section), [Body Section](html-foundations.concept.md#body-section)

```interview-question
Why is the `lang` attribute important on the `<html>` element?
---
answer:
The `lang` attribute tells browsers, screen readers, and search engines which language the document is written in.

This improves accessibility, pronunciation, and language-aware processing.
hints:
- Think accessibility first.
- Screen readers use it.
- SEO benefits also exist.
```

Related concepts: [Language Declaration](html-foundations.concept.md#language-declaration)

```interview-choice
Which meta tag is most directly responsible for correct mobile viewport scaling behavior?
---
options:
- `<meta charset="UTF-8">`
- `<meta name="viewport" content="width=device-width, initial-scale=1.0">`
- `<meta name="author" content="...">`
correct: 1
explanation:
The viewport meta tag tells the browser how to size and scale the page on mobile devices.
```

```interview-code
language: html
prompt: Complete the document so it declares modern HTML and sets the language to English.
starter:

<html >
  <head></head>
  <body></body>
</html>
solution:
<!DOCTYPE html>
<html lang="en">
  <head></head>
  <body></body>
</html>
checks:
- includes: <!DOCTYPE html>
- includes: lang="en"
```
