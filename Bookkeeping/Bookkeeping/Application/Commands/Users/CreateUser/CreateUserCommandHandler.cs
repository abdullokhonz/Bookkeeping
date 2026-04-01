using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler
        : CreateBaseCommandHandler<User, RegisterUserDto, UserResponseDto>,
        IRequestHandler<CreateUserCommand, Result<UserResponseDto>>
    {
        public CreateUserCommandHandler(
            IUserService service,
            IMapper mapper,
            ILogger<CreateUserCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<UserResponseDto>> Handle(
            CreateUserCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
