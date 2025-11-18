# Blazouter.Web

[![NuGet](https://img.shields.io/nuget/v/Blazouter.Web.svg)](https://www.nuget.org/packages/Blazouter.Web/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Blazouter.Web.svg)](https://www.nuget.org/packages/Blazouter.Web/)

> **⚠️ DEPRECATED:** This package has been deprecated. For Blazor Web Applications, please use `Blazouter.Server` for the server project and `Blazouter.WebAssembly` for the client project instead. This provides a clearer, more consistent package structure.
>
> **Migration Guide:** See the [Blazor Web Application section in the main README](https://github.com/Taiizor/Blazouter#readme) for updated instructions.

Blazor Web App extensions for Blazouter - the React Router-like routing library for Blazor applications. This package provides necessary components and extensions for the unified Blazor Web App model (.NET 8+) that supports both Server and WebAssembly render modes.

## What is Blazor Web App?

Blazor Web App is the unified hosting model introduced in .NET 8 that combines the best of both worlds:
- **Server-side rendering** with SignalR (InteractiveServer)
- **Client-side rendering** with WebAssembly (InteractiveWebAssembly)
- **Automatic mode selection** (InteractiveAuto) - starts with Server, then downloads WebAssembly in the background

This model allows you to build a single application that can leverage both rendering approaches, optimizing for performance and user experience.

## Features

- ✅ All core Blazouter features
- ✅ InteractiveServer render mode
- ✅ InteractiveAuto render mode (hybrid)
- ✅ InteractiveWebAssembly render mode
- ✅ Full Blazor Web App support (.NET 8+)
- ✅ Enhanced routing for unified web model
- ✅ Static SSR (Server-Side Rendering) support
- ✅ Optimized for both server and client rendering

## Installation

```bash
dotnet add package Blazouter
dotnet add package Blazouter.Web
```

**Note**: This package requires the core `Blazouter` package.

## Quick Start

### 1. Configure Services

```csharp
// Program.cs
using Blazouter.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services for both Server and WebAssembly components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazouter(); // Add Blazouter services

var app = builder.Build();

// Configure HTTP pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
```

### 2. Add Blazouter Support to Routing

**Important**: Add `AddBlazouterSupport()` to enable Blazouter routing in the Web App:

```csharp
using Blazouter.Web.Extensions;

app.MapRazorComponents<App>()
    .AddBlazouterSupport()  // Required for Blazouter
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
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

Choose your preferred render mode:

**Option A: InteractiveServer** (Server-side only)
```razor
<Routes @rendermode="InteractiveServer" />
```

**Option B: InteractiveWebAssembly** (Client-side only)
```razor
<Routes @rendermode="InteractiveWebAssembly" />
```

**Option C: InteractiveAuto** (Hybrid - recommended)
```razor
<Routes @rendermode="InteractiveAuto" />
```

InteractiveAuto provides the best user experience by:
1. Starting with fast Server-side rendering (instant interactivity via SignalR)
2. Downloading WebAssembly in the background
3. Automatically switching to WebAssembly once it's ready (no more SignalR overhead)

### 5. Include CSS

Add to your `App.razor` or layout:

```html
<link rel="stylesheet" href="_content/Blazouter/blazouter[.min].css" />
```

## Render Modes Explained

### InteractiveServer
- Components run on the server
- UI updates sent via SignalR
- No client-side download required
- Best for: Server-heavy operations, quick startup

```razor
<Routes @rendermode="InteractiveServer" />
```

### InteractiveWebAssembly
- Components run in the browser (WebAssembly)
- No server connection after initial load
- Larger initial download
- Best for: Client-heavy operations, offline scenarios

```razor
<Routes @rendermode="InteractiveWebAssembly" />
```

### InteractiveAuto
- Starts with Server rendering
- Downloads WebAssembly in background
- Automatically switches to WebAssembly
- Best for: Most scenarios, optimal UX

```razor
<Routes @rendermode="InteractiveAuto" />
```

## Per-Component Render Modes

You can also specify render modes per component:

```csharp
new RouteConfig
{
    Path = "/fast-page",
    Component = typeof(Pages.FastPage),
    // Will use parent render mode (from Routes component)
}
```

Then in your component:
```razor
@rendermode InteractiveServer
<!-- or -->
@rendermode InteractiveWebAssembly
<!-- or -->
@rendermode InteractiveAuto
```

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
        
        // No layout for this route
        new RouteConfig 
        { 
            Path = "/print", 
            Component = typeof(Pages.Print),
            Layout = null
        }
    };
}
```

Your layout component must inherit from `LayoutComponentBase`:

```razor
@inherits LayoutComponentBase

<div class="app-layout">
    <nav><!-- Navigation --></nav>
    <main>
        @Body  <!-- Page component renders here -->
    </main>
    <footer><!-- Footer --></footer>
</div>
```

## Nested Routes

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

Define nested routes:
```csharp
new RouteConfig
{
    Path = "/products",
    Component = typeof(ProductLayout),
    Children = new List<RouteConfig>
    {
        new RouteConfig 
        { 
            Path = "", 
            Component = typeof(ProductList),
            Exact = true 
        },
        new RouteConfig 
        { 
            Path = ":id", 
            Component = typeof(ProductDetail) 
        }
    }
}
```

**Note**: `<RouterOutlet />` is for nested routing within a component hierarchy, while `Layout` wraps entire routes with a common layout structure.

## Route Guards

Protect routes with authentication/authorization:

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

## Lazy Loading

Reduce initial bundle size with lazy-loaded components:

```csharp
new RouteConfig
{
    Path = "/reports",
    ComponentLoader = async () =>
    {
        // Simulate loading or dynamic import
        await Task.Delay(100);
        return typeof(ReportsPage);
    }
}
```

## Programmatic Navigation

```csharp
@inject RouterNavigationService NavService

<button @onclick="NavigateToUser">Go to User</button>

@code {
    private void NavigateToUser()
    {
        NavService.NavigateTo("/users/123");
    }
}
```

## Access Route Parameters

```csharp
@using Blazouter.Services
@inject RouterStateService RouterState

<h1>User: @_userId</h1>

@code {
    private string? _userId;

    protected override void OnInitialized()
    {
        _userId = RouterState.GetParam("id");
    }
}
```

## Route Transitions

Apply smooth transitions between routes:

```csharp
new RouteConfig
{
    Path = "/",
    Component = typeof(Home),
    Transition = RouteTransition.Fade  // or Slide, Scale, Flip, etc.
}
```

Available transitions: `None`, `Fade`, `Slide`, `Scale`, `Flip`, `Pop`, `SlideUp`, `Rotate`, `Reveal`, `SlideFade`, `Blur`, `Swipe`, `Curtain`, `Spotlight`, `Lift`

## Target Frameworks

- .NET 8.0
- .NET 9.0
- .NET 10.0

**Note**: Blazor Web App model was introduced in .NET 8.

## Complete Example

```csharp
// Program.cs
using Blazouter.Extensions;
using Blazouter.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazouter();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddBlazouterSupport()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
```

```razor
<!-- Routes.razor -->
@using Blazouter.Models
@using Blazouter.Components

<Router Routes="@_routes" DefaultLayout="typeof(Layout.MainLayout)">
    <NotFound><h1>404 - Page Not Found</h1></NotFound>
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
            Path = "/about",
            Component = typeof(Pages.About),
            Transition = RouteTransition.Slide
        }
    };
}
```

```razor
<!-- App.razor -->
<!DOCTYPE html>
<html>
<head>
    <link rel="stylesheet" href="_content/Blazouter/blazouter.min.css" />
</head>
<body>
    <Routes @rendermode="InteractiveAuto" />
</body>
</html>
```

## Documentation

- [Core Features](https://github.com/Taiizor/Blazouter/blob/develop/FEATURES.md)
- [API Reference](https://github.com/Taiizor/Blazouter)
- [Main Documentation](https://github.com/Taiizor/Blazouter)

## Comparison: Web App vs Server vs WebAssembly

| Feature | Blazouter.Server | Blazouter.WebAssembly | Blazouter.Web |
|---------|------------------|----------------------|---------------|
| Server Rendering | ✅ | ❌ | ✅ |
| Client Rendering | ❌ | ✅ | ✅ |
| Auto Mode | ❌ | ❌ | ✅ |
| .NET 6-7 | ✅ | ✅ | ❌ |
| .NET 8+ | ✅ | ✅ | ✅ |
| Best For | Server-only apps | Client-only apps | Hybrid/Modern apps |

## Migration from Blazouter.Server or Blazouter.WebAssembly

If you're using `Blazouter.Server` or `Blazouter.WebAssembly` and want to adopt the new Web App model:

1. Update to .NET 8 or later
2. Replace package reference with `Blazouter.Web`
3. Update namespace from `Blazouter.Server.Extensions` or `Blazouter.WebAssembly.Extensions` to `Blazouter.Web.Extensions`
4. Add both render mode support in Program.cs
5. Choose your render mode in App.razor (InteractiveAuto recommended)

## License

MIT License - see [LICENSE](https://github.com/Taiizor/Blazouter/blob/develop/LICENSE) for details.

## Support

- [GitHub Issues](https://github.com/Taiizor/Blazouter/issues)
- [GitHub Discussions](https://github.com/Taiizor/Blazouter/discussions)

Made with ❤️ for the Blazor community