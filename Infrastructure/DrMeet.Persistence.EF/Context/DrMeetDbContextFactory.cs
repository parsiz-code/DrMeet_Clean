using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DrMeet.Persistence.EF.Context;

public class DrMeetDbContextFactory : IDesignTimeDbContextFactory<DrMeetDbContext>
{
    public DrMeetDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory()) // Or adjust path
           .AddJsonFile("appsettings.json")
           .Build();

        var connectionString = configuration.GetConnectionString("DrMeetConnection");

        var optionsBuilder = new DbContextOptionsBuilder<DrMeetDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DrMeetDbContext(optionsBuilder.Options);
    }
}
