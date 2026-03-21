using Bookkeeping.Application.Commands.Base.RemoveTreeBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.IfrsAccounts.RemoveTreeIfrsAccount
{
    public class RemoveTreeIfrsAccountCommandHandler
        : RemoveTreeBaseCommandHandler<IfrsAccount>,
        IRequestHandler<RemoveTreeIfrsAccountCommand, Result>
    {
        public RemoveTreeIfrsAccountCommandHandler(
            IIfrsAccountService service,
            ILogger<RemoveTreeIfrsAccountCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            RemoveTreeIfrsAccountCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
