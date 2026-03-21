using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetTreeBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetTreeCategoryAccount5d
{
    public class GetTreeCategoryAccount5dQueryHandler
        : GetTreeBaseQueryHandler<CategoryAccount5d, CategoryAccount5dTreeDto>,
        IRequestHandler<GetTreeCategoryAccount5dQuery, Result<IEnumerable<CategoryAccount5dTreeDto>>>
    {
        public GetTreeCategoryAccount5dQueryHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<GetTreeCategoryAccount5dQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<CategoryAccount5dTreeDto>>> Handle(
            GetTreeCategoryAccount5dQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
