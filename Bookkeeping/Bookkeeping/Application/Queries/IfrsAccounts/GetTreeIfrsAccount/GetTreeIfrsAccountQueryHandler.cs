using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetTreeBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetTreeIfrsAccount
{
    public class GetTreeIfrsAccountQueryHandler
        : GetTreeBaseQueryHandler<IfrsAccount, IfrsAccountTreeDto>,
        IRequestHandler<GetTreeIfrsAccountQuery, Result<IEnumerable<IfrsAccountTreeDto>>>
    {
        public GetTreeIfrsAccountQueryHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<GetTreeIfrsAccountQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<IfrsAccountTreeDto>>> Handle(
            GetTreeIfrsAccountQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
