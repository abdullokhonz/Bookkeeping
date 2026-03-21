using Bookkeeping.Contracts.Enums;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.Base;
using Bookkeeping.Entities.ReferenceBooks;

namespace Bookkeeping.Entities.CashReceiptOrders
{
    public class CashReceiptOrder : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string DocumentNumber { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public int DocumentYear { get; set; }

        public DateTime OperationDate { get; set; }

        public decimal Amount { get; set; }

        public DocumentStatus Status { get; set; }

        public Guid DebitIfrsAccountId { get; set; }
        public IfrsAccount? DebitIfrsAccount { get; set; }

        public Guid CreditIfrsAccountId { get; set; }
        public IfrsAccount? CreditIfrsAccount { get; set; }

        public Guid IncomeCategoryId { get; set; }
        public IncomeCategory? IncomeCategory { get; set; }

        public Guid ReferenceBookId { get; set; }
        public ReferenceBook? ReferenceBook { get; set; }

        public Guid? VatTaxId { get; set; }
        public VatTax? VatTax { get; set; }

        public string Accountant { get; set; } = string.Empty;
        public string Cashier { get; set; } = string.Empty;
    }
}
