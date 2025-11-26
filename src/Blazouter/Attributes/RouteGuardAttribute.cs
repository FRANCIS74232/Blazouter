using Blazouter.Interfaces;

namespace Blazouter.Attributes
{
    /// <summary>
    /// Specifies a route guard that controls access to a route.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows declarative configuration of route guards on component classes. Guards
    /// provide a powerful mechanism to control route access based on authentication, authorization,
    /// feature flags, data availability, or any custom logic. Multiple guards can be applied to a
    /// single route by using this attribute multiple times - all guards must pass for navigation
    /// to succeed.
    /// </para>
    /// <para>
    /// <strong>Execution Order:</strong> Guards are executed sequentially in the order they are declared
    /// on the component. If any guard fails (returns false from CanActivateAsync), navigation is blocked
    /// and subsequent guards are not executed. The first failing guard's GetRedirectPathAsync determines
    /// where the user is redirected.
    /// </para>
    /// <para>
    /// <strong>Guard Resolution:</strong> The router first attempts to resolve guards from the dependency
    /// injection container. If not found, it tries to create an instance using Activator.CreateInstance.
    /// For guards with dependencies, register them in DI during application startup.
    /// </para>
    /// <para>
    /// Common use cases for route guards:
    /// </para>
    /// <list type="bullet">
    /// <item><description><strong>Authentication:</strong> Verify user is logged in before accessing protected routes</description></item>
    /// <item><description><strong>Authorization:</strong> Check user has required roles, permissions, or claims</description></item>
    /// <item><description><strong>Feature flags:</strong> Enable/disable routes based on feature toggles</description></item>
    /// <item><description><strong>Data validation:</strong> Ensure required data exists before showing a route</description></item>
    /// <item><description><strong>Age gates:</strong> Verify user age for age-restricted content</description></item>
    /// <item><description><strong>Subscription checks:</strong> Verify user has active subscription for premium features</description></item>
    /// <item><description><strong>Unsaved changes:</strong> Warn users before leaving forms with unsaved data</description></item>
    /// </list>
    /// <para>
    /// The guard type must implement the <see cref="IRouteGuard"/> interface, which defines two methods:
    /// CanActivateAsync (determines if navigation is allowed) and GetRedirectPathAsync (specifies where
    /// to redirect if access is denied).
    /// </para>
    /// </remarks>
    /// <example>
    /// Single guard for authentication:
    /// <code>
    /// [Route("/profile")]
    /// [RouteGuard(typeof(AuthGuard))]
    /// public class ProfilePage : ComponentBase
    /// {
    ///     // Only accessible to authenticated users
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Multiple guards executed in order:
    /// <code>
    /// [Route("/admin")]
    /// [RouteGuard(typeof(AuthGuard))]        // First: Check if logged in
    /// [RouteGuard(typeof(AdminRoleGuard))]   // Second: Check if admin role
    /// [RouteGuard(typeof(SubscriptionGuard))] // Third: Check subscription
    /// public class AdminPage : ComponentBase
    /// {
    ///     // All three guards must pass
    /// }
    /// </code>
    /// </example>
    /// <example>
    /// Custom guard implementation:
    /// <code>
    /// public class SubscriptionGuard : IRouteGuard
    /// {
    ///     private readonly ISubscriptionService _subscriptionService;
    ///     
    ///     public SubscriptionGuard(ISubscriptionService subscriptionService)
    /// {
    ///         _subscriptionService = subscriptionService;
    ///     }
    ///     
    ///     public async Task&lt;bool&gt; CanActivateAsync(RouteMatch match)
    ///     {
    ///         return await _subscriptionService.IsActiveAsync();
    ///     }
    ///     
    ///     public Task&lt;string?&gt; GetRedirectPathAsync(RouteMatch match)
    ///     {
    ///         return Task.FromResult&lt;string?&gt;("/subscription/upgrade");
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RouteGuardAttribute"/> class.
    /// </remarks>
    /// <param name="guardType">The type of the route guard. Must implement IRouteGuard.</param>
    /// <exception cref="ArgumentNullException">Thrown when guardType is null.</exception>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RouteGuardAttribute(Type guardType) : Attribute
    {
        /// <summary>
        /// Gets the type of the route guard.
        /// </summary>
        /// <value>
        /// A Type that implements <see cref="IRouteGuard"/>. This type will be instantiated or resolved
        /// from dependency injection when the route is accessed.
        /// </value>
        /// <remarks>
        /// The guard type is resolved in the following order:
        /// 1. First, attempts to get the guard from the dependency injection container
        /// 2. If not registered in DI, attempts to create using Activator.CreateInstance
        /// 3. If both fail, the guard is skipped (navigation is allowed)
        /// 
        /// For guards with dependencies, register them in DI during application startup:
        /// <code>services.AddScoped&lt;MyCustomGuard&gt;();</code>
        /// </remarks>
        public Type GuardType { get; } = guardType ?? throw new ArgumentNullException(nameof(guardType));
    }
}