using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.VatTaxes
{
    public partial class AllVatTaxes
    {
        private bool _isLoading = false;
        private List<VatTaxGetDto> _allTaxes = new();

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
                var url = "/api/v1/VatTax/GetPaged?page=1&size=10000";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<VatTaxGetDto>>>(url);

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _allTaxes = response.Data;
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Не удалось загрузить список налогов", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void ResetSearch()
        {
            _searchString = string.Empty;
            _searchField = "All";
        }

        private bool FilterFunc(VatTaxGetDto element)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            var search = _searchString.Trim().ToLower();
            var rateStr = element.VatRate.ToString("N2").ToLower();

            return _searchField switch
            {
                "Rate" => rateStr.Contains(search),
                "Description" => (element.Description ?? "").ToLower().Contains(search),
                "All" or _ => rateStr.Contains(search) ||
                              (element.Description ?? "").ToLower().Contains(search)
            };
        }
    }
}
