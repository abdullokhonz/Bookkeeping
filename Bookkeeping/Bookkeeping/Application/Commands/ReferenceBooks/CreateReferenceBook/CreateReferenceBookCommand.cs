using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Application.Commands.ReferenceBooks.CreateReferenceBook
{
    public record CreateReferenceBookCommand(ReferenceBookCreateDto Dto)
        : CreateBaseCommand<ReferenceBook, ReferenceBookCreateDto, ReferenceBookGetDto>(Dto);
}
