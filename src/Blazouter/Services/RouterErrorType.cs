namespace Blazouter.Services
{
    /// <summary>
    /// Defines the types of errors that can occur during routing operations.
    /// </summary>
    public enum RouterErrorType
    {
        /// <summary>
        /// An unspecified error occurred.
        /// </summary>
        Unknown,

        /// <summary>
        /// An error occurred during navigation operations.
        /// </summary>
        Navigation,

        /// <summary>
        /// An error occurred during route matching.
        /// </summary>
        RouteMatching,

        /// <summary>
        /// An error occurred while executing a route guard.
        /// </summary>
        GuardExecution,

        /// <summary>
        /// An error occurred while lazy loading a component.
        /// </summary>
        ComponentLoading,

        /// <summary>
        /// An error occurred while rendering a route component.
        /// </summary>
        ComponentRendering
    }
}