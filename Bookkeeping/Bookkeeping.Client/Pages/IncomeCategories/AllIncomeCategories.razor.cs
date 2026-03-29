using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class AllIncomeCategories
    {
        private bool _isLoading = false;
        private List<IncomeCategoryGetDto> _allCategories = new();
        private Dictionary<Guid, string> _accountCache = new();

        private string _searchString = "";
        private string _searchField = "All";

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _isLoading = true;
            try
            {
                var url = "/api/v1/IncomeCategory/GetPaged?page=1&size=10000";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<IncomeCategoryGetDto>>>(url);

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _allCategories = response.Data;
                    await FetchMissingAccountNames(_allCategories);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка при загрузке данных", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
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
                        StateHasChanged();
                    }
                }
                catch { }
            }
        }

        private void ResetSearch()
        {
            _searchString = string.Empty;
            _searchField = "All";
        }

        private bool FilterFunc(IncomeCategoryGetDto element)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            var search = _searchString.Trim().ToLower();

            _accountCache.TryGetValue(element.IfrsAccountId, out var accountNumber);
            accountNumber ??= "";

            return _searchField switch
            {
                "Name" => element.Name.ToLower().Contains(search),
                "Description" => (element.Description ?? "").ToLower().Contains(search),
                "Account" => accountNumber.ToLower().Contains(search),
                "All" or _ => element.Name.ToLower().Contains(search) ||
                              (element.Description ?? "").ToLower().Contains(search) ||
                              accountNumber.ToLower().Contains(search)
            };
        }

        private double ParseAccountNumberForSorting(Guid accountId)
        {
            if (!_accountCache.TryGetValue(accountId, out var numberStr) || string.IsNullOrEmpty(numberStr))
                return 0;

            if (double.TryParse(numberStr.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
                return result;

            return 0;
        }
    }
}
