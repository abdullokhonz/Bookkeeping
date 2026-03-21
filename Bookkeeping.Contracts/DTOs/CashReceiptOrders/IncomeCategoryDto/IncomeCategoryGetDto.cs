namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto
{
    public class IncomeCategoryGetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid IfrsAccountId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
