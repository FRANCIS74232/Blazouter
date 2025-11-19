using Blazouter.Attributes;
using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Web.Client.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates dynamic route parameters with attribute-based routing.
    /// </summary>
    [RouteTitle("Dynamic Params")]
    [RouteTransition(RouteTransition.Fade)]
    [Route("/attribute-examples/user/:userId/post/:postId")]
    public partial class DynamicParamsExample : ComponentBase, IDisposable
    {
        [Inject] private RouterStateService RouterState { get; set; } = default!;
        [Inject] private RouterNavigationService NavigationService { get; set; } = default!;

        private string? UserId { get; set; }
        private string? PostId { get; set; }

        protected override void OnInitialized()
        {
            UpdateParameters();
            RouterState.OnRouteChanged += HandleRouteChanged;
        }

        private void HandleRouteChanged(RouteMatch? match)
        {
            UpdateParameters();
            StateHasChanged();
        }

        private void UpdateParameters()
        {
            UserId = RouterState.GetParam("userId") ?? "not set";
            PostId = RouterState.GetParam("postId") ?? "not set";
        }

        private void NavigateTo(string userId, string postId)
        {
            NavigationService.NavigateTo($"/attribute-examples/user/{userId}/post/{postId}");
        }

        public void Dispose()
        {
            RouterState.OnRouteChanged -= HandleRouteChanged;
        }
    }
}