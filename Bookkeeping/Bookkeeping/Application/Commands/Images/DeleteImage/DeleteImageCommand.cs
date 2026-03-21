using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.Images.DeleteImage
{
    public record DeleteImageCommand(Guid Id)
        : DeleteBaseCommand<Image>(Id);
}
