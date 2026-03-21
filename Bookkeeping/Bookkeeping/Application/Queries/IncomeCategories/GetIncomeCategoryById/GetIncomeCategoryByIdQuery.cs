using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetIncomeCategoryById
{
    public record GetIncomeCategoryByIdQuery(Guid Id)
        : GetBaseByIdQuery<IncomeCategory, IncomeCategoryGetDto>(Id);
}
