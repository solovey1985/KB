# Advanced Graphics Media Interop

WPF supports richer desktop visuals than standard forms-style UI. It includes animation, media playback, drawing APIs, 3D primitives, and interop with older Windows UI stacks when full migration is not practical.

Related topics: [Templates Styles And Resources](templates-styles-and-resources.md), [Layout Controls And Visual Composition](layout-controls-and-visual-composition.md), [Modern Windows Desktop Apps](modern-windows-desktop-apps.md)

## Storyboards And Animations

Animations in WPF commonly use timelines and storyboards.

```xml
<Border x:Name="Card" Width="120" Height="60" Background="CornflowerBlue">
    <Border.Triggers>
        <EventTrigger RoutedEvent="Loaded">
            <BeginStoryboard>
                <Storyboard>
                    <DoubleAnimation Storyboard.TargetName="Card"
                                     Storyboard.TargetProperty="Opacity"
                                     From="0"
                                     To="1"
                                     Duration="0:0:0.4" />
                </Storyboard>
            </BeginStoryboard>
        </EventTrigger>
    </Border.Triggers>
</Border>
```

WPF can animate dependency properties, including transforms, opacity, colors, and layout-related values. Prefer transform and opacity animation over repeated layout-heavy animation when possible.

## Media Playback

WPF includes `MediaElement` for audio and video playback.

```xml
<MediaElement Source="intro.mp4"
              LoadedBehavior="Manual"
              UnloadedBehavior="Stop" />
```

Media support is tied to underlying Windows codecs, so behavior can vary by machine. For advanced playback scenarios, teams often use specialized libraries or native integrations.

## Drawing APIs

WPF offers multiple drawing layers.

- Shapes such as `Rectangle`, `Ellipse`, and `Path` are easiest for normal UI.
- `OnRender` is useful for lightweight custom drawing in controls.
- `DrawingVisual` is lower-level and efficient for large custom drawing surfaces.

`OnRender` example:

```csharp
protected override void OnRender(DrawingContext drawingContext)
{
    base.OnRender(drawingContext);
    drawingContext.DrawLine(
        new Pen(Brushes.DarkSlateBlue, 2),
        new Point(0, 0),
        new Point(ActualWidth, ActualHeight));
}
```

Use the simplest layer that meets the requirement. Shapes integrate best with layout and styling, while lower-level drawing scales better for custom visual surfaces.

## 3D Basics

WPF includes a basic retained-mode 3D system through `Viewport3D`, cameras, lights, meshes, materials, and transforms.

It is suitable for moderate 3D UI and educational scenarios, but it is not a modern high-performance 3D engine. For advanced rendering, teams usually integrate a specialized graphics technology.

## Windows Forms Interop

WPF and Windows Forms can host each other for gradual migration.

- `WindowsFormsHost` places a WinForms control inside WPF.
- `ElementHost` places a WPF control inside WinForms.

```xml
<WindowsFormsHost>
    <wf:MaskedTextBox x:Name="LegacyMaskedInput" />
</WindowsFormsHost>
```

Interop is useful for legacy controls and incremental upgrades, but it adds complexity around styling, DPI, airspace behavior, and input consistency.

## Interop Tradeoffs

Mixing rendering systems is not free.

- styling consistency becomes harder
- DPI and scaling differences can appear
- focus and keyboard behavior may need extra handling
- debugging visual composition becomes more complex

Use interop as a migration tool or a targeted exception, not the default architecture.

## Resources

- Animation overview: https://learn.microsoft.com/dotnet/desktop/wpf/graphics-multimedia/animation-overview
- Storyboards overview: https://learn.microsoft.com/dotnet/desktop/wpf/graphics-multimedia/storyboards-overview
- Graphics rendering overview: https://learn.microsoft.com/dotnet/desktop/wpf/graphics-multimedia/wpf-graphics-rendering-overview
- 3D graphics overview: https://learn.microsoft.com/dotnet/desktop/wpf/graphics-multimedia/3-d-graphics-overview
- WPF and Windows Forms interoperation: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/wpf-and-windows-forms-interoperation
