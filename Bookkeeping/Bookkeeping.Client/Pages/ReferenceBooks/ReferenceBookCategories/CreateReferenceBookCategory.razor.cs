using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks.ReferenceBookCategories
{
    public partial class CreateReferenceBookCategory
    {
        private ReferenceBookCategoryCreateDto _category = new();
        private List<IfrsAccountTreeDto>? _ifrsAccounts;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadIfrsAccounts();

        private async Task LoadIfrsAccounts()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<IfrsAccountTreeDto>>>("/api/v1/IfrsAccount/tree");
                if (response != null && response.IsSuccess)
                {
                    _ifrsAccounts = response.Data;
                }
                else
                {
                    Snackbar.Add("Не удалось загрузить список счетов", Severity.Warning);
                    _ifrsAccounts = new();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка при загрузке счетов: " + ex.Message, Severity.Error);
                _ifrsAccounts = new();
            }
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/ReferenceBookCategory/Create", _category);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория справочников создана", Severity.Success);
                    Nav.NavigateTo("/reference-books");
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

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
