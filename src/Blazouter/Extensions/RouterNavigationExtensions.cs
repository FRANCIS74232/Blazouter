using Blazouter.Services;
using Blazouter.Utilities;

namespace Blazouter.Extensions
{
    /// <summary>
    /// Extension methods for RouterNavigationService to simplify navigation with query strings.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These extensions provide convenient methods for building and navigating to URLs with query parameters
    /// using the fluent QueryStringBuilder API.
    /// </para>
    /// </remarks>
    public static class RouterNavigationExtensions
    {
        /// <summary>
        /// Navigates to a path with query parameters built using a fluent API.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="path">The path to navigate to (without query string).</param>
        /// <param name="buildQuery">A function that builds the query string using QueryStringBuilder.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <example>
        /// <code>
        /// NavService.NavigateToWithQuery("/search", q => q
        ///     .Add("term", "blazor")
        ///     .Add("page", 1)
        ///     .Add("active", true));
        /// </code>
        /// </example>
        public static void NavigateToWithQuery(
            this RouterNavigationService navigationService,
            string path,
            Func<QueryStringBuilder, QueryStringBuilder> buildQuery,
            bool forceLoad = false)
        {
            QueryStringBuilder builder = buildQuery(new QueryStringBuilder());
            string queryString = builder.Build();
            string fullPath = string.IsNullOrEmpty(queryString) ? path : $"{path}?{queryString}";
            navigationService.NavigateTo(fullPath, forceLoad);
        }

        /// <summary>
        /// Navigates to a path by updating specific query parameters while preserving others.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="routerState">The RouterStateService instance to get current query parameters.</param>
        /// <param name="path">The path to navigate to. If null, uses current path.</param>
        /// <param name="updateQuery">A function that updates the query parameters.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <remarks>
        /// <para>
        /// This method is useful for updating pagination, sorting, or filters without losing other query parameters.
        /// </para>
        /// <para>
        /// To replace existing parameter values, use the Set() method on the builder. To add multiple values
        /// for the same parameter, use Add(). To remove parameters, use Remove().
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Update only the page parameter, keep other parameters intact
        /// NavService.NavigateToWithUpdatedQuery(RouterState, null, q => q
        ///     .Set("page", currentPage + 1));
        /// 
        /// // Add multiple updates
        /// NavService.NavigateToWithUpdatedQuery(RouterState, null, q => q
        ///     .Set("updated", DateTime.Now)
        ///     .Set("version", 2));
        /// </code>
        /// </example>
        public static void NavigateToWithUpdatedQuery(
            this RouterNavigationService navigationService,
            RouterStateService routerState,
            string? path,
            Func<QueryStringBuilder, QueryStringBuilder> updateQuery,
            bool forceLoad = false)
        {
            // Get current query parameters
            Dictionary<string, string> currentParams = routerState.GetAllQueryParams();

            // Build new query string starting with current parameters
            QueryStringBuilder builder = new();
            foreach (KeyValuePair<string, string> kvp in currentParams)
            {
                builder.Add(kvp.Key, kvp.Value);
            }

            // Apply updates
            builder = updateQuery(builder);

            // Navigate
            string targetPath = path ?? routerState.CurrentPath;
            string queryString = builder.Build();
            string fullPath = string.IsNullOrEmpty(queryString) ? targetPath : $"{targetPath}?{queryString}";
            navigationService.NavigateTo(fullPath, forceLoad);
        }

        /// <summary>
        /// Navigates to a path, replacing all existing query parameters.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="path">The path to navigate to. If null, uses current path from the state.</param>
        /// <param name="buildQuery">A function that builds the new query string.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <remarks>
        /// Unlike NavigateToWithUpdatedQuery, this method replaces all query parameters instead of merging them.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Replace all query parameters with new ones
        /// NavService.NavigateToWithReplacedQuery("/products", q => q
        ///     .Add("category", "electronics")
        ///     .Add("sort", "price"));
        /// </code>
        /// </example>
        public static void NavigateToWithReplacedQuery(
            this RouterNavigationService navigationService,
            string path,
            Func<QueryStringBuilder, QueryStringBuilder> buildQuery,
            bool forceLoad = false)
        {
            QueryStringBuilder builder = buildQuery(new QueryStringBuilder());
            string queryString = builder.Build();
            string fullPath = string.IsNullOrEmpty(queryString) ? path : $"{path}?{queryString}";
            navigationService.NavigateTo(fullPath, forceLoad);
        }

