using Microsoft.JSInterop;

namespace Blazouter.Interops
{
    /// <summary>
    /// Provides JavaScript interop for browser navigation operations.
    /// </summary>
    /// <remarks>
    /// This service wraps the browser's History API with type-safe C# methods,
    /// using the TypeScript-defined JavaScript interop functions.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="NavigationInterop"/> class.
    /// </remarks>
    /// <param name="jsRuntime">The JavaScript runtime for interop.</param>
    public class NavigationInterop(IJSRuntime jsRuntime)
    {
        /// <summary>
        /// Navigates back in browser history.
        /// </summary>
        /// <remarks>
        /// This method calls the browser's history.back() function only if there is
        /// history to navigate back to (history.length > 1).
        /// </remarks>
        public async Task GoBackAsync()
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.goBack");
        }

        /// <summary>
        /// Navigates forward in browser history.
        /// </summary>
        public async Task GoForwardAsync()
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.goForward");
        }

        /// <summary>
        /// Navigates to a specific position in browser history.
        /// </summary>
        /// <param name="delta">
        /// The number of steps to navigate. Negative values go back, positive values go forward.
        /// </param>
        /// <example>
        /// <code>
        /// // Go back 2 pages
        /// await navigationInterop.GoAsync(-2);
        /// 
        /// // Go forward 1 page
        /// await navigationInterop.GoAsync(1);
        /// </code>
        /// </example>
        public async Task GoAsync(int delta)
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.go", delta);
        }

        /// <summary>
        /// Gets the current history length.
        /// </summary>
        /// <returns>The number of entries in the browser's history stack.</returns>
        public async Task<int> GetHistoryLengthAsync()
        {
            return await jsRuntime.InvokeAsync<int>("blazouterNavigation.getHistoryLength");
        }

        /// <summary>
        /// Checks if the browser can navigate back.
        /// </summary>
        /// <returns>True if there is history to navigate back to; otherwise, false.</returns>
        public async Task<bool> CanGoBackAsync()
        {
            return await jsRuntime.InvokeAsync<bool>("blazouterNavigation.canGoBack");
        }

        /// <summary>
        /// Pushes a new state to browser history without navigation.
        /// </summary>
        /// <param name="state">The state object to push.</param>
        /// <param name="title">The title (currently unused by most browsers).</param>
        /// <param name="url">The optional URL to display in the address bar.</param>
        public async Task PushStateAsync(object? state, string title, string? url = null)
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.pushState", state, title, url);
        }

        /// <summary>
        /// Replaces the current state in browser history.
        /// </summary>
        /// <param name="state">The state object to replace.</param>
        /// <param name="title">The title (currently unused by most browsers).</param>
        /// <param name="url">The optional URL to display in the address bar.</param>
        public async Task ReplaceStateAsync(object? state, string title, string? url = null)
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.replaceState", state, title, url);
        }

        /// <summary>
        /// Gets the current history state.
        /// </summary>
        /// <returns>The current state object.</returns>
        public async Task<object?> GetStateAsync()
        {
            return await jsRuntime.InvokeAsync<object?>("blazouterNavigation.getState");
        }

        /// <summary>
        /// Gets the current URL.
        /// </summary>
        /// <returns>The full current URL.</returns>
        public async Task<string> GetCurrentUrlAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterNavigation.getCurrentUrl");
        }

        /// <summary>
        /// Gets the current pathname.
        /// </summary>
        /// <returns>The pathname portion of the current URL.</returns>
        public async Task<string> GetPathnameAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterNavigation.getPathname");
        }

        /// <summary>
        /// Gets the current hash (without the # symbol).
        /// </summary>
        /// <returns>The hash portion of the URL without the # symbol.</returns>
        public async Task<string> GetHashAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterNavigation.getHash");
        }

        /// <summary>
        /// Sets the hash without page reload.
        /// </summary>
        /// <param name="hash">The hash value (without # symbol).</param>
        public async Task SetHashAsync(string hash)
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.setHash", hash);
        }

        /// <summary>
        /// Gets the current query string (without the ? symbol).
        /// </summary>
        /// <returns>The query string without the ? symbol.</returns>
        public async Task<string> GetQueryStringAsync()
        {
            return await jsRuntime.InvokeAsync<string>("blazouterNavigation.getQueryString");
        }

        /// <summary>
        /// Gets a query parameter value by name.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <returns>The parameter value, or null if not found.</returns>
        public async Task<string?> GetQueryParamAsync(string name)
        {
            return await jsRuntime.InvokeAsync<string?>("blazouterNavigation.getQueryParam", name);
        }

        /// <summary>
        /// Gets all query parameters as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing all query parameters.</returns>
        public async Task<Dictionary<string, string>> GetAllQueryParamsAsync()
        {
            return await jsRuntime.InvokeAsync<Dictionary<string, string>>("blazouterNavigation.getAllQueryParams");
        }

        /// <summary>
        /// Reloads the current page.
        /// </summary>
        /// <param name="forceReload">Whether to force reload from server (bypass cache). Note: Modern browsers may ignore this parameter.</param>
        public async Task ReloadAsync(bool forceReload = false)
        {
            await jsRuntime.InvokeVoidAsync("blazouterNavigation.reload", forceReload);
        }
    }
}