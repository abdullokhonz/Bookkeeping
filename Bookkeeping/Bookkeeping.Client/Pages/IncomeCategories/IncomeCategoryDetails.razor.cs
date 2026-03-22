using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.IncomeCategories
{
    public partial class IncomeCategoryDetails
    {
        [Parameter] public Guid Id { get; set; }

        private IncomeCategoryGetDto? _category;
        private IfrsAccountTreeDto? _accountInfo;
        private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new BreadcrumbItem("Статьи доходов", href: "/income-categories"),
        new BreadcrumbItem("Детали статьи", href: null, disabled: true)
    };

        protected override async Task OnParametersSetAsync()
        {
            _category = null;
            _accountInfo = null;
            await LoadCategoryDetails();
        }

        private async Task LoadCategoryDetails()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<IncomeCategoryGetDto>>($"/api/v1/IncomeCategory/GetById/{Id}");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _category = response.Data;
                    _breadcrumbs[1] = new BreadcrumbItem(_category.Name, href: null, disabled: true);
                    await LoadAccountInfo(_category.IfrsAccountId);
                }
                else
                {
                    Snackbar.Add("Не удалось найти статью доходов", Severity.Warning);
                    GoToMain();
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка связи с сервером", Severity.Error);
                GoToMain();
            }
        }

        private async Task LoadAccountInfo(Guid accountId)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{accountId}");
                if (result?.IsSuccess == true && result.Data != null)
                {
                    _accountInfo = result.Data;
                }
            }
            catch { }
        }

        private void GoToMain() => Nav.NavigateTo("/income-categories");
        private void GoToEdit() => Nav.NavigateTo($"/income-categories/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/income-categories/delete/{Id}");
    }
}
