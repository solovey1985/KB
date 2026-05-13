# Layout Controls And Visual Composition

WPF layout is a two-pass system: measure determines desired size, arrange assigns final size and position. Choosing the right panel and item control has a major impact on correctness, maintainability, and performance.

Related topics: [WPF Overview And Runtime](wpf-overview-and-runtime.md), [XAML Fundamentals](xaml-fundamentals.md), [Data Binding And Collection Views](data-binding-and-collection-views.md)

## Core Layout Panels

Use `Grid` for most application screens. It handles rows, columns, proportional sizing, and alignment well.

```xml
<Grid Margin="16">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <TextBlock Grid.Row="0" Text="Customers" FontSize="20" />
    <ListView Grid.Row="1" ItemsSource="{Binding Customers}" />
</Grid>
```

Use `StackPanel` for simple one-dimensional stacking. Be careful inside scrollable item controls because it can measure children with infinite space and disable virtualization patterns.

Use `DockPanel` for toolbars/status bars around a central region. Use `Canvas` only when absolute positioning is the actual requirement.

## Content Controls And ContentPresenter

`ContentControl` displays one piece of content. `ContentPresenter` is the template placeholder that renders that content.

```xml
<ContentControl Content="{Binding CurrentPage}" />
```

Inside a custom control template, `ContentPresenter` preserves the consumer-provided content.

```xml
<ControlTemplate TargetType="Button">
    <Border Padding="8" Background="{TemplateBinding Background}">
        <ContentPresenter HorizontalAlignment="Center"
                          VerticalAlignment="Center" />
    </Border>
</ControlTemplate>
```

## ItemsControl, ListBox, And ListView

`ItemsControl` displays collections. `ListBox` adds selection. `ListView` adds richer view support such as `GridView`.

```xml
<ListView ItemsSource="{Binding Orders}" SelectedItem="{Binding SelectedOrder}">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="Order" DisplayMemberBinding="{Binding OrderNumber}" />
            <GridViewColumn Header="Total" DisplayMemberBinding="{Binding Total}" />
        </GridView>
    </ListView.View>
</ListView>
```

Use `DataTemplate` when the visual should be more than a simple text member.

## ScrollViewer And Virtualization

To add scrolling around arbitrary content, wrap it in `ScrollViewer`.

```xml
<ScrollViewer VerticalScrollBarVisibility="Auto">
    <StackPanel>
        <!-- content -->
    </StackPanel>
</ScrollViewer>
```

For large lists, prefer virtualizing item controls instead of wrapping lists in external `ScrollViewer` controls. External scroll viewers can disable item virtualization.

```xml
<ListBox ItemsSource="{Binding Items}"
         VirtualizingPanel.IsVirtualizing="True"
         VirtualizingPanel.VirtualizationMode="Recycling"
         ScrollViewer.CanContentScroll="True" />
```

## Visual Composition Tradeoffs

WPF's retained visual tree is powerful, but every element has cost. Deep trees, nested panels, many bindings, heavy templates, and unnecessary effects can slow layout and rendering.

Practical habits:

- Prefer fewer nested panels.
- Use `Grid` instead of multiple nested `StackPanel` containers when layout is tabular.
- Avoid bitmap effects and expensive opacity animations on large regions.
- Use virtualization for large item sets.
- Freeze `Freezable` objects such as brushes when reused from code.

## Resources

- Layout overview: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/layout
- Panels overview: https://learn.microsoft.com/dotnet/desktop/wpf/controls/panels-overview
- ContentPresenter: https://learn.microsoft.com/dotnet/api/system.windows.controls.contentpresenter
- Optimizing controls performance: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/optimizing-performance-controls
