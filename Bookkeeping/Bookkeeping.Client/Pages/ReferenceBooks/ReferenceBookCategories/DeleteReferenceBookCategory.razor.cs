using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.ReferenceBooks.ReferenceBookCategories
{
    public partial class DeleteReferenceBookCategory
    {
        [Parameter] public Guid Id { get; set; }
        private ReferenceBookCategoryGetDto? _model;
        private string? _linkedAccountName;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadData();

        private async Task LoadData()
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookCategoryGetDto>>($"/api/v1/ReferenceBookCategory/GetById/{Id}");
                if (result?.IsSuccess == true)
                {
                    _model = result.Data;
                    await LoadAccountInfo(_model!.IfrsAccountId);
                }
            }
            catch { Snackbar.Add("Ошибка загрузки данных", Severity.Error); }
        }

        private async Task LoadAccountInfo(Guid accountId)
        {
            var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{accountId}");
            if (result?.IsSuccess == true)
                _linkedAccountName = $"{result.Data?.AccountNumber} {result.Data?.AccountName}";
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.DeleteAsync($"/api/v1/ReferenceBookCategory/HardDelete/{Id}/permanent");
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Категория удалена", Severity.Success);
                    Nav.NavigateTo("/reference-books");
                }
                else Snackbar.Add("Ошибка при удалении", Severity.Error);
            }
            finally { _isProcessing = false; }
        }

        private void Cancel() => Nav.NavigateTo("/reference-books");
    }
}
