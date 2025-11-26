# TypeScript Integration for JavaScript Interop

Blazouter provides comprehensive TypeScript-based JavaScript interop for enhanced browser integration. The TypeScript definitions ensure type safety when calling JavaScript functions from C#.

## Overview

The TypeScript integration provides 5 comprehensive interop services:

1. **NavigationInterop** - Browser history and URL management
5. **ClipboardInterop** - Clipboard operations with permission handling
2. **DocumentInterop** - Document manipulation, meta tags, and scrolling
3. **StorageInterop** - LocalStorage and SessionStorage with JSON serialization
4. **ViewportInterop** - Viewport dimensions, device detection, and fullscreen API

All services are fully type-safe with TypeScript definitions (`.d.ts` files) for IntelliSense support.

## Installation

### 1. Add Blazouter Services

```csharp
// In Program.cs
builder.Services.AddBlazouter();
builder.Services.AddBlazouterInterop();  // Enable JavaScript interop services
```

### 2. Required: Import JavaScript Module

**IMPORTANT:** You must import the Blazouter JavaScript module for the interop services to work.

**For Blazor WebAssembly and Hybrid:**

Add to your `wwwroot/index.html` in the `<head>` section or before `</body>`:

```html
<script type="module" src="_content/Blazouter/js/index.js"></script>
```

**For Blazor Server:**

Add to your `Components/App.razor` or `Pages/_Host.cshtml` before the closing `</body>` tag:

```html
<script type="module" src="_content/Blazouter/js/index.js"></script>
```

This script loads the TypeScript-compiled JavaScript modules that expose the following global objects:
- `window.blazouterStorage`
- `window.blazouterDocument`
- `window.blazouterViewport`
- `window.blazouterClipboard`
- `window.blazouterNavigation`

**Without this import, you will see `undefined` errors in the browser console and the interop services will not function.**

## Navigation API

### Overview

The `NavigationInterop` service (namespace: `Blazouter.Interops`) provides type-safe access to the browser's History API and URL management.

### Features

- **History Navigation**: back, forward, go to specific position
- **History State**: push, replace, and get state
- **URL Information**: current URL, pathname, hash
- **Query Parameters**: get query string, individual parameters, or all parameters
- **Page Reload**: programmatic page refresh

### Usage

```csharp
@using Blazouter.Interops
@inject NavigationInterop Navigation

<button @onclick="HandleBackButton">Go Back</button>
<button @onclick="ShowUrlInfo">Show URL Info</button>

@code {
    private async Task HandleBackButton()
    {
        if (await Navigation.CanGoBackAsync())
        {
            await Navigation.GoBackAsync();
        }
    }

    private async Task ShowUrlInfo()
    {
        string url = await Navigation.GetCurrentUrlAsync();
        string pathname = await Navigation.GetPathnameAsync();
        string hash = await Navigation.GetHashAsync();
        
        Console.WriteLine($"URL: {url}");
        Console.WriteLine($"Path: {pathname}");
        Console.WriteLine($"Hash: {hash}");
    }
}
```

### RouterNavigationService Integration

The `RouterNavigationService` provides convenient async methods that integrate with the Navigation API:

```csharp
@inject RouterNavigationService NavService

<button @onclick="GoBack">← Back</button>
<button @onclick="GoForward">Forward →</button>

@code {
    private async Task GoBack()
    {
        await NavService.GoBackAsync();  // Uses NavigationInterop internally
    }

    private async Task GoForward()
    {
        await NavService.GoForwardAsync();  // Uses NavigationInterop internally
    }
}
```

### API Reference

#### History Navigation

**GoBackAsync()**
Navigates back in browser history.

```csharp
await Navigation.GoBackAsync();
```

**GoForwardAsync()**
Navigates forward in browser history.

```csharp
await Navigation.GoForwardAsync();
```

**GoAsync(delta)**
Navigates to a specific position in history relative to current page.

```csharp
await Navigation.GoAsync(-2);  // Go back 2 pages
await Navigation.GoAsync(1);   // Go forward 1 page
```

**CanGoBackAsync()**
Returns `true` if there is history to navigate back to.

```csharp
bool canGoBack = await Navigation.CanGoBackAsync();
```

**GetHistoryLengthAsync()**
Gets the number of entries in the history stack.

```csharp
int length = await Navigation.GetHistoryLengthAsync();
```

#### History State Management

**PushStateAsync(state, title, url)**
Pushes a new state to history without navigation.

```csharp
await Navigation.PushStateAsync(
    new { userId = 123 }, 
    "User Profile", 
    "/users/123"
);
```

**ReplaceStateAsync(state, title, url)**
Replaces the current state in history.

```csharp
await Navigation.ReplaceStateAsync(
    new { userId = 456 }, 
    "Different User", 
    "/users/456"
);
```

**GetStateAsync()**
Gets the current history state.

```csharp
var state = await Navigation.GetStateAsync();
```

#### URL Information

**GetCurrentUrlAsync()**
Gets the complete current URL.

```csharp
string url = await Navigation.GetCurrentUrlAsync();
// Returns: "https://example.com/products?category=shoes#reviews"
```

