using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReiRexhajEpSolution.DataAccess.Context
{
    public class PollDbContextFactory : IDesignTimeDbContextFactory<PollDbContext>
    {
        public PollDbContext CreateDbContext(string[] args)
        {
            // Get the path to the Presentation project (startup project)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ReiRexhajEpSolution.Presentation");

            // Load configuration from appsettings.json in the Presentation project
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<PollDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new PollDbContext(optionsBuilder.Options);
        }
    }
}