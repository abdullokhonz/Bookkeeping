using Bookkeeping.Contracts.DTOs.Users;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace Bookkeeping.Client.Pages.Auth
{
    public partial class Register
    {
        private MudForm _form = null!;
        private bool _isValid;
        private bool _isLoading;

        private RegisterUserDto _model = new();
        private string _contactInput = "";

        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        void TogglePasswordVisibility()
        {
            _passwordVisibility = !_passwordVisibility;
            _passwordInputIcon = _passwordVisibility ? Icons.Material.Filled.Visibility : Icons.Material.Filled.VisibilityOff;
            _passwordInput = _passwordVisibility ? InputType.Text : InputType.Password;
        }

        private async Task SubmitRegister()
        {
            await _form.ValidateAsync();
            if (!_isValid) return;

            if (string.IsNullOrWhiteSpace(_contactInput))
            {
                Snackbar.Add("Укажите контактные данные", Severity.Warning);
                return;
            }

            _model.Email = null;
            _model.PhoneNumber = null;

            if (_contactInput.Contains("@"))
            {
                _model.Email = _contactInput.Trim();
            }
            else
            {
                _model.PhoneNumber = Regex.Replace(_contactInput, @"[^\d+]", "");
            }

            _isLoading = true;
            try
            {
                var response = await Http.PostAsJsonAsync("api/v1/Auth/register", _model);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Регистрация успешна!", Severity.Success);

                    if (!string.IsNullOrEmpty(_model.Email))
                    {
                        NavManager.NavigateTo($"/confirm-code?email={_model.Email}");
                    }
                    else
                    {
                        NavManager.NavigateTo($"/confirm-code?phone={_model.PhoneNumber}");
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Snackbar.Add($"Ошибка: {error}", Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("Ошибка соединения с сервером", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
