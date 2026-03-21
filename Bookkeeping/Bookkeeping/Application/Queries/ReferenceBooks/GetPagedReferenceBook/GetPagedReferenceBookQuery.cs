using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetPagedReferenceBook
{
    public record GetPagedReferenceBookQuery(int Page, int Size)
        : GetPagedBaseQuery<ReferenceBook, ReferenceBookGetDto>(Page, Size);
}
