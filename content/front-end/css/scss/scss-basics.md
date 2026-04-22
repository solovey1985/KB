# SCSS Basics

SCSS adds authoring features on top of CSS, then compiles down to standard CSS.

## Why teams use SCSS

SCSS helps with:

- variables
- mixins
- reusable functions
- modular partials
- structured nesting

## Variables

```scss
$color-primary: #2563eb;
$radius-card: 12px;

.button {
  background: $color-primary;
}
```

## Nesting

```scss
.card {
  padding: 1rem;

  &__title {
    font-weight: 700;
  }

  &:hover {
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
  }
}
```

Nesting is helpful, but too much nesting becomes hard to reason about.

## Mixins

```scss
@mixin focus-ring($color) {
  outline: 2px solid $color;
  outline-offset: 2px;
}

.button:focus-visible {
  @include focus-ring(#2563eb);
}
```

## Modules

Modern SCSS favors `@use` and `@forward` over older global `@import` patterns.

```scss
@use 'tokens' as t;

.button {
  color: t.$color-primary;
}
```

## SCSS versus CSS custom properties

SCSS variables are compile-time.

CSS custom properties are runtime.

That means SCSS variables are good for authoring convenience, while CSS variables are better for theming that changes in the browser.

## Interview reminders

- explain SCSS as a preprocessor, not a browser feature
- mention `@use` and `@forward` as the modern module approach
- distinguish compile-time variables from runtime CSS variables
