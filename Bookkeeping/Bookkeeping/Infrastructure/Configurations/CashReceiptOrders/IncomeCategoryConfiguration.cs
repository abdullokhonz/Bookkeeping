using Bookkeeping.Entities.CashReceiptOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.CashReceiptOrders
{
    public class IncomeCategoryConfiguration : IEntityTypeConfiguration<IncomeCategory>
    {
        public void Configure(EntityTypeBuilder<IncomeCategory> builder)
        {
            builder.ToTable("IncomeCategories");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasOne(p => p.IfrsAccount)
                  .WithOne()
                  .HasForeignKey<IncomeCategory>(p => p.IfrsAccountId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
