using Microsoft.JSInterop;

namespace Blazouter.Hybrid.Sample.Components.Layout
{
    public partial class ThemeSwitcher
    {
        private bool _isDark = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isDark = await JS.InvokeAsync<bool>("eval", "document.documentElement.classList.contains('dark')");
                StateHasChanged();
            }
        }

        private async Task ToggleTheme()
        {
            _isDark = !_isDark;
            await JS.InvokeVoidAsync("eval",
                _isDark
                    ? "document.documentElement.classList.add('dark'); localStorage.setItem('theme', 'dark');"
                    : "document.documentElement.classList.remove('dark'); localStorage.setItem('theme', 'light');");
        }
    }
}