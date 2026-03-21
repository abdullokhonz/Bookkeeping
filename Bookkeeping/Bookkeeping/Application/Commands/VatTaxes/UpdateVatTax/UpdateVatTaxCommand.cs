using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.VatTaxes.UpdateVatTax
{
    public record UpdateVatTaxCommand(Guid Id, VatTaxUpdateDto Dto)
        : UpdateBaseCommand<VatTax, VatTaxUpdateDto>(Id, Dto);
}
