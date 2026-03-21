using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.DeleteCategoryAccount5d
{
    public record DeleteIfrsAccountCommand(Guid Id)
        : DeleteBaseCommand<CategoryAccount5d>(Id);
}
