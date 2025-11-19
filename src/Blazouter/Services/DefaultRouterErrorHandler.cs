using Microsoft.Extensions.Logging;

namespace Blazouter.Services
{
    /// <summary>
    /// Provides a default implementation of <see cref="IRouterErrorHandler"/> with structured error logging.
    /// </summary>
    /// <remarks>
    /// <para>
    /// DefaultRouterErrorHandler offers a simple, production-ready error handling strategy that logs routing
    /// errors with structured information and always allows the Router to display its ErrorContent UI. This
    /// provides a fail-safe error handling behavior that prevents application crashes while providing
    /// diagnostic information through logging.
    /// </para>
    /// <para>
    /// The handler logs errors at the ERROR level using structured logging, including:
    /// </para>
    /// <list type="bullet">
    /// <item><description>The exception with full stack trace</description></item>
    /// <item><description>Error type categorization (routing, loading, rendering, etc.)</description></item>
    /// <item><description>The URL being navigated to</description></item>
    /// <item><description>The route pattern being matched</description></item>
    /// <item><description>The component type involved (if applicable)</description></item>
    /// </list>
    /// <para>
    /// This handler is automatically used by the Router if no custom IRouterErrorHandler is registered in
    /// the dependency injection container. You can replace it with a custom implementation for more sophisticated
    /// error handling needs:
    /// </para>
    /// <code>
    /// // Register default handler explicitly
    /// services.AddBlazouterErrorHandler&lt;DefaultRouterErrorHandler&gt;();
    /// 
    /// // Or register your custom handler
    /// services.AddBlazouterErrorHandler&lt;MyCustomErrorHandler&gt;();
    /// </code>
    /// <para>
    /// <strong>Design Philosophy:</strong> This handler follows a "fail gracefully" approach, always returning
    /// true to display error UI rather than letting exceptions propagate. This ensures users always see a
    /// helpful error message rather than experiencing application crashes or blank screens.
    /// </para>
    /// </remarks>
    /// <example>
    /// Using the default handler with logging configuration:
    /// <code>
    /// // Program.cs
    /// builder.Services.AddLogging(config =>
    /// {
    ///     config.AddConsole();
    ///     config.SetMinimumLevel(LogLevel.Information);
    /// });
    /// 
    /// builder.Services.AddBlazouter();
    /// // DefaultRouterErrorHandler is used automatically if no custom handler registered
    /// </code>
    /// </example>
    /// <example>
    /// Example log output from DefaultRouterErrorHandler:
    /// <code>
    /// [Error] Routing error occurred. 
    ///   Type: ComponentLoading, 
    ///   URL: /admin/users, 
    ///   Route: /admin/:section, 
    ///   Component: AdminUsersPage
    ///   Exception: System.Net.Http.HttpRequestException: Failed to load assembly...
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DefaultRouterErrorHandler"/> class.
    /// </remarks>
    /// <param name="logger">
    /// Optional logger for error logging. If null, errors are silently handled without logging, which is
    /// useful for testing scenarios or applications that don't use logging infrastructure.
    /// </param>
    public class DefaultRouterErrorHandler(ILogger<DefaultRouterErrorHandler>? logger = null) : IRouterErrorHandler
    {
        /// <summary>
        /// Handles routing errors by logging them with structured information and allowing error UI display.
        /// </summary>
        /// <param name="exception">
        /// The exception that occurred during routing. Contains the error message, stack trace, and any inner exceptions.
        /// </param>
        /// <param name="context">
        /// Context information about the error, including error type, URL, route path, and component type.
        /// </param>
        /// <returns>
        /// Always returns true, indicating the error should be handled gracefully by displaying the Router's
        /// ErrorContent rather than propagating the exception.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method performs structured logging using the ILogger instance provided at construction. The log
        /// entry includes the exception object (with stack trace) and structured properties for error type, URL,
        /// route path, and component name, making it easy to filter and analyze routing errors in log aggregation
        /// systems.
        /// </para>
        /// <para>
        /// If no logger was provided (null), the method silently succeeds without logging, allowing error UI
        /// display without logging infrastructure. This is useful for testing or minimal deployments.
        /// </para>
        /// <para>
        /// The method always returns true, signaling to the Router that:
        /// </para>
        /// <list type="bullet">
        /// <item><description>The error has been "handled" (logged)</description></item>
        /// <item><description>The ErrorContent UI should be displayed</description></item>
        /// <item><description>The exception should not be rethrown</description></item>
        /// <item><description>The application should remain stable</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// The logged error will appear in your logging sink with structure like:
        /// <code>
        /// {
        ///   "Timestamp": "2024-01-15T10:30:45.123Z",
        ///   "Level": "Error",
        ///   "MessageTemplate": "Routing error occurred. Type: {ErrorType}, URL: {Url}, Route: {RoutePath}, Component: {ComponentType}",
        ///   "Properties": {
        ///     "ErrorType": "ComponentLoading",
        ///     "Url": "/products/123",
        ///     "RoutePath": "/products/:id",
        ///     "ComponentType": "ProductDetailPage"
        ///   },
        ///   "Exception": "System.Net.Http.HttpRequestException: ..."
        /// }
        /// </code>
        /// </example>
        public Task<bool> HandleErrorAsync(Exception exception, RouterErrorContext context)
        {
            logger?.LogError(exception,
                    "Routing error occurred. Type: {ErrorType}, URL: {Url}, Route: {RoutePath}, Component: {ComponentType}",
                    context.ErrorType,
                    context.Url ?? "N/A",
                    context.RoutePath ?? "N/A",
                    context.ComponentType?.Name ?? "N/A");

            // Always handle gracefully - show error UI
            return Task.FromResult(true);
        }
    }
}