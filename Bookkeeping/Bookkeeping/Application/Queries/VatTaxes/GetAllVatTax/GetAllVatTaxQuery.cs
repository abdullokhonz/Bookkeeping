using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.VatTaxes.GetAllVatTax
{
    public record GetAllVatTaxQuery()
        : GetAllBaseQuery<VatTax, VatTaxGetDto>;
}
