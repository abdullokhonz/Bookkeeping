using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MediatR;

namespace Bookkeeping.Application.Commands.ReferenceBooks.SoftDeleteReferenceBook
{
    public class SoftDeleteReferenceBookCommandHandler
        : SoftDeleteBaseCommandHandler<ReferenceBook>,
        IRequestHandler<SoftDeleteReferenceBookCommand, Result>
    {
        public SoftDeleteReferenceBookCommandHandler(
            IReferenceBookService service,
            ILogger<SoftDeleteReferenceBookCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteReferenceBookCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
