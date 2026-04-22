---
title: HTML Document Foundations Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core ideas behind HTML documents and base markup.

Study pages: [Section Index](index.md) | [Material Notes](html-foundations.md) | [Interview Practice](html-foundations.interview.md)

## Foundations Map

```concept-card
id: html
term: HTML
children:
- document-structure
- doctype
- head-section
- body-section
- language-declaration
- metadata
- block-versus-inline
- data-attributes
summary:
HTML is the markup language used to structure and describe the meaning of web content.
details:
It provides the semantic and structural layer of a page, while CSS and JavaScript handle presentation and behavior.
example:
Headings, paragraphs, links, forms, and landmarks are all defined with HTML.
mnemonic:
HTML gives the page its structure and meaning.
recall:
- What does HTML provide that CSS and JavaScript do not?
- Why is HTML considered the structural layer of the web?
```

```concept-card
id: document-structure
term: Document Structure
parents:
- html
summary:
Document structure is the overall organization of the HTML document from root element to visible content.
details:
It includes the doctype, root html element, metadata in the head, and visible content in the body.
example:
`<!DOCTYPE html><html><head>...</head><body>...</body></html>`
mnemonic:
Good pages start with good structure.
recall:
- What major sections does an HTML document contain?
- Why is document structure the first HTML topic to learn?
```

```concept-card
id: doctype
term: DOCTYPE
parents:
- html
summary:
The doctype declaration tells the browser to use modern standards mode parsing for the document.
details:
It helps avoid quirks-mode behavior and establishes modern HTML expectations.
example:
`<!DOCTYPE html>`
mnemonic:
DOCTYPE puts the browser in modern mode.
recall:
- Why is DOCTYPE important?
- What problem does it help avoid?
```

```concept-card
id: head-section
term: Head Section
parents:
- html
related:
- metadata
summary:
The head section contains document metadata, linked resources, and setup information.
details:
It is not where visible page content lives, but it strongly affects document behavior and presentation.
example:
Title, meta tags, stylesheets, and scripts commonly live in the head.
mnemonic:
The head prepares the document.
recall:
- What kinds of things belong in the head?
- Why is the head different from visible content?
```

```concept-card
id: body-section
term: Body Section
parents:
- html
summary:
The body section contains the visible page content users read and interact with.
details:
It is the main content area of the document, including text, media, forms, and interactive markup.
example:
Paragraphs, buttons, images, and sections are placed inside the body.
mnemonic:
The body holds what the user sees.
recall:
- What belongs in the body?
- Why should metadata not be confused with body content?
```

```concept-card
id: language-declaration
term: Language Declaration
parents:
- html
summary:
The `lang` attribute declares the primary language of the document.
details:
It improves accessibility, pronunciation in screen readers, and language-aware processing by browsers and search engines.
example:
`<html lang="en">`
mnemonic:
Declare the language so tools can read the page correctly.
recall:
- Why is `lang` important?
- Which technologies benefit from correct language declaration?
```

```concept-card
id: metadata
term: Metadata
parents:
- html
related:
- head-section
summary:
Metadata describes the document rather than appearing as visible main content.
details:
It includes encoding, viewport settings, descriptions, social data, and other page-level information.
example:
`<meta charset="UTF-8">` and `<meta name="viewport" content="width=device-width, initial-scale=1.0">`
mnemonic:
Metadata describes the page behind the scenes.
recall:
- Which metadata tags matter most in everyday frontend work?
- Why is metadata important even though users do not see it directly?
```

```concept-card
id: block-versus-inline
term: Block Versus Inline Elements
parents:
- html
summary:
Block and inline elements differ in how they participate in document flow.
details:
Blocks typically start on new lines, while inline elements flow within text content.
example:
`<p>` is block-level by default, while `<span>` is inline by default.
mnemonic:
Blocks stack, inline flows.
recall:
- What makes a block element different from an inline element?
- Why does this distinction matter for real page structure?
```

```concept-card
id: data-attributes
term: data-* Attributes
parents:
- html
summary:
`data-*` attributes attach custom metadata to HTML elements.
details:
They are useful for DOM hooks, configuration values, and structured custom data that does not belong in standard HTML attributes.
example:
`<article data-product-id="42"></article>`
mnemonic:
Custom data belongs in data attributes, not invented attributes.
recall:
- When are `data-*` attributes appropriate?
- Why are they better than random custom attributes?
```
