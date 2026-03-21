using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetAllCashReceiptOrder
{
    public record GetAllCashReceiptOrderQuery
        : GetAllBaseQuery<CashReceiptOrder, CashReceiptOrderGetDto>;
}
