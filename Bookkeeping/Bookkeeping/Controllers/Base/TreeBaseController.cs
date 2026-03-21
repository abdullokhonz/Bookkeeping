using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class TreeBaseController<TEntity, TGetDto, TCreateDto, TUpdateDto>
        : BaseController<TEntity, TGetDto, TCreateDto, TUpdateDto>
        where TEntity : class
    {
        protected TreeBaseController(
            IMediator mediator,
            ILogger<TreeBaseController<TEntity, TGetDto, TCreateDto, TUpdateDto>> logger)
            : base(mediator, logger)
        {

        }

        protected abstract IRequest<Result<IEnumerable<TGetDto>>> GetTreeQuery();

        protected abstract IRequest<Result> RemoveTreeCommand(Guid id);

        [HttpGet("tree")]
        public virtual async Task<ActionResult<ApiResponse<IEnumerable<TGetDto>>>> GetTree(CancellationToken ct)
        {
            var result = await _mediator.Send(GetTreeQuery(), ct);
            return HandleResult(result, "Дерево сущностей получено.");
        }

        [HttpDelete("recursive/{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<object>>> DeleteRecursive(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(RemoveTreeCommand(id), ct);

            return HandleResult(result, "Запись полностью рекурсивно удалена из системы.");
        }
    }
}
