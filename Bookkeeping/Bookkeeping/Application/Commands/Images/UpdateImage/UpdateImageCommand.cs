using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.UpdateImage
{
    public record UpdateImageCommand(Guid Id, ImageUpdateDto Dto, IFormFile? File)
        : IRequest<Result<ImageGetDto>>;
}
