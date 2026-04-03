using Bookkeeping.Client.Dialogs;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class EditCashReceiptOrder
    {
        [Parameter] public Guid Id { get; set; }

        public record IncomeCategoryGetDto(Guid Id, string Name);
        public record ReferenceBookGetDto(Guid Id, string Name);
        public record VatTaxGetDto(Guid Id, decimal VatRate);

        public class ApiResponse<T>
        {
            public bool IsSuccess { get; set; }
            public T? Data { get; set; }
            public string? Message { get; set; }
        }

        private CashReceiptOrderUpdateDto? _pkoModel;
        private ImageGetDto? _existingImage;
        private ImageUpdateDto _imageUpdateModel = new();
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
                var orderTask = Http.GetFromJsonAsync<ApiResponse<CashReceiptOrderGetDto>>($"/api/v1/CashReceiptOrder/GetById/{Id}");

                await Task.WhenAll(vatTask, catTask, refTask, orderTask);

                _vatTaxes = vatTask.Result?.Data ?? new();
                _incomeCategories = catTask.Result?.Data?.OrderBy(a => a.Name).ToList() ?? new();
                _referenceBooks = refTask.Result?.Data ?? new();

                var orderData = orderTask.Result?.Data;
                if (orderData != null)
                {
                    _pkoModel = new CashReceiptOrderUpdateDto
                    {
                        Name = orderData.Name,
                        Description = orderData.Description,
                        Amount = orderData.Amount,
                        Status = orderData.Status,
                        IncomeCategoryId = orderData.IncomeCategoryId,
                        ReferenceBookId = orderData.ReferenceBookId,
                        VatTaxId = orderData.VatTaxId,
                        Accountant = orderData.Accountant,
                        Cashier = orderData.Cashier
                    };

                    await LoadImageInfo();
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Ошибка загрузки: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isLoadingData = false;
            }
        }

        private async Task LoadImageInfo()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<ImageGetDto>>>("/api/v1/Image/GetAll");
                if (response?.IsSuccess == true && response.Data != null)
                {
                    var foundImage = response.Data.FirstOrDefault(x => x.EntityId == Id);
                    if (foundImage != null)
                    {
                        _existingImage = foundImage;
                        _imageUpdateModel.Name = foundImage.Name;
                        _imageUpdateModel.Description = foundImage.Description;
                        _imageUpdateModel.EntityId = Id;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка загрузки файла: {ex.Message}"); }
        }

        private async Task RemoveFileAsync()
        {
            if (_existingImage != null)
            {
                var parameters = new DialogParameters
            {
                { nameof(ConfirmDeleteDialog.ContentText), $"Удалить файл \"{_existingImage.Name}\" из базы данных навсегда?" },
                { nameof(ConfirmDeleteDialog.Title), "Удаление файла" },
                { nameof(ConfirmDeleteDialog.ConfirmText), "Удалить" }
            };
                var options = new DialogOptions { CloseButton = false, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
                var dialog = await DialogService.ShowAsync<ConfirmDeleteDialog>("Удаление", parameters, options);
                var result = await dialog.Result;

                if (result!.Canceled) return;

                var resp = await Http.DeleteAsync($"/api/v1/Image/HardDelete/{_existingImage.Id}/permanent");
                if (!resp.IsSuccessStatusCode)
                {
                    Snackbar.Add("Ошибка при удалении из базы", Severity.Error);
                    return;
                }
                Snackbar.Add("Файл удален", Severity.Success);
            }

            _existingImage = null;
            _selectedFile = null;
            _imageUpdateModel = new ImageUpdateDto();
            StateHasChanged();
        }

        private void OnFileSelected(IBrowserFile? file)
        {
            if (file == null) return;
            _selectedFile = file;
            _imageUpdateModel.EntityId = Id;
            if (string.IsNullOrWhiteSpace(_imageUpdateModel.Name))
            {
                _imageUpdateModel.Name = Path.GetFileNameWithoutExtension(file.Name);
            }
        }

        private async Task HandleValidSubmit()
        {
            if (_pkoModel == null) return;
            _isProcessing = true;
            try
            {
                var pkoResponse = await Http.PutAsJsonAsync($"/api/v1/CashReceiptOrder/Update/{Id}", _pkoModel);
                if (!pkoResponse.IsSuccessStatusCode)
                {
                    Snackbar.Add("Ошибка при сохранении ПКО", Severity.Error);
                    return;
                }

                bool fileProcessed = await ProcessFileAsync();
                if (!fileProcessed)
                {
                    Snackbar.Add("ПКО обновлен, но возникла ошибка при работе с файлом", Severity.Warning);
                }
                else
                {
                    Snackbar.Add("Изменения успешно сохранены", Severity.Success);
                }

                Nav.NavigateTo("/cash-receipt-orders");
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Критическая ошибка: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private async Task<bool> ProcessFileAsync()
        {
            try
            {
                if (_existingImage == null && _selectedFile == null)
                    return true;

                if (_selectedFile != null)
                {
                    using var content = new MultipartFormDataContent();
                    content.Add(new StringContent(_imageUpdateModel.Name ?? ""), "Name");
                    content.Add(new StringContent(_imageUpdateModel.Description ?? ""), "Description");
                    content.Add(new StringContent(Id.ToString()), "EntityId");

                    var stream = _selectedFile.OpenReadStream(10 * 1024 * 1024);
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(_selectedFile.ContentType);
                    content.Add(fileContent, "file", _selectedFile.Name);

                    if (_existingImage != null)
                    {
                        var response = await Http.PutAsync($"/api/v1/Image/Update/{_existingImage.Id}", content);
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        var response = await Http.PostAsync("/api/v1/Image/Upload", content);
                        return response.IsSuccessStatusCode;
                    }
                }

                if (_existingImage != null && (_imageUpdateModel.Name != _existingImage.Name ||
                                                _imageUpdateModel.Description != _existingImage.Description))
                {
                    using var content = new MultipartFormDataContent();
                    content.Add(new StringContent(_imageUpdateModel.Name ?? ""), "Name");
                    content.Add(new StringContent(_imageUpdateModel.Description ?? ""), "Description");
                    content.Add(new StringContent(Id.ToString()), "EntityId");
                    var response = await Http.PutAsync($"/api/v1/Image/Update/{_existingImage.Id}", content);
                    return response.IsSuccessStatusCode;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки файла: {ex.Message}");
                return false;
            }
        }

        private string GetStatusName(DocumentStatus status) => status switch
        {
            DocumentStatus.Draft => "Черновик",
            DocumentStatus.Processed => "Проведен",
            DocumentStatus.Canceled => "Отменен",
            _ => status.ToString()
        };

        private Color GetStatusColor(DocumentStatus status) => status switch
        {
            DocumentStatus.Processed => Color.Success,
            DocumentStatus.Canceled => Color.Error,
            DocumentStatus.Draft => Color.Warning,
            _ => Color.Default
        };

        private string GetStatusIcon(DocumentStatus status) => status switch
        {
            DocumentStatus.Processed => Icons.Material.Filled.CheckCircle,
            DocumentStatus.Canceled => Icons.Material.Filled.Cancel,
            DocumentStatus.Draft => Icons.Material.Filled.EditNote,
            _ => Icons.Material.Filled.Error
        };
    }
}
