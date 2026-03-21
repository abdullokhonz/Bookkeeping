using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.IfrsAccounts.SoftDeleteIfrsAccount
{
    public record SoftDeleteIfrsAccountCommand(Guid Id)
        : SoftDeleteBaseCommand<IfrsAccount>(Id);
}
