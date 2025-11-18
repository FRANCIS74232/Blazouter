# Blazouter - Core Library

[![NuGet](https://img.shields.io/nuget/v/Blazouter.svg)](https://www.nuget.org/packages/Blazouter/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Blazouter.svg)](https://www.nuget.org/packages/Blazouter/)

A powerful React Router-like routing library for Blazor applications. This is the core library that provides the foundational routing capabilities for all Blazor hosting models.

## Features

- ✅ **Type-safe** - Full IntelliSense support
- ✅ **True nested routing** - Full hierarchical route structures
- ✅ **Beautiful transitions** - Smooth animations between routes
- ✅ **Programmatic navigation** - Navigate imperatively with ease
- ✅ **Lazy loading** - Load components on-demand for better performance
- ✅ **Built-in route guards** - Protect routes with authentication/authorization
- ✅ **Dynamic route parameters** - Easy access to route and query parameters
- ✅ **Flexible layout system** - Default and per-route layouts with @Body support

## Installation

```bash
dotnet add package Blazouter
```

For hosting-specific implementations, also install:
- **Blazor Server**: `Blazouter.Server`
- **Blazor Hybrid/MAUI**: `Blazouter.Hybrid`
- **Blazor WebAssembly**: `Blazouter.WebAssembly`

## Quick Start

### Blazor WebAssembly

```csharp
// Program.cs
using Blazouter.Extensions;

builder.Services.AddBlazouter();
```

```razor
<!-- App.razor -->
@using Blazouter.Models
@using Blazouter.Components

<Router Routes="@_routes" DefaultLayout="typeof(MainLayout)">
    <NotFound>
        <h1>404 - Page Not Found</h1>
    </NotFound>
</Router>

@code {
    private List<RouteConfig> _routes = new()
    {
        new RouteConfig
        {
            Path = "/",
            Component = typeof(Home),
            Transition = RouteTransition.Fade
        }
    };
}
```

### Blazor Server

```csharp
// Program.cs
using Blazouter.Extensions;
using Blazouter.Server.Extensions;

builder.Services.AddBlazouter();

app.MapRazorComponents<App>()
    .AddBlazouterSupport()
    .AddInteractiveServerRenderMode();
```

### Blazor Hybrid (MAUI)

```csharp
// MauiProgram.cs
using Blazouter.Hybrid.Extensions;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();
        
        // Add Blazouter support
        builder.AddBlazouterSupport();

        return builder.Build();
    }
}
```

```razor
<!-- Main.razor or your root component -->
@using Blazouter.Models
@using Blazouter.Components

<Router Routes="@_routes">
    <NotFound>
        <h1>404 - Page Not Found</h1>
    </NotFound>
</Router>

@code {
    private List<RouteConfig> _routes = new()
    {
        new RouteConfig
        {
            Path = "/",
            Component = typeof(Home),
            Transition = RouteTransition.Fade
        }
    };
}
```

## Key Components

### Router Component
The main router component that handles route matching and rendering.

### RouterOutlet Component
Used in parent components to render child routes in nested routing scenarios.

### RouterLink Component
Navigation links with automatic active state detection.

```razor
<RouterLink Href="/" Exact="true" ActiveClass="active">Home</RouterLink>
<RouterLink Href="/about" ActiveClass="active">About</RouterLink>
```

## Layouts

Set a default layout for all routes and override per route:

```csharp
<Router Routes="@_routes" DefaultLayout="typeof(MainLayout)">
    <NotFound><h1>404</h1></NotFound>
</Router>

@code {
    private List<RouteConfig> _routes = new()
    {
        // Uses DefaultLayout (MainLayout)
        new RouteConfig { Path = "/", Component = typeof(Home) },
        
        // Override with different layout
        new RouteConfig 
        { 
            Path = "/admin", 
            Component = typeof(Admin),
            Layout = typeof(AdminLayout)
        },
        
        // No layout for this route
        new RouteConfig 
        { 
            Path = "/print", 
            Component = typeof(Print),
            Layout = null
        }
    };
}
```

## Route Configuration

```csharp
new RouteConfig
{
    Path = "/users",
    Component = typeof(UserLayout),
    Title = "Users",
    Layout = typeof(MainLayout),  // Optional: override default layout
    Transition = RouteTransition.Slide,
    Guards = new List<Type> { typeof(AuthGuard) },
    Children = new List<RouteConfig>
    {
        new RouteConfig 
        { 
            Path = ":id", 
            Component = typeof(UserDetail) 
        }
    }
}
```

## Route Guards

Create custom guards by implementing `IRouteGuard`:

```csharp
using Blazouter.Guards;
using Blazouter.Models;

public class AuthGuard : IRouteGuard
{
    public async Task<bool> CanActivateAsync(RouteMatch match)
    {
        return await IsAuthenticated();
    }

    public Task<string?> GetRedirectPathAsync(RouteMatch match)
    {
        return Task.FromResult<string?>("/login");
    }
}
```

## Lazy Loading

```csharp
new RouteConfig
{
    Path = "/reports",
    ComponentLoader = async () =>
    {
        await Task.Delay(100); // Simulated delay
        return typeof(ReportsPage);
    }
}
```

## Programmatic Navigation

```csharp
@inject RouterNavigationService NavService

private void NavigateToUser()
{
    NavService.NavigateTo("/users/123");
}
```

## Route Parameters

```csharp
@inject RouterStateService RouterState

protected override void OnInitialized()
{
    var userId = RouterState.GetParam("id");
}
```

## Transitions

Built-in transitions: `None`, `Pop`, `Blur`, `Fade`, `Flip`, `Lift`, `Scale`, `Slide`, `Swipe`, `Reveal`, `Rotate`, `Curtain`, `SlideUp`, `SlideFade`, `Spotlight`

```csharp
new RouteConfig
{
    Path = "/",
    Component = typeof(Home),
    Transition = RouteTransition.Fade
}
```

## Multi-Platform Support

- Blazor Server
- Blazor WebAssembly
- Blazor Hybrid (MAUI)
- .NET 6.0, 7.0, 8.0, 9.0, 10.0

## Documentation

- [Features](https://github.com/Taiizor/Blazouter/blob/develop/FEATURES.md)
- [Sample Application](https://github.com/Taiizor/Blazouter/tree/develop/samples)
- [Full Documentation](https://github.com/Taiizor/Blazouter)

## License

MIT License - see [LICENSE](https://github.com/Taiizor/Blazouter/blob/develop/LICENSE) for details.

## Support

- [GitHub Issues](https://github.com/Taiizor/Blazouter/issues)
- [GitHub Discussions](https://github.com/Taiizor/Blazouter/discussions)

Made with ❤️ for the Blazor community