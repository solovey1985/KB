# User Controls Custom Controls

WPF offers two main reuse mechanisms for UI building blocks: `UserControl` and custom controls. They solve different problems and should not be treated as interchangeable.

Related topics: [Templates Styles And Resources](templates-styles-and-resources.md), [Dependency Properties And Routed Events](dependency-properties-and-routed-events.md), [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md)

## UserControl

`UserControl` is best when you want to bundle an existing chunk of XAML and code into a reusable composite view.

```xml
<UserControl x:Class="WpfApp.Controls.CustomerCard"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Border Padding="12" BorderBrush="Gray" BorderThickness="1">
        <StackPanel>
            <TextBlock Text="{Binding Name}" FontWeight="Bold" />
            <TextBlock Text="{Binding Email}" />
        </StackPanel>
    </Border>
</UserControl>
```

Use `UserControl` when:

- the visual structure is mostly fixed
- reuse is local to your app or a small set of screens
- full lookless templating is not required

Avoid embedding too much business logic in the code-behind. A `UserControl` should still expose a clean API and rely on binding where practical.

## Custom Control

A custom control derives from `Control` or another WPF base control and defines its appearance through templates. This is the right choice for reusable control libraries and themeable controls.

```csharp
public class RatingControl : Control
{
    static RatingControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(RatingControl),
            new FrameworkPropertyMetadata(typeof(RatingControl)));
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int), typeof(RatingControl), new PropertyMetadata(0));

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
```

Use a custom control when:

- the control needs to be skinnable through `ControlTemplate`
- consumers should be able to restyle it without rewriting logic
- the control belongs in a shared library
- it needs dependency properties, visual states, and richer template parts

## Generic.xaml And Default Style Keys

Custom controls usually define their default style in `Themes/Generic.xaml`.

```xml
<Style TargetType="{x:Type local:RatingControl}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:RatingControl}">
                <Border Padding="8" Background="LightGray">
                    <TextBlock Text="{TemplateBinding Value}" />
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

`DefaultStyleKeyProperty.OverrideMetadata` tells WPF to look up the default style by control type.

## Dependency Properties And API Design

For reusable controls, dependency properties are the public surface area. Expose state that must be bindable, styleable, or template-aware.

Good control APIs usually:

- use dependency properties for bindable state
- keep names clear and consistent with built-in WPF controls
- avoid exposing internal template elements directly
- document required template parts and expected visual states when needed

If a control requires named template elements, override `OnApplyTemplate`.

```csharp
public override void OnApplyTemplate()
{
    base.OnApplyTemplate();
    _valueText = GetTemplateChild("PART_ValueText") as TextBlock;
}
```

## UserControl Versus Custom Control

Choose `UserControl` for composition. Choose a custom control for reusable behavior plus interchangeable presentation.

Practical rule:

- `UserControl`: app-level reusable view
- custom control: framework-like reusable control

Starting with `UserControl` is often fine. Move to a custom control when theming, control templating, or external reuse becomes a real requirement.

## Resources

- Control authoring overview: https://learn.microsoft.com/dotnet/desktop/wpf/controls/control-authoring-overview
- Creating a control with an existing customizable appearance: https://learn.microsoft.com/dotnet/desktop/wpf/controls/creating-a-control-that-has-a-customizable-appearance
- ControlTemplate: https://learn.microsoft.com/dotnet/api/system.windows.controls.controltemplate
- Dependency properties overview: https://learn.microsoft.com/dotnet/desktop/wpf/properties/dependency-properties-overview
