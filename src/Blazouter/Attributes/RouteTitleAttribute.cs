namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies the title for a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of the route title on component classes.
    /// The title is stored as metadata in the RouteConfig and can be accessed through the
    /// RouterStateService. While the router doesn't automatically update the browser's document
    /// title, the title is available for application code to use for various purposes.
    /// </para>
    /// <para>
    /// Common use cases for route titles:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Browser page title:</strong> Update document.title for SEO and browser tabs</description></item>
    /// <item><description><strong>Breadcrumb navigation:</strong> Display hierarchical navigation paths</description></item>
    /// <item><description><strong>Navigation menus:</strong> Show human-readable route names</description></item>
    /// <item><description><strong>Page headers:</strong> Display dynamic page headings</description></item>
    /// <item><description><strong>Analytics:</strong> Track page views with readable names</description></item>
    /// <item><description><strong>Accessibility:</strong> Provide context for screen readers</description></item>
    /// </list>
    /// <para>
    /// <strong>Best Practices:</strong>
    /// </para>
    /// <list type="bullet">
    /// <item><description>Keep titles concise (50-60 characters for SEO)</description></item>
    /// <item><description>Use descriptive, user-friendly language</description></item>
    /// <item><description>Avoid technical jargon in user-facing titles</description></item>
    /// <item><description>Consider localization for multi-language applications</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Basic title for a simple page:
    /// <code>
    /// [Route("/about")]
    /// [RouteTitle("About Us")]
    /// public class AboutPage : ComponentBase
    /// {
    ///     // Component implementation
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Descriptive titles for nested routes:
    /// <code>
    /// [Route("/products")]
    /// [RouteTitle("Product Catalog")]
    /// public class ProductsLayout : ComponentBase { }
    /// 
    /// [Route("/products/:id")]
    /// [RouteTitle("Product Details")]
    /// public class ProductDetail : ComponentBase { }
    /// </code>
    /// </example>
    /// <example>
    /// Accessing and using route title in a component:
    /// <code>
    /// @inject RouterStateService RouterState
    /// 
    /// @code {
    ///     protected override void OnInitialized()
    ///     {
    ///         var title = RouterState.CurrentRoute?.Route.Title;
    ///         if (!string.IsNullOrEmpty(title))
    ///         {
    ///             // Update browser title
    ///             // Update breadcrumb component
    ///             // Log analytics event
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteTitleAttribute"/> class.
    /// </remarks>
    /// <param name="title">The title for the route.</param>
    /// <exception cref="ArgumentNullException">Thrown when title is null.</exception>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteTitleAttribute(string title) : Attribute
    {
        /// <summary>
        /// Gets the title for this route.
        /// </summary>
        /// <value>
        /// A string containing the route title. This should be a human-readable, descriptive title
        /// suitable for display in UI elements or browser chrome.
        /// </value>
        /// <remarks>
        /// The title is accessible through the RouteConfig.Title property and can be retrieved via
        /// RouterStateService.CurrentRoute.Route.Title. Applications are responsible for implementing
        /// any automatic title updates (e.g., setting document.title via JSInterop).
        /// </remarks>
        public string Title { get; } = title ?? throw new ArgumentNullException(nameof(title));
    }
}