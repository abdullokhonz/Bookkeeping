using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.CashReceiptOrders
{
    public class IncomeCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid IfrsAccountId { get; set; }
        public IfrsAccount? IfrsAccount { get; set; }
    }
}