**GetPathnameAsync()**
Gets the pathname portion of the URL.

```csharp
string pathname = await Navigation.GetPathnameAsync();
// Returns: "/products"
```

**GetHashAsync()**
Gets the hash (fragment) from the URL.

```csharp
string hash = await Navigation.GetHashAsync();
// Returns: "#reviews" or "" if no hash
```

**SetHashAsync(hash)**
Sets the hash without full page reload.

```csharp
await Navigation.SetHashAsync("#section-2");
```

#### Query Parameters

**GetQueryStringAsync()**
Gets the complete query string (without the `?`).

```csharp
string queryString = await Navigation.GetQueryStringAsync();
// Returns: "category=shoes&size=10"
```

**GetQueryParamAsync(name)**
Gets a specific query parameter value.

```csharp
string? category = await Navigation.GetQueryParamAsync("category");
// Returns: "shoes" or null if not present
```

**GetAllQueryParamsAsync()**
Gets all query parameters as a dictionary.

```csharp
Dictionary<string, string> params = await Navigation.GetAllQueryParamsAsync();
// Returns: { "category": "shoes", "size": "10" }
```

#### Page Reload

**ReloadAsync()**
Reloads the current page.

```csharp
await Navigation.ReloadAsync();
```

## Document API

### Overview

The `DocumentInterop` service provides comprehensive document manipulation capabilities including title management, meta tags, scrolling, CSS class manipulation, and element visibility detection.

### Features

- **Page Title**: get and set document title
- **Meta Tags**: manage description, keywords, and custom meta tags
- **Open Graph Tags**: for social media sharing
- **Canonical URLs**: for SEO
- **Scroll Control**: scroll position tracking and element scrolling
- **CSS Classes**: add, remove, toggle classes on elements
- **Element Visibility**: check if element is in viewport
- **Focus Management**: programmatically focus elements
- **Document State**: check document ready state

### Usage

#### Dynamic Page Title and SEO

```csharp
@using Blazouter.Interops
@inject DocumentInterop Document

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Set page title
            await Document.SetTitleAsync("Products - My Store");

            // Set meta tags for SEO
            await Document.SetMetaTagAsync("description", "Browse our product catalog");
            await Document.SetMetaTagAsync("keywords", "products, shopping, store");

            // Set Open Graph tags for social media
            await Document.SetOpenGraphTagAsync("og:title", "Products - My Store");
            await Document.SetOpenGraphTagAsync("og:description", "Browse our catalog");
            await Document.SetOpenGraphTagAsync("og:image", "https://example.com/og-image.jpg");
            await Document.SetOpenGraphTagAsync("og:url", "https://example.com/products");

            // Set canonical URL
            await Document.SetCanonicalUrlAsync("https://example.com/products");
        }
    }
}
```

#### Scroll Management

```csharp
@using Blazouter.Interops
@inject DocumentInterop Document

<button @onclick="ScrollToTop">↑ Top</button>
<button @onclick="ScrollToSection">Go to Section 2</button>
<button @onclick="GetScrollPos">Get Scroll Position</button>

@code {
    private async Task ScrollToTop()
    {
        await Document.ScrollToTopAsync(smooth: true);
    }

    private async Task ScrollToSection()
    {
        bool success = await Document.ScrollToElementAsync("#section-2", smooth: true);
        if (!success)
        {
            Console.WriteLine("Section not found");
        }
    }

    private async Task GetScrollPos()
    {
        var pos = await Document.GetScrollPositionAsync();
        Console.WriteLine($"Scroll: X={pos.X}, Y={pos.Y}");
    }

    private async Task SetScrollPos()
    {
        await Document.SetScrollPositionAsync(0, 500, smooth: true);
    }
}
```

#### CSS Class Manipulation

```csharp
@using Blazouter.Interops
@inject DocumentInterop Document

<button @onclick="HighlightElement">Highlight</button>

@code {
    private async Task HighlightElement()
    {
        await Document.AddClassAsync("#my-element", "highlighted");
        
        // Toggle class
        await Document.ToggleClassAsync("#my-element", "active");
        
        // Remove class
        await Document.RemoveClassAsync("#my-element", "old-class");
    }
}
```

#### Element Visibility Detection

```csharp
@using Blazouter.Interops
@inject DocumentInterop Document

@code {
    private async Task CheckVisibility()
    {
        bool isVisible = await Document.IsElementVisibleAsync("#my-section");
        
        if (!isVisible)
        {
            await Document.ScrollToElementAsync("#my-section");
        }
    }
}
```

### API Reference

#### Title Management

**SetTitleAsync(title)**
Sets the document title displayed in the browser tab.

```csharp
await Document.SetTitleAsync("Home - My App");
```

**GetTitleAsync()**
Gets the current document title.

```csharp
string title = await Document.GetTitleAsync();
```

#### Meta Tags

**SetMetaTagAsync(name, content)**
Sets or updates a meta tag. Creates the tag if it doesn't exist.

```csharp
await Document.SetMetaTagAsync("description", "Page description");
await Document.SetMetaTagAsync("keywords", "keyword1, keyword2");
```

