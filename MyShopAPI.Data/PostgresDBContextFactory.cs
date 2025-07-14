using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyShopAPI.Data
{
    public class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDatabaseContext>
    {
        public PostgresDatabaseContext CreateDbContext(string[] args)
        {
            // Build configuration manually
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PostgresDatabaseContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new PostgresDatabaseContext(optionsBuilder.Options);
        }
    }
}
