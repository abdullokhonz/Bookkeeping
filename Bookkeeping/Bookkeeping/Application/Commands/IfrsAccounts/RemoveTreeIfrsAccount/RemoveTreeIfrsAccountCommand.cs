using Bookkeeping.Application.Commands.Base.RemoveTreeBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.IfrsAccounts.RemoveTreeIfrsAccount
{
    public record RemoveTreeIfrsAccountCommand(Guid Id)
        : RemoveTreeBaseCommand<IfrsAccount>(Id);
}
