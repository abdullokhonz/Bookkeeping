using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.CashReceiptOrders
{
    public class Image : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid EntityId { get; set; }
    }
}
