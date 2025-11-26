using Blazouter.Interops;

namespace Blazouter.WebAssembly.Sample.Pages
{
    public partial class TypeScript
    {
        // Navigation state
        private bool _canGoBack;
        private int _historyLength;
        private string _currentUrl = "";
        private string _currentPath = "";
        private string _currentHash = "";
        private string _queryParam = "";

        // Document state
        private string _currentTitle = "";
        private string _newTitle = "TypeScript - Blazouter";
        private string _newDescription = "Demonstrating TypeScript integration in Blazouter";
        private int _scrollX;
        private int _scrollY;
        private bool _isDocumentReady;

        // Storage state
        private string _storageKey = "demo-data";
        private string _storageValue = "Hello from LocalStorage!";
        private string _retrievedValue = "";
        private string[] _localStorageKeys = [];

        // Viewport state
        private int _viewportWidth;
        private int _viewportHeight;
        private string _deviceType = "";
        private string _orientation = "";
        private double _pixelRatio;
        private bool _isFullscreen;

        // Clipboard state
        private string _textToCopy = "Hello from Blazouter!";
        private string _clipboardContent = "";
        private bool _isClipboardSupported;

        // General state
        private bool _isInteropAvailable => NavigationInterop != null && DocumentInterop != null &&
                                            StorageInterop != null && ViewportInterop != null && ClipboardInterop != null;

        private string _statusMessage = "";

        protected override async Task OnInitializedAsync()
        {
            if (_isInteropAvailable)
            {
                await RefreshAllState();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && _isInteropAvailable)
            {
                await DocumentInterop!.SetTitleAsync("TypeScript Integration - Blazouter");
                await RefreshAllState();
            }
        }

        private async Task RefreshAllState()
        {
            await RefreshNavigationState();
            await RefreshDocumentState();
            await RefreshStorageState();
            await RefreshViewportState();
            await RefreshClipboardState();
        }

        #region Navigation Methods

        private async Task RefreshNavigationState()
        {
            if (NavigationInterop == null)
            {
                return;
            }

            try
            {
                _canGoBack = await NavigationInterop.CanGoBackAsync();
                _historyLength = await NavigationInterop.GetHistoryLengthAsync();
                _currentUrl = await NavigationInterop.GetCurrentUrlAsync();
                _currentPath = await NavigationInterop.GetPathnameAsync();
                _currentHash = await NavigationInterop.GetHashAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error refreshing navigation state: {ex.Message}", true);
            }
        }

