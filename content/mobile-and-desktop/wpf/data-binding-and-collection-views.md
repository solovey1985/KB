# Data Binding And Collection Views

Data binding is one of WPF's core strengths. It connects UI properties to data sources and is the foundation of MVVM-style applications.

Related topics: [XAML Fundamentals](xaml-fundamentals.md), [Threading Dispatcher And Async](threading-dispatcher-and-async.md)

## Binding Basics

A binding connects a target dependency property to a source property.

```xml
<TextBox Text="{Binding CustomerName, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
<TextBlock Text="{Binding CustomerName}" />
```

Common binding modes:

- `OneWay`: source updates target.
- `TwoWay`: source and target update each other.
- `OneTime`: target is initialized once.
- `OneWayToSource`: target updates source.

Most display text is `OneWay`. Editable form fields are often `TwoWay`.

## INotifyPropertyChanged

For binding updates from a view model, implement `INotifyPropertyChanged`.

```csharp
public sealed class CustomerViewModel : INotifyPropertyChanged
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

Raise `PropertyChanged` for computed properties too when their dependencies change.

## Observable Collections

Use `ObservableCollection<T>` when the UI needs to react to items being added, removed, or reset.

```csharp
public ObservableCollection<OrderViewModel> Orders { get; } = new();
```

```xml
<ListBox ItemsSource="{Binding Orders}" DisplayMemberPath="OrderNumber" />
```

`ObservableCollection<T>` notifies collection changes, not changes inside each item. Each item still needs `INotifyPropertyChanged` if its displayed properties can change.

## Collection Views

WPF creates a default view over collections. A `CollectionView` can sort, filter, group, and track current item without changing the underlying collection.

```csharp
var view = CollectionViewSource.GetDefaultView(Orders);
view.SortDescriptions.Add(new SortDescription(nameof(OrderViewModel.OrderDate), ListSortDirection.Descending));
view.Filter = item => item is OrderViewModel order && order.IsOpen;
```

Grouping example in XAML:

```xml
<CollectionViewSource x:Key="OrdersView" Source="{Binding Orders}">
    <CollectionViewSource.GroupDescriptions>
        <PropertyGroupDescription PropertyName="Status" />
    </CollectionViewSource.GroupDescriptions>
</CollectionViewSource>

<ListView ItemsSource="{Binding Source={StaticResource OrdersView}}" />
```

## UpdateSourceTrigger

`UpdateSourceTrigger` controls when a target value writes back to the source.

```xml
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
```

Use `PropertyChanged` for search boxes or live validation. Use the default `LostFocus` behavior when every keystroke should not update the model.

## RelativeSource And Element Binding

`RelativeSource` finds another object relative to the current binding target. It is useful inside templates where the normal `DataContext` points to data rather than the control.

```xml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}" />
```

Element-to-element binding references another named element.

```xml
<Slider x:Name="ZoomSlider" Minimum="0.5" Maximum="2" Value="1" />
<TextBlock FontSize="{Binding ElementName=ZoomSlider, Path=Value}" Text="Preview" />
```

## Value Converters

Converters adapt a source value to a target property type or display shape.

```csharp
public sealed class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility.Visible;
    }
}
```

Prefer simple converters. If the converter contains business logic, move that logic into the view model.

## Resources

- Data binding overview: https://learn.microsoft.com/dotnet/desktop/wpf/data/
- Binding declarations: https://learn.microsoft.com/dotnet/desktop/wpf/data/binding-declarations-overview
- Collection views: https://learn.microsoft.com/dotnet/desktop/wpf/data/how-to-sort-data-in-a-view
- `INotifyPropertyChanged`: https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged
