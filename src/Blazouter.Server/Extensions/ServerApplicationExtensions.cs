using Microsoft.AspNetCore.Builder;

namespace Blazouter.Server.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Blazouter in Blazor Server applications.
    /// </summary>
    /// <remarks>
    /// These extensions are specifically designed for Blazor Server hosting scenarios and ensure
    /// that Blazouter components are properly registered and discoverable by the Blazor Server runtime.
    /// </remarks>
    public static class ServerApplicationExtensions
    {
        /// <summary>
        /// Adds Blazouter component support to the Razor Components endpoint configuration.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="RazorComponentsEndpointConventionBuilder"/> to configure.
        /// </param>
        /// <returns>
        /// The same <see cref="RazorComponentsEndpointConventionBuilder"/> so that additional calls can be chained.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method must be called when configuring Razor Components endpoints in Blazor Server applications
        /// to ensure that Blazouter's internal components are properly discovered and
        /// can be used in routing.
        /// </para>
        /// <para>
        /// The method registers the Blazouter.Server assembly, making its components available to the
        /// Blazor Server runtime. Without this registration, Blazouter-specific server components may not
        /// be recognized, leading to runtime errors.
        /// </para>
        /// </remarks>
        /// <example>
        /// Configure Blazouter support in a Blazor Server application:
        /// <code>
        /// // In Program.cs
        /// app.MapRazorComponents&lt;App&gt;()
        ///     .AddBlazouterSupport()  // Add this line for Blazouter
        ///     .AddInteractiveServerRenderMode();
        /// </code>
        /// </example>
        /// <example>
        /// Complete Blazor Server setup with Blazouter:
        /// <code>
        /// // Program.cs
        /// var builder = WebApplication.CreateBuilder(args);
        /// 
        /// builder.Services.AddRazorComponents()
        ///     .AddInteractiveServerComponents();
        /// 
        /// builder.Services.AddBlazouter(); // Add Blazouter services
        /// 
        /// var app = builder.Build();
        /// 
        /// app.MapRazorComponents&lt;App&gt;()
        ///     .AddBlazouterSupport()  // Required for Blazor Server
        ///     .AddInteractiveServerRenderMode();
        /// 
        /// app.Run();
        /// </code>
        /// </example>
        public static RazorComponentsEndpointConventionBuilder AddBlazouterSupport(this RazorComponentsEndpointConventionBuilder builder)
        {
            return builder.AddAdditionalAssemblies(typeof(Pages.Base).Assembly);
        }
    }
}