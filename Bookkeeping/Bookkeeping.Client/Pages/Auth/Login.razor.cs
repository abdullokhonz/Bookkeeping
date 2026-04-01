using Bookkeeping.Client.Providers;
using Bookkeeping.Contracts.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Auth
{
    public partial class Login
    {
        [SupplyParameterFromQuery]
        public string? ReturnUrl { get; set; }

        private LoginRequestDto _model = new();
        private bool _isValid;
        private bool _isLoading;
        private MudForm _form = null!;
        private bool _showPassword;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated == true)
            {
                NavManager.NavigateTo(ReturnUrl ?? "/");
            }
        }

        void TogglePassword()
        {
            _showPassword = !_showPassword;
            _passwordInput = _showPassword ? InputType.Text : InputType.Password;
            _passwordInputIcon = _showPassword ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
        }

        private async Task Submit()
        {
            _isLoading = true;
            try
            {
                var response = await Http.PostAsJsonAsync("api/v1/Auth/login", _model);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.AccessToken);

                        if (!string.IsNullOrEmpty(result.RefreshToken))
                        {
                            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);
                        }

                        Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
                        ((JwtAuthStateProvider)AuthStateProvider).NotifyUserAuthentication(result.AccessToken);

                        NavManager.NavigateTo(ReturnUrl ?? "/");
                    }
                }
                else { Snackbar.Add("Ошибка входа", Severity.Error); }
            }
            catch { Snackbar.Add("Ошибка сети", Severity.Error); }
            finally { _isLoading = false; }
        }
    }
}
