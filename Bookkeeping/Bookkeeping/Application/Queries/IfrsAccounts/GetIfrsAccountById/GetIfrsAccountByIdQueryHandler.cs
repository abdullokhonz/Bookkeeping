using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetIfrsAccountById
{
    public class GetIfrsAccountByIdQueryHandler
        : GetBaseByIdQueryHandler<IfrsAccount, IfrsAccountTreeDto>,
        IRequestHandler<GetIfrsAccountByIdQuery, Result<IfrsAccountTreeDto>>
    {
        public GetIfrsAccountByIdQueryHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<GetIfrsAccountByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IfrsAccountTreeDto>> Handle(
            GetIfrsAccountByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
