using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks
{
    public partial class ReferenceBookDetails
    {
        [Parameter] public Guid Id { get; set; }

        private ReferenceBookGetDto? _book;
        private string? _categoryName;

        private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new BreadcrumbItem("Справочники", href: "/reference-books"),
        new BreadcrumbItem("Детали справочника", href: null, disabled: true)
    };

        protected override async Task OnParametersSetAsync()
        {
            _book = null;
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookGetDto>>($"/api/v1/ReferenceBook/GetById/{Id}");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _book = response.Data;
                    _breadcrumbs[1] = new BreadcrumbItem($"Справочник: {_book.Name}", href: null, disabled: true);
                    await LoadCategoryName(_book.ReferenceBookCategoryId);
                }
                else
                {
                    Snackbar.Add("Не удалось найти данные", Severity.Warning);
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
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookCategoryGetDto>>($"/api/v1/ReferenceBookCategory/GetById/{categoryId}");
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

        private void GoToMain() => Nav.NavigateTo("/reference-books");
        private void GoToEdit() => Nav.NavigateTo($"/reference-books/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/reference-books/delete/{Id}");
    }
}
