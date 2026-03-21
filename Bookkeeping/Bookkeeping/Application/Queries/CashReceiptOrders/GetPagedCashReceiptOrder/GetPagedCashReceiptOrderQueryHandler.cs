using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.CashReceiptOrders.GetPagedCashReceiptOrder
{
    public class GetPagedCashReceiptOrderQueryHandler
        : GetPagedBaseQueryHandler<CashReceiptOrder, CashReceiptOrderGetDto>,
        IRequestHandler<GetPagedCashReceiptOrderQuery, Result<PagedList<CashReceiptOrderGetDto>>>
    {
        public GetPagedCashReceiptOrderQueryHandler(
            ICashReceiptOrderService service,
            IMapper mapper,
            ILogger<GetPagedCashReceiptOrderQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<CashReceiptOrderGetDto>>> Handle(
            GetPagedCashReceiptOrderQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
