# HTML Forms and Validation

HTML forms are not just input containers. They define how browsers collect, validate, and submit user data.

## Basic form structure

```html
<form action="/signup" method="post">
  <label for="email">Email</label>
  <input id="email" name="email" type="email" required />

  <label for="age">Age</label>
  <input id="age" name="age" type="number" min="18" />

  <button type="submit">Create account</button>
</form>
```

## Labels and names

Important distinctions:

- `id` connects a control to its label
- `name` controls what key is submitted with the form
- `value` is the current field value
- `placeholder` is not a replacement for a label

## Input types

HTML5 introduced richer input types such as:

- `email`
- `number`
- `date`
- `url`
- `search`
- `tel`

These help browsers provide better keyboards, validation, and user feedback.

## Native validation

HTML can validate many form rules without JavaScript.

```html
<input type="password" minlength="8" required />
<input type="text" pattern="[A-Za-z]{3,}" />
```

Useful attributes include:

- `required`
- `pattern`
- `min`
- `max`
- `step`
- `minlength`
- `maxlength`

## GET versus POST

Use `GET` when the form is retrieving or filtering data and URL visibility is acceptable.

Use `POST` when the form submits data that changes server state or should not appear in the URL.

## Interview reminders

- labels are mandatory for usable forms
- placeholder is not a label
- native validation is useful, but not the full validation story
