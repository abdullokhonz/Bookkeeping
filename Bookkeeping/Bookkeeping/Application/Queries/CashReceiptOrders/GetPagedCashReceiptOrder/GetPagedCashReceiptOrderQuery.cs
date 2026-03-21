using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetPagedCashReceiptOrder
{
    public record GetPagedCashReceiptOrderQuery(int Page, int Size)
        : GetPagedBaseQuery<CashReceiptOrder, CashReceiptOrderGetDto>(Page, Size);
}
