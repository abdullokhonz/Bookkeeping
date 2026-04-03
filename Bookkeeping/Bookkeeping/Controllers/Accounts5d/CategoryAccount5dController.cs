using Bookkeeping.Application.Commands.CategoryAccounts5d.CreateCategoryAccount5d;
using Bookkeeping.Application.Commands.CategoryAccounts5d.DeleteCategoryAccount5d;
using Bookkeeping.Application.Commands.CategoryAccounts5d.RemoveTreeCategoryAccount5d;
using Bookkeeping.Application.Commands.CategoryAccounts5d.SoftDeleteCategoryAccount5d;
using Bookkeeping.Application.Commands.CategoryAccounts5d.UpdateCategoryAccount5d;
using Bookkeeping.Application.Queries.CategoryAccounts5d.GetAllCategoryAccount5d;
using Bookkeeping.Application.Queries.CategoryAccounts5d.GetCategoryAccount5dById;
using Bookkeeping.Application.Queries.CategoryAccounts5d.GetPagedCategoryAccount5d;
using Bookkeeping.Application.Queries.CategoryAccounts5d.GetTreeCategoryAccount5d;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Controllers.Base;
using Bookkeeping.Entities.Accounts5d;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.Accounts5d
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryAccount5dController : TreeBaseController
            <CategoryAccount5d,
            CategoryAccount5dTreeDto,
            CategoryAccount5dCreateDto,
            CategoryAccount5dUpdateDto>
    {
        public CategoryAccount5dController(
            IMediator mediator,
            ILogger<CategoryAccount5dController> logger)
            : base(mediator, logger)
        {

        }

        protected override IRequest<Result<IEnumerable<CategoryAccount5dTreeDto>>> GetAllQuery()
            => new GetAllCategoryAccount5dQuery();

        protected override IRequest<Result<CategoryAccount5dTreeDto>> GetByIdQuery(Guid id)
            => new GetCategoryAccount5dByIdQuery(id);

        protected override IRequest<Result<CategoryAccount5dTreeDto>> CreateCommand(CategoryAccount5dCreateDto dto)
            => new CreateCategoryAccount5dCommand(dto);

        protected override IRequest<Result> UpdateCommand(Guid id, CategoryAccount5dUpdateDto dto)
            => new UpdateCategoryAccount5dCommand(id, dto);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteCategoryAccount5dCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteIfrsAccountCommand(id);

        protected override IRequest<Result<PagedList<CategoryAccount5dTreeDto>>> GetPagedQuery(int page, int size)
            => new GetPagedCategoryAccount5dQuery(page, size);

        protected override IRequest<Result<IEnumerable<CategoryAccount5dTreeDto>>> GetTreeQuery()
            => new GetTreeCategoryAccount5dQuery();

        protected override IRequest<Result> RemoveTreeCommand(Guid id)
            => new RemoveTreeCategoryAccount5dCommand(id);
    }
}
