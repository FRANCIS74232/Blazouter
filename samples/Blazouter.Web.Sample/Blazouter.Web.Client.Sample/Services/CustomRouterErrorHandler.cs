using Blazouter.Interfaces;
using Blazouter.Models;

namespace Blazouter.Web.Client.Sample.Services
{
    /// <summary>
    /// Custom error handler for the Blazouter Web sample client application.
    /// Logs routing errors and handles them gracefully.
    /// </summary>
    public class CustomRouterErrorHandler(ILogger<CustomRouterErrorHandler> logger) : IRouterErrorHandler
    {
        public Task<bool> HandleErrorAsync(Exception exception, RouterErrorContext context)
        {
            // Log the error with detailed context
            logger.LogError(exception,
                "Routing error occurred in Web client app. " +
                "Type: {ErrorType}, URL: {Url}, Route: {RoutePath}, Component: {ComponentType}",
                context.ErrorType,
                context.Url ?? "N/A",
                context.RoutePath ?? "N/A",
                context.ComponentType?.Name ?? "N/A");

            // You can add additional logic here:
            // - Send to error tracking service (e.g., Sentry, Application Insights)
            // - Store in database for analytics
            // - Send notifications for critical errors

            // Always handle gracefully - show error UI instead of crashing
            return Task.FromResult(true);
        }
    }
}