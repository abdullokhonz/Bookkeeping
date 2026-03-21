namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto
{
    public class VatTaxGetDto
    {
        public Guid Id { get; set; }

        public decimal VatRate { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
