using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Commands.Users.UpdateUser
{
    public record UpdateUserCommand(Guid Id, UserUpdateDto Dto)
        : UpdateBaseCommand<User, UserUpdateDto>(Id, Dto);
}
