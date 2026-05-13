# XAML Fundamentals

XAML is the declarative markup language used by WPF to construct object graphs, set properties, connect resources, and describe bindings. It keeps UI structure readable and works naturally with MVVM.

Related topics: [WPF Overview And Runtime](wpf-overview-and-runtime.md), [Data Binding And Collection Views](data-binding-and-collection-views.md)

## Namespaces

Every WPF XAML file declares XML namespaces. The default namespace maps unprefixed elements such as `Grid` and `Button` to WPF controls. The `x` namespace provides XAML language features such as `x:Class`, `x:Key`, and `x:Name`.

```xml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:WpfApp">
</Window>
```

Use `clr-namespace:` to reference your own controls, converters, and view models from XAML.

## Attribute Syntax And Property Elements

Attribute syntax is concise and works well for simple values.

```xml
<TextBlock Text="Customer" FontWeight="SemiBold" />
```

Property element syntax is better when a property value is complex, contains nested objects, or spans multiple lines.

```xml
<Button>
    <Button.Content>
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="Save" />
            <TextBlock Text="Ctrl+S" Margin="8,0,0,0" Opacity="0.7" />
        </StackPanel>
    </Button.Content>
</Button>
```

## Markup Extensions

Markup extensions provide dynamic values inside braces. Common examples include `Binding`, `StaticResource`, `DynamicResource`, `x:Static`, and `RelativeSource`.

```xml
<TextBlock Text="{Binding CustomerName}" />
<Button Style="{StaticResource PrimaryButtonStyle}" />
```

`StaticResource` resolves once during loading. `DynamicResource` stays connected and can update when the resource changes, which is useful for runtime theme switching.

## Resources And x:Key

Resources store reusable objects such as styles, brushes, converters, templates, and dimensions.

```xml
<Window.Resources>
    <SolidColorBrush x:Key="AccentBrush" Color="#2563EB" />

    <Style x:Key="PrimaryButtonStyle" TargetType="Button">
        <Setter Property="Background" Value="{StaticResource AccentBrush}" />
        <Setter Property="Foreground" Value="White" />
        <Setter Property="Padding" Value="12,6" />
    </Style>
</Window.Resources>
```

`x:Key` gives a resource an explicit lookup key. A style without `x:Key` but with `TargetType` becomes an implicit style for that target type within the resource scope.

## Attached Properties

Attached properties let a parent layout or service define properties on child elements. `Grid.Row`, `Grid.Column`, and `DockPanel.Dock` are common examples.

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <TextBlock Grid.Row="0" Text="Header" />
    <ListBox Grid.Row="1" ItemsSource="{Binding Orders}" />
</Grid>
```

## Event Handlers In XAML

XAML can wire events directly to code-behind, but MVVM apps usually prefer commands for user actions.

```xml
<Button Content="Refresh" Click="RefreshButton_Click" />
```

```csharp
private void RefreshButton_Click(object sender, RoutedEventArgs e)
{
    // Prefer commands for real MVVM workflows.
}
```

Use code-behind for view-specific behavior. Use commands when the action belongs to the view model.

## Resources

- XAML overview: https://learn.microsoft.com/dotnet/desktop/wpf/xaml/
- XAML namespaces: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/xaml-namespaces-and-namespace-mapping-for-wpf-xaml
- WPF resources: https://learn.microsoft.com/dotnet/desktop/wpf/systems/xaml-resources-overview
- Markup extensions: https://learn.microsoft.com/dotnet/desktop/xaml-services/markup-extensions-overview
