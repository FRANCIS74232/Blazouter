namespace Blazouter.Models
{
    /// <summary>
    /// Represents the result of matching a URL path against route configurations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouteMatch contains all information about a successfully matched route, including the route configuration,
    /// extracted parameters, query string values, and any nested child route matches. This object is created by
    /// the IRouteMatcherService when a URL is matched against the configured routes.
    /// </para>
    /// <para>
    /// The match includes both route parameters (from the URL path pattern, e.g., :id) and query string parameters.
    /// For nested routes, the Child property creates a hierarchy of matches representing the full route tree.
    /// </para>
    /// </remarks>
    public class RouteMatch
    {
        /// <summary>
        /// Gets or sets the route configuration that was matched.
        /// </summary>
        /// <value>
        /// The RouteConfig object that matched the current URL path.
        /// </value>
        /// <remarks>
        /// This provides access to all route configuration properties including component type, guards,
        /// children, transitions, and custom data.
        /// </remarks>
        public RouteConfig Route { get; set; } = null!;

        /// <summary>
        /// Gets or sets the route parameters extracted from the URL path.
        /// </summary>
        /// <value>
        /// A dictionary mapping parameter names to their extracted values from the URL.
        /// </value>
        /// <remarks>
        /// <para>
        /// Parameters are defined in the route path using the ':' prefix (e.g., "/users/:id").
        /// When a URL like "/users/123" matches this pattern, Params will contain { "id": "123" }.
        /// </para>
        /// <para>
        /// All parameter values are URL-decoded strings. Applications should parse them to appropriate types
        /// (e.g., int, Guid) and handle parsing errors gracefully.
        /// </para>
        /// </remarks>
        /// <example>
        /// For route pattern "/products/:category/:id" matching URL "/products/electronics/42":
        /// <code>
        /// Params["category"] // Returns "electronics"
        /// Params["id"]       // Returns "42"
        /// </code>
        /// </example>
        public Dictionary<string, string> Params { get; set; } = [];

        /// <summary>
        /// Gets or sets the query string parameters from the URL.
        /// </summary>
        /// <value>
        /// A dictionary mapping query parameter names to their values.
        /// </value>
        /// <remarks>
        /// <para>
        /// Query parameters are extracted from the URL query string (after the '?' character).
        /// For example, URL "/search?q=blazor&amp;page=2" will result in Query containing
        /// { "q": "blazor", "page": "2" }.
        /// </para>
        /// <para>
        /// All query values are URL-decoded strings. Applications should parse them to appropriate types
        /// and provide sensible defaults for missing or invalid values.
        /// </para>
        /// </remarks>
        public Dictionary<string, string> Query { get; set; } = [];

        /// <summary>
        /// Gets or sets the actual URL path that was matched.
        /// </summary>
        /// <value>
        /// The full URL path (without query string) that matched the route pattern.
        /// </value>
        /// <remarks>
        /// This is the normalized path used for matching, with leading/trailing slashes handled appropriately.
        /// It does not include the query string or fragment identifier.
        /// </remarks>
        public string MatchedPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the child route match for nested routes.
        /// </summary>
        /// <value>
        /// A RouteMatch object representing the matched child route, or null if no child route matched.
        /// </value>
        /// <remarks>
        /// <para>
        /// When routes have nested children, this property creates a hierarchy of matches. The parent match
        /// represents the outer route, and Child represents the inner route. This hierarchy continues for
        /// deeply nested routes.
        /// </para>
        /// <para>
        /// Components can access child route information to determine what content to display in their
        /// RouterOutlet component.
        /// </para>
        /// </remarks>
        /// <example>
        /// For URL "/products/electronics/42" with routes:
        /// - /products (parent)
        /// - /products/:category (child level 1)
        /// - /products/:category/:id (child level 2)
        /// 
        /// The RouteMatch hierarchy would be:
        /// <code>
        /// match.Route.Path           // "/products"
        /// match.Child.Route.Path     // ":category"
        /// match.Child.Child.Route.Path // ":id"
        /// </code>
        /// </example>
        public RouteMatch? Child { get; set; }

        /// <summary>
        /// Gets or sets the component type to render for this matched route.
        /// </summary>
        /// <value>
        /// The Type of the Blazor component to render, or null if not yet resolved (e.g., during lazy loading).
        /// </value>
        /// <remarks>
        /// <para>
        /// For routes using direct component reference (Route.Component), this is set immediately during matching.
        /// For routes using lazy loading (Route.ComponentLoader), this is set after the ComponentLoader function
        /// completes asynchronously.
        /// </para>
        /// <para>
        /// The Router component uses this type to create the DynamicComponent that renders the route's content.
        /// </para>
        /// </remarks>
        public Type? ComponentType { get; set; }
    }
}