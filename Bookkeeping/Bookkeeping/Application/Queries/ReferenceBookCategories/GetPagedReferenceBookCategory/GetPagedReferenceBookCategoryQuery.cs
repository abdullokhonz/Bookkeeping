using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetPagedReferenceBookCategory
{
    public record GetPagedReferenceBookCategoryQuery(int Page, int Size)
        : GetPagedBaseQuery<ReferenceBookCategory, ReferenceBookCategoryGetDto>(Page, Size);
}
