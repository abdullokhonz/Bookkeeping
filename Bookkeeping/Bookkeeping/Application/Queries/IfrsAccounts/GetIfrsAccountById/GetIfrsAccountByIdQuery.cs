using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetIfrsAccountById
{
    public record GetIfrsAccountByIdQuery(Guid Id)
        : GetBaseByIdQuery<IfrsAccount, IfrsAccountTreeDto>(Id);
}
