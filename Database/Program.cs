
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Database {
    public class Program: IDesignTimeDbContextFactory<DatabaseContext> {
        public DatabaseContext CreateDbContext(string[] args) {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
                .UseNpgsql(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}