using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks
{
    public partial class EditReferenceBook
    {
        [Parameter] public Guid Id { get; set; }

        private ReferenceBookUpdateDto? updateModel;
        private List<ReferenceBookCategoryGetDto>? categories;

        private class KeyValueItem
        {
            public string Key { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }
        private List<KeyValueItem> _infoItems = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            await LoadData();
        }

        private async Task LoadCategories()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<List<ReferenceBookCategoryGetDto>>>("/api/v1/ReferenceBookCategory/GetAll");
            if (result != null && result.IsSuccess) categories = result.Data;
        }

        private async Task LoadData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookGetDto>>($"/api/v1/ReferenceBook/GetById/{Id}");
                if (result != null && result.IsSuccess && result.Data != null)
                {
                    var data = result.Data;
                    // Мапим GetDto в UpdateDto
                    updateModel = new ReferenceBookUpdateDto
                    {
                        Name = data.Name,
                        Description = data.Description,
                        ReferenceBookCategoryId = data.ReferenceBookCategoryId,
                        Info = data.Info
                    };

                    // Распаковываем словарь для UI
                    if (data.Info != null)
                    {
                        _infoItems = data.Info.Select(kvp => new KeyValueItem
                        {
                            Key = kvp.Key,
                            Value = kvp.Value?.ToString() ?? string.Empty
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add("Ошибка загрузки данных: " + ex.Message, Severity.Error);
            }
        }

        private void AddInfoItem() => _infoItems.Add(new KeyValueItem());
        private void RemoveInfoItem(KeyValueItem item) => _infoItems.Remove(item);

        private async Task HandleValidSubmit()
        {
            if (updateModel == null) return;

            // Собираем словарь обратно
            updateModel.Info = _infoItems
                .Where(x => !string.IsNullOrWhiteSpace(x.Key))
                .ToDictionary(x => x.Key, x => (object)x.Value);

            var response = await Http.PutAsJsonAsync($"/api/v1/ReferenceBook/Update/{Id}", updateModel);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Изменения сохранены", Severity.Success);
                Nav.NavigateTo("/reference-books");
            }
            else
            {
                Snackbar.Add("Не удалось сохранить изменения", Severity.Error);
            }
        }

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
