using Blazouter.Enums;

namespace Blazouter.Models
{
    /// <summary>
    /// Provides detailed contextual information about a routing error for error handlers and logging.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterErrorContext encapsulates all relevant metadata about a routing failure, enabling error handlers
    /// to make informed decisions about error handling strategies, logging detail levels, and user notifications.
    /// This context is passed to IRouterErrorHandler implementations and is included in RouterErrorEventArgs.
    /// </para>
    /// <para>
    /// The context captures information from different phases of the routing pipeline, allowing handlers to:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Identify the specific routing operation that failed (via ErrorType)</description></item>
    /// <item><description>Determine which URL and route configuration were involved</description></item>
    /// <item><description>Track which component type was being loaded or rendered</description></item>
    /// <item><description>Include custom diagnostic data through AdditionalData</description></item>
    /// <item><description>Implement targeted recovery strategies based on error context</description></item>
    /// </list>
    /// <para>
    /// This structured approach to error context makes it easier to implement sophisticated error handling,
    /// including conditional logging, user-specific error messages, and automated error recovery attempts.
    /// </para>
    /// </remarks>
    /// <example>
    /// Using context in a custom error handler:
    /// <code>
    /// public class TelemetryErrorHandler : IRouterErrorHandler
    /// {
    ///     public async Task&lt;bool&gt; HandleErrorAsync(Exception exception, RouterErrorContext context)
    ///     {
    ///         // Build telemetry event from context
    ///         var telemetryEvent = new Dictionary&lt;string, string&gt;
    ///         {
    ///             ["ErrorType"] = context.ErrorType.ToString(),
    ///             ["Url"] = context.Url ?? "Unknown",
    ///             ["RoutePath"] = context.RoutePath ?? "Unknown",
    ///             ["Component"] = context.ComponentType?.FullName ?? "None",
    ///             ["Message"] = exception.Message
    ///         };
    ///         
    ///         // Include any additional custom data
    ///         foreach (var kvp in context.AdditionalData)
    ///         {
    ///             telemetryEvent[$"Custom_{kvp.Key}"] = kvp.Value?.ToString() ?? "null";
    ///         }
    ///         
    ///         await _telemetryClient.TrackExceptionAsync(exception, telemetryEvent);
    ///         return true; // Handle gracefully
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Conditional handling based on context:
    /// <code>
    /// public async Task&lt;bool&gt; HandleErrorAsync(Exception exception, RouterErrorContext context)
    /// {
    ///     // Different logging levels based on error type
    ///     var logLevel = context.ErrorType switch
    ///     {
    ///         RouterErrorType.ComponentLoading => LogLevel.Warning,  // Might be transient
    ///         RouterErrorType.GuardExecution => LogLevel.Information, // Expected in some cases
    ///         _ => LogLevel.Error  // Unexpected errors
    ///     };
    ///     
    ///     _logger.Log(logLevel, exception,
    ///         "Routing error at {Url} for route {Route}",
    ///         context.Url, context.RoutePath);
    ///     
    ///     return true;
    /// }
    /// </code>
    /// </example>
    public class RouterErrorContext
    {
        /// <summary>
        /// Gets or sets the URL that was being navigated to when the error occurred.
        /// </summary>
        /// <value>
        /// The absolute or relative URL string that the router was attempting to navigate to, or null if the
        /// error occurred before URL determination or if not applicable to the error type.
        /// </value>
        /// <remarks>
        /// <para>
        /// This URL represents the target of the navigation attempt. It includes the path and may include
        /// query parameters, but does not include the fragment identifier (hash).
        /// </para>
        /// <para>
        /// Use this property for:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Logging which URLs are causing routing failures</description></item>
        /// <item><description>Implementing URL-based error recovery (e.g., redirect to similar valid URL)</description></item>
        /// <item><description>Tracking problematic routes in analytics</description></item>
        /// <item><description>Displaying to users what page they were trying to access</description></item>
        /// </list>
        /// </remarks>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the route path pattern that was being matched or processed when the error occurred.
        /// </summary>
        /// <value>
        /// The route pattern string from RouteConfig (e.g., "/users/:id", "/products/:category/:id"), 
        /// or null if the error occurred before route matching or if not applicable to the error type.
        /// </value>
        /// <remarks>
        /// <para>
        /// The route path represents the pattern defined in the route configuration, including parameter
        /// placeholders (e.g., :id, :slug). This is distinct from the Url property which contains the actual
        /// URL being navigated to.
        /// </para>
        /// <para>
        /// This property helps identify which route configuration is problematic, making it easier to:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Track which route patterns fail most frequently</description></item>
        /// <item><description>Identify configuration issues in route definitions</description></item>
        /// <item><description>Correlate errors with specific route configurations for debugging</description></item>
        /// <item><description>Implement route-specific error recovery strategies</description></item>
        /// </list>
        /// </remarks>
        public string? RoutePath { get; set; }

