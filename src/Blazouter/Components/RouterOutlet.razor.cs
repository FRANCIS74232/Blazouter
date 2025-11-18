using Blazouter.Models;
using Microsoft.AspNetCore.Components;

namespace Blazouter.Components
{
    /// <summary>
    /// A component that renders child routes within nested route hierarchies.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterOutlet is used in parent route components to display their matched child routes.
    /// It's the key component for implementing nested routing in Blazouter, similar to React Router's
    /// Outlet component.
    /// </para>
    /// <para>
    /// When a parent route matches and has children defined, the parent component renders normally
    /// and can include a RouterOutlet component where the child route's component should appear.
    /// The outlet automatically determines which child route matches and renders it.
    /// </para>
    /// <para>
    /// Key features:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Automatic child route detection and rendering</description></item>
    /// <item><description>Support for transition animations on child routes</description></item>
    /// <item><description>Component key generation to force re-render on parameter changes</description></item>
    /// <item><description>Reactive updates when navigation occurs</description></item>
    /// <item><description>Optional default content when no child matches</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Parent component with outlet:
    /// <code>
    /// @* UserLayout.razor *@
    /// &lt;div class="user-layout"&gt;
    ///     &lt;h1&gt;Users&lt;/h1&gt;
    ///     &lt;nav&gt;
    ///         &lt;RouterLink Href="/users"&gt;List&lt;/RouterLink&gt;
    ///         &lt;RouterLink Href="/users/new"&gt;New User&lt;/RouterLink&gt;
    ///     &lt;/nav&gt;
    ///     &lt;RouterOutlet&gt;
    ///         &lt;DefaultContent&gt;
    ///             &lt;p&gt;Select a user or create a new one&lt;/p&gt;
    ///         &lt;/DefaultContent&gt;
    ///     &lt;/RouterOutlet&gt;
    /// &lt;/div&gt;
    /// </code>
    /// </example>
    /// <example>
    /// Route configuration for nested routes:
    /// <code>
    /// new RouteConfig
    /// {
    ///     Path = "/users",
    ///     Component = typeof(UserLayout),
    ///     Children = new List&lt;RouteConfig&gt;
    ///     {
    ///         new RouteConfig { Path = "", Component = typeof(UserList), Exact = true },
    ///         new RouteConfig { Path = ":id", Component = typeof(UserDetail) },
    ///         new RouteConfig { Path = "new", Component = typeof(UserCreate) }
    ///     }
    /// }
    /// </code>
    /// </example>
    public partial class RouterOutlet
    {
        /// <summary>
        /// Gets or sets the content to display when no child route matches.
        /// </summary>
        /// <value>
        /// A <see cref="RenderFragment"/> containing the default content, or null to display nothing.
        /// </value>
        /// <remarks>
        /// <para>
        /// This content is shown when the parent route matches but no child route matches the current URL.
        /// It's useful for providing guidance or default information in the outlet area.
        /// </para>
        /// <para>
        /// For example, in a user management section with /users (list) and /users/:id (detail), the
        /// default content could appear at /users when no specific user is selected.
        /// </para>
        /// </remarks>
        [Parameter]
        public RenderFragment? DefaultContent { get; set; }

        /// <summary>
        /// Gets or sets the current route match from the parent component.
        /// </summary>
        /// <value>
        /// A <see cref="RouteMatch"/> object cascaded from the parent router, or null.
        /// </value>
        /// <remarks>
        /// This is a cascading parameter that receives the current route match from ancestor components.
        /// It's used internally to determine which child route should be rendered. Applications typically
        /// don't need to set this parameter explicitly.
        /// </remarks>
        [CascadingParameter]
        public RouteMatch? RouteMatch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether route transitions and animations are enabled for child routes.
        /// </summary>
        /// <value>
        /// true to enable transitions (default); false to disable transitions for this outlet.
        /// </value>
        /// <remarks>
        /// <para>
        /// When enabled, the outlet applies CSS transition classes based on each child route's Transition
        /// property, creating smooth visual effects when switching between child routes.
        /// </para>
        /// <para>
        /// This can be disabled per-outlet without affecting the main Router's transition settings,
        /// allowing fine-grained control over where animations appear in the application.
        /// </para>
        /// </remarks>
        [Parameter]
        public bool EnableTransitions { get; set; } = true;

        /// <summary>
        /// The matched child route
        /// </summary>
        private RouteMatch? _childMatch;

        /// <summary>
        /// The type of the child component
        /// </summary>
        private Type? _childComponentType;

        /// <summary>
        /// Parameters for the child component
        /// </summary>
        private Dictionary<string, object> _componentParameters = [];

        /// <summary>
        /// CSS class for route transitions
        /// </summary>
        private string _transitionClass = "";

        /// <summary>
        /// Unique key for the child component
        /// </summary>
        private string _componentKey = "";

