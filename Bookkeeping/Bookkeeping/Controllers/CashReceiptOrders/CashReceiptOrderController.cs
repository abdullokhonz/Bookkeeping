using Bookkeeping.Application.Commands.CashReceiptOrders.CreateCashReceiptOrder;
using Bookkeeping.Application.Commands.CashReceiptOrders.DeleteCashReceiptOrder;
using Bookkeeping.Application.Commands.CashReceiptOrders.SoftDeleteCashReceiptOrder;
using Bookkeeping.Application.Commands.CashReceiptOrders.UpdateCashReceiptOrder;
using Bookkeeping.Application.Queries.CashReceiptOrders.GetAllCashReceiptOrder;
using Bookkeeping.Application.Queries.CashReceiptOrders.GetCashReceiptOrderById;
using Bookkeeping.Application.Queries.CashReceiptOrders.GetPagedCashReceiptOrder;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.CashReceiptOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.CashReceiptOrders
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CashReceiptOrderController
        : BaseController<
            CashReceiptOrder,
            CashReceiptOrderGetDto,
            CashReceiptOrderCreateDto,
            CashReceiptOrderUpdateDto>
    {
        public CashReceiptOrderController(IMediator mediator, ILogger<CashReceiptOrderController> logger) : base(mediator, logger) { }

        protected override IRequest<Result<IEnumerable<CashReceiptOrderGetDto>>> GetAllQuery()
            => new GetAllCashReceiptOrderQuery();

        protected override IRequest<Result<CashReceiptOrderGetDto>> GetByIdQuery(Guid id)
            => new GetCashReceiptOrderByIdQuery(id);

        protected override IRequest<Result<CashReceiptOrderGetDto>> CreateCommand(CashReceiptOrderCreateDto dto)
            => new CreateCashReceiptOrderCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, CashReceiptOrderUpdateDto dto)
            => new UpdateCashReceiptOrderCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteCashReceiptOrderCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteCashReceiptOrderCommand(id);

        protected override IRequest<Result<PagedList<CashReceiptOrderGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedCashReceiptOrderQuery(page, size);
    }
}
