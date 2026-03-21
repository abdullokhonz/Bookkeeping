using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class DeleteVatTax
    {
        [Parameter] public Guid Id { get; set; }
        private VatTaxGetDto? _model;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<VatTaxGetDto>>($"/api/v1/VatTax/GetById/{Id}");
            if (result?.IsSuccess == true) _model = result.Data;
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            var response = await Http.DeleteAsync($"/api/v1/VatTax/HardDelete/{Id}/permanent");
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Ставка удалена", Severity.Success);
                Nav.NavigateTo("/vat-taxes");
            }
            _isProcessing = false;
        }

        private void Cancel() => Nav.NavigateTo("/vat-taxes");
    }
}
