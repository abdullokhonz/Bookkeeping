using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Queries.Images.GetPagedImage
{
    public record GetPagedImageQuery(int Page, int Size)
        : GetPagedBaseQuery<Image, ImageGetDto>(Page, Size);
}
