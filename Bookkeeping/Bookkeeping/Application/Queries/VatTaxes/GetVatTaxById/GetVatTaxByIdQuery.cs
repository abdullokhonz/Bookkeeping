using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.VatTaxes.GetVatTaxById
{
    public record GetVatTaxByIdQuery(Guid Id)
        : GetBaseByIdQuery<VatTax, VatTaxGetDto>(Id);
}
