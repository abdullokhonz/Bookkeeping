using Bookkeeping.Contracts.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Auth
{
    public partial class ConfirmCode
    {
        [Parameter][SupplyParameterFromQuery] public string? Email { get; set; }
        [Parameter][SupplyParameterFromQuery] public string? Phone { get; set; }

        private VerifyCodeDto _model = new();
        private MudForm _form = null!;
        private bool _isLoading;

        private string Identifier => Email ?? Phone ?? "указанный адрес/номер";

        protected override void OnInitialized()
        {
            _model.Identifier = Email ?? Phone ?? string.Empty;

            if (string.IsNullOrEmpty(_model.Identifier))
            {
                Snackbar.Add("Данные для подтверждения не найдены", Severity.Warning);
            }
        }

        private async Task VerifyCode()
        {
            if (string.IsNullOrWhiteSpace(_model.Code)) return;

            _isLoading = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/Auth/verify-code", _model);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Аккаунт успешно подтвержден!", Severity.Success);
                    NavManager.NavigateTo("/login");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Snackbar.Add($"Ошибка: {error}", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка соединения с сервером", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task ResendCode()
        {
            Snackbar.Add("Запрос на повторную отправку отправлен", Severity.Info);
            // Здесь можно вызвать эндпоинт для переотправки кода
            // await Http.PostAsJsonAsync("api/v1/Auth/resend-code", new { Identifier = _model.Identifier });
        }
    }
}
