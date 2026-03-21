using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetAllReferenceBook
{
    public class GetAllReferenceBookQueryHandler
        : GetAllBaseQueryHandler<ReferenceBook, ReferenceBookGetDto>,
        IRequestHandler<GetAllReferenceBookQuery, Result<IEnumerable<ReferenceBookGetDto>>>
    {
        public GetAllReferenceBookQueryHandler(
            IReferenceBookService service,
            IMapper mapper,
            ILogger<GetAllReferenceBookQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<ReferenceBookGetDto>>> Handle(
            GetAllReferenceBookQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
