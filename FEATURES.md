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
- Unlimited nesting depth for complex route hierarchies
- Parent routes with child routes using `RouterOutlet` component

### ✅ 2. Route Guards (Protected Routes)
- Tested and verified working
- `CanActivateAsync` method to control route access
- Example: `SampleAuthGuard` in the sample application
- `GetRedirectPathAsync` for redirect on denied access
- Support for multiple guards per route (executed in order)
- `IRouteGuard` interface for custom authentication/authorization logic
- Guards can be registered via dependency injection or created via Activator

### ✅ 3. Lazy Loading
- Reduces initial bundle size
- Tested and verified working
- Async component loading with `Task<Type>`
- Example: Lazy page with simulated 1-second delay
- `ComponentLoader` function property on `RouteConfig`
- Loading state support with `<Loading>` parameter in Router
- Component caching after first load for performance

### ✅ 4. Route Transitions/Animations
- Tested and verified working
- 14 built-in transition types
- Custom animations can be added via CSS
- Configurable per-route via `Transition` property
- Automatic animation application on route change
- GPU-accelerated animations for smooth performance
- Respects prefers-reduced-motion accessibility preference
- Built-in transitions: `None`, `Pop`, `Blur`, `Fade`, `Flip`, `Lift`, `Scale`, `Slide`, `Swipe`, `Reveal`, `Rotate`, `Curtain`, `SlideUp`, `SlideFade`, `Spotlight`

### ✅ 5. Programmatic Navigation
- Query parameter support
- Browser history integration
- Tested and verified working
- `NavigateTo(path)` method
- Support for relative and absolute navigation
- Integration with Blazor's `NavigationManager`
- `RouterNavigationService` for imperative navigation

### ✅ 6. Dynamic Route Parameters
- Tested and verified working
- Parameter change notifications
- Type-safe parameter extraction
- Query string parameter support
- Support for nested route parameters
- Path parameters using `:paramName` syntax
- Easy access via `RouterStateService.GetParam(key)`

### ✅ 7. Active Link State
- `Exact` matching option
- Tested and verified working
- Visual feedback for current route
- Supports nested route active states
- Automatic update on route changes
- `ActiveClass` property for custom styling
- `RouterLink` component with automatic active class

### ✅ 8. Layout System
- Tested and verified working
- Layout state preservation during navigation
- Seamless layout switching during navigation
- Support for no-layout routes by setting Layout to null
- `RouteConfig.Layout` property for route-specific layouts
- `Router.DefaultLayout` parameter for application-wide layout
- Layout priority: Route.Layout > Router.DefaultLayout > No Layout
- Automatic layout wrapping using @Body from LayoutComponentBase

