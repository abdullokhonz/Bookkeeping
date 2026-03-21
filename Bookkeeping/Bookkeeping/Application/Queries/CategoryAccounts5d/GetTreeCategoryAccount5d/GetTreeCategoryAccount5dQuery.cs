using Bookkeeping.Application.Queries.Base.GetTreeBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetTreeCategoryAccount5d
{
    public record GetTreeCategoryAccount5dQuery
        : GetTreeBaseQuery<CategoryAccount5d, CategoryAccount5dTreeDto>;
}
