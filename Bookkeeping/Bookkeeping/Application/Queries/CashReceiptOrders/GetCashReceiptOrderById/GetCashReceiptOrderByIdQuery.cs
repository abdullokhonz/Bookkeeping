using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetCashReceiptOrderById
{
    public record GetCashReceiptOrderByIdQuery(Guid Id)
        : GetBaseByIdQuery<CashReceiptOrder, CashReceiptOrderGetDto>(Id);
}
