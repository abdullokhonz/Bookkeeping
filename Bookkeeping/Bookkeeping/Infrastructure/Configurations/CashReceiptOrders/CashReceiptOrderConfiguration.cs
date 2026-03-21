using Bookkeeping.Entities.CashReceiptOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.CashReceiptOrders
{
    public class CashReceiptOrderConfiguration : IEntityTypeConfiguration<CashReceiptOrder>
    {
        public void Configure(EntityTypeBuilder<CashReceiptOrder> builder)
        {
            builder.ToTable("CashReceiptOrders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                  .IsRequired()
                  .HasMaxLength(250);

            builder.Property(x => x.Description)
                  .HasMaxLength(500);

            builder.Property(x => x.DocumentNumber)
                  .IsRequired()
                  .HasMaxLength(50);
            builder.HasIndex(x => new { x.DocumentYear, x.SequenceNumber })
                  .IsUnique();

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Accountant)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Cashier)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(x => x.DebitIfrsAccount)
                .WithMany()
                .HasForeignKey(x => x.DebitIfrsAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreditIfrsAccount)
                .WithMany()
                .HasForeignKey(x => x.CreditIfrsAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.IncomeCategory)
                .WithMany()
                .HasForeignKey(x => x.IncomeCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReferenceBook)
                .WithMany()
                .HasForeignKey(x => x.ReferenceBookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.VatTax)
                .WithMany()
                .HasForeignKey(x => x.VatTaxId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