        /// <summary>
        /// Navigates to the current path with one or more query parameters removed.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="routerState">The RouterStateService instance to get current state.</param>
        /// <param name="keysToRemove">The query parameter keys to remove.</param>
        /// <example>
        /// <code>
        /// // Remove the 'filter' parameter from the current URL
        /// NavService.NavigateToWithRemovedQuery(RouterState, "filter");
        /// 
        /// // Remove multiple parameters
        /// NavService.NavigateToWithRemovedQuery(RouterState, "filter", "sort");
        /// </code>
        /// </example>
        public static void NavigateToWithRemovedQuery(
            this RouterNavigationService navigationService,
            RouterStateService routerState,
            params string[] keysToRemove)
        {
            Dictionary<string, string> currentParams = routerState.GetAllQueryParams();
            QueryStringBuilder builder = new();

            // Add all parameters except the ones to remove
            foreach (KeyValuePair<string, string> kvp in currentParams.Where(p => !keysToRemove.Contains(p.Key)))
            {
                builder.Add(kvp.Key, kvp.Value);
            }

            string queryString = builder.Build();
            string path = routerState.CurrentPath;
            string fullPath = string.IsNullOrEmpty(queryString) ? path : $"{path}?{queryString}";
            navigationService.NavigateTo(fullPath);
        }

        /// <summary>
        /// Navigates to the current path with all query parameters removed.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="routerState">The RouterStateService instance to get current path.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <example>
        /// <code>
        /// // Clear all query parameters from the current URL
        /// NavService.NavigateToWithClearedQuery(RouterState);
        /// </code>
        /// </example>
        public static void NavigateToWithClearedQuery(
            this RouterNavigationService navigationService,
            RouterStateService routerState,
            bool forceLoad = false)
        {
            navigationService.NavigateTo(routerState.CurrentPath, forceLoad);
        }

        /// <summary>
        /// Navigates to a path with a single query parameter.
        /// </summary>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="path">The path to navigate to.</param>
        /// <param name="key">The query parameter key.</param>
        /// <param name="value">The query parameter value.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <remarks>
        /// This is a convenience method for simple cases with a single query parameter.
        /// </remarks>
        /// <example>
        /// <code>
        /// NavService.NavigateToWithSingleQuery("/user", "id", 123);
        /// // Navigates to: /user?id=123
        /// </code>
        /// </example>
        public static void NavigateToWithSingleQuery(
            this RouterNavigationService navigationService,
            string path,
            string key,
            string value,
            bool forceLoad = false)
        {
            string queryString = new QueryStringBuilder()
                .Add(key, value)
                .Build();
            string fullPath = $"{path}?{queryString}";
            navigationService.NavigateTo(fullPath, forceLoad);
        }

        /// <summary>
        /// Navigates to a path with a single typed query parameter.
        /// </summary>
        /// <typeparam name="T">The type of the query parameter value.</typeparam>
        /// <param name="navigationService">The RouterNavigationService instance.</param>
        /// <param name="path">The path to navigate to.</param>
        /// <param name="key">The query parameter key.</param>
        /// <param name="value">The query parameter value.</param>
        /// <param name="forceLoad">If true, forces a full page reload.</param>
        /// <remarks>
        /// This method supports int, long, decimal, double, bool, DateTime, Guid, and enum types.
        /// </remarks>
        /// <example>
        /// <code>
        /// NavService.NavigateToWithSingleQuery("/products", "page", 2);
        /// // Navigates to: /products?page=2
        /// 
        /// NavService.NavigateToWithSingleQuery("/items", "active", true);
        /// // Navigates to: /items?active=true
        /// </code>
        /// </example>
        public static void NavigateToWithSingleQuery<T>(
            this RouterNavigationService navigationService,
            string path,
            string key,
            T value,
            bool forceLoad = false)
        {
            QueryStringBuilder builder = new();

            // Handle different types
            switch (value)
            {
                case int intVal:
                    builder.Add(key, intVal);
                    break;
                case long longVal:
                    builder.Add(key, longVal);
                    break;
                case decimal decVal:
                    builder.Add(key, decVal);
                    break;
                case double dblVal:
                    builder.Add(key, dblVal);
                    break;
                case bool boolVal:
                    builder.Add(key, boolVal);
                    break;
                case DateTime dtVal:
                    builder.Add(key, dtVal);
                    break;
                case Guid guidVal:
                    builder.Add(key, guidVal);
                    break;
                case Enum enumVal:
                    builder.Add(key, enumVal.ToString());
                    break;
                default:
                    builder.Add(key, value?.ToString());
                    break;
            }

            string queryString = builder.Build();
            string fullPath = string.IsNullOrEmpty(queryString) ? path : $"{path}?{queryString}";
            navigationService.NavigateTo(fullPath, forceLoad);
        }
    }
}