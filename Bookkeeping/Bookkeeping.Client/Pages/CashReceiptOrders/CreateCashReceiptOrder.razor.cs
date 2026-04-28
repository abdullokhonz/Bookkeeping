using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class CreateCashReceiptOrder
    {
        public record IncomeCategoryGetDto(Guid Id, string Name);
        public record ReferenceBookGetDto(Guid Id, string Name);
        public record VatTaxGetDto(Guid Id, decimal VatRate);

        private CashReceiptOrderCreateDto _pkoModel = new();
        private ImageCreateDto _imageModel = new();
        private IBrowserFile? _selectedFile;

        private bool _isLoadingData = true;
        private bool _isProcessing = false;

        private List<IncomeCategoryGetDto> _incomeCategories = new();
        private List<ReferenceBookGetDto> _referenceBooks = new();
        private List<VatTaxGetDto> _vatTaxes = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var vatTask = Http.GetFromJsonAsync<ApiResponse<List<VatTaxGetDto>>>("/api/v1/VatTax/GetAll");
                var catTask = Http.GetFromJsonAsync<ApiResponse<List<IncomeCategoryGetDto>>>("/api/v1/IncomeCategory/GetAll");
                var refTask = Http.GetFromJsonAsync<ApiResponse<List<ReferenceBookGetDto>>>("/api/v1/ReferenceBook/GetAll");

                await Task.WhenAll(vatTask, catTask, refTask);

                _vatTaxes = vatTask.Result?.Data ?? new();
                _incomeCategories = catTask.Result?.Data?.OrderBy(a => a.Name).ToList() ?? new();
                _referenceBooks = refTask.Result?.Data ?? new();
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка при загрузке справочников. Проверьте соединение.", Severity.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private void OnFileSelected(IBrowserFile? file)
        {
            _selectedFile = file;

            if (file == null)
            {
                _imageModel = new();
                return;
            }

            if (string.IsNullOrWhiteSpace(_imageModel.Name))
            {
                _imageModel.Name = Path.GetFileNameWithoutExtension(file.Name);
            }
        }

        private void ClearSelectedFile()
        {
            _selectedFile = null;
            _imageModel = new();
            StateHasChanged();
        }

        private async Task HandleValidSubmit()
        {
            _isProcessing = true;
            try
            {
                var response = await Http.PostAsJsonAsync("/api/v1/CashReceiptOrder/Create", _pkoModel);

                if (!response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Не удалось создать ордер. Проверьте правильность заполнения.", Severity.Error);
                    return;
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonContent);

                if (doc.RootElement.TryGetProperty("data", out var dataProp) && dataProp.TryGetProperty("id", out var idProp))
                {
                    Guid newId = idProp.GetGuid();

                    if (_selectedFile != null)
                    {
                        await UploadImageAsync(newId);
                    }

                    Snackbar.Add("Приходный кассовый ордер успешно создан!", Severity.Success);
                    Nav.NavigateTo("/cash-receipt-orders");
                }
                else
                {
                    Snackbar.Add("Ордер создан, но не удалось получить его ID для загрузки файла.", Severity.Warning);
                    Nav.NavigateTo("/cash-receipt-orders");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private async Task UploadImageAsync(Guid entityId)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                content.Add(new StringContent(_imageModel.Name ?? ""), "Name");
                content.Add(new StringContent(_imageModel.Description ?? ""), "Description");
                content.Add(new StringContent(entityId.ToString()), "EntityId");

                var streamContent = new StreamContent(_selectedFile!.OpenReadStream(10 * 1024 * 1024));
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(_selectedFile.ContentType);
                content.Add(streamContent, "file", _selectedFile.Name);

                var res = await Http.PostAsync("/api/v1/Image/Upload", content);

                if (!res.IsSuccessStatusCode)
                {
                    Snackbar.Add("ПКО сохранен, но файл не был загружен (ошибка сервера).", Severity.Warning);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("ПКО сохранен, но произошла ошибка при передаче файла.", Severity.Warning);
            }
        }

        private void Cancel() => Nav.NavigateTo("/cash-receipt-orders");

        private void CreateVatTax() => Nav.NavigateTo("/vat-taxes/create");

        private void CreateIncomeCategory() => Nav.NavigateTo("/income-categories/create");

        private void CreateReferenceBook() => Nav.NavigateTo("/reference-books/create");
    }
}
