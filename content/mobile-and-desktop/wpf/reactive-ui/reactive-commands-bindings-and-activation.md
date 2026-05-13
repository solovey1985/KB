# Reactive Commands Bindings And Activation

ReactiveUI replaces a large amount of string-based XAML wiring with typed bindings and observable command patterns in view code. On WPF, that is especially valuable for async workflows and for managing view lifetime safely.

Related topics: [ReactiveUI Fundamentals For WPF](reactiveui-fundamentals-for-wpf.md), [Routing And Application Composition](routing-and-application-composition.md)

## ReactiveCommand

`ReactiveCommand` is an async-aware implementation of `ICommand`. It adds observable execution state, typed outputs, and explicit exception handling.

```csharp
public ReactiveCommand<Unit, Unit> SaveCommand { get; }

public CustomerEditorViewModel()
{
    var canSave = this.WhenAnyValue(x => x.Name)
        .Select(name => !string.IsNullOrWhiteSpace(name));

    SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync, canSave);

    SaveCommand.ThrownExceptions.Subscribe(error =>
    {
        // Handle expected save failures here.
    });
}
```

Important behaviors:

- async commands block re-entry while executing
- `IsExecuting` exposes running state as an observable
- failures flow through `ThrownExceptions`
- command results can be observed like any other observable

## Typed Bindings In WPF Views

ReactiveUI bindings are usually set up in the view constructor inside `WhenActivated`.

```csharp
public partial class CustomerEditorView : ReactiveUserControl<CustomerEditorViewModel>
{
    public CustomerEditorView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.Name, v => v.NameTextBox.Text)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.SaveButton)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.CanSave, v => v.SaveButton.IsEnabled)
                .DisposeWith(disposables);
        });
    }
}
```

Common binding helpers:

- `Bind` for two-way binding
- `OneWayBind` for view model to view
- `BindCommand` for command wiring
- `BindTo` for custom observable-to-property flows

## Why Use WhenActivated On WPF

ReactiveUI specifically calls out XAML and dependency-property-based platforms such as WPF as places where bindings and subscriptions can leak if they outlive the visual attachment of a view.

`WhenActivated` provides a safe place to:

- attach bindings
- subscribe to view-model observables
- hook view events
- dispose everything when the view deactivates

```csharp
this.WhenActivated(disposables =>
{
    this.WhenAnyValue(v => v.ViewModel.IsBusy)
        .BindTo(this, v => v.BusyIndicator.Visibility)
        .DisposeWith(disposables);
});
```

## Practical WPF Guidance

- Put bindings in `WhenActivated` for WPF views.
- Do not ignore `ThrownExceptions` on commands that can fail.
- Prefer view-model properties and command pipelines over view event handlers for business behavior.
- Keep view code focused on bindings, focus management, and view-only concerns.

## Resources

- Commands: https://www.reactiveui.net/documentation/handbook/commands/
- Data binding: https://www.reactiveui.net/documentation/handbook/data-binding/
- WhenActivated: https://www.reactiveui.net/documentation/handbook/when-activated/
