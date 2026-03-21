using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBooks.SoftDeleteReferenceBook
{
    public record SoftDeleteReferenceBookCommand(Guid Id)
        : SoftDeleteBaseCommand<ReferenceBook>(Id);
}
