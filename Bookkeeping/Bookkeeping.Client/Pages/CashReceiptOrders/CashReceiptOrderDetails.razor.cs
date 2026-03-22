using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class CashReceiptOrderDetails
    {
        [Parameter] public Guid Id { get; set; }

        private CashReceiptOrderGetDto? _order;
        private IfrsAccountTreeDto? _debitAccount;
        private IfrsAccountTreeDto? _creditAccount;
        private IncomeCategoryGetDto? _incomeCategory;
        private ReferenceBookGetDto? _referenceBook;
        private VatTaxGetDto? _vatTax;
        private ImageGetDto? _image;

        private List<BreadcrumbItem> _breadcrumbs = new()
    {
        new BreadcrumbItem("Приходные кассовые ордера", href: "/cash-receipt-orders"),
        new BreadcrumbItem("Детали документа", href: null, disabled: true)
    };

        protected override async Task OnParametersSetAsync()
        {
            _order = null;
            _debitAccount = null;
            _creditAccount = null;
            _incomeCategory = null;
            _referenceBook = null;
            _vatTax = null;
            _image = null;
            await LoadOrderDetails();
        }

        private async Task LoadOrderDetails()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<CashReceiptOrderGetDto>>($"/api/v1/CashReceiptOrder/GetById/{Id}");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _order = response.Data;
                    _breadcrumbs[1] = new BreadcrumbItem($"ПКО №{_order.DocumentNumber}", href: null, disabled: true);

                    var tasks = new List<Task>();

                    if (_order.DebitIfrsAccountId != Guid.Empty)
                        tasks.Add(LoadDebitAccount(_order.DebitIfrsAccountId));
                    if (_order.CreditIfrsAccountId != Guid.Empty)
                        tasks.Add(LoadCreditAccount(_order.CreditIfrsAccountId));
                    if (_order.IncomeCategoryId != Guid.Empty)
                        tasks.Add(LoadIncomeCategory(_order.IncomeCategoryId));
                    if (_order.ReferenceBookId != Guid.Empty)
                        tasks.Add(LoadReferenceBook(_order.ReferenceBookId));
                    if (_order.VatTaxId.HasValue)
                        tasks.Add(LoadVatTax(_order.VatTaxId.Value));
                    tasks.Add(LoadImage());

                    await Task.WhenAll(tasks);
                }
                else
                {
                    Snackbar.Add("Не удалось найти данные ордера", Severity.Warning);
                    GoToMain();
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка связи с сервером", Severity.Error);
            }
        }

        private async Task LoadDebitAccount(Guid id)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{id}");
                if (result?.IsSuccess == true && result.Data != null)
                    _debitAccount = result.Data;
            }
            catch { }
        }

        private async Task LoadCreditAccount(Guid id)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IfrsAccountTreeDto>>($"/api/v1/IfrsAccount/GetById/{id}");
                if (result?.IsSuccess == true && result.Data != null)
                    _creditAccount = result.Data;
            }
            catch { }
        }

        private async Task LoadIncomeCategory(Guid id)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<IncomeCategoryGetDto>>($"/api/v1/IncomeCategory/GetById/{id}");
                if (result?.IsSuccess == true && result.Data != null)
                    _incomeCategory = result.Data;
            }
            catch { }
        }

        private async Task LoadReferenceBook(Guid id)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<ReferenceBookGetDto>>($"/api/v1/ReferenceBook/GetById/{id}");
                if (result?.IsSuccess == true && result.Data != null)
                    _referenceBook = result.Data;
            }
            catch { }
        }

        private async Task LoadVatTax(Guid id)
        {
            try
            {
                var result = await Http.GetFromJsonAsync<ApiResponse<VatTaxGetDto>>($"/api/v1/VatTax/GetById/{id}");
                if (result?.IsSuccess == true && result.Data != null)
                    _vatTax = result.Data;
            }
            catch { }
        }

        private async Task LoadImage()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<List<ImageGetDto>>>("/api/v1/Image/GetAll");
                if (response?.IsSuccess == true && response.Data != null)
                    _image = response.Data.FirstOrDefault(x => x.EntityId == Id);
            }
            catch { }
        }

        private string FormatAmount(decimal amount)
        {
            return amount % 1 == 0 ? amount.ToString("N0") : amount.ToString("N2");
        }

        private string FormatRate(decimal rate)
        {
            return rate % 1 == 0 ? rate.ToString("0") : rate.ToString("N2");
        }

        private void CopyDocumentNumber()
        {
            Snackbar.Add($"Номер {_order?.DocumentNumber} скопирован!", Severity.Success);
        }

        private void DownloadFile()
        {
            if (!string.IsNullOrWhiteSpace(_image?.Path))
            {
                Nav.NavigateTo($"/api/v1/Image/Download/{_image.Id}", forceLoad: true);
            }
            else
            {
                Snackbar.Add("Файл недоступен для скачивания", Severity.Warning);
            }
        }

        private void GoToMain() => Nav.NavigateTo("/cash-receipt-orders");
        private void GoToEdit() => Nav.NavigateTo($"/cash-receipt-orders/edit/{Id}");
        private void GoToDelete() => Nav.NavigateTo($"/cash-receipt-orders/delete/{Id}");

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

        private string GetReadableStatus(DocumentStatus status) => status switch
        {
            DocumentStatus.Draft => "Черновик",
            DocumentStatus.Processed => "Проведен",
            DocumentStatus.Canceled => "Отменен",
            _ => status.ToString()
        };
    }
}
