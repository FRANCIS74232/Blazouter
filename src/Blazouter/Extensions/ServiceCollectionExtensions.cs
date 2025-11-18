using Blazouter.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blazouter.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Blazouter services in the dependency injection container.
    /// </summary>
    /// <remarks>
    /// This class contains extension methods that simplify the registration of all required Blazouter services.
    /// These methods should be called during application startup in the service configuration phase.
    /// </remarks>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all required Blazouter services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add Blazouter services to.</param>
        /// <returns>
        /// The same service collection so that multiple calls can be chained.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method registers the following services:
        /// </para>
        /// <list type="bullet">
        /// <item>
        /// <description><see cref="RouterStateService"/> as a singleton - Manages global router state.</description>
        /// </item>
        /// <item>
        /// <description><see cref="RouterNavigationService"/> as scoped - Provides programmatic navigation per request/circuit.</description>
        /// </item>
        /// <item>
        /// <description><see cref="IRouteMatcherService"/> as a singleton - Provides route matching logic.</description>
        /// </item>
        /// </list>
        /// <para>
        /// The singleton services ensure consistent route matching and state management across the application.
        /// The scoped navigation service ensures proper isolation in server-side scenarios where multiple
        /// users may be using the application simultaneously.
        /// </para>
        /// </remarks>
        /// <example>
        /// Register Blazouter services in Program.cs:
        /// <code>
        /// // Blazor WebAssembly
        /// builder.Services.AddBlazouter();
        /// 
        /// // Blazor Server
        /// builder.Services.AddBlazouter();
        /// 
        /// // .NET MAUI Blazor Hybrid
        /// builder.Services.AddBlazouter();
        /// </code>
        /// </example>
        public static IServiceCollection AddBlazouter(this IServiceCollection services)
        {
            services.AddSingleton<RouterStateService>();
            services.AddScoped<RouterNavigationService>();
            services.AddSingleton<IRouteMatcherService, RouteMatcherService>();

            return services;
        }

        /// <summary>
        /// Registers a custom error handler for Blazouter routing errors.
        /// </summary>
        /// <typeparam name="THandler">
        /// The type of the custom error handler that implements <see cref="IRouterErrorHandler"/>.
        /// </typeparam>
        /// <param name="services">The service collection to add the error handler to.</param>
        /// <returns>
        /// The same service collection so that multiple calls can be chained.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method registers a custom implementation of <see cref="IRouterErrorHandler"/> as a scoped service.
        /// The error handler will be called whenever routing errors occur, such as component loading failures,
        /// route guard exceptions, or navigation errors.
        /// </para>
        /// <para>
        /// The error handler is registered as scoped to ensure proper isolation in server-side scenarios and
        /// to allow access to scoped services like logging or authentication state.
        /// </para>
        /// <para>
        /// If no custom error handler is registered, Blazouter will use its default error handling behavior,
        /// which displays the error UI defined in the Router component's ErrorContent parameter.
        /// </para>
        /// <para>
        /// For the error handler to display custom error UI, you must also define an ErrorContent section
        /// in your Router component. The error handler primarily controls logging and whether errors should
        /// be handled gracefully (return true) or rethrown (return false).
        /// </para>
        /// </remarks>
        /// <example>
        /// Register a custom error handler in Program.cs:
        /// <code>
        /// // Blazor WebAssembly
        /// builder.Services.AddBlazouter();
        /// builder.Services.AddBlazouterErrorHandler&lt;CustomRouterErrorHandler&gt;();
        /// 
        /// // Blazor Server
        /// builder.Services.AddBlazouter();
        /// builder.Services.AddBlazouterErrorHandler&lt;CustomRouterErrorHandler&gt;();
        /// 
        /// // Custom error handler implementation:
        /// public class CustomRouterErrorHandler : IRouterErrorHandler
        /// {
        ///     private readonly ILogger&lt;CustomRouterErrorHandler&gt; _logger;
        ///     
        ///     public CustomRouterErrorHandler(ILogger&lt;CustomRouterErrorHandler&gt; logger)
        ///     {
        ///         _logger = logger;
        ///     }
        ///     
        ///     public Task&lt;bool&gt; HandleErrorAsync(Exception exception, RouterErrorContext context)
        ///     {
        ///         _logger.LogError(exception, "Routing error: {ErrorType}", context.ErrorType);
        ///         return Task.FromResult(true); // Handle gracefully
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="IRouterErrorHandler"/>
        /// <seealso cref="RouterErrorContext"/>
        /// <seealso cref="DefaultRouterErrorHandler"/>
        public static IServiceCollection AddBlazouterErrorHandler<THandler>(this IServiceCollection services) where THandler : class, IRouterErrorHandler
        {
            services.AddScoped<IRouterErrorHandler, THandler>();

            return services;
        }
    }
}