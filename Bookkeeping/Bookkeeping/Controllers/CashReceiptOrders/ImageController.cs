using Bookkeeping.Application.Commands.Images.DeleteImage;
using Bookkeeping.Application.Commands.Images.SoftDeleteImage;
using Bookkeeping.Application.Commands.Images.UpdateImage;
using Bookkeeping.Application.Commands.Images.UploadImage;
using Bookkeeping.Application.Queries.Images.GetAllImage;
using Bookkeeping.Application.Queries.Images.GetImageById;
using Bookkeeping.Application.Queries.Images.GetPagedImage;
using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
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
    public class ImageController
        : BaseController<Image, ImageGetDto, ImageCreateDto, ImageUpdateDto>
    {
        public ImageController(IMediator mediator, ILogger<ImageController> logger) : base(mediator, logger) { }

        protected override IRequest<Result<IEnumerable<ImageGetDto>>> GetAllQuery()
            => new GetAllImageQuery();

        protected override IRequest<Result<ImageGetDto>> GetByIdQuery(Guid id)
            => new GetImageByIdQuery(id);

        protected override IRequest<Result> SoftDeleteCommand(Guid id)
            => new SoftDeleteImageCommand(id);

        protected override IRequest<Result> DeleteCommand(Guid id)
            => new DeleteImageCommand(id);

        protected override IRequest<Result<PagedList<ImageGetDto>>> GetPagedQuery(int page, int size)
            => new GetPagedImageQuery(page, size);

        // Заглушки
        protected override IRequest<Result<ImageGetDto>> CreateCommand(ImageCreateDto dto) =>
            throw new NotSupportedException();
        protected override IRequest<Result> UpdateCommand(Guid id, ImageUpdateDto dto) =>
            throw new NotSupportedException();

        [NonAction]
        public override Task<ActionResult<ApiResponse<ImageGetDto>>> Create([FromBody] ImageCreateDto dto, CancellationToken ct) =>
            throw new NotSupportedException();

        [NonAction]
        public override Task<ActionResult<ApiResponse<ImageGetDto>>> Update(Guid id, [FromBody] ImageUpdateDto dto, CancellationToken ct) =>
            throw new NotSupportedException();


        [HttpPost("Upload")]
        public async Task<ActionResult<ApiResponse<ImageGetDto>>> Upload([FromForm] ImageCreateDto dto, IFormFile file, CancellationToken ct)
        {
            _logger.LogInformation("Создание Image");

            if (file == null || file.Length == 0)
                return BadRequest("Файл не предоставлен."); // Или BadRequest(DomainErrors.Image.EmptyFile)

            var result = await _mediator.Send(new UploadImageCommand(dto, file), ct);

            return HandleResult(result, "Запись успешно создана.");
        }

        [HttpPut("Update/{id:guid}")]
        public async Task<ActionResult<ApiResponse<ImageGetDto>>> Update(Guid id, [FromForm] ImageUpdateDto dto, IFormFile? file, CancellationToken ct)
        {
            _logger.LogInformation("Обновление Image {Id} с файлом", id);

            var result = await _mediator.Send(new UpdateImageCommand(id, dto, file), ct);

            return HandleResult(result, "Обновление прошло успешно.");
        }
    }
}
