using Blazouter.Extensions;
using Blazouter.Web.Client.Sample.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazouter.Web.Client.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Register custom error handler for routing errors
            builder.Services.AddBlazouterErrorHandler<CustomRouterErrorHandler>();

            await builder.Build().RunAsync();
        }
    }
}