**GetMetaTagAsync(name)**
Gets the content of a meta tag.

```csharp
string? description = await Document.GetMetaTagAsync("description");
```

**RemoveMetaTagAsync(name)**
Removes a meta tag from the document.

```csharp
await Document.RemoveMetaTagAsync("keywords");
```

#### Open Graph Tags

**SetOpenGraphTagAsync(property, content)**
Sets or updates an Open Graph meta tag for social media sharing.

```csharp
await Document.SetOpenGraphTagAsync("og:title", "Article Title");
await Document.SetOpenGraphTagAsync("og:description", "Article description");
await Document.SetOpenGraphTagAsync("og:image", "https://example.com/image.jpg");
await Document.SetOpenGraphTagAsync("og:type", "article");
await Document.SetOpenGraphTagAsync("og:url", "https://example.com/article");
```

#### Canonical URL

**SetCanonicalUrlAsync(url)**
Sets the canonical URL for the page to avoid duplicate content issues in SEO.

```csharp
await Document.SetCanonicalUrlAsync("https://example.com/products/123");
```

#### Scroll Control

**ScrollToTopAsync(smooth)**
Scrolls to the top of the page.

```csharp
await Document.ScrollToTopAsync(smooth: true);  // Smooth scroll
await Document.ScrollToTopAsync(smooth: false); // Instant scroll
```

**ScrollToElementAsync(selector, smooth)**
Scrolls to an element by CSS selector. Returns `true` if successful.

```csharp
bool success = await Document.ScrollToElementAsync("#section-2", smooth: true);
```

**GetScrollPositionAsync()**
Gets the current scroll position. Returns a `ScrollPosition` record with `X` and `Y` properties.

```csharp
DocumentInterop.ScrollPosition pos = await Document.GetScrollPositionAsync();
Console.WriteLine($"X: {pos.X}, Y: {pos.Y}");
```

**SetScrollPositionAsync(x, y, smooth)**
Sets the scroll position.

```csharp
await Document.SetScrollPositionAsync(0, 500, smooth: true);
```

#### CSS Class Manipulation

**AddClassAsync(selector, className)**
Adds a CSS class to an element.

```csharp
await Document.AddClassAsync("#my-element", "active");
```

**RemoveClassAsync(selector, className)**
Removes a CSS class from an element.

```csharp
await Document.RemoveClassAsync("#my-element", "active");
```

**ToggleClassAsync(selector, className)**
Toggles a CSS class on an element.

```csharp
await Document.ToggleClassAsync("#my-element", "hidden");
```

#### Element Visibility

**IsElementVisibleAsync(selector)**
Checks if an element is visible in the viewport.

```csharp
bool isVisible = await Document.IsElementVisibleAsync("#my-section");
```

#### Focus Management

**FocusElementAsync(selector)**
Focuses an element by CSS selector. Returns `true` if successful.

```csharp
bool focused = await Document.FocusElementAsync("#search-input");
```

#### Document State

**GetReadyStateAsync()**
Gets the document ready state ("loading", "interactive", or "complete").

```csharp
string readyState = await Document.GetReadyStateAsync();
```

**IsDocumentReadyAsync()**
Returns `true` if the document is fully loaded (ready state is "complete").

```csharp
bool isReady = await Document.IsDocumentReadyAsync();
```

## Storage API

### Overview

The `StorageInterop` service provides type-safe access to browser localStorage and sessionStorage with automatic JSON serialization for complex objects.

### Features

- **LocalStorage**: persistent storage across browser sessions
- **SessionStorage**: storage cleared when tab is closed
- **Generic Types**: full support for any serializable type
- **JSON Serialization**: automatic serialization/deserialization
- **Key Management**: list all keys, check existence
- **Clear Operations**: remove specific items or clear all

### Usage

#### LocalStorage Operations

```csharp
@using Blazouter.Interops
@inject StorageInterop Storage

@code {
    // Save complex object
    private async Task SaveUserPreferences()
    {
        var prefs = new UserPreferences 
        { 
            Theme = "dark", 
            Language = "en",
            Notifications = true
        };
        await Storage.SetLocalStorageAsync("userPrefs", prefs);
    }

    // Load object
    private async Task LoadUserPreferences()
    {
        var prefs = await Storage.GetLocalStorageAsync<UserPreferences>("userPrefs");
        if (prefs != null)
        {
            // Apply preferences
            Console.WriteLine($"Theme: {prefs.Theme}");
        }
    }

    // Save simple value
    private async Task SaveLastVisit()
    {
        await Storage.SetLocalStorageAsync("lastVisit", DateTime.Now);
    }

    // Remove item
    private async Task RemovePreferences()
    {
        await Storage.RemoveLocalStorageAsync("userPrefs");
    }

    // Check if key exists
    private async Task CheckPreferences()
    {
        bool exists = await Storage.HasLocalStorageAsync("userPrefs");
        Console.WriteLine($"Preferences exist: {exists}");
    }

    // List all keys
    private async Task ListAllKeys()
    {
        string[] keys = await Storage.GetLocalStorageKeysAsync();
        Console.WriteLine($"Keys: {string.Join(", ", keys)}");
    }

    // Clear all localStorage
    private async Task ClearAll()
    {
        await Storage.ClearLocalStorageAsync();
    }
}
```

