using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetAllReferenceBookCategory
{
    public record GetAllReferenceBookCategoryQuery()
        : GetAllBaseQuery<ReferenceBookCategory, ReferenceBookCategoryGetDto>;
}
