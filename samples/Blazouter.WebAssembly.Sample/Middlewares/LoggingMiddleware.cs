using Blazouter.Interfaces;
using Blazouter.Models;

namespace Blazouter.WebAssembly.Sample.Middlewares
{
    /// <summary>
    /// Example middleware that logs navigation events to the console.
    /// </summary>
    /// <remarks>
    /// This middleware demonstrates how to execute logic before and after navigation.
    /// In a real application, you would use ILogger or a logging service instead of Console.WriteLine.
    /// </remarks>
    public class LoggingMiddleware : IRouteMiddleware
    {
        public async Task InvokeAsync(RouteMiddlewareContext context, Func<Task> next)
        {
            // Log before navigation
            Console.WriteLine($"[LoggingMiddleware] Navigating to: {context.Path}");
            Console.WriteLine($"[LoggingMiddleware] Route pattern: {context.Match.Route.Path}");

            // Continue to next middleware or component
            await next();

            // Log after navigation
            Console.WriteLine($"[LoggingMiddleware] Navigation to {context.Path} completed");
        }
    }
}