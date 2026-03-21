using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.VatTaxes.DeleteVatTax
{
    public record DeleteVatTaxCommand(Guid Id)
        : DeleteBaseCommand<VatTax>(Id);
}
