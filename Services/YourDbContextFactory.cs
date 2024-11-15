using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HL7APIProject.Services
{
    public class YourDbContextFactory : IDesignTimeDbContextFactory<YourDbContext>
    {
        public YourDbContext CreateDbContext(string[] args)
        {
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            
            var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
            optionsBuilder.UseMySql(
                configuration.GetConnectionString("YourDatabaseConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("YourDatabaseConnection"))
            );

            return new YourDbContext(optionsBuilder.Options);
        }
    }
}
