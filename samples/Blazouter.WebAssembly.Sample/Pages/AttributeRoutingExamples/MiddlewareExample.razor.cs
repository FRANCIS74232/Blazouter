using Blazouter.Attributes;
using Blazouter.Enums;
using Blazouter.Services;
using Blazouter.WebAssembly.Sample.Middlewares;
using Microsoft.AspNetCore.Components;
using RouteAttribute = Blazouter.Attributes.RouteAttribute;

namespace Blazouter.WebAssembly.Sample.Pages.AttributeRoutingExamples
{
    /// <summary>
    /// Demonstrates route middleware with attribute-based routing.
    /// </summary>
    [RouteTitle("Middleware Example")]
    [RouteTransition(RouteTransition.Pop)]
    [Route("/attribute-examples/middleware")]
    [RouteMiddleware(typeof(TimingMiddleware))]
    [RouteMiddleware(typeof(LoggingMiddleware))]
    [RouteMiddleware(typeof(AnalyticsMiddleware))]
    public partial class MiddlewareExample : ComponentBase
    {
        [Inject]
        private RouterNavigationService NavigationService { get; set; } = default!;

        [Parameter]
        public Guid? PageViewId { get; set; }

        [Parameter]
        public TimeSpan? NavigationDuration { get; set; }
    }
}