using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Services.Interfaces.Accounts5d;
using MediatR;

namespace Bookkeeping.Application.Commands.CategoryAccounts5d.SoftDeleteCategoryAccount5d
{
    public class SoftDeleteCategoryAccount5dCommandHandler
        : SoftDeleteBaseCommandHandler<CategoryAccount5d>,
        IRequestHandler<SoftDeleteCategoryAccount5dCommand, Result>
    {
        public SoftDeleteCategoryAccount5dCommandHandler(
            ICategoryAccount5dService service,
            ILogger<SoftDeleteCategoryAccount5dCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteCategoryAccount5dCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
