using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBooks.DeleteReferenceBook
{
    public record DeleteReferenceBookCommand(Guid Id)
        : DeleteBaseCommand<ReferenceBook>(Id);
}
