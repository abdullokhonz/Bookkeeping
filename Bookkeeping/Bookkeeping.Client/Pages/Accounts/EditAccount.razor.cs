using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts
{
    public partial class EditAccount
    {
        [Parameter] public Guid Id { get; set; }

        private IfrsAccountUpdateDto? updateModel;
        private List<CategoryAccount5dTreeDto>? categories;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            await LoadAccountData();
        }

        private async Task LoadAccountData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountUpdateDto>>($"/api/v1/IfrsAccount/GetById/{Id}");
                if (result != null && result.IsSuccess)
                {
                    updateModel = result.Data;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка загрузки данных: " + ex.Message, Severity.Error);
            }
        }

        private async Task LoadCategories()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<List<CategoryAccount5dTreeDto>>>("/api/v1/CategoryAccount5d/GetAll");
            if (result != null && result.IsSuccess) categories = result.Data;
        }

        private async Task HandleValidSubmit()
        {
            var response = await Http.PutAsJsonAsync($"/api/v1/IfrsAccount/Update/{Id}", updateModel);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Изменения сохранены", Severity.Success);
                Nav.NavigateTo("/accounts");
            }
            else
            {
                Snackbar.Add("Не удалось сохранить изменения", Severity.Error);
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
