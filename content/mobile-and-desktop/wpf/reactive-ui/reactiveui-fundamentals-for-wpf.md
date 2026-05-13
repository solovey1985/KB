# ReactiveUI Fundamentals For WPF

ReactiveUI view models are usually plain .NET classes that inherit from `ReactiveObject`. Instead of putting imperative UI logic in setters and event handlers, the common style is to describe how properties and commands relate to each other using observables.

Related topics: [ReactiveUI For WPF](index.md), [Reactive Commands Bindings And Activation](reactive-commands-bindings-and-activation.md)

## ReactiveObject

`ReactiveObject` provides `INotifyPropertyChanged` support plus observable change streams.

```csharp
public sealed class CustomerEditorViewModel : ReactiveObject
{
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
}
```

`RaiseAndSetIfChanged` is the standard property pattern for mutable view model state.

## Derived State With WhenAnyValue

ReactiveUI encourages describing derived state from other properties rather than pushing logic into setters.

```csharp
private readonly ObservableAsPropertyHelper<bool> _canSave;
public bool CanSave => _canSave.Value;

public CustomerEditorViewModel()
{
    this.WhenAnyValue(x => x.Name)
        .Select(name => !string.IsNullOrWhiteSpace(name))
        .ToProperty(this, x => x.CanSave, out _canSave);
}
```

This makes relationships explicit and easier to test.

## ObservableAsPropertyHelper And ToProperty

`ObservableAsPropertyHelper<T>` is used for read-only output properties that are produced by observable pipelines.

```csharp
private readonly ObservableAsPropertyHelper<string> _normalizedName;
public string NormalizedName => _normalizedName.Value;

public CustomerEditorViewModel()
{
    this.WhenAnyValue(x => x.Name)
        .Select(name => name?.Trim() ?? string.Empty)
        .ToProperty(this, x => x.NormalizedName, out _normalizedName);
}
```

Use normal mutable properties for user-editable state. Use OAPH-backed properties for computed output.

## Reactive Views In WPF

ReactiveUI provides WPF-friendly base classes that implement `IViewFor<TViewModel>`.

- `ReactiveUserControl<TViewModel>` for user controls
- `ReactiveWindow<TViewModel>` for windows

```csharp
public partial class CustomerEditorView : ReactiveUserControl<CustomerEditorViewModel>
{
    public CustomerEditorView()
    {
        InitializeComponent();
    }
}
```

These base classes are used so ReactiveUI bindings and activation patterns can work consistently in WPF.

## Source Generators And Boilerplate

ReactiveUI also supports source generators for reducing property and command boilerplate. That can be useful in large view model sets, but the explicit `RaiseAndSetIfChanged` and `ToProperty` patterns are still worth understanding because they explain the underlying model clearly.

## Resources

- View models: https://www.reactiveui.net/documentation/handbook/view-models/
- WhenAny: https://www.reactiveui.net/documentation/handbook/when-any/
- OAPH: https://www.reactiveui.net/documentation/handbook/observable-as-property-helper/
- Boilerplate code: https://www.reactiveui.net/documentation/handbook/view-models/boilerplate-code/
