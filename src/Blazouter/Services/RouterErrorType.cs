namespace Blazouter.Services
{
    /// <summary>
    /// Defines the types of errors that can occur during routing operations in Blazouter.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This enumeration categorizes routing errors to help applications handle different failure scenarios
    /// appropriately. Error handlers can use these types to implement specific recovery strategies, logging
    /// levels, or user messaging based on the nature of the failure.
    /// </para>
    /// <para>
    /// Each error type represents a distinct phase in the routing pipeline where failures can occur, from
    /// initial route matching through final component rendering. Understanding these types helps in debugging
    /// routing issues and implementing robust error handling.
    /// </para>
    /// </remarks>
    /// <example>
    /// Implementing type-specific error handling:
    /// <code>
    /// public class CustomErrorHandler : IRouterErrorHandler
    /// {
    ///     public Task&lt;bool&gt; HandleErrorAsync(Exception exception, RouterErrorContext context)
    ///     {
    ///         switch (context.ErrorType)
    ///         {
    ///             case RouterErrorType.ComponentLoading:
    ///                 // Log as warning - might be temporary network issue
    ///                 _logger.LogWarning(exception, "Component failed to load");
    ///                 return Task.FromResult(true); // Show error UI
    ///                 
    ///             case RouterErrorType.GuardExecution:
    ///                 // Log as info - expected in authorization scenarios
    ///                 _logger.LogInformation("Guard denied access");
    ///                 return Task.FromResult(false); // Don't show error, guard handles redirect
    ///                 
    ///             case RouterErrorType.Unknown:
    ///             default:
    ///                 // Log as error - unexpected failure
    ///                 _logger.LogError(exception, "Unexpected routing error");
    ///                 return Task.FromResult(true); // Show error UI
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public enum RouterErrorType
    {
        /// <summary>
        /// An unspecified or unexpected error occurred during routing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This error type indicates a failure that doesn't fit into other specific categories or represents
        /// an unexpected exception during the routing process. It serves as a catch-all for unusual scenarios
        /// that weren't anticipated during development.
        /// </para>
        /// <para>
        /// Common scenarios that result in Unknown errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Unhandled exceptions in application code during routing</description></item>
        /// <item><description>Infrastructure failures (out of memory, system errors)</description></item>
        /// <item><description>Errors in custom route matching logic</description></item>
        /// <item><description>Edge cases not covered by other error types</description></item>
        /// </list>
        /// <para>
        /// <strong>Recommended Action:</strong> Log these errors with full stack traces as they indicate
        /// unexpected behavior that may require code changes. Consider adding more specific error handling
        /// for recurring Unknown errors.
        /// </para>
        /// </remarks>
        Unknown,

        /// <summary>
        /// An error occurred during programmatic navigation operations.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Navigation errors occur when attempting to navigate to a route programmatically, such as through
        /// NavigationManager.NavigateTo or RouterNavigationService methods. These typically indicate issues
        /// with the target URL format or navigation state.
        /// </para>
        /// <para>
        /// Common causes of navigation errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Invalid URL format or malformed paths</description></item>
        /// <item><description>Navigation to non-existent routes</description></item>
        /// <item><description>Circular redirects creating infinite loops</description></item>
        /// <item><description>Navigation attempts during component disposal</description></item>
        /// <item><description>Browser navigation API failures</description></item>
        /// </list>
        /// <para>
        /// <strong>Recovery Strategy:</strong> Validate URLs before navigation, implement navigation guards
        /// to prevent invalid state transitions, and provide user feedback when navigation fails. Consider
        /// logging the attempted URL and current route state for debugging.
        /// </para>
        /// </remarks>
        Navigation,

        /// <summary>
        /// An error occurred during the route matching process.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Route matching errors happen when the route matcher service encounters problems while trying to
        /// match the current URL against configured routes. These are rare in production but can occur due
        /// to malformed route configurations or URL parsing issues.
        /// </para>
        /// <para>
        /// Typical causes of route matching errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Malformed route patterns with invalid parameter syntax</description></item>
        /// <item><description>Extremely complex nested route hierarchies causing stack overflow</description></item>
        /// <item><description>Invalid characters in route paths or parameters</description></item>
        /// <item><description>Query string parsing failures with malformed URLs</description></item>
        /// <item><description>Circular route definitions causing infinite recursion</description></item>
        /// </list>
        /// <para>
        /// <strong>Prevention:</strong> Validate route configurations at startup, use simple route patterns
        /// when possible, and test with various URL formats. Route matching errors usually indicate
        /// configuration problems that should be fixed in the route definitions.
        /// </para>
        /// </remarks>
        RouteMatching,

        /// <summary>
        /// An error occurred while executing a route guard.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Guard execution errors happen when a route guard's CanActivateAsync method throws an unhandled
        /// exception. Guards are meant to return true/false for authorization, so exceptions indicate
        /// unexpected failures in the guard logic itself, not authorization denial.
        /// </para>
        /// <para>
        /// Common causes of guard execution errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Exceptions in authentication state retrieval (e.g., expired tokens)</description></item>
        /// <item><description>Database or API failures during permission checks</description></item>
        /// <item><description>Null reference errors in guard logic</description></item>
        /// <item><description>Timeout exceptions in async guard operations</description></item>
        /// <item><description>Dependency injection failures for guard dependencies</description></item>
        /// </list>
        /// <para>
        /// <strong>Best Practice:</strong> Guards should handle their own exceptions and return false on
        /// errors rather than throwing. When GuardExecution errors occur, review the guard implementation
        /// to add proper error handling. Log the specific guard type and route for easier debugging.
        /// </para>
        /// </remarks>
        GuardExecution,

        /// <summary>
        /// An error occurred while lazy loading a route component.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Component loading errors occur when a route's ComponentLoader function fails to load or return
        /// the component type. This typically happens with lazy-loaded routes where the component is loaded
        /// on-demand rather than at application startup.
        /// </para>
        /// <para>
        /// Common causes of component loading errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Network failures when fetching lazy-loaded assemblies (WebAssembly)</description></item>
        /// <item><description>Missing or deleted component assemblies</description></item>
        /// <item><description>Type resolution failures (wrong type name or namespace)</description></item>
        /// <item><description>Reflection restrictions in trimmed or AOT-compiled applications</description></item>
        /// <item><description>Timeout while loading large component assemblies</description></item>
        /// </list>
        /// <para>
        /// <strong>User Experience:</strong> Show retry options for component loading failures, especially
        /// in WebAssembly where network issues are common. Consider implementing exponential backoff for
        /// retries and caching successfully loaded components to avoid repeated loading.
        /// </para>
        /// </remarks>
        ComponentLoading,

        /// <summary>
        /// An error occurred while rendering a route component.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Component rendering errors happen after a component has been successfully loaded and matched,
        /// but fails during its initialization or rendering lifecycle. These are typically caused by bugs
        /// in the component's code rather than routing configuration issues.
        /// </para>
        /// <para>
        /// Common causes of component rendering errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Null reference exceptions in component OnInitialized or OnParametersSet</description></item>
        /// <item><description>Invalid component parameters or missing required parameters</description></item>
        /// <item><description>Exceptions in component constructors or dependency injection</description></item>
        /// <item><description>Errors in markup rendering (invalid Razor syntax results)</description></item>
        /// <item><description>Failed data fetching in component initialization</description></item>
        /// </list>
        /// <para>
        /// <strong>Debugging Tip:</strong> Component rendering errors usually indicate bugs in the component
        /// code itself. Check the component's lifecycle methods, parameter validation, and data access logic.
        /// Use Blazor error boundaries to catch and display these errors gracefully. The exception details
        /// will include the specific component type and stack trace for debugging.
        /// </para>
        /// </remarks>
        ComponentRendering,

        /// <summary>
        /// An error occurred while executing route middleware.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Middleware execution errors occur when a route middleware's InvokeAsync method throws an unhandled
        /// exception. Middleware are meant to execute logic and call next() to continue the pipeline, so
        /// exceptions indicate unexpected failures in the middleware logic itself.
        /// </para>
        /// <para>
        /// Common causes of middleware execution errors:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Exceptions in logging or analytics service calls</description></item>
        /// <item><description>Network failures when preloading data</description></item>
        /// <item><description>Null reference errors in middleware logic</description></item>
        /// <item><description>Timeout exceptions in async middleware operations</description></item>
        /// <item><description>Dependency injection failures for middleware dependencies</description></item>
        /// </list>
        /// <para>
        /// <strong>Best Practice:</strong> Middleware should handle their own exceptions gracefully rather
        /// than throwing. When MiddlewareExecution errors occur, review the middleware implementation to add
        /// proper error handling. Middleware executes before guards, so a middleware error will prevent
        /// guards from running. Log the specific middleware type and route for easier debugging.
        /// </para>
        /// </remarks>
        MiddlewareExecution
    }
}