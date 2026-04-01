using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;

namespace Bookkeeping.Application.Queries.Users.GetPagedUser
{
    public record GetPagedUserQuery(int Page, int Size)
        : GetPagedBaseQuery<User, UserResponseDto>(Page, Size);
}
