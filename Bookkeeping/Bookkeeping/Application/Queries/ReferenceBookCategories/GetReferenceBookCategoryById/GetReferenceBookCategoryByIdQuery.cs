using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetReferenceBookCategoryById
{
    public record GetReferenceBookCategoryByIdQuery(Guid Id)
        : GetBaseByIdQuery<ReferenceBookCategory, ReferenceBookCategoryGetDto>(Id);
}
