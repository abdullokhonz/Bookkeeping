using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetAllCategoryAccount5d
{
    public record GetAllCategoryAccount5dQuery()
        : GetAllBaseQuery<CategoryAccount5d, CategoryAccount5dTreeDto>;
}
