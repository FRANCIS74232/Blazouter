# Changelog

All notable changes to Blazouter will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.0.10] - 2025-11-19

### Added
- **Enhanced Route Transitions**: Expanded from 4 to 14 built-in transition types
  - Added: `None`, `Pop`, `Blur`, `Reveal`, `Rotate`, `Curtain`, `SlideFade`, `Spotlight`, `Swipe`, `Lift`
  - All transitions are GPU-accelerated for smooth performance
  - Respects `prefers-reduced-motion` accessibility preference
  - Comprehensive XML documentation for each transition type with use cases and best practices
- **Attribute-Based Routing System**: Complete declarative routing configuration
  - 8 attribute types: `[Route]`, `[RouteGuard]`, `[RouteTransition]`, `[RouteLayout]`, `[RouteTitle]`, `[RouteData]`, `[RouteRedirect]`, `[RouteExact]`
  - `RouteAttributeDiscoveryService` for automatic route discovery via assembly scanning
  - `AddAttributeRoutes()` extension method for mixing programmatic and attribute-based routes
  - `FromAttributes()` static method for pure attribute-based configuration
  - Full support for nested routes and complex configurations via attributes
  - Detailed documentation in ATTRIBUTE_ROUTING.md
- **Comprehensive Error Handling System**
  - `IRouterErrorHandler` interface for custom error handling implementations
  - `DefaultRouterErrorHandler` with built-in console logging
  - `RouterErrorContext` providing detailed error information and context
  - `RouterErrorType` enum with error categories: `ComponentLoadFailed`, `GuardRejected`, `NavigationFailed`, `InvalidRoute`
  - `ErrorContent` RenderFragment parameter in Router component for custom error UI
  - Retry mechanism support for failed operations
  - Error event notifications throughout routing lifecycle
  - `AddBlazouterErrorHandler<T>()` extension method for registering custom error handlers
- **Enhanced Layout System**
  - `DefaultLayout` parameter on Router component for application-wide layouts
  - Per-route layout override via `RouteConfig.Layout` property
  - Support for no-layout routes by explicitly setting `Layout = null`
  - Layout priority system: Route.Layout > Router.DefaultLayout > No Layout
  - `HasExplicitLayout` internal flag for proper layout resolution
  - Seamless layout switching during navigation with state preservation
- **Additional Components and Services**
  - `BlankLayout` component for routes requiring no layout structure
  - Enhanced `RouteMatcherService` with improved pattern matching
  - `RouterStateService` with parameter change notifications
  - Loading state support with `<Loading>` RenderFragment parameter in Router
  - Component caching for lazy-loaded routes after first load

### Changed
- **Documentation Overhaul**
  - README.md: Updated to reflect all 14 transitions, added error handling section, enhanced project structure
  - FEATURES.md: Expanded from 8 to 10 features with detailed implementation descriptions
  - All sample applications updated to showcase 14 transitions instead of 4
  - Added comprehensive comparison table showing Blazouter advantages over traditional Blazor routing
  - Project Structure section now includes all folders: Attributes/, Extensions/, Resources/, Components/Layouts/
- **Enhanced RouteConfig Model**
  - Added `HasExplicitLayout` property for proper layout handling
  - Improved XML documentation for all properties
  - Better support for complex nested route scenarios
- **Sample Applications**
  - All 4 sample apps (WebAssembly, Server, Hybrid, Web) updated with transition demos
  - Added demo pages for all 14 transition types with descriptions and code examples
  - Enhanced Home.razor with accurate feature showcase
  - Added Transitions.razor pages demonstrating all transition types

### Fixed
- Layout handling when explicitly set to null vs. not set
- Documentation inconsistencies between README.md and FEATURES.md
- Sample applications showing outdated feature counts

## [1.0.9] - 2025-11-18

### Changed
- Documentation links updated to reflect new repository location

## [1.0.6-1.0.8] - 2025-11-16 to 2025-11-17

### Note
- Internal version increments for package stability and distribution
- Minor bug fixes and improvements

### Deprecated
- **Blazouter.Web package**: The `Blazouter.Web` package has been deprecated in favor of using `Blazouter.Server` and `Blazouter.WebAssembly` for Blazor Web Applications. This provides a clearer, more consistent package structure where:
  - Server project uses: `Blazouter.Server`
  - Client project uses: `Blazouter.WebAssembly`

### Changed
- **Documentation updated**: All documentation (README.md, FEATURES.md, package READMEs) updated to reflect the deprecated status of `Blazouter.Web` and provide guidance on using `Blazouter.Server` + `Blazouter.WebAssembly` for Blazor Web Applications
- **Sample application**: Blazor Web sample now uses `Blazouter.Server` for server project and `Blazouter.WebAssembly` for client project

## [1.0.5] - 2025-11-16

### Added
- **Multiple NuGet Packages**: Split into 4 specialized packages for better modularity
  - `Blazouter`: Core library (required for all hosting models)
  - `Blazouter.Hybrid`: MAUI/Hybrid support for iOS, Android, macOS, and Windows
  - `Blazouter.Server`: Server-side Blazor extensions with `AddBlazouterSupport()`
  - `Blazouter.WebAssembly`: WebAssembly-specific optimizations
- Professional NuGet package metadata for all packages
  - Package-specific README files
  - Optimized package icons
  - Comprehensive descriptions and tags
  - SourceLink support for debugging
  - Symbol packages (.snupkg) for all packages
- Multi-framework targeting:
  - Core library: net6.0, net7.0, net8.0, net9.0, net10.0
  - Server: net8.0, net9.0, net10.0
  - Hybrid: net9.0, net10.0 (platform-specific)
  - WebAssembly: net6.0, net7.0, net8.0, net9.0, net10.0

### Changed
- Project structure reorganized into multiple packages
- Documentation updated to reflect 4-package architecture
- README files now specific to each package's features and use cases

## [1.0.2] - 2025-11-15

### Added
- Type-safe `RouteTransition` enum for better IntelliSense and compile-time safety
- Enhanced sample application with comprehensive demos:
  - Professional home page with feature showcase
  - Navigation demo page with programmatic navigation examples
  - Transitions demo page showcasing all 4 transition types
- Loading state for lazy-loaded routes to prevent 404 flash
- XML documentation comments for better IDE support

### Fixed
- Component re-rendering issue when navigating between routes with same component but different parameters
- Lazy-loaded routes showing 404 page briefly before component loads
- Visual Studio IntelliSense warnings for multiple RenderFragment parameters

### Changed
- Improved sample application UI/UX with modern gradient design
- Enhanced documentation with more examples and use cases

## [1.0.1] - 2025-11-14

### Added
- Initial release of Blazouter
- Core routing components (Router, RouterLink, RouterOutlet)
- Nested routes support
- Route guards for authentication/authorization
- Lazy loading with ComponentLoader
- Route transitions (fade, slide, slide-up, scale)
- Programmatic navigation service
- Dynamic route parameters
- Query string support
- Active link state management

### Documentation
- Comprehensive README with examples
- FEATURES.md detailing all capabilities
- Sample application demonstrating key features

[1.0.10]: https://github.com/Taiizor/Blazouter/compare/v1.0.9...v1.0.10
[1.0.9]: https://github.com/Taiizor/Blazouter/compare/v1.0.5...v1.0.9
[1.0.6-1.0.8]: https://github.com/Taiizor/Blazouter/compare/v1.0.5...v1.0.9
[1.0.5]: https://github.com/Taiizor/Blazouter/compare/v1.0.2...v1.0.5
[1.0.2]: https://github.com/Taiizor/Blazouter/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Taiizor/Blazouter/releases/tag/v1.0.1