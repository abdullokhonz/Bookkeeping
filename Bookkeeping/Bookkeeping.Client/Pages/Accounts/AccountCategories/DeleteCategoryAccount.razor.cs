using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts.AccountCategories
{
    public partial class DeleteCategoryAccount
    {
        [Parameter] public Guid Id { get; set; }

        private CategoryAccount5dTreeDto? _category;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadCategoryData();

        private async Task LoadCategoryData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dTreeDto>>($"/api/v1/CategoryAccount5d/GetById/{Id}");
                if (result != null && result.IsSuccess) _category = result.Data;
            }
            catch { Nav.NavigateTo("/accounts"); }
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.DeleteAsync($"/api/v1/CategoryAccount5d/HardDelete/{Id}/permanent");
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория удалена", Severity.Success);
                    Nav.NavigateTo("/accounts");
                }
                else
                {
                    Snackbar.Add("Ошибка: возможно, в категории есть счета", Severity.Error);
                }
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/accounts");
    }
}
