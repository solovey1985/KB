# Modern Windows Desktop Apps

Modern WPF work is not only about classic XAML skills. It also includes SDK-style projects, current .NET versions, deployment choices, migration strategy, and understanding when WPF is still the right technology compared with newer desktop UI stacks.

Related topics: [WPF Overview And Runtime](wpf-overview-and-runtime.md), [Performance Memory And Diagnostics](performance-memory-and-diagnostics.md), [Resources](resources.md)

## WPF On .NET Versus .NET Framework

WPF started on .NET Framework and now also runs on modern .NET. New WPF applications should usually target current supported .NET releases unless they depend on older framework-only libraries.

Modern .NET WPF gives you:

- SDK-style project files
- current runtime and language improvements
- modern tooling and packaging paths
- simpler multi-project dependency management

Legacy .NET Framework WPF apps still exist widely, especially in enterprise environments with older dependencies or deployment constraints.

## SDK-Style Projects

Modern WPF projects use concise SDK-style project files.

```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

This model is easier to maintain than older verbose project files and fits current .NET build tooling.

## Deployment Options

Common deployment models include:

- framework-dependent deployment
- self-contained deployment
- MSIX packaging
- single-file publishing, with caveats depending on assets and runtime behavior

There is no universally best option. The right choice depends on install experience, update strategy, machine control, app size, and corporate IT constraints.

## Migration Strategy

Many teams maintain existing WPF code rather than rewriting it. Incremental migration is often safer than full replacement.

Practical migration path:

1. move to SDK-style projects where possible
2. update class libraries and dependencies
3. migrate to supported .NET versions
4. improve architecture, diagnostics, and testability while migrating

Large UI rewrites are expensive and risky unless there is a clear product reason beyond technical freshness.

## Relationship To Newer Desktop Stacks

WPF remains a strong option for Windows-only desktop apps, especially where mature data-heavy UI, existing investments, and broad third-party control ecosystems matter.

Compare with nearby options:

- WinUI 3 / Windows App SDK: modern Windows-native direction, but different maturity and ecosystem tradeoffs
- .NET MAUI: cross-platform focus, not a direct WPF replacement for every desktop scenario
- Avalonia: XAML-inspired cross-platform UI with growing adoption
- Uno Platform: cross-platform approach built around WinUI-style concepts

Platform choice should follow product needs, team skills, and deployment targets rather than trend alone.

## Best Practices For Modern WPF

- prefer current supported .NET versions
- keep project files SDK-style
- use MVVM and testable service boundaries
- watch packaging and deployment constraints early
- profile real performance issues before redesigning screens
- modernize incrementally instead of rewriting by default

## Resources

- Create a WPF app on .NET: https://learn.microsoft.com/dotnet/desktop/wpf/get-started/create-app-visual-studio
- WPF overview: https://learn.microsoft.com/dotnet/desktop/wpf/overview/
- .NET deployment overview: https://learn.microsoft.com/dotnet/core/deploying/
- MSIX overview: https://learn.microsoft.com/windows/msix/overview
- Windows App SDK: https://learn.microsoft.com/windows/apps/windows-app-sdk/
- WinUI: https://learn.microsoft.com/windows/apps/winui/
- .NET MAUI: https://learn.microsoft.com/dotnet/maui/
