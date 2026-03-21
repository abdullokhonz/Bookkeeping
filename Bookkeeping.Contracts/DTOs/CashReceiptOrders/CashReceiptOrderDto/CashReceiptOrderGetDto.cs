using Bookkeeping.Contracts.Enums;

namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto
{
    public class CashReceiptOrderGetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DocumentNumber { get; set; } = string.Empty;

        public DocumentStatus Status { get; set; }

        public DateTime OperationDate { get; set; }

        public decimal Amount { get; set; }

        public Guid DebitIfrsAccountId { get; set; }

        public Guid CreditIfrsAccountId { get; set; }

        public Guid IncomeCategoryId { get; set; }

        public Guid ReferenceBookId { get; set; }

        public Guid? VatTaxId { get; set; }

        public string Accountant { get; set; } = string.Empty;
        public string Cashier { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
