using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Queries.Users.GetAllUser
{
    public class GetAllUserQueryHandler
        : GetAllBaseQueryHandler<User, UserResponseDto>,
        IRequestHandler<GetAllUserQuery, Result<IEnumerable<UserResponseDto>>>
    {
        public GetAllUserQueryHandler(
            IUserService service,
            IMapper mapper,
            ILogger<GetAllUserQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<UserResponseDto>>> Handle(
            GetAllUserQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
