using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetCashReceiptOrderById
{
    public class GetCashReceiptOrderByIdQueryHandler
        : GetBaseByIdQueryHandler<CashReceiptOrder, CashReceiptOrderGetDto>,
        IRequestHandler<GetCashReceiptOrderByIdQuery, Result<CashReceiptOrderGetDto>>
    {
        public GetCashReceiptOrderByIdQueryHandler(
            ICashReceiptOrderService service,
            IMapper mapper,
            ILogger<GetCashReceiptOrderByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<CashReceiptOrderGetDto>> Handle(
            GetCashReceiptOrderByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
