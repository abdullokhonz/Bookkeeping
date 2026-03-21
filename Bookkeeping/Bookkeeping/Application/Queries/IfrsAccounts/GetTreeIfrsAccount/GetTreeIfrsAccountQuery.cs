using Bookkeeping.Application.Queries.Base.GetTreeBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetTreeIfrsAccount
{
    public record GetTreeIfrsAccountQuery
        : GetTreeBaseQuery<IfrsAccount, IfrsAccountTreeDto>;
}
