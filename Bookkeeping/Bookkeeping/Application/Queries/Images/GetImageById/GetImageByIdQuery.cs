using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.Images.GetImageById
{
    public record GetImageByIdQuery(Guid Id)
        : GetBaseByIdQuery<Image, ImageGetDto>(Id);
}
