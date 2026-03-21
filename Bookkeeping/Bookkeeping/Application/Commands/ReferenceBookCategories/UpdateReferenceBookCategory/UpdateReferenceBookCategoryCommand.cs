using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.UpdateReferenceBookCategory
{
    public record UpdateReferenceBookCategoryCommand(Guid Id, ReferenceBookCategoryUpdateDto Dto)
        : UpdateBaseCommand<ReferenceBookCategory, ReferenceBookCategoryUpdateDto>(Id, Dto);
}
