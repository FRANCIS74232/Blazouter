namespace Blazouter.Models
{
    /// <summary>
    /// Represents the context passed to route middleware during navigation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The RouteMiddlewareContext provides middleware with access to route information,
    /// navigation state, and the ability to store and retrieve data that can be shared
    /// between middleware and components.
    /// </para>
    /// <para>
    /// Middleware can use this context to:
    /// - Access route parameters and query strings
    /// - Store data for components to consume
    /// - Track navigation state and history
    /// - Implement cross-cutting concerns like logging, analytics, or data loading
    /// </para>
    /// </remarks>
    /// <example>
    /// Using context to store data for components:
    /// <code>
    /// public class DataLoadingMiddleware : IRouteMiddleware
    /// {
    ///     public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
    ///     {
    ///         // Load data before navigation
    ///         var userData = await LoadUserDataAsync(context.Match.Params["id"]);
    ///         context.Data["UserData"] = userData;
    ///         
    ///         await next();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class RouteMiddlewareContext
    {
        /// <summary>
        /// Gets or sets the route match information for the current navigation.
        /// </summary>
        /// <value>
        /// A <see cref="RouteMatch"/> object containing route parameters, path information,
        /// and the matched route configuration.
        /// </value>
        /// <remarks>
        /// The RouteMatch provides access to dynamic route parameters, query strings,
        /// and the full route configuration. This is useful for middleware that needs
        /// to make decisions based on the destination route.
        /// </remarks>
        public RouteMatch Match { get; set; } = null!;

        /// <summary>
        /// Gets or sets the absolute path being navigated to.
        /// </summary>
        /// <value>
        /// A string representing the full URL path including query parameters.
        /// </value>
        /// <remarks>
        /// This is the raw path from the browser URL, including any query strings
        /// or fragments. Use this for logging, analytics, or debugging purposes.
        /// </remarks>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a dictionary for storing arbitrary data that can be shared
        /// between middleware and accessed by components.
        /// </summary>
        /// <value>
        /// A dictionary mapping string keys to object values. Defaults to an empty dictionary.
        /// </value>
        /// <remarks>
        /// <para>
        /// This data store allows middleware to share information with subsequent middleware
        /// in the pipeline and with the destination component. Common uses include:
        /// - Pre-loading data for components
        /// - Storing authentication/authorization information
        /// - Tracking analytics or telemetry data
        /// - Caching computed values
        /// </para>
        /// <para>
        /// Data stored here is available for the duration of the navigation and can be
        /// accessed by components through dependency injection or passed as parameters.
        /// </para>
        /// </remarks>
        /// <example>
        /// Storing and retrieving data:
        /// <code>
        /// // In middleware
        /// context.Data["Timestamp"] = DateTime.UtcNow;
        /// context.Data["UserId"] = currentUserId;
        /// 
        /// // In component
        /// var timestamp = context.Data["Timestamp"] as DateTime?;
        /// </code>
        /// </example>
        public Dictionary<string, object> Data { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether navigation should be aborted.
        /// </summary>
        /// <value>
        /// true to abort navigation; false to continue with the navigation pipeline. Defaults to false.
        /// </value>
        /// <remarks>
        /// <para>
        /// When set to true, the middleware pipeline stops executing and navigation is cancelled.
        /// The user remains on the current page. This is useful for implementing conditional
        /// navigation or validation logic.
        /// </para>
        /// <para>
        /// If you want to redirect instead of just aborting, use the <see cref="RedirectPath"/>
        /// property to specify where to redirect.
        /// </para>
        /// </remarks>
        /// <example>
        /// Aborting navigation based on validation:
        /// <code>
        /// public Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
        /// {
        ///     if (!IsValidRequest(context.Path))
        ///     {
        ///         context.Abort = true;
        ///         return Task.CompletedTask;
        ///     }
        ///     
        ///     return next();
        /// }
        /// </code>
        /// </example>
        public bool Abort { get; set; } = false;

        /// <summary>
        /// Gets or sets the path to redirect to when aborting navigation.
        /// </summary>
        /// <value>
        /// A string representing the redirect path, or null to remain on the current page.
        /// </value>
        /// <remarks>
        /// <para>
        /// When both <see cref="Abort"/> is true and RedirectPath is specified, navigation
        /// to the original destination is cancelled and the browser is redirected to the
        /// specified path instead.
        /// </para>
        /// <para>
        /// If Abort is true but RedirectPath is null, the navigation is simply cancelled
        /// without any redirect occurring.
        /// </para>
        /// </remarks>
        /// <example>
        /// Redirecting to login when unauthorized:
        /// <code>
        /// public async Task InvokeAsync(RouteMiddlewareContext context, Func&lt;Task&gt; next)
        /// {
        ///     if (!await IsAuthorizedAsync())
        ///     {
        ///         context.Abort = true;
        ///         context.RedirectPath = "/login";
        ///         return;
        ///     }
        ///     
        ///     await next();
        /// }
        /// </code>
        /// </example>
        public string? RedirectPath { get; set; } = null;
    }
}