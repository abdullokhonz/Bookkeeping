using Bookkeeping.Application.Commands.VatTaxes.CreateVatTax;
using Bookkeeping.Application.Commands.VatTaxes.DeleteVatTax;
using Bookkeeping.Application.Commands.VatTaxes.SoftDeleteVatTax;
using Bookkeeping.Application.Commands.VatTaxes.UpdateVatTax;
using Bookkeeping.Application.Queries.VatTaxes.GetAllVatTax;
using Bookkeeping.Application.Queries.VatTaxes.GetPagedVatTax;
using Bookkeeping.Application.Queries.VatTaxes.GetVatTaxById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.CashReceiptOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.CashReceiptOrders
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VatTaxController
        : BaseController<VatTax, VatTaxGetDto, VatTaxCreateDto, VatTaxUpdateDto>
    {
        public VatTaxController(IMediator mediator, ILogger<VatTaxController> logger) : base(mediator, logger) { }

        protected override IRequest<Result<IEnumerable<VatTaxGetDto>>> GetAllQuery()
            => new GetAllVatTaxQuery();

        protected override IRequest<Result<VatTaxGetDto>> GetByIdQuery(Guid id)
            => new GetVatTaxByIdQuery(id);

        protected override IRequest<Result<VatTaxGetDto>> CreateCommand(VatTaxCreateDto dto)
            => new CreateVatTaxCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, VatTaxUpdateDto dto)
            => new UpdateVatTaxCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteVatTaxCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteVatTaxCommand(id);

        protected override IRequest<Result<PagedList<VatTaxGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedVatTaxQuery(page, size);
    }
}
