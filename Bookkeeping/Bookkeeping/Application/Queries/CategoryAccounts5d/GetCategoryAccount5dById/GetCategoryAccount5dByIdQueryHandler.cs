using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetCategoryAccount5dById
{
    public class GetCategoryAccount5dByIdQueryHandler
        : GetBaseByIdQueryHandler<CategoryAccount5d, CategoryAccount5dTreeDto>,
        IRequestHandler<GetCategoryAccount5dByIdQuery, Result<CategoryAccount5dTreeDto>>
    {
        public GetCategoryAccount5dByIdQueryHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<GetCategoryAccount5dByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<CategoryAccount5dTreeDto>> Handle(
            GetCategoryAccount5dByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
