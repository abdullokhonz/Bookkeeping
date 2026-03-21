using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.DeleteCashReceiptOrder
{
    public class DeleteCashReceiptOrderCommandHandler
        : DeleteBaseCommandHandler<CashReceiptOrder>,
        IRequestHandler<DeleteCashReceiptOrderCommand, Result>
    {
        public DeleteCashReceiptOrderCommandHandler(
            ICashReceiptOrderService service,
            ILogger<DeleteCashReceiptOrderCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            DeleteCashReceiptOrderCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
