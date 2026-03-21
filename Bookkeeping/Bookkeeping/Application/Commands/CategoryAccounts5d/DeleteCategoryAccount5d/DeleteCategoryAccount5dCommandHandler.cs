using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.DeleteCategoryAccount5d
{
    public class DeleteIfrsAccountCommandHandler
        : DeleteBaseCommandHandler<CategoryAccount5d>,
        IRequestHandler<DeleteIfrsAccountCommand, Result>
    {
        public DeleteIfrsAccountCommandHandler(
            ICategoryAccount5dService service,
            ILogger<DeleteIfrsAccountCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteIfrsAccountCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
