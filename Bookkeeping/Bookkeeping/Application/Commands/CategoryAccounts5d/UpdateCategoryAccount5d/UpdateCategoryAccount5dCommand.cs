using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.UpdateCategoryAccount5d
{
    public record UpdateCategoryAccount5dCommand(Guid Id, CategoryAccount5dUpdateDto Dto)
        : UpdateBaseCommand<CategoryAccount5d, CategoryAccount5dUpdateDto>(Id, Dto);
}
