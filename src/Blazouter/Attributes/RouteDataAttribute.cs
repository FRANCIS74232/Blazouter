namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies custom data to associate with a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of route data on component classes. Route data provides
    /// a mechanism to attach arbitrary metadata or configuration values to routes without polluting the URL
    /// with query parameters. Multiple data attributes can be applied to add multiple key-value pairs.
    /// </para>
    /// <para>
    /// Route data is automatically passed to the component as parameters if the component declares matching
    /// properties with [Parameter] attributes. This enables clean separation of routing concerns from
    /// component implementation.
    /// </para>
    /// <para>
    /// Common use cases include:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Permission requirements for access control (e.g., "RequireAdmin", "RequiredRole")</description></item>
    /// <item><description>UI metadata (page icons, colors, sections)</description></item>
    /// <item><description>Analytics and tracking data</description></item>
    /// <item><description>Feature flags or conditional rendering settings</description></item>
    /// <item><description>Breadcrumb configuration</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Basic usage with permission flags:
    /// <code>
    /// [Route("/admin")]
    /// [RouteData("RequireAdmin", true)]
    /// [RouteData("Section", "Management")]
    /// [RouteData("Icon", "admin-icon")]
    /// public class AdminPage : ComponentBase
    /// {
    ///     [Parameter] public bool RequireAdmin { get; set; }
    ///     [Parameter] public string? Section { get; set; }
    ///     [Parameter] public string? Icon { get; set; }
    ///     
    ///     // Component implementation has access to route data
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Using route data for breadcrumb configuration:
    /// <code>
    /// [Route("/products/:category")]
    /// [RouteData("BreadcrumbLabel", "Products")]
    /// [RouteData("BreadcrumbParent", "/")]
    /// [RouteData("ShowInNav", true)]
    /// public class ProductCategoryPage : ComponentBase
    /// {
    ///     // Route data accessible via RouterStateService
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteDataAttribute"/> class.
    /// </remarks>
    /// <param name="key">The key for the data entry.</param>
    /// <param name="value">The value for the data entry.</param>
    /// <exception cref="ArgumentNullException">Thrown when key or value is null.</exception>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RouteDataAttribute(string key, object value) : Attribute
    {
        /// <summary>
        /// Gets the key for the data entry.
        /// </summary>
        /// <value>
        /// A string representing the data key. This key is used to store and retrieve the data value.
        /// Keys are case-sensitive and should follow standard naming conventions for consistency.
        /// </value>
        /// <remarks>
        /// The key name should match a component parameter name (including casing) if you want the value
        /// to be automatically injected into the component. Otherwise, access the data through the
        /// RouteMatch object in RouterStateService.
        /// </remarks>
        public string Key { get; } = key ?? throw new ArgumentNullException(nameof(key));

        /// <summary>
        /// Gets the value for the data entry.
        /// </summary>
        /// <value>
        /// An object containing the data value. Can be any serializable type including primitives,
        /// strings, complex objects, or collections.
        /// </value>
        /// <remarks>
        /// <para>
        /// The value is stored as an object reference, preserving its original type. When accessed
        /// as a component parameter, it maintains its type without requiring casting or conversion.
        /// </para>
        /// <para>
        /// For best practices, use simple types (string, int, bool) or immutable objects to avoid
        /// unintended side effects from shared route data instances.
        /// </para>
        /// </remarks>
        public object Value { get; } = value ?? throw new ArgumentNullException(nameof(value));
    }
}