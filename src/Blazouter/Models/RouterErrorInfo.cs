using Blazouter.Enums;

namespace Blazouter.Models
{
    /// <summary>
    /// Provides comprehensive information about a routing error for display in the Router's ErrorContent UI.
    /// </summary>
    /// <remarks>
    /// <para>
    /// RouterErrorInfo is passed as the context parameter to the Router component's ErrorContent render fragment,
    /// providing all necessary information to display meaningful error messages and recovery options to users.
    /// Unlike RouterErrorEventArgs (used in the OnError event), this class is specifically designed for UI
    /// rendering purposes with convenient properties and a retry mechanism.
    /// </para>
    /// <para>
    /// This object provides multiple levels of error detail:
    /// </para>
    /// <list type="bullet">
    /// <item><description>High-level categorization through ErrorType</description></item>
    /// <item><description>User-friendly error message through Message property</description></item>
    /// <item><description>Technical details through FullDetails for debugging</description></item>
    /// <item><description>Contextual information (URL, route, component) for troubleshooting</description></item>
    /// <item><description>Built-in retry functionality through Retry action</description></item>
    /// </list>
    /// <para>
    /// The retry functionality allows users to attempt the failed navigation again, which is particularly
    /// useful for transient errors like network issues during component loading or temporary authentication
    /// failures.
    /// </para>
    /// </remarks>
    /// <example>
    /// Display error information in Router ErrorContent:
    /// <code>
    /// &lt;Router Routes="@_routes"&gt;
    ///     &lt;ErrorContent Context="errorInfo"&gt;
    ///         &lt;div class="error-container"&gt;
    ///             &lt;h1&gt;❌ Navigation Error&lt;/h1&gt;
    ///             &lt;p&gt;@errorInfo.Message&lt;/p&gt;
    ///             
    ///             @if (!string.IsNullOrEmpty(errorInfo.Url))
    ///             {
    ///                 &lt;p&gt;&lt;small&gt;URL: @errorInfo.Url&lt;/small&gt;&lt;/p&gt;
    ///             }
    ///             
    ///             &lt;button @onclick="errorInfo.Retry"&gt;Try Again&lt;/button&gt;
    ///             &lt;RouterLink Href="/"&gt;Go Home&lt;/RouterLink&gt;
    ///         &lt;/div&gt;
    ///     &lt;/ErrorContent&gt;
    /// &lt;/Router&gt;
    /// </code>
    /// </example>
    /// <example>
    /// Display error type-specific messages:
    /// <code>
    /// &lt;ErrorContent Context="errorInfo"&gt;
    ///     &lt;div class="alert alert-danger"&gt;
    ///         @switch (errorInfo.ErrorType)
    ///         {
    ///             case RouterErrorType.ComponentLoading:
    ///                 &lt;h3&gt;Failed to Load Page&lt;/h3&gt;
    ///                 &lt;p&gt;The requested page could not be loaded. This might be a temporary network issue.&lt;/p&gt;
    ///                 break;
    ///                 
    ///             case RouterErrorType.GuardExecution:
    ///                 &lt;h3&gt;Access Denied&lt;/h3&gt;
    ///                 &lt;p&gt;You don't have permission to access this page.&lt;/p&gt;
    ///                 break;
    ///                 
    ///             default:
    ///                 &lt;h3&gt;Navigation Error&lt;/h3&gt;
    ///                 &lt;p&gt;@errorInfo.Message&lt;/p&gt;
    ///                 break;
    ///         }
    ///         
    ///         &lt;button class="btn btn-primary" @onclick="errorInfo.Retry"&gt;Retry&lt;/button&gt;
    ///     &lt;/div&gt;
    /// &lt;/ErrorContent&gt;
    /// </code>
    /// </example>
    /// <example>
    /// Show detailed error information in development:
    /// <code>
    /// &lt;ErrorContent Context="errorInfo"&gt;
    ///     &lt;div class="error-details"&gt;
    ///         &lt;h2&gt;Routing Error&lt;/h2&gt;
    ///         &lt;p&gt;&lt;strong&gt;Message:&lt;/strong&gt; @errorInfo.Message&lt;/p&gt;
    ///         &lt;p&gt;&lt;strong&gt;Type:&lt;/strong&gt; @errorInfo.ErrorType&lt;/p&gt;
    ///         &lt;p&gt;&lt;strong&gt;URL:&lt;/strong&gt; @errorInfo.Url&lt;/p&gt;
    ///         &lt;p&gt;&lt;strong&gt;Route:&lt;/strong&gt; @errorInfo.RoutePath&lt;/p&gt;
    ///         &lt;p&gt;&lt;strong&gt;Component:&lt;/strong&gt; @errorInfo.ComponentType?.Name&lt;/p&gt;
    ///         
    ///         #if DEBUG
    ///         &lt;details&gt;
    ///             &lt;summary&gt;Stack Trace&lt;/summary&gt;
    ///             &lt;pre&gt;@errorInfo.FullDetails&lt;/pre&gt;
    ///         &lt;/details&gt;
    ///         #endif
    ///         
    ///         &lt;button @onclick="errorInfo.Retry"&gt;Retry&lt;/button&gt;
    ///     &lt;/div&gt;
    /// &lt;/ErrorContent&gt;
    /// </code>
    /// </example>
    public class RouterErrorInfo
    {
        /// <summary>
        /// Gets the URL that was being navigated to when the error occurred.
        /// </summary>
        /// <value>
        /// The target URL string, or null if the error occurred before URL determination.
        /// </value>
        /// <remarks>
        /// This property contains the full URL path that the application was attempting to navigate to when
        /// the routing error occurred. It's useful for displaying to users so they understand what page they
        /// were trying to access, and for logging purposes to track which routes are causing issues.
        /// </remarks>
        public string? Url { get; init; }

