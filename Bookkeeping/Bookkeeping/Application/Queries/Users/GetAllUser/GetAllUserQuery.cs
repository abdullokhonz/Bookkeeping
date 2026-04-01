using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Queries.Users.GetAllUser
{
    public record GetAllUserQuery
        : GetAllBaseQuery<User, UserResponseDto>;
}
