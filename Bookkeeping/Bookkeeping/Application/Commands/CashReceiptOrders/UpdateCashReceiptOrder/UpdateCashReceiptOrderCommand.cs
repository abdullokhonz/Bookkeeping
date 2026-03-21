using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.UpdateCashReceiptOrder
{
    public record UpdateCashReceiptOrderCommand(Guid Id, CashReceiptOrderUpdateDto Dto)
        : UpdateBaseCommand<CashReceiptOrder, CashReceiptOrderUpdateDto>(Id, Dto);
}
