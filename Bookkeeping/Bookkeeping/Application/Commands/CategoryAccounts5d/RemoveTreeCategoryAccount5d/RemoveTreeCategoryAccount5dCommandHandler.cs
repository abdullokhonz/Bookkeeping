using Bookkeeping.Application.Commands.Base.RemoveTreeBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.RemoveTreeCategoryAccount5d
{
    public class RemoveTreeCategoryAccount5dCommandHandler
        : RemoveTreeBaseCommandHandler<CategoryAccount5d>,
        IRequestHandler<RemoveTreeCategoryAccount5dCommand, Result>
    {
        public RemoveTreeCategoryAccount5dCommandHandler(
            ICategoryAccount5dService service,
            ILogger<RemoveTreeCategoryAccount5dCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            RemoveTreeCategoryAccount5dCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
