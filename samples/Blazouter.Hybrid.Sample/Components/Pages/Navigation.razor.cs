using Blazouter.Extensions;

namespace Blazouter.Hybrid.Sample.Components.Pages
{
    public partial class Navigation
    {
        private string _selectedRoute = "/";

        private void NavigateToSelected()
        {
            if (!string.IsNullOrEmpty(_selectedRoute))
            {
                NavService.NavigateTo(_selectedRoute);
            }
        }

        // NEW: Demonstrate QueryStringBuilder fluent API
        private void NavigateWithBuilder()
        {
            NavService.NavigateToWithQuery("/users", q => q
                .Add("sort", "name")
                .Add("order", "asc")
                .Add("filter", "active")
                .Add("page", 2));
        }

        // NEW: Demonstrate typed parameters
        private void NavigateWithTypedParams()
        {
            NavService.NavigateToWithQuery("/users", q => q
                .Add("page", 3)
                .Add("pageSize", 25)
                .Add("active", true)
                .Add("minScore", 85.5m)
                .Add("date", DateTime.Now));
        }

        // NEW: Single parameter shortcut
        private void NavigateWithSingleParam()
        {
            NavService.NavigateToWithSingleQuery("/users", "sort", "date");
        }

        // NEW: Update current query parameters
        private void UpdateCurrentQuery()
        {
            NavService.NavigateToWithUpdatedQuery(RouterState, null, q => q
                .Set("updated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Set("filter", "deactive")
                .Set("version", 2));
        }

        // NEW: Remove specific query parameter
        private void RemoveQueryParam()
        {
            NavService.NavigateToWithRemovedQuery(RouterState, "filter", "sort");
        }

        // NEW: Clear all query parameters
        private void ClearAllQuery()
        {
            NavService.NavigateToWithClearedQuery(RouterState);
        }
    }
}