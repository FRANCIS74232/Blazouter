using Blazouter.Services;

namespace Blazouter.Models
{
    /// <summary>
    /// Provides data for the OnError event.
    /// </summary>
    public class RouterErrorEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the error has been handled and
        /// the default error UI should be suppressed.
        /// </summary>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the exception that caused the routing error.
        /// </summary>
        public Exception Exception { get; init; } = null!;

        /// <summary>
        /// Gets contextual information about the routing error.
        /// </summary>
        public RouterErrorContext Context { get; init; } = null!;
    }
}