using Bookkeeping.Entities.CashReceiptOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.CashReceiptOrders
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Path)
                      .IsRequired();

            builder.Property(p => p.EntityId)
                  .IsRequired();

            builder.HasIndex(p => new { p.EntityId, p.IsDeleted });

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
