using Blazouter.Extensions;

namespace Blazouter.Hybrid.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Blazouter in .NET MAUI Blazor Hybrid applications.
    /// </summary>
    /// <remarks>
    /// These extensions are specifically designed for .NET MAUI Hybrid hosting scenarios, enabling
    /// Blazouter routing in native mobile and desktop applications that host Blazor components.
    /// </remarks>
    public static class HybridApplicationExtensions
    {
        /// <summary>
        /// Adds Blazouter routing support to a .NET MAUI application.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="MauiAppBuilder"/> to configure.
        /// </param>
        /// <returns>
        /// The same <see cref="MauiAppBuilder"/> so that additional calls can be chained.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method configures all necessary Blazouter services for use in a .NET MAUI Blazor Hybrid
        /// application. It registers route matching, state management, and navigation services that work
        /// seamlessly with the MAUI WebView hosting model.
        /// </para>
        /// <para>
        /// Blazouter in MAUI Hybrid applications supports:
        /// </para>
        /// <list type="bullet">
        /// <item><description>Native platform navigation integration</description></item>
        /// <item><description>Deep linking support</description></item>
        /// <item><description>Platform-specific optimizations for iOS, Android, macOS, and Windows</description></item>
        /// <item><description>All standard Blazouter features (nested routes, guards, lazy loading, transitions)</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Configure Blazouter in a MAUI application:
        /// <code>
        /// // In MauiProgram.cs
        /// public static class MauiProgram
        /// {
        ///     public static MauiApp CreateMauiApp()
        ///     {
        ///         var builder = MauiApp.CreateBuilder();
        ///         builder
        ///             .UseMauiApp&lt;App&gt;()
        ///             .ConfigureFonts(fonts =>
        ///             {
        ///                 fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        ///             });
        /// 
        ///         builder.Services.AddMauiBlazorWebView();
        ///         
        ///         #if DEBUG
        ///         builder.Services.AddBlazorWebViewDeveloperTools();
        ///         #endif
        ///         
        ///         // Add Blazouter support
        ///         builder.AddBlazouterSupport();
        /// 
        ///         return builder.Build();
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// Alternative configuration using the core extension method:
        /// <code>
        /// // Both approaches are equivalent
        /// builder.AddBlazouterSupport();
        /// // or
        /// builder.Services.AddBlazouter();
        /// </code>
        /// </example>
        public static MauiAppBuilder AddBlazouterSupport(this MauiAppBuilder builder)
        {
            builder.Services.AddBlazouter();

            return builder;
        }
    }
}