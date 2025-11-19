using Blazouter.Extensions;
using Blazouter.WebAssembly.Sample.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazouter.WebAssembly.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Add Blazouter services
            builder.Services.AddBlazouter();

            // Register sample services
            builder.Services.AddSingleton<AuthService>();

            // Register custom error handler for routing errors
            builder.Services.AddBlazouterErrorHandler<CustomRouterErrorHandler>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}