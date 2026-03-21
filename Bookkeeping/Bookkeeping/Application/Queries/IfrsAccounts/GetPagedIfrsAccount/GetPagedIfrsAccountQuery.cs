using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.IfrsAccounts.GetPagedIfrsAccount
{
    public record GetPagedIfrsAccountQuery(int Page, int Size)
        : GetPagedBaseQuery<IfrsAccount, IfrsAccountTreeDto>(Page, Size);
}
