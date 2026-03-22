using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts
{
    public partial class DeleteAccount
    {
        [Parameter] public Guid Id { get; set; }

        private IfrsAccountUpdateDto? account;
        private string? _categoryName;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadAccountData();

        private async Task LoadAccountData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountUpdateDto>>($"/api/v1/IfrsAccount/GetById/{Id}");
                if (result != null && result.IsSuccess)
                {
                    account = result.Data;

                    if (account!.CategoryAccountId.HasValue)
                    {
                        await LoadCategoryName(account.CategoryAccountId.Value);
                    }
                }
                else
                {
                    Snackbar.Add("Запись не найдена", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка загрузки данных", Severity.Error);
                Nav.NavigateTo("/accounts");
            }
        }

        private async Task LoadCategoryName(Guid categoryId)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dTreeDto>>($"/api/v1/CategoryAccount5d/GetById/{categoryId}");
                if (result != null && result.IsSuccess)
                {
                    _categoryName = result.Data?.Name;
                }
            }
            catch
            {
                _categoryName = "Не удалось загрузить категорию";
            }
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.DeleteAsync($"/api/v1/IfrsAccount/HardDelete/{Id}/permanent");

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Счет успешно удален", Severity.Success);
                    Nav.NavigateTo("/accounts");
                }
                else
                {
                    Snackbar.Add("Ошибка при удалении счета", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка при удалении счета", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
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
