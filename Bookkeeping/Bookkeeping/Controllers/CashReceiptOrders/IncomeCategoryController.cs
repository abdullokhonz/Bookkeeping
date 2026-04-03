using Bookkeeping.Application.Commands.IncomeCategories.CreateIncomeCategory;
using Bookkeeping.Application.Commands.IncomeCategories.DeleteIncomeCategory;
using Bookkeeping.Application.Commands.IncomeCategories.SoftDeleteIncomeCategory;
using Bookkeeping.Application.Commands.IncomeCategories.UpdateIncomeCategory;
using Bookkeeping.Application.Queries.IncomeCategories.GetAllIncomeCategory;
using Bookkeeping.Application.Queries.IncomeCategories.GetIncomeCategoryById;
using Bookkeeping.Application.Queries.IncomeCategories.GetPagedIncomeCategory;
using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
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
    public class IncomeCategoryController
        : BaseController<
            IncomeCategory,
            IncomeCategoryGetDto,
            IncomeCategoryCreateDto,
            IncomeCategoryUpdateDto>
    {
        public IncomeCategoryController(IMediator mediator, ILogger<IncomeCategoryController> logger) : base(mediator, logger) { }

        protected override IRequest<Result<IEnumerable<IncomeCategoryGetDto>>> GetAllQuery()
            => new GetAllIncomeCategoryQuery();

        protected override IRequest<Result<IncomeCategoryGetDto>> GetByIdQuery(Guid id)
            => new GetIncomeCategoryByIdQuery(id);

        protected override IRequest<Result<IncomeCategoryGetDto>> CreateCommand(IncomeCategoryCreateDto dto)
            => new CreateIncomeCategoryCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, IncomeCategoryUpdateDto dto)
            => new UpdateIncomeCategoryCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteIncomeCategoryCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteIncomeCategoryCommand(id);

        protected override IRequest<Result<PagedList<IncomeCategoryGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedIncomeCategoryQuery(page, size);
    }
}
