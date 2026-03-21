using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Application.Commands.ReferenceBookCategories.DeleteReferenceBookCategory;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBookCategories.DeleteReferenceBookCategoryCategory
{
    public class DeleteReferenceBookCategoryCommandHandler
        : DeleteBaseCommandHandler<ReferenceBookCategory>,
        IRequestHandler<DeleteReferenceBookCategoryCommand, Result>
    {
        public DeleteReferenceBookCategoryCommandHandler(
            IReferenceBookCategoryService service,
            ILogger<DeleteReferenceBookCategoryCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteReferenceBookCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
