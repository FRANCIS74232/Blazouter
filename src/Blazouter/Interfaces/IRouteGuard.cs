using Blazouter.Models;

namespace Blazouter.Interfaces
{
    /// <summary>
    /// Defines a contract for route guards that control access to routes based on custom logic.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Route guards provide a declarative way to protect routes and control navigation. Guards are executed
    /// before rendering a route's component, allowing you to implement authentication, authorization,
    /// data validation, or any other pre-navigation logic.
    /// </para>
    /// <para>
    /// Guards can be registered in dependency injection or instantiated directly. Multiple guards can be
    /// applied to a single route, and they are executed sequentially in the order specified. All guards
    /// must pass for navigation to proceed.
    /// </para>
    /// <para>
    /// Common use cases include:
    /// - Authentication checks (ensuring user is logged in)
    /// - Authorization checks (ensuring user has required permissions)
    /// - Data validation (ensuring required data exists)
    /// - Unsaved changes warnings (preventing navigation away from forms)
    /// - Feature flag checks (controlling access to experimental features)
    /// </para>
    /// </remarks>
    /// <example>
    /// Simple authentication guard:
    /// <code>
    /// public class AuthGuard : IRouteGuard
    /// {
    ///     private readonly AuthenticationStateProvider _authProvider;
    ///     
    ///     public AuthGuard(AuthenticationStateProvider authProvider)
    ///     {
    ///         _authProvider = authProvider;
    ///     }
    ///     
    ///     public async Task&lt;bool&gt; CanActivateAsync(RouteMatch match)
    ///     {
    ///         var authState = await _authProvider.GetAuthenticationStateAsync();
    ///         return authState.User.Identity?.IsAuthenticated ?? false;
    ///     }
    ///     
    ///     public Task&lt;string?&gt; GetRedirectPathAsync(RouteMatch match)
    ///     {
    ///         return Task.FromResult&lt;string?&gt;("/login");
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IRouteGuard
    {
        /// <summary>
        /// Determines whether navigation to the specified route should be allowed.
        /// </summary>
        /// <param name="match">
        /// The <see cref="RouteMatch"/> object containing information about the route being navigated to,
        /// including route parameters, query parameters, and the route configuration.
        /// </param>
        /// <returns>
        /// A task that resolves to true if navigation should proceed, or false if navigation should be blocked.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is called before the route's component is rendered. Implementations can perform
        /// asynchronous operations such as API calls, database queries, or other validation logic.
        /// </para>
        /// <para>
        /// If this method returns false, navigation is blocked and the <see cref="GetRedirectPathAsync"/>
        /// method is called to determine where to redirect (if anywhere).
        /// </para>
        /// <para>
        /// Guards should handle exceptions gracefully and return false in error scenarios to prevent
        /// navigation to potentially broken or inaccessible routes.
        /// </para>
        /// </remarks>
        Task<bool> CanActivateAsync(RouteMatch match);

        /// <summary>
        /// Gets the path to redirect to when navigation is not allowed.
        /// </summary>
        /// <param name="match">
        /// The <see cref="RouteMatch"/> object containing information about the blocked route navigation attempt.
        /// </param>
        /// <returns>
        /// A task that resolves to a redirect path string, or null to remain on the current route.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method is only called when <see cref="CanActivateAsync"/> returns false. The returned path
        /// should be a valid route in the application. Returning null will prevent navigation but keep the
        /// user on their current route.
        /// </para>
        /// <para>
        /// The default implementation returns "/" (root path). Override this method to provide custom
        /// redirect behavior, such as redirecting to a login page or an access denied page.
        /// </para>
        /// <para>
        /// You can use information from the RouteMatch to create dynamic redirects. For example, you could
        /// append a return URL to redirect back after successful authentication.
        /// </para>
        /// </remarks>
        /// <example>
        /// Redirect to login with return URL:
        /// <code>
        /// public Task&lt;string?&gt; GetRedirectPathAsync(RouteMatch match)
        /// {
        ///     string returnUrl = Uri.EscapeDataString(match.MatchedPath);
        ///     return Task.FromResult&lt;string?&gt;($"/login?returnUrl={returnUrl}");
        /// }
        /// </code>
        /// </example>
        Task<string?> GetRedirectPathAsync(RouteMatch match)
        {
            return Task.FromResult<string?>("/");
        }
    }
}