# Blazouter.Server

[![NuGet](https://img.shields.io/nuget/v/Blazouter.Server.svg)](https://www.nuget.org/packages/Blazouter.Server/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Blazouter.Server.svg)](https://www.nuget.org/packages/Blazouter.Server/)

Server-side specific extensions for Blazouter - the React Router-like routing library for Blazor applications. This package provides necessary components and extensions for Blazor Server applications.

## Features

- ✅ Blazor Server integration
- ✅ All core Blazouter features
- ✅ Server-side rendering support
- ✅ Enhanced routing for server mode
- ✅ Optimized for server-side performance

## Installation

```bash
dotnet add package Blazouter
dotnet add package Blazouter.Server
```

**Note**: This package requires the core `Blazouter` package.

## Quick Start

### 1. Configure Services

```csharp
// Program.cs
using Blazouter.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazouter(); // Add Blazouter services

var app = builder.Build();
```

### 2. Add Blazouter Support to Routing

**Important**: Add `AddBlazouterSupport()` to enable Blazouter routing in server mode:

```csharp
using Blazouter.Server.Extensions;

app.MapRazorComponents<App>()
    .AddBlazouterSupport()  // Required for Blazouter
    .AddInteractiveServerRenderMode();
```

### 3. Create Routes Component

Create `Routes.razor` in your Components folder:

```razor
@using Blazouter.Models
@using Blazouter.Components

<Router Routes="@_routes" DefaultLayout="typeof(Layout.MainLayout)">
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
            Component = typeof(Pages.Home),
            Transition = RouteTransition.Fade
        },
        new RouteConfig
        {
            Path = "/users",
            Component = typeof(Pages.UserLayout),
            Children = new List<RouteConfig>
            {
                new RouteConfig 
                { 
                    Path = ":id", 
                    Component = typeof(Pages.UserDetail) 
                }
            }
        }
    };
}
```

### 4. Use in App.razor

```razor
<Routes @rendermode="InteractiveServer" />
```

**Important**: The `@rendermode="InteractiveServer"` attribute is required to enable SignalR connection and interactivity in Blazor Server applications (.NET 8+). Without this attribute, pages will render statically but interactive features (navigation, buttons, etc.) will not work.

### 5. Include CSS

Add to your `App.razor`:

```html
<link rel="stylesheet" href="@Assets["_content/Blazouter/blazouter[.min].css"]" />
```

## Server-Specific Features

### AddBlazouterSupport Extension
The `AddBlazouterSupport()` extension method:
- Registers Blazouter endpoints
- Configures server-side routing
- Enables proper route resolution in server mode

## Layouts

Set a default layout for all routes and override per route as needed:

```csharp
<Router Routes="@_routes" DefaultLayout="typeof(Layout.MainLayout)">
    <NotFound><h1>404</h1></NotFound>
</Router>

@code {
    private List<RouteConfig> _routes = new()
    {
        // Uses DefaultLayout (MainLayout)
        new RouteConfig 
        { 
            Path = "/", 
            Component = typeof(Pages.Home) 
        },
        
        // Override with different layout
        new RouteConfig 
        { 
            Path = "/admin", 
            Component = typeof(Pages.Admin),
            Layout = typeof(Layout.AdminLayout)
        },
        
        // No layout for this route (e.g., for printing)
        new RouteConfig 
        { 
            Path = "/print", 
            Component = typeof(Pages.Print),
            Layout = null
        }
    };
}
```

## Nested Routes in Server Mode

Use `<RouterOutlet />` in parent components to render child routes:

```razor
<!-- Parent Component (UserLayout.razor) -->
@using Blazouter.Components

<div class="user-layout">
    <h1>Users Section</h1>
    <nav>
        <RouterLink Href="/users">All Users</RouterLink>
    </nav>
    <RouterOutlet />
</div>
```

**Note**: `<RouterOutlet />` is for nested routing within a component hierarchy, while `Layout` (via `DefaultLayout` or `RouteConfig.Layout`) wraps entire routes with a common layout structure like headers, footers, and navigation.

## Route Guards

```csharp
new RouteConfig
{
    Path = "/admin",
    Component = typeof(AdminPanel),
    Guards = new List<Type> { typeof(AuthGuard) }
}

public class AuthGuard : IRouteGuard
{
    private readonly AuthenticationStateProvider _authProvider;
    
    public AuthGuard(AuthenticationStateProvider authProvider)
    {
        _authProvider = authProvider;
    }

    public async Task<bool> CanActivateAsync(RouteMatch match)
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        return authState.User.Identity?.IsAuthenticated ?? false;
    }

    public Task<string?> GetRedirectPathAsync(RouteMatch match)
    {
        return Task.FromResult<string?>("/login");
    }
}
```

## Programmatic Navigation

```csharp
@inject RouterNavigationService NavService

private void GoToPage()
{
    NavService.NavigateTo("/users/123");
}
```

## Performance Tips for Server Mode

1. **Enable lazy loading** for large components
2. **Use route guards** to protect expensive operations
3. **Configure transitions** appropriately for server latency
4. **Use RouterOutlet** for nested routes to improve rendering performance

## Target Frameworks

- .NET 8.0
- .NET 9.0
- .NET 10.0

## Example Application

See the [sample application](https://github.com/Taiizor/Blazouter/tree/develop/samples) for a complete working example.

## Documentation

- [Core Features](https://github.com/Taiizor/Blazouter/blob/develop/FEATURES.md)
- [API Reference](https://github.com/Taiizor/Blazouter)
- [Main Documentation](https://github.com/Taiizor/Blazouter)

## Migration from Standard Blazor Routing

Blazouter maintains compatibility while adding powerful features:

| Blazor Router | Blazouter |
|---------------|-----------|
| No guards | Built-in `IRouteGuard` |
| No transitions | Built-in transitions |
| Limited nesting | Full nested route support |
| `@page "/path"` | `RouteConfig` with `Path = "/path"` |
| `NavigationManager` | `RouterNavigationService` (enhanced) |

## License

MIT License - see [LICENSE](https://github.com/Taiizor/Blazouter/blob/develop/LICENSE) for details.

## Support

- [GitHub Issues](https://github.com/Taiizor/Blazouter/issues)
- [GitHub Discussions](https://github.com/Taiizor/Blazouter/discussions)

Made with ❤️ for the Blazor community