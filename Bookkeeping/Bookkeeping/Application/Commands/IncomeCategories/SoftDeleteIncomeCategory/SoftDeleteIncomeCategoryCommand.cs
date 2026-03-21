using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.IncomeCategories.SoftDeleteIncomeCategory
{
    public record SoftDeleteIncomeCategoryCommand(Guid Id)
        : SoftDeleteBaseCommand<IncomeCategory>(Id);
}
