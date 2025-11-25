using Blazouter.Middleware;
using Blazouter.Models;

namespace Blazouter.WebAssembly.Sample.Middlewares
{
    /// <summary>
    /// Example middleware that tracks page views for analytics.
    /// </summary>
    /// <remarks>
    /// This middleware demonstrates how to send analytics data during navigation.
    /// In a real application, you would integrate with an analytics service like Google Analytics or Application Insights.
    /// </remarks>
    public class AnalyticsMiddleware : IRouteMiddleware
    {
        public async Task InvokeAsync(RouteMiddlewareContext context, Func<Task> next)
        {
            // Track page view (in a real app, send to analytics service)
            Console.WriteLine($"[AnalyticsMiddleware] Page view tracked: {context.Path}");
            Console.WriteLine($"[AnalyticsMiddleware] Route title: {context.Match.Route.Title ?? "N/A"}");

            // Store analytics data in context
            context.Data["PageViewTimestamp"] = DateTime.UtcNow;
            context.Data["PageViewId"] = Guid.NewGuid();

            // Continue to next middleware or component
            await next();
        }
    }
}