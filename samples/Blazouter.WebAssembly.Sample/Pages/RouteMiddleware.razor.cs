using Microsoft.AspNetCore.Components;

namespace Blazouter.WebAssembly.Sample.Pages
{
    public partial class RouteMiddleware
    {
        // Middleware can pass data to components via parameters
        [Parameter]
        public Guid? PageViewId { get; set; }

        [Parameter]
        public TimeSpan? NavigationDuration { get; set; }
    }
}