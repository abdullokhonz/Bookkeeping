using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.IfrsAccounts.UpdateIfrsAccount
{
    public record UpdateIfrsAccountCommand(Guid Id, IfrsAccountUpdateDto Dto)
        : UpdateBaseCommand<IfrsAccount, IfrsAccountUpdateDto>(Id, Dto);
}
