using Blazouter.Attributes;
using Blazouter.Models;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Web.Client.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates route transition configuration with attributes.
    /// </summary>
    [RouteTitle("Transitions Example")]
    [RouteTransition(RouteTransition.Slide)]
    [Route("/attribute-examples/transitions")]
    public partial class TransitionsExample : ComponentBase { }
}