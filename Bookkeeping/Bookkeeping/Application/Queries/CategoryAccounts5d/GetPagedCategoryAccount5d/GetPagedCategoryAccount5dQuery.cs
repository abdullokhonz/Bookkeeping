using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetPagedCategoryAccount5d
{
    public record GetPagedCategoryAccount5dQuery(int Page, int Size)
        : GetPagedBaseQuery<CategoryAccount5d, CategoryAccount5dTreeDto>(Page, Size);
}
