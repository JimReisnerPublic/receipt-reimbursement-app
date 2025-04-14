
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReceiptReimbursement.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Find the path to the Web API project
            // This assumes a standard directory structure where the Data project and Web API project 
            // are sibling directories under the solution folder
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ReceiptReimbursementApi"));

            // If that doesn't work, you might need to adjust the relative path
            if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
            {
                // Try going up one more directory level (if you're running from the Data project's bin directory)
                basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "ReceiptReimbursementApi"));
            }

            // Build configuration from the appsettings.json in the Web API project
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
