using Blazouter.Models;

namespace Blazouter.Services
{
    /// <summary>
    /// Provides the default implementation of route matching logic for Blazouter.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouteMatcherService implements sophisticated pattern matching to find the best route match for a given URL.
    /// It handles complex routing scenarios including:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Dynamic parameters (e.g., :id, :slug) with automatic extraction and URL decoding</description></item>
    /// <item><description>Nested routes with hierarchical path matching</description></item>
    /// <item><description>Exact vs. partial matching for routes with and without children</description></item>
    /// <item><description>Query string parsing and parameter extraction</description></item>
    /// <item><description>Wildcard routes using the * symbol</description></item>
    /// <item><description>Route redirects</description></item>
    /// </list>
    /// <para>
    /// The matcher processes routes in order and returns the first match found. For nested routes, it recursively
    /// searches child routes to find the deepest matching route in the hierarchy.
    /// </para>
    /// <para>
    /// This implementation is thread-safe and registered as a singleton, making it efficient for concurrent use
    /// in server-side scenarios.
    /// </para>
    /// </remarks>
    /// <example>
    /// Route matching examples:
    /// <code>
    /// var matcher = new RouteMatcherService();
    /// 
    /// // Simple static route
    /// matcher.MatchRoute("/about", routes);
    /// 
    /// // Route with parameter
    /// matcher.MatchRoute("/users/123", routes);  // Extracts { "id": "123" }
    /// 
    /// // Route with query string
    /// matcher.MatchRoute("/search?q=blazor&amp;page=2", routes);  // Extracts query params
    /// 
    /// // Nested route
    /// matcher.MatchRoute("/products/electronics/42", routes);  // Matches parent and child
    /// </code>
    /// </example>
    public class RouteMatcherService : IRouteMatcherService
    {
        /// <summary>
        /// Matches a URL path against a collection of route configurations.
        /// </summary>
        /// <param name="path">
        /// The URL path to match, optionally including a query string (e.g., "/users/123?tab=profile").
        /// The path is automatically normalized by removing trailing slashes and parsing the query string.
        /// </param>
        /// <param name="routes">
        /// The collection of route configurations representing the application's route tree. Routes are
        /// processed in order, and the first matching route is returned.
        /// </param>
        /// <returns>
        /// A <see cref="RouteMatch"/> object containing the matched route configuration, extracted parameters,
        /// and query string values, or null if no route matches the path.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The matching process:
        /// </para>
        /// <list type="number">
        /// <item><description>Separates the path from the query string</description></item>
        /// <item><description>Normalizes the path (handles trailing slashes, empty paths become "/")</description></item>
        /// <item><description>Parses the query string into key-value pairs</description></item>
        /// <item><description>Recursively searches through routes and their children for a match</description></item>
        /// <item><description>Extracts dynamic parameters from matching route patterns</description></item>
        /// </list>
        /// <para>
        /// For routes with children, the algorithm attempts to find the deepest matching child route.
        /// If a parent route matches but no child matches, the parent is returned only if it matches exactly.
        /// </para>
        /// </remarks>
        public RouteMatch? MatchRoute(string path, List<RouteConfig> routes)
        {
            // Remove query string from path
            string pathWithoutQuery = path.Split('?')[0];
            string queryString = path.Contains('?') ? path.Split('?')[1] : string.Empty;
            Dictionary<string, string> query = ParseQueryString(queryString);

            // Normalize path
            pathWithoutQuery = pathWithoutQuery.TrimEnd('/');
            if (string.IsNullOrEmpty(pathWithoutQuery))
            {
                pathWithoutQuery = "/";
            }

            return MatchRouteRecursive(pathWithoutQuery, routes, query, "");
        }

