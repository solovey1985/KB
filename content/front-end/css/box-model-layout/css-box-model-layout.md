# CSS Box Model and Layout Basics

This topic explains how elements take up space and how early layout decisions affect every page.

## Box model

Every element box is built from:

- content
- padding
- border
- margin

```css
.card {
  width: 320px;
  padding: 16px;
  border: 1px solid #d1d5db;
  margin: 24px;
}
```

## box-sizing

`box-sizing` changes how width and height are calculated.

```css
* {
  box-sizing: border-box;
}
```

`border-box` is commonly preferred because padding and border stay inside the declared width.

## Display modes

The most common display behaviors:

- `block`
- `inline`
- `inline-block`

```css
.tag {
  display: inline-block;
  padding: 0.25rem 0.5rem;
}
```

## Margin collapsing

Vertical margins between block elements can collapse into one shared margin.

This surprises people when stacked sections seem to have less space than expected.

## Positioning

The main positioning values are:

- `static`
- `relative`
- `absolute`
- `fixed`
- `sticky`

```css
.tooltip {
  position: absolute;
  top: 100%;
  left: 0;
}
```

## Centering

Different centering tools solve different problems.

For a block with known width:

```css
.container {
  width: 50%;
  margin: 0 auto;
}
```

For modern alignment, Flexbox and Grid are often better.

## Interview reminders

- know the four box model layers
- explain `border-box` clearly
- distinguish `absolute` from `fixed` and `sticky`
- do not claim there is only one centering technique
