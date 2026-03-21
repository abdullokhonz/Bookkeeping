using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBooks.UpdateReferenceBook
{
    public class UpdateReferenceBookCommandHandler
        : UpdateBaseCommandHandler<ReferenceBook, ReferenceBookUpdateDto>,
        IRequestHandler<UpdateReferenceBookCommand, Result>
    {
        public UpdateReferenceBookCommandHandler(
            IReferenceBookService service,
            IMapper mapper,
            ILogger<UpdateReferenceBookCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateReferenceBookCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
