using Microsoft.JSInterop;

namespace Bookkeeping.Components.Layout
{
    public partial class MainLayout
    {
        private bool _isDarkMode;
        private bool _open = true;
        private bool _isInitialized;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    var savedDark = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "darkMode");
                    if (bool.TryParse(savedDark, out var isDark))
                        _isDarkMode = isDark;
                    else
                        _isDarkMode = false;

                    var savedDrawer = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "drawerOpen");
                    if (bool.TryParse(savedDrawer, out var isOpen))
                        _open = isOpen;
                    else
                        _open = true;
                }
                catch
                {
                    _isDarkMode = false;
                    _open = true;
                }
                _isInitialized = true;
                StateHasChanged();
            }
        }

        private async Task ToggleDarkMode()
        {
            _isDarkMode = !_isDarkMode;
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "darkMode", _isDarkMode.ToString());
        }

        private async Task ToggleDrawer()
        {
            _open = !_open;
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "drawerOpen", _open.ToString());
        }
    }
}