#### SessionStorage Operations

SessionStorage has the exact same API as LocalStorage, but data is cleared when the browser tab is closed:

```csharp
@using Blazouter.Interops
@inject StorageInterop Storage

@code {
    // Save to session storage (cleared when tab closes)
    private async Task SaveTempData()
    {
        await Storage.SetSessionStorageAsync("tempData", new { value = "temporary" });
    }

    // Load from session storage
    private async Task LoadTempData()
    {
        var data = await Storage.GetSessionStorageAsync<dynamic>("tempData");
    }

    // Other operations work the same way
    private async Task ManageSessionStorage()
    {
        await Storage.RemoveSessionStorageAsync("tempData");
        await Storage.ClearSessionStorageAsync();
        string[] keys = await Storage.GetSessionStorageKeysAsync();
        bool has = await Storage.HasSessionStorageAsync("tempData");
    }
}
```

### API Reference

#### LocalStorage Methods

**SetLocalStorageAsync<T>(key, value)**
Saves a value to localStorage with JSON serialization.

```csharp
await Storage.SetLocalStorageAsync("key", myObject);
await Storage.SetLocalStorageAsync("count", 42);
```

**GetLocalStorageAsync<T>(key)**
Retrieves a value from localStorage with JSON deserialization. Returns `null` if not found.

```csharp
MyObject? obj = await Storage.GetLocalStorageAsync<MyObject>("key");
int? count = await Storage.GetLocalStorageAsync<int>("count");
```

**RemoveLocalStorageAsync(key)**
Removes an item from localStorage.

```csharp
await Storage.RemoveLocalStorageAsync("key");
```

**ClearLocalStorageAsync()**
Clears all items from localStorage.

```csharp
await Storage.ClearLocalStorageAsync();
```

**GetLocalStorageKeysAsync()**
Gets all keys from localStorage.

```csharp
string[] keys = await Storage.GetLocalStorageKeysAsync();
```

**HasLocalStorageAsync(key)**
Checks if a key exists in localStorage.

```csharp
bool exists = await Storage.HasLocalStorageAsync("key");
```

#### SessionStorage Methods

All SessionStorage methods have identical signatures with `SessionStorage` instead of `LocalStorage`:

- `SetSessionStorageAsync<T>(key, value)`
- `GetSessionStorageAsync<T>(key)`
- `RemoveSessionStorageAsync(key)`
- `ClearSessionStorageAsync()`
- `GetSessionStorageKeysAsync()`
- `HasSessionStorageAsync(key)`

## Viewport API

### Overview

The `ViewportInterop` service provides information about the viewport, screen, device characteristics, and fullscreen capabilities.

### Features

- **Viewport Dimensions**: width, height, and size
- **Screen Information**: screen dimensions
- **Device Pixel Ratio**: for high-DPI displays
- **Orientation Detection**: portrait or landscape
- **Device Type Detection**: mobile, tablet, or desktop
- **Fullscreen API**: enter/exit fullscreen mode

### Usage

#### Responsive Design

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport

@code {
    private string _deviceType = "";
    private bool _isMobile = false;
    private int _viewportWidth = 0;

    protected override async Task OnInitializedAsync()
    {
        _deviceType = await Viewport.GetDeviceTypeAsync();
        _isMobile = await Viewport.IsMobileAsync();
        _viewportWidth = await Viewport.GetViewportWidthAsync();

        if (_isMobile)
        {
            // Show mobile-optimized layout
        }
        else if (_viewportWidth < 768)
        {
            // Tablet layout
        }
    }
}
```

#### Viewport Dimensions

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport

@code {
    private async Task GetDimensions()
    {
        // Get size object
        var size = await Viewport.GetViewportSizeAsync();
        Console.WriteLine($"Viewport: {size.Width}x{size.Height}");

        // Get individual dimensions
        int width = await Viewport.GetViewportWidthAsync();
        int height = await Viewport.GetViewportHeightAsync();

        // Get screen size
        var screenSize = await Viewport.GetScreenSizeAsync();
        Console.WriteLine($"Screen: {screenSize.Width}x{screenSize.Height}");
    }
}
```

#### Orientation Detection

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport

@code {
    private async Task CheckOrientation()
    {
        string orientation = await Viewport.GetOrientationAsync();
        bool isPortrait = await Viewport.IsPortraitAsync();
        bool isLandscape = await Viewport.IsLandscapeAsync();

        Console.WriteLine($"Orientation: {orientation}");
        
        if (isPortrait)
        {
            // Adjust layout for portrait mode
        }
    }
}
```

#### Device Detection

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport

@code {
    private async Task DetectDevice()
    {
        string deviceType = await Viewport.GetDeviceTypeAsync();
        // Returns: "mobile", "tablet", or "desktop"

        bool isMobile = await Viewport.IsMobileAsync();
        bool isTablet = await Viewport.IsTabletAsync();
        bool isDesktop = await Viewport.IsDesktopAsync();

        double pixelRatio = await Viewport.GetPixelRatioAsync();
        Console.WriteLine($"Pixel Ratio: {pixelRatio}");
    }
}
```

