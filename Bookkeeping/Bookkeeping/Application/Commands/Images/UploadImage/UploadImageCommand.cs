using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.UploadImage
{
    public record UploadImageCommand(ImageCreateDto Dto, IFormFile File)
        : IRequest<Result<ImageGetDto>>;
}
