using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;

namespace Blazouter.WebAssembly.Sample
{
    public partial class App
    {
        [Inject] private RouterNavigationService _navService { get; set; } = default!;

        private List<RouteConfig> _routes =
        [
            new RouteConfig
            {
                Path = "/",
                Component = typeof(Pages.Home),
                Title = "Home",
                Transition = RouteTransition.Blur
            },
            new RouteConfig
            {
                Path = "/about",
                Component = typeof(Pages.About),
                Title = "About",
                Transition = RouteTransition.Fade
            },
            new RouteConfig
            {
                Path = "/navigation",
                Component = typeof(Pages.Navigation),
                Title = "Navigation Demo",
                Transition = RouteTransition.Flip
            },
            new RouteConfig
            {
                Path = "/transitions",
                Component = typeof(Pages.Transitions),
                Title = "Transitions Demo",
                Transition = RouteTransition.Lift
            },
            new RouteConfig
            {
                Path = "/users",
                Component = typeof(Pages.Users.UserLayout),
                Title = "Users",
                Transition = RouteTransition.Swipe,
                Children =
                [
                    new RouteConfig
                    {
                        Path = "",
                        Component = typeof(Pages.Users.UserList),
                        Title = "User List",
                        Exact = true
                    },
                    new RouteConfig
                    {
                        Path = ":id",
                        Component = typeof(Pages.Users.UserDetail),
                        Title = "User Details"
                    }
                ]
            },
            new RouteConfig
            {
                Path = "/protected",
                Component = typeof(Pages.Protected),
                Title = "Protected Page",
                Guards = [typeof(Guards.AuthGuard)],
                Transition = RouteTransition.Rotate
            },
            new RouteConfig
            {
                Path = "/lazy",
                ComponentLoader = async () =>
                {
                    await Task.Delay(1000); // Simulate loading
                    return typeof(Pages.LazyPage);
                },
                Title = "Lazy Loaded Page",
                Transition = RouteTransition.Curtain
            },
            new RouteConfig
            {
                Path = "/error-example",
                Component = typeof(Pages.ErrorExample),
                Title = "Error Example",
                Transition = RouteTransition.Spotlight
            },
            new RouteConfig
            {
                Path = "/test-error",
                ComponentLoader = async () =>
                {
                    if (new Random().Next(2) == 1)
                    {
                        try
                        {
                            // Simulated failure
                            throw new InvalidOperationException("Component failed to load due to an unexpected condition.");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(
                                message: $"Blazouter failed to load component for route '{"/test-error"}'. " +
                                         $"Reason: {ex.Message}",
                                innerException: ex
                            );
                        }
                    }
                    else{
                        return typeof(Pages.ErrorExample);
                    }
                },
                Title = "Test Error"
            }
        ];
    }
}