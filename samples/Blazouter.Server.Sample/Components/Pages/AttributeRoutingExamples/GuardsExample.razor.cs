using Blazouter.Attributes;
using Blazouter.Models;
using Blazouter.Server.Sample.Services;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Server.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates route guards with attribute-based routing.
    /// </summary>
    [RouteTitle("Guards Example")]
    [Route("/attribute-examples/guards")]
    [RouteGuard(typeof(Guards.AuthGuard))]
    [RouteTransition(RouteTransition.Rotate)]
    public partial class GuardsExample : ComponentBase
    {
        [Inject] private AuthService AuthService { get; set; } = default!;
    }
}