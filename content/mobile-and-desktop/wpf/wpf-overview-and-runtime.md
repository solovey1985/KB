# WPF Overview And Runtime

Windows Presentation Foundation is Microsoft's XAML-based UI framework for Windows desktop applications. It is still relevant for Windows-only business apps that need mature desktop controls, deep Windows integration, strong data binding, and long-term compatibility.

Related topics: [XAML Fundamentals](xaml-fundamentals.md), [Threading Dispatcher And Async](threading-dispatcher-and-async.md), [Data Binding And Collection Views](data-binding-and-collection-views.md)

## Main Components

WPF applications are built from several cooperating systems:

- XAML for declarative UI structure.
- Controls such as `Button`, `TextBox`, `ListBox`, `DataGrid`, and `ContentControl`.
- Layout containers such as `Grid`, `StackPanel`, `DockPanel`, and `Canvas`.
- Dependency properties for binding, styling, animation, inheritance, and metadata.
- Routed events and commands for input handling.
- Resources, styles, templates, and data templates for visual reuse.
- Rendering through a retained-mode visual system.

Minimal WPF window:

```xml
<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Orders" Height="300" Width="500">
    <Grid Margin="16">
        <TextBlock Text="WPF is running" FontSize="24" />
    </Grid>
</Window>
```

## Logical Tree And Visual Tree

The logical tree describes the content relationships you author: a `Window` contains a `Grid`; the `Grid` contains a `Button`.

The visual tree is the expanded rendering structure after templates are applied. A simple `Button` may contain borders, presenters, text visuals, and state visuals.

Why this matters:

- Resource lookup often follows logical relationships.
- Event routing travels through element trees.
- Template debugging requires visual-tree inspection.
- Performance issues often come from too many generated visuals.

Example visual-tree inspection helper:

```csharp
static T? FindVisualChild<T>(DependencyObject root) where T : DependencyObject
{
    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
    {
        var child = VisualTreeHelper.GetChild(root, i);
        if (child is T match) return match;

        var nested = FindVisualChild<T>(child);
        if (nested is not null) return nested;
    }

    return null;
}
```

## Resolution Independence And High DPI

WPF uses device-independent pixels. One WPF unit is 1/96 inch, not necessarily one physical pixel. This lets layout scale with monitor DPI.

Use these habits for modern displays:

- Prefer vector assets for icons when possible.
- Provide high-resolution bitmap assets when bitmaps are required.
- Enable layout rounding to avoid blurry subpixel edges.
- Test per-monitor DPI setups when the app moves across monitors.

```xml
<Window UseLayoutRounding="True"
        SnapsToDevicePixels="True">
    <Grid>
        <TextBlock Text="DPI aware layout" />
    </Grid>
</Window>
```

## WPF On Modern .NET

WPF on .NET Core 3+ and modern .NET is Windows-only but actively supported as part of the Windows desktop workload. Modern WPF apps normally use SDK-style projects and target `net8.0-windows` or another Windows-specific target framework.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
</Project>
```

Use WPF when you need mature Windows desktop functionality. Consider WinUI 3 or Windows App SDK for newer Windows platform UI patterns, and Avalonia or .NET MAUI when cross-platform desktop is a core requirement.

## Resources

- WPF overview: https://learn.microsoft.com/dotnet/desktop/wpf/overview/
- Create a WPF app on .NET: https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio
- WPF trees: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/trees-in-wpf
- High DPI desktop guidance: https://learn.microsoft.com/windows/win32/hidpi/high-dpi-desktop-application-development-on-windows
