using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class EditIncomeCategory
    {
        [Parameter] public Guid Id { get; set; }

        private IncomeCategoryUpdateDto? _model;
        private List<IfrsAccountTreeDto>? _accounts;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
            await LoadCategoryData();
        }

        private async Task LoadAccounts()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<List<IfrsAccountTreeDto>>>("/api/v1/IfrsAccount/GetAll");
            if (result?.IsSuccess == true) _accounts = result.Data;
        }

        private async Task LoadCategoryData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IncomeCategoryUpdateDto>>($"/api/v1/IncomeCategory/GetById/{Id}");
                if (result?.IsSuccess == true) _model = result.Data;
                else Snackbar.Add("Статья не найдена", Severity.Error);
            }
            catch { Snackbar.Add("Ошибка загрузки данных", Severity.Error); }
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PutAsJsonAsync($"/api/v1/IncomeCategory/Update/{Id}", _model);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Статья успешно обновлена", Severity.Success);
                    Nav.NavigateTo("/income-categories");
                }
                else Snackbar.Add("Ошибка при сохранении", Severity.Error);
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/income-categories");
    }
}
