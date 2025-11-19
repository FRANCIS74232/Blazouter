using Blazouter.Extensions;
using Blazouter.Server.Extensions;
using Blazouter.Web.Client.Sample.Services;
using Blazouter.Web.Sample.Components;
using Imports = Blazouter.Web.Client.Sample._Imports;

namespace Blazouter.Web.Sample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // Add Blazouter services
            builder.Services.AddBlazouter();

            // Register sample services
            builder.Services.AddSingleton<AuthService>();

            // Register custom error handler for routing errors
            builder.Services.AddBlazouterErrorHandler<CustomRouterErrorHandler>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();

            app.MapRazorComponents<App>()
                .AddBlazouterSupport()  // Required for Web mode
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Imports).Assembly);

            app.Run();
        }
    }
}