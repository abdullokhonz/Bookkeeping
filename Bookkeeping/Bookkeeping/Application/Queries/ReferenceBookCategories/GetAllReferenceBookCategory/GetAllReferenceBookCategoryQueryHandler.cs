using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetAllReferenceBookCategory
{
    public class GetAllReferenceBookCategoryQueryHandler
        : GetAllBaseQueryHandler<ReferenceBookCategory, ReferenceBookCategoryGetDto>,
        IRequestHandler<GetAllReferenceBookCategoryQuery, Result<IEnumerable<ReferenceBookCategoryGetDto>>>
    {
        public GetAllReferenceBookCategoryQueryHandler(
            IReferenceBookCategoryService service,
            IMapper mapper,
            ILogger<GetAllReferenceBookCategoryQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<ReferenceBookCategoryGetDto>>> Handle(
            GetAllReferenceBookCategoryQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
