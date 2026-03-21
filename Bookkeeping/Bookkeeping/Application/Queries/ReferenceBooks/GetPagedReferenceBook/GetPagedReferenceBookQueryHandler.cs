using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBooks.GetPagedReferenceBook
{
    public class GetPagedReferenceBookQueryHandler
        : GetPagedBaseQueryHandler<ReferenceBook, ReferenceBookGetDto>,
        IRequestHandler<GetPagedReferenceBookQuery, Result<PagedList<ReferenceBookGetDto>>>
    {
        public GetPagedReferenceBookQueryHandler(
            IReferenceBookService service,
            IMapper mapper,
            ILogger<GetPagedReferenceBookQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<ReferenceBookGetDto>>> Handle(
            GetPagedReferenceBookQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
