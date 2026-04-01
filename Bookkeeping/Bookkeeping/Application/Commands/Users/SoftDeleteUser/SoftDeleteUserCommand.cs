using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Commands.Users.SoftDeleteUser
{
    public record SoftDeleteUserCommand(Guid Id)
        : SoftDeleteBaseCommand<User>(Id);
}
