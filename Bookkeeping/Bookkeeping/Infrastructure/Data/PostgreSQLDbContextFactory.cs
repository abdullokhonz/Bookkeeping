using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookkeeping.Infrastructure.Data
{
    public class PostgreSQLDbContextFactory : IDesignTimeDbContextFactory<PostgreSQLDbContext>
    {
        public PostgreSQLDbContext CreateDbContext(string[] args)
        {
            // Собираем конфигурацию точно так же, как это делает Program.cs
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DbPostgres");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException(
                    "Connection string 'DbPostgres' not found. Check appsettings.json and environment variables.");

            var optionsBuilder = new DbContextOptionsBuilder<PostgreSQLDbContext>();
            optionsBuilder.UseNpgsql(connectionString)
                          .LogTo(Console.WriteLine, LogLevel.Information)
                          .EnableSensitiveDataLogging(false)
                          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new PostgreSQLDbContext(optionsBuilder.Options);
        }
    }
}
