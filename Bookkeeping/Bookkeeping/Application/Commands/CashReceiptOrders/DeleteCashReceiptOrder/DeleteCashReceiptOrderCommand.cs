using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.DeleteCashReceiptOrder
{
    public record DeleteCashReceiptOrderCommand(Guid Id)
        : DeleteBaseCommand<CashReceiptOrder>(Id);
}
