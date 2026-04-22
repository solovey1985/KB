# CSS Foundations

CSS controls the visual presentation of HTML by matching elements with selectors and applying property-value rules.

## How CSS is included

The most common and maintainable approach is an external stylesheet.

```html
<link rel="stylesheet" href="styles.css" />
```

Inline styles and `<style>` blocks exist, but they are weaker defaults for larger codebases.

## Selectors

Selectors choose which elements a rule applies to.

```css
body {
  font-family: system-ui, sans-serif;
}

.card {
  border-radius: 12px;
}

#hero {
  background: #0f172a;
}

button:hover {
  background: #2563eb;
}
```

Common selectors:

- element selectors
- class selectors
- ID selectors
- attribute selectors
- pseudo-classes
- pseudo-elements

## Class versus ID

Classes are reusable across many elements.

IDs are intended for unique elements.

For styling, classes are usually the better default because they are easier to reuse and keep less rigid than ID-heavy CSS.

## Cascade and specificity

When multiple rules match the same element, CSS resolves conflicts through:

1. importance
2. specificity
3. source order

```css
.button {
  color: blue;
}

button.primary {
  color: red;
}
```

The second selector is more specific, so it wins.

## Pseudo-classes and pseudo-elements

Pseudo-classes style elements based on state.

Pseudo-elements style virtual parts of an element.

```css
a:hover {
  text-decoration: underline;
}

.quote::before {
  content: '"';
}
```

## Reset and normalize

Browsers ship default styles that differ slightly.

A reset or normalization layer makes layout behavior more predictable.

```css
* {
  box-sizing: border-box;
}

body,
h1,
p,
ul {
  margin: 0;
}
```

## Interview reminders

- explain the cascade as rule conflict resolution
- say classes are the normal styling default
- distinguish `:hover` from `::before`
- mention resets as consistency tools, not design systems
