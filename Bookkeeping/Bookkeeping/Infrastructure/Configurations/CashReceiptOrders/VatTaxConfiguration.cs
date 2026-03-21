using Bookkeeping.Entities.CashReceiptOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.CashReceiptOrders
{
    public class VatTaxConfiguration : IEntityTypeConfiguration<VatTax>
    {
        public void Configure(EntityTypeBuilder<VatTax> builder)
        {
            builder.ToTable("VatTaxes");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.VatRate)
                .IsUnique();

            builder.Property(p => p.VatRate)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
