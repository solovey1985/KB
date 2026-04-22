# Advanced HTML Patterns

Advanced HTML is often about delivering resilient pages that cooperate well with CSS, JavaScript, search engines, and browsers.

## Data attributes

`data-*` attributes are useful when elements need custom structured metadata.

```html
<button data-action="save" data-entity-id="42">Save</button>
```

## Script loading

Scripts can block parsing if loaded carelessly.

```html
<script src="app.js" defer></script>
```

Key ideas:

- `defer` delays execution until after parsing
- `async` loads independently and executes as soon as ready
- script loading order affects behavior

## Metadata for SEO and sharing

Important metadata includes:

- `title`
- description
- canonical URL
- social sharing tags when relevant

## Progressive enhancement

Pages should remain meaningful and usable even before full JavaScript hydration or enhancement.

This is one reason why strong HTML structure still matters in modern frontend apps.

## Interview reminders

- explain `defer` and `async` carefully
- mention metadata as machine-facing page description
- progressive enhancement is an HTML mindset, not only a JS concern
