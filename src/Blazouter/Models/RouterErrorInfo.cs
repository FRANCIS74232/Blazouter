using Blazouter.Services;

namespace Blazouter.Models
{
    /// <summary>
    /// Provides information about a routing error for display in error UI.
    /// </summary>
    public class RouterErrorInfo
    {
        /// <summary>
        /// Gets the URL that was being navigated to when the error occurred.
        /// </summary>
        public string? Url { get; init; }

        /// <summary>
        /// Gets the action to retry the failed navigation.
        /// </summary>
        public Func<Task>? Retry { get; init; }

        /// <summary>
        /// Gets the route path that was being processed.
        /// </summary>
        public string? RoutePath { get; init; }

        /// <summary>
        /// Gets the type of component that failed.
        /// </summary>
        public Type? ComponentType { get; init; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message => Exception.Message;

        /// <summary>
        /// Gets the type of routing error that occurred.
        /// </summary>
        public RouterErrorType ErrorType { get; init; }

        /// <summary>
        /// Gets the exception that caused the routing error.
        /// </summary>
        public Exception Exception { get; init; } = null!;

        /// <summary>
        /// Gets the full exception details including stack trace.
        /// </summary>
        public string FullDetails => Exception.ToString();
    }
}