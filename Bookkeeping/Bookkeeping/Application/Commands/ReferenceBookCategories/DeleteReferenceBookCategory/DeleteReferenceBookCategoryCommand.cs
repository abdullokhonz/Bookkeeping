using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.DeleteReferenceBookCategory
{
    public record DeleteReferenceBookCategoryCommand(Guid Id)
        : DeleteBaseCommand<ReferenceBookCategory>(Id);
}
