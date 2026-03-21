using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.VatTaxes.CreateVatTax
{
    public class CreateVatTaxCommandHandler
        : CreateBaseCommandHandler<VatTax, VatTaxCreateDto, VatTaxGetDto>,
        IRequestHandler<CreateVatTaxCommand, Result<VatTaxGetDto>>
    {
        public CreateVatTaxCommandHandler(
            IVatTaxService service,
            IMapper mapper,
            ILogger<CreateVatTaxCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<VatTaxGetDto>> Handle(
            CreateVatTaxCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
