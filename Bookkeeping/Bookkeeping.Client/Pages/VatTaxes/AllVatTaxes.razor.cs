using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class AllVatTaxes
    {
        private MudTable<VatTaxGetDto> _table = null!;
        private bool _isLoading = false;

        private async Task<TableData<VatTaxGetDto>> ServerReload(TableState state, CancellationToken token)
        {
            _isLoading = true;
            try
            {
                var url = $"/api/v1/VatTax/GetPaged?page={state.Page + 1}&size={state.PageSize}";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<VatTaxGetDto>>>(url, token);

                if (response != null && response.IsSuccess)
                {
                    return new TableData<VatTaxGetDto>()
                    {
                        TotalItems = response.Metadata?.TotalCount ?? response.Count ?? 0,
                        Items = response.Data ?? new List<VatTaxGetDto>()
                    };
                }
            }
            catch (Exception ex) when (ex is not TaskCanceledException)
            {
                Snackbar.Add("Не удалось загрузить список налогов", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }

            return new TableData<VatTaxGetDto>() { TotalItems = 0, Items = new List<VatTaxGetDto>() };
        }
    }
}
