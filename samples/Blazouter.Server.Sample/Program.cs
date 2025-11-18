using Blazouter.Extensions;
using Blazouter.Server.Extensions;
using Blazouter.Server.Sample.Components;
using Blazouter.Server.Sample.Services;

namespace Blazouter.Server.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Add Blazouter services
            builder.Services.AddBlazouter();

            // Register custom error handler for routing errors
            builder.Services.AddBlazouterErrorHandler<CustomRouterErrorHandler>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            app.MapRazorComponents<App>()
                .AddBlazouterSupport()  // Required for Server mode
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}