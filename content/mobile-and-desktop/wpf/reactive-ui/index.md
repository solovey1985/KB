# ReactiveUI For WPF

This topic covers how ReactiveUI is used in WPF applications as a separate application architecture and MVVM style, without mixing it into the framework-neutral WPF fundamentals pages.

ReactiveUI builds on `System.Reactive` and provides strongly typed bindings, reactive state composition, async-aware commands, and view activation patterns that fit WPF desktop applications well.

## Learning Path

1. [ReactiveUI Fundamentals For WPF](reactiveui-fundamentals-for-wpf.md)
2. [Reactive Commands Bindings And Activation](reactive-commands-bindings-and-activation.md)
3. [Routing And Application Composition](routing-and-application-composition.md)

## When ReactiveUI Is Useful In WPF

ReactiveUI is a strong fit when a WPF application has:

- complex view state derived from multiple inputs
- async workflows that need explicit loading and error handling
- many commands with shared executability rules
- reusable MVVM patterns across multiple screens or platforms
- a team already comfortable with Rx-style composition

It is usually less useful for small apps where standard WPF binding and a small relay-command pattern already keep the code easy to understand.

## Main Concepts In This Topic

- `ReactiveObject` for observable view model state
- `WhenAnyValue` and `ToProperty` for derived state
- `ReactiveCommand` for async-aware user actions
- `Bind`, `OneWayBind`, and `BindCommand` for typed view bindings
- `WhenActivated` and `DisposeWith` for view lifetime management on WPF
- `IScreen`, `RoutingState`, and `RoutedViewHost` for navigation

## Package And Setup Notes

ReactiveUI guidance currently recommends modern app bootstrap through `RxAppBuilder` with `.WithWpf()`. WPF applications also commonly use `ReactiveUI.WPF` plus `Splat` for registration and service location patterns.

## Resources

- ReactiveUI home: https://www.reactiveui.net/
- Getting started: https://www.reactiveui.net/documentation/getting-started/
- Handbook: https://www.reactiveui.net/documentation/handbook/
- WPF samples: https://github.com/reactiveui/ReactiveUI/tree/main/src/examples/ReactiveUI.Samples.Wpf
- Builder-based WPF sample: https://github.com/reactiveui/ReactiveUI/tree/main/src/examples/ReactiveUI.Builder.WpfApp
