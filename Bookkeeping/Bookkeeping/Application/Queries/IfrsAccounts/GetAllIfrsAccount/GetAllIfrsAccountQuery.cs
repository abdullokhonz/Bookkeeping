using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetAllIfrsAccount
{
    public record GetAllIfrsAccountQuery()
        : GetAllBaseQuery<IfrsAccount, IfrsAccountTreeDto>;
}
