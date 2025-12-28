# Angular Material & Flex Layout Snippet Cheatsheet

Quick reference for triggers provided by the "Angular Material 2, Flex layout" VS Code extension.

## Trigger Prefixes & Suffixes

| Symbol | Meaning |
| --- | --- |
| `mat-` | Angular Material component snippet |
| `@mat` | Angular Material attribute/directive snippet |
| `@fx` | Angular Flex Layout directive snippet |
| `*` | Snippet renders a repeated `*ngFor` variation |
| `$` | Reactive forms / observable-based variation |
| `_` | Partial snippet (supplementary markup) |
| `+` | Responsive configuration variant |
| `:**` | Maximum options variant |
| `:?` | Includes inline usage help |

Use `Ctrl+Space` after typing a trigger to view completions.

## Core Angular Material Snippets

### Layout & Navigation

| Trigger | Description |
| --- | --- |
| `mat-toolbar` / `mat-toolbar:color` | App toolbar (optionally themed) |
| `mat-sidenav` / `mat-sidenav:**` | Responsive sidenav shell |
| `mat-menu` / `mat-menu:**` | Menu with menu items |
| `mat-tab` / `mat-tab-*` | Tab group (single or repeated) |
| `mat-accordion` / `mat-accordion-_expansion-panel` | Expansion panels with optional actions |

### Buttons & Indicators

| Trigger | Description |
| --- | --- |
| `mat-button` / `mat-button:color` | Standard buttons + themed variants |
| `mat-button-raised` / `mat-button-icon` | Raised and icon buttons |
| `mat-button-fab` / `mat-button-mini-fab` | Floating action button options |
| `mat-progress-bar-determinate` / `-indeterminate` | Linear progress indicators |
| `mat-progress-spinner-determinate` / `-indeterminate` | Circular progress indicators |

### Forms & Inputs

| Trigger | Description |
| --- | --- |
| `mat-input` / `mat-input$` | Text input (template vs reactive forms) |
| `mat-select` / `mat-select$` | Select dropdown (template vs reactive, optional optgroups) |
| `mat-autocomplete` / `mat-autocomplete$` | Autocomplete field variants |
| `mat-checkbox` / `mat-checkbox$` | Checkbox (template vs reactive) |
| `mat-radio-group` / `mat-radio$-group` | Radio button groups |
| `mat-slide-toggle` / `mat-slide-toggle$` | Slide toggle control |
| `mat-slider` / `mat-slider$` | Slider input |
| `mat-datepicker` / `mat-datepicker$` | Datepicker (template vs reactive) |

### Data Display

| Trigger | Description |
| --- | --- |
| `mat-card` / `mat-card:avatar` | Card layouts with media/avatar slots |
| `mat-list` / `mat-list-*` | Lists, nav lists, and supporting partials |
| `mat-chip` / `mat-chip-*` | Chip list variations |
| `mat-table` (via `mat-grid-*`) | Grid-based table layouts (fit/fixed/ratio) |
| `mat-dialog` / `mat-dialog:**` | Dialog scaffolding with actions |
| `mat-tooltip` (`@matTooltip`) | Tooltip attribute with advanced options |

> **Tip:** Attribute snippets like `@matTooltip` or `@matCardTitle` expand in-place on existing elements.

## Angular Flex Layout Snippets

All flex snippets start with `@fx` and target attributes.

| Trigger | Description |
| --- | --- |
| `@fxLayout` / `@fxLayout+` | Container layout direction (responsive variant with `+`) |
| `@fxLayoutAlign` / `@fxLayoutAlign+` | Cross-axis and main-axis alignment |
| `@fxLayoutGap` / `@fxLayoutGap+` | Spacing between child elements |
| `@fxFlex` / `@fxFlex+` | Item flex sizing with responsive overrides |
| `@fxFlexFill` | Forces child to fill remaining space |
| `@fxFlexOrder` / `@fxFlexOrder+` | Ordering of flex children |
| `@fxFlexOffset` / `@fxFlexOffset+` | Sets offset/margin before elements |
| `@fxShow` / `@fxHide` (and `+` variants) | Show/hide elements responsively |
| `@fxClass` | Apply CSS classes via media queries |
| `@fxStyle` | Inline styles bound to layout breakpoints |

Keep this page handy when working inside Angular templates to quickly recall the trigger text provided by the VS Code extension.
