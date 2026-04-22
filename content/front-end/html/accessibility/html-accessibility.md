# HTML Accessibility and Inclusive Markup

Accessible HTML starts with choosing the right native elements before adding ARIA or JavaScript fixes.

## Native semantics first

Native HTML elements already carry useful semantics.

```html
<button type="button">Open menu</button>
<a href="/pricing">Pricing</a>
```

Using a real button is usually better than styling a `<div>` to behave like one.

## Labels and descriptions

Accessible forms need clear names and helpful descriptions.

```html
<label for="password">Password</label>
<input id="password" type="password" aria-describedby="password-help" />
<p id="password-help">Must be at least 8 characters long.</p>
```

## Images and alternative text

```html
<img src="team-photo.jpg" alt="The design team standing in front of the studio" />
```

If an image is purely decorative, use an empty alt attribute.

```html
<img src="divider.svg" alt="" />
```

## ARIA carefully

ARIA is useful when native HTML cannot express the needed meaning fully, but native elements should come first whenever possible.

Rule of thumb:

- prefer native semantics
- add ARIA to fill real gaps
- avoid using ARIA to patch avoidable bad markup

## Interview reminders

- semantic HTML is the first accessibility tool
- visible focus and keyboard behavior matter
- `alt=""` is correct for decorative images
- ARIA should not be used to recreate native semantics unnecessarily