        private async Task HandleGoBack()
        {
            if (NavigationInterop == null)
            {
                return;
            }

            try
            {
                await NavigationInterop.GoBackAsync();
                await Task.Delay(100);
                await RefreshNavigationState();
                SetStatus("Navigated back");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task HandleGoForward()
        {
            if (NavigationInterop == null)
            {
                return;
            }

            try
            {
                await NavigationInterop.GoForwardAsync();
                await Task.Delay(100);
                await RefreshNavigationState();
                SetStatus("Navigated forward");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task GetQueryParam()
        {
            if (NavigationInterop == null)
            {
                return;
            }

            try
            {
                string? param = await NavigationInterop.GetQueryParamAsync("test");
                _queryParam = param ?? "No 'test' parameter found";
                SetStatus("Query parameter retrieved");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        #endregion

        #region Document Methods

        private async Task RefreshDocumentState()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                _currentTitle = await DocumentInterop.GetTitleAsync();
                DocumentInterop.ScrollPosition scrollPos = await DocumentInterop.GetScrollPositionAsync();
                _scrollX = scrollPos.X;
                _scrollY = scrollPos.Y;
                _isDocumentReady = await DocumentInterop.IsDocumentReadyAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error refreshing document state: {ex.Message}", true);
            }
        }

        private async Task UpdateTitle()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                await DocumentInterop.SetTitleAsync(_newTitle);
                await RefreshDocumentState();
                SetStatus("Title updated");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task UpdateMetaDescription()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                await DocumentInterop.SetMetaTagAsync("description", _newDescription);
                SetStatus("Meta description updated");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task ScrollToTop()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                await DocumentInterop.ScrollToTopAsync(smooth: true);
                await Task.Delay(500);
                await RefreshDocumentState();
                SetStatus("Scrolled to top");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task ScrollToBottom()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                await DocumentInterop.ScrollToElementAsync("#page-bottom", smooth: true);
                await Task.Delay(500);
                await RefreshDocumentState();
                SetStatus("Scrolled to bottom");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task FocusTitleInput()
        {
            if (DocumentInterop == null)
            {
                return;
            }

            try
            {
                await DocumentInterop.FocusElementAsync("input[type='text']");
                SetStatus("Focused title input");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        #endregion

        #region Storage Methods

        private async Task RefreshStorageState()
        {
            if (StorageInterop == null)
            {
                return;
            }

            try
            {
                _localStorageKeys = await StorageInterop.GetLocalStorageKeysAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error refreshing storage state: {ex.Message}", true);
            }
        }

        private async Task SaveToLocalStorage()
        {
            if (StorageInterop == null)
            {
                return;
            }

            try
            {
                await StorageInterop.SetLocalStorageAsync(_storageKey, _storageValue);
                await RefreshStorageState();
                SetStatus($"Saved to localStorage: {_storageKey}");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task LoadFromLocalStorage()
        {
            if (StorageInterop == null)
            {
                return;
            }

            try
            {
                _retrievedValue = await StorageInterop.GetLocalStorageAsync<string>(_storageKey) ?? "(not found)";
                SetStatus($"Loaded from localStorage: {_storageKey}");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task RemoveFromLocalStorage()
        {
            if (StorageInterop == null)
            {
                return;
            }

            try
            {
                await StorageInterop.RemoveLocalStorageAsync(_storageKey);
                _retrievedValue = "";
                await RefreshStorageState();
                SetStatus($"Removed from localStorage: {_storageKey}");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        #endregion

        #region Viewport Methods

        private async Task RefreshViewportState()
        {
            if (ViewportInterop == null)
            {
                return;
            }

            try
            {
                _viewportWidth = await ViewportInterop.GetViewportWidthAsync();
                _viewportHeight = await ViewportInterop.GetViewportHeightAsync();
                _deviceType = await ViewportInterop.GetDeviceTypeAsync();
                _orientation = await ViewportInterop.GetOrientationAsync();
                _pixelRatio = await ViewportInterop.GetPixelRatioAsync();
                _isFullscreen = await ViewportInterop.IsFullscreenAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error refreshing viewport state: {ex.Message}", true);
            }
        }

        private async Task ToggleFullscreen()
        {
            if (ViewportInterop == null)
            {
                return;
            }

            try
            {
                if (_isFullscreen)
                {
                    await ViewportInterop.ExitFullscreenAsync();
                    SetStatus("Exited fullscreen");
                }
                else
                {
                    await ViewportInterop.RequestFullscreenAsync();
                    SetStatus("Entered fullscreen");
                }
                await Task.Delay(300);
                await RefreshViewportState();
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        #endregion

        #region Clipboard Methods

        private async Task RefreshClipboardState()
        {
            if (ClipboardInterop == null)
            {
                return;
            }

            try
            {
                _isClipboardSupported = await ClipboardInterop.IsClipboardSupportedAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error refreshing clipboard state: {ex.Message}", true);
            }
        }

        private async Task CopyToClipboard()
        {
            if (ClipboardInterop == null)
            {
                return;
            }

            try
            {
                bool success = await ClipboardInterop.CopyTextAsync(_textToCopy);
                SetStatus(success ? "Copied to clipboard!" : "Failed to copy");
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        private async Task ReadFromClipboard()
        {
            if (ClipboardInterop == null)
            {
                return;
            }

            try
            {
                _clipboardContent = await ClipboardInterop.ReadTextAsync() ?? "(empty or no permission)";
                SetStatus("Read from clipboard");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}", true);
            }
        }

        #endregion

        private void SetStatus(string message, bool isError = false)
        {
            _statusMessage = isError ? $"❌ {message}" : $"✅ {message}";
            StateHasChanged();

            // Clear message after 3 seconds
            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                _statusMessage = "";
                StateHasChanged();
            });
        }

        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}