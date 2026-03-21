using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBooks.UpdateReferenceBook
{
    public record UpdateReferenceBookCommand(Guid Id, ReferenceBookUpdateDto Dto)
        : UpdateBaseCommand<ReferenceBook, ReferenceBookUpdateDto>(Id, Dto);
}
