using Bookkeeping.Entities.ReferenceBooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.ReferenceBooks
{
    public class ReferenceBookConfiguration : IEntityTypeConfiguration<ReferenceBook>
    {
        public void Configure(EntityTypeBuilder<ReferenceBook> builder)
        {
            builder.ToTable("ReferenceBooks");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasOne(p => p.ReferenceBookCategory)
                .WithMany()
                .HasForeignKey(p => p.ReferenceBookCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.SubIfrsAccount)
                 .WithOne()
                 .HasForeignKey<ReferenceBook>(p => p.SubIfrsAccountId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Info)
                .HasColumnType("jsonb");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
