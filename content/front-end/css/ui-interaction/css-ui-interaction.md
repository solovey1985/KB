# CSS UI States, Forms, and Interaction

Interactive CSS is about communicating state clearly while keeping interfaces usable and accessible.

## Pseudo-classes for state

```css
button:hover {
  transform: translateY(-1px);
}

button:focus-visible {
  outline: 3px solid #2563eb;
  outline-offset: 2px;
}
```

Important state selectors include:

- `:hover`
- `:focus`
- `:focus-visible`
- `:active`
- `:disabled`
- `:checked`

## Pseudo-elements

Pseudo-elements help with decoration and generated content.

```css
.external-link::after {
  content: '↗';
  margin-left: 0.25rem;
}
```

## Form styling

Forms need both visual clarity and accessibility.

```css
input:focus-visible,
select:focus-visible,
textarea:focus-visible {
  outline: 2px solid #2563eb;
}
```

## Transitions and animations

Transitions are best for state changes.

Animations are best for more explicit motion sequences.

```css
.button {
  transition: background-color 150ms ease, transform 150ms ease;
}
```

## Interview reminders

- accessibility matters when discussing focus states
- explain `:focus-visible` rather than relying only on `:focus`
- distinguish transitions from keyframe animations
