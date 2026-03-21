using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Queries.ReferenceBookCategories.GetReferenceBookCategoryById
{
    public class GetReferenceBookCategoryByIdQueryHandler
        : GetBaseByIdQueryHandler<ReferenceBookCategory, ReferenceBookCategoryGetDto>,
        IRequestHandler<GetReferenceBookCategoryByIdQuery, Result<ReferenceBookCategoryGetDto>>
    {
        public GetReferenceBookCategoryByIdQueryHandler(
            IReferenceBookCategoryService service,
            IMapper mapper,
            ILogger<GetReferenceBookCategoryByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<ReferenceBookCategoryGetDto>> Handle(
            GetReferenceBookCategoryByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
