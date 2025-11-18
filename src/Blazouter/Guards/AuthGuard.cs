using Blazouter.Models;

namespace Blazouter.Guards
{
    /// <summary>
    /// Provides a basic implementation of <see cref="IRouteGuard"/> for authentication scenarios.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is an example guard demonstrating how to implement authentication checks. In production,
    /// you should create your own guard that integrates with your authentication system (e.g., ASP.NET Core Identity,
    /// Azure AD, Auth0, etc.).
    /// </para>
    /// <para>
    /// The guard uses a function delegate to determine authentication status, making it flexible and testable.
    /// The default constructor creates a guard that always denies access, which is safe for demonstration purposes.
    /// </para>
    /// </remarks>
    /// <example>
    /// Using AuthGuard with a custom authentication function:
    /// <code>
    /// var authGuard = new AuthGuard(async () => 
    /// {
    ///     var authState = await authStateProvider.GetAuthenticationStateAsync();
    ///     return authState.User.Identity?.IsAuthenticated ?? false;
    /// });
    /// </code>
    /// </example>
    /// <example>
    /// Applying AuthGuard to a route:
    /// <code>
    /// new RouteConfig
    /// {
    ///     Path = "/admin",
    ///     Component = typeof(AdminPage),
    ///     Guards = new List&lt;Type&gt; { typeof(AuthGuard) }
    /// }
    /// </code>
    /// </example>
    public class AuthGuard(Func<Task<bool>> isAuthenticatedFunc) : IRouteGuard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthGuard"/> class with default behavior.
        /// </summary>
        /// <remarks>
        /// The default constructor creates a guard that always denies access (returns false).
        /// This is a safe default for demonstration purposes. In production, use the parameterized
        /// constructor or create a custom guard implementation.
        /// </remarks>
        public AuthGuard() : this(() => Task.FromResult(false)) { }

        /// <summary>
        /// Determines if the user is authenticated and can access the protected route.
        /// </summary>
        /// <param name="match">The route match information for the navigation attempt.</param>
        /// <returns>
        /// A task that resolves to true if the user is authenticated, false otherwise.
        /// </returns>
        /// <remarks>
        /// This method executes the authentication function provided during construction.
        /// The function is expected to perform the actual authentication check against your
        /// authentication system.
        /// </remarks>
        public async Task<bool> CanActivateAsync(RouteMatch match)
        {
            return await isAuthenticatedFunc();
        }

        /// <summary>
        /// Gets the redirect path when authentication fails.
        /// </summary>
        /// <param name="match">The route match information for the blocked navigation.</param>
        /// <returns>
        /// A task that resolves to "/login", directing unauthenticated users to the login page.
        /// </returns>
        /// <remarks>
        /// This implementation redirects to a hardcoded "/login" path. In production scenarios,
        /// consider creating a custom guard that can:
        /// - Redirect to a configurable login page
        /// - Include a return URL parameter
        /// - Handle different authentication failure scenarios
        /// </remarks>
        public Task<string?> GetRedirectPathAsync(RouteMatch match)
        {
            return Task.FromResult<string?>("/login");
        }
    }
}