#### Fullscreen Support

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport

<button @onclick="ToggleFullscreen">Toggle Fullscreen</button>

@code {
    private async Task ToggleFullscreen()
    {
        bool isFullscreen = await Viewport.IsFullscreenAsync();
        
        if (isFullscreen)
        {
            await Viewport.ExitFullscreenAsync();
        }
        else
        {
            await Viewport.RequestFullscreenAsync();
        }
    }
}
```

### API Reference

#### Viewport Dimensions

**GetViewportSizeAsync()**
Returns viewport dimensions as a `Size` record with `Width` and `Height` properties.

```csharp
ViewportInterop.Size size = await Viewport.GetViewportSizeAsync();
Console.WriteLine($"{size.Width}x{size.Height}");
```

**GetViewportWidthAsync()**
Gets the viewport width in pixels.

```csharp
int width = await Viewport.GetViewportWidthAsync();
```

**GetViewportHeightAsync()**
Gets the viewport height in pixels.

```csharp
int height = await Viewport.GetViewportHeightAsync();
```

#### Screen Dimensions

**GetScreenSizeAsync()**
Returns screen dimensions as a `Size` record.

```csharp
ViewportInterop.Size screenSize = await Viewport.GetScreenSizeAsync();
```

**GetPixelRatioAsync()**
Gets the device pixel ratio (e.g., `2.0` for Retina displays).

```csharp
double pixelRatio = await Viewport.GetPixelRatioAsync();
```

#### Device Type Detection

**GetDeviceTypeAsync()**
Returns `"mobile"`, `"tablet"`, or `"desktop"`.

```csharp
string deviceType = await Viewport.GetDeviceTypeAsync();
```

**IsMobileAsync()**
Returns `true` if device is mobile (viewport width < 768px).

```csharp
bool isMobile = await Viewport.IsMobileAsync();
```

**IsTabletAsync()**
Returns `true` if device is tablet (viewport width 768-1024px).

```csharp
bool isTablet = await Viewport.IsTabletAsync();
```

**IsDesktopAsync()**
Returns `true` if device is desktop (viewport width >= 1024px).

```csharp
bool isDesktop = await Viewport.IsDesktopAsync();
```

#### Orientation

**GetOrientationAsync()**
Returns `"portrait"` or `"landscape"`.

```csharp
string orientation = await Viewport.GetOrientationAsync();
```

**IsPortraitAsync()**
Returns `true` if in portrait orientation (height > width).

```csharp
bool isPortrait = await Viewport.IsPortraitAsync();
```

**IsLandscapeAsync()**
Returns `true` if in landscape orientation (width >= height).

```csharp
bool isLandscape = await Viewport.IsLandscapeAsync();
```

#### Fullscreen

**IsFullscreenAsync()**
Returns `true` if the page is in fullscreen mode.

```csharp
bool isFullscreen = await Viewport.IsFullscreenAsync();
```

**RequestFullscreenAsync()**
Requests fullscreen mode for the document.

```csharp
await Viewport.RequestFullscreenAsync();
```

**ExitFullscreenAsync()**
Exits fullscreen mode.

```csharp
await Viewport.ExitFullscreenAsync();
```

## Clipboard API

### Overview

The `ClipboardInterop` service provides access to the Clipboard API with fallback support for older browsers and permission handling.

### Features

- **Copy to Clipboard**: with fallback for older browsers
- **Read from Clipboard**: with permission requests
- **Feature Detection**: check if Clipboard API is supported
- **Permission Checks**: verify read/write permissions

### Usage

#### Copy to Clipboard

```csharp
@using Blazouter.Interops
@inject ClipboardInterop Clipboard

<input @bind="_shareUrl" />
<button @onclick="CopyLink">Copy Link</button>
<p>@_message</p>

@code {
    private string _shareUrl = "https://example.com/share/123";
    private string _message = "";

    private async Task CopyLink()
    {
        bool success = await Clipboard.CopyTextAsync(_shareUrl);
        
        _message = success 
            ? "Link copied to clipboard!" 
            : "Failed to copy link";
        
        StateHasChanged();
    }
}
```

#### Read from Clipboard

```csharp
@using Blazouter.Interops
@inject ClipboardInterop Clipboard

<button @onclick="PasteFromClipboard">Paste</button>
<p>Pasted: @_pastedText</p>

@code {
    private string _pastedText = "";

    private async Task PasteFromClipboard()
    {
        string? text = await Clipboard.ReadTextAsync();
        
        if (text != null)
        {
            _pastedText = text;
            StateHasChanged();
        }
        else
        {
            Console.WriteLine("Clipboard is empty or permission denied");
        }
    }
}
```

#### Feature Detection

```csharp
@using Blazouter.Interops
@inject ClipboardInterop Clipboard

