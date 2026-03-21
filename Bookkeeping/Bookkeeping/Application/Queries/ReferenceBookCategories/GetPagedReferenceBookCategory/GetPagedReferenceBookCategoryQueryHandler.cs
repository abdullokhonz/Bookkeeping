using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetPagedReferenceBookCategory
{
    public class GetPagedReferenceBookCategoryQueryHandler
        : GetPagedBaseQueryHandler<ReferenceBookCategory, ReferenceBookCategoryGetDto>,
        IRequestHandler<GetPagedReferenceBookCategoryQuery, Result<PagedList<ReferenceBookCategoryGetDto>>>
    {
        public GetPagedReferenceBookCategoryQueryHandler(
            IReferenceBookCategoryService service,
            IMapper mapper,
            ILogger<GetPagedReferenceBookCategoryQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<ReferenceBookCategoryGetDto>>> Handle(
            GetPagedReferenceBookCategoryQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
