using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Queries.CategoryAccounts5d.GetCategoryAccount5dById
{
    public record GetCategoryAccount5dByIdQuery(Guid Id)
        : GetBaseByIdQuery<CategoryAccount5d, CategoryAccount5dTreeDto>(Id);
}
