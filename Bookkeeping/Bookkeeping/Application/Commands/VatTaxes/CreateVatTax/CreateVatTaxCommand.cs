using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.VatTaxes.CreateVatTax
{
    public record CreateVatTaxCommand(VatTaxCreateDto Dto)
        : CreateBaseCommand<VatTax, VatTaxCreateDto, VatTaxGetDto>(Dto);
}
