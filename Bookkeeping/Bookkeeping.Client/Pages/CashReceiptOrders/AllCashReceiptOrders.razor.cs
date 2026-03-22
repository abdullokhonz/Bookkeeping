using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class AllCashReceiptOrders
    {
        private MudTable<CashReceiptOrderGetDto> _table = null!;
        private bool _isLoading = false;

        private Dictionary<Guid, decimal> _vatRates = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var vatRes = await Http.GetFromJsonAsync<ApiResponse<List<VatTaxGetDto>>>("/api/v1/VatTax/GetAll");
                if (vatRes?.Data != null)
                {
                    _vatRates = vatRes.Data.ToDictionary(x => x.Id, x => x.VatRate);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Не удалось загрузить ставки НДС для таблицы", Severity.Warning);
            }
        }

        private async Task<TableData<CashReceiptOrderGetDto>> ServerReload(TableState state, CancellationToken token)
        {
            _isLoading = true;
            try
            {
                var url = $"/api/v1/CashReceiptOrder/GetPaged?page={state.Page + 1}&size={state.PageSize}";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<CashReceiptOrderGetDto>>>(url, token);

                if (response != null && response.IsSuccess)
                {
                    return new TableData<CashReceiptOrderGetDto>()
                    {
                        TotalItems = response.Metadata?.TotalCount ?? response.Count ?? 0,
                        Items = response.Data ?? new List<CashReceiptOrderGetDto>()
                    };
                }
            }
            catch (Exception ex) when (ex is not TaskCanceledException)
            {
                Snackbar.Add("Не удалось загрузить список ПКО", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }

            return new TableData<CashReceiptOrderGetDto>() { TotalItems = 0, Items = new List<CashReceiptOrderGetDto>() };
        }
    }
}
