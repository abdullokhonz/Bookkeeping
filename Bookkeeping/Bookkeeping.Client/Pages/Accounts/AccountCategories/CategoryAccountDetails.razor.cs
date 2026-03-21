using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Accounts.AccountCategories
{
    public partial class CategoryAccountDetails
    {
        [Parameter] public Guid Id { get; set; }
        private CategoryAccount5dTreeDto? _category;
        private string? _parentName;

        private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new BreadcrumbItem("План счетов", href: "/accounts"),
        new BreadcrumbItem("Категория", href: null, disabled: true)
    };

        protected override async Task OnParametersSetAsync()
        {
            _category = null;
            _parentName = null;
            await LoadCategoryDetails();
        }

        private async Task LoadCategoryDetails()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dTreeDto>>($"/api/v1/CategoryAccount5d/GetById/{Id}");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _category = response.Data;
                    _breadcrumbs[1] = new BreadcrumbItem(_category.Name, href: null, disabled: true);
                    if (_category.ParentId.HasValue) await LoadParentName(_category.ParentId.Value);
                }
                else { GoToMain(); }
            }
            catch (Exception) { Snackbar.Add("Ошибка загрузки", Severity.Error); }
        }

        private async Task LoadParentName(Guid parentId)
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<CategoryAccount5dTreeDto>>($"/api/v1/CategoryAccount5d/GetById/{parentId}");
            if (result?.IsSuccess == true) _parentName = result.Data?.Name;
        }

        private void GoToMain() => Nav.NavigateTo("/accounts");
        private void GoToEdit() => Nav.NavigateTo($"/accounts/_categories/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/accounts/_categories/delete/{Id}");
    }
}
