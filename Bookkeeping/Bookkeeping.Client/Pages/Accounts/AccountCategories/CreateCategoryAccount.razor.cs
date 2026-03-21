using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts.AccountCategories
{
    public partial class CreateCategoryAccount
    {
        private CategoryAccount5dCreateDto _category = new();
        private List<CategoryAccount5dTreeDto>? _parentCategories;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadParentCategories();

        private async Task LoadParentCategories()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<CategoryAccount5dTreeDto>>>("/api/v1/CategoryAccount5d/GetAll");
                if (response != null && response.IsSuccess)
                {
                    _parentCategories = response.Data;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Не удалось загрузить список категорий: " + ex.Message, Severity.Error);
            }
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/CategoryAccount5d/Create", _category);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория успешно создана!", Severity.Success);
                    Nav.NavigateTo("/accounts");
                }
                else
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                    Snackbar.Add(errorResult?.Message ?? "Ошибка при создании категории", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка сети: " + ex.Message, Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/accounts");
    }
}
