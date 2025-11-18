using Microsoft.AspNetCore.Components;

namespace Blazouter.Components
{
    /// <summary>
    /// A navigation link component that automatically applies an active class when its route matches the current URL.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterLink provides an enhanced anchor element specifically designed for Blazouter navigation.
    /// It automatically detects when its href matches the current route and applies an active CSS class,
    /// making it easy to highlight the current page in navigation menus.
    /// </para>
    /// <para>
    /// Key features:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Automatic active state detection based on current route</description></item>
    /// <item><description>Support for exact and prefix matching</description></item>
    /// <item><description>Client-side navigation (no page reload by default)</description></item>
    /// <item><description>Standard anchor element attributes support</description></item>
    /// <item><description>Reactive updates when route changes</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Basic navigation link:
    /// <code>
    /// &lt;RouterLink Href="/about" ActiveClass="nav-active"&gt;
    ///     About Us
    /// &lt;/RouterLink&gt;
    /// </code>
    /// </example>
    /// <example>
    /// Navigation menu with exact matching for home:
    /// <code>
    /// &lt;nav&gt;
    ///     &lt;RouterLink Href="/" Exact="true" ActiveClass="active"&gt;Home&lt;/RouterLink&gt;
    ///     &lt;RouterLink Href="/products" ActiveClass="active"&gt;Products&lt;/RouterLink&gt;
    ///     &lt;RouterLink Href="/about" ActiveClass="active"&gt;About&lt;/RouterLink&gt;
    /// &lt;/nav&gt;
    /// </code>
    /// </example>
    public partial class RouterLink
    {
        /// <summary>
        /// Gets or sets the target path to navigate to when the link is clicked.
        /// </summary>
        /// <value>
        /// A string representing the relative or absolute path. Defaults to "/" if not specified.
        /// </value>
        /// <remarks>
        /// The href can include query strings (e.g., "/search?q=blazor") and will be properly
        /// handled during navigation. The path is used both for navigation and for determining
        /// the active state of the link.
        /// </remarks>
        [Parameter]
        public string Href { get; set; } = "/";

        /// <summary>
        /// Gets or sets the content to display inside the link element.
        /// </summary>
        /// <value>
        /// A <see cref="RenderFragment"/> containing the link's content (text, icons, etc.).
        /// </value>
        /// <remarks>
        /// This can contain any valid Razor markup, including text, HTML elements, or other components.
        /// </remarks>
        /// <example>
        /// <code>
        /// &lt;RouterLink Href="/profile"&gt;
        ///     &lt;i class="icon-user"&gt;&lt;/i&gt;
        ///     &lt;span&gt;My Profile&lt;/span&gt;
        /// &lt;/RouterLink&gt;
        /// </code>
        /// </example>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the base CSS class(es) to apply to the link element.
        /// </summary>
        /// <value>
        /// A string containing one or more CSS class names separated by spaces, or null.
        /// </value>
        /// <remarks>
        /// These classes are always applied to the link, regardless of active state. The active class
        /// is added in addition to these when the route matches.
        /// </remarks>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// Gets or sets the CSS class to add when the link's route is currently active.
        /// </summary>
        /// <value>
        /// A string containing the CSS class name. Defaults to "active".
        /// </value>
        /// <remarks>
        /// <para>
        /// This class is dynamically added when the current route matches this link's href,
        /// based on the Exact parameter's value. This allows for visual feedback in navigation menus.
        /// </para>
        /// <para>
        /// The class is combined with any classes specified in the Class parameter.
        /// </para>
        /// </remarks>
        [Parameter]
        public string ActiveClass { get; set; } = "active";

