using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.VatTaxes.SoftDeleteVatTax
{
    public class SoftDeleteVatTaxCommandHandler
        : SoftDeleteBaseCommandHandler<VatTax>,
        IRequestHandler<SoftDeleteVatTaxCommand, Result>
    {
        public SoftDeleteVatTaxCommandHandler(
            IVatTaxService service,
            ILogger<SoftDeleteVatTaxCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteVatTaxCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
