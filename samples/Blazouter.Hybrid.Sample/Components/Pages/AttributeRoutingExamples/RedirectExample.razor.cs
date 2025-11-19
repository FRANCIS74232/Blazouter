using Blazouter.Attributes;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Hybrid.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates route redirect with attributes. This component should redirect to /attribute-examples/basic.
    /// </summary>
    [RouteTitle("Redirect Example")]
    [Route("/attribute-examples/old-path")]
    [RouteRedirect("/attribute-examples/basic")]
    public partial class RedirectExample : ComponentBase
    {
        // This component will not be rendered - navigation will redirect to /attribute-examples/basic
    }
}