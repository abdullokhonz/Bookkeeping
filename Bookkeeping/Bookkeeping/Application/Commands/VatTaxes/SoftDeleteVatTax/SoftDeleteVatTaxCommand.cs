using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.VatTaxes.SoftDeleteVatTax
{
    public record SoftDeleteVatTaxCommand(Guid Id)
        : SoftDeleteBaseCommand<VatTax>(Id);
}
