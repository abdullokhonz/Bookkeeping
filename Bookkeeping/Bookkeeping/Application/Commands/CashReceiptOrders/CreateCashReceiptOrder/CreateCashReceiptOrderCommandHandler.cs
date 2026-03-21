using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.CreateCashReceiptOrder
{
    public class CreateCashReceiptOrderCommandHandler
        : CreateBaseCommandHandler<CashReceiptOrder, CashReceiptOrderCreateDto, CashReceiptOrderGetDto>,
        IRequestHandler<CreateCashReceiptOrderCommand, Result<CashReceiptOrderGetDto>>
    {
        public CreateCashReceiptOrderCommandHandler(
            ICashReceiptOrderService service,
            IMapper mapper,
            ILogger<CreateCashReceiptOrderCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<CashReceiptOrderGetDto>> Handle(
            CreateCashReceiptOrderCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
