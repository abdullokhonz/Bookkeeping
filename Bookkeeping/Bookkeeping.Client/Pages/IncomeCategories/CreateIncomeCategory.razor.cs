using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class CreateIncomeCategory
    {
        private IncomeCategoryCreateDto _model = new();
        private List<IfrsAccountTreeDto>? _accounts;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
        }

        private async Task LoadAccounts()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<IfrsAccountTreeDto>>>("/api/v1/IfrsAccount/tree");
                if (response != null && response.IsSuccess)
                {
                    _accounts = response.Data;
                }
                else
                {
                    Snackbar.Add("Не удалось загрузить список счетов", Severity.Warning);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка сети: {ex.Message}", Severity.Error);
            }
        }

        private async Task HandleValidSubmit()
        {
            if (_model.IfrsAccountId == null)
            {
                Snackbar.Add("Пожалуйста, выберите счет из плана счетов", Severity.Warning);
                return;
            }

            _isProcessing = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/IncomeCategory/Create", _model);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Статья доходов успешно создана", Severity.Success);
                    Nav.NavigateTo("/income-categories");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Snackbar.Add($"Ошибка сервера: {response.StatusCode}", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Критическая ошибка: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/income-categories");
    }
}
