using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.Images.GetAllImage
{
    public record GetAllImageQuery()
        : GetAllBaseQuery<Image, ImageGetDto>;
}
