using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.SoftDeleteCashReceiptOrder
{
    public record SoftDeleteCashReceiptOrderCommand(Guid Id)
        : SoftDeleteBaseCommand<CashReceiptOrder>(Id);
}
