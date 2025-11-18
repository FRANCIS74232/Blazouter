using Blazouter.Models;

namespace Blazouter.Services
{
    /// <summary>
    /// Manages the global router state and provides access to current route information.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterStateService is a singleton service that maintains the current route match and path.
    /// It provides a centralized location for components to access route information and subscribe
    /// to route change notifications.
    /// </para>
    /// <para>
    /// Components can inject this service to:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Access the current route match and its properties</description></item>
    /// <item><description>Retrieve route parameters and query string values</description></item>
    /// <item><description>Subscribe to route change events</description></item>
    /// <item><description>Determine the current path for conditional rendering</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Access route parameters in a component:
    /// <code>
    /// @inject RouterStateService RouterState
    /// 
    /// &lt;h1&gt;User: @_userId&lt;/h1&gt;
    /// 
    /// @code {
    ///     private string? _userId;
    ///     
    ///     protected override void OnInitialized()
    ///     {
    ///         _userId = RouterState.GetParam("id");
    ///     }
    /// }
    /// </code>
    /// </example>
    public class RouterStateService
    {
        /// <summary>
        /// Occurs when the current route changes.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Subscribe to this event to be notified whenever navigation occurs. The event provides
        /// the new RouteMatch object, or null if no route matched the new URL.
        /// </para>
        /// <para>
        /// This is useful for components that need to react to navigation, such as breadcrumb
        /// components, analytics tracking, or components that display route-dependent information.
        /// </para>
        /// <para>
        /// <strong>Important:</strong> Remember to unsubscribe from this event in the component's
        /// Dispose method to prevent memory leaks.
        /// </para>
        /// </remarks>
        /// <example>
        /// Subscribe to route changes:
        /// <code>
        /// protected override void OnInitialized()
        /// {
        ///     RouterState.OnRouteChanged += HandleRouteChange;
        /// }
        /// 
        /// private void HandleRouteChange(RouteMatch? match)
        /// {
        ///     // React to route change
        ///     StateHasChanged();
        /// }
        /// 
        /// public void Dispose()
        /// {
        ///     RouterState.OnRouteChanged -= HandleRouteChange;
        /// }
        /// </code>
        /// </example>
        public event Action<RouteMatch?>? OnRouteChanged;

        /// <summary>
        /// Gets the current route match containing all route information.
        /// </summary>
        /// <value>
        /// The current <see cref="RouteMatch"/> if a route is matched, or null if no route matches the current URL.
        /// </value>
        /// <remarks>
        /// This property provides access to the complete route match including the route configuration,
        /// extracted parameters, query values, and nested child matches. It is updated automatically
        /// by the Router component during navigation.
        /// </remarks>
        public RouteMatch? CurrentRoute { get; private set; }

        /// <summary>
        /// Gets the current URL path without query string or fragment.
        /// </summary>
        /// <value>
        /// A string representing the current path (e.g., "/users/123"). Defaults to "/" if no navigation has occurred.
        /// </value>
        /// <remarks>
        /// This normalized path can be used for path-based conditional logic, such as highlighting
        /// active navigation items or determining which layout to use.
        /// </remarks>
        public string CurrentPath { get; private set; } = "/";

        /// <summary>
        /// Updates the current route state and notifies all subscribers.
        /// </summary>
        /// <param name="route">The new route match, or null if no route matched.</param>
        /// <param name="path">The new current path.</param>
        /// <remarks>
        /// <para>
        /// This method is called internally by the Router component during navigation. It should not
        /// typically be called by application code.
        /// </para>
        /// <para>
        /// After updating the state, this method raises the <see cref="OnRouteChanged"/> event to
        /// notify all subscribers of the navigation.
        /// </para>
        /// </remarks>
        public void SetCurrentRoute(RouteMatch? route, string path)
        {
            CurrentPath = path;
            CurrentRoute = route;
            OnRouteChanged?.Invoke(route);
        }

        /// <summary>
        /// Retrieves a route parameter value by key.
        /// </summary>
        /// <param name="key">The parameter name as defined in the route pattern (without the ':' prefix).</param>
        /// <returns>
        /// The URL-decoded parameter value if found, or null if the parameter doesn't exist in the current route hierarchy.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method searches for the parameter in the current route and all child routes in the hierarchy.
        /// Parameters defined at any level of a nested route structure are accessible.
        /// </para>
        /// <para>
        /// Parameter values are returned as URL-decoded strings. Applications should parse them to appropriate
        /// types (int, Guid, etc.) and handle parsing errors gracefully.
        /// </para>
        /// </remarks>
        /// <example>
        /// Retrieve and parse route parameters:
        /// <code>
        /// // For route pattern "/users/:id"
        /// string? userIdStr = RouterState.GetParam("id");
        /// if (int.TryParse(userIdStr, out int userId))
        /// {
        ///     // Use userId
        /// }
        /// 
        /// // For route pattern "/posts/:slug"
        /// string? slug = RouterState.GetParam("slug");
        /// </code>
        /// </example>
        public string? GetParam(string key)
        {
            // Check current route params
            if (CurrentRoute?.Params.TryGetValue(key, out string? value) == true)
            {
                return value;
            }

            // Check child route params
            RouteMatch? child = CurrentRoute?.Child;
            while (child != null)
            {
                if (child.Params.TryGetValue(key, out value))
                {
                    return value;
                }
                child = child.Child;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a query string parameter value by key.
        /// </summary>
        /// <param name="key">The query parameter name.</param>
        /// <returns>
        /// The URL-decoded query parameter value if found, or null if the parameter doesn't exist in the query string.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Query parameters are extracted from the URL query string (the part after '?'). Multiple parameters
        /// are separated by '&amp;'. This method returns the value for a single parameter.
        /// </para>
        /// <para>
        /// Values are returned as URL-decoded strings. Applications should parse them to appropriate types
        /// and provide sensible defaults for missing or invalid values.
        /// </para>
        /// </remarks>
        /// <example>
        /// Retrieve query parameters:
        /// <code>
        /// // For URL "/search?q=blazor&amp;page=2"
        /// string? searchQuery = RouterState.GetQuery("q");        // Returns "blazor"
        /// string? pageStr = RouterState.GetQuery("page");          // Returns "2"
        /// 
        /// int page = int.TryParse(pageStr, out int p) ? p : 1;   // Default to 1 if invalid
        /// </code>
        /// </example>
        public string? GetQuery(string key)
        {
            if (CurrentRoute?.Query.TryGetValue(key, out string? value) == true)
            {
                return value;
            }
            return null;
        }
    }
}