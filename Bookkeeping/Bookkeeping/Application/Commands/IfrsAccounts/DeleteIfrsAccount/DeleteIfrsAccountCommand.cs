using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.IfrsAccounts.DeleteIfrsAccount
{
    public record DeleteIfrsAccountCommand(Guid Id)
        : DeleteBaseCommand<IfrsAccount>(Id);
}