        /// <summary>
        /// Initializes the RouterOutlet component and subscribes to route change events.
        /// </summary>
        /// <remarks>
        /// This method subscribes to route changes and performs the initial child route detection
        /// to display the appropriate component on first render.
        /// </remarks>
        protected override void OnInitialized()
        {
            RouterState.OnRouteChanged += OnRouteChanged;
            UpdateChildRoute();
        }

        /// <summary>
        /// Responds to parameter changes by updating the displayed child route.
        /// </summary>
        /// <remarks>
        /// Called whenever any parameter changes, ensuring the outlet displays the correct
        /// child component if the cascaded RouteMatch or other parameters change.
        /// </remarks>
        protected override void OnParametersSet()
        {
            UpdateChildRoute();
        }

        /// <summary>
        /// Handles route change events to update the displayed child component.
        /// </summary>
        /// <param name="route">The new route match after navigation.</param>
        /// <remarks>
        /// When navigation occurs, this handler re-evaluates which child route matches and
        /// triggers a re-render to display the updated component.
        /// Uses InvokeAsync to ensure the state change happens on the correct dispatcher thread,
        /// which is required in Blazor Server scenarios.
        /// </remarks>
        private void OnRouteChanged(RouteMatch? route)
        {
            _ = InvokeAsync(() =>
            {
                UpdateChildRoute();
                StateHasChanged();
            });
        }

        /// <summary>
        /// Updates the child route based on the current router state.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method:
        /// </para>
        /// <list type="number">
        /// <item><description>Retrieves the current route from RouterStateService</description></item>
        /// <item><description>Searches for a matching child route in the hierarchy</description></item>
        /// <item><description>Extracts the child component type and parameters</description></item>
        /// <item><description>Applies transition classes if enabled</description></item>
        /// <item><description>Generates a unique component key to force recreation on parameter changes</description></item>
        /// </list>
        /// <para>
        /// The component key ensures that when navigating between routes with the same component type
        /// but different parameters (e.g., /users/1 to /users/2), Blazor creates a new component instance
        /// rather than reusing the existing one, ensuring proper lifecycle method execution.
        /// </para>
        /// </remarks>
        private void UpdateChildRoute()
        {
            RouteMatch? currentRoute = RouterState.CurrentRoute;

            // Find the child match by traversing the route hierarchy
            _childMatch = FindChildMatch(currentRoute);

            if (_childMatch != null)
            {
                _childComponentType = _childMatch.ComponentType;

                // Build parameters for the child component - only include route data
                _componentParameters = [];

                // Add route data as parameters
                foreach (KeyValuePair<string, object> data in _childMatch.Route.Data)
                {
                    _componentParameters[data.Key] = data.Value;
                }

                // Set transition class
                if (EnableTransitions && _childMatch.Route.Transition != RouteTransition.None)
                {
                    string transitionName = _childMatch.Route.Transition.ToCssClass();
                    _transitionClass = $"transition-{transitionName}";
                }

                // Generate a unique key based on the route path and parameters to force component recreation when they change
                string paramKey = string.Join("_", _childMatch.Params.Select(p => $"{p.Key}={p.Value}"));
                _componentKey = $"{_childMatch.MatchedPath}_{paramKey}";
            }
            else
            {
                _childComponentType = null;
                _transitionClass = "";
                _componentKey = "";
            }
        }

        /// <summary>
        /// Finds the child route match from the given parent route match.
        /// </summary>
        /// <param name="match">The parent route match to search within.</param>
        /// <returns>
        /// The child <see cref="RouteMatch"/> if the parent has a matched child route, or null otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method traverses the route match hierarchy to find the immediate child of the current
        /// route. In nested routing scenarios, each RouteMatch can have a Child property pointing to
        /// the next level in the hierarchy.
        /// </para>
        /// <para>
        /// The method only returns the direct child, not deeper descendants. For deeply nested routes,
        /// each level uses its own RouterOutlet to display the next level's component.
        /// </para>
        /// </remarks>
        private RouteMatch? FindChildMatch(RouteMatch? match)
        {
            if (match == null)
            {
                return null;
            }

            // Check if this match has a child
            if (match.Child != null)
            {
                return match.Child;
            }

            return null;
        }

        /// <summary>
        /// Performs cleanup when the RouterOutlet component is disposed.
        /// </summary>
        /// <remarks>
        /// This method unsubscribes from the RouterStateService's OnRouteChanged event to prevent
        /// memory leaks and ensure proper garbage collection.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the object and optionally releases the managed resources.
        /// </summary>
        /// <remarks>This method is called by the public Dispose() method and the finalizer. When
        /// disposing is true, this method can dispose managed resources in addition to unmanaged resources. Override
        /// this method to release resources specific to the derived class.</remarks>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                RouterState.OnRouteChanged -= OnRouteChanged;
            }
        }
    }
}