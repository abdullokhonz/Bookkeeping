using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBooks.CreateReferenceBook
{
    public class CreateReferenceBookCommandHandler
        : CreateBaseCommandHandler<ReferenceBook, ReferenceBookCreateDto, ReferenceBookGetDto>,
        IRequestHandler<CreateReferenceBookCommand, Result<ReferenceBookGetDto>>
    {
        public CreateReferenceBookCommandHandler(
            IReferenceBookService service,
            IMapper mapper,
            ILogger<CreateReferenceBookCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<ReferenceBookGetDto>> Handle(
            CreateReferenceBookCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
