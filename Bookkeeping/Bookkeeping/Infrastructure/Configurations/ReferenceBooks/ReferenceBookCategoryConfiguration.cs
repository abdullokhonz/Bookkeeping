using Bookkeeping.Entities.ReferenceBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.ReferenceBooks
{
    public class ReferenceBookCategoryConfiguration : IEntityTypeConfiguration<ReferenceBookCategory>
    {
        public void Configure(EntityTypeBuilder<ReferenceBookCategory> builder)
        {
            builder.ToTable("ReferenceBookCategories");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasOne(p => p.IfrsAccount)
                  .WithOne()
                  .HasForeignKey<ReferenceBookCategory>(p => p.IfrsAccountId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
