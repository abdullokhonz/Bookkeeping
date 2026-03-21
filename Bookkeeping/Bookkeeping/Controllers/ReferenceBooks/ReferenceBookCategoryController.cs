using Bookkeeping.Application.Commands.ReferenceBookCategories.CreateReferenceBookCategory;
using Bookkeeping.Application.Commands.ReferenceBookCategories.DeleteReferenceBookCategory;
using Bookkeeping.Application.Commands.ReferenceBookCategories.SoftDeleteReferenceBookCategory;
using Bookkeeping.Application.Commands.ReferenceBookCategories.UpdateReferenceBookCategory;
using Bookkeeping.Application.Queries.ReferenceBookCategories.GetAllReferenceBookCategory;
using Bookkeeping.Application.Queries.ReferenceBookCategories.GetPagedReferenceBookCategory;
using Bookkeeping.Application.Queries.ReferenceBookCategories.GetReferenceBookCategoryById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.ReferenceBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.ReferenceBooks
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReferenceBookCategoryController : BaseController
            <ReferenceBookCategory,
            ReferenceBookCategoryGetDto,
            ReferenceBookCategoryCreateDto,
            ReferenceBookCategoryUpdateDto>
    {
        public ReferenceBookCategoryController(
            IMediator mediator,
            ILogger<ReferenceBookCategoryController> logger)
            : base(mediator, logger)
        {

        }

        protected override IRequest<Result<IEnumerable<ReferenceBookCategoryGetDto>>> GetAllQuery()
            => new GetAllReferenceBookCategoryQuery();

        protected override IRequest<Result<ReferenceBookCategoryGetDto>> GetByIdQuery(Guid id)
            => new GetReferenceBookCategoryByIdQuery(id);

        protected override IRequest<Result<ReferenceBookCategoryGetDto>> CreateCommand(ReferenceBookCategoryCreateDto dto)
            => new CreateReferenceBookCategoryCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, ReferenceBookCategoryUpdateDto dto)
            => new UpdateReferenceBookCategoryCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteReferenceBookCategoryCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteReferenceBookCategoryCommand(id);

        protected override IRequest<Result<PagedList<ReferenceBookCategoryGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedReferenceBookCategoryQuery(page, size);
    }
}
