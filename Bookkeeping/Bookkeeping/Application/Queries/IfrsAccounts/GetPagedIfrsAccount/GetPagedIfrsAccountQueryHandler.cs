using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetPagedIfrsAccount
{
    public class GetPagedIfrsAccountQueryHandler
        : GetPagedBaseQueryHandler<IfrsAccount, IfrsAccountTreeDto>,
        IRequestHandler<GetPagedIfrsAccountQuery, Result<PagedList<IfrsAccountTreeDto>>>
    {
        public GetPagedIfrsAccountQueryHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<GetPagedIfrsAccountQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<IfrsAccountTreeDto>>> Handle(
            GetPagedIfrsAccountQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
