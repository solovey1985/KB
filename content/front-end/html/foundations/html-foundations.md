# HTML Document Foundations

HTML provides the structure and meaning of a web page.

It is not responsible for presentation or behavior by itself. CSS styles the page, and JavaScript adds behavior.

## Basic document shape

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Product Details</title>
  </head>
  <body>
    <h1>Product Details</h1>
    <p>Structured content goes here.</p>
  </body>
</html>
```

## What the major pieces do

- `<!DOCTYPE html>` tells the browser to use modern HTML parsing mode.
- `<html>` is the root element.
- `lang` helps accessibility and search engines understand the document language.
- `<head>` contains metadata and linked resources.
- `<body>` contains visible page content.

## Metadata

Metadata matters for:

- encoding
- responsive behavior
- page title
- descriptions for search and sharing

```html
<meta name="description" content="Practical HTML study notes" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
```

## Block and inline elements

Some elements naturally behave like blocks, others like inline content.

```html
<p>This is a paragraph.</p>
<span>This is inline text.</span>
```

That difference matters for layout, semantics, and how the browser flows content.

## Comments and data attributes

HTML comments are useful for author notes but should not carry hidden business logic.

```html
<!-- Product summary section -->
<article data-product-id="42"></article>
```

`data-*` attributes are useful for attaching custom metadata to elements without inventing invalid HTML attributes.

## Interview reminders

- explain HTML as structure and semantics
- mention `lang`, `charset`, and `viewport` when discussing document setup
- distinguish visible content from metadata
