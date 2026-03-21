using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetPagedCategoryAccount5d
{
    public class GetPagedCategoryAccount5dQueryHandler
        : GetPagedBaseQueryHandler<CategoryAccount5d, CategoryAccount5dTreeDto>,
        IRequestHandler<GetPagedCategoryAccount5dQuery, Result<PagedList<CategoryAccount5dTreeDto>>>
    {
        public GetPagedCategoryAccount5dQueryHandler(
            ICategoryAccount5dService service,
            IMapper mapper,
            ILogger<GetPagedCategoryAccount5dQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<CategoryAccount5dTreeDto>>> Handle(
            GetPagedCategoryAccount5dQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
