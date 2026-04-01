using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Commands.Users.DeleteUser
{
    public record DeleteUserCommand(Guid Id)
        : DeleteBaseCommand<User>(Id);
}
