using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.IncomeCategories.UpdateIncomeCategory
{
    public record UpdateIncomeCategoryCommand(Guid Id, IncomeCategoryUpdateDto Dto)
        : UpdateBaseCommand<IncomeCategory, IncomeCategoryUpdateDto>(Id, Dto);
}
