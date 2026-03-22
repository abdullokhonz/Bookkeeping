using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks
{
    public partial class CreateReferenceBook
    {
        private ReferenceBookCreateDto _referenceBook = new()
        {
            Info = new Dictionary<string, object>()
        };

        private class KeyValueItem
        {
            public string Key { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }

        private List<KeyValueItem> _infoItems = new();

        public record ReferenceBookCategoryGetDto(Guid Id, string Name);
        private List<ReferenceBookCategoryGetDto>? _categories;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<ReferenceBookCategoryGetDto>>>("/api/v1/ReferenceBookCategory/GetAll");
                if (response != null && response.IsSuccess)
                {
                    _categories = response.Data;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка загрузки категорий: " + ex.Message, Severity.Error);
            }
        }

        private void AddInfoItem()
        {
            _infoItems.Add(new KeyValueItem());
        }

        private void RemoveInfoItem(KeyValueItem item)
        {
            _infoItems.Remove(item);
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                _referenceBook.Info = _infoItems
                    .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                    .ToDictionary(
                        x => x.Key,
                        x => (object)x.Value
                    );

                var response = await Http.PostAsJsonAsync("/api/v1/ReferenceBook/Create", _referenceBook);

                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Справочник успешно создан!", Severity.Success);
                    Nav.NavigateTo("/reference-books");
                }
                else
                {
                    Snackbar.Add("Ошибка при сохранении справочника", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Произошла ошибка: " + ex.Message, Severity.Error);
            }
        }

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
