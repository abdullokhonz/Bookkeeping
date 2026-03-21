using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetPagedIncomeCategory
{
    public record GetPagedIncomeCategoryQuery(int Page, int Size)
        : GetPagedBaseQuery<IncomeCategory, IncomeCategoryGetDto>(Page, Size);
}
