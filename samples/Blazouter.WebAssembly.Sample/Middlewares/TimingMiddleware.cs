using Blazouter.Interfaces;
using Blazouter.Models;
using System.Diagnostics;

namespace Blazouter.WebAssembly.Sample.Middlewares
{
    /// <summary>
    /// Example middleware that measures navigation performance.
    /// </summary>
    /// <remarks>
    /// This middleware demonstrates how to measure the time taken for navigation
    /// and store timing data in the context for components to access.
    /// </remarks>
    public class TimingMiddleware : IRouteMiddleware
    {
        public async Task InvokeAsync(RouteMiddlewareContext context, Func<Task> next)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Store start time in context
            context.Data["NavigationStartTime"] = DateTime.UtcNow;

            // Continue to next middleware or component
            await next();

            stopwatch.Stop();

            // Store elapsed time in context
            context.Data["NavigationDuration"] = stopwatch.Elapsed;

            Console.WriteLine($"[TimingMiddleware] Navigation to {context.Path} took {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}