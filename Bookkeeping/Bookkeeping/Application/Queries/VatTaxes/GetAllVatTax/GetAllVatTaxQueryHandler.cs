using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.VatTaxes.GetAllVatTax
{
    public class GetAllVatTaxQueryHandler
        : GetAllBaseQueryHandler<VatTax, VatTaxGetDto>,
        IRequestHandler<GetAllVatTaxQuery, Result<IEnumerable<VatTaxGetDto>>>
    {
        public GetAllVatTaxQueryHandler(
            IVatTaxService service,
            IMapper mapper,
            ILogger<GetAllVatTaxQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<VatTaxGetDto>>> Handle(
            GetAllVatTaxQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
