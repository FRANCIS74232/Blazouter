namespace Blazouter.Services
{
    /// <summary>
    /// Defines a contract for handling routing errors in Blazouter applications.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface allows you to implement custom error handling logic for routing failures,
    /// such as component loading errors, guard failures, or navigation errors. Implementations
    /// can log errors, send telemetry, or provide custom recovery strategies.
    /// </para>
    /// <para>
    /// Register your implementation in the DI container:
    /// </para>
    /// <code>
    /// services.AddScoped&lt;IRouterErrorHandler, CustomErrorHandler&gt;();
    /// </code>
    /// </remarks>
    public interface IRouterErrorHandler
    {
        /// <summary>
        /// Handles an error that occurred during routing operations.
        /// </summary>
        /// <param name="exception">The exception that was thrown.</param>
        /// <param name="context">Contextual information about where the error occurred.</param>
        /// <returns>
        /// A task that completes when error handling is finished. The return value indicates
        /// whether the error was handled (true) or should be rethrown (false).
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is called whenever an error occurs during:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Route matching</description></item>
        /// <item><description>Component lazy loading</description></item>
        /// <item><description>Route guard execution</description></item>
        /// <item><description>Component rendering</description></item>
        /// <item><description>Navigation operations</description></item>
        /// </list>
        /// <para>
        /// Return true to indicate the error was handled and routing should continue with error UI.
        /// Return false to rethrow the exception and let Blazor's error boundary handle it.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// public class LoggingErrorHandler : IRouterErrorHandler
        /// {
        ///     private readonly ILogger&lt;LoggingErrorHandler&gt; _logger;
        ///     
        ///     public LoggingErrorHandler(ILogger&lt;LoggingErrorHandler&gt; logger)
        ///     {
        ///         _logger = logger;
        ///     }
        ///     
        ///     public Task&lt;bool&gt; HandleErrorAsync(Exception exception, RouterErrorContext context)
        ///     {
        ///         _logger.LogError(exception, 
        ///             "Routing error in {Context}: {Message}", 
        ///             context.ErrorType, 
        ///             exception.Message);
        ///         
        ///         // Always handle gracefully - show error UI
        ///         return Task.FromResult(true);
        ///     }
        /// }
        /// </code>
        /// </example>
        Task<bool> HandleErrorAsync(Exception exception, RouterErrorContext context);
    }
}