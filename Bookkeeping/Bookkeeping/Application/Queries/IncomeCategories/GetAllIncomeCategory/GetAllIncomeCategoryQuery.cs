using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetAllIncomeCategory
{
    public record GetAllIncomeCategoryQuery()
        : GetAllBaseQuery<IncomeCategory, IncomeCategoryGetDto>;
}