        /// <summary>
        /// Gets or sets a value indicating whether the href must match the current path exactly to be considered active.
        /// </summary>
        /// <value>
        /// true to require an exact match; false (default) to match if the current path starts with the href.
        /// </value>
        /// <remarks>
        /// <para>
        /// When false (default), the link is active if the current path starts with the href. For example,
        /// href="/products" would be active for both "/products" and "/products/123".
        /// </para>
        /// <para>
        /// When true, the paths must match exactly. This is useful for home links ("/") which would
        /// otherwise always be active since all paths start with "/".
        /// </para>
        /// </remarks>
        [Parameter]
        public bool Exact { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to prevent the default anchor behavior and use client-side navigation.
        /// </summary>
        /// <value>
        /// true (default) to prevent page reload and use client-side navigation; false to allow normal anchor behavior.
        /// </value>
        /// <remarks>
        /// When true, clicking the link doesn't cause a full page reload. Instead, it uses Blazor's
        /// NavigationManager for client-side navigation, maintaining application state. Set to false
        /// only if you need to force a full page reload or navigate to external URLs.
        /// </remarks>
        [Parameter]
        public bool PreventDefault { get; set; } = true;

        /// <summary>
        /// Gets or sets additional attributes to apply to the rendered anchor element.
        /// </summary>
        /// <value>
        /// A dictionary of attribute names and values, or null.
        /// </value>
        /// <remarks>
        /// <para>
        /// This allows passing any standard HTML attributes to the anchor element that aren't
        /// explicitly defined as parameters, such as title, target, rel, aria-*, data-*, etc.
        /// </para>
        /// <para>
        /// The CaptureUnmatchedValues attribute means any attribute specified on the RouterLink
        /// component that doesn't match a defined parameter will be collected here and rendered
        /// on the final anchor element.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// &lt;RouterLink Href="/external" target="_blank" rel="noopener"&gt;
        ///     External Link
        /// &lt;/RouterLink&gt;
        /// </code>
        /// </example>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Gets the combined CSS classes including the active class if the route is active
        /// </summary>
        private string CombinedClass
        {
            get
            {
                List<string> classes = [];

                if (!string.IsNullOrEmpty(Class))
                {
                    classes.Add(Class);
                }

                if (IsActive())
                {
                    classes.Add(ActiveClass);
                }

                return string.Join(" ", classes);
            }
        }

        /// <summary>
        /// Initializes the RouterLink component and subscribes to route change events.
        /// </summary>
        /// <remarks>
        /// This method subscribes to the RouterStateService's OnRouteChanged event to receive
        /// notifications when navigation occurs, allowing the link to update its active state.
        /// </remarks>
        protected override void OnInitialized()
        {
            RouterState.OnRouteChanged += OnRouteChanged;
        }

        /// <summary>
        /// Handles route change events to update the link's active state.
        /// </summary>
        /// <param name="route">The new route match after navigation.</param>
        /// <remarks>
        /// When the route changes, this handler triggers a re-render to update the link's CSS classes
        /// based on whether the new route matches this link's href.
        /// Uses InvokeAsync to ensure the state change happens on the correct dispatcher thread,
        /// which is required in Blazor Server scenarios.
        /// </remarks>
        private void OnRouteChanged(Models.RouteMatch? route)
        {
            _ = InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Determines whether this link should be considered active based on the current route.
        /// </summary>
        /// <returns>
        /// true if the link's href matches the current route according to the Exact parameter; false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The matching logic depends on the Exact parameter:
        /// </para>
        /// <list type="bullet">
        /// <item><description>When Exact is true: The current path must equal the href exactly.</description></item>
        /// <item><description>When Exact is false: The current path must start with the href (prefix matching).</description></item>
        /// </list>
        /// <para>
        /// Query strings are ignored during matching - only the path portion of the URLs is compared.
        /// </para>
        /// </remarks>
        private bool IsActive()
        {
            string currentPath = RouterState.CurrentPath;
            string hrefPath = Href.Split('?')[0]; // Remove query string

            if (Exact)
            {
                return currentPath == hrefPath;
            }
            else
            {
                return currentPath.StartsWith(hrefPath);
            }
        }

        /// <summary>
        /// Handles click events on the link element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If PreventDefault is true (default), this method performs client-side navigation using
        /// NavigationManager, preventing the browser's default anchor behavior that would cause a
        /// full page reload.
        /// </para>
        /// <para>
        /// If PreventDefault is false, the browser's default behavior occurs (full page navigation).
        /// </para>
        /// </remarks>
        private void OnClick()
        {
            if (PreventDefault)
            {
                NavigationManager.NavigateTo(Href);
            }
        }

        /// <summary>
        /// Performs cleanup when the RouterLink component is disposed.
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
        /// disposing is true, this method can release managed resources in addition to unmanaged resources. Override
        /// this method to provide custom cleanup logic for derived classes.</remarks>
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