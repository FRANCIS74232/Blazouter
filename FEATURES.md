# Blazouter Features

Blazouter is a comprehensive routing library for Blazor applications, available as multiple specialized packages to support different hosting models.

## 📦 Package Overview

| Package | Purpose | Platforms |
|---------|---------|-----------|
| **Blazouter** | Core routing library with all base features | All Blazor platforms |
| **Blazouter.Server** | Server-side rendering optimizations | Blazor Server / Blazor Web App (server project) |
| **Blazouter.Hybrid** | Native mobile/desktop support | .NET MAUI (iOS, Android, macOS, Windows) |
| **Blazouter.WebAssembly** | Client-side optimizations | Blazor WebAssembly / Blazor Web App (client project) |

> **Note:** The `Blazouter.Web` package has been deprecated. For Blazor Web Applications, use `Blazouter.Server` for the server project and `Blazouter.WebAssembly` for the client project.

## Implemented Features

### ✅ 1. Nested Routes
- Tested and verified working
- Full hierarchical route structure support
- Example: `/users` (parent) → `/users/:id` (child)
- Parent routes with child routes using `RouterOutlet` component

### ✅ 2. Route Guards (Protected Routes)
- Tested and verified working
- `CanActivateAsync` method to control route access
- Example: `SampleAuthGuard` in the sample application
- `GetRedirectPathAsync` for redirect on denied access
- `IRouteGuard` interface for custom authentication/authorization logic

### ✅ 3. Lazy Loading
- Reduces initial bundle size
- Tested and verified working
- Async component loading with `Task<Type>`
- Example: Lazy page with simulated 1-second delay
- `ComponentLoader` function property on `RouteConfig`

### ✅ 4. Route Transitions/Animations
- Tested and verified working
- Custom animations can be added via CSS
- Configurable per-route via `Transition` property
- Automatic animation application on route change
- Built-in transitions: `None`, `Pop`, `Blur`, `Fade`, `Flip`, `Lift`, `Scale`, `Slide`, `Swipe`, `Reveal`, `Rotate`, `Curtain`, `SlideUp`, `SlideFade`, `Spotlight`

### ✅ 5. Programmatic Navigation
- Query parameter support
- Tested and verified working
- `NavigateTo(path)` method
- Integration with Blazor's `NavigationManager`
- `RouterNavigationService` for imperative navigation

### ✅ 6. Dynamic Route Parameters
- Tested and verified working
- Query string parameter support
- Support for nested route parameters
- Path parameters using `:paramName` syntax
- Easy access via `RouterStateService.GetParam(key)`

### ✅ 7. Active Link State
- `Exact` matching option
- Tested and verified working
- Visual feedback for current route
- `ActiveClass` property for custom styling
- `RouterLink` component with automatic active class

### ✅ 8. Layout System
- Tested and verified working
- Seamless layout switching during navigation
- Support for no-layout routes by setting Layout to null
- `RouteConfig.Layout` property for route-specific layouts
- `Router.DefaultLayout` parameter for application-wide layout
- Layout priority: Route.Layout > Router.DefaultLayout > No Layout
- Automatic layout wrapping using @Body from LayoutComponentBase

## Components

### Router
Main routing component that:
- Applies transitions
- Handles lazy loading
- Executes route guards
- Matches current URL to route configuration
- Renders matched components with optional layouts
- Manages layout wrapping via DefaultLayout parameter

### RouterLink
Navigation link component that:
- Supports exact matching
- Applies active class to current route
- Renders anchor tags with proper href
- Handles click events for SPA navigation

### RouterOutlet
Nested route renderer that:
- Supports transitions
- Maintains route hierarchy
- Displays child route components

## Services

### RouteMatcherService
- Query string parsing
- Nested route matching
- Pattern matching for routes
- Dynamic parameter extraction

### RouterStateService
- Parameter access
- Event notifications
- Current route tracking
- Central state management

### RouterNavigationService
- Query parameter handling
- Programmatic navigation API
- Wrapper around NavigationManager

## Configuration

### RouteConfig Properties
- `RedirectTo`: Redirect path
- `Data`: Custom data dictionary
- `Transition`: Animation name
- `Title`: Route title for metadata
- `Exact`: Exact path matching flag
- `Guards`: List of route guard types
- `Path`: Route pattern (e.g., "/users/:id")
- `Children`: Nested route configurations
- `Component`: Component type to render
- `Layout`: Layout component type (overrides DefaultLayout)
- `ComponentLoader`: Async component loader for lazy loading

## Package-Specific Features

### Blazouter (Core)
- Navigation services
- Lazy loading support
- Route guards interface
- Route configuration and matching
- Multi-framework support (net6.0-net10.0)
- All routing components (Router, RouterLink, RouterOutlet)

### Blazouter.Server
- Server-side route optimization
- Framework references for ASP.NET Core
- Enhanced performance for server scenarios
- `AddBlazouterSupport()` extension for endpoint mapping

### Blazouter.WebAssembly
- SPA navigation support
- Browser platform support
- Client-side route optimization
- Browser-specific enhancements
- Reduced bundle size optimizations

### Blazouter.Hybrid
- Native navigation integration
- MAUI-specific routing features
- Native mobile/desktop integration
- Platform-specific optimizations for iOS, Android, macOS, Windows

## Sample Application

The sample application demonstrates:
1. Protected page with route guard
2. Home page with feature overview
3. About page with library information
4. Lazy loaded page with component loader
5. Users page with nested routing (list and detail views)

All features are interactive and can be tested by navigating through the application.

## Comparison with Traditional Blazor Routing

| Feature | Traditional Blazor | Blazouter |
|---------|-------------------|-----------|
| Transitions | ❌ None | ✅ Built-in |
| Active Links | ⚠️ Manual | ✅ Automatic |
| Lazy Loading | ⚠️ Limited | ✅ Full support |
| Route Guards | ❌ Manual | ✅ Built-in |
| Nested Routes | ❌ Limited | ✅ Full support |
| Parameter Access | ✅ Basic | ✅ Enhanced |
| Programmatic Nav | ✅ Basic | ✅ Enhanced |
| Layout System | ⚠️ Static @layout | ✅ Dynamic per-route |