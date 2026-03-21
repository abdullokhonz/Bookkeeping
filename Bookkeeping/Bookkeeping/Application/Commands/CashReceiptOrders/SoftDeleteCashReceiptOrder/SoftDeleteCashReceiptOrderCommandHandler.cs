using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.SoftDeleteCashReceiptOrder
{
    public class SoftDeleteCashReceiptOrderCommandHandler
        : SoftDeleteBaseCommandHandler<CashReceiptOrder>,
        IRequestHandler<SoftDeleteCashReceiptOrderCommand, Result>
    {
        public SoftDeleteCashReceiptOrderCommandHandler(
            ICashReceiptOrderService service,
            ILogger<SoftDeleteCashReceiptOrderCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteCashReceiptOrderCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