### ✅ 9. Attribute-Based Routing
- Tested and verified working
- Mix programmatic and attribute-based routes
- Declarative route configuration using attributes
- Full documentation in [ATTRIBUTE_ROUTING.md](https://github.com/Taiizor/Blazouter/blob/develop/ATTRIBUTE_ROUTING.md)
- Automatic route discovery via assembly scanning
- `AddAttributeRoutes()` extension for easy integration
- `FromAttributes()` for pure attribute-based configuration
- 8 attribute types: `[Route]`, `[RouteGuard]`, `[RouteTransition]`, `[RouteLayout]`, `[RouteTitle]`, `[RouteData]`, `[RouteRedirect]`, `[RouteExact]`

### ✅ 10. Error Handling
- Error event notifications
- Comprehensive error handling system
- Retry mechanism for failed operations
- `DefaultRouterErrorHandler` with built-in logging
- `RouterErrorContext` with detailed error information
- `ErrorContent` parameter in Router for custom error UI
- `IRouterErrorHandler` interface for custom error handlers
- Custom error handler registration via `AddBlazouterErrorHandler<T>()`
- Error types: `ComponentLoadFailed`, `GuardRejected`, `NavigationFailed`, `InvalidRoute`

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

### RouteMatcherService (IRouteMatcherService)
- Registered as scoped service
- Route priority and exact match handling
- Dynamic parameter extraction from URL paths
- Query string parsing and parameter extraction
- Pattern matching for routes with wildcard support
- Nested route matching with parent-child relationships

### RouterStateService
- Query parameter retrieval
- Registered as scoped service
- Component lifecycle integration
- Event notifications for route changes
- Current route tracking with RouteMatch
- Parameter access via GetParam(key) method
- Central state management for routing context

### RouterNavigationService
- Browser history integration
- Registered as scoped service
- Support for relative and absolute navigation
- Query parameter handling and URL building
- Programmatic navigation API with NavigateTo(path)
- Wrapper around NavigationManager for enhanced functionality

### RouteAttributeDiscoveryService
- Supports all 8 route attributes
- Reflection-based with trimming warnings
- Converts attributes to RouteConfig objects
- Static service for route discovery at startup
- Discovers components with [Route] attribute
- Assembly scanning for attribute-based routes

### IRouterErrorHandler / DefaultRouterErrorHandler
- Error type categorization
- Retry mechanism support
- Default handler with console logging
- Error context with detailed information
- Error handling interface for custom implementations
- Registered as scoped service via AddBlazouterErrorHandler<T>()

## Configuration

### RouteConfig Properties
- `Exact`: Exact path matching flag - When true, requires exact URL match
- `Data`: Custom data dictionary - Arbitrary metadata passed to components
- `Transition`: Animation type - One of 14 built-in RouteTransition enum values
- `HasExplicitLayout`: Internal flag - Tracks if Layout was explicitly set (even if null)
- `Layout`: Layout component type - Overrides DefaultLayout, set to null for no layout
- `Path`: Route pattern (e.g., "/users/:id") - Supports dynamic parameters with `:` prefix
- `Title`: Route title for metadata - Used for page title, breadcrumbs, or navigation labels
- `Children`: Nested route configurations - List of child RouteConfig for hierarchical routing
- `RedirectTo`: Redirect path - Automatically redirects to specified path when route matches
- `Guards`: List of route guard types - Array of IRouteGuard implementations for access control
- `Component`: Component type to render - Direct component reference for immediate loading
- `ComponentLoader`: Async component loader for lazy loading - Returns `Task<Type>` for on-demand loading

## Package-Specific Features

### Blazouter (Core)
- Lazy loading support with ComponentLoader
- 14 transition types with RouteTransition enum
- Comprehensive XML documentation for IntelliSense
- Layout system support (DefaultLayout, per-route Layout)
- CSS animations included (blazouter.css, blazouter.min.css)
- All routing components (Router, RouterLink, RouterOutlet)
- Multi-framework support (net6.0, net7.0, net8.0, net9.0, net10.0)
- Navigation services (RouterNavigationService, RouterStateService)
- Route guards interface (IRouteGuard, AuthGuard base implementation)
- Route configuration and matching (RouteConfig, RouteMatcherService)
- Attribute-based routing (RouteAttributeDiscoveryService, 8 route attributes)
- Error handling (IRouterErrorHandler, DefaultRouterErrorHandler, RouterErrorContext)

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
| Transitions | ❌ None | ✅ 14 built-in transitions |
| Active Links | ⚠️ Manual | ✅ Automatic with RouterLink |
| Lazy Loading | ⚠️ Limited | ✅ Full support with ComponentLoader |
| Route Guards | ❌ Manual | ✅ Built-in IRouteGuard interface |
| Layout System | ⚠️ Static @layout | ✅ Dynamic per-route with priority |
| Error Handling | ❌ Manual | ✅ Built-in with IRouterErrorHandler |
| Nested Routes | ❌ Limited | ✅ Unlimited nesting depth |
| Attribute Routes | ❌ @page only | ✅ 8 attribute types with full config |
| Parameter Access | ✅ Basic | ✅ Enhanced with RouterStateService |
| Programmatic Nav | ✅ Basic | ✅ Enhanced with RouterNavigationService |