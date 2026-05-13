# MVVM Commands And Navigation

MVVM is the dominant WPF application pattern because WPF binding and commands make it practical to separate UI markup from state, behavior, and testable application logic.

Related topics: [Data Binding And Collection Views](data-binding-and-collection-views.md), [Threading Dispatcher And Async](threading-dispatcher-and-async.md), [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md)

## MVVM Roles

The view is XAML and view-specific behavior. The view model exposes state and commands for binding. The model represents business data and domain behavior.

```csharp
public sealed class CustomerDetailsViewModel : INotifyPropertyChanged
{
    private string _customerName = string.Empty;

    public string CustomerName
    {
        get => _customerName;
        set
        {
            if (_customerName == value) return;
            _customerName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerName)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
```

Keep the view model free of direct `TextBox`, `Window`, and `Button` references. Use interfaces or services for dialogs, navigation, and file picking when those behaviors must be testable.

## Binding A ViewModel To A View

For simple apps, setting `DataContext` directly is enough. Larger apps usually rely on dependency injection or view model locators.

```xml
<Window x:Class="WpfApp.CustomerDetailsView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="clr-namespace:WpfApp.ViewModels">
    <Window.DataContext>
        <vm:CustomerDetailsViewModel />
    </Window.DataContext>

    <TextBox Text="{Binding CustomerName, UpdateSourceTrigger=PropertyChanged}" />
</Window>
```

Avoid doing heavy work in view model constructors. Use async initialization methods or loading commands when data comes from I/O.

## ICommand And Relay Commands

`ICommand` lets buttons and menu items call view model behavior without code-behind event handlers.

```csharp
public sealed class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => _execute();
    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
```

```xml
<Button Content="Save" Command="{Binding SaveCommand}" />
```

For production apps, prefer a mature implementation from a toolkit such as CommunityToolkit.Mvvm or Prism rather than rewriting command infrastructure everywhere.

## Async Commands

Long-running commands should not block the UI thread. Use an async command abstraction that disables duplicate execution and observes exceptions.

```csharp
public async Task SaveAsync()
{
    IsBusy = true;
    try
    {
        await _customerService.SaveAsync(Customer);
    }
    finally
    {
        IsBusy = false;
    }
}
```

Do not use `async void` for view model commands except through a command wrapper that handles exceptions.

## Navigation Patterns

WPF navigation depends on application style.

- `Frame` and `Page` work for page-style apps.
- A shell view with `ContentControl` and view-model-first templates works well for MVVM apps.
- Dialogs are best wrapped behind an interface so view models do not create windows directly.

View-model-first navigation example:

```xml
<Window.Resources>
    <DataTemplate DataType="{x:Type vm:CustomersViewModel}">
        <views:CustomersView />
    </DataTemplate>
</Window.Resources>

<ContentControl Content="{Binding CurrentPage}" />
```

The shell view model changes `CurrentPage`; WPF chooses the matching data template.

## Resources

- MVVM Toolkit: https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/
- Commanding overview: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/commanding-overview
- Data templating overview: https://learn.microsoft.com/dotnet/desktop/wpf/data/data-templating-overview
- Prism WPF: https://prismlibrary.com/docs/wpf/
