using Blazouter.Extensions;
using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;
using AuthGuard = Blazouter.Web.Client.Sample.Components.Guards.AuthGuard;
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
                Component = typeof(ServerPages.Home),
                Title = "Home",
                Transition = RouteTransition.Blur
            },
            new() {
                Path = "/about",
                Component = typeof(ClientPages.About),
                Title = "About",
                Transition = RouteTransition.Fade
            },
            new() {
                Path = "/navigation",
                Component = typeof(ClientPages.Navigation),
                Title = "Navigation Demo",
                Transition = RouteTransition.Flip
            },
            new() {
                Path = "/transitions",
                Component = typeof(ClientPages.Transitions),
                Title = "Transitions Demo",
                Transition = RouteTransition.Lift
            },
            new() {
                Path = "/users",
                Component = typeof(ClientPages.Users.UserLayout),
                Title = "Users",
                Transition = RouteTransition.Swipe,
                Children =
                [
                    new RouteConfig
                    {
                        Path = "",
                        Component = typeof(ClientPages.Users.UserList),
                        Title = "User List",
                        Exact = true
                    },
                    new RouteConfig
                    {
                        Path = ":id",
                        Component = typeof(ClientPages.Users.UserDetail),
                        Title = "User Details"
                    }
                ]
            },
            new() {
                Path = "/protected",
                Component = typeof(ClientPages.Protected),
                Title = "Protected Page",
                Guards = [typeof(AuthGuard)],
                Transition = RouteTransition.Rotate
            },
            new() {
                Path = "/lazy",
                ComponentLoader = async () =>
                {
                    await Task.Delay(1000); // Simulate loading
                    return typeof(ClientPages.LazyPage);
                },
                Title = "Lazy Loaded Page",
                Transition = RouteTransition.Curtain
            },
            new() {
                Path = "/error-example",
                Component = typeof(ClientPages.ErrorExample),
                Title = "Error Example",
                Transition = RouteTransition.Spotlight
            },
            new() {
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
                        return typeof(ClientPages.ErrorExample);
                    }
                },
                Title = "Test Error"
            }
        }.AddAttributeRoutes(typeof(ServerProgram).Assembly, typeof(ClientProgram).Assembly);
    }
}