@code {
    protected override async Task OnInitializedAsync()
    {
        bool isSupported = await Clipboard.IsClipboardSupportedAsync();
        
        if (!isSupported)
        {
            Console.WriteLine("Clipboard API not supported in this browser");
            return;
        }

        // Check specific permissions
        bool canRead = await Clipboard.HasClipboardReadPermissionAsync();
        bool canWrite = await Clipboard.HasClipboardWritePermissionAsync();

        Console.WriteLine($"Can read: {canRead}, Can write: {canWrite}");
    }
}
```

### API Reference

**CopyTextAsync(text)**
Copies text to the clipboard. Returns `true` if successful.

```csharp
bool success = await Clipboard.CopyTextAsync("text to copy");
```

**ReadTextAsync()**
Reads text from the clipboard. Returns `null` if permission denied or clipboard is empty.

```csharp
string? text = await Clipboard.ReadTextAsync();
```

**IsClipboardSupportedAsync()**
Returns `true` if the Clipboard API is supported by the browser.

```csharp
bool supported = await Clipboard.IsClipboardSupportedAsync();
```

**HasClipboardReadPermissionAsync()**
Returns `true` if read permission is granted.

```csharp
bool canRead = await Clipboard.HasClipboardReadPermissionAsync();
```

**HasClipboardWritePermissionAsync()**
Returns `true` if write permission is granted.

```csharp
bool canWrite = await Clipboard.HasClipboardWritePermissionAsync();
```

## TypeScript Development

### Project Structure

The TypeScript source files are organized in a separate project:

```
src/
├── Blazouter.TypeScript/           # Separate TypeScript project
│   ├── TypeScript/                 # TypeScript source files
│   │   ├── navigation.ts           # Navigation API implementation
│   │   ├── document.ts             # Document API implementation
│   │   ├── storage.ts              # Storage API implementation
│   │   ├── viewport.ts             # Viewport API implementation
│   │   ├── clipboard.ts            # Clipboard API implementation
│   │   └── index.ts                # Main entry point, imports all modules
│   ├── package.json                # NPM dependencies
│   └── tsconfig.json               # TypeScript compiler configuration
│
├── Blazouter/                      # Main C# project
│   ├── Interops/                   # C# interop services
│   │   ├── NavigationInterop.cs
│   │   ├── DocumentInterop.cs
│   │   ├── StorageInterop.cs
│   │   ├── ViewportInterop.cs
│   │   └── ClipboardInterop.cs
│   └── wwwroot/js/                 # Compiled JavaScript output (included in NuGet)
│       ├── navigation.js           # Compiled module
│       ├── navigation.d.ts         # TypeScript definitions
│       ├── navigation.js.map       # Source map
│       ├── document.js
│       ├── document.d.ts
│       ├── document.js.map
│       ├── storage.js
│       ├── storage.d.ts
│       ├── storage.js.map
│       ├── viewport.js
│       ├── viewport.d.ts
│       ├── viewport.js.map
│       ├── clipboard.js
│       ├── clipboard.d.ts
│       ├── clipboard.js.map
│       ├── index.js                # Main entry point
│       ├── index.d.ts
│       └── index.js.map
```

### Building TypeScript

To compile the TypeScript source files:

```bash
cd src/Blazouter.TypeScript

# Install dependencies (first time only)
npm install

# Build once
npm run build

# Watch for changes and rebuild automatically
npm run watch
```

The compiled JavaScript files are automatically copied to `src/Blazouter/wwwroot/js/` and included in the NuGet package.

### TypeScript Configuration

The `tsconfig.json` is configured for:

- **Target**: ES2020 (modern JavaScript)
- **Module**: ES2020 (ES modules)
- **Module Resolution**: Node
- **Output**: `../Blazouter/wwwroot/js/` (compiled to main project)
- **Strict Mode**: Enabled for type safety
- **Source Maps**: Generated for debugging
- **Declaration Files**: `.d.ts` files generated for IntelliSense
- **Libraries**: DOM and ES2020

### Module Structure

Each TypeScript module exposes functions via a global `window` object:

- `window.blazouterNavigation` - navigation.ts exports
- `window.blazouterDocument` - document.ts exports
- `window.blazouterStorage` - storage.ts exports
- `window.blazouterViewport` - viewport.ts exports
- `window.blazouterClipboard` - clipboard.ts exports

The `index.ts` file imports all modules and ensures they're initialized.

## Best Practices

### 1. Always Check Service Availability

When injecting interop services, use nullable types and check for `null`:

```csharp
@inject NavigationInterop? Navigation

@code {
    private async Task GoBack()
    {
        if (Navigation != null)
        {
            await Navigation.GoBackAsync();
        }
        else
        {
            // Fallback or log error
            Console.WriteLine("Navigation service not available");
        }
    }
}
```

### 2. Handle Exceptions

Always wrap interop calls in try-catch blocks:

```csharp
@inject DocumentInterop Document

