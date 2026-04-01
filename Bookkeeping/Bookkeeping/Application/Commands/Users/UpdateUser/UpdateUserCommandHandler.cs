using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler
        : UpdateBaseCommandHandler<User, UserUpdateDto>,
        IRequestHandler<UpdateUserCommand, Result>
    {
        public UpdateUserCommandHandler(
            IUserService service,
            IMapper mapper,
            ILogger<UpdateUserCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateUserCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
