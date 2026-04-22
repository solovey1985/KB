# CSS Modern Layout and Responsive Design

Modern CSS layout is built mainly around Flexbox and Grid, with media queries shaping how layouts adapt across viewports.

## Flexbox

Flexbox is designed for one-dimensional layout.

Use it when you are aligning items in a row or column.

```css
.toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 1rem;
}
```

Key ideas:

- main axis
- cross axis
- grow, shrink, basis

## Grid

Grid is designed for two-dimensional layout.

Use it when rows and columns both matter.

```css
.dashboard {
  display: grid;
  grid-template-columns: 240px 1fr;
  gap: 1.5rem;
}
```

## Media queries

Media queries change styles based on viewport or device conditions.

```css
@media (max-width: 768px) {
  .dashboard {
    grid-template-columns: 1fr;
  }
}
```

## Mobile-first design

Mobile-first means the default styles fit smaller screens, and larger layouts are progressively added.

This usually leads to cleaner responsive design than trying to shrink a desktop-first layout later.

## Interview reminders

- Flexbox is one-dimensional
- Grid is two-dimensional
- media queries are not the same as responsive design strategy
- mobile-first is about the default baseline, not only small devices
