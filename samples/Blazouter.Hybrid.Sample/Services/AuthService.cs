namespace Blazouter.Hybrid.Sample.Services
{
    /// <summary>
    /// Simple authentication service for demonstration purposes.
    /// In a real application, this would connect to an actual authentication provider.
    /// </summary>
    public class AuthService
    {
        /// <summary>
        /// Gets whether the user is currently authenticated.
        /// </summary>
        public bool IsAuthenticated { get; private set; } = true;

        /// <summary>
        /// Simulates logging in a user.
        /// </summary>
        public void Login()
        {
            IsAuthenticated = true;
        }

        /// <summary>
        /// Simulates logging out a user.
        /// </summary>
        public void Logout()
        {
            IsAuthenticated = false;
        }
    }
}