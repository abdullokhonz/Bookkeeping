using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.Base;

namespace Bookkeeping.Services.Interfaces.CashReceiptOrders
{
    public interface IImageService : IBaseService<Image>
    {
        Task<Result<Image>> UploadImageAsync(ImageCreateDto dto, IFormFile file, CancellationToken ct = default);

        Task<Result> UpdateImageAsync(Guid id, ImageUpdateDto entity, IFormFile? file, CancellationToken ct = default);
    }
}
