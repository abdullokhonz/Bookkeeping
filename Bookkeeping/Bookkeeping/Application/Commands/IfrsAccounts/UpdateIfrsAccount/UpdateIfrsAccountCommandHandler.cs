using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.IfrsAccounts.UpdateIfrsAccount
{
    public class UpdateIfrsAccountCommandHandler
        : UpdateBaseCommandHandler<IfrsAccount, IfrsAccountUpdateDto>,
        IRequestHandler<UpdateIfrsAccountCommand, Result>
    {
        public UpdateIfrsAccountCommandHandler(
            IIfrsAccountService service,
            IMapper mapper,
            ILogger<UpdateIfrsAccountCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateIfrsAccountCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
