using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookkeeping.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController<TEntity, TGetDto, TCreateDto, TUpdateDto> : ControllerBase
        where TEntity : class
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<BaseController<TEntity, TGetDto, TCreateDto, TUpdateDto>> _logger;

        protected BaseController(IMediator mediator, ILogger<BaseController<TEntity, TGetDto, TCreateDto, TUpdateDto>> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected abstract IRequest<Result<IEnumerable<TGetDto>>> GetAllQuery();
        protected abstract IRequest<Result<TGetDto>> GetByIdQuery(Guid id);
        protected abstract IRequest<Result<TGetDto>> CreateCommand(TCreateDto dto);
        protected abstract IRequest<Result> UpdateCommand(Guid id, TUpdateDto dto);
        protected abstract IRequest<Result> SoftDeleteCommand(Guid id);
        protected abstract IRequest<Result> DeleteCommand(Guid id);
        protected abstract IRequest<Result<PagedList<TGetDto>>> GetPagedQuery(int page, int size);

        [HttpGet("GetAll")]
        public virtual async Task<ActionResult<ApiResponse<IEnumerable<TGetDto>>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(GetAllQuery(), ct);
            return HandleResult(result, "Данные успешно получены.");
        }

        [HttpGet("GetById/{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<TGetDto>>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(GetByIdQuery(id), ct);
            return HandleResult(result, "Запись найдена.");
        }

        [HttpPost("Create")]
        public virtual async Task<ActionResult<ApiResponse<TGetDto>>> Create([FromBody] TCreateDto dto, CancellationToken ct)
        {
            if (dto == null) return BadRequest(ApiResponse<object>.Fail("Тело запроса пустое", "General.EmptyBody"));

            var result = await _mediator.Send(CreateCommand(dto), ct);
            return HandleResult(result, "Запись успешно создана.");
        }

        [HttpPut("Update/{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<TGetDto>>> Update(Guid id, [FromBody] TUpdateDto dto, CancellationToken ct)
        {
            var result = await _mediator.Send(UpdateCommand(id, dto), ct);
            return HandleResult(result, "Обновление прошло успешно.");
        }

        [HttpDelete("SoftDelete/{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<object>>> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(SoftDeleteCommand(id), ct);
            return HandleResult(result, "Запись успешно перемещена в корзину.");
        }

        [HttpDelete("HardDelete/{id:guid}/permanent")]
        public virtual async Task<ActionResult<ApiResponse<object>>> HardDelete(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(DeleteCommand(id), ct);
            return HandleResult(result, "Запись полностью удалена.");
        }

        [HttpGet("GetPaged")]
        public virtual async Task<ActionResult<ApiResponse<IReadOnlyList<TGetDto>>>> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10, CancellationToken ct = default)
        {
            var result = await _mediator.Send(GetPagedQuery(page, size), ct);
            return HandleResult(result, "Страница данных получена.");
        }

        // ============================
        // ОБНОВЛЕННЫЕ HANDLE RESULT (теперь возвращают типы для Swagger)
        // ============================

        protected ActionResult HandleResult<T>(Result<T> result, string successMessage)
        {
            if (result.IsSuccess)
            {
                if (result.Value is PagedList<TGetDto> pagedList)
                {
                    var metadata = new PaginationMetadata(
                        pagedList.TotalCount, pagedList.PageSize, pagedList.Page, pagedList.TotalPages);

                    return Ok(ApiResponse<IReadOnlyList<TGetDto>>.Success(pagedList.Items, successMessage, metadata));
                }

                return Ok(ApiResponse<T>.Success(result.Value, successMessage));
            }

            return MapErrorToResponse(result.Error);
        }

        protected ActionResult HandleResult(Result result, string successMessage)
        {
            if (result.IsSuccess)
                return Ok(ApiResponse<object>.Success(null!, successMessage));

            return MapErrorToResponse(result.Error);
        }

        private ActionResult MapErrorToResponse(Error error)
        {
            var response = ApiResponse<object>.Fail(error.Message, error.Code);

            return error.Code switch
            {
                var c when c.EndsWith("NotFound") => NotFound(response),
                var c when c.EndsWith("Validation") => BadRequest(response),
                var c when c.EndsWith("AlreadyExists") => Conflict(response),
                _ => BadRequest(response)
            };
        }
    }
}