        /// <summary>
        /// Gets or sets the type of the component that failed to load or render.
        /// </summary>
        /// <value>
        /// The component Type that was being loaded or rendered when the error occurred, or null if the error
        /// didn't involve a specific component (e.g., routing or matching errors).
        /// </value>
        /// <remarks>
        /// <para>
        /// This property is primarily populated for ComponentLoading and ComponentRendering errors, where a
        /// specific component type has been identified. For other error types (RouteMatching, Navigation,
        /// GuardExecution), this may be null.
        /// </para>
        /// <para>
        /// Component type information enables:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Tracking which components have the most failures</description></item>
        /// <item><description>Implementing component-specific error handling or fallbacks</description></item>
        /// <item><description>Detailed error logging with component identification</description></item>
        /// <item><description>Developer-friendly error messages during debugging</description></item>
        /// </list>
        /// </remarks>
        public Type? ComponentType { get; set; }

        /// <summary>
        /// Gets or sets the category of routing operation that failed.
        /// </summary>
        /// <value>
        /// A value from the <see cref="RouterErrorType"/> enumeration indicating the specific phase of the
        /// routing pipeline where the error occurred.
        /// </value>
        /// <remarks>
        /// <para>
        /// The error type categorizes failures into specific phases of routing, enabling targeted error handling.
        /// This is one of the most important properties for implementing intelligent error handling strategies.
        /// </para>
        /// <para>
        /// Use ErrorType to:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Select appropriate log levels (Warning for transient errors, Error for critical failures)</description></item>
        /// <item><description>Choose recovery strategies (retry for loading errors, redirect for guard failures)</description></item>
        /// <item><description>Customize user messages based on error category</description></item>
        /// <item><description>Filter and analyze error patterns in telemetry</description></item>
        /// </list>
        /// </remarks>
        public RouterErrorType ErrorType { get; set; }

        /// <summary>
        /// Gets or sets additional custom data related to the error for extended context.
        /// </summary>
        /// <value>
        /// A dictionary of key-value pairs containing custom diagnostic data, or an empty dictionary if no
        /// additional data has been added.
        /// </value>
        /// <remarks>
        /// <para>
        /// This extensibility point allows error handlers and routing components to attach arbitrary contextual
        /// data to error events. This can include user context, application state, performance metrics, or any
        /// other information relevant for error diagnosis.
        /// </para>
        /// <para>
        /// Common uses for additional data:
        /// </para>
        /// <list type="bullet">
        /// <item><description>User identification for correlating errors with specific users</description></item>
        /// <item><description>Performance timing data (how long operations took before failure)</description></item>
        /// <item><description>Application state snapshots for reproduction</description></item>
        /// <item><description>Guard-specific information (which permission check failed)</description></item>
        /// <item><description>Retry attempt count for recurring failures</description></item>
        /// </list>
        /// <para>
        /// The dictionary accepts any object type as values, allowing for rich contextual data while maintaining
        /// type safety through the strongly-typed dictionary keys.
        /// </para>
        /// </remarks>
        /// <example>
        /// Adding custom context data:
        /// <code>
        /// var context = new RouterErrorContext
        /// {
        ///     ErrorType = RouterErrorType.GuardExecution,
        ///     Url = "/admin/users",
        ///     AdditionalData = new Dictionary&lt;string, object&gt;
        ///     {
        ///         ["UserId"] = currentUser.Id,
        ///         ["RequiredRole"] = "Admin",
        ///         ["UserRoles"] = string.Join(",", currentUser.Roles),
        ///         ["AttemptCount"] = retryCount
        ///     }
        /// };
        /// </code>
        /// </example>
        public Dictionary<string, object> AdditionalData { get; set; } = [];
    }
}