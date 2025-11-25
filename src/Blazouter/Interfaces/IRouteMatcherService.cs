using Blazouter.Models;

namespace Blazouter.Interfaces
{
    /// <summary>
    /// Defines a contract for services that match URL paths to route configurations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The route matcher is responsible for analyzing URLs and finding matching route configurations
    /// from the application's route tree. It handles complex scenarios including:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Dynamic parameter extraction (e.g., /users/:id)</description></item>
    /// <item><description>Query string parsing</description></item>
    /// <item><description>Nested route matching</description></item>
    /// <item><description>Exact vs. partial matching</description></item>
    /// <item><description>Wildcard routes</description></item>
    /// </list>
    /// <para>
    /// Implementations of this interface must be thread-safe as they are registered as singletons
    /// and may be called concurrently in server-side scenarios.
    /// </para>
    /// </remarks>
    public interface IRouteMatcherService
    {
        /// <summary>
        /// Matches a URL path against a collection of route configurations.
        /// </summary>
        /// <param name="path">
        /// The URL path to match, including query string if present (e.g., "/users/123?tab=profile").
        /// </param>
        /// <param name="routes">
        /// The collection of route configurations to search. This represents the application's route tree.
        /// </param>
        /// <returns>
        /// A <see cref="RouteMatch"/> object containing the matched route and extracted parameters, or null
        /// if no route in the collection matches the given path.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The matching algorithm processes routes in the order they appear in the collection. The first
        /// route that matches the path is returned. For routes with children, the algorithm recursively
        /// searches child routes to find the deepest match.
        /// </para>
        /// <para>
        /// The path parameter can include a query string, which is automatically parsed and included in
        /// the returned RouteMatch's Query property.
        /// </para>
        /// <para>
        /// If no route matches, null is returned, allowing the Router component to display its NotFound content.
        /// </para>
        /// </remarks>
        /// <example>
        /// Example implementation usage:
        /// <code>
        /// var matcher = new RouteMatcherService();
        /// var routes = new List&lt;RouteConfig&gt;
        /// {
        ///     new RouteConfig { Path = "/users/:id", Component = typeof(UserDetail) }
        /// };
        /// 
        /// var match = matcher.MatchRoute("/users/123?tab=profile", routes);
        /// if (match != null)
        /// {
        ///     string userId = match.Params["id"];        // "123"
        ///     string tab = match.Query["tab"];           // "profile"
        /// }
        /// </code>
        /// </example>
        RouteMatch? MatchRoute(string path, List<RouteConfig> routes);
    }
}