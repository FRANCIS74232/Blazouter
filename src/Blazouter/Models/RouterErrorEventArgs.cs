using Blazouter.Services;

namespace Blazouter.Models
{
    /// <summary>
    /// Provides data for the Router component's OnError event when routing errors occur.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterErrorEventArgs is passed to the OnError event handler, providing complete information
    /// about the routing error including the exception, contextual details, and the ability to mark
    /// the error as handled to customize error presentation behavior.
    /// </para>
    /// <para>
    /// The event args allow applications to implement custom error handling logic such as:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Logging errors to external services (Application Insights, Sentry, etc.)</description></item>
    /// <item><description>Displaying custom error messages or notifications to users</description></item>
    /// <item><description>Implementing error recovery strategies based on error type</description></item>
    /// <item><description>Suppressing default error UI when custom handling is sufficient</description></item>
    /// <item><description>Collecting telemetry or analytics on routing failures</description></item>
    /// </list>
    /// <para>
    /// The OnError event fires before the Router's ErrorContent is displayed, giving you the opportunity
    /// to handle the error programmatically. Setting Handled = true will prevent the default error UI
    /// from appearing.
    /// </para>
    /// </remarks>
    /// <example>
    /// Basic error logging in Router component:
    /// <code>
    /// &lt;Router Routes="@_routes" OnError="@HandleError"&gt;
    ///     &lt;NotFound&gt;&lt;h1&gt;404&lt;/h1&gt;&lt;/NotFound&gt;
    ///     &lt;ErrorContent Context="errorInfo"&gt;
    ///         &lt;h1&gt;Error&lt;/h1&gt;
    ///         &lt;p&gt;@errorInfo.Message&lt;/p&gt;
    ///     &lt;/ErrorContent&gt;
    /// &lt;/Router&gt;
    /// 
    /// @code {
    ///     private async Task HandleError(RouterErrorEventArgs args)
    ///     {
    ///         // Log to external service
    ///         await Logger.LogErrorAsync(args.Exception, args.Context);
    ///         
    ///         // Error UI will still display (Handled is false by default)
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Custom error handling with suppressed default UI:
    /// <code>
    /// private async Task HandleError(RouterErrorEventArgs args)
    /// {
    ///     // Log the error
    ///     Logger.LogError(args.Exception, "Routing error: {ErrorType}", args.Context.ErrorType);
    ///     
    ///     // Show custom toast notification
    ///     await ToastService.ShowErrorAsync("Navigation failed. Please try again.");
    ///     
    ///     // Suppress default error UI since we showed a toast
    ///     args.Handled = true;
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Type-specific error handling:
    /// <code>
    /// private async Task HandleError(RouterErrorEventArgs args)
    /// {
    ///     switch (args.Context.ErrorType)
    ///     {
    ///         case RouterErrorType.ComponentLoading:
    ///             // Network issue - show retry option
    ///             await ToastService.ShowWarningAsync("Failed to load page. Check your connection.");
    ///             args.Handled = true; // Use toast instead of error UI
    ///             break;
    ///             
    ///         case RouterErrorType.GuardExecution:
    ///             // Authorization issue - redirect to login
    ///             NavigationManager.NavigateTo("/login");
    ///             args.Handled = true; // Handled by redirect
    ///             break;
    ///             
    ///         default:
    ///             // Other errors - use default error UI
    ///             await Logger.LogErrorAsync(args.Exception);
    ///             // args.Handled = false (default), will show ErrorContent
    ///             break;
    ///     }
    /// }
    /// </code>
    /// </example>
    public class RouterErrorEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the error has been handled and the default error UI should be suppressed.
        /// </summary>
        /// <value>
        /// true to suppress the Router's ErrorContent display; false (default) to show the error UI.
        /// </value>
        /// <remarks>
        /// <para>
        /// When set to true, this property prevents the Router from displaying its ErrorContent render fragment,
        /// allowing you to handle error presentation completely through custom logic (such as toast notifications,
        /// modal dialogs, or programmatic navigation).
        /// </para>
        /// <para>
        /// The default value is false, meaning the Router will display its ErrorContent after the OnError event
        /// completes. This provides a fail-safe error display even if the event handler doesn't explicitly handle
        /// the error.
        /// </para>
        /// <para>
        /// <strong>Important:</strong> If you set Handled = true, ensure you provide some form of user feedback
        /// about the error. Silent failures create poor user experience and make debugging difficult.
        /// </para>
        /// </remarks>
        /// <example>
        /// Suppressing error UI after showing custom notification:
        /// <code>
        /// private async Task HandleError(RouterErrorEventArgs args)
        /// {
        ///     // Show custom notification
        ///     await NotificationService.ShowAsync("Unable to navigate to the requested page.", NotificationType.Error);
        ///     
        ///     // Suppress Router's error UI since we displayed our notification
        ///     args.Handled = true;
        /// }
        /// </code>
        /// </example>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the exception that caused the routing error.
        /// </summary>
        /// <value>
        /// The <see cref="Exception"/> object containing details about what went wrong during routing.
        /// </value>
        /// <remarks>
        /// <para>
        /// This property provides access to the original exception thrown during the routing process. The exception
        /// type and message vary depending on what phase of routing failed (matching, guard execution, component
        /// loading, etc.).
        /// </para>
        /// <para>
        /// Use this exception for:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Logging detailed error information including stack traces</description></item>
        /// <item><description>Determining specific error conditions through exception type checking</description></item>
        /// <item><description>Extracting error messages for user display</description></item>
        /// <item><description>Sending error reports to telemetry services</description></item>
        /// </list>
        /// <para>
        /// The exception's stack trace can help identify the exact location in your code where the error occurred,
        /// which is especially useful for ComponentRendering and GuardExecution errors.
        /// </para>
        /// </remarks>
        /// <example>
        /// Logging exception details:
        /// <code>
        /// private async Task HandleError(RouterErrorEventArgs args)
        /// {
        ///     _logger.LogError(
        ///         args.Exception,
        ///         "Routing error: {ErrorType} at {Url}. Message: {Message}",
        ///         args.Context.ErrorType,
        ///         args.Context.Url,
        ///         args.Exception.Message);
        ///         
        ///     // Include stack trace in debug builds
        ///     #if DEBUG
        ///     _logger.LogDebug("Stack trace: {StackTrace}", args.Exception.StackTrace);
        ///     #endif
        /// }
        /// </code>
        /// </example>
        public Exception Exception { get; init; } = null!;

