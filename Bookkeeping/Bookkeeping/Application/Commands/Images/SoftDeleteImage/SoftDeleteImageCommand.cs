using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.Images.SoftDeleteImage
{
    public record SoftDeleteImageCommand(Guid Id)
        : SoftDeleteBaseCommand<Image>(Id);
}
