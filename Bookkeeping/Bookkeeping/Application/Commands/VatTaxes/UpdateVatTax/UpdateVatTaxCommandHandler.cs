using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.VatTaxes.UpdateVatTax
{
    public class UpdateVatTaxCommandHandler
        : UpdateBaseCommandHandler<VatTax, VatTaxUpdateDto>,
        IRequestHandler<UpdateVatTaxCommand, Result>
    {
        public UpdateVatTaxCommandHandler(
            IVatTaxService service,
            IMapper mapper,
            ILogger<UpdateVatTaxCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateVatTaxCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