        /// <summary>
        /// Recursively matches a route by traversing the route tree
        /// </summary>
        /// <param name="path">The normalized path to match</param>
        /// <param name="routes">The route configurations to search</param>
        /// <param name="query">Parsed query string parameters</param>
        /// <param name="parentPath">The parent route path for building nested paths</param>
        /// <returns>The matched route with parameters, or null if no match found</returns>
        private RouteMatch? MatchRouteRecursive(string path, List<RouteConfig> routes, Dictionary<string, string> query, string parentPath)
        {
            foreach (RouteConfig route in routes)
            {
                // Build the full route path
                string routePath = BuildRoutePath(parentPath, route.Path);

                // Handle redirect
                if (!string.IsNullOrEmpty(route.RedirectTo))
                {
                    if (PathMatches(path, routePath, out Dictionary<string, string>? redirectParams))
                    {
                        return new RouteMatch
                        {
                            Route = route,
                            Params = redirectParams,
                            Query = query,
                            MatchedPath = route.RedirectTo
                        };
                    }
                    continue;
                }

                // Try to match this route
                if (PathMatches(path, routePath, out Dictionary<string, string>? parameters) || PathMatchesPartially(path, routePath, out parameters))
                {
                    RouteMatch match = new()
                    {
                        Route = route,
                        Query = query,
                        MatchedPath = path,
                        Params = parameters,
                        ComponentType = route.Component
                    };

                    // Check for exact match requirement
                    if (route.Exact && !PathMatchesExactly(path, routePath))
                    {
                        continue;
                    }

                    // Check for nested routes
                    if (route.Children.Count != 0)
                    {
                        // Try to match child routes
                        foreach (RouteConfig childRoute in route.Children)
                        {
                            string childPath = BuildRoutePath(routePath, childRoute.Path);

                            if (PathMatches(path, childPath, out Dictionary<string, string>? childParams))
                            {
                                RouteMatch childMatch = new()
                                {
                                    Query = query,
                                    MatchedPath = path,
                                    Route = childRoute,
                                    Params = childParams,
                                    ComponentType = childRoute.Component
                                };

                                match.Child = childMatch;
                                return match;
                            }
                        }

                        // If we have children but none matched, only return this match if path exactly matches parent
                        if (PathMatchesExactly(path, routePath))
                        {
                            return match;
                        }
                    }
                    else
                    {
                        // No children, return this match if it's exact
                        if (PathMatchesExactly(path, routePath))
                        {
                            return match;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Builds a full route path by combining parent and child paths
        /// </summary>
        /// <param name="parent">The parent route path</param>
        /// <param name="child">The child route path</param>
        /// <returns>The combined route path with proper slash handling</returns>
        private string BuildRoutePath(string parent, string child)
        {
            if (string.IsNullOrEmpty(parent))
            {
                return child.StartsWith('/') ? child : "/" + child;
            }

            parent = parent.TrimEnd('/');
            child = child.TrimStart('/');

            if (string.IsNullOrEmpty(child))
            {
                return parent;
            }

            return parent + "/" + child;
        }

        /// <summary>
        /// Checks if an actual path matches a route pattern exactly (same number of segments)
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser</param>
        /// <param name="routePath">The route pattern to match against (may contain parameters like :id)</param>
        /// <param name="parameters">Output dictionary containing extracted route parameters</param>
        /// <returns>True if the path matches exactly, false otherwise</returns>
        private bool PathMatches(string actualPath, string routePath, out Dictionary<string, string> parameters)
        {
            parameters = [];

            // Normalize paths
            actualPath = actualPath.Trim('/');
            routePath = routePath.Trim('/');

            if (string.IsNullOrEmpty(actualPath))
            {
                actualPath = "";
            }

            if (string.IsNullOrEmpty(routePath))
            {
                routePath = "";
            }

            // Split into segments
            string[] actualSegments = string.IsNullOrEmpty(actualPath) ? [] : actualPath.Split('/');
            string[] routeSegments = string.IsNullOrEmpty(routePath) ? [] : routePath.Split('/');

            // Must have same number of segments
            if (actualSegments.Length != routeSegments.Length)
            {
                return false;
            }

            // Match each segment
            for (int i = 0; i < routeSegments.Length; i++)
            {
                string routeSegment = routeSegments[i];
                string actualSegment = actualSegments[i];

                if (routeSegment.StartsWith(':'))
                {
                    // Dynamic parameter
                    string paramName = routeSegment[1..];
                    parameters[paramName] = Uri.UnescapeDataString(actualSegment);
                }
                else if (routeSegment == "*")
                {
                    // Wildcard - matches anything
                    return true;
                }
                else if (routeSegment != actualSegment)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if an actual path partially matches a route pattern (for nested routes)
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser</param>
        /// <param name="routePath">The route pattern to match against</param>
        /// <param name="parameters">Output dictionary containing extracted route parameters</param>
        /// <returns>True if the path matches the route pattern partially, false otherwise</returns>
        private bool PathMatchesPartially(string actualPath, string routePath, out Dictionary<string, string> parameters)
        {
            parameters = [];

            // Normalize paths
            actualPath = actualPath.Trim('/');
            routePath = routePath.Trim('/');

            // Split into segments
            string[] actualSegments = string.IsNullOrEmpty(actualPath) ? [] : actualPath.Split('/');
            string[] routeSegments = string.IsNullOrEmpty(routePath) ? [] : routePath.Split('/');

            // Actual path must be at least as long as route path
            if (actualSegments.Length < routeSegments.Length)
            {
                return false;
            }

            // Match each segment of the route
            for (int i = 0; i < routeSegments.Length; i++)
            {
                string routeSegment = routeSegments[i];
                string actualSegment = actualSegments[i];

                if (routeSegment.StartsWith(':'))
                {
                    // Dynamic parameter
                    string paramName = routeSegment[1..];
                    parameters[paramName] = Uri.UnescapeDataString(actualSegment);
                }
                else if (routeSegment != actualSegment)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if an actual path matches a route pattern exactly in terms of structure and segment count
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser</param>
        /// <param name="routePath">The route pattern to match against</param>
        /// <returns>True if the path structure matches exactly, false otherwise</returns>
        private bool PathMatchesExactly(string actualPath, string routePath)
        {
            actualPath = actualPath.Trim('/');
            routePath = routePath.Trim('/');

            string[] actualSegments = string.IsNullOrEmpty(actualPath) ? [] : actualPath.Split('/');
            string[] routeSegments = string.IsNullOrEmpty(routePath) ? [] : routePath.Split('/');

            if (actualSegments.Length != routeSegments.Length)
            {
                return false;
            }

            for (int i = 0; i < routeSegments.Length; i++)
            {
                string routeSegment = routeSegments[i];
                string actualSegment = actualSegments[i];

                if (!routeSegment.StartsWith(':') && routeSegment != actualSegment)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Parses a URL query string into a dictionary of key-value pairs
        /// </summary>
        /// <param name="queryString">The query string to parse (without the leading '?')</param>
        /// <returns>A dictionary containing the parsed query parameters</returns>
        private Dictionary<string, string> ParseQueryString(string queryString)
        {
            Dictionary<string, string> result = [];

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return result;
            }

            string[] pairs = queryString.Split('&');
            foreach (string pair in pairs)
            {
                string[] keyValue = pair.Split('=');
                if (keyValue.Length >= 2)
                {
                    string key = Uri.UnescapeDataString(keyValue[0]);
                    string value = Uri.UnescapeDataString(keyValue[1]);
                    result[key] = value;
                }
            }

            return result;
        }
    }
}