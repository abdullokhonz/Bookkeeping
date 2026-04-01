using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Users;
using Bookkeeping.Services.Interfaces.Users;
using MediatR;

namespace Bookkeeping.Application.Commands.Users.SoftDeleteUser
{
    public class SoftDeleteUserCommandHandler
        : SoftDeleteBaseCommandHandler<User>,
        IRequestHandler<SoftDeleteUserCommand, Result>
    {
        public SoftDeleteUserCommandHandler(
            IUserService service,
            ILogger<SoftDeleteUserCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteUserCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
