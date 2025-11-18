namespace Blazouter.Server.Sample.Components.Pages
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
    }
}