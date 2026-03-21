using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetReferenceBookById
{
    public class GetReferenceBookByIdQueryHandler
        : GetBaseByIdQueryHandler<ReferenceBook, ReferenceBookGetDto>,
        IRequestHandler<GetReferenceBookByIdQuery, Result<ReferenceBookGetDto>>
    {
        public GetReferenceBookByIdQueryHandler(
            IReferenceBookService service,
            IMapper mapper,
            ILogger<GetReferenceBookByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<ReferenceBookGetDto>> Handle(
            GetReferenceBookByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
