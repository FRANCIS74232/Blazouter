namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies one or more middleware types to execute during navigation to a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute enables declarative middleware configuration on components used as routes.
    /// Multiple middleware can be applied to a single route by using multiple RouteMiddleware attributes.
    /// Middleware are executed in the order they are declared, before route guards.
    /// </para>
    /// <para>
    /// Middleware provide a way to execute code before and after route navigation, enabling cross-cutting
    /// concerns like logging, analytics, data preloading, caching, and more. Unlike guards which focus
    /// on access control, middleware can perform arbitrary logic and modify the navigation context.
    /// </para>
    /// <para>
    /// The middleware types specified must implement the IRouteMiddleware interface. Middleware can be
    /// registered in dependency injection or will be instantiated using Activator.CreateInstance.
    /// </para>
    /// </remarks>
    /// <example>
    /// Using RouteMiddleware attribute:
    /// <code>
    /// [Route("/admin")]
    /// [RouteMiddleware(typeof(LoggingMiddleware))]
    /// [RouteMiddleware(typeof(TimingMiddleware))]
    /// [RouteMiddleware(typeof(AnalyticsMiddleware))]
    /// [RouteGuard(typeof(AuthGuard))]
    /// public class AdminPage : ComponentBase
    /// {
    ///     // Component implementation
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteMiddlewareAttribute"/> class.
    /// </remarks>
    /// <param name="middlewareType">
    /// The type of the middleware to execute. Must implement IRouteMiddleware.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="middlewareType"/> is null.
    /// </exception>
    /// <remarks>
    /// The middleware type will be instantiated during navigation either through dependency
    /// injection (if registered) or via Activator.CreateInstance. Ensure the middleware has
    /// a public parameterless constructor if not registered in DI.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RouteMiddlewareAttribute(Type middlewareType) : Attribute
    {
        /// <summary>
        /// Gets the type of the middleware to execute.
        /// </summary>
        /// <value>
        /// A Type that implements IRouteMiddleware interface.
        /// </value>
        public Type MiddlewareType { get; } = middlewareType ?? throw new ArgumentNullException(nameof(middlewareType));
    }
}