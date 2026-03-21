using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBooks.DeleteReferenceBook
{
    public class DeleteReferenceBookCommandHandler
        : DeleteBaseCommandHandler<ReferenceBook>,
        IRequestHandler<DeleteReferenceBookCommand, Result>
    {
        public DeleteReferenceBookCommandHandler(
            IReferenceBookService service,
            ILogger<DeleteReferenceBookCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteReferenceBookCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
