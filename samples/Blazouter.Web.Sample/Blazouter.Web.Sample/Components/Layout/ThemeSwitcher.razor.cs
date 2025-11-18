using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazouter.Web.Sample.Components.Layout
{
    public partial class ThemeSwitcher : ComponentBase, IAsyncDisposable
    {
        private bool _isDark = false;
        private IJSObjectReference? _module;
        private bool _isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            // Load the JS module early
            try
            {
                _module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
                // Initialize theme protection on first load
                await _module.InvokeVoidAsync("initializeTheme");
            }
            catch
            {
                // Module loading will be retried in OnAfterRenderAsync
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                // Load module if not already loaded
                if (_module == null)
                {
                    try
                    {
                        _module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/theme.js");
                    }
                    catch
                    {
                        // Continue with fallback
                    }
                }

                // Always sync theme state with DOM - crucial for navigation scenarios
                bool currentDarkMode = _module != null
                    ? await _module.InvokeAsync<bool>("isDarkMode")
                    : await JS.InvokeAsync<bool>("eval", "document.documentElement.classList.contains('dark')");

                if (_isDark != currentDarkMode)
                {
                    _isDark = currentDarkMode;
                    _isInitialized = true;
                    await InvokeAsync(StateHasChanged);
                }
                else if (!_isInitialized)
                {
                    _isInitialized = true;
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch
            {
                // Fallback to eval
                try
                {
                    bool currentDarkMode = await JS.InvokeAsync<bool>("eval", "document.documentElement.classList.contains('dark')");
                    if (_isDark != currentDarkMode)
                    {
                        _isDark = currentDarkMode;
                        await InvokeAsync(StateHasChanged);
                    }
                }
                catch
                {
                    // Silently ignore errors
                }
            }
        }

        private async Task ToggleTheme()
        {
            _isDark = !_isDark;

            if (_module != null)
            {
                await _module.InvokeVoidAsync("setDarkMode", _isDark);
            }
            else
            {
                // Fallback
                await JS.InvokeVoidAsync("eval",
                    _isDark
                        ? "document.documentElement.classList.add('dark'); localStorage.setItem('theme', 'dark');"
                        : "document.documentElement.classList.remove('dark'); localStorage.setItem('theme', 'light');");
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_module != null)
            {
                try
                {
                    await _module.DisposeAsync();
                }
                catch { }
            }
        }
    }
}