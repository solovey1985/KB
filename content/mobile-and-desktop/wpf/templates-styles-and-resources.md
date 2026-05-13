# Templates Styles And Resources

WPF separates control behavior from presentation. Styles set properties, templates define visuals, and resources make visual assets reusable across scopes.

Related topics: [XAML Fundamentals](xaml-fundamentals.md), [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md), [User Controls Custom Controls](user-controls-custom-controls.md)

## Styles

A style groups property setters and triggers for a target type. An explicit style uses `x:Key`; an implicit style applies by `TargetType` within its resource scope.

```xml
<Window.Resources>
    <Style TargetType="Button">
        <Setter Property="Padding" Value="12,6" />
        <Setter Property="Margin" Value="4" />
    </Style>
</Window.Resources>
```

Explicit style:

```xml
<Style x:Key="PrimaryButtonStyle" TargetType="Button">
    <Setter Property="Background" Value="#2563EB" />
    <Setter Property="Foreground" Value="White" />
</Style>
```

```xml
<Button Content="Save" Style="{StaticResource PrimaryButtonStyle}" />
```

## StaticResource And DynamicResource

`StaticResource` resolves during loading and is usually the default choice. `DynamicResource` keeps a runtime resource reference and can update when the resource changes.

```xml
<Border Background="{StaticResource CardBackgroundBrush}" />
<TextBlock Foreground="{DynamicResource AppForegroundBrush}" />
```

Use `DynamicResource` for runtime theme changes. Do not use it everywhere by default because it has extra lookup behavior.

## DataTemplate

`DataTemplate` defines how data objects are displayed. It is used by content controls and item controls.

```xml
<DataTemplate DataType="{x:Type vm:OrderViewModel}">
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="{Binding OrderNumber}" FontWeight="SemiBold" />
        <TextBlock Text="{Binding Total, StringFormat=C}" Margin="8,0,0,0" />
    </StackPanel>
</DataTemplate>
```

The data object remains the `DataContext` inside the template.

## ControlTemplate

`ControlTemplate` changes a control's visual structure while preserving its behavior and public API.

```xml
<Style x:Key="RoundedButtonStyle" TargetType="Button">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border Background="{TemplateBinding Background}"
                        CornerRadius="6"
                        Padding="{TemplateBinding Padding}">
                    <ContentPresenter HorizontalAlignment="Center"
                                      VerticalAlignment="Center" />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

Use `TemplateBinding` for simple bindings from template visuals to the templated control.

## Style Triggers

Triggers react to property values and can change setters inside a style.

```xml
<Style TargetType="TextBox">
    <Setter Property="BorderBrush" Value="Gray" />
    <Style.Triggers>
        <Trigger Property="IsKeyboardFocusWithin" Value="True">
            <Setter Property="BorderBrush" Value="#2563EB" />
        </Trigger>
    </Style.Triggers>
</Style>
```

For complex state transitions in custom controls, use `VisualStateManager` rather than many scattered triggers.

## Resource Dictionaries

Resource dictionaries organize shared styles, brushes, templates, converters, and dimensions. Use merged dictionaries for theme and module separation.

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Themes/Colors.xaml" />
            <ResourceDictionary Source="Themes/Controls.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

Keep resource dictionaries scoped. Application-wide resources are convenient but can become hard to maintain when every screen depends on them.

## Resources

- Styles and templates overview: https://learn.microsoft.com/dotnet/desktop/wpf/controls/styles-templates-overview
- Data templating overview: https://learn.microsoft.com/dotnet/desktop/wpf/data/data-templating-overview
- ControlTemplate: https://learn.microsoft.com/dotnet/api/system.windows.controls.controltemplate
- WPF resources overview: https://learn.microsoft.com/dotnet/desktop/wpf/systems/xaml-resources-overview
