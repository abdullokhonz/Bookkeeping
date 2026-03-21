using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.SoftDeleteCategoryAccount5d
{
    public record SoftDeleteCategoryAccount5dCommand(Guid Id)
        : SoftDeleteBaseCommand<CategoryAccount5d>(Id);
}
