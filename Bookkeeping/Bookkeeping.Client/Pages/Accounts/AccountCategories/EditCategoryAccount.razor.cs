using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts.AccountCategories
{
    public partial class EditCategoryAccount
    {
        [Parameter] public Guid Id { get; set; }

        private CategoryAccount5dUpdateDto? _updateModel;
        private List<CategoryAccount5dTreeDto>? _parentCategories;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadParentCategories();
            await LoadCategoryData();
        }

        private async Task LoadCategoryData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dUpdateDto>>($"/api/v1/CategoryAccount5d/GetById/{Id}");
                if (result != null && result.IsSuccess)
                {
                    _updateModel = result.Data;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка загрузки данных: " + ex.Message, Severity.Error);
            }
        }

        private async Task LoadParentCategories()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<List<CategoryAccount5dTreeDto>>>("/api/v1/CategoryAccount5d/GetAll");
            if (result != null && result.IsSuccess) _parentCategories = result.Data;
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PutAsJsonAsync($"/api/v1/CategoryAccount5d/Update/{Id}", _updateModel);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория обновлена", Severity.Success);
                    Nav.NavigateTo("/accounts");
                }
                else
                {
                    Snackbar.Add("Ошибка при сохранении", Severity.Error);
                }
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/accounts");
    }
}
