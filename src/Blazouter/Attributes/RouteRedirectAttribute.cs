namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies that this route should redirect to another path.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of route redirects on component classes.
    /// When a route with this attribute is matched, navigation will automatically redirect to the
    /// specified path without rendering the component. The redirect occurs immediately during the
    /// routing phase, before any component initialization or guard execution.
    /// </para>
    /// <para>
    /// <strong>Important:</strong> When using RouteRedirect, the component itself will not be rendered,
    /// and other route attributes on the same component (RouteTransition, RouteGuard, RouteLayout, etc.)
    /// will be ignored since they apply to component rendering, which never occurs for redirect routes.
    /// </para>
    /// <para>
    /// Common use cases for route redirects:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>URL migration:</strong> Redirect old URLs to new ones after restructuring</description></item>
    /// <item><description><strong>Route aliases:</strong> Provide multiple paths to the same resource</description></item>
    /// <item><description><strong>Default routes:</strong> Redirect root or base paths to specific pages</description></item>
    /// <item><description><strong>Shortened URLs:</strong> Create simple aliases for complex routes</description></item>
    /// <item><description><strong>Legacy support:</strong> Maintain backwards compatibility with old URL structures</description></item>
    /// </list>
    /// <para>
    /// <strong>Performance Note:</strong> Redirects are fast and lightweight since no component is created
    /// or rendered. However, avoid redirect chains (A→B→C) as they result in multiple navigation events.
    /// </para>
    /// <para>
    /// <strong>SEO Consideration:</strong> For public websites, consider implementing 301 redirects at the
    /// server level for better SEO. Client-side redirects are not visible to all search engine crawlers.
    /// </para>
    /// </remarks>
    /// <example>
    /// Simple URL redirect for migration:
    /// <code>
    /// [Route("/old-path")]
    /// [RouteRedirect("/new-path")]
    /// public partial class OldPathRedirect : ComponentBase
    /// {
    ///     // This component won't be rendered; navigation redirects to /new-path
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Create short aliases for long URLs:
    /// <code>
    /// [Route("/docs")]
    /// [RouteRedirect("/documentation/getting-started")]
    /// public partial class DocsAlias : ComponentBase { }
    /// 
    /// [Route("/admin")]
    /// [RouteRedirect("/administration/dashboard")]
    /// public partial class AdminAlias : ComponentBase { }
    /// </code>
    /// </example>
    /// <example>
    /// Redirect root to default page:
    /// <code>
    /// [Route("/")]
    /// [RouteRedirect("/home")]
    /// public partial class RootRedirect : ComponentBase { }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteRedirectAttribute"/> class.
    /// </remarks>
    /// <param name="redirectPath">The target redirect path.</param>
    /// <exception cref="ArgumentNullException">Thrown when redirectPath is null.</exception>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteRedirectAttribute(string redirectPath) : Attribute
    {
        /// <summary>
        /// Gets the path to redirect to when this route matches.
        /// </summary>
        /// <value>
        /// A string representing the target redirect path. Can be a relative path (e.g., "/new-path")
        /// or an absolute URL (e.g., "https://example.com/page").
        /// </value>
        /// <remarks>
        /// <para>
        /// The redirect path is passed directly to NavigationManager.NavigateTo(), which handles both
        /// relative and absolute URLs. For relative paths, navigation stays within the application.
        /// For absolute URLs, navigation may leave the application depending on the browser's behavior.
        /// </para>
        /// <para>
        /// Query strings and fragments can be included in the redirect path (e.g., "/search?q=term#results").
        /// </para>
        /// </remarks>
        public string RedirectPath { get; } = redirectPath ?? throw new ArgumentNullException(nameof(redirectPath));
    }
}