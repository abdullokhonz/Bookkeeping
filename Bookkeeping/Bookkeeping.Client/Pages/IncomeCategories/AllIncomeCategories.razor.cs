using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class AllIncomeCategories
    {
        private MudTable<IncomeCategoryGetDto> _table = null!;
        private bool _isLoading = false;
        private Dictionary<Guid, string> _accountCache = new();

        private async Task<TableData<IncomeCategoryGetDto>> ServerReload(TableState state, CancellationToken token)
        {
            _isLoading = true;
            try
            {
                var url = $"/api/v1/IncomeCategory/GetPaged?page={state.Page + 1}&size={state.PageSize}";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<IncomeCategoryGetDto>>>(url, token);

                if (response != null && response.IsSuccess)
                {
                    var items = response.Data ?? new List<IncomeCategoryGetDto>();
                    _ = FetchMissingAccountNames(items);

                    return new TableData<IncomeCategoryGetDto>()
                    {
                        TotalItems = response.Metadata?.TotalCount ?? response.Count ?? 0,
                        Items = items
                    };
                }
            }
            catch (Exception ex) when (ex is not TaskCanceledException)
            {
                Snackbar.Add("Ошибка загрузки данных", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }

            return new TableData<IncomeCategoryGetDto>() { TotalItems = 0, Items = new List<IncomeCategoryGetDto>() };
        }

        private async Task FetchMissingAccountNames(List<IncomeCategoryGetDto> items)
        {
            var uniqueIds = items.Select(x => x.IfrsAccountId).Distinct().Where(id => !_accountCache.ContainsKey(id));

            foreach (var id in uniqueIds)
            {
                try
                {
                    var res = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{id}");
                    if (res != null && res.IsSuccess && res.Data != null)
                    {
                        _accountCache[id] = res.Data.AccountNumber;
                        await InvokeAsync(StateHasChanged);
                    }
                }
                catch { }
            }
        }
    }
}
