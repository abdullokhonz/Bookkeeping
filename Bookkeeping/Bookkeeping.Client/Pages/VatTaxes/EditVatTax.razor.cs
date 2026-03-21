using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class EditVatTax
    {
        [Parameter] public Guid Id { get; set; }
        private VatTaxUpdateDto? _model;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<VatTaxUpdateDto>>($"/api/v1/VatTax/GetById/{Id}");
            if (result?.IsSuccess == true) _model = result.Data;
        }

        private async Task HandleSubmit()
        {
            _isProcessing = true;
            var response = await Http.PutAsJsonAsync($"/api/v1/VatTax/Update/{Id}", _model);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Обновлено", Severity.Success);
                Nav.NavigateTo("/vat-taxes");
            }
            _isProcessing = false;
        }

        private void Cancel() => Nav.NavigateTo("/vat-taxes");
    }
}