@code {
    private async Task UpdateTitle()
    {
        try
        {
            await Document.SetTitleAsync("New Title");
        }
        catch (JSException ex)
        {
            Console.WriteLine($"Failed to set title: {ex.Message}");
        }
    }
}
```

### 3. Use OnAfterRender for Initial Interop

JavaScript interop is not available during pre-rendering. Use `OnAfterRenderAsync`:

```csharp
@inject DocumentInterop Document

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Safe to call JS interop here
            await Document.SetTitleAsync("My Page");
        }
    }
}
```

### 4. Integrate with RouterStateService

Combine interop services with routing events for dynamic behavior:

```csharp
@inject DocumentInterop Document
@inject RouterStateService RouterState
@implements IDisposable

@code {
    protected override void OnInitialized()
    {
        RouterState.OnRouteChanged += HandleRouteChanged;
    }

    private async Task HandleRouteChanged()
    {
        // Update document properties on route change
        await Document.ScrollToTopAsync();
        
        var route = RouterState.CurrentRoute;
        if (route?.Route?.Title != null)
        {
            await Document.SetTitleAsync($"{route.Route.Title} - My App");
        }
    }

    public void Dispose()
    {
        RouterState.OnRouteChanged -= HandleRouteChanged;
    }
}
```

### 5. Use Proper Types for Storage

Take advantage of generic type support in StorageInterop:

```csharp
@inject StorageInterop Storage

@code {
    // Define a model
    public class UserSettings
    {
        public string Theme { get; set; } = "light";
        public string Language { get; set; } = "en";
        public bool Notifications { get; set; } = true;
    }

    private async Task SaveSettings(UserSettings settings)
    {
        // Type-safe storage
        await Storage.SetLocalStorageAsync("settings", settings);
    }

    private async Task<UserSettings?> LoadSettings()
    {
        // Type-safe retrieval
        return await Storage.GetLocalStorageAsync<UserSettings>("settings");
    }
}
```

## Migration Guide

### From Manual JS Interop

**Before:**
```csharp
@inject IJSRuntime JSRuntime

private async Task GoBack()
{
    await JSRuntime.InvokeVoidAsync("eval", "window.history.back()");
}
```

**After:**
```csharp
@inject RouterNavigationService NavService

private async Task GoBack()
{
    await NavService.GoBackAsync();
}
```

### From Old RouterNavigationService.GoBack()

The old synchronous `GoBack()` method was a placeholder. Replace it with the new async version:

**Before:**
```csharp
@inject RouterNavigationService NavService

private void GoBack()
{
    NavService.GoBack();  // Old placeholder method
}
```

**After:**
```csharp
@inject RouterNavigationService NavService

private async Task GoBack()
{
    await NavService.GoBackAsync();  // Proper browser navigation
}
```

## Troubleshooting

### Service Not Registered Error

**Error:**
```
InvalidOperationException: Unable to resolve service for type 'Blazouter.Interops.NavigationInterop'
```

**Solution:**
Add `AddBlazouterInterop()` to your service registration in `Program.cs`:

```csharp
builder.Services.AddBlazouter();
builder.Services.AddBlazouterInterop();  // Add this line
```

### JavaScript Module Not Found / Undefined Errors

**Error in browser console:**
```
Uncaught TypeError: Cannot read properties of undefined (reading 'goBack')
```

**Solution:**
Add the JavaScript module import to your `index.html` or `App.razor`:

```html
<script type="module" src="_content/Blazouter/js/index.js"></script>
```

### TypeScript Compilation Errors

**Error:**
```
error TS2307: Cannot find module './navigation.js'
```

**Solution:**
Ensure you're using `.js` extensions in imports (required for ES modules):

```typescript
// Correct:
import * as navigation from './navigation.js';

// Wrong:
import * as navigation from './navigation';
```

### Build Errors

If TypeScript files are not compiling:

```bash
cd src/Blazouter.TypeScript

# Clean and reinstall
rm -rf node_modules package-lock.json
npm install

# Rebuild
npm run build
```

## Complete Examples

### Full SEO and Social Media Setup

```csharp
// ProductDetail.razor
@using Blazouter.Interops
@inject DocumentInterop Document
@inject ProductService ProductService
@inject RouterStateService RouterState

<h1>@_product?.Name</h1>
<p>@_product?.Description</p>

// ProductDetail.razor.cs
using Blazouter.Attributes;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

[Route("/products/:id")]
[RouteTitle("Product Details")]
public partial class ProductDetail : ComponentBase
{
    [Inject] private DocumentInterop Document { get; set; } = default!;
    [Inject] private ProductService ProductService { get; set; } = default!;
    [Inject] private RouterStateService RouterState { get; set; } = default!;

    private string? _productId;
    private Product? _product;

