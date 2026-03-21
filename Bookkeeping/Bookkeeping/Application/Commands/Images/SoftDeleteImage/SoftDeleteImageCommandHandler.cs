using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.SoftDeleteImage
{
    public class SoftDeleteImageCommandHandler
        : SoftDeleteBaseCommandHandler<Image>,
        IRequestHandler<SoftDeleteImageCommand, Result>
    {
        public SoftDeleteImageCommandHandler(
            IImageService service,
            ILogger<SoftDeleteImageCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (SoftDeleteImageCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
