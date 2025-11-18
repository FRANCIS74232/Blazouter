# Changelog

All notable changes to Blazouter will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

[1.0.5]: https://github.com/Taiizor/Blazouter/compare/v1.0.2...v1.0.5
[1.0.2]: https://github.com/Taiizor/Blazouter/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Taiizor/Blazouter/releases/tag/v1.0.1