using Blazouter.Attributes;
using Blazouter.Enums;
using Blazouter.WebAssembly.Sample.Guards;
using Blazouter.WebAssembly.Sample.Services;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.WebAssembly.Sample.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates route guards with attribute-based routing.
    /// </summary>
    [RouteTitle("Guards Example")]
    [Route("/attribute-examples/guards")]
    [RouteGuard(typeof(AuthenticationGuard))]
    [RouteTransition(RouteTransition.Rotate)]
    public partial class GuardsExample : ComponentBase
    {
        [Inject] private AuthenticationService AuthenticationService { get; set; } = default!;
    }
}