using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks.ReferenceBookCategories
{
    public partial class EditReferenceBookCategory
    {
        [Parameter] public Guid Id { get; set; }

        private ReferenceBookCategoryUpdateDto? _model;
        private List<IfrsAccountTreeDto>? _accounts;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAccounts();
            await LoadCategoryData();
        }

        private async Task LoadAccounts()
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<List<IfrsAccountTreeDto>>>("/api/v1/IfrsAccount/tree");
            if (result?.IsSuccess == true) _accounts = result.Data?.OrderBy(a => a.AccountNumber).ToList();
        }

        private async Task LoadCategoryData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookCategoryUpdateDto>>($"/api/v1/ReferenceBookCategory/GetById/{Id}");
                if (result?.IsSuccess == true) _model = result.Data;
            }
            catch { Snackbar.Add("Ошибка загрузки данных", Severity.Error); }
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PutAsJsonAsync($"/api/v1/ReferenceBookCategory/Update/{Id}", _model);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория обновлена", Severity.Success);
                    Nav.NavigateTo("/reference-books");
                }
                else Snackbar.Add("Ошибка при сохранении", Severity.Error);
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
