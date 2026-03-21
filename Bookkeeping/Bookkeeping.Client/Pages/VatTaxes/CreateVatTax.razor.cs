using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class CreateVatTax
    {
        private VatTaxCreateDto _model = new();
        private bool _isProcessing = false;

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/VatTax/Create", _model);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Ставка НДС успешно создана", Severity.Success);
                    Nav.NavigateTo("/vat-taxes");
                }
                else
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                    Snackbar.Add(errorResult?.Message ?? "Ошибка при создании записи", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка сети: " + ex.Message, Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/vat-taxes");
    }
}
