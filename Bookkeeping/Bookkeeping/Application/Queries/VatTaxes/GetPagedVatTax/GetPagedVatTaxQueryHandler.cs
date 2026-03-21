using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.VatTaxes.GetPagedVatTax
{
    public class GetPagedVatTaxQueryHandler
        : GetPagedBaseQueryHandler<VatTax, VatTaxGetDto>,
        IRequestHandler<GetPagedVatTaxQuery, Result<PagedList<VatTaxGetDto>>>
    {
        public GetPagedVatTaxQueryHandler(
            IVatTaxService service,
            IMapper mapper,
            ILogger<GetPagedVatTaxQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<VatTaxGetDto>>> Handle(
            GetPagedVatTaxQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
