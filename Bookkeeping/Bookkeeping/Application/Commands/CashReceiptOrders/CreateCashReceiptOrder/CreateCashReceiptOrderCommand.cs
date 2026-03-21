using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;

namespace Bookkeeping.Application.Commands.CashReceiptOrders.CreateCashReceiptOrder
{
    public record CreateCashReceiptOrderCommand(CashReceiptOrderCreateDto Dto)
        : CreateBaseCommand<CashReceiptOrder, CashReceiptOrderCreateDto, CashReceiptOrderGetDto>(Dto);
}
