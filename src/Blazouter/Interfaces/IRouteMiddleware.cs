using Blazouter.Models;

namespace Blazouter.Interfaces
{
    /// <summary>
    /// Defines a contract for route middleware that executes logic during navigation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Route middleware provides a way to execute code before and after route navigation,
    /// enabling cross-cutting concerns like logging, analytics, data preloading, caching,
    /// and more. Unlike route guards which focus on access control, middleware can perform
    /// any arbitrary logic and modify the navigation context.
    /// </para>
    /// <para>
    /// Middleware executes in a pipeline pattern where each middleware decides whether to
    /// continue to the next middleware by calling the provided next delegate. This allows
    /// middleware to:
    /// - Execute logic before navigation (by placing code before calling next())
    /// - Execute logic after navigation (by placing code after calling next())
    /// - Short-circuit navigation by not calling next()
    /// - Modify context data that components can access
    /// </para>
    /// <para>
    /// Middleware are executed in the order they appear in the route configuration, before
    /// route guards are evaluated. Multiple middleware can be chained together to create
    /// complex navigation pipelines.
    /// </para>
    /// <para>
    /// Common use cases include:
    /// - Logging and analytics tracking
    /// - Performance monitoring and timing
    /// - Data preloading and caching
    /// - Request/response transformation
    /// - Feature flags and A/B testing
    /// - User session management
    /// - Error tracking and reporting
    /// - Authorization enrichment
    /// </para>
    /// </remarks>
    /// <example>
    /// Simple logging middleware:
    /// <code>
    /// public class LoggingMiddleware : IRouteMiddleware
    /// {
    ///     private readonly ILogger&lt;LoggingMiddleware&gt; _logger;
    ///     
    ///     public LoggingMiddleware(ILogger&lt;LoggingMiddleware&gt; logger)
    ///     {
    ///         _logger = logger;
    ///     }
    ///     
    ///     public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
    ///     {
    ///         var stopwatch = Stopwatch.StartNew();
    ///         _logger.LogInformation("Navigating to {Path}", context.Path);
    ///         
    ///         try
    ///         {
    ///             await next();
    ///             _logger.LogInformation("Navigation to {Path} completed in {ElapsedMs}ms", 
    ///                 context.Path, stopwatch.ElapsedMilliseconds);
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             _logger.LogError(ex, "Navigation to {Path} failed", context.Path);
    ///             throw;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Data preloading middleware:
    /// <code>
    /// public class DataPreloadMiddleware : IRouteMiddleware
    /// {
    ///     private readonly IDataService _dataService;
    ///     
    ///     public DataPreloadMiddleware(IDataService dataService)
    ///     {
    ///         _dataService = dataService;
    ///     }
    ///     
    ///     public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
    ///     {
    ///         // Preload data before navigation
    ///         if (context.Match.Params.TryGetValue("id", out string? id))
    ///         {
    ///             var data = await _dataService.GetAsync(id);
    ///             context.Data["PreloadedData"] = data;
    ///         }
    ///         
    ///         await next();
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IRouteMiddleware
    {
        /// <summary>
        /// Executes middleware logic during route navigation.
        /// </summary>
        /// <param name="context">
        /// The <see cref="RouteMiddlewareContext"/> containing information about the current
        /// navigation, including route match, path, and a data dictionary for sharing state.
        /// </param>
        /// <param name="next">
        /// A delegate representing the next middleware in the pipeline. Call this to continue
        /// execution to the next middleware or the route component. Not calling this effectively
        /// short-circuits the navigation pipeline.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous middleware execution.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Implementations should call the next delegate to continue the middleware pipeline.
        /// Code placed before calling next() executes before navigation, while code after
        /// calling next() executes after navigation.
        /// </para>
        /// <para>
        /// To abort navigation, set context.Abort to true and optionally set context.RedirectPath
        /// to redirect elsewhere. Then return without calling next().
        /// </para>
        /// <para>
        /// Exceptions thrown from middleware will be caught by the router's error handling system
        /// and displayed using the ErrorContent parameter if configured.
        /// </para>
        /// <para>
        /// Middleware can be obtained from dependency injection or instantiated directly. When
        /// registered in DI, middleware can receive other services through constructor injection.
        /// </para>
        /// </remarks>
        /// <example>
        /// Middleware with before and after logic:
        /// <code>
        /// public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
        /// {
        ///     // Before navigation
        ///     Console.WriteLine($"Before: {context.Path}");
        ///     context.Data["StartTime"] = DateTime.UtcNow;
        ///     
        ///     // Continue to next middleware or component
        ///     await next();
        ///     
        ///     // After navigation
        ///     var startTime = (DateTime)context.Data["StartTime"];
        ///     var duration = DateTime.UtcNow - startTime;
        ///     Console.WriteLine($"After: {context.Path}, Duration: {duration.TotalMilliseconds}ms");
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// Middleware that conditionally aborts navigation:
        /// <code>
        /// public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
        /// {
        ///     // Check if maintenance mode is enabled
        ///     if (await IsMaintenanceModeAsync())
        ///     {
        ///         context.Abort = true;
        ///         context.RedirectPath = "/maintenance";
        ///         return;
        ///     }
        ///     
        ///     await next();
        /// }
        /// </code>
        /// </example>
        Task InvokeAsync(RouteMiddlewareContext context, Func<Task> next);
    }
}