using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Commands.Users.CreateUser
{
    public record CreateUserCommand(RegisterUserDto Dto)
        : CreateBaseCommand<User, RegisterUserDto, UserResponseDto>(Dto);
}
