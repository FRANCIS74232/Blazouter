using Microsoft.AspNetCore.Builder;

namespace Blazouter.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring Blazouter in Blazor Web App applications.
    /// </summary>
    /// <remarks>
    /// These extensions are specifically designed for the Blazor Web App hosting model (.NET 8+)
    /// that supports both Server and WebAssembly render modes (InteractiveServer, InteractiveWebAssembly, 
    /// and InteractiveAuto). This ensures that Blazouter components are properly registered and 
    /// discoverable by the Blazor runtime in all render modes.
    /// </remarks>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Adds Blazouter component support to the Razor Components endpoint configuration for Blazor Web Apps.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="RazorComponentsEndpointConventionBuilder"/> to configure.
        /// </param>
        /// <returns>
        /// The same <see cref="RazorComponentsEndpointConventionBuilder"/> so that additional calls can be chained.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method must be called when configuring Razor Components endpoints in Blazor Web App applications
        /// to ensure that Blazouter's internal components are properly discovered and can be used in routing
        /// across all render modes (InteractiveServer, InteractiveWebAssembly, and InteractiveAuto).
        /// </para>
        /// <para>
        /// The method registers the Blazouter.Web assembly, making its components available to the
        /// Blazor runtime. Without this registration, Blazouter-specific components may not be recognized,
        /// leading to runtime errors.
        /// </para>
        /// <para>
        /// This extension supports the unified Blazor Web App model where components can render with:
        /// </para>
        /// <list type="bullet">
        /// <item><description><b>InteractiveServer</b> - Server-side rendering with SignalR</description></item>
        /// <item><description><b>InteractiveWebAssembly</b> - Client-side rendering with WebAssembly</description></item>
        /// <item><description><b>InteractiveAuto</b> - Automatic selection between Server and WebAssembly</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Configure Blazouter support in a Blazor Web App:
        /// <code>
        /// // In Program.cs
        /// app.MapRazorComponents&lt;App&gt;()
        ///     .AddBlazouterSupport()  // Add this line for Blazouter
        ///     .AddInteractiveServerRenderMode()
        ///     .AddInteractiveWebAssemblyRenderMode();
        /// </code>
        /// </example>
        /// <example>
        /// Complete Blazor Web App setup with Blazouter:
        /// <code>
        /// // Program.cs
        /// var builder = WebApplication.CreateBuilder(args);
        /// 
        /// builder.Services.AddRazorComponents()
        ///     .AddInteractiveServerComponents()
        ///     .AddInteractiveWebAssemblyComponents();
        /// 
        /// builder.Services.AddBlazouter(); // Add Blazouter services
        /// 
        /// var app = builder.Build();
        /// 
        /// app.MapRazorComponents&lt;App&gt;()
        ///     .AddBlazouterSupport()  // Required for Blazor Web App
        ///     .AddInteractiveServerRenderMode()
        ///     .AddInteractiveWebAssemblyRenderMode();
        /// 
        /// app.Run();
        /// </code>
        /// </example>
        /// <example>
        /// Using with InteractiveAuto render mode:
        /// <code>
        /// // Program.cs
        /// var builder = WebApplication.CreateBuilder(args);
        /// 
        /// builder.Services.AddRazorComponents()
        ///     .AddInteractiveServerComponents()
        ///     .AddInteractiveWebAssemblyComponents();
        /// 
        /// builder.Services.AddBlazouter();
        /// 
        /// var app = builder.Build();
        /// 
        /// app.MapRazorComponents&lt;App&gt;()
        ///     .AddBlazouterSupport()
        ///     .AddInteractiveServerRenderMode()
        ///     .AddInteractiveWebAssemblyRenderMode();
        /// 
        /// app.Run();
        /// 
        /// // In App.razor or Routes.razor
        /// &lt;Routes @rendermode="InteractiveAuto" /&gt;
        /// </code>
        /// </example>
        public static RazorComponentsEndpointConventionBuilder AddBlazouterSupport(this RazorComponentsEndpointConventionBuilder builder)
        {
            return builder.AddAdditionalAssemblies(typeof(Pages.Base).Assembly);
        }
    }
}