        /// <summary>
        /// Gets the action to retry the failed navigation.
        /// </summary>
        /// <value>
        /// An async function that attempts to repeat the failed routing operation, or null if retry is not available.
        /// </value>
        /// <remarks>
        /// <para>
        /// The Retry action provides a built-in mechanism for users to attempt the failed navigation again.
        /// This is particularly useful for transient errors such as:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Network failures during lazy component loading</description></item>
        /// <item><description>Temporary authentication token expiration</description></item>
        /// <item><description>Database connection timeouts in route guards</description></item>
        /// <item><description>Race conditions in component initialization</description></item>
        /// </list>
        /// <para>
        /// When invoked, this action attempts to re-execute the routing operation from the point of failure.
        /// If successful, the error UI is automatically dismissed and the requested component is displayed.
        /// If it fails again, a new error will be shown.
        /// </para>
        /// <para>
        /// <strong>UI Pattern:</strong> Present this as a "Try Again" or "Retry" button in your error UI.
        /// Consider adding loading indicators and disabling the button during retry to prevent multiple
        /// simultaneous retry attempts.
        /// </para>
        /// </remarks>
        /// <example>
        /// Retry button with loading state:
        /// <code>
        /// &lt;button @onclick="HandleRetry" disabled="@_isRetrying"&gt;
        ///     @if (_isRetrying)
        ///     {
        ///         &lt;span&gt;Retrying...&lt;/span&gt;
        ///     }
        ///     else
        ///     {
        ///         &lt;span&gt;Try Again&lt;/span&gt;
        ///     }
        /// &lt;/button&gt;
        /// 
        /// @code {
        ///     private bool _isRetrying = false;
        ///     
        ///     private async Task HandleRetry()
        ///     {
        ///         _isRetrying = true;
        ///         try
        ///         {
        ///             await errorInfo.Retry();
        ///         }
        ///         finally
        ///         {
        ///             _isRetrying = false;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public Func<Task>? Retry { get; init; }

        /// <summary>
        /// Gets the route path pattern that was being processed when the error occurred.
        /// </summary>
        /// <value>
        /// The route pattern string (e.g., "/users/:id"), or null if the error occurred before route matching.
        /// </value>
        /// <remarks>
        /// <para>
        /// This property contains the route pattern from the RouteConfig that was being processed. Route patterns
        /// include parameter placeholders (e.g., :id, :slug) and represent the template against which URLs are
        /// matched, not the actual URL itself.
        /// </para>
        /// <para>
        /// This information is primarily useful for developers during debugging to identify which route
        /// configuration is causing issues. In production error UI, consider showing the URL property instead
        /// as it's more meaningful to end users.
        /// </para>
        /// </remarks>
        public string? RoutePath { get; init; }