    protected override async Task OnInitializedAsync()
    {
        _productId = RouterState.GetParam("id");
        
        if (_productId != null)
        {
            _product = await ProductService.GetProductAsync(_productId);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && _product != null)
        {
            // Set page title
            await Document.SetTitleAsync($"{_product.Name} - My Store");

            // Set meta tags
            await Document.SetMetaTagAsync("description", _product.Description);
            await Document.SetMetaTagAsync("keywords", string.Join(", ", _product.Tags));

            // Set Open Graph tags for social media
            await Document.SetOpenGraphTagAsync("og:title", _product.Name);
            await Document.SetOpenGraphTagAsync("og:description", _product.Description);
            await Document.SetOpenGraphTagAsync("og:image", _product.ImageUrl);
            await Document.SetOpenGraphTagAsync("og:type", "product");
            await Document.SetOpenGraphTagAsync("og:url", $"https://mystore.com/products/{_productId}");

            // Set canonical URL
            await Document.SetCanonicalUrlAsync($"https://mystore.com/products/{_productId}");

            // Scroll to top on page load
            await Document.ScrollToTopAsync();
        }
    }
}
```

### Responsive Navigation Menu

```csharp
@using Blazouter.Interops
@inject ViewportInterop Viewport
@inject RouterNavigationService NavService

<div class="@(_showMobileMenu ? "mobile-menu-open" : "")">
    @if (_isMobile)
    {
        <button @onclick="ToggleMobileMenu">☰ Menu</button>
        
        @if (_showMobileMenu)
        {
            <nav class="mobile-nav">
                <a href="/" @onclick="NavigateHome">Home</a>
                <a href="/about" @onclick="NavigateAbout">About</a>
                <a href="/products" @onclick="NavigateProducts">Products</a>
            </nav>
        }
    }
    else
    {
        <nav class="desktop-nav">
            <a href="/">Home</a>
            <a href="/about">About</a>
            <a href="/products">Products</a>
        </nav>
    }
    
    <button @onclick="GoBack" disabled="@(!_canGoBack)">← Back</button>
</div>

@code {
    private bool _isMobile = false;
    private bool _showMobileMenu = false;
    private bool _canGoBack = false;

    protected override async Task OnInitializedAsync()
    {
        _isMobile = await Viewport.IsMobileAsync();
        _canGoBack = await NavService.Navigation?.CanGoBackAsync() ?? false;
    }

    private void ToggleMobileMenu()
    {
        _showMobileMenu = !_showMobileMenu;
    }

    private async Task GoBack()
    {
        await NavService.GoBackAsync();
    }

    private void NavigateHome() => CloseMenuAndNavigate("/");
    private void NavigateAbout() => CloseMenuAndNavigate("/about");
    private void NavigateProducts() => CloseMenuAndNavigate("/products");

    private void CloseMenuAndNavigate(string path)
    {
        _showMobileMenu = false;
        NavService.NavigateTo(path);
    }
}
```

### Persistent User Preferences

```csharp
@using Blazouter.Interops
@inject StorageInterop Storage
@implements IDisposable

<div class="preferences">
    <label>
        Theme:
        <select @bind="_theme" @bind:after="SavePreferences">
            <option value="light">Light</option>
            <option value="dark">Dark</option>
            <option value="auto">Auto</option>
        </select>
    </label>

    <label>
        Language:
        <select @bind="_language" @bind:after="SavePreferences">
            <option value="en">English</option>
            <option value="tr">Türkçe</option>
            <option value="es">Español</option>
        </select>
    </label>

    <label>
        <input type="checkbox" @bind="_notifications" @bind:after="SavePreferences" />
        Enable Notifications
    </label>
</div>

@code {
    private string _theme = "light";
    private string _language = "en";
    private bool _notifications = true;

    public class UserPreferences
    {
        public string Theme { get; set; } = "light";
        public string Language { get; set; } = "en";
        public bool Notifications { get; set; } = true;
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadPreferences();
    }

    private async Task LoadPreferences()
    {
        var prefs = await Storage.GetLocalStorageAsync<UserPreferences>("userPreferences");
        
        if (prefs != null)
        {
            _theme = prefs.Theme;
            _language = prefs.Language;
            _notifications = prefs.Notifications;
            
            ApplyPreferences(prefs);
        }
    }

    private async Task SavePreferences()
    {
        var prefs = new UserPreferences
        {
            Theme = _theme,
            Language = _language,
            Notifications = _notifications
        };

        await Storage.SetLocalStorageAsync("userPreferences", prefs);
        ApplyPreferences(prefs);
    }

    private void ApplyPreferences(UserPreferences prefs)
    {
        // Apply theme, language, etc.
        Console.WriteLine($"Applied preferences: {prefs.Theme}, {prefs.Language}");
    }

    public void Dispose()
    {
        // Clean up if needed
    }
}
```

## Conclusion

The TypeScript integration in Blazouter provides 5 comprehensive, type-safe interop services for browser APIs:

1. **ClipboardInterop** - Clipboard operations with permissions
2. **ViewportInterop** - Responsive design and device detection
3. **StorageInterop** - Type-safe localStorage and sessionStorage
4. **NavigationInterop** - Complete history and URL management
5. **DocumentInterop** - Document manipulation, SEO, and scrolling

All services are:
- **Optional** - existing apps work unchanged
- **Type-safe** - full TypeScript and C# type support
- **Production-ready** - error handling and fallbacks included
- **Well-documented** - comprehensive API reference and examples

For more information:
- [Changelog](CHANGELOG.md) - Version history
- [Contributing](CONTRIBUTING.md) - Development guidelines
- [Main Documentation](README.md) - Getting started guide
- [Features Documentation](FEATURES.md) - Complete feature list
