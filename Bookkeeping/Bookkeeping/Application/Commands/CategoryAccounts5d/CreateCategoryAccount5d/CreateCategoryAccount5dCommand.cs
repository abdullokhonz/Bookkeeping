using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.CreateCategoryAccount5d
{
    public record CreateCategoryAccount5dCommand(CategoryAccount5dCreateDto Dto)
        : CreateBaseCommand<CategoryAccount5d, CategoryAccount5dCreateDto, CategoryAccount5dTreeDto>(Dto);
}
