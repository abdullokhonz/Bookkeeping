using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetAllReferenceBook
{
    public record GetAllReferenceBookQuery()
        : GetAllBaseQuery<ReferenceBook, ReferenceBookGetDto>;
}
