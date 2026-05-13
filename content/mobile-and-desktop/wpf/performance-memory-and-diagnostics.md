# Performance Memory And Diagnostics

WPF performance issues usually come from layout churn, large visual trees, expensive bindings, non-virtualized item controls, image misuse, and object lifetime mistakes. The goal is rarely micro-optimization. The goal is keeping rendering, input, and memory behavior predictable.

Related topics: [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md), [Data Binding And Collection Views](data-binding-and-collection-views.md), [Resources](resources.md)

## UI Virtualization

Virtualization prevents item controls from creating visuals for every data item at once.

```xml
<ListBox ItemsSource="{Binding Items}"
         VirtualizingPanel.IsVirtualizing="True"
         VirtualizingPanel.VirtualizationMode="Recycling"
         ScrollViewer.CanContentScroll="True" />
```

Virtualization can be disabled accidentally by:

- wrapping the list in an outer `ScrollViewer`
- using a non-virtualizing panel
- forcing item containers to measure with infinite space
- turning off logical scrolling

For large datasets, virtualization is usually the first thing to verify.

## Binding And Layout Performance

Binding itself is not usually the bottleneck, but many bindings plus frequent property changes plus deep visual trees can create expensive layout and render work.

Helpful habits:

- avoid unnecessary nested panels
- batch view model updates when practical
- avoid constantly changing properties that affect measure or arrange
- keep converters simple and allocation-light
- use `OneTime` bindings for static values

Properties like `Width`, `Height`, `Margin`, and `Visibility` can trigger layout work. Frequent changes to them across many elements can become noticeable.

## Freezables And Resource Reuse

Types such as `Brush`, `Transform`, and `Geometry` derive from `Freezable`. Frozen instances can be shared more efficiently.

```csharp
var brush = new SolidColorBrush(Colors.SteelBlue);
brush.Freeze();
```

Reuse shared brushes and geometries from resources instead of recreating them per element or per frame.

## Images And Media

Large images can consume surprising memory. Scale source assets appropriately and decode to a practical display size.

```csharp
var bitmap = new BitmapImage();
bitmap.BeginInit();
bitmap.UriSource = new Uri("photo.jpg", UriKind.Relative);
bitmap.DecodePixelWidth = 400;
bitmap.EndInit();
```

Prefer setting decode dimensions when you do not need the original full-resolution image in memory.

## Common Memory Leak Patterns

WPF apps often leak through references rather than through unmanaged memory mistakes.

Frequent causes:

- long-lived event subscriptions
- static event handlers
- timers keeping views or view models alive
- cached windows or dialogs that were expected to die
- collections or services holding references to view models indefinitely

If a short-lived object subscribes to a long-lived publisher, unsubscribe or use a weak event pattern.

```csharp
publisher.Changed += OnChanged;
...
publisher.Changed -= OnChanged;
```

## Diagnostics Tools

Use the right tool for the symptom.

- Visual Studio Diagnostic Tools: memory, CPU, and timeline profiling
- Live Visual Tree and Live Property Explorer: inspect runtime UI structure
- PerfView and `dotnet-trace`: deeper .NET runtime analysis
- ETW-based tools: rendering and UI responsiveness investigation

For binding problems, enable binding trace output.

```xml
<TextBox Text="{Binding Name,
                PresentationTraceSources.TraceLevel=High}" />
```

The Output window often reveals broken paths, conversion failures, or data context mismatches immediately.

## Performance Review Checklist

When a WPF screen feels slow, check in this order:

1. Is virtualization active?
2. Is the visual tree larger than necessary?
3. Are bindings or converters firing excessively?
4. Are layout-affecting properties changing too often?
5. Are images or media assets larger than needed?
6. Are objects being retained after the screen closes?

## Resources

- Optimizing WPF application performance: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-wpf-application-performance
- Optimizing layout and design: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-performance-layout-and-design
- Optimizing controls: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-performance-controls
- WPF trees and debugging: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/trees-in-wpf
- .NET diagnostics: https://learn.microsoft.com/dotnet/core/diagnostics/
