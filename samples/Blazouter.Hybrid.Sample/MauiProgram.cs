using Blazouter.Hybrid.Extensions;
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

            builder.Services.AddBlazorWebViewDeveloperTools();

            return builder.Build();
        }
    }
}