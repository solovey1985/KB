# Dependency Properties And Routed Events

Dependency properties and routed events are core WPF infrastructure. They enable binding, styling, animation, resource inheritance, layout behavior, and event routing through element trees.

Related topics: [WPF Overview And Runtime](wpf-overview-and-runtime.md), [XAML Fundamentals](xaml-fundamentals.md), [MVVM Commands And Navigation](mvvm-commands-and-navigation.md)

## Dependency Properties

A dependency property is a WPF property backed by the dependency property system instead of a normal field. It supports binding, styling, animation, default values, metadata, inheritance, and change callbacks.

```csharp
public sealed class RatingControl : Control
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
```

Use dependency properties for control APIs that need XAML binding, styling, animation, or template interaction. Use normal CLR properties for ordinary view model state.

## Property Metadata And Callbacks

Metadata controls behavior such as default values, binding mode, layout invalidation, value inheritance, and property-changed callbacks.

```csharp
public static readonly DependencyProperty IsBusyProperty =
    DependencyProperty.Register(
        nameof(IsBusy),
        typeof(bool),
        typeof(BusyIndicator),
        new FrameworkPropertyMetadata(false, OnIsBusyChanged));

private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    ((BusyIndicator)d).UpdateVisualState();
}
```

Keep callbacks small. They run on the UI thread and can be triggered frequently by binding, styles, animations, or coercion.

## Attached Properties

Attached properties let one type define properties that can be set on another type. Layout panels use this heavily.

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>

    <TextBlock Grid.Column="0" Text="Name" />
    <TextBox Grid.Column="1" Text="{Binding Name}" />
</Grid>
```

Custom attached properties are useful for reusable view behaviors, but avoid turning them into hidden business logic.

## Routed Events

Routed events travel through the element tree instead of only notifying the element that raised them.

Routing strategies:

- Bubbling: starts at the source and moves upward, such as `Button.Click`.
- Tunneling: starts at the root and moves downward, usually named with `Preview`.
- Direct: behaves like a normal CLR event.

```xml
<StackPanel Button.Click="AnyButton_Click">
    <Button Content="Save" />
    <Button Content="Cancel" />
</StackPanel>
```

```csharp
private void AnyButton_Click(object sender, RoutedEventArgs e)
{
    if (e.OriginalSource is Button button)
    {
        Debug.WriteLine(button.Content);
    }
}
```

## Commands Versus Events

Use events for view-specific behavior, such as focus, animations, and local input behavior. Use commands for user actions that belong to the view model.

```xml
<Button Content="Save" Command="{Binding SaveCommand}" />
```

Commands integrate with `CanExecute`, menus, buttons, keyboard gestures, and MVVM. Routed events remain useful for lower-level control and input handling.

## Resources

- Dependency properties overview: https://learn.microsoft.com/dotnet/desktop/wpf/properties/dependency-properties-overview
- Custom dependency properties: https://learn.microsoft.com/dotnet/desktop/wpf/properties/custom-dependency-properties
- Attached properties overview: https://learn.microsoft.com/dotnet/desktop/wpf/properties/attached-properties-overview
- Routed events overview: https://learn.microsoft.com/dotnet/desktop/wpf/events/routed-events-overview
