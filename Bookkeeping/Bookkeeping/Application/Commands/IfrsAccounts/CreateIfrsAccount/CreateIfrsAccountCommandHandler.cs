using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.IfrsAccounts.CreateIfrsAccount
{
    public class CreateIfrsAccountCommandHandler
        : CreateBaseCommandHandler<IfrsAccount, IfrsAccountCreateDto, IfrsAccountTreeDto>,
        IRequestHandler<CreateIfrsAccountCommand, Result<IfrsAccountTreeDto>>
    {
        public CreateIfrsAccountCommandHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<CreateIfrsAccountCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IfrsAccountTreeDto>> Handle(
            CreateIfrsAccountCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
