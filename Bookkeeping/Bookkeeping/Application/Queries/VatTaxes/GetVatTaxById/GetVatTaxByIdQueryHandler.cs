using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.VatTaxes.GetVatTaxById
{
    public class GetVatTaxByIdQueryHandler
        : GetBaseByIdQueryHandler<VatTax, VatTaxGetDto>,
        IRequestHandler<GetVatTaxByIdQuery, Result<VatTaxGetDto>>
    {
        public GetVatTaxByIdQueryHandler(
            IVatTaxService service,
            IMapper mapper,
            ILogger<GetVatTaxByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<VatTaxGetDto>> Handle(
            GetVatTaxByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
