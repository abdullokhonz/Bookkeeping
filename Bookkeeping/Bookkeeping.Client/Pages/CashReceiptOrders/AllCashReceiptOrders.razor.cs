using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Contracts.Enums;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class AllCashReceiptOrders
    {
        private bool _isLoading = false;
        private Dictionary<Guid, decimal> _vatRates = new();

        private List<CashReceiptOrderGetDto> _allOrders = new();
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
                var vatRes = await Http.GetFromJsonAsync<ApiResponse<List<VatTaxGetDto>>>("/api/v1/VatTax/GetAll");
                if (vatRes?.Data != null)
                {
                    _vatRates = vatRes.Data.ToDictionary(x => x.Id, x => x.VatRate);
                }

                var url = "/api/v1/CashReceiptOrder/GetPaged?page=1&size=10000";
                var response = await Http.GetFromJsonAsync<ApiResponse<List<CashReceiptOrderGetDto>>>(url);

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _allOrders = response.Data;
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Не удалось загрузить данные", Severity.Error);
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

        private bool FilterFunc(CashReceiptOrderGetDto element)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            var search = _searchString.Trim().ToLower();

            var vatAmountStr = "";
            if (element.VatTaxId.HasValue && _vatRates.TryGetValue(element.VatTaxId.Value, out var rate))
            {
                var vatAmount = element.Amount * (rate / 100m);
                vatAmountStr = (vatAmount % 1 == 0 ? vatAmount.ToString("N0") : vatAmount.ToString("N2")).ToLower();
            }

            var dateStr = element.OperationDate.ToString("dd.MM.yyyy").ToLower();

            return _searchField switch
            {
                "DocumentNumber" => element.DocumentNumber.ToLower().Contains(search),
                "OperationDate" => dateStr.Contains(search),
                "Amount" => element.Amount.ToString().Contains(search),
                "Status" => GetStatusText(element.Status).ToLower().Contains(search),
                "Vat" => vatAmountStr.Contains(search),
                "All" or _ => element.DocumentNumber.ToLower().Contains(search) ||
                              element.Amount.ToString().Contains(search) ||
                              GetStatusText(element.Status).ToLower().Contains(search) ||
                              dateStr.Contains(search) ||
                              vatAmountStr.Contains(search)
            };
        }

        private string GetStatusText(DocumentStatus status)
        {
            return status switch
            {
                DocumentStatus.Draft => "черновик",
                DocumentStatus.Processed => "проведен",
                DocumentStatus.Canceled => "отменен",
                _ => status.ToString().ToLower()
            };
        }

        private long ParseNumberForSorting(string docNumber)
        {
            if (string.IsNullOrWhiteSpace(docNumber)) return 0;

            var digitsOnly = new string(docNumber.Where(char.IsDigit).ToArray());

            if (long.TryParse(digitsOnly, out long result))
                return result;

            return 0;
        }

        private decimal CalculateVatAmount(decimal amount, Guid? vatTaxId)
        {
            if (vatTaxId.HasValue && _vatRates.TryGetValue(vatTaxId.Value, out var rate))
            {
                return amount * (rate / 100m);
            }
            return 0m;
        }
    }
}
