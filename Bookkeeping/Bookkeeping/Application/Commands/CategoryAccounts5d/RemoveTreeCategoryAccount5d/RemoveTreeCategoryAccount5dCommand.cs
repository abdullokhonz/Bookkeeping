using Bookkeeping.Application.Commands.Base.RemoveTreeBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.RemoveTreeCategoryAccount5d
{
    public record RemoveTreeCategoryAccount5dCommand(Guid Id)
        : RemoveTreeBaseCommand<CategoryAccount5d>(Id);
}
