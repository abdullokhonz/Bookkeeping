using Bookkeeping.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.Users
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            // Ограничения длины по твоим правилам (250 для имен, 500 для Description)
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.DateOfBirth)
                .IsRequired(false);

            builder.Property(p => p.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Location)
                .HasMaxLength(250);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
