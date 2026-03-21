using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.DeleteImage
{
    public class DeleteImageCommandHandler
        : DeleteBaseCommandHandler<Image>,
        IRequestHandler<DeleteImageCommand, Result>
    {
        public DeleteImageCommandHandler(
            IImageService service,
            ILogger<DeleteImageCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteImageCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
