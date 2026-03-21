using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks
{
    public partial class DeleteReferenceBooks
    {
        [Parameter] public Guid Id { get; set; }

        private ReferenceBookGetDto? book;
        private string? _categoryName;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadData();

        private async Task LoadData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookGetDto>>($"/api/v1/ReferenceBook/GetById/{Id}");
                if (result != null && result.IsSuccess)
                {
                    book = result.Data;
                    await LoadCategoryName(book!.ReferenceBookCategoryId);
                }
                else
                {
                    Snackbar.Add("Запись не найдена", Severity.Error);
                    Nav.NavigateTo("/reference-books");
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка загрузки данных", Severity.Error);
            }
        }

        private async Task LoadCategoryName(Guid categoryId)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookCategoryGetDto>>($"/api/v1/ReferenceBookCategory/GetById/{categoryId}");
                if (result != null && result.IsSuccess) _categoryName = result.Data?.Name;
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
                var response = await Http.DeleteAsync($"/api/v1/ReferenceBook/HardDelete/{Id}/permanent");
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Справочник успешно удален", Severity.Success);
                    Nav.NavigateTo("/reference-books");
                }
                else
                {
                    Snackbar.Add("Ошибка при удалении справочника", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Критическая ошибка при удалении", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
