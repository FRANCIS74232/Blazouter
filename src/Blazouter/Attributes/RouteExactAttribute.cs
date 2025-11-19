namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies that this route should match the URL exactly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of exact route matching on component classes.
    /// By default, routes use flexible matching: a route path like "/users" will match both "/users"
    /// (exact) and "/users/123" (partial). When exact matching is enabled, the route only matches
    /// when the URL path exactly corresponds to the route pattern.
    /// </para>
    /// <para>
    /// Exact matching is particularly important in these scenarios:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Root routes:</strong> "/" should only match the homepage, not all paths</description></item>
    /// <item><description><strong>List vs. detail views:</strong> Differentiate "/products" (list) from "/products/123" (detail)</description></item>
    /// <item><description><strong>Nested routing:</strong> Control whether parent routes render for child URLs</description></item>
    /// <item><description><strong>Default child routes:</strong> Match only when no child matches</description></item>
    /// <item><description><strong>Conflicting patterns:</strong> Disambiguate similar route patterns</description></item>
    /// </list>
    /// <para>
    /// <strong>Default Behavior Without Exact:</strong> For routes with children, the router can match
    /// partially, allowing the parent component to render while determining which child to display.
    /// For routes without children, matching is implicitly exact even without this attribute.
    /// </para>
    /// <para>
    /// <strong>Common Mistake:</strong> Forgetting to set exact=true on the root route ("/") causes it
    /// to match every URL since all paths start with "/". Always use [RouteExact(true)] on root routes.
    /// </para>
    /// </remarks>
    /// <example>
    /// Basic exact matching for a products list:
    /// <code>
    /// [Route("/products")]
    /// [RouteExact(true)]
    /// public partial class ProductsPage : ComponentBase
    /// {
    ///     // This route only matches exactly "/products", not "/products/123"
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Root route with exact matching (critical!):
    /// <code>
    /// [Route("/")]
    /// [RouteExact(true)]
    /// public class HomePage : ComponentBase
    /// {
    ///     // Matches only "/" not "/about" or "/products"
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Parent-child route distinction:
    /// <code>
    /// // Parent route without exact - can match "/users" and "/users/123"
    /// [Route("/users")]
    /// public class UsersLayout : ComponentBase
    /// {
    ///     // Contains RouterOutlet for children
    /// }
    /// 
    /// // Child route for list - exact match only
    /// [Route("/users")]
    /// [RouteExact(true)]
    /// public class UsersList : ComponentBase
    /// {
    ///     // Shows only when exactly "/users"
    /// }
    /// 
    /// // Child route for detail
    /// [Route("/users/:id")]
    /// public class UserDetail : ComponentBase
    /// {
    ///     // Shows when "/users/123"
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteExactAttribute"/> class.
    /// </remarks>
    /// <param name="exact">Whether the route path must match the URL exactly.</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteExactAttribute(bool exact = true) : Attribute
    {
        /// <summary>
        /// Gets a value indicating whether this route should match the URL exactly.
        /// </summary>
        /// <value>
        /// true if the route path must match the URL exactly; false to allow partial matches.
        /// </value>
        public bool Exact { get; } = exact;
    }
}