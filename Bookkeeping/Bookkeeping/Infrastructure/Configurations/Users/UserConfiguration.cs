using Bookkeeping.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookkeeping.Infrastructure.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasFilter("\"Email\" IS NOT NULL"); // Синтаксис фильтра для Postgres

            builder.Property(u => u.Email)
                .HasMaxLength(250);

            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique()
                .HasFilter("\"PhoneNumber\" IS NOT NULL");

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.UserType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.UserRole)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(u => u.RefreshToken)
                .HasMaxLength(500);

            builder.Property(u => u.RefreshTokenExpiryTime)
                .IsRequired(false);

            builder.Property(u => u.ConfirmationCode)
                .HasMaxLength(100);

            // Связь 1-к-1: У одного User строго один UserProfile
            builder.HasOne(u => u.Profile)
                   .WithOne(p => p.User)
                   .HasForeignKey<UserProfile>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Удаляем юзера -> удаляется его профиль

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
