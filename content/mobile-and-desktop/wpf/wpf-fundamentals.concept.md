---
title: WPF Fundamentals Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core WPF runtime and XAML concepts that underpin the rest of the WPF topic.

Study pages: [Section Index](index.md) | [WPF Overview And Runtime](wpf-overview-and-runtime.md) | [XAML Fundamentals](xaml-fundamentals.md) | [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md)

## Fundamentals Map

```concept-card
id: wpf-runtime
term: WPF Runtime
children:
- logical-tree
- visual-tree
- device-independent-pixels
- xaml-object-graph
- dependency-property
- routed-event
summary:
The WPF runtime combines a retained visual system, XAML object construction, layout, rendering, and a property/event infrastructure for desktop UI.
details:
Understanding the runtime explains why binding, templating, event routing, resources, and layout all feel connected in WPF instead of being isolated features.
example:
Changing a control template changes visuals, while dependency properties and routed events still keep the control participating in the same WPF runtime model.
mnemonic:
Runtime first, features second.
recall:
- Which WPF concepts sit directly under the runtime model?
- Why do so many WPF features depend on shared infrastructure?
```

```concept-card
id: logical-tree
term: Logical Tree
parents:
- wpf-runtime
related:
- visual-tree
summary:
The logical tree represents the high-level content and ownership structure of WPF elements.
details:
It influences resource lookup, data-context inheritance, and how controls and content are composed from an application point of view.
example:
A `Window` containing a `Grid` and a `Button` forms a simple logical tree.
mnemonic:
Logical means ownership and composition.
recall:
- What WPF behaviors depend on the logical tree?
- How is the logical tree different from the visual tree?
```

```concept-card
id: visual-tree
term: Visual Tree
parents:
- wpf-runtime
related:
- logical-tree
- control-template
summary:
The visual tree contains the rendered visual elements that WPF uses for drawing, hit testing, and templated UI structure.
details:
It is often deeper than the logical tree because control templates add internal visuals such as borders, presenters, and chrome.
example:
A `Button` usually renders as more than a single visual element because its template expands into nested visuals.
mnemonic:
Visual means what actually gets rendered.
recall:
- Why is the visual tree usually deeper than the logical tree?
- Which debugging scenarios require visual-tree inspection?
```

```concept-card
id: device-independent-pixels
term: Device Independent Pixels
parents:
- wpf-runtime
summary:
WPF measures layout in device-independent pixels so UI scales more consistently across display densities.
details:
This model helps with DPI awareness, but image quality, layout rounding, and control rendering still need attention for crisp high-DPI UI.
example:
A `Width="200"` button keeps roughly the same physical size across displays with different pixel densities.
mnemonic:
Measure for intent, render for device.
recall:
- Why does WPF use device-independent pixels?
- What display concerns still remain even with DPI abstraction?
```

```concept-card
id: xaml-object-graph
term: XAML Object Graph
parents:
- wpf-runtime
children:
- markup-extension
- attached-property
summary:
XAML declaratively creates the object graph that becomes a WPF view.
details:
Namespaces, attributes, property elements, and markup extensions let WPF build runtime objects, assign properties, and connect resources and bindings.
example:
`<Button Content="Save" Margin="8" />` becomes a `Button` object with initialized properties in the runtime tree.
mnemonic:
XAML describes objects, WPF builds them.
recall:
- What does XAML actually produce at runtime?
- Which two XAML mechanisms commonly extend plain property assignment?
```

```concept-card
id: markup-extension
term: Markup Extension
parents:
- xaml-object-graph
summary:
A markup extension injects non-literal values into XAML, such as bindings, resources, and type references.
details:
Common examples include `Binding`, `StaticResource`, `DynamicResource`, and `x:Type`.
example:
`Text="{Binding CustomerName}"` uses the `Binding` markup extension instead of a literal string.
mnemonic:
Curly braces mean runtime help.
recall:
- Why are markup extensions important in XAML?
- Which common WPF features are expressed as markup extensions?
```

```concept-card
id: attached-property
term: Attached Property
parents:
- xaml-object-graph
- dependency-property
summary:
An attached property lets one type define a property value that is set on another type.
details:
Layout containers such as `Grid` use attached properties to let children declare layout behavior without the child owning the property.
example:
`Grid.Row="1"` is an attached property assignment.
mnemonic:
Parent-owned rule, child-set value.
recall:
- Why do layout panels rely on attached properties?
- What is a common attached property example in WPF?
```

```concept-card
id: dependency-property
term: Dependency Property
parents:
- wpf-runtime
children:
- attached-property
related:
- routed-event
- control-template
summary:
A dependency property is a WPF property backed by the property system rather than a normal field.
details:
Dependency properties enable binding, styling, animation, metadata, default values, inheritance, and template interaction.
example:
`TextBox.Text` is a dependency property, which is why it can participate in two-way binding and styles.
mnemonic:
WPF properties do more than store values.
recall:
- Why do WPF controls use dependency properties?
- Which WPF features depend on dependency properties?
```

```concept-card
id: routed-event
term: Routed Event
parents:
- wpf-runtime
related:
- dependency-property
summary:
A routed event travels through the WPF element tree rather than notifying only the source element.
details:
WPF supports bubbling, tunneling, and direct event strategies, which helps controls and containers participate in input and interaction flow.
example:
A `Button.Click` can be handled by a parent `StackPanel` because the event bubbles upward.
mnemonic:
Events can travel, not just fire.
recall:
- What are the three routed event strategies?
- Why are routed events useful in composite WPF UI?
```

```concept-card
id: control-template
term: Control Template
parents:
- dependency-property
related:
- visual-tree
summary:
A control template defines the visual structure of a control while preserving its behavior and API.
details:
Templates explain why the same control can look very different while still supporting the same dependency properties, commands, and state model.
example:
A default `Button` template can be replaced with a rounded-card template without changing how `Command` or `Content` work.
mnemonic:
Same behavior, different skin.
recall:
- What does a control template change and what does it keep?
- Why do control templates affect the visual tree?
```
