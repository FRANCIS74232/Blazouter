using Blazouter.Enums;
using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;

namespace Blazouter.Hybrid.Sample.Components
{
    public partial class Routes
    {
        [Inject] private RouterNavigationService _navService { get; set; } = default!;

        private readonly List<RouteConfig> _routes =
        [
            new() {
                Path = "/",
                Component = typeof(Pages.Home),
                Title = "Home",
                Transition = RouteTransition.Blur
            }
        ];
    }
}