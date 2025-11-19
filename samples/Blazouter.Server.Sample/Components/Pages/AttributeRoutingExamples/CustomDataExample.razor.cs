using Blazouter.Attributes;
using Blazouter.Models;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.Server.Sample.Components.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates custom route data with attributes.
    /// </summary>
    [RouteData("Priority", 3)]
    [RouteTitle("Custom Data")]
    [RouteData("Section", "Examples")]
    [RouteData("Category", "Advanced")]
    [RouteTransition(RouteTransition.Blur)]
    [Route("/attribute-examples/custom-data")]
    public partial class CustomDataExample : ComponentBase
    {
        [Parameter]
        public int Priority { get; set; } = 0;

        [Parameter]
        public string Section { get; set; } = "N/A";

        [Parameter]
        public string Category { get; set; } = "N/A";

        private Dictionary<string, object>? RouteData { get; set; }

        protected override void OnInitialized()
        {
            RouteData ??= [];

            RouteData.Add("Section", Section);
            RouteData.Add("Category", Category);
            RouteData.Add("Priority", Priority);
        }
    }
}