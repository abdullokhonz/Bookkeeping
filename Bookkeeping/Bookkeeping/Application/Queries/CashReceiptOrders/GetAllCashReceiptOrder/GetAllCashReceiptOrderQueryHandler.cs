using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetAllCashReceiptOrder
{
    public class GetAllCashReceiptOrderQueryHandler
        : GetAllBaseQueryHandler<CashReceiptOrder, CashReceiptOrderGetDto>,
        IRequestHandler<GetAllCashReceiptOrderQuery, Result<IEnumerable<CashReceiptOrderGetDto>>>
    {
        public GetAllCashReceiptOrderQueryHandler(
            ICashReceiptOrderService service,
            IMapper mapper,
            ILogger<GetAllCashReceiptOrderQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<CashReceiptOrderGetDto>>> Handle(
            GetAllCashReceiptOrderQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
