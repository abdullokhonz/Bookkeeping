using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetAllIfrsAccount
{
    public class GetAllIfrsAccountQueryHandler
        : GetAllBaseQueryHandler<IfrsAccount, IfrsAccountTreeDto>,
        IRequestHandler<GetAllIfrsAccountQuery, Result<IEnumerable<IfrsAccountTreeDto>>>
    {
        public GetAllIfrsAccountQueryHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<GetAllIfrsAccountQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<IfrsAccountTreeDto>>> Handle(
            GetAllIfrsAccountQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