        /// <summary>
        /// Gets the type of the component that failed to load or render.
        /// </summary>
        /// <value>
        /// The component Type, or null if the error occurred before component determination or doesn't involve a specific component.
        /// </value>
        /// <remarks>
        /// <para>
        /// For ComponentLoading and ComponentRendering errors, this property identifies the specific component
        /// class that failed. For other error types (RouteMatching, GuardExecution, Navigation), this may be null
        /// since the error occurred before a component was selected.
        /// </para>
        /// <para>
        /// Use ComponentType.Name to get the simple class name for display, or ComponentType.FullName for the
        /// fully qualified name including namespace. This is helpful for debugging but should be shown primarily
        /// in development environments or debug builds.
        /// </para>
        /// </remarks>
        /// <example>
        /// Display component information conditionally:
        /// <code>
        /// @if (errorInfo.ComponentType != null)
        /// {
        ///     &lt;p&gt;Component: @errorInfo.ComponentType.Name&lt;/p&gt;
        ///     
        ///     #if DEBUG
        ///     &lt;p&gt;&lt;small&gt;Full name: @errorInfo.ComponentType.FullName&lt;/small&gt;&lt;/p&gt;
        ///     #endif
        /// }
        /// </code>
        /// </example>
        public Type? ComponentType { get; init; }

        /// <summary>
        /// Gets a user-friendly error message describing what went wrong.
        /// </summary>
        /// <value>
        /// The exception message string extracted from the underlying Exception.
        /// </value>
        /// <remarks>
        /// <para>
        /// This property provides the error message from the underlying exception. While not always perfectly
        /// user-friendly (as it comes directly from exception messages which are often technical), it provides
        /// a starting point for displaying error information.
        /// </para>
        /// <para>
        /// For production applications, consider mapping common exception messages to more user-friendly text,
        /// or using the ErrorType property to provide custom messages based on the category of error.
        /// </para>
        /// </remarks>
        public string Message => Exception.Message;

        /// <summary>
        /// Gets the category of routing error that occurred.
        /// </summary>
        /// <value>
        /// A <see cref="RouterErrorType"/> enum value indicating the phase of routing where the error occurred.
        /// </value>
        /// <remarks>
        /// <para>
        /// Use this property to customize error messages and recovery options based on the type of failure.
        /// Different error types warrant different user messaging and actions:
        /// </para>
        /// <list type="bullet">
        /// <item><description><b>ComponentLoading</b> - Suggest retry, check network connection</description></item>
        /// <item><description><b>GuardExecution</b> - Suggest login, explain permission requirements</description></item>
        /// <item><description><b>ComponentRendering</b> - Technical error, suggest contact support</description></item>
        /// <item><description><b>RouteMatching</b> - Invalid URL, suggest return to home</description></item>
        /// <item><description><b>Navigation</b> - Generic navigation failure, provide retry option</description></item>
        /// </list>
        /// </remarks>
        public RouterErrorType ErrorType { get; init; }

        /// <summary>
        /// Gets the underlying exception that caused the routing error.
        /// </summary>
        /// <value>
        /// The <see cref="Exception"/> object containing complete error information.
        /// </value>
        /// <remarks>
        /// This property provides access to the raw exception for detailed error analysis, logging, or
        /// custom error presentation logic. Use Exception.Message for the error message, and Exception.StackTrace
        /// or Exception.ToString() for debugging information.
        /// </remarks>
        public Exception Exception { get; init; } = null!;

        /// <summary>
        /// Gets the complete exception details including type, message, and stack trace.
        /// </summary>
        /// <value>
        /// A formatted string containing the full exception information from Exception.ToString().
        /// </value>
        /// <remarks>
        /// <para>
        /// This property provides the complete exception details in a formatted string, including the exception
        /// type, message, stack trace, and any inner exceptions. This is extremely useful for debugging but
        /// contains technical information that should generally not be shown to end users in production.
        /// </para>
        /// <para>
        /// <strong>Recommended Usage:</strong>
        /// </para>
        /// <list type="bullet">
        /// <item><description>Display in development builds for debugging (#if DEBUG)</description></item>
        /// <item><description>Log to error tracking services</description></item>
        /// <item><description>Include in support tickets or error reports</description></item>
        /// <item><description>Show in expandable "Technical Details" sections for advanced users</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Show full details in debug mode only:
        /// <code>
        /// #if DEBUG
        /// &lt;details class="error-details"&gt;
        ///     &lt;summary&gt;Technical Details (Debug Only)&lt;/summary&gt;
        ///     &lt;pre&gt;@errorInfo.FullDetails&lt;/pre&gt;
        /// &lt;/details&gt;
        /// #endif
        /// </code>
        /// </example>
        public string FullDetails => Exception.ToString();
    }
}