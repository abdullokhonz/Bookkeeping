using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.IncomeCategories.CreateIncomeCategory
{
    public record CreateIncomeCategoryCommand(IncomeCategoryCreateDto Dto)
        : CreateBaseCommand<IncomeCategory, IncomeCategoryCreateDto, IncomeCategoryGetDto>(Dto);
}
