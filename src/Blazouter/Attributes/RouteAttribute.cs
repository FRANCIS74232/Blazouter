namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies the route path for a component.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute enables declarative route configuration on component classes as an alternative
    /// to programmatic route configuration using RouteConfig objects. The component decorated with
    /// this attribute will be automatically registered in the routing system.
    /// </para>
    /// <para>
    /// Path patterns support dynamic parameters using the ':' prefix (e.g., "/users/:id").
    /// </para>
    /// <para>
    /// Note: This attribute is named RouteAttribute to avoid conflicts with the built-in
    /// Microsoft.AspNetCore.Components.RouteAttribute. You can still use it as [Route("/path")].
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [Route("/admin")]
    /// [RouteGuard(typeof(AuthGuard))]
    /// public class AdminPage : ComponentBase
    /// {
    ///     // Component implementation
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteAttribute"/> class.
    /// </remarks>
    /// <param name="path">The route path pattern.</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RouteAttribute(string path) : Attribute
    {
        /// <summary>
        /// Gets the path pattern for this route.
        /// </summary>
        /// <value>
        /// A string representing the URL pattern. Can include dynamic parameters prefixed with ':' (e.g., "/users/:id").
        /// </value>
        public string Path { get; } = path ?? throw new ArgumentNullException(nameof(path));
    }
}