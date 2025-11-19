using Blazouter.Attributes;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Server.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates basic attribute-based routing with minimal configuration.
    /// </summary>
    [RouteTitle("Basic Example")]
    [Route("/attribute-examples/basic")]
    public partial class BasicExample : ComponentBase { }
}