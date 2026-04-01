using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Queries.Users.GetUserById
{
    public class GetUserByIdQueryHandler
        : GetBaseByIdQueryHandler<User, UserResponseDto>,
        IRequestHandler<GetUserByIdQuery, Result<UserResponseDto>>
    {
        public GetUserByIdQueryHandler(
            IUserService service,
            IMapper mapper,
            ILogger<GetUserByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<UserResponseDto>> Handle(
            GetUserByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
