using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.VatTaxes.DeleteVatTax
{
    public class DeleteVatTaxCommandHandler
        : DeleteBaseCommandHandler<VatTax>,
        IRequestHandler<DeleteVatTaxCommand, Result>
    {
        public DeleteVatTaxCommandHandler(
            IVatTaxService service,
            ILogger<DeleteVatTaxCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteVatTaxCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
