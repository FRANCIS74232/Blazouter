using Microsoft.AspNetCore.Components;

namespace Blazouter.Services
{
    /// <summary>
    /// Provides programmatic navigation capabilities for Blazouter routing.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterNavigationService wraps Blazor's NavigationManager with additional functionality tailored
    /// for Blazouter's routing system. It provides methods for navigating to routes, building query strings,
    /// and accessing current URI information.
    /// </para>
    /// <para>
    /// This service is registered as scoped, ensuring proper isolation in Blazor Server scenarios where
    /// multiple users may be using the application simultaneously.
    /// </para>
    /// </remarks>
    /// <example>
    /// Inject and use the navigation service in a component:
    /// <code>
    /// @inject RouterNavigationService NavService
    /// 
    /// private void GoToUserProfile(int userId)
    /// {
    ///     NavService.NavigateTo($"/users/{userId}");
    /// }
    /// </code>
    /// </example>
    public class RouterNavigationService(NavigationManager navigationManager, RouterStateService routerState)
    {
        /// <summary>
        /// Navigates to the specified path.
        /// </summary>
        /// <param name="path">The relative or absolute path to navigate to.</param>
        /// <param name="forceLoad">
        /// If true, forces a full page reload. If false (default), uses client-side navigation.
        /// </param>
        /// <remarks>
        /// <para>
        /// Client-side navigation (forceLoad = false) is preferred for SPA behavior, as it doesn't
        /// cause a full page reload and maintains application state. Use forceLoad = true only when
        /// you need to reload the entire application or navigate to external URLs.
        /// </para>
        /// <para>
        /// The path can be relative ("/users/123") or absolute ("https://example.com"). For relative
        /// paths, the navigation is relative to the application's base URI.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Client-side navigation (default)
        /// NavService.NavigateTo("/products/42");
        /// 
        /// // Force full page reload
        /// NavService.NavigateTo("/admin/reset", forceLoad: true);
        /// 
        /// // Navigate to external URL
        /// NavService.NavigateTo("https://example.com", forceLoad: true);
        /// </code>
        /// </example>
        public void NavigateTo(string path, bool forceLoad = false)
        {
            navigationManager.NavigateTo(path, forceLoad);
        }

        /// <summary>
        /// Navigates to a path with query string parameters.
        /// </summary>
        /// <param name="path">The relative or absolute path to navigate to (without query string).</param>
        /// <param name="queryParams">Dictionary of query parameter key-value pairs to append to the path.</param>
        /// <param name="forceLoad">
        /// If true, forces a full page reload. If false (default), uses client-side navigation.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method builds a properly formatted query string from the provided dictionary, handling
        /// URL encoding automatically. The resulting URL will be in the format: /path?key1=value1&amp;key2=value2
        /// </para>
        /// <para>
        /// All parameter keys and values are URL-encoded to ensure special characters are handled correctly.
        /// Empty dictionaries result in navigation to the path without any query string.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Navigate to /search?q=blazor&amp;category=libraries
        /// var searchParams = new Dictionary&lt;string, string&gt;
        /// {
        ///     { "q", "blazor" },
        ///     { "category", "libraries" }
        /// };
        /// NavService.NavigateTo("/search", searchParams);
        /// 
        /// // Navigate to /products?page=2&amp;sort=price
        /// NavService.NavigateTo("/products", new Dictionary&lt;string, string&gt;
        /// {
        ///     { "page", "2" },
        ///     { "sort", "price" }
        /// });
        /// </code>
        /// </example>
        public void NavigateTo(string path, Dictionary<string, string> queryParams, bool forceLoad = false)
        {
            string queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            string fullPath = string.IsNullOrEmpty(queryString) ? path : $"{path}?{queryString}";
            navigationManager.NavigateTo(fullPath, forceLoad);
        }

        /// <summary>
        /// Attempts to navigate back in browser history.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <strong>Note:</strong> This is a placeholder implementation that navigates to the current path.
        /// True browser back navigation requires JavaScript interop and is not currently implemented.
        /// </para>
        /// <para>
        /// For proper back navigation in production applications, consider implementing JavaScript interop
        /// to call the browser's history.back() method, or maintain your own navigation stack in application state.
        /// </para>
        /// </remarks>
        /// <example>
        /// Custom back navigation implementation pattern:
        /// <code>
        /// // In your JavaScript file
        /// window.blazouterHelpers = {
        ///     goBack: function() {
        ///         window.history.back();
        ///     }
        /// };
        /// 
        /// // In your C# code
        /// await JSRuntime.InvokeVoidAsync("blazouterHelpers.goBack");
        /// </code>
        /// </example>
        public void GoBack()
        {
            // Note: Browser back navigation requires JavaScript interop
            // This is a placeholder - actual implementation would need JS interop
            navigationManager.NavigateTo(routerState.CurrentPath);
        }

        /// <summary>
        /// Gets the current absolute URI of the application.
        /// </summary>
        /// <returns>
        /// A string containing the complete current URI including protocol, host, path, and query string.
        /// </returns>
        /// <remarks>
        /// The returned URI includes the full URL (e.g., "https://example.com/users/123?tab=profile").
        /// Use this when you need the complete URI for sharing, logging, or external integrations.
        /// </remarks>
        /// <example>
        /// <code>
        /// string currentUrl = NavService.GetCurrentUri();
        /// // Example: "https://localhost:5001/products/42?sort=price"
        /// </code>
        /// </example>
        public string GetCurrentUri()
        {
            return navigationManager.Uri;
        }

        /// <summary>
        /// Gets the base URI of the application.
        /// </summary>
        /// <returns>
        /// A string containing the base URI including protocol, host, and base path.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The base URI is the root URL of the application. For applications hosted at the domain root,
        /// this is just the protocol and host (e.g., "https://example.com/"). For applications hosted
        /// in a subdirectory, it includes that path (e.g., "https://example.com/myapp/").
        /// </para>
        /// <para>
        /// Use this when constructing absolute URLs or when you need to determine the application's
        /// deployment location programmatically.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string baseUrl = NavService.GetBaseUri();
        /// // Root deployment: "https://localhost:5001/"
        /// // Subdirectory deployment: "https://example.com/myapp/"
        /// </code>
        /// </example>
        public string GetBaseUri()
        {
            return navigationManager.BaseUri;
        }
    }
}