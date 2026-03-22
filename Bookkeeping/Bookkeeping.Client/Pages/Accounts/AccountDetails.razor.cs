using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts
{
    public partial class AccountDetails
    {
        [Parameter] public Guid Id { get; set; }

        private IfrsAccountTreeDto? _account;
        private string? _categoryName;

        private List<BreadcrumbItem> _breadcrumbs = new()
        {
            new BreadcrumbItem("План счетов", href: "/accounts"),
            new BreadcrumbItem("Детали счета", href: null, disabled: true)
        };

        protected override async Task OnParametersSetAsync()
        {
            _account = null;
            await LoadAccountDetails();
        }

        private async Task LoadAccountDetails()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{Id}");

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _account = response.Data;

                    _breadcrumbs[1] = new BreadcrumbItem($"Счет {_account.AccountNumber}", href: null, disabled: true);

                    await LoadCategoryName(_account.CategoryAccountId);
                }
                else
                {
                    Snackbar.Add("Не удалось найти данные счета", Severity.Warning);
                    GoToMain();
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка связи с сервером", Severity.Error);
            }
        }

        private async Task LoadCategoryName(Guid categoryId)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dTreeDto>>($"/api/v1/CategoryAccount5d/GetById/{categoryId}");
                if (result != null && result.IsSuccess && result.Data != null)
                {
                    _categoryName = result.Data.Name;
                }
            }
            catch
            {
                _categoryName = "Категория не определена";
            }
        }

        private void CopyAccountNumber()
        {
            Snackbar.Add($"Номер {_account?.AccountNumber} скопирован!", Severity.Success);
        }

        private void GoToMain() => Nav.NavigateTo("/accounts");
        private void GoToEdit() => Nav.NavigateTo($"/accounts/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/accounts/delete/{Id}");

        private string GetReadableNature(AccountNature nature) => nature switch
        {
            AccountNature.Active => "Активный",
            AccountNature.Passive => "Пассивный",
            AccountNature.ActivePassive => "Активно-пассивный",
            _ => nature.ToString()
        };
    }
}
