using Bookkeeping.Entities.Accounts5d;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.Accounts5d
{
    public class IfrsAccountConfiguration : IEntityTypeConfiguration<IfrsAccount>
    {
        public void Configure(EntityTypeBuilder<IfrsAccount> builder)
        {
            builder.ToTable("IfrsAccounts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AccountNumber)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(p => p.AccountName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CategoryAccount)
                .WithMany()
                .HasForeignKey(p => p.CategoryAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.AccountNature)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
