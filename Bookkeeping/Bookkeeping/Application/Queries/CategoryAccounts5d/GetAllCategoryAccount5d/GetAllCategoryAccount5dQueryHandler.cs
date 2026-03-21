using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetAllCategoryAccount5d
{
    public class GetAllCategoryAccount5dQueryHandler
        : GetAllBaseQueryHandler<CategoryAccount5d, CategoryAccount5dTreeDto>,
        IRequestHandler<GetAllCategoryAccount5dQuery, Result<IEnumerable<CategoryAccount5dTreeDto>>>
    {
        public GetAllCategoryAccount5dQueryHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<GetAllCategoryAccount5dQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<CategoryAccount5dTreeDto>>> Handle(
            GetAllCategoryAccount5dQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
