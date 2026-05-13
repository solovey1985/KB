# WPF And Modern Windows Desktop Apps

This section covers WPF fundamentals and the advanced topics needed to maintain or build modern Windows desktop applications on .NET.

Source materials:

- `wpf.json` — source question metadata.
- `wpf.answers.md` — free answer notes for questions 1-15.

## Learning Path

1. [WPF Overview And Runtime](wpf-overview-and-runtime.md)
2. [XAML Fundamentals](xaml-fundamentals.md)
3. [Threading Dispatcher And Async](threading-dispatcher-and-async.md)
4. [Data Binding And Collection Views](data-binding-and-collection-views.md)
5. [MVVM Commands And Navigation](mvvm-commands-and-navigation.md)
6. [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md)
7. [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md)
8. [Resources](resources.md)
9. [Templates Styles And Resources](templates-styles-and-resources.md)
10. [User Controls Custom Controls](user-controls-custom-controls.md)
11. [Performance Memory And Diagnostics](performance-memory-and-diagnostics.md)
12. [Advanced Graphics Media Interop](advanced-graphics-media-interop.md)
13. [Modern Windows Desktop Apps](modern-windows-desktop-apps.md)

Related subtopic:

- [ReactiveUI For WPF](reactive-ui/index.md)

## Topic Relationships

WPF runtime concepts sit underneath everything else:

- The logical and visual trees explain resource lookup, event routing, templating, and debugging.
- Device-independent pixels and DPI awareness affect layout, images, and rendering quality.
- The UI thread and dispatcher control safe UI updates and async workflows.

XAML connects visual composition to runtime behavior:

- XAML namespaces and markup extensions create object graphs declaratively.
- Resources and `x:Key` support reuse.
- Attached properties connect children to layout containers.
- Bindings connect view properties to view model state.

Data binding is the bridge toward MVVM:

- `INotifyPropertyChanged` updates individual property bindings.
- `ObservableCollection<T>` updates item lists.
- `CollectionView` adds sorting, filtering, grouping, and current-item behavior.
- Commands and navigation will build on these binding foundations.

UI composition and styling build on the same runtime model:

- Dependency properties expose control state to binding, styles, templates, and animation.
- Routed events and commands provide different layers of user interaction handling.
- Layout panels control measurement and arrangement.
- Styles, templates, and resource dictionaries separate reusable visuals from behavior.

Reusable control authoring and modern app concerns sit on top of those foundations:

- `UserControl` helps with app-level composition while custom controls support theming and reusable control APIs.
- Performance work focuses on virtualization, layout cost, memory retention, and diagnostics.
- Advanced graphics, media, and interop are specialized capabilities rather than default application structure.
- Modern WPF development also includes SDK-style projects, deployment choices, and platform comparisons.

## Coverage Map

| Source Category | Questions | Pages |
|---|---:|---|
| WPF Basics | 1-9 | [WPF Overview And Runtime](wpf-overview-and-runtime.md), [Threading Dispatcher And Async](threading-dispatcher-and-async.md) |
| XAML | 10-18 | [XAML Fundamentals](xaml-fundamentals.md) |
| Data Binding | 19-26 | [Data Binding And Collection Views](data-binding-and-collection-views.md) |
| Controls and UI | 27-33 | [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md), [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md) |
| Styles, Templates, Resources | 34-38 | [Templates Styles And Resources](templates-styles-and-resources.md), [Resources](resources.md) |
| Animations and Media | 39-41 | [Advanced Graphics Media Interop](advanced-graphics-media-interop.md) |
| MVVM | 42-46 | [MVVM Commands And Navigation](mvvm-commands-and-navigation.md) |
| Events and Commands | 47-49 | [MVVM Commands And Navigation](mvvm-commands-and-navigation.md), [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md) |
| Performance Optimization | 50-52 | [Performance Memory And Diagnostics](performance-memory-and-diagnostics.md) |
| Advanced Features | 53-55 | [Advanced Graphics Media Interop](advanced-graphics-media-interop.md), [User Controls Custom Controls](user-controls-custom-controls.md) |
| Best Practices and Design Patterns | 56-58 | [MVVM Commands And Navigation](mvvm-commands-and-navigation.md), [User Controls Custom Controls](user-controls-custom-controls.md), [Performance Memory And Diagnostics](performance-memory-and-diagnostics.md) |
| WPF and .NET Core | 59-65 | [Modern Windows Desktop Apps](modern-windows-desktop-apps.md) |

## Resource Links

- WPF overview: https://learn.microsoft.com/dotnet/desktop/wpf/overview/
- XAML overview: https://learn.microsoft.com/dotnet/desktop/wpf/xaml/
- Data binding overview: https://learn.microsoft.com/dotnet/desktop/wpf/data/
- WPF threading model: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/threading-model
- WPF performance: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-wpf-application-performance
- WPF on .NET: https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio
- Windows App SDK: https://learn.microsoft.com/windows/apps/windows-app-sdk/
- WinUI: https://learn.microsoft.com/windows/apps/winui/

## Interactive Study Pages

Concept pages:

- [WPF Fundamentals Concept Map](wpf-fundamentals.concept.md)
- [WPF Binding And MVVM Concept Map](wpf-binding-mvvm.concept.md)
- [WPF UI Composition Concept Map](wpf-ui-composition.concept.md)
- [WPF Performance And Modern Desktop Concept Map](wpf-performance-modern-desktop.concept.md)
