using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.VatTaxes.GetPagedVatTax
{
    public record GetPagedVatTaxQuery(int Page, int Size)
        : GetPagedBaseQuery<VatTax, VatTaxGetDto>(Page, Size);
}
