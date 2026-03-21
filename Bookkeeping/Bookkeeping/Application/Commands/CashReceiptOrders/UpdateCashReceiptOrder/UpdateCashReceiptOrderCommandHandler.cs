using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.UpdateCashReceiptOrder
{
    public class UpdateCashReceiptOrderCommandHandler
        : UpdateBaseCommandHandler<CashReceiptOrder, CashReceiptOrderUpdateDto>,
        IRequestHandler<UpdateCashReceiptOrderCommand, Result>
    {
        public UpdateCashReceiptOrderCommandHandler(
            ICashReceiptOrderService service,
            IMapper mapper,
            ILogger<UpdateCashReceiptOrderCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateCashReceiptOrderCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
