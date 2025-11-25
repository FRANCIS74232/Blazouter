using Blazouter.Middleware;
using Blazouter.Models;

namespace Blazouter.WebAssembly.Sample.Middlewares
{
    /// <summary>
    /// Example middleware that preloads data before navigation completes.
    /// </summary>
    /// <remarks>
    /// This middleware demonstrates how to fetch data during navigation and make it available
    /// to components via the context.Data dictionary.
    /// </remarks>
    public class DataPreloadMiddleware : IRouteMiddleware
    {
        public async Task InvokeAsync(RouteMiddlewareContext context, Func<Task> next)
        {
            // Check if route has an 'id' parameter
            if (context.Match.Params.TryGetValue("id", out string? id))
            {
                Console.WriteLine($"[DataPreloadMiddleware] Preloading data for ID: {id}");

                // Simulate data loading (in a real app, call an API)
                await Task.Delay(100);

                // Store preloaded data in context
                context.Data["PreloadedData"] = new
                {
                    Id = id,
                    Name = $"Sample Item {id}",
                    LoadedAt = DateTime.UtcNow
                };

                Console.WriteLine($"[DataPreloadMiddleware] Data preloaded for ID: {id}");
            }

            // Continue to next middleware or component
            await next();
        }
    }
}