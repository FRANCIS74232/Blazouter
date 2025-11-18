using Blazouter.Services;
using Microsoft.Extensions.Logging;

namespace Blazouter.Hybrid.Sample.Services
{
    /// <summary>
    /// Custom error handler for the Blazouter WebAssembly sample application.
    /// Logs routing errors to the console and handles them gracefully.
    /// </summary>
    public class CustomRouterErrorHandler(ILogger<CustomRouterErrorHandler> logger) : IRouterErrorHandler
    {
        public Task<bool> HandleErrorAsync(Exception exception, RouterErrorContext context)
        {
            // Log the error with detailed context
            logger.LogError(exception,
                "Routing error occurred in WebAssembly app. " +
                "Type: {ErrorType}, URL: {Url}, Route: {RoutePath}, Component: {ComponentType}",
                context.ErrorType,
                context.Url ?? "N/A",
                context.RoutePath ?? "N/A",
                context.ComponentType?.Name ?? "N/A");

            // Additional logging to console for development
            Console.WriteLine($"[Blazouter Error] {context.ErrorType}: {exception.Message}");
            if (context.Url != null)
            {
                Console.WriteLine($"[Blazouter Error] URL: {context.Url}");
            }

            // Always handle gracefully - show error UI instead of crashing
            return Task.FromResult(true);
        }
    }
}