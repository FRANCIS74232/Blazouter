namespace Blazouter.Services
{
    /// <summary>
    /// Provides contextual information about a routing error.
    /// </summary>
    /// <remarks>
    /// This class contains metadata about where and when a routing error occurred,
    /// helping error handlers make informed decisions about how to handle the error.
    /// </remarks>
    public class RouterErrorContext
    {
        /// <summary>
        /// Gets or sets the URL that was being navigated to when the error occurred.
        /// </summary>
        /// <value>
        /// The absolute or relative URL string, or null if not applicable.
        /// </value>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the route path pattern that was being matched or processed.
        /// </summary>
        /// <value>
        /// The route pattern string (e.g., "/users/:id"), or null if not applicable.
        /// </value>
        public string? RoutePath { get; set; }

        /// <summary>
        /// Gets or sets the type of the component that failed to load or render.
        /// </summary>
        /// <value>
        /// The component type, or null if the error didn't involve a specific component.
        /// </value>
        public Type? ComponentType { get; set; }

        /// <summary>
        /// Gets or sets the type of routing operation that failed.
        /// </summary>
        /// <value>
        /// A value from the <see cref="RouterErrorType"/> enumeration indicating
        /// the phase of routing where the error occurred.
        /// </value>
        public RouterErrorType ErrorType { get; set; }

        /// <summary>
        /// Gets or sets additional custom data related to the error.
        /// </summary>
        /// <value>
        /// A dictionary of key-value pairs with additional context, or an empty dictionary.
        /// </value>
        public Dictionary<string, object> AdditionalData { get; set; } = [];
    }
}