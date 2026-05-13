# Routing And Application Composition

ReactiveUI includes a view-model-first routing model for WPF applications. It is useful when the application has a shell, a navigation stack, and multiple screens that should be composed without tightly coupling views to navigation logic.

Related topics: [ReactiveUI For WPF](index.md), [Reactive Commands Bindings And Activation](reactive-commands-bindings-and-activation.md)

## Routing Building Blocks

ReactiveUI routing for WPF centers on four concepts:

- `IScreen` as the navigation host
- `RoutingState` as the navigation stack
- `IRoutableViewModel` as a navigable view model
- `RoutedViewHost` as the WPF control that renders the active route

```csharp
public sealed class MainViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new();
}
```

```csharp
public sealed class DetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment => "details";
    public IScreen HostScreen { get; }

    public DetailsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}
```

## RoutedViewHost In WPF

The active route is displayed through `RoutedViewHost`.

```xml
<rxui:ReactiveWindow
    x:Class="WpfApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:rxui="http://reactiveui.net">
    <Grid>
        <rxui:RoutedViewHost x:Name="RouterHost" />
    </Grid>
</rxui:ReactiveWindow>
```

```csharp
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.Router, v => v.RouterHost.Router)
                .DisposeWith(disposables);
        });
    }
}
```

## Navigation Commands

Routing actions are commonly exposed as `ReactiveCommand` instances.

```csharp
public ReactiveCommand<Unit, IRoutableViewModel> OpenDetails { get; }

public MainViewModel()
{
    OpenDetails = ReactiveCommand.CreateFromObservable(
        () => Router.Navigate.Execute(new DetailsViewModel(this)));
}
```

This keeps navigation explicit and testable at the view-model layer.

## App Bootstrap And Registration

ReactiveUI now recommends bootstrapping through `RxAppBuilder`.

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)
    .WithRegistration(locator =>
    {
        locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
    })
    .BuildApp();
```

ReactiveUI applications also commonly use `Splat` for registration, view resolution, and lightweight DI patterns.

## When To Use Routing

ReactiveUI routing is a good fit when:

- the app has a shell and multiple screen transitions
- navigation should be controlled by view models
- view lookup should remain separate from navigation intent

It may be unnecessary for a simple window/dialog application with only local content swapping.

## Resources

- Routing: https://www.reactiveui.net/documentation/handbook/routing
- RxAppBuilder: https://www.reactiveui.net/documentation/handbook/rxappbuilder/
- View location: https://www.reactiveui.net/documentation/handbook/view-location/
- Splat: https://github.com/reactiveui/splat
