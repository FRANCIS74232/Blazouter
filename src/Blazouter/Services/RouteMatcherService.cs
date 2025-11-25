using Blazouter.Interfaces;
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
        /// Recursively matches a route by traversing the route tree to find the best match for the given path.
        /// </summary>
        /// <param name="path">The normalized path to match (without query string, with leading slash).</param>
        /// <param name="routes">The collection of route configurations to search at the current level.</param>
        /// <param name="query">Parsed query string parameters from the original URL.</param>
        /// <param name="parentPath">The accumulated parent route path for building complete nested paths.</param>
        /// <returns>
        /// A <see cref="RouteMatch"/> object containing the matched route and all extracted parameters,
        /// or null if no route in the collection matches the path.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method implements the core routing algorithm for Blazouter. It performs a depth-first
        /// search through the route tree, trying to match the URL path against each route's pattern
        /// and its children recursively.
        /// </para>
        /// <para>
        /// The matching algorithm works as follows:
        /// </para>
        /// <list type="number">
        /// <item><description>Iterate through routes in order (first match wins)</description></item>
        /// <item><description>Build the complete route path by combining parent and child paths</description></item>
        /// <item><description>Check for redirects - if found, return immediately with redirect info</description></item>
        /// <item><description>Try to match the path against the route pattern (exact or partial)</description></item>
        /// <item><description>If route has Exact=true, verify it's an exact match</description></item>
        /// <item><description>If route has children, recursively search them for a deeper match</description></item>
        /// <item><description>Return the deepest match found, or parent if no child matches</description></item>
        /// </list>
        /// <para>
        /// For nested routes, the method creates a hierarchy of RouteMatch objects linked through the
        /// Child property, representing the complete matched path from root to leaf.
        /// </para>
        /// </remarks>
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
        /// Builds a full route path by combining parent and child paths with proper slash normalization.
        /// </summary>
        /// <param name="parent">The parent route path (may be empty for root-level routes).</param>
        /// <param name="child">The child route path to append to the parent.</param>
        /// <returns>
        /// The combined route path with normalized slashes, ensuring proper path formation.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method handles various edge cases in path construction:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Empty parent: Returns child with leading slash (e.g., "" + "users" → "/users")</description></item>
        /// <item><description>Empty child: Returns parent as-is (e.g., "/users" + "" → "/users")</description></item>
        /// <item><description>Both non-empty: Combines with single slash (e.g., "/users" + ":id" → "/users/:id")</description></item>
        /// <item><description>Trailing/leading slashes: Normalized to prevent double slashes</description></item>
        /// </list>
        /// <para>
        /// Examples of path building:
        /// </para>
        /// <code>
        /// BuildRoutePath("", "users")        → "/users"
        /// BuildRoutePath("/", "users")       → "/users"
        /// BuildRoutePath("/users", ":id")    → "/users/:id"
        /// BuildRoutePath("/users/", ":id")   → "/users/:id"
        /// BuildRoutePath("/api", "v1/users") → "/api/v1/users"
        /// </code>
        /// </remarks>
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
        /// Checks if an actual path matches a route pattern exactly with the same number of segments.
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser (e.g., "/users/123").</param>
        /// <param name="routePath">The route pattern to match against (e.g., "/users/:id").</param>
        /// <param name="parameters">
        /// When this method returns true, contains a dictionary of extracted route parameters.
        /// Parameter names (keys) do not include the ':' prefix. Values are URL-decoded.
        /// </param>
        /// <returns>
        /// true if the actual path matches the route pattern exactly (same number of segments,
        /// all static segments match, dynamic parameters extracted); false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method performs strict segment-by-segment matching with these rules:
        /// </para>
        /// <list type="bullet">
        /// <item><description><strong>Segment count:</strong> Must be identical (e.g., "/a/b" matches "/users/:id" but not "/users")</description></item>
        /// <item><description><strong>Static segments:</strong> Must match exactly and case-sensitively</description></item>
        /// <item><description><strong>Dynamic parameters:</strong> Segments starting with ':' match any value</description></item>
        /// <item><description><strong>Wildcards:</strong> '*' segment matches any single segment</description></item>
        /// <item><description><strong>Root path:</strong> "/" is treated as zero segments, matching empty patterns</description></item>
        /// </list>
        /// <para>
        /// Examples of matching:
        /// </para>
        /// <code>
        /// PathMatches("/users/123", "/users/:id", out var p)     → true,  p["id"] = "123"
        /// PathMatches("/users/123/edit", "/users/:id", out var p) → false (too many segments)
        /// PathMatches("/users", "/users/:id", out var p)          → false (too few segments)
        /// PathMatches("/users/123", "/posts/:id", out var p)      → false (static mismatch)
        /// PathMatches("/api/v1/users", "/api/*/users", out var p) → true  (wildcard matches "v1")
        /// PathMatches("/", "/", out var p)                        → true  (root matches root)
        /// </code>
        /// <para>
        /// Parameter values are automatically URL-decoded, so encoded characters like %20 (space)
        /// are converted to their actual characters.
        /// </para>
        /// </remarks>
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
        /// Checks if an actual path partially matches a route pattern, used for parent routes with children.
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser (e.g., "/users/123/profile").</param>
        /// <param name="routePath">The route pattern to match against (e.g., "/users/:id").</param>
        /// <param name="parameters">
        /// When this method returns true, contains a dictionary of extracted route parameters from
        /// the matched segments. Parameter names (keys) do not include the ':' prefix. Values are URL-decoded.
        /// </param>
        /// <returns>
        /// true if the actual path starts with segments that match the route pattern; false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Partial matching is essential for nested routing. It allows parent routes to match when the URL
        /// has additional segments that will be matched by child routes. Unlike <see cref="PathMatches"/>,
        /// this method only requires that the beginning of the path matches the route pattern.
        /// </para>
        /// <para>
        /// The actual path must have at least as many segments as the route pattern, but can have more.
        /// All route pattern segments must match their corresponding actual path segments.
        /// </para>
        /// <para>
        /// Examples of partial matching:
        /// </para>
        /// <code>
        /// // Parent route pattern: "/users/:id"
        /// PathMatchesPartially("/users/123", "/users/:id", out var p)          → true,  p["id"] = "123"
        /// PathMatchesPartially("/users/123/profile", "/users/:id", out var p)  → true,  p["id"] = "123" (extra segment ignored)
        /// PathMatchesPartially("/users", "/users/:id", out var p)               → false (not enough segments)
        /// PathMatchesPartially("/posts/123", "/users/:id", out var p)           → false (static mismatch)
        /// 
        /// // The extra "/profile" segment would be matched by a child route
        /// </code>
        /// <para>
        /// This method is used in combination with child route matching. When a parent route partially
        /// matches, the routing algorithm continues to search the parent's children for a complete match
        /// of the remaining path segments.
        /// </para>
        /// </remarks>
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
        /// Checks if an actual path matches a route pattern exactly in terms of structure and segment count,
        /// used for enforcing exact matching requirements.
        /// </summary>
        /// <param name="actualPath">The actual URL path from the browser (e.g., "/users/123").</param>
        /// <param name="routePath">The route pattern to match against (e.g., "/users/:id").</param>
        /// <returns>
        /// true if the paths have the same number of segments and all static segments match exactly;
        /// false otherwise.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is used to enforce exact matching when a route has Exact=true in its configuration.
        /// It verifies that the path structure is identical without extracting or returning parameters
        /// (unlike <see cref="PathMatches"/>).
        /// </para>
        /// <para>
        /// The validation logic:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Both paths must have identical segment counts</description></item>
        /// <item><description>Static segments must match exactly and case-sensitively</description></item>
        /// <item><description>Dynamic parameter positions (:param) are accepted as matches</description></item>
        /// <item><description>No parameter extraction is performed</description></item>
        /// </list>
        /// <para>
        /// This is faster than PathMatches since it doesn't extract parameters, making it ideal for
        /// the quick validation check needed when a route specifies Exact=true.
        /// </para>
        /// <para>
        /// Examples:
        /// </para>
        /// <code>
        /// PathMatchesExactly("/users/123", "/users/:id")      → true
        /// PathMatchesExactly("/users/123/edit", "/users/:id") → false (different lengths)
        /// PathMatchesExactly("/users", "/users/:id")          → false (different lengths)
        /// PathMatchesExactly("/posts/123", "/users/:id")      → false (static mismatch)
        /// PathMatchesExactly("/", "/")                        → true
        /// </code>
        /// </remarks>
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
        /// Parses a URL query string into a dictionary of key-value pairs.
        /// </summary>
        /// <param name="queryString">
        /// The query string to parse (without the leading '?' character). For example: "q=blazor&amp;page=2"
        /// </param>
        /// <returns>
        /// A dictionary containing the parsed query parameters. Keys and values are URL-decoded.
        /// Returns an empty dictionary if the query string is null, empty, or whitespace.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The parser handles standard URL query string format with these characteristics:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Parameters are separated by '&amp;' characters</description></item>
        /// <item><description>Each parameter is a key=value pair</description></item>
        /// <item><description>Both keys and values are URL-decoded (e.g., %20 becomes space)</description></item>
        /// <item><description>Parameters without '=' are ignored</description></item>
        /// <item><description>Duplicate keys: last value wins (standard dictionary behavior)</description></item>
        /// </list>
        /// <para>
        /// Examples:
        /// </para>
        /// <code>
        /// ParseQueryString("q=blazor&amp;page=2")              → { "q": "blazor", "page": "2" }
        /// ParseQueryString("search=hello%20world")         → { "search": "hello world" }
        /// ParseQueryString("")                             → { } (empty dictionary)
        /// ParseQueryString("q=blazor&amp;q=router")            → { "q": "router" } (last wins)
        /// ParseQueryString("filter=price&amp;sort=asc&amp;limit=10") → { "filter": "price", "sort": "asc", "limit": "10" }
        /// </code>
        /// <para>
        /// <strong>Note:</strong> This is a basic implementation. For more complex query string handling
        /// (array parameters, multiple values per key, etc.), consider using a specialized query string
        /// parsing library or enhancing this implementation.
        /// </para>
        /// </remarks>
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