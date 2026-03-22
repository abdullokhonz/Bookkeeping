using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.CashReceiptOrders
{
    public partial class DeleteCashReceiptOrder
    {
        [Parameter] public Guid Id { get; set; }

        private CashReceiptOrderGetDto? order;
        private ImageGetDto? image;
        private bool _isProcessing = false;

        protected override async Task OnInitializedAsync() => await LoadData();

        private async Task LoadData()
        {
            try
            {
                var orderTask = Http.GetFromJsonAsync<ApiResponse<CashReceiptOrderGetDto>>($"/api/v1/CashReceiptOrder/GetById/{Id}");
                var imagesTask = Http.GetFromJsonAsync<ApiResponse<List<ImageGetDto>>>("/api/v1/Image/GetAll");

                await Task.WhenAll(orderTask, imagesTask);

                var orderRes = orderTask.Result;
                if (orderRes != null && orderRes.IsSuccess)
                    order = orderRes.Data;
                else
                    Snackbar.Add("Запись не найдена", Severity.Error);

                var imagesRes = imagesTask.Result;
                if (imagesRes?.IsSuccess == true && imagesRes.Data != null)
                    image = imagesRes.Data.FirstOrDefault(x => x.EntityId == Id);
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка загрузки данных", Severity.Error);
                Nav.NavigateTo("/cash-receipt-orders");
            }
        }

        private async Task HandleDelete()
        {
            _isProcessing = true;
            try
            {
                bool fileDeleted = true;
                if (image != null)
                {
                    var fileResponse = await Http.DeleteAsync($"/api/v1/Image/HardDelete/{image.Id}/permanent");
                    if (!fileResponse.IsSuccessStatusCode)
                    {
                        Snackbar.Add("Не удалось удалить прикреплённый файл", Severity.Warning);
                        fileDeleted = false;
                    }
                }

                var orderResponse = await Http.DeleteAsync($"/api/v1/CashReceiptOrder/HardDelete/{Id}/permanent");

                if (orderResponse.IsSuccessStatusCode)
                {
                    Snackbar.Add("Ордер успешно удален", Severity.Success);
                    Nav.NavigateTo("/cash-receipt-orders");
                }
                else
                {
                    Snackbar.Add("Ошибка при удалении ордера", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка при удалении", Severity.Error);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/cash-receipt-orders");

        private Color GetStatusColor(DocumentStatus status) => status switch
        {
            DocumentStatus.Processed => Color.Success,
            DocumentStatus.Canceled => Color.Error,
            DocumentStatus.Draft => Color.Warning,
            _ => Color.Default
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
