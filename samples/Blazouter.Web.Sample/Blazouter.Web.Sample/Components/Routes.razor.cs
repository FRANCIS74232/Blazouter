using Blazouter.Extensions;
using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;
using ClientPages = Blazouter.Web.Client.Sample.Components.Pages;
using ClientProgram = Blazouter.Web.Client.Sample.Program;
using ServerPages = Blazouter.Web.Sample.Components.Pages;
using ServerProgram = Blazouter.Web.Sample.Program;

namespace Blazouter.Web.Sample.Components
{
    public partial class Routes
    {
        [Inject] private RouterNavigationService _navService { get; set; } = default!;

        private readonly List<RouteConfig> _routes = new List<RouteConfig>
        {
            new() {
                Path = "/",
                RedirectTo = "/server"
            },
            new() {
                Path = "/server",
                Component = typeof(ServerPages.Server),
                Title = "Server",
                Transition = RouteTransition.Blur
            },
            new() {
                Path = "/client",
                Component = typeof(ClientPages.Client),
                Title = "Client",
                Transition = RouteTransition.Lift
            }
        }.AddAttributeRoutes(typeof(ServerProgram).Assembly, typeof(ClientProgram).Assembly);
    }
}