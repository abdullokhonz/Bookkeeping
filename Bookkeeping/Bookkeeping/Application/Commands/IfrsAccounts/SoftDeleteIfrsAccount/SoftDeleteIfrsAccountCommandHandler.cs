using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.IfrsAccounts.SoftDeleteIfrsAccount
{
    public class SoftDeleteIfrsAccountCommandHandler
        : SoftDeleteBaseCommandHandler<IfrsAccount>,
        IRequestHandler<SoftDeleteIfrsAccountCommand, Result>
    {
        public SoftDeleteIfrsAccountCommandHandler(
            IIfrsAccountService service,
            ILogger<SoftDeleteIfrsAccountCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteIfrsAccountCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
