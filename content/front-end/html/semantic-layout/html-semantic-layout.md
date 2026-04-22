# HTML Semantic Layout and Content Meaning

Semantic HTML means choosing elements based on what content means, not just how it should look.

## Why semantics matter

Semantic markup improves:

- accessibility
- SEO and machine understanding
- maintainability
- collaboration between developers

## Common semantic layout elements

```html
<header>
  <nav>
    <a href="/">Home</a>
    <a href="/about">About</a>
  </nav>
</header>

<main>
  <article>
    <h1>Designing Better Forms</h1>
    <p>Article content...</p>
  </article>

  <aside>
    Related links
  </aside>
</main>

<footer>
  <small>&copy; 2026 Example Inc.</small>
</footer>
```

## `section` versus `article`

Use `article` for content that stands on its own.

Use `section` for a thematic grouping inside a larger document.

## `button` versus `a`

Use:

- `<a>` for navigation
- `<button>` for actions

This distinction is important for semantics, keyboard behavior, and accessibility.

## Text-level semantics

```html
<p><strong>Important:</strong> Save your changes.</p>
<p>Press <em>Enter</em> to continue.</p>
<p><mark>Limited time offer</mark></p>
```

Useful semantic tags include:

- `strong`
- `em`
- `mark`
- `small`
- `time`
- `figure` / `figcaption`

## Interview reminders

- semantics is about meaning, not styling
- a layout full of generic `div`s is often a missed opportunity
- `button` versus `a` is a recurring frontend interview distinction
