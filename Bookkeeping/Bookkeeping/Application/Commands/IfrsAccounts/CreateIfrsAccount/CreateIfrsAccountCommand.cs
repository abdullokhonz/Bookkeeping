using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.IfrsAccounts.CreateIfrsAccount
{
    public record CreateIfrsAccountCommand(IfrsAccountCreateDto Dto)
        : CreateBaseCommand<IfrsAccount, IfrsAccountCreateDto, IfrsAccountTreeDto>(Dto);
}
