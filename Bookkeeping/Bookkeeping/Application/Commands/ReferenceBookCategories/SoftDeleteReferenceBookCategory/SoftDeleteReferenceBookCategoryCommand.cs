using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.SoftDeleteReferenceBookCategory
{
    public record SoftDeleteReferenceBookCategoryCommand(Guid Id)
        : SoftDeleteBaseCommand<ReferenceBookCategory>(Id);
}
