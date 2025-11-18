using Microsoft.AspNetCore.Components;

namespace Blazouter.Web.Client.Sample.Components.Pages
{
    public partial class Navigation : ComponentBase
    {
        private string _selectedRoute = "/";

        private void NavigateToSelected()
        {
            if (!string.IsNullOrEmpty(_selectedRoute))
            {
                NavService.NavigateTo(_selectedRoute);
            }
        }
    }
}