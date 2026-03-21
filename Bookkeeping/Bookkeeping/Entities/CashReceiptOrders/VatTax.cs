using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.CashReceiptOrders
{
    public class VatTax : BaseEntity
    {
        public decimal VatRate { get; set; }

        public string? Description { get; set; }
    }
}
