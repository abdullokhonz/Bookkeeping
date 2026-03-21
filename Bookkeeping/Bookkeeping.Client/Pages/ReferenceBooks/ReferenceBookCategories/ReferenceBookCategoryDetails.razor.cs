using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks.ReferenceBookCategories
{
    public partial class ReferenceBookCategoryDetails
    {
        [Parameter] public Guid Id { get; set; }
        private ReferenceBookCategoryGetDto? _category;
        private IfrsAccountTreeDto? _accountInfo;

        private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new BreadcrumbItem("Справочники", href: "/reference-books"),
        new BreadcrumbItem("Детали категории", href: null, disabled: true)
    };

        protected override async Task OnInitializedAsync() => await LoadCategoryDetails();

        private async Task LoadCategoryDetails()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookCategoryGetDto>>($"/api/v1/ReferenceBookCategory/GetById/{Id}");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    _category = response.Data;
                    _breadcrumbs[1] = new BreadcrumbItem(_category.Name, href: null, disabled: true);
                    await LoadAccountInfo(_category.IfrsAccountId);
                }
            }
            catch { Snackbar.Add("Ошибка связи с сервером", Severity.Error); }
        }

        private async Task LoadAccountInfo(Guid accountId)
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{accountId}");
            if (result?.IsSuccess == true) _accountInfo = result.Data;
        }

        private void GoToMain() => Nav.NavigateTo("/reference-books");
        private void GoToEdit() => Nav.NavigateTo($"/reference-books/categories/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/reference-books/categories/delete/{Id}");
    }
}
