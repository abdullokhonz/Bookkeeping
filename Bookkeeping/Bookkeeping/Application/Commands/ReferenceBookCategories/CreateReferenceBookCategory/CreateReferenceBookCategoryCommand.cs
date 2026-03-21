using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.CreateReferenceBookCategory
{
    public record CreateReferenceBookCategoryCommand(ReferenceBookCategoryCreateDto Dto)
        : CreateBaseCommand<ReferenceBookCategory, ReferenceBookCategoryCreateDto, ReferenceBookCategoryGetDto>(Dto);
}
