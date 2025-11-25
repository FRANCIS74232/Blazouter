using Blazouter.Guards;
using Blazouter.Models;

namespace Blazouter.WebAssembly.Sample.Guards
{
    /// <summary>
    /// Sample authentication guard for demonstration
    /// </summary>
    public class AuthenticationGuard : IRouteGuard
    {
        public Task<bool> CanActivateAsync(RouteMatch match)
        {
            // In a real application, you would check authentication status here
            // For demo purposes, we'll allow access
            return Task.FromResult(true);
        }

        public Task<string?> GetRedirectPathAsync(RouteMatch match)
        {
            // In a real application, redirect to login page if not authenticated
            return Task.FromResult<string?>("/");
        }
    }
}