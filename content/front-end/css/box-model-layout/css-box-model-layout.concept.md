---
title: CSS Box Model and Layout Basics Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes how CSS boxes occupy space and how common display and positioning modes behave.

Study pages: [Section Index](index.md) | [Material Notes](css-box-model-layout.md) | [Interview Practice](css-box-model-layout.interview.md)

## Layout Map

```concept-card
id: box-model
term: Box Model
children:
- box-sizing
- margin-collapsing
- display-types
- positioning
- centering
summary:
The box model describes how each element uses space through content, padding, border, and margin.
details:
It is the foundation of CSS layout because every visible element occupies space through this layered structure.
example:
An element with `padding: 16px` and `border: 1px solid` is larger than its raw content size.
mnemonic:
Content inside, spacing outside.
recall:
- What are the four layers of the box model?
- Why does box-model understanding matter for layout debugging?
```

```concept-card
id: box-sizing
term: box-sizing
parents:
- box-model
summary:
`box-sizing` controls whether declared width and height include padding and border.
details:
`border-box` is often easier for component layout because the total rendered size stays closer to the declared size.
example:
`* { box-sizing: border-box; }`
mnemonic:
Border-box keeps the math honest.
recall:
- What is the difference between `content-box` and `border-box`?
- Why is `border-box` a common default?
```

```concept-card
id: margin-collapsing
term: Margin Collapsing
parents:
- box-model
summary:
Margin collapsing is the combining of adjacent vertical margins into one effective margin.
details:
It affects block layout and often surprises developers when stacked elements do not add margins the way they expect.
example:
Two stacked paragraphs with vertical margins may visually show only the larger margin instead of both added together.
mnemonic:
Two vertical margins may act like one.
recall:
- When does margin collapsing happen?
- Why does it confuse spacing expectations?
```

```concept-card
id: display-types
term: Display Types
children:
- block-layout
- inline-layout
- inline-block-layout
summary:
Display types define how elements participate in layout flow.
details:
The classic distinctions between block, inline, and inline-block explain many everyday layout behaviors.
example:
`span` is inline by default, while `div` is block by default.
mnemonic:
Display decides how the box behaves.
recall:
- Why do `div` and `span` behave differently by default?
- When is `inline-block` useful?
```

```concept-card
id: block-layout
term: Block
parents:
- display-types
summary:
Block elements typically start on a new line and expand across available width.
details:
They are the normal building blocks for larger page sections and stacked layout structures.
example:
`div`, `section`, and `article` commonly behave as block elements.
mnemonic:
Block means row-level structure.
recall:
- What default behavior makes an element block-level?
- Why are blocks common for structural layout?
```

```concept-card
id: inline-layout
term: Inline
parents:
- display-types
summary:
Inline elements flow with surrounding text and do not start new lines by default.
details:
They are useful for text-level styling and small inline content.
example:
`a` and `span` are commonly inline.
mnemonic:
Inline stays in the text flow.
recall:
- What makes inline elements different from blocks?
- Why do width and height behave differently on inline elements?
```

```concept-card
id: inline-block-layout
term: Inline-Block
parents:
- display-types
summary:
Inline-block combines inline placement with box-like sizing behavior.
details:
It allows elements to sit next to each other while still respecting width, height, and padding like blocks.
example:
Badges and pills often use `display: inline-block`.
mnemonic:
Inline placement, block sizing.
recall:
- What makes inline-block a hybrid?
- When is it more useful than plain inline?
```

```concept-card
id: positioning
term: Positioning
children:
- absolute-positioning
- fixed-positioning
- sticky-positioning
summary:
Positioning controls how an element is placed relative to normal flow, an ancestor, or the viewport.
details:
Understanding positioning is essential for overlays, sticky UI, tooltips, and controlled offsets.
example:
Tooltips often use `position: absolute`, while persistent top bars may use `fixed` or `sticky`.
mnemonic:
Position decides which coordinate system matters.
recall:
- What does normal flow mean in positioning discussions?
- Why do absolute, fixed, and sticky behave differently?
```

```concept-card
id: absolute-positioning
term: Absolute Positioning
parents:
- positioning
summary:
Absolutely positioned elements are removed from normal flow and positioned relative to the nearest positioned ancestor.
details:
They are useful for overlays, menus, and tooltips, but they require careful containing context.
example:
`.tooltip { position: absolute; top: 100%; left: 0; }`
mnemonic:
Absolute escapes flow, anchors to context.
recall:
- What ancestor does `absolute` positioning use?
- Why can missing positioning context cause bugs?
```

```concept-card
id: fixed-positioning
term: Fixed Positioning
parents:
- positioning
summary:
Fixed positioning anchors an element to the viewport rather than to normal document flow.
details:
It is useful for persistent navbars, floating actions, and overlays that should stay visible during scroll.
example:
`header { position: fixed; top: 0; left: 0; right: 0; }`
mnemonic:
Fixed stays with the viewport.
recall:
- Why does fixed content stay visible during scroll?
- What layout issue often appears with fixed headers?
```

```concept-card
id: sticky-positioning
term: Sticky Positioning
parents:
- positioning
summary:
Sticky positioning acts like normal flow until a threshold is reached, then sticks within its scroll container.
details:
It is useful for section headers and navigation that should remain visible only after scrolling to a point.
example:
`.section-title { position: sticky; top: 0; }`
mnemonic:
Flows first, sticks later.
recall:
- How is sticky different from fixed?
- Why is sticky often good for section headers?
```

```concept-card
id: centering
term: Centering
parents:
- box-model
summary:
Centering is the set of techniques used to align content horizontally, vertically, or both.
details:
The correct technique depends on the layout context, such as block flow, Flexbox, or Grid.
example:
`margin: 0 auto` centers a fixed-width block horizontally, while Flexbox can center both axes.
mnemonic:
Centering depends on context, not one magic rule.
recall:
- Why is there no single centering technique for all cases?
- When is auto-margin enough and when is Flexbox better?
```
