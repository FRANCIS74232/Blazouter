using Blazouter.Extensions;
using Blazouter.Hybrid.Extensions;
using Blazouter.Hybrid.Sample.Services;
using Microsoft.Extensions.Logging;

namespace Blazouter.Hybrid.Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .AddBlazouterSupport() // Adds Blazouter support to the MAUI app
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Logging.AddDebug();

            builder.Services.AddMauiBlazorWebView();

            // Register sample services
            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddBlazorWebViewDeveloperTools();

            // Register custom error handler for routing errors
            builder.Services.AddBlazouterErrorHandler<CustomRouterErrorHandler>();

            return builder.Build();
        }
    }
}