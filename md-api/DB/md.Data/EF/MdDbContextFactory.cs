using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace md.Data.EF
{
    public class MdDbContextFactory : IDesignTimeDbContextFactory<MdDbContext>
    {
        public MdDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("mdDb");

            var optionsBuilder = new DbContextOptionsBuilder<MdDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MdDbContext(optionsBuilder.Options);
        }
    }
}
