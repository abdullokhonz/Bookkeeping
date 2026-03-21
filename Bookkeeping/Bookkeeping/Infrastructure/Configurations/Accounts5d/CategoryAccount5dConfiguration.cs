using Bookkeeping.Entities.Accounts5d;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.Accounts5d
{
    public class CategoryAccount5dConfiguration : IEntityTypeConfiguration<CategoryAccount5d>
    {
        public void Configure(EntityTypeBuilder<CategoryAccount5d> builder)
        {
            builder.ToTable("CategoryAccounts5d");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
