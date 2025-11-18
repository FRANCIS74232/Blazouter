namespace Blazouter.WebAssembly.Sample.Pages
{
    public partial class ErrorExample
    {
        private bool _showErrorMessage = false;

        private void TriggerComponentLoadError()
        {
            _showErrorMessage = true;
            // This would normally navigate to a route with a failing ComponentLoader
            // For demo purposes, we just show a message
            StateHasChanged();
        }

        private void TriggerGuardError()
        {
            _showErrorMessage = true;
            // This would normally navigate to a route with a failing guard
            // For demo purposes, we just show a message
            StateHasChanged();
        }
    }
}