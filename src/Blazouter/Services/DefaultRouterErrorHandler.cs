using Microsoft.Extensions.Logging;

namespace Blazouter.Services
{
    /// <summary>
    /// Provides a default implementation of <see cref="IRouterErrorHandler"/> with basic error logging.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This handler logs errors using the configured ILogger and always returns true to display
    /// the error UI gracefully rather than crashing the application.
    /// </para>
    /// <para>
    /// You can register this handler or create your own implementation:
    /// </para>
    /// <code>
    /// services.AddScoped&lt;IRouterErrorHandler, DefaultRouterErrorHandler&gt;();
    /// // Or use your custom implementation:
    /// services.AddScoped&lt;IRouterErrorHandler, MyCustomErrorHandler&gt;();
    /// </code>
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DefaultRouterErrorHandler"/> class.
    /// </remarks>
    /// <param name="logger">Optional logger for error logging. If null, errors are not logged.</param>
    public class DefaultRouterErrorHandler(ILogger<DefaultRouterErrorHandler>? logger = null) : IRouterErrorHandler
    {
        /// <summary>
        /// Handles routing errors by logging them and returning true to display error UI.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="context">Context information about the error.</param>
        /// <returns>Always returns true to handle errors gracefully.</returns>
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