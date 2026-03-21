using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.SoftDeleteReferenceBookCategory
{
    public class SoftDeleteReferenceBookCategoryCommandHandler
        : SoftDeleteBaseCommandHandler<ReferenceBookCategory>,
        IRequestHandler<SoftDeleteReferenceBookCategoryCommand, Result>
    {
        public SoftDeleteReferenceBookCategoryCommandHandler(
            IReferenceBookCategoryService service,
            ILogger<SoftDeleteReferenceBookCategoryCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteReferenceBookCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
