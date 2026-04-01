using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Queries.Users.GetUserById
{
    public record GetUserByIdQuery(Guid Id)
        : GetBaseByIdQuery<User, UserResponseDto>(Id);
}