        /// <summary>
        /// Gets contextual information about the routing error.
        /// </summary>
        /// <value>
        /// A <see cref="RouterErrorContext"/> object containing metadata about where and when the error occurred.
        /// </value>
        /// <remarks>
        /// <para>
        /// The context provides structured information about the routing failure, including the error type,
        /// the URL being navigated to, the route pattern being matched, and the component type involved. This
        /// information helps in diagnosing the root cause and implementing targeted error handling.
        /// </para>
        /// <para>
        /// Key context properties:
        /// </para>
        /// <list type="bullet">
        /// <item><description><b>ErrorType</b> - Categorizes the error (matching, loading, rendering, etc.)</description></item>
        /// <item><description><b>Url</b> - The URL that was being navigated to when the error occurred</description></item>
        /// <item><description><b>RoutePath</b> - The route pattern that was being processed</description></item>
        /// <item><description><b>ComponentType</b> - The component type that failed (if applicable)</description></item>
        /// <item><description><b>AdditionalData</b> - Custom data for extended error context</description></item>
        /// </list>
        /// <para>
        /// Use the context to make intelligent decisions about error handling strategy. For example, ComponentLoading
        /// errors might warrant retry logic, while GuardExecution errors might redirect to login.
        /// </para>
        /// </remarks>
        /// <example>
        /// Using context for conditional error handling:
        /// <code>
        /// private async Task HandleError(RouterErrorEventArgs args)
        /// {
        ///     var ctx = args.Context;
        ///     
        ///     _logger.LogError(
        ///         args.Exception,
        ///         "Routing error: {ErrorType}\nURL: {Url}\nRoute: {Route}\nComponent: {Component}",
        ///         ctx.ErrorType,
        ///         ctx.Url ?? "Unknown",
        ///         ctx.RoutePath ?? "Unknown",
        ///         ctx.ComponentType?.Name ?? "Unknown");
        ///     
        ///     // Handle based on error type from context
        ///     if (ctx.ErrorType == RouterErrorType.ComponentLoading)
        ///     {
        ///         await ShowRetryDialog($"Failed to load {ctx.ComponentType?.Name}");
        ///         args.Handled = true;
        ///     }
        /// }
        /// </code>
        /// </example>
        public RouterErrorContext Context { get; init; } = null!;
    }
}