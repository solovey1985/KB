# 08 — CSS & SCSS Deep Dive

> Comprehensive guide to CSS layout, responsive design, SCSS architecture, and modern CSS patterns — with real code from the Angular Student Dashboard and **25+ interview questions**.

---

## Table of Contents

1. [CSS Box Model](#1-css-box-model)
2. [Flexbox — One-Dimensional Layout](#2-flexbox--one-dimensional-layout)
3. [CSS Grid — Two-Dimensional Layout](#3-css-grid--two-dimensional-layout)
4. [Flexbox vs Grid — When to Use Which](#4-flexbox-vs-grid--when-to-use-which)
5. [Responsive Design & Mobile-First](#5-responsive-design--mobile-first)
6. [CSS Custom Properties vs SCSS Variables](#6-css-custom-properties-vs-scss-variables)
7. [SCSS Features in Depth](#7-scss-features-in-depth)
8. [Positioning](#8-positioning)
9. [Specificity and the Cascade](#9-specificity-and-the-cascade)
10. [Pseudo-Classes and Pseudo-Elements](#10-pseudo-classes-and-pseudo-elements)
11. [Animations and Transitions](#11-animations-and-transitions)
12. [Modern CSS Features](#12-modern-css-features)
13. [Accessibility (a11y) in CSS](#13-accessibility-a11y-in-css)
14. [Angular Component Styles](#14-angular-component-styles)
15. [SCSS Architecture: File Organization](#15-scss-architecture-file-organization)
16. [Interview Questions & Answers](#16-interview-questions--answers)

---

## 1. CSS Box Model

Every element in CSS is a rectangular box. The box model defines how `width`, `height`, `padding`, `border`, and `margin` interact.

```
┌──────────────── margin ────────────────┐
│  ┌──────────── border ──────────────┐  │
│  │  ┌──────── padding ──────────┐   │  │
│  │  │  ┌───── content ──────┐   │   │  │
│  │  │  │                    │   │   │  │
│  │  │  └────────────────────┘   │   │  │
│  │  └───────────────────────────┘   │  │
│  └──────────────────────────────────┘  │
└────────────────────────────────────────┘
```

### content-box vs border-box

```css
/* DEFAULT: content-box — width does NOT include padding/border */
.box-content {
  box-sizing: content-box;  /* browser default */
  width: 200px;
  padding: 20px;
  border: 1px solid black;
  /* Actual rendered width: 200 + 20 + 20 + 1 + 1 = 242px */
}

/* PREFERRED: border-box — width INCLUDES padding and border */
.box-border {
  box-sizing: border-box;
  width: 200px;
  padding: 20px;
  border: 1px solid black;
  /* Actual rendered width: 200px (content shrinks to 158px) */
}
```

**In our app** — we apply `border-box` globally so all calculations are intuitive:

```scss
// src/styles.scss:110-114
*,
*::before,
*::after {
  box-sizing: border-box;
}
```

> **Interview tip:** Always mention that `border-box` is the preferred default for modern CSS. Frameworks like Tailwind and Bootstrap set it globally.

---

## 2. Flexbox — One-Dimensional Layout

Flexbox handles layout in **one direction** at a time — either a row or a column. It excels at distributing space along a single axis.

### Key Properties

| Container (`display: flex`)  | Item (children)          |
|------------------------------|--------------------------|
| `flex-direction`             | `flex-grow`              |
| `flex-wrap`                  | `flex-shrink`            |
| `justify-content` (main)    | `flex-basis`             |
| `align-items` (cross)       | `align-self`             |
| `align-content` (multi-row) | `order`                  |
| `gap`                        | `flex` (shorthand)       |

### Real Code: Centering with Flexbox

The most common interview question: **"How do you center a div?"**

```scss
// src/styles/_mixins.scss:60-64 — The flex-center mixin
@mixin flex-center {
  display: flex;
  align-items: center;     // Centers on the CROSS axis (vertical in row)
  justify-content: center; // Centers on the MAIN axis (horizontal in row)
}
```

Used in the login page — the classic "center content on the full viewport" pattern:

```scss
// src/app/pages/login/login.component.scss:28-37
.login-page {
  @include flex-center;
  min-height: 100vh;
  background: linear-gradient(135deg, $color-bg 0%, $color-primary-light 50%, $color-bg 100%);
  padding: $space-lg;
}
```

### Real Code: Flex Column Layout

The sidebar uses a vertical flex layout — items stack downward with a gap:

```scss
// src/styles/_mixins.scss:67-73 — Parameterized mixin
@mixin flex-column($gap: 0) {
  display: flex;
  flex-direction: column;
  @if $gap != 0 {
    gap: $gap;
  }
}

// src/app/pages/dashboard/dashboard.component.scss:63
.sidebar {
  @include flex-column($space-lg);
}
```

### Real Code: Flex Wrap for Filter Pills

The category filter buttons wrap to the next line when they run out of space:

```scss
// src/app/pages/enrollment/enrollment.component.scss:89-92
.category-filters {
  display: flex;
  gap: $space-sm;
  flex-wrap: wrap;  // Items wrap to next line instead of overflowing
}
```

### flex-grow, flex-shrink, flex-basis

```css
/* flex: <grow> <shrink> <basis> */
.item { flex: 1 1 auto; }

/* Common patterns: */
.grow-equal  { flex: 1; }         /* = flex: 1 1 0% — equal size */
.no-shrink   { flex: 0 0 auto; }  /* = fixed size, won't shrink */
.fill-space  { flex: 1 1 auto; }  /* = fill remaining space */
```

Used for card action buttons — they share available space equally:

```scss
// src/app/components/course-card/course-card.component.scss:130-133
.btn {
  flex: 1 1 auto;     // Buttons grow to share space equally
  text-align: center;
  min-width: 0;        // Allow button text to truncate if needed
}
```

---

## 3. CSS Grid — Two-Dimensional Layout

Grid handles layout in **both directions** simultaneously — rows AND columns. It excels at complex page layouts.

### Key Properties

| Container (`display: grid`)           | Item (children)        |
|---------------------------------------|------------------------|
| `grid-template-columns`               | `grid-column`          |
| `grid-template-rows`                  | `grid-row`             |
| `grid-template-areas`                 | `grid-area`            |
| `gap` / `column-gap` / `row-gap`     | `justify-self`         |
| `justify-items` / `justify-content`   | `align-self`           |
| `align-items` / `align-content`       | `place-self`           |
| `grid-auto-flow`                      | `order`                |

### Real Code: Dashboard Sidebar + Content Layout

This is the signature Grid use case — two-column page layout:

```scss
// src/app/pages/dashboard/dashboard.component.scss:29-45
.dashboard-layout {
  // Mobile: single column (Flexbox fallback)
  display: flex;
  flex-direction: column;
  gap: $space-lg;
  padding: clamp(#{$space-md}, 4vw, #{$space-2xl});
  max-width: $max-width-content;
  margin: 0 auto;

  // Tablet+: switch to Grid
  @include respond-to('tablet') {
    display: grid;
    grid-template-columns: $sidebar-width 1fr;  // 280px sidebar + flexible content
    gap: $space-2xl;
    padding: $space-2xl;
  }

  @include respond-to('desktop-lg') {
    gap: $space-3xl;
  }
}
```

### auto-fill vs auto-fit

Both create a responsive grid without media queries, but they behave differently with empty space:

```scss
// src/app/pages/dashboard/dashboard.component.scss:154-162
.course-grid {
  display: grid;
  grid-template-columns: 1fr;  // Mobile: single column
  gap: $space-lg;

  @include respond-to('tablet') {
    // auto-fill: creates empty tracks, preserves column width
    grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  }
}
```

**auto-fill** creates columns for as many 280px-wide items as fit. If there are only 2 items but room for 4, the 2 empty tracks still exist (items don't stretch).

**auto-fit** collapses empty tracks, so items stretch to fill the full width.

```css
/* Visual difference with 2 items in a wide container: */

/* auto-fill: [item][item][empty][empty] — items stay 280px-ish */
grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));

/* auto-fit:  [item           ][item           ] — items stretch */
grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
```

### Spanning the Full Grid

```scss
// src/app/pages/enrollment/enrollment.component.scss:123-128
.no-results {
  grid-column: 1 / -1;  // Spans from column 1 to the LAST column
  text-align: center;
  color: var(--color-text-muted);
  padding: $space-4xl 0;
}
```

### Grid Gotcha: min-width: auto

Grid items default to `min-width: auto`, which can cause overflow:

```scss
// src/app/pages/dashboard/dashboard.component.scss:93-96
.content {
  // min-width: 0 prevents grid blowout when children overflow.
  // Grid items default to min-width: auto, which refuses to shrink
  // below their content's intrinsic size.
  min-width: 0;
}
```

> **Interview tip:** If you see a grid child overflowing its container, the fix is `min-width: 0`. This is one of the most common Grid bugs.

---

## 4. Flexbox vs Grid — When to Use Which

This is one of the most common CSS interview questions. Here's the practical answer, with examples from our app:

| Criteria                    | Flexbox                        | Grid                            |
|-----------------------------|--------------------------------|---------------------------------|
| **Direction**               | One axis (row OR column)       | Two axes (row AND column)       |
| **Content vs Layout**       | Content dictates size          | Layout dictates size            |
| **Use case**                | Aligning items in a line       | Page/section layout             |
| **Wrapping**                | Items wrap naturally            | Explicit column/row definitions |
| **Overlap**                 | Not possible                    | Items can overlap (grid areas)  |

### Side-by-Side App Examples

| Component              | Layout System | Why                                                        |
|------------------------|--------------|-------------------------------------------------------------|
| `.dashboard-layout`    | **Grid**     | 2D: sidebar + content columns with aligned rows             |
| `.course-grid`         | **Grid**     | 2D: auto-fill columns that adapt to viewport width          |
| `.sidebar`             | **Flex**     | 1D: vertical stack of profile card + button                  |
| `.card-header`         | **Flex**     | 1D: badge and credits on one row with space-between          |
| `.card-actions`        | **Flex**     | 1D: buttons in a row, wrapping on small screens              |
| `.category-filters`    | **Flex**     | 1D: filter pills in a wrapping row                           |
| `.detail-header`       | **Flex**     | 1D: title block + meta, wraps to column on mobile            |
| `.login-page`          | **Flex**     | 1D: single centered child (flex-center pattern)              |
| `.alert-error`         | **Flex**     | 1D: message text + dismiss button on one row                 |
| `.loading-container`   | **Flex**     | 1D: spinner + text stacked vertically                        |

**Rule of thumb:** If you're aligning items along a single line, use Flexbox. If you need to control both columns and rows, use Grid.

---

## 5. Responsive Design & Mobile-First

### Mobile-First vs Desktop-First

**Mobile-first** = start with mobile styles, add complexity for larger screens using `min-width`.
**Desktop-first** = start with desktop styles, override for smaller screens using `max-width`.

Mobile-first is preferred because:
- Mobile styles are simpler (less CSS to parse on slow devices)
- Progressive enhancement is more robust than graceful degradation
- Mobile traffic exceeds desktop globally

### Real Code: Responsive Breakpoint Mixins

```scss
// src/styles/_variables.scss:98-105
$breakpoints: (
  'mobile-sm':  320px,
  'mobile':     480px,
  'tablet':     768px,
  'desktop':    1024px,
  'desktop-lg': 1200px,
  'desktop-xl': 1440px,
);

// src/styles/_mixins.scss:23-33 — Mobile-first (min-width)
@mixin respond-to($breakpoint) {
  $value: map.get($breakpoints, $breakpoint);

  @if $value == null {
    @error "Unknown breakpoint: #{$breakpoint}. Available: #{map.keys($breakpoints)}";
  }

  @media (min-width: $value) {
    @content;  // @content = whatever CSS the caller passes in {}
  }
}

// src/styles/_mixins.scss:37-47 — Desktop-first (max-width)
@mixin respond-below($breakpoint) {
  $value: map.get($breakpoints, $breakpoint);

  @media (max-width: ($value - 1px)) {
    @content;
  }
}
```

### Real Code: Dashboard Goes from Stacked to Grid

```scss
// src/app/pages/dashboard/dashboard.component.scss
.dashboard-layout {
  // MOBILE: single column flex stack
  display: flex;
  flex-direction: column;
  gap: $space-lg;

  // TABLET+: switch to two-column grid
  @include respond-to('tablet') {
    display: grid;
    grid-template-columns: $sidebar-width 1fr;
    gap: $space-2xl;
  }
}
```

### Real Code: Buttons Stack on Mobile

```scss
// src/app/pages/course-detail/course-detail.component.scss:138-145
.actions {
  display: flex;
  gap: $space-md;
  flex-wrap: wrap;

  @include respond-below('tablet') {
    flex-direction: column;  // Stack buttons vertically
  }
}

.btn {
  @include respond-below('tablet') {
    width: 100%;             // Full-width on mobile
    text-align: center;
  }
}
```

### clamp() for Fluid Values

`clamp(min, preferred, max)` — a single value that scales between a minimum and maximum:

```scss
// src/app/pages/dashboard/dashboard.component.scss:35
padding: clamp(#{$space-md}, 4vw, #{$space-2xl});
// At 320px viewport: 12px
// At 600px viewport: 24px (4% of 600)
// At 800px viewport: 24px (capped)

// Fluid typography
h1 {
  font-size: clamp(#{$font-size-xl}, 4vw, #{$font-size-2xl});
  // 1.5rem minimum, scales with viewport, caps at 2rem
}
```

### min() for Responsive Max-Width

```scss
// src/app/pages/course-detail/course-detail.component.scss:19
.detail-page {
  // min() picks the smaller value — responsive without a media query
  max-width: min(#{$max-width-narrow}, calc(100vw - #{$space-3xl}));
  // On wide screens: 800px
  // On narrow screens: viewport width minus 32px
}
```

### Responsive Visibility Helpers

```scss
// src/styles.scss:351-371
.hide-mobile {
  @include respond-below('tablet') {
    display: none !important;
  }
}

.show-mobile {
  display: none !important;
  @include respond-below('tablet') {
    display: block !important;
  }
}
```

---

## 6. CSS Custom Properties vs SCSS Variables

This is a critical distinction that interviewers love to ask about:

| Feature                   | SCSS Variables (`$var`)    | CSS Custom Properties (`--var`) |
|---------------------------|----------------------------|---------------------------------|
| **When resolved**         | Compile-time               | Runtime (in browser)            |
| **Can change at runtime** | No                         | Yes (JS, media queries, :hover) |
| **Scoping**               | SCSS block scope           | DOM cascade (inherited)         |
| **Math/logic**            | Full SCSS functions         | `calc()` only                    |
| **Media queries**         | Can't change inside         | Can change inside               |
| **Browser support**       | Any (compiles away)         | All modern browsers             |

### Real Code: Both Working Together

SCSS variables define the values at build time. CSS custom properties expose them at runtime:

```scss
// src/styles/_variables.scss:16-18 — Build-time constants
$color-primary:       #1a237e;
$color-primary-dark:  #0d1642;
$color-primary-light: #e3f2fd;

// src/styles.scss:27-31 — Runtime tokens
:root {
  --color-primary:       #{$color-primary};
  --color-primary-dark:  #{$color-primary-dark};
  --color-primary-light: #{$color-primary-light};
}
```

Components use CSS custom properties when the value might need to change at runtime:

```scss
// src/app/pages/dashboard/dashboard.component.scss — uses var()
h1 { color: var(--color-primary); }
```

SCSS variables are used when compile-time logic is needed (maps, math, conditions):

```scss
// src/app/pages/dashboard/dashboard.component.scss — uses SCSS
border: 1px solid shade($color-danger-light, 15%);  // SCSS function
@include respond-to('tablet') { ... }                // needs $breakpoints map
```

### Scoped CSS Custom Properties

CSS custom properties can be overridden per-element — powerful for component variants:

```scss
// src/app/components/course-card/course-card.component.scss:22-38
.course-card {
  --card-accent: var(--color-border);      // Default accent
  border-left: 3px solid var(--card-accent);

  &.enrolled {
    --card-accent: var(--color-success);   // Green accent when enrolled
    background: linear-gradient(135deg, $color-bg-white 85%, $color-success-light);
  }

  &.full {
    --card-accent: var(--color-danger-light);  // Red accent when full
    opacity: 0.7;
  }
}
```

### CSS Custom Properties with Fallbacks

Inline styles can reference CSS custom properties with fallback values:

```scss
// src/app/components/enrollment-status/enrollment-status.component.ts
.status-badge {
  border-radius: var(--radius-xl, 12px);   // Fallback if --radius-xl not defined
  transition: background var(--transition-fast, 0.15s ease);
}
```

---

## 7. SCSS Features in Depth

### 7.1 Variables and Data Types

```scss
// src/styles/_variables.scss
$color-primary: #1a237e;        // Color
$space-lg: 16px;                // Number with unit
$font-family-base: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;  // String/list
$sidebar-width: 280px;          // Number
```

### 7.2 Nesting

SCSS nesting mirrors HTML structure. The `&` refers to the parent selector:

```scss
// src/app/pages/course-detail/course-detail.component.scss:62-77
.detail-body {
  section {
    margin-bottom: $space-xl;

    h2 {
      color: var(--color-text);
      font-size: $font-size-lg;
    }

    p, ul {
      color: var(--color-text-muted);
      line-height: 1.6;
    }
  }
}
```

The `&` operator:

```scss
// src/app/pages/enrollment/enrollment.component.scss
.filter-btn {
  background: white;

  &:hover { background: var(--color-primary-light); }     // .filter-btn:hover
  &.active { background: var(--color-primary); }           // .filter-btn.active
}
```

### 7.3 Maps

SCSS maps are key-value pairs — like objects in JavaScript:

```scss
// src/styles/_variables.scss:47-53
$status-colors: (
  'available':  ($color-success-light, #2e7d32),
  'enrolled':   ($color-info-light, $color-info),
  'waitlisted': ($color-warning-light, $color-warning),
  'full':       ($color-danger-light, $color-danger-dark),
  'dropped':    (#f5f5f5, #757575),
);

// src/styles/_variables.scss:98-105
$breakpoints: (
  'mobile-sm':  320px,
  'mobile':     480px,
  'tablet':     768px,
  'desktop':    1024px,
  'desktop-lg': 1200px,
  'desktop-xl': 1440px,
);
```

Accessing map values:

```scss
// src/styles/_mixins.scss:24
$value: map.get($breakpoints, $breakpoint);

// src/styles/_functions.scss:40
$value: map.get($breakpoints, $name);
```

### 7.4 @each Loop (Iterating Maps)

Generates utility classes from the status map:

```scss
// src/styles.scss:330-340
@each $status, $colors in $status-colors {
  .status-#{$status} {
    display: inline-block;
    padding: 2px 10px;
    border-radius: var(--radius-xl);
    font-size: $font-size-xs;
    font-weight: 600;
    background: list.nth($colors, 1);
    color: list.nth($colors, 2);
  }
}
// Generates: .status-available, .status-enrolled, .status-waitlisted, etc.
```

### 7.5 @for Loop

Generates spacing utility classes:

```scss
// src/styles.scss:283-294
@for $i from 0 through 8 {
  .mt-#{$i} { margin-top: #{$i * 4}px; }
  .mb-#{$i} { margin-bottom: #{$i * 4}px; }
  .ml-#{$i} { margin-left: #{$i * 4}px; }
  .mr-#{$i} { margin-right: #{$i * 4}px; }
  .pt-#{$i} { padding-top: #{$i * 4}px; }
  .pb-#{$i} { padding-bottom: #{$i * 4}px; }
  .pl-#{$i} { padding-left: #{$i * 4}px; }
  .pr-#{$i} { padding-right: #{$i * 4}px; }
  .p-#{$i}  { padding: #{$i * 4}px; }
  .m-#{$i}  { margin: #{$i * 4}px; }
}
// Generates: .mt-0 (0px) through .mt-8 (32px), etc.
```

### 7.6 Mixins with @content Blocks

Mixins can accept a `@content` block — the caller injects CSS into the mixin:

```scss
// src/styles/_mixins.scss:23-33 — Mixin definition
@mixin respond-to($breakpoint) {
  $value: map.get($breakpoints, $breakpoint);
  @media (min-width: $value) {
    @content;  // ← CSS from the caller goes here
  }
}

// Usage in component — caller passes the @content block
.dashboard-layout {
  @include respond-to('tablet') {
    display: grid;                            // ← this is @content
    grid-template-columns: $sidebar-width 1fr;  // ← this too
  }
}
```

### 7.7 @function vs @mixin

```
@function → computes and RETURNS a value (used in property values)
@mixin   → generates and OUTPUTS CSS (used with @include)
```

```scss
// src/styles/_functions.scss:21-23 — FUNCTION: returns a value
@function rem($px, $base: 16) {
  @return math.div($px, $base) * 1rem;
}
// Usage: font-size: rem(14);  →  font-size: 0.875rem;

// src/styles/_mixins.scss:94-101 — MIXIN: outputs CSS
@mixin spinner($size: 40px, $color: $color-primary) {
  width: $size;
  height: $size;
  border: 4px solid $color-border;
  border-top-color: $color;
  border-radius: $radius-round;
  animation: spin 0.8s linear infinite;
}
// Usage: @include spinner;  →  outputs 6 CSS properties
```

### 7.8 @use and @forward (Module System)

Modern SCSS replaces `@import` with `@use` and `@forward`:

```scss
// @use loads a module and namespaces it
@use 'styles/variables' as vars;
// Access: vars.$color-primary

// @use with * removes the namespace
@use 'styles/variables' as *;
// Access: $color-primary (no prefix)

// How our component files load shared SCSS:
// src/app/pages/dashboard/dashboard.component.scss:14-16
@use 'styles/variables' as *;
@use 'styles/mixins' as *;
@use 'styles/functions' as *;
```

`@use` differs from `@import`:
- Each file is only loaded once (no duplication)
- Variables are scoped to the namespace
- Encourages explicit dependencies
- `@import` is deprecated in Dart Sass

### 7.9 @extend vs @mixin

```scss
// @extend: creates SHARED selectors (smaller CSS)
%btn-base { padding: 8px 16px; border: none; }
.btn-primary { @extend %btn-base; background: blue; }
.btn-danger  { @extend %btn-base; background: red; }
// Output: .btn-primary, .btn-danger { padding: 8px 16px; border: none; }

// @mixin: DUPLICATES the CSS at each include (no shared selectors)
@mixin btn-base { padding: 8px 16px; border: none; }
.btn-primary { @include btn-base; background: blue; }
.btn-danger  { @include btn-base; background: red; }
// Output: .btn-primary { padding: 8px 16px; border: none; background: blue; }
//         .btn-danger  { padding: 8px 16px; border: none; background: red; }
```

**We use mixins** because Angular's emulated view encapsulation adds attribute selectors to component styles (`[_ngcontent-abc]`). `@extend` across components would break because the shared selector wouldn't have the right encapsulation attribute.

---

## 8. Positioning

### Position Values

| Value      | Offset from             | Creates stacking context | In flow? |
|------------|------------------------|--------------------------|----------|
| `static`   | N/A (default)          | No                       | Yes      |
| `relative` | Its normal position     | Yes (if z-index set)     | Yes      |
| `absolute` | Nearest positioned ancestor | Yes                | No       |
| `fixed`    | Viewport               | Yes                      | No       |
| `sticky`   | Scroll threshold       | Yes                      | Yes (until stuck) |

### Real Code: Enrollment Bar (relative + absolute)

Text sits on TOP of the progress fill — classic relative/absolute pattern:

```scss
// src/app/pages/course-detail/course-detail.component.scss:99-120
.enrollment-bar {
  position: relative;        // Creates the positioning context
  background: var(--color-border);
  border-radius: $radius-sm;
  height: 24px;
  overflow: hidden;          // Clips the fill at rounded corners
}

.enrollment-fill {
  background: var(--color-info-fill);
  height: 100%;
  transition: width $transition-slow;
}

.enrollment-text {
  position: absolute;        // Removed from flow, positioned inside parent
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);  // Center precisely
  font-size: $font-size-sm;
  font-weight: 500;
}
```

**How it works:**
1. `.enrollment-bar` has `position: relative` — this makes it the "positioning context"
2. `.enrollment-text` has `position: absolute` — it's positioned relative to the bar, not the page
3. `top: 50%; left: 50%` moves the element's top-left corner to the center
4. `transform: translate(-50%, -50%)` shifts it back by half its own size — true centering

### Real Code: Sticky Sidebar

```scss
// src/app/pages/dashboard/dashboard.component.scss:68-75
.sidebar {
  @include respond-to('tablet') {
    position: sticky;
    top: $space-2xl;  // Sticks 24px from viewport top when scrolling
    max-height: calc(100vh - #{$space-2xl * 2});
    overflow-y: auto;
  }
}
```

`position: sticky` is a hybrid — it behaves like `relative` until the element reaches the `top` threshold, then it "sticks" like `fixed` within its containing block.

### Real Code: Overlay (fixed + inset)

```scss
// src/styles/_mixins.scss:152-158
@mixin overlay($opacity: 0.5) {
  position: fixed;
  inset: 0;       // Shorthand for top: 0; right: 0; bottom: 0; left: 0;
  background: rgba(0, 0, 0, $opacity);
  z-index: $z-overlay;
}
```

---

## 9. Specificity and the Cascade

### Specificity Hierarchy (highest to lowest)

| Specificity | Selector Type                   | Example                |
|-------------|---------------------------------|------------------------|
| 1-0-0-0     | Inline styles                   | `style="color: red"`  |
| 0-1-0-0     | ID selectors                    | `#header`              |
| 0-0-1-0     | Class, attribute, pseudo-class  | `.btn`, `[type=text]`, `:hover` |
| 0-0-0-1     | Element, pseudo-element         | `div`, `::before`      |
| 0-0-0-0     | Universal, combinators          | `*`, `>`, `+`, `~`    |

### Specificity Math

```css
/* Specificity: 0-0-1-1 (1 class + 1 element) */
div.alert { }

/* Specificity: 0-0-2-0 (2 classes) */
.alert.alert-error { }

/* Specificity: 0-0-2-1 (2 classes + 1 pseudo) */
.btn:hover:not(:disabled) { }

/* Specificity: 0-1-0-0 (1 ID — don't use IDs for styling!) */
#sidebar { }
```

### The Cascade Order

When specificity is equal, the cascade decides:
1. **Origin** (user-agent → user → author)
2. **Specificity** (inline > ID > class > element)
3. **Source order** (later wins)
4. **!important** (reverses the cascade — avoid if possible)

### Real Code: :not() and Specificity

`:not()` takes the specificity of its argument:

```scss
// src/app/pages/login/login.component.scss
.btn-login:hover:not(:disabled) { background: var(--color-primary-dark); }
// Specificity: 0-0-3-0 (3 pseudo-classes: :hover + :not + :disabled)
```

---

## 10. Pseudo-Classes and Pseudo-Elements

### Pseudo-Classes (single colon)

State-based selectors:

```scss
// Our app uses these pseudo-classes extensively:

// :hover — mouse over
.btn-browse:hover:not(:disabled) { background: var(--color-primary-dark); }

// :focus — any focus (keyboard or mouse)
.search-input:focus { border-color: var(--color-primary); }

// :focus-visible — keyboard focus ONLY (not mouse clicks)
:focus-visible { outline: 2px solid var(--color-primary); }

// :disabled — disabled form elements
.btn-login:disabled { opacity: 0.7; cursor: not-allowed; }

// :not() — negation
.btn-login:hover:not(:disabled) { background: var(--color-primary-dark); }

// :first-child, :last-child, :nth-child()
// (not used in our app but common in interviews)

// .active — not a pseudo-class, but Angular binds it dynamically:
.filter-btn.active { background: var(--color-primary); }
```

### Pseudo-Elements (double colon)

Create virtual elements:

```scss
// ::before, ::after — virtual content
// ::placeholder — placeholder text
.search-input::placeholder {
  color: var(--color-text-faint);
}

// ::-webkit-scrollbar — custom scrollbar (Webkit)
// src/styles/_mixins.scss:183-196
@mixin custom-scrollbar($width: 6px, $thumb: #bdbdbd, $track: transparent) {
  &::-webkit-scrollbar { width: $width; }
  &::-webkit-scrollbar-track { background: $track; }
  &::-webkit-scrollbar-thumb {
    background: $thumb;
    border-radius: $width / 2;
  }
  scrollbar-width: thin;            // Firefox
  scrollbar-color: $thumb $track;   // Firefox
}
```

---

## 11. Animations and Transitions

### Transitions vs Animations

| Feature        | `transition`                    | `animation`                       |
|----------------|--------------------------------|------------------------------------|
| Triggers       | State change (hover, class)     | Automatic or class-based           |
| Keyframes      | Only start → end               | Multiple keyframes (0% → 100%)    |
| Repetition     | Once per trigger               | Can loop (infinite, count)         |
| Direction      | Only forward                    | Can alternate                      |
| Complexity     | Simple A → B                    | Complex multi-step                 |

### Real Code: Transitions

```scss
// src/app/components/course-card/course-card.component.scss — card hover
@mixin card-base {
  transition: box-shadow $transition-base;  // 0.2s ease
  &:hover { box-shadow: $shadow-md; }
}

// src/app/pages/enrollment/enrollment.component.scss — multi-property
.filter-btn {
  transition: background $transition-base,
              color $transition-base,
              border-color $transition-base;
}

// src/app/pages/login/login.component.scss — button lift effect
.btn-login {
  &:hover:not(:disabled) {
    box-shadow: $shadow-md;
    transform: translateY(-1px);  // Subtle lift
  }
  &:active:not(:disabled) {
    transform: translateY(0);     // Press back down
    box-shadow: $shadow-sm;
  }
}
```

### Real Code: Keyframe Animations

```scss
// src/styles.scss:202-236 — Global keyframes

// Spinner rotation
@keyframes spin {
  to { transform: rotate(360deg); }
}

// Fade in with slight slide
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(4px); }
  to   { opacity: 1; transform: translateY(0); }
}

// Slide up (for login card)
@keyframes slideUp {
  from { opacity: 0; transform: translateY(20px); }
  to   { opacity: 1; transform: translateY(0); }
}

// Pulse (for skeleton loaders)
@keyframes pulse {
  0%, 100% { opacity: 1; }
  50%      { opacity: 0.5; }
}
```

Used in components:

```scss
// src/styles/_mixins.scss:94-101 — Spinner mixin
@mixin spinner($size: 40px, $color: $color-primary) {
  width: $size;
  height: $size;
  border: 4px solid $color-border;
  border-top-color: $color;
  border-radius: $radius-round;
  animation: spin 0.8s linear infinite;  // Runs forever
}

// Page fade-in
.enrollment-page {
  animation: fadeIn $transition-slow;  // 0.3s, runs once
}

// Login card slide-up
.login-card {
  animation: slideUp $transition-slow;
}
```

---

## 12. Modern CSS Features

### :has() — The Parent Selector

`:has()` selects an element based on its descendants. It's essentially "if this element contains X":

```scss
// src/app/components/course-card/course-card.component.scss:50-55
.course-card.full {
  &:has(.enrollment-fill) {
    .enrollment-fill {
      background: var(--color-text-light);  // Gray bar when full
    }
  }
}
```

`:has()` is supported in Chrome 105+, Safari 15.4+, Firefox 121+.

### :is() and :where()

`:is()` groups selectors (takes highest specificity of its arguments):

```scss
// src/app/components/course-card/course-card.component.scss:106-111
:is(.instructor, .schedule) {
  margin: 2px 0;
  font-size: $font-size-sm;
  color: var(--color-text-muted);
  @include text-truncate;
}
// Equivalent to: .instructor, .schedule { ... }
// But more readable and works better with nesting
```

`:where()` is identical but has **zero specificity** — useful for defaults:

```css
/* :is() — takes highest specificity of arguments */
:is(.a, #b) { }  /* Specificity: 0-1-0-0 (from #b) */

/* :where() — always zero specificity */
:where(.a, #b) { }  /* Specificity: 0-0-0-0 */
```

### inset

Shorthand for `top`, `right`, `bottom`, `left`:

```scss
// src/styles/_mixins.scss:154-155
@mixin overlay($opacity: 0.5) {
  position: fixed;
  inset: 0;  // = top: 0; right: 0; bottom: 0; left: 0;
}
```

### aspect-ratio

```scss
// src/styles/_mixins.scss:176-179
@mixin aspect-ratio($ratio: 16/9) {
  aspect-ratio: $ratio;
  overflow: hidden;
}
```

### text-wrap: pretty

Avoids orphaned words (single word on the last line):

```scss
// src/styles.scss:182
p {
  text-wrap: pretty;  // Browser intelligently wraps to avoid orphans
}
```

### Linear Gradients

```scss
// src/app/pages/login/login.component.scss — Background gradient
.login-page {
  background: linear-gradient(135deg, $color-bg 0%, $color-primary-light 50%, $color-bg 100%);
}

// src/app/components/course-card/course-card.component.scss — Subtle card tint
&.enrolled {
  background: linear-gradient(135deg, $color-bg-white 85%, $color-success-light);
}

// Progress bar gradient
.enrollment-fill {
  background-image: linear-gradient(to right, var(--color-info-fill), var(--color-info));
}
```

### margin-inline and Logical Properties

```scss
// src/styles.scss:302
.mx-auto { margin-inline: auto; }  // Centers block elements horizontally
// margin-inline = margin-left + margin-right (writing-direction aware)
```

---

## 13. Accessibility (a11y) in CSS

### :focus-visible

Only shows focus outlines on keyboard navigation, not mouse clicks:

```scss
// src/styles.scss:401-408
:focus-visible {
  outline: 2px solid var(--color-primary);
  outline-offset: 2px;
}

:focus:not(:focus-visible) {
  outline: none;  // No outline on mouse click
}
```

### Visually Hidden (Screen Reader Only)

Content hidden from sight but announced by screen readers:

```scss
// src/styles/_mixins.scss:163-173
@mixin sr-only {
  position: absolute;
  width: 1px;
  height: 1px;
  padding: 0;
  margin: -1px;
  overflow: hidden;
  clip: rect(0, 0, 0, 0);
  white-space: nowrap;
  border: 0;
}
```

### Reduced Motion

Disables animations for users who prefer reduced motion (vestibular disorders):

```scss
// src/styles.scss:381-390
@media (prefers-reduced-motion: reduce) {
  *,
  *::before,
  *::after {
    animation-duration: 0.01ms !important;
    animation-iteration-count: 1 !important;
    transition-duration: 0.01ms !important;
    scroll-behavior: auto !important;
  }
}
```

### Focus Ring Mixin

Consistent focus indicators across interactive elements:

```scss
// src/styles/_mixins.scss:200-205
@mixin focus-ring($color: $color-primary, $offset: 2px) {
  &:focus-visible {
    outline: 2px solid $color;
    outline-offset: $offset;
  }
}

// Used on buttons — danger buttons get a red focus ring:
.btn-dismiss {
  @include focus-ring($color-danger);
}
```

---

## 14. Angular Component Styles

### View Encapsulation

Angular scopes component styles using attribute selectors (emulated Shadow DOM):

```html
<!-- Angular adds attributes like _ngcontent-abc to elements -->
<div _nghost-abc>
  <h1 _ngcontent-abc>Dashboard</h1>
</div>
```

```css
/* Your SCSS: */
h1 { color: blue; }

/* Angular compiles to: */
h1[_ngcontent-abc] { color: blue; }
```

This means:
- Component styles **don't leak** to other components
- Global styles **don't reach** into components (unless using `::ng-deep`)
- `@extend` across components **doesn't work** (shared selectors get different attributes)

### Inline vs External Styles

Our app demonstrates both approaches:

**External SCSS file** (preferred for complex styles):
```typescript
// Uses @use for SCSS variables/mixins
@Component({
  styleUrl: './dashboard.component.scss',  // Can use @use, maps, functions
})
```

**Inline styles** (fine for simple components):
```typescript
// Can use CSS custom properties but NOT @use/@import
@Component({
  styles: `
    .profile-card {
      background: var(--color-bg-white);    // ✅ CSS custom properties work
      border-radius: var(--radius-lg);
      // @use 'styles/variables' as *;       // ❌ Can't use @use in inline
    }
  `,
})
```

**When to use which:**
- External: complex styles, needs SCSS features (@use, maps, mixins, functions)
- Inline: simple styles, only needs CSS custom properties
- Login component was moved from inline → external because it needed mixins

### :host Selector

Styles the component's host element:

```typescript
// src/app/app.ts
@Component({
  styles: `
    :host {
      display: block;
      min-height: 100vh;
      font-family: var(--font-family-base);
    }
  `,
})
```

### Global Styles vs Component Styles

| Concern                | Where to put it              | Example                        |
|------------------------|-----------------------------|---------------------------------|
| CSS reset              | `styles.scss` (global)       | `*, *::before { box-sizing }` |
| CSS custom properties  | `styles.scss` (global)       | `:root { --color-primary }`   |
| @keyframes             | `styles.scss` (global)       | `@keyframes spin { ... }`     |
| Utility classes        | `styles.scss` (global)       | `.text-center`, `.sr-only`    |
| Component layout       | `component.scss` (scoped)    | `.dashboard-layout { ... }`   |
| Component elements     | `component.scss` (scoped)    | `.btn-browse { ... }`         |

---

## 15. SCSS Architecture: File Organization

### Our File Structure

```
src/
├── styles/                       # SCSS partials (imported, not compiled directly)
│   ├── _variables.scss          # Design tokens, colors, spacing, breakpoints
│   ├── _mixins.scss             # Reusable patterns (respond-to, spinner, btn-base)
│   └── _functions.scss          # Computed values (rem, shade, space, z)
├── styles.scss                  # Global styles (reset, custom properties, utilities)
└── app/
    ├── pages/
    │   ├── dashboard/dashboard.component.scss
    │   ├── enrollment/enrollment.component.scss
    │   ├── course-detail/course-detail.component.scss
    │   └── login/login.component.scss
    └── components/
        └── course-card/course-card.component.scss
```

### Import Path Configuration

Angular needs to know where to find `@use 'styles/variables'`:

```json
// angular.json
"stylePreprocessorOptions": {
  "includePaths": ["src"]
}
```

This allows `@use 'styles/variables'` to resolve to `src/styles/_variables.scss`.

### The Underscore Convention

Files prefixed with `_` are **partials** — they're meant to be imported, not compiled as standalone CSS files:
- `_variables.scss` → imported with `@use 'styles/variables'`
- `_mixins.scss` → imported with `@use 'styles/mixins'`
- `styles.scss` (no underscore) → compiled as a standalone file

### Dependency Chain

```
_variables.scss  ← No dependencies (pure values)
       ↑
_functions.scss  ← Uses sass:math, sass:color, sass:map + variables
       ↑
_mixins.scss     ← Uses sass:map + variables (could use functions too)
       ↑
styles.scss      ← Uses all three + sass:list
       ↑
*.component.scss ← Each component uses all three
```

### Before and After: Eliminating Duplication

**Before** — spinner defined 3 times:

```scss
// dashboard.component.scss, enrollment.component.scss, course-detail.component.scss
// ALL had this duplicated:
.spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #e0e0e0;
  border-top-color: #1a237e;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}
@keyframes spin { to { transform: rotate(360deg); } }
```

**After** — one mixin, one keyframe:

```scss
// _mixins.scss — defined ONCE
@mixin spinner($size: 40px, $color: $color-primary) { ... }

// styles.scss — keyframes defined ONCE globally
@keyframes spin { to { transform: rotate(360deg); } }

// Each component — one line
.spinner { @include spinner; }
```

---

## 16. Interview Questions & Answers

### Q1: What is the CSS box model? Explain `content-box` vs `border-box`.

**A:** Every element is a box with content, padding, border, and margin layers. `content-box` (default) means `width` only applies to the content area — padding and border are added on top. `border-box` means `width` includes padding and border, so the element never exceeds the specified width. Modern projects always set `*, *::before, *::after { box-sizing: border-box; }` globally.

---

### Q2: How do you center a div horizontally and vertically?

**A:** The most reliable modern approach is Flexbox:
```css
.parent {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
}
```
Grid also works: `display: grid; place-items: center;`

For inline elements: `text-align: center` + `line-height`. For absolute positioning: `top: 50%; left: 50%; transform: translate(-50%, -50%)`.

---

### Q3: Explain the difference between Flexbox and Grid. When do you use each?

**A:** Flexbox is one-dimensional — it controls layout along a single axis (row or column). Grid is two-dimensional — it controls rows and columns simultaneously. Use Flexbox for aligning items in a row (nav items, buttons, tags). Use Grid for page layouts (sidebar + content) and card grids. In our app, the dashboard uses Grid for the two-column layout and Flexbox for the sidebar's vertical stack.

---

### Q4: What does `position: sticky` do?

**A:** It's a hybrid of `relative` and `fixed`. The element scrolls normally with the page until it reaches its `top` (or `bottom`) threshold, then it "sticks" to that position within its containing block. It stops sticking when the containing block scrolls past. In our app, the dashboard sidebar uses `position: sticky; top: 24px` so it follows the user as they scroll through courses.

---

### Q5: What is CSS specificity? How is it calculated?

**A:** Specificity determines which CSS rule wins when multiple rules target the same element. It's calculated as a tuple: (inline, IDs, classes/attributes/pseudo-classes, elements/pseudo-elements). `#id` = 0-1-0-0, `.class` = 0-0-1-0, `div` = 0-0-0-1. When specificity ties, the later rule in source order wins. `!important` overrides everything but should be avoided.

---

### Q6: What is the difference between `display: none` and `visibility: hidden`?

**A:** `display: none` removes the element from the layout entirely — it takes up no space and other elements fill the gap. `visibility: hidden` hides the element visually but it still occupies space in the layout. Screen readers also skip `display: none` but may still read `visibility: hidden`.

---

### Q7: Explain `z-index`. When does it not work?

**A:** `z-index` controls the stacking order of overlapping elements along the z-axis. However, it only works on positioned elements (`relative`, `absolute`, `fixed`, `sticky`). A common mistake is setting `z-index` on a `position: static` element. Also, `z-index` creates a stacking context — a child with `z-index: 9999` can't escape its parent's stacking context. In our app, we centralize z-index values to avoid "z-index wars":
```scss
$z-dropdown: 100; $z-sticky: 200; $z-overlay: 300; $z-modal: 400; $z-toast: 500;
```

---

### Q8: What is `auto-fill` vs `auto-fit` in CSS Grid?

**A:** Both create responsive columns in `repeat()`, but they differ when there's extra space. `auto-fill` creates empty tracks to fill the space (items maintain their size). `auto-fit` collapses empty tracks, stretching items to fill the row. With 2 items on a wide screen: `auto-fill` gives `[item][item][empty][empty]`, `auto-fit` gives `[item-stretched][item-stretched]`.

---

### Q9: What is the difference between `em` and `rem`?

**A:** `em` is relative to the parent element's font-size. `rem` is relative to the root element's (`<html>`) font-size (usually 16px). `rem` is more predictable because it doesn't compound — nested `em` values multiply (1.2em inside 1.2em = 1.44 times the root). Use `rem` for consistent sizing, `em` for component-relative scaling.

---

### Q10: Explain mobile-first responsive design.

**A:** Write base CSS for mobile screens, then add complexity for larger screens using `min-width` media queries. Mobile styles load first (fast on slow connections), and larger screens progressively enhance. Desktop-first (using `max-width`) requires overriding styles for mobile, resulting in more CSS. In our app, the dashboard starts as `display: flex; flex-direction: column` and upgrades to `display: grid` at the tablet breakpoint.

---

### Q11: What are CSS custom properties? How do they differ from SCSS variables?

**A:** CSS custom properties (e.g., `--color-primary: #1a237e`) are resolved at runtime in the browser. They cascade through the DOM, can be scoped to selectors, changed via JavaScript, and respond to media queries. SCSS variables (e.g., `$color-primary: #1a237e`) are resolved at compile time and don't exist in the browser. Use SCSS variables for build-time logic (maps, loops, math) and CSS custom properties for runtime theming.

---

### Q12: What is `@mixin` vs `@extend` in SCSS? Which should you use?

**A:** `@mixin` duplicates CSS at each `@include` call site. `@extend` creates shared selectors. `@mixin` is preferred because: (1) it works with parameters, (2) it works with Angular's view encapsulation (shared selectors get different scope attributes), (3) `@extend` can create unexpected selector bloat with nested selectors. The consensus in the SCSS community is to prefer mixins over extend.

---

### Q13: What is `clamp()` and how is it useful for responsive design?

**A:** `clamp(min, preferred, max)` returns a value that's at least `min`, at most `max`, and `preferred` if it falls between. It creates fluid values without media queries. Example: `font-size: clamp(1rem, 4vw, 2rem)` — text is 1rem minimum, scales with viewport width, caps at 2rem. We use it for fluid padding and typography in our dashboard.

---

### Q14: What is `min-width: 0` and why is it needed in Flexbox/Grid?

**A:** Flex and grid items have an implicit `min-width: auto`, which prevents them from shrinking below their content's intrinsic size. This can cause overflow. Setting `min-width: 0` allows the item to shrink. Our dashboard's `.content` element uses this to prevent grid blowout when course cards have long text.

---

### Q15: How do you truncate text with CSS?

**A:** Single-line truncation:
```css
.truncate { overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
```
Multi-line truncation (line clamping):
```css
.clamp { display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden; }
```
Our app uses both — single-line for instructor names, multi-line for course names.

---

### Q16: Explain `::before` and `::after` pseudo-elements.

**A:** They create virtual child elements that don't exist in the DOM. They require `content: ''` to appear. `::before` inserts before the element's content, `::after` inserts after. They're useful for decorative elements (icons, dividers, tooltips) that shouldn't be in the HTML markup. They inherit styles from their parent and can be positioned absolutely.

---

### Q17: What is `:focus-visible` and why is it important?

**A:** `:focus-visible` triggers only on keyboard focus, not mouse clicks. Without it, every button click shows a focus outline (ugly). With it, outlines only appear during keyboard navigation (accessible). Our global styles use `:focus-visible { outline: 2px solid var(--color-primary); }` and `:focus:not(:focus-visible) { outline: none; }` to provide accessibility without visual noise.

---

### Q18: How does Angular's view encapsulation affect CSS?

**A:** Angular's default "emulated" encapsulation adds unique attributes (like `[_ngcontent-abc]`) to each component's elements and selectors. This scopes styles so they don't leak or conflict. It means: (1) global styles don't penetrate components unless using `::ng-deep`, (2) `@extend` across components won't work, (3) `@keyframes` need to be global (in styles.scss). Components can opt out with `encapsulation: ViewEncapsulation.None`.

---

### Q19: What is the `inset` property?

**A:** `inset` is shorthand for `top`, `right`, `bottom`, `left`. `inset: 0` sets all four to 0 — commonly used for overlays and fullscreen elements with `position: fixed` or `position: absolute`. It follows the same shorthand pattern as `margin`/`padding`: `inset: top right bottom left`.

---

### Q20: Explain `prefers-reduced-motion`. Why is it important?

**A:** It's a CSS media query that detects if the user has enabled "Reduce motion" in their OS settings. People with vestibular disorders can experience nausea from animations. Our app respects this by setting all animation/transition durations to near-zero:
```css
@media (prefers-reduced-motion: reduce) { * { animation-duration: 0.01ms !important; } }
```
This is required for WCAG 2.1 AA compliance.

---

### Q21: What is the `:has()` selector?

**A:** `:has()` selects an element based on its descendants — effectively a "parent selector" that CSS never had before. Example: `div:has(> img)` selects any `div` that directly contains an `img`. In our course card, `.course-card.full:has(.enrollment-fill)` styles the fill bar differently when the course is full. Supported in Chrome 105+, Safari 15.4+, Firefox 121+.

---

### Q22: What is the difference between `@use` and `@import` in SCSS?

**A:** `@use` is the modern module system: files are loaded once (no duplication), variables are namespaced (e.g., `vars.$color`), and dependencies are explicit. `@import` is deprecated: files can be loaded multiple times (causing duplication), everything is dumped into the global scope, and you can get naming conflicts. Always use `@use` in new projects.

---

### Q23: How do you make images responsive?

**A:** Set `img { display: block; max-width: 100%; height: auto; }`. `max-width: 100%` ensures the image never overflows its container. `height: auto` preserves the aspect ratio. For modern layouts, `aspect-ratio` can also help: `img { aspect-ratio: 16/9; object-fit: cover; }`. Our global reset applies responsive image styles by default.

---

### Q24: What is `BEM` naming convention?

**A:** BEM stands for Block-Element-Modifier. It structures CSS class names for maintainability:
- **Block:** `.card` (standalone component)
- **Element:** `.card__header` (part of the block, uses `__`)
- **Modifier:** `.card--featured` (variant of the block, uses `--`)

Our app uses a simplified BEM-like approach — Angular's view encapsulation makes strict BEM less critical since styles are already scoped.

---

### Q25: What is `overflow-wrap: break-word`?

**A:** It allows long unbroken words (like URLs) to break and wrap to the next line instead of overflowing their container. Unlike `word-break: break-all`, it only breaks words when necessary (tries to break at natural break points first). Our global styles apply it to headings and paragraphs:
```scss
h1, h2, h3, h4, h5, h6 { overflow-wrap: break-word; }
p { overflow-wrap: break-word; }
```

---

### Q26: What is `text-size-adjust` and why do we set it?

**A:** Mobile browsers sometimes inflate font sizes on landscape orientation or narrow viewports to improve readability. Setting `text-size-adjust: none` prevents this, giving you full control over typography. We set all three vendor prefixes:
```scss
-moz-text-size-adjust: none;
-webkit-text-size-adjust: none;
text-size-adjust: none;
```

---

### Q27: What is a stacking context? Name 3 ways to create one.

**A:** A stacking context is a three-dimensional conceptualization of HTML elements along the z-axis. Elements within a stacking context are painted as a unit, and a child's `z-index` cannot escape its parent's stacking context. Ways to create one:
1. `position: relative/absolute/fixed` with a `z-index` value
2. `opacity` less than 1
3. `transform`, `filter`, or `will-change` properties
4. `position: fixed` or `position: sticky` (always)
5. `isolation: isolate`

---

## Summary

| Concept | Files | What We Demonstrated |
|---------|-------|----------------------|
| SCSS Variables & Maps | `_variables.scss` | `$color-primary`, `$breakpoints` map, `$status-colors` nested map |
| SCSS Mixins | `_mixins.scss` | `respond-to()` with `@content`, `spinner()` with params, `flex-center` |
| SCSS Functions | `_functions.scss` | `rem()`, `shade()`, `tint()`, `space()`, `z()` |
| CSS Custom Properties | `styles.scss` `:root` | Design tokens bridged from SCSS vars to runtime |
| Modern Reset | `styles.scss` | `box-sizing`, `margin: 0`, responsive images, form font inheritance |
| Utility Classes | `styles.scss` | `@for` loop spacing, `@each` loop status badges, flex/text/display utils |
| CSS Grid Layout | `dashboard.component.scss` | `grid-template-columns`, `auto-fill`, `minmax()`, grid blowout fix |
| Flexbox Layout | `login.component.scss` | `flex-center`, `flex-column`, `flex-wrap`, `flex: 1 1 auto` |
| Mobile-First Responsive | All component SCSS | `@include respond-to('tablet')`, `clamp()`, `min()`, fluid typography |
| Positioning | `course-detail.component.scss` | `relative`+`absolute` (progress bar), `sticky` (sidebar) |
| Animations | `styles.scss` + components | `@keyframes spin/fadeIn/slideUp`, `transition` multi-property |
| Modern CSS | `course-card.component.scss` | `:has()`, `:is()`, `inset`, `text-wrap: pretty`, scoped custom properties |
| Accessibility | `styles.scss` + mixins | `:focus-visible`, `sr-only`, `prefers-reduced-motion`, `focus-ring` mixin |
| Angular Styles | All components | External vs inline, view encapsulation, `stylePreprocessorOptions` |

**Files modified/created in this chapter:**

| File | Action |
|------|--------|
| `src/styles/_variables.scss` | Created — SCSS variables, maps, breakpoints |
| `src/styles/_mixins.scss` | Created — 15+ reusable mixins |
| `src/styles/_functions.scss` | Created — 7 utility functions |
| `src/styles.scss` | Rewritten — CSS custom properties, reset, utilities, keyframes |
| `dashboard.component.scss` | Rewritten — responsive grid, shared mixins |
| `enrollment.component.scss` | Rewritten — responsive, shared mixins |
| `course-detail.component.scss` | Rewritten — responsive, positioning patterns |
| `course-card.component.scss` | Rewritten — advanced CSS (:has, :is, scoped vars) |
| `login.component.scss` | Created (moved from inline) — gradient, form styling |
| `login.component.ts` | Modified — `styles` → `styleUrl` |
| `student-profile.component.ts` | Modified — CSS custom properties in inline styles |
| `enrollment-status.component.ts` | Modified — CSS custom properties with fallbacks |
| `not-found.component.ts` | Modified — CSS custom properties, fluid typography |
| `app.ts` | Modified — CSS custom property for font-family |
| `angular.json` | Modified — `stylePreprocessorOptions.includePaths` |
