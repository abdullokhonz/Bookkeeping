using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler
        : DeleteBaseCommandHandler<User>,
        IRequestHandler<DeleteUserCommand, Result>
    {
        public DeleteUserCommandHandler(
            IUserService service,
            ILogger<DeleteUserCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            DeleteUserCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
