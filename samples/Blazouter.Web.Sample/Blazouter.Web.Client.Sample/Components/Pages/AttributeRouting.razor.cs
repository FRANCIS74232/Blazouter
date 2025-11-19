using Blazouter.Attributes;
using Blazouter.Models;
using Blazouter.Services;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Web.Client.Sample.Components.Pages
{
    /// <summary>
    /// Main attribute-based routing example page with links to detailed examples.
    /// </summary>
    [RouteExact(true)]
    [Route("/attribute-examples")]
    [RouteData("Section", "Admin")]
    [RouteTitle("Attribute Example")]
    [RouteGuard(typeof(Guards.AuthGuard))]
    [RouteTransition(RouteTransition.Fade)]
    public partial class AttributeRouting
    {
        [Inject] private RouterNavigationService NavigationService { get; set; } = default!;

        /// <summary>
        /// Route data parameter - automatically injected from RouteData attribute
        /// </summary>
        [Parameter]
        public string Section { get; set; } = "Admin";

        /// <summary>
        /// The route title is available from route data
        /// </summary>
        protected string RouteTitle { get; set; } = "Admin Example";
    }
}