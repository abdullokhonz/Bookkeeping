using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Enums;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts
{
    public partial class CreateAccount
    {
        private IfrsAccountCreateDto _account = new()
        {
            AccountNature = null,
            CategoryAccountId = null,
            ParentId = null,
            IsActive = true
        };

        private List<CategoryAccount5dTreeDto>? _categories;

        protected override async Task OnInitializedAsync() => await LoadCategories();

        private async Task LoadCategories()
        {
            try
            {
                // ВНИМАНИЕ: Проверь в F12, как называется поле с данными: Data, Value или Result?
                var response = await Http.GetFromJsonAsync<ApiResponse<List<CategoryAccount5dTreeDto>>>("/api/v1/CategoryAccount5d/GetAll");
                if (response != null && response.IsSuccess)
                {
                    _categories = response.Data;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка сети: " + ex.Message, Severity.Error);
            }
        }

        private async Task HandleValidSubmit()
        {
            var response = await Http.PostAsJsonAsync("/api/v1/IfrsAccount/Create", _account);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Счет создан!", Severity.Success);
                Nav.NavigateTo("/accounts");
            }
            else
            {
                Snackbar.Add("Ошибка сохранения", Severity.Error);
            }
        }

        private void Cancel() => Nav.NavigateTo("/accounts");

        private string GetReadableName(AccountNature type) => type switch
        {
            AccountNature.Active => "Активный",
            AccountNature.Passive => "Пассивный",
            AccountNature.ActivePassive => "Активно-пассивный",
            _ => type.ToString()
        };
    }
}
