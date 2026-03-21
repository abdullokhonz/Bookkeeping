using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetReferenceBookById
{
    public record GetReferenceBookByIdQuery(Guid Id)
        : GetBaseByIdQuery<ReferenceBook, ReferenceBookGetDto>(Id);
}
