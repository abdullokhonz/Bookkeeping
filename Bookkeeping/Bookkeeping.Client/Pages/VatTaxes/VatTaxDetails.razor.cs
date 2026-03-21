using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class VatTaxDetails
    {
        [Parameter] public Guid Id { get; set; }
        private VatTaxGetDto? _model;

        protected override async Task OnInitializedAsync()
        {
            var response = await Http.GetFromJsonAsync<ApiResponse<VatTaxGetDto>>($"/api/v1/VatTax/GetById/{Id}");
            if (response?.IsSuccess == true) _model = response.Data;
        }

        private void GoBack() => Nav.NavigateTo("/vat-taxes");
        private void GoToEdit() => Nav.NavigateTo($"/vat-taxes/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/vat-taxes/delete/{Id}");
    }
}
