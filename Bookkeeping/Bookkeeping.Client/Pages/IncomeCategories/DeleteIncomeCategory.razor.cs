using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class DeleteIncomeCategory
    {
        [Parameter] public Guid Id { get; set; }
        private IncomeCategoryUpdateDto? _model;
        private string? _linkedAccountName;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadData();

        private async Task LoadData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IncomeCategoryUpdateDto>>($"/api/v1/IncomeCategory/GetById/{Id}");
                if (result?.IsSuccess == true)
                {
                    _model = result.Data;
                    if (_model!.IfrsAccountId.HasValue) await LoadAccountInfo(_model.IfrsAccountId.Value);
                }
            }
            catch { Snackbar.Add("Ошибка загрузки данных", Severity.Error); }
        }

        private async Task LoadAccountInfo(Guid accountId)
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{accountId}");
            if (result?.IsSuccess == true)
                _linkedAccountName = $"{result.Data?.AccountNumber} {result.Data?.AccountName}";
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.DeleteAsync($"/api/v1/IncomeCategory/HardDelete/{Id}/permanent");
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Статья полностью удалена", Severity.Success);
                    Nav.NavigateTo("/income-categories");
                }
                else Snackbar.Add("Не удалось удалить статью", Severity.Error);
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/income-categories");
    }
}
