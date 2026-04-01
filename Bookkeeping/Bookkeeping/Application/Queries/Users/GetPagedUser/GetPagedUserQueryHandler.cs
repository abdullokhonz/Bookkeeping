using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Queries.Users.GetPagedUser
{
    public class GetPagedUserQueryHandler
        : GetPagedBaseQueryHandler<User, UserResponseDto>,
        IRequestHandler<GetPagedUserQuery, Result<PagedList<UserResponseDto>>>
    {
        public GetPagedUserQueryHandler(
            IUserService service,
            IMapper mapper,
            ILogger<GetPagedUserQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<UserResponseDto>>> Handle(
            GetPagedUserQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
