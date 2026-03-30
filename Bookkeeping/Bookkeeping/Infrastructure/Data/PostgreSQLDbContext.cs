using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.Base;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Infrastructure.Data
{
    public class PostgreSQLDbContext : DbContext
    {
        public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryAccount5d> CategoryAccounts5d { get; set; } = null!;

        public DbSet<IfrsAccount> IfrsAccounts { get; set; } = null!;

        public DbSet<IncomeCategory> IncomeCategories { get; set; } = null!;

        public DbSet<VatTax> VatTaxes { get; set; } = null!;

        public DbSet<ReferenceBookCategory> ReferenceBookCategories { get; set; } = null!;

        public DbSet<ReferenceBook> ReferenceBooks { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<CashReceiptOrder> CashReceiptOrders { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseEntity>();

            // Вместо .HasPostgresEnum<>() в OnModelCreating
            // используется .HasConversion<string>() в конфигурациях сущностей

            // Если используете Npgsql.EntityFrameworkCore.PostgreSQL,
            // то можно зарегистрировать enum-ы так:
            /*
            modelBuilder.HasPostgresEnum<AccountNature>("AccountNature");
            modelBuilder.HasPostgresEnum<DocumentStatus>("DocumentStatus");
            modelBuilder.HasPostgresEnum<UserType>("UserType");
            modelBuilder.HasPostgresEnum<UserRole>("UserRole");
            modelBuilder.HasPostgresEnum<UserGender>("UserGender");
            */

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSQLDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                // Если soft delete — можно тут же установить DeletedAt
                if (entry.State == EntityState.Deleted)
                {
                    // Но лучше не физически удалять, а использовать флаг, 
                    // тогда ставить state в Modified и делать IsDeleted = true в сервисе
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
