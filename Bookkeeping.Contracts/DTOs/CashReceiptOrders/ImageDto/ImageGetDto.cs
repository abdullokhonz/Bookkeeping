namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto
{
    public class ImageGetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid EntityId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
