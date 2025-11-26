using Microsoft.JSInterop;

namespace Blazouter.Interops
{
    /// <summary>
    /// Provides JavaScript interop for viewport and screen dimension operations.
    /// </summary>
    /// <remarks>
    /// This service wraps browser viewport and screen APIs with type-safe C# methods,
    /// using the TypeScript-defined JavaScript interop functions. Useful for responsive design
    /// and device detection.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ViewportInterop"/> class.
    /// </remarks>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public class ViewportInterop(IJSRuntime jsRuntime)
    {
        /// <summary>
        /// Represents viewport or screen dimensions.
        /// </summary>
        public record Size(int Width, int Height);

        #region Viewport Dimensions

        /// <summary>
        /// Gets the current viewport width in pixels.
        /// </summary>
        /// <returns>The viewport width.</returns>
        public async Task<int> GetViewportWidthAsync()
        {
            return await jsRuntime.InvokeAsync<int>("blazouterViewport.getViewportWidth");
        }

        /// <summary>
        /// Gets the current viewport height in pixels.
        /// </summary>
        /// <returns>The viewport height.</returns>
        public async Task<int> GetViewportHeightAsync()
        {
            return await jsRuntime.InvokeAsync<int>("blazouterViewport.getViewportHeight");
        }

        /// <summary>
        /// Gets the current viewport dimensions.
        /// </summary>
        /// <returns>The viewport width and height.</returns>
        /// <example>
        /// <code>
        /// var size = await viewportInterop.GetViewportSizeAsync();
        /// Console.WriteLine($"Viewport: {size.Width}x{size.Height}");
        /// </code>
        /// </example>
        public async Task<Size> GetViewportSizeAsync()
        {
            dynamic result = await jsRuntime.InvokeAsync<dynamic>("blazouterViewport.getViewportSize");
            return new Size((int)result.width, (int)result.height);
        }

        #endregion

        #region Screen Dimensions

        /// <summary>
        /// Gets the screen width in pixels.
        /// </summary>
        /// <returns>The screen width.</returns>
        public async Task<int> GetScreenWidthAsync()
        {
            return await jsRuntime.InvokeAsync<int>("blazouterViewport.getScreenWidth");
        }

        /// <summary>
        /// Gets the screen height in pixels.
        /// </summary>
        /// <returns>The screen height.</returns>
        public async Task<int> GetScreenHeightAsync()
        {
            return await jsRuntime.InvokeAsync<int>("blazouterViewport.getScreenHeight");
        }

        /// <summary>
        /// Gets the screen dimensions.
        /// </summary>
        /// <returns>The screen width and height.</returns>
        public async Task<Size> GetScreenSizeAsync()
        {
            dynamic result = await jsRuntime.InvokeAsync<dynamic>("blazouterViewport.getScreenSize");
            return new Size((int)result.width, (int)result.height);
        }

        #endregion

        #region Device Detection

        /// <summary>
        /// Gets the device pixel ratio.
        /// </summary>
        /// <returns>The device pixel ratio (typically 1, 1.5, 2, or higher for retina displays).</returns>
        /// <remarks>
        /// Higher values indicate higher resolution displays. Useful for serving appropriate image resolutions.
        /// </remarks>
        public async Task<double> GetPixelRatioAsync()
        {
            return await jsRuntime.InvokeAsync<double>("blazouterViewport.getPixelRatio");
        }

        /// <summary>
        /// Checks if the viewport is in portrait orientation.
        /// </summary>
        /// <returns>True if the viewport height is greater than width; otherwise, false.</returns>
        public async Task<bool> IsPortraitAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isPortrait");
        }

        /// <summary>
        /// Checks if the viewport is in landscape orientation.
        /// </summary>
        /// <returns>True if the viewport width is greater than height; otherwise, false.</returns>
        public async Task<bool> IsLandscapeAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isLandscape");
        }

        /// <summary>
        /// Gets the screen orientation.
        /// </summary>
        /// <returns>"portrait" or "landscape".</returns>
        public async Task<string> GetOrientationAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterViewport.getOrientation");
        }

        /// <summary>
        /// Checks if the device is mobile-sized (viewport width &lt; 768px).
        /// </summary>
        /// <returns>True if mobile-sized; otherwise, false.</returns>
        /// <remarks>
        /// Uses standard responsive breakpoints: mobile &lt; 768px, tablet 768-1024px, desktop &gt;= 1024px.
        /// </remarks>
        public async Task<bool> IsMobileAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isMobile");
        }

        /// <summary>
        /// Checks if the device is tablet-sized (768px &lt;= viewport width &lt; 1024px).
        /// </summary>
        /// <returns>True if tablet-sized; otherwise, false.</returns>
        public async Task<bool> IsTabletAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isTablet");
        }

        /// <summary>
        /// Checks if the device is desktop-sized (viewport width &gt;= 1024px).
        /// </summary>
        /// <returns>True if desktop-sized; otherwise, false.</returns>
        public async Task<bool> IsDesktopAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isDesktop");
        }

        /// <summary>
        /// Gets the device type based on viewport width.
        /// </summary>
        /// <returns>"mobile", "tablet", or "desktop".</returns>
        /// <example>
        /// <code>
        /// var deviceType = await viewportInterop.GetDeviceTypeAsync();
        /// if (deviceType == "mobile") {
        ///     // Show mobile-specific UI
        /// }
        /// </code>
        /// </example>
        public async Task<string> GetDeviceTypeAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterViewport.getDeviceType");
        }

        #endregion

        #region Fullscreen

        /// <summary>
        /// Checks if the page is currently in fullscreen mode.
        /// </summary>
        /// <returns>True if in fullscreen mode; otherwise, false.</returns>
        public async Task<bool> IsFullscreenAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.isFullscreen");
        }

        /// <summary>
        /// Requests fullscreen mode for the document.
        /// </summary>
        /// <returns>True if the request succeeded; otherwise, false.</returns>
        /// <remarks>
        /// This must be triggered by a user action (e.g., button click) due to browser security restrictions.
        /// </remarks>
        /// <example>
        /// <code>
        /// // In a button click handler
        /// bool success = await viewportInterop.RequestFullscreenAsync();
        /// </code>
        /// </example>
        public async Task<bool> RequestFullscreenAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.requestFullscreen");
        }

        /// <summary>
        /// Exits fullscreen mode.
        /// </summary>
        /// <returns>True if the operation succeeded; otherwise, false.</returns>
        public async Task<bool> ExitFullscreenAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterViewport.exitFullscreen");
        }

        #endregion
    }
}