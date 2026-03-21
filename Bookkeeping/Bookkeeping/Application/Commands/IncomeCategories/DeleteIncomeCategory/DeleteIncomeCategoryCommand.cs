using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.IncomeCategories.DeleteIncomeCategory
{
    public record DeleteIncomeCategoryCommand(Guid Id)
        : DeleteBaseCommand<IncomeCategory>(Id